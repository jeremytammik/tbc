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


twitter:

 the @AutodeskRevit #RevitAPI #BIM @DynamoBIM

...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

###


####<a name="2"></a>

- link revit files in acc docs
  https://aps.autodesk.com/blog/bim360-docs-setting-external-references-between-files-upload-linked-files
  we need to link 2 revit file (1 architectural and 1 structural) in ACC project via APS API.
  https://stackoverflow.com/questions/78279915/aps-data-management-api-bim360-currently-does-not-support-the-creation-of-refs
  https://aps.autodesk.com/blog/bim360-docs-setting-external-references-between-files-upload-linked-files

- RevitLookup 2025.0.3
  https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.3
  ## General
  - **Memory diagnoser**
    ![изображение](https://github.com/jeremytammik/RevitLookup/assets/20504884/dfa0fc23-5a63-452d-8a73-25009424c99c)
    Memory column contains the size of allocated **managed memory**.
    Native ETW and allocations in C++ code are not included to avoid severe performance degradation.
  ## Improvements
  - The different method overloading variations, are now displayed in the `Variants` collection
    ![изображение](https://github.com/jeremytammik/RevitLookup/assets/20504884/22d8c84b-097c-4da3-9dfa-f091a6de9b7f)
    Previous: **GeometryElement**
    Now: **Variants\<GeometryElement\>**
  - ConnectorManager class support
      - Added `ConnectorManager.Lookup` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/227
  - Wire class support
      - Added `Wire.GetVertex` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/228
  - IndependentTag class support
      - Added `IndependentTag.CanLeaderEndConditionBeAssigned` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/229
      - Added `IndependentTag.GetLeaderElbow` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/229
      - Added `IndependentTag.GetLeaderEnd` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/229
      - Added `IndependentTag.HasLeaderElbow` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/229
      - Added `IndependentTag.IsLeaderVisible` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/229
  - CurveElement class support
      - Added `CurveElement.GetAdjoinedCurveElements` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/230
      - Added `CurveElement.HasTangentLocks` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/230
      - Added `CurveElement.GetTangentLock` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/230
      - Added `CurveElement.HasTangentJoin` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/230
      - Added `CurveElement.IsAdjoinedCurveElement` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/230
  - TableView class support
      - Added `TableView.GetAvailableParameters` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/231
      - Added `TableView.GetCalculatedValueName` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/231
      - Added `TableView.GetCalculatedValueText` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/231
      - Added `TableView.IsValidSectionType` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/231
      - Added `TableView.GetCellText` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/231
  - DatumPlane class support
      - Added `DatumPlane.CanBeVisibleInView` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/232
      - Added `DatumPlane.GetPropagationViews` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/232
      - Added `DatumPlane.CanBeVisibleInView` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/232
      - Added `DatumPlane.GetPropagationViews` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/232
      - Added `DatumPlane.GetDatumExtentTypeInView` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/232
      - Added `DatumPlane.HasBubbleInView` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/232
      - Added `DatumPlane.IsBubbleVisibleInView` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/232
      - Added `DatumPlane.GetCurvesInView` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/232
      - Added `DatumPlane.GetLeader` support by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/232
  - Extensions:
    - Added Family class extension `FamilySizeTableManager.GetFamilySizeTableManager` by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/233
    - Added FamilyInstance class extension `AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds`
    - Added FamilyInstance class extension `AdaptiveComponentInstanceUtils.IsAdaptiveComponentInstance`
    - Added Solid class extension `SolidUtils.SplitVolumes`
    - Added Solid class extension `SolidUtils.IsValidForTessellation`
  Full changelog: https://github.com/jeremytammik/RevitLookup/compare/2025.0.2...2025.0.3
  RevitLookup versioning: https://github.com/jeremytammik/RevitLookup/wiki/Versions


<pre><code class="language-cs">
</code></pre>


<center>
<img src="img/" alt="" title="" width="99"/> <!-- Pixel Height: 358 Pixel Width: 599 -->
</center>


