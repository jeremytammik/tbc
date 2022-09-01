<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- pick point in cloud point
  https://forums.autodesk.com/t5/revit-api-forum/definition-of-work-plane-for-picking-point-of-point-cloud-in/td-p/11366329

- rpthomas explains
  https://forums.autodesk.com/t5/revit-api-forum/access-point-cloud-points-through-api/m-p/11374437#M65416

- 8125 [Carla Ferreyra: Link Point Cloud with BIM] 
  open source point cloud engine https://github.com/potree/potree
  Here are two point cloud demos with the Forge viewer using potree.js
  by [Petr Broz](https://github.com/petrbroz) and [Michael Beale](https://github.com/wallabyway):
  - [Pointcloud with Revit](https://github.com/wallabyway/forge-pointcloud-with-revit)
  - [forge-potree-demo](https://github.com/petrbroz/forge-potree-demo)
  According to Michael’s slide deck, you need to convert your point cloud files to the potree supported format via `PotreeConverter` before using the potree viewer extension. A `.pts` file requires conversion to `.las`, `.zlas`, or `.bin` file before passing to `PotreeConverter`.

- https://forums.autodesk.com/t5/revit-api-forum/highlight-100-000-nodes-xyz-coordinates-at-once-in-revit/m-p/9349258
  search todo_tbc.txt for 'point cloud api'

- https://forums.autodesk.com/t5/revit-api-forum/sdk-sample-pointcloudengine-crash-for-revit-2018-and-2019/m-p/8580174

twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; ...

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

###

####<a name="2"></a>

**Question:** 

**Answer:** 

**Response:** 

<center>
<img src="img/.png" alt="" title="" width="600"/> <!-- 960 x 540 -->
</center>

####<a name="3"></a> Pick and Access Point Cloud Points

Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
solved two tricky point cloud related questions in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) that
I earmarked for editing and republishing here.

The latest explains how to address 
the [definition of work plane for picking point of point cloud in orthographic 3D view](https://forums.autodesk.com/t5/revit-api-forum/definition-of-work-plane-for-picking-point-of-point-cloud-in/td-p/11366329).
In summary, Richard recommends using the `Selection.PickObject` method specifying `ObjectType.PointOnElement` and shares code to:

-  Get the minimum point of the UIView from UIView.GetZoomCorners
-  Set up a work plane using view direction and min point from (1)*
-  Pick the point on the plane
-  Filter the cloud by creating a box aligned with the view direction around the picked point
-  Transform the filtered cloud points to model space and project them onto plane and find nearest to original picked point.
 
There is a lot more to it than it sounds, though...

Before that, he also addressed
how to [access point cloud points through API](https://forums.autodesk.com/t5/revit-api-forum/access-point-cloud-points-through-api/m-p/11374437):

**Question:** I imported an `.rcs` point cloud to Revit 2022 using Insert &gt; Point Cloud.
I would like to access its points and colors through Revit API.
For that, I found the `PointCloudInstance.GetPoints` method.
Its third input is `numPoints`, representing the number of points which should be extracted from the point cloud.
However, this input cannot be larger than 1 million.
And if I try to make it larger, it raises an error message: 'It can be from 1 to 1000000.'
Does this mean that for any point cloud we import to Revit, we can only access its first 1 million points through Revit API, and those beyond are inaccessible?

**Answer:** I think the statement regarding how many points you can access is incorrect.

Yes, there is a limit, but you can process the cloud in blocks by adjusting the filter limits and thus access all the points, not just the limit of one block.

A confusing aspect is the `averageDistance`:

> Desired average distance between "adjacent" cloud points (Revit units of length). The smaller the averageDistance the larger number of points will be returned up to the numPoints limit. Specifying this parameter makes actual number of points returned for a given filter independent of the density of coverage produced by the scanner.

What is the lowest number I can set this to, the limit of the scanner or smallest possible +ve double value? I think it should be an optional parameter whereby it just returns all the points for an area up to the max number of points limit. It says: "Specifying this parameter...", making it sound like it is optional, but it doesn't tell me it can be null for example.

I would probably split the cloud up into blocks and test the number of points has not reached the limit for each.
If it has reached the limit for a given block then subdivide that block up into more blocks.

Note:

> If there are more points in the cloud passing the filter than the number requested in this function, the results may not be consistent if the same call is made again.

So, if the amount of points is less then the max number then I assume you have all the points for that block.

Additionally the `PointCollection` has the member `GetpointBufferPointer`; this could then provides a faster way to count the points in each block to establish if the block needs further subdividing.

Many thanks to Richard for his helpful advice and great expertise.
