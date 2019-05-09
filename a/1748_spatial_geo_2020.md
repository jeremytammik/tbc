<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

</head>

<!---

- 2019 Autodesk show reels are out

- Add-In Manager Missing in Revit 2020 SDK
  https://forums.autodesk.com/t5/revit-api-forum/revit-2020-addin-manager-missing/m-p/8774075

- https://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html#comment-4452689539
  can you help dan with his question on your SpatialElementGeometryCalculator sample?
  https://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html#comment-4452599622
  dan confirmed that he fixed it himself in a subsequent comment
  i migrated the add-in to Revit 2020 and integrated his changes in the github repo
  so, really, all you could help with is to confirm the change that dan suggested, subtracting the opening area:
  https://github.com/jeremytammik/SpatialElementGeometryCalculator/compare/2020.0.0.1...2020.0.0.2
 
- english spelling
  the english word 'fish' could theoretically be spelled 'ghoti':
  f = gh from tough
  i = o from women
  sh = ti from satisfaction
  english spelling is pretty arbitrary sometimes...

twitter:

Autodesk show reels, spatial element geometry calculator and Add-In Manager update for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/spatialgeo2020

New Autodesk show reels, a solution to the lack of an add-in manager in the Revit 2020 SDK, an update for the SpatialElementGeometryCalculator and an interesting observation on English spelling
&ndash; 2019 Autodesk show reels
&ndash; The Add-In Manager for Revit 2019 still works
&ndash; Spatial element geometry calculator update
&ndash; English spelling...

linkedin:

 the #RevitAPI #bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

Autodesk show reels, spatial element geometry calculator and Add-In Manager update for the #RevitAPI

http://bit.ly/spatialgeo2020

New Autodesk show reels, a solution to the lack of an add-in manager in the Revit 2020 SDK, an update for the SpatialElementGeometryCalculator and an interesting observation on English spelling:

- 2019 Autodesk show reels
- The Add-In Manager for Revit 2019 still works
- Spatial element geometry calculator update
- English spelling...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

-->

### Spatial Geometry, Add-In Manager and Show Reels

New Autodesk show reels, a solution to the lack of an add-in manager in the Revit 2020 SDK, an update for the SpatialElementGeometryCalculator and an interesting observation on English spelling:

- [2019 Autodesk show reels](#2) 
- [The Add-In Manager for Revit 2019 still works](#3) 
- [Spatial element geometry calculator update](#4)
- [Håvard's new suggestion](#4.1)
- [English spelling](#5) 

####<a name="2"></a> 2019 Autodesk Show Reels

Autodesk produces show reels highlighting exciting uses of the products in various domains.

The 2019 updates have now been released on YouTube and may come in handy to fill some time waiting for the main show to begin:

- [Corporate](https://youtu.be/KWvPfmjwjOM)
- [M&amp;E &ndash; Media and Entertainment ](https://youtu.be/bUwbe7oIMxU)
- [MFG &ndash; Manufacturing ](https://youtu.be/361wG7e8lCg)
- [AEC &ndash; Architecture Engineering and Construction](https://youtu.be/Kuqg0OitrSc)

####<a name="3"></a> The Add-In Manager for Revit 2019 Still Works

As several people already pointed out, the Add-In Manager used by many developers in their application testing workflow is missing in the Revit 2020 SDK.

A simple solution was now suggested and confirmed in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit 2020 add-in manager missing](https://forums.autodesk.com/t5/revit-api-forum/revit-2020-addin-manager-missing/m-p/8774075):

**Question:** I downloaded the Revit 2020 SDK from the [Revit development centre](https://www.autodesk.com/developer-network/platform-technologies/revit).

I see that the package is missing the Add-In Manager.

Does anyone know where I can find it for Revit 2020?

**Answer:** I am using the old version.

It can also be used on Revit 2020.

You can try it out yourself.

The installation path is the same: *C:\ProgramData\Autodesk\Revit\Addins\2020*.

Please notify if successful.

**Response:** Thanks for the reply and pointing in the right direction.

I copied the `.dll` and `.addin` from the 2019.2 SDK into *C:\ProgramData\Autodesk\Revit\Addins\2020*, and all works as it should.


####<a name="4"></a> Spatial Element Geometry Calculator Update

Dan pointed out an inconsistency in
the [SpatialElementGeometryCalculator](https://github.com/jeremytammik/SpatialElementGeometryCalculator)
in [his comment](https://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html#comment-4452599622)
on [determining wall opening areas per room](https://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html) that led me to migrate the add-in to Revit 2020 and integrate his code fix:

Dan: If I understand the sample model provided correctly, the room height is taken into account and not the full wall height.
So, in room 7, even though the wall is 4 m high, only 3 m will be taken into account, since that is the height of the room.
Fair enough; this would mean 30 m<sup>2</sup> gross for each wall, since they are all 10 m long inside the room.
However, for the wall with the opening in room 7, shouldn't the gross area be 30 m<sup>2</sup>, and the net 2 m<sup>2</sup> less (28 m<sup>2</sup>)?

If so, for the total output, I would expect these values:

<pre>
Room 7; Wall3(308817): net 28; opening 2; gross 30
</pre>

instead of

<pre>
Room 7; Wall3(308817): net 30; opening 2; gross 32
</pre>

I fixed the issue in the following line by subtracting the opening area, since `GetSubface().Area` apparently returns the gross area, not the net:

<pre class="code">
  spatialData.dblNetArea&nbsp;=&nbsp;<span style="color:#2b91af;">Util</span>.sqFootToSquareM(
  &nbsp;&nbsp;spatialSubFace.GetSubface().Area&nbsp;-&nbsp;openingArea&nbsp;);&nbsp;
</pre>

Many thanks to Dan for pointing this out!

It prompted me to migrate the sample from Revit 2016 to Revit 2020 and add his fix
in [SpatialElementGeometryCalculator release 2020.0.0.2](https://github.com/jeremytammik/SpatialElementGeometryCalculator/releases/tag/2020.0.0.2).

Here are the diffs:

- [Migration from Revit 2016 to Revit 2020 API](https://github.com/jeremytammik/SpatialElementGeometryCalculator/compare/2016.0.0.3...2020.0.0.0)
- [Fixing the assembly architecture mismatch warning](https://github.com/jeremytammik/SpatialElementGeometryCalculator/compare/2020.0.0.0...2020.0.0.1)
- [Integrating Dan's code fix](https://github.com/jeremytammik/SpatialElementGeometryCalculator/compare/2020.0.0.1...2020.0.0.2)

<center>
<img src="img/SpatialElementGeometryCalculator2Test3d.png" alt="SpatialElementGeometryCalculator test model 3D view" width="400">
</center>

####<a name="4.1"></a> Håvard's New Suggestion

Håvard Leding of [Symetri](https://www.symetri.com) {whose last name used to be Dagsvik) very
kindly [answered Dan's comment](https://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html#comment-4454441990),
pointing out that the current implementation might possibly bear some fundamental improvement:

> Hi Dan, seems you found a bug there... sorry about that :-) &nbsp;
In our final app, the `openingArea` property is applied later on.
I missed that here in the shortened version.
Glad its still of use to someone.
Doing this again today, I might use the `GetDependentElements` method more.
First, get the openings like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;wall&nbsp;openings&nbsp;using&nbsp;GetDependentElements</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;GetOpenings(&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;wall&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementMulticategoryFilter</span>&nbsp;emcf
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementMulticategoryFilter</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;()&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(<span style="color:#2b91af;">BuiltInCategory</span>.OST_Windows),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(<span style="color:#2b91af;">BuiltInCategory</span>.OST_Doors)&nbsp;}&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;wall.GetDependentElements(&nbsp;emcf&nbsp;);
&nbsp;&nbsp;}
</pre>

> Then, for each dependent of interest, use `GetDependentElements` again to get the `openingSolid` as described here in the recent suggestion how
to [determine exact opening by demolishing](https://thebuildingcoder.typepad.com/blog/2019/03/determine-exact-opening-by-demolishing.html).


####<a name="5"></a> English Spelling

Let's finish off on a completely different topic, the baffling idiosyncrasies of English spelling.

The most extreme example I know is the suggestion that the English word 'fish' could just as well be spelled 'ghoti':

- f = gh from tough
- i = o from women
- sh = ti from satisfaction

English spelling sure is pretty arbitrary sometimes...
