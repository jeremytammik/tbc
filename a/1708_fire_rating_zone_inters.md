<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- [Automatic Creation of Void Extrusion Element/ Retrieve Cut Area from element](https://forums.autodesk.com/t5/revit-api-forum/automatic-creation-of-void-extrusion-element-retrieve-cut-area/m-p/8451742)

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash; 
...

-->

### Fire Rating Zone Intersection

Last week, we looked at some 3D intersection filtering tasks.

Now, let's tackle a 2D intersection one, brought up and solved 
by [Jack Bird](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/6830764) in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [automatic creation of void extrusion and retrieval of cut area from element](https://forums.autodesk.com/t5/revit-api-forum/automatic-creation-of-void-extrusion-element-retrieve-cut-area/m-p/8451742):

Actually, it was originally raised a 3D intersection task.

Reducing it to 2D makes it much more tractable:


#### <a name="3"></a> 3D Solid Extrusion Creation and Intersection Task

**Question:** I’m finding it difficult to find any informative material on a subject in the API that I was hoping might have already been explored.

Specifically I was hoping there would be an instance where a “model in place” void element is programmatically created in the model using given parameters for shape and dimensions.

For my purposes I was hoping to find a way to use an existing Solid Extrusion Element’s Geometry to define the geometry of this void as I create it.

The overall point would be to:

- Have a Transparent extrusion hidden in the model that represents the Property Boundary for the Site of the Building, and the area 900 mm in from it (like a 3D cubic Object that represents the legal fire rating zone).
- Create a list of elements from the exterior of the building that need to be tested for needing a fire proof layer; (such as Walls, Soffits ETC).

Then in the same transaction:

- create a void Extrusion element the same shape and size as the Solid extrusion,
- Iterate through each of the elements in need of testing and attempt to cut them with the void,
- if the cut is successful record the cut area of the element that is being tested,
- Keep that as the area of that element that needs to be fire rated,
- Then, having tested all the relevant elements, Rolling back the transaction.

I Don’t Know if that is even possible, but I thought it might allow me to automatically store the required fire rated area for each element. All the drafts person has to do is draw in a Solid extrusion 900 mm wide and as tall as the building based on the plan of subdivision and hide it in the model.

I would be really Stoked for any advice, references or links that seem relevant to the task. it would be much appreciated.

Here is an image that may help with visualizing the problem:

<center>
<img src="img/fire_rating_zone_inters.png" alt="Fire rating zone intersection and subdivision" width="1104">
</center>

**Answer:** Here are some discussions dealing wit various aspects of voids:

http://thebuildingcoder.typepad.com/blog/2011/06/boolean-operations-and-instancevoidcututils.html

http://thebuildingcoder.typepad.com/blog/2010/07/beam-maker-using-a-void-extrusion-to-cut.html

http://thebuildingcoder.typepad.com/blog/2014/04/instancevoidcututils-and-need-for-regeneration.html

http://thebuildingcoder.typepad.com/blog/2014/10/brussels-hackathon-and-determining-pipe-wall-thickn...

http://thebuildingcoder.typepad.com/blog/2017/03/wta-mech-and-ttt-for-provision-for-voids.html

http://thebuildingcoder.typepad.com/blog/2017/06/findinserts-determines-void-instances-cutting-a-flo...


#### <a name="4"></a> 2D Area Intersection Task

Looking at your descriptive image, the problem seems quite simple to me.

You have a certain area of interest, and certain elements that partially intersect the volume above it.

Instead of determining the intersecting volume, I would suggest retaining the area of interest in 2D, in the XY plane, and projecting the elements onto the XY plane as well.

Once you have everything in 2D in the XY plane, you have reduced the problem to a simple 2D Boolean intersection task.

That can be perfectly and completely addressed using a library to
compute [Boolean Operations for 2D Polygons](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.2).

**Response:** That  RvtClipper.zip  was a very useful resource thank you. i had a play with the clipperlib on a previous task but did not consider putting it to use here.

I am currently working on adapting it to my aim and will update this post as soon as i think I've figured it out.

**Answer:** Great! I am very glad it helped. I think this can be used to create a very optimal solution to the ask you describe. I look forward very much to hearing what you come up with.

**Response:** Thanks again, that worked out really well.

In the end I swapped out the RvtClipper Result Execute() method to be a List<CurveArray> Execute() Method,

and substituted the doc.selection based component for 2  element parameters that i pass when i call it.

(Being the boundary element and whichever element I am currently testing for an intersection as i iterate through them)

I then Loop through the resulting CurveArrays that it returns and:

- Subject the Curve Arrays to Your SortCurvesContiguous() Method
- Convert them to CurveLoops,
- Check if the CurveLoop is Open,
- If it is; I take the start of the 1st Curve and the end of the last Curve and add a new Curve to the loop using these vertices as its start and end,
- Next I Convert the loop back to the Array,
- I then Apply your SortCurvesContiguous() Method again just to be sure,
- Then lastly just create a floor of a previously prepared type named "Fire Rated Layer" using these Curves that sits just on top of the soffit that was being tested.

All in all, I'm Chuffed with the outcome.

All the best and Thank You,

I would be glad to share the code and sample; I wonder though;

as i have utilized this function as a component of a bigger two part method - and have to some degree intertwined the two functions, would you still like to have it.

furthermore, i have developed it to be reliant on several other methods so it may seem somewhat verbose as a Sample.

With all this taken into account, i wasn't sure if you would like to retract your offer as the sample will be quite large and not entirely focused on the topics touched on in this conversation.

I will outline the whole thing so you may consider.

The overall method now takes all of the soffits of a specific type (Named "Eave") and:

1. spends some time Breaking them into segments that replace the initial elements.

Then

2. Collects and subjects the new elements to the boundary overlap Query, creating the fire rating layer elements.

Step one not being entirely relevant to step 2 but contingent non the less.

Having Gone back through and doing a bit of house keeping, its not as bad as i had apprehended.

please forgive my habit of notating via regions, i have a bit of a thing for abstraction whilst i work.

it is all attached below.


#### <a name="5"></a> Jack's Sample and Description

Jack shared
a [complete soffit subdivision and fire rating sample](zip/jb_soffit_subdivision_and_fire_rating.zip) including
a [sample model](zip/jb_soffit_subdivision_and_fire_rating_example.rvt), [sample C# code](#4) and the following description:

The notation within the sample will hopefully take care of most of the describing,

I would however like to make note of the fact that any time I use the term 'Soffit', it is because that is what 
the element in question represents as far as function goes. Where in reality the 'Soffits' are all floors.

The three types that are used in this sample are a floor named "Boundary", a floor named "Eave" and a floor named
"Fire Rated Layer". They have no particular properties that are essential to the method other than their names, 
as that is how I identify and collect them.

Other than that, the view in the model called "Isolated" is just the "Soffit" and the boundary by themselves;
that is the best veiw to test in, I find.

All the best.

Thanks.

#### <a name="6"></a> Soffit Subdivision Sample Code

<pre class="code">
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Microsoft.Win32;
using System.Windows.Forms;
using AddPanel.Classes;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Analysis;
using System.Diagnostics;
using ClipperLib;
using Polygon = System.Collections.Generic.List<ClipperLib.IntPoint>;
using Polygons = System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>;
using Microsoft.JScript.Vsa;

namespace AddPanel.Classes
{
  class Example
  {
    private void SubDivideSoffits_CreateFireRatedLayers(ExternalCommandData revit, Document doc)
    {
      try
      {
        #region Get Soffits
        List<Element> Soffits = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Floors).ToElements().Where(m => !(m is ElementType)).ToList();

        #endregion

        //Subdivide
        foreach (Element Soffit in Soffits.Where(m => m.Name.ToLower().Contains("eave")))
        {
          #region Get Soffit Geometry
          Options ops = new Options();
          ops.DetailLevel = ViewDetailLevel.Fine;
          ops.IncludeNonVisibleObjects = true;
          GeometryElement Geo = Soffit.get_Geometry(ops);

          #endregion

          foreach (var item in Geo)
          {
            if (item is Solid)
            {
              #region Get one of the Main Faces, it doesn't really matter if it is top or bottom
              Solid GSol = item as Solid;
              List<Face> Fs = new List<Face>();
              foreach (Face f in GSol.Faces)
              {
                Fs.Add(f);
              }
              Face F = Fs.Where(m => m.Area == Fs.Max(a => a.Area)).First();
              #endregion

              #region Triangulate the Face with max detail
              Mesh M = F.Triangulate(1);
              #endregion

              #region Create Variables for: the curves that will define the new Soffits, List of Custom Triangle Class, List of Custom Pair of Triangle Class
              List<List<Curve>> LLC = new List<List<Curve>>();
              List<Triangle> Triangles = new List<Triangle>();
              List<TrianglePair> TPairs = new List<TrianglePair>();

              #endregion

              #region Loop Through Triangles & Add Them to the list of My Triangle Class
              for (int i = 0; i < M.NumTriangles; i++)
              {
                List<Curve> LC = new List<Curve>();

                #region Make List of Curves From Triangle
                MeshTriangle MT = M.get_Triangle(i);
                List<Curve> Curves = new List<Curve>();
                Curve C = Line.CreateBound(MT.get_Vertex(0), MT.get_Vertex(1)) as Curve;
                Curves.Add(C);
                C = Line.CreateBound(MT.get_Vertex(1), MT.get_Vertex(2)) as Curve;
                Curves.Add(C);
                C = Line.CreateBound(MT.get_Vertex(2), MT.get_Vertex(0)) as Curve;
                Curves.Add(C);
                #endregion

                Triangle T = new Triangle();
                T.Sides = new List<Curve>();
                T.Sides = Curves;

                T.Vertices = new List<XYZ>();
                T.Vertices.Add(MT.get_Vertex(0));
                T.Vertices.Add(MT.get_Vertex(1));
                T.Vertices.Add(MT.get_Vertex(2));
                Triangles.Add(T);
              }
              #endregion

              #region Loop Through Triangles And Create Trapezoid Pairs To Catch The Segments, Getting Rid of The Shared sides
              bool GO = true;
              do
              {
                Triangle TKeeper1 = new Triangle();
                Triangle TKeeper2 = new Triangle();

                foreach (Triangle T in Triangles)
                {
                  TKeeper1 = new Triangle();
                  foreach (Triangle T2 in Triangles)
                  {
                    TKeeper2 = new Triangle();
                    if (T != T2)
                    {
                      if (FindCurvesFacing(T, T2) != null)
                      {
                        if (FindCurvesFacing(T, T2)[0].Length == T.Sides.Min(c => c.Length) ||
                          FindCurvesFacing(T, T2)[1].Length == T2.Sides.Min(c => c.Length))
                        {
                          continue;
                        }
                        Curve[] Cs = FindCurvesFacing(T, T2);
                        T.Sides.Remove(Cs[0]);
                        T2.Sides.Remove(Cs[1]);
                        if (T.Sides.Count() == 2 && T2.Sides.Count() == 2)
                        {
                          TKeeper1 = T;
                          TKeeper2 = T2;
                          goto ADDANDGOROUND;
                        }
                      }
                    }
                  }
                }
                GO = false;
                ADDANDGOROUND:
                if (GO)
                {
                  Triangles.Remove(TKeeper1);
                  Triangles.Remove(TKeeper2);
                  TrianglePair TP = new TrianglePair();
                  TP.T1 = TKeeper1;
                  TP.T2 = TKeeper2;
                  TPairs.Add(TP);
                }
              } while (GO);

              #endregion

              #region Create Curve Loops From Triangle Pairs
              foreach (TrianglePair TPair in TPairs)
              {
                List<Curve> Cs = new List<Curve>();

                Cs.AddRange(TPair.T1.Sides);
                Cs.AddRange(TPair.T2.Sides);

                LLC.Add(Cs);
              }
              #endregion

              double Offset = Convert.ToDouble(Soffit.LookupParameter("Height Offset From Level").AsValueString());
              FloorType FT = (Soffit as Floor).FloorType;
              Level Lvl = doc.GetElement((Soffit as Floor).LevelId) as Level;

              #region Delete Old Soffit If All Went Well
              using (Transaction T = new Transaction(doc, "Delete Soffit"))
              {
                T.Start();
                try
                {
                  doc.Delete(Soffit.Id);
                }
                catch (Exception ex)
                {
                  T.RollBack();
                }
                doc.Regenerate();
                T.Commit();
              }
              #endregion

              #region Sort The Lists of Curves and Create The New Segments
              foreach (List<Curve> LC in LLC)
              {
                List<Curve> LCSorted = new List<Curve>();
                try
                {
                  LCSorted = SortCurvesContiguous(LC, false);
                }

                #region Exception Details if Curves Could not be sorted
                catch (Exception EXC)
                {
                  string exmsge = EXC.Message;
                }

                #endregion

                CurveArray CA = new CurveArray();
                foreach (Curve C in LCSorted)
                {
                  CA.Append(C);
                }

                using (Transaction T = new Transaction(doc, "Make Segment"))
                {
                  T.Start();
                  try
                  {
                    Floor newFloor = doc.Create.NewFloor(CA, FT, Lvl, false);

                    newFloor.LookupParameter("Height Offset From Level").SetValueString(Offset.ToString());
                  }
                  catch (Exception ex)
                  {
                    T.RollBack();
                  }
                  doc.Regenerate();
                  T.Commit();
                }

              }
              #endregion
            }
          }
        }
        //refresh collection
        Soffits = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Floors).ToElements().Where(m => !(m is ElementType)).ToList();
        //test soffits for needing fire rating
        foreach (Element Soffit in Soffits.Where(m => m.Name.ToLower().Contains("eave")))
        {
          #region Get Soffit Geometry
          Options ops = new Options();
          ops.DetailLevel = ViewDetailLevel.Fine;
          ops.IncludeNonVisibleObjects = true;
          GeometryElement Geo = Soffit.get_Geometry(ops);

          #endregion

          foreach (var item in Geo)
          {
            if (item is Solid)
            {
              #region Find boundary Void Element
              List<Element> MaybeBoundary = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Floors).ToElements().Where(m => !(m is ElementType)).ToList();
              Element BoundryElement = MaybeBoundary.Where(m => !(m is FloorType) && m.Name == "Boundary").First();

              #endregion

              #region Get Intersection of Boundary and eave
              PolygonAnyliser com = new PolygonAnyliser();
              string RefString = "";
              List<CurveArray> CArray = com.Execute(revit, ref RefString, BoundryElement as Floor, Soffit as Floor);

              Level L = doc.GetElement(Soffit.LevelId) as Level;

              #endregion

              foreach (CurveArray CA in CArray)
              {
                #region Sort The Curves 
                IList<Curve> CAL = new List<Curve>();
                foreach (Curve C in CA)
                {
                  CAL.Add(C);
                }

                List<Curve> Curves = SortCurvesContiguous(CAL, false);
                List<XYZ> NewCurveEnds = new List<XYZ>();

                #endregion

                #region Close the loop if nesesary
                CurveLoop CL = new CurveLoop();
                int a = 0;
                foreach (Curve curv in Curves)
                {
                  CL.Append(curv);
                }
                if (CL.IsOpen())
                {
                  Curves.Add(Line.CreateBound(CL.First().GetEndPoint(0), CL.Last().GetEndPoint(1)) as Curve);
                }
                #endregion

                #region Recreate a Curve Array
                Curves = SortCurvesContiguous(Curves, false);

                CurveArray CA2 = new CurveArray();

                int i = 0;
                foreach (Curve c in Curves)
                {
                  CA2.Insert(c, i);
                  i += 1;
                }

                #endregion

                #region Create The New Fire Rated Layer element
                FloorType ft = new FilteredElementCollector(doc).WhereElementIsElementType().OfCategory(BuiltInCategory.OST_Floors).ToElements().Where(m => m.Name == "Fire Rated Layer").First() as FloorType;
                Transaction T = new Transaction(doc, "Fire Rated Layer Creation");
                try
                {
                  T.Start();
                  Floor F = doc.Create.NewFloor(CA2, ft, L, false);
                  string s = Soffit.LookupParameter("Height Offset From Level").AsValueString();
                  double si = Convert.ToDouble(s);
                  si = si + (Convert.ToDouble(Soffit.LookupParameter("Thickness").AsValueString()));
                  F.LookupParameter("Height Offset From Level").SetValueString(si.ToString());
                  T.Commit();
                }
                catch (Exception EX)
                {
                  T.RollBack();
                  string EXmsg = EX.Message;
                }

                #endregion
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        string mesg = ex.Message;
      }
    }

    //T0 will get it's curve that faces T1 as index 0 and T1 get it's curve that faces T0 as index 1
    Curve[] FindCurvesFacing(Triangle T0, Triangle T1)
    {
      Curve[] FacingCurves = null;
      XYZ outerVert0 = new XYZ();
      XYZ outerVert1 = new XYZ();
      int workedforvertice = 0;
      int workedforLine = 0;
      foreach (XYZ T0vertice in T0.Vertices)
      {
        foreach (XYZ T1Vertice in T1.Vertices)
        {
          if (T0vertice.IsAlmostEqualTo(T1Vertice))
          {
            continue;
          }
          if (T0.Sides.Where(m => m.GetEndPoint(0).IsAlmostEqualTo(T0vertice) && m.GetEndPoint(1).IsAlmostEqualTo(T1Vertice)).Count() == 0
            && T0.Sides.Where(m => m.GetEndPoint(1).IsAlmostEqualTo(T0vertice) && m.GetEndPoint(0).IsAlmostEqualTo(T1Vertice)).Count() == 0)
          {
            outerVert0 = T0vertice;
            outerVert1 = T1Vertice;
            workedforvertice += 1;
          }
          else
          {
            workedforLine += 1;
          }
        }
        if (workedforvertice == 1 && workedforLine == 2)
        {
          break;
        }
        else
        {
          workedforvertice = 0;
          workedforLine = 0;
        }
      }

      if (workedforvertice == 1 && workedforLine == 2)
      {
        FacingCurves = new Curve[2];

        Curve Hyp0 = null;
        Curve Hyp1 = null;

        foreach (Curve side in T0.Sides)
        {
          List<XYZ> ends = new List<XYZ>();
          ends.Add(side.GetEndPoint(0));
          ends.Add(side.GetEndPoint(1));

          if (ends.Where(m => m.IsAlmostEqualTo(outerVert0)).Count() == 0)
          {
            Hyp0 = side;
            break;
          }
        }
        foreach (Curve side in T1.Sides)
        {
          List<XYZ> ends = new List<XYZ>();
          ends.Add(side.GetEndPoint(0));
          ends.Add(side.GetEndPoint(1));

          if (ends.Where(m => m.IsAlmostEqualTo(outerVert1)).Count() == 0)
          {
            Hyp1 = side;
            break;
          }
        }

        FacingCurves[0] = Hyp0;
        FacingCurves[1] = Hyp1;

      }

      return FacingCurves;
    }

    const double _inch = 1.0 / 12.0;
    const double _sixteenth = _inch / 16.0;
    static Curve CreateReversedCurve(

    Curve orig)
    {

      if (orig is Line || orig is Curve)
      {
        return Line.CreateBound(
          orig.GetEndPoint(1),
          orig.GetEndPoint(0));
      }
      else
      {
        throw new Exception(
          "CreateReversedCurve - Unreachable");
      }
    }
    public List<Curve> SortCurvesContiguous(
    IList<Curve> curves,
    bool debug_output)
    {
      int n = curves.Count;

      // Walk through each curve (after the first) 
      // to match up the curves in order

      for (int i = 0; i < n; ++i)
      {
        Curve curve = curves[i];
        XYZ endPoint = curve.GetEndPoint(1);

        if (debug_output)
        {
          Debug.Print("{0} endPoint {1}", i,
            Util.PointString(endPoint));
        }

        XYZ p;

        // Find curve with start point = end point

        bool found = (i + 1 >= n);

        for (int j = i + 1; j < n; ++j)
        {
          p = curves[j].GetEndPoint(0);

          // If there is a match end->start, 
          // this is the next curve

          if (_sixteenth > p.DistanceTo(endPoint))
          {
            if (debug_output)
            {
              Debug.Print(
                "{0} start point, swap with {1}",
                j, i + 1);
            }

            if (i + 1 != j)
            {
              Curve tmp = curves[i + 1];
              curves[i + 1] = curves[j];
              curves[j] = tmp;
            }
            found = true;
            break;
          }

          p = curves[j].GetEndPoint(1);

          // If there is a match end->end, 
          // reverse the next curve

          if (_sixteenth > p.DistanceTo(endPoint))
          {
            if (i + 1 == j)
            {
              if (debug_output)
              {
                Debug.Print(
                  "{0} end point, reverse {1}",
                  j, i + 1);
              }

              curves[i + 1] = CreateReversedCurve(
                 curves[j]);
            }
            else
            {
              if (debug_output)
              {
                Debug.Print(
                  "{0} end point, swap with reverse {1}",
                  j, i + 1);
              }

              Curve tmp = curves[i + 1];
              curves[i + 1] = CreateReversedCurve(
                 curves[j]);
              curves[j] = tmp;
            }
            found = true;
            break;
          }
        }
        if (!found)
        {
          throw new Exception("SortCurvesContiguous:"
            + " non-contiguous input curves");
        }
      }
      return curves.ToList();

    }

    public class PolygonAnyliser
    {
      //Nearly Entirely the Work of The Builder Coder

      /// <summary>
      /// Consider a Revit length zero 
      /// if is smaller than this.
      /// </summary>
      const double _eps = 1.0e-9;

      /// <summary>
      /// Conversion factor from feet to millimetres.
      /// </summary>
      const double _feet_to_mm = 25.4 * 12;

      /// <summary>
      /// Conversion a given length value 
      /// from feet to millimetres.
      /// </summary>
      static long ConvertFeetToMillimetres(double d)
      {
        if (0 < d)
        {
          return _eps > d
            ? 0
            : (long)(_feet_to_mm * d + 0.5);

        }
        else
        {
          return _eps > -d
            ? 0
            : (long)(_feet_to_mm * d - 0.5);

        }
      }

      /// <summary>
      /// Conversion a given length value 
      /// from millimetres to feet.
      /// </summary>
      static double ConvertMillimetresToFeet(long d)
      {
        return d / _feet_to_mm;
      }

      /// <summary>
      /// Return a clipper integer point 
      /// from a Revit model space one.
      /// Do so by dropping the Z coordinate
      /// and converting from imperial feet 
      /// to millimetres.
      /// </summary>
      public IntPoint GetIntPoint(XYZ p)
      {
        return new IntPoint(
          ConvertFeetToMillimetres(p.X),
          ConvertFeetToMillimetres(p.Y));
      }

      /// <summary>
      /// Return a Revit model space point 
      /// from a clipper integer one.
      /// Do so by adding a zero Z coordinate
      /// and converting from millimetres to
      /// imperial feet.
      /// </summary>
      public XYZ GetXyzPoint(IntPoint p)
      {
        return new XYZ(
          ConvertMillimetresToFeet(p.X),
          ConvertMillimetresToFeet(p.Y),
          0.0);
      }

      /// <summary>
      /// Retrieve the boundary loops of the given slab 
      /// top face, which is assumed to be horizontal.
      /// </summary>
      Polygons GetBoundaryLoops(CeilingAndFloor slab)
      {
        int n;
        Polygons polys = null;
        Document doc = slab.Document;
        Autodesk.Revit.ApplicationServices.Application app = doc.Application;

        Options opt = app.Create.NewGeometryOptions();

        GeometryElement geo = slab.get_Geometry(opt);

        foreach (GeometryObject obj in geo)
        {
          Solid solid = obj as Solid;
          if (null != solid)
          {
            foreach (Face face in solid.Faces)
            {
              PlanarFace pf = face as PlanarFace;
              if (null != pf
                && pf.FaceNormal.IsAlmostEqualTo(XYZ.BasisZ))
              {
                EdgeArrayArray loops = pf.EdgeLoops;

                n = loops.Size;
                polys = new Polygons(n);

                foreach (EdgeArray loop in loops)
                {
                  n = loop.Size;
                  Polygon poly = new Polygon(n);

                  foreach (Edge edge in loop)
                  {
                    IList<XYZ> pts = edge.Tessellate();

                    n = pts.Count;

                    foreach (XYZ p in pts)
                    {
                      poly.Add(GetIntPoint(p));
                    }
                  }
                  polys.Add(poly);
                }
              }
            }
          }
        }
        return polys;
      }

      public List<CurveArray> Execute(
        ExternalCommandData commandData,
        ref string message, Floor boundary, Floor eave)
      {
        List<CurveArray> Results = new List<CurveArray>();

        UIApplication uiapp = commandData.Application;
        UIDocument uidoc = uiapp.ActiveUIDocument;
        Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
        Document doc = uidoc.Document;

        // Two slabs to intersect.

        CeilingAndFloor[] slab
          = new CeilingAndFloor[2] { eave, boundary };

        // Retrieve the two slabs' boundary loops

        Polygons subj = GetBoundaryLoops(slab[0]);
        Polygons clip = GetBoundaryLoops(slab[1]);

        // Calculate the intersection

        Polygons intersection = new Polygons();

        Clipper c = new Clipper();

        c.AddPolygons(subj, PolyType.ptSubject);

        c.AddPolygons(clip, PolyType.ptClip);

        c.Execute(ClipType.ctIntersection, intersection,
          PolyFillType.pftEvenOdd, PolyFillType.pftEvenOdd);

        // Check for a valid intersection

        if (0 < intersection.Count)
        {

          foreach (Polygon poly in intersection)
          {

            CurveArray curves = app.Create.NewCurveArray();
            IntPoint? p0 = null; // first
            IntPoint? p = null; // previous

            foreach (IntPoint q in poly)
            {
              if (null == p0)
              {
                p0 = q;
              }
              if (null != p)
              {
                curves.Append(
                  Line.CreateBound(
                    GetXyzPoint(p.Value),
                    GetXyzPoint(q)));
              }
              p = q;
            }

            Results.Add(curves);
          }

        }
        return Results;
      }
    }
  }
}
</pre>

