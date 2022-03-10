<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.4.0
Minimize, maximize support #134. Fixed problem with sending a print job #133

Allow user maximize form full screen #134

> Enable user to maximize all forms to full screen; useful to display long string data or to  expand the form full size of review 

Fix automatic execute method SubmitPrint #133

> A problem when user snoops to `PrintManager` and invokes the `SubmitPrint` method

Many thanks to 
[Chuong Ho](https://github.com/chuongmep)
[Roman 'Nice3point'](https://github.com/Nice3point)

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Drilling Holes in Beams and Other Projects

I am working on various side projects and proofs of concept for my own and other teams, so I find myself a bit challenged finding enough time for blogging and monitoring the forum at the same time:

####<a name="2"></a> RvtParamDrop Exports Visible Element Properties
####<a name="3"></a> RvtLock3r Validate BIM Element Properties
####<a name="3.1"></a> Motivation
####<a name="3.2"></a> Validation
####<a name="3.3"></a> Preparation
####<a name="3.4"></a> Storage
####<a name="4"></a> Drilling Holes in Beams

####<a name="2"></a> RvtParamDrop Exports Visible Element Properties

The first side project was [RvtParamDrop](https://github.com/jeremytammik/RvtParamDrop).

It simply exports all properties of all elements visible in a selected view for comparison and verification of all expected parameter values.

The most interesting aspect is that all referenced elements and all their parameter also need to be included, recursively.

In more detail, it generates a count and a `csv` of Revit parameters on elements in a view.

- Do not limit yourself to shared parameters
- Do limit yourself to parameters with a value
- Name of the parameter
- Schema (`TypeId`)

A small number of parameters are intentionally ignored as redundant:

- ELEM_CATEGORY_PARAM
- ELEM_CATEGORY_PARAM_MT
- ELEM_FAMILY_AND_TYPE_PARAM
- ELEM_TYPE_PARAM
- SYMBOL_ID_PARAM

Include parameters from both elements and their types, i.e., both instance and type parameters.

Actually, it's more complicated than that.
Anything that is visible in the view will include its instance and type parameters.
If an instance or a type has a parameter that refers to another `Element`, its instance and type parameters are also exported, regardless of whether it is visible or not.
That is recursive, so if X references Y references Z references W, then W's parameters are exported if X, Y, or Z is visible.
We follow all references.
Who are we to say that a referenced `Element` isn't useful?

If you find this useful or interesting, please let me know. 

####<a name="3"></a> RvtLock3r Validate BIM Element Properties 

Another on-going project is still WIP, and also an exercise getting started with the Revit API for
my new colleague [Caroline](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/11981988).

[RvtLock3r](https://github.com/jeremytammik/RvtLock3r) validates
that certain specified BIM element properties have not been modified.

Here are some notes from the current state of the project repository readme:

####<a name="3.1"></a> Motivation

Revit does not provide any functionality to ensure that shared parameter values are not modified.

The add-in stores a checksum for the original intended values of selected shared parameters and implements a validation function to ensure that the current values compute the same checksum.

The validation function is initially implemented as an external command.

It may later be triggered automatically on opening or saving a document to notify the user that undesired tampering has taken place.

####<a name="3.2"></a> Validation

The customer add-in reads a set of [ground truth](https://en.wikipedia.org/wiki/Ground_truth) data from some [storage location](#storage). It contains a list of triples:

- `ElementId`
- Shared parameter `GUID`
- Checksum

The add-in iterates over all elements and shared parameters specified by these triples, reads the corresponding shared parameter value, calculates its checksum and validates it by comparison with the ground truth value.

Discrepancies are logged and a report is presented to the user.

The add-in does not care what kind of elements or shared parameters are being examined.
That worry is left up to whoever creates the ground truth file.

In the initial proof of concept, the triples are simply space separated in individual lines in a text file.

####<a name="3.3"></a> Preparation

There are various possible approaches to prepare
the [ground truth](https://en.wikipedia.org/wiki/Ground_truth) input text file,
and they can be completely automated, more or less programmatically assisted, or fully manual.

In all three cases, you will first need to determine up front what elements and which shared parameters on them are to be checked. Retrieve the corresponding parameter values, compute their checksums, and save the above-mentioned triples.

####<a name="3.4"></a> Storage

The ground truth data triples containing the data rerquired for integrity validation needs to be stored somewhere. That could be hard-wired directly into the add-in code for a specific BIM, stored in an external text file, within the `RVT` document, or elsewhere; it may be `JSON` formatted; it may be encrypted; still to be decided.

Two options are available for storing custom data directly within the `RVT` project file: shared parameters and extensible storage.
The latter is more modern and explicitly tailored for use by applications and data that is not accessible to the end user or even Revit itself.
That seems most suitable for our purpose here.
Extensible storage can be added to any database element.
However, it interferes least with Revit operation when placed on a dedicated `DataStorage` element,
especially [in a worksharing environment](http://thebuildingcoder.typepad.com/blog/2015/02/extensible-storage-in-a-worksharing-environment.html).
Creation and population of a `DataStorage` element is demonstrated by the [named GUID storage for project identification](https://thebuildingcoder.typepad.com/blog/2016/04/named-guid-storage-for-project-identification.html) sample.

####<a name="4"></a> Drilling Holes in Beams

Getting back to real-life issues,
Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas explains
how to drill a hole in a beam using a void or an opening by face in a family definition in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to create different unattached families to a `.rft` file without stacking it](https://forums.autodesk.com/t5/revit-api-forum/how-to-create-different-unattached-families-to-a-rft-file/td-p/10934607):

**Question:** I'm coding a tool to drill holes to beams (structural framing).
Each beam will host 2 drills, one at each end.
To create my void extrusion to cut the host, I create an `Arc` at the coordinate (0, 0, 0) and extrude it in my *Metric Generic Model.rtf* located in the *C:\ProgramData\Autodesk\RVT 2020\Family Templates\English* directory.
Then, I load the family into the document with the beams to be drilled and get the `FamilySymbol` like this:

<pre class="code">
  Family&nbsp;family&nbsp;=&nbsp;familyTemplateDoc.LoadFamily(doc);
 
  FamilySymbol&nbsp;familySymbol&nbsp;=&nbsp;family.GetFamilySymbolIds()
  &nbsp;&nbsp;.Select(<span style="color:#1f377f;">x</span>&nbsp;=&gt;&nbsp;doc.GetElement(x)&nbsp;<span style="color:blue;">as</span>&nbsp;FamilySymbol)
  &nbsp;&nbsp;.FirstOrDefault();
</pre>

In my document I insert the family instance using `NewFamilyInstance` at the desired coordinate and finally `AddInstanceVoidCut` the beams.
I'm facing the following issue (I moved the voids up so we can see it easily):

<center>
<img src="img/stacked_voids.png" alt="Stacked voids" title="Stacked voids" width="400"/> <!-- 1427 -->
</center>

Apparently, each time I extrude a new `Arc` I am stacking it (the thicker cylinder is above for visualisation, it would overwrite the thinner one).
The next beam to be drilled will keep stacking the void forms. So if I select 4 beams, the 8th hole will be done by the 8 void forms stacked.
What could I be doing wrong?
The extrusion needs to be created at the (0, 0, 0) coordinate so it can be easily modified at the .rfa file, beeing easily found throughout the .rfa file.

**Answer:** This sounds like a case of wrong type of family template and wrong `NewFamilyInstance` overload.
There are easier ways to create holes in beams:

- Use "Metric Generic Model face based.rft" cut the host with the void in the family.
- Load the family into the project
- Host the family on the beam web:
- NewFamilyInstance(Face, XYZ, XYZ, FamilySymbol) or
- NewFamilyInstance(Reference, XYZ, XYZ, FamilySymbol)

Note also there is no need to even create a family for this, you can create an opening by face:

- Document.NewOpening

**Response:** I wanted to use voids in order to learn it; I'm new at the API and I was overcomplicating the solution.
It's kinda overkill what I was trying to do...

Switched up to `NewOpening` and it works like a charm! Thank you.

Many thanks once again to Richard for contributing all his reliable in-depth help and experience in the forum!

