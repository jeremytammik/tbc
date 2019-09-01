<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


twitter:

Parameters and preview images in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...


linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Face Intersect Does Not Intersect Faces 

My work on setting up a new PC is nearing completion.

Let's also try to clarify the confusing issue of using the `Face.Intersect` method:



####<a name="2"></a> Discussion on the Face.Intersect method

Several [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) threads are discussing the stutus of
the [Face.Intersect (Face) method](https://www.revitapidocs.com/2020/91f650a2-bb95-650b-7c00-d431fa613753.htm):

- [Surprising results from `Face.Intersect(Face)` method](https://forums.autodesk.com/t5/revit-api-forum/surprising-results-from-face-intersect-face-method/m-p/8992586)
- [Problems with `Intersect` method (`Face`)](https://forums.autodesk.com/t5/revit-api-forum/problems-with-intersect-method-face/m-p/8992566)
- [Get conection type and geometry between two elements from the model](https://forums.autodesk.com/t5/revit-api-forum/get-conection-type-and-geometry-between-two-elements-from-the/m-p/6465671)
- [`Face` class `Intersect` method problem](https://forums.autodesk.com/t5/revit-api-forum/face-class-intersect-method-problem/m-p/7460720)

Let's try to summarise:

**Question:** This issue was mentioned in 2016, in the third thread listed above, but it is worth bringing up again, as the issue hasn't been resolved yet in 2018.

As far as I can tell, the `Face.Intersect(face)` method always returns `FaceIntersectionFaceResult.Intersecting`.

When I run the code below in a view with a single wall and single floor, each face to face test returns an intersection:

<center>
<img src="img/face_intersect_face_1.png" alt="Non-intersecting faces" width="100">
</center>

<pre class="code">
</pre>

**Answer:** Thank you for your report and reproducible case.

 

I see the same behaviour in Revit 2019 as well.

 

I added some code to The Building Coder samples to test and report in more depth:

 

https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/C...

 

I created the following disjunct floor and wall:

<center>
<img src="img/floor_wall_disjunct.png" alt="Disjunct floor and wall" width="100">
</center>

My sample code reports:

</pre>
  Floor has 7 faces.
  Wall has 6 faces.
  38 face-face intersections.
 </pre>

So, in fact, not every face-to-face test reports an intersection, because 7*6 equals 42.

Only the vast majority do &nbsp; :-)

I logged the issue *REVIT-133627 [Face.Intersect returns false positives]* with our development team to explore this further and provide an explanation.

This turned out to be a duplicate of an existing older issue, *REVIT-58034 [API Face.Intersect(Face) returns true even if two faces don't intersect with each other]*, and was therefore closed again.

The development team replied:

> We are aware of this issue.
This function does indeed not do what one expects.
At most, it computes intersections between the underlying (unbounded) surfaces, not the (bounded) faces lying in the surfaces.
As a first step, the documentation will be updated to reflect this fact.
Then, we'll see whether resources can be found to fully implement the face intersection functionality, or remove the incomplete functionality.

Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen adds:

I have previously plotted the intersections using the `ByRef` curve overload and found this to be the case, as explained in the thread 
on the [`Face` class `Intersect` method problem](https://forums.autodesk.com/t5/revit-api-forum/face-class-intersect-method-problem/m-p/7460720):

> Apparently, the intersecting faces are considered infinite with therefore many possible intersections beyond the range of the element itself. 

> In this image, the green lines are plotted using curves from the overload `Face.Intersect(ByVal Face, ByRef Curve)`:

<center>
<img src="img/face_intersect_wall_intersects.png" alt="Intinite face intersections between two walls" width="100">
</center>

> Hard to understand at first why they all exist, but once you trace along parallel to the faces, you can see all are valid.

In actual fact, an API user may prefer to find this form of intersection, rather than be told no intersection exists (due to the bounds of the face preventing it).

You can always compare points on the original face to those on the curve intersect result to check if there is an actual intersection.

However, if no results were given, then that would not be useful at all.

Probably, there should be a bound versus unbound option, perhaps.

Here is the current state of things:

REVIT-58034 [API Face.Intersect(Face) returns true even if two faces don't intersect with each other]
REVIT-58034 [Improve documentation for API Face.Intersect(Face) to reflect what the function actually does] &ndash; closed
REVIT-133627 [Face.Intersect returns false positives] &ndash; closed
REVIT-133819 [Improve API Face.Intersect(Face) to actually intersect faces, not underlying surfaces] &ndash; closed

Calculating the Intersection between to Rectangular Faces


[A Fast Triangle-Triangle Intersection Test](https://web.stanford.edu/class/cs277/resources/papers/Moller1997b.pdf)

[Efficient AABB/triangle intersection in C#](https://stackoverflow.com/questions/17458562/efficient-aabb-triangle-intersection-in-c-sharp)


####<a name="3"></a> 

**Question:** 

**Answer:** 

**Response:** 



<pre class="code">
</pre>

**Answer:** 

####<a name="4"></a> Copy as HTML Update

On the new computer I am now using Visual Studio 2017 innstead of Visual Studio 2015, and I ionce needed to install some kind
of [source code colourizer](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.36).

as describerd this spring 
[Installing PowerTools 2015 Copy HTML Markup](https://thebuildingcoder.typepad.com/blog/2019/04/close-doc-and-zero-doc-rvtsamples.html#4)

[Productivity Power Tools 2017/2019](https://marketplace.visualstudio.com/items?itemName=VisualStudioPlatformTeam.ProductivityPowerPack2017)

####<a name="4"></a> Visual Studio Revit Add-In Wizard Update

updated readme and set up for Visual Studio 2017


https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases/tag/2020.0.0.2