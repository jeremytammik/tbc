<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

Stable reference relationships in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/stablerefrel

Håvard Leding explores relationships between stable reference of main elements and their sub-elements, related to the intriguing undocumented <code>ElementId</code> relationships and stable representation magic voodoo.
We need to access the sketch association to the element being sketched.
I printed out stable references for 3 floors, and also the references for each <code>ModelLine</code> in their 3 <code>Sketch</code> elements...

&ndash; 
...

linkedin:

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).

-->

### Stable Reference Relationships

Håvard Leding of [Symetri](https://www.symetri.com) recently contributed
[new RevitLookup commands for edges, faces and linked elements](https://thebuildingcoder.typepad.com/blog/2019/01/new-revitlookup-snoops-edge-face-link.html).

He now brings up a different topic, related
the intriguing [undocumented `ElementId` relationships](http://thebuildingcoder.typepad.com/blog/2011/11/undocumented-elementid-relationships.html)
and [stable representation magic voodoo](http://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html).

In his own words:

We have a Revit Idea Station request to access 
the [sketch association to the element being sketched](https://forums.autodesk.com/t5/revit-ideas/sketch-association-to-element-being-sketched/idi-p/8578998).
 
> As a developer, I need access to the model lines that constitute the sketch lines for objects like Floors/Walls/Roofs etc.
As of now these sketch lines are contained in a Sketch.
I need the stable reference of the Sketch element (or the ModelLines themselves) to reflect its subordinate relationship to the element being sketched.
So, the first part of a Sketch stable reference could be the UniqueId of a Floor, followed by its own Id.

I printed out stable references for 3 floors, and also the references for each `ModelLine` in the 3 `Sketch` elements.

I copy/pasted those into Excel and sorted them.
An obvious pattern emerges:

<pre>
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ff91***FLOOR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ff92:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ff93:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ff94:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ff95:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffa4***FLOOR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffa5:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffa6:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffa7:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffa8:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffad***FLOOR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffae:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffaf:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffb0:0:LINEAR
37627a50-6c26-4b9d-a7cc-0deeb5800e11-0004ffb1:0:LINEAR
</pre>

I'm not sure this pattern can be fully trusted.
This is currently the only way I have found to connect sketch lines to the element they are sketching.
The lines would have to be sorted start to end.
And, once in a `Curveloop`, sorted again by inner/outer loops.
 
An idea would be to put some data on the `Sketch` element that allows us to know which floor the sketch belongs to.

Stable references in Revit sometimes follow a format of "Parent + sub identification".

Here are some examples with the parent part underlined:
 
- Linked Element &ndash;
A stable reference to a linked element is prefixed by the __RevitLinkedInstance.UniqueID__: 
__b4d5315c-7e9d-4bfe-a65e-ba68ec294640-0004fc27__:0:RVTLINK/b4d5315c-7e9d-4bfe-a65e-ba68ec294640-0004fc26:1471417
- Face &ndash;
A stable reference to a wall Face is prefixed by the __Wall.UniqueID__:
__37627a50-6c26-4b9d-a7cc-0deeb5800e11-00050110__:28:SURFACE
- Hatch pattern lines &ndash; 
A stable reference to a hatch pattern line is prefixed by the __Face.Reference__ it is placed on:
__37627a50-6c26-4b9d-a7cc-0deeb5800e11-00050110:28:SURFACE__/1
 
Investigating this, I was hoping `Sketch` was using the same "Parent + sub identification" format.

That way we could get the element being sketched, i.e., the Parent.
 
Having something analogous like this would be nice:

- Sketch &ndash; 
A stable reference to a Sketch is prefixed by the __Floor.UniqueID__ its belongs to:
__17627a50-6c26-4b9d-a7cc-0deeb5800e11-00050110__:0:SKETCH

I guess there already is a request for getting the profile of a Floor or Wall.

To do it now, we have to analyse each edge of each triangulated face.

Here is an image of a Floor modelled by a landscape architect:

<center>
<img src="img/floor_by_landscape_architect.jpg" alt="Floor modelled by a landscape architect" width="423">
</center>

Many thanks to Håvard for sharing these observations!
