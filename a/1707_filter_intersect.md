<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- [How to use the ElementIntersectsElementFilter from the RevitLinkInstance?](https://forums.autodesk.com/t5/revit-api-forum/how-to-use-the-elementintersectselementfilter-from-the/m-p/8440333)

- [Get all associated Rebars which attach to the Structural Element](https://forums.autodesk.com/t5/revit-api-forum/get-all-associated-rebars-which-attach-to-the-structural-element/m-p/8446328)

Using an intersection filter for linked elements in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/linkedintersectionfilter

Intersecting elements has always been a hot topic, cf. various previous discussions on 3D Booleans, cutting and joining elements;
intersecting with elements in a linked file is even more challenging.
Happily, the Revit API provides tools to support that as well
&ndash; Intersecting linked elements with current project ones
&ndash; Retrieving rebars intersecting a structural element...

-->

### Using an Intersection Filter for Linked Elements

Intersecting elements has always been a hot topic, cf. various previous discussions
on [3D Booleans, cutting and joining elements](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.30);
intersecting with elements in a linked file is even more challenging.

Happily, the Revit API provides tools to support that as well:

- [Intersecting linked elements with current project ones](#2) 
- [Retrieving rebars intersecting a structural element](#3) 

<center>
<img src="img/rebar_intersect_column.png" alt="Rebar intersecting column" width="103">
</center>

#### <a name="2"></a> Intersecting Linked Elements with Current Project Ones

Yongyu [@wlmsingle](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/6363417) Deng raised and answered an interesting question in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to use the `ElementIntersectsElementFilter` from the `RevitLinkInstance`](https://forums.autodesk.com/t5/revit-api-forum/how-to-use-the-elementintersectselementfilter-from-the/m-p/8440333) to
retrieve MEP elements from a linked file and intersect them with structural elements in the current project:

**Question:** I need to get the MEP elements from a linked file and intersect them with some structural elements in the current project.

I succeeded with this using the `ElementIntersectsElementFilter` and the `ElementIntersectsSolidFilter`.

The filters are both works fine if there is no transform (i.e., identity) between the `RevitLinkInstance` and the current project.

However, if the `RevitLinkInstance` is moved or rotated after importing, the calculation result of the filters is incorrect.

Are there any tricks to solve a case like that?

For example, a method for passing a transform to the filter?

If not, please share a good algorithm to get the intersection result between two solid elements.

**Answer:** Retrieve solids from the elements of interest and use
the [`SolidUtils.CreateTransformed` method](https://apidocs.co/apps/revit/2019/22592761-f39c-4f53-d33b-6c21a4fa9d2d.htm) on them.

**Response:** Thanks a lot!

You inspired me to develop a new solution.

If there is a non-identity transform on the `RevitLinkInstance`, the key point to use the `ElementIntersectsSolidFilter` correctly is simply to transform the element in the current project to the linked file project coordinate system:

Here is a slightly cleaned up version of Yongyu Deng's code that I added to
[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2019.0.144.4](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.4):

<pre class="code">
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;Collect&nbsp;the&nbsp;element&nbsp;ids&nbsp;of&nbsp;all&nbsp;elements&nbsp;in&nbsp;the&nbsp;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;linked&nbsp;documents&nbsp;intersecting&nbsp;the&nbsp;given&nbsp;element.</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>e<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">Target&nbsp;element</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>links<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">Linked&nbsp;documents</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>ids<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">Return&nbsp;intersecting&nbsp;element&nbsp;ids</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
  <span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span><span style="color:green;">Number&nbsp;of&nbsp;intersecting&nbsp;elements&nbsp;found</span><span style="color:gray;">&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
  <span style="color:blue;">int</span>&nbsp;GetIntersectingLinkedElementIds(&nbsp;
  &nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e,
  &nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">RevitLinkInstance</span>&gt;&nbsp;links,
  &nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;ids&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;count&nbsp;=&nbsp;ids.Count();
  &nbsp;&nbsp;<span style="color:#2b91af;">Solid</span>&nbsp;solid&nbsp;=&nbsp;GetSolid(&nbsp;e&nbsp;);
   
  &nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">RevitLinkInstance</span>&nbsp;i&nbsp;<span style="color:blue;">in</span>&nbsp;links&nbsp;)
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;GetTransform&nbsp;or&nbsp;GetTotalTransform&nbsp;or&nbsp;what?</span>
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;transform&nbsp;=&nbsp;i.GetTransform();&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!transform.AlmostEqual(&nbsp;<span style="color:#2b91af;">Transform</span>.Identity)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;solid&nbsp;=&nbsp;<span style="color:#2b91af;">SolidUtils</span>.CreateTransformed(&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;solid,&nbsp;transform.Inverse&nbsp;);
  &nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementIntersectsSolidFilter</span>&nbsp;filter&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementIntersectsSolidFilter</span>(&nbsp;solid&nbsp;);
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;intersecting&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;i.GetLinkDocument()&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;filter&nbsp;);
   
  &nbsp;&nbsp;&nbsp;&nbsp;ids.AddRange(&nbsp;intersecting.ToElementIds()&nbsp;);
  &nbsp;&nbsp;}
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;ids.Count&nbsp;-&nbsp;count;
  }
</pre>


#### <a name="3"></a> Retrieving Rebars Intersecting a Structural Element

Another [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
deals with [getting all associated rebars that attach to a structural element](https://forums.autodesk.com/t5/revit-api-forum/get-all-associated-rebars-which-attach-to-the-structural-element/m-p/8446328):

**Question:** How can I get all associated rebars which attach to a structural element such as a column by picking that?

**Answer:** Picking an element is described in
the [Revit API getting started material](https://thebuildingcoder.typepad.com/blog/about-the-author.html#2) and
also demonstrated in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).

For instance, in the latter, you can check out
the [various element selection utility methods](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/Util.cs#L1227-L1365) and
examine how they are used in the sample commands.

Once you have found a usage pattern that you like in some sample command, search for the description of it
in [The Building Coder blog](https://thebuildingcoder.typepad.com).

Once you have picked your structural element, use a filtered element collector to retrieve the intersecting rebar.

Set it up to retrieve rebar elements only, and add a filter for the column solid:

- [Bounding box filter is always axis aligned](https://thebuildingcoder.typepad.com/blog/2018/04/bounding-box-filter-always-axis-aligned.html)
- [Using intersection filter with linked file](https://thebuildingcoder.typepad.com/blog/2018/04/using-intersection-filter-with-linked-file.html)

[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
includes some examples of using a solid intersection filter, e.g., the [`GetInstancesIntersectingElement` method](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCollectorPerformance.cs#L1294-L1430) showing
how to retrieve family instances intersecting a given BIM element:

<pre class="code">
&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Retrieve&nbsp;all&nbsp;family&nbsp;instances&nbsp;intersecting&nbsp;a</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;given&nbsp;BIM&nbsp;element,&nbsp;e.g.&nbsp;all&nbsp;columns&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;intersecting&nbsp;a&nbsp;wall.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">void</span>&nbsp;GetInstancesIntersectingElement(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;e.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Solid</span>&nbsp;solid&nbsp;=&nbsp;e.get_Geometry(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfType&lt;<span style="color:#2b91af;">Solid</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where&lt;<span style="color:#2b91af;">Solid</span>&gt;(&nbsp;s&nbsp;=&gt;&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;s&nbsp;&amp;&amp;&nbsp;!s.Edges.IsEmpty&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;intersectingInstances
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementIntersectsSolidFilter</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;solid&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;n1&nbsp;=&nbsp;intersectingInstances.Count&lt;<span style="color:#2b91af;">Element</span>&gt;();
 
&nbsp;&nbsp;&nbsp;&nbsp;intersectingInstances
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementIntersectsElementFilter</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;n&nbsp;=&nbsp;intersectingInstances.Count&lt;<span style="color:#2b91af;">Element</span>&gt;();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;n.Equals(&nbsp;n1&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;solid&nbsp;intersection&nbsp;to&nbsp;equal&nbsp;element&nbsp;intersection&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;result&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;{0}&nbsp;family&nbsp;instance{1}&nbsp;intersect{2}&nbsp;the&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;selected&nbsp;element&nbsp;{3}{4}&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;n,&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;n&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(&nbsp;1&nbsp;==&nbsp;n&nbsp;?&nbsp;<span style="color:#a31515;">&quot;s&quot;</span>&nbsp;:&nbsp;<span style="color:#a31515;">&quot;&quot;</span>&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.ElementDescription(&nbsp;e&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.DotOrColon(&nbsp;n&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;id_list&nbsp;=&nbsp;0&nbsp;==&nbsp;n
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;<span style="color:blue;">string</span>.Empty
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span style="color:blue;">string</span>.Join(&nbsp;<span style="color:#a31515;">&quot;,&nbsp;&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;intersectingInstances
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Select&lt;<span style="color:#2b91af;">Element</span>,&nbsp;<span style="color:blue;">string</span>&gt;(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x&nbsp;=&gt;&nbsp;x.Id.IntegerValue.ToString()&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.&quot;</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.InfoMsg2(&nbsp;result,&nbsp;id_list&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Retrieve&nbsp;all&nbsp;beam&nbsp;family&nbsp;instances&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;intersecting&nbsp;two&nbsp;columns,&nbsp;cf.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;http://forums.autodesk.com/t5/revit-api/check-to-see-if-beam-exists/m-p/6223562</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>
&nbsp;&nbsp;&nbsp;&nbsp;GetBeamsIntersectingTwoColumns(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;column1,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;column2&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;column1.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;column2.Document.GetHashCode()&nbsp;!=&nbsp;doc.GetHashCode()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentException</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Expected&nbsp;two&nbsp;columns&nbsp;from&nbsp;same&nbsp;document.&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;intersectingStructuralFramingElements
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_StructuralFraming&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementIntersectsElementFilter</span>(&nbsp;column1&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementIntersectsElementFilter</span>(&nbsp;column2&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;n&nbsp;=&nbsp;intersectingStructuralFramingElements.Count&lt;<span style="color:#2b91af;">Element</span>&gt;();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;result&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;{0}&nbsp;structural&nbsp;framing&nbsp;family&nbsp;instance{1}&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;intersect{2}&nbsp;the&nbsp;two&nbsp;beams{3}&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;n,&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;n&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(&nbsp;1&nbsp;==&nbsp;n&nbsp;?&nbsp;<span style="color:#a31515;">&quot;s&quot;</span>&nbsp;:&nbsp;<span style="color:#a31515;">&quot;&quot;</span>&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.DotOrColon(&nbsp;n&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;id_list&nbsp;=&nbsp;0&nbsp;==&nbsp;n
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;<span style="color:blue;">string</span>.Empty
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span style="color:blue;">string</span>.Join(&nbsp;<span style="color:#a31515;">&quot;,&nbsp;&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;intersectingStructuralFramingElements
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Select&lt;<span style="color:#2b91af;">Element</span>,&nbsp;<span style="color:blue;">string</span>&gt;(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x&nbsp;=&gt;&nbsp;x.Id.IntegerValue.ToString()&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.&quot;</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.InfoMsg2(&nbsp;result,&nbsp;id_list&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;intersectingStructuralFramingElements;
&nbsp;&nbsp;}
</pre>
