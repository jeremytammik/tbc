<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Revit Macro Study Shareback
  https://www.autodeskresearchcommunity.com/hub/posts/post-25914628
  zip/revit_macro_study_shareback.pdf
  https://forums.autodesk.com/t5/revit-api-forum/research-how-do-you-use-revit-macros/m-p/11158305

- add-in manager Debug Trace
  Chuong Ho
  Revit add-in manager supports Debug/Trace Writeline include dockpanel for developer now.
  It's an improvement that i think it will save even more debugging time for programmers 🤗
  Download at opensource : https://lnkd.in/gtpy9RpV
  #developer #revitapi #autodesk #bim #AEC #addinmanager
  addinmanager_debugtrace.jpg 386
  [RevitAddInManager](https://github.com/chuongmep/RevitAddInManager)
  > Usually, when developing and debugging an addin with RevitAPI, user has to close & re-open Revit each time he/she modifies the addin code and wants to re-compile. But with Add-In Manager, user can modify and run the addin directly without closing & re-opening Revit again and again.

- interesting project
  [Journalysis](https://github.com/andydandy74/Journalysis) by
  Andreas [@andydandy74](https://github.com/andydandy74) Dieckmann
  Berlin, Germany
  > Journalysis is a Revit journal, worksharing log and keyboard shortcuts analysis package for the Dynamo visual programming environment.
  > Since there is hardly any documentation on Revit journals, it is a slow process. I have started writing some documentation in the [wiki](https://github.com/andydandy74/Journalysis/wiki) that may, however, not be entirely complete.
  > This package is aimed at automating the analysis of Revit journals and worksharing logs for statistical purposes. Some possible use cases:
  Monitor crashes
  Monitor API errors
  Monitor memory usage
  Monitor sync with central duration
  Keyboard shortcut usage

- 3D construction printing -- https://cobod.com -- https://cobod.com/videos/

- Switch to HSL Color Format](https://youtu.be/VInSzHOeFkE)
  with numerous useful CSS links (ca. 7 minutes)

twitter:

Looking at Revit macro usage, open source WPF add-in manager debug/trace functionality and journal analysis in Python, Dynamo and the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/journalysis

Looking at Revit macro usage, add-in manager debug/trace functionality and journal analysis in Python and Dynamo
&ndash; Revit macro study shareback
&ndash; Add-in manager with debug trace
&ndash; Journal file analysis
&ndash; Plugging the HSL colour format...

linkedin:

Looking at Revit macro usage, open source WPF add-in manager debug/trace functionality and journal analysis in Python, Dynamo and the #RevitAPI

https://autode.sk/journalysis

- Revit macro study shareback
- Add-in manager with debug trace
- Journal file analysis
- Plugging the HSL colour format...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Analysis of Macros, Journals and Add-In Manager

Let's look at the Revit macro study results, add-in manager debug/trace functionality and a Python and Dynamo journal analysis tool:

- [Revit macro study shareback](#2)
- [Add-in manager with debug trace](#3)
- [Journal file analysis](#4)
- [Plugging the HSL colour format](#5)

####<a name="2"></a> Revit Macro Study Shareback

We recently
asked for feedback from the add-in developer community in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how you use Revit Macros](https://forums.autodesk.com/t5/revit-api-forum/research-how-do-you-use-revit-macros/m-p/11158305).

The results are now in, and we share them with you for further evaluation and feedback:

> Feel free to review the research result summary and add any comments or suggestions at: 

> <center><a href="https://www.autodeskresearchcommunity.com/hub/posts/post-25914628">Revit Macro Study Shareback</a></center>

You have to set up an account with Autodesk research, fill in a survey and await the response email to see them.

To save others the same process, time and effort, I took the liberty of printing the results to PDF and sharing them here in [revit_macro_study_shareback.pdf](zip/revit_macro_study_shareback.pdf).

Many thanks to the Revit development team and Siyu Guo for the shareback and interesting results.

####<a name="3"></a> Add-In Manager with Debug Trace

We recently mentioned
Chuong Ho's [open source add-in manager](https://thebuildingcoder.typepad.com/blog/2022/01/add-in-manager-formulamanager-and-tiger-year.html#2).

> Usually, when developing and debugging an add-in with Revit API, user has to recompile, close and reopen Revit each time they modify the add-in code. 
With Add-In Manager, you can modify and run the add-in directly without closing and reopening Revit again and again.

Chuong announces new enhancements:

> Revit Add-in Manager supports Debug/Trace WriteLine including a dockable panel now.
> It's an improvement that I think will save even more debugging time for programmers 🤗
> Download from the [RevitAddInManager GitHub repo](https://github.com/chuongmep/RevitAddInManager).


<center>
<img src="img/addinmanager_debugtrace.jpg" alt="Add-in manager with debug trace" title="Add-in manager with debug trace" width="386"/> <!-- 386 -->
</center>

By the way, for the sake of completeness, note that
the [.NET hot reload for editing code at runtime](https://devblogs.microsoft.com/dotnet/introducing-net-hot-reload)
in Visual Studio 2019 also enables you to update your add-in code on the fly, cf.
[apply code changes debugging Revit add-in](https://thebuildingcoder.typepad.com/blog/2021/10/localised-forge-intros-and-apply-code-changes.html#4).

####<a name="4"></a> Journal File Analysis

I happened to notice
Andreas [@andydandy74](https://github.com/andydandy74) Dieckmann's
interesting Python and Dynamo project [Journalysis](https://github.com/andydandy74/Journalysis).

> Journalysis is a Revit journal, worksharing log and keyboard shortcuts analysis package for the Dynamo visual programming environment.
> Since there is hardly any documentation on Revit journals, it is a slow process.
> I have started writing some documentation in the [wiki](https://github.com/andydandy74/Journalysis/wiki) that may not be entirely complete.
> This package is aimed at automating the analysis of Revit journals and worksharing logs for statistical purposes.
> It helps track and monitor crashes, API errors, memory usage, sync with central duration, keyboard shortcut usage and more.

####<a name="5"></a> Plugging the HSL Colour Format

Unrelated to the Revit API, I found the 7-minute video explaining and motivating us 
to [switch to HSL colour format](https://youtu.be/VInSzHOeFkE) very interesting and informative:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/VInSzHOeFkE"
  title="Switch to HSL colour format" frameborder="0"
  allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
  allowfullscreen></iframe>
</center>
