<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- AU london http://au.autodesk.com/london?linkId=33228124

- http://02f0a47.netsolhost.com/images/Rules_of_Thumb_for_Change_Agents.pdf

- [How Invisible Interfaces are going to transform the way we interact with computers](https://medium.com/startup-grind/how-invisible-interfaces-are-going-to-transform-the-way-we-interact-with-computers-39ef77a8a982#.cy69o7yo7)

- https://medium.freecodecamp.com/what-i-learned-from-analyzing-the-top-253-medium-stories-of-2016-9f5f1d0a2d1c

- http://forums.autodesk.com/t5/revit-api-forum/hermite-to-bezier-or-nurbs/m-p/6789868

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

I took a first look at deep learning for question answering systems. Before that, I share some news on AU in Europe, thoughts on UI and innovation, and point out a useful Revit API feature for converting a Hermite spline to Bezier
&ndash; Autodesk University in London
&ndash; Rules of thumb for change agents
&ndash; Invisible user interfaces
&ndash; Conclusions from lexical analysis of top news stories
&ndash; Converting a Revit Hermite spline to Bezier or NURBS
&ndash; Lots of literature on deep learning...

Deep learning for question answering systems, #AULondon, #UI, #innovation, #RevitAPI, @AutodeskRevit bit.ly/2j7Sxkb

-->

### AU in London and Deep Learning

I took a first look at deep learning for question answering systems.

Before that, I share some news on AU in Europe, thoughts on UI and innovation, and point out a useful Revit API feature for converting a Hermite spline to Bezier:

- [Autodesk University in London](#2)
- [Rules of thumb for change agents](#3)
- [Invisible user interfaces](#4)
- [Conclusions from lexical analysis of top news stories](#5)
- [Converting a Revit Hermite spline to Bezier or NURBS](#6)
- [Lots of literature on deep learning](#7)


####<a name="2"></a>Autodesk University in London

[AU London](http://au.autodesk.com/london) is
coming to Tobacco Dock, London, E1 on June 21-22, 2017.

The first English speaking Autodesk University in Northern Europe!

The [Call for Papers](http://gems.autodesk.com/events/autodesk-university-london-2017-cfp/event-summary-c3a8107e83504cf1beb09c9fe183a1bb.aspx) is already live and is closing very soon, on January 20, 2017.

Please submit a class paper on your area of expertise.

If you have any questions, please reach out to the [AU London Team](mailto:au.london.team@autodesk.com).

<center>
<img src="img/au_london.jpg" alt="AU London" width="450"/>
</center>



####<a name="3"></a>Rules of Thumb for Change Agents

Herb Shepard shares eight [rules of thumb for change agents](http://02f0a47.netsolhost.com/images/Rules_of_Thumb_for_Change_Agents.pdf) that
I think also apply very well to the art and craft of software development:

<ol type="I">
<li>Stay alive.</li>
<li>Start where the system is.</li>
<li>Never work uphill.<br/>Corollaries:</li>
<ol>
<li>Don't build hills as you go.</li>
<li>Work in the most promising arena.</li>
<li>Build resources.</li>
<li>Don't over-organize.</li>
<li>Don't argue if you can't win.</li>
<li>Play God a little.</li>
</ol>
<li>Innovation requires a good idea, initiative and a few friends.</li>
<li>Load experiments for success.</li>
<li>Light many fires.</li>
<li>Keep an optimistic bias.</li>
<li>Capture the moment.</li>
</ol>

These aphorisms are not so much bits of advice as things to think about when you are being a change agent, a consultant, an organization or community development specialist &ndash; or when you are just being yourself trying to bring about something that involves other people.

<p style="font-size: 80%"><i>Herb Shepard was a pioneering thinker in the Organization Development movement, founded and directed the first doctoral program in Organization Development at Case Western, developed a residency in administrative psychiatry at Yale University School of Medicine, and was also President of The Gestalt Institute of Cleveland. In management consulting, his clients included some of the biggest firms in the world, as well as many federal government departments of the United States and Canada.</i></p>

####<a name="4"></a>Invisible User Interfaces

A short note to point out this interesting article
on [how invisible interfaces are going to transform the way we interact with computers](https://medium.com/startup-grind/how-invisible-interfaces-are-going-to-transform-the-way-we-interact-with-computers-39ef77a8a982#.cy69o7yo7).

####<a name="5"></a>Conclusions from Lexical Analysis of Top News Stories 

Another short note to point out that it is possible to draw useful conclusions from pure statistical lexical analysis: 
[lessons learned from analysing the top 252 stories of 2016](https://medium.freecodecamp.com/what-i-learned-from-analyzing-the-top-253-medium-stories-of-2016-9f5f1d0a2d1c).

####<a name="6"></a>Converting a Revit Hermite Spline to Bezier or NURBS

A finally, yet another short Revit related note to highlight a new answer to 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [Hermite to Bezier (or NURBS)](http://forums.autodesk.com/t5/revit-api-forum/hermite-to-bezier-or-nurbs/m-p/6789868):

**Question:** I'm trying to find a way to convert a Hermite Spline to a Bezier Spline.
Take a flexible pipe (or duct) as an example:

<center>
<img src="img/140602_hermite_spline_to_bezier.png" alt="Hermite Spline to Bezier" width="425"/>
</center>

It seems Autodesk must have conversion routines in the core as DWG export provides a NURBS curve.

**Answer:** Check the Revit 2017 API. It introduces
new [`NurbSpline` methods](http://www.revitapidocs.com/2017/cfb90f78-dc27-a305-031d-5e4ffac8a9ce.htm) for
converting a Hermite spline to `NurbSpline` or `Curve`:

- [Create (HermiteSpline)](http://www.revitapidocs.com/2017/63ae2cbb-72eb-0e56-2d04-c7edb82a43cd.htm) &ndash; create a new geometric `NurbSpline` object from a `HermiteSpline`.
- [CreateCurve (HermiteSpline)](http://www.revitapidocs.com/2017/c0191746-fdd6-4f24-cfb7-29a6a88bc05b.htm) &ndash; create a new geometric `Curve` object by converting the given `HermiteSpline`. The created curve may be a `NURBSpline` or a simpler curve such as line or arc.


####<a name="7"></a>Lots of Literature on Deep Learning

As I mentioned last month, I would like to experiment with an [automated system to answer the most common recurring questions on the Revit API](http://thebuildingcoder.typepad.com/blog/2016/10/ai-edit-and-continue.html#3).

The most inspiring motivation out there for me so far is 
the [deep learning based teaching assistant Jill Watson](http://www.news.gatech.edu/2016/05/09/artificial-intelligence-course-creates-ai-teaching-assistant) implemented
by Professor Ashok Goel, who teaches Knowledge Based Artificial Intelligence (KBAI) at the Georgia Tech School of Interactive Computing.

Here is a quote from a pre-Jill paper by Ashok Goel et al
on [Using Watson for Enhancing Human-Computer Co-Creativity](http://www.aaai.org/ocs/index.php/FSS/FSS15/paper/viewFile/11713/11472) in
which Jill is mentioned as work in progress:

<!---- by Ashok Goel, Brian Creeden, Mithun Kumble, Shanu Salunke, Abhinaya Shetty, Bryan Wiltgen at the Design & Intelligence Laboratory, School of Interactive Computing, Georgia Institute of Technology; goel@cc.gatech.edu, brian.creeden@gatech.edu, mkumble3@gatech.edu, salunkeshanu91@gmail.com, shetty_abhinaya@yahoo.co.in, bryan.wiltgen@gatech.edu ---->

<i>We are presently exploring the use of Watson as a cognitive system for answering frequently asked questions in an online class. Goel ... teaches an online course ... as part of Georgia Tech’s Online MS in CS program. The online course uses [Piazza](https://piazza.com) as the forum for online class discussions. The classroom discussions on the Piazza forum in the KBAI course tend to be both extensive and intensive, attracting about ~6950 and ~11,000 messages from ~170 and ~240 students in the Fall 2014 and the Spring 2015 classes, respectively. We believe that the large numbers of messages in the discussion forums are indicative of the strong motivation and deep engagement of the students in the KBAI classes. Nevertheless, these large numbers also make for significant additional work for the teaching team that needs to monitor all messages and answer a good subset of them. Thus, using the questions and answers from the Fall 2014 and Spring 2015 KBAI classes, we are developing a new Watson-powered technology to automatically answer frequently asked questions in future offerings of the online KBAI class.</i>

The result was successful and inspiring, cf. Ashok's [20-minute presentation about a teaching assistant named Jill Watson](https://youtu.be/WbCguICyfTA) at TEDx in San Francisco:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/WbCguICyfTA?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

[Jill Watson, round three](http://www.news.gatech.edu/2017/01/09/jill-watson-round-three),
is coming up now as the Georgia Tech course prepares for its third semester with virtual teaching assistants.

I spent some time yesterday browsing the Internet on this topic and discovered an overwhelming amount of interesting information, most of which I just skimmed over.

Here is the table of contents, mainly for my personal use and future reference:

- [Natural language understanding](http://www.jfsowa.com/talks/nlu.pdf) including interesting facts on Google Translate
- [What Watson is about](https://groups.google.com/forum/#!topic/ontolog-forum/SjYwxXr7xBQ)
- [Is Big Data Taking Us Closer to the Deeper Questions in Artificial Intelligence?](https://www.edge.org/conversation/gary_marcus-is-big-data-taking-us-closer-to-the-deeper-questions-in-artificial) &ndash; long conversation with  Gary Marcus in April 2016.
- [Deep Learning Winter School References](http://dl4mt.computing.dcu.ie/DL4MT-ref.html)
- [Deeplearning4j](https://deeplearning4j.org/) &ndash; commercial-grade, open-source, distributed deep-learning library written for Java and Scala.
- [List of open-source deep learning frameworks](http://venturebeat.com/2015/11/14/deep-learning-frameworks/)
- [Facebook Open-Sources Deep Learning Project Torchnet](https://www.infoq.com/news/2016/07/facebook-opensource-torchnet)
- [FAIR open sources deep-learning modules for Torch](https://research.fb.com/fair-open-sources-deep-learning-modules-for-torch/)
- [RNN &ndash; recurrent neural network](https://en.wikipedia.org/wiki/Recurrent_neural_network)
- [Deep Character-Level Neural Machine Translation](https://github.com/swordyork/dcnmt) based on Theano and Blocks in Python.
- [Watson natural language question answering computer system](https://en.wikipedia.org/wiki/Watson_%28computer%29)
- [IBM Watson](https://www.ibm.com/watson/)
- [Watson Developer Cloud Node.js SDK](https://www.npmjs.com/package/watson-developer-cloud)
- [Question answering](https://en.wikipedia.org/wiki/Question_answering)
- [OpenEphyra](https://github.com/TScottJ/OpenEphyra), an open framework for question answering (QA).
- [Sempre](https://github.com/percyliang/sempre) &ndash; Semantic Parser with Execution.
- [Qanus Question-Answering by NUS](http://www.qanus.com/), National University of Singapore.
- [Adam QAS](https://github.com/5hirish/adam_qas)
- [Yodaqa](https://github.com/brmson/yodaqa) &ndash; A Question Answering system built on top of the Apache UIMA framework &rarr; [web site](http://ailao.eu/yodaqa).
- [Uncc2014watsonsim](https://github.com/SeanTater/uncc2014watsonsim) &ndash; Open-domain question answering system from UNC Charlotte.
- [Watsonsim](http://blog.watsonphd.com)
- [Domain Specific Question Answering System](http://www.irdindia.in/journal_ijeecs/pdf/vol3_iss2/8.pdf): Closed-Domain QA System: Closed-domain question answering deals with the questions under a specific domain, and can be seen as an easier task because NLP systems can exploit domain-specific knowledge frequently formalized in ontologies. It has very high accuracy but requires extensive language processing and limited to one domain. The example of such a system is medicines or automotive maintenance.
- [A Question Answering System on Domain Specific Knowledge with Semantic Web Support](https://www.researchgate.net/publication/261795194_A_question_answering_system_on_domain_specific_knowledge_with_semantic_web_support) &ndash; for Slovenian.
- [Creating a Natural Language Question Answering System with IBM Watson](https://fartashh.github.io/post/qa-system-watson/) provides some practical implementation guidance and real-world advice.
- [Creating Domain-specific Question Answering Systems](http://datascience.stackexchange.com/questions/12225/creating-domain-specific-question-answering-systems) &ndash; In short, the typical approach people took: (i) build a knowledge base from domain specific articles and/or Wikipedia (ii) index these articles with lucene or other IR system (iii) for each question/answer pair, retrieve most relevant articles and use them as features (iv) build a classifier using these features to classifying whether an answer is correct or incorrect.
- [Approaches for implementing Domain-specific Question answering Systems](http://datascience.stackexchange.com/questions/12248/approaches-for-implementing-domain-specific-question-answering-system) &ndash; IR == Information retrieval, Ontology based, Machine learning; all the best systems (e.g., IBM Watson) use a hybrid approach.

The last three are most practical, down to earth and provide quick and easily accessible introductions including practical implementation aspects.

My next moves might be registering for an IBM Watson account and starting to assemble test cases and a base data set for training.

