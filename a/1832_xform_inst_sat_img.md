<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- using PlanarFace.GetEdgesAsCurveLoops and FilledRegion.Create together
  https://forums.autodesk.com/t5/revit-api-forum/using-planarface-getedgesascurveloops-and-filledregion-create/m-p/9405257
  instance transform from symbol to instance placement in project

- Importing and Displaying Satellite Images
  https://forums.autodesk.com/t5/revit-api-forum/importing-and-displaying-satellite-images/m-p/9406063

- https://www.freecodecamp.org/news/coronavirus-academy/
  Learn to Code From Home: The Coronavirus Quarantine Developer Skill Handbook

twitter:

Transforming symbol geometry to instance placement, importing and displaying satellite images in the #RevitAPI #DynamoBim 
@AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/satimginstxform
 
I hope you are doing well and remaining healthy!
Topics for today
&ndash; Transforming symbol geometry to instance placement
&ndash; Importing and displaying satellite images
&ndash; Free time? Learn! Free code camp! ...

linkedin:

Transforming symbol geometry to instance placement, importing and displaying satellite images in the #RevitAPI

http://bit.ly/satimginstxform

I hope you are doing well and remaining healthy!

Topics for today:

- Transforming symbol geometry to instance placement
- Importing and displaying satellite images
- Free time? Learn! Free code camp! ...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Satellite Images and Instance Transforms

Things have slowed down a bit in the discussion forum recently, like in many other parts of the world.
I am very glad I have a garden to go out and sit in, and springtime and sunshine to enjoy.
I hope you are doing well and remaining healthy also!
Topics for today:

- [Transforming symbol geometry to instance placement](#2)
- [Importing and displaying satellite images](#3)
- [Free time? Learn! Free code camp!](#4)

#### <a name="2"></a>Transforming Symbol Geometry to Instance Placement

A fundamental question reappeared in a new form in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [using `PlanarFace` `GetEdgesAsCurveLoops` and `FilledRegion` `Create` together](https://forums.autodesk.com/t5/revit-api-forum/using-planarface-getedgesascurveloops-and-filledregion-create/m-p/9405257),
easily resolved by applying the instance placement transformation to transform the symbol geometry from the family definition coordinate system to the real-world instance placement in the project space:

**Question:** I'm trying to create a plugin that will allow me to create filled regions just by selecting a face.

I use *Selection.PickObject( ObjectType.Face )*, convert that reference to a `PlanarFace` type, and then use `GetEdgesAsCurveLoops` to get the collection of `CurveLoops` and pass that to `FilledRegion` `Create` to create my filled region.

However, it's resulting in unexpected behaviour.
If I click on the face of a roof surface, it creates the filled region exactly over the face like I want it to.
When I click the face of a family instance, it still creates the filled region &ndash; but instead of creating it over the face of the family instance, it creates the instance at the project origin of the document.

Here are images illustrating the two scenarios:

Highlighting the roof face and selecting it applies the FilledRegion like expected:

<center>
<img src="img/xform_inst_1.png" alt="Roof face and filled region" title="Roof face and filled region" width="400"/> <!-- 423 -->
</center>

However, when I click this electrical equipment face...

<center>
<img src="img/xform_inst_2.png" alt="Electrical equipment face" title="Electrical equipment face" width="800"/> <!-- 1081 -->
</center>

It creates the filled region over at the project origin:

<center>
<img src="img/xform_inst_3.png" alt="Filled region at project origin" title="Filled region at project origin" width="800"/> <!-- 1156 -->
</center>

Here is the relevant code snippet:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;faceRef&nbsp;=&nbsp;sel.PickObject(&nbsp;<span style="color:#2b91af;">ObjectType</span>.Face&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">GeometryObject</span>&nbsp;geoObj&nbsp;=&nbsp;doc.GetElement(&nbsp;faceRef&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.GetGeometryObjectFromReference(&nbsp;faceRef&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">PlanarFace</span>&nbsp;moduleFace&nbsp;=&nbsp;geoObj&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">PlanarFace</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">CurveLoop</span>&gt;&nbsp;faceEdges&nbsp;=&nbsp;moduleFace.GetEdgesAsCurveLoops();
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilledRegion</span>&nbsp;fillRegion&nbsp;=&nbsp;<span style="color:#2b91af;">FilledRegion</span>.Create(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;stringFillType.Id,&nbsp;currentView.Id,&nbsp;faceEdges&nbsp;);
</pre>

Does anyone know how to get around this? Thanks!

**Answer:** The family instance reuses the geometry defined for the family symbol by applying a transform to it.

The symbol geometry is generally close to the origin.

Every instance needs a different transformation, depending on where the instance is placed in the model.

You need to apply the family instance transformation to the geometry to transform it from the family symbol definition coordinates to the project model coordinates where the instance has been placed:

- [GetTransform](https://www.revitapidocs.com/2020/50aa275d-031e-ce19-9cfd-18a7a341ed19.htm)
- [GetTotalTransform](https://www.revitapidocs.com/2020/8c8aff2b-5ff9-e43a-3b5c-308cd0174f1f.htm)

The roof element is different, since it is modelled in place using a sketch.

**Response:** Awesome! This worked! Thank you much Jeremy!

For anyone interested, here is how I edited the code based on Jeremy's suggestion:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;faceRef&nbsp;=&nbsp;sel.PickObject(&nbsp;<span style="color:#2b91af;">ObjectType</span>.Face&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">GeometryObject</span>&nbsp;geoObj&nbsp;=&nbsp;doc.GetElement(&nbsp;faceRef&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.GetGeometryObjectFromReference(&nbsp;faceRef&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Instance</span>&nbsp;moduleInstance&nbsp;=&nbsp;doc.GetElement(&nbsp;faceRef&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Instance</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;moduleTransform&nbsp;=&nbsp;moduleInstance.GetTotalTransform();
 
&nbsp;&nbsp;<span style="color:#2b91af;">PlanarFace</span>&nbsp;moduleFace&nbsp;=&nbsp;geoObj&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">PlanarFace</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">CurveLoop</span>&gt;&nbsp;faceEdges&nbsp;=&nbsp;moduleFace.GetEdgesAsCurveLoops();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">CurveLoop</span>&nbsp;loop&nbsp;<span style="color:blue;">in</span>&nbsp;faceEdges&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;loop.Transform(&nbsp;moduleTransform&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilledRegion</span>&nbsp;fillRegion&nbsp;=&nbsp;<span style="color:#2b91af;">FilledRegion</span>.Create(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;stringFillType.Id,&nbsp;currentView.Id,&nbsp;faceEdges&nbsp;);
</pre>

Many thanks to Chris for raising the issue and confirming the solution!

#### <a name="3"></a>Importing and Displaying Satellite Images

Revitalizer came up with a very nice suggestion
for [importing and displaying satellite images](https://forums.autodesk.com/t5/revit-api-forum/importing-and-displaying-satellite-images/m-p/9406063):

**Question:** I'm building an add-in for Revit and I would like to be able to import and display third-party satellite imagery in order to place buildings in their 'real' position.
I would like to be able to do this in a 3D view, but I don't know how.

The user workflow for my add-in is this:

- A user opens the add-in and is prompted to input a location through a WPF window.
- Once a location is confirmed, a number of things are created/imported into the active project to make it look as close as possible to its actual real-life location. One of these things is the satellite image I'm seeking to import here.

Essentially, my question is exactly the same
as [Google maps image in 3D view](https://forums.autodesk.com/t5/revit-architecture-forum/google-maps-image-in-3d-view/m-p/8100681),
but instead doing that programmatically/automatically through an add-in.
In that thread, a suggestion is made to create a decal with the desired image, but this does not seem to be supported through the API.

Another approach I found is
to [use `PostCommand`](https://thebuildingcoder.typepad.com/blog/2013/10/programmatic-custom-add-in-external-command-launch.html) to
create and place decals, but these commands are apparently
only [executed after exiting the API context](https://knowledge.autodesk.com/support/revit-products/learn-explore/caas/CloudHelp/cloudhelp/2014/ENU/Revit/files/GUID-1C7289DE-8D10-47B5-B6DB-EA1310851C8F-htm.html) and
only one at a time.
As my add-in aims to perform a whole bunch of functionalities in one go, this seems ill-suited for my use case.
It seems to be possible to chain a bunch of PostCommands, but this is a little 'hacky' and not recommended, especially for commercial use.

Am I overlooking some existing functionality?
Is my use case just not supported in current Revit?
I'm new to programming for Revit, so it's very possible I've missed something.

I'm running / programming for Revit 2019 on Windows 10.

**Answer:** What about creating a new material, setting its texture path, then making a `TopoSurface` and assigning the material to it, cf. [modifying material visual appearance](https://thebuildingcoder.typepad.com/blog/2017/11/modifying-material-visual-appearance.html)?

I don't know how to adjust the `UV` mapping for the `TopoSurface`, but if it worked, you would see your satellite image in 3D.

**Response:** Thanks to all for the replies!
It took some time to try out the proposed solution (accessing AppearanceElements is convoluted!), so that's why it took me this long to reply.

In the end, though I had to work around some weird quirks with the API, adding the image as a texture to a topography through a material works great.

Thanks again for the suggestion, I couldn't have made it work without it.

Many thanks to Harm for raising this and to Revitalizer for the good suggestion.


#### <a name="4"></a>Free Time? Learn Things! Free Code Camp!

I have repeatedly pointed out the incredible programming training resources offered
by Quincy Larson and [freecodecamp.org](https://www.freecodecamp.org).

In case you happen to have more time on your hands than usual these days, you might want to see it as an opportunity
to [learn to code from home with the Coronavirus quarantine developer skill handbook](https://www.freecodecamp.org/news/coronavirus-academy).

By the way, I very much enjoyed some of Quincy's recent quotes of the week:

- A computer once beat me at chess, but it was no match for me at kickboxing &ndash; *Emo Philips*
- The greatest obstacle to discovery is not ignorance, but the illusion of knowledge &ndash; *Daniel Boorstin*
- Good judgment comes from experience. Experience comes from bad judgment &ndash; *unknown*
- Optimism is an occupational hazard of programming. Testing is the treatment &ndash; *Kent Beck*
- Act in haste and repent at leisure. Code too soon and debug forever &ndash; *Dr. Raymond Kennington*
