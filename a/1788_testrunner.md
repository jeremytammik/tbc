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

twitter:

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### TestRunner &ndash; Run Unit Tests in Revit

We have mentioned a couple
of [Revit add-in unit testing frameworks](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.16) in
the past.

Tobias Flöscher of [Geberit](https://www.geberit.com) now shares one that looks very promising indeed in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [`Revit.TestRunner` &ndash; run unit tests in Revit](https://forums.autodesk.com/t5/revit-api-forum/revit-testrunner-run-unittests-in-revit/m-p/9070621), saying:

####<a name="2"></a> Revit.TestRunner &ndash; Run Unit Tests in Revit

Hello Developers,

In 2018, I started to program in the Revit API environment.
It was completely new to me, so I had to get familiar with the API and its peculiarities.
I found The Building Coder blog, which helped me a lot.

We insourced an add-in that was originally developed by an external company.
The whole code lives in an assembly which references the Revit API.
Then I realized, testing is hard.
I refactured the code so that it was as much as possible independent from the Revit API.
This code was testable as I knew it, using tests run
on [Contiguous Integration](https://en.wikipedia.org/wiki/Continuous_integration).
But a lot of code remained in the project referencing the Revit API.

I asked Google what to do, but apparently there is not THE way to solve this problem.

There is some stuff around that seems cool, e.g.,  [RevitTestFramework](https://thebuildingcoder.typepad.com/blog/2018/08/revit-unit-test-framework-improvements.html)),
but I was not happy with it.
Furthermore, all I found uses NUnit 2.6, whereas all my Revit independent code is tested by the NUnit 3.x.
The build server doesn’t like the mix.

As a result, I started to write my own TestRunner using NUnit 3.
It is far from perfect; nevertheless, I would like to share it with the community.

Please have a look at it in 
the [Revit.TestRunner GitHub repository](https://github.com/geberit/Revit.TestRunner).

Features so far:

- Using NUnit 3
- No reference to Revit.TestRunner needed in test assembly
- Support of NUnit attributes SetUp and TearDown
- Injection of API objects in test method
- Works as an addin with GUI
- Installation script for libraries
- Post build event in VS project to place addin file

Feedback welcome!

Very many thanks to Tobias for implementing, documenting and sharing this very important new tool!


