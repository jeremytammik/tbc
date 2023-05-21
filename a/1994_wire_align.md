<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Electrical Wire not found in BoundingBoxIsInsideFilter
  https://forums.autodesk.com/t5/revit-api-forum/electrical-wire-not-found-in-boundingboxisinsidefilter/m-p/11938583
  ricaun created the BoundingBoxViewIntersectsFilter and BoundingBoxViewIsInsideFilter:
  https://gist.github.com/ricaun/14ec0730e7efb3cc737f2134475e2539

- https://forums.autodesk.com/t5/revit-api-forum/visualizing-circuits-in-3d/td-p/11937368

- align two elements:
  How to use the Alignment method for family Instance
  https://forums.autodesk.com/t5/revit-api-forum/how-to-use-the-alignment-method-using-for-family-instance/m-p/11938454

twitter:

Aligning two elements for a constraint and two electrical wire issues, filtering and visualising circuits in 3D in the @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS https://autode.sk/find_see_wire

Two challenging electrical wire issues addressed by @ricaun and a recurring question on element alignment for defining a constraint
&ndash; Bounding box filter for wires
&ndash; Visualizing circuits in 3D
&ndash; Aligning two elements...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Aligning Elements, Finding and Visualising Wires

Two challenging electrical wire issues addressed by
Luiz Henrique [@ricaun](https://github.com/ricaun) Cassettari and
a recurring question on element alignment for defining a constraint:

- [Bounding box filter for wires](#2)
- [Visualizing circuits in 3D](#3)
- [Aligning two elements](#4)

####<a name="2"></a> Bounding Box Filter for Wires

Luiz shared a solution for the task of retrieving electrical wires within a bounding box in the thread
on [electrical wire not found in `BoundingBoxIsInsideFilter`](https://forums.autodesk.com/t5/revit-api-forum/electrical-wire-not-found-in-boundingboxisinsidefilter/m-p/11938583):

**Question:** I am currently trying to locate electrical wires that exist within a dependent view's bounding box. However, I have not been successful in using methods such as BoundingBoxIsInsideFilter, BoundingBoxIntersectsFilter, or VisibleInViewFilter.

In order to obtain a wire's bounding box, I had to use `wire.get_BoundingBox`, passing in the view it exists on, to get any value for its location.
Despite following Jeremy's example in message #4 in the thread
on how to [check to see if a point is inside bounding box](https://forums.autodesk.com/t5/revit-api-forum/check-to-see-if-a-point-is-inside-bounding-box/m-p/4354446),
I am still unable to get any wires into my list.

<center>
<img src="img/electrical_wire_collector.png" alt="Electrical wire collector" title="Electrical wire collector" width="800"/> <!-- Pixel Height: 534 Pixel Width: 1,757 -->
<p style="font-size: 80%; font-style:italic">Electrical wire collector</p>
</center>

**Answer:** I am not sure whether the filtered element collectors are ever able to take wire geometry into account.
Do your wires have real geometry, e.g., a curve and a location?
The filtered element collectors only deal with BIM elements and BIM element geometry.
If your wires have valid geometry, it may not be recognised by them a valid BIM element geometry,
so you will have to treat is as abstract pure non-BIM geometry and use other means than the filtered element collectors to retrieve it.
So, yes, using a pure geometry bounding box sounds like a good way to go.
Just be clear that this is completely separate from filtered element collectors.

**Response:** Yes sir they do have a curve and location, I am able to find them just fine through the `FilteredElementCollector` for each View, except on a Dependent View.

The Dependent View will show all wires of the Primary View not just what is within its Crop Region.
Took a few iterations but I finally found how to get the actual Crop Region of the Dependent View,
the first few ways I tried gave me the Primary Views Crop Region, so I ended up having to get the
extents from the BuiltInParameter.

I also tried to extend my Z from that 1000' in either direction and it still did not show up,
but if I invert any of those three filters, it finds all the wires of the Primary View even
including the handful inside the Crop Region which shouldn't with it inverted.

<!--
This is the extents of the Dependent Views Crop Region:

MikeM615_3-1682684885657.png

And these are the wires within it:

MikeM615_4-1682684901736.pngMikeM615_5-1682684903736.pngMikeM615_6-1682684905452.png
-->

Everything at face value looks like the filters should work, it just seems I am missing something simple.
Here is the section of code I used just to produce those values, but the `BoundingBoxIsInsideFilter` is failing on:

<pre class="prettyprint">
  private List&lt;Wire&gt; WireCollector( ForEachView viewPlan )
  {
    List&lt;Wire&gt; wireCollector
      = new FilteredElementCollector( _Doc, viewPlan.Id )
        .OfCategory( BuiltInCategory.OST_Wire )
        .WhereElementIsNotElementType( )
        .OfType&lt;Wire&gt;( )
        .ToList( );

    // If view is dependent view, filter wires by bounding box

    if ( viewPlan.PrimaryPlan != ElementId.InvalidElementId )
    {
      // Set Z value to 1 ft above and below level

      XYZ viewBoundingBoxMin = new XYZ(
        viewPlan.ViewBoundingBox.Min.X,
        viewPlan.ViewBoundingBox.Min.Y, -1 );
      XYZ viewBoundingBoxMax = new XYZ(
        viewPlan.ViewBoundingBox.Max.X,
        viewPlan.ViewBoundingBox.Max.Y,  1 );

      // Create Outline and BoundingBox

      Outline outline = new Outline( viewBoundingBoxMin,
        viewBoundingBoxMax );
      BoundingBoxIsInsideFilter boundingBoxIsInsideFilter
        = new BoundingBoxIsInsideFilter( outline );

      // Test Value Reporting

      TaskDialog.Show( "Wire Export", viewBoundingBoxMin.ToString( )
        + " Min " + viewBoundingBoxMax.ToString( ) + " Max " );

      // Add elements to new list if passes filter

      // Test Value Reporting

      foreach ( Wire wire in wireCollector)
      {
        TaskDialog.Show( "Wire Export", wire.get_BoundingBox(
            viewPlan.CropRegionElement ).Min.ToString( )
          + " Min " + wire.get_BoundingBox(
            viewPlan.CropRegionElement ).Max.ToString( )
          + "Max" );
      }

      List&lt;Wire&gt; filteredWires = wireCollector
        .Where( w =&gt; boundingBoxIsInsideFilter.PassesFilter(w) )
        .ToList( );

      // Test Value Reporting

      TaskDialog.Show( "Wire Export",
        $"There are {filteredWires.Count} wires in the view {viewPlan.Name}." );
      return filteredWires;
    }
    else
    {
      // Test Value Reporting

      TaskDialog.Show( "Wire Export",
        $"There are {wireCollector.Count} wires in the view {viewPlan.Name}." );
      return wireCollector;
    }
  }
</pre>

**Answer:** I'm not sure the bounding box filter works with 2d elements that are owned by a view.

I guess in the bound box filter implementation, the filter tries to get the bound box of the element without a view, like `element.get_BoundingBox(null)`, and the result would be null, resulting in a PassesFilter to false.

I guess the only way would be to create your own filter and add a View to use in the comparison (Gonna be a slow filter to use with Linq).

I created a [Revit API Filter for BoundingBox element in a View](https://gist.github.com/ricaun/14ec0730e7efb3cc737f2134475e2539) with
`BoundingBoxViewIntersectsFilter`, `BoundingBoxViewIsInsideFilter` and a command to test them:

- [BoundingBoxViewIntersectsFilter.cs](https://gist.github.com/ricaun/14ec0730e7efb3cc737f2134475e2539#file-boundingboxviewintersectsfilter-cs)
- [BoundingBoxViewIsInsideFilter.cs](https://gist.github.com/ricaun/14ec0730e7efb3cc737f2134475e2539#file-boundingboxviewisinsidefilter-cs)
- [CommandWireIsInside.cs](https://gist.github.com/ricaun/14ec0730e7efb3cc737f2134475e2539#file-commandwireisinside-cs)

Here is a code sample to test; I use a big tolerance to force the `PassesFilter` to return true.

<pre class="prettyprint">
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using System.Linq;

namespace RevitAddin.Commands
{
  [Transaction(TransactionMode.Manual)]
  public class CommandWireIsInside : IExternalCommand
  {
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elementSet)
    {
      UIApplication uiapp = commandData.Application;

      UIDocument uidoc = uiapp.ActiveUIDocument;
      Document document = uidoc.Document;
      View view = uidoc.ActiveView;

      var wires = new FilteredElementCollector(document)
          .OfCategory(BuiltInCategory.OST_Wire)
          .WhereElementIsNotElementType()
          .OfType&lt;Wire&gt;()
          .ToList();

      System.Console.WriteLine($"Wires: {wires.Count}");

      var tolerance = 1e3;
      var viewBox = view.CropBox;
      var outline = new Outline(viewBox.Min, viewBox.Max);
      var boundingBoxFilter = new BoundingBoxViewIsInsideFilter(
        outline, view, tolerance);

      var wiresBox = wires
          .Where(boundingBoxFilter.PassesFilter)
          .ToList();

      System.Console.WriteLine($"WiresBox: {wiresBox.Count}");

      TaskDialog.Show(
        $"Wires: {wires.Count}",
        $"WiresBox: {wiresBox.Count}");

      return Result.Succeeded;
    }
  }
}
</pre>

**Response:** Thank you @ricaun, that makes a lot of sense and matches what I was seeing in my results and assumptions;
I definitely got stuck on the problem and couldn't think of a next step at all in the moment!

Thank you for the examples, that is exactly what I needed!

####<a name="3"></a> Visualizing Circuits in 3D

Luiz also shared advice
on [visualizing circuits in 3D](https://forums.autodesk.com/t5/revit-api-forum/visualizing-circuits-in-3d/td-p/11937368):

**Question:** I'm trying to create a script that will help me better visualize circuits in 3D.
Basically, you select one or more electrical panels, and temporary generic model lines will get
drawn in between all the elements in each circuit on the panel.
The problem is that a single circuit may have 10 or more receptacles, and I don't want a line
going from the panelboard to each receptacle &ndash; I'd like to show them daisy-chained together
like when you create a circuit.
I set up a nearest-neighbor algorithm and a minimum spanning tree algorithm, but neither really
matches the results that you'd see if you tab-select a circuit and add wiring.
Here's my current implementation in pyRevit:

<pre class="prettyprint">
""" Visualize Connected Circuits

Author: Perry Lackowski
Tested: 2021.1
"""

import sys

from pyrevit import DB, forms, revit, script
from Autodesk.Revit.UI.Selection import ObjectType
from Autodesk.Revit.UI.Selection import ISelectionFilter

from enum import Enum
from rpw import ui

doc = revit.doc
uidoc = revit.uidoc
output = script.get_output()

selectable_categories = [
  int(DB.BuiltInCategory.OST_ElectricalEquipment),
  int(DB.BuiltInCategory.OST_ElectricalFixtures)
]

def UI_get_equipment():
  selected_equipment_ids = uidoc.Selection.GetElementIds()

  # If pre-selection, convert ids to elements, and remove anything that
  # isn't electrical equipment.
  if selected_equipment_ids:
    selected_equipment = [doc.GetElement(eId) for eId in selected_equipment_ids]
    selected_equipment = [x for x in selected_equipment if x.Category.Id.IntegerValue in selectable_categories]
    use_preselected_elements = forms.alert(msg='You currently have {} elements selected. Do you want to proceed with the currently selected item(s)?'.format(len(selected_equipment)),ok=False,yes=True, no=True)
    if use_preselected_elements:
      return selected_equipment

  # If post selection, use the Revit's PickObjects to select items.
  # selection_filter limits selection to electrical equipment.
  selection_filter = electrical_equipment_filter()
  try:
    selection_reference = uidoc.Selection.PickObjects(ObjectType.Element, selection_filter, 'Select Electrical Equipment to rename.')
  except:
    # if selection is aborted, it throws an exception...
    sys.exit()
  if not selection_reference:
    sys.exit()
  selected_equipment = [doc.GetElement(r.ElementId) for r in selection_reference]

  return selected_equipment

class electrical_equipment_filter(ISelectionFilter):
  def __init__(self):
    pass
  def AllowElement(self, element):
    if element.Category.Id.IntegerValue in selectable_categories:
      return True
    else:
      return False
  def AllowReference(self, element):
    return False

def get_parents_and_children(elem):
  parents_and_children = list(elem.MEPModel.GetElectricalSystems())

  children = list(elem.MEPModel.GetAssignedElectricalSystems())
  parents = []
  for es in parents_and_children:
    if es.Id not in [x.Id for x in children]:
      parents.append(es)

  # To get the corresponding elements from the circuit, you can typically use
  # circuit.Elements. However, if you're looking at the parent circuit, the
  # base element element will be the only item in this list. In parent
  # circuits, you'll want to look at the BaseEquipment property.

  return parents, children

class Colors(Enum):
  Red = DB.Color(255,0,0)
  Green = DB.Color(0,128,0)
  Blue = DB.Color(70,65,240)

def create_line(start, end):
  if start.DistanceTo(end) &lt; 1:
     return False
  new_line = DB.Line.CreateBound(start,end)
  return new_line

def make_shape(lines, color):
  new_shape = DB.DirectShape.CreateElement(doc, DB.ElementId(DB.BuiltInCategory.OST_GenericModel))
  new_shape.SetShape(lines)

  graphic_settings = DB.OverrideGraphicSettings()
  graphic_settings.SetProjectionLineColor(color)
  graphic_settings.SetProjectionLineWeight(6)

  doc.ActiveView.SetElementOverrides(new_shape.Id, graphic_settings)

  return new_shape

def set_shape_params(shape, circuit_slot, circuit_name):
  shape.get_Parameter(DB.BuiltInParameter.ALL_MODEL_MARK).Set('pyRevit Electrical Circuit Visualization')
  shape.get_Parameter(DB.BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set('{} - {}'.format(circuit_slot, circuit_name))

from itertools import izip, tee
def pairwise(iterable):
  "s -&gt; (s0, s1), (s1, s2), (s2, s3), ..."
  a, b = tee(iterable)
  next(b, None)
  return izip(a, b)

# def minimum_spanning_tree(source, points):
#   # Create a set to keep track of visited points
#   visited = set()

#   # Initialize the source point
#   visited.add(source)

#   # Initialize the lines list
#   lines = []

#   # Loop until all points have been visited
#   while len(visited) &lt; len(points) + 1:
#     # Find the closest unvisited point to the visited set
#     min_dist = float('inf')
#     min_point = None
#     for visited_point in visited:
#       for neighbor in points:
#         if neighbor in visited:
#           continue
#         dist = visited_point.DistanceTo(neighbor)
#         if dist &lt; min_dist:
#           min_dist = dist
#           min_point = neighbor

#     # Add the closest point to the visited set and draw a line to it
#     visited.add(min_point)
#     new_line = create_line(min_point, visited_point)
#     lines.append(new_line)

#   return lines

def nearest_neighbor(source, points):
  unvisited = set(points)
  sorted_points = [source]
  while unvisited:
    closest = min(unvisited, key=lambda p: p.DistanceTo(sorted_points[-1]))
    sorted_points.append(closest)
    unvisited.remove(closest)
  lines = []
  for x, y in pairwise(sorted_points):
    line = create_line(x, y)
    lines.append(line)
  return lines

# def star_connect(source, points):
#   lines = []
#   for point in points:
#     line = create_line(source, point)
#     lines.append(line)
#   return lines

def visualize_circuits(equipment):
  equipment_origin = equipment.GetTransform().Origin
  parents, children = get_parents_and_children(equipment)
  new_shapes = []
  lines = []

  # First draw circuits for parents (upstream sources)
  for circuit in parents:
    circuit_slot = circuit.Name
    circuit_name = circuit.get_Parameter(DB.BuiltInParameter.RBS_ELEC_CIRCUIT_NAME).AsString()
    line = create_line(equipment_origin, circuit.BaseEquipment.GetTransform().Origin)
    if line:
      lines.append(line)
  if lines:
    shape = make_shape(lines, Colors.Red.value)
    set_shape_params(shape, circuit_slot, circuit_name)
    new_shapes.append(shape)
    print('Source Vector: {}'.format(output.linkify(shape.Id)))

  children = sorted(children, key=lambda x: x.Name)

  # Next draw circuits for children (downstream loads)
  for circuit in children:
    circuit_slot = circuit.Name
    circuit_name = circuit.get_Parameter(DB.BuiltInParameter.RBS_ELEC_CIRCUIT_NAME).AsString()
    if 'SPARE' in circuit_name or 'SPACE' in circuit_name:
      continue
    print('{} - {}'.format(circuit_slot, circuit_name))
    load_locations = [elem.GetTransform().Origin for elem in circuit.Elements]

    lines = nearest_neighbor(equipment_origin, load_locations)
    ## Two alternate algorithms for drawing lines:
    # lines = minimum_spanning_tree(equipment_origin, load_locations)
    # lines = star_connect(equipment_origin, load_locations)

    if lines:
      shape = make_shape(lines, Colors.Blue.value)
      set_shape_params(shape, circuit_slot, circuit_name)
      new_shapes.append(shape)
      print('Load Vector: {}'.format(output.linkify(shape.Id)))
  return new_shapes

selected_equipment = UI_get_equipment()
t = DB.Transaction(doc, "Visualize Electrical Equipment")
t.Start()
all_created_circuits = []

for equipment in selected_equipment:
  all_created_circuits.append(visualize_circuits(equipment))

if all_created_circuits:
  selection = ui.Selection()
  selection.clear()

  for shape in all_created_circuits:
    selection.add(shape)

  selection.update()
t.Commit()
</pre>

Here are screenshots of the results of each:

<center>
<img src="img/pr_circuit_nearest_neighbour.png" alt="Nearest Neighbor" title="Nearest Neighbor" width="800"/> <!-- Pixel Height: 1,332 Pixel Width: 1,822 -->
<p style="font-size: 80%; font-style:italic">Nearest Neighbor</p>
</center>

<center>
<img src="img/pr_circuit_minimum_spanning_tree.png" alt="Minimum Spanning Tree" title="Minimum Spanning Tree" width="800"/> <!-- Pixel Height: 1,474 Pixel Width: 2,002 -->
<p style="font-size: 80%; font-style:italic">Minimum Spanning Tree</p>
</center>

<center>
<img src="img/pr_circuit_star_connected.png" alt="Star Connected" title="Star Connected" width="800"/> <!-- Pixel Height: 1,326 Pixel Width: 1,800 -->
<p style="font-size: 80%; font-style:italic">Star Connected</p>
</center>

**Answer:** I would suggest taking a step back from Revit and model lines and ponder how
to best [display complex graph relationships](https://duckduckgo.com/?q=display+complex+graph+relationships).

I used the [Delaunay algorithm](https://en.wikipedia.org/wiki/Delaunay_triangulation) to
connect all the elements that have electrical circuits:

<center>
<img src="img/pr_circuit_ricaun_delaunay.png" alt="Delaunay algorithm" title="Delaunay algorithm" width="800"/> <!-- Pixel Height: 2,160 Pixel Width: 3,840 -->
<p style="font-size: 80%; font-style:italic">Delaunay algorithm</p>
</center>

That was the best I found to visualize circuits in 3d.

**Response:** The goal of this script is to help our team decide if panels can/should be relocated based on the locations of their loads (to minimize feeder lengths and voltage drop). Typically, I might just go to a plan view and look at the wiring, but with the size of the project we're working on, we have 21 views across two floors, across multiple disciplines like lighting, power distribution, house power.

The star-connected approach has been ok, as long as I group the generic models I'm creating by circuit so I can see which clusters of lines belong together. But it looks very messy when two circuits feed to the same room, so there might be 10-20 lines going off in the same direction. I'm really looking to generate a node graph that matches the proposed wiring that Revit provides when you create new circuits (see attached image). If I could, I'd like to just use the existing wiring that's in the views in the project (basically I'd do a search for the wires that are linked with that circuit and use the end points of those wires as the X- and Y-coordinates, then add the Z-coordinate from the model elements to draw my 3D graph lines), however I can't guarantee that every element is wired in the plans, and there's also no guarantee that the wire layouts are up-to-date/accurate. So I'd settle for an approximate layout that uses Revit's predictive wiring system.

I see in the API that Revit has a Wire Create() method, but it seems like the set of XYZ points you provide to the method argument are used to generate a single wire at a time. Is there a way to provide multiple XYZ points and have Revit insert multiple wires to inter-connect the points?

Otherwise, after finding this page, using a force-directed graph with simulated annealing seems like the closest approach. However, I'm in way over my head here, and sadly I just won't have time to implement this. It also looks like it's generating a 'closed' graph, similar to the Delaunay approach, where all the nodes are interconnected. It would still need some second function to eliminate closed loops if we want it to be more accurate.

Finally, are there any recommendations as far as modeling these vectors go? I'm generating lines and placing them in Generic Model elements right now, but I'm wondering if that's the best approach. It offers no easy way for me to select all similar and delete them when I'm done, so if I forget they are there and start doing other things, I'll eventually need a script to find and delete them. Perhaps putting them in a unique sub-category of electrical equipment, or maybe under analytical models somewhere?

Thanks for the help!

<center>
<img src="img/pr_circuit_desired_result.png" alt="Desired Result" title="Desired Result" width="800"/> <!-- Pixel Height: 902 Pixel Width: 1,096 -->
<p style="font-size: 80%; font-style:italic">Desired Result</p>
</center>

**Answer:** Have you looked at [ElectricalSystem.GetCircuitPath()](https://apidocs.co/apps/revit/2020.1/0448a0ee-c9bf-f037-c1b7-d49ce03ffa71.htm)?

**Response:** I did try that at first, but it had its own problems.
The first is that the circuit's Path Mode must be set to 'All Devices', rather than 'Farthest Device'.
This isn't always the case. If it's set to Farthest Device, then GetCircuitPath() only returns the points to get to the farthest device. I'd have to override the existing Path Mode settings for each circuit on the panel, and then set them back when done - and this means taking ownership over all the circuits, which may not be feasible with the number of users we have on this project.

And while that would likely get us pretty close to the desired result, it's also still not perfect. As a test, I cleared the wires from a lighting circuit and redrew them using the automatic tool. Then I opened up the Edit Path tool and set the Path Mode to All Devices, and you can see in the attachment that the path still contains closed loops, whereas the wires do not.

<center>
<img src="img/pr_circuit_problem_case.png" alt="Problem Case" title="Problem Case" width="800"/> <!-- Pixel Height: 1,346 Pixel Width: 2,720 -->
<p style="font-size: 80%; font-style:italic">Problem Case</p>
</center>

**Answer:** If your goal is to check if your panel is near or far from the load, the best approach should be to create a load center from the panel.
Basically, the interpolation between each element location using the load value (Load1 x Location1 + Load2 x Location2) / (Load1 + Load2).

I don't use Revit Wire, I have a plugin to create wires inside Conduit/CableTray, that's a requirement in my country, so Revit Wire is useless in my case.

If you only need to verify the panel location probably messing with Wire is a bad choice, Wire is a 2d element that needs to have a view to work. You could try to get the location of the Wire and draw the lines, but I'm not sure if gonna be easy to see in a 3d view.

You already using DirectShape to create the lines, I guess that is the easiest way. You could set a name in the DirectShape element and use that to select every single one and delete using another command.

**Response:** Good ideas @ricaun.
Finding a center-point that's weighted based on the loads may be an easier approach.
I could then use the distance between that center-point and the panel origin as a metric that I could even potentially calculate for every panel, without having to model anything. I may eventually put that together as a separate tool which you could use first, to find the problem panels.

I agree using wire is not ideal - it would take quite a bit of manipulation to get from 2D wire to 3D vector shapes. If I eventually find time to pursue this further, I'll likely use a Delaunay implementation as you have suggested. It's too bad Revit wire isn't 3D - I have gotten in trouble before for copying a plan view with wires; I eventually deleted the wires from the first view and it took me ages to figure out why I still couldn't recircuit the elements.

Also, good to know I can SetName on the DirectShapes. I have just been storing info in the comment and mark parameters, but that leaves them open to editing by others, which is risky if I ever need to search through them and delete them based on a filter.

**Answer:** I'm not sure if the distance is too useful to know where is the best place to put the panel.
And probably gonna add some features like that in the plugin ElectricalUtils;
using `DirectContext3D` would be fun to show the load center without creating any element.

Many thanks to Luiz 'ricaun' for sharing these great suggestions and his extensive experience!

####<a name="4"></a> Aligning Two Elements

Let's wrap up with some hints
on [how to use the alignment method for family instance](https://forums.autodesk.com/t5/revit-api-forum/how-to-use-the-alignment-method-using-for-family-instance/m-p/11938454):

**Question:** I need to align a `FamilyInstance` which I created using C# to a line that I also created Via C# in Revit 2023,
like in this picture showing a structural column and a `ModelCurve` that are not yet aligned:

<center>
<img src="img/align_element_1.png" alt="Align element &ndash; not aligned" title="Align element &ndash; not aligned" width="300"/>
<p style="font-size: 80%; font-style:italic">Align element &ndash; not aligned</p>
</center>

This is how I want the column to be aligned to the `ModelCurve`:

<center>
<img src="img/align_element_2.png" alt="Align element" title="Align element" width="300"/>
<p style="font-size: 80%; font-style:italic">Align element</p>
</center>

Here is sample code from my script:

<pre class="prettyprint">
  Line line = Line.CreateBound(startPoint, endPoint);
  Element newPile = doc.Create.NewFamilyInstance(
    point, symbol, Level, structuralType);
</pre>

**Answer:** Well, first of all you need to understand how to implement such a constraint manually in the end user interface.
I believe you define a dimension between the two objects to do so, and constrain it to a zero distance.
The [Family API samples](https://thebuildingcoder.typepad.com/blog/2009/08/the-revit-family-api.html) demonstrate
how such a constraint can be set up programmatically.

Reading that myself, I discover that
the [NewAlignment method](https://www.revitapidocs.com/2023/b3c10008-aba6-9eee-99c9-7e05ace75796.htm) might
come in handy.
Searching this forum for other threads on `NewAlignment` ought to turn up something useful for you.

**Response:** Thanks a lot for introducing this method for me .
I am trying to align the family instance called newPile to the model Curve however I am getting this error:

- Autodesk.Revit.Exceptions.ArgumentException:
  The two references are not geometrically aligned so the Alignment cannot be created.
  Parameter name: reference2

I have created both the model curve and the family instance on the same level.

This is the code that I am using:

<pre class="prettyprint">
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Document = Autodesk.Revit.DB.Document;

namespace Tunnel
{
  public class SheetPile
  {
    double meters = 3.28084;

    public SheetPile(Document doc, double x, String PileName, Level Level, TunnelFloors tunnelFloors)
    {
      Options options = new Options();
      Plane plane = Plane.CreateByNormalAndOrigin(new XYZ(0, 0, 1), new XYZ(0, 0, 0));
      SketchPlane sketchPlane = SketchPlane.Create(doc, plane);
      // Create a new view plan for Level 1
      // Get the floor plan view family type
      ViewFamilyType viewFamilyType = new FilteredElementCollector(doc)
        .OfClass(typeof(ViewFamilyType))
        .Cast&lt;ViewFamilyType&gt;()
        .FirstOrDefault(v =&gt; v.ViewFamily == ViewFamily.StructuralPlan);

      // Create a new view plan for Level 1

      ViewPlan viewPlan = ViewPlan.Create(doc, viewFamilyType.Id, Level.Id);
      // Convert input values from feet to meters
      x = x * meters;

      // Define the family symbol and structural type
      FamilySymbol symbol = new FilteredElementCollector(doc)
        .OfClass(typeof(FamilySymbol))
        .OfCategory(BuiltInCategory.OST_StructuralColumns)
        .FirstOrDefault(e =&gt; e.Name == PileName) as FamilySymbol;

      StructuralType structuralType = StructuralType.Column;

      // Create a list of curves
      List&lt;Curve&gt; curves = new List&lt;Curve&gt;();
      foreach (Floor floor in tunnelFloors.Floors)
      {
        Sketch sketch = doc.GetElement(floor.SketchId) as Sketch;
        foreach (CurveArray curveArray in sketch.Profile)
        {
          foreach (Curve curve in curveArray)
          {
            XYZ p0 = (new XYZ(curve.GetEndPoint(0).X, curve.GetEndPoint(0).Y, 0));
            XYZ p1 = (new XYZ(curve.GetEndPoint(1).X, curve.GetEndPoint(1).Y, 0));
            Curve c1 = Line.CreateBound(p0, p1);
            curves.Add(curve);
          }
        }
      }

      foreach (Curve curve in curves)
      {
        ModelCurve m1 = doc.Create.NewModelCurve(curve, sketchPlane);
        // Move the family instance along the curve by the distance variable
        double length = curve.Length;
        int count = (int)(length / x);
        for (int j = 1; j &lt;= count; j++)
        {
          XYZ point = curve.Evaluate((double)j * x / length, true);
          FamilyInstance newPile = doc.Create.NewFamilyInstance(point, symbol, Level, structuralType);
          newPile.LookupParameter("Top Level").Set("Level 2");
          Reference s = newPile.GetReferenceByName("SS");
          // Get the reference plane named "SS" from the family instance

          //uidoc.Selection.PickObject(ObjectType.PointOnElement);
          doc.Create.NewAlignment(viewPlan, s, curve.Reference);
        }
      }
    }
  }
}
</pre>

**Answer:** Did you read the remarks on
the [NewAlignment method](https://www.revitapidocs.com/2023/b3c10008-aba6-9eee-99c9-7e05ace75796.htm) in
the Revit API docs?

> These references must be already geometrically aligned (this function will not force them to become aligned).

**Response:** Is there any way I can force them to be aligned using the Revit API?

**Answer:** The easiest way to ensure they are aligned is to create them accordingly in the first place, if they are being generated from scratch.
Otherwise, you can use the standard translation and rotation functionality provided by `ElementTransformUtils`.
Or, you can set the location curve via the `Location` property.

**Response:** I will rest for a few then see which approach fits best.
I am using a family which is already loaded in the Project and I am placing them on a line with a specific distance .
However I want them to rotate according to the Curve or Line they are placed on.
I haven't been able to do such thing.
Should I use `ElementTransformUtils.Rotate` in this case?

**Answer:** Either `ElementTransformUtils.Rotate` or just manipulate the `LocationPoint` or `LocationCurve`
via [`Rotate`](https://www.revitapidocs.com/2023/ed4de043-9a60-f6cd-c09b-b13c4612b343.htm).

