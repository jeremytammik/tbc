<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---



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


