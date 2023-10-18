<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


twitter:

 the #RevitAPI @AutodeskRevit #BIM @DynamoBIM @AutodeskAPS


&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Xgbxml Fixes Gaps in Building Geometry

Jake of [Ripcord Engineering](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3926242) recently
shared a bunch of valuable [Python and gbXML tips](https://thebuildingcoder.typepad.com/blog/2023/07/export-gbxml-and-python-tips.html) and followed up with a pointer
to [Shapely](https://pypi.org/project/shapely/)
to [Find and Fix a gap in the building geometry](https://thebuildingcoder.typepad.com/blog/2023/09/element-diff-compare-shapely-and-rdbe.html#4).

He followed up with this impressive and beautiful documentation of
an entire gbXML [small surface solution addressing the whole building](doc/gbxml_small_surface_solution_whole_building.pdf) that I think warrantds an entire blog post of its own, so here goes:

####<a name="2"></a> Small Surface Solution &ndash; Whole Building

Addressing the issue
on [gbXML from adjacent conceptual mass/adjacent space missing small surface](https://forums.autodesk.com/t5/revit-api-forum/gbxml-from-adjacent-conceptual-mass-adjacent-space-missing-small/m-p/12232100).

####<a name="3"></a> Setup

Import packages:

<pre class="prettyprint">
# import packages
from xgbxml import get_parser
from xgbxml import geometry_functions, gbxml_functions, render_functions from lxml import etree
import matplotlib.pyplot as plt
import copy
import math
from uuid import uuid4
</pre>

Generate parser:

# uses xgbxml to generate a lxml parser to read gbXML version 0.37
parser=get_parser(version='0.37')

####<a name="3"></a> Open gbXML BIM Model

Open file *23-013 WH Swan Hill_Mass_23-08-30.xml*:

# opens the file using the custom lxml parser
fp='23-013 WH Swan Hill_Mass_23-08-30.xml' tree=etree.parse(fp,parser) gbxml=tree.getroot()
# renders the Campus element
ax=gbxml.Campus.render() ax.figure.set_size_inches(8, 8) ax.set_title(fp)
plt.show()
1 of 6 10/4/2023, 19:59
small_surface_solution - whole_building file:///C:/Users/admin/AppData/Local/Temp/pid-10720/small_surface_s...

####<a name="3"></a> Identify Gaps in Geometry

Identify all gaps in the surfaces of the building This uses a new method of the Building element -> get_gaps_in_surfaces .
In [5]:
Out[5]: [{'space_ids': ['aim2197'],
'shell': [(72.2287629, -0.3141381, 0.0),
           (72.2287629, -0.4999998, 0.0),
           (72.0986211, -0.4999998, 0.0),
           (72.2287629, -0.3141381, 0.0)]},
         {'space_ids': ['aim2553', 'aim7413'],
          'shell': [(80.2291667, 14.5625, 10.0),
           (80.0208333, 14.5625, 10.0),
           (80.0208333, 16.020833, 10.0),
           (80.2291667, 16.020833, 10.0),
           (80.2291667, 14.5625, 10.0)]},
         {'space_ids': ['aim6674'],
          'shell': [(72.2287629, -0.4999998, 10.0),
           (72.2287629, -0.3141381, 10.0),
           (72.0986211, -0.4999998, 10.0),
           (72.2287629, -0.4999998, 10.0)]}]
   # identify gaps in surfaces of building
gaps=gbxml.Campus.Building.get_gaps_in_surfaces() gaps
 The result is a list of dictionaries. Each dictionary contains two items:
2 of 6 10/4/2023, 19:59

small_surface_solution - whole_building file:///C:/Users/admin/AppData/Local/Temp/pid-10720/small_surface_s...
 • 'space_ids': a list of the ids of the adjacent Spaces.
• 'shell': a list of the coordinates of the exterior polygon of the gaps.
Here the first and third items appear to be triangle gaps with only one adjacent space - so these are exterior gaps also adjacent to the outside.


####<a name="3"></a> Add Missing Sufaces


Adding the missing surfaces to the building; first gap:

In [6]:
Out[6]: {'space_ids': ['aim2197'],
'shell': [(72.2287629, -0.3141381, 0.0),
   # print gap
gap=gaps[0] gap
 In [7]:
(72.2287629, -0.4999998, 0.0),
(72.0986211, -0.4999998, 0.0),
(72.2287629, -0.3141381, 0.0)]}
   # add Surface
# surface element surface=gbxml.Campus.add_Surface(
id=str(uuid4()), surfaceType=None, # to do constructionIdRef=None, # to do exposedToSun=None # to do
)
# adjacent space id child element
for space_id in gap['space_ids']: surface.add_AdjacentSpaceId(
spaceIdRef=space_id
)
# planar geometry child element
planar_geometry = surface.add_PlanarGeometry() planar_geometry.set_shell(gap['shell'])
# check
print(surface.tostring())
 <Surface xmlns="http://www.gbxml.org/schema" id="f20a7dbc-94d5-43ee-bf64-748c3e61658
b">
  <AdjacentSpaceId spaceIdRef="aim2197"/>
  <PlanarGeometry>
    <PolyLoop>
      <CartesianPoint>
        <Coordinate>72.2287629</Coordinate>
        <Coordinate>-0.3141381</Coordinate>
        <Coordinate>0.0</Coordinate>
      </CartesianPoint>
      <CartesianPoint>
        <Coordinate>72.2287629</Coordinate>
        <Coordinate>-0.4999998</Coordinate>
3 of 6 10/4/2023, 19:59

small_surface_solution - whole_building file:///C:/Users/admin/AppData/Local/Temp/pid-10720/small_surface_s...
                 <Coordinate>0.0</Coordinate>
              </CartesianPoint>
              <CartesianPoint>
                <Coordinate>72.0986211</Coordinate>
                <Coordinate>-0.4999998</Coordinate>
                <Coordinate>0.0</Coordinate>
              </CartesianPoint>
            </PolyLoop>
          </PlanarGeometry>
        </Surface>


####<a name="3"></a> Second Gap


Second gap
In [8]:
Out[8]: {'space_ids': ['aim2553', 'aim7413'], 'shell': [(80.2291667, 14.5625, 10.0),
   # print gap
gap=gaps[1] gap
 In [9]:
(80.0208333, 14.5625, 10.0),
(80.0208333, 16.020833, 10.0),
(80.2291667, 16.020833, 10.0),
(80.2291667, 14.5625, 10.0)]}
   # add Surface
# surface element surface=gbxml.Campus.add_Surface(
id=str(uuid4()), surfaceType=None, # to do constructionIdRef=None, # to do exposedToSun=None # to do
)
# adjacent space id child element
for space_id in gap['space_ids']: surface.add_AdjacentSpaceId(
spaceIdRef=space_id
)
# planar geometry child element
planar_geometry = surface.add_PlanarGeometry() planar_geometry.set_shell(gap['shell'])
# check
print(surface.tostring())
 <Surface xmlns="http://www.gbxml.org/schema" id="407a76aa-3287-4b5e-ac62-0440fb629f7
2">
  <AdjacentSpaceId spaceIdRef="aim2553"/>
  <AdjacentSpaceId spaceIdRef="aim7413"/>
  <PlanarGeometry>
    <PolyLoop>
      <CartesianPoint>
        <Coordinate>80.2291667</Coordinate>
        <Coordinate>14.5625</Coordinate>
        <Coordinate>10.0</Coordinate>
      </CartesianPoint>
      <CartesianPoint>
4 of 6 10/4/2023, 19:59

small_surface_solution - whole_building file:///C:/Users/admin/AppData/Local/Temp/pid-10720/small_surface_s...
                  <Coordinate>80.0208333</Coordinate>
                 <Coordinate>14.5625</Coordinate>
                 <Coordinate>10.0</Coordinate>
               </CartesianPoint>
               <CartesianPoint>
                 <Coordinate>80.0208333</Coordinate>
                 <Coordinate>16.020833</Coordinate>
                 <Coordinate>10.0</Coordinate>
               </CartesianPoint>
               <CartesianPoint>
                 <Coordinate>80.2291667</Coordinate>
                 <Coordinate>16.020833</Coordinate>
                 <Coordinate>10.0</Coordinate>
               </CartesianPoint>
             </PolyLoop>
           </PlanarGeometry>
         </Surface>

####<a name="3"></a> Second Gap

In [10]:
Out[10]: {'space_ids': ['aim6674'],
'shell': [(72.2287629, -0.4999998, 10.0),
   # print gap
gap=gaps[2] gap
 (72.2287629, -0.3141381, 10.0),
(72.0986211, -0.4999998, 10.0),
(72.2287629, -0.4999998, 10.0)]}
   # add Surface
# surface element surface=gbxml.Campus.add_Surface(
id=str(uuid4()), surfaceType=None, # to do constructionIdRef=None, # to do exposedToSun=None # to do
)
# adjacent space id child element
for space_id in gap['space_ids']: surface.add_AdjacentSpaceId(
spaceIdRef=space_id
)
# planar geometry child element
planar_geometry = surface.add_PlanarGeometry() planar_geometry.set_shell(gap['shell'])
# check
print(surface.tostring())
 In [11]:
<Surface xmlns="http://www.gbxml.org/schema" id="96ad28f6-56fb-42b8-94d0-93c73d39886
6">
  <AdjacentSpaceId spaceIdRef="aim6674"/>
  <PlanarGeometry>
    <PolyLoop>
      <CartesianPoint>
<Coordinate>72.2287629</Coordinate>
5 of 6 10/4/2023, 19:59

small_surface_solution - whole_building file:///C:/Users/admin/AppData/Local/Temp/pid-10720/small_surface_s...
<Coordinate>-0.4999998</Coordinate>
                 <Coordinate>10.0</Coordinate>
               </CartesianPoint>
               <CartesianPoint>
                 <Coordinate>72.2287629</Coordinate>
                 <Coordinate>-0.3141381</Coordinate>
                 <Coordinate>10.0</Coordinate>
               </CartesianPoint>
               <CartesianPoint>
                 <Coordinate>72.0986211</Coordinate>
                 <Coordinate>-0.4999998</Coordinate>
                 <Coordinate>10.0</Coordinate>
               </CartesianPoint>
             </PolyLoop>
           </PlanarGeometry>
         </Surface>

####<a name="3"></a> Verify Waterproof

Recheck gaps in surfaces of building; there should now be no gaps.

# identify gaps in surfaces of building
gaps=gbxml.Campus.Building.get_gaps_in_surfaces() gaps

Out[12]:

[]

####<a name="3"></a> Save Model

Save the updated gbxml file.

In [13]:

# writes the gbXML etree to a local file
tree.write('23-013 WH Swan Hill_Mass_23-08-30-UPDATED.xml', pretty_print=True)
