<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- Re: ExternalEvent
  https://forums.autodesk.com/t5/revit-api-forum/externalevent/m-p/9029731
  [Q] Hi! I want to create ExternalEvent on button click. But I get exception: "Attempting to create an ExternalEvent outside of a standard API execution". When I checked the thread of the main window and the Command class, they were the same thread. What's the magic? I created the class for reseting event in Command class.
  [A] Look at the Revit SDK sample ModelessDialog/ModelessForm_ExternalEvent.
  The Building Coder also shares lots of samples implementing external events:
  https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28
  Sean Page adds a helpful pointer, saying:
  I found this site to be exceptionally helpful when creating external event handlers.
  https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/Revit-API/files/GUID-0A0D656E-5C44-49E8-A891-6C29F88E35C0-htm.html
  https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/Revit-API/files/GUID-0A0D656E-5C44-49E8-A891-6C29F88E35C0-htm.html
  Very strangely, I can no longer find the Revit 2016 knowledgebase article Sean points out in the Revit 2020 knowledgebase.
  Unless someone can tell me where an up-to-date link for the current product version can be found, it might make sense to copy this to the blog or somewhere to preserve it for posterity.
  BobbyC.Jones adds:
  You cannot call ExternalEvent.Create() outside of a valid Revit API context, and a callback from a button click is most definitely not a valid context.
  What i do is create an instance of the IExternalEventHandler and call ExternalEvent.Create() from the IExternalCommand, or other valid context, and pass them to the viewmodel (or create them in the ViewModel contstructor if you're newing up the ViewModel in the IExternalCommand.  I prefer MVVM, if you do not then pass them to or create them where you're UI logic resides, a Controller, or directly in the Form, or wherever.  Then in your button click callback you pass necessary state info to your IExternalEventHandler and then call Raise().

- [trouble reading entity schema]
  https://forums.autodesk.com/t5/revit-api-forum/trouble-reading-entity-schema/m-p/9046186
[Q] I seem to be having some trouble getting access to the entity schema I created.
About 7 years ago, I created an app that reads & writes a schema to the project info element. A year later I created another app that only reads this schema. Now I am updating the second app and I find with Revit 2019 it can no longer read the schema. I keep getting the error "Schema not available to this application" but in the schema builder the read\write access is set to vendor. The name, guid and vendor id are set the same in both apps. I can still get the schema with the latest version of the app that created it, it's just the second app that can't.
Is this a bug in the most recent API's?
Later: Never mind. I figured it out.
I had loaded my app through the wonderful add-in manager you created. So the schema thought it was the add-in Manager that was trying to access my schema. Everything works properly if I load my app with the manifest file without ever having loaded the app through the manager.
[A] by @cwaluga Christian Waluga
You can try to change the vendor/addin identification in the AddInManager .addin file to yours. I do this with Revit Lookup to snoop my extensible storage.

- https://forums.autodesk.com/t5/revit-api-forum/get-sides-of-cylinder-element/td-p/9044459

- System.Data.Sqlite cannot be loaded
  https://forums.autodesk.com/t5/revit-api-forum/syste-data-sqlite-can-not-loaded/m-p/9039972
  [Q] I am writing an app that use sqlite to write to db and I make a refrence to System.Data.Sqlite but when I run the app inside Revit I got the error message "Could not load file or assembly or one of its dependencies"
  What can be the cause of that error? Thank you
  [A by Revitalizer] Autodesk itself uses a bunch of .NET DLL files in its own add-ins that are shipped with Revit.
  Since they are loaded before your add-in, there may be conflicts if different versions are used.
  So, make sure you use the same dll versions as Autodesk uses.
  This applies to all the DLLs you see in the Revit.exe directory, e.g., Sqlite, Log4Net, ...

twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Extensible Storage, External Event, DLL and Pipe Face

Presenving another couple of interesting recent threads from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160):


<center>
<img src="img/herbstbild.jpg" alt="Autumn" width="605">
</center>

####<a name="2"></a> 

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread


**Question:** 

**Answer:** 

<pre class="code">
</pre>


####<a name="3"></a> 

####<a name="4"></a> 

Many thanks to  for sharing this!

