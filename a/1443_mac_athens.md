<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

My Mac Died and I am Alive in Greece #revitAPI #3dwebcoder @AutodeskForge #adsk #aec #bim #socket.io

I had a couple of eventful days
&ndash; My Mac crashed
&ndash; Setting up the new Mac
&ndash; Recompiling the samples connecting BIM and cloud
&ndash; Travel to Athens
&ndash; Athens Forge Meetup
&ndash; Athens Forge Workshop...

-->

### My Mac Died and I am Alive in Greece

I had a couple of eventful days:

- [My Mac crashed](#2)
- [Setting up the new Mac](#3)
- [Recompiling the samples connecting BIM and cloud](#4)
- [Travel to Athens](#5)
- [Athens Forge Meetup](#6)
- [Athens Forge Workshop](#7)

#### <a name="2"></a>My Mac Crashed

Last Friday morning my long-awaited new PC arrived.

The old one, my first Mac, was three and a half years old.

At one o'clock the very same afternoon it crashed with no warning.

Life is very strange.

Full of so-called coincidences.

From then on, I had the 'flashing hard disk' error and was unable to boot.

Starting it up in recovery mode by holding down `Cmd` + `R` brought me to the disk utility, which was able to verify but not repair my disk.

I visited the nearest Apple store, they took the machine with them to the workshop.
When they returned it twenty minutes later, the disk was no longer accessible at all and almost all hope of saving its contents was lost.

So then I started setting up the new computer.

Before the crash, I was under total stress.

After the crash, I relaxed a bit.

I was still 100% committed to getting things up and running plus complete all important pending tasks, knowing that several days of additional work are needed, and yet I was somehow less under stress.

Life is strange indeed.


#### <a name="3"></a>Setting Up the New Mac

I received the Mac with Microsoft Office for Mac set up.

From then on, I was on my own.

I had a backup of all my personal data files, just a day or two out of date, but all my installed applications and settings were lost.

So far, I downloaded, installed and reactivated the following components and 'connecting desktop and cloud' samples:

- Chrome for Mac
- iTerm2 2.1.4
- TrueCrypt 7.1a Mac OSX
- Skype 7.28.316
- Git 2.8.1
- TextMate 2.0 beta 9.2
- GCC, Portfile
- Parallels Desktop 11 for Mac Upgrade 11.2.0
- Windows 8.1 with Update x64 UK English
- Revit 2017
- Chrome for Windows
- CouchDB on Windows
- Restored roomedit CouchDB database
- Visual Studio Enterprise 2015 with Update 2
- [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) sample [release 2017.0.0.3](https://github.com/jeremytammik/RoomEditorApp/releases/tag/2017.0.0.3)
- Revit 2017 SDK
- Pcal 4.11.0, the Postscript calendar program 
- Node.js for Mac 4.4.5
- Node.js for Windows
- [Roomedit3d](https://github.com/jeremytammik/roomedit3d) sample [release 0.0.6](https://github.com/jeremytammik/roomedit3d/releases/tag/0.0.6)
- [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud). sample [release 2017.0.0.18](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.18)
- [Python-Markdown](https://pythonhosted.org/Markdown) to process this blog post &nbsp; :-)
- [MediaInfo command line version](http://mediaarea.net/en/MediaInfo)
- [Visual Studio Productivity Power Tools 2015](https://visualstudiogallery.msdn.microsoft.com/34ebc6a2-2777-421d-8914-e29c1dfa7f5d)
- [Kdiff3](https://sourceforge.net/projects/kdiff3), a graphical text difference analyser
- [GIMP](http://www.gimp.org) 2.8.16
- [Kid3](http://kid3.sourceforge.net) audio tagger
- [Vox](http://coppertino.com) music player

(P.S. I added the last few music related items later for my personal reference)


I am still working on getting the 'wherever change directory' utility `wcd` up and running, which requires a wide character version of `ncurses`...

Maybe I can ditch it and use the newest iTerm2 functionality instead, though...

I am also continuously editing my `.bash_profile` to restore lost settings like the command prompt, paths, aliases, etc.

I just had an idea: I now store `.bash_profile` in my personal backed-up data area, and just place a hard link to it in my un-backed-up home directory.

I was previously using the Komodo editor and now switched to TextMate instead, so there is lots of keyboard shortcut learning and macro reimplementation waiting for me there.

#### <a name="4"></a>Recompiling the samples connecting BIM and cloud

I mentioned the three reanimated samples connecting BIM and cloud above in the list of downloads:

- [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) sample [release 2017.0.0.3](https://github.com/jeremytammik/RoomEditorApp/releases/tag/2017.0.0.3)
- [Roomedit3d](https://github.com/jeremytammik/roomedit3d) sample [release 0.0.6](https://github.com/jeremytammik/roomedit3d/releases/tag/0.0.6)
- [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud). sample [release 2017.0.0.18](https://github.com/jeremytammik/FireRatingCloud/releases/tag/2017.0.0.18)

Most of the enhancements I made were related to using the official NuGet packages instead of manually installed ones, and fixing the ensuing differences and errors.

#### <a name="5"></a>Travel to Athens

Two reasons for the pressure I felt last Friday were the preparations for the upcoming Forge DevCon, and, more imminently, 
the [Athens Forge meetup and workshop](http://thebuildingcoder.typepad.com/blog/2016/04/room-editor-first-revit-2017-addin-migration.html#1) taking 
place this week.

Accordingly, I am now in Greece, where I happily and unexpectedly ended up at the Lake Marathon:

<center>
	<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157669153659585" title="Lake Marathon"><img src="https://c8.staticflickr.com/8/7627/27380355175_4053c18c0a_n.jpg" width="320" height="240" alt="Lake Marathon"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

Here are the two events here in the coming days:


#### <a name="6"></a>Athens Forge Meetup

Tomorrow, we are presenting at 
the [meetup](http://www.meetup.com/de-DE/I-love-3D-Athens/events/230543759) 
at [The Cube Athens](http://thecube.gr):

- Overview of the Forge Platform
- Presentations by local partners describing their use of and advantages using web services
- BIM 360 at a glance

Be among the first to use the next generation of Autodesk cloud services. Get hands-on with the tools that will help you to create new service offerings, solutions, and integrations for a cloud-connected product-development ecosystem.

In detail, we present:

- Peter on the [Autodesk Forge](http://forge.autodesk.com) program, platform and fund
- Lambros from Plexscape on his progress using Forge web services
- Anthony from Solid Iris Technologies is the maker of [Thea Render](https://www.thearender.com), a high performance physically based rendering solution and will talk about his Autodesk Fusion 360 app, including the journey to app submission. 
- Jeremy on BIM360 at a glance.


#### <a name="7"></a>Athens Forge Workshop

A workshop on how to set up a Node.js web server and client-side JavaScript code to display and interact with CAD models from over 50 different source file formats, based on open source WebGL and Three.js technology.

The API allows developers to create interactive websites for all kinds of different markets where 3D interaction could be added.

The web world has changed. Now that all the most popular web browsers support WebGL, 3D on the web is a reality. With this workshop, JS and 3D enthusiasts can learn to create interactive web application and also get a head start on VR apps.

We share our knowledge using WebGL and Three.js technology based on two APIs and guide attendees through the entire process of building a simple application, including writing a node.js web server, use of npm, hosting the web application and interacting with the 3D model by running different viewer JS extensions.

First, using the REST API, we set up the Node.js server, enabling you to upload and translate 2D/3D models into a light-weight format for downloading and displaying on the client side. 

The second part is a client-side JS API that allows you to embed, customize through JS extensions and automate an interactive 2D and 3D model viewer on your web page. We can also investigate the use of a Web IDE that allows us to host our created Github repos, run a dedicated NPM and host our 3D website in the Cloud. 

The intended audience for this workshop is JS and 3D enthusiasts with the desire to take their web apps to the next level and create attractive and eye-candy apps with a head start to the design of their own VR apps.

The workshop enables you to:

- View 2D and 3D models in any browser or device without installing any plug-in or additional software. 
- Create and run a Node.js application. 
- Host your 3D website using 3rd party Web IDE. 
- Use the Extension API in the WebGL viewer to interact with the 2D and 3D models. 
- Identify resources for learning more about the technology. 
