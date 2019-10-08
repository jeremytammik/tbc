<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- Revit.TestRunner - Run UnitTests in Revit
  https://forums.autodesk.com/t5/revit-api-forum/revit-testrunner-run-unittests-in-revit/m-p/9070621
  https://github.com/geberit/Revit.TestRunner

- Confusing assembly location when use one assembly for two Revit-Addin projects
  https://forums.autodesk.com/t5/revit-api-forum/confusing-assembly-location-when-use-one-assembly-for-two-revit/m-p/9070282

- importing PDFs made easy
  https://archi-lab.net/importing-pdfs-made-easy

twitter:

TestRunner for add-in unit testing, support assembly locations and importing PDF files in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/nunittestrunner

A new Revit add-in unit testing framework, a short note on support assembly locations, and an article on importing PDF files
&ndash; <code>Revit.TestRunner</code> runs unit tests in Revit
&ndash; Getting started with <code>TestRunner</code>
&ndash; Unconfusing support assemblies
&ndash; Importing PDFs made easy...

linkedin:

TestRunner for add-in unit testing, support assembly locations and importing PDF files in the #RevitAPI

http://bit.ly/nunittestrunner

A new Revit add-in unit testing framework, a short note on support assembly locations, and an article on importing PDF files:

- Revit.TestRunner runs unit tests in Revit
- Getting started with TestRunner
- Unconfusing support assemblies
- Importing PDFs made easy...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### TestRunner &ndash; Run Unit Tests in Revit

A new Revit add-in unit testing framework, a short note on support assembly locations, and an article on importing PDF files:

- [`Revit.TestRunner` &ndash; run unit tests in Revit](#2)
- [Getting started with `TestRunner`](#3)
- [Unconfusing support assemblies](#4)
- [Importing PDFs made easy](#5)

####<a name="2"></a> Revit.TestRunner &ndash; Run Unit Tests in Revit

We mentioned several
different [Revit add-in unit testing frameworks](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.16) in
the past.

Tobias Flöscher of [Geberit](https://www.geberit.com) now
shares a new implementation based on [NUnit 3](https://nunit.org) that looks very promising indeed,
in the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [`Revit.TestRunner` &ndash; run unit tests in Revit](https://forums.autodesk.com/t5/revit-api-forum/revit-testrunner-run-unittests-in-revit/m-p/9070621),
saying:

Hello Developers,

In 2018, I started to program in the Revit API environment.
It was completely new to me, so I had to get familiar with the API and its peculiarities.
I found The Building Coder blog, which helped me a lot.

We insourced an add-in that was originally developed by an external company.
The whole code lives in an assembly which references the Revit API.
Then I realized, testing is hard.
I refactored the code so that it was as much as possible independent from the Revit API.
This code was testable as I knew it, using tests run
on [continuous integration](https://en.wikipedia.org/wiki/Continuous_integration).
But a lot of code remained in the project referencing the Revit API.

I asked Google what to do, but apparently there is not THE way to solve this problem.

There is some stuff around that seems cool, e.g.,  [RevitTestFramework](https://thebuildingcoder.typepad.com/blog/2018/08/revit-unit-test-framework-improvements.html),
but I was not happy with that.
Furthermore, all I found uses NUnit 2.6, whereas all my Revit independent code is tested by the NUnit 3.x.
The build server doesn't like the mix.

As a result, I started to write my own TestRunner using NUnit 3.
It is far from perfect; nevertheless, I would like to share it with the community.

Please have a look at it in 
the [Revit.TestRunner GitHub repository](https://github.com/geberit/Revit.TestRunner).

tf_testrunner_start.png

Features so far:

- Using NUnit 3
- No reference to Revit.TestRunner needed in test assembly
- Support of NUnit attributes SetUp and TearDown
- Injection of API objects in test method
- Works as an add-in with GUI
- Installation script for libraries
- Post build event in VS project to place add-in manifest

Feedback welcome!

####<a name="3"></a> Getting Started with TestRunner

Get the code from GitHub and compile it.
The *Revit.TestRunner.addin* file will be automatically placed in the *ProgramData* add-in folder of the selected Revit version,

It is also possible to download the precompiled binaries.
Start the *InstallAddin v20xx.cmd* of your favourite Revit version and run Revit. 

The add-in hooks into the Revit 'Add-Ins' ribbon. 

<center>
<img src="img/tf_testrunner_start.png" alt="TestRunner ribbon tab" width="347">
</center>

By pressing the button, a dialogue will appear.
By choosing your testing assembly, the view will show all your tests:

<center>
<img src="img/tf_testrunner_ui.png" alt="TestRunner user interface" width="802">
</center>

Select the node you want to test and press the 'Run'. All tests below the selected node will be executed.

<center>
<img src="img/tf_testrunner_ui_executed.png" alt="TestRunner results" width="802">
</center>

Very many thanks to Tobias for implementing, documenting and sharing this very important new tool!

####<a name="4"></a> Unconfusing Support Assemblies

A quick note on 
a [confusing assembly location when using one assembly for two Revit add-in projects](https://forums.autodesk.com/t5/revit-api-forum/confusing-assembly-location-when-use-one-assembly-for-two-revit/m-p/9070282):

**Question:** I created a support library project implement custom classes and functions and then used this library reference in two Revit add-in projects.

I want to install each project separately, so each project should contain their own library assembly in their setup folder.
In this way, I can easily manage every single project without caring about others.

I use the method `Assembly.GetExecutingAssembly` to check where the library.dll is referring to.
When I first run the project 1, I get the correct assembly path &ndash; the project 1 folder.
Then, I run project 2 and get the unexpected result: the path is in the project 1 folder again.
This should return project 2 folder as my expectation.
A similar problem occurs if I run the project 2 first.

**Answer:** If you load an assembly `A` into the .NET framework from directory `Da` and then load another version of assembly `A` from a different location `Db`, the .NET framework will determine that `A` has already been loaded and reuse the existing instance from `Da`, ignoring your request to load a second instance from `Db`. There are ways to work around this, presumably, but I will not even try to dive into that. I suggest that you design your two add-ins so that they do not rely on the location of the assembly `A` that you load in any way. You can easily move your assembly location determination code out of `A` into the main two add-ins instead.


####<a name="5"></a> Importing PDFs Made Easy

Konrad Sobon of [@arch_laboratory](https://twitter.com/arch_laboratory) shared a new Dynamo solution
on [archi+lab](http://archi-lab.net)
for [importing PDFs made easy](https://archi-lab.net/importing-pdfs-made-easy),
using and expanding on the Revit 2020 ability to import PDF files and making it a little bit easier to place multi-page documents.

