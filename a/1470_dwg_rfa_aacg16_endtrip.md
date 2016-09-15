<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

- general advice: do not import DWG instances into a family definition
  CAD import into a family is considered bad content
  you want to put in a native lines etc. and not an imported cad family
  an imported cad family in a family definition will cause perform,ance issues and slow down revit
  they get duplicated in the project
  depends on how detailed and complicated the cad import is

- endtrip
  https://vimeo.com/73537360 -- Original version by The Outpost
  https://www.youtube.com/watch?v=WjLWsTdISfY -- Sergio Ramis - Endtrip Rescored
  https://vimeo.com/148054745 christopher Endtrip (full sound replacement)

Avoid CAD Import in RFA, #AAG2016 and Endtrip #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim @chtammik

Some interesting topics of the day for this first cloudy one after a truly wonderful Indian summer
&ndash; Avoid imported CAD content in RFA family definitions
&ndash; Advances in Architectural Geometry 2016
&ndash; Endtrip full sound replacement...

-->

### Avoid CAD Import in RFA, AAG16 and Endtrip

Some interesting topics of the day for this first cloudy one after a truly wonderful Indian summer:

- [Avoid imported CAD content in RFA family definitions](#2)
- [Advances in Architectural Geometry 2016](#3)
- [Endtrip full sound replacement](#4)


#### <a name="2"></a>Avoid Imported CAD Content in RFA Family Definitions

I was in a meeting with Sasha Crotty, Revit API Product Manager, last week, and she mentioned an important aspect of content creation that I was not previously fully aware of:

Here is some general advice on creating Revit families: do not import DWG instances into a family definition.

CAD import into a family is considered bad content.

As much as possible, you would want to define a family using native Revit geometry native lines etc., not imported CAD data.

An imported CAD object in a family definition will cause performance issues and slow down Revit.

It gets duplicated in the project for every family instance placed.

Obviously the degradation will depend on the detail and complexity of the CAD content, and the replication of the family instances.

In general, though, it is to be avoided.

As if we couldn't have guessed...



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



#### <a name="4"></a>Endtrip Full Sound Replacement

Let's round off for today with an impressive demonstration of the importance of an audio track complementing a video experience.

My son [Christopher](http://tammik.ca) studied sound design at the [Vancouver Film School](http://vfs.edu/programs/sound-design) last year.

He then launched his career as an independent computer game sound designer, only to veer off into a more programming oriented direction once he realised that the most efficient approach to accomplishing his goals would require his to implement his own tools in the Unity gaming engine first anyway.

Anyway, back to the VFS:

For his final exam, Chris picked a five-minute video equipped with a sound track and implemented a full sound replacement for it.

This involves music, a cast of voice characters, background sound replacement, synchronisation, etc. It is a bit harder than you might initially imagine &nbsp; :-)

Here is the original video and the full sound replacement version side by side:

- [Endtrip &ndash; Original version by The Outpost](https://vimeo.com/73537360)
- [Endtrip &ndash; full sound replacement by Christopher](https://vimeo.com/148054745)

<center>
<iframe src="https://player.vimeo.com/video/148054745" width="480" height="297" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
<p><a href="https://vimeo.com/148054745">Endtrip (full sound replacement)</a> from <a href="https://vimeo.com/chtammik">Chris Tammik</a> on <a href="https://vimeo.com">Vimeo</a>.</p>
</center>

For the sake of completeness, yet another version exists as well, [Endtrip Rescored by Sergio Ramis](https://www.youtube.com/watch?v=WjLWsTdISfY).

I found the comparison utterly fascinating; maybe you will too?
