<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

Compiling the Revit 2021.1 SDK samples with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/rvt2021sdk

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Compile and Install Revit 2021.1 SDK Samples


####<a name="2"></a> Compiling the Revit 2021.1 SDK Samples

A long time ago, compiling the Revit SDK Samples updated for each new major release was major undertaking due to hundreds of compiler errors.

In the last couple of years, happily, it became a piece of cake.

This time around, unfortunately, it was a lot trickier again.

Stupidly enough, all the hassle involved
in [compiling the initial Revit 2021 SDK](https://thebuildingcoder.typepad.com/blog/2020/05/compiling-the-revit-2021-sdk-samples.html) needs to be repeated.

Happily, I documented that in full detail, so all I need to do here is repeat the same steps again.

The source of the problem is pretty basic and not really hard to fix: the references to the Revit API assemblies in each of the 190 sample projects is set up for different developers' specific environments.

All you need to do is to hunt them all down and point them all to the official Revit installation location *C:\Program Files\Autodesk\Revit 2021*.

Hey, development team, can't you please do that for us yourself, before releasing the SDK to the general unsuspecting public?

It would just save a couple of hour's work for every single developer that inadvertently runs into this issue.

Once again, I copied the original state of the Revit 2021.1 SDK to
the [RevitSdkSamples GitHub repository](https://github.com/jeremytammik/RevitSdkSamples) before
analysing the problem and applying the fixes, so you can easily retrace my steps or download the final results of my fixes and save yourself the effort of reinventing this particular wheel.

Here is a list of some of my commits, releases, and error logs:

- [Release 2021.1.0.0](https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2021.1.0.0)
  &ndash; replaced my fixed version of the initial Revit 2021 SDK by the official Revit 2021.1 SDK release
  &ndash; [0 projects succeeded, 191 failed, 8977 errors and 1119 warnings](zip/revit_2021_sdk_samples_errors_warnings_1.txt)

I performed the following steps to locate the API assemblies and fix these errors:

- Fixed `HintPath` in SDKSamples.targets
- Updated RevitAPI and RevitAPIUI `HintPath` settings in individual project files
- Fixed `HintPath` in SDKSamples.VB.targets
- Fixed `HintPath` in SDKSamples.Steel.targets
- Added local reference to Microsoft.Office.Interop.Excel.dll in Massing/PointCurveCreation

That enabled the first successful compilation, still producing a bunch of warnings, though:

- [Release 2021.0.0.1](https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2021.0.0.1)
  &ndash; [190 projects succeeded, 0 errors and 36 warnings](zip/revit_2021_sdk_samples_errors_warnings_5.txt)

Most of the warnings concern the
ever-recurring [architecture mismatch issue](http://thebuildingcoder.typepad.com/blog/2013/06/processor-architecture-mismatch-warning.html)
and can be resolved using
my [DisableMismatchWarning.exe utility](http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html)
implemented back in 2013 and available from
the [DisableMismatchWarning GitHub repository](https://github.com/jeremytammik/DisableMismatchWarning).
I ran it individually on each of the projects causing the warning. The result is

- [Release 2021.0.0.2](https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2021.0.0.2)
  &ndash; [190 projects succeeded, 0 errors and 5 warnings](zip/revit_2021_sdk_samples_errors_warnings_6.txt)

I will leave the remaining five warnings in there for now.

I hope that I can persuade the development team to clean up the SDK before their next release of it to save us having to repeat these steps again next time around.


####<a name="3"></a> Reading the Value of a Parameter

Talking about the Revit SDK samples, let's address another (even more) beginner's question, on reading the value of a parameter, raised in
a [comment](https://thebuildingcoder.typepad.com/blog/2011/09/unofficial-parameters-and-bipchecker.html#comment-4905987898)
on [unofficial parameters and BipChecker](https://thebuildingcoder.typepad.com/blog/2011/09/unofficial-parameters-and-bipchecker.html):

**Question:** I am trying to read the following parameters of a wall: volume and area.

I have been using `getparameter`, `get_parameter` and `lookupparameter`, but the return value is always empty.

Are there any tutorials, examples or links where I may get the answer?

**Answer:** Reading the value of a parameter is a very basic and fundamental task in the Revit API, so it is covered in depth by
the [getting started material](https://thebuildingcoder.typepad.com/blog/about-the-author.html#2).

Please work through that before doing anything else, if you have not done so already.

The main steps are:

- Retrieve the building element
- Determine what parameter you wish to read
- Built-in parameters are easiest; I assume that volume and area are indeed built-in

Exploring the element properties and determining exactly which parameters to use to retrieve the desired information is vastly simplified
by [RevitLookup](https://github.com/jeremytammik/RevitLookup),
an interactive Revit BIM database exploration tool to view and navigate element properties and relationships.

If you have not already installed and started using that, you should do so right away!

Once you have completed those preparations, reading those two parameter values can be accomplished like this in code:

<pre class="code">
  Element e = null; // get the BIM element
  double a = e.get_Parameter( BuiltInParameter.HOST_AREA_COMPUTED );
  double v = e.get_Parameter( BuiltInParameter.HOST_VOLUME_COMPUTED );
</pre>

The age-old article
discussing [compound wall layer volumes](https://thebuildingcoder.typepad.com/blog/2009/02/compound-wall-layer-volumes.html) shows
a practical example of retrieving those two values.

<center>
<img src="img/jeremy_with_face_mask.jpg" alt="Jeremy with a face mask" title="Jeremy with a face mask" width="300"/>
<p style="font-size: 80%; font-style:italic">Be scarier than the virus</p>
</center>

