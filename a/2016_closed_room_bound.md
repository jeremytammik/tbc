<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Boost Your BIM by Harry Mattison
  Journal File Magic & Exporting Groups to File (the grand finale)
  https://boostyourbim.wordpress.com/2023/11/03/journal-file-magic-exporting-groups-to-file-the-grand-finale/
  Journal File Magic & Exporting Groups to File (part 2)
  https://boostyourbim.wordpress.com/2023/11/02/journal-file-magic-exporting-groups-to-file-part-2/
  Journal File Magic & Exporting Groups to File (part 1)
  https://boostyourbim.wordpress.com/2023/11/01/journal-file-magic-exporting-groups-to-file-part-1/


- Revit server buffer size settings
  Synchronizing Revit Central Models between Offices over Internet-VPN
  https://forums.autodesk.com/t5/revit-api-forum/synchronizing-revit-central-models-between-offices-over-internet/td-p/12313273

- https://stackoverflow.com/questions/77243537/revit-api-try-to-get-all-the-elements-from-revit-file

- replacing [Revit 2024 'Other' Parameter Group] or BuiltInParameterGroup.INVALID
  https://forums.autodesk.com/t5/revit-api-forum/revit-2024-other-parameter-group/td-p/12086226
  Kevin Fielding
  Revit 2024 'Other' Parameter Group
  With the change over to ForgeTypeId and GroupTypeId in Revit 2024 instead of BuiltInParameterGroup enumerations, I just wanted to share how to define the 'Other' group for parameters as it doesn't appear to be documented.
  Whereas previously you would use
  BuiltInParameterGroup.INVALID
  In 2024 and beyond you need to use
  new ForgeTypeId(string.Empty)
  Other groups can be found using the GroupTypeId class like GroupTypeId.Data
  Hope this helps others searching for this.
  came up again in
  Revit 2024 GroupTypeId missing ParameterGroup Other (Invalid)
  https://forums.autodesk.com/t5/revit-api-forum/revit-2024-grouptypeid-missing-parametergroup-other-invalid/m-p/12288651/highlight/false#M74502
  thanks to kevin

twitter:

 with the @AutodeskAPS @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Closed Room Boundary


####<a name="2"></a>

####<a name="3"></a> Closed Contiguous Room Boundary Loop

get closed contiguous boundary loop from room:
using GetRoomBoundaryAsCurveLoopArray from the ExporterIFC module
Creating a Generic Model from Area Boundaries
https://forums.autodesk.com/t5/revit-api-forum/creating-a-generic-model-from-area-boundaries/m-p/12371317#M75201
Loren Routh
San Francisco, California, US
SDM Specialist
GSA
Building Owners
https://www.gsa.gov/about-us/gsa-regions/region-9-pacific-rim/buildings-and-facilities/california
https://www.gsa.gov/

> This just in:
I tried the `GetRoomBoundaryAsCurveLoopArray` method, and it totally worked!
You need to import the `ExporterIFC` module, etc.
This method eliminated a chunk of code, no sorting or extracting the curves.
It let me create an extrusion (manually) with no errors at all!
As you can see by the pic below, this was not a rectangle.
Definitely has my vote to be included in the regular Revit API.
Now to make it work with Generic Models...

<pre class="prettyprint">
  import clr
  clr.AddReferenceToFileAndPath(r'C:\Program Files\Autodesk\Revit 2023\AddIns\IFCExporterUI\Autodesk.IFC.Export.UI.dll')
  clr.AddReference("RevitAPIIFC")
  from Autodesk.Revit.DB.IFC import ExporterIFC
  from Autodesk.Revit.DB.IFC import ExporterIFCUtils

  opt = DB.SpatialElementBoundaryOptions()

  curve_loop = ExporterIFCUtils.GetRoomBoundaryAsCurveLoopArray(selected_area, opt, True)

  with DB.Transaction(doc, "Create Model Lines") as tx:
    tx.Start()

    sketch_plane = DB.SketchPlane.Create(doc,selected_area.LevelId)

    for loop in curve_loop:
      for line in loop:
        crv = doc_create.NewModelCurve(line, sketch_plane)

    tx.Commit()
</pre>

<center>
<img src="img/getroomboundaryascurvelooparray.png" alt="GetRoomBoundaryAsCurveLoopArray" title="GetRoomBoundaryAsCurveLoopArray" width="500"/> <!-- Pixel Height: 1,278 Pixel Width: 1,590 -->
</center>

Many thanks to Loren for sharing this valuable hint.

####<a name="4"></a> AI May Obsolete All Apps

Bill Gates present an interesting vision of the future of personal computing
in [The future of agents &ndash; AI is about to completely change how you use computers &ndash; and upend the software industry](https://www.gatesnotes.com/AI-agents).



