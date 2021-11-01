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

This exciting rapid evolution continues with an untiring stint of contributions
from Roman [@Nice3point](https://github.com/Nice3point) and
his extensive series of pull requests:

- [100](https://github.com/jeremytammik/RevitLookup/pull/100) &ndash; Fix naming and implerment pattern matching
- [101](https://github.com/jeremytammik/RevitLookup/pull/101) &ndash; Cleanup and build system
- [102](https://github.com/jeremytammik/RevitLookup/pull/102) &ndash; Changelog and remove unused files 
- [104](https://github.com/jeremytammik/RevitLookup/pull/104) &ndash; Fix snoop db exception 
- [105](https://github.com/jeremytammik/RevitLookup/pull/105) &ndash; Update badges 
- [107](https://github.com/jeremytammik/RevitLookup/pull/107) &ndash; Renaming 
- [108](https://github.com/jeremytammik/RevitLookup/pull/108) &ndash; Multi-version installer 
- [110](https://github.com/jeremytammik/RevitLookup/pull/110) &ndash; Update Readme.md 

As a result, RevitLookup now boasts a modern up-to-date build system, a multi-version installer, a separate GitHub developer branch `dev`, and many other enhancements:

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

####<a name="3"></a> Bye Bye Lookup Builds

Until now, you could always download the most recent build of RevitLookup
from [lookupbuilds.com](https://lookupbuilds.com),
provided by [Build Informed](https://www.buildinformed.com),
[implemented back in 2017](https://thebuildingcoder.typepad.com/blog/2017/04/forgefader-ui-lookup-builds-purge-and-room-instances.html#3) and
diligently maintained ever since by [Peter Hirn](https://github.com/peterhirn).

The new build system broke that and replaces it, as discussed in
the [issue #103 &ndash; Gitlab pipeline broken](https://github.com/jeremytammik/RevitLookup/issues/103).

Ever so many thanks once again to Peter for all his work with this over the past years!


####<a name="3"></a> Image Cleanup, Robot Arm and Happiness

Some nice little snippets pointed out to me by colleagues yesterday:

- [Quick and free alternative for cleaning up artifacts in images](https://cleanup.pictures)
- [Robotic arm with full range of motion and static strength](https://youtu.be/H19p43NFqp4)
- [Sam Berns' TEDx talk on his philosophy for a happy life](https://youtu.be/36m1o-tM05g)



<center>
<img src="img/" alt="Outdoor seatbelt" title="Outdoor seatbelt" width="360"/> <!-- 720 -->
</center>

