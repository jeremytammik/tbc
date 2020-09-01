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
    public class CloseWorksetsCommand : IExternalCommand
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
            var doc = this.CloseAllWorkSets(modelPath, app);

            string name = System.IO.Path.GetFileNameWithoutExtension(@filename);
            string ext = System.IO.Path.GetExtension(@filename);
            string path = System.IO.Path.GetDirectoryName(@filename);
            string newName = System.IO.Path.Combine(path, $"{name}_detached{ext}");
            var saveAsOptions = new SaveAsOptions();
            var worksharingOptions = new WorksharingSaveAsOptions();
            worksharingOptions.SaveAsCentral = true;
            saveAsOptions.SetWorksharingOptions(worksharingOptions);
            doc.SaveAs(newName, saveAsOptions);
            doc.Close(false);

            return Result.Succeeded;
        }

        private Document CloseAllWorkSets(ModelPath location, Autodesk.Revit.ApplicationServices.Application app)
        {
            var openConfig = new WorksetConfiguration(WorksetConfigurationOption.CloseAllWorksets);

            var openOptions = new OpenOptions();
            openOptions.Audit = false;
            openOptions.SetOpenWorksetsConfiguration(openConfig);
            openOptions.DetachFromCentralOption = DetachFromCentralOption.DetachAndPreserveWorksets;
            return app.OpenDocumentFile(location, openOptions);
        }
    }
}
