<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

twitter:

 the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

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

### Understanding the Face BoundingBoxUV

Today, let's look at a short summary of 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to understand the `BoundingBoxUV` of face](https://forums.autodesk.com/t5/revit-api-forum/how-to-understand-the-boundingboxuv-of-face/m-p/9374555):

**Question:** I am doing something with AVF, and have some problems with the `BoundingBoxUV` of face.

I use the following code to find the feature points in the bottom-left corner area of the face:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxUV</span>&nbsp;bb&nbsp;=&nbsp;face.GetBoundingBox();
&nbsp;&nbsp;<span style="color:#2b91af;">UV</span>&nbsp;pmid&nbsp;=&nbsp;0.5&nbsp;*&nbsp;(bb.Min&nbsp;+&nbsp;bb.Max);
</pre>

However, the result shows that these points refer to the upper-right corner area:

<center>
<img src="img/face_bounding_box_ll.png" alt="Face bounding box lower left" title="Face bounding box lower left" width="500"/> <!-- 1142 -->
</center>

I'm confused about this result! Can anyone can help me? Thanks in advance!

**Answer:** You need to know in which `XYZ` direction in 3D space the `UV` vector components `U` and `V` are pointing.

You can find out by using
the [Project method](https://www.revitapidocs.com/2020/802cc09b-d0a4-dfc5-8ca1-e8c5e8cd4ced.htm) to
project a `XYZ` point onto the face surface and seeing where it ends up.

There is probably some more direct way to query either the face or its underlying surface for the U and V directions in 3D space.

**Response:** Forgive me, I don't understand your method, but it inspired me to think of another way.

I use Dynamo to query the U and V directions of the face, and I am glad to share it:

<center>
<img src="img/face_bounding_box_uv_dyn.png" alt="Dynamo query for face U and V" title="Dynamo query for face U and V" width="800"/> <!-- 1506 -->
</center>

Many thanks to Bing Yongcao for sharing this solution!
