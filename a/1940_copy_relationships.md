<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- ACS Integration Partner Summit 2022
  https://forge.autodesk.com/blog/acs-integration-partner-summit-2022
  registration
  https://autodesk.registration.goldcast.io/events/636f754d-f617-4a4f-8fa9-38108c6f19d7

- maintain relationships copying elements
  PhaseCreated & PhaseDemolished after using CopyElements()
  https://forums.autodesk.com/t5/revit-api-forum/phasecreated-amp-phasedemolished-after-using-copyelements/m-p/10964247

- Unsplash
  https://unsplash.com
  Unsplash has over a million free high-resolution photos. Explore these popular photo categories on Unsplash. All photos here are free to download and use under the Unsplash License.

twitter:

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### ACS Summit and Copy Relationships

####<a name="2"></a> ACS Integration Partner Summit 2022

Are you interested to learn more about Autodesk Construction Cloud products and make use of 
the [Autodesk Construction Cloud (ACC) APIs](https://forge.autodesk.com/apis-and-services/autodesk-construction-cloud-acc-apis)?

If so, you are invited you to join the [ACC Integration Partner Summit 2022](https://autodesk.registration.goldcast.io/events/636f754d-f617-4a4f-8fa9-38108c6f19d7),
a virtual event on March 17, 2022.
Your Autodesk hosts will be Jim Lynch, SVP & GM of ACS, Josh Cheney, Senior Manager of Strategic Alliances, Jim Gray, Director of Product, ACS Service Infrastructure, and Anna Lazar, Strategic Alliances & Partnerships.

For more information about the event, pleasze refer to the community blog article
on [ACS Integration Partner Summit 2022](https://forge.autodesk.com/blog/acs-integration-partner-summit-2022),
or you can jump directly to
the [registration form](https://autodesk.registration.goldcast.io/events/636f754d-f617-4a4f-8fa9-38108c6f19d7).

####<a name="3"></a> Maintain Relationships Copying Elements

Returning to the pure .NET desktop Revit API, some interesting aspects of maintaining relationships between elements were discussed in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [PhaseCreated and PhaseDemolished after using `CopyElements`](https://forums.autodesk.com/t5/revit-api-forum/phasecreated-amp-phasedemolished-after-using-copyelements/m-p/10964247):

**Question:**

 FabioLorettiOliveira 69 Views, 4 Replies
‎2022-02-16 08:09 PM 
PhaseCreated & PhaseDemolished after using CopyElements()
Dear Santa,

I know I'm a bit late but hopefully I can get this for my 2022 Christmas.

I'm trying to copy elements from one document into another, which I'm managing fine. The issue I'm facing is that, despite having the origin document phases present in the destination document (same phase names at least), the copied elements' Phase Created and Phase Demolished are not matching the original elements'.

I've checked if the ElementIds returned by CopyElements() come up in the same order as the given ElementIds but unfortunately that was not the case and once the elements are copied I'm struggling to make a connection between origin and destination so I can match the phases.

I'll try and behave this year!

Fabio

 Solved by sragan. Go to Solution.

Tags (0)
Add tags
Report
4 REPLIES 
Sort: 
MESSAGE 2 OF 5
FabioLorettiOliveira
 Advocate FabioLorettiOliveira in reply to: FabioLorettiOliveira
‎2022-02-16 08:12 PM 
Oh, and I've seen this too but it didn't help.

Tags (0)
Add tags
Report
MESSAGE 3 OF 5
jeremy.tammik
 Employee jeremy.tammik in reply to: FabioLorettiOliveira
‎2022-02-17 12:58 AM 
I believe that when you collect a bunch of elements and copy them all together in one single operation, Revit will try to maintain and restore all their mutual relationships in the target database. Therefore, it might help if you add all possible references these elements have to other source database elements to the set of elements to copy. These references will include the element instances themselves, their types, phases, levels, views, materials, and whatever other objects you are interested in. Then you will have to test and see what Revit can do to try to avoid creating duplicates of them in the target database and map them to existing target objects instead.

This behaviour is hinted at in the list of extensible storage features:

https://thebuildingcoder.typepad.com/blog/2011/06/extensible-storage-features.html#7

Jeremy Tammik,  Developer Advocacy and Support, The Building Coder, Autodesk Developer Network, ADN Open
Tags (0)
Add tags
Report
MESSAGE 4 OF 5
sragan
 Advocate sragan in reply to: FabioLorettiOliveira
‎2022-02-17 11:23 AM 
That’s standard behavior for the Revit User Interface.  Elements that are pasted into a view do not get their phases from the copied elements.  Instead, they get their phases from the phase of the view they are pasted into.   For example, if the view pasted into has a new construction phase, that’s the phase that the elements will inherit for their created in phase.

It’s possible to check the phases of every element copied, store those in an array, and then apply the phases to the new elements.  I’ve created an addin that does that, and I’ve posted the code online.  You will have to do some extra work to deal with the fact that you are pasting into a different document, but that should be fairly easy to do.

https://sites.google.com/site/revitapi123/copy-similar-code

Tags (0)
Add tags
Report
MESSAGE 5 OF 5
FabioLorettiOliveira
 Advocate FabioLorettiOliveira in reply to: FabioLorettiOliveira
‎2022-02-21 11:26 PM 
Thanks for the help, @jeremy.tammik and @sragan .

@sragan I was doing exactly what you suggested but I was getting inconsistent results when comparing the elements from the source model against the destination model. I then tried a few different ways and ended up realising that the CopyElements() does return the same order of the given ElementId list, which then solved my problem.

When I collected the elements in my source document I also collected their Phase Created and Phase Demolished and stored all 3 in a list as shown below. This allowed me to compared the output of CopyElements() with that list and apply the settings of each element individually.

List<KeyValuePair<ElementId, KeyValuePair<string, string>>>()

**Answer:** 

####<a name="4"></a> Unsplash with Free Images

I am a fan of open source, the creative commons license, free stuff, good will, sharing and learning together as a community.
Consequently, I share everything I can in public in the hope that it will come in useful for others as well and help make the world a better place.

In a similar vein, a colleague involved in community work pointed
out [Unsplash](https://unsplash.com):

> Unsplash has over a million free high-resolution photos grouped in popular photo categories.
All photos are free to download and use under
the [Unsplash License](https://unsplash.com/license).

<center>
<img src="img/clark_van_der_beken_dtFnCDYHA2Q_unsplash.jpg" alt="Unsplash" title="Unsplash" width="500"/> <!-- 1000 -->
</center>
