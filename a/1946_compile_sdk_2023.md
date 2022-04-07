<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


twitter:

Compile Revit 2023 SDK samples, set up RvtSamples and update the RevitSdkSamples repo for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/rvt2023sdk

I log my work compiling the new Revit 2023 SDK and setting up RvtSamples to load the external commands
&ndash; Update the RevitSdkSamples repo
&ndash; Set the Revit API references
&ndash; Eliminate processor architecture mismatch warning
&ndash; Set up RvtSamples
&ndash; DatumsModification
&ndash; ContextualAnalyticalModel
&ndash; CivilAlignments...

linkedin:

Compile Revit 2023 SDK samples, set up RvtSamples and update the RevitSdkSamples repo for the #RevitAPI

https://autode.sk/rvt2023sdk

I log my work compiling the new Revit 2023 SDK and setting up RvtSamples to load the external commands:

- Update the RevitSdkSamples repo
- Set the Revit API references
- Eliminate processor architecture mismatch warning
- Set up RvtSamples
- DatumsModification
- ContextualAnalyticalModel
- CivilAlignments...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Compiling the Revit 2023 SDK Samples

The first thing I do after installing the new release of Revit is compile the Revit SDK and set up RvtSamples to load the external commands.
So, here we go again:

- [Update the RevitSdkSamples repo](#2)
- [Set the Revit API references](#3)
- [Eliminate processor architecture mismatch warning](#4)
- [Missing XML comments](#5)
- [Targets and rule sets](#6)
- [Set up RvtSamples](#7)
    - [DatumsModification commands](#7.2)
    - [ContextualAnalyticalModel commands](#7.3)
    - [CivilAlignments commands](#7.4)
- [Conclusion](#8)

####<a name="2"></a> Update the RevitSdkSamples Repo

I installed the new SDK and performed a pretty exhaustive comparison with the previous version to ensure that I do not overwrite anything important in
the [RevitSdkSamples repository](https://github.com/jeremytammik/RevitSdkSamples).

The advantage of maintaining a public repository for this is that changes can easily be tracked and I can share the steps that I take to compile the SDK samples and set up `RvtSamples` to load them.

I enjoyed the support and conversation with [jmcouffin](https://github.com/jmcouffin) during this process, in
his [pull request #1 2023SDK samples](https://github.com/jeremytammik/RevitSdkSamples/pull/1).
That was the first time anyone offered to help with this repository, by the way, so thank you very much indeed for that!

I compiled [diff_2022_2023.txt](zip/diff_2022_2023.txt) listing
the differences between the Revit 2023 SDK samples and previous versions, both in the directory structure and individual files.

Afaict from that analysis, three new samples were added:

- SheetToView3D
- SelectionChanged
- ContextualAnalyticalModel

We need to take a look at those asap.
First things first, though: compile the SDK and set up `RvtSamples`.


####<a name="3"></a> Set the Revit API References

The first attempt at compiling `SDKSamples.sln` right out of the box produced 9544 error and 1168 warnings;
the namespace `Autodesk` could not be found.

SDKSamples includes 199 solution projects, and I am not going to edit their references one by one, so I need to implement a batch update somehow.

Luckily, there is no need to do so either; searching globally for `RevitAPI.dll`, I find only 8 occurrences, in the `targets` files in the `VSProps` folder and in the `ContextualAnalyticalModel.csproj`.
Apparently, the latter is an exception.

I ended up fixing the Revit API references by replacing variable expressions by a constant *C:\Program Files\Autodesk\Revit 2023* in the following files:

- ContextualAnalyticalModel.csproj
- SDKSamples.CivilAlignments.targets
- SDKSamples.VB.targets
- SDKSamples.targets
- SDKSamples.Steel.targets

With those modifications, I was able to successfully compile all 199 projects with zero errors. 

####<a name="4"></a> Eliminate Processor Architecture Mismatch Warning

95 warnings remain. Most of those concern the everlasting processor architecture mismatch, e.g.:

- Warning: There was a mismatch between the processor architecture of the project being built "MSIL" and the processor architecture of the reference "RevitAPIUI", "AMD64". This mismatch may cause runtime failures. Please consider changing the targeted processor architecture of your project through the Configuration Manager so as to align the processor architectures between your project and references, or take a dependency on references with a processor architecture that matches the targeted processor architecture of your project.

I mentioned this warning when it appeared and have been fixing it in every release ever since:

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/06/processor-architecture-mismatch-warning.html">Processor Architecture Mismatch Warning and Key Hook</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html">Recursively Disable Architecture Mismatch Warning</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/09/architecture-mismatch-warning-disabler-update.html">Architecture Mismatch Warning Disabler Update</a></li>
</ul>

I implemented the [DisableMismatchWarning](https://github.com/jeremytammik/DisableMismatchWarning) utility to help me do so, and make use of that now as well:

After running that over all samples,
only [6 warnings](zip/revit_2023_sdk_samples_errors_warnings_1.txt) remain.

####<a name="5"></a> Missing XML Comments

Two of the remaining six warnings are easy to fix, so let's do so:

- Warning CS1591: Missing XML comment for publicly visible type or member 'ExportPDFData.Combine' in ImportExport,
  *...\ImportExport\CS\Export\ExportPDFData.cs* line 41
- Warning CS1591: Missing XML comment for publicly visible type or member 'ExportPDFOptionsForm.ExportPDFOptionsForm(ExportPDFData)' in ImportExport,
  *...\ImportExport\CS\Export\ExportPDFOptionsForm.cs*, line 45
  
I simply added placeholder XML documentation to both methods.

####<a name="6"></a> Targets and Rule Sets

The four remaining warnings seem unimportant and can be ignored:

- *C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\Microsoft.Csharp.targets* cannot be imported again.
  It was already imported at *Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\VSProps\SDKSamples.targets (29,3)*.
  This is most likely a build authoring error.
  This subsequent import will be ignored.
  SampleCommandsSteelElements   
- Warning  Could not find rule set file "Migrated rules for FrameBuilder.ruleset". FrameBuilder   
- Warning  Could not find rule set file "GeometryCreation_BooleanOperation.ruleset". GeometryCreation_BooleanOperation   
- Warning  Could not find rule set file "ProximityDetection_WallJoinControl.ruleset". ProximityDetection_WallJoinControl   

####<a name="7"></a> Set Up RvtSamples

I added my local system path *Y:/a/src/rvt/RevitSdkSamples/SDK/Samples/* in both `RvtSamples.addin` and `RvtSamples.txt`.

Some add-in file paths are not found, so I set the debugging flag in `RvtSamples` `Application.cs`:

<pre class="code">
  bool testClassName = true; // jeremy
</pre>

The culprits are the usual suspects, all of them VB.NET samples, but strangely enough not all VB.NET samples,
cf. [revit_2023_sdk_samples_errors_warnings_2.txt](zip/revit_2023_sdk_samples_errors_warnings_2.txt).

####<a name="7.2"></a> DatumsModification Commands

RvtSamples.txt erroneously listed an external command named `Command` for the `DatumsModification` SDK sample.
In fact, it defines no such command; instead, it implements three others:

- DatumAlignment
- DatumPropagation
- DatumStyleModification

I updated RvtSamples.txt accordingly.

####<a name="7.3"></a> ContextualAnalyticalModel Commands

ContextualAnalyticalModel defines 15 external commands:

- AddAssociation
- AnalyticalNodeConnStatus
- CreateAnalyticalPanel
- CreateAnalyticalMember
- FlipAnalyticalMember
- MemberForcesAnalyticalMember
- ModifyPanelContour
- MoveAnalyticalMemberUsingElementTransformUtils
- MoveAnalyticalMemberUsingSetCurve
- MoveAnalyticalNodeUsingElementTransformUtils
- MoveAnalyticalPanelUsingElementTransformUtils
- MoveAnalyticalPanelUsingSketchEditScope
- ReleaseConditionsAnalyticalMember
- RemoveAssociation
- SetOuterContourForPanels

RvtSamples.txt erroneously lists three others: AddRelation, UpdateRelation and BreakRelation.

I replaced them by AddAssociation and RemoveAssociation.

####<a name="7.4"></a> CivilAlignments Commands

The CivilAlignments sample defines two commands and tries to create menu entries named "Infrastructure Alignments" or both of them.

This causes Revit to throw an exception.

I fixed that by naming them 'Infrastructure Alignments CreateAlignmentStationLabelsCmd' and 'Infrastructure Alignments ShowPropertiesCmd', respectively.

####<a name="8"></a> Conclusion

I finished off by resetting the `testClassName` debug switch and creating the final
[RevitSdkSamples release 2023.0.0.3](https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2023.0.0.3).

<center>
<img src="img/RvtSamples2023.png" alt="RvtSamples 2023" title="RvtSamples 2023" width="800"/> <!-- 2216 -->
</center>

