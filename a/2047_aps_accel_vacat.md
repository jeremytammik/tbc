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

- blog about barcelona accelerator:

- revisiting q4r4 with llm and rag
  Breaking up is hard to do: Chunking in RAG applications
  https://stackoverflow.blog/2024/06/06/breaking-up-is-hard-to-do-chunking-in-rag-applications/

- graphrag -- https://youtu.be/r09tJfON6kE

- claude.ai helped chunk tbc blog posts

- vacation

twitter:

APS accelerator in Barcelona and using AI to implement chunking of TBC blog posts for LLM RAG for the @AutodeskRevit #RevitAPI question answering system Q4R4 #BIM @DynamoBIM https://autode.sk/q4r4chunk

Vacation time, accelerator time, and chunking TBC for LLM RAG
&ndash; APS accelerator Barcelona in September
&ndash; Q4R4 with LLM and RAG
&ndash; Claude.ai helped chunk TBC blog posts
&ndash; Vacation...

linkedin:

APS accelerator in Barcelona and using AI to implement chunking of TBC blog posts for LLM RAG for the #RevitAPI question answering system Q4R4

https://autode.sk/q4r4chunk

- APS accelerator Barcelona in September
- Q4R4 with LLM and RAG
- Claude.ai helped chunk TBC blog posts
- Vacation...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### APS Accelerator and Q4R4 Chunking with Claude

Vacation time, accelerator time, and chunking TBC for LLM RAG:

- [APS accelerator Barcelona in September](#2)
- [Q4R4 with LLM and RAG](#3)
- [Claude.ai helped chunk TBC blog posts](#4)
- [Vacation](#5)

####<a name="2"></a> APS Accelerator Barcelona in September

Time to plan not just your summer vacation, but what follows after as well.

Registration is open for the next European face-to-face APS accelerator, scheduled to take place in the Autodesk offices on the beach in Barcelona September 23-27.

Very attractive and popular, all of the items above: APS itself, the accelerators, the office, the beach, and the location Barcelona.
Address and directions: [Autodesk SA, Carrer de Josep Pla 2, Torre B2, Planta 6, 08019 Barcelona, Spain](https://www.google.com/maps/dir//C/+Josep+Pla+2+Building+B2+Sant+Marti+08019+Barcelona+Spain)

In the accelerator, you will benefit from dedicated time to develop your own chosen Autodesk Platform Services application with direct live help and training from my APS engineering expert colleagues to help creative developers leverage the [Autodesk Platform Services Cloud APIs](https://aps.autodesk.com), i.e., your choice of the following APS APIs:

- Authentication (OAuth)
- Autodesk Construction Cloud (ACC)
- BIM 360
- Data Exchange
- Data Management
- Data Visualization
- Forma
- Model Derivative
- Parameters
- Premium Reporting
- Reality Capture
- Token Flex
- Viewer
- Webhooks

Last but not least, of course, we cover the desktop .NET *Revit API* as well as the *APS Design Automation APIs* for 3ds Max, AutoCAD, Inventor and Revit.

The deadline for submitting your proposal is Friday, September 13, 2024:

- [Registration](https://www.eventbrite.com/e/autodesk-platform-services-accelerator-barcelona-september-23-27-2024-tickets-866126125557)
- [Other accelerators](https://aps.autodesk.com/accelerator-program)

<center>
<img src="img/aps_barca_2024.png" alt="APS accelerator Barcelona" title="APS accelerator Barcelona" width="400"/>
</center>

####<a name="3"></a> Q4R4 with LLM and RAG

I spent some time in 2017
pondering [Q4R4](https://thebuildingcoder.typepad.com/blog/r4q4/),
*Question Answering for Revit API*, a Revit API question answering system.

That was before the advent and rapid advancement of LLMs in more recent years.

Now, it is probably much simpler to achieve a better solution making use of the new technologies.

Some useful sources for priming an LLM with Revit API knowledge might be:

- The Revit API help file, originally Windows CHM, available online at [reviapidocs](https://www.revitapidocs.com/)
- The Revit API developers guide, available online within the Revit help at [Revit API developers guide](https://help.autodesk.com/view/RVT/2025/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html)
- [The Building Coder blog; published version](https://thebuildingcoder.typepad.com/)
- The Building Coder HTML and markdown source files on GitHub [tbc](https://github.com/jeremytammik/tbc)
- [The Revit API discussion forum](https://forums.autodesk.com/t5/revit-api-forum/bd-p/160)

Some of that material could be fed in directly from the sources; other parts might need scraping from the web.

One useful approach to integrate this Revit API domain-specific data with a base LLM is [RAG, retrieval-augmented generation](https://duckduckgo.com/?q=rag+llm).

So, for instance, I would like to prepare The Building Coder blog post sources for RAG, cf.:

- [Breaking up is hard to do: chunking in RAG applications](https://stackoverflow.blog/2024/06/06/breaking-up-is-hard-to-do-chunking-in-rag-applications/).
- [Graphrag](https://youtu.be/r09tJfON6kE)

####<a name="4"></a> Claude.ai Helped Chunk TBC Blog Posts

I asked [Claude](https://claude.ai/) to chunk The Building Coder blog posts for LLM RAG with the following series of prompts:

- how would you suggest chunking this markdown-formatted blog post, splitting it up into separate documents delineated by the `####` `h4` section headers?
- that sounds good. how would you handle the same task automatically for 2046 blog posts?
- could you suggest how to code this in Python, please?
- actually, please improve the script as follows: split the input MD files into chunks using all headers as separators, and store the output in JSON files. each JSON should contain the following fields: original filename, header text, local header `href`, and chunk text.
- the script you provided misses many of the section headers, because they have a href html tag directly joined to the markup header hash characters, like this: `####<a name="2"></a> Personalised Material Asset Properties`

The script generated 696 json files, one for each blog posts from number 1351 to today's number 2046

The result looks perfect.
I corrected nothing whatsoever, didn't even look at the code generated.
All I did was type in the input and output folder paths.

The earlier blog posts until number 1350 were written in HTML, so they require a different script for chunking.
I went on to ask how to process those using the following prompts:

- that worked very well, and the result looks good. i also have a collection of older blog posts that i wrote in html instead of markdown. could you please write a similar script to chunk up the html blog posts in a similar way to the same json format?
- that script worked fine for a few of the files, but then it produced the following error:
    - UnicodeDecodeError: 'utf-8' codec can't decode byte 0xe9 in position 4049: invalid continuation byte`
- i'm afraid that made things worse. now it produces an error in the very first file, saying:
    - File "/Users/jta/a/src/python/tbcchunk/tbcchunk3.py", line 34, in chunk_html: `for elem in soup.body.children`: AttributeError: 'NoneType' object has no attribute 'children'

After that, all was well, all 2046 blog posts processed and chunked.

If you are interested in seeing the code produced by Claude and the blog post chunks generated, you can check it out in
my [tbcchunk GitHub repository](https://github.com/jeremytammik/tbcchunk).

####<a name="5"></a> Vacation

I am on vacation next week, on a bike tour (my first) in the Massiv Central in France.

So, you and
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) will
be left to your own devices for a while.

<center>
<img src="img/rags.png" alt="Rags" title="Rags" width="400"/>
</center>
