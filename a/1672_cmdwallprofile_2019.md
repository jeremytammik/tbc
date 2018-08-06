<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

 in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

&ndash; 
...

--->

### Wall Profile Command Update

I returned from my time off in July and am still taking it easy in August as well.

I dived in deep into the Revit API again to help resolve an issue with The Building Coder samples external command to retrieve wall profile curves.

Here are some other notworthy things to go with that:



<center>
<img src="img/wall_profile_lines_2019_all.png" alt="Wall profile lines inner and outer loops" width="407"/>
</center>

<center>
<img src="img/exception_curve_must_be_in_plane.png" alt="Exception 'curve must be in plane'" width="503"/>
</center>



#### <a name="2"></a> CmdWallProfile Update


Eden Oo, Modeler at Tiong Seng Construction Pte Ltd, raised an issue with The Building Coder samples to retrieve wall profile loops in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread 
on a [get wall profile error]:

**Question:** I got an error testing
the [CmdWallProfile external command](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdWallProfile.cs):

<center>
<img src="img/exception_in_getWallFace.jpg" alt="Exception in getWallFace" width="685"/>
</center>

Here is a [sample model that you can use to reproduce it](zip/GetWallPf.zip).


**Answer:** 

/a/doc/revit/tbc/git/a/img/exception_curve_must_be_in_plane.png

Revit encountered a Autodesk.Revit.Exceptions.ArgumentException: Curve must be in the plane
Parameter name: pCurveCopy
   at ?A0x7b300619.newModelCurveInternal(ADocument* pADoc, Sketch* pSketch, ElementId sketchPlaneId, GCurve* pCurveCopy)
   at Autodesk.Revit.Creation.ItemFactoryBase.NewModelCurveArray(CurveArray geometryCurveArray, SketchPlane sketchPlane)
   at BuildingCoder.CmdWallProfile.Execute3(ExternalCommandData commandData, String& message, ElementSet elements) in C:\a\vs\the_building_coder_samples\BuildingCoder\BuildingCoder\CmdWallProfile.cs:line 451
   at BuildingCoder.CmdWallProfile.Execute(ExternalCommandData cd, String& msg, ElementSet els) in C:\a\vs\the_building_coder_samples\BuildingCoder\BuildingCoder\CmdWallProfile.cs:line 490
   at apiManagedExecuteCommand(AString* assemblyName, AString* className, AString* vendorDescription, MFCApp* pMFCApp, DBView* pDBView, AString* message, Set<ElementId\,std::less<ElementId>\,tnallc<ElementId> >* ids, Map<AString\,AString\,std::less<AString>\,tnallc<std::pair<AString const \,AString> > >* data, AString* exceptionName, AString* exceptionMessage)

I debugged CmdWallProfile, especialy the method Execute3 and added some debug logging messages.

The offset distance is defined as 5.

The messages tell me the following:

wall orientation (1,0,0)
face origin (5.06,24.16,0), face normal (1,0,0)
plane origin (5.06,24.82,4.92), plane normal (1,0,0)

Due to the offset distance of 5, I am surprised by the plane origin located at a Z offset of 4.92.

I replaced the plane definition using `curveLoopOffset.GetPlane` by a plane defined using the nromal and face origin instead. That solves the initial problem.

In the next wall face curve loop, however, I encounter a new issue:

Revit encountered a Autodesk.Revit.Exceptions.InvalidOperationException: Curve loop couldn't be properly trimmed.
   at Autodesk.Revit.DB.CurveLoop.CreateViaOffset(CurveLoop original, Double offsetDist, XYZ normal)
   at BuildingCoder.CmdWallProfile.Execute3(ExternalCommandData commandData, String& message, ElementSet elements) in C:\a\vs\the_building_coder_samples\BuildingCoder\BuildingCoder\CmdWallProfile.cs:line 444
   at BuildingCoder.CmdWallProfile.Execute(ExternalCommandData cd, String& msg, ElementSet els) in C:\a\vs\the_building_coder_samples\BuildingCoder\BuildingCoder\CmdWallProfile.cs:line 516
   at apiManagedExecuteCommand(AString* assemblyName, AString* className, AString* vendorDescription, MFCApp* pMFCApp, DBView* pDBView, AString* message, Set<ElementId\,std::less<ElementId>\,tnallc<ElementId> >* ids, Map<AString\,AString\,std::less<AString>\,tnallc<std::pair<AString const \,AString> > >* data, AString* exceptionName, AString* exceptionMessage)

/a/doc/revit/tbc/git/a/img/exception_curve_loop_cannot_be_trimmed.png

This is caused by the call to `CurveLoop.CreateViaOffset` in the walls than host windows.

I removed that call, since it is no longer needed, and now I can retrieve the inner and outer loops of all the walls successfully, as you can see in the screen snapshot at the top.

They are represented by model lines; the outer loops are displayed in red, and the inner ones in green.

The fixes are captured
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2019.0.143.1](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.143.1).

All the modifications I applied can be examined in
the [diff between the previous and the current version](https://github.com/jeremytammik/the_building_coder_samples/compare/2019.0.143.0...2019.0.143.1).

#### <a name="3"></a>


 

#### <a name="4"></a> 

