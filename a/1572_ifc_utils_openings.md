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

- 13121211 [GetInstanceCutoutFromWall Problem]
  https://forums.autodesk.com/t5/revit-api-forum/getinstancecutoutfromwall-problem/m-p/7167002
  Use ExporterIFCUtils.GetInstanceCutoutFromWall to get the outer CurveLoop of a window or a door.
  Openings must have an OpeningCut object. If not, GetInstanceCutoutFromWall will fail!
  topics: openings, gross versus net, utils

- 13124936 [AddIn Manager: How to disable copy dialog?]
  https://forums.autodesk.com/t5/revit-api-forum/addin-manager-how-to-disable-copy-dialog/m-p/7180913
  AddIn Manager issues and set copy local to false

Set the Copy Local to False for #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/ifc_utils_openings
IFC Utils return wall openings in #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/ifc_utils_openings

New important issues are researched and brilliant solutions shared daily in the Revit API discussion forum.
Here are two from the current crop
&ndash; IFC helper returns outer <code>CurveLoop</code> of door or window
&ndash; Setting <code>Copy Local</code> to <code>False</code> resolves AddIn Manager issue...

-->

### Copy Local False and IFC Utils for Wall Openings

New important issues are researched and brilliant solutions shared daily in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160).

Here are two from the current crop:

- [IFC helper returns outer `CurveLoop` of door or window](#2)
- [Setting `Copy Local` to `False` resolves AddIn Manager issue](#3)


#### <a name="2"></a>IFC Helper Returns Outer CurveLoop of Door or Window

Jan Grenov raised and solved 
his [GetInstanceCutoutFromWall problem](https://forums.autodesk.com/t5/revit-api-forum/getinstancecutoutfromwall-problem/m-p/7167002) using
the [`ExporterIFCUtils`](http://www.revitapidocs.com/2017/e0e78d67-739c-0cd6-9e3d-359e42758c93.htm)
method [`GetInstanceCutoutFromWall`](http://www.revitapidocs.com/2017/07529283-96a7-8aca-5edf-906d8ddd3b7d.htm) to
determine the outer CurveLoop of a window or a door.

The Revit API documentation states that it:

> Gets the curve loop corresponding to the hole in the wall made by the instance.

Jan determined that each opening must have an `OpeningCut` object. If not, `GetInstanceCutoutFromWall` will fail!

Here is Jan's full explanation in all its gory detail:

> **Question:** I find that the `ExporterIFCUtils` `GetInstanceCutoutFromWall` is a fine way to get the outer CurveLoop of a window or a door, but sometimes for some strange reason it does not work, and no helpful error message is supplied.
 
> I attached:

> - A [simple test project including only one wall containing two windows](zip/GetWindowCurveLoopTest.rvt)
> - [Sample code that demonstrates the problem](zip/GetWindowCurveLoop.zip)

> I hope someone can explain why `GetInstanceCutoutFromWall` works on one window but not on the other.
 
> **Answer:** Now I determined when the error occurs!

> It happens whenever an opening family (door or window) is defined without an opening cut.

> Openings must have an `OpeningCut` object. If not, `GetInstanceCutoutFromWall` will fail!

This helps explain some issues people had with this method in the past, and also fits into several existing topic groups, such as use
of [the frequently overlooked Revit API utility classes](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.52) on
one hand, determining wall openings in general, gross versus net areas and volumes in particular, on the other:

- [Opening geometry](http://thebuildingcoder.typepad.com/blog/2012/01/opening-geometry.html)
- [The temporary transaction trick for gross slab data](http://thebuildingcoder.typepad.com/blog/2012/10/the-temporary-transaction-trick-for-gross-slab-data.html)
- [Retrieving wall openings and sorting points](http://thebuildingcoder.typepad.com/blog/2015/12/retrieving-wall-openings-and-sorting-points.html)
- [Wall opening profiles](http://thebuildingcoder.typepad.com/blog/2015/12/wall-opening-profiles-and-happy-holidays.html#3)
- [Determining wall opening areas per room](http://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html#4)
- [More on wall opening areas per room](http://thebuildingcoder.typepad.com/blog/2016/04/more-on-wall-opening-areas-per-room.html)
- [Two energy model types](http://thebuildingcoder.typepad.com/blog/2017/01/family-category-and-two-energy-model-types.html#3)


#### <a name="3"></a>Setting Copy Local to False Resolves AddIn Manager Issue

Yet another solution provided by Fair59 who suggested setting the `Copy Local` flag to `false` to resolve an issue with 
the [AddIn Manager: How to disable copy dialog?](https://forums.autodesk.com/t5/revit-api-forum/addin-manager-how-to-disable-copy-dialog/m-p/7180913):

**Question:** Anytime I run a command from the AddIn Manager, a copy dialog shows up.
 
<center>
<img src="img/addin_manager_copy_dialog.png" alt="AddIn Manager copy dialogue" width="450">
</center>

How to disable it, please?

**Answer:** I've had that message a few times, when I forgot to set the `RevitAPI` and `RevitAPIUI` references' `Local copy` flag to `false`:

<center>
<img src="img/CopyLocal.png" alt="Set Copy Local flag to false" width="582">
</center>

Many thanks to Fair59 for this solution and important note in general.

We have in fact pointed out the need
to [set `Copy Local` to `false`](http://thebuildingcoder.typepad.com/blog/2011/08/set-copy-local-to-false.html) numerous
times in the past...
