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

####<a name="2"></a> 


<center>
<img src="img/problem_no_problem.jpg" alt="Problem &ndash; no problem" title="Problem &ndash; no problem" width="100"/> <!-- 921 -->
</center>


####<a name="3"></a> 


ExportCncFab updated for Revit 2022 https://github.com/jeremytammik/ExportCncFab/releases/tag/2022.0.0.0
ExportCncFab eliminated deprecated API usage https://github.com/jeremytammik/ExportCncFab/releases/tag/2022.0.0.1

####<a name="4"></a> 


lots of interest and many threads on multi-version Revit add-in
check out wizard alternatives
https://forums.autodesk.com/t5/revit-api-forum/multi-version-revit-template/m-p/10659412
https://github.com/jeremytammik/VisualStudioRevitAddinWizard#alternatives

####<a name="5"></a> 


Additional .dll files as resource
https://forums.autodesk.com/t5/revit-api-forum/additional-dll-files-as-resource/m-p/10653802#M58650
ricaun in reply to: antonio.hipolito
@jrothMEIand @antonio.hipolito you could use Fody.Costura to embed the .dll references automatically, the Costura.Template has the ILTemplate.cs and Common.cs to handle all the load resources files, if the Assembly is already loaded the code does not force it to load again.
@jeremy.tammik I use this technic on the ConduitMaterial and others plugins.
Adding... ILTemplate.Attach(); on the IExternalApplication should do the trick.


<pre class="code">

</pre>
