<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>
</head>

<!---

https://www.linkedin.com/feed/update/urn:li:activity:7206337441664749569?utm_source=share&utm_medium=member_desktop
Roman Karpovich
Revit Geometry Visualization
Introducing the new Geometry Visualization feature in RevitLookup! Now you can visualize various geometry objects directly within the interface. Enhance your BIM workflow with this powerful tool!

https://lnkd.in/dgZk9q7T

hashtag#Revit hashtag#BIM hashtag#Geometry hashtag#AEC
Activate to view larger image,
Image preview
Activate to view larger image,
likelovecelebrate
208


twitter:

 in the @AutodeskRevit #RevitAPI #BIM @DynamoBIM

...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### RevitLookup Geometry Visualisation

A small step for Roman, a giant leap for the Revit add-in developer community:


RevitLookup has been rewarded 1000 well-earned stars on GitHub.
To celebrate,
Roman [@Nice3point](https://t.me/nice3point) Karpovich, aka Роман Карпович,
presents a huge new chunk of RevitLookup functionality enabling Revit BIM element geometry visualization:

> Introducing the new Geometry Visualization feature in RevitLookup!
Now you can visualize various geometry objects directly within the interface.
Enhance your BIM workflow with this powerful tool!

<center>
<img src="img/revitlookup_geovis01.jpg" alt="RevitLookup geometry visualisation" title="RevitLookup geometry visualisation" width="600"/>
</center>

####<a name="2"></a> RevitLookup 1000 Stars on GitHub

We're proud to share that RevitLookup has achieved 1000 stars on GitHub!
This milestone is a testament to its value and the dedication of our community.
Thank you for helping us reach this landmark!

<a href="https://star-history.com/#jeremytammik/RevitLookup&Date">
    <picture>
        <source media="(prefers-color-scheme: dark)" srcset="https://api.star-history.com/svg?repos=jeremytammik/RevitLookup&type=Date&theme=dark" />
        <source media="(prefers-color-scheme: light)" srcset="https://api.star-history.com/svg?repos=jeremytammik/RevitLookup&type=Date" />
        <img alt="Star History Chart" src="https://api.star-history.com/svg?repos=jeremytammik/RevitLookup&type=Date" />
    </picture>
</a>

To celebrate it, we are excited to introduce a major new feature in this release that will transform your interaction with models, offering a deeper understanding of the geometric objects that constitute your models.

####<a name="3"></a> RevitLookup Geometry Visualization

The new visualisation functionality is described in
the [wiki documentation of RevitLookup Geometry Visualization](https://github.com/jeremytammik/RevitLookup/wiki/Visualization).
It was mainly implemented in release 2025.0.5, with further enahncements following in 2025.0.6 and 2025.0.7.

####<a name="4"></a> RevitLookup 2025.0.5

[RevitLookup 2025.0.5](https://github.com/jeremytammik/RevitLookup/releases/edit/2025.0.6) includes
comprehensive geometry visualization capabilities, enabling users to visualize various geometry objects directly within the RevitLookup interface.

In Revit, geometry is at the core of every model.
Whether you are dealing with simple shapes or intricate structures, having the ability to visualize geometric elements can significantly improve your workflow, analysis and understanding of the BIM.

To illustrate the power of these visualization capabilities, here are samples of the geometric objects you can now explore directly within RevitLookup:

| Mesh        geovis02mesh.png|
| Face        geovis03face.png|
| Solid       geovis04solid.png|
| Curve       geovis05curve.png|
| Edge        geovis06edge.png|
| BoundingBox geovis07boundingbox.png|
| XYZ         geovis08xyz.png|

For detailed documentation, check
the [wiki documentation of RevitLookup Geometry Visualization](https://github.com/jeremytammik/RevitLookup/wiki/Visualization).

Feel free to leave comments and suggestions regarding visualization in
the [pill request 245](https://github.com/jeremytammik/RevitLookup/pull/245).
Your input help improve this tool for everyone in the Revit community.

Other improvements include:

- **BoundingBoxXYZ** class support
    - Added `Bounds` method support
    - Added `MinEnabled` method support
    - Added `MaxEnabled` method support
    - Added `BoundEnabled` method support
- Added **Edit parameter** icon
- Added **Select** context menu action for Reference type
- Added **Export family size table** for FamilySizeTableManager type by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/244
- Added new extensions:

| Type           | Extension                                 | Description                                                        |
|:---------------|-------------------------------------------|--------------------------------------------------------------------|
| Application    | GetFormulaFunctions                       | Gets list of function names supported by formula engine            |
| Application    | GetFormulaOperators                       | Gets list of operator names supported by formula engine            |
| BoundingBoxXYZ | Centroid                                  | Gets the bounding box center point                                 |
| BoundingBoxXYZ | Vertices                                  | Gets list of bounding box vertices                                 |
| BoundingBoxXYZ | Volume                                    | Evaluate bounding box volume                                       |
| BoundingBoxXYZ | SurfaceArea                               | Evaluate bounding box surface area                                 |
| Document       | GetAllGlobalParameters                    | Returns all global parameters available in the given document      |
| Document       | GetLightGroupManager                      | Gets a light group manager object from the given document          |
| Document       | GetTemporaryGraphicsManager               | Gets a TemporaryGraphicsManager reference of the document          |
| Document       | GetAnalyticalToPhysicalAssociationManager | Gets a AnalyticalToPhysicalAssociationManager for this document    |
| Document       | GetFamilySizeTableManager                 | Gets a FamilySizeTableManager from a Family                        |
| UIApplication  | CurrentTheme                              | Gets a current theme                                               |
| UIApplication  | CurrentCanvasTheme                        | Gets a current canvas theme                                        |
| UIApplication  | FollowSystemColorTheme                    | Indicate if the overall theme follows operating system color theme |
| View           | GetSpatialFieldManager                    | Retrieves manager object for the given view                        |

Hope everyone enjoys the new release.
Thanks!

Made with love by [@Nice3point](https://t.me/nice3point).

####<a name="5"></a> RevitLookup 2025.0.6

[RevitLookup 2025.0.6](https://github.com/jeremytammik/RevitLookup/releases/edit/2025.0.6) implements:

- Visualization dark theme support https://github.com/jeremytammik/RevitLookup/issues/250
- [Full changelog](https://github.com/jeremytammik/RevitLookup/compare/2025.0.5...2025.0.6)

####<a name="6"></a> RevitLookup 2025.0.7

[RevitLookup 2025.0.7](https://github.com/jeremytammik/RevitLookup/releases/edit/2025.0.7) implements
solid scaling, theme synchronisation with revit and other improvements:

Solid scaling:

Visualisation now supports scaling a solid, relative to its centre. Exploring small objects is now even easier https://github.com/jeremytammik/RevitLookup/issues/251

<center>
<img src="img/geovis09solid2.png" alt="Solid scaling" title="Solid scaling" width="600"/>
</center>

Theme synchronisation with Revit:

Starting with Revit 2024, you can choose to automatically change the RevitLookup theme.
Fans of darker colors will no longer have to dig through the settings every time

<center>
<img src="img/geovis10theme.png" alt="Theme synchronisation" title="Theme synchronisation" width="600"/>
</center>

Other improvements:

- Improved arrow position for vertical edges on visualization
- Multithreading visualization support. Changing settings now does not affect rendering. Previously there were artifacts due to fast settings changes
- Added new `Element` extensions:
    - GetCuttingSolids &ndash; Gets all the solids which cut the input element
    - GetSolidsBeingCut &ndash; Get all the solids which are cut by the input element
    - IsAllowedForSolidCut &ndash; Validates that the element is eligible for a solid-solid cut
    - IsElementFromAppropriateContext &ndash; Validates that the element is from an appropriate document

####<a name="7"></a> RevitLookup Versions and Visualisation Wiki

- [RevitLookup versioning](https://github.com/jeremytammik/RevitLookup/wiki/Versions)
- [RevitLookup visualization](https://github.com/jeremytammik/RevitLookup/wiki/Visualization)

