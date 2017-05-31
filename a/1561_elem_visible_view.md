<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 11203423 [Get ElementId of all visible entities in a viewport]
  http://forums.autodesk.com/t5/revit-api/get-elementid-of-all-visible-entities-in-a-viewport/m-p/5879077

- http://stackoverflow.com/questions/44012630/determine-is-a-familyinstance-is-visible-in-a-view
I have found that the most reliable way of knowing whether an element is visible in a view is to use a FilteredElementCollector specific to that view. There are so many different ways of controlling the visibility of an element that it would be impractical to try to determine this any other way.
Below is the utility function I use to achieve this. Note this works for any element, and not just for family instances.
...
The category filter is used to eliminate any element not of the desired category before using the slower parameter filter to find the desired element. It is probably possible to speed this up further with clever usage of filters, but I have found that it is plenty fast enough for me in practice.
Colin Stark
Thank you for that, Colin! I added it to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) [CmdViewsShowingElements](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdViewsShowingElements.cs), described in the discussion of [Determining Views Showing an Element](http://thebuildingcoder.typepad.com/blog/2016/12/determining-views-showing-an-element.html).

- CmdViewsShowingElements
  - [Determine Views Displaying Given Element](http://thebuildingcoder.typepad.com/blog/2014/05/views-displaying-given-element-svg-and-nosql.html#6)
  - [Determining Views Showing an Element](http://thebuildingcoder.typepad.com/blog/2016/12/determining-views-showing-an-element.html)

Retrieving Elements Visible in View @AutodeskForge #ForgeDevCon #RevitAPI @AutodeskRevit #bim #dynamobim http://bit.ly/elem_visible_view

Minho re-opened the Revit API discussion forum thread on getting the ElementId of all visible entities in a viewport, providing a good opportunity to mention Colin Stark's answer to the StackOverflow thread on determining whether a FamilyInstance is visible in a View. Question: I am looking for code to get the ElementIds of all entities inside a viewport. A viewport is a region of a big view plan...

-->

### Retrieving Elements Visible in View

Song 'Minho'  [@moshpit](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1967259) Min re-opened
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [getting the `ElementId` of all visible entities in a viewport](http://forums.autodesk.com/t5/revit-api/get-elementid-of-all-visible-entities-in-a-viewport/m-p/5879077),
providing a good opportunity to mention Colin Stark's answer to the StackOverflow thread
on [determining whether a `FamilyInstance` is visible in a View](http://stackoverflow.com/questions/44012630/determine-is-a-familyinstance-is-visible-in-a-view):

#### <a name="2"></a>Question

I am looking for code to get the ElementIds of all entities inside a viewport.

A viewport is a region of a big view plan.

How to retrieve the `ElementId` of all visible entities in a viewport?

#### <a name="3"></a>Answer

Use the `FilteredElementCollector` constructor overload taking a view id argument:

<pre>
  public FilteredElementCollector(
    Document document,
    ElementId viewId )
</pre>

It returns the host document elements visible in this view.

There is a limitation for elements in linked documents, as explained in the thread
on [elements from linked document](http://forums.autodesk.com/t5/revit-api/elements-from-linked-document/m-p/5867049).

In order to include linked elements, you can use
a [custom exporter](http://thebuildingcoder.typepad.com/blog/2013/07/graphics-pipeline-custom-exporter.html).
For more info on that, please refer
to [The Building Coder custom exporter topic group](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.1).

The solution for your specific case is to find the `ViewPlan` associated with the viewport and check the  `BoundingBox` of all elements on the level of this `ViewPlan` to find ones that fit inside the `ViewPlan.CropBox`.

#### <a name="4"></a>Question 2
 
I am learning Revit API and a beginner. I am so desperate to learn what you have mentioned. Could you provide a code of how the method you have mentioned can be fleshed out? 

#### <a name="5"></a>Answer 2
 
Welcome to Revit API programming!
 
I suggest that you first of all take a look at
the [Revit API getting started material](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2) and
work through the step-by-step instructions provided by the DevTV and My First Revit Plugin video tutorials.
 
That will show you what other important material is available that you MUST be aware of, answer this question of yours, and many, many more besides.
 
Once you understand the basics of using filtered element collectors, the answers provided above will become self-explanatory.
 
They already answer your question in full.
 
The Building Coder discussed the related topic of determining all views displaying a specific element in depth, implementing an external command named `CmdViewsShowingElements` to try out some approaches:
 
- [Determine views displaying given element](http://thebuildingcoder.typepad.com/blog/2014/05/views-displaying-given-element-svg-and-nosql.html)
- [Determining views showing an element](http://thebuildingcoder.typepad.com/blog/2016/12/determining-views-showing-an-element.html)
 
A similar question recently also arose in the StackOverflow thread on how
to [determine whether a `FamilyInstance` is visible in a `View`](http://stackoverflow.com/questions/44012630/determine-is-a-familyinstance-is-visible-in-a-view).
 
Colin Stark answered that succinctly, saying:
 
> I have found that the most reliable way of knowing whether an element is visible in a view is to use a FilteredElementCollector specific to that view. There are so many different ways of controlling the visibility of an element that it would be impractical to try to determine this any other way.
 
> Below is the utility function I use to achieve this. Note this works for any element, and not just for family instances.

> The category filter is used to eliminate any element not of the desired category before using the slower parameter filter to find the desired element. It is probably possible to speed this up further with clever usage of filters, but I have found that it is plenty fast enough for me in practice.
 
I added Colin's code to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
module [CmdViewsShowingElement](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdViewsShowingElements.cs).

<pre class="code">
&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Determine&nbsp;whether&nbsp;an&nbsp;element&nbsp;is&nbsp;visible&nbsp;in&nbsp;a&nbsp;view,&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;by&nbsp;Colin&nbsp;Stark,&nbsp;described&nbsp;in</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;http://stackoverflow.com/questions/44012630/determine-is-a-familyinstance-is-visible-in-a-view</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;IsElementVisibleInView(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">View</span>&nbsp;view,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;el&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;view&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentNullException</span>(&nbsp;<span style="color:blue;">nameof</span>(&nbsp;view&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;el&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentNullException</span>(&nbsp;<span style="color:blue;">nameof</span>(&nbsp;el&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Obtain&nbsp;the&nbsp;element&#39;s&nbsp;document.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;el.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;elId&nbsp;=&nbsp;el.Id;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;FilterRule&nbsp;that&nbsp;searches&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;for&nbsp;an&nbsp;element&nbsp;matching&nbsp;the&nbsp;given&nbsp;Id.</span>
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilterRule</span>&nbsp;idRule&nbsp;=&nbsp;<span style="color:#2b91af;">ParameterFilterRuleFactory</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.CreateEqualsRule(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.ID_PARAM&nbsp;),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;elId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;idFilter&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(&nbsp;idRule&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Use&nbsp;an&nbsp;ElementCategoryFilter&nbsp;to&nbsp;speed&nbsp;up&nbsp;the&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;search,&nbsp;as&nbsp;ElementParameterFilter&nbsp;is&nbsp;a&nbsp;slow&nbsp;filter.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Category</span>&nbsp;cat&nbsp;=&nbsp;el.Category;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;catFilter&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementCategoryFilter</span>(&nbsp;cat.Id&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Use&nbsp;the&nbsp;constructor&nbsp;of&nbsp;FilteredElementCollector&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;that&nbsp;accepts&nbsp;a&nbsp;view&nbsp;id&nbsp;as&nbsp;a&nbsp;parameter&nbsp;to&nbsp;only&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;search&nbsp;that&nbsp;view.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Also&nbsp;use&nbsp;the&nbsp;WhereElementIsNotElementType&nbsp;filter&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;to&nbsp;eliminate&nbsp;element&nbsp;types.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;collector&nbsp;=
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc,&nbsp;view.Id&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;catFilter&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;idFilter&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;If&nbsp;the&nbsp;collector&nbsp;contains&nbsp;any&nbsp;items,&nbsp;then&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;we&nbsp;know&nbsp;that&nbsp;the&nbsp;element&nbsp;is&nbsp;visible&nbsp;in&nbsp;the</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;given&nbsp;view.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;collector.Any();
&nbsp;&nbsp;}
}
</pre>

<center>
<img src="img/eyes26.gif" alt="Eyes" width="160">
</center>


