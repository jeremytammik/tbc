<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- find references intersector
  http://thebuildingcoder.typepad.com/blog/2010/01/findreferencesbydirection.html#comment-4067218487
  Return reference to ceiling face to place lighting fixture above a given point.
  
- FindReferencesByDirection sample by Joshua Lumley

- add joshua comment to main blog post
  http://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html#comment-4068022721
  http://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html#6

- email [Deleting Unnamed Non-Hosting Reference Planes] Austin Sudtelgte
  /a/doc/revit/tbc/git/a/zip/as_RemoveReferencePlanes.zip
  /a/doc/revit/tbc/git/a/img/as_RemoveReferencePlanes01.png
  /a/doc/revit/tbc/git/a/img/as_RemoveReferencePlanes02.png
  http://thebuildingcoder.typepad.com/blog/2014/02/deleting-unnamed-non-hosting-reference-planes.html#comment-3985629186
    Austin Sudtelgte
    As of 2017 this method no longer works, Revit doesn't throw an error when deleting a plane with something hosted to it. You will have to get all family instances in the document, check their host ID parameter, get all reference planes excluding the ones whose IDs we just collected, then delete. Dimensions that measure to a reference plane will be removed with the reference plane. The methods used above will still work to determine if one of those will be deleted or not. Alternatively, as of 2018.1, there is an Element.GetDependentElements() method that will return the same things as what is returned when deleting the element, just without having to delete it and roll back a transaction.
  http://thebuildingcoder.typepad.com/blog/2018/04/whats-new-in-the-revit-2019-api.html#comment-3985568426
    Austin Sudtelgte
    Point 2.3 Element.GetDependentElements() seems to require an element filter rather than having it be optional as noted above. It's possible (since the complete API doesn't seem to be posted to revitapidocs.com yet) that in 2019 the filter is optional but as of 2018.1 it was not.
  0738_melbourne_devlab.htm: Delete reference planes not hosting any elements: DeleteUnnamedNonHostingReferencePlanes.
    /a/doc/revit/tbc/git/a/0738_melbourne_devlab.htm#2
    Delete Reference Planes Not Hosting Any Elements
    Nick proposed deleting all reference planes that
    Have not been assigned a name, and
    Do not host any elements.
  1098_delete_unused_ref_plane.htm:5
    CmdDeleteUnusedRefPlanes in The Building Coder samples, migrated
    Construct a parameter filter to get only unnamed reference planes, i.e. reference planes whose name equals the empty string:
  We already have the global solution to purge unused objects:
  http://thebuildingcoder.typepad.com/blog/2018/08/purge-unused-using-performance-adviser.html
  However, let's pick up a specific sub-problem that may or may not be covered and examine it once again, for the third time:

Reference intersector and deleting reference planes in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/refintdelrefplane

We already looked at deleting unnamed non-hosting reference planes back in 2012 and 2014.
Some things have changed since then, and the old code requires fixing and updating.
Other interesting topics also want to be mentioned
&ndash; Embodyment workshop
&ndash; B&ouml;s Fulen mountain hike
&ndash; Using <code>ReferenceIntersector</code> to place lighting fixture on ceiling face
&ndash; Reformat stable representation string for dimensioning
&ndash; Deleting unnamed non-hosting reference planes updated...

--->

### Reference Intersector and Deleting Reference Planes

We already looked
at [deleting unnamed non-hosting reference planes](http://thebuildingcoder.typepad.com/blog/2014/02/deleting-unnamed-non-hosting-reference-planes.html) back
in 2012 and 2014.
Some things have changed since then, and the old code requires fixing and updating once again.

Other interesting topics also want to be mentioned:

- [Embodyment workshop](#2) 
- [B&ouml;s Fulen mountain hike](#3) 
- [Using `ReferenceIntersector` to place lighting fixture on ceiling face](#4) 
- [Reformat stable representation string for dimensioning](#5) 
- [Deleting unnamed non-hosting reference planes updated](#6) 

####<a name="2"></a> Embodyment Workshop

I attended a very nourishing dance workshop
with [Alain Allard](https://www.facebook.com/pg/Alain-Allard-5-Rhythms-Moves-into-Consciousness-179038505454123)
of [Moves into Consciousness](http://www.movesintoconsciousness.com), UK.

Alain is a dancer, fully licensed and practising psychotherapist, and meditates.

On that basis, he led us through two very fulfilling days of attending to the rhythms of the body and listening more clearly the rhythms of life, the voice of the heart and gut intelligence, practicing giving up the everyday habits of dulling our creative response to Life’s unfolding by numbing down and overthinking.

It was very enjoyable and enlivening indeed!

Thank you, Alain, and thank you, Bea, for inviting and organising!

####<a name="3"></a> B&ouml;s Fulen Mountain Hike

I followed up on the dance workshop with a mountain hike with Alex to
the [B&ouml;s Fulen](https://en.wikipedia.org/wiki/B%C3%B6s_Fulen),
highest mountain in the Swiss Canton Schwyz:

<center>
<img src="img/boes_fulen.png" alt="B&ouml;s Fulen" width="269"/>
</center>

In spite of its looks, it in in fact a pretty easy hike across the entire ridge a couple of hundred meters to the summit.

Unfortunately, the weather we had was not as good as in the picture above.
We had some sight, though much of it was of dramatic clouds forming all around us, and we got pretty wet walking out again.

Here is Alex' [15-minute GoPro video of the foggy walk back across the ridge](https://youtu.be/jPsrhhfrUGs), just before the rain started coming in fully:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/jPsrhhfrUGs" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center>


####<a name="4"></a> Using ReferenceIntersector to Place Lighting Fixture on Ceiling Face

Returning to the Revit API, Joshua Lumley shared a nice little example of using the `ReferenceIntersector` class to determine a point on and a reference to the ceiling face in a linked file to place a lighting fixture above a given point, in
his [comment](http://thebuildingcoder.typepad.com/blog/2010/01/findreferencesbydirection.html#comment-4067218487) on the old discussion of
the [`FindReferencesByDirection` method](http://thebuildingcoder.typepad.com/blog/2010/01/findreferencesbydirection.html), the precursor of `ReferenceIntersector`.

I generalised and simplified his code snippet to this `GetCeilingReferenceAbove` method, which I added
to [The Building Coder Samples](https://github.com/jeremytammik/the_building_coder_samples)
in the module `CmdDimensionWallsFindRefs.cs`:

<pre class="code">
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;reference&nbsp;to&nbsp;ceiling&nbsp;face&nbsp;to&nbsp;place&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;lighting&nbsp;fixture&nbsp;above&nbsp;a&nbsp;given&nbsp;point.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;GetCeilingReferenceAbove(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">View3D</span>&nbsp;view,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementClassFilter</span>&nbsp;filter&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementClassFilter</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Ceiling</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ReferenceIntersector</span>&nbsp;refIntersector&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ReferenceIntersector</span>(&nbsp;filter,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FindReferenceTarget</span>.Face,&nbsp;view&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;refIntersector.FindReferencesInRevitLinks&nbsp;=&nbsp;<span style="color:blue;">true</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ReferenceWithContext</span>&nbsp;rwc&nbsp;=&nbsp;refIntersector.FindNearest(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisZ&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;r&nbsp;=&nbsp;(<span style="color:blue;">null</span>&nbsp;==&nbsp;rwc)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;<span style="color:blue;">null</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;rwc.GetReference();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;r&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Windows.<span style="color:#2b91af;">MessageBox</span>.Show(&nbsp;<span style="color:#a31515;">&quot;no&nbsp;intersecting&nbsp;geometry&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;r;
&nbsp;&nbsp;}
</pre>

Here is a sample snippet showing how the method expects to be used:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">void</span>&nbsp;TestGetCeilingReferenceAbove(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">View3D</span>&nbsp;view&nbsp;=&nbsp;doc.GetElement(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;147335&nbsp;)&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">View3D</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Space</span>&nbsp;space&nbsp;=&nbsp;doc.GetElement(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;151759&nbsp;)&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Space</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;center&nbsp;=&nbsp;(&nbsp;(<span style="color:#2b91af;">LocationPoint</span>)&nbsp;space.Location&nbsp;).Point;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;r&nbsp;=&nbsp;GetCeilingReferenceAbove(&nbsp;view,&nbsp;center&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Populate&nbsp;these&nbsp;as&nbsp;needed:</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;startPoint&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FamilySymbol</span>&nbsp;sym&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;doc.Create.NewFamilyInstance(&nbsp;r,&nbsp;startPoint,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY,&nbsp;sym&nbsp;);
&nbsp;&nbsp;}
</pre>

####<a name="5"></a> Reformat Stable Representation String for Dimensioning

By the way, Joshua also added an important note on how 
to [reformat the stable representation string for dimensioning](http://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html#6) in the discussion
on the [reference stable representation magic voodoo](http://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html).

Many thanks for your helpful contributions, Joshua!

####<a name="6"></a> Deleting Unnamed Non-Hosting Reference Planes Updated 

Another important update was initiated by Austin Sudtelgte, who pointed out that the approach described in 2014
for [deleting unnamed non-hosting reference planes](http://thebuildingcoder.typepad.com/blog/2014/02/deleting-unnamed-non-hosting-reference-planes.html) no longer works.
[Austin adds](http://thebuildingcoder.typepad.com/blog/2014/02/deleting-unnamed-non-hosting-reference-planes.html#comment-3985629186):

> As of 2017 this method no longer works, because Revit doesn't throw an error when deleting a plane with something hosted to it. You will have to get all family instances in the document, check their host ID parameter, get all reference planes excluding the ones whose IDs we just collected, then delete. Dimensions that measure to a reference plane will be removed with the reference plane. The methods used above will still work to determine if one of those will be deleted or not. Alternatively, as of 2018.1, there is an `Element.GetDependentElements` method that will return the same things as what is returned when deleting the element, just without having to delete it and roll back a transaction.

He also points out in
his [comment on *What's New in the Revit 23019 API*](http://thebuildingcoder.typepad.com/blog/2018/04/whats-new-in-the-revit-2019-api.html#comment-3985568426):

> [Point 2.3 on finding element dependencies](http://thebuildingcoder.typepad.com/blog/2018/04/whats-new-in-the-revit-2019-api.html#4.2.3) with
`Element.GetDependentElements` seems to require an element filter rather than having it be optional as noted above. It's possible that in 2019 the filter is optional, but in 2018.1 it was not.

This is relevant 
in [The Building Coder sample](https://github.com/jeremytammik/the_building_coder_samples)
`CmdDeleteUnusedRefPlanes.cs` to delete unnamed non-hosting reference planes, originally discussed and implemented at
the [Melbourne DevLab](http://thebuildingcoder.typepad.com/blog/2012/03/melbourne-devlab.html#2) in 2012 to delete all reference planes that:

- Have not been assigned a name, and
- Do not host any elements.

The original approach stopped working and was [updated and added to The Building Coder samples in 2014](http://thebuildingcoder.typepad.com/blog/2014/02/deleting-unnamed-non-hosting-reference-planes.html),
when the `Delete` method overload taking an `Element` argument was deprecated in Revit 2014 and the deletion of a reference plane hosting elements started throwing an exception.

Austin shared his version of the broken command, plus his new working solution.

I added them both 
to [The Building Coder Samples](https://github.com/jeremytammik/the_building_coder_samples)
[module `CmdDeleteUnusedRefPlanes.cs`](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdDeleteUnusedRefPlanes.cs).

Yet again, I discovered a couple of optimisation possibilities that I very frequently keep pointing out and also added to Austin's code:

- Iterate directly over the filtered element collectors instead of converting it to a list, which wastes time and space for no benefit whatsoever.
- Use a `Dictionary` instead of a `List`, since it is much more efficient looking up a dictionary key than searching an unsorted list for a specific entry.
- Only instantiate the transaction if it is actually going to be used.

You can see exactly what I modified in
the [commit highlighting those changes](https://github.com/jeremytammik/the_building_coder_samples/commit/21ef175572ee1b4ac7933af391135004c8889343).

Here Austin's solution in its current form:

<pre class="code">
[<span style="color:#2b91af;">Transaction</span><span style="color:#2b91af;">Attribute</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">CmdDeleteUnusedRefPlanes</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;uiapp.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;refplaneids
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">ReferencePlane</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToElementIds();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">TransactionGroup</span>&nbsp;tg&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TransactionGroup</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tg.Start(&nbsp;<span style="color:#a31515;">&quot;Remove&nbsp;unused&nbsp;reference&nbsp;planes&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;instances
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:#2b91af;">ElementId</span>,&nbsp;<span style="color:blue;">int</span>&gt;&nbsp;toKeep&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:#2b91af;">ElementId</span>,&nbsp;<span style="color:blue;">int</span>&gt;();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;i&nbsp;<span style="color:blue;">in</span>&nbsp;instances&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Ensure&nbsp;the&nbsp;element&nbsp;is&nbsp;hosted</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;i.Host&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;hostId&nbsp;=&nbsp;i.Host.Id;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Check&nbsp;list&nbsp;to&nbsp;see&nbsp;if&nbsp;we&#39;ve&nbsp;already&nbsp;added&nbsp;this&nbsp;plane</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!toKeep.ContainsKey(&nbsp;hostId&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;toKeep.Add(&nbsp;hostId,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;++toKeep[hostId];
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Loop&nbsp;through&nbsp;reference&nbsp;planes&nbsp;and&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;delete&nbsp;the&nbsp;ones&nbsp;not&nbsp;in&nbsp;the&nbsp;list&nbsp;toKeep.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;refid&nbsp;<span style="color:blue;">in</span>&nbsp;refplaneids&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!toKeep.ContainsKey(&nbsp;refid&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Removing&nbsp;plane&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;doc.GetElement(&nbsp;refid&nbsp;).Name&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Ensure&nbsp;there&nbsp;are&nbsp;no&nbsp;dimensions&nbsp;measuring&nbsp;to&nbsp;the&nbsp;plane</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;doc.Delete(&nbsp;refid&nbsp;).Count&nbsp;&gt;&nbsp;1&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Dispose();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tg.Assimilate();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
}
</pre>

Many thanks to Austin for picking up and fixing this!

