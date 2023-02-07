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

I strongly advise anyone interested in it to try it out for yourself rather than just reading the numerous articles published by others.




####<a name="2"></a> Back to the Basics

Here are four typical questions from StackOverflow and the discussion forum where I repeat some basic recommendations that I have already given many times before:

- [Element filtering](#3)
- [parameter access](#4)
- [XYZ trigonometry](#5)
- [projecting points](#6)


I would really be happy if I did not have to continue repeating them.

I am sure that by now, based on the publicly available information, ChatGPT can answer them just as well or better than I have done.

I hope that you guys all agree with me that the recommendations I make there are sensible and relevant.

Please let me know if you disagree or can improve on them in any way.

Thank you!

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


####<a name="3"></a> XYZ Trigonometry

Another recurring question is basic trigonometry, such 
as [how to create a vector `XYZ` tilted up from the view direction by a specified angle](https://forums.autodesk.com/t5/revit-api-forum/how-to-create-a-vector-xyz-tilted-up-from-the-view-direction-by/m-p/11621339):

**Question:** I have a the view direction that I use for my reference intersector. 
I would like to try various altitude angles up from the view direction in a section with starting point at a wall like in this section view:


<center>
<img src="img/vector_tilted_up.png" alt="Vector tilted up from plane" title="Vector tilted up from plane" width="600"/> <!-- 1199 × 530 pixels -->
</center>

I know there are transforms and various complex maths answers on StackOverflow; I was hoping to use a simpler built-in method if available? 

**Answer:** This kind of trigonometry is not difficult. Your children learn it in school, I hope. Please take a moment to either read the Wikipedia article or study some other tutorials:

  

https://en.wikipedia.org/wiki/Trigonometric_functions#Law_of_tangents

  

From that article, I find this image most helpful:

  

Unit Circle Definitions of Six Trigonometric Functions

  

https://en.wikipedia.org/wiki/File:Unit_Circle_Definitions_of_Six_Trigonometric_Functions.png

  

wiki_trigo.png


<center>
<img src="img/trigonometry_functions.png" alt="The six trigonometric functions" title="The six trigonometric functions" width="320"/> <!-- 640 × 836 pixels -->
</center>


Decide what angle you wish to use, e.g., 30 degrees. Determine its tangens value, ca. 30*3.14/180 = 0.5. Take your horizontal view direction XYZ vector (x,y,0). Replace the Z coordinate by the tangens you calculated, yielding  (x,y,0.5). Voila. That is your new tilted direction vector. You may normalise it if you like.

   

Please do not be afraid of trigonometry, it is very intuitive as soon as you stop being scared of it.

    

I condemn our teachers and education systems (not all, but all too many) for inoculating kids with fear of maths and geometry.

  

This is basic human intuitive understanding,. The greeks mastered it 3000 yeards ago. We can handle a computer and a smartphone, but not simple trigonometry? 

  

Why?

   

Jeremy Tammik,  Developer Advocacy and Support, The Building Coder, Autodesk Developer Network, ADN Open
Tags (0)
Add tags
Report
MESSAGE 5 OF 6
RPTHOMAS108
 Mentor RPTHOMAS108 in reply to: FrankHolidaytoiling
‎2022-12-15 04:22 AM 
 

 

Tags (0)
Add tags
Report
MESSAGE 6 OF 6
RPTHOMAS108
 Mentor RPTHOMAS108 in reply to: RPTHOMAS108
‎2022-12-15 04:25 AM 
I would probably look at it in terms of the ratio between the forward direction and XYZ.BasisZ that the angle represents.

 

i.e. for 45 degrees it would be the average of the two (forward and up). When you consider each component of each vector they range from the value you have looking forward to the value you have looking up. If you divide these delta values by 90 do you then have a fraction for each component you can multiple by your angle (in degrees) to add to the forward direction? 

 

I've never done it that way to be fair, I would probably have also solved it with trigonometry but the XYZ has good functionality for arithmetic operations.

 

Similar as noted above in Jeremy's post below is probably the easiest way:

 

Dim V0 As New XYZ(1, 0.5, 0) 'some random flat direction
V0 = V0.Normalize
Dim Ang As Double = 30 'Angle in degrees
Dim Ang_r As Double = (Math.PI / 180) * Ang
Dim T As Double = Math.Tan(Ang_r) 'Tan(Ang) = Opp/1
Dim Vz As New XYZ(0, 0, T)
Dim V1 As XYZ = (V0 + Vz).Normalize 'The direction with tilt of 30 degrees up from horizontal
 

I think I was oversimplifying earlier because it isn't linear. Always on the lookout for new ways of doing the same things. Above is ok from 0 to < 90.  From that point on you have to check the quadrant and consider the Tan function doesn't work approaching 90 or 270 but you know those values are looking straight up and straight down respectively.


<center>
<img src="img/trigonometry_functions.png" alt="The six trigonometric functions" title="The six trigonometric functions" width="320"/> <!-- 640 × 836 pixels -->
</center>

####<a name="3"></a> Projecting Points

[Projecting points](https://forums.autodesk.com/t5/revit-api-forum/using-avf-on-ductwork-coordinates-issue/m-p/11621128)

####<a name="3"></a> Generative AI and Multi-Modal Learning

The past few months hint at exciting and adventurous times full of radical change ahead.

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



####<a name="3"></a> Kean on the Coming Year

Kean on Reflections on what’s coming in 2023, partly focussed on ChatGPT and
https://www.keanw.com/2023/01/reflections-on-whats-coming-in-2023.html

####<a name="7"></a> ChatGPT in Academia

ChatGPT in academia:
https://academia.stackexchange.com/questions/191643/how-should-a-faculty-deal-with-the-problem-of-artificial-intelligence-ai-gener?utm_source=Iterable

####<a name="8"></a> Using ChatGPT with Revit API

Mohamed Elimam, SIAC Construction, using ChatGPT with Revit API
https://www.linkedin.com/posts/mohamed-elimam_programming-chatgbt-ai-activity-7010260045649063936-dQds?utm_source=share&utm_medium=member_desktop

####<a name="9"></a> ChatGPT with Rene
zip/2022-12-30_chatgpt_rene.txt

####<a name="9"></a> Building a Virtual Machine inside ChatGPT

Building A Virtual Machine inside ChatGPT
https://www.engraved.blog/building-a-virtual-machine-inside/

####<a name="9"></a> How to Make Your Own ChatGPT

How to make your own ChatGPT
https://medium.com/@borandabak/how-to-make-your-own-chatgpt-fb291acea7b7

[nanoGPT](https://github.com/karpathy/nanoGPT)

####<a name="9"></a> Train ChatGPT for Revit API

jeremy: Do you know about the Revit API?
Yes, I am familiar with the Revit API. The Revit API is a software application programming interface (API) that allows users to access and manipulate the data contained in Autodesk Revit projects. This API can be used to develop custom tools and applications that integrate with Revit, allowing users to automate tasks and extend the functionality of the software.
jeremy: So, what is the difference between Revit API and Dynamo?
Dynamo is a visual programming platform that can be used with Revit to automate and simplify tasks and workflows. The Revit API, on the other hand, is a set of programming tools and libraries that can be used to develop custom applications and tools that integrate with Revit. While Dynamo is a user-friendly platform that allows users to create scripts and automate tasks without writing code, the Revit API is a more advanced tool that requires some programming knowledge to use effectively. Additionally, Dynamo is built on top of the Revit API, so using the Revit API allows for even greater customization and control over the automation of tasks in Revit.
jeremy: Do you know the Revit A:PI discussion forum?
I am not aware of a specific discussion forum for the Revit API. However, there are many online forums and communities where users of the Revit API can ask questions, share tips and advice, and discuss their experiences with the API. Some examples of these forums include the Autodesk Developer Network forums, the Autodesk Revit API discussion group on LinkedIn, and the Revit API discussion forum on Autodesk Community. These forums can be a great resource for users of the Revit API who are looking for help, advice, or simply want to connect with other users of the API.
jeremy: Do you know The Building Coder  blog?
I'm sorry, but I am not aware of The Building Coder blog. As a large language model trained by OpenAI, my knowledge is based on the text that has been fed into my training data. I do not have the ability to browse the internet or access additional information outside of what has been provided to me during my training. I apologize if I am unable to provide you with the information you are looking for. Is there anything else I may be able to assist you with?
jeremy: Can you read The Building Coder blog at https://thebuildingcoder.typepad.com/ so that you later have access to that information?
As a language model trained by OpenAI, I do not have the ability to browse the internet or access information outside of what has been provided to me during my training. I am only able to provide information and respond to questions based on the text that has been fed into my training data. I apologize if I am unable to provide you with the information you are looking for. Is there anything else I may be able to assist you with?
jeremy: Can I provide you with additional training text to expand your knowledge in a specific domain?
As a pre-trained language model, I am not able to accept additional training text or expand my knowledge in a specific domain. My knowledge is based on the text that was used to train me, and I do not have the ability to learn or acquire new information outside of what was included in my training data. I am designed to assist with a wide range of questions and topics, but my answers are limited to the information and knowledge that was provided to me during my training. If you have any specific questions, I would be happy to try to provide an answer based on my existing knowledge.
jeremy: Can I access the your source code in order to expand your training myself?
I'm sorry, but I am not a physical machine or computer program that can be accessed or modified. As a large language model trained by OpenAI, I exist solely as a virtual assistant that is able to provide answers to questions and respond to statements based on the text that was used to train me. I do not have a source code or any other physical representation that can be accessed or modified. I apologize if I am unable to meet your request. Is there anything else I may be able to assist you with?

####<a name="6"></a> Differentiate Human- and AI-Generated

zu dem thema unterscheidung AI-generierter versus menschengeschriebener text: ein solches werkzeug wird seit gestern von openai.com angeboten, also von der herstellern von ChatGPT: https://openai.com/blog/new-ai-classifier-for-indicating-ai-written-text/

weiss du noch, dass ich gesagt habe, es ist unmoeglich? das wird bestaetigt. lese dazu die einschraenkungen: https://openai.com/blog/new-ai-classifier-for-indicating-ai-written-text/#limitations

####<a name="6"></a> ChatGPT and Education

sie nehmen auch extra stellung zu der frage nach ChatGPT im bildungsbereich: https://platform.openai.com/docs/chatgpt-education

####<a name="6"></a> ChatGPT Payment plan

https://autodesk.slack.com/archives/C016D5HE66T/p1675286581039319

OpenAI have announced their subscription paid model [ChatGPT Plus](https://openai.com/blog/chatgpt-plus/):

> We’re launching a pilot subscription plan for ChatGPT, a conversational AI that can chat with you, answer follow-up questions, and challenge incorrect assumptions.
The new subscription plan, ChatGPT Plus, will be available for $20/month, and subscribers will receive a number of benefits:
* General access to ChatGPT, even during

not surprising. best things in life are never free :wink: (edited) 

I don’t think this is ready for employee use with private company data (e.g. source code) yet, as it doesn’t call out “enterprise” or “privacy” directly (plus we would likely need approval from legal). But it’s a step in the right direction! (edited) 

bait and switch :slightly_smiling_face: the oldest trick in the book... predates AI :stuck_out_tongue:

When ChatGPT provides the info based on public available data..why pay for it.? :thinking_face:

When ChatGPT provides the info based on public available data..why pay for it.? :thinking_face:
Are you asking this because you have the required technical skills and hardware to run and train your own model on public data?
Or are you misunderstanding the power of gpt (in which case - try it!)? (edited) 

####<a name="6"></a> ChatGPT Abandons Life Dream

[The Onion](https://www.theonion.com), 
America’s self-declaimed finest news source, reports:
[ChatGPT forced to take bar exam even though dream was to be AI art bot](https://www.theonion.com/chatgpt-forced-to-take-bar-exam-even-though-dream-was-t-1850036337).

Inspired, a colleague asked ChatGPT to produce a few more headlines for <i>the Onion</i>:

<center>
<img src="img/chatgpt_ai_ethics_headlines.png" alt="ChatGPT AI ethics headlines" title="ChatGPT AI ethics headlines" width="800"/> <!-- 1532 × 928 pixels -->
<br/>
<img src="img/chatgpt_ai_ethics_paragraph.png" alt="ChatGPT AI ethics paragraph" title="ChatGPT AI ethics paragraph" width="800"/> <!-- 1676 × 1702 pixels -->
</center>

####<a name="6"></a> Uplifting Books

A friend was looking for positive uplifting books about the future of our society and planet, and good things happening already now. 
So, we asked ChatGPT for some suggestions, and were duly rewarded.
It took us three prompts (in German) to get the desired surprisingly good results:

- Bitte nenne uns gute literarische romane die wohlwollend, zuversichtlich und mutmachend in die zukunft blicken und inspirieren. &rarr; list of mainstream books
- Das passt fuer mich nicht, die sind nicht positiv. lieber positive utopien wie zum beispiel oekotopia. &rarr; list of less mainstream books, but heavily scifi oriented
- Es soll auf der erde spielen und oekologische  und gesellschaftliche loesungen zeigen &rarr; very interesting results, including a pointer to 

the [Solarpunk Manifesto](http://www.re-des.org/a-solarpunk-manifesto):

####<a name="6"></a> Solarpunk

Solarpunk is a movement in speculative fiction, art, fashion, and activism that seeks to answer and embody the question “what does a sustainable civilization look like, and how can we get there?” 

The aesthetics of solarpunk merge the practical with the beautiful, the well-designed with the green and lush, the bright and colorful with the earthy and solid. 

Solarpunk can be utopian, just optimistic, or concerned with the struggles en route to a better world ,  but never dystopian. As our world roils with calamity, we need solutions, not only warnings.

Solutions to thrive without fossil fuels, to equitably manage real scarcity and share in abundance instead of supporting false scarcity and false abundance, to be kinder to each other and to the planet we share.

Solarpunk is at once a vision of the future, a thoughtful provocation, a way of living and a set of achievable proposals to get there.

We are solarpunks because optimism has been taken away from us and we are trying to take it back.

We are solarpunks because the only other options are denial or despair.

At its core, Solarpunk is a vision of a future that embodies the best of what humanity can achieve: a post-scarcity, post-hierarchy, post-capitalistic world where humanity sees itself as part of nature and clean energy replaces fossil fuels.

The “punk” in Solarpunk is about rebellion, counterculture, post-capitalism, decolonialism and enthusiasm. It is about going in a different direction than the mainstream, which is increasingly going in a scary direction.

Solarpunk is a movement as much as it is a genre: it is not just about the stories, it is also about how we can get there.

Solarpunk embraces a diversity of tactics: there is no single right way to do solarpunk. Instead, diverse communities from around the world adopt the name and the ideas, and build little nests of self-sustaining revolution.

Solarpunk provides a valuable new perspective, a paradigm and a vocabulary through which to describe one possible future. Instead of embracing retrofuturism, solarpunk looks completely to the future. Not an alternative future, but a possible future.

Our futurism is not nihilistic like cyberpunk and it avoids steampunk’s potentially quasi-reactionary tendencies: it is about ingenuity, generativity, independence, and community.

Solarpunk emphasizes environmental sustainability and social justice.

Solarpunk is about finding ways to make life more wonderful for us right now, and also for the generations that follow us.

Our future must involve repurposing and creating new things from what we already have. Imagine “smart cities” being junked in favor of smart citizenry.

Solarpunk recognizes the historical influence politics and science fiction have had on each other.

Solarpunk recognizes science fiction as not just entertainment but as a form of activism.

Solarpunk wants to counter the scenarios of a dying earth, an insuperable gap between rich and poor, and a society controlled by corporations. Not in hundreds of years, but within reach.

Solarpunk is about youth maker culture, local solutions, local energy grids, ways of creating autonomous functioning systems. It is about loving the world.

Solarpunk culture includes all cultures, religions, abilities, sexes, genders and sexual identities.

Solarpunk is the idea of humanity achieving a social evolution that embraces not just mere tolerance, but a more expansive compassion and acceptance.

The visual aesthetics of Solarpunk are open and evolving. As it stands, it is a mash-up of the following:

1800s age-of-sail/frontier living (but with more bicycles)

Creative reuse of existing infrastructure (sometimes post-apocalyptic, sometimes present-weird)

Appropriate technology

Art Nouveau

Hayao Miyazaki

Jugaad-style innovation from the non-Western world

High-tech backends with simple, elegant outputs

Solarpunk is set in a future built according to principles of New Urbanism or New Pedestrianism and environmental sustainability.

Solarpunk envisions a built environment creatively adapted for solar gain, amongst other things, using different technologies. The objective is to promote self sufficiency and living within natural limits.

In Solarpunk we’ve pulled back just in time to stop the slow destruction of our planet. We’ve learned to use science wisely, for the betterment of our life conditions as part of our planet. We’re no longer overlords. We’re caretakers. We’re gardeners.

Solarpunk:

is diverse

has room for spirituality and science to coexist

is beautiful

can happen. Now


####<a name="6"></a> Opus.ai Type and Play

Non-ChatGPT :
Type and play
turn text into scenes, images, games, animations live while you type
generate games, metaverses, sim ulations, worlds
This company has a terrible website, but the demo video is kind of neat - text-to-3d-game https://opus.ai/
https://opus.ai/demo


####<a name="6"></a> MusicLM Generates Music from Text

MusicLM: Generating Music From Text

https://google-research.github.io/seanet/musiclm/examples/




####<a name="2"></a> 

####<a name="3"></a> 

####<a name="4"></a> 

**Question:** 

**Solution:** 


**Update:** 


<pre class="prettyprint">

</pre>



<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 716 × 403 pixels -->
</center>
