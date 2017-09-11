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

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

&ndash; ...

--->

### Creating Shared Parameters from Spreadsheet

I spent last week working on a new little BIM sample showing a round trip connection between Revit and Forge.

In Forge, Philippe Leefsma implemented
a [meta property editor](http://meta-editor.autodesk.link) enabling you to modify existing and add custom data to any Forge model.

The additional data is stored in a parallel database.

On the Revit side, I implemented the RvtMetaProp add-in to read in the additional or modified properties and update the BIM accordingly.

Handily enough, it can also be used as a stand-alone utility to automatically create shared parameters and populate their values on BIM elements from a spreadsheet, completely independant of the Forge app.


####<a name="2"></a>Task


####<a name="2"></a>Forge Configurator
####<a name="2"></a>CSV and JSON Export
####<a name="2"></a>Creating a Shared Parameter
####<a name="2"></a>Round-Trip
####<a name="2"></a>Video Recording
####<a name="2"></a>Download


<center>
<img src="img/.png" alt="" width="259"/>
</center>


####<a name="2"></a>Translation of Shared Parameters to Forge

**Question:** Are shared parameters included in the RVT to Forge translation? Or only some of them? What are the criteria, please? I added a shared parameter to a BIM, and it does not appear in the translated Forge result. Thank you!

**Answer:** Shared parameters are NOT included in Revit extraction. Since shared parameters are saved in external file outside of RVT, the shared parameter files are not available to extractor during translation.

**Response:** the shared parameter values, however, seem to make it across, according to my testing. here is the value of a shared parameter named "jeremy_text" in the built-in parameter group '`General` that i created in Revit being displayed on A360: (edited)

shared this image: shared_property_a360.png

**Answer:** Visible shared parameters are included in the Revit extraction. While invisible shared parameters are Not included.

####<a name="2"></a>Handling BIM Floors in Forge

**Qyestion:** Can you suggest a viable approach to identify (and display) building objects (from furniture to multi-floor structural objects) on specific floors?

I would like to present analysis results 'by floor' and am challenged to deal with some entities on a specific floor while others (like columns and curtain walls) span multiple floors.
 
**Answer:** Floors is definitely an issue, especially in some models which include half-floors.

We also see limitations associated with model elements which span multiple floors.

Another problem is caused by modelling errors, generating elements whose levels have been incorrectly designated in the source BIM.
 
One possible approach to address the first issue would be to use Revit Parts to divide elements by level, enabling each part to be separately associated with its appropriate floor level. This process can be automated using an add-in. Otherwise, it requires extensive input to the Revit model.
 
One can use a settings file to specify an association between the floor names to display for a particular model and the level property associated with each element coming from the BIM model. Often there are multiple levels defined in Revit for a single floor; these multiples can be mapped to a single floor name which appears in the Forge navigation. The highlighting functionality associated with the Forge navigation is enabled by the room objects in the model and their association with a particular level. Once a user selects a level to view in isolation, the visibility of elements is filtered based on the level property of each individual object.
 
Aside from the elements which span multiple floors, the effective display of floor levels in the Forge Viewer is dependent on the rigor used in developing the BIM model itself. For example, if elements are associated with the 1st floor but reside on the 2nd floor, the results in the viewer will be messy once the filters are applied to the display.
 


