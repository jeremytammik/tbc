<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- RevitLookup
  https://github.com/jeremytammik/RevitLookup/releases/tag/2016.0.0.11
  implemented support for Element bounding box

- 11106220 [When creating a family document is it possible to set the document name in memory?]

#adsk #adskdevnetwrk
#dotnet #csharp #geometry
#fsharp #dynamobim #python
#grevit
#responsivedesign #typepad
#ah8 #augi #au2015 #dotnet #dynamobim
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon #revitapi #adsk #3dwebcoder
#javascript
#au2015 #rtceur
#RestSharp #restapi

Revit API, Jeremy Tammik, akn_include

SVG, In-Memory Family, RevitLookup BoundingBox #revitapi #au2015 #bim #aec #3dwebcoder #mongoosejs #mongodb #nodejs

The cloud accelerator kept me busy last week, I am working hard on the CompHound project and documenting that work on The 3D Web Coder.
Besides that, I submitted several enhancements to various Revit add-in projects.
Here are some of my recent activities
&ndash; CompHound component tracker project development
&ndash; RevitLookup displays element bounding box
&ndash; SvgExport for Revit 2016
&ndash; In-memory family creation and structural stiffener migration from 2012 to 2016...

-->

### SVG, In-Memory Family, RevitLookup BoundingBox

I have been blogging less here and doing more work elsewhere in the past couple of days.

The [cloud accelerator](http://autodeskcloudaccelerator.com/prague/) kept
me busy last week, I am working hard on
the [CompHound project](https://github.com/CompHound/CompHound.github.io) and documenting that work
on [The 3D Web Coder](http://the3dwebcoder.typepad.com).

Besides that, I submitted several enhancements to various Revit add-in projects.

Here are some of my recent activities:

- [CompHound component tracker project development](#2)
- [RevitLookup displays element bounding box](#3)
- [SvgExport for Revit 2016](#4)
- [In-memory family creation and structural stiffener migration](#5) from 2012 to 2016


#### <a name="2"></a>CompHound Component Tracker Project Development

If you are interested in connecting Revit or any other desktop application with the cloud, you may be interested in
my [CompHound](https://github.com/CompHound/CompHound.github.io)
and [FireRating in the Cloud](https://github.com/jeremytammik/firerating) projects.

CompHound is a cloud-based universal component and asset usage analysis, report, bill of materials and visualisation project.

I am working on it to provide sample code for two presentations on connecting the desktop with the cloud at
[RTC Europe](http://www.rtcevents.com/rtc2015eu) in Budapest end of October and
[Autodesk University](http://au.autodesk.com) in Las Vegas in December.

It currently consists of two parts:

- [CompHoundWeb](https://github.com/CompHound/CompHoundWeb),
a Node.js web server, mongo database and
[Autodesk View and Data API](https://developer.autodesk.com) viewer.
- [CompHoundRvt](https://github.com/CompHound/CompHoundRvt),
a Revit add-in to populate the CompHound web database with ±BIM family instances.

CompHound is based on and derived from the **FireRating in the Cloud** sample, consisting of the
[FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) C# .NET REST API client Revit add-in and the
[fireratingdb](https://github.com/jeremytammik/firerating) Node.js mongoDB web server.

I hope you are aware of the standard Revit SDK FireRating sample... FireRatingCloud is a rewrite of it to support multiple projects and collect the relevant data from all of them in one single scalable cloud database. It is really minimal and efficient, well worth checking out.

The FireRating in the Cloud sample has no user interface, whereas CompHound does. It is still under development, and will soon include reporting, bill of materials, [View and Data API](https://developer.autodesk.com) viewing and navigation functionality.

For more information, please refer to the project home pages on GitHub
and [The 3D Web Coder](http://the3dwebcoder.typepad.com) detailed articles describing its implementation and evolution so far:

- [Project definition and christening](http://the3dwebcoder.typepad.com/blog/2015/09/comphound-jsfiddle-and-my-first-react-component.html).
- [First implementation](http://the3dwebcoder.typepad.com/blog/2015/09/comphound-restsharp-mongoose-put-and-post.html#2) based on
[fireratingdb](https://github.com/jeremytammik/firerating) and
[FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud).
- [Steps towards a database table view](http://the3dwebcoder.typepad.com/blog/2015/09/towards-a-comphound-mongo-database-table-view.html).
- [The CompHound Mongoose DataTable](http://the3dwebcoder.typepad.com/blog/2015/09/the-comphound-mongoose-datatable.html)
- [CompHound Heroku Deployment](http://the3dwebcoder.typepad.com/blog/2015/09/comphound-heroku-deployment-and-urban-farming.html)

Back from the cloud to the pure desktop Revit API...



#### <a name="3"></a>RevitLookup Displays Element Bounding Box

Participants at the cloud accelerator were interested in the bounding box of various elements, to enable zooming to rooms and floors in the
[View and Data API](https://developer.autodesk.com/) viewer.

I was surprised that RevitLookup did not list that information.

Not that surprising, actually: in general, it just lists property values, and mostly does not evaluate and display results from explicit method calls.

I added just four lines of code:

<pre class="code">
&nbsp; <span class="teal">BoundingBoxXYZ</span> bb = elem.get_BoundingBox( <span class="blue">null</span> );
&nbsp; <span class="blue">if</span>( <span class="blue">null</span> != bb )
&nbsp; {
&nbsp; &nbsp; data.Add( <span class="blue">new</span> Snoop.Data.<span class="teal">Object</span>( <span class="maroon">&quot;Bounding box&quot;</span>, bb ) );
&nbsp; }
</pre>

The update is provided in
[RevitLookup release 2016.0.0.11](https://github.com/jeremytammik/RevitLookup/releases/tag/2016.0.0.11).
Please always download and install the latest version to ensure you are up to date.



#### <a name="4"></a>SvgExport for Revit 2016

I also migrated SvgExport, my Revit add-in to export element geometry to SVG, discussed in the posts on
[displaying 2D graphics via a node server](http://the3dwebcoder.typepad.com/blog/2015/04/displaying-2d-graphics-via-a-node-server.html) and
[sending a room boundary to an SVG node web server](http://thebuildingcoder.typepad.com/blog/2015/04/sending-a-room-boundary-to-an-svg-node-web-server.html).

The current version including some small clean-up is now
[SvgExport release 2016.0.0.1](https://github.com/jeremytammik/SvgExport/releases/tag/2016.0.0.1).


#### <a name="5"></a>In-memory Family Creation and Structural Stiffener Migration

Finally, to complete the series of updates and migrations, I looked at implementing in-memory family creation, loading, and instance placement.

This issue was prompted by a developer query:

**Question:**
When creating a family document the title is an automatically assigned name.
Is it possible to change the title and family name without saving the document?
I don't want to save the file.
I want to load it directly into another model that is in memory.

If I have to access the hard disk change the name each time. I have to potentially create thousands of very small families.

Theoretically the hard disk is about 80000 times slower then memory.
So I want to avoid setting the family name by saving the document.

**Answer:**
As you know and are already doing, you can create a family definition in memory and load it into the project without ever saving to disk.

I tried to implement that back in 2011, for Revit 2012, as explained in the discussion
on [creating and inserting an extrusion family](http://thebuildingcoder.typepad.com/blog/2011/06/creating-and-inserting-an-extrusion-family.html) for
a structural stiffener.

At that time, the pure in-memory loading did not work as expected.

That has been fixed in the meantime, though, probably in Revit 2013 or so.

In order to answer your question on setting the family and symbol name, I created a
new [Stiffener GitHub repository](https://github.com/jeremytammik/Stiffener) for
it and migrated it to Revit 2014.

The first Revit 2014 version is just a flat migration that reproduces the Revit 2012 behaviour, saving the family definition to disk before loading it:

<center>
<img src="img/stiffener_2014.png" alt="Revit 2014 stiffener family saved to disk" width="446"/>
</center>

I then implemented some significant changes to enable loading the family directly from memory.

That works, and presumably displays the behaviour that you are currently observing:

<center>
<img src="img/stiffener_2014_in_memory.png" alt="Revit 2014 stiffener family loaded from memory" width="433"/>
</center>

Next, I set the family name. That is possibly a way to achieve part of what you want:

<center>
<img src="img/stiffener_2014_in_memory_with_name.png" alt="Revit 2014 stiffener family loaded from memory with family name" width="458"/>
</center>

Finally, I migrated to Revit 2015 and 2016 as well, and added a line of code to set the symbol name as well as the family name:

<center>
<img src="img/stiffener_2016_in_memory_with_both_names.png" alt="Revit 2014 stiffener family loaded from memory with family and symbol name" width="346"/>
</center>

Does that fulfil your needs?

Here is the complete list of releases and modifications:

- [2012.0.0.0](https://github.com/jeremytammik/Stiffener/releases/tag/2012.0.0.0) initial commit for Revit 2012, copied from the June 13 2011 blog post
- [2014.0.0.0](https://github.com/jeremytammik/Stiffener/releases/tag/2014.0.0.0) migration to Revit 2014 including code update, .NET framework target 4.0, removal of architecture mismatch warning and obsolete API usage
- [2014.0.0.1](https://github.com/jeremytammik/Stiffener/releases/tag/2014.0.0.1) updated to place instance from in-memory family definition with no file save, restructured logic, added using statements around transactions
- [2014.0.0.2](https://github.com/jeremytammik/Stiffener/releases/tag/2014.0.0.2) set family name after loading into project
- [2014.0.0.3](https://github.com/jeremytammik/Stiffener/releases/tag/2014.0.0.3) set 'copy local' to false on the Revit API assemblies
- [2015.0.0.0](https://github.com/jeremytammik/Stiffener/releases/tag/2015.0.0.0) flat migration to Revit 2015
- [2016.0.0.0](https://github.com/jeremytammik/Stiffener/releases/tag/2016.0.0.0) migration to Revit 2016, need to activate symbol, set symbol name as well as family
- [2016.0.0.1](https://github.com/jeremytammik/Stiffener/releases/tag/2016.0.0.1) fixed deprecated API usage

Please use the GitHub diff tool to compare the differences between each of these.

For instance, to see how I set the family name, you can compare version 2014.0.0.1 and 2014.0.0.2 using the
URL [github.com/jeremytammik/Stiffener/compare/2014.0.0.1...2014.0.0.2](https://github.com/jeremytammik/Stiffener/compare/2014.0.0.1...2014.0.0.2).

It was fun to migrate all the way from release 2012 all the way to 2016 in one single sitting.

The final implementation now looks like this and includes a couple of comments that illustrate further implementation details:

<pre class="code">
[<span class="teal">Transaction</span>( <span class="teal">TransactionMode</span>.Manual )]
<span class="blue">public</span> <span class="blue">class</span> <span class="teal">Command</span> : <span class="teal">IExternalCommand</span>
{
<span class="blue">&nbsp; #region</span> Constants
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Family template filename extension</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">const</span> <span class="blue">string</span> _family_template_ext = <span class="maroon">&quot;.rft&quot;</span>;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Revit family filename extension</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">const</span> <span class="blue">string</span> _rfa_ext = <span class="maroon">&quot;.rfa&quot;</span>;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Family template library path</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="green">//const string _path = &quot;C:/ProgramData/Autodesk/RST 2012/Family Templates/English&quot;;</span>
&nbsp; <span class="blue">const</span> <span class="blue">string</span> _path = <span class="maroon">&quot;C:/Users/All Users/Autodesk/RVT 2014/Family Templates/English&quot;</span>;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Family template filename stem</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">const</span> <span class="blue">string</span> _family_template_name = <span class="maroon">&quot;Metric Structural Stiffener&quot;</span>;
&nbsp;
&nbsp; <span class="green">// Family template path and filename for imperial units</span>
&nbsp;
&nbsp; <span class="green">//const string _path = &quot;C:/ProgramData/Autodesk/RST 2012/Family Templates/English_I&quot;;</span>
&nbsp; <span class="green">//const string _family_name = &quot;Structural Stiffener&quot;;</span>
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Name of the generated stiffener family</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">const</span> <span class="blue">string</span> _family_name = <span class="maroon">&quot;Stiffener&quot;</span>;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Conversion factor from millimetre to foot</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">const</span> <span class="blue">double</span> _mm_to_foot = 1 / 304.8;
<span class="blue">&nbsp; #endregion</span> <span class="green">// Constants</span>
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Convert a given length to feet</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">double</span> MmToFoot( <span class="blue">double</span> length_in_mm )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> _mm_to_foot * length_in_mm;
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Convert a given point defined in millimetre units to feet</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="teal">XYZ</span> MmToFootPoint( <span class="teal">XYZ</span> p )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> p.Multiply( _mm_to_foot );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">static</span> <span class="blue">int</span> n = 4;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Extrusion profile points defined in millimetres.</span>
&nbsp; <span class="gray">///</span><span class="green"> Here is just a very trivial rectangular shape.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; _countour = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt;( n )
&nbsp; {
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( 0 , -75 , 0 ),
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( 508, -75 , 0 ),
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( 508, 75 , 0 ),
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XYZ</span>( 0, 75 , 0 )
&nbsp; };
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Extrusion thickness for stiffener plate</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">const</span> <span class="blue">double</span> _thicknessMm = 20.0;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return the first element found of the </span>
&nbsp; <span class="gray">///</span><span class="green"> specific target type with the given name.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="teal">Element</span> FindElement(
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="teal">Type</span> targetType,
&nbsp; &nbsp; <span class="blue">string</span> targetName )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .OfClass( targetType )
&nbsp; &nbsp; &nbsp; .First&lt;<span class="teal">Element</span>&gt;( e =&gt; e.Name.Equals( targetName ) );
&nbsp;
&nbsp; &nbsp; <span class="green">// Obsolete code parsing the collection for the </span>
&nbsp; &nbsp; <span class="green">// given name using a LINQ query. </span>
&nbsp;
&nbsp; &nbsp; <span class="green">//var targetElems </span>
&nbsp; &nbsp; <span class="green">//&nbsp; = from element in collector </span>
&nbsp; &nbsp; <span class="green">//&nbsp; &nbsp; where element.Name.Equals( targetName ) </span>
&nbsp; &nbsp; <span class="green">//&nbsp; &nbsp; select element;</span>
&nbsp;
&nbsp; &nbsp; <span class="green">//return targetElems.First&lt;El</span>
&nbsp; &nbsp; <span class="green">//List&lt;Element&gt; elems = targetElems.ToList&lt;Element&gt;();</span>
&nbsp;
&nbsp; &nbsp; <span class="green">//if( elems.Count &gt; 0 )</span>
&nbsp; &nbsp; <span class="green">//{&nbsp; // we should have only one with the given name. </span>
&nbsp; &nbsp; <span class="green">//&nbsp; return elems[0];</span>
&nbsp; &nbsp; <span class="green">//}</span>
&nbsp;
&nbsp; &nbsp; <span class="green">// cannot find it.</span>
&nbsp; &nbsp; <span class="green">//return null;</span>
&nbsp;
&nbsp; &nbsp; <span class="green">/*</span>
<span class="green">&nbsp; &nbsp; // most efficient way to find a named </span>
<span class="green">&nbsp; &nbsp; // family symbol: use a parameter filter.</span>
&nbsp;
<span class="green">&nbsp; &nbsp; ParameterValueProvider provider</span>
<span class="green">&nbsp; &nbsp; &nbsp; = new ParameterValueProvider(</span>
<span class="green">&nbsp; &nbsp; &nbsp; &nbsp; new ElementId( BuiltInParameter.DATUM_TEXT ) ); // VIEW_NAME for a view</span>
&nbsp;
<span class="green">&nbsp; &nbsp; FilterStringRuleEvaluator evaluator</span>
<span class="green">&nbsp; &nbsp; &nbsp; = new FilterStringEquals();</span>
&nbsp;
<span class="green">&nbsp; &nbsp; FilterRule rule = new FilterStringRule(</span>
<span class="green">&nbsp; &nbsp; &nbsp; provider, evaluator, targetName, true );</span>
&nbsp;
<span class="green">&nbsp; &nbsp; ElementParameterFilter filter</span>
<span class="green">&nbsp; &nbsp; &nbsp; = new ElementParameterFilter( rule );</span>
&nbsp;
<span class="green">&nbsp; &nbsp; return new FilteredElementCollector( doc )</span>
<span class="green">&nbsp; &nbsp; &nbsp; .OfClass( targetType )</span>
<span class="green">&nbsp; &nbsp; &nbsp; .WherePasses( filter )</span>
<span class="green">&nbsp; &nbsp; &nbsp; .FirstElement();</span>
<span class="green">&nbsp; &nbsp; */</span>
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Convert a given list of XYZ points </span>
&nbsp; <span class="gray">///</span><span class="green"> to a CurveArray instance. </span>
&nbsp; <span class="gray">///</span><span class="green"> The points are defined in millimetres, </span>
&nbsp; <span class="gray">///</span><span class="green"> the returned CurveArray in feet.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="teal">CurveArray</span> CreateProfile( <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; pts )
&nbsp; {
&nbsp; &nbsp; <span class="teal">CurveArray</span> profile = <span class="blue">new</span> <span class="teal">CurveArray</span>();
&nbsp;
&nbsp; &nbsp; <span class="blue">int</span> n = _countour.Count;
&nbsp;
&nbsp; &nbsp; <span class="blue">for</span>( <span class="blue">int</span> i = 0; i &lt; n; ++i )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">int</span> j = ( 0 == i ) ? n - 1 : i - 1;
&nbsp;
&nbsp; &nbsp; &nbsp; profile.Append( <span class="teal">Line</span>.CreateBound(
&nbsp; &nbsp; &nbsp; &nbsp; MmToFootPoint( pts[j] ),
&nbsp; &nbsp; &nbsp; &nbsp; MmToFootPoint( pts[i] ) ) );
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> profile;
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Create an extrusion from a given thickness </span>
&nbsp; <span class="gray">///</span><span class="green"> and list of XYZ points defined in millimetres</span>
&nbsp; <span class="gray">///</span><span class="green"> in the given family document, which&nbsp; must </span>
&nbsp; <span class="gray">///</span><span class="green"> contain a sketch plane named &quot;Ref. Level&quot;.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="teal">Extrusion</span> CreateExtrusion(
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; pts,
&nbsp; &nbsp; <span class="blue">double</span> thickness )
&nbsp; {
&nbsp; &nbsp; Autodesk.Revit.Creation.<span class="teal">FamilyItemFactory</span> factory
&nbsp; &nbsp; &nbsp; = doc.FamilyCreate;
&nbsp;
&nbsp; &nbsp; <span class="teal">SketchPlane</span> sketch = FindElement( doc,
&nbsp; &nbsp; &nbsp; <span class="blue">typeof</span>( <span class="teal">SketchPlane</span> ), <span class="maroon">&quot;Ref. Level&quot;</span> )
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">SketchPlane</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">CurveArrArray</span> curveArrArray = <span class="blue">new</span> <span class="teal">CurveArrArray</span>();
&nbsp;
&nbsp; &nbsp; curveArrArray.Append( CreateProfile( pts ) );
&nbsp;
&nbsp; &nbsp; <span class="blue">double</span> extrusionHeight = MmToFoot( thickness );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> factory.NewExtrusion( <span class="blue">true</span>,
&nbsp; &nbsp; &nbsp; curveArrArray, sketch, extrusionHeight );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Application</span> app = uiapp.Application;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp; &nbsp; <span class="teal">Document</span> fdoc = <span class="blue">null</span>;
&nbsp; &nbsp; <span class="teal">Transaction</span> t = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == doc )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; message = <span class="maroon">&quot;Please run this command in an open document.&quot;</span>;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Failed;
&nbsp; &nbsp; }
&nbsp;
<span class="blue">&nbsp; &nbsp; #region</span> Create a new structural stiffener family
&nbsp;
&nbsp; &nbsp; <span class="green">// Check whether the family has already</span>
&nbsp; &nbsp; <span class="green">// been created or loaded previously.</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">Family</span> family
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">Family</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; .Cast&lt;<span class="teal">Family</span>&gt;()
&nbsp; &nbsp; &nbsp; &nbsp; .FirstOrDefault&lt;<span class="teal">Family</span>&gt;( e
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; =&gt; e.Name.Equals( _family_name ) );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != family )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; fdoc = family.Document;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">string</span> templateFileName = <span class="teal">Path</span>.Combine( _path,
&nbsp; &nbsp; &nbsp; &nbsp; _family_template_name + _family_template_ext );
&nbsp;
&nbsp; &nbsp; &nbsp; fdoc = app.NewFamilyDocument(
&nbsp; &nbsp; &nbsp; &nbsp; templateFileName );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == fdoc )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; message = <span class="maroon">&quot;Cannot create family document.&quot;</span>;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Failed;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">using</span>( t = <span class="blue">new</span> <span class="teal">Transaction</span>( fdoc ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; t.Start( <span class="maroon">&quot;Create structural stiffener family&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; CreateExtrusion( fdoc, _countour, _thicknessMm );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; t.Commit();
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">//fdoc.Title = _family_name; // read-only property</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">bool</span> needToSaveBeforeLoad = <span class="blue">false</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( needToSaveBeforeLoad )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Save our new family background document</span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// and reopen it in the Revit user interface.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">string</span> filename = <span class="teal">Path</span>.Combine(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Path</span>.GetTempPath(), _family_name + _rfa_ext );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">SaveAsOptions</span> opt = <span class="blue">new</span> <span class="teal">SaveAsOptions</span>();
&nbsp; &nbsp; &nbsp; &nbsp; opt.OverwriteExistingFile = <span class="blue">true</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; fdoc.SaveAs( filename, opt );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">bool</span> closeAndOpen = <span class="blue">true</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( closeAndOpen )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Cannot close the newly generated family file</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// if it is the only open document; that throws </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// an exception saying &quot;The active document may </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// not be closed from the API.&quot;</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; fdoc.Close( <span class="blue">false</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// This obviously invalidates the uidoc </span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// instance on the previously open document.</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//uiapp.OpenAndActivateDocument( filename );</span>
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
<span class="blue">&nbsp; &nbsp; #endregion</span> <span class="green">// Create a new structural stiffener family</span>
&nbsp;
<span class="blue">&nbsp; &nbsp; #region</span> Load the structural stiffener family
&nbsp;
&nbsp; &nbsp; <span class="green">// Must be outside transaction; otherwise Revit </span>
&nbsp; &nbsp; <span class="green">// throws InvalidOperationException: The document </span>
&nbsp; &nbsp; <span class="green">// must not be modifiable before calling LoadFamily. </span>
&nbsp; &nbsp; <span class="green">// Any open transaction must be closed prior the call.</span>
&nbsp;
&nbsp; &nbsp; <span class="green">// Calling this without a prior call to SaveAs</span>
&nbsp; &nbsp; <span class="green">// caused a &quot;serious error&quot; in Revit 2012:</span>
&nbsp;
&nbsp; &nbsp; family = fdoc.LoadFamily( doc );
&nbsp;
&nbsp; &nbsp; <span class="green">// Workaround for Revit 2012, </span>
&nbsp; &nbsp; <span class="green">// no longer needed in Revit 2014:</span>
&nbsp;
&nbsp; &nbsp; <span class="green">//doc.LoadFamily( filename, out family );</span>
&nbsp;
&nbsp; &nbsp; <span class="green">// Setting the name requires an open </span>
&nbsp; &nbsp; <span class="green">// transaction, of course.</span>
&nbsp;
&nbsp; &nbsp; <span class="green">//family.Name = _family_name;</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">FamilySymbol</span> symbol = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">ElementId</span> id
&nbsp; &nbsp; &nbsp; <span class="blue">in</span> family.GetFamilySymbolIds() )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">// Our family only contains one</span>
&nbsp; &nbsp; &nbsp; <span class="green">// symbol, so pick it and leave.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; symbol = doc.GetElement( id ) <span class="blue">as</span> <span class="teal">FamilySymbol</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; }
<span class="blue">&nbsp; &nbsp; #endregion</span> <span class="green">// Load the structural stiffener family</span>
&nbsp;
<span class="blue">&nbsp; &nbsp; #region</span> Insert stiffener family instance
&nbsp;
&nbsp; &nbsp; <span class="blue">using</span>( t = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; t.Start( <span class="maroon">&quot;Insert structural stiffener family instance&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// Setting the name requires an open </span>
&nbsp; &nbsp; &nbsp; <span class="green">// transaction, of course.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; family.Name = _family_name;
&nbsp; &nbsp; &nbsp; symbol.Name = _family_name;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">// Need to activate symbol before </span>
&nbsp; &nbsp; &nbsp; <span class="green">// using it in Revit 2016.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; symbol.Activate();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">bool</span> useSimpleInsertionPoint = <span class="blue">true</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( useSimpleInsertionPoint )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> p = uidoc.Selection.PickPoint(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Please pick a point for family instance insertion&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">StructuralType</span> st = <span class="teal">StructuralType</span>.UnknownFraming;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; doc.Create.NewFamilyInstance( p, symbol, st );
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">bool</span> useFaceReference = <span class="blue">false</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( useFaceReference )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Reference</span> r = uidoc.Selection.PickObject(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ObjectType</span>.Face,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Please pick a point on a face for family instance insertion&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Element</span> e = doc.GetElement( r.ElementId );
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">GeometryObject</span> obj = e.GetGeometryObjectFromReference( r );
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">PlanarFace</span> face = obj <span class="blue">as</span> <span class="teal">PlanarFace</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == face )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; message = <span class="maroon">&quot;Please select a point on a planar face.&quot;</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; t.RollBack();
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Failed;
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> p = r.GlobalPoint;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//XYZ v = face.Normal.CrossProduct( XYZ.BasisZ ); // 2015</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> v = face.FaceNormal.CrossProduct( <span class="teal">XYZ</span>.BasisZ ); <span class="green">// 2016</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( v.IsZeroLength() )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; v = face.FaceNormal.CrossProduct( <span class="teal">XYZ</span>.BasisX );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc.Create.NewFamilyInstance( r, p, v, symbol );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// This throws an exception saying that the face has no reference on it:</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//doc.Create.NewFamilyInstance( face, p, v, symbol ); </span>
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; t.Commit();
&nbsp; &nbsp; }
<span class="blue">&nbsp; &nbsp; #endregion</span> <span class="green">// Insert stiffener family instance</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
}
</pre>

I hope this helps.

<!----
#### <a name="6"></a>Creating a Roof

email jaime...

<center>
<img src="" alt="" width="400"/>
</center>


#### <a name="2"></a>FireRating in the Cloud Enhancements

My real goal is to switch back and continue work on the new
[CompHound component tracker](http://the3dwebcoder.typepad.com/blog/2015/09/comphound-restsharp-mongoose-put-and-post.html#2) as
fast as possible, in preparation for the upcoming conference presentations at
[RTC Europe](http://www.rtcevents.com/rtc2015eu) in Budapest end of October and
[Autodesk University](http://au.autodesk.com) in Las Vegas in December.

However, since the
[FireRating in the Cloud](https://github.com/jeremytammik/FireRatingCloud) sample
is more generic and fundamental, I want to clean it up to utter perfection first, before expanding into new areas.

[FireRating in the Cloud](https://github.com/jeremytammik/FireRatingCloud) covers the connection of Revit database and element data to a cloud-hosted external database.

The [CompHound component tracker](https://github.com/CompHound) will add a user interface, reports, and a 3D model viewer and navigator to that.

Here is a list of the recent firerating enhancements:

- [Using RestSharp for Rest API GET](http://the3dwebcoder.typepad.com/blog/2015/09/using-restsharp-for-rest-api-get.html)
- [Mongodb Upsert](http://the3dwebcoder.typepad.com/blog/2015/09/mongodb-upsert.html)
- C# DoorData and Node.js DoorService Classes
    - [DoorData Container Class](http://the3dwebcoder.typepad.com/blog/2015/09/c-doordata-and-nodejs-doorservice-classes.html#3)
    - [REST GET returns a list of deserialised DoorData instances](http://the3dwebcoder.typepad.com/blog/2015/09/c-doordata-and-nodejs-doorservice-classes.html#4)
    - [Passing a DoorData instance to the Put method](http://the3dwebcoder.typepad.com/blog/2015/09/c-doordata-and-nodejs-doorservice-classes.html#5)
    - [Implementing a REST API router DoorService class](http://the3dwebcoder.typepad.com/blog/2015/09/c-doordata-and-nodejs-doorservice-classes.html#6)


Using [Handlebars](http://handlebarsjs.com)...

http://localhost:3001/html/instances

http://localhost:3001/api/v1/instances

http://localhost:3001/api/v1/instances/48891eaa-9041-405b-a10f-f06585de3cbb-0001de6d


Antonio Manzani
La Costola di Adamo
detective story
full of love for humanity
anger also
p. 268-269
2015-09-18: Antonio Manzini, La costola di Adamo [it] [i love rocco schiavione and antonio manzani better still; radical militant violent personal integrity, regardless of law and order, serving a higher and personal truth; the topic is political and sociological, femmicide; read the last sentence of the acknowledgements before starting the book; i wish there were more rocco stories]
Domenica sono andata in chiesa. Non volevo sentire la messa. Voleva guardare la chiesa. Ho sbagliato orario. Sono entrata e c'era proprio la messa. Il prete ha letto la Genesi, 2:21.23. Me la sono riletta a casa. Dice: Allora l'eterno Iddio fece cadere un profondo sonno sull'uomo, che s'addormento. E prese una delle costole di lui; e richiuse la carne al posto d'essa. E l'eterno Iddio , con la costola che aveva tolta all'uomo, formo una donna e la condusse all'uomo. E l'uomo disse: "Questa finalmente e ossa delle mie ossa e carne della mia carne. Ella sara chiamata donna perche e stata tratta dall'uomo."
Mi sono messa a pensare. A sentire la storiella, la donna nasce dall'uomo, anzi ne e proprio un pezzo. E l'uomo impazzisce per la donna, la ama. In realta ama se stesso. Ama un pezzo di se, non un altro da se. Vive e fa i figli e fa l'amore con se stesso. Un amore concentrato sulla propria persona che niente ha che fare con l'amore. Credo che sia quanto di piu perverso abbia mai letto. Il maschio e innamorato solo di se. Questo dicono le Sacre Scritture. L'inferiorita femmenile non c'entra. E solo


#### <a name="7"></a>Creating a Roof

http://stackoverflow.com/questions/32718999/creating-a-roof-function/32732012#32732012

**Question:**
I'm having trouble to create a roof via code. I know how to create a stairs for example : I start a StairsEditScope and use CreateSketchedLanding with all the right parameters to create my stairs and just commit the StairsEditScope, but for a roof i cant find a clue on how to create it from scratch, any leads?

**Answer:**
Revit provides different kinds of roofs. It is best to understand the various types from an end user point of view before starting to drive them programmatically. The simplest one is defined by a horizontal outline. You can create a roof from such an outline using the Document.NewFootPrintRoof method. Such a roof can be flat, or you can specify a slope for each edge of the outline profile. The Building Coder Xtra labs provide a working sample in the external command Lab2_0_CreateLittleHouse in Labs2.cs:

https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs2.cs

Here are some other roof-related posts on The Building Coder:

RoomsRoofs SDK Sample
Roof Eave Cut
Creating an Extrusion Roof

I hope this helps.

Cheers, Jeremy.

<!- 0015 0198 1215 ->
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/09/roomsroofs-sdk.html">RoomsRoofs SDK Sample</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/08/roof-eave-cut-in-revit-and-aca.html">Roof Eave Cut in AutoCAD Architecture and Revit</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/09/events-again-and-creating-an-extrusion-roof.html">Events, Again, and Creating an Extrusion Roof</a></li>
</ul>

-->
