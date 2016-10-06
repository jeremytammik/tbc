<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

- Elitsa Slavova <Slavova@cobuilder.no> Re: Autodesk Forge Research Enquiry

- where will i demo connecting desktop and cloud?
  https://twitter.com/DarickBrokaw/status/783844746418675712?cn=ZmF2b3JpdGVfbWVudGlvbmVkX3VzZXI%3D&refsrc=email 
  Darick Brokaw
  ‏@DarickBrokaw
  @jeremytammik would you be willing to demo this remotely via Web for @dtoug6 Design Technology Orlando, maybe 45mins..? DM me if interested
  @DarickBrokaw @dtoug6 i would love to if i had the time. please attend one of the upcoming public demos
  1. try it yurself at home; most effective: 1. fork, clone, compile install revit add-in, run my web instance; get your autodesk forge app credentials, click the deploy to horoku button;
  2. attend one of the upcoming public demonstrations
  3. questions in public, please: http://thebuildingcoder.typepad.com/blog/about-the-author.html#1

- need for two transactions, need for regen
  http://forums.autodesk.com/t5/revit-api-forum/isolate-elements-on-a-newly-created-view/m-p/6604625
  12169975 [Isolate elements on a newly created view]
  http://forums.autodesk.com/t5/revit-api-forum/isolate-elements-on-a-newly-created-view/m-p/6206355
  
Need to Commit Twice, Roomedit3dv3 and Forge @AutodeskForge #revitapi #3dwebcoder @AutodeskRevit #aec #bim @RTCEvents

Lots of exciting news for today, like every day, once again on the pure Revit API, connecting the desktop and the cloud, and Autodesk Forge
&ndash; Isolating an element in a newly created view requires two transactions
&ndash; Forge research enquiry
&ndash; Connecting desktop and cloud &ndash; Roomedit3dv3 live
&ndash; Could you demo this remotely?
&ndash; Connecting desktop and cloud draft handout
&ndash; Table of contents...

-->

### Need to Commit Twice, Roomedit3dv3 and Forge

Lots of exciting news for today, like every day, once again on the pure Revit API, connecting the desktop and the cloud, and Autodesk Forge:

- [Isolating an element in a newly created view requires two transactions](#2)
- [Forge research enquiry](#3)
- [Connecting desktop and cloud &ndash; Roomedit3dv3 live](#4)
- [Could you demo this remotely?](#5)
- [Connecting desktop and cloud draft handout](#6)
    - [Table of contents](#7)


#### <a name="2"></a>Isolating an Element in a Newly Created View Requires Two Transactions

The aim
of [isolating an element in a newly created view](http://forums.autodesk.com/t5/revit-api-forum/isolate-elements-on-a-newly-created-view/m-p/6206355) was
discussed by Tuuletin, Revitalizer, Scott Wilson, FirstJet and Michal-sk in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) and
resolved by an interesting workaround discovered by Michael Morris, Principal Engineer of the Revit development team.

This is another example in the series of situations with a
special [need to regenerate](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33)
and [handling transactions and transaction groups](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.53):

**Question:** My isolation of an element in a newly created view does not work.
 
I do the following:

1. I have a set of elements to isolate `els = System.Collections.Generic.List(ElementId)`.
2. Then a view is created: `view = ViewSection.CreateSection(doc, viewFamilyTypeId, BoundingBox)`.
3. After that I try to isolate `els`: `view.IsolateElementsTemporary(els)`.

If I open this view, the isolation mode is on (light blue frame is on), but nothing from `els` is isolated. The other view parameters (via `get_Parameter`) are accessible and writeable.

On the other hand, `view.IsolateCategoriesTemporary()` works fine!

What's the problem?

**Answer:** Perhaps you need a call to `document.Regenerate` or `uiDocument.RefreshActiveView`?

**Response:** No, this doesn't help.

More peculiarity: the 'reverse' method, `View.HideElements`, works perfectly!
 
Now, I see no other way to solve my task.

The first collector will include any element in model (including links).

The second collector will include elements to be isolated.

The third collector consists of the first collector with the excluded second collector.

Then, I just hide any element I don't want to be isolated by means of the third collector.
 
Seems like a very bad way to evade this bug...

**Answer:** Are the elements actually visible in the view if you don't enable the isolate?

**Response:** Yes, they are visible.

I create a new view (so, any graphic settings are set to default — all elements are visible), then I want to isolate some elements in this created view...

I have stopped seeking the solution. Maybe it is just a lack of the Revit 2015 API. Hope, this will be fixed in later versions.

**Question 2:** I would like to re-open this issue. It is still happening in Revit 2017. I have created a simple test case with macros embedded to show it
in [isolate_element_in_new_view.rvt](zip/isolate_element_in_new_view.rvt):
 
- `testOne` macro shows that we can isolate a element in the view.
- `testTwo` macro duplicates the Level 1 floorplan view and tries to isolate a Element. But the element is not isolated, unexpected behaviour.

**Answer:** I escalated this thread to an ADN case *12169975 [Isolate elements on a newly created view]* and created an internal development issue *REVIT-99069 [Isolate elements on a newly created view -- 12169975]* for the development team to take a closer look. They say:
 
I can reproduce this issue. When just 1 transaction is used to create the new view and also to isolate the element, then the isolate element fails. 
 
However, I can get the desired behaviour using two separate transactions as follows:
 
- Use a transaction to create the new view. Close transaction.
- Activate the new view. This is important; isolation fails if the view is not active.
- Use 2nd transaction to isolate the wall. Close transaction.
 
Is this an acceptable workaround?
 
Note: I also tried the attached file and macros in our internal development release cannot reproduce the behaviour there.
 
I tested macro `test 2` which uses just 1 transaction to create the new view and isolate the wall, and when the screen updates the single wall is really and truly isolated in the new view.
 
So the workaround described above will not be needed forever.
 
Also, it appears the real issue here is that isolate element only works in the active view. For example, edit macro `testOne` to NOT activate floor plan Level 1. In the UI, open views Level 1 and Level 2 side by side, but make Level 2 be the active view. Run macro `testOne`. Notice that Level 1 view is not updated and the wall is not isolated.

So you need to activate the view before doing `IsolateElementTemporary`.

In fact, this appears to be needed for all uses of `IsolateElementTemporary`, regardless of if the view is newly created or not.
 

**Response:** Thanks for the work around.

I tried two transactions myself as well before, but it didn't work then.

The trick is, as you guys described, to set the active view to the new created view.
 
Here is the working snippet for those interested:
 
<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;testTwo(&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;newView;
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Trans&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;Floorplan&nbsp;for&nbsp;Level1&nbsp;and&nbsp;copy&nbsp;its&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;properties&nbsp;for&nbsp;ouw&nbsp;newly&nbsp;to&nbsp;create&nbsp;ViewPlan.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;existingView&nbsp;=&nbsp;doc.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;312&nbsp;)&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">View</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;new&nbsp;Floorplan.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;newView&nbsp;=&nbsp;doc.GetElement(&nbsp;existingView.Duplicate(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ViewDuplicateOption</span>.Duplicate&nbsp;)&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">View</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Important&nbsp;to&nbsp;set&nbsp;new&nbsp;view&nbsp;as&nbsp;active&nbsp;view.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;uidoc.ActiveView&nbsp;=&nbsp;newView;
 
&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Trans&nbsp;2&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Try&nbsp;to&nbsp;isolate&nbsp;a&nbsp;Wall.&nbsp;Fails.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;newView.IsolateElementTemporary(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;317443&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Change&nbsp;the&nbsp;View&nbsp;to&nbsp;the&nbsp;new&nbsp;View.</span>
 
&nbsp;&nbsp;uidoc.ActiveView&nbsp;=&nbsp;newView;
}
</pre>

It somewhat sucks having to commit twice and therefore having to regenerate twice. But the workaround is solid and does the job. Thanks guys!

Many thanks indeed to everybody involved in this exploration, especially to Michael for his in-depth research and effective workaround!


#### <a name="3"></a>Forge Research Enquiry

An example of the many daily requests I receive and try to solve as efficiently and publicly as possible.

I am sorry that I cannot answer each issue in private and thank you for your understanding!

**Question:** I am conducting research related to Autodesk Forge and how we can collaborate with it.

I have several questions with regard to Forge. 

Can we arrange a Skype conversation?

**Answer:** I avoid one-on-one conversations where possible, since most questions I encounter are of general interest to a large number of people.
 
Could you please first of all consult the existing online documentation, and then formulate your questions you have in writing?
 
- [forge.autodesk.com](https://forge.autodesk.com)
- [developer.autodesk.com](https://developer.autodesk.com)
 
With your concrete questions in hand, I will happily take them up with you personally or direct you to one of my colleagues.
 
Thank you for your understanding!

**Response:** I have already checked the websites. Here are my questions:

1. Since the Forge platform is a set of cloud services: if I am developing an application for the desktop, e.g., Revit or NavisWorks, will this mean that the Forge Platform will not be relevant?
2. Could you give some examples of companies/and apps that utilize the Data Management API? By using this API, can I access data from Revit via A360?
3. Can the Model Derivative API translate the design into IFC format? Does it support IFC 4? Does it support the buildingSMART International standards for Model View Definitions, like reference view, design transfer view etc.?
4. Can the Viewer render only SVF file format?
5. Can you apply for the Forge Funding only if you are building apps using the Forge platform web services?

**Answer:** Thank you very much for your pertinent questions.
 
I discussed them with my colleagues, and we put together the following answers for you:
 
1. Forge may be extremely important for a Revit or NavisWorks add-in or other desktop application.
For instance, if your Revit add-in needs to use A360, or connect with any of your own or the Autodesk or other web services, Forge may be very important indeed.
The number of web services is growing daily, and you should urgently consider implementing your own functionality along those lines as well.
That is the whole point of my presentation
on [freeing your BIM data](http://thebuildingcoder.typepad.com/blog/2016/06/free-your-bim-data-and-roomedit3d-thee-legged-forge-oauth.html) that
I held at the Forge Developer Conference in June.
You may want to make use of cloud services to address a larger audience with your application, and yet connect it back to the Revit BIM, cf. my newest sample on [connecting the desktop and the cloud](http://thebuildingcoder.typepad.com/blog/2016/10/roomedit3dv3-up-and-running-with-demo-recording.html).
2. If a CAD model is uploaded to A360 in a way that the references can be resolved, so it becomes viewable in the A360 UI, then it can be translated into SVF as well (in A360 this happens automatically), which will also trigger property generation for it that can be accessed through the Model Derivative endpoints as well as from the Viewer. 
Anyone uploading a file to translate for the viewer is using the Data Management API.
Uploading a file is a necessary part of the translation service.
Look at the Forge samples on GitHub and the Forge DevCon presentations:
    - [github.com/Developer-Autodesk](https://github.com/Developer-Autodesk)
    - [github.com/Autodesk-Forge](https://github.com/Autodesk-Forge)
    - [forge.autodesk.com/devcon-2016](https://forge.autodesk.com/devcon-2016)
3. Not yet.
The other way around is supported, IFC to Forge.
We are in the process of adding IFC support to the MD API right now.
Revit itself offers IFC4 support.  What that means right now is:
    - We generate IFC4 Reference View files.  These aren’t certified, because certification doesn’t exist yet.  We continue to improve our output so that our output passes certification, when it officially begins.
    - We generate IFC4 Design Transfer View files.  These aren’t certified, because certification doesn’t exist yet.  Furthermore, there is still some discussion about what would be certified, which may lead to further modifications of this MVD.  We will continue to improve our output so that our output passes certification, when it officially begins and is finalized.
    - For extraction, it is intended to work as well as Revit itself.
4. `SVF` for 3D and `F2D` for 2D views.
The render format depends on the input format and its extension (`obj`, `glTF`, etc.).
Different formats may interact slightly differently in the viewer regarding selection, metadata, etc.
The viewer is primarily intended to read the single file format (what we generically refer to as SVF for both 2D and 3D models).
The Model Derivative API allows you to convert all other formats into SVF.
The viewer is not intended to be a standalone multi-format viewer &ndash; it is intended to be used in conjunction with the Model Derivative API.
5. You can apply without necessarily using Forge API.
You should definitely mention what Forge APIs and how you are planning to use them.
 
I hope this helps.
 

#### <a name="4"></a>Connecting Desktop and Cloud &ndash; Roomedit3dv3 Live

On popular request, here are the various upcoming events at which I will be presenting on the topics
of [freeing your BIM data](http://thebuildingcoder.typepad.com/blog/2016/06/free-your-bim-data-and-roomedit3d-thee-legged-forge-oauth.html)
and [Connecting the desktop and the cloud with a live roomedit3dv3 demonstration](http://thebuildingcoder.typepad.com/blog/2016/10/roomedit3dv3-up-and-running-with-demo-recording.html):

- Oct. 18-19 &ndash; Forge and BIM, [ISEPBIM](https://www.facebook.com/ISEPBIM) at [ISEP](http://www.isep.ipp.pt), Porto University  
- Oct. 20-22 &ndash; [RTCEU Revit Technology Conference Europe](http://www.rtcevents.com/rtc2016eur), Porto
- Oct. 24-28 &ndash; [Forge Accelerator](http://autodeskcloudaccelerator.com), Munich
- Nov. 4 &ndash; Forge and BIM Workshop, [Darmstadt University](http://www.tu-darmstadt.de)
- Nov. 14-17 &ndash; [Autodesk University](http://au.autodesk.com), Las Vegas
- Dec. 5 &ndash; [DevDay Europe](http://autodeskdevdays.com/devdays-2016), Munich
- Dec. 6-9 &ndash; [DevDay Forge Accelerator](http://autodeskdevdays.com/accelerator), Munich

I hope to be able to meet you in person at one of these.


#### <a name="5"></a>Could You Demo this Remotely?

I completed the [roomedit3dv3](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d) project and published
a [demo recording](http://thebuildingcoder.typepad.com/blog/2016/10/roomedit3dv3-up-and-running-with-demo-recording.html) to show it in action.

One response via twitter includes another request for a non-public answer, which I am sorry to say I am unable to satisfy:

**Question:** Would you be willing to demo this remotely via web, maybe 45 mins...? 

**Answer:** I would love to if I had the time to spare.

Unfortunately, I am rather busy trying to answer as many questions as possible for the whole community.

Therefore, I always [prefer to discuss everything I do in public](http://thebuildingcoder.typepad.com/blog/about-the-author.html#1) and
enable the entire community to contribute and share when possible.

Here are the most effective ways to see the demo live and explore it in further depth yourself:

1. Attend one of the demos at the [upcoming public events listed above](#3).
2. Try it for yourself at home; that is probably the most effective approach of all.
You can fork, clone, compile and install
the [Roomedit3dApp](https://github.com/jeremytammik/Roomedit3dApp) Revit add-in and either run my
existing [web instance roomedit3dv3](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d),
or [get your own Autodesk forge app credentials](https://developer.autodesk.com) and use those
to [deploy your own Heroku instance](https://github.com/Autodesk-Forge/forge-boilers.nodejs/tree/roomedit3d#prerequisites-and-sample-setup).

As I keep saying all too often now, please ask all your questions in public, e.g., Revit API related ones in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160)
and [all Forge related issues on StackOverflow](https://developer.autodesk.com/en/support/get-help).


#### <a name="6"></a>Connecting Desktop and Cloud Draft Handout

I completed an initial version of the handout document for my presentation on connecting the desktop and the cloud at 
the [RTC Revit Technology Conference Europe](http://www.rtcevents.com/rtc2016eur) in Porto the week after next.

Here is the complete PDF handout document [s1_4_hand_connect_desktop_cloud_jtammik.pdf](/a/doc/revit/rtc/2016/doc/s1_4_hand_connect_desktop_cloud_jtammik.pdf).

For your convenience, I extracted this sneak preview of the class description and table of contents:

#### <a name="7"></a>Session 1.4 &ndash; Connecting any Desktop Application or Revit Add-in with Autodesk Forge and the Cloud

Class Description: We discuss the opportunities and advantages offered by accessing and interacting with BIM from the cloud, using both open source and Autodesk Forge web services and cloud-based APIs. We show how they can be accessed and used from any desktop application, including your Revit custom add-in. This provides access to powerful functionality to design, visualise, collaborate, make and use BIM and other CAD data. I present a Forge overview and my suite of examples connecting the desktop and the cloud, culminating in the newest 3D Forge round-trip real-time room editor.

Table of Contents:

- Introduction
- Free Your BIM Data!
- What Cloud? Private Versus Public; Security!
- Message, Takeaway and Sample Overview
- The 2D Cloud-Based Round-Trip Room Editor
- Software Architecture Connecting Desktop and Cloud
- NoSQL Databases, CAP Theorem and ACID vs BASE
- FireRating in the Cloud
- Revit Add-In C REST Client
- Nodejs MongoDB Web Server
- Autodesk Forge
- Forge-Based 2D + 3D Round-Trip BIM Editor
- Samples Connecting Desktop and Cloud
- Conclusion
- Learning More

I hope this is of interest and use to you!

Now I am really looking forward to a week's vacation before RTC!

<center>
<img src="img/RTC-EUR-2016-240w.png" alt="RTC Europe 2016" width="240">
</center>
