<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Real-Time BIM Update via FireRatingCloud Windows Client #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim @AutodeskForge #3dwebaccel

Today, I address the first item in yesterday's to do list
&ndash; Document and improve FireRatingClient, the stand-alone Windows client &ndash; we will need this to demonstrate the real-time BIM update from arbitrary sources
&ndash; Context
&ndash; FireRatingClient
&ndash; Adding the <code>modified</code> Field
&ndash; Updating the <code>modified</code> Field on Edit
&ndash; FireRatingClient Live BIM Update Demo Recording
&ndash; Download
&ndash; To Do...

-->

### Real-Time BIM Update via FireRatingCloud Windows Client

Today, I address the first item in [the to do list that I published yesterday](http://thebuildingcoder.typepad.com/blog/2016/04/real-time-bim-update-with-fireratingcloud-2017.html#11):

- Document and improve [FireRatingClient](https://github.com/jeremytammik/FireRatingCloud/tree/master/FireRatingClient),
the stand-alone Windows client &ndash; we will need this to demonstrate the real-time BIM update from arbitrary sources.

Let's look at this task in context and then address it right away:

- [Context](#2)
- [FireRatingClient](#3)
- [Adding the `modified` Field](#4)
- [Updating the `modified` Field on Edit](#5)
- [FireRatingClient Live BIM Update Demo Recording](#6)
- [Download](#7)
- [To Do](#8)


#### <a name="2"></a>Context

In the past few days, I worked on updating and migrating my samples connecting BIM and the cloud:

- [RoomEditorApp for Revit 2017](http://thebuildingcoder.typepad.com/blog/2016/04/room-editor-first-revit-2017-addin-migration.html#3)
- [Real-Time BIM Update with FireRatingCloud 2017](http://thebuildingcoder.typepad.com/blog/2016/04/real-time-bim-update-with-fireratingcloud-2017.html)

The [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) demonstrates
a full round-trip real-time connection between the Revit BIM and a simplified 2D view that can be graphically edited on any mobile device.
As a side effect, it also includes support for non-graphical editing of parameter values on family instances.

The [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) sample is somewhat simpler.
It also demonstrates a full round-trip real-time connection between the Revit BIM and an external NoSQL database, in this case connecting to a MongoDB database via a node.js web server.
It supports non-graphical editing of a specific shared parameter value representing the fire rating values on door family instance elements.

For both these applications, the database and web server can be hosted either in the cloud or locally on the same system running Revit.
Of course, global accessibility is easier to implement by hosting them on the web.

The stand-alone Windows client presented here shows that you can make selected BIM data available to absolutely anybody, absolutely anywhere, with a truly minimal amount of effort, providing full real-time round-trip editing capability.


#### <a name="3"></a>FireRatingClient

This stand-alone Windows client to edit the FireRatingCloud database values was originally implemented by
Jose Ignacio Montes, [@montesherraiz](https://github.com/Montesherraiz), of [Avatar BIM](http://avatarbim.com) in
Madrid, during the [BIM Programming Workshop](http://www.bimprogramming.com) in January.

It is extremely simple and obviously not fit for real-world use in a production environment in its current state.

For instance, the project name is not displayed, and elements cannot be filtered by project &ndash; or anything else either, for that matter &ndash; so currently all database records are loaded.
That will no longer work once there are enough records in the database.

It is totally inacceptable in the long run, but perfectly fine for my demonstration purposes here.

Also, simplicity is important for easier understanding.

I will now add the newly implemented `modified` timestamp field to each door data record that it displays, and update that field automatically when the `firerating` value is edited.

The modified values are immediately written back to the mongo database.

With the new database polling functionality added yesterday to the Revit add-in, these modifications will be detected and immediately and automatically reflected in the BIM, provided we have subscribed to that notification.

So let's get going.


#### <a name="4"></a>Adding the `modified` Field

In order to support the real-time BIM update from the mongo database,
I [implemented my own timestamp in C#](http://the3dwebcoder.typepad.com/blog/2016/04/fireratingcloud-document-modification-timestamp.html#5).

For simplicity, I use
a [Unix epoch timestamp](https://en.wikipedia.org/wiki/Unix_time),
i.e., the count of seconds since January 1, 1970, and store it as a simple number in MongoDB.

In C#, it is implemented by the `DoorData` class:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">DoorData</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_id&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;project_id&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;level&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;tag&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">double</span>&nbsp;firerating&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">uint</span>&nbsp;modified&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}


&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Constructor&nbsp;to&nbsp;populate&nbsp;instance&nbsp;by&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;deserialising&nbsp;the&nbsp;REST&nbsp;GET&nbsp;response.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;DoorData()
&nbsp;&nbsp;{
&nbsp;&nbsp;}
</pre>

The corresponding [mongoose](http://mongoosejs.com) schema definition to hold it looks like this:

<pre class="prettyprint">
var doorSchema = new Schema(
  { _id          : RvtUniqueId // suppress automatic generation
    , project_id : String
    , level      : String
    , tag        : String
    , firerating : Number
    , modified   : Number },
  { _id          : false } // suppress automatic generation
);
</pre>

I updated the FireRatingClient to display the modified field and published the result
as [release 2017.0.0.13](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.13).

You can see exactly what I changed to add the new column by looking at
the [diffs](https://github.com/jeremytammik/FireRatingCloud/compare/2017.0.0.12...2017.0.0.13).

Here is the new form in all its glory:

<center>
<img src="img/fireratingclient_with_modified_field.png" alt="FireRatingClient with modified field" width="">
</center>

As I mentioned directly when initially implementing the `modified` timestamp, anyone who updates a database entry is now responsible for explicitly setting the correct modified value as well.

That is exactly what we are about to do right now for FireRatingClient.


#### <a name="5"></a>Updating the `modified` Field on Edit

Updating the `modified` field each time a door data record is edited turned out to be incredibly easy.

Jose already set up the Windows client to transmit each and every interactive edit immediately to the mongo db via REST in the `ExportData` method called by the `OnDoorsCellEditFinished` event handler.

I added the timestamp update to it like this:

<pre class="code">
<span style="color:blue;">void</span>&nbsp;ExportData(&nbsp;<span style="color:#2b91af;">DoorData</span>&nbsp;dd&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">uint</span>&nbsp;timestamp&nbsp;=&nbsp;<span style="color:#2b91af;">Util</span>.UnixTimestamp();

&nbsp;&nbsp;dd.modified&nbsp;=&nbsp;timestamp;

&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;jsonResponse,&nbsp;errorMessage;

  <span style="color:#2b91af;">HttpStatusCode</span>&nbsp;sc&nbsp;=&nbsp;<span style="color:#2b91af;">Util</span>.Put(&nbsp;
  &nbsp;&nbsp;<span style="color:blue;">out</span>&nbsp;jsonResponse,&nbsp;<span style="color:blue;">out</span>&nbsp;errorMessage,
  &nbsp;&nbsp;<span style="color:#a31515;">&quot;doors/&quot;</span>&nbsp;+&nbsp;dd._id,&nbsp;dd&nbsp;);
}

<span style="color:blue;">void</span>&nbsp;OnDoorsCellEditFinished(
&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;sender,
&nbsp;&nbsp;BrightIdeasSoftware.<span style="color:#2b91af;">CellEditEventArgs</span>&nbsp;e&nbsp;)
{
&nbsp;&nbsp;ExportData(&nbsp;e.RowObject&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">DoorData</span>&nbsp;);
}
</pre>

Now all I want to do further is set up a nice demo for you and record it.


#### <a name="6"></a>FireRatingClient Live BIM Update Demo Recording

Here is an four-minute recording demonstrating the final result,
[real-time Revit BIM update via Windows Forms FireRatingClient](https://youtu.be/vJyXmxHgu9g):

<center>
<iframe width="420" height="315" src="https://www.youtube.com/embed/vJyXmxHgu9g?rel=0" frameborder="0" allowfullscreen></iframe>
</center>


#### <a name="7"></a>Download

The versions of FireRatingCloud and FireRatingClient presented above
are included in [release 2017.0.0.14](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.14),
available from the [FireRatingCloud GitHub repository](https://github.com/jeremytammik/FireRatingCloud).


#### <a name="8"></a>To Do

Let's update [the to do list that I published yesterday](http://thebuildingcoder.typepad.com/blog/2016/04/real-time-bim-update-with-fireratingcloud-2017.html#11).

I have several more exciting tasks lined up, all related to connecting BIM and the cloud:

- Completely rewrite the existing [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) sample to move it from CouchDB to node.js plus MongoDB.
- Implement the database portion of the [TrackChangesCloud](https://github.com/jeremytammik/TrackChangesCloud) sample.

I think I'll tackle the latter first...