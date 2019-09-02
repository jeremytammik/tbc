<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Get Preview Image from Revit API
  https://forums.autodesk.com/t5/revit-api-forum/get-preview-image-from-revit-api/m-p/8985870

- How to bind a Shared Parameter to elements of both Type and Instance
  https://stackoverflow.com/questions/57653886/how-to-bind-a-shared-parameter-to-elements-of-both-type-and-instance


twitter:

Parameters and preview images in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...


linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Parameters and Preview Images





####<a name="3"></a> Shared Parameter is Either Type or Instance

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
