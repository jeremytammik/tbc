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

- https://forums.autodesk.com/t5/revit-api-forum/best-api-for-beginners/m-p/8050002
  Best API for Beginners?

- scott rosenbloom learning path Re: Getting Started Request for Recommendation

- renaming the family after loading the symbol
  Zhong (John) Wu
  Subject: FamilySymbol.Family behave different while getting by Doc.LoadFamilySymbol()

- Extensible Storage - best practices with plug-in development & growth
  https://forums.autodesk.com/t5/revit-api-forum/extensible-storage-best-practices-with-plug-in-development-amp/m-p/8050177

Extensible storage versioning and renaming a family in a project in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/renamefamily

An interesting question was raised on renaming a family after it has been loaded into a project, which throws some light on the underlying relationship between the family name and the <code>RFA</code> filename.
Before that, we revisit the eternal topic of getting started with the Revit API, and what to do to prepare for it before actually touching Revit at all.
Afterwards, and similarly, we pick up a good suggestion or two on planning and preparing your extensible storage schema for future version handling
&ndash; Before getting started
&ndash; Renaming a family in a project
&ndash; Extensible storage &ndash; versioning best practices...

--->

### Renaming a Family in a Project

An interesting question was raised on renaming a family after it has been loaded into a project, which throws some light on the underlying relationship between the family name and the `RFA` filename.

Before that, we revisit the eternal topic of getting started with the Revit API, and what to do to prepare for it before actually touching Revit at all.

Afterwards, and similarly, we pick up a good suggestion or two on planning and preparing your extensible storage schema for future version handling.

Explaining the similarity between the last two items mentioned above is left as an exercise to the reader &nbsp; :-)

- [Before getting started](#2) 
- [Renaming a family in a project](#3) 
- [Extensible storage &ndash; versioning best practices](#4) 

####<a name="2"></a>Before Getting Started

This topic was raised again in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread on choosing
the [best API for beginners](https://forums.autodesk.com/t5/revit-api-forum/best-api-for-beginners/m-p/8050002):

**Question:**  I know AutoLISP and made some tools for AutoCAD, and now I'm starting to take a look at Revit APIs.

My target is to create small tools, not big apps.

What is the recommended API to learn?

I don't want to learn C# or VB.NET, that's maybe a little bit too big.

There is also "Macro with Python" and "Dynamo" &ndash; can they be compared (from their features and possibilities), or is there a completely different approach?

**Answer:**  Welcome to the Revit API!

The one and only Revit API is based on .NET.

It makes no difference what language you use to address it: C#, VB, Python or other; they all address the same API, and they are all translated
to [.NET IL, intermediate language](https://en.wikipedia.org/wiki/Common_Intermediate_Language).

That also means that it is easy to translate between these languages, since
the [IL can be decompiled again](https://docs.microsoft.com/en-us/dotnet/framework/tools/ildasm-exe-il-disassembler) to
a different language than the one used to generate it.

[Dynamo ](https://forum.dynamobim.com) is
a separate environment using Python for programming that talks to the Revit API and also adds its own functionality.

[The Revit API getting started material](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2) lists
several other learning resources.

My personal opinion:

- VB is horrible and obsolete, cf. [is VB worth learning](https://www.quora.com/Is-Visual-Basic-NET-a-language-worth-learning)?
- C# and Python are both fine.
 
Almost all the official Revit SDK samples, Internet sample code and The Building Coder samples are in C#. 

By the way: please try to utterly forget AutoLISP and AutoCAD and all its APIs when learning Revit API,
because [Revit and its API is different](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.41).

Please also learn as much as possible about Revit and BIM philosophy before you try to start programming, or nothing will make any sense at all.

Good luck and have fun!

Another conversation on this topic took place via private email:

**Question:**  What C# programming topics would you recommend that someone learn <b><i>prior</i></b> to learning the Revit API?

**Answer:** You just need general .NET programming basics, nothing very high-level or complex.

The more the better, of course.

Actually, any and all programming experience is helpful.

One of the easiest languages to get started with is Python, IMHO.

Some old pointers to related resources:

- [A .NET Language Learning Resource](http://thebuildingcoder.typepad.com/blog/2009/07/a-net-language-learning-resource.html)
- [Cairo and Free .NET Books](http://thebuildingcoder.typepad.com/blog/2010/10/cairo-and-free-net-books.html)
- [C# and .NET Little Wonders](http://thebuildingcoder.typepad.com/blog/2010/11/c-and-net-little-wonders.html)

A good understanding of event-driven programming is very helpful, since the Revit API is entirely event driven.

**Response:**  I've been going through the [SoloLearn C# training](https://www.sololearn.com/Play/CSharp#).

And while it's really good, there's certainly nothing related to Revit at all.

I've really wanted to be able to create customisations for Revit for some time and really want to focus on it to get to a point where I can create, even some rudimentary programs.

**Answer:** How did you find the [Revit API getting started information](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2)?

**Response:**  After quite some time, here is the path I’ve taken in pursuit of learning the Revit API:

First, I took a course on the Microsoft Virtual Academy site called [C# Fundamentals for Absolute Beginners](https://mva.microsoft.com/en-us/training-courses/c-fundamentals-for-absolute-beginners-16169).

At the moment, I’m almost finished with the second section of Harry Mattison’s [Learn to Program the Revit API by Boost Your BIM](https://www.udemy.com/revitapi) on Udemy.com.

At the same time, I’m using the [SoloLearn](https://www.sololearn.com) app/website to continue reinforcing the basics of C#, and have also started looking at Google’s new 'learn to code' app called [Grasshopper](https://play.google.com/store/apps/details?id=com.area120.grasshopper&hl=en_US).

Any other thoughts?

**Answer:**  Congratulations on the good progress and well targeted approach.

All I can add is: anyone interested in cloud and web-based technologies and/or making a good career and lots of money programming, might want to take a look at [freecodecamp](https://www.freecodecamp.com).

That has nothing to do with Revit either, however.

<center>
<img src="img/bachelor_masters_degree_cap.png" alt="Cap" width="360"/>
</center>

####<a name="3"></a>Renaming a Family in a Project

**Question:**  I have a question related to the `LoadFamilySymbol` method.

Here is the description:

<pre class="code">
&nbsp;&nbsp;doc.LoadFamilySymbol(&nbsp;<span style="color:#a31515;">&quot;test.rfa&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;type1&quot;</span>,&nbsp;<span style="color:blue;">out</span>&nbsp;symbol1&nbsp;);
&nbsp;&nbsp;doc.LoadFamilySymbol(&nbsp;<span style="color:#a31515;">&quot;test.rfa&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;type2&quot;</span>,&nbsp;<span style="color:blue;">out</span>&nbsp;symbol2&nbsp;);
</pre>

When I load two family symbols together as above, only one single family is created with the two types.

I can modify the family name by using either one of the symbols like this:

<pre class="code">
&nbsp;&nbsp;symbol2.Family.Name&nbsp;=&nbsp;<span style="color:#a31515;">&quot;NewName&quot;</span>;
</pre>

Then `symbol1.Family.Name` will be also updated to "NewName".

But the behaviour changes if I rename the family after the first one has been loaded, for example:

<pre class="code">
&nbsp;&nbsp;document.LoadFamilySymbol(&nbsp;<span style="color:#a31515;">&quot;test.rfa&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;type1&quot;</span>,&nbsp;<span style="color:blue;">out</span>&nbsp;symbol1&nbsp;);
&nbsp;&nbsp;symbol1.Family.Name&nbsp;=&nbsp;<span style="color:#a31515;">&quot;NewName&quot;</span>;
</pre>

After this, the second load will create a new family. Renaming the symbol to the same name then doesn't work:

<pre class="code">
&nbsp;&nbsp;document.LoadFamilySymbol(&nbsp;<span style="color:#a31515;">&quot;test.rfa&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;type2&quot;</span>,&nbsp;<span style="color:blue;">out</span>&nbsp;symbol2&nbsp;);
</pre>

It creates new family, not associated with the first; `symbol2.Family.Name` remains as the old name.

**Answer:**  Yes, that makes sense to me.

The family name is identical to the `RFA` filename.

Case 1:

- load two symbols
- both are associated with 'test'
- rename the family
- both symbols have a new family name

Case 2:

- load one symbol
- it is associated with 'test'
- rename the family --> association lost
- load the second family
- it is associated with 'test', but the first is <b><i>not</i></b>
- the two symbols now have different family names

Totally logical.

**Response:**  Thanks Jeremy.
So, the family name is always identical with the `RFA` filename.
This should be the answer for the issue.
Yes, it makes sense for me.

Is there some way to load other family symbols to the existing family after it has been renamed, instead of creating a new one associated with ‘test’?
That’s actually the essential question; I want to load family symbols one by one to save performance, and rename the family sometime after loading the first symbol.

**Answer:**  Yes, I do believe that a very simple workaround can be created based on what we already said:

- original family name: A.
- load symbol S1 from family A &rarr; A.S1.
- rename S1's family to B &rarr; B.S1 is present in the project.

Now, to load A.S2, it might be sufficient to:

- temporarily rename A.rfa to B.rfa.
- load S2 from B.rfa &rarr; B.S1, B.S2 are present in the project.
- rename B.rfa back to A.rfa.

Please try it out, test and confirm.

**Response:**  I can confirm the workaround is working after my verification: Revit will load and name the family by its `RFA` filename.


####<a name="4"></a>Extensible Storage &ndash; Versioning Best Practices

Another interesting question was raised in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [extensible storage best practices with plug-in development and growth](https://forums.autodesk.com/t5/revit-api-forum/extensible-storage-best-practices-with-plug-in-development-amp/m-p/8050177),
with helpful answers provided
by David Robison, [Design Master Electrical RT](http://www.designmaster.biz/revit)
and Alexander [@aignatovich](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478) Ignatovich, aka Александр Игнатович:

**Question:** I'm currently developing a Revit plug-in and need to utilise a schema; for now, I will just need two bool fields.

However, the plug-in will grow over time and eventually will become a ribbon with multiple buttons and various other functionality.

Thus, I assume that at some point I might need more sophisticated extensible storage data.

I already know that once a schema is built, it cannot be extended; what is then the best practice in such situations?

To have separate schemas for each part of my plug-in?

**Answer 1:** Some people store data in serialized form (such as `JSON`).

I don't think that it is good practice.
Imagine that your app v1.0 requires 2 pieces of data: A and B, and your app v1.2 requires 3 pieces of data: A, B and C. Your v1.2 app reads storage and sees that there is no C. What does this mean? Was it due to old data, or C should not be present? Your next app v.1.3 requires new data format for B &rarr; B', may be with old data structure. Again, you have to deal with ambiguous environments; moreover, your app v.1.3 has to deal with 3 data formats: (A, B), (A, B, C) and (A, B', C).

To handle such a situation, you should define and include data version information and build a specific processor for each version:

- (A, B) &rarr; v1.0 processor that, for example, asks user about C and transfers data for the next processor.
- (A, B, C) &rarr; v.1.2 processor that converts B to B' using some algorithm.
- (A, B', C) &rarr; just use the data to do useful things.

Since schemas cannot be modified, each specific schema with its unique `GUID` id works as a version mark. It has strongly typed fields and it is rather comfortable to use. Your task as a software developer is just to organise your code, encapsulate details, and not repeat yourself. Prohibition of scheme modification is a good thing in my opinion.

**Answer 2:** Not being able to modify schemas does become a problem as your add-in becomes more complicated.

I store everything in map fields, like this:

<pre class="code">
&nbsp;&nbsp;builder.AddMapField(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;strings&quot;</span>,&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:blue;">string</span>&nbsp;),&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:blue;">string</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;builder.AddMapField(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;ints&quot;</span>,&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:blue;">string</span>&nbsp;),&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:blue;">int</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;FieldBuilder&nbsp;field&nbsp;=&nbsp;builder.AddMapField(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;doubles&quot;</span>,&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:blue;">string</span>&nbsp;),&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:blue;">double</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;field.SetUnitType(&nbsp;<span style="color:#2b91af;">UnitType</span>.UT_Custom&nbsp;);
 
&nbsp;&nbsp;builder.AddMapField(&nbsp;<span style="color:#a31515;">&quot;bools&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:blue;">string</span>&nbsp;),&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:blue;">bool</span>&nbsp;)&nbsp;);
</pre>

These fields allow me to change what I am storing without modifying my schema. The first value I always add is a version to the ints. When I get my entity from an element, if it's not there, I know I need to add it and do any required initialisation. If it is there, I check the version and do whatever upgrades are necessary if it is not the most recent.

**Response:** It seems like having these four dictionaries (strings, bools, ints, doubles) should be enough to handle every possible data. If you need other types, you can derive them from these four, one way or another.

I really like this idea, it's more generic from Alexander's, yet still it enables version control.

Thanks for sharing, you two!

Jeremy adds: for Revit-specific element relationships, you might also need the `ElementId` data type.
