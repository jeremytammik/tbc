<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 7580 [Error on Saving a RVT as the C4R model]
  save as cloud model does not work in DA
  
- How to save Revit file in BIM 360 as cloud model using Forge Design Automation API?
  https://stackoverflow.com/questions/62307069/how-to-save-revit-file-in-bim-360-as-cloud-model-using-forge-design-automation-a
  Q: Thank you. I thought the only limitations of Design Automation API is using Revit UI. The method `SaveAsCloudModel()` has nothing to do with Revit UI. So, is there a place where I can see all the methods that are not supported by Design Automation API?
  A: Known restrictions are documented here:
  Restrictions for the Forge Design Automation API
  https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/restrictions
  Design Automation API for Revit
  https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/restrictions/#design-automation-api-for-revit

twitter:

Forge Design Automation for Revit or DA4R cannot save RVT as a cloud model with the #RevitAPI, and an analysis of security and privacy noting that every program launch may be logged @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/da4rsavetocloud

Today, let's address a resurfacing question in the Forge Design Automation for Revit or DA4R context
&ndash; DA4R cannot save RVT as a cloud model
&ndash; Every program launch is logged...

linkedin:

Forge Design Automation for Revit or DA4R cannot save RVT as a cloud model with the #RevitAPI, and an analysis of security and privacy noting that every program launch may be logged

https://bit.ly/da4rsavetocloud

Today, let's address a resurfacing question in the Forge Design Automation for Revit or DA4R context:

- DA4R cannot save RVT as a cloud model
- Every program launch is logged...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### DA4R Cannot Save Directly to Cloud

Today, let's address a resurfacing question in the Forge Design Automation for Revit or DA4R context and an interesting security aspect that probably applies to all PC users, more or less:

- [DA4R cannot save RVT as a cloud model](#2)
- [Every program launch is logged](#3)

####<a name="2"></a> DA4R Cannot Save RVT as a Cloud Model

As is well known and clearly documented,
[Forge Design Automation for Revit](https://forge.autodesk.com/api/design-automation-cover-page) or
[DA4R](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.55) currently
does not enable any Internet access whatsoever to the running workitem.

This obviously also prevents the ability to store an RVT BIM straight to BIM360 or C4R, Collaboration for Revit, or anywhere else in the cloud.

<!-- 
This issue came up quite a number of times already:

- [Do we have API access to C4R functionalities in Forge or Revit API?](https://stackoverflow.com/questions/58483626/do-we-have-api-access-to-c4rcollaboration-for-revit-functionalities-in-forge-a)
-->

We discussed it again in the following ticket:

**Question:** I am trying DA4R (Design Automation v3 for Revit) and would like to save a Revit file as the C4R model on BIM 360 docs.

But my DA addin raises an Exception when executing `doc.SaveAsCloudModel()`.

<pre class="code">
  LogTrace($"params.AccountGuid: {params.AccountGuid}");
  LogTrace($"params.ProjectGuid: {params.ProjectGuid}");
  LogTrace($"params.FolderId: {params.FolderId}");
  LogTrace($"params.FileName: {params.FileName}");
  
  Guid accountId = new Guid(params.AccountGuid);
  Guid projectId = new Guid(params.ProjectGuid);
  
  // Save as a RVT file as the C4R model on BIM360 docs
  
  doc.SaveAsCloudModel( accountId, projectId, params.FolderId,
    params.FileName );
</pre>

Would you tell me how to make this operation work properly?

**Answer:** Thank you for your query.

You are creating and passing in new, arbitrary GUIDs for the BIM 360 account id and project id as the first two arguments to the SaveAsCloudModel method.

This will not work. The GUIDs are pre-defined by the system.

They can be obtained as described
in [how to save a local file to a non-workshared cloud model](https://thebuildingcoder.typepad.com/blog/2020/04/revit-2021-cloud-model-api.html#4.5).

Here are some further, related discussions that I have been collecting and that may also be of interest:

- [Save RVT from cloud to local file](https://forums.autodesk.com/t5/revit-api-forum/doc-saveas-on-active-cloudmodel/m-p/9059906)
- [Accessing BIM360 Models](https://thebuildingcoder.typepad.com/blog/2018/12/forge-devcon-keynote-and-bim360-model-access.html#3)
- [How to create URN for Revit file from Revit plugin or using Forge APIs](https://stackoverflow.com/questions/53538382/how-to-create-urn-for-revit-file-from-revit-plugin-or-using-forge-apis)
- Forge blog post on [accessing BIM 360 design models on Revit](https://forge.autodesk.com/blog/accessing-bim-360-design-models-revit)
- [`IOpenFromCloudCallback` and the `DefaultOpenFromCloudCallback` class](https://thebuildingcoder.typepad.com/blog/2019/05/precast-api-and-cloud-open-callback.html#3) &ndash; using the Revit 2019 API to detach a model from BIM360
- [Accessing BIM360 cloud links](https://thebuildingcoder.typepad.com/blog/2019/06/accessing-bim360-cloud-links-thumbnail-and-dynamo.html#2)
- [Update Revit custom properties by web API](https://stackoverflow.com/questions/54788232/update-revit-custom-properties-by-web-api)
- [Open Revit BIM360 Model via command line](https://stackoverflow.com/questions/57806330/open-revit-bim360-model-via-command-line)
- [Do we have API access to C4R (Collaboration for Revit) functionalities in Forge API/Revit API?](https://stackoverflow.com/questions/58483626/do-we-have-api-access-to-c4rcollaboration-for-revit-functionalities-in-forge-a)

The last one pretty exactly matches the question you have as well.

**Response:** In my DA add-in, `SaveAsCloudCloudModel` was called with the following parameters:

<pre class="code">
  doc.SaveAsCloudModel(
    new Guid("xxxxxxxx-xxxx-xxxx-xxxx-x...x"), // BIM360 docs AccountGUID
    new Guid("xxxxxxxx-xxxx-xxxx-xxxx-x...x"), // BIM360 docs ProjectGUID
    "urn:adsk.wipprod:fs.folder:XXXXX...XXX%22, // BIM360 docs FolderID
    "test-floor-model.rvt" // FileName to save
  );
</pre>

These GUIDs and IDs were determined as described
in [how to save a local file to a non-workshared cloud model](https://thebuildingcoder.typepad.com/blog/2020/04/revit-2021-cloud-model-api.html#4.5).

However, the result was the same (log of DA WorkItem):

<pre>
  [10/08/2020 23:20:40] Autodesk.Revit.Exceptions.InvalidOperationException: Could not obtain entitlement server.
  [10/08/2020 23:20:40]    at Autodesk.Revit.DB.Document.SaveAsCloudModel(Guid accountId, Guid projectId, String folderId, String modelName)
  [10/08/2020 23:20:40]    at IfcToRvt.Commands.ImportIFC(Document doc)
  [10/08/2020 23:20:40]    at IfcToRvt.Commands.DesignAutomationBridge_DesignAutomationReadyEvent(Object sender, DesignAutomationReadyEventArgs e)
  [10/08/2020 23:20:40]    at DesignAutomationFramework.DesignAutomationBridge.RaiseDesignAutomationReadyEvent(DesignAutomationReadyEventArgs e)
  [10/08/2020 23:20:40]    at RevitCoreEngineTest.RceConsoleApplication.Program.UserMain(CommandLineArgs cl)
</pre>

Is there a mistake or misunderstanding in my code above?

[Worksharing model support by Revit Design Automation](https://forge.autodesk.com/blog/worksharing-model-support-revit-design-automation-0) clearly
states that DA can open a Worksharing model from C4R on BIM 360 docs.

Is it possible for DA to save a RVT file as the C4R model on BIM360 docs?

**Answer:** No, unfortunately that is currently not possible, as explained in [how to save Revit file in BIM 360 as cloud model using Forge Design Automation API](https://stackoverflow.com/questions/62307069/how-to-save-revit-file-in-bim-360-as-cloud-model-using-forge-design-automation-a):

> It is a current limitation of Design Automation that model saves can only be done into the current working directory of the cloud machine.
This is because user apps are forbidden from accessing the network.

> As such, `doc.SaveAsCloudModel` is not currently supported on Design Automation.
That said, we have noted your request for BIM 360 functionality in Design Automation!

I checked with my colleagues whether this is still the case.

They answered: There is no alternative to save to RCW.
Open/Save operations on RCW are on the priority (no ETA).

Thank you for clarifying!
Some further Q &amp; A on this:

[Q] What exactly does RCW stand for, please?
Is that equivalent to BIM360?
If not, what is the exact relationship between RCW, BIM360 and Forge, please? 

[A] Revit Cloud Worksharing (RCW) consists of the cloud service which sends and receives streams of element data from the cloud to your local model.
BIM360 is the Cloud service which hosts the streams of element data and enables RCW workflow.
BTW, it is suggested to use Revit Cloud Models rather than RCW.

[Q] Thank you for clarifying RCW.
Thank you also for surprising me with a new acronym, RCM.
You say that RCM is recommended over RCW.
What is RCM? Is that part of Forge, or BIM360, or yet something else?
How can we save to, or use, in general, RCM instead of RCW?
Can `SaveAsCloudModel` save to both RCM and RCW?
How can that be controlled?
This sounds to me as if a whitepaper on the whole topic and all these acronyms would be more useful than answers to individual random question.
Does such a whitepaper exist, or some source of information, or overview?

[A] Let me give it a shot, I believe this is an accurate overview:

- BIM 360 = Unified platform which hosts a number of services (such as BIM 360 Document Management)
- Revit Cloud Worksharing (RCW) = Service for Revit that supports multi-user collaboration, hosted on BIM 360 Document Management
- Cloud Models for Revit (RCM) = Use Cloud Models for Revit to save your non-workshared, local model, hosted on BIM 360 Document Management

Of these three, Design Automation for Revit can only access or save models on BIM 360.

Design Automation for Revit does not have access to and cannot sync, save, or open Revit cloud worksharing models, including Cloud Models for Revit.
These are considered the "live" versions of the model customers are actively working in.

When a customer publishes a version of the "live" model, it ends up on BIM 360, and that is the version Design Automation for Revit can access. 

[Q] Then, I assume that the answer to the main question above is a clear und succinct ‘currently no’?

[A] Correct.

So, all in all, there is currently no alternative to save to the Revit Cloud Worksharing (RCW).

Open/Save operations on RCW are high priority, but no ETA defined yet.

####<a name="3"></a> Every Program Launch is Logged

So, whereas DA4R is isolated from Internet access, you computer very probably is not, whether you wish it to be or not.

Jeffrey Paul of [EEQJ](https://eeqj.com) published an interesting analysis of computer privacy, or the lack of it, hitting a new record with the launch of the new MacOS, in his analysis and documentation on how and 
why [Your Computer Isn't Yours](https://sneak.berlin/20201112/your-computer-isnt-yours):

> ... in the current version of the macOS, the OS sends to Apple a hash of each and every program you run, when you run it
> ... using the internet, the server sees your IP, of course, and knows what time the request came in.
An IP address allows for coarse, city-level and ISP-level geolocation, and allows for a table that has the following headings:
    <pre>
    Date, Time, Computer, ISP, City, State, App_Hash</pre>
> Apple (or anyone else) can, of course, calculate these hashes for common programs: everything in the App Store, the Creative Cloud, Tor Browser, cracking or reverse engineering tools, whatever.
This means that Apple knows when you’re at home. When you’re at work. What apps you open there, and how often. They know when you open Premiere over at a friend’s house on their Wi-Fi, and they know when you open Tor Browser in a hotel on a trip to another city...
> ... you can have a fast and efficient machine, or you can have a private one.

<center>
<img src="img/transparent_tiny_house.jpg" alt="Transparent" title="Transparent" width="300"/> <!-- 600 -->
</center>
