<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 12658352 [Visual Studio 2015 templates for Revit add-ins]
  http://forums.autodesk.com/t5/revit-api-forum/visual-studio-2015-templates-for-revit-add-ins/m-p/6866605

- 12661081 [`Use managed compatibility mode` or `Enable native code debugging`?]
  http://forums.autodesk.com/t5/revit-api-forum/use-managed-compatibility-mode-or-enable-native-code-debugging/m-p/6868592

- https://autodesk.taleo.net/careersection/adsk_gen/jobdetail.ftl?job=17WD22765
  Software Engineer, Machine Learning

- https://www.freecodecamp.com/email-signup jeremy@tammik.ca ,2gether
  https://www.freecodecamp.com/challenges/learn-how-free-code-camp-works

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

I have been maintaining a simple Visual Studio Revit add-in wizard for a number of years. Now Andrey Bushman implemented a set of more advanced and convenient Visual Studio templates for creation of Revit add-ins for his own use. The discussion of those led us to once more revisit the topic of the Visual Studio settings for debugging Revit add-ins
&ndash; New Visual Studio 2015 templates for Revit add-ins
&ndash; Use Managed Compatibility Mode or Enable Native Code Debugging?
&ndash; Machine learning software engineer job in Switzerland
&ndash; FreeCodeCamp...

-->

### New Visual Studio Templates for Revit Add-Ins

I have been maintaining a
simple [Visual Studio Revit add-in wizard](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20) for
a number of years, hosted in
the [VisualStudioRevitAddinWizard GitHub repo](https://github.com/jeremytammik/VisualStudioRevitAddinWizard).

Now Andrey Bushman implemented a set of more advanced and convenient Visual Studio templates for creation of  Revit add-ins for his own use.

The discussion of those led us to once more revisit the topic of the Visual Studio settings for debugging Revit add-ins:

- [New Visual Studio 2015 templates for Revit add-ins](#2)
    - [Feature comparison](#3)
    - [Q &amp; A](#4)
- [Use Managed Compatibility Mode or Enable Native Code Debugging?](#5)
- [Machine learning software engineer job in Switzerland](#6)
- [FreeCodeCamp](#7)



#### <a name="2"></a>Visual Studio 2015 Templates for Revit Add-Ins

Andrey Bushman shared a new and more advanced set of convenient Visual Studio templates for creation of Revit add-ins in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Visual Studio 2015 Templates for Revit Add-Ins](http://forums.autodesk.com/t5/revit-api-forum/visual-studio-2015-templates-for-revit-add-ins/m-p/6866605):

In Andrey's own words:

I need a set of convenient Visual Studio templates for creation of  Revit add-ins.
I am a very lazy person and I prefer that the boring operations were done by a template instead of me.

At first I wanted to improve
the [VisualStudioRevitAddinWizard](https://github.com/jeremytammik/VisualStudioRevitAddinWizard) template by Jeremy Tammik.
But Jeremy ignored [my pull request](https://github.com/jeremytammik/VisualStudioRevitAddinWizard/pull/3).
Therefore, I refused the subsequent attempts to improve his template.
Therefore, I made templates myself.

Here are my [Visual Studio 2015 templates for Revit add-in creation](https://github.com/Andrey-Bushman/Revit2017AddInTemplateSet).

This repo also contains a set of video lessons with English comments:

1. [Download and install the templates](https://www.youtube.com/watch?v=SYm-yxQ9jFk&t=1s)
2. [Create new project](https://www.youtube.com/watch?v=TU5HoTxpgbk&t=5s)
3. [Add new command](https://www.youtube.com/watch?v=mtw8PAf5eus&t=2s)
4. [Link the command with the command availability](https://www.youtube.com/watch?v=-_79p0CnKJY&t=8s)
5. [Add ribbon tabs and panels](https://www.youtube.com/watch?v=wlskC5PTmH8&t=4s)
6. [Template resources using](https://www.youtube.com/watch?v=_aQ30GHl3as&t=1s)
7. [Multilanguage add-ins creating](https://www.youtube.com/watch?v=abxy-Ynff3w)

Also, I wrote an article [Revit Visual Studio](https://revit-addins.blogspot.ru/2017/02/revit-visual-studio.html) about it in my blog (Russian text).

Perhaps these templates will be useful for other programmers too.

#### <a name="3"></a>Feature Comparison

Andrey provided the following table comparing the feature list of the old and simple VisualStudioRevitAddinWizard `W` versus the new and feature-packed Revit2017AddInTemplateSet `T`:

1. Templates for C#.
2. Templates for VB.NET.
3. Use NuGet-packages for Revit assemblies.
4. Visual Studio Project Templates for external application.
5. Visual Studio Project Templates for external DB-level application.
6. Visual Studio Item Templates for external command.
7. Visual Studio Item Templates for external command availability.
8. Visual Studio Item Templates for Updater.
9. Require writing of additional code for the registration of commands.
10. Automatically create the ribbon tabs, panels, and buttons for your commands. Also, allows to you to manage by this behaviour.
11. Automatically create the binding for your commands and some default topic for the stub help file.
12. Allow creation of multilanguage add-ins.
13. Create a subfolder for each add-in in the Revit Add-Ins folder (for the DEBUG configuration).
14. Uses PVS-Studio static code analyzer.

<center>
<table>
 <tr>
 <td style="text-align: right">#&nbsp;&nbsp;&nbsp;</td>
 <td><code>W</code></td>
 <td><code>T</code></td>
 </tr>
 <tr>
 <td style="text-align: right">1&nbsp;&nbsp;&nbsp;</td>
 <td>+</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">2&nbsp;&nbsp;&nbsp;</td>
 <td>+</td>
 <td>-</td>
 </tr>
 <tr>
 <td style="text-align: right">3&nbsp;&nbsp;&nbsp;</td>
 <td>-</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">4&nbsp;&nbsp;&nbsp;</td>
 <td>+</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">5&nbsp;&nbsp;&nbsp;</td>
 <td>-</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">6&nbsp;&nbsp;&nbsp;</td>
 <td>-</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">7&nbsp;&nbsp;&nbsp;</td>
 <td>-</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">8&nbsp;&nbsp;&nbsp;</td>
 <td>-</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">9&nbsp;&nbsp;&nbsp;</td>
 <td>+</td>
 <td>-</td>
 </tr>
 <tr>
 <td style="text-align: right">10&nbsp;&nbsp;&nbsp;</td>
 <td>-</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">11&nbsp;&nbsp;&nbsp;</td>
 <td>-</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">12&nbsp;&nbsp;&nbsp;</td>
 <td>-</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">13&nbsp;&nbsp;&nbsp;</td>
 <td>-</td>
 <td>+</td>
 </tr>
 <tr>
 <td style="text-align: right">14&nbsp;&nbsp;&nbsp;</td>
 <td>-</td>
 <td>+</td>
 </tr>
</table>
</center>

#### <a name="4"></a>Q &amp; A

<b>[Q]</b> Is the new `Revit2017AddInTemplateSet` compatible with the previous `VisualStudioRevitAddinWizard`?

<b>[A]</b> Revit2017AddInTemplateSet is not based on the VisualStudioRevitAddinWizard. Revit2017AddInTemplateSet has other architecture and ideas.
Therefore, Revit2017AddInTemplateSet cannot automatically be merged with VisualStudioRevitAddinWizard.

<b>[Q]</b> What about a version for Visual Basic?

<b>[A]</b> I don't use VB.NET. I created the templates for the programming language which I use. If someone creates variant for VB and sends this patch to me, then I won't mind and I will accept this patch provided that it is made qualitatively.

<b>[Q]</b> If possible, I imagine it would be useful for the entire Revit API developer community if we can manage to maintain a single optimal set of Wizards that satisfies all needs.
What do you think? Should we aim at maintaining just one version together, or keep the two separate?

<b>[A]</b> Jeremy, if you apply my pull request and replace the hard references of Revit API to their NuGet package then your template will be more convenient for using.

<!-- I described my wishes about VisualStudioRevitAddinWizard in my article which I pointed to in [my previous post](???). -->

All developers are different. Some of them prefer to write code manually completely. VisualStudioRevitAddinWizard template can be interesting for such people. Other developers prefer to concentrate completely on the solvable task, without being distracted by the minor things, such as UI creation. For such people, the Revit2017AddInTemplateSet templates can be interesting.

My template is more difficult than yours because it shall decide automatically much more tasks. But my template isn't so difficult that it was difficult to be understood. Having created the new project on the basis of my template it is possible to study the generated code and to understand how it works.

Therefore, perhaps it makes a sense that at the same time there were two different templates (VisualStudioRevitAddinWizard and Revit2017AddInTemplateSet) that allow to people to select that which more suits them.

In my opinion your template requires improving. If it is interesting for you then I am ready to discuss it. At this case I recommend to you to read those notes which I listed in the article of my blog.

<b>[Q]</b> In that case, we should also compile a list of differences to explain to people why they might want to choose one above the other.

No problem. I can do it if it will be necessary.

P.S.

In my opinion VisualStudioRevitAddinWizard is not a "wizard". I expected that wizard opens some dialog window and allows to user to point some predefined settings on the base of which wizard will generate a new project. Am I right? Therefore, my projects haven't "Wizard" word inside of their names.

On a slightly different topic that lead to the following subsequent discussion, 
I turned off the `Enable native code debugging` option in the project templates.


#### <a name="5"></a>Use Managed Compatibility Mode or Enable Native Code Debugging?

Andrey raised another topic in the thread 
on [using `Managed compatibility mode` or `Enable native code debugging`](http://forums.autodesk.com/t5/revit-api-forum/use-managed-compatibility-mode-or-enable-native-code-debugging/m-p/6868848):

**Question:** I write .NET add-ins for Autodesk Revit using Visual Studio 2015. Revit is an unmanaged application. So, for successfully debugging I have to turn ON either the `Use managed compatibility mode` or `Enable native code debugging` option (or for both). Otherwise, debugging cannot be launched.

The first of them is applied for all projects. The second of them is used for each project individually.


<center>
<img src="img/0_vs_debugging_options.png" alt="Debugging options" width="917"/>
</center>

What is the difference between these options? I don't understand what they do. What option it is more correct to use in my case?

**Answer 1:** Hans Passant answered. I underlined what may be the reason why I am to use this option for Revit add-ins debugging:
 
You do not have to enable unmanaged debugging to debug your plugin. Breakpoints in your code will activate (turn from hollow to solid) when the host application loads your add-in. If you are not sure if this happened then have a look at the Debug &gt; Windows &gt; Modules window.
 
Enabling unmanaged debugging does not otherwise greatly affect the debugging session, it can however take quite a bit longer to get started and you may need to temporarily disable the symbol server to avoid getting annoyed at it.
 
The Tools &gt; Options settings have rather poor names. Microsoft has been working on new debugging engines but was forced (or chose) to drop some features. "Use Managed Compatibility Mode" forces an older version of the managed debugger to be loaded, the one that was used in VS2010. It is required when you debug C++/CLI code. <u>It can be also useful in VS2015, its managed debugging engine is very buggy.</u> You'll miss out on some new debugging features like return value inspection and 64-bit edit+continue. You don't otherwise need it to debug your add-in.
 
Much the same story for "Use Native Compatibility Mode", it enables an older version of the unmanaged debugging engine, the one in VS2012 afaik. You'll miss out on the new Natvis visualizers. I have not yet found a compelling reason to need it, other than keep the old visualizers working.

**Answer 2 by Matt Taylor:** Here are my successful Revit 2017 / Visual Studio Pro 2013 (Update 5) settings, if anyone is interested:

[Project specific debug options](img/ProjectSpecificDebugOptions1.png):

<center>
<img src="img/ProjectSpecificDebugOptions1.png" alt="Project specific debug options" width="312"/>
</center>

[General debug options](img/GeneralDebugOptions.png):

<center>
<img src="img/GeneralDebugOptions.png" alt="General debug options" width="800"/> <!-- 4465 --> 
</center>


#### <a name="6"></a>Machine Learning Software Engineer Job in Switzerland

Autodesk is seeking a [Machine Learning Software Engineer for a post in Neuch&acirc;tel](https://autodesk.taleo.net/careersection/adsk_gen/jobdetail.ftl?job=17WD22765).

Check it out in case of interest.

#### <a name="7"></a>FreeCodeCamp

I took a quick look at [FreeCodeCamp](https://www.freecodecamp.com) and am very impressed.

A non-profit organisation that also does not accept donations of any kind, their goal is to efficiently educate new software engineers, and they have a track record of doing so with great success.

If you would like to learn more about programming and practice the skills that are of real importance today and for the future, this is probably one of the best choices you can make.


