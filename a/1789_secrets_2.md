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

twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Secret Series 2


Joshua Lumley shares video #2 in his secret series,


####<a name="2"></a> Secrets of Revit API Coding Part 2

Last year, Joshua Lumley shared the recording he made for his BILT submission
on [five secrets of Revit API C# coding](https://thebuildingcoder.typepad.com/blog/2018/09/five-secrets-of-revit-api-coding.html).

In his [comment today](https://thebuildingcoder.typepad.com/blog/2019/08/zero-touch-node-element-wrapper-and-load-from-stream.html#comment-4646680624)
on [Loading a .NET assembly from a memory stream](https://thebuildingcoder.typepad.com/blog/2019/08/zero-touch-node-element-wrapper-and-load-from-stream.html#3),
he points out part two, for this year's event:

> What really gets really tricky is when your addin references another DLL not provided by Microsoft's .net framework.

> I made a 43-minute video on how to do it with the Xceed Extended.Wpf.Toolkit, [cf. below](#3).

> It also avoids 'double' loading.


####<a name="3"></a> Video 2 in the Secret Series

Joshua Lumley's video #2 in the Secret Series, 
[Revit API C# make installer MSI file and avoid my blunders](https://youtu.be/S0MPxBRL7c0):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/S0MPxBRL7c0" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

> In Support of BILT ANZ 2019 150 minute Lab by Joshua Lumley from Christchurch, New Zealand.

> Turn your Revit macro, that was initiated from macro manager into a proper plugin installer (an MSI files) you can distribute to other computers with no fuss.

> Includes use of Nuget packages: Extended WPF Toolkit & Ookii Dialogues.

> In the hope you will learn from my mistakes &ndash; throughout this lab I share a few embarrassing moments, where I spent many hours solving issues that turned out to have very simple resolutions.

Many thanks to Joshua for his great work and sharing!


####<a name="3"></a> Getting

- CAD link status
  https://forums.autodesk.com/t5/revit-api-forum/cad-link-status/m-p/9075576
  https://forums.autodesk.com/t5/revit-api-forum/cad-link-status/td-p/9073926
  How to get the Status of the Revit Link? 
  https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-status-of-the-revit-link/td-p/9072787
  


<center>
<img src="img/.png" alt="" width="347">
</center>

####<a name="4"></a> 

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

[How to change the color a element](https://forums.autodesk.com/t5/revit-api-forum/how-change-the-color-a-element/m-p/5651177)

**Question:**

**Answer:** 


####<a name="5"></a> Revit DWG Export

Tim Burnham Sep 18th at 7:06 PM
I have a Revit plugin that exports to DWG.
I have an AutoCAD plugin that imports the newly created DWG.
For Revit exports, some revit family instances are exported as 1-1 Fam Instance->Blocks while other revit family instances with similar geo complexity are exported as a collection of individual lines, arcs, squares, etc (not a block).  I’m trying to find a way to tell Revit to export family instances as blocks always but unsure if this is possible.  Also unsure if I can add a property to a Revit fam instance inside Revit in a way that when it exports to the DWG as multiple primitives, I could cleverly reassemble the primitives as a block.  Could anyone help me identify if either API workflow is possible?


Scott Conover  21 days ago
@dobres Any ideas if there are DWG export options that address this?  (I assume that if there options, they are not just exposed to the API, but also in the UI)

Tim Burnham  20 days ago
I'm attempting everything as a user first.  The settings for export don't have a block setting (only solids vs mesh setting, have used both).  I'm not able to add properties that carry over to let me identify objects in Autocad (example, element id's don't come in as attributes so I don't have any logic that can sweep up primitives and assemble as a block).  Also, I have fam instances like electrical fixtures that come in as several arcs, lines and annotation layered vs single block with geo layer so I'm not able to identify a programmatic way to assemble the anno, primitive, elec fixtures and turn them into a single block as geo layer since there are no mapping id's.   I've used revit Snoop and acad MgdDbg to look under the hood for any matching id's between outgoing element, incoming objects but they don't exist.  I found id's in acad object XData but they are not related to element id's either.    Has anyone created an alternative export tool that gives more control to modify objects programmatically after import?  Maybe I just export all geometry myself in Revit with unique id's and metadata in json/xml and construct things on the import side?  Is that dumb?

Tim Burnham  20 days ago
Hi @shafirb and @velezan, Sasha and Diane recommend I reach out to you about the above.  Essentially trying to fill requirements in the attached but my description above should cover it.  Looking to programmatically allow customers to export parameters, export geometry consistently, but moreso be able to discover on the autocad side whether any mapped unique id can be used to say "hey you were this in revit, let me change you after import based on additional metadata I stored somewhere".
PowerPoint Presentation Bldgs_RVTtoDWG_trimmed.pptx

/a/doc/revit/tbc/doc/Bldgs_RVTtoDWG_trimmed.pptx

Angel Velez:squirrel:  20 days ago
.@dobres is the person that will have real knowledge here.  My export DWG information is obsolete by about 15 years.

Stefan Dobre  20 days ago
The DWG export algorithm use the geometry that the instance object is producing for drawing on screen and based on the types of primitives will export it as a block or not.
Most of the families produce GInstance nodes. They which behaves like a block in AutoCAD. It is a collection of geometric primitives( lines, arc etc) and a Transformation that tells the coordinates. This types of nodes are exported as blocks.
As you observed there are families that produce only geometric primitives and the exporter doesn't consider them as a block.
Unfortunately I don't have any advices to you. It seems that need to be something implemented on Revit side - The Families that are exported as lines and arc should construct their representation as a GInstance. I'm not sure this is doable.
Could you please give us a list of families that doesn't export as blocks?
I'm also adding my PO @pauneso to this thread.

Stefan Dobre  20 days ago
@burnhat,I’m also thinking at the solutions with ids. I remember that each entity Autocad Entity contains in its xdata the ElementId from Revit element. You can see this using xdlist command in Autocad. For families exported as block you should run this command on the block reference (not on the entities that compose the block – these doesn’t have xdata). Can you give me an example where this rule doesn’t apply? I remember that there were some issues for elements that comes from linked files (they have ids but from a different document).
I want to ask you why do you group Autocad entities same as the Revit entities? What are you trying to achieve?

Tim Burnham  20 days ago
Hi @dobres, first thanks for taking the time to respond.  We don't have a list of families, we're just starting with one to keep a controlled environment.  I've simplified an example with the attached ZIP.  It includes a single RVT to export a single sheet, outputted DWG of that single sheet and two image files for commentary.  You can dismiss any links that cannot be found.  The category I believe is electrical fixtures.  This is 2019 but its probably the same behavior in 2020.  The images will state the problem probably best.  Maybe nested families behave differently?  What could we do to either programmatically change it on AutoCAD or prep the RVT pre-export?

Tim Burnham  17 days ago
@dobres We are creating a "super exporter" where we want to leverage the export to dwg feature via API but also we want to export (separately) a lot of param data and change layers outside the settings feature.  If you look at my ZIP, there is one specific example where a family instance comes over as individual primitives on ANNO layer, whereas I would expect one block on Geo layer. Because its broken up into many primitives I can't retrace it back to the original fam instance nor know how to group them into a new block since the xdata for at least one of the prims doesn't have original fam instance revit ID.   Could you look at the attache?  The two images provided paint the problem best.

Angel Velez:squirrel:  17 days ago
Have you looked at IFC?  Is DWG the base point you want for geometry + data?

Tim Burnham  17 days ago
Stantec wants a fully automated DWG deliverable to their customers where the model stems from Revit.  They do a lot of cleanup work that I'm automating.  IFC is not preferred in this case and these guys are pretty neck deep for years into the export feature so the issues are very isolated.

Tim Burnham  17 days ago
The attached zip above is really asking how I can either prep the fam instance on Revit's side so that it exports as a block, or is there something i can do on the autocad side to discover and group all these primitives into a block.  Hoping the former has a workflow I can do.

Stefan Dobre  17 days ago
I looked at the attached files and I understood the issue. It’s about nested families in certain conditions – The nested Family is “Shared” and the nested family is an annotation family while the main one isn't.
Let me check with my Product Owner  and see what we can do. Today he is on vacation. I’ll discuss tomorrow with him and come back with an answer

Tim Burnham  17 days ago
Awesome that is great, thank you!  If anything could be done API wise I am happy to write.  Cheers!

Stefan Dobre  16 days ago
In your example the symbol (shared family) has a mark that is different from the mark of family instance containing it. It looks like the intent is to schedule the two components differently, even though in Revit they are placed using the same family.
If this is the case, you can add a logic to group all the primitives from the shared family into its own block.
You can look at each entity’s xData where you will find ID of element that produced that entity.
I don’t know of a way to get the ID of the family instance and tie it to all the shared families contained, in case you want a block containing all the entities from the family and shared families, so I’m adding @dobriai.

Tim Burnham  16 days ago
This is great, thank you Stefan.  One other easy'ish question. What are other xdata index values indicate?

/a/doc/revit/tbc/git/a/img/rvt_dwg_export_xdata.png

Stefan Dobre  15 days ago
Here are the codes that we use:

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
