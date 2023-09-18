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

Shed some light on family and section view creation, a system family predicate, level-based family template and creating section view in Python with @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS https://autode.sk/pythonsection

Today, we shed some light on family and section view creation
&ndash; System family predicate
&ndash; Level-based family template
&ndash; Create section view in Python...

linkedin:

Shed some light on family and section view creation, a system family predicate, level-based family template and creating section view in Python with #RevitAPI

https://autode.sk/pythonsection

- System family predicate
- Level-based family template
- Create section view in Python...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### System Family Predicate and Python Section Creation

Today, we shed some light on family and section view creation:

- [System family predicate](#2)
- [Level-based family template](#3)
- [Create section view in Python](#4)

####<a name="2"></a> System Family Predicate

Jacob Small and Ivan Dobrianov suggest an efficient and simple solution to identify system families:

**Question:** Given a family, what is the most straight-forward approach to determine programmatically whether it is a system family or not in the C# Revit API?

**Answer:** I would try to use a combination of the `IsEditable` and `IsInPlace` properties.
`IsEditable` will let you know if the family can be saved, which means it isn't a system family.
It would however return a false positive for in-place families.
`IsInPlace` will cover the in-place families:

- [IsEditable](https://www.revitapidocs.com/2024/d7d3ef05-d2bd-b770-47df-96b7fd280f9f.htm)
- [IsInPlace](https://www.revitapidocs.com/2024/eb138fd5-6092-5257-e6e1-073013cb8582.htm)

**Question:**  A meaningful related question:
Given an `ElementType`, does it specify a system family or a real, user-defined RFA-based family?

**Answer:** To answer that, you can try to cast it to a `FamilySymbol`.
If that fails, it refers to a system family.
If it succeeds, you can read the `Family` property, which is a `Family` object that you can interrogate further for in-place, editability, etc.

Thank you for that, Jacob and Ivan.

####<a name="3"></a> Level-Based Family Template

In another family-related discussion
on [custom family type built-in parameters](https://forums.autodesk.com/t5/revit-api-forum/custom-family-type-builtin-parameters/m-p/12203945),
Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
explains the need to use the appropriate family definition template in order to obtain the built-in properties required to attach to bottom and top levels, for instance.
To create structural columns, you need to use the *Metric Structural Column* (or its imperial equivalent).
Similarly, to create a structural framing family, you should use the *Metric Structural Framing - Beams and Braces* (or imperial equivalent).
In detail:

**Question:** I am using Revit API 2023 to create a family.
The family is getting created just fine.
Then I load that family and create a `FamilyInstance` of type `StructuralColumn` and `StructuralFraming`.
So far, so good.

The problem I am facing is when I try to set the offset related parameters, e.g., `FAMILY_TOP_LEVEL_OFFSET_PARAM` and `FAMILY_BASE_LEVEL_OFFSET_PARAM`.
These are null when I try to retrieve them via `get_Parameter`.

These are not custom parameters, but built-in.
I read that [I can't create a built-in parameter because it is built-in](https://forums.autodesk.com/t5/revit-api-forum/add-built-in-parameter-to-family/td-p/10674669).
It talks about adding them to an existing family and the answer is NO, we can't.
But I want to know if I can add them to a newly created family through API.

**Answer:** You can only change the values of a built-in parameter, not add it to a family; they either exist or they don't, depending on the category.

I tend to use

- FAMILY_BASE_LEVEL_PARAM
- FAMILY_TOP_LEVEL_PARAM

They are not read-only:

<center>
<img src="img/column_family_level_4.png" alt="Column family properties" title="Column family properties" width="650"/>
<p style="font-size: 80%; font-style:italic">FAMILY_TOP_LEVEL_PARAM</p>
<br/>
<img src="img/column_family_level_5.png" alt="Column family properties" title="Column family properties" width="650"/>
<p style="font-size: 80%; font-style:italic">FAMILY_BASE_LEVEL_PARAM</p>
</center>

The associated schedule ones are also not read-only but I would not use those since they were added for schedules.
It doesn't make sense to me to use the schedule ones when the intended family ones do exactly the same thing, have existing for longer and were obviously created for that purpose.

Families placed on a level such as columns point upwards from that level this is unlike in the UI where the column ends up below the view level you place it on.
So, your `toplevel` notation indicates to me that you are setting the bottom of your column onto the top level and also setting the top of the column to that same level after placement.

To be honest, that should trigger a warning, i.e., the top and bottom level can't be the same:

<center>
<img src="img/column_family_level_6.png" alt="Column family properties" title="Column family properties" width="650"/>
<p style="font-size: 80%; font-style:italic">LevelId</p>
<br/>
<img src="img/column_family_level_7.png" alt="Column family properties" title="Column family properties" width="450"/>
<p style="font-size: 80%; font-style:italic">Level constraints</p>
</center>

**Response:**  I tried to use the FAMILY_BASE_LEVEL_PARAM and FAMILY_TOP_LEVEL_PARAM in my case but they are coming as null.
Then I tried to use the inbuilt families in Revit and for them these parameters are coming as not null.
As a next step I tried to see the Revit SDK sample code from GenericModelCreation to create the custom family and used that family in my code but for that as well these parameters FAMILY_BASE_LEVEL_PARAM etc. are coming as null.
I believe that something is missing from the family creation code which is controlling which built-in parameters should be present in the instance.

**Answer:** If you are using the generic model template to create your column family, then that isn't going to add those built-in parameters, even if you set the category.

That is the same as the UI.

To create structural columns, you need to use the *Metric Structural Column* (or imperial equivalent).
Similarly, to create a structural framing family you should use *Metric Structural Framing - Beams and Braces* (or imperial equivalent).
You should have an imperial and metric version of all your templates/content in reality (likewise versions with localised parameter names if you are localising).

So, the problem is easier than you think, i.e., if you don't see the built-in parameters in your family (when placed in the project) then you are using the wrong family template to create the family.

One of the most confusing things when people first interact with the Revit family environment is due to the incorrect assumption that any type of element functionality can come from the same RFT or RFA file.
In reality, the content of those templates is hard-coded to suit a certain purpose and you can't change it to match another template type.
In that respect it is a bit different to other programs where they would perhaps use a different file extension to note different template types.

**Response:** Thanks for the advice.
I have now integrated the structural column template and it is giving me non-null parameters for top and base levels.
I am at least able to move it ahead.

Many thanks to Richard for all his help.

####<a name="4"></a> Create Section View in Python

Pieter Lamoen raised and solved a question on how
to [create section with Revit API and Python](https://forums.autodesk.com/t5/revit-api-forum/create-section-with-revit-api-python/m-p/12211534)
that creates and orients a section looking at a window from the outside:

**Question:** I'm trying to create a section with Python.
Creating the section is easy.
Is there a simple way to change the orientation?
By default, the section looks down; I would like it to look forward, like a normal section, e.g., `XYZ(0,1,0)`.
How can I achieve this?
I only know Python and no C#.

**Answer:** I love Python. C# is also very nice.
Most of my Revit API samples are in C#.
The Building Coder includes a whole topic group
on [articles on how to set up section views](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.38).

**Response:**  First of all, thanks for all the sample code!
I came up with the idea to use ChatGPT to translate your code to Python and then changed it to fit my needs.
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

**Answer:** Great!
Congratulations!
Thank you for the interesting point that ChatGPT was useful in translating the code.

Many thanks to Pieter for raising this and sharing his nice solution and coding approach.

