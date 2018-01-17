<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- learned the basics of CMake
  implemented a Windows DLL and EXE build system
  https://github.com/chtammik/YoAudio
  https://github.com/chtammik/YoAudioEditor
  
- 13782556 [SDK Available]
  AutoCAD and Revit SDK access
  
- 13747652 [How do I find a direction and a value of AreaLoad from its 3 ForceVectors?]
  https://forums.autodesk.com/t5/revit-api-forum/how-do-i-find-a-direction-and-a-value-of-areaload-from-its-3/m-p/7660527

- cnc export video https://www.youtube.com/watch?v=uNJ9RTppqoU
  updated for 2018

AreaLoad Force Direction, CMake, SDK Access #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/arealoaddirection

Here are some of the topics I dealt with in the last couple of days
&ndash; CMake and YoAudio
&ndash; AutoCAD and Revit SDK Access and Content
&ndash; Determining <code>AreaLoad</code> direction and value from its force vectors
&ndash; ExportCncFab 2018...

--->

### AreaLoad Force Direction, CMake, SDK Access

Here are some of the topics I dealt with in the last couple of days:

- [CMake and YoAudio](#2)
- [AutoCAD and Revit SDK Access and Content](#3)
- [Determining `AreaLoad` direction and value from its force vectors](#4)
- [ExportCncFab 2018](#5)


####<a name="2"></a>CMake and YoAudio

I spend some time last week learning the basics
of [CMake](https://cmake.org) to
help Chris set up a build system for
their [stand-alone YoAudio C++ audio environment](https://github.com/chtammik/YoAudio) and
the [accompanying YoAudioEditor](https://github.com/chtammik/YoAudioEditor).

CMake provides a pretty efficient way to handle multi-platform configuration, once figured out, as explained in the comparison
with [using `make` for cross platform compilation](https://stackoverflow.com/questions/9791127/using-make-for-cross-platform-compilation).

Once we had the build environment set up, we still needed to figure out how to access the required C++ symbols from within the DLL, either
by [exporting all symbols at once](https://stackoverflow.com/questions/225432/export-all-symbols-when-creating-a-dll),
or, as we ended up doing, using `__declspec(dllexport)` on each individual symbol.


####<a name="3"></a>AutoCAD and Revit SDK Access and Content

**Question:** I have an add-in for Revit 2017, so I have to compile it against the Revit 2017 assemblies.

Is there an SDK or lightweight download for these assemblies, or do I have to install Revit 2018 in parallel with 2017?

Also, is there any SDK available for AutoCAD?

**Answer:** Let's start from the end:

> Is there an SDK for AutoCAD?

Yes, several. AutoCAD sports a Lisp interface, the ObjectARX C++ API, and the AutoCAD.NET .NET API.

Please refer to 
the [AutoCAD developer centre at www.autodesk.com/developautocad](http://www.autodesk.com/developautocad) for
more information.

> Is there an SDK or lightweight download for the Revit assemblies, or do I have to install Revit 2018 in parallel with 2017?

You may very well be able to run your Revit API add-in compiled for Revit 2017 unmodified on Revit 2018.

You would have to test, of course. It will only cause problems if you use Revit API calls in the Revit 2017 API that have been removed or modified in the Revit 2018 API. This is probably not the case.

You can download the Revit SDK only without a full Revit installation for the last couple of SDK releases from
the [Revit developer centre at www.autodesk.com/developrevit](http://www.autodesk.com/developrevit).

However, you do not need the SDK at all to compile or run your add-in.

The SDK contains documentation and sample code, nothing else.

All you need to <b><i>compile</i></b> the add-in are the Revit API assemblies, e.g. RevitAPI.dll and RevitAPIUI.dll. These are included with the Revit product installation in the same folder as Revit.exe.

As you know, you need to launch Revit.exe to <b><i>run</i></b> the add-in.


####<a name="4"></a>Determining AreaLoad Direction and Value from its Force Vectors

This topic came up in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [direction and a value of `AreaLoad` from its 3 force vectors](https://forums.autodesk.com/t5/revit-api-forum/how-do-i-find-a-direction-and-a-value-of-areaload-from-its-3/m-p/7660527):

**Question:** There can be 2 or even 3 directions of `AreaLoad`: `ForceVector1`, `ForceVector2` and `ForceVector3`.

But Revit always draws the `AreaLoad` as directed to one direction.

How do I find that one direction?

How do I find a value of that one-directed load, at least at the points returned by the `GetRefPoint` method?

<center>
<img src="img/AreaLoad2.png" alt="AreaLoad" width="491"/>
</center>

**Answer:** Internally, all three forces are being stored as vectors and their length is the actual force. 

The direction of the load is a vector that, simplified, equals `(F1+F2+F3).Normalize()` &ndash; this is not exposed.

I will ask for a request to add an API property the force direction since, well, it is only logical to exist. 

Many thanks to Dragos Turmac and
Alexander [@aignatovich](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478) Ignatovich
for helping to verify this.



####<a name="5"></a>ExportCncFab 2018

Based on new requests, I updated
the [ExportCncFab add-in to export Revit wall parts to DXF or SAT for CNC fabrication](https://github.com/jeremytammik/ExportCncFab):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/uNJ9RTppqoU" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center>

Happily, no code modification was required at all for the update.

