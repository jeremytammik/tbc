<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

BIM360 Question? Join a Forge accelerator! Choosing between C# and Python; The Building Coder samples 2021 migration in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/bim360cloudaccelerator

I am too busy! Ouch! Here are just a few of today's topics
&ndash; BIM360 Question? Join Accelerator!
&ndash; What language to choose for a Revit Add-In?
&ndash; The Building Coder samples 2021 migration...

linkedin:

BIM360 Question? Join a Forge accelerator! Choosing between C# and Python; The Building Coder samples 2021 migration in the #RevitAPI

https://bit.ly/bim360cloudaccelerator

I am too busy! Ouch! Here are just a few of today's topics:

- BIM360 Question? Join Accelerator!
- What language to choose for a Revit Add-In?
- The Building Coder samples 2021 migration...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### 2021 Migration, Add-In Language, BIM360 Research

I am too busy! Ouch! Here are just a few of today's topics:

- [BIM360 Question? Join Accelerator!](#2)
- [What language to choose for a Revit Add-In?](#3)
    - [Addendum &ndash; C&#35; versus Python](#3.1)
    - [Python and .NET](#3.2)
- [The Building Coder samples 2021 migration](#4)

####<a name="2"></a> BIM360 Question? Join Accelerator!

A quick question on logging in to BIM360 from Håvard Vasshaug and Dimitar Venkov of
the [Bad Monkeys](https://www.badmonkeys.net) (not to be confused with the [thriller of the same name](https://en.wikipedia.org/wiki/Bad_Monkeys) &ndash; featuring Jane, who claims that she works for a secret organization devoted to fighting evil and that she is the operative for the Department for the Final Disposition of Irredeemable Persons, also known as Bad Monkeys):

**Question:** We are using Revit Batch Processor to open multiple Revit models and run a selection of Python scripts on each in order to standardise their content and settings.

One client is asking if we can build it to support BIM360.

So, we wonder:

Can we use the secure sign-on that's already present in Revit (the one that gives users access through the Revit home screen), so that we can gain access to Forge, see the projects and files shared with that user and finally read the `project_Id` and `model_Id` of those files?

Do you know where we should be looking and who we should talk with?

**Answer:** That sounds cool, and the idea sounds good to me.

However, I have avoided involvement with security and credentials as much as possible, so I don't really know.

Your best bet for getting a reliable answer is to ask through
the [regular Forge help channels](https://forge.autodesk.com/en/support/get-help).

Better still, another approach yet more effective would be
to [join a Forge accelerator](https://forge.autodesk.com/accelerator-program) and ask there.

Since they are virtual nowadays, more people can participate.

That will guarantee you both an answer to your question and ensure you have ongoing support for the proof of concept.

You might even get your whole application completed right away during the accelerator.

How does that sound to you?

**Response:** I will check out the accelerator program for sure.

Thank you!

<center>
<img src="img/Bad_Monkeys_2007_book_cover.jpg" alt="Bad Monkeys book cover" title="Bad Monkeys book cover" width="190"/>
</center>


####<a name="3"></a> What Language to choose for a Revit Add-In?

**Question:** What language would you recommend me to start learning to program with Revit API?
Is [Python](https://www.python.org) fully functional by itself or does it need [pyRevit](https://github.com/eirannejad/pyRevit) installed to work?
Can you program independent Applications with Python?

**Answer:** Here are my off-hand answers:

&gt; What language would you recommend me to start learning to program with Revit API?

Pick the one you like best. It must support .NET.

&gt; Is Python fully functional by itself?

No, because it does not support .NET out of the box. Therefore, you need some kind of .NET support for it to interact with Revit API.

&gt; Does it need pyRevit installed to work?

No, not necessarily. That is one possible way to go. Another is the RevitPythonShell. Another is IronPython.

&gt; Can you program independent Applications with Python?

Yes, by including the .NET support in one way or another.

Anyway, what is 'independent'?

Every Revit add-in needs Revit to execute.

Depending on how your code is packaged, you may need Revit-plus-something.

I do all my work in C#, because then I am completely independent of all the complexities mentioned above.

However, I also sometimes like the flexibility of a Python command line.

The RevitPythonShell gives me that when I really need to dig deeper interactively.

By the way, you can also start off by writing macros instead of stand-alone add-ins.

The Revit macro environment supports both C# and Python right out of the box.

####<a name="3.1"></a> Addendum &ndash; C&#35; versus Python

**Sean Page adds:** I would second what Jeremy stated. I myself am exclusively C# when it comes to add-in or ZT nodes for Dynamo. I just prefer the ease of it just working, and the vast resources openly available for it. I would also say in terms of the Revit API, forcing yourself to use C# and explicitly define everything is a great way to learn, but may be a little slow at first because some things are a little more convoluted than they need to be.

**Steve R adds:** I would also recommend C#, just because you will find the most examples in C#, and probably the most help from others if you use C#. 

I'm still a beginner at this programming stuff, but the comment about Python needing another program to work with .NET confuses me.  Both the SharpDevelop built-in macro editor and Microsoft Visual Studio provide the options for either C# or Python. I was under the impression that using either would basically create the same DLL file.

Am I completely misunderstanding, or maybe things have changed and Python no longer has the same limitations it once had?
 
**Richard Thomas adds:** Also worth mentioning that Python can be used to extend Dynamo and that is interesting given the possibilities that Dynamo is now offering to those starting out their path to automation, e.g., generative design, rapid generation of geometry, linking different programs etc. Although you can also develop custom nodes in Dynamo with C# but inherently Dynamo is extended with Python code blocks.

Having said that I would choose C# over Python since it is one of the languages explicitly written for .NET.
IronPython is a .NET implementation of Python.

If you choose Python at an earlier stage in life then IronPython is how you would write a .NET application with it.

If you choose C at an earlier stage of life then C# would be your choice.

If you are only just making the choice you would choose either C# or VB.Net since they have the best IDE support in VS. Your Tab and BackSpace keys will also thank you, less work for them.

Many thanks for your contributions and clarifications!


####<a name="3.2"></a> Python and .NET

Some ruminations in response to Steve's question above:

Hmm. I am not aware of any possibility to reference .NET assemblies in Python in Visual Studio out of the box.

Yes, Revit's SharpDevelop built-in macro IDE includes support for Python.
I believe that uses IronPython internally to hook up the Python code with .NET.

I looked at the [overview of Python tools for Visual Studio](https://docs.microsoft.com/en-us/visualstudio/python/overview-of-python-tools-for-visual-studio?view=vs-2019) and found a note saying, *Visual Studio also supports IPython/Jupyter in the REPL, including inline plots, .NET, and Windows Presentation Foundation (WPF)*, but nothing else hinting at .NET support for Python in that environment.

Maybe Visual Studio can be combined with IronPython, as hinted at in the discussion
on [IronPython &ndash; Visual Studio 2010 or SharpDevelop?](https://stackoverflow.com/questions/2851898/ironpython-visual-studio-2010-or-sharpdevelop).

However, I also see this unanswered StackOverflow question
on [using Python for .NET for Python 3.4 in Visual Studio](https://stackoverflow.com/questions/54474775/using-python-for-net-for-python3-4-in-visual-studio) that seems to indicate it is not supported out of the box.

Actually, if you look directly at the [IronPython](https://ironpython.net) site and its [.NET integration documentation](https://ironpython.net/documentation/dotnet/), it seems to be pretty straightforward in any context.


####<a name="4"></a> The Building Coder Samples 2021 Migration

I quickly completed the flat migration
of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) to Revit 2021,
producing [release 2021.0.148.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2021.0.148.0).

The only changes involve the .NET target framework and the Revit API assembly references, cf.
the [diff to the previous version](https://github.com/jeremytammik/the_building_coder_samples/compare/2020.0.148.5...2021.0.148.0).

The result of this flat migration generates [162 warnings](zip/tbc_samples_2021_migr_01.txt),
all associated with obsolete and deprecated methods and enumerations caused by 
the [Units API changes](https://thebuildingcoder.typepad.com/blog/2020/04/whats-new-in-the-revit-2021-api.html#4.1.3).
