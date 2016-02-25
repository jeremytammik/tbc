<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 11039110 [Drawing revision cloud with our custom design]
- 11039134 [Run a process in background]

#revitapi #adsk #3dwebcoder
#restapi #python
#bim #aec #adskdevnetwrk #dotnet #csharp
#geometry
#grevit
#responsivedesign #typepad
#ah8 #augi #au2015 #dotnet #dynamobim
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce

#Markdown, #Fusion360 #Fusion360Hackathon, Revisions and Bulk Upgrade #revitapi #adsk #3dwebcoder #aec #bim

Revit API, Jeremy Tammik, akn_include

-->



### Markdown, the Fusion Accelerator, Revisions and Bulk Upgrade

Today, I discuss my new Markdown blog post format, Fusion 360, the Fusion accelerator and a couple of Revit issues:

- [Using the Markdown blog post format](#2)
- [Fusion 360](#3)
- [Fusion 360 for game artists](#4)
- [Fusion 360 accelerator](#5)
- [The Revit Revision API](#6)
- [Running a background bulk upgrade batch](#7)
- [Banksy's Dismaland bemusement park](#8)


#### <a name="2"></a>Using the Markdown Blog Post Format

This is my first post on The Building Coder using
[Markdown](http://daringfireball.net/projects/markdown) instead
of handcrafted HTML.

I tested it yesterday on
[The 3D Web Coder](http://the3dwebcoder.typepad.com),
where I also explain some of the
[motivation behind this change](http://the3dwebcoder.typepad.com/blog/2015/08/hackergarten-chromium-and-markdown.html#4).

Suffice to say, it is saving me some time today.

<!--- The markdown version of this post is 9438 bytes in size, the HTML file that I generate from it 8933 bytes. -->

For instance, here is the markdown source code of this section you are reading:

<pre class="prettyprint">
#### &lt;a name="2"&gt;&lt;/a&gt;Using the Markdown Blog Post Format

This is my first post on The Building Coder using
[Markdown](http://daringfireball.net/projects/markdown) instead
of handcrafted HTML.

I tested it yesterday on
[The 3D Web Coder](http://the3dwebcoder.typepad.com),
where I also explain some of the
[motivation behind this change](http://the3dwebcoder.typepad.com/blog/2015/08/hackergarten-chromium-and-markdown.html#4).

Suffice to say, it is saving me some time today.
</pre>

The equivalent HTML looks like this:

<pre class="prettyprint">
&lt;h4&gt;&lt;a name="2"&gt;&lt;/a&gt;Using the Markdown Blog Post Format&lt;/h4&gt;
&lt;p&gt;This is my first post on The Building Coder using
&lt;a href="http://daringfireball.net/projects/markdown"&gt;Markdown&lt;/a&gt; instead
of handcrafted HTML.&lt;/p&gt;
&lt;p&gt;I tested it yesterday on
&lt;a href="http://the3dwebcoder.typepad.com"&gt;The 3D Web Coder&lt;/a&gt;,
where I also explain some of the motivation behind this change.&lt;/p&gt;
&lt;p&gt;Suffice to say, it is saving me some time today.&lt;/p&gt;
</pre>

Typepad supports markdown right out of the box:

<center>
<img src="img/typepad_markdown.png" alt="Typepad Markdown" width="600"/>
</center>

Unfortunately, I discovered yesterday that
[its excerpt generation machinery does not](http://the3dwebcoder.typepad.com/blog/2015/08/hackergarten-chromium-and-markdown.html#4),
so I now have to add that myself by hand.

#### <a name="3"></a>Fusion 360

As an architect, you may not be aware of
[Fusion 360](http://www.autodesk.com/products/fusion-360),
a cloud-based design, test, fabrication and collaboration tool targeted primarily at the manufacturing side of CAD.


#### <a name="4"></a>Fusion 360 for Game Artists

That is not all, however, as Lester Bank's article on
[Fusion 360 for Game Artists](http://lesterbanks.com/2015/08/fusion-360-for-game-artists) explains:

> Solid modellers are just better at some things. Solid modellers are great for knocking out quick forms, and really make short work of Boolean operations, bevels, chamfers and building complex components.

> The problem with Solid modelling applications is that they are typically CAD/CAM, CAE tools not really meant for artists. One application is kind of changing that, and that is Autodesk’s Fusion 360. The application was built from the ground up to be an easy to use solid modeller for conceptual design, engineering, matching and sharing CAD content... using Autodesk’s Fusion 360 in a game art pipeline... get started with Fusion 360’s interface and tools... put together some game art assets... completely affordable... free for students, enthusiasts, hobbyists, and startups...

Maybe you will discover that it is a powerful and useful tool for architects as well?


#### <a name="5"></a>Fusion 360 Accelerator

If you are interested in taking a closer look at Fusion 360, you might as well hit the ground running and earn some money with it right away, in this once in a decade opportunity:

Build your first Fusion 360 app using JavaScript, Python or C++ by participating in the upcoming
[Fusion 360 Hackathon](http://fusion360hackathon.com).

<center>
<p style="font-size:500%; font-weight:bold; color:darkorange">$500</p>
</center>

Build and publish a Fusion 360 app in the
[Autodesk App Store](https://apps.exchange.autodesk.com/FUSION/en/Home/Index) during
the Hackathon, and you earn a $500 reward, too!

For more information, you can also head over to Adam Nagy's take on the
[Fusion 360 hackathon site](http://adndevblog.typepad.com/manufacturing/2015/08/fusion-360-hackathon-site-live.html) and
follow the event tweets at [#Fusion360Hackathon](https://twitter.com/hashtag/fusion360hackathon).

<center>
<img src="img/fusion360hackathon.png" alt="Fusion 360 hackathon" width="400"/>
</center>

<!---
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d14f4463970c-pi" style="display: inline;"><img alt="Hackathon" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d14f4463970c image-full img-responsive" src="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d14f4463970c-800wi" title="Hackathon" /></a></p>
-->

Good luck and have fun!



#### <a name="6"></a>The Revit Revision API

**Question:** I would like to develop a custom operator in Revit with the same
functionality as Revit's Revision cloud but with our custom cloud design
(the cloud design something like Revision cloud of AutoCAD).
Does the Revit API provide the facility to draw these, or free-hand sketching
using the mouse with our custom operator?

**Answer:**
Revit 2015 provides programmatic access to this area, so the
[What's New in the Revit 2015 API](http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html) documentation
for that version covers just about all there is to know about the
[Revision API](http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html#3.04).

You may also be interested in the
[GetRevisionData Revit add-in](http://thebuildingcoder.typepad.com/blog/2014/06/the-revision-api-and-a-form-on-the-fly.html).

I just upgraded it to Revit 2016 for you; check it out in the
[GetRevisionData GitHub repository](https://github.com/jeremytammik/GetRevisionData).


#### <a name="7"></a>Running a Background Bulk Upgrade Batch

**Question:** I am working on a Revit plugin to bulk upgrade Revit's old
version files to a new version. I need to run this process in the background
so that other tasks can be performed in Revit during this upgrade process.
Can you please guide me on how to run an upgrade process in the background?

**Answer:**
The topic of driving Revit from outside or other background threads has been discussed extensively in the
[Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) and on The Building Coder blog,
so all the information that I am aware of on this topic is already sitting there waiting for your perusal.

Please check out the topic group on
[driving Revit from outside](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28) and
explore that material.

I would recommend starting at the end, since the last two entries in that topic list provide a good overview.

Regarding the bulk upgrade, you will probably also be interested in taking a look at Saikat Bhattacharya's
[File Upgrader add-in](http://thebuildingcoder.typepad.com/blog/2011/06/file-upgrader-plugin-of-the-month.html).

It was originally written for Revit 2012 and updated several times for later Revit versions.

For a while, it was also available from the [Autodesk Exchange AppStore](https://apps.exchange.autodesk.com).

I am including the most recent version that I happen to have at hand here, in
[FileUpgrader_2015-08-27.zip](zip/FileUpgrader_2015-08-27.zip).


#### <a name="8"></a>Banksy's Dismaland Bemusement Park

Let's finish off with something unrelated to the Revit API.

Are you interested in modern art or culture?

Somehow, both always seem to include and almost require a certain provocative tinge.

If you are, you are probably aware of the world's most renowned and valued &ndash; both artistically and $-wise &ndash; graffiti artist,
[Banksy](https://en.wikipedia.org/wiki/Banksy).

Check out his newest project, the
[Dismaland Bemusement Park](http://www.banksy.co.uk),
a 'family theme park unsuitable for small children'.

Here is a
[Channel4 news item](http://www.channel4.com/news/dismaland-banksy-bemusement-park-shocking-funny-extraordinary) and two-minute
[YouTube video](https://www.youtube.com/watch?v=PTr9SZTtCHo) to give you a first impression:

<center>
<iframe width="500" height="281" src="https://www.youtube.com/embed/PTr9SZTtCHo" frameborder="0" allowfullscreen></iframe>
</center>
