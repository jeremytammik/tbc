<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- connect RevitLookup with RevitPythonShell
  Chuong Ho breathed new life into the faltering RevitPythonShell,
  updating it for Revit 2023, integrated a CI pipeline, and adding new powerful functionality by hooking it up directly with RevitLookup
  so, now you can snoop your database in an interactive REPL commandline console
  database explorastion power par excellence, never previously available to such an extent
  
  Automatic Process CI/CD Maintain Support 2023 #122
  https://github.com/architecture-building-systems/revitpythonshell/pull/122
  
  Need create a branch dev to collaborator #123
  https://github.com/architecture-building-systems/revitpythonshell/issues/123

  Update Support use method from revitlookup to snoop #124
  https://github.com/architecture-building-systems/revitpythonshell/pull/124
  
  demo video
  https://user-images.githubusercontent.com/31106432/176649030-d07dc40e-8662-47af-8a0b-528128c45384.gif

- getting started with the Revit API for MEP
  [Q] Now we are trying to develop some Revit add-ins.  I would like to know how to allow the user to insert a predefined MEP component in a model and how to detect if two components, for example two pipes, are connected in the codes.  Could you please let me know where I can find the related information?  Do you have any related examples to share with me?  Thanks!
  [A] Welcome to the Revit API!
  The Building Coder shares a wealth of information on getting started with the Revit API:
  https://thebuildingcoder.typepad.com/blog/about-the-author.html#2
  However, before you start even thinking about programming, it is important to gain some fundamental understanding of the BIM, the BIM paradigm and BIM processes.
  Furthermore, you should determine in detail exactly how to address your task manually through the end user interface making use of the optimal workflows and respecting best practices, before you start trying to automate the task.
  All the questions you raise are adressed by and demonstrated in the Revit SDK samples. The SDK can be downloaded from the official Autodesk Revit developer page:
  https://www.autodesk.com/developer-network/platform-technologies/revit
  MEP components are represented by family instances, so you can simply use generic code to insert a family instance.
  However, there are also many MEP-specific enhancements that may or may not apply.
  To determine whether two pipes are connected, you simply query them for their connectors, represented by the Connector class:
  https://www.revitapidocs.com/2022/11e07082-b3f2-26a1-de79-16535f44716c.htm
  It has a ConnectorManager property:
  https://www.revitapidocs.com/2022/61339b71-5d90-c53d-bec4-2209bab97787.htm
  That in turn proves access to the neighbouring parts' connectors.
  System traversal is also demonstrated by some of the SDK samples.
  In addition to the official sample material from Autodesk, you can also check out Building Coder blog.
  Here is my favourite series of articles from there on connecting pipes:
  http://thebuildingcoder.typepad.com/blog/2014/01/final-rolling-offset-using-pipecreate.html
  Here are some relevant articles from there on system traversal:
  Simple MEP System Traversal -- http://thebuildingcoder.typepad.com/blog/2013/02/simple-mep-system-traversal.html
  Traversing and Exporting all MEP System Graphs -- http://thebuildingcoder.typepad.com/blog/2016/06/traversing-and-exporting-all-mep-system-graphs.html

- RevitLookup 

twitter:

 in the #RevitAPI  @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Getting Started with MEP API and RevitPythonShell with Lookup

####<a name="2"></a> 


####<a name="3"></a> 

**Question:** 

<pre class="prettyprint">
</pre>




**Answer:** 


**Response:** 



####<a name="4"></a> 

####<a name="5"></a> 

<center>
<img src="img/.png" alt="" title="" width="500"/> <!-- 1000 -->
</center>

