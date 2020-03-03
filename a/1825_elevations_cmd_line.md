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

 in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

An exciting discussion on applying minimal adjustments to the model, and yet another research result on the effectivity of visual presentation
&ndash; Adjusting versus recreating wall location curve
&ndash; Multimedia communication versus bullet points...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Adjusting Wall Location Curve and Visual Presentation

An exciting discussion on applying minimal adjustments to the model, and yet another research result on the effectivity of visual presentation:

- [Adjusting versus recreating wall location curve](#2)
- [Multimedia communication versus bullet points](#3)

#### <a name="2"></a>Learning to Code with Interior Elevations

Micah [kraftwerk15](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4045014) Gray points out another useful learning resource in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [interior elevations code examples with repo](https://forums.autodesk.com/t5/revit-api-forum/interior-elevations-code-examples-w-repo/m-p/9348862):

> Another resource that is updated providing insight on writing code looking at interior elevations.
As always, .... not mine. 🙂 Just placing here for more people to learn:
- [lm2.me/posts](https://lm2.me/posts?dark=true)
- [github.com/lm2-me/RevitAddIns](https://github.com/lm2-me/RevitAddIns)

He points to a blog post on [ΩWinForms ComboBox](https://lm2.me/post/2020/02/07/winformscombobox).

It forms part of a whole series of articles by [lisa-marie mueller &ndash; *let's build the next thing together*](https://lm2.me).

She adds:

> If you want to learn to code and don’t know where to start, check out my posts about
Steps to Learn to Code for architects and designers:
- [Part 1](https://lm2.me/post/2019/08/19/learntocode-1)
- [Part 2](https://lm2.me/post/2019/08/23/learntocode-2)

Here is the rest of the series:

1. <a href="https://lm2.me/post/2019/10/04/filteredelementcollector">filtered element collector [c#]</a>
2. <a href="https://lm2.me/post/2019/10/11/consideringexceptions">finding centroids and considering exceptions</a>
3. <a href="https://lm2.me/post/2019/10/18/viewfamilytypeid">ViewFamilyTypeId</a>
4. <a href="https://lm2.me/post/2019/10/25/viewplanidandlevels">ViewPlanId and Levels</a>
5. <a href="https://lm2.me/post/2019/11/01/phasesandgoal1">phases &amp; goal #1 complete</a>&nbsp;[includes GitHub link to the release]
6. <a href="https://lm2.me/post/2019/11/08/viewtemplates">view templates</a>
7. <a href="https://lm2.me/post/2019/11/15/resizingcropboxes">resizing CropBoxes</a>
8. <a href="https://lm2.me/post/2019/11/22/creatingfilledregions">creating FilledRegions &amp; Goal #2 Complete</a>&nbsp;[includes GitHub link to release]
9. <a href="https://lm2.me/post/2019/12/06/coordinatesystemutilities">coordinate system utilities</a>
10. <a href="https://lm2.me/post/2019/12/13/renameviews">rename views &amp; goal #3 complete</a>&nbsp;[includes GitHub link to release]

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 1104 -->
</center>

Many thanks to Lisa-Marie for creating and sharing this valuable resource, and to Micah for his helpful pointer.

#### <a name="3"></a> Revit Command Line Switches Updated

The Building Coder discussed some command line switch related topics in the past:

- [Revit Command-Line Switches](https://thebuildingcoder.typepad.com/blog/2017/01/distances-switches-kiss-ing-and-a-dino.html#3)
- [Passing an Add-In Custom Command Line Parameters](https://thebuildingcoder.typepad.com/blog/2019/01/face-methods-and-custom-command-line-arguments.html#2)

Vladimir Michl of [cadstudio.cz](https://www.cadstudio.cz) provides an update to this in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit command-line switches list](https://forums.autodesk.com/t5/revit-api-forum/revit-command-line-switches-list/m-p/9345809):

**Question:** I am trying to find a list with all command-line switches that can be used with Revit.
Something equivalent to [this list that was shared for AutoCAD]().
I found some of them in a [previous post here](), but I am pretty sure those are just a few and not the complete list.

**Answer:** Here is a more recent [overview of runstring parameters for Revit.exe](https://www.cadforum.cz/cadforum_en/overview-of-revit-runstring-parameters-for-revit-exe-tip12524) of
the switches and parameters for the current versions for both full Revit and for Revit LT:

> Autodesk Revit (and Revit LT) is launched using the executable Revit.exe.

> In its parameters &ndash; from the desktop icon or on the command line &ndash; you can use a number of optional runstring parameters (switch, option):

>   - fully qualified name of a RVT/RTE/RFA file - opens a given project or template or family
- fully qualified name of a journal file - executes (repeats) commands stored in the journal log file (.txt)
- /language CODE - run Revit in the given language (if the respective language pack is installed) - e.g. "/language FRA"
- /nosplash - run Revit without the initial graphical splash/jingle
- /viewer - run Revit in the no-license view-only mode (R/O)
- /runmaximized - run in a maximized application window
- /runhidden - run in an invisible (hidden) application window
- /noninteractive - run in a non-interactive mode (cannot control Revit from its UI)
- /debugmode - run in a debug mode
- /3GB (only for older, 32-bit versions) - enable access to RAM over the 2GB limit

#### <a name="4"></a> World-Wide Connectivity

In the context of arranging the
world-wide [Forge Accelerators](http://autodeskcloudaccelerator.com/forge-accelerator),
Jaime Rosales Duque points out a handy solution for world-wide Internet connectivity:

> When I went to Colombia, I rented a device called SkyRoam.
It is a world traveler hotspot that allows you to connect up to 10 devices for unlimited data per day (9$) or per month ($80) anywhere in the world.
At the London Accelerator,  I went out and bought one outright.
So far, this little device have saved the day.
Here is the [web site where you can get the Skyroam Solis X](https://www.skyroam.com).
It is easy to operate using the mobile app on IOS or Android.
I think but it can be operated through a website too.

Many thanks to Jaime for the useful hint.


