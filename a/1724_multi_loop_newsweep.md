<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


- 14908176 [How to create a sweep with multiple closed loops in profile]
  https://forums.autodesk.com/t5/revit-api-forum/how-to-create-a-sweep-with-multiple-closed-loops-in-profile/m-p/8477617

- [Poverty isn't a lack of character; it's a lack of cash](https://youtu.be/ydKcaIE6O1k) TED talk by historian Rutger Bregman on June 13, 2017.
> "Ideas can and do change the world," he says, sharing his case for a provocative one: guaranteed basic income. Learn more about the idea's 500-year history and a forgotten modern experiment where it actually worked -- and imagine how much energy and talent we would unleash if we got rid of poverty once and for all.
<iframe width="560" height="315" src="https://www.youtube.com/embed/ydKcaIE6O1k" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

twitter:

Creating a sweep with multiple closed loops in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon and a TED talk on poverty versus universal basic income http://bit.ly/multiloopsweep

A demonstration of using the <code>NewSweep</code> method was very kindly provided in yet another helpful answer by Frank @Fair59 Aarssen to the Revit API discussion forum thread on how to create a sweep with multiple closed loops in profile.
Let me also highlight an interesting TED talk on the topic of poverty versus universal basic income
&ndash; Creating a sweep with multiple closed loops
&ndash; Poverty versus universal basic income...

linkedin:

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).

-->

### Creating a Sweep with Multiple Closed Loops

Until today, [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) lacked
a demonstration of using the `NewSweep` method.

That was very kindly provided in yet another helpful answer by 
Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen to 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to create a sweep with multiple closed loops in profile](https://forums.autodesk.com/t5/revit-api-forum/how-to-create-a-sweep-with-multiple-closed-loops-in-profile/m-p/8477617).

Let me also highlight an interesting TED talk on the topic of poverty versus universal basic income:

- [Creating a sweep with multiple closed loops](#2) 
- [Poverty versus universal basic income](#3) 

#### <a name="2"></a> Creating a Sweep with Multiple Closed Loops

**Question:** I want to create a sweep with multiple closed loops in profile, like this:

<center>
<img src="img/profile_with_two_holes.png" alt="Profile with multiple loops" width="240">
</center>

I can draw the profile on the plane with model lines, but if I use it to create a sweep, it will report errors such as "cannot create sweep" without any other tips.

I have created sweeps using profiles with two loops successfully like this:

<center>
<img src="img/extrusion_with_hole_1.png" alt="Profile with one hole" width="300">

<img src="img/extrusion_with_hole_2.png" alt="Profile with the other hole" width="300">
</center>

Is there any limitation, for example, the sweep profile can only have two closed loops at most, otherwise it will be wrong?

Later: After further testing, I still cannot create a sweep with three closed loops.

I tried orienting the two inner loops both clockwise and counterclockwise, but nothing helps.

**Answer:** Every loop needs to be a separate `CurveArray`!

I added Frank's correction to the test code provided and integrated it
into [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) 
module [CmdNewSweptBlend.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdNewSweptBlend.cs):

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Sweep</span>&nbsp;CreateSweepWithMultipleLoops(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Extrusion&nbsp;path</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CurveArray</span>&nbsp;path&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveArray</span>();
 
&nbsp;&nbsp;&nbsp;&nbsp;path.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;5,&nbsp;0&nbsp;)&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Profile&nbsp;vertices:&nbsp;rectangle&nbsp;with&nbsp;two</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;rectangular&nbsp;holes</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;0,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p3&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;15,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p4&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;15,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;a1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;1,&nbsp;5,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;a2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;3,&nbsp;5,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;a3&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;3,&nbsp;10,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;a4&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;1,&nbsp;10,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;b1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;5,&nbsp;5,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;b2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;7,&nbsp;5,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;b3&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;7,&nbsp;10,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;b4&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;5,&nbsp;10,&nbsp;0&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CurveArrArray</span>&nbsp;arrcurve&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveArrArray</span>();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CurveArray</span>&nbsp;curve&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveArray</span>();
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;p1,&nbsp;p2&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;p2,&nbsp;p3&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;p3,&nbsp;p4&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;p4,&nbsp;p1&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;arrcurve.Append(&nbsp;curve&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveArray</span>();
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;a1,&nbsp;a4&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;a4,&nbsp;a3&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;a3,&nbsp;a2&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;a2,&nbsp;a1&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;arrcurve.Append(&nbsp;curve&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CurveArray</span>();
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;b1,&nbsp;b4&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;b4,&nbsp;b3&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;b3,&nbsp;b2&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;curve.Append(&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;b2,&nbsp;b1&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;arrcurve.Append(&nbsp;curve&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;app&nbsp;=&nbsp;doc.Application;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SweepProfile</span>&nbsp;profile&nbsp;=&nbsp;app.Create
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.NewCurveLoopsProfile(&nbsp;arrcurve&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;plane&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByNormalAndOrigin(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisZ,&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SketchPlane</span>&nbsp;sketchPlane&nbsp;=&nbsp;<span style="color:#2b91af;">SketchPlane</span>.Create(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;plane&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Sweep</span>&nbsp;sweep&nbsp;=&nbsp;doc.FamilyCreate.NewSweep(&nbsp;<span style="color:blue;">true</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;path,&nbsp;sketchPlane,&nbsp;profile,&nbsp;0,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ProfilePlaneLocation</span>.Start&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;sweep;
&nbsp;&nbsp;}
</pre>

Here is the result of running this code in a new family document:

<center>
<img src="img/sweep_with_multiple_loops.png" alt="Sweep with multiple loops" width="240">
</center>

Many thanks to Frank for this solution!

#### <a name="3"></a> Poverty versus Universal Basic Income

In case you are interested in the topic of universal basic income, you might find this TED talk quite illuminating:
[Poverty isn't a lack of character; it's a lack of cash](https://youtu.be/ydKcaIE6O1k), by historian Rutger Bregman, on June 13, 2017.

> "Ideas can and do change the world," he says, sharing his case for a provocative one: guaranteed basic income. Learn more about the idea's 500-year history and a forgotten modern experiment where it actually worked &ndash; and imagine how much energy and talent we would unleash if we got rid of poverty once and for all.

<center>
<iframe width="560" height="315" src="https://www.youtube.com/embed/ydKcaIE6O1k" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>
