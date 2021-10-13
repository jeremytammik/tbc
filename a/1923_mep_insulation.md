<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- mention gibson agency
  william_gibson_agency.jpg

- performance issue retrieving MEP pipe insulation elements using GetDependentElements
  by Александр Игнатович <cadbimdeveloper@yandex.ru>
  I have a short note for TBC. Only a little part of the blogpost somewhere in the future.
  Recently I faced with a performance issue getting pipes insulation. My previous implementation looked like this:
  var pipeInsulation = pipe
    .GetDependentElements(new ElementClassFilter(typeof(PipeInsulation)))
    .Select(pipe.Document.GetElement)
    .Cast<PipeInsulation>()
    .FirstOrDefault();
  I didn't notice it before because I tested the code on a small model, however in the big model entire calculation tooked more than hour. Calculations were also huge, so I spent some time trying to figure out what is going wrong. Now the solution looks like this:
  var pipeInsulation = InsulationLiningBase
    .GetInsulationIds(pipe.Document, pipe.Id)
    .Select(pipe.Document.GetElement)
    .OfType<PipeInsulation>()
    .FirstOrDefault();
  Now the entire calculations take seconds instead of hours.
  This issue is related only to MEP; I haven't faced any other performance issues usng the `GetDependentElements` method.
  thank you very much for the interesting observation!

- lanuages to learn
  email [Computational Strategy - Question for the Expert] Andrea Rolle
  [Q] We are now starting our in-house computational strategy in order to automate processes on both Revit-Dynamo and Inventor-iLogic and I am struggling to decide which language code we should start to learn.
  C#, Python or F#? I am moving my first steps into coding but I have a few years of experience on Dynamo & Grasshopper. 
  I would like to add that our Inventor has got an automation interface developed by a consultant company written in F# which eventually we would like to take control over it in the long term.
  Long story short I would like to understand the pro and cons about  which language should we start to learn (between C#, Python and F#) to better unify a future workflow between Revit and Inventor, keeping in mind that we have something in house already developed in F#.
  [A] I would say:
  - Python: best fpr learning, and for Dynamo
  - C# best for pure Revit API, most example code, cleanest .NET interface
  - F# best for stateless procedural generic logical lambda computation, and you'll need it in the long run anyway

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

### MEP Pipe Insulation Retrieval Performance

Three quick notes from my recent email correspondance and reading:


####<a name="2"></a> MEP Pipe Insulation Retrieval Performance

- performance issue retrieving MEP pipe insulation elements using GetDependentElements
  by Александр Игнатович <cadbimdeveloper@yandex.ru>
  I have a short note for TBC. Only a little part of the blogpost somewhere in the future.
  Recently I faced with a performance issue getting pipes insulation. My previous implementation looked like this:
  var pipeInsulation = pipe
    .GetDependentElements(new ElementClassFilter(typeof(PipeInsulation)))
    .Select(pipe.Document.GetElement)
    .Cast<PipeInsulation>()
    .FirstOrDefault();
  I didn't notice it before because I tested the code on a small model, however in the big model entire calculation tooked more than hour. Calculations were also huge, so I spent some time trying to figure out what is going wrong. Now the solution looks like this:
  var pipeInsulation = InsulationLiningBase
    .GetInsulationIds(pipe.Document, pipe.Id)
    .Select(pipe.Document.GetElement)
    .OfType<PipeInsulation>()
    .FirstOrDefault();
  Now the entire calculations take seconds instead of hours.
  This issue is related only to MEP; I haven't faced any other performance issues usng the `GetDependentElements` method.
  thank you very much for the interesting observation!



<center>
<img src="img/william_gibson_agency.jpg" alt="William Gibson Agency" title="William Gibson Agency" width="400"/> <!-- 1843 -->
</center>

####<a name="3"></a>


####<a name="4"></a> 



