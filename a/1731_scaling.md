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

Scaling, face and mesh triangle normals in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/triangles-scaling

Let's look at a couple of scaling and triangle orientation issues that recently came up
&ndash; Transform and scaling in Forge
&ndash; ModPlus and scaling text in dockable panel
&ndash; Exporting view image extents
&ndash; Mesh triangle orientation
&ndash; <code>PlanarFace</code> normal...

linkedin:

Scaling, face and mesh triangle normals in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/triangles-scaling

Let's look at a couple of scaling and triangle orientation issues that recently came up:

- Transform and scaling in Forge
- ModPlus and scaling text in dockable panel
- Exporting view image extents
- Mesh triangle orientation
- PlanarFace normal...

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).

-->

### Scaling, Face and Mesh Triangle Normals

Let's look at a couple of scaling and triangle orientation issues that recently came up:

- [Transform and scaling in Forge](#2) 
- [ModPlus and scaling text in dockable panel](#3) 
- [Exporting view image extents](#4) 
- [Mesh triangle orientation](#5) 
- [`PlanarFace` normal](#6) 


#### <a name="2"></a> Transform and Scaling in Forge

Konrad Sobon of [@arch_laboratory](https://twitter.com/arch_laboratory) raised a question on scaling and transformation of a Revit model in the Forge viewer:

**Question:** Quick question. I noticed that you recently talked
about [exporting room boundaries to CSV to be imported into Forge](https://thebuildingcoder.typepad.com/blog/2019/01/room-boundaries-to-csv-and-wpf-template.html#3).

I am curious about the process of actually importing extra geometrical data into Forge, and if you have any samples you can share.

I am specifically trying to get things like location points for certain families to be able to dimension to them using the measuring tool in Forge. The thing is that reference points are technically not a mesh so they don't get exported to Forge during regular model derivative translation process. I was hoping to be able to export them to a CSV file, and import into Forge Viewer, creating custom three.js geometry just for that purpose. I am a little lost when it comes to any coordinates translations that I would have to apply. All my points when exported to default decimal feet coordinates end up all over the place. Any ideas?

**Answer:** In my experiments [connecting desktop and cloud](https://github.com/jeremytammik/FireRatingcloud),
the Forge viewer just replicated the internal Revit database units one to one.

**Response:** Sure. I have been looking at this some more, and there seems to be an offset applied that is based on the bounding box size.
Perhaps Forge Viewer measures centre of the geometry bounding box, and places that at 0,0,0?
That would explain the offset.
Thoughts?

**Answer:** Yes, that is an extremely viable theory.
I think I have indeed heard that exact explanation in the past.
Origin at the centre, unity scaling, i.e., imperial feet,
the [internal Revit database units](http://thebuildingcoder.typepad.com/blog/2011/03/internal-imperial-units.html).


#### <a name="3"></a> ModPlus and Scaling Text in Dockable Panel

Alexander Pekshev, aka [Александр Пекшев](https://www.linkedin.com/in/%D0%B0%D0%BB%D0%B5%D0%BA%D1%81%D0%B0%D0%BD%D0%B4%D1%80-%D0%BF%D0%B5%D0%BA%D1%88%D0%B5%D0%B2-141268163) is
the author of the [ModPlus blog](http://blog.modplus.org),
where he published an interesting article explaining how
to [correct the problem of incorrect display of the contents of the Dockable Panel when the screen is scaled](http://blog.modplus.org/index.php/17-fixdockablepanelonscreenscale) using
a [.NET `Decorator` class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.decorator?view=netframework-4.7.2).

<center>
<img src="img/modplus_scaling_in_dockable_panel.png" alt="ModPlus Dockable Panel text scaling issue" width="498">
</center>

This problem is apparently fixed in Revit 2019, so the solution is relevant for the preceding versions.

The `Decorator` class still has other uses as well, of course.


#### <a name="4"></a> Exporting View Image Extents

Starting to move away from the topic of scaling, but still vaguely related, some interesting conversations took place in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160)
on [how to get the extents when exporting an image from a view](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-extents-when-exporting-an-image-from-a-view-using/td-p/8656885),
again vaguely related
to [exporting images of all my doors](https://forums.autodesk.com/t5/revit-api-forum/export-images-of-all-my-doors/m-p/8655358).

If you have any questions about image export, check them out.


#### <a name="5"></a> Mesh Triangle Orientation

Two other discussions on the recurring topic of triangle orientation.

First, [about the normal of a mesh triangle](https://forums.autodesk.com/t5/revit-api-forum/about-the-normal-of-a-mesh-triangle/m-p/8546140):

**Question:** I am using a series of triangle meshes to form a polyhedron as an approximation of a very complicated solid.

Therefore, I triangulate each face of the solid with `face.Triangulate` and collect those triangle meshes. However, I found that the triangle meshes don't have normals. I am wondering how to get the correct normal for each triangle, that points outwards from the polyhedron.

**Answer:** Number each triangle's vertices clockwise from 1 to 3, then take something like (x3-x1).CrossProduct(x2-x1). Of course, this local numbering should be consistently enforced globally in the provided mesh data to fix the sign, otherwise you will have some pointing inward, some outward of your polyhedron.

**Response:** I am wondering how to determine the 'right' clockwise or counter-clockwise orientation of a triangle. As is shown in the figure below, the vertices of triangle 1 are arranged counterclockwise and the normal can be calculated correctly (right-hand rule is applied); however, those of triangle 2 are also arranged counterclockwise, but the normal is incorrect.

<center>
<img src="img/triangle_orientation.jpg" alt="Triangle orientation" width="428">
</center>

If every face of a solid is a planar face, that is not a problem because the triangles can directly get the face normal, but in my case, some faces are curved.
Therefore, it is not easy to get the right normal directions.

**Answer:** You can calculate the normal on any face with the `face.ComputeNormal` method:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;f;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;MeshPt;&nbsp;<span style="color:green;">//&nbsp;x1,&nbsp;x2&nbsp;or&nbsp;x3</span>
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;NormalFromPoints;&nbsp;<span style="color:green;">//&nbsp;(x3-x1).CrossProduct(x2-x1)</span>
&nbsp;&nbsp;<span style="color:#2b91af;">UV</span>&nbsp;UVpt&nbsp;=&nbsp;f.Project(&nbsp;MeshPt&nbsp;).UVPoint;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;face_normal&nbsp;=&nbsp;f.ComputeNormal(&nbsp;UVpt&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;surface_normal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;f.OrientationMatchesSurfaceOrientation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;face_normal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;face_normal.Negate();
 
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;MeshNormal&nbsp;=&nbsp;surface_normal
&nbsp;&nbsp;&nbsp;&nbsp;.DotProduct(&nbsp;NormalFromPoints&nbsp;)&nbsp;&gt;&nbsp;0&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;NormalFromPoints&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;NormalFromPoints.Negate();
</pre>

**Response:** I'm afraid I cannot use the method `OrientationMatchesSurfaceOrientation`, because I am working in the Revit 2017 API.

It was introduced as
a  [surface and face API enhancement](https://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html#3.23.12) in
Revit 2018.

I tested the code without that method and found that the normal calculated like this always returns the same direction as  `Face.Project`:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;tri&nbsp;=&nbsp;mesh.get_Triangle(&nbsp;I&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pt0&nbsp;=&nbsp;tri.get_Vertex(&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pt1&nbsp;=&nbsp;tri.get_Vertex(&nbsp;1&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pt2&nbsp;=&nbsp;tri.get_Vertex(&nbsp;2&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;vec1&nbsp;=&nbsp;pt1&nbsp;-&nbsp;pt0;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;vec2&nbsp;=&nbsp;pt2&nbsp;-&nbsp;pt0;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;normal&nbsp;=&nbsp;vec1.CrossProduct(&nbsp;vec2&nbsp;);
</pre>


#### <a name="6"></a> PlanarFace Normal

A closely related discussion asks about
the [opposition direction of Planarface.Normal](https://forums.autodesk.com/t5/revit-api-forum/opposition-direction-of-planerface-normal/m-p/8668274):

**Question:** How can I convert `PlanarFace.Normal` to opposite direction?

**Answer:** Do you just want to get the opposite direction?

If so, just write `-1` `*` `PlanarFace.Normal` or `PlanarFace.Normal.Negate()`.

However, if you want to flip the face, it is not possible.

Here are some related discussions:

- [Faces of the room from solid clockwise and counter-clockwise](https://forums.autodesk.com/t5/revit-api-forum/faces-of-the-room-from-solid-clockwise-and-counter-clockwise/m-p/7913404)
- [About the normal of a mesh triangle](https://forums.autodesk.com/t5/revit-api-forum/about-the-normal-of-a-mesh-triangle/m-p/8546140)
- [Incorrect face normal](https://forums.autodesk.com/t5/revit-api-forum/incorrect-face-normal/m-p/7108787)

I believe that, together, they answer all possible questions on face normals and mesh triangle orientation.

Note the tricky bits in the third discussion, explaining why you need to differentiate between:

- A picked face and the face returned by the element geometry.
- A face belonging to the symbol geometry (family definition coordinate space) and instance geometry (project coordinates).

Many thanks to @Revitalizer and @FAIR59 for their analysis and illuminating explanations in those threads!

