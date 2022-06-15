<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- alpha shape
  Get room centerline(s) and intersections (For corridors/passage type rooms)
  https://forums.autodesk.com/t5/revit-api-forum/get-room-centerline-s-and-intersections-for-corridors-passage/m-p/11212632
  https://forums.autodesk.com/t5/revit-api-forum/get-room-centerline-s-and-intersections-for-corridors-passage/m-p/11216756

twitter:

A request for feedback on the upcoming Copy and Paste API enhancements and various interesting approaches to defining a corridor centre line in the #RevitAPI  @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/corridorskeleton

An urgent request for feedback from the Revit development team and an especially interesting thread from
the Revit API discussion forum
&ndash; Copy and paste API Feedback
&ndash; Corridor skeleton...

linkedin:

A request for feedback on the upcoming Copy and Paste API enhancements and various interesting approaches to defining a corridor centre line in the #RevitAPI

https://autode.sk/corridorskeleton

- Copy and paste API Feedback
- Corridor skeleton...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Corridor Skeleton, Copy and Paste API

Today, we highlight an urgent request for feedback from the Revit development team and an especially interesting thread from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160):

- [Copy and paste API feedback](#2)
- [Corridor skeleton](#3)

####<a name="2"></a> Copy and Paste API Feedback

The development team announced the Copy/Paste API enhancement and request feedback on it in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread asking 
to [help us test the enhanced Copy API for sketch members!](https://forums.autodesk.com/t5/revit-api-forum/june-preview-release-help-us-test-the-enhanced-copy-api-for/td-p/11234171)

> If you are a participant in the Revit beta program, please check out this month's June preview release!

> We have an enhanced API to better support Copy-Paste of Sketch members and would like your feedback on it.

> We’ve made Copy-Pasting of Sketch members in API more consistent with UI, and in the process fixed multiple bugs.

> Find the summary of changes and code samples to try out the new functionality on our Preview Release Forum.

> Apply for access at [revit.preview.access@autodesk.com](mailto:revit.preview.access@autodesk.com).

> For any questions or concerns you might have, please get in touch with us at [feedback.autodesk.com](mailto:feedback.autodesk.com).
 
####<a name="3"></a> Corridor Skeleton

An exciting discussion ensued in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread on how 
to [get room centerlines and intersections for corridors and passages](https://forums.autodesk.com/t5/revit-api-forum/get-room-centerline-s-and-intersections-for-corridors-passage/m-p/11216756):

**Question:** How do I get the centerlines of a single room shown below?
I know one way is to divide the room manually into rectangular rooms with additional room separation lines, that way I can just use bounding boxes and get individual center lines.
But how Do I approach this for a single room with multiple turns/crossing/intersection??

<center>
<img src="img/corridor_centerline_1.png" alt="Corridor centerline" title="Corridor centerline" width="400"/> <!-- 1117 -->
</center>

**Answer:** This sounds like an interesting computational algebra task.
I would suggest doing some purely geometrical research completely disconnected from Revit and BIM, to start with.
Initially, I thought that
an [alpha shape](https://en.wikipedia.org/wiki/Alpha_shape) might
be useful for you, but just looking at the picture,
the [minimum spanning tree](https://en.wikipedia.org/wiki/Minimum_spanning_tree) looks more like your goal.

**Response:** Thanks for the Alpha Shape suggestion.
That is what I am ultimately looking for.
It's also called Concave hull in some forums.

I went through the steps where one has to vary the 'Alpha' to get the desired outcome which is basically the radius of the circle getting used in the algorithm.

I looked at this [alpha shape (concave hull) algorithm in C#](https://stackoverflow.com/questions/16625063/alpha-shape-concave-hull-algorithm-in-c-sharp).
I definitely want to implement this for my case.

Meanwhile, me being lazy, I figured out an easy way to solve my particular problem.

I used part of
your [Revit API code for convex hull](https://thebuildingcoder.typepad.com/blog/2016/08/online-revit-api-docs-and-convex-hull.html#3) to
get the vertices of the room. 
This is necessary, as there are multiple boundary segments when there is an overlapping room separation line, a door in the wall, etc.
Instead of calculating the convex hull, I just iterate over those resulting vertices and determine the pair of points which are 'nearby'.
As the corridor width is fairly standard, I can define my own suitable 'nearby' tolerance.
Then, I calculate the midpoint of each pair.
That basically provides the centerline vertices:

<center>
<img src="img/corridor_centerline_2.png" alt="Corridor centerline" title="Corridor centerline" width="400"/> <!-- 1135 -->
</center>

I understand this is just a temporary and lazy solution that only works for my case.
I intend to implement the Alpha shape (concave hull) algorithm over the weekend. 

**Answer:** The simpler the better.
[Kiss](https://en.wikipedia.org/wiki/KISS_principle)!
I love your pragmatic closest-point-pair approach.
Thank you very much for sharing that, and looking forward to the alpha shape results.
Happy, fruitful weekend and successful coding!

**Answer2:** I think a [straight skeleton](https://en.wikipedia.org/wiki/Straight_skeleton),
or some other topological skeleton, would work much better than an alpha shape for your situation.

**Response:** So, I was implementing Alpha shape for this polygon; halfway through, I realized that already is an Alpha shape. :-)
I read more about Straight skeleton.
It led me to Medial Axis, which is basically the centerlines that we are talking about.
There are a few algorithms to calculate Medial Axis, e.g.,
to [find medial axis of a polygon using C#](https://stackoverflow.com/questions/1069523/find-medial-axis-of-a-polygon-using-c-sharp).
But this seems quite time consuming, and for now I am adhering
to [Kiss](https://en.wikipedia.org/wiki/KISS_principle)! :-)
Thanks @jeremy.tammik and @mhannonQ65N2 for your inputs. 

Here is the Method which returns the list of pairs of vertices which are 'nearby'.

Note: This only works when the corridor width is around 1500mm (which is design standard in my firm). For larger width, we can vary the 'tolerance' variable. Also, the length of a single branch/ junction is not less than 2550mm. Otherwise it'll return additional pairs. Which is more or less fine as the center points of those pairs will also lie on the Medial Axis (corridor centerline)

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;List&lt;List&lt;XYZ&gt;&gt;&nbsp;<span style="color:#74531f;">ReturnVertexPairs</span>(Room&nbsp;<span style="color:#1f377f;">corridorRoom</span>)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;SpatialElementBoundaryOptions&nbsp;<span style="color:#1f377f;">opt</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;SpatialElementBoundaryOptions
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SpatialElementBoundaryLocation&nbsp;=
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SpatialElementBoundaryLocation.Finish
&nbsp;&nbsp;&nbsp;&nbsp;};
 
&nbsp;&nbsp;&nbsp;&nbsp;IList&lt;IList&lt;BoundarySegment&gt;&gt;&nbsp;<span style="color:#1f377f;">loops</span>&nbsp;=&nbsp;corridorRoom.GetBoundarySegments(opt);
 
&nbsp;&nbsp;&nbsp;&nbsp;List&lt;XYZ&gt;&nbsp;<span style="color:#1f377f;">roomVertices</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;XYZ&gt;();&nbsp;<span style="color:green;">//List&nbsp;of&nbsp;all&nbsp;room&nbsp;vertices</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">foreach</span>&nbsp;(IList&lt;BoundarySegment&gt;&nbsp;<span style="color:#1f377f;">loop</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;loops)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//TaskDialog.Show(&quot;Revit&quot;,&nbsp;&quot;Total&nbsp;Segments&nbsp;=&nbsp;&quot;&nbsp;+&nbsp;loop.Count().ToString());</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;<span style="color:#1f377f;">p0</span>&nbsp;=&nbsp;<span style="color:blue;">null</span>;&nbsp;<span style="color:green;">//previous&nbsp;segment&nbsp;start&nbsp;point</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;<span style="color:#1f377f;">p</span>&nbsp;=&nbsp;<span style="color:blue;">null</span>;&nbsp;<span style="color:green;">//&nbsp;segment&nbsp;start&nbsp;point</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;<span style="color:#1f377f;">q</span>&nbsp;=&nbsp;<span style="color:blue;">null</span>;&nbsp;<span style="color:green;">//&nbsp;segment&nbsp;end&nbsp;point</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">foreach</span>&nbsp;(BoundarySegment&nbsp;<span style="color:#1f377f;">seg</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;loop)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;q&nbsp;=&nbsp;seg.GetCurve().GetEndPoint(1);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(p&nbsp;==&nbsp;<span style="color:blue;">null</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;roomVertices.Add(seg.GetCurve().GetEndPoint(0));
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p&nbsp;=&nbsp;seg.GetCurve().GetEndPoint(0);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p0&nbsp;=&nbsp;p;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">continue</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p&nbsp;=&nbsp;seg.GetCurve().GetEndPoint(0);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(p&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;&amp;&amp;&nbsp;p0&nbsp;!=&nbsp;<span style="color:blue;">null</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(AreCollinear(p0,&nbsp;p,&nbsp;q))<span style="color:green;">//skipping&nbsp;the&nbsp;segments&nbsp;that&nbsp;are&nbsp;collinear</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p0&nbsp;=&nbsp;p;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">continue</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;roomVertices.Add(p);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p0&nbsp;=&nbsp;p;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;<span style="color:#1f377f;">tolerance</span>&nbsp;=&nbsp;2550;&nbsp;<span style="color:green;">//Distance&nbsp;between&nbsp;two&nbsp;Points&nbsp;(in&nbsp;mm)&nbsp;should&nbsp;be&nbsp;less&nbsp;than&nbsp;this&nbsp;number</span>
&nbsp;&nbsp;&nbsp;&nbsp;List&lt;List&lt;XYZ&gt;&gt;&nbsp;<span style="color:#1f377f;">nearbyPairs</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;List&lt;XYZ&gt;&gt;();&nbsp;<span style="color:green;">//List&nbsp;of&nbsp;Pairs&nbsp;of&nbsp;nearby&nbsp;points</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">for</span>&nbsp;(<span style="color:blue;">int</span>&nbsp;<span style="color:#1f377f;">i</span>&nbsp;=&nbsp;0;&nbsp;i&nbsp;&lt;&nbsp;roomVertices.Count()&nbsp;-&nbsp;1;&nbsp;i++)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">for</span>&nbsp;(<span style="color:blue;">int</span>&nbsp;<span style="color:#1f377f;">j</span>&nbsp;=&nbsp;i&nbsp;+&nbsp;1;&nbsp;j&nbsp;&lt;&nbsp;roomVertices.Count();&nbsp;j++)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;<span style="color:#1f377f;">dist</span>&nbsp;=&nbsp;roomVertices[i].DistanceTo(roomVertices[j])&nbsp;*&nbsp;304.8;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(dist&nbsp;&lt;&nbsp;tolerance)&nbsp;<span style="color:green;">//checking&nbsp;whether&nbsp;two&nbsp;points&nbsp;are&nbsp;nearby&nbsp;based&nbsp;on&nbsp;tolerance</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nearbyPairs.Add(<span style="color:blue;">new</span>&nbsp;List&lt;XYZ&gt;&nbsp;{&nbsp;roomVertices[i],&nbsp;roomVertices[j]&nbsp;});
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//TaskDialog.Show(&quot;Revit&quot;,&nbsp;&quot;Total&nbsp;points&nbsp;=&nbsp;&quot;&nbsp;+&nbsp;roomVertices.Count().ToString()</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;&nbsp;+&nbsp;Environment.NewLine&nbsp;+&nbsp;&quot;Total&nbsp;Pairs&nbsp;=&nbsp;&quot;&nbsp;+&nbsp;nearbyPairs.Count());</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;nearbyPairs;
&nbsp;&nbsp;}
 
</pre>

Helper method to check whether points are collinear, used to skip collinear boundary segments:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;<span style="color:#74531f;">AreCollinear</span>(XYZ&nbsp;<span style="color:#1f377f;">p1</span>,&nbsp;XYZ&nbsp;<span style="color:#1f377f;">p2</span>,&nbsp;XYZ&nbsp;<span style="color:#1f377f;">p3</span>)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;<span style="color:#1f377f;">collinear</span>&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;<span style="color:#1f377f;">area</span>&nbsp;=&nbsp;0.5&nbsp;*&nbsp;Math.Abs(p1.X&nbsp;*&nbsp;(p2.Y&nbsp;-&nbsp;p3.Y)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;p2.X&nbsp;*&nbsp;(p3.Y&nbsp;-&nbsp;p1.Y)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;p3.X&nbsp;*&nbsp;(p1.Y&nbsp;-&nbsp;p2.Y));
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//sometimes&nbsp;area&nbsp;is&nbsp;not&nbsp;exactly&nbsp;zero&nbsp;but&nbsp;is&nbsp;very&nbsp;small&nbsp;number</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(area&nbsp;&lt;&nbsp;0.1)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;collinear&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;collinear;
&nbsp;&nbsp;}
</pre>

Many thanks to @amrut.modani.wwi for raising this and sharing his nice approach!
