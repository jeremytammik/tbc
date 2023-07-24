<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- [Ripcord Engineering](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3926242) shares
  a possible solution to DLL Hell in certain circumstances, using the Python `subprocess` module for disentanglement without need for any IPC:
  CPython and PyRevit
  https://forums.autodesk.com/t5/revit-api-forum/cpython-and-pyrevit/m-p/12011805
  pyRevit - Dynamo Incompatibility: Two versions of Same DLL #1731
  https://github.com/eirannejad/pyRevit/issues/1731
  Python Module subprocess — Subprocess management
  https://docs.python.org/3/library/subprocess.html

- Export of multiple GBXML models
  https://forums.autodesk.com/t5/revit-api-forum/export-of-multiple-gbxml-models/m-p/12011838#M71878

- gbXml export using energy settings
  https://forums.autodesk.com/t5/revit-api-forum/gbxml-export-using-energy-settings/m-p/12011894#M71881

twitter:

 with the @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS

&ndash; ...

linkedin 1:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

###

####<a name="2"></a>

<center>
<img src="img/.png" alt="" title="" width="100"/>
</center>




<pre class="prettyprint">

</pre>



####<a name="3"></a>

**Question:**

**Answer:**

####<a name="4"></a>

####<a name="4"></a> DLL Paradise in Python

Jake of [Ripcord Engineering](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3926242) shares
a possible solution to DLL Hell in certain circumstances, using the Python `subprocess` module for disentanglement without need for any IPC:
CPython and PyRevit
https://forums.autodesk.com/t5/revit-api-forum/cpython-and-pyrevit/m-p/12011805
pyRevit - Dynamo Incompatibility: Two versions of Same DLL #1731
https://github.com/eirannejad/pyRevit/issues/1731
Python Module subprocess — Subprocess management
https://docs.python.org/3/library/subprocess.html


first of all, this question might be repetitive. if so, please point me to the right solution.
Here is my question: I need to use CPython via PyRevit to have access to libraries such as numpy and pandas. At the same time, I want to take advantage of Pyrevit’s capabilities such as forms etc. As far as I understood, I can’t have both of these on a single script file. If I got this correctly, is there any way to do this?
Thanks in advance
 Solved by RIPENG. Go to Solution.
Tags (0)
Add tags
Report
Labels (1)
PyRevit
5 REPLIES
Sort:
MESSAGE 2 OF 6
jeremy.tammik
  Autodesk jeremy.tammik  in reply to: pedramno
‎2023-05-31 03:45 AM

Sounds like a sensible question to me. However, this is not the optimal place for it. This forum is for discussing the Revit API, pure and simple. You might want to check out the issues and solutions in the pyRevit repository:

https://github.com/eirannejad/pyRevit

Quite a few people here in the forum also use pyRevit, though, and may be able to help. I am sure your goal can be achieved. There are many ways to disentangle and compartmentalise functionality. One important aspect to ponder is: how tightly are your Revit API bit and pieces interwoven with your numpy and pandas bits? I should hope that they are already pretty well decoupled to start with. In that case, you can make use of numerous IPC solutions to establish the connection required:

https://en.wikipedia.org/wiki/Inter-process_communication

Jeremy Tammik,  Developer Advocacy and Support, The Building Coder, Autodesk Developer Network, ADN Open
Tags (0)
Add tags
Report
MESSAGE 3 OF 6
pedramno
  Explorer pedramno  in reply to: jeremy.tammik
‎2023-06-01 09:52 AM
Thanks very much, Jeremy. I also checked the pyRevit forum and asked the same question there. The numpy part is quite decoupled since it is meant to help me with the data exchange process from other data sources and after that point, everything would be focused on Revit APIs. I'm not pretty familiar with IPC solutions but I'll take a look at them to familiarize myself more with them.
Tags (0)
Add tags
Report
MESSAGE 4 OF 6
RIPENG
  Enthusiast RIPENG  in reply to: pedramno
‎2023-06-05 06:05 AM

Dealt with the same challenge a little while back.

Please click here for a short discussion on Python subprocess in the Revit/pyRevit context.

While I am not a Revit API / Python / pyRevit expert I can report that subprocess worked well enough. Learning subprocess should be a productive use of time assuming the underlying characteristics are a good match for your application.

-Jake
Tags (0)
Add tags
Report
MESSAGE 5 OF 6
pedramno
  Explorer pedramno  in reply to: RIPENG
‎2023-06-07 08:11 AM
Thanks, Jake. I tried the same process, and it also worked perfectly for my case. Appreciate it.
Tags (0)
Add tags
Report
MESSAGE 6 OF 6
RIPENG
  Enthusiast RIPENG  in reply to: pedramno
‎2023-06-07 08:17 AM
Thanks for giving it a go. And thanks for the feedback.

-Jake

####<a name="4"></a> Multiple GbXML Export

- Export of multiple GBXML models
https://forums.autodesk.com/t5/revit-api-forum/export-of-multiple-gbxml-models/m-p/12011838#M71878


For my Thesis work at my university I have to make a lot of different GBXML models (around 18000). No way I can do that without code.
This is what I came up with (I attached only a part of it; FloorR, WallsR, RoofR are lists to set R value of corresponding elements):

####### Setting Enegry Analysis parameters  #############

opt=Analysis.EnergyAnalysisDetailModelOptions()
opt.EnergyModelType=Analysis.EnergyModelType.BuildingElement
opt.ExportMullions=False
opt.IncludeShadingSurfaces=False
opt.SimplifyCurtainSystems=True
opt.Tier=Analysis.EnergyAnalysisDetailModelTier.SecondLevelBoundaries



##### loop over all R-value combinations and create models    ####################

t=Transaction(doc,"R change")
c=Transaction(doc,"model creation")

for i in range(len(FloorR)):
    for j in range(len(WallsR)):
      for k in range(len(RoofR)):
        t.Start()
        Floor.Set(FloorR[i]/0.3048)  #R-value change for floor
        Wall.Set(WallsR[j]/0.3048)#R-value change for Walls
        Roof.Set(RoofR[k]/0.3048)#R-value change for roof
        t.Commit()
        t.Dispose()

        c.Start()
        model=Analysis.EnergyAnalysisDetailModel.Create(doc, opt)
        model.TransformModel()
        GBopt=GBXMLExportOptions()
        GBopt.ExportEnergyModelType=ExportEnergyModelType.BuildingElement
        doc.Export("C:\Users\Миша\Desktop\ASD","0"+","+str(0.2/FloorR[i])+","+str(0.3/WallsR[j])+","+str(0.3/RoofR[k]), GBopt)
        c.Commit()


This creates models, but I ran into a problem I dont fully understand: as the process continues, it slowes down and stops at about 170-175 created models. Apparently, comething is taking up the memory. I tried to doc.Delete(model) at the end of each 'for' loop, but it didnt help either.

What could be a solution?
Thank you !

 Solved by jeremytammik. Go to Solution.
Tags (0)
Add tags
Report
3 REPLIES
Sort:
MESSAGE 2 OF 4
jeremytammik
  Autodesk jeremytammik  in reply to: mikhail.demianenko
‎2020-03-22 07:09 AM

The behaviour you describe is completely expected and as designed.

Revit is an end user product designed to be driven by a human being.

Human beings are not expected to sit down and create 18000 models in one sitting.

I suggest you implement an external executable that drives Revit using the code you shared above and monitors progress as you export results from the models you create.

Whenever Revit starts slowing down, take note of how far you got in processing, kill the process, restart Revit and continue from where you left off.

This is a common approach to programmatically drive processes in batch mode that were not designed for it.

You can also search The Building Coder for further hints on batch processing Revit documents:

https://www.google.com/search?q=batch+processing&as_sitesearch=thebuildingcoder.typepad.com

Alternatively, you could generate your 18000 models online using Forge and DA4R:

https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.55

Good luck and have fun with your thesis!

Cheers,

Jeremy



Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Tags (0)
Add tags
Report
MESSAGE 3 OF 4
mikhail.demianenko
  Participant mikhail.demianenko  in reply to: jeremytammik
‎2020-03-22 07:45 AM
I'll try those out, thank you for your answer!
Tags (0)
Add tags
Report
MESSAGE 4 OF 4
RIPENG
  Enthusiast RIPENG  in reply to: mikhail.demianenko
‎2023-06-05 06:19 AM

Based on the code snippet provided, it appears that only R-values are manipulated and not the underlying model geometry. If that's the case, best to use Revit to export a single gbXML seed file. Then iterate over desired seed file parameters (like R-value) in an environment like Python which is excellent for large scale text operations.

Two utilities that would help with the route described above:
1. XmlNotepad (to build familiarity with gbXML structure and mechanization),
2. xgbxml (Python library for gbXML parsing and manipulation).

-Jake


####<a name="4"></a> GbXML Energy Settings

Jake points to the same solution to answer another question as well,
on [gbXml export using energy settings](https://forums.autodesk.com/t5/revit-api-forum/gbxml-export-using-energy-settings/m-p/12011894):

**Question:** Sorry to revive a thread which has been solved more than one year ago, however the solution provided is not working for me as I am programming in Python.

I am using pyRevit to program functions in Python.
When I use the code presented above, I get the following error:

<pre>
  EnergyAnalysisDetailModelOptions.ExportMullions = False

  Traceback (most recent call last):
    File "&lt;stdin&gt;", line 1, in &lt;module&gt;
  AttributeError: static property 'ExportMullions' of 'EnergyAnalysisDetailModelOptions' can only be assigned to through a type, not an instance
</pre>

If I understand this correctly, I am having a problem due to the type of variable in my code.
However, Python does not allow the declaration of variables.
How can I make the statement to become a type and not an instance?

Any idea how I can get past this issue without moving on to another language?

**Answer:** A nice example of Python `EnergyAnalysisDetailModelOptions` administration is discussed in
the [export of multiple gbXML models](https://forums.autodesk.com/t5/revit-api-forum/export-of-multiple-gbxml-models/m-p/9392003).

The relevant code snippet is this:

<pre class="prettyprint">
### Setting Energy Analysis parameters ###

opt=Analysis.EnergyAnalysisDetailModelOptions()
opt.EnergyModelType=Analysis.EnergyModelType.BuildingElement
opt.ExportMullions=False
opt.IncludeShadingSurfaces=False
opt.SimplifyCurtainSystems=True
opt.Tier=Analysis.EnergyAnalysisDetailModelTier.SecondLevelBoundaries
</pre>
