<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- add-in guid
  https://autodesk.slack.com/archives/C0SJ4U3PE/p1497273372195629
  [q] if we create a revit addin, should the AddinId be unique across different versions of Revit (2017/2018/2019)? Or should they keep the same AddinId?
  [a] They can have the same GUID. Not only can, but should keep the same add-in id. Some frameworks - updaters, external services and extensible storage, uses the add-in id and serializes it with relevant data.  So you will want the same add-in id in future versions so that Revit doesn't treat the capability as unrecognized.

- https://forums.autodesk.com/t5/revit-api-forum/dimension-on-hatch-pattern-slab/td-p/7063302
  /a/doc/revit/tbc/draft/dim_hatch.txt
  undocumented relationships
  
- lb http://thebuildingcoder.typepad.com/blog/2011/11/undocumented-elementid-relationships.html
  0702_avoid_idling.htm:1
  0849_stair_geometry.htm:2
  0996_attrib_relations.htm:1
  1076_framing_xsec_analyse.htm:1
  1205_image_relationships.htm:2
  1565_dim_hatch.md:1

Why to retain your #RevitAPI Add-In GUID @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/dim_hatch_vodoo
Dimension hatch pattern with voodoo magic #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/dim_hatch_vodoo

Reporting on a very exciting topic from the Revit API discussion forum from the Forge accelerator in Barcelona
&ndash; Barcelona Forge accelerator
&ndash; Retain the Add-In GUID
&ndash; Dimension on hatch pattern...

-->

### Hatch Line Dimensioning Voodoo

Reporting on a very exciting topic from the Revit API discussion forum from the Forge accelerator in Barcelona:

- [Barcelona Forge accelerator](#2)
- [Retain the Add-In GUID](#3)
- [Dimension on hatch pattern](#4)


#### <a name="2"></a>Barcelona Forge Accelerator

I am back in the Barcelona Autodesk office supporting the [Forge Accelerator](http://autodeskcloudaccelerator.com/forge-accelerator).

I was here [last year](http://thebuildingcoder.typepad.com/blog/2016/03/adn-becomes-forge-and-barcelona-accelerator.html#3)
[as well](http://thebuildingcoder.typepad.com/blog/2016/05/forge-accelerator-devcon-and-answer-day.html), where
I started working on
the [roomedit3d project](https://github.com/jeremytammik/roomedit3d), implementing all 
its [Revit-independent functionality](http://thebuildingcoder.typepad.com/blog/2016/05/roomedit3d-console-test-and-rendering-assets.html#2).

Let's look at two Revit API issues before I dig in deeper into Forge:


#### <a name="3"></a>Retain the Add-In GUID

**Question:** If I create a Revit add-in, should the `AddinId` be unique across different versions of Revit (2017/2018/2019)?

Or should I retain the same `AddinId`?

**Answer:** The different versions can have the same GUID.

Not only can, but they definitely should keep the same add-in id.

Some frameworks, e.g., updaters, external services and extensible storage, use the add-in id and serialise it with relevant data.

Therefore, you will want to retain the same add-in id in future versions, so that Revit doesn't treat the capability as unrecognized.


#### <a name="4"></a>Dimension on Hatch Pattern

On to a much more complex question, adding a new entry to our collection
of [undocumented `ElementId` relationships](http://thebuildingcoder.typepad.com/blog/2011/11/undocumented-elementid-relationships.html).

It came up in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on creating a [dimension on hatch pattern slab](https://forums.autodesk.com/t5/revit-api-forum/dimension-on-hatch-pattern-slab/td-p/7063302) and
was answered very creatively indeed by Fair59, who has already contributed numerous answers to other very tricky issues.

Fair59's solution reminds me of
Scott Wilson's [`Reference` stable representation magic voodoo](http://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html):


**Question:** I want to get dimension on hatch pattern slab like this: 

<center>
<img src="img/dim_hatch_01.png" alt="Dimension hatch lines" width="400">
</center>

But I don't know how to retrieve hatch line.


**Answer 1:** First: Use [RevitLookup](https://github.com/jeremytammik/RevitLookup) to
find out what elements you are after:
 
Second: Retrieve their geometry, asking for `ComputeReferences = true`.
 
Third: Create the dimensioning using the references:

- [Dimension walls by iterating faces](http://thebuildingcoder.typepad.com/blog/2011/02/dimension-walls-by-iterating-faces.html)
- [Dimension walls using `FindReferencesByDirection`](http://thebuildingcoder.typepad.com/blog/2011/02/dimension-walls-using-findreferencesbydirection.html)

**Response:** That does not help.

1. I already know it's a floor and can get it.
2. I already retrieve the solid of floor.

The problem is, from the solid I cannot retrieve the geometry of the line pattern in the floor to create dimensions between them.
 
Can you explain more details, please?
 
Here my code in this project so far:
 
<pre class="code">
<span style="color:green;">//&nbsp;Pick&nbsp;floor&nbsp;element</span>
<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;elems&nbsp;=&nbsp;sel.PickElementsByRectangle();
<span style="color:#2b91af;">Floor</span>&nbsp;fl&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
<span style="color:green;">//&nbsp;Loop&nbsp;elems&nbsp;to&nbsp;get&nbsp;Floor</span>
<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;<span style="color:blue;">in</span>&nbsp;elems&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;e&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">Floor</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;fl&nbsp;=&nbsp;(<span style="color:#2b91af;">Floor</span>)&nbsp;e;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;}
}
 
<span style="color:#2b91af;">Solid</span>&nbsp;s&nbsp;=&nbsp;<span style="color:blue;">null</span>;
<span style="color:#2b91af;">Options</span>&nbsp;o&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>();
o.ComputeReferences&nbsp;=&nbsp;<span style="color:blue;">true</span>;
<span style="color:#2b91af;">GeometryElement</span>&nbsp;geoElem&nbsp;=&nbsp;fl.get_Geometry(&nbsp;o&nbsp;);
 
<span style="color:green;">//&nbsp;Loop&nbsp;the&nbsp;geometry&nbsp;of&nbsp;Floor&nbsp;to&nbsp;get&nbsp;solid</span>
<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GeometryObject</span>&nbsp;geoObj&nbsp;<span style="color:blue;">in</span>&nbsp;geoElem&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;s&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;s&nbsp;=&nbsp;(<span style="color:#2b91af;">Solid</span>)&nbsp;geoObj;
}
 
<span style="color:green;">//&nbsp;Stop&nbsp;here,have&nbsp;solid&nbsp;of&nbsp;floor&nbsp;but&nbsp;don&#39;t&nbsp;know&nbsp;</span>
<span style="color:green;">//&nbsp;how&nbsp;to&nbsp;retrieve&nbsp;line&nbsp;in&nbsp;fill&nbsp;pattern&nbsp;of&nbsp;floor&nbsp;:(</span>
</pre>


**Answer 2:** The answer is: you cannot get the geometry objects of the hatch pattern, at all.
 
You even cannot calculate the hatch line positions manually, since there is no information about origin and direction in relation to the face.

Also, the  user may have edited the hatch pattern origin by hand.

Additionally, there are two kinds of fill patterns: 'model' and 'drawing'. One of them depends on view's scale, the other not.

You can analySe the related FillPattern's FillGrids, but you don't have the logical connection to the Face's coordinates.
 
So, no, it cannot be done.


**Answer 3:** There is indeed no direct way to get to hatch lines.

However, we have just about enough information to construct them.
 
I had a hunch, that the reference to the surface and to the hatch lines are related.

A call to `Reference.ConvertToStableRepresentation` returns:

- Surface: 926c1621-1982-4ef6-bcbc-21fe350f0087-001f2d0b:1:SURFACE
- Hatch Line: 926c1621-1982-4ef6-bcbc-21fe350f0087-001f2d0b:1:SURFACE/6

So, given a `StableRepresentation` of a surface , you can construct a `Reference` to a hatch line like this, where `index` is an integer `>1`:

<pre class="code">
<span style="color:#2b91af;">Reference</span>&nbsp;HatchRef
&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">Reference</span>.ParseFromStableRepresentation(&nbsp;doc,
&nbsp;&nbsp;&nbsp;&nbsp;StableRepresentation&nbsp;of&nbsp;<span style="color:#2b91af;">Surface</span>&nbsp;+&nbsp;<span style="color:#a31515;">&quot;/index&quot;</span>&nbsp;);
</pre> 

The indices are distributed over the different hatch lines as follows:

<center>
<img src="img/dim_hatch_line_index.png" alt="Dimension hatch line indices" width="600">
</center>

Take 2 references to a HatchLine and you can create a dimension.

Once you have the dimension, you can determine the direction and position of the 'unbound' lines.
 
Here is my code the creates that dimension:

<pre class="code">
<span style="color:#2b91af;">Floor</span>&nbsp;_floor;
<span style="color:#2b91af;">Reference</span>&nbsp;top&nbsp;=&nbsp;<span style="color:#2b91af;">HostObjectUtils</span>.GetTopFaces(&nbsp;_floor&nbsp;)
&nbsp;&nbsp;.First();
 
<span style="color:#2b91af;">PlanarFace</span>&nbsp;topFace&nbsp;=&nbsp;_floor.GetGeometryObjectFromReference(&nbsp;
&nbsp;&nbsp;top&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">PlanarFace</span>;
 
<span style="color:green;">//&nbsp;Check&nbsp;for&nbsp;model&nbsp;surfacepattern</span>
 
<span style="color:#2b91af;">Material</span>&nbsp;mat&nbsp;=&nbsp;doc.GetElement(&nbsp;
&nbsp;&nbsp;topFace.MaterialElementId&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Material</span>;
 
<span style="color:#2b91af;">FillPatternElement</span>&nbsp;patterntype&nbsp;=&nbsp;doc.GetElement(&nbsp;
&nbsp;&nbsp;mat.SurfacePatternId&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">FillPatternElement</span>;
 
<span style="color:blue;">if</span>(&nbsp;patterntype&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Failed;
 
<span style="color:#2b91af;">FillPattern</span>&nbsp;pattern&nbsp;=&nbsp;patterntype.GetFillPattern();
 
<span style="color:blue;">if</span>(&nbsp;pattern.IsSolidFill&nbsp;
&nbsp;&nbsp;||&nbsp;pattern.Target&nbsp;==&nbsp;<span style="color:#2b91af;">FillPatternTarget</span>.Drafting&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Failed;
 
<span style="color:green;">//&nbsp;Get&nbsp;number&nbsp;of&nbsp;gridLines&nbsp;in&nbsp;pattern&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
 
<span style="color:blue;">int</span>&nbsp;_gridCount&nbsp;=&nbsp;pattern.GridCount;
 
<span style="color:green;">//&nbsp;Construct&nbsp;StableRepresentations&nbsp;and&nbsp;find&nbsp;the&nbsp;</span>
<span style="color:green;">//&nbsp;Reference&nbsp;to&nbsp;HatchLines</span>
 
<span style="color:blue;">string</span>&nbsp;StableRef&nbsp;=&nbsp;top.ConvertToStableRepresentation(&nbsp;
&nbsp;&nbsp;doc&nbsp;);
 
<span style="color:#2b91af;">ReferenceArray</span>&nbsp;_resArr&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ReferenceArray</span>();
<span style="color:blue;">for</span>(&nbsp;<span style="color:blue;">int</span>&nbsp;ip&nbsp;=&nbsp;0;&nbsp;ip&nbsp;&lt;&nbsp;2;&nbsp;ip++&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;index&nbsp;=&nbsp;1&nbsp;+&nbsp;(&nbsp;ip&nbsp;*&nbsp;_gridCount&nbsp;*&nbsp;2&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;StableHatchString&nbsp;=&nbsp;StableRef&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:blue;">string</span>.Format(&nbsp;<span style="color:#a31515;">&quot;/{0}&quot;</span>,&nbsp;index&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;HatchRef&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;HatchRef&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">Reference</span>.ParseFromStableRepresentation(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;StableHatchString&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">catch</span>
&nbsp;&nbsp;{&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;HatchRef&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;<span style="color:blue;">continue</span>;
&nbsp;&nbsp;_resArr.Append(&nbsp;HatchRef&nbsp;);
}
 
<span style="color:green;">//&nbsp;2&nbsp;or&nbsp;more&nbsp;References&nbsp;=&gt;&nbsp;create&nbsp;dimension</span>
 
<span style="color:blue;">if</span>(&nbsp;_resArr.Size&nbsp;&gt;&nbsp;1&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;dimension&nbsp;Hatch&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dimension</span>&nbsp;_dimension&nbsp;=&nbsp;doc.Create.NewDimension(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.ActiveView,&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero,&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;0,&nbsp;0&nbsp;)&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_resArr&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Move&nbsp;dimension&nbsp;a&nbsp;tiny&nbsp;amount&nbsp;to&nbsp;orient&nbsp;the&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;dimension&nbsp;perpendicular&nbsp;to&nbsp;the&nbsp;hatchlines</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;I&nbsp;can&#39;t&nbsp;say&nbsp;why&nbsp;it&nbsp;works,&nbsp;but&nbsp;it&nbsp;does.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementTransformUtils</span>.MoveElement(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_dimension.Id,&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;.1,&nbsp;0,&nbsp;0&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;}
}
</pre>   
 
**Response:** I ended up avoiding the problem by using a `FilledRegion` previous set on the floor pattern. The `FilledRegion` dimension should be equal to one tile dimension.

So, I can locate one tile on Floor, then create a dimension from that to others.  

Many thanks to Fair59 for this extremely creative approach and another gem in the collection of tips and tricks making use of undocumented Revit `ElementId` relationships!

