<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script> -->
<!-- https://highlightjs.org/#usage -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
</head>

<!---

- AU 2023 classes -- https://www.autodesk.com/autodesk-university/search?fields.year=2023

- Dependency Injection for Revit API
  https://forums.autodesk.com/t5/revit-api-forum/dependency-injection-for-revit-api/td-p/12467760

- model checker api:
  https://forums.autodesk.com/t5/revit-api-forum/setting-up-iprebuiltoptionsservice-options-for-checkset-in-ait/td-p/12455815

- Maestro AI for Revit scripting
  https://maestro.bltsmrt.com/
  https://www.linkedin.com/posts/christopher-wiesen-b9693b67_maestroforrevit-aectech-automation-activity-7143312975838232576-gLEv?utm_source=share&utm_medium=member_desktop
  Christopher Wiesen, President, BLT SMRT LLC, Las Vegas, Nevada, United States

- Total Construction Spending: Manufacturing in the United States (TLMFGCONS)
  https://fred.stlouisfed.org/series/TLMFGCONS#0

- most interesting philosophical discussion i have seen for a long tim:
  Dennett vs Sapolsky on free will: A clash over different claims?
  https://philosophy.stackexchange.com/questions/106926/dennett-vs-sapolsky-on-free-will-a-clash-over-different-claims?utm_source=iterable&utm_medium=email&utm_campaign=the-overflow-newsletter
  comparing a boulder crashing down a mountain and a skier who skis down the mountain

twitter:

AU 2023 classes, dependency injection for #RevitAPI, RevitLookup updates, model checker API docs, AI for Revit scripting in @AutodeskRevit @AutodeskAPS #BIM @DynamoBIM https://autode.sk/dependencyinjection

Happy New Year!
&ndash; AU 2023 classes
&ndash; Dependency injection for Revit API
&ndash; RevitLookup updates
&ndash; Model checker API docs
&ndash; ChatGPT and Maestro AI for Revit scripting
&ndash; Construction spending rising in the US
&ndash; Free Will
&ndash; Vuca...

linkedin:

Happy New Year!

AU 2023 classes, dependency injection for #RevitAPI, RevitLookup updates, model checker API docs, AI for Revit scripting:

https://autode.sk/dependencyinjection

- AU 2023 classes
- Dependency injection for Revit API
- RevitLookup updates
- Model checker API docs
- ChatGPT and Maestro AI for Revit scripting
- Construction spending rising in the US
- Free Will
- Vuca...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Dependency Injection and Model Checker API

Happy New Year!

Let's begin it gently with the following notes on topics that caught my eye and interest:

- [AU 2023 classes](#2)
- [Dependency injection for Revit API](#3)
- [RevitLookup updates](#4)
- [Model checker API docs](#5)
- [ChatGPT and Maestro AI for Revit scripting](#6)
- [Construction spending rising in the US](#7)
- [Free Will](#8)
- [Vuca](#9)

####<a name="2"></a> AU 2023 Classes

Did you miss an interesting class at AU?
Check out the entire collection
of [Autodesk University 2023 classes online](https://www.autodesk.com/autodesk-university/search?fields.year=2023).

####<a name="3"></a> Dependency Injection for Revit API

Between Christmas and New Year,
Luiz Henrique [@ricaun](https://ricaun.com/) Cassettari implemented, documented and shared a complete solution
for [dependency injection for Revit API](https://forums.autodesk.com/t5/revit-api-forum/dependency-injection-for-revit-api/td-p/12467760),
saying:

> I created a library to help create a container for Dependency Injection, designed to work with Revit API.
It is open-source and has a package in the Nuget:

> - [github.com/ricaun-io/ricaun.Revit.DI](https://github.com/ricaun-io/ricaun.Revit.DI)
- [www.nuget.org/packages/ricaun.Revit.DI](https://www.nuget.org/packages/ricaun.Revit.DI)

> I created this [22-minute video](https://youtu.be/Q_greabHlUQ) on how to add the package and a simple example with an `ICommand` implementation:

> - [github.com/ricaun-io/RevitAddin.DI.Example](https://github.com/ricaun-io/RevitAddin.DI.Example)

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/Q_greabHlUQ?si=7pyYCcqMuyy3XL-J" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
</center>

> That's it for the year 2023; Happy New Year with best regards!

Happy New Year to you too, <i>ricaun</i>, and to the entire community from me as well!

####<a name="4"></a> RevitLookup Updates

Before the DI project, ricaun also contributed to RevitLookup, working with
Roman [Nice3point](https://github.com/Nice3point), principal maintainer, helping to produce:

- [RevitLookup release 2024.0.11](https://github.com/jeremytammik/RevitLookup/releases/tag/2024.0.11)
- [RevitLookup release 2024.0.12](https://github.com/jeremytammik/RevitLookup/releases/tag/2024.0.12)

RevitLookup 2024.0.11 welcomes you with improved visuals, support for templates to fine-tune data display, improved navigation, in-depth color support:

- Navigation. Updated navigation allows `Ctrl + Click` in the tree or grid to open any selected item or group of items in a new tab.
This also allows you to analyze items that RevitLookup doesn't support, e.g., looking at StackTrace for exceptions.
- Color Preview. Changes to the user interface give us the ability to customize the display of any type of data.
Now you will be able to visually see how materials or ribbon look.
`Autodesk.Revit.DB.Color` and `System.Windows.Media.Color` are supported.
- Update available notification**. Updates are now checked automatically and an icon is displayed in the navigation area if a new version is available.
- Background effects. Available on windows 11 only: Acrylic, Blur; the visual representation of the background depends on your desktop image and current theme.
- Color extensions. Convert color to other formats HEX, CMYK, etc. Color name identification, `en` and `ru` localizations available. `Autodesk.Revit.DB.Color` and `System.Windows.Media.Color` are supported.
- Fixed incorrect display when switching themes on Windows 10.
- Returned deleted notification when checking for updates.
- Updated developer's [guide](https://github.com/jeremytammik/RevitLookup/blob/dev/Contributing.md#styles).
- Full changelog: [2024.0.10...2024.0.11](https://github.com/jeremytammik/RevitLookup/compare/2024.0.10...2024.0.11)

Here, I'm wrapping things up. Wishing everyone a splendid New Year and a joyous Christmas ahead. As always, yours truly &ndash; @Nice3point

RevitLookup 2024.0.12 is the last corrective update for 2023, bringing minor tweaks and improvements:

- Add theme update for all open RevitLookup instances by @ricaun in [#200](https://github.com/jeremytammik/RevitLookup/pull/200)
- Fix incorrect Hue calculation for some colour formats
- Disable all background effects for Windows 10. Thanks @ricaun for help and testing [#194](https://github.com/jeremytammik/RevitLookup/issues/194)
- Full changelog: [2024.0.11...2024.0.12](https://github.com/jeremytammik/RevitLookup/compare/2024.0.11...2024.0.12)

That's all for now.
Again, wishing you all a Happy New Year with best regards, do what you love, evolve, travel, don't forget to have a rest and keep coding! &ndash; @ricaun

####<a name="5"></a> Model Checker API Docs

<i>Shrey_shahE5SN4</i> very kindly points out
the [Model Checker API documentation](https://help.autodesk.com/view/AIT4RVT/ENU/?guid=InteroperabilityToolsForRevit_040mcxr_0404mcxr_html) in
his question
on [setting up `IPreBuiltOptionsService` options for CheckSet in AIT](https://forums.autodesk.com/t5/revit-api-forum/setting-up-iprebuiltoptionsservice-options-for-checkset-in-ait/td-p/12455815):

> I am... building an add-in button.
When clicked, it will execute the Model Checker from Autodesk Interoperability Tools.
Following the provided guidelines, I am progressing through the necessary steps:

> [Model Checker API](https://help.autodesk.com/view/AIT4RVT/ENU/?guid=InteroperabilityToolsForRevit_040mcxr_0404mcxr_html)

Thank you for that hint, Shrey_shahE5SN4.

####<a name="6"></a> ChatGPT and Maestro AI for Revit Scripting

AI programming assistants are boosting developer effectivity in many areas.
Here is one dedicated to Revit customisation:
[Maestro AI for Revit scripting](https://maestro.bltsmrt.com/).
Looking forward to hearing how it shapes out.

Eric Boehlke of [Truevis](https://truevis.com) has also been working to focus LLMs to work better with programming Revit and shared some results:

> My latest attempt:

> - [OpenAI bim-coding-coach](https://chat.openai.com/g/g-7gcy5wueV-bim-coding-coach)
- [BIM-Coding-Coach GitHub repository](https://github.com/truevis/BIM-Coding-Coach)

> I haven't tested it with C&#35; yet, but it is working well with Python and DesignScript.

####<a name="7"></a> Construction Spending Rising in the US

Good news for the AEC industry: an impressive positive jump
in [total construction spending: manufacturing in the United States (TLMFGCONS)](https://fred.stlouisfed.org/series/TLMFGCONS#0):

<center>
<img src="img/total_construction_us.png" alt="Total construction spending" title="Total construction spending" width="1200"/> <!-- Pixel Height: 848 Pixel Width: 2,598 -->
</center>

####<a name="8"></a> Free Will

As a scientifically and technically minded person, I often find philosophical pondering rather vague.
I was therefore pleased to read the interesting and precise analytical philosophical discussion
on [Dennett vs Sapolsky on free will: a clash over different claims?](https://philosophy.stackexchange.com/questions/106926/dennett-vs-sapolsky-on-free-will-a-clash-over-different-claims),
comparing the volition and predetermination of a boulder crashing down a mountain and a skier who skis down the mountain, including the possible influence of quantum mechanical effects.

####<a name="9"></a> Vuca

Have you ever heard the term "vuca"?
I had not.
Apparently, it stands for volatility, uncertainty, complexity and ambiguity.
Facing us in the recent past, and possibly in coming years as well.
Which leads to the dread of a long-term state, a “permavucalution”.
Oh dear.
Let's hope that our humanity and free will can help handle it.

