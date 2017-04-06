<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="run_prettify.js" type="text/javascript"></script> --> 
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 12837247 [Giving back - A workshared file to be local file opener]
  https://forums.autodesk.com/t5/revit-api-forum/giving-back-a-workshared-file-to-be-local-file-opener/m-p/6990230

Modeless WPF RVT file sniffer and opener http://bit.ly/aksopenforgefader #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge
RvtFader and ForgeFader compare #RevitAPI w/ @AutodeskForge http://bit.ly/aksopenforgefader @AutodeskRevit #aec #bim #dynamobim 

I completed a first revision of the ForgeFader project, bringing it up to par with RvtFader.
It is pretty cool seeing the same functionality implemented in two such different ways, on completely different platforms, using different tools.
Alan Seidel shared another exciting Revit add-in.
First and not least, another exciting topic for me personally is switching to half-time work
&ndash; Work half
&ndash; AKS Opener
&ndash; Video
&ndash; GitHub repository
&ndash; Why?
&ndash; Specific interest
&ndash; RvtFader
&ndash; ForgeFader...

-->

### Work Half, AKS Opener, RvtFader and ForgeFader

I completed a first revision of 
the [ForgeFader](https://github.com/jeremytammik/forgefader) project,
bringing it up to par with [RvtFader](https://github.com/jeremytammik/Rvtfader).

It is pretty cool seeing the same functionality implemented in two such different ways, on completely different platforms, using different tools.

Alan Seidel shared another exciting Revit add-in.

First and not least, another exciting topic for me personally is switching to half-time work:

- [Work half](#2)
- [AKS Opener](#3)
    - [Video](#4)
    - [GitHub repository](#5)
    - [Why?](#6)
    - [Specific interest](#7)
- [RvtFader](#8)
- [ForgeFader](#9)

#### <a name="2"></a>Work Half

Starting April 1, I am working half time.

Unfortunately, April 1 fell on Saturday, we held a team meeting in Gothenburg, and my flight back home with Air Berlin was cancelled, so I ended up working a lot on that specific day, and a weekend, to boot.

I also ended up working more than full time the last two days, finishing off the ForgeFader sample.

Up to me to stop, though.

From now on, my aim is to focus on the Revit question answering system Q4R4, blog, mentor my new colleagues (when they materialise) and spend less time with cases and repetitive questions.

They should be handled automatically by Q4R4 anyway &nbsp; :-)

#### <a name="3"></a>AKS Opener

Allan
[@aksaks](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/540057)
[@akseidel](https://github.com/akseidel)
Seidel is really churning them out now!

He shared another full-fledged and advanced Revit add-in in
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread 
on [giving back &ndash; a workshared file to be local file opener](https://forums.autodesk.com/t5/revit-api-forum/giving-back-a-workshared-file-to-be-local-file-opener/m-p/6990230):

> In the spirit of giving back for all the help garnered here to experiment with this idea, and also the challenge to make a Revit file opening video slightly more interesting than watching paint dry, here is something that might be useful to some. Its purpose is to be a one click open a Revit workshared file to be a local file without needing further user input and to do so maintaining the local Revit disk storage area organized better than a heap.
>
> - [Video](https://www.youtube.com/watch?v=oquPOrq7ORA) 
> - [Repository](https://github.com/akseidel/RevitAKSOpen)

This is a beautiful and advanced sample that provides many important programming pointers.

I love the `RevitFileSniffer` to read `BasicFileInfo` directly from binary RVT.
I love all the aspects of it!
Modeless WPF, the works!

Here are Allan's video and repository notes:

#### <a name="4"></a>AKS Opener Video

What is more boring than a Revit file opener? A video of opening a Revit file. That was the challenge here; how to show a one click Revit workshared file opener that opens a workshared central file as a local file without requiring any more user input beyond that first click.

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/oquPOrq7ORA?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

#### <a name="5"></a>AKS Opener GitHub Repository

[RevitAKSOpen](https://github.com/akseidel/RevitAKSOpen) is
a Revit add-in in C# that creates a custom ribbon tab with a single control for opening workshared Revit files in one click.
The same control is added to the Addins tab.
The controls are modeless, active at all times.

<center>
<img src="img/akseidel_AKSOpen3.png" alt="RevitAKSOpen GenieOpener" width="64"/>
</center>

#### <a name="6"></a>Why?

Opening a workshared Revit file to be a Local work file is a multistep process.
Revit, furthermore, is sloppy about it.
It trashes the same local folder with all the local files and their overhead files it saves.
Revit also leaves a trap in the Revit desktop when it deposits an icon for the central file one chose.
Of course, one can reopen the saved local file instead of the central file, but some are not so daring and often forget to do the critical initial sync.
Their Revit managers might insist they never use a local to avoid "issues".
Autodesk's publications actually suggest to operate that way. Situations where the Revit coordinator replaces the central file with a copy orphans the local file, so one has to start from the central file again.

Why not present the user with choices from what they have already been working on or point them to choices of the appropriate Revit version to be opened ready to go as a local file stored in an organized, structured pattern while also saving previous work in a similar manner. This add-in does that. It tries, in one click, to automatically pass the various Revit roadblocks and hazards, with added smarts, to properly open a workshared file.      

This add-in demonstrates many of the typical tasks and implementation required for providing a tab menu interface and dealing with files.
A back-burner improvement is to make the user selection landing areas larger, perhaps changing to or adding preview icons to the interface.

#### <a name="7"></a>Specific Interest

**"Tell Me About It" Mode**

- A function reporting a Revit file's metadata to determine what Revit version it may be. It reports other useful information. This is one way to determine a Revit file's provenance without actually opening the file in Revit.

**Zero document state operation**

- Having the add-in available to run when there is not already a Revit file open is called zero document state availability. It makes sense for this add-in to have that availability.

**Writing to the Revit status bar**

- A thing to know. Revit funnels most of its feedback to the status bar. This add-in uses the status bar to be a whimsical check on who is paying attention.

**Useful mundane file and directory operations for reuse**

- Making local folders identified with the user's initials.
- Moving prior local files to a local stash folder.
- Indexing the prior local file names so that one can see their generation. This involves the "make a unique name" task one runs into needing in many situations.
- Pruning the number of stashed files.

This repository is provided for sharing and learning purposes. Perhaps someone might provide improvements or education. Perhaps it will help to boost someone further up the steep learning curve needed to create Revit task add-ins. Hopefully it does not show too much of the wrong way.  

Much of the code is by others. Its mangling and ignorant misuse is my ongoing doing. Much thanks to the professionals like Jeremy Tammik who provided the means directly or by mention one way or another for probably all the code needed.

Many thanks to Allan for putting together and sharing this!

#### <a name="8"></a>RvtFader

[RvtFader](https://github.com/jeremytammik/Rvtfader) is pretty well documented in its GitHub repository.

I described the initial release talking
about [RvtFader, AVF, ray tracing and signal attenuation](http://thebuildingcoder.typepad.com/blog/2017/03/rvtfader-avf-ray-tracing-and-signal-attenuation.html).

I think it provides a cool starting point for any new little application as well, implementing:

- A nice external little external application with custom ribbon tab, panel, split button, main and settings commands

<center>
<img src="img/rvtfader_ribbon_tab.png" alt="RvtFader ribbon tab" width="141"/>
</center>

- Manage settings to be edited and stored in a JSON text file.
- Enable user to pick a source point on a floor.
- Determine the floor boundaries.
- Shoot rays from the picked point to an array of other target points covering the floor.
- Determine the obstacles encountered by the ray, specifically wall elements.
- Display a 'heat map', i.e. colour gradient, representing the signal loss caused by the distance and number of walls between the source and the target points.

It uses the Revit API `ReferenceIntersector` ray tracing functionality to detect walls and
the [analysis visualisation framework AVF](http://thebuildingcoder.typepad.com/blog/avf) to display the heat map.

The result of launching the command and picking a point looks like this:

<center>
<img src="img/rvtfader_result.png" alt="Signal attenuation calculated and displayed by RvtFader" width="494"/>
</center>


#### <a name="9"></a>ForgeFader

[ForgeFader](https://github.com/jeremytammik/forgefader) implements
the same functionality as RvtFader in
the [Forge](https://forge.autodesk.com)
[viewer](https://developer.autodesk.com/en/docs/viewer/v2/overview) environment.

It is an extension app that calculates and displays signal attenuation caused by distance and obstacles in a building model with a floor plan containing walls.

Instead of the Revit API functionality, it makes use of JavaScript
and [three.js](https://threejs.org).

Here is the result of processing the model displayed above in Revit using ForgeFader:

<center>
<img src="img/forgefader_test_result.png" alt="Signal attenuation calculated and displayed by ForgeFader" width="554"/>
</center>

The four-minute [ForgeFader Autodesk Forge sample app](https://youtu.be/78JlGnf49mc) YouTube video explains some of the background and shows this sample app live in action:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/78JlGnf49mc?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

So far, I discussed two implementation steps here on The Building Coder; and a third important step was contributed by Cyrille Fauvel:

- [Adding custom geometry to the Forge viewer](http://thebuildingcoder.typepad.com/blog/2017/03/adding-custom-geometry-to-the-forge-viewer.html)
- [Three.js raytracing in the Forge Viewer](http://thebuildingcoder.typepad.com/blog/2017/03/threejs-raytracing-in-the-forge-viewer.html)
- [Implementing a custom shader in the Forge Viewer](https://github.com/jeremytammik/forgefader#implementing-a-custom-shader-in-the-forge-viewer)

ForgeFader is based 
on [Philippe Leefsma](https://github.com/leefsmp)'s 
[Forge React boilerplate sample](https://github.com/Autodesk-Forge/forge-react-boiler.nodejs).
Please refer to that for more details on the underlying architecture and components used.

Now the time is more than overdue for me to stop working and get out and do something else!

Already half-way through the third day of the week, and sitting here blogging...
