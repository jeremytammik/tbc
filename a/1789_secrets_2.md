<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- CAD link status
  https://forums.autodesk.com/t5/revit-api-forum/cad-link-status/m-p/9075576
  https://forums.autodesk.com/t5/revit-api-forum/cad-link-status/td-p/9073926
  How to get the Status of the Revit Link? 
  https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-status-of-the-revit-link/td-p/9072787
  
- https://forums.autodesk.com/t5/revit-api-forum/how-change-the-color-a-element/m-p/5651177
  already pointed out in 1615 and tbc topic group 5.24

twitter:

BILT NZ 2019 Secret Series #2, accessing CAD link status and making blocks in the DWG export in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/biltsecrets2

Joshua Lumley shares video #2 in his secret series, access to the CAD link status string and block in DWG export
&ndash; Secrets of Revit API coding part 2
&ndash; Video 2 in the secret series
&ndash; Getting CAD link status
&ndash; Making blocks in Revit DWG export...

linkedin:

BILT NZ 2019 Secret Series #2, accessing CAD link status and making blocks in the DWG export in the #RevitAPI

http://bit.ly/biltsecrets2

Joshua Lumley shares video #2 in his secret series, access to the CAD link status string and block in DWG export

- Secrets of Revit API coding part 2
- Video 2 in the secret series
- Getting CAD link status
- Making blocks in Revit DWG export...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Secret Series 2, CAD Link Status and DWG Blocks

Joshua Lumley shares video #2 in his secret series, access to the CAD link status string and block in DWG export:

- [Secrets of Revit API coding part 2](#2)
- [Video 2 in the secret series](#3)
- [Getting CAD link status](#4)
- [Making blocks in Revit DWG export](#5)


####<a name="2"></a> Secrets of Revit API Coding Part 2

Last year, Joshua Lumley shared the recording he made for his BILT submission
on [five secrets of Revit API C# coding](https://thebuildingcoder.typepad.com/blog/2018/09/five-secrets-of-revit-api-coding.html).

In his [comment](https://thebuildingcoder.typepad.com/blog/2019/08/zero-touch-node-element-wrapper-and-load-from-stream.html#comment-4646680624)
on [Loading a .NET assembly from a memory stream](https://thebuildingcoder.typepad.com/blog/2019/08/zero-touch-node-element-wrapper-and-load-from-stream.html#3),
he points out part two, for this year's event:

> What really gets really tricky is when your add-in references another DLL not provided by Microsoft's .net framework.

> I made a 43-minute video on how to do it with the Xceed Extended.Wpf.Toolkit, [cf. below](#3).

> It also avoids 'double' loading.

####<a name="3"></a> Video 2 in the Secret Series

Joshua Lumley's video #2 in the Secret Series, 
[Revit API C# make installer MSI file and avoid my blunders](https://youtu.be/S0MPxBRL7c0):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/S0MPxBRL7c0" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

> In Support of BILT ANZ 2019 150-minute Lab by Joshua Lumley from Christchurch, New Zealand.

> Turn your Revit macro that was initiated from macro manager into a proper plugin installer (an MSI file) you can distribute to other computers with no fuss.

> Includes use of Nuget packages: Extended WPF Toolkit & Ookii Dialogues.

> In the hope you will learn from my mistakes &ndash; throughout this lab I share a few embarrassing moments, where I spent many hours solving issues that turned out to have very simple resolutions.

Many thanks to Joshua for his useful work and kind sharing!

####<a name="4"></a> Getting CAD Link Status

Two different threads in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) ask
how to determine the loaded versus unloaded status of a CAD link, so let's summarise the answer here:

- [CAD link status](https://forums.autodesk.com/t5/revit-api-forum/cad-link-status/m-p/9075576)
- [How to get the status of the Revit link?](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-status-of-the-revit-link/td-p/9072787)
  
**Question:** I'm trying to get the status of my linked CAD files through Revit API; more specifically, I want to list all the CAD links that were not found.

I know that I'm dealing with `ImportInstance`, I know that I can use `.IsLinked` to determine if it was linked or imported.

I can't find any way to get the status info, and even finding a path to the original file seems impossible.

Has any of you dealt with this before? Is it even possible? 

By using the method `RevitLinkType.IsLoaded`, I can only tell whether or not the link is loaded (True or False).
Is there a way to get the Status string?

<center>
<img src="img/cad_links_not_loaded.png" alt="CAD links not loaded" width="271">
</center>

**Answer:** I believe that the solution can be found in the discussion
on [automatically reloading links after migration](https://thebuildingcoder.typepad.com/blog/2016/08/automatically-reload-links-after-migration.html).

It includes statements like, *It took a bit of time to find the right classes as there are no less than four levels of indirection to get from RevitLinkType to the 'Saved Path' and then use that in the call to LoadFrom.*

Maybe
he [`ExternalFileReference.GetLinkedFileStatus` method](https://www.revitapidocs.com/2020/cd21f80a-f8be-535a-0793-7c113f27c487.htm) provides
what you need.

Try using this code:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;linktypes&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">CADLinkType</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsElementType();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;<span style="color:blue;">in</span>&nbsp;linktypes&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalFileReference</span>&nbsp;efr&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;e.GetExternalFileReference();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;linkStatus&nbsp;=&nbsp;efr
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetLinkedFileStatus().ToString();
&nbsp;&nbsp;}
</pre>

**Response:** Fantastic! Thank you both for constructive replies!

####<a name="5"></a> Making Blocks in Revit DWG Export

A recent discussion on how to ensure that family instances in Revit are exported to DWG blocks, or, alternatively, how to reassemble together block from them after the export:

**Question:** I have a Revit plugin that exports to DWG and an AutoCAD plugin that imports the newly created DWG.
For Revit exports, some Revit family instances are exported as 1-1 Fam Instance->Blocks, while other Revit family instances with similar geo complexity are exported as a collection of individual lines, arcs, squares, etc. &ndash; not a block.
I’m trying to find a way to tell Revit to export family instances as blocks always but unsure if this is possible.
Also unsure if I can add a property to a Revit fam instance inside Revit in a way that when it exports to the DWG as multiple primitives, I could cleverly reassemble the primitives as a block.
Could anyone help me identify if either API workflow is possible?

**Answer:** I assume that if there options, they are not just exposed to the API, but also in the UI.

**Response:** I'm attempting everything as a user first.
The settings for export don't have a block setting (only solids vs. mesh setting, have used both).
I'm not able to add properties that carry over to let me identify objects in AutoCAD.
For example, element id's don't come in as attributes, so I don't have any logic that can sweep up primitives and assemble as a block.
Also, I have fam instances like electrical fixtures that come in as several arcs, lines and annotation layered vs. single block with geo layer, so I'm not able to identify a programmatic way to assemble the anno, primitive, elec fixtures and turn them into a single block as geo layer since there are no mapping id's.
I've used Revit Snoop and Acad MgdDbg to look under the hood for any matching id's between outgoing element and incoming objects, but they don't exist.
I found id's in acad object `XData` but they are not related to element id's either.
Has anyone created an alternative export tool that gives more control to modify objects programmatically after import?
Maybe I just export all geometry myself in Revit with unique id's and metadata in json/xml and construct things on the import side?
Is that dumb?

<!---
Essentially trying to fill requirements in the attached but my description above should cover it.
Looking to programmatically allow customers to export parameters, export geometry consistently, but moreso be able to discover on the autocad side whether any mapped unique id can be used to say "hey you were this in revit, let me change you after import based on additional metadata I stored somewhere".

PowerPoint Presentation Bldgs_RVTtoDWG_trimmed.pptx

/a/doc/revit/tbc/doc/Bldgs_RVTtoDWG_trimmed.pptx
--->

**Answer:** The DWG export algorithm uses the geometry that the instance object is producing for drawing on screen and based on the types of primitives will export it as a block or not.
Most families produce a `GInstance` nodes, which behaves like a block in AutoCAD.
It is a collection of geometric primitives (lines, arc etc.) and a `Transformation` that defines the coordinates.
This type of node is exported as a block.
As you observed, there are families that produce only geometric primitives and the exporter doesn't consider them as a block.
Unfortunately, I don't have any advice to you. It seems that needs to be implemented on Revit side.
The families that are exported as lines and arcs should construct their representation as a `GInstance`.
I'm not sure this is doable.

I’m also thinking about the solutions with ids.
I remember that each AutoCAD entity's xdata contains the `ElementId` from the Revit element.
You can see this using the `xdlist` command in AutoCAD.
For families exported as blocks, you should run this command on the block reference, not on the entities that compose the block &ndash; these don’t have xdata.
Can you give me an example where this rule doesn’t apply? I remember that there were some issues for elements that comes from linked files (they have ids but from a different document).
Why do you group AutoCAD entities same as the Revit entities? What are you trying to achieve?

<!--- 
**Response:**

We don't have a list of families, we're just starting with one to keep a controlled environment.  I've simplified an example with the attached ZIP.  It includes a single RVT to export a single sheet, outputted DWG of that single sheet and two image files for commentary.  You can dismiss any links that cannot be found.  The category I believe is electrical fixtures.  This is 2019 but its probably the same behavior in 2020.  The images will state the problem probably best.  Maybe nested families behave differently?  What could we do to either programmatically change it on AutoCAD or prep the RVT pre-export?

We are creating a "super exporter" where we want to leverage the export to dwg feature via API but also we want to export (separately) a lot of param data and change layers outside the settings feature.  If you look at my ZIP, there is one specific example where a family instance comes over as individual primitives on ANNO layer, whereas I would expect one block on Geo layer. Because its broken up into many primitives I can't retrace it back to the original fam instance nor know how to group them into a new block since the xdata for at least one of the prims doesn't have original fam instance Revit id. Could you look at the attached?  The two images provided paint the problem best.

Angel Velez:squirrel:  17 days ago
Have you looked at IFC?  Is DWG the base point you want for geometry + data?

Tim Burnham  17 days ago
Stantec wants a fully automated DWG deliverable to their customers where the model stems from Revit.  They do a lot of cleanup work that I'm automating.  IFC is not preferred in this case and these guys are pretty neck deep for years into the export feature so the issues are very isolated.

Tim Burnham  17 days ago
The attached zip above is really asking how I can either prep the fam instance on Revit's side so that it exports as a block, or is there something i can do on the autocad side to discover and group all these primitives into a block.  Hoping the former has a workflow I can do.

**Answer:** 
--->

I looked at your sample files and I understood the issue.

It’s about nested families in certain conditions &ndash; the nested Family is 'shared' and the nested family is an annotation family while the main one isn't.

In your example the symbol (shared family) has a mark that is different from the mark of family instance containing it.
It looks like the intent is to schedule the two components differently, even though in Revit they are placed using the same family.
If this is the case, you can add a logic to group all the primitives from the shared family into its own block.
You can look at each entity’s xData where you will find ID of element that produced that entity.
I don’t know of a way to get the ID of the family instance and tie it to all the shared families contained, in case you want a block containing all the entities from the family and shared families.

**Response:** This is great, thank you!

One other easy'ish question: What do the other xdata index values indicate?

<center>
<img src="img/rvt_dwg_export_xdata.png" alt="Revit DWG export xdata" width="600">
</center>

**Answer:** Here are the codes that we use, cf.
the [DWG and DXF export `Xdata` specification](https://thebuildingcoder.typepad.com/blog/2010/08/dwg-and-dxf-export-xdata-specification.html):

<pre class="code">
  // XData Identifiers
  #define XDATA_ELEMENT_ID                     1
  #define XDATA_CATEGORY_ID                    2
  #define XDATA_SUB_CATEGORY_ID                3
  #define XDATA_MATERIAL_ID                    4
  #define XDATA_TYPE_ID                        5
  #define XDATA_IS_MATERIAL_OVERRIDDEN_BY_FACE 6
  // Room boundary and area XDATA
  #define XDATA_ROOM_NAME_ID                 101
  #define XDATA_ROOM_NUMBER_ID               102
  #define XDATA_ROOM_OCCUPANCY_ID            103
  #define XDATA_ROOM_DEPARTMENT_ID           104
  #define XDATA_ROOM_COMMENTS_ID             105
</pre>
