<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

twitter:

the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Comparing Families Using Part Atoms

Here is a quick hint to answer a support case that just came in today, and a pointer to a drum solo:

- [Comparing families using part atoms](#2)
- [Neil Peart drum solo](#3)

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
<pre style="font-family:Consolas;font-size:13px;color:black;background:white;"><span style="color:blue;">Imports</span>&nbsp;Autodesk.Revit
<span style="color:blue;">Imports</span>&nbsp;Autodesk.Revit.Attributes
&lt;<span style="color:#2b91af;">Transaction</span>(<span style="color:#2b91af;">TransactionMode</span>.Manual)&gt;
&lt;<span style="color:#2b91af;">Journaling</span>(<span style="color:#2b91af;">JournalingMode</span>.NoCommandData)&gt;
<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Class</span>&nbsp;<span style="color:#2b91af;">InternalExportPartAtoms</span>
&nbsp;&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.<span style="color:#2b91af;">IExternalCommand</span>
&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByVal</span>&nbsp;commandData&nbsp;<span style="color:blue;">As</span>&nbsp;UI.<span style="color:#2b91af;">ExternalCommandData</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByRef</span>&nbsp;message&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>,_
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByVal</span>&nbsp;elements&nbsp;<span style="color:blue;">As</span>&nbsp;DB.ElementSet)&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">As</span>&nbsp;UI.Result&nbsp;<span style="color:blue;">Implements</span>&nbsp;UI.IExternalCommand.Execute
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
</pre></pre>

Then do
a [diff](https://en.wikipedia.org/wiki/Diff) on the two outputs,
e.g., [using `diffchecker.com`](https://www.diffchecker.com/Unw6nrB2):

<center>
<img src="img/diff_between_family_part_atoms.jpg" alt="Diff between family part atoms" title="Diff between family part atoms" width="800"/> <!-- 1661 -->
</center>

This won't catch all tampering, but it's a decent tool for comparison.

Many thanks to Matt for this invaluable tip!


#### <a name="3"></a>Neil Peart Drum Solo 

In closing, you may enjoy
this [pretty cool drum solo by Neil Peart live in Frankfurt](https://youtu.be/LWRMOJQDiLU):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/LWRMOJQDiLU" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

[Neil Peart](https://en.wikipedia.org/wiki/Neil_Peart) was
a fellow Canadian &ndash; you may be surprised to hear that I am one too, besides other things &mdash; and passed away last week, on January 7, 2020.
