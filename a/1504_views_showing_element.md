<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- filtering for elements visible in views and quick filter performance
  http://forums.autodesk.com/t5/revit-api/find-views-in-which-an-element-is-visible-by-geometry-or-actual/m-p/6522892

Determining Views Showing an Element #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

The topic of how to determine all views in which a given element is visible has been discussed several times on the past, and a couple of viable solutions have been suggested.
However, for large projects, performance becomes an issue.
This question was raised again by Abba Lustgarten and Erik Eriksson in the Revit API discussion forum thread on finding views in which an element is visible (by geometry or actual visibility), with Erik sharing a <code>View</code> extension method <code>IntersectsBoundingBox</code> that helps alleviate the performance impact...

-->

### Determining Views Showing an Element

The topic of how to determine all views in which a given element is visible has been discussed several times on the past, and a couple of viable solutions have been suggested.

However, for large projects, performance becomes an issue.

This question was raised again by Abba Lustgarten
of [Abba CAD Abba Inc.](http://www.abbaservicesinc.com) and
discussed quite extensively with Erik Eriksson
of [White](http://white.se) in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [finding views in which an element is visible (by geometry or actual visibility)](http://forums.autodesk.com/t5/revit-api-forum/find-views-in-which-an-element-is-visible-by-geometry-or-actual/m-p/6522892),
with Erik sharing a `View` extension method `IntersectsBoundingBox` that helps alleviate the performance impact.

Before getting to that, here is a picture from the early morning stroll from the hotel to the Munich Forge accelerator:

<center>
<img src="img/2016-12-07_devday_munich_312_cropped.jpg" alt="Early morning on the way to the Munich Forge accelerator" width="500"/>
</center>


####<a name="2"></a>Question

Here is what I would like to do:

- Given any particular element (by extension, perhaps a set of elements):
    - Find all views in the project in which the element is visible.

For example, if I select a specific door, then I want to find all the floor plan views that include the door, plus section views that show the door (cut or projection), any elevation views that show the door, etc.

Without having given this too much work yet, it would seem that there are two general issues here, and I am guessing that Revit might deal with them differently:

- Given the geometric location of the element, does it occupy the space that is visible in any given view (considering clipping, etc.)
- Given the visibility parameters, filters, etc. &ndash; is the element shown/hidden due to its category, parameters, etc.

In my situation, what I really would like to find at the moment is: If all visibility of all model elements is on, then what views would show the element.

####<a name="3"></a>Answer

Lucky you, asking the right question.

This one, we can answer, and already have, including a dedicated external command `CmdViewsShowingElements` to prove it to The Building Coder samples:

- [Determine Views Displaying Given Element](http://thebuildingcoder.typepad.com/blog/2014/05/views-displaying-given-element-svg-and-nosql.html#6)
- [Revision help: which views show this object?](http://forums.autodesk.com/t5/Revit-API/Revision-help-which-views-show-this-object/m-p/5029772)
- [The Building Coder samples GitHub repository](https://github.com/jeremytammik/the_building_coder_samples)
    - [`CmdViewsShowingElements`](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdViewsShowingElements.cs)
    

####<a name="4"></a>Response

Thanks for your reference to the sample code and the brief history of this.

I am just going to give your `CmdViewsShowingElements` a try, and will let you know how it works for me.

As I do so, I just have this question:

You mentioned that your initial solution for this involved looping through views and using a filtered element collector (with a view_ID parameter) to check for object visibility.  Colin pointed out the potential slowness of this, and your note implied that he had come up with what you considered to be a 'better way'.

From my first glance, it looks to me like this sample also loops through a series of views and uses a view-specific filtered element collector to check for element visibility in each view.  I don't see your original code to compare, but I am wondering if Colin's code does in fact overcome the efficiency problem that he pointed out, or if he just found a better way to implement the approach.

I am only asking to make sure I am not missing something, and to make sure I don't go too far down the wrong path as I am bringing my Revit API skills up to speed.

####<a name="5"></a>Answer

I think the blog post is pretty clear on that: "Here is the `CmdViewsShowingElements` implementation including Colin's two extension methods..."

What more can I do to clarify?

Please note that the GitHub repo is maintained and contains the most up-to-date code.

####<a name="5"></a>Response

What I wanted to clarify was this:

In the 'original' code, (which I haven't seen), Colin raised the concern
that when checking each view for element visibility, Revit would essentially
have to open the view and this could take a long time, especially if a lot
of views are being checked.

In the posted code, do you think this problem is overcome? Or would the
implementation of the filtered element collector still be 'opening' the views,
with the potential time problem.

In my case, I am likely to be looking at hundreds of views, so this could be
significant.

Practically speaking, as you advise, I am working on the assumption that the
GitHub posted sample is most recently 'recommended' approach. Thanks for
putting it there and helping me find it.

####<a name="6"></a>Answer

Revit will open all the views and it is going to take A LOT of time if you have a lot of views within a complex project.

I've just run into that problem and ended up here.

Revit caches the results for a while so if you ask for the same thing one more time it's going to go a lot faster, but if you change your input, you are going to have to wait again.

I'm seeing times of 11 sec when I'm going through 560 views.

I don't think the filtered element collector is designed with this in mind...

####<a name="7"></a>Response

So far, there isn't an alternative that I can see easily. It seems that the
key for me will probably be to 'pre-filter' the list of views that I want to
check.

For example, by the name of the view, or by the type of view (only Floor
Plans, or Section Views, etc.).

I built an interface that does this and gives me a list of 'eligible' views
and lets me select the ones I want to check for element visibility. This is
mostly for testing the whole mechanism at the moment, since I don't totally
trust the filter mechanism yet.

This is all a bit preliminary. 
I found that if I select something like a Stair, or a wall, it seems to
reliably find it in views. However, I tried selecting some other things,
like a mullion on a curtain wall, etc., and that doesn't seem to register as
visible to the filtered element collector, even though it seems to be
visible in the view. If and when I find a clearer pattern, I will post
something about it, and if I don't, I will post something about it.

As for the performance, I also noticed that 'cache' effect, so that running
the filter repeatedly on the same view is much faster.

I am actually seeing the Revit interface show me that it is regenerating the
view (at the bottom of the screen) when it is big and takes a while.

I have a question for you:

You mentioned 560 views taking 11 seconds. Did that mean 11 seconds to scan
all of them, or 11 seconds per view?

I.e., are you getting through 50 views per second?

I am looking at quite a large model with some fairly big and complex views,
but it is averaging a few views per second on small ones and a few seconds
per view on big ones.

For my purposes, 11 seconds for 560 views would be ok.

Again, thanks for your contributions. I would be eager to hear if you have
something working well for you.

####<a name="8"></a>Answer

Ok, yes. Minimising the views is key, I also prefilter a lot.

Yes, the 11 sec are for all 560 views and that's the total time for prefiltering as well, so I might be getting little less than 10 secs for the views.

However, I might have found another way that looks really promising.

By taking and comparing bounding boxes of views and the target elements (intersections), I was able to get results in under a second for all views.
However, in contrast to the filtered element collector way, this requires some testing and setup to get accurate results.
You need to make sure that the view has cropping enabled, set the correct height of the view bounding box and some more.
Furthermore, I noticed that section views need some special care.

I haven't finished my tests, but like I said, it looks promising.

####<a name="9"></a>Response

Does sound promising. I was planning on using the bounding box idea, but I
am somewhat new at the Revit API, and have not dealt with the bounding box
of views yet. Somehow I suspect there is a level of complexity once you
factor in different coordinate systems (e.g. shared coordinates), view
clipping, floor level heights, etc.

If you have encountered any of these pitfalls, I would love to hear about them.

####<a name="10"></a>Answer

Thank you for the valuable discussion, especially Erik with his in-depth real-world experience.

The note on the huge speed-up using bounding boxes is of fundamental importance.

You absolutely must be aware
of [quick filters](http://thebuildingcoder.typepad.com/blog/2015/12/quick-slow-and-linq-element-filtering.html#6)
versus [slow filters](http://thebuildingcoder.typepad.com/blog/2015/12/quick-slow-and-linq-element-filtering.html#7) and 
the difference between them.

Quick filters can be executed without regenerating the views and without fully loading the nitty-gritty detailed data of a huge model.

The bounding box check is a quick filter.

####<a name="11"></a>Answer

Actually, I'm not using the quick filter. I tried to, but the bounding boxes of the views need altering because the Z values are not correct (for plan views at least). I'm prefiltering a lot with quick filters, but when it comes to the actual bounding box comparison I've expanded the views into memory. You can see this in the code in the bottom.

Anyway, I've perfected my bounding box filter into something that works for me. I tried to get it to work in section views, but that was just too big of an obstacle for me today, the client didn't need that in the end, so I just skipped that part. The problem was figuring out the bounding box since the coordinate system shifts in section views.

I tried to look at the transform of the bounding box, and shift it accordingly, but it didn't work.

Maybe you can shed some light on what is wrong or how to accomplish it, Jeremy?

The performance of the bounding box version is extreme in comparison, using a stopwatch I measured the following times:

- ~22 secs for the traditional way
- 125 ms using the bounding box way

Anyway, here is the extension method I use right now:

<pre class="code">
∫∫<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;View&nbsp;extension&nbsp;predicate&nbsp;method:&nbsp;does&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;this&nbsp;view&nbsp;intersect&nbsp;the&nbsp;given&nbsp;bounding&nbsp;box?</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;IntersectsBoundingBox(&nbsp;
&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">View</span>&nbsp;view,&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;targetBoundingBox&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;view.Document;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;viewBoundingBox&nbsp;=&nbsp;view.CropBox;
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!view.CropBoxActive&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tr&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//If&nbsp;the&nbsp;cropbox&nbsp;is&nbsp;not&nbsp;active&nbsp;we&nbsp;can&#39;t&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//extract&nbsp;the&nbsp;boundingbox&nbsp;(we&nbsp;rollback&nbsp;so&nbsp;we&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//don&#39;t&nbsp;change&nbsp;anything&nbsp;and&nbsp;also&nbsp;increase&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//performance)</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tr.Start(&nbsp;<span style="color:#a31515;">&quot;Temp&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.CropBoxActive&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewBoundingBox&nbsp;=&nbsp;view.CropBox;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tr.RollBack();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:#2b91af;">Outline</span>&nbsp;viewOutline&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;view&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">ViewPlan</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;viewRange&nbsp;=&nbsp;(&nbsp;view&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">ViewPlan</span>&nbsp;).GetViewRange();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//We&nbsp;need&nbsp;to&nbsp;change&nbsp;the&nbsp;boundingbox&nbsp;Z-values&nbsp;because&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//they&nbsp;are&nbsp;not&nbsp;correct&nbsp;(for&nbsp;some&nbsp;reason).</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;bottomXYZ&nbsp;=&nbsp;(&nbsp;doc.GetElement(&nbsp;viewRange
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetLevelId(&nbsp;<span style="color:#2b91af;">PlanViewPlane</span>.BottomClipPlane&nbsp;)&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Level</span>&nbsp;).Elevation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;viewRange.GetOffset(&nbsp;<span style="color:#2b91af;">PlanViewPlane</span>.BottomClipPlane&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;topXYZ&nbsp;=&nbsp;(&nbsp;doc.GetElement(&nbsp;viewRange
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetLevelId(&nbsp;<span style="color:#2b91af;">PlanViewPlane</span>.CutPlane&nbsp;)&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Level</span>&nbsp;).Elevation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;viewRange.GetOffset(&nbsp;<span style="color:#2b91af;">PlanViewPlane</span>.CutPlane&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;viewOutline&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewBoundingBox.Min.X,&nbsp;viewBoundingBox.Min.Y,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bottomXYZ&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;viewBoundingBox.Max.X,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewBoundingBox.Max.Y,&nbsp;topXYZ&nbsp;)&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:green;">//this&nbsp;is&nbsp;where&nbsp;I&nbsp;try&nbsp;to&nbsp;handle&nbsp;section views.&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//But&nbsp;I&nbsp;can&#39;t&nbsp;get&nbsp;it&nbsp;to&nbsp;work!!</span>
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!viewBoundingBox.Transform.BasisY.IsAlmostEqualTo(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY&nbsp;)&nbsp;)&nbsp;
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;viewOutline&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;viewBoundingBox.Min.X,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewBoundingBox.Min.Z,&nbsp;viewBoundingBox.Min.Y&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;viewBoundingBox.Max.X,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewBoundingBox.Max.Z,&nbsp;viewBoundingBox.Max.Y&nbsp;)&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;boundingBoxAsOutline&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;targetBoundingBox.Min,&nbsp;targetBoundingBox.Max&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;boundingBoxAsOutline.Intersects(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewOutline,&nbsp;0&nbsp;);
&nbsp;&nbsp;}
}
</pre>

I hope someone finds it useful and good luck Abba!

####<a name="12"></a>Response

Thank you Erik. I will hopefully soon have a chance to dig into this in
full. Right away, I can see a few major tricks that I am sure would have
taken me quite a while to uncover.

In particular, needing to make the crop box active to get the bounding box,
and thus using a transaction and rolling it back in order to do this.
Question: if the cropping box is NOT active on a particular view, is the
bounding box really what reflects the visibility of an object in it?

My gut told me that levels, transforms, etc. would be necessary to filter
views according to their geometrical extents. This seems to have been
right, and I haven't gotten my hands very wet with those yet. Jeremy, can
you give a brief tutorial (or point to a sample or previous overview) on:

- Bounding Boxes (views/elements)
- Coordinate systems and their relationship to levels
- Transformations of coordinates

It turns out that section views are probably the most important for me, so
it would be great to have a 'universal' view extent calculator method.

It does make sense that filtering by the geometrical extents would be much
faster, even with slow filters, since we are considering the extents of only
a few objects (view, cropping box, element?), compared with many thousands
of elements that must be processed in some way when a view is actually
opened, as seems to happen when the filtered element collector goes at it.
However, I think it will be critical to make it work reliably on all
appropriate types of views.

Thanks especially for the snippet. If and when I get this working, I will
post whatever extent-getter I come up with.

####<a name="13"></a>Answer

I'm glad you found some useful bits.

I don't think that Revit is using the bounding box to calculate of the element is visible in the view, it's far too slow for that (select element by id and press show, for instance...).
It's just another way of doing it, maybe they don't do it because they too know they can't rely on the bounding box extents and the fact that it must be turned on or maybe we'll see a much faster way of showing elements in the UI in the coming version of Revit =P

I'm sure there is a way of using this method with section views and from what I've seen, Jeremy is a whole lot better at this type of math than I am =)

By looking at the bounding box of the section view, it's pretty clear that the Z values are messed up here to (or have some other meaning, that I can't comprehend).

God, while writing this I started investigating a little bit more.

I wrote a Room drawing generator last year and that had some heavy lifting when it came section views and figuring out and changing their cropboxes.

This is a theory: but if you skip the original cropbox and use the new `GetCropRegionShape` using the `CropManager`. You get the crop in lines in project coordinates and then you can create a Solid by extrusion (using the far clip offset) using the GeometryCreationUtils and then use the SolidUtils and calculate solid intersections. That would probably be a lot faster.

This has an obvious problem and that is that there could still be elements hiding your elements in the views. That is a problem I have in my bounding box version too.

This is just me thinking out loud, there might be other problems with this workflow, what do you think, Jeremy?

####<a name="14"></a>Conclusion

Sorry that I never answered your questions, guys, and left some open ends dangling.

I hope we can resolve them by and by.

Here are some pretty old and rudimentary introductions to the topics that you asked for, Abba:

- [Element Bounding Box](http://thebuildingcoder.typepad.com/blog/2008/10/element-bounding-box.html)
- [Transform](http://thebuildingcoder.typepad.com/blog/2009/03/transform.html)
- [Transform instance coordinates](http://thebuildingcoder.typepad.com/blog/2009/03/transform-instance-coordinates.html)

I added Erik's `View` extension method `IntersectsBoundingBox` 
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2017.0.131.3](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.131.3) in the
module [CmdViewsShowingElements.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdViewsShowingElements.cs),
in case anyone else would like to play with it.
