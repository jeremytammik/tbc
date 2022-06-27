<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- RevitLookup Release 2023.1.0
  New: Hello World window changed to About
  New: re-sorted commands on the Revit ribbon, frequently used moved to the top
  New: added Snoop Active Document command
  Fix: revert support search index from keyboard
  Fix: removed label if ElementID was -1

- Filter different BuiltInCategories at same time
  https://forums.autodesk.com/t5/revit-api-forum/can-i-filter-different-builtincategories-at-same-time/m-p/11254486

- How do I get all the outermost walls in the model?
  https://forums.autodesk.com/t5/revit-api-forum/how-do-i-get-all-the-outermost-walls-in-the-model/m-p/11250597#M64192
  1656_exterior_walls.md
  addendum
[m.de.vriesTH5VM](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/5479182)
This algorithm to create a room around the building and let it determine its boundary walls is the most reliable and fastest solution to this particular problem of finding the exterior walls of a building. It also works better with e.g. Dutch drawing practice where compound walls are split into individual walls for its layers, making approaches that rely on 'isExternal' properties or the count of bounded rooms unreliable at best.
There is however one caveat that is important to keep in mind. This is essentially a 2D approach to finding boundaries as you are working with boundary lines and not planes. This appears to be a fundamental limitation of the Revit API. This is not normally a problem for this particular algorithm, but it will become one if your model has vertically stacked walls within a single level. In that case only one wall of that stack will be found, usually the one closest to the bottom of the temporary room.
There are of course workarounds to this: Determine the z-heights of each wall in the stack and create a separate room for each of those heights. This of course has to be done for every wall that falls into or crosses a particular z-height range that you are interested in, so the scan for finding all the z-heights that need to be checked separately can be time consuming for large designs.
I am guessing the `BuildingEnvelopeAnalyzer` needs a cell size variable to solve this same problem (though they are more likely to internally use a horizontal section plane to find intersecting walls and determine the exterior walls from a much smaller set of candidates).
So, the room around the entire design is probably the best solution with the least requirements, but it will need some non-trivial modifications to be able to handle designs with stacked walls  

- zen and the art of react programming
  [How to build Gmail-like UI with React Native at a Zen temple Koshoji](https://youtu.be/w-M9UFHLAl0)
  two-and-a-half-hour full feature film with beautiful views 

twitter:

 the #RevitAPI  @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash;
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Outer Walls

####<a name="2"></a>

- RevitLookup Release 2023.1.0
New: Hello World window changed to About
New: re-sorted commands on the Revit ribbon, frequently used moved to the top
New: added Snoop Active Document command
Fix: revert support search index from keyboard
Fix: removed label if ElementID was -1

####<a name="3"></a> 

- Filter different BuiltInCategories at same time
https://forums.autodesk.com/t5/revit-api-forum/can-i-filter-different-builtincategories-at-same-time/m-p/11254486

####<a name="4"></a> 

- How do I get all the outermost walls in the model?
https://forums.autodesk.com/t5/revit-api-forum/how-do-i-get-all-the-outermost-walls-in-the-model/m-p/11250597#M64192
1656_exterior_walls.md
addendum
[m.de.vriesTH5VM](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/5479182)
This algorithm to create a room around the building and let it determine its boundary walls is the most reliable and fastest solution to this particular problem of finding the exterior walls of a building. It also works better with e.g. Dutch drawing practice where compound walls are split into individual walls for its layers, making approaches that rely on 'isExternal' properties or the count of bounded rooms unreliable at best.
There is however one caveat that is important to keep in mind. This is essentially a 2D approach to finding boundaries as you are working with boundary lines and not planes. This appears to be a fundamental limitation of the Revit API. This is not normally a problem for this particular algorithm, but it will become one if your model has vertically stacked walls within a single level. In that case only one wall of that stack will be found, usually the one closest to the bottom of the temporary room.
There are of course workarounds to this: Determine the z-heights of each wall in the stack and create a separate room for each of those heights. This of course has to be done for every wall that falls into or crosses a particular z-height range that you are interested in, so the scan for finding all the z-heights that need to be checked separately can be time consuming for large designs.
I am guessing the `BuildingEnvelopeAnalyzer` needs a cell size variable to solve this same problem (though they are more likely to internally use a horizontal section plane to find intersecting walls and determine the exterior walls from a much smaller set of candidates).
So, the room around the entire design is probably the best solution with the least requirements, but it will need some non-trivial modifications to be able to handle designs with stacked walls  

####<a name="5"></a> 

- zen and the art of react programming
[How to build Gmail-like UI with React Native at a Zen temple Koshoji](https://youtu.be/w-M9UFHLAl0)
two-and-a-half-hour full feature film with beautiful views 
 

**Question:** 

<center>
<img src="img/.png" alt="Corridor centerline" title="Corridor centerline" width="400"/> <!-- 1117 -->
</center>

**Answer:** 


**Response:** 

<pre class="code">

</pre>

Many thanks to 
for raising this and sharing his nice approach!
