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

Use Forge or Spreadsheet to Create Shared Parameters #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/rvtmetaprop

I spent last week working on a new little BIM sample showing a round trip connection between Revit and Forge, and an automated way of generating and populating shared parameters from spreadsheet data
&ndash; RvtMetaProp &ndash; Revit meta property add-in
&ndash; CSV and JSON input file format
&ndash; Forge configurator sample
&ndash; Round-Trip Forge meta property editor
&ndash; BipGroupList lists built-in parameter group enums and labels
&ndash; Two options to add custom properties to the Revit BIM
&ndash; Translation of shared parameters to Forge
&ndash; Handling BIM floors in Forge...

--->

### Use Forge or Spreadsheet to Create Shared Parameters

I spent last week working on a new little BIM sample showing a round trip connection between Revit and Forge, and an automated way of generating and populating shared parameters from spreadsheet data:

- [RvtMetaProp &ndash; Revit meta property add-in](#1)
- [CSV and JSON input file format](#2)
- [Forge configurator sample](#3)
- [Round-Trip Forge meta property editor](#4)
- [BipGroupList lists built-in parameter group enums and labels](#5)
- [Two options to add custom properties to the Revit BIM](#6)
- [Translation of shared parameters to Forge](#7)
- [Handling BIM floors in Forge](#8)

####<a name="1"></a>RvtMetaProp &ndash; Revit Meta Property Add-In

In Forge, [Philippe Leefsma](https://twitter.com/F3lipek) implemented
a [meta property editor](http://meta-editor.autodesk.link) enabling
you to modify existing and add custom data to any Forge model.

The additional data is stored in a parallel database.

On the Revit side, I implemented
the [RvtMetaProp add-in](https://github.com/jeremytammik/rvtmetaprop) to
read in the additional or modified properties and update the BIM accordingly.

Handily enough, it can also be used as a stand-alone utility to automatically create shared parameters and populate their values on BIM elements from a spreadsheet, completely independent of the Forge app.

It reads the properties associated with individual BIM elements from a `CSV` or `JSON` file.

If the property corresponds to an existing parameter on a BIM element, its value is updated accordingly.

For a new property, a shared parameter is created.

For a quick first impression, check out
the [four-and-a-half-minute recording](https://youtu.be/I5AvbSrZ3Wk) of this add-in in action.

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/I5AvbSrZ3Wk?rel=0" frameborder="0" allowfullscreen></iframe>
</center>


####<a name="2"></a>CSV and JSON Input File Format

The `CSV` and `JSON` input files specify the following data, which correspond to the list Revit information:

- `externalId` &ndash; the Revit database element `UniqueId`
- `component` &ndash; element name and element id (ignored)
- `displayCategory` &ndash; built-in parameter group name under which to display and store a shared parameter
- `categoryId` &ndash; built-in parameter group enumeration value as string
- `displayName` &ndash; meta property name
- `displayValue` &ndash; meta property value
- `metaType` &ndash; meta property data type; for Revit, all but Double, Int and Text are ignored
- `filelink` &ndash; meta property file URL (ignored)
- `filename` &ndash; meta property file name (ignored)
- `link` &ndash; meta property link URL (ignored)

As you can see, some information defined in Forge and specified in the files may be ignored when importing into Revit.

The `CSV` file format looks like this:

<pre>
"externalId","component","displayCategory","categoryId","displayName","displayValue","metaType","filelink","filename","link"
"7df7740a-9736-4a3e-81ec-45e05b0d2ad2-0000c28d","Basic Wall [49805]","General","PG_GENERAL","test_text","this is a text added in forge","Text",,,
"7df7740a-9736-4a3e-81ec-45e05b0d2ad2-0000c28d","Basic Wall [49805]","General","PG_GENERAL","test_real","0.12","Double",,,
"7df7740a-9736-4a3e-81ec-45e05b0d2ad2-0000c28d","Basic Wall [49805]","General","PG_GENERAL","test_int","12","Int",,,
</pre>

The fields are read in an order dependent manner.

The `JSON` file contents are analogous:

<pre>
[
  {
    "displayCategory": "General",
    "displayValue": "this is a text added in forge",
    "displayName": "test_text",
    "categoryId": "PG_GENERAL",
    "externalId": "7df7740a-9736-4a3e-81ec-45e05b0d2ad2-0000c28d",
    "component": "Basic Wall [49805]",
    "metaType": "Text"
  },
  {
    "displayCategory": "General",
    "displayValue": "0.12",
    "displayName": "test_real",
    "categoryId": "PG_GENERAL",
    "externalId": "7df7740a-9736-4a3e-81ec-45e05b0d2ad2-0000c28d",
    "component": "Basic Wall [49805]",
    "metaType": "Double"
  },
  {
    "displayCategory": "General",
    "displayValue": "12",
    "displayName": "test_int",
    "categoryId": "PG_GENERAL",
    "externalId": "7df7740a-9736-4a3e-81ec-45e05b0d2ad2-0000c28d",
    "component": "Basic Wall [49805]",
    "metaType": "Int"
  }
]
</pre>

You can create a `CSV` or `JSON` input file matching this format to generate new shared parameters in your BIM, either using the Forge meta property editor, or in any other way you like.


####<a name="3"></a>Forge Configurator Sample

A sample meta property editor is included in 
the [online Forge configurator sample](https://forge-rcdb.autodesk.io/configurator):

- Scroll down through the models to *Meta Properties*.
- In the left-hand drop-down menu, select *Office*.
- Click on the *Meta Properties* box.

The [office model](https://forge-rcdb.autodesk.io/configurator?id=59780eec17d671029c53420e) is
displayed, and its properties displayed in a panel on the right-hand side.

You can select any BIM element and see all its properties as well.

The buttons on the top right-hand side of the property panel enable search, export to `CSV` and `JSON`, and adding new properties.

Each property can also be deleted.

The models in this sample are hard-wired.


####<a name="4"></a>Round-Trip Forge Meta Property Editor

To demonstrate round-trip meta property editing on your own Revit BIM model in the Forge Viewer, Philippe implemented
the [Forge meta property editor](http://meta-editor.autodesk.link).

<center>
<img src="img/meta_editor.png" alt="Forge meta property editor" width="600"/>
</center>

It enables you to upload your own model, add properties to it in the Forge viewer, download the meta property specifications, and integrate them into the BIM seed CAD file using the RvtMetaProp add-in.

Again, the buttons on the top right-hand side of the property panel enable adding new properties (the plus sign icon) and export to `CSV` and `JSON` (the cloud icon).

RvtMetaProp reads modification into Revit and updates the BIM accordingly.

Retranslation of the updated BIM to Forge completes the round trip.

In order to enable the round trip intact, the meta property data types and group names are restricted to those supported by Revit shared parameters:

- Data types &ndash; restricted to Revit parameter storage types
    - Text
    - Double
    - Int
    
<center>
<img src="img/meta_editor_data_types.png" alt="Forge meta property editor data types" width="334"/>
</center>

- Property group &ndash; restricted to one of the 116 Revit built-in parameter groups
    - Data
    - General
    - Other
    - Text
    - ...

<center>
<img src="img/meta_editor_param_group.png" alt="Forge meta property editor parameter groups" width="488"/>
</center>

####<a name="5"></a>BipGroupList Lists Built-In Parameter Group Enums and Labels

The list of Revit built-in parameter group enumeration values and display string labels was generated by
the [BipGroupList add-in](https://github.com/jeremytammik/BipGroupList) and
reformatted into a JavaScript dictionary mapping the enums to the labels using a regular expression.



####<a name="6"></a>Two Options to Add Custom Properties to the Revit BIM

Before implementing RvtMetaProp, I pondered the best way to add custom properties to a Revit BIM.

Basically, there are two fundamentally different approaches, as shown by the following Q &amp; A:

**Question:** How can I import updated and added properties into the Revit BIM?

**Answer:** If all you need to do is attach additional information such as your database GUID to a Revit database element, the solution is easy.

You have two options to programmatically attach arbitrary data to building elements in the Revit BIM:
 
- Traditional end user approach: [shared parameters](https://knowledge.autodesk.com/support/revit-products/learn-explore/caas/CloudHelp/cloudhelp/2015/ENU/Revit-Model/files/GUID-E7D12B71-C50D-46D8-886B-8E0C2B285988-htm.html) &ndash; pros and cons:
    - It comes with a user interface, the standard element property panel.
    - It is visible to Revit, exported to Forge can be used for scheduling, etc.
    - Shared parameters are defined per `Category`, not on a per-`Element` basis.
- New, API specific functionality: [extensible storage](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23) &ndash; pros and cons:
    - No UI, you would have to implement that yourself.
    - The data is equipped with a protection level, is mostly ignored by Revit, therefore cannot be used for scheduling, is not exported to Forge, etc.
    - Extensible storage is assigned on a per-`Element` basis.


####<a name="7"></a>Translation of Shared Parameters to Forge

An important fundamental factor affecting this workflow is the question of how shared parameter values make it across through the translation into Forge.

**Question:** Are shared parameters included in the RVT to Forge translation? Or only some of them? What are the criteria, please? I added a shared parameter to a BIM, and it does not appear in the translated Forge result. Thank you!

**Answer:** Shared parameters are NOT included in Revit extraction. Since shared parameters are saved in external file outside of RVT, the shared parameter files are not available to extractor during translation.

**Response:** the shared parameter values, however, seem to make it across, according to my testing. here is the value of a shared parameter named "jeremy_text" in the built-in parameter group '`General` that I created in Revit being displayed on A360: (edited)

<center>
<img src="img/shared_property_a360.png" alt="Shared parameter in A360 viewer" width="489"/>
</center>

**Answer:** Visible shared parameters are included in the Revit extraction. While invisible shared parameters are Not included.

####<a name="8"></a>Handling BIM Floors in Forge

While on the topic of using Forge with BIM, let's also mention some tips and tricks on handling floors in Forge:

**Question:** Can you suggest a viable approach to identify (and display) building objects (from furniture to multi-floor structural objects) on specific floors?

I would like to present analysis results 'by floor' and am challenged to deal with some entities on a specific floor while others (like columns and curtain walls) span multiple floors.
 
**Answer:** Floors is definitely an issue, especially in some models which include half-floors.

We also see limitations associated with model elements which span multiple floors.

Another problem is caused by modelling errors, generating elements whose levels have been incorrectly designated in the source BIM.
 
One possible approach to address the first issue would be to use Revit Parts to divide elements by level, enabling each part to be separately associated with its appropriate floor level. This process can be automated using an add-in. Otherwise, it requires extensive input to the Revit model.
 
One can use a settings file to specify an association between the floor names to display for a particular model and the level property associated with each element coming from the BIM model. Often there are multiple levels defined in Revit for a single floor; these multiples can be mapped to a single floor name which appears in the Forge navigation. The highlighting functionality associated with the Forge navigation is enabled by the room objects in the model and their association with a particular level. Once a user selects a level to view in isolation, the visibility of elements is filtered based on the level property of each individual object.
 
Aside from the elements which span multiple floors, the effective display of floor levels in the Forge Viewer is dependent on the rigor used in developing the BIM model itself. For example, if elements are associated with the 1st floor but reside on the 2nd floor, the results in the viewer will be messy once the filters are applied to the display.
 
