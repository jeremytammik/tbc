<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- high-performance optimisation using Revit API for outline for many elements (>100'000 items)
  e.g., for 54000 walls (after filtering) and 18000 pipes, leading to 972'000'000 operation.
  How can I get bounding box for several elements?
  https://stackoverflow.com/questions/63083938/revit-api-how-can-i-get-bounding-box-for-several-elements
  how to get element nearby specific element using bounding box if it is just outside(some distance apart) bounding box?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-get-element-nearby-specific-element-using-bounding-box-if/m-p/9741883

- set the clipped/unclipped state of the base and survey points in 2021.1
  https://autodesk.slack.com/archives/C0SR6NAP8/p1600379512087800
  Jacob Small:mega-thinking: 17 Sep at 23:51
Can anyone in Dev confirm this statement is still true?
The clipped/unclipped state of the base and survey points cannot be set via the API. You can pin them using the Element.Pinned property.
https://thebuildingcoder.typepad.com/blog/2012/11/survey-and-project-base-point.html
Jennifer (Xue) Li  5 days ago
We exposed a new property Clipped for Base Point in R2021.1. So starting from this version, you will have the ability to get/set clipped state for Survey Point. And for Project Base Point, the property is readonly and will always return false because we’ve removed the clipped state fro PBP.
:celebrate:
Jacob Small:mega-thinking:  5 days ago
Yay!!!!! Huge help thanks!

- BIM360 apps from German university startups now live
  [15 New Integrations with Autodesk Construction Cloud ](https://constructionblog.autodesk.com/15-integrations-autodesk-construction)
  two Startups from the Forge developer Universities:
  Gamma AR &ndash; RWTH Aachen 
  4d planner &ndash; TU Berlin 
  https://twitter.com/ADSK_Construct/status/1311699100312666113

- [Inventing Virtual Meetings of Tomorrow with NVIDIA AI Research](https://youtu.be/NqmMnjJ6GEg)
  [Nvidia Maxine Cloud-AI Video-Streaming Platform](https://developer.nvidia.com/maxine)
New AI breakthroughs in NVIDIA Maxine, cloud-native video streaming AI SDK, slash bandwidth use while make it possible to re-animate faces, correct gaze and animate characters for immersive and engaging meetings. Learn more: https://nvda.ws/3l9foIn
AI-based face recognition and reconstruction is used, enabling bandwidth reduction by transmitting only animated face keypoint data instead of the entire video keyframe information.

twitter:

with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

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

### High Performance Outline for many Elements


####<a name="2"></a> 


**Question:** 

**Answer:** 

####<a name="3"></a> 

####<a name="4"></a> 

####<a name="5"></a> 

<center>
<img src="img/openai_microscope.png" alt="OpenAI Microscope" title="OpenAI Microscope" width="445"/> <!-- 991 -->
</center>


