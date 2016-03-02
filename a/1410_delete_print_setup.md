<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 11556999 [Export to DWF / PrintSetup]

- delete print setup:
  http://forums.autodesk.com/t5/revit-api/delete-printsetup-and-viewsheetsettings/m-p/6063449

- reload dll for debugging without restart
  http://stackoverflow.com/questions/33525908/revit-api-load-command-auto-reload

- draw a curve:
  http://forums.autodesk.com/t5/revit-api/draw-curve-in-activeuidocument-document-using-ilist-lt-xyz-gt/td-p/6063446

- distance from family instance to floor or elevation
  http://forums.autodesk.com/t5/revit-api/elevation-of-family-instance-from-floor-or-level-below-it/m-p/6058148

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewAPI #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restAPI
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge
@AutodeskReCap @Adsk3dsMax
#revitAPI #bim #aec #3dwebcoder #adsk #adskdevnetwrk @jimquanci @keanw
#au2015 #rtceur
#eraofconnection
#RMS @researchdigisus
@adskForge #3dwebaccel
#a360
 @github

Revit API, Jeremy Tammik, akn_include

Index, Debug, Curves, Distance, Deleting PrintSetup #revitAPI #3dwebcoder @AutodeskRevit #bim #aec #adsk #adskdevnetwrk

Here is another bunch of issues addressed in
the Revit API discussion forum and
elsewhere in the past day or two
&ndash; The Building Coder blog source text and index
&ndash; Reloading add-in DLL for debugging without restart
&ndash; Drawing curves from a list of points
&ndash; Determining the distance of a family instance to the floor or elevation
&ndash; Deleting a PrintSetup or ViewSheetSetting...

-->

### Index, Debug, Curves, Distance, Deleting PrintSetup

Here is another bunch of issues addressed in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) and
elsewhere in the past day or two:

- [The Building Coder blog source text and index](#2)
- [Reloading add-in DLL for debugging without restart](#3)
- [Drawing curves from a list of points](#4)
- [Determining the distance of a family instance to the floor or elevation](#5)
- [Deleting a PrintSetup or ViewSheetSetting](#6)


#### <a name="2"></a>The Building Coder Blog Source Text and Index

As you may have noticed, I published
[The Building Coder entire source text and complete index on GitHub](http://thebuildingcoder.typepad.com/blog/2016/02/tbc-the-building-coder-source-and-index-on-github.html) last week.

If you look now at the right hand side bar or the navigation bar at the bottom, you will see the two new
entries [index](https://jeremytammik.github.io/tbc/a)
and [source](https://github.com/jeremytammik/tbc) that take you straight there.

<center>
<img src="img/index_finger.jpg" alt="Index finger" width="360">
</center>

I hope you find the complete index and full source text access useful!


#### <a name="3"></a>Reloading Add-in DLL for Debugging Without Restart

We have often dealt here with topics
around [edit and continue, debug without restart and 'live development'](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.49).

This note is just to point out another contribution to that series, a StackOverflow question
on [Revit API load command &ndash; auto reload](http://stackoverflow.com/questions/33525908/revit-api-load-command-auto-reload),
in case you are interested in this too.


#### <a name="4"></a>Drawing Curves from a List of Points

This is a very common need, brought up again by Dirk Neethling in the thread
on [drawing a curve in ActiveUIDocument.Document using IList&lt;XYZ&gt;](http://forums.autodesk.com/t5/revit-api/draw-curve-in-activeuidocument-document-using-ilist-lt-xyz-gt/td-p/6063446):

**Question:** I'm trying to draw a contiguous curve in an ActiveUIDocument.Document, using a List of `XYZ` objects.
Most examples draw a curve in a FamilyDocument, and I could not adapt it for an ActiveUIDocument.Document.
Is it necessary to create a plane for such a curve?

**Answer:** Yes, it is.

You can create a model curve or a detail curve, and both reside on a sketch plane.

If you care about efficiency, you might care to reuse the sketch planes as much as possible.

Note that some existing samples create a new sketch plane for each individual curve, which is not a nice thing to do.

The Building Coder provides a number of samples, e.g.:

- [Model and Detail Curve Colour](http://thebuildingcoder.typepad.com/blog/2010/01/model-and-detail-curve-colour.html)
- [Detail Curve Must Indeed lie in Plane](http://thebuildingcoder.typepad.com/blog/2010/05/detail-curve-must-indeed-lie-in-plane.html)
- [Model Curve Creator](http://thebuildingcoder.typepad.com/blog/2010/05/model-curve-creator.html)
- [Generating a MidCurve Between Two Curve Elements](http://thebuildingcoder.typepad.com/blog/2013/08/generating-a-midcurve-between-two-curve-elements.html)

A `Creator` model curve helper class is also included in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples),
in the module [Creator.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/Creator.cs).

Furthermore, the `CmdDetailCurves` sample command shows how to create detail lines, in the
module [CmdDetailCurves.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdDetailCurves.cs).

The GitHub repository master branch is always up to date, and previous versions of the Revit API are supported by
different [releases](https://github.com/jeremytammik/the_building_coder_samples/releases).

As you should probably know if you are reading this, detail lines can only be created and viewed in a plan view.

Also, it is useful to know that the view graphics and visibility settings enable you to control the model line appearance, e.g. colour and line type.



#### <a name="5"></a>Determining the Distance of a Family Instance to the Floor or Elevation

Here is another common need, to determine
the [distance from family instance to the floor or elevation below](http://forums.autodesk.com/t5/revit-api/elevation-of-family-instance-from-floor-or-level-below-it/m-p/6058148),
raised by Kailash Kute:

**Question:** I want to calculate the elevation (distance) of a selected family instance with respect to the Floor or Level below it.

How to get the floor or level which is below the selected family instance?

How to get the elevation of the instance with respect to that level?

Later:

The help text
on [finding geometry by ray projection](http://help.autodesk.com/view/RVT/2016/ENU/?guid=GUID-B3EE488D-2287-49A2-A772-C7164B84A648)
([Revit 2014 Czech](http://forums.autodesk.com/t5/revit-api/elevation-of-family-instance-from-floor-or-level-below-it/m-p/6057571),
[Revit 2015 English](http://help.autodesk.com/view/RVT/2014/CSY/?guid=GUID-B3EE488D-2287-49A2-A772-C7164B84A648)) provides
part of the answer, but is valid only if there is a Floor below the family instance.

If I only have a Level below it, the ray passing through does not return any distance (proximity).

Now my question gets filtered down to: How to find Intersection with Level?

**Answer:** If all you need is the distance between the family instance and the level, I see a possibility for a MUCH easier approach:

Ask the family instance for its bounding box and determine its bottom Z coordinate `Z1`.

Determine the elevation `Z0` of the level.

The distance you seek is `Z1 - Z0`.

**Response:** A little bit of work around with points got from bounding box and done.

Wow, really a MUCH easier approach.


#### <a name="6"></a>Deleting a PrintSetup or ViewSheetSetting

Eirik Aasved Holst yesterday brought up and solved the question that regularly comes up on how
to [delete PrintSetup and ViewSheetSettings](http://forums.autodesk.com/t5/revit-api/delete-printsetup-and-viewsheetsettings/m-p/6063449):

**Question:** [TL;DR](https://en.wikipedia.org/wiki/TL;DR): Is there a way of deleting a specific PrintSetup and ViewSheetSetting programmatically if I know its name?

Long version:

I'm writing a function that can print a large set of sheets in various paper sizes to separate/combined PDFs.

To be able to apply the PrintSetup, it needs to be set in an "in-session" state and saved using `SaveAs()` (to my knowledge).

But after saving an "in-session"-PrintSetup to a new PrintSetup, I cannot find a way of deleting said new PrintSetup.

After the PrintManager has printed the sheets, I'm stuck with lots of temporary PrintSetups. The same goes for ViewSheetSettings.

<pre class="code">
  try
  {
    pMgr.PrintSetup.Delete();
    pMgr.ViewSheetSetting.Delete();
  }
  catch (Exception ex)
  {
    //Shows 'The <in-session> print setup cannot be deleted'
    TaskDialog.Show("REVIT", ex.Message);
  }
</pre>

So the problem is: I'm not able to apply the PrintSetup and ViewSheetSettings unless I'm using them "in-session" and saving them using SaveAs, and I'm not able to delete the PrintSetup and ViewSheetSettings afterwards.

Has anyone experienced similar issues, or found a way to solve it?

**Answer:** This discussion
on [setting the ViewSheetSetting.InSession.Views property](http://stackoverflow.com/questions/14946217/setting-viewsheetsetting-insession-views-property) makes
one brief mention of deleting a print setting, afaik can tell.

This other one
on [PrinterManager PrintSetup not applying settings](http://forums.autodesk.com/t5/revit-api/printermanager-printsetup-do-not-apply-settings/td-p/3676618) appears
to suggest a similar approach.

**Response:** These links unfortunately do not help much.

It would be nice if it was possible to loop through the saved PrintSetup's, or at least get a PrintSetup if you knew it's name, so that you can delete it.

In the thread
on [PrinterManager PrintSetup not applying settings](http://forums.autodesk.com/t5/revit-api/printermanager-printsetup-do-not-apply-settings/td-p/3676618),
the user aricke mentions:

> Note that once you have done the SaveAs, you can then delete the newly saved PrintSetup.

I cannot seem to get that to work; even the following code raises an exception:

<pre class="code">
  pSetup.SaveAs("tmp");
  pSetup.Delete();
</pre>

**Solution:** I finally managed to create a CleanUp-method that works. If others are interested, here it goes:

<pre class="code">
&nbsp; <span class="blue">private</span> <span class="blue">void</span> CleanUp( <span class="teal">Document</span> doc )
&nbsp; {
&nbsp; &nbsp; <span class="blue">var</span> pMgr = doc.PrintManager;
&nbsp; &nbsp; <span class="blue">using</span>( <span class="blue">var</span> trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; trans.Start( <span class="maroon">&quot;CleanUp&quot;</span> );
&nbsp; &nbsp; &nbsp; CleanUpTemporaryViewSheets( doc, pMgr );
&nbsp; &nbsp; &nbsp; CleanUpTemporaryPrintSettings( doc, pMgr );
&nbsp; &nbsp; &nbsp; trans.Commit();
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="blue">private</span> <span class="blue">void</span> CleanUpTemporaryPrintSettings(
&nbsp; &nbsp; <span class="teal">Document</span> doc, <span class="teal">PrintManager</span> pMgr )
&nbsp; {
&nbsp; &nbsp; <span class="blue">var</span> printSetup = pMgr.PrintSetup;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="blue">var</span> printSettingsToDelete
&nbsp; &nbsp; &nbsp; <span class="blue">in</span> ( <span class="blue">from</span> element
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">in</span> <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">PrintSetting</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ToElements()
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">where</span> element.Name.Contains( _tmpName )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; element.IsValidObject
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">select</span> element <span class="blue">as</span> <span class="teal">PrintSetting</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ToList()
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .Distinct( <span class="blue">new</span> EqualElementId() ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; printSetup.CurrentPrintSetting
&nbsp; &nbsp; &nbsp; &nbsp; = pMgr.PrintSetup.InSession;
&nbsp;
&nbsp; &nbsp; &nbsp; printSetup.CurrentPrintSetting
&nbsp; &nbsp; &nbsp; &nbsp; = printSettingsToDelete <span class="blue">as</span> <span class="teal">PrintSetting</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; pMgr.PrintSetup.Delete();
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="blue">private</span> <span class="blue">void</span> CleanUpTemporaryViewSheets(
&nbsp; &nbsp; <span class="teal">Document</span> doc, <span class="teal">PrintManager</span> pMgr )
&nbsp; {
&nbsp; &nbsp; <span class="blue">var</span> viewSheetSettings = pMgr.ViewSheetSetting;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="blue">var</span> viewSheetSetToDelete
&nbsp; &nbsp; &nbsp; <span class="blue">in</span> ( <span class="blue">from</span> element
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">in</span> <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">ViewSheetSet</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ToElements()
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">where</span> element.Name.Contains( _tmpName )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; element.IsValidObject
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">select</span> element <span class="blue">as</span> <span class="teal">ViewSheetSet</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ToList()
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .Distinct( <span class="blue">new</span> EqualElementId() ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; viewSheetSettings.CurrentViewSheetSet
&nbsp; &nbsp; &nbsp; &nbsp; = pMgr.ViewSheetSetting.InSession;
&nbsp;
&nbsp; &nbsp; &nbsp; viewSheetSettings.CurrentViewSheetSet
&nbsp; &nbsp; &nbsp; &nbsp; = viewSheetSetToDelete <span class="blue">as</span> <span class="teal">ViewSheetSet</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; pMgr.ViewSheetSetting.Delete();
&nbsp; &nbsp; }
&nbsp; }
</pre>

Many thanks to Eirik for discovering and sharing this solution!

