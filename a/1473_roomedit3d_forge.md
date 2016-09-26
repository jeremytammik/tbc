<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

The Birth of Roomedit3dv3 and Forge Webinar 2 #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

I made my choice on the starting point for the new <code>roomedit3d</code> incarnation, and the second Forge webinar was successfully held
&ndash; The birth of Roomedit3dv3
&ndash; Forge webinar session 2 on OAuth and the Data Management API...

-->

### The Birth of Roomedit3dv3 and Forge Webinar 2

I made my choice on the starting point for the new [roomedit3d](https://github.com/jeremytammik/roomedit3d) incarnation, and the second Forge webinar was successfully held:

- [The birth of Roomedit3dv3](#2)
- [Forge webinar series](#3)


#### <a name="2"></a>The Birth of Roomedit3dv3

So far, I evaluated two of Augusto's apps and Philippe's series of boilerplate sample apps as potential starting points for a new incarnation
of [roomedit3d](https://github.com/jeremytammik/roomedit3d):

- [data.management-nodejs-integration.box](https://github.com/Developer-Autodesk/data.management-nodejs-integration.box), demonstrating 3-legged OAuth access to models on A360 and transferring them back and forth between A360 and the [Box content management platform](https://www.box.com).
- [model.derivative-nodejs-box.viewer](https://github.com/Developer-Autodesk/model.derivative-nodejs-box.viewer), demonstrating access to models on Box and displaying them in the Forge Viewer.
- [forge-boilers.nodejs](https://github.com/Autodesk-Forge/forge-boilers.nodejs) demonstrating the suite of Forge APIs and components:
    - Authenticate and authorise the user &ndash; [Authentication (OAuth)](https://developer.autodesk.com/en/docs/oauth/v2)
    - Access and download a RVT project file from A360 &ndash; [Data Management API](https://developer.autodesk.com/en/docs/data/v2)
    - Translate and access its geometry and metadata &ndash; [Model Derivative API](https://developer.autodesk.com/en/docs/model-derivative/v2)
    - Display to the user &ndash; [Viewer](https://developer.autodesk.com/en/docs/viewer/v2)
    
Each of these is fully documented in its own GitHub repository, and the first two are briefly highlighted in Augusto's discussion
on [Box &amp; Forge](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/box-forge.html).

I also made some notes on them in my first search for a
suitable [roomedit3dv3 starting point](http://thebuildingcoder.typepad.com/blog/2016/09/roomedit3d-update-for-connecting-desktop-and-forge.html#4),
followed by [more roomedit3dv3 starting points](http://thebuildingcoder.typepad.com/blog/2016/09/forge-webinar-series-and-more-roomedit-starting-point-samples.html#2).

I finally decided to go for the latter, for the sake of completeness.
Besides including all the Forge APIs that I might possibly need, it also makes use of a full web development and deployment stack including
the [Webpack module bundler](https://webpack.github.io) and NPM packages to build and generate the frontend code.
On one hand, this requires an extra build step.
On the other, it directly supports [ES6 code](https://en.wikipedia.org/wiki/ECMAScript#ES6), since all the JavaScript is passed through [Babel](https://babeljs.io) via the webpack build and bundling step.

I forked Philippe's repository and renamed it
to [roomedit3dv3](https://github.com/jeremytammik/roomedit3dv3),
tagging this initial forked state as [release 0.0.0](https://github.com/jeremytammik/roomedit3dv3/releases/tag/0.0.0) in
order to track all further changes right from the start. 

So far, all I have done is rewrite the documentation, remove the simpler boilerplate code samples #1-#5, and test that the one I am using, #6, still is in a working state.
I now reached [release 0.0.4](https://github.com/jeremytammik/roomedit3dv3/releases/tag/0.0.4) just doing that.

I actually hope and expect that work to be the largest chunk, and that adapting the existing viewer extension to do its job in the new environment will be less work the documentation work already completed &nbsp; :-)

It is looking good, though, and you can check out the further details by looking at the [readme](https://github.com/jeremytammik/roomedit3dv3).

#### <a name="3"></a>Forge Webinar Series

Augusto Goncalves presented the second session in the ongoing Forge webinar series.

Again, support documentation and a recording have already been published for your future reference and enjoyment:

- September 20 [Introduction to Autodesk Forge and the Autodesk App Store](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-autodesk-forge-and-the-autodesk-app-store.html)
- September 22 [Introduction to OAuth and Data Management API](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-oauth-and-data-management-api.html)
&ndash; on [OAuth](https://developer.autodesk.com/en/docs/oauth/v2/overview)
and [Data Management API](https://developer.autodesk.com/en/docs/data/v2/overview), providing token-based authentication, authorization and a unified and consistent way to access data across A360, Fusion 360, and the Object Storage Service.

Here are the rest of the series coming up and continuing during the remainder of
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

