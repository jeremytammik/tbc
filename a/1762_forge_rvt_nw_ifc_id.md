<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- /a/doc/revit/tbc/git/a/img/808_jeremy_jakobiger_gipfelkreuz_600x800.jpg

- more trees can save the planet (and us) from global warming

- Roger Penrose On Why Consciousness Does Not Compute
  The emperor of physics defends his controversial theory of mind.
  http://nautil.us/issue/47/consciousness/roger-penrose-on-why-consciousness-does-not-compute
  Steve Paulson
  BY STEVE PAULSON
  MAY 4, 2017

- new topic group on element id and guid in rvt, ifc, nw and forge

- Revit element ids in Forge via Navisworks NW

twitter:

 Forge in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/roomvolumegltf

&ndash;
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

-->

### Identifying an Element in RVT, IFC, NW and Forge


####<a name="2"></a> Two Nice Summer Mountain Hikes

I spent the last two weekends in the mountains north and south of the Gotthard pass.

First, I crossed the ridges between Grossgand, Ruchälplistock and Jakobiger with my friend Nik, and we spent the night in a pleasant, warm, windless bivouac on the latter at 2505 metres before descending for coffee in the Leutschachhütte next morning.

Last weekend, I hiked from Mergugno over Birissago up to the Rifugio al Legn overlooking the Lago Maggiore.
I continued from there up to Fumadiga (2010 m) and through the upper part of Bocchetta di Valle to Monte Limidario, aka Gridone (2188 m).

<center>
<img src="img/808_jeremy_jakobiger_gipfelkreuz_600x800.jpg" alt="Jeremy on Jakobiger" width="300">
</center>




####<a name="2"></a> Revit element ids in Forge via Navisworks NW

This questions propted me to create a new topic group on element identification using `ElementId` and `UniqueId` RVT, IFC, NW and Forge:

**Question:** I created a Navisworks file from a Revit file and submitted it to Forge.
The returning translated JSON looks like this:

<center>
<img src="img/forge_model_derivative-nw-rvt.png" alt="RVT to NW element id in Forge" width="809">
</center>

What is this `externalId` and where does it come from?

The `Id` property maps to the Revit element id.

Can you please confirm?

Where is the source Revit element unique id?

Is it lost during this process?

**Answer:** The `externalId` is the path through the model selection tree to that item. As far as I know, Revit doesn't have per-element GUIDs, but instead each element has a unique ID. This is what you have highlighted at the bottom of the image.

This is how we expect NWD files to look like when converted to SVF.

You cannot expect the data to look exactly the same as the RVT file.

**Response:** @priestm please see below the source Revit file JSON. In this case the externalId is the Element.UniqueID GUID. The Name contains the Element.Id.
mderivative-rvt.png

/a/doc/revit/tbc/git/a/img/forge_model_derivative-rvt.png

**Answer:** Yes, I know they are different, which you would expect really as you are converting different types of files

**Answer:** Revit creates a unique ID by appending together some document-level GUID with the Element ID in hex

**Answer:** `0088CA1F` = `8964639`

**Response:** In this case, in order to find the Revit element in the converted Navisworks file, we actually need to parse the name and get the Id out of it? I was expecting a property to hold this value.

**Response:** I find strange that in the translated source Revit file we don't actually have an "Id" property. (edited)

**Response:** I thought that the "externalId", in the translated source RVT file, was the actual id to look for in the translated NWD file.

**Response:** Our customer needs to consistently identify elements in NWD and IFC file generated out of a source RVT file. Looking at the Forge MD JSON files it seems we can have : 1) take the Id out of the Name (for the RVT), 2) the Element.Id for the NWD and 3) the IfcTag for the IFC. See mapping image.
mderivative-nwd-rvt-ifc.png

/a/doc/revit/tbc/git/a/img/forge_model_derivative-nwd-rvt-ifc.png

**Response:** Do we have some id data mapping for different translated formats? It is a reasonable request to preserve the identify of elements across different file formats.

**Answer:** OK, so you can do it. The Revit extractor puts the Element ID in the object name (I don't know if it also puts it in a dedicated property). So you can get it from there. The NW extractor has the Element ID as a specific property (in the Element category). It looks like the IFC file has the Element ID in the IfcTag attribute, so you can find it that way.

**Answer:** So yes, I'm basically agreeing with you

**Answer:** In NW, we try to read as much property data as we can. From there, if the property is visible in NW, it should get exported into the SVF file. What the NW -> SVF extractor do is do special case code for different formats. Once the file has been converted to NWC/NWD, we don't really know what source file type it was. And because not all file formats have unique IDs for elements, NW doesn't have a concept of unique elements IDs either, hence it can only output the path through the tree for the `externalId` property. Hope this all makes sense.


forge_model_derivative-nwd-rvt-ifc.png 1802
forge_model_derivative-rvt.png 715

<center>
<img src="img/forge_model_derivative-nw-rvt.png" alt="RVT to NW element id in Forge " width="809">
</center>
