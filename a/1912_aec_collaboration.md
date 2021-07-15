<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

-  [Register for the Live webinar *AEC Collaboration: BIM Collaborate for Project Managers*](https://forums.autodesk.com/t5/revit-api-forum/register-for-the-live-webinar-aec-collaboration-bim-collaborate/m-p/10462552)

- decompile revitapi.dll
  Orson Liu -- https://autodesk.slack.com/archives/C0SR6NAP8/p1624975627139600

- railing geometry -- https://autodesk.slack.com/archives/C0SR6NAP8/p1624977919139900
  Stephen Smith

twitter:

add #thebuildingcoder

An invitation to the upcoming AEC collaboration webinar and overviews over dockable panels with and without WebView2, dialogue handling, decompilation and railing geometry in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/aeccollaboration

An invitation to the upcoming AEC collaboration webinar and overviews over dockable panels, dialogue handling, decompilation and railing geometry
&ndash; AEC collaboration webinar
&ndash; Dockable panels and <code>WebView2</code>
&ndash; Dismissing Revit pop-ups
&ndash; Check API changes using decompilation
&ndash; Railing geometry...

linkedin:

An invitation to the upcoming AEC collaboration webinar and overviews over dockable panels with and without WebView2, dialogue handling, decompilation and railing geometry in the #RevitAPI

https://autode.sk/aeccollaboration

- AEC collaboration webinar
- Dockable panels and <code>WebView2</code>
- Dismissing Revit pop-ups
- Check API changes using decompilation
- Railing geometry...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Question:** 

**Answer:**

**Response:**  

Many thanks to  for this very helpful explanation!

-->

### Collaboration, Dockables, Railings, Pop-Ups, ILSpy

Here is an invitation to the upcoming AEC collaboration webinar and overviews over dockable panels, dialogue handling, decompilation and railing geometry:

- [AEC collaboration webinar](#2)
- [Dockable panels and `WebView2`](#3)
- [Dismissing Revit pop-ups](#4)
- [Check API changes using decompilation](#5)
- [Railing geometry](#6)

<center>
<img src="img/aec_collaboration_webinar.png" alt="AEC Collaboration: BIM Collaborate for Project Managers" title="AEC Collaboration: BIM Collaborate for Project Managers" width="600"/> <!-- 999 -->
</center>

####<a name="2"></a> AEC Collaboration Webinar

Interested in AEC collaboration and coordination?

We have a one-hour live webinar
on [AEC Collaboration: BIM Collaborate for Project Managers](https://www.autodesk.com/webinars/aec/bim-collaborate-for-project-managers) coming
up on [July 28, 2021 at 10am PT / 1pm ET / 19:00 CET](https://www.timeanddate.com/worldclock/converter.html?iso=20210728T170000&p1=tz_pt&p2=tz_et&p3=tz_cest).

Take your work anywhere, see your design progress clearly and work flexibly without disruption.

Join Autodesk Technical Specialists as they guide you into workflows supported by Autodesk BIM Collaborate. Learn to use analytical tools and reports to make data driven decisions for your project, see an overview of Insight and see how AI can help keep projects on track.

Learn the power of automated clash detection of Model Coordination, how to create and manage issues for coordination and explore other tools that save time and improve change visibility in your models; no Revit skills necessary.

Our experts will cover:

- Reports
- Insight and Design Risk dashboard
- Model Coordination
- Meetings
- Change visualization tracker

[Link to registration](https://www.autodesk.com/webinars/aec/bim-collaborate-for-project-managers).

Can't attend live?
[Register](https://www.autodesk.com/webinars/aec/bim-collaborate-for-project-managers) anyway
and we'll send you the recording after the webinar.

####<a name="3"></a> Dockable Panels and WebView2

Konrad Sobon presents a very nice general introduction get started with dockable panels in his article
on [WebView2 and Revit’s Dockable Panel](https://archi-lab.net/webview2-and-revits-dockable-panel).

Unfortunately, he runs into a problem using `WebView2` to host a browser in them.

Many thanks to Konrad for the nice introduction to dockable panels and subsequent problem analysis!

Jason Masters explained [how he solved the conflict using named pipes to connect to a separate UI process](https://archi-lab.net/webview2-and-revits-dockable-panel/#comment-2813), similar to the suggestion to achieve
[disentanglement and independence via IPC](https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html#6):

> It’s so frustrating, because DLL hell was solved by Microsoft, like 15 years ago, using strong naming, but Autodesk just doesn’t support it.

> Personally, I just use Electron, built my whole client application there, and just shuttle `json` data back and forth to a thin Revit wrapper over named pipes.
Still, the potential for DLL conflicts with different versions of `Newtonsoft.json` remains, but thankfully its core API has stayed pretty stable and consistent.

####<a name="4"></a> Dismissing Revit Pop-Ups

Another article by Konrad
discusses [dismissing Revit pop-ups &ndash; the easy and not so easy ways](https://archi-lab.net/dismissing-revit-pop-ups-the-easy-and-not-so-easy-ways) and
explains

- How to set up and use the `DialogBoxShowing` event for the Revit-API-style solution
- Using the Win32Api `FindWindow` and `GetWindowText` methods to find the right button and simulate a user click on it

Yet again many thanks to Konrad for this helpful overview!

This explanation nicely complements the existing articles in the topic group
5.32 on [detecting and handling dialogues and failures](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.32).

####<a name="5"></a> Check API Changes using Decompilation

Comparing changes between different versions of the Revit API is possible using
the free [ILSpy tool](https://github.com/icsharpcode/ILSpy) (e.g., version 5.0.0.4861-preview3)
to:

- Open the `RevitAPI.dll` assembly
- Select the root node
- Select File &gt; Save Code...
- Select an empty folder to save all the decompiled source code
- Create a git repo 
- Save the API source code

Repeat this process with another version of `RevitAPI.dll` to generate the decompiled source code, commit into the git repo to compare the different releases and the changes between them.

####<a name="6"></a> Railing Geometry

Some useful aspects of the tricky task of accessing railing geometry can be gleaned from these snippets of an internal discussion:

Q: How can I extract the geometry from a `RailingType` element?

A: Last time I tried, the best way to get geometry was using the `IModelExportContext`, but I forget whether a railing type actually has geometry one can get from API or if it's all on the railing.

There is also the `Element.Geometry` property, `GeometryElement` and `GeometryInstance` objects that can be obtained directly, but in case of railings it could have some specific complications.

Exactly, railings have a complicated representation and I have no idea what Element.get_Geometry actually gives for a RailingType.
So, I'd need to look into an example and see what they give and what's best to use.

For `RailingType` elements, `get_Geometry` returns null.
I then used `GetDependentElements` with a filter for `Railing` elements.
I can then call `get_Geometry` and/or `GetGeometryInstances` on the `Railing` element.
Is this a valid workflow?
Also, I would like to use `GetGeometryInstances` on the `Railing` element, but it is returning an identity transform; so, it is not using instanced geometry?

Well, a railing has its own RailingType, so the railing being an identity transform of something does make sense regardless of where the railing is positioned.

The issue is that the railing element is instanced 600 places in the model.
I thought I would be able to get the instance geometry and it's transform.

I'm confused &ndash; railings aren't instanced.

I think I answered my own question.
The RailingType has a dependent Railing element.
This Railing element has a GeometryInstance in its GeometryElement.
I can get the geometry and its transform from this element.

That sounds right &ndash; a Railing geometrically should be small, one or a few GeometryInstances would be expected.

There is some potential difference with the continuous rails; these are separate Elements but you may see them as part of the Railing's geometry.
In that case, it's also easy to encounter the continuous rails twice; in fact, Revit draws them twice, which is why you can tab select a top rail, for example, to toggle between the top rail alone and the railing which its attached to.
