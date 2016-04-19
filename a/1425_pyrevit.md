<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- http://forums.autodesk.com/t5/revit-api/revitlookup-for-revit-2016-is-here/m-p/6258933
  https://github.com/eirannejad/pyRevit
  https://github.com/eirannejad/pyRevit/issues/70

The pyRevit IronPython Script Library #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim #python #dynamobim

jeremy tammik ‏@jeremytammik  27 May 2015
RevitLookup in Python Shell and Multi-Release Solution #revitapi #adsk #python #revitpythonshell #revitlookup http://bit.ly/1PMDUgF

Two weeks ago, Maltezc raised a question on the availability of a version of RevitLookup for Python.
I am not aware of any Python version of RevitLookup, but you can certainly call into RevitLookup from RevitPythonShell.
Maltezc pointed out the pyRevit IronPython script library.
Ehsan Iran-Nejad, pyRevit creator and maintainer, now wrote a blog post describing this powerful and popular collection...

-->

### The pyRevit IronPython Script Library

Two weeks ago, Maltezc raised a question on the availability of a version
of [RevitLookup](https://github.com/jeremytammik/RevitLookup) for Python in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [RevitLookup for Revit 2016](http://forums.autodesk.com/t5/revit-api/revitlookup-for-revit-2016-is-here/m-p/6258933).

I am not aware of any Python version of RevitLookup, but you can
certainly [call into RevitLookup from RevitPythonShell](http://thebuildingcoder.typepad.com/blog/2015/05/copyelements-revit-2016-scalability-python-and-ruby-shells.html#4).

In his question, Maltezc pointed out
the [pyRevit IronPython script library](https://github.com/eirannejad/pyRevit) that
I was previously unaware of.

A [chat](https://github.com/eirannejad/pyRevit/issues/70)
with [Ehsan Iran-Nejad](https://github.com/eirannejad), pyRevit creator and maintainer, led to him writing
a [blog post](https://github.com/eirannejad/pyRevit/blob/gh-pages/pyRevitBlogPost.md) describing
this powerful and popular collection, reproduced below in full after a couple
[of](https://github.com/eirannejad/pyRevit/pull/73)
[minor](https://github.com/eirannejad/pyRevit/pull/74)
[updates](https://github.com/eirannejad/pyRevit/pull/75).

I hope you find this as interesting and useful as I do.

Here is Ehsan's introduction to the package:

- [Introduction to pyRevit](#2)
- [Quick Look at some pyRevit Scripts](#3)
- [An Even Quicker but Deeper Look at Setting up pyRevit](#4)
- [pyRevit overview, installation and tutorial video](#5)


#### <a name="2"></a>Introduction to pyRevit

**Question:** I'm an architect and engineer and love coding.
Unfortunately, I don't have the time and experience to code in complex languages that require special coding environments (e.g. Visual Studio) or need to be compiled and reloaded after each change.
I therefore like scripts.
I can create or modify them for a highly specific task, in the least amount of time, and get the job done.
I want to learn how to use IronPython for Revit and I need examples.
Do you know a good resource for that?

**Response:** Yes, definitely!

Take a look at [pyRevit](https://github.com/eirannejad/pyRevit).

***pyRevit*** is an IronPython script library for Revit.
However, it is not really written as an example library.
It is a working set of tools fully written in IronPython that explores the power of scripting for Revit and also adds some cool functionality.

Download and install it, launch Revit and you will note the new ***pyRevit*** tab that hosts buttons to launch all the scripts provided by the package to easily run them without the need to load them in [RevitPythonShell](https://github.com/architecture-building-systems/revitpythonshell) or some similar IronPython console.

You can also write your own scripts and add them to the tab.

There is even a Reload Scripts button than dynamically adds the new scripts to the current Revit session without the need to restart Revit.

All the scripts are provided in the `pyRevit` folder which is downloaded at installation.
You can look into them and learn how to use IronPython for Revit to perform different tasks.

Please refer to the [pyRevit](https://github.com/eirannejad/pyRevit) GitHub repository for links and instructions on how to install on your machine.


#### <a name="3"></a>Quick Look at some pyRevit Scripts

Let's take a quick look at some of the more useful scripts in this library:

#### <a name="3.1"></a>Selection Memory

A couple of scripts help you select object more efficiently in Revit. They are similar to the M+, M-, buttons on calculators where you can add or deduct values from memory and read the final value using the MR button.

Under the ***pyRevit*** tab, you'll find MAppend, MAppendOverwrite, MRead, MDeduct, and MClear buttons that add, add and overwrite, read, deduct, and clear the contents of the selection memory. Using these tools, you can navigate between multiple views and select objects, add them to the memory and when you're done, recall the selection. These tools work really well in combination with other selection tools under ***pyRevit*** tab. See images here for the tools and tooltips.

Each tooltip shows the button name, the script that the button is associated with and a description of what the script does.

<center>
<img src="img/pyrevit_mappendoverwrite.png" alt="Memory append overwrite" width="471">
</center>

Memory read:

<center>
<img src="img/pyrevit_mread.png" alt="Memory read" width="407">
</center>

Other selection memory utilities:

<center>
<img src="img/pyrevit_othermemory.png" alt="Other selection memory utilities" width="211">
</center>

#### <a name="3.2"></a>Copy and Convert Legend Views

This set of scripts help you copy Legend Views to all other project open within a Revit session.
You can copy the Legends as Legend views or as Drafting views.

<center>
<img src="img/pyrevit_copylegends.png" alt="Copy legends" width="480">
</center>

Two more scripts duplicate and convert Legend views to Drafting views and vice versa within the same project.

<center>
<img src="img/pyrevit_convertlegends.png" alt="Convert legends" width="480">
</center>

#### <a name="3.3"></a>Matching Element Graphic Overrides

This one is pretty obvious. Run the script, select your source object to pick up the style, and then one by one, select the destination objects to apply the graphic overrides. You can also navigate to other views and apply to objects within that view.

<center>
<img src="img/pyrevit_matchgraphicoverrides.png" alt="Match graphic overrides" width="416">
</center>

***pyRevit*** provides many other powerful scripts, and most of them are really useful in a production environment.

<center>
<img src="img/pyrevit_analysisandmodifypallete.png" alt="Analyse and modify pallete" width="701">
</center>

Project palette:

<center>
<img src="img/pyrevit_projectpallete.png" alt="Project palette" width="222">
</center>

Desktop palette:

<center>
<img src="img/pyrevit_desktoppallete.png" alt="Desktop pallette" width="176">
</center>


#### <a name="4"></a>An Even Quicker but Deeper Look at Setting up pyRevit

Now let's take an even quicker and slightly deeper look at setting up [pyRevit](https://github.com/eirannejad/pyRevit):

In it's simplest form, it's a folder filled with `.py` IronPython scripts for Revit.

<center>
<img src="img/pyrevit_pyrevitfolder.png" alt="pyRevit folder" width="362">
</center>

Since Revit itself does not provide an IronPython console, you
need [RevitPythonShell](https://github.com/architecture-building-systems/revitpythonshell) to
run them.

<center>
<img src="img/pyrevit_revitpythonshellconsole.png" alt="RevitPythonShell console" width="965">
</center>

Let's say you have written a script that automatically designs amazing buildings and creates the Revit model and construction documents for it, and let's say you want to run this script as fast as you can and make a whole buncha money really quickly, but it takes time to open the command prompt every time, browse to the script file, open it and run it, so you naturally want something faster!

In order to make [pyRevit](https://github.com/eirannejad/pyRevit) more user friendly, it includes a helper script that finds all the other scripts and creates buttons for them in the Revit user interface.
This way. you can just click on the buttons instead of using the command prompt.

This script is appropriately called `__init__.py` and lives in
the [pyRevit](https://github.com/eirannejad/pyRevit) library root folder.

<center>
<img src="img/pyrevit_initscript.png" alt="pyRevit _init_ script" width="379">
</center>

What's neat about this is that the user interface buttons only store the address to each script.
The script is reloaded and run every time the user clicks on the button.

This means that you can change a script on the fly while Revit is running, and the next time you click on the button, Revit will run the modified script.

But how do you tell Revit to run this script during start-up?

There are two ways to achieve this:

- The easy way:
[RevitPythonShell](https://github.com/architecture-building-systems/revitpythonshell) has
an option under `Configuration` to run an IronPython script at Revit start-up. Just download
the [pyRevit](https://github.com/eirannejad/pyRevit) repository,
set the ***RevitPythonShell*** start-up script address to the file address of the `__init__.py` script, and restart Revit.
Voila, the ***pyRevit*** tab appears.

- The even easier way:
Download the setup package from
the [pyRevit](https://github.com/eirannejad/pyRevit) GitHub repository and install.
Done! Launch your Revit and ***pyRevit*** will be there.

If you'd like to find out more about ***pyRevit*** and how to add your own scripts, visit the [pyRevit GitHub home page](https://github.com/eirannejad/pyRevit) and everything you want to know about it is provided.

Happy scripting!

Many thanks to Ehsan for creating, sharing, maintaining, and documenting pyRevit with and for us all!


#### <a name="5"></a>pyRevit Overview, Installation and Tutorial Video

Neil Reilly created a [26-minute pyRevit overview, installation and tutorial video](https://www.youtube.com/watch?v=71rvCspWNHs) on it:

<center>
<iframe width="420" height="315" src="https://www.youtube.com/embed/71rvCspWNHs?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

Many thanks to you too, Neil!