<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- NetTopologySuite
  Room boundary to baseboards: group up the connected line segments
  https://forums.autodesk.com/t5/revit-api-forum/room-boundary-to-baseboards-group-up-the-connected-line-segments/m-p/11582383#M67643
  BenoitE&A in reply to: MiguelGT17
  I'm wondering what you are trying to do. Creating railings is tedious if the aim is only to work on geometry.
  We've done a lot of Geometry of this type. I guess you retrieve the geometry of the room with GetBoundarySegments.
  I have never crossed a case where the arrangement of the BoundarySegments are not consecutive (and I've seen tens and my algos thousands) so I'm really curious (and skeptical) about that. 
  We made the choice to compute geometry using both internal and external tools (NetTopologySuite) because we needed to have boolean algebra and specific tools to work on Room boundaries, which we found tedious using XYZ.
  Anyway I'm curious...
  https://nettopologysuite.github.io/NetTopologySuite/index.html
  ... sounds very interesting indeed! Thank you for pointing it out. Would you like to share some examples of using it in combination with the Revit API? This might make a brilliant article for The Building Coder and motivate many others to widen their horizon working with 2D geometry in the Revit API.
  Ahah we did many things:
  - automatically place furniture in housing (from beds, the easiest, to TV set, kitchen appliances and bathroom stuff)
  - automatically place electric fixtures in housing (lights, switches, plugs) or HVAC elements (ventilation, heating)
  - automatically recognize housing units and name Rooms from their caracteristics
  The first 2 examples use mainly simple geometric rules while the last makes an extensive use of boolean 2D operations.
  And we are currently working on connecting 2 elements with for ex. cold water pipe, which is not that easy (more geometry there ;))

twitter:

 with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #ForgeDevCon https://autode.sk/64bitelementid

...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Nairobi, Kenya


####<a name="2"></a> DAS Team in Nairobi, Kenya

Unexpectedly, I find myself travelling again, on rather short notice, to our new office in Nairobi, Kenya.

I arrived Monday night and find it very pleasant here.

Met with the Nairobi DAS team yesterday:

<center>
<img src="img/2022-12-06_nairobi_team.jpg" alt="Nairobi team" title="Nairobi team" width="690"/>  <!-- 1380 × 880 pixels -->
<p style="font-size: 80%; font-style:italic">Nairobi team: Fidel, Carol, Brian, Jeremy, Harun, Emmanuel, George</p>
</center>

Today I spent the morning with Timothy and Allan.

####<a name="3"></a> NetTopologySuite in Revit Add-Ins

Returning to the Revit API, 
Benoit Favre, CEO of [etudes &amp; automates](http://www.etudesetautomates.com), 
made an interesting suggestion in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [room boundary to baseboards: group up the connected line segments](https://forums.autodesk.com/t5/revit-api-forum/room-boundary-to-baseboards-group-up-the-connected-line-segments/m-p/11582383#M67643):

> ... Creating railings is tedious if the aim is only to work on geometry.
We've done a lot of geometry of this type.
I guess you retrieve the geometry of the room with `GetBoundarySegments`.
I never encountered a case where the arrangement of the BoundarySegments are not consecutive (and I've seen tens, and my algos thousands), so, I'm really curious (and skeptical) about that. 
We made the choice to compute geometry using both internal and external tools,
e.g., [NetTopologySuite](https://nettopologysuite.github.io/NetTopologySuite/index.html),
because we need to have Boolean algebra and specific tools to work on Room boundaries, which we found tedious using XYZ.

> We did many things with NetTopologySuite:
- automatically place furniture in housing (from beds, the easiest, to TV set, kitchen appliances and bathroom stuff)
- automatically place electric fixtures in housing (lights, switches, plugs) or HVAC elements (ventilation, heating)
- automatically recognize housing units and name Rooms from their caracteristics

> The first 2 examples use mainly simple geometric rules, while the last makes an extensive use of Boolean 2D operations.
And we are currently working on connecting 2 elements with for ex. cold water pipe, which is not that easy (more geometry there ;))

Many thanks to Benoit for the interesting pointer!

####<a name="4"></a> 



<pre class="code">

</pre>

Thank you, !

