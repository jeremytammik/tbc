<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/get-tree-view-from-system-browser-revit-mep/m-p/7855032

- connector neighbours
  https://forums.autodesk.com/t5/revit-api-forum/connector-neighbours/m-p/7816952
  https://github.com/geoffoverfield/RevitAPI_SystemSearch
  https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/MepSystemSearch.cs

- connect into
  https://forums.autodesk.com/t5/revit-api-forum/connect-into/m-p/7834417

- 13840360 [How do I create conduit between 2 connectors?]
  https://forums.autodesk.com/t5/revit-api-forum/how-do-i-create-conduit-between-2-connectors/m-p/7727929

- [read-only transition diameter](https://forums.autodesk.com/t5/revit-api-forum/transition-diameter-read-only/m-p/7620036)
  Q: in pipe transition, parameters for Diameter is Read Only, any one know how to walk around this?
  I thing I can do small pipes on both site, but I would like to avoid this.
  A: you can use dummy pipes for creating transitions.
  Create two pipes of the desired diameters somewhere outside all other elements.
  Connect them by doc.NewTransitionFitting.
  Delete the dummy pipes.
  The remaining transition FamilyInstance will still have the correct diameter parameter values.
  Move it to the desired location.
  Revitalizer

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon

&ndash;
...

--->

### Connector, Neighbour, Conduit, Transition

Here are a couple of MEP and connector related cases that I made notes of during the past few weeks:

- [Traversing an MEP system and retrieving connected elements](#2)
- [Reproducing the MEP electrical system browser hierarchy](#3)
- [Connect into](#4)
- [Creating a conduit between two connectors](#4)
- [Dealing with the read-only transition diameter](#5)


####<a name="2"></a>Traversing an MEP System and Retrieving Connected Elements

connector neighbours
https://forums.autodesk.com/t5/revit-api-forum/connector-neighbours/m-p/7816952
https://github.com/geoffoverfield/RevitAPI_SystemSearch
https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/MepSystemSearch.cs

I am not sure which of the apporaches is best.

It can't hurt to check them out and compare them.

Please let me know what you think and how well they suit your needs.

####<a name="3"></a>Reproducing the MEP Electrical System Browser Hierarchy

https://forums.autodesk.com/t5/revit-api-forum/get-tree-view-from-system-browser-revit-mep/m-p/7855032

I implemented two different methods to reproduce the MEP Electrical System Browser Hierarchy structure.

- Before the advent of any MEP specific functionality in the Revit API, I made use of generic parameter values stored on the respective system elements.
- After the introduction of the Revit MEP API, I implemented a simpler and safer solution making use of that.


####<a name="4"></a>Connect Into

[connect into](https://forums.autodesk.com/t5/revit-api-forum/connect-into/m-p/7834417)

####<a name="4"></a>Creating a Conduit Between Two Connectors

[How do I create conduit between 2 connectors](https://forums.autodesk.com/t5/revit-api-forum/how-do-i-create-conduit-between-2-connectors/m-p/7727929)





####<a name="5"></a>Dealing with the Read-Only Transition Diameter

In the
corresponding [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread,
Rudi [@Revitalizer](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1103138) Honke
explained how to deal with
the [read-only transition diameter](https://forums.autodesk.com/t5/revit-api-forum/transition-diameter-read-only/m-p/7620036):

**Question:** In a pipe transition the parameters defining the diameter are read-only; does anyone know how to walk around this?

I think I can create small dummy pipes on both sides, but I would like to avoid this.

**Answer:** You can use dummy pipes for creating transitions.

- Create two pipes of the desired diameters somewhere far away from all other elements.
- Connect them by calling `doc.NewTransitionFitting`.
- Delete the dummy pipes.

The remaining transition `FamilyInstance` will retain the correct diameter parameter values.

Move it to the desired location.

Many thanks to Revitalizer for this hint!


<center>
<img src="img/.png" alt="" width="100"/>
</center>


<pre class="code">
</pre>

