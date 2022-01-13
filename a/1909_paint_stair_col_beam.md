<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Forge accelerators: join now or wait for longer
  break during july august september
  have you been thinking about joining to get started efficiently?
  now's the time!
  Upcoming Forge Virtual Accelerator - Thuvaraiyam Pathi - June 21-25, 2021
  https://www.eventbrite.com/e/autodesk-virtual-forge-accelerator-thuvaraiyam-pathi-june-21-25-2021-registration-138643130335
  Benefit from dedicated time to develop your Forge application – with direct help and training from Forge engineering experts.
  Real time expert help dramatically shortening your learning and dev time – and create trusting relationships with Forge experts at Autodesk.
  Submit your proposal today!
  https://twitter.com/AfroJme/status/1402684098687143942?s=20
  [Enjoying an early summer quiet period? Attend next week’s Forge Accelerator!](https://www.keanw.com/2021/06/enjoying-an-early-summer-quiet-period-attend-next-weeks-forge-accelerator.html)
  https://autodesk.slack.com/archives/C0PLC20PP/p1623259873052400

- paint stairs
  [Paint Stair Faces](https://forums.autodesk.com/t5/revit-api-forum/paint-stair-faces/m-p/10388359)
  solved by Bruce [@canyon.des[(https://forums.autodesk.com/t5/user/viewprofilepage/user-id/10032309) Hans
  [Q] Is there any reason why stair faces can be painted in the UI, and not in the API? The question was raised in 2015 and apparently still exists:
  I'm trying to paint some of the faces of a stair (a monolithic stair) through the 2015 revit API. It appears that it cannot be done programmatically (i get an error message that "the element faces cannot be painted") even if i can do this stair face painting manually in revit. Is there a way to paint automatically those faces? An excerpt of my code below. 
  [A] For stairs, you need to use `GetStairsLandings` and `GetStairsRuns` to get the `ElementId` to paint landings or runs. It's not intuitive but works. It's the same to find out whether the landing or run faces are painted or not.
  Thx

- find beams intersecting columns using ray tracing versus the column location line, Face.IsInside and bounding box intersection
  Ray Projection Not Picking Up Beams
  https://forums.autodesk.com/t5/revit-api-forum/ray-projection-not-picking-up-beams/m-p/10388868

twitter:

add #thebuildingcoder

Painting stairs and shooting for the beams with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/paintstair

Two Revit API discussion forum threads that I am currently involved in
&ndash; Painting stairs
&ndash; Ray tracing vs bounding box to find beams intersecting columns...

linkedin:

Painting stairs and shooting for the beams with the #RevitAPI

https://autode.sk/paintstair

Two Revit API discussion forum threads that I am currently involved in:

- Painting stairs
- Ray tracing vs bounding box to find beams intersecting columns...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Question:** 

**Answer:** 

Many thanks to  for this very helpful explanation!

-->

### Painting Stairs and Shooting for the Beams

Here are 
two [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) threads
that I am currently quite involved in:

- [Painting stairs](#2)
- [Ray tracing vs bounding box to find beams intersecting columns](#3)

<center>
<img src="img/painted_stair_in_seoul_south_korea.jpg" alt="Painted stair" title="Painted stair" width="430"/> <!-- 860 -->
</center>

<!--

oops... too late...

####<a name="0"></a> Join Forge Accelerator Now or Wait

Have you been thinking about joining
a [Forge accelerator](https://forge.autodesk.com/accelerator-program) sometime soon to get an efficient start
with [Autodesk Forge](https://forge.autodesk.com/) to access design and engineering data in the cloud?

Now's the time!

The next one coming up
is [Thuvaraiyam Pathi, June 21-25, 2021 &ndash; registration](https://www.eventbrite.com/e/autodesk-virtual-forge-accelerator-thuvaraiyam-pathi-june-21-25-2021-registration-138643130335)

It is followed by a break during July, August and possibly September.

In the Forge accelerator, you can benefit from dedicated time to develop your Forge application with direct help and training from Forge engineering experts.
Real time expert help can dramatically shorten your learning and dev time and create trusting relationships with Forge experts at Autodesk.

Submit your proposal today!

You can also check further invitations
from [Jaime](https://twitter.com/AfroJme/status/1402684098687143942)
and [Kean](https://www.keanw.com/2021/06/enjoying-an-early-summer-quiet-period-attend-next-weeks-forge-accelerator.html)

-->

####<a name="2"></a> Painting Stairs

A long-standing [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread on how
to [paint stair faces](https://forums.autodesk.com/t5/revit-api-forum/paint-stair-faces/m-p/10388359) was finally answered quite simply
by Bruce [@canyon.des](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/10032309) Hans:

**Question:** Is there any reason why stair faces can be painted in the UI, and not in the API?
The question was raised in 2015 and apparently still exists:

I'm trying to paint some of the faces of a stair (a monolithic stair) through the Revit API.
It appears that it cannot be done programmatically &ndash; I get an error message saying that "the element faces cannot be painted" &ndash; even if I can achieve this stair face painting manually in the Revit UI.
Is there a way to automatically paint these faces?

**Answer:** For stairs, you need to use `GetStairsLandings` and `GetStairsRuns` to get the `ElementId` to paint landings or runs.
It's not intuitive but works.
Use the same to find out whether the landing or run faces are painted or not.

<pre class="code">
<span style="color:blue;">void</span>&nbsp;PaintStairs(&nbsp;UIDocument&nbsp;uidoc,&nbsp;Material&nbsp;mat&nbsp;)
{
&nbsp;&nbsp;Document&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
&nbsp;&nbsp;Selection&nbsp;sel&nbsp;=&nbsp;uidoc.Selection;
 
&nbsp;&nbsp;<span style="color:green;">//FaceSelectionFilter&nbsp;filter&nbsp;=&nbsp;new&nbsp;FaceSelectionFilter();</span>
&nbsp;&nbsp;Reference&nbsp;pickedRef&nbsp;=&nbsp;sel.PickObject(
&nbsp;&nbsp;&nbsp;&nbsp;ObjectType.PointOnElement,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//filter,&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Please&nbsp;select&nbsp;a&nbsp;Face&quot;</span>&nbsp;);
 
&nbsp;&nbsp;Element&nbsp;elem&nbsp;=&nbsp;doc.GetElement(&nbsp;pickedRef&nbsp;);
 
&nbsp;&nbsp;GeometryObject&nbsp;geoObject&nbsp;=&nbsp;elem
&nbsp;&nbsp;&nbsp;&nbsp;.GetGeometryObjectFromReference(&nbsp;pickedRef&nbsp;);
 
&nbsp;&nbsp;Face&nbsp;fc&nbsp;=&nbsp;geoObject&nbsp;<span style="color:blue;">as</span>&nbsp;Face;
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;elem.Category.Id.IntegerValue&nbsp;==&nbsp;-2000120&nbsp;)&nbsp;<span style="color:green;">//&nbsp;Stairs</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;flag&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;Stairs&nbsp;str&nbsp;=&nbsp;elem&nbsp;<span style="color:blue;">as</span>&nbsp;Stairs;
&nbsp;&nbsp;&nbsp;&nbsp;ICollection&lt;ElementId&gt;&nbsp;landings&nbsp;=&nbsp;str.GetStairsLandings();
&nbsp;&nbsp;&nbsp;&nbsp;ICollection&lt;ElementId&gt;&nbsp;runs&nbsp;=&nbsp;str.GetStairsLandings();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;Transaction&nbsp;transaction&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Transaction(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Start(&nbsp;<span style="color:#a31515;">&quot;Paint&nbsp;Material&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;ElementId&nbsp;id&nbsp;<span style="color:blue;">in</span>&nbsp;landings&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.Paint(&nbsp;id,&nbsp;fc,&nbsp;mat.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;flag&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!flag&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;ElementId&nbsp;id&nbsp;<span style="color:blue;">in</span>&nbsp;runs&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.Paint(&nbsp;id,&nbsp;fc,&nbsp;mat.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

Many thanks to Bruce for this simple and effective solution.
We have not finished discussing this issue yet, so please refer to the discussion thread for more updates.

####<a name="3"></a> Ray Tracing vs Location to Find Beams Intersecting Columns

Another recurring topic is how to find intersecting elements.

The thread
on [ray projection not picking up beams](https://forums.autodesk.com/t5/revit-api-forum/ray-projection-not-picking-up-beams/m-p/10388868) ends
up solved with two different approaches demonstrating possible ways to find beams intersecting columns using ray tracing versus the column location line, `Face.IsInside` and bounding box intersection:

**Question:** I'm trying to create a ray projection that finds the closest beam or slab from a column and attach it to the top to beam/slab found by the ray projection.
For some reason, I can't get it to find the beams I want it to attach too.
It only finds the slab.
Any Ideas?

This is what it looks like before I run my current code:

<center>
<img src="img/find_beams_intersecting_column_01.png" alt="Beams intersecting columns" title="Beams intersecting columns" width="600"/> <!-- 1346 -->
</center>

This is what it looks like afterwards; it only finds the slabs:

<center>
<img src="img/find_beams_intersecting_column_02.png" alt="Beams intersecting columns" title="Beams intersecting columns" width="600"/> <!-- 1170 -->
</center>

I want it to stop at the bottom of both beams and slabs.

Sooo... I tried to change the code to just pick up the beams:

This is what I have after that; circled in blue is what didn't attach to the beam above.
Some did attach:

<center>
<img src="img/find_beams_intersecting_column_03.png" alt="Beams intersecting columns" title="Beams intersecting columns" width="600"/> <!-- 1294 -->
</center>

So, why is this?
And, is there anything I can do to fix it.

Thanks ahead of time for any responses!

I think it has something to do with whether the column is centred under the beam, but I didn't think that would matter, because I'm using `FindReferenceTarget.All` in my ray projection.

Example of off-centre column not attaching:

<center>
<img src="img/find_beams_intersecting_column_04.png" alt="Beams intersecting columns" title="Beams intersecting columns" width="600"/> <!-- 2560 -->
</center>

**Answer:** It absolutely matters. The ray you shoot is an infinitely thin line, so you can easily miss something. You could try using five rays per pillar, e.g., one in the centre and one in each corner. I would suggest that you add some visual debugging code that represents part of your infinite shooting ray with a model line to visualise what is going on and whether a beam is hit or missed.

**Question:** I don't really understand, if the center of my column (which is where the ray is generated from) is within the bounds of the beam how does it not pick up the face of the beam? It seems to only pick up the centreline of the beam... Does `FindReferenceTarget.All` not find the face of beam? And if I add rays to the corners of the column I don't see how that would help if it only finds the beam when you hit the beam centreline straight on. I hope that makes sense.  Any ideas?

**Answer:** Normally, the reference intersector is set up so that an infinitely thin ray is shot and all intersections with faces or edges are reported. Just as you say, hitting an edge or a centreline or any other infinitely thin object is infinitely improbable.

**Question:** What's a good way to do multiple rays for a single object?

I also just figured out why the ray bounce isn't working.
It wasn't working on columns that extended past the beam already.
That's because Revit already cuts out the column from the beam.
So, there is no face for it to hit.
It works if the columns are below the beam.

<center>
<img src="img/find_beams_intersecting_column_05.png" alt="Beams intersecting columns" title="Beams intersecting columns" width="350"/> <!-- 729 -->
</center>

When I hide the column:

<center>
<img src="img/find_beams_intersecting_column_06.png" alt="Beams intersecting columns" title="Beams intersecting columns" width="350"/> <!-- 1014 -->
</center>

So, I think that your method of doing rays on the corners of the columns would help possibly pick up the intersecting edge.
I just don't know of a way to assign 5 rays to the column.
Can you point me in the correct direction?
I haven't seen anything on multiple rays per single object.

**Answer:** Simply calculate the four column bottom face corner points and the column centre line direction vector and use that data to define the four rays.

**Question:** lol... I wish it was "simply". Haha!
I have been trying to figure out how to get the bottom column corners with little success...
I have seen your blog about finding the bottom of walls and top of sloped walls but can't find anything on bottom of columns.
Also, once I get the points, where do I place them so it generates multiple rays?

**Answer:** There are ever so many different ways.

Maybe easier to iterate over the faces rather than the edges.
Either way is fine, though.

If you know that the cross section is rectangular and the column is vertical, you know that you have four bottom corners, and you can differentiate them from all other vertices simply by picking the four ones with minimal Z coordinates.

**Answer 2:** This task can actually be done without `ReferenceIntersector`

1) You can extract the Z extents of slabs/beams and columns from bounding box to compare level proximity.

2) You can use `Face.IsInside` to determine if a column location line is within the limits of the bottom face of slab or beam matching (1).
For slanted columns, you'll have to follow a point at base up the vector of the slant to see where point ends up at slab/beam underside.
Multiply `XY` components of vector by height difference between point at column base and underside of beam/slab, then add them to point at base.

Likely however that the `IsInside` will be affected by joins, as the `ReferenceIntersector` is.

You can probably identify a second capture group via bounding box intersection filters, i.e., the cases where the ray missing the faces due to join will be a cases where there is a bounding box intersection between such elements.

Could also use `JoinGeometry.UnjoinGeometry` between columns and slabs/beams prior to investigation.

Note that occasionally attaching column to underside will fail if column profile is not completely covered.
Often, you can partially cover a column and still get it to attach, but there is a limit on that.

**Response:**  Thank you both for the responses.
I changed my approach to use bounding boxes like you suggest in Answer 2.
It seems to be working for me.

Here's what worked for me:

<pre class="code">
<span style="color:blue;">void</span>&nbsp;AdjustColumnHeightsUsingBoundingBox(
&nbsp;&nbsp;Document&nbsp;doc,
&nbsp;&nbsp;IList&lt;ElementId&gt;&nbsp;ids&nbsp;)
{
&nbsp;&nbsp;View&nbsp;view&nbsp;=&nbsp;doc.ActiveView;
 
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;allColumns&nbsp;=&nbsp;0;
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;successColumns&nbsp;=&nbsp;0;
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;view&nbsp;<span style="color:blue;">is</span>&nbsp;View3D&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;Transaction&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Transaction(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Adjust&nbsp;Column&nbsp;Heights&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;ElementId&nbsp;elemId&nbsp;<span style="color:blue;">in</span>&nbsp;ids&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Element&nbsp;elem&nbsp;=&nbsp;doc.GetElement(&nbsp;elemId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Check&nbsp;if&nbsp;element&nbsp;is&nbsp;column</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;(BuiltInCategory)&nbsp;elem.Category.Id.IntegerValue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==&nbsp;BuiltInCategory.OST_StructuralColumns&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;allColumns++;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FamilyInstance&nbsp;column&nbsp;=&nbsp;elem&nbsp;<span style="color:blue;">as</span>&nbsp;FamilyInstance;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Collect&nbsp;beams&nbsp;and&nbsp;slabs&nbsp;within&nbsp;bounding&nbsp;box</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;BuiltInCategory&gt;&nbsp;builtInCats&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;BuiltInCategory&gt;();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;builtInCats.Add(&nbsp;BuiltInCategory.OST_Floors&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;builtInCats.Add(&nbsp;BuiltInCategory.OST_StructuralFraming&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElementMulticategoryFilter&nbsp;beamSlabFilter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;ElementMulticategoryFilter(&nbsp;builtInCats&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BoundingBoxXYZ&nbsp;bb&nbsp;=&nbsp;elem.get_BoundingBox(&nbsp;view&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Outline&nbsp;myOutLn&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Outline(&nbsp;bb.Min,&nbsp;bb.Max&nbsp;+&nbsp;100&nbsp;*&nbsp;XYZ.BasisZ&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BoundingBoxIntersectsFilter&nbsp;bbFilter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;BoundingBoxIntersectsFilter(&nbsp;myOutLn&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FilteredElementCollector&nbsp;collector&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FilteredElementCollector(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;beamSlabFilter&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;bbFilter&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;Element&gt;&nbsp;intersectingBeams&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;Element&gt;();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;Element&gt;&nbsp;intersectingSlabs&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;Element&gt;();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ColumnAttachment.GetColumnAttachment(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;column,&nbsp;1&nbsp;)&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Change&nbsp;color&nbsp;of&nbsp;columns&nbsp;to&nbsp;green</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Color&nbsp;color&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Color(&nbsp;(<span style="color:blue;">byte</span>)&nbsp;0,&nbsp;(<span style="color:blue;">byte</span>)&nbsp;255,&nbsp;(<span style="color:blue;">byte</span>)&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OverrideGraphicSettings&nbsp;ogs&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;OverrideGraphicSettings();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ogs.SetProjectionLineColor(&nbsp;color&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.SetElementOverrides(&nbsp;elem.Id,&nbsp;ogs&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;Element&nbsp;e&nbsp;<span style="color:blue;">in</span>&nbsp;collector&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;e.Category.Name&nbsp;==&nbsp;<span style="color:#a31515;">&quot;Structural&nbsp;Framing&quot;</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;intersectingBeams.Add(&nbsp;e&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>&nbsp;<span style="color:blue;">if</span>(&nbsp;e.Category.Name&nbsp;==&nbsp;<span style="color:#a31515;">&quot;Floors&quot;</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;intersectingSlabs.Add(&nbsp;e&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;intersectingBeams.Any()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Element&nbsp;lowestBottomElem&nbsp;=&nbsp;intersectingBeams.First();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;Element&nbsp;beam&nbsp;<span style="color:blue;">in</span>&nbsp;intersectingBeams&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BoundingBoxXYZ&nbsp;thisBeamBB&nbsp;=&nbsp;beam.get_BoundingBox(&nbsp;view&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BoundingBoxXYZ&nbsp;currentLowestBB&nbsp;=&nbsp;lowestBottomElem.get_BoundingBox(&nbsp;view&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;thisBeamBB.Min.Z&nbsp;&lt;&nbsp;currentLowestBB.Min.Z&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lowestBottomElem&nbsp;=&nbsp;beam;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ColumnAttachment.AddColumnAttachment(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;column,&nbsp;lowestBottomElem,&nbsp;1,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ColumnAttachmentCutStyle.None,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ColumnAttachmentJustification.Minimum,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;successColumns++;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>&nbsp;<span style="color:blue;">if</span>(&nbsp;intersectingSlabs.Any()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Element&nbsp;lowestBottomElem&nbsp;=&nbsp;intersectingSlabs.First();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;Element&nbsp;slab&nbsp;<span style="color:blue;">in</span>&nbsp;intersectingSlabs&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BoundingBoxXYZ&nbsp;thisSlabBB&nbsp;=&nbsp;slab.get_BoundingBox(&nbsp;view&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BoundingBoxXYZ&nbsp;currentLowestBB&nbsp;=&nbsp;lowestBottomElem.get_BoundingBox(&nbsp;view&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;thisSlabBB.Min.Z&nbsp;&lt;&nbsp;currentLowestBB.Min.Z&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lowestBottomElem&nbsp;=&nbsp;slab;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ColumnAttachment.AddColumnAttachment(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;column,&nbsp;lowestBottomElem,&nbsp;1,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ColumnAttachmentCutStyle.None,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ColumnAttachmentJustification.Minimum,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;successColumns++;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Change&nbsp;color&nbsp;of&nbsp;columns&nbsp;to&nbsp;red</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Color&nbsp;color&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Color(&nbsp;(<span style="color:blue;">byte</span>)&nbsp;255,&nbsp;(<span style="color:blue;">byte</span>)&nbsp;0,&nbsp;(<span style="color:blue;">byte</span>)&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OverrideGraphicSettings&nbsp;ogs&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;OverrideGraphicSettings();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ogs.SetProjectionLineColor(&nbsp;color&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.SetElementOverrides(&nbsp;elem.Id,&nbsp;ogs&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;TaskDialog.Show(&nbsp;<span style="color:#a31515;">&quot;Columns&nbsp;Changed&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>.Format(&nbsp;<span style="color:#a31515;">&quot;{0}&nbsp;of&nbsp;{1}&nbsp;Columns&nbsp;Changed&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;successColumns,&nbsp;allColumns&nbsp;)&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;TaskDialog.Show(&nbsp;<span style="color:#a31515;">&quot;Revit&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Run&nbsp;Script&nbsp;in&nbsp;3D&nbsp;View.&quot;</span>&nbsp;);
&nbsp;&nbsp;}
}
</pre>

I'd love to see an example of multiple rays per element if you ever decided to do a blog post about it.

**Answer:** There is nothing special about multiple rays per element at all.

In your code above, you shoot a ray upwards parallel to the Z axis from the element location point:

<pre class="code">
  <span style="color:green;">//&nbsp;ray&nbsp;direction&nbsp;for&nbsp;raybounce</span>
  XYZ&nbsp;newPP&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;elemLoc.X,&nbsp;elemLoc.Y,&nbsp;elemLoc.Z&nbsp;+&nbsp;1&nbsp;);
  XYZ&nbsp;rayd&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;0,&nbsp;0,&nbsp;1&nbsp;);
</pre>

You can define any other source point you like, e.g., each of the four bottom corner points in turn, and also any other direction you like, and simply repeat the same process using the same reference intersector in the same view by repeatedly calling
its [Find method with the new source point and direction vector](https://www.revitapidocs.com/2021.1/6abd0586-5d7e-68c6-2e64-46199f457499.htm).

**Response:**  I finally figured it out using the ray projection method as well.
Thanks to all your responses; I was super over-complicating it.
Thank you again for all your help.

Here is the working code with ray projection as well:

<pre class="code">
<span style="color:blue;">void</span>&nbsp;AdjustColumnHeightsUsingReferenceIntersector(
&nbsp;&nbsp;Document&nbsp;doc,
&nbsp;&nbsp;IList&lt;ElementId&gt;&nbsp;ids&nbsp;)
{
&nbsp;&nbsp;View3D&nbsp;view&nbsp;=&nbsp;doc.ActiveView&nbsp;<span style="color:blue;">as</span>&nbsp;View3D;
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;view&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;Exception(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Please&nbsp;run&nbsp;this&nbsp;command&nbsp;in&nbsp;a&nbsp;3D&nbsp;view.&quot;</span>&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;allColumns&nbsp;=&nbsp;0;
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;successColumns&nbsp;=&nbsp;0;
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;Transaction&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Transaction(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Attach&nbsp;Columns&nbsp;Tops&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;ElementId&nbsp;elemId&nbsp;<span style="color:blue;">in</span>&nbsp;ids&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Element&nbsp;elem&nbsp;=&nbsp;doc.GetElement(&nbsp;elemId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;(BuiltInCategory)&nbsp;elem.Category.Id.IntegerValue
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==&nbsp;BuiltInCategory.OST_StructuralColumns&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;allColumns++;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FamilyInstance&nbsp;column&nbsp;=&nbsp;elem&nbsp;<span style="color:blue;">as</span>&nbsp;FamilyInstance;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Collect&nbsp;beams&nbsp;and&nbsp;slabs</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;BuiltInCategory&gt;&nbsp;builtInCats&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;BuiltInCategory&gt;();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;builtInCats.Add(&nbsp;BuiltInCategory.OST_Floors&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;builtInCats.Add(&nbsp;BuiltInCategory.OST_StructuralFraming&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElementMulticategoryFilter&nbsp;filter
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;ElementMulticategoryFilter(&nbsp;builtInCats&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Remove&nbsp;old&nbsp;column&nbsp;attachement</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ColumnAttachment.GetColumnAttachment(&nbsp;column,&nbsp;1&nbsp;)&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ColumnAttachment.RemoveColumnAttachment(&nbsp;column,&nbsp;1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BoundingBoxXYZ&nbsp;elemBB&nbsp;=&nbsp;elem.get_BoundingBox(&nbsp;view&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;elemLoc&nbsp;=&nbsp;(elem.Location&nbsp;<span style="color:blue;">as</span>&nbsp;LocationPoint).Point;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;elemCenter&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;elemLoc.X,&nbsp;elemLoc.Y,&nbsp;elemLoc.Z&nbsp;+&nbsp;0.1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;b1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;elemBB.Min.X,&nbsp;elemBB.Min.Y,&nbsp;elemBB.Min.Z&nbsp;+&nbsp;0.1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;b2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;elemBB.Max.X,&nbsp;elemBB.Max.Y,&nbsp;elemBB.Min.Z&nbsp;+&nbsp;0.1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;b3&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;elemBB.Min.X,&nbsp;elemBB.Max.Y,&nbsp;elemBB.Min.Z&nbsp;+&nbsp;0.1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;b4&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;elemBB.Max.X,&nbsp;elemBB.Min.Y,&nbsp;elemBB.Min.Z&nbsp;+&nbsp;0.1&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;XYZ&gt;&nbsp;points&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;XYZ&gt;(&nbsp;5&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;points.Add(&nbsp;b1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;points.Add(&nbsp;b2&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;points.Add(&nbsp;b3&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;points.Add(&nbsp;b4&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;points.Add(&nbsp;elemCenter&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ReferenceIntersector&nbsp;refI&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;ReferenceIntersector(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;filter,&nbsp;FindReferenceTarget.All,&nbsp;view&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;rayd&nbsp;=&nbsp;XYZ.BasisZ;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ReferenceWithContext&nbsp;refC&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;XYZ&nbsp;pt&nbsp;<span style="color:blue;">in</span>&nbsp;points&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;refC&nbsp;=&nbsp;refI.FindNearest(&nbsp;pt,&nbsp;rayd&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;refC&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;refC&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Reference&nbsp;reference&nbsp;=&nbsp;refC.GetReference();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElementId&nbsp;id&nbsp;=&nbsp;reference.ElementId;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Element&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;id&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ColumnAttachment.AddColumnAttachment(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;column,&nbsp;e,&nbsp;1,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ColumnAttachmentCutStyle.None,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ColumnAttachmentJustification.Minimum,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;successColumns++;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Change&nbsp;color&nbsp;of&nbsp;columns&nbsp;to&nbsp;red</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Color&nbsp;color&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Color(&nbsp;(<span style="color:blue;">byte</span>)&nbsp;255,&nbsp;(<span style="color:blue;">byte</span>)&nbsp;0,&nbsp;(<span style="color:blue;">byte</span>)&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OverrideGraphicSettings&nbsp;ogs&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;OverrideGraphicSettings();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ogs.SetProjectionLineColor(&nbsp;color&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.SetElementOverrides(&nbsp;elem.Id,&nbsp;ogs&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;}
}
</pre>

**Answer:** Congratulations on simplifying and solving this.
[Keeping it simple](https://en.wikipedia.org/wiki/KISS_principle) works wonders, doesn't it?

Thank you for sharing the two approaches, and thanks
to Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
for the non-raytracing suggestion!

<!---

oops... this video was taken down from youtube again...

####<a name="4"></a> Play Doom on Wireless Lamp

Computers are still getting smaller and more powerful.

It has reached a point where you can
play [Doom on an IKEA Trådfri lamp](https://youtu.be/7ybybf4tJWw)

> We ported Doom to the Silicon lab's MGM210L RF module found in the IKEA TRÅDFRI RGB GU10 lamp model LED1923R5. 
The module has only 108 kB of RAM, so we had to optimize a lot the RAM usage. 
The module has only 1 MB of internal flash, therefore we added an external SPI flash to store the WAD file, which can be uploaded using YMODEM.
The display is a cheap and widespread 160x128 16bpp, 1.8" TFT.

-->

