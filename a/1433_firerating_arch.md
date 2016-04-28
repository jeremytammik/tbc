<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

FireRatingCloud Context and Architecture #revitAPI #3dwebcoder @AutodeskRevit #bim @AutodeskForge #3dwebaccel

A picture says more than a thousand words.
I find a compelling and clear picture takes a lot of work, though.
I created the image below to explain the context and architecture of
the FireRatingCloud and other samples connecting BIM and the cloud
like RoomEditorApp, followed by some musings on using Revit as a server
&ndash; FireRatingCloud context and architecture
&ndash; You cannot use Revit as a server...

-->

### FireRatingCloud Context and Architecture

A picture says more than a thousand words.

I find a compelling and clear picture takes a lot of work, though.

I normally go for the words instead, as you may have noticed.

Unfortunately, it probably takes just as much effort to reduce ten thousand meandering words to the one thousand succinct and clear ones that your really need.

> Perfection is achieved, not when there is nothing more to add, but when there is nothing left to take away <p style="text-align: right"><i>Antoine de Saint-Exupéry</i></p>

So maybe the net effort remains the same in the end.

I made an exception last night and created the image below to explain the context and architecture of
the [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) and other samples connecting BIM and the cloud
like [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp), followed by some musings on using Revit as a server:

- [FireRatingCloud context and architecture](#2)
- [You cannot use Revit as a server](#3)



#### <a name="2"></a>FireRatingCloud Context and Architecture

I took a first stab yesterday at explaining
the [context of the FireRatingCloud sample](http://thebuildingcoder.typepad.com/blog/2016/04/real-time-bim-update-via-fireratingcloud-windows-client.html#2).

I later took that one step further, creating the image below and adding an in-depth explanation of
the [context and architecture](https://github.com/jeremytammik/FireRatingCloud#context-and-architecture) to
the [FireRatingCloud GitHub repository](https://github.com/jeremytammik/FireRatingCloud):

FireRatingCloud is a C# .NET Revit add-in. It is a multi-project re-implementation of the FireRating SDK sample.

It uses a REST API to access a cloud-based database managed by the
[fireratingdb](https://github.com/jeremytammik/firerating)
[node.js](https://nodejs.org)
[MongoDB](https://www.mongodb.org) web server.

The repo also includes two other projects:

- [FireRatingClient](https://github.com/jeremytammik/FireRatingCloud/tree/master/FireRatingClient), a stand-alone Windows forms executable implementing a read-write
fireratingdb client that you can use to remotely edit the BIM without entering or even installing Revit.
- [FireRating](https://github.com/jeremytammik/FireRatingCloud/tree/master/FireRating), a shared library used by both FireRatingClient and FireRatingCloud.

Here is an image showing the links and relationships between BIM, cloud, Revit, node.js and MongoDB and explaining how and where fireratingdb and the three FireRatingCloud components fit into the picture:

<center>
<img src="img/fireratingcloud_architecture.png" alt="FireRatingCloud modules and architecture" width="">
</center>

I created this drawing using [draw.io](https://www.draw.io), and the
source [XML file](https://github.com/jeremytammik/FireRatingCloud/blob/master/img/fireratingcloud_architecture.xml) is provided.

All REST API calls on the desktop are handled by the shared .NET class library FireRating.dll and passed to the `firerating` database in MongoDB via the node.js web server. It contains one single collection `doors` containing door data JSON documents. Other clients can connect to that server as well, from any kind of device. Both the node web server and the mongodb database can actually be run either in the cloud or locally on your own system, even on the Windows system running Revit. These two choices are controlled by Boolean flags in the FireRating library and web server, respectively.

Only a few technical users will interact with full-fledged Revit and the BIM. A much larger number of all kinds of users can be provided access to relevant subsets of the BIM data using this technology. This and others samples demonstrate how that access can include real-time editing and BIM updating, if you so please. The FireRatingCloud sample is intentionally kept simple and limited to managing and providing access to one single shared parameter value. The [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) shows how you can extract and interact with graphical data as well, including graphical interaction on any mobile device with a simplified 2D view rendered using SVG in the browser.

I hope that all is clear now, and [everything is illuminated](https://en.wikipedia.org/wiki/Everything_Is_Illuminated) (one of my favourite films *and* books).

The topic of connecting BIM and the cloud leads to another frequently asked question, which also just came up again this very day in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160):


#### <a name="3"></a>You Cannot Use Revit as a Server

Inevitably, talking about connecting BIM and the cloud, the question on using Revit as a server pops up.

We provided several examples in the past that might help enable that kind of usage, e.g.:

- [A pattern for semi-asynchronous Idling API access](http://thebuildingcoder.typepad.com/blog/2010/11/pattern-for-semi-asynchronous-idling-api-access.html)
- [Driving Revit through a WCF service](http://thebuildingcoder.typepad.com/blog/2012/11/drive-revit-through-a-wcf-service.html)
- [Processing a queue of commands to control Revit from a modeless dialogue](http://thebuildingcoder.typepad.com/blog/2013/06/behind-the-scenes-of-the-nbs-revit-add-in.html#3)
- [Batch processing Revit documents](http://thebuildingcoder.typepad.com/blog/2015/08/batch-processing-dwfx-links-and-future-proofing.html#4)
- [Revit Python shell in the cloud as a web server](http://thebuildingcoder.typepad.com/blog/2015/07/firerating-and-the-revit-python-shell-in-the-cloud-as-web-servers.html#5)

Please be aware, though, that the EULA or *end user license agreement* clearly prohibits the use of Revit as a server, and it cannot be used as part of a web site or service.

If you would be interested in any such-like future functionality,
[Autodesk is interested in talking with you](http://thebuildingcoder.typepad.com/blog/2016/02/what-can-revit-on-the-cloud-do-for-you.html) about
it to help make a decision on possibly exposing some sort of 'Revit I/O' web service.

Here is a question raised today by Guillaume
on [updating family information with scripts without using Revit locally](http://forums.autodesk.com/t5/revit-api/updating-family-information-with-scripts-without-using-locally/m-p/6298561):

**Question:** Is it possible to modify only the information part of a RFA without modifying the geometric part by using  server scripts?

I know I can do it by using the API within Revit, but is there any other way?

Is it possible to use some kind of Revit cloud license to update a RFA file with the new parameters?

Do you have Revit licences for Linux servers?

**Answer:** Thank you for your very relevant and pertinent questions.

We were just discussing a similar topic internally this very morning.

Let's start at the end:

Q: Do you have Revit licences for Linux servers?

A: No.

Q: Is it possible to use some kind of Revit cloud license to update a RFA file with the new parameters?

A: No, there is currently no such license. Revit is not suited for use as a server. It is user interface oriented. Using it as a server violates the EULA. We are thinking about it, though. More on this below.

Q: Is it possible to modify only the information part of a RFA without modifying the geometric part by using server scripts? I know I can do it by using the API within Revit, but is there any other way?

A: I do not think so. You can read part atoms, but can you modify them? Please try it out and let us know what you find out. Here is a discussion on the topic
of [reading an RVT file without Revit](http://thebuildingcoder.typepad.com/blog/2016/02/reading-an-rvt-file-without-revit.html).

You should definitely contact Jim Quanci and discuss your wishes and requirements with him, as he invited all developers to do in January this year,
asking [what can Revit on the cloud do for you?](http://thebuildingcoder.typepad.com/blog/2016/02/what-can-revit-on-the-cloud-do-for-you.html#2)

I put that discussion into context later,
exploring [why BIM Cloud?](http://thebuildingcoder.typepad.com/blog/2016/04/forge-devcon-spring-impermanence-and-bim-cloud.html#6)

Regardless of the context, all external access to the Revit API has to take into account the rules and restrictions
that apply to [Idling and external events for modeless access and driving Revit from outside](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

Instead of using Revit as a server, you can always explore generating a public format and interacting with that, or interacting with specific subset of the data, as demonstrated by my samples to connect BIM and the cloud, most recently these three based on Revit 2017:

- [Room Editor &ndash; First Revit 2017 Addin Migration](http://thebuildingcoder.typepad.com/blog/2016/04/room-editor-first-revit-2017-addin-migration.html)
- [Real-Time BIM Update with FireRatingCloud 2017](http://thebuildingcoder.typepad.com/blog/2016/04/real-time-bim-update-with-fireratingcloud-2017.html)
- [Real-Time BIM Update via a Windows Forms Client](http://thebuildingcoder.typepad.com/blog/2016/04/real-time-bim-update-via-fireratingcloud-windows-client.html)

They show how you can easily extract a minimal set of relevant data from the BIM and make that globally available, including the possibility to edit it and reintegrate the changes back into the BIM in real-time.

I hope this helps and look forward to hearing the outcome of your discussion.
