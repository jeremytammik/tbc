<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- Adjusting Wall.LocationCurve.Curve results in unexpected behaviour
  https://forums.autodesk.com/t5/revit-api-forum/adjusting-wall-locationcurve-curve-results-in-unexpected/m-p/9328145

- How can I get the center point of the part of wall which inside the room?
  https://forums.autodesk.com/t5/revit-api-forum/how-can-i-get-the-center-point-of-the-part-of-wall-which-inside/m-p/9331417#M44758
  beautiful solution by fair59

- Advice From The CIA: How To Sabotage Your Workplace
  https://corporate-rebels.com/cia-field-manual
  if you want to work effectively in a team, here are some very valid hints on what to avoid.

twitter:

 in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

An exciting discussion on applying minimal adjustments to the model, and yet another research result on the effectivity of visual presentation
&ndash; Adjusting versus recreating wall location curve
&ndash; Multimedia communication versus bullet points...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Adjusting Wall Location Curve and Visual Presentation

An exciting discussion on applying minimal adjustments to the model, and yet another research result on the effectivity of visual presentation:

- [Adjusting versus recreating wall location curve](#2)
- [Multimedia communication versus bullet points](#3)

#### <a name="2"></a>Adjusting versus Recreating Wall Location Curve

Harald Schmidt pointed out an interesting and important aspect of the old discussion of how
to [edit wall length](https://thebuildingcoder.typepad.com/blog/2010/08/edit-wall-length.html) in
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread,
on [adjusting Wall.LocationCurve.Curve results in unexpected behaviour](https://forums.autodesk.com/t5/revit-api-forum/adjusting-wall-locationcurve-curve-results-in-unexpected/m-p/9328145),
and once again Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen
came to the rescue with the final solution:

**Question:** My add-in adjusts walls lines slightly; it moves and rotates them a bit, and also slightly adjusts their length.

Using `wall.Location.Move` and `wall.Location.Rotate` enables adjusting the location and rotation of the walls, but not their length.

So, we decided to follow the approach suggested 10 years ago by The Building Coder to [
to [edit wall length](https://thebuildingcoder.typepad.com/blog/2010/08/edit-wall-length.html) by
creating a completely new wall location line from scratch like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:green;">//&nbsp;get&nbsp;the&nbsp;current&nbsp;wall&nbsp;location</span>
&nbsp;&nbsp;<span style="color:#2b91af;">LocationCurve</span>&nbsp;wallLocation&nbsp;=&nbsp;myWall.Location
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>;
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;get&nbsp;the&nbsp;points</span>
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pt1&nbsp;=&nbsp;wallLocation.Curve.get_EndPoint(&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pt2&nbsp;=&nbsp;wallLocation.Curve.get_EndPoint(&nbsp;1&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;change&nbsp;one&nbsp;point,&nbsp;e.g.&nbsp;move&nbsp;1000&nbsp;mm&nbsp;on&nbsp;X&nbsp;axis</span>
 
&nbsp;&nbsp;pt2&nbsp;=&nbsp;pt2.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0.01&nbsp;),&nbsp;0,&nbsp;0&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;create&nbsp;a&nbsp;new&nbsp;LineBound</span>
&nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;newWallLine&nbsp;=&nbsp;app.Create.NewLineBound(
&nbsp;&nbsp;&nbsp;&nbsp;pt1,&nbsp;pt2&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;update&nbsp;the&nbsp;wall&nbsp;curve</span>
&nbsp;&nbsp;wallLocation.Curve&nbsp;=&nbsp;newWallLine;
</pre>

This works fine for single walls, but fails in many cases where the wall has hosted elements like windows, and even in complex scenarios with multiple walls.

Note: the movement is always less than a very few millimetres!

To show you what I mean, I wrote a short macro embedded in the RVT attached.

The macro `LocationLineReset` just moves the wall inside the RVT by approx. 3 mm using the method suggested by The Building Coder:

<center>
<img src="img/adjust_wall_locationcurve_curve_macros.png" alt="Adjust wall LocationCurve curve macros" title="Adjust wall LocationCurve curve macros" width="600"/> <!-- 1104 -->
</center>

The result is the following:

<center>
<img src="img/adjust_wall_locationcurve_curve_error.png" alt="Adjust wall LocationCurve curve error" title="Adjust wall LocationCurve curve error" width="600"/> <!-- 840 -->
</center>

This seems like an unexpected behaviour.
The window is now located outside the wall, although the wall has moved only about 3 mm.
Is this a bug?

If I use the second macro `ShiftWall`, which just calls `wall.LocationCurve.Move` to create an equivalent translation comparable to the former macro, the result is fine.

Is there any method to adjust the length of the wall except resetting the `wall.LocationCurve.Curve` by creating a new curve using `Line.CreateBound`? That would solve our issue as well.

**Answer:** Thank you for your interesting observation and careful analysis.

I looked at your macros and can reproduce what you say.

Besides the macro you list, `LocationLineReset`, there is another one that slightly moves the existing location line instead of creating a new one, `ShiftWall`.

That latter macro completes successfully:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;LocationLineReset()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;doc&nbsp;=&nbsp;<span style="color:blue;">this</span>.Application.ActiveUIDocument.Document;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;wall&nbsp;=&nbsp;doc.GetElement(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;305891&nbsp;)&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Wall</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TransactionStatus</span>&nbsp;status;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;trans&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc,&nbsp;<span style="color:#a31515;">&quot;bla&quot;</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;trans.Start();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">LocationCurve</span>&nbsp;locationCurve&nbsp;=&nbsp;wall.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;wallLine&nbsp;=&nbsp;locationCurve.Curve&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Line</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;startPoint&nbsp;=&nbsp;wallLine.GetEndPoint(&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;endPoint&nbsp;=&nbsp;wallLine.GetEndPoint(&nbsp;1&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;minimalMoveVector&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0.01&nbsp;<span style="color:green;">/*&nbsp;=~&nbsp;3mm*/</span>,&nbsp;0.01,&nbsp;0.0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;startPoint&nbsp;+=&nbsp;minimalMoveVector;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;endPoint&nbsp;+=&nbsp;minimalMoveVector;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;locationCurve.Curve&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;startPoint,&nbsp;endPoint&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;status&nbsp;=&nbsp;trans.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;status&nbsp;!=&nbsp;<span style="color:#2b91af;">TransactionStatus</span>.Committed&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(&nbsp;<span style="color:#a31515;">&quot;Commit&nbsp;failed&quot;</span>&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;ShiftWall()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;doc&nbsp;=&nbsp;<span style="color:blue;">this</span>.Application.ActiveUIDocument.Document;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;wall&nbsp;=&nbsp;doc.GetElement(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;305891&nbsp;)&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Wall</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TransactionStatus</span>&nbsp;status;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;bSuccess&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;trans&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc,&nbsp;<span style="color:#a31515;">&quot;bla&quot;</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;trans.Start();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;minimalMoveVector&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0.01&nbsp;<span style="color:green;">/*&nbsp;=~&nbsp;3mm*/</span>,&nbsp;0.01,&nbsp;0.0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bSuccess&nbsp;=&nbsp;wall.Location.Move(&nbsp;minimalMoveVector&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;status&nbsp;=&nbsp;trans.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;status&nbsp;==&nbsp;<span style="color:#2b91af;">TransactionStatus</span>.Committed&nbsp;&amp;&amp;&nbsp;bSuccess&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(&nbsp;<span style="color:#a31515;">&quot;Shift&nbsp;succeeded&quot;</span>&nbsp;);
&nbsp;&nbsp;}
</pre>

I would assume that during the process of resetting the wall location line from scratch, the window position gets completely lost.

When you simply perform a small adjustment to the existing location line, the window position is retained and adjusted accordingly.

Therefore, I would suggest using the latter approach whenever possible.

You could even make use of the latter approach in several steps, adjust first one and then the other location line endpoint.

That should enable you to handle all required situations.

You can modify just one of the line endpoints without changing the other one, and you can also change both endpoints at the same time by moving them by different translation vectors...

... or so I thought, until this very moment.

Now I looked at the Revit API documentation of the Line and [Curve member methods](https://www.revitapidocs.com/2020/92a388f3-4949-465c-b938-2906ff6bdf5b.htm), expecting to point out the `SetEndPoint` methods to you, only to discover they do not exist.

Oh dear.

In that case, you really do have to create a new curve from scratch, and the problem you describe arises.

The only other way I could think of to try to modify an existing curve's length is to change its start and end parameters.

In theory, this can be achieved by calling MakeBound. However, all my attempts to use that method to modify the wall length had no result. Here is the final attempt:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;ShortenWall()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;<span style="color:blue;">this</span>.Application.ActiveUIDocument.Document;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;wall&nbsp;=&nbsp;doc.GetElement(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;305891&nbsp;)&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Wall</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TransactionStatus</span>&nbsp;status;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;trans&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;trans.Start(&nbsp;<span style="color:#a31515;">&quot;Shorten&nbsp;Wall&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">LocationCurve</span>&nbsp;lc&nbsp;=&nbsp;wall.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;ll&nbsp;=&nbsp;lc.Curve&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Line</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;pstart&nbsp;=&nbsp;ll.GetEndParameter(&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;pend&nbsp;=&nbsp;ll.GetEndParameter(&nbsp;1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;pdelta&nbsp;=&nbsp;0.05&nbsp;*&nbsp;(pend&nbsp;-&nbsp;pstart);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lc.Curve.MakeBound(&nbsp;pstart&nbsp;+&nbsp;pdelta,&nbsp;pend&nbsp;-&nbsp;pdelta&nbsp;);&nbsp;<span style="color:green;">//&nbsp;no&nbsp;observable&nbsp;change&nbsp;to&nbsp;wall</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(wall.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>).Curve.MakeUnbound();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(wall.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>).Curve.MakeBound(&nbsp;<span style="color:green;">//&nbsp;no&nbsp;observable&nbsp;change&nbsp;to&nbsp;wall</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pstart&nbsp;+&nbsp;pdelta,&nbsp;pend&nbsp;-&nbsp;pdelta&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;status&nbsp;=&nbsp;trans.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;status&nbsp;==&nbsp;<span style="color:#2b91af;">TransactionStatus</span>.Committed&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(&nbsp;<span style="color:#a31515;">&quot;Shorten&nbsp;Wall&nbsp;succeeded&quot;</span>&nbsp;);
&nbsp;&nbsp;}
</pre>

At this point, Fair59 comes to the rescue and adds:

You can adjust the curve of a wall with hosted elements, if the "curve definition" stays the same, i.e. just changing the start- and end parameters.

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">LocationCurve</span>&nbsp;lc&nbsp;=&nbsp;wall.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;ll&nbsp;=&nbsp;lc.Curve;
 
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;pstart&nbsp;=&nbsp;ll.GetEndParameter(&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;pend&nbsp;=&nbsp;ll.GetEndParameter(&nbsp;1&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;pdelta&nbsp;=&nbsp;0.001&nbsp;*&nbsp;(pend&nbsp;-&nbsp;pstart);
 
&nbsp;&nbsp;ll.MakeUnbound();
&nbsp;&nbsp;ll.MakeBound(&nbsp;pstart&nbsp;+&nbsp;pdelta,&nbsp;pend&nbsp;-&nbsp;pdelta&nbsp;);
 
&nbsp;&nbsp;lc.Curve&nbsp;=&nbsp;ll;
</pre>

**Response:** Thank you both for solving the issue!

Just a minor question: can we expect that the wall line parameter scales the position of the start/end point uniformly to feet?
E.g., if we want to adjust the length of the wall by 0.1 feet, can we always do the following:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;pstart&nbsp;=&nbsp;ll.GetEndParameter(&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;pend&nbsp;=&nbsp;ll.GetEndParameter(&nbsp;1&nbsp;);
 
&nbsp;&nbsp;ll.MakeUnbound();
&nbsp;&nbsp;ll.MakeBound(&nbsp;pstart,&nbsp;pend&nbsp;+&nbsp;0.1&nbsp;);&nbsp;<span style="color:green;">//&nbsp;move&nbsp;end&nbsp;point&nbsp;by&nbsp;0.1&nbsp;feet</span>
&nbsp;&nbsp;lc.Curve&nbsp;=&nbsp;ll;
</pre>

In my example it did, but I am not sure if this is an invariant we can rely on?

**Answer:** See the remarks in
the [online documentation of the GetEndParameter method](https://www.revitapidocs.com/2016/0f4b2c25-35f8-4e3c-c71a-0d41fb6935ce.htm):

> Returns the raw parameter value at the start or end of this curve.

> The start and end value of the parameter can be any value (as it is determined by the system based on the inputs).
For curves with regular curvature like lines and arcs, the raw parameter can be used to measure along the curve in Revit's default units (feet).
Raw parameters are also the only way to evaluate points along unbound curves.

Many thanks to Harald for raising this important issue and for Fair59 for his help in solving it.

#### <a name="3"></a>Multimedia Communication versus Bullet Points

Moving onto a more generic non-Revit-API topic, how many times have we heard
of [death by PowerPoint](https://duckduckgo.com/?q=death+by+PowerPoint) and
bullet list slide decks?

Still, they persist.

Now three researchers conducted another experiment, with over a thousand participants in eight countries:
[How the Multimedia Communication of Strategy Can Enable More Effective Recall and Learning](https://journals.aom.org/doi/10.5465/amle.2018.0066) by
Duncan N. Angwin, Stephen Cummings and Urs Daellenbach.

They conducted

> a multi-country experiment that tests the effects of different modes of strategy communication on student learning.
The results show the learning benefits to students of multimedia presentations of strategy and suggests how strategy professors should further encourage students to draw strategies in class.

A summary and discussion of the results is also provided in
the [TIM Lecture Series &ndash; Communicating Strategy: How Drawing Can Create Better Engagement](https://timreview.ca/article/922) by Stephen Cummings.

An even shorter summary was published by Krogerus and Tschaeppeler in [Das Magazin](https://www.dasmagazin.ch), who resume:

The presentation explained a strategy proposal.
Afterwards, participants were asked to summarise the main elements of the strategy and assess how secure they would feel discussing it with others.

Most importantly: half of them were shown slides with text; the other half received the explanation including illustrations.

The result was astonishing and intriguing:

The audience that enjoyed the visual presentation:

- Remembers <b><u>twice</u></b> as many strategy elements
- Understands complex elements <b><u>better</u></b>
- Feels <b><u>less</u></b> secure discussing the content with others
