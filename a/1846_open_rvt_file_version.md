<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- How to get Revit export image coordinates or reference point?
  https://stackoverflow.com/questions/62004785/how-to-get-revit-export-image-coordinates-or-reference-point

- William Felipe
  Q: I'm trying to compile the SDK Revit 2019.2 Samples, but the buttons are disabled. Do you have any suspicion of what may be happening?
  A: yes: https://thebuildingcoder.typepad.com/blog/2020/05/compiling-the-revit-2021-sdk-samples.html
  You should also read The numerous Building Coder descriptions of all the various problems encountered installing them for previous versions. Please refer to the blog!

- RvtVerFileOpen

- a pretty impressive little app tutorial
  How To Create An Optical Character Reader Using Angular And Azure Computer Vision
  https://www.freecodecamp.org/news/how-to-create-an-optical-character-reader-using-angular-and-azure-computer-vision/
  How to Get Started with React — A Modern Project-based Guide for Beginners (Including Hooks)
  https://www.freecodecamp.org/news/getting-started-with-react-a-modern-project-based-guide-for-beginners-including-hooks-2/

- The most successful developers share more than they take
  https://stackoverflow.blog/2020/05/14/the-most-successful-developers-share-more-than-they-take
  It doesn't just apply to developers, but to BIM experts as well, e.g., [Vasshaug]()
  <li><a href="http://thebuildingcoder.typepad.com/blog/2015/09/sharing-dynamo-and-a-chinese-book.html">Sharing, Dynamo and a Chinese Book</a></li>

twitter:

Automatically determine and open correct RVT file version, exact export image coordinates and scaling and compiling and installing the Revit SDK samples for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/rvtfileveropen

Today, let's address a couple of quick questions and share a useful utility
&ndash; How to determine Revit export image coordinates
&ndash; Problems compiling and installing the Revit SDK samples
&ndash; RvtVerFileOpen utility opens correct RVT file version
&ndash; Angular and react tutorials
&ndash; Give and take...

linkedin:

Automatically determine and open correct RVT file version, exact export image coordinates and scaling and compiling and installing the Revit SDK samples for the #RevitAPI

https://bit.ly/rvtfileveropen

Today, let's address a couple of quick questions and share a useful utility:

- How to determine Revit export image coordinates
- Problems compiling and installing the Revit SDK samples
- RvtVerFileOpen utility opens correct RVT file version
- Angular and react tutorials
- Give and take...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Automatically Open Correct RVT File Version

Today, let's address a couple of quick questions and share a useful utility:

- [How to determine Revit export image coordinates](#2)
- [Problems compiling and installing the Revit SDK samples](#3)
- [RvtVerFileOpen &ndash; open correct RVT file version](#4)
- [Angular and react tutorials](#5)
- [Give and take](#6)

####<a name="2"></a> How to Determine Revit Export Image Coordinates

Raised in the StackOverflow question
on [how to get Revit export image coordinates or reference point](https://stackoverflow.com/questions/62004785/how-to-get-revit-export-image-coordinates-or-reference-point):

**Question:** I want to have a reference point or know the coordinates of any point on an exported Image (from any view) from Revit.

For example, in this Revit floor plan export, I'd like to know the bounding box of the picture, its middle point, or any other reliable reference point:

<center>
<img src="img/floor_plan.jpg" alt="Floor plan" title="Floor plan" width="500"/> <!-- 3093 -->
</center>

Is there a way to extract the bounding box coordinates of the picture?

**Answer:** I would suggest defining two diagonally opposite points in your image file that you can identify precisely in your Revit model.

Determine their image pixel coordinates, export their Revit model coordinates, and use this information to determine the appropriate scaling and translation.

The [RoomEditorApp Revit add-in](https://github.com/jeremytammik/RoomEditorApp) and its
corresponding [roomedit CouchDb](https://github.com/jeremytammik/roomedit) web interface
demonstrate exporting an SVG image from Revit, scaling it for display in a web browser, and transformation and calculation of exact coordinates back and forth between two environments.

**Response:** Thank you for your reply Jeremy, I'm looking into that.

I've noticed the `BoundingBoxUV` in the view I'm exporting changing based on the elements on the edges of the view; can this bounding box help me determine the exported image bounding box?

The units are different than the "specify coordinates at point", so I'm not sure.

**Answer:** They might certainly be useful, and almost equally certainly they will not enable any totally reliable and precise transformation.

Therefore, I would still stick with my initial suggestion.


####<a name="3"></a> Problems Compiling and Installing the Revit SDK Samples

**Question:** I'm trying to compile the SDK Revit 2019.2 Samples, but the buttons are disabled:

<center>
<img src="img/rvtsamples_greyed_out.png" alt="RvtSamples greyed out" title="RvtSamples greyed out" width="500"/> <!-- 1035 -->
</center>

Do you have any suspicion of what may be happening?

**Answer:** Yes, certainly.

Actually, what you show in the screen snapshot is the external application RvtSamples that creates a user interface to load and launch all the Revit SDK sample external commands.

The problem is probably caused by incorrect paths to the samples being specified in `RvtSamples.txt`.

I have run into similar issues many times over in the past, e.g., most lately,
[compiling the Revit 2021 SDK samples](https://thebuildingcoder.typepad.com/blog/2020/05/compiling-the-revit-2021-sdk-samples.html) and
[setting up RvtSamples for Revit 2021](https://thebuildingcoder.typepad.com/blog/2020/05/setting-up-rvtsamples-for-revit-2021.html).

You can check out those two posts and 
the [numerous descriptions of various problems encountered compiling the Revit SDK](https://www.google.com/search?q=compiling+sdk+samples&as_sitesearch=thebuildingcoder.typepad.com)
and [installing RvtSamples](https://www.google.com/search?q=installing+rvtsamples&as_sitesearch=thebuildingcoder.typepad.com) for
previous versions.


####<a name="4"></a> RvtVerFileOpen &ndash; Open Correct RVT File Version

From a running Revit session, you can easily determine its version number via
the [application `VersionNumber` property](https://www.revitapidocs.com/2020/35b18b73-4c47-fee3-d2f9-21298f029f7f.htm), cf., e.g., 
[how to get the current build of an open project file?](https://stackoverflow.com/questions/61936125/revit-python-how-to-get-the-current-build-of-an-open-project-file).

Determining the Revit version that saved an RVT file stored in the OS file system and not currently opened in Revit, however, is a different matter.

It can be handled using the [BasicFileInfo class](https://www.revitapidocs.com/2020/475edc09-cee7-6ff1-a0fa-4e427a56262a.htm),
as explained in several places, e.g., the discussion
on [basic file info and RVT file version](https://thebuildingcoder.typepad.com/blog/2013/01/basic-file-info-and-rvt-file-version.html).

Vin Gallo now shared a new utility making use of that functionality in
his [recent comment](https://thebuildingcoder.typepad.com/blog/2013/01/basic-file-info-and-rvt-file-version.html#comment-4927991760) on
that article:

> I've written a small app that opens any RVT file in the correct Revit version, simply by double-clicking the file in Windows Explorer.

> I have used some of the logic in this thread, which works for all versions of Revit up to 2021.

> I can share the source code here, but it's probably better to just share the Visual Studio Source, if anybody is interested.

> Here's a [short video](https://www.dropbox.com/s/eqwwk0zb4s9hee1/RvtFileOpen.mp4?dl=0).

> [Source Code](https://www.dropbox.com/s/1zvfnwxmju8z1z1/RvtVer.zip?dl=0).

> To make it work, you'll have to associate RVT files with this app.

Many thanks to Vin for implementing and sharing this!

For safety's sake, I made local copies of his video and source code here:

- [Demo video](zip/vg_RvtVerFileOpen.mp4)
- [Source code](zip/vg_RvtVerFileOpen.zip)

####<a name="5"></a> Angular and React Tutorials

Let's round off with some pretty impressive and pretty basic web related stuff, respectively:

- [How to create an optical character reader using Angular And Azure Computer Vision](https://www.freecodecamp.org/news/how-to-create-an-optical-character-reader-using-angular-and-azure-computer-vision)
- [How to get started with React &ndash; a modern project-based guide for beginners (including hooks)](https://www.freecodecamp.org/news/getting-started-with-react-a-modern-project-based-guide-for-beginners-including-hooks-2)

####<a name="6"></a> Give and Take

Finally, a short note on giving and taking:

[The most successful developers share more than they take](https://stackoverflow.blog/2020/05/14/the-most-successful-developers-share-more-than-they-take).

This doesn't just apply to developers, by the way, but to BIM experts as well,
cf. [Håvard Vasshaug on Learning Dynamo and Sharing Content
](https://thebuildingcoder.typepad.com/blog/2015/09/sharing-dynamo-and-a-chinese-book.html#2).

I love and believe in sharing and open source, cf. my
old [Book Recommendation: *The Cathedral and the Bazaar*](https://thebuildingcoder.typepad.com/blog/2011/09/revit-ifc-exporter-released-as-open-source.html#4).
