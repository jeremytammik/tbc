<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

12177368 [创建剖面视图时遇到的问题 -- When you create a cross-sectional view of the problems encountered]

Flipping a Section View and @AutodeskForge Webinar 3 #revitapi #3dwebcoder @AutodeskRevit #aec #bim

I already created a starting point for the new roomedit3d incarnation last week, in its own repository. This week, I agreed with Philippe to retain the new project in an own branch within the Forge boilerplate code sample collection. Hopefully, that will simplify keeping my BIM sample synchronised with any future updates make to that. Now I need to get started implementing and testing the actual viewer extension functionality itself. My main topic today is something different and purely Revit API oriented, besides the next Forge session
&ndash; Flipping a section view
&ndash; Forge webinar series...

-->

### Flipping a Section View and Forge Webinar 3

I already created a starting point for the
new [roomedit3d](https://github.com/jeremytammik/roomedit3d) incarnation
last week, in its own
repository [roomedit3dv3](https://github.com/jeremytammik/roomedit3dv3).

However, please forget that right away.

This week, I agreed with Philippe to retain the new project in an own
dedicated [roomedit3d branch](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d) within his
original [Forge boilerplate code sample collection](https://github.com/Autodesk-Forge/forge-boilers.nodejs).

Hopefully, that will simplify keeping my BIM sample synchronised with any future updates make to that.

Now I need to get started implementing and testing the actual viewer extension functionality itself.

My main topic today is something different and purely Revit API oriented, besides the next Forge session:

- [Flipping a section view](#2)
- [Forge webinar series](#3)


#### <a name="2"></a>Flipping a Section View

Here is a recent interesting little case handled by Jim Jia and answered by Paolo Serra:

**Question:** I want to create a new model with a section view based on an existing one with the same cross-sectional view but a different crop box.

To do so, I perform, these steps:

1. Open the old model file, find the required cross-sectional view.

2. Create a new model within the same bounding box, using the old model view CropBox created like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;transform&nbsp;=&nbsp;view.CropBox.Transform;
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;box&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>();
&nbsp;&nbsp;box.Enabled&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;maxPoint&nbsp;=&nbsp;view.CropBox.Max;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;minPoint&nbsp;=&nbsp;view.CropBox.Min;
&nbsp;&nbsp;box.Transform&nbsp;=&nbsp;transform;
&nbsp;&nbsp;box.Max&nbsp;=&nbsp;maxPoint;
&nbsp;&nbsp;box.Min&nbsp;=&nbsp;minPoint;
&nbsp;&nbsp;<span style="color:#2b91af;">ViewSection</span>&nbsp;viewSection&nbsp;=&nbsp;<span style="color:#2b91af;">ViewSection</span>.CreateSection(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;view.GetTypeId(),&nbsp;box&nbsp;);
</pre>

This works and I can create a cross-sectional view through this method, but in the opposite direction of the existing view, like this:

<center>
<img src="img/viewsection_flipped.png" alt="Section view flipped" width="515">
</center>

How can I flip the view direction to solve this problem?

**Answer:** A few days ago I was trying to do the same.

What worked for me was to reverse the `RightDirection` vector of the original view and recreate a transform to apply to the `BoundingBoxXYZ`.

You should also take into consideration the different transforms you might have in the two documents.

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;tr&nbsp;=&nbsp;view.CropBox.Transform;
 
&nbsp;&nbsp;tr.BasisX&nbsp;=&nbsp;-view.RightDirection;
&nbsp;&nbsp;tr.BasisY&nbsp;=&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisZ;
&nbsp;&nbsp;tr.BasisZ&nbsp;=&nbsp;tr.BasisX.CrossProduct(&nbsp;tr.BasisY&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;bb&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>();
&nbsp;&nbsp;bb.Transform&nbsp;=&nbsp;tr;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;min&nbsp;=&nbsp;view.CropBox.Min;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;max&nbsp;=&nbsp;view.CropBox.Max;
&nbsp;&nbsp;bb.Min&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;min.X,&nbsp;min.Y,&nbsp;0&nbsp;);
&nbsp;&nbsp;bb.Max&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;max.X,&nbsp;max.Y,&nbsp;-min.Z&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;vftId&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">ViewFamilyType</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsElementType()
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">ViewFamilyType</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.First(&nbsp;x&nbsp;=&gt;&nbsp;x.ViewFamily&nbsp;==&nbsp;<span style="color:#2b91af;">ViewFamily</span>.Section&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Id;
 
&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;newView&nbsp;=&nbsp;<span style="color:#2b91af;">ViewSection</span>.CreateSection(&nbsp;doc,&nbsp;vftId,&nbsp;bb&nbsp;);
</pre>

**Response:** Thanks a lot, it works!

Many thanks to Jim and Paolo for sharing this!


#### <a name="3"></a>Forge Webinar Series

Tomorrow we are presenting the third session in the ongoing Forge webinar series, on the Model Derivative API.

Support documentation and recordings from the first two sessions for your future reference and enjoyment:

- September 20 [Introduction to Autodesk Forge and the Autodesk App Store](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-autodesk-forge-and-the-autodesk-app-store.html)
- September 22 [Introduction to OAuth and Data Management API](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-oauth-and-data-management-api.html)
&ndash; on [OAuth](https://developer.autodesk.com/en/docs/oauth/v2/overview)
and [Data Management API](https://developer.autodesk.com/en/docs/data/v2/overview), providing token-based authentication, authorization and a unified and consistent way to access data across A360, Fusion 360, and the Object Storage Service.

Upcoming sessions continuing during the remainder of
the [Autodesk App Store Forge and Fusion 360 Hackathon](http://autodeskforge.devpost.com) until the end of October:

- September 27 &ndash; [Model Derivative API](https://developer.autodesk.com/en/docs/model-derivative/v2/overview) &ndash; enable users to represent and share their designs in different formats and extract metadata.
- September 29 &ndash; [Viewer API](https://developer.autodesk.com/en/docs/viewer/v2/overview) &ndash; 
formerly part of the 'View and Data API', a WebGL-based, JavaScript library for 3D and 2D model rendering a CAD data from seed models, e.g., [AutoCAD](http://www.autodesk.com/products/autocad/overview), [Fusion 360](http://www.autodesk.com/products/fusion-360/overview), [Revit](http://www.autodesk.com/products/revit-family/overview), and many other formats.
- October 4 &ndash; [Design Automation API](https://developer.autodesk.com/en/docs/design-automation/v2/overview) &ndash; formerly known as 'AutoCAD I/O', run scripts on design files.
- October 6 &ndash; [BIM360](https://developer.autodesk.com/en/docs/bim360/v1/overview) &ndash; develop apps that integrate with BIM 360 to extend its capabilities in the construction ecosystem.
- October 11 &ndash; [Fusion 360 Client API](http://help.autodesk.com/view/NINVFUS/ENU/?guid=GUID-A92A4B10-3781-4925-94C6-47DA85A4F65A) &ndash; an integrated CAD, CAM, and CAE tool for product development, built for the new ways products are designed and made.
- October 13 &ndash; Q&A on all APIs.
- October 20 &ndash; Q&A on all APIs.
- October 27 &ndash; Submitting a web service app to Autodesk App store.

<center>
<img src="img/forge_accelerator.png" alt="Forge &ndash; build the future of making things together" width="400">
</center>

