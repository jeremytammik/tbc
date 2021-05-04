<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Convert ParameterType.FixtureUnit to ForgeTypeId
  Revit 2022: ParameterType.Text to ForgeTypeId

- edge loops
  13179537 [Is the first Edgeloop still the outer loop?]
  https://forums.autodesk.com/t5/revit-api-forum/is-the-first-edgeloop-still-the-outer-loop/m-p/7225379
  Is the first Edgeloop still the outer loop?
  https://forums.autodesk.com/t5/revit-api-forum/is-the-first-edgeloop-still-the-outer-loop/m-p/10242847
  /a/rvt/sort_multiple_edge_loops.rvt
  
$ lb is-the-first-edgeloop-still-the-outer-loop
1581_edge_refplane_column.md:2
1592_disjunct_outer_loops.md:2
1904_sort_loops.md:1

$ blmd 1581 1592
<!-- 1581 1592 --> <ul> - [Edge Loop, Point Reference Plane and Column Line](http://thebuildingcoder.typepad.com/blog/2017/08/edge-loop-point-reference-plane-and-column-line.html) == - [Disjunct Planar Face Outer Loops](http://thebuildingcoder.typepad.com/blog/2017/10/disjunct-outer-loops-from-planar-face-with-separate-parts.html) == </ul>

- Revit 2022 – Missing Fabrication Addins (temporary fix)
  https://www.darrenjyoung.com/2021/04/08/revit-2022-missing-fabrication-addins-temporary-fix/
  
twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; ...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Sorting Non-Planar Curve Loops

Very short notice on the *Ask Me Anything* session taking place later today, and a vastly enhanced method for sorting curve loops:



Before getting technical, a personal note:

I celebrated my covid coronation last week.

I suffered very mildly.
My parter had a strong headache and took a positive test.
Next day, felt slightly dizzy, my bones and eyes felt tired and hurt in the evening,
followed by a scratchy throat, slight fever, ache in the legs and back, exhaustion, higher pulse.
I slept more, stopped drinking coffee for five days and otherwise kept to my normal everyday routines and work.
So, I was lucky.

####<a name="2"></a> Today: Inside the Factory, Ask Me Anything

The annual Revit Inside the Factory *Ask Me Anything* session is talking place later today, May the Fourth
at [11:30am PT / 14:30pm ET / 19:30 CET](https://www.timeanddate.com/worldclock/converter.html?iso=20210504T183000&p1=tz_pt&p2=tz_et&p3=tz_cet).

Questions about the future of Revit?
Join for the Public Roadmap and to ask me anything.

- [Revit Public Roadmap](https://trello.com/b/ldRXK9Gw/revit-public-roadmap)
- [AMA YouTube Livestream](https://youtu.be/KNG4PPpKgzM)

<center>
<img src="img/2021-05-04_revit_ama.jpg" alt="Ask me anything" title="Ask me anything" width="300"/> <!-- 900 -->
</center>


####<a name="3"></a> Sorting Non-Planar Curve Loops

Stefano Menci picked up an old solution for sorting curve loops and expanded it to handle non-planar faces by transforming the 3D loop coordinates from the curved face XYZ space to its 2D UV parametrisation space.

The original question was raised in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [is the first Edgeloop still the outer loop?](https://forums.autodesk.com/t5/revit-api-forum/is-the-first-edgeloop-still-the-outer-loop/m-p/10242847),
solved by Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
and edited by The Building Coder in two posts:

- [Determining the outer-most EdgeLoop](https://thebuildingcoder.typepad.com/blog/2017/08/edge-loop-point-reference-plane-and-column-line.html#3)
- [Disjunct planar face outer loops](http://thebuildingcoder.typepad.com/blog/2017/10/disjunct-outer-loops-from-planar-face-with-separate-parts.html) 

Here is a summary of the new conversation with Stefano and Richard:

**Question:** I tried using `SortCurveLoops` on the the face shown below, expecting to get one list with the outer loop and one list with the 3 inner loops, but I got one list containing one list containing two loops.

<center>
<img src="img/2021-05-04_revit_ama.jpg" alt="Ask me anything" title="Ask me anything" width="300"/> <!-- 900 -->
</center>


/a/case/sfdc/13179537/attach/sort_curve_loops_sm_curved_face_1.png

This article also mentions co-planar loops in the description of ValidateCurveLoops. Perhaps the problem I had is caused by the fact that also SortCurveLoops only works with co-planar loops?

Here is the code I use to find the outer loop and all the inner loops. It finds the outer loop by cycling through all the tessellated points of all the loop edges and finding the one with the lowest U.

I did a few tests and it seems to work well. I am surprised to see that my short function does the same job as other long functions shown in the previous posts.  I am learning LINQ, so I spent some time to get this to work with LINQ, but my previous version using 3 nested foreach was very simple to do.

Am I doing something wrong?

Am I doing something different from what this post describes?

public Face Face;
private CurveLoop _outerLoop;
private List<CurveLoop> _innerLoops;

private void GetInnerAndOuterLoops()
{
    var allLoops = Face.GetEdgesAsCurveLoops();

    _outerLoop = allLoops
        .SelectMany(loop => loop
            .SelectMany(curve => curve.Tessellate()
                .Select(point => (Face.Project(point).UVPoint.V, Loop: loop))))
        .Aggregate((lowestLoop, nextLoop) => lowestLoop.V < nextLoop.V ? lowestLoop : nextLoop).Loop;

    _innerLoops = allLoops.Where(loop => loop != _outerLoop).ToList();
}
 
-----------------------------------------------------------------------
RPTHOMAS108
04-14-2021 02:34 PM 

If it works it works.

One thing I considered at the time was resolution of the points you get from tessellate i.e. if you have a curve does one of the points on that curve (from tessellate) describe the actual minimum location of that curve (for some curves that is unlikely to be the case). Since there is the parametric curve and the actual points are obtained from that. Below is an exaggerated example of what I mean by that. Depends also on rotation of UV axis on face in comparison to those points.

210414a.PNG

/a/case/sfdc/13179537/attach/sort_curve_loops_rt_tessellation_resolution.png

For the most part I don't think such a thing would cause issues unless you set out to prove it didn't work, i.e., in a real world scenario, there is no arrangement you would likely have that would be affected by such things. So it's a question of comfort level through testing, really.

I think it has also since been noted that there are patterns in how the faces are constructed that gives away the actual outer loop of a face.

Was also at the time dealing with PlanarFaces only so also should note that for those you can use Face.IsInside with solid creation utils, this makes things far more straightforward than my original above code and perhaps more reliable i.e. extrude each loop and check points from each within faces of one another to find other loop.

-----------------------------------------------------------------------
stefanomenci
04-14-2021 02:51 PM 

The documentation of Curve.Tessellate says both the tolerance is slightly larger than 1/16” and is defined internally by Revit to be adequate for display purposes.

Tessellation in computer graphics is often adjusted to the zoom level or to the desired rendering quality. In other words, if "you can see" that one curve is below the other curve, then the tessellation can see it too. But I don't know if Curve.Tessellation is the same tessellation used for the graphics card. We could also talk about the definition of "you can see", but let's not add speculation to the speculation 🙂

-----------------------------------------------------------------------
RPTHOMAS108
04-14-2021 03:09 PM 

Yes it is unlikely but possible.

-----------------------------------------------------------------------
$ bl 0031 0032 0033 0038 0039 0374  0375 0576 0620 0666 0839 0999 1070 1085 1217 1307 1410
<!-- 0031 0032 0033 0038 0039 0374 0375 0576 0620 0666 0839 0999 1070 1085 1217 1307 1410 -->
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/10/slab-boundary.html">Slab Boundary</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/11/model-line-creation.html">Model Line Creation</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/11/slab-side-faces.html">Slab Side Faces</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-compound-layers.html">Wall Compound Layers</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-elevation-profile.html">Wall Elevation Profile</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/05/curtain-wall-geometry.html">Curtain Wall Geometry</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/05/model-curve-creator.html">Model Curve Creator</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/05/iteration-and-springtime-change-is-the-only-constant.html">Iteration and Springtime &ndash; Change is the Only Constant</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/07/top-faces-of-wall.html">Top Faces of Sloped Wall</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/10/retrieving-detailed-wall-layer-geometry.html">Retrieving Detailed Wall Layer Geometry</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/10/slab-boundary-revisited.html">Slab Boundary Revisited</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/08/generating-a-midcurve-between-two-curve-elements.html">Generating a MidCurve Between Two Curve Elements</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/12/au-day-1-revit-2014-api-class-and-bounding-box-rotation.html">AU Day 1, Revit 2014 API Class and Bounding Box Rotation</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/01/calculating-a-rolling-offset-between-two-pipes.html">Calculating a Rolling Offset Between Two Pipes</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/10/creating-a-sloped-wall.html">Creating a Sloped Wall</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/04/curved-wall-elevation-profile-and-creator-class-update.html">Curved Wall Elevation Profile and Creator Class Update</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/03/index-reloading-curves-distance-and-deleting-printsetup.html">Index, Debug, Curves, Distance, Deleting PrintSetup</a></li>
</ul>

-----------------------------------------------------------------------
jeremy.tammik in reply to: stefanomenci
‎04-15-2021 01:37 AM 

You say you got one list with one list with two loops...

If you are unsure which loop is which, or, in any case, I would highly recommend implementing some little debugging utility functions to display those loops graphically, or it will be very hard to understand what you are getting.

I implemented such stuff in several blog posts using the Creator class:

Slab Boundary
Model Line Creation
Slab Side Faces
Wall Compound Layers
Wall Elevation Profile
Curtain Wall Geometry
Model Curve Creator
Iteration and Springtime – Change is the Only Constant
Top Faces of Sloped Wall
Retrieving Detailed Wall Layer Geometry
Slab Boundary Revisited
Generating a MidCurve Between Two Curve Elements
AU Day 1, Revit 2014 API Class and Bounding Box Rotation
Calculating a Rolling Offset Between Two Pipes
Creating a Sloped Wall
Curved Wall Elevation Profile and Creator Class Update
Index, Debug, Curves, Distance, Deleting PrintSetup

-----------------------------------------------------------------------
stefanomenci in reply to: jeremy.tammik
‎04-15-2021 01:15 PM 
 
Thank you for the list of articles, it will be very helpful in the near future!

This article seems to mention the same problem that I found in SortCurveLoops.

I did another test with SortCurveLoops, and indeed it seems to be working reliably only with planar faces. With curved faces it usually does nothing. Only in one curved face I was able to get 2 out of the 4 loops, but it usually gets none.

In this snapshot you can see two masses. The first one has only planar faces, the second one is a copy of the first one with a void that creates a curved face.

The texts show the first line of each loop with the loop indexes as returned by SortCurveLoops. I like to create texts at 1/3 of each line, so it visually gives an idea of the direction of the loop. Just looking at the texts you immediately understand which loops are clockwise and which ones are counterclockwise.

I have the feeling that SortCurveLoops projects the curves to a plane, then crunches the numbers on the projected curves. If this is the case, then it will never be reliable on curved faces. The correct approach would be to work on the UV coordinates.

stefanomenci_0-1618514863757.png

/a/case/sfdc/13179537/attach/sort_curve_loops_sm_curved_face_2.png

Here is the code I used:

var loops = face.GetEdgesAsCurveLoops();
var sortedLoops = ExporterIFCUtils.SortCurveLoops(loops);
for (var i = 0; i < sortedLoops.Count; i++)
{
    for (var j = 0; j < sortedLoops[i].Count; j++)
    {
        CreateTextNote($"[{i}][{j}]", sortedLoops[i][j].First().Evaluate(0.33, true), doc);
    }
}

TextNote CreateTextNote(string text, XYZ origin, Document doc)
{
    var options = new TextNoteOptions
    {
        HorizontalAlignment = HorizontalTextAlignment.Center,
        VerticalAlignment = VerticalTextAlignment.Middle,
        TypeId = doc.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType)
    };

    return TextNote.Create(doc, doc.ActiveView.Id, origin, text, options);
}

-----------------------------------------------------------------------
RPTHOMAS108
04-15-2021 03:07 PM 

Below is another issue I now recall that you might want to consider:

When face is disjunct (single solid made of two or more parts) you need to be able to identify both outer loops of the face not just the outer loop to the furthest left or right.

Solved previously for this purpose.

210415.PNG

/a/case/sfdc/13179537/attach/sort_curve_loops_rt_disjunct.png

-----------------------------------------------------------------------
stefanomenci
04-15-2021 06:51 PM 

I created a version of SortCurveLoops that converts the XYZ points to UV points, then works on planar loops. I only tested it with a few cases where ExporterIFCUtils.SortCurveLoops fails, and it works well. I will start using it and see if it breaks in the next weeks.

My function takes in input a Face instead of a list of CurveLoops, because all the curves are tessellated and converted to UV. The Face is used for both finding the CurveLoops and converting to UV.

If there are outer loops contained in other outer loops (like in the third snapshot), the innermost loops are first in the resulting list.

stefanomenci_2-1618538325268.png

stefanomenci_1-1618538302000.png

stefanomenci_0-1618538271711.png

/a/case/sfdc/13179537/attach/sort_curve_loops_sm_uv_solution_1_planar.png

/a/case/sfdc/13179537/attach/sort_curve_loops_sm_uv_solution_2_curved.png

/a/case/sfdc/13179537/attach/sort_curve_loops_sm_uv_solution_3_disjunct.png

This is the function:

public static List<List<CurveLoop>> SortCurveLoops(Face face)
{
    var allLoops = face.GetEdgesAsCurveLoops().Select(loop => new CurveLoopUV(loop, face)).ToList();

    var outerLoops = allLoops.Where(loop => loop.IsCounterclockwise).ToList();
    var innerLoops = allLoops.Where(loop => !outerLoops.Contains(loop)).ToList();

    // sort outerLoops putting last the ones that are outside all the preceding loops
    bool somethingHasChanged;
    do
    {
        somethingHasChanged = false;
        for (var i = 1; i < outerLoops.Count(); i++)
        {
            var point = outerLoops[i].StartPointUV;
            var loop = outerLoops[i - 1];
            if (loop.IsPointInside(point) is CurveLoopUV.PointLocation.Inside)
            {
                var tmp = outerLoops[i];
                outerLoops[i] = outerLoops[i - 1];
                outerLoops[i - 1] = tmp;

                somethingHasChanged = true;
            }
        }
    } while (somethingHasChanged);

    var result = new List<List<CurveLoop>>();
    foreach (var outerLoop in outerLoops)
    {
        var list = new List<CurveLoop> {outerLoop.Loop3d};

        for (var i = innerLoops.Count - 1; i >= 0; i--)
        {
            var innerLoop = innerLoops[i];
            if (outerLoops.Count() == 1 // skip testing whether the inner loop is inside the outer loop
                || outerLoop.IsPointInside(innerLoop.StartPointUV) == CurveLoopUV.PointLocation.Inside)
            {
                list.Add(innerLoop.Loop3d);
                innerLoops.RemoveAt(i);
            }
        }

        result.Add(list);
    }

    return result;
}
This is the class CurveLoopUV that converts the curves from 3D XYZ to UV, then to planar XYZ. 

class CurveLoopUV : IEnumerable<Curve>
{
    public enum PointLocation
    {
        Outside,
        OnTheEdge,
        Inside,
    }

    public CurveLoop Loop3d { get; }
    private readonly CurveLoop _loop2d;

    public readonly double MinX, MaxX, MinY, MaxY;

    public CurveLoopUV(CurveLoop curveLoop, Face face)
    {
        Loop3d = curveLoop;
        _loop2d = new CurveLoop();
        
        var points3d = Loop3d.SelectMany(curve => curve.Tessellate().Skip(1));
        var pointsUv = points3d.Select(point3d => face.Project(point3d).UVPoint);
        var points2d = pointsUv.Select(pointUv => new XYZ(pointUv.U, pointUv.V, 0)).ToList();

        MinX = MinY = 1.0e100;
        MaxX = MaxY = -1.0e100;
        var nPoints = points2d.Count();
        for (var i = 0; i < nPoints; i++)
        {
            var p1 = points2d[i];
            var p2 = points2d[(i + 1) % nPoints];
            _loop2d.Append(Line.CreateBound(p1, p2));
            if (p1.X < MinX) MinX = p1.X;
            if (p1.Y < MinY) MinY = p1.Y;
            if (p1.X > MaxX) MaxX = p1.X;
            if (p1.Y > MaxY) MaxY = p1.Y;
        }
    }

    public PointLocation IsPointInside(XYZ point)
    {
        if (point.Y + Eps < MinY || point.Y + Eps > MaxY)
            return PointLocation.Outside;

        if (_loop2d.Any(curve => curve.Distance(point) < Eps))
            return PointLocation.OnTheEdge;

        var line = Line.CreateBound(point, new XYZ(1.0e100, point.Y, 0));
        var nIntersections = _loop2d.Count(edge => edge.Intersect(line) == SetComparisonResult.Overlap);
        return nIntersections % 2 == 1 ? PointLocation.Inside : PointLocation.Outside;
    }

    public bool IsCounterclockwise => _loop2d.IsCounterclockwise(XYZ.BasisZ);

    public XYZ StartPointUV => _loop2d.First().GetEndPoint(0);

    public IEnumerator<Curve> GetEnumerator() => _loop2d.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

-----------------------------------------------------------------------
jeremy.tammik
04-16-2021 01:20 AM 

Wow! Fantastic job! This is real research, with real test cases. I love it. Thank you very much for your work and important results. I'll take a closer look and would probably like to share this on the blog soon.

Cheers,

Jeremy

-----------------------------------------------------------------------
stefanomenci
04-16-2021 01:03 PM 

Here is a little improvement on CurveLoopUV.IsPointInside:

public PointLocation IsPointInside(XYZ point)
{
    // Check if the point is outside of the loop bounding box
    if (point.X - Eps < MinX
        || point.X + Eps > MaxX
        || point.Y - Eps < MinY
        || point.Y + Eps > MaxY)
        return PointLocation.Outside;

    // Check if the point is on the loop
    if (_loop2d.Any(curve => curve.Distance(point) < Eps))
        return PointLocation.OnTheEdge;

    // Count the number of intersections between a line starting from point and going outside
    // of the loop. If the number of intersection is odd, then point is inside the loop.
    // Discard the solutions where the intersection is the edge start point, because these
    // intersections have already been counted when intersecting the end point of the
    // previous segments
    var line = Line.CreateBound(point, new XYZ(MaxX + 1, MaxY + 1, 0));
    var nIntersections = _loop2d
        .Where(edge => edge.Intersect(line) == SetComparisonResult.Overlap)
        .Count(edge => line.Distance(edge.GetEndPoint(0)) > Eps);

    return nIntersections % 2 == 1 ? PointLocation.Inside : PointLocation.Outside;
}

-----------------------------------------------------------------------
jeremy.tammik
04-19-2021 04:33 AM 

Brilliant! Thank you very much for your careful research and nice implementation!

Thank you also for your pull request to The Building Coder Samples:

https://github.com/jeremytammik/the_building_coder_samples/pull/16

I integrated it into release 2021.0.150.25:

https://github.com/jeremytammik/the_building_coder_samples/compare/2021.0.150.24...2021.0.150.25

Now to edit the discussion above into a succinct blog post...

-----------------------------------------------------------------------
stefanomenci
04-19-2021 05:49 AM 

Here is the model I used for testing.

As you can tell, I'm learning how Revit works, I'm sure there are better ways to create faces with multiple nested loops.

multiple loops.rvt

/a/rvt/sort_multiple_edge_loops.rvt

-----------------------------------------------------------------------






<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 1134 -->
</center>

<!--

Revit native project identifier

A very knowledgeable Revit MEP add-in developer, Olli Kattelus of MagiCAD Group, raises a serious question that I would appreciate some guidance on:

I have been studying how to get an unique (Guid) identifier for the project. I know there has been few posts already here and in The Building Coder blog about the subject.

But the bottom line is that I REALLY would like to have a Revit native method, mainly to avoid challenges in the work sharing environment. Below some highlights from my study:

- I have managed to confirm that using a project information element unique id is NOT an option, as several different projects can have identical id (perhaps it comes from the template...?).
- Utilizing the `ExportUtils.GetExportId()` with project information seems to result exactly the same. So probably this method uses the unique id too. Not usable.
- `ExportUtils.GetGBXMLDocumentId()` works somewhat better, and seems pretty good
- `ExporterIFCUtils.CreateProjectLevelGUID()` seems to be best though. Difference to previous is that this method results different id for project that has been detached from the central and saved with Save as (which sound reasonable).

So... I was pretty much already made my decision to use `CreateProjectLevelGUID()`... until I saw compilation warning from R2022 build. It has been deprecated :-(. "Ok, no problem" I though, as there has always been new alternative mentioned in the warning text. In this case it says:

"This function is deprecated in Revit 2022. Please see the IFC open source function `CreateProjectLevelGUID` for examples of how to do this starting in Revit 2022."

So eventually I decided to download the source codes from the "https://github.com/Autodesk/revit-ifc", as that's what it means, right!?

Moreover, I found the method `CreateProjectLevelGUID()` and took a look of the implementation. There's no native Revit mechanism I could utilize:

- Seems that the project information element dos NOT have following parameters by default (even in the R2022 level project): `IfcProjectGuid`, `BuiltInParameter.IFC_PROJECT_GUID`
- The implementation in the comments (refering also to R2022) seems incorrect to me, as a) it is utilizing the `IFCProjectLevelGUIDType` enumeration which is also deprecated. b) moreover creating an `ElementId` from the enumeration value seems quite odd as according to the IL reflection tool, the enumeration values are using default enum values (0,1,2...)

So my questions basically is:

Is there a native method/technique replacing the deprecated `CreateProjectLevelGUID()`, or, what's the best alternative (not necessarily specific to IFC)?

-->
