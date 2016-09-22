<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

Forge Webinar Series and Roomedit Starting Points #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

I am still in the initial steps of preparing for my upcoming presentations on connecting the desktop and the cloud and exploring more Forge sample starting points. At the same time, we are in the midst of the Forge and AppStore online hackathon webinars, with one down and nine more to come
&ndash; More Roomedit3dv3 starting points
&ndash; Forge webinar series...

-->

### Forge Webinar Series and Roomedit Starting Points

I am still in the initial steps
of [preparing for my upcoming presentations on connecting the desktop and the cloud](http://thebuildingcoder.typepad.com/blog/2016/09/roomedit3d-update-for-connecting-desktop-and-forge.html) and
exploring more Forge sample starting points.

At the same time, we are in the midst of
the [Forge and AppStore online hackathon webinars](http://thebuildingcoder.typepad.com/blog/2016/09/forge-and-appstore-online-hackathon-webinars.html) that
I mentioned last week, with one down and nine more to come:

- [More Roomedit3dv3 starting points](#2)
- [Forge webinar series](#3)

#### <a name="2"></a>More Roomedit3dv3 Starting Points

I mentioned Augusto's two nice up-to-date samples as potential starting points for my planned `roomedit3dv3` renovation of the ancient 2D `roomedit` and the initial Forge-based `roomedit3d` samples last week, discussing:

- The [forge-3leg.nodejs-template](https://github.com/augustogoncalves/forge-3leg.nodejs-template) demonstrating 3-legged OAuth for logging into and retrieving models from A360.
- The [model.derivative-nodejs-box.viewer](https://github.com/Developer-Autodesk/model.derivative-nodejs-box.viewer) using
the [Model Derivative API](https://developer.autodesk.com/en/docs/model-derivative/v2/overview) to
translate [Box](https://www.box.com) files and display them in
the [Viewer](https://developer.autodesk.com/en/docs/viewer/v2/overview).

Philippe also pointed out his more complete series of up-to-date [Forge boilerplate templates](https://github.com/Autodesk-Forge/forge-boilers.nodejs).

In Philippe's own words:

I'm polishing a new set of forge samples, which I also designed to be used as boilers that range from low to medium complexity.

Multiple projects can be deployed independently to Heroku:

1. viewer-offline
2. viewer-barebone
3. viewer+server
4. viewer+server+oss
5. viewer+server+oss+derivatives
6. viewer+server+data-mng+derivatives

You can also run the samples locally, just take care about matching the callback url that you specify for your forge app in the portal.
 
For example, I created an App called "Forge DEV" intended to test my local set up.
 
Callback URL: `http://localhost:3000/api/forge/callback/oauth`.
 
This should work as-is when you run the sample locally.
 
For Heroku deployment I created another forge App with callback URL `https://adsk-forge.herokuapp.com/api/forge/callback/oauth`.
 
When deploying to Heroku, I use `HOST_URL` = `https://adsk-forge.herokuapp.com`.
 
You can test the app at [adsk-forge.herokuapp.com](https://adsk-forge.herokuapp.com).


#### <a name="3"></a>Forge Webinar Series

Jim Quanci himself already presented the first webinar session two days ago, with full documentation and recording already up and published live for your future reference and enjoyment:

- September 20 &ndash; [Introduction to Autodesk Forge and the Autodesk App Store](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-autodesk-forge-and-the-autodesk-app-store.html)

Here are the rest of the series, starting later today and continuing during the remainder of
the [Autodesk App Store Forge and Fusion 360 Hackathon](http://autodeskforge.devpost.com) running until the end of October:

- September 22 &ndash; [OAuth](https://developer.autodesk.com/en/docs/oauth/v2/overview) and [Data Management API](https://developer.autodesk.com/en/docs/data/v2/overview) &ndash; token-based authentication, authorization and a unified and consistent way to access data across A360, Fusion 360, and the Object Storage Service.
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
