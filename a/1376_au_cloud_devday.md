<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #adsklabs #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#rtceur


Revit API, Jeremy Tammik, akn_include

Connecting Desktop and Cloud at AU and DevDays #revitapi #bim #aec #3dwebcoder #adsk #geometry #au2015 #3d #apis

I am still working on my Autodesk University preparations, and, as always, lots of Revit API cases, both from ADN and Revit discussion forum threads. Let's look at the current state of affairs regarding AU, DevDays, and two of those threads
&ndash; AU 2015 &ndash; Connecting Desktop and Cloud for AEC
&ndash; Regeneration invalidates all geometry
&ndash; Autodesk DevDays 2015 and Cloud Accelerator Week...

-->

### Connecting Desktop and Cloud at AU and DevDays

I am still working on my Autodesk University preparations, and, as always, lots of Revit API cases, both from ADN and [Revit discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) threads.

Let's look at the current state of affairs regarding AU, DevDays, and two of those threads:

- [AU 2015 &ndash; Connecting Desktop and Cloud for AEC](#2)
- [Regeneration invalidates all geometry](#3)
- [Autodesk DevDays 2015 and Cloud Accelerator Week](#4)


#### <a name="2"></a>AU 2015 &ndash; Connecting Desktop and Cloud for AEC

We decided that my topic <b><i>Connecting Desktop and Cloud</i></b> will be of interest at the AEC Booth at AU as well, so we plan to set up and demo the projects that I discuss during my
class [SD11048](https://events.au.autodesk.com/connect/dashboard.ww#loadSearch-searchPhrase=SD11048&searchType=session&tc=0) there also.

I presented part of the session content to my colleagues to discuss the installation details and posted the recording in a 36-minute YouTube video on [AU 2015 Connecting Desktop and Cloud for AEC](https://youtu.be/fTDr1Tcn4QI):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/fTDr1Tcn4QI?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

Since the presentation went so well and is entirely based on live demos in Revit and on web sites, existing blog posts, documentation and source code in GitHub repositories, I plan to reduce my slide deck to one single page, saying:

<center>
<div style="border: 1px solid; color: orange">
<p style="font-size: 150%; font-weight: bold">Slides are for Wimps</p>
<p>Real developers put it on the web.</p>
</div>
</center>

... followed by a pointer to the handout document and a list of URLs.

I hope the audience and you agree &nbsp; :-)

I also updated the various GitHub repositories of the projects we demonstrate, adding installation instructions to each:

- Room Editor
    - [RoomEditorApp Revit add-in](https://github.com/jeremytammik/RoomEditorApp)
    - [Roomedit CouchDB database app](https://github.com/jeremytammik/roomedit)
- FireRating in the Cloud
    - [FireRatingCloud Revit add-in](https://github.com/jeremytammik/FireRatingCloud)
    - [Fireratingdb node.js web server driving a MongoDB database](https://github.com/jeremytammik/firerating)

One cool aspect is that all the so-called cloud-based components can be run either locally or on the web.

If they are local, you obviously need to set up the appropriate infrastructure, such as
[CouchDB](http://couchdb.apache.org), [Node](https://nodejs.org) and [MongoDB](https://www.mongodb.org).

That gives you privacy and peace of mind, once it is running.

Alternatively, you can host all the 'cloud' components in free trial sandbox servers on the web instead, installing nothing at all yourself, which provides peace of mind in another way.

Back to the Revit API and an interesting question that was &ndash; once again &ndash; finally resolved by Arnošt Löbel:


#### <a name="3"></a>Regeneration Invalidates All Geometry

Here is an issue that Scott Wilson has been struggling with for a while and brought up in several threads, e.g., suspecting
an [API bug in Document.Regenerate](http://forums.autodesk.com/t5/revit-api/api-bug-in-document-regenerate/m-p/5912740)
and struggling with [invalid face references after Regenerate](http://forums.autodesk.com/t5/revit-api/invalid-face-references-after-regenerate/m-p/5893667):

**Question:** Has anyone else had the problem where a valid PlanarFace of a wall becomes unusable for family creation after calling Document.Regenerate within a dynamic updater. If I pass in a reference to the face while creating a line-based family I get an error that says the instance can't be created on this face. If I just pass in the face itself I get a message saying that the geometry object doesn't contain a reference and that I should set compute references when extracting faces from an element. The thing is, I did use compute references and have what look to be valid references. I even tried creating a stable reference string before calling regenerate and then parse the stable reference string to recreate the reference after the regen then pass that into the creation method and still get the same error. If I remove the regen call the problem goes away.

I have done some more investigation and have kind-of narrowed it down to a bug in the API (I say kind-of because although I can watch it happen and recreate it reliably in my production code I am as yet unable to reproduce it in isolation using minimal example code that would be suitable for posting here). Whatever is happening is definitely triggered by Document.Regenerate though, which is why I am blaming the API.

The basic outline of the problem is as follows:

- I obtain the PlanarFace that was created in a wall by the opening cut of a door instance and store it in a variable (specifically the head face which has face normal of 0, 0, -1).
- I then place some line-based family instances on some other wall faces created by the same door without any problems.
- I call Document.Regenerate.
- I then attempt to place a line-based family instance on the head face and CreateFamilyInstance fails.
- If I remove the Regenerate call and try again it all works perfectly.
- Stepping through with the debugger I put a watch on both the Normal and Origin property of the PlanarFace object and verified that they remained unchanged all of the way through until after the Document.Regenerate call where the values for both instantly changed. It's as if the object now references a completely different face.

Another strange behaviour is that if I call Document.Regenerate before obtaining the Face objects, then subsequent calls do not trigger the bug; it seems to only happen for the first regen call within a transaction.

**Answer:** This is not a bug.

This is simply a proof that you cannot store a PlanarFace.

How do you want to achieve that?

It is not possible.

Revit geometry is read-only, and represents a view of the parametric BIM, a momentary snapshot.

As soon as you add something to the wall, e.g. the family instances you mention, the wall is affected.

Regenerate creates a new snapshot of the current situation, and the planar face that you previously "stored" (I wish I had triple double quotes here) is in blissful oblivion.

I hope this clarifies.

**Arnošt confirms:** It is not a bug; Jeremy was correct. Faces are not guaranteed to survive regenerations; they are not elements. Thus it is not surprising that you cannot use your 'held to' face anymore after you regenerate the model. In fact, it is possible that Revit will recreated faces even if they were not changed, technically.

So, it leavers two options:

- You need to re-fetch the faces you need based on some criteria, e.g. their location.
- You store a reference object to the face instead of the face itself. Then you use the reference (or its 'stable reference') to obtain the face it refers to. If it still exists you ought to get it.


#### <a name="4"></a>Autodesk DevDays 2015 and Cloud Accelerator Week

The annual, global &ndash; and free &ndash; Autodesk DevDays events will take place around the globe from November 2015 through to January 2016. In order to give developers more opportunity and to provide one-on-one help building apps using Autodesk web services, we are changing the format this year.  DevDays will consist of week-long events including the traditional DevDay event on the first day followed by an optional &ndash; and highly recommended &ndash; 3-4 day (depending on location) abbreviated form of our extremely popular [Autodesk Cloud Accelerator](http://autodeskcloudaccelerator.com/autodesk-cloud-accelerator) program.

Attendees at DevDays will discover Autodesk Cloud Services &ndash; the technology and business platform for a new generation of connected Cloud, Web and Mobile apps and based on our web services such as View &amp; Data, AutoCAD I/O, Fusion 360, BIM 360, ReCap 360, and InfraWorks 360. They will also see and learn about the upcoming enhancements in the capabilities and APIs of the next Desktop platform releases.

The Accelerator workshops during DevWeek are an unparalleled opportunity for developers to learn and work intensively on their chosen project with help, support and training from Autodesk Cloud Engineering experts.  By investing a few days now they'll save precious development time and accelerate their project development.

DevDays is a traditional road show to a number of cities worldwide for the benefit of members of the Autodesk Developer Network who can register through [autodeskdevdays.com](http://autodeskdevdays.com).  However, we're happy to hear from and help innovators, forward thinkers and companies just getting started, so if you have a partner who is not an ADN member and would be interested in this event, we encourage them to contact us at (mailto:devdaysinfo@autodesk.com).  View the information, locations and schedule at [autodeskdevdays.com](http://autodeskdevdays.com).  Please note that material demonstrated and discussed at DevDays is confidential and potential attendees will be asked to sign a Non-disclosure Agreement to participate.

<center>
<img src="img/devdays_2015.png" alt="DevDays 2015" width="384">
</center>

The largest single DevDay event will take place &ndash; as usual &ndash; the day
before [AU 2015](http://au.autodesk.com/las-vegas/overview)
in [Las Vegas](http://autodeskdevdays.com/las-vegas).
I'll be there for that, of course, and also attend
the [main European DevDay + Accelerator](http://autodeskdevdays.com/munich-3) in Munich during the week of January 18-22, 2016.
I'm very much looking forward to spending some more quality time with European developers.
I am sure we will have a lot of fun and be very productive as well.
