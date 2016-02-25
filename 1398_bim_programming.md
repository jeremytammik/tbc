<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewAPI #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restAPI
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge
@AutodeskReCap @Adsk3dsMax
#revitAPI #bim #aec #3dwebcoder #adsk #adskdevnetwrk @jimquanci @keanw
#au2015 #rtceur
#eraofconnection

Revit API, Jeremy Tammik, akn_include

BIM Programming Madrid and Spanish Connectivity #revitAPI #bim #aec #3dwebcoder #adsk @AutodeskRevit #adskdevnetwrk

I spent this week in Madrid, presenting at
the BIM Programming conference and teaching the subsequent two-day workshop on the Revit API and connecting the desktop and the cloud
&ndash; BIM Programming mainstage presentation
&ndash; The Spanish nature of connectivity
&ndash; Castafiore
&ndash; Zazen
&ndash; Matins
&ndash; AlphaGo, machine learning and intuition...

-->

### BIM Programming Madrid and Spanish Connectivity

I spent this week in Madrid, presenting at
the [BIM Programming](http://www.bimprogramming.com) conference and teaching the subsequent two-day workshop on the Revit API and *Connecting the desktop and the cloud*.

- [BIM Programming mainstage presentation](#2)
- [The Spanish nature of connectivity](#3)
- [Castafiore](#4)
- [Zazen](#5)
- [Matins](#6)
- [AlphaGo, machine learning and intuition](#7)


<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157663375201559" title="BIM Programming Madrid"><img src="https://farm2.staticflickr.com/1566/24591730181_6902d72677_n.jpg" width="320" height="240" alt="BIM Programming Madrid"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>


#### <a name="2"></a>BIM Programming Mainstage Presentation

My mainstage presentation addressed the following topics:

- The future of making things, [IoT](https://en.wikipedia.org/wiki/Internet_of_Things), [Forge](http://forge.autodesk.com)
- WebGL and the [View and Data API](https://developer.autodesk.com/api/view-and-data-api/)
- BIM Programming
    - Connecting the desktop and the cloud
    - [2D cloud-based Revit room editor](https://github.com/jeremytammik/RoomEditorApp)
    - [FireRating in the cloud](https://github.com/jeremytammik/FireRatingCloud)



<!---

- Future of making things, IoT, Forge

15:00 Acreditaciones
15:15 CONFERENCIAS
jeremy: bim programming: revit, view and data api, ...
17:00 TALLERES
ejercicio
20:30 Beers&Chips

17:00 TALLERES
20:30 Beers&Chips



I'll share more details on

#### <a name="99"></a>IoT &ndash; Internet of Things

- [Cyrille's House](https://plougonvelin.herokuapp.com) &ndash;
- [Cyrille's webcam user and password is Socket5](http://ic8879.myfoscam.org:88) &ndash;
- [Pier 9 IoT Viewer talk with cyrille!](http://pier9.herokuapp.com/?model=reduced) &ndash;




- present fireratingcloud create shared parameters
- present fireratingcloud export parameter values
- install mongo, create \data\db, start mongod
- install node.js

- slides from:
  /a/doc/au/2015/doc/sd11048_connecting_desktop_and_cloud_slides.pptx
  /a/doc/techsummit/2014/doc/ts2014_jeremy_tammik.pptx
  /a/devdays/2015/doc/DevDays 2015 Morning FOMT IOT Forge and More.pptx
  jaime

- madrid:
  revit connect desktop an cloud
  view and data examples
  view and data hands-on workshop
  bim360 opportunities

- decide which things to show
  clone jaime and edit
  update connect desktop cloud, add nosql slides
  point to the hands-on workshops
  ask cyrille about iot demo
  blog on tbc
  add slides from jim
  https://github.com/StephenPreston/tutorial-my.first.viewer-view-and.data
  https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data
  set up fireratingcloud



09:00 – 15:00 Meetings with Avatar and European University.

15:00 - 16:30 Presentation (150 participants):

Future of making things, IoT, Forge
WebGL and the View and Data API
BIM Programming
Connecting the desktop and the cloud
2D cloud-based Revit room editor
FireRating in the cloud

17:00 - 20:30 Workshop (40 participants):

Getting started with the Revit API and Connecting Desktop and Cloud (part 1)
20:30 - 22:00 Social Event

22:00 - 24:00 Dinner with Alberto and Jose

Long day!

Now we are both in the middle of part 2 of the workshop.


&infin;

&#2744;

-->

Everything went very well indeed, with a rather Spanish schedule meaning late hours, so I ended up eating dinner between ten o'clock in the evening and midnight, and falling into bed between one and two in the morning every night.

I am exhausted!




#### <a name="3"></a>The Spanish Nature of Connectivity

So it turned out to be a pretty crazy week with very late hours compared to my usual habits, little sleep, and many exciting technical discussions.

I probably talked more here in the last few days than I have in the entire last few months back in Switzerland, at least as far as programming is concerned.

Connecting the desktop and the cloud is so utterly easy!

The BIM and developer community here is Spain is incredibly enthusiastic about the possibilities this offers.

It has been a great pleasure and honour to work together so closely and intensively with Alberto Arteaga Garcia and above all Jose Ignacio Montes of [Avatar BIM](http://avatarbim.com).

In the past days, Jose and I implemented [FireRatingClient](https://github.com/jeremytammik/FireRatingCloud/tree/master/FireRatingClient),
a new stand-alone [fireratingdb](https://github.com/jeremytammik/firerating) client,
a Revit-independent Windows forms-based sibling of
the [FireRatingCloud Revit add-in](https://github.com/jeremytammik/FireRatingCloud).

You can check it out right away.
The GitHub readme tells you all you need to know to understand it.

You can also check out the [to-do list](https://github.com/jeremytammik/FireRatingCloud#todo) to get an idea of the direction we are headed.

Furthermore, we are working on improvements to the [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp),
which we intend to migrate from CouchDB to node.js and mongodb.


#### <a name="4"></a>Castafiore

On my last night here, Alberto took us
to [La Castafiore](http://www.lacastafiore.net) ([twitter](https://twitter.com/LaCastafioreav)).

A nice dinner accompanied by opera arias and ending in a rather unusual yet also very Spanish manner:

<center>
<img src="/p/2016/2016-01-27_madrid/888_castafiore_600x360.jpg" alt="La Castafiore" width="600">
</center>


#### <a name="5"></a>Zazen

This coming Sunday morning, I will be leading a short [Zazen](https://en.wikipedia.org/wiki/Zazen) meditation session, so here are some notes by [Thích Nhất Hạnh](https://en.wikipedia.org/wiki/Th%C3%ADch_Nh%E1%BA%A5t_H%E1%BA%A1nh) on [how to sit](http://www.lionsroar.com/thich-nhat-hanh-sit):

- Set aside a room or corner or a cushion that you use just for sitting.
- The sound of a bell is a wonderful way to begin sitting meditation. If you don’t have a bell you can download a recording of the sound of a bell onto your phone or computer.
- When you sit, keep your spinal column quite straight, while allowing your body to be relaxed. Relax every muscle in your body, including the muscles in your face. Consider smiling slightly, a natural smile. Your smile relaxes all your facial muscles.
- Notice your breathing. As you breathe in, be aware that you are breathing in. As you breathe out, notice that you are breathing out. As soon as we pay attention to our breath, body, breath and mind come together. Every in-breath can bring joy; every out-breath can bring calm and relaxation. This is a good enough reason to sit.
- When you breathe in mindfully and joyfully, don’t worry about what your sitting looks like from the outside. Sit in such a way that you feel you have already arrived.
- It’s wonderful to have a quiet place to sit in your home or workplace. If you are able to find a cushion that fits your body well, you can sit for a long time without feeling tired. But you can practice mindful sitting wherever you are. If you ride the bus or the train to work, use your time to nourish and heal yourself.
- If you sit regularly, it will become a habit. Even the Buddha still practiced sitting every day after his enlightenment. Consider daily sitting practice to be a kind of spiritual food. Don’t deprive yourself and the world of it.


#### <a name="6"></a>Matins

To end the sitting, I plan to read this nice morning poem by the late [John O'Donohue](http://www.johnodonohue.com) together:

<center>
**Matins &ndash; Eternal Echoes**

II.

I arise today

In the name of Silence
<br/>Womb of the Word,
<br/>In the name of Stillness
<br/>Home of Belonging,
<br/>In the name of the Solitude
<br/>Of the Soul and the Earth.

&#2744;

I arise today

Blessed by all things,
<br/>Wings of breath,
<br/>Delight of eyes,
<br/>Wonder of whisper,
<br/>Intimacy of touch,
<br/>Eternity of soul,
<br/>Urgency of thought,
<br/>Miracle of health,
<br/>Embrace of God.

&#2744;

May I live this day

Compassionate of heart,
<br/>Gentle in word,
<br/>Gracious in awareness,
<br/>Courageous in thought,
<br/>Generous in love.

&#2744;
</center>

It is especially nice in German, or maybe I am just more used to that version nowadays:

<center>
**Morgengedanken**

ich erhebe mich heute
<br/>im namen des schweigens &ndash; schoss des wortes
<br/>im namen der stille &ndash; heim des zugehörens
<br/>im namen der einsamkeit &ndash; der seele und der erde

&#2744;

ich erhebe mich heute
<br/>gesegnet von jeglichem ding
<br/>schwingen des atems
<br/>wonne der augen
<br/>staunen des flüsterns
<br/>nähe der berührung
<br/>dringlichkeit des gedankens
<br/>wunder der gesundheit
<br/>gottes umarmung

&#2744;

möge ich verleben diesen tag als mensch
<br/>mitfühlenden herzens
<br/>gütigen wortes
<br/>freundlichen achtens
<br/>mutigen sinns
<br/>freigebiger liebe

&#2744;
</center>


#### <a name="7"></a>AlphaGo, Machine Learning, Machine Intuition?

Talking about Japanese culture, the [AlphaGo](https://en.wikipedia.org/wiki/AlphaGo) Go computer program has now beaten a grand master of Go.

Machine learning is gradually conquering areas that cannot be cracked by pure combinatorial analysis, like chess, but require learning and something akin to intuition to solve.
