<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- [Finland is making its online AI crash course free to the world]
  https://www.theverge.com/2019/12/18/21027840/online-course-basics-of-ai-finland-free-elements

  [Elements of AI](https://www.elementsofai.com)

- https://youtu.be/_IHXGXh_wsg
  《草原上, 田再励中胡独奏 》On the Grassland (Zhonghu Solo)
The mellow and thick sound of the zhonghu sings a melody of the Inner Mongolian style. The listener is brought to the vast Mongolian prairie where the white clouds float in the blue sky and cattle are grazing on the green grasslands.
Zhonghu and gaohu are modern instrumens, the original is called erhu:
https://en.wikipedia.org/wiki/Zhonghu
https://en.wikipedia.org/wiki/Gaohu
https://en.wikipedia.org/wiki/Erhu

twitter:

AI training for everybody and single-click migration for imperial and metric project unit toggle in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/unitmigration

I just helped address a wish in the Revit Idea Station.
I also started taking a course on AI, designed for absolutely everybody
&ndash; Single-click imperial and metric project unit toggle
&ndash; Solution
&ndash; Elements of AI &ndash; crash course for everyone
&ndash; Zhonghu solo music...

linkedin:

AI training for everybody and single-click migration for imperial and metric project unit toggle in the #RevitAPI

http://bit.ly/unitmigration

I just helped address a wish in the Revit Idea Station.

I also started taking a course on AI, designed for absolutely everybody:

- Single-click imperial and metric project unit toggle
- Solution
- Elements of AI - crash course for everyone
- Zhonghu solo music...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Unit Migration Toggles Imperial and Metric Project Units

I just helped address a wish in the Revit Idea Station.
Very satisfying.
I also started taking a course on AI, designed for absolutely everybody:

- [Single-click imperial and metric project unit toggle](#2)
    - [Solution](#3)
- [Elements of AI &ndash; crash course for everyone](#4)
- [Zhonghu solo music](#5)


#### <a name="2"></a>Single-Click Imperial and Metric Project Unit Toggle

The new [UnitMigration add-in](https://github.com/jeremytammik/UnitMigration) migrates
all unit settings from a source RVT to a folder full of target RVT models.

This add-in has been created and published in response to the Revit Idea Station request by Kasper Miller
to [change project units between imperial and metric with one button](https://forums.autodesk.com/t5/revit-ideas/change-project-units-between-imperial-and-metric-with-one-button/idi-p/9235848).

Kasper explains:

This should be easy implement and will save a lot of time.

Currently, the process of converting an Imperial Revit family, project or template to metric or vice versa is very cumbersome.

Particularly if you have do so with several items, which is a task all of us are confronted with very frequently.

One has to toggle through all the unit options, which might consist of 7 categories.
Within each of these, we usually have to change at least two options.

That makes 14 clicks and changes for each instance.

What often happens then, is that users just change the `Length` category, leaving the family mixed, part imperial, part metric, for instance like this:

<center>
<img src="img/project_units_part_imperial_part_metric.png" alt="Mixed project units part imperial part metric" title="Mixed project units part imperial part metric" width="368"/>
</center>

#### <a name="3"></a>Solution

The solution is straight-forward: all unit settings seen by a user in a document in `ProjectUnits` are stored in a container class `DisplayUnit`.

These settings can be read from one document through its `DisplayUnitSystem` and set to another with the `SetUnits` method.

The add-in take the source data from a source document, stores it, and sets it to all target documents. 
 
One could obviously alter this to read data from a text file or something more fancy &ndash; but why bother when you can use a Revit template?

Want to convert to metric?
Select the metric template of your choice and paste it where you want.

Want to convert to imperial? Same thing.

Imperial decimal to imperial fractional? Just like before.

The add-in requires a source file (like a template) from which to read the units; then, it will write those units to all Revit files inside a selected folder.

Grab the add-in source code from
the [UnitMigration GitHub repository](https://github.com/jeremytammik/UnitMigration).

Many thanks to Dragos Turmac, Principal Engineer, Autodesk and Bogdan Teodorescu, Team Manager, Autodesk for implementing and sharing this!

#### <a name="4"></a>Elements of AI &ndash; Crash Course for Everyone

Finland created an official artificial intelligence crash course,
the [Elements of AI](https://www.elementsofai.com), 
to educate its citizens on the basics of this new technology.

More than 1 percent of the entire Finnish 5.5 million population already signed up.

The web site blurb explains:

- Are you wondering how AI might affect your job or your life?
- Do you want to learn more about what AI really means &ndash; and how it’s created?
- Do you want to understand how AI will develop and affect us in the coming years?

Our goal is to demystify AI.

The Elements of AI is a series of free online courses created by Reaktor and the University of Helsinki.
We want to encourage as broad a group of people as possible to learn what AI is, what can (and can’t) be done with AI, and how to start creating AI methods.
The courses combine theory with practical exercises and can be completed at your own pace.

Here is a table of contents:

- Chapter 1 &ndash; What is AI?
    - I. How should we define AI?
    - II. Related fields
    - III. Philosophy of AI
- Chapter 2 &ndash; AI problem solving
    - I. Search and problem solving
    - II. Solving problems with AI
    - III. Search and games
- Chapter 3 &ndash; Real world AI
    - I. Odds and probability
    - II. The Bayes rule
    - III. Naive Bayes classification
- Chapter 4 &ndash; Machine learning
    - I. The types of machine learning
    - II. The nearest neighbor classifier
    - III. Regression
- Chapter 5 &ndash; Neural networks
    - I. Neural network basics
    - II. How neural networks are built
    - III. Advanced neural network techniques
- Chapter 6 &ndash; Implications
    - I. About predicting the future
    - II. The societal implications of AI
    - III. Summary

I already signed up, myself, and completed Chapter 1 in a bit over an hour.

#### <a name="5"></a>Zhonghu Solo Music

To round this off, some beautiful relaxing music,
a [Zhonghu solo titled 草原上, 田再励中胡独奏, On the Grassland](https://youtu.be/_IHXGXh_wsg):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/_IHXGXh_wsg" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

> The mellow and thick sound of the zhonghu sings a melody of the Inner Mongolian style.
The listener is brought to the vast Mongolian prairie where the white clouds float in the blue sky and cattle are grazing on the green grasslands.

Wikipedia teaches me
that [zhonghu](https://en.wikipedia.org/wiki/Zhonghu)
and [gaohu](https://en.wikipedia.org/wiki/Gaohu) are
modern instrumens, developed in the last century, based on the original Mongolian instrument
called [erhu](https://en.wikipedia.org/wiki/Erhu).



