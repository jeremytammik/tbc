<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

-  [Register for the Live webinar *AEC Collaboration: BIM Collaborate for Project Managers*](https://forums.autodesk.com/t5/revit-api-forum/register-for-the-live-webinar-aec-collaboration-bim-collaborate/m-p/10462552)

twitter:

add #thebuildingcoder

#RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


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

### AEC Collaboration



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

Jason Masters addad a [comment on how he solved the conflict by disentanglement](https://archi-lab.net/webview2-and-revits-dockable-panel/#comment-2813), like the suggestion to achieve
[Disentanglement and Independence via IPC](https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html#6), and adding:

> It’s so frustrating because DLL hell was solved by Microsoft, like 15 years ago, using strong naming, but Autodesk just doesn’t support it.

> Personally, I just use Electron, built my whole client application there, and just shuttle `json` data back and forth to a thin Revit wrapper over named pipes.
Still, the potential for DLL conflicts with different versions of `Newtonsoft.json`, but thankfully its core api has stayed pretty stable and consistent.

####<a name="4"></a> Dismissing Revit Pop-Ups

Another article by Konrad
discusses [dismissing Revit pop-ups &ndash; the easy and not so easy ways](https://archi-lab.net/dismissing-revit-pop-ups-the-easy-and-not-so-easy-ways) and
explains

- How to set up and use the `DialogBoxShowing` event for the Revit-API-style solution, as well as,
- Using the Win32Api `FindWindow` and `GetWindowText` methods to find the right button and simulate a user click on it




<pre class="code">
</pre>







