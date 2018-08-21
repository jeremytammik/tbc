<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/revit-test-framework-improvements/m-p/8212702

 in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon

The UpCodes AI team shared some significant Revit Test Framework improvements.
&ndash; Created a NuGet package
&ndash; Added the ability to group tests by the model
&ndash; Added ability to use wildcards for model filenames
&ndash; Clear messaging and indication of failures...

--->

### Revit Unit Test Framework Improvements

Today, let's highlight an exciting and very useful contribution by Mark Vulfson
at [UpCodes](https://up.codes) Engineering and the entire 
[UpCodes AI team](https://up.codes/ai), presented in 
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit Test Framework improvements](https://forums.autodesk.com/t5/revit-api-forum/revit-test-framework-improvements/m-p/8212702).

Many thanks to Mark and the UpCodes team for making this available!

<center>
<img src="img/sigmaxxmax.png" alt="Test load" width="275"/>
</center>

Hello everyone,

We are currently developing a pretty elaborate Revit plugin,
the [UpCodes AI for building code automation](https://up.codes/features/ai):

> UpCodes AI is a design aid for architects and engineers. View code errors in real-time directly in Revit with a “spell check” for compliance.

During the course of this work, we ran into many issues (and surely will run into many more), and this forum has helped us resolve many of them.

We'd like to contribute something back to the community by telling you about the improvements we've made to the awesome
[Dynamo Revit unit test framework](http://thebuildingcoder.typepad.com/blog/2013/10/the-dynamo-revit-unit-test-framework.html),
`RevitTestFramework` or RTF, by the Dynamo team.

It's an invaluable tool if you are developing a Revit add-in, as it allows you to run integration tests and control Revit in an automated fashion.

However, the RTF hasn't had much development done on it in a while (other than making it compatible with Revit 2019) and as such we found a few things lacking; the biggest such shortcoming was a lack of NuGet package making usage of the RTF on a build server/continuous integration server very difficult.

Along the way, we made a few other improvements that I think you will find interesting and useful.

Summary of main changes we've made so far:

- [1. Created a NuGet package](#1) 
- [2. Added the ability to group tests by the model](#2) 
- [3. Added ability to use wildcards for model filenames](#3) 
- [4. Clear messaging and indication of failures](#4)


#### <a name="1"></a> 1. Created a NuGet package

You can download it here:

- [www.nuget.org/packages/revittestframework](https://www.nuget.org/packages/revittestframework)

#### <a name="2"></a> 2. Added the ability to group tests by the model

Opening a new RVT file for each test significantly slows down the execution of the tests. We wanted our Revit tests to run for each pull request our engineers make, so it has to be fast (and reliable).

The `groupByModel` option significantly improves performance if you have multiple tests that operate on the same model.

When using `--groupByModel`, the RTF will order the tests such that all tests that operate on the same model file are executed sequentially without closing and reopening the model. Naturally, it requires the `--continuous` parameter to be effective.

Also, it goes without saying that if your tests leave side effects on a model which may break a subsequent test, this option will not work for you.
Luckily for us, none of our tests have side effects (which, in general, is a good practice).

#### <a name="3"></a> 3. Added ability to use wildcards for model filenames

We have multiple models which we want to run a particular test on.

Today, every time we make a new model we have to copy/paste a unit test with a new model file.

To make this simpler, you can now specify a wildcard for your model file on a test and RTF will enumerate all files in the directory and run the test on each one.

For example:

<pre class="prettyprint">
  [TestFixture]
  public class TestAllModels
  {
    [Test, TestModel(@"C:\Models\test_models_2019\*.rvt")]
    public void SomeTest()
    {
      ...
    }
  }
</pre>

will run `SomeTest` on every RVT file it finds under *C:\Models\test_models_2019*.

One of the ways we use this is for performance testing. All that this test does is measure the speed with which we finish processing a given model.

This way, we can run the test suite on each release and assess whether we have significantly regressed our performance from our previous release.

#### <a name="4"></a> 4. Clear messaging and indication of failures

Engineers don't love writing tests and they hate debugging tests. Especially, when diagnosing what went wrong is very difficult.

We wanted to make sure that an engineer could:

- Look at the log output from a build server and quickly be able to tell what went wrong and where to start debugging.
- Run the test easily with nice UI on their local computer to quickly iterate on a solution to fix the problem.

This includes:

- Clear indication when a model file is not found (prior to that RTF would just silently complete the test suite without errors).
- Automatically expand failing tests in the UI so that errors are super obvious.
- Have a clear summary at the end of a test run for the number of passed/failed/etc tests.
This way, you can go grab a coffee while the tests run and know if there were any errors with a quick glance at the summary.
- We utilize categories (e.g. [Category("Doors")]) for grouping.
But RTF UI didn't show tests without a category; this is now fixed.
- All `Console.PrintLine` messages from the actual unit tests are now sent back to the RTF server so you can see them in a single contiguous log &ndash; yay (this one is my favourite)!
- Test completion information is displayed in the console as soon as the test itself is completed; RTF used to wait till all tests have finished before showing you the status of all individual tests; now each pass/fail is printed as soon as the test is completed.

We also made a bunch of small bug fixes around the stability of the RTF.

There is still work to be done on the framework, but we hope you find these changes useful.

We are working with the Dynamo team to bring all these changes back to the main branch of RTF, but, for now, you are welcome to contribute with us in our [forked RevitTestFramework repo](https://github.com/upcodes/RevitTestFramework).

Mark &ndash; Engineering at [UpCodes](https://up.codes)

p.s. [we are hiring](https://up.codes/careers) &ndash; if you love building Revit add-ins please reach out!

