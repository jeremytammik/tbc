<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- clean up AVF results before design option switch
  how to search all AVF analysis result and remove them?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-search-all-avf-analysis-result-and-remove-them/td-p/10437422

- kfpopeye Revit projects
  Project Sweeper, ReVVed, and other apps now open source
  https://forums.autodesk.com/t5/revit-api-forum/project-sweeper-revved-and-other-apps-now-open-source/m-p/10617548

twitter:

add #thebuildingcoder

Open source projects clean up parameters, the Revit model, and other operations, an important AVF cleanup required to prevent crashing, and some youngsters cleaning up some cash with the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon https://autode.sk/avfcleanup

Open source projects that help clean up parameter values, the Revit model and other operations, an important AVF cleanup required to prevent crashing, and some youngsters cleaning up some cash
&ndash; Kfpopeye open source projects
&ndash; AVF result clean-up before design option switch
&ndash; Young teen and kid sister crypto entrepreneurs...

linkedin:

Open source projects clean up parameters, the Revit model, and other operations, an important AVF cleanup required to prevent crashing, and some youngsters cleaning up some cash with the #RevitAPI

https://autode.sk/avfcleanup

- Kfpopeye open source projects
- AVF result clean-up before design option switch
- Young teen and kid sister crypto entrepreneurs...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Question:** 

**Answer:**

**Response:**  

Many thanks to  for this very helpful explanation!

<pre class="code">
</pre>

-->

### Kfpopeye Open Source, AVF and Other Cleanup

Lots of clean-up operations:
open source projects that help clean up a Revit model, certain parameter values and other operations, an important AVF cleanup required to prevent crashing, and some youngsters cleaning up some cash:

- [Kfpopeye open source projects](#2)
- [AVF result clean-up before design option switch](#3)
    - [Problem description](#3.1)
    - [Workaround](#3.2)
- [Young teen and kid sister crypto entrepreneurs](#4)

####<a name="2"></a> Kfpopeye Open Source Projects

[Kfpemail-2](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/11350013) kindly announced a bunch of new open Revit add-in projects in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Project Sweeper, ReVVed, and other apps now open source](https://forums.autodesk.com/t5/revit-api-forum/project-sweeper-revved-and-other-apps-now-open-source/m-p/10617548):

A while ago I decided to shut down [pkh Lineworks](http://www.pkhlineworks.ca) and
discontinue work on my apps Project Sweeper, MLTE, ReVVed, Paraline and Knock Knock.
I have now decided to make them open source, so anyone can download the code and continue to update them for future versions of Revit.

The repositories can be found in
the [Kfpopeye](https://github.com/kfpopeye)
[repositories](https://github.com/kfpopeye?tab=repositories) on GitHub:

- MLTE
- Knock-Knock
- Paraline
- Project-Sweeper
- ReVVed

I just added a readme file with a bit more information for them, plus a zip file `html_help_files.zip` containing the help documentation in html format providing detailed descriptions of what each command does:

- MLTE &ndash; M.L.T.E. (pronounced "multi") is program extension that is used inside Autodesk® Revit. It is a multiline text editor that can be used for editing the parameters of anything inside Revit but in a multi-line view instead of the single line that Revit's Properties Palette gives you.
In addition to editing the values of parameters M.L.T.E. can also edit the formulas when used inside the Revit Family Editor.
It even has syntax highlighting and auto-formatting to make complex nested if statements easier to follow and debug.
- Knock-Knock &ndash; a program extension for editing the values of door instance parameters, not for changing the way your door schedule is set up.
The primary way Knock Knock does this is by making text parameters act like `Yes`/`No` parameters.
With a simple click, users can change parameters values between multiple pre-defined values.
There are more features as well.
- Paraline &ndash; a Revit program extension that allows you to convert the detail elements from standard orthographic drawings like plans and elevations into 3D isometric drawings.
- Project-Sweeper &ndash; a collection of tools that allow a user to quickly and accurately remove the following clutter from Revit projects: line styles line patterns text styles fill region types and fill patterns.
Except for text styles, these items are not checked by Revit's *Purge Unused* command.
Project Sweeper goes beyond just checking for unused styles and patterns.
It also allows a user to convert from one style or pattern to another, delete all the elements using a certain style or pattern and preview all the views or elements using a style or pattern before removing them.
- ReVVed &ndash; an extension of commands for use within Revit.

Many thanks to [pkh Lineworks](http://www.pkhlineworks.ca) and [Kfpopeye](https://github.com/kfpopeye) for sharing this work!

####<a name="3"></a> AVF Result Clean-Up before Design Option Switch

Zhu Liyi raises a serious issue highlighting the urgent need to clean up AVF results before a design option switch in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to search all AVF analysis result and remove them](https://forums.autodesk.com/t5/revit-api-forum/how-to-search-all-avf-analysis-result-and-remove-them/td-p/10437422),
prompting a development ticket *REVIT-182024 &ndash; `SpatialFieldManager` within design option duplicated crashes* to improve this behaviour in future.

Happily, a simple workaround is perfectly feasible.

####<a name="3.1"></a> Problem Description 

SpatialFieldManager within design option duplicated crashes.

The `SpatialFieldManager` class is an AVF object that exists only in RAM, not in the model database.

It will cause Revit to crash if the result is created inside a design option and that design option is duplicated.

I would like to detect any result that's inside a design option and warn the user, but can't find a way to search for them.

It would be nice to fix the crash bug, or disallow analysis result to be placed inside design option altogether.

I cannot submit a sample model, since the object does not exist in model, only in RAM.

The way to reproduce this is:

- Create design option set and some design options
- Get inside a design option (make it active)
- Use some tool to create AVF object. The API sample add-in should do.
- Exit to main model, duplicate the design option that contains AVF object.
- Revit will crash.

####<a name="3.2"></a> Workaround

Alexander [@aignatovich](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478) [@CADBIMDeveloper](https://github.com/CADBIMDeveloper) Ignatovich, aka Александр Игнатович, suggested a fix:

**Answer:** I have not faced crashes myself, because I haven't used it with design options yet.
But I'll try to suggest a workaround:

- Collect open views via the `UIDocument` `GetOpenUIViews` method
- For each opened view, try to retrieve the spatial field manager via the `SpatialFieldManager` `GetSpatialFieldManager` method; if it returns non-null, the spatial field manager exists
- Call `SpatialFieldManager.Clear` to remove AVF

**Response:** This is the solution! Thanks.

I did a complete AVF clearing of all views in document.

Here is the code:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;views&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FilteredElementCollector(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;View&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;View&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;var&nbsp;view&nbsp;<span style="color:blue;">in</span>&nbsp;views&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;sfm&nbsp;=&nbsp;SpatialFieldManager.GetSpatialFieldManager(&nbsp;view&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;sfm&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">continue</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sfm.Clear();
&nbsp;&nbsp;}
</pre>

Since there is no change to the model itself, no need to open a transaction.

**Answer:** I haven't tested your code, but I see some potential problems (they could or could not really occur).

The first is View itself, it could be a template, a schedule or other table views, it could be a view sheet or some "internal" views such as project browser. Not sure if GetSpatialManager would throw an exception in these cases now (remember, this behaviour could change in future Revit releases), but I would add a check, something like that:

<pre class="code">
  ...
  .Cast&lt;View&gt;()
  .Where(&nbsp;x&nbsp;=&gt;&nbsp;x.AllowsAnalysisDisplay()
</pre>

The second thing, are you sure you have to check all views from the model? Maybe it will be enough to check opened views only?

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;views&nbsp;=&nbsp;uidoc
&nbsp;&nbsp;&nbsp;&nbsp;.GetOpenUIViews()
&nbsp;&nbsp;&nbsp;&nbsp;.Select(&nbsp;x&nbsp;=&gt;&nbsp;doc.GetElement(&nbsp;x.ViewId&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;View&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x&nbsp;=&gt;&nbsp;x.AllowsAnalysisDisplay()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
</pre>

**Response:** Thanks for the check.

Yes; I would have added `AllowAnalysisDisplay` too, if I had known it exists : P

I tried closing the view, then re-opening it; the AVF object is still there.
So, I need to do a document-wide search, not just opened views.

Many thanks to Zhu Liyi for raising this and to Alexander for the good solution!

####<a name="4"></a> Young Teen and Kid Sister Crypto Entrepreneurs

A sweet and impressive story about how much is possible nowadays, given appropriate support and motivation:

[14- and 9-year-old siblings earn over $30,000 a month mining cryptocurrency](https://www.cnbc.com/2021/08/31/kid-siblings-earn-thousands-per-month-mining-crypto-like-bitcoin-eth.html)

<center>
<img src="img/crypto_entrepreneurs.jpg" alt="Crypto entrepreneurs" title="Crypto entrepreneurs" width="400"/> <!-- 630 -->
</center>

