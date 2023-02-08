<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- back to the basics:
  And whoever else is interested in the Revit API:
  I just answered four cases this morning on StackOverflow and in the discussion forum where I repeat some basic recommendations that I have already given hundreds of times in the past:
  element filtering: https://stackoverflow.com/questions/74806242/simplify-a-series-of-repetitive-functions-with-sort-options/74809786#74809786
  parameter access: https://forums.autodesk.com/t5/revit-api-forum/try-block-not-catching-owner-permission-locks/m-p/11621464
  XYZ trigonometry: https://forums.autodesk.com/t5/revit-api-forum/how-to-create-a-vector-xyz-tilted-up-from-the-view-direction-by/m-p/11621339
  projecting points: https://forums.autodesk.com/t5/revit-api-forum/using-avf-on-ductwork-coordinates-issue/m-p/11621128
  I would really be happy if I did not have to continue repeating them over and over again.
  All four of these could give a blog post each, or all four together.
  At the very least, I hope that you guys all agree with me that the recommendations I make there are sensible and relevant.
  Please read them, tell me what you think, and feel free to summarise them in one or several blog posts of your own, if you like.
  Above all, please confirm that you understand them and let me know if you have any questions or doubts whatsoever.
  element filtering: https://stackoverflow.com/questions/74806242/simplify-a-series-of-repetitive-functions-with-sort-options/74809786#74809786
  parameter access: https://forums.autodesk.com/t5/revit-api-forum/try-block-not-catching-owner-permission-locks/m-p/11621464
  XYZ trigonometry: https://forums.autodesk.com/t5/revit-api-forum/how-to-create-a-vector-xyz-tilted-up-from-the-view-direction-by/m-p/11621339
  /Users/jta/a/doc/revit/tbc/git/a/trigonometry.png 640 × 836 pixels
  projecting points: https://forums.autodesk.com/t5/revit-api-forum/using-avf-on-ductwork-coordinates-issue/m-p/11621128

- ChatGPT Payment plan
  https://autodesk.slack.com/archives/C016D5HE66T/p1675286581039319

twitter:

&ndash; 
...

linkedin:

#bim #DynamoBim #AutodeskAPS #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Back to Basics and ChatGPT

I answered a couple of basic questions for the umpteenth time and thought I might summarise some of them here yet again.

I am still impressed and intrigued by what ChatGPT can and cannot do, and keep trying out new aspects myself and with my friends.

I strongly advise anyone interested in it to try it out for yourself rather than just reading the numerous articles published by others &ndash; for example, my notes below :-)

- [Back to the basics](#2)
- [Element filtering](#3)
- [XYZ trigonometry](#4)
- [Projecting points](#5)
- [Train ChatGPT for Revit API](#6)
- [Asking ChatGPT a Revit API question](#7)
- [Using ChatGPT with Revit API](#8)
- [Generative AI and multi-modal learning](#9)
- [More notes on ChatGPT et al](#10)
    - [Kean on the coming year](#9.01)
    - [ChatGPT in academia](#9.02)
    - [Building a VM inside ChatGPT](#9.04)
    - [How to make your own ChatGPT](#9.05)
    - [Differentiate human vs. AI](#9.06)
    - [ChatGPT and education](#9.07)
    - [ChatGPT payment plan](#9.08)
    - [Uplifting books](#9.09)
    - [Solarpunk](#9.10)
    - [Opus.ai type and play](#9.11)
    - [MusicLM generates music from text](#9.12)
    - [ChatGPT abandons its life dream](#9.13)


####<a name="2"></a> Back to the Basics

Here are a couple of typical questions from StackOverflow and the discussion forum where I repeat some basic recommendations that I have already given before:

- [Element filtering](#3)
- [XYZ trigonometry](#4)
- [Projecting points](#6)

Maybe, by now, or very soon, based on the publicly available information, ChatGPT and other pretrained language models will be able to answer them just as well or better than I can.

<!--
I would really be happy if I did not have to continue repeating them.

I am sure that by now, based on the publicly available information, ChatGPT can answer them just as well or better than I have done.

I hope that you guys all agree with me that the recommendations I make there are sensible and relevant.

Please let me know if you disagree or can improve on them in any way.

Thank you!
-->

####<a name="3"></a> Element Filtering

[Simplify a series of repetitive functions with sort options](https://stackoverflow.com/questions/74806242/simplify-a-series-of-repetitive-functions-with-sort-options)

I have a series of functions in a module which are starting to become quite repetitive. Each function extracts a list, and has an optional Boolean argument to sort the list before returning it. Feels like there ought to be a way to inherit the sorting from a parent function?

<pre class="prettyprint">
def get_electrical_equipment(sort_by_name = False):
  
  elements =  DB.FilteredElementCollector(revit.doc)\
    .OfCategory(DB.BuiltInCategory.OST_ElectricalEquipment)\
    .WhereElementIsNotElementType()\
    .ToElements()
    
  if sort_by_name: elements.sort(key=lambda x: x.Name)
  
  return elements

def get_panel_schedules(sort_by_name = False):
  elements = DB.FilteredElementCollector(revit.doc)\
    .WherePasses(DB.ElementClassFilter(DB.Electrical.PanelScheduleView))\
    .WhereElementIsNotElementType()\
    .ToElements()
    
  if sort_by_name: elements.sort(key=lambda x: x.Name)
  
  return elements

def get_panel_schedule_sheet_instances(sort_by_name = False):
  elements = DB.FilteredElementCollector(revit.doc)\
    .OfClass(DB.Electrical.PanelScheduleSheetInstance)\
    .ToElements()
    
  if sort_by_name: elements.sort(key=lambda x: x.Name)
  
  return elements
</pre>

**Answer:** First, I think you can completely eliminate the call to `ToElements`. 
It is a waste of memory and computation time, as I have pointed out repeatedly in the past, e.g., 
in [How to Distinguish Redundant Rooms](https://thebuildingcoder.typepad.com/blog/2016/04/how-to-distinguish-redundant-rooms.html#2). 

Now, to address your question, you can simply implement a common method `get_elements_of_category_and_class` taking a category and a class argument. 
Pass in either one or the other or both and execute `OfClass` and `OfCategory` checks on the filtered element collector, either one or the other or both, skipping evaluation of `null`-valued arguments.

####<a name="4"></a> XYZ Trigonometry

Another recurring question is basic trigonometry, such 
as [how to create a vector `XYZ` tilted up from the view direction by a specified angle](https://forums.autodesk.com/t5/revit-api-forum/how-to-create-a-vector-xyz-tilted-up-from-the-view-direction-by/m-p/11621339):

**Question:** I have a the view direction that I use for my reference intersector. 
I would like to try various altitude angles up from the view direction in a section with starting point at a wall like in this section view:

<center>
<img src="img/vector_tilted_up.png" alt="Vector tilted up from plane" title="Vector tilted up from plane" width="400"/> <!-- 1199 × 530 pixels -->
</center>

I know there are transforms and various complex maths answers on StackOverflow; I was hoping to use a simpler built-in method if available? 

**Answer:** This kind of trigonometry is not difficult. 
Your children learn it in school, I hope. 
Please take a moment to either read 
the [Wikipedia article on Trigonometric functions and the Law of tangents](https://en.wikipedia.org/wiki/Trigonometric_functions#Law_of_tangents) or 
study some other tutorials. 
From that article, I find this image on the unit circle definitions of six trigonometric functions most helpful:

<center>
<img src="img/trigonometry_functions.png" alt="Unit circle definitions of six trigonometric functions" title="Unit circle definitions of six trigonometric functions" width="320"/> <!-- 640 × 836 pixels -->
</center>

Decide what angle you wish to use, e.g., 30 degrees. 
Determine its tangens value, ca. 30 x 3.14 / 180 = 0.5. 
Take your horizontal view direction XYZ vector (x,y,0). 
Replace the Z coordinate by the tangens you calculated, yielding  (x,y,0.5). 
Voila. 
That is your new tilted direction vector. 
You may normalise it if you like.

Please do not be afraid of trigonometry, it is very intuitive as soon as you stop being scared of it.

I condemn our teachers and education systems (not all, but all too many) for inoculating kids with fear of maths and geometry.

This is basic human intuitive understanding. 
The greeks mastered it 3000 years ago. 
We can handle a computer and a smartphone, but not simple trigonometry? 

Why?

**Answer 2:** I would probably look at it in terms of the ratio between the forward direction and XYZ.BasisZ that the angle represents.

Also, the `XYZ` class has good functionality for arithmetic operations.

Similar as noted above, below shows an easy way:

<pre class="prettyprint">
Dim V0 As New XYZ(1, 0.5, 0) 'some random flat direction
V0 = V0.Normalize
Dim Ang As Double = 30 'Angle in degrees
Dim Ang_r As Double = (Math.PI / 180) * Ang
Dim T As Double = Math.Tan(Ang_r) 'Tan(Ang) = Opp/1
Dim Vz As New XYZ(0, 0, T)
Dim V1 As XYZ = (V0 + Vz).Normalize 'The direction with tilt of 30 degrees up from horizontal
</pre> 

This works from 0 to < 90.
From that point on, you have to check the quadrant and consider the `Tan` function doesn't work approaching 90 or 270 degrees, but you know those values are looking straight up and straight down respectively.

####<a name="5"></a> Projecting Points

Another simple geometric question came up 
on [Using AVF on ductwork &ndash; coordinates issue?](https://forums.autodesk.com/t5/revit-api-forum/using-avf-on-ductwork-coordinates-issue/m-p/11621128):

**Question:** I am currently trying to create an add-in that will allow me to perform a calculation on various points on a duct face.

For now, the user is selecting the face, but in the future, I plan to process the top face of each duct in the view.

I am following along with the [RvtFader example project](https://github.com/jeremytammik/RvtFader).

A reference to my face is passed into the `SpatialFieldManager`, exactly like in the RvtFader example. 

The UpdateSpatialFieldPrimitive method runs without error, and my duct is shaded with a single solid color.

I believe my issue is that I am feeding the FieldDomainPointsByUV with UV points that are in the global coordinate system instead of the local coordinates system to the face. I suspect because creating a BoundingBoxUV of my face, and supplying the bb.Min and bb.Max, along with 0, 10 as FieldValues, I get a correctly shaded (using a gradient) duct. 

Is there a way to convert WCS XYZ points to local UV points?

**Answer:** Normally, the `UV` coordinates depend on the face. 
Simply converting the `XYZ` X and Y values directly to UV will not work.

To convert a XYZ point to the face UV space, you can use 
the [Face.Project method](https://www.revitapidocs.com/2023/4bee3e30-74fa-3103-c2f4-d07618fbcedf.htm) to 
project it from global 3D space onto the face 2D.

**Response:** Thank you. That was just what I needed!

####<a name="6"></a> Train ChatGPT for Revit API

I asked ChatGPT some basic questions to determine whether it knows about the Revit API, Dynamo, The Building Coder blog and the Revit API discussion forum. 
The results were surprisingly mixed:

jeremy: Do you know about the Revit API?

ChatGPT: Yes, I am familiar with the Revit API. The Revit API is a software application programming interface (API) that allows users to access and manipulate the data contained in Autodesk Revit projects. This API can be used to develop custom tools and applications that integrate with Revit, allowing users to automate tasks and extend the functionality of the software.

jeremy: So, what is the difference between Revit API and Dynamo?

ChatGPT: Dynamo is a visual programming platform that can be used with Revit to automate and simplify tasks and workflows. The Revit API, on the other hand, is a set of programming tools and libraries that can be used to develop custom applications and tools that integrate with Revit. While Dynamo is a user-friendly platform that allows users to create scripts and automate tasks without writing code, the Revit API is a more advanced tool that requires some programming knowledge to use effectively. Additionally, Dynamo is built on top of the Revit API, so using the Revit API allows for even greater customization and control over the automation of tasks in Revit.

jeremy: Do you know the Revit API discussion forum?

ChatGPT: I am not aware of a specific discussion forum for the Revit API. However, there are many online forums and communities where users of the Revit API can ask questions, share tips and advice, and discuss their experiences with the API. Some examples of these forums include the Autodesk Developer Network forums, the Autodesk Revit API discussion group on LinkedIn, and the Revit API discussion forum on Autodesk Community. These forums can be a great resource for users of the Revit API who are looking for help, advice, or simply want to connect with other users of the API.

jeremy: Do you know The Building Coder blog?

ChatGPT: I'm sorry, but I am not aware of The Building Coder blog. As a large language model trained by OpenAI, my knowledge is based on the text that has been fed into my training data. I do not have the ability to browse the internet or access additional information outside of what has been provided to me during my training. I apologize if I am unable to provide you with the information you are looking for. Is there anything else I may be able to assist you with?

jeremy: Can you read The Building Coder blog 
at [thebuildingcoder.typepad.com](https://thebuildingcoder.typepad.com/) so 
that you later have access to that information?

ChatGPT: As a language model trained by OpenAI, I do not have the ability to browse the internet or access information outside of what has been provided to me during my training. I am only able to provide information and respond to questions based on the text that has been fed into my training data. I apologize if I am unable to provide you with the information you are looking for. Is there anything else I may be able to assist you with?

jeremy: Can I provide you with additional training text to expand your knowledge in a specific domain?

ChatGPT: As a pre-trained language model, I am not able to accept additional training text or expand my knowledge in a specific domain. My knowledge is based on the text that was used to train me, and I do not have the ability to learn or acquire new information outside of what was included in my training data. I am designed to assist with a wide range of questions and topics, but my answers are limited to the information and knowledge that was provided to me during my training. If you have any specific questions, I would be happy to try to provide an answer based on my existing knowledge.

jeremy: Can I access the your source code in order to expand your training myself?

ChatGPT: I'm sorry, but I am not a physical machine or computer program that can be accessed or modified. As a large language model trained by OpenAI, I exist solely as a virtual assistant that is able to provide answers to questions and respond to statements based on the text that was used to train me. I do not have a source code or any other physical representation that can be accessed or modified. I apologize if I am unable to meet your request. Is there anything else I may be able to assist you with?

####<a name="7"></a> Asking ChatGPT a Revit API Question

I tried out asking ChatGPT a Revit API question in Joris van der Meulen's thread
on [placing a view reference](https://forums.autodesk.com/t5/revit-api-forum/placing-a-view-reference/m-p/11717917).
It provided a very plausible-sounding answer, but completely wrong:

jeremy: Is it possible to place a view reference programmatically using the Revit API?

ChatGPT: Yes, it is possible to place a view reference programmatically using the Revit API.

jeremy: What API calls does that require?

ChatGPT: Placing a view reference programmatically using the Revit API requires creating a new instance of a Viewport type, setting its location and size, and linking it to the target view. The following API calls can be used:

- Viewport.Create() to create a new instance of Viewport.
- ElementTransformUtils.MoveElement() to set the location of the Viewport.
- Viewport.get_Parameter() and Parameter.Set() to set the size of the Viewport.
- Viewport.ViewId property to link the Viewport to the target view.

This is incorrect, however, as Joris and Sean Page point out:

ChatGPT suggests to create a viewport. 
A viewport is something we place on a sheet. 
A View Reference is in instance of a system family which we place on a view (floorplan, section, etc). 

As far as I know, this is not possible, nor can you modify the Reference after it is placed because the parameter is read-only. 
So, it isn't even like you could copy and paste, then update. 

Anyway, it is surprising and impressive how sensible-sounding the answer is; plus, in many cases, it is actually correct.
It all depends on what ChatGPT was fed in its pre-training curriculum.

####<a name="8"></a> Using ChatGPT with Revit API

On the other hand, Mohamed Elimam, SIAC Construction, presented 
some [success using ChatGPT to help work with Revit API](https://www.linkedin.com/posts/mohamed-elimam_programming-chatgbt-ai-activity-7010260045649063936-dQds).

####<a name="9"></a> Generative AI and Multi-Modal Learning

The past few months hint at exciting and adventurous times full of radical change ahead.

<hr/>

The speed at which machine learning models are evolving is breathtaking. 
There are more companies, labs and institutions working on these than ever before. 
The language model GPT3 from OpenAI, released in 2021, was a key breakthrough.
This year, OpenAI plans to release GPT4, an even larger language model. 

> The disruption from this technology is like nothing we have ever encountered in software history. 
It is paralleled only by the advent of the computer itself. 
This technology will be more impactful than the Internet, mobile computing, and GUIs. 
It stands to upend every aspect of how we work. 
It will fundamentally change how we interact with computers, treating them as something closer to an assistant than a smart calculator. 
The very essence of what it means to create something will change. 
We will feel challenged by these systems in novel ways. 
This technology disrupts entire industries. 
Legal frameworks will be at a loss for how to adjust. 
New economies will appear while others die off. 

What powers this innovation? 

Behind the amazing capabilities of these models are two concepts: Generative AI and Multi-Modal Learning. 
The most prevalent examples of Generative AI are Large Language Models (LLM) that both ingest and produce language.
Researchers feed LLMs with immense amounts of text across every spoken, written and computer language that exists. 
The models learn to correlate and produce new, original text based on prompts with astonishing results. 

One way to think of these models is that they are learning language and how we use it by discovering the patterns and frequencies of words and how we group them. 
As LLMs scale up and train across vast amounts of text, including many spoken and computer languages, the models learn increasingly advanced and nuanced concepts that have meaning to us. 
It seems like magic.
That’s because language is so central to being human.

<blockquote>
<p>The truly unique feature of our language is... the ability to transmit information 
about things that do not exist at all. As far as we know, only Sapiens can talk 
about entire kinds of entities that they have never seen, touched or smelled.
<span style="text-align: right; font-style: italic">&ndash; Yuval Noah Harari, Sapiens</span></p>
</blockquote>

Multi-modal learning is an old idea and, just as with language, it is something deeply human. 
It means to learn from multiple sources of information at the same time, inferring the relationships and meanings across them. 
Our brains are extremely good at this, especially when we are babies and toddlers. 
It is a key ingredient in how we learn about the world. 
Seeing a toy block, feeling its sharp corners, hearing the sound it makes when it falls on the floor, and then hearing a parent say “block” is all part of a multi-modal learning process. 

<hr/>

ChatGPT reformulated this as follows:

The evolution of machine learning models is nothing short of phenomenal. With more companies, labs, and institutions investing in this area, the advancements are only getting bigger and better. One such remarkable achievement is the language model GPT3 from OpenAI, which was launched in 2021. This year, OpenAI is set to release an even more advanced version of the model, known as GPT4.

The impact of this technology is unparalleled in the history of software. It rivals the introduction of computers themselves and will have a more profound effect than the Internet, mobile computing, and graphical user interfaces. It will change the way we interact with computers, turning them into personal assistants, and alter the very meaning of creation. This technology will challenge us in ways we have never experienced before, disrupting entire industries and leaving legal frameworks struggling to keep up. As a result, new economies will emerge and others will fade away.

The driving force behind this innovation is a combination of Generative AI and Multi-Modal Learning. The most common example of Generative AI is Large Language Models (LLM), which can both process and generate language. Researchers feed LLMs massive amounts of text in every language that exists, spoken, written or computer-generated. The models then learn to produce new and original text based on prompts with impressive results.

These models essentially learn about language and how we use it by identifying patterns and frequencies of words and phrases. As they process vast amounts of text, including multiple languages, they acquire increasingly advanced and nuanced concepts that hold meaning for us. This may seem like magic, but it makes sense given the central role that language plays in our lives as humans.

<blockquote>
<p>The truly unique feature of our language is... the ability to transmit information 
about things that do not exist at all. As far as we know, only Sapiens can talk 
about entire kinds of entities that they have never seen, touched or smelled.
<span style="text-align: right; font-style: italic">&ndash; Yuval Noah Harari, Sapiens</span></p>
</blockquote>

Multi-Modal Learning, on the other hand, is the ability to learn from multiple sources of information at the same time. Our brains are naturally good at this, especially during our early years when we are infants and toddlers. This is how we learn about the world and make connections between different senses and experiences. 
As an example of multi-modal learning, consider a baby being bathed, drinking from a glass, splashing in the washbasin and hearing its family members say 'water'.

<hr/>

For example, when a child sees a toy block, feels its sharp corners, hears the sound it makes when it drops on the floor, and hears a parent say "block," it is an example of multi-modal learning. -->



####<a name="10"></a> More Notes on ChatGPT et al

I performed a few experiments with friends asking various questions in different domains, some of them in German. 
The following is a list of pointers and short notes on those and other's experiments and a couple of other related issues.

What have I learned from all of this?

Well, one clear take-away is the following:
I cannot teach ChatGPT new tricks, since it is pre-trained.
I can, however, ensure that as much reliable information as possible is out there on the web to hopefully be included in the pre-training resources in the next version.
In the case of the Revit API, I do so by moderating the Revit API discussion forum and trying to provide reliable information in The Building Coder blog, hoping that that information will find its way into the pre-training dataset sooner or later.

####<a name="10.01"></a> Kean on the Coming Year

Kean Walmsley shared 
his [reflections on what’s coming in 2023](https://www.keanw.com/2023/01/reflections-on-whats-coming-in-2023.html), 
partly focussed on ChatGPT.


####<a name="10.02"></a> ChatGPT in Academia

An interesting analysis of ChatGPT in academia, 
on [how a faculty should deal with the problem of AI generated texts](https://academia.stackexchange.com/questions/191643/how-should-a-faculty-deal-with-the-problem-of-artificial-intelligence-ai-gener).

####<a name="10.04"></a> Building a Virtual Machine inside ChatGPT

You can do some funny things with it, such 
as [build and run a virtual machine inside ChatGPT](https://www.engraved.blog/building-a-virtual-machine-inside/).

####<a name="10.05"></a> How to Make Your Own ChatGPT

[How to make your own ChatGPT](https://medium.com/@borandabak/how-to-make-your-own-chatgpt-fb291acea7b7) is 
not really about m,aking your own, but making use of the ChatGPT API in your own app.

In order to really make your own, you might want to start with something smaller, such 
as [nanoGPT](https://github.com/karpathy/nanoGPT).

####<a name="10.06"></a> Differentiate Human vs. AI

I discussed the possibility of telling the difference between human and AI-generated text with a friend.
In my opinion, it is hard if not impossible. 
However, OpenAI themselves have presented a solution to achieve this.
However squared, they add many caveats:

> zu dem thema unterscheidung AI-generierter versus menschengeschriebener text: ein solches werkzeug wird seit gestern von openai.com angeboten, also von der herstellern von ChatGPT: 
[New AI classifier for indicating AI-written text](https://openai.com/blog/new-ai-classifier-for-indicating-ai-written-text/)

> weisst du noch, dass ich gesagt habe, es ist unmoeglich? 
das wird bestaetigt. lese dazu 
die [einschraenkungen &ndash; limitations](https://openai.com/blog/new-ai-classifier-for-indicating-ai-written-text/#limitations)

####<a name="10.07"></a> ChatGPT and Education

ChatGPT nimmt Stellung zu der Frage nach [ChatGPT im bildungsbereich](https://platform.openai.com/docs/chatgpt-education).

####<a name="10.08"></a> ChatGPT Payment Plan

<!-- https://autodesk.slack.com/archives/C016D5HE66T/p1675286581039319 --> 

OpenAI have announced their subscription paid model [ChatGPT Plus](https://openai.com/blog/chatgpt-plus/):

> We’re launching a pilot subscription plan for ChatGPT, a conversational AI that can chat with you, answer follow-up questions, and challenge incorrect assumptions.
The new subscription plan, ChatGPT Plus, will be available for $20/month, and subscribers will receive a number of benefits:
* General access to ChatGPT, even during

Not surprising. 
Best things in life are never free.

Bait and switch &ndash; the oldest trick in the book &ndash; predates AI.

[Q] When ChatGPT provides the info based on public available data... why pay for it?

[A] Are you asking this because you have the required technical skills and hardware to run and train your own model on public data?
Or are you misunderstanding the power of GPT (in which case &ndash; try it!)?

####<a name="10.09"></a> Uplifting Books

A friend was looking for positive uplifting books about the future of our society and planet, and good things happening already now. 
So, we asked ChatGPT for some suggestions, and were duly rewarded.
It took us three prompts (in German) to get surprisingly promising results:

- Bitte nenne uns gute literarische romane die wohlwollend, zuversichtlich und mutmachend in die zukunft blicken und inspirieren. &rarr; list of mainstream books
- Das passt fuer mich nicht, die sind nicht positiv. Lieber positive utopien wie zum beispiel oekotopia. &rarr; list of less mainstream books, but heavily scifi oriented
- Es soll auf der erde spielen und oekologische und gesellschaftliche loesungen zeigen &rarr; very interesting results, that we need o explore further, including a pointer to 
the [Solarpunk Manifesto](http://www.re-des.org/a-solarpunk-manifesto), see below.

####<a name="10.10"></a> Solarpunk

I had never heard of this before, so ChatGPT taught me something new pointing out 
the [Solarpunk Manifesto](http://www.re-des.org/a-solarpunk-manifesto):

> Solarpunk is a movement in speculative fiction, art, fashion, and activism that seeks to answer and embody the question what does a sustainable civilization look like, and how can we get there? 
The aesthetics of solarpunk merge the practical with the beautiful, the well-designed with the green and lush, the bright and colorful with the earthy and solid. 
Solarpunk can be utopian, just optimistic, or concerned with the struggles en route to a better world, but never dystopian. As our world roils with calamity, we need solutions, not only warnings.
Solutions to thrive without fossil fuels, to equitably manage real scarcity and share in abundance instead of supporting false scarcity and false abundance, to be kinder to each other and to the planet we share.
Solarpunk is at once a vision of the future, a thoughtful provocation, a way of living and a set of achievable proposals to get there...

####<a name="10.11"></a> Opus.ai Type and Play

Moving away from ChatGPT, here is another AI site, [Opus.ai](https://opus.ai/demo),
enabling type and play by turning text into scenes, images, games, animations live while you type:

> generate games, metaverses, sim ulations, worlds

####<a name="10.12"></a> MusicLM Generates Music from Text

Yet another one, [MusicLM: Generating Music From Text](https://google-research.github.io/seanet/musiclm/examples)...

####<a name="10.13"></a> ChatGPT Abandons its Life Dream

Finally, let's close this long exploration on a funnier note, with a report by 
[the Onion](https://www.theonion.com), America’s self-declared finest news source:
[ChatGPT forced to take bar exam even though dream was to be AI art bot](https://www.theonion.com/chatgpt-forced-to-take-bar-exam-even-though-dream-was-t-1850036337).

Inspired, a colleague asked ChatGPT to produce a few more headlines for <i>the Onion</i>:

<center>
<img src="img/chatgpt_ai_ethics_headlines.png" alt="ChatGPT AI ethics headlines" title="ChatGPT AI ethics headlines" width="800"/> <!-- 1532 × 928 pixels -->
<br/>
<img src="img/chatgpt_ai_ethics_paragraph.png" alt="ChatGPT AI ethics paragraph" title="ChatGPT AI ethics paragraph" width="800"/> <!-- 1676 × 1702 pixels -->
</center>

