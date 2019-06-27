<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- img/adam_jeremy_luis_alvaro_petr_manrique_aecom_1200x733.jpg
  Among the participant are Álvaro Pérez, Manrique Gómez and Luis López of the [AECOM](https://www.aecom.com) BIM team in Madrid, who say:
  > We have been developing desktop solutions for Revit for years and are currently moving to the cloud and the Forge platform.
  > The experiences at this Forge accelerator in Barcelona are invaluable for us thanks to all the support from the Autodesk guys.
  Posing for us in the background, you can admire the Autodesk Forge table-tennis experts Adam and Petr.

- IFC exporter utilities enable you to add your own IFC related built-in parameters to elements
  how to [add an IFC_GUID BuiltInParameter to a beam that doesn't contain such parameter](https://forums.autodesk.com/t5/revit-api-forum/add-a-ifc-guid-builtinparameter-to-a-beam-that-doesn-t-contain/m-p/8870228)

twitter:

&ndash;
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

-->

### Room Closed Shell Issues in Forge

Three of main topics I explored here at the Forge accelerator:

- Room closed shell solid visibility in the Forge viewer
- Rebar simplification: replace rebar elements with simplified solids or model curves
- [glTF]() export

Today, I'll dive deeper into the first of these and one or two others:


####<a name="2"></a> IFC Exporter Utilities Add New Built-In Parameter

Before diving deeper into the Forge acceleratr topics, here is an interesting topic from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160).

The IFC exporter utilities enable you to add your own IFC related built-in parameters to elements.

This solution was discovered and shared by
... in
to [add an IFC_GUID BuiltInParameter to a beam that doesn't contain such a parameter](https://forums.autodesk.com/t5/revit-api-forum/add-a-ifc-guid-builtinparameter-to-a-beam-that-doesn-t-contain/m-p/8870228):

**Question:**

**Answer:**

<pre class="code">
</pre>


####<a name="3"></a> Barcelona Forge Accelerator

Traditionally, Thursday evening is the celebratory dinner together

<center>
<img src="img/.jpg" alt="Forge accelerator celebratory dinner in Marina Bay" width="10">
</center>

- group photo on stairs

The accelerator participants include Álvaro Pérez, Manrique Gómez and Luis López of
the [AECOM](https://www.aecom.com) BIM team in Madrid, who say:

> We have been developing desktop solutions for Revit for years and are currently moving to the cloud and the Forge platform.

> The experiences at this Forge accelerator in Barcelona are invaluable for us thanks to all the support from the Autodesk guys.

Posing for us in the background, you can also admire the table-tennis skills of Forge experts Adam and Petr:

<center>
<img src="img/adam_jeremy_luis_alvaro_petr_manrique_aecom_1200x733.jpg" alt="Forge accelerator celebratory dinner in Marina Bay" width="10">
</center>



####<a name="4"></a> Room Closed Shell in the Forge Viewer

I recently implemented an external command
that [creates `DirectShape` elements to represent room volumes](https://thebuildingcoder.typepad.com/blog/2019/05/generate-directshape-element-to-represent-room-volume.html).

The implementation was very simple, since the `Room.GetClosedShell` method returns a `GeometryElement` that can be passed straight into the direct shape `SetShape` method with no further ado.

Unfortunately, on further testing, we discovered that the resulting generic model direct shape elements do not show up as expected in the viewer.

This led to a lot of further research.

Apparently, the solid returned by the Revit API for the room closed shell is flawed in some way.

I explored a number of approaches to address this, tqo of which turned out to produce reliable results:

- Fix
- Triangulate face by face
- Recreate solid using `EdgeLoops` &ndash; orientation needs to be fixed
- Recreate solid using `...` &ndash; orientation needs to be fixed
- Triangulate entire solid with ...


####<a name="5"></a> Triangulate the Solid Face by Face

One reliable way to generate a valid solid to replace it is to query each face for its triangulation and use the triangular facets to define the direct shape intead, as described in

the blog post on ... flattening everything to direct shapes?

The following code achieves this:

<pre class="code">
</pre>

Unfortunately, all the triangle edges remain visible in the Revit model, even interior faces within a planar face:

<center>
<img src="img/.jpg" alt="Direct shape defined using triangles" width="10">
</center>


####<a name="6"></a> Triangulate Entire Solid with SolidUtils...

One serious problem may arise triangulating each face separately as shown above:

If two face meeting in a curved edge are independently triangulated, their respective tessellation of the shared edge may differ, resulting in triangle vertices on one face not matching the ones on the other, causing gaps in the resulting solid.

That can be solved only by triangukatng the entire solid in one fell swoop, which can be easily achieved using the `SolidUtils....` method as follows:

<pre class="code">
</pre>


####<a name="7"></a> ... Controls Accuacy Documentation Error

Documentation error: the accuracy is described as:

*The given value for accuracy must be greater than 0 and no more than 30000 feet*.

I specified 0.003, corresponding to ca. 0.9 mm ,and an exception was thrown.

Raising it to 0.006, a bit over 1.8 mm, it passed.

I assume the limit correlates witht the Revit minimum model line length limit, which is around 1/16th of an inch.

<pre>
  jc&gt; inch = 25.4

  jc&gt; foot = 12 * inch = 304.8

  jc&gt; minlen = inch / 16 = 1.5875

  jc&gt; accuracy = 0.003

  jc&gt; accuracy * foot = 0.9144

  jc&gt; 0.006 * foot = 1.8288
</pre>

