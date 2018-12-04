<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

#ForgeDevCon DevCon Keynote and BIM360 model access in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim http://bit.ly/devconbim360

The November Forge DevCon keynote address has been published.
Fittingly, some new hints on accessing Revit models from BIM360
&ndash; Forge DevCon 2018 keynote
&ndash; Accessing BIM360 models...

-->

### Forge DevCon Keynote and BIM360 Model Access

The November Forge DevCon keynote address has been published.

Fittingly, some new hints on accessing Revit models from BIM360:

- [Forge DevCon 2018 keynote](#2) 
- [Accessing BIM360 models](#3) 

<center>
<img src="img/jim_quanci_pacific_cup.jpg" alt="Jim Quanci" width="320">
</center>

#### <a name="2"></a> Forge DevCon 2018 Keynote

The [Forge DevCon 2018 Keynote](https://youtu.be/lNlG00ZVUyI) address from November 12, 2018, is now posted on YouTube:

<center>
<iframe width="560" height="315" src="https://www.youtube.com/embed/lNlG00ZVUyI" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

This is the full 90-minute presentation complete with slides, featuring speakers from Autodesk, namely Sam Ramji, Brian Roepke, Jim Quanci, and including special guests
Wendy Rogers, CEO of [eSUB](https://esub.com), and
Terry Davis, Director of Digital Marketing of [JELD-WEN](http://www.jeld-wen.com).

This version includes both video and slide content, as opposed to an earlier video-only version.


#### <a name="3"></a> Accessing BIM360 Models

Talking about Forge and cloud related topics, let's point out some new entries and finally an answer to 
the long-standing [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) discussion 
on [browsing model files in the cloud (A360 C4R)](https://forums.autodesk.com/t5/revit-api-forum/browsing-model-files-in-the-cloud-a360-c4r/m-p/8432797):

**Question 1:** I need to set up a project on BIM360 Document Management, so I need to upload around 50 Revit files to BIM360 Doc. As Cloud Model, not Revit File, and I cannot do it with Dynamo, Python, Revit API or BIM360 Desktop Connector. How can I set up the project file on BIM360 Document Management? 

**Question 2:** I'm also interested in automating cloud collaboration for models so that they can be linked into Cloud Workshared Models rather than having users do that manually every time they receive a model. 

**Question 3:** We are also trying to achieve this. Lacking any other API for this, we created an add-in which stores the BIM360 central path to our DB while adding files to BIM360. From our DB, we populate a grid that shows all the files which were added.

For creating a local copy, I use the following method:

<pre class="code">
  Document</span>&nbsp;OpenNewLocal(
&nbsp;&nbsp;&nbsp;&nbsp;Autodesk.Revit.ApplicationServices.<span style="color:#2b91af;">Application</span>&nbsp;app,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ModelPath</span>&nbsp;centralPath,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ModelPath</span>&nbsp;localPath&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;the&nbsp;new&nbsp;local&nbsp;at&nbsp;the&nbsp;given&nbsp;path</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">WorksharingUtils</span>.CreateNewLocal(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;centralPath,&nbsp;localPath&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;...
&nbsp;&nbsp;}
</pre>

Is this a valid approach for the current scenario?

**Question 4:** Is there any possibility to create the URN for a Revit file which is stored in BIM 360 Design? 

I have Model GUID and Project GUID in my hand.

**Answer:** The last question was also raised and answered by Eason Kang on StackOverflow, 
on [how to create URN for Revit file from Revit plugin or using Forge APIs](https://stackoverflow.com/questions/53538382/how-to-create-urn-for-revit-file-from-revit-plugin-or-using-forge-apis):

While initializing the cloud collaboration in BIM360 Design via Revit's `Collaborate` button in the `Collaborate` tab &gt; `Manage Collaboration` panel, Revit will upload your model and simultaneously publish the first version of it to the BIM360 Document Management, aka BIM360 Docs.

After completing the initialization, you can use
the [Forge](https://autodesk-forge.github.io)
[Data Management APIs](https://forge.autodesk.com/en/docs/data/v2/developers_guide/overview) to
obtain model URNs in your BIM360 project, such
as [projects/:project_id/folders/:folder_id/contents](https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-folder_id-contents-GET)
or [GET Versions](https://developer.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-items-item_id-versions-GET/?_ga=2.69623760.1996415122.1543328615-671758876.1517749558).

Afterward, iterate the items in the API response and find a version which matches the Model GUID and Project GUID you mentioned.

It will look something like this, and the `id` value is the URN you are after:

<pre class="code">
{  
   "type":"versions",
   "id":"urn:adsk.wipprod:fs.file:vf.abcd1234?version=1",
   "attributes":{  
      "name":"fileName.rvt",
      "displayName":"fileName.rvt",
      ...
      "mimeType":"application/vnd.autodesk.r360",
      "storageSize":123456,
      "fileType":"rvt",
      "extension":{  
         "type":"versions:autodesk.bim360:C4RModel",
         ....
         "data":{  
            ...
            "projectGuid":"48da72af-3aa6-4b76-866b-c11bb3d53883",
            ....
            "modelGuid":"e666fa30-9808-42f4-a05b-8cb8da576fe9",
            ....
         }
      }
   },
   ....
}
</pre>

This topic is discussed in more depth in the Forge blog post
on [accessing BIM 360 design models on Revit](https://forge.autodesk.com/blog/accessing-bim-360-design-models-revit).

