<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

Angel Velez RE: Case 11876523  help:  Anormal modification of Element.UniqueId

11876523 [Anormal modification of Element.UniqueId]

http://forums.autodesk.com/t5/revit-api/anormal-modification-of-element-uniqueid/m-p/6372628

Consistency of IFC GUID and UniqueId #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

Angel Velez provided further clarification on the relationship between the Revit element UniqueId and the IFC GUID generated from it, prompted by the Revit API discussion forum thread on abnormal modification of Element.UniqueId...

-->

### Consistency of IFC GUID and UniqueId

Angel Velez provided further clarification on the relationship between the Revit element `UniqueId` and the IFC GUID generated from it, prompted by
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread 
on [abnormal modification of `Element.UniqueId`](http://forums.autodesk.com/t5/revit-api/anormal-modification-of-element-uniqueid/m-p/6372628):

#### <a name="2"></a>Question

We encounter a problem with the management of the GUID returned by `Element.UniqueId`.

- When deleting an element, and then using the Undo function, the element that reappears still has the same element id and the same `UniqueId`.
- In another case, if after deleting, we save the project and then use the Undo function, the element that reappears still has the same Id but the `UniqueId` changed!
- The same problem also exists with IFC GUID.
 
This bug obviously causes problems in plugins that manage an external database or other tools using the IFC export.

#### <a name="3"></a>Answer

First of all, here are some previous discussions on the relationship between the unique id and the IFC GUID:

- [UniqueId, DWF and IFC GUID](http://thebuildingcoder.typepad.com/blog/2009/02/uniqueid-dwf-and-ifc-guid.html)
- [IFC GUID Algorithm in C#](http://thebuildingcoder.typepad.com/blog/2010/06/ifc-guid-algorithm-in-c.html)
- [IFC GUID Generation and Uniqueness](http://thebuildingcoder.typepad.com/blog/2012/09/ifc-guid-generation-and-uniqueness.html)
- [IFC GUID Algorithm Update and Family Modification](http://thebuildingcoder.typepad.com/blog/2014/07/ifc-guid-algorithm-update-and-family-modification.html)

In them, we explain that the GUID generation routines have an internal cache that prevents duplicate GUIDs from being generated. In IFC, these functions are called once per element, and then a call to the `ExporterIFCUtils.EndExportInternal` method clears this cache for the next IFC export. Please note that the first time you call these routines, they have consistent values from previous sessions.

For now, there are two workarounds:

1. Only call the functions once per session, and store their values.
2. Call `ExporterIFCUtils.EndExportInternal` to reset the internal cache.

To recapitulate, the IFC GUID is based on the Revit `UniqueId`.
If the `UniqueId` is changing, then so will the IFC GUID.

I suspect that when you save a file, you change the `EpisodeId` of the document.
The unique id of an element is based on the `EpisodeId` and the `ElementId`.
If the `EpisodeId` is reset on an undo of a delete, that would explain it.  
 
For the issue of consistent GUIDs, Revit ensures that:
 
1. No two GUIDs in the IFC file are ever the same. This is a 100% guarantee.
2. For all IFC entities that are created from Revit elements, we create a consistent GUID (it doesn't change between exports).  Note that that is based on the `UniqueId` being consistent, so if there is an issue there, then that will clearly affect IFC export.
3. For other entities, on a case-by-case basis, we can make some of them have consistent GUIDs.  For many, though, it isn't possible (or extremely difficult). 
 
I looked at your sample files and note a couple of cases that could be improved.
In general, though, most of the GUIDs are consistent and unique as per rules 1 and 2.
We will improve this over time to reduce the noise in 3, but it is very difficult to remove all of the noise without a lot of extra storage in the Revit file.
