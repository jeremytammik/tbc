<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- used migration assistant to move to loaner machine
  /Users/jta/a/doc/revit/tbc/git/a/img/problem_no_problem.jpg

- ExportCncFab updated for Revit 2022 https://github.com/jeremytammik/ExportCncFab/releases/tag/2022.0.0.0
  ExportCncFab eliminated deprecated API usage https://github.com/jeremytammik/ExportCncFab/releases/tag/2022.0.0.1

- lots of interest and many threads on multi-version Revit add-in
  check out wizard alternatives
  https://forums.autodesk.com/t5/revit-api-forum/multi-version-revit-template/m-p/10659412
  https://github.com/jeremytammik/VisualStudioRevitAddinWizard#alternatives
  
- Additional .dll files as resource
  https://forums.autodesk.com/t5/revit-api-forum/additional-dll-files-as-resource/m-p/10653802#M58650
  ricaun in reply to: antonio.hipolito
  @jrothMEIand @antonio.hipolito you could use Fody.Costura to embed the .dll references automatically, the Costura.Template has the ILTemplate.cs and Common.cs to handle all the load resources files, if the Assembly is already loaded the code does not force it to load again.
  @jeremy.tammik I use this technic on the ConduitMaterial and others plugins.
  Adding... ILTemplate.Attach(); on the IExternalApplication should do the trick.

twitter:

add #thebuildingcoder

 with the #RevitAPI add-in #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

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

### Multi-Version Add-Ins and DLL as Resource

Successfully moved from my broken-screen laptop to a loaner machine, updated 

####<a name="2"></a> Migration Assistant Rocks

I mentioned
my [computer mishap resulting in a broken screen](https://thebuildingcoder.typepad.com/blog/2021/10/localised-forge-intros-and-apply-code-changes.html#2) and
the happy fact that the rest of the system still works.
Unfortunately, requiring an external screen and hence mains power significantly reduces mobility.

As a next step, I was able to transfer the entire system with all user and application data to a temporary loaner machine using the Mac migration assistant.

That worked riht out the box.
The only problem remaining being that the updated OS prevented me
from [setting up my personal root level directories](https://thebuildingcoder.typepad.com/blog/2021/08/revit-roadmap-api-and-da4r-survey.html#4).
I am forced to rewrite all my (numerous) shell scripts and add `$HOME` to them.
I guess I can live with that.

<center>
<img src="img/problem_no_problem.jpg" alt="Problem &ndash; no problem" title="Problem &ndash; no problem" width="100"/> <!-- 921 -->
</center>


####<a name="3"></a> ExportCncFab 2022 

By popular demand, I now
updated [ExportCncFab](https://github.com/jeremytammik/ExportCncFab) for Revit 2022.

It is a Revit .NET API add-in that exports Revit wall parts to DXF or SAT for CNC fabrication, demonstrating various useful aspects along the way, such as:

- Implement the external application
- Create ribbon panel and command push buttons
- Load icons from assembly resources
- Select wall parts and handle optional pre-selection
- Export wall part elements to DWF or SAT
- Manage, create and populate shared parameter values

The flat migration is captured
in [ExportCncFab release 2022.0.0.0](https://github.com/jeremytammik/ExportCncFab/releases/tag/2022.0.0.0) and
some deprecated API usage is eliminated in the subsequent
[release 2022.0.0.1](https://github.com/jeremytammik/ExportCncFab/releases/tag/2022.0.0.1).

####<a name="4"></a> Multi-Version Revit Add-In

There has been a lot of interest and several new threads on multi-version Revit add-ins recently, e.g.,
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [multi-version Revit template](https://forums.autodesk.com/t5/revit-api-forum/multi-version-revit-template/m-p/10659412).

Several Visual Studio Revit add-in templates now implement support for that functionality right out of the box.

To provide an overview of the some available options, I added
a [list of alternatives](https://github.com/jeremytammik/VisualStudioRevitAddinWizard#alternatives) to
the [VisualStudioRevitAddinWizard GitHub repository](https://github.com/jeremytammik/VisualStudioRevitAddinWizard).

####<a name="5"></a> Deploy DLL File as a Resource

In the thread
on [additional `.dll` files as resource](https://forums.autodesk.com/t5/revit-api-forum/additional-dll-files-as-resource/m-p/10653802#M58650),
Luiz Henrique [@ricaun](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4176855) Cassettari provides
some useful hints on embedding a DLL in the add-iun assembly, saying:

> You could
use [Fody.Costura](https://github.com/Fody/Costura) to
embed the .dll references automatically.
The [Costura.Template](https://github.com/Fody/Costura/tree/develop/src/Costura.Template) provides
`ILTemplate.cs` and `Common.cs` to handle all the load resources files.
If the Assembly is already loaded, the code does not force it to load again.
I use this technique on
the [ConduitMaterial](https://apps.autodesk.com/RVT/en/Detail/Index?id=9120027511121592515) and
others plugins.
Adding `ILTemplate.Attach()` on the `IExternalApplication` should do the trick.

Many thanks to Luiz Henrique for sharing this!
