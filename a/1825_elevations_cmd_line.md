<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/interior-elevations-code-examples-w-repo/m-p/9348862
  kraftwerk15 
  Micah Gray

- updated command line switches
  Vladimir Michl of [cadstudio.cz](https://www.cadstudio.cz) provided an update on the Revit command line switches in
  on [Revit command-line switches list](https://forums.autodesk.com/t5/revit-api-forum/revit-command-line-switches-list/m-p/9345809):

- need connectivity? Jaime Rosales D. https://www.skyroam.com

twitter:

Another inspiring guide to getting started coding with the Revit API, creating interior elevations and revisiting the Revit command line switches with the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/learncodingelevations

Another inspiring guide to getting started with the Revit API, creating interior elevations and revisiting the Revit command line switches
&ndash; Learning to code with interior elevations
&ndash; Revit command line switches updated
&ndash; World-wide connectivity...

linkedin:

Another inspiring guide to getting started coding with the Revit API, creating interior elevations and revisiting the Revit command line switches with the #RevitAPI

http://bit.ly/learncodingelevations

- Learning to code with interior elevations
- Revit command line switches updated
- World-wide connectivity...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Coding, Interior Elevations and Command Line Options

Another inspiring guide to getting started with the Revit API, creating interior elevations and revisiting the Revit command line switches:

- [Learning to code with interior elevations](#2)
- [Revit command line switches updated](#3)
- [World-wide connectivity](#4)

#### <a name="2"></a>Learning to Code with Interior Elevations

Micah [kraftwerk15](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4045014) Gray points out another useful learning resource in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [interior elevations code examples with repo](https://forums.autodesk.com/t5/revit-api-forum/interior-elevations-code-examples-w-repo/m-p/9348862):

> Another resource that is updated providing insight on writing code looking at interior elevations.
As always, .... not mine. 🙂 Just placing here for more people to learn:

>    - [lm2.me/posts](https://lm2.me/posts?dark=true)
>    - [github.com/lm2-me/RevitAddIns](https://github.com/lm2-me/RevitAddIns)

The blog post on [WinForms ComboBox](https://lm2.me/post/2020/02/07/winformscombobox) forms
part of a whole series of articles by [lisa-marie mueller &ndash; *let's build the next thing together*](https://lm2.me).

She adds:

> If you want to learn to code and don’t know where to start, check out my posts about
Steps to Learn to Code for architects and designers:

>    - [Part 1](https://lm2.me/post/2019/08/19/learntocode-1)
>    - [Part 2](https://lm2.me/post/2019/08/23/learntocode-2)

Here is the rest of the series:

1. <a href="https://lm2.me/post/2019/10/04/filteredelementcollector">filtered element collector [c#]</a>
2. <a href="https://lm2.me/post/2019/10/11/consideringexceptions">finding centroids and considering exceptions</a>
3. <a href="https://lm2.me/post/2019/10/18/viewfamilytypeid">ViewFamilyTypeId</a>
4. <a href="https://lm2.me/post/2019/10/25/viewplanidandlevels">ViewPlanId and Levels</a>
5. <a href="https://lm2.me/post/2019/11/01/phasesandgoal1">phases &amp; goal #1 complete</a>&nbsp;[includes GitHub link]
6. <a href="https://lm2.me/post/2019/11/08/viewtemplates">view templates</a>
7. <a href="https://lm2.me/post/2019/11/15/resizingcropboxes">resizing CropBoxes</a>
8. <a href="https://lm2.me/post/2019/11/22/creatingfilledregions">creating FilledRegions &amp; Goal #2 Complete</a>&nbsp;[includes GitHub link]
9. <a href="https://lm2.me/post/2019/12/06/coordinatesystemutilities">coordinate system utilities</a>
10. <a href="https://lm2.me/post/2019/12/13/renameviews">rename views &amp; goal #3 complete</a>&nbsp;[includes GitHub link to release]

<center>
<img src="img/lisa_marie_mueller.png" alt="lisa-marie mueller" title="lisa-marie mueller" width="500"/> <!-- 1504 -->
</center>

Many thanks to Lisa-Marie for creating and sharing this valuable resource, and to Micah for his helpful pointer.

#### <a name="3"></a> Revit Command Line Switches Updated

We mentioned some command line switch related topics here in the past:

- [Revit Command-Line Switches](https://thebuildingcoder.typepad.com/blog/2017/01/distances-switches-kiss-ing-and-a-dino.html#3)
- [Passing an Add-In Custom Command Line Parameters](https://thebuildingcoder.typepad.com/blog/2019/01/face-methods-and-custom-command-line-arguments.html#2)

Vladimir Michl of [cadstudio.cz](https://www.cadstudio.cz) provides an update to these in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit command-line switches list](https://forums.autodesk.com/t5/revit-api-forum/revit-command-line-switches-list/m-p/9345809):

**Question:** I am trying to find a list with all command-line switches that can be used with Revit.
Something equivalent to [this list that was shared for AutoCAD]().
I found some of them in a [previous post here](), but I am pretty sure those are just a few and not the complete list.

**Answer:** Here is a more recent [overview of runstring parameters for Revit.exe](https://www.cadforum.cz/cadforum_en/overview-of-revit-runstring-parameters-for-revit-exe-tip12524) of
the switches and parameters for the current versions for both full Revit and for Revit LT:

> Autodesk Revit (and Revit LT) is launched using the executable Revit.exe.

> In its parameters &ndash; from the desktop icon or on the command line &ndash; you can use a number of optional runstring parameters (switch, option):

- fully qualified name of a RVT/RTE/RFA file &ndash; open a given project or template or family
- fully qualified name of a journal file &ndash; execute (repeat) commands stored in the journal log file (.txt)
- /language CODE &ndash; run Revit in the given language (if the respective language pack is installed) &ndash; e.g. "/language FRA"
- /nosplash &ndash; run Revit without the initial graphical splash/jingle
- /viewer &ndash; run Revit in the no-license view-only mode (R/O)
- /runmaximized &ndash; run in a maximized application window
- /runhidden &ndash; run in an invisible (hidden) application window
- /noninteractive &ndash; run in a non-interactive mode (cannot control Revit from its UI)
- /debugmode &ndash; run in a debug mode
- /3GB (only for older, 32-bit versions) &ndash; enable access to RAM over the 2GB limit

#### <a name="4"></a> World-Wide Connectivity

In the context of arranging the
world-wide [Forge Accelerators](http://autodeskcloudaccelerator.com/forge-accelerator),
Jaime Rosales Duque points out a handy solution for world-wide Internet connectivity:

> When I went to Colombia, I rented a device called SkyRoam.
It is a world traveller hotspot that allows you to connect up to 10 devices for unlimited data per day (9$) or per month ($80) anywhere in the world.
At the London Accelerator,  I went out and bought one outright.
So far, this little device has saved the day.
Here is the [web site where you can get the Skyroam Solis X](https://www.skyroam.com).
It is easy to operate using the mobile app on IOS or Android.
I think but it can be operated through a website too.

Many thanks to Jaime for the useful hint.
