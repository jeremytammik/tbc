<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Convert ParameterType.FixtureUnit to ForgeTypeId
  Revit 2022: ParameterType.Text to ForgeTypeId

- edge loops
  Is the first Edgeloop still the outer loop?
  https://forums.autodesk.com/t5/revit-api-forum/is-the-first-edgeloop-still-the-outer-loop/m-p/10242847
  /a/rvt/sort_multiple_edge_loops.rvt
  
$ lb is-the-first-edgeloop-still-the-outer-loop
1581_edge_refplane_column.md:2
1592_disjunct_outer_loops.md:2
1904_sort_loops.md:1

$ blmd 1581 1592
<!-- 1581 1592 --> <ul> - [Edge Loop, Point Reference Plane and Column Line](http://thebuildingcoder.typepad.com/blog/2017/08/edge-loop-point-reference-plane-and-column-line.html) == - [Disjunct Planar Face Outer Loops](http://thebuildingcoder.typepad.com/blog/2017/10/disjunct-outer-loops-from-planar-face-with-separate-parts.html) == </ul>

- Revit 2022 – Missing Fabrication Addins (temporary fix)
  https://www.darrenjyoung.com/2021/04/08/revit-2022-missing-fabrication-addins-temporary-fix/
  
twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; ...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Sorting Curve Loops

####<a name="2"></a>

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 1134 -->
</center>

<!--

Revit native project identifier

A very knowledgeable Revit MEP add-in developer, Olli Kattelus of MagiCAD Group, raises a serious question that I would appreciate some guidance on:

I have been studying how to get an unique (Guid) identifier for the project. I know there has been few posts already here and in The Building Coder blog about the subject.

But the bottom line is that I REALLY would like to have a Revit native method, mainly to avoid challenges in the work sharing environment. Below some highlights from my study:

- I have managed to confirm that using a project information element unique id is NOT an option, as several different projects can have identical id (perhaps it comes from the template...?).
- Utilizing the `ExportUtils.GetExportId()` with project information seems to result exactly the same. So probably this method uses the unique id too. Not usable.
- `ExportUtils.GetGBXMLDocumentId()` works somewhat better, and seems pretty good
- `ExporterIFCUtils.CreateProjectLevelGUID()` seems to be best though. Difference to previous is that this method results different id for project that has been detached from the central and saved with Save as (which sound reasonable).

So... I was pretty much already made my decision to use `CreateProjectLevelGUID()`... until I saw compilation warning from R2022 build. It has been deprecated :-(. "Ok, no problem" I though, as there has always been new alternative mentioned in the warning text. In this case it says:

"This function is deprecated in Revit 2022. Please see the IFC open source function `CreateProjectLevelGUID` for examples of how to do this starting in Revit 2022."

So eventually I decided to download the source codes from the "https://github.com/Autodesk/revit-ifc", as that's what it means, right!?

Moreover, I found the method `CreateProjectLevelGUID()` and took a look of the implementation. There's no native Revit mechanism I could utilize:

- Seems that the project information element dos NOT have following parameters by default (even in the R2022 level project): `IfcProjectGuid`, `BuiltInParameter.IFC_PROJECT_GUID`
- The implementation in the comments (refering also to R2022) seems incorrect to me, as a) it is utilizing the `IFCProjectLevelGUIDType` enumeration which is also deprecated. b) moreover creating an `ElementId` from the enumeration value seems quite odd as according to the IL reflection tool, the enumeration values are using default enum values (0,1,2...)

So my questions basically is:

Is there a native method/technique replacing the deprecated `CreateProjectLevelGUID()`, or, what's the best alternative (not necessarily specific to IFC)?

-->
