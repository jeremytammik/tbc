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

- The difference between Length and ApproximateLength in Curve class
  https://forums.autodesk.com/t5/revit-api-forum/the-difference-between-length-and-approximatelength-in-curve/m-p/12841543/


twitter:

Revit API discussions on @AutodeskRevit #RevitAPI add-in licensing and multi-version use of the AppStore Entitlement API #BIM @DynamoBIM https://autode.sk/entitlement

Two illuminating posts from the Revit API discussion forum on licensing and the entitlement API
&ndash; Multi-version Revit entitlement API
&ndash; Add-in licensing...

linkedin:

Revit API discussions on #RevitAPI add-in licensing and multi-version use of the AppStore Entitlement API:

https://autode.sk/entitlement

- Multi-version Revit entitlement API
- Add-in licensing...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Length Query and RevitLookup DLL Paradise


####<a name="2"></a>

<pre><code class="language-cs">public static bool CheckOnline( string appId, string userId )

</code></pre>



<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 430 Pixel Width: 496 -->
</center>


Many thanks to all of you for sharing this.

####<a name="2"></a> RevitLookup Dependency Isolation Ends DLL Hell

The new [RevitLookup release 2025.0.8](https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.8) runs
in an isolated container for addin dependencies.

This new capability prevents conflicts and compatibility issues arising from different library versions between plugins, ensuring a more stable and reliable environment for plugin execution.

This enhancement uses the `Nice3point.Revit.Toolkit` to manage the isolation process, effectively eliminating DLL conflicts.
By integrating this package, RevitLookup ensures a consistent and predictable user experience.

Detailed description how it works: https://github.com/Nice3point/RevitToolkit/releases/tag/2025.0.1

Dependency isolation is available starting with Revit 2025.
Note that the isolation mechanism is implemented by an additional library that must be loaded into Revit at first startup for it to work.
Therefore, if your other plugins use `Nice3point.Revit.Toolkit`, it must be updated to version `2025.0.1`, which introduces this feature

## Improvements

- Added new extensions in https://github.com/jeremytammik/RevitLookup/pull/255, https://github.com/jeremytammik/RevitLookup/pull/257:

| Type      | Extension                          | Description                                                                                                                                    |
|:----------|------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------|
| Part      | IsMergedPart                       | Is the Part the result of a merge.                                                                                                             |
| Part      | IsPartDerivedFromLink              | Is the Part derived from link geometry                                                                                                         |
| Part      | GetChainLengthToOriginal           | Calculates the length of the longest chain of divisions/ merges to reach to an original non-Part element that is the source of the tested part |
| Part      | GetMergedParts                     | Retrieves the element ids of the source elements of a merged part                                                                              |
| Part      | ArePartsValidForDivide             | Identifies if provided members are valid for dividing parts                                                                                    |
| Part      | FindMergeableClusters              | Segregates a set of elements into subsets which are valid for merge                                                                            |
| Part      | ArePartsValidForMerge              | Identifies whether Part elements may be merged                                                                                                 |
| Part      | GetAssociatedPartMaker             | Gets associated PartMaker for an element                                                                                                       |
| Part      | GetSplittingCurves                 | Identifies the curves that were used to create the part                                                                                        |
| Part      | GetSplittingElements               | Identifies the elements ( reference planes, levels, grids ) that were used to create the part                                                  |
| Part      | HasAssociatedParts                 | Checks if an element has associated parts                                                                                                      |
| PartMaker | GetPartMakerMethodToDivideVolumeFW | Obtains the object allowing access to the divided volume properties of the PartMaker                                                           |
| Element   | GetCheckoutStatus                  | Gets the ownership status of an element                                                                                                        |
| Element   | GetWorksharingTooltipInfo          | Gets worksharing information about an element to display in an in-canvas tooltip                                                               |
| Element   | GetModelUpdatesStatus              | Gets the status of a single element in the central model                                                                                       |
| Element   | AreElementsValidForCreateParts     | Identifies if the given elements can be used to create parts                                                                                   |

## Solved issues

- Dependencies conflict https://github.com/jeremytammik/RevitLookup/issues/210, https://github.com/jeremytammik/RevitLookup/issues/252
- Request for adding WorksharingTooltipInfo Properties https://github.com/jeremytammik/RevitLookup/issues/254
- AssemblyLoadContext discussion https://github.com/jeremytammik/RevitLookup/issues/246

Full changelog: https://github.com/jeremytammik/RevitLookup/compare/2025.0.7...2025.0.8
RevitLookup versioning: https://github.com/jeremytammik/RevitLookup/wiki/Versions

