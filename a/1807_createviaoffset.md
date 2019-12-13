<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---


twitter:

 in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Dashboards and CreateViaOffset



####<a name="2"></a> Extracting Data for Project Dashboard

**Question:**

We are having a few discussions with an engineering firm who are investigating whether or not some parts of their project management procedures can be automated. Today they provide an overview using PowerBI on project progression by combining data from financials, project controlling (hours, job packages etc.) and the likes. However, progress on the content and completion of design tasks is done manually with input  from the project manager, making it difficult to track progress directly.
 
The assumption is that many of their graphic files such as .dwg contains data that describes how many documents that have been created or how many tasks have been completed. If they are able to extract these data somehow it could be build into some tool that could provide a overview. 
 
Let’s imagine they are preparing a renovation of a building and they know they have to complete a set number of PI diagrams, as part of the overall design, if they can easily extract this information from the .dwg files they are able to add this to the PowerBi datasets to be included in the overall reporting.
 
Today many projects are running smoothly due to experienced project leaders. They would like to create smooth systems to support the process as well as mitigate the risk of being able to attract senior or skilled enough people to handle the project.
 
Me and my colleagues have tried to investigate whether or not information/lists/drawing overviews from Revit and AutoCad can be exported into something like .csv .xlsx or similar user/human friendly format. Until now we have not been able to answer the questions. We have talked with 3 different people from AutoDesk chat support and read several of your blog post answers.
 
Could you somehow simplify this for me? Is it at all possible to generally export overview of the content of a dwg file in a simple format readable for other applications?
 
**Answer:**

I am not currently very involved with AutoCAD and DWG development, more with Revit RVT.
 
It is certainly both possible and easy to extract all the data you can possibly want from both of them.
 
You can do that using AutoCAD and Revit add-ins making use of the desktop .NET APIs.
 
If you are making use of any cloud-based workflows and storage, you can also do so online in the web, e.g., making use of the Forge APIs.
 
In fact, several people have already done so to create a number of different so-called dashboards that implement exactly what you are asking for.
 
Here are two in-depth demonstrations of such technology from Autodesk University 2018, a year ago:
 
https://www.autodesk.com/autodesk-university/class/Using-Forge-Revolutionize-Coordinated-Project-Information-2018
https://www.autodesk.com/autodesk-university/class/How-Use-Forge-and-BIM-360-Get-Insight-and-Improve-BIM-Management-Your-Project-2018
 
By the way, the latter also mentions PowerBI.
 
There are probably much more advanced samples available today, a year later.

Some recent [Forge Design Automation for Revit or DA4R](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.55) news
and samples:

Here is a [link to view all samples](https://forge.autodesk.com/categories/code-samples).


####<a name="3"></a>

https://autodesk.slack.com/archives/C0SR6NAP8/p1576232650000100?thread_ts=1576185616.111700&cid=C0SR6NAP8


- Room boundary polygons
  https://forums.autodesk.com/t5/revit-api-forum/room-boundary-polygons/m-p/9157379
  CreateViaOffset to offset room boundary inwards or outwards
  https://forums.autodesk.com/t5/revit-api-forum/createviaoffset/m-p/9159500
  CreateViaOffset(CurveLoop original,IList<double> offsetDists,XYZ normal)
  https://forums.autodesk.com/t5/revit-api-forum/createviaoffset-curveloop-original-ilist-lt-double-gt/m-p/9196659  

Scott Conover  13 hours ago
There's a code snippet using it in structure.  private void ModifyBentFabricSheet(Document document, FabricSheet bentFabricSheet)        {            CurveLoop newBendingProfile = CurveLoop.CreateViaOffset(bentFabricSheet.GetBendProfile(), 0.5, new XYZ(0, 0, -1));            bentFabricSheet.SetBendProfile(newBendingProfile);
            // Give the user some information            TaskDialog.Show("Revit", string.Format("Bent Fabric Sheet ID='{0}' modified successfully.", bentFabricSheet.Id.IntegerValue));        }


Scott Conover  13 hours ago
But that sample might not be too helpful.  In the customer's example, it looks like one of the walls is split in two and meet in the middle.  Perhaps the curves he has are not colinear to start?  That, it would seem, could easily result in a disconnected loop.

Dragos Turmac  3 hours ago
This is a sample from our add-in code:
         // previously filled list with simple double values that represent the distance to offset; that distance is calculated as the length of a segment that is perpendicular on the curve to be offset
         List<double> offsetArray;
         IList<Curve> polyCurves = new List<Curve>();
         foreach (var curve in contour3D.curves)
         {
            polyCurves.Add(curve.asRevitCurve(doc.Application));
         }
         if (polyCurves.none())
            return false;
         CurveLoop curveLoop = CurveLoop.Create(polyCurves);
            if (curveLoop != null)
            {
               XYZ normal = XYZ.BasisZ;
               if (curveLoop.HasPlane())
                  normal = curveLoop.GetPlane().Normal;
               curveLoop = CurveLoop.CreateViaOffset(curveLoop, offsetArray, normal);
               if (curveLoop != null)
               {
// do stuff with curve
               }
            }
It's nothing special. BUT... (edited) 

Dragos Turmac  3 hours ago
I suspect the problem is with the little vertical segment where the different width walls meet. We had problems with this method treating small segments from a structural contour, so the code above is put in a big try/catch and if it fails, we go to another offset algorithm that is far worse in general but treats small segments/self-intersections slightly better (revit's offset seems more skeptical to prolong segments when it's the case and I couldn't find code for self-intersections).
L.E. Edited testing assumptions that are invalid since offset is positive, not negative (edited) 

Dragos Turmac  1 hour ago
I have tested on our code the drawing provided as follows: the contour line is based on the exterior contour of the wall; the first curve in loop is the small one, contour being parsed clockwise; based on your/their code, the offset array is
		[0]	0.75616799999999995	double
		[1]	0.42808400000000002	double
		[2]	0.42808400000000002	double
		[3]	1.0842520000000000	double
		[4]	1.0842520000000000	double
		[5]	0.75616799999999995	double
I tried both positive and negative offsets, and the result is attached.
No exceptions, everything worked. I don't have anymore time to look into this, but I still stand on my original assumption that it has to do with the small segment, depending on the epsilon set by  add-on; instead of using wall thickness + 0.1 (which is feet, if unconverted), you may try with a fixed % from wall thickness instead (5-10%?) - it may reduce peculiar situations like teeny tiny segments after offset that can't be handled properly (edited) 
image.png
/a/doc/revit/tbc/git/a/img/createviaoffset_success.png



**Question:** 

<pre class="code">
</pre>



**Answer**


<center>
<img src="img/.png" alt="" width="100"> <!--642-->
</center>

####<a name="4"></a> 

