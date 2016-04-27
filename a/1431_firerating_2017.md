<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Real-Time BIM Update with FireRatingCloud 2017 #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim @AutodeskForge #3dwebaccel

Yesterday, I migrated RoomEditorApp to Revit 2017 and mentioned the BIM and cloud related projects I am working on.
Next, let's focus on the FireRatingCloud sample.
The main goal there is to implement fully automatic real-time BIM update from the cloud.
Now, 24 hours after writing the previous sentence, I can tell you that I succeeded.
Well, add another six hours to edit this post...
Here is the updated FireRatingCloud custom ribbon tab with its new set of commands...

-->

### Real-Time BIM Update with FireRatingCloud 2017

Yesterday,
I [migrated RoomEditorApp to Revit 2017](http://thebuildingcoder.typepad.com/blog/2016/04/room-editor-first-revit-2017-addin-migration.html#3) and
mentioned
the [BIM and cloud related projects](http://thebuildingcoder.typepad.com/blog/2016/04/room-editor-first-revit-2017-addin-migration.html#1) I am working on.

Next, let's focus on
the [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) sample.

The main goal there is to implement fully automatic real-time BIM update from the cloud.

Now, 24 hours after writing the previous sentence, I can tell you that I succeeded.

Well, add another six hours to edit this post...

And yet another six hours to struggle with Typepad, which is blocking me from publishing this...

Here is the updated FireRatingCloud custom ribbon tab with its new set of commands:

<center>
<img src="img/firerating_2017_ribbon_tab.png" alt="FireRatingCloud ribbon tab and commands in Revit 2017" width="442">
</center>

The commands are now:

- Cmd_0_About.cs &ndash; display an About... box.
- Cmd_1_CreateAndBindSharedParameter &ndash; I won't ever tell.
- Cmd_2a_ExportSharedParameterValues &ndash; upload data to cloud database using individual REST API calls for each record.
- Cmd_2b_ExportSharedParameterValuesBatch &ndash; batch upload data to cloud database using one single REST API call.
- Cmd_3_ImportSharedParameterValues &ndash; interactively download data and update BIM from cloud database.
- Cmd_4_Subscribe &ndash; subscribe to automatic real-time BIM updates.

Achieving that required the following steps:

- [Already done and yet to do](#2)
- [Migration to Revit 2017](#3)
- [Reusing the RoomEditorApp infrastructure](#4)
- [Redesign](#5)
    - [Typepad blocking source code in the blog post](#5.0)
    - [App.cs](#5.1)
    - [BimUpdater.cs](#5.2)
    - [Cmd_4_Subscribe.cs](#5.3)
    - [DbAccessor.cs](#5.4)
- [FireRatingCloud video recording](#9)
- [Download](#10)
- [To do](#11)


#### <a name="2"></a>Already Done and Yet To Do

As said, our main goal right now is the support of a round-trip data exchange with the cloud with an optional automatic real-time update of the BIM.

FireRatingCloud will require almost exactly the same functionality as our trusty old [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) sample to support that.

We can reuse a lot of the RoomEditorApp implementation, e.g., its external application managing the database polling in a separate thread, the external event to update the BIM when the polling returns modified records, the ribbon user interface for the growing number of commands, the toggle button to turn subscription on and off, etc.

Some tasks that I already addressed for this project include:

- [REST API batch upload and Windows client](http://the3dwebcoder.typepad.com/blog/2016/03/fireratingcloud-rest-api-batch-upload-and-windows-client.html) &ndash; the original FireRatingCloud sample made a separate REST API call for each door firerating shared parameter value that it exported to the cloud database. Reimporting modified values uses one single REST call for the whole batch. This enhancement exports in batch as well.
- [Database document modification timestamp](http://the3dwebcoder.typepad.com/blog/2016/04/fireratingcloud-document-modification-timestamp.html) &ndash; in order to retrieve modified database records only, a timestamp is required.
- [Retrieving updated docs](http://the3dwebcoder.typepad.com/blog/2016/04/fireratingcloud-query-retrieving-updated-docs.html) &ndash; implementation and testing of the timestamp marker and retrieval.

Now I am ready to address the main goal, almost the holy grail:

- Implement automatic round-trip real-time BIM update from the cloud

This entails a couple of subtasks:

- Implement an external event to poll the cloud database for updated records, making use of the document modification timestamp and retrieval of updated docs already implemented.
- Implement an external application to manage the external event.
- Implement a custom ribbon tab and buttons for the user interface to handle the real-time update subscription toggle button.
- Migrate to Revit 2017. I already migrated
the [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) sample and am reusing its implementation for several of the preceding items.

Once we have completed the FireRatingCloud sample, here are a few more exciting upcoming cloud related tasks:

- Document and improve [FireRatingClient](), the stand-alone Windows client &ndash; we will need this to demonstrate the real-time BIM update from arbitrary sources more nicely than the simple setup I discuss below.
- Completely rewrite the existing [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) sample to move it from CouchDB to node.js plus MongoDB.
- Implement the database portion of the [TrackChangesCloud](https://github.com/jeremytammik/TrackChangesCloud) sample.


#### <a name="3"></a>Migration to Revit 2017

Migrating the [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) to Revit 2017 yesterday was really easy.

Now let's do the same for FireRatingCloud before anything else.

As always, I need to reference the new Revit API assemblies.

For Revit 2017, I also need to switch from Visual Studio 2012 to 2015 and update the .NET framework version from 4.5 to 4.5.2.

Furthermore, FireRatingCloud references the [RestSharp library](http://restsharp.org), which provides separate assemblies for those two .NET framework versions, so we need to update those references as well.

That was it.

No code changes required, nor anything else further at all.

Here is the full solution in Visual Studio 2015 after implementing all the further enhancements described below:

<center>
<img src="img/firerating_2017_sln.png" alt="FireRatingCloud solution in Visual Studio 2015" width="276">
</center>

The initial [release 2017.0.0.0](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.0) is live in
the [FireRatingCloud GitHub repository](https://github.com/jeremytammik/FireRatingCloud),
and you can examine the flat migration changes by looking at
the [diffs](https://github.com/jeremytammik/FireRatingCloud/compare/2016.0.0.25...2017.0.0.0).

There is much more coming, though.


#### <a name="4"></a>Reusing the RoomEditorApp Infrastructure

The migration was trivial, and only a minute part of the task I am addressing.

Once that was done, I was able to integrate and reuse all the important RoomEditorApp infrastructure to implementing the external application, external event, ribbon UI, command buttons, subscription command toggle button and database polling loop in a separate thread.

That took about a day.


#### <a name="5"></a>Redesign

I spent another day cleaning up the result to make it cleaner and easier to understand.

One important cleanup step, for instance, was to separate the misleadingly named DbUpdater class into two separate classes named BimUpdater and DbAccessor.

The former implements the actual shared parameter value update in the BIM using a method that is used both by the interactive import command and the external event triggered by the database polling when it detects modification to be applied. It also implements this external event.

The latter reads records from the database, also for both the external command and the external event. It also implements the database polling loop that runs in a separate thread and raises the external event when it finds pending modification to be applied.

Can you imagine how much easier it is to understand the architecture when these two components are cleanly separated, and how confusing it is when they are bunched together into one, like they were before?

It all has historical reasons, of course.

Anyway, this sample is now hopefully much easier to comprehend than the RoomEditorApp.

Hence my aim to completely rewrite RoomEditorApp, basing it on this sample next time.

You can possibly get an idea of the various enhancement steps I made by looking at the list of intermediate releases:

- [2017.0.0.1](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.1) &ndash; implemented real-time bim update: external app, external event, ribbon ui, subscribe command and toggle button
- [2017.0.0.2](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.2) &ndash; converted timestamp to unsigned
- [2017.0.0.3](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.3) &ndash; split DbUpdater into separate DbAccessor and BimUpdater classes
- [2017.0.0.4](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.4) &ndash; moved and renamed UpdateBimFromDb to BimUpdater.UpdateBim
- [2017.0.0.5](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.5) &ndash; cleanup: all except DbAccessor and BimUpdater is now clear
- [2017.0.0.6](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.6) &ndash; rewrote UpdateBim to take list of modified doors from DbAccessor or import command
- [2017.0.0.7](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.7) &ndash; remove obsolete external commands from add-in manifest and clean up
- [2017.0.0.8](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.8) &ndash; replace Debug.Print by Util.Log and clean up using namespace statements
- [2017.0.0.9](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.9) &ndash; fixed and created recording
- [2017.0.0.10](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.10) &ndash; added readme documents for the two subprojects

FireRatingCloud now consists of the following modules:

- App.cs
- BimUpdater.cs
- Cmd_0_About.cs
- Cmd_1_CreateAndBindSharedParameter.cs
- Cmd_2a_ExportSharedParameterValues.cs
- Cmd_2b_ExportSharedParameterValuesBatch.cs
- Cmd_3_ImportSharedParameterValues.cs
- Cmd_4_Subscribe.cs
- DbAccessor.cs
- DoorData.cs
- Util.cs

The most interesting parts are the new additions, of course:

- [App.cs](#5.1) implements the external application, ribbon UI, command buttons, subscription command toggle button, and manages the external event.
- [BimUpdater.cs](#5.2) implements the external event and the method updating the Revit shared parameters.
- [Cmd_4_Subscribe.cs](#5.3) implements the new subscription external command.
- [DbAccessor.cs](#5.4) implements the database polling loop in a separate thread and raises the external event when external modifications are detected.

Let's look at them one by one.

I checked all the code comments.

They are just about as extensive as they ought to be, no more, no less, and all up to date.

So please read them as well  :-)


#### <a name="5.0"></a>Typepad Blocking Source Code in the Blog Post

Typepad blocked me from posting the four following source code sections.

Every time I tried, it triggered a message saying I have been blocked for security reasons:

> Sorry, you have been blocked. You are unable to access typepad.com. Why have I been blocked? This website is using a security service to protect itself from online attacks. The action you just performed triggered the security solution. There are several actions that could trigger this block including submitting a certain word or phrase, a SQL command or malformed data. CloudFlare Ray ID: 29a278666db82690 &ndash; Your IP: XXXXX &ndash; Performance & security by CloudFlare...

I submitted a ticket...

I experienced this once already, last month, and it took hours to resolve, wasted for both me and them. More on my side, of course. Painful.

Meanwhile, you can read the [full post](http://jeremytammik.github.io/tbc/a/1431_firerating_2017.html) in the [tbc GitHub repository](https://github.com/jeremytammik/tbc) without the Typepad support.

24 hours later and after several email exchanges and ticket submissions, they say the problem is resolved.

Let's see... yes, it works!


#### <a name="5.1"></a>App.cs

Implements the external application, ribbon UI, command buttons, subscription command toggle button, and manages the external event:

<pre class="code">
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">App</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalApplication</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Caption</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;Caption&nbsp;=&nbsp;<span style="color:#a31515;">&quot;FireRatingCloud&quot;</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Switch&nbsp;between&nbsp;subscribe&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;and&nbsp;unsubscribe&nbsp;commands.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_subscribe&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Subscribe&quot;</span>;
&nbsp;&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_unsubscribe&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Unsubscribe&quot;</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Subscription&nbsp;debugging&nbsp;benchmark&nbsp;timer.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:green;">//static&nbsp;JtTimer&nbsp;_timer&nbsp;=&nbsp;null;</span>

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Store&nbsp;the&nbsp;external&nbsp;event.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">ExternalEvent</span>&nbsp;_event&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Executing&nbsp;assembly&nbsp;namespace</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_namespace&nbsp;=&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">App</span>&nbsp;).Namespace;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Command&nbsp;name&nbsp;prefix</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_cmd_prefix&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Cmd_&quot;</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Currently&nbsp;executing&nbsp;assembly&nbsp;path</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_path&nbsp;=&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">App</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Assembly.Location;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Keep&nbsp;track&nbsp;of&nbsp;our&nbsp;ribbon&nbsp;&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;buttons&nbsp;to&nbsp;toggle&nbsp;their&nbsp;text.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">RibbonItem</span>[]&nbsp;_buttons;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Kepp&nbsp;track&nbsp;of&nbsp;subscription&nbsp;command&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;button&nbsp;whose&nbsp;text&nbsp;is&nbsp;toggled.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">int</span>&nbsp;_subscribeButtonIndex&nbsp;=&nbsp;4;

&nbsp;&nbsp;<span style="color:gray;">#region</span>&nbsp;Icon&nbsp;resource,&nbsp;bitmap&nbsp;image&nbsp;and&nbsp;ribbon&nbsp;panel&nbsp;stuff
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;path&nbsp;to&nbsp;embedded&nbsp;resource&nbsp;icon</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;IconResourcePath(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;name,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;size&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;_namespace
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.&quot;</span>&nbsp;+&nbsp;<span style="color:#a31515;">&quot;Icon&quot;</span>&nbsp;<span style="color:green;">//&nbsp;folder&nbsp;name</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.&quot;</span>&nbsp;+&nbsp;name&nbsp;+&nbsp;size&nbsp;<span style="color:green;">//&nbsp;icon&nbsp;name</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.png&quot;</span>;&nbsp;<span style="color:green;">//&nbsp;filename&nbsp;extension</span>
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Load&nbsp;a&nbsp;new&nbsp;icon&nbsp;bitmap&nbsp;from&nbsp;embedded&nbsp;resources.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;For&nbsp;the&nbsp;BitmapImage,&nbsp;make&nbsp;sure&nbsp;you&nbsp;reference&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;WindowsBase&nbsp;and&nbsp;PresentationCore,&nbsp;and&nbsp;import&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;the&nbsp;System.Windows.Media.Imaging&nbsp;namespace.&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">BitmapImage</span>&nbsp;GetBitmapImage(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Assembly</span>&nbsp;a,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;path&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;to&nbsp;read&nbsp;from&nbsp;an&nbsp;external&nbsp;file:</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//return&nbsp;new&nbsp;BitmapImage(&nbsp;new&nbsp;Uri(</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;&nbsp;Path.Combine(&nbsp;_imageFolder,&nbsp;imageName&nbsp;)&nbsp;)&nbsp;);</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>[]&nbsp;names&nbsp;=&nbsp;a.GetManifestResourceNames();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Stream</span>&nbsp;s&nbsp;=&nbsp;a.GetManifestResourceStream(&nbsp;path&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;s,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;valid&nbsp;icon&nbsp;resource&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapImage</span>&nbsp;img&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BitmapImage</span>();

&nbsp;&nbsp;&nbsp;&nbsp;img.BeginInit();
&nbsp;&nbsp;&nbsp;&nbsp;img.StreamSource&nbsp;=&nbsp;s;
&nbsp;&nbsp;&nbsp;&nbsp;img.EndInit();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;img;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Create&nbsp;our&nbsp;custom&nbsp;ribbon&nbsp;panel&nbsp;and&nbsp;populate</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;it&nbsp;with&nbsp;our&nbsp;commands,&nbsp;saving&nbsp;the&nbsp;resulting</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;ribbon&nbsp;items&nbsp;for&nbsp;later&nbsp;access.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;AddRibbonPanel(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>[]&nbsp;tooltip&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:blue;">string</span>[]&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Create&nbsp;and&nbsp;bind&nbsp;shared&nbsp;parameter&nbsp;definition.&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Export&nbsp;shared&nbsp;parameter&nbsp;values&nbsp;one&nbsp;by&nbsp;one&nbsp;creating&nbsp;new&nbsp;and&nbsp;updating&nbsp;existing&nbsp;documents.&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Export&nbsp;shared&nbsp;parameter&nbsp;values&nbsp;in&nbsp;batch&nbsp;after&nbsp;deleting&nbsp;all&nbsp;existing&nbsp;project&nbsp;documents.&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Import&nbsp;shared&nbsp;parameter&nbsp;values.&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Subscribe&nbsp;to&nbsp;or&nbsp;unsubscribe&nbsp;from&nbsp;updates.&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;About&nbsp;&quot;</span>&nbsp;+&nbsp;Caption&nbsp;+&nbsp;<span style="color:#a31515;">&quot;:&nbsp;...&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;};

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>[]&nbsp;text&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:blue;">string</span>[]&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Bind&nbsp;Shared&nbsp;Parameter&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Export&nbsp;one&nbsp;by&nbsp;one&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Export&nbsp;batch&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Import&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Subscribe&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;About...&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;};

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>[]&nbsp;classNameStem&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:blue;">string</span>[]&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;1_CreateAndBindSharedParameter&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;2a_ExportSharedParameterValues&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;2b_ExportSharedParameterValuesBatch&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;3_ImportSharedParameterValues&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;4_Subscribe&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;0_About&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;};

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>[]&nbsp;iconName&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:blue;">string</span>[]&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Knot&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;1Up&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;2Up&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;1Down&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;ZigZagRed&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Question&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;};

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;n&nbsp;=&nbsp;classNameStem.Length;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;text.Length&nbsp;==&nbsp;n
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;tooltip.Length&nbsp;==&nbsp;n
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;iconName.Length&nbsp;==&nbsp;n,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;equal&nbsp;number&nbsp;of&nbsp;text&nbsp;and&nbsp;class&nbsp;name&nbsp;entries&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;text[_subscribeButtonIndex].Equals(&nbsp;_subscribe&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Did&nbsp;you&nbsp;set&nbsp;the&nbsp;correct&nbsp;_subscribeButtonIndex?&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;_buttons&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">RibbonItem</span>[n];

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">RibbonPanel</span>&nbsp;panel
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;a.CreateRibbonPanel(&nbsp;Caption&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SplitButtonData</span>&nbsp;splitBtnData
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">SplitButtonData</span>(&nbsp;Caption,&nbsp;Caption&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SplitButton</span>&nbsp;splitBtn&nbsp;=&nbsp;panel.AddItem(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;splitBtnData&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">SplitButton</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Assembly</span>&nbsp;asm&nbsp;=&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">App</span>&nbsp;).Assembly;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">for</span>(&nbsp;<span style="color:blue;">int</span>&nbsp;i&nbsp;=&nbsp;0;&nbsp;i&nbsp;&lt;&nbsp;n;&nbsp;++i&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PushButtonData</span>&nbsp;d&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;classNameStem[i],&nbsp;text[i],&nbsp;_path,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_namespace&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.&quot;</span>&nbsp;+&nbsp;_cmd_prefix
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;classNameStem[i]&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d.ToolTip&nbsp;=&nbsp;tooltip[i];

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d.Image&nbsp;=&nbsp;GetBitmapImage(&nbsp;asm,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IconResourcePath(&nbsp;iconName[i],&nbsp;<span style="color:#a31515;">&quot;16&quot;</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d.LargeImage&nbsp;=&nbsp;GetBitmapImage(&nbsp;asm,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IconResourcePath(&nbsp;iconName[i],&nbsp;<span style="color:#a31515;">&quot;32&quot;</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d.ToolTipImage&nbsp;=&nbsp;GetBitmapImage(&nbsp;asm,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IconResourcePath(&nbsp;iconName[i],&nbsp;<span style="color:#a31515;">&quot;&quot;</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_buttons[i]&nbsp;=&nbsp;splitBtn.AddPushButton(&nbsp;d&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:gray;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Icon&nbsp;resource,&nbsp;bitmap&nbsp;image&nbsp;and&nbsp;ribbon&nbsp;panel&nbsp;stuff</span>

&nbsp;&nbsp;<span style="color:gray;">#region</span>&nbsp;External&nbsp;event&nbsp;subscription&nbsp;and&nbsp;handling
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Are&nbsp;we&nbsp;currently&nbsp;subscribed&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;to&nbsp;automatic&nbsp;cloud&nbsp;updates?</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;Subscribed
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;rc&nbsp;=&nbsp;_buttons[_subscribeButtonIndex]
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ItemText.Equals(&nbsp;_unsubscribe&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;(&nbsp;_event&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;==&nbsp;rc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;synchronised&nbsp;handler&nbsp;and&nbsp;button&nbsp;text&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;rc;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Toggle&nbsp;on&nbsp;and&nbsp;off&nbsp;subscription&nbsp;to&nbsp;automatic&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;cloud&nbsp;updates.&nbsp;Return&nbsp;true&nbsp;when&nbsp;subscribed.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;ToggleSubscription2(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IExternalEventHandler</span>&nbsp;handler&nbsp;)&nbsp;
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;Subscribed&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:#a31515;">&quot;Unsubscribing...&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_event.Dispose();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_event&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_buttons[_subscribeButtonIndex].ItemText&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;_subscribe;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//_timer.Stop();</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//_timer.Report(&nbsp;&quot;Subscription&nbsp;timing&quot;&nbsp;);</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//_timer&nbsp;=&nbsp;null;</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:#a31515;">&quot;Unsubscribed.&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:#a31515;">&quot;Subscribing...&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_event&nbsp;=&nbsp;<span style="color:#2b91af;">ExternalEvent</span>.Create(&nbsp;handler&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_buttons[_subscribeButtonIndex].ItemText&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;_unsubscribe;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//_timer&nbsp;=&nbsp;new&nbsp;JtTimer(&nbsp;&quot;Subscription&quot;&nbsp;);</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:#a31515;">&quot;Subscribed.&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;_event;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Provide&nbsp;public&nbsp;read-only&nbsp;access&nbsp;to&nbsp;external&nbsp;event.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">ExternalEvent</span>&nbsp;Event
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>&nbsp;{&nbsp;<span style="color:blue;">return</span>&nbsp;_event;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:gray;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;External&nbsp;event&nbsp;subscription&nbsp;and&nbsp;handling</span>

&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;OnStartup(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;AddRibbonPanel(&nbsp;a&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;OnShutdown(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;Subscribed&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_event.Dispose();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_event&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
}
</pre>


#### <a name="5.2"></a>BimUpdater.cs

Implements the external event and the method updating the Revit shared parameters:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;BIM&nbsp;updater,&nbsp;driven&nbsp;both&nbsp;via&nbsp;external&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;command&nbsp;and&nbsp;external&nbsp;event&nbsp;handler.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">BimUpdater</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalEventHandler</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Update&nbsp;the&nbsp;BIM&nbsp;with&nbsp;the&nbsp;given&nbsp;database&nbsp;records.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;UpdateBim(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;FireRating.<span style="color:#2b91af;">DoorData</span>&gt;&nbsp;doors,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;error_message&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Guid</span>&nbsp;paramGuid;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;!<span style="color:#2b91af;">Util</span>.GetSharedParamGuid(&nbsp;doc.Application,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">out</span>&nbsp;paramGuid&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;error_message&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Shared&nbsp;parameter&nbsp;GUID&nbsp;not&nbsp;found.&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Stopwatch</span>&nbsp;stopwatch&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Stopwatch</span>();
&nbsp;&nbsp;&nbsp;&nbsp;stopwatch.Start();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Loop&nbsp;through&nbsp;the&nbsp;doors&nbsp;and&nbsp;update&nbsp;&nbsp;&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;their&nbsp;firerating&nbsp;parameter&nbsp;values.</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;doors&nbsp;&amp;&amp;&nbsp;0&nbsp;&lt;&nbsp;doors.Count&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>&nbsp;(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Import&nbsp;Fire&nbsp;Rating&nbsp;Values&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;element&nbsp;unique&nbsp;id&nbsp;and&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;FireRating&nbsp;parameter&nbsp;values.</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>&nbsp;(&nbsp;FireRating.<span style="color:#2b91af;">DoorData</span>&nbsp;d&nbsp;<span style="color:blue;">in</span>&nbsp;doors&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;uid&nbsp;=&nbsp;d._id;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;uid&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;error_message&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Error&nbsp;retrieving&nbsp;element&nbsp;for&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;unique&nbsp;id&nbsp;{0}.&quot;</span>,&nbsp;uid&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;p&nbsp;=&nbsp;e.get_Parameter(&nbsp;paramGuid&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;p&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;error_message&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Error&nbsp;retrieving&nbsp;shared&nbsp;parameter&nbsp;on&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;element&nbsp;with&nbsp;unique&nbsp;id&nbsp;{0}.&quot;</span>,&nbsp;uid&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;fire_rating&nbsp;=&nbsp;d.firerating;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p.Set(&nbsp;(<span style="color:blue;">double</span>)&nbsp;fire_rating&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p&nbsp;=&nbsp;e.get_Parameter(&nbsp;<span style="color:#2b91af;">DoorData</span>.BipMark&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;p&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;error_message&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Error&nbsp;retrieving&nbsp;ALL_MODEL_MARK&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;built-in&nbsp;parameter&nbsp;on&nbsp;element&nbsp;with&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;unique&nbsp;id&nbsp;{0}.&quot;</span>,&nbsp;uid&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p.Set(&nbsp;(<span style="color:blue;">string</span>)&nbsp;d.tag&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;stopwatch.Stop();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;{0}&nbsp;milliseconds&nbsp;to&nbsp;import&nbsp;{1}&nbsp;element{2}.&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stopwatch.ElapsedMilliseconds,&nbsp;doors.Count,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;doors.Count)&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Execute&nbsp;method&nbsp;invoked&nbsp;by&nbsp;Revit&nbsp;via&nbsp;the&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;external&nbsp;event&nbsp;as&nbsp;a&nbsp;reaction&nbsp;to&nbsp;a&nbsp;call&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;to&nbsp;its&nbsp;Raise&nbsp;method.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Execute(&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">uint</span>&nbsp;timestamp_before_bim_update&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">Util</span>.UnixTimestamp();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;a.ActiveUIDocument.Document;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;<span style="color:#2b91af;">Util</span>.GetProjectIdentifier(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Equals(&nbsp;<span style="color:#2b91af;">DbAccessor</span>.ProjectId&nbsp;),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;same&nbsp;project&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;error_message&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;rc&nbsp;=&nbsp;UpdateBim(&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DbAccessor</span>.ModifiedDoors,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;error_message&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;rc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DbAccessor</span>.Timestamp&nbsp;=&nbsp;timestamp_before_bim_update;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">SystemException</span>(&nbsp;error_message&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Required&nbsp;IExternalEventHandler&nbsp;interface&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;method&nbsp;returning&nbsp;a&nbsp;descriptive&nbsp;name.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;GetName()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">App</span>.Caption&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;&quot;</span>&nbsp;+&nbsp;GetType().Name;
&nbsp;&nbsp;}
}
</pre>


#### <a name="5.3"></a>Cmd_4_Subscribe.cs

Implements the new subscription external command:

<pre class="code">
[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.ReadOnly&nbsp;)]
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">Cmd_4_Subscribe</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uiapp.ActiveUIDocument.Document;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Determine&nbsp;custom&nbsp;project&nbsp;identifier.</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;project_id&nbsp;=&nbsp;<span style="color:#2b91af;">Util</span>.GetProjectIdentifier(&nbsp;doc&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;!<span style="color:#2b91af;">App</span>.Subscribed&nbsp;&amp;&amp;&nbsp;0&nbsp;==&nbsp;<span style="color:#2b91af;">DbAccessor</span>.Timestamp&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DbAccessor</span>.Init(&nbsp;project_id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DbAccessor</span>.ToggleSubscription(&nbsp;uiapp&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
}
</pre>


#### <a name="5.4"></a>DbAccessor.cs

Implements the database polling loop in a separate thread and raises the external event when external modifications are detected:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Read&nbsp;records&nbsp;from&nbsp;the&nbsp;database,&nbsp;optionally&nbsp;filtering&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;according&nbsp;to&nbsp;the&nbsp;modified&nbsp;timestamp,&nbsp;and&nbsp;manage&nbsp;the&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;separate&nbsp;thread&nbsp;running&nbsp;the&nbsp;database&nbsp;polling&nbsp;loop&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;for&nbsp;the&nbsp;subscription&nbsp;command.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">DbAccessor</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Current&nbsp;document&nbsp;project&nbsp;id.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Todo:&nbsp;update&nbsp;this&nbsp;when&nbsp;switching&nbsp;Revit&nbsp;documents.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_project_id&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;the&nbsp;current&nbsp;Revit&nbsp;project&nbsp;id.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;ProjectId
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;_project_id;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;For&nbsp;subscription&nbsp;to&nbsp;automatic&nbsp;BIM&nbsp;updates,</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;retrieve&nbsp;database&nbsp;records&nbsp;modified&nbsp;after&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;this&nbsp;timestamp.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">uint</span>&nbsp;Timestamp
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">set</span>;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Initialise&nbsp;project&nbsp;id&nbsp;and&nbsp;set&nbsp;the&nbsp;timestamp&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;to&nbsp;start&nbsp;polling&nbsp;for&nbsp;database&nbsp;updates.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">uint</span>&nbsp;Init(&nbsp;<span style="color:blue;">string</span>&nbsp;project_id&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;_project_id&nbsp;=&nbsp;project_id;

&nbsp;&nbsp;&nbsp;&nbsp;Timestamp&nbsp;=&nbsp;<span style="color:#2b91af;">Util</span>.UnixTimestamp();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.InfoMsg(&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Timestamp&nbsp;set&nbsp;to&nbsp;{0}.&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\nChanges&nbsp;from&nbsp;now&nbsp;on&nbsp;will&nbsp;be&nbsp;retrieved.&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Timestamp&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;Timestamp;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Store&nbsp;the&nbsp;modified&nbsp;door&nbsp;records&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;retrieved&nbsp;from&nbsp;the&nbsp;database.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;FireRating.<span style="color:#2b91af;">DoorData</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;_modified_door_records&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;the&nbsp;current&nbsp;modified&nbsp;door&nbsp;records&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;retrieved&nbsp;from&nbsp;the&nbsp;cloud&nbsp;database.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;FireRating.<span style="color:#2b91af;">DoorData</span>&gt;&nbsp;ModifiedDoors
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;_modified_door_records;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Retrieve&nbsp;all&nbsp;door&nbsp;documents&nbsp;for&nbsp;the&nbsp;specified&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Revit&nbsp;project&nbsp;identifier,&nbsp;optionally&nbsp;filtering&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;for&nbsp;documents&nbsp;modified&nbsp;after&nbsp;the&nbsp;specified&nbsp;timestamp.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;FireRating.<span style="color:#2b91af;">DoorData</span>&gt;&nbsp;GetDoorRecords(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;project_id,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">uint</span>&nbsp;timestamp&nbsp;=&nbsp;0&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;all&nbsp;doors&nbsp;referencing&nbsp;this&nbsp;project.</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;query&nbsp;=&nbsp;<span style="color:#a31515;">&quot;doors/project/&quot;</span>&nbsp;+&nbsp;project_id;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;0&nbsp;&lt;&nbsp;timestamp&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;timestamp&nbsp;to&nbsp;query.</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Retrieving&nbsp;door&nbsp;documents&nbsp;modified&nbsp;after&nbsp;{0}&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;timestamp&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;query&nbsp;+=&nbsp;<span style="color:#a31515;">&quot;/newer/&quot;</span>&nbsp;+&nbsp;timestamp.ToString();
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Util</span>.Get(&nbsp;query&nbsp;);
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Count&nbsp;total&nbsp;number&nbsp;of&nbsp;checks&nbsp;for</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;database&nbsp;updates&nbsp;made&nbsp;so&nbsp;far.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">int</span>&nbsp;_nLoopCount&nbsp;=&nbsp;0;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Count&nbsp;total&nbsp;number&nbsp;of&nbsp;checks&nbsp;for</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;database&nbsp;updates&nbsp;made&nbsp;so&nbsp;far.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">int</span>&nbsp;_nCheckCount&nbsp;=&nbsp;0;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Count&nbsp;total&nbsp;number&nbsp;of&nbsp;database&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;updates&nbsp;requested&nbsp;so&nbsp;far.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">int</span>&nbsp;_nUpdatesRequested&nbsp;=&nbsp;0;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Number&nbsp;of&nbsp;milliseconds&nbsp;to&nbsp;wait&nbsp;and&nbsp;relinquish</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;CPU&nbsp;control&nbsp;before&nbsp;next&nbsp;check&nbsp;for&nbsp;pending</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;database&nbsp;updates.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">int</span>&nbsp;_timeout&nbsp;=&nbsp;500;

&nbsp;&nbsp;<span style="color:gray;">#region</span>&nbsp;Windows&nbsp;API&nbsp;DLL&nbsp;Imports
&nbsp;&nbsp;<span style="color:green;">//&nbsp;DLL&nbsp;imports&nbsp;from&nbsp;user32.dll&nbsp;to&nbsp;set&nbsp;focus&nbsp;to</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Revit&nbsp;to&nbsp;force&nbsp;it&nbsp;to&nbsp;forward&nbsp;the&nbsp;external&nbsp;event</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Raise&nbsp;to&nbsp;actually&nbsp;call&nbsp;the&nbsp;external&nbsp;event&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Execute.</span>

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;The&nbsp;GetForegroundWindow&nbsp;function&nbsp;returns&nbsp;a&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;handle&nbsp;to&nbsp;the&nbsp;foreground&nbsp;window.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;[<span style="color:#2b91af;">DllImport</span>(&nbsp;<span style="color:#a31515;">&quot;user32.dll&quot;</span>&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;GetForegroundWindow();

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Move&nbsp;the&nbsp;window&nbsp;associated&nbsp;with&nbsp;the&nbsp;passed&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;handle&nbsp;to&nbsp;the&nbsp;front.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;[<span style="color:#2b91af;">DllImport</span>(&nbsp;<span style="color:#a31515;">&quot;user32.dll&quot;</span>&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;SetForegroundWindow(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;hWnd&nbsp;);
&nbsp;&nbsp;<span style="color:gray;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Windows&nbsp;API&nbsp;DLL&nbsp;Imports</span>

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;This&nbsp;method&nbsp;runs&nbsp;in&nbsp;a&nbsp;separate&nbsp;thread&nbsp;and</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;continuously&nbsp;polls&nbsp;the&nbsp;database&nbsp;for&nbsp;modified</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;records.&nbsp;If&nbsp;any&nbsp;are&nbsp;detected,&nbsp;raise&nbsp;an&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;external&nbsp;event&nbsp;to&nbsp;update&nbsp;the&nbsp;BIM.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Relinquish&nbsp;control&nbsp;and&nbsp;wait&nbsp;for&nbsp;the&nbsp;specified</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;timeout&nbsp;period&nbsp;between&nbsp;each&nbsp;attempt.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;CheckForPendingDatabaseChanges()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">while</span>&nbsp;(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;<span style="color:#2b91af;">App</span>.Event&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;++_nLoopCount;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;<span style="color:#2b91af;">App</span>.Event.IsPending&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;CheckForPendingDatabaseChanges&nbsp;loop&nbsp;{0}&nbsp;-&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;database&nbsp;update&nbsp;event&nbsp;is&nbsp;pending&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_nLoopCount&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//using(&nbsp;JtTimer&nbsp;pt&nbsp;=&nbsp;new&nbsp;JtTimer(</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;&nbsp;&quot;CheckForPendingDatabaseChanges&quot;&nbsp;)&nbsp;)</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;++_nCheckCount;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;CheckForPendingDatabaseChanges&nbsp;loop&nbsp;{0}&nbsp;-&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;check&nbsp;for&nbsp;changes&nbsp;{1}&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_nLoopCount,&nbsp;_nCheckCount&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_modified_door_records&nbsp;=&nbsp;GetDoorRecords(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_project_id,&nbsp;Timestamp&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;_modified_door_records&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;0&nbsp;&lt;&nbsp;_modified_door_records.Count&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">App</span>.Event.Raise();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;++_nUpdatesRequested;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;database&nbsp;update&nbsp;pending&nbsp;event&nbsp;raised&nbsp;{0}&nbsp;times&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_nUpdatesRequested&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;focus&nbsp;to&nbsp;Revit&nbsp;for&nbsp;a&nbsp;moment.</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Otherwise,&nbsp;it&nbsp;may&nbsp;take&nbsp;a&nbsp;while&nbsp;before&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Revit&nbsp;reacts&nbsp;to&nbsp;the&nbsp;raised&nbsp;event&nbsp;and</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;actually&nbsp;calls&nbsp;the&nbsp;event&nbsp;handler&nbsp;Execute&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;method.</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;hBefore&nbsp;=&nbsp;GetForegroundWindow();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetForegroundWindow(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ComponentManager</span>.ApplicationWindow&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetForegroundWindow(&nbsp;hBefore&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Wait&nbsp;and&nbsp;relinquish&nbsp;control&nbsp;before</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;next&nbsp;check&nbsp;for&nbsp;pending&nbsp;database&nbsp;updates.</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Thread</span>.Sleep(&nbsp;_timeout&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Separate&nbsp;thread&nbsp;running&nbsp;the&nbsp;loop</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;polling&nbsp;for&nbsp;pending&nbsp;database&nbsp;changes.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Thread</span>&nbsp;_thread&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Toggle&nbsp;subscription&nbsp;to&nbsp;automatic&nbsp;database&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;updates.&nbsp;Forward&nbsp;the&nbsp;call&nbsp;to&nbsp;the&nbsp;external&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;application&nbsp;that&nbsp;creates&nbsp;the&nbsp;external&nbsp;event,</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;store&nbsp;it&nbsp;and&nbsp;launch&nbsp;a&nbsp;separate&nbsp;thread&nbsp;checking&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;for&nbsp;database&nbsp;updates.&nbsp;When&nbsp;changes&nbsp;are&nbsp;pending,</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;invoke&nbsp;the&nbsp;external&nbsp;event&nbsp;Raise&nbsp;method.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;ToggleSubscription(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Todo:&nbsp;stop&nbsp;thread&nbsp;first!</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;<span style="color:#2b91af;">App</span>.ToggleSubscription2(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BimUpdater</span>()&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Start&nbsp;a&nbsp;new&nbsp;thread&nbsp;to&nbsp;regularly&nbsp;check&nbsp;the</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;database&nbsp;status&nbsp;and&nbsp;raise&nbsp;the&nbsp;external&nbsp;event</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;when&nbsp;updates&nbsp;are&nbsp;pending.</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_thread&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Thread</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CheckForPendingDatabaseChanges&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_thread.Start();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_thread.Abort();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_thread&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>


#### <a name="9"></a>FireRatingCloud Video Recording

Here is an eight and a half minute recording demonstrating the final result, [FireRatingCloud up and running in Revit 2017](https://youtu.be/8VsQJkikXbA):

<center>
<iframe width="420" height="315" src="https://www.youtube.com/embed/8VsQJkikXbA?rel=0" frameborder="0" allowfullscreen></iframe>
</center>


#### <a name="10"></a>Download

The version presented above
is [release 2017.0.0.12](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.12),
available from the [FireRatingCloud GitHub repository](https://github.com/jeremytammik/FireRatingCloud).


#### <a name="11"></a>To Do

As already mentioned above, I have several more exciting tasks lined up, all related to connecting BIM and the cloud:

- Document and improve [FireRatingClient](https://github.com/jeremytammik/FireRatingCloud/tree/master/FireRatingClient),
the stand-alone Windows client &ndash; we will need this to demonstrate the real-time BIM update from arbitrary sources more elegantly than I did above using the minimalistic and rudimentary mongolab web site.
- Completely rewrite the existing [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) sample to move it from CouchDB to node.js plus MongoDB.
- Implement the database portion of the [TrackChangesCloud](https://github.com/jeremytammik/TrackChangesCloud) sample.

Stay tuned and wish me luck.

And lots of extra time &ndash; I could use a lot more than 24 hours per day, man.

Well, who couldn't?
