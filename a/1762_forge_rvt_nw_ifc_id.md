<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- /a/doc/revit/tbc/git/a/img/808_jeremy_jakobiger_gipfelkreuz_600x800.jpg

- Revit element ids in Forge via Navisworks NW

- new topic group on element id and guid in rvt, ifc, nw and forge

- more trees can save the planet (and us) from global warming

- Roger Penrose On Why Consciousness Does Not Compute
  The emperor of physics defends his controversial theory of mind.
  http://nautil.us/issue/47/consciousness/roger-penrose-on-why-consciousness-does-not-compute
  Steve Paulson

twitter:

Element Identifiers in RVT, IFC, NW and Forge using the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/rvt_nw_ifc_forge_ids

A recent internal discussion clarifies Revit element identification in Forge, Navisworks and IFC, some new thoughts on consciousness versus AI, and a couple of topics of personal interest
&ndash; Two Nice summer mountain hikes
&ndash; Revit element ids in Forge via Navisworks and IFC
&ndash; Some aspects of consciousness may be beyond reach of AI
&ndash; Trees might help against global warming
&ndash; Holidays ahead...

linkedin:

Element Identifiers in RVT, IFC, NW and Forge using the #RevitAPI

http://bit.ly/rvt_nw_ifc_forge_ids

A recent internal discussion clarifies Revit element identification in Forge, Navisworks and IFC, some new thoughts on consciousness versus AI, and a couple of topics of personal interest:

- Two Nice summer mountain hikes
- Revit element ids in Forge via Navisworks and IFC
- Some aspects of consciousness may be beyond reach of AI
- Trees might help against global warming
- Holidays ahead...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

-->

### Element Identifiers in RVT, IFC, NW and Forge

A recent internal discussion clarifies Revit element identification in Forge, Navisworks and IFC, some new thoughts on consciousness versus AI, and a couple of topics of personal interest:

- [Two nice summer mountain hikes](#2)
- [Revit element ids in Forge via Navisworks and IFC](#3)
- [Some aspects of consciousness may be beyond reach of AI](#4)
- [Trees might help against global warming](#5)
- [Holidays ahead](#6)

####<a name="2"></a> Two Nice Summer Mountain Hikes

I spent the last two weekends in the mountains north and south of the Gotthard pass.

First, I crossed the ridges between Grossgand, Ruchälplistock and Jakobiger with my friend Nik, and we spent the night in a pleasant, warm, windless bivouac on the latter at 2505 metres before descending for coffee in the Leutschachhütte next morning ([https://flic.kr/s/aHsmEUnYFd](photo album)).

The weekend after, I hiked with Moni from Mergugno over Brissago up to the Rifugio al Legn overlooking the Lago Maggiore.
I continued alone from there up to Fumadiga and through the upper part of Bocchetta di Valle to Monte Limidario, aka Gridone.

<center>
<img src="img/808_jeremy_jakobiger_gipfelkreuz_600x800.jpg" alt="Jeremy on Jakobiger" width="300">
</center>


####<a name="3"></a> Revit Element Ids in Forge via Navisworks and IFC

This question prompted me to create a new topic group
on [element identification using `ElementId` and `UniqueId` RVT, IFC, NW and Forge](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.56):

**Question:** I created a Navisworks file from a Revit file and submitted it to Forge.
The returning translated JSON looks like this:

<center>
<img src="img/forge_model_derivative-nw-rvt.png" alt="RVT to NW element id in Forge" width="600"> <!-- 809 -->
</center>

What is this `externalId` and where does it come from?

The `Id` property maps to the Revit element id.

Can you please confirm?

Where is the source Revit element unique id?

Is it lost during this process?

**Answer:** The `externalId` is the path through the model selection tree to that item.
As far as I know, Revit doesn't have per-element GUIDs, but instead each element has a unique id.
This is what you have highlighted at the bottom of the image.

This is how we expect NWD files to look like when converted to SVF.

You cannot expect the data to look exactly the same as the RVT file.

**Response:** This is what the source Revit file looks like in JSON:

<center>
<img src="img/forge_model_derivative-rvt.png" alt="RVT element ids in Forge" width="715">
</center>

In this case, the `externalId` is the `Element.UniqueID` GUID.

The `Name` property contains the `Element.Id`.


**Answer:** Yes, I know they are different, which you would expect, really, as you are converting different types of files.

Revit creates a unique id by appending together
the [document-level EpisodeId GUID with the Element ID in hex](https://thebuildingcoder.typepad.com/blog/2009/02/uniqueid-dwf-and-ifc-guid.html#2):

<pre>
  0088CA1F hex = 8964639 decimal
</pre>

**Response:** In this case, in order to find the Revit element in the converted Navisworks file, we actually need to parse the name and get the Id out of it? I was expecting a property to hold this value.

I find strange that in the translated source Revit file we don't actually have an "Id" property.

I thought that the `"`externalId` in the translated source RVT file, was the actual id to look for in the translated NWD file.

I need to consistently identify elements in NWD and IFC files generated out of a source RVT file.

Looking at the Forge model derivative JSON files, it seems we can have

- Take the `Id` out of the `Name` for the RVT
- The `Element.Id` for the NWD
- The `IfcTag` for the IFC

Here is an image showing all three:

<center>
<img src="img/forge_model_derivative-nwd-rvt-ifc.png" alt="Element ids in Forge RVT, NW and IFC files" width="1802">
</center>

**Answer:** Yes.

The Revit extractor puts the Element ID in the object name, so you can get it from there.

The NW extractor has the Element ID as a specific property (in the Element category).

The IFC file has the Element ID in the IfcTag attribute.

In NW, we try to read as much property data as we can. From there, if the property is visible in NW, it is exported on into the SVF file. The NW to SVF extractor implements special handling for different formats. Once the file has been converted to NWC/NWD, we don't really know what source file type it was originally. Furthermore, because not all file formats have unique ids for elements, NW doesn't have a concept of unique elements ids either; hence, it can only output the path through the tree for the `externalId` property.



####<a name="4"></a> Some Aspects of Consciousness May be Beyond Reach of AI

A very interesting overview by Steve Paulson,
[Roger Penrose on why consciousness does not compute](http://nautil.us/issue/47/consciousness/roger-penrose-on-why-consciousness-does-not-compute),
describing some of Penrose's theories on consciousness that cannot simply be simulated by AI.


####<a name="5"></a> Trees Might Help Against Global Warming

The prestigious Zurich university ETH just published some positive news on a possible approach to the climate change; their research
shows [how trees could save the climate](https://ethz.ch/en/news-and-events/eth-news/news/2019/07/how-trees-could-save-the-climate.html).


####<a name="6"></a> Holidays Ahead

Similar to last year, I am heading off to France for some camping and a meditation retreat
in [Plum Village](http://thebuildingcoder.typepad.com/blog/2018/07/mindful-living-and-smiling-to-myself.html),
a Buddhist monastery near Bordeaux.

On the way there, I'm also visiting my friend George in a camping ground on the beach of the Atlantic for a few days.

So I will be less active in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) in
the coming weeks.

I wish you a wonderful summer!
