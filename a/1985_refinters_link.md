<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js"></script>
</head>

<!---

- /Users/jta/a/doc/revit/tbc/git/a/img/referenceintersector_with_link_that_moved.png

- ReferenceIntersector+BoundingBoxIntersectsFilter fails on RevitLinkInstance ?
  https://forums.autodesk.com/t5/revit-api-forum/referenceintersector-boundingboxintersectsfilter-fails-on/td-p/11782120

twitter:

the @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS

&ndash;
...

linkedin:

#BIM #DynamoBim #AutodeskAPS #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Reference Intersector with Links



####<a name="2"></a>

A lot of interesting information and experience on using the reference intersector in conjunction with other filters and in linked files was discussed
by Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
and Thomas Mahon of [](https://bimorph.com) in the question
on [`ReferenceIntersector` + `BoundingBoxIntersectsFilter` fails on `RevitLinkInstance`](https://forums.autodesk.com/t5/revit-api-forum/referenceintersector-boundingboxintersectsfilter-fails-on/td-p/11782120) raised
by Chris Hanschen of [LKSVDD architecten](https://www.lksvdd.nl):

**Question:** I am a big fan of the ReferenceIntersector filter, finding elements like that is great!

In this post Jeremy Tammik explains this slow filter and the use of combinations of filters:

https://forums.autodesk.com/t5/revit-api-forum/how-to-use-the-elementintersectselementfilter-from-th...

The combination of BoundingBoxIntersectsFilter and ReferenceIntersector can boost the performance of the slow ReferenceIntersector Filter. Even when using linked-models, the ReferenceIntersector can find the link (at first) and the ReferenceWithContext can give you information about the element in the RevitLinkInstance, works great!



When the RevitLinkInstance is NOT moved, the combination of ReferenceIntersector+BoundingBoxIntersectsFilter works great, but when the RevitLinkInstance is moved, no elements are found.



It looks like the BoundingBoxIntersectsFilter not only filters elements in the opened model, but also the elements in the linked model, by check of coordinates or something.

But when the RevitLinkInstance is moved in your opened models, the coordinates are different, the BoundingBoxIntersectsFilter fails.



See attached image, the linked model is not found within the BoundingBoxIntersectsFilter, by the ReferenceIntersector, but is is there!

The Ray realy hits the linked model, the linked element, within the (local) BoundingBoxIntersectsFilter, but nothing is found.

Looks like the ReferenceIntersector+BoundingBoxIntersectsFilter fails on moved RevitLinkInstance.



Am I doing something wrong or does the BoundingBoxIntersectsFilter fails om then moved (elements in the) RevitLinkInstance ??


referenceintersector_with_link_01.png

ReferenceIntersector and BoundingBoxIntersectsFilter.png

BoundingBoxIntersectsFilter fail ReferenceIntersector RevitLinkInstance

jeremy.tammik

Interesting question. I asked the development team for you whether this behaviour is known, understood and intentional.

They explain:  Since the filters mentioned do not know about the link's transform, they assume no transform exists.   You can't really affect the ElementInteresectsElementFilter, as it looks directly at the Element's geometry within the link, but you can apply the transform to the bounding box for BoundingBoxIntersectsFilter before you pass it in.  Note that rotations might cause a different size bounding box to be generated as the input bounding box is always aligned with whatever coordinate system is in the host model.


c.hanschen

"but you can apply the transform to the bounding box for BoundingBoxIntersectsFilter before you pass it in"
That would only be a possibility when when you are just investigating this one link, no other links (with different transform) or elements in the opened model with the same Ray.



"Since the filters mentioned do not know about the link's transform"

Why not? When hitting a RevitLinkInstance, the transform is known, can't this be used?



The ReferenceIntersector is a great way to find ALL elements (multiplex links, with different transforms, and model elements) at the same time, with just 1 Ray. But it is a slow filter, so you want to be able to speed this up by using BoundingBoxIntersectsFilter. The Elements are there! the RevitLinkInstance should be found.



Is there a way to get this working properly?


RPTHOMAS108

Filters work within the document so the bounding box is relevant to the space within that i.e. within link document. The link document doesn't know where it is placed in the host document since that is information associated with the link instance in the host document. Hence that is why the filter doesn't know.



The ReferenceIntersector is not a slow filter or any kind of filter it is a utility to strike something with a ray based on origin and direction. The ReferenceIntersector works within the current document where the various links take up a final specific known position (so it is not analogous to an element filter or limitation of such).



The RefereceIntersector will only find things based upon visibility of the elements in the 3D view provided. So that is one way of filtering beforehand what the RefereceIntersector strikes. Although visibility control of elements in links via the API is fairly limited still. Regarding links the suggestion I believe is to transform the bounding box into the space of the linked document.



Not sure I follow your issue with transforming the bounding box to the link space. If you have a link instance in multiple positions then you only need to check one instance of that link. Since transforming the box into the link document will result in the same target position in the link document? Depends how you are using the bounding box filter to begin with? You are interested in a certain region so what link instances are in that region to transform the box into and check for initial elements?



The only real limitation as mentioned is that the bounding box is parallel to the document space and this may not be the same in the linked document as the host document. Since object you use is 'Outline' and that doesn't have a transform to give you an alternative orientation for the model it is used in. However it is a rough exercise for an initial coarse result. I would probably create the bounding box corner points in the host document for a box aligned with the link transform axis, so that they match up when transformed into the link. If you create the corner points based on a box aligned with the host document then it may not represent the same when those points are transformed into the associated linked document positions.

c.hanschen

My issue: combination of BoundingBoxIntersectsFilter and ReferenceIntersector fails for LinkedDocumentElements, fails only when LinkedInstance is moved.
I understand why, understand this is not easily solved, but for now I can't use this combination of filters.
The use of only ReferenceIntersector gives me the result I need, but when firering more than x1000 rays, the use of only ReferenceIntersector is way slower than the combination of these 2 filters.
The geometry is there, at the location given, in the 3D view given, so i did not for see this behavior.

Still a huge fan of the ReferenceIntersector Filter, but a little bit disappointed in the combination of these filters 🙂

RPTHOMAS108

You know the link instance position via its transform so you know where the bounding box needs to be relocated to suit that. The below code demonstrates using a logical or filter with bounding box for each inverse link instance location.



I tested this on two instances of the same link relocated from where they were inserted (X=0, Y=0):

230503a.PNG

referenceintersector_with_link_02.png


    Private Function Obj_230305a(ByVal commandData As Autodesk.Revit.UI.ExternalCommandData,
ByRef         Dim UIApp As UIApplication = commandData.Application
        Dim UIDoc As UIDocument = commandData.Application.ActiveUIDocument
        If UIDoc Is Nothing Then Return Result.Cancelled Else
        Dim IntDoc As Document = UIDoc.Document

        Dim FEC As New FilteredElementCollector(IntDoc)
        Dim RvtLnks As List(Of RevitLinkInstance) =
              FEC.OfClass(GetType(RevitLinkInstance)).OfType(Of RevitLinkInstance).ToList

        Dim BBOrds As XYZ() = New XYZ(1) {New XYZ(-11.3, 10, -1), New XYZ(2.3, 31.9, 0.1)}
        Dim EFs As ElementFilter() = New ElementFilter(RvtLnks.Count - 1) {}

        For i = 0 To RvtLnks.Count - 1
            Dim RInst As RevitLinkInstance = RvtLnks(i)
            Dim Tinv As Transform = RInst.GetTransform.Inverse
            Dim Min As XYZ = Tinv.OfPoint(BBOrds(0))
            Dim Max As XYZ = Tinv.OfPoint(BBOrds(1))
            Dim OL As New Outline(Min, Max)
            EFs(i) = New BoundingBoxIsInsideFilter(OL)
        Next

        Dim LorF As New LogicalOrFilter(EFs.ToList)
        Dim V3D As View3D = TryCast(UIDoc.ActiveGraphicalView, View3D)
        If V3D Is Nothing Then Return Result.Cancelled Else

        Dim REFInt As New ReferenceIntersector(LorF, FindReferenceTarget.Element, V3D) With {.FindReferencesInRevitLinks = True}

        Dim R As Reference = Nothing
        Try
            R = UIDoc.Selection.PickObject(Selection.ObjectType.Element, "Pick ray line")
        Catch ex As Exception
            Return Result.Cancelled
        End Try
        Dim CE As CurveElement = TryCast(IntDoc.GetElement(R), CurveElement)
        If CE Is Nothing Then Return Result.Cancelled Else
        Dim LN As Line = TryCast(CE.GeometryCurve, Line)
        If LN Is Nothing Then Return Result.Cancelled Else

        Dim Res As List(Of ReferenceWithContext) = REFInt.Find(LN.GetEndPoint(0), LN.Direction)

        For i = 0 To Res.Count - 1
            Dim RwC As ReferenceWithContext = Res(i)
            Dim Rf As Reference = RwC.GetReference
            Debug.WriteLine($"{Rf.ElementId.IntegerValue}, {Rf.LinkedElementId?.IntegerValue}, {RwC.Proximity}")
        Next
        Return Result.Succeeded

    End Function


Output:



432129, 432128, 6.68864267244516
432129, 432128, 3.38973099408717
432129, 432168, 13.2864660291611
432129, 432168, 9.98755435080315
432142, 432128, 17.9884761277055
432142, 432128, 14.6895644493475
432142, 432168, 24.5862994844214
432142, 432168, 21.2873878060635


Eight faces, two linked element ids in each of the two element ids representing link instances i.e. four unique permutations of ElementId, LinkedElementId.



Can be noted that the faces are not returned in order of proximity.



Note no link instance in position where bounding box points are transformed to in host document:

230503b.PNG

referenceintersector_with_link_03.png


thomas

The limitations you've encountered are explained in the ReferenceIntersector documentation.



The long and short of it is you have one option available to get reliable results where links are concerned, and that's using the ReferenceIntersector(Autodesk.Revit.DB.View3D view3d) overload only. This method is really slow, so you you could follow what @RPTHOMAS108 suggested, 'transform' the BB to the location of the link instance then provide your element filter. The problem with this is you'll need to do this for each link in your document and you'll end up accumulating live elements with each ReferenceIntersector + link elements from other links if their origin-to-origin location happens to coincide with your transformed BB. Subsequently, you'll have the additional problem of identifying duplicates and omitting them from your combined list of results once all your ReferenceIntersector's have run.



thomas_0-1678528264706.png

referenceintersector_with_link_04.png


RPTHOMAS108

In reality you need only one bounding box filter per link instance which could be combined into a logical or filter as above. So not multiple reference intersections just a single one or one for all the links combined and one for other elements (non-linked). You would need to include a non transformed bounding box if looking for elements not in links.



All of the bounding boxes used in the or filter will be overlapping in a similar position if they come from a single bounding box position that the ReferenceIntersector is using to focus on in the model. That transformed bounding box remote position will wrongly capture elements in the model away from the focus of the ReferenceIntersector but the ReferenceIntersector itself will rule them out. I think further testing is required for cases where the link instance is moved parallel with the ReferenceIntersector ray. It would probably be a good idea to separate out the links into their own ReferenceIntersector and limit the length of the ray. I think there are probably simple cases we can prove where the above may go wrong.



If using multiple ReferenceIntersectors then identifying duplicates would not be a major issue. In any scenario we get back an ElementId and a linked ElementId so a comparison based on that combination could be done with Distinct/Union etc.





thomas

That's a good point but you're only going to get a unique element using FindNearest(). If you use Find() then you'll end up with duplicates as the BBs in a LogicOrFilter are iterated over. I guess it could be solved by adding all the BBs together, if they happen to intersect. If they don't but are in close proximity then the same problem will occur. Untransformed BBs also wont help to preclude linked elements (if the flag is set); for example if the origin-to-origin location of the link happens to coincide with the BB, linked elements will end up in the result. There's no good way to go about it really, but your suggestion to transform the bb is the way to go if thats what the @c.hanschen needs otherwise see if there are better ways of achieving your end-goal.

RPTHOMAS108

Yes it is a bit more complicated than initially considered.



We get back references and I think it is true to say that the elements not in links can be found via Reference.LinkedElementId being -1.



So we could perhaps reduce the problem to two ReferenceIntersectors one for the links where we have to filter out the non-linked elements. The second ReferenceIntersector would not have the flag set so only find elements in the main document.



I did some simple testing of problematic cases I thought would cause issues but didn't encounter such issues in practice. However I can't foresee every scenario used so suggest @c.hanschen satisfies themselves with testing the specific cases they are likely to encounter.

thomas
The ReferenceIntersectors accepting linked elements will still collect any live elements in the host document if the ray hits, so removing duplicates post-process would still be necessary given this could occur in any given model.



I've just discovered another problem using a filter with linked elements flagged: any live elements will be returned 3 times, irrespective of whether it passes the filter. Linked elements seem to be returned twice (which might make sense assuming the ray hits both sides of a wall element, but when omitting linked elements, only one live element is returned instead so its inconsistent?). Not sure if this is a bug CC @jeremy.tammik?



Looks like the performance hit from ReferenceIntersector(Autodesk.Revit.DB.View3D view3d) overload is the lesser of all evils!



public IList<int> GetElementsFromRayshoot(Document document, IList<BoundingBoxXYZ> boundingBoxes, XYZ origin, XYZ rayDirection)
{
  var filters = new List<ElementFilter>();
  foreach (var boundingBox in boundingBoxes)
  {
    var outline = new Outline(boundingBox.Min, boundingBox.Max);

    filters.Add(new BoundingBoxIntersectsFilter(outline))
  }

  var logicFilter = new LogicalOrFilter(filters);

  var referenceIntersector = new ReferenceIntersector(logicFilter, FindReferenceTarget.Element, document.ActiveView)
  {
    FindReferencesInRevitLinks = True
  };

  var refWithContextResults = referenceIntersector.Find(origin, rayDirection);

  var elementIds = new List<Int>();
  foreach (var refWithContext in refWithContextResults)
  {
    var reference = refWithContext.GetReference();

    var linkedElementId = reference.LinkedElementId;
    if linkedElementId.Equals(ElementId.InvalidElementId):
      elementIds.append(reference.ElementId.IntegerValue)
    else:
      elementIds.append(linkedElementId.IntegerValue)
  }

  return elementIds;
}


Test model R2022:

BB1.Min/Max = (-12.4839, -8.3542, 0) , (-5.0602, -0.9306, 9.8425)

BB2.Min/Max = (-11.4847, -12.3077, 0) , (-6.0974, -10.8641, 9.8425)

Ray = XYZ.BasisX

Origin = BB1.Min with Z+3

TestProject.rvt

Project1.rvt

RPTHOMAS108

Interesting I think it is varying the option set for FindReferenceTarget in order to find the elements (by nested references) in the link then as a consequence of that it


FindReferencesInRevitLinks = True, FindReferenceTarget.Element
309836, 309843, REFERENCE_TYPE_SURFACE, 3.76375750430962 (Linked wall near face)
309836, 309843, REFERENCE_TYPE_SURFACE, 4.71520107386343 (Linked wall far face)
310123, -1, REFERENCE_TYPE_SURFACE, 2.84087372715355 (Wall far face)
310123, -1, REFERENCE_TYPE_SURFACE, 1.39730417334777 (Wall near face)
310123, -1, REFERENCE_TYPE_NONE, 1.39730417334777 (Wall element)

'FindReferencesInRevitLinks = False, FindReferenceTarget.Element
310123, -1, REFERENCE_TYPE_NONE, 1.39730417334777 (Wall element)

'FindReferencesInRevitLinks = False, FindReferenceTarget.All
310123, -1, REFERENCE_TYPE_SURFACE, 2.84087372715355 (Wall far face)
310123, -1, REFERENCE_TYPE_SURFACE, 1.39730417334777 (Wall near face)
310123, -1, REFERENCE_TYPE_NONE, 1.39730417334777 (Wall element)






230312.PNG

referenceintersector_with_link_05.png

I've not checked regarding inclusion of elements outside of bounding box scope. Currently using the or filter I would have expected the two walls captured by the or combination of the two bounding boxes. When in reality only the top bounding box was required to capture both. I think however  it does demonstrate that the ReferenceIntersector isn't internally processing the two bounding boxes for the check in an unconsolidated way.



I think also we have not considered nested links but they are all instanced top level in reality. The requirement to supply a 3D view for the ReferenceIntersector often puts me off using it since there is the requirement to ensure the elements you are looking for are visible in the view and we know elements can be not visible for numerous reasons.



I wouldn't be using the ReferenceIntersector to find elements unless I knew the type of elements I was looking for with it i.e. unless I could limit the search to certain categories and understand what affects the visibility of such.



The bounding box and ReferenceIntersector combination is a bit of an odd one anyway since you know where the ray is pointing so know the region the bounding box should occupy. The bounding box size then therefore becomes about the extents along the ray to capture. The wider those extents the larger the overall bounding box but the ray should ideally pass through the centre of it (why put the box somewhere the ray will not hit).  Would be easier to rule out the rays that don't pass through the box to start with if using some mass ray casting approach.



thomas

i would avoid using the ReferenceIntersector if possible, its too inefficient. Coincidentally, we just removed calls to the ReferenceIntersector in one of our apps having used it for over 2 years. We found that as modelling complexity increased over this time, so did compute time, resulting in a performance increase of around 95% once we replaced it with an alternative (surface projection once, then storing this result in our object model).



The ReferenceIntersector is also inconsistent if other types of element filter are used, so returning to the OP, your best bet (simple, consistent but slow) is to use ReferenceIntersector(Autodesk.Revit.DB.View3D view3d) overload as addition of filters create more problems than they solve, otherwise, see if there are alternatives you can use.

c.hanschen

Thanks for all your replies!

I agree with the conclusion of @thomas  : combining the RefIntersector with other Filters create more problems than they solve! Using Transform to change the Boundingboxfilter is no solution, it has to be done for all the linkinstances, so same conclusion her: create more problems than they solve.



Thanks again for all your replies! much appreciated!


####<a name="3"></a>


<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 600 x 367 pixels -->
</center>


Many thanks to
 for sharing this nice solution.




####<a name="4"></a>

**Question:**


**Answer:**

<pre class="prettyprint lang-json">
</pre>


<pre class="prettyprint lang-cs">

</pre>




