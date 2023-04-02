<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-ideas/change-project-units-between-imperial-and-metric-with-one-button/idc-p/11852849#M50519
  https://github.com/jeremytammik/UnitMigration/releases/tag/2021.0.0.0
  https://github.com/jeremytammik/UnitMigration/releases/tag/2021.0.0.1
  https://github.com/jeremytammik/UnitMigration/releases/tag/2023.0.0.0
  https://github.com/jeremytammik/UnitMigration/releases/tag/2023.0.0.1

- What is UV?
  https://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-6144595024

- What is fuzz
  https://forums.autodesk.com/t5/revit-api-forum/analytical-node-vs-analytical-member-coordinate-accuracy-issue/m-p/11848971

- Whats the deal with get_Parameter()? (insert Seinfeld Bassline here)
  https://forums.autodesk.com/t5/revit-api-forum/whats-the-deal-with-get-parameter-insert-seinfeld-bassline-here/m-p/11845778

- https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2023.1.153.6
  https://autodesk.slack.com/archives/C0SR6NAP8/p1679402072325839
  https://forums.autodesk.com/t5/revit-api-forum/doc-enableworksharing-amp-language-versions/m-p/11845252

- Eric Boehlke shares some of his interesting experiences
  [programming Python and Dynnamo with Bing chat AI as helper]
  https://revthat.com/programming-with-bing-chat-ai-as-helper/

- Claude, now in Slack
  https://www.anthropic.com/index/claude-now-in-slack
  the app can summarize threads, answer questions, and more

- GPT4 AGI?
  https://autodesk.slack.com/archives/C016D5HE66T/p1679589579479459
  Good to be skeptical when someone says AGI in relation to current work
  But here is a 154 page (101 + appendix) paper by Microsoft Research that came out yesterday
  A lot of great capability assessments in here!
  https://arxiv.org/abs/2303.12712
  arXiv.orgarXiv.org
  Sparks of Artificial General Intelligence: Early experiments with GPT-4
  Artificial intelligence (AI) researchers have been developing and refining
  large language models (LLMs) that exhibit remarkable capabilities across a
  variety of domains and tasks, challenging our understanding of learning and
  cognition. The latest model developed by OpenAI, GPT-4, was trained using an
  unprecedented scale of compute and data.

twitter:

Emergence, sparks of artificial general intelligence, switching metric + imperial units, UV, fuzz, and the .NET property get_ prefix in the #RevitAPI @AutodeskRevit #BIM @DynamoBIM @AutodeskAPS https://autode.sk/uv_fuzz_get

Current topics of interest from the Revit API discussion forum and elsewhere
&ndash; Switch metric + imperial units
&ndash; What is UV?
&ndash; What is fuzz?
&ndash; What is get_Parameter?
&ndash; Default localised workset names
&ndash; Bing Chat Python and Dynamo tutor
&ndash; Claude on Slack summarises and answers questions
&ndash; Emergence and sparks of artificial general intelligence...

linkedin:

Emergence, sparks of artificial general intelligence, switching metric + imperial units, UV, fuzz, and the .NET property get_ prefix in the #RevitAPI

https://autode.sk/uv_fuzz_get

- Switch metric + imperial units
- What is UV?
- What is fuzz?
- What is get_Parameter?
- Default localised workset names
- Bing Chat Python and Dynamo tutor
- Claude on Slack summarises and answers questions
- Emergence and sparks of artificial general intelligence...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### UV, Emergence, Fuzz and the get_ Prefix

Here are some recent topics of interest that came up in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) and elsewhere:

- [Switch metric + imperial units](#2)
- [What is UV?](#3)
- [What is fuzz?](#4)
- [What is get_Parameter and get_Geometry?](#5)
- [Default localised workset names](#6)
- [Bing chat Python and Dynamo tutor](#7)
- [Claude on Slack summarises and answers questions](#8)
- [Sparks of artificial general intelligence](#9)


####<a name="2"></a> Switch Metric + Imperial Units

The Revit Idea Station wish list entry
to [change project units between imperial and metric with one button click](https://forums.autodesk.com/t5/revit-ideas/change-project-units-between-imperial-and-metric-with-one-button/idc-p/11852849) has
already been addressed by providing
the [UnitMigration add-in solution and source code](https://github.com/jeremytammik/UnitMigration).
However, since some people lack the possibility to compile it, I shared updated compiled .NET assemblies
for [Revit 2021](https://github.com/jeremytammik/UnitMigration/releases/tag/2021.0.0.1)
and [Revit 2023](https://github.com/jeremytammik/UnitMigration/releases/tag/2023.0.0.1) that
can be downloaded and installed right out of the box.

####<a name="3"></a> What is UV?

Angelo Mastroberardino [commented](https://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-6144595024)
on [Planes, Projections and Picking Points](https://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html) and
the [revitapidocs `ComputeDerivatives` method](https://www.revitapidocs.com/2021.1/77ca18ef-783e-9db5-a37a-2d76f637d1a1.htm) asking:

**Question:** What is the point `UV`?
What is its reference frame and origin?
Are these `UV` the same (x,y) coordinates of any `XYZ` points of the `Face.EdgeLoops` without the z-coordinate?

**Answer:** The best place to start understanding UV coordinates is Scott Conover's 2010 AU class
on [Analysing Building Geometry](https://thebuildingcoder.typepad.com/blog/2010/01/analyse-building-geometry.html),
spread out over ten or so blog posts.

Read that and all will be clear.
Drilling down into further detail and practical application, here is an article explaining
the [relationship between 2D UV and 3D XYZ coordinates](https://thebuildingcoder.typepad.com/blog/2011/03/converting-between-2d-uv-and-3d-xyz-coordinates.html).

To round off, here is a nice debugging explanation showing how you
can [use AVF to document and label the UV coordinates](https://thebuildingcoder.typepad.com/blog/2020/12/dynamo-book-and-texture-bitmap-uv-coordinates.html#2).

####<a name="4"></a> What is Fuzz?

Confusion around comparison of floating point values persists, and came up again in the question
on [analytical node vs analytical member coordinate accuracy](https://forums.autodesk.com/t5/revit-api-forum/analytical-node-vs-analytical-member-coordinate-accuracy-issue/m-p/11848971):

**Question:** I came across a puzzling inconsistency with the new Analytical Members vs Analytical Nodes.
It seems that somewhere in the guts of Revit, the geometry curve end point coordinates that get reported get mysteriously rounded, but the `ReferencePoint` coordinates do not.
In my case, the AnalyticalNode ReferencePoint X Coordinate is 100.000002115674, but the AnalyticalMember Line.EndPoint(0) sees it's X coordinate as 100.000000000.
I'm no expert, but that seems bad to me; even though it's very small numbers, any operation looking for A==B is not going to have any success unless someone knew ahead of time that they needed to go on and round down to 5 decimal places before doing a comparison.

**Answer:** Yes.
In fact, anyone experienced in the comparison of real floating-point numbers does already know that you [need to perform some rounding operation when comparing any and all such numbers on a digital computer](https://duckduckgo.com/?q=comparing+floating+point+number).
That is standard.
The Building Coder quite regularly repeats the [need for fuzz](https://www.google.com/search?q=fuzz&as_sitesearch=thebuildingcoder.typepad.com).

In the case of Revit, matters are even worse than in some other areas, since the Revit database represents property values and dimensions such as length using `float` instead of `double`.
Hence, the need to [think big in Revit](https://thebuildingcoder.typepad.com/blog/2009/07/think-big-in-revit.html) and ignore every deviation below a certain (quite large) tolerance as irrelevant to the BIM, any "length below about 0.004 feet, i.e. ca. 0.05 inches or 1.2 millimetres".

Personally, when I retrieve vertex or coordinate data from Revit, I simply round it to the closest millimetre while retrieving it. I add every vertex or coordinate data item as a key to a dictionary. Every new item is looked up in the dictionary and considered equal to the existing item if it lies within a millimetre of it.

So, in my case, A==B if A-B is smaller than 1 mm.

####<a name="5"></a> What is get_Parameter and get_Geometry?

For a final FAQ reiteration, we address
[what's the deal with `get_Parameter`](https://forums.autodesk.com/t5/revit-api-forum/whats-the-deal-with-get-parameter-insert-seinfeld-bassline-here/m-p/11845778):

**Question:** This is more of a request for a history lesson here about the Revit API, but what's the deal with the method `get_Parameter`?
I've seen it used in past forum posts for use in Revit versions before 2022, but I don't see any references to it in the [revitapidocs](https://www.revitapidocs.com/).
Does anyone know where it stems from (the API, another API, the enchanted jungle of BIM?) and if still a valid method to use today in lieu of the `GetParameter` method (seems far easier to just use `get_Parameter` with a GUID vs `GetParameter`.

**Answer:** The [`GetParameter` method](https://www.revitapidocs.com/2023/9c22c68a-8fd5-850e-9aa8-cf7298ceebd0.htm) is
only available in the family definition environment, working with an RFA.
The `get_` prefix is automatically generated by .NET to enable a C# client to pass in arguments to a property call, making the [`Element.Parameter` property](https://www.revitapidocs.com/2023/a742d71a-b415-9e99-2978-abd3b5bae7f2.htm) with its various overloads taking a GUID, a parameter id or a parameter definition look like a method instead of a property.

In C#, the same prefix is also added to the [`Geometry` property](https://www.revitapidocs.com/2023/d8a55a5b-2a69-d5ab-3e1f-6cf1ee43c8ec.htm), as explained by Maxim [architect.bim](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4552025) Stepannikov
in [ImportInstance Geometry vs get_Geometry](https://forums.autodesk.com/t5/revit-api-forum/importinstance-geometry-vs-get-geometry/m-p/11720577):

**Question:** I am working thru some sample code to get the geometry from an AutoCAD import instance and I have a question about the `get_Geometry` call.
My understanding is that the code gets an `ImportInstance` element and then calls `get_Geometry` to get the geometry.
However, when I look at the Revit API docs for this object, I see only a `Geometry` property, not a `get_Geometry` property or method.
Shouldn't the code call the `Geometry` property, since that is what the API says is available?
What am I missing?

Also, when I am working in the Revit Python Shell app, the autocomplete shows that the `Geometry` property is available for use, but not `get_Geometry`.
If I try to use the `Geometry` property, I get an error, but if I try `get_Geometry`, things work.
So, why is `get_Geometry` not listed?
And why does `Geometry` return an error, <i>TypeError: indexer# is not callable</i>?

**Answer:** I you look into the documentation, you can see that `Geometry` is actually
an [indexer](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/).
To get its value, you need to specify the index in square brackets:

<pre class="prettyprint">
# Python sample
geometry = import.Geometry[Options()]
</pre>

But also any property, including the indexer, can be replaced by a method with a `get` prefix.
In this case, the value is passed in parentheses, not square brackets.
This is not mentioned in the Revit API documentation, as it is the basic syntax of IronPython and C# languages:

<pre class="prettyprint">
# Python sample
geometry = import.get_Geometry(Options())
</pre>

**Response:** Thank you for the response and for including the link to Indexers.
So, after reading the material on Indexers, I see that it is the word "this" (and maybe the square brackets) in the Revit API that indicates that ImportInstance.Geometry is an indexer.
Since I don't know C# and barely remember the original C language, it was certainly something that flew over my head.

<pre class="prettyprint">
public GeometryElement this[
  Options options
] { get; }
</pre>

Is it correct to say, the statement `y = ImportInstance.Geometry[Options()]` returns a value based on the index presented by `Options()`?
Put another way, if I had bucket of misc fruit with an indexer attached to it and wrote `y =  FruitBucket["apple"]`, the indexer would return an object associated with index of `"apple"` &ndash; that is, an `Apple` object?

Also, if there is a `get_` prefix, would there also be a `set_` prefix in IronPython?

Finally, would anyone know of a good book, or online link, that would discuss this `_get` prefix in more detail?

**Answer:** Here is a nice little booklet on the topic [.net get_ property prefix](https://duckduckgo.com/?q=.net+get_+property+prefix) for you to print at your leisure... &nbsp; :-)

####<a name="6"></a> Default Localised Workset Names

[Julian Wandzilak](https://w7k.pl) raised a thread
on [doc.EnableWorksharing &amp; language versions](https://forums.autodesk.com/t5/revit-api-forum/doc-enableworksharing-amp-language-versions/m-p/11845252) asking
for all the different language-specific default names of the two predefined default worksets.
Unfortunately, these are not provided by the Revit API, so he went ahead and implemented his own, which I also added
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples):


<div style="border: #000080 1px solid; color: #000; font-family: 'Cascadia Mono', Consolas, 'Courier New', Courier, Monospace; font-size: 10pt">
<div style="background: #f3f3f3; color: #000000; max-height: 300px; overflow: auto">
<ol start="2147" style="background: #ffffff; margin: 0; padding: 0;">
<li><span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;summary&gt;</span></li>
<li style="background: #f3f3f3"><span style="color:#808080">///</span><span style="color:#008000"> Return default workset names </span></li>
<li><span style="color:#808080">///</span><span style="color:#008000"> for all languages supported by Revit</span></li>
<li style="background: #f3f3f3"><span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;/summary&gt;</span></li>
<li><span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;param</span> <span style="color:#808080">name=&quot;</span>sLanguage<span style="color:#808080">&quot;&gt;</span><span style="color:#008000">`app.Language.ToString()`</span><span style="color:#808080">&lt;/param&gt;</span></li>
<li style="background: #f3f3f3"><span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;returns&gt;</span><span style="color:#008000">`false` if no valid language input argument provided, else `true`</span><span style="color:#808080">&lt;/returns&gt;</span></li>
<li><span style="color:#0000ff">bool</span> GetDefaultWorksetNames(</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#0000ff">string</span> sLanguage, </li>
<li>&#160;&#160;&#160; <span style="color:#0000ff">out</span> <span style="color:#0000ff">string</span> wsnLevelsAndGrids, </li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#0000ff">out</span> <span style="color:#0000ff">string</span> wsnWorkset1 )</li>
<li>{</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#0000ff">string</span>.Empty;</li>
<li>&#160;&#160;&#160; wsnWorkset1 = <span style="color:#0000ff">string</span>.Empty;</li>
<li style="background: #f3f3f3">&nbsp;</li>
<li>&#160;&#160;&#160; <span style="color:#0000ff">switch</span> (sLanguage)</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; {</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Unknown&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Shared Levels and Grids&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Workset1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;English_USA&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Shared Levels and Grids&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Workset1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;German&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Gemeinsam genutzte Ebenen und Raster&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Bearbeitungsbereich1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Spanish&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Niveles y rejillas compartidos&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Subproyecto1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;French&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Quadrillages et niveaux partagés&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Sous-projet 1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Italian&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Griglie e livelli condivisi&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Workset1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#008000">//case &quot;Dutch&quot;:</span></li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#008000">//&#160; wsnLevelsAndGrids = &quot;Shared Levels and Grids&quot;;</span></li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#008000">//&#160; wsnWorkset1 = &quot;Workset1&quot;;</span></li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#008000">//&#160; break;</span></li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Chinese_Simplified&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;共享标高和轴网&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;工作集1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Chinese_Traditional&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;共用的樓層和網格&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;工作集 1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Japanese&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;共有レベルと通芯&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;ワークセット1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Korean&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;공유 레벨 및 그리드&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;작업세트1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Russian&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Общие уровни и сетки&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Рабочий набор 1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Czech&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Sdílená podlaží a osnovy&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Pracovní sada1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Polish&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Współdzielone poziomy i osie&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Zadanie1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#008000">//case &quot;Hungarian&quot;:</span></li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#008000">//&#160; wsnLevelsAndGrids = &quot;Shared Levels and Grids&quot;;</span></li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#008000">//&#160; wsnWorkset1 = &quot;Workset1&quot;;</span></li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#008000">//&#160; break;</span></li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;Brazilian_Portuguese&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Níveis e eixos compartilhados&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Workset1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> <span style="color:#a31515">&quot;English_GB&quot;</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Shared Levels and Grids&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Workset1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">default</span>:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnLevelsAndGrids = <span style="color:#a31515">&quot;Shared Levels and Grids&quot;</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; wsnWorkset1 = <span style="color:#a31515">&quot;Workset1&quot;</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160; }</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#0000ff">return</span> 0 &lt; wsnLevelsAndGrids.Length;</li>
<li>}</li>
</ol>
</div>
</div>

Additional comments on this by Julian: I decided to implement a default language checker returns names for the two default worksets.
It was a rather tedious job, so please find it above.
I haven't double-checked all of them, so please report if something is wrong.
There are some interesting findings in it.

There is an "Unknown type" &ndash; I didn't know if I should add it (default in a switch would cover it).
In the end, I left it in the code.
What should we do to get this type from Revit API?

We have two language types in the Revit API, Hungarian and Dutch, that are not listed in
the [language help](https://help.autodesk.com/view/RVT/2023/ENU/?guid=GUID-BD09C1B4-5520-475D-BE7E-773642EEBD6C).
I assume that those language versions are being developed, so, for now, I commented them out in the code.

Also, it is interesting that some translators decided to keep `Workset1` as default &ndash; we have it not only in English, but also in Italian and Brazilian Portuguese.
I like this approach because, in my opinion,  the Polish translation `Zadanie1` is rather bad and confusing; it means something closer to "task".
Because of that, I have seen some projects where worksets were named "Tasks of Adam", "Tasks of Kate", etc.

The other interesting thing is lack of consistency regarding the number `1`.
In most languages, there is no extra space between `Workset` and `1`, but in Russian, French and traditional Chinese there is.
Chinese is double interesting, because in simplified Chinese, we don't have the empty space.

It made me wonder about how Revit is treating languages &ndash; I knew before that if for example you load a family (with family types in text file) from different language libraries you will get an error (because of a translation of default parameters like height/width/ length).
But you can open a project in a correct language, then load families into project and after opening again in your previous language you will be fine most of the time.

Is it possible to get parameter name in a currently used language?
That would be super useful for translations etc.

Also, I started to look around and decided to test the levels &ndash; I was slightly disappointed to learn that `Level.Create(doc, elevation)` adds a level with a name similar to the last created or last renamed (whatever happened last).

It works this way even if you decide to delete all the levels in a project &ndash; it is not easy, but I managed to delete them manually.
After doing it, you might get into a problem of creating new levels.
And here it starts to be interesting.
If you add a level using Revit API, Revit still keeps the same way of naming the levels &ndash; for example, you will get `Level 3` (after deleting Level 1 and 2).
But if you go to View/plan views/ you will get a pop-out which gives you a default Level 0 / Poziom 0 (I tested it also in Polish).

That would mean that Revit saves somewhere the name of the next level to create, and at the same time somewhere keeps the default level values.
Enough nerding for today.

Many thanks to Julian for taking on this laborious task and sharing his interesting thoughts and results!

####<a name="7"></a> Bing Chat Python and Dynamo Tutor

Looking at some recent AI and LLM developments, Eric Boehlke shares some of his interesting
experiences [programming Python and Dynamo with Bing chat AI as helper](https://revthat.com/programming-with-bing-chat-ai-as-helper).

####<a name="8"></a> Claude on Slack Summarises and Answers Questions

[Claude, now in Slack](https://www.anthropic.com/index/claude-now-in-slack) is
an app that can summarise threads, answer questions, and more, with a focus on reliability, albeit including a caveat...

####<a name="9"></a> Sparks of Artificial General Intelligence

My most interesting read last week was
the 154-page (101 + appendix) paper by Microsoft Research
presenting some fascinating capability assessments and discussing the open question of emergence:

[Sparks of Artificial General Intelligence: Early experiments with GPT-4](https://arxiv.org/abs/2303.12712)

> Artificial intelligence (AI) researchers have been developing and refining large language models (LLMs) that exhibit remarkable capabilities across a variety of domains and tasks, challenging our understanding of learning and cognition.
The latest model developed by OpenAI, GPT-4, was trained using an unprecedented scale of compute and data.
In this paper, we report on our investigation of an early version of GPT-4, when it was still in active development by OpenAI.
We contend that (this early version of) GPT-4 is part of a new cohort of LLMs (along with ChatGPT and Google's PaLM for example) that exhibit more general intelligence than previous AI models.
We discuss the rising capabilities and implications of these models.
We demonstrate that, beyond its mastery of language, GPT-4 can solve novel and difficult tasks that span mathematics, coding, vision, medicine, law, psychology and more, without needing any special prompting.
Moreover, in all of these tasks, GPT-4's performance is strikingly close to human-level performance, and often vastly surpasses prior models such as ChatGPT.
Given the breadth and depth of GPT-4's capabilities, we believe that it could reasonably be viewed as an early (yet still incomplete) version of an artificial general intelligence (AGI) system.
In our exploration of GPT-4, we put special emphasis on discovering its limitations, and we discuss the challenges ahead for advancing towards deeper and more comprehensive versions of AGI, including the possible need for pursuing a new paradigm that moves beyond next-word prediction.
We conclude with reflections on societal influences of the recent technological leap and future research directions.

One aspect that especially caught my attention was the topic
of [emergence](https://en.wikipedia.org/wiki/Emergence) that
is currently being observed (surprisingly) in the LLMs:

> emergence occurs when an entity is observed to have properties its parts do not have on their own, properties or behaviours that emerge only when the parts interact in a wider whole.

<center>
<img src="img/emergence_in_snowflake.jpg" alt="Emergence in snowflakes" title="Emergence in snowflakes" width="420"/> <!-- Pixel Height: 1,080 Pixel Width: 845 -->
</center>

