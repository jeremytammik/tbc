<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

RevitLookup Using Reflection for Cross-Version Compatibility #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Today, I present a large and drastic contribution to RevitLookup from Andy @awmcc90 McCloskey of RevDev Studios that will help significantly in supporting both past and future releases of Revit
&ndash; Drastic changes making use of object inspection via <code>Reflection</code>
&ndash; Cross version compatibility
&ndash; Removal of events and unused functionality
&ndash; Commit summary
&ndash; <code>Snoop</code> / <code>CollectorExts</code>
&ndash; <code>Utils</code>
&ndash; <code>Tests</code>
&ndash; File Changes
&ndash; Download...

-->

### RevitLookup Reflection Cross-Version Compatibility

Today, I present a large and drastic contribution 
to [RevitLookup](https://github.com/jeremytammik/RevitLookup) from
Andy [@awmcc90](https://github.com/awmcc90) McCloskey
of [RevDev Studios](https://twitter.com/revdevstudios) that will help significantly in supporting both past and future releases of Revit:

- [Drastic changes making use of object inspection via `Reflection`](#2)
- [Cross version compatibility](#3)
- [Removal of events and unused functionality](#4)
- [Commit summary](#5)
    - [`Snoop` / `CollectorExts`](#6)
    - [`Utils`](#7)
    - [`Tests`](#8)
    - [File Changes](#9)
- [Download](#10)
- [RevDev Studios](#11)
- [Addendum &ndash; never catch all exceptions](#12)


#### <a name="2"></a>Drastic Changes Making Use of Object Inspection via Reflection

This weekend, 
Andy [@awmcc90](https://github.com/awmcc90) McCloskey
of [RevDev Studios](https://twitter.com/revdevstudios) submitted
[RevitLookup pull request #22 &ndash; Object inspection via `Reflection`](https://github.com/jeremytammik/RevitLookup/pull/22).

The new functionality it incorporates will help significantly in supporting both past and future releases of Revit:

It includes drastic changes to the library to load object information directly from the assembly using .NET `Reflection`.

This makes more information available and the product more flexible.

Above all, the use of `Reflection` makes RevitLookup cross compatible with different versions of the Revit API.

In Andy's own words:

> I think many people will greatly benefit from it. 

> In the past I have made small modifications to RevitLookup on GitHub. For the past few months I have been making some rather serious modifications to the source code in order to more fully explore every object that the API has to offer. This has primarily been to supplement my own add-in creation but also to get a better understanding of the underlying types of the API. Furthermore, I wanted to make RevitLookup work for any version of Revit, meaning I have been doing the majority of my changes via reflection.

> I would like to contribute these changes to the project. Given how drastic the changes were, I wanted to give you a copy and see how you wanted to handle this.

Please refer to the [commit summary](#4) below for more information on the changes made to individual modules.

Here is a screen snapshot snooping the properties of a roof using the `Reflection` enhanced version of RevitLookup:

<center>
<img src="img/revitlookup_reflect.png" alt="RevitLookup using Reflection" width="777"/>
</center>


#### <a name="3"></a>Cross Version Compatibility

Andy's original version was compiled using the Revit 2016 API assemblies, yet the use of Reflection enables it to explore methods and properties added in Revit 2017 as well:

> The version is based around 2016 Revit API libraries but that was just for compatibility. I don't particularly like handling separate projects so I resolve to wrap older commands and check to make sure they exist with whatever version of the API you are using and then choose the correct one. The version you are using will work as is for 2017 (from the most recent source code on github) and if you change the references to 2017 you can build and compile it fine as well.

> Most of the additions from other users are completely obsolete at this point. Given everything available to the API object is now loaded directly from the assembly, we shouldn't be missing any information. 

> One feature that this does change is it simply gives you the name of the method or property directly from the library, whereas you were originally naming these specifically. I think I prefer this method because most of us using RevitLookup are doing it in order to do something else with the API, and it helps to have the exact name of the property and method when we go to start programming.

> That handles most of the major changes. There are definitely some minor ones that I am missing but I think this is a good start.


#### <a name="4"></a>Removal of Events and Unused Functionality

Functionality not required for the fundamental object inspection task has been removed:

> I took out a several of the main commands, such as events, etc., because I was only interested in the element information. It shouldn't be too hard to piece those commands back in but wanted to make sure you knew. I was focused on the object inspection only.


#### <a name="5"></a>Commit Summary

For the sake of completeness, here is the detailed commit summary of
Andy's [RevitLookup pull request #22 &ndash; Object inspection via `Reflection`](https://github.com/jeremytammik/RevitLookup/pull/22):

#### <a name="6"></a>Snoop / CollectorExts

Removed all modules spare two, the non abstract one which is the main worker of loading information via reflection. In general, I felt like it was a lot of code which needed to be specifically typed out in order to get parameters and method values that were already available. The new worker is very simple: when the collect event is called, we pass the object to the worker, the worker determines which types the object is associated with and places those in a list. Then we loop through each type and collect properties and methods that meet certain requirements, for each type:

- Properties have to have a get method and they can't require any parameters to be passed in.
- Methods cannot require any parameters to be passed in and they also can't have a void return type.

We avoid making any changes to the document by invoking the methods in a try statement, and given we are outside of a transaction we catch any error that it would throw due to that. I did a simple (and I mean simple) check of what parameters we have already seen so that we don't continue to log duplicate information in the snoop dialog. I considered leaving all the properties and methods in for every type but it made the list of information very long and I wasn't sure how much I benefited from showing all that information.

Certain specific properties and methods that I wanted, such as element geometry and boundary segments, I had to add on a case by case basis but even this is much more efficient because we still don't require separate methods for each type. We can continue to update the list of specific properties and methods that have parameter inputs as an ongoing work.

#### <a name="7"></a>Utils

I thinned out the Utils class to remove anything that we no longer needed. The changes here were minor.

#### <a name="8"></a>Tests

I had to remove the vast majority of the tests given they were no longer required and more importantly the methods that they called no longer existed.

#### <a name="9"></a>File Changes

Here is the list of files modified (M), replaced (R) and deleted (D).

As you can see, quite a number have been completely removed, significantly simplifying the project with a simultaneous enhancement of functionality:

- M CS/App.cs (1)
- R CS/ColumnSorter.cs (240)
- D CS/Graphics/GraphicsStreamRevit.cs (126)
- M CS/RevitLookup.csproj (175)
- M CS/Snoop/CollectorExts/CollectorExt.cs (2)
- D CS/Snoop/CollectorExts/CollectorExtApp.cs (258)
- D CS/Snoop/CollectorExts/CollectorExtAreas.cs (95)
- D CS/Snoop/CollectorExts/CollectorExtCreation.cs (113)
- D CS/Snoop/CollectorExts/CollectorExtDoc.cs (572)
- D CS/Snoop/CollectorExts/CollectorExtEditor.cs (103)
- M CS/Snoop/CollectorExts/CollectorExtElement.cs (3339)
- D CS/Snoop/CollectorExts/CollectorExtGeom.cs (1084)
- D CS/Snoop/CollectorExts/CollectorExtMEP.cs (301)
- D CS/Snoop/CollectorExts/CollectorExtMisc.cs (1140)
- D CS/Snoop/CollectorExts/CollectorExtParams.cs (304)
- D CS/Snoop/CollectorExts/CollectorExtRooms.cs (93)
- D CS/Snoop/CollectorExts/CollectorExtSite.cs (127)
- D CS/Snoop/CollectorExts/CollectorExtStructural.cs (461)
- D CS/Snoop/CollectorExts/CollectorExtSymbol.cs (1150)
- M CS/Snoop/Collectors/Collector.cs (28)
- M CS/Snoop/Collectors/CollectorEventArgs.cs (10)
- M CS/Snoop/Collectors/CollectorObj.cs (44)
- M CS/Snoop/Data/ClassSeparator.cs (2)
- A CS/Snoop/Data/MemberSeparator.cs (44)
- M CS/Snoop/Utils.cs (283)
- D CS/Test/EStorageSchemaDefinitions/EStorageBundle1.cs (76)
- D CS/Test/EStorageSchemaDefinitions/EStorageBundle2.cs (80)
- D CS/Test/ExIm/BrowseCategory.Designer.cs (73)
- D CS/Test/ExIm/BrowseCategory.cs (100)
- D CS/Test/ExIm/BrowseCategory.resx (120)
- D CS/Test/ExIm/ImportErrorLogger.cs (94)
- D CS/Test/ExIm/Importer.cs (394)
- D CS/Test/ExIm/XmlMapBuilder.cs (33)
- D CS/Test/ExIm/XmlSchemaBuilder.cs (138)
- D CS/Test/Forms/Elements.Designer.cs (79)
- D CS/Test/Forms/Elements.cs (132)
- D CS/Test/Forms/Elements.resx (120)
- D CS/Test/Forms/Levels.Designer.cs (102)
- D CS/Test/Forms/Levels.cs (106)
- D CS/Test/Forms/Levels.resx (120)
- D CS/Test/SDKSamples/AnalyticalSupportData/Info.cs (185)
- D CS/Test/SDKSamples/AnalyticalSupportData/InfoForm.cs (64)
- D CS/Test/SDKSamples/AnalyticalSupportData/InfoForm.designer.cs (153)
- D CS/Test/SDKSamples/AnalyticalSupportData/InfoForm.resx (132)
- D CS/Test/SDKSamples/CreateSheet/AllViewsForm.cs (165)
- D CS/Test/SDKSamples/CreateSheet/AllViewsForm.designer.cs (166)
- D CS/Test/SDKSamples/CreateSheet/AllViewsForm.resx (120)
- D CS/Test/SDKSamples/CreateSheet/Views.cs (276)
- D CS/Test/SDKSamples/ExportToExcel/Exporter.cs (31)
- D CS/Test/SDKSamples/FireRating/ImporterExporter.cs (32)
- D CS/Test/SDKSamples/FireRating/SharedParam.cs (132)
- D CS/Test/SDKSamples/LevelProperties/LevelsCommand.cs (164)
- D CS/Test/SDKSamples/LevelProperties/LevelsDataSource.cs (87)
- D CS/Test/SDKSamples/LevelProperties/LevelsForm.Designer.cs (132)
- D CS/Test/SDKSamples/LevelProperties/LevelsForm.cs (292)
- D CS/Test/SDKSamples/LevelProperties/LevelsForm.resx (123)
- D CS/Test/SDKSamples/SDKTestFuncs.cs (224)
- D CS/Test/SDKSamples/SharedParams/Create.cs (161)
- D CS/Test/SDKSamples/StructuralSample/StructSample.cs (313)
- D CS/Test/SDKSamples/TypeSelector/TypeSelectorForm.Designer.cs (110)
- D CS/Test/SDKSamples/TypeSelector/TypeSelectorForm.cs (258)
- D CS/Test/SDKSamples/TypeSelector/TypeSelectorForm.resx (120)
- D CS/Test/TestApplication.cs (144)
- D CS/Test/TestDocument.cs (387)
- D CS/Test/TestEStorage.cs (183)
- D CS/Test/TestElements.cs (1892)
- D CS/Test/TestFramework/RvtMgdDbgTestForm.cs (769)
- D CS/Test/TestFramework/RvtMgdDbgTestForm.resx (147)
- D CS/Test/TestFramework/RvtMgdDbgTestFuncInfo.cs (136)
- D CS/Test/TestFramework/RvtMgdDbgTestFuncs.cs (67)
- D CS/Test/TestGeometry.cs (122)
- D CS/Test/TestGraphicsStream.cs (147)
- D CS/Test/TestImportExport.cs (255)
- D CS/Test/TestUi.cs (92)
- D CS/Test/Tower.cs (301)
- M CS/TestCmds.cs (525)
- D CS/Utils/Convert.cs (55)
- D CS/Utils/Dwg.cs (23)
- D CS/Utils/Elements.cs (500)
- D CS/Utils/FamilyUtil.cs (62)
- D CS/Utils/Geometry.cs (214)
- D CS/Utils/ParamUtil.cs (103)
- D CS/Utils/Selection.cs (129)
- D CS/Utils/StrElementIdPair.cs (67)
- D CS/Utils/UserInput.cs (84)
- D CS/Utils/View.cs (154)



#### <a name="10"></a>Download

The new functionality is provided
in [RevitLookup release 2017.0.0.14](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.14) and
later versions.

If you would like to access any part of the functionality that was considered unimportant and removed in this release, please feel free to grab it
from [release 2017.0.0.13](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.13).

I am also perfectly happy to restore code that was removed and that you would like preserved. Simply create a pull request for that, explain your need and motivation, and I will gladly merge it back again.


#### <a name="11"></a>RevDev Studios

[RevDev Studios](https://twitter.com/revdevstudios) provides custom add-in development services for Revit.

I guess I really ought to add a comment about them to the post
on [finding a development partner](http://thebuildingcoder.typepad.com/blog/2011/12/finding-a-development-partner.html),
or update that post in general.


#### <a name="12"></a>Addendum &ndash; Never Catch All Exceptions


Compilation
of [RevitLookup release 2017.0.0.14](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.14) discussed
above causes two warnings `CS0168` in `CollectorExtElement.cs` saying, "The variable 'ex' is declared but never used" in exception handlers like this:

<pre class="code">
  <span style="color:blue;">catch</span>(&nbsp;<span style="color:#2b91af;">Exception</span>&nbsp;ex&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:green;">//&nbsp;Probably&nbsp;is&nbsp;that&nbsp;this&nbsp;specific&nbsp;element
  &nbsp;&nbsp;// doesn&#39;t&nbsp;have&nbsp;the&nbsp;property&nbsp;-&nbsp;ignore</span>
  &nbsp;&nbsp;<span style="color:green;">//data.Add(new&nbsp;Snoop.Data.Exception(pi.Name,&nbsp;ex));</span>
  }
</pre>

However, you should [never, ever, catch all exceptions](http://thebuildingcoder.typepad.com/blog/2016/04/how-to-distinguish-redundant-rooms.html#3).

Andy very kindly fixed that as well,
submitting [pull request #23 &ndash; catching proper exceptions for reflection invocation](https://github.com/jeremytammik/RevitLookup/pull/23).

Now the exception handler looks like this instead, catching specific exceptions and leaving the really unexpected, exceptional exceptions untouched:

<pre class="code">
  <span style="color:blue;">catch</span>&nbsp;(<span style="color:#2b91af;">TargetException</span>&nbsp;ex)
  {
&nbsp;&nbsp;&nbsp;&nbsp;data.Add(<span style="color:blue;">new</span>&nbsp;Snoop.Data.<span style="color:#2b91af;">Exception</span>(pi.Name,&nbsp;ex));
  }
  <span style="color:blue;">catch</span>&nbsp;(<span style="color:#2b91af;">TargetInvocationException</span>&nbsp;ex)
  {
&nbsp;&nbsp;&nbsp;&nbsp;data.Add(<span style="color:blue;">new</span>&nbsp;Snoop.Data.<span style="color:#2b91af;">Exception</span>(pi.Name,&nbsp;ex));
  }
  <span style="color:blue;">catch</span>&nbsp;(<span style="color:#2b91af;">TargetParameterCountException</span>&nbsp;ex)
  {
&nbsp;&nbsp;&nbsp;&nbsp;data.Add(<span style="color:blue;">new</span>&nbsp;Snoop.Data.<span style="color:#2b91af;">Exception</span>(pi.Name,&nbsp;ex));
  }
</pre>

[RevitLookup release 2017.0.0.15](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.15) with
this fix integrated no longer violates this recommendation and compiles with zero warnings.

Thanks again to Andy!
