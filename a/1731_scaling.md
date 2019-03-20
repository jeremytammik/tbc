<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Konrad Sobon
  @arch_laboratory
  [Q] Quick question. I noticed that in this write up: https://thebuildingcoder.typepad.com/blog/2019/01/room-boundaries-to-csv-and-wpf-template.html#3 … you talk about exporting room boundaries to CSV to be imported into Forge. I am curious about the process of actually importing extra geometrical data into Forge and if you have any samples you can share. 
  I am specifically tying to get things like location points for certain families to be able to dimension to them using the measuring tool in Forge. The thing is that reference points are technically not a mesh so they don't get exported to Forge during regular model derivative translation process. I was hoping to be able to export them to a CSV file, and import into Forge Viewer, creating custom three.js geometry just for that purpose. I am a little lost when it comes to any coordinates translations that I would have to apply. All my points when exported to default decimal feet coordinates end up all over the place. Any ideas? 
  [A] hi konrad, thank you for an interesting and very valid question. @ipetrbroz , can you please answer konrad and share the answer with me so i can share on the blog? thank you!
  [R] Thank you Jeremy. Hi @ipetrbroz . Hopefully you can shed some light on this. I would really appreciate that. Even a link to some openly available code would help me here. I can sort it out from there. I just havent seen any examples of this out there or google searches fail me. Either way thanks!
  [A] in my experiments connecting desktop and cloud, the Forge viewer just replicated the internal Revit database units one to one:
  [R] sure. I have been looking at this some more, and there seems to be an offset applied that is based on the bounding box size. perhaps Forge Viewer measures center of the geometry bounding box, and places that at 0,0,0? That would explain the offset. Thoughts?
  [A] yes, that is an extremely viable theory. i think i have indeed heard that exact explanation in the past. origin at the center, unity scaling, i.e., imperial feet.

- Fixing the scaling of text
  Alexander Pekshev, author of the [ModPlus blog](http://blog.modplus.org), Александр Пекшев, Автор – ModPlus for AutoCAD, Revit, Renga, Moscow, Russian Federation
  https://www.linkedin.com/in/%D0%B0%D0%BB%D0%B5%D0%BA%D1%81%D0%B0%D0%BD%D0%B4%D1%80-%D0%BF%D0%B5%D0%BA%D1%88%D0%B5%D0%B2-141268163/
  Revit: Correct the problem of incorrect display of the contents of the Dockable Panel when the screen is scaled
  using a [.NET `Decorator` class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.decorator?view=netframework-4.7.2)
  This problem is present in Revit 2015-2017 and possibly in 2018, and apparently fixed in Revit 2019.
  http://blog.modplus.org/index.php/17-fixdockablepanelonscreenscale

- https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-extents-when-exporting-an-image-from-a-view-using/td-p/8656885
  How to get the extents when exporting an image from a view using C# API? 
  [how to get the extents when exporting an image from a view](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-extents-when-exporting-an-image-from-a-view-using/td-p/8656885),
  [exporting images of all my doors](https://forums.autodesk.com/t5/revit-api-forum/export-images-of-all-my-doors/m-p/8655358).

- face normals and mesh triangle orientation
  https://forums.autodesk.com/t5/revit-api-forum/about-the-normal-of-a-mesh-triangle/m-p/8546140

twitter:

Scaling right left and centre in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/snoopdependents

&ndash; 
...

linkedin:

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).

-->

### Scaling Right Left and Centre

Let's look at a couple of scaling issues in all kinds of different contexts that recently came up:

#### <a name="2"></a> Transform and Scaling in Forge

Konrad Sobon of [@arch_laboratory](https://twitter.com/arch_laboratory) raised a question on scaling and transformation of a Revit model in the Forge viewer:

**Question:** Quick question. I noticed that you recently talked
about [exporting room boundaries to CSV to be imported into Forge](https://thebuildingcoder.typepad.com/blog/2019/01/room-boundaries-to-csv-and-wpf-template.html#3).

I am curious about the process of actually importing extra geometrical data into Forge, and if you have any samples you can share. 

I am specifically trying to get things like location points for certain families to be able to dimension to them using the measuring tool in Forge. The thing is that reference points are technically not a mesh so they don't get exported to Forge during regular model derivative translation process. I was hoping to be able to export them to a CSV file, and import into Forge Viewer, creating custom three.js geometry just for that purpose. I am a little lost when it comes to any coordinates translations that I would have to apply. All my points when exported to default decimal feet coordinates end up all over the place. Any ideas? 

**Answer:** In my experiments connecting desktop and cloud, the Forge viewer just replicated the internal Revit database units one to one:

**Response:** Sure. I have been looking at this some more, and there seems to be an offset applied that is based on the bounding box size. perhaps Forge Viewer measures center of the geometry bounding box, and places that at 0,0,0? That would explain the offset. Thoughts?

**Answer:** Yes, that is an extremely viable theory.
I think I have indeed heard that exact explanation in the past.
Origin at the center, unity scaling, i.e., imperial feet, the internal Revit database units.


#### <a name="3"></a> ModPlus and Scaling Text in Dockable Panel

Alexander Pekshev, aka [Александр Пекшев](https://www.linkedin.com/in/%D0%B0%D0%BB%D0%B5%D0%BA%D1%81%D0%B0%D0%BD%D0%B4%D1%80-%D0%BF%D0%B5%D0%BA%D1%88%D0%B5%D0%B2-141268163) is
the author of the [ModPlus blog](http://blog.modplus.org),
where he publsihed an interesting article explaining how
to [correct the problem of incorrect display of the contents of the Dockable Panel when the screen is scaled](http://blog.modplus.org/index.php/17-fixdockablepanelonscreenscale) using
a [.NET `Decorator` class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.decorator?view=netframework-4.7.2).

<center>
<img src="img/modplus_scaling_in_dockable_panel.png" alt="ModPlus Dockable Panel text scaling issue" width="498">
</center>

This problem is apparently fixed in Revit 2019, so the solution is relevant for the preceding versions.

The `Decorator` class may still have other uses as well, of course.

#### <a name="4"></a> Exporting View Image Extents

Starting to move away from the main topic of scaling, some interesting conversations took place in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160)
on [how to get the extents when exporting an image from a view](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-extents-when-exporting-an-image-from-a-view-using/td-p/8656885),
vaguely related to [exporting images of all my doors](https://forums.autodesk.com/t5/revit-api-forum/export-images-of-all-my-doors/m-p/8655358).

If you have any questions about image export, check them out.

#### <a name="5"></a> Face Normals and Mesh Triangle Orientation

Finally, an issue not related to scaling,
[about the normal of a mesh triangle](https://forums.autodesk.com/t5/revit-api-forum/about-the-normal-of-a-mesh-triangle/m-p/8546140):

**Question:**

Current I tried to use a series of triangle meshs  to form a polyhedron as an approximation of a very complcated solid. Therefore I triangulated each face of the solid by face.Triangulate() method and collect those triangle meshes. However, I found that the triangle meshes don't have normals. I am wondering how to get the correct normal of each triangle that points outside the polyhedron.

**Answer:** Number each triangles vertices clockwise from 1 to 3, then take something like (x3-x1).CrossProduct(x2-x1). Of course, this local numbering should be consistently enforced globally in the provided mesh data to fix the sign, otherwise you would have some pointing inward, some outward of your polyhedron.

**Response:**

I am wondering how to determine the "right" clockwise or counter-clockwise of a triangle. As is shown in the figure below, the vertice of triangle 1 are arranged counterclockwisely and the normal can be calculated correctly(right-hand rule is applied); however, those of triangle 2 are also arranged counterclockwisely but the normal is incorrect.

<center>
<img src="img/triangle_orientation.jpg" alt="" width="428">
</center>

If every face of a solid is a planar face that is not a problem because the triangles can directly get the face normal, but in my case some faces are curved ones, therefore it is not easy to get the right normal directions.

Thanks again and I'm looking forward to your reply.

**Answer:**
You can calculate the normal on any face with the  face.ComputeNormal() method.

<pre class="code">
			Face f;
			XYZ MeshPt; // x1, x2 or x3
			XYZ NormalFromPoints; //  (x3-x1).CrossProduct(x2-x1)
			UV UVpt = f.Project(MeshPt).UVPoint;
			XYZ face_normal = f.ComputeNormal(UVpt);
			XYZ surface_normal = f.OrientationMatchesSurfaceOrientation? face_normal : face_normal.Negate();
			
			XYZ MeshNormal = surface_normal.DotProduct(NormalFromPoints)> 0? NormalFromPoints : NormalFromPoints.Negate();
</pre>

**Response:**

Thank you very much for helping me again.

I'm afraid I cannot find the method "OrientationMatchesSurfaceOrientation" maybe because my API is from Revit 2017.

I test the code without the method and found that the normal calculated in this method:

<pre class="code">
  var tri = mesh.get_Triangle(I);
  XYZ pt0 = tri.get_Vertex(0);
  XYZ pt1 = tri.get_Vertex(1);
  XYZ pt2 = tri.get_Vertex(2);
  XYZ vec1 = pt1 - pt0;
  XYZ vec2 = pt2 - pt0;
  XYZ normal = vec1.CrossProduct(vec2);
</pre>

can always return the same direction as the face.project method does. is that sufficient to say the Revit software have taken the direction of the vertices of the mesh into consideration?

Thank you again.

<pre class="code">
</pre>

<center>
<img src="img/.png" alt="" width="100">
</center>

I hope you find this useful and wish you lots of fun and success experimenting and enhancing further.
