<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

MEP System Structure in Hierarchical JSON Graph #revitapi #3dwebcoder @AutodeskForge #ForgeDevCon #3dwebaccel

&ndash; ...

-->

### Hierarchical MEP System Structure Tree in Forge Viewer


synchronising the GitHub repository master and gh-pages branches:

I added the two `push` lines to `.git/config`:

<pre class="prettyprint">
[core]
  repositoryformatversion = 0
  filemode = true
  bare = false
  logallrefupdates = true
  symlinks = false
  ignorecase = true
  precomposeunicode = true
[remote "origin"]
  url = https://github.com/jeremytammik/TraverseAllSystems
  fetch = +refs/heads/*:refs/remotes/origin/*
  push = +refs/heads/master:refs/heads/gh-pages
  push = +refs/heads/master:refs/heads/master
[branch "master"]
  remote = origin
  merge = refs/heads/master
</pre>at the result of running the add-in in the UCS sample model and `rme_advanced_sample_model.rvt`.

Look

new [TraverseAllSystems](https://github.com/jeremytammik/TraverseAllSystems) add-in
to [traverse all MEP system graphs](http://thebuildingcoder.typepad.com/blog/2016/06/traversing-and-exporting-all-mep-system-graphs.html) and
export their connected hierarchical structure to JSON and XML that I am helping
the [USC](http://www.usc.edu) team with here at the San Francisco cloud accelerator.

<center>
<img src="img/2016-06_sf_accelerator.jpg" alt="San Francisco cloud accelerator" width="400">
</center>

I continued with that today, and also integrated a minor enhancement to RevitLookup:

- [TraverseAllSystems updates](#1)
- [Shared parameter creation](#2)
- [Options](#3)
- [Bottom-up JSON structure](#4)
- [Top-down JSON structure](#5)
- [TraversalTree JSON output generator](#6)
- [TreeNode JSON output generator](#7)
- [Download and to do](#8)
- [RevitLookup updates](#9)


#### <a name="1"></a>TraverseAllSystems Updates

The aim of the TraverseAllSystems project is to present the MEP system graphs in a separate tree view panel integrated in
the [Forge viewer](https://developer.autodesk.com/en/docs/viewer/v2/overview) and
hook up the tree view nodes bi-directionally with the 2D and 3D viewer elements.

To achieve that, I implemented a couple of significant enhancements over the simple XML file storage:

- Store the MEP system graph structure in JSON instead of XML
- Implement both bottom-up and top-down storage according to
the [jsTree JSON spec](https://www.jstree.com/docs/json).
- Support both element id and UniqueId node identifiers.
- Store the JSON output in a shared parameter attached to the MEP system element,
so that it is automatically included in the Forge SVF translation generated from the RVT input file.

Here is a list of the update releases so far:

- [2017.0.0.2](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.2) &ndash; implemented visited element dictionary to prevent infinite recursion loop
- [2017.0.0.3](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.3) &ndash; implemented DumpToJson
- [2017.0.0.4](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.4) &ndash; implemented shared parameter creation
- [2017.0.0.5](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.5) &ndash; implemented shared parameter value population, tested and verified graph structure json is written out
- [2017.0.0.6](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.6) &ndash; renamed json text field to name
- [2017.0.0.7](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.7) &ndash; implemented top-down json graph storage
- [2017.0.0.8](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.8) &ndash; automatically create shared parameter, eliminated separate command, wrap json strings in double quotes, validated json output

#### <a name="2"></a>


<pre class="prettyprint">
</pre>

#### <a name="6"></a>


<pre class="code">
</pre>


#### <a name="8"></a>Download and To Do

The current state of this project is available from
the [TraverseAllSystems GitHub repository](https://github.com/jeremytammik/TraverseAllSystems), and the version discussed above
is [release 2017.0.0.9](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.9).

The next step will consist of the Forge viewer extension implementation displaying a custom panel in the user interface hosting a tree view of the MEP system graphs and implementing two-way linking and selection functionality back and forth between the tree view nodes and the 2D and 3D viewer elements.

List of releases:


store entire json graph for all systems on project info instead of separate subgraph on each individual system


Do the Elements and ComnnectorManager properties return all system elements?

http://thebuildingcoder.typepad.com/blog/2013/02/simple-mep-system-traversal.html

/a/src/forge/usc-forge/

https://docs.google.com/document/d/1nWrnWucfnIahIosIFKagMYlkjITldb3lZ7NF_IUAlSU/edit?usp=sharing_eid&invite=CIS997cK&ts=57684551

https://github.com/kevinscake/usc-forge/

- usc fm project
  https://miniruby.atlassian.net/secure/WelcomeToJIRA.jspa jeremy.tammik@autodesk.com qy
  https://miniruby.atlassian.net/secure/RapidBoard.jspa?projectKey=FOR&useStoredSettings=true&rapidView=3
  https://docs.google.com/document/d/1nWrnWucfnIahIosIFKagMYlkjITldb3lZ7NF_IUAlSU/edit?usp=sharing_eid&invite=CIS997cK&ts=57684551



#### <a name="6"></a>Unity Scripting and C# Events

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



2017.0.0.18
@jeremytammik jeremytammik released this 15 hours ago

removed redundant top-level nodes and used hash code to generate unique jstree ids for top-level project, mechanical, electrical and piping nodes
Downloads
Source code (zip)
Source code (tar.gz)
 2017.0.0.17
 8a5e076
Edit
2017.0.0.17
@jeremytammik jeremytammik released this 2 days ago · 1 commit to master since this release

split into three domain specific subgraphs and add full element description
Downloads
Source code (zip)
Source code (tar.gz)
 2017.0.0.16
 ab1bd77
Edit
2017.0.0.16
@jeremytammik jeremytammik released this 4 days ago · 6 commits to master since this release

integrated pull request #1 by @ChengZY
Downloads
Source code (zip)
Source code (tar.gz)
 2017.0.0.15
 f312ebe
Edit
2017.0.0.15
@jeremytammik jeremytammik released this 4 days ago · 11 commits to master since this release

before integrating changes from ziyu
Downloads
Source code (zip)
Source code (tar.gz)
 2017.0.0.14
 6a4949b
Edit
2017.0.0.14
@jeremytammik jeremytammik released this 4 days ago · 13 commits to master since this release

store entire json graph for all systems on project info instead of separate subgraph on each individual system
Downloads
Source code (zip)
Source code (tar.gz)
 2017.0.0.13
 5adb06b
Edit
2017.0.0.13
@jeremytammik jeremytammik released this 4 days ago · 16 commits to master since this release

removed duplicate root id -1
Downloads
Source code (zip)
Source code (tar.gz)
 2017.0.0.12
 48ae4a2
Edit
2017.0.0.12
@jeremytammik jeremytammik released this 4 days ago · 17 commits to master since this release

added json sample data from rme_advanced_sample_model
Downloads
Source code (zip)
Source code (tar.gz)
 2017.0.0.11
 875799e
Edit
2017.0.0.11
@jeremytammik jeremytammik released this 4 days ago · 21 commits to master since this release

implemented test page in gh-pages and test link to it, split out test treedata into separate json file
Downloads
Source code (zip)
Source code (tar.gz)
 2017.0.0.10
 084fb12
Edit
2017.0.0.10
@jeremytammik jeremytammik released this 4 days ago · 24 commits to master since this release

implemented jstree test file and verified proper tree population





/a/doc/revit/tbc/git/a/img/custom_exporter_adn_mesh_json_crash.jpg

/a/doc/revit/tbc/git/a/img/custom_exporter_adn_mesh_json_crash_2.jpg
