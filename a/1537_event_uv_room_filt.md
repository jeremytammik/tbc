<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- http://aechackathon-germany.de/

- https://forums.autodesk.com/t5/revit-api-forum/collect-all-room-in-leve-xx/m-p/6939202
  tbc sample update
  filtering for a non-native class

Registration opened for Autodesk University in London #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/events_uv_rooms_level
Preparing the Forge Accelerator in Gotherburg #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/events_uv_rooms_level
Collect all rooms on a given level #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/events_uv_rooms_level

A lot of interesting solutions were shared in the Revit API discussion forum and private email messages during my absence last week, and several exciting events are looming
&ndash; Forge Accelerator in Gothenburg
&ndash; AEC Hackathon in Munich
&ndash; Autodesk University in London
&ndash; Retrieve and map texture UV coordinates exporting geometry and material
&ndash; Collect all rooms on a given level...

-->

### Events, UV Coordinates and Rooms on Level

A lot of interesting solutions were shared in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) and
private email messages during my absence last week, and several exciting events are looming:

- [Forge Accelerator in Gothenburg](#2)
- [AEC Hackathon in Munich](#3)
- [Autodesk University in London](#4)
- [Retrieve and map texture UV coordinates exporting geometry and material](#5)
- [Collect all rooms on a given level](#6)


#### <a name="2"></a>Forge Accelerator in Gothenburg

We have two [Forge accelerators](http://autodeskcloudaccelerator.com/) coming up in Europe
in the [next couple of months](http://autodeskcloudaccelerator.com/prague-2/):

- Gothenburg, Sweden &ndash; March 27-30
- Barcelona, Spain &ndash; June 12-16

I am planning to attend both and would love to see you there too.

In fact, the complete EMEA DevTech Team will be present to support you over the course of the week.

They take place in the Autodesk offices with lots of space and cool recreation areas.

Attendance is free of charge &ndash; all you pay for is your travel, accommodation and living expenses.  

You can still apply to participate in either of these two events by sending your Forge App development project ideas to [adn-training-worldwide@autodesk.com](mailto:adn-training-worldwide@autodesk.com).

More information is available at [autodeskcloudaccelerator.com](http://autodeskcloudaccelerator.com).

Check out the videos on the landing page to hear what great results the attendees at previous accelerators achieved after just one week of intensive teamwork and training.


#### <a name="3"></a>AEC Hackathon in Munich

Directly after the Gothenburg accelerator, March 31 - April 2,
the [AEC Hackathon Germany](http://aechackathon-germany.de) is
taking off at the Technical University Munich.

It gives everyone designing, building, and maintaining our built environment the opportunity to collaborate with cutting edge technologies and its developers and designers.

It’s a weekend of geeking at its finest for improving the industries that affect all that live or work in a house or building.

Jaime is going, and unfortunately, I am not...


#### <a name="4"></a>Autodesk University in London

As I mentioned,
[Autodesk University is coming to London](http://thebuildingcoder.typepad.com/blog/2017/01/au-in-london-and-deep-learning.html#2),
to Tobacco Dock, E1 on June 21-22, 2017;
the first English speaking Autodesk University in Northern Europe!

Registration for Autodesk University London 2017 is now open.
Number are limited and an early bird discount is available until April 14.
 
To get a taste of what we’ve got in store for you this year, check out the venue, view the agenda and timings and find out all you need to know, visit the [AU London website](https://gems.autodesk.com/events/au-london-2017/event-summary-9dba4a429f994dbab348c68dfad1ca6a.aspx).

<center>
<img src="img/2017_au_london_2.png" alt="AU London 2017" width="500"/>
</center>


#### <a name="5"></a>Retrieve and Map Texture UV Coordinates Exporting Geometry and Material

**Question:** I have a question looking at the rather outdated discussion
on [texture data UV coordinates and FBX](http://thebuildingcoder.typepad.com/blog/2010/02/texture-data-uv-coordinates-and-fbx.html):

I find that the API `Mesh.UVs` is not available in Revit 2014 or later.

How can I retrieve the mesh UVs now?

How can I map texture UV accurately when exporting Revit geometry and materials?

I already tried `Edge.TessellateOnFace(Face)` and `Face.GetBoundBoxingUV`; neither of them works as desired (not accurate).

<pre class="code">
  <span style="color:#2b91af;">Mesh</span>&nbsp;geomMesh&nbsp;=&nbsp;geomFace.Triangulate();
  XYZArray&nbsp;vertices&nbsp;=&nbsp;geomMesh.Vertices;
  UVArray&nbsp;uvs&nbsp;=&nbsp;geomMesh.UVs;&nbsp;<span style="color:green;">//&nbsp;This&nbsp;API&nbsp;is&nbsp;not&nbsp;available</span>
</pre>

**Answer:** You should use the `CustomExporter` &ndash; the `OnMaterial` and `OnPolymesh` methods are designed for these sorts of export operations.

`XYZArray` is way obsolete, and most custom Revit API collections have been replaced by generic ones in the past few years, e.g., `List<XYZ>`, in this case.


#### <a name="6"></a>Collect all Rooms on a Given Level

From
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [collecting all rooms in level xx](https://forums.autodesk.com/t5/revit-api-forum/collect-all-room-in-leve-xx/m-p/6939202):

**Question:** How can I collect all rooms on a given level?

**Answer 1:** You can't collect `Rooms` directly, you need to collect `SpatialElement` instead, its parent class, and then post-process the results:

<pre class="code">
  <span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Room</span>&gt;&nbsp;GetRoomFromLevel(&nbsp;
  &nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;document,&nbsp;
  &nbsp;&nbsp;<span style="color:#2b91af;">Level</span>&nbsp;level&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;Rooms&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;document&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">SpatialElement</span>&nbsp;)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;room&nbsp;=&gt;&nbsp;room.GetType()&nbsp;==&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Room</span>&nbsp;)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
   
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Room</span>&gt;(&nbsp;Rooms.Where(&nbsp;room&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;document.GetElement(&nbsp;room.LevelId&nbsp;)&nbsp;==&nbsp;level&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.Select(&nbsp;r&nbsp;=&gt;&nbsp;r&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Room</span>&nbsp;)&nbsp;);
  }
</pre>

The `Where` and `List<>` stuff comes from the `System.Collections.Generic` namespace.

**Answer 2:** Here is a new method `GetRoomsOnLevel` that I added
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2017.0.132.8](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.132.8) sporting
some small improvements:

<pre class="code">
  <span style="color:blue;">#region</span>&nbsp;Retrieve&nbsp;all&nbsp;rooms&nbsp;on&nbsp;a&nbsp;given&nbsp;level
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;Retrieve&nbsp;all&nbsp;rooms&nbsp;on&nbsp;a&nbsp;given&nbsp;level.</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Room</span>&gt;&nbsp;GetRoomsOnLevel(&nbsp;
  &nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,
  &nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;idLevel&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
  &nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">SpatialElement</span>&nbsp;)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;e&nbsp;=&gt;&nbsp;e.GetType()&nbsp;==&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Room</span>&nbsp;)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;e&nbsp;=&gt;&nbsp;e.LevelId.IntegerValue.Equals(&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;idLevel.IntegerValue&nbsp;)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">Room</span>&gt;();
  }
  <span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;all&nbsp;rooms&nbsp;on&nbsp;a&nbsp;given&nbsp;level</span>
</pre>

This is more efficient than the first version due to:
 
- Elimination of `ToList`
- Elimination of `New List<>`
- Elimination of `doc.GetElement`

The reason you cannot filter directly for `Room` elements is explained in the discussion 
on [filtering for a non-native class](http://thebuildingcoder.typepad.com/blog/2010/08/filtering-for-a-nonnative-class.html) and in
the [remarks on the `ElementClassFilter` class](http://www.revitapidocs.com/2017/4b7fb6d7-cb9c-d556-56fc-003a0b8a51b7.htm).
