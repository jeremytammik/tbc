<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- mention gibson agency
  william_gibson_agency.jpg

- performance issue retrieving MEP pipe insulation elements using GetDependentElements
  by Александр Игнатович <cadbimdeveloper@yandex.ru>
  I have a short note for TBC. Only a little part of the blogpost somewhere in the future.
  Recently I faced with a performance issue getting pipes insulation. My previous implementation looked like this:
  var pipeInsulation = pipe
    .GetDependentElements(new ElementClassFilter(typeof(PipeInsulation)))
    .Select(pipe.Document.GetElement)
    .Cast<PipeInsulation>()
    .FirstOrDefault();
  I didn't notice it before because I tested the code on a small model, however in the big model entire calculation tooked more than hour. Calculations were also huge, so I spent some time trying to figure out what is going wrong. Now the solution looks like this:
  var pipeInsulation = InsulationLiningBase
    .GetInsulationIds(pipe.Document, pipe.Id)
    .Select(pipe.Document.GetElement)
    .OfType<PipeInsulation>()
    .FirstOrDefault();
  Now the entire calculations take seconds instead of hours.
  This issue is related only to MEP; I haven't faced any other performance issues usng the `GetDependentElements` method.
  thank you very much for the interesting observation!

- programming languages to learn
  email [Computational Strategy - Question for the Expert] Andrea Rolle
  [Q] We are now starting our in-house computational strategy in order to automate processes on both Revit-Dynamo and Inventor-iLogic and I am struggling to decide which language code we should start to learn.
  C#, Python or F#? I am moving my first steps into coding but I have a few years of experience on Dynamo & Grasshopper. 
  I would like to add that our Inventor has got an automation interface developed by a consultant company written in F# which eventually we would like to take control over it in the long term.
  Long story short I would like to understand the pro and cons about  which language should we start to learn (between C#, Python and F#) to better unify a future workflow between Revit and Inventor, keeping in mind that we have something in house already developed in F#.
  [A] I would say:
  - Python: best fpr learning, and for Dynamo
  - C# best for pure Revit API, most example code, cleanest .NET interface
  - F# best for stateless procedural generic logical lambda computation, and you'll need it in the long run anyway

/Users/jta/a/doc/revit/tbc/git/a % tbcsh_search.py language
0163:Language Integrated Query Linq
0178:Language Independent Category Access
0188:A .NET Language Learning Resource
0512:Language Independent Subcategory Creation
0799:Running Language Code and More Exporters
0930:MUI: Multiple Language Interface Update
0946:Removing Extreneous Mac Architectures, Languages and Files
0948:All-zero Language Codes in the Revit Product GUID
0998:Language Independent Section View Type Id Retrieval
1078:Multi-language Shared Parameters
1347:The Most Popular Programming Languages 2015
1368:<"#15">Choose a Programming Language
1368:<"#16">Converting Code from One Language to Another
1523:Multiple Language RESX Resource Files
1523:<"#2">Supporting Multiple Language Resource Files
1792:<"#5"> Most Popular Programming Languages 1965-2019
1838:2021 Migration, Add-In Language, BIM360 Research
1838:<"#3"> What Language to choose for a Revit Add-In?
1880:<"#3"> Más Allá de Dynamo &ndash; Spanish-Language Book
1885:<"#4"> Internationalisation Using .NET Language Resources
1892:Model Group and DA4R Language in Forge
1892:<"#3"> Specifying the Revit UI Language in DA4R
1920:<"#3"> Local Language Forge Classes

/Users/jta/a/doc/revit/tbc/git/a % bl 0188 0799 0946 1347 1368 1792
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/07/a-net-language-learning-resource.html">A .NET Language Learning Resource</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/07/determine-running-language-code.html">Running Language Code and More Exporters</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/05/removing-extreneous-mac-architectures-and-languages.html">Removing Unused Mac Architectures, Languages and Files</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/08/revit-future-and-saving-user-configuration-settings.html">Revit Future and Saving User Configuration Settings</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html">RTC Classes and Getting Started with Revit Macros</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/10/invitation-to-devcon-visual-programming-in-infrastructure.html">DevCon Invitation and Dynamo for Infrastructure</a></li>
</ul>

/Users/jta/a/doc/revit/tbc/git/a % blmd 0188 0799 0946 1347 1368 1792
- [A .NET Language Learning Resource](http://thebuildingcoder.typepad.com/blog/2009/07/a-net-language-learning-resource.html) == - [Running Language Code and More Exporters](http://thebuildingcoder.typepad.com/blog/2012/07/determine-running-language-code.html) == - [Removing Unused Mac Architectures, Languages and Files](http://thebuildingcoder.typepad.com/blog/2013/05/removing-extreneous-mac-architectures-and-languages.html) == - [Revit Future and Saving User Configuration Settings](http://thebuildingcoder.typepad.com/blog/2015/08/revit-future-and-saving-user-configuration-settings.html) == - [RTC Classes and Getting Started with Revit Macros](http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html) == - [DevCon Invitation and Dynamo for Infrastructure](https://thebuildingcoder.typepad.com/blog/2019/10/invitation-to-devcon-visual-programming-in-infrastructure.html)

twitter:

add #thebuildingcoder

Critical and inspiring sci-fi, MEP pipe insulation retrieval performance and programming languages to learn for the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon https://autode.sk/pipeinsulation

Three quick notes on critical and inspiring sci-fi, programming languages and MEP filtering
&ndash; Pipe insulation retrieval performance
&ndash; Programming languages to learn
&ndash; Agency by William Gibson...

linkedin:

Critical and inspiring sci-fi, MEP pipe insulation retrieval performance and programming languages to learn for the #RevitAPI

https://autode.sk/pipeinsulation

- Pipe insulation retrieval performance
- Programming languages to learn
- Agency by William Gibson...

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

### Sci-Fi, Languages and Pipe Insulation Retrieval

Three quick notes from my recent email correspondence and reading:

- [Pipe insulation retrieval performance](#2)
- [Programming languages to learn](#3)
- [Agency by William Gibson](#4)

####<a name="2"></a> Pipe Insulation Retrieval Performance

Alexander [@aignatovich](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478) [@CADBIMDeveloper](https://github.com/CADBIMDeveloper) Ignatovich, aka Александр Игнатович,
shares an interesting observation on a performance issue retrieving MEP pipe insulation elements using `GetDependentElements`:

Recently, I faced with a performance issue getting pipe insulation.
My previous implementation looked like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;pipeInsulation&nbsp;=&nbsp;pipe
&nbsp;&nbsp;&nbsp;&nbsp;.GetDependentElements(&nbsp;<span style="color:blue;">new</span>&nbsp;ElementClassFilter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">typeof</span>(&nbsp;PipeInsulation&nbsp;)&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Select(&nbsp;pipe.Document.GetElement&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;PipeInsulation&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault();
</pre>

I didn't notice it before because I tested the code on a small model.

However, in the big model, the entire calculation took over an hour.
Calculations were also huge, so I spent some time trying to figure out what is going wrong.

The improved solution looks like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;pipeInsulation&nbsp;=&nbsp;InsulationLiningBase
&nbsp;&nbsp;&nbsp;&nbsp;.GetInsulationIds(&nbsp;pipe.Document,&nbsp;pipe.Id&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Select(&nbsp;pipe.Document.GetElement&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfType&lt;PipeInsulation&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault();
</pre>

With that, the entire calculations take seconds instead of hours.

This issue is related only to MEP; I haven't faced any other performance issues using the `GetDependentElements` method.

Thank you very much for the interesting observation, Alex!

####<a name="3"></a> Programming Languages to Learn

A frequent question is which programming language to learn to implement Revit add-ins.
Here it comes up again, with an F# twist:

**Question:** We are now starting our in-house computational strategy in order to automate processes on both Revit-Dynamo and Inventor-iLogic and I am struggling to decide which language code we should start to learn.
C#, Python or F#?
I am takin my first steps into coding, but I have a few years of experience on Dynamo &amp; Grasshopper. 
I would also like to add that our Inventor application has an automation interface developed by a consultant company written in F# that we would like to take control over in the long term.
Long story short: I would like to understand the pro and cons of which of these three languages should we start to learn to better unify a future workflow between Revit and Inventor, keeping in mind that we have something in-house already developed in F#.

**Answer:** Since the Revit API is completely .NET based and all .NET languages are completely interoperable, it really does not matter much at all which one you learn and use.
Any one of them can be used to interact fully with any other.

Furthermore, all .NET languages compile to the same underlying IL or intermediate language.
From IL, you can decompile back into any other .NET language, making it easy to switch back and forth between languages and even transform your code base from one to another.

Therefore, obviously, you need not really worry about learning F# at all, if you are not interested in procedural programming yourself.

In short, I would say:

- Python: best for learning, and for Dynamo
- C#: best for pure Revit API, most example code available, cleanest .NET interface
- F#: best for generic stateless procedural logical lambda computation, and you'll need it in the long run anyway

Here are some other thoughts on this topic:

- [What Language to choose for a Revit Add-In?](https://thebuildingcoder.typepad.com/blog/2020/04/2021-migration-add-in-language-and-bim360-login.html)
- [A .NET Language Learning Resource](http://thebuildingcoder.typepad.com/blog/2009/07/a-net-language-learning-resource.html)
- [Running Language Code and More Exporters](http://thebuildingcoder.typepad.com/blog/2012/07/determine-running-language-code.html)
- [Revit Future and Saving User Configuration Settings](http://thebuildingcoder.typepad.com/blog/2015/08/revit-future-and-saving-user-configuration-settings.html)
- [RTC Classes and Getting Started with Revit Macros](http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html)
    - [Choose a Programming Language](http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html#15)
    - [Converting Code from One Language to Another](http://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html#16)
- [Most Popular Programming Languages 1965-2019](https://thebuildingcoder.typepad.com/blog/2019/10/invitation-to-devcon-visual-programming-in-infrastructure.html#5)
- [The Most Popular Programming Languages 2015](http://thebuildingcoder.typepad.com/blog/2013/05/removing-extreneous-mac-architectures-and-languages.html#3)

####<a name="4"></a> Agency by William Gibson

I just finished reading *Agency* by William Gibson, a brilliant sci-fi including a critical look at politics and its rather helpless and fruitless attempts to control climate change, big business, the current pandemic, probably AI, soon, and other interesting challenges.
It includes an original new (for me, anyway) idea on time travel and its impossibility.
It treats the possibility of benevolent and humane AI with a lot of optimism, which I agree with.
Gibson is a true visionary.
He also coins (?) the term CCA, competitive control area, a territory where it is unclear who holds the power: government, warlords, multinational companies, criminal organisations...
one wonders whether that might be an accurate and critical way of viewing our current real world right now...

<center>
<img src="img/william_gibson_agency.jpg" alt="William Gibson Agency" title="William Gibson Agency" width="260"/> <!-- 1843 -->
</center>

####<a name="5"></a> Addendum &ndash; Jackpot

Wow! Good news!

I just discovered that Agency is part two of the [Jackpot trilogy](https://en.wikiquote.org/wiki/Jackpot_trilogy).

So, next thing I do is order part one, [The Peripheral](https://en.wikipedia.org/wiki/The_Peripheral).
