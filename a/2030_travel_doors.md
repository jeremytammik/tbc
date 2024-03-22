<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>
</head>

<!---

- Feedback Requested - Bowerbird: C# Scripting for Revit
  https://forums.autodesk.com/t5/revit-api-forum/feedback-requested-bowerbird-c-scripting-for-revit/td-p/12643568

- Doors traversed on path of travel lines
  https://forums.autodesk.com/t5/revit-api-forum/doors-traversed-on-path-of-travel-lines/td-p/12616109
  % bl 1740 1744 1781 1836 1871 1917 2028
  <ul>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html">What's New in the Revit 2020 API</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2019/04/new-revit-2020-sdk-samples.html">New Revit 2020 SDK Samples</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2019/09/whats-new-in-the-revit-20201-api.html">What's New in the Revit 2020.1 API</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/whats-new-in-the-revit-2021-api.html">What's New in the Revit 2021 API</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2020/10/firerevit-deprecated-api-and-elbow-centre-point.html">FireRevit, Deprecated API and Elbow Centre Point</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2021/09/view-sheet-from-view-and-select-all-on-level.html">View Sheet from View and Select All on Level</a></li>
  <li><a href="https://thebuildingcoder.typepad.com/blog/2024/02/interactive-bim-notebook-temporary-graphics-and-ai.html">Interactive BIM Notebook, Temporary Graphics and AI</a></li>
  </ul>

- We’re excited to introduce Claude 3, our next generation of vision-enabled AI models - now available on claude.ai.
  https://www.anthropic.com/news/claude-3-family
  https://claude.ai/

- Introducing Devin, the first AI software engineer
  https://www.cognition-labs.com/blog
  March 12th, 2024 | Written by Scott Wu
  Meet Devin, the world’s first fully autonomous AI software engineer.

- generates image, but badly, inattentive, does not follow the detailed prompt: https://imagine.meta.com/

- What is climate change? A really simple guide
  https://www.bbc.com/news/science-environment-24021772

twitter:

Bowerbird scripting, doors traversed by path of travel, camera mapping between APS and @AutodeskRevit with the #RevitAPI #BIM @DynamoBIM https://autode.sk/camerasettings

New exciting Revit API solutions and furious pace of LLM development
&ndash; Bowerbird C&#35; scripting for Revit
&ndash; Doors traversed by path of travel
&ndash; Camera mapping between APS and Revit
&ndash; Claude 3 can see
&ndash; Devin, an AI software engineer
&ndash; Meta Imagine generates images
&ndash; An LLM for decompiling binary code
&ndash; Simple climate change overview...

linkedin:

Bowerbird scripting, doors traversed by path of travel, camera mapping between APS and Revit with the #RevitAPI

https://autode.sk/camerasettings

- Bowerbird C&#35; scripting for Revit
- Doors traversed by path of travel
- Camera mapping between APS and Revit
- Claude 3 can see
- Devin, an AI software engineer
- Meta Imagine generates images
- An LLM for decompiling binary code
- Simple climate change overview...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Camera Settings, Doors Traversed, Script on the Fly

New exciting Revit API solutions and continued furious pace of LLM development:

- [Bowerbird C&#35; scripting for Revit](#2)
- [Doors traversed by path of travel](#3)
- [Camera mapping between APS and Revit](#4)
- [Claude 3 can see](#5)
- [Devin, an AI software engineer](#6)
- [Meta Imagine generates images](#7)
- [An LLM for decompiling binary code](#8)
- [Simple climate change overview](#9)

####<a name="2"></a> Bowerbird C&#35; Scripting for Revit

[Christopher Diggins](https://github.com/cdiggins) published
[Bowerbird](https://github.com/ara3d/bowerbird) for
quick and easy C&#35; tool and plug-in development by dynamically compiling C# source files,
and a [request for feedback on it](https://forums.autodesk.com/t5/revit-api-forum/feedback-requested-bowerbird-c-scripting-for-revit/td-p/12643568)
in the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160),
saying:

> I've released a new open-source project for Revit C# developers called Bowerbird.
It uses the Roslyn C# compiler to allow users to create and edit new commands directly from C# source files,
without having to go through the process of creating and deploying a plug-in, and re-launching Revit.

> It is inspired
by [pyRevit](https://github.com/eirannejad/pyRevit) by [Ehsan Iran-Nejad](https://github.com/eirannejad)
and [Revit.ScriptCS](https://github.com/sridharbaldava/Revit.ScriptCS) by [Sridhar Baldava](https://github.com/sridharbaldava).

> I'd greatly appreciate any feedback or contributions in
the [Bowerbird GitHub project](https://github.com/ara3d/bowerbird/).
Thanks in advance!

Many thanks to Christopher for creating and sharing this helpful tool!

####<a name="3"></a> Doors Traversed by Path of Travel

A while ago, I took a look at determining the doors traversed by a path of travel and shared some thoughts on that in
the [PathOfTravelDoors GitHub repo](https://github.com/jeremytammik/PathOfTravelDoors).

They were picked up again in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [doors traversed on path of travel lines](https://forums.autodesk.com/t5/revit-api-forum/doors-traversed-on-path-of-travel-lines/td-p/12616109).

**Question:** I want to list all the doors that are crossed by a path of travel line.
I tried to code that, but it seems that the `ReferenceIntersector` finds more doors that are not on the path of travel, because the introduced ray is reaching them.

<center>
<img src="img/path_of_travel_doors.png" alt="Path of travel doors" title="Path of travel doors" width="600"/> <!-- Pixel Height: 622 Pixel Width: 1,152 -->
</center>

I looked at the Revit SDK sample PathOfTravel, but that does not help.

The [ReferenceIntersector documentation](https://www.revitapidocs.com/2024/c4fb6c89-ca34-7c56-b730-98755d11fedf.htm) is illuminating, and
the [FindNearest method](https://www.revitapidocs.com/2024/866e1f2b-c79a-4d9f-1db1-9e386dd42941.htm) ought
to ensure that I only get a maximum of one single intersected door.

**Answer:** Hmm. Maybe, this task can be addressed simpler.
How about this approach without using the reference intersector at all?

- Retrieve the path of travel curve tessellation
- For each line segment, determine whether it intersects a door

Afaict, that should solve the problem right there.
What do you think?

**Response:** Just cracked it an hour ago!
I tackled it with this trick:
For each curve in the path of travel line I did once from the start point following the curve's direction, and once from the endpoint with the reversed direction.
Then, I accepted the points that appeared in both.

I also thought of another approach as you mentioned: generating an imaginary line at each door location and checking whether the path of travel line segments intersects that.
However, this method required finding the two points of each door, possibly by examining the geometry of a wall for its opening.
While it seems plausible, I decided against pursuing it initially due to its complexity.

Later: Unfortunately, the described technique fails to yield the intended results across certain models, resulting in a null output from the `ReferenceIntersector`.

As a workaround for those specific models, an alternative approach was employed:

- Extract the start and end points of each line from the door geometry instance
- Construct an imaginary line corresponding to the door location (the bounding box includes door swing, so not proper for my case)
- Examine the intersection of this imaginary line with the curves of the path of travel lines

**Answer:**
Glad to hear that you found an approach that works reliably for all door instances.

I cannot say for sure why the reference intersector fails in some cases.
One thing to consider, though, is that a content creator has complete freedom in the family definition.
So, some content creators might choose to represent doors in a completely unconventional manner.
They might define the door geometry so that no solids or faces exist for the reference intersector to detect, which might lead to such failures.
The infinite flexibility provided for Revit family definitions can make it hard to ensure that an approach always covers all cases.
This makes unit testing on a large collection of possible BIM variations all the more important.

The approach you describe is very generic: every door opening is defined by one single line from start to end point, and that line must be crossed to pass through the door.
That sounds pretty fool-proof to me.

<!--

% bl 1740 1744 1781 1836 1871 1917 2028
<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html">What's New in the Revit 2020 API</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/04/new-revit-2020-sdk-samples.html">New Revit 2020 SDK Samples</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/09/whats-new-in-the-revit-20201-api.html">What's New in the Revit 2020.1 API</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/whats-new-in-the-revit-2021-api.html">What's New in the Revit 2021 API</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/10/firerevit-deprecated-api-and-elbow-centre-point.html">FireRevit, Deprecated API and Elbow Centre Point</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/09/view-sheet-from-view-and-select-all-on-level.html">View Sheet from View and Select All on Level</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2024/02/interactive-bim-notebook-temporary-graphics-and-ai.html">Interactive BIM Notebook, Temporary Graphics and AI</a></li>
</ul>

-->


####<a name="4"></a> Camera Mapping Between APS and Revit

In 2019, Eason Kang shared a very helpful explanation on how
to [map Forge viewer camera back to Revit](https://aps.autodesk.com/blog/map-forge-viewer-camera-back-revit).

However, some aspects changed, and some were left uncovered back then, as discussed in
the new [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160)
on [Revit 3D view camera settings](https://forums.autodesk.com/t5/revit-api-forum/revit-3d-view-camera-settings/m-p/12629132).

So, Eason took another deep dive into the topic, researched, tested, organized all the material and published it in two blog posts:

- [From APS viewer to Revit](https://aps.autodesk.com/blog/camera-mapping-between-aps-viewer-and-revit-part-i-restore-viewer-camera-revit)
- [From Revit to APS viewer](https://aps.autodesk.com/blog/camera-mapping-between-aps-viewer-and-revit-part-ii-restore-revit-camera-viewer)

The associated sample code lives in the

- [aps-viewer-revit-camera-sync GitHub repo](https://github.com/yiskang/aps-viewer-revit-camera-sync)

<!--
If you’re interested, here are the old discussions with another customer about doing something similar.
Cf. https://forge.zendesk.com/agent/tickets/14155
-->

<center>
<img src="img/aps_viewer_camera_perspective.png" alt="APS perspective view camera" title="APS perspective view camera" width="600"/> <!-- Pixel Height: 576 Pixel Width: 1,024 -->
</center>

Ever so many thanks to Eason for his very careful research and documentation.

####<a name="5"></a> Claude 3 can See

Claude 3 LLM AI model was released, now vision-enabled and with scores in several intelligence tests:

- [Claude 3 announcement](https://www.anthropic.com/news/claude-3-family)
- [Claude 3 entry point](https://claude.ai/)

####<a name="6"></a> Devin, an AI Software Engineer

Another announcement
introduces [Devin, the first AI software engineer](https://www.cognition-labs.com/blog),
a fully autonomous AI software engineer.

####<a name="7"></a> Meta Imagine Generates Images

[Meta Imagine](https://imagine.meta.com/) generates images,
cf., [Meta launches web-based AI image generator trained on your Instagram pics](https://uk.pcmag.com/ai/150034/meta-launches-web-based-ai-image-generator-ai-updates-across-its-apps).

I briefly tested it myself, trying to approximate an image of the real-world scene in front of me, and was unable to tweak the prompt to generate a satisfactory result.
My impression was that it very quickly ignored important aspects of my prompt, e.g., specific colour requests, etc., even when I repeated them, so I quickly gave up, unsatisfied.

####<a name="8"></a> An LLM for Decompiling Binary Code

Yet another use of LLM,
[LLM4Decompile: decompiling binary code with large language models](https://arxiv.org/abs/2403.05286),
with its [LLM4Decompile GitHub repo](https://github.com/albertan017/LLM4Decompile).

####<a name="9"></a> Simple Climate Change Overview

On another ever-present and looming topic of our days,
BBC shares a nice and simple comprehensive article
explaining [What is climate change? A really simple guide](https://www.bbc.com/news/science-environment-24021772).

