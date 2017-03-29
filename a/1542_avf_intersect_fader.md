<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="run_prettify.js" type="text/javascript"></script> --> 
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- revitwebcam

- rvtfader

Display webcam image on Revit element using AVF #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/avf_raytrace_webcam
Calculate signal attenuation in BIM using ReferenceIntersector ray tracing #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/avf_raytrace_webcam

I implemented a neat new little sample add-in, RvtFader, that  calculates and displays signal attenuation caused by distance and obstacles, specifically walls.
It uses two very interesting pieces of Revit API functionality
&ndash; AVF, the Analysis Visualisation Framework, for displaying graphical data in a transient manner directly in the BIM
&ndash; The ReferenceIntersector ray tracing functionality to detect walls and other obstacles between two points.
In the course of implementing the AVF part of things, I also resuscitated my trusty old RevitWebcam add-in
&ndash; RevitWebcam
&ndash; RvtFader
&ndash; Task
&ndash; Implementation
&ndash; Further Reading...

-->

### AVF, Ray Tracing and Signal Attenuation

I have been a bit quieter in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) in
the past day or two.

Why?

Well, I implemented a neat new little sample add-in, [RvtFader](https://github.com/jeremytammik/RvtFader).

In a rather simplified manner, it calculates and displays signal attenuation caused by distance and obstacles, specifically walls.

That provided an opportunity for me to dive in again into two very interesting pieces of Revit API functionality:

- [AVF, the Analysis Visualisation Framework](http://thebuildingcoder.typepad.com/blog/avf), for displaying graphical data in a transient manner directly in the BIM.
- The `ReferenceIntersector` ray tracing functionality to detect walls and other obstacles between two points.

In the course of implementing the AVF part of things, I also resuscitated my trusty
old [RevitWebcam](https://github.com/jeremytammik/RevitWebcam) add-in.

- [RevitWebcam](#2)
- [RvtFader](#3)
- [Task](#4)
- [Implementation](#5)
- [Further Reading](#6)


#### <a name="2"></a> RevitWebcam

[RevitWebcam](https://github.com/jeremytammik/RevitWebcam) uses
AVF and an external event to display a live webcam image on a selected element face.

The external event polls the webcam for updated images at regular intervals.

I now created a new GitHub repository to host this add-in and migrated it to Revit 2017.

Here it is displaying a webcam image on a wall:

<center>
<img src="img/webcam_on_wall_2017.png" alt="RevitWebcam in action in Revit 2017" width="500"/>
</center>

Back to `RvtFader`, though:


#### <a name="3"></a> RvtFader

RvtFader is a Revit C# .NET API add-in to calculate and display signal attenuation using 
the [analysis visualisation framework](http://thebuildingcoder.typepad.com/blog/avf) AVF
and `ReferenceIntersector` ray tracing.

<center>
<img src="img/rvtfader_command_icon.png" alt="RvtFader" width="32"/>
</center>


#### <a name="4"></a> Task

This application works in a Revit model with a floor plan containing walls.

It calculates the signal attenuation caused by distance and obstacles.

In the first iteration, the only obstacles taken into account are walls.

Two signal attenuation values in decibels are defined in the application settings:

- Attenuation per metre in air
- Attenuation by a wall

Given a source point, calculate the attenuation in a widening circle around it and display that as a heat map.


#### <a name="5"></a> Implementation

To achieve this task, RvtFader implements the following:

- Manage settings to be edited and stored (signal loss in dB).
- Enable user to pick a source point on a floor.
- Determine the floor boundaries.
- Shoots rays from the picked point to an array of other target points covering the floor.
- Determine the obstacles encountered by the ray, specifically wall elements.
- Display a 'heat map', i.e. colour gradient, representing the signal loss caused by the distance and number of walls between the source and the target points.

Summary of the steps towards achieving this:

- Skeleton add-in using the [Visual Studio Revit Add-In Wizards](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20).
- External command for the settings user interface displaying a Windows form and storing data in JSON as developed for
the [HoloLens escape path waypoint JSON exporter](http://thebuildingcoder.typepad.com/blog/2016/09/hololens-escape-path-waypoint-json-exporter.html):
    - Display modal Windows form.
    - Implement form validation using `ErrorProvider` class, `Validating` and `Validated` events.
    - Store add-in option settings in JSON using the `JavaScriptSerializer` class.
- AVF heat map, initially simply based on distance from the selected source point:

<center>
<img src="img/rvtfader_avf.png" alt="RvtFader displaying distance using AVF" width="188"/>
</center>

- Graphical debugging displaying model lines representing the `ReferenceIntersector` rays traced using `ReferenceIntersector`, conditionally compiled based on the pragma definition `DEBUG_GRAPHICAL`:</br>

<center>
<img src="img/rvtfader_graphical_debug_model_line.png" alt="Graphical debugging displaying model lines" width="339"/>
</center>

- `AttenuationCalculator` taking walls and door openings into account:</br>

<center>
<img src="img/rvtfader_attenuation_with_doors.png" alt="Attenuation calculation results" width="269"/>
</center>

For more details on the implementation steps, please refer to
the [list of releases](https://github.com/jeremytammik/RvtFader/releases)
and [commits](https://github.com/jeremytammik/RvtFader/commits).


#### <a name="6"></a> Further Reading

- **The Analysis Visualisation Framework AVF**:
    - An introduction to AVF programming basics is provided by Matt Mason's Autodesk University
class [CP5229 Seeing Data and More &ndash; The Analysis Visualization Framework](http://aucache.autodesk.com/au2011/sessions/5229/class_handouts/v1_CP5229-SeeingDataAndMore-TheAVFinRevitAPI.pdf)
([^](doc/cp5229_matt_mason_avf.pdf))
    - [Discussion of AVF by The Building Coder](http://thebuildingcoder.typepad.com/blog/avf)
- **`ReferenceIntersector` ray tracing**:
    - The `ReferenceIntersector` was previously named [`FindReferencesByDirection`](http://thebuildingcoder.typepad.com/blog/2010/01/findreferencesbydirection.html)
    - [Dimension walls using `FindReferencesByDirection`](http://thebuildingcoder.typepad.com/blog/2011/02/dimension-walls-using-findreferencesbydirection.html)
    - [Intersect Solid Filter, AVF vs DirectShape Debugging](http://thebuildingcoder.typepad.com/blog/2015/07/intersect-solid-filter-avf-and-directshape-for-debugging.html)
    - [Using `ReferenceIntersector` in linked files](http://thebuildingcoder.typepad.com/blog/2015/07/using-referenceintersector-in-linked-files.html)
- **Signal attenuation**:
    - [Attenuation](https://en.wikipedia.org/wiki/Attenuation)
    - [Modelling Signal Attenuation in IEEE 802.11 Wireless LANs - Vol. 1](http://www-cs-students.stanford.edu/~dbfaria/files/faria-TR-KP06-0118.pdf)
    - [The Basics of Signal Attenuation](http://www.dataloggerinc.com/content/resources/white_papers/332/the_basics_of_signal_attenuation/)
    - [RF Basics - Part 1](http://community.arubanetworks.com/aruba/attachments/aruba/tkb@tkb/121/1/RF-Basics_Part1.pdf) says "the free-space loss for 2.4 GHz at 100 meters from the transmitter is about 80 dB".


#### <a name="7"></a>Highlights

- External application with custom ribbon panel, custom tab, split button with main command and settings, always defaulting to main command.
- Settings storage in external JSON text file, displayed to user in Windows form, validated with detailed feedback.
