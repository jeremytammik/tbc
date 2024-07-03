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

 @AutodeskRevit #RevitAPI #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Material Assets


####<a name="2"></a>

**Question:**

**Answer:**




Many thanks to ... of you for your thorough implementation, testing, discussion and documentation!


####<a name="2"></a> jacob.small 'info' on what I think I know about material assets

jacob.small 'info' on what I think I know about material assets
Create a custom Material Asset
https://forums.autodesk.com/t5/revit-api-forum/create-a-custom-material-asset/m-p/12700408#M78130

####<a name="3"></a> CefSharp versus WebView2 Embedded Browser

Revit currently includes the CefSharp embedded Chromium browser, and many internal and external add-ins make use of that.
Another option for Chromium embedding is provided by WebView2, and some add-ins already use that instead.
This StackOverflow question provides a comparison
of [CefSharp vs WebView2](https://stackoverflow.com/questions/70360189/cefsharp-vs-webview2).
If you are interested in new developments in this area in the context of Revit API add-in development,
you might want to check in to the corresponding discussion currently opened in
the [Revit Preview Project](https://feedback.autodesk.com/key/LHMJFVHGJK085G2M).

####<a name="4"></a> Twentytwo

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

I use `grep` a lot in my everyday work, searching for text in text files.
With `ugrep`, this workflow can be easily expanded to cover all kinds of file formats and directory structures,
cf. [The ugrep file pattern searcher](https://ugrep.com), with
its [ugrep GitHub repo](https://github.com/Genivia/ugrep).

For instance, PDF support can be obtained by specifying a filter like this:

<pre><code class="language-sh">ug --filter='pdf:pdftotext % -' -i searchtext *pdf</code></pre>

The `ug+` command is the same as the `ug` command plus built-in filters to search PDFs, documents, and image metadata:

<pre><code class="language-sh">ug+ -i searchtext *pdf</code></pre>

Fuzzy search is also supported, among tons of other features:

<pre><code class="language-sh">ug+ -Z -i searchtext *pdf</code></pre>

####<a name="6"></a> AI Mesh Understanding

An overview of the MeshAnything project aiming to generate more effective meshing of 3D objects:

- 44-second video blurb: [AI just figured out Meshes](https://youtu.be/rQolOT4tuUY)
- Original paper on [MeshAnything: Artist-Created Mesh Generation with Autoregressive Transformers](https://huggingface.co/papers/2406.10163)
- [MeshAnything demo](https://huggingface.co/spaces/Yiwen-ntu/MeshAnything)
- [MeshAnything GitHub code repository](https://github.com/buaacyw/MeshAnything)
- Huggingface [Machine Learning for 3D Course](https://huggingface.co/learn/ml-for-3d-course/unit0/introduction)

####<a name="7"></a> LLM Self-Reflection

[Josh Whiton](https://x.com/joshwhiton/) performed an interesting experiment on LLM self-reflection showing
that [Claude Sonnet 3.5 passes the AI mirror test](https://x.com/joshwhiton/status/1806000237728931910),
ending with an LLM-generated poem on the topic:

<center>
<img src="img/claude_sonnet_selfreflect.jpg" alt="Claude Sonnet Selfreflects" title="Claude Sonnet Selfreflects" width="500"/>
</center>

####<a name="8"></a> LLM Deep Stupidity

Mark Bishop wrote critical article stating
that [artificial intelligence is stupid and causal reasoning will not fix it](https://www.frontiersin.org/journals/psychology/articles/10.3389/fpsyg.2020.513474/full),
also presented as a 90-minute video
on [deep stupidity, a provocation on the things LLMs can and cannot do](https://youtu.be/sN-vsd7SVqs).

####<a name="9"></a> LLM AI Sans MatMul

This interesting research may affect both the enourmous resources consumed by AI and the chip maker stock prices:
[Researchers upend AI status quo by eliminating matrix multiplication in LLMs](https://arstechnica.com/information-technology/2024/06/researchers-upend-ai-status-quo-by-eliminating-matrix-multiplication-in-llms/).

