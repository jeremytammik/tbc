<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Revit 2022.1 SDK posting
  https://www.autodesk.com/developer-network/platform-technologies/revit

- Revit 2022.1 API unintended breaking change
Revit 2022.1 unfortunately introduced a breaking change.
https://autodesk.slack.com/archives/C0SR6NAP8/p1635432979033800
Revit team is working on Knowledge Base article.
RevitApi 2022 update change WallCrossSection to WallCrossSectionDefinition
https://forums.autodesk.com/t5/revit-api-forum/revitapi-2022-update-change-wallcrosssection-to/td-p/10720345
The good news is the same enum value like @jeremy.tammik comment, I did not test but this code should work in both versions 2022.0 and 2022.1
// BuiltInParameterGroup.PG_WALL_CROSS_SECTION_DEFINITION; // -5000228,
// BuiltInParameterGroup.PG_WALL_CROSS_SECTION; // -5000228,
var PG_WALL_CROSS_SECTION = (BuiltInParameterGroup)(-5000228);


- img/revitlookup_installer.png

twitter:

add #thebuildingcoder

Modeless RevitLookup, the most exciting enhancement in its entire history, and yet another need for regeneration accessing a read-only parameter with the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon https://autode.sk/modelesslookup

The most exciting RevitLookup enhancement in its entire history, yet another need for regeneration and a great new option for your personal safety
&ndash; Modeless RevitLookup
&ndash; Need for regen for read-only parameter
&ndash; Structural bridge design
&ndash; Outdoor seatbelt...

linkedin:

Modeless RevitLookup, the most exciting enhancement in its entire history, and yet another need for regeneration accessing a read-only parameter with the #RevitAPI

https://autode.sk/modelesslookup

- Modeless RevitLookup
- Need for regen for read-only parameter
- Structural bridge design
- Outdoor seatbelt...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Question:** 

**Answer:**

**Response:**  

Many thanks to  for this very helpful explanation!

<pre class="code">
</pre>

-->

###


[Revit Developer Center](https://www.autodesk.com/developer-network/platform-technologies/revit)

You can download the updated Revit SDK here:

Revit 2022.1 SDK (Update October 28, 2021)
Revit 2022 SDK (Update April 12, 2021)

####<a name="2"></a> Major additions to the Revit 2022.1 API
 
View API additions

Duplicating Sheets 

The new enum: 

- SheetDuplicateOption

allows you to indicate what information should be copied when duplicating a sheet. Its values are:

- DuplicateEmptySheet - Only copies the title block. 
- DuplicateSheetWithDetailing - Copies the title block and details. 
- DuplicateSheetWithViewsOnly - Copies the title block, details, viewports and contained views. The newly created sheet will reference the newly duplicated views. 
- DuplicateSheetWithViewsAndDetailing - Copies the title block, details, and viewports. Duplicates the sheet's contained views with detailing. The newly created sheet will reference the newly duplicated views.
- DuplicateSheetWithViewsAsDependent - Copies the title block, details, and viewports. Duplicates the sheet's contained views as dependent. The newly created sheet will reference the newly duplicated dependent views.

The new methods:

- ViewSheet.Duplicate(SheetDuplicateOption)  
- ViewSheet.CanBeDuplicated(SheetDuplicateOption)  

allow you to duplicate sheets and identify sheets which can be duplicated. 

Schedule API additions
Schedule heights on sheets

The new class:

- Autodesk.Revit.DB.ScheduleHeightsOnSheet

returns the heights of schedule title, column header and each body row on sheet view.

The new method: 

- ViewSchedule.GetScheduleHeightsOnSheet() 

will return the heights object. 

Managing schedule segments 

The new methods:

- ViewSchedule.IsSplit() 
- ViewSchedule.Split(int segmentNumber)  
- ViewSchedule.Split(IList<double> segmentHeights) 
- ViewSchedule.SplitSegment() 
- ViewSchedule.DeleteSegment() 
- ViewSchedule.MergeSegments() 
- ViewSchedule.GetSegmentCount() 
- ViewSchedule.GetSegmentHeight() 
- ViewSchedule.SetSegmentHeight() 

provide the ability to split schedules and manage schedule segments. 

The new method: 

- ViewSchedule.GetScheduleInstances() 

will return the schedule sheet instances for a schedule segment. 

The new methods: 

- ScheduleSheetInstance.SegmentIndex 
- ScheduleSheetInstance.Create(Document document, ElementId viewSheetId, ElementId scheduleId, XYZ origin, int segmentIndex) 

provide the ability to place a schedule segment on sheet and to get and set the schedule segment instance's segment index. 

Worksharing API additions

Delete Workset API

The new method:

- WorksetTable.DeleteWorkset()

supports deleting of worksets from the model.  It takes a DeleteWorksetSettings input with options for what to do with elements contained by that workset. 

Import and Export API additions 

AXM Import 

The new class:

- Autodesk.Revit.DB.AXMImportOptions

allows user to determine the import options when importing an AXM file.

The new method:

- Document.Import(String, AXMImportOptions, View)

imports an AXM file into the document.

The new method:

- OptionalFunctionalityUtils.IsAXMImportLinkAvailable()

checks if the Import FormIt function is available. 

OBJ & STL import/export

The new method:

- Document.Export(String, String, OBJExportOptions)

supports export of Revit geometry to OBJ format.  It uses a new class containing the options available for export:

- OBJExportOptions.TargetUnit
- OBJExportOptions.SurfaceTolerance
- OBJExportOptions.NormalTolerance
- OBJExportOptions.MaxEdgeLength
- OBJExportOptions.GridAspectRatio
- OBJExportOptions.SetTessellationSettings()

The new methods:

- Document.Import(String, OBJImportOptions, View)
- Document.Import(String, STLImportOptions, View)

provide support for importing files of STL and OBJ formats.  These methods use new classes representing the options for each of the new formats.

The new property:

- BaseImportOptions.DefaultLengthUnit

supports specification of a default length unit to use during import of unitless STL and OBJ files.



####<a name="2"></a> RevitLookup Build and Install

Just last week saw a very exciting contribution to create
a [modeless version of RevitLookup](https://thebuildingcoder.typepad.com/blog/2021/10/bridges-regeneration-and-modeless-revitlookup.html).

The excitement continued with an untiring stint of contributions from Roman [@Nice3point](https://github.com/Nice3point) in a series of pull requests:



- [110](https://github.com/jeremytammik/RevitLookup/pull/110) &ndash; Update Readme.md 
- [108 ](https://github.com/jeremytammik/RevitLookup/pull/108 ) &ndash; Multi-version installer 
- [107 ](https://github.com/jeremytammik/RevitLookup/pull/107 ) &ndash; Renaming 
- [105 ](https://github.com/jeremytammik/RevitLookup/pull/105 ) &ndash; Update badges 
- [104 ](https://github.com/jeremytammik/RevitLookup/pull/104 ) &ndash; Fix snoop db exception 
- [102 ](https://github.com/jeremytammik/RevitLookup/pull/102 ) &ndash; Changelog. Remove unused files 
- [101 ](https://github.com/jeremytammik/RevitLookup/pull/101 ) &ndash; Cleanup. Build system
- [100](https://github.com/jeremytammik/RevitLookup/pull/100) &ndash; CleanUp. Fix naming. Pattern matching

As a result, RevitLookup now boasts a modern up-to-date build system, a multi-version installer, separate GitHub developer branch `dev`, and many other enhancements:

<center>
<img src="img/.png" alt="RevitLookup installer" title="RevitLookup installer" width="600"/> <!--  -->
</center>

Here are some of his explanations from the pull request conversations:

- Corrected the style of the code in accordance with the latest guidelines. Access modifiers and some unused variables and methods are not affected. The .sln file has been moved to the root folder, otherwise the development environment will not capture the installer project and other supporting files.
- In recent commits, I have integrated the build system from my template https://github.com/Nice3point/RevitTemplates. Now the installer will build directly to github. After installation, I launched Revit, everything seems to work. For debugging added copying to AppData\Roaming\Autodesk\Revit\Addins\2022. To local build, net core 5 is required. If you still have version 3, please update
- You can check how git action works here https://github.com/Nice3point/RevitLookup/actions/runs/1392275582 Artifacts will have an installer
- to build installer on local machine install this via terminal dotnet tool install Nuke.GlobalTool --global Then you can run nuke command. Watch the video https://github.com/Nice3point/RevitTemplates/wiki/Installer-creation
- moved the changelog to a separate file.
- I think you can use this to automatically create a release https://github.com/marketplace/actions/create-release or https://github.com/marketplace/actions/automatic-releases or https://github.com/marketplace/actions/github-upload-release-artifacts
- Removed the version numbers from the .csproj file. This is redundant information and removes duplication. This solution is not a nuget package and is not used in other projects. Therefore, the most correct decision is to change the installer version number, the dll version number in this case does not affect anything. For now, all you need to remember is to update the version number here https://github.com/jeremytammik/RevitLookup/blob/master/installer/Installer.cs#L19 and generate a new guid https://github.com/jeremytammik/RevitLookup/blob/master/installer/Installer.cs#L37 after Revit 2023 is released
- Now the version of the file will be the same for the installer and dll. Will only be listed in the RevitLookup.csproj file
- The `20` prefix in `2022` can be left to keep the usual versioning. Here's what I found about the version limitation in Windows http://msdn.microsoft.com/en-us/library/aa370859%28v=vs.85%29.aspx
- Added all the latest versions of the plugin to the installer. DLLs are stored in the "Releases" folder, if you think that some versions are too outdated, delete the folder from there, the build system picks up the builds automatically, no hardcode. Adding the current assembly, now it is 2022, is not necessary, will cause a conflict. The current assembly is added after the project is compiled
- Kept the old documentation somewhere, as history, for nostalgic reasons, in honour of Jim Awe. It is authored by Jim Awe, the original implementor of both RevitLookup and the corresponding AutoCAD snooping tool, so it has historical value in itself, i think.
- you don't need to run nuke to debug. Only the green arrow on the VisualStudio panel. Nuke is used only for the purpose of building a project, it simplifies building if, for example, the project has several configurations, for example, for the 20th, 21st and 22nd versions of revit, Nuke build all dll variants at once. The build system is only needed to release a product.
- Also, the project was refactored taking into account the latest versions of the C# language.
Some places have been optimized, for the ribbon I created extension methods that are from

optionsBtn.AddPushButton (new PushButtonData (" HelloWorld "," Hello World ... ", ExecutingAssemblyPath, typeof (HelloWorld) .FullName));

write like this

optionsBtn.AddPushButton (typeof (HelloWorld)," HelloWorld "," Hello World ... ");

The latest versions of the C # language allow you to write like this:

MApp.DocumentClosed += m_app_DocumentClosed;

instead of

m_app.DocumentClosed += new EventHandler<Autodesk.Revit.DB.Events.DocumentClosedEventArgs>(m_app_DocumentClosed);

Ever so many thanks to Roman for all his inspired work helping this tool move forward and especially his untiring efforts supporting me getting to grips with the new technology!













####<a name="3"></a> Need for Regen for Read-Only Parameter

A surprising new context to add to
our [list of situations with a need for regeneration](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33) came up in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on being [unable to get parameter `AsString` value when the parameter is read-only](https://forums.autodesk.com/t5/revit-api-forum/unable-to-get-parameter-asstring-value-when-the-parameter-is/m-p/10713499):

**Question:** I've been struggling with this weird problem for a few hours now.

I have some elements that has a parameter called `PANEL ID`.
Sometimes, this parameter is in read-only mode.

When I'm accessing the element directly, I can get the parameter value perfectly fine.

When accessing the same kinds of elements as part of any collection, the value is an empty string `""`.

**Answer:** Seems like my problem was not using `doc.Regenerate()`.

My read-only params were updated from other elements, that changed those parameter values.
But I couldn't see it in the API until I used `doc.Regenerate()`.

Here are some other previous examples that I also already earmarked for inclusion in
the ['need to regenerate' list](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33):

- [`LevelOffset` not working for arc extrusion roof](https://forums.autodesk.com/t5/revit-api-forum/leveloffset-not-working-for-arc-extrusion-roof/m-p/7681949)
<br/>**Question:** I've encountered a strange result and I'd like to know if it's my fault or if there's an error somewhere or a known issue.
I'm trying to create a new `ExtrusionRoof` with an arc profile and then change the `Level Offset` parameter to a different value.
I use the `ROOF_CONSTRAINT_OFFSET_PARAM` `BuiltInParameter` to find that parameter and then set it.
Unexpectedly, Revit sets the parameter to my value, but the roof doesn't shift.
So, there's a discrepancy between the parameter value and the effective position of the roof.
<br/>**Answer:** You are creating geometry and then in the same transaction make a change to it.
In the past, such a workflow caused problems that could be solved by separating the creation and modification into 2 transactions.
I would suggest you do the same and see if that resolves your problem.
- [Circular chain of reference when creating opening on a floor](https://forums.autodesk.com/t5/revit-api-forum/error-circular-chain-of-reference-when-creating-opening-on-a/td-p/7681213)
<br/>**Question:** Unable to create opening on a created floor, posted error "circular chain of reference"
Problem is floor can be create while not the opening.
<br/>**Answer:** Try to regenerate the model after creating the floor and before creating the opening.

####<a name="4"></a> Structural Bridge Design

**Question:** A question came up on
accessing [Structural Bridge Design](https://www.autodesk.com/products/structural-bridge-design/overview) from
an external app.

**Answer:** There is no API, but there is a command line that you can use:

From the 2020 release on, we have command led automation for the design section and design beam creation and analysis.
All data and commands are written to a `json` file which drives the automation.
For more details, please refer
to the [ASBD Automation Overview](https://help.autodesk.com/view/SBRDES/ENU/?guid=ASBD_Automation_Overview_html).

####<a name="5"></a> Outdoor Seatbelt

If you learned to enjoy wearing a mask when alone, you might also feel safer with an outdoor seatbelt:

<center>
<img src="img/outdoor_seatbelt.jpg" alt="Outdoor seatbelt" title="Outdoor seatbelt" width="360"/> <!-- 720 -->
</center>

