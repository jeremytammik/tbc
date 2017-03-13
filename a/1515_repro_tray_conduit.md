<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- creating a miminal reproducible case can indeed solve the problem
  private message in the Autodesk Community. From: kinjal Subject: Re: Kind attention towards a coordinate system issue
  http://forums.autodesk.com/t5/revit-api-forum/different-coordinate-system-for-referenceintersector/m-p/6591571

- how to research revit api solution
  12162499 [How can I get the connectors of a FamilyInstance?]
http://thebuildingcoder.typepad.com/blog/2016/09/macromanager-materials-kiss-and-getting-started.html#4
http://thebuildingcoder.typepad.com/blog/2016/09/macromanager-materials-kiss-and-getting-started.html#5
http://thebuildingcoder.typepad.com/blog/2016/09/trusted-signature-motivation-and-fishing.html#3
http://forums.autodesk.com/t5/revit-api-forum/access-to-line-based-elements/m-p/6569135

- CF-3559 [API wish: access to ElectricalSettings cable tray and conduit settings] was fixed and closed
http://thebuildingcoder.typepad.com/blog/2011/09/transfer-project-standards.html#comment-2340822661
REVIT-79737 [As a third part developers I want to be able to access the cable/tray and conduit sizes/settings through public API] was fixed and closed

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

I have probably asked developers for reproducible cases a couple of thousand times by now.
Let's discuss that and also return to the topic of how to research a Revit API problem in general.
Finally, I'll point out a Revit 2017 API MEP electrical feature.
Oh, and I continued my research into deep learning for a Revit API question answering system
&ndash; Creating a reproducible case may well solve the issue at hand
&ndash; How to research to find a Revit API solution
&ndash; Access to cable tray and conduit settings
&ndash; Getting started implementing a question answering system
&ndash; First impression from IBM Bluemix
&ndash; Open source QAS options
&ndash; Building a Revit API ontology...

#AULondon, #UI, #innovation, #RevitAPI, @AutodeskRevit bit.ly/2j7Sxkb

-->

### Virtues Reproduction, Research, Settings, Ontology

I have probably asked developers for reproducible cases a couple of thousand times by now.

Let's discuss that in a little bit more depth for the nonce, and also return once again to the topic of how to research a Revit API problem in general.

Finally, I'll point out a possibly unnoticed Revit 2017 API MEP electrical feature.

Oh, yes, and I continued my research into deep learning for a Revit API question answering system:

- [Creating a reproducible case may well solve the issue at hand](#2)
- [How to research to find a Revit API solution](#3)
    - [Question &ndash; how can I get the connectors of a `FamilyInstance`?](#3.1)
    - [Answer &ndash; search online and samples](#3.2)
    - [How to research for a Revit API solution](#3.3)
    - [Response &ndash; Yay!](#3.4)
- [Access to cable tray and conduit settings](#4)
- [Getting started implementing a question answering system](#5)
- [First impression from IBM Bluemix](#6)
- [Open source QAS options](#7)
- [Building a Revit API ontology](#8)


####<a name="2"></a>Creating a Reproducible Case May Well Solve the Issue at Hand

As said, I frequently ask for
a [reproducible test case](http://thebuildingcoder.typepad.com/blog/about-the-author.html#1b) to
analyse when researching a problem report.

It normally includes as many of the following items as possible:

- A non-confidential minimal reproducible test case. That should mostly include:
    - A minimal sample macro embedded in...
    - A minimal project file to run it in
    - Detailed step-by-step instructions specifying exactly:
        - What you are trying to achieve
        - The behaviour you observe
        - The difference between the two
        - How to reproduce the issue
        
Without a possibility to reproduce the problem, there is no way for the development team or anyone else to analyse and discover what it is.

Normally, the best way to explore a programming issue in more detail is to run it in the debugger.

Furthermore, if the problem can be reproduced and solved, we still need to test the fix and verify that it really does what we expect.

Another even more beneficial aspect for all involved is that creating a minimal reproducible case like this can help you discover for yourself what you were doing wrong, and the issue becomes moot.

That was evidenced by a recent issue raised by Kinjal in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread 
on [different coordinate system for `ReferenceIntersector`](http://forums.autodesk.com/t5/revit-api-forum/different-coordinate-system-for-referenceintersector/m-p/6591571):

**Question:** Since a few days I am facing a rather strange issue.
`ReferenceIntersector` started returning off-the-chart values, e.g., referring a floor that is at elevation of 22ft per ProjectBasePoint, it reported -24ft in `Reference.GlobalPoint` for a particular Revit project, while it works well for other projects.

A long explanation and further suggestions from other developers follow, ending with my suggestion to create and submit a reproducible case:

**Answer:** Please submit a minimal reproducible case so this can be discussed with and analysed by the development team...

**Response:** Thanks for your response. Silly enough, but the problem is solved now. Sorry for asking. I'd lay it out for someone who might benefit from it...

Isn't that nice?

Many thanks to Kinjal for his friendly appreciation, confirmation and further explanation of the underlying problem!


####<a name="3"></a>How to Research to Find a Revit API Solution

Creating a reproducible case is vaguely linked to the more general and complex question about how to research a Revit API task in general.

I keep putting together and repeating variations of advice on that and would like to consolidate my efforts at some point.

Here are some recent examples of my attempts so far:

- [Getting started and changing the colour of a wall](http://thebuildingcoder.typepad.com/blog/2016/09/macromanager-materials-kiss-and-getting-started.html#4)
- [Getting started and using the Visual Studio Revit Add-In wizard auto-installer](http://thebuildingcoder.typepad.com/blog/2016/09/macromanager-materials-kiss-and-getting-started.html#5)
- [What happened to `LoadCaseArray`; how and where to search for help on a Revit API question?](http://thebuildingcoder.typepad.com/blog/2016/09/trusted-signature-motivation-and-fishing.html#3)
- [Access to line-based elements?](http://forums.autodesk.com/t5/revit-api-forum/access-to-line-based-elements/m-p/6569135)

Now for yet another attempt to complete finalise this advice, from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread 
on [accessing the connectors of a FamilyInstance](http://forums.autodesk.com/t5/revit-api-forum/how-can-i-get-the-connectors-of-a-familyinstance/m-p/6558978):

####<a name="3.1"></a>Question &ndash; How Can I Get the Connectors of a FamilyInstance?

How can I get the connectors of a FamilyInstance?

####<a name="3.2"></a>Answer &ndash; Search Online and Samples
 
Please perform a search of your own before asking questions here, both for your own efficiency and the sake of others.

In this case, a search
for [revit api familyinstance connectors](https://duckduckgo.com/?q=revit+api+familyinstance+connectors)
or [thebuildingcoder familyinstance connectors](https://duckduckgo.com/?q=thebuildingcoder+familyinstance+connectors) will certainly provide some starting points for you.

Obviously, you should also always search the Revit API help file, now accessible [online at `revitapidocs.com`](http://www.revitapidocs.com).

That immediately yields the following call sequence to explore: `FamilyInstance` &gt; `MEPModel` &gt; `ConnectorManager` &gt; `Connectors`.

Next, turn to the [Revit SDK](www.autodesk.com/developrevit) samples and search for something like `ConnectorManager`; that causes 132 hits in 8 files.

[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) also provide a whole bunch of samples handling connectors in various ways.

That should give you more than enough to get started, and hopefully answer an infinite number of similar future questions at the same time.

In addition to these obvious searches, there are indeed several other levels of information that are worthwhile checking out.
 
I keep trying to create the ultimate complete list of them, and have not succeeded to my complete satisfaction so far.
 
Let me expand
on [my last attempt](http://thebuildingcoder.typepad.com/blog/2016/09/trusted-signature-motivation-and-fishing.html#3) based on what I recently said there and elsewhere:

####<a name="3.3"></a>How to Research for a Revit API Solution

1. Determine the optimal solution manually through the end user interface. Make sure you follow best practices and make use of existing built-in Revit functionality. If you skip this step or do not research deeply enough, you run a large risk of programming something that will be painful both to implement, maintain, debug and use.
2. Determine the names of the Revit classes, methods and properties that will help you achieve your task. For example, create the appropriate situation and sample BIM via the user interface and analyse it before and after making the modifications you need, e.g., using:
    - [RevitLookup](https://github.com/jeremytammik/RevitLookup)
    - [BipChecker](https://github.com/jeremytammik/BipChecker)
    - [The element lister](https://github.com/jeremytammik/AdnRevitApiLabsXtra)
    - [Other, more intimate Revit database exploration tools](http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html),  such as the Revit Python or Ruby shell
3. Once you know what Revit API objects are required, learn how to access, manipulate and drive them, their relationships and how they interact with each other:
    - Revit API help file `RevitAPI.chm` installed locally or [online at revitapidocs.com](http://www.revitapidocs.com) provides detailed info on classes, properties and methods.
    - [Revit online help](http://help.autodesk.com/view/RVT/2017/ENU) &gt; Developers &gt; [Revit API Developers Guide](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42) explains the Revit API usage in much more depth and provides invaluable background information.
    - Revit SDK sample collection installed locally and managed by Visual Studio via `SDKSamples.sln` shows how Revit API objects work together to solve specific tasks.
    - [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) provide another large bunch of sample external commands implementing numerous different tasks.
 
After you have exhausted those options, search the Internet for 'revit api' or 'thebuildingcoder' plus the Revit API names that you are interested in.
 
I very much hope that this does not only feed you for the moment, but also supports you in the process of being transformed into a competent fisherman &nbsp; :-)

####<a name="3.4"></a>Response &ndash; Yay!

Thank you, Jeremy!
 
This will certainly help me much in the future!


####<a name="4"></a>Access to Cable Tray and Conduit Settings

Let's round off this discussion by pointing out an easy new solution to a previously intractable requirement:

The development team just pointed out to me that the wish list item I raised for Mark over a year ago has long been fulfilled, so I thought I would share that happy news here with you are well.

Mark asked in
a [comment](http://thebuildingcoder.typepad.com/blog/2011/09/transfer-project-standards.html#comment-2340822661)
on [transferring project standards](http://thebuildingcoder.typepad.com/blog/2011/09/transfer-project-standards.html):

**Question:** I would like to change or add conduit sizes in the MEP Electrical and Conduit settings through the Revit API.
The `ElectricalSetting` class does not support Tray or Conduit settings.
Conduit settings can be modified from the GUI.
Is there any Revit API that supports this, or any workaround for the issue?

**Answer:** New APIs for this access were added in Revit 2017:

- `Autodesk.Revit.DB.Electrical.CableTraySettings`
- `Autodesk.Revit.DB.Electrical.ConduitSettings`

These two classes were also mentioned in the section
on [Electrical API additions](http://thebuildingcoder.typepad.com/blog/2016/04/whats-new-in-the-revit-2017-api.html#3.14)
in [What's New in the Revit 2017 API](http://thebuildingcoder.typepad.com/blog/2016/04/whats-new-in-the-revit-2017-api.html).


####<a name="5"></a>Getting Started Implementing a Question Answering System

As I mentioned last month, I would like to experiment with
an [automated system to answer the most common recurring questions on the Revit API](http://thebuildingcoder.typepad.com/blog/2016/10/ai-edit-and-continue.html#3),
i.e., a [question answering](https://en.wikipedia.org/wiki/Question_answering) system or QAS.

I started doings some basic fleeting research this week, skimming
the [Internet literature on deep learning](http://thebuildingcoder.typepad.com/blog/2017/01/au-in-london-and-deep-learning.html#7).

####<a name="6"></a>First Impression from IBM Bluemix

Based on a quick skim of the literature, my first impression was that the best system might be the IBM Watson one used by Ashok Goel for his automated teaching assistant, which does a similar job.

I registered to the [IBM Bluemix](www.IBM.com/bluemix) platform and started exploring that.

It is free for thirty days and then starts billing, with free options only for the most limited use and no obvious provisions for greater support for open source projects

Examining the pricing options turned me off, even without having any idea what these numbers might mean:

- Document Conversion Pricing Standard CHF 0.0474 CHF/MB, First 100 MB per month free, additional MB $0.05 per MB.
- Retrieve and Rank &ndash; The IBM Watson Retrieve and Rank service helps users find the most relevant information for their query by using a combination of search and machine learning algorithms to detect "signals" in the data. Built on top of Apache Solr, developers load their data into the service, train a machine learning model based on known relevant results, then leverage this model to provide improved results to their end users based on their question or query. &ndash; Pricing Plan: Monthly prices shown are for country or region: Switzerland; Standard; Retrieve: 1 shared Solr cluster (up to 50 MB) free per month, Rank: 1 Rank Model free per month, 1000 API calls free per month, 4 Training Events free per month, CHF 0.28 CHF/dedicated high availability Retrieve Solr cluster per instance hour, CHF 9.48 CHF/Instance Rank machine learning model per month, CHF 0.00084 CHF/Rank API call, CHF 1.90 CHF/Rank training event; Retrieve: You will be charged per instance hour for each Solr cluster based on the size of that cluster. The size of the cluster can be set by using the cluster_size parameter when creating the cluster. The base cluster is 4 GB of memory and 32 GB of storage, and includes high availability. Clusters can be scaled up to 7X the base cluster size (28 GB of memory, 224 GB of storage). For example, a 2X cluster is 8 GB of memory and 64 GB of storage and would be charged twice the amount of a base cluster. Rank: You will be charged for each API call, model, and training event.
- Natural Language Classifier &ndash; The Natural Language Classifier service applies cognitive computing techniques to return the best matching classes for a sentence or phrase. For example, you submit a question and the service returns keys to the best matching answers or next actions for your application. You create a classifier instance by providing a set of representative strings and a set of one or more correct classes for each training. After training, the new classifier can accept new questions or phrases and return the top matches with a probability value for each match. &ndash; Pricing Standard: 1 Natural Language Classifier instance free per month, 1000 API calls free per month, 4 Training Events free per month, CHF 18.95 CHF/ Instance per month, CHF 0.00332 CHF/ API call, CHF 2.85 CHF/ Training Event; you will be charged per API call, per instance, and per training event.

Hmm. No, thank you, not right now.

####<a name="7"></a>Open Source QAS Options

Looking for open source alternatives to IBM Watson yields immediate promising results:

- [Watson wannabes: 4 open source projects for machine intelligence](http://www.infoworld.com/article/2858891/machine-learning/four-open-source-watson-machine-intelligence.html) &ndash; these projects are not as established as Watson, but they're open source and ready for work.
    - [DARPA DeepDive](http://deepdive.stanford.edu/) &ndash; SQL and Python.
    - [Apache UIMA](http://uima.apache.org/), Unstructured Information Management, and derivative projects such as [YodaQA](https://github.com/brmson/yodaqa).
    - [OpenCog](http://opencog.org/) &ndash; not much obvious immediate info on the web site.
    - [OAQA, Open Advancement of Question Answering Systems](http://oaqa.github.io/)

I explored those from the last to the first, and they looked more promising the further I went towards the top of the list:

The [OAQA GitHub organisation](https://github.com/oaqa) lists numerous repositories which might provide useful staring points for an own project.
The documentation is a bit scant and bumpy, though.

[YodaQA](https://github.com/brmson/yodaqa) looks very interesting, with more documentation, still a bit rough, though.

[DeepDive](http://deepdive.stanford.edu/) looks more interesting still, with professional-looking documentation, tutorials, existing real-world projects making use of it, etc.

I'm raring to get going diving deeper into DeepDive.

<center>
<img src="img/deep_dive_header_logo.png" alt="DeepDive" width="350"/>
</center>

####<a name="8"></a>Building a Revit API Ontology

Obviously, the system will never know more than what you tell it to start with, and teach it as it goes along.

Luckily, this is a so-called <b><i>closed domain</i></b> and thus an easier task because the system can exploit domain-specific knowledge, as opposed to an open domain dealing with questions about nearly anything whatsoever.

I need to assemble a Revit API [ontology](https://en.wikipedia.org/wiki/Ontology_(information_science)).

I can use various sources for that:

- The Revit API help file `RevitAPI.chm` contents, now accessible [online at `revitapidocs.com`](http://www.revitapidocs.com).
- The Revit [Developer Guide](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42).
- [The Building Coder](http://thebuildingcoder.typepad.com) and other blog content.
- Existing Q &amp; A:
    - The [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160).
    - StackOverflow questions tagged with [`revit-api`](http://stackoverflow.com/questions/tagged/revit-api) and [`revitpythonshell`](http://stackoverflow.com/questions/tagged/revitpythonshell).
    - [The Building Coder](http://thebuildingcoder.typepad.com) comments and posts explicitly listing Q &amp; A.
    - Revit API related ADN cases. Luckily, I even have all my cases stored locally in text files...
    - Email.
- Many other sources.

That is normally the biggest job in setting up such a system.

If anyone would like to chip in and help in any way whatsoever, that would be most appreciated, e.g.:

- Suggestions for ontology creating, sources, structure, population, storage, etc.
- Experience or advice in this or related areas.
- Anything else you would like to contribute that you think might help.

