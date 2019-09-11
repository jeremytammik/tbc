<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- intersection of cylinders
  [solid intersection questions](https://forums.autodesk.com/t5/revit-api-forum/solid-intersection-questions/m-p/9005827):

- future online assets
  https://movielabs.com/production-technology/

twitter:

Cylinder intersection requires edges and a vision of online assets in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/cylinderinters

A surprising new aspect of Revit's built-in solid intersection algorithms and a ten-year vision for online assets
&ndash; No face to face intersection without edges
&ndash; Evolution of media creation &ndash; Vision of online assets...

linkedin:

Cylinder intersection requires edges and a vision of online assets in the #RevitAPI 

http://bit.ly/cylinderinters

A surprising new aspect of Revit's built-in solid intersection algorithms and a ten-year vision for online assets:

- No face to face intersection without edges
- Evolution of media creation - Vision of online assets...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Cylinder Intersection and Vision of Online Assets

A surprising new aspect of Revit's built-in solid intersection algorithms and a ten-year vision for online assets:

- [No face to face intersection without edges](#2)
- [Evolution of media creation &ndash; Vision of online assets](#3)

####<a name="2"></a> No Face to Face Intersection Without Edges

A fundamental new aspect of the built-in Revit intersection calculation algorithm was unearthed thanks to 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [solid intersection questions](https://forums.autodesk.com/t5/revit-api-forum/solid-intersection-questions/m-p/9005827):

**Question:** I have a couple of questions about how Revit handles solid intersection for Interference Check, `ElementIntersectsElement`, `ElementIntersectsSolid` and `BooleanOperationUtilities`.

I've put together an extremely simple model consisting of 4 instances of a Specialty Equipment family that consists only of a cylindrical extrusion. Among these 4, some of them intersect shallowly on their cylindrical faces. The exact setup can be seen here, with element IDs to refer to the elements more clearly:

<center>
<img src="img/intersecting_cylinders.png" alt="Intersecting cylinders" width="432">
</center>

As you can see, we should find the following intersections:

- 1150420 and 1149599/1149413
- 1150613 and 1149599

However, when we run Interference Check, some of those intersections are not found.

I've thrown together a small test app which separately:

- Tries to find intersections using the `ElementIntersectsElement` filter
- Uses a bidirectional application of the `ElementIntersectsSolid` filter
- Attempts to create intersections using `BooleanOperationUtils` and checks them for positive volume.

Code can be found in the
attached [InterferenceCheckIssue.zip](zip/dt_InterferenceCheckIssue.zip) along
with higher resolution screenshots and the model on which the code was run in Revit 2018 and Revit 2020.
The issue occurs in both.

All three approaches give the same result, only finding intersections between the elements highlighted by the Interference check.

So, I suppose my questions would be:

- What causes this to happen? (As we've seen it on several different setups involving CylindricalFaces or RevolvedFaces)
- What can be done to tackle this issue in a consistent and efficient manner?
- Is there a better way to look for solid intersections than any of the approaches via the API than those I've attempted here?

**Answer:** I logged the issue *REVIT-152087 [incomplete intersection results]* with our development team for this on your behalf, and they explain:

> The functions you mentioned probably all use the same underlying code to intersect solids, or to determine if two solids intersect. One of its limitations is that it does not detect intersections between two faces that lie entirely in the interiors of the faces. It's likely that in the cases when the intersection between two cylinders is not detected, the edges of each cylinder do not intersect any faces of the other cylinder. Note that in Revit, a solid cylinder's boundary will (typically) have two semi-cylindrical faces.

> A workaround in such cases is to rotate one or both objects to ensure that edges of one solid intersect faces of the other solid, though in a complex case involving many solids that may not always be possible.

> It's probably not hard to make the interference-checking code handle some special cases of this sort, such as two cylinders or two spheres, but the cost/benefit ratio of enhancing Boolean operations to handle such cases is probably too high to make it worthwhile, as such cases don't arise very often in practice.

This issue has in fact been raised in the past and is covered by several existing development tickets, e.g., REVIT-32243 and the items it links to, including the old "master" SPR #123893 for this issue and the items _it_ links to.

It was a known limitation from the start.

Since 2001, about a dozen such items have been filed. 

There is usually a fairly simple workaround, so up until now, this issue, though annoying, hasn't been a serious obstacle.

**Response:** Thank you so much for your extensive feedback. We had suspected in-house that it was something along these lines causing the inconsistency. Especially the references to old discussions of the issue are very helpful.

To summarize and clarify, the root of the issue/known limitation is that solid interference is based on detection of edge -> face intersections, rather than face -> face intersections, right? So, I need at least one "Edge" of solid 1's faces to penetrate one or more faces of solid 2, or vice versa, right?

One workaround we've come up with for our purely cylindrical/revolved families is calculating the path of the solid and generating an approximated planar-Face-based solid to perform interference checks with.

Of course, the biggest remaining issue then would appear when we're trying to run interference checks between things which have cylindrical components, but are not detected by our algorithm as "fully-cylindrical-things." (i.e. a cube/prism with a cylinder poking off of it in some direction) While I don't know that we can write these off full-stop, it is my suspicion that these are a semi-rare occurrence.

In any case, this at least satisfies my curiosity in what was going on behind the scenes.

**Answer:** Yes, I believe your description of the situation matches what the development team mean.

In my fantasy, I would have thought that an easier workaround than any of the ones suggested so far would be to add four model lines per cylinder to the Revit model, or even just add four non-database line segments in-memory for each cylinder to the cylinder-cylinder intersection cases, one along each of each cylinder's four cross-section quadrants, and include intersection checks between the four quadrant lines along one cylinder with the other cylinder's surface. Would that catch all possible cases?

Another suggestion, from Revitalizer: you could create some lines alongside a pipe's outer hull, e.g. 4, 8 or 12 lines, depending on accuracy needed.

For each of these lines (plus the pipe centre line), use a `ReferenceIntersector` to find intersecting pipes.

Make sure the `View3D` used for the `ReferenceIntersector` displays the pipe's geometry in a proper way.

**Response:** I think where we've settled with this in the office is using `Curve` objects constructed from `CylindricalFace` and `RevolvedFace` geometric information and checking their intersection with candidate solids.

Because this is a fairly processing-heavy task, we're using it as a "last resort" after checking that the elements in question have certain parameter information, lie within (or intersecting) one another's bounding boxes and are not already flagged by the `ElementIntersectsSolidFilter` before reaching this point. This way, we narrow ourselves down to an extremely small subset of possible intersections to check.

Thanks again for all your help in this matter.


####<a name="3"></a> Evolution of Media Creation &ndash; Vision of Online Assets

A colleague pointed out a thought-provoking white paper by MovieLabs and its member studios (Paramount Pictures, Sony Pictures Entertainment, Universal Studios, Walt Disney Pictures and Television and Warner Bros. Entertainment) laying out a bold 10-year vision for the future of media creation technology, for filmmaking during the next 10 years, with a call to action for the industry to collaborate by appropriate means to achieve shared goals and continue to empower future storytellers and the creative community. They describe future technological advances that will enable seismic changes in media workflows with one objective in mind &ndash; to empower storytellers to tell more amazing stories while delivering at a speed and efficiency not possible today.

Many of the necessary shifts articulated in this paper are quite closely aligned with strategic intents of Autodesk, the Forge team, the AEC and other collaborative industries in general.

Here are the ten foundational principles formulated in this document:

1. All assets are created or ingested straight into the cloud and do not need to be moved.
2. Applications come to the media.
3. Propagation and distribution of assets is a “publish” function.
4. Archives are deep libraries with access policies matching speed, availability and security to the economics of the cloud.
5. Preservation of digital assets includes the future means to access and edit them.
6. Every individual on a project is identified and verified, and their access permissions are efficiently and consistently managed.
7. All media creation happens in a highly secure environment that adapts rapidly to changing threats.
8. Individual media elements are referenced, accessed, tracked and interrelated using a universal linking system.
9. Media workflows are non-destructive and dynamically created using common interfaces, underlying data formats and metadata.
10. Workflows are designed around real-time iteration and feedback.

If this sounds interesting to you, go ahead, download and take a look at the white paper when you have a chance:

<center>
<a href="https://movielabs.com/production-technology">The Evolution of Media Creation &ndash; A 10-Year Vision for the Future of Media Production, Post and Creative Technologies</a>
</center>
  
<!-- zip/MovieLabs-Evolution-of-Media-Creation.pdf -->
