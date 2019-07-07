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


I spent the last two weekends in the mountains north and south of the Gotthard pass.

A bviouac with my friend Nik on

2019-06-30 and 2019-07-01: niklaus mueller Amsteg arni Intschibahn arnisee sunnigrat jakobiger leutschachhuette
Strecke	Arnisee Bergstation 1360 m - Arnisee 1370 m - Torli - Chäserli 1479 m - Boedemli - Furt - Chaelengraetli - Grossgand - Ruchaelplistock - Jakobiger (bivouac) - Leidseepass - Leutschachhütte SAC 2218 m - Nidersee 2091 m - Furt - Boedemli - Chäserli 1479 m - Hinter Arni - Arnisee 1370 m - Arnisee Bergstation 1360 m
Anfahrt	mit dem Auto mit Moni bis zur Seilbahn, direkt an dira, zu Fuss hoch nach Piazzogna
/a/doc/travel/trip/intschi/map_intschi_seilbahn.png

A planned glacier tour with sunrise on the Oberaarhorn was unfortunately cancelled.

Cancelled: 2019-07-07 Oberaarhorn 3630m, ab Berghaus Oberaar.
Markus Häbig, Julia Brück, Adrian Meier, Jeremy Tammik, Ines Merten, Max Sieber, Harry Weiss
Aufstieg bis zur Hütte ca. 4h, gemütlich. Übernachten und früh morgens zum Sonnaufgang auf den Gipfel, ca. 1h.
Danach mit Pausen, gemütlich wieder zurück zum Berghaus Oberaar.
https://alpsinsight.com/stories/climbing-the-oberaarhorn/

Instwad,

2019-07-06 gridone aka monte limidario, 2186 m, with moni.
moni and i drove to brissago, from there up to mergugno, and parked.
we walked up to the rifugio al legn, where moni stayed and waited for me to return..
i continued up to fumadiga (2010 m), then through the upper part of bocchetta di valle to gridone (2186 m).
i returned via the ridge west of bocchetta di valle.


808_jeremy_jakobiger_gipfelkreuz_600x800.jpg 600



- new topic group on element id and guid in rvt, ifc, nw and forge

- Revit element ids in Forge via Navisworks NW
Antonio Nogueira: Our customer created a Navisworks file from a Revit file. When they look at the returning translated JSON they see something like this. What is this externalId coming from? The Id property is mapping to the Revit element Id. Can you please confirm? Where is the source Revit element Guid? Is it lost during this process?
mderivative-nw-rvt.png

/a/doc/revit/tbc/git/a/img/forge_model_derivative-nw-rvt.png

Michael Priestman: The `externalId` is the path through the model selection tree to that item. As far as I know, Revit doesn't have per-element GUIDs, but instead each element has a unique ID. This is what you have highlighted at the bottom of the image.

Michael Priestman: This is how we expect NWD files to look like when converted to SVF. You cannot expect the data to look exactly the same as the RVT file.

Antonio Nogueira: @priestm please see below the source Revit file JSON. In this case the externalId is the Element.UniqueID GUID. The Name contains the Element.Id.
mderivative-rvt.png

/a/doc/revit/tbc/git/a/img/forge_model_derivative-rvt.png

Michael Priestman: Yes, I know they are different, which you would expect really as you are converting different types of files

Michael Priestman: Revit creates a unique ID by appending together some document-level GUID with the Element ID in hex

Michael Priestman: `0088CA1F` = `8964639`

Antonio Nogueira: In this case, in order to find the Revit element in the converted Navisworks file, we actually need to parse the name and get the Id out of it? I was expecting a property to hold this value.

Antonio Nogueira: I find strange that in the translated source Revit file we don't actually have an "Id" property. (edited)

Antonio Nogueira: I thought that the "externalId", in the translated source RVT file, was the actual id to look for in the translated NWD file.

Antonio Nogueira: Our customer needs to consistently identify elements in NWD and IFC file generated out of a source RVT file. Looking at the Forge MD JSON files it seems we can have : 1) take the Id out of the Name (for the RVT), 2) the Element.Id for the NWD and 3) the IfcTag for the IFC. See mapping image.
mderivative-nwd-rvt-ifc.png

/a/doc/revit/tbc/git/a/img/forge_model_derivative-nwd-rvt-ifc.png

Antonio Nogueira: Do we have some id data mapping for different translated formats? It is a reasonable request to preserve the identify of elements across different file formats.

Michael Priestman: OK, so you can do it. The Revit extractor puts the Element ID in the object name (I don't know if it also puts it in a dedicated property). So you can get it from there. The NW extractor has the Element ID as a specific property (in the Element category). It looks like the IFC file has the Element ID in the IfcTag attribute, so you can find it that way.

Michael Priestman: So yes, I'm basically agreeing with you

Michael Priestman: In NW, we try to read as much property data as we can. From there, if the property is visible in NW, it should get exported into the SVF file. What the NW -> SVF extractor do is do special case code for different formats. Once the file has been converted to NWC/NWD, we don't really know what source file type it was. And because not all file formats have unique IDs for elements, NW doesn't have a concept of unique elements IDs either, hence it can only output the path through the tree for the `externalId` property. Hope this all makes sense.


forge_model_derivative-nwd-rvt-ifc.png 1802
forge_model_derivative-rvt.png 715

<center>
<img src="img/forge_model_derivative-nw-rvt.png" alt="RVT to NW element id in Forge " width="809">
</center>
