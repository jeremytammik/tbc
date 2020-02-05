<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

twitter:

Autodesk open positions, communicating from outside with Revit and an async/await external event wrapper for the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/asyncawaitexternal

<code>async</code> and <code>await</code> external event wrapper!
Communicating from outside with Revit is often better replaced by the Forge Design Automation API for Revit.
However, it is also possible to make use of Revit as a server in a limited way via an external event
&ndash; Communicating with another process
&ndash; Autodesk open positions...

linkedin:

Autodesk open positions, communicating from outside with Revit and an async/await external event wrapper for the #RevitAPI 

http://bit.ly/asyncawaitexternal

async and await external event wrapper!

Communicating from outside with Revit is often better replaced by the Forge Design Automation API for Revit.

However, it is also possible to make use of Revit as a server in a limited way via an external event:

- Communicating with another process
- Autodesk open positions...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### External Communication and Async Await Event

The question of communicating from outside with Revit is popping up with increasing frequency.

Since Revit is designed as an interactive end user tool, misusing it as a server may fail and may also violate the license agreement.

Therefore, in many cases, the cleanest (or only clean) solution will involve use of
the [Forge Design Automation API for Revit](https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview)
or [DA4R](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.55).

However, it is also possible to make use of Revit as a server in a limited way via
an [external event](https://www.revitapidocs.com/2020/05089477-4612-35b2-81a2-89c4f44370ea.htm).

Many aspects of this have been discussed numerous times in the past in the topic group
on [external events for modeless access and driving Revit from outside](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

However, people sometimes prefer not to read and research, not to fish, but rather ask repeated questions and be fed, so we return to this topic again (and again).

Furthermore, our new hero Igor shared a cool wrapper for external events that makes the process easier than ever before:

- [`async` and `await` external event wrapper](#2)
- [Communicating with another process](#3)
- [Autodesk open positions](#4)

<center>
<img src="img/communication.png" alt="Communication" title="Communication" width="400"/>
</center>

#### <a name="2"></a>Async and Await External Event Wrapper

A Revit add-in can accept requests from an external source that executes outside the valid Revit API context by implementing an external event and providing a method for the external client to raise it.

This functionality can obviously also be wrapped in an `async` and `await` structure.

Igor Serdyukov, also known as Игорь Сердюков or [WhiteSharq](https://github.com/WhiteSharq) has implemented such a wrapper, saying:

> I thought you might be interested in my small humble invention for Revit coders.
I call it `RevitTask`.
You can find details in the [RevitTask project GitHub page](https://github.com/WhiteSharq/RevitTask).
It is also available as a [nuget package](https://www.nuget.org/packages/RevitTask).
It includes two samples, a minimal test and a little more interesting example project using Revit as a server and handling requests from the browser.

#### <a name="3"></a>Communicating with Another Process

The issue of communicating with another process was also discussed back and forth a couple of times in the past few days in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit add-in communicating with other process](https://forums.autodesk.com/t5/revit-api-forum/revit-addin-communicating-with-other-process/m-p/9275981):

**Question:** I need to write a Revit add-in (the external command) which listens to messages from other process (not from Revit itself).
In other words, the Revit add-in would be the server, another process would be a client.

I tried to use async pipe to communicate with other process but the Revit add-in doesn't listen to any messages.

I think it's because of my server-side pipe in Revit is closed as soon as the `Execute` method returns and terminates.

Is there any way to keep the server pipe still alive, even after the add-in's `Execute` finishes?

I think I should create a thread in Execute to resolve this issue. Is this approach feasible?

I would really appreciate any example code.

**Answer:** Yes. Thank you for the relevant and interesting question.

The recommended approach is to implement and use an external event for this.

The external event SDK sample illustrates:

- SDK/Samples/ModelessDialog/ModelessForm_ExternalEvent/

Many related discussions and solutions are listed by The Building Coder
on [External Events for Modeless Access and Driving Revit from Outside](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

Also check out some more recent discussions on using IPC:

- [Set Floor Level and Use IPC for Disentanglement](https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html)
- [Scaling an Add-In for a 4K High Resolution Screen](https://thebuildingcoder.typepad.com/blog/2019/09/scaling-an-add-in-for-a-4k-high-resolution-screen.html)
- [Integrating a Web-Based UI](https://thebuildingcoder.typepad.com/blog/2019/09/ui-top-forms-buttons-web-etc.html#4)
- [Integrating the Helix 3D Viewer with a WPF Add-In](https://thebuildingcoder.typepad.com/blog/2019/11/integrating-the-helix-3d-viewer-with-a-wpf-add-in.html)

**Response:** I really appreciate your prompt answer.

I'm curious about the entry point.
The only way I know in order to load my customized code is by implementing an external command,
You said to forget about external command.
Does Revit have any other interfaces to import customised code?

**Answer:** Yes.

The Revit API is completely event driven.

Many (or most? or all?) of the Revit events provide a valid Revit API context.

Look at the [Autodesk.Revit.DB.Events namespace](https://www.revitapidocs.com/2020/b86712d6-83b3-e044-8016-f9881ecd3800.htm).

**Response:** I have one more question.

From the first link you gave me on using IPC for disentanglement, I found an example code *IPC_test_revit_plugin.zip*.

It includes a browser project and a Revit add-in project.
Is this add-in (external command) able to listen for messages from other applications until Revit is terminated?

**Answer:** An external command listens to one message only, and nothing else.

The only message an external command is ever interested in is the `Execute` message that it implements a handler for.

The only instance that can send that message is Revit.exe.

The only time the message is sent is when Revit.exe wishes the external command to be executed.

**Response:** I want to develop an add-in that can be loaded in Revit that listens to messages from another process (application) as long as Revit is running. Is it possible to implement?

**Answewr:** Yes, using an [external event](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

However, Revit is not designed for that purpose and you may violate the license agreement by doing so.

For that purpose,
a [DA4R application](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.55) may
be a more appropriate choice:

#### <a name="4"></a>Autodesk Open Positions

Autodesk is offering a number of exciting jobs in engineering positions in various parts of the world.

Two open positions in Europe right now are for software engineers in Cambridge, UK, with the following job ids:

- [19WD36914](https://rolp.co/Fzo7i)
- [19WD36916](https://rolp.co/IEBFh)

Here are the details on those two:

- Position Overview &ndash; 
Do you want to be part of Autodesk’s digital transformation of manufacturing and construction? Do you want to help create a platform company by building cloud services, applications and components? Are you passionate about engaging with colleagues across the globe and working in an Agile development environment? If this sounds like you, read on! In this position, you will play an important role in the definition and development of core technologies that make up Autodesk’s manufacturing and construction products. You will use your knowledge and enthusiasm on projects covering all aspects of the software development process. You will join a Scrum team and actively contribute to the team’s success by reviewing and assessing customer problems, architecting and implementing solutions and presenting the results. You will work within a small and supportive group in Cambridge, alongside more than 1000 developers across the company, in an environment that is both challenging and rewarding.
- Responsibilities
    - Prototype, implement, and maintain production cloud services and software components
    - Collaborate with teams of talented engineers to design, plan, develop, refactor, test, deliver and maintain complex features within cloud-based, web and desktop components
    - Interact with the product owner and product management teams to review and implement proposed designs
    - Being Agile and using LEAN methodology as an active member of a Scrum team
    - Collaborate using tools such as Git, JIRA, Slack and wiki pages
- Minimum Qualifications
    - Knowledge of Software Engineering Processes and Practices
    - A flexible and fast learner and keen to expand your skills in the world of Desktop and cloud technologies
    - First or upper-second class degree in Computer Science, Mathematics, Engineering or a related technical field
    - BREP modelling, mesh modelling, JavaScript, C++ 17, Node, Docker, Python, AWS, Artifactory, CMake, Jenkins, Ansible

Good luck applying for these or other opportunities that you can find in the [Autodesk career site](https://www.autodesk.com/careers)!

You can ask me for a personal referral link if you find something that you are interested in.
