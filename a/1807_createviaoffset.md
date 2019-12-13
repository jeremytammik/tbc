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


**Question:** 

<pre class="code">
</pre>



**Answer**


<center>
<img src="img/.png" alt="" width="100"> <!--642-->
</center>

####<a name="4"></a> 

