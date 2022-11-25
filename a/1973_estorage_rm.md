<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Not able to delete Extensible Storage schema
  https://forums.autodesk.com/t5/revit-api-forum/not-able-to-delete-extensible-storage-schema/m-p/11541801
  REVIT-196204 [IFC link prevents extensible storage delete]

twitter:

A new team member, new Revit API and WPF tutorials, and new insights on handling extensible storage deletion and conflicts with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/estoragedeletion

A new team member, new Revit API and WPF tutorials, and new insights on handling extensible storage
&ndash; Welcome, George!
&ndash; WPF form UIApplication access
&ndash; Mazri's Revit, Dynamo, web and WPF playlists
&ndash; Extensible storage schema deletion
&ndash; Extensible storage schema conflict...

linkedin:

A new team member, new Revit API and WPF tutorials, and new insights on handling extensible storage deletion and conflicts with the #RevitAPI

https://autode.sk/estoragedeletion

- Welcome, George!
- WPF form UIApplication access
- Mazri's Revit, Dynamo, web and WPF playlists
- Extensible storage schema deletion
- Extensible storage schema conflict...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Extensible Storage Schema Deletion

A new team member, new Revit API and WPF tutorials, and new insights on handling extensible storage:

- [Welcome, George!](#2)
- [WPF form UIApplication access](#3)
- [Mazri's Revit, Dynamo, web and WPF playlists](#4)
- [Extensible storage schema deletion](#5)
- [Extensible storage schema conflict](#6)

####<a name="2"></a> Welcome, George!

A warm welcome to my new colleague George Moturi!
He joined the DAS Developer Advocacy and Support team in September.
He is based in Nairobi, Kenya.
He comes with a computer science background and has worked for a few companies as a web developer.
Initially, he will be focusing on learning and supporting the Revit API, and later stepping into Forge and ACC, the Autodesk Construction Cloud.
In his own words:
 
> My name is Moturi George and I am joining as a Developer Advocate.
Prior to that, I was a software developer for 5 years.
I worked for fintech, media &amp; advertising and a digital marketing company, involving API development and integration with different providers and vendors.
I have developed applications using the .NET Framework in C#, built websites using PHP, Vue JS, HTML and CSS.
When not working, I love to watch documentaries, play computer games and having a chat on current affairs with my neighbours and friends. 
I'm happy and excited to be part of this amazing team that is building great Autodesk Community experiences!

<center>
<img src="img/george_moturi.png" alt="George Moturi" title="George Moturi" width="260"/>  <!-- 565 × 881 pixels -->
</center>

####<a name="3"></a> WPF Form UIApplication Access

George just added a helpful pointer to 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [How to call UIApplication of my current Document](https://forums.autodesk.com/t5/revit-api-forum/how-to-call-uiapplication-of-my-current-document/m-p/11570137):

**Question:** I defined a command class that implements the `Execute` method and some other methods and Windows form named `LoadFamily`.
Its first parameter is `UIApplication`.
However, I don't know how I should call that parameter when I'm in a form button.
In other word, how should I call the `UIApplication` of my command class in one of its other methods?

**Answer:** Make the `UIApplication` instance a member variable of your form class.
Store the value you receive in the constructor in the member variable.
Use its value in the button click handler method.

Here is a tutorial that explains step by step how to create WPF forms and call its methods outside your command class:

- [Revit + WPF -Quick starting guide- Ep4: adding UI to a command](https://youtu.be/vHsqxRAqQOg)

####<a name="4"></a> Mazri's Revit, Dynamo, Web and WPF Playlists

Looking more closely at George's recommendation in the answer above, I am very impressed
with [Mazri](https://twitter.com/mazri_a),
his [YouTube BIM Diary](https://www.youtube.com/@mazrisbimdiary2045)
and [tutorial playlists](https://www.youtube.com/@mazrisbimdiary2045/playlists), covering the following areas:

- Web dev
- Dynamo &amp; Data
- Make Your own Revit Plug-ins
- Revit Programming using Python
- Revit + WPF

Specifically targeted at getting started with C# programming and WPF, Mazri recommends:

- A [nice friendly C# course for beginners](https://www.youtube.com/playlist?list=PLLAZ4kZ9dFpNIBTYHNDrhfE9C-imUXCmk)
- Microsoft's [XAML overview (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/xaml/?view=netdesktop-6.0)
- Tim Corey's [Intro to WPF: Learn the basics and best practices of WPF for C#](https://youtu.be/gSfMNjWNoX0)
- AngelSix' [C# WPF UI Tutorials: 01 - The Basics](https://youtu.be/Vjldip84CXQ)

Looks like great stuff!

Many thanks to Mazri for his work, and to George for making me aware of it.

####<a name="5"></a> Extensible Storage Schema Deletion

Some developers have encountered issues creating and deleting extensible storage schemata.

Many of these can be resolved by understanding and adhering to the underlying basic principles.

The most fundamental ones are:

- A schema is identified by its GUID
- A schema can never be modified
- A schema is universal

You can get into as nice mess by violating any of these principles.
To be more precise, they cannot be violated, but people sometimes still inadvertently try to, and then things get unpleasant.

A new aspect came up in the following extensive discussion 
on [not able to delete extensible storage schema](https://forums.autodesk.com/t5/revit-api-forum/not-able-to-delete-extensible-storage-schema/m-p/11541801):

- The [`ExtensibleStorage.Schema` version of `EraseSchemaAndAllEntities` erasing `Schema` from all open documents had been deprecated and removed](https://www.revitapidocs.com/2023/80983aac-0cca-c211-1c7b-b5350624f046.htm)
  &ndash;
  the [Document.EraseSchemaAndAllEntities](https://www.revitapidocs.com/2023/50debcb0-3c4f-b32b-2edb-8a6ef7b4bf8d.htm) method
should be used instead
  &ndash; maybe in earlier releases as well...

Here is an edited version of the conversation:

**Question:** I'm trying to put together a command to erase Extensible Storage (ES) data and the corresponding Schemas for our addin in case if unwanted by the users or corrupt. I had no luck so far. 

After the ES data has been written to the file it won't go away. I am being able to delete the DataStorage elements with no troubles, but the schemas keep popping up after erasing them.

The procedure I'm trying is:

- Open the Revit file with the saved ES data.
- Delete all the DataStorage elements holding the data.
- Save and close Revit, reopen the file again.
- Erase all schemas created by our addin.
- Save and close Revit to make sure I clear the schemas in memory.
- Start Revit again.
- Use the Schema.ListSchemas() function with no documents open to make sure our custom schemas are not loaded, this clears out.
- Open the file and use the Schema.ListSchemas() function, now all the schemas that I have erased previously reappear after opening the file!

I'm using the same procedure found in the "ExtensibleStorageUtility" code example in the Revit SDK in a macro.

One observation I have that if I close Revit without saving after step 4, I get no warning to save the file, as if no changes happened to it, although I'm doing the erase inside a transaction.

I tried to utilize the info on ES and Schema behaviours
from [CADD helpdesk hot picks – classification manager schema error](https://www.caddmicrosystems.com/blog/cadd-helpdesk-hot-picks-classification-manager-schema-error),
but that didn't work:

Any light on this would be much appreciated.

The add-in does not create the schema when it's loaded; the schema is created on demand.

**Answer:** I would avoid the try/catch with an empty catch-all.
Using that, you will never notice if anything goes wrong.
[Never catch all exceptions](https://thebuildingcoder.typepad.com/blog/2017/05/prompt-cancel-throws-exception-in-revit-2018.html#5)!

Here is an old article by The Building Coder
on [erasing extensible storage](https://thebuildingcoder.typepad.com/blog/2013/11/erasing-extensible-storage-with-linked-files.html).

**Response:** I already looked at all the Schema related posts in The Building Coder blog.
None helped solving the issue in hand.

After further investigation, it turned out that the `Schema.EraseSchemaAndAllEntitie` function is working fine in Revit 2019; it is in Revit 2022 that it is failing.
I haven't tried the in-between versions, but I suspect that this is the case for 2021 as well, as it is in that version that the API change happened, deprecating this function from the `Schema` class and providing one in the `Document` class.

<!------

I attached 2 Revit files with embedded macros for versions 2019 and 2022, both files have the same code except that the 2022 file is using the new Document.EraseSchemaAndAllEntities(...) function along with the deprecated one.

To replicate the issue:

Open file es test-V22.rvt in Revit 2022
Run the embedded macro ListSchemas to examine the OOTB existing schemas
Run the embedded macro WriteESData to write sample extensible storage data
repeat step #2, you should see a new entry of MYID:  SomeData
Save the file
Run the embedded macro DeleteSchemas
Repeat step #2, you should see that the new entry is gone
Save the file
Close Revit 2022 (Just to make sure that the schemas are cleared from memory)
Open Revit and the file again
Repeat step #2, the MYID:  SomeData schema reappears again!
If you follow the same steps on the file es test-V19.rvt in Revit 2019 the deletion will be successful.

I hope I am doing something silly, but I really think that it is an issue with the API update.
Please let me know if you need further info on the test case.

Thanks,

Sam

es test - V19.rvt
 
es test - V22.rvt

jeremy.tammik

Dear Sam,

Sorry for not following up immediately and thank you for pointing out this again.

@RPTHOMAS108  just discussed and resolved a similar issue in another thread here in the forum, and I am pretty sure that addresses the same root cause. His explanations will hopefully help resolve your problem as well:

https://thebuildingcoder.typepad.com/blog/2021/11/new-analytical-model-api.html#6

I very much hope this helps.

Cheers,

Jeremy

snajjar

Thank you for your reply @jeremy.tammik, I much appreciate all the help you provide.

-->

I read through all the blog posts and the solution by @RPTHOMAS108, and I'm sorry to say that it does not address the issue I'm having.

The solution and guidelines are addressing `DataStorage` and `Schema` creation and retrieval,
I don't have issues there, and I can say that we are following the proposed best practices mentioned in the blog post for the creation procedures:

- Schemas are being created on-demand
- Using new Entity(Schema) instead of new Entity(GUID)
- Not passing entities ByRef
- Utilizing ExtensibleStorageFilter and Schema.Lookup(SchemaGuid).

My issue is with <u>deleting</u> the schema, which is working fine in 2019 but not in 2022.

I am testing this in freshly created Revit files which I have included in my previous response with embedded macro code. The same code is being used in both files, but the 2022 one is not working.

The reason we are seeking Schema deletion is that when a Revit file that contains a newer version of the schema (adding more fields for example) and it has a linked Revit file with an older version, Revit will display a warning when the file is opened. This warning dialog is hindering our client's automation software that opens Revit files and process them automatically. We don't have control over that software nor want to suppress this warning because, while we are sure that this will not affect the tools we are developing and the integrity of the file, we can't guarantee that for all other vendors and the suppression will hide all such warnings for data coming from all the installed addins. Our goal is to provide a tool to delete our schema from the linked files to resolve the issue.

I really hope this can be looked at by the development team to see if it is a bug caused by the API update that took place in Revit 2021.

**Answer:** I've not looked into this;
it should be possible to delete the schema if it is not used regardless, so that does seem wrong (especially if it previously worked).
Did you have schema conflicts in the 2019 version though?

However, one thing that occurred to me as I read your latest message is that you should only get that warning in the first place if you've done something wrong in terms of managing the schemas.
When you create a schema with a GUID that is that version forever.
To add additional members, you should create a new version and transfer the data (leave the old one alone, do not reuse it's GUID).

You say, <i>a newer version of the schema (adding more fields for example)</i>.

No such thing exists!

If you have created such a situation, you are in serious trouble.

An existing schema cannot be modified:

**Response:** I followed the procedure above (2022 version only) and was also not able to remove the schema, so regardless of misuse something seemingly warranting some further investigation with the `EraseSchemaAndAllEntities` methods perhaps, i.e., this is a clean file to start with, presumably.

Note that the macro doesn't delete the `DataStorage` element, but I deleted this manually prior to step 6.
However, I still wasn't able to permanently remove the schema.

I note that the old method `Schema.EraseSchemaAndAllEntities` has a Boolean `overrideWriteAccessWithUserPermission` which is not present in the new `Document` method of the same name.

This may be a conceptual choice going forward but may be part of the issue (changes to underlying method that serves both). Also, the test was only conducted within the macro environment (add-in id is explicitly set in the attribute).

I made the below minor change so any exceptions would show up but still no exceptions were reported:

<center>
<img src="img/estorage_erase_error.png" alt="Extensible storage schema erase error" title="Extensible storage schema erase error" width="600"/>  <!-- 856 × 324 p -->
</center>

**Response:** Your observation is spot-on, my usual procedure is to change the guid if I ever change something in the schema so a new schema would be created, but it looks like I missed doing that in one class, and this is why I am seeking the deletion functionality.
This should not be an issue for future updates, but I need to provide a resolution for the models edited by the current version.

The issue with not being able to delete the schema is irrelevant to what I did wrong.

The sample files are in their simplest forms, freshly made out of the architectural template and contain no elements, just the embedded macro scripts.

**Answer:** The devteam replied: After running `DeleteSchemas` macro, open Manage > Purge Unused.
In the tree, select Extensible Storage Schema.
Check the schema a9dc2b48 and click OK to purge it.
Run `ListSchemas` &ndash; the schema is purged.
So, please use Purge Unused to delete schemas without entities.
 
I pointed out that this question comes from add-in developers automating processes and asked how they should run the purge command programmatically.
We'll see what they come up with next.
  
A similar issue came up again with a new customer, and the devteam underline:

I would strongly recommend to any developer not to reuse a GUID for a new or modified schema for any reason, even if you think it has been purged from one document it may not be purged from other documents and you are setting yourselves up for a potential in-memory conflict.
 
We have seen this many times from developers who copied and did not modify the GUID in our samples.

**Question:** Recently I faced the same issue: for some projects, I got an InternalException while trying to remove ExtensibleStorage schemas.
The exception does not occur after I unloaded IFC links. 

**Answer:** Thank you for your interesting observation.
It would be nice (and very useful) if the other parties concerned could test and hopefully verify this solution.

**Response:** I created a very small example with IFC link (link with a few walls created in Revit2020):

- [zip/estorage_delete_schema_example.zip](/Users/jta/a/doc/revit/tbc/git/a/zip/estorage_delete_schema_example.zip)

It includes 4 macros:

- Create example schema
- Remove example schema
- List all schemata
- Remove all schemata

In Revit 2020, I am not able to remove any schemata when the IFC link is loaded.

An InternalException also occurs when removing schemata when there is more than one project file opened (Revit 2020).

**Answer:** I logged the issue REVIT-196204 [IFC link prevents extensible storage delete] for this 

InternalException when trying to remove schemas is currently not a big issue for me.

I landed on this thread because of difficulties related to upgrade from Revit 2020 to Revit 2023.
In a few models we experience some critical errors when trying to upgrade to version 2023.
We came to the point that it could be directly or indirectly related to schemas.
After we remove ExStorage schemas the upgrade to 2023 is successful.

I noticed that removing schemas and entities sometimes causes Errors and Warnings, for example:

- Error: Can't keep elements joined. (Model Lines, Walls)
- Warning: Highlighted lines overlap. Lines may not form closed loops. (Model Lines)

Possibly this is just because the elements are regenerated after the entity is removed.

I wouldn't believe that ExStorages could cause any upgrade issues  if I didn't see it happening.

What is your opinion on the possibility that ExtensibleStorage schemas and entities may cause upgrade issues?

**Answer:** A link opens a document in the background, but you can't edit that linked document, can you?
Does the linked document contain the same schema itself?
If so, then I would see it as logical why the linked document causes issues. Should not cause an exception though.

Extensible storage schema is an application-wide object.
If it exists at all in the application, it will populate and "infect" every single document that you touch.
That makes it hard to remove, and complicated to understand.

The development team analysed the development ticket and decided not to make any changes, explaining:

The `Document` class provides the method `EraseSchemaAndAllEntities`:

<pre>
  # Erases Schema and all its Entities from the document.
  remarks
     # The Schema remains in memory.
  in ESSchema* schema
     # The Schema to erase.
  since 2021
  validate DocumentValidation::isDocumentModifiable(this)
  throws ArgumentException
     # No write access to this Schema.
</pre>

The [version of `EraseSchemaAndAllEntities` erasing `Schema` from all open documents had been deprecated and removed](https://www.revitapidocs.com/2023/80983aac-0cca-c211-1c7b-b5350624f046.htm):

- 2023 | Resource Not Available for the Active API Year: `EraseSchemaAndAllEntities` Method
  &ndash; Erases all Entities corresponding to this Schema from all open documents and erases this Schema from memory.

The [Document.EraseSchemaAndAllEntities](https://www.revitapidocs.com/2023/50debcb0-3c4f-b32b-2edb-8a6ef7b4bf8d.htm) method
should be used instead:

- Erases Schema and all its Entities from the document.

Many thanks to Sam, Marek and Richard for all their research and the fruitful discussion.

####<a name="6"></a> Extensible Storage Schema Conflict

Another aspect was tagged on at the end of the above:

**Question:** OK, I guess I need to jump on board the Schema issue.
I'm also getting an error for an EnigmaSchema that is in multiple files:

<center>
<img src="img/estorage_schema_conflict.png" alt="Extensible storage schema conflict" title="Extensible storage schema conflict" width="445"/>  <!-- 445 × 331 -->
</center>

No idea how to get this to be removed.
Any help would be appreciated.

**Answer:** You have to write a macro/addin that calls the above function and if that doesn't work it is usually due to entities on elements in nested families etc.
So, it is not easy because sometimes it involves iterating families opening them calling the above and reloading them.
This issue is also evident on links where the documents for such are loaded in the background and are not editable from the file they are linked into.

Once a schema is loaded, then a different one loaded with the same id will cause this.
Developers often don't appreciate that even changing the documentation for a schema makes it different.
Any change to the structure of the schema or documentation makes it different.
In this case, prevention is better than cure because the cure is very hard to achieve.

Not sure who wrote the EnigmaSchema, but they obviously wanted to leave no details.

What I've also noticed in the past (not sure it is still true) is that once you get this dialogue then you can't remove the schema because it is in conflict.
So, in the case of links for example you had to unload them all and deal with each file separately, i.e., to have Revit always in a state where the conflict had not arisen when you go to remove the schema.
It made removing them from loaded families virtually impossible.
Hopefully things are not that hard anymore.

See also the suggestion above about purging, which may work from an end user perspective:

> After running DeleteSchemas macro, open Manage &gt; Purge Unused.
In the tree, select Extensible Storage Schema.
Check the schema id and click OK to purge it.
Run ListSchemas &ndash; the schema is purged.
So, please use Purge Unused to delete schemas without entities.
