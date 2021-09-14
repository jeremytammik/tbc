<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- clean up AVF results before design option switch
  how to search all AVF analysis result and remove them?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-search-all-avf-analysis-result-and-remove-them/td-p/10437422

- kfpopeye Revit projects
  Project Sweeper, ReVVed, and other apps now open source
  https://forums.autodesk.com/t5/revit-api-forum/project-sweeper-revved-and-other-apps-now-open-source/m-p/10617548

twitter:

add #thebuildingcoder

 the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

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

<pre class="code">
</pre>

-->

### Cleanup Projects: Kfpopeye Sweeper, Open Source, AVF



####<a name="2"></a> Kfpopeye Open Source Projects

[kfpemail-2](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/11350013) kindly announced a bunch of new open Revit add-in projects in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Project Sweeper, ReVVed, and other apps now open source](https://forums.autodesk.com/t5/revit-api-forum/project-sweeper-revved-and-other-apps-now-open-source/m-p/10617548):

A while ago I decided to shut down [pkh Lineworks](http://www.pkhlineworks.ca) and
I discontinued work on my apps Project Sweeper, MLTE, ReVVed, Paraline and Knock Knock.
I have now decided to make them open source, so anyone can download the code and continue to update them for future versions of Revit.

The repositories can be found in
the [kfpopeye](https://github.com/kfpopeye)
[repositories](https://github.com/kfpopeye?tab=repositories) on GitHub:

- MLTE
- Knock-Knock
- Paraline
- Project-Sweeper
- ReVVed

I just added a readme file with a bit more information for them, plus a zip file `html_help_files.zip` containing the help documentation in html format providing detailed descriptions of what each command does::

- MLTE &ndash; M.L.T.E. (pronounced "multi") is program extension that is used inside Autodesk® Revit. It is a multiline text editor that can be used for editing the parameters of anything inside Revit but in a multi-line view instead of the single line that Revit's Properties Palette gives you. In addition to editing the values of parameters M.L.T.E. can also edit the formulas when used inside the Revit Family Editor. It even has syntax highlighting and auto-formatting to make complex nested if statements easier to follow and debug.
- Knock-Knock &ndash; a program extension that is used inside Autodesk® Revit. It is designed for editing the values of door instance parameters, not for changing the way your door schedule is set up. The primary way Knock Knock oes this is by making text parameters act like Yes\No parameters. With a simple click users can change parameters values between multiple pre-defined values. There are more features as well.
- Paraline &ndash; a Revit program extension that allows you to convert the detail elements from standard orthographic drawings like plans and elevations into 3D isometric drawings.
- Project-Sweeper &ndash; a collection of tools that allow a user to quickly and accurately remove the following clutter from Revit projects: line styles line patterns text styles fill region types and fill patterns. Except for text styles, these items are not checked by Revit's "Purge Unused" command. Project Sweeper goes beyond just checking for unused styles and patterns. It also allows a user to convert from one style\pattern to another, delete all the elements using a style\pattern and to preview all the views\elements using a style\pattern before removing it.
- ReVVed &ndash; an extension of commands for use within Autodesk® Revit

Many thanks to [pkh Lineworks](http://www.pkhlineworks.ca) and [kfpopeye](https://github.com/kfpopeye) for sharing this work!

####<a name="3"></a> AVF Result Clean-Up before Design Option Switch


<pre class="prettyprint">
</pre>


</pre>


<center>
<img src="img/" alt="" title="" width="100"/> <!-- 1000 -->
</center>

####<a name="4"></a> 


Thanks to ... for sharing this.

####<a name="5"></a> 
 
 
<pre class="code">
</pre>




