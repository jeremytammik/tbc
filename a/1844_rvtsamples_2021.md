<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- updated RvtSamples for Revit 2021 and added support for AdnRevitApiLabsXtra and the_building_coder_samples https://github.com/jeremytammik/RevitSdkSamples

- updated Revit API training material for Revit 2019, 2020 and 2021

- updated RevitSdkSamples for Revit 2021 https://github.com/jeremytammik/RevitSdkSamples

- getting started with a database
  Best Database type for Revit data
  https://forums.autodesk.com/t5/revit-api-forum/best-database-type-for-revit-data/m-p/9503730

twitter:

Choosing a database and loading all the Revit 2021 SDK, TBC samples and labs with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/rvtsamples2021

Another busy week so far
&ndash; Loading all Revit 2021 SDK samples
&ndash; Loading The Building Coder samples and labs
&ndash; What database is best for Revit data?...

linkedin:

Choosing a database and loading all the Revit 2021 SDK, TBC samples and labs with the #RevitAPI

https://bit.ly/rvtsamples2021

- Loading all Revit 2021 SDK samples
- Loading The Building Coder samples and labs
- What database is best for Revit data?...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Setting up RvtSamples for Revit 2021

Another overly busy week so far:

- [Loading all Revit 2021 SDK samples](#2)
- [Loading The Building Coder samples and labs](#3)
- [What database is best for Revit data?](#4)

####<a name="2"></a> Loading all Revit 2021 SDK Samples

On Monday, I discussed my struggles
to [successfully compile the Revit 2021 SDK samples](https://thebuildingcoder.typepad.com/blog/2020/05/compiling-the-revit-2021-sdk-samples.html).

The next step for me is always the installation of RvtSamples to load all the SDK samples into Revit for easier access for testing.

Since the Revit SDK includes 190 separate Visual Studio projects, you can imagine the work it would take to load them individually.

RvtSamples reads a text file listing the external command data and assemblies to load and makes them all available in one fell swoop.

Inclusion of additional files is supported, and I use that for The Building Coder samples and training labs.

Setting up the correct paths for all the SDK samples is often significant work, because the Revit development team make modification that require attention and do not keep RvtSamples.txt up to date.

The fixes I applied can be examined in
the [RevitSdkSamples list of releases](https://github.com/jeremytammik/RevitSdkSamples/releases).

####<a name="3"></a> Loading The Building Coder Samples and Labs

Once I have the official SDK samples installed, I also migrate and add a number of additional samples to the collection of external commands loaded by RvtSamples:

- The [ADN Revit API training material](https://github.com/ADN-DevTech/RevitTrainingMaterial) (nowadays DAS, by the way)
- The [Xtra labs](https://github.com/jeremytammik/AdnRevitApiLabsXtra) (comprising the former plus their older incarnation)
- [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) 

That task is now completed as well.

There is not that much more to say about it, really.

The migration was straightforward, and the installation as well.

You can examine the exact required steps by analysing the GitHub commits and diffs.

I thank Naveen T Kumar for his support migrating the official DAS Revit API training material.

<center>
<img src="img/rvtsamples_2021.png" alt="RvtSamples 2021" title="RvtSamples 2021" width="903"/> <!-- 903 -->
</center>

####<a name="4"></a> What Database is Best for Revit Data?

An interesting question popped up in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [best database type for Revit data](https://forums.autodesk.com/t5/revit-api-forum/best-database-type-for-revit-data/m-p/9503730):

**Question:** My question is about choosing a database type for exporting my Revit data.

I'm not a database expert, but I know that the following types exist: MySQL, PostgreSQL and NoSQL.

I would like to program something that exports my data from Revit to a database for further use (creating BoQ, ...)

However, I don't know which database type is best suited for exporting my data to?

Should I use MySQL, PostgreSQL or NoSQL?

You're probably thinking "well it depends on what you want to do".

But the thing is that I can't tell all the future use cases at this time, I just want to have a database with all of my Revit data so that I can do pretty much what I want with it in the future.

Any advice on this topic?

Thanks a lot in advance!

**Answer:** Good question! Thank you!

The best database for storing a Revit BIM model is the Revit database in the RVT file format.

Unfortunately, you have no direct access to the underlying data, so that won't help resolve your question.

You are basically answering your own question quite well, I think.

It depends on what you want to do. It also depends very much on your past experience, your personal preferences and your future plans.

I am not a professional programmer or a database expert, so you should take any advice I give with many grains of salt.

I would advise: avoid traditional SQL!

Unless you are an expert on that or have other pressing reasons to choose it.

Instead:

Go for the much more modern, scalable, minimalistic, low-cost, simple to use, web-adapted NoSQL options instead.

This personal opinion of mine in based on my experience developing
several samples to [connect the desktop and cloud](https://github.com/jeremytammik/FireRatingCloud)
using Revit and its API on the desktop and JavaScript, Node.js web servers, CouchDB and MongoDB databases in the cloud.

**Response:** The only issue I'm having is that we would also like to link some of our internal databases to the Revit database.

The internal databases are MySQL, so is it possible to link a NoSQL database to a MySQL database?

**Answer:** To me, that sounds like a good reason to choose MySQL after all.

Poor you, you will not be learning anything new...

<center>
<img src="img/friends_use_nosql.jpg" alt="Friends use NoSQL" title="Friends use NoSQL" width="500"/> <!-- 1400 -->
</center>

