<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- http://adndevblog.typepad.com/cloud_and_mobile/2016/04/early-bird-registration-for-forge-devcon.html

- upcoming events:
  Barcelona May 16-20 -- cloud accelerator http://autodeskcloudaccelerator.com
  Athens June 1-2 -- meetup, BIM und forge workshop
  San Fransico June 15-16 -- forge developer conference http://forge.autodesk.com/conference
  San Fransico June 20-24 -- forge accelerator http://autodeskcloudaccelerator.com
  Porto October 20-22 -- RTC Europe Revit Technology Conference http://www.revitconference.com/rtc2016eur/

- David Manning <dlmanning@me.com> Re: Application for Autodesk Developer Network/Open source

- my class mate passed away
  a close friend. we travelled together for a couple of months after finishing high school together and before moving on to university and adult life
  https://internwebben.ki.se/en/memory-professor-lorenz-poellinger

- pictures of spring

Forge DevCon, Spring, Impermanence, BIM Cloud #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim @adskForge #3dwebaccel

Spring is getting into swing, and new life is budding.
I hope the same holds true for all your development efforts and visions of the future.
Not everything is budding into new life, though... part of existence is passing away, as well.
One of my dearest school classmates and closest friends of my youth, Prof. Dr. Lorenz Poellinger, unexpectedly passed on.
The two of us travelled together for several of months, climbing and hitchhiking...

-->

### Forge DevCon, Spring, Impermanence, BIM Cloud

Spring is getting into swing, and new life is budding.

<center>
<img src="/p/2016/2016-04-07_spring_budding/923_tree_in_bloom_cropped.jpg" alt="Tree in bloom" width="300">
</center>

I hope the same holds true for all your development efforts and visions of the future.

#### <a name="2"></a>Impermanence

Not everything is budding into new life, though... part of existence is passing away, as well.

<center>
<img src="/p/2016/2016-04-07_spring_budding/912_petals_cropped.jpg" alt="Petals" width="300">
</center>

One of my dearest school classmates and closest friends of my youth,
[Prof. Dr. Lorenz Poellinger](https://internwebben.ki.se/en/memory-professor-lorenz-poellinger),
unexpectedly passed on.

Lorenz was an active and successful scientist at [Karolinska Institutet](http://ki.se/en) in Stockholm
and the [Cancer Science Institute CSI](https://www.csi.nus.edu.sg/ws/uncategorized/remembering-prof-lorenz-poellinger/remembering-professor-lorenz-poellinger) in Singapore.

The two of us travelled together for several of months, climbing and hitchhiking all over southern Europe, just after finishing high school and before moving on to university and adult life.

He was the one who initiated climbing in our hiking circle, and he died in the mountains as well.

[Impermanence](https://en.wikipedia.org/wiki/Impermanence),
[arising and passing away](http://www.changesurfer.com/Bud/Begin.html).

Hej då, Lolle.


#### <a name="3"></a>Sommerlied

<p style="padding-left: 10%">wir sind menschen auf den wiesen
<br/>bald sind wir menschen unter den wiesen
<br/>und werden wiesen, und werden wald
<br/>das wird ein heiterer landaufenthalt</p>

<p style="text-align: right; padding-right: 10%"><i>Ernst Jandl</i></p>


#### <a name="4"></a>Projects

Looking forward, I have a whole bunch of exciting events and projects lined up for the next couple of months:

- [Cloud accelerator](http://autodeskcloudaccelerator.com) in Barcelona May 16-20
- Meetup, BIM und forge workshop in Athens June 1-2
- [Forge DevCon developer conference](http://forge.autodesk.com/conference) in San Francisco June 15-16
- [Forge accelerator](http://autodeskcloudaccelerator.com) in San Fransisco June 20-24
- [RTC Europe Revit Technology Conference](http://www.revitconference.com/rtc2016eur) in Porto October 20-22 &ndash; somewhat further away in time...

The most exciting event by far is the [Forge DevCon developer conference](#5).

Let's look at that in more detail, and add some motivation on the [cloud-based orientation](#6) of all this:


#### <a name="5"></a>Forge DevCon Developer Conference

The first ever Autodesk Forge Developer Conference will take place in San Francisco on June 15-16.
The conference is for programmers and entrepreneurs interested in using 'cloud' technologies to disrupt the AEC and Manufacturing industries.
Click the image below to go to the conference website.

<center>
<a href="http://forge.autodesk.com/conference">
<img src="img/forge_devcon.png" alt="Forge DevCon" width="600">
</a>
</center>

Early bird registration is due to end on April 15th, so you don't have much time to buy your ticket at the low cost of $499.

If you're a student, you can come for free &ndash; just sign up for a student ticket using an `.edu` email address.

We are still working on the agenda.
You can explore the list of the classes we currently have planned
at [forge.autodesk.com/tracks-and-speakers](http://forge.autodesk.com/tracks-and-speakers).

I will definitely be attending both the conference, a meetup or two before and afterwards, and the accelerator in the following week.

I am already busy planning and researching for my presentation on BIM, connecting the desktop and cloud.


#### <a name="6"></a>BIM Cloud? Why?

**Question:** I am interested is in developing a free library that would provide some manner of abstraction over the Revit API, with an intention of making it more accessible beyond the confines of C#. There are two possible avenues I’d like to explore:

1. I believe it may be fairly simply to wrap some portion of the API using an open source framework called [EdgeJS](http://tjanczuk.github.io/edge), which would enable JavaScript calls from within Node.js, and thereby provide a bridge to an existing ecosystem of tools and utilities. Developers working against the Revit API are currently unable to leverage this wider world of technologies, and many reduplicate others' efforts attempting to bridge the gap.

2. A somewhat more ambitious approach would involve making some subset of the API accessible over an http server communicating directly with Revit. Traditionally such an approach would use ASP.NET’s WebAPI framework to provide a RESTful interface. However, I would also like to explore integrating an emerging web technology called [GraphQL](http://graphql.org), which has recently been developed and released by Facebook, and which allows for client definable data queries against a server-defined data schema. Here I would begin with the .NET implementation of the [GraphQL standard](https://github.com/graphql-dotnet/graphql-dotnet).

As to why I would want to do this: a friend of mine is a BIM lead for a largish architecture firm and frequently talks to me about tools she wishes existed for Revit. I know many other BIM people are asking for similar functionality. I am a software developer, with no interest in buildings or architecture, but a lot of interest in data modelling.

I would like to try to make some tools to help the BIM community. Should this effort lead to anything useful, I have no interest in making money from it, but would prefer to release it as open source for others to use in their own projects.


**Answer:** I am sure that the direct Revit API access via http that you describe can be implemented.

In fact, here is an example of exactly that,
[driving Revit through a WCF service](Http://thebuildingcoder.typepad.com/blog/2012/11/drive-revit-through-a-wcf-service.html),
and it is not the only one.

Look at The Building Coder topic group
on [modeless access and driving Revit from outside](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28) for
several other related ideas.

It would not be stable, however, so your framework would definitely have to monitor performance, keep track of tasks queued and executed, and deal with the fact that Revit might need to be killed and restarted occasionally.
A product designed with an end user interface in mind is simply not built to act as a pure server.

That is why we just recently published a request for feedback on what developers would like to see and to implement in the kind of scenario you describe:

- [What can Revit on the cloud do for you?](Http://thebuildingcoder.typepad.com/blog/2016/02/what-can-revit-on-the-cloud-do-for-you.html#2)
- [Reading an RVT file without Revit](Http://thebuildingcoder.typepad.com/blog/2016/02/reading-an-rvt-file-without-revit.html)

On the other hand, I can see a number of different ways to approach the wish for powerful BIM analysis and reporting.

This fits in really well with some projects I have been working on in the past year or so to connect desktop and cloud:

- [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp), a 2D cloud-based Revit room editor
- [FireRating in the cloud](https://github.com/jeremytammik/FireRatingCloud)
    - FireRatingCloud batch upload enhancement
    - FireRatingClient ubiquitous BIM data access and editing
- [CompHound](https://github.com/CompHound/CompHound.github.io), a cloud-based system to analyse, visualise and report on universal component and asset usage
- [TrackChangesCloud](https://github.com/jeremytammik/TrackChangesCloud)

I am sure they will be of interest to you as well.

Maybe some parts of them will provide a good basis for implementing exactly what you think your wife would need  :-)


**Response:** Yes, I work with web centric stuff. Most of what I’ve done for the last few years has centred around Node.js and JavaScript. This is probably the perspective that makes me interested in getting Revit content accessible in a more platform agnostic form.

For example, imagine that there were a JSON serializable format for Revit objects, such as would allow modification and reintroduction into an existing model (substitute protobufs or Cap’n Proto for JSON if you like). Aside from analytics, you could make a voxel representation of a project, or write a custom ReactJS renderer to display model views. A company could hook up a real-time dashboard of statistics about how many different wall types they’ve used today (exciting!). Of course, these things are possible from the .NET API, there’s just a much higher tooling buy in to Visual Studio, .NET, et al, and a higher barrier to integration with existing technologies where many people already have existing skills. This will tend to make 3rd party development a sort of dark art practiced only by a few cognoscenti, and its broader appeal more happenstance. The more accessible the data, the more weird, little, idiosyncratic things Autodesk’s customers can do for themselves, and the more valuable Autodesk’s products become.

Indeed the links you send are very interesting and I’ll no doubt be referring to them for guidance if I’m able to proceed. RoomEditorApp in particular is very close to the kind of thing I was talking about. You’ve effectively defined a serialization of a subset of Revit objects in defining your [DbObj](https://github.com/jeremytammik/RoomEditorApp/blob/master/RoomEditorApp/DbModel.cs#L12).

Thanks for pointing this out to me! Of course you’re right that any abstraction layer would need to track API changes, and Revit isn’t intended to function as an http server. Offhand I'd say I’d probably treat it more like a database I wasn’t sure was going to be there, make my own server, and have an addin register itself as a resource with the server when it was around as well as provide the interface.

Anyway, this would all be a lot of fun to try.

**Answer:** I think trying to capture the entire complexity of a Revit BIM would be a crazy undertaking.

Doing something limited and specialised like the room editor on the other hand is absolutely trivial and offers an infinite number of incredibly powerful possibilities with extremely little cost and effort.

That is exactly what I thought when I first had it up and running two or three years ago.

I realised that with that technology alone in hand, I could go out and found a hundred different startups right away, and dozens of them would certainly be successful.

It is so simple!

All you need is ideas.

I am absolutely convinced you can easily achieve something extremely useful using some flavour or other of the room editor.

Oh, and have you looked at the [open source BIM](https://duckduckgo.com/?q=open+source+bim) initiatives around?

There are oodles of them!
