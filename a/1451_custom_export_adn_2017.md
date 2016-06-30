<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

 #revitapi #3dwebcoder @AutodeskForge #ForgeDevCon #3dwebaccel

&ndash; ...

-->

### Custom Exporter for WebGL and Forge

I am back in Switzerland again and glad to be here.

I returned from the Forge DevCon developer conference and cloud accelerator in San Francisco via Vancouver, saving a little on airfare costs and enabling me to visit my son Christopher living there:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157670359072255" title="Vancouver"><img src="https://c8.staticflickr.com/8/7554/27886955751_5bda328ae8_n.jpg" width="320" height="240" alt="Vancouver"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

I was busy during the trip, rounding off the hierarchical MEP system exporter to the Forge viewer, working on an update of the ADN JSON mesh exporter, and, last but not least, helping Christopher getting started with C# scripting to drive audio effects for professional spatialised sound design for computer games:

- [More TraverseAllSystems updates](#2)
- [CustomExporterAdnMeshJson updates](#3)
- [Options to view RVT in WebGL](#4)
- [Getting started with Unity C# scripting](#5)


#### <a name="2"></a>More TraverseAllSystems Updates

At the San Francisco cloud accelerator, I started working on
the [TraverseAllSystems](https://github.com/jeremytammik/TraverseAllSystems) add-in
to [traverse all MEP system graphs](http://thebuildingcoder.typepad.com/blog/2016/06/traversing-and-exporting-all-mep-system-graphs.html) and
export their connected hierarchical structure XML and
a [top-down JSON graph](http://thebuildingcoder.typepad.com/blog/2016/06/store-mep-systems-in-hierarchical-json-graph.html) for
the [University of Southern California](http://www.usc.edu)
[Facilities Management](http://facilities.usc.edu)
[CAD Services](http://facilities.usc.edu/multisidebar_sublinks.asp?ItemID=236) team.

Here is a picture from the last day of [Forge Cloud Accelerator](http://autodeskcloudaccelerator.com) of
the entire team with me in the background, Cyrille Fauvel, Shiya Luo, Adam Nagy, and Philippe Leefsma:

<center>
<img src="img/2016-06-23_forge_accelerator_participants.jpg" alt="San Francisco cloud accelerator participants" width="500">
</center>

The aim of the TraverseAllSystems project is to present the MEP system graphs in a separate tree view panel integrated in
the [Forge viewer](https://developer.autodesk.com/en/docs/viewer/v2/overview) and
hook up the tree view nodes bi-directionally with the 2D and 3D viewer elements.

With the newest enhancements, it provides the following functionality:

- Store the MEP system graph structure in JSON
- Implement both bottom-up and top-down storage according to
the [jsTree JSON spec](https://www.jstree.com/docs/json).
- Support both element id and UniqueId node identifiers.
- Store the JSON output in a shared parameter attached to the MEP system element,
so that it is automatically included in the Forge SVF translation generated from the RVT input file.
- Store the entire JSON graph for all systems on the project info singleton element in the RVT file instead of separate subgraphs on each individual system
-


Here is a detailed list of the newest enhancements:

2017.0.0.10 &ndash; implemented jstree test file and verified proper tree population
2017.0.0.11 &ndash; implemented test page in gh-pages and test link to it, split out test treedata into separate json file
2017.0.0.12 &ndash; added json sample data from rme_advanced_sample_model
2017.0.0.13 &ndash; removed duplicate root id -1
2017.0.0.14 &ndash; store entire json graph for all systems on project info instead of separate subgraph on each individual system
2017.0.0.15 &ndash; before integrating pull request #1 from @ChengZY
2017.0.0.16 &ndash; integrated pull request #1 by @ChengZY
2017.0.0.17 &ndash; split into three domain specific subgraphs and add full element description
2017.0.0.18 &ndash; removed redundant top-level nodes and used hash code to generate unique jstree ids for top-level project, mechanical, electrical and piping nodes


The current state of this project is available from
the [TraverseAllSystems GitHub repository](https://github.com/jeremytammik/TraverseAllSystems), and the version discussed above
is [release 2017.0.0.9](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.9).

The next step will consist of the Forge viewer extension implementation displaying a custom panel in the user interface hosting a tree view of the MEP system graphs and implementing two-way linking and selection functionality back and forth between the tree view nodes and the 2D and 3D viewer elements.

Do the Elements and ComnnectorManager properties return all system elements?

http://thebuildingcoder.typepad.com/blog/2013/02/simple-mep-system-traversal.html

/a/src/forge/usc-forge/

https://docs.google.com/document/d/1nWrnWucfnIahIosIFKagMYlkjITldb3lZ7NF_IUAlSU/edit?usp=sharing_eid&invite=CIS997cK&ts=57684551

https://github.com/kevinscake/usc-forge/

- usc fm project
  https://miniruby.atlassian.net/secure/WelcomeToJIRA.jspa jeremy.tammik@autodesk.com qy
  https://miniruby.atlassian.net/secure/RapidBoard.jspa?projectKey=FOR&useStoredSettings=true&rapidView=3
  https://docs.google.com/document/d/1nWrnWucfnIahIosIFKagMYlkjITldb3lZ7NF_IUAlSU/edit?usp=sharing_eid&invite=CIS997cK&ts=57684551


One of the main new features is the online jsTree test page, hosted by gh-pages at

There is currently one manual step involved in generating the jsTree test: I copy the JOSN graph string from the Visual Studio debug output window to the respective JSON sounrce files in the test subfolder. That could also easily be automated, but for now, I will leave it as it is.


#### <a name="3"></a>CustomExporterAdnMeshJson Updates

/a/doc/revit/tbc/git/a/img/custom_exporter_adn_mesh_json_crash.jpg

/a/doc/revit/tbc/git/a/img/custom_exporter_adn_mesh_json_crash_2.jpg

#### <a name="4"></a>Options to view RVT in WebGL

#### <a name="5"></a>Unity Scripting and C# Events

My son [Christopher](http://tammik.ca) is learning C# for Unity scripting to implement his own personal sound design toolbelt within that framework.

I am very impressed by the super powerful and clean C# Unity API, and also by the rapid progress Chritopher is making.

For instance, he is implementing his own events, dictionaries, abstract base classes &ndash; impressive stuff for one or two days of coding.

For that events, the Unity discussion on [using C# events with Unity](http://forum.unity3d.com/threads/using-c-events-with-unity.58367) helped, especially the contribution by DifficultMass, supplying these lines of code:

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





<pre class="prettyprint">
</pre>

#### <a name="6"></a>

<pre class="code">
</pre>
