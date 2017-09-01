<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- RevitLookup 2018.0.0.3
  https://github.com/jeremytammik/RevitLookup/releases/tag/2018.0.0.3
  merged pull request [#36](https://github.com/jeremytammik/RevitLookup/pull/36) from [@Andrey-Bushman](https://github.com/andrey-bushman): switch target platform to.Net 4.6 and replace Revit 2017 NuGet package by Revit 2018.1 NuGet package
  [Q] why do you require .net 4.6? this is not required by revit, is it? does the NuGet package need this?
  [A] Because I read about .net 4.6 [here](https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2018/ENU/Revit-API/files/GUID-FEF0ED40-8658-4C69-934D-7F83FB5D5B63-htm.html):
  Revit's binaries are built using .NET 4.5.2. However, Revit uses the runtime from .NET 4.6.
  [Q] why do you add so many new revit api dll references? adwindows, macros, uimacros, IFC...? does the NuGet package need this? i prefer keeping RevitLookup as minimal as possible, for obvious reasons... i would prefer leaving the .net version as required by revit, and only reference the necessary revit api dlls, though.
  [A] These DLL files were added by NuGet packages. The RevitAPI.dll and RevitAPIUI.dll libraries are used by Revit2018DevTools - this is one of my NuGet packages, it is used by Revit2018AddInTemplateSet templates.
  Additional references don't take a place, but it is convenient when they already added instead of requiring the programmer to add them each time manually. Creating a NuGet package for each DLL and then downloading each one separately is inconvenient and annoying to the developers. I asked some of them about it. Therefore they are the part of my NuGet packages.

- determine outer loop
  13179537 [Is the first Edgeloop still the outer loop?]
  https://forums.autodesk.com/t5/revit-api-forum/is-the-first-edgeloop-still-the-outer-loop/m-p/7225379

- https://forums.autodesk.com/t5/revit-api-forum/locationcurve-for-steel-column/m-p/7327197

- https://forums.autodesk.com/t5/revit-api-forum/reference-plane-from-reference-point/m-p/7329237

Determine outer edge loop #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/edge_refplane_column
Determining location curve for a steel column #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/edge_refplane_column
Determining reference plane from refpoint #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/edge_refplane_column

A whole bunch of exciting Revit API topics to start the week
&ndash; RevitLookup updated to use NuGet Revit API package
&ndash; Determining the outer-most <code>EdgeLoop</code>
&ndash; How to determine the location curve for a steel column
&ndash; Determining a reference plane from a reference point...

--->

### Edge Loop, Point Reference Plane and Column Line

A whole bunch of exciting Revit API topics to start the week:

- [RevitLookup updated to use NuGet Revit API package](#2)
- [Determining the outer-most `EdgeLoop`](#3)
- [How to determine the location curve for a steel column](#4)
- [Determining a reference plane from a reference point](#5)
- [Beware of multiple outer loops](#6)

####<a name="2"></a>RevitLookup Updated to use NuGet Revit API Package

Andrey Bushman updated RevitLookup with [pull request #36](https://github.com/jeremytammik/RevitLookup/pull/36) to
switch the target platform to .NET 4.6 and replace the Revit 2017 NuGet package by the Revit 2018.1 NuGet package.

**Question:** Why do you require .NET 4.6? This is not required by the Revit API, is it? Does the NuGet package need this?

**Answer:** Because I read about .NET 4.6 in the  [official Revit development requirements](https://knowledge.autodesk.com/search-result/caas/cloudHelp/cloudhelp/2018/ENU/Revit-API/files/GUID-FEF0ED40-8658-4C69-934D-7F83FB5D5B63-htm.html). They say: Revit's binaries are built using .NET 4.5.2. However, Revit uses the runtime from .NET 4.6.

**Question:** Why do you add so new Revit API DLL references? AdWindows, Macros, UIMacros, IFC...? Does the NuGet package need this? I prefer keeping RevitLookup as minimal as possible, for obvious reasons... I would prefer leaving the .NET version as required by Revit, and only reference the necessary Revit API DLLs.

**Answer:** These DLL references are automatically added by NuGet packages. The RevitAPI.dll and RevitAPIUI.dll libraries are used by Revit2018DevTools &ndash; this is one of my NuGet packages, it is used by Revit2018AddInTemplateSet templates.

Additional references don't take any space, and it is convenient when they are already added instead of requiring the programmer to add them each time manually. Creating a NuGet package for each DLL and then downloading each one separately is inconvenient and annoying for developers. I asked some of them about it. Therefore, they are the part of my NuGet packages.

Many thanks to Andrey for this update, included
in [RevitLookup 2018.0.0.3](https://github.com/jeremytammik/RevitLookup/releases/tag/2018.0.0.3).


####<a name="3"></a>Determining the Outer-Most EdgeLoop

[Richard Thomas](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) shared a nice solution to the question raised in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
asking [is the first EdgeLoop still the outer loop?](https://forums.autodesk.com/t5/revit-api-forum/is-the-first-edgeloop-still-the-outer-loop/m-p/7225379)

**Question:** I try to determine for all types of surfaces what is the outer `EdgeLoop`.
 
Is it always the first one returned by `face.EdgeLoops`?
 
If not, how to find it?
 
**Answer:** The Building Coder discussed how to
determine [2D polygon areas and outer loop](http://thebuildingcoder.typepad.com/blog/2008/12/2d-polygon-areas-and-outer-loop.html) by area comparison.

Richard presents an alternative solution based on a simple max and min calculation. In his words:

The area approach mentioned there is probably more reliable, but I had an alternate idea based on getting all co-ordinates linked to the edge loops they are from and then finding the minimums in a certain direction. Below, I've taken `min.x`, but but same would apply for `min.y`, `max.x` and `max.y`.
 
I could not think of a situation where if you convert the 3D co-ordinates of each edge curve to a co-ordinate system of the plane you would end up with a min.x for the outer loop greater than the min.x for the inner loop. Similarly, the max.x of the outer loop should be greater than the max.x of the inner loop. This appeared to be true for most cases but then there was the issue of circular curves where the two end points could be further back than a point projecting into the centre of the curve. So, I decided to treat cyclic curves differently by checking for min.x along the curve.
 
I came up with the two below functions, one for planar faces and the other for faces in general. They both seem to give reliable results on the cases in the image below (based on expected edge counts for the outer loops). However, there may be a case that fails. The parts related to co-ordinate transformations may need looking at (there may be better approaches). I checked the z of each transformed point to see that it was 0 or virtually 0, so seems to be as expected.
 
For the more generalised method I'm using API functionality which projects a point onto a face, I was expecting some possible intersection errors since I'm checking points on the boundary of the face. I've not checked such things in detail.
 
In summary, the area method mentioned in link is likely better and more reliable; not sure which is faster.

<center>
<img src="img/slabtypes.png" alt="Slab types" width="500"/>
</center>

<pre class="code">
<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Shared</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;Run(<span style="color:blue;">ByVal</span>&nbsp;commandData&nbsp;<span style="color:blue;">As</span>&nbsp;Autodesk.Revit.UI.<span style="color:#2b91af;">ExternalCommandData</span>)&nbsp;<span style="color:blue;">As</span>&nbsp;Autodesk.Revit.UI.<span style="color:#2b91af;">Result</span>
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;IntApp&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;=&nbsp;commandData.Application
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;IntUIDoc&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;=&nbsp;IntApp.ActiveUIDocument
&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;IntUIDoc&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Failed&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;IntDoc&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;=&nbsp;IntUIDoc.Document
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;R&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;=&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;<span style="color:blue;">Try</span>
&nbsp;&nbsp;&nbsp;&nbsp;R&nbsp;=&nbsp;IntUIDoc.Selection.PickObject(<span style="color:#2b91af;">Selection</span>.ObjectType.Face)
&nbsp;&nbsp;<span style="color:blue;">Catch</span>&nbsp;ex&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Exception</span>
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Try</span>
&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;R&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Cancelled&nbsp;<span style="color:blue;">Else</span>
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;F_El&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;=&nbsp;IntDoc.GetElement(R.ElementId)
&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;F_El&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Failed&nbsp;<span style="color:blue;">Else</span>
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;F&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(F_El.GetGeometryObjectFromReference(R),&nbsp;<span style="color:#2b91af;">Face</span>)
&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;F&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Failed&nbsp;<span style="color:blue;">Else</span>
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;EA1&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;=&nbsp;PlannerFaceOuterLoop(F)
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;EA2&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;=&nbsp;OuterLoop(F)
 
&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded
<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
 
<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Shared</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;OuterLoop(F&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Face</span>)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;MinU&nbsp;=&nbsp;<span style="color:blue;">Function</span>(C&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Curve</span>)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Double</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;C.IsCyclic&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Min&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Double</span>&nbsp;=&nbsp;<span style="color:blue;">Double</span>.NaN
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">For</span>&nbsp;I&nbsp;=&nbsp;0&nbsp;<span style="color:blue;">To</span>&nbsp;20
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Param&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Double</span>&nbsp;=&nbsp;I&nbsp;/&nbsp;20
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;CuvPt&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;=&nbsp;C.Evaluate(Param,&nbsp;<span style="color:blue;">True</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;IR&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">IntersectionResult</span>&nbsp;=&nbsp;F.Project(CuvPt)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;IR&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;=&nbsp;<span style="color:blue;">False</span>&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;Min&nbsp;=&nbsp;<span style="color:blue;">Double</span>.NaN&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Min&nbsp;=&nbsp;IR.UVPoint.U
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;IR.UVPoint.U&nbsp;&lt;&nbsp;Min&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Min&nbsp;=&nbsp;IR.UVPoint.U
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Next</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Min
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Pt1&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;=&nbsp;C.GetEndPoint(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Pt2&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;=&nbsp;C.GetEndPoint(1)
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;IR1&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">IntersectionResult</span>&nbsp;=&nbsp;F.Project(Pt1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;IR2&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">IntersectionResult</span>&nbsp;=&nbsp;F.Project(Pt2)
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;IR1&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">OrElse</span>&nbsp;IR2&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:blue;">Double</span>.NaN
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;IR1.UVPoint.U&nbsp;&lt;&nbsp;IR2.UVPoint.U&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;IR1.UVPoint.U
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;IR2.UVPoint.U
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Loops&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>)
&nbsp;&nbsp;Loops&nbsp;=&nbsp;<span style="color:blue;">From</span>&nbsp;L&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;<span style="color:blue;">In</span>&nbsp;F.EdgeLoops
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Select</span>&nbsp;L
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Cv&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">Tuple</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>,&nbsp;<span style="color:blue;">Double</span>))
&nbsp;&nbsp;Cv&nbsp;=&nbsp;<span style="color:blue;">From</span>&nbsp;L2&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;<span style="color:blue;">In</span>&nbsp;Loops
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">From</span>&nbsp;L3&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Edge</span>&nbsp;<span style="color:blue;">In</span>&nbsp;L2
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Let</span>&nbsp;Mu&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Double</span>&nbsp;=&nbsp;MinU(L3.AsCurve)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Where</span>&nbsp;Mu&nbsp;&lt;&gt;&nbsp;<span style="color:blue;">Double</span>.NaN
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Select</span>&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:#2b91af;">Tuple</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>,&nbsp;<span style="color:blue;">Double</span>)(L2,&nbsp;Mu)
 
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Out&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Tuple</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>,&nbsp;<span style="color:blue;">Double</span>)&nbsp;=&nbsp;Cv.ToList.Find(<span style="color:blue;">Function</span>(Jx)&nbsp;Jx.Item2&nbsp;=&nbsp;Cv.Min(<span style="color:blue;">Function</span>(Jv)&nbsp;Jv.Item2))
&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;Out&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Out.Item1
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
 
<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Shared</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;PlannerFaceOuterLoop(F&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Face</span>)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;PF&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">PlanarFace</span>&nbsp;=&nbsp;<span style="color:blue;">TryCast</span>(F,&nbsp;<span style="color:#2b91af;">PlanarFace</span>)
&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;PF&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Else</span>
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;FN&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;=&nbsp;PF.FaceNormal
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;T&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;=&nbsp;<span style="color:#2b91af;">Transform</span>.Identity
&nbsp;&nbsp;T.BasisZ&nbsp;=&nbsp;FN
&nbsp;&nbsp;T.BasisX&nbsp;=&nbsp;PF.XVector
&nbsp;&nbsp;T.BasisY&nbsp;=&nbsp;PF.YVector
&nbsp;&nbsp;T.Origin&nbsp;=&nbsp;PF.Origin
 
&nbsp;&nbsp;<span style="color:green;">&#39;Dim&nbsp;Zeds&nbsp;As&nbsp;New&nbsp;List(Of&nbsp;Double)</span>
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;MinU&nbsp;=&nbsp;<span style="color:blue;">Function</span>(C&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Curve</span>)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Double</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;C.IsCyclic&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Min&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Double</span>&nbsp;=&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">For</span>&nbsp;I&nbsp;=&nbsp;0&nbsp;<span style="color:blue;">To</span>&nbsp;20
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Param&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Double</span>&nbsp;=&nbsp;I&nbsp;/&nbsp;20
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;CuvPt&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;=&nbsp;C.Evaluate(Param,&nbsp;<span style="color:blue;">True</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;XYZt&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;=&nbsp;T.Inverse.OfPoint(CuvPt)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;I&nbsp;=&nbsp;0&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Min&nbsp;=&nbsp;XYZt.X
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;XYZt.X&nbsp;&lt;&nbsp;Min&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Min&nbsp;=&nbsp;XYZt.X
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Next</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Min
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Pt1&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;=&nbsp;C.GetEndPoint(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Pt2&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;=&nbsp;C.GetEndPoint(1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;XYZp&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">XYZ</span>()&nbsp;=&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(1)&nbsp;{}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZp(0)&nbsp;=&nbsp;T.Inverse.OfPoint(Pt1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZp(1)&nbsp;=&nbsp;T.Inverse.OfPoint(Pt2)
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">&#39;&nbsp;Zeds.Add(XYZp(0).Z)</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">&#39;&nbsp;Zeds.Add(XYZp(1).Z)</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;XYZp(0).X&nbsp;&lt;&nbsp;XYZp(1).X&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;XYZp(0).X
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;XYZp(1).X
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Loops&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>)
&nbsp;&nbsp;Loops&nbsp;=&nbsp;<span style="color:blue;">From</span>&nbsp;L&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;<span style="color:blue;">In</span>&nbsp;F.EdgeLoops
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Select</span>&nbsp;L
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Cv&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">Tuple</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>,&nbsp;<span style="color:blue;">Double</span>))
&nbsp;&nbsp;Cv&nbsp;=&nbsp;<span style="color:blue;">From</span>&nbsp;L2&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;<span style="color:blue;">In</span>&nbsp;Loops
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">From</span>&nbsp;L3&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Edge</span>&nbsp;<span style="color:blue;">In</span>&nbsp;L2
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Let</span>&nbsp;Mu&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Double</span>&nbsp;=&nbsp;MinU(L3.AsCurve)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Select</span>&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:#2b91af;">Tuple</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>,&nbsp;<span style="color:blue;">Double</span>)(L2,&nbsp;Mu)
 
 
&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Out&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Tuple</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>,&nbsp;<span style="color:blue;">Double</span>)&nbsp;=&nbsp;Cv.ToList.Find(<span style="color:blue;">Function</span>(Jx)&nbsp;Jx.Item2&nbsp;=&nbsp;Cv.Min(<span style="color:blue;">Function</span>(Jv)&nbsp;Jv.Item2))
&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;Out&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:blue;">Nothing</span>
&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Out.Item1
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
</pre>

I like Richard's idea, so I rewrote his functions in C# and added them
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).

Instead of using `C.Evaluate` twenty times over, I call the curve `Tessellate` method. That also handles the case of a linear curve gracefully, returning just the two end points, so the `MinU` function can be shortened and simplified:
 
<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">double</span>&nbsp;MinU(&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;C,&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;F&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;C.Tessellate()
&nbsp;&nbsp;&nbsp;&nbsp;.Select&lt;<span style="color:#2b91af;">XYZ</span>,&nbsp;<span style="color:#2b91af;">IntersectionResult</span>&gt;(&nbsp;p&nbsp;=&gt;&nbsp;F.Project(&nbsp;p&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Min&lt;<span style="color:#2b91af;">IntersectionResult</span>&gt;(&nbsp;ir&nbsp;=&gt;&nbsp;ir.UVPoint.U&nbsp;);
}
 
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">double</span>&nbsp;MinX(&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;C,&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;Tinv&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;C.Tessellate()
&nbsp;&nbsp;&nbsp;&nbsp;.Select&lt;<span style="color:#2b91af;">XYZ</span>,&nbsp;<span style="color:#2b91af;">XYZ</span>&gt;(&nbsp;p&nbsp;=&gt;&nbsp;Tinv.OfPoint(&nbsp;p&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Min&lt;<span style="color:#2b91af;">XYZ</span>&gt;(&nbsp;p&nbsp;=&gt;&nbsp;p.X&nbsp;);
}
 
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;OuterLoop(&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;F&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;eaMin&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">EdgeArrayArray</span>&nbsp;loops&nbsp;=&nbsp;F.EdgeLoops;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;uMin&nbsp;=&nbsp;<span style="color:blue;">double</span>.MaxValue;
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;a&nbsp;<span style="color:blue;">in</span>&nbsp;loops&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;uMin2&nbsp;=&nbsp;<span style="color:blue;">double</span>.MaxValue;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Edge</span>&nbsp;e&nbsp;<span style="color:blue;">in</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;min&nbsp;=&nbsp;MinU(&nbsp;e.AsCurve(),&nbsp;F&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;min&nbsp;&lt;&nbsp;uMin2&nbsp;)&nbsp;{&nbsp;uMin2&nbsp;=&nbsp;min;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;uMin2&nbsp;&lt;&nbsp;uMin&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uMin&nbsp;=&nbsp;uMin2;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eaMin&nbsp;=&nbsp;a;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;eaMin;
}
 
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;PlanarFaceOuterLoop(&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;F&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">PlanarFace</span>&nbsp;face&nbsp;=&nbsp;F&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">PlanarFace</span>;
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;face&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;T&nbsp;=&nbsp;<span style="color:#2b91af;">Transform</span>.Identity;
&nbsp;&nbsp;T.BasisZ&nbsp;=&nbsp;face.FaceNormal;
&nbsp;&nbsp;T.BasisX&nbsp;=&nbsp;face.XVector;
&nbsp;&nbsp;T.BasisY&nbsp;=&nbsp;face.YVector;
&nbsp;&nbsp;T.Origin&nbsp;=&nbsp;face.Origin;
&nbsp;&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;Tinv&nbsp;=&nbsp;T.Inverse;
 
&nbsp;&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;eaMin&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">EdgeArrayArray</span>&nbsp;loops&nbsp;=&nbsp;F.EdgeLoops;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;uMin&nbsp;=&nbsp;<span style="color:blue;">double</span>.MaxValue;
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">EdgeArray</span>&nbsp;a&nbsp;<span style="color:blue;">in</span>&nbsp;loops&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;uMin2&nbsp;=&nbsp;<span style="color:blue;">double</span>.MaxValue;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Edge</span>&nbsp;e&nbsp;<span style="color:blue;">in</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;min&nbsp;=&nbsp;MinX(&nbsp;e.AsCurve(),&nbsp;Tinv&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;min&nbsp;&lt;&nbsp;uMin2&nbsp;)&nbsp;{&nbsp;uMin2&nbsp;=&nbsp;min;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;uMin2&nbsp;&lt;&nbsp;uMin&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uMin&nbsp;=&nbsp;uMin2;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eaMin&nbsp;=&nbsp;a;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;eaMin;
}
</pre>
 
I added them
to [The Building Coder samples release 2018.0.134.3](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.134.3)
module [CmdSlabBoundaryArea.cs L29-L102](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdSlabBoundaryArea.cs#L29-L102).
 
Many thanks to Richard for implementing, testing and sharing this simple approach!


####<a name="4"></a>How to Determine the Location Curve for a Steel Column 

Richard also answered 
another [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on retrieving the [location curve of a steel column](https://forums.autodesk.com/t5/revit-api-forum/xyz-question/m-p/6460982):

**Question:** How can I get the location curve for a steel member column?

<center>
<img src="img/steel_column_location_curve.png" alt="Steel column location curve" width="227"/>
</center>

**Answer:**  When searching for an answer to a question like this, you should always start by first checking the examples provided by the official Revit SDK and the developer guide. I count 58 occurrences of `LocationCurve` in the SDK samples. The Building Coder also includes 64 different examples of accessing the `LocationCurve` of an element.

However, there are some twists involved specifically with steel columns, so this is a trick question.
 
A vertical column will have a location point and no curve at all.

Furthermore, its Z component will be zero, regardless what level it resides on.
 
A slanted column, on the other hand, is equipped with a location curve.
 
For vertical columns, you can derive end points via level parameters and offsets. Getting such information is not generally as easy as just getting a location curve. It follows the below procedure:
 
Check what type of column you are dealing with via `SLANTED_COLUMN_TYPE_PARAM` (Int32); it takes its value  from the `SlantedOrVerticalColumnType` enumeration.

Determine the start and end level elements from:

- `FAMILY_TOP_LEVEL_PARAM` (ElementID)
- `FAMILY_BASE_LEVEL_PARAM` (ElementID)

Consider the `Elevation` or `ProjectElevation` parameters of those level elements (depending on need).

Determine the offsets of the column ends from the corresponding levels via:

- `FAMILY_BASE_LEVEL_OFFSET_PARAM` (Double)
- `FAMILY_TOP_LEVEL_OFFSET_PARAM` (Double)

Add those to the numerical level of each end.
 
Alternatively, change `SLANTED_COLUMN_TYPE_PARAM` from `0` to `1` or `2`, which stands for `angle driven` and `endpoint driven`. Then, the column becomes equipped with a location curve and you can extract information from that.
 
I favour the first longer approach because it is purely reading the database, not transacting with it.

The above-mentioned built-in parameters have changed for consistency; older versions of Revit use ones specific to columns. Check the API documentation for details on this.

Finally, for the sake of completeness, here are previous discussions by The Building Coder on some aspects of slanted columns that illuminate further:
 
- [Creating a slanted column](http://thebuildingcoder.typepad.com/blog/2009/06/creating-a-slanted-column.html)
- [Slanted column cross section rotation](http://thebuildingcoder.typepad.com/blog/2011/03/slanted-column-cross-section-rotation.html)


####<a name="5"></a>Determining a Reference Plane from a Reference Point

Last but not least, another very useful and illuminating solution that deserves mentioning is from Fair59, once again, in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread on obtaining 
a [reference plane from reference point](https://forums.autodesk.com/t5/revit-api-forum/reference-plane-from-reference-point/m-p/7329237):

**Question:** In the Revit family environment, I am drawing two reference points `RefArrPts` and drawing a reference line `crv` that connects the two points. In the next step, I extract the reference points from the reference line `CrvRefPts`. In the last step, I try to extract the XY plane `plnRef` from the reference points `CrvRefPts`. The returned value is of type `REFERENCE`, the element id of `plnRef` is the same element id of the `CrvStRefPt`. The method `GetCoordinatePlaneReferenceXY` states that it returns a reference to the XY plane of the coordinate system, but it is returning a reference to the point instead.

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">CurveByPoints</span>&nbsp;crv&nbsp;=&nbsp;RvtDoc.FamilyCreate
&nbsp;&nbsp;&nbsp;&nbsp;.NewCurveByPoints(&nbsp;RefArrPts&nbsp;);
 
&nbsp;&nbsp;crv.IsReferenceLine&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">ReferencePointArray</span>&nbsp;CrvRefPts&nbsp;=&nbsp;crv.GetPoints();
&nbsp;&nbsp;<span style="color:#2b91af;">ReferencePoint</span>&nbsp;CrvStRefPt&nbsp;=&nbsp;CrvRefPts.get_Item(&nbsp;0&nbsp;);
&nbsp;&nbsp;plnRef&nbsp;=&nbsp;CrvStRefPt.GetCoordinatePlaneReferenceXY();
</pre>

What am I doing wrong? Any guidance would be greatly appreciated.

The line I am drawing is not orthogonal; therefore, I need to extract the plane from the curve end points so that the plane is perpendicular to the line. If I extract the plane from the original two points, I will get planes orthogonal to the world coordinates, which is not useful to me.

<center>
<img src="img/EndPt_Planes_01-01.jpg" alt="Reference point planes" width="624"/>
</center>

The reason for extracting these planes is to draw geometry on them (via the API) and then allow the user to move the line and have the constrained geometry update.

The steps the user would execute are as follows:

1. Run a command that draws the line and the geometry on the reference planes.
2. Move the line as desired within the model; all geometry will update. 

**Answer:** You can easily generate pure geometric planes yourself like this:
 
<pre class="code">
  XYZ p1 = first point
  XYZ q2 = second point
  XYZ v = p2 - p1
  XYZ normal = v.Normalise
  Plane plane1 = Plane.Create( p1, normal )
  Plane plane2 = Plane.Create( p2, normal )
</pre>

If you require a reference to an existing plane element in the model, you could search all of them for one with a matching origin and normal vector.

The planes you are after are faces of the solid of the reference line, the solid returned by `crv.get_geometry`. Unfortunately, they don't have a reference. If you require a reference to host the geometry, then you'd have to host two extra reference points on the reference line and use the references from those.

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ReferencePoint</span>&nbsp;rPnt0&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">ReferencePoint</span>&nbsp;rPnt1&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">SubTransaction</span>&nbsp;st&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">SubTransaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;st.Start();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PointLocationOnCurve</span>&nbsp;location&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PointLocationOnCurve</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PointOnCurveMeasurementType</span>.NormalizedCurveParameter,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0,&nbsp;<span style="color:#2b91af;">PointOnCurveMeasureFrom</span>.Beginning&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PointOnEdge</span>&nbsp;pointOnEdge&nbsp;=&nbsp;doc.Application.Create
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.NewPointOnEdge(&nbsp;crv.GeometryCurve.Reference,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;location&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;rPnt0&nbsp;=&nbsp;doc.FamilyCreate.NewReferencePoint(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pointOnEdge&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;location&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PointLocationOnCurve</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PointOnCurveMeasurementType</span>.NormalizedCurveParameter,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1,&nbsp;<span style="color:#2b91af;">PointOnCurveMeasureFrom</span>.Beginning&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;pointOnEdge&nbsp;=&nbsp;doc.Application.Create.NewPointOnEdge(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;crv.GeometryCurve.Reference,&nbsp;location&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;rPnt1&nbsp;=&nbsp;doc.FamilyCreate.NewReferencePoint(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pointOnEdge&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;st.Commit();
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:#2b91af;">ReferencePointArray</span>&nbsp;CrvRefPts&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ReferencePointArray</span>();
 
&nbsp;&nbsp;CrvRefPts.Append(&nbsp;rPnt0&nbsp;);
&nbsp;&nbsp;CrvRefPts.Append(&nbsp;rPnt1&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">ReferencePoint</span>&nbsp;CrvStRefPt&nbsp;=&nbsp;CrvRefPts.get_Item(&nbsp;0&nbsp;);
&nbsp;&nbsp;plnRef&nbsp;=&nbsp;CrvStRefPt.GetCoordinatePlaneReferenceYZ();
</pre>

Many thanks to Fair59 for explaining this and sharing the solution!


####<a name="6"></a>Beware of Multiple Outer Loops

Adrian Esdaile added 
a [warning in his comment below](http://thebuildingcoder.typepad.com/blog/2017/08/edge-loop-point-reference-plane-and-column-line.html#comment-3491949399) that
you should definitely be aware of:

In reference to the question about Outer loops: Unfortunately, the quirky nature of Revit will defeat you here. It is entire possible to create valid geometry in Revit composed of TWO (yes, TWO, or more...) OUTER loops. How this will behave with your code (or indeed ANY code!) is questionable.

For example &ndash; create a Floor in Sketch mode, create two 'islands' of floor, click OK... done! Revit calls this ONE floor object, but it's clearly TWO separate pieces &ndash; that behave as one. It is considered VERY POOR DRAFTING to create floors like this; but "users gonna use" if you give them a chance, and it's a quick and lazy way of doing things.

In a perfect world, Revit would slap you with an error for trying to create a daft and confusing object like this; but this world (and Revit, bless its binary heart) is far from perfect.

For an example of unimaginably bad practice in Revit drafting (but very common in the field, dammit...) See this [example file](zip/floors_of_unimaginable_evil.zip) I call
&ndash; [floors_of_unimaginable_evil.rvt](zip/floors_of_unimaginable_evil.rvt):

<center>
<img src="img/floors_of_unimagnable_evil_rvt.png" alt="Floors of unimagnable evil" width="500"/>
</center>

Look at the image above &ndash; there is something terribly wrong with the area reported for this floor... and what about the volume...? Huh? Why would someone do that? Doesn't matter why, but "users gonna use" as I said &ndash; if Revit allows it, users WILL do it! RVT 2016 format, by the way. Feel free to use this file as a test case for 'what can happen' :-D

Yes, I've used a Floor in this example, but Revit is not consistent in when it will or won't allow multiple curves to count as single curve. I suspect you can do this with columns, too, as a column might be defined by a swept blend... which greatly expands the possibilities for evil.

**Response:** Obviously, it is not that hard to test whether a Revit so-called edge loop consists of multiple disjoint loops, but it places substantial additional burden on the programmer.

My preferred way to deal with this (or at least protect myself as an add-in developer) would be to implement a reliable model checking algorithm, run automatically at regular intervals as well as before saving the BIM project, that checks for and warns about weird user input of this kind.

Obviously, the number of creative and involuntarily evil things that users can get up to is absolutely unlimited, so the number of checks performed would grow with time as new evil possibilities are discovered and discouraged.

