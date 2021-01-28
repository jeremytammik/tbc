<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- .NET Core
  https://autodesk.slack.com/archives/C0SR6NAP8/p1605727684149900
  [Q] Rahul Bhobe 18 Nov 2020 at 20:28
  A question on my AU class page: Will we be able to use the Revit API with .NET 5? Do we have a blog post announcement on this?
  [A] Scott Conover
  We should try to be prepared to answer.  So far, there are complications.  Revit consumes Autodesk wide .NET components.  We'd need to ensure that those are .NET 5 compatible before we switch our runtime.  Revit API also runs in-process using Revit's runtime, so I'm not sure it would be possible to preserve Revit's 4.8 runtime and allow add-in code to run .NET 5.0.   We have not tested either scenario yet.  Any other thoughts @Jan Richter or others on this thread?
  [A] Jan Richter
  Basically, they will be able to reference Revit's dlls (.NET 4.8) in their .NET 5 projects, but there is no guarantee that everything will work. Some (if not most) things might, but I would not recommend going that way. The problem is that .NET 5 is based on .NET Core, not on the big .NET framework, and there are some incompatibilities.
  As for switching Revit to .NET 5, that's something we will definitely need to do as .NET 4.8 is the last version of the big .NET. However, the switch is not as trivial as changing the version dropdown (like it was from 4.7 to 4.8). We will have to convert to a new project format, fix some code and possibly find replacement for some frameworks that were present in .NET 4.8, but are not anymore in .NET 5. (edited)
  https://forums.autodesk.com/t5/revit-api-forum/does-revit-target-net-standard/m-p/10021984
  [Q] Will we be able to use the Revit API with .NET 5?
  [A] The factory should try to be prepared to answer. So far, there are complications. Revit consumes Autodesk-wide .NET components. We'd need to ensure that those are .NET 5 compatible before we switch our runtime. Revit API also runs in-process using Revit's runtime, so I'm not sure it would be possible to preserve Revit's 4.8 runtime and allow add-in code to run .NET 5.0. We have not tested either scenario yet.
  Basically, developers will be able to reference Revit's dlls (.NET 4.8) in their .NET 5 projects, but there is no guarantee that everything will work. Some (if not most) things might, but I would not recommend going that way. The problem is that .NET 5 is based on .NET Core, not on the big .NET framework, and there are some incompatibilities.
  As for switching Revit to .NET 5, that's something we will definitely need to do as .NET 4.8 is the last version of the big .NET. However, the switch is not as trivial as changing the version dropdown (like it was from 4.7 to 4.8). We will have to convert to a new project format, fix some code and possibly find replacement for some frameworks that were present in .NET 4.8, but are not anymore in .NET 5.

- Controlling triangulation LOD
  8307 [Level of detail in Revit addin in design automation]
  https://forge.zendesk.com/agent/tickets/8307

- I was and still remain a fan of NoSQl, even after being intrigued by and reading an article by John Biggs and Ryan Donovan asking,
  [Have the tables turned on NoSQL?](https://stackoverflow.blog/2021/01/14/have-the-tables-turned-on-nosql) and concluding that
  NoSQL is "not so great for your side hustle" and that "a consensus has emerged in conferences and blogs that SQL is the gold
  standard &ndash; with a lot of emphasis on PostgeSQL &ndash; and you should use it by default, only deviating if you have
  good reasons to use NoSQL." I assume they know a lot more than I do in that area, so I guess I should trust their judgement
  more than mine in this case.

twitter:

SQL versus NoSQL, using .NET 5 and Core and controlling the face triangulation LOD in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://autode.sk/triangulationlod

Today, we discuss cores, splinters and data
&ndash; Using .NET 5 and Core
&ndash; Controlling face triangulation LOD
&ndash; SQL versus NoSQL...

linkedin:

SQL versus NoSQL, using .NET 5 and Core and controlling the face triangulation LOD in the #RevitAPI

http://autode.sk/triangulationlod

- Using .NET 5 and Core
- Controlling face triangulation LOD
- SQL versus NoSQL...

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

### Face Triangulation LOD, .NET 5 and Core

Today, we discuss cores, splinters and data:

- [Using .NET 5 and Core](#2)
- [Using .NET Standard 2.0 for Revit Add-Ins](#2.1)
- [Controlling face triangulation LOD](#3)
- [SQL versus NoSQL](#4)

####<a name="2"></a> Using .NET 5 and Core

[Olli Kattelus](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/774564), MEP Software Engineer of the Finnish MagiCAD Group,
updated the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) 
question [does Revit target .NET standard](https://forums.autodesk.com/t5/revit-api-forum/does-revit-target-net-standard/m-p/10021984) to
include coverage of .NET 5:

**Question:** I would like to use .NET Core to build my plugin, but I'm a bit confused whether it will cause issues or not.
From my understanding of .NET, if Revit targets a specific .NET standard, I will be able use any implementation of .NET (.NET Core, or .NET Framework) that conforms to that standard.
Does Revit target versions of .NET standards or does it just target versions of .NET Framework?

**Answer:** This is clearly defined in
the [Revit 2021 API development requirements](https://help.autodesk.com/view/RVT/2021/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Getting_Started_Welcome_to_the_Revit_Platform_API_Development_Requirements_html)
and [getting started page](https://help.autodesk.com/view/RVT/2021/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Getting_Started_html):

> The Revit Platform API is fully accessible by any language compatible with the Microsoft .NET Framework 4.8, such as Visual C# or Visual Basic .NET (VB.NET). Both Visual C# and VB.NET are commonly used to develop Revit Platform API applications. However, the focus of this manual is developing applications using Visual C#.

If your .NET core is compatible with the Microsoft .NET Framework 4.8, all should be fine.

**Question:** Will we be able to use the Revit API with .NET 5?

**Answer:** The factory should try to be prepared to answer.
So far, there are complications. Revit consumes Autodesk-wide .NET components.
We'd need to ensure that those are .NET 5 compatible before we switch our runtime.
Revit API also runs in-process using Revit's runtime, so I'm not sure it would be possible to preserve Revit's 4.8 runtime and allow add-in code to run .NET 5.0.
We have not tested either scenario yet.

Basically, developers will be able to reference Revit's dlls (.NET 4.8) in their .NET 5 projects, but there is no guarantee that everything will work.
Some (if not most) things might, but I would not recommend going that way.
The problem is that .NET 5 is based on .NET Core, not on the big .NET framework, and there are some incompatibilities.

As for switching Revit to .NET 5, that's something we will definitely need to do as .NET 4.8 is the last version of the big .NET.
However, the switch is not as trivial as changing the version dropdown (like it was from 4.7 to 4.8).
We will have to convert to a new project format, fix some code and possibly find replacement for some frameworks that were present in .NET 4.8, and are not anymore in .NET 5.

To understand more about .NET 5 Core and Framework enhancements, I found the official Microsoft overview
of [What's new in .NET 5](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five) pretty illuminating.

####<a name="2.1"></a> Using .NET Standard 2.0 for Revit Add-Ins

Thiago Almeida added a [helpful comment clarifying how to proceed](https://thebuildingcoder.typepad.com/blog/2021/01/face-triangulation-lod-net-5-and-core.html#comment-5244565338):

It is possible to target projects with .Net Standard to compile shared code between .Net Core and .Net Framework.

.Net Framework 4.6.1 is compatible with .Net Standard 2.0, which covers most of the common used APIs across the board.
The actual recommendation is to start from .Net Framework 4.7.2 as for consideration (2) on Microsoft's compatibility sheet (search
for "[.NET implementation support](https://duckduckgo.com/?q=.NET+implementation+support)").

Translating to Revit API add-ins:
You should be able to share code between Core platforms (.Net Core 3.1 or .Net 5) with Revit 2019 and above (.Net Framework 4.7.2) using .Net Standard 2.0.

Thank you very much for that, Thiago!

<center>
<img src="img/apple_core.png" alt="Apple core" title="Apple core" width="188"/> <!-- 376 -->
</center>

####<a name="3"></a> Controlling Face Triangulation LOD

A very useful solution for the desktop Revit API came about after observing a significantly different level of detail triangulating a face in a Revit add-in running in Forge design automation:

**Question:** I am working on a Revit add-in that I modified to run in the Forge design automation environment.
I am using a `CustomExporter` to export a Revit model to an `obj` file.
It uses the `Triangulate` method on the `Autodesk.Revit.DB.Face` class to triangulate some of the faces in the model at a certain level of detail (LOD).
The LOD value is passed to the `Face.Triangulate` method.
I noticed that when running the add-in in design automation on Forge, the triangle count of the exported model is much higher, compared to when it is run locally in the desktop on my computer.
I would like the detail of the model to be the same when exported on Forge as when it is exported on a desktop PC. 
Why this might be happening and how could it be fixed?


**Answer:** My first suspicion was that the LOD is affected by the graphics screen properties.
In the Forge environment, no real screen is attached, and that causes a much higher resolution to be assumed.

The development team respond:

In our CustomExporter, we set the value of `ViewNode.LevelOfDetail` in `OnViewBegin`.
However, we don't know what this does in comparison to `Face.Triangulate`.

In the version of `Face.Triangulate` that takes a `levelOfDetail` input of type `double`, that input controls the granularity of the triangulation.
`levelOfDetail` should lie in the range [0.0, 1.0], with 0.0 being the coarsest and 1.0 the finest.
The internal code uses an integer "level of detail" in the range [0, 15], and the input to Face.Triangulate is mapped to an integer by dividing the range [0.0, 1.0] into sixteen equal segments (i.e., multiply by 16, round down, and restrict to the range [0.0, 1.0]).

This ends up at the internal function `GFace::updateCachedFacets`, which also takes various other things into account (whether there's view-specific data, properties of the face in question, etc.). 

There's also a version of `Face.Triangulate` that takes no input and uses a different approach to choose a triangulation granularity.

By design of CustomExporter 3D, the main (only?) factor controlling the quality should be the view node's level of detail, as mentioned above.
However, what happens inside Face.Triangulate is separate from CustomExporter.
The scale of the view might have something do with any discrepancy between different export workflows.
On the other hand, Revit version and the presence of UI should not have an impact, although we cannot completely rule those factors out.

The Revit version can certainly play a role in principle.
For example, improvements to face triangulation are made from time to time, and that changes the way certain objects are triangulated in some cases.

We had thought presence or absence of UI would not have an impact for other things, and been proven wrong, e.g., garbage collection and image export.

If it's reproducible, in particular it might be reproducible without Custom Exporter and just running the same small routines on Revit UI and DA of going to one reasonably complex element, finding a target face, and Triangulating, we should look at it as a Problem Report.

Can you provide
a [minimal reproducible case](https://thebuildingcoder.typepad.com/blog/about-the-author.html#1b) for
the development team to analyse?

**Response:** I am using the 2020 version of Revit, both on desktop and in DA.

We have now solved our problem.

The solution was simply to set `ViewNode.LevelOfDetail` to the desired level of detail in `IExportContext.OnViewBegin` and then collect all the geometry in the `IExportContext.OnPolymesh` callback instead of the `OnFaceBegin` callback.

Before this, we were collecting some geometry by getting faces in `IExportContext.OnFaceBegin` and then triangulating those faces with `Face.Triangulate(LOD)`.

For some reason, the resulting geometry had much higher triangle count when the add-in was run in design automation.

We do not need any more assistance on this.

If the development team wants to reproduce it, here are the basic steps to do so:

- Create a Revit add-in containing a class that implements `IExportContext` and a `CustomExporter` that uses it.
- Make sure to set the `IncludeGeometricObjects` property on the `CustomExporter` object to `true`.
- In the `OnFaceBegin` callback of the `IExportContext` class, add the following code:
<pre class="code">
  public RenderNodeAction OnFaceBegin(FaceNode node)
  {
    Autodesk.Revit.DB.Face face = node.GetFace();
    Autodesk.Revit.DB.Mesh m = face.Triangulate(0.1);
    int vertCount = m.Vertices.Count;
    return RenderNodeAction.Proceed;
  }
</pre>
- Run this in Revit on a desktop computer and also in design automation on Forge.

The add-in will need to be modified to run in design automation.
Modify the code so the value of `vertCount` can be verified both in the desktop version and DA version.
I was expecting the value to be the same both on desktop and in DA, however, in my experience, the model was much more detailed when exporting in DA.

####<a name="4"></a> SQL Versus NoSQL

I became a fan of NoSQL while working on
the [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) project,
a multi-RVT-project reimplementation of the FireRating SDK sample.

It forms part of my research connecting desktop and cloud prior to the emergence of Forge and uses a REST API to access a cloud-based NoSQL MongoDB database managed by a node.js web server.

I still remain a fan of NoSQL, even after being intrigued by an article by John Biggs and Ryan Donovan asking,
[Have the tables turned on NoSQL?](https://stackoverflow.blog/2021/01/14/have-the-tables-turned-on-nosql)

They conclude that NoSQL is "not so great for your side hustle" and that 

> a consensus has emerged in conferences and blogs that SQL is the gold standard 
&ndash; with a lot of emphasis on PostgeSQL &ndash; and you should use it by default,
only deviating if you have good reasons to use NoSQL.

I assume they know a lot more than I do in this area, so I guess I should trust their judgement more than mine in this case.

But I naively continue to prefer NoSQL anyway :-)

