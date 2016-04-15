<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

More on Wall Opening Areas per Room #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim @AutodeskLabs


Håvard added some clarifications and background information on his sample code to determine wall opening areas per room: I have some questions on Håvard's initial approaches. Using a Boolean subtraction between transient solids from the actual wall and the family symbol sounds like the right track, but I don’t understand the need for the family instance, because the wall already provides the net geometry...

-->

### More on Wall Opening Areas per Room

Håvard added some clarifications and background information on his sample code
to [determine wall opening areas per room](http://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html):

**Question:** I have some questions on [Håvard's initial approaches](http://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-cut-area-for-a-specific-room.html).

Using a Boolean subtraction between transient solids from the actual wall and the family symbol sounds like the right track, but I don’t understand the need for the family instance, because the wall already provides the net geometry.

Regarding the real solution, which IMHO applies to ANY wall, not just curtain walls, you say, 'intersect a curtain wall solid with the room solid, where the intersected solid holds the opening area for that specific room'. However, they won’t intersect, will they, as they are just touching. Further work is needed to achieve an intersection. That’s why I said that some complex geometrical manipulation based on the full wall geometry and the subfaces retrieved via `SpatialElement*` is needed.

**Answer:** Regarding activating the correct family symbol first:

What I mean is to use the Opening Cut element in the Door family.
I could not find any reference to that element in the project.
Short of finding it I used the IFC utils instead, which returns the same curve loop, as far as I can tell.

<pre class="code">
&nbsp; <span class="teal">CurveLoop</span> curveLoop
&nbsp; &nbsp; = <span class="teal">ExporterIFCUtils</span>.GetInstanceCutoutFromWall(
&nbsp; &nbsp; &nbsp; fi.Document, wall, fi, <span class="blue">out</span> cutDir );
&nbsp;
&nbsp; <span class="teal">IList</span>&lt;<span class="teal">CurveLoop</span>&gt; loops = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">CurveLoop</span>&gt;( 1 );
&nbsp; loops.Add( curveLoop );
&nbsp;
&nbsp; <span class="blue">return</span> <span class="teal">ExporterIFCUtils</span>.ComputeAreaOfCurveLoops( loops );
</pre>

With stacked walls, it’s a little different story.

Instead of returning `ExporterIFCUtils.ComputeAreaOfCurveLoops`, I create an 'opening' solid from the curve loop using `out cutDir` and intersect that with the stacked wall member solid, returning the 'opening' solid for just that member.

Almost the same thing with embedded curtain walls but I intersect the room instead of the host wall.

Embedded curtain walls in stacked walls might be yet another story J
But it's probably the same or a combination.

Here is an embedded curtain wall in stacked wall:

<center>
<img src="img/SpatialElementGeometryCalculator91.png" alt="Curtain wall embedded in stacked wall" width="186">
</center>

Here we see the real challenge using `SpatialElementBoundarySubface` in the first place.
It will only pick up elements that have 'touching' surfaces.

So if there was a room in this side of the pictured stacked wall, the top stacked member will not be detected.

At AU2014, I remember Scott Conover did a demo of Revit API news.
He had a slide where a room extended up, and then sideways.
Either I'm mis-remembering this or I just haven't been able to recreate it yet.

One could also get the bounding box of the curtain wall, rotate the box to the wall and intersect the room or host.

This bounding box will be wider than the host wall due to this:

<center>
<img src="img/SpatialElementGeometryCalculator92.png" alt="Wider bounding box" width="250">
</center>

Unless the host wall is unusually wide...

So I guess the best thing is still what we do now done, get the profile and use a sufficient solid depth.

**Response:** Now I understand.
I found the same &ndash; basically the Room volume seems like a '2.5D object' with the 'profile' detected at the specified bottom elevation and it does NOT then detect any elements which may start 'higher'. I can understand why it may be so as it is hard to 'close' the volume in all cases w/o making some kind of simplification assumption.

**Comment:** Arif Hanif added another [comment](http://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html#comment-2622637976) to the post, saying, 'Very elegant; I was post processing with clipper. I will adapt my code for spaces in linked models. Håvard, awesome job!'

**Answer:** Thanks Arif.
Linked models is on my todo-list as well.
I would include the document of the subface element in the SpatialDataCache.
So when you postprocess the cache ids to get names you have the correct document readily available.
Other than that there are still improvements to be made.
As I remember, the method for getting the elevation profile from a curtain wall will not get an edited profile.
This is used to create the opening solid which intersects the room solid.
And the `GetLargestFaceArea` method could use a `face.Normal` check, though you would have to have really thick wall, like 7 feet or so, for it to fail.

**Response:** That is what I did store the document of the subface, as I am primarily working on spaces with linked room data to translate over to energy modelling tool.

Many thanks again to Miroslav, Arif and above all Håvard for your contributions!
