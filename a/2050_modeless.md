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

- https://forums.autodesk.com/t5/revit-api-forum/revit-addin-without-blocking-revit-ui/m-p/12951089#M80731

- limited PDF printing speed
  https://autodesk.slack.com/archives/C0SR6NAP8/p1721996815735469
  Riley Peterson
  Jacob Small

- Code samples for working with Family Instances.
  https://help.autodesk.com/view/RVT/2025/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Revit_Geometric_Elements_Family_Instances_Code_Samples_html

- llm arena
  https://arena.lmsys.org/
  LMSYS Chatbot Arena (Multimodal): Benchmarking LLMs and VLMs in the Wild
  Chatbot Arena Leaderboard
  We've collected 1,000,000+ human votes to compute an LLM Elo leaderboard for 100+ models. Find out who is the 🥇LLM Champion here!

- Apple Intelligence Foundation Language Models
  https://machinelearning.apple.com/papers/apple_intelligence_foundation_language_models.pdf
  present foundation language models developed to power Apple Intelligence features, including a ∼3 billion parameter model designed to run efficiently on devices and a large server-based language model designed for Private Cloud Compute

twitter:

 with the @AutodeskRevit #RevitAPI #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Modeless Add-In


####<a name="2"></a>

[Revit Addin without blocking Revit UI](https://forums.autodesk.com/t5/revit-api-forum/revit-addin-without-blocking-revit-ui/m-p/12951089#M80731

I have my Revit Add-in and I would like now to make it running without blocking the UI.
Should I use the MVVM design pattern for that?

In any case, how would you recommend me to make this shift and make my addin not blocking the Revit UI?
I followed the examples of @nice3point here, but am not sure what is the best approach for me now.

Thank you,
Amir

Tags (0)
Add tags
Report
5 REPLIES
Sort:
MESSAGE 2 OF 6
jeremy_tammik
  Autodesk jeremy_tammik  in reply to: mizrachi_amir2

Repetition number 849 (well, actually number print(int(1000 &asterisk; random.random())))...

The most important differentiation in this context is between modal and modeless.

Please be aware that the Revit API is single-threaded and only runs within a valid Revit API context, Such a context is only provided by Revit in one of the numerous event handlers defined by the API and runs in the main thread of Revit. This blocks the UI, just as you say.

You can avoid this by executing parts of your add-in in a separate modeless thread. The modeless part can release the main thread, allow Revit to continue doing other stuff, and both Revit and the add-in can continue interacting with the user. However, this modeless part of your add-in has no access to the Revit API.

The preferred (and almost only) way for the modeless part of your add-in to interact with the Revit API and gain access to a valid Revit API context enabling it to do so is to implement an external event. The external event can be raised from the modeless part of the add-in. Revit then calls the corresponding event handler and provides it with a valid Revit API context.

This is discussed in great depth and with many examples by The Building Coder, and in nuerous other posts here in the forum:

https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28


nice3point


The link you provided has an example "Single-project WPF Application (Modeless)", this is the preferred way of interacting a modeless WPF application and Revit, without blocking the UI

mizrachi_amir2

Thank you for the detailed explanation.
What is Modal though? Is it simply the pop up window using WPF? Is it related to a MVVM design pattern?

What is the advantage of using MVVM in Revit plugins?

So, basically the difference between the template of Single-project WPF Application (Modeless) and Single-project WPF Application (Modal) is that the latter blocks Revit UI?

nice3point

Yes, modeless doesn't block Revit, Modal does. As Jeremy said and as it is written in the samples Readme, in modeless variant you have to use IExternalEventHandler to modify the document

You may not use MVVM if you are just learning, but it is a good practice and should be considered for commercial plugin development


####<a name="2"></a> Limited PDF Printing Speed

**Question:**
I'm working on a Revit DA project to do sheet to PDF printing, however, I'm noticing that the Revit API export function is pretty slow, my current benchmark is about 100 sheets / hour to a combined PDF.
Does anyone have tips on how the export process could be sped up? Has anyone used an 3rd party library to plot the PDF's more quickly? I'm having a hard time accepting that 30 seconds / sheet is the fastest we can achieve.


Jacob Small

I used to plan on 2 minutes per sheet when I was practicing... meant that if we had to have the set ready on Friday we either had to have multiple people coordinating who's printing what, or the read deadline was Thursday and all day friday my CPU was just making prints.
15:24
Curious what you would expect here - PDF encoding isn't quick in my experience.

Riley Peterson

We're benchmarking against clarity which can do the same 300 sheet set in 1 hr, we're at 3, was hoping to get into a similar ballpark.


Jacob Small
What driver is the clarity print method using?


Riley Peterson
PDF-xchange


Jacob Small
yeah - might be that their print driver is a bit faster; could also be that it's using another method of generation so it isn't a 1:1 comparison. Are they both all vector or is one doing raster output?


####<a name="2"></a> Code samples for working with Family Instances.

Code samples for working with Family Instances.
https://help.autodesk.com/view/RVT/2025/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Revit_Geometric_Elements_Family_Instances_Code_Samples_html

####<a name="2"></a> llm arena

llm arena
https://arena.lmsys.org/
LMSYS Chatbot Arena (Multimodal): Benchmarking LLMs and VLMs in the Wild
Chatbot Arena Leaderboard
We've collected 1,000,000+ human votes to compute an LLM Elo leaderboard for 100+ models. Find out who is the 🥇LLM Champion here!

####<a name="2"></a> Apple Intelligence Foundation Language Models

Apple Intelligence Foundation Language Models
https://machinelearning.apple.com/papers/apple_intelligence_foundation_language_models.pdf
present foundation language models developed to power Apple Intelligence features, including a ∼3 billion parameter model designed to run efficiently on devices and a large server-based language model designed for Private Cloud Compute



####<a name="2"></a>


<pre><code class="language-cs">
</code></pre>



<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 546 Pixel Width: 736 -->
</center>



