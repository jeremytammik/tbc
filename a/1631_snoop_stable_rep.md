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

RevitLookup snoops stable representation of references in #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/snoopstablerep
Export geometry to AutoCAD via XML using #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/snoopstablerep

Александр Пекшев aka Modis @Pekshev submitted
a very succinct and useful pull request for RevitLookup that
I integrated right away, and provides many other valuable inputs as well
&ndash; Snoop stable representation of References
&ndash; Project point on plane correction
&ndash; Revit export geometry to AutoCAD via XML
&ndash; RevitExportGeometryToAutocad
&ndash; Description
&ndash; Versions
&ndash; Using
&ndash; Example...

--->

### Export Geometry and Snoop Stable Representation

Александр Пекшев aka Modis [@Pekshev](https://github.com/Pekshev) submitted
a very succinct and useful pull request for [RevitLookup](https://github.com/jeremytammik/RevitLookup) that
I integrated right away, and provides many other valuable inputs as well:

- [Snoop stable representation of References](#2)
- [Project point on plane correction](#3)
- [Revit export geometry to AutoCAD via XML](#4)
    - [RevitExportGeometryToAutocad](#4.1)
    - [Description](#4.2)
    - [Versions](#4.3)
    - [Using](#4.4)
    - [Example](#4.5)


####<a name="2"></a>Snoop Stable Representation of References

Alexander's raised
the [issue #40](https://github.com/jeremytammik/RevitLookup/issues/40) and subsequently
submitted [pull request #41](https://github.com/jeremytammik/RevitLookup/pull/41) to
display the result of the `ConvertToStableRepresentation` method when snooping `Reference` objects.

I integrated his improvements
in [RevitLookup](https://github.com/jeremytammik/RevitLookup)
[release 2018.0.0.7](https://github.com/jeremytammik/RevitLookup/releases/tag/2018.0.0.7).

Here is the result of snooping a reference from a dimension between two walls:

<center>
<img src="img/dimension_reference_stable_rep.png" alt="Dimension reference stable representation" width="600"/>
</center>

Many thanks to Alexander for his efficient enhancement!

Take a look at
the [diff from the previous version](https://github.com/jeremytammik/RevitLookup/compare/2018.0.0.6...2018.0.0.7) to
see how elegantly this was achieved.


####<a name="3"></a>Project Point on Plane Correction

Alexander also raised some other interesting issues in in the past in comments on
the [wall graph](http://thebuildingcoder.typepad.com/blog/2008/12/wall-graph.html#comment-3490286732),
[point in polygon algorithm](http://thebuildingcoder.typepad.com/blog/2010/12/point-in-polygon-containment-algorithm.html#comment-3504414240),
[wall elevation profile](http://thebuildingcoder.typepad.com/blog/2015/01/getting-the-wall-elevation-profile.html#comment-3759178237) and,
most recently and significantly,
on [projecting](http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-3765799540)
[a point](http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-3779858513)
[onto a plane](http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-3779960537),
uncovering an error in The Building Coder samples `ProjectOnto` method that projects a given 3D `XYZ` point onto a plane.

I originally presented this method in the discussion
on [planes, projections and picking points](http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html):
[projecting a 3D point onto a plane](http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#12).

Swapping the sign seems to have fixed it, as proved
by [Alexander's ProjectPointOnPlanetest sample add-in](https://github.com/Pekshev/ProjectPointOnPlanetest):

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Project&nbsp;given&nbsp;3D&nbsp;XYZ&nbsp;point&nbsp;onto&nbsp;plane.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;ProjectOnto(
&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;plane,
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;d&nbsp;=&nbsp;plane.SignedDistanceTo(&nbsp;p&nbsp;);

&nbsp;&nbsp;<span style="color:green;">//XYZ&nbsp;q&nbsp;=&nbsp;p&nbsp;+&nbsp;d&nbsp;*&nbsp;plane.Normal;&nbsp;//&nbsp;wrong&nbsp;according&nbsp;to&nbsp;Ruslan&nbsp;Hanza&nbsp;and&nbsp;Alexander&nbsp;Pekshev&nbsp;in&nbsp;their&nbsp;comments&nbsp;http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-3765750464</span>
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;q&nbsp;=&nbsp;p&nbsp;-&nbsp;d&nbsp;*&nbsp;plane.Normal;

&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.IsZero(&nbsp;plane.SignedDistanceTo(&nbsp;q&nbsp;)&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;point&nbsp;on&nbsp;plane&nbsp;to&nbsp;have&nbsp;zero&nbsp;distance&nbsp;to&nbsp;plane&quot;</span>&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;q;
}
</pre>


####<a name="4"></a>Revit Export Geometry to AutoCAD via XML

Browsing Alexander's other GitHub repositories, one that particularly caught my eye is his
[RevitExportGeometryToAutocad add-in](https://github.com/Pekshev/RevitExportGeometryToAutocad),
documented in Russian.

I read the Google-translated English description and think this sounds as if it might be very useful to others as well:

####<a name="4.1"></a>RevitExportGeometryToAutocad

Auxiliary libraries for rendering geometry from Revit to AutoCAD in the form of simple objects (a segment, an arc, a point) by exporting to XML.

####<a name="4.2"></a>Description

Libraries are useful in the development of plug-ins related to geometry, for convenient visual perception of the results.
In my opinion, viewing the result in AutoCAD is much more convenient.

This project provides two libraries (one for Revit, the second for AutoCAD) and a demo project for Revit.

####<a name="4.3"></a>Versions

The project for AutoCAD is built using libraries from AutoCAD 2013. It will work with all subsequent versions of AutoCAD.

The Revit project is built using Revit 2015 libraries. It should work with subsequent versions as well (tested for 2015-2018).

####<a name="4.4"></a>Using

The solution also contains a demo project for Revit.
Description of use for the example of this project:

**In Revit**

Connect to the project a link to the `RevitGeometryExporter.dll` library.

Before using export methods, you need to specify the folder to export xml

<pre class="code">
&nbsp;&nbsp;<span style="color:green;">//&nbsp;setup&nbsp;export&nbsp;folder</span>
&nbsp;&nbsp;ExportGeometryToXml.FolderName&nbsp;=&nbsp;@&nbsp;<span style="color:#a31515;">&quot;C:\Temp&quot;</span>;
</pre>

By default, the library has the path *C:\Temp\RevitExportXml*.
In the absence of a directory, it will be created.

Call one or more methods for exporting geometry, for example:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Wall</span>&gt;&nbsp;wallsToExport&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Wall</span>&gt;();
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;reference&nbsp;<span style="color:blue;">in</span>&nbsp;selectionResult&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;wall&nbsp;=&nbsp;(<span style="color:#2b91af;">Wall</span>)&nbsp;doc.GetElement(&nbsp;reference&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;wallsToExport.Add(&nbsp;wall&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;ExportGeometryToXml.ExportWallsByFaces(&nbsp;wallsToExport,&nbsp;<span style="color:#a31515;">&quot;walls&quot;</span>&nbsp;);
</pre>

Or

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;&nbsp;familyInstances&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;();
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;reference&nbsp;<span style="color:blue;">in</span>&nbsp;selectionResult&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;el&nbsp;=&nbsp;doc.GetElement(&nbsp;reference&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;el&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;familyInstance)
&nbsp;&nbsp;&nbsp;&nbsp;familyInstances.Add(&nbsp;familyInstance&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;ExportGeometryToXml.ExportFamilyInstancesByFaces(&nbsp;familyInstances,&nbsp;<span style="color:#a31515;">&quot;families&quot;</span>,&nbsp;<span style="color:blue;">false</span>&nbsp;);
</pre>

**In AutoCAD**

Use the NETLOAD command to load the `CadDrawGeometry.dll` library.

Use one of the two available commands:

- DrawFromOneXml &ndash; Draw geometry from one specified XML file
- DrawXmlFromFolder &ndash; Draw the geometry from the specified folder in which the XML files reside

####<a name="4.5"></a>Example

Elements in Revit:

<center>
<img src="img/ap_rvt_to_xml_to_acad_1.png" alt="Elements in Revit" width="400"/>
</center>

The result of exporting and rendering geometry in AutoCAD:

<center>
<img src="img/ap_rvt_to_xml_to_acad_2.png" alt="Export result rendered in AutoCAD" width="400"/>
</center>

Many thanks again to Alexander for sharing this!
