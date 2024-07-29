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

### Webview2 Plans



<center>
<img src="img/.png" alt="" title="" width="100"/>
</center>



####<a name="2"></a> Move from CefSharp to WebView2

The heads-up that we shared in the beginning of July
on [CefSharp versus WebView2 Embedded Browser](https://thebuildingcoder.typepad.com/blog/2024/07/material-assets-chromium-and-sorting-schedules.html#3) is
stabilisingto the extens that the development team has decided to announce a plan to migrate CefSharp to WebView2 in the next major release:

> Revit is removing all CefSharp binaries from its distribution package starting in the next major release.
Revit add-ins can keep using CefSharp as a standard 3rd party component.
To do so CefSharp, please ensure your add-in will deliver CefSharp binaries with your add-in.
However, it is recommended to use WebView2 as an alternative to avoid potential issues from different CefSharp release versions in one Revit session.

####<a name="2"></a> CrowdStrike Outage

If you are interested in some technical background details on the recent CrowdStrike outage, two informative videos by Dave's Garage explain what happened quite well and reinforce the importance of input validation:

- [CrowdStrike IT outage](https://youtu.be/wAzEJxOo1ts)
- [CrowdStrike update latest news, lessons learned](https://youtu.be/ZHrayP-Y71Q)

####<a name="2"></a> Ai Models Trained on AI-Generated Data Collapse

What we all intuitively knew has now been scientifically corroborated &ndash;
[AI models collapse when trained on recursively generated data](https://www.nature.com/articles/s41586-024-07566-y):

> generative artificial intelligence (AI) such as large language models (LLMs) is here to stay and will substantially change the ecosystem of online text and images ... consider what may happen to GPT-{n} once LLMs contribute much of the text ... indiscriminate use of model-generated content in training causes irreversible defects in the resulting models, in which tails of the original content distribution disappear. We refer to this effect as ‘model collapse’ and show that it can occur in LLMs as well as in variational autoencoders (VAEs) and Gaussian mixture models (GMMs)...

####<a name="2"></a> Open Source AI Is the Path Forward

Mark Zuckerberg, founder and CEO of Meta, reiterates and clarifies their vision
that [open-source AI is the path forward](https://about.fb.com/news/2024/07/open-source-ai-is-the-path-forward/).

