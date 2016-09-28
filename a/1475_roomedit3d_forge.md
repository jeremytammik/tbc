<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

Roomedit3d Broadcast, Teigha BIM and @AutodeskForge Webinar 3 #revitapi #3dwebcoder @AutodeskRevit #aec #bim

My discussion with Philippe on integrating <code>roomedit3dv3</code> with the rest of his Forge node.js boilerplate samples continues, as does the Forge hackathon webinar series. Furthermore, the Open Design Alliance made an interesting Revit related announcement
&ndash; REST vs WebSocket and Roomedit3dv3 broadcast architecture
&ndash; Teigha BIM announcement
&ndash; Forge webinar series...

-->

### Roomedit3d Broadcast, Teigha BIM and Forge Webinar 3

My discussion with Philippe on
integrating [roomedit3dv3](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d) with the rest of
his [Forge node.js boilerplate samples](https://github.com/Autodesk-Forge/forge-boilers.nodejs) continues, as does the Forge hackathon webinar series.

Furthermore, the Open Design Alliance made an interesting Revit related announcement:

- [REST vs WebSocket and Roomedit3dv3 broadcast architecture](#2)
- [Teigha BIM announcement](#3)
- [Forge webinar series](#4)


#### <a name="2"></a>REST vs WebSocket and Roomedit3dv3 Broadcast Architecture 

[Philippe Leefsma](http://twitter.com/F3lipek) and I discussed how to communicate end user modifications made using the custom viewer extension back to the Revit add-in to update the BIM.

Philippe suggested:

> Putting all the code into a viewer extension to make it pretty easy to manage. I could add potentially useful features to my server if needed by your app.
 
**Question:** In my previous architecture, I used REST to notify the server from the client, and `socket.io` from the server to the rest of the world.
 
- Client viewer extension &rarr; REST &rarr; server &rarr; `socket.io` &rarr; desktop
 
Therefore, I did not just add a pure client viewer extension, but also fiddled around in the server a bit as well.
 
Should I try to put the `socket.io` broadcast directly into the viewer extension, and avoid the extra communication step?
 
- Client viewer extension &rarr; `socket.io` &rarr; desktop
 
Here is the architecture implementation that I used successfully for that last time around:
 
- [Viewer extension](https://github.com/jeremytammik/roomedit3d/blob/master/www/js/extensions/Roomedit3dTranslationTool.js)
- [Client notification to server](https://github.com/jeremytammik/roomedit3d/blob/master/www/js/roomedit3dapiclient.js)
- [Server broadcast request API endpoint](https://github.com/jeremytammik/roomedit3d/blob/master/routes/api/roomedit3d.js)
- [Server broadcast relay](https://github.com/jeremytammik/roomedit3d/blob/master/server.js#L54-L60)
 
**Answer:** You have to go from client to server if you want to broadcast a message to other connected clients, but it seems like you don't need to handle any very specific logic on the server; basically the server can expose a generic REST endpoint which will receive a message and dispatch it to all other clients except the one that initiated the request.
 
The server already implements a `SocketSvc` socket service which we can use for that.

There are two options how this can be implemented:

1. Similar to what you did last time: REST endpoint exposed by server, client calls POST with a payload containing message, server broadcast to all other clients.
2. Client uses socket to send message to server, server broadcasts message to all other clients.

In both cases I need to add a little piece of code to my server, so the feature is more generic than what you did last time. I will also add a SocketService on the client side, so it's easy to use.

Here is an illuminating read on a [REST vs WebSocket comparison and benchmarks](http://blog.arungupta.me/rest-vs-websocket-comparison-benchmarks).

Take a look at that and decide which is more suitable for your scenario.

Later: I just finished adding a thin wrapper around client side socket.
 
It is now wrapped in a 'service' making it avail application-wide through the client-side `ServiceManager` like this:

<pre class="prettyprint">
import ServiceManager from 'Services/SvcManager'
 
var svc = ServiceManager.getService('SocketSvc')
 
svc.broadcast('your-msg-id', {data...})
</pre>

All other clients but the initiator will receive the message.
 
An example of using this is provided in the [client App.js](https://github.com/Autodesk-Forge/forge-boilers.nodejs/blob/master/6%20-%20viewer%2Bserver%2Bdata-mng%2Bderivatives/src/client/App.js#L251).

Time for me to get going on implementing and using this, then.


#### <a name="3"></a>Teigha BIM Announcement

The [Open Design Alliance ODA](https://www.opendesign.com) is working on [Teigha BIM](https://www.opendesign.com/products/teigha-bim) to provide access to Revit model data and family definitions in `RVT` and `RFA` files without a running session of Revit.

Here is the [ODA Teigha BIM announcement](https://www.opendesign.com/news/2016/september/oda-announces-teigha%C2%AE-bim) and
an [Architosh article](http://architosh.com/2016/09/oda-accelerates-bim-interoperability-with-native-revit-file-support) on its current status.



#### <a name="4"></a>Forge Webinar Series

Yesterday, Adam Nagy presented the third session in the ongoing Forge webinar series, on the Model Derivative API.

Here are the recordings, presentations and support material of the sessions held so far:

- September 20 &ndash; [Introduction to Autodesk Forge and the Autodesk App Store](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-autodesk-forge-and-the-autodesk-app-store.html)
- September 22 &ndash; [Introduction to OAuth and Data Management API](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-oauth-and-data-management-api.html)
&ndash; on [OAuth](https://developer.autodesk.com/en/docs/oauth/v2/overview)
and [Data Management API](https://developer.autodesk.com/en/docs/data/v2/overview), providing token-based authentication, authorization and a unified and consistent way to access data across A360, Fusion 360, and the Object Storage Service.
- September 27 &ndash; [Introduction to Model Derivative API](http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-model-derivative-api.html)
&ndash; on the [Model Derivative API](https://developer.autodesk.com/en/docs/model-derivative/v2/overview) that enables users to represent and share their designs in different formats and extract metadata.
 
Upcoming sessions continue during the remainder of
the [Autodesk App Store Forge and Fusion 360 Hackathon](http://autodeskforge.devpost.com) until the end of October:

- September 29 &ndash; [Viewer API](https://developer.autodesk.com/en/docs/viewer/v2/overview) &ndash; 
formerly part of the 'View and Data API', a WebGL-based, JavaScript library for 3D and 2D model rendering a CAD data from seed models, e.g., [AutoCAD](http://www.autodesk.com/products/autocad/overview), [Fusion 360](http://www.autodesk.com/products/fusion-360/overview), [Revit](http://www.autodesk.com/products/revit-family/overview), and many other formats.
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

<center>
<img src="img/forge_accelerator.png" alt="Forge &ndash; build the future of making things together" width="400">
</center>
