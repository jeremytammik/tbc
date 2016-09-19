<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

 #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

&ndash; 
...

-->

### Roomedit3d Update for Connecting Desktop and Forge

As I recently pointed out, it is really time now to get started with my preparations for the upcoming presentations on connecting the desktiop and the cloud.

Here is an overview of the material I created in the past:

Now that the Forge platform is stable and fully evolved, I want to rewrite the room editor to take advantage of it.

I already did so a couple of months ago with roomedit3d and roomedit3dv2.


- [](#2)
- [](#3)
- [](#4)


#### <a name="2"></a>Starting Point

what is the best starting point for a new forge viewer sample?
 
i would like the 3-legged oath so i can access models on a360.
 
i would like the viewer, and want to add an extension to it.
 
i used philippe's template last time:
 
https://github.com/leefsmp/view.and.data-boilerplate
 
that was perfect.
 
do i need to add augusto's template to that now?
 
https://github.com/augustogoncalves/forge-3leg.nodejs-template
 
or is there a suitable starting point that provides me with both?
 
what is the best starting point for a new forge viewer sample fulfilling these requirements, please?

[A] For  A360 files, use this to get started: https://github.com/Developer-Autodesk/data.management-nodejs-integration.box
 
It’s based on the template you mentioned, just remove the “box” files and parts… sorry I don’t have a “template” for DM + Viewer right now.

And please review these standards: https://wiki.autodesk.com/display/DTAL/NodeJS

yes, i did read your email on those guidelines very carefully and like it very much indeed!

how about putting that on a public site?
 
or even into a blog post?
 
even if it is just for internal use, there is no need to make a secret of it, is there?
 
and a lot of it is good advice for anybody, i think.

[Q] by the way, do you have a blog post describing the data.management-nodejs-integration.box sample in more detail?
 
what functionality does it implement?
 
·         3-legged oauth? (for a360 access?)
·         2-legged oauth? (for box access?)
·         a360 file download and upload
·         box file download and upload
 
anything else?
 
those are six question marks... could you provide six yes/no answers to them, please?

[A] Just have a very short blog post

http://adndevblog.typepad.com/cloud_and_mobile/2016/09/box-forge.html
 
·         3-legged oauth? (for a360 access?)
yes, using NPM packaged for DM API
·         2-legged oauth? (for box access?)
actually 3-legged too, using BOX npm package (see here https://github.com/Developer-Autodesk/data.management-nodejs-integration.box#packages-used)
·         a360 file download and upload
yes, again using the NPM package
·         box file download and upload
yes, using box NPM package
 
See integration .js file functions:
 
sendToBox: https://github.com/Developer-Autodesk/data.management-nodejs-integration.box/blob/master/server/data.management.box.integration.js#L42
sendToAutodesk: https://github.com/Developer-Autodesk/data.management-nodejs-integration.box/blob/master/server/data.management.box.integration.js#L108
 
the .tree.js files implement the respective jsTree functions… see server folder

https://github.com/Developer-Autodesk/data.management-nodejs-integration.box/tree/master/server

Augusto
Goncalves' [data.management-nodejs-integration.box](https://github.com/Developer-Autodesk/data.management-nodejs-integration.box) sample
shows a simple integration between the
Forge [Data Management API](https://developer.autodesk.com/en/docs/data/v2/overview) and
the [Box content management platform](https://www.box.com).

The part of it that I lika and plan to reuse os the basic of my new roomedit3d sample is the 



Bolg post

<p>It starts September 6th: <a href="https://developer.box.com/boxdev">DevBox @ BoxWorks 16</a>. And we prepared a few samples that show Forge working with box. The first sample is a webapp to transfer files&#0160;between them using <a href="https://developer.autodesk.com/en/docs/data/v2/overview/">Data Management API</a>. Check it <a href="http://forgedmboxintegration.herokuapp.com/">live here</a>. Get the NodeJS source code at <a href="https://github.com/Developer-Autodesk/data.management-nodejs-integration.box">Github</a>.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d219451a970c-pi" style="display: inline;"><img alt="Indexpage" class="asset  asset-image at-xid-6a0167607c2431970b01b8d219451a970c img-responsive" height="389" src="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d219451a970c-320wi" title="Indexpage" width="500" /></a></p>
<p>The second uses <a href="https://developer.autodesk.com/en/docs/model-derivative/v2">Model Derivative API</a> transparently to show compatible files on <a href="https://developer.autodesk.com/en/docs/viewer/v2/overview/">Viewer</a>. Want to see in action? <a href="http://forgeboxviewer.herokuapp.com">Try live</a>! Get the full NodeJS source at <a href="https://github.com/Developer-Autodesk/model.derivative-nodejs-box.viewer">Github</a>.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d219452a970c-pi" style="display: inline;"><img alt="Indexpage" class="asset  asset-image at-xid-6a0167607c2431970b01b8d219452a970c img-responsive" height="225" src="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d219452a970c-320wi" title="Indexpage" width="500" /></a></p>
<p>Ah! Both samples can be easily deployed on Heroku! See respective github readme (&quot;Deploy to Heroku&quot; button).</p>




#### <a name="3"></a>Advances in Architectural Geometry 2016

The [AAG16](http://www.aag2016.ch) conference on advances in architectural geometry at the ETH Zurich just completed.

For some exciting computational generative architectural innovation idea, you can take a look at the wonderful, impressive, beautiful and
exciting [Advances in Architectural Geometry 2016](http://vdf.ch/advances-in-architectural-geometry-2016-e-book.html) e-book. 

To give you a quick first impression, here is its table of contents:

- Analysis and Design of Curved Support Structures
- Measuring and Controlling Fairness of Triangulations
- Face-Offsetting Polygon Meshes with Variable Offset Rates
- Marionette Mesh: From Descriptive Geometry to Fabrication-Aware Design
- Designing with Curved Creases: Digital and Analog Constraints
- A Double-Layered Timber Plate Shell: Computational Methods for Assembly, Prefabrication, and Structural Design
- On the Hierarchical Construction of SL Blocks: A Generative System that Builds Self-Interlocking Structures
- Tree Fork Truss: Geometric Strategies for Exploiting Inherent Material Form
- Textile Fabrication Techniques for Timber Shells: Elastic Bending of Custom-Laminated Veneer for Segmented Shell Construction Systems
- Bending-Active Plates: Form and Structure
- Underwood Pavilion: A Parametric Tensegrity Structure
- Safra Neuron Screen: Design and Fabrication
- Scissor Mechanisms for Transformable Structures with Curved Shape: The 'Jet d’Eau' Movable Footbridge in Geneva
- Mastering the 'Sequential Roof': Computational Methods for Integrating Design, Structural Analysis, and Robotic Fabrication
- Adaptive Meshing for Bi-directional Information Flows: A Multi-Scale Approach to Integrating Feedback between Design, Simulation, and Fabrication
- Dimensionality Reduction for Parametric Design Exploration
- Force Adaptive Hot-Wire Cutting: Integrated Design, Simulation, and Fabrication of Double-Curved Surface Geometries
- Designing for Hot-Blade Cutting: Geometric Approaches for High-Speed Manufacturing of Doubly-Curved Architectural Surfaces
- Cuttable Ruled Surface Strips for Milling
- The Armadillo Vault: Computational Design and Digital Fabrication of a Freeform Stone Shell
- CASTonCAST Shell Structures: Realisation of a 1:10 Prototype of a Post-Tensioned Shell Structure from Precast Stackable Components
- Lightweight Conical Components for Rotational Parabolic Domes: Geometric Definition, Structural Behaviour, Optimisation and Digital Fabrication

<center>
<img src="img/aag16_brg.png" alt="AAG16 BRG" width="368">
</center>



#### <a name="4"></a>

People keep asking about how to access different Revit views in the Forge viewer.

We already discussed [access to the 3D views](http://thebuildingcoder.typepad.com/blog/2016/07/selecting-views-for-forge-translation.html),
which is kind of tricky.

Access to the 2D views is much more straightofrward, though, as we can see in the StackOverflow question raised by Greg Bluntzer
on [Autodesk Viewer: Suggestions for 2D view of floor view](http://stackoverflow.com/questions/39533258/autodesk-viewer-suggestions-for-2d-view-of-floor-view/39533388#39533388):

[Q] I have a Revit file and am able to convert it via the cloud to a SVF and view it in the 3D viewer. It works both as conversion of the `.rvt` file directly and as an export from the NavisWorks add-in exporter.

My question: I want the user of my app to also be able to see the floor 2D view as you can in Revit.

I have looked through all the manifest files and do not see a F2D for the floor view.

What do you suggest I use for the 2D view? Note that I will have many drawings to process and view so I would prefer not to have to export a DWG for each view then convert those. I am hoping there is a special setting I can pass to the the converter that will create the 2D views.

(Note : I also want to be able to highlight and texture the rooms of this view dynamically. So I will need to be able to access the geometry like you can in the 3D viewer).



[A] Afaik all 2D views defined in the Revit project file are automatically translated and included in the Forge output.
Have you looked at the LmvNav sample, for instance, also known
as [LMV Nav Test](https://calm-inlet-4387.herokuapp.com)?

It displays both 2D and 3D views. 

Note the list of available 2D views in the Secondary View dropdown:

<center>
<img src="img/lmvnavtest.png" alt="Default 3D and selected 2D secondary view" width="650">
</center>

It even links the elements in the two views so that anything selected in one is highlighted in the other:

<center>
<img src="img/lmvnavtest.png" alt="Selected element synchronisation across views" width="619">
</center>

You could check from where it gets the 2D streams.
The source is avaialble from
the [LmvNavTest GitHub repository](https://github.com/JimAwe/LmvNavTest).

I think you select whether you want a 2D or a 3D stream when you supply it to the viewer.
