<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---


<code></code>

BIM@TuDa, DevDays, Forge News and More Events @AutodeskForge #revitapi @AutodeskRevit #aec #bim @adskdevnetwrk

I am in Darmstadt preparing the Forge and BIM presentation and hands-on workshop at Technische Universität Darmstadt, Institut für Numerische Methoden und Informatik im Bauwesen, the institute for numerical methods and computer science in the construction industry at the technical university here. Many other larger events are coming up after this
&ndash; BIM@TuDa agenda
&ndash; Getting started with Dynamo
&ndash; Forge news
&ndash; New Forge resources
&ndash; Forge events and community
&ndash; DevDays &ndash; Developer Day conferences and accelerators...

-->

### BIM@TuDa, DevDays, Forge News and More Events

I am in Darmstadt preparing
the [Forge and BIM](http://www.bim.tu-darmstadt.de) presentation
and hands-on workshop 
at [Technische Universität Darmstadt](http://www.tu-darmstadt.de),
[Institut für Numerische Methoden und Informatik im Bauwesen](http://www.iib.tu-darmstadt.de),
the institute for numerical methods and computer science in the construction industry at the technical university here.

Many other larger events are coming up after this:

- [BIM@TuDa agenda](#2)
- [Getting started with Dynamo](#3)
- [Forge news](#4)
- [New Forge resources](#5)
- [Forge events and community](#6)
- [DevDays &ndash; Developer Day conferences and accelerators](#7)


#### <a name="2"></a>BIM@TuDa Agenda

The agenda here consists of two parts, a 90-minute presentation and a 4-hour hands-on workshop:

- Presentation &ndash; [Connecting desktop and cloud](http://thebuildingcoder.typepad.com/blog/2016/10/connecting-desktop-and-cloud-at-rtc-material.html)
- Workshop
    - Revit API &ndash; [creating a Revit add-in with one single click](http://thebuildingcoder.typepad.com/blog/2016/10/revit-api-and-connecting-desktop-and-cloud-tuda.html#4)
    - Connecting BIM with the cloud &ndash; [connecting Revit and Forge with 25 lines of code](http://thebuildingcoder.typepad.com/blog/2016/11/roomedit3dv3-diff-from-boilerplate-code.html#9)

Since some participants are probably not well versed in the Autodesk biosphere, we'll try to cram in some supplementary information, resulting in the following agenda:

- Presentation
    - Do you know Revit?
    - Do you know Forge? Quick Forge intro
    - Getting started with &ndash; theory
        - Revit API: add-in, macros, dynamo
        - Forge: WebGL, [developer.autodesk](https://developer.autodesk.com), [tutorial](https://developer.autodesk.com/en/docs/viewer/v2/tutorials/basic-viewer/)
    - Connecting desktop and cloud, Forge and BIM
- Workshop
    - Getting started with &ndash; practice
        - [Revit API add-ins](http://thebuildingcoder.typepad.com/blog/2016/10/revit-api-and-connecting-desktop-and-cloud-tuda.html#4)
        - [Forge viewer tutorial](https://developer.autodesk.com/en/docs/viewer/v2/tutorials/basic-viewer/)
        - [Adapting boilerplate code to connect Forge and BIM](http://thebuildingcoder.typepad.com/blog/2016/11/roomedit3dv3-diff-from-boilerplate-code.html)

Some last-minute coordination on this with Philipp Mueller is still outstanding...

<center>
<img src="img/logo_tuda_150x309.png" alt="Technische Universität Darmstadt" width="309">
</center>


#### <a name="3"></a>Getting Started with Dynamo

Since I am not a Dynamo expert myself, and other people recently also asked me how to get started with it efficiently, here are some more or less random notes on that topic, in case that is of interest to our participants:

- <b><u>A</u></b>dam Sheather recommends
  the [Dynamo samples in the DynamoDS repo](https://github.com/DynamoDS/Dynamo) as a great start.
- [<b><u>B</u></b>IMThoughts](http://bimthoughts.com) asks: Want to Learn Dynamo? Check
  out [DynamoThoughts on YouTube](https://www.youtube.com/c/dynamothoughts) with Ian Siegel!
- [<b><u>D</u></b>ynamo primer](http://dynamoprimer.com/en/)

Before looking forward to further upcoming events and learning opportunities, a quick look at the breaking news on Forge.


#### <a name="4"></a>Forge News

- The grace period
for [token scope enforcement](https://developer.autodesk.com/en/docs/oauth/v2/overview/scopes) will
expire for grandfathered-in apps on January 2, 2017. Please make sure to update your apps accordingly.
- A number of improvements to the viewer, including an
updated [getting started tutorial](https://developer.autodesk.com/en/docs/viewer/v2/tutorials/basic-viewer/),
a new [InViewerSearch extension](https://developer.autodesk.com/en/docs/viewer/v2/tutorials/in-viewer-search-ext/),
and [version 2.11](https://developer.autodesk.com/en/docs/viewer/v2/overview/changelog/2.11/):
    - `viewer.getProperties()` now returns `attributeName`, which can be used as a filter for search geometry snapping for mark-ups.
    - more efficient `viewer.restoreState()`
- The Data Management API will soon be introducing a new `hubs:autodesk.a360:PersonalHub` type that will allow distinguishing Team Hubs from Personal Hubs. More details about the update and any needed changes to your code will be described
in [Recent Changes](https://developer.autodesk.com/en/docs/data/v2/overview/changelog/),
and the full documentation will be on
the [GET hubs endpoint](https://developer.autodesk.com/en/docs/data/v2/reference/http/hubs-GET/) reference page.
- The 3D Print API will be retired on January 15, 2017, when all existing keys will lose access to this API. Read more in the [end of life notice](https://developer.autodesk.com/en/docs/print/v1/overview/end-of-life/). New APIs will be announced in the near future!

#### <a name="5"></a>Forge Resources

[Recordings of the six recent technical webinars on Forge APIs](http://adndevblog.typepad.com/cloud_and_mobile/2016/10/new-training-on-autodesk-forge-apis-five-webinars-now-available-for-viewing.html) are now available, covering workflows, platform use cases, and code samples.

#### <a name="6"></a>Forge Events and Community

Loads of events are coming up! Check out the [complete list of upcoming events](http://adndevblog.typepad.com/cloud_and_mobile/2016/10/upcoming-forge-related-events.html) with full descriptions. Here are the highlights:

- November 14 &ndash; [DevDays and DevLab](http://au.autodesk.com/las-vegas/pre-conference/adn-conference-devdays) &ndash; Las Vegas, Nevada
- November 15-17 &ndash; [Autodesk University](http://au.autodesk.com/) &ndash; Las Vegas, Nevada &ndash; 
Join us at our [user study](https://autodeskuserresearch.doodle.com/poll/tbrmmpng3zevqnbr) while you're at AU!
- December 5-23 &ndash; [ADN DevDays](http://autodeskdevdays.com/) &ndash; Worldwide

Questions? Suggestions? Let us know at [@ForgePlatform](https://twitter.com/ForgePlatform) or check out our tips on finding answers on [StackOverflow](https://developer.autodesk.com/en/support/get-help).



#### <a name="7"></a>DevDays &ndash; Developer Day Conferences and Accelerators

Partially covering Forge and also addressing the desktop products, the main traditional Autodesk developer events of year are coming: the annual, worldwide [Developer Day Conferences and Accelerator workshops](http://autodeskdevdays.com/).

Free of charge, they kick off in two weeks' time, starting in Las Vegas, Nevada, then moving on to Europe and Asia.

Attending DevDays is the best way for you to learn about the latest and upcoming Autodesk application development technologies.  You can learn about the Autodesk Cloud Platform [Forge](https://forge.autodesk.com/), as well as network with Autodesk engineers and other developers working with Autodesk APIs. 

Personally, I am attending the following sessions in USA and Europe:

- United States
    - November 14, 2016 &ndash; DevDay at AU, Las Vegas
    - November 15, 2016 &ndash; DevLab at AU, Las Vegas
- Germany
    - December 5, 2016 &ndash; DevDay, Munich
    - December 6-9, 2016 &ndash; Accelerator, Munich

If you have already registered to attend one of the events &ndash; congratulations, me and my colleagues look forward to seeing you soon!

If not, read on.

Registration for DevDays Las Vegas is via the [Autodesk University website](http://au.autodesk.com/las-vegas/pre-conference/adn-conference-devdays).
Register and choose your sessions here.

If you are attending another Monday conference at AU and therefore cannot register for DevDays but would like to drop in at one or more DevDay sessions, please let us know so that we can add you to a list for special access.  Just send us an [email to adnreg@autodesk.com](mailto:adnreg@autodesk.com) with your name and company name and we’ll take care of it.

Registration for Asia and Europe is easy.  Simply visit the event website at [www.autodeskdevdays.com](http://autodeskdevdays.com) for full information and click on the [Register button](http://autodeskdevdays.com/register).
