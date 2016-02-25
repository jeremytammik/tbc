using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit;
using Autodesk.Revit.Geometry;
using Autodesk.Revit.Elements;
using Autodesk.Revit.Spaces;
using Autodesk.Revit.Parameters;
using Autodesk.Revit.Enums;
using Autodesk.Revit.Symbols;
using Autodesk.Revit.Structural.Enums;
using Autodesk.Revit.Structural;
using System.Windows.Forms;
using System.Collections;

using Uwe.Vertieferarbeit.OgreConverter;


namespace RevitToOgre//Uwe.Vertieferarbeit.RevitToOgre
{
	public class RO_Operations : IExternalCommand
	{

		private OC_RevitConversions             ocConvert;
		private RO_Gpc                          gpcOp;
		private Document                        doc;
		private Autodesk.Revit.Geometry.Options geomOption;
		private Autodesk.Revit.Application      application;
		private double                          wallGridU;
		private double                          wallGridV;
		private GridOptions                     gridOps;
		//private Hashtable vertexTable;
        private Hashtable                       cuboidTable;
        private Dictionary<ElementId, Dictionary<ElementId, bool>> adjacentElements;
        private ElementIdSet                    convertedElementIds;

		public IExternalCommand.Result Execute(
		ExternalCommandData commandData,
		ref string message,
		ElementSet elements)
		{
			
			application = commandData.Application;
			geomOption = application.Create.NewGeometryOptions();
			doc = application.ActiveDocument;
			ocConvert = new OC_RevitConversions(doc.Title);
			gpcOp = new RO_Gpc(ocConvert);
			gridOps = new GridOptions();
			//vertexTable = new Hashtable();
            cuboidTable = new Hashtable();
            adjacentElements = new Dictionary<ElementId, Dictionary<ElementId, bool>>();
            convertedElementIds = application.Create.NewElementIdSet();

			using (RO_StartupForm startupForm = new RO_StartupForm(gridOps))
			{
				DialogResult result = startupForm.ShowDialog();
				
				if (result == DialogResult.Cancel)
				{
					return IExternalCommand.Result.Cancelled;
				}
				else if (result == DialogResult.OK)
				{
                    if (null != geomOption)
                    {
                        geomOption.ComputeReferences = true;
                       // geomOption.DetailLevel = Autodesk.Revit.Geometry.Options.DetailLevels.Medium;
                        geomOption.DetailLevel = Autodesk.Revit.Geometry.Options.DetailLevels.Fine;
                        
                    }

					//Problem #1
					List<FamilyInstance> columns = getColumns();
                    foreach (FamilyInstance column in columns)
                    {
                        generateSingleElement(column as Autodesk.Revit.Element);
                    }
					
					//Problem #2
                    List<Autodesk.Revit.Element> stairs = getStairs();
                    foreach (Autodesk.Revit.Element stair in stairs)
                    {
                        generateSingleElement(stair);
                    }
                   

				}
			}
			return IExternalCommand.Result.Succeeded;
		}



      

        private void addVerticesToList(PlanarFace planarFace, ref XYZArray xyzList)
		{
			EdgeArrayArray edgeArrArr = planarFace.EdgeLoops;

			foreach (EdgeArray edgeArr in edgeArrArr)
			{
				
				foreach(Edge edge in edgeArr )
				{
					XYZArray edgeTess = edge.Tessellate();
					foreach (XYZ point in edgeTess)
					{
                        if(!RO_Utils.isInList(point, xyzList))
						    xyzList.Append(point);
                       
					}
				}
			}
		}
       


       


        private List<Autodesk.Revit.Element> getStairs()
        { 
            // Create a Filter to get all the stairs in the document
            List<Autodesk.Revit.Element> stairs = new List<Autodesk.Revit.Element>();
            Autodesk.Revit.Creation.Filter filterCreator = doc.Application.Create.Filter;
            // Stairs are type Element, so we don't need to include derived types
            TypeFilter elementOnlyFilter = filterCreator.NewTypeFilter(typeof(Autodesk.Revit.Element), false);
            CategoryFilter stairsCategoryfilter = filterCreator.NewCategoryFilter(BuiltInCategory.OST_Stairs);
            Filter stairsFilter = filterCreator.NewLogicAndFilter(elementOnlyFilter, stairsCategoryfilter);

            // Apply the filter to the elements in the active document
            ElementIterator iterator = doc.get_Elements(stairsFilter);

            iterator.Reset();
            while (iterator.MoveNext())
            {
                Autodesk.Revit.Element stair = iterator.Current as Autodesk.Revit.Element;

                if (null != stair.ObjectType)
                {
                    stairs.Add(stair);
                }
            }
            return stairs;
        }

      

        private /*CuboidStructure */ void generateSingleElement(Autodesk.Revit.Element ele)
        {

            XYZArray xyzList = new XYZArray();

            XYZ pos = new XYZ();

            Autodesk.Revit.Geometry.Element geomElem = ele.get_Geometry(geomOption);

            if (geomElem != null)
            {
                GeometryObjectArray geoArray = geomElem.Objects;

                foreach (GeometryObject geomObj in geoArray)
                {
                    Solid geomSolid = geomObj as Solid;
                    if (null != geomSolid)
                    {

                        FaceArray faces = geomSolid.Faces;
                        foreach (Face solFace in faces)
                        {

                            PlanarFace planarFace = solFace as PlanarFace;
                            if (null != planarFace)
                            {
                                addVerticesToList(planarFace, ref xyzList);
                                
                                foreach (XYZ pt in xyzList)
                                {
                                    // you need to translate the pt in xyzList using transform.
                                    transform.OfPoint(pt);
                                    // add it to array....

                                }
                                double areaSize = planarFace.Area;
                                
                                XYZ normal = planarFace.Normal;
                                double[] dblNormal = RO_Utils.xyzToDoubleMetre(normal);

                                XYZ origin = planarFace.Origin;
                                Mesh mesh = planarFace.Triangulate();

                                for (int tri = 0; tri < mesh.NumTriangles; tri++)
                                {
                                    MeshTriangle meshTriangle = mesh.get_Triangle(tri);
                                    ArrayList trianglesAsDouble = new ArrayList();

                                    for (int vi = 0; vi < 3; vi++)
                                    {
                                        //convert it with transform.
                                        XYZ vert = meshTriangle.get_Vertex(vi);
                                        vert = transform.OfPoint(vert);
                                        double[] dblVert = RO_Utils.xyzToDoubleMetre(vert);
                                        trianglesAsDouble.Add(dblVert);

                                    }
                      //              ocConvert.addTriangle(dblNormal, trianglesAsDouble);
                                }
                            }
                        }
                    }
                    else if (geomObj is Autodesk.Revit.Geometry.Instance)
                    {
                        Autodesk.Revit.Geometry.Instace geoInstance = geomObj as Autodesk.Revit.Geometry.Instance;
                        Autodesk.Revit.Geometry.Element geoElement = geoInstance.SymbolGeometry;
                        Autodesk.Revit.Geometry.Transform transform = geoInstance.Transform;

                        GeometryObjectArray geoArray = geoElement.Objects;

                        //using your above method to get the geometry.
                        //please don't forget to convert with the transform.
                        foreach (GeometryObject geomObj in geoArray)
                        {
                            Solid geomSolid = geomObj as Solid;
                            if (null != geomSolid)
                            {

                                FaceArray faces = geomSolid.Faces;
                                foreach (Face solFace in faces)
                                {

                                    PlanarFace planarFace = solFace as PlanarFace;
                                    if (null != planarFace)
                                    {
                                        addVerticesToList(planarFace, ref xyzList);
                                        double areaSize = planarFace.Area;

                                        XYZ normal = planarFace.Normal;
                                        double[] dblNormal = RO_Utils.xyzToDoubleMetre(normal);

                                        XYZ origin = planarFace.Origin;
                                        Mesh mesh = planarFace.Triangulate();

                                        for (int tri = 0; tri < mesh.NumTriangles; tri++)
                                        {
                                            MeshTriangle meshTriangle = mesh.get_Triangle(tri);
                                            ArrayList trianglesAsDouble = new ArrayList();

                                            for (int vi = 0; vi < 3; vi++)
                                            {
                                                XYZ vert = meshTriangle.get_Vertex(vi);

                                                double[] dblVert = RO_Utils.xyzToDoubleMetre(vert);
                                                trianglesAsDouble.Add(dblVert);

                                            }
                                            
                                        }
                                    }
                                }
                            }
                        }

                        
                    }
                }
            }

    
            //if (ocConvert.getTriangleCount() != 0)
            //{
            //    double[] position = RO_Utils.xyzToDoubleMetre(pos);
            //    // double[] position = RO_Utils.xyzToDoubleMetre(posPoint);

            //    ocConvert.generateOgreMesh(position, ele.Id.Value + "_" + /*ele.Category.Name*/ ele.ObjectType.Name);
            //}
        }

        private List<FamilyInstance> getColumns()
        {
            List<FamilyInstance> familyInstanceList = new List<FamilyInstance>();
            List<Autodesk.Revit.Element> instances = new List<Autodesk.Revit.Element>();
            doc.get_Elements(typeof(FamilyInstance),
              instances);

            Dictionary<string, Family> families
              = new Dictionary<string, Family>();

            foreach (FamilyInstance inst in instances)
            {
                if (inst.StructuralType.Equals(StructuralType.Column))
                {
                    familyInstanceList.Add(inst);
                }
                if( inst.get_Geometry(geomOption) != null && inst.Category.Name.Equals("Stützen"))
                {
                    familyInstanceList.Add(inst);
                }
                
                Family family = inst.Symbol.Family;
                if (!families.ContainsKey(family.Name))
                {
                    families[family.Name] = family;
                }
            }
            return familyInstanceList;
        }
		
		 private List<FamilyInstance> getColumns_notWorking()
        {//FamilyInstances from this Method don't have anything in common with Columns
            List<FamilyInstance> columns = new List<FamilyInstance>();

            Autodesk.Revit.Creation.Filter filterCreator =
                doc.Application.Create.Filter;
            TypeFilter columnsInstanceFilter =
                filterCreator.NewTypeFilter(typeof(FamilyInstance));
            CategoryFilter columnsCategoryfilter =
                filterCreator.NewCategoryFilter(BuiltInCategory.OST_Columns);
            Filter columnsInstancesFilter =
                filterCreator.NewLogicAndFilter(columnsInstanceFilter, columnsCategoryfilter);
            // Apply the filter to the elements in the active document
            ElementIterator iterator = doc.get_Elements(columnsInstanceFilter);

            iterator.Reset();
            while (iterator.MoveNext())
            {
                FamilyInstance column = iterator.Current as FamilyInstance;
                if (column != null)
                {
                    columns.Add(column);
                }
            }
            return columns;
        }

      
	}
}
