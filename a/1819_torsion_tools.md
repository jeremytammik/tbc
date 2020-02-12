<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- Torsion Tools Update #2 - Copy Linked Legends, Schedules and Reference Views
  https://youtu.be/C2dBRqXl9UA
  <iframe width="560" height="315" src="https://www.youtube.com/embed/C2dBRqXl9UA" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
  GitHub: https://github.com/TorsionTools/R20
  Autodesk Revit 2020 API Visual Studio Solution Template with Code Examples for Common Tools
  Update #2 will walk through the added tools to Copy Legends, Schedules, and Reference Views  from a Linked Model. 
  The Copy Legends and Schedules tools allow you to select the Link to copy them from, and then select the Legends or Schedules you would like to copy. It will check to see if they already exist in the project and prompt you if they do. 
  The Linked Views tool will allow you to select a Linked Model, Title Block, Viewport Type and then the Views you would like from the Linked Model. It will create a drafting view in the current Document with the Name, Detail Number and Sheet of the Linked Model.

- in 15056740 [Adding Project Template to 'New Project' via API]
https://forums.autodesk.com/t5/revit-api-forum/adding-project-template-to-new-project-via-api/m-p/8585348
Peter @cig_ad Ciganek just discovered:
> Autodesk.Revit.ApplicationServices.Application.CurrentUsersDataFolderPath returns the path where Revit.ini is located.

- in [localization website](https://forums.autodesk.com/t5/revit-api-forum/localization-website/m-p/8500166),
Susan Renna pointed out the new location for the Autodesk NeXLT localization website:
> You can find it here:  https://ls-wst.autodesk.com/app/nexlt-plus/app/home/search

- calculate volume and area of triangulated solid
  https://stackoverflow.com/questions/1406029/how-to-calculate-the-volume-of-a-3d-mesh-object-the-surface-of-which-is-made-up
  https://forge.autodesk.com/blog/get-volume-and-surface-area-viewer

twitter:

Revit.ini file path, NeXLT localization URL and Torsion Tools two for the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/torsiontoolstwo

More Revit API tutorial material and tools, plus a couple of hints from the Revit API discussion forum and the Forge blog
&ndash; Torsion Tools two
&ndash; Retrieve path to Revit.ini
&ndash; Updated NeXLT localization URL
&ndash; Volume and area of triangulated solid...

linkedin:

Revit.ini file path, NeXLT localization URL and Torsion Tools two for the #RevitAPI

http://bit.ly/torsiontoolstwo

More Revit API tutorial material and tools, plus a couple of hints from the Revit API discussion forum and the Forge blog:

- Torsion Tools two
- Retrieve path to Revit.ini
- Updated NeXLT localization URL
- Volume and area of triangulated solid...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Ini File, Localisation and Torsion Tools Two

More Revit API tutorial material and tools plus a couple of hints from the Revit API discussion forum and the Forge blog:

- [Torsion Tools two](#2)
- [Retrieve path to Revit.ini](#3)
- [Updated NeXLT localization URL](#4)
- [Volume and area of triangulated solid](#5)


#### <a name="2"></a>Torsion Tools Two

Continuing right on from the
fruitful [collection of videos and tutorials](https://thebuildingcoder.typepad.com/blog/2020/02/search-for-getting-started-tutorials-and-videos.html) presented
yesterday, an update to
the [Torsion Tools add-in template with code examples for common tools](https://thebuildingcoder.typepad.com/blog/2020/01/torsion-tools-command-event-and-info-in-da4r.html) introduced last week:

- [Torsion Tools Update #2 &ndash; Copy Linked Legends, Schedules and Reference Views](https://youtu.be/C2dBRqXl9UA):

> Autodesk Revit 2020 API Visual Studio Solution Template with Code Examples for Common Tools.
Update #2 walks through the added tools to copy legends, schedules, and reference views from a linked model.

> The Copy Legends and Schedules tools allow you to select the Link to copy them from, then select the Legends or Schedules you would like to copy.
It will check whether they already exist in the project and prompt you if they do.

> The Linked Views tool allows you to select a linked model, title block, viewport type and then the views you would like from the linked model.
It creates a drafting view in the current document with the name, detail number and sheet of the linked model.

> [TorsionTools GitHub repo](https://github.com/TorsionTools/R20)

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/C2dBRqXl9UA" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

Many thanks to Torsion Tools for creating and sharing this powerful resource!

#### <a name="3"></a>Retrieve Path to Revit.ini

A small note from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [adding project template to 'New Project' via API](https://forums.autodesk.com/t5/revit-api-forum/adding-project-template-to-new-project-via-api/m-p/8585348) by 
Peter @cig_ad Ciganek:

> The *Autodesk.Revit.ApplicationServices.Application* property `CurrentUsersDataFolderPath` returns the path where Revit.ini is located.

#### <a name="4"></a>Updated NeXLT Localization URL

In another small not from 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on the [localization website](https://forums.autodesk.com/t5/revit-api-forum/localization-website/m-p/8500166),
Susan Renna points out the new URL:

> You can find the Autodesk NeXLT localization website here:
[ls-wst.autodesk.com/app/nexlt-plus/app/home/search](https://ls-wst.autodesk.com/app/nexlt-plus/app/home/search)

#### <a name="5"></a>Volume and Area of Triangulated Solid

Finally, a useful generic pure geometry utility that might come in handy in the Revit environment as well
[to calculate the volume of a 3D mesh object, the surface of which is made up triangles](https://stackoverflow.com/questions/1406029/how-to-calculate-the-volume-of-a-3d-mesh-object-the-surface-of-which-is-made-up):

[Reading this paper](http://chenlab.ece.cornell.edu/Publication/Cha/icip01_Cha.pdf),
it is actually a pretty simple calculation.

The trick is to calculate the **signed volume** of a tetrahedron &ndash; based on your triangle and topped off at the origin.
The sign of the volume comes from whether your triangle is pointing in the direction of the origin
(The normal of the triangle is itself dependent upon the order of your vertices, which is why you don't see it explicitly referenced below).

This all boils down to the following simple function:

<pre class="code">
  public float SignedVolumeOfTriangle(Vector p1, Vector p2, Vector p3) {
    var v321 = p3.X*p2.Y*p1.Z;
    var v231 = p2.X*p3.Y*p1.Z;
    var v312 = p3.X*p1.Y*p2.Z;
    var v132 = p1.X*p3.Y*p2.Z;
    var v213 = p2.X*p1.Y*p3.Z;
    var v123 = p1.X*p2.Y*p3.Z;
    return (1.0f/6.0f)*(-v321 + v231 + v312 - v132 - v213 + v123);
  }
</pre>

A driver uses it to calculate the volume of the mesh:

<pre class="code">
  public float VolumeOfMesh(Mesh mesh) {
    var vols = from t in mesh.Triangles
      select SignedVolumeOfTriangle(t.P1, t.P2, t.P3);
    return Math.Abs(vols.Sum());
  }
</pre>

Adam Nagy picked this up and used it
to [calculate volume and surface area in the Forge viewer](https://forge.autodesk.com/blog/get-volume-and-surface-area-viewer),
verifying that the resulting values match the corresponding properties provided by Inventor:

<center>
<img src="img/forge_viewer_volume_surface_area.png" alt="Volume and surface area in the Forge viewer" title="Volume and surface area in the Forge viewer" width="600"/> <!-- 800 -->
</center>
