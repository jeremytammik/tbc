<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- compare element
  Chuong Ho
  It's been a while since our last Revit Add-in manager update, but we've got some exciting news to share today!
  I thrilled to introduce a new tool that's now part of the Add-in manager. This tool is a game-changer for both developers and users as it allows you to easily compare differences between two elements. Not only that, but it also uses color to visually highlight all parameter variations, making it incredibly intuitive and user-friendly. Plus, you can view the similarity results of value comparisons between parameters from those two elements
  Stay tuned for more updates and get ready to experience a whole new level of efficiency with our latest addition to the Revit Add-in manager!"
  Open Source : https://github.com/chuongmep/RevitAddInManager
  Documentation : [How to use Compare Parameter Element](https://github.com/chuongmep/RevitAddInManager/wiki/How-to-use-Compare-Parameter-Element)
  #OpenSource #addinmanager #bim #autodesk #revitapi
  /Users/jta/a/doc/revit/tbc/git/a/img/ch_compare_element.png

- Revit database explorer (RDBE)
  https://github.com/NeVeSpl/RevitDBExplorer
  mentioned last year
  https://thebuildingcoder.typepad.com/blog/2022/07/immutable-uniqueid-and-revit-database-explorer.html#3
  The fastest, most advanced, asynchronous Revit database exploration tool for Revit 2021+.**
  Yet another [RevitLookup](https://github.com/jeremytammik/RevitLookup) like tool. RevitLookup was an indispensable tool to work with Revit API for many years. But now, there is a better tool for the job. Let me introduce you to RDBE and its capabilities. RDBE not only allows us to explore database in a more efficient way thanks to querying, but also to modify Revit database through ad hoc scripts written in C#.
  - [query Revit database](#query-revit-database-with-rdq-revit-database-querying)
  - [script Revit database](#script-revit-database-with-rds-revit-database-scripting)
    - [ad hoc SELECT query](#ad-hoc-select-query)
    - [ad hoc UPDATE command](#ad-hoc-update-command)
  - [filterable tree of elements and list of properties and methods](#filterable-tree-of-elements-and-list-of-properties-and-methods)
  - [easy access to Revit API documentation](#easy-access-to-revit-api-documentation)
  - [edit parameter value](#edit-parameter-value)
  - [extensive support for ForgeTypeId](#extensive-support-for-forgetypeid)
  - [better support for Revit Extensible Storage](#better-support-for-revit-extensible-storage)
  - [easier work with Element.Geometry](#easier-work-with-elementgeometry)
  - [dark and light UI themes](#dark-and-light-ui-themes)
  - [more advanced tree view](#more-advanced-tree-view)
  - [snoop Revit events](#snoop-revit-events-with-rem-revit-event-monitor)
  - [snoop updaters](#snoop-updaters)
  You can always use an alternative tool that offers access to Extensible Storage Scheme. (:
  https://github.com/NeVeSpl/RevitDBExplorer#better-support-for-revit-extensible-storage

- Python 2D Geometry Library
  https://forums.autodesk.com/t5/revit-api-forum/gbxml-from-adjacent-conceptual-mass-adjacent-space-missing-small/m-p/12238726#M74138
  shapely
  Manipulation and analysis of geometric objects in the Cartesian plane.
  https://pypi.org/project/shapely/
  https://pypi.org/project/xgbxml/
  https://shapely.readthedocs.io/en/stable/set_operations.html

- Dan North & Associates discuss measuring developer productivity, describing
  [The Worst Programmer I Know](https://dannorth.net/2023/09/02/the-worst-programmer/):
  > Measure productivity by all means...
  Just don’t try to measure the individual contribution of a unit in a complex adaptive system...

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

### Element Diff, Shapely and RDBE


####<a name="2"></a> Revit Element Difference Comparison

- compare element
Chuong Ho
It's been a while since our last Revit Add-in manager update, but we've got some exciting news to share today!
I thrilled to introduce a new tool that's now part of the Add-in manager. This tool is a game-changer for both developers and users as it allows you to easily compare differences between two elements. Not only that, but it also uses color to visually highlight all parameter variations, making it incredibly intuitive and user-friendly. Plus, you can view the similarity results of value comparisons between parameters from those two elements
Stay tuned for more updates and get ready to experience a whole new level of efficiency with our latest addition to the Revit Add-in manager!"
Open Source : https://github.com/chuongmep/RevitAddInManager
Documentation : [How to use Compare Parameter Element](https://github.com/chuongmep/RevitAddInManager/wiki/How-to-use-Compare-Parameter-Element)

<center>
<img src="img/ch_compare_element.png" alt="Compare Element" title="Compare Element" width="670"/> <!-- Pixel Height: 588 Pixel Width: 1,336 -->
</center>

####<a name="3"></a> The Revit database explorer RDBE

We already [mentioned](https://thebuildingcoder.typepad.com/blog/2022/07/immutable-uniqueid-and-revit-database-explorer.html#3)
the [Revit database explorer RDBE](https://github.com/NeVeSpl/RevitDBExplorer) last year:

> The fastest, most advanced, asynchronous Revit database exploration tool for Revit 2021+.**
Yet another [RevitLookup](https://github.com/jeremytammik/RevitLookup) like tool. RevitLookup was an indispensable tool to work with Revit API for many years. But now, there is a better tool for the job. Let me introduce you to RDBE and its capabilities. RDBE not only allows us to explore database in a more efficient way thanks to querying, but also to modify Revit database through ad hoc scripts written in C#.

I was prompted to point it out again by the recommendation in a RevitLookup issue to use an alternative Revit database exploration tool that offers better access to Extensible Storage Schemata, cf. RDBE's extensive list of features:

- [Query Revit database](#query-revit-database-with-rdq-revit-database-querying)
- [Script Revit database](#script-revit-database-with-rds-revit-database-scripting)
- [Ad hoc SELECT query](#ad-hoc-select-query)
- [Ad hoc UPDATE command](#ad-hoc-update-command)
- [Filterable tree of elements and list of properties and methods](#filterable-tree-of-elements-and-list-of-properties-and-methods)
- [Easy access to Revit API documentation](#easy-access-to-revit-api-documentation)
- [Edit parameter value](#edit-parameter-value)
- [Extensive support for ForgeTypeId](#extensive-support-for-forgetypeid)
- [Better support for Revit Extensible Storage](#better-support-for-revit-extensible-storage)
- [Easier work with Element.Geometry](#easier-work-with-elementgeometry)
- [Dark and light UI themes](#dark-and-light-ui-themes)
- [More advanced tree view](#more-advanced-tree-view)
- [Snoop Revit events](#snoop-revit-events-with-rem-revit-event-monitor)
- [Snoop updaters](#snoop-updaters)

####<a name="4"></a> The Shapely Python 2D Geometry Library

- Python 2D Geometry Library
https://forums.autodesk.com/t5/revit-api-forum/gbxml-from-adjacent-conceptual-mass-adjacent-space-missing-small/m-p/12238726#M74138
shapely
Manipulation and analysis of geometric objects in the Cartesian plane.
https://pypi.org/project/shapely/
https://pypi.org/project/xgbxml/
https://shapely.readthedocs.io/en/stable/set_operations.html

####<a name="5"></a> Measuring Developer Productivity

Dan North and Associates share some interesting insights discussing how to measure developer productivity,
describing [The Worst Programmer I Know](https://dannorth.net/2023/09/02/the-worst-programmer/):

> ... Measure productivity by all means...
Just don’t try to measure the individual contribution of a unit in a complex adaptive system...


<center>
<img src="img/.png" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
<br/>
</center>

**Question:**
**Answer:**
**Response:**

<pre class="prettyprint">
</pre>


