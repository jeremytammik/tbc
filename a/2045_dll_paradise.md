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

- ricaun video on end of dll hell
  https://www.linkedin.com/posts/ricaun_revitlookup-end-of-dll-hell-revit-api-activity-7217554542312239105-BvN3?utm_source=share&utm_medium=member_desktop

twitter:

Clarification on the arc Length versus ApproximateLength properties provided by curve elements and the new RevitLookup add-in dependencies isolation solves @AutodeskRevit #RevitAPI add-in DLL hell #BIM @DynamoBIM https://autode.sk/end_dll_hell

New RevitLookup solves Revit add-in DLL hell, and a clarification on the arc length properties provided by curve elements
&ndash; Curve Length versus ApproximateLength
&ndash; RevitLookup dependency isolation ends DLL hell
&ndash; Add-in dependencies isolation...

linkedin:

Clarification on the arc Length versus ApproximateLength properties provided by curve elements and the new RevitLookup add-in dependencies isolation solves #RevitAPI add-in DLL hell

https://autode.sk/end_dll_hell

- Curve Length versus ApproximateLength
- RevitLookup dependency isolation ends DLL hell
- Add-in dependencies isolation...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Length Query and RevitLookup Heralds DLL Paradise

Exciting new RevitLookup release solves Revit add-in DLL hell, and a clarification on the arc length properties provided by curve elements:

- [Curve Length versus ApproximateLength](#2)
- [RevitLookup dependency isolation ends DLL hell](#3)
- [Add-in dependencies isolation](#4)

####<a name="2"></a> Curve Length versus ApproximateLength

Last week, the Revit development team helped to provide a useful clarification
of [the difference between `Length` and `ApproximateLength` in `Curve` class](https://forums.autodesk.com/t5/revit-api-forum/the-difference-between-length-and-approximatelength-in-curve/m-p/12841543/):

**Question:**
The Curve class provides two properties to get the length of a curve, `Length` and `ApproximateLength`.
What is the difference?

**Answer:**
For many types of curves, the results will probably be identical.
Differences probably only occur for complex curves.
For example, there is no closed-form expression for the arc length of an ellipse.
One efficient and fruitful way to address this kind of question is to implement a little test suite with benchmarking and try it out for yourself.

The development team clarify:

Seems pretty well documented in the remarks for the `ApproximateLength` property to me.
ApproximateLength is completely accurate for uniform curves (lines and arcs) but could be off by as much as 2x for non-uniform curves, and so it may be worth checking the curve’s class and deciding which method to call from there, or just using the `Length` property in all cases, if accuracy is a concern.
I recommend the latter, as the time savings hasn’t shown to be significant in my work.

`ApproximateLength` uses a rough approximation that depends on the curve type.
There's no guarantee that the approximation method will be unchanged in future releases.

`Length` performs a line integral to compute the curve length, which can be considered exact for all practical purposes.
Lines and arcs have closed-form expressions for their lengths that are used instead.
I agree that a user is unlikely to see a noticeable performance difference between the two methods.
The performance differences are mostly relevant for internal usage, e.g., in graphics functionality.

<center>
<img src="img/arc_length.png" alt="Arc length" title="Arc length" width="300"/>
</center>

####<a name="3"></a> RevitLookup Dependency Isolation Ends DLL Hell

The new [RevitLookup release 2025.0.8](https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.8) runs
in an isolated container for addin dependencies.

This new capability prevents conflicts and compatibility issues arising from different library versions between plugins, ensuring a more stable and reliable environment for plugin execution.

This enhancement uses the `Nice3point.Revit.Toolkit` to manage the isolation process, effectively eliminating DLL conflicts.
By integrating this package, RevitLookup ensures a consistent and predictable user experience.

The detailed description how it works is provided in the release notes
for [RevitToolkit release 2025.0.1](https://github.com/Nice3point/RevitToolkit/releases/tag/2025.0.1),
also reproduced [below](#4).

<!--
Please note that this is unrelated
to [the recent RevitLookup hotfix 2025.0.1](https://thebuildingcoder.typepad.com/blog/2024/04/revitlookup-hotfix-and-the-revit-2025-sdk.html#2).
-->

The dependency isolation is available starting with Revit 2025.
Note that the isolation mechanism is implemented by an additional library that must be loaded into Revit at first startup for it to work.
Therefore, if your other plugins use `Nice3point.Revit.Toolkit`, it must be updated to version `2025.0.1`, which introduces this feature.

RevitLookup 2025.0.8 addresses the following issues:

- Dependency conflicts [latest release won't run #210](https://github.com/jeremytammik/RevitLookup/issues/210) and
  [I get an error #252](https://github.com/jeremytammik/RevitLookup/issues/252)
- [Request for adding `WorksharingTooltipInfo` properties #254](https://github.com/jeremytammik/RevitLookup/issues/254)
- A discussion of the `AssemblyLoadContext` used to implement the dependency isolation,
  [Build Automation Version is breaking Revit 2025 #246](https://github.com/jeremytammik/RevitLookup/issues/246)

As further improvements, the following type extensions are added
for the [`Part` class, associated classes #255](https://github.com/jeremytammik/RevitLookup/pull/255)
and [`WorksharingUtils` #257](https://github.com/jeremytammik/RevitLookup/pull/257):

Part:

- IsMergedPart: Is the Part the result of a merge.
- IsPartDerivedFromLink: Is the Part derived from link geometry
- GetChainLengthToOriginal: Calculates the length of the longest chain of divisions/ merges to reach to an original non-Part element that is the source of the tested part
- GetMergedParts: Retrieves the element ids of the source elements of a merged part
- ArePartsValidForDivide: Identifies if provided members are valid for dividing parts
- FindMergeableClusters: Segregates a set of elements into subsets which are valid for merge
- ArePartsValidForMerge: Identifies whether Part elements may be merged
- GetAssociatedPartMaker: Gets associated PartMaker for an element
- GetSplittingCurves: Identifies the curves that were used to create the part
- GetSplittingElements: Identifies the elements ( reference planes, levels, grids ) that were used to create the part
- HasAssociatedParts: Checks if an element has associated parts

PartMaker:
- GetPartMakerMethodToDivideVolumeFW: Obtains the object allowing access to the divided volume properties of the PartMaker

Element:
- GetCheckoutStatus: Gets the ownership status of an element
- GetWorksharingTooltipInfo: Gets worksharing information about an element to display in an in-canvas tooltip
- GetModelUpdatesStatus: Gets the status of a single element in the central model
- AreElementsValidForCreateParts: Identifies if the given elements can be used to create parts

####<a name="4"></a> Add-In Dependencies Isolation

The RevitLookup isolated plugin dependency container is built using .NET `AssemblyLoadContext`.

This feature enables plugins to run in a separate, isolated context, ensuring independent execution and preventing conflicts from incompatible library versions.

This enhancement is available for Revit 2025 and higher, addressing the limitations of Revit's traditional plugin loading mechanism, which loads plugins by path without native support for isolation.

How it works:

The core functionality centres on `AssemblyLoadContext`, which creates an isolated container for each plugin.

When a plugin is loaded, it is assigned a unique `AssemblyLoadContext` instance, encapsulating the plugin and its dependencies to prevent interference with other plugins or the main application.

To use this isolation feature, developers must inherit their classes from:

- ExternalCommand
- ExternalApplication
- ExternalDbApplication
- ExternalCommandAvailability

These classes contain the built-in isolation mechanism under the hood.
Plugins using interfaces such as `IExternalCommand` will not benefit from this isolation and will run in the default context.

Limitations:

- The isolated plugin context feature is available starting with Revit 2025.
- For older Revit versions, this library uses a ResolveHelper to help load dependencies from the plugin's folder, but does not protect against conflicts arising from incompatible packages.
- Additionally, plugins that do not inherit from the specified classes will not be isolated and may experience compatibility issues if they rely on the default context.

For further details, please refer to the discussion between ricaun and Nice3point
on [build automation version breaking Revit 2025 #246](https://github.com/jeremytammik/RevitLookup/issues/246)

They recommend that Autodesk and Revit adopt similar functionality and include it in the basic Revit API add-in handling architecture, so that all add-in dependencies are automatically isolated and DLL hell conflicts never occur.

ricaun recorded a nine-minute video
on [RevitLookup - End of DLL hell - Revit API](https://youtu.be/cpy4J_6-8WY) explaining
and demonstrating exactly how RevitLookup for Revit 2025 can herald the end of DLL hell:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/cpy4J_6-8WY?si=05tXP34MEFOGqFGB" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</center>

Many thanks to both of you for your thorough implementation, testing, discussion and documentation!
