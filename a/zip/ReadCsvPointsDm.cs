using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

[TransactionAttribute(TransactionMode.Manual)]
[RegenerationAttribute(RegenerationOption.Manual)]
public class Lab1PlaceGroup : IExternalCommand
{
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements)
    {
 
        //Get application and document objects
        Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;
        UIApplication uiApp = commandData.Application;
        Document doc = uiApp.ActiveUIDocument.Document;

        StreamReader readFile = new StreamReader(@"c:\Users\dmcm029\Documents\Visual Studio 2010\Projects\points.csv");

        TaskDialog.Show("Revit", "Read csv!");

        string line;

      

        //Define a Reference object to accept the pick result.
        Reference pickedRef = null;

        TaskDialog.Show("Revit", "Select your marker");

        //Pick a group
        Selection sel = uiApp.ActiveUIDocument.Selection;
        pickedRef = sel.PickObject(ObjectType.Element, "Please select a group");
        Element elem = pickedRef.Element;
        Group group = elem as Group;

      
        //Pick a point
      //  XYZ point = sel.PickPoint("Please pick a point to place group");
        
      



        //Place the group
  Transaction trans = new Transaction(doc);
        trans.Start("Lab");
  TaskDialog.Show("Revit", "Going to start adding snags");
        while ((line = readFile.ReadLine()) != null)
        {

            string[] data = line.Split(',');
          
         

            XYZ xyz = app.Create.NewXYZ(Convert.ToDouble(data[0]), Convert.ToDouble(data[1]), Convert.ToDouble(data[2]));
           // TaskDialog.Show("Revit", (data[0]) + "," + (data[1]) + "," + (data[2]));
            // ReferencePoint rp = doc.FamilyCreate.NewReferencePoint(xyz);

            
            doc.Create.PlaceGroup(xyz, group.GroupType);

        }


      
      //  for (int i = 0; i < 4; i++)
      //  {   
      //      XYZ point1 = new XYZ(x,y,z);
      //      doc.Create.PlaceGroup(point, group.GroupType);
      //      doc.Create.PlaceGroup(point1, group.GroupType);
      //      x += 10;
      //  }
         trans.Commit();

        return Result.Succeeded;
    }
}