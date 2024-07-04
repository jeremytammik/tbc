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

- jacob.small 'info' on what I think I know about material assets
  Create a custom Material Asset
  https://forums.autodesk.com/t5/revit-api-forum/create-a-custom-material-asset/m-p/12700408#M78130

- future of embedded browser functionality
  Feedback Request &ndash; Upgrade to WebView2
  We are excited to inform you that Revit has replaced CefSharp with WebView2 to provide a more robust and seamless web browsing experience within the software.
  Why the change?
  WebView2 is a web control provided by Microsoft Edge (Chromium) that allows developers to host web content in native applications. It offers better performance, compatibility, and support compared to the previously used CefSharp. This ensures consistent rendering across all Windows devices, improved security, and the ability to use the latest web technologies.
  What does this mean for you?
  The transition to WebView2 is designed to be as seamless as possible. However, as with any significant change, there may be minor adjustments. We kindly ask you to test if all the existing features work fine as usual.
  How can you help?
  Please spend some time using Revit as you normally would, and pay particular attention to any features that use web content. If you encounter any issues or notice any changes in functionality, we would greatly appreciate your feedback.
  We thank you in advance for testing this enhancement!

- Twentytwo
  a place for BIM Programming enthusiasts
  WRITTEN BY MIN.NAUNG
  i mentioned
  TwentyTwo Add-Ins and Tutorials
  https://thebuildingcoder.typepad.com/blog/2022/10/element-level-and-ifc-properties-.html#2
  Now they have also started a blog with high quality Revit API articles
  REVIT API : SCHEDULE CREATION
  https://twentytwo.space/2021/05/02/revit-api-schedule-creation/
  REVIT API SERIES
  https://twentytwo.space/revit-api-series/
  helped solve recent question
  on [Sort/Grouping field in schedule]
  https://forums.autodesk.com/t5/revit-api-forum/sort-grouping-field-in-schedule/m-p/12869665

- ugrep
  https://ugrep.com
  https://github.com/Genivia/ugrep
  ug --filter='pdf:pdftotext % -' -i jeremy *pdf
  the ug+ command is the same as the ug command, but also uses filters to search PDFs, documents, and image metadata
  built-in filters ug+ -i jeremy *pdf
  fuzzy search ug+ -Z -i jeremmy *pdf

- AI just figured out Meshes
  https://youtu.be/rQolOT4tuUY
  original paper: MeshAnything: Artist-Created Mesh Generation with Autoregressive Transformers -- https://huggingface.co/papers/2406.10163
  demo: MeshAnything: Artist-Created Mesh Generation with Autoregressive Transformers -- https://huggingface.co/spaces/Yiwen-ntu/MeshAnything
  code: GitHub https://github.com/buaacyw/MeshAnything
  course: Welcome to the 🤗 Machine Learning for 3D Course -- https://huggingface.co/learn/ml-for-3d-course/unit0/introduction

- Claude Sonnet Selfreflects
  Claude Sonnet 3.5 Passes the AI Mirror Test
  https://x.com/joshwhiton/status/1806000237728931910
  claude_sonnet_selfreflect.jpg

- Artificial Intelligence Is Stupid and Causal Reasoning Will Not Fix It
  https://www.frontiersin.org/journals/psychology/articles/10.3389/fpsyg.2020.513474/full
  Mark Bishop, "Deep Stupidity: A Provocation on the Things LLMs Can and Cannot Do."
  https://youtu.be/sN-vsd7SVqs

- Researchers upend AI status quo by eliminating matrix multiplication in LLMs
  https://arstechnica.com/information-technology/2024/06/researchers-upend-ai-status-quo-by-eliminating-matrix-multiplication-in-llms/

twitter:

Personalised material asset properties, CefSharp versus WebView2 embedded browser, creating and sorting schedules with the @AutodeskRevit #RevitAPI #BIM @DynamoBIM https://autode.sk/materialassetprops

Material assets, built-in browser functionality, create schedules, search text and miscellaneous LLM-related news items
&ndash; Personalised material asset properties
&ndash; CefSharp versus WebView2 embedded browser
&ndash; Twentytwo on schedule creation
&ndash; Ugrep enhanced grep
&ndash; AI mesh understanding
&ndash; LLM self-reflection, deep stupidity, sans <code>MatMul</code>, and locally the easy way...

linkedin:

Personalised material asset properties, CefSharp versus WebView2 embedded browser, creating and sorting schedules with the #RevitAPI

https://autode.sk/materialassetprops

- Personalised material asset properties
- CefSharp versus WebView2 embedded browser
- Twentytwo on schedule creation
- Ugrep enhanced grep
- AI mesh understanding
- LLM self-reflection, deep stupidity, sans <code>MatMul</code>, and locally the easy way...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Material Assets, Chromium and Sorting Schedules

Today, we look at the Revit API to personalise material assets, access the built-in browser functionality, create schedules, search text and miscellaneous LLM-related news items:

- [Personalised material asset properties](#2)
- [CefSharp versus WebView2 embedded browser](#3)
- [Twentytwo on schedule creation](#4)
- [Ugrep enhanced grep](#5)
- [AI mesh understanding](#6)
- [LLM self-reflection](#7)
- [LLM deep stupidity](#8)
- [LLM AI sans `MatMul`](#9)
- [Local LLM AI the easy way](#10)

####<a name="2"></a> Personalised Material Asset Properties

Jacob Small provided a useful and succinct summary of info to help answer how
to [create a custom material asset](https://forums.autodesk.com/t5/revit-api-forum/create-a-custom-material-asset/m-p/12700408):

**Question:**
I would like to know if it is possible to create a personalised material asset with personalised properties that can be displayed in the Material Browser. For example, a material asset called "Test" placed after "Thermal". And in "Test" put a property like "Address".
If this isn't possible, is it possible to add the "Address" property to an asset that already exists, for example "Identity"?
If so, can we then create a new section in the Identity asset as "Additional information"?

**Answer:**
Some quick 'info' on what I think I know about material assets:

The only "asset types" in the UI are Appearance, Thermal, and Physical. In the API these are called Appearance, Thermal, and Structural (because why would they match?). I'll be using the API names going forward because we're in that forum (perhaps someone curious can ask about the UI stuff in the other forum).

The Thermal and Structural asset types can be deleted from a material in the UI and set as an invalid Element Id in the API. The Appearance asset cannot.

The other tabs in the material editor are collections of properties and parameters of the material element itself, not a linked asset - but are masquerading as an asset due to how they are presented in the UI.  For example to get the name (shows in the identity tab) you get the Name property of a material element. To get the foreground surface pattern (shows in the graphics tab) you'd get the SurfaceForegroundPatternId property. To set the Comments in the Identity tab you'd use a Set method on the comment parameter.

The only thing unavailable is the keywords (noted as missing in 2019 and as far as I know still unavailable).

So the question becomes, how can you accomplish what you're after?

You could map a property from the identity tab (URL looks like an option) to an extensible storage object with the data you need; however users won't be able to edit it in the material editor. Personally I would make 'non-UserModifiable' parameters on the materials category, and associate them to the materials in your template, and let them be edited/reviewed via an add-in which would also allow updating the rest of the assets. This add-in could also ensure that materials added via your tool would have these hidden parameters quickly set when materials from your library (a sub-component of your add-in) are added to the model.

It'd be a big lift but likely one which would benefit many beyond your company.

Many thanks to Jacob for this helpful summary.

####<a name="3"></a> CefSharp versus WebView2 Embedded Browser

Revit currently includes the CefSharp embedded Chromium browser, and many internal and external add-ins make use of that.
Another option for Chromium embedding is provided by WebView2, and some add-ins already use that instead.
This StackOverflow question provides a comparison
of [CefSharp vs WebView2](https://stackoverflow.com/questions/70360189/cefsharp-vs-webview2).
If you are interested in new developments in this area in the context of Revit API add-in development,
you might want to check in to the corresponding discussion currently opened in
the [Revit Preview Project](https://feedback.autodesk.com/key/LHMJFVHGJK085G2M).

####<a name="4"></a> Twentytwo on Schedule Creation

[Twentytwo](https://twentytwo.space), written by Min Naung, provides a place and resources for BIM Programming enthusiasts.
Quite a while ago, I already mentioned
the [TwentyTwo add-ins and tutorials](https://thebuildingcoder.typepad.com/blog/2022/10/element-level-and-ifc-properties-.html#2).
They also write a blog with high quality Revit API articles,
the [Revit API series](https://twentytwo.space/revit-api-series/).

One of them, for instance,
on [schedule creation](https://twentytwo.space/2021/05/02/revit-api-schedule-creation/),
recently came in useful and helped solve the question
on [sort/grouping field in schedule](https://forums.autodesk.com/t5/revit-api-forum/sort-grouping-field-in-schedule/m-p/12869665).

Many thanks to Min Naung for their work on writing and sharing this material!

####<a name="5"></a> Ugrep Enhanced Grep

I use `grep` in my everyday work to search for text in text files.
With `ugrep`, this workflow can be easily expanded to cover all kinds of other file formats and directory structures,
cf. [the ugrep file pattern searcher](https://ugrep.com), with
its [ugrep GitHub repo](https://github.com/Genivia/ugrep).

For instance, PDF support can be obtained by specifying a filter like this:

<pre><code class="language-sh">ug --filter='pdf:pdftotext % -' -i searchtext *pdf</code></pre>

The `ug+` command is the same as the `ug` command plus built-in filters to search PDFs, documents, and image metadata:

<pre><code class="language-sh">ug+ -i searchtext *pdf</code></pre>

Fuzzy search is also supported, among tons of other features:

<pre><code class="language-sh">ug+ -Z -i searchtext *pdf</code></pre>

####<a name="6"></a> AI Mesh Understanding

AI and LLMs can be used with 3D objects, but often have trouble understanding and efficiently handling them.
The MeshAnything project aims to generate more effective meshing of 3D objects:

- 44-second video blurb: [AI just figured out Meshes](https://youtu.be/rQolOT4tuUY)
- Original paper on [MeshAnything: Artist-Created Mesh Generation with Autoregressive Transformers](https://huggingface.co/papers/2406.10163)
- [MeshAnything demo](https://huggingface.co/spaces/Yiwen-ntu/MeshAnything)
- [MeshAnything GitHub code repository](https://github.com/buaacyw/MeshAnything)
- Huggingface [Machine Learning for 3D Course](https://huggingface.co/learn/ml-for-3d-course/unit0/introduction)

####<a name="7"></a> LLM Self-Reflection

[Josh Whiton](https://x.com/joshwhiton/) performed an experiment on LLM self-reflection showing
that [Claude Sonnet 3.5 passes the AI mirror test](https://x.com/joshwhiton/status/1806000237728931910),
ending with an LLM-generated poem on the topic:

<center>
<img src="img/claude_sonnet_selfreflect.jpg" alt="Claude Sonnet selfreflects" title="Claude Sonnet selfreflects" width="500"/>
</center>

####<a name="8"></a> LLM Deep Stupidity

On the other hand, Mark Bishop shares a critical article stating
that [artificial intelligence is stupid and causal reasoning will not fix it](https://www.frontiersin.org/journals/psychology/articles/10.3389/fpsyg.2020.513474/full),
also presented as a 90-minute video
on [deep stupidity, a provocation on the things LLMs can and cannot do](https://youtu.be/sN-vsd7SVqs).

####<a name="9"></a> LLM AI Sans MatMul

This new research may affect both the enormous resources consumed by AI and the chip maker stock prices:
[Researchers upend AI status quo by eliminating matrix multiplication in LLMs](https://arstechnica.com/information-technology/2024/06/researchers-upend-ai-status-quo-by-eliminating-matrix-multiplication-in-llms/).

####<a name="10"></a> Local LLM AI the Easy Way

For experimentation and learning,
the [CodeProject.AI Server provides AI the easy way](https://www.codeproject.com/Articles/5322557/CodeProject-AI-Server-AI-the-easy-way):

> CodeProject.AI Server is a locally installed, self-hosted, fast, free and open-source AI server for any platform, any language.
No off-device or out of network data transfer, no messing around with dependencies, and able to be used from any platform, any language.
Runs as a Windows Service or a Docker container.

