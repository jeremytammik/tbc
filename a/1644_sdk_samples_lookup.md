<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

RevitLookup 2019 and new Revit SDK samples for #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/lookup2019newsamples

I migrated RevitLookup to Revit 2019, which was very easy.
Next, I compared the directory contents to discover the new SDK samples
&ndash; RevitLookup 2019
&ndash; New Revit 2019 SDK samples...

--->

### RevitLookup 2019 and New SDK Samples

Last week, I described how I installed Revit 2019
and [compiled the Revit 2019 SDK samples](http://thebuildingcoder.typepad.com/blog/2018/04/compiling-the-revit-2019-sdk-samples.html).

On Sunday, I migrated RevitLookup to Revit 2019, which was very easy.

Next, I compared the directory contents to discover the new SDK samples:

- [RevitLookup 2019](#2) 
- [New Revit 2019 SDK samples](#3) 


####<a name="2"></a>RevitLookup 2019

The migration of [RevitLookup](https://github.com/jeremytammik/RevitLookup) to Revit 2019 was trivial.

It just required updating the .NET framework target version to 4.7 and pointing the Revit API assembly references to the new DLLs.

No code changes were needed.

To add the final finishing touch, I also updated the readme file with a new Revit version badge.

The current version is [2019.0.0.1](https://github.com/jeremytammik/RevitLookup/releases/tag/2019.0.0.1).

[Builds](https://github.com/jeremytammik/RevitLookup#builds) are available
from [lookupbuilds.com](https://lookupbuilds.com).


####<a name="3"></a>New Revit 2019 SDK Samples

Comparing the Revit SDK directory contents, I discovered the following new samples:

- [AppearanceAssetEditing](http://thebuildingcoder.typepad.com/blog/2017/11/modifying-material-visual-appearance.html) &ndash; added
in [Revit 2018.1](http://thebuildingcoder.typepad.com/blog/2017/08/revit-20181-and-the-visual-materials-api.html).
- RebarFreeForm &ndash; external command to create a Rebar FreeForm element and external application to implement the custom server used to regenerate the rebar geometry based on constraints.
- SampleCommandsSteelElements &ndash; sample commands for steel elements demonstrating creation, modification and deletion of them.

The new API functionality is discussed in the document *Revit Platform API Changes and Additions.docx* and the *What's New* section of the Revit API help file RevitAPI.chm.

The SDK also sports a new undocumented structural analysis DLL:

- Structural Analysis SDK/Examples/References/CodeChecking/Engineering/rcuapiNET.dll

<center>
<img src="img/steel_connection.png" alt="Steel connection" width="260"/>
</center>
