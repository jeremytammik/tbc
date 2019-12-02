<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- Combine connected edge segments into one continuous line
  https://forums.autodesk.com/t5/revit-api-forum/combine-connected-edge-segments-into-one-continuous-line/m-p/9126098
  https://stackoverflow.com/questions/26889624/sorting-of-edges-of-polygon
  https://stackoverflow.com/search?q=sorting+edges+polygon
  https://stackoverflow.com/questions/13114378/sorting-vertices-of-a-polygon-in-ccw-or-cw-direction
  Sort and Orient Curves to Form a Contiguous Loop
  https://thebuildingcoder.typepad.com/blog/2013/03/sort-and-orient-curves-to-form-a-contiguous-loop.html
  sorting and orienting curves to form a contiguous loop
  One method that does part of the work that you should definitely be aware of is the Edge.AsCurveFollowingFace method that returns a curve corresponding to the edge oriented in its topological direction on the specified face. That is the simplest option and a good place to start.

- Russian letters doesn't export in lookup tables
  https://forums.autodesk.com/t5/revit-api-forum/russian-letters-doesn-t-export-in-lookup-tables/m-p/9116678
  I myself haven't done that yet but my colleague did - it works. Now when he switches to the Russian keyboard he can insert a csv to a family with Cyrillic text with no loss.

- [Moving a grid](https://forums.autodesk.com/t5/revit-api-forum/moving-a-grid/m-p/9115841)

twitter:

Cyrillic lookup tables, moving grids and combining face loop edges in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/combiningedges

Topics to start this week
&ndash; Why SVG?
&ndash; Internet privacy
&ndash; Pointless meetings research
&ndash; Cyrillic characters in lookup tables
&ndash; Moving a grid
&ndash; Combining edges...

linkedin:

Cyrillic lookup tables, moving grids and combining face loop edges in the #RevitAPI 

http://bit.ly/combiningedges

Topics to start this week:

- Why SVG?
- Internet privacy
- Pointless meetings research
- Cyrillic characters in lookup tables
- Moving a grid
- Combining edges...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Cyrillic Lookup, Moving Grids and Combining Edges

Let's start this week with the following topics
from [freecodecamp](https://www.freecodecamp.org) and 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160):

- [Why SVG, internet privacy and pointless meetings research](#2)
- [Cyrillic characters in lookup tables](#3)
- [Moving a grid](#4)
- [Combining edges](#5)

####<a name="2"></a> SVG, Internet Privacy and Research on Pointless Meetings

Here are a couple of recent articles I found interesting, pointed out in
the [freecodecamp](https://www.freecodecamp.org) newsletter:

- [Why you should use SVG images: how to animate your SVGs and make them lightning fast](https://www.freecodecamp.org/news/a-fresh-perspective-at-why-when-and-how-to-use-svg/)
- [The best personal privacy and security tools for 2019](https://www.freecodecamp.org/news/privacy-tools)
- [Pointless work meetings are really a form of therapy](https://www.bbc.com/news/education-50418317)

> "To err is human. But to really foul things up, you need a computer." &ndash; Paul Ehrlich

Back to the Revit API and some useful solutions discussed in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160):

####<a name="3"></a> Cyrillic Characters in Lookup Tables

There seem to be some issues handling Cyrillic characters in lookup tables, as pointed out in the thread 
on [Russian letters doesn't export in lookup tables](https://forums.autodesk.com/t5/revit-api-forum/russian-letters-doesn-t-export-in-lookup-tables/m-p/9116678).
Happily, however, at least a partial solution can be found:

> ... it works. Now, when he switches to the Russian keyboard, he can insert a CSV to a family with Cyrillic text with no loss.

####<a name="4"></a> Moving a Grid

Matt Taylor very kindly pointed out the solution to another old thread
on [moving a grid](https://forums.autodesk.com/t5/revit-api-forum/moving-a-grid/m-p/9115841):

> I would guess that the grid cannot be moved because it doesn't have a leader.
Use the `AddLeader` method to add a leader first.
You'll also probably need to do a `document.Regenerate` afterwards, before you set the leader points.

> I found the old [blog post that contains the code](https://adndevblog.typepad.com/aec/2016/04/forge-devcon-early-bird-revit-api-moving-a-grid-with-datumplane-method.html).
Seeing the code, I can confirm that my guess was correct.


####<a name="5"></a> Combining Edges

To wrap it up, Lucas Moreira shared a more complex solution
to [combine connected edge segments into one continuous line](https://forums.autodesk.com/t5/revit-api-forum/combine-connected-edge-segments-into-one-continuous-line/m-p/9126098):

**Question:** I am fetching the edge loops of a face.

When I do so, depending on the original geometry that created the solids, these loop segments can be composed of 2 or more subsegments to form an edge, cf., the orange and red semi-segments below:

<center>
<img src="img/edgesegment.png" alt="Two semi segments forming an edge" width="500"> <!--680-->
<p style="font-size: 80%; font-style:italic">Two semi segments forming an edge</p>
</center>

How can I identify and retrieve a continuous line for each edge?

Is there a way I can do that recursively regardless of the number of segments forming the edge?

I am coding for Revit 2019 and retrieving the segments using the `Face.EdgeLoops` property.

**Answer:** Two steps:

- Sort the edges so they in the right order
- Combine all groups of consecutive collinear edges into single ones

Here are hints from StackOverflow for the first:

- [Search results for 'sorting edges polygon'](https://stackoverflow.com/search?q=sorting+edges+polygon)
- [Sorting vertices of a polygon in CCW or CW direction](https://stackoverflow.com/questions/13114378/sorting-vertices-of-a-polygon-in-ccw-or-cw-direction)

The Building Coder provides some older thoughts on sorting and orienting curves to form a contiguous loop:

- [Sort and orient curves to form a contiguous loop](https://thebuildingcoder.typepad.com/blog/2013/03/sort-and-orient-curves-to-form-a-contiguous-loop.html)
- [Sorting face loop edges](https://thebuildingcoder.typepad.com/blog/2015/01/autodesk-internship-in-california-and-sorting-edges.html#3)

One method that does part of the work that you should definitely be aware of is the `Edge.AsCurveFollowingFace` method that returns a curve corresponding to the edge oriented in its topological direction on the specified face.
That is the simplest option and a good place to start.

**Response:** That helped me a lot and I am really close to a solution.

My scenario is slightly different as I am only using horizontally aligned edges of the exterior face of the geometry.
I think it actually makes it easier, because I don't have to sort the edges, but sort only the segments that would compose one straight edge.

I used the Python node on Dynamo to prototype what I need &ndash; the visual 3d space there helps me with debugging.

What I am doing is getting a list with all the `Edges.AsCurveLoops`, separating the horizontally aligned ones.
The algorithm evaluates that list and groups the curves to be joined into sublists, because I was already making sure that all the curves are oriented in the same direction.
I just need to pair the curve start points with their matching endpoints and use the unmatched points to form my 'new' undivided edge curve.
I will use the logic
in [sorting vertices of a polygon in CCW or CW direction](https://stackoverflow.com/questions/13114378/sorting-vertices-of-a-polygon-in-ccw-or-cw-direction) for
that.

I know that this code can be optimized, but I will post it here for the sake of completion.
It might help someone:

<pre class="prettyprint">
import clr
clr.AddReference('RevitAPI')
from Autodesk.Revit.DB import *
import Autodesk
clr.AddReference('RevitServices')
import RevitServices
from RevitServices.Persistence import DocumentManager
from RevitServices.Transactions import TransactionManager
clr.AddReference("RevitNodes")
import Revit
clr.ImportExtensions(Revit.GeometryConversion)
#from Revit import GeometryConversion as gp

import math

curves = IN[0]
#The next 2 methods will assume that the directions is known.
#The start point of a curve
def startPoint(curve):
    return curve.GetEndPoint(0)

#The end point of a curve
def endPoint(curve):
    return curve.GetEndPoint(1)
#Groups lines to be joined in sublists with the curves that have to be joined    
def joinCurves(list):
  comp=[]
  re=[]
  unjoined = []
  for c in curves:
    c = c.ToRevitType()
    match = False
    for co in comp:
      if startPoint(c).IsAlmostEqualTo(startPoint(co)) and endPoint(c).IsAlmostEqualTo(endPoint(co)):
        match = True
    if match:
      continue
    else:
      comp.append(c)      
      joined = []
      for c2 in curves:
        
        match = False
        c2 = c2.ToRevitType()
        for co in comp:
          if startPoint(c2).IsAlmostEqualTo(startPoint(co)) and endPoint(c2).IsAlmostEqualTo(endPoint(co)):
            match = True
        if match:
          continue
        else:
          if c2.Intersect(c) == SetComparisonResult.Disjoint:
            continue
          elif c2.Intersect(c) ==  SetComparisonResult.Equal:
            continue
          elif c2.Intersect(c) == SetComparisonResult.Subset:
            comp.append(c2)
            joined.append(c2.ToProtoType())
    joined.append(c.ToProtoType())
    re.append(joined)

  return re

result = joinCurves(curves)
OUT = result
</pre>

Many thanks to Lucas for raising and solving this interesting task.
