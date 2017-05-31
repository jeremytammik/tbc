<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 13012962 [通り芯の始終点（３D)を編集する方法]

SDK Update and RvtSamples 2018 @AutodeskForge #ForgeDevCon #RevitAPI @AutodeskRevit #bim #dynamobim http://bit.ly/mod_grid_point
SetCurveInView to modify grid endpoint @AutodeskForge #ForgeDevCon #RevitAPI @AutodeskRevit #bim #dynamobim http://bit.ly/mod_grid_point

An updated version of the Revit SDK was published, I set up <code>RvtSamples</code> for Revit 2018, which I use to load The Building Coder samples, and we present a useful employment of the <code>DatumPlane</code> class methods <code>GetCurvesInView</code> and <code>SetCurveInView</code>
&ndash; Revit 2018 SDK Update
&ndash; RvtSamples for Revit 2018
&ndash; How to Modify Grid Curve End Points...

-->

### SDK Update, RvtSamples and Setting Grid Endpoint

An updated version of the Revit SDK was published, I set up `RvtSamples` for Revit 2018, which I use to
load [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples),
and we present a useful employment of the  `DatumPlane` class methods `GetCurvesInView` and `SetCurveInView`:

- [Revit 2018 SDK Update](#2)
- [RvtSamples for Revit 2018](#3)
- [How to Modify Grid Curve End Points](#4)


#### <a name="2"></a>Revit 2018 SDK Update

An update of the Revit SDK has been posted to
the [Revit Developer Centre](http://www.autodesk.com/developrevit):

- [Revit 2018 SDK (Update May 19, 2017)](http://download.autodesk.com/us/revit-sdk/REVIT_2018_SDK_1.msi) (msi - 355088Kb)

It includes the
new [DuplicateGraphics](http://thebuildingcoder.typepad.com/blog/2017/05/revit-2017-and-2018-sdk-samples.html#4.2) sample
that was omitted in the first customer shipment of the Revit 2018 SDK.


#### <a name="3"></a>RvtSamples for Revit 2018

I use the Revit SDK external application `RvtSamples` to load all the SDK samples for testing and debugging purposes.

I first described it in The Building Coder's fifth blog post
on [Managing SDK Samples](http://thebuildingcoder.typepad.com/blog/2008/08/managing-sdk-sa.html).

I soon implemented
the [include file functionality](http://thebuildingcoder.typepad.com/blog/2008/11/loading-the-building-coder-samples.html) to
also use it to load all other sample commands that I regularly use,
including [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).

Last year, Dan Tartaglia raised and I addressed several issues setting
up [RvtSamples for Revit 2017](http://thebuildingcoder.typepad.com/blog/2016/04/rvtsamples-for-revit-2017.html).

This year, I went through a similar process.

For the sake of efficiency, I am here simply posting
the [entire contents of my RvtSamples folder](zip/RvtSamples_2018.zip) as
it stands now, up and running on my system.

The only files that I modified are:

- Application.cs
- RvtSamples.addin
- RvtSamples.txt

<center>
<img src="img/rvtsamples_2018.png" alt="RvtSamples in Revit 2018" width="794">
</center>

Now to return to the topic for today:


#### <a name="4"></a>How to Modify Grid Curve End Points

**Question:** I would like to modify end points of a grid curve for 3D extent, but the `Grid.Curve` property is read-only, so I cannot set a new curve.

Is there any way to edit a grid curve?

**Answer:** Ever since Revit 2016, the `Grid` class provides the methods `GetCurvesInView` and `SetCurveInView.

More precisely, these methods were added to the `DatumPlane` class, which is a base class of `Grid`.

[GetCurvesInView](http://www.revitapidocs.com/2017/2f93dd88-baac-8e61-377e-b937f3faaff6.htm) retrieves a collection of curves representing a `DatumPlane` element in a given view:

<pre class="code">
  <span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">Curve</span>&gt;&nbsp;GetCurvesInView(
  &nbsp;&nbsp;<span style="color:#2b91af;">DatumExtentType</span>&nbsp;extentMode,
  &nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;view
  )
</pre>

They can be set using [SetCurveInView](http://www.revitapidocs.com/2017/eaff0038-34f2-03cf-185b-2872cffb84af.htm):

<pre class="code">
  <span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetCurveInView(
  &nbsp;&nbsp;<span style="color:#2b91af;">DatumExtentType</span>&nbsp;extentMode,
  &nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;view,
  &nbsp;&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;curve
  )
</pre>

The `DatumExtentType` specifies what type of datum extent that is displayed in a particular view.

If you want the actual 3D extents, you need to pass in `DatumExtentType.Model`.

After retrieving the current grid curves, you can determine their end points, create a new line using those points, and set it back to the grid via `Grid.SetCurveInView`.

Here is code snippet demonstrating this:

<pre class="code">
<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;uiapp.ActiveUIDocument;
<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
<span style="color:#2b91af;">Selection</span>&nbsp;sel&nbsp;=&nbsp;uidoc.Selection;
<span style="color:#2b91af;">View</span>&nbsp;view&nbsp;=&nbsp;doc.ActiveView;

<span style="color:#2b91af;">ISelectionFilter</span>&nbsp;f
&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">JtElementsOfClassSelectionFilter</span>&lt;<span style="color:#2b91af;">Grid</span>&gt;();

<span style="color:#2b91af;">Reference</span>&nbsp;elemRef&nbsp;=&nbsp;sel.PickObject(
&nbsp;&nbsp;<span style="color:#2b91af;">ObjectType</span>.Element,&nbsp;f,&nbsp;<span style="color:#a31515;">&quot;Pick&nbsp;a&nbsp;grid&quot;</span>&nbsp;);

<span style="color:#2b91af;">Grid</span>&nbsp;grid&nbsp;=&nbsp;doc.GetElement(&nbsp;elemRef&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Grid</span>;

<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">Curve</span>&gt;&nbsp;gridCurves&nbsp;=&nbsp;grid.GetCurvesInView(&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">DatumExtentType</span>.Model,&nbsp;view&nbsp;);

<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
{
&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Modify&nbsp;Grid&nbsp;Endpoints&quot;</span>&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;c&nbsp;<span style="color:blue;">in</span>&nbsp;gridCurves&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;start&nbsp;=&nbsp;c.GetEndPoint(&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;end&nbsp;=&nbsp;c.GetEndPoint(&nbsp;1&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;newStart&nbsp;=&nbsp;start&nbsp;+&nbsp;10&nbsp;*&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;newEnd&nbsp;=&nbsp;end&nbsp;-&nbsp;10&nbsp;*&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;newLine&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;newStart,&nbsp;newEnd&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;grid.SetCurveInView(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DatumExtentType</span>.Model,&nbsp;view,&nbsp;newLine&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;tx.Commit();
}
</pre>

Many thanks to Ryuji Ogasawara for sharing this!

There is hardly any error checking here, so you need to know exactly what to pick.

It moves the grid endpoints vertically, so you need to select a vertically oriented grid for it to work.

I added Ryuji's sample as a new external command
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2018.0.133.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.133.0) in the
module [CmdSetGridEndpoint.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdSetGridEndpoint.cs).

Here are the isolated grids in *rac_basic_sample.rvt*:

<center>
<img src="img/rac_basic_sample_project_grids.png" alt="Grids in rac_basic_sample.rvt" width="329">
</center>

Launching `CmdSetGridEndpoint` and picking 3, 4, 5 and 6 generates this:

<center>
<img src="img/rac_basic_sample_project_grids_mod.png" alt="Modified grid endpoints" width="332">
</center>
