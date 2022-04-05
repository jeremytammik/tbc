<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

- Migrated CustomExporterAdnMeshJson from Revit 2014 to Revit 2017
  https://github.com/jeremytammik/CustomExporterAdnMeshJson/releases/tag/2017.0.0.1
  prompted by comments from Helen Huang

Exporting RVT BIM to WebGL and Forge #revitapi #3dwebcoder @AutodeskForge #ForgeDevCon #3dwebaccel

Exporting RVT #BIM to #JSON, #WebGL and @AutodeskForge #revitapi #3dwebcoder @AutodeskRevit #threejs


I am happy to say that I returned safe and sound to Switzerland from my travels for the Forge DevCon and Cloud Accelerator. Since then, I rounded off the hierarchical MEP system exporter to a JSON graph for interaction with it in the Forge viewer, worked on an update of the ADN JSON mesh exporter and helped my son Christopher getting started with C# scripting to drive audio effects for professional spatialized sound design for computer games
&ndash; More TraverseAllSystems updates
&ndash; CustomExporterAdnMeshJson updates
&ndash; ADN JSON Exporter Alternatives to View RVT in WebGL
&ndash; vA3C
&ndash; The Forge Viewer
&ndash; Getting started with Unity C# scripting
&ndash; Opendesk at Autodesk Forge Accelerator Barcelona...

-->

### Exporting RVT BIM to JSON, WebGL and Forge

I am happy to say that I returned safe and sound to Switzerland from my long stay in San Francisco for
the [Forge DevCon](http://forge.autodesk.com/conference) developer conference
and [Cloud Accelerator](http://autodeskcloudaccelerator.com).

I travelled there via Vancouver, paradoxically saving a little bit on airfare costs:

<!-- and enabling me to visit my son Christopher -->

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157670359072255" title="Vancouver"><img src="https://c8.staticflickr.com/8/7554/27886955751_5bda328ae8_n.jpg" width="320" height="240" alt="Vancouver"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

During the trip back and since, I rounded off the hierarchical MEP system exporter to a JSON graph for interaction with it in
the [Forge viewer](https://developer.autodesk.com/en/docs/viewer/v2/overview/),
worked on an update of the ADN JSON mesh exporter, and, last but not least, helped Christopher getting started with C# scripting to drive audio effects for professional spatialized sound design in Unity computer games:

- [More TraverseAllSystems updates](#2)
- [CustomExporterAdnMeshJson updates](#3)
- [ADN JSON Exporter Alternatives to View RVT in WebGL](#4)
    - [vA3C](#5)
    - [The Forge Viewer](#6)
- [Getting started with Unity C# scripting](#7)
- [Opendesk at Autodesk Forge Accelerator Barcelona](#8)


#### <a name="2"></a>More TraverseAllSystems Updates

At the San Francisco cloud accelerator, I started working on
the [TraverseAllSystems](https://github.com/jeremytammik/TraverseAllSystems) add-in
to [traverse all MEP system graphs](http://thebuildingcoder.typepad.com/blog/2016/06/traversing-and-exporting-all-mep-system-graphs.html) and
export their connected hierarchical structure to XML files and
a [top-down JSON graph](http://thebuildingcoder.typepad.com/blog/2016/06/store-mep-systems-in-hierarchical-json-graph.html) for
the [University of Southern California](http://www.usc.edu)
[Facilities Management](http://facilities.usc.edu)
[CAD Services](http://facilities.usc.edu/multisidebar_sublinks.asp?ItemID=236) team.

Here is a picture from the last day at
the [Forge Cloud Accelerator](http://autodeskcloudaccelerator.com) of
the entire USC team, other accelerator participants, and, from the Autodesk side, me, Cyrille Fauvel, Augusto Gonçalves, Shiya Luo, Adam Nagy, Zhong John Wu, Xiaodong Liang and Philippe Leefsma:

<center>
<img src="img/2016-06-23_forge_accelerator_participants.jpg" alt="San Francisco cloud accelerator participants" width="500">
</center>

The aim of the TraverseAllSystems project is to present the MEP system graphs in a separate tree view panel integrated in
the [Forge viewer](https://developer.autodesk.com/en/docs/viewer/v2/overview) and
hook up the tree view nodes bi-directionally with the 2D and 3D viewer elements.

The [simple MEP system traversal](http://thebuildingcoder.typepad.com/blog/2013/02/simple-mep-system-traversal.html) shows
how to retrieve an unsorted list of system elements, if the connection graph is not needed.

With the newest enhancements, TraverseAllSystems provides the following functionality:

- Store the MEP system graph structure in JSON.
- Implement both bottom-up and top-down storage according to
the [jsTree JSON spec](https://www.jstree.com/docs/json).
- Support both element id and UniqueId node identifiers.
- Store the JSON output in a shared parameter attached to the MEP system element,
so that it is automatically included in the Forge SVF translation generated from the RVT input file.
- Store the entire JSON graph for all systems on the project info singleton element in the RVT file instead of separate subgraphs on each individual system.
- Implement a [jsTree test page](https://jeremytammik.github.io/TraverseAllSystems/test) on GitHub pages.
- Integrate [pull request #1](https://github.com/jeremytammik/TraverseAllSystems/pull/1)
from [Ziyu Cheng](https://github.com/ChengZY) adding
single level trees listing all elements of each system.

One of the main new features is the gh-pages hosted online [jsTree test page](https://jeremytammik.github.io/TraverseAllSystems/test).

There is currently one manual step involved in generating the jsTree test page: I copy the JOSN graph string from the Visual Studio debug output window to the respective JSON source files in the test subfolder. That could easily be automated, but for now, I will leave it as it is.

Here is the detailed list of the newer releases:

- [2017.0.0.10](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.10) &ndash; implemented jstree test file and verified proper tree population.
- [2017.0.0.11](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.11) &ndash; implemented test page in gh-pages and test link to it, split out test treedata into separate json file.
- [2017.0.0.12](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.12) &ndash; added json sample data from rme_advanced_sample_model.
- [2017.0.0.13](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.13) &ndash; removed duplicate root id -1.
- [2017.0.0.14](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.14) &ndash; store entire json graph for all systems on project info instead of separate subgraph on each individual system.
- [2017.0.0.15](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.15) &ndash; before integrating pull request #1 from @ChengZY.
- [2017.0.0.16](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.16) &ndash; integrated pull request #1 by @ChengZY.
- [2017.0.0.17](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.17) &ndash; split into three domain specific subgraphs and add full element description.
- [2017.0.0.18](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.18) &ndash; removed redundant top-level nodes and used hash code to generate unique jstree ids for top-level project, mechanical, electrical and piping nodes.

The current state of this project is available from
the [TraverseAllSystems GitHub repository](https://github.com/jeremytammik/TraverseAllSystems), and the version discussed above
is [release 2017.0.0.18](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.18).

As said, the next step will consist of the Forge viewer extension implementation displaying a custom panel in the user interface hosting a tree view of the MEP system graphs and implementing two-way linking and selection functionality back and forth between the tree view nodes and the 2D and 3D viewer elements.

<!--- Do the Elements and ComnnectorManager properties return all system elements?

/a/src/forge/usc-forge/

https://docs.google.com/document/d/1nWrnWucfnIahIosIFKagMYlkjITldb3lZ7NF_IUAlSU/edit?usp=sharing_eid&invite=CIS997cK&ts=57684551

https://github.com/kevinscake/usc-forge/

- usc fm project
  https://miniruby.atlassian.net/secure/WelcomeToJIRA.jspa jeremy.tammik@autodesk.com qy
  https://miniruby.atlassian.net/secure/RapidBoard.jspa?projectKey=FOR&useStoredSettings=true&rapidView=3
  https://docs.google.com/document/d/1nWrnWucfnIahIosIFKagMYlkjITldb3lZ7NF_IUAlSU/edit?usp=sharing_eid&invite=CIS997cK&ts=57684551

--->


#### <a name="3"></a>CustomExporterAdnMeshJson Updates

During the trip back to Europe, I had an extensive correspondence with Helen Huang via a series of comments
on [The Building Coder](http://thebuildingcoder.typepad.com) discussion
on the [ADN Mesh Data Custom Exporter to JSON](http://thebuildingcoder.typepad.com/blog/2013/07/adn-mesh-data-custom-exporter-to-json.html):

**Question:** I tried to use the code to export the Revit sample project to JSON.

However, I got a 'cannot divide by zero' exception in the `CustomerExporter.Export` function.

My Revit version is 2016.

I wonder if you have a version of the JSON Exporter that works with this version of Revit.

Much thanks for your help!

**Answer:** I no longer have Revit 2016 installed, so I cannot say.

I am sure you will be able to find one.

I do not think that the version of JSON library has any effect on the custom exporter.

Good luck!

**Question:** I wonder if you can also help me to understand the meaning of NormalIndices in your example.
Not sure why all vertices are repeated three times here... Much thanks for your help!

<pre>
  "NormalIndices":[0,0,0, 1,1,1, 2,2,2, 3,3,3]
</pre>

**Answer:** Yes, this is easy to explain, although it highlights a rather inefficient aspect of the data structure used in this sample.

There are four triangles, each with three vertices, resulting in a total count of 12 vertices.

Hence the 12 numbers in the list of normal indices.

Each one of these is an index into the list of normals.

`Normals` defines coordinates for four normal vectors, one for each triangle, hence the values of these 12 numbers range from 0 to 3.

Each group of 3 numbers specifies the normal vector for three triangle vertices.

Each triangle has the same normal vector at each of its corners, so each number repeats three times.

The result is flat triangles, with sharp corners between each of them.

I hope this clarifies.

**Response:**
I can upgrade to Revit 2017 if that is the one you are using. Let me try the exporter again with Revit 2017 and see if it works. Thanks!

**Answer:** I created
a [GitHub repository for the CustomExporterAdnMeshJson project](https://github.com/jeremytammik/CustomExporterAdnMeshJson),
[migrated the code to Revit 2017](https://github.com/jeremytammik/CustomExporterAdnMeshJson/releases/tag/2017.0.0.0) for
you, and performed a successful test using [version 2017.0.0.1](https://github.com/jeremytammik/CustomExporterAdnMeshJson/releases/tag/2017.0.0.1).

**Response:** I tried the new code with Revit 2017, unfortunately I still got the same exception. I took a couple of screenshots for you. Can you please take a look and see what might be wrong? Also, I wonder if you can share the source code of the exporter. I will be able to step through the code find out more information for you to debug that way.

By the way my Revit 2017 is a trial version. Do you think it matters?

The following are the steps to reproduce. Hope they are helpful.

- Build latest code
- Run Revit 2017, open the rac_basic_sample_project in 3D view. Select one object, like the roof, and click the add in, and the exception will be thrown. I tried selection multiple objects, and also not selecting any objects before trying the exporter, but all ends up with the same exception.

Much thanks again for your help!

**Answer:** Thank you very much for your testing and pointing this out.

No, I do not have access to the source code of the exporter, so I cannot provide that.

Yes, I can reproduce what you describe:

<center>
<img src="img/custom_exporter_adn_mesh_json_crash_3.png" alt="Custom JSON exporter exception" width="500">
</center>

I removed the some of the calls to throw an exception in the custom export context class, but I still see the following lines logged in the Visual Studio debug output window just before and after the export context Finish method is called:

<pre>
  InstanceEnd
  ElementEnd
  ElementBegin id 423099
  ElementEnd
  Exception thrown: 'Autodesk.Revit.Exceptions.ArgumentException' in RevitAPI.dll
  Finish
  Exception thrown: 'Autodesk.Revit.Exceptions.ExternalApplicationException' in RevitAPI.dll
</pre>

Sorry, I cannot explore this further myself, so I raised the issue <i><b>REVIT-94525</b> CustomExporter throws ExternalApplicationException after call to Finish</i> with the development team for them to analyse in more depth.

I hope there is a simple solution to this.

Later: Unexpected good news: wrap the call to exporter.Export in an exception handler and all is well,
cf. [release 2017.0.0.3](https://github.com/jeremytammik/CustomExporterAdnMeshJson/releases/tag/2017.0.0.3).

I rather suspect that this add-in code is flawed in some manner, though.

I hope the devteam will clarify based on the exploration of REVIT-94525.

Meanwhile, you have a stop-gap solution.

**Response:** Thank Jeremy for your quick response. Can you please let me know how to follow up the issue that you have opened, REVIT-94525?

**Answer:** All you can do about an internal development issue is wait, and prompt me or one of my colleagues for an update status when you feel you have waited long enough. The best channel for such a prompt is the [Revit API discussion forum](http://forums.autodesk.com/t5/Revit-API/bd-p/160), since that is monitored by the entire developer support team.

Sorry, that we cannot provide an externally accessible online access to the development database.

#### <a name="4"></a>ADN JSON Exporter Alternatives to View RVT in WebGL

**Response:** Thanks a lot for your time and effort. I tried your latest code, revision 2017.0.0.3, and was able to export the model finally. However, it does not seem right after I loaded it with the json/webgl viewer mentioned on this blog. Some of the components of the building are missing. I guess these components could be the ones that have caused the exceptions that we have seen before.

I also looked at the exported json model, and found it does not contain the information on materials and textures...
I wonder if you have another exporter which does export this information, along with the faces, indices and normals?
It does not have to use this particular json format...

Our goal is to export the Revit model and show it in webgl, and hopefully, when a user clicks any component
of the model in the web browser, we shall be able to display more information for that component, like the component's name and id... I guess we might have to come up with our own exporter at some point. but can you please point us to some good examples to get started...

Really appreciate your help!

**Answer:** Thank you for your appreciation and extremely sensible question.

I was planning to get to that as well, to ask you what your final goal actually is.

This sample is really targeted at just demonstrating the Revit custom exporter and the custom ADN JSON format.

For real-world use, however, forget it.

Happily, there are lots of alternatives.

The two alternatives of choice would be [vA3C](#5) and the [Forge Viewer](#6).

#### <a name="5"></a>vA3C

[vA3C](https://va3c.github.io) is an open-source WebGL AEC viewer, complete with a C# .NET Revit API add-in exporter. The advantage of this one is that it is completely open source, also based on the custom exporter, this time exporting the complete model to the
standard [three.js WebGL format](http://threejs.org).
Its disadvantages: lack of support for materials and textures, does not perform well for really large models.

#### <a name="6"></a>The Forge Viewer

The [Forge Viewer](https://developer-autodesk.github.io) is
an important part of the official Autodesk Forge platform with full support for materials, textures, and really large models. In fact, it used to be nicknamed LMV for large model viewer.

The Forge viewer is completely open source and also based on three.js WebGL. It consumes a JSON stream in a format named `SVF`.
The Forge platform supports translation of over 60 CAD seed file formats to SVF, so you can display and interactively manipulate views coming from  basically any standard CAD format.

The translation process is not open source and happens on Autodesk servers.

All use of the entire Forge platform is completely free for all research and software development. Customers can use it for free up to a certain limit. The exact limit is currently in the process of being defined, and until September 15 absolutely all usage of Forge is completely free.

Probably, the Forge viewer is the most promising possibility for you to explore further.

For more information, please refer to:

- [vA3C](http://thebuildingcoder.typepad.com/blog/2014/05/rvtva3c-revit-va3c-generic-aec-viewer-json-export.html)
- [Custom exporter](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.1)
- [Forge Platform API](https://developer.autodesk.com)
- [Forge](http://forge.autodesk.com)

For more information on samples connecting Revit models with Forge, please refer to my recent presentation
on [Freeing your BIM data](http://thebuildingcoder.typepad.com/blog/2016/06/free-your-bim-data-and-roomedit3d-thee-legged-forge-oauth.html).

I also implemented a number of earlier samples on connecting the desktop and the cloud, e.g., RoomEditorApp, FireRatingCloud and Roomedit3d, all available from GitHub and extensively documented by The Building Coder.


#### <a name="7"></a>Unity Scripting and C# Events

<!-- My son [Christopher](http://tammik.ca) is a -->

A professional independent sound designer friend is specialising in audio games and now learning C# for Unity scripting to implement their own personal sound design toolbelt within that framework.

I am very impressed by the super powerful, clean, effective C# Unity API, and by the rapid progress they are making with it.

For instance, they are implementing their own events using delegates, dictionaries, abstract base classes, timers, separate threads, called co-routines in Unity &ndash; impressive stuff for just a couple of days of coding.

For the delegate implementation, the Unity discussion on [using C# events with Unity](http://forum.unity3d.com/threads/using-c-events-with-unity.58367) helped, especially the contribution by DifficultMass, supplying these lines of code:

<pre class="code">
  class EventTriggerer
  {
    public event EventHandler OurEvent;

    public void TriggerEvent()
    {
      if(OurEvent != null)
      {
        OurEvent();
      }
    }
  }
</pre>

<!--

Here is an early snapshot of the project as WIP:

<center>
<blockquote class="twitter-tweet" data-lang="en"><p lang="en" dir="ltr">I have been working on this <a href="https://twitter.com/hashtag/gameaudio?src=hash">#gameaudio</a> script for <a href="https://twitter.com/unity3d">@unity3d</a> over the weekend. Loads more ideas I want to add... <a href="https://twitter.com/hashtag/VR?src=hash">#VR</a> <a href="https://t.co/07ezzqcgKT">pic.twitter.com/07ezzqcgKT</a></p>&mdash; Chris Tammik (@chtammik) <a href="https://twitter.com/chtammik/status/747289609973862404">June 27, 2016</a></blockquote>
<script async src="http://platform.twitter.com/widgets.js" charset="utf-8"></script>
</center>
-->

#### <a name="8"></a>Opendesk at Autodesk Forge Accelerator Barcelona

For some background info on Forge and the
recent [Forge accelerator](http://autodeskcloudaccelerator.com) in
Barcelona, here is feedback from one of the participating
companies, [Opendesk at the Forge Accelerator](https://www.opendesk.cc/blog/opendesk-at-autodesk).
