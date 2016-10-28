<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

Revit 2017.1 SDK posting -- email Mikako Harada

Pawel Madej RE: New version of Revit SDK 2017.1 with new REX SDK sample - FreezeDrawing

<code></code>

 @AutodeskForge #revitapi @AutodeskRevit #aec #bim

&ndash; 
...

-->

### Revit 2017.1 SDK and REX FreezeDrawing

I returned late last night from the 
one-week [Munich Forge accelerator](http://autodeskcloudaccelerator.com) workshop.

I travelled back to Switzerland by train.
For ecological reasons, I prefer to avoid flying whenever I possibly can.

Today is the deadline for submitting my Autodesk University handout, so I am pretty busy with my last-minute preparation of that.

- [Revit 2017.1 SDK](#2)
- [REX SDK FreezeDrawing sample](#3)
- [Forge accelerator topics](#4)
- [Neo4j database](#5)

#### <a name="2"></a>Revit 2017.1 SDK

The new version of the Revit SDK for Revit 2017.1 is now available on the Autodesk webpage:

http://usa.autodesk.com/adsk/servlet/index?id=2484975&siteID=123112

#### <a name="3"></a>REX SDK FreezeDrawing Sample

One important structural part of the Revit SDK is the [REX SDK](http://thebuildingcoder.typepad.com/blog/2015/12/rex-app-development-and-migration.html).

The 2017.1 version of the REX SDK contains new sample project `DRevitFreezeDrawing` that was created from a discontinued REX module.
  
Many thanks to Paweł Madej, Senior Software Engineer in Krakow, Poland, for pointing it out and supplying the following detailed description:
 
<u>Subject</u>

Separate a drawing or view from model so that the state of the drawing or view stays unchanged (frozen).

<u>Summary</u>

This sample contains the discontinued Revit Extensions module Freeze Drawing.

It enables you to separate a drawing or view from an object model so that its state remains unchanged (frozen) or export it to a DWG file.
 
<u>REXSDK Values</u>

- Real module with complete UI and help.
- Fully functional REX module.
- Presents sample source code for view exporting and importing.
 
<u>Description</u>

The extension is based on the Revit DWG Import and DWG Export functionality.
All frozen drawings or views are placed in newly created views.
Selected views are imported to DWG files with user-defined parameters.
Freezing of drawings does not include 3D views or sheets.
The extension also offers options for saving frozen drawings and views on the disk.
  
<u>Instructions</u>

- Copy the module to the `...Modules/Category` directory.
- Open Revit.
- Define model. 
- Run the extension.
- Choose the view to be exported or frozen.
- Press OK to see result and new Drafting View (Detail).
- From now on, the frozen view will remain  even if the model is changed.
 
<u>Screen Shots</u>

After compilation of the sample modules, the new command is visible at the bottom of the samples list:
 
<center>
<img src="img/freeze_drawing_1.png" alt="Freeze Drawing command" width="272">
</center>

The main dialogue prompts whether to freeze the current view or selected ones:

<center>
<img src="img/freeze_drawing_2.png" alt="Freeze Drawing main dialogue" width="488">
</center>

The view selection form looks like this:
 
<center>
<img src="img/freeze_drawing_3.png" alt="Freeze Drawing view selection form" width="484">
</center>

Thanks again to Paweł for pointing it out!


#### <a name="4"></a>Forge Accelerator Topics

#### <a name="5"></a>House Configurator Approaches

Two completely different approaches toward the implementation of a web or Forge based house configurator, a la IKEA for a house:
    
estonia - rhino -> threejs --> revit

belgium - aggregation sample https://forge.autodesk.io

#### <a name="6"></a>Neo4j

[Neo4j](https://neo4j.com/)

[document-based or graph database, MongoDB vs Neo4j](http://stackoverflow.com/questions/14793335/should-i-go-for-document-based-or-graph-database-mongodb-vs-neo4j)

Revit centric TBI Voorbij pre-cast factory design to fabrication process: part 4 of this video, Jos Mulkens and Jeroen Pat from TBI 

http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2015/class-detail/11913#chapter=0

Leo van Ruijven thought leader - http://www.iso.org/iso/catalogue_detail.htm?csnumber=57859

<center>
</center>
