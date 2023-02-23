<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- cleaning up and simplifying curve loops
Benoit Favre, CEO of [etudes &amp; automates](http://www.etudesetautomates.com)
Boundary Segments Issue
https://forums.autodesk.com/t5/revit-api-forum/boundary-segments-issue/m-p/11732446#M69140
Funny to get this very old post alive.
I'd change my answer from the time and say:
- sometimes the BoundarySegment list is holed (around windows and at the end of walls ending in the middle of the Room). So you have to close the List, practically we add another Segment to the List.
- check either Douglas Peucker or Visvaligham algorithms, easy to implement and very useful. At least that's what we use and these work fine for us.
douglas peucker algorithm
https://duckduckgo.com/?q=douglas+peucker+algorithm
Ramer–Douglas–Peucker algorithm
https://en.wikipedia.org/wiki/Ramer%E2%80%93Douglas%E2%80%93Peucker_algorithm
visvalingam algorithm
https://duckduckgo.com/?q=visvalingam+algorithm
Visvalingam–Whyatt algorithm
https://en.wikipedia.org/wiki/Visvalingam%E2%80%93Whyatt_algorithm
Many thanks to Benoit for the interesting pointer!

- clarifying geometry options, app.Create.NewGeometryOptions, 
  Find Centroid of wall in Revit API
  https://forums.autodesk.com/t5/revit-api-forum/find-centroid-of-wall-in-revit-api/m-p/11748826

- Greg version of JtClicker in 2023
  Jacopo Chiappetti
  Senior Analyst & Developer
  One Team srl
  Some annotations, schedules, view templates, filters, and views related to analytical elements might be modified or lost during the upgrade process
  https://forums.autodesk.com/t5/revit-api-forum/some-annotations-schedules-view-templates-filters-and-views/m-p/11721147

- A awesome package for MEP, Computational Design Inside Dynamo Revit
https://github.com/chuongmep/OpenMEP
OpenMEP Package also includes a comprehensive library of MEP components, making it easy to select and incorporate the right components into your design.This library includes a wide range of mechanical, electrical, and plumbing components, including pipes, fittings, valves, ducts, electrical equipment, and more fully automate your design process in design, maintenance, calculation and analysis,...
I believe that the MEP Package will be a valuable asset to construction professionals looking to streamline the MEP design process and ensure that their projects are completed on time and within budget.
https://www.linkedin.com/posts/chuongmep_opensource-dynamo-autodesk-activity-7033100499461570561-OUfG?utm_source=share&utm_medium=member_desktop
Chuong Ho
Computational Design Researcher | Autodesk Expert Elite
Hi everyone, today I want to tell you that a stable and long-term support package for MEP engineers open source has been released.
Open MEP Package also includes a comprehensive library of MEP components, making it easy to select and incorporate the right components into your design. This library includes a wide range of mechanical, electrical, and plumbing components, including pipes, fittings, valves, ducts, electrical equipment, and more fully automate your design process in design, maintenance, calculation and analysis,...
I believe that the MEP Package will be a valuable asset to construction professionals looking to streamline the MEP design process and ensure that their projects are completed on time and within budget.
Discuss on forum : https://lnkd.in/gtAi8RUP
Open Source : https://lnkd.in/gcWvCCXK
Now, I accept all ideas and all problems, contributions from all engineers, communities around the world.

- [Why we all need subtitles now](https://www.youtube.com/watch?v=VYJtb2YXae8)

twitter:

with the @AutodeskRevit #RevitAPI #BIM @AutodeskAPS 

&ndash; 
...

linkedin:

#bim #DynamoBim #AutodeskAPS #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Geometry Options and Clean Simple Curves

####<a name="2"></a> Curve Loop Simplify and Clean Up

Benoit Favre, CEO of [etudes &amp; automates](http://www.etudesetautomates.com) shared 
some interesting advice on how to simplify and clean up curve loops in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on a [boundary segments issue](https://forums.autodesk.com/t5/revit-api-forum/boundary-segments-issue/m-p/11732446#m69140):

Funny to get this very old post alive.

I'd change my answer from the first time and now say:

- Sometimes the `BoundarySegment` list has gaps, e.g., around windows and at the end of walls ending in the middle of a room. 
  So, you have to close the gap; in practice, we add another segment to the list.
- Check out either 
  the [Douglas Peucker](https://duckduckgo.com/?q=douglas+peucker+algorithm)
  or [Visvaligham algorithm](https://duckduckgo.com/?q=visvalingam+algorithm);
  they are easy to implement and very useful. 
  At least that's what we use and they work fine for us.

On Wikipedia:

- [Ramer–Douglas–Peucker algorithm](https://en.wikipedia.org/wiki/Ramer%E2%80%93Douglas%E2%80%93Peucker_algorithm)
- [Visvalingam–Whyatt algorithm](https://en.wikipedia.org/wiki/Visvalingam%E2%80%93Whyatt_algorithm)

Many thanks to Benoit for the interesting pointers!

####<a name="2"></a> Geometry Options

The question on how to [find centroid of wall in Revit API](https://forums.autodesk.com/t5/revit-api-forum/find-centroid-of-wall-in-revit-api/m-p/11748826)
provided an opportunity to clarify the meaning of specific settings in the geometry `Options` and the use of the `NewGeometryOptions` method:

[`ComputeReferences`](https://www.revitapidocs.com/2023/d7da6de4-74a9-60e2-826f-698a5730d0a8.htm) is 
only needed if you require references to the geometry, e.g., for dimensioning purposes. 
Furthermore, it adds computational effort. 
Therefore, you should not set it to true unless needed, as explained in the 2010 article 
on [Geometry Options](https://thebuildingcoder.typepad.com/blog/2010/01/geometry-options.html).

The effect of turning off `ComputeReferences` was recently benchmarked in the discussion 
on [computing the correlation of objects in Revit](https://forums.autodesk.com/t5/revit-api-forum/computing-the-correlation-of-objects-in-revit/m-p/11701329/highlight/true#M68810):

It includes the final code and the benchmark results:

PC specs:

- CPU: 11th Gen Intel(R) Core(TM) i5-11400F @ 2.60GHz 2.59 GHz
- GPU: NVidia GeForce GTX 1650
- RAM: 32.0 GB
- OS: Windows 10 Pro 

The call to <i>BooleanOperationsUtils.ExecuteBooleanOperation(solidST, solidAR, BooleanOperationsType.Intersect)</i>
is triggered 113,696 times, both lists columnsSTR and columnsARC have 336 items each.

- Code runtime with `ComputeReferences` = true : 10.38 sec, AVG. 91.34 micro-seconds per intersection.
- Code runtime with `ComputeReferences` = false : 9.52 sec, AVG. 83.76 micro-seconds per intersection.

`IncludeNonVisibleObjects` is only required for certain supplementary graphical elements, e.g., 
for [curtain walls](https://thebuildingcoder.typepad.com/blog/2010/05/curtain-wall-geometry.html).

I am pretty sure that it is never needed for such basic element geometry as solids.

So, I would leave both of those turned off in this case, set to their default value of false.

Furthermore, I very much doubt that there is any different between using `new Options` and `app.Create.NewGeometryOptions`. 

However, specifying a view argument in the options will definitely make a difference, depending on the view you supply. 
That can be achieved using both `new Options` and `app.Create.NewGeometryOptions`. 

####<a name="2"></a> JtClicker

- Greg version of JtClicker in 2023
Jacopo Chiappetti
Senior Analyst & Developer
One Team srl
Some annotations, schedules, view templates, filters, and views related to analytical elements might be modified or lost during the upgrade process
https://forums.autodesk.com/t5/revit-api-forum/some-annotations-schedules-view-templates-filters-and-views/m-p/11721147

####<a name="2"></a> OpenMEP

- A awesome package for MEP, Computational Design Inside Dynamo Revit
https://github.com/chuongmep/OpenMEP
OpenMEP Package also includes a comprehensive library of MEP components, making it easy to select and incorporate the right components into your design.This library includes a wide range of mechanical, electrical, and plumbing components, including pipes, fittings, valves, ducts, electrical equipment, and more fully automate your design process in design, maintenance, calculation and analysis,...
I believe that the MEP Package will be a valuable asset to construction professionals looking to streamline the MEP design process and ensure that their projects are completed on time and within budget.
https://www.linkedin.com/posts/chuongmep_opensource-dynamo-autodesk-activity-7033100499461570561-OUfG?utm_source=share&utm_medium=member_desktop
Chuong Ho
Computational Design Researcher | Autodesk Expert Elite
Hi everyone, today I want to tell you that a stable and long-term support package for MEP engineers open source has been released.
Open MEP Package also includes a comprehensive library of MEP components, making it easy to select and incorporate the right components into your design. This library includes a wide range of mechanical, electrical, and plumbing components, including pipes, fittings, valves, ducts, electrical equipment, and more fully automate your design process in design, maintenance, calculation and analysis,...
I believe that the MEP Package will be a valuable asset to construction professionals looking to streamline the MEP design process and ensure that their projects are completed on time and within budget.
Discuss on forum : https://lnkd.in/gtAi8RUP
Open Source : https://lnkd.in/gcWvCCXK
Now, I accept all ideas and all problems, contributions from all engineers, communities around the world.

####<a name="2"></a> Unintelligible

- [Why we all need subtitles now](https://www.youtube.com/watch?v=VYJtb2YXae8)

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 491 x 509 pixels -->
</center>

**Answer:** 

<pre class="prettyprint">

</pre>

Many thanks to  for the nice sample!

####<a name="4"></a> 

**Answer:** 

