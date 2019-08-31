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

This was a busy week discussing numerous issues and no blog post so far.

Let's clarify the confusing issue of using 

####<a name="2"></a> Discussion on the Face.Intersect method

Two [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) threads are discussing the stutus of
the [Face.Intersect  (Face) method](https://www.revitapidocs.com/2020/91f650a2-bb95-650b-7c00-d431fa613753.htm):

- [Surprising results from `Face.Intersect(Face)` method](https://forums.autodesk.com/t5/revit-api-forum/surprising-results-from-face-intersect-face-method/m-p/8992586)
- [Problems with `Intersect` method (`Face`)](https://forums.autodesk.com/t5/revit-api-forum/problems-with-intersect-method-face/m-p/8992566)
- [Get conection type and geometry between two elements from the model](https://forums.autodesk.com/t5/revit-api-forum/get-conection-type-and-geometry-between-two-elements-from-the/m-p/6465671)

**Question:** This isuue was mentioned in 2016, in the third thread liste4d above, but maybe it's worth bringing up again, as the issue hasn't resolved yet in 2018.

As far as I can tell, the `Face.Intersect(face)` method always returns `FaceIntersectionFaceResult.Intersecting`.


<center>
<img src="img/pl_volume_dynamo.png" alt="Dynamo volumes">  <!-- width="100" -->
</center>

<pre class="code">
</pre>



Here is the current state of things:

2019-08-28 16:10 16:20 adn_aec REVIT-58034 [API Face.Intersect(Face) returns true even if two faces don't intersect with each other]
2019-08-28 16:20 16:30 adn_aec REVIT-58034 [Improve documentation for API Face.Intersect(Face) to reflect what the function actually does]
2019-08-28 16:30 16:40 adn_aec REVIT-133819 Improve API Face.Intersect(Face) to actually intersect faces, not underlying surfaces]
2019-08-28 16:40 16:50 adn_aec REVIT-133627 [Face.Intersect returns false positives]


####<a name="3"></a> Shared Parameters are for either Types or Instances

From the StackOverflow question
on [how to bind a shared parameter to both type and instance elements](https://stackoverflow.com/questions/57653886/how-to-bind-a-shared-parameter-to-elements-of-both-type-and-instance):

**Question:** I create a Shared Parameter programatically. This works well. I can also bind that parameter to different element types (categories) like Windows or Doors. However, once that is done, I struggle to create a new binding to for example a room which is not a family type, and should be bound to instances instead.

Is there any way to use ONE single shared parameter and bind that to both types and instances?

**Answer:** I believe not. A shared parameter is bound to either types or instances, not both.

**Response:** Thank you for answering so quickly.

I now tried to do it manually as well, through the UI, and got the message that it has already been added, so you unfortunately seems to be right. I am probably not the first to say this, but that is just veird behaviour. It forces me to create two parameters which have the completely identical defintion to add them to different categories.

**Answer:** Yes.

You must take into consideration that the shared parameters functionality was added very early in the Revit lifecycle and was completely oriented toward end user interaction. At the time, the Revit API did not even exist, and was not planned for.

Possibly your use case is weird, from a BIM point of view. I am not an expert on the user interface side of things, so I cannot say.

On the other hand, [extensible storage](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23) was explicitly designed for API use.

If you do not explicitly need the end user or Revit to see and manipulate your data, e.g., for scheduling or other purposes, you should consider using extensible storage instead.

Neither Revit nor the end user will effectively see your extensible storage data.

Depending on your exact needs, this may be what you want or not.

####<a name="4"></a> Retrieve Element Parameters

<center>
<img src="img/pl_directshape_parameters.png" alt="DirectShape parameters">  <!-- width="100" -->
</center>

####<a name="4"></a> Hiding DirectShape Internal Face Edges

<center>
<img src="img/pl_volume_directshape.png" alt="DirectShape internal face edges">  <!-- width="100" -->
</center>

<center>
<img src="img/pl_volume_dynamo.png" alt="Dynamo volumes">  <!-- width="100" -->
</center>

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