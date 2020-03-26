<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- Is there any api available to split a duct programmatically?
  https://forums.autodesk.com/t5/revit-api-forum/is-there-any-api-available-to-split-a-duct-programmatically/td-p/6926621

- https://thebuildingcoder.typepad.com/blog/2020/03/split-pipe-and-headless-revit.html#comment-4835671086
  Matt Taylor

twitter:

We discussed the BreakCurve API for splitting a pipe.
Here comes a much more comprehensive discussion on splitting a duct, not just an example, an entire tutorial in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon https://bit.ly/splitduct

We recently shared a brief note on using the <code>BreakCurve</code> API for splitting a pipe.
Matt Taylor now pointed out a much more comprehensive discussion asking, is there any API available to split a duct programmatically?
That is not just an example, but an entire tutorial, so I think it is very useful to share here as well for all to enjoy..

&ndash; 
...

linkedin:

We discussed the BreakCurve API for splitting a pipe.

Here comes a much more comprehensive discussion on splitting a duct, not just an example, an entire tutorial in the #RevitAPI

https://bit.ly/splitduct

We recently shared a brief note on using the BreakCurve API for splitting a pipe.

Matt Taylor now pointed out a much more comprehensive discussion asking, is there any API available to split a duct programmatically?

That is not just an example, but an entire tutorial, so I think it is very useful to share here as well for all to enjoy...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Splitting a Duct in More Depth

We recently shared a brief note on using the `BreakCurve` API for [splitting a pipe](https://thebuildingcoder.typepad.com/blog/2020/03/split-pipe-and-headless-revit.html).

In a [comment](https://thebuildingcoder.typepad.com/blog/2020/03/split-pipe-and-headless-revit.html#comment-4835671086) on that,
Matt Taylor pointed out a much more comprehensive discussion in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
asking [is there any API available to split a duct programmatically?](https://forums.autodesk.com/t5/revit-api-forum/is-there-any-api-available-to-split-a-duct-programmatically/td-p/6926621).

That is not just an example, but an entire tutorial, so I think it is very useful to share here as well for all to enjoy:

**Question:** I need to split a duct programmatically ? Is there any API available to do so?

**Answer:** I explained how this can be achieved using the `BreakCurve` method in my comments
on [duct splitting](https://forums.autodesk.com/t5/revit-api-forum/duct-splitting/m-p/6784012)

**Response:** I tried using the `BreakCurve` method for splitting a duct, but it throws an exception at the time of execution.

Can you please guide me on how to pass the third parameter, `XYZ` `ptBreak`?

<center>
<img src="img/split_duct_BreakCurveIssue.jpg" alt="BreakCurve issue" title="BreakCurve issue" width="800"/> <!-- 1264 -->
</center>


**Answer:** Well, I think the error says it all! The point isn't on the duct curve.

Here's a simple example that breaks a duct 2 feet along its length.

<pre class="code">
&lt;<span style="color:#2b91af;">Transaction</span>(<span style="color:#2b91af;">TransactionMode</span>.Manual)&gt;
&lt;<span style="color:#2b91af;">Regeneration</span>(<span style="color:#2b91af;">RegenerationOption</span>.Manual)&gt;
&lt;<span style="color:#2b91af;">Journaling</span>(<span style="color:#2b91af;">JournalingMode</span>.UsingCommandData)&gt;
<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Class</span>&nbsp;<span style="color:#2b91af;">TransactionCommand</span>
&nbsp;&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.IExternalCommand
&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByVal</span>&nbsp;commandData&nbsp;<span style="color:blue;">As</span>&nbsp;UI.ExternalCommandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByRef</span>&nbsp;message&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>,&nbsp;<span style="color:blue;">ByVal</span>&nbsp;elements&nbsp;<span style="color:blue;">As</span>&nbsp;DB.ElementSet)&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">As</span>&nbsp;UI.Result&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.IExternalCommand.Execute
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;app&nbsp;<span style="color:blue;">As</span>&nbsp;ApplicationServices.Application&nbsp;=&nbsp;commandData.Application.Application
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;doc&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Document&nbsp;=&nbsp;commandData.Application.ActiveUIDocument.Document
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;docUi&nbsp;<span style="color:blue;">As</span>&nbsp;UI.UIDocument&nbsp;=&nbsp;commandData.Application.ActiveUIDocument
 
&nbsp;&nbsp;&nbsp;&nbsp;Execute&nbsp;=&nbsp;UI.Result.Failed
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Using</span>&nbsp;transaction&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;DB.Transaction(doc,&nbsp;<span style="color:#a31515;">&quot;Break&nbsp;Duct&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Start()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;duct&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Mechanical.Duct&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(doc.GetElement(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">New</span>&nbsp;DB.ElementId(1789723)),&nbsp;DB.Mechanical.Duct)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;curve&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Curve&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(duct.Location,&nbsp;DB.LocationCurve).Curve
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;pt0&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;curve.GetEndPoint(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;pt1&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;curve.GetEndPoint(1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;vector&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;pt1.Subtract(pt0).Normalize
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;breakPt&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;pt0.Add(vector.Multiply(2.0))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;newDuctId&nbsp;<span style="color:blue;">As</span>&nbsp;DB.ElementId&nbsp;=&nbsp;DB.Mechanical.MechanicalUtils.BreakCurve(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;duct.Id,&nbsp;breakPt)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Commit()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">&#39;&nbsp;change&nbsp;our&nbsp;result&nbsp;to&nbsp;successful</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Execute&nbsp;=&nbsp;UI.Result.Succeeded
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Using</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Execute
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Class</span>
</pre>

**Response:** It worked pleasantly.  Smiley Happy

Normally, if I split a duct in the UI, it adds a family instance, e.g., a fitting, between the two separated elements. 

`BreakCurve` just breaks the element into two parts at the specified length, which is really good, but I also need a specific family instance to be added between the two elements, just like in the UI.

<center>
<img src="img/split_duct_2.jpg" alt="Splitting duct" title="Splitting duct" width="600"/> <!-- 1264 -->
</center>

**Answer:** Well, you probably need to break the duct in two places.

You have the elementIds of the two ducts after one break; you just need to use one of those in your second break.

The second break would have to be enclosed in a second transaction.

Once you have the third elementId, you can get its duct element and use `duct.ChangeTypeId` to change the type should you desire it.

**Response:** My requirement is different.
This is what the `BreakCurve` method generates:

<center>
<img src="img/split_duct_AfterUsingBreakCurve.jpg" alt="After using BreakCurve" title="After using BreakCurve" width="600"/> <!-- 1264 -->
</center>

Can I add a fitting between the two duct elements that will act as a separation point?

Here are screenshots showing the situation before and after splitting an element via the UI and API:

Before:

<center>
<img src="img/split_duct_BeforeFromUI.jpg" alt="Before" title="Before" width="600"/> <!-- 1264 -->
</center>

After, when splitting manually via UI:

<center>
<img src="img/split_duct_AfterUsingSplitFromUI.jpg" alt="After UI split" title="After UI split" width="600"/> <!-- 1264 -->
</center>

Here, you can see 3 elements.
Two are child elements created by splitting the parent element.
The third one is the fitting that separates the two.

This is the fitting that was added:

<center>
<img src="img/split_duct_ManualSplit.jpg" alt="Fitting" title="Fitting" width="600"/> <!-- 1264 -->
</center>

After, when splitting using the `BreakCurve` API:

<center>
<img src="img/split_duct_after_using_BreakCurve_2.jpg" alt="BreakCurve result" title="BreakCurve result" width="600"/> <!-- 1264 -->
</center>

So, here the fitting needs to be added between the two duct elements, just like it was using the manual split command.

**Answer:** Riiiiiiiight. You need to add a union fitting. That is a duct fitting, and a family instance.

When in doubt about the wording, you can describe the elements using the Revit descriptions or the RevitLookup (API) names.
That reduces the potential for confusion.

I originally used two transactions, but it appears a regen will do the trick:

<pre class="code">
&lt;<span style="color:#2b91af;">Transaction</span>(<span style="color:#2b91af;">TransactionMode</span>.Manual)&gt;
&lt;<span style="color:#2b91af;">Regeneration</span>(<span style="color:#2b91af;">RegenerationOption</span>.Manual)&gt;
&lt;<span style="color:#2b91af;">Journaling</span>(<span style="color:#2b91af;">JournalingMode</span>.UsingCommandData)&gt;
<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Class</span>&nbsp;<span style="color:#2b91af;">TransactionCommand</span>
&nbsp;&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.IExternalCommand
&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByVal</span>&nbsp;commandData&nbsp;<span style="color:blue;">As</span>&nbsp;UI.ExternalCommandData,&nbsp;<span style="color:blue;">ByRef</span>&nbsp;message&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByVal</span>&nbsp;elements&nbsp;<span style="color:blue;">As</span>&nbsp;DB.ElementSet)&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">As</span>&nbsp;UI.Result&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.IExternalCommand.Execute
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;app&nbsp;<span style="color:blue;">As</span>&nbsp;ApplicationServices.Application&nbsp;=&nbsp;commandData.Application.Application
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;doc&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Document&nbsp;=&nbsp;commandData.Application.ActiveUIDocument.Document
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;docUi&nbsp;<span style="color:blue;">As</span>&nbsp;UI.UIDocument&nbsp;=&nbsp;commandData.Application.ActiveUIDocument
 
&nbsp;&nbsp;&nbsp;&nbsp;Execute&nbsp;=&nbsp;UI.Result.Failed
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;newDuctId&nbsp;<span style="color:blue;">As</span>&nbsp;DB.ElementId&nbsp;=&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;ductOrig&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Mechanical.Duct&nbsp;=&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;duct2&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Mechanical.Duct&nbsp;=&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;breakPt&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Using</span>&nbsp;transaction&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;DB.Transaction(doc,&nbsp;<span style="color:#a31515;">&quot;Break&nbsp;Duct&nbsp;+&nbsp;Add&nbsp;Fitting&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Start()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ductOrig&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(doc.GetElement(<span style="color:blue;">New</span>&nbsp;DB.ElementId(1789660)),&nbsp;DB.Mechanical.Duct)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;curve&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Curve&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(ductOrig.Location,&nbsp;DB.LocationCurve).Curve
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;pt0&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;curve.GetEndPoint(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;pt1&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;curve.GetEndPoint(1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;vector&nbsp;<span style="color:blue;">As</span>&nbsp;DB.XYZ&nbsp;=&nbsp;pt1.Subtract(pt0).Normalize
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;breakPt&nbsp;=&nbsp;pt0.Add(vector.Multiply(2.0))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newDuctId&nbsp;=&nbsp;DB.Mechanical.MechanicalUtils.BreakCurve(doc,&nbsp;ductOrig.Id,&nbsp;breakPt)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;duct2&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(doc.GetElement(newDuctId),&nbsp;DB.Mechanical.Duct)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.Regenerate()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;connectorOrig&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Connector&nbsp;=&nbsp;ductOrig.ConnectorManager.Lookup(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;connector1&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Connector&nbsp;=&nbsp;duct2.ConnectorManager.Lookup(1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;familySymbol&nbsp;<span style="color:blue;">As</span>&nbsp;DB.FamilySymbol&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(doc.GetElement(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">New</span>&nbsp;DB.ElementId(755396)),&nbsp;DB.FamilySymbol)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;famInstance&nbsp;<span style="color:blue;">As</span>&nbsp;DB.FamilyInstance&nbsp;=&nbsp;doc.Create.NewFamilyInstance(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;breakPt,&nbsp;familySymbol,&nbsp;DB.Structure.StructuralType.NonStructural)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;fitting&nbsp;<span style="color:blue;">As</span>&nbsp;DB.MEPModel&nbsp;=&nbsp;famInstance.MEPModel
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;fittingConnector0&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Connector&nbsp;=&nbsp;fitting.ConnectorManager.Lookup(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;fittingConnector1&nbsp;<span style="color:blue;">As</span>&nbsp;DB.Connector&nbsp;=&nbsp;fitting.ConnectorManager.Lookup(1)
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;connectorOrig.ConnectTo(fittingConnector1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;connector1.ConnectTo(fittingConnector0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Commit()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">&#39;&nbsp;change&nbsp;our&nbsp;result&nbsp;to&nbsp;successful</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;UI.Result.Succeeded
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Using</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Execute
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Class</span>
</pre>

**Response:** It works great.

Yet, I have another issue. This method only works well if we are not modifying the element's orientation. 

I tested this on a duct. I first added a duct and implemented the method just after placing an instance of that duct and it worked great.

But, If I rotate the element to its opposite direction and then implementing that methods throws multiple errors. 

Do you have any idea how to resolve these?

Here are the instructions to reproduce the issue:

<center>
<img src="img/split_duct_step_1_AddedDuct.jpg" alt="Added duct" title="Added duct" width="600"/> <!-- 1264 -->
<br/>
<img src="img/split_duct_step_2_RotateDuct.jpg" alt="Rotated duct" title="Rotated duct" width="600"/> <!-- 1264 -->
<br/>
<img src="img/split_duct_step_3_SplitError.jpg" alt="Split error" title="Split error" width="600"/> <!-- 1264 -->
</center>

**Answer:** The family instance needs rotating in the same orientation as the duct curve.

I'm sure there are plenty of examples of how to rotate family instances on this forum. Report back with how you get on!

**Response:** I searched but haven't found much on how to adjust (rotate) the duct fitting as per the element direction and shape.

I tried the rotate method, but that just rotates the new instance (duct fitting) to a 180-degree angle.
So, this only works for a 180-degree rotated element.

<center>
<img src="img/split_duct_issue.jpg" alt="Split issue" title="Split issue" width="600"/> <!-- 1264 -->
</center>

**Answer:** You can find an example showing how to achieve what you ask by @aksaks in
his [GitHub repository](https://github.com/akseidel/WTA_FireP/blob/feb6cff675a2143fffb55e65dd95eb9a73b9c553/WTA_FireP/PlunkOClass.cs)

**Response:** I checked the link.
It provides lots of methods.

Not sure which one to choose.
I tried implementing a few of them but there are many undefined and unknown classes.

Can you please show me a precise way to get the resolution ?

**Answer:** There is in fact no need for the rotation.

You can place the duct fitting with the right direction.

<pre class="code">
  <span style="color:blue;">Dim</span>&nbsp;famInstance&nbsp;<span style="color:blue;">As</span>&nbsp;DB.FamilyInstance&nbsp;_
  =&nbsp;doc.Create.NewFamilyInstance(
  &nbsp;&nbsp;breakPt,&nbsp;FamilySymbol,&nbsp;vector,&nbsp;null,
  &nbsp;&nbsp;DB.Structure.StructuralType.NonStructural)
</pre>

Of course, it can't hurt to learn how to rotate an element.

**Response:** Thank you so much.
It worked pleasantly

Very many thanks to Matt and Fair59 for their kind support and great patience helping out in such detail and depth!

