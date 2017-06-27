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

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

Here is another brilliant and super succinct solution provided by Fair59, answering the Revit API discussion forum thread on how to get cutting void instances in the floor using the <code>HostObject</code> <code>FindInserts</code> method &ndash; Question: I have a floor on which a family instance is inserted on the face of the floor (the instance host is also the floor). I checked in the family the "Cut with Void When Loaded" parameter, so that the void is created in the floor. Now, I want to retrieve all the instances that create voids in the floor...

-->

### Retrieve Openings in Wall with IFC Utils and Set Copy Local to False


<center>
<img src="img/" alt="" width="400">
</center>

Before that, let me share another brilliant and super succinct solution provided by Fair59, answering
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread on how


**Question:**

<center>
<img src="img/.png" alt="" width="1386">
</center>

<pre class="code">
</pre>

**Answer:** 


**Response:** 
 
Thank you FAIR59 ;)

