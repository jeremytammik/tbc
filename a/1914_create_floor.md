<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- get total polygon count
  How to get polygon count of the project
  https://forums.autodesk.com/t5/revit-api-forum/how-to-get-polygon-count-of-the-project/m-p/10530975
  CmdTriangleCount
  tbc_samples_triangle_count.png 974
  tbc samples implemented and tested and debugged and improved CmdTriangleCount
  https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2022.0.151.0

- blog about the code snippet and instruction on floor creation API   
  https://autodesk.slack.com/archives/C0SR6NAP8/p1627395932194800
  Oleg Sheydvasser 27 Jul at 16:25
  It appears we need to provide clarifications to the users on the new Floor creation API that was introduced in R2022.
  A few old APIs were obsoleted, but the new methods work a bit differently (e.g. see https://app.slack.com/client/T02NW42JD/threads), so we need to provide instructions on how to migrate from the old API to new.
  Where should I put the instructions? The What's new section, the snippets?
  Scott Conover:no_entry:  22 days ago
  Snippets would be helpful.  For immediate availability, I'd suggest providing any guidance to @Jeremy Tammik so he can post about it faster than we can release an updated SDK.

- Dynamo Studio EOL
  https://knowledge.autodesk.com/support/dynamo-studio/learn-explore/caas/simplecontent/content/dynamo-studio-faq.html

- solar panels
  many_solar_panels.jpg
  jtracer
  running into all the hurdles described in [Learning from the real world: A hardware hobby project]
  https://stackoverflow.blog/2021/07/12/the-difference-between-software-and-hardware-projects/

twitter:

add #thebuildingcoder

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

Getting back into the flow after my summer break
&ndash; Model polygon or triangle count
&ndash; Floor creation API clarification
&ndash; Dynamo Studio EOL
&ndash; My solar power project...

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

### Triangle Count, New Floor and Slab Creation

I am back from my break, which I mostly spent chilling with friends and hiking in the Jura and Ticino hills and mountains in the west and south of Switzerland.

Let's start getting back into the flow again with these news bites:

- [Model polygon or triangle count](#2)
- [Floor creation API clarification](#3)
- [Dynamo Studio EOL](#4)
- [My solar power project](#5)

####<a name="2"></a> Total Modal Polygon or Triangle Count

A recent [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
asked [how to get polygon count of the project](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-polygon-count-of-the-project/m-p/10530975):

**Question:** I am exporting the model into OBJ format.
Is it possible to the polygon count or the number of triangles in the model before export?
Does Revit provide any API for that?
Can I count the polygons for each element and then add them all?

**Answer:** No, no such API is provided ready-built.

As a non-programmer, you could export your Revit project (.rvt) or family (.rfa) file to FBX format and use an external viewer to view and analyse that.
For instance, in Revit, go to File > Export > FBX.
Open the FBX file in [Microsoft 3D Viewer](https://www.microsoft.com/en-us/p/3d-viewer/9nblggh42ths).
Depending on the file size, it may take a little while to open.
A spinning 3D box icon will indicate loading is in progress.
In the viewer, go to Tools and click on "Stats & Shading".
It will list the number of triangles and vertices.
The 3D Viewer is a free download if you don't already have it in Windows 10.

As a programmer, you can do as you suggest, retrieve all the element geometry, tessellate it, and sum up the total number of triangles.

You might find it easiest to do so using
a [custom exporter](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.1).
The result should be the same as in the FBX export.

[@techXMKH9](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/5770785) very
kindly shared a nice and minimal sample custom exporter implementation that I integrated into a new external command `CmdTriangleCount`
in [release 2022.0.151.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2022.0.151.0)
of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples):

<pre class="code">
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">TriangleCounterContext</span>&nbsp;:&nbsp;IExportContext
{
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;Document&nbsp;document;
 
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;Func&lt;<span style="color:blue;">bool</span>&gt;&nbsp;isCanceled;
 
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Callback&nbsp;at&nbsp;end&nbsp;with&nbsp;total&nbsp;count&nbsp;of&nbsp;model&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;geometry&nbsp;triangles&nbsp;and&nbsp;material&nbsp;ids</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;Action&lt;<span style="color:blue;">long</span>,&nbsp;<span style="color:blue;">int</span>&gt;&nbsp;callback;
 
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">long</span>&nbsp;numTriangles;
 
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;List&lt;ElementId&gt;&nbsp;materialIds;
 
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;includeMaterials&nbsp;=&nbsp;<span style="color:blue;">true</span>;
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;TriangleCounterContext(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;document,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Func&lt;<span style="color:blue;">bool</span>&gt;&nbsp;isCanceled,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Action&lt;<span style="color:blue;">long</span>,&nbsp;<span style="color:blue;">int</span>&gt;&nbsp;callback&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.isCanceled&nbsp;=&nbsp;isCanceled;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.callback&nbsp;=&nbsp;callback;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.document&nbsp;=&nbsp;document;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.materialIds&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;ElementId&gt;();
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnPolymesh(&nbsp;PolymeshTopology&nbsp;polymesh&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.numTriangles&nbsp;+=&nbsp;(<span style="color:blue;">long</span>)&nbsp;polymesh.NumberOfFacets;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Finish()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.callback(&nbsp;<span style="color:blue;">this</span>.numTriangles,&nbsp;<span style="color:blue;">this</span>.materialIds.Count&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;IsCanceled()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;Start()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.materialIds&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;ElementId&gt;();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnRPC(&nbsp;RPCNode&nbsp;node&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnLight(&nbsp;LightNode&nbsp;node&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;RenderNodeAction&nbsp;OnViewBegin(&nbsp;ViewNode&nbsp;node&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;node.LevelOfDetail&nbsp;=&nbsp;8;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;0;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnViewEnd(&nbsp;ElementId&nbsp;elementId&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;RenderNodeAction&nbsp;OnFaceBegin(&nbsp;FaceNode&nbsp;node&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;0;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnFaceEnd(&nbsp;FaceNode&nbsp;node&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;RenderNodeAction&nbsp;OnElementBegin(&nbsp;ElementId&nbsp;elementId&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;0;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnElementEnd(&nbsp;ElementId&nbsp;elementId&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;RenderNodeAction&nbsp;OnInstanceBegin(&nbsp;InstanceNode&nbsp;node&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;0;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnInstanceEnd(&nbsp;InstanceNode&nbsp;node&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;RenderNodeAction&nbsp;OnLinkBegin(&nbsp;LinkNode&nbsp;node&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;0;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnLinkEnd(&nbsp;LinkNode&nbsp;node&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnMaterial(&nbsp;MaterialNode&nbsp;node&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">this</span>.includeMaterials&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;node.MaterialId&nbsp;==&nbsp;ElementId.InvalidElementId&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">this</span>.materialIds.Contains(&nbsp;node.MaterialId&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.materialIds.Add(&nbsp;node.MaterialId&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
 
<span style="color:blue;">void</span>&nbsp;TriangleCountReport(&nbsp;<span style="color:blue;">long</span>&nbsp;nTriangles,&nbsp;<span style="color:blue;">int</span>&nbsp;nMaterials&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Total&nbsp;number&nbsp;of&nbsp;model&nbsp;triangles&nbsp;and&nbsp;materials:&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;{0}&nbsp;triangle{1},&nbsp;{2}&nbsp;material{3}&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;nTriangles,&nbsp;Util.PluralSuffix(&nbsp;nTriangles&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;nMaterials,&nbsp;Util.PluralSuffix(&nbsp;nMaterials&nbsp;)&nbsp;);
 
&nbsp;&nbsp;Debug.Print(&nbsp;s&nbsp;);
&nbsp;&nbsp;TaskDialog.Show(&nbsp;<span style="color:#a31515;">&quot;Triangle&nbsp;Count&quot;</span>,&nbsp;s&nbsp;);
}
 
<span style="color:blue;">public</span>&nbsp;Result&nbsp;Execute(
&nbsp;&nbsp;ExternalCommandData&nbsp;commandData,
&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;ElementSet&nbsp;elements&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;app&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;uidoc&nbsp;=&nbsp;app.ActiveUIDocument;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;TriangleCounterContext&nbsp;context&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;TriangleCounterContext(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;<span style="color:blue;">null</span>,&nbsp;TriangleCountReport&nbsp;);
 
&nbsp;&nbsp;CustomExporter&nbsp;exporter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;CustomExporter(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;context&nbsp;);
 
&nbsp;&nbsp;exporter.Export(&nbsp;doc.ActiveView&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;Result.Succeeded;
}
</pre>

Here is the result of running it in a minimal sample model:

<center>
<img src="img/tbc_samples_triangle_count.png" alt="Triangle count" title="Triangle count" width="487"/> <!-- 974 -->
</center>

####<a name="3"></a> Floor Creation API Clarification

The development team provide some clarification on how to user
the [new floor creation API](https://thebuildingcoder.typepad.com/blog/2021/04/whats-new-in-the-revit-2022-api.html#4.1.4.1) introduced
in Revit 2022.

Some old APIs were obsoleted, and the new methods work a bit differently, so some instructions on how to migrate from the old API to the new may come in handy, especially a sample code snippet like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;The&nbsp;example&nbsp;below&nbsp;shows&nbsp;how&nbsp;to&nbsp;use&nbsp;Floor.Create&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;method&nbsp;to&nbsp;create&nbsp;a&nbsp;new&nbsp;Floor&nbsp;with&nbsp;a&nbsp;specified&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;elevation&nbsp;on&nbsp;a&nbsp;level&nbsp;using&nbsp;a&nbsp;geometry&nbsp;profile&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;and&nbsp;a&nbsp;floor&nbsp;type.&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;It&nbsp;shows&nbsp;how&nbsp;to&nbsp;adapt&nbsp;your&nbsp;old&nbsp;code&nbsp;using&nbsp;the</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;NewFloor&nbsp;and&nbsp;NewSlab&nbsp;methods,&nbsp;which&nbsp;became&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;obsolete&nbsp;with&nbsp;Revit&nbsp;2022.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;In&nbsp;this&nbsp;sample,&nbsp;the&nbsp;geometry&nbsp;profile&nbsp;is&nbsp;a&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;CurveLoop&nbsp;of&nbsp;lines;&nbsp;you&nbsp;can&nbsp;also&nbsp;use&nbsp;arcs,&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;ellipses&nbsp;and&nbsp;splines.</span>
&nbsp;&nbsp;Floor&nbsp;CreateFloorAtElevation(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;document,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;elevation&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;a&nbsp;floor&nbsp;type&nbsp;for&nbsp;floor&nbsp;creation</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;You&nbsp;must&nbsp;provide&nbsp;a&nbsp;valid&nbsp;floor&nbsp;type&nbsp;(unlike&nbsp;the&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;obsolete&nbsp;NewFloor&nbsp;and&nbsp;NewSlab&nbsp;methods).</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;ElementId&nbsp;floorTypeId&nbsp;=&nbsp;Floor.GetDefaultFloorType(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document,&nbsp;<span style="color:blue;">false</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;a&nbsp;level</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;You&nbsp;must&nbsp;provide&nbsp;a&nbsp;valid&nbsp;level&nbsp;(unlike&nbsp;the&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;obsolete&nbsp;NewFloor&nbsp;and&nbsp;NewSlab&nbsp;methods).</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;offset;
&nbsp;&nbsp;&nbsp;&nbsp;ElementId&nbsp;levelId&nbsp;=&nbsp;Level.GetNearestLevelId(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document,&nbsp;elevation,&nbsp;<span style="color:blue;">out</span>&nbsp;offset&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Build&nbsp;a&nbsp;floor&nbsp;profile&nbsp;for&nbsp;the&nbsp;floor&nbsp;creation</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;first&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;0,&nbsp;0,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;second&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;20,&nbsp;0,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;third&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;20,&nbsp;15,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;fourth&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;0,&nbsp;15,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;CurveLoop&nbsp;profile&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;CurveLoop();
&nbsp;&nbsp;&nbsp;&nbsp;profile.Append(&nbsp;Line.CreateBound(&nbsp;first,&nbsp;second&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;profile.Append(&nbsp;Line.CreateBound(&nbsp;second,&nbsp;third&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;profile.Append(&nbsp;Line.CreateBound(&nbsp;third,&nbsp;fourth&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;profile.Append(&nbsp;Line.CreateBound(&nbsp;fourth,&nbsp;first&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;The&nbsp;elevation&nbsp;of&nbsp;the&nbsp;curve&nbsp;loops&nbsp;is&nbsp;not&nbsp;taken&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;into&nbsp;account&nbsp;(unlike&nbsp;the&nbsp;obsolete&nbsp;NewFloor&nbsp;and&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;NewSlab&nbsp;methods).</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;If&nbsp;the&nbsp;default&nbsp;elevation&nbsp;is&nbsp;not&nbsp;what&nbsp;you&nbsp;want,&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;you&nbsp;need&nbsp;to&nbsp;set&nbsp;it&nbsp;explicitly.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;floor&nbsp;=&nbsp;Floor.Create(&nbsp;document,&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;CurveLoop&gt;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;profile&nbsp;},&nbsp;floorTypeId,&nbsp;levelId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;Parameter&nbsp;param&nbsp;=&nbsp;floor.get_Parameter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;param.Set(&nbsp;offset&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;floor;
&nbsp;&nbsp;}
</pre>

Sorry for the late information, and I hope it still helps with your migration.

It definitely helps for me, since I still have exactly two remaining warnings when compiling The Building Coder samples:

- Warning	CS0618 `Document.NewFloor(CurveArray, bool)` is obsolete; this method is deprecated in Revit 2022 and may be removed in the future version of Revit. To create new instance of Floor, call Floor.Create() &ndash; in CmdEditFloor.cs line 119
- Warning	CS0618 `Document.NewSlab(CurveArray, Level, Line, double, bool)` is obsolete; this method is deprecated in Revit 2022 and may be removed in the future version of Revit. To create new instance of Floor, call Floor.Create() &ndash; in CmdCreateSlopedSlab.cs line 88

I can make use of the sample snippet to fix these two now :-)

Many thanks to Oleg Sheidwasser for providing this!

####<a name="4"></a> Dynamo Studio EOL

Dynamo Studio is nearing its end of life.

That does not affect the rest of the Dynamo project in any way, though, nor its many other incarnations in various shapes and forms.

Please refer to
the [Dynamo Studio Frequently Asked Questions](https://knowledge.autodesk.com/support/dynamo-studio/learn-explore/caas/simplecontent/content/dynamo-studio-faq.html) for
detailed information on the current state and future plans for Dynamo.

####<a name="5"></a> Solar Power Project

I started experimenting with solar panels and an off-grid system requiring a charger, battery and inverter to generate standard 230 V AC power. 

I installed four 100 W peak panels on a small rather steep south facing balcony roof.
Actually, it has a 33 degree twist towards the west, so the direction is SSW.

They did not provide much power before almost midday in summertime, so I later added four more facing east, or rather ESE.

I ran into numerous challenges working with hardware rather than software, nicely described in the article
on [learning from the real world: a hardware hobby project](https://stackoverflow.blog/2021/07/12/the-difference-between-software-and-hardware-projects).

One such challenge was implementing a performance monitor for the charger, including the required serial connection cables, etc.
I published a little piece of associated software in
the [jtracer GitHub repository](https://github.com/jeremytammik/jtracer)

I was initially working with a 12 V battery and am now in the process of upgrading to 24 V.

Once that is up and running, I may start on something bigger, trying to provide enough electrical power for several more apartments.

I hope I don't end up with something like this:

<center>
<img src="img/many_solar_panels.jpg" alt="Solar panels" title="Solar panels" width="400"/> <!-- 800 -->
</center>

