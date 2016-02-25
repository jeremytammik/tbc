<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- blog about rtc and desktop-cloud

#dotnet #csharp #geometry
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #adsklabs #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon #revitapi #3dwebcoder
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#au2015 #rtceur
#adskdevnetwrk

Revit API, Jeremy Tammik, akn_include

Connecting Desktop and Cloud, Room Editor Update #revitapi #bim #aec #3dwebcoder #au2015 #rtceur #cloud #svg

Last week, I listed my three Revit Technology Conference classes on connecting the desktop and the cloud, The Building Coder chatroom, and published the full detailed handout of the lab on getting started with Revit macros. This week, I published the notes from the Revit API panel Q&A. So what do we have left to talk about? Oh yes, connecting the desktop and the cloud. That is actually the most interesting of the three, and also the topic of my Autodesk University session
&ndash; Connecting the desktop and the cloud
&ndash; Message and Takeaway
&ndash; The 2D cloud-based round-trip room editor
&ndash; FireRating in the cloud
&ndash; The CompHound component tracker
&ndash; Dotty animated 3D assembly instructions
&ndash; Conclusion...

-->

### Connecting Desktop and Cloud, Room Editor Update

Last week, I listed
my [three Revit Technology Conference classes](http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html) on
connecting the desktop and the cloud, The Building Coder chatroom,
and published the full detailed handout of the lab
on [getting started with Revit macros](http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html#7).

This week, I published the notes from
the [Revit API panel Q&amp;A](http://thebuildingcoder.typepad.com/blog/2015/11/rtc-budapest-and-the-revit-api-panel.html) held during The Building Coder chatroom.

So what do we have left to talk about?

Oh yes, connecting the desktop and the cloud.

That is actually the most interesting of the three, and one I spent a lot of time and effort preparing for in the past month or two, working on
the [CompHound component tracker project](https://github.com/CompHound/CompHound.github.io).

I will also be presenting this at Autodesk University in Las Vegas in a few weeks time, in
session [SD11048 *Connect desktop and cloud: analyse, visualise and report universal component usage*](http://thebuildingcoder.typepad.com/blog/2015/05/connecting-desktop-cloud-lines-and-grid-segments.html#2), cf.
[AU class catalog](https://events.au.autodesk.com/connect/dashboard.ww) &gt;
[SD11048](https://events.au.autodesk.com/connect/dashboard.ww#loadSearch-searchPhrase=SD11048&searchType=session&tc=0).

I still want to flesh it out a bit further, and putting down what I have so far here and now will definitely help.

By the way, my AU handout is due next Friday... four more working days to go... and a weekend...

So here goes:

- [Connecting the desktop and the cloud](#2)
- [Message and Takeaway](#2.1)
- [The 2D cloud-based round-trip room editor](#3)
- [FireRating in the cloud](#4)
- [The CompHound component tracker](#5)
- [Dotty animated 3D assembly instructions](#6)
- [Conclusion](#7)



#### <a name="2"></a>Connecting the Desktop and the Cloud

Here is a summary of my session #44 at
the Revit Technology Conference [RTC Europe](http://www.rtcevents.com/rtc2015eu) in
Budapest, *Connecting the Desktop and the Cloud &ndash; cloud-based universal component and asset usage analysis, visualisation and reporting*.

I present four different examples:

- [The 2D cloud-based round-trip room editor](#3)
- [FireRating in the Cloud](#4)
- [The CompHound component tracker](#5)
- [Dotty animated 3D assembly instructions](#6)

Let's look at each of these in turn.

#### <a name="2.1"></a>Message and Takeaway

The message and takeaway that I want to bring across is this:

It is easy to hook up a Revit or any other desktop application with the cloud.

This enables you to provide read and write access to any properties and data you like.

The possibilities are powerful and infinite.

If you have looked at any web technologies, you will have noticed an abundance of open source technology stacks and libraries providing all functionality you can ever possibly use and more.

In this presentation, I demonstrate and discuss a very simple working example, FireRating in the Cloud, and my current work in progress, CompHound.

You can grab these samples to get started implementing your ideas really fast.

All my research is extensively documented, so it should help you circumvent al the numerous snags I hit and resolved.

Let's dive in:



#### <a name="3"></a>The 2D Cloud-Based Round-Trip Room Editor

This project was my first serious exploration of a desktop-cloud connection.

I worked on it back in 2013, presented it at two Autodesk Tech Summit conferences and at Autodesk University 2013, in the session
[DV1736 &ndash; *Cloud-Based, Real-Time, Round-Trip, 2D Revit Model Editing on Any Mobile Device*](http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2013/building-design-suite/dv1736#chapter=0).

It consists of two components:

- The [RoomEditorApp Revit add-in](https://github.com/jeremytammik/RoomEditorApp)
- The [roomedit CouchDB web-based NoSQL database](https://github.com/jeremytammik/roomedit)

It demonstrates bi-directional data exchange between the two, i.e., between a Revit BIM and a globally accessible cloud-based web database, usable on any device, in any browser.

The display and editing includes 2D dragging, implemented using
SVG, [scalable vector graphics](https://en.wikipedia.org/wiki/Scalable_Vector_Graphics).

The Revit add-in captures a simplified plan view of rooms and the furniture contained in them and exports that to the web database.

<center>
<img src="img/roomedit_2014_cafeteria.png" alt="Room editor in browser">
</center>

It captures the containment and relationship hierarchy including Project &rarr; Level &rarr; Room &rarr; Furniture = family instance &rarr; family symbol.

For the latter three, it also captures the 2D geometry:

- Room: 2D boundary loops.
- Furniture instance: 2D transform, i.e. location and rotation.
- Furniture family symbol: XY-projected 2D boundary loops.

The geometry is encoded in SVG strings, enabling easy visualisation in any browser on any device.

This data is stored in the database and displayed in the browser, supporting the following steps:

- Navigate through the Project &rarr; Level &rarr; Room containment hierarchy
- Display a 2D view of a selected room with the furniture it contains
- Click (or touch) and drag to modify the furniture rotation and location within the room
- Edit any of the writeable furniture Revit parameters

The main point is still to come, though:

<center>
<img src="img/roomedit_ext_app_icons.png" alt="Room editor add-in commands" width="367">
</center>

Besides simply exporting the BIM data in the `Upload Rooms` and `Upload All Rooms` external commands, the Revit add-in implements two more:

- `Update Furniture`: reimport modified data from the database &ndash; this manual operation reads the browser-edited furniture rotation and location and updates the BIM accordingly.
- `Subscribe`: set up an external event to automatically poll for web database changes and immediately update the BIM in real time with no manual intervention at all.

Look at the [AU recording](http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2013/building-design-suite/dv1736) pointed
to above to see this live, and check out the implementation and complete source code in the
two [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp)
and [roomedit](https://github.com/jeremytammik/roomedit) GitHub repos.

For more information and the entire development process, please refer to
the [numerous other discussions](https://duckduckgo.com/?q=Jeremy+Tammik+1736+Cloud-based+Real-time+Round-trip+2D+Revit+Model+Editing+on+any+Mobile+Device).

By the way, I implemented a couple more enhancements to the Revit add-in in the last couple of days:

- Migration to Revit 2016.
- Cleanup of the external event handling unsubscription process.

That brings me up to the current [release 2016.0.0.5](https://github.com/jeremytammik/RoomEditorApp/releases/tag/2016.0.0.5).



#### <a name="4"></a>FireRating in the Cloud

[FireRating in the Cloud](https://github.com/jeremytammik/FireRatingCloud) is
a multi-project re-implementation of the FireRating SDK sample, again with a Revit add-in interacting with a cloud-based database.

In this case, the [fireratingdb](https://github.com/jeremytammik/firerating) database is implemented as
a [node.js](https://nodejs.org) web server driving
a [MongoDB](https://www.mongodb.org)
scalable [NoSQL](https://en.wikipedia.org/wiki/NoSQL) web database.

Again, bi-directional data exchange between the Revit BIM and a globally accessible cloud-based web database is enabled, again usable on any device, in any browser.

I discussed the research and development for this in ample depth
on [The 3D Web Coder](http://the3dwebcoder.typepad.com) blog.
Here is
an [overview of the related articles](https://github.com/jeremytammik/FireRatingCloud).

I host the node.js web server
on [Heroku](https://heroku.com) for
free, and the MongoDB web database
on [mongolab](https://mongolab.com).

Deploying a GitHub project to Heroku can be totally automated.

Using Mongolab to host the database is more comfortable than keeping it locally.

I can switch back and forth each of these between remote or local deployment by simply setting two Boolean variables.

Showing these components with Heroku, Mongolab, and Revit all working together is utterly cool.

To recapitulate:

The FireRating in the Cloud project is a slight enhancement and modernisation of the ancient and well-known FireRating Revit SDK sample.

The latter implements three external commands:

- Create a shared 'Fire Rating' parameter and bind it to the Doors category.
- Export fire rating values for all doors in a project to an external spreadsheet.
- Import the modified values back into the project.

FireRating in the Cloud adds one single little enhancement:

- Store data for multiple projects to one single cloud-hosted database.

Implementation:

- FireRating in the Cloud consists of two components, a web server and a Revit add-in.
    - The web server is a very simple node.js REST server driving a MongoDB database.
    - The Revit add-in implements the same three external commands as the original SDK sample and uses REST to read and write the fire rating values to the cloud-hosted database.

Demo: we looked at this live, both locally on the desktop and live on the web, with the web server hosted by Heroku and the database by mongolab, all completely free of cost for this small sample.

We can also dive into the source code at this point.

The repercussions are huge:

- All users can share data globally for all projects with zero installation on any device.
- You can restrict access as you see fit.

You can imagine the rest for yourself.

The developers loved it, both in its efficacy and simplicity.

Even some Autodesk developers, including a senior Revit software architect, said they learned a lot of new stuff about possibilities using the cloud and scalable NoSQL databases to take back to the Revit development team  :-)



#### <a name="5"></a>The CompHound Component Tracker

I have been working heavily on
the [CompHound component tracker](https://github.com/CompHound/CompHound.github.io) in
the past couple of weeks.

CompHound is similar to and based on the FireRating in the Cloud project.

Again, it consists of a web server driving a mongo database with a REST API invoked by a Revit add-in.

In addition, it also sports a user interface for online component usage analysis, reporting, bills of materials, viewing and model navigation.

The Revit add-in is simpler that FireRatingCloud one, since it only implements one-way data writing from the desktop to the cloud.

The web server is significantly more complex, though.

For more details, please refer to
the [overview of the existing extensive documentation](https://github.com/CompHound/CompHound.github.io).

The one and only thing that I will mention is that it also includes a viewer enabling 3D exploration of the component occurrences in situ, using
the [Autodesk View and Data API](http://developer.autodesk.com).

Here is another compelling example of using that:



#### <a name="6"></a>Dotty Animated 3D Assembly Instructions

This one is really self-explanatory.

Check out this super cool sample showing how
the [View and Data API](https://developer.autodesk.com) viewer
can be used to
display [animated 3D assembly instructions](http://trial.dotdotty.com/share?shareId=431c-bac8-eedb-e43d-fc79&iframe=true) in
the browser:

<center>
<a href="http://trial.dotdotty.com/share?shareId=431c-bac8-eedb-e43d-fc79&iframe=true">
<img src="img/dotty_assembly_instructions.png" alt="Dotty animated 3D assembly instructions" width="400"/>
</a>
</center>

Can you imagine assembly instructions based on simple 2D images, drawing, words, trying to compete with this?

Think of the end user aspect, reading and understanding static information.

Also consider the preparation side, all the careful analysis, editing and translation that needs to go into such a textual description.

You have probably seen the industry and culture moving away from text to printed 2D images with minimal wording... imagine where it will go from here.

How long do you think people will still be using static assembly instructions printed on paper?

Me, I even keep my personal [collection of recipes](http://jeremytammik.github.io/recipe) in a GitHub repository so I can read them comfortably on an iPad in the kitchen while cooking...


#### <a name="7"></a>Conclusion

The participants were quite excited about this session, and I received a lot of positive feedback and interest on it during the following days at the RTC.

I guess the main takeaway is:

- Wow, how simple is this!
- Wow, how scalable is this! I can put specific data for all my projects into one single container for global search and analysis.
- Wow, how powerful is this stuff working with interactive 3D models in the browser!
- Wow, how useful is it to be able to share certain data globally in the browser, with anybody I choose, with zero installation, on any mobile device!

I am sure you can find some use for these kinds of opportunities as well.

If you don't, please watch out for those that do...

For the sake of completeness, here is
my [slide deck](zip/s1_3_pres_cloud_based_analyse_view_jtammik.pdf) for this session.

What a nice way to finish my week!

Happy weekend to all!

<!-----

#### <a name="#6"></a>Lukas Baerfuss Swiss Critical Text and Reactions

Switzerland has recently been discussing [Lukas Bärfuss' critical text](/j/tmp/)

http://www.faz.net/aktuell/feuilleton/debatten/streit-um-die-schweiz-dieser-versuchsballon-reicht-nicht-er-stinkt-zum-himmel-13866863.html

http://www.faz.net/aktuell/feuilleton/debatten/streit-um-die-schweiz-dieser-versuchsballon-reicht-nicht-er-stinkt-zum-himmel-13866863-p2.html

http://www.faz.net/aktuell/feuilleton/debatten/streit-um-die-schweiz-dieser-versuchsballon-reicht-nicht-er-stinkt-zum-himmel-13866863.html?printPagedArticle=true#pageIndex_2

<center>
<img src="" alt="">
</center>

https://internal.autodesk360beta.com/portal/groups/2013071140751/pages/PG5a730QTbf0022d075180c24de184e2f60d?cacheToken=tWlK57sCgwrwQzb3CXjqp6g7Y50CHYWkF3z1hGTFrbgyLdgN0T1qJzB8kb745f24be7f9150d1817116

https://internal.autodesk360beta.com/portal/groups/2013071140751/pages/PG5a730QTbf0022d075180c24de184e2f60d

--->
