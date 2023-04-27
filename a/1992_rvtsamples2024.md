<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- /Users/jta/Pictures/2023/2023-04-26_nice_aps_accelerator/

- RevitLookup release 2024.0.4, and support for previous Revit versions
  Links to the most recent RevitLookup versions for each Revit version are listed here:
  https://github.com/jeremytammik/RevitLookup/wiki/Versions

- RvtSamples 2024
  Y:\a\rvt\rvt_2024_empty.rvt

  rvtsamples2024.png

REVIT-206304 [Update RvtSamples.txt for Revit 2024 SDK]

https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2024.0.0.3

Compiling the Revit 2023 SDK Samples
https://thebuildingcoder.typepad.com/blog/2022/04/compiling-the-revit-2023-sdk-samples.html

Set Up RvtSamples
https://thebuildingcoder.typepad.com/blog/2022/04/compiling-the-revit-2023-sdk-samples.html#7

C:\Users\jta\AppData\Roaming\Autodesk\Revit\Addins\2024>
copy Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\RvtSamples\CS\RvtSamples.txt
copy Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\RvtSamples\CS\RvtSamples.addin

DatumsModification

DatumAlignment
DatumPropagation
DatumStyleModification

ContextualAnalyticalModel

The SDK source code actually implements the following 21 ContextualAnalyticalModel external commands:

Use `grep "class.*IExternalCom" *cs`
AddAssociation
AddCustomAssociation
AnalyticalNodeConnStatus
CreateAnalyticalPanel
CreateAnalyticalCurvedPanel
CreateAnalyticalMember
CreateAreaLoadWithRefPoint
CreateCustomAreaLoad
CreateCustomLineLoad
CreateCustomPointLoad
FlipAnalyticalMember
MemberForcesAnalyticalMember
ModifyPanelContour
MoveAnalyticalMemberUsingElementTransformUtils
MoveAnalyticalMemberUsingSetCurve
MoveAnalyticalNodeUsingElementTransformUtils
MoveAnalyticalPanelUsingElementTransformUtils
MoveAnalyticalPanelUsingSketchEditScope
ReleaseConditionsAnalyticalMember
RemoveAssociation
SetOuterContourForPanels

These are the ContextualAnalyticalModel external commands listed in RvtSamples.txt:

Use `grep "^ContextualAnalyticalModel" RvtSamples.txt | sort`
ContextualAnalyticalModel.AddRelation
ContextualAnalyticalModel.AnalyticalNodeConnStatus
ContextualAnalyticalModel.BreakRelation
ContextualAnalyticalModel.CreateAnalyticalCurvedPanel
ContextualAnalyticalModel.CreateAnalyticalMember
ContextualAnalyticalModel.CreateAnalyticalPanel
ContextualAnalyticalModel.FlipAnalyticalMember
ContextualAnalyticalModel.MemberForcesAnalyticalMember
ContextualAnalyticalModel.ModifyPanelContour
ContextualAnalyticalModel.MoveAnalyticalMemberUsingElementTransformUtils
ContextualAnalyticalModel.MoveAnalyticalMemberUsingSetCurve
ContextualAnalyticalModel.MoveAnalyticalNodeUsingElementTransformUtils
ContextualAnalyticalModel.MoveAnalyticalPanelUsingElementTransformUtils
ContextualAnalyticalModel.MoveAnalyticalPanelUsingSketchEditScope
ContextualAnalyticalModel.ReleaseConditionsAnalyticalMember
ContextualAnalyticalModel.SetOuterContourForPanels
ContextualAnalyticalModel.UpdateRelation

RvtSamples: The name already exists in pulldown:Infrastructure alignments

Infrastructure Alignment Station Label
Infrastructure Alignment Properties

The Toposolid sample only has one entry in RvtSamples.txt specifying an external command named

- Revit.SDK.Samples.Toposolid.CS.Command

This command does not exist. Instead, the sample implements the following external commands:

ToposolidCreation
ToposolidFromDWG
ContourSettingCreation
ContourSettingModification
ToposolidFromSurface
SSEPointVisibility
SplitToposolid
SimplifyToposolid

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\PhaseSample\VB.NET\bin\Debug\PhaseSample.dll' specified in line 175 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Exception 'Could not load file or assembly 'file:///Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\PhaseSample\VB.NET\bin\Debug\PhaseSample.dll' or one of its dependencies. The system cannot find the file specified.'
testing assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\PhaseSample\VB.NET\bin\Debug\PhaseSample.dll'
specified in line 175 of RvtSamples.txt

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\DesignOptionReader\VB.NET\bin\Debug\DesignOptionReader.dll' specified in line 203 of RvtSamples.txt not found

[OK]

\\VB.NET\\bin\\Debug\\

\\VB.NET\\bin\\


[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
External command class Revit.SDK.Samples.DatumsModification.CS.Command in assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\DatumsModification\CS\bin\Debug\DatumsModification.dll' specified in line 371 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
External command class Revit.SDK.Samples.Toposolid.CS.Command in assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\Toposolid\CS\bin\Debug\Toposolid.dll' specified in line 490 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
External command class ContextualAnalyticalModel.AddRelation in assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\ContextualAnalyticalModel\CS\bin\Debug\ContextualAnalyticalModel.dll' specified in line 532 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
External command class ContextualAnalyticalModel.UpdateRelation in assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\ContextualAnalyticalModel\CS\bin\Debug\ContextualAnalyticalModel.dll' specified in line 539 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
External command class ContextualAnalyticalModel.BreakRelation in assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\ContextualAnalyticalModel\CS\bin\Debug\ContextualAnalyticalModel.dll' specified in line 546 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\Viewers\ElementViewer\VB.NET\bin\ElementViewer.dll' specified in line 1008 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Exception 'Could not load file or assembly 'file:///Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\Viewers\ElementViewer\VB.NET\bin\ElementViewer.dll' or one of its dependencies. The system cannot find the file specified.'
testing assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\Viewers\ElementViewer\VB.NET\bin\ElementViewer.dll'
specified in line 1008 of RvtSamples.txt

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\CreateShared\VB.NET\bin\CreateShared.dll' specified in line 1316 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Exception 'Could not load file or assembly 'file:///Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\CreateShared\VB.NET\bin\CreateShared.dll' or one of its dependencies. The system cannot find the file specified.'
testing assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\CreateShared\VB.NET\bin\CreateShared.dll'
specified in line 1316 of RvtSamples.txt

[OK]

-----
[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
External command class Revit.SDK.Samples.Toposolid.CS.Command in assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\Toposolid\CS\bin\Debug\Toposolid.dll' specified in line 504 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\Viewers\ElementViewer\VB.NET\bin\ElementViewer.dll' specified in line 1050 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Exception 'Could not load file or assembly 'file:///Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\Viewers\ElementViewer\VB.NET\bin\ElementViewer.dll' or one of its dependencies. The system cannot find the file specified.'
testing assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\Viewers\ElementViewer\VB.NET\bin\ElementViewer.dll'
specified in line 1050 of RvtSamples.txt

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\CreateShared\VB.NET\bin\CreateShared.dll' specified in line 1358 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\CreateShared\VB.NET\bin\CreateShared.dll' specified in line 1365 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
The name already exists in pulldown:Infrastructure alignments
Parameter name: newItemName: n = 1862, k = 1862, lines[k] = eof

[OK]

-----

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\TestFloorThickness\VB.NET\bin\TestFloorThickness.dll' specified in line 1477 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
Assembly 'Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\TestWallThickness\VB.NET\bin\TestWallThickness.dll' specified in line 1484 of RvtSamples.txt not found

[OK]

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
The name already exists in pulldown:Create Analytical Member
Parameter name: newItemName: n = 1911, k = 1911, lines[k] = eof

[OK]

-------

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
The name already exists in pulldown:Datumplane modification
Parameter name: newItemName: n = 1911, k = 1911, lines[k] = eof

[OK]

-----

[Window Title]
RvtSamples - RvtSamples

[Main Instruction]
The name already exists in pulldown:Toposolid API Samples
Parameter name: newItemName: n = 1911, k = 1911, lines[k] = eof

[OK]




twitter:

APS cloud accelerators in Nice and Medellin, and configuring RvtSamples for the Revit 2024 SDK samples in the @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS https://autode.sk/rvt2024sdk

APS cloud accelerators in Nice and Medellin
&ndash; Compiling the Revit 2024 SDK samples
&ndash; Visual introduction to machine learning...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Configuring RvtSamples 2024

I'm in Nice, France, today and tomorrow, attending the [APS cloud accelerator](https://aps.autodesk.com/accelerator-program).
This one is help in parallel with another one in Medellin, Columbia, so we are hosting two at the same time this week.

After successfully [compiling the Revit 2024 SDK samples](https://thebuildingcoder.typepad.com/blog/2023/04/nice-accelerator-and-compiling-the-revit-2024-sdk.html),
the time os now ripe to configure the RvtSamples external application to load all the external commands defined by the Revit 2024 SDK samples.

Mainly, this consists of editing RvtSamples.txt, the input text file specifying the name and location of the commands and the .NET assembly DLLs implementing them.

By the way, we also have a new release of RevtLookup to celebrate, including some minor fixes:


####<a name="2"></a> RevitLookup 2024.0.4

Roman [Nice3point](https://github.com/Nice3point) published another update,
[RevitLookup 2024.0.4](https://github.com/jeremytammik/RevitLookup/releases/tag/2024.0.4):

Improvements include:

- Added Workset support
- Added WorksetTable support
- Added Document.GetUnusedElements support
- Fixed Dashboard window startup location

<center>
<img src="img/revitlookup2024dashboard.png" alt="RevitLookup 2024 dashboard" title="RevitLookup 2024 dashboard" width="800"/> <!-- Pixel Height: 2,014
Pixel Width: 2,096 -->
</center>

####<a name="3"></a> Configuring RvtSamples 2024

I installed the Revit 2024 SDK samples and updated
the [RevitSdkSamples repository](https://github.com/jeremytammik/RevitSdkSamples) to
the original pristine [release 2024.0.0.0](https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2024.0.0.0).



I captured this state of affairs
in [RevitSdkSamples release 2024.0.0.2](https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2024.0.0.2).


