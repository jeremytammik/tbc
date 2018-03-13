<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- [Retrieving All Family Instances in a Room](http://thebuildingcoder.typepad.com/blog/2017/04/forgefader-ui-lookup-builds-purge-and-room-instances.html#5)
  [Filter for Family Instances in a Room](http://thebuildingcoder.typepad.com/blog/2013/03/filter-for-family-instances-in-a-room.html)
  [irregular room shape](http://thebuildingcoder.typepad.com/blog/2013/03/filter-for-family-instances-in-a-room.html#comment-3781548196)

- Find Intersecting Elements
  http://thebuildingcoder.typepad.com/blog/2010/12/find-intersecting-elements.html#comment-3783584153
  http://thebuildingcoder.typepad.com/blog/2010/12/find-intersecting-elements.html#comment-3783587401
  https://stackoverflow.com/questions/49070566/bounding-box-intersection

Filter for intersecting elements and create 2D arc in #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/filterintersect

Several questions on filtering for intersecting elements came up recently.
It is pretty easy as long as a bounding box can be used.
However, the bounding box is generally aligned with the cardinal X, Y and Z axes.
If the containing volume of interest is not, too many elements may be selected
&ndash; Family instances in a diagonal room
&ndash; Conduits intersecting a junction box
&ndash; Generate 2D arc from radius, start and end points...

--->

### Create 2D Arc and Filter for Intersecting Elements

Several questions on filtering for intersecting elements came up recently.

It is pretty easy as long as a bounding box can be used.

However, the bounding box is generally aligned with the cardinal X, Y and Z axes.

If the containing volume of interest is not, too many elements may be selected.

This can be addressed in various ways, e.g., post-processing the bounding box results, or using a more precise intersection filter.

- [Family instances in a diagonal room](#2)
- [Conduits intersecting a junction box](#3)
- [Generate 2D arc from radius, start and end points](#4)


####<a name="2"></a>Family Instances in a Diagonal Room

Kavitha asked the question in
his [comment](http://thebuildingcoder.typepad.com/blog/2013/03/filter-for-family-instances-in-a-room.html#comment-3781548196)
on [filtering for family instances in a room](http://thebuildingcoder.typepad.com/blog/2013/03/filter-for-family-instances-in-a-room.html):

**Question:** Can someone explain for me: if the room is diagonal, then the bounding box value will be wrong; in that case, how can I get the elements properly?

**Answer:** You are right, of course.

The bounding box will be too large and therefore possibly contain too many elements.

You can eliminate the elements outside the room by many means.

[IsPointInRoom](http://www.revitapidocs.com/2018.1/21d28ce3-3c1a-43cd-9714-0fe7223c5636.htm), for example, determines whether a given point lies within the volume of the room.

If you have an element with some geometry, e.g., a solid, you could test each of its vertices with that method.

If you are interested in the 2D plan view only, you could also implement a more precise test by performing a Boolean operation between a polygon containing the element and the room boundary polygon.

My [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) exports
rooms and the family instances they contain to a cloud database; for that, it obviously determines which instances lie within which room, as mentioned in the note
on [retrieving all family instances in a room](http://thebuildingcoder.typepad.com/blog/2017/04/forgefader-ui-lookup-builds-purge-and-room-instances.html#5) elsewhere,

It just uses a filtered element collector based on the room bounding box, though, in the
method [`GetFurniture( Room room )`](https://github.com/jeremytammik/RoomEditorApp/blob/master/RoomEditorApp/CmdUploadRooms.cs#L157-L217),
so it does not answer your question on the diagonal case.

For a precise handling of the diagonal or any other irregularly shaped case, you could also retrieve the room solid from
its [`ClosedShell` property](http://www.revitapidocs.com/2018.1/1a510aef-63f6-4d32-c0ff-a8071f5e23b8.htm) and use
an [`ElementIntersectsSolidFilter`](http://www.revitapidocs.com/2018.1/19276b94-fa39-64bb-bfb8-c16967c83485.htm) based on that, cf. below.

####<a name="3"></a>Conduits Intersecting a Junction Box

Right after that, I started out discussing another intersection and containment issue with Tiago Cerqueria
in [his](http://thebuildingcoder.typepad.com/blog/2010/12/find-intersecting-elements.html#comment-3783584153)
[comments](http://thebuildingcoder.typepad.com/blog/2010/12/find-intersecting-elements.html#comment-3783587401)
on [finding intersecting elements](http://thebuildingcoder.typepad.com/blog/2010/12/find-intersecting-elements.html)
and StackOverflow question on [bounding box intersection](https://stackoverflow.com/questions/49070566/bounding-box-intersection):

**Question:** To find elements that are intersecting a geometry, I am using the example
to [find intersecting elements](http://thebuildingcoder.typepad.com/blog/2010/12/find-intersecting-elements.html).

The main goal of my add-in was find conduits clashing with a junction box and access all the elements connected with it to insert information.

But the bounding box is always parallel to the cardinal X, Y and Z axes, and this may cause a problem, like returning elements that are not really clashing, because sometimes the bounding box is not coincident with the geometry because the family instance is rotated.

Besides that, there is the problem that the bounding box will consider the geometry of the symbol and not the instance, and will consider the flipped geometry too, meaning that the bounding box is bigger than I am looking for.

Is there a way to get the real geometry that are in the currently view? How can I solve this problem?

**Answer:** There are many ways to address this.

Generally, when performing clash detection, you will always run a super fast pre-processing step first to determine candidate elements, and then narrow down the search step by step more precisely in following steps. In this case, you can consider the bounding box intersection the first step, and then perform post-processing afterwards to narrow down the result to your exact goal.

One important question is: does the bounding box really give you all the elements you need, plus more? Are you sure there are none missing?

Once that is settled, all you need to do is add post-processing steps applying the detailed considerations that you care about to remove superfluous elements.

A simple property to check might be: are all the target element geometry vertices contained in the target volume?

A more complex one might involve retrieving the full solid of the target element and the target volume and performing a Boolean intersection between them to determine completely and exactly whether they intersect, are disjunct, or contained in each other.

Many others are conceivable.

**Response:** I am using another strategy that accesses the geometry of the instance to verify whether the face of the family instance clashes with a closer conduit:

Here follows the code that I created and that works perfectly.

I am accessing the geometry of the family by the method `get_Geometry(options)` to retrieve the instance geometry  located in project coordinates. From this I get the face and verify whether there is an intersection:

<pre class="code">
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">FindIntersection</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Conduit</span>&nbsp;ConduitRun&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;Jbox&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Conduit</span>&gt;&nbsp;GetListOfConduits&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Conduit</span>&gt;();

&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;FindIntersection(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;jbox,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uiDoc&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;jboxPoint&nbsp;=&nbsp;(&nbsp;jbox.Location
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationPoint</span>&nbsp;).Point;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;filteredCloserConduits
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;uiDoc.Document&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;listOfCloserConduit
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;filteredCloserConduits
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Conduit</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToList()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;(&nbsp;(&nbsp;x&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Conduit</span>&nbsp;).Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>&nbsp;).Curve
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetEndPoint(&nbsp;0&nbsp;).DistanceTo(&nbsp;jboxPoint&nbsp;)&nbsp;&lt;&nbsp;30
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;(&nbsp;(&nbsp;x&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Conduit</span>&nbsp;).Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>&nbsp;).Curve
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetEndPoint(&nbsp;1&nbsp;).DistanceTo(&nbsp;jboxPoint&nbsp;)&nbsp;&lt;&nbsp;30&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToList();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;getting&nbsp;the&nbsp;location&nbsp;of&nbsp;the&nbsp;box&nbsp;and&nbsp;all&nbsp;conduit&nbsp;around.</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Options</span>&nbsp;opt&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>();
&nbsp;&nbsp;&nbsp;&nbsp;opt.View&nbsp;=&nbsp;uiDoc.ActiveView;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">GeometryElement</span>&nbsp;geoEle&nbsp;=&nbsp;jbox.get_Geometry(&nbsp;opt&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;getting&nbsp;the&nbsp;geometry&nbsp;of&nbsp;the&nbsp;element&nbsp;to&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;access&nbsp;the&nbsp;geometry&nbsp;of&nbsp;the&nbsp;instance.</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GeometryObject</span>&nbsp;geomObje1&nbsp;<span style="color:blue;">in</span>&nbsp;geoEle&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">GeometryElement</span>&nbsp;geoInstance&nbsp;=&nbsp;(&nbsp;geomObje1
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">GeometryInstance</span>&nbsp;).GetInstanceGeometry();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;the&nbsp;geometry&nbsp;of&nbsp;the&nbsp;family&nbsp;instance&nbsp;can&nbsp;be&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;accessed&nbsp;by&nbsp;this&nbsp;method&nbsp;that&nbsp;returns&nbsp;a&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;GeometryElement&nbsp;type.&nbsp;so&nbsp;we&nbsp;must&nbsp;get&nbsp;the&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;GeometryObject&nbsp;again&nbsp;to&nbsp;access&nbsp;the&nbsp;Face&nbsp;of&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;the&nbsp;family&nbsp;instance.</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;geoInstance&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GeometryObject</span>&nbsp;geomObje2&nbsp;<span style="color:blue;">in</span>&nbsp;geoInstance&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Solid</span>&nbsp;geoSolid&nbsp;=&nbsp;geomObje2&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Solid</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;geoSolid&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;face&nbsp;<span style="color:blue;">in</span>&nbsp;geoSolid.Faces&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;cond&nbsp;<span style="color:blue;">in</span>&nbsp;listOfCloserConduit&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Conduit</span>&nbsp;con&nbsp;=&nbsp;cond&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Conduit</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;conCurve&nbsp;=&nbsp;(&nbsp;con.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>&nbsp;).Curve;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SetComparisonResult</span>&nbsp;set&nbsp;=&nbsp;face.Intersect(&nbsp;conCurve&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;set.ToString()&nbsp;==&nbsp;<span style="color:#a31515;">&quot;Overlap&quot;</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//getting&nbsp;the&nbsp;conduit&nbsp;the&nbsp;intersect&nbsp;the&nbsp;box.</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetListOfConduits.Add(&nbsp;con&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

**Answer:** I considered this problem myself as well in the meantime and thought of two filters that may achieve a similar result more efficiently.

Using those, you do not have to retrieve the conduits and their geometry one by one and analyse them yourself, because the filters already do it for you.

One is quick and compares the axis-aligned bounding box.

The other is slow and intersects the exact element geometry.

It is important to realise and always have in mind
the [important difference between quick and slow filters](http://thebuildingcoder.typepad.com/blog/2015/12/quick-slow-and-linq-element-filtering.html#3).

You may want to try them out yourself:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;<span style="color:#2b91af;">Util</span>.SelectSingleElement(
&nbsp;&nbsp;&nbsp;&nbsp;uidoc,&nbsp;<span style="color:#a31515;">&quot;a&nbsp;junction&nbsp;box&quot;</span>&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;bb&nbsp;=&nbsp;e.get_BoundingBox(&nbsp;<span style="color:blue;">null</span>&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">Outline</span>&nbsp;outLne&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;bb.Min,&nbsp;bb.Max&nbsp;);

&nbsp;&nbsp;<span style="color:green;">//&nbsp;Use&nbsp;a&nbsp;quick&nbsp;bounding&nbsp;box&nbsp;filter&nbsp;-&nbsp;axis&nbsp;aligned</span>

&nbsp;&nbsp;<span style="color:#2b91af;">ElementQuickFilter</span>&nbsp;fbb
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxIntersectsFilter</span>(&nbsp;outLne&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;conduits
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Conduit</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;fbb&nbsp;);

&nbsp;&nbsp;<span style="color:green;">//&nbsp;How&nbsp;many&nbsp;elements&nbsp;did&nbsp;we&nbsp;find?</span>

&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;nbb&nbsp;=&nbsp;conduits.GetElementCount();

&nbsp;&nbsp;<span style="color:green;">//&nbsp;Use&nbsp;a&nbsp;slow&nbsp;intersection&nbsp;filter&nbsp;-&nbsp;exact&nbsp;results</span>

&nbsp;&nbsp;<span style="color:#2b91af;">ElementSlowFilter</span>&nbsp;intersect_junction
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementIntersectsElementFilter</span>(&nbsp;e&nbsp;);

&nbsp;&nbsp;conduits&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Conduit</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;intersect_junction&nbsp;);

&nbsp;&nbsp;<span style="color:green;">//&nbsp;How&nbsp;many&nbsp;elements&nbsp;did&nbsp;we&nbsp;find?</span>

&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;nintersect&nbsp;=&nbsp;conduits.GetElementCount();

&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;nintersect&nbsp;&lt;=&nbsp;nbb,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;element&nbsp;intersection&nbsp;to&nbsp;be&nbsp;stricter&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;than&nbsp;bounding&nbsp;box&nbsp;containment&quot;</span>&nbsp;);
</pre>

I implemented a new external
command [CmdIntersectJunctionBox](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdIntersectJunctionBox.cs)
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) to try this out, in
[release 2018.0.137.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.137.0).

Waiting for a suitable sample model to test this on...

Thank you, Thiago, for sharing your alternative approach!


####<a name="4"></a>Generate 2D Arc from Radius, Start and End Points

Finally, a pure geometric non-filtering question that came up today and was answered very precisely and exhaustively by Scott Wilson,
on [creating an arc when only the radius, start and end point is known](https://forums.autodesk.com/t5/revit-api-forum/create-a-curve-when-only-the-start-point-end-point-amp-radius-is/m-p/7830079):

**Question:** I am attempting to create an arc, but I only know the following properties:

- Radius
- Start point (`XYZ`)
- End point (`XYZ`)

Afaik, there aren't any functions that can create an arc using this information.

**Answer:** Technically speaking, 2 points and a radius are not enough to define a unique arc, as there are up to 4 possible arcs that will fit for any plane of reference. Test this yourself by drawing 2 circles of the same size that overlap each other and then draw a line between the 2 intersecting points. Each side of the line will contain 2 arcs that have the same end points and radius.

You can get away with it if you firstly adopt the convention that all arcs are to be drawn in the same direction from start to end (say, anticlockwise) when looking at them in the negative Z direction of the target plane, and secondly, that you always want either the arc with included angle of less than or equal to 180 degrees or the larger arc of over 180 degrees.

That aside, your best option is to calculate a point that lies on the arc and use the 3 points method that you have already used, but this time you will be giving it the correct 3rd point, and there will be no need to attempt changing the radius afterwards. We will use the anti-clockwise convention as that is what Revit also uses, we will also assume that the smaller arc is desired.

Here is a little bit of code I just whipped up for you; I have done some minimal testing and it appears to be working fine. I have assumed that you have the 3 following variables already defined and in scope:

- `XYZ end0`
- `XYZ end1`
- `Double radius`

I have also assumed that the arc lies on a plane with a normal vector of `XYZ.BasisZ`:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Double</span>&nbsp;sagitta&nbsp;=&nbsp;radius&nbsp;-&nbsp;<span style="color:#2b91af;">Math</span>.Sqrt(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Pow(&nbsp;radius,&nbsp;2.0&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;<span style="color:#2b91af;">Math</span>.Pow(&nbsp;(&nbsp;end1&nbsp;-&nbsp;end0&nbsp;).GetLength()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;2.0,&nbsp;2.0&nbsp;)&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;midPointOfChord&nbsp;=&nbsp;(&nbsp;end0&nbsp;+&nbsp;end1&nbsp;)&nbsp;/&nbsp;2.0;

&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;midPointOfArc&nbsp;=&nbsp;midPointOfChord&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#2b91af;">Transform</span>.CreateRotation(&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisZ,&nbsp;<span style="color:#2b91af;">Math</span>.PI&nbsp;/&nbsp;2.0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfVector(&nbsp;(&nbsp;end0&nbsp;-&nbsp;end1&nbsp;).Normalize()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Multiply(&nbsp;sagitta&nbsp;)&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">Arc</span>&nbsp;myArc&nbsp;=&nbsp;<span style="color:#2b91af;">Arc</span>.Create(&nbsp;end0,&nbsp;end1,&nbsp;midPointOfArc&nbsp;);
</pre>

If you want the larger arc, simply change the sagitta calculation to be `radius +` instead of `radius -`.

If you aren't familiar with the mathematics used to calculate arcs, I would suggest heading over
to the [Math Open Reference sagitta explanation](http://www.mathopenref.com/sagitta.html).

No problems, I've just recently been doing some work on line-based families with arc location lines, so it was all fresh in my head.

I think you may have some issues achieving what you are after though; the solution I gave will only really work accurately for 2D data unless you also plug in the correct normal vector (axis) of each arc, basically everywhere that I have used `XYZ.BasisZ` would need to be replaced with the correct normal vector.

From the data you have it is not possible to determine the correct orientation of the arcs. You will need either a 3rd point on the arc provided for you (which would solve all your problems), or a normal vector of the target plane. Are you sure that there isn't any other information exported for arcs? If not, then the exporter isn't doing a very good job.

A possible solution then could be to see if you can first tessellate the arcs in the source software and then export just the straight line data. You could then rebuild the arcs from the lines as they will give you enough points to define a plane, but the trick would be knowing which lines belong to an arc and which don't. To solve this, you could export 2 sets of data, one with tessellation and one without and then match the end points found in the tessellated data against the other set to determine whether you need to build an arc or not.

Ok, I'll stop rambling now. Long story short: you need more data!

Edit: I just had the thought that if you have a mixture of arcs and lines and the lines are always tangent to the arcs, you could use the lines each side of an arc to calculate the correct plane.

If your data is actually 2D (null Z value), you should be fine unless you also encounter data with Z-values.

Many thanks to Scott for this nice solution and complete explanation!

I added it
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) as well, in C#,
in [release 2018.0.137.1](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.137.1),
[like this](https://github.com/jeremytammik/the_building_coder_samples/compare/2018.0.137.0...2018.0.137.1):

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Create&nbsp;an&nbsp;arc&nbsp;in&nbsp;the&nbsp;XY&nbsp;plane&nbsp;from&nbsp;a&nbsp;given</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;start&nbsp;point,&nbsp;end&nbsp;point&nbsp;and&nbsp;radius.&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Arc</span>&nbsp;CreateArc2dFromRadiusStartAndEndPoint(&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;ps,&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pe,&nbsp;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;radius,&nbsp;
&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;largeSagitta&nbsp;=&nbsp;<span style="color:blue;">false</span>,
&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;clockwise&nbsp;=&nbsp;<span style="color:blue;">false</span>&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;midPointChord&nbsp;=&nbsp;0.5&nbsp;*&nbsp;(&nbsp;ps&nbsp;+&nbsp;pe&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;v&nbsp;=&nbsp;pe&nbsp;-&nbsp;ps;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;d&nbsp;=&nbsp;0.5&nbsp;*&nbsp;v.GetLength();&nbsp;<span style="color:green;">//&nbsp;half&nbsp;chord&nbsp;length</span>

&nbsp;&nbsp;<span style="color:green;">//&nbsp;Small&nbsp;and&nbsp;large&nbsp;circle&nbsp;sagitta:</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;http://www.mathopenref.com/sagitta.html</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;https://en.wikipedia.org/wiki/Sagitta_(geometry)</span>

&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;s&nbsp;=&nbsp;largeSagitta
&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;radius&nbsp;+&nbsp;<span style="color:#2b91af;">Math</span>.Sqrt(&nbsp;radius&nbsp;*&nbsp;radius&nbsp;-&nbsp;d&nbsp;*&nbsp;d&nbsp;)&nbsp;<span style="color:green;">//&nbsp;sagitta&nbsp;large</span>
&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;radius&nbsp;-&nbsp;<span style="color:#2b91af;">Math</span>.Sqrt(&nbsp;radius&nbsp;*&nbsp;radius&nbsp;-&nbsp;d&nbsp;*&nbsp;d&nbsp;);&nbsp;<span style="color:green;">//&nbsp;sagitta&nbsp;small</span>

&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;midPointOffset&nbsp;=&nbsp;<span style="color:#2b91af;">Transform</span>
&nbsp;&nbsp;&nbsp;&nbsp;.CreateRotation(&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisZ,&nbsp;0.5&nbsp;*&nbsp;<span style="color:#2b91af;">Math</span>.PI&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfVector(&nbsp;v.Normalize().Multiply(&nbsp;s&nbsp;)&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;midPointArc&nbsp;=&nbsp;clockwise
&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;midPointChord&nbsp;+&nbsp;midPointOffset
&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;midPointChord&nbsp;-&nbsp;midPointOffset;

&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Arc</span>.Create(&nbsp;ps,&nbsp;pe,&nbsp;midPointArc&nbsp;);
}
</pre>

<center>
<img src="img/sagitta.png" alt="Sagitta" width="299"/>
</center>
