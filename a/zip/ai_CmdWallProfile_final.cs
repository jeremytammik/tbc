#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
using BuildingCoder;

#endregion

namespace WallProfileForBuildingCoder
{
    [Transaction(TransactionMode.Manual)]
    public class CmdWallProfile : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            View view = doc.ActiveView;

            Autodesk.Revit.Creation.Application creapp
              = app.Create;

            Autodesk.Revit.Creation.Document credoc
              = doc.Create;

            Reference r = uidoc.Selection.PickObject(
              ObjectType.Element, "Select a wall");

            Element e = uidoc.Document.GetElement(r);

            Creator creator = new Creator(doc);

            Wall wall = e as Wall;
            if (wall == null)
                return Result.Cancelled;

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Wall Profile");

                // Get the external wall face for the profile
                // a little bit simpler than in the last realization

                Reference sideFaceReference = HostObjectUtils.GetSideFaces(wall, ShellLayerType.Exterior).First();

                Face face = (Face)wall.GetGeometryObjectFromReference(sideFaceReference);

                // The normal of the wall external face.

                XYZ normal = wall.Orientation;

                // Offset curve copies for visibility.

                Transform offset = Transform.CreateTranslation(
                  5 * normal);

                // If the curve loop direction is counter-
                // clockwise, change its color to RED.

                Color colorRed = new Color(255, 0, 0);

                // Get edge loops as curve loops.

                IList<CurveLoop> curveLoops
                  = face.GetEdgesAsCurveLoops();

                foreach (var curveLoop in curveLoops)
                {
                    CurveArray curves = creapp.NewCurveArray();

                    foreach (Curve curve in curveLoop)
                        curves.Append(curve.CreateTransformed(offset));

                    var isCounterClockwize = curveLoop.IsCounterclockwise(normal);

                    // Create model lines for an curve loop if it is made 

                    if (((LocationCurve) wall.Location).Curve is Line)
                    {
                        Plane plane = creapp.NewPlane(curves);

                        SketchPlane sketchPlane
                            = SketchPlane.Create(doc, plane);

                        ModelCurveArray curveElements
                            = credoc.NewModelCurveArray(curves,
                                                        sketchPlane);

                        if (isCounterClockwize)
                            SetModelCurvesColor(curveElements, view, colorRed);
                    }
                    else
                        foreach (var curve in curves.Cast<Curve>())
                        {
                            var curveElements = creator.CreateModelCurves(curve);
                            if (isCounterClockwize)
                                SetModelCurvesColor(curveElements, view, colorRed);
                        }
                }

                tx.Commit();
            }

            return Result.Succeeded;
        }

        private void SetModelCurvesColor(ModelCurveArray modelCurve, View view, Color color)
        {
            foreach (var curve in modelCurve.Cast<ModelCurve>())
            {
                var overrides = view.GetElementOverrides(curve.Id);

                overrides.SetProjectionLineColor(color);

                view.SetElementOverrides(curve.Id, overrides);
            }
        }
    }
}
