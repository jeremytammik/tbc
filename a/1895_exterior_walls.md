<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- /a/src/rvt/RevitFindExteriorWalls/
  https://thebuildingcoder.typepad.com/blog/2018/05/filterrule-use-and-retrieving-exterior-walls.html#comment-5289806219

  First round at [Retrieving All Exterior Walls](https://thebuildingcoder.typepad.com/blog/2018/05/drive-revit-via-a-wcf-service-wall-directions-and-parameters.html#8)
  - using the built-in wall function parameter `FUNCTION_PARAM` to filter for exterior walls, `IsExterior( w.WallType )` returns true
  The Revit API also provides a BuildingEnvelopeAnalyzer class that should help with this, but there seem to be problems using it, cf.:
  - Finding exterior walls by BuildingEnvelopeAnalyzer
  - Filtering exterior walls
  Yet another workaround was suggested: Place some room separation lines outside the building envelope and create a huge room around the entire building. Then, it’s just a matter of getting room boundaries, filtering out the RSLs, appending the remaining elements to your list, deleting the room and RSLs, and moving up to the next level. It may not work for some bad modelling cases, but catches most.
  After further discussion with the development team, they asked: Is the building model enclosed? It needs to be in order for the analyzer to work. In other words, do you have Roof and Floor elements to form enclosed spaces in the model?
  Ten days later:
  Several possible approaches to [retrieve all exterior walls](https://thebuildingcoder.typepad.com/blog/2018/05/filterrule-use-and-retrieving-exterior-walls.html#2)
  Now a discussion between ...

- Get the walls, ceiling and floor of a room?
  https://forums.autodesk.com/t5/revit-api-forum/get-the-walls-ceiling-and-floor-of-a-room/m-p/9915923

- typography:
  The Reason Comic Sans Is a Public Good
  https://www.thecut.com/2020/08/the-reason-comic-sans-is-a-public-good.html
  I am a bit of a typography junkie and pay far too much fanatic attention to that aspect of a text.
  I sometimes fell forced to reformat a text just to make it more readable before I even start to take it in.
  Until now, I have always gone for pretty standard fonts and avoided Comic Sans.
  I am surprised to learn that there are good reasons not to continue doing so:

twitter:

Retrieving outer boundaries of buildings and rooms, exterior walls and room bounding elements with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://autode.sk/exteriorwalls

Let's look at the outer boundaries of both buildings and rooms today
&ndash; Finding exterior walls continued
&ndash; Retrieving room bounding elements
&ndash; Comic Sans is a public good...

linkedin:

Retrieving outer boundaries of buildings and rooms, exterior walls and room bounding elements with the #RevitAPI

http://autode.sk/exteriorwalls

Let's look at the outer boundaries of both buildings and rooms today:

- Finding exterior walls continued
- Retrieving room bounding elements
- Comic Sans is a public good...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### Exterior Walls and Room Bounding Elements

Let's look at the outer boundaries of both buildings and rooms today;
note that both of these topics are continued in the subsequent follow-up post
on [boundary elements](https://thebuildingcoder.typepad.com/blog/2021/03/boundary-elements-and-stirrup-constraints.html):

- [Finding exterior walls continued](#2)
- [Retrieving room bounding elements](#3)
- [Comic Sans is a public good](#4)

####<a name="2"></a> Finding Exterior Walls Continued

We discussed some approaches
to [retrieve all exterior walls](https://thebuildingcoder.typepad.com/blog/2018/05/drive-revit-via-a-wcf-service-wall-directions-and-parameters.html#8) three years ago using different approaches:

- The built-in wall function parameter `FUNCTION_PARAM`, tested by the `IsExterior` API method
- Using the `BuildingEnvelopeAnalyzer` class
- Placing room separation lines outside the building envelope and creating a huge room around the entire building

Further aspects were added a week later in a second round
on [retrieving all exterior walls revisited](https://thebuildingcoder.typepad.com/blog/2018/05/filterrule-use-and-retrieving-exterior-walls.html#2).

The discussion now continued between Александр Пекшев or Alexander Pekshev of [ModPlus](https://modplus.org/en) and
Lucas Moreira in a series of [comments](https://thebuildingcoder.typepad.com/blog/2018/05/filterrule-use-and-retrieving-exterior-walls.html#comment-5289806219)
on that post:

Александр: There is one simple enough algorithm that allows you to find the exterior walls, even in an open loop.
The algorithm consists of two parts:

1. The main part &ndash; 
From the centre of the LocationCurve of the wall, two perpendicular rays (long lines) are shot on either side of the LocationCurve.
Determine the number of intersections of these rays with other walls.
If the number of intersections on one side is zero, this is an outer wall.
2. An additional part &ndash; 
The first part will not find all the walls, so the second pass should check the remaining walls at the intersections with the found exterior walls: if the wall with its ends connects to the two outer walls, it means it is also external

The working capacity of this algorithm is almost ideal.

Here are the results of my algorithm:

<center>
<img src="img/ap_exterior_walls_1.png" alt="Exterior walls" title="Exterior walls" width="400"/> <!-- 493 -->
<br/>
<img src="img/ap_exterior_walls_2.png" alt="Exterior walls" title="Exterior walls" width="400"/> <!-- 591 -->
</center>

I describe the algorithm in more detail in my article
on [Revit: Exterior Wall Search Algorithm](https://blog.modplus.org/index.php/11-revitapi/10-revit-find-external-walls-algorithm).

The code is provided in
the [RevitFindExteriorWalls GitHub repository](https://github.com/Pekshev/RevitFindExteriorWalls).

Lucas: how do you solve the problem of having an interior wall that touches 2 exterior walls?

<center>
<img src="img/ap_exterior_walls_3.png" alt="Exterior walls" title="Exterior walls" width="600"/> <!-- 857 -->
</center>

Александр: Today, I figured out the problem in your example.
In this case, it is necessary to improve the second part of the check and introduce some additional checks.
There will always be special cases.
The screenshot shows that three walls are connected there in one place &ndash; it seems to me that this needs to be taken into account.

In the 'main part' of the algorithm, if there are intersections on both sides of the wall, then it is internal.
And the connections at the ends of the wall are no longer important.

Lucas: Thanks for your response.
What I did is use 3 rays at different angles from each side: 45, 90 and 135 degrees.
If at least one of the rays doesn't hit anything, it's an exterior wall.
This took care of the special cases I had so far.
I believe shallower angles might be best.
So, maybe something like (5, 90, 175) will probably yield better, more correct results.
Another thing is I am not levelling the location lines.
Meaning that walls have to be on the same level to intersect each other.

<center>
<img src="img/ap_exterior_walls_4.png" alt="Exterior walls" title="Exterior walls" width="400"/> <!-- 454 -->
</center>

All blue walls are exterior, and the garage in the top right is on a different level of the rest.

Another approach that worked is using a convex hull to find at least one exterior wall (any location line that intersects the hull is an outermost wall).
Then, from the selected exterior wall, move counterclockwise, always trying to find a right turn; this is a bit slower, but needs no special cases to work with all the projects I've tested.

Александр: Convex hull is suitable if the outer walls form a precise chain of connections.
But it is not always the case.
In addition, there it will be necessary to collect these very connections, which is also not an easy task (there are many nuances).

While writing the message, another idea came to mind:

1. Determine the overall contour of all selected walls and find the midpoint
2. Around the contour with an indent from the contour, throw the rays towards the midpoint. Throw the rays through a certain step.

The first wall that intersects with the ray is the outer one.

Interesting idea! Need to try it out.

So, it is necessary to clarify that this is not a 100% solution and some special cases turn out to be expensive.

Many thanks to Alexander and Lucas for the interesting discussion.

####<a name="3"></a> Retrieving Room Bounding Elements

Moving inwards from the exterior walls into the building interior, I summarise the interesting discussion between
Samuel Arsenault-Brassard and Yien Chao, Architect, BIM Director and Computational BIM Manager
at [MSDL architectes](https://www.msdl.ca) on how
to [get the walls, ceiling and floor of a room](https://forums.autodesk.com/t5/revit-api-forum/get-the-walls-ceiling-and-floor-of-a-room/m-p/9915923):

**Question:** I've been able to get a list of all the elements that are in a room, but I am not able to figure out how to automatically obtain the walls, ceiling and floor that are associated with these rooms.

Is this information possible through the API?

And yes, I do understand that a wall, floor and ceiling may have a relationship with multiple rooms, not just one.

**Answer:** Try
the [BoundingBoxIntersectsFilter](https://www.revitapidocs.com/2020/1fbe1cff-ed94-4815-564b-05fd9e8f61fe.htm), maybe?

A simple bounding box filter and a multi-category filter... and voila!

<center>
<img src="img/room_bounding_elements_1.jpg" alt="Room bounding elements code" title="Room bounding elements code" width="800"/> <!-- 1829 -->
</center>

**Response:** One last question, is the bounding box actually a square box?
I am wondering if it will capture rogue elements if the room is not square, for example a serpentine corridor.

<center>
<img src="img/room_bounding_elements_2_bb.jpg" alt="Room bounding elements bb" title="Room bounding elements bb" width="400"/> <!-- 2257 -->
</center>

**Answer:** The [`BoundingBoxXYZ` class](https://www.revitapidocs.com/2020/3c452286-57b1-40e2-2795-c90bff1fcec2.htm)
is a three-dimensional rectangular box at an arbitrary location and orientation within the Revit model.

However, the bounding box returned by the BIM element property is always a rectangular box, or, more precisely, a rectangular cuboid, with X, Y and Z axis-aligned faces.

**Response:** So, it would create a problem for a non-rectangular room right? (trying to detect related walls, floors and ceilings of a room).

**Answer:** I would try different approach for that.
You could try to use the [`IsPointInRoom` method](https://www.revitapidocs.com/2020/96e29ddf-d6dc-0c40-b036-035c5001b996.htm) instead? 

For ceiling, wall and floor, you can just take the centre points and project the points to the room.

**Response:** You say, 'project the points to room.'
I'm not sure I understand this part.
I guess you can draw a line from the centroid of the room to the centroid of the walls?

The inherent problem I see is that the centroid of each wall/floor/ceiling is going to be outside each room since it is its shell. It's almost like we need to offset each room's volume to encompass the centroids of its shells.

<center>
<img src="img/room_bounding_elements_3_wall_middle.jpg" alt="Wall middle" title="Wall middle" width="400"/> <!-- 1794 -->
</center>

Typo:

- blue line = extent of *room*

**Answer:** Example: choose centre face of wall, then project the point according to normal by .
Then, use the `IsPointInRoom` method for each point; you should have 2 rooms for a single wall.
It is easier with ceilings and floor finishes.

**Response:** Interesting.

I can imagine lots of problems with this approach; for instance, a corridor that borders 20 rooms will only detect one room on each side.

Same with a floor or ceiling being shared by multiple rooms.
Especially if the designers were lazy and modelled the floors/ceilings to go through walls.

It's a very interesting problem, I will keep pondering it.
I'm actually surprised there's no direct way to do this directly in the API or in Revit schedules.
Feels like every walls should know what rooms accost them and all rooms should know what surfaces bound them.

**Answer:** I think you can start another thread on that particular topic.

In the meantime, I think the first question has been resolved.

Many thanks to Samuel and Yien for the interesting discussion, and to Yien for all his good suggestions!


####<a name="4"></a> Comic Sans is a Public Good

Moving away for the Revit API for a moment, I am a bit of a typography junkie and pay far too much fanatic attention to that aspect of a text.

I sometimes even feel compelled to reformat a text more nicely to suit my taste just to make it more readable before I even start to take it in.

Until now, I have always gone for pretty traditional fonts and avoided Comic Sans.

I was interested and surprised to learn that there are good reasons not to do so, though, reading
about [the reason Comic Sans is a public good](https://www.thecut.com/2020/08/the-reason-comic-sans-is-a-public-good.html).

