<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 13559666 [Strange graphics in Revit 2016 from a plug-in working fine in 2015]
  REVIT-123143 [Strange graphics in Revit 2016 from a plug-in working fine in 2015 - case 13559666]
  avoid exorbitant coordinates
  https://en.wiktionary.org/wiki/exorbitant#English
  
Avoid exorbitant coordinates in #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/exorbitantcoord

Slabs created by an add-in are displayed perfectly in Revit 2015.
In Revit 2016, they are not.
What can be the problem?
These slabs are located at an exorbitant distance from the origin.
Relocating them closer to the origin resolves the problem...

--->

### Avoid Exorbitant Coordinates

With help from the Revit API development team, my
colleague [Jim Jia](http://thebuildingcoder.typepad.com/blog/2017/11/cloud-model-predicate-and-set-parameter-regenerates.html#2) resolved an issue involving large coordinates beyond
the [20-mile limit](#2) that is important to be aware of:

**Question:** My program sets the parameters for a precast concrete slab family.

The family was created in Revit 2015 and all runs perfectly. 

The family has been upgraded to Revit 2016 and the plug in still runs fine. 

However, the slabs created in Revit 2016 look completely wrong.

Here is how they appear correctly in Revit 2015:

<center>
<img src="img/exorbitant_2015_slabs_good.jpg" alt="Good slabs in Revit 2015" width="400"/>
</center>

In Revit 2016, the slabs are displayed incorrectly and look like this:

<center>
<img src="img/exorbitant_2016_slabs_bad.jpg" alt="Bad slabs in Revit 2016" width="400"/>
</center>

If you hover the mouse over a slab in Revit 2016, the blue selection outline shows the correct slab geometry:

<center>
<img src="img/exorbitant_2016_hover_slab_correct_outline.jpg" alt="Hovering over slabs in Revit 2016" width="400"/>
</center>

I tried two different Revit 2016 installations with different graphics cards and get the same problem.

I also tried Revit 2018 and get the same problem.

**Answer:** Is the slab possibly more than 20 miles from the origin?

Yes!

The instance locations I am getting from the input file are at:

<pre>
  X = ~355 miles
  Y = ~130 miles
</pre>

For testing, I added this line to the macro:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;origin&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;slabDetail.OX&nbsp;/&nbsp;mmToFeetRatio,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;slabDetail.OY&nbsp;/&nbsp;mmToFeetRatio,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;slabDetail.OZ&nbsp;/&nbsp;mmToFeetRatio&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Instance&nbsp;location&quot;</span>,&nbsp;origin.ToString()&nbsp;);
</pre>

This is where I see feet values of `>1.87` million for `X`.

These coordinates are truly [exorbitant](https://en.wiktionary.org/wiki/exorbitant#English)!

Revit expects geometry to be placed relatively near its origin.

If this is violated, graphical issues such as these can result.

Why did it work in Revit 2015?

No idea, but placing elements at these distances has never, ever, been a good idea.

**Response:** Wow, great!

I moved the family instances near to the origin and they are now created well.

####<a name="2"></a>Why the Limit?

**Question:** David Veld asks [below](http://thebuildingcoder.typepad.com/blog/2017/11/avoid-exorbitant-coordinates.html#comment-3639729627): I've always wondered where this 20 mile limit comes from, from a coding-technical point of view. If Revit is written in native C++ and stores its coordinates in feet, as doubles, the limit should be 1.8 × 10^308 (right?) &ndash; this is waaaay beyond 20 miles. Or does this mean they are not and perhaps stored in another way?  

**Answer:** First of all, read about [demystifying floating point precision](https://blog.demofox.org/2017/11/21/floating-point-precision).
The precision is reduced the higher the number that is stored.

20 miles is 105600 feet. According to the table there, the precision is about 0.008 feet at that range for floats. This is about 0.2 cm, which is pretty close to Revit's built-in precision limit of about 1/16th of an inch that we repeatedly encountered in the past:

- [Think big in Revit](http://thebuildingcoder.typepad.com/blog/2009/07/think-big-in-revit.html)
- [DirectShape minimum size](http://thebuildingcoder.typepad.com/blog/2014/05/directshape-performance-and-minimum-size.html#3)

The Revit help clearly documents
the [maximum distance limit of 20 miles](https://knowledge.autodesk.com/support/revit-products/learn-explore/caas/CloudHelp/cloudhelp/2018/ENU/Revit-Model/files/GUID-3F79BF5A-F051-49F3-951E-D3E86F51BECC-htm.html).

It covers the projection of a plane onto a curved spherical surface, and also the double accuracy issue
that is probably causing the weird slabs.
