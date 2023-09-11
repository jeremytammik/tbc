<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- determine is family system or not?
  https://autodesk.slack.com/archives/C0SR6NAP8/p1691697108465579
  [Q] Given a Family in the API what is the most straight forward approach to determining if the family is a system family or not in C#?
  [A] Jacob Small

- you need a column family to define a column family
  Custom Family Type - BuiltIn Parameters
  https://forums.autodesk.com/t5/revit-api-forum/custom-family-type-builtin-parameters/m-p/12203945

- Create section with Revit API, Python
  creates a section that looks at a window from the outside
  https://forums.autodesk.com/t5/revit-api-forum/create-section-with-revit-api-python/m-p/12211534


twitter:

 @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS


&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### System Family Predicate and Python Section Creation


####<a name="2"></a> System Family Predicate

Jacob Small and Ivan Dobrianov suggest a efficient and simple solution to identify system families:

**Question:** Given a family, what is the most straight-forward approach to determine programmatically whether it is a system family or not in the C# Revit API?

**Answer:** I would try to use a combination of the `IsEditable` and `IsInPlace` properties.
`IsEditable` will let you know if the family can be saved, which means it isn't a system family.
It would however return a false positive for in-place families.
`IsInPlace` will cover the in-place families:

- [IsEditable](https://www.revitapidocs.com/2024/d7d3ef05-d2bd-b770-47df-96b7fd280f9f.htm)
- [IsInPlace](https://www.revitapidocs.com/2024/eb138fd5-6092-5257-e6e1-073013cb8582.htm

**Question:**  A meaningful related question:
Given an `ElementType`, does it specify a system family or a real, user-defined RFA-based family?

**Answer:** To answer that, you can try to cast it to a `FamilySymbol`.
If that fails, it refers to a system family.
If it succeeds, you can read he `Family` property, which is a `Family` object that you can interrogate further for in-place, editability, etc.

Thank you for that, Jacob and Ivan.

####<a name="3"></a> Level-Based Family Requires Appropriate Stuff

Another family related discussion explains the need to

Category
Template

- you need a  column family to define a column family
Custom Family Type - BuiltIn Parameters
https://forums.autodesk.com/t5/revit-api-forum/custom-family-type-builtin-parameters/m-p/12203945


####<a name="4"></a>

Pieter Lamoenraised and solved a question on how
to [create section with Revit API and Python](https://forums.autodesk.com/t5/revit-api-forum/create-section-with-revit-api-python/m-p/12211534)
that creates a section looking at a window from the outside:

**Question:** I'm trying to create a section with Python.
Creating the section is easy.
Is there a simple way to change the oriëntation?
By default, the section looks down; I would like it to look forward, like a normal section, e.g., `XYZ(0,1,0)`.
How can I acieve this?
I only know Python and no C#.

**Answer:** I love Python. C# is also very nice.
Most of my Revit API samples are in C#.
The Building Coder includes a whole topic group
on [articles on how to set up section views](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.38).

**Response:**  First of all thanks for all the sample code!
I came up with the idea to use chatGPT to translate your code to Python and then changed it to fit my needs.
My code now creates a section that looks at a window from the outside.
This code gets the job done:

<pre class="prettyprint">
def raamstaat_sections(window):
  # find window dimensions, center and host(wall)
  window_bb = window.get_BoundingBox(doc.ActiveView)
  window_center = (window_bb.Min + window_bb.Max)/2
  window_width = window.LookupParameter("Width").AsDouble()
  window_height = window.LookupParameter("Height").AsDouble()
  window_sill_height = window.LookupParameter("Sill Height").AsDouble()
  host = window.Host

  # vectors for view direction
  wall_ext = host.Orientation
  wall_direction = XYZ(wall_ext.Y, -wall_ext.X, wall_ext.Z)
  up_direction = XYZ.BasisZ
  wall_int = XYZ(-wall_ext.X, -wall_ext.Y, wall_ext.Z)

  ### BOUNDING BOX FOR ELEVATION
  # Calculate the bounding box
  min_point_elev = XYZ((-window_width/2) - 500/304.8, (-window_height/2) - window_sill_height - 750/304.8, -1000/304.8)
  max_point_elev = XYZ((window_width/2) + 500/304.8, (window_height/2) + 1000/304.8, 0)

  # create 'rotated' bounding box
  rotation_transform_elev = Transform.Identity
  rotation_transform_elev.Origin = window_center
  rotation_transform_elev.BasisX = wall_direction
  rotation_transform_elev.BasisY = up_direction
  rotation_transform_elev.BasisZ = wall_int

  rotated_bounding_box_elev = BoundingBoxXYZ()
  rotated_bounding_box_elev.Transform = rotation_transform_elev
  rotated_bounding_box_elev.Min = min_point_elev
  rotated_bounding_box_elev.Max = max_point_elev

  ### CREATE SECTION
  # create sections, change name, apply view template
  t = Transaction(doc, "create section")
  t.Start()

  type = window.LookupParameter("S3A_Type").AsString()
  #elevation
  newViewElev = ViewSection.CreateSection(doc, SectionTypes[1].Id, rotated_bounding_box_elev)
  newViewElev.Name = type + " - aanzicht TEST"
  newViewElev.ViewTemplateId = templateS.Id

  t.Commit()
</pre>

**Answer:** Great! Congratulations!
Thank you for the interesting point that ChatGPT was useful in translating the code.

Many thanks to Pieter for raising this and sharing his nice solution and coding approach.




####<a name="4"></a>

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 353 Pixel Width: 974 -->
</center>

