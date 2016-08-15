<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

- Stephen Preston <stephen.preston@autodesk.com> Re: Forums are closing

- http://forums.autodesk.com/t5/revit-api/how-to-generate-x-and-y-axes-of-rooms-in-the-revit-api-in/m-p/6452079

- 11959053 [Model Bounding Box]
  http://forums.autodesk.com/t5/revit-api/how-to-get-the-total-width-and-length-of-the-design-using-revit/m-p/6419530
  12061737 [如何显示选中元素的剖面框]

Vacation End, Forge News and Bounding Boxes #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

I am back from a very relaxing vacation. I did next to nothing, and that felt fine. Meanwhile, obviously, lots of important and exciting Forge community and Revit API related happenings
&ndash; PyRevit Blog
&ndash; Forge DevCon 2016 material and 2017 dates
&ndash; Forge forums closing in favour of StackOverflow
&ndash; Forge Accelerator in Munich
&ndash; Bounding Box <code>ExpandToContain</code> Extension Methods
&ndash; Bounding Box and Lower Left Corner of Rooms
&ndash; Bounding Box of Selected Elements or Entire Model
&ndash; Setting 3D section box to selected elements' extents...

-->

### Vacation End, Forge News and Bounding Boxes

I am back from a very relaxing vacation, spent in the French Jura countryside on the western Swiss border and in Ticino in the south of Switzerland.

I did next to nothing, and that felt fine.

My greatest achievement was probably climbing the (very slippery welded steel) summit cross on Mount Tamaro &ndash; :-)

<center>
<img src="img/20160808_152704_jeremy_tamaro_gipfelkreuz_400.jpg" alt="Jeremy on Mount Tamaro" width="200">
</center>

Meanwhile, obviously, lots of important and exciting Forge community and Revit API related happenings, of which I will just name a first few examples to start off blogging again: 

- [PyRevit Blog](#2)
- [Forge DevCon 2016 material and 2017 dates](#3)
- [Forge forums closing in favour of StackOverflow](#4)
- [Forge Accelerator in Munich](#5)
- [Bounding Box `ExpandToContain` Extension Methods](#6)
- [Bounding Box and Lower Left Corner of Rooms](#7)
- [Bounding Box of Selected Elements or Entire Model](#8)
- [Setting 3D section box to selected elements' extents](#9)


#### <a name="2"></a>PyRevit Blog

PyRevit has a blog now, hosted by GitHub pages &ndash; :-)

<center>
<span style="font-size: 120%; font-weight: bold">
[eirannejad.github.io/pyRevit](http://eirannejad.github.io/pyRevit)
</span>
</center>


#### <a name="3"></a>Forge DevCon 2016 Material and 2017 Dates

The [Forge DevCon 2016 developer conference material](http://adndevblog.typepad.com/cloud_and_mobile/2016/08/content-from-forge-devcon-2016.html) is
now published and online.

In case you like to plan far ahead, as is common here in Switzerland, please also take note that the dates for the Forge DevCon 2017 developer conference have been settled: June 27-28, 2017.


#### <a name="4"></a>Forge Forums Closing in Favour of StackOverflow

As you probably are aware, all Forge questions are now being discussed on [StackOverflow](http://stackoverflow.com).

Simply search for the StackOverflow tags `autodesk`, `forge`, and the various Forge APIs. Typing the former currently lists the following:

- [autodesk](http://stackoverflow.com/questions/tagged/autodesk)
- [autodesk-forge](http://stackoverflow.com/questions/tagged/autodesk-forge)
- [autodesk-viewer](http://stackoverflow.com/questions/tagged/autodesk-viewer)
- [autodesk-model-derivative](http://stackoverflow.com/questions/tagged/autodesk-model-derivative)
- [autodesk-inventor](http://stackoverflow.com/questions/tagged/autodesk-inventor)
- [autodesk-vault](http://stackoverflow.com/questions/tagged/autodesk-vault)
- [autodesk-data-management](http://stackoverflow.com/questions/tagged/autodesk-data-management)
- [autodesk-designautomation](http://stackoverflow.com/questions/tagged/autodesk-designautomation)
- [autodesk-d-print](http://stackoverflow.com/questions/tagged/autodesk-3d-print)

Accordingly, the Autodesk discussion forums with the obsolete names 'View &amp; Data API' and 'AutoCAD I/O' are now closed for new posts:

- [AutoCAD I/O now closing](http://forums.autodesk.com/t5/autocad-i-o/this-forum-is-closing/td-p/6476336)
- [View &amp; Data now closing](http://forums.autodesk.com/t5/view-and-data-api/this-forum-is-closing/td-p/6476330)
- [Getting help for Forge APIs](http://adndevblog.typepad.com/cloud_and_mobile/2016/08/getting-help-for-forge-apis.html)

By the way, regarding Revit, I hope that you are also aware of the Revit related tags:

- [revit](http://stackoverflow.com/questions/tagged/revit)
- [revit-api](http://stackoverflow.com/questions/tagged/revit-api)
- [revitpythonshell](http://stackoverflow.com/questions/tagged/revitpythonshell)
- [revit-2015](http://stackoverflow.com/questions/tagged/revit-2015)
- [pyrevit](http://stackoverflow.com/questions/tagged/pyrevit)


#### <a name="5"></a>Forge Accelerator in Munich

The next [Forge Accelerator](http://autodeskcloudaccelerator.com) is taking place in Munich, Germany, October 24-28, 2016.

Mark your calendar, and please submit your proposal, if you are interested in participating.
The proposal deadline is in September.

I will be there, and I hope you will too!



<!--- #### <a name="6"></a>Forge Hackathon Coming Up -->

#### <a name="6"></a>Bounding Box `ExpandToContain` Extension Methods

Returning to the Revit API, a number of recent developer support issues
and [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) threads
address questions related to determining a bounding box for selected elements or the entire model, the lower left corner of rooms, and setting the section box of a 3D view to a selected element's bounding box.

In order to help address these, I started off by implementing two `ExpandToContain` extension methods for the `BoundingBoxXYZ` class, to expand an existing bounding box by adding a `XYZ` point or another bounding box to it:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">JtBoundingBoxXyzExtensionMethods</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Expand&nbsp;the&nbsp;given&nbsp;bounding&nbsp;box&nbsp;to&nbsp;include&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;and&nbsp;contain&nbsp;the&nbsp;given&nbsp;point.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;ExpandToContain(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;bb,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;bb.Min&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;<span style="color:#2b91af;">Math</span>.Min(&nbsp;bb.Min.X,&nbsp;p.X&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Min(&nbsp;bb.Min.Y,&nbsp;p.Y&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Min(&nbsp;bb.Min.Z,&nbsp;p.Z&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;bb.Max&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;<span style="color:#2b91af;">Math</span>.Max(&nbsp;bb.Max.X,&nbsp;p.X&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Max(&nbsp;bb.Max.Y,&nbsp;p.Y&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Max(&nbsp;bb.Max.Z,&nbsp;p.Z&nbsp;)&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Expand&nbsp;the&nbsp;given&nbsp;bounding&nbsp;box&nbsp;to&nbsp;include&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;and&nbsp;contain&nbsp;the&nbsp;given&nbsp;other&nbsp;one.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;ExpandToContain(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;bb,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;other&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;bb.ExpandToContain(&nbsp;other.Min&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;bb.ExpandToContain(&nbsp;other.Max&nbsp;);
&nbsp;&nbsp;}
}
</pre>

Check it out live 
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[Util.cs utility class](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/Util.cs), currently
in [lines 1275-1305](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/Util.cs#L1275-L1305).

Here follow some usage examples.



#### <a name="7"></a>Bounding Box and Lower Left Corner of Rooms

Question on [how to generate x and y-axes of rooms in the Revit API in millimetres](http://forums.autodesk.com/t5/revit-api/how-to-generate-x-and-y-axes-of-rooms-in-the-revit-api-in/m-p/6452079):

I'm trying to write code that gives the x and y-coordinates of the bottom-left point of each room in the design, based on the length in millimetre. If for somehow the code sets the bottom-left corner of the design at (0,0) and then from there if wall surrounds all the inner space such that wall width is 290mm, then the first room at the bottom-left corner starts at (291,291) coordinates. The code reads all the rooms and returns the list of coordinates in (mm) for each of them.
 
I tried to use the following code to achieve this:

<pre>
  Location loc = room.Location;
  LocationPoint lp = loc as LocationPoint;
  XYZ p = (null == lp) ? XYZ.Zero : lp.Point;
  msg.Add(room.Name + " - " + p.X + ", "+ p.Y);
</pre>

However, I found some of the X and Y axes are in minus and moreover they are not in millimetre, which is meaningless for my purpose. 

<center>
<img src="img/room_corners_annotated_design.png" alt="Room corners" width="400">
</center>

**Answer:** This is easy to answer and solve.
 
Two steps are required:
 
First of all, all Revit database [length measurements are in imperial feet](http://thebuildingcoder.typepad.com/blog/2011/03/internal-imperial-units.html).
 
There is no way to change that. 
 
Therefore, when you retrieve coordinates via the Revit API, they will always be in feet, and you will have to convert them to millimetres yourself.
 
That is very easy, of course.
 
Secondly, if you are interested in the lower left-hand corner of the room, the room location point is of absolutely no use whatsoever.
 
Instead, you could, for instance, retrieve the room boundary, iterate over all the curves, and determine which endpoint is on the lower left.
 
If your rooms have straight edges, this is trivial.
 
If the edges are curved, it might get a bit more complicated.
 
Third, you might also be able to use the room bounding box. That would be simpler still, but maybe less precise.

I implemented the second option for you
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples), in the
module [CmdListAllRooms.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdListAllRooms.cs).

In it, I iterate over all the room boundary segments, tessellate them, and use the resulting points to generate an accurate 2D bounding box for the room:
 
<pre class="code">
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;bounding&nbsp;box&nbsp;calculated&nbsp;from&nbsp;the&nbsp;room&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;boundary&nbsp;segments.&nbsp;The&nbsp;lower&nbsp;left&nbsp;corner&nbsp;turns&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;out&nbsp;to&nbsp;be&nbsp;identical&nbsp;with&nbsp;the&nbsp;one&nbsp;returned&nbsp;by&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;the&nbsp;standard&nbsp;room&nbsp;bounding&nbsp;box.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;GetBoundingBox(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">BoundarySegment</span>&gt;&gt;&nbsp;boundary&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;bb&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;infinity&nbsp;=&nbsp;<span style="color:blue;">double</span>.MaxValue;
 
&nbsp;&nbsp;&nbsp;&nbsp;bb.Min&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;infinity,&nbsp;infinity,&nbsp;infinity&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;bb.Max&nbsp;=&nbsp;-bb.Min;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">BoundarySegment</span>&gt;&nbsp;loop&nbsp;<span style="color:blue;">in</span>&nbsp;boundary&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">BoundarySegment</span>&nbsp;seg&nbsp;<span style="color:blue;">in</span>&nbsp;loop&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;c&nbsp;=&nbsp;seg.GetCurve();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;&nbsp;pts&nbsp;=&nbsp;c.Tessellate();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p&nbsp;<span style="color:blue;">in</span>&nbsp;pts&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bb.ExpandToContain(&nbsp;p&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;bb;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;List&nbsp;some&nbsp;properties&nbsp;of&nbsp;a&nbsp;given&nbsp;room&nbsp;to&nbsp;the</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Visual&nbsp;Studio&nbsp;debug&nbsp;output&nbsp;window.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">void</span>&nbsp;ListRoomData(&nbsp;<span style="color:#2b91af;">Room</span>&nbsp;room&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SpatialElementBoundaryOptions</span>&nbsp;opt
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">SpatialElementBoundaryOptions</span>();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;nr&nbsp;=&nbsp;room.Number;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;name&nbsp;=&nbsp;room.Name;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;area&nbsp;=&nbsp;room.Area;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Location</span>&nbsp;loc&nbsp;=&nbsp;room.Location;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">LocationPoint</span>&nbsp;lp&nbsp;=&nbsp;loc&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationPoint</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p&nbsp;=&nbsp;(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;lp&nbsp;)&nbsp;?&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero&nbsp;:&nbsp;lp.Point;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;bb&nbsp;=&nbsp;room.get_BoundingBox(&nbsp;<span style="color:blue;">null</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">BoundarySegment</span>&gt;&gt;&nbsp;boundary
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;room.GetBoundarySegments(&nbsp;opt&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;nLoops&nbsp;=&nbsp;boundary.Count;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;nFirstLoopSegments&nbsp;=&nbsp;0&nbsp;&lt;&nbsp;nLoops
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;boundary[0].Count
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;0;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;boundary_bounding_box
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;GetBoundingBox(&nbsp;boundary&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Room&nbsp;nr.&nbsp;&#39;{0}&#39;&nbsp;named&nbsp;&#39;{1}&#39;&nbsp;at&nbsp;{2}&nbsp;with&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;lower&nbsp;left&nbsp;corner&nbsp;{3},&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;bounding&nbsp;box&nbsp;{4}&nbsp;and&nbsp;area&nbsp;{5}&nbsp;sqf&nbsp;has&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;{6}&nbsp;loop{7}&nbsp;and&nbsp;{8}&nbsp;segment{9}&nbsp;in&nbsp;first&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;loop.&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nr,&nbsp;name,&nbsp;<span style="color:#2b91af;">Util</span>.PointString(&nbsp;p&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.PointString(&nbsp;boundary_bounding_box.Min&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BoundingBoxString2(&nbsp;bb&nbsp;),&nbsp;area,&nbsp;nLoops,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;nLoops&nbsp;),&nbsp;nFirstLoopSegments,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;nFirstLoopSegments&nbsp;)&nbsp;)&nbsp;);
&nbsp;&nbsp;}
</pre>

That generates the following result for as simple test with ten rooms, showing that the result from the room boundary edges is identical with the standard, simpler and more efficiently Revit element accessible bounding box for these simple rectangular samples: 

<pre> 
Room nr. '1' named 'Room 1' at (-31.85,19.49,0) with lower left corner (-39.74,13.41,0), bounding box ((-39.74,13.41,0),(-20.71,25.88,13.12)) and area 237.236585584282 sqf has 1 loop and 4 segments in first loop.
Room nr. '2' named 'Room 2' at (-31.85,5.67,0) with lower left corner (-39.74,0.29,0), bounding box ((-39.74,0.29,0),(-20.71,12.76,13.12)) and area 237.236585584282 sqf has 1 loop and 4 segments in first loop.
Room nr. '3' named 'Room 3' at (-16.54,19.49,0) with lower left corner (-20.05,13.41,0), bounding box ((-20.05,13.41,0),(-10.87,25.88,13.12)) and area 114.528006833791 sqf has 1 loop and 4 segments in first loop.
Room nr. '4' named 'Room 4' at (-16.54,5.67,0) with lower left corner (-20.05,0.29,0), bounding box ((-20.05,0.29,0),(-10.87,12.76,13.12)) and area 114.528006833791 sqf has 1 loop and 4 segments in first loop.
Room nr. '5' named 'Room 5' at (-4.97,19.49,0) with lower left corner (-10.21,13.41,0), bounding box ((-10.21,13.41,0),(-1.02,25.88,13.12)) and area 114.528006833791 sqf has 1 loop and 4 segments in first loop.
Room nr. '6' named 'Room 6' at (-4.97,5.67,0) with lower left corner (-10.21,0.29,0), bounding box ((-10.21,0.29,0),(-1.02,12.76,13.12)) and area 114.528006833791 sqf has 1 loop and 4 segments in first loop.
Room nr. '7' named 'Room 7' at (3.62,19.49,0) with lower left corner (-0.37,13.41,0), bounding box ((-0.37,13.41,0),(8.82,25.88,13.12)) and area 114.528006833791 sqf has 1 loop and 4 segments in first loop.
Room nr. '8' named 'Room 8' at (3.62,5.67,0) with lower left corner (-0.37,0.29,0), bounding box ((-0.37,0.29,0),(8.82,12.76,13.12)) and area 114.528006833791 sqf has 1 loop and 4 segments in first loop.
Room nr. '9' named 'Room 9' at (14.45,19.49,0) with lower left corner (9.47,13.41,0), bounding box ((9.47,13.41,0),(18.66,25.88,13.12)) and area 114.528006833791 sqf has 1 loop and 4 segments in first loop.
Room nr. '10' named 'Room 10' at (14.45,5.67,0) with lower left corner (9.47,0.29,0), bounding box ((9.47,0.29,0),(18.66,12.76,13.12)) and area 114.528006833789 sqf has 1 loop and 4 segments in first loop.
</pre>

In other words, I believe that the second and third suggestions above will always generate the same results.


#### <a name="8"></a>Bounding Box of Selected Elements or Entire Model

Another issue revolving around bounding boxes for selected elements or the entire model extents was raised in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread 
on [how to get the total width and length of the design](http://forums.autodesk.com/t5/revit-api/how-to-get-the-total-width-and-length-of-the-design-using-revit/m-p/6419530):

**Question:** I want to get the total width (along x-axis) and the total depth/length (along y-axis) of the given design.

Does anyone know what would be the best way to achieve it using Revit API?

**Answer:** The number of possible approaches is infinite.
 
I would probably start off
by [computing the convex hull](http://thebuildingcoder.typepad.com/blog/2009/06/convex-hull-and-volume-computation.html) of
the entire design, and then determine the width and length of that.
 
Another approach, which may or may not be faster, depending on many factors:
 
Skip the convex hull part and just iterate over all building elements, enlarging a bounding box containing all their vertices as you go along:
 
<pre>
  bounding box B = (+infinity, -infinity)

  for all elements
    for all element geometry vertices P
      if P < B.min: B.min = P
      if P > B.max: B.max = P
</pre>

Another approach, definitely faster than the last:
 
Use the element bounding box vertices instead of all the element geometry vertices.
 
All of these approaches would make very nice little samples.
 
There are certainly more ways to approach this task. 
 
I have a strong personal tendency towards the pure geometric.
 
Implement them and provide the code and a couple of examples designs to test them on, including extreme cases, and I'll clean it up and publish it for you on The Building Coder.
 
Funnily enough, a similar question was raised shortly afterwards by another developer:
 
**Question:** Is there a built-in method to get a bounding box of the entire model, similar to the method `Element.get_BoundingBox`?
 
The only way I see right now is to use `IExportContext`, go through all the visible elements and get the minimum and maximum coordinates among the all points. But on the large models this method may take a while.
 
Is there some faster method?
 
**Answer:** Yes, almost certainly.
 
I already suggested three different approaches above.
 
One aspect that I did not mention that makes a huge difference for large models:
 
The bounding box of all elements is immediately available from the element header information, and thus corresponds to a <b><u>quick</u></b> filtering method, whereas all the approaches that require the full geometry correspond to a <b><u>slow</u></b> filter.
 
Using a custom exporter, like you suggest, is also a good idea, and corresponds to a slow filter.
 
If you are interested in good performance in large models, I would aim for a quick filter method, e.g., like this, using the bounding box extension methods provided above:

<pre class="code">
<span style="color:blue;">#region</span>&nbsp;Get&nbsp;Model&nbsp;Extents
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;a&nbsp;bounding&nbsp;box&nbsp;enclosing&nbsp;all&nbsp;model&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;elements&nbsp;using&nbsp;only&nbsp;quick&nbsp;filters.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;GetModelExtents(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;quick_model_elements
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsViewIndependent();
 
&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">BoundingBoxXYZ</span>&gt;&nbsp;bbs&nbsp;=&nbsp;quick_model_elements
&nbsp;&nbsp;&nbsp;&nbsp;.Where&lt;<span style="color:#2b91af;">Element</span>&gt;(&nbsp;e&nbsp;=&gt;&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;e.Category&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Select&lt;<span style="color:#2b91af;">Element</span>,<span style="color:#2b91af;">BoundingBoxXYZ</span>&gt;(&nbsp;e&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;e.get_BoundingBox(&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;bbs.Aggregate&lt;<span style="color:#2b91af;">BoundingBoxXYZ</span>&gt;(&nbsp;(&nbsp;a,&nbsp;b&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;{&nbsp;a.ExpandToContain(&nbsp;b&nbsp;);&nbsp;<span style="color:blue;">return</span>&nbsp;a;&nbsp;}&nbsp;);
}
<span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;Model&nbsp;Extents</span>
</pre>
 
I implemented that for you and included it
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2017.0.127.6](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.127.6).
 
**Response:** So, in fact I was right and the only method is to get this information from the geometry.
 
I’ll benchmark with CustomExport (as I suggested) and FilteredElementCollector (as you did) with large models and let you know.
 
I think your way should be faster, but not correct in some cases:
 
1. Linked models. It doesn’t consider linked models if they are.
2. Even if we extend this method and get the elements geometry from the linked models, we need to transform the coordinates from linked model to the hosted model, as BoundingBox returns the coordinates in a source model.
3. Your method returns all the elements. In my case I need only the view specific Model BoundingBox. Yes, we can use FilteredElementCollector with ActiveView, but it can be a section view or 3d view bounded by section box.
 
Therefore, FilteredElementCollector should be faster, but needs to consider all the cases (maybe there are more than I mentioned). As CustomExporter iterate only the visible geometry, exactly like it presented on the view, this way is more reliable.
 
Anyway, the benchmark will show the results &ndash; :-)

#### <a name="9"></a>Setting 3D Section Box to Selected Elements' Extents

Finally, my colleague Jim Jia just answered a related developer support issue on setting the section box of a 3D view to the extents of a selected element:

**Question:** I use `uiDoc.Selection.SetElementIds (elemIds); view3D.IsSectionBoxActive = true;` to show the section box, but that displays the entire project.

What I want is to display a selected element in the section box.

How can this be achieved, please?

**Answer:** You can set the size of the section box yourself.

If you want the section box to display only the currently selected element, simply calculate the appropriately sized `BoundingBoxXYZ` by retrieving the bounding box of the selected element.

Here is an example:

<pre class="code">
<span style="color:blue;">#region</span>&nbsp;SetSectionBox
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Set&nbsp;3D&nbsp;view&nbsp;section&nbsp;box&nbsp;to&nbsp;selected&nbsp;element&nbsp;extents.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SectionBox(<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;view&nbsp;=&nbsp;doc.ActiveView;
 
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;Min_X&nbsp;=&nbsp;<span style="color:blue;">double</span>.MaxValue;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;Min_Y&nbsp;=&nbsp;<span style="color:blue;">double</span>.MaxValue;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;Min_Z&nbsp;=&nbsp;<span style="color:blue;">double</span>.MaxValue;
 
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;Max_X&nbsp;=&nbsp;Min_X;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;Max_Y&nbsp;=&nbsp;Min_Y;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;Max_Z&nbsp;=&nbsp;Min_Z;
 
&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;ids&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;uidoc.Selection.GetElementIds();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;id&nbsp;<span style="color:blue;">in</span>&nbsp;ids&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;elm&nbsp;=&nbsp;doc.GetElement(&nbsp;id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;box&nbsp;=&nbsp;elm.get_BoundingBox(&nbsp;view&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;box.Max.X&nbsp;&gt;&nbsp;Max_X&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Max_X&nbsp;=&nbsp;box.Max.X;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;box.Max.Y&nbsp;&gt;&nbsp;Max_Y&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Max_Y&nbsp;=&nbsp;box.Max.Y;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;box.Max.Z&nbsp;&gt;&nbsp;Max_Z&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Max_Z&nbsp;=&nbsp;box.Max.Z;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;box.Min.X&nbsp;&lt;&nbsp;Min_X&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Min_X&nbsp;=&nbsp;box.Min.X;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;box.Min.Y&nbsp;&lt;&nbsp;Min_Y&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Min_Y&nbsp;=&nbsp;box.Min.Y;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;box.Min.Z&nbsp;&lt;&nbsp;Min_Z&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Min_Z&nbsp;=&nbsp;box.Min.Z;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;Max&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;Max_X,&nbsp;Max_Y,&nbsp;Max_Z&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;Min&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;Min_X,&nbsp;Min_Y,&nbsp;Min_Z&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;myBox&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>();
 
&nbsp;&nbsp;myBox.Min&nbsp;=&nbsp;Min;
&nbsp;&nbsp;myBox.Max&nbsp;=&nbsp;Max;
 
&nbsp;&nbsp;(&nbsp;view&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">View3D</span>&nbsp;).SetSectionBox(&nbsp;myBox&nbsp;);
}
<span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;SetSectionBox</span>
</pre>

This method could obviously be simplified a bit by making use of the `ExpandToContain` extension methods above.

There you are.

Now I am starting to get back to normal again.

Everything discussed above is now online and live 
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2017.0.127.8](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.127.8).

Lots more stuff coming up.

Have fun!


Dear Bilal,

Thank you for your appreciation.

I summarised, cleaned up and published our conversation on The Building Coder:

http://thebuildingcoder.typepad.com/blog/2016/08/vacation-end-forge-news-and-bounding-boxes.html#8

Cheers,

Jeremy