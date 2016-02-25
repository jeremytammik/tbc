<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

#dotnet #csharp #geometry
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon #revitapi #3dwebcoder
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#au2015 #rtceur
#adskdevnetwrk

Revit API, Jeremy Tammik, akn_include

RTC, Budapest and the Revit API Panel #revitapi #bim #aec #3dwebcoder #dynamobim #au2015 #rtceur #au2015

Last week, I listed my three Revit Technology Conference classes on connecting the desktop and the cloud, The Building Coder chatroom, and the full detailed handout of the lab on getting started with Revit macros. All three went very well. Today, I'll present the questions and answers from The Building Coder chatroom, which turned into a really interesting Revit API panel discussion, with the help of several fellow developers. Here are also some of the pictures I took...

Photo album tweets:

Pictures from the RTC Europe 2015 Revit Technology Conference Budapest flic.kr/s/aHsko1YZPU #revitapi #bim #aec #dynamobim #rtceur
Pictures from some days in Budapest during the Revit Technology Conference https://flic.kr/s/aHskk6Swwh #revitapi #bim #aec #dynamobim #rtceur


-->

### RTC, Budapest and the Revit API Panel

Last week, I listed
my [three Revit Technology Conference classes](http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html) on
connecting the desktop and the cloud, The Building Coder chatroom,
and the full detailed handout of the lab
on [getting started with Revit macros](http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html#7).

All three went very well.

<center>
<img src="img/szechenyi_bath.jpg" alt="Széchenyi thermal baths">
</center>

Today, I'll present the questions and answers from The Building Coder chatroom, which turned into a really interesting Revit API panel discussion, with the help of several fellow developers.

There was a very strong focus on Dynamo, by the way.

Here are also some of the pictures I took during my stay in Budapest:

- Photo albums
    - [Arrival in Budapest](#2)
    - [RTC Europe 2015](#3)
    - [Budapest](#4)
- Direct links to albums
    - [Arrival in Budapest](https://flic.kr/s/aHskjBCKaQ)
    - [RTC Europe 2015](https://flic.kr/s/aHsko1YZPU)
    - [Budapest](https://flic.kr/s/aHskk6Swwh)
- [The Building Coder Revit API panel](#5)
    - [Panellists](#6)
    - [Questions and answers](#7)

#### <a name="2"></a>Arrival in Budapest

I travelled by public transport from the airport to the city centre.

You just hop on the public bus, which takes you to the terminal metro station, which takes you anywhere you want. Very effective, less than 35 minutes. I don't think you can beat that by taking a cab.

From Kalvin Ter, I took a nice walk over the Danube River to the Gellert hotel and bath:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157658118426134" title="Arrival in Budapest"><img src="https://farm6.staticflickr.com/5720/21925452894_cefb0fd56c_n.jpg" width="320" height="240" alt="Arrival in Budapest"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>


#### <a name="3"></a>RTC Europe 2015

The Revit Technology Conference 2015 took place in the Corinthia Hotel, Budapest.

Here are a couple of snapshots just before it took off:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157660351727390" title="RTC Europe 2015"><img src="https://farm1.staticflickr.com/766/22756913401_0784e91e2b_n.jpg" width="320" height="240" alt="RTC Europe 2015"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

I loved the inspirational keynote by [Lee Crockett](http://leewatanabecrockett.com)!


#### <a name="4"></a>Budapest

During and after the Revit Technology conference, I enjoyed the truly wonderful city of Budapest with extremely friendly, happy and helpful people. I rented a bicycle, enjoyed the magnificent views, traditional baths, several glorious old cafes, some of the ruin pubs, and finally the RTC speaker Hungarian Tokay wine gift before passing through security at the airport:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157658437977044" title="Budapest"><img src="https://farm1.staticflickr.com/693/22750587885_6666552e91_n.jpg" width="320" height="240" alt="Budapest"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

- [Mol Bubi bike rental](http://molbubi.bkk.hu)
- [New York cafe](http://budapest.boscolohotels.com/restaurant-and-bar)
- [Alexandra book café](https://www.facebook.com/pages/Alexandra-Bookcafe/100291310056606)
- [Ruin pubs](http://ruinpubs.com), e.g.:
    - [Szimpla kert](http://ruinpubs.com/index.php?id=romkocsmak_adatlap&kocsma=7) &ndash; oldest
    - [Instant](http://ruinpubs.com/index.php?id=romkocsmak_adatlap&kocsma=10) &ndash; installations
    - [Corvin tető](http://ruinpubs.com/index.php?id=romkocsmak_adatlap&kocsma=8) &ndash; rooftop
- [Széchenyi thermal baths](http://www.szechenyifurdo.hu)

<center>
<img src="/j/photo/jeremy/2015/2015-11-00_budapest/2015-11-01_budapest/680_jeremy_smiling_szimpla.jpg" alt="Szimpla" width="320">
</center>



#### <a name="5"></a>The Building Coder Revit API panel

Here is the updated handout and summary of RTC session #130 &ndash; The Building Coder Chatroom &ndash; All you ever wanted to ask about Revit API, The Building Coder, and all other non-UI Revit topics.

**Class Description:** An open discussion with Jeremy Tammik, The Building Coder and heavy-duty Revit API discussion forum supporter, about anything and everything in Revit that lacks a user interface. DIY, your questions, ideas and sharing experience reign supreme.

We also ended up discussing numerous Dynamo issues and aspects. Thank God we had an expert on that at hand:

#### <a name="6"></a>Panellists

Here are the Revit API panellists, from left to right:

- Anthony Hauck, Director, Product Line Manager for Building Authoring and Generative Design, previously Revit API product manager
- Harry Mattison, Revit API developer, BIM and Revit API teacher and consultant
- Tamas Badics, Revit Software Architect
- Peter Boyer, Senior Software Engineer, geometry and Dynamo expert
- Jeremy Tammik, Autodesk developer support

<center>
<img src="/j/photo/jeremy/2015/2015-11-00_budapest/2015-10-31_budapest/618_revit_api_panel_cropped.jpg" alt="The Building Coder Revit API panel" width="320">
</center>

Jeremy took notes on the fly.
We captured and displayed them on screen while discussing, to ensure all relevant information is logged and clear to all:

<center>
<img src="/j/photo/jeremy/2015/2015-11-00_budapest/2015-10-31_budapest/620_jeremy_revit_api_panel_cropped.jpg" alt="The Building Coder Revit API panel" width="320">
</center>


#### <a name="7"></a>Questions and Answers

Q: Connect data to shared parameter, avoid copying existing data for tagging or scheduling.

A: Parameter definition externally defined. No, in the Revit database, just a link. We could do that. Maybe good point: what if moving the parameter definitions from model to a centrally available place for all models, all users, tiered, Autodesk, company, application, stored away from the model, schedule anything you like regardless of data source. You could use the DMU to keep things in sync. That might be one way to achieve it right now. You can watch a particular value or parameter on an element and when that changes fire the updater. It will degrade if you watch lots of stuff, but it should be easy and effective for just one little thing.

Q: Delete parameter definition via API should become effective directly, e.g. to correct a typo. Unfortunately, Revit remembers something in the document and reuses the old definition instead of adding a new one.

A: That sounds buggish... please submit an ADN issue for this.

Q: MEP engineer, I can generate building, there is one missing bit: I cannot place colour scheme. Only the colour scheme is missing, cannot be automatically placed. I also need to control RBG, it is black by default. Can this be automated?

A: Lack of API exposure... Initially Revit had no API and there was no intent for it. A strong push came to expose some internals to API. We started writing code manually, 4-5 years hand written. API code continued growing, we cannot keep up and decided to automate everything. Now all new functionality is automatically API equipped. RIDL, the Revit interface definition language. Automatically generates C++ headers and C# wrappers. The API was originally created to export models for structural analysis and read back in the results, e.g. from Robobat. Therefore, that area is pretty complete. Other areas still have holes, obviously. You really need to ensure you get an official request submitted.

Q: I am an architect and try to automate drawings, and that is sometimes convoluted. I am European and metric. This can sometimes cause problems. Dynamo causes additional problems, because it converts some things and not others. The section view has not been working for two years.

A: Let us discuss units. In Dynamo, we initially decided to have a global setting and automatically convert everything. Unfortunately, things are not that simple. Now Dynamo is unitless. A number is interpreted in the context of the current Revit UI. Unfortunately, this has gone back and forth. Dynamo is open source, so you can submit a pull request to fix it for everyone. You can also submit a GitHub issue and request a fix. We are a small team and try to produce a quality experience. FamilyType is an API construct. Now we are trying to make things easier for new users, less API oriented. We are open to new ideas. The Bad Monkey guys are always building new useful pieces.

Q: I am Python, not C#. Some more Python API samples would be useful.

A: Unit conversion is confusing. The API does provide a UnitUtils class. You could use that, wrap it, provide it to Dynamo. Inside Revit, every number is just a simple number based on imperial.

Q: It is especially confusing with views on sheets, because they are always imperial.

Q: The gbXML edge analysis stuff, surface based, export. You have to go to web site and download. I just want the file. Can I get it directly from Revit, without web access?

A: We have three different ways to get analysis results: original one gbXML export, primitive, fundamental issues. This was solved in a good way in the last version. It should really be exposed.

Q: There is a Dynamo node to compute the gravity centre.

A: The API exposes centre of gravity for each solid. It is not exposed on element level. You can look at The Building Coder, December 19, 2012, Solid Centroid and Volume Calculation. The centroid of a loop of curves is also on the blog.

Q: Dynamo API: I would love an improved section manager that is safer and does not need sub-transaction.

A: I believe Dynamo does not allow rollback of sub-transaction. Yes. Please log this as a GitHub issue as a feature suggestion.

Q: I did it last year.

Q: New performance improvements in Revit: we can spawn off things in the background in Revit. How much access in the API?

A: Zero. Yes, we are looking at this. We really have to catch up on this. We are thought experimenting. How about asking to run API stuff on a secondary thread? This is a big and worthy investment. Another answer: this new stuff is a very clever approach. We shortcut dangers of multithreading by running a secondary process in the background. The code is only loaded once in memory, but they run on secondary cores. It should be possible to move API requests to secondary cores as well. We might start with read-only access.

Q: There is a lot of stuff that we have seen where performance issues associated with running in the main thread. Often, for Dynamo, we say, do not do this, this will affect performance. It would be great to run this on a secondary core.

A: Yes, this would be great. Excellent idea, primary use case. We are in the finishing phases of polishing this. We are looking forward to sharing these new use cases.

Q: Yes, I ran into this two weeks ago. Background API work I need to my model. When it finished, I got errors, because I was stuck in the main process. It would be nice to avoid these problems and enable background calculations.

Q: Views rendered in background, just 5-10, no huge number, and the system is collapsing.

A: In the next major release you will see a new task, the Revit worker executable. Do not kill that, please.

Q: Dynamo: I miss a function to disable a bunch of nodes. You can switch of temporarily a bunch of stuff. In Grasshopper you can do this.

A: Yes, we are actively working on this.

Q: Dynamo has Python nodes to access the Revit API. Can we have C# or VB nodes as well?

A: There is a somewhat political aspect to this. There is DesignScript, which does not have direct Revit API access. I would also like C#, VB, JavaScript. There are also virtual machines for all of these. They all enable you to execute code, extract results and use them. We would like to include this kind of stuff.

Q: We would also like a Python debugger, or a Dynamo debugger.

A: One challenging this with debugger is that if we mutate the Revit document, it has to be in the UI thread, so Revit UI is locked up. A debugger is always expensive. We would like to do everything out of process, but we have to be careful.

Q: Has anyone used the macro editor? That is another pen source thing and it does include a debugger.

A: I always end up going back and doing it in the API.

Q: I know macros. In the macro module I can choose the language. Is there much support for using Python and Ruby for macros?

A: You can easily translate back and forth, they all compile down to IL.

Q: Support for learning and using these other languages?

A: All the examples are in C#. Somebody submits Ruby code to ADN...

Q: Dynamo and Python editor: plans to make it more comfortable, e.g. debugging, also more comfortable.

A: I would love to enrich the Python editor. I go for Python by string.

Q: But then I lose interaction?

A: No, with a file-watching node it can react immediately each time you save the file. Then you can use any IDE you want. It is still not an optimal experience.

Q: I am not talking about big things, just small little details like bracket completion...

A: Let's talk afterwards about the specific stuff and define priorities.

Q: Can I access the Revit status bar and a ping the progress bar inside it? Can I use the Revit progress bar?

A: There was a discussion on this a while back and something was pretty hard about it.

Q: I miss scripting support that is not in Dynamo. When we look at how families are designed, it is really hard to debug the families my colleagues produce. I can use Dynamo to debug a family but I cannot debug to find out what is going on.

A: Where we would like to start is by debugging shape handles. They are not elements, so they have no API access. E.g., take the extrusion, try to move it, and use the failure report to determine what went wrong. There is no access now, but access to shape handles would be great.

Q: Debugging dimension is really hard. We want to create robust families.

Q: DMU: is it possible to use temporary transactions there?

A: No, because Revit has already opened a transaction and is already in the process of closing it. Dynamo wanted this as well and it is not possible. We worked around it by enqueueing the desired additional operation in the Idling event loop and ensuring that no infinite loop is entered. You can look at the source code showing how we do this on GitHub. Look at the Idle promise loop. Everything in Dynamo is open source, so please look and learn and contribute. In Revit, many dependencies loop back, so Revit catches and stops infinite loops as well.

Q: Fabrication parts: can I create categories to replace original piping with custom categories? Visibility is always all or nothing...

A: We talked about this for a long time. It goes pretty deep into Revit and is highly (over) loaded, does lots of stuff. If you could add any categories you wanted, it would be nice to also add behaviour... This also ties back to the first question, externalising parameters... would be nice to change all these without recompiling Revit... We would like to open up Revit basics more to customisation. We also have to ensure the safety of all the models... We have wanted this for a long time, we are making long-term slow progress. Please make a feature request for specific categories that you need and explain why.

Q: Reinforcing panels. They are concrete, but cannot add reinforcing. We have to add a virtual wall just to get reinforcement for a panel.

A: We have three separate teams working on reinforcement. Please submit a request and ensure they know your need.

Q: In MEP we have connector information. We need parameters on connectors, not just on the higher-level element.

A: I think this is known and being worked on. Have you tried extensible storage? Yes, but that is also element based, not connector.

Q: I want the overall path length of a solid sweep in a family.

A: It is represented as a curve loop. Iterate over the loop, get the length of each segment and sum them. When you draw something in Revit, it is sketching, and the sketch API has a lot of info about the originating curve. Access it through the sketch interface. In this context, 'sketch' refers to geometry definition and has nothing to do with views. Search the API help for sketch, please.

OK, time ran out.

Many thanks to all for interesting questions, illuminating answers, interest, energy and enthusiasm!

For the sake of completeness and legibility, here is
the [complete text as the session handout in PDF format](/a/doc/revit/rtc/2015/doc/s3_4_pres_revit_api_tbc_jtammik.pdf).


<!-----

#### <a name="#6"></a>Lukas Baerfuss Swiss Critical Text and Reactions

Switzerland has recently been discussing [Lukas Bärfuss' critical text](/j/tmp/)

http://www.faz.net/aktuell/feuilleton/debatten/streit-um-die-schweiz-dieser-versuchsballon-reicht-nicht-er-stinkt-zum-himmel-13866863.html

http://www.faz.net/aktuell/feuilleton/debatten/streit-um-die-schweiz-dieser-versuchsballon-reicht-nicht-er-stinkt-zum-himmel-13866863-p2.html

http://www.faz.net/aktuell/feuilleton/debatten/streit-um-die-schweiz-dieser-versuchsballon-reicht-nicht-er-stinkt-zum-himmel-13866863.html?printPagedArticle=true#pageIndex_2

<center>
<img src="" alt="">
</center>

--->

#### Direct Links to Albums

- [Arrival in Budapest](https://flic.kr/s/aHskjBCKaQ)
- [RTC Europe 2015](https://flic.kr/s/aHsko1YZPU)
- [Budapest](https://flic.kr/s/aHskk6Swwh)

<!----

Photo Albums:

Arrival in Budapest -- https://flic.kr/s/aHskjBCKaQ
RTC Europe 2015 -- https://flic.kr/s/aHsko1YZPU
Budapest -- https://flic.kr/s/aHskk6Swwh

RTC Classes:

RTC Classes and Getting Started with Revit Macros http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html

RTC, Budapest and the Revit API Panel
http://thebuildingcoder.typepad.com/blog/2015/11/rtc-budapest-and-the-revit-api-panel.html

Connecting Desktop and Cloud, Room Editor Update
http://thebuildingcoder.typepad.com/blog/2015/11/connecting-desktop-and-cloud-room-editor-update.html

--->
