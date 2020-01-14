<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

twitter:

I will be attending Swissbau Basel on Wednesday, comparing families using part atoms and determining the maximum area rectangle inscribed in a polygon in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/familycompare

Here is a last-minute announcement that I will be attending the Swissbau Basel on Wednesday, a quick hint to answer a support case that just came in today, a forum thread issue, and a pointer to a drum solo:
&ndash; Attending Swissbau Basel on Wednesday
&ndash; Comparing families using part atoms
&ndash; Maximum area rectangle in polygon
&ndash; Neil Peart drum solo...

linkedin:

I will be attending Swissbau Basel on Wednesday!

Comparing families using part atoms and determining the maximum area rectangle inscribed in a polygon in the #RevitAPI 

http://bit.ly/familycompare

Here is a last-minute announcement that I will be attending the Swissbau Basel on Wednesday, a quick hint to answer a support case that just came in today, a forum thread issue, and a pointer to a drum solo:

- Attending Swissbau Basel on Wednesday
- Comparing families using part atoms
- Maximum area rectangle in polygon
- Neil Peart drum solo...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Swissbau Basel and Comparing Family Part Atoms

Here is a last-minute announcement that I will be attending the Swissbau Basel on Wednesday, a quick hint to answer a support case that just came in today, a forum thread issue, and a pointer to a drum solo:

- [Attending Swissbau Basel on Wednesday](#1)
- [Comparing families using part atoms](#2)
- [Maximum area rectangle in polygon](#3)
- [Neil Peart drum solo](#4)

#### <a name="1"></a>Attending Swissbau Basel on Wednesday

I am attending [Swissbau Basel](https://www.swissbau.ch) on Wednesday, January 15.

Please reach out if you would like to meet there.

Thank you, and looking forward to seeing you!

#### <a name="2"></a>Comparing Families using Part Atoms

I recently discussed [comparing symbols and comparison operators](https://thebuildingcoder.typepad.com/blog/2019/12/comparing-symbols-and-comparison-operators.html),
including pointers to previous ponderings
on [defining your own key for comparison purposes](https://thebuildingcoder.typepad.com/blog/2012/03/great-ocean-road-and-creating-your-own-key.html#2)
and [tracking element modification](https://thebuildingcoder.typepad.com/blog/2016/01/tracking-element-modification.html#5.1).

In a [comment on that post](https://thebuildingcoder.typepad.com/blog/2019/12/comparing-symbols-and-comparison-operators.html#comment-4718582177),
Matt Taylor suggested an effective solution to compare family definitions using the Revit API `ExtractPartAtomFromFamilyFile` method:

I would say that this is a good candidate for
the [ExtractPartAtomFromFamilyFile](https://www.revitapidocs.com/2020/1f2c631b-2733-0aa7-051c-42bccb07f05e.htm)
and [ExtractPartAtom methods](https://www.revitapidocs.com/2020/d477cf8f-0dfe-4055-a787-315c84ef5530.htm).

You can call those on your family and compare the results they return.

The article on [Extract Part Atom Revisited](https://thebuildingcoder.typepad.com/blog/2010/09/extract-part-atom-revisited.html) shows
how they can be invoked.

As an example, consider a basic column family with width and depth parameters, both set to 600mm, and a type named '600x600'.

Load that into a project and change the width to 590.

Export the part atoms of each, e.g., like this in VB.NET:

<pre class="code">
<span style="color:blue;">Imports</span>&nbsp;Autodesk.Revit
<span style="color:blue;">Imports</span>&nbsp;Autodesk.Revit.Attributes
&lt;<span style="color:#2b91af;">Transaction</span>(<span style="color:#2b91af;">TransactionMode</span>.Manual)&gt;
&lt;<span style="color:#2b91af;">Journaling</span>(<span style="color:#2b91af;">JournalingMode</span>.NoCommandData)&gt;
<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Class</span>&nbsp;<span style="color:#2b91af;">InternalExportPartAtoms</span>
&nbsp;&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.<span style="color:#2b91af;">IExternalCommand</span>
 
&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByVal</span>&nbsp;commandData&nbsp;<span style="color:blue;">As</span>&nbsp;UI.<span style="color:#2b91af;">ExternalCommandData</span>,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByRef</span>&nbsp;message&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByVal</span>&nbsp;elements&nbsp;<span style="color:blue;">As</span>&nbsp;DB.<span style="color:#2b91af;">ElementSet</span>)&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">As</span>&nbsp;UI.<span style="color:#2b91af;">Result</span>&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.<span style="color:#2b91af;">IExternalCommand</span>.Execute
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;app&nbsp;<span style="color:blue;">As</span>&nbsp;ApplicationServices.<span style="color:#2b91af;">Application</span>&nbsp;=&nbsp;commandData.Application.Application
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;doc&nbsp;<span style="color:blue;">As</span>&nbsp;DB.<span style="color:#2b91af;">Document</span>&nbsp;=&nbsp;commandData.Application.ActiveUIDocument.Document
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;family&nbsp;<span style="color:blue;">As</span>&nbsp;DB.<span style="color:#2b91af;">Family</span>&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(doc.GetElement(<span style="color:blue;">New</span>&nbsp;DB.<span style="color:#2b91af;">ElementId</span>(4568558)),&nbsp;DB.<span style="color:#2b91af;">Family</span>)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;familyName&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>&nbsp;=&nbsp;family.Name
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;logFileFolder&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>&nbsp;=&nbsp;<span style="color:#a31515;">&quot;C:\Users\&lt;login&gt;\Desktop\PartAtoms\&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;app.ExtractPartAtomFromFamilyFile(logFileFolder&nbsp;&amp;&nbsp;familyName&nbsp;&amp;&nbsp;<span style="color:#a31515;">&quot;.rfa&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;logFileFolder&nbsp;&amp;&nbsp;familyName&nbsp;&amp;&nbsp;<span style="color:#a31515;">&quot;-file.xml&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;family.ExtractPartAtom(logFileFolder&nbsp;&amp;&nbsp;familyName&nbsp;&amp;&nbsp;<span style="color:#a31515;">&quot;-family.xml&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;UI.<span style="color:#2b91af;">Result</span>.Succeeded
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Class</span>
</pre>

Then do
a [diff](https://en.wikipedia.org/wiki/Diff) on the two outputs,
e.g., [using `diffchecker.com`](https://www.diffchecker.com/Unw6nrB2):

<center>
<img src="img/diff_between_family_part_atoms.jpg" alt="Diff between family part atoms" title="Diff between family part atoms" width="1200"/> <!-- 1661 -->
</center>

This won't catch all tampering, but it's a decent tool for comparison.

Many thanks to Matt for this invaluable tip!


#### <a name="3"></a>Maximum Area Rectangle in Polygon

Completely unrelated to the Revit API, an interesting question popped up today in 
the [discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [drawing maximum area rectangle inside a polygon](https://forums.autodesk.com/t5/revit-api-forum/wanted-to-draw-maximum-area-rectangle-inside-a-polygon/m-p/9248884)
&ndash; it is such a nice question, I picked it up anyway:

**Question:** We are working on a project related to the construction industry, building an iPad application, where we need to find a MAXIMUM POSSIBLE RECTANGLE INSIDE THE GIVEN POLYGON lines. We totally approached around 100's of ways, but till now we are not getting a right solution. Any suggestions will be really helpful!

<center>
<img src="img/maximum_area_rectangle_in_polygon.png" alt="Maximum area rectangle in polygon" title="Maximum area rectangle in polygon" width="350"/> <!-- 700 -->
</center>

**Answer:** I searched the Internet for [maximum inscribed area rectangle in polygon](https://duckduckgo.com/?q=maximum+inscribed+area+rectangle+in+polygon).

The first hit I found was this same question of yours in a different forum with several very helpful suggestions for solving it,
on [How to find the maximum-area-rectangle inside a convex polygon](https://gis.stackexchange.com/questions/59215/how-to-find-the-maximum-area-rectangle-inside-a-convex-polygon).

Another hit shares a proven working solution for
the [largest rectangle in a polygon &ndash; an approximation algorithm for finding the largest rectangle inside a non-convex polygon](https://d3plus.org/blog/behind-the-scenes/2014/07/08/largest-rect).

The latter solution also includes several references to scientific papers on the topic.


#### <a name="4"></a>Neil Peart Drum Solo 

In closing, you may enjoy
this [pretty cool drum solo by Neil Peart live in Frankfurt](https://youtu.be/LWRMOJQDiLU):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/LWRMOJQDiLU" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

[Neil Peart](https://en.wikipedia.org/wiki/Neil_Peart) was
a fellow Canadian &ndash; you may be surprised to hear that I am one too, besides other things &ndash; and passed away last week, on January 7, 2020.
