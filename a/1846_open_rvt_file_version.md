<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- William Felipe
Q: I'm trying to compile the SDK Revit 2019.2 Samples, but the buttons are disabled. Do you have any suspicion of what may be happening?
A: yes: https://thebuildingcoder.typepad.com/blog/2020/05/compiling-the-revit-2021-sdk-samples.html
You should also read The numerous Building Coder descriptions of all the various problems encountered installing them for previous versions. Please refer to the blog!

- RvtVerFileOpen
https://thebuildingcoder.typepad.com/blog/2013/01/basic-file-info-and-rvt-file-version.html#comment-4927991760
Vin Gallo
I've written a small app that opens any RVT file in the correct Revit version, simply by double-clicking the file in Windows Explorer.
I have used some of the logic in this thread, which works for all versions of Revit up to 2021.
I can share the source code here, but it's probably better to just share the Visual Studio Source, if anybody is interested.
Here's a short video:
https://www.dropbox.com/s/eqwwk0zb4s9hee1/RvtFileOpen.mp4?dl=0
Source Code:
https://www.dropbox.com/s/1zvfnwxmju8z1z1/RvtVer.zip?dl=0
To make it work, you'll have to associate RVT files with this app.
Local copies:
vg_RvtVerFileOpen.mp4
vg_RvtVerFileOpen.zip

- How to get Revit export image coordinates or reference point?
  https://stackoverflow.com/questions/62004785/how-to-get-revit-export-image-coordinates-or-reference-point
  Q: I want to have a reference point or to know the coordinates of any point on an exported Image (from any view) from Revit.
For example in the attached image exported from Revit, I'd like to know the bounding box of the picture or the middle point of the picture (in X,Y coordinates) or any other reference point.
  img/floor_plan.jpg
Is there a way to extract the bounding box coordinates of the picture?
A: I would suggest defining two diagonally opposite points in your image file that you can identify precisely in your Revit model. Determine their image pixel coordinates, export their Revit model coordinates, and use this information to determine the appropriate scaling and translation.
The [RoomEditorApp Revit add-in](https://github.com/jeremytammik/RoomEditorApp) and its corresponding [roomedit CouchDb](https://github.com/jeremytammik/roomedit) web interface demonstrate exporting an SVG image from Revit, scaling it for display in a web browser, and transformation and calculation of exact coordinates back and forth between two environments.
R: Thank you for your reply Jeremy, I'm looking into that. I've notice the BoundingBoxUV in the view Im exporting changing based on the elements on the edges of the view, is this boundingbox can help me determine the exported image bounding box? The units are different than the "specify coordinates at point" so Im not sure. – Razyo 23 hours ago
A: They might certainly be useful, and almost equally certainly they will not enable any precise transformation. Therefore I would still stick with my suggestion. – Jeremy Tammik just now   Edit

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

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

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

### Automatically Open Currect RVT File Version


####<a name="2"></a> 


####<a name="3"></a> 


**Question:** 

**Answer:** 



**Response:** 


####<a name="5"></a> 

<pre class="code">
</pre>


<center>
<img src="img/" alt="" title="" width="327"/>
</center>


