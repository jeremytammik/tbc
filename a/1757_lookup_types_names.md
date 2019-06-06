<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- design.automation-nodejs-sketchIt
  https://github.com/Autodesk-Forge/design.automation-nodejs-sketchIt
  SketchIt is web application that creates walls and floors in a SVG Canvas to later create & visualize the result RVT file.
  [demo](https://sketchitapp.herokuapp.com)
  /a/src/web/forge/da4r/design.automation-nodejs-sketchIt/README.md

twitter:

SketchIt generates walls and BIM from node.js SVG canvas, RevitLookup family types and parameter definition names in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/lookup_types_names

@afrojme shares a neat Forge Design Automation for Revit sample app enabling you to sketch walls on a canvas and generate a live Revit BIM RVT from that -- https://thebuildingcoder.typepad.com/blog/2019/06/lookup-family-types-and-parameter-definition-names.html#4

Today, we present yet another RevitLookup enhancement, a note on an undocumented built-in parameter change and a neat Forge Design Automation for Revit sample app
&ndash; RevitLookup family types and parameter definition names
&ndash; Bitmap aspect ratio built-in parameter renamed
&ndash; DA4R SketchIt demo generates walls...

linkedin:

SketchIt generates walls and BIM from node.js SVG canvas, RevitLookup family types and parameter definition names in the #RevitAPI

http://bit.ly/lookup_types_names

Today, we present yet another RevitLookup enhancement, a note on an undocumented built-in parameter change and a neat Forge Design Automation for Revit sample app:

- RevitLookup family types and parameter definition names
- Bitmap aspect ratio built-in parameter renamed
- DA4R SketchIt demo generates walls

@afrojme shares a neat Forge Design Automation for Revit sample app enabling you to sketch walls on a canvas and generate a live Revit BIM RVT from that...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

-->

### SketchIt, Lookup Family Types, Definition Names

Today, we present yet another RevitLookup enhancement, a note on an undocumented built-in parameter change and a neat Forge Design Automation for Revit sample app:

- [RevitLookup family types and parameter definition names](#2)
- [Bitmap aspect ratio built-in parameter renamed](#3)
- [DA4R SketchIt demo generates walls](#4)


####<a name="2"></a> RevitLookup Family Types and Parameter Definition Names

Alexander [@aignatovich](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478) [@CADBIMDeveloper](https://github.com/CADBIMDeveloper) Ignatovich, aka Александр Игнатович,
submitted yet another useful [RevitLookup](https://github.com/jeremytammik/RevitLookup) enhancement
in [pull request #53 &ndash; available values for parameters (`ParameterType.FamilyType`) and `FamilyParameter` titles](https://github.com/jeremytammik/RevitLookup/pull/53).

In his own words:

> I added 2 improvements to the RevitLookup tool.

> The first is about available parameters values for parameters with `ParameterType` == `ParameterType.FamilyType`:

<center>
<img src="img/revitlookup_pull_request_53_1.png" alt="RevitLookup lists family types" width="415">
</center>

> We can retrieve these values using the `Family.GetFamilyTypeParameterValues` method.
The elements are either of class `ElementType` or `NestedFamilyTypeReference`:

<center>
<img src="img/revitlookup_pull_request_53_2.png" alt="RevitLookup lists family types" width="667">
</center>

> The second is very simple: Now the tool shows `FamilyParameter` definition names in the left pane:

<center>
<img src="img/revitlookup_pull_request_53_3.png" alt="RevitLookup lists family types" width="596">
</center>

Yet again many thanks to Alexander for his numerous invaluable contributions!

This enhancement is captured
in [RevitLookup release 2020.0.0.2](https://github.com/jeremytammik/RevitLookup/releases/tag/2020.0.0.2).


####<a name="3"></a> Bitmap Aspect Ratio Built-in Parameter Renamed

Rudolf [@Revitalizer](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1103138) Honke
and I completed a quick 20-km hilly run together yesterday.

This morning he pointed out an irritating change between the Revit 2019 and Revit 2020 APIs, an undocumented modification of the name of a built-in parameters defining the locked aspect ratio of a bitmap image, or *Seitenverhältnis sperren von eingefügten Rasterbildern* in German.

The underlying integer value remains unchanged, however, `-1007752`:

- Revit 2019: `BuiltInParameter.RASTER_MAINTAIN_ASPECT_RATIO`
- Revit 2020: `BuiltInParameter.RASTER_LOCK_PROPORTIONS`

Useful to know, just in case you happen to run into this yourself.


####<a name="4"></a> DA4R SketchIt Demo Generates Walls

I just noticed a neat
[Forge](https://forge.autodesk.com)
[Design Automation for Revit](https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview)
or [DA4R](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.55) sample
application created by my colleague
Jaime [@afrojme](https://twitter.com/AfroJme) Rosales,
[Forge Partner Development](http://forge.autodesk.com):

[SketchIt](https://github.com/Autodesk-Forge/design.automation-nodejs-sketchIt) is
a web application that enables the user to sketch out walls and floors in an SVG Canvas to later create and visualise them in an automatically generated RVT BIM model:

<center>
<img src="img/jr_da4r_sketchit_demo.gif" alt="SketchIt demo" width="600">
</center>

You can try it out live yourself in the [demo web page](https://sketchitapp.herokuapp.com).

This is a [node.js](https://nodejs.org) app demonstrating an end to end use case for external developers using Design Automation for Revit.
In addition to using Design Automation for Revit REST APIs, this app also leverages other Autodesk Forge services like Data Management API (OSS), the Viewer API and Model Derivative services.

The sketcher is built using Redux with React and makes extensive use of Flux architecture.

Main Parts

- Create a Revit Plugin to be used within AppBundle of Design Automation for Revit.
- Create your App, upload the AppBundle, define your Activity and test the workitem.
- Create the Web App to call the workitem.
