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
  Jan Grenov
  https://forums.autodesk.com/t5/revit-api-forum/getinstancecutoutfromwall-problem/m-p/7167002
  Use ExporterIFCUtils.GetInstanceCutoutFromWall to get the outer CurveLoop of a window or a door.
  Openings must have an OpeningCut object. If not, GetInstanceCutoutFromWall will fail!
  topics: openings, gross versus net, utils

- 13124936 [AddIn Manager: How to disable copy dialog?]
  https://forums.autodesk.com/t5/revit-api-forum/addin-manager-how-to-disable-copy-dialog/m-p/7180913
  AddIn Manager issues and set copy local to false

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

...

-->

### Copy Local False and IFC Utils for Wall Openings

New brillinat solutions are researched and found daily in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160).

Here are two from the current crop:

#### <a name="2"></a>IFC Utils Returns Outer CurveLoop of Door or Window

Jan Grenov raised and solved 
his [GetInstanceCutoutFromWall problem](https://forums.autodesk.com/t5/revit-api-forum/getinstancecutoutfromwall-problem/m-p/7167002) using
the [`ExporterIFCUtils`](http://www.revitapidocs.com/2017/e0e78d67-739c-0cd6-9e3d-359e42758c93.htm)
method [`GetInstanceCutoutFromWall`](http://www.revitapidocs.com/2017/07529283-96a7-8aca-5edf-906d8ddd3b7d.htm) to
determine the outer CurveLoop of a window or a door.

The documenation states that it

> Gets the curve loop corresponding to the hole in the wall made by the instance.

Jan determined that each opening must have an `OpeningCut` object. If not, `GetInstanceCutoutFromWall` will fail!

This ties in with several groups of topics discussed in the past, such
as [the frequently overlooked Revit API utility classes](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.52)
determining wall openings in general, gross versus net areas and volumes in particular.

- [Opening geometry](http://thebuildingcoder.typepad.com/blog/2012/01/opening-geometry.html)
- [The temporary transaction trick for gross slab data](http://thebuildingcoder.typepad.com/blog/2012/10/the-temporary-transaction-trick-for-gross-slab-data.html)
- [Retrieving wall openings and sorting points](http://thebuildingcoder.typepad.com/blog/2015/12/retrieving-wall-openings-and-sorting-points.html)
- [Wall opening profiles](http://thebuildingcoder.typepad.com/blog/2015/12/wall-opening-profiles-and-happy-holidays.html)
- [Determining wall opening areas per room](http://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html)
- [More on wall opening areas per room](http://thebuildingcoder.typepad.com/blog/2016/04/more-on-wall-opening-areas-per-room.html)
- [Two energy model types](http://thebuildingcoder.typepad.com/blog/2017/01/family-category-and-two-energy-model-types.html#3)


#### <a name="3"></a>Setting Copy Local to False Resolves AddIn Manager Issue

Yet another solution provided by Fair59 who suggested setting the `Copy Local` flag to `false` to resolve an issue with 
the [AddIn Manager: How to disable copy dialog?](https://forums.autodesk.com/t5/revit-api-forum/addin-manager-how-to-disable-copy-dialog/m-p/7180913):



<center>
<img src="img/" alt="" width="400">
</center>



**Question:**

<center>
<img src="img/.png" alt="" width="1386">
</center>

<pre class="code">
</pre>

**Answer:** 


**Response:** 
 
Thank you FAIR59 ;)

