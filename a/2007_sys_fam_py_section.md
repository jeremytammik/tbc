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



Custom Family Type - BuiltIn Parameters

Hello
I am using Revit API 2023 to create a Family ( something similar to what is referred to in The Building Coder: SVG, In-Memory Family, RevitLookup BoundingBox (typepad.com)  ) .  Family is getting created just fine. Then I load that family and lastly I create a FamilyInstance of type StructuralColumn and StructuralFraming.  So far so good.

The problem I am facing is when I try to set the Offset related parameters e.g.  FAMILY_TOP_LEVEL_OFFSET_PARAM and FAMILY_BASE_LEVEL_OFFSET_PARAM . These are null when I try to retrieve them via   familyInstance.get_Parameter  before trying to set the values to it.

I am not sure if I have to specify something specific at the time of Family creation to let API know to associate the builtin Parameters to the Family or FamilyInstance.

Any help is much appreciated.
 Solved by RPTHOMAS108. Go to Solution.
Tags (0)
Add tags
Report
Labels (2)
CustomFamilyCreation  RevitApi
12 REPLIES
Sort:
MESSAGE 2 OF 13
Mohamed_Arshad
  Advocate Mohamed_Arshad  in reply to: rnilay81
‎2023-06-14 11:16 PM
HI @rnilay81
Are you trying to create FAMILY_TOP_LEVEL_OFFSET_PARAM and FAMILY_BASE_LEVEL_OFFSET_PARAM custom parameters to Built-In-Parameters ?
Thanks & Regards,
Mohamed Arshad K
Tags (0)
Add tags
Report
MESSAGE 3 OF 13
rnilay81
  Contributor rnilay81  in reply to: Mohamed_Arshad
‎2023-06-15 01:49 AM

Hi @Mohamed_Arshad

Thanks for the reply. These are not custom parameters. but BuiltinParmeters ( BuiltInParameter Enumeration (revitapidocs.com)) .As per the Revit Documentation I read that I can't create the BuiltinParameter because they are "BuiltIn"  ( Solved: Add built-in parameter to family - Autodesk Community - Revit Products)  .   Of course the link talks about adding Builtin Params to the existing family and the answer is NO, we can't.   But I want to know if we can add BuitlinParameter for newly created family through API.

For more details, below are the steps to give an idea as to why I want to have this functionality
1) I create the FamilyInstance and apply the offset.  Once the entire model is built with API in this way then
2) I read the model for some post processing in another use case.  As the normal Revit API flow is to read the offsets through FAMILY_TOP_LEVEL_OFFSET_PARAM and FAMILY_BASE_LEVEL_OFFSET_PARAM BuiltInParmeters, I need to have the values set to it at the time of creation of the model creation( Step 1 above) .

I can easily create the custom parameters and assign the values to it but the worry is that Revit won't be able to understand those parameters and won't be having the same offsetting effect on the 3D model.

Not sure what the way forward is.
Tags (0)
Add tags
Report
MESSAGE 4 OF 13
rnilay81
  Contributor rnilay81  in reply to: rnilay81
‎2023-08-23 10:37 PM
Hi Mohamed,

Thanks for helping me out. So I have tried to have these values SCHEDULE_LEVEL_PARAM
set . The problem I faced is when I try to do this through API, then the value SCHEDULE_LEVEL_PARAM
 is coming as ready only and can't be set..  Then I am trying with having different families for Columns ( Structural column family ) and for Beams ( Structural Beam & Truss family) and trying to set these values. Still trying these things and will update by today.
Tags (0)
Add tags
Report
MESSAGE 5 OF 13
jeremy.tammik
  Autodesk jeremy.tammik  in reply to: rnilay81
‎2023-08-23 10:56 PM

For an introduction to programmatically creating a family, please work through the Family API labs:

https://thebuildingcoder.typepad.com/blog/2009/08/the-revit-family-api.html

You will probably also find the other family creation articles useful:

https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.25.1

You cannot create built-in parameters. They are built in to Revit, hard-coded.

You can however create family parameters and make use of those to control the types and symbols generated by by your family definition.

Jeremy Tammik,  Developer Advocacy and Support, The Building Coder, Autodesk Developer Network, ADN Open
Tags (0)
Add tags
Report
MESSAGE 6 OF 13
Mohamed_Arshad
  Advocate Mohamed_Arshad  in reply to: rnilay81
‎2023-08-23 11:16 PM

HI @rnilay81
One thing you have understand. You're Using Structural Framing Category with Line Based Family so below parameters are must.
1. Curve
2. Level
3. Which Type of Family
4. Structural Type or Non Structural Type

By using this create New Family Instance by using the NewFamilyInstance Method, Then Access the below two parameter for offset from the level. SCHEDULE_LEVEL_PARAM can accessed at the time of creating Instance. Once Instance is created. It will not accessed.

BuiltInParameter: STRUCTURAL_BEAM_END0_ELEVATION
BuiltInParameter: STRUCTURAL_BEAM_END1_ELEVATION
Hope this will Helps 🙂
Thanks & Regards,
Mohamed Arshad K
Tags (0)
Add tags
Report
MESSAGE 7 OF 13
rnilay81
  Contributor rnilay81  in reply to: jeremy.tammik
‎2023-08-24 02:54 AM

Thanks @jeremy.tammik  for replying. I have followed these posts already to create the families . They are getting created fine. The place I am struggling is - I want my Column to have the constraints as "Base Level" and "Top Level" with values to be set as Levels from the model ( Snapshot is from Sample model and I want it similar for my own model through API )  .
rnilay81_0-1692870064097.png
As the existing values of the "Top Level" parameter such as SCHEDULE_TOP_LEVEL_PARAM I get from the existing templates is coming as readyonly, the values can not be set. So I tried to add the parameter through below method

fmgr.AddParameter("Top Level", GroupTypeId.Constraints,

but I am struggling to understand what the other parameter of ForgeTypeId would be for Type Level so that it would be displayed under Constraints when the model is ready . All the examples I get is either for ParameterType.Length or Material or Text. But I don't understand how to add parameter with type for ElementId as Level so that when I get the custom Parameter then it can be set as

baseLevelParam.Set(baseLevel.Id);

Basically I want to create the custom family parameter but want it to behave exactly like BuiltInParameter of base level and top level because they are readonly.

In the absence of any way to set base level and top level, my columns are just horizontally placed

rnilay81_0-1692871361091.png

instead of expected Vertical

rnilay81_1-1692871399162.png


Hope I am able to clearly put across the problem.
Tags (0)
Add tags
Report
MESSAGE 8 OF 13
rnilay81
  Contributor rnilay81  in reply to: Mohamed_Arshad
‎2023-08-24 03:06 AM
Hi @Mohamed_Arshad  for replying. Actually I am passing in the level at the time of creation of the instance as


fi = m_revit.ActiveUIDocument.Document.Create.NewFamilyInstance(point1, fs, topLevel, st);


where topLevel is of type Level .  st is StructuralType i.e. Column or Beam  ,  fs is family symbol , point1 is the starting point in the base level.

But unfortunately as I said in the above post, the columns are just coming in one level only ( Horizontal)  instead of  starting at base level and ending at top level (  Vertical ) .
Tags (0)
Add tags
Report
MESSAGE 9 OF 13
RPTHOMAS108
  Mentor RPTHOMAS108  in reply to: rnilay81
‎2023-08-24 04:16 PM

You can only change the values of built-in parameter not add them to a family (they exist or they don't depending on category).

I tend to use
FAMILY_BASE_LEVEL_PARAM
FAMILY_TOP_LEVEL_PARAM

They are not read-only
RPTHOMAS108_0-1692918336616.pngRPTHOMAS108_1-1692918378264.png

The associated schedule ones are also not read-only but I would not use those since they were added for schedules. It doesn't make sense to me to use the schedule ones when the intended family ones do exactly the same thing, have existing for longer and were obviously created for that purpose.

Families placed on a level such as columns point upwards from that level this is unlike in the UI where the column ends up below the view level you place it on. So your 'toplevel' notation indicates to me that you are setting the bottom of your column onto the top level and also setting the top of the column to that same level after placement.

To be honest that should trigger a warning i.e. the top and bottom level can't be the same.
RPTHOMAS108_2-1692918942890.pngRPTHOMAS108_3-1692918959936.png


Tags (0)
Add tags
Report
MESSAGE 10 OF 13
rnilay81
  Contributor rnilay81  in reply to: rnilay81
‎2023-08-28 04:35 AM

Hi @RPTHOMAS108 for replying. I tried to use the FAMILY_BASE_LEVEL_PARAM and FAMILY_TOP_LEVEL_PARAM in my case but they are coming as null. Then I tried to use the inbuilt families in Revit and for them these parameters are coming as not null. As a next step I tried to see the code to create the custom family from sample code under
Revit 2023.1 SDK\Samples\FamilyCreation\GenericModelCreation

and used that family in my code but for that as well these parameters FAMILY_BASE_LEVEL_PARAM etc. are coming as null. I have attached a reproducible case by combining the code from two samples given under Revit SDK ( CreateBeamsColumnsBraces and GenericModelCreation) . When I select the custom generated family in the dropdown of my solution, concerned parameters are always null but when I select the inbuilt families ( the ones we download/install from Revit ), these parameters are not null.

This leads me to believe that something is missing from the family creation code which is controlling which built-in parameters should be present in the instance.

Steps to reproduce the problem (  I have Revit 2023 and VS 2022)
1) Download and unzip the attached file
2) Run the solution from VS
3) Open any Revit file
4) Go to Add-Ins --> External Tools --> Create beams,columns and braces(CS)
5) From the "Type of Columns" dropdown , select "Generic Family"
( this is family we have created in the code i,e. custom family)
6) In the PlaceColumn method of CreateBeamsColumnsBraces.CS.Command  , you can see baseLevelParameter and topLevelParameter are null at Line No. 367 and 368.

Any help is much appreciated.

CreateRevitColumn.zip

Tags (0)
Add tags
Report
MESSAGE 11 OF 13
Mohamed_Arshad
  Advocate Mohamed_Arshad  in reply to: rnilay81
‎2023-08-28 05:31 AM
@rnilay81
Let me check and update you
Thanks & Regards,
Mohamed Arshad K
Tags (0)
Add tags
Report
MESSAGE 12 OF 13
RPTHOMAS108
  Mentor RPTHOMAS108  in reply to: rnilay81
‎2023-08-28 08:52 AM

If you are using the generic model template to create your column family then that isn't going to add those built-in parameters, even if you set the category.

That is the same as the UI.

To create structural columns you need to use
Metric Structural Column (or imperial equivalent)
Similarly to create a structural framing family you should use:
Metric Structural Framing - Beams and Braces (or imperial equivalent)
You should have an imperial and metric version of all your templates/content in reality (likewise versions with localised parameter names if you are localising).

So the problem is easier than you think i.e. if you don't see the built-in parameters in your family (when placed in the project) then you are using the wrong family template to create the family.

One of the most confusing things when people first interact with the Revit family environment is due to the incorrect assumption that any type of element functionality can come from the same RFT or RFA file. In reality the content of those templates is hard coded to suit a certain purpose and you can't change it to match another template type. In that respect it is a bit different to other programs where they would perhaps use a different file extension to note different template types.
Tags (0)
Add tags
Report
MESSAGE 13 OF 13
rnilay81
  Contributor rnilay81  in reply to: rnilay81
‎2023-08-29 05:18 AM
@RPTHOMAS108  Thanks for the advice. I have now integrated the structural column template and it is giving me non null parameters for top and base levels. I am at least able to move it ahead. Thanks a lot for all your help. Also, a big thanks to @Mohamed_Arshad @jeremy.tammik

/Users/jta/a/doc/revit/tbc/git/a/zip/create_revit_column.zip



####<a name="4"></a>

Pieter Lamoen raised and solved a question on how
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

