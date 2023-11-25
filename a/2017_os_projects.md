<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://github.com/jeremytammik/RevitLookup/releases

- https://forum.dynamobim.com/t/revitpythondocs-for-dynamo-and-pyrevit-request-for-feedback/95280?u=jacob.small

- interactive open source Revit SDK sample browser and launcher
  https://forums.autodesk.com/t5/revit-api-forum/feedback-requested-on-open-source-revit-api-sample-browser/m-p/12386403

- purge add-in with rave reviews searching for new maintainer
  https://forums.autodesk.com/t5/revit-api-forum/project-sweeper-revved-and-other-apps-now-open-source/m-p/12386626

- speedcad tools: OptionsBar, StatusBar
  https://forums.autodesk.com/t5/revit-api-forum/optionsbar/m-p/12377344
  https://github.com/SpeedCAD/SCADtools.Revit.UI.ProgressMeter
  https://github.com/SpeedCAD/SCADtools.Revit.UI.OptionsBar
  cf. Roman's Open-Source OptionsBar
  https://thebuildingcoder.typepad.com/blog/2023/09/optionsbar-and-bye-bye-to-da4r-2018.html#2

- Jean-Marc Couffin pointed out some interesting solutions to manage multiple versions of API on the pyRevit forum
  Support multiple versions of Revit in invokebuttons (dll projects, Visual Studio)
  https://discourse.pyrevitlabs.io/t/support-multiple-versions-of-revit-in-invokebuttons-dll-projects-visual-studio/1849/9?u=ali.tehami

- designscript, rhino, and other geometry libraries in revit add-in
  Using DesignScript in Revit addin
  https://forums.autodesk.com/t5/revit-api-forum/using-designscript-in-revit-addin/td-p/8203199

twitter:

#RevitAPI preview C# 7, RevitPythonDocs, Revit SDK sample browser, purge add-in, OptionsBar, StatusBar, multiple API versions, DesignScript, Rhino, and other geometry libraries with @AutodeskAPS @AutodeskRevit #BIM @DynamoBIM https://autode.sk/dotnetcorepreview

Open-source related discussions, interesting Revit API and AI related news
&ndash; Revit preview release with C&#35; 7
&ndash; RevitLookup 2024.0.9 is here
&ndash; RevitPythonDocs for Dynamo and pyRevit
&ndash; New Revit SDK sample browser and launcher
&ndash; Purge add-in with rave reviews
&ndash; SpeedCad tools OptionsBar and StatusBar
&ndash; Managing multiple Revit API versions
&ndash; DesignScript, Rhino, and other geometry libraries
&ndash; Trading glass beads for AI IP...

linkedin:

#RevitAPI preview C# 7, RevitPythonDocs, Revit SDK sample browser, purge add-in, OptionsBar, StatusBar, multiple API versions, DesignScript, Rhino, and other geometry libraries

https://autode.sk/dotnetcorepreview

- Revit preview release with C&#35; 7
- RevitLookup 2024.0.9 is here
- RevitPythonDocs for Dynamo and pyRevit
- New Revit SDK sample browser and launcher
- Purge add-in with rave reviews
- SpeedCad tools OptionsBar and StatusBar
- Managing multiple Revit API versions
- DesignScript, Rhino, and other geometry libraries
- Trading glass beads for AI IP...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### .NET Core Preview and Open Source Add-In Projects

A lot of open-source related interaction going on, plus other interesting Revit API and AI related news:

- [Revit preview release with C&#35; 7](#2)
- [RevitLookup 2024.0.9 is here](#3)
- [RevitPythonDocs for Dynamo and pyRevit](#4)
- [New Revit SDK sample browser and launcher](#5)
- [Purge add-in with rave reviews](#6)
- [SpeedCad tools OptionsBar and StatusBar](#7)
- [Managing multiple Revit API versions](#8)
- [DesignScript, Rhino, and other geometry libraries](#9)
- [Trading glass beads for AI IP](#10)

####<a name="2"></a> Revit Preview Release with C&#35; 7

As my colleague George points out, the current Revit preview release includes access to the a more modern .NET framework:
[Revit API is moving to .NET Core 7.0](https://adndevblog.typepad.com/aec/2023/11/revit-api-is-moving-to-net-core-70.html).

The new preview is now available from
the [Revit Preview Project](https://feedback.autodesk.com/key/LHMJFVHGJK085G2M).

[First reactions](https://www.linkedin.com/feed/update/urn:li:activity:7133897795630985216):

Pedro Nadal [says](https://www.linkedin.com/feed/update/urn:li:activity:7133897795630985216?commentUrn=urn%3Ali%3Acomment%3A%28activity%3A7133897795630985216%2C7133910174628524032%29&dashCommentUrn=urn%3Ali%3Afsd_comment%3A%287133910174628524032%2Curn%3Ali%3Aactivity%3A7133897795630985216%29):
The possibility of being able to develop Revit addins in .NET Core is one of the most awaited news for a long time!
Fantastic, hopefully the project will go ahead and we will have news soon.

Deniz Maral [adds a correction](https://www.linkedin.com/feed/update/urn:li:activity:7133897795630985216?commentUrn=urn%3Ali%3Acomment%3A%28activity%3A7133897795630985216%2C7133915379029925888%29&dashCommentUrn=urn%3Ali%3Afsd_comment%3A%287133915379029925888%2Curn%3Ali%3Aactivity%3A7133897795630985216%29):
.NET Core 7 comes with C# 11.0, not 7.0.
In .NET Framework 4.8 we had already C# 7.3 &nbsp; :)

Thank you for your thoughts and corrections, Pedro and Deniz!

####<a name="3"></a> RevitLookup 2024.0.9 is Here

A new version RevitLookup 2024.0.9 has been released.
Check out the numerous enhancements in
the [RevitLookup releases](https://github.com/jeremytammik/RevitLookup/releases).

Many thanks
to [Sergey Nefyodov](https://github.com/SergeyNefyodov)
and Roman [@Nice3point](https://t.me/nice3point) Karpovich, aka Роман Карпович,
for all their contributions and maintenance work!

####<a name="4"></a> RevitPythonDocs for Dynamo and pyRevit

[Gerhard P](https://forum.dynamobim.com/u/gerhard.p)
shared a new Dynamo and pyRevit initiative,
[RevitPythonDocs for Dynamo and pyRevit &ndash; request for feedback](https://forum.dynamobim.com/t/revitpythondocs-for-dynamo-and-pyrevit-request-for-feedback/95280):

> Going the first steps at creating a collection of Python scripts for Dynamo and pyRevit 1, I´d like to know what are your needs when you are looking for code, classes, methods.

> - [Revit Python Docs](http://www.revitpythondocs.com/)

> It will for sure get search and filter options, but for now it´s more of creating a good naming convention and finding out what code is really needed. In the following what I have come up with so far and I´m looking forward to your thoughts and hopefully contributions! The code is on Github.

> Some rules for the code:

> - Every script is a full working code including imports.
- Necessary imports only.
- No inputs from dynamo needed, necessary elements are created in the code.
- Dynamo code: tabs as indent only.
- pyRevit 1 code: spaces as indent only.
- No comments, just clean code.
- snake_case only, for variables, functions, everything.
- No “for i in a:”, proper names for everything (except list comprehension).

> Code is available in different versions, [check the original post for examples](https://forum.dynamobim.com/t/revitpythondocs-for-dynamo-and-pyrevit-request-for-feedback/95280)...

Many thanks to Gerhard for the initiative and to Jacob Small for pointing it out.

####<a name="5"></a> New Revit SDK Sample Browser and Launcher

Christopher Diggins presents a new interactive open-source Revit SDK sample browser and launcher:
[feedback requested on open-source Revit API sample browser](https://forums.autodesk.com/t5/revit-api-forum/feedback-requested-on-open-source-revit-api-sample-browser/m-p/12386403):

> I do a fair amount of development using the Revit API.
For my own reference, and to test some other plug-ins I am developing, I aggregated all of the Revit C# SDK Sample into a single project.
I provided a simple UI so that each of the Commands and Applications can be launched by double clicking on the names.
Selecting an item's load the associated readme file in a rich text edit box:

<center>
<img src="img/cd_revit_sample_browser.png" alt="Revit SDK sample browser" title="Revit SDK sample browser" width="442"/> <!-- Pixel Height: 613 Pixel Width: 881 -->
</center>

> I've posted the code to Github here:

> - [Ara 3D Revit Sample Browser](https://github.com/ara3d/revit-sample-browser)

> I haven't had time to test all of the samples, but it seems to be working.
I'd appreciate any feedback!

This looks like an absolutely brilliant project and a great piece of work.
Thanks ever so much to Christopher for picking it up, tackling and sharing it.
It has some slight similarity to my own RvtSamples project, as aged and old-fashioned as many of the SDK samples.
That enables launching all the Revit SDK external commands but implements a more primitive UI and provides no built-in access to the sample documentation.
The new sample browser also supports external applications as well as external commands!
Great job!

####<a name="6"></a> Purge Add-In with Rave Reviews

[Kfpopeye shared several useful open-source Revit API projects](https://thebuildingcoder.typepad.com/blog/2021/09/kfpopeye-open-source-avf-and-other-cleanup.html#2) two years ago.

Some of them received rave reviews and a lot of interest was expressed to update them and find a new name and maintainer in the recent continuation of
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Project Sweeper, ReVVed, and other apps now open source](https://forums.autodesk.com/t5/revit-api-forum/project-sweeper-revved-and-other-apps-now-open-source/m-p/12386626).

It will be interesting to see where this leads.

####<a name="7"></a> SpeedCad Tools OptionsBar and StatusBar

In yet another discussion on open-source code sharing and the importance of adding a suitable license in order to enable people to make use of their code, Speed_CAD shared
their [ProgressMeter](https://forums.autodesk.com/t5/revit-api-forum/progressmeter/td-p/12363674)
and [OptionsBar](https://forums.autodesk.com/t5/revit-api-forum/optionsbar/m-p/12377344) utility DLLs.
Here are the associated GitHub repositories:

- [ProgressMeter](https://github.com/SpeedCAD/SCADtools.Revit.UI.ProgressMeter)
- [OptionsBar](https://github.com/SpeedCAD/SCADtools.Revit.UI.OptionsBar)

By the way, the latter is comparable with Roman's recent
fully [Open-Source OptionsBar](https://thebuildingcoder.typepad.com/blog/2023/09/optionsbar-and-bye-bye-to-da4r-2018.html#2).

####<a name="8"></a> Managing Multiple Revit API Versions

Andrea Tassera, Jean-Marc Couffin and others discuss some interesting solutions to manage multiple versions of API on the pyRevit forum thread on how
to [support multiple versions of Revit in invokebuttons (dll projects, Visual Studio)](https://discourse.pyrevitlabs.io/t/support-multiple-versions-of-revit-in-invokebuttons-dll-projects-visual-studio).

####<a name="9"></a> DesignScript, Rhino, and other Geometry Libraries

Some interesting and illuminating aspects of using DesignScript, Rhino, and other geometry libraries with the Revit API are discussed in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [using DesignScript in Revit addin](https://forums.autodesk.com/t5/revit-api-forum/using-designscript-in-revit-addin/td-p/8203199).

####<a name="10"></a> Trading Glass Beads for AI IP

Not too long ago, some people acquired coveted goods from less business savvy folks with glass beads.

Nowadays, one example of coveted goods are AI IP, the glass beads are compute time, the less business savvy seem to be the AI enthusiasts of OpenAI, and guess who is business savvy?

- [OpenAI’s Misalignment and Microsoft’s Gain](https://stratechery.com/2023/openais-misalignment-and-microsofts-gain/)

