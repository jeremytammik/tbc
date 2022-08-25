<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Get FamilyInstances within The Room
  https://forums.autodesk.com/t5/revit-api-forum/get-familyinstances-within-the-room/td-p/11364696

- another look at fuzz:
  Unit converted parameter value not matching parsed string value
  https://forums.autodesk.com/t5/revit-api-forum/unit-converted-parameter-value-not-matching-parsed-string-value/m-p/11353053
  Comparing double values in C#
  https://stackoverflow.com/questions/1398753/comparing-double-values-in-c-sharp

twitter:

Retrieving family instances with a given room and comparing real numbers in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/instancesinroom

Common tasks include finding instances with a given room, comparing real numbers, and sharing documentation online
&ndash; Get family instances within room
&ndash; Again, the need for fuzz
&ndash; Avoid PDF for on-screen reading...

linkedin:

Retrieving family instances with a given room and comparing real numbers in the #RevitAPI

https://autode.sk/instancesinroom

Common tasks include finding instances with a given room, comparing real numbers, and sharing documentation online:

- Get family instances within room
- Again, the need for fuzz
- Avoid PDF for on-screen reading...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Instances in Room and Need for Fuzz

Common tasks include finding instances with a given room, comparing real numbers, and sharing documentation online:

- [Get family instances within room](#2)
- [Again, the need for fuzz](#3)
- [Avoid PDF for on-screen reading](#4)

####<a name="2"></a> Get Family Instances Within Room

A common task is
to [get FamilyInstances within a room](https://forums.autodesk.com/t5/revit-api-forum/get-familyinstances-within-the-room/td-p/11364696):

**Question:** I'm facing a small issue in filtering family instances within the room.
A few families do not get filtered; for example, non-solid families are getting excluded.

<center>
<img src="img/instances_in_room.png" alt="Family instances in room" title="Family instances in room" width="600"/> <!-- 1258 x 776 -->
</center>

How to filter intersected 2D families also?

This my code:

<pre class="code">
  <span style="color:green;">//Get&nbsp;Family&nbsp;Instance</span>
  <span style="color:blue;">public</span>&nbsp;List&lt;FamilyInstance&gt;&nbsp;<span style="color:#74531f;">GetFamilyInstance</span>(
    Document&nbsp;<span style="color:#1f377f;">revitDoc</span>,
    Room&nbsp;<span style="color:#1f377f;">room</span>)
  {
  &nbsp;&nbsp;<span style="color:green;">//Get&nbsp;Closed&nbsp;Shell</span>
  &nbsp;&nbsp;GeometryElement&nbsp;<span style="color:#1f377f;">geoEle</span>&nbsp;=&nbsp;room.ClosedShell;
  &nbsp;&nbsp;GeometryObject&nbsp;<span style="color:#1f377f;">geoObject</span>&nbsp;=&nbsp;<span style="color:blue;">null</span>;
  &nbsp;&nbsp;<span style="color:green;">//Get&nbsp;Geometry&nbsp;Object&nbsp;From&nbsp;Geometry&nbsp;Element</span>
  &nbsp;&nbsp;<span style="color:#8f08c4;">foreach</span>&nbsp;(GeometryObject&nbsp;<span style="color:#1f377f;">obj</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;geoEle)
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(obj&nbsp;<span style="color:blue;">is</span>&nbsp;Solid)
  &nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;geoObject&nbsp;=&nbsp;obj;
  &nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;}
   
  &nbsp;&nbsp;ElementIntersectsSolidFilter&nbsp;<span style="color:#1f377f;">elementIntersectsSolidFilter</span>
  &nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;ElementIntersectsSolidFilter(geoObject&nbsp;<span style="color:blue;">as</span>&nbsp;Solid);
   
  &nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;FilteredElementCollector(revitDoc)
  &nbsp;&nbsp;&nbsp;&nbsp;.OfClass(<span style="color:blue;">typeof</span>(FamilyInstance))
  &nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType().
  &nbsp;&nbsp;&nbsp;&nbsp;WherePasses(elementIntersectsSolidFilter).
  &nbsp;&nbsp;&nbsp;&nbsp;Cast&lt;FamilyInstance&gt;().
  &nbsp;&nbsp;&nbsp;&nbsp;ToList();
  }
</pre>

**Answer:** The `ElementIntersectsSolidFilter` requires the filtered elements to have solid geometry and be of a category supported by interference checking.
Your 2D instances do not fulfil this requirement.

You can try using the family instance `Room` property like this:

<pre class="code">
  <span style="color:blue;">bool</span>&nbsp;<span style="color:#74531f;">IsInstanceInRoom</span>(FamilyInstance&nbsp;<span style="color:#1f377f;">instance</span>,&nbsp;Room&nbsp;<span style="color:#1f377f;">room</span>)
  {
  &nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">isInstanceInRoom</span>&nbsp;=&nbsp;(instance.Room&nbsp;!=&nbsp;<span style="color:blue;">null</span>)&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;instance.Room.Id&nbsp;==&nbsp;room.Id;
  &nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;isInstanceInRoom;
  }
   
  <span style="color:blue;">public</span>&nbsp;List&lt;FamilyInstance&gt;&nbsp;<span style="color:#74531f;">GetFamilyInstance</span>(
    Document&nbsp;<span style="color:#1f377f;">revitDoc</span>,
    Room&nbsp;<span style="color:#1f377f;">room</span>)
  {
  &nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">elements</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FilteredElementCollector(revitDoc)
    &nbsp;&nbsp;.OfClass(<span style="color:blue;">typeof</span>(FamilyInstance))
    &nbsp;&nbsp;.WhereElementIsNotElementType()
    &nbsp;&nbsp;.Cast&lt;FamilyInstance&gt;()
    &nbsp;&nbsp;.Where(<span style="color:#1f377f;">i</span>&nbsp;=&gt;&nbsp;IsInstanceInRoom(i,&nbsp;room))
    &nbsp;&nbsp;.ToList();
  &nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;elements;
  }
</pre>

Another approach is to use the `Room.IsPointInRoom` predicate and check the family instance location point or constructing some other point based on geometry location.
You may need to elevate it slightly off the floor to ensure it will be found within vertical limits of room.

**Response:** So, I need to take a center point of the family instance and then use `IsPointInRoom` to ensure that it is within the room limits.
Can you write some simple code for me?

**Answer:** The solution is very kindly given by EatRevitPoopCad
explaining [how to determine if a toilet belongs to a room](https://forums.autodesk.com/t5/revit-api-forum/how-to-determinate-if-a-toilet-belongs-to-a-room/m-p/10305412).

You could also simply search this forum for `IsPointInRoom` to find it yourself.

Many thanks to Sam Berk,
Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
and EatRevitPoopCad for their helpful advice!

####<a name="2.1"></a> Rooms in Linked Models

As Joshua Lumley points out in his [comment below](https://thebuildingcoder.typepad.com/blog/2022/08/instances-in-room-and-need-for-fuzz.html#comment-5959441109),
`IsPointInRoom` works well for rooms in linked models as well; just remember to inverse the total transform of the linkedinstance before doing it.



####<a name="3"></a> Again, the Need for Fuzz

We take yet another look at fuzz, required in order to deal with comparison of real numbers on digital computers.

The topic came up once again in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [unit converted parameter value not matching parsed string value](https://forums.autodesk.com/t5/revit-api-forum/unit-converted-parameter-value-not-matching-parsed-string-value/m-p/11353053).

To skip the jabber and jump right to a solution, please refer to the StackOverflow discussion
on [comparing double values in C#](https://stackoverflow.com/questions/1398753/comparing-double-values-in-c-sharp).

**Question:** Strictly speaking, this is not a Revit API issue I'm facing, but after some research I couldn't find the answer here or anywhere else and it's still a challenge related to Revit.

I'm comparing a family parameter value with values in a spreadsheet so I can update the parameter accordingly if they don't match, but I'm getting weird results when comparing them.
The snippet below shows how I'm extracting the double value of the `WW_Width` parameter and converting it to millimetres.
Then, I compare it to a parsed string with the exact same value as a double, but I get a false result as to whether or not they match.

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">famPars</span>&nbsp;=&nbsp;doc.FamilyManager.Parameters;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">famTypes</span>&nbsp;=&nbsp;doc.FamilyManager.Types;
 
&nbsp;&nbsp;<span style="color:#8f08c4;">foreach</span>&nbsp;(FamilyParameter&nbsp;<span style="color:#1f377f;">param</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;famPars)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">foreach</span>&nbsp;(FamilyType&nbsp;<span style="color:#1f377f;">famtype</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;famTypes)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(param.Definition.Name&nbsp;==&nbsp;<span style="color:#a31515;">&quot;WW_Width&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.ShowBoldMessage(<span style="color:#a31515;">&quot;WW_Width&quot;</span>);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.ShowMessage(<span style="color:#a31515;">$&quot;UnitType:&nbsp;</span>{param.Definition.UnitType}<span style="color:#a31515;">&quot;</span>);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.ShowMessage(<span style="color:#a31515;">$&quot;StorageType:&nbsp;</span>{param.StorageType}<span style="color:#a31515;">\n&quot;</span>);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;<span style="color:#1f377f;">valueInMM</span>&nbsp;=&nbsp;UnitUtils.ConvertFromInternalUnits(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span style="color:blue;">double</span>)&nbsp;famtype.AsDouble(param),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayUnitType.DUT_MILLIMETERS);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;<span style="color:#1f377f;">parsedString</span>&nbsp;=&nbsp;<span style="color:blue;">double</span>.Parse(<span style="color:#a31515;">&quot;2448&quot;</span>);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.ShowMessage(<span style="color:#a31515;">$&quot;Value:&nbsp;</span>{valueInMM}<span style="color:#a31515;">&quot;</span>);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.ShowMessage(<span style="color:#a31515;">$&quot;Parsed&nbsp;string:&nbsp;</span>{parsedString}<span style="color:#a31515;">&quot;</span>);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.ShowMessage(<span style="color:#a31515;">$&quot;Values&nbsp;match:&nbsp;</span>{valueInMM&nbsp;==&nbsp;parsedString}<span style="color:#a31515;">&quot;</span>);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

This is what my console shows as a result; I don't understand why I'm getting a mismatch:

<pre class="code">
  WW_Width
  UnitType: UT_Length
  StorageType: Double
  
  Value: 2448
  Parsed string: 2448
  Values match: False
</pre>

**Answer:** You need to add some fuzz; you
can [search The Building Coder for 'fuzz'](https://www.google.com/search?q=fuzz&as_sitesearch=thebuildingcoder.typepad.com).

**Response:** Thanks for the steer in the right direction.
I wasn't aware of that issue with doubles/floats.
Instead of directly comparing the doubles, I'm subtracting one from the other and checking if the difference is under a certain tolerance (e.g. A - B < 0.001), and that works.

The StackOverflow article
on [comparing double values in C#](https://stackoverflow.com/questions/1398753/comparing-double-values-in-c-sharp) explains
it well in case anyone faces this issue in the future.

####<a name="4"></a> Avoid PDF for On-Screen Reading

I just read a piece of advice that I have not heeded so far, and am unsure whether to heed in the future.

However, I am very glad to be well informed about my bad manners.

The recommendation stands
to [avoid PDF for on-screen reading](https://www.nngroup.com/articles/avoid-pdf-onscreen-reading-original).
It was voiced a long time ago and apparently still stands,
cf. [PDF: still unfit for human consumption, 20 years later](https://www.nngroup.com/articles/pdf-unfit-for-human-consumption).
Instead, the author recommends using HTML gateway pages instead of PDFs,
since [gateway pages prevent pdf shock](https://www.nngroup.com/articles/gateway-pages-prevent-pdf-shock).

I fully agree with the latter, and mostly try to clearly mark links that lead to a PDF so that the unwary reader is well prepared for leaving the world of HTML before following the link.

