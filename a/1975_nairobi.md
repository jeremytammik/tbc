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
  I have never crossed a case where the arrangement of the BoundarySegments is not consecutive (and I've seen tens and my algos thousands) so I'm really curious (and sceptical) about that. 
  We made the choice to compute geometry using both internal and external tools (NetTopologySuite) because we needed to have boolean algebra and specific tools to work on Room boundaries, which we found tedious using XYZ.
  Anyway I'm curious...
  https://nettopologysuite.github.io/NetTopologySuite/index.html
  ... sounds very interesting indeed! Thank you for pointing it out. Would you like to share some examples of using it in combination with the Revit API? This might make a brilliant article for The Building Coder and motivate many others to widen their horizon working with 2D geometry in the Revit API.
  Ahah we did many things:
  - automatically place furniture in housing (from beds, the easiest, to TV set, kitchen appliances and bathroom stuff)
  - automatically place electric fixtures in housing (lights, switches, plugs) or HVAC elements (ventilation, heating)
  - automatically recognize housing units and name Rooms from their characteristics
  The first 2 examples use mainly simple geometric rules while the last makes an extensive use of boolean 2D operations.
  And we are currently working on connecting 2 elements with for ex. cold water pipe, which is not that easy (more geometry there ;))

twitter:

I am in Nairobi, Kenya, right now, getting to know the team here; new APS landing page, create RVT using APS and using NetTopologySuite with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #ForgeDevCon https://autode.sk/nairobi

I am writing this in Nairobi, Kenya, getting to know the team here; also, the new APS landing page just went live, and Benoit points out a useful geometric modelling library to help power your Revit add-in
&ndash; DAS team in Nairobi, Kenya
&ndash; NetTopologySuite in Revit add-ins
&ndash; New APS landing page
&ndash; You can create RVT using APS...

linkedin:

I am in Nairobi, Kenya, right now, getting to know the team here; new APS landing page, create RVT using APS and using the NetTopologySuite geometric modelling library with the #RevitAPI

https://autode.sk/nairobi

- DAS team in Nairobi, Kenya
- NetTopologySuite in Revit add-ins
- New APS landing page
- You can create RVT using APS...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### APS, Nairobi and NetTopologySuite

I am writing this in Nairobi, Kenya, getting to know the team here; also, the new APS landing page just went live, and Benoit points out a useful geometric modelling library to help power your Revit add-in:

- [DAS team in Nairobi, Kenya](#2)
- [NetTopologySuite in Revit add-ins](#3)
- [New APS landing page](#4)
- [You can create RVT using APS](#5)

####<a name="2"></a> DAS Team in Nairobi, Kenya

Unexpectedly, I find myself travelling again, on rather short notice, to our new office in Nairobi, Kenya.

I arrived Monday night and find it very pleasant here.

Met with the Nairobi DAS team yesterday:

<center>
<img src="img/2022-12-06_nairobi_team.jpg" alt="Nairobi team" title="Nairobi team" width="690"/>  <!-- 1380 × 880 pixels -->
<p style="font-size: 80%; font-style:italic">Nairobi team: Fidel, Carol, Brian, Jeremy, Harun, Emmanuel, George (sans Timothy and Allan)</p>
</center>

Today I spent the morning with Timothy and Allan.
The purpose of the visit is team building.
I am also continuing with my normal work, i.e., supporting
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) and
blogging.

Plus, I received my new PC, a MacBook M1, and am hoping to set it up during my visit here.
Hopefully, the reports on running Windows in Parallels and [Revit on the MacBook M1](https://kinship.io/blog/revit-m1-macbook-pro/) will indeed work out.

####<a name="3"></a> NetTopologySuite in Revit Add-Ins

Returning to the Revit API and current cases, 
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

> We did many things with [NetTopologySuite](https://nettopologysuite.github.io/NetTopologySuite/index.html):

> - automatically place furniture in housing (from beds, the easiest, to TV set, kitchen appliances and bathroom stuff)
- automatically place electric fixtures in housing (lights, switches, plugs) or HVAC elements (ventilation, heating)
- automatically recognize housing units and name Rooms from their characteristics

> The first 2 examples use mainly simple geometric rules, while the last makes an extensive use of Boolean 2D operations.
And we are currently working on connecting 2 elements with for ex. cold water pipe, which is not that easy (more geometry there ;))

Many thanks to Benoit for the interesting pointer!

####<a name="4"></a> New APS Landing Page

As you may have heard,
[Autodesk Forge was renamed to Autodesk Platform Services, APS](https://thebuildingcoder.typepad.com/blog/2022/09/aps-au-and-miter-wall-join-for-full-face.html#2).

Now, we are glad and proud to announce the new [APS landing page](https://aps.autodesk.com) went live.

I immediately grabbed the chance to highlight that in this clarification on how to create an RVT project file from scratch without running Revit locally:

####<a name="5"></a> You Can Create RVT using APS 

APS came up in the question on [creating .RVT file from C#](https://forums.autodesk.com/t5/revit-api-forum/create-rvt-file-from-c/td-p/9693451):

**Question:** I am a software developer and I am very new to Revit and AutoCAD.
For a project, I need to create RVT file from C# using the Revit API.
I do not want to develop a plugin, I want to simply be able to draw objects and save them in a file `.rvt` extension so that the end user can just double click on this file and open it directly in Revit. 
I am unable to figure out a way to do this since the API documentation suggests its usage for building plugins. 

**Answer:** Using APS, you can manipulate models without the need to open Revit.

There is no (official) way to programmatically create an RVT project file without making use of the Revit API, and the Revit API requires a running session of Revit.exe to obtain a valid Revit API context. Without such a valid Revit API context, the API cannot be used. It is completely event driven, and only Revit can launch the necessary events.
So, for the desktop, this means you need to have a full Revit product installation and a running Revit session.

However, you can make use of the [APS Autodesk Platform Services (formerly Forge)](https://aps.autodesk.com).

The APS Design Automation API for Revit enables you to create an RVT project file without installing Revit on your local machine.
Instead, a Revit engine is launched in the cloud and executes your add-in code on a server, returning the resulting RVT file for you to download locally:

- [APS Design Automation API](https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/)
- [APS Design Automation API for Revit](https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/#design-automation-api-for-revit)
