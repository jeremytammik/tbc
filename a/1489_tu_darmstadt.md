<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

<code></code>

Connecting Desktop and Cloud @ TUDa #RTCEUR @RTCEvents @AutodeskForge #revitapi @AutodeskRevit #aec #bim

The RTC Revit Technology Conference Europe and my stay here in Porto is nearing its end
&ndash; Wrapping up in Porto
&ndash; Connecting BIM and Cloud @ <b>TUDa</b>
&ndash; Creating a Revit add-in with one single click
&ndash; Connecting Revit and Forge in 55 minutes...

-->

### Revit API Connecting Desktop and Cloud @ TUDa

The [RTC Revit Technology Conference Europe](http://www.rtcevents.com/rtc2016eur) and
my stay here in Porto is nearing its end:

- [Wrapping up in Porto](#2)
- [Connecting BIM and Cloud @ <b>TUDa</b>](#3)
- [Creating a Revit add-in with one single click](#4)
- [Connecting Revit and Forge in 55 minutes](#5)


#### <a name="2"></a>Wrapping up in Porto

Here
are [some pictures](https://flic.kr/s/aHskK62RcB) of
the Ponte Luis I from the evening event and the morning walk to Alfândega do Porto:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157674181041821" title="Last Night and Morning at RTC"><img src="https://c4.staticflickr.com/6/5833/30400227011_e5d736383c_n.jpg" width="320" height="240" alt="Last Night and Morning at RTC"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

I have a rather busy time ahead, leaving early Sunday morning straight to Munich for the 
one-week [Forge accelerator](http://autodeskcloudaccelerator.com) workshop.

I am flying there with the Portuguese [transavia](https://www.transavia.com) airline.

I only mention that to point out that they have implemented the most perfect online check-in experience I ever used. This is the first time ever that I was completely and enthusiastically happy with it 100% from start to finish.

I wish every web site was like theirs.


#### <a name="3"></a>Connecting BIM and Cloud @ <b>TUDa</b>

The Forge accelerator in Munich is immediately followed by the next one-day workshop:

<center>
<span style="font-size: 120%; font-weight: bold">
[Connecting BIM and cloud @ TUDa](http://www.bim.tu-darmstadt.de)
</span>
</center>

I lead that one all on my own
at [Technische Universität Darmstadt](http://www.tu-darmstadt.de),
[Institut für Numerische Methoden und Informatik im Bauwesen](http://www.iib.tu-darmstadt.de),
the institute for numerical methods and computer science in the construction industry at the technical university in the very pleasant German city [Darmstadt](https://en.wikipedia.org/wiki/Darmstadt), in Hessen, just south of Frankfurt.

<center>
<img src="img/logo_tuda_150x309.png" alt="Technische Universität Darmstadt" width="309">
</center>

The agenda is simple:

- 10:45-12:15 presentation &ndash; [Connecting desktop and  cloud](http://thebuildingcoder.typepad.com/blog/2016/10/connecting-desktop-and-cloud-at-rtc-material.html)
- 13:00-17:00 hands-on workshop with two topics:
    - Revit API &ndash; [creating a Revit add-in with one single click](#4)
    - Connecting BIM with the cloud &ndash; [connecting Revit and Forge in 55 minutes](#5)


#### <a name="4"></a>Creating a Revit Add-In with One Single Click

I can create a new Revit add-in DLL, compile, load and execute it with one single click or two using
the [Visual Studio Revit add-in wizards](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20),
and I demonstrate that live in a handful of seconds.

Obviously, if everything were as simple as one single click, life would be extremely boring and no challenge at all.

It makes things a lot more interesting if you understand what is going on under the hood.

That is easily achieved by working through
the [Revit API getting started material](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2),
looking at the basic installation sections of
the [Revit API Developer Guide](http://help.autodesk.com/view/RVT/2016/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42),
and installing the Revit SDK from
the [Revit Developer Centre](http://www.autodesk.com/developrevit).

With that material in hand, you will understand and be able to make good and exciting use of the single-click functionality provided by the [Visual Studio Revit add-in wizards](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20).

The most effective way to get started programming with the Revit API is to work through the step-by-step instructions provided by the *DevTV* and *My First Revit Plugin* video tutorials.

- [DevTV &ndash; Introduction to Revit Programming](https://www.youtube.com/watch?v=l8dQxoAjSP8)
- [DevTV &ndash; Introduction to Revit Programming Part 2](https://www.youtube.com/watch?v=zL8pQRJbcyA)
- [My First Revit Plug-in](http://www.autodesk.com/myfirstrevitplugin) tutorial

After that, you can dive in deeper by working through the
self-documenting [ADN Revit API Training Labs](https://github.com/ADN-DevTech/RevitTrainingMaterial).
They consist of a series of hands-on lab exercises in both C# and VB.NET accompanied by detailed step-by-step training instruction documents.

Visit the [Revit Developer Centre](http://www.autodesk.com/developrevit) to 
download the Revit SDK containing documentation and samples:

- Install the Revit API help file <code>RevitAPI.chm</code> as a desktop shortcut and keep at hand at all times, or refer to the [online Revit API documentation at www.revitapidocs.com](http://www.revitapidocs.com).
- Install the SDK samples solution <code>SDKSamples.sln</code> for global source code searches.
- Install the <code>RvtSamples</code> external application to launch and debug the SDK samples.
- For more in-depth training, work through the [ADN Revit API Training Labs](https://github.com/ADN-DevTech/RevitTrainingMaterial).
- For the developer guide, visit [Revit Online Help](http://www.autodesk.com/revitapi-help) &gt; Developers
([2016](http://help.autodesk.com/view/RVT/2016/ENU)
[2015](http://help.autodesk.com/view/RVT/2015/ENU))
    - The Revit SDK provides the basic Revit API help file documentation in <code>RevitAPI.chm</code>.
    - The Revit API Developer Guide provides in-depth explanations.

I already wrote similar instructions
for [preparing for a hands-on Revit API training](http://thebuildingcoder.typepad.com/blog/2012/01/preparing-for-a-hands-on-revit-api-training.html) back in 2012 which are also still absolutely relevant.


#### <a name="5"></a>Connecting Revit and Forge in 55 Minutes

Connecting Revit and Forge is extremely simple, thanks to the large amount of existing boiler-plate code available, mainly in the
GitHub [Developer-Autodesk](https://github.com/Developer-Autodesk)
and [Autodesk-Forge](https://github.com/Autodesk-Forge) collections.

We take a closer look at
the [roomedit3dv3](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d) sample enabling a super simple minimal round-trip editing operation in the Forge viewer sending back a trivial transformation to be applied to a single Revit BIM element via socket.io.

This web server application is based
on [Philippe Leefsma](http://twitter.com/F3lipek)'s
[`forge-boilers.nodejs` node.js-based boilerplate projects](https://github.com/Autodesk-Forge/forge-boilers.nodejs) for
the [Autodesk Forge Web Services APIs](http://forge.autodesk.com).

It also includes a viewer extension for selecting and translating elements based on Philippe's 
[Viewing.Extension.Transform viewer extension](https://github.com/Developer-Autodesk/library-javascript-viewer-extensions/tree/master/src/Viewing.Extension.Transform),
from his
huge [library of JavaScript viewer extensions](https://github.com/Developer-Autodesk/library-javascript-viewer-extensions).

All in all, I cannot have added much more than one or a couple of dozen lines of code to the boilerplate provided by Philippe.

Here is the full detailed description of all my research and implementation efforts right from the start:

- [Introduction and first search for a starting point](http://thebuildingcoder.typepad.com/blog/2016/09/roomedit3d-update-for-connecting-desktop-and-forge.html)
- [The Birth of Roomedit3dv3](http://thebuildingcoder.typepad.com/blog/2016/09/the-birth-of-roomedit3dv3-and-forge-webinar-series.html#2)
- [Roomedit3dv3 Broadcast Architecture](http://thebuildingcoder.typepad.com/blog/2016/09/roomedit3d-broadcast-teigha-bim-and-forge-webinar-3.html#2)
- [Roomedit3dv3 Transform Viewer Extension](http://thebuildingcoder.typepad.com/blog/2016/09/warning-swallower-and-roomedit3d-viewer-extension.html#3)
- [Retrieving and Broadcasting the Roomedit3dv3 Translation](http://thebuildingcoder.typepad.com/blog/2016/10/retrieving-and-broadcasting-the-roomedit3dv3-translation.html)
- [Roomedit3dv3 Up and Running with Demo Recording](http://thebuildingcoder.typepad.com/blog/2016/10/roomedit3dv3-up-and-running-with-demo-recording.html)
- [Recording of the complete *Connecting Desktop and Cloud* presentation](http://thebuildingcoder.typepad.com/blog/2016/10/connecting-desktop-and-cloud-at-rtc-material.html)

Let's see whether (and hope that) I manage to package this in a suitable format for a 55-minute hands-on lab in the coming week.

