<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- get text font outline geometry from TextNode in custom export
  Converting text to geometry when performing a 2d view export
  https://forums.autodesk.com/t5/revit-api-forum/converting-text-to-geometry-when-performing-a-2d-view-export/m-p/10201712#M54774
  /Users/jta/a/doc/revit/tbc/git/a/img/text_geometry.png

- retrieve 2D geometry of generic element
  View Reference Location
  https://forums.autodesk.com/t5/revit-api-forum/view-reference-location/m-p/10867150

- The height and width of the dimension text
  https://forums.autodesk.com/t5/revit-api-forum/the-height-and-width-of-the-dimension-text/m-p/10873262

- detailed wall layer geometry
  Retrieving Detailed Wall Layer Geometry
  https://forums.autodesk.com/t5/revit-api-forum/retrieving-detailed-wall-layer-geometry/m-p/10865604

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash;
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Detailed Wall Layer and 2D Text Geometry

Determining the extents of a text element has been a recurring and challenging task with several tricky solutions suggested in the past.

The advent of the 2D custom exporter brings new possibilities to address this.


<!-- 0610 0646 1223 1440 1517 -->

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/07/text-size.html">Text Size</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/09/textnote-lost-in-space.html">TextNote Lost in Space?</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/10/new-text-note-and-text-width-calculation.html">New Text Note and Text Width Calculation</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/05/idea-station-and-textnote-bounding-box.html">TextNote Bounding Box</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/01/textnote-rotation-forge-devcon-tensorflow-and-keras.html">TextNote Rotation</a></li>
</ul>

####<a name="2"></a> Retrieve Dimension Text Height and Width

The latest question in this series asks how to determine
[the height and width of the dimension text ](https://forums.autodesk.com/t5/revit-api-forum/the-height-and-width-of-the-dimension-text/m-p/10873262):

**Question: When dimension text overlaps, I want to move one of the dimensions to avoid the overlap.
My idea is to calculate the rectangular border of the text through the position of the text and the width and height of the text, and then judge whether the rectangular borders intersect.
So, how to calculate the width and height of dimension text?

**Answer:** That should be possible using the approaches described in these two other recent threads:

Look at these two recent threads here in the forum:

- Get the text font outline geometry from the `TextNode` in a 2D custom export as described
for [converting text to geometry when performing a 2D view export](https://forums.autodesk.com/t5/revit-api-forum/converting-text-to-geometry-when-performing-a-2d-view-export/m-p/10201712)
- Retrieve 2D geometry of generic element, explained in the question
on [view reference location](https://forums.autodesk.com/t5/revit-api-forum/view-reference-location/m-p/10867150)

####<a name="3"></a> Determine Text Font Geometry

Haroon Haider describes a successful approach to access text font geometry in their thread
on [converting text to geometry when performing a 2D view export](https://forums.autodesk.com/t5/revit-api-forum/converting-text-to-geometry-when-performing-a-2d-view-export/m-p/10201712):

**Question:** We have been able to get access to the lines, grids, annotations and other basic elements of a 2D view using the `IExportContext2D` interface.
Is there a way to convert the annotation text from strings to geometry using Revit API similar to how you can do in AutoCAD? 
I'm looking to get something like this:

<center>
<img src="img/text_font_geom.png" alt="Text font geometry" title="Text font geometry" width="223"/> <!-- 223 -->
</center>

**Answer:**: I ended up getting what I need by processing a `TextNode` from the export context method `OnText` call.
There is enough information in the text node class to be able to convert it to a `GraphicsPath` and pull out the geometry from there.

Many thanks to Haroon for sharing this!











####<a name="3"></a> 

