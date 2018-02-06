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

- 13849819 [DirectShape内にボイドを入れてジオメトリをカットできますか(すみませんが、少し急ぎでお願いします)]

DirectShape from BrepBuilder and Boolean #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/brepbuilder

Here is an interesting code snippet illustrating the use of <code>BRepBuilder</code> and Boolean operations to generate a <code>DirectShape</code>.
It might come in useful somewhere, even though this approach is non-optimal to address the task at hand, as explained below...

--->

### DirectShape from BrepBuilder and Boolean

Here is an interesting code snippet illustrating the use of `BRepBuilder` and Boolean operations to generate a `DirectShape`.

It might come in useful somewhere, even though this approach is non-optimal to address the task at hand, as explained below.

**Question:** I want to create a `DirectShape` object which is cut by void geometry.

I found that
the [`BRepBuilder` constructor](http://www.revitapidocs.com/2018.1/b3eb95b6-2297-44dc-df94-38aed1940b8c.htm) accepts
both `BRepType.Void` and `BRepType.Solid`, so I tested if I could use that.

However, my attempts so far lead to an internal error.
 
Is there any mistake in my test code?

Does the `DirectShape.AppendShape` method not accept a void shape?


<center>
<img src="img/directshape_brepbuilder_boolean_2.png" alt="DirectShape from BrepBuilder Boolean" width="255"/>
</center>


**Answer:** It seems to me that you actually want to do is create two solid geometries, and then use a Boolean operation to subtract one from the other.

This will give the desired shape.

Taking a look at the code, I notice that the function `CreateBrepVoid` is incorrect &ndash; it tells the `BRepBuilder` that it wants to create a void, but the geometry defines a solid, not a void. This is determined by the face and edge loop orientations. I assume that `BRepBuilder` complains about invalid input at some point in the process.
 
The orientation conventions are discussed in the descriptions of
BRepBuilder [AddFace](http://www.revitapidocs.com/2018.1/cb899f6d-c4e0-0983-ab70-bae0a620dc8d.htm)
and [AddCoEdge](http://www.revitapidocs.com/2018.1/c4713a48-712b-e293-6745-a266af97e195.htm) methods.
 
Please note that `BRepBuilder` wasn’t really meant for 'manually' constructing geometry. Its interface is very cumbersome for that purpose. It was meant for translating existing geometry into Revit, with rather thorough validation of the input geometry.

Here is the code that constructs two solid geometries and then applies the Boolean operation to find the difference.

The code constructs the desired `DirectShape` shown in the image above:

<pre class="code">
[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">CmdBrepBuilder</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Create&nbsp;a&nbsp;cube&nbsp;100&nbsp;x&nbsp;100&nbsp;x&nbsp;100,&nbsp;from&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;(0,0,0)&nbsp;to&nbsp;(100,&nbsp;100,&nbsp;100).</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">BRepBuilder</span>&nbsp;CreateBrepSolid()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilder</span>&nbsp;b&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BRepBuilder</span>(&nbsp;<span style="color:#2b91af;">BRepType</span>.Solid&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;1.&nbsp;Planes.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;naming&nbsp;convention&nbsp;for&nbsp;faces&nbsp;and&nbsp;planes:</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;We&nbsp;are&nbsp;looking&nbsp;at&nbsp;this&nbsp;cube&nbsp;in&nbsp;an&nbsp;isometric&nbsp;view.&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;X&nbsp;is&nbsp;down&nbsp;and&nbsp;to&nbsp;the&nbsp;left&nbsp;of&nbsp;us,&nbsp;Y&nbsp;is&nbsp;horizontal&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;and&nbsp;points&nbsp;to&nbsp;the&nbsp;right,&nbsp;Z&nbsp;is&nbsp;up.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;front&nbsp;and&nbsp;back&nbsp;faces&nbsp;are&nbsp;along&nbsp;the&nbsp;X&nbsp;axis,&nbsp;left&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;and&nbsp;right&nbsp;are&nbsp;along&nbsp;the&nbsp;Y&nbsp;axis,&nbsp;top&nbsp;and&nbsp;bottom&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;are&nbsp;along&nbsp;the&nbsp;Z&nbsp;axis.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;bottom&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByOriginAndBasis(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;50,&nbsp;50,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;1,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;1,&nbsp;0&nbsp;)&nbsp;);&nbsp;<span style="color:green;">//&nbsp;bottom.&nbsp;XY&nbsp;plane,&nbsp;Z&nbsp;=&nbsp;0,&nbsp;normal&nbsp;pointing&nbsp;inside&nbsp;the&nbsp;cube.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;top&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByOriginAndBasis(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;50,&nbsp;50,&nbsp;100&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;1,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;1,&nbsp;0&nbsp;)&nbsp;);&nbsp;<span style="color:green;">//&nbsp;top.&nbsp;XY&nbsp;plane,&nbsp;Z&nbsp;=&nbsp;100,&nbsp;normal&nbsp;pointing&nbsp;outside&nbsp;the&nbsp;cube.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;front&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByOriginAndBasis(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;50,&nbsp;50&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;1&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;1,&nbsp;0&nbsp;)&nbsp;);&nbsp;<span style="color:green;">//&nbsp;front&nbsp;side.&nbsp;ZY&nbsp;plane,&nbsp;X&nbsp;=&nbsp;0,&nbsp;normal&nbsp;pointing&nbsp;inside&nbsp;the&nbsp;cube.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;back&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByOriginAndBasis(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;50,&nbsp;50&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;1&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;1,&nbsp;0&nbsp;)&nbsp;);&nbsp;<span style="color:green;">//&nbsp;back&nbsp;side.&nbsp;ZY&nbsp;plane,&nbsp;X&nbsp;=&nbsp;0,&nbsp;normal&nbsp;pointing&nbsp;outside&nbsp;the&nbsp;cube.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;left&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByOriginAndBasis(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;50,&nbsp;0,&nbsp;50&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;1&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;1,&nbsp;0,&nbsp;0&nbsp;)&nbsp;);&nbsp;<span style="color:green;">//&nbsp;left&nbsp;side.&nbsp;ZX&nbsp;plane,&nbsp;Y&nbsp;=&nbsp;0,&nbsp;normal&nbsp;pointing&nbsp;inside&nbsp;the&nbsp;cube</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;right&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByOriginAndBasis(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;50,&nbsp;100,&nbsp;50&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;1&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;1,&nbsp;0,&nbsp;0&nbsp;)&nbsp;);&nbsp;<span style="color:green;">//&nbsp;right&nbsp;side.&nbsp;ZX&nbsp;plane,&nbsp;Y&nbsp;=&nbsp;100,&nbsp;normal&nbsp;pointing&nbsp;outside&nbsp;the&nbsp;cube</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;2.&nbsp;Faces.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;faceId_Bottom&nbsp;=&nbsp;b.AddFace(&nbsp;<span style="color:#2b91af;">BRepBuilderSurfaceGeometry</span>.Create(&nbsp;bottom,&nbsp;<span style="color:blue;">null</span>&nbsp;),&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;faceId_Top&nbsp;=&nbsp;b.AddFace(&nbsp;<span style="color:#2b91af;">BRepBuilderSurfaceGeometry</span>.Create(&nbsp;top,&nbsp;<span style="color:blue;">null</span>&nbsp;),&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;faceId_Front&nbsp;=&nbsp;b.AddFace(&nbsp;<span style="color:#2b91af;">BRepBuilderSurfaceGeometry</span>.Create(&nbsp;front,&nbsp;<span style="color:blue;">null</span>&nbsp;),&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;faceId_Back&nbsp;=&nbsp;b.AddFace(&nbsp;<span style="color:#2b91af;">BRepBuilderSurfaceGeometry</span>.Create(&nbsp;back,&nbsp;<span style="color:blue;">null</span>&nbsp;),&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;faceId_Left&nbsp;=&nbsp;b.AddFace(&nbsp;<span style="color:#2b91af;">BRepBuilderSurfaceGeometry</span>.Create(&nbsp;left,&nbsp;<span style="color:blue;">null</span>&nbsp;),&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;faceId_Right&nbsp;=&nbsp;b.AddFace(&nbsp;<span style="color:#2b91af;">BRepBuilderSurfaceGeometry</span>.Create(&nbsp;right,&nbsp;<span style="color:blue;">null</span>&nbsp;),&nbsp;<span style="color:blue;">false</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;3.&nbsp;Edges.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;3.a&nbsp;(define&nbsp;edge&nbsp;geometry)</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;walk&nbsp;around&nbsp;bottom&nbsp;face</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeBottomFront&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;100,&nbsp;0&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeBottomRight&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;100,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;100,&nbsp;0&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeBottomBack&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;100,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;0&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeBottomLeft&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;0,&nbsp;0&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;now&nbsp;walk&nbsp;around&nbsp;top&nbsp;face</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeTopFront&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;0,&nbsp;100&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;100,&nbsp;100&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeTopRight&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;100,&nbsp;100&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;100,&nbsp;100&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeTopBack&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;100,&nbsp;100&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;100&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeTopLeft&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;100&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;0,&nbsp;100&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;sides</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeFrontRight&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;100,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;100,&nbsp;100&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeRightBack&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;100,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;100,&nbsp;100&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeBackLeft&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;100&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;edgeLeftFront&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;100,&nbsp;0,&nbsp;100&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;3.b&nbsp;(define&nbsp;the&nbsp;edges&nbsp;themselves)</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_BottomFront&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeBottomFront&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_BottomRight&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeBottomRight&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_BottomBack&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeBottomBack&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_BottomLeft&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeBottomLeft&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_TopFront&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeTopFront&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_TopRight&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeTopRight&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_TopBack&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeTopBack&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_TopLeft&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeTopLeft&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_FrontRight&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeFrontRight&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_RightBack&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeRightBack&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_BackLeft&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeBackLeft&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;edgeId_LeftFront&nbsp;=&nbsp;b.AddEdge(&nbsp;edgeLeftFront&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;4.&nbsp;Loops.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;loopId_Bottom&nbsp;=&nbsp;b.AddLoop(&nbsp;faceId_Bottom&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;loopId_Top&nbsp;=&nbsp;b.AddLoop(&nbsp;faceId_Top&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;loopId_Front&nbsp;=&nbsp;b.AddLoop(&nbsp;faceId_Front&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;loopId_Back&nbsp;=&nbsp;b.AddLoop(&nbsp;faceId_Back&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;loopId_Right&nbsp;=&nbsp;b.AddLoop(&nbsp;faceId_Right&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;loopId_Left&nbsp;=&nbsp;b.AddLoop(&nbsp;faceId_Left&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;5.&nbsp;Co-edges.&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Bottom&nbsp;face.&nbsp;All&nbsp;edges&nbsp;reversed</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Bottom,&nbsp;edgeId_BottomFront,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;front&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Bottom,&nbsp;edgeId_BottomLeft,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;left&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Bottom,&nbsp;edgeId_BottomBack,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;back&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Bottom,&nbsp;edgeId_BottomRight,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;right&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishLoop(&nbsp;loopId_Bottom&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishFace(&nbsp;faceId_Bottom&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Top&nbsp;face.&nbsp;All&nbsp;edges&nbsp;NOT&nbsp;reversed.</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Top,&nbsp;edgeId_TopFront,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;front&nbsp;loop.</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Top,&nbsp;edgeId_TopRight,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;right&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Top,&nbsp;edgeId_TopBack,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;back&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Top,&nbsp;edgeId_TopLeft,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;left&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishLoop(&nbsp;loopId_Top&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishFace(&nbsp;faceId_Top&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Front&nbsp;face.</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Front,&nbsp;edgeId_BottomFront,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;bottom&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Front,&nbsp;edgeId_FrontRight,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;right&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Front,&nbsp;edgeId_TopFront,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;top&nbsp;loop.</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Front,&nbsp;edgeId_LeftFront,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;left&nbsp;loop.</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishLoop(&nbsp;loopId_Front&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishFace(&nbsp;faceId_Front&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Back&nbsp;face</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Back,&nbsp;edgeId_BottomBack,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;bottom&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Back,&nbsp;edgeId_BackLeft,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;left&nbsp;loop.</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Back,&nbsp;edgeId_TopBack,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;top&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Back,&nbsp;edgeId_RightBack,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;right&nbsp;loop.</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishLoop(&nbsp;loopId_Back&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishFace(&nbsp;faceId_Back&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Right&nbsp;face</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Right,&nbsp;edgeId_BottomRight,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;bottom&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Right,&nbsp;edgeId_RightBack,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;back&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Right,&nbsp;edgeId_TopRight,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;top&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Right,&nbsp;edgeId_FrontRight,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;front&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishLoop(&nbsp;loopId_Right&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishFace(&nbsp;faceId_Right&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Left&nbsp;face</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Left,&nbsp;edgeId_BottomLeft,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;bottom&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Left,&nbsp;edgeId_LeftFront,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;front&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Left,&nbsp;edgeId_TopLeft,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;top&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Left,&nbsp;edgeId_BackLeft,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;&nbsp;<span style="color:green;">//&nbsp;other&nbsp;direction&nbsp;in&nbsp;back&nbsp;loop</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishLoop(&nbsp;loopId_Left&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishFace(&nbsp;faceId_Left&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;b.Finish();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;b;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Create&nbsp;a&nbsp;cylinder&nbsp;to&nbsp;subtract&nbsp;from&nbsp;the&nbsp;cube.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">BRepBuilder</span>&nbsp;CreateBrepVoid()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Naming&nbsp;convention&nbsp;for&nbsp;faces&nbsp;and&nbsp;edges:&nbsp;we&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;assume&nbsp;that&nbsp;x&nbsp;is&nbsp;to&nbsp;the&nbsp;left&nbsp;and&nbsp;pointing&nbsp;down,&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;y&nbsp;is&nbsp;horizontal&nbsp;and&nbsp;pointing&nbsp;to&nbsp;the&nbsp;right,&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;z&nbsp;is&nbsp;up.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilder</span>&nbsp;b&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BRepBuilder</span>(&nbsp;<span style="color:#2b91af;">BRepType</span>.Solid&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;The&nbsp;surfaces&nbsp;of&nbsp;the&nbsp;four&nbsp;faces.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Frame</span>&nbsp;basis&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Frame</span>(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;50,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;1,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;-1,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;1&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CylindricalSurface</span>&nbsp;cylSurf&nbsp;=&nbsp;<span style="color:#2b91af;">CylindricalSurface</span>.Create(&nbsp;basis,&nbsp;40&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;top1&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByNormalAndOrigin(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;1&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;100&nbsp;)&nbsp;);&nbsp;&nbsp;<span style="color:green;">//&nbsp;normal&nbsp;points&nbsp;outside&nbsp;the&nbsp;cylinder</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;bottom1&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByNormalAndOrigin(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;1&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;0&nbsp;)&nbsp;);&nbsp;<span style="color:green;">//&nbsp;normal&nbsp;points&nbsp;inside&nbsp;the&nbsp;cylinder</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;the&nbsp;four&nbsp;faces</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;frontCylFaceId&nbsp;=&nbsp;b.AddFace(&nbsp;<span style="color:#2b91af;">BRepBuilderSurfaceGeometry</span>.Create(&nbsp;cylSurf,&nbsp;<span style="color:blue;">null</span>&nbsp;),&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;backCylFaceId&nbsp;=&nbsp;b.AddFace(&nbsp;<span style="color:#2b91af;">BRepBuilderSurfaceGeometry</span>.Create(&nbsp;cylSurf,&nbsp;<span style="color:blue;">null</span>&nbsp;),&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;topFaceId&nbsp;=&nbsp;b.AddFace(&nbsp;<span style="color:#2b91af;">BRepBuilderSurfaceGeometry</span>.Create(&nbsp;top1,&nbsp;<span style="color:blue;">null</span>&nbsp;),&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;bottomFaceId&nbsp;=&nbsp;b.AddFace(&nbsp;<span style="color:#2b91af;">BRepBuilderSurfaceGeometry</span>.Create(&nbsp;bottom1,&nbsp;<span style="color:blue;">null</span>&nbsp;),&nbsp;<span style="color:blue;">true</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Geometry&nbsp;for&nbsp;the&nbsp;four&nbsp;semi-circular&nbsp;edges&nbsp;and&nbsp;two&nbsp;vertical&nbsp;linear&nbsp;edges</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;frontEdgeBottom&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:#2b91af;">Arc</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;90,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;50,&nbsp;40,&nbsp;0&nbsp;)&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;backEdgeBottom&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:#2b91af;">Arc</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;90,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;50,&nbsp;-40,&nbsp;0&nbsp;)&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;frontEdgeTop&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:#2b91af;">Arc</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;0,&nbsp;100&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;90,&nbsp;0,&nbsp;100&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;50,&nbsp;40,&nbsp;100&nbsp;)&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;backEdgeTop&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:#2b91af;">Arc</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;0,&nbsp;100&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;90,&nbsp;0,&nbsp;100&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;50,&nbsp;-40,&nbsp;100&nbsp;)&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;linearEdgeFront&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;90,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;90,&nbsp;0,&nbsp;100&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>&nbsp;linearEdgeBack&nbsp;=&nbsp;<span style="color:#2b91af;">BRepBuilderEdgeGeometry</span>.Create(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;0,&nbsp;0&nbsp;),&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;0,&nbsp;100&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;the&nbsp;six&nbsp;edges</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;frontEdgeBottomId&nbsp;=&nbsp;b.AddEdge(&nbsp;frontEdgeBottom&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;frontEdgeTopId&nbsp;=&nbsp;b.AddEdge(&nbsp;frontEdgeTop&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;linearEdgeFrontId&nbsp;=&nbsp;b.AddEdge(&nbsp;linearEdgeFront&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;linearEdgeBackId&nbsp;=&nbsp;b.AddEdge(&nbsp;linearEdgeBack&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;backEdgeBottomId&nbsp;=&nbsp;b.AddEdge(&nbsp;backEdgeBottom&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;backEdgeTopId&nbsp;=&nbsp;b.AddEdge(&nbsp;backEdgeTop&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Loops&nbsp;of&nbsp;the&nbsp;four&nbsp;faces</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;loopId_Top&nbsp;=&nbsp;b.AddLoop(&nbsp;topFaceId&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;loopId_Bottom&nbsp;=&nbsp;b.AddLoop(&nbsp;bottomFaceId&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;loopId_Front&nbsp;=&nbsp;b.AddLoop(&nbsp;frontCylFaceId&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilderGeometryId</span>&nbsp;loopId_Back&nbsp;=&nbsp;b.AddLoop(&nbsp;backCylFaceId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;coedges&nbsp;for&nbsp;the&nbsp;loop&nbsp;of&nbsp;the&nbsp;front&nbsp;face</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Front,&nbsp;linearEdgeBackId,&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Front,&nbsp;frontEdgeTopId,&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Front,&nbsp;linearEdgeFrontId,&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Front,&nbsp;frontEdgeBottomId,&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishLoop(&nbsp;loopId_Front&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishFace(&nbsp;frontCylFaceId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;coedges&nbsp;for&nbsp;the&nbsp;loop&nbsp;of&nbsp;the&nbsp;back&nbsp;face</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Back,&nbsp;linearEdgeBackId,&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Back,&nbsp;backEdgeBottomId,&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Back,&nbsp;linearEdgeFrontId,&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Back,&nbsp;backEdgeTopId,&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishLoop(&nbsp;loopId_Back&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishFace(&nbsp;backCylFaceId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;coedges&nbsp;for&nbsp;the&nbsp;loop&nbsp;of&nbsp;the&nbsp;top&nbsp;face</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Top,&nbsp;backEdgeTopId,&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Top,&nbsp;frontEdgeTopId,&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishLoop(&nbsp;loopId_Top&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishFace(&nbsp;topFaceId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;coedges&nbsp;for&nbsp;the&nbsp;loop&nbsp;of&nbsp;the&nbsp;bottom&nbsp;face</span>
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Bottom,&nbsp;frontEdgeBottomId,&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.AddCoEdge(&nbsp;loopId_Bottom,&nbsp;backEdgeBottomId,&nbsp;<span style="color:blue;">false</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishLoop(&nbsp;loopId_Bottom&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;b.FinishFace(&nbsp;bottomFaceId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;b.Finish();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;b;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;app&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;app.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Execute&nbsp;the&nbsp;BrepBuilder&nbsp;methods.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilder</span>&nbsp;brepBuilderSolid&nbsp;=&nbsp;CreateBrepSolid();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BRepBuilder</span>&nbsp;brepBuilderVoid&nbsp;=&nbsp;CreateBrepVoid();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Solid</span>&nbsp;cube&nbsp;=&nbsp;brepBuilderSolid.GetResult();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Solid</span>&nbsp;cylinder&nbsp;=&nbsp;brepBuilderVoid.GetResult();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Determine&nbsp;their&nbsp;Boolean&nbsp;difference.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Solid</span>&nbsp;difference&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">BooleanOperationsUtils</span>.ExecuteBooleanOperation(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cube,&nbsp;cylinder,&nbsp;<span style="color:#2b91af;">BooleanOperationsType</span>.Difference&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">GeometryObject</span>&gt;&nbsp;list&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">GeometryObject</span>&gt;();
&nbsp;&nbsp;&nbsp;&nbsp;list.Add(&nbsp;difference&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tr&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tr.Start(&nbsp;<span style="color:#a31515;">&quot;Create&nbsp;a&nbsp;DirectShape&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;direct&nbsp;shape.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DirectShape</span>&nbsp;ds&nbsp;=&nbsp;<span style="color:#2b91af;">DirectShape</span>.CreateElement(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_GenericModel&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ds.SetShape(&nbsp;list&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tr.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
}
</pre>

Note that it’s much easier to use `GeometryCreationUtilities` functions to create extrusions than it is to use `BRepBuilder`, unless the whole point of this exercise is to use the latter.

As said, using `BRepBuilder` to 'manually' construct geometry is very inconvenient and error-prone, and that isn’t its purpose.

Many thanks to my colleagues Ryuji Ogasawara, Angel Velez, Boris Shafiro and John Mitchell for sharing, correcting, and commenting on this nice example!

I added it 
to [The Building Coder Samples](https://github.com/jeremytammik/the_building_coder_samples) 
[release 2018.0.136.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.136.0) in
the new [module CmdBrepBuilder.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdBrepBuilder.cs).
