<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

</head>

<!---

twitter:

AttachedDetailGroup, CreateTrianglesTopography, Custom2DExporter, PathOfTravel, ViewTemplateCreation -- new Revit 2020 SDK samples in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/2020sdksamples

Question: What can we do with the new Revit 2020 API features?
Well, just like most of the existing functionality, the enhancements and some typical yet simple use cases for them are demonstrated by the updated SDK samples.
Revit 2020 sports five brand new SDK samples
&ndash; AttachedDetailGroup
&ndash; CreateTrianglesTopography
&ndash; Custom2DExporter
&ndash; PathOfTravel
&ndash; ViewTemplateCreation
&ndash; This list also highlights some the most exciting areas of enhancement...

linkedin:

New Revit 2020 SDK samples in the #RevitAPI #bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK

http://bit.ly/2020sdksamples

Question: What can we do with the new Revit 2020 API features?

Well, just like most of the existing functionality, the enhancements and some typical yet simple use cases for them are demonstrated by the updated SDK samples.

Revit 2020 sports five brand new SDK samples:

- AttachedDetailGroup
- CreateTrianglesTopography
- Custom2DExporter
- PathOfTravel
- ViewTemplateCreation

This list also highlights some the most exciting areas of enhancement...

-->

### New Revit 2020 SDK Samples

So far, we looked at the following aspects of the Revit 2020 API enhancements:

- [Revit 2020 DevDays Online presentation recording](https://thebuildingcoder.typepad.com/blog/2019/04/the-revit-2020-fcs-api-and-sdk.html#3)
- [Compiling the Revit 2020 SDK samples](https://thebuildingcoder.typepad.com/blog/2019/04/the-revit-2020-fcs-api-and-sdk.html#7) 
- [Setting up RvtSamples](https://thebuildingcoder.typepad.com/blog/2019/04/the-revit-2020-fcs-api-and-sdk.html#8)
- [RevitLookup for Revit 2020](https://thebuildingcoder.typepad.com/blog/2019/04/revitlookup-and-sdk-for-revit-2020.html#2) 
- [Revit 2020 SDK posted to Revit Developer Centre](https://thebuildingcoder.typepad.com/blog/2019/04/revitlookup-and-sdk-for-revit-2020.html#3)
- [What's New in the Revit 2020 API](https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html)
- [Migration of The Building Coder samples](https://thebuildingcoder.typepad.com/blog/2019/04/close-doc-and-zero-doc-rvtsamples.html#3)

The next obvious question is:

What can we do with the new features?

Well, just like most of the existing functionality, the enhancements and some typical yet simple use cases for them are demonstrated by the updated SDK samples.

Revit 2020 sports five brand new SDK samples:

- [AttachedDetailGroup](#2)
- [CreateTrianglesTopography](#3)
- [Custom2DExporter](#4)
- [PathOfTravel](#5)
- [ViewTemplateCreation](#6)

This list also highlights some the most exciting areas of enhancement.

AttachedDetailGroup implements an external application with a ribbon panel UI providing access to its two commands.

The other four implement just one external command each.

####<a name="2"></a> AttachedDetailGroup

Revit 2020 boasts
several [attached detail group API additions](https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html#4.2.1).

The AttachedDetailGroup sample shows how to use these to show and hide the attached detail groups of a model group in the active view.

In its readme file, it purports to be released with Revit 2019.1, although in fact it did not appear before the Revit 2020 SDK.

It includes three modules:

- Application.cs defines the Application class that creates the UI ribbon components when Revit starts.
- AttachedDetailGroupShowAllCommand.cs and AttachedDetailGroupHideAllCommand.cs implements its two external commands to show and hide attached detail groups, respectively.

Demonstration instructions:

- Create two parallel walls with an aligned dimension between them.
- Select the two walls and the dimension and create a group.
    - Expected result: a model group containing the two walls and an attached detail group containing the dimension are created.
- Select the model group.
- Select the Addins ribbon menu.
- Execute the Hide All Detail Groups command in the Attached Detail Group ribbon panel.
    - Expected result: The attached detail group is no longer visible.
- Execute the Show All Detail Groups command in the Attached Detail Group ribbon panel.
    - Expected result: The attached detail group is now visible.

####<a name="3"></a> CreateTrianglesTopography

Revit 2020 supports the creation of concave topography surfaces using
the [new `TopographySurface` API additions](https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html#4.2.12.1).

The CreateTrianglesTopography SDK sample creates a new topography surface element from triangle points and triangle facets using the new overload of the `TopographySurface` `Create` API method,

In its readme file, it purports to be released with Revit 2019.2, although in fact it did not appear before the Revit 2020 SDK.

Command.cs implements the *Create Triangles Topography* external command.

TrianglesData.cs defines two classes, `TrianglesData` and `XYZConverter`.

The `TrianglesData.json` input file contains all the points and facets data to define the topography surface.
The contents of this file can be deserialised to a `TrianglesData` object.

`TrianglesData` parses the triangle points and triangle facets stored in the `TrianglesData.json` input file.
These points and facets are passed to Revit API method to create the topography surface.

`XYZConverter` converts to Revit `XYZ` instances using a `JavaScriptSerializer`.

<center>
<img src="img/CreateTrianglesTopography.png" alt="CreateTrianglesTopography SDK sample" width="252">
</center>

####<a name="4"></a> Custom2DExporter

In Revit 2020,
the [`CustomExporter` supports some 2D views](https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html#4.2.16.1).

The Custom2DExporter SK sample demonstrates its use to export 2D views using various exporter options, enabling the user to export and visualise 2D model and annotation geometry.

This sample lives in the new `CustomExporter` subfolder.

In its readme file, it purports to be released with Revit 2019.0, although in fact it did not appear before the Revit 2020 SDK.

The external command defined by Command.cs displays the dialog defined in Export2DForm.cs enabling you to pick the exporter options and  export the current view.

TessellatedGeomAndText2DExportContext.cs implements the `IExportContext2D` used for the 2D export. This particular implementation exports 2D curves tessellated into lines and also does some rudimentary text exporting.

Notes:

- When exporting, the current view is used.
- All exported geometry is tessellated into lines. 
- Exported geometry is then visualised by hiding all elements in the view and displaying all exported lines with detail lines. 
- To view your original un-exported 2D view, undo once to remove the drawn export.

The sample also includes a proof of concept for exporting text. The absolute minimum is exported, with all text notes separated by a newline. However, `OnText` receives all necessary information about text location, font, size and formatting, so the API user can reconstruct it in its entirety.

Some important details:

- Only visible geometry is exported.
- Meshes are not supported.

Known issues:

- Some annotations will not automatically export in this sample. Known examples are: sections, reference planes, scope boxes, plan regions. It is recommended that additional code be written in `OnElementBegin` to detect these elements and export any necessary geometry by writing custom code.

Instructions:

- Export model geometry:
    - Create a Revit model which contains both model and annotation elements.
    - Execute the 'Export 2D Views with CustomExporter' command on a plan, elevation or section. Before executing, do not pick any extra options (annotations or patterns) in the dialog.
    - Expected result: all elements in the view are hidden, and instead of them model geometry is drawn using detail lines; annotations are hidden. Any non-line model curves are tessellated into lines. Dismiss the results dialog and undo once to get to your pre-command model state.
- Export model and annotations:
    - Create a Revit model which contains both model and annotation elements..
    - Execute the 'Export 2D Views with CustomExporter' command on a plan, elevation or section. Before executing, pick 'Export annotations and text.
    - Expected result: all elements in the view are hidden, and instead of them, model and annotation geometry is drawn using detail lines. Any non-line model or annotation curves are tessellated into lines. In the results dialog, you get a dump of all text that was found in the view. Dismiss the results dialog and undo once to get to your pre-command model state.
- Export pattern lines:
    - If your model contains elements which have patterns applied on their faces, pick 'Export pattern lines' to export and display those patterns on screen.
	

####<a name="5"></a> PathOfTravel

Revit 2020 enhances the Analysis API with support for
a new [Path of Travel API](https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html#4.2.19).

The PathOfTravel SDK sample demonstrates how to use it to create `PathOfTravel` elements from rooms to doors in a plan view and:

-	Automatically computes room centre and corner points to use for the start point of the path.
-	When the option for path to a single door is selected, lets the user pick which door to use for the end point of the path.
-	Automatically creates PathOfTravel elements between the selected combination of room and door elements.

The external command defined by Command.cs displays the dialog defined in CreateForm.cs.

It consists of three radio controls selecting one of three different PathOfTravel creation methods.

Instructions:

- Create some rooms and exit doors.
- Open a plan view in which the created elements are visible.
- Execute the external command. In the dialog, pick an option to create the paths. After clicking OK, the paths will be created and displayed in the view.

####<a name="6"></a> ViewTemplateCreation

The Revit 2020 API includes
some [View Template API additions](https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html#4.2.10.1).

The ViewTemplateCreation SDK sample shows how to use them to create and configure a new view template from a regular view and a view template settings configuration, including:

-	Create new view template based on the selected view
-	Set include setting of 'Parts Visibility' parameter
-	Set the value of 'Detail Level' parameter
-	Change some Visibility/Graphics overrides for a few model categories

The external command defined in Command.cs creates and shows the view template creation form.

ViewTemplateCreationForm.cs declares this form, enabling selection of a view for template creation and setting the 'Parts Visibility' and 'Detail Level' parameters. On pressing 'Apply', a new view template will be created based on the selected view and parameter settings. V/G Overrides Model cut patterns settings will be changed for the following categories: Columns, Doors, Walls, Windows.
V/G Model cut patterns settings will be changed in the following way: 'Foreground Pattern' will be set to 'Solid fill', 'Background color' will be set to 'Black'. Finally, the newly created template will be assigned to the selected view.

Utils.cs contains several helper methods for showing message boxes and a constant string with the name of the sample.

Instructions:

Launch Revit and create a new project from the Architectural template.

- Execute the command.
    - Expected result: View template creation form appears.
- Select a view for view template creation.
- Select include setting of 'Parts Visibility' parameter.
- Select value of 'Detail Level' parameter.
- Press 'Apply' button.
    - Expected result: new view template is created based on the selected view, parameter settings are applied to parameters, V/G Overrides Model cut patterns settings are changed for following categories: Columns, Doors, Walls, Windows. V/G Model cut patterns settings are changed in the following way: 'Foreground Pattern' is set to 'Solid fill', 'Background color' is set to 'Black'. The newly created view template is assigned to the selected view.

I hope you find these enhancements useful.

Many of them are driven by developer requests.

I look forward to hearing about exciting new applications using them.
