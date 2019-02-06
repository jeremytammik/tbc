<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

 #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash; 
...

linkedin:

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).

-->

### Cancelling Filtered Element Collection

I just discussed an interesting aspect of using a filtered element collector that I was not previously so clear on, and that you might find interesting too, raised in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [aborting a long running `ElementIntersectsElementFilter`](https://forums.autodesk.com/t5/revit-api-forum/aborting-long-running-elementintersectselementfilter/m-p/8576368):

**Question:**

I have really large models where I do some ElementIntersectsElementFilter that can take a long time to process, and sometimes I want to abort them in a graceful way. Is this possible?

I've read about task cancellation tokens, but all the examples I've seen using this approach have been loops where I can check if cancellation has been requested. But the ElementIntersectsElementFilter is out of my control and I can't really see how I can cancel it.

I tried to implement the filter as efficiently as possible.

I thought maybe a bounding box filter would make it quicker.

However, after trying, the `ElementIntersectsElementFilter` seemed to be a bit faster than that.

But this is how it currently looks. I have a loop that I can cancel via a cancellation token. But I'm still interested in a more efficient filter if possible. Do you see anything obvious?

private List<Element> WallMepClashDetection(Document doc, List<Element> walls)
{
    var filterCategories = new List<BuiltInCategory>
        {
            BuiltInCategory.OST_DuctCurves,
            BuiltInCategory.OST_PipeCurves
        };

    foreach (var wall in walls)
    {
        var clashingElements = new FilteredElementCollector(doc)
            .WhereElementIsNotElementType()
            .WherePasses(new ElementMulticategoryFilter(filterCategories))
            .WherePasses(new ElementIntersectsElementFilter(wall));

        // Do stuff...
    }
}

**Answer:**

I can hardly imagine that it remains slow if you optimise it as much as possible, however large the model is.

The bounding box filter is a quick filter, whereas the element intersection one is slow.

This can make a huge difference, especially in large models:

http://thebuildingcoder.typepad.com/blog/2015/12/quick-slow-and-linq-element-filtering.html

Can you try this for starters?

  var cats = new List<BuiltInCategory>
  {
    BuiltInCategory.OST_DuctCurves,
    BuiltInCategory.OST_PipeCurves
  };

  var mepfilter = new ElementMulticategoryFilter( cats );

  BoundingBoxXYZ bb = wall.get_BoundingBox( null );
  Outline o = new Outline( bb.Min, bb.Max );

  var bbfilter = new BoundingBoxIntersectsFilter( o );

  var clashingElements 
    = new FilteredElementCollector( doc )
      .WhereElementIsNotElementType()
      .WherePasses( mepfilter )
      .WherePasses( bbfilter );

If the element intersection filter is equally fast or faster, that means that all your elements have already been completely loaded when the collector is executed. I guess you have a lot of memory.

**Response:** Should I use the BoundingBoxIntersectsFilter to lower the memory footprint?

** Answer:** If your elements are not already all loaded in memory, the element intersection filter will force them to load. The bounding box filter will not.

Another question: Do you want to cancel because you found what you were looking for, or because you reached some time limit?

Generally, the evaluation is on demand, using iterators, so as long as you don't try to convert the results to a collection, you can write the code to stop whenever you  want.

Oh, that's interesting. I will have to check memory usage when comparing the two cases, just out of curiosity.

When I started out I didn't have any means of seeing progress when running heavy intersection filters. And my main motivation was to be able to cancel if the command felt unresponsive or hung. But I've since refactored to use the loop approach which makes it possible to do that and also show a progress bar. I think this applies to any long running command that is out of the control of the developer.

Yes, please check memory usage.

Also understand the concept of iterators.

You can do the following:

  filtered element collector X = ...
  foreach element in X:
    do something to X
    break out of the loop if you wish at any time

You can interrupt your processing of the results returned by the collector at any time.

You should NOT convert the collector to a list or any other collection.

That would force it to return all the elements, which would cost time if there are many.

Iterating over them one by one does not, costs no time, and can be interrupted any time you like.

In one of the samples, they loop over walls and call the same filter for each. So there's an opportunity to stop at each wall before calling it again. But even within the same iteration, it should be possible to break early if it's reached some limit.

**Response:** Thank you for the information. I use loops now so I will be able to cancel the command as suggested.

Thanks for taking the time to help, I've learned some things =)

#### <a name="2"></a>

#### <a name="3"></a>

I spent some thought on how to best prepare the existing add-in project for creating the DA4R version.

Create a separate branch in GitHub?
Define compile-time switches?
Implement a multi-target Visual Studio project?

In the end, I decided that the simplest and most obvious approach would probably be best:

- Isolate almost all the add-in functionality into a separate `Exporter` class library
- Reimplement and reduce the add-in to the bare minimum essentials, and call the `Exporter` to do all the work
- Implement a new DA4R-specific DB application, also reduced to the bare minimum essentials

Accordingly, the Visual Studio solution now contains three projects:

- Exporter &ndash; does all the work
- Addin &ndash; the normal Revit desktop add-in
- AppBundle &ndash; the Forge DA4R app

Both the desktop add-in and the Forge DA4R `AppBundle` use the same `Exporter` class library and only contain minimal architectural code.

#### <a name="4"></a> Local Testing versus Live Deployment

In addition to the separation between desktop add-in and DA4R appbundle, the latter can also be switched back and forth for local testing versus real live Forge DA4R deployment, by defining or undefining the constant `FORGE_DA4R_TEST_LOCALLY`
in [AppBundle/App.cs](https://github.com/jeremytammik/IfcSpaceZoneBoundaries/blob/master/AppBundle/App.cs).

Here is a [diff between the two states](https://github.com/jeremytammik/IfcSpaceZoneBoundaries/compare/2019.0.0.21..2019.0.0.22).

Depending on which state we are in, we use either the `ApplicationInitialized` or the `DesignAutomationReadyEvent` to run the application functionality.

Here is the entire external DB application implementation:

<pre class="code">
<span style="color:green;">//#define&nbsp;FORGE_DA4R_TEST_LOCALLY</span>

<span style="color:blue;">#region</span>&nbsp;Namespaces
<span style="color:blue;">using</span>&nbsp;System;
<span style="color:blue;">using</span>&nbsp;System.IO;
<span style="color:blue;">using</span>&nbsp;System.Reflection;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.ApplicationServices;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.DB;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.DB.Events;
<span style="color:blue;">using</span>&nbsp;DesignAutomationFramework;
<span style="color:blue;">using</span>&nbsp;IfcSpaceZoneBoundaries.Exporter;
<span style="color:blue;">#endregion</span>

<span style="color:blue;">namespace</span>&nbsp;IfcSpaceZoneBoundaries.AppBundle
{
&nbsp;&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">App</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalDBApplication</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Export&nbsp;all&nbsp;linked-in&nbsp;IFC&nbsp;document&nbsp;rooms&nbsp;and&nbsp;zones</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;ExportLinkedInIfcDocs(&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;app&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;0&nbsp;==&nbsp;app.Documents.Size&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;path&nbsp;=&nbsp;<span style="color:#2b91af;">JtSettings</span>.Instance
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.IfcRvtInputFilePath;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;app.OpenDocumentFile(&nbsp;path&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;doc&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Could&nbsp;not&nbsp;open&nbsp;document&nbsp;{0}.&quot;</span>,&nbsp;path&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">JtLogger</span>.Log(&nbsp;s&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">InvalidOperationException</span>(&nbsp;s&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">RoomZoneExporter</span>.ExportAll(&nbsp;app&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}

<span style="color:blue;">#if</span>&nbsp;FORGE_DA4R_TEST_LOCALLY
<span style="color:gray;">&nbsp;&nbsp;&nbsp;&nbsp;void&nbsp;OnApplicationInitialized(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;object&nbsp;sender,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ApplicationInitializedEventArgs&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;//&nbsp;`sender`&nbsp;is&nbsp;an&nbsp;Application&nbsp;instance:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Application&nbsp;app&nbsp;=&nbsp;sender&nbsp;as&nbsp;Application;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ExportLinkedInIfcDocs(&nbsp;app&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
</span><span style="color:blue;">#else</span>&nbsp;<span style="color:green;">//&nbsp;if&nbsp;not&nbsp;FORGE_DA4R_TEST_LOCALLY</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnDesignAutomationReadyEvent(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DesignAutomationReadyEventArgs</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;`sender`&nbsp;is&nbsp;an&nbsp;Application&nbsp;instance:</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;app&nbsp;=&nbsp;sender&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Application</span>;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ExportLinkedInIfcDocs(&nbsp;app&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
<span style="color:blue;">#endif</span>&nbsp;<span style="color:green;">//&nbsp;FORGE_DA4R_TEST_LOCALLY</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">ExternalDBApplicationResult</span>&nbsp;OnStartup(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;path&nbsp;=&nbsp;<span style="color:#2b91af;">Assembly</span>.GetExecutingAssembly().Location;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">JtLogger</span>.Init(&nbsp;<span style="color:#2b91af;">Path</span>.ChangeExtension(&nbsp;path,&nbsp;<span style="color:#a31515;">&quot;log&quot;</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">JtSettings</span>.Init(&nbsp;<span style="color:#2b91af;">Path</span>.ChangeExtension(&nbsp;path,&nbsp;<span style="color:#a31515;">&quot;json&quot;</span>&nbsp;)&nbsp;);

<span style="color:blue;">#if</span>&nbsp;FORGE_DA4R_TEST_LOCALLY
<span style="color:gray;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a.ApplicationInitialized&nbsp;+=&nbsp;OnApplicationInitialized;
</span><span style="color:blue;">#else</span>&nbsp;<span style="color:green;">//&nbsp;if&nbsp;not&nbsp;FORGE_DA4R_TEST_LOCALLY</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DesignAutomationBridge</span>.DesignAutomationReadyEvent&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+=&nbsp;OnDesignAutomationReadyEvent;
<span style="color:blue;">#endif</span>&nbsp;<span style="color:green;">//&nbsp;FORGE_DA4R_TEST_LOCALLY</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">ExternalDBApplicationResult</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">ExternalDBApplicationResult</span>&nbsp;OnShutdown(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">JtSettings</span>.Save();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">JtLogger</span>.Done();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">ExternalDBApplicationResult</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

#### <a name="5"></a> User Defined Input Arguments for DA4R

I implemented the `JtSettings` class to demonstrate defining and passing in input parameters to a DA4R app via an input parameter file.

I simplified and minimised its interface to reduce the amount of code to be duplicated in the add-in and appbundle, e.g., by converting it to a self-contained singleton.

The interface to read the user-defined settings in DA4R currently consists of just two lines of code, as you can see above:

- A call to `JtSettings.Init` specifying the path to read from
- A call to the setting itself via the static singleton instance:

<pre class="code">
</pre>


<center>
<img src="img/" alt="" width="100">
</center>
