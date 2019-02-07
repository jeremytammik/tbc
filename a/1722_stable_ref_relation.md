<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

 #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

I just discovered an interesting and not completely obvious aspect of using a filtered element collector in
the Revit API discussion forum thread on aborting a long running filtered element collector.
Question: I have really large models where I use an <code>ElementIntersectsElementFilter</code> that can take a long time to process, and sometimes I want to abort it in a graceful way. Is this possible?

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
the [sketch association to element being sketched](https://forums.autodesk.com/t5/revit-ideas/sketch-association-to-element-being-sketched/idi-p/8578998).
 
> As a developer, I need access to the model lines that constitute the sketch lines for objects like Floors/Walls/Roofs etc.
As of now these sketclines are contained in a Sketch.
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
 
Having something like this would be nice:

Sketch &ndash; 
A stable reference to a Sketch is prefixed by the __Floor.UniqueID__ its belongs to:
__17627a50-6c26-4b9d-a7cc-0deeb5800e11-00050110__:0:SKETCH

Many thanks to Håvard for sharing these observations!

<center>
<img src="img/linking_chains.jpg" alt="Linking chains" width="300">
</center>
