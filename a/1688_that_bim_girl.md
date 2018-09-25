<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

@ThatBIMGirl helps getting started with the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/thatbimgirl

That BIM Girl helps getting started with the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/thatbimgirl

I arrived in Rome Sunday afternoon and am now happy to be here at 
the Forge accelerator together
with my Autodesk colleagues and the other attending companies.
Before diving in further into the accelerator, I would like to point out the new inspiring AEC resources shared by That Bim Girl, and also post some notes to self on Q4R4
&ndash; That BIM Girl
&ndash; Notes to Self on AskNow for Q4R4...

-->

### That BIM Girl and AskNow

I ate a dubious mushroom Saturday evening with dire consequences.

In spite of that, I managed the trip to Rome Sunday afternoon and am now happy to be here at 
the [Forge accelerator](http://autodeskcloudaccelerator.com) together
with my Autodesk colleagues and the other attending companies.

Before diving in further into the accelerator, I would like to point out the new inspiring AEC resources shared by That Bim Girl, and also post some notes to self on Q4R4:

- [That BIM Girl](#2) 
- [Notes to Self on AskNow for Q4R4](#3) 

#### <a name="2"></a> That BIM Girl

No need for many words from me to present That BIM Girl,
aka [Jacqueline Rohrmann](https://www.linkedin.com/in/jacqueline-rohrmann-b17130141),
student at the [Technical University of Munich, TUM](https://en.wikipedia.org/wiki/Technical_University_of_Munich)
since she does a perfect job of it herself, e.g., in
this 43-second [trailer on coding for AEC](https://youtu.be/Ra9qIPEz-kg), [#CfAEC](https://twitter.com/search?q=CfAEC):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/Ra9qIPEz-kg" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>  
</center>

Here are some other resources, above all her main YouTube channel:

- [That BIM Girl YouTube channel](https://www.youtube.com/channel/UC7L-NLe8FUBJAKrZi2tIWlQ)
- [@ThatBIMGirl on Twitter](https://twitter.com/ThatBIMGirl)
- [Instagram](https://www.instagram.com/thatbimgirl)
- [Facebook](https://www.facebook.com/That-BIM-Girl-734890670043010)
- [LinkedIn](https://www.linkedin.com/company/thatbimgirl)

Specifically, here are the first two episodes on getting started writing a Revit add-in:

- [Coding for AEC Episode 1 &ndash; (C#, Variables, Functions, ...](https://www.youtube.com/watch?v=J8UJJTH1Ql4), 21:30
- [How to write a plugin for Revit &ndash; Coding for AEC Lesson 2](https://www.youtube.com/watch?v=ulvaJP4kjKE), 19:50

<center>
<img src="img/tbg_revit_plugin.jpg" alt="Coding for AEC Revit add-in" width="400">
</center>

#### <a name="3"></a> Notes to Self on AskNow for Q4R4

My [Q4R4 project](http://thebuildingcoder.typepad.com/blog/r4q4) has been dormant for quite a while.

However, I just noticed a research project that seems as if it could cover all that I need and more, [AskNowQA](https://github.com/AskNowQA):

The [Smart Data Analytics group](http://sda.tech) announced [AskNow 0.1, an initial release of Question Answering Components and Tools over RDF Knowledge Graphs](http://sda.cs.uni-bonn.de/asknow-0-1-released):

- [Website](http://asknow.sda.tech)
- [Demo](http://asknowdemo.sda.tech)
- [GitHub](https://github.com/AskNowQA)

The following components with corresponding features are currently supported by AskNow:

- EARL 0.1 EARL performs entity linking and relation linking as a joint task. It uses machine learning in order to exploit the Connection Density between nodes in the knowledge graph. It relies on three base features and re-ranking steps in order to predict entities and relations. 
    - [ISWC 2018](https://arxiv.org/pdf/1801.03825.pdf)
    - [Demo](https://earldemo.sda.tech)
    - [GitHub](https://github.com/AskNowQA/EARL)
- SQG 0.1: This is a SPARQL Query Generator with modular architecture. SQG enables easy integration with other components for the construction of a fully functional QA pipeline. Currently entity relation, compound, count, and Boolean questions are supported.
    - [ESWC 2018](http://jens-lehmann.org/files/2018/eswc_qa_query_generation.pdf)
    - [GitHub](https://github.com/AskNowQA/SQG)
- AskNow UI 0.1: The UI interface works as a platform for users to pose their questions to the AskNow QA system. The UI displays the answers based on whether the answer is an entity or a list of entities, Boolean or literal. For entities it shows the abstracts from DBpedia. 
    - [GitHub](https://github.com/AskNowQA/AskNowUI)
- SemanticParsingQA 0.1: The Semantic Parsing-based Question Answering system is built on the integration of EARL, SQG and AskNowUI.
    - [Demo](http://asknowdemo.sda.tech)
    - [GitHub](https://github.com/AskNowQA/SemanticParsingQA)

