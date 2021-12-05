<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- logging with journal vs slog
  https://forums.autodesk.com/t5/revit-api-forum/how-to-stop-slog-file-from-getting-overwritten-removing-previous/m-p/10757827

- on monitoring deleted elements by rpt:
  https://forums.autodesk.com/t5/revit-api-forum/find-the-user-of-the-deleted-element-in-revit/m-p/10743767
  10292440 [Accessing deleted elements]
  https://forums.autodesk.com/t5/revit-api-forum/get-uniqueid-of-deleted-element-within-dynamic-updater-with/m-p/10622152
  CF-2162 [API: DocumentChanging or ElementsDeleting event before element is deleted -- 10292440]

- marking and retrieving a custom element
  https://autodesk.slack.com/archives/C0SR6NAP8/p1638455279133900
  Shane Bluemel 
  Hi folks. We're currently spiking a new feature in the Revit Issues Addin where we create our own view to show additional ACC data within Revit. We've hit a bump around the view ID and wondered whether there's something in the API we haven't spotted that might be able to help us.   
  We essentially want to create our own temporary view within Revit so that we can show ACC issues which are not on the current model loaded in Revit (i.e. from linked files or from a multi-model view only available in ACC). Our current spike creates a view, populates it and deletes it on shutdown. This is all fine. However, if the view is the only one open in Revit on shutdown it'll get saved into the file. This got us thinking that we could just save a default Addins view and look for it on next load. However, we can't find a way to determine the ID that's used for the view so we won't know what we're looking for. Some questions where you may be able to help us:
  Can we create a view with a read-only name (so the user can't edit it and we can search for that)?
  Can we define a Revit View ID using a GUID somehow?
  Is there somewhere we could store the view ID used within the file so that we can retrieve it on load? We considered storing it in our own settings file but that doesn't work if the file gets sent to another user.
  Any help or advice you can offer would be much appreciated.
  Jacob Small
  This is a prime case for extensible storage in my opinion. Make a new schema and save the GUID of the view into it.
  Users will delete that view though, and if it doesn’t file into the project browser correctly there will be push back. Expect to delete and recreate the view often (even mid session).
  Also ensure that we have good product documentation on why this is in the file, and how it can be worked with, and the like. Otherwise we will have a LOT support cases around the feature.
  Shane Bluemel
  Fantastic, thanks Jacob. Yes, documentation and the options we present to the user around how they use this feature are important. We also need to be careful around the default name of the view so that it's purpose is obvious enough. Thanks for the help and advice.

- Do you need to add a `favicon` to your web site?
  If so, it might be a good idea to first check out the definitive edition
  of [how to Favicon in 2021](https://dev.to/masakudamatsu/favicon-nightmare-how-to-maintain-sanity-3al7)
  recommended by the extensive [favicon analysis by iconmap](https://iconmap.io/blog) based
  on their cool [favicon map](https://iconmap.io).

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Logging and Monitoring Deleted Data

####<a name="2"></a> 

**Question:** 

**Answer:** 

**Response:** 

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 902 -->
</center>

<pre class="code">
</pre>

####<a name="3"></a> Monitoring Deleted Elements

Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
adeed some useful thoughts on monitoring deleted elements in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on how to [Find the user of the deleted Element in Revit](https://forums.autodesk.com/t5/revit-api-forum/find-the-user-of-the-deleted-element-in-revit/m-p/10743767):

Yes as Jeremy noted  it is pretty difficult to retrieve information about deleted elements.
You could log the ElementIds against username but there are mainly two issues with that:

- The ElementId may have changed from what you later check against to identify the element from the id (old copy of model).
- The element is only deleted in central during synchronisation and that happens along with a load of other changes which are not monitored by the API.
So, even if a locally generated log says the element was deleted, you don't know if that change was pushed through to central, i.e., Delete > Undo > Synchronise (there is no recording of Undo, just the ability to log the deleted and the synchronisation).

Seems to be a recurring theme that people want to know more about the deleted items; what would be the motivation?
The element is no more...it is an ex-element!
If you don't want something deleted, it seems better for users to run an add-in that marks the element, then either posts a failure or cancels an event when deletion is attempted.

**Response:** The real purpose of tracking is to monitor if the project runs smoothly and the users do their job properly.

Is it possible to track it with some kind of Logging outside Revit DB when something like delete event happens in the central model?
Would it be a good idea to log the unique ID of element instead of Element ID ?

**Answer 1:** If you just want to track whether people are deleting a lot of elements in general, you can very easily implement a `DocumentChanged` event handler that is notified when elements are added, modified and deleted.
The affected element ids are provided.
However, as noted above, no further data is available for the deleted ones.
More on `DocumentChanged` and the more complex and powerful DMU framework in
the [Dynamic Model Updater framework topic group](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.31)
More thoughts and a PoC on tracking model modifications in general is provided by
the [TrackChangesCloud sample](https://thebuildingcoder.typepad.com/blog/2016/03/implementing-the-trackchangescloud-external-event.html).

**Answer 2:** In practical terms you would be using either one of:

- *DocumentChangedEventArgs.GetDeletedElementIds* from Application.DocumentChanged
- *UpdaterData.GetDeletedElementIds* from IUpdater.Execute

You can't get UID from these, since the element is no longer available to look it up on.

I rack my brains to think how you could do this without implicating the wrong person but I think the simple answer is whatever method you use system failures could lead to the wrong person being implicated.

As an extreme example:

Every time Application.DocumentSynchronizedWithCentral occurs, you make a copy of the central and store it somewhere.
By comparison of time stamps or information added to the central, you can tell who synchronised that copy.

However, what if copying failed, leading to a missing record.
Under that circumstance, the last record shows the element missing, but perhaps that was just because they acquired the change from elsewhere via ReloadLatest.

It's a human management issue, no?
If you have two production lines with two people on each and on one of those lines they are pointing the finger at one another, then swap one of those with one from the other production line.
In the end, you'll find the common denominator(s).
Also they have to address the atmosphere and what leads to the person not being open about the mistakes they've made.
There used to be this idea in construction that mistakes are so critical to identify that you have to foster an atmosphere where when somebody makes a mistake they can be open about it.
A 'first mistake = last mistake' atmosphere leads to a situation where the mistakes are left to reveal themselves.

The other issue is that in the end someone deleted it but someone should spot it missing.
If a drawing goes out with a missing column for example, then that is a systemic problem.
There should be more than one person at fault in that scenario, or it points to someone being overburdened with both production and final checking (akin to an overburdened defender in chess).
Any system will fail if it doesn't recognise that humans operating within it are not infallible.
If the error was spotted before there were major consequences, then that is worth recognising as the system working.

As a developer, I would tend to focus on what can be done to enable users not to make such errors.
As noted previously, there are mechanisms to protect things considered unchangeable.
There are even therefore ways of restricting what categories or individual elements a particular user can change.

**Answer 3:** It's not an API solution, but, honestly, I think the best solution is to Pin important things you don't want deleted.
Its kind of a pain, because to move or edit anything that is pinned, you have to unpin it and then re-pin it.

But a user can't delete anything that's pinned without unpinning it first.
That does stop a lot of non-intentional deletions.

Many thanks to Richard and Steve @sragan Ragan for their valuable thoughts and advice.

If you are interested in an earlier in-depth discussion of accessing deleted element data, you can also refer to the thread on hgow
to [get `UniqueId` of deleted element within dynamic updater (with feature request)](https://forums.autodesk.com/t5/revit-api-forum/get-uniqueid-of-deleted-element-within-dynamic-updater-with/m-p/10622152) and
the wish list item *CF-2162 &ndash; API: DocumentChanging or ElementsDeleting event before element is deleted -- 10292440*.


####<a name="4"></a> How to Favicon

Do you need to add a `favicon` to your web site?

If so, it might be a good idea to first check out the definitive edition
of [how to Favicon in 2021](https://dev.to/masakudamatsu/favicon-nightmare-how-to-maintain-sanity-3al7)
recommended by the extensive [favicon analysis by iconmap](https://iconmap.io/blog) based
on their cool [favicon map](https://iconmap.io).

####<a name="5"></a> DAO

Our new word to learn today, this week's word of the week, is DAO
or [decentralized autonomous organization](https://en.wikipedia.org/wiki/Decentralized_autonomous_organization).

Find out all you need to know from
the [Wikipedia link](https://en.wikipedia.org/wiki/Decentralized_autonomous_organization) and
the CNBC article explaining [What are DAOs? Here’s what to know about the 'next big trend' in crypto](https://www.cnbc.com/2021/10/25/what-are-daos-what-to-know-about-the-next-big-trend-in-crypto.html).

####<a name="6"></a> Palindrome and Ambigram

Better late than never, I noticed that I missed
a [special date last week](https://twitter.com/MLKessel/status/1466416660458491913?s=20):

> Not only is today's date a palindrome, it's also an ambigram.
You can read it left to right, right to left, and also upside down.

<center>
<img src="img/20211202.png" alt="Palindrome date" title="Palindrome date" width="200"/> <!-- 398 -->
</center>

