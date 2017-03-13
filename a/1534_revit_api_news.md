<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- Revit API News
  https://www.youtube.com/watch?v=7haXE4oOgZk&list=PL4CYqBFEfhpJ5oh_vZzqpD_8mIpFLk5yB&index=2
  https://www.youtube.com/watch?v=7haXE4oOgZk
  The section discussing the Revit API news begins at [12:45](https://www.youtube.com/watch?v=7haXE4oOgZk&t=765s).
  To learn more about Revit API basics, please refer to the [discussion and overview of getting started material](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2﻿).

RevitLookup and DevDays Online API News #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/devdays2016online

Unprecedented exciting information to share with you today!
DevDays Online recordings are available right now, before the product launch!
Furthermore, we are proud to present another little update of the revamped version of RevitLookup
&ndash; Forward-looking DevDays Online API news
&ndash; Revit API news for the next major release
&ndash; Revit API news slide deck contents
&ndash; Must do
&ndash; New functionality
&ndash; Revit roadmap
&ndash; RevitLookup enhancements...

-->

### RevitLookup and DevDays Online API News

Lots of exciting information to share with you today, unprecedented!

Just as in previous years, we held a series of worldwide DevDays conference series in the last few months, including at Autodesk University in Las Vegas.

Also, as in previous years, we presented the same information online in the past couple of weeks.

For the first time ever, this year, recordings of these presentations are made publicly available before the official public product release and end of the information embargo.

I extracted the section on the Revit API and hosted it separately in order to enhance it with a table of contents.

Furthermore, we are proud to present another little update of the revamped version of RevitLookup:

- [Forward-looking DevDays Online API news](#2)
- [Revit API news for the next major release](#3)
- [Revit API news slide deck contents](#4)
    - [Must do](#5)
    - [New functionality](#6)
    - [Revit roadmap](#7)
- [RevitLookup enhancements](#8)

<center>
<img src="img/devdays_2016.png" alt="DevDays 2016" width="505"/>
</center>

#### <a name="2"></a>Forward-Looking DevDays Online API News

Here are links to the recordings of the recent DevDays Online webinar presentations, all of them hosted on
the [Autodesk DevTV YouTube channel](https://www.youtube.com/channel/UC6Fl_1mFNt0rBqa068Vaxcg):
 
- [Forge platform overview and roadmap](https://youtu.be/42g6JVTox4k?list=PL4CYqBFEfhpJ5oh_vZzqpD_8mIpFLk5yB), Feb 07, 2017
- [AutoCAD and Design Automation API updates](https://youtu.be/Pi-xhPoAEzs?list=PL4CYqBFEfhpJ5oh_vZzqpD_8mIpFLk5yB), Feb 09, 2017
- [BIM 360 API update](https://youtu.be/_BpV0D4y-U8?list=PL4CYqBFEfhpJ5oh_vZzqpD_8mIpFLk5yB), Feb 23, 2017
- [Forge API updates in detail](https://youtu.be/f4pzeGqIS0Q?list=PL4CYqBFEfhpJ5oh_vZzqpD_8mIpFLk5yB), Feb 14, 2017
- [Revit API, Civil 3D and Infraworks updates](https://youtu.be/7haXE4oOgZk?list=PL4CYqBFEfhpJ5oh_vZzqpD_8mIpFLk5yB), Feb 21, 2017
&ndash; the Revit API news begins at [12:45](https://www.youtube.com/watch?v=7haXE4oOgZk&t=765s)
- [Inventor and Fusion API updates](https://youtu.be/byZYsUJcyHc?list=PL4CYqBFEfhpJ5oh_vZzqpD_8mIpFLk5yB), Feb 16, 2017


#### <a name="3"></a>Revit API News for The Next Major Release

As said, I extracted and slightly edited the Revit API news portion of the DevDays Online recording, skipping the Civil 3D and Infraworks portion and adding a table of contents for easier navigation.

Here is the link to that, plus the supporting material:

- [Revit API News DevDays Online webinar recording](https://adn.autodesk.io/uploads/file/revit_2018_api_news/index.html) with TOC
- Slide deck: [PDF](http://thebuildingcoder.typepad.com/devday/2016/Revit_API_2018.pdf)
and on [SlideShare](https://www.slideshare.net/JeremyTammik1/revit-2018-api-news-73083190)

To learn more about Revit API basics, please refer to
the [discussion and overview of getting started material](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2﻿).

For the sake of completeness, I am also adding the slide deck contents here:


#### <a name="4"></a>Revit API News Slide Deck Contents

Disclaimer:

We may make statements regarding planned or future development efforts for our existing or new products and services. These statements are not intended to be a promise or guarantee of future availability of products, services or features but merely reflect our current plans and based on factors currently known to us.  These planned and future development efforts may change without notice.  Purchasing decisions should not be made based upon reliance on these statements.

These statements are being made as of February 2017 and we assume no obligation to update these forward-looking statements to reflect events that occur or circumstances that exist or change after the date on which they were made. If this presentation is reviewed after this date, these statements may no longer contain current or accurate information.

Three agenda sections:

- [Must Do](#5)
- [New Functionality](#6)
- [Revit Roadmap](#7)


#### <a name="5"></a>Revit API News &ndash; Must Do

- Edit in Perspective Views
  - APIs Enabled in Perspective Views
    - Modification of many object types is now allowed in perspective views
    - Most commands allowed in 3D views are now allowed in perspective views
    - Exception: Annotation & MEP tab
  - External API Commands and applications are now enabled by default in perspective views.
  - Macros, Macro Manager tools, Dynamo scripts, and the Dynamo editor are also enabled.
- Subelements
    - Subelements are a way for parts of an element to behave like real elements without the overhead of a full element
    - Subelements have typical element behaviors: Create/Delete, Select, Reference, Category, Type, Bounding Box, Geometry, Unique Id, Parameters
    - Subelement class can refer to either an Element or a specific subelement.
    - Example parent elements: Rebar, RebarContainer, FabricSheet
    - Stairs in MultistoryStairs, Railings, Continuous Rails
- Application Version
    - Return a string representing the major-minor version number of the application
    - Format MajorVersion.MinorVersion.Update, for example, "2018.0.0"
- Visual Assets
    - `Asset*` classes (e.g., `AssetProperty`) moved from `Autodesk.Revit.Utility` to `Autodesk.Revit.DB.Visual`
    - AssetPropertyType enum values renamed (integer values are unchanged)
- Direct Shapes
    - New DirectShape behaviors
    - Tagging: DirectShapes can be tagged with Revit tag tools
    - Dimensions to Edges: Referenceable DirectShapes now support dimensioning to edge references
    - Host Connectors: Referenceable DirectShapes can host connector elements in families
    - Host Rebar: Direct shapes of some categories can act as a rebar host
- Miscellaneous Changes
    - Dynamic Updaters on Reload Latest
    - Dynamic updaters are now triggered on Reload Latest for the elements added or changed in the central file.
    - Export to DWG/DXF API
    - ACADVersion::R2018 added and is the default option
    - UIDocument.PromptForFamilyInstancePlacement() now works like PickObject() methods &ndash; the placement operation will be cancelled when the "x" button of Revit is clicked
- Obsolete API Removal
    - All APIs marked as deprecated in Revit 2017 have been removed.

#### <a name="6"></a>Revit API News &ndash; New Functionality

- Reviewable Warnings
    - Document.GetWarnings() list of failure messages generated from persistent (reviewable) warnings in the document
- Family Instance References
    - FamilyInstance.GetReference*()
    - Enable access to FamilyInstance references that correspond to reference planes and lines in the family
    - Find references by name or by type
    - FamilyInstanceReferenceType &ndash; enum describing reference types available in "Is Reference" and "Reference" parameters (e.g. Strong, Weak, Left, etc.)
- Display External 3D Graphics
    - Use DirectContext3D API to display geometry in Revit by pushing directly to Revit graphics
    - Revit's rendering pipeline asks registered servers to provide the geometry for rendering
    - DirectContext3D.IDirectContext3DServer &ndash; The interface to be implemented
    - DirectContext3D.DrawContext &ndash; provides drawing functionality for use by DirectContext3D servers
    - DirectContext3D.VertexBuffer &ndash; stores vertex data for rendering
- Display External 3D Graphics
    - DirectContext3DDocumentUtils
    - Support persistence and user manipulation of streamed graphics
    - DirectContext3DHandleSettings
    - Access to Visibility/Graphics override settings applied to DirectContext3D handles
    - DirectContext3DHandleOverrides
    - Access to DirectContext3DHandleSettings stored by a view
- Dimensions
    - DimensionEqualityLabelFormatting
    - Set properties of dimension equality formulas for continuous linear or angular dimensions
    - Access via DimensionType.Get/SetEqualityFormula()
    - DimensionType.Get/SetUnitsFormatOptions()
    - Access the Unit Format for a dimension style
    - OrdinateDimensionSetting
    - Customize ordinate dimension's text position, orientation, line style, and visibility
    - Access via DimensionType.Get/SetOrdinateDimensionSetting()
- Tags
    - SpatialElementTag &ndash; base class for Room, Area, and Space tags
    - HasElbow &ndash; Identifies if the tag's leader has an elbow point or not.
    - TagText &ndash; text displayed by the tag
    - IndependentTag: Create() replaces Revit.Creation.Document.NewTag()
    - Supports elements and subelements
    - GetTaggedReference() reference to the item which has been tagged
    - IsTaggedOnSubelement() Identifies if tag is referencing a subelement
    - HasElbow Indicates if the leader on the tag has an elbow point
- Geometry API
    - Face.GetSurface() returns a copy of face's surface
    - RevolvedSurface.GetProfileCurveInWorldCoordinates() returns copy of profile curve expressed in the world coordinate system
    - RuledSurface.HasFirstProfilePoint() and RuledSurface.HasSecondProfilePoint() check if a point was used to define one of the surface profiles
- Parts
    - Access and manipulate the offset applied to a given face of a Part element
    - Part.ResetFaceOffset()
    - Part.GetFaceOffset()
- Shared Coordinates
    - SiteLocation.GeoCoordinateSystemId
    - Read-only geographic coordinate system can be acquired from DWG file
    - Returns geographic coordinate system ID, e.g. "Beijing1954/a.GK3d-40"
    - Document.Acquire/PublishCoordinates()
    - Acquire project coordinates from RVT or DWG link or publish to specified ProjectLocation
    - ProjectLocation.Create() Creates new project location from specified SiteLocation
- Links
    - IExternalResourceServer now supports CAD, DWF, and IFC links
    - CADLinkType now supports Reload() and LoadFrom() operation
    - RevitLinkType.UpdateFromIFC()
    - Allows specification of IFC file by name
    - RevitLinkInstance.Create(ImportPlacement)
    - Create new instance of a Revit link according to placement type
    - ImportInstance.Create(*) support creation of DWG or DXF instances
- DWG Export
    - ExportColorMode.TrueColorPerView
    - Colors from the Revit project will be exported as 24-bit RGB values as specified in view
    - ACADExportOptions.UseHatchBackgroundColor
    - ACADExportOptions.HatchBackgroundColor
    - Define color that will be set as hatch background color on the exported hatch
    - ExportDWGSettings.FindByName()
    - Returns the pre-defined DWG export settings
    - ExportDWGSettings.GetActivePredefinedSettings()
    - Returns the active DWG export settings
- Miscellaneous
    - Level.FindAssociatedPlanViewId() returns first-found plan view associated with specified level
    - ShapeImporter.Get/SetDefaultLengthUnit() length unit to be used during import if not specified in SAT file
- C4R Worksharing Events
    - Events.WorksharedOperationProgressChanged notifies of Collaboration for Revit's synch progress
    - Event consists of several phases with event args:
    - DocumentSaveToLocalProgessChangedEventArgs
    - DocumentReloadLatestProgressChangedEventArgs
    - DocumentSaveToCentralProgessChangedEventArgs
- Dockable Frames
    - Custom Dockable Panes now support display of dynamic UI elements (e.g. web browser controls)
    - IFrameworkElementCreator
    - New interface to support dynamic content
    - CreateFrameworkElement() constructs and returns the WPF Framework element embedded in dockable pane
    - DockablePaneProviderData.GetFrameworkElement() and FrameworkElementCreator provide ability for application to delivery a framework element ot a dockable pane
- Multi-Story Stairs
    - MultistoryStairs create stairs that span multiple levels
    - Add/RemoveStairsByLevelIds() &ndash; Adds or remove stairs to the given levels.
    - Unpin() &ndash; Enables custom modification of one story of stairs
    - Stairs.MultistoryStairsId indicates the id of the associated MultistoryStairs element
    - StairsPath.CreateOnMultistoryStairs() support creation of new stairs paths for stairs in a multistory stairs element
- Railings
    - Railing Now support hosting railings on multistory stairs
    - Get/SetMultistoryStairsPlacementLevels()
    - GetSubelementOnLevel() provide access to Railings hosted on multistory stairs
    - Create(..., multistoryStairsId, levelId,  ...) Places railing on a given level of given multistory stair
    - Create(..., stairsId,...) Now supports multistory stair as input
    - HostId Now supports stairs or stairs components from multistory stairs
- HVAC
    - HVACLoadType new base class for building type and space type
    - New properties to control air change, area per person, latent heat, lighting load, power, and heat gain settings
    - Subclasses:
    - HVACLoadSpaceType the type element of space
    - Access via MassZone.SpaceTypeId and Space.SpaceTypeId
    - HVACLoadBuildingType the type element to access building type properties
- MEP Fabrication Parts
    - FabricationPart.SplitStraight()
    - Splits the fabrication part into two at specified point
    - Detailed Fabrication additions
    - Several methods, properties classes and enumerations have been added to allow the user to access detailed fabrication information
    - Part status
    - Properties were added to fabrication part to allow the user to query and set the part fabrication status field
    - Hanger rod additions
    - Better control over hanger rod thicknesses
- Electrical
    - ElectricalSystem.Create() methods replace obsoleted APIs
    - PanelScheduleView.AddSpace()/AddSpare() add a space or spare at specific cell
    - enum ElectricalCircuitPathMode An enumerated type indicates the circuit path mode
    - ElectricalSystem new properties Control the mode, offset, and path of electrical circuit path
- MEP Analytical Connections
    - MEPAnalyticalConnection an analytical element that connects mechanical equipment to a piping network
    - GetFlow() &ndash; returns the up-to-date flow value
    - MEPAnalyticalConnectionType Type element of an MEPAnalyticalConnection
    - The type's PressureLoss value is included in the network critical path calculation
- Rebar
    - Rebar supports shape-driven and free-form layouts
    - Rebar.IsRebarFreeForm/IsRebarShapeDriven
    - Layout specific functionality in accessors:
    - Rebar.GetShapeDrivenAccessor() Replaces shape-driven only Rebar class members
    - Rebar.GetShapeDrivenAccessor()/GetFreeFormAccessor() Return shape-driven & free-form accessors respectively
    - IRebarUpdateServer Interface used to drive the generation and update of free-form geometry
- Structural Steel Sections
    - StructuralSection & and derived classes have new properties and input parameters
    - StructuralSectionUtils.GetStructuralElementDefinitionData defines the section and the position of the structural element.
    - StructuralSection.GetStructuralSectionGeneralShape general shape provides information about the geometry
    - StructuralSectionAnalysisParams defines common set of parameters for structural analysis.
    - StructuralSectionGeneral*  (e.g. StructuralSectionGeneralC) define parameter sets for specific shapes

#### <a name="7"></a>Revit API News &ndash; Revit Roadmap

- Public Revit roadmap at [www.autodesk.com/revitroadmap](http://www.autodesk.com/revitroadmap) &ndash; Last month, we launched a new platform for you to share and talk with Autodesk about the future of Revit.  Revit ideas is a forum where you can submit, vote and comment on product improvements. Already, we have about 800 ideas from customer like you and thousands of votes.   Our team is investigating a number of ideas right now.  Other products have done this for a while, and I am glad to say that Revit is now part of the party.  The product team will be there listening and responding to items as they become popular.  Help us drive the direction of Revit and get your voice heard.
- Revit Ideas at [www.autodesk.com/revitideas](http://www.autodesk.com/revitideas) &ndash; Last month, we launched a new platform for you to share and talk with Autodesk about the future of Revit.  Revit ideas is a forum where you can submit, vote and comment on product improvements. Already, we have about 800 ideas from customer like you and thousands of votes.   Our team is investigating a number of ideas right now.  Other products have done this for a while, and I am glad to say that Revit is now part of the party.  The product team will be there listening and responding to items as they become popular.  Help us drive the direction of Revit and get your voice heard.
- Revit preview &ndash; Monthly online builds and updates; Quarterly install builds
    - Access via the ADN Beta Program at [beta.autodesk.com](http://beta.autodesk.com)
    - Invitation link on product download page at [ADN extranet](http://adn.autodesk.com)
    - Last month, we launched a new platform for you to share and talk with Autodesk about the future of Revit.  Revit ideas is a forum where you can submit, vote and comment on product improvements. Already, we have about 800 ideas from customer like you and thousands of votes.   Our team is investigating a number of ideas right now.  Other products have done this for a while, and I am glad to say that Revit is now part of the party.  The product team will be there listening and responding to items as they become popular.  Help us drive the direction of Revit and get your voice heard.


#### <a name="8"></a>RevitLookup Enhancements

Finally, to round off this exciting forward-looking excursion with something down to earth right here and now, our irreplaceable Revit BIM database exploration
tool [RevitLookup](https://github.com/jeremytammik/RevitLookup) has
been significantly restructured in the past few weeks to use `Reflection` and reduce code duplication:

- [Using `Reflection` for cross-version compatibility](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-using-reflection-for-cross-version-compatibility.html)
- [Basic clean-up of the new version](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-with-reflection-cleanup.html)
- [Restore access to extensible storage data](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-extensible-storage-restored.html#3)

After adding the latter fixes related to extensible storage,
Alexander Ignatovich, [@CADBIMDeveloper](https://github.com/CADBIMDeveloper), aka Александр Игнатович,
submitted a couple of further enhancements in his pull request #27
to [restore category `BuiltInCategory`, watch properties with nullable<double> type and fix empty lists](https://github.com/jeremytammik/RevitLookup/pull/27/commits):

- Restore ability to see category `BuiltInCategory` enum value
- Add ability to see nullable double values in property, e.g., for `Dimension.Value`
- Display empty list in non-bold and prevent drilling down into
- Remove code duplication

Many thanks to Alexander for these improvements!

I integrated them 
into [RevitLookup release 2017.0.0.18](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.18).

The most up-to-date version is always provided in the master branch of 
the [RevitLookup GitHub repository](https://github.com/jeremytammik/RevitLookup).

If you would like to access any part of the functionality that was removed when switching to the `Reflection` based approach, please grab it
from [release 2017.0.0.13](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.13) or earlier.

I am also happy to restore any other code that was removed and that you would like preserved.
Simply create a pull request for that, explain your need and motivation, and I will gladly merge it back again.


