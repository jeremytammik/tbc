<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- list all rooms, coordinate rvt rooms with forge viewer

- https://twitter.com/imaliasad/status/1078989674172035072
  Ali Asad ‏@imaliasad
  Excited to release VisualStudioRevitTemplate https://github.com/imAliAsad/VisualStudioRevitTemplate
  - Develop @AutodeskRevit add-ins using WPF
  - Ready to use MVVM code in your application

Happy New Year! Retrieve room boundaries for Forge surface classification from the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon and a new Visual Studio WPF MVVM Revit add-in template by @imaliasad http://bit.ly/roombdrycsv

Happy New Year!
I spent some time during the winter break working on CSV export of room boundaries for a Forge BIM surface classification tool.
Ali Asad presented a new Visual Studio WPF MVVM Revit add-in template
&ndash; Export room boundaries to CSV for Forge surface classification
&ndash; Visual Studio WPF MVVM Revit add-in template...

-->

### Room Boundaries to CSV and WPF Template

Happy New Year to the Revit API developer community!

I spent some time during the winter break working on CSV export of room boundaries for a Forge BIM surface classification tool.

Ali Asad presented a new Visual Studio WPF MVVM Revit add-in template:

- [Export room boundaries to CSV for Forge surface classification](#2) 
- [Visual Studio WPF MVVM Revit add-in template](#3) 


#### <a name="2"></a> Export Room Boundaries to CSV for Forge Surface Classification

A Forge BIM surface classification tool requires room boundaries to display them in the Forge viewer.

<center>
<img src="img/forge_bim_surface_classification.png" alt="Forge BIM surface classification" width="600">
</center>

One simple way to obtain them via the Revit API is demonstrated
by [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) in
the [external command `CmdListAllRooms`](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdListAllRooms.cs).

It was originally presented in 2011, and enhanced in some further discussions:

- [Accessing room data](http://thebuildingcoder.typepad.com/blog/2011/11/accessing-room-data.html).
- [How to distinguish redundant rooms](http://thebuildingcoder.typepad.com/blog/2016/04/how-to-distinguish-redundant-rooms.html)
- [Bounding box `ExpandToContain` and lower left corner of room](http://thebuildingcoder.typepad.com/blog/2016/08/vacation-end-forge-news-and-bounding-boxes.html#6)
- [2D convex hull algorithm in C# using `XYZ`](http://thebuildingcoder.typepad.com/blog/2016/08/online-revit-api-docs-and-convex-hull.html#3)

I modified its output to generate the required data and export that to CSV in a number of release updates:

- [2019.0.144.14](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.14) &ndash; export room boundaries in millimetres
- [2019.0.144.13](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.13) &ndash; implemented IntPoint3d
- [2019.0.144.12](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.12) &ndash; implemented IntPoint2d
- [2019.0.144.11](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.11) &ndash; implemented onlySpaceSeparator argument for PointString and PointArrayString
- [2019.0.144.10](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.10) &ndash; remove Z component from room boundary and convex hull
- [2019.0.144.9](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.9) &ndash; implemented CSV export for CmdListAllRooms
- [2019.0.144.8](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.8) &ndash; implemented export of complete list of points of first room boundary loop
- [2019.0.144.7](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.7) &ndash; handle empty boundary curve in GetConvexHullOfRoomBoundary

Next, I might reimplement the external command as a DB-only add-in to be run in the DA4R
or [Forge Design Automation for Revit](https://thebuildingcoder.typepad.com/blog/2018/11/forge-design-automation-for-revit-at-au-and-in-public.html) environment.


#### <a name="3"></a> Visual Studio WPF MVVM Revit Add-In Template

[Ali @imaliasad Asad](https://twitter.com/imaliasad)
[presented](https://twitter.com/imaliasad/status/1078989674172035072)
a [Visual Studio WPF Revit add-in template](https://github.com/imAliAsad/VisualStudioRevitTemplate).

It empowers you to use the Visual Studio WPF template for Revit add-in development and includes:

- Well organized MVVM architecture for Revit add-in development
- WPF user control to design beautiful Revit add-in
- Auto create ribbon tab and panel
- `Util.cs` for writing helper methods

<center>
<img src="img/Revit2017WPF.png" alt="Visual Studio WPF MVVM Revit add-in template" width="600">
</center>

Many thanks to Ali for sharing this useful tool!


<!----


Lots of other topics were discussed as well, including site and project base point transformations and ... :


#### <a name="3"></a> Site Location and Project Base Point Transforms

A StackOverflow question on 

<pre class="code">
</pre>

**Question:** 

<pre class="prettyprint">
</pre>



#### <a name="4"></a> 

**Answer:** 

**Response:** 

#### <a name="5"></a> 


---->
