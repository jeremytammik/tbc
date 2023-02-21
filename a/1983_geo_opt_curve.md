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

### 

####<a name="2"></a> 


####<a name="3"></a> 

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 491 x 509 pixels -->
</center>

**Answer:** 

<pre class="prettyprint">


</pre>



Many thanks to  for the nice sample!

####<a name="4"></a> 


**Answer:** 



