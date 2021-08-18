<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- blog about the code snippet and instruction on floor creation API   
  https://autodesk.slack.com/archives/C0SR6NAP8/p1627395932194800
  Oleg Sheydvasser 27 Jul at 16:25
  It appears we need to provide clarifications to the users on the new Floor creation API that was introduced in R2022.
  A few old APIs were obsoleted, but the new methods work a bit differently (e.g. see https://app.slack.com/client/T02NW42JD/threads), so we need to provide instructions on how to migrate from the old API to new.
  Where should I put the instructions? The What's new section, the snippets?
  Scott Conover:no_entry:  22 days ago
  Snippets would be helpful.  For immediate availability, I'd suggest providing any guidance to @Jeremy Tammik so he can post about it faster than we can release an updated SDK.

- solar panels
  many_solar_panels.jpg
  jtracer
  running into all the hurdles described in [Learning from the real world: A hardware hobby project]
  https://stackoverflow.blog/2021/07/12/the-difference-between-software-and-hardware-projects/

twitter:

add #thebuildingcoder

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

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

-->

###

####<a name="2"></a> Floor Creation API Clarification

The developement team provide some clarification on how to user
the [new floor creation API](https://thebuildingcoder.typepad.com/blog/2021/04/whats-new-in-the-revit-2022-api.html#4.1.4.1) introduced
in Revit 2022.

Some old APIs were obsoleted, and the new methods work a bit differently, so some instructions on how to migrate from the old API to the new may come in handy, especially a sample code snippet like this:

<pre class="code">
/// The example below shows how to use Floor.Create method to create a new Floor with specified elevation, on one level 
/// using a geometry profile and a floor type. 
/// It shows how to adapt your code that used NewFloor and NewSlab methods, which are obsolete since 2022.
/// In this sample, the geometry profile is a CurveLoop of lines, you can also use arcs, ellipses and splines.
Floor CreateFloorAtElevation(Document document, double elevation)
{
   // Get a floor type for floor creation
   // You must provide a valid floor type (unlike in now obsolete NewFloor and NewSlab methods).
   ElementId floorTypeId = Floor.GetDefaultFloorType(document, false);

   // Get a level
   // You must provide a valid level (unlike in now obsolete NewFloor and NewSlab methods).
   double offset;
   ElementId levelId = Level.GetNearestLevelId(document, elevation, out offset);

   // Build a floor profile for the floor creation
   XYZ first = new XYZ(0, 0, 0);
   XYZ second = new XYZ(20, 0, 0);
   XYZ third = new XYZ(20, 15, 0);
   XYZ fourth = new XYZ(0, 15, 0);
   CurveLoop profile = new CurveLoop();
   profile.Append(Line.CreateBound(first, second));
   profile.Append(Line.CreateBound(second, third));
   profile.Append(Line.CreateBound(third, fourth));
   profile.Append(Line.CreateBound(fourth, first));

   // The elevation of the curve loops is not taken into account (unlike in now obsolete NewFloor and NewSlab methods).
   // If the default elevation is not what you want, you need to set it explicitly.
   var floor = Floor.Create(document, new List<CurveLoop> { profile }, floorTypeId, levelId);
   Parameter param = floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
   param.Set(offset);

   return floor;
}
</pre>

Sorry for the late information, and I hope it still helps with your migration.



<center>
<img src="img/.jpg" alt="AU" title="" width="100"/> <!-- 774 -->
</center>

####<a name="3"></a>

####<a name="4"></a> Solar Panels

I started experimenting with solar panels and an off-grid system requiring a charger, battery and inverter to generate standard 230 V AC power. 

I installed four 100 W peak panels on a small rather steep south facing balcony roof.
Actually, it has a 33 degree twist towards the west, so the direction is SSW.

They did not provide much power before almost midday in summertime, so I later added four more facing east, or rather ESE.

I ran into numerous challenges working with hardware rather than software, nicely described in the article
on [learning from the real world: a hardware hobby project](https://stackoverflow.blog/2021/07/12/the-difference-between-software-and-hardware-projects).

One such challenge was implemeting a perfomance monitor for the charger, including the required serial conection cables, etc.
I published a little piece of associated software in
the [jtracer GitHub repository](https://github.com/jeremytammik/jtracer)

I was initially working with a 12 V battery and am now in the process of upgrading to 24 V.

Once that is up and running, I may start on something bigger, trying to provide enough electrical power for several more apartments.

I hope I don't end up with something like this:

<center>
<img src="img/many_solar_panels.jpg" alt="Solar panels" title="Solar panels" width="100"/> <!-- 774 -->
</center>

