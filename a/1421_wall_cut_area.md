<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Determining Wall Cut Area for a Specific Room #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim

We looked at calculating gross and net wall areas last year, with a later enhancement to use IFCExportUtils to determine the door and window area, resulting in the two respective projects and GitHub repositories, SpatialElementGeometryCalculator and ExporterIfcUtilsWinArea. Several developers have been busy expanding on those to determine surface areas of subfaces, for instance for openings in walls spanning multiple rooms...

-->

### Determining Wall Cut Area for a Specific Room

We looked
at [calculating gross and net wall areas](http://thebuildingcoder.typepad.com/blog/2015/03/calculating-gross-and-net-wall-areas.html) last
year, with a later enhancement to
use [IFCExportUtils to determine the door and window area](http://thebuildingcoder.typepad.com/blog/2015/03/ifcexportutils-methods-determine-door-and-window-area.html),
resulting in the two respective projects and GitHub repositories:

- [SpatialElementGeometryCalculator](https://github.com/jeremytammik/SpatialElementGeometryCalculator)
- [ExporterIfcUtilsWinArea](https://github.com/jeremytammik/ExporterIfcUtilsWinArea)

Several developers have been busy expanding on those to determine surface areas of subfaces, for instance for openings in walls spanning multiple rooms.

These two project demonstrate useful and powerful starting points but obviously do not yet satisfy all needs.

Here are some additional aspects brought up by [Arif](#2), [Miroslav](#3) and [Håvard](#4):


#### <a name="2"></a>Arif

Arif Hanif explored the use the spatial geometry calculator and noted in
a [comment on FindInserts retrieving openings](http://thebuildingcoder.typepad.com/blog/2015/03/findinserts-retrieves-all-openings-in-all-wall-types.html#comment-2424158731) that
it **does** in fact return the subface linked instance element ids. You have to set the `SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Center` to close the geometry, and it does not work for Finish.

More significantly, he explored the subface issue in
a [comment on getting the wall elevation profile](http://thebuildingcoder.typepad.com/blog/2015/01/getting-the-wall-elevation-profile.html#comment-2567466439)
([image](http://thebuildingcoder.typepad.com/blog/2015/01/getting-the-wall-elevation-profile.html#comment-2570111596)):

**Question:** I am working on an interesting problem, which I have been trying to see if anyone else has been solving. I tried searching your blog and Internet (Revit API forum) but did not see it come up. I need to determine the vertices of an embedded wall returned by FindInserts part of a spatial geometry calculator return. I have the edge and curve loops of the wall in the spaces creating the bounding element. I have used `SpatialElementBoundarySubface` to return the bounding element and determined the id of the embedded wall in this case a curtain wall. The issue I am running into is that the geometry of the embedded wall spans multiple spaces and have not figured out how to determine only the portion of the embedded wall that belongs to the spatial element boundary. As I read more seems maybe the reference intersector class might be something I look at, any recommendations on your end would be greatly appreciated.

<center>
<img src="img/room_surface_area_02.png" alt="Subfaces in different spaces" width="400">
</center>

Solve for the shaded curtain wall vertices.

**Answer:** It looks to me as if one way to go would be to project all the relevant elements onto the wall plane to convert the problem to a two-dimensional one, and then use 2D Boolean operations on the partial surfaces to determine their intersections and overlap.

Look at The Building Coder topic group on [2D Booleans and Adjacent Areas](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.2).


#### <a name="3"></a>Miroslav

Miroslav Schonauer adds:

I’ve been playing with `SpatialElementGeometryCalculator` and `SpatialElementGeometryResults` in order to retrieve room bounding surfaces for a project to apply finishes.

I’m successfully getting subfaces which include the originating element ids (at least for the 'vertical' room faces from walls.
My aim is to get the *net* wall face geometries rather than *gross* which I understand is by-design in the `SpatialElementBoundary*` classes.

I was hoping that `SpatialElementBoundarySubface.GetBoundingElementFace` may get me to the source element face, but unfortunately it does not and that also seems by design. The help file remarks say that 'Faces do not contain voids in room-bounding elements (such the voids in walls created by doors and windows'. It gives me the same or typically just a slightly bigger gross Face that goes a bit 'beyond' the room bounding inside shell due to the wall joints

What would be my best bet to get the geometry of the room bounding parts of walls same as `SpatialElementBoundarySubface` but excluding the openings in the walls?

In other words, I need a combination of 'net-ness' of the original wall inside face(s) (which are net as needed, but extend beyond the room inside shell) and the 'room-belong-ness' of `SpatialElementBoundarySubface` (which are exact in terms of room inside shell, but unfortunately gross excluding the openings).

Afaik, there is no built-in Revit API functionality to get what I want.

A solution for *planar* faces can be implemented but requires some complex geometrical manipulation based on the full wall geometry and the subfaces obtainable via `SpatialElement*`.


#### <a name="4"></a>Håvard

Håvard Dagsvik explored this issue further and implemented solutions to address several of these needs after raising the issue in
a [comment on calculating gross and net wall areas](http://thebuildingcoder.typepad.com/blog/2015/03/calculating-gross-and-net-wall-areas.html#comment-2531680684):

**Question:** Right now I am getting the opening area from built-in height and width parameters.

But it's not ideal, as doors are made in different ways.

In theory the opening could be any kind of family instance. And sometimes it only partially cuts the wall, e.g. in a stacked wall.
It might even extend beyond the room or ceiling as an embedded curtain wall might do, or a standard window for that matter.

<center>
<img src="img/room_surface_area_03.png" alt="Subfaces in different spaces" width="400">
</center>

So the temporary transaction trick won't always get the correct 'spatial' opening area but I had to try anyway.

What sometimes happens is that Revit will throw other warnings, such as 'Geometry no longer defines a plane', as a consequence.
Sometimes not always, depends on the model.

Which the user has to cancel manually, even though the function proceeds probably because we roll back.

I decided not to use a temp transaction.

If you have a WallOpening or a OpeningByFace the spatial area returned already has those subtracted.

Strange this doesn't happen with FamilyInstances.

Anyway the opening area should be available through the EnergyAnalysis API, but then we would have to generate the energy model first.
If gbXML could generate areas at the room face we could do it all there.
But I think gbXML only calculates at the wall centreline. (Fine for opening areas though).

Another method might be to get the actual OpeningCut element in the family.

Some Boolean subtraction trick using transient solids from the actual wall residing in the family document. A bit like the temp transaction only using transient family document geometry. But as I realize just now you would also have to set the correct family symbol active first.

**Answer:** Yes, things are a bit complicated.

If you put in enough effort, I think you can catch and
automatically [handle any kind of warning message that can ever comes up](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.32).

Regarding bits sticking out from the wall, you can always reduce the problem to a much simpler two-dimensional issue by projecting everything onto the wall plane.

The [ExtrusionAnalyzer class](http://thebuildingcoder.typepad.com/blog/2013/04/extrusion-analyser-and-plan-view-boundaries.html) can help with that.

A [2D Boolean library](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.2) might also come in handy.

I assume that the gbXML functionality does use that kind of functionality, since you say it works in the wall centre line.

**Response:** I found that using solid manipulation worked better for me than temporary transactions.

Especially when the opening is an embedded curtain wall spanning multiple rooms and stories.

So I intersect a curtain wall solid with the room solid, where the intersected solid holds the opening area for that specific room.

I also used more of the `IFCUtils` classes from Angel's team, and one of your methods from The Building Coder to retrieve the wall profile.

Modified to include non-visible objects in order
to [get 'invisible' curtain wall geometry](http://thebuildingcoder.typepad.com/blog/2010/05/curtain-wall-geometry.html),
also based on one of your suggestions in another case.

Here is a small example on how I achieve this:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">double</span> GetWallCutArea(
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="teal">FamilyInstance</span> fi,
&nbsp; &nbsp; <span class="teal">Wall</span> wall,
&nbsp; &nbsp; <span class="blue">bool</span> isStacked )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Options</span> optCompRef
&nbsp; &nbsp; &nbsp; = doc.Application.Create.NewGeometryOptions();
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != optCompRef )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; optCompRef.ComputeReferences = <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; optCompRef.DetailLevel = <span class="teal">ViewDetailLevel</span>.Medium;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">SolidHandler</span> solHandler = <span class="blue">new</span> <span class="teal">SolidHandler</span>();
&nbsp; &nbsp; <span class="teal">XYZ</span> cutDir = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( !isStacked )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">CurveLoop</span> curveLoop
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">ExporterIFCUtils</span>.GetInstanceCutoutFromWall(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; fi.Document, wall, fi, <span class="blue">out</span> cutDir );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">CurveLoop</span>&gt; loops = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">CurveLoop</span>&gt;( 1 );
&nbsp; &nbsp; &nbsp; loops.Add( curveLoop );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">ExporterIFCUtils</span>.ComputeAreaOfCurveLoops(
&nbsp; &nbsp; &nbsp; &nbsp; loops );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">else</span> <span class="blue">if</span>( isStacked )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">CurveLoop</span> curveLoop
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">ExporterIFCUtils</span>.GetInstanceCutoutFromWall(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; fi.Document, wall, fi, <span class="blue">out</span> cutDir );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">CurveLoop</span>&gt; loops = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">CurveLoop</span>&gt;( 1 );
&nbsp; &nbsp; &nbsp; loops.Add( curveLoop );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> geomElemHost = wall.get_Geometry(
&nbsp; &nbsp; &nbsp; &nbsp; optCompRef ) <span class="blue">as</span> <span class="teal">GeometryElement</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Solid</span> solidOpening
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">GeometryCreationUtilities</span>.CreateExtrusionGeometry(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; loops, cutDir.Negate(), .1 );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Solid</span> solidHost = solHandler.CreateSolidFromBoundingBox(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">null</span>, geomElemHost.GetBoundingBox(), <span class="blue">null</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( solidHost == <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> 0;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Solid</span> intersectSolid
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">BooleanOperationsUtils</span>.ExecuteBooleanOperation(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; solidOpening, solidHost,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BooleanOperationsType</span>.Intersect );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( intersectSolid.Faces.Size.Equals( 0 ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; solidOpening
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">GeometryCreationUtilities</span>.CreateExtrusionGeometry(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; loops, cutDir, .1 );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; intersectSolid
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">BooleanOperationsUtils</span>.ExecuteBooleanOperation(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; solidOpening, solidHost,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BooleanOperationsType</span>.Intersect );
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="teal">DebugHandler</span>.EnableSolidUtilityVolumes )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; trans.Start( <span class="maroon">&quot;stacked1&quot;</span> );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ShapeCreator</span>.CreateDirectShape( doc,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; intersectSolid, <span class="maroon">&quot;stackedOpening&quot;</span> );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; trans.Commit();
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> solHandler.GetLargestFaceArea( intersectSolid );
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> 0;
&nbsp; }
</pre>

There are other fall-backs implemented as well such as just getting the area from height and width parameters, and another method for handling curtain walls.

No temporary transactions are used at all :-)

Jeremy adds: I started adding Håvard's functionality to
the [SpatialElementGeometryCalculator GitHub project](https://github.com/jeremytammik/SpatialElementGeometryCalculator),
and it is integrated into the current [release 2016.0.0.2](https://github.com/jeremytammik/SpatialElementGeometryCalculator/releases/tag/2016.0.0.2) but still not hooked up and as of yet completely untested in that project.

Håvard is using is successfully in his own project, though.

Please feel free to fork, explore, enhance and issue pull requests back.

Many thanks to everybody for their valuable contributions, especially Håvard for sharing this solution!

Happy weekend to all!