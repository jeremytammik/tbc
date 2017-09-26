<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/my-first-plugin-quick-modification/m-p/7408596

- getting started with web programming

- problem in SDK sample?
  https://forums.autodesk.com/t5/revit-api-forum/modelessform-externalevent-sdk-sample/m-p/7370034
  workaround

My First #RevitAPI Plugin Enhancements @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/firstplugin

Today, let's return to the recurring topic of getting started with the Revit API, its SDK samples, and programming in general
&ndash; My First Revit Plugin enhancements
&ndash; Getting started with web programming &ndash; FreeCodeCamp
&ndash; ModelessForm_ExternalEvent initialisation...

--->

### My First Revit Plugin Enhancements

Today, let's return to the recurring topic of getting started with the Revit API, its SDK samples, and programming in general:

- [My First Revit Plugin enhancements](#2)
- [Getting started with web programming &ndash; FreeCodeCamp](#3)
- [ModelessForm_ExternalEvent initialisation](#4)

####<a name="2"></a>My First Revit Plugin Enhancements

A couple of people recently mentioned issues getting started with the Revit API, e.g. in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) threads
on [Revit API Visual Studio error/warnings](https://forums.autodesk.com/t5/revit-api-forum/revit-api-visual-studio-error-warnings/m-p/7357530/thread-id/24998) 
and [IExternalCommand could not be found](https://forums.autodesk.com/t5/revit-api-forum/iexternalcommand-could-not-be-found/m-p/7386154).

There are several ways to quickly get started with the Revit API, which I present as completely and succinctly as I can in
the [getting started overview](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2).

The first problem you might encounter is getting your add-in loaded into Revit.

That is simple to resolve once you understand what is going on.

Once way is to follow
the [developer guide section on getting started](http://help.autodesk.com/view/RVT/2018/ENU/?guid=GUID-C574D4C8-B6D2-4E45-93A5-7E35B7E289BE) and
work through the hello world samples presented there.
 
Another way is to follow the [*My First Plugin* video tutorial](http://www.autodesk.com/myfirstrevitplugin).

[Elie Accari](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/5126255) very
kindly shared a couple of valuable enhancement suggestions for it in his thread 
on [My First Plugin &ndash; quick modification](https://forums.autodesk.com/t5/revit-api-forum/my-first-plugin-quick-modification/m-p/7408596):

I refer to the high entropy body of knowledge
in [My First Plugin](http://www.autodesk.com/myfirstrevitplugin).
 
Other threads mentioned issues some users are facing getting the plugin to work.
 
The above linked page includes a feedback email which has bounced back ("myfirstplugin wasn't found at autodesk.com"), so I am posting here in case new users seek to implement the same modification.
 
<b><u>Issue #1:</u></b> Location of the Plugin

Not all users have access to the `AddIns` folder in `Program Files`, but all users have access to the *%AppData%\Roaming\Autodesk\Revit\addins\<version>* folder. If this is mentioned in the tutorial, it can reduce the number of questions asked.
 
<b><u>Issue #2:</u></b> Unfamiliar Behaviour

The code does not properly copy the group. It just places it where you click, which could be in the middle of the other room. A more familiar way is to do a Copy-From:-To:. Only 3 lines of code including 2 modifications are required.
 
<b><u>Modification #1:</u></b> Pick 2 points and calculate the distance between the two, instead of picking only 1 point.
 
To do so, replace the following code in the original lesson:

<pre class="code">
  //Pick a point
  XYZ point = sel.PickPoint("Please pick a point to place group");
</pre>

With the following:
 
<pre class="code">
  //Pick a point
  XYZ pointFrom = sel.PickPoint("Pick a point to copy From");
  XYZ pointTo = sel.PickPoint("Pick a point to copy To");
  //Calculate the distance between the two points
  XYZ ptDist = pointTo - pointFrom;
</pre> 
 
<b><u>Modification #2:</u></b> Use the Copy method instead of Place
 
Replace the following line between the `trans.Start` and `trans.Commit` lines:
 
<pre class="code">
  doc.Create.PlaceGroup(point, group.GroupType);     
</pre> 
 
With this line:
 
<pre class="code">
  ElementTransformUtils.CopyElement(doc, elem.Id, ptDist);
</pre>

Finally, a suggestion: the text seem to entice Revit users into programming. In this regard, I would consider doing the following:
 
Add Lesson 0 (or renumber the lessons): Implement the same code in a macro using the Edit / SharpDevelop IDE readily accessible from inside Revit, without going to Microsoft Visual Studio.
 
Then Lesson 1 could become: *Now let's do this again by converting this macro into an add-in*.

Ever so many thanks to Elie for his constructive criticism and extremely helpful advice and suggestions!

I passed it on to my colleagues for consideration next time the tutorial is re-recorded.
 

####<a name="3"></a>Getting Started with Web Programming &ndash; FreeCodeCamp 

By the way, in case you are interested in getting started with web programming in addition to the desktop and .NET based Revit API, I can recommend the training sequence provided 
by [freeCodeCamp](https://www.freecodecamp.org) covering the following list of topics:

<center>
<img src="img/freecodecamp_logo.png" alt="freeCodeCamp" width="263"/>
</center>

- Front End Development Certification
    - HTML5 and CSS
    - Responsive Design with Bootstrap
    - jQuery
    - Basic Front End Development Projects
    - Basic JavaScript
    - Object Oriented and Functional Programming
    - Basic Algorithm Scripting
    - JSON APIs and Ajax
    - Intermediate Front End Development Projects
    - Intermediate Algorithm Scripting
    - Advanced Algorithm Scripting
    - Advanced Front End Development Projects
    - Claim Your Front End Development Certificate
- Data Visualization Certification
    - Sass
    - React
    - React Projects
    - D3
    - Data Visualization Projects
    - Claim Your Data Visualization Certificate
- Back End Development Certification
    - Automated Testing and Debugging
    - Node.js and Express.js
    - Git
    - MongoDB
    - API Projects
    - Dynamic Web Application Projects
    - Claim Your Back End Development Certificate
- Video Challenges
    - Computer Basics
    - The DOM
    - JavaScript Lingo
    - Chrome Developer Tools
    - Big O Notation
    - Accessibility
    - Agile
    - Computer Science
    - Data Visualization
    - Embedded and Internet of Things
    - Game Development
    - Gamification
    - Machine Learning
    - Math for Programmers
    - Mobile JavaScript Development
    - DevOps
    - Software Engineering Principles
    - Statistics
    - Tools
    - User Experience Design
    - Visual Design
- Open Source for Good
- Coding Interview Preparation
    - Coding Interview Training
    - Mock Interviews

If you are a complete beginner undecided what language to start with, the 11-minute video on [what programming language should I learn? Front-end? Back-end? Machine learning?](https://youtu.be/VqiEhZYmvKk) might help:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/VqiEhZYmvKk" frameborder="0" allowfullscreen></iframe>
</center>


####<a name="4"></a>ModelessForm_ExternalEvent Initialisation
 
Before closing, let me mention another little hint resolving an issue encountered
by [Aziz](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2043172) in 
the [ModelessForm_ExternalEvent SDK sample](https://forums.autodesk.com/t5/revit-api-forum/modelessform-externalevent-sdk-sample/m-p/7370034):

**Question:** I am new to develop in Revit API getting started tutorials and all is going well until now, when I started with the SDK samples.
 
I am just trying to implement the `ModelessForm_ExternalEvent` sample, but it keeps throwing me an exception stating that *Object reference not set to an instance of an object*.

**Answer:** However, after more trials, I tried to change something and it did magically work. I created a class instance from but not assigning it a null value. I am sharing it below in case someone else is experiencing the same issue:
 
<pre class="code">
  // class instance
  Public static Application thisApp = new Application();
</pre>

