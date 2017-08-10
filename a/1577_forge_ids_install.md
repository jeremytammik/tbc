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

- Forge Viewer: Unique IDs for clickable objects
  https://stackoverflow.com/questions/45577971/forge-viewer-unique-ids-for-clickable-objects
  https://github.com/Autodesk-Forge/bim360appstore-model.derivative-nodejs-xls.exporter
  I mentioned http://thebuildingcoder.typepad.com/blog/2017/02/rvt-properties-to-xls-and-forge-accelerators.html
  Augusto Goncalves implemented the viewer-javascript-extract.spreadsheet Forge sample to read all the properties on all BIM elements in an RVT file and export them to an XLSX spreadsheet.
  https://forge.autodesk.com/blog/create-spreadsheet-excel-client-translated-revit-files

- revitlookup installation
  email [Revit2017 Look UP - How to install?] 

- updated wizard installation

Revit versus Forge, Ids and Add-In Installation #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/forgervtidsinstall

Forge versus Revit programming
&ndash; Access Revit BIM data and element ids from BIM360
&ndash; Unique IDs for Forge viewer elements
&ndash; Edit and export Revit properties in Forge
&ndash; Upcoming Forge accelerators
&ndash; Updated Visual Studio Revit add-in wizard installation
&ndash; Question on RevitLookup installation
&ndash; RevitLookup cannot snoop everything...

--->

### Revit versus Forge, Ids and Add-In Installation

Still getting back into normal work flow after some time off, I have a bunch of stuff to share:

- [Forge versus Revit programming](#2)
- [Access Revit BIM data and element ids from BIM360](#3)
- [Unique IDs for Forge viewer elements](#4)
- [Edit and export Revit properties in Forge](#5)
- [Upcoming Forge accelerators](#6)
- [Updated Visual Studio Revit add-in wizard installation](#7)
- [Question on RevitLookup installation](#8)
- [RevitLookup cannot snoop everything](#9)


####<a name="2"></a>Forge versus Revit Programming

Before designing or implementing a Revit add-in, it is well worth pondering the status, use and cost of Revit in depth.

Who is supposed to make use of your add-in?

Who is supposed to install &ndash; and pay for &ndash; Revit?

Revit is a very specialised, high-level and costly BIM design tool.

I would expect a limited number of people to be directly involved in creative hard-core BIM work.

Many add-ins can add immense value to the BIM and construction process with no need for such a tight integration with Revit.

Many add specific data items or perform read-only analysis and reporting tasks.

For all such tasks, it may be well worthwhile thinking of shifting your app development platform to a much more accessible &ndash; and cheaper &ndash; environment, e.g., [Forge](https://autodesk-forge.github.io).

Two of my recent samples demonstrate the implementation of equivalent functionality using Revit and its API versus Forge and JavaScript:

- [RvtFader](https://github.com/jeremytammik/RvtFader) &ndash; C# .NET Revit add-in to calculate and display signal attenuation using AVF
- [ForgeFader](https://github.com/jeremytammik/forgefader) &ndash; Forge viewer extension to calculate and display signal attenuation, cf.
the [online demo](https://forge-rcdb.autodesk.io/configurator?id=59041f250007f5c0eef482f2)

Interestingly enough, moving your app development out of the specialised .NET Revit API into a much more generic JavaScript based development platform makes development easier, faster, enables much better integration with other components, and provides access to a  larger number of existing libraries for almost any conceivable task.

Furthermore, if you really do require Revit specific functionality, e.g., you wish to modify the BIM and write `RVT` files, and want to provide your app to a large or unlimited number of users in a flexible fashion, not necessarily bound to a full-fledged Revit desktop installation, be aware that we are continuing to work hard on a Forge design automation API
to [provide all you need online, aka Revit I/O](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28b).


####<a name="3"></a>Access Revit BIM Data and Element Ids from BIM360

Continuing in this vein, are you interested in easy and ubiquitous access to your Revit model properties?

Well, you can have that right now, even without any Revit installation whatsoever.

The Forge [Model Derivative API](https://developer.autodesk.com/en/docs/model-derivative/v2/overview/) extracts
a streamlined copy of all geometry, BIM element hierarchy and properties from over fifty file formats, and Revit `RVT` files are just one of them.

As [mentioned](http://thebuildingcoder.typepad.com/blog/2017/02/rvt-properties-to-xls-and-forge-accelerators.html) a
while ago, [Augusto Goncalves](https://twitter.com/search?q=augusto%20goncalves) implemented
the `viewer-javascript-extract.spreadsheet` Forge sample to read all the properties on all BIM elements in an `RVT` file and export them to an `XLSX` spreadsheet.

- [Test run it yourself](https://autodesk-forge.github.io/viewer-javascript-extract.spreadsheet)
- [Source code on GitHub](https://github.com/Autodesk-Forge/viewer-javascript-extract.spreadsheet)
- [Blog post announcement](https://forge.autodesk.com/blog/create-spreadsheet-excel-client-translated-revit-files)

My new colleague
[Michael](https://forge.autodesk.com/author/michael-beale)
[Beale](https://twitter.com/micbeale) now
released another sample that adds the functionality to access a Revit BIM stored in BIM360: the
[BIM360 Revit Export to Excel sample](https://github.com/Autodesk-Forge/bim360appstore-model.derivative-nodejs-xls.exporter)
demonstrates how to access and extract Revit data on your BIM360 account using 3-legged `OAuth` to log in, a simple viewer extension and code to read the Revit meta-data and format it into an Excel spreadsheet file:

- [Live demo](https://bim360xls.autodesk.io)
- [Video](https://youtu.be/800d2xmQl0s)

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/800d2xmQl0s?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

Many thanks to Augusto and Michael for putting these together!


####<a name="4"></a>Unique IDs for Forge Viewer Elements

Augusto uses Michael's sample to answer the StackOverflow question
on [Forge Viewer: unique IDs for clickable objects](https://stackoverflow.com/questions/45577971/forge-viewer-unique-ids-for-clickable-objects):

**Question:** I am experimenting with Revit and the Forge Viewer.
I noticed that Forge has its own unique IDs for everything you can click on in the Revit model, so I used those instead of the Revit identifiers.

For example, the door of my building has the id: "3003".

My question is: where is this data stored and how can I change "3003" into "Door 1"?

**Answer:** The Viewer gives access to three types of IDs when dealing with Revit files:

- `dbId`: this is viewer specific and used to manipulate elements within the viewer, such as for the `.getProperties()` method.
- Revit `ElementID`: exposed as part of the `Name` property in the viewer. When you select something, the Property panel title is in the form of 'Name [12345]'. You can parse this name string and extract the element id.
- Revit `UniqueID`: exposed as the `externalId` property in the `.getProperty()` response.

The
[BIM360 Revit Export to Excel sample](https://github.com/Autodesk-Forge/bim360appstore-model.derivative-nodejs-xls.exporter)
mentioned above exports properties into a spreadsheet, which ought to answer your question.


####<a name="5"></a>Edit and Export Revit Properties in Forge

Philippe Leefsma demonstrates similar functionality in his [Configurator &ndash; Meta Properties](https://forge-rcdb.autodesk.io/configurator?id=59780eec17d671029c53420e) sample.

- Look at
the [online Forge configurator sample](https://forge-rcdb.autodesk.io/configurator).
- Scroll down through the models to *Meta Properties*.
- In the left-hand drop-down menu, select *Office*.
- Click on the *Meta Properties* box.

The office model is displayed, and its properties displayed in a panel on the right-hand side.

You can select any BIM element and see all its properties as well.

The buttons on the right-hand side of the property panel enable search, export to `CSV` and `JSON`, and adding new properties.

Each property can also be deleted.


####<a name="6"></a>Upcoming Forge Accelerators

If you would like to learn more about Forge, don't miss the chance to
[join one of the upcoming accelerators](http://autodeskcloudaccelerator.com/prague-2):

- Bangalore, India &ndash; September 4-8
- Moscow, Russia &ndash; September 25-29
- Lyon, France &ndash; October 23-27
- Munich, Germany &ndash; date TBD

<center>
<img src="img/2017-08_upcoming_accelerators.png" alt="Upcoming Forge accelerators" width="500"/>
</center>


####<a name="7"></a>Updated Visual Studio Revit Add-In Wizard Installation

Back to pure Revit API related topics, 
[Irneb](https://github.com/irneb) raised an issue with the [Visual Studio Revit Add-In Wizards](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20) 
in his [issue #6 &ndash; install script alteration suggestion](https://github.com/jeremytammik/VisualStudioRevitAddinWizard/issues/6), and solved it 
with [pull request #7 &ndash; path to profile folder](https://github.com/jeremytammik/VisualStudioRevitAddinWizard/pull/7).

He says:

In the install.bat file you're getting to the documents folder by building up the path using the username. Unfortunately, this isn't always the case. Also, the C drive is not necessarily where the user's home folder resides.

I suggest changing the 6th line from this:

<pre>
set "D=C:\Users\%USERNAME%\Documents\Visual Studio 2015\Templates\ProjectTemplates"
</pre>

to this:

<pre>
set "D=%userprofile%\Documents\Visual Studio 2015\Templates\ProjectTemplates"
</pre>

That should at least help where the user's home folder isn't in the usual position (or named the same as the username).

What is a bit cumbersome though is if the Documents folder under that has been relocated. Unfortunately, the only place this is available is in registry. Else you can get to it using vbscript or PowerShell.

Here's a way to use PowerShell inside a `BAT` file:

<pre>
set T="%TEMP%\docspath.tmp" powershell -Command "[Environment]::GetFolderPath('MyDocuments') | Out-File '%T%' -Encoding ascii" set /p DOCSPATH=< "%T%" del "%T%"
</pre>

Thereafter the line above can be changed to this instead:

<pre>
set "D=%DOCSPATH%\Visual Studio 2015\Templates\ProjectTemplates"
</pre>

Though it just feels way too complicated for my taste.

I fully agree.

I'm definitely not a Windows shell or installation expert, and I am sure there are much more elegant ways to address this.

Still, being as it is, the updated version is captured
in [release 2018.0.0.1](https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases/tag/2018.0.0.1).

The [difference to the previous version](https://github.com/jeremytammik/VisualStudioRevitAddinWizard/compare/2018.0.0.0...2018.0.0.1) consists
of just two modified lines in `TemplateRevitCs.csproj` and `install.bat`.


####<a name="8"></a>Question on RevitLookup Installation

[RevitLookup](https://github.com/jeremytammik/RevitLookup) is *the* main Revit BIM database exploration tool.

**Question:** I am trying to install RevitLookup on Revit 2017, and having a bit of difficulty figuring out how to go about the installation in Revit.

I downloaded it from GitHub and unpacked the `ZIP` file. Now, according to the instructions, I have to do the following:
 
- Copy the add-in manifest and assembly DLL to the Revit `AddIns` folder.
 
I located the folder with this path: Local disk (C:) &ndash; Program Data &ndash; Autodesk &ndash; Revit &ndash; Addins &ndash; 2017
 
Now, I am wondering how to identify the assembly DLL and whether I need to specify a 'key' in order to sign it.
 
Secondly, I can’t find the add-in manifest anywhere in the repository.

**Answer:** To understand the Revit add-in installation process properly in depth, I suggest that you study the [Revit API getting started material](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2).
 
It explains all you need to know about installing a Revit add-in.
 
So does the [developer guide](http://help.autodesk.com/view/RVT/2018/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42) in
the [Revit help](http://help.autodesk.com/view/RVT/2018/ENU),
specifically in its more succinct section
on [add-in registration](http://help.autodesk.com/view/RVT/2018/ENU/?guid=GUID-4FFDB03E-6936-417C-9772-8FC258A261F7).
 
No, you do not need to sign anything.
 
Normally, you do not need to make any changes at to the add-in manifest.
 
It is provided right there, in the repository
folder [`CS`](https://github.com/jeremytammik/RevitLookup/blob/master/CS):
 
- [RevitLookup.addin](https://github.com/jeremytammik/RevitLookup/blob/master/CS/RevitLookup.addin)
 
Furthermore, very conveniently, YouTube presents
several [videos on installing RevitLookup](https://www.youtube.com/results?search_query=revitlookup).


####<a name="9"></a>RevitLookup Cannot Snoop Everything

Another little note on RevitLookup.

Prompted by the [issue #35 &ndash; RevitLookup doesn't snoop all members](https://github.com/jeremytammik/RevitLookup/issues/35),
I enhanced the documentation with the [caveat &ndash; RevitLookup cannot snoop everything](https://github.com/jeremytammik/RevitLookup#caveat--revitlookup-cannot-snoop-everything):

**Question:** I tried snooping a selected Structural Rebar element in the active view and found not all of the Rebar class members showed up in the Snoop Objects window. One of many members that weren't there: `Rebar.GetFullGeometryForView` method.

Is this the expected behaviour? I was thinking I could get all object members just with  RevitLookup and without the Revit API help file `RevitAPI.chm`.

**Answer:** RevitLookup cannot report **all** properties and methods on **all** elements.

For instance, in the case of GetFullGeometryForView, a view input argument is required. How is RevitLookup supposed to be able to guess what view you are interested in?

For methods requiring dynamic input that cannot be automatically determined, you will have to [make use of more intimate interactive database exploration tools, e.g. RevitPythonShell](http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html).
