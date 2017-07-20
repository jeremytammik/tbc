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

- [Incorrect face normal](https://forums.autodesk.com/t5/revit-api-forum/incorrect-face-normal/m-p/7108787)
  Family instance geometry in LCS or WCS
  Fair59 presents another brilliant solution, reminding me of the lessons learned 
  using [voodoo magic to retrieve global instance edges](http://thebuildingcoder.typepad.com/blog/2016/08/voodoo-magic-retrieves-global-instance-edges.html),
  including [snooping the family instance geometry](http://thebuildingcoder.typepad.com/blog/2016/08/voodoo-magic-retrieves-global-instance-edges.html#3) and
  also demonstrated by the [structural concrete setout point add-in](http://thebuildingcoder.typepad.com/blog/2016/08/voodoo-magic-retrieves-global-instance-edges.html#7).

- Another example describing the symptoms well:
  13047044 [Arcadis DEVR6076 - Revit API pointOnEdge]
  https://forums.autodesk.com/t5/revit-api-forum/edge-reference-of-a-family-instance/m-p/7088651

- family instance location on custom exporter and GetTransformed
  11271005 [Issues with Custom Export]

- 09754737 [Retrieval of Picked geometry faces from instance is untransformed?]

Picked Instance Face Geometry in LCS Versus WCS #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/pickfacecs

Depending on circumstances, interactively picked family instance geometry may be returned in the global Revit world coordinate system WCS, or in the family instance definition local coordinate system LCS.
A family instance may have its own non-empty solid, or use the symbol geometry. The symbol geometry requires keeping track of the instance transform to map it to the actual instance project location
&ndash; Question on Incorrect Face Normal
&ndash; Working Plane has no Effect
&ndash; Non-Picked Face Normals are Correct
&ndash; Solution &ndash; Detecting When to Use LCS versus WCS
&ndash; Retrieval of Picked Geometry Face from Instance is Untransformed...

--->

### Picked Instance Face Geometry in LCS Versus WCS

A number of people have run into issues retrieving geometry from interactively picked family instances.

Depending on circumstances, the geometry may be returned in the global Revit world coordinate system WCS, or in the family instance definition local coordinate system LCS.

My first encounter with that effect was
when [retrieving a solid from an element](http://thebuildingcoder.typepad.com/blog/2012/06/obj-model-exporter-take-one.html#7)
during the implementation of the OBJ exporter, then expanding that
to [handle elements with multiple solids](http://thebuildingcoder.typepad.com/blog/2012/07/obj-model-exporter-with-multiple-solid-support.html#3):

> The `GetSolid` helper method retrieves the first non-empty solid found for a given element. In case it is a family instance, it may have its own non-empty solid, in which case we use that. Otherwise we search the symbol geometry. If we use the symbol geometry, we might have to keep track of the instance transform to map it to the actual instance project location. Instead, we ask for transformed geometry to be returned, so the resulting solid is already in place...

I also used this approach in
the [structural concrete setout point add-in](http://thebuildingcoder.typepad.com/blog/2016/08/voodoo-magic-retrieves-global-instance-edges.html#7).

The issue is even more confusing when combined with interactive picking, and keeps coming up from time to  time, most recently in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread on
an [incorrect face normal](https://forums.autodesk.com/t5/revit-api-forum/incorrect-face-normal/m-p/7108787).

Once again, Fair59 presented a brilliant solution and explanation of this issue, making use of some of the lessons learned 
using [Voodoo magic to retrieve global instance edges](http://thebuildingcoder.typepad.com/blog/2016/08/voodoo-magic-retrieves-global-instance-edges.html)
and [snooping the family instance geometry](http://thebuildingcoder.typepad.com/blog/2016/08/voodoo-magic-retrieves-global-instance-edges.html#3):

- [Question on Incorrect Face Normal](#2)
- [Working Plane has no Effect](#3)
- [Non-Picked Face Normals are Correct](#4)
- [Solution &ndash; Detecting When to Use LCS versus WCS](#5)
- [Retrieval of Picked Geometry Face from Instance is Untransformed](#6)

#### <a name="2"></a>Question on Incorrect Face Normal

**Question:** I created two beams through different codes. Their FamilySymbols are the same. But their left faces' (marked as red in pic) normal are different!

<center>
<img src="img/incorrect_face_normal_test1.png" alt="Incorrect face normal" width="971">
</center>

The face normal of the left beam is the same as I expected, (-1,0,0). 

The face normal of the right beam is wrong and I don't know why. 

Attached is my [project](zip/incorrect_face_normal_project1.rvt) for your reference.


#### <a name="3"></a>Working Plane has no Effect

**Answer:** It seems that one beam is bound to a working plane, and the other one is unbound. Is this intended? This binding can influence the position of the FamilyInstance in WCS.

**Response:** Bounding to a working plane is not my intention.
Both beams are created using this method:

<pre>
  Instance = Doc.Create.NewFamilyInstance( line,
    FamilySymbol, Level, StructuralType.Beam)
</pre>

How can I avoid bounding to a working plane when creating? Why and how would a working plane influence the `FamilyInstance`? If I can't change the way of creating the right beam, how can I get the correct face normal like the left beam? Thanks!

**Answer:** You can try passing null for `Level`. Unfortunately, it's not explicitly mentioned in the documentation, but the `Level` parameter is optional. The beam, after all, it is a family; like every family, it has a local coordinate system. If you bind it to a plane, then its LCS may no longer be identical to WCS; its local Z axis will correspond to the Z axis of the reference plane. What you are seeing is the beam's face normal in LCS; that's why they differ, even if they point in the same WCS direction.

**Response:** I passed null for Level and the right beam was bound to no working plane this time. But both beams' faces' normal are the same as previous.


#### <a name="4"></a>Non-Picked Face Normals are Correct

**Answer:** The Revit API documentation `RevitAPI.chm` says this about the **PlanarFace.FaceNormal property**:

This property is the "face normal" vector, and thus should return a vector consistently pointing out of the solid that this face is a boundary for (if it is a part of a solid).
 
There is also another method, `Face.ComputeNormal`: It will always be oriented to point out of a solid that contains the face.

I downloaded your sample file and investigated the face normals. For each of the two Elements, there are six sides having `FaceNormal` values as expected, all pointing outwards. Could it be that you compare the faces by index, meaning the first face in the left element's face list is compared to the first face in the second one's face list? The geometry objects are not returned in order but randomized. As far as I can see, there is no problem at all.

**Response:** I understand your points and thank you. Below is my test code to calculate the face normal and snapshot of the result. I was not using index to get the face.

<pre class="code">
  <span style="color:#2b91af;">Reference</span>&nbsp;refFace&nbsp;=&nbsp;<span style="color:blue;">null</span>;
  <span style="color:blue;">while</span>(&nbsp;<span style="color:blue;">true</span>&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">try</span>
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;refFace&nbsp;=&nbsp;sel.PickObject(&nbsp;<span style="color:#2b91af;">ObjectType</span>.Face,
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;select&nbsp;a&nbsp;face&quot;</span>&nbsp;);
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;selectedElement&nbsp;=&nbsp;doc.GetElement(
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;refFace&nbsp;);
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">GeometryObject</span>&nbsp;selectedGeoObject&nbsp;=&nbsp;selectedElement
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetGeometryObjectFromReference(&nbsp;refFace&nbsp;);
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;selectedFace&nbsp;=&nbsp;selectedGeoObject&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Face</span>;
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PlanarFace</span>&nbsp;selectedPlanarFace&nbsp;=&nbsp;selectedFace&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">PlanarFace</span>;
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxUV</span>&nbsp;box&nbsp;=&nbsp;selectedFace.GetBoundingBox();
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UV</span>&nbsp;faceCenter&nbsp;=&nbsp;(&nbsp;box.Max&nbsp;+&nbsp;box.Min&nbsp;)&nbsp;/&nbsp;2;
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;computedFaceNormal&nbsp;=&nbsp;selectedFace
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ComputeNormal(&nbsp;faceCenter&nbsp;).Normalize();
   
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;faceNormal&nbsp;=&nbsp;selectedPlanarFace.FaceNormal;
   
  &nbsp;&nbsp;&nbsp;&nbsp;MessageBox.Show(&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">$&quot;computedFaceNormal:&nbsp;</span>{computedFaceNormal.ToString()}<span style="color:#a31515;">,&nbsp;&quot;</span>
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;faceNormal:&nbsp;{faceNormal.ToString()}&quot;</span>&nbsp;);
  &nbsp;&nbsp;}
  &nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;Autodesk.Revit.Exceptions.<span style="color:#2b91af;">OperationCanceledException</span>&nbsp;e&nbsp;)
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Cancelled;
  &nbsp;&nbsp;}
  }
</pre>

<center>
<img src="img/incorrect_face_normal_test2.png" alt="Incorrect face normal" width="721">
</center>

When you say, "there are six sides having FaceNormal values as expected", do you mean the left face's normal of the right beam is (-1,0,0)?

**Answer:** Yes. I just read the solid's faces via RevitLookup. For both of the elements, there were six PlanarFaces, each with perfect `FaceNormal` values. May it be that the selection function itself returns a false face? Seems to be the front face instead of the displayed lateral one.

**Response:** I added this code to display the area:

<pre class="code">
  MessageBox.Show(
  &nbsp;&nbsp;<span style="color:#a31515;">$&quot;computedFaceNormal:&nbsp;</span>{computedFaceNormal.ToString()}<span style="color:#a31515;">,&nbsp;&quot;</span>
  &nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;faceNormal:&nbsp;{faceNormal.ToString()},&nbsp;&quot;</span>
  &nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;Area:&nbsp;{selectedFace.Area.ToString()}&quot;</span>&nbsp;);
</pre>

I tested it again. The area is correct, but the face normal is not.

<center>
<img src="img/incorrect_face_normal_test3.png" alt="Incorrect face normal" width="721">
</center>

**Answer:** When getting the solids and their faces, I draw the normals as ModelLines, starting at the faces' centre points:

<center>
<img src="img/incorrect_face_normal_normals.png" alt="Incorrect face normal" width="400">
</center>

Everything looks correct, this way.
 
When I pick a face, I get this result with the left one:
 
<center>
<img src="img/incorrect_face_normal_normals_picked_left.png" alt="Incorrect face normal" width="400">
</center>

But I get this when picking the right one:

<center>
<img src="img/incorrect_face_normal_normals_picked_right.png" alt="Incorrect face normal" width="500">
</center>

What does it mean ?

In fact, the face returned is not transformed to the project context.

The solid resides around the 0/0/0 project origin.

I've drawn the face boundaries, too:

<center>
<img src="img/incorrect_face_normal_normals_picked_rightallsides.png" alt="Incorrect face normal" width="500">
</center>
 
Strange. No idea.

**Response:** I did further investigation. I found that I've added coping on the left beam before picking it. So, when I add coping on the right one it works right! Why?

#### <a name="5"></a>Solution &ndash; Detecting When to Use LCS versus WCS

**Answer:** Your comment on coping is the last piece of the puzzle.

When a family instance is

- cut,
- joined,
- coped
- and (apparently) has been copied

Revit has to calculate the solids of the instance "in situ" as it will be different from the solids from the family definition. So, the normal of the face will be relative to the project.
 
In all (??) other cases Revit treats the solids as "instances" of the solids from the family definition. And by some Revit-logic, when asked for `Face.ComputeNormal`, it gives the normal relative to the family. Quirkier still, it gives the `Face.Origin` in project coordinates.
 
So, with family instances that are not cut, joined or coped, you need to transform the faceNormal to project coordinates.

As you already have a reference to the face, you can easily test for this condition:

<pre class="code">
  refFace.ConvertToStableRepresentation(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;.Contains(&nbsp;<span style="color:#a31515;">&quot;INSTANCE&quot;</span>&nbsp;)
</pre>

So, add this to your code:

<pre class="code">
  <span style="color:blue;">if</span>(&nbsp;refFace.ConvertToStableRepresentation(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;.Contains(&nbsp;<span style="color:#a31515;">&quot;INSTANCE&quot;</span>&nbsp;)&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;trans&nbsp;=&nbsp;(&nbsp;selectedElement&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;).GetTransform();
   
  &nbsp;&nbsp;computedFaceNormal&nbsp;=&nbsp;trans.OfVector(&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;computedFaceNormal&nbsp;);
   
  &nbsp;&nbsp;faceNormal&nbsp;=&nbsp;trans.OfVector(&nbsp;faceNormal&nbsp;);
  }
</pre>

**Response:** Classic! Thank you all so much!

Ever so many thanks to Fair59 for coming to the rescue once again!


<!---

#### <a name="3"></a>

... getting an [edge reference of a family instance](https://forums.autodesk.com/t5/revit-api-forum/edge-reference-of-a-family-instance/m-p/7115555)

**Question:** I'm trying to create a tool that will host an adaptive component on an edge in the project environment. The tool should prompt for an adaptive family to host and then for the edge to host it on.
 
I created the following helper method:

<pre class="code">
  Function HostOnEdge(
    document As Document,
    familyTohost As FamilySymbol,
    edgeReference As Reference,
    normalizedParameter As Double) As FamilyInstance
  
    'create a family instance and get it's adaptive points
    Dim familyInstance As FamilyInstance = AdaptiveComponentInstanceUtils.CreateAdaptiveComponentInstance(document, familyTohost)
    Dim placementPoints As IList(Of ElementId) = AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(familyInstance)
  
    'create reference point
    Dim location As New PointLocationOnCurve(PointOnCurveMeasurementType.NormalizedCurveParameter, normalizedParameter, PointOnCurveMeasureFrom.Beginning)
    Dim pointOnEdge As PointOnEdge = document.Application.Create.NewPointOnEdge(edgeReference, location)
  
    'attach first adaptive point to ref point
    Dim firstPoint As ReferencePoint = TryCast(document.GetElement(placementPoints(0)), ReferencePoint)
    firstPoint.SetPointElementReference(pointOnEdge)
  
    Return familyInstance
  
  End Function
</pre>

I then call it from an external command as follows:
 
<pre class="code">
  . . .
  
  'select family to host
  Dim selectedElementId As ElementId = UiDocument.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select Element To Host:").ElementId
  
  'select edge
  Dim selectedEdge As Reference = UiDocument.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Edge)
  
  'get the selected elements family symbol
  Dim familyInstance As FamilyInstance = TryCast(Document.GetElement(selectedElementId), FamilyInstance)
  Dim familySymbol As FamilySymbol = familyInstance.Symbol
  
  'create hosted element
  HostOnEdge(Document, familySymbol, selectedEdge, 0.5)
  
  . . .
</pre>

If I select a system family like a floor edge or a wall edge as the host it works as expected. (see attached GIF) If i select an edge of a loaded family such as a beam an internal exception is thrown (snippet attached).

Additionally, if the I pick a loaded family that has been joined/cut/copped in the project the tool works as expected. 

What is the correct way to create a pointOnEdge on an edge of a family instance?

Attachment  hostExample.gif ‏622 KB
Attachment  exception.PNG ‏285 KB


**Answer:** The cause of your problem is that Revit has two ways of calculating solids, as explained in my answer to
the [incorrect face normal](https://forums.autodesk.com/t5/revit-api-forum/incorrect-face-normal/td-p/7108787).
 
In this case, I think it's an error in the Revit-API.  The perfect valid EdgeReference ( of the "SolidInstance") apparently can't be used in the  firstPoint.SetPointElementReference() method. I say perfect valid, because you can use the EdgeReference for the creation of a Dimension without a problem.
 
If you have a familyInstance that is cut, joined (or Coped) your code will work.

**Response:** Thanks for the tip. As suggested if the "host" family is joined/cut/copped by another element it works as expected. If not the exception is thrown.
--->


#### <a name="6"></a>Retrieval of Picked Geometry Face from Instance is Untransformed

The discussion above also helps understand this old case on how to transform picked element face geometry to WCS:

**Question:** I am currently picking faces from geometry that is likely to be inside of a linked DWG file.

From the reference, I access the element geometry like this:

<pre class="code">
  <span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;<span style="color:#2b91af;">Document</span>.GetElement(&nbsp;reference&nbsp;);
   
  <span style="color:#2b91af;">GeometryObject</span>&nbsp;go&nbsp;
  &nbsp;&nbsp;=&nbsp;e.GetGeometryObjectFromReference(&nbsp;reference&nbsp;);
</pre>

My problem is that the faces that are retrieved in this case are not transformed to the instance location.

**Answer:** I would imagine that if the element `e` is an instance, then:

1. You can query it for its geometry.
2. The geometry will contain a geometry instance.
3. The transform is provided by the `GeometryInstance.Transform` property.

**Response:** The problem is that I'm trying to select specific faces from within a DWG instance which has hundreds of faces.

So, while I can get all of the geometry from the element (transformed), I'm not sure if I can figure out which `Reference` or `GeometryObject` matches the selected face.

**Answer:** You can use the `Instance.GetTransform` method. That is at the element level.

**Response:** Yes, `Instance.GetTransform` would return the transform of the instance, but using that with the untransformed face from the pick would require me to transform everything that comes out of the face, such as normals, curvature, evaluated points, triangulation points, etc.

That sounds like a lot of work!

**Answer:** Unfortunately, I see other option for this. If you are interested in the edges and edge points, the curves can be transformed. The face itself cannot.
