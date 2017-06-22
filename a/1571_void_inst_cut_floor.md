<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

Determine RVT version using #Python #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

&ndash; 
...

-->

### Determining Void Instances Cutting a Floor

Yet another brilliant and super succinct solution provided by Fair59, answering
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread on how
to [get cutting void instances in the floor](https://forums.autodesk.com/t5/revit-api-forum/get-cutting-void-instances-in-the-floor/m-p/7170237):

**Question:** I have a floor on which a family instance is inserted on the face of the floor (the instance host is also the floor).

I checked in the family the "Cut with Void When Loaded" parameter, so that the void is created in the floor.

Now, I want to retrieve all the instances that create voids in the floor.

I did some research, and found the discussion
of [Boolean operations and `InstanceVoidCutUtils`](http://thebuildingcoder.typepad.com/blog/2011/06/boolean-operations-and-instancevoidcututils.html).

But when I use the `InstanceVoidCutUtils` `GetCuttingVoidInstances` method, it returns an empty list.
 
I also looked
at the [`ElementIntersectsSolidFilter` problem and solution](http://thebuildingcoder.typepad.com/blog/2015/07/intersect-solid-filter-avf-and-directshape-for-debugging.html#2) and
tried `ElementIntersectsElementFilter` and `ElementIntersectsSolidFilter`.

Those filters do not return the expected result for me to deduce the voids in the floor either; in fact, they say that no elements intersect.
 
First case &ndash; area = 607.558m2 and Volume = 243.023m3:
 
<center>
<img src="img/void_inst_cut_floor_1.png" alt="Void instances cutting floor" width="1386">
</center>

Second case &ndash; area = 607.558m2 and Volume = 243.023m3:

<center>
<img src="img/void_inst_cut_floor_2.png" alt="Void instances cutting floor" width="1379">
</center>

`Family` parameter "Cut with Voids When Loaded":
 
<center>
<img src="img/void_inst_cut_floor_3.png" alt="Void instances cutting floor" width="1285">
</center>

`FamilyInstance` cutting host:

<center>
<img src="img/void_inst_cut_floor_4.png" alt="Void instances cutting floor" width="1293">
</center>

Here is the code I use:

<pre class="code">
Solid solid = floor.get_Geometry(new Options())
.OfType<Solid>()
.Where<Solid>(s => null != s && !s.Edges.IsEmpty)
.FirstOrDefault();

FilteredElementCollector intersectingInstances = new FilteredElementCollector(doc)
.OfClass(typeof(FamilyInstance))
.WherePasses(new ElementIntersectsSolidFilter(solid));

int n1 = intersectingInstances.Count<Element>();

intersectingInstances = new FilteredElementCollector(doc)
.OfClass(typeof(FamilyInstance))
.WherePasses(new ElementIntersectsElementFilter(floor));

int n = intersectingInstances.Count<Element>();
</pre>

Here, both `n` and `n1` are equal to 0.
 
**Answer:** Try using
 
<pre class="code">
List<ElementId> intersectingInstanceIds = floor.FindInserts(false,false,false,true).ToList();
</pre>

**Response:** I have done some tests and here are my results:

<center>
<img src="img/void_inst_cut_floor_6.png" alt="Void instances cutting floor" width="1405">
</center>

Situation:

- `Fl_1` is hosted by Level 3 and intersects the floor.
- `Fl_2` is hosted by the floor and intersects it.

Results:

1. Do not cut geometry:
    - `InstanceVoidCutUtils.GetCuttingVoidInstances(floor)` returns `void`
    - `floor.FindInserts(false,false,false,true)` returns `Fl_2`
2. Cut geometry:
    - `InstanceVoidCutUtils.GetCuttingVoidInstances(floor)` returns `Fl_1`
    - `floor.FindInserts(false,false,false,true)` returns both `Fl_1` and `Fl_2`

In summary, `FindInserts` returns `FI_1` even if its host (Level 3) is not the floor.

It's good.

I think we can say that the problem is solved.
 
Thank you FAIR59 ;)

