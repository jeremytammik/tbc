<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- TwentyTwo
  TwentyTwo is creating forever free Autodesk® Add-Ins which help you done more with less time and effort ... Delivers as efficient, simplest as possible applications to handle tedious tasks and complex operations.
  [NAVISWORKS ADD-INS AND API TUTORIALS](https://twentytwo.space/navisworks-add-ins-development)
  [REVIT add-in DEVELOPMENT](https://twentytwo.space/revit-add-in-development/)
  [TwentyTwo blog](https://twentytwo.space/)

- Forge and IFC by Eason Kang
  https://twitter.com/AutodeskForge/status/1567800161983676416?s=20&t=qIK9wsdnKoIEA7_oVygclw
  
- [Q] Can Revit API add custom properties in an IFC file opened in Revit? Can Revit export this IFC with those new properties?
  [A] Do you know if DGN supports changing or adding properties? Is it viable using Revit API?
  Eason Kang
  For adding custom props to IFC, there is no direct way with Revit. We need to the below to achieve that:
  Open the IFC model with Revit’s OpenIFC (API: Application.OpenIFCDocument) to convert IFC to RVT
  Add customer props by share parameters and give values (sample code from Jeremy)
  Define custom Property Sets for IFC (here is a tutorial video from 3rd-party or check this AKN page)
  Specify the custom Property Sets in IFC export setup (See userDefinedPSets in Revit IFC repo to know how to construct IFCExportOptions for API)
  Export the modified RVT to IFC (API: Document.Export)
  custom Property Sets in IFC export setup.png
  Demo-Added custom prop `FM ID` in IFC.png
  Imported IFC in Navisworks.png
  Content of my user define property set.png
  In Forge Viewer
  Screen Shot 2022-07-14 at 10.49.50 AM.png 
  ifc_custom_property_1_export_setup.png
  ifc_custom_property_2_added_fm_id.png
  ifc_custom_property_3_nw_import.png
  ifc_custom_property_4_content.png
  ifc_custom_property_5_forge.png
  Hey @Eason Kang thank you for the nice explanation on adding
 
- https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html#comment-5925189938
  xikes
  For those who are still stuck with this problem even when using the correct overload:
    public FamilyInstance NewFamilyInstance(
      XYZ location,
      FamilySymbol symbol,
      Element host,
      StructuralType structuralType
    )
  It is essential to pass in the function parameter host as a Level and not as an Element.
  Add a quick cast like (Level)MyHostElement and it should do the trick and the Level parameter is created properly and is not read-only.
  Keep in mind, this will screw up the offset values, but you can adjust those afterwards.

- set level id of existing element
  $ tbcsh_search.py level
  https://forums.autodesk.com/t5/revit-api-forum/levelid-is-null/m-p/11392692
  https://forums.autodesk.com/t5/revit-api-forum/change-level-on-line-based-family/m-p/10307454
  [Q] I'm placing a new face-based family instance into my Revit model with the help of the NewFamilyInstance method taking (Face, XYZ, XYZ, FamilySymbol).
  This works fine, except the instance does not have its level set to that of the host; it's set to -1 in the API and just left blank in the UI.
  I tried setting the level like such usng the placed instance `LevelId` property and also tried setting its `BuiltInParameter` `FAMILY_LEVEL_PARAM`.
  Both throw an error saying the parameter is read-only.
  [A] On some elements, the element level can only be set during the creation of the element. For that, I would assume that you need to use a different [overload of the `NewFamilyInstance` method](https://www.revitapidocs.com/2017/0c0d640b-7810-55e4-3c5e-cd295dede87b.htm). Please refer to this explanation by The Building Coder and a few recent discussions of related topics in the Revit API discussion forum:
  - [Change level of existing element](https://thebuildingcoder.typepad.com/blog/2020/06/creating-material-texture-and-retaining-pixels.html#4)
  - [LevelId is null](https://forums.autodesk.com/t5/revit-api-forum/levelid-is-null/m-p/11392692)
  - [Change level on line based family](https://forums.autodesk.com/t5/revit-api-forum/change-level-on-line-based-family/m-p/10307454)

 twitter:

Tips on IFC custom properties in APS, Forge and Revit and how to control the level of BIM elements, existing and new, in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/ifcproperties

Tips on handling IFC and recent discussions on controlling the level of BIM elements
&ndash; TwentyTwo add-ins and tutorials
&ndash; IFC tips for APS and Forge
&ndash; IFC custom properties in Revit
&ndash; Set level id of existing element
&ndash; Set level in NewFamilyInstance...

linkedin:

Tips on IFC custom properties in APS, Forge and Revit and how to control the level of BIM elements, existing and new, in the #RevitAPI

https://autode.sk/ifcproperties

- TwentyTwo add-ins and tutorials
- IFC tips for APS and Forge
- IFC custom properties in Revit
- Set level id of existing element
- Set level in NewFamilyInstance...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Element Level and IFC Properties 

Today, we mention another publisher of Revit API tutorials and add-ins, some tips on handling IFC, and recent discussions on controlling the level of BIM elements:

- [TwentyTwo add-ins and tutorials](#2)
- [IFC tips for APS and Forge](#3)
- [IFC custom properties in Revit](#4)
- [Set level id of existing element](#5)
- [Set level in NewFamilyInstance](#6)

####<a name="2"></a> TwentyTwo Add-Ins and Tutorials

According to its own mission statement, 
[TwentyTwo](https://twentytwo.space) is creating forever free Autodesk add-ins that help you do more with less time and effort,
delivering efficient applications, as simple as possible, to handle tedious tasks and complex operations.
Besides the [TwentyTwo blog](https://twentytwo.space),
they also share add-ins and API tutorials for both 
[Navisworks](https://twentytwo.space/navisworks-add-ins-development) and
[Revit](https://twentytwo.space/revit-add-in-development).

Many thanks to [Min Naung](https://twentytwo.space/author/mgjean) for putting together and sharing this material!

####<a name="3"></a> IFC Tips for APS and Forge

Wondering about your options when translating IFC model formats using the Autodesk Platform Services (APS), previously known as Forge?
Developer advocate Eason [@yiskang](https://twitter.com/yiskang) Kang put together
a comprehensive list of [FAQ and Tips for IFC translation of Model Derivative API](https://forge.autodesk.com/blog/faq-and-tips-ifc-translation-model-derivative-api) that
might help, including and not limited to:

- Overview of different available IFC conversion methods
- Georeferencing in IFC
- Troubleshooting locally
- Testing with Navisworks
- Testing with Revit
- 3rd-party IFC viewers
- Show All Presentations

####<a name="4"></a> IFC Custom Properties in Revit

Eason also recently addressed another important IFC related question:

**Question:** Can the Revit API be used to add custom properties in an IFC file opened in Revit?
Can Revit export this IFC with those new properties?

**Answer:** There is no direct way in Revit to add custom properties to IFC.
However, it can be achieved indirectly through the following steps:

- Open the IFC model with Revit’s OpenIFC
(API: [Application.OpenIFCDocument](https://www.revitapidocs.com/2023/bb14933b-a758-2b34-b160-686a28cc48cb.htm)) to
convert IFC to RVT
- Add customer properties by adding shared parameters and specifying values for them
([sample code](https://github.com/jeremytammik/FireRatingCloud/blob/master/FireRatingCloud/Cmd_1_CreateAndBindSharedParameter.cs) from
The Building Coder)
- Define custom Property Sets for IFC (here is a [tutorial video from 3rd-party](https://youtu.be/SswHKtcM3mI) or
check this [AKN page](https://knowledge.autodesk.com/search-result/caas/simplecontent/content/export-custom-bim-standards-and-property-sets-to-ifc.html))
- Specify the custom Property Sets in IFC export setup
(See [userDefinedPSets in Revit IFC repo](https://github.com/Autodesk/revit-ifc/blob/df1485b9accd598c2912a055af205ee1b03648c7/Source/IFCExporterUIOverride/IFCExportConfiguration.cs#L425) to
know how to construct IFCExportOptions for API)
- Export the modified RVT to IFC
(API: [Document.Export](https://www.revitapidocs.com/2023/7efa4eb3-8d94-b8e7-f608-3dbae751331d.htm))

<center>
<img src="img/ifc_custom_property_1_export_setup.png" alt="Custom property sets in IFC export setup" title="Custom property sets in IFC export setup" width="600" height=""/> <!-- 872 x 556 -->
<p style="font-size: 80%; font-style:italic">Custom property sets in IFC export setup</p>

<img src="img/ifc_custom_property_2_added_fm_id.png" alt="Demo-added custom prop `FM ID` in IFC" title="Demo-added custom prop `FM ID` in IFC" width="600" height=""/> <!-- 2032 x 1167 -->
<p style="font-size: 80%; font-style:italic">Demo-added custom prop `FM ID` in IFC</p>

<img src="img/ifc_custom_property_3_nw_import.png" alt="Imported IFC in Navisworks" title="Imported IFC in Navisworks" width="600" height=""/> <!-- 1920 x 1055 -->
<p style="font-size: 80%; font-style:italic">Imported IFC in Navisworks</p>

<img src="img/ifc_custom_property_4_content.png" alt="Content of user defined property set" title="Content of user defined property set" width="600" height=""/> <!-- 1429 x 725 -->
<p style="font-size: 80%; font-style:italic">Content of user defined property set</p>

<img src="img/ifc_custom_property_5_forge.png" alt="Forge Viewer" title="Forge Viewer" width="600" height=""/> <!-- 1583 x 982 -->
<p style="font-size: 80%; font-style:italic">Forge Viewer</p>
</center>

Many thanks to Eason for the useful explanation!

####<a name="5"></a> Set Level Id of Existing Element

Returning to pure desktop Revit API topics, several discussions recently in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) circled
around setting the level of an existing element, e.g.:

<!--

[`LevelId` is null](https://forums.autodesk.com/t5/revit-api-forum/levelid-is-null/m-p/11392692) and
how to [change level on line based family](https://forums.autodesk.com/t5/revit-api-forum/change-level-on-line-based-family/m-p/10307454).

$ tbcsh_search.py level

0079 0107 0301 0333 0340 0346 0383 0464 0525 0716 0830 0840 0860 0903 0904 0938 1093 1107 1158 1246 1311 1406 1429 1529 1537 1551 1728 1732 1737 1828 1850 1917

- [Walls and Doors on Two Levels](http://thebuildingcoder.typepad.com/blog/2009/01/walls-and-doors-on-two-levels.html)
- [Create Room on Level in Phase](http://thebuildingcoder.typepad.com/blog/2009/03/create-room-on-level-in-phase.html)
- [Detail Curve on Level](http://thebuildingcoder.typepad.com/blog/2010/02/detail-curve-on-level.html)
- [Collector Benchmark](http://thebuildingcoder.typepad.com/blog/2010/04/collector-benchmark.html)
- [Element Level Events](http://thebuildingcoder.typepad.com/blog/2010/04/element-level-events.html)
- [Retrieve Stairs on Level](http://thebuildingcoder.typepad.com/blog/2010/04/retrieve-stairs-on-level.html)
- [Parameter Filter](http://thebuildingcoder.typepad.com/blog/2010/06/parameter-filter.html)
- [Level Filter Benchmark](http://thebuildingcoder.typepad.com/blog/2010/10/level-filter-benchmark.html)
- [Family Instance Missing Level Property](http://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html)
- [Level Generator ADN Plugin of the Month](http://thebuildingcoder.typepad.com/blog/2012/02/level-generator-adn-plugin-of-the-month.html)
- [Mobile Device Room Location](http://thebuildingcoder.typepad.com/blog/2012/09/mobile-device-room-location.html)
- [UIView, Windows Coordinates, ReferenceIntersector and My Own Tooltip](http://thebuildingcoder.typepad.com/blog/2012/10/uiview-windows-coordinates-referenceintersector-and-my-own-tooltip.html)
- [Building Performance Analysis and Face Tessellation](http://thebuildingcoder.typepad.com/blog/2012/11/building-performance-analysis-and-face-tessellation.html)
- [What's New in the Revit 2012 API](http://thebuildingcoder.typepad.com/blog/2013/02/whats-new-in-the-revit-2012-api.html)
- [What's New in the Revit 2013 API](http://thebuildingcoder.typepad.com/blog/2013/03/whats-new-in-the-revit-2013-api.html)
- [What's New in the Revit 2014 API](http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html)
- [Final Rolling Offset Using Pipe.Create](http://thebuildingcoder.typepad.com/blog/2014/01/final-rolling-offset-using-pipecreate.html)
- [Different Revit API Aspects and Features](http://thebuildingcoder.typepad.com/blog/2014/02/different-revit-api-aspects-and-features.html)
- [Views Displaying Given Element, SVG and NoSQL](http://thebuildingcoder.typepad.com/blog/2014/05/views-displaying-given-element-svg-and-nosql.html)
- [WebGL Goes Mobile and Sorted Level Retrieval](http://thebuildingcoder.typepad.com/blog/2014/11/webgl-goes-mobile-and-sorted-level-retrieval.html)
- [What's New in the Revit 2016 API](http://thebuildingcoder.typepad.com/blog/2015/04/whats-new-in-the-revit-2016-api.html)
- [IFC Import Levels and MEP Element Shapes](http://thebuildingcoder.typepad.com/blog/2016/02/ifc-import-levels-and-mep-element-shapes.html)
- [Reference Stable Representation Magic Voodoo](http://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html)
- [RevitLookup with Reflection Cleanup](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-with-reflection-cleanup.html)
- [Events, UV Coordinates and Rooms on Level](http://thebuildingcoder.typepad.com/blog/2017/03/events-uv-coordinates-and-rooms-on-level.html)
- [What's New in the Revit 2018 API](http://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html)
- [Assigning a Level to an Element Missing It](https://thebuildingcoder.typepad.com/blog/2019/03/assigning-a-level-to-an-element-missing-it.html)
- [Forge Picture, Debugging, Snooping Appearances](https://thebuildingcoder.typepad.com/blog/2019/03/-architecture-edit-and-continue-snooping-appearance-assets.html)
- [Set Floor Level and Use IPC for Disentanglement](https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html)
- [Panel Schedule Slots and Changing Room Level](https://thebuildingcoder.typepad.com/blog/2020/03/panel-schedule-slots-and-change-room-level.html)
- [Creating Material Texture and Retaining Pixels](https://thebuildingcoder.typepad.com/blog/2020/06/creating-material-texture-and-retaining-pixels.html)
- [View Sheet from View and Select All on Level](https://thebuildingcoder.typepad.com/blog/2021/09/view-sheet-from-view-and-select-all-on-level.html)

-->

**Question:** I'm placing a new face-based family instance into my Revit model with the help of the NewFamilyInstance method taking (Face, XYZ, XYZ, FamilySymbol).
This works fine, except the instance does not have its level set to that of the host; it's set to -1 in the API and just left blank in the UI.
I tried setting the level like such using the placed instance `LevelId` property and also tried setting its `BuiltInParameter` `FAMILY_LEVEL_PARAM`.
Both throw an error saying the parameter is read-only.

**Answer:** On some elements, the element level can only be set during the creation of the element.
For that, I would assume that you need to use a different [overload of the `NewFamilyInstance` method](https://www.revitapidocs.com/2017/0c0d640b-7810-55e4-3c5e-cd295dede87b.htm).
Please refer to this explanation by The Building Coder and a few recent discussions of related topics in the Revit API discussion forum:

- [Change level of existing element](https://thebuildingcoder.typepad.com/blog/2020/06/creating-material-texture-and-retaining-pixels.html#4)
- [LevelId is null](https://forums.autodesk.com/t5/revit-api-forum/levelid-is-null/m-p/11392692)
- [Change level on line based family](https://forums.autodesk.com/t5/revit-api-forum/change-level-on-line-based-family/m-p/10307454)

Another potentially helpful suggestion came up on the blog:

####<a name="6"></a> Set Level in NewFamilyInstance

Xikes shared a valuable observation in
their [comment](https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html#comment-5925189938)
on [family instance missing `Level` property](https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html):

For those who are still stuck with this problem even when using the correct overload:

<pre class="code">
  public FamilyInstance NewFamilyInstance(
    XYZ location,
    FamilySymbol symbol,
    Element host, 
    StructuralType structuralType )
</pre>

It is essential to pass in the function parameter `host` as a `Level` and not as an `Element`.
Add a quick cast like `(Level) myHostElement`.
It should do the trick.
The `Level` parameter is created properly and is not read-only.
Keep in mind that this will screw up the offset values, but you can adjust those afterwards.

It would be very helpful if other developers could confirm this observation.
Thank you.
