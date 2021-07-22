<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- au registration open and free
  email Autodesk University Registration is Now Open

- the importance of fuzz
  https://forums.autodesk.com/t5/revit-api-forum/weird-double-value-that-suppose-to-be-0-but-isn-t/m-p/10443154
  almost_zero_1.png
  almost_zero_2.png
  https://forums.autodesk.com/t5/revit-api-forum/element-geometry-not-returning-expected-face-count/m-p/10473778
  zr_beam_and_slab_surfaces_1.png
  zr_beam_and_slab_surfaces_dynamo.png

- afaik, the ultimate guide on [Getting started with Python]
  https://stackoverflow.blog/2021/07/14/getting-started-with-python/
  
- solar panels
  many_solar_panels.jpg
  jtracer
  running into all the hurdles described in [Learning from the real world: A hardware hobby project]
  https://stackoverflow.blog/2021/07/12/the-difference-between-software-and-hardware-projects/

twitter:

add #thebuildingcoder

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash;
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Question:** 

**Answer:**

**Response:**  

Many thanks to  for this very helpful explanation!

-->

### Fuzz and Autodesk University 



<center>
<img src="img/au_2022_registration.jpg" alt="AU 2022 registration" title="AU 2022 registration" width="400"/> <!-- 774 -->
</center>

####<a name="2"></a> Autodesk University Open and Free

This email is to let you know that registration is officially live for Autodesk University.
 
This year the event is a free, virtual and global conference held on October 5-14. For more info on the conference as a whole, visit the conference page. And if you are curious what Forge is doing you can check out our web page or our blog.
 
Please find attached and linked a promo cheat sheet which has the following:
Important dates
Marketing Activities
Key Links
Social posts
Email copy
Imagery
 
Our plan is to encourage the Forge community to register early for the conference and engage via social with our blog and weekly Twitter trivia. We also encourage you to promote early and often so we can gather and maintain Forge interest in the event.
 
Feel welcome to share this email with your colleagues. We’ll send one more like this when classes are released in late August.


####<a name="3"></a> Real Number Comparison Requires Fuzz

On a ditital computer, all real number comparison requires fuzz.

This means that you cannot compare two real numbers directly.
Instead, you test whether they are close enough together, where 'close enough' is defined by a given tolerance.
The tolerance depends on the context and the type of comparison being made.

Revit BIM geometry often ends up with significant imprecision due to various complex editing steps, so fuzz is especially important in this area.

This was highlighted yet again by the 
recent [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on a [weird double value supposed to be zero but isn't](https://forums.autodesk.com/t5/revit-api-forum/weird-double-value-that-suppose-to-be-0-but-isn-t/m-p/10443154):

<center>
<img src="img/almost_zero_1.png" alt="Number almost zero" title="Number almost zero" width="400"/> <!-- 1974 -->
<img src="img/almost_zero_2.png" alt="Number almost zero" title="Number almost zero" width="400"/> <!-- 1077 -->
</center>


<!--
https://forums.autodesk.com/t5/revit-api-forum/element-geometry-not-returning-expected-face-count/m-p/10473778
zr_beam_and_slab_surfaces_1.png
zr_beam_and_slab_surfaces_dynamo.png
-->


####<a name="4"></a> Getting Started with Python

In case you are considering learning Python, or to get started with programming in general, 
[freecosecamp](https://www.freecodecamp.org) published the ultimate guide
on [getting started with Python](https://stackoverflow.blog/2021/07/14/getting-started-with-python).

For more material on Dynamo, Python, and .NET, please refer to our earlier notes on

<!--
0964:Python the Hard Way
1057:Interactive Revit Database Exploration Using the Revit Python Shell
1078:How to use Python with Revit
1143:WAV Database, Python and GUI Tutorials
1448:<"#3">RevitPythonShell Dynamic Model Updater Tutorial
1452:<"#2">Retrieving a C# <code>out</code> Argument Value in Python
1570:Determining RVT File Version Using Python
1712:<"#1"> Cyril's Python HVAC Blog
1712:<"#2"> Rotating Elements Around Their Centre in Python
1712:<"#6"> Python Popularity Growing
1715:Retrieving Linked IfcZone Elements Using Python
1756:<"#3"> Retrieve RVT Preview Thumbnail Image with Python
1786:Pet Change &ndash; Python + Dynamo Swap Nested Family
1821:<"#2">Duplicate Legend Component in Python
1821:<"#3">Convert Latitude and Longitude to Metres in Python
1838:<"#3.1"> C&#35; versus Python
1838:<"#3.2"> Python and .NET
1890:<"#5"> Python and Dynamo Autotag Without Overlap
1893:<"#3"> Learning Python and Dynamo
1893:<"#3.2"> Take Dynamo Further Using Python

0964 1057 1078 1143 1448 1452 1570 1712 1712 1712 1715 1756 1786 1821 1821 1838 1838 1890 1893 1893
-->

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/06/python-and-ruby-scripting-resources-and-the-sharp-glyph.html">Python and Ruby Scripting Resources and the Sharp Glyph</a></li>        0964:Python the Hard Way
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html">Interactive Revit Database Exploration with the Python Shell</a></li> 1057:Interactive Revit Database Exploration Using the Revit Python Shell
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/12/devlab-munich.html">DevLab Munich</a></li>                                                                                            1078:How to use Python with Revit
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/wav-database-python-and-gui-tutorials.html">WAV Database, Python and GUI Tutorials</a></li>                                           1143:WAV Database, Python and GUI Tutorials
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/06/revitpythonshell-dynamic-model-updater-tutorial-and-wizard-update.html">Dynamic Model Updater Tutorial and Wizard Update</a></li>     1448:<"#3">RevitPythonShell Dynamic Model Updater Tutorial
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/07/retrieving-a-c-out-argument-value-in-python.html">Retrieving a C# out Argument Value in Python</a></li>                               1452:<"#2">Retrieving a C# <code>out</code> Argument Value in Python
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html">Determining RVT File Version Using Python</a></li>                                    1570:Determining RVT File Version Using Python
<li><a href="https://thebuildingcoder.typepad.com/blog/2018/12/rotate-picked-element-around-bounding-box-centre-in-python.html">Python Rotate Picked Around Bounding Box Centre</a></li>            1712:<"#1"> Cyril's Python HVAC Blog
<li><a href="https://thebuildingcoder.typepad.com/blog/2018/12/rotate-picked-element-around-bounding-box-centre-in-python.html">Python Rotate Picked Around Bounding Box Centre</a></li>            1712:<"#2"> Rotating Elements Around Their Centre in Python
<li><a href="https://thebuildingcoder.typepad.com/blog/2018/12/rotate-picked-element-around-bounding-box-centre-in-python.html">Python Rotate Picked Around Bounding Box Centre</a></li>            1712:<"#6"> Python Popularity Growing
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/01/retrieving-linked-ifczone-elements-using-python.html">Retrieving Linked IfcZone Elements Using Python</a></li>                       1715:Retrieving Linked IfcZone Elements Using Python
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/06/accessing-bim360-cloud-links-thumbnail-and-dynamo.html">Access BIM360 Cloud Links, Thumbnail and Dynamo</a></li>                     1756:<"#3"> Retrieve RVT Preview Thumbnail Image with Python
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/10/pet-change-python-and-dynamo-swap-nested-families.html">Pet Change &ndash; Python + Dynamo Swap Nested Family</a></li>               1786:Pet Change &ndash; Python + Dynamo Swap Nested Family
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/02/lat-long-to-metres-and-duplicate-legend-component.html">Lat Long to Metres and Duplicate Legend Component</a></li>                   1821:<"#2">Duplicate Legend Component in Python
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/02/lat-long-to-metres-and-duplicate-legend-component.html">Lat Long to Metres and Duplicate Legend Component</a></li>                   1821:<"#3">Convert Latitude and Longitude to Metres in Python
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/2021-migration-add-in-language-and-bim360-login.html">2021 Migration, Add-In Language, BIM360 Research</a></li>                      1838:<"#3.1"> C&#35; versus Python
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/2021-migration-add-in-language-and-bim360-login.html">2021 Migration, Add-In Language, BIM360 Research</a></li>                      1838:<"#3.2"> Python and .NET
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/02/splits-persona-collector-region-tag-modification.html">Splits: Persona, Collector, Region, Tag, Modification</a></li>                1890:<"#5"> Python and Dynamo Autotag Without Overlap
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/02/addin-file-learning-python-and-ifcjs.html#3">Learning Python and Dynamo</a></li>                                           1893:<"#3"> Learning Python and Dynamo
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/02/addin-file-learning-python-and-ifcjs.html#3.2">Take Dynamo Further Using Python</a></li>                                           1893:<"#3.2"> Take Dynamo Further Using Python
</ul>


####<a name="5"></a> 


solar panels
many_solar_panels.jpg
jtracer
running into all the hurdles described in [Learning from the real world: A hardware hobby project]
https://stackoverflow.blog/2021/07/12/the-difference-between-software-and-hardware-projects/
