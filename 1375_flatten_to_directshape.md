<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #adsklabs #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#rtceur


Revit API, Jeremy Tammik, akn_include

Flatten Everything to DirectShape #revitapi #bim #aec #3dwebcoder #adsk #adskdevnetwrk #geometry #au2015 #3d #apis

I am still busy preparing my presentations for Autodesk University and making slow progress due to handling too many Revit API issues in between. And blogging, as well. Here is a new cool sample contributed by Nikolay Shulga, Senior Principal Engineer in the Revit development team. In his own words
&ndash; Name: Flatten
&ndash; Motivation: I wanted to see whether DirectShapes could be used to lock down a Revit design &ndash; remove most intelligence, make it read-only, perhaps improve performance
&ndash; Spec: converts full Revit elements into DirectShapes that hold the same shape and have the same categories
&ndash; Implementation: see below
&ndash; Cool API aspects: copy element’s geometry and use it elsewhere
&ndash; Cool ways to use it: lock down your project; make a copy of your element for presentation/export
&ndash; How it can be enhanced: the sky is the limit...

-->

### Flatten All Elements to DirectShape

I am still busy preparing my presentations for Autodesk University and making slow progress due to handling too many Revit API issues in between.

And blogging, as well.

Here is a new cool sample contributed by Nikolay Shulga, Senior Principal Engineer in the Revit development team.

In his own words:

- Name: Flatten
- Motivation: I wanted to see whether DirectShapes could be used to lock down a Revit design &ndash; remove most intelligence, make it read-only, perhaps improve performance.
- Spec: converts full Revit elements into DirectShapes that hold the same shape and have the same categories.
- Implementation: see below.
- Cool API aspects: copy element’s geometry and use it elsewhere.
- Cool ways to use it: lock down your project; make a copy of your element for presentation/export.
- How it can be enhanced: the sky is the limit.
- A suitable sample model: any Revit project. Note that the code changes the current project &ndash; make a backup copy.

This fits in well with the growing interest in direct shapes, as you can observe by the rapidly growing number of entries in
the [DirectShape topic group](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.50).

I implemented a new external
command [CmdFlatten](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdFlatten.cs)
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) to
test and demonstrate this functionality.

Nikolay's original code was for a future release of Revit, so some backwards adaptation was necessary.

You can see the changes by looking at the last few GitHub commits.

Here is the final result for running in Revit 2016:

<pre class="code">
&nbsp; <span class="blue">const</span> <span class="blue">string</span> _direct_shape_appGUID = <span class="maroon">&quot;Flatten&quot;</span>;
&nbsp;
&nbsp; <span class="teal">Result</span> Flatten(
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="teal">ElementId</span> viewId )
&nbsp; {
&nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> col
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc, viewId )
&nbsp; &nbsp; &nbsp; &nbsp; .WhereElementIsNotElementType();
&nbsp;
&nbsp; &nbsp; <span class="teal">Options</span> geometryOptions = <span class="blue">new</span> <span class="teal">Options</span>();
&nbsp;
&nbsp; &nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> tx = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( tx.Start( <span class="maroon">&quot;Convert elements to DirectShapes&quot;</span> )
&nbsp; &nbsp; &nbsp; &nbsp; == <span class="teal">TransactionStatus</span>.Started )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Element</span> e <span class="blue">in</span> col )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> gelt = e.get_Geometry(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; geometryOptions );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != gelt )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">string</span> appDataGUID = e.Id.ToString();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Currently create direct shape </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// replacement element in the original </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// document &#8211; no API to properly transfer </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// graphic styles to a new document.</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// A possible enhancement: make a copy </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// of the current project and operate </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// on the copy.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">DirectShape</span> ds
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">DirectShape</span>.CreateElement( doc,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; e.Category.Id, _direct_shape_appGUID,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; appDataGUID );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ds.SetShape(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">GeometryObject</span>&gt;( gelt ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Delete original element</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc.Delete( e.Id );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">catch</span>( Autodesk.Revit.Exceptions
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .<span class="teal">ArgumentException</span> ex )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Failed to replace {0}; exception {1} {2}&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Util</span>.ElementDescription( e ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ex.GetType().FullName,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ex.Message );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; tx.Commit();
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; <span class="green">// At the moment we convert to DirectShapes </span>
&nbsp; &nbsp; <span class="green">// &quot;in place&quot; - that lets us preserve GStyles </span>
&nbsp; &nbsp; <span class="green">// referenced by element shape without doing </span>
&nbsp; &nbsp; <span class="green">// anything special.</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> Flatten( doc, uidoc.ActiveView.Id );
&nbsp; }
</pre>

Here is a trivial example of flattening a wall to a direct shape:

<center>
<img src="img/flatten_01.png" alt="Original wall element" width="242">
</center>

This is the automatically generated DirectShape replacement element, retaining the wall category and storing its original element id:

<center>
<img src="img/flatten_02.png" alt="DirectShape replacement element" width="193">
</center>

Let's try it out on a slightly more complex model:

<center>
<img src="img/flatten_03.png" alt="Walls with doors and windows" width="465">
</center>

The family instances get lost by the conversion in its current implementation:

<center>
<img src="img/flatten_04.png" alt="DirectShape replacement element" width="322">
</center>

If you wish to retain family instances, you should probably explore their geometry in a little bit more detail, e.g., to extract all the solids they contain and convert them individually.

The current version is provided in the
module [CmdFlatten.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdFlatten.cs)
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2016.0.123.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2016.0.123.0).

Have fun playing with this, and many thanks to Nikolay for implementing and sharing it!
