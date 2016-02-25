#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using FloorCopy.Properties;

#endregion

namespace FloorCopy
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        const double _eps = 1.0e-9;

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Reference r;
            try
            {
                r = uidoc.Selection.PickObject(ObjectType.Element,
                    new FloorSelectionFilter(), Resources.SelectFloor);
            }
            catch (Exception)
            {

                return Result.Cancelled;
            }

            var floor = doc.GetElement(r.ElementId) as Floor;

            using (Transaction t =
                new Transaction(doc, Resources.CopyFloor))
            {
                t.Start();

                IList<FloorContainer> newFloors =
                    CopyFloor(floor);

                t.Commit();

                // Sort floors by area

                List<FloorContainer> newFloorsSorted =
                    newFloors.OrderByDescending(fc =>
                        fc.Floor.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED).AsDouble())
                        .ToList();
                
                // The largest of the floors was built from the outer boundary,
                // this is the Floor to keep

                Floor newFloor =
                    newFloorsSorted[0].Floor;
                newFloorsSorted.RemoveAt(0);

                // Prepare the other floors for deletion

                List<ElementId> delIds = 
                    newFloorsSorted
                        .Select(fc => fc.Floor.Id)
                        .ToList();

                // Extract boundaries (those of all smaller floors)
                // for opening creation

                IEnumerable<CurveArray> sourceEdgesForOpenings =
                    newFloorsSorted.Select(fc => fc.Boundary);

                List<Opening> openings = new List<Opening>();

                t.Start("Create openings and delete smaller floors");

                foreach(CurveArray boundary in sourceEdgesForOpenings)
                {
                    var newOpening =
                        doc
                            .Create
                            .NewOpening(
                                newFloor,
                                boundary,
                                true );
                    openings.Add(newOpening);
                }

                doc.Delete(delIds);
                
                // Transform newly created floor

                XYZ translationVector = new XYZ(0, 0, 10);
                ElementTransformUtils.MoveElement(
                    doc,
                    newFloor.Id,
                    translationVector);
                t.Commit();
            }

            return Result.Succeeded;
        }

        // Create a new Floor from each EdgeLoop of the source Floor, 
        // store the new Floor and its corresponding boundary for later re-use.

        private IList<FloorContainer> CopyFloor(Floor sourceFloor)
        {
            var floorGeometryElement =
                sourceFloor.get_Geometry(new Options());

            foreach (var geometryObject in floorGeometryElement)
            {
                var floorSolid =
                    geometryObject as Solid;

                if (floorSolid == null)
                    continue;

                var topFace =
                    GetTopFace(floorSolid);

                if (topFace == null)
                    throw new NotSupportedException(Resources.FloorDoesNotHaveTopFace);

                if (topFace.EdgeLoops.IsEmpty)
                    throw new NotSupportedException(Resources.FloorTopFateDoesNotHaveEdges);

                var newFloorContainers = new List<FloorContainer>();

                foreach(EdgeArray edgeloop in topFace.EdgeLoops) 
                {

                    // create new floor

                    CurveArray floorCurveArray =
                        GetCurveArrayFromEdgeArray(edgeloop);

                    var newFloor =
                        sourceFloor
                            .Document
                            .Create
                            .NewFloor(floorCurveArray, false);

                    newFloorContainers.Add(new FloorContainer(newFloor, floorCurveArray));
                }

                return newFloorContainers;
            }

            return null;
        }

        private CurveArray GetCurveArrayFromEdgeArray(EdgeArray edgeArray)
        {
            CurveArray curveArray =
                new CurveArray();

            foreach (Edge edge in edgeArray)
            {
                var edgeCurve =
                        edge.AsCurve();

                curveArray.Append(edgeCurve);
            }

            return curveArray;
        }


        PlanarFace GetTopFace(Solid solid)
        {
            PlanarFace topFace = null;
            FaceArray faces = solid.Faces;
            foreach (Face f in faces)
            {
                PlanarFace pf = f as PlanarFace;
                if (null != pf
                  && (Math.Abs(pf.Normal.X - 0) < _eps && Math.Abs(pf.Normal.Y - 0) < _eps))
                {
                    if ((null == topFace)
                      || (topFace.Origin.Z < pf.Origin.Z))
                    {
                        topFace = pf;
                    }
                }
            }
            return topFace;
        }
    }

    struct FloorContainer
    {
        public Floor Floor;
        public CurveArray Boundary;
        public FloorContainer(Floor floor, CurveArray boundary)
        {
            Floor = floor;
            Boundary = boundary;
        }
    }
    public class FloorSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem is Floor;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
}
