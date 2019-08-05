<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 15582252 [Create dimensions for Filled Region Boundary]
  https://forums.autodesk.com/t5/revit-api-forum/create-dimensions-for-filled-region-boundary/m-p/8926301

twitter:

Auto-dimensioning filled region boundaries using the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/dimfilledregion

I am back from my break and picked up the question about creating dimensions for a filled region boundary
&ndash; Programmatically creating dimensions for a filled region
&ndash; Coding suggestion
&ndash; Final solution...

linkedin:

Auto-dimensioning filled region boundaries using the #RevitAPI

http://bit.ly/dimfilledregion

I am back from my break and picked up the question about creating dimensions for a filled region boundary:

- Programmatically creating dimensions for a filled region
- Coding suggestion
- Final solution...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

-->

### Auto-Dimension Filled Region Boundary

I am back from my break in the French Jura and looking at all the
interesting [Revit API forum discussions](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) again.

One that stands out and that I'll pick up to get back into the blogging rhythm again is Jorge Villarroel's question
about [creating dimensions for a filled region boundary](https://forums.autodesk.com/t5/revit-api-forum/create-dimensions-for-filled-region-boundary/m-p/8926301),
answered by Alexander [@aignatovich](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478) [@CADBIMDeveloper](https://github.com/CADBIMDeveloper) Ignatovich, aka Александр Игнатович:

- [Programmatically creating dimensions for a filled region](#2)
- [Coding suggestion](#3)
- [Final solution](#4)

<center>
<img src="img/filled_region_dimensions_auto.png" alt="Filled regions auto-dimensioned" width="209">
</center>

####<a name="2"></a> Programmatically Creating Dimensions for a Filled Region

I am working with dimensions for multiple objects.
The dimension creation method needs a `ReferenceArray` to work.
Now, I need to create dimensions for a filled region:

<center>
<img src="img/filled_region.png" alt="Filled region" width="300">
<p style="font-size: 80%; font-style:italic">Filled region</p>
</center>

I can create dimensions manually in the user interface using native commands, no API, just clicking, using "Align Dimension":

<center>
<img src="img/filled_region_dimensions.png" alt="Dimensions for the filled region" width="300">
<p style="font-size: 80%; font-style:italic">Dimensions for the filled region</p>
</center>

However, I can't retrieve the reference for the boundary curves to create them programmatically.

I used RevitLookup to search for some reference in the Filled Region sub-elements with no results.

Also tried to get the references from the `CurveLoop` curves, but again, with no results.

Any tip of advice will be very well received.



####<a name="3"></a> Coding Suggestion

Hi!

The trick is to retrieve the filled region geometry using the appropriate view and setting `ComputeReferences` to true.

Try this code:

<pre class="code">
[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">CreateFillledRegionDimensionsCommand</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;uidoc&nbsp;=&nbsp;uiapp.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;view&nbsp;=&nbsp;uidoc.ActiveGraphicalView;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;filledRegions&nbsp;=&nbsp;FindFilledRegions(&nbsp;doc,&nbsp;view.Id&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;transaction&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;filled&nbsp;regions&nbsp;dimensions&quot;</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Start();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;filledRegion&nbsp;<span style="color:blue;">in</span>&nbsp;filledRegions&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CreateDimensions(&nbsp;filledRegion,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-1&nbsp;*&nbsp;view.RightDirection&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CreateDimensions(&nbsp;filledRegion,&nbsp;view.UpDirection&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;CreateDimensions(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilledRegion</span>&nbsp;filledRegion,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;dimensionDirection&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;document&nbsp;=&nbsp;filledRegion.Document;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;view&nbsp;=&nbsp;(<span style="color:#2b91af;">View</span>)&nbsp;document.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;filledRegion.OwnerViewId&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;edgesDirection&nbsp;=&nbsp;dimensionDirection.CrossProduct(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.ViewDirection&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;edges&nbsp;=&nbsp;FindRegionEdges(&nbsp;filledRegion&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x&nbsp;=&gt;&nbsp;IsEdgeDirectionSatisfied(&nbsp;x,&nbsp;edgesDirection&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToList();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;edges.Count&nbsp;&lt;&nbsp;2&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;shift&nbsp;=&nbsp;<span style="color:#2b91af;">UnitUtils</span>.ConvertToInternalUnits(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-10&nbsp;*&nbsp;view.Scale,&nbsp;<span style="color:#2b91af;">DisplayUnitType</span>.DUT_MILLIMETERS&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;edgesDirection;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;dimensionLine&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateUnbound(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;filledRegion.get_BoundingBox(&nbsp;view&nbsp;).Min&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;shift,&nbsp;dimensionDirection&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;references&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ReferenceArray</span>();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;edge&nbsp;<span style="color:blue;">in</span>&nbsp;edges&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;references.Append(&nbsp;edge.Reference&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;document.Create.NewDimension(&nbsp;view,&nbsp;dimensionLine,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;references&nbsp;);
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;IsEdgeDirectionSatisfied(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Edge</span>&nbsp;edge,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;edgeDirection&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;edgeCurve&nbsp;=&nbsp;edge.AsCurve()&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Line</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;edgeCurve&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">false</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;edgeCurve.Direction.CrossProduct(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;edgeDirection&nbsp;).IsAlmostEqualTo(&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero&nbsp;);
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Edge</span>&gt;&nbsp;FindRegionEdges(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilledRegion</span>&nbsp;filledRegion&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;view&nbsp;=&nbsp;(<span style="color:#2b91af;">View</span>)&nbsp;filledRegion.Document.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;filledRegion.OwnerViewId&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;options&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;View&nbsp;=&nbsp;view,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ComputeReferences&nbsp;=&nbsp;<span style="color:blue;">true</span>
&nbsp;&nbsp;&nbsp;&nbsp;};

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;filledRegion
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.get_Geometry(&nbsp;options&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfType&lt;<span style="color:#2b91af;">Solid</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.SelectMany(&nbsp;x&nbsp;=&gt;&nbsp;x.Edges.Cast&lt;<span style="color:#2b91af;">Edge</span>&gt;()&nbsp;);
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">FilledRegion</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;FindFilledRegions(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;document,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;viewId&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;collector&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document,&nbsp;viewId&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;collector
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FilledRegion</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">FilledRegion</span>&gt;();
&nbsp;&nbsp;}
}
</pre>

It produces something like this:

<center>
<img src="img/filled_region_dimensioned_by_ai.png" alt="Filled regions dimensioned by Alexander's code" width="422">
</center>

Dimensioning in Revit is one of my favorite topics &nbsp; :-)


####<a name="4"></a> Final Solution

Thanks, @aignatovich. I really appreciate it.

Your suggestion was the solution to my problem!

I extended the approach, so the method asks for the type name (string) of the dimension you want to assign:

<pre class="code">
<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;CreateDimensions(
&nbsp;&nbsp;<span style="color:#2b91af;">FilledRegion</span>&nbsp;filledRegion,
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;dimensionDirection,
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;typeName&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;document&nbsp;=&nbsp;filledRegion.Document;

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;view&nbsp;=&nbsp;(<span style="color:#2b91af;">View</span>)&nbsp;document.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;filledRegion.OwnerViewId&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;edgesDirection&nbsp;=&nbsp;dimensionDirection.CrossProduct(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;view.ViewDirection&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;edges&nbsp;=&nbsp;FindRegionEdges(&nbsp;filledRegion&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x&nbsp;=&gt;&nbsp;IsEdgeDirectionSatisfied(&nbsp;x,&nbsp;edgesDirection&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.ToList();

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;edges.Count&nbsp;&lt;&nbsp;2&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>;

&nbsp;&nbsp;<span style="color:green;">//&nbsp;Se&nbsp;hace&nbsp;este&nbsp;ajuste&nbsp;para&nbsp;que&nbsp;la&nbsp;distancia&nbsp;no&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;depende&nbsp;de&nbsp;la&nbsp;escala.&nbsp;&lt;&lt;&lt;&lt;&lt;&lt;&nbsp;evaluar&nbsp;para&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;información&nbsp;de&nbsp;acotado&nbsp;y&nbsp;etiquetado!!!</span>

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;shift&nbsp;=&nbsp;<span style="color:#2b91af;">UnitUtils</span>.ConvertToInternalUnits(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;5&nbsp;*&nbsp;view.Scale,&nbsp;<span style="color:#2b91af;">DisplayUnitType</span>.DUT_MILLIMETERS&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;*&nbsp;edgesDirection;

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;dimensionLine&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateUnbound(
&nbsp;&nbsp;&nbsp;&nbsp;filledRegion.get_BoundingBox(&nbsp;view&nbsp;).Min&nbsp;+&nbsp;shift,
&nbsp;&nbsp;&nbsp;&nbsp;dimensionDirection&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;references&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ReferenceArray</span>();

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;edge&nbsp;<span style="color:blue;">in</span>&nbsp;edges&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;references.Append(&nbsp;edge.Reference&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">Dimension</span>&nbsp;dim&nbsp;=&nbsp;document.Create.NewDimension(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;view,&nbsp;dimensionLine,&nbsp;references&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;dr_id&nbsp;=&nbsp;DimensionTypeId(
&nbsp;&nbsp;&nbsp;&nbsp;document,&nbsp;typeName&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;dr_id&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;dim.ChangeTypeId(&nbsp;dr_id&nbsp;);
&nbsp;&nbsp;}
}

<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;IsEdgeDirectionSatisfied(&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">Edge</span>&nbsp;edge,&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;edgeDirection&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;edgeCurve&nbsp;=&nbsp;edge.AsCurve()&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Line</span>;

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;edgeCurve&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">false</span>;

&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;edgeCurve.Direction.CrossProduct(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;edgeDirection&nbsp;).IsAlmostEqualTo(&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero&nbsp;);
}

<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">FilledRegion</span>&gt;&nbsp;
&nbsp;&nbsp;FindFilledRegions(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;document,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;viewId&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;collector&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;document,&nbsp;viewId&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;collector
&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FilledRegion</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">FilledRegion</span>&gt;();
}

<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Edge</span>&gt;&nbsp;
&nbsp;&nbsp;FindRegionEdges(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilledRegion</span>&nbsp;filledRegion&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;view&nbsp;=&nbsp;(<span style="color:#2b91af;">View</span>)&nbsp;filledRegion.Document.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;filledRegion.OwnerViewId&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;options&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;View&nbsp;=&nbsp;view,
&nbsp;&nbsp;&nbsp;&nbsp;ComputeReferences&nbsp;=&nbsp;<span style="color:blue;">true</span>
&nbsp;&nbsp;};

&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;filledRegion
&nbsp;&nbsp;&nbsp;&nbsp;.get_Geometry(&nbsp;options&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfType&lt;<span style="color:#2b91af;">Solid</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.SelectMany(&nbsp;x&nbsp;=&gt;&nbsp;x.Edges.Cast&lt;<span style="color:#2b91af;">Edge</span>&gt;()&nbsp;);
}

<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;DimensionTypeId(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;typeName&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;mt_coll&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">DimensionType</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsElementType();

&nbsp;&nbsp;<span style="color:#2b91af;">DimensionType</span>&nbsp;dimType&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;type&nbsp;<span style="color:blue;">in</span>&nbsp;mt_coll&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;type&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">DimensionType</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;type.Name&nbsp;==&nbsp;typeName&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dimType&nbsp;=&nbsp;type&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">DimensionType</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;dimType.Id;
}
</pre>

This code produces dimensioning as shown in the top-most screen snapshot.

Hope this is helpful for others also!

Many thanks to Jorge and Alexander for this nice solution!
