<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- [trouble reading entity schema]
  https://forums.autodesk.com/t5/revit-api-forum/trouble-reading-entity-schema/m-p/9046186

- Re: ExternalEvent
  https://forums.autodesk.com/t5/revit-api-forum/externalevent/m-p/9029731

- System.Data.Sqlite cannot be loaded
  https://forums.autodesk.com/t5/revit-api-forum/syste-data-sqlite-can-not-loaded/m-p/9039972

- https://forums.autodesk.com/t5/revit-api-forum/get-sides-of-cylinder-element/td-p/9044459

twitter:

Extensible storage trouble, external events, DLLs and pipe side faces in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/extensibleexternal

Preserving another couple of interesting recent threads from the Revit API discussion forum on various extensible and external topics
&ndash; Extensible storage reading trouble
&ndash; External event implementation
&ndash; External DLL loading
&ndash; External side face of a pipe...

linkedin:

Extensible storage trouble, external events, DLLs and pipe side faces in the #RevitAPI 

http://bit.ly/extensibleexternal

Preserving another couple of interesting recent threads from the Revit API discussion forum on various extensible and external topics:

- Extensible storage reading trouble
- External event implementation
- External DLL loading
- External side face of a pipe...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Extensible Storage, External Event, DLL, Pipe Face

Preserving another couple of interesting recent threads from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) on
various extensible and external topics:

- [Extensible storage reading trouble](#2)
- [External event implementation](#3)
- [External DLL loading](#4)
- [External side face of a pipe](#5)

<center>
<img src="img/herbstbild.jpg" alt="Autumn leaves" width="605">
<p style="font-size: 80%; font-style:italic">Autumn leaves</p>
</center>

####<a name="2"></a> Extensible Storage Reading Trouble

A creative suggestion for reading existing extensible storage data was made even after discovering a happy resolution for 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [trouble reading entity schema](https://forums.autodesk.com/t5/revit-api-forum/trouble-reading-entity-schema/m-p/9046186):

**Question:** I seem to be having some trouble getting access to the entity schema I created.

About 7 years ago, I created an app that reads & writes a schema to the project info element. A year later I created another app that only reads this schema. Now I am updating the second app and I find with Revit 2019 it can no longer read the schema. I keep getting the error "Schema not available to this application" but in the schema builder the read/write access is set to vendor. The name, GUID and vendor id are set the same in both apps. I can still get the schema with the latest version of the app that created it, it's just the second app that can't.

Is this a bug in the most recent API's?

Later: Never mind. I figured it out.
I had loaded my app through the wonderful add-in manager you created.
So, the schema thought it was the add-in manager that was trying to access my schema.
Everything works properly if I load my app with the manifest file without ever having loaded the app through the manager.

**Answer by Christian @cwaluga Waluga:** Glad to hear you figured it out!

You can also try to change the vendor and add-in identification in the AddInManager `.addin` manifest file to yours.

I do the same with RevitLookup to snoop my own extensible storage.

Many thanks to Christian for this useful hint!

####<a name="3"></a> External Event Implementation

A pointer to a useful Autodesk knowledge base article was mentioned in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [ExternalEvent](https://forums.autodesk.com/t5/revit-api-forum/externalevent/m-p/9029731),
prompting me to copy it to this blog for secure preservation for posterity:

**Question:** I want to create an `ExternalEvent` on button click.
But I get an exception: *Attempting to create an ExternalEvent outside of a standard API execution*.
When I checked the thread of the main window and the `Command` class, they were the same thread.
What's the magic? I created the class for resetting event in `Command` class.

**Answer:** Look at the Revit SDK sample *ModelessDialog/ModelessForm_ExternalEvent*
and [The Building Coder samples implementing external events](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

Sean Page adds a helpful pointer, saying:

> I found this site to be exceptionally helpful when creating external event handlers:
[External Events in knowledge.autodesk.com](https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/Revit-API/files/GUID-0A0D656E-5C44-49E8-A891-6C29F88E35C0-htm.html)

This knowledgebase article refers to Revit 2016.
Strangely, I cannot find an updated link for Revit 2020
For safety's sake, I created a [local copy here of the External Events knowledgebase article](zip/external_events_knowledgebase.html).

Bobby C. Jones completes the answer, explaining: You cannot call `ExternalEvent.Create` outside of a valid Revit API context, and a callback from a button click is most definitely not a valid context.

What I do is create an instance of the `IExternalEventHandler` and call `ExternalEvent.Create` from the `IExternalCommand`, or other valid context, and pass them to the viewmodel (or create them in the ViewModel constructor if you're newing up the ViewModel in the `IExternalCommand`.
I prefer MVVM; if you do not, then pass them to or create them where you're UI logic resides, a Controller, directly in the Form, or wherever.
In your button click callback, you pass necessary state info to your `IExternalEventHandler` and then call `Raise`.

####<a name="4"></a> External DLL Loading

Another recurring issue deals with a problem loading an external DLL; in this case,
[System.Data.Sqlite cannot be loaded](https://forums.autodesk.com/t5/revit-api-forum/syste-data-sqlite-can-not-loaded/m-p/9039972):

**Question:** I am writing an app that uses sqlite to write to a db and I make a reference to `System.Data.Sqlite`, but when I run the app inside Revit, I get the error message *Could not load file or assembly or one of its dependencies*.

What can be the cause of that error? 

**Answer by Revitalizer:** Autodesk itself uses a bunch of .NET DLL files in its own add-ins that are shipped with Revit.

Since they are loaded before your add-in, there may be conflicts if different versions are used.

So, make sure you use the same DLL versions as Autodesk does.

This applies to all the DLLs you see in the Revit.exe directory, e.g., Sqlite, Log4Net, ...

####<a name="5"></a> External Side Face of a Pipe

Finally, on a topic that I really enjoy, geometrical, on finding the external side face of a pipe, in the thread
on [get sides of cylinder element](https://forums.autodesk.com/t5/revit-api-forum/get-sides-of-cylinder-element/td-p/9044459):

**Question:** Does anyone have ideas on specifying a particular side of a cylinder/element?

I need to get the northern/top side in this view.

<center>
<img src="img/pipe_cylinder_element_sides.png" alt="Pipe cylinder element sides" width="500"> <!-- 1640 -->
</center>

I know this could be done manually by picking one side or the other on the element, however I am attempting to this with no user input.  

I have attempted using the actual geometry class in hope of being able to cycle through the faces, but, since it is a cylinder, I am only getting mesh objects which I am not entirely sure how to use in this way. Any ideas are much appreciated.

**Answer:** Depending on how the cylinder has been constructed, the two sides you point out may actually belong to the same face. Therefore, there are possibly no two faces there at all. So, you will have to specify more exactly what you actually want to achieve. If you need a point on the north side of the cylinder face, you can calculate it by projecting a point lying further north down onto the cylinder face, for instance.

**Response:** Unfortunately, I came to the same realization.
Using a point that is just somewhere north of the pipe in the original pdf would work perfectly for that instance, but it would not always be the case.
I realize now I should have given some deeper context and thought out the question more thoroughly.
To elaborate...

In general, this is only needed in instances where the pipe is connecting into an elbow, as shown above.
When it does so, I need to get the side of the pipe that is on the corresponding longer radius side of the elbow it is connecting into.
Here is a clearer picture with more examples:

<center>
<img src="img/pipe_cylinder_sides_needed.png" alt="Pipe cylinder sides needed" width="500"> <!-- 1714 -->
</center>

I suppose the real question is, how to get the longer radius side of an elbow fitting.

I have tried a few approaches thus far... first bounding boxes, then connectors, and now geometry.
But haven't yet yielded any consistent results due to the variably of the instances. 
 
**Answer:** Oh, but that is pretty easy, though.

Of course, there are innumerable ways to approach and solve it.

Here is the first that comes to mind:

The elbow has two connectors, and they each have a connection point and a direction pointing out of the elbow, say, `p`, `q`, `v` and `w`. These two points and vectors define two infinite lines. The two lines intersect somewhere, let's say in `r`. The intersection point is the same as the intersection point of the two connected pipes location lines, extended to meet. The outside edges of the two pipes lie on the 'outside' of `r`, where the 'inside' of `r` is defined as the part of the plane spanned from `r` by all positive linear combinations of `v` and `w`.

I hope you get the idea. Here is a sketch to clarify:

<center>
<img src="img/pipe_cylinder_sides_positive.png" alt="Positive sides of an elbow" width="191">
</center>

Please excuse my poor digital drawing skills.
