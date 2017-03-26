<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="run_prettify.js" type="text/javascript"></script> --> 
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/3d-aimer-just-because/m-p/6938219
  The 3d Aimer, which rotates a family in many directions, is part of a larger project. I have created a public repository for that project. Here is the link. 
  https://github.com/akseidel/WTA_FireP
  https://www.youtube.com/watch?v=fKrtMx0R9k8

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge 

I am traveling to Gothenburg for the Forge accelerator. Meanwhile, Allan Seidel shared his WTA FireP 3D Aimer add-in that creates a custom ribbon tab with discipline related tools for placing specific Revit family types with some families requiring parameter settings made on the fly and the 3D Aimer example that "aims" a special Revit family to a target...

&ndash; 
...

-->

### WTA FireP 3D Aimer Tools

I am traveling to Gothenburg for the
[Forge accelerator](http://thebuildingcoder.typepad.com/blog/2017/03/events-uv-coordinates-and-rooms-on-level.html#2).

On the way, I noticed another update to the interesting thread in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160)
by Allan Seidel,
[@aksaks](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/540057)
and [@akseidel](https://github.com/akseidel), 
sharing his [3D Aimer, just because](https://forums.autodesk.com/t5/revit-api-forum/3d-aimer-just-because/m-p/6938219).

Says he:

If a picture is worth a thousand words then maybe a video is at least that, well maybe not.

Words for this might be,"What for?".

The answer is, "Just because." Sorry for the file size. The guts to how this is done have been previously posted. 

- [AIM3D_DOWNSIZED.mp4](https://forums.autodesk.com/autodesk/attachments/autodesk/160/21574/1/AIM3D_DOWNSIZED.mp4), 21 MB

The overall intent is to share ideas and show what has been used using information learned
from [The Building Coder](http://thebuildingcoder.typepad.com/) site.

The 3d Aimer is a proof of concept.

It has roots in something similar made in AutoCAD VBA 11 years ago that was part of a technical lighting design, but in Revit it is a curiosity. Perhaps someone might see that it performs a needed task.

The other things going on in the video are just as much part of the show for sharing.

Like what those rounded status windows are and how they come into being and change during the workflow or how a selection swipe across everything only selected the items that would be rotated. The whole megillah, from ribbon start to end, is best for seeing so that one starting from scratch can see the implementation, for good or for bad example.

#### <a name="2"></a>Update

The 3d Aimer, which rotates a family in many directions, is part of a larger project.

I have created a public GitHub repository and a YouTube posting for it:
 
- [WTA_FireP GitHub repo](https://github.com/akseidel/WTA_FireP)
- [Revit 3D Aimer add-in example concept](https://www.youtube.com/watch?v=fKrtMx0R9k8)

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/fKrtMx0R9k8?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

#### <a name="3"></a>Documentation

![RibbonTab](img/akseidel_FPRibbonTab.png)

Revit Add-in in C# : Creates a custom ribbon Tab with discipline related tools. The tools at this writing are for placing specific Revit family types with some families requiring parameter settings made on the fly.

Included is the 3dAimer example that "aims" a special Revit family to a target. The enclosed Revit project is for demonstrating the 3dAimer.

This repository is provided for sharing and learning purposes. Perhaps someone might provide improvements or education. Perhaps it will help to boost someone further up the steep learning curve needed to create Revit task add-ins. Hopefully it does not show too much of the wrong way.  

Used by the tools in this ribbon are classes intended to provide a Revit family instance placement without the Revit use interface overhead normally required by Revit. The classes are intended to provide a universal mechanism for placing some types of Revit families. This includes tags, which is a task not in this discipline Tab but is in other discipline add-ins. The custom tab employs menu methods not commonly explained, for example a split button sets a family placement mode that is exposed to the functions called by command picks. Other tools use Add-in application settings as a way to persist settings or communicate to code that runs subsequent within a command that provides a task workflow.

What goes on in this add-in is much of the typical tasks required for providing a Tab menu interface involving family placement tasks and for implementing those tasks. This means things like:

* Creating a ribbon tab populated with some controls
  - Tool tips
  - Image file to Button image
  - Communication between controls and commands the controls execute
* Establishing the family type for placement
  - Determine if the correct pairing exists in the current file
  - Automatically discovering and loading the family if it does not exist in the current file but does exist somewhere staring from some set directory
* Providing the family type placement interface
  - In multiple mode or one shot mode
  - With a heads-up status/instruction interface form
    - As WPF with independent behavior
      - Sending focus back to the Revit view
  - Returning the family instance placement for further processing after the instance has been placed
  - Managing an escape out from the process
  - Handling correct view type context
* Changing family parameter values

Much of the code is by others. Its mangling and ignorant misuse is my ongoing doing. Much thanks to the professionals like Jeremy Tammik who provided the means directly or by mention one way or another for probably all the code needed.

Many thanks to Allan for sharing this!
