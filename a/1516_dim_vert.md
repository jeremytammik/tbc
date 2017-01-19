<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- create vertical dimensioning
12551564 [How can I create dimension line that is not parallel to detail line]
http://forums.autodesk.com/t5/revit-api-forum/how-can-i-create-dimension-line-that-is-not-parallel-to-detail/m-p/6801271

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Today, I highlight yet another interesting Revit API discussion forum thread and list my ongoing research links on implementing a Revit API question answering system
&ndash; Creating vertical dimensioning
&ndash; Revit API QAS research continued
&ndash; QnA Maker
&ndash; First steps with DeepDive
&ndash; YodaQA, DL-Learner and OWL
&ndash; My current open questions...

Vertical Dimensioning and Revit API QAS Research #RevitAPI @AutodeskRevit #dynamobim http://bit.ly/1516_dim_vert

-->

### Vertical Dimensioning and Revit API QAS Research

Today, I highlight yet another
interesting [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
and list my ongoing research links on implementing a Revit API question answering system:

- [Creating vertical dimensioning](#2)
- [Revit API QAS research continued](#3)
- [QnA Maker](#4)
- [First steps with DeepDive](#5)
- [YodaQA, DL-Learner and OWL](#6)
- [My current open questions](#7)


####<a name="2"></a>Creating Vertical Dimensioning

A summary of the discussion thread
on [how to create dimension line that is not parallel to detail
line](http://forums.autodesk.com/t5/revit-api-forum/how-can-i-create-dimension-line-that-is-not-parallel-to-detail/m-p/6801271),
answered by Fair59:

**Question:** I am trying to create a dimension line that measures the Z component of a sloped detail line.

I can do this manually in the Revit user interface but cannot figure out how to do it with the API.

I've used [RevitLookup](https://github.com/jeremytammik/RevitLookup) to compare the differences in the dimension lines.

The `ReferenceArray` is the same; the main difference seems to be the `Curve` property which I'm assuming is set with the `Line` property when I call `NewDimension(Section, Line, ReferenceArray)`. 
 
Here is my current (failing) code:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;point3&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;417.8,&nbsp;80.228,&nbsp;46.8&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;point4&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;417.8,&nbsp;80.811,&nbsp;46.3&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;geomLine3&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;point3,&nbsp;point4&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//Plane&nbsp;geomPlane&nbsp;=&nbsp;new&nbsp;Plane(&nbsp;XYZ.BasisX,&nbsp;XYZ.Zero&nbsp;);&nbsp;//&nbsp;2016</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;geomPlane&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByNormalAndOrigin(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisX&nbsp;);&nbsp;<span style="color:green;">//&nbsp;2017</span>
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;tx&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SketchPlane</span>&nbsp;sketch&nbsp;=&nbsp;<span style="color:#2b91af;">SketchPlane</span>.Create(&nbsp;doc,&nbsp;geomPlane&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DetailLine</span>&nbsp;line3&nbsp;=&nbsp;doc.Create.NewDetailCurve(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewSection,&nbsp;geomLine3&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">DetailLine</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ReferenceArray</span>&nbsp;refArray&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ReferenceArray</span>();
&nbsp;&nbsp;&nbsp;&nbsp;refArray.Append(&nbsp;line3.GeometryCurve.GetEndPointReference(&nbsp;0&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;refArray.Append(&nbsp;line3.GeometryCurve.GetEndPointReference(&nbsp;1&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;dimPoint1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;417.8,&nbsp;80.118,&nbsp;46.8&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;dimPoint2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;417.8,&nbsp;80.118,&nbsp;46.3&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;dimLine3&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;dimPoint1,&nbsp;dimPoint2&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dimension</span>&nbsp;dim&nbsp;=&nbsp;doc.Create.NewDimension(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewSection,&nbsp;dimLine3,&nbsp;refArray&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;}
</pre>

The line I'm sending into `NewDimension` is along the Z axis, but the dimension line I'm getting back is sloped parallel to the detail line and gives the length of the entire detail line when I want just its Z component. Using RevitLookup, I see the `Curve` `Direction` has non-zero components for both Y and Z: `(0, 0.759, -0.651)`. When I create the line I want in Revit manually, the `Curve` has a `Direction` of `(0, 0, 1)` and gives just the length of the Z component of the sloped detail line. 
 
Here is an image of the section with the sloped detail line at the bottom right. It shows the dimension I created with the API (labelled with length of 0' - 9 7/32") and the one I created manually that I want to create with the API (labelled as 0' - 6"):
 
<center>
<img src="img/revit_dimension_parallel.png" alt="Creating vertical dimensioning" width="270"/>
</center>

I think I am in the correct plane. I am in the Y-Z plane with the X axis perpendicular to the plane and trying to measure a dimension in the Z direction. This works with a detail line that has end points with the same Y or Z coordinate, but is not working with the detail line with end points with different Y and Z coordinates, although the X coordinates are the same, so still in the Y-Z plane.
 
I did more debugging in the code and found that the `Dimension` object returned by `doc.Create.NewDimension` is correct and what I want. For example, it has the value string of "0' - 6\"" which is the length of the detail line in the Z direction as I expect. But after the call to `Transaction.Commit`, the `Dimension` object changes. For example, the value string changes to "0' - 9 7/32\"", which is the total length of the detail line.
 
So, the call to `Transaction.Commit` is changing the `Dimension` object and forcing the dimension line to be parallel to the detail line. I tried setting the `Dimension` property `IsLocked` to true before calling `Commit`, hoping that would fix the problem, but that does something even more odd. Here is an image to showing the new output, which has a line with the correct value string but parallel to the detail line instead of only in the Z direction:
 
<center>
<img src="img/revit_dimension_parallel_2.png" alt="Creating vertical dimensioning" width="270"/>
</center>

**Answer:** I examined your code and see that you are not using the `geomPlane` variable.
 
I found this method that will result in a "stable" dimension with a correct direction and value:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;point3&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;417.8,&nbsp;80.228,&nbsp;46.8&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;point4&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;417.8,&nbsp;80.811,&nbsp;46.3&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;geomLine3&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;point3,&nbsp;point4&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;dummyLine&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;tx&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DetailLine</span>&nbsp;line3&nbsp;=&nbsp;doc.Create.NewDetailCurve(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewSection,&nbsp;geomLine3&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">DetailLine</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DetailLine</span>&nbsp;dummy&nbsp;=&nbsp;doc.Create.NewDetailCurve(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewSection,&nbsp;dummyLine&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">DetailLine</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ReferenceArray</span>&nbsp;refArray&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ReferenceArray</span>();
&nbsp;&nbsp;&nbsp;&nbsp;refArray.Append(&nbsp;dummy.GeometryCurve.Reference&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;refArray.Append(&nbsp;line3.GeometryCurve.GetEndPointReference(&nbsp;0&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;refArray.Append(&nbsp;line3.GeometryCurve.GetEndPointReference(&nbsp;1&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;dimPoint1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;417.8,&nbsp;80.118,&nbsp;46.8&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;dimPoint2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;417.8,&nbsp;80.118,&nbsp;46.3&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;dimLine3&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;dimPoint1,&nbsp;dimPoint2&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dimension</span>&nbsp;dim&nbsp;=&nbsp;doc.Create.NewDimension(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewSection,&nbsp;dimLine3,&nbsp;refArray&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;doc.Delete(&nbsp;dummy.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;}
</pre>

**Response:** This solved the issue. I was able to adapt it to get the Y dimension of the sloped line as well. Thank you for the solution!

I wonder what happens if you delete the dummy line... will the stable reference remain? Probably not...

####<a name="3"></a>More Research on a Revit QAS

Back to my favourite current 'hobby' topic.

As I already reported, I would like to implement a question answering system for the Revit API and all aspects of Revit API usage and add-in programming:

- [Jill Watson as motivation and lots of literature on deep learning](http://thebuildingcoder.typepad.com/blog/2017/01/au-in-london-and-deep-learning.html#7)
- [Getting started implementing a question answering system](http://thebuildingcoder.typepad.com/blog/2017/01/virtues-of-reproduction-research-mep-settings-ontology.html#5)
- [First impression from IBM Bluemix](http://thebuildingcoder.typepad.com/blog/2017/01/virtues-of-reproduction-research-mep-settings-ontology.html#6)
- [Open source QAS options](http://thebuildingcoder.typepad.com/blog/2017/01/virtues-of-reproduction-research-mep-settings-ontology.html#7)
- [Building a Revit API ontology](http://thebuildingcoder.typepad.com/blog/2017/01/virtues-of-reproduction-research-mep-settings-ontology.html#8)

Here are some new additions and further steps in my exploration:


####<a name="4"></a>QnA Maker

Matt Taylor added
a [comment](http://thebuildingcoder.typepad.com/blog/2017/01/au-in-london-and-deep-learning.html#comment-3095462007) to
the last post, pointing out
the [Microsoft QnA Maker](https://qnamaker.ai) service
that supports you in building, training and publishing a simple question and answer bot based on FAQ URLs, structured documents or editorial content in minutes:

> I posted another comment on your last AI post that you may have missed.
This is the link I thought would be of interest: 
[Microsoft's new service turns FAQs into bots](http://www.infoworld.com/article/3149710/cloud-computing/microsofts-new-service-turns-faqs-into-bots.html). Good luck with your quest!

Checking out further related news, I also noticed:

- [noHold launches Sicura QuickStart](http://www.nohold.com/sicura-quickstart.html) empowering non-programmers to create their own Virtual Assistant (bot).
- [Albert](http://www.albertai.com/), a virtual assistant or bot that can ingest information from a document; looks similar to a live chat application, but end users interact with artificial intelligence (AI) instead of a person.

Yes, these do indeed look similar to my goal.

It seems I am on to a hot topic here...

####<a name="5"></a>First Steps with DeepDive

I installed [Stanford DARPA DeepDive](http://deepdive.stanford.edu/), ran through its tutorial and looked at some related papers:

- [Managing input data and data products](http://deepdive.stanford.edu/ops-data)
- [DeepDive papers](http://deepdive.stanford.edu/papers)
- [DeepDive: A Data Management System for Automatic Knowledge Base Construction](http://cs.stanford.edu/people/czhang/zhang.thesis.pdf)
- [HazyResearch DeepDive chat](https://gitter.im/HazyResearch/deepdive)
- [Tutorial: Entity Linking for Locations](https://github.com/raphaelhoffmann/tutorials) detects
mentions of geographic locations (in 20k Reuters news articles) and links these unambiguously to
a [Wikidata](http://www.wikidata.org) database of locations.
- Tips on [extracting text from an existing docx file using `python-docx`](http://stackoverflow.com/questions/25228106/how-to-extract-text-from-an-existing-docx-file-using-python-docx) to use as DeepDive input.

The `spouse_example` used in the DeepDive tutorial and other examples I have seen so far don't look much like Q &amp; A to me, so I took another look at alternatives.


####<a name="6"></a>YodaQA, DL-Learner and OWL

At a certain point, I went back to exploring alternative open source answering systems to see whether some other system might be more accessible:

- [Are there any automatic open source question answering system engines](https://www.quora.com/Are-there-any-automatic-open-source-question-answering-system-engines)?
- [YodaQA](https://github.com/brmson/yodaqa), and a [YodaQA slide deck](http://pasky.or.cz/dev/brmson/brmson-cig2015.pdf)
- [Hub for preprocessing of questions for YodaQA](https://github.com/brmson/hub)
- [YodaQA learned to tweet](http://petr-marek.com/blog/2016/04/03/yodaqa-learned-to-tweet/)
- [YodaQA on Docker](https://eclubprague.com/blog/)
- [Biomedical question answering using the YodaQA system: prototype notes](https://pdfs.semanticscholar.org/6060/5dc7210eb9986c7e8c037430ffb37a0da532.pdf)
- [Petr answered question on StackOverflow](http://stackoverflow.com/questions/27602489/running-java-program-by-considering-import-dependencies)
- [DragonFire](https://pypi.python.org/pypi/dragonfire) &ndash; combine [YodaQA](https://github.com/brmson/yodaqa) and [Teachable AI](http://teach.dragon.computer/gui/jquery/multibot_gui_with_chatlog.php) ([Program O](https://github.com/Program-O/Program-O)?)
- [NLIWOD &ndash; natural language interfaces for the web of data](https://github.com/AKSW/NLIWOD)
- [OpenQA open source question answering framework](https://github.com/AKSW/openQA)
- [DL-Learner](http://dl-learner.org/), a framework for supervised Machine Learning in OWL, RDF and Description Logics, [manual](http://dl-learner.org/Resources/Documents/manual.pdf)

DL-Learner sounds very much like what I need... this led me deeper into ontology again:

- [OWL Web Ontology Language](https://en.wikipedia.org/wiki/Web_Ontology_Language), built on the W3C [RDF Resource Description Framework](https://en.wikipedia.org/wiki/Resource_Description_Framework)
- [Description logic](https://en.wikipedia.org/wiki/Description_logic)
- [AquaLog](http://technologies.kmi.open.ac.uk/aqualog/), a portable question-answering system which takes queries expressed in natural language and an ontology as input and returns answers drawn from one or more knowledge bases, [demo](http://technologies.kmi.open.ac.uk/aqualog/demo.html) not working, latest publication from 2007...
- [SolrSherlock](https://github.com/SolrSherlock/SolrSherlock), last update 2013...
- [Flexible ontology population from text: the OwlExporter](https://www.researchgate.net/publication/220746985_Flexible_Ontology_Population_from_Text_The_OwlExporter)
- [Domain ontology for programming languages](http://www.scienpress.com/Upload/JCM/Vol%202_4_4.pdf) uses Protege4 and Reasoner from Stanford
- [Protégé](http://protege.stanford.edu/) [wiki](http://protegewiki.stanford.edu), a free, open-source platform that provides a growing user community with a suite of tools to construct domain models and knowledge-based applications with ontologies.
- Create an ontology for the Revit API including all the known tips and tricks for using it, collected from reference docs, blogs and public discussion forums.
- [Ontology development 101: a guide to creating your first ontology](http://protegewiki.stanford.edu/wiki/Ontology101)
- [Protege ontology library](http://protegewiki.stanford.edu/wiki/Protege_Ontology_Library) &ndash; existing ontologies

After a deeper look, it seems like an ontology might possibly be an overkill.

At least, it is insufficient on its own to implement a complex answering system.

Such as system would seem to require more generic, complex and fuzzy reactions than a pure ontology based one can provide.

I do not need to ask hard facts, like 'what is a ModelLine?'

I need fuzzier stuff like 'how do I set the view direction of a 3D view?'

Something like this looks like a good mix, but a bit dated:

- [QASYO: A Question Answering System for YAGO Ontology](http://www.sersc.org/journals/IJDTA/vol4_no2/9.pdf) (2011) &ndash; integrate natural language processing, ontologies and information retrieval technologies in a unified framework.

Looking again at YodaQA, I see that it clearly states support for all three data source types: full text, database and ontology. That sounds perfect for my purposes, so I went ahead and followed the YodaQA installation instructions. Unfortunately, I have not yet been able to get an answer from the system, because it times out accessing the YodaQA system author's knowledge base in Prague.

In fact, two existing YodaQA GitHub issues pretty precisely match my needs:

- [YodaQA issue #17 Domain Adaptations](https://github.com/brmson/yodaqa/issues/17)
- [YodaQA issue #62 How to add more full text sources](https://github.com/brmson/yodaqa/issues/62)

Also, just a few days ago,
[Charles Miller](http://ai.stackexchange.com/users/4627/charles-miller) asked
about [Using YodaQA with a non-Wikipedia source](http://ai.stackexchange.com/questions/2649/anyone-used-yodaqa) to
implement a closed-domain QAS:

> Has anyone used YodaQA for natural language processing? How easy is it to link to a document database other than Wikipedia?

> We're thinking we can create a bot to use AI to analyse our developer and user documentation and provide a written or spoken answer in reply. YodaQA comes linked to Wikipedia for starters, but we'd need to link to our own source info. I'm trying to get an idea of the development time required to set up the AI and then to link to the database.

I am eagerly awaiting an answer to that question as well...



####<a name="7"></a>My Current Open Questions on Question Answering Systems

So, all in all, I am currently exploring both DeepDive and YodaQA. 
Both seem promising and not completely straightforward to use, expand, or adapt to a closed domain.

Here the main question I currently have and would love to hear an answer on:

- Does it make sense for me to combine all three knowledge base sources, i.e., an ontology, full text and a Q &amp; A database?

Some obvious follow-up questions arise:

- If so, how do I achieve that?
- How do I create these sources, format and store them?
- How do I feed them into the existing systems, e.g., DeepDive and YodaQA?
- Should I consider some other system instead?
- Is there a simpler approach?

Let me reiterate the raw information sources available:

- The Revit API help file `RevitAPI.chm` contents, accessible [online at `revitapidocs.com`](http://www.revitapidocs.com).
- The Revit [Developer Guide](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42).
- The [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160).
- StackOverflow questions tagged with [`revit-api`](http://stackoverflow.com/questions/tagged/revit-api) and [`revitpythonshell`](http://stackoverflow.com/questions/tagged/revitpythonshell).
- [The Building Coder](http://thebuildingcoder.typepad.com) and other blog content.
- Emails and other sources.

The mapping between these raw sources and the target knowledge base components appears obvious to me:

- Ontology &ndash; generate automatically (by programming) from RevitAPI.chm.
- Full text &ndash; generate from developer guide and blog posts.
- Q &amp; A database &ndash; generate from discussion forum threads and blog posts containing clearly marked pairs of questions and answers.

