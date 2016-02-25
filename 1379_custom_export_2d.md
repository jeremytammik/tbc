<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- no custom exporter for 2D
  11272210 [Development/API - 2D Plan views using CustomExporter]

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
#geometry #3d
#xaml

Revit API, Jeremy Tammik, akn_include

#DevDays @ #AU2015, Using a Custom Exporter for 2D #revitapi #bim #aec #3dwebcoder #adsk

Several developers recently asked how to export 2D shapes through a custom exporter. I recently discussed this issue with Arnošt Löbel and below present a summary and combination of several different conversation threads on the topic. First, let me mention that I arrived safe and sound at Autodesk University. I travelled on my birthday last week, so I was upgraded to fly in style
&ndash; in a sledge
&ndash; sleeping in the desert
&ndash; DevDay@AU...

-->

### DevDay@AU and Using a Custom Exporter for 2D

Several developers recently asked how to export 2D shapes through a [custom exporter](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.1).

I recently discussed this issue with Arnošt Löbel and below present a summary and combination of several different conversation threads on the topic of [using a custom exporter for 2D](#2).

First, let me mention that I arrived safe and sound at Autodesk University.

I travelled on my birthday last week, so I was upgraded to fly in style &ndash; in a sledge!

<center>
<img src="/p/2015/2015-11-26_gatwick/jeremy_xmas_sledge_400x550.jpg" alt="Jeremy in sledge class" width="320">
</center>

At least I got to have an extra long birthday of 32 hours instead of 24.

I also spent a couple of cold nights out in the Nevada desert, mainly contemplating the views and sleeping long hours:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157659495325804" title="Sleeping in the Desert"><img src="https://farm1.staticflickr.com/619/23410878355_a3024f4b90_n.jpg" width="320" height="240" alt="Sleeping in the Desert"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

Anyway, Autodesk University starts tomorrow, and the very well attended DevDay conference took place today:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157661173495609" title="DevDay@AU"><img src="https://farm6.staticflickr.com/5685/22808116483_6e312a0790_n.jpg" width="320" height="240" alt="DevDay@AU"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

And now back to the Revit API:


#### <a name="2"></a>Using a Custom Exporter for 2D


**Question:** I'm trying to obtain the 2D representation of a given element in my model. I'm using a CustomExporter object to get all the visible elements in the current 3D view. I've been reading several posts and found in one of them, on the [extrusion analyser and plan view boundaries](http://thebuildingcoder.typepad.com/blog/2013/04/extrusion-analyser-and-plan-view-boundaries.html#6), that I should do something like "switch to a 2D plan view instead, and ask for the view-specific family instance representation". Now I'm stuck in how to get the correct 2D plan view if I actually have several in my model. Could you point me in the right direction?

For instance, to make it really short: Is there any way to export annotation symbols?

**Answer:** Take a look at the [custom exporter](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.1).

In Revit 2015 it was 3D only.

In Revit 2016 it also supports 2D (1).

Take a look at my [room editor add-in](https://github.com/jeremytammik/RoomEditorApp).

There are recordings, AU class material and lots of blog posts on it.

They include SVG export (2).

Combine (1) and (2).


**Response:** Do you mean using the CustomExporter class? The problem is that all Export methods need a 3D view as input but I cannot get any for annotations. As far as I know, the Revit 2016 CustomExporter supports 2D output but still requires a 3D view as a starting point.

Can I really grab 2D geometry from a 2D view via custom exporter?


**Answer:** You are totally correct &ndash; Custom Exporter can still export 3D views only. If a 2D view is given to it, an exception will be thrown.

However, you are also correct that since Revit 2016 the custom exporter can also export text, which is a 2D entity. It also can export model lines &ndash; they are 3D entities, but they weren't exported in previous versions because they do not render.


**Question:** Are there any plans or even wishes or vague thoughts on removing this limitation some time in the future?

Is there any way we could work around this limitation as it currently stands?

Can we create a 3D view that looks exactly like a 2D one?

Can we somehow view the annotation symbols in a 3D view?

**Answer:** No, sorry:

- There are no immediate plans to support 2D custom export.
- I do not think you can create a 3D view that will look exactly like a 2D, but it greatly depends on the view and what's in it.
- You will get text output when exporting a 3D view. Try out and see what kind of annotation is included. Presumably, if you can see it in the 3D view it should make its way to the export context as well.


**Response:** Thanks for your reply. I found The Building Coder discussion on [views displaying a given element, SVG and NoSQL](http://thebuildingcoder.typepad.com/blog/2014/05/views-displaying-given-element-svg-and-nosql.html) that was very helpful and  ended up using a similar approach. In the post you use an ElementMulticlassFilter to filter View3D, ViewPlan and ViewSection that can display a certain list of elements. In my case I just need all the plan views in the model. I iterate over that list until I find one that displays the element I need. Once I find the view I send it to an Options object and then send that object to the element  get_Geometry method to get the correct 2D representation of my element.

Here is the code showing how this can be done:

<pre class="code">
<span class="blue">internal</span> <span class="blue">class</span> <span class="teal">YBExporteContext</span> : <span class="teal">IExportContext</span>
{
&nbsp; <span class="blue">private</span> <span class="teal">Document</span> _host_document;
&nbsp; <span class="blue">private</span> <span class="teal">IEnumerable</span>&lt;<span class="teal">View</span>&gt; _2D_views_that_can_display_elements;
&nbsp;
&nbsp; <span class="blue">public</span> YBExporteContext(
&nbsp; &nbsp; <span class="teal">Document</span> document,
&nbsp; &nbsp; <span class="teal">View</span> activeView )
&nbsp; {
&nbsp; &nbsp; <span class="blue">this</span>._host_document = document;
&nbsp; &nbsp; <span class="blue">this</span>._2D_views_that_can_display_elements
&nbsp; &nbsp; &nbsp; = <span class="teal">YbUtil</span>.FindAllViewsThatCanDisplayElements(
&nbsp; &nbsp; &nbsp; &nbsp; document );
&nbsp; }
&nbsp;
&nbsp; <span class="green">/*</span>
<span class="green">&nbsp; &nbsp; * Lot of code here implementing the </span>
<span class="green">&nbsp; &nbsp; * &quot;IExportContext&quot; interface...</span>
<span class="green">&nbsp; &nbsp; */</span>
&nbsp;
&nbsp; <span class="blue">private</span> <span class="teal">GeometryElement</span> _get2DRepresentation(
&nbsp; &nbsp; <span class="teal">Element</span> element )
&nbsp; {
&nbsp; &nbsp; <span class="teal">View</span> view = <span class="blue">this</span>._get2DViewForElement( element );
&nbsp; &nbsp; <span class="blue">if</span>( view == <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">Options</span> options = <span class="blue">new</span> <span class="teal">Options</span>();
&nbsp; &nbsp; options.View = view;
&nbsp; &nbsp; <span class="blue">return</span> element.get_Geometry( options );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Gets any 2D view where the element is displayed</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;element&quot;&gt;&lt;/param&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;returns&gt;</span><span class="green">A 2D view where the element is displayed</span><span class="gray">&lt;/returns&gt;</span>
&nbsp; <span class="blue">private</span> <span class="teal">View</span> _get2DViewForElement( <span class="teal">Element</span> element )
&nbsp; {
&nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> collector;
&nbsp; &nbsp; <span class="teal">ICollection</span>&lt;<span class="teal">ElementId</span>&gt; elements_in_view;
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">View</span> view <span class="blue">in</span>
&nbsp; &nbsp; &nbsp; <span class="blue">this</span>._2D_views_that_can_display_elements )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; collector = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">this</span>._host_document, view.Id )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .WhereElementIsNotElementType();
&nbsp;
&nbsp; &nbsp; &nbsp; elements_in_view = collector.ToElementIds();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( elements_in_view.Contains( element.Id ) )
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> view;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">null</span>;
&nbsp; }
}
&nbsp;
<span class="blue">public</span> <span class="blue">static</span> <span class="blue">class</span> <span class="teal">YbUtil</span>
{
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="teal">IEnumerable</span>&lt;<span class="teal">View</span>&gt;
&nbsp; &nbsp; FindAllViewsThatCanDisplayElements(
&nbsp; &nbsp; &nbsp; <span class="teal">Document</span> doc )
&nbsp; {
&nbsp; &nbsp; <span class="teal">ElementMulticlassFilter</span> filter
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">ElementMulticlassFilter</span>( <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Type</span>&gt;
&nbsp; &nbsp; &nbsp; &nbsp; { <span class="blue">typeof</span>( <span class="teal">ViewPlan</span> ) } );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .WherePasses( filter )
&nbsp; &nbsp; &nbsp; .Cast&lt;<span class="teal">View</span>&gt;()
&nbsp; &nbsp; &nbsp; .Where( v =&gt; !v.IsTemplate &amp;&amp; v.CanBePrinted );
&nbsp; }
}
</pre>

**Answer:** Great solution! Thank you for sharing it.

For my room editor, I create a simplified 2D view from the 3D geometry using the extrusion analyser, and that also works well.

There are lots of different approaches, and you will have to try out for yourself which one works best for you.

Please let us know what you end up using.

Good luck and have fun!
