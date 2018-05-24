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

Installing the Revit 2019 SDK April update in #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/rvt2019sdkapril

After the significant struggle I had to compile the initial release of the Revit 2019 SDK samples and set up RvtSamples 2019, I am happy to report that installing and compiling the Revit 2019 SDK April 27 update is a lot easier
&ndash; Downloading the April 27 SDK update 
&ndash; Initial compilation &ndash; 41 warnings 
&ndash; Processor architecture mismatch suppressed &ndash; 5 warnings 
&ndash; Update reference to <code>RevitAPISteel.dll</code> &ndash; 3 warnings 
&ndash; Setting up <code>RvtSamples</code> 
&ndash; Updated <code>RvtSamples</code> download...

--->

### Installing the Revit 2019 SDK April Update

After the significant struggle I had
to [compile the initial release of the Revit 2019 SDK samples](http://thebuildingcoder.typepad.com/blog/2018/04/compiling-the-revit-2019-sdk-samples.html)
and [set up RvtSamples 2019](http://thebuildingcoder.typepad.com/blog/2018/04/rvtsamples-2019.html),
I am happy to report that installing and compiling the Revit 2019 SDK April 27 update is a lot easier:

- [Downloading the April 27 SDK update](#2) 
- [Initial compilation &ndash; 41 warnings](#3) 
- [Processor architecture mismatch suppressed &ndash; 5 warnings](#4) 
- [Update reference to `RevitAPISteel.dll` &ndash; 3 warnings](#5) 
- [Setting up `RvtSamples`](#6) 
- [Updated `RvtSamples` download](#7) 


####<a name="2"></a>Downloading the April 27 SDK Update

We are still waiting for the Revit 2019 SDK April 27 Update to appear in its proper location in
the [Revit Developer Centre](http://autodesk.com/developrevit)
at [autodesk.com/developrevit](http://autodesk.com/developrevit),
which nowadays points to a new location
at [www.autodesk.com/developer-network/platform-technologies/revit](https://www.autodesk.com/developer-network/platform-technologies/revit).

If you are in a hurry, you can grab it right now from
the [direct `REVIT_2019_SDK.msi` download link](http://download.autodesk.com/us/revit-sdk/REVIT_2019_SDK.msi),
which is [download.autodesk.com/us/revit-sdk/REVIT_2019_SDK.msi](http://download.autodesk.com/us/revit-sdk/REVIT_2019_SDK.msi).

The resulting Revit 2019 SDK update installer file dated April 27, 2018, is 375521280 bytes in size.


####<a name="3"></a>Initial compilation &ndash; 41 Warnings

I ran the installer and loaded the SDK solution `SDKSamples` into Visual Studio.

The first compilation  worked pretty well right out of the box.

It reported [186 projects succeeded, 0 failed, 0 errors and 41 warnings](zip/revit_2019_sdk_samples_errors_warnings_9_1.txt).

Almost all the warnings are related to the processor architecture mismatch:

- Warning: There was a mismatch between the processor architecture of the project being built "MSIL" and the processor architecture of the reference "RevitAPI", "AMD64". This mismatch may cause runtime failures. Please consider changing the targeted processor architecture of your project through the Configuration Manager so as to align the processor architectures between your project and references, or take a dependency on references with a processor architecture that matches the targeted processor architecture of your project.	RebarFreeForm			

These warnings can easily be suppressed using
my [`DisableMismatchWarning.exe` command line utility](https://github.com/jeremytammik/DisableMismatchWarning) 
to [recursively disable architecture mismatch warnings](http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html).

####<a name="4"></a>Eliminated Processor Architecture Mismatch Warnings &ndash; 5 Warnings

After running the DisableMismatchWarning utility,
the [number of warnings is reduced from 41 to 5](zip/revit_2019_sdk_samples_errors_warnings_9_2.txt).

- FrameBuilder: Could not find rule set file "Migrated rules for FrameBuilder.ruleset".				
- GeometryCreation_BooleanOperation: Could not find rule set file "GeometryCreation_BooleanOperation.ruleset".				
- ProximityDetection_WallJoinControl: Could not find rule set file "ProximityDetection_WallJoinControl.ruleset".				
- SampleCommandsSteelElements: The referenced component 'RevitAPISteel' could not be found.				
- SampleCommandsSteelElements: Could not resolve this reference. Could not locate the assembly "RevitAPISteel". Check to make sure the assembly exists on disk. If this reference is required by your code, you may get compilation errors.				

I will ignore the first three for now.

The last two are more serious, of course.

####<a name="5"></a>Update Reference to RevitAPISteel.dll &ndash; 3 Warnings

The missing reference to the `RevitAPISteel.dll` .NET library assembly can be easily resolved by manually updating it to point to the existing DLL in the Revit executable folder:

- C:\Program Files\Autodesk\Revit 2019\RevitAPISteel.dll

That reduced
the [number of warning messages from 5 to 3](zip/revit_2019_sdk_samples_errors_warnings_9_3.txt),
which, as said, I will ignore:

- FrameBuilder: Could not find rule set file "Migrated rules for FrameBuilder.ruleset".				
- GeometryCreation_BooleanOperation: Could not find rule set file "GeometryCreation_BooleanOperation.ruleset".				
- ProximityDetection_WallJoinControl: Could not find rule set file "ProximityDetection_WallJoinControl.ruleset".				

####<a name="6"></a>Setting up RvtSamples

With the SDK samples compiling successfully, and the number of warnings reduced to a tolerable number, I next set up RvtSamples to load all the external commands.

First of all, I added the add-in manifest and the RvtSamples input text file to the project for easier access and modification:

<center>
<img src="img/RvtSamples_project_files.png" alt="RvtSamples project files" width="269"/>
</center>

Next, I updated the paths in both of them to point to my SDK samples folder.

The original input text file still refers to *C:/Revit Copernicus SDK/Samples/*.

I replaced the backslashes to forward slashes for simpler regular expression editing and updated the paths to point to my installation in *C:/a/lib/revit/2019/SDK/Samples/*.

To test that all the external commands listed in the text file are found, I temporarily toggled the `testClassName` flag to true:

<pre class="code">
  <span style="color:blue;">bool</span>&nbsp;testClassName&nbsp;=&nbsp;<span style="color:blue;">true</span>;&nbsp;<span style="color:green;">//&nbsp;jeremy</span>
</pre>

<!----

It brings up the same old well-known indexing error that has remained unchanged for years:

<center>
<img src="img/RvtSamples_index_error.png" alt="RvtSamples.txt index error" width="372"/>
</center>

<pre>
  [Window Title]
  RvtSamples External Application - RvtSamples
  
  [Main Instruction]
  Index and count must refer to a location within the string.
  Parameter name: count: n = 1364, k = 130, lines[k] = C:/a/lib/revit/2019/SDK_2_jeremy/Samples/PlacementOptions\CS\bin\Debug\PlacementOptions.dll
  
  [OK]
</pre>

---->

With that flag enabled, a number of warnings are issued:

- VB not loaded DeleteObject 63
- VB not loaded HelloRevit 84
- Tooltip missing PlacementOptions 147 fixed
- VB not loaded RotateFramingObjects 210
- VB not loaded MaterialProperties 805
- External command not found FabricationPartLayout 854
- Assembly missing CreateShared 917 removed
- Assembly missing CreateShared 924 removed
- VB not loaded SlabProperties 1008
- VB not loaded CreateBeamsColumnsBraces 1050
- VB not loaded StructuralLayerFunction 1267
- Assembly does not exist 1365

After some more twiddling, just the seven or eight VB problems remain, and I am satisfied:

<center>
<img src="img/RvtSamples_2019_april_27_update.png" alt="RvtSamples ribbon panel" width="797"/>
</center>

I mustn't forget to toggle off the debugging flag again...


####<a name="7"></a>Updated RvtSamples Download

For your convenience, here is my freshly
baked [RvtSamples_2019_april_update.zip archive file](/a/doc/revit/tbc/git/a/zip/RvtSamples_2019_april_update.zip).

