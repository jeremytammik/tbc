<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

Face.Intersect face treats faces as unbounded; handling that, source code colourisation and updated add-in wizard for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/faceintersect

My work on setting up a new PC is nearing completion.
There is also a need to clarify the use of the <code>Face.Intersect(Face)</code> method
&ndash; The unbounded <code>Face.Intersect</code> method
&ndash; Making use of the unbounded face intersection
&ndash; Rectangular face intersection ideas
&ndash; Copy as HTML update
&ndash; Visual Studio Revit add-in wizard update...

linkedin:

Face.Intersect face treats faces as unbounded; handling that, source code colourisation and updated add-in wizard for the #RevitAPI

http://bit.ly/faceintersect

My work on setting up a new PC is nearing completion.

There is also a need to clarify the use of the Face.Intersect(Face) method:

- The unbounded Face.Intersect method
- Making use of the unbounded face intersection
- Rectangular face intersection ideas
- Copy as HTML update
- Visual Studio Revit add-in wizard update...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Face Intersect Face is Unbounded

My work on setting up a new PC is nearing completion.

There is also a need to clarify the use of the `Face.Intersect(Face)` method:

- [The unbounded `Face.Intersect` method](#2)
- [Making use of the unbounded face intersection](#3)
- [Rectangular face intersection ideas](#4)
- [Copy as HTML update](#5)
- [Visual Studio Revit add-in wizard update](#6)

####<a name="2"></a> The Unbounded Face.Intersect Method 

Several [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) threads are discussing the status of
the [Face.Intersect (Face) method](https://www.revitapidocs.com/2020/91f650a2-bb95-650b-7c00-d431fa613753.htm):

- [Surprising results from `Face.Intersect(Face)` method](https://forums.autodesk.com/t5/revit-api-forum/surprising-results-from-face-intersect-face-method/m-p/8992586)
- [Problems with `Intersect` method (`Face`)](https://forums.autodesk.com/t5/revit-api-forum/problems-with-intersect-method-face/m-p/8992566)
- [Get connection type and geometry between two elements from the model](https://forums.autodesk.com/t5/revit-api-forum/get-conection-type-and-geometry-between-two-elements-from-the/m-p/6465671)
- [`Face` class `Intersect` method problem](https://forums.autodesk.com/t5/revit-api-forum/face-class-intersect-method-problem/m-p/7460720)

Briefly, the main problem is this:

**Question:** This issue was mentioned in 2016, in the third thread listed above, but it is worth bringing up again, as the issue hasn't been resolved yet in 2018.

As far as I can tell, the `Face.Intersect(face)` method always returns `FaceIntersectionFaceResult.Intersecting`.

When I run the code below in a view with a single wall and single floor, each face to face test returns an intersection:

<center>
<img src="img/face_intersect_face_1.png" alt="Non-intersecting faces" width="657">
</center>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;list&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;commandData.Application.ActiveUIDocument.Document,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;commandData.View.Id&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;e&nbsp;=&gt;&nbsp;e&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;||&nbsp;e&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">Floor</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;f1&nbsp;<span style="color:blue;">in</span>&nbsp;list.First()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.get_Geometry(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfType&lt;<span style="color:#2b91af;">Solid</span>&gt;().First()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Faces.OfType&lt;<span style="color:#2b91af;">Face</span>&gt;()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;f2&nbsp;<span style="color:blue;">in</span>&nbsp;list.Last()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.get_Geometry(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfType&lt;<span style="color:#2b91af;">Solid</span>&gt;().First()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Faces.OfType&lt;<span style="color:#2b91af;">Face</span>&gt;()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;f1.Intersect(&nbsp;f2&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==&nbsp;<span style="color:#2b91af;">FaceIntersectionFaceResult</span>.Intersecting&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;System.Windows.Forms.<span style="color:#2b91af;">MessageBox</span>.Show(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Intersects&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Continue&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Windows.Forms.<span style="color:#2b91af;">MessageBoxButtons</span>.OKCancel,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Windows.Forms.<span style="color:#2b91af;">MessageBoxIcon</span>.Exclamation&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==&nbsp;System.Windows.Forms.<span style="color:#2b91af;">DialogResult</span>.Cancel&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
</pre>

**Answer:** Thank you for your report and reproducible case.

I see the same behaviour in Revit 2019 as well.

I added some code to The Building Coder samples
in [CmdIntersectJunctionBox.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdIntersectJunctionBox.cs#L27-L116) to
test and report in more depth:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;all&nbsp;faces&nbsp;from&nbsp;first&nbsp;solid&nbsp;&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;of&nbsp;the&nbsp;given&nbsp;element.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Face</span>&gt;&nbsp;GetFaces(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Options</span>&nbsp;opt&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>();
&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Face</span>&gt;&nbsp;faces&nbsp;=&nbsp;e
&nbsp;&nbsp;&nbsp;&nbsp;.get_Geometry(&nbsp;opt&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfType&lt;<span style="color:#2b91af;">Solid</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.First()
&nbsp;&nbsp;&nbsp;&nbsp;.Faces
&nbsp;&nbsp;&nbsp;&nbsp;.OfType&lt;<span style="color:#2b91af;">Face</span>&gt;();
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;n&nbsp;=&nbsp;faces.Count();
&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;{0}&nbsp;has&nbsp;{1}&nbsp;face{2}.&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;e.GetType().Name,&nbsp;n,&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;n&nbsp;)&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;faces;
}
 
<span style="color:blue;">void</span>&nbsp;TestIntersect(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;view&nbsp;=&nbsp;doc.ActiveView;
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;list&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc,&nbsp;view.Id&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;e&nbsp;=&gt;&nbsp;e&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;||&nbsp;e&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">Floor</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;n&nbsp;=&nbsp;list.Count();
 
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;floor&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;wall&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;2&nbsp;==&nbsp;n&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;floor&nbsp;=&nbsp;list.First()&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Floor</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;floor&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floor&nbsp;=&nbsp;list.Last()&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Floor</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;wall&nbsp;=&nbsp;list.First()&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Wall</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;wall&nbsp;=&nbsp;list.Last()&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Wall</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;floor&nbsp;||&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;wall&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.ErrorMsg(&nbsp;<span style="color:#a31515;">&quot;Please&nbsp;run&nbsp;this&nbsp;command&nbsp;in&nbsp;a&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;document&nbsp;with&nbsp;just&nbsp;one&nbsp;floor&nbsp;and&nbsp;one&nbsp;wall&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;with&nbsp;no&nbsp;mutual&nbsp;intersection&quot;</span>&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Options</span>&nbsp;opt&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Face</span>&gt;&nbsp;floorFaces&nbsp;=&nbsp;GetFaces(&nbsp;floor&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Face</span>&gt;&nbsp;wallFaces&nbsp;=&nbsp;GetFaces(&nbsp;wall&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;n&nbsp;=&nbsp;0;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;f1&nbsp;<span style="color:blue;">in</span>&nbsp;floorFaces&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;f2&nbsp;<span style="color:blue;">in</span>&nbsp;wallFaces&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;f1.Intersect(&nbsp;f2&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==&nbsp;<span style="color:#2b91af;">FaceIntersectionFaceResult</span>.Intersecting&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;++n;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;System.Windows.Forms.<span style="color:#2b91af;">MessageBox</span>.Show(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Intersects&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Continue&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Windows.Forms.<span style="color:#2b91af;">MessageBoxButtons</span>.OKCancel,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Windows.Forms.<span style="color:#2b91af;">MessageBoxIcon</span>.Exclamation&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==&nbsp;System.Windows.Forms.<span style="color:#2b91af;">DialogResult</span>.Cancel&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;{0}&nbsp;face-face&nbsp;intersection{1}.&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;n,&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;n&nbsp;)&nbsp;);
&nbsp;&nbsp;}
}
</pre>

Here is a simple test model with a disjunct floor and wall:

<center>
<img src="img/floor_wall_disjunct.png" alt="Disjunct floor and wall" width="388">
</center>

My sample code reports:

<pre>
  Floor has 7 faces.
  Wall has 6 faces.
  38 face-face intersections.
</pre>

So, in fact, not every face-to-face test reports an intersection, because 7*6 equals 42.

Only the vast majority do &nbsp; :-)

I logged the issue *REVIT-133627 [Face.Intersect returns false positives]* with our development team to explore this further and provide an explanation.

This turned out to be a duplicate of an existing older issue, *REVIT-58034 [API Face.Intersect(Face) returns true even if two faces don't intersect with each other]* and was therefore closed again.

The development team replied:

> We are aware of this issue.
This function does indeed not do what one expects.
At most, it computes intersections between the underlying (unbounded) surfaces, not the (bounded) faces lying in the surfaces.
As a first step, the documentation will be updated to reflect this fact.
Then, we'll see whether resources can be found to fully implement the face intersection functionality, or remove the incomplete functionality.

As a result, *REVIT-58034* was renamed. Currently, the following related tickets have all been closed:

- REVIT-58034 [Improve documentation for API Face.Intersect(Face) to reflect what the function actually does] 
- REVIT-133627 [Face.Intersect returns false positives] 
- REVIT-133819 [Improve API Face.Intersect(Face) to actually intersect faces, not underlying surfaces] 

####<a name="3"></a> Making Use of the Unbounded Face Intersection

Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen adds:

I have previously plotted the intersections using the `ByRef` curve overload and found this to be the case, as explained in the thread 
on the [`Face` class `Intersect` method problem](https://forums.autodesk.com/t5/revit-api-forum/face-class-intersect-method-problem/m-p/7460720):

> Apparently, the intersecting faces are considered infinite with therefore many possible intersections beyond the range of the element itself. 

> In this image, the green lines are plotted using curves from the overload `Face.Intersect(ByVal Face, ByRef Curve)`:

<center>
<img src="img/face_intersect_wall_intersects.png" alt="Unbounded face intersections between two walls" width="930">
</center>

> Hard to understand at first why they all exist, but once you trace along parallel to the faces, you can see all are valid.

In actual fact, an API user may prefer to find this form of intersection, rather than be told no intersection exists (due to the bounds of the face preventing it).

You can always compare points on the original face to those on the curve intersect result to check if there is an actual intersection.

However, if no results were given, then that would not be useful at all.

Probably, there should be a bound versus unbound option, perhaps.

####<a name="4"></a> Rectangular Face Intersection Ideas

The sample images shown above all include rectangular wall faces.

If the potentially intersecting faces  are rectangular, the problem can presumably be reduced to a pretty simple query.

One approach would be to retrieve the intersection curves of the unbounded faces and test whether they lie on the bounded faces, as suggested above by Frank.

Alternatively, a DIY solution may also be feasible and more efficient.

After all, a rectangular face can be seen as two coplanar triangles, and the intersection of 3D triangles is a pretty common requirement.

Here are some useful-looking results that showed up in a couple of quick Internet searches:

- [A Fast Triangle-Triangle Intersection Test](https://web.stanford.edu/class/cs277/resources/papers/Moller1997b.pdf)
- [Efficient AABB/triangle intersection in C#](https://stackoverflow.com/questions/17458562/efficient-aabb-triangle-intersection-in-c-sharp)
- [How do I find the Intersection of two 3D triangles?](https://math.stackexchange.com/questions/1220102/how-do-i-find-the-intersection-of-two-3d-triangles)
- [Algorithms for ray (or segment) and triangle intersection, triangle-plane and triangle-triangle intersection](http://geomalgorithms.com/a06-_intersect-2.html)

If anyone would like to share a reliable, robust, and efficient 3D triangle or rectangle intersection algorithm, please let us know.

Thank you!


####<a name="5"></a> Copy as HTML Update

I implemented and installed a couple of 
different [source code colourizer tools](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.36) in
the past.

This spring, I set up 
the [PowerTools 2015 Copy HTML Markup](https://thebuildingcoder.typepad.com/blog/2019/04/close-doc-and-zero-doc-rvtsamples.html#4) functionality.

On the new computer, I updated to Visual Studio 2017 instead of Visual Studio 2015.

Accordingly, I update the code colouriser
to [Productivity Power Tools 2017/2019](https://marketplace.visualstudio.com/items?itemName=VisualStudioPlatformTeam.ProductivityPowerPack2017).

####<a name="6"></a> Visual Studio Revit Add-In Wizard Update

In the same vein, I reinstalled
the [Visual Studio Revit Add-In Wizards](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20),
again, slightly updated for Visual Studio 2017, captured
in [VisualStudioRevitAddinWizard release 2020.0.0.2)](https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases/tag/2020.0.0.2).
