<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 14189116 [Visibility of DWG layer]
  https://forums.autodesk.com/t5/revit-api-forum/visibility-of-dwg-layer/m-p/7975284
  CmdProcessVisibleDwg

- 14216556 [raster image Import in Revit]

Foreground image import and visible DWG geometry in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/visibledwggeo

Today, we explore how to retrieve visible DWG geometry, i.e., geometry elements contained in a CAD import instance on a layer that is visible in the currently active view, and how to import an image to the foreground instead of the default background setting
&ndash; Retrieve CAD import geometry on visible layer
&ndash; Import image using foreground option...

--->

### Foreground Image Import and Visible DWG Geometry

Today, we explore how to retrieve visible DWG geometry, i.e., geometry elements contained in a CAD import instance on a layer that is visible in the currently active view, and how to import an image to the foreground instead of the default background setting:

- [Retrieve CAD import geometry on visible layer](#2) 
- [Import image using foreground option](#3) 


####<a name="2"></a>Retrieve CAD Import Geometry on Visible Layer

Ryan Goertzen of [Goertzen Enterprises](http://goertzen-ltd.com) shared a nice sample showing how to retrieve visible DWG geometry, i.e., geometry elements contained in a CAD import instance on a layer that is visible in the currently active view, in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [visibility of DWG layer](https://forums.autodesk.com/t5/revit-api-forum/visibility-of-dwg-layer/m-p/7975284):

**Question:** I am trying to extract geometry from an import instance whose layers are currently visible in the current view.

I pass the view into the geometry options, but it still returns all geometry in the instance.

I can get the name of the DWG layer from:

<pre class="code">
  (curDoc.GetElement(curObj.GraphicsStyleId)
    as GraphicsStyle).Name;
</pre>

However, I cannot seem to find anywhere in the `View` class to check if this layer is on or off, which is achievable with the UI visibility dialog.

What would be the best way to approach this?

**Answer:** I looked at the description how
to [hide layers in CAD files](http://help.autodesk.com/view/RVT/2019/ENU/?guid=GUID-EBCAFF76-1BF5-45F1-AB51-9130227091D1) (also
in [LT](https://knowledge.autodesk.com/support/revit-lt/learn-explore/caas/CloudHelp/cloudhelp/2019/ENU/RevitLT-Model/files/GUID-EBCAFF76-1BF5-45F1-AB51-9130227091D1-htm.html?_ga=2.222683099.1372620627.1525160338-2130181328.1465883366)).
It refers to 'imported categories'.

Therefore, you may be able to affect the visibility in the Revit view by controlling the category visibility.

The following two methods do so:

- [GetCategoryHidden](http://www.revitapidocs.com/2018.1/52ce4cea-6f27-9e85-f82a-115e308eebfc.htm) &ndash; Checks if elements of the given category are set to be invisible (hidden) in this view
- [SetCategoryHidden](http://www.revitapidocs.com/2018.1/87a1e1e2-ee81-1a73-19d7-895b1fa10158.htm) &ndash; Sets if elements of the given category will be visible in this view

To test, you might call `GetCategoryHidden`, make a note of what it returns, modify the setting for a specific layer manually, and check the return value again to see whether it changed.

If so, it may be possible to use `SetCategoryHidden` to control the layer visibility.

**Response:** I see now that the layer categories that I was after to pass into `GetCategoryHidden` are nested  one step further down in `GraphicsStyle` &rarr; `GraphicsStyleCategory` &rarr; `Id`.

In my testing, I was thinking that the API wanted a built-in category. For that, all I could find was the built-in category `OST_ImportObjectStyles`.

The following code works as expected, only adding the DWG geometry that is currently visible in view to the list:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GeometryObject</span>&nbsp;gObject&nbsp;<span style="color:blue;">in</span>&nbsp;gElement&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;gInstance&nbsp;=&nbsp;(<span style="color:#2b91af;">GeometryInstance</span>)&nbsp;gObject;
&nbsp;&nbsp;&nbsp;&nbsp;gElement2&nbsp;=&nbsp;gInstance.GetInstanceGeometry();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GeometryObject</span>&nbsp;obj&nbsp;<span style="color:blue;">in</span>&nbsp;gElement2&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;gStyle&nbsp;=&nbsp;curDoc.GetElement(&nbsp;obj.GraphicsStyleId&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">GraphicsStyle</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Try&nbsp;Catch&nbsp;because&nbsp;i&nbsp;was&nbsp;getting&nbsp;some&nbsp;Object</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Instance&nbsp;errors&nbsp;with&nbsp;the&nbsp;graphics&nbsp;style&nbsp;object.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;object&nbsp;to&nbsp;list</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!view.GetCategoryHidden(&nbsp;gStyle.GraphicsStyleCategory.Id&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;list.Add(&nbsp;obj&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>&nbsp;{&nbsp;<span style="color:blue;">continue</span>;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

This required cleaning it up a bit to avoid calling `GetInstanceGeometry` without first checking whether `gInstance` is null, and eliminate
the [evil catch-all exception handler](http://thebuildingcoder.typepad.com/blog/2017/05/prompt-cancel-throws-exception-in-revit-2018.html#5).

To try it out yourself at home, here is
a [simple demonstration project, `dwg_visible_geo_2018.rvt`](zip/dwg_visible_geo_2018.rvt)
([also for Revit 2019](zip/zip/dwg_visible_geo_2019.rvt)).

The project includes a basic macro that will draw a detailed region around the visible layer in
the [linked CAD file, `dwg_visible_geo.dwg`](zip/dwg_visible_geo.dwg):

<pre class="code">
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Pick&nbsp;a&nbsp;DWG&nbsp;import&nbsp;instance,&nbsp;extract&nbsp;polylines&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;from&nbsp;it&nbsp;visible&nbsp;in&nbsp;the&nbsp;current&nbsp;view&nbsp;and&nbsp;create</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;filled&nbsp;regions&nbsp;from&nbsp;them.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;ProcessVisible(&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;active_view&nbsp;=&nbsp;doc.ActiveView;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">GeometryObject</span>&gt;&nbsp;visible_dwg_geo&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">GeometryObject</span>&gt;();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Pick&nbsp;Import&nbsp;Instance</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;r&nbsp;=&nbsp;uidoc.Selection.PickObject(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ObjectType</span>.Element,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">JtElementsOfClassSelectionFilter</span>&lt;<span style="color:#2b91af;">ImportInstance</span>&gt;()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;import&nbsp;=&nbsp;doc.GetElement(&nbsp;r&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">ImportInstance</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;Geometry</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;ge&nbsp;=&nbsp;import.get_Geometry(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;go&nbsp;<span style="color:blue;">in</span>&nbsp;ge&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;go&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">GeometryInstance</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;gi&nbsp;=&nbsp;go&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">GeometryInstance</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;ge2&nbsp;=&nbsp;gi.GetInstanceGeometry();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ge2&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;obj&nbsp;<span style="color:blue;">in</span>&nbsp;ge2&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Only&nbsp;work&nbsp;on&nbsp;PolyLines</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;obj&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">PolyLine</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Use&nbsp;the&nbsp;GraphicsStyle&nbsp;to&nbsp;get&nbsp;the&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;DWG&nbsp;layer&nbsp;linked&nbsp;to&nbsp;the&nbsp;Category&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;for&nbsp;visibility.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;gStyle&nbsp;=&nbsp;doc.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;obj.GraphicsStyleId&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">GraphicsStyle</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Check&nbsp;if&nbsp;the&nbsp;layer&nbsp;is&nbsp;visible&nbsp;in&nbsp;the&nbsp;view.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!active_view.GetCategoryHidden(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;gStyle.GraphicsStyleCategory.Id&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;visible_dwg_geo.Add(&nbsp;obj&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Do&nbsp;something&nbsp;with&nbsp;the&nbsp;info</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;visible_dwg_geo.Count&nbsp;&gt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;first&nbsp;filled&nbsp;region&nbsp;type</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;filledType&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FilledRegionType</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfType&lt;<span style="color:#2b91af;">FilledRegionType</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.First();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;ProcessDWG&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;obj&nbsp;<span style="color:blue;">in</span>&nbsp;visible_dwg_geo&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;poly&nbsp;=&nbsp;obj&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">PolyLine</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Draw&nbsp;a&nbsp;filled&nbsp;region&nbsp;for&nbsp;each&nbsp;polyline</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;poly&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;loops&nbsp;for&nbsp;detail&nbsp;region</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;curveLoop&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveLoop</span>();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;points&nbsp;=&nbsp;poly.GetCoordinates();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">for</span>(&nbsp;<span style="color:blue;">int</span>&nbsp;i&nbsp;=&nbsp;0;&nbsp;i&nbsp;&lt;&nbsp;points.Count&nbsp;-&nbsp;1;&nbsp;++i&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;curveLoop.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;points[i],&nbsp;points[i&nbsp;+&nbsp;1]&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilledRegion</span>.Create(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;filledType.Id,&nbsp;active_view.Id,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">CurveLoop</span>&gt;()&nbsp;{&nbsp;curveLoop&nbsp;}&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

<center>
<img src="img/dwg_visible_geo.png" alt="Visible DWG geometry" width="407"/>
</center>

Many thanks to Ryan for raising, solving and sharing this!

I copied the macro code to
[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2019.0.139.2](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.139.2)
in
order to format and preserve it for posterity in
the [module CmdProcessVisibleDwg.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdProcessVisibleDwg.cs).


####<a name="3"></a>Import Image Using Foreground Option
 
Another import question, this time on images:

**Question:** When I insert a raster image into Revit, by default, it is displayed as 'background'.

Is there any way I can do it using the 'Foreground' option?

It would be a great help to know the property that I can set using the C# Revit API.

I am working on some automation projects, and all the images must be displayed as 'Foreground'.

**Answer:** I first took a look at the Revit API documentation on
the [Import method with `ImageImportOptions`](http://www.revitapidocs.com/2018.1/493d203e-ef4c-b447-f979-52b22725b2ad.htm),
the [ImageImportOptions class](http://www.revitapidocs.com/2018.1/0d92888c-aa24-5d7e-c7f9-a7bdf24e5581.htm) and
[its members](http://www.revitapidocs.com/2018.1/b45a2657-8cdf-6471-4929-a758d1675b17.htm).
It does not answer your question off-hand.

Next, I asked the development team, who explained:

> Not at the time of import.

> However, you can set the built-in parameter `IMPORT_BACKGROUND` to `0` on the `ImageInstance` afterwards.

> The element id of the image instance is returned from the `Document.Import` overloaded method for importing images.

**Response:** As you suggested, I set the parameter `IMPORT_BACKGROUND` to `0`.

It worked.

Thanks a lot for your help.
