<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Problem with Constraining Stirrups to the Cover of the Host Element
  https://forums.autodesk.com/t5/revit-api-forum/problem-with-constraining-stirrups-to-the-cover-of-the-host/m-p/10111388
  https://autodesk.slack.com/archives/C0SR6NAP8/p1612179673038400

- getting started
  Revit add-in style guide
  email [Revit add-in style guide] John Callen

twitter:

Rebar stirrup constraints, boundary elements for the entire building and individual rooms, exterior bounding walls, non-bounding interior walls, floors and ceilings with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://autode.sk/boundaryelements

A lengthy and fruitful conversation on implementing valid rebar stirrup constraints and more advice on determining boundary elements for the entire building and individual rooms
&ndash; Rebar stirrup constraints
&ndash; Exterior bounding walls
&ndash; All walls including non-bounding interior walls
&ndash; Floors and ceilings
&ndash; Revit API and UX style guide
&ndash; Today's collective action problem...

linkedin:

Rebar stirrup constraints, boundary elements for the entire building and individual rooms, exterior bounding walls, non-bounding interior walls, floors and ceilings with the #RevitAPI

http://autode.sk/boundaryelements

A lengthy and fruitful conversation on implementing valid rebar stirrup constraints and more advice on determining boundary elements for the entire building and individual rooms:

- Rebar stirrup constraints
- Exterior bounding walls
- All walls including non-bounding interior walls
- Floors and ceilings
- Revit API and UX style guide
- Today's collective action problem...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### Boundary Elements and Stirrup Constraints

I had a lengthy and fruitful conversation on implementing valid stirrup constraints, and several experienced developers added more advice on determining boundary elements for the entire building and individual rooms:

- [Rebar stirrup constraints](#2)
- [Jason and Matt on bounding elements](#3)
    - [Just exterior bounding walls](#3.1)
    - [All walls including non-bounding interior walls](#3.2)
    - [Floors and ceilings](#3.3)
    - [Matt's approach](#3.4)
- [Revit API and UX style guide](#4)
- [Today's collective action problem](#5)


####<a name="2"></a> Rebar Stirrup Constraints

Summarising a nice conversation leading to a satisfactory conclusion in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [problem with constraining stirrups to the cover of the host element](https://forums.autodesk.com/t5/revit-api-forum/problem-with-constraining-stirrups-to-the-cover-of-the-host/m-p/10111388):

**Question:** I have been working on an add-in to automatically place stirrups inside a host element (Structural Column, in this case).
I use the `Rebar.CreateFromRebarShape` method in order to create the stirrup inside the column.
However, the RebarShape came with its own dimensions and thus didn't fit inside the concrete cover.

Through the Revit user interface, every time the stirrup is placed manually inside a host element in the plan view, Revit can automatically constrain the stirrup edges to be inside the cover.
How to do the same thing through the Revit API?
I have tried getting the RebarConstraintManager of the newly created stirrup, iterating through all of its handles and changing the preferred RebarConstraint to `ToCover` as illustrated in the code snippet shown below:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Rebar</span>&nbsp;stirrup&nbsp;=&nbsp;<span style="color:#2b91af;">Rebar</span>.CreateFromRebarShape(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rebarShape,&nbsp;barType,&nbsp;column,&nbsp;bottomLeftXYZ1,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisX,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Modify&nbsp;the&nbsp;RebarConstraint</span>
&nbsp;&nbsp;<span style="color:#2b91af;">RebarConstraintsManager</span>&nbsp;rebarConstraintsManager&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;stirrup.GetRebarConstraintsManager();
 
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">RebarConstrainedHandle</span>&gt;&nbsp;rebarConstrainedHandles&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;rebarConstraintsManager.GetAllConstrainedHandles();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">RebarConstrainedHandle</span>&nbsp;handle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">in</span>&nbsp;rebarConstrainedHandles&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">RebarConstraint</span>&gt;&nbsp;constraintCandidates&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;rebarConstraintsManager
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetConstraintCandidatesForHandle(&nbsp;handle&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">RebarConstraint</span>&nbsp;toCoverConstraint&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;constraintCandidates.Find(&nbsp;c&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;c.IsToCover()&nbsp;==&nbsp;<span style="color:blue;">true</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">RebarConstraint</span>&nbsp;constraint&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;rebarConstraintsManager
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetCurrentConstraintOnHandle(&nbsp;handle&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!constraint.IsToCover()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rebarConstraintsManager
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.SetPreferredConstraintForHandle(&nbsp;handle,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;toCoverConstraint&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

After running the program, nothing changed at all to the stirrup and it still didn't fit inside the concrete cover of the column host.
By debugging, I discovered that the `GetAllConstrainedHandles` method returns 0 handles, which is why the rest of the program didn't work.
Why did the method return zero handles even though the handles are visible in the user interface (both the dot and triangle handles)?
Are there other methods to snap the stirrup inside the concrete cover?

**Answer 1:** Rebar is quite a complex area of the API due to the various objects involved:

`RebarShapeDrivenAccessor` `ScaleToBox` purportedly uses the same algorithm as when you place shape in section view for example.

From this, you probably have to work out the size of the rectangle by looking at cross section deducting covers, not sure there is a faster method. You probably have to size to get it somewhere near and then constrain (historically I've noticed similar in UI).

There is a related method `RebarShapeDrivenAccessor` `ScaleToBoxFor3D`.
RebarShapeDrivenAccessor comes from Rebar.GetShapeDrivenAccessor and is specific to shape driven rebar.

I'm not sure this algorithm always gets it right (from UI experience with it) and there is an element of rationalisation of free end dimensions that need to be applied afterwards.
Perhaps sometimes it can't be placed at all, and perhaps sometimes when you increase the bar diameter the shape can't be made (after placing a smaller bar diameter size), i.e., due to the larger bending diameter reducing the distance between straights.

**Response:** I tried the `ScaleToBox` method that you suggested as shown in the code snippet below:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Rebar</span>&nbsp;stirrup&nbsp;=&nbsp;<span style="color:#2b91af;">Rebar</span>.CreateFromRebarShape(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;rebarShape,&nbsp;barType,&nbsp;column,&nbsp;bottomLeftXYZ1,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisX,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY&nbsp;);
 
&nbsp;&nbsp;stirrup.GetShapeDrivenAccessor().ScaleToBox(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;bottomLeftXYZ1,&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;horDist,&nbsp;0,&nbsp;0&nbsp;),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;vertDist,&nbsp;0&nbsp;)&nbsp;);
</pre>

It seems to work well, the stirrup indeed got resized according to the rectangle that is specified by the ScaleToBox() method, I will double check whether all the parameters are correct after the scaling.

**Answer 2:** You should use:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">RebarConstrainedHandle</span>&gt;&nbsp;GetAllHandles();
</pre>

That returns all RebarConstrainedHandles of this bar.
All `RebarConstrainedHandle` objects will be returned, regardless of whether there are constraints associated to them.

The `GetAllConstrainedHandles` function returns all handles that are already constrained to external references.

**Response:** I tried the method that the development team suggested and indeed it works!
I can now get the handles in the rebar.
However, after setting a new RebarConstraint to each handle in order to snap them to the concrete cover of the host element, the stirrup didn't change to be inside the cover.

Let me provide more details to my case.
In the beginning, I tried to create a stirrup inside the column by using `CreateFromRebarShape` like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Rebar</span>&nbsp;stirrup&nbsp;=&nbsp;<span style="color:#2b91af;">Rebar</span>.CreateFromRebarShape(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rebarShape,&nbsp;barType,&nbsp;column,&nbsp;bottomLeftXYZ1,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisX,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY&nbsp;);
</pre>

I used the default RebarShape called `T1` which is provided as a template by Revit, and the result of the above code is a stirrup. However, the sizes of the stirrup don't match my current column (each rebar shape should have their own default dimensions) as shown below:

<center>
<img src="img/stirrup_constraint_01.png" alt="Stirrup constraint" title="Stirrup constraint" width="300"/> <!-- 493 -->
</center>

That is why I am trying to fit the stirrup inside my column as what the Revit can do through the user interface as shown below:

<center>
<img src="img/stirrup_constraint_02_rebar.png" alt="Stirrup constraint" title="Stirrup constraint" width="200"/> <!-- 493 -->
</center>

Placing a stirrup through the user interface (the selected rebar shape T1 automatically got constrained inside the cover)

I fixed my C# code to the following based on your suggestion in order to change the RebarConstraints to be inside the cover:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Rebar</span>&nbsp;stirrup&nbsp;=&nbsp;<span style="color:#2b91af;">Rebar</span>.CreateFromRebarShape(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rebarShape,&nbsp;barType,&nbsp;column,&nbsp;bottomLeftXYZ1,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisX,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY&nbsp;);
 
&nbsp;&nbsp;<span style="color:gray;">#region</span>&nbsp;//&nbsp;Modify&nbsp;the&nbsp;RebarConstraint&nbsp;
&nbsp;&nbsp;<span style="color:green;">//&nbsp;(Trying&nbsp;to&nbsp;snap&nbsp;the&nbsp;stirrup&nbsp;to&nbsp;the&nbsp;rebar)</span>
&nbsp;&nbsp;<span style="color:#2b91af;">RebarConstraintsManager</span>&nbsp;rebarConstraintsManager&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;stirrup.GetRebarConstraintsManager();
 
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">RebarConstrainedHandle</span>&gt;&nbsp;rebarConstrainedHandles&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;rebarConstraintsManager.GetAllHandles();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">RebarConstrainedHandle</span>&nbsp;handle&nbsp;<span style="color:blue;">in</span>&nbsp;rebarConstrainedHandles&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">RebarConstraint</span>&gt;&nbsp;constraintCandidates&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;rebarConstraintsManager
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetConstraintCandidatesForHandle(&nbsp;handle&nbsp;).ToList();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">RebarConstraint</span>&nbsp;toCoverConstraint&nbsp;=&nbsp;constraintCandidates.Find(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;c&nbsp;=&gt;&nbsp;c.IsToCover()&nbsp;==&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">RebarConstraint</span>&nbsp;constraint&nbsp;=&nbsp;rebarConstraintsManager
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetCurrentConstraintOnHandle(&nbsp;handle&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;constraint&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;||&nbsp;!constraint.IsToCover()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rebarConstraintsManager.SetPreferredConstraintForHandle(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;handle,&nbsp;toCoverConstraint&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

Indeed, the RebarConstraint did change to be constrained to the cover as shown in the following images:

Before implementing the code (the handles are constrained to the Host face as shown by the orange line at the host face):

<center>
<img src="img/stirrup_constraint_03.png" alt="Stirrup constraint" title="Stirrup constraint" width="400"/> <!-- 493 -->
<p style="font-size: 80%; font-style:italic">Before implementing the code (the handles are constrained to the host face)</p>
</center>

After implementing the code (the handles are now constrained to the cover as shown by the blue logo under the triangle handles):

<center>
<img src="img/stirrup_constraint_04.png" alt="Stirrup constraint" title="Stirrup constraint" width="400"/> <!-- 493 -->
<p style="font-size: 80%; font-style:italic">After implementing the code snippet (the handles are indeed constrained to the cover as shown by the blue toggle rebar cover constraint logo)</p>
</center>

Even though the stirrup indeed got constrained to the cover, but it didn't automatically resize just like in the user interface. Any suggestion on how I can achieve similar result as through the user interface?

**Answer:** You should set the distance between bar segment and the cover to zero, i.e., `constraint.SetDistanceToTargetCover(0.0)`.

Another thing I observe in the last picture: the highlighted segment is constrained to the bottom cover; however, it should be constrained to the upper one.

In this case, for each segment, there are two constraint candidates that are to cover.
You should choose the one that is closer to the segment.
`constraint.GetDistanceToTargetCover` can be used to obtain the distance between bar segment and the cover candidate.

**Response:** I believe I am pretty close in cracking the case down.
I implemented the suggestions into the following code snippet:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Rebar</span>&nbsp;stirrup&nbsp;=&nbsp;<span style="color:#2b91af;">Rebar</span>.CreateFromRebarShape(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rebarShape,&nbsp;barType,&nbsp;column,&nbsp;bottomLeftXYZ1,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisX,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">RebarConstraintsManager</span>&nbsp;rebarConstraintsManager&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;stirrup.GetRebarConstraintsManager();
 
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">RebarConstrainedHandle</span>&gt;&nbsp;rebarConstrainedHandles
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;rebarConstraintsManager.GetAllHandles();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">RebarConstrainedHandle</span>&nbsp;handle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">in</span>&nbsp;rebarConstrainedHandles&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">RebarConstraint</span>&gt;&nbsp;constraintCandidates&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;rebarConstraintsManager
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetConstraintCandidatesForHandle(&nbsp;handle&nbsp;).ToList();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">RebarConstraint</span>&gt;&nbsp;toCoverConstraints&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;constraintCandidates.FindAll(&nbsp;c&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;c.IsToCover()&nbsp;==&nbsp;<span style="color:blue;">true</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Find&nbsp;the&nbsp;nearest&nbsp;cover&nbsp;constraint&nbsp;to&nbsp;the&nbsp;handle</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Temporarily&nbsp;set&nbsp;the&nbsp;variable&nbsp;to&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;the&nbsp;first&nbsp;RebarConstraint&nbsp;element</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">RebarConstraint</span>&nbsp;nearestToCoverConstraint&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;toCoverConstraints[&nbsp;0&nbsp;];
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Loop&nbsp;through&nbsp;the&nbsp;RebarConstraint&nbsp;list&nbsp;to&nbsp;find&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;the&nbsp;nearest&nbsp;cover&nbsp;constraint</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">RebarConstraint</span>&nbsp;constraint&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">in</span>&nbsp;toCoverConstraints&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;constraint.GetDistanceToTargetCover()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;&nbsp;nearestToCoverConstraint.GetDistanceToTargetCover()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nearestToCoverConstraint&nbsp;=&nbsp;constraint;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Setting&nbsp;distance&nbsp;of&nbsp;the&nbsp;handle&nbsp;to&nbsp;the&nbsp;Host&nbsp;cover</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;nearestToCoverConstraint.SetDistanceToTargetCover(&nbsp;0.0&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Setting&nbsp;the&nbsp;RebarConstraint&nbsp;as&nbsp;the&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;preferred&nbsp;constraint&nbsp;to&nbsp;the&nbsp;handle</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">RebarConstraint</span>&nbsp;currentConstraint&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;rebarConstraintsManager.GetCurrentConstraintOnHandle(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;handle&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;currentConstraint&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;!currentConstraint.IsToCover()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rebarConstraintsManager.SetPreferredConstraintForHandle(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;handle,&nbsp;nearestToCoverConstraint&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">RebarConstraint</span>&nbsp;constraintTest&nbsp;=&nbsp;rebarConstraintsManager
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetPreferredConstraintOnHandle(&nbsp;handle&nbsp;);
&nbsp;&nbsp;}
</pre>

Compared to the previous code snippet, I now store all of the possible `ToCover` constraints inside the toCoverConstraints List, and precisely as you have mentioned, there are 2 possible ToCover constraints detected and I need to find the closest one to the handle.
I then implemented the `GetDistanceToTargetCover` method as you suggested in order to find the constraint that is closest to the handle.
Afterwards, I set the distance to 0 and then I set the modified constraint to the handle.
However, an error occurred (Rebar Shape Failure) inside Revit when running the add-in:

<center>
<img src="img/stirrup_constraint_07.png" alt="Stirrup constraint" title="Stirrup constraint" width="400"/> <!-- 493 -->
</center>

First thing that I did trying to solve this issue was checking the Handle Types of each of the Rebar handles obtained from  `RebarConstraintsManager` `GetAllHandles`.
There are 7 Handles in total, 1 handle has the RebarHandleType of RebarPlane, 4 handles have the RebarHandleType of Edge, the last two Handles are of the type StartOfBar and EndOfBar respectively.
I tried setting the ToCover constraint only to specific handle types (to Edge type only, to StartOfBar and EndOfBar handles only, etc) but the same Rebar Shape Failure still appear.

If I try to model the stirrups manually using the Revit User Interface (using the same Rebar Shape that I used in the Add-in) and then checking each of the Rebar Constraint, I got the following set up in the User Interface:

<center>
<img src="img/stirrup_constraint_08.png" alt="Stirrup constraint" title="Stirrup constraint" width="200"/> <!-- 493 -->
<p style="font-size: 80%; font-style:italic">One of the Edge handle, constrained to cover at zero distance</p>
<br/>
<img src="img/stirrup_constraint_09.png" alt="Stirrup constraint" title="Stirrup constraint" width="200"/> <!-- 493 -->
<p style="font-size: 80%; font-style:italic">StartOfBar handle, constrained to cover at zero distance</p>
<br/>
<img src="img/stirrup_constraint_10.png" alt="Stirrup constraint" title="Stirrup constraint" width="200"/> <!-- 493 -->
<p style="font-size: 80%; font-style:italic">EndOfBar handle, constrained to cover at zero distance</p>
</center>

Looking at these handle constraints of the manually created stirrups in the User Interface, they are all constrained to Cover, and have their distance set at zero.
The suggestions you provided should lead to the correct approach (The code snippet already implemented approaches to find the correct cover and then setting the distance to zero).
However, I still couldn't get the same result as the stirrup created through the User Interface.
Instead, I am getting the Rebar Shape Failure Error that forces me to delete the rebar.
Is there something that I am missing here that I am still unaware of?

**Answer:** It looks like the constraints that were set were not good.
You should debug more to understand what is happening.
For example, the API can set the constraints but without setting the distance zero to cover.
Then, look in Edit Constraints and check if each handle is constrained to the expected cover.
Here, I'm expecting to see that the constraints were not set to the correct cover and should be investigated.

More can be done.
Create the bar without setting any constraints from API: from Revit UI, Edit Constraints, go and set constrains manually for each segment.
Then, with an API command, for each constraint, set the distance to 0.
There should be no error.
I'm expecting this to work without any problems.

To debug it, you need the sample model and the entire code, not just the part with constraints, but also the code that calculates the rebar curves.

**Response:** Once again, thank you so much for taking your time in answering my queries.

You are right!
After debugging in the manner suggested, I found out that the handles are constrained to the wrong side of the Rebar Cover, thus setting the distance to zero will result in an error.
After checking my code again, I figured out that `RebarConstraint` `GetDistanceToTargetCover will return distance with the plus/minus sign.
Thus, in order to find the nearest constraint to the cover, I need to compare the absolute value.

In the end, I can successfully get the correct result!

Thank you.


####<a name="3"></a> Jason and Matt on Bounding Elements

Jason [@mastjaso](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1058186) Masters
very kindly jumped in with some experienced advice
on [exterior walls and room bounding elements](https://thebuildingcoder.typepad.com/blog/2021/03/exterior-walls-and-room-bounding-elements.html) in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on how to [get the walls, ceiling and floor of a room](https://forums.autodesk.com/t5/revit-api-forum/get-the-walls-ceiling-and-floor-of-a-room/m-p/9915923), saying:

I've been down this road before and it depends on what precisely you're trying to do with the walls, and whether you need interior walls etc.

####<a name="3.1"></a> Just Exterior Bounding Walls

If you just need the exterior walls of the room, then the simplest method is to use
the [`GetBoundarySegments` method](https://www.revitapidocs.com/2015/8e0919af-6172-9d16-26d2-268e42f7e936.htm).
Every room, space, and area in Revit inherits from the `SpatialElement` class which has this method, and will return a list of RoomBoundarySegments, each one containing the curve segment, and what element is creating it (wall, room boundary line, etc.).
This will solve that whole wall centroid being outside of the room problem.

It's probably worth noting that this method will not work in precise situations with linked files,
a bug I documented [here](https://forums.autodesk.com/t5/revit-api-forum/select-room-edges/m-p/9086794).
I haven't tested it since then, but I would be very surprised if Revit actually fixed it since, so it's worth watching out for. 

####<a name="3.2"></a> All Walls Including Non-Bounding Interior Walls

Otherwise, if you need walls that are non-bounding and interior to the room, I found that, in general, all of Revit's projection methods are somewhat slow (and algorithms using them often have edge cases that might be missed), but, like others have said, I found both the `BoundingBoxIntersector` and the `IsPointInRoom` methods to be very fast and performant.
To get *all* of the walls associated with a room, including interior ones, I would first use the boundary segments method to get the bounding walls / walls that won't be *inside* the room.
Then, use the bounding box intersector filter to get all the walls in the model that intersect your room (as said, bounding boxes are rectangular and don't rotate, so this will be rough and pick up walls from other rooms).
Set aside the walls that you already know are the boundaries; then, for the rest, check the coordinates of the end point and midpoint of each wall segment to see if any of those points are in the room with the `IsPointInRoom` method.
If one of them is, then that wall cuts through the interior of the room. 

I believe the only situation that this might miss is if you had a room that is bounded by room boundary lines, and a super long non-bounding wall cut entirely through the room without any end points or midpoints landing within the room. 

####<a name="3.3"></a> Floors And Ceilings

Getting the floors and ceiling is somewhat more complicated and I believe will likely necessitate projection if you want to be able to capture every bulk head / potential split level etc.
What I would do is start with your room's bounding box.
Then, generate a grid of points with even spacing inside this box (maybe 0.5 - 1ft apart), halfway between the top and bottom. Test each point with the `IsPointInRoom` method and discard the ones that aren't.
You'll now have an irregular grid of points, all located within the bounds of your room.
For each point, project a ray upwards and one downwards and capture any floors or ceilings that they intersect before leaving your room bounds.
This should reliably capture every single floor and ceiling associated with a room. 
If it is missing any, you can just increase the resolution of your point grid.

####<a name="3.4"></a> Matt's Approach

Matt Kincaid of [Lighting Analysts](https://lightinganalysts.com) adds in
a [comment on LinkedIn](https://www.linkedin.com/feed/update/urn:li:activity:6775405665461551104?commentUrn=urn%3Ali%3Acomment%3A%28activity%3A6775405665461551104%2C6775552577707761664%29):
 
We've grappled with different approaches for finding exterior walls for some time.
Ultimately, settled on using the `IsExterior` API method.
But that relies on the user to set the function parameter properly, which isn’t always easy (e.g., if it’s in a linked model).
Will have to give this approach a try.
 
For getting the Room Bounding elements, we use this algorithm:

1. Buffer the room footprint by offsetting the boundary by 1/16" outward (to include walls) using NetTopologySuite.
2. Create a vertical extrusion using the buffered foot print using the `GeometryCreationUtilities` API.
Also buffered by 1/16" to include the floor and ceiling.
3. Use the `ElementIntersectsSolidFilter` to find all elements intersecting the extrusion.

This fails if the Room can't be represented as a simple extrusion (e.g., multiple/sloped ceilings or floors).
But works well for many cases.
Ultimately, it would be handy if the Revit API exposed a way to just get all the room bounding elements which are constraining the room geometry.
Something for Revit Ideas, perhaps.

Many thanks to Jason and Matt for their good advice and experience!



####<a name="4"></a> Revit API and UX Style Guide

**Question:** I am exploring developing Revit add-ins making use of our proprietary lighting controls expertise.
I was wondering if Autodesk has a style guide for Revit add-ins, or Revit itself, that we could access to ensure consistency of the user experience?
 
**Answer:** Have you seen the Philips Lighting design app built by one of our partners, Xinaps?
Web based... automates lighting selection and design and inserts directly into Revit models.
If not,
a [YouTube search on Philips Xinaps Revit](https://www.youtube.com/results?search_query=Philips+Xinaps+Revit) should
get you there.

Style guide for Revit add-ins...
Sorry, but no we don't.
Best your UX people can do is plagiarize as much of the Revit look and feel as they can, possibly with some subtle additional signature effects, like subtle use of your specific brand colour.

The Revit API supports some UI functionality.
Everything that you create using that official API will fit in with the standard Revit style.
However, it is pretty limited, so many developers end up adding some of their own WPF and other UI on top of that.
If you refrain from that, you are guaranteed style compatibility.

**Response:** From my (novice) vantage, there are a couple of areas that our add-in would need to integrate with:

- Ribbon definition and navigation
- Interaction with application elements: picking elements, locating objects, etc.
- Database read/write at both the project and object levels
- Specific dialogues to support our add-in
 
Can you suggest some reference materials which would help us better understand programming Revit?

**Answer:** Sure:
 
1. [Getting started with the Revit API](https://thebuildingcoder.typepad.com/blog/about-the-author.html#2), including
the [My First Revit Plug-In video tutorial](https://www.autodesk.com/developer-network/platform-technologies/revit)
2. Ribbon definition is discussed in the [Revit API training labs](https://github.com/ADN-DevTech/RevitTrainingMaterial), specifically part 2, Revit UI API
3. Db read-write is included in the getting started material
4. WPF or WinForms or whatever you are already using... anything that works with .NET
In addition, all your questions are amply covered in the Revit API discussion forum

####<a name="5"></a> Today's Collective Action Problem

[Mancur Olson](https://en.wikipedia.org/wiki/Mancur_Olson)
published [The Logic of Collective Action](https://en.wikipedia.org/wiki/The_Logic_of_Collective_Action) in 1965,
analysing situations in which human beings motivated by individual gain behave in a destructive manner for the common good,
the [collective action problem](https://en.wikipedia.org/wiki/Collective_action_problem),
calling it a *tragedy of the commons*.

In 1990, in her 1990 book *Governing the Commons*,
[Elinor Ostrom](https://en.wikipedia.org/wiki/Elinor_Ostrom) named
examples of communities that deal rationally with their limited shared resources.
This may sound simple and banal, but was a revolutionary thought: people can maintain shared goods rationally and sustainably without privatisation or laws imposed from outside.
To discover this, we must look at the local level, e.g., small towns, villages and cooperatives.
For instance, locally managed forests are often better protected than state parks, since many people do not identify with the rules imposed from 'above'.
Based on this, Ostrom recommends a 'polycentric' approach to deal with the climate change challenge, the greatest collective action problem of our time and maybe all times.
We all need to feel our collective responsibility.
We cannot just sit around and wait for a single global solution.
For this insight, in 2009, Ostrom became the first woman to be honoured with a Nobel prize in economy.
