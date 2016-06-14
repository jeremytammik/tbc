<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Filtering for View Specific Elements #revitAPI #3dwebcoder @AutodeskForge #adsk #aec #bim #socket.io

While preparing for the Forge DevCon in SF and the Athens Forge meetup and web server workshop at The Cube Athens, I also happened to hear about the solution to the question raised by Chema in the Revit API discussion forum thread on deleting an area in a drafting view &ndash; I need to delete some elements (detail items) in a given area of my drafting view...

-->

### Filtering for View Specific Elements

While preparing for 
the [Forge DevCon](http://forge.autodesk.com/conference) in SF and 
the [Athens Forge meetup](http://www.meetup.com/de-DE/I-love-3D-Athens/events/230543759) and
[web server workshop](http://www.meetup.com/de-DE/I-love-3D-Athens/events/230544059) 
at [The Cube Athens](http://thecube.gr), 
I also happened to hear about the solution to the question raised by Chema in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [deleting an area in a drafting view](http://forums.autodesk.com/t5/revit-api/delete-an-area-in-a-drafting-view/td-p/6342882):

**Question:** I am working on a tool that creates schematics. I need to delete some elements (detail items) in a given area of my drafting view:

<center>
 <img src="img/filter_detail_item.png" alt="Filter detail item" width="433">
</center>

In the above example I need to remove the lower detail item and keep the other two. I am trying to use the `ElementOwnerViewFilter` combined with the `BoundingBoxIntersectsFilter` as shown below.

<pre class="code">
<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;CleardBArea(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;vId,
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;dBPos,
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;width,
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;height&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;min&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;(&nbsp;height&nbsp;/&nbsp;4&nbsp;)&nbsp;*&nbsp;dBPos,&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;max&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;width,&nbsp;(&nbsp;height&nbsp;/&nbsp;4&nbsp;)&nbsp;*&nbsp;(&nbsp;dBPos&nbsp;+&nbsp;1&nbsp;),&nbsp;0&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Outline</span>&nbsp;outline&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;min,&nbsp;max&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxIntersectsFilter</span>&nbsp;bbF
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxIntersectsFilter</span>(&nbsp;outline&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">ElementOwnerViewFilter</span>&nbsp;eOVF
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementOwnerViewFilter</span>(&nbsp;vId&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;vColl
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;eOVF&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;bbF&nbsp;);
 
&nbsp;&nbsp;ClearElements(&nbsp;vColl&nbsp;);
}
</pre>

It works fine if I try to delete model lines, but unfortunately, when I try to delete detail items, the filter ignores it.

Could you please recommend any other way to develop it?

I attached a zip file [filter_detail_item.zip](zip/filter_detail_item.zip) with the minimal information to reproduce my problem: a Revit 2015 project file containing a macro that should remove half of the drafting view. It works with all elements excepts the family instances.


Answer: The `BoundingBoxIntersectsFilter` is intended for use with a model geometry bounding box. 

A detail item only has a view specific geometry bounding box, so it fails to filter them. 

The workaround is to retrieve the view specific bounding box and use `Outline.Intersects` to perform the equivalent check, e.g., like this:

<pre class="code">
  <span style="color:blue;">var</span>&nbsp;b1&nbsp;=&nbsp;detailItem.get_BoundingBox(&nbsp;<span style="color:blue;">this</span>.ActiveView&nbsp;);
  <span style="color:#2b91af;">XYZ</span>&nbsp;min&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>();
  <span style="color:#2b91af;">XYZ</span>&nbsp;max&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;1.969,&nbsp;0.656,&nbsp;0&nbsp;);
  <span style="color:blue;">var</span>&nbsp;outline&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;min,&nbsp;max&nbsp;);
  <span style="color:blue;">var</span>&nbsp;outlineOfDetailItem&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;b1.Min,&nbsp;b1.Max&nbsp;);
  outline.Intersects(&nbsp;outlineOfDetailItem,&nbsp;0.00001&nbsp;);
</pre>

