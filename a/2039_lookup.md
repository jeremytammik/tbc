<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>
</head>

<!---

- bimrras
  email [Re: [BIMrras] Interview Publication Tomorrow] Evelio E. Sánchez Juncal <eveliosanchez@spaplaneamento.com>
  https://www.bimrras.com/episodio/157-building-with-code-with-jeremy-tammik/?utm_source=newsletter&utm_medium=tuit&utm_campaign=episodio
  https://bit.ly/BIMrras157Bol
  2024-05-06_bimrras_157a.pdf
  /Users/jta/a/doc/revit/tbc/git/a/doc/2024-05-06_bimrras_157b.txt

twitter:

New releases of RevitLookup vastly expand coverage to include numerous new classes and properties, BIMrras podcast interview, linking Revit files in BIM360 Docs and Outline versus BoundingBox in the @AutodeskRevit #RevitAPI #BIM @DynamoBIM https://autode.sk/bimrras

New releases of RevitLookup vastly expand coverage to include numerous new classes and properties
&ndash; BIMrras podcast interview
&ndash; RevitLookup 2025.0.3
&ndash; RevitLookup 2025.0.4
&ndash; <code>Outline</code> versus <code>BoundingBox</code>
&ndash; Linking Revit files in BIM360 Docs...

linkedin:

New releases of RevitLookup vastly expand coverage to include numerous new classes and properties, BIMrras podcast interview, linking Revit files in BIM360 Docs and Outline versus BoundingBox in the #RevitAPI

https://autode.sk/bimrras

- BIMrras podcast interview
- RevitLookup 2025.0.3
- RevitLookup 2025.0.4
- Outline versus BoundingBox
- Linking Revit files in BIM360 Docs...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### RevitLookup Updates, Bounding Boxes and Podcast

A quick heads-up on a podcast interview, new releases of RevitLookup vastly expanding coverage to include numerous new classes and properties, and other notes of interest:

- [BIMrras podcast interview](#2)
- [RevitLookup 2025.0.3](#3)
- [RevitLookup 2025.0.4](#4)
- [`Outline` versus `BoundingBox`](#5)
- [Linking Revit files in BIM360 Docs](#6)

####<a name="2"></a> BIMrras Podcast Interview

Evelio Sánchez y Rogelio Carballo invited me to participate in
their [BIMrras Podcast](https://www.bimrras.com/):

> El Primer Podcast BIM Colaborativo
<br/>¡El podcast sobre BIM que Chuck Norris no se atreve a escuchar!

I joined them last week for a very pleasant chat in
episode [157 Building with code, with Jeremy Tammik](https://www.bimrras.com/episodio/157-building-with-code-with-jeremy-tammik/).

####<a name="3"></a> RevitLookup 2025.0.3

Roman [@Nice3point](https://t.me/nice3point) Karpovich, aka Роман Карпович,
published [RevitLookup release 2025.0.3](https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.3),
integrating extensive work
by [Sergey Nefyodov](https://github.com/SergeyNefyodov) to expand coverage to numerous new classes, properties and contexts.

Sergey packaged his enhancements in pull requests
[227 (`ConnectorManager`)](https://github.com/jeremytammik/RevitLookup/pull/227),
[228 (`Wire`)](https://github.com/jeremytammik/RevitLookup/pull/228),
[229 (`IndependentTag`)](https://github.com/jeremytammik/RevitLookup/pull/229),
[230 (`CurveElement`)](https://github.com/jeremytammik/RevitLookup/pull/230),
[231 (`TableView`)](https://github.com/jeremytammik/RevitLookup/pull/231),
[232 (`DatumPlane`)](https://github.com/jeremytammik/RevitLookup/pull/232)
and [233 (extensions)](https://github.com/jeremytammik/RevitLookup/pull/233).

Specific improvement include:

Memory diagnoser:

<center>
<img src="img/revitlookup_memory.png" alt="Memory diagnoser" title="Memory diagnoser" width="800"/> <!-- Pixel Height: 555 Pixel Width: 1,016 -->
</center>

- The `Memory` column shows the size of allocated **managed memory**
- Native ETW and allocations in C++ code are not included to avoid severe performance degradation

Different method overload variations now displayed in a `Variants` collection:

- Previously: `GeometryElement`
- Now: `Variants<GeometryElement>`

<center>
<img src="img/revitlookup_variants.png" alt="Overload variations" title="Overload variations" width="800"/> <!-- Pixel Height: 555 Pixel Width: 1,016 -->
</center>

More:

- ConnectorManager class support
  &ndash; Added `ConnectorManager.Lookup`
- Wire class support
  &ndash; Added `Wire.GetVertex`
- IndependentTag class support
  &ndash; Added `IndependentTag.CanLeaderEndConditionBeAssigned`, `IndependentTag.GetLeaderElbow`, `IndependentTag.GetLeaderEnd`, `IndependentTag.HasLeaderElbow`, `IndependentTag.IsLeaderVisible`
- CurveElement class support
  &ndash; Added `CurveElement.GetAdjoinedCurveElements`, `CurveElement.HasTangentLocks`, `CurveElement.GetTangentLock`, `CurveElement.HasTangentJoin`, `CurveElement.IsAdjoinedCurveElement`
- TableView class support
  &ndash; Added `TableView.GetAvailableParameters`, `TableView.GetCalculatedValueName`, `TableView.GetCalculatedValueText`, `TableView.IsValidSectionType`, `TableView.GetCellText`
- DatumPlane class support
  &ndash; Added `DatumPlane.CanBeVisibleInView`, `DatumPlane.GetPropagationViews`, `DatumPlane.CanBeVisibleInView`, `DatumPlane.GetPropagationViews`, `DatumPlane.GetDatumExtentTypeInView`, `DatumPlane.HasBubbleInView`, `DatumPlane.IsBubbleVisibleInView`, `DatumPlane.GetCurvesInView`, `DatumPlane.GetLeader`
- Extensions
  &ndash; Added Family class extension `FamilySizeTableManager.GetFamilySizeTableManager`, FamilyInstance class extension `AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds`, FamilyInstance class extension `AdaptiveComponentInstanceUtils.IsAdaptiveComponentInstance`, Solid class extension `SolidUtils.SplitVolumes`, Solid class extension `SolidUtils.IsValidForTessellation`
- [Full changelog 2025.0.2...2025.0.3](https://github.com/jeremytammik/RevitLookup/compare/2025.0.2...2025.0.3)
- [RevitLookup versioning](https://github.com/jeremytammik/RevitLookup/wiki/Versions)

####<a name="4"></a> RevitLookup 2025.0.4

As if that were not enough, Roman and Sergey immediately followed up
with [RevitLookup release 2025.0.4](https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.4),
integrating the further pull requests
[235](https://github.com/jeremytammik/RevitLookup/pull/235)
and [236](https://github.com/jeremytammik/RevitLookup/pull/236),
focused on improving core functionalities and robustness of the product.

- Introducing a preview feature for **Family Size Table**, making it easier to manage and visualize family sizes

<center>
<img src="img/revitlookup_family_size_table.png" alt="Family size table" title="Family size table" width="800"/> <!-- Pixel Height: 555 Pixel Width: 1,016 -->
</center>

To access it:

- Enable **Show Extensions** in the view menu
- Select any **FamilyInstance**
- Navigate to the **Symbol**
- Navigate to the **Family** (or just search for Family class objects in the **Snoop database** command)
- Navigate to the **GetFamilySizeTableManager** method
- Navigate to the **GetSizeTable** method
- Right-click on one of the tables and select the **Show table** command
- Note: Family size table is currently in read-only mode

More:

- Added new context menu item for selecting elements without showing
- Added new fresh, intuitive icons to the context menu for a more user-friendly interface.
- Refined labels, class names, and exception messages
- Resolved an issue where the delete action was not displayed in the context menu for ElementType classes
- Fixed the context menu display issue for Element classes, broken in previous release
- Fixed the order of descriptors to prevent missing extensions and context menu items in some classes, broken in previous release
- [Full changelog](https://github.com/jeremytammik/RevitLookup/compare/2025.0.3...2025.0.4)
- [RevitLookup versioning](https://github.com/jeremytammik/RevitLookup/wiki/Versions)

Ever so many thanks to Roman and Sergey for their impressive and untiring implementation and maintenance work!

####<a name="5"></a> Outline Versus BoundingBox

Interesting aspects of different kinds of bounding boxes and their uses in intersection filters are discussed in the thread
on [`Outline` vs `BoundingBoxXYZ` in Revit API](https://forums.autodesk.com/t5/revit-api-forum/outline-vs-boundingboxxyz-in-revit-api/m-p/12670522).

####<a name="6"></a> Linking Revit Files in BIM360 Docs

Several users asked whether it is possible to link Revit projects directly in ACC and BIM360 Docs.
Luckily, Eason Kang has covered that topic extensively in his article
on [BIM360 Docs: Setting up external references between files (Upload Linked Files)](https://aps.autodesk.com/blog/bim360-docs-setting-external-references-between-files-upload-linked-files).
