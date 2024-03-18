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

 with the #RevitAPI @AutodeskRevit #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Path of Travel Doors Traversed


####<a name="2"></a> Bowerbird C&#35; Scripting for Revit

[Christopher Diggins](https://github.com/cdiggins) published
[Bowerbird](https://github.com/ara3d/bowerbird).
It accelerates and simplifies C# tool and plug-in development by dynamically compiling C# source files:
He would appreciate comments on it in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160)
thread [Feedback Requested &ndash; Bowerbird](https://forums.autodesk.com/t5/revit-api-forum/feedback-requested-bowerbird-c-scripting-for-revit/td-p/12643568),
saying:

> I've released a new open-source project for Revit C# developers called Bowerbird.
It uses the Roslyn C# compiler to allow users to create and edit new commands directly from C# source files,
without having to go through the process of creating and deploying a plug-in, and re-launching Revit.

> It is inspired
by [pyRevit](https://github.com/eirannejad/pyRevit) by [Ehsan Iran-Nejad](https://github.com/eirannejad)
and [Revit.ScriptCS](https://github.com/sridharbaldava/Revit.ScriptCS) by [Sridhar Baldava](https://github.com/sridharbaldava).

> I'd greatly appreciate any feedback or contributions at https://github.com/ara3d/bowerbird/
Thanks in advance!

Many thanks to Christopher for creating and sharing this helpful tool!

####<a name="2"></a> Doors traversed on path of travel lines

Doors traversed on path of travel lines
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

####<a name="2"></a> We’re excited to introduce Claude 3, our next generation of vision-enabled AI models - now available on claude.ai.

We’re excited to introduce Claude 3, our next generation of vision-enabled AI models - now available on claude.ai.
https://www.anthropic.com/news/claude-3-family
https://claude.ai/

####<a name="2"></a> Introducing Devin, the first AI software engineer

Introducing Devin, the first AI software engineer
https://www.cognition-labs.com/blog
March 12th, 2024 | Written by Scott Wu
Meet Devin, the world’s first fully autonomous AI software engineer.

####<a name="2"></a> generates image, but badly, inattentive, does not follow the detailed prompt: https://imagine.meta.com/

generates image, but badly, inattentive, does not follow the detailed prompt: https://imagine.meta.com/
Meta Launches Web-Based AI Image Generator Trained on Your Instagram Pics
https://uk.pcmag.com/ai/150034/meta-launches-web-based-ai-image-generator-ai-updates-across-its-apps
I very briefly tested Meta Imagine to create an image purely based on a text prompt and was unable to tweak the prompt to generate a satisfactory result.
My impression was that it very quickly ignored important aspects of my prompt, e.g., specific colour requests etc.

####<a name="2"></a> What is climate change? A really simple guide

What is climate change? A really simple guide
https://www.bbc.com/news/science-environment-24021772


####<a name="2"></a>

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 303 Pixel Width: 740 -->
</center>


Many thanks to  for


<pre><code class="language-csharp">
</code></pre>


<pre><code class="language-python">
</code></pre>

Many thanks to ??? for creating and sharing this powerful toolkit!

