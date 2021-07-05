<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- for all rooms, map all their bounding walls to the neighbouring rooms; then, add a list of all neighbouring rooms to the comment field on each wall 
  https://forums.autodesk.com/t5/revit-api-forum/extract-the-names-of-the-rooms-separated-by-a-wall/m-p/10428696
  another example of a relationship inversion:
  thee room maintains a relationship to its bounding elements, the walls.
  by retrieving that mapping, we can int=vert the raltionship and add information to each wall about its adjacent rooms.
  A [Relationship Inverter](http://thebuildingcoder.typepad.com/blog/2008/10/relationship-in.html) was
  the topic of one of The Building Coder's very first posts, #16, in October 2008.  

- get all warnings
  https://forums.autodesk.com/t5/revit-api-forum/get-a-list-of-all-the-revit-warnings/m-p/10399203
   schnierer.gabor
  Gábor Schnierer
  > Using the awesome code
  from [@FAIR59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518)
  and [@perry.swoboda](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/7186046) here
  is a spreadsheet containing
  the [BuiltInFailures for Revit 2022](https://docs.google.com/spreadsheets/d/12glULCZL_yJkq7ko_vI-gEHu69dUIoiCRnmvdLpIoSU/edit#gid=0) with
  their Severity, Classname, Guid and Description. Might come handy.

- revitlookup updates + installation

twitter:

add #thebuildingcoder

 with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/assetkeyword


&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Question:** 

**Answer:**

**Response:**  

Many thanks to  for this very helpful explanation!

-->

### Installer, Warning List, Adjacent Rooms and Walls

A number of useful and important enhancement to RevitLookup, a list of all built-in Revit failures, and a neat utility to determine and document all room-wall adjacencies:



<center>
<img src="img/" alt="" title="" width="100"/> <!-- 850 -->
</center>

####<a name="2"></a> Adjacent Rooms and Walls

https://forums.autodesk.com/t5/revit-api-forum/extract-the-names-of-the-rooms-separated-by-a-wall/m-p/10428696

**Answer:**


for all rooms, map all their bounding walls to the neighbouring rooms; then, add a list of all neighbouring rooms to the comment field on each wall 


<pre class="code">
<pre>
<span style="color:green;">//&nbsp;Originally&nbsp;implemented&nbsp;by&nbsp;Richard&nbsp;@RPThomas108&nbsp;Thomas&nbsp;in&nbsp;VB.NET&nbsp;in</span>
<span style="color:green;">//&nbsp;https://forums.autodesk.com/t5/revit-api-forum/extract-the-names-of-the-rooms-separated-by-a-wall/m-p/10428696</span>
 
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;For&nbsp;all&nbsp;rooms,&nbsp;determine&nbsp;all&nbsp;adjacent&nbsp;walls,</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;create&nbsp;dictionary&nbsp;mapping&nbsp;walls&nbsp;to&nbsp;adjacent&nbsp;rooms,</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;and&nbsp;tag&nbsp;the&nbsp;walls&nbsp;with&nbsp;the&nbsp;adjacent&nbsp;room&nbsp;names.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">void</span>&nbsp;TagWallsWithAdjacentRooms(&nbsp;Document&nbsp;doc&nbsp;)
{
&nbsp;&nbsp;FilteredElementCollector&nbsp;rooms
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FilteredElementCollector(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;BuiltInCategory.OST_Rooms&nbsp;);
 
&nbsp;&nbsp;Dictionary&lt;ElementId,&nbsp;List&lt;<span style="color:blue;">string</span>&gt;&gt;&nbsp;map_wall_to_rooms
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Dictionary&lt;ElementId,&nbsp;List&lt;<span style="color:blue;">string</span>&gt;&gt;();
 
&nbsp;&nbsp;SpatialElementBoundaryOptions&nbsp;opts
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;SpatialElementBoundaryOptions();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;Room&nbsp;room&nbsp;<span style="color:blue;">in</span>&nbsp;rooms&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;IList&lt;IList&lt;BoundarySegment&gt;&gt;&nbsp;loops&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;room.GetBoundarySegments(&nbsp;opts&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>&nbsp;(IList&lt;BoundarySegment&gt;&nbsp;loop&nbsp;<span style="color:blue;">in</span>&nbsp;loops&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;BoundarySegment&nbsp;seg&nbsp;<span style="color:blue;">in</span>&nbsp;loop&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElementId&nbsp;idWall&nbsp;=&nbsp;seg.ElementId;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ElementId.InvalidElementId&nbsp;!=&nbsp;idWall&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(!map_wall_to_rooms.ContainsKey(idWall))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map_wall_to_rooms.Add(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;idWall,&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;<span style="color:blue;">string</span>&gt;()&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;room_name&nbsp;=&nbsp;room.Name;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(!map_wall_to_rooms[idWall].Contains(&nbsp;room_name&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map_wall_to_rooms[&nbsp;idWall&nbsp;].Add(&nbsp;room_name&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;Transaction&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Transaction(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Add&nbsp;list&nbsp;of&nbsp;adjacent&nbsp;rooms&nbsp;to&nbsp;wall&nbsp;comments&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;Dictionary&lt;ElementId,&nbsp;List&lt;<span style="color:blue;">string</span>&gt;&gt;.KeyCollection&nbsp;ids
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;map_wall_to_rooms.Keys;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;ElementId&nbsp;id&nbsp;<span style="color:blue;">in</span>&nbsp;ids&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Element&nbsp;wall&nbsp;=&nbsp;doc.GetElement(&nbsp;id&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Parameter&nbsp;p&nbsp;=&nbsp;wall.get_Parameter(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;p&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s&nbsp;=&nbsp;<span style="color:blue;">string</span>.Join(&nbsp;<span style="color:#a31515;">&quot;&nbsp;/&nbsp;&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map_wall_to_rooms[&nbsp;id&nbsp;]&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p.Set(&nbsp;s&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;}
}
</pre>

This is yet another example of a relationship inversion:
every room maintains a relationship to its bounding elements, the walls.
by retrieving and processing that mapping, we can invert the relationship and use that to add information to each wall about its adjacent rooms.

This is a very common Revit API task. 
A [relationship inverter](http://thebuildingcoder.typepad.com/blog/2008/10/relationship-in.html) was
the topic of one of The Building Coder's very first posts, #16, in October 2008.  


####<a name="3"></a> 

- get all warnings
https://forums.autodesk.com/t5/revit-api-forum/get-a-list-of-all-the-revit-warnings/m-p/10399203
 schnierer.gabor
Gábor Schnierer
> Using the awesome code
from [@FAIR59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518)
and [@perry.swoboda](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/7186046) here
is a spreadsheet containing
the [BuiltInFailures for Revit 2022](https://docs.google.com/spreadsheets/d/12glULCZL_yJkq7ko_vI-gEHu69dUIoiCRnmvdLpIoSU/edit#gid=0) with
their Severity, Classname, Guid and Description. Might come handy.

####<a name="4"></a> Recent RevitLookup Updates

The number of pull requests to add enhancements 
to [RevitLookup](https://github.com/jeremytammik/RevitLookup) has
increased recently significantly.

That is great news!

Each individual improvement may be small and simple.
However, they all add up, and the entire community ends up enjoying a brilliant and full-fledged tool.

Here are the important enhancements made since 
the [previous bunch of updates](https://thebuildingcoder.typepad.com/blog/2021/05/revitlookup-update-fuslogvw-and-override-joins.html). 

- [2022.0.0.10](https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.10) fix error where element cannot be retrieved for an element id because `SupportedColorFillCategoryIds` returns category ids instead
- [2022.0.0.11](https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.11) add `PlanViewRange` functionality to display view range level id and offset
- [2022.0.0.13](https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.13) add `OnLoad` to increrase and optimise width of Snoop window value `ListView` last column
- [2022.0.0.15](https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.15) added RevitLookup.Installation

Many thanks to
all [contributors](https://github.com/jeremytammik/RevitLookup/graphs/contributors) for
your great support!

####<a name="5"></a> RevitLookup Installation

Luiz Henrique [@ricaun](https://github.com/ricaun) Cassettari created
the [RevitLookup.Installation](https://github.com/ricaun/RevitLookup.Installation) project,
a simple installation using [Inno Setup](https://jrsoftware.org/isinfo.php) to
extract the files to the `ApplicationPlugins` folder.

It generates a digitally signed version of RevitLookup and supports the Revit releases 2017, 2018, 2019, 2020, 2021 and 2022.

This can obviously also be used as a starting point for your own add-in installer.

Many thanks to Luiz Henrique for this and his other nice contributions!

