<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

<code></code>

#RTCEUR Revit API Panel, Idea Station, Edit and Continue @RTCEvents @AutodeskForge #revitapi @AutodeskRevit #aec #bim

This morning saw some lively discussion in the Revit API Panel at the RTC Revit Technology Conference Europe 
&ndash; Panel Members
&ndash; Questions and Answers
&ndash; Notes from Previous Revit API Panel Sessions
&ndash; Session Materials...

-->

### RTC Revit API Panel, Idea Station, Edit and Continue

This morning saw some lively discussion in the Revit API Panel at
the [RTC Revit Technology Conference Europe](http://www.rtcevents.com/rtc2016eur):

- [Panel Members](#2)
- [Questions and Answers](#3)
- [Notes from Previous Revit API Panel Sessions](#4)
- [Session Materials](#5)

Before diving into that, here
are [some pictures](https://flic.kr/s/aHskM6z9Y4) from
the conference building window overlooking the Douro River yesterday and during the walk to get here this morning:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157675499863595" title="Views around RTC"><img src="https://c8.staticflickr.com/6/5739/29836760303_27c1e0ac14_n.jpg" width="320" height="240" alt="Views around RTC"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

#### <a name="2"></a>Panel Members

- Harry Mattison
- Sasha Crotty
- Jeremy Tammik

I was lucky enough to have Sasha Crotty and Harry Mattison as panel members.

Sasha is the product manager responsible for the Revit API.

Harry is [BoostYourBIM](https://boostyourbim.wordpress.com)
([Twitter](https://twitter.com/BoostYourBIM)),
an independent Revit API teacher, add-in developer, consultant, and previously also a member of the Autodesk Revit development team.

This represents an ideal combination, bringing in-depth knowledge both from inside the Revit development team and from the point of view of an independent external software developer.

#### <a name="3"></a>Questions and Answers

<b>[Q]:</b> How to combine BIM with SmartCity? Missing piping functionality? Tools to manage sewerage?

<b>[A]:</b> We are implementing generic functionality. We cannot currently invest in all areas at the same time. Keep telling us what you need. That helps drive the development effort in the right direction.

<b>[Q]:</b> Does the Revit API support inserting a family instance driven from an external database?

<b>[A]:</b> Yes, the Revit SDK includes several samples that demonstrate that kind of functionality, and The Building Coder blog discusses many more. 

<b>[Q]:</b> It would be nice to be able to place instances programmatically and still include more interaction, e.g., programmatically select the family and then enable the user to interactively define the exact rotation. This is currently really hard or even impossible to do through the API. You can prompt for points, but you cannot currently display temporary graphics to show what the final instance placement will look like. 

<b>[A]:</b> Please put a wish like this on the Revit Idea Station and ensure it gets a lot of votes:

- [Revit Idea Station](http://forums.autodesk.com/t5/revit-ideas/idb-p/302/)
- [Tag: API](http://forums.autodesk.com/t5/tag/API/tg-p/board-id/302)
- [Introducing the Revit Idea Station](http://thebuildingcoder.typepad.com/blog/2016/05/idea-station-and-textnote-bounding-box.html#2)

The idea station currently lists over 1300 wishes. We need votes to help prioritise them.

<b>[Q]:</b> Colouring scheme API?

<b>[A]:</b> Put it on the idea station and ensure it gets a lot of votes. We implement things starting at the top of the list. We have strategic directions in which we need to go on one hand, and developer requests on the other, which is quite challenging.

Discussion on the idea station, and existing wish list items…

We have not yet transferred any existing wish list items from the Revit development database to the Revit idea station.

Please submit your wish list items to idea station as well.

There are currently only 22 Revit API related idea station issues.

If you submit a request there that you already submitted as a wish list item, please include the ADN case number, Revit API discussion forum thread URL, the development issue number, e.g., CF-XXXX, etc.

That will help coordinate. There is no need to resubmit all the sample material.
Things are moving forward. Lately, several important issues were resolved, some of them really fast.

<b>[Q]:</b> Is there any difference between programming in Dynamo and the pure Revit API?

<b>[A]:</b> No, not really, as far as Revit is concerned. Dynamo is just a wrapper on top of the Revit API via the .NET framework. Dynamo does include other additional exciting functionality that has nothing to do with Revit such as DesignScript and its own geometry kernel. There are currently two versions of Dynamo: a standalone one and Dynamo for Revit, containing Revit-specific nodes and interactions.

<b>[Q]:</b> How does Python interact with Revit?

<b>[A]:</b> Python interaction is implemented using IronPython, which connects the Python engine with the .NET framework and thus provides access to the same, standard, one and only Revit API.

<b>[Q]:</b> Can I access the .NET framework libraries such as LYNC from Python? 

<b>[A]:</b> The .NET framework comes with one bunch of libraries, Python comes with a completely different huge set. If you are using Python to interact with Revit, your Python code is on the Python side of the IronPython .NET wrapper and the .NET libraries on the other.

<b>[Q]:</b> Can I use my existing C# Revit add-in functionality together with Dynamo?

<b>[A]:</b> You could to implement your own Dynamo nodes to provide such an access. Alternatively, define some other more convenient place to hook up with existing C# library functionality. Dynamo is completely open source. Please extend Dynamo yourself to implement support for anything that is important to you.

<b>[Q]:</b> Renaming a shared parameter. I implemented a workaround using a temporary shared parameter, deleting the old, adding the new… Is there an easier way, or could one be implemented?

<b>[A]:</b> Be very careful using a workaround like that. You could cause serious problems for yourself. Using the same GUID with a different name can cause serious problems. Multi-lingual apps currently should use separate GUIDs for separate parameter names. We are thinking about implementing an improved global cloud-based shared parameter solution as a web service. That would allow renaming, multiple names for different languages, sharing between companies, global definitions shared across the entire world, etc. You can implement an interim solution as a bridge until this happens. Harry implemented a SQL database solution for handling several of these issues which avoids the need of managing hundreds of shared parameter files. 

<b>[Q]:</b> Is there a public roadmap for API changes?

<b>[A]:</b> We don’t have anything API specific. We published an official public Revit product development roadmap yesterday:

<center>
[www.autodesk.com/revitroadmap](http://www.autodesk.com/revitroadmap)
</center>

<b>[Q]:</b> Purge?

<b>[A]:</b> Harry published some purge code on his
blog [BoostYourBIM](https://boostyourbim.wordpress.com) yesterday
implementing [#RTCEUR API Wish #1: Purging Types and Families](https://boostyourbim.wordpress.com/2016/10/20/rtceur-api-wish-1-purging-types-and-families/).

<b>[Q]:</b> Plans to make Revit warnings more accessible?

<b>[A]:</b> Yes, that access has been implemented in response to wish list item *<b>CF-2116</b> API ability to retrieve Revit Warnings like Manage tab--Inquiry pane—Warnings* raised for the Revit API discussion forum thread on [warning information](http://forums.autodesk.com/t5/revit-api/warning-information/m-p/5450023) for possible release in a future version, as a property on the Revit document class.

<b>[Q]:</b> How can I see what was implemented in the new release, or in an intermediate pre-release?

<b>[A]:</b> We have a beta forum for future development access that provides that kind of information. The Revit SDK is published in intermediate versions during the pre-release phase. That information is updated as we go along and ends up as the final ‘What’s New’ information.

<b>[Q]:</b> How to debug a Revit command without relaunching?

<b>[A]:</b> One simple possibility is to implement your code as a macro, debug, then convert to an add-in. Several other possibilities are discussed in The Building Coder topic group 5.49
on [Edit and Continue, Debug without Restart, Live Development](http://thebuildingcoder.typepad.com/blog/about-the-author.html - 5.49).

Alternatively, use the AddIn Manager. If your add-in is loaded by the AddIn Manager, you can debug it in Visual Studio using ‘Attach to’ the Revit.exe process. That enables you to recompile and reload updated code without restarting Revit.

<b>[Q]:</b> What is the process to convert code between a macro and an add-in?

<b>[A]:</b> The changes are pretty small. Suggestion: always pass in the document as an argument. Then the macro can pass in this.Document to it, and the add-in can use the document instance obtained from the command data argument passed into the Execute method. 

<b>[Q]:</b> The debugging situation in SharpDevelop could be improved a little bit…

<b>[A]:</b> Aha.

<b>[Q]:</b> Can you launch a Dynamo script programmatically?

<b>[A]:</b> Yes, you may have to twiddle things. Everything happening between Dynamo and Revit is based on .NET and completely open source.
Q; Can I specify the Revit Add-In folder location?

<b>[A]:</b> No. Revit specifies two locations that cannot be changed. You have to place the add-in manifest `addin` file in one of those. The add-in .NET assembly DLLs can be placed anywhere you like.

<b>[Q]:</b> How to deploy add-ins for other Autodesk products like Robot, Navisworks, etc.?

<b>[A]:</b> You can use the bundle folder supported by the AppStore installation. Look where the AppStore installer puts things.

<b>[Q]:</b> Can we combine our add-in installer with the Revit installer?

<b>[A]:</b> Discuss that with the AppStore team, please. They have more installer expertise than we do.

#### <a name="4"></a>Notes from Previous Revit API Panel Sessions

Here are some notes from previous similar Revit API panel sessions:

- [SD5156 &ndash; The Revit API Expert Panel at Autodesk University 2014](http://thebuildingcoder.typepad.com/blog/2014/12/the-revit-api-panel-at-autodesk-university.html#1)
- [The Building Coder Revit API Panel at the Revit Technology Conference RTC Europe 2015](http://thebuildingcoder.typepad.com/blog/2015/11/rtc-budapest-and-the-revit-api-panel.html#5)
- [SD10181 &ndash; Revit API Expert Roundtable at Autodesk University 2015](http://thebuildingcoder.typepad.com/blog/2015/12/au-keynote-and-revit-api-panel.html#9)



#### <a name="5"></a>Session Materials

Here are the PDF versions of the session materials:

- [Slide deck](/a/doc/revit/rtc/2016/doc/s2_1_pres_revit_api_tbc_jtammik.pdf)
- [Handout document](/a/doc/revit/rtc/2016/doc/s2_1_hand_revit_api_tbc_jtammik.pdf)

