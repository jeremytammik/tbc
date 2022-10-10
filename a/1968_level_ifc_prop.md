<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- TwentyTwo
  TwentyTwo is creating forever free Autodesk® Add-Ins which help you done more with less time and effort ... Delivers as efficient, simplest as possible applications to handle tedious tasks and complex operations.
  [NAVISWORKS ADD-INS AND API TUTORIALS](https://twentytwo.space/navisworks-add-ins-development)
  [REVIT add-in DEVELOPMENT](https://twentytwo.space/revit-add-in-development/)
  [TwentyTwo blog](https://twentytwo.space/)

- Forge and IFC by Eason Kang
  https://twitter.com/AutodeskForge/status/1567800161983676416?s=20&t=qIK9wsdnKoIEA7_oVygclw
  
- [Q] Can Revit API add custom properties in an IFC file opened in Revit? Can Revit export this IFC with those new properties?
  [A] Do you know if DGN supports changing or adding properties? Is it viable using Revit API?
  Eason Kang
  For adding custom props to IFC, there is no direct way with Revit. We need to the below to achieve that:
  Open the IFC model with Revit’s OpenIFC (API: Application.OpenIFCDocument) to convert IFC to RVT
  Add customer props by share parameters and give values (sample code from Jeremy)
  Define custom Property Sets for IFC (here is a tutorial video from 3rd-party or check this AKN page)
  Specify the custom Property Sets in IFC export setup (See userDefinedPSets in Revit IFC repo to know how to construct IFCExportOptions for API)
  Export the modified RVT to IFC (API: Document.Export)
  custom Property Sets in IFC export setup.png
  Demo-Added custom prop `FM ID` in IFC.png
  Imported IFC in Navisworks.png
  Content of my user define property set.png
  In Forge Viewer
  Screen Shot 2022-07-14 at 10.49.50 AM.png 
  ifc_custom_property_1_export_setup.png
  ifc_custom_property_2_added_fm_id.png
  ifc_custom_property_3_nw_import.png
  ifc_custom_property_4_content.png
  ifc_custom_property_5_forge.png
  Hey @Eason Kang thank you for the nice explanation on adding
 
- https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html#comment-5925189938
  xikes
  For those who are still stuck with this problem even when using the correct overload:
    public FamilyInstance NewFamilyInstance(
      XYZ location,
      FamilySymbol symbol,
      Element host,
      StructuralType structuralType
    )
  It is essential to pass in the function parameter host as a Level and not as an Element.
  Add a quick cast like (Level)MyHostElement and it should do the trick and the Level parameter is created properly and is not read-only.
  Keep in mind, this will screw up the offset values, but you can adjust those afterwards.

- set level id of existing element
  $ tbcsh_search.py level
  https://forums.autodesk.com/t5/revit-api-forum/levelid-is-null/m-p/11392692
  https://forums.autodesk.com/t5/revit-api-forum/change-level-on-line-based-family/m-p/10307454
  [Q] I'm placing a new face-based family instance into my Revit model with the help of the NewFamilyInstance method taking (Face, XYZ, XYZ, FamilySymbol).
  This works fine, except the instance does not have its level set to that of the host; it's set to -1 in the API and just left blank in the UI.
  I tried setting the level like such usng the placed instance `LevelId` property and also tried setting its `BuiltInParameter` `FAMILY_LEVEL_PARAM`.
  Both throw an error saying the parameter is read-only.
  [A] On some elements, the element level can only be set during the creation of the element. For that, I would assume that you need to use a different [overload of the `NewFamilyInstance` method](https://www.revitapidocs.com/2017/0c0d640b-7810-55e4-3c5e-cd295dede87b.htm). Please refer to this explanation by The Building Coder and a few recent discussions of related topics in the Revit API discussion forum:
  - [Change level of existing element](https://thebuildingcoder.typepad.com/blog/2020/06/creating-material-texture-and-retaining-pixels.html#4)
  - [LevelId is null](https://forums.autodesk.com/t5/revit-api-forum/levelid-is-null/m-p/11392692)
  - [Change level on line based family](https://forums.autodesk.com/t5/revit-api-forum/change-level-on-line-based-family/m-p/10307454)

 twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

###Element Level and IFC Properties 


####<a name="2"></a> 

Many thanks to , and looking forward to many future posts.

####<a name="3"></a> 

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 802 x 603 -->
</center>

