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


####<a name="2"></a> Simultaneous Add-In and Revit Interacxtion

A nice opportunity to reiteraste some basic facts and consequences of the fundamental Revit add-in architecture is provided by
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit Addin without blocking Revit UI](https://forums.autodesk.com/t5/revit-api-forum/revit-addin-without-blocking-revit-ui/m-p/12951089):

**Question:**
I would like to have my Revit add-in running without blocking the Revit UI.
Should I use the MVVM design pattern for that?

In any case, how would you recommend me to make this shift and make my add-in not blocking the Revit UI?
I followed the examples of @nice3point [RevitTemplates/samples](https://github.com/Nice3point/RevitTemplates/tree/develop/samples),
but am not sure what is the best approach for me now.

**Answer:**
The most important differentiation in this context is between modal and modeless.

Please be aware that the Revit API is single-threaded and only runs within a valid Revit API context:

- [Modeless Door Lister Flaws](http://thebuildingcoder.typepad.com/blog/2011/01/modeless-door-lister-flaws.html)
- [No Multithreading in Revit](http://thebuildingcoder.typepad.com/blog/2011/06/no-multithreading-in-revit.html)
- [New Revit 2013 SDK Samples](http://thebuildingcoder.typepad.com/blog/2012/03/new-revit-2013-sdk-samples.html)
- [What's New in the Revit 2014 API](http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html)
- [Replacing an Idling Event Handler by an External Event](http://thebuildingcoder.typepad.com/blog/2013/12/replacing-an-idling-event-handler-by-an-external-event.html)
- [Multithreading Throws Exceptions in Revit 2015](http://thebuildingcoder.typepad.com/blog/2014/05/multithreading-throws-exceptions-in-revit-2015.html)
- [The Revit API is Never Ever Thread Safe](http://thebuildingcoder.typepad.com/blog/2014/11/the-revit-api-is-never-ever-thread-safe.html)
- [Happy New Year and New Beginnings!](http://thebuildingcoder.typepad.com/blog/2015/01/happy-new-year-and-new-beginnings.html)
- [PickPoint with WPF and No Threads](http://thebuildingcoder.typepad.com/blog/2015/11/pickpoint-with-wpf-and-no-threads.html)
- [Implementing the TrackChangesCloud External Event](http://thebuildingcoder.typepad.com/blog/2016/03/implementing-the-trackchangescloud-external-event.html)
- [Multi-Threading Family Instance Placement Monitor](https://thebuildingcoder.typepad.com/blog/2020/02/multi-threading-family-instance-placement-monitor.html)
- [Selection in Link, Cancel in Export, Multithreading](https://thebuildingcoder.typepad.com/blog/2020/07/selection-link-support-cancel-custom-export-multithreading.html)
- [Selection in Link, Cancel in Export, Multithreading](https://thebuildingcoder.typepad.com/blog/2020/07/selection-link-support-cancel-custom-export-multithreading.html)
- [Add-In Threads and Geometry Comparison](https://thebuildingcoder.typepad.com/blog/2023/09/add-in-threads-and-geometry-comparison.html)
- [Add-In Threads and Geometry Comparison](https://thebuildingcoder.typepad.com/blog/2023/09/add-in-threads-and-geometry-comparison.html)

A valid Revit API context is only provided by Revit in one of the numerous event handlers defined by the API and runs in the main thread of Revit.
This blocks the UI, just as you say.

You can avoid this by executing parts of your add-in in a separate modeless thread.
The modeless part can release the main thread, allow Revit to continue doing other stuff, and both Revit and the add-in can continue interacting with the user.
However, this modeless part of your add-in has no access to the Revit API.

The preferred (and almost only) way for the modeless part of your add-in to interact with the Revit API and gain access to a valid Revit API context enabling it to do so is to implement an external event.
The external event can be raised from the modeless part of the add-in.
Revit then calls the corresponding event handler and provides it with a valid Revit API context.

This is discussed in great depth and with many examples by The Building Coder in the topic group
on [Idling and External Events for Modeless Access and Driving Revit from Outside](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

The link you provided has an example "Single-project WPF Application (Modeless)", which is one way of interacting with a modeless WPF application and Revit, without blocking the UI.

**Response:**
Thank you for the detailed explanation.
What is modal though? Is it simply the pop up window using WPF? Is it related to a MVVM design pattern?
What is the advantage of using MVVM in Revit plugins?
So, basically the difference between the template of Single-project WPF Application (Modeless) and Single-project WPF Application (Modal) is that the latter blocks Revit UI?

**Answer:**
Yes, modeless doesn't block Revit, modal does.
As said and also stated in the samples Readme, in modeless variant you have to use `IExternalEventHandler` to modify the document.
You don't need to use MVVM if you are just learning, but it is a good practice and should be considered for commercial plugin development.

####<a name="3"></a> Limited PDF Printing Speed

**Question:**
I'm working on a Revit DA project to do sheet to PDF printing.
However, I'm noticing that the Revit API export function is pretty slow; my current benchmark reports about 100 sheets / hour to a combined PDF.
Does anyone have tips on how the export process could be sped up?
Has anyone used an 3rd party library to plot the PDF's more quickly?
I'm having a hard time accepting that 30 seconds / sheet is the fastest we can achieve.

**Answer:**
I used to plan on 2 minutes per sheet when I was practicing... that meant that if we had to have the set ready on Friday we either had to have multiple people coordinating who's printing what, or the read deadline was Thursday and all day Friday my CPU was just making prints.
Curious to hear what you would expect here &ndash; PDF encoding isn't quick in my experience.

**Response:**
We're benchmarking against Clarity using the PDF-xchange print driver, which can do the same 300 sheet set in 1 hr; we're at 3; was hoping to get into a similar ballpark.

**Answer:**
Maybe their print driver is a bit faster; could also be that it's using another method of generation so it doesn't allow a 1:1 comparison.
Are they both all vector or is one doing raster output?

####<a name="4"></a> Family Instance Code Samples

Note to self: the help file provides some
nice [code samples for working with family instances](https://help.autodesk.com/view/RVT/2025/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Revit_Geometric_Elements_Family_Instances_Code_Samples_html) that
I was previously not aware of:

- Load a family of Tables into a Revit project
- Create instances from all symbols in a family
- Loading a single `FamilySymbol`
- Placing symbols on a host at a specific location
- Placing family instances using reference directions

####<a name="5"></a> Apple Intelligence Foundation Language Models

Apple published a paper describing
the [Apple Intelligence Foundation Language Models](https://machinelearning.apple.com/papers/apple_intelligence_foundation_language_models.pdf):

> We present foundation language models developed to power Apple Intelligence features, including a ∼3 billion parameter model designed to run efficiently on devices and a large server-based language model designed for Private Cloud Compute.
These models are designed to perform a wide range of tasks efficiently, accurately, and responsibly.
This report describes the model architecture, the data used to train the model, the training process, how the models are optimized for inference, and the evaluation results.
We highlight our focus on Responsible AI and how the principles are applied throughout the model development.

####<a name="6"></a> LLM Arena

Are yoiu interested in comparing results from various different LLMs?

The [LMSYS Chatbot Arena](https://arena.lmsys.org/) supports benchmarking LLMs and VLMs in the wild and provides a chatbot arena leaderboard:

> We've collected 1,000,000+ human votes to compute an LLM Elo leaderboard for 100+ models.

> Find out who is the LLM Champion [here](https://chat.lmsys.org/?leaderboard)!

<center>
<img src="img/arena.png" alt="Arena" title="Arena" width="600"/>
</center>



