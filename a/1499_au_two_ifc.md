<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

AU Day Two, IFC and Revit API Panel #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge #AU2016

The second day. I finally got to attend Angel Velez' IFC class &ndash; it always clashed with my own in previous years &ndash; followed by the product keynote and the Revit API panel discussion
&ndash; Angel Velez on IFC
&ndash; Product keynote
&ndash; Revit API panel
&ndash; Panel members
&ndash; Questions and answers
&ndash; Notes from previous Revit API panel sessions
&ndash; Session materials...

-->

### AU Day Two, IFC and Revit API Panel

The second day.

I finally got to attend Angel Velez' IFC class
&ndash; it always clashed with my own in previous years 
&ndash; followed by the product keynote and the Revit API panel discussion:

- [Angel Velez on IFC](#2)
- [Product keynote](#3)
- [Revit API panel](#4)
    - [Panel members](#5)
    - [Questions and answers](#6)
    - [Notes from previous Revit API panel sessions](#7)
    - [Session materials](#8)

Before diving into that, here is 
[a handful of snapshots from AU day two](https://flic.kr/s/aHskNSZLp8):

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157676670264565" title="AU2016 Day Two"><img src="https://c6.staticflickr.com/6/5348/30920166821_d5fc293b94_n.jpg" width="320" height="240" alt="AU2016 Day Two"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>


####<a name="2"></a>Angel Velez on IFC

Here are the slides and handout from Angel Velez' class **AR20462** *IFC Technical Overview and Survey of Autodesk Products, including Revit 2017*:

- [Handout](zip/ar20462_angel_velez_ifc_handout.pdf)
- [Slide deck](zip/ar20462_angel_velez_ifc_slides.pdf)

Their content is not identical, so you might want to go through both.

<center>
<img src="img/116_angel_velez_500.jpg" alt="Angel Velez" width="500">
</center>

I took a very few notes on some of the slides:

- 8: It makes no sense to talk of IFC without also specifying what MVD you are interested in. Each MVD specifies a subset of IFC that is mandatory, and another is forbidden
- 16: Use reference view MVD only if you cannot use transfer view. 
- 42: Check the handout for more info.
- 48: Explained in much greater detail in the repo, and step by step in handout.
- 55+57: You can completely redefine both link and open IFC yourself. Link is more fully supported, though.

Especially noteworthy: the number of downloads of the open source implementation over the course of the years, currently well over 140k:

<center>
<img src="img/ifc_open_source_downloads.png" alt="IFC open source download numbers over time from 2011 until today" width="500">
</center>

I snapped up one Forge and IFC related question:

<b>[Q]</b> What settings are used by the Forge RVT IFC translation?

<b>[A]</b> Forge RVT IFC translation uses default settings. We are discussing internally how to enable users to set specific additional options.


####<a name="3"></a>Product Keynote

Extremely fragmentary notes from the product keynote:

- Fusion 360, Shotgun, BIM 360...
- Project IQ pilot...
- Project Quantum: connect design, engineering, structural analysis, construction and fabrication.
This project will be presented in more detail this afternoon at 2-4 pm in Marco Polo 703, Level 1.
In case of interest, [email quantum@autodesk.com](mailto:quantum@autodesk.com).
Looks a bit like Dynamo...
- John Jacobs, CIO of [JE Dunn Construction](http://www.jedunn.com), talked about their use of Forge already today to significantly enhance and accelerate real live construction industry projects.



####<a name="4"></a>Revit API Panel

My last session of AU day two was the Revit API Panel **SD20891** *Revit API Expert Roundtable: Open House on the Factory Floor*:

- [Panel members](#5)
- [Questions and answers](#6)
- [Notes from previous Revit API panel sessions](#7)
- [Session materials](#8)


####<a name="5"></a>Panel Members

We have the following panel members:

- Angel Velez, Senior Principal Engineer, Autodesk
- Mikako Harada, AEC Technical Lead and Americas Manager, Autodesk
- Miroslav Schonauer, Solutions Software Architect, Autodesk
- Saikat Bhattacharya, Senior Technical Consulting Manager, Autodesk
- Sasha Crotty, Revit Core Product Manager, Autodesk
- Jeremy Tammik, Developer Support, Forge Platform Development

This represents an ideal combination of API designers, users and supporters.

Now we just await the participants input.

And here it comes!

####<a name="6"></a>Questions and Answers

Q. We heard that wishes now go into Revit Idea Station:

- [Revit Idea Station](http://forums.autodesk.com/t5/revit-ideas/idb-p/302)
- [Tag: API](http://forums.autodesk.com/t5/tag/API/tg-p/board-id/302)
- [Introducing the Revit Idea Station](http://thebuildingcoder.typepad.com/blog/2016/05/idea-station-and-textnote-bounding-box.html#2)

Will you pre-populate it with existing wish list items?

A. Please report them again. Please refer to the existing wish list numbers. We would like to clean up and separate the external ones from internal ones. All the existing wish list items still exist!

Q. How to separate product features and API requests?

A. Use the API tag.

Q. Access to the scale list?

A. We don’t know off-hand.

Q. Will you be exposing APIs for old missing objects, like ceilings?

A. All new functionality is required to be equipped with an API. Other than that, get many votes for it on Idea Station. Cut-off is currently 50, apparently.

Q. Suppressing messages?

A. There are several levels, cf. The Building Coder topic group
on [Detecting and Handling Dialogues and Failures](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.32).

Tell us what specific messages you have problems with. If all else fails, there is a Windows API that you can use as a workaround.

Q. Task dialogue ids are undocumented. We are forced to debug and reverse engineer to determine which id to catch. All info is in the UIFrameworkRes.dll, so it’s available. Could it be documented?

A. You can solve this more easily using logging. Every command can be logged. The log includes the id. Implement a log for everything you do, log all the dialogue info, the more info you have the better.

Q. Instantiating thousands of family instances (millions of bricks, in fact!) takes a long time. Also, a simple transformation. AutoCAD does this much faster.

A. Revit is more complicated. Submit a very specific reproducible case. We need a file to analyse. Analyse whether you can make it more effective. Revit was not designed for that kind of scale. Group things differently. Don’t model individual bricks, for instance.

Q. Multithreading support in the API?

A. Revit runs all API operations in a single thread. We cannot change this easily. We can move other operations into other threads. This applies to model modification. Extraction can conceivably run in a different worker thread. That might be exposed to the API sometime.

Q. Copy monitor in API?

A. No.

Q. Support programmatic material creation with images?

A. Look at the [public Revit roadmap](http://www.autodesk.com/revitroadmap):

<center>
[www.autodesk.com/revitroadmap](http://www.autodesk.com/revitroadmap)
</center>

It is on there (applause).

Q. Is there a significant difference between the size of a Revit family inside versus outside a project?

A. They should be pretty close. The contents are copied from the family into the project, some things are shared, reducing size, nested stuff increases it, …

Q. Does the size matter?

A. High level yes, but it depends. A few instances make no difference. Millions of instances do. So does complex geometry and complex joins. We have worked hard at improving scalability and performance. Views and navigation has been speeded up.

Q. Can I edit a group?

A. No, sorry. Ungroup and regroup.

Q. New functionality coming out is built using the API. How many developers are working on that kind of thing?

A. A lot of our teams. We build as many features as we can on top of the API. They may be working on internal functionality at the same time. API and core functionality… 

Q. Can you provide more examples using Python? 

A. Nothing planned at this point. Good idea though. Log it.

Q. Point snapping when you manually instantiate. Can that be monitored? We would like to snap to specific points. A snap filter.

A. Not currently.

Q. Documentation has very little detail. Could this be published in a wiki-style format, so we can add comments?

A. We are looking at that. We need something more scalable. There are challenges.

By the way, look at [Revit API docs, web-hosted CHM content](http://thebuildingcoder.typepad.com/blog/2016/10/token-expiry-and-online-revit-api-docs.html#2):

<center>
[www.revitapidocs.com](http://www.revitapidocs.com)
</center>

Q. Can we get location points from free-form elements? And rotation?

A. Use `DirectShape` instead. We use that a lot, and it is being actively developed.

Q. Can we use free-form surfaces with `DirectShape`?

A. The Revit 2015 API introduced the tessellated shape builder. In 2017 the b-rep shape builder was added for curved surfaces. The original Dynamo builder was built early. If the B-rep builder fails, it tries to use the tessellated one.

Q. In the worksharing tooltip info, can the dates be exposed as well?

A. Not to our knowledge.

Q. How can I know when `PostCommand` is complete? Or a modal window?

A. Maybe using the Idling event? Or the progress changed notification?

Q. Can I host a fixture to linked elements?

A. We guess not. One participant adds: I wouldn’t do that. We tried it. As an MEP coordinator, we explicitly un-host things for many reasons. Another participant copies the fixture to keep track of …

Q. How much custom development was done to integrate Dynamo?

A. Dynamo uses the Revit API. It may use one or two undocumented API hooks, but definitely nothing fundamental. If it was, it should be made public.

Q. Is the Dynamo player supported by the API?

A. It has no additional API at all. It is simply built on top of Dynamo and the public Revit API.

Q. Can I control the visibility state of face edges, hide specific face edges? E.g., for multiple tessellated faces, hide internal edges.

A. No. In the UI, you might be able to manually override. In direct shape, you have control over categories, and could use those to control visibility.

Q. What do you say about [ODA Teigha BIM](http://thebuildingcoder.typepad.com/blog/2016/09/roomedit3d-broadcast-teigha-bim-and-forge-webinar-3.html#3)?

A. No comment. We do however see that there is considerable interest for that sort of thing out there.

Q. Will we see Forge design automation support for Revit?

A. Yes, we are looking at that. The Forge model derivative API already supports generating IFC and DWG from RVT.

Q. Access to link visibility settings?

A. No.

Q. Access to element relationships? Stair to handrail, e.g.

A. In many cases there is a one-directional relationship.
You can easily invert it, cf. blog post number 16 from 2008 on
the [relationship inverter](http://thebuildingcoder.typepad.com/blog/2008/10/relationship-in.html).

You can use filtered element collectors to very effectively implement such a relationship inverter.

Q. Debug and continue?

A. Yes! Please refer to the recent blog post on [edit and continue](http://thebuildingcoder.typepad.com/blog/2016/10/ai-edit-and-continue.html#2) using 'attach to process' and the AddInManager.

Q. New database connections, e.g. SQL server 2016?

A. Please submit to Revit Idea Station.

Q. Will you recycle aging Revit Idea Station entries?

A. Yes, if a wish sits for months with few votes it will be archived.

Q. Is 50 the right threshold for API wishes?

A. The number one API wish list has been implemented and had more votes than that. We may consider lower rated wishes as well.


####<a name="7"></a>Notes from Previous Revit API Panel Sessions

Here are the notes from previous similar Revit API panel sessions:

- [SD5156 &ndash; The Revit API Expert Panel at Autodesk University 2014](http://thebuildingcoder.typepad.com/blog/2014/12/the-revit-api-panel-at-autodesk-university.html#1)
- [The Building Coder Revit API Panel at the Revit Technology Conference RTC Europe 2015](http://thebuildingcoder.typepad.com/blog/2015/11/rtc-budapest-and-the-revit-api-panel.html#5)
- [SD10181 &ndash; Revit API Expert Roundtable at Autodesk University 2015](http://thebuildingcoder.typepad.com/blog/2015/12/au-keynote-and-revit-api-panel.html#9)
- [RTC 2016 Revit API Panel, Idea Station, Edit and Continue](http://thebuildingcoder.typepad.com/blog/2016/10/rtc-revit-api-panel-idea-station-edit-and-continue.html)


####<a name="8"></a>Session Materials

Here are the class materials:

- [Handout](zip/sd20908_connect_desktop_cloud_handout.pdf)
- [Slide deck](zip/sd20908_connect_desktop_cloud_slides.pdf)

<center>
<img src="img/au2016_2.jpeg" alt="AU2016" width="624">
</center>

