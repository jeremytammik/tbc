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

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

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

###

####<a name="2"></a>

**Question:**

<pre class="code">
</pre>

<center>
<img src="img/.jpg" alt="" title="" width="445"/> <!-- 445 -->
</center>

**Answer:** 

**Response:** 

Thanks to 
for their input on this.

####<a name="3"></a> 

- Get the walls, ceiling and floor of a room?
  https://forums.autodesk.com/t5/revit-api-forum/get-the-walls-ceiling-and-floor-of-a-room/m-p/9915923

raised by Samuel Arsenault-Brassard and resolved by Yien Chao, Architect, BIM Director and Computational BIM Manager at 
[MSDL architectes](https://www.msdl.ca).

   Samuel.Arsenault-Brassard 276 Views, 12 Replies
‎12-04-2020 09:27 AM 
Get the walls, ceiling and floor of a room?
I've been able to get a list of all the elements that are in a room, but I am not able to figure out how to automatically obtain the walls, ceiling and floor that are associated with these rooms.

Is this information possible through the API?

And yes, I do understand that a wall, floor and ceiling may have a relationship with multiple rooms, not just one

 Solved by Yien_Chao. Go to Solution.

Tags (0)
Add tags
Report
12 REPLIES 
Sort: 
MESSAGE 2 OF 13
Yien_Chao
 Collaborator Yien_Chao in reply to: Samuel.Arsenault-Brassard
‎12-04-2020 11:40 AM 
hi Samuel,

try this maybe?

https://www.revitapidocs.com/2020/1fbe1cff-ed94-4815-564b-05fd9e8f61fe.htm

Tags (0)
Add tags
Report
MESSAGE 3 OF 13
Yien_Chao
 Collaborator Yien_Chao in reply to: Samuel.Arsenault-Brassard
‎12-04-2020 12:58 PM 
a simple boundingbox filter and a multicategory filter.. and voila!

2020-12-04_15-57-11.jpg

Tags (0)
Add tags
Report
MESSAGE 4 OF 13
Samuel.Arsenault-Brassard
 Contributor Samuel.Arsenault-Brassard in reply to: Yien_Chao
‎12-10-2020 05:17 AM 
One last question, is the bounding box actually a square box? I am wondering if it will capture rogue elements if the room is not square, for example a serpentine corridor.

BoundingBox.jpg

Tags (0)
Add tags
Report
MESSAGE 5 OF 13
jeremytammik
 Employee jeremytammik in reply to: Samuel.Arsenault-Brassard
‎12-10-2020 06:20 AM 
The bounding box is always a rectangular box, or, more precisely, a rectangular cuboid, with X, Y and Z axis-aligned faces.

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Tags (0)
Add tags
Report
MESSAGE 6 OF 13
Yien_Chao
 Collaborator Yien_Chao in reply to: Samuel.Arsenault-Brassard
‎12-10-2020 06:21 AM 
i dont think so...

https://www.revitapidocs.com/2020/3c452286-57b1-40e2-2795-c90bff1fcec2.htm

A three-dimensional rectangular box at ...

Tags (0)
Add tags
Report
MESSAGE 7 OF 13
Samuel.Arsenault-Brassard
 Contributor Samuel.Arsenault-Brassard in reply to: jeremytammik
‎12-10-2020 06:22 AM 
So, it would create a problem for a non-rectangular room right? (trying to detect related walls, floors and ceilings of a room)

Tags (0)
Add tags
Report
MESSAGE 8 OF 13
Yien_Chao
 Collaborator Yien_Chao in reply to: Samuel.Arsenault-Brassard
‎12-10-2020 06:39 AM 
i would try different approach to that.

try to use IsPointInRoom() instead? https://www.revitapidocs.com/2020/96e29ddf-d6dc-0c40-b036-035c5001b996.htm

for ceiling , wall and floor, you can just take the center points and project the points to room.

hope that may help.

Tags (0)
Add tags
Report
MESSAGE 9 OF 13
Samuel.Arsenault-Brassard
 Contributor Samuel.Arsenault-Brassard in reply to: Yien_Chao
‎12-10-2020 07:03 AM 
"project the points to room." I'm not sure I understand this part. I guess you can draw a line from the centroid of the room to the centroid of the walls?

The inherent problem I see is that the centroid of each wall/floor/ceiling is going to be outside each room since it is its shell. It's almost like we need to offset each room's volume to encompass the centroids of its shells.

WallMiddle.jpg

Tags (0)
Add tags
Report
MESSAGE 10 OF 13
Samuel.Arsenault-Brassard
 Contributor Samuel.Arsenault-Brassard in reply to: Samuel.Arsenault-Brassard
‎12-10-2020 07:03 AM 
Typo:

blue line = extent of *room*

Tags (0)
Add tags
Report
MESSAGE 11 OF 13
Yien_Chao
 Collaborator Yien_Chao in reply to: Samuel.Arsenault-Brassard
‎12-10-2020 08:25 AM 
example : choose center face of wall, then project the point according to normal by . Then use the isinroom() for each point, you should have 2 rooms for a single wall.

easier with ceilings and floor finishes.

Tags (0)
Add tags
Report
MESSAGE 12 OF 13
Samuel.Arsenault-Brassard
 Contributor Samuel.Arsenault-Brassard in reply to: Yien_Chao
‎12-10-2020 09:03 AM 
Interesting.

I can imagine lots of problems with this approach like a corridor that borders 20 rooms will only detect one room on each sides.

Same with a floor or ceiling being shared by multiple rooms. Especially if the designers were lazy and modeled the floors/ceilings to go through walls.

It's a very interesting problem, I will keep pondering on it. I'm actually surprised there's no direct way to do this directly in the API or in Revit schedules. Feels like every walls should know what rooms acost them and all rooms should know what surfaces bound them.

Tags (0)
Add tags
Report
MESSAGE 13 OF 13
Yien_Chao
 Collaborator Yien_Chao in reply to: Samuel.Arsenault-Brassard
‎12-11-2020 06:16 AM 
i think you can start another thread on the particuliar topic.

In the meantime, , i think the first question has been resolve.

####<a name="4"></a> Comic Sans is a Public Good

I am a bit of a typography junkie and pay far too much fanatic attention to that aspect of a text.

I sometimes even feel compelled to reformat a text more nicely to suit my taste just to make it more readable before I even start to take it in.

Until now, I have always gone for pretty traditional fonts and avoided Comic Sans.

I was surprised to learn that there are good reasons not to continue doing so, reading
about [the reason Comic Sans is a public good](https://www.thecut.com/2020/08/the-reason-comic-sans-is-a-public-good.html).

