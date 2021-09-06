<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- eliminated deprecated API usage of `NewFloor` and `NewSlab` methods
  https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2022.0.151.4
  making use of
  the [Floor Creation API Clarification](https://thebuildingcoder.typepad.com/blog/2021/08/triangle-count-floor-and-slab-creation.html#3)
  [diff](https://github.com/jeremytammik/the_building_coder_samples/compare/2022.0.151.3...2022.0.151.4)

- Select all content on level
  https://forums.autodesk.com/t5/revit-api-forum/select-all-content-on-level/m-p/10577273

- why “Modify parameters” returns null for newly created Structural Connection?
  https://forums.autodesk.com/t5/revit-api-forum/why-modify-parameters-returns-null-for-newly-created-structural/m-p/10577688

- Get ViewSheet from View
  https://forums.autodesk.com/t5/revit-api-forum/get-viewsheet-from-view/m-p/10491156
  12966349 [Get ViewSheet from View]
  https://forums.autodesk.com/t5/revit-api-forum/get-viewsheet-from-view/m-p/7075550
  Getting Title Block Data and ViewSheet from View
  https://thebuildingcoder.typepad.com/blog/2020/02/get-title-block-data-and-viewsheet-from-view.html
  Get ViewSheet from a given View
  Just a snippet get_sheet_from_view(view) that will return you ViewSheet for a given View with the help of FilteredElementCollector and FilterStringRule.
  https://www.erikfrits.com/blog/get-viewsheet-from-given-view-with-filteredelementcollector-and-filterstringrule/

twitter:

add #thebuildingcoder

 @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

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

<pre class="code">
</pre>

-->

### View Sheet from View and Select Level

####<a name="2"></a> The Building Coder Samples Clean

Before diving into the other topics, I'll just mention in passing that the deprecated Revit API usage that remained after the initial migration to the Revit 2022 API has now been removed.

The deprecated API usage was caused by calls to the `NewFloor` and `NewSlab` methods.

The recent [floor creation API clarification](https://thebuildingcoder.typepad.com/blog/2021/08/triangle-count-floor-and-slab-creation.html#3) explained
how to address this,
and [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2022.0.151.4](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2022.0.151.4) implement
the fix, cf.
the [diff to the previous release](https://github.com/jeremytammik/the_building_coder_samples/compare/2022.0.151.3...2022.0.151.4).

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 719 -->
</center>

####<a name="3"></a>

Perry Lackowski jumped through several hoops
to [select all content on level](https://forums.autodesk.com/t5/revit-api-forum/select-all-content-on-level/m-p/10577273) and
very kindly documented his progress and re4sults to achieve this:

***Question:** I'm trying to put together a new script for a structural engineer.
He wants to be able to quickly see all the elements that are linked to a particular `Level` of his choosing.
I chose to break this into two parts:

- Take the first element in his selection and retrieve its Level.
- Find all elements with that reference Level and update the selection to match.

Step 1 was more challenging than I realized, because some elements like cable trays store the associated Level in the `ReferenceLevel` instead of in the `LevelId`.
Their `LevelId` returns `-1` or an invalid element id.

Step 2 is where I'm struggling now.
I use a `FilteredElementCollector` to find all matching LevelIds, but similar to step 1, this fails to include the cable trays in the resulting selection because they have a LevelId of -1.

**Answer:** Your explanation makes perfect sense, and also points towards the solution.

Just as you noticed in retrieving the level from the selected element, different elements store their level in different ways.
Unfortunately, some do not store any level information directly at all.
Those could be retrieved by determining their Z elevation and comparing that with the various level's Z coordinates.

Many elements provide a valid `LevelId` property, and you have used that property to retrieve the level from the selected element.

The cable trays apparently do not, and you have to use the `ReferenceLevel` property instead.

I assume that the `ElementLevelFilter` is also based on the `LevelId` property.
Therefore, it will not retrieve the desired cable trays.
For those, you can implement a second, separate, filtered element collector that first filters for cable trays, e.g., using their category or some other quick filter property.
In a post-processing step, you could check that the value of their `ReferenceLevel` property matches the desired value.

These two separate filtered element collectors can be combined into one using a Boolean operation.

I used this technique to put together such combinations of filters
to [retrieve structural elements](http://thebuildingcoder.typepad.com/blog/2010/07/retrieve-structural-elements.html)
and [MEP elements and their connectors](http://thebuildingcoder.typepad.com/blog/2010/06/retrieve-mep-elements-and-connectors.html), respectively.

You can check out The Building Coder topic group
on [filtering for elements](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.9) to
see many more examples.

**Response:** I didn't realize there was so much diversity in the way Revit handles the different type categories behind the scenes until I downloaded the RevitLookup snoop tool yesterday.
For example, I see that Duct and Cable Tray are broken out into separate Duct and CableTray Objects, and they both use Reference Levels.
But Duct Fittings and Cable Tray Fittings are saved under the same FamilyInstance object and they both use LevelIds.
Is there a guide/roadmap for how different type categories map to different database objects?
It sounds like I need to find out how each and every type category handles levels behind the scenes, then set up different filters to separate them ... which could be very time consuming.

Also (and perhaps this would be a better question to ask on the pyRevit forums) is there a better way to check for a null value on the `LevelId` parameter?
I feel like converting the result to a string is not the right solution here. 

Later: I'm beginning to wrap my head around the complexity of this problem.
From the items I have snooped so far, here are my results:

- Light Fixtures use Level (LevelId behind the scenes), unless they are face based, then they use Schedule Level (not in snoop tool, and LevelId is null).
- Cable Tray and Duct use Reference Level (Does appear in snoop tool, LevelId is null). 
- Model Groups use Reference Level (LevelId behind the scenes)
- Structural Columns use Base Level (LevelId behind the scenes)
- Structural Foundations use Level (LevelId behind the scenes)
- Structural Framing has a Reference Level (not in snoop tool, and LevelId is null).

Clearly, there's not a lot of consistency behind the scenes.
I figured I'd take a look at the parameters next, and maybe filter based on those.
Using the RevitAPI doc for `BuiltInParameter` enumeration, I'm seeing a lot of potential parameters that could house the level:

- Reference Level corresponds to MULTISTORY_STAIRS_REF_LEVEL, FABRICATION_LEVEL_PARAM, TRUSS_ELEMENT_REFERENCE_LEVEL_PARAM, GROUP_LEVEL, SPACE_REFERENCE_LEVEL_PARAM, RBS_START_LEVEL_PARAM, FACEROOF_LEVEL_PARAM, STRUCTURAL_REFERENCE_LEVEL_ELEVATION, ROOF_CONSTRAINT_LEVEL_PARAM, INSTANCE_REFERENCE_LEVEL_PARAM
- Base Level corresponds to DPART_BASE_LEVEL_BY_ORIGINAL, DPART_BASE_LEVEL, STAIRS_BASE_LEVEL, STAIRS_RAILING_BASE_LEVEL_PARAM, IMPORT_BASE_LEVEL, STAIRS_BASE_LEVEL_PARAM, VIEW_UNDERLAY_BOTTOM_ID, SCHEDULE_BASE_LEVEL_PARAM, ROOF_BASE_LEVEL_PARAM, FAMILY_BASE_LEVEL_PARAM
- Schedule Level corresponds to INSTANCE_SCHEDULE_ONLY_LEVEL_PARAM
- Level corresponds to PATH_OF_TRAVEL_LEVEL_NAME, SYSTEM_ZONE_LEVEL_ID, ZONE_LEVEL_ID, WALL_SWEEP_LEVEL_PARAM, ROOM_LEVEL_ID, SLOPE_ARROW_LEVEL_END, CURVE_LEVEL, VIEW_GRAPH_SCHED_BOTTOM_LEVEL, SCHEDULE_LEVEL_PARAM, LEVEL_PARAM, STRUCTURAL_REFERENCE_LEVEL_ELEVATION, STRUCTURAL_ATTACHMENT_START_LEVEL_REFERENCE, FAMILY_LEVEL_PARAM
- Associated Level corresponds to PLAN_VIEW_LEVEL

Admittedly, a lot of these built-in parameters sound like they correspond to families that I'm not looking for, but there are certainly quite a few contenders that may contain the information I need.
How would I filter for these?
I'm thinking I can create a list of all the parameters, then check for the parameters on each element and if I find a non-null parameter I can compare it to my selected level.
But this would be a very slow filter.
And how would I get a list of all the elements in the first place?

Later: OK, so I think I figured out the first part with this new method.
Basically, it just searches for every parameter in the list, and if it finds one that doesn't equal `-1`, it will return it as the Element Id of the corresponding level.
Next, I need to find a way to run this on every element in the project so I can compare the element ids.
This feels like the slowest, most brute-force way to accomplish this, so I'm open to alternatives...

Later: Latest version, basically complete!

I discovered that some levels can't be retrieved through the `get_Parameter` method &ndash; they only appear in the `LevelId` and `ReferenceLevel` properties.
But these methods don't exist for every element type, so I wrapped them in some Try/Except statements at the end of the level retrieval function.

I discovered a solution to the `-1` issue as well.
If you retrieve the element id and it's null, it means that level parameter doesn't exist for that object.
But, if you retrieve an element id that equals `-1`, that means the parameter exists, but was never set.
I believe the correct way to check for this is by comparing the element id to `ElementId.InvalidElementId`, like so:

<pre class="prettyprint">
  level_id.Compare(ElementId.InvalidElementId) == 1:
</pre>

I also added some options so you can select the starting element before or after launching the script.

Unfortunately, I was forced to make a list of all the categories I want to search through, since I haven't found an easier way to filter down the `FilteredElementCollector`.
I included maybe 30 of the morer than 1000 categories, but it's not an exhaustive list, and there's a possibility I missed a few important ones that I'll discover later.
I wish this page separated the 3D model categories from the rest, but alas.

<pre class="prettyprint">
"""
Selects all elements that share the same Reference Level as the selected element.

TESTED REVIT API: 2020.2.4

Author: Robert Perry Lackowski

"""

from Autodesk.Revit.DB import ElementLevelFilter, FilteredElementCollector
from Autodesk.Revit.DB import Document, BuiltInParameter, BuiltInCategory, ElementFilter, ElementCategoryFilter, LogicalOrFilter, ElementIsElementTypeFilter, ElementId
from Autodesk.Revit.Exceptions import OperationCanceledException
# from pyrevit import DB
doc = __revit__.ActiveUIDocument.Document
uidoc = __revit__.ActiveUIDocument

from rpw import ui
import sys

#Ask user to pick an object which has the desired reference level
def pick_object():
  from Autodesk.Revit.UI.Selection import ObjectType
  
  try:
    picked_object = uidoc.Selection.PickObject(ObjectType.Element, "Select an element.")
    if picked_object:
      return doc.GetElement(picked_object.ElementId)
    else:
      sys.exit()
  except:
    sys.exit()
  
def get_level_id(elem):
  
  BIPs = [
    BuiltInParameter.CURVE_LEVEL,
    BuiltInParameter.DPART_BASE_LEVEL_BY_ORIGINAL,
    BuiltInParameter.DPART_BASE_LEVEL,
    # BuiltInParameter.FABRICATION_LEVEL_PARAM,
    BuiltInParameter.FACEROOF_LEVEL_PARAM,
    BuiltInParameter.FAMILY_BASE_LEVEL_PARAM,
    BuiltInParameter.FAMILY_LEVEL_PARAM,
    BuiltInParameter.GROUP_LEVEL,
    BuiltInParameter.IMPORT_BASE_LEVEL,
    BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM,
    BuiltInParameter.INSTANCE_SCHEDULE_ONLY_LEVEL_PARAM,
    BuiltInParameter.LEVEL_PARAM,
    BuiltInParameter.MULTISTORY_STAIRS_REF_LEVEL,
    BuiltInParameter.PATH_OF_TRAVEL_LEVEL_NAME,
    BuiltInParameter.PLAN_VIEW_LEVEL,
    # BuiltInParameter.RBS_START_LEVEL_PARAM,
    BuiltInParameter.ROOF_BASE_LEVEL_PARAM,
    BuiltInParameter.ROOF_CONSTRAINT_LEVEL_PARAM,
    BuiltInParameter.ROOM_LEVEL_ID,
    BuiltInParameter.SCHEDULE_BASE_LEVEL_PARAM,
    BuiltInParameter.SCHEDULE_LEVEL_PARAM,
    BuiltInParameter.SLOPE_ARROW_LEVEL_END,
    # BuiltInParameter.SPACE_REFERENCE_LEVEL_PARAM,
    BuiltInParameter.STAIRS_BASE_LEVEL,
    BuiltInParameter.STAIRS_BASE_LEVEL_PARAM,
    BuiltInParameter.STAIRS_RAILING_BASE_LEVEL_PARAM,
    BuiltInParameter.STRUCTURAL_REFERENCE_LEVEL_ELEVATION,
    BuiltInParameter.SYSTEM_ZONE_LEVEL_ID,
    BuiltInParameter.TRUSS_ELEMENT_REFERENCE_LEVEL_PARAM,
    BuiltInParameter.VIEW_GRAPH_SCHED_BOTTOM_LEVEL,
    BuiltInParameter.VIEW_UNDERLAY_BOTTOM_ID,
    BuiltInParameter.WALL_BASE_CONSTRAINT,
    BuiltInParameter.WALL_SWEEP_LEVEL_PARAM
    # BuiltInParameter.ZONE_LEVEL_ID,
  ]
  
  level_id = None
    
  for BIP in BIPs:
    param = elem.get_Parameter(BIP)
    if param:
      # print "A common level parameter has been found:" + str(BIP)
      param_elem_id = param.AsElementId()
      if param_elem_id.Compare(ElementId.InvalidElementId) == 1:
        level_id = param_elem_id
        # print "match found on common level parameter " + str(BIP) + "Level ID: " + str(level_id)
        return level_id
  
  # print "No matching common level parameters found, checking for .LevelId"
  try:
    level_id = elem.LevelId
    if level_id.Compare(ElementId.InvalidElementId) == 1:
      # print "match found on .LevelId. Level ID: " + str(level_id)
      return level_id
  except:
    # print "No LevelId parameter on this element."
    pass

  # print "Still no matches. Try checking for .ReferenceLevel.Id"
  
  try:
    level_id = elem.ReferenceLevel.Id
    if level_id.Compare(ElementId.InvalidElementId) == 1:
      # print "match found on .ReferenceLevel.Id Level ID: " + str(level_id)      
      return level_id
  except:
    # print "No ReferenceLevel parameter on this element."
    pass
  
  # print "No matches found. Returning None..."
  return None

# print "get selected element, either from current selection or new selection"
selection = ui.Selection()

if selection:
  selected_element = selection[0]
else:
  selected_element = pick_object()

#print "Element selected: " + selected_element.Name

# print "Search selected element for its reference level's element ID"
target_level_id = get_level_id(selected_element)
# print target_level_id

if target_level_id is not None:
  
  #poor attempts at filtering FECs. Not filtered enough - they contain far too many elements.
  #all_elements = FilteredElementCollector(doc).ToElements()
  #all_elements = FilteredElementCollector(doc).WherePasses(LogicalOrFilter(ElementIsElementTypeFilter( False ), ElementIsElementTypeFilter( True ) ) ).ToElements()
  
  #Create a filter. If this script isn't selecting the elements you want, it's possible the category needs to be added to this list.
  BICs = [
    BuiltInCategory.OST_CableTray,
    BuiltInCategory.OST_CableTrayFitting,
    BuiltInCategory.OST_Conduit,
    BuiltInCategory.OST_ConduitFitting,
    BuiltInCategory.OST_DuctCurves,
    BuiltInCategory.OST_DuctFitting,
    BuiltInCategory.OST_DuctTerminal,
    BuiltInCategory.OST_ElectricalEquipment,
    BuiltInCategory.OST_ElectricalFixtures,
    BuiltInCategory.OST_FloorOpening,
    BuiltInCategory.OST_Floors,
    BuiltInCategory.OST_FloorsDefault,
    BuiltInCategory.OST_LightingDevices,
    BuiltInCategory.OST_LightingFixtures,
    BuiltInCategory.OST_MechanicalEquipment,
    BuiltInCategory.OST_PipeCurves,
    BuiltInCategory.OST_PipeFitting,
    BuiltInCategory.OST_PlumbingFixtures,
    BuiltInCategory.OST_RoofOpening,
    BuiltInCategory.OST_Roofs,
    BuiltInCategory.OST_RoofsDefault,
    BuiltInCategory.OST_SpecialityEquipment,
    BuiltInCategory.OST_Sprinklers,
    BuiltInCategory.OST_StructuralStiffener,
    BuiltInCategory.OST_StructuralTruss,
    BuiltInCategory.OST_StructuralColumns,
    BuiltInCategory.OST_StructuralFraming,
    BuiltInCategory.OST_StructuralFramingSystem,
    BuiltInCategory.OST_StructuralFramingOther,
    BuiltInCategory.OST_StructuralFramingOpening,
    BuiltInCategory.OST_StructuralFoundation,
    BuiltInCategory.OST_Walls,
    BuiltInCategory.OST_Wire,
  ]
  
  category_filters = []
  
  for BIC in BICs:
    category_filters.Add(ElementCategoryFilter(BIC))
  
  final_filter = LogicalOrFilter(category_filters)
  
  #Apply filter to create list of elements
  all_elements = FilteredElementCollector(doc).WherePasses(final_filter).WhereElementIsNotElementType().WhereElementIsViewIndependent().ToElements()

  # print "Number of elements that passed collector filters:" + str(len(all_elements))

  selection.clear()

  for elem in all_elements:
    elem_level_id = get_level_id(elem)
    if elem_level_id == target_level_id:
      selection.add(elem)

  selection.update()
  
else:
  
  print "No level associated with element."
</pre>

Many thanks to Perry for all his research and documentation of this work.

####<a name="4"></a> 

####<a name="5"></a> 
