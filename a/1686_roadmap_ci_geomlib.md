<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- Sasha Crotty updated the [Revit Public Roadmap &ndash; September 2018](http://blogs.autodesk.com/revit/2018/09/14/revit-public-roadmap-september-2018)

&ndash; Configuring CI to use the RevittestFramework](https://github.com/upcodes/RevitTestFramework/blob/mark/Revit2019/docs/using_with_ci.md)
We recently discussed some [Revit Unit Test Framework Improvements](http://thebuildingcoder.typepad.com/blog/2018/08/revit-unit-test-framework-improvements.html).
Mark Vulfson now added some very informative additional notes on [Configuring CI to use the RevittestFramework](https://github.com/upcodes/RevitTestFramework/blob/mark/Revit2019/docs/using_with_ci.md).
Check them out if you have any ioterest in this topic.

- geometry library in addition to revit
a little [twitter discussion](https://twitter.com/HossZamani/status/1035128771735474179) on geometry library 
with [Hoss Zamani](https://twitter.com/HossZamani)
Q: This is an excerpt of the awesome Building Coder blog by @jeremytammik in 2008. 10 years from this post Revit still doesn't provide a solid Geometry library through its API? Is it only me that finds this absurd?
img/readonlygeometrylibrary.jpg
A: Yes.  :-)
R: Lol. Apparently you're right 🙂 But why is that, that the developers are expected to have their own math library? Why a software that one of its main uses is to create geometry, makes it so hard to create & manipulate geometry through its API?
A: because the main purpose is NOT to create geometry, like a geometry library, freeform, unrestricted. the one and only purpose is to manage a BIM, which is almost exactly the opposite of freeform unrestricted geometry.
therefore, if you want to create geometry yourself, you really will need your own geometry library, and revit will not do very much to help. although its geometry library is gradually also adding some read-write access... previously, it was totally and purely read-only...

Revit public roadmap update, CI for RTF and geometry library limitations in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/roadmapgeomlib

Lots going on, and off to Rome next week for the Forge accelerator.
Meanwhile, here three of the many topics recently discussed
&ndash; Revit Public Roadmap &ndash; September 2018](#2) 
&ndash; Configuring CI to use the RevitTestFramework RTF
&ndash; Revit Geometry library limitations...

-->

### Roadmap, CI for RTF, Geometry Library

Lots going on, and off to Rome next week for the Forge accelerator.

Meanwhile, here are three of the many topics recently discussed:

- [Revit Public Roadmap &ndash; September 2018](#2) 
- [Configuring CI to use the RevitTestFramework RTF](#3) 
- [Revit Geometry library limitations](#4) 


#### <a name="2"></a> Revit Public Roadmap &ndash; September 2018

Sasha Crotty updated the [Revit Public Roadmap &ndash; September 2018](http://blogs.autodesk.com/revit/2018/09/14/revit-public-roadmap-september-2018).

Check it out to see where Revit is heading, and get some idea about why and how.

To help drive development, please submit your wishes to the [Revit Idea Station](https://forums.autodesk.com/t5/revit-ideas/idb-p/302) at [www.autodesk.com/revitideas](http://www.autodesk.com/revitideas).

There is a tag for [API related wishes](https://forums.autodesk.com/t5/revit-ideas/idb-p/302/label-name/api).


#### <a name="3"></a> Configuring CI to use the RevitTestFramework RTF

We recently discussed
some [Revit Unit Test Framework Improvements](http://thebuildingcoder.typepad.com/blog/2018/08/revit-unit-test-framework-improvements.html).

Mark Vulfson now added additional notes to that,
on [configuring CI to use the RevitTestFramework RTF](https://github.com/upcodes/RevitTestFramework/blob/mark/Revit2019/docs/using_with_ci.md).

Check them out if you have any interest in this specific topic, or even
just [continuous integration](https://en.wikipedia.org/wiki/Continuous_integration) in general.


#### <a name="4"></a> Revit Geometry Library Limitations

People often ask why the Revit geometry library is not as full-fledged as some others, e.g., AutoCAD ObjectARX `AcGe`.

Here are some thought of mine on that subject from 
a [twitter discussion on geometry library](https://twitter.com/HossZamani/status/1035128771735474179) 
with [Hoss Zamani](https://twitter.com/HossZamani):

**Question:** This is an excerpt of the awesome Building Coder blog
by [@jeremytammik](https://twitter.com/jeremytammik) in 2008.

<center>
<img src="img/readonlygeomliblimitation2008.jpg" alt="Read-only geometry library limitation in 2008" width="400">
</center>

Ten years later, Revit still doesn't provide a solid geometry library through its API? Is it only me that finds this absurd?

**Answer:** Yes.  :-)

**Response:** Lol. Apparently you're right &nbsp; :-)

But why is it that developers are expected to have their own math library?

Why does a software, one of whose main uses is to create geometry, make it so hard to create and manipulate geometry through its API?

**Answer:** Because the main purpose of Revit is NOT to create geometry, like a geometry library, freeform, unrestricted.

Its one and only purpose is to manage a BIM, which is almost exactly the opposite of freeform unrestricted geometry.

Therefore, if you want to create geometry yourself, you really will need your own geometry library, and Revit will not do very much to help.

Although, on the other hand, its geometry library is gradually also adding some read-write access.

Previously, it was totally and purely read-only.

Many thanks to Hoss for raising this pertinent question!
