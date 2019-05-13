<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

</head>

<!---

- 10027136 [highlight and tag linked elements]
  http://forums.autodesk.com/t5/revit-api/highlight-and-tag-linked-elements/m-p/5294217
  tagging linked elements can be solved using two different approaches, either via RevitLinkInstance + CreateLinkReference or using the ParseFromStableRepresentation method.

- 15175390 [Tagging Linked Elements using Revit API]
  https://forums.autodesk.com/t5/revit-api-forum/tagging-linked-elements-using-revit-api/m-p/8669001

twitter:

Autodesk show reels, spatial element geometry calculator and Add-In Manager update for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/spatialgeo2020

&ndash; 
...

linkedin:

 the #RevitAPI #bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

-->

### Tagging a Linked Element


`CreateLinkReference` was introduced way back in
the [Revit 2014 API](https://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html).

It is mentioned as one of the major enhancements to the Revit API:

####<a name="2"></a> Enhancements to Interactions with Links &ndash; Conversion of Geometric References

The API calls:

- [Reference.LinkedElementId](http://www.revitapidocs.com/2020/97813744-6e64-00a7-da5c-b2c6de7919ad.htm) &ndash; The id of the top-level element in the linked document that is referred to by this reference.
- [Reference.CreateLinkReference(RevitLinkInstance)](http://www.revitapidocs.com/2020/919d7d3f-f8c2-eb12-4069-0022c20fa13a.htm) &ndash; Creates a `Reference` from a `Reference` in an RVT Link.
- [Reference.CreateReferenceInLink()](http://www.revitapidocs.com/2020/20a8bee7-2378-c0a6-36f0-07ca42eaedc3.htm) &ndash; Creates a `Reference` in an RVT Link from a `Reference` in the RVT host file.

allow conversion between `Reference` objects which reference only the contents of the link and `Reference` objects which reference the host.

This allows an application, for example, to look at the geometry in the link, find the needed face, and convert the reference to that face into a reference in the host suitable for use to place a face-based instance.

Also, they allow you to obtain a reference in the host (e.g., from a dimension or family) and convert it to a reference in the link, suitable for use in `Element.GetGeometryObjectFromReference`.

This enhancement was often overlooked, and several questions were raised on how to tag an element in a linked file.


####<a name="3"></a> Tagging a Linked Element

Ilia Ivanov used these methods to answer his own question in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [tagging linked elements using Revit API](https://forums.autodesk.com/t5/revit-api-forum/tagging-linked-elements-using-revit-api/m-p/8669001):

**Question:** Hello, Is it possibly to tag a linked element?

And also retrieve the reference of the tagged linked element?

**Answer:** Hello, I have done it:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">RevitLinkInstance</span>&nbsp;link&nbsp;=&nbsp;doc.GetElement(
&nbsp;&nbsp;&nbsp;&nbsp;tag.TaggedElementId.LinkInstanceId&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">RevitLinkInstance</span>;
 
&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;refer&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Reference</span>(
&nbsp;&nbsp;&nbsp;&nbsp;link.GetLinkDocument()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetElement(&nbsp;tag.TaggedElementId.LinkedElementId&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.CreateLinkReference(&nbsp;link&nbsp;);
</pre>

Many thanks to Ilia for sharing this!


####<a name="4"></a> Using the Stable Representation to Tag a Linked Element

In another extensive thread
on [highlighting and tagging linked elements](http://forums.autodesk.com/t5/revit-api/highlight-and-tag-linked-elements/m-p/5294217).
Carolina Machado suggested an alternative approach and less official solution to tag a linked element using the `ParseFromStableRepresentation` method instead:

> Using RevitLookup and a post from your blog, I noticed that the Stable Representation of references in linked instances conform to the following pattern:

> <code>&nbsp; revitLinkInstance.UniqueId
<br/>&nbsp; &nbsp; +<span style="color:#a31515;">&quot;:0:RVTLINK/&quot;</span>&nbsp;+&nbsp;revitLinkType.UniqueId
<br/>&nbsp; &nbsp; +<span style="color:#a31515;">&quot;:&quot;</span>&nbsp;+&nbsp;element.Id.ToString()</code>

> Using this string, it is possible to get the `Reference` through `Reference.ParseFromStableRepresentation` method and then use it to tag the element.

Many thanks to Carolina for sharing this!


####<a name="5"></a> List All Untagged Doors

On a vaguely related topic, here are two suggestions by my colleague Naveen Kumar and
Alexander [@aignatovich](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478) Ignatovich, aka Александр Игнатович,
to retrieve all untagged doors in the model, answering a whole slew of questions on detecting tagged elements:

- [I want to check whether tag is present on door by API](https://forums.autodesk.com/t5/revit-api-forum/i-want-to-check-whether-tag-is-present-on-door-by-api-how-should/td-p/8532032)
- [How to get relation of element with its tag or its label](https://forums.autodesk.com/t5/revit-api-forum/how-to-gets-relation-of-element-with-its-tag-or-its-label/td-p/8602124)
- [How to verify label on element using Revit API](https://forums.autodesk.com/t5/revit-api-forum/how-to-verify-label-on-element-using-revit-api/td-p/8594801)

**Question:** Can we determine the relationship between a tag and its tagged element?

I can retrieve all independent tags of particular category.

E.g., having 6 doors, I can retrieve the 6 door tags.

Suppose one of doors does not have tag.

How can I find the particular door lacking a tag?

In other words, how to find relation between element category and element tag category.

**Answer:** Try using the below code. It will highlight the elements that are not tagged:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;doors
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Doors&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">IndependentTag</span>&gt;&nbsp;tags
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">IndependentTag</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">IndependentTag</span>&gt;();
 
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;untagged_elements&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;<span style="color:blue;">in</span>&nbsp;doors&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!tags.Any(&nbsp;q&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;q.TaggedLocalElementId&nbsp;==&nbsp;e.Id&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;untagged_elements.Add(&nbsp;e.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;uidoc.Selection.SetElementIds(&nbsp;untagged_elements&nbsp;);
&nbsp;&nbsp;uidoc.RefreshActiveView();
</pre>

**Answer 2:** To check whether a specific door is untagged, you can find all `IndependentTag` elements present in the document of `OST_DoorTags` category and the door elements that they are tagging like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;collector&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">IndependentTag</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_DoorTags&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;doorTagsIds
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">HashSet</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;collector.OfType&lt;<span style="color:#2b91af;">IndependentTag</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Select(&nbsp;x&nbsp;=&gt;&nbsp;x.GetTaggedLocalElement()?.Id&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x&nbsp;=&gt;&nbsp;x&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;);
</pre>

Then you can iterate over your doors collection and check if `doorTagsIds` contains the `door.Id`.

<center>
<img src="img/tag_linked_element.jpg" alt="Tag linked element" width="475">
</center>
