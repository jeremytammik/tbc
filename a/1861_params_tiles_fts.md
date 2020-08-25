<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- an interesting motivational article by Jesse Hall on unintentionally starting a successful YouTube channel and ending up earning moeny with it, all just to prove a point to his two teenage kids:
How I Went From 0 to 70k Subscribers on YouTube in 1 Year – And How Much Money I Made
https://www.freecodecamp.org/news/how-to-grow-your-youtube-channel/
> In conclusion, I just want to say that anyone can do anything. If you have (1) the proper motivation, (2) realistic expectations, and (3) you don't overwork yourself, you can be successful.

- Parameter type changes to custom in Revit 2021.1
https://forums.autodesk.com/t5/revit-api-forum/parameter-type-changes-to-custom-in-revit-2021-1/m-p/9693021

In Revit 2021.1, the following parameter types are automatically changed to `custom` after creation:

- Angular Speed
- Cost per Area
- Cost Rate Energy
- Cost Rate Power
- Diffusivity
- Distance
- Flow per Power
- Isothermal Moisture Capacity
- Mass per Time
- Power per Flow
- Power per Length
- Rotation Angle
- Stationing Interval
- Thermal Gradient Coefficient for Moisture Capacity

Is that intentional?

- effect of home office on meeting culture
  Microsoft analyzed data on its newly remote workforce
  https://hbr.org/2020/07/microsoft-analyzed-data-on-its-newly-remote-workforce

- How to get number of cut tiles in a room using revit api
  https://forums.autodesk.com/t5/revit-api-forum/parameter-type-changes-to-custom-in-revit-2021-1/m-p/9693021

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Custom Parameters

#### <a name="2"></a>Parameter Type Changes to Custom

A question came up on why a number of parameter types can no longer be created programmatically, in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [parameter type changes to custom in Revit 2021.1](https://forums.autodesk.com/t5/revit-api-forum/parameter-type-changes-to-custom-in-revit-2021-1/m-p/9693021):

**Question:** In Revit 2021.1, the following parameter types are automatically changed to `custom` after creation:

- Angular Speed
- Cost per Area
- Cost Rate Energy
- Cost Rate Power
- Diffusivity
- Distance
- Flow per Power
- Isothermal Moisture Capacity
- Mass per Time
- Power per Flow
- Power per Length
- Rotation Angle
- Stationing Interval
- Thermal Gradient Coefficient for Moisture Capacity

<center>
<img src="img/parameter_type_custom_in_2021_1.png" alt="Parameter type Custom in 2021.1" title="Parameter type Custom in 2021.1" width="600"/> <!-- 1166 -->
</center>

It looks like we don't have API support (for creating) these parameter types.

Is that intentional?

**Answer:** The development team replied:

Yes, it's intentional, and we recognise it as a limitation.

A future major release of Revit will offer a better experience.

The reason behind it is that the new data types in this list can be identified by `ForgeTypeId` but do not map to values in the deprecated `UnitType` enumeration or the `ParameterType` enumeration (which will be deprecated in future releases of Revit).

You can use these new data types to create parameters in the Revit user interface, but if you try to read the `ParameterType` value in the API, you'll get Custom.

However, I believe you can use the `Definition.GetSpecTypeId` method to distinguish between these Custom parameters.

So, you are absolutely correct: currently no support for these parameter types.

That support will come in a future major release of Revit.

It is in fact also already available in the current preview release.

#### <a name="3"></a>Determining Cut Tiles in Room

[How to get the number of cut tiles in a room using the Revit API](https://forums.autodesk.com/t5/revit-api-forum/parameter-type-changes-to-custom-in-revit-2021-1/m-p/9693021)

prasannamurumkar 129 Views, 8 Replies
‎2020-08-18 08:00 AM 
How to get number of cut tiles in a room using revit api?
 want to get number of tiles in a room using api.

After getting number of tiles want to get the cut tiles in percentage of tiles.

collected rooms in floor plan is their any way to get the tiles?

Tags (0)
Add tags
Report
Back to Topic Listing Previous Next 
MESSAGE 2 OF 9
jeremytammik
 Employee jeremytammik in reply to: prasannamurumkar
‎2020-08-18 10:54 AM 
This is an interesting task.

I am not aware of any support for this in the Revit API right out of the box.

Is this functionality provided by the Revit end user interface?

The API hardly ever provides functionality that is not available in the end user interface.

If not, you can definitely implement something yourself to retrieve the exact area that you wish to cover with tiles and run some kind of partitioning and optimisation algorithm on it to achieve the task you describe.

Here are some interesting links regarding this topic:

https://en.wikipedia.org/wiki/Euclidean_tilings_by_convex_regular_polygons
https://en.wikipedia.org/wiki/Tiling_with_rectangles
https://cs.stackexchange.com/questions/16661/tiling-an-orthogonal-polygon-with-squares
https://github.com/datagovsg/hextile

Please do let us know how you end up solving this.

Thank you!

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 3 OF 9
jeremytammik
 Employee jeremytammik in reply to: prasannamurumkar
‎2020-08-18 11:15 AM 
Another idea:

Implement a family instance representing one tile.
Use the Revit 2021 generative design functionality to place a maximum number of tiles on the desired surface.

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 4 OF 9
jeremytammik
 Employee jeremytammik in reply to: prasannamurumkar
‎2020-08-18 11:33 AM 
Continued researching this:

https://stackoverflow.com/questions/2675123/nesting-maximum-amount-of-shapes-on-a-surface

The proper term to search for is 'packing':

https://en.wikipedia.org/wiki/Packing_problems

https://github.com/topics/packing-algorithm

https://stackoverflow.com/questions/1213394/what-algorithm-can-be-used-for-packing-rectangles-of-dif...

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 5 OF 9
jeremytammik
 Employee jeremytammik in reply to: prasannamurumkar
‎2020-08-18 11:34 AM 
Do remember to appropriately align the polygon surface you wish to fill (or tile).

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 6 OF 9
RPTHOMAS108
 Advisor RPTHOMAS108 in reply to: prasannamurumkar
‎2020-08-20 01:38 AM 
Not to complicate the issue but is this tiles with grout tolerance allowance between the tiles?

If I were thinking of how I would do it then I'd place the tiles as solids in a given grid arrangement overlapping the room boundary, get the total volume of this grid arrangement = Vol1

Cut the room shape out of this tile arrangement to get the volume again = Vol2

You can then get the volume inside the room by deducting Vol2 from Vol1.

Separate out the disjoined solids in Vol2 (I believe there is a function for that).

Then you can also identify the cut tiles around the perimeter of the room in Vol2 because each of those have a volume less than a tile. This is the inverse volume so you deduct each of these from the tile size to give you each partial tile size and it's location. I speak in terms of volumes because volume are easy to determine from solids and then you can divide by tile thickness.

Also I'm using the inverse because I know the boundary of the room to use for deducting from the overall set. I could do it the other way around (deduct the excess from around the room) but I would have to allow a border around the room to ensure all the excess tiles are deducted and this seems slightly more complicated in determining that border width (probably a tile size plus an allowance).

I think where you start the tiling becomes arbitrary in a sense because rarely in reality are tiles set out on site to such precision. I imagine you usually start from the centre and work your way out to the edges so it gives the impression of symmetry.  I'm wondering how many variations you'd get by changing the tile offset i.e. if all the tiles are the same size do you cover the variations by offsetting through the tile unit dimensions? Do you then discount arrangements that lead to impractical tile sizes?

In the end what area of tiles do you need to fill a room of 10m2? Workmanship will play a large part in how many tiles you need so costs are usually not that specific. It'll likely be 10m2 worth of tiles + tolerance because I can't sell you half tiles (you cut them). Also I'll sell you boxes of 20 tiles not individual ones. Then there is colour variations in tiles perhaps you have a pettern and you want to know how many of each colour?

Add tags
Report
MESSAGE 7 OF 9
prasannamurumkar
 Advocate prasannamurumkar in reply to: jeremytammik
‎2020-08-20 06:15 AM 
Thank you for reply.will let you know once done.

Add tags
Report
MESSAGE 8 OF 9
prasannamurumkar
 Advocate prasannamurumkar in reply to: RPTHOMAS108
‎2020-08-20 07:00 AM 
Thank you for reply.we are notconsidering tolerance part,colour of diiferent tiles as of now

i am thinking to do some different way.

(Assming room is rectanular in our task) for this POC. for current work

I will get centre of rectangle.

then i will fire ref.intersector to both direction(x,y).

i know size of tiles(width,height).divide reference intersector by length.i will get no of tiles in that line.

if multiply by room dimension get total tiles.

If reference intersector is not divide equally then last tiles are cut tiles i will get total cut tiles by multiplying with length of room.

Add tags
Report
MESSAGE 9 OF 9
RPTHOMAS108
 Advisor RPTHOMAS108 in reply to: prasannamurumkar
‎2020-08-20 12:38 PM 
Sounds like an approach but I'm not sure you need a ReferenceIntersector for a rectangular room?

If room is rectangular then you just need to find the room boundaries with opposite or same vector direction and measure the distance between them to get your room size. Then it's just as you say dividing room dimension into tile dimension to give you a count. The remainder is then the portion of cut tile at either end:

5000 / 300 = 16.6666 recurring (16 full units)

0.6666 * 300 = 200 (rounded) so a strip of 100mm at each end to close the gap.

That kind of thing?

Grout tolerance likely means to artificially increase the tile size dimensions by the grout width. We do similar with reinforcement bars to get maximum spacing but we are then counting the gaps.

<pre class="code">
</pre>

####<a name="4"></a>Effect of Home Office on Meeting Culture

[Microsoft analyzed data on its newly remote workforce](https://hbr.org/2020/07/microsoft-analyzed-data-on-its-newly-remote-workforce)

####<a name="5"></a>Motivating Kids is Harder than Being Successful

An interesting motivational article by Jesse Hall on semi-unintentionally starting a successful YouTube channel and ending up earning money with it, all just to prove a point to his two teenage kids:

[How I Went From 0 to 70k Subscribers on YouTube in 1 Year &ndash; And How Much Money I Made](https://www.freecodecamp.org/news/how-to-grow-your-youtube-channel):

> In conclusion, I just want to say that anyone can do anything.
If you have (1) the proper motivation, (2) realistic expectations, and (3) you don't overwork yourself, you can be successful.

