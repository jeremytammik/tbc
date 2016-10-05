<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

  
Roomedit3dv3 Up and Running with Demo Recording @AutodeskForge #revitapi #3dwebcoder @AutodeskRevit #aec #bim @RTCEvents

I completed the <code>roomedit3dv3</code> project
&ndash; Enhancements to the Forge Node.js boilerplate code
&ndash; RoomEditorApp Revit add-in update
&ndash; Roomedit3dv3 demo recording
&ndash; Samples connecting desktop and cloud
&ndash; Forge Webinar Series 5 &ndash; Design Automation API...

-->

### Roomedit3dv3 Up and Running with Demo Recording

I completed the [roomedit3dv3](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d) project:

- [Enhancements to the Forge Node.js boilerplate code](#2)
- [RoomEditorApp Revit add-in update](#3)
- [Roomedit3dv3 demo recording](#4)
- [Samples connecting desktop and cloud](#5)

Before I get to that, I should also mention that
the [Forge webinar series](http://autodeskforge.devpost.com/details/webinars) session 5
is being held today, by Albert Szilvasy, on the Design Automation API.

You can register by [clicking here](https://global.gotowebinar.com/join/134416283).

#### <a name="1"></a>Forge Webinar Series 5 &ndash; Design Automation API

Recordings, presentations and support material of the past sessions are available for viewing and download:

- September 20 &ndash; [Introduction to Autodesk Forge and the Autodesk App Store](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-autodesk-forge-and-the-autodesk-app-store.html)
- September 22 &ndash; [Introduction to OAuth and Data Management API](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-oauth-and-data-management-api.html)
&ndash; on [OAuth](https://developer.autodesk.com/en/docs/oauth/v2/overview)
and [Data Management API](https://developer.autodesk.com/en/docs/data/v2/overview), providing token-based authentication, authorization and a unified and consistent way to access data across A360, Fusion 360, and the Object Storage Service.
- September 27 &ndash; [Introduction to Model Derivative API](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-model-derivative-api.html)
&ndash; on the [Model Derivative API](https://developer.autodesk.com/en/docs/model-derivative/v2/overview) that enables users to represent and share their designs in different formats and extract metadata.
- September 29 &ndash; [Introduction  to Viewer](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-viewer-api.html)
&ndash; the [Viewer](https://developer.autodesk.com/en/docs/viewer/v2/overview), formerly part of the 'View and Data API', is a WebGL-based JavaScript library for 3D and 2D model rendering of CAD models from seed files, e.g., [AutoCAD](http://www.autodesk.com/products/autocad/overview), [Fusion 360](http://www.autodesk.com/products/fusion-360/overview), [Revit](http://www.autodesk.com/products/revit-family/overview) and many other formats.

Upcoming sessions continue during the remainder of
the [Autodesk App Store Forge and Fusion 360 Hackathon](http://autodeskforge.devpost.com) until the end of October:

- October 4 &ndash; [Design Automation API](https://developer.autodesk.com/en/docs/design-automation/v2/overview) &ndash; formerly known as 'AutoCAD I/O', run scripts on design files.
- October 6 &ndash; [BIM360](https://developer.autodesk.com/en/docs/bim360/v1/overview) &ndash; develop apps that integrate with BIM 360 to extend its capabilities in the construction ecosystem.
- October 11 &ndash; [Fusion 360 Client API](http://help.autodesk.com/view/NINVFUS/ENU/?guid=GUID-A92A4B10-3781-4925-94C6-47DA85A4F65A) &ndash; an integrated CAD, CAM, and CAE tool for product development, built for the new ways products are designed and made.
- October 13 &ndash; Q&A on all APIs.
- October 20 &ndash; Q&A on all APIs.
- October 27 &ndash; Submitting a web service app to Autodesk App store.

Quick access links:

- For API keys, go to [developer.autodesk.com](https://developer.autodesk.com)
- For code samples, go to [github.com/Developer-Autodesk](https://github.com/Developer-Autodesk)
 
Feel free to contact us at [forgehackathon@autodesk.com](mailto:forgehackathon@autodesk.com) at any time with any questions.

Back to the room editor:

<center>
<img src="img/roomedit3dv3_broadcast.png" alt="Roomedit3dv3 Forge extension in action" width="385">
</center>

#### <a name="2"></a>Enhancements to the Forge Node.js Boilerplate Code

Roomedit3dv3 builds on [Philippe Leefsma](http://twitter.com/F3lipek)'s
[Forge node.js boilerplate samples](https://github.com/Autodesk-Forge/forge-boilers.nodejs),
adding a few simple enhancements in order to demonstrate interactive movement of BIM element instances in the Forge viewer and updating the Revit model accordingly in real time via a `socket.io` broadcast.

The additional enhancements include:

- [Adding a viewer `transform` extension](http://thebuildingcoder.typepad.com/blog/2016/09/warning-swallower-and-roomedit3d-viewer-extension.html#3).
- [Retrieving and broadcasting the affected element `UniqueId` and translation](http://thebuildingcoder.typepad.com/blog/2016/10/retrieving-and-broadcasting-the-roomedit3dv3-translation.html) via `socket.io`.

I created a new Forge app for the production version, set up its `PROD` environment and deployed to [Heroku](http://heroku.com) with no major issues.

Everything went well, as you can see below in my description of the [Revit add-in adaptation](#3) and [test run](#4).

Here is are all the nitty-gritty details on choosing a suitable starting point and adapting the boilerplate code for my needs:

- [Roomedit3d Update for Connecting Desktop and Forge](http://thebuildingcoder.typepad.com/blog/2016/09/roomedit3d-update-for-connecting-desktop-and-forge.html)
- [More Roomedit3dv3 Starting Points](http://thebuildingcoder.typepad.com/blog/2016/09/forge-webinar-series-and-more-roomedit-starting-point-samples.html#2)
- [The Birth of Roomedit3dv3](http://thebuildingcoder.typepad.com/blog/2016/09/the-birth-of-roomedit3dv3-and-forge-webinar-series.html#2)
- [REST vs WebSocket and Roomedit3dv3 Broadcast Architecture](http://thebuildingcoder.typepad.com/blog/2016/09/roomedit3d-broadcast-teigha-bim-and-forge-webinar-3.html#2)
- [Roomedit3dv3 Transform Viewer Extension](http://thebuildingcoder.typepad.com/blog/2016/09/warning-swallower-and-roomedit3d-viewer-extension.html#3)
- [Retrieving and Broadcasting the Roomedit3dv3 Translation](http://thebuildingcoder.typepad.com/blog/2016/10/retrieving-and-broadcasting-the-roomedit3dv3-translation.html)

Now that the web server part is complete, let's test it from Revit:


#### <a name="3"></a>RoomEditorApp Revit Add-in Update

The `socket.io` broadcast is picked up by the [Roomedit3dApp](https://github.com/jeremytammik/Roomedit3dApp) Revit add-in and applied to the Revit model just like in previous versions.

All I had to do was update the `socket.io` URL.

I also added a check for the correct Revit project document and a valid element `UniqueId`, since the viewer extension might be running in any number of instances for various users at the same time, and no information is currently added to the socket.io broadcast to tell which Revit model is being edited by who.

Therefore, in the current implementation, you might be bombarded with millions of transform messages from all the thousands of different users playing with this simultaneously worldwide.

Thus the importance of checking whether the notification you just received really does apply and can be applied to your current document.

Here is the new `BimUpdater.Execute` method implementation that hopefully takes care of that:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Execute&nbsp;method&nbsp;invoked&nbsp;by&nbsp;Revit&nbsp;via&nbsp;the&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;external&nbsp;event&nbsp;as&nbsp;a&nbsp;reaction&nbsp;to&nbsp;a&nbsp;call&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;to&nbsp;its&nbsp;Raise&nbsp;method.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Execute(&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;a&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;0&nbsp;&lt;&nbsp;_queue.Count,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;why&nbsp;are&nbsp;we&nbsp;here&nbsp;with&nbsp;nothing&nbsp;to&nbsp;do?&quot;</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;a.ActiveUIDocument.Document;
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Ensure&nbsp;that&nbsp;the&nbsp;unique&nbsp;id&nbsp;refers&nbsp;to&nbsp;a&nbsp;valid</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;element&nbsp;from&nbsp;the&nbsp;current&nbsp;doucument.&nbsp;If&nbsp;not,&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;no&nbsp;need&nbsp;to&nbsp;start&nbsp;a&nbsp;transaction.</span>
 
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;uid&nbsp;=&nbsp;_queue.Peek().Item1;
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;uid&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;e&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;GetName()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">while</span>(&nbsp;0&nbsp;&lt;&nbsp;_queue.Count&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Tuple</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:#2b91af;">XYZ</span>&gt;&nbsp;task&nbsp;=&nbsp;_queue.Dequeue();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;Translating&nbsp;{0}&nbsp;by&nbsp;{1}&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;task.Item1,&nbsp;<span style="color:#2b91af;">Util</span>.PointString(&nbsp;task.Item2&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;task.Item1&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementTransformUtils</span>.MoveElement(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;e.Id,&nbsp;task.Item2&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

I tagged the 
new [Roomedit3dApp](https://github.com/jeremytammik/Roomedit3dApp)
version [release 2017.0.0.6](https://github.com/jeremytammik/Roomedit3dApp/releases/tag/2017.0.0.6).


#### <a name="4"></a>Roomedit3dv3 Demo Recording

<!--
"Video For Everybody" http://camendesign.com/code/video_for_everybody
<video controls="controls" poster="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2249fe3970c-800wi" width="640" height="360">
 <source src="http://thebuildingcoder.typepad.com/roomedit3dv3.mp4" type="video/mp4" />
 <source src="http://thebuildingcoder.typepad.com/roomedit3dv3.webm" type="video/webm" />
 <source src="http://thebuildingcoder.typepad.com/roomedit3dv3.ogv" type="video/ogg" />
 <img alt="roomedit3dv3 demonstration" src="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2249fe3970c-800wi" width="640" height="360" title="No video playback capabilities, please download the video below" />
</video>
<p>
 <strong>Download video:</strong> <a href="http://thebuildingcoder.typepad.com/roomedit3dv3.mp4">MP4 format</a> | <a href="http://thebuildingcoder.typepad.com/roomedit3dv3.ogv">Ogg format</a> | <a href="http://thebuildingcoder.typepad.com/roomedit3dv3.webm">WebM format</a>
</p>
-->

With everything up and running smoothly, I created
a [ten-minute demo recording](https://youtu.be/wNtVBcbfhSw) on 
the [Roomedit3dv3 Revit BIM Autodesk Forge Viewer Extension](https://youtu.be/wNtVBcbfhSw):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/wNtVBcbfhSw?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

To summarise, it demonstrates real-time round-trip editing of Revit BIM via the Autodesk Forge Viewer using
[Roomedit3dv3](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d), which implements
a Forge Viewer extension to move building elements and update the Revit BIM in real-time using `socket.io`.

You can easily deploy your own version of it to Heroku with one simple button click, cf.
the [instructions in the GitHub repository](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d#prerequisites-and-sample-setup),
or test run my own web personal server instance directly at [roomedit3dv3.herokuapp.com](https://roomedit3dv3.herokuapp.com).


#### <a name="5"></a>Further Samples Connecting Desktop and Cloud 

I continue testing and documenting the other samples connecting the desktop and the cloud for 
the [RTC Revit Technology Conference Europe](http://www.rtcevents.com/rtc2016eur) in Porto,
each consisting of two components, a C# .NET Revit API desktop add-in and a web server:

- [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) and
the [roomeditdb](https://github.com/jeremytammik/roomedit)
[CouchDB](https://couchdb.apache.org)
database and web server demonstrating real-time round-trip graphical editing of furniture family instance location and rotation plus textual editing of element properties in a simplified
2D [SVG](https://www.w3.org/Graphics/SVG/) representation of the 3D BIM.
- [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) and
the [fireratingdb](https://github.com/jeremytammik/firerating)
[node.js](https://nodejs.org)
[MongoDB](https://www.mongodb.com) web server demonstrating real-time round-trip editing of Revit element shared parameter values stored in
a globally accessible [mongolab](http://mongolab.com)-hosted db.
- [Roomedit3dApp](https://github.com/jeremytammik/Roomedit3dApp) and
the first [roomedit3d](https://github.com/jeremytammik/roomedit3d) Forge Viewer extension demonstrating translation of BIM elements in the viewer and updating the Revit model in real time via a 'socket.io' broadcast.
- The sample discussed above, adding the option to select any Revit model hosted
on [A360](https://a360.autodesk.com), again using 
the [Roomedit3dApp](https://github.com/jeremytammik/Roomedit3dApp) Revit add-in working with the 
new [roomedit3dv3](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d)
[Autodesk Forge](https://forge.autodesk.com)
[Viewer](https://developer.autodesk.com/en/docs/viewer/v2/overview) extension
to demonstrate translation of BIM element instances in the viewer and updating the Revit model in real time via a `socket.io` broadcast.

All is still looking good so far.

