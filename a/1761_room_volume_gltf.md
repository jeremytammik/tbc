<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

Room volume glTF generator for Forge in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/roomvolumegltf

I travelled home from the Barcelona Forge accelerator and continued working on the room volume exporter.
I now implemented support for glTF, the GL Transmission Format.
A few more little items to wrap up the Barcelona topic
&ndash; Kean Walmsley's report on this year's Forge Accelerator in Barcelona
&ndash; My two favourite restaurants in Poblenou...

linkedin:

Room volume glTF generator for Forge in the #RevitAPI

http://bit.ly/roomvolumegltf

I travelled home from the Barcelona Forge accelerator and continued working on the room volume exporter.

I now implemented support for glTF, the GL Transmission Format.

A few more little items to wrap up the Barcelona topic:

- Kean Walmsley's report on this year's Forge Accelerator in Barcelona
- My two favourite restaurants in Poblenou...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

-->

### Room Volume glTF Generator

I travelled home last night from the Barcelona Forge accelerator and continued working on
the [room volume exporter](https://thebuildingcoder.typepad.com/blog/2019/06/improved-room-closed-shell-directshape-for-forge-viewer.html).

As suggested by [Michael Beale](https://forge.autodesk.com/author/michael-beale),
I now implemented support
for [glTF, the GL Transmission Format](https://en.wikipedia.org/wiki/GlTF).

As described yesterday, I generate generic model `DirectShape` elements to represent the room volume in the Forge viewer.

I initially generated them using solids returned by the Revit API `GetClosedShell` method.
However, these not work properly in the Forge viewer.

Therefore, I implemented a triangulation of those solids and generate new ones from that.
They work fine.

While I have the triangulation at hand, it is easy to also generate data for glTF.

That is now implemented in
[RoomVolumeDirectShape](https://github.com/jeremytammik/RoomVolumeDirectShape)
[release 2020.0.0.8](https://github.com/jeremytammik/RoomVolumeDirectShape/releases/tag/2020.0.0.8).

It still needs some final tweaks to feed it straight into a glTF viewer, e.g.,
[magicien's GLTFQuickLook](https://github.com/magicien/GLTFQuickLook) for Mac,
but we're getting there.

A few more little items to wrap up the Barcelona topic:

- Kean Walmsley's report on [this year's Forge Accelerator in Barcelona](https://www.keanw.com/2019/06/this-years-forge-accelerator-in-barcelona.html)
- My two favourite restaurants in Barcelona, both in Poblenou, 20 minutes' walk from the Autodesk office:
    - [Fish &ndash; Restaurant Els Pescadors](http://www.elspescadors.com)
    - [Vegetarian &ndash; Aguaribay](http://www.aguaribay-bcn.com)

<!-- <br/><i>Els Pescadors, situat a Barcelona, és un restaurant especialitzat en arròs i peix des de 1980. Els nostres plats estan elaborats amb producte de proximitat, ecològic i fresc. Vinguin a viure l'experiència de gaudir d'un àpat tranquil i gustós al nucli antic de Poblenou.</i> -->

<center>
<img src="img/forge_team_lunch.jpg" alt="Forge team lunch on the beach" width="500">
</center>
