<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<!--
<script src="run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true&amp;skin=sunburst&amp;lang=css" defer="defer"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true&amp;skin=sunburst" defer="defer"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js"></script>
</head>

<!---

Highlighting Revit Rooms in the Forge Viewer @AutodeskForge @CubeAthens #3dwebcoder #revitapi #3dweb #threejs

I am leaving Athens today. The Athens Forge meetup and web server workshop at The Cube went well.
Now it is full steam ahead towards the Forge DevCon in San Francisco week after next. Not to forget the 3D Web Fest on June 15, in parallel with Forge DevCon, showcasing the best of the 3D web as live performance art with a catered reception in a film festival atmosphere...

-->


### Highlighting Revit Rooms in the Forge Viewer

I am leaving Athens today.

The [Athens Forge meetup](http://www.meetup.com/de-DE/I-love-3D-Athens/events/230543759) and
[web server workshop](http://www.meetup.com/de-DE/I-love-3D-Athens/events/230544059)
at [The Cube](http://thecube.gr) went well.

Here are a couple of pictures before and after the events:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157669263727915" title="Athens"><img src="https://c8.staticflickr.com/8/7125/27430452255_afc4f13059_n.jpg" width="320" height="240" alt="Athens"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>
	
Now it is full steam ahead towards
the [Forge DevCon](http://forge.autodesk.com/conference) in San Francisco week after next.

Not to forget the [3D Web Fest](http://www.3dwebfest.com) on June 15, in parallel with Forge DevCon, showcasing the best of the 3D web as live performance art with a catered reception in a film festival atmosphere. This year all proceed go to the non-profit [CODAME](http://www.codame.com) ART+TECH project.

At the recent Barcelona [Forge](http://forge.autodesk.com) [Accelerator](http://autodeskcloudaccelerator.com), I talked with Jaroslav Daníček
of [CAD Studio a.s.](http://www.cadstudio.cz).

Jaroslav very kindly shared his hard-won experience and algorithm how to [highlight a specific Room in the Forge viewer](#4).

The [Forge Viewer discussion forum](http://forums.autodesk.com/t5/view-and-data-api/bd-p/95)
includes some other hints
on [handling Revit rooms](http://forums.autodesk.com/t5/view-and-data-api/revit-rooms/m-p/5297233) that might be worth mentioning first.


#### <a name="2"></a>Accessing Room Data

The Forge viewer displays geometrical objects.

By default, the rooms in a Revit model do not generate any geometry.

However, their other information, attributes and parameters are all present in the translated model.

If you know the room element id, you can retrieve it from the translated viewer data.

It is not immediately accessible through the user interface, since there is no geometry associated with it.

A room is a void, and the viewer is mainly focused on displaying (non-void) geometrical objects.

You can easily implement your own extensions to retrieve, display and interact with the room data in any way you like.


#### <a name="3"></a>Automatically Navigating to a Specific Room

One approach to automatically navigate to a specific room was implemented at
the [cloud accelerator in Prague](http://the3dwebcoder.typepad.com/blog/2015/09/towards-a-comphound-mongo-database-table-view.html)
in September last year by Jaroslav's colleague Vitezslav Peka:

Several participant interested in Revit models ran into this issue, and various solutions were implemented.

The simplest approach to zoom to a Revit room in the Forge viewer seems to be the following:

- Place a family instance with some geometry inside each room. You might want to place it at the ceiling level, somewhere in the 'centre' of the room, for the zooming and cutting described below.
- Save and translate the modified file, then discard it and revert to original state, e.g., by saving active doc A to new project B, open B in background, modify, save, translate, delete, continue working in A.
- Search in the viewer for the Revit `Rooms` category.
- That returns a list of the (invisible) rooms.
- Find the corresponding family instance for the room to retrieve its data.
- Get its bounding box in the viewer.
- Make a section cut at that location to show the room.
- Set the camera to the correct view and zoom.


#### <a name="4"></a>Highlighting a Specific Room in the Forge Viewer

Jaroslav took Vitezslav's solution one step further to implement Revit room highlighting in the Forge viewer. In his own words:

Here is what I did basically:

Revit side:

- Add lines for every room following its 'floor plan', so in the viewer every line has its own object and DbID.
- Sort them in order like 'drawn with pencil in one move', second line should start where previous ends.
- Upload Revit model to the cloud.
- Store information in the database which lines (respective DbIDs of lines) belongs to which rooms and their order.

If a room contains holes it is a little bit more complicated for drawing in the viewer; you must also store information whether a line represents a hole or main 'circle'.

Viewer side (for 3D models only):

- When selecting a room from the room list, identify the room and its lines in the database, via ajax send DbIDs of lines back.
- Now you have got array of DbIDs of lines.
- For every line get its start and end point; they provide coordinates for the vertices.
- Create a 2D shape from the vertices and extrude shape to height of a room or cut plane.
- From shape and extrusion setup create a mesh with transparent material and add it to the viewer.
- Set correct Z coordinate for the new mesh.
- For better orientation and visibility is good to set the cut plane to the height of a level where a room is located and animate the camera to look from above.

Here are some sample images:

A clean level cut:

<center>
	<img src="img/viewer_hilite_room_1.png" alt="Viewer highlight room &mdash; a clean level cut" width="460">
</center>

A complicated room shape:

<center>
	<img src="img/viewer_hilite_room_2.png" alt="Viewer highlight room &mdash; a complicated room shape" width="460">
</center>

A simple room shape next to the complicated one:

<center>
	<img src="img/viewer_hilite_room_3.png" alt="Viewer highlight room &mdash; a simple room shape next to the complicated one" width="460">
</center>

Many thanks to Jaroslav for sharing these valuable hints!

Have fun implementing your own Forge viewer extensions!

#### <a name="5"></a>Forge Viewer Extension Starting Points

If you are looking for handy starting points, you can look at
my [roomedit3d](https://github.com/jeremytammik/roomedit3d) sample,
Kean Walmsley's [introduction to creating extensions](http://through-the-interface.typepad.com/through_the_interface/2016/05/creating-extensions-for-the-autodesk-viewer.html)
and [vertical toolbar extension](http://through-the-interface.typepad.com/through_the_interface/2016/05/creating-a-vertical-toolbar-extension-for-the-autodesk-viewer.html),
and, above all, the mother of them all, Philippe Leefsma's
[collection of JavaScript extensions for the viewer](https://github.com/Developer-Autodesk/library-javascript-viewer-extensions) and gallery.

Maxim [@redcraft](https://github.com/redcraft) Gurkin's
[lmv-extensions](https://github.com/Developer-Autodesk/lmv-extensions) also
look interesting... has anyone taken a look at them?
