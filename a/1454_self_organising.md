<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

Selecting RVT Views for Forge Translation #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #ForgeDevCon

By default, the SVF translation process for the Forge Viewer extracts and transmits all 2D views from a Revit RVT BIM project file, but only the standard "{3D}" view. This behaviour can be modified manually by installing A360 Collaboration for Revit (C4R), launching 'Views for A360' and selecting the desired additional views. How can I programmatically select which views are being extracted from a Revit document so they show up in the Forge Viewer? 

-->

### Self-Organising Architecture, Voxel-Based Energy Analysis

I had an interesting chat last week with a group of students from the University of Augsburg.

The original question they raised was how to perform energy analysis on a bunch of autonomously assembled small building blocks, such as a colony of ants might put together.

The question is: how can the resulting small building blocks be converted to a valid Revit BIM that can be used for building performance analysis?

That led to the following topics:

- [BPA is now Insight360](#2)
- [Self-organising construction and architecture](#3)
- [Back to the ants &ndash; project summary](#4)
- [Q &amp; A](#5)
- [Two different energy model types](#6)


#### <a name="2"></a>BPA is now Insight360

Talking about [building performance analysis](http://autodesk.typepad.com/bpa),
I just noticed that [BPA has now become insight360](http://autodesk.typepad.com/bpa/2015/11/weve-moved-.html).

With the release of [Insight 360](https://insight360.autodesk.com),
the Autodesk Building Performance Analysis blog migrated to a new platform:

<center>
[blogs.autodesk.com/insight360](http://blogs.autodesk.com/insight360)
</center>


#### <a name="3"></a>Self-Organising Construction and Architecture

The Augsburg university has the only chair in Germany researching Self-Organising Construction in the architectural realm.

They are also hosting the [SASO 2016 conference](https://saso2016.informatik.uni-augsburg.de),
the 10th IEEE International Conference on Self-Adaptive and Self-Organizing Systems, University of Augsburg, Germany, September 12-16 2016.




#### <a name="4"></a>Back to the Ants &ndash; Project Summary

Here is the project summary, first in German, followed by my feeble attempt at beating Google translate:

Innovationen in Software, Robotik und 3D-Druck rücken Selbstorganisation als Planungs- und Konstruktionsansatz in greifbare Nähe. Die Vorteile sind mannigfach und reichen von der automatischen Generierung einer Vielzahl von Designs über deren ideale Integration in die gebaute Umgebung, deren strukturelle und automatisierte Optimierung bis zur dynamischen Adaptivität über lange Zeiträume. Motiviert durch neuste Erkenntnisse aus der Natur über die Konstruktionsweisen sozialer Insekten, werden in diesem Projekt Softwareansätze aufgezeigt, wie man Selbstorganisation in den Designprozess von Architektur einfließen lassen kann. Die wissenschaftlichen Arbeiten des Hauptreferenten umfassen beispielsweise das Design und die Optimierung selbstorganisierender Softwareagenten und deren produzierte Artefakte. Ein entsprechender programmatischer Ansatz zur selbstorganisierten Konstruktion wird vorgestellt, der die API des Revit 2016 Softwareframeworks nutzt.

<!-- 
Innovations in software, robotics and 3D printing ruecken self-organization as a planning and design approach in tangible proximity. The advantages are manifold, ranging from the automatic generation liking by their perfect integration in the built environment, whose structural and automated optimization to dynamic adaptivity for long periods. Motivated by the latest findings from nature about the construction methods of social insects, Softwareansaetze be shown how to make self-organization incorporated in the design process of architecture in this project. The scientific work of the keynote speakers include, for example, the design and optimization of self-organizing software agents and their produced artifacts. A corresponding programmatic approach to self-assembled structure is presented, which uses the API of Revit 2016 software frameworks.
-->

*Innovations in software, robotics and 3D printing enable self-organization as a feasible planning and design approach with many advantages, ranging from the automatic generation of huge numbers of designs to their perfect integration into the built environment, their structural and automated optimization, all the way to dynamic adaptivity over long periods of time. Motivated by the latest findings from nature about the construction methods used by social insects, we demonstrate software approaches to incorporate self-organization in the architectural design process. The scientific work of the main speaker includes the design and optimization of self-organizing software agents and their produced artefacts. We present a corresponding programmatic approach to self-assembled construction using the Revit API.*

That sounds pretty exciting to me.

<center>
<img src="img/ant.jpg" alt="Ant" width="246">
</center>



#### <a name="5"></a>Q &amp; A

Here are the notes from the Q &amp; A session we had in the conversation between Simon, Manuel, Sarah, Phil and Jeremy:

- C# vs Python?
    - The APIs are equivalent, all code is compiled to Intermediate Language (IL) that can be decompiled again using  [Reflector](http://www.red-gate.com/products/dotnet-development/reflector) and the .NET Reflection library, e.g. for reverse engineering.
- Families
    - Inefficient in this case, since the advantages of families are not used here and cost time and effort, both for implementation and at runtime.
    - DirectShape elements are better for this use case.
- DirectShape
    - Better performance, by several orders of magnitude.
    - Can be used as room dividers.
    - Can be assigned a category, e.g., Walls, Floors, Ceilings.
    - Minimal size ca. 1.2 mm, 1/16th of an inch.
    - All API lengths are handled in imperial feet.
- Analysis Cloud
    - [BPA blog](http://autodesk.typepad.com/bpa), now [Insight360 blog](http://blogs.autodesk.com/insight360).
    - The BPA team and various universities already implemented programmatic iteratively improving building analysis projects evaluating numerous variants.
    - Besides [Insight 360](https://insight360.autodesk.com) we might also want to evaluate the open source [EnergyPlus](https://energyplus.net) package.
- Journal files
    - Automatically log all Revit user interface interaction from every session.
    - Complete session can be reproduced for unit testing &ndash; or machine learning?
- Join Geometry
    - Join concrete elements or family definition geometry.
    - Irrelevant for DirectShape elements.
- Energy Analysis
    - Supports [two different energy model types](#5).
- Do DirectShape elements have to form a closed shell to make use of the energy analysis tools?
    - Open.



#### <a name="6"></a>Two Different Energy Model Types

This question has come up repeatedly in the past, so it is worthwhile pointing out again.

By default, the energy analysis is performed on rooms or spaces and treats their boundaries as vertical.

This is inappropriate in buildings using free-form geometry.

Therefore, a second setting was introduced in Revit 2016, so that we now have two different Energy Model Types: surface versus [voxel](https://en.wikipedia.org/wiki/Voxel).

Calculations are based either on the 2D plan surface views or by filling the entire building volumes with voxels, little sugar cubes.

The two different options are selected by settings in the `EnergyAnalysisDetailModelOptions` to base calculations either on spatial elements &ndash; i.e., rooms and spaces, assuming vertical boundaries &ndash; or bounded by actual building elements &ndash; i.e., floors, walls and ceilings, which may be arbitrarily shaped.

For more detail, please refer
to [What's New in the Revit 2016 API](http://thebuildingcoder.typepad.com/blog/2015/04/whats-new-in-the-revit-2016-api.html),
[Energy Analysis and gbXML API changes](http://thebuildingcoder.typepad.com/blog/2015/04/whats-new-in-the-revit-2016-api.html#4.06):

`EnergyModelType`

- SpatialElement &ndash; Energy model based on rooms or spaces. This is the default for calls when this option is not set, and matches the behaviour prior to Revit 2016.
- BuildingElement &ndash; Energy model based on analysis of building element volumes.

