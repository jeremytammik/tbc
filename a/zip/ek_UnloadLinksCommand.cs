using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using System.Windows.Forms;

namespace UnloadRevitLinks
{
    [Transaction(TransactionMode.Manual)]
    public class UnloadLinksCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string filename = "";
            using(var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog.FileName;
                }
            }

            if (string.IsNullOrEmpty(filename))
                return Result.Cancelled;

            var uiApp = commandData.Application;
            var app = uiApp.Application;
            var modelPath = ModelPathUtils.ConvertUserVisiblePathToModelPath(@filename);
            var doc = this.UnloadRevitLinks(modelPath, app);

            string name = System.IO.Path.GetFileNameWithoutExtension(@filename);
            string ext = System.IO.Path.GetExtension(@filename);
            string path = System.IO.Path.GetDirectoryName(@filename);
            string newName = System.IO.Path.Combine(path, $"{name}_unloadLinks{ext}");
            doc.SaveAs(newName);
            doc.Close(false);

            return Result.Succeeded;
        }

        private Document UnloadRevitLinks(ModelPath location, Autodesk.Revit.ApplicationServices.Application app)
        ///  This method will set all Revit links to be unloaded the next time the document at the given location is opened. 
        ///  The TransmissionData for a given document only contains top-level Revit links, not nested links.
        ///  However, nested links will be unloaded if their parent links are unloaded, so this function only needs to look at the document's immediate links. 
        {
            // access transmission data in the given Revit file
            TransmissionData transData = TransmissionData.ReadTransmissionData(location);

            if (transData != null)
            {
                // collect all (immediate) external references in the model
                ICollection<ElementId> externalReferences = transData.GetAllExternalFileReferenceIds();

                // find every reference that is a link
                foreach (ElementId refId in externalReferences)
                {
                    ExternalFileReference extRef = transData.GetLastSavedReferenceData(refId);

                    if (extRef.ExternalFileReferenceType == ExternalFileReferenceType.RevitLink)
                    {
                        // we do not want to change neither the path nor the path-type
                        // we only want the links to be unloaded (shouldLoad = false)
                        transData.SetDesiredReferenceData(refId, extRef.GetPath(), extRef.PathType, false);
                    }
                }

                // make sure the IsTransmitted property is set 
                transData.IsTransmitted = true;

                // modified transmission data must be saved back to the model
                TransmissionData.WriteTransmissionData(location, transData);

                OpenOptions openOptions = new OpenOptions();
                return app.OpenDocumentFile(location, openOptions);
            }
            else
            {
                Autodesk.Revit.UI.TaskDialog.Show("Unload Links", "The document does not have any transmission data");
            }
            return null;
        }
    }
}
