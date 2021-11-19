<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- FormIt and its JavaScript API
  https://www.keanw.com/2021/11/autodesk-formit-and-its-javascript-api.html

- Matterlab FormIt plugin provides geographical context in Revit
  https://twitter.com/radugidei/status/1458370952652378113?s=20
  Radu Gidei @radugidei
  Replying to @keanw
  Nice one and great intro to FormIt API! Look forward to the VASA integration, sounds very cool! 
  Btw, some of our team are working on some FormIt stuff as we speak, some cool things  coming for the community! (cough docs cough)
  https://github.com/matterlab-co/FormIt-Context-Plugin

- Get translation and rotation for a FamilyInstance (Export)
  https://forums.autodesk.com/t5/revit-api-forum/get-translation-and-rotation-for-a-familyinstance-export/m-p/10758975

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### FormIt and Quaterions

####<a name="2"></a> FormIt API and Geographical Context

Things are happening
with [FormIt](https://formit.autodesk.com), a useful multi-platform architectural modelling, conceptual design and analysis tool.
With FormIt, architects can sketch, collaborate, analyze, and revise early-stage design concepts with BIM-based conceptual design.
Kean Walmsley took a closer look
at [FormIt and its JavaScript API](https://www.keanw.com/2021/11/autodesk-formit-and-its-javascript-api.html) and
describes hiow to get started with FormIt plugins.

As as erious example of what you can achieve,
the [Matterlab FormIt plugin provides geographical context in Revit](https://twitter.com/radugidei/status/1458370952652378113?s=20);
  Radu Gidei @radugidei
  Replying to @keanw
  Nice one and great intro to FormIt API! Look forward to the VASA integration, sounds very cool! 
  Btw, some of our team are working on some FormIt stuff as we speak, some cool things  coming for the community! (cough docs cough)
  https://github.com/matterlab-co/FormIt-Context-Plugin

https://github.com/matterlab-co/FormIt-Context-Plugin


<center>
<img src="img/" alt="" title="" width="100"/> <!-- 834 -->
</center>

####<a name="3"></a> 

####<a name="4"></a> Benchmarking Generic Any versus Count

where [Roman @Nice3point](https://github.com/Nice3point) points out:

Many thanks to Roman for this little suggestion and benchmark, and much more so for all his other outstanding work recently improving and maintaining RevitLookup!

####<a name="5"></a> Escape RevitLookup

Before we escape this topic, yet another little RevitLookup update bears mentioning:

Luiz Henrique [@ricaun](https://github.com/ricaun) Cassettari submitted
[pull request #115 to add keyboard escape to close form](https://github.com/jeremytammik/RevitLookup/pull/115):

Published in [RevitLookup release 2022.0.2.4](https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.2.4).

Many thanks to Luiz Henrique for catching this!

####<a name="6"></a> Transform and Quaternions

In
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [getting translation and rotation for a `FamilyInstance`](https://forums.autodesk.com/t5/revit-api-forum/get-translation-and-rotation-for-a-familyinstance-export/m-p/10758975),
Matthew [mhannonQ65N2](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/8377999) Hannon
shares a very nice and succinct explanation of quaternions and how they relate to Revit transformations:

For rotations, what you are trying to do is transform between two different representations of the 3d rotation group (aka the Special Orthogonal group of dimension 3, SO(3)). Revit provides rotations in the form of a 3x3 matrix whose three columns are the BasisX, BasisY, and BasisZ properties of Transform. This rotates vectors by standard matrix multiplication.

However, what you are using in SharpGLTF requires a quaternion. To be precise, it actually requires a unit quaternion, which is a quaternion of length 1. The equation for this is <i>x²+y²+z²+w²=1</i> (this is identical to the equation of the unit sphere in 4-dimensional space, known as the 3-sphere because its 'surface' is 3-dimensional). Furthermore, the product of two unit quaternions is also a unit quaternion. Unit quaternions rotate 3d vectors in a more mathematically complicated way that is actually faster to compute (I won't get into the details). A significant consequence of how unit quaternions are used to rotate vectors is that multiplying the unit quaternion by <i>-1</i> does not change how the vector is rotated. As such each 3d rotation can be represented by two different quaternions, <i>q</i>, and <i>-q</i>. As such, the group of unit quaternions is called a 'double cover' of SO(3).

Given an axis you wish to rotate about (as a unit vector, <b>v</b>) and the amount you wish to rotate, <b>θ</b>, (in radians), the process of constructing a unit quaternion for that rotation is straight forward. The x, y, and z components of the unit quaternion are the x, y, and z components of v, multiplied by sin(θ/2) and the fourth component is cos(θ/2).

If you don't know the axis and angle but only have the rotation matrix (i.e. a Revit Transform), there are algorithms for converting from a 3d rotation matrix to a quaternion, though I won't go into any here. Alternatively, it looks like in the latest version of SharpGLTF, AffineTransform has a constructor that takes a 4x4 matrix. To make such a matrix from a Revit Transform, the first 3 columns should be the BasisX, BasisY, and BasisZ of the Transform, with the fourth member of the column being zero, and the last column should be the Transform's origin, with the fourth member of the column being one.

Many thanks to Matthew for this very nice overview!
