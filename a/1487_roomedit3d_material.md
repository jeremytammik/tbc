<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

<code></code>

Connecting Desktop and Cloud #RTCEUR Material @RTCEvents @AutodeskForge #revitapi @AutodeskRevit #aec #bim

I am in the last stages of preparing my presentation this afternoon on connecting the desktop and the cloud for the RTC Revit Technology Conference Europe. For your and the audience's convenience, here are the materials I am presenting, and some of the main links to further information &ndash; Handout &ndash; Slide deck &ndash; Samples Connecting Desktop and Cloud...

-->

### Connecting Desktop and Cloud RTC Material

I am in the last stages of preparing my presentation this afternoon on connecting the desktop and the cloud for 
the [RTC Revit Technology Conference Europe](http://www.rtcevents.com/rtc2016eur).

For your and the audience's convenience, here are the materials I am presenting, and some of the main links to further information:

- [Slide deck](/a/doc/revit/rtc/2016/doc/s1_4_pres_connect_desktop_cloud_jtammik.pdf)
- [Handout document](/a/doc/revit/rtc/2016/doc/s1_4_hand_connect_desktop_cloud_jtammik.pdf)
- [Recording (1h 15min)](https://youtu.be/XJ3OLsOeeUc)

<center>
<br/>
<iframe width="480" height="270" src="https://www.youtube.com/embed/XJ3OLsOeeUc?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

#### <a name="1"></a>Samples Connecting Desktop and Cloud

Here is a summary overview of the samples discussed.

Each consists of two components, a C# .NET Revit API desktop add-in and a web server.

Each of them lives in an own GitHub repository with its own documentation pointing to more detailed underlying research and implementation steps discussed in sequences of blog posts:

- [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) and the [roomeditdb](https://github.com/jeremytammik/roomedit) CouchDB database and web server demonstrating real-time round-trip graphical editing of furniture family instance location and rotation plus textual editing of element properties in a simplified 2D SVG representation of the 3D BIM.
- [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) and the [fireratingdb](https://github.com/jeremytammik/firerating) node.js MongoDB web server demonstrating real-time round-trip editing of Revit element shared parameter values stored in a globally accessible mongolab-hosted db.
- [Roomedit3dApp](https://github.com/jeremytammik/Roomedit3dApp) and the first [roomedit3d](https://github.com/jeremytammik/roomedit3d) Forge Viewer extension demonstrating translation of BIM elements in the viewer and updating the Revit model in real time via a socket.io broadcast.
- [Roomedit3dv3](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d),
the new Forge-based sample, a viewer extension to demonstrate translation of BIM element instances in the viewer and updating the Revit model in real time via a `socket.io` broadcast, adding the option to select any Revit model hosted on A360, again using the [Roomedit3dApp](https://github.com/jeremytammik/Roomedit3dApp) Revit add-in.
    - [GitHub](https://github.com/jeremytammik/roomedit3dv3)
    - [Live sample URL](https://roomedit3dv3.herokuapp.com)
    - [Discussion and first demo recording](http://thebuildingcoder.typepad.com/blog/2016/10/roomedit3dv3-up-and-running-with-demo-recording.html)

#### <a name="2"></a>Other Topics

- [Forge for BIM Programming](https://github.com/jeremytammik/forge_bim_programming)
&ndash; [online presentation](http://jeremytammik.github.io/forge_bim_programming)
- [Requesting input on Revit I/O](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28b)
- [LmvNav](https://calm-inlet-4387.herokuapp.com)

<center>
<img src="/p/2016/2016-10-20_rtc_ah/669_anthony_hauck.jpg" alt="Anthony Hauck RTC keynote" width="400">
</center>


