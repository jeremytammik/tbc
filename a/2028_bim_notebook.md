<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
</head>

<!---

- BIM Interactive Notebooks
  Chuong Ho
  https://chuongmep.com/
  https://www.linkedin.com/posts/chuongmep_opensource-ai-dataanalysis-activity-7164531381107183616-TYNP?utm_source=share&utm_medium=member_desktop
  I'm thrilled to share an open-source project that I believe holds immense potential.
  It enables you to integrate and analyze Revit model data in various ways using Jupyter Notebook use mutiple progaming language under Interactive .NET.
  The project has been brought to fruition, and it's truly gratifying to see its development, as it opens up significant opportunities with AI and data analysis.
  Check out the GitHub repository to explore more:
  https://lnkd.in/gWSsWppG
  /Users/jta/a/doc/revit/tbc/git/a/img/ch_jupyter.gif

- NotebookLM
  https://notebooklm.google/
  > NotebookLM gives you a personalized AI, grounded in the information you trust.
  NotebookLM is only available in the U.S. for users 18 and up

- TemporaryGraphicsManager in Action
  https://forums.autodesk.com/t5/revit-api-forum/temporarygraphicsmanager-in-action/td-p/12566892
  W7k Revit API Experiments - TemporaryGraphicsManager
  https://youtu.be/Q7aKEocvRtk
  LinkedIn BIM Experts -- https://www.linkedin.com/feed/update/urn:li:activity:7164217836867895296?utm_source=share&utm_medium=member_desktop
  Julian Wandzilak
  Today, I would like to share a recent discovery I made while working with Revit API. As many of you know, creating new graphical elements within Revit can be quite challenging and limited. However, I stumbled upon a little-known class called TemporaryGraphicsManager.
  The TemporaryGraphicsManager allows us to add temporary graphical elements directly to the model or drawing space. These graphics are not subject to undo actions and are not permanently saved anywhere. While they won’t clutter up your project, they provide a powerful way to enhance your user experience.
  Surprisingly, I haven’t seen this class widely used in other plugins or extensions. So, when I first encountered it, I knew I had to put it to the test.
  In the initial part of my video, I demonstrate how to align title lines to previously saved points. With a simple click, you can create temporary graphics that will guide your design process and allow you to snap your title lines to them!
  In the second part of the video, I collect points and save them to an external file for future reference. The TemporaryGraphicsManager conveniently marks their locations, eliminating the need to remember which points I’ve already saved.
  Is this the easiest method of controlling title lines? Perhaps not. In an upcoming update to my Drafter tool (to which I added recently 30 days of trial), I’ll introduce further automations in this area. But one thing is certain: I’ll continue to leverage the power of TemporaryGraphicsManage.

- classify line styles built-in versus user defined
  https://forums.autodesk.com/t5/revit-api-forum/finding-user-line-styles/m-p/12566994#M76897

- The curious case of JavaScript
  https://www.linkedin.com/pulse/curious-case-javascript-sandip-jadhav-ebobf

- LLM tokenisation 2:13:35 video
  Let's build the GPT Tokenizer
  https://youtu.be/zduSFxRajkE
  <iframe width="560" height="315" src="https://www.youtube.com/embed/zduSFxRajkE?si=H0TaI7Ro1ZOpmv0i" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>

- World Model on Million-Length Video and Language with RingAttention
  https://largeworldmodel.github.io/
  > The ability to correctly answer questions about an hour-long video is pretty impressive

- Air Canada must honor refund policy invented by airline’s chatbot
  https://arstechnica.com/tech-policy/2024/02/air-canada-must-honor-refund-policy-invented-by-airlines-chatbot/

- https://diataxis.fr/
  Diátaxis
  A systematic approach to technical documentation authoring.

- amara's law -- ubunbtu

- John Burn-Murdoch of the Financial Times
  Is the west talking itself into decline?

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

### Interactive BIM Notebook and Temporary Graphics





####<a name="2"></a>


<pre><code class="language-cs">ts.Start();

</code></pre>

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 358 Pixel Width: 602 -->
</center>



####<a name="2"></a> BIM Interactive Notebooks

Hot on the foot of
Joel Waldheim Saury's [interactive .NET Revit BIM Polyglot Notebook](https://thebuildingcoder.typepad.com/blog/2024/02/net-core-c4r-views-and-interactive-hot-reload.html#4),
[Chuong Ho](https://chuongmep.com/) shares
his [BIM Interactive Notebooks](https://www.linkedin.com/posts/chuongmep_opensource-ai-dataanalysis-activity-7164531381107183616-TYNP?utm_source=share&utm_medium=member_desktop):

> I'm thrilled to share an open-source project that I believe holds immense potential.
It enables you to integrate and analyze Revit model data in various ways using Jupyter Notebook use mutiple progaming language under Interactive .NET.
The project has been brought to fruition, and it's truly gratifying to see its development, as it opens up significant opportunities with AI and data analysis.
Explore further in the

- [BIM Interactive Notebooks GitHub repository](https://github.com/jowsy/bim-net-interactive)

<center>
<img src="img/ch_jupyter.gif" alt="BIM interactive notebooks" title="BIM interactive notebooks" width="748"/> <!-- Pixel Height: 656 Pixel Width: 748 -->
</center>

####<a name="2"></a> TemporaryGraphicsManager in Action

TemporaryGraphicsManager in Action
https://forums.autodesk.com/t5/revit-api-forum/temporarygraphicsmanager-in-action/td-p/12566892
W7k Revit API Experiments - TemporaryGraphicsManager
https://youtu.be/Q7aKEocvRtk
LinkedIn BIM Experts -- https://www.linkedin.com/feed/update/urn:li:activity:7164217836867895296?utm_source=share&utm_medium=member_desktop
Julian Wandzilak
Today, I would like to share a recent discovery I made while working with Revit API. As many of you know, creating new graphical elements within Revit can be quite challenging and limited. However, I stumbled upon a little-known class called TemporaryGraphicsManager.
The TemporaryGraphicsManager allows us to add temporary graphical elements directly to the model or drawing space. These graphics are not subject to undo actions and are not permanently saved anywhere. While they won’t clutter up your project, they provide a powerful way to enhance your user experience.
Surprisingly, I haven’t seen this class widely used in other plugins or extensions. So, when I first encountered it, I knew I had to put it to the test.
In the initial part of my video, I demonstrate how to align title lines to previously saved points. With a simple click, you can create temporary graphics that will guide your design process and allow you to snap your title lines to them!
In the second part of the video, I collect points and save them to an external file for future reference. The TemporaryGraphicsManager conveniently marks their locations, eliminating the need to remember which points I’ve already saved.
Is this the easiest method of controlling title lines? Perhaps not. In an upcoming update to my Drafter tool (to which I added recently 30 days of trial), I’ll introduce further automations in this area. But one thing is certain: I’ll continue to leverage the power of TemporaryGraphicsManage.

####<a name="2"></a> Classify Line Styles Built-In vs User

An interesting example of several completely different possible approaches to
classify line styles in built-in versus user defined was finally solved
by Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [finding user line styles](https://forums.autodesk.com/t5/revit-api-forum/finding-user-line-styles/m-p/12566994):

**Question:**
I’m writing an addon that changes all the line styles of Detail Lines, Model Lines and the boarders of Filled Regions to a certain line style. But I'm having issues with distinguishing user built line styles from system / inbuilt ones.

To get all the lines styles and including the user and system ones, I use a FilteredElementCollector to find class GraphicsStyle:

IList<Element> getgs = new FilteredElementCollector(doc).OfClass(typeof(GraphicsStyle)).ToElements();


This works but returns a lot of styles some unrelated to lines, but I can use the GraphicStyleCategory then Parent then Name to find styles with the parent name “Lines”:

if (mystyle.GraphicsStyleCategory.Parent.Name == "Lines")

So now I have all the line styles, but I want to remove the system ones, which I thought were lines styles within < and >, such as <Thin Lines> or <Wide Lines>.

But, looking at the names that are returned, some have <>, some have only a > at the end and others don't have anything to distinguish them. These are the names returned:

<Room Separation>
<Insulation Batting Lines>
<Sketch>
<Lines>
<Thin Lines>
<Medium Lines>
<Wide Lines>
<Overhead>
<Hidden>
<Demolished>
<Beyond>
Boundary>
Riser>
Run>
Landing Center>
<Area Boundary>
<Hidden Lines>
<Space Separation>
<Lines>
<Lines>
<Lines>
Stair Path>
<Fabric Envelope>
<Fabric Sheets>
Railing Rail Path Lines
Railing Rail Path Extension Lines
<Centerline>
<Axis of Rotation>
<Path of Travel Lines>
<Load Area Separation>
MyStyle

Seems a little inconsistent to me... Is there any good way to pick out the 'system' line styles so that I can only find ones the user has created?


**Answer:**
One possible approach that might work is to look at their element ids. These objects are elements, stored in the BIM db, hence equipped with element ids, aren't they? The element ids are assigned one by one as things get added to the database. Hence, higher element ids are added later. They are also incremented consecutively as work progresses. While this behaviour is undocumented and not officially supported or guaranteed in any way whatsoever, it has been working like that forever, afaik. Therefore, if you determine the highest element id in your project right now, you know that everything with a higher id has been added later. Therefore, you know that all line styles with an id higher than the highest one when you started initial work on your BIM are user generated in one way or another, and all lower ones are built-in. Does this help?

**Response:**
Thanks for your response.

I think I've found another way which better does what I'm doing for. After reading more I found that I can collect all CurveElements from the project with this:

            List<CurveElement> AllCurves = new List<CurveElement>(new FilteredElementCollector(doc).OfClass(typeof(CurveElement)).ToElements().Cast<CurveElement>());

Iterate through them and use the method GetLineStyleIds() to get the GraphicsStyles... luckly these appear to be named correctly with <> denoting 'System' line styles.

**Answer:**
A category has the property `BuiltInCategory`.
The graphicalStyleCategory of a user-defined line has a built-in category value of `Invalid`:

<pre>
  StringBuilder sb = new StringBuilder();
  Category LinesCat = Category.GetCategory(doc,
    BuiltInCategory.OST_Lines);
  IEnumerable&lt;GraphicsStyle&gt; getgs
    = new FilteredElementCollector(doc)
      .OfClass(typeof(GraphicsStyle))
      .Cast&lt;GraphicsStyle>();
  foreach(GraphicsStyle gs in getgs)
  {
    Category cat = gs.GraphicsStyleCategory;
    if(cat==null || cat.Parent==null)
      continue;
    if(cat.Parent.Id.IntegerValue != LinesCat.Id.IntegerValue)
      continue;
    if(cat.BuiltInCategory == BuiltInCategory.INVALID)
    {
      sb.AppendLine(string.Format(
        "User defined Line: {0}",cat.Name));
    }
    else
    {
      sb.AppendLine(string.Format(
        "System defined Line {0} / {1}",
        cat.BuiltInCategory, cat.Name));
    }
  }
  TaskDialog.Show("debug",sb.ToString());
</pre>

####<a name="2"></a> The Curious Case of JavaScript

For an interesting overview of the evolution and power of JavaScript, Sandip Jadhav describes his personal exploration
in [The curious case of JavaScript](https://www.linkedin.com/pulse/curious-case-javascript-sandip-jadhav-ebobf).

####<a name="2"></a> Magika AI-Based File Type Classification

Unrelated to BIM, determining the type of data contained in a computer file can be surprisingly tricky.
One important utility to address that need was provided in 1973 by
the [Unix `file` command](https://en.wikipedia.org/wiki/File_(command)).
50 years later, Google now open-sourced [Magika](https://google.github.io/magika/),
an AI-based approach to this task with higher performance:

- [Magika blog post](https://opensource.googleblog.com/2024/02/magika-ai-powered-fast-and-efficient-file-type-identification.html)
- [Magika GitHub repository](Repo: )https://github.com/google/magika/)

<center>
<img src="img/magika_performance.png" alt="Magika performance" title="Magika performance" width="600"/> <!-- Pixel Height: 720 Pixel Width: 1,328 -->
</center>

####<a name="2"></a> NotebookLM

Google also introduced
the [NotebookLM experiment](https://notebooklm.google/) touting
an interface that lets you easily shift between reading a text, asking questions about it and writing with built-in AI support that can
also transform your set of notes into an outline, blog post, business plan, and more:

> NotebookLM gives you a personalized AI, grounded in the information you trust.
NotebookLM is only available in the U.S. for users 18 and up.

####<a name="2"></a> World Model on Million-Length Video and Language with RingAttention

World Model on Million-Length Video and Language with RingAttention
https://largeworldmodel.github.io/
> The ability to correctly answer questions about an hour-long video is pretty impressive

####<a name="2"></a> Airline Chatbot Invented a Refund Policy

... and [Air Canada must honor refund policy invented by airline’s chatbot](https://arstechnica.com/tech-policy/2024/02/air-canada-must-honor-refund-policy-invented-by-airlines-chatbot/).

####<a name="2"></a> LLM Tokenisation

LLM tokenisation 2:13:35 video
Let's build the GPT Tokenizer
https://youtu.be/zduSFxRajkE
<iframe width="560" height="315" src="https://www.youtube.com/embed/zduSFxRajkE?si=H0TaI7Ro1ZOpmv0i" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>

####<a name="2"></a> Diátaxis Systematic Technical Documentation

A colleague pointed out [Diátaxis](https://diataxis.fr/):

> A systematic approach to technical documentation authoring.

It applies to my work as well, and our entire team's.

####<a name="2"></a> Amara's Law

Roy Amara formulated [Amara's law](https://en.wikipedia.org/wiki/Roy_Amara#Amara's_law) in the 1960s:

<center>
<p style="font-style:italic"></p>We tend to overestimate the effects of a technology in the short run and underestimate them in the long run.
</center>

<!--

«Wir neigen daeu, die Auswirkungen einer Technologie kursfristig zu überschätzen und langfristig Zu unterschätsen.»
Diesen Satz formulierte in den Sechzigerjahren der Computerspezialist Roy Amara. Seine Beobachtung galt dabei nicht den technologischen Entwicklungen, sondern dem menschlichen Verhalten: Wir sind neugierig, aber ungeduldig. Wir lassen uns leicht von Neuem begeistern, aber sind schnell enttäuscht, wenn es nicht so gut funktioniert wie gedacht.
Natürlich stösst alles Neue immer auch auf Gegenwind, der Mensch liebt Beständigkeit und fürchtet Veränderung. Aber gerade bei Technologien, so Amaras Beobachtung, versprechen wir uns zu schnell zu viel und haben zu wenig Verständnis dafür, dass eine Innovation sich entwickeln muss. Langsam.
Ein aktuelles Beispiel? KI. Anfangs herrschte eine Mischung aus Euphorie und Untergangsstimmung. Die einen knieten vor KI wie vor einer Marienerscheinung, die anderen tanzten den Apocalypso.
Inzwischen hat sich vieles relativiert, die Begeisterung, aber auch die Befürchtungen wurden ein bisschen runtergerechnet. Amara würde sagen: Wir neiggen dazu, die Auswirkungen einer Technologie kurzfristig zu überschätzen und langfristig zu unterschätzen.
Diesen Satz, der als «Amara's Law» oder «Amaras Gesetz» bekannt wurde, kann man auf fast alle Bereiche anwenden. Zum Beispiel auf unser Gesundheitsverhalten: Wir neigen dazu, Verhaltensänderungen - eine neue Trainingsmethode, Meditation, Keto-Diät - anfangs zu überschätzen und begeistert als Allheilmittel zu betrachten, den langfristigen Nutzen aber zu unterschätzen. Der Punkt ist: Die meisten bleiben nicht lange genug bei einem Verhalten, um seinen wahren Nutzen und Ertrag zu erkennen.
Oder nehmen wir Menschen: Wir lernen eine Person kennen. Sie gefällt uns. Wir verlieben uns. Sehen sie durch eine rosarote Brille. Dann fangen wir an, Fehler zu erkennen. Was uns anfangs noch begeisterte, irritiert. Wenn wir die Phase aber durchstehen, wenn wir merken, dass die andere Person nicht perfekt ist (wir aber auch nicht), dann wendet sich das Blatt. Wenn wir uns selbst und das Gegenüber in der Unperfektion lieben lernen, entsteht eine tiefere Verbindung. Kurz: Wir neigen dazu, Menschen kurzfristig zu überschätzen und langfristig zu unterschätzen.
-->

His observation was not about technology, but human behavior: we are curious and impatient.
We get excited about new things and are quickly disappointed when they don't immediately perform as expected.
Especially from technologies, we expect too much too quickly and have little patience with the need for an innovation to develop with time.
AI is a good example: the initial mixture of euphoria and panic has rapidly faded.
Another example, our health behavior: we often initially overestimate behavioral changes &ndash; a new training method, meditation, keto diet &ndash; and enthusiastically view them as a panacea, underestimating long-term benefits.
Often, people don't stick with a behavior long enough to see its true benefits and returns.
Or, other people: we get to know a person, like her, fall in love.
Reality sets in, initially exciting aspects irritate.
Get through that phase and realise that the other person is not perfect (but neither are we), and the tide turns.
So, again, we tend to overestimate people in the short term and underestimate them in the long term.

####<a name="2"></a> Talking About Progress and Doom

John Burn-Murdoch of the Financial Times presented recently asked,
[Is the west talking itself into decline?](https://duckduckgo.com/?q=Is+the+west+talking+itself+into+decline%3F)

A recent [scientific paper](https://docs.iza.org/dp16674.pdf) used textual analysis of 173,031 works printed in England between 1500 and 1900 and found significant correlation between vocabulary and culture.

> Extending the same analysis to the present, a striking picture emerges:
over the past 60 years, the west has begun to shift away from the culture of progress, and towards one of caution, worry and risk-aversion, with economic growth slowing over the same period.
The frequency of terms related to progress, improvement and the future has dropped by about 25 per cent since the 1960s, while those related to threats, risks and worries have become several times more common.

So: don't worry, be happy!

Take a risk and innovate!

