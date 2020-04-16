<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- set up .net framework 4.8
  https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-web-installer
  dotnet_4_8_download.png
  https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-developer-pack-offline-installer
  developer pack installer
  
- /a/doc/revit/tbc/git/a/zip/revitlookup_2021_warnings_01.txt
  installed Revit 2021 SDK to C:\a\lib\2021\SDK
  installed .net framework 4.8 developer tools
  flat migration of RevitLookup
  https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.0

- harry mattison added support for mult-config
  to build, i had to set the configuration
  it defaulted to 2019, which i do not have installed, so the Revit API assemblies were not found
  vs_configuration_manager.png
  once i set the configuration to Revit 20201, it compiled as before, obviously still with the [three warnings](zip/revitlookup_2021_warnings_01.txt) listed above.

twitter:

Revit 2021 add-ins require .NET 4.8, RevitLookup migration and support for multi-release building in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon https://bit.ly/revitlookup2021

I hope you are happy and healthy and enjoyed your Easter eggs!
During the holiday, I updated RevitLookup for Revit 2021, and Harry Mattison added his multi-release building enhancements into the main solution as well
&ndash; Revit 2021 add-ins require .NET 4.8
&ndash; RevitLookup flat migration to Revit 2021
&ndash; Support for multi-release building...

linkedin:

Revit 2021 add-ins require .NET 4.8, RevitLookup migration and support for multi-release building in the #RevitAPI

https://bit.ly/revitlookup2021

I hope you are happy and healthy and enjoyed your Easter eggs!

During the holiday, I updated RevitLookup for Revit 2021, and Harry Mattison added his multi-release building enhancements into the main solution as well:

- Revit 2021 add-ins require .NET 4.8
- RevitLookup flat migration to Revit 2021
- Support for multi-release building...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### RevitLookup 2021 with Multi-Release Support

I hope you are happy and healthy and enjoyed your Easter eggs!

[Revit 2021](https://thebuildingcoder.typepad.com/blog/2020/04/revit-2021-cloud-model-api.html#2) was
released last week with
its [multi-region cloud model API](https://thebuildingcoder.typepad.com/blog/2020/04/revit-2021-cloud-model-api.html#4) and
numerous other enhancements.

During the holiday, I updated RevitLookup for Revit 2021, and Harry Mattison added his multi-release building enhancements into the main solution as well:

- [Revit 2021 add-ins require .NET 4.8](#2)
- [RevitLookup flat migration to Revit 2021](#3)
- [Support for multi-release building](#4)

#### <a name="2"></a>Revit 2021 Add-Ins Require .NET 4.8

I installed the Revit SDK to read about the new Revit add-in system requirements in the *What's New* section of the help file:

<center>
<img src="img/revit_2021_addin_requirements.png" alt="Add-in requirements" title="Add-in requirements" width="493"/> <!-- 493 -->
</center>

The Revit 2021 API assemblies are built using .NET 4.8.

At a minimum, add-ins need to target .NET 4.8 for Revit 2021. 

Accordingly, I set up the .NET framework 4.8 and developer pack on my system from the [Microsoft .NET](https://dotnet.microsoft.com) website:

- [Download .NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

The developer pack is apparently required from Visual Studio to offer the option of targeting .NET 4.8.

The framework itself has already been installed by Revit 2021:

<center>
<img src="img/dotnet_4_8_download.png" alt=".NET 4.8 installation" title=".NET 4.8 installation" width="800"/> <!-- 2002 -->
</center>


#### <a name="3"></a>RevitLookup Flat Migration to Revit 2021

With the .NET 4.8 developer pack installed, I was ready for the flat migration of RevitLookup to the new version.

I incremented the RevitLookup .NET framework target version and pointed to the new location for the Revit API references.

It compiled right away with zero errors.

The compilation does cause [three warnings](zip/revitlookup_2021_warnings_01.txt), though, associated with deprecated enumerations, properties and methods due to the Units API changes documented in the help file:

- Warning	CS0618 `DisplayUnitType` is obsolete: Please use the `ForgeTypeId` class instead. Use constant members of the `UnitTypeId` class to replace uses of specific values of this enumeration.
- Warning	CS0618 `UnitUtils.GetValidDisplayUnits(UnitType)` is obsolete: Please use the `GetValidUnits(ForgeTypeId)` method instead.
- Warning	CS0618 `Field.UnitType` is obsolete: Please use the `GetSpecTypeId()` method instead.

We'll take a closer look at these later.

The result of this initial flat migration is available
as [RevitLookup release 2021.0.0.0](https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.0).

#### <a name="4"></a>Support for Multi-Release Building

[Harry Mattison](https://github.com/harrymattison) of [Boost your BIM](https://twitter.com/BoostYourBIM) added
support for multi-release building in his
subsequent [pull request 58 &ndash; solution changes for multi-release building](https://github.com/jeremytammik/RevitLookup/pull/58):

> Create a signed MSI installer using Advanced Installer.
Modify the Visual Studio solution to use macro variables to ease the process of building for different Revit versions.
All you need to do now is create a new configuration and the output direction, DLL references, and other items will automatically update.
It is designed to be very generic and not at all specific to me or anyone else.
The `sign.bat` line for signing the installed is commented out with a `REM` statement, so it won't affect anyone in its current state.
I left it there as a guide for other people to see a nice way to do the signing in a post-build event.

You can read more about the Advanced Installer that Harry used in his own article
on [RevitLookup install for Revit 2021 and using Advanced Installer for easy MSI generation](https://boostyourbim.wordpress.com/2020/04/15/revit-lookup-install-for-revit-2021).

Many thanks to Harry for this useful contribution!

After integrating his changes, all I had to do was set the configuration to Revit 2021.

It had defaulted to 2019, which I do not have installed, so the Revit API assemblies were not found initially.

<center>
<img src="img/vs_configuration_manager.png" alt="Visual Studio configuration manager" title="Visual Studio configuration manager" width="701"/> <!-- 701 -->
</center>

Once I set the configuration to Revit 20201, it compiled as before, obviously still with the
same [three warnings](zip/revitlookup_2021_warnings_01.txt) listed above.

The current release of RevitLookup including Harry's enhancements
is [2021.0.0.2](https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.2).

I look forward to receiving your pull request to cover new aspects of the new Revit API functionality!

<center>
<img src="img/pandemic_influence_on_relative_importance.png" alt="Pandemic influence on relative importance" title="Pandemic influence on relative importance" width="360"/>
<p style="font-size: 80%; font-style:italic">Pandemic influence on relative importance</p>
</center>
