<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- eliminated deprecated API usage warnings by removing calls to pre-ForgeTypeId unit functionality
  https://github.com/jeremytammik/the_building_coder_samples/compare/2021.0.150.5...2021.0.150.6

- How to calculate the center point of elbow?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-calculate-the-center-point-of-elbow/m-p/9804339
  elbow_arc_centre_point_1.png + 2

- Neo Sheng <wc36170565@gmail.com>
  FireRevit: Using Revit files to identify the room locations of fires and escape routes
  https://forums.autodesk.com/t5/revit-api-forum/firerevit-using-revit-files-to-identify-the-room-locations-of/td-p/9809450
  FireRevit GitHub repository https://github.com/LuhanSheng/Revit_To_Database
  FireRevit: Using Revit Files to Identify the Room Locations of Fires and Escape Routes.
  https://cs.nyu.edu/media/publications/RevitToDatabase.pdf
  1468_hololens_exitpath.md

twitter:

Revit 2021 DisplayUnitType, deprecated API usage, calculating the elbow centre and FireRevit app to identify room locations for fire escape routes  in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/elbowcentre

Topics for today
&ndash; Revit 2021 <code>DisplayUnitType</code>
&ndash; Eliminated TBC samples deprecated API usage
&ndash; Calculating the elbow centre
&ndash; FireRevit identifies room location for fire escape routes...

linkedin:

Revit 2021 DisplayUnitType, deprecated API usage, calculating the elbow centre and FireRevit app to identify room locations for fire escape routes  in the #RevitAPI

http://bit.ly/elbowcentre

Topics for today:

- Revit 2021 DisplayUnitType
- Eliminated TBC samples deprecated API usage
- Calculating the elbow centre
- FireRevit identifies room location for fire escape routes...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### FireRevit, Deprecated API and Elbow Centre Point

Struggling to find time to blog between cases, here is today's ration:

- [Revit 2021 `DisplayUnitType`](#2)
- [Eliminated TBC samples deprecated API usage](#3)
- [Calculating the elbow centre](#4)
- [FireRevit identifies room location for fire escape routes](#5)

####<a name="2"></a> Revit 2021 DisplayUnitType

Stephen Harrison raised and solved an issue on handling 
[Revit 2021 `DisplayUnitType`](https://forums.autodesk.com/t5/revit-api-forum/revit-2021-displayunittype/m-p/9810626):

**Question:** I am struggling to update a section of my code I have been utilising for a few years now.

In Revit 2021, it is causing a deprecated API usage warning, so I need to update it:

- warning CS0618: `DisplayUnitType` is obsolete:
This enumeration is deprecated in Revit 2021 and may be removed in a future version of Revit.
Please use the `ForgeTypeId` class instead.
Use constant members of the `UnitTypeId` class to replace uses of specific values of this enumeration.

My code snippet is:

<pre class="code">
  <span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.Double:
  &nbsp;&nbsp;<span style="color:blue;">double</span>?&nbsp;nullable&nbsp;=&nbsp;t.AsDouble(&nbsp;fp&nbsp;);
  &nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;nullable.HasValue&nbsp;)
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DisplayUnitType</span>&nbsp;displayUnitType&nbsp;=&nbsp;fp.DisplayUnitType;
  &nbsp;&nbsp;&nbsp;&nbsp;value&nbsp;=&nbsp;<span style="color:#2b91af;">UnitUtils</span>.ConvertFromInternalUnits(&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nullable.Value,&nbsp;displayUnitType&nbsp;).ToString();
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
  &nbsp;&nbsp;}
</pre>

Note: `t` is a `FamilyType` and `fp` is a `FamilyParameter` object.

**Answer:** Embarrassing how simple the solution was:

<pre class="code">
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Pre&nbsp;2021</span>

&nbsp;&nbsp;<span style="color:#2b91af;">DisplayUnitType</span>&nbsp;displayUnitType&nbsp;=&nbsp;fp.DisplayUnitType;
&nbsp;&nbsp;value&nbsp;=&nbsp;<span style="color:#2b91af;">UnitUtils</span>.ConvertFromInternalUnits(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;nullable.Value,&nbsp;displayUnitType&nbsp;).ToString();
 
&nbsp;&nbsp;<span style="color:green;">//2021</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">ForgeTypeId</span>&nbsp;forgeTypeId&nbsp;=&nbsp;fp.GetUnitTypeId();
&nbsp;&nbsp;value&nbsp;=&nbsp;<span style="color:#2b91af;">UnitUtils</span>.ConvertFromInternalUnits(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;nullable.Value,&nbsp;forgeTypeId&nbsp;).ToString();
</pre>

Many thanks to Stephen for sharing this!

####<a name="3"></a> Eliminated TBC Samples Deprecated API Usage

Stephen's question and answer prompted me to take another look at and try to eliminate the deprecated API usage warnings
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).
After the initial [flat migration to Revit 2021](https://thebuildingcoder.typepad.com/blog/2020/04/2021-migration-add-in-language-and-bim360-login.html#4),
the compilation still generates [162 warnings](zip/tbc_samples_2021_migr_01.txt),
all associated with deprecated methods and enumerations caused by 
the [Units API changes](https://thebuildingcoder.typepad.com/blog/2020/04/whats-new-in-the-revit-2021-api.html#4.1.3).

The resolution was actually quite simple.

I removed some sections of code completely that dealt exclusively with the Revit Unit API functionality, since they ought to be rewritten from scratch for the new unit handling methods. It would make little sense to migrate them step by step. That left a handful of trivial issues to fix.

The new [release 2021.0.150.6](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2021.0.150.6) compiles with zero errors and warnings.

You can see exactly how that was achieved by analysing
the [differences to the previous release](https://github.com/jeremytammik/the_building_coder_samples/compare/2021.0.150.5...2021.0.150.6).

####<a name="4"></a> Calculating the Elbow Centre

From
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to calculate the centre point of an elbow](https://forums.autodesk.com/t5/revit-api-forum/how-to-calculate-the-center-point-of-elbow/m-p/9804339):

**Question:** How to calculate the centre point of elbow?

I am trying to get to centre point of an elbow.
I have checked all the data of the elbow in RevitLookup but did not find anything.
So, is there any way I can get that information?

<center>
<img src="img/elbow_arc_centre_point_1.png" alt="Elbow arc centre point" title="Elbow arc centre point" width="400"/> <!-- 790 -->
</center>

**Answer:** Yes.

This is a pretty simple geometrical exercise.

You have the full information about the start and end point, specifically including location and direction == face normal.

The start and end point directions are not parallel. Therefore, they define a 2D plane. The centre point will normally lie in that plane, so you just have a (simpler) 2D task to solve, not 3D.

In the 2D plane, you can determine the normal vector to the start and end directions. Extend those two normal vectors to define infinite lines and determine their intersection. That is your centre point.

**Response:** Thank you for your answer.
I just found out that actually the elbow already has the centre point information under its Geometry information:

<center>
<img src="img/elbow_arc_centre_point_2.png" alt="Snooping elbow arc centre point" title="Snooping elbow arc centre point" width="800"/> <!-- 1194 -->
</center>

So, my working code is:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;GetCenterofElbow(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;selectedDuct&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;output&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;List&lt;<span style="color:#2b91af;">Connector</span>&gt;&nbsp;allConnectors&nbsp;=&nbsp;selectedDuct.MEPModel
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ConnectorManager.Connectors
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">Connector</span>&gt;().ToList();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Connector</span>&nbsp;connectorA&nbsp;=&nbsp;allConnectors[&nbsp;0&nbsp;];
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Connector</span>&nbsp;connectorB&nbsp;=&nbsp;allConnectors[&nbsp;0&nbsp;];
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">GeometryElement</span>&nbsp;geometryElement&nbsp;=&nbsp;selectedDuct
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.get_Geometry(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;List&lt;<span style="color:#2b91af;">GeometryInstance</span>&gt;&nbsp;ginsList&nbsp;=&nbsp;selectedDuct
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.get_Geometry(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;o&nbsp;=&gt;&nbsp;o&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">GeometryInstance</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">GeometryInstance</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GeometryInstance</span>&nbsp;gins&nbsp;<span style="color:blue;">in</span>&nbsp;ginsList&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GeometryObject</span>&nbsp;ge&nbsp;<span style="color:blue;">in</span>&nbsp;gins.GetInstanceGeometry()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Arc</span>&nbsp;centerArc&nbsp;=&nbsp;ge&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Arc</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;output&nbsp;=&nbsp;centerArc.Center;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;<span style="color:#2b91af;">Exception</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;output;
&nbsp;&nbsp;}
</pre>

**Answer:** Thank you for letting us know that simple solution!

Oh dear. I wasted some effort, then.

I implemented a geometrical solution for you based on the elbow connectors and their coordinate systems:

- [Connector Origin](https://www.revitapidocs.com/2020/28a9cf5e-9191-f9ce-74c8-622a681201f6.htm)
- [CoordinateSystem property returning a Transform object](https://www.revitapidocs.com/2020/cb6d725d-654a-f6f3-fed0-96cc618a42f1.htm)
- [New `GetElbowConnectors` and `GetElbowCentre` methods in The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdRectDuctCorners.cs#L240-L319) 

<pre class="code">
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;elbow&nbsp;connectors.</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;null&nbsp;if&nbsp;the&nbsp;given&nbsp;element&nbsp;is&nbsp;not&nbsp;a&nbsp;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;family&nbsp;instance&nbsp;with&nbsp;exactly&nbsp;two&nbsp;connectors.</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Connector</span>&gt;&nbsp;GetElbowConnectors(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Connector</span>&gt;&nbsp;cons&nbsp;=&nbsp;<span style="color:blue;">null</span>;
  &nbsp;&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;fi&nbsp;=&nbsp;e&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">FamilyInstance</span>;
  &nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;fi&nbsp;)
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">MEPModel</span>&nbsp;m&nbsp;=&nbsp;fi.MEPModel;
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;m&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ConnectorManager</span>&nbsp;cm&nbsp;=&nbsp;m.ConnectorManager;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;cm&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ConnectorSet</span>&nbsp;cs&nbsp;=&nbsp;cm.Connectors;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;2&nbsp;==&nbsp;cs.Size&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cons&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Connector</span>&gt;(&nbsp;2&nbsp;);
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;first&nbsp;=&nbsp;<span style="color:blue;">true</span>;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Connector</span>&nbsp;c&nbsp;<span style="color:blue;">in</span>&nbsp;cs&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;first&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cons[0]&nbsp;=&nbsp;c;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cons[1]&nbsp;=&nbsp;c;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;}
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;cons;
  }
   
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;elbow&nbsp;centre&nbsp;point.</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;null&nbsp;if&nbsp;the&nbsp;start&nbsp;and&nbsp;end&nbsp;points&nbsp;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;and&nbsp;direction&nbsp;vectors&nbsp;are&nbsp;not&nbsp;all&nbsp;coplanar.</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:#2b91af;">XYZ</span>&nbsp;GetElbowCentre(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pc&nbsp;=&nbsp;<span style="color:blue;">null</span>;
  &nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Connector</span>&gt;&nbsp;cons&nbsp;=&nbsp;GetElbowConnectors(&nbsp;e&nbsp;);
  &nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;cons&nbsp;)
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;start&nbsp;and&nbsp;end&nbsp;point&nbsp;and&nbsp;direction</span>
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;ps&nbsp;=&nbsp;cons[&nbsp;0&nbsp;].CoordinateSystem.Origin;
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;vs&nbsp;=&nbsp;cons[&nbsp;0&nbsp;].CoordinateSystem.BasisZ;
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pe&nbsp;=&nbsp;cons[&nbsp;1&nbsp;].CoordinateSystem.Origin;
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;ve&nbsp;=&nbsp;cons[&nbsp;1&nbsp;].CoordinateSystem.BasisZ;
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;vd&nbsp;=&nbsp;pe&nbsp;-&nbsp;ps;
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;For&nbsp;a&nbsp;regular&nbsp;elbow,&nbsp;Z&nbsp;vector&nbsp;is&nbsp;normal&nbsp;</span>
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;of&nbsp;the&nbsp;2D&nbsp;plane&nbsp;spanned&nbsp;by&nbsp;the&nbsp;coplanar</span>
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;start&nbsp;and&nbsp;end&nbsp;points&nbsp;and&nbsp;direction&nbsp;vectors.</span>
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;vz&nbsp;=&nbsp;vs.CrossProduct(&nbsp;vd&nbsp;);
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!vz.IsZeroLength()&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;vxs&nbsp;=&nbsp;vs.CrossProduct(&nbsp;vz&nbsp;);
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;vxe&nbsp;=&nbsp;ve.CrossProduct(&nbsp;vz&nbsp;);
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pc&nbsp;=&nbsp;<span style="color:#2b91af;">Util</span>.LineLineIntersection(&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ps,&nbsp;vxs,&nbsp;pe,&nbsp;vxe&nbsp;);
  &nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;}
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;pc;
  }
</pre>

Would you like to test my code and see whether it returns the same result?

By the way, some elbow families have no arc segment at all.

For example, some can consist of two 45-degree segments, connected by a cylindrical part in between.

In this case, there would be two arcs with different centre points, so the connector based approach seems more flexible for different family content.

For example, here is a typical [German chimney exhaust elbow](https://www.ofenseite.com/1020147-pelletofenrohr-90-bogen-mit-kesselanschluss-muffe)

<center>
<img src="img/elbow_arc_centre_point_3.jpg" alt="Chimney 90 degree elbow" title="Chimney 90 degree elbow" width="250"/> <!-- 600 -->
</center>

In this case, there isn't any arc at all.

The connector-based code should solve the task for that as well.

Please confirm that it works.

The code is untested.

####<a name="5"></a> FireRevit Identifies Room Location for Fire Escape Routes

[Luhan Neo Sheng](mailto:Neo Sheng <wc36170565@gmail.com>) shared a research project with potential practical applications in
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [FireRevit: Using Revit files to identify the room locations of fires and escape routes](https://forums.autodesk.com/t5/revit-api-forum/firerevit-using-revit-files-to-identify-the-room-locations-of/td-p/9809450):

FireRevit is software that parses Revit files in order to map from a location given as (latitude, longitude, height) to a Revit room identifier.

FireRevit also combines the Revit file and information about which rooms are inaccessible in an emergency to guide residents to the nearest exit by giving a route map.

Our application use case is firefighting where drones identify the locations of rooms having fires both to direct firefighters to rooms on fire and to help residents escape. 

We believe this could be useful in general for Revit users.

Here is [our technical report RevitToDatabase.pdf](https://cs.nyu.edu/media/publications/RevitToDatabase.pdf)
([local copy](zip/firerevit_room_location.pdf)) and 
the source code in the [LuhanSheng Revit_To_Database GitHub repository](https://github.com/LuhanSheng/Revit_To_Database).

Noteworthy related topics:

- Kean Walmsley implemented a [HoloLens-based tool for navigating low visibility environments and following an exit path](https://thebuildingcoder.typepad.com/blog/2016/09/hololens-escape-path-waypoint-json-exporter.html)
- Revit 2020 enhanced the Analysis API with support for a [Path of Travel API](https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html#4.2.19)
- The Revit SDK includes a [PathOfTravel SDK sample](https://thebuildingcoder.typepad.com/blog/2019/04/new-revit-2020-sdk-samples.html#5)

