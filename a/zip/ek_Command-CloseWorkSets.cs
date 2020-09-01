using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using System.Windows.Forms;

namespace CloseWorksets
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
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
            var modelPath = ModelPathUtils.ConvertUserVisiblePathToModelPath(@filename);
            this.CloseAllWorkSets(modelPath, uiApp);

            return Result.Succeeded;
        }

        private void CloseAllWorkSets(ModelPath location, UIApplication uiApplication)
        {
            var openConfig = new WorksetConfiguration(WorksetConfigurationOption.CloseAllWorksets);

            var openOptions = new OpenOptions();
            openOptions.Audit = false;
            openOptions.SetOpenWorksetsConfiguration(openConfig);
            openOptions.DetachFromCentralOption = DetachFromCentralOption.DetachAndPreserveWorksets;
            uiApplication.OpenAndActivateDocument(location, openOptions, false);
        }
    }
}
