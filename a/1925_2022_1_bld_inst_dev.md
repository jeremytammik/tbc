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

### Revit 2022.1 SDK and RevitLookup Installer

Exciting news and a lot of changes to RevitLookup:

- [Revit 2022.1 SDK released](#2)
- [`WallCrossSection` vs. `WallCrossSectionDefinition`](#3)
- [RevitLookup build and install](#4)
- [Bye bye lookup builds](#5)
- [Image cleanup and a robot arm](#6)

####<a name="2"></a> Revit 2022.1 SDK Released

The Revit 2022.1 SDK has been released on 
the [Revit Developer Center](https://www.autodesk.com/developer-network/platform-technologies/revit).

The initial Revit 2022 SDK dates from April 12, 2021.

The updated Revit 2022.1 SDK was released on October 28, 2021.

It includes important enhancements addressing new Revit product functionality and developer wishes and requests.
A list of what's new is provided in the SDK documentation in the *What's New* section and the *Changes and Additions* document.
I'll put that information online for easier searching and finding asap.

####<a name="3"></a> WallCrossSection vs. WallCrossSectionDefinition

The Revit 2022.1 unfortunately introduced a breaking change:

`WallCrossSection` was renamed to `WallCrossSectionDefinition`

The Revit team is working on a knowledge base article to document this and provide a recommendation on how to handle it.

The issue was brought to our attention by [@ricaun](), Luiz Henrique Cassettari, in
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [RevitApi 2022 update change WallCrossSection to WallCrossSectionDefinition](https://forums.autodesk.com/t5/revit-api-forum/revitapi-2022-update-change-wallcrosssection-to/td-p/10720345):

**Question:** I was messing with `GroupTypeId` and found something interesting and a little worrying.

On my computer, I have the Revit 2022 first release.

- Product Version: 20210224_1515(x64)
- RevitApi.dll Version: 22.0.2.392

My plugin application uses
the NuGet package [Revit_All_Main_Versions_API_x64](https://www.nuget.org/packages/Revit_All_Main_Versions_API_x64),
and I usually use the last version:

- Product Version: 20210921_1515(x64)
- RevitApi.dll Version: 22.1.1.516

The `GroupTypeId` property `WallCrossSection` changed to `WallCrossSectionDefinition` in the Revit 2022.1 update.

If I use the old property, it will throw an exception on the new version.

I use the new one, the first Revit 2022 release will break.

I will probably never use this `ForgeTypeId`, but I worry that other things could change in this update.

Should I be worried about my application breaking each time a new Hotfix is released?

**Answer:** Very sorry about this mishap!

No, you should not need to worry about that; this is an exceptional case and an error in the update release.

Thank you very much for pointing it out!

This launched a significant discussion in the development team.

First of all, it did indeed happen as you noted.

Secondly, it should not have happened.

They are discussing both how to handle this specific issue and how to prevent anything similar from occurring in the future.

The simple workaround for the moment is to use the integer value of the enum to cover both 2022.0 and 2022.1.

They discussed reverting back again in future updates, but that would cause even more disruption.

They discussed defining both enumerations with the same underlying integer values.

For the moment, just using the underlying integers is the safest way to go, I guess:

<pre class="code>
  // BuiltInParameterGroup.PG_WALL_CROSS_SECTION_DEFINITION; // -5000228,
  // BuiltInParameterGroup.PG_WALL_CROSS_SECTION; // -5000228,
  var PG_WALL_CROSS_SECTION = (BuiltInParameterGroup)(-5000228);
</pre>

####<a name="4"></a> RevitLookup Build and Install

Moving on to more positive news, we were blessed last week with a very exciting contribution to create
a [modeless version of RevitLookup](https://thebuildingcoder.typepad.com/blog/2021/10/bridges-regeneration-and-modeless-revitlookup.html).

This rapid evolution continues with an untiring stint of contributions
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
<img src="img/revitlookup_installer.png" alt="RevitLookup installer" title="RevitLookup installer" width="517"/> <!-- 1034 -->
</center>

Here are some of Roman's explanations from our pull request conversations:

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
Some places have been optimized; for the ribbon, I created extension methods to enable shortening of lengthy repetitive data like this:

<pre class="code>
  optionsBtn.AddPushButton (new PushButtonData (" HelloWorld "," Hello World ... ", ExecutingAssemblyPath, typeof (HelloWorld) .FullName));
</pre>

It can now be written like this:

<pre class="code>
  optionsBtn.AddPushButton (typeof (HelloWorld)," HelloWorld "," Hello World ... ");
</pre>

The latest versions of the C# language allow you to write like this:

<pre class="code>
  MApp.DocumentClosed += m_app_DocumentClosed;
</pre>

instead of

<pre class="code>
 m_app.DocumentClosed += new EventHandler<Autodesk.Revit.DB.Events.DocumentClosedEventArgs>(m_app_DocumentClosed);
</pre>

Ever so many thanks to Roman for all his inspired work helping this tool move forward and especially his untiring efforts supporting me getting to grips with the new technology!

####<a name="5"></a> Bye Bye Lookup Builds

Until now, you could always download the most recent build of RevitLookup
from [lookupbuilds.com](https://lookupbuilds.com),
provided by [Build Informed](https://www.buildinformed.com),
[implemented back in 2017](https://thebuildingcoder.typepad.com/blog/2017/04/forgefader-ui-lookup-builds-purge-and-room-instances.html#3) and
diligently maintained ever since by [Peter Hirn](https://github.com/peterhirn).

The new build system broke that and replaces it, as discussed in
the [issue #103 &ndash; Gitlab pipeline broken](https://github.com/jeremytammik/RevitLookup/issues/103).

Ever so many thanks once again to Peter for all his work with this over the past years!

####<a name="6"></a> Image Cleanup and a Robot Arm

Two nice little snippets that caught my attention, a useful image editor tool and impressive novel hardware technology:

- [Quick and free alternative for cleaning up artifacts in images](https://cleanup.pictures)
- [Robotic arm with full range of motion and static strength](https://youtu.be/H19p43NFqp4)
