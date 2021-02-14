<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- How to draw transient graphics in Revit
  https://forums.autodesk.com/t5/revit-api-forum/how-to-draw-transient-graphics-in-revit/td-p/10000065/jump-to/first-unread-message

- The human side of AI for chess
  https://www.microsoft.com/en-us/research/blog/the-human-side-of-ai-for-chess/

- An Engineering Argument for Basic Income
  https://scottsantens.com/engineering-argument-for-unconditional-universal-basic-income-ubi-fault-tolerance-graceful-failure-redundancy
  Utilizing fault-tolerant design in critical life support systems

- Assessing Mandatory Stay-at-Home and Business Closure Effects on the Spread of COVID-19
  https://onlinelibrary.wiley.com/doi/10.1111/eci.13484

- Stanford Studie mit Top Medizin-Wissenschaftler Ioannidis zeigt keinen Nutzen von Lockdowns
  https://tkp.at/2021/01/11/stanford-studie-mit-top-medizin-wissenschaftler-ioannidis-zeigt-keinen-nutzen-von-lockdowns/

twitter:

AI emulating human-style chess, basic income as fault-tolerant engineering, lockdown effectivity and transient graphics with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://autode.sk/transientgfx

A quick look at various methods to integrate transient graphics into Revit and pointers to articles of interest on other current non-API-related topics such as chess AI, basic income and the effectiveness of lockdowns
&ndash; Transient graphics
&ndash; AI learns to emulate human-style chess
&ndash; Basic income as fault-tolerant engineering
&ndash; Lockdown effectivity...

linkedin:

AI emulating human-style chess, basic income as fault-tolerant engineering, lockdown effectivity and transient graphics with the #RevitAPI

http://autode.sk/transientgfx

A quick look at various methods to integrate transient graphics into Revit and pointers to articles of interest on other current non-API-related topics such as chess AI, basic income and the effectiveness of lockdowns:

- Transient graphics
- AI learns to emulate human-style chess
- Basic income as fault-tolerant engineering
- Lockdown effectivity...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk 

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### Transient Graphics, Humane AI, BI and Lockdown

Today, I take a quick look at various methods to integrate transient graphics into Revit and point to articles of interest on other current non-API-related topics such as chess AI, basic income and the effectiveness of lockdowns:

- [Transient graphics](#2)
- [AI learns to emulate human-style chess](#3)
- [Basic income as fault-tolerant engineering](#4)
- [Lockdown effectivity](#5)

####<a name="2"></a> Transient Graphics

Here is an answer I put together to address 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to draw transient graphics in Revit](https://forums.autodesk.com/t5/revit-api-forum/how-to-draw-transient-graphics-in-revit/m-p/10000065):

**Question:** I want to draw 2D texts in a view, like Transient Graphics in AutoCAD.
These texts draw upon all model elements and will not adapt their size when scaling the view.
This can be applied to a situation such as when the grid names are outside of the view, the texts representing the grids display at the edge of view, so the user can easily know the current position while modelling.

**Answer:** Two pieces of Revit API functionality come to mind: the older [Analysis Visualisation Framework or AVF](https://thebuildingcoder.typepad.com/blog/avf) and the more recent DirectContext3D.

AVF is designed for displaying structural and other analysis results on the BIM element faces.
The Building Coder has presented numerous examples in the past, starting with the decade-old [Revit webcam](https://thebuildingcoder.typepad.com/blog/2012/02/revit-webcam-2012.html).

DirectContext3D is for displaying external graphics in the context of a Revit model:

- The [`IDirectContext3DServer` interface](https://www.revitapidocs.com/2020/7709521d-9954-ef80-1f13-3bc6ee660d5d.htm) for
displaying arbitrary 3D graphics in Revit, [introduced in Revit 2018](https://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html#3.26)
- The [DuplicateGraphics SDK sample](https://thebuildingcoder.typepad.com/blog/2017/05/revit-2017-and-2018-sdk-samples.html#4.2) demonstrates
basic usage
- How to [draw or render over the active view](https://forums.autodesk.com/t5/revit-api-forum/draw-render-over-the-activeview/m-p/7074503)
- [DirectContext Rectangle Jig](https://thebuildingcoder.typepad.com/blog/2020/10/onbox-directcontext-jig-and-no-cdn.html#3) example
- [IDirectContext3DServer and scene size](https://forums.autodesk.com/t5/revit-api-forum/idirectcontext3dserver-and-scene-size/m-p/9939322)

For your use case, however, I can well imagine that a simpler and more effective solution can be implemented independently of Revit and its API, by making use of the native Windows API or .NET libraries.
However, that lies outside my area of expertise, so I have no specific advice to offer in that area.
Except: maybe you can simply use the [`DrawText` function from `winuser.h`](https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-drawtext).

<center>
<img src="img/andy_goldsworthy_leaves.jpg" alt="Transient nature art by Andy Goldsworthy" title="Transient nature art by Andy Goldsworthy" width="400"/> <!-- 800 -->
</center>

####<a name="3"></a> AI Learns to Emulate Human-Style Chess

The Microsoft Research Blog published an article
on [the human side of AI for chess](https://www.microsoft.com/en-us/research/blog/the-human-side-of-ai-for-chess) demonstrating
how AI can be taught to emulate human moves rather than the 'non-human' style developed by pure self-learning AI implementations.

####<a name="4"></a> Basic Income as Fault-Tolerant Engineering 

Scott Santens
presents [an engineering argument for basic income](https://scottsantens.com/engineering-argument-for-unconditional-universal-basic-income-ubi-fault-tolerance-graceful-failure-redundancy) based
on the fundamental engineering principle of utilising fault-tolerant design in critical life support systems.

####<a name="5"></a> Lockdown Effectivity

Lockdown effectivity is scientifically analysed in a peer-reviewed article in
the European Journal of Clinical Investigation (EJCI)
on [Assessing Mandatory Stay-at-Home and Business Closure Effects on the Spread of COVID-19](https://onlinelibrary.wiley.com/doi/10.1111/eci.13484),
concluding that:

> While small benefits cannot be excluded, we do not find significant benefits on case growth of more restrictive NPIs (nonpharmaceutical interventions).
Similar reductions in case growth may be achievable with less restrictive interventions.

<!--
Stanford Studie mit Top Medizin-Wissenschaftler Ioannidis zeigt keinen Nutzen von Lockdowns
https://tkp.at/2021/01/11/stanford-studie-mit-top-medizin-wissenschaftler-ioannidis-zeigt-keinen-nutzen-von-lockdowns/
-->
