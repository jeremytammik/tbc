<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

Join in the Revit UI can mean several completely different things:

- Family editor, two solids -- CombineElements
- Family editor, two colinear beams of same material -- visual join only, remove edges
- Project editor, two colinear walls of same material -- creates a single wall?
- Project editor, two perpendicular walls of same material -- visual join only, remove edges
- what other joins are there?
- how are they represented and accessible in the API?

- Michael Porskær of [Orbicon A/S](http://www.orbicon.dk)

- Historic Achievement: Microsoft researchers reach human parity in conversational speech recognition
http://blogs.microsoft.com/next/2016/10/18/historic-achievement-microsoft-researchers-reach-human-parity-conversational-speech-recognition/#c4Eb3dzo3TjivSZY.99
  http://blogs.microsoft.com/next/2016/10/18/historic-achievement-microsoft-researchers-reach-human-parity-conversational-speech-recognition/#sm.0000otuyxfvc7fj810qiwj3udx94g
  
- portable sound system with really good loudpseakers: HK Audio Lucas Nano 600 eur 1300 -- http://hkaudio.com/products.php?id=414

- http://www.moving-awareness.com -- open floor wave # mohr–villa gewölbesaal, situlistraße 73, 80939 münchen, in der mohr–villa in münchen freimann, u6 r garching bis freimann, dann der beschilderung folgen, zu fuß ca. 5 minuten,


<code></code>

 @AutodeskForge #revitapi @AutodeskRevit #aec #bim

&ndash; 
...

-->

### Edit and Continue

Here I am in Munich, supporting the
one-week [Forge accelerator](http://autodeskcloudaccelerator.com) workshop.

I still have lots of exciting things to report from the
the [RTC Revit Technology Conference Europe](http://www.rtcevents.com/rtc2016eur) in Porto last week.

Maybe the most , and

here at the Autodesk offices in Munich.

A lot of questions already came up:

- [](#2)
- [](#3)
- [](#4)
- [](#5)

- [The first-ever public Revit roadmap](#7)


<center>
</center>

#### <a name="2"></a>Arrival in Munich and Hotel Schlicker

I am visiting a friend here in Munich starting today and needed a hotel for the first night.

I had no time to research and book anything in advance, so I just got off the subway at Marienplatz and started walking.

As luck would have it, I discovered the nicest hotel I have ever stayed at.

With just three stars and a moderate price,
[Hotel Schlicker](http://www.hotel-schlicker.de) beats
every single one of the five-star global hotel chain establishments that I normally have the bad luck to end up in.

Highly recommended!


#### <a name="3"></a>Art of README

A very nice and fundamentally important article for anyne sharing code, whether on GitHub or elsewhere, pointed out by  [Philippe Leefsma](http://twitter.com/F3lipek):

<center>
<span style="font-size: 120%; font-weight: bold">
[Art of README](https://github.com/noffle/art-of-readme)
</span>
</center>

Thank you, Philippe, very nice indeed!



#### <a name="4"></a>Neo4j

[Neo4j](https://neo4j.com/)


[document-based or graph database, MongoDB vs Neo4j](http://stackoverflow.com/questions/14793335/should-i-go-for-document-based-or-graph-database-mongodb-vs-neo4j)


#### <a name="5"></a>What is Join?

I made a shocking discovery in a discussion with a participant here at the Forge accelerator.

I always thought that Revit 'Join' operation was a sort of Boolean operation.

Now I discovered that it is not at all.

In some cases, it is more about not displaying certain edges of adjacent colinear and coplanar elements of the same type and material.

Maybe this is also the functionality driven by the [JoinGeometryUtils class](http://www.revitapidocs.com/2017/c45b6484-3efd-1d81-0b47-ba678857fff1.htm)?

Not a Boolean operation at all?

Under other circumstances, it can apparently mean other things.

For instance, in the family editor, joining two solids may or may not cause a Boolean operation, which may or may not correspond to the [CombineElements method](http://www.revitapidocs.com/2017/5c33a711-2891-f353-5f39-24ba175be452.htm).

I would love to have more clarity on this, and so might others as well...


#### <a name="2"></a>Can I Replace Myself by Artificial Intelligence?

I answered a lot of repetitive questions in the last couple of weeks and months &ndash; not to mention years and decades.

That led me to realise recently that I am pretty sure that at least 10-20% of my work could be automated.

The tasks that seem most easy to address are responses to simple beginner or background questions raised in certain email messages and discussion forum threads.

I would need tools to:

- Monitor everything I do, specifically email and discussion forum thread responses.
- Observe and learn which cases lead to similar and simple replies.
- Be able to automatically and autonomously scan my email input and certain discussion forums.

After a couple of months of training, I think the system should be able to:

- Monitor my incoming email and a couple of forums for issues to arise that it recognises.
- Automatically generate a response and rate the probability that it is appropriate.

Initially, I would expect it to rate all probabilities at zero percent.

At that point, I should be able to start training it.

Some of the auomatically generated reponses I would classify as acceptable and use as is.

Others (or all?) might need some tweaking, and could also be used after slight improvement.

Same might be way off, of course.

Training would begin.

Who know, maybe I am already much further, and the text you are reading here now was AI generated?

Here are two other AI, CAD and communication related items:

- [Autodesk Design Graph](#)
- [Artificial Intelligence Recognises conversational speech](#)



#### <a name="2"></a>Design Graph

[Design Graph](https://dg.autodesk.com) provides a new way to explore your 3D design data using shape-based machine learning to recognize and understand parts, assemblies and entire designs. It learns to identify the relationships between all parts within and across all of your designs, irrespective of whether cross-references exist. It learns to interpret designs in terms of those parts and provides a way to navigate your data using simple text search, learned categories of parts, shape similarity, usage patterns and even smart filters for part numbers, materials and other properties.


#### <a name="5"></a>Artificial Intelligence Recognises conversational speech

[Historic Achievement: Microsoft researchers reach human parity in conversational speech recognition](http://blogs.microsoft.com/next/2016/10/18/historic-achievement-microsoft-researchers-reach-human-parity-conversational-speech-recognition/#c4Eb3dzo3TjivSZY.99)

[Historic Achievement: Microsoft researchers reach human parity in conversational speech recognition](http://blogs.microsoft.com/next/2016/10/18/historic-achievement-microsoft-researchers-reach-human-parity-conversational-speech-recognition/#sm.0000otuyxfvc7fj810qiwj3udx94g)

#### <a name="5"></a>Good Portable Loudspeakers

I went to a nice little [open floor](http://openfloor.org/) dance event Sunday evening after arriving here in Munich, in
the [Mohr-Villa Kulturzentrum](http://www.mohr-villa.de/), organised
by [who?](http://www.moving-awareness.com).

I only mention it to point out (and remember for myself) that she was using a portable sound system with really good loudpseakers,
the [HK Audio Lucas Nano 600](http://hkaudio.com/products.php?id=414)... just a note to self.



<center>
<img src="img/.png" alt="" width="309">
</center>

aggregation sample https://forge.autodesk.io

https://github.com/leefsmp/forge



#### <a name="6"></a>The First-Ever Public Revit Roadmap

Join the discussion!

http://www.autodesk.com/revitroadmap

http://forums.autodesk.com/t5/revit-roadmaps/the-first-ever-public-revit-roadmap/ba-p/6633199

mentioned briefly in the Revit API panel discussion notes

http://thebuildingcoder.typepad.com/blog/2016/10/rtc-revit-api-panel-idea-station-edit-and-continue.html