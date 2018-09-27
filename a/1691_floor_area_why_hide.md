<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 14660821 [Get the Floor Area above a Room]
  https://forums.autodesk.com/t5/revit-api-forum/get-the-floor-area-above-a-room/m-p/8295372

- very illuminating discussion on:
  mysterious element hiding activity performed by Revit in a newly created 3D view, what is causing it, how to resolve it
  [Made a new 3DView. It's empty. Why?](https://forums.autodesk.com/t5/revit-api-forum/made-a-new-3dview-it-s-empty-why/m-p/8285271)
  the kind of problems a newbie to Revit runs into, in spite of being experienced in other CAD systems:
  > I've coded in CATIA, Rhino, AutoCAD, and Maya for years without any issues, but I've never had such trouble than when I want to do the simplest things in Revit.
  Fair59 explains in loving, detailed, didactic depth... very many thanks indeed for your continued invaluable support!

Floor area above room and mysterious hidden elements in new 3D view in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/floorarea

Today, we focus on two pure programming questions from the Revit API discussion forum
&ndash; Area of an exterior floor above a room
&ndash; Mysterious element hiding activity...

-->

### Floor Area Above Room and Mysterious Hide

Today, let's take a break from the last two
day's [Forge](https://autodesk-forge.github.io)
[Design Automation API](https://forge.autodesk.com/en/docs/design-automation/v2/overview) for Revit explorations
on [swallowing warnings](http://thebuildingcoder.typepad.com/blog/2018/09/swallowing-stairsautomation-warnings.html)
and [auto-running an add-in](http://thebuildingcoder.typepad.com/blog/2018/09/auto-run-an-add-in-for-design-automation.html) and
focus on two pure programming questions from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread instead:

- [Area of an exterior floor above a room](#2) 
- [Mysterious element hiding activity](#3) 


#### <a name="2"></a> Area of an Exterior Floor Above a Room

Sicheng Zhu asked how
to [get the floor area above a room](https://forums.autodesk.com/t5/revit-api-forum/get-the-floor-area-above-a-room/m-p/8295372):

**Question:** I want to get the exterior floor area above a room, assuming that the floors are modelled separately and I already know which floor is an exterior floor:

<center>
<img src="img/findexteriorfloorarea0.png" alt="Find exterior floor area above room" width="400">
</center>

I want a common solution. Sometimes the floor area is the same as the room area. Sometimes its projection is within the room boundaries. Sometimes they have intersections:

<center>
<img src="img/findexteriorfloorarea1.png" alt="Find exterior floor area above room" width="400">
</center>

Perhaps we can use the class `SolidCurveIntersection` to get the intersected curves and discuss in situations?

Perhaps you can figure out some better solutions?

**Answer:** Thank you for your interesting query.

For a completely generic solution, it seems to me that you will require a 2D Boolean operation.

The Revit geometry API does not provide that built-in, but you can
easily [integrate an external 2D Boolean operations library](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.2).

Once you have the 2D Boolean operations in place, you can easily create two overlapping horizontal polygons:

- Retrieving the room boundary curves and creating a contiguous curve loop from them.
- Projecting the floor boundary curves, projecting them onto the room top surface, and creating a contiguous curve loop from them.

Here is an example of [projecting and transforming a polygon in Revit](http://thebuildingcoder.typepad.com/blog/2008/12/polygon-transformation.html).

This assumes your wall segments are straight, and not curved.

If they are curved, I would suggest using the curve tessellation provided by the Revit geometry API to convert them to straight segments.

With two overlapping 2D polygons and a 2D Boolean operation in place, I hope your problem is perfectly solved in a totally 'common' way.

#### <a name="3"></a> Mysterious Element Hiding Activity

I would also like to highlight one more
of Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen's very illuminating answers.

This one explores the mysterious element hiding activity performed by Revit in a newly created 3D view, what is causing it, how to resolve it:

- [Made a new 3DView. It's empty. Why?](https://forums.autodesk.com/t5/revit-api-forum/made-a-new-3dview-it-s-empty-why/m-p/8285271)

It also demonstrates the kind of problems a newbie to Revit commonly runs into, in spite of being experienced in other CAD systems:

> I've coded in CATIA, Rhino, AutoCAD, and Maya for years without any issues, but I've never had such trouble than when I want to do the simplest things in Revit.

Fair59 explains in with care and loving, detailed, didactic depth...

**Question:** I created a new 3D view.

But it doesn't anything.

Why?

Please help.

Note: When I iterate through all the objects found using a `FilteredElementCollector` on that specific 3d View, it returns over 9000 elements with their respective element ids.  That means there is geometry in that view, but for some reason it's not visible in 3D.

I tried various ViewOrientations and nothing appears in my view.

Nothing is working.

This seems really dumb and simple.  Does anyone have a simple portion of Revit API code that makes a 3D view, and has objects in it?  What is the proper way to make a 3D View in these API's?

I've coded in CATIA, Rhino, AutoCAD, and Maya for years without any issues, but I've never had such trouble than when I want to do the simplest things in Revit.

**Answer:** Yes, indeed, it can help a lot if you can manage to forget what you know about programming other CAD systems before diving into the Revit API... [Revit and its API is different](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.41).

First of all, you might want to make a list of the error elements to see the Revit classes to which they belong:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;errorIds;
&nbsp;&nbsp;StringBuilder&nbsp;sb&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;StringBuilder();
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;id&nbsp;<span style="color:blue;">in</span>&nbsp;errorIds&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;sb.AppendLine(&nbsp;<span style="color:blue;">string</span>.Format(&nbsp;<span style="color:#a31515;">&quot;&lt;{0}&gt;&nbsp;{1}&nbsp;&nbsp;[{2}]&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;id,&nbsp;e.Name,&nbsp;e.GetType()&nbsp;)&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;debug&quot;</span>,&nbsp;sb.ToString()&nbsp;);
</pre>

I think you should make your own ViewFamilyType, without ViewTemplate so all elements are Visible on creation.

You can then hide all elements (that can be hidden) except the error elements.

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;errorIds;
&nbsp;&nbsp;<span style="color:#2b91af;">ViewFamilyType</span>&nbsp;viewFamilyType&nbsp;=&nbsp;(&nbsp;<span style="color:blue;">from</span>&nbsp;v&nbsp;<span style="color:blue;">in</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">ViewFamilyType</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">ViewFamilyType</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">where</span>&nbsp;v.ViewFamily&nbsp;==&nbsp;<span style="color:#2b91af;">ViewFamily</span>.ThreeDimensional
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">select</span>&nbsp;v&nbsp;).First();
 
&nbsp;&nbsp;<span style="color:#2b91af;">View3D</span>&nbsp;view;
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">TransactionGroup</span>&nbsp;tg&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TransactionGroup</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;tg.Start(&nbsp;<span style="color:#a31515;">&quot;create&nbsp;Error&nbsp;View&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Create&nbsp;Issues&nbsp;View&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;create&nbsp;new&nbsp;ViewFamilyType&nbsp;without&nbsp;ViewTemplate</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ViewFamilyType</span>&nbsp;my_type&nbsp;=&nbsp;viewFamilyType
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Duplicate(&nbsp;<span style="color:#a31515;">&quot;Error_View&quot;</span>&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">ViewFamilyType</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;my_type.get_Parameter(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.DEFAULT_VIEW_TEMPLATE&nbsp;).Set(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>.InvalidElementId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view&nbsp;=&nbsp;<span style="color:#2b91af;">View3D</span>.CreateIsometric(&nbsp;doc,&nbsp;my_type.Id&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Hide&nbsp;DWG,&nbsp;Analytical&nbsp;Categories,&nbsp;Annotaion</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.AreImportCategoriesHidden&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.AreAnalyticalModelCategoriesHidden&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.AreCoordinationModelHandlesHidden&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.AreAnnotationCategoriesHidden&nbsp;=&nbsp;<span style="color:blue;">true</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Hide&nbsp;RVT&nbsp;Links</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.SetCategoryHidden(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_RvtLinks&nbsp;),&nbsp;<span style="color:blue;">true</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;show&nbsp;Parts</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.get_Parameter(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.VIEW_PARTS_VISIBILITY&nbsp;).Set(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;elemsInView
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc,&nbsp;view.Id&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Excluding(&nbsp;errorIds&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;elemsToHide&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;<span style="color:blue;">in</span>&nbsp;elemsInView&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;e.CanBeHidden(&nbsp;view&nbsp;)&nbsp;)&nbsp;elemsToHide.Add(&nbsp;e.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;hide&nbsp;elements&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.HideElements(&nbsp;elemsToHide.ToList()&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;tg.Assimilate();
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">this</span>.ActiveUIDocument.ActiveView&nbsp;=&nbsp;view;
&nbsp;&nbsp;<span style="color:blue;">this</span>.ActiveUIDocument.ShowElements(&nbsp;errorIds&nbsp;);
</pre>

Last thing to check (if needed) visibility of worksets; turn on all worksets, e.g., using `view.SetWorksetVisibility`.

**Response:** Thank you for your detailed answer.  I like your approach.

I have a few questions.

- It looks like we are duplicating the first view found via the filter criteria, so the file is basically taking an existing view and duplicating it.
Is that better than the `Create` method?
- Then you are getting a parameter and setting it to an ElementId enumeration of invalid element?  Why?  Is ElementId.InvalidElementId not an enumeration?
- What is this line setting the `VIEW_PARTS_VISIBILITY` parameter doing exactly?  Why `2`?

<pre class="code">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;view.get_Parameter(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.VIEW_PARTS_VISIBILITY&nbsp;).Set(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2&nbsp;);
</pre>

All of this is really good.  I'll try it out.  I have seen most of what you are providing, but never employed it in that way.  Much smarter looping and filtering in your suggestion than my original code, but I'm still negotiating how Revit works.

**Answer:** *Regarding 'Why duplicate?'*

It is not duplicating an existing view, but duplicating an existing `ViewFamilyType`.

Because I don't know which elements are on the `errorId` list, I want to make sure that all elements are visible. A view has a `ViewFamilyType`. The `ViewFamilyType` can have a `ViewTemplate` (which regulates visibility / non-visibility of categories)  assigned to it, that will be applied to newly created views. As I want all elements visible, I want to use a `ViewFamilyType` without a `ViewTemplate`. To avoid messing with the model, I duplicate the first `ViewFamilyType` found, and remove the `ViewTemplate` from it.

<pre class="code">
&nbsp;&nbsp;my_type.get_Parameter(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.DEFAULT_VIEW_TEMPLATE&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Set(&nbsp;<span style="color:#2b91af;">ElementId</span>.InvalidElementId&nbsp;);
</pre>

`ElementId.InvalidElementId` is the equivalent to null for an ElementId value, meaning, do not assign a `ViewTemplate`.

Then I use this `ViewFamilyType` to create the 3D view.

*Regarding `BuiltInParameter.VIEW_PARTS_VISIBILITY`*:

Revit has the possibility to [divide (system) families into `Parts`](htttp://help.autodesk.com/view/RVT/2018/ENU/?guid=GUID-DA150C6B-996C-4C70-9E8C-3C536C232851).

A view can show either the original element, the parts, or both. By setting the `BuiltInParameter` to `2`, I set the view to show the original element and the parts.

Once again, very many thanks indeed to Fair59 for your continued invaluable support!
