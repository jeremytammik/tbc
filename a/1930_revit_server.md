<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- access revit server docs
  https://forums.autodesk.com/t5/revit-api-forum/how-do-i-invoke-the-revit-server-base-interface/m-p/10700292
  
- access RVT on Revit Server
  How do I invoke the Revit Server base interface?
  https://forums.autodesk.com/t5/revit-api-forum/how-do-i-invoke-the-revit-server-base-interface/m-p/10679810

- alert user of missing data
  issue a warning versus highlight graphics
  Did you fill the parameter value or is it still blank?
  https://forums.autodesk.com/t5/revit-api-forum/did-you-fill-the-parameter-value-or-is-it-still-blank/td-p/10627151

twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/formitapi

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Revit Server

####<a name="2"></a> Revit Server

Hernan Echevarria shared some valuable experience on working with Revit Server in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to invoke the Revit Server base interface](https://forums.autodesk.com/t5/revit-api-forum/how-do-i-invoke-the-revit-server-base-interface/m-p/10700292):

**Question:** The SDK provides limited information about Revit Server，i want to calling the base interface method like:download...,can you provide an example?

**Answer:** Maybe some of these can help:

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/11/au-classes-on-python-ui-server-and-framework-apis.html">AU Classes on Python, UI, Server and Framework APIs</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/08/the-revit-server-rest-api.html">The Revit Server REST API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/08/revit-server-api-access-and-vbscript.html">Revit Server API Access and VBScript</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/12/saving-a-new-central-file-to-revit-server.html">Saving a New Central File to Revit Server</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/01/rest-post-request-to-revit-server-2014.html">REST POST Request to Revit Server 2014</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/08/accessing-a-revit-server-central-model-path.html">Accessing a Revit Server Central Model Path</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/08/revit-server-thumbnail-requires-redistributable.html">Revit Server Thumbnail Requires Redistributable</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/03/pi-day-meeting-ski-tours-and-revit-server-bar-separator.html">Pi Day, Meeting, Ski Tour, Revit Server Bar Separator</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/02/revitserverapilib-truss-members-and-layers.html">Revit Server API Lib, Truss Members and Layers</a></li>
</ul>

**Response:** Thanks for your solution, your answer helped me a lot; after reading these articles，I still have a question: after getting the Revit Server filepath, how do I open the file by Revit API?

**Answewr:** I created several add-ins to manage Revit Server files.
One of them is to batch export Navisworks files from the Revit Server and the other one for exporting Revit files.

I didn't use the Revit Server API.
You can use normal Revit and Windows methods to manage the paths and open the files.

You can access the Revit Server project through Windows File Explorer.
If you don't know the path, ask your IT department.
You will probably need permission to access these folders.

In this image, you can see what it looks like (blurred bits for confidentiality):

<center>
<img src="img/revit_server_projects.png" alt="Revit Server projects" title="Revit Server projects" width="451"/> <!-- 902 -->
</center>

Note that each Revit file is represented by a folder, which ends in ".rvt"!
This is not the actual `rvt` file.
However, if you get the path of one of these folders, you can use it to open the model.

**Response:** I read all the Revit Server articles above and I have a general understanding of how Revit Server works.
As described above, I can get the Revit Server `RVT` file through Windows File Explorer.
For example, on a Revit Server "172.18.1.32", save a central file at "testfolder\1.rvt".
Now I can open the file in two ways; first:

<pre class="code">
  doc.OpenFile( @"RSN://172.18.1.32/testfolder/1.rvt" )
</pre>

Second:

- Use the method `Application.CopyModel`
- Copy the file as a local file
- Open the local file

After modifying the file, I want to synchronize with the central file.

Will I get a different result depending on which of the two way used to open the file?

<!---

**Answer:** Can you try it out and let us know how it works for you?

**Response:** When I enter the file path like this in the Revit open file dialogue: *"RSN://172.18.1.32/8364dfab-968d-4a9c-8b6b-f75d0c1b5330/04_二次沉淀池_结构.rvt"*, 
it is opened successfully.
When I try to open it using *Application.OpenDocumentFile( @"RSN://172.18.1.32/8364dfab-968d-4a9c-8b6b-f75d0c1b5330/04_二次沉淀池_结构.rvt")*,
however, it fails; Revit returns the error message "the filepath to be opened doesn't exist",the method  Application.CopyModel(),return the same message.

Now I'm very confused; can you provide an example about open revit server file?

--->

**Response:** I copy the folder from Revit Server to local, but I don't know how to open the `.rvt` project though the folder and sub file:

<center>
<img src="img/revit_server_projects_2.png" alt="Revit Server projects" title="Revit Server projects" width="495"/> <!-- 991 -->
</center>

**Answer:** In Revit, you need to open the highlighted folder:

<center>
<img src="img/revit_server_projects_3.png" alt="Revit Server projects" title="Revit Server projects" width="278"/> <!-- 476 -->
</center>

You need to pass Revit the path to that folder, not to what is inside!

So, for example, assume you have the path *Autodesk/Revit Server 2021/Projects/FOLDER.rvt*.

Just pass that path to the Revit API. I know it is weird, because it is a folder and not a Revit file!!

But this is how it works.

Also, you will need to transform the path to a Revit Server path.

So, given this path:

- *\\revit-london\Autodesk$\Revit Server 2021\Projects\2054 SingleHouseProject\Model1.rvt*

I need to transform it to:

- *RSN://REVIT-LONDON.zzz.s/Autodesk$/Revit Server 2021/Projects/2054 SingleHouseProject/Model1.rvt*

Note that I also changed slash `/` to backslash `\`.

Many thanks to Hernan for his experience and guidance!

####<a name="3"></a> 


####<a name="4"></a> 

