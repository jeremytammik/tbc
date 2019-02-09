<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

Aborting filtered element collection in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/abortfilter

I just discovered an interesting and not completely obvious aspect of using a filtered element collector in
the Revit API discussion forum thread on aborting a long running filtered element collector.
Question: I have really large models where I use an <code>ElementIntersectsElementFilter</code> that can take a long time to process, and sometimes I want to abort it in a graceful way. Is this possible?

&ndash; 
...

linkedin:

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).

-->

### Aborting Filtered Element Collection

I just discovered an interesting and not completely obvious aspect of using a filtered element collector in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [aborting a long running `ElementIntersectsElementFilter`](https://forums.autodesk.com/t5/revit-api-forum/aborting-long-running-elementintersectselementfilter/m-p/8576368):

**Question:** I have really large models where I use an `ElementIntersectsElementFilter` that can take a long time to process, and sometimes I want to abort it in a graceful way. Is this possible?

I've read about task cancellation tokens, but all the examples I've seen using this approach have been loops where I can check if cancellation has been requested. But the ElementIntersectsElementFilter is out of my control and I can't really see how I can cancel it.

I tried to implement the filter as efficiently as possible.

I thought maybe a bounding box filter would make it quicker.

However, after trying, the `ElementIntersectsElementFilter` seemed to be a bit faster than that.

This is how it currently looks.
I have a loop that I can cancel via a cancellation token.
But I'm still interested in a more efficient filter if possible.
Do you see anything obvious?

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;WallMepClashDetection(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;walls&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;filterCategories&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">BuiltInCategory</span>&gt;
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_DuctCurves,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_PipeCurves
&nbsp;&nbsp;&nbsp;&nbsp;};
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;wall&nbsp;<span style="color:blue;">in</span>&nbsp;walls&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;clashingElements&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementMulticategoryFilter</span>(&nbsp;filterCategories&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementIntersectsElementFilter</span>(&nbsp;wall&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Do&nbsp;stuff...</span>
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

**Answer:** I can hardly imagine that it remains slow if you optimise it as much as possible, however large the model is.

The bounding box filter is a quick filter, whereas the element intersection one is slow,
cf., [quick, slow and LINQ element filtering](http://thebuildingcoder.typepad.com/blog/2015/12/quick-slow-and-linq-element-filtering.html)

This can make a huge difference, especially in large models.

Can you try this for starters?

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;cats&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">BuiltInCategory</span>&gt;
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_DuctCurves,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_PipeCurves
&nbsp;&nbsp;};
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;mepfilter&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementMulticategoryFilter</span>(&nbsp;cats&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;bb&nbsp;=&nbsp;wall.get_BoundingBox(&nbsp;<span style="color:blue;">null</span>&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">Outline</span>&nbsp;o&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;bb.Min,&nbsp;bb.Max&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;bbfilter&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxIntersectsFilter</span>(&nbsp;o&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;clashingElements
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;mepfilter&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;bbfilter&nbsp;);
</pre>

If the element intersection filter is equally fast or faster, that means that all your elements have already been completely loaded when the collector is executed.
I guess you have a lot of memory.

**Response:** Should I use the `BoundingBoxIntersectsFilter` to lower the memory footprint?

**Answer:** If your elements are not already all loaded in memory, the element intersection filter will force them to load.
The bounding box filter will not.

Another question: Do you want to cancel because you found what you were looking for, or because you reached some time limit?

Generally, the evaluation is on demand, using iterators, so as long as you don't try to convert the results to a collection, you can write the code to stop whenever you  want.

**Response:** Oh, that's interesting. I will have to check memory usage when comparing the two cases, just out of curiosity.

When I started out I didn't have any means of seeing progress when running heavy intersection filters. And my main motivation was to be able to cancel if the command felt unresponsive or hung. But I've since refactored to use the loop approach which makes it possible to do that and also show a progress bar. I think this applies to any long running command that is out of the control of the developer.

**Answer:** Yes, please check memory usage.

Also understand the concept of iterators.

You can do the following:

<pre class="code">
  filtered element collector X = ...
  foreach element in X:
    do something with element
    break out of the loop if you wish at any time
</pre>

You can interrupt your processing of the results returned by the collector at any time.

You should NOT convert the collector to a list or any other collection.

That would force it to return all the elements, which would cost time and memory, with a performance hit if there are many of them.

Iterating over them one by one does not, costs no time, and can be interrupted any time you like.

You loop over walls and call the same filter for each.
So, there's an opportunity to stop at each wall before calling it again.
In addition, even within the same iteration, you can break early, any time you like, e.g., if you reach some limit.

**Response:** Thank you for the information.

I use loops now so I will be able to cancel the command as suggested.

Thanks for taking the time to help, I've learned some things &nbsp; =)

<center>
<img src="img/file_is_processing.png" alt="File is processing..." width="117">
</center>
