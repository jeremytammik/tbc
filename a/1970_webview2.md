<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- wv2
  https://autodesk.slack.com/archives/C0SR6NAP8/p1666290375135259
  Michael Dewberry, 20 Oct at 20:26
  Best practices for Revit add-ins using WebView2

- get ref plane in element
  https://autodesk.slack.com/archives/C0SR6NAP8/p1663057482687699

- Remove Revisions on Sheets
  https://forums.autodesk.com/t5/revit-api-forum/remove-revisions-on-sheets/m-p/11449618

- LandXML P tag
  https://forums.autodesk.com/t5/revit-api-forum/a-question-about-exporting-and-reading-landxml/m-p/11405400

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; Chaining Idling events and other solutions...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### WebView2

####<a name="2"></a> Future of CefSharp and WebView2 for Revit add-ins

Here are some thoughts and experiences and

An internal discussion aimed to define best practices for Revit add-ins using WebView2.

**Question:** I would like to make use of WaebView2 in my add-in.
What should I do?

- Rely on future versions of Revit installing the evergreen runtime?
- Use the same User Data Folder created by Revit for instances of the web view in add-ins?

For add-ins using WebView2 in Revit 2023 and earlier, before Revit itself includes it, is there a consensus on the best place to create a custom UDF?

My interpretation of the `CoreWebView2Environment.CreateAsync` docs are that all WebView2 invocations from the same process need to agree on the `options` object, but do not need to agree on the `userDataFolder`.

A sensible location for the UDF might be a subfolder of *%LOCALAPPDATA%\Autodesk\Revit\Autodesk Revit 2023*
&ndash; this is where Revit is currently creating the CEF cache folder &ndash;
but it's not clear if we should namespace that subfolder name with our add-in name or not. 

**Answer:** You only need to agree on the UDF if you want to use the same main WV2 process, i.e., act like a "tab" in the browser as opposed to starting a new browser "window". 
 The "tab" way &ndash; sharing the UDF and options &ndash; is more performant and uses less memory.

In that case, you need to use the same options as all the other instances.
If you use your own UDF, you are free to set any options you like.

However, we are currently finding WV2 unstable with Revit and may choose not to use it, in which case we will continue using CEF.

<!--

We are seeing what looks like crashes of the rendering subprocesses in some situations like opening certain large files... memory issues? Blocking the main thread for too long?
We don't know yet.
It also crashes after leaving Revit idle overnight.

-->

The worst part is that if one instance of WV2 crashes, all the other instances stop working, even newly created ones after that.
The only remedy is to restart Revit.
Other products' experience with WV2 seems to be fine and I haven't been able to reproduce these issues outside of Revit.
You might want to put your add-in through some tests.

<!--

Actually, if it works for you without problems, we should probably compare notes how we are using and setting up WV2.

-->

In Revit, we are working on a separate component that isolates the browser code from Revit and I would like all our uses to go through that.
It supports both CEF and WV2, so it's easy to switch which one we want to use.
Among other things, it takes care of setting up the UDF so they are all in the same location.

The development builds of Revit include both WV2 and the evergreen runtime.
In fact, it was put into Revit even before we started the migration; looks like the SSO component uses it now.

Dynamo is currently fully committed to WebView2 from v2.17, so we are testing its stability there as well.

**Response:** If there is a push to revert back to cefsharp, does this mean future releases won't have wv2?

 **Answer:** No, I think we will still be shipping both CefSharp and WV2 for some time, since our component needs both even if we're using just one of them.
 We will try to switch again once WV2 is more stable or once we have more time to figure out what's going on.
If anything, we might stop shipping CefSharp at some point. 

Also, WV2 comes included in Win11 by default and can't be uninstalled (I think...), so that will be no issue hopefully soon.

####<a name="3"></a> Retrieve Reference Plane in Element

Some clarification on how symbol and instance geometry is generated from the family definiton:

**Question:** How can I retrieve a reference plane that is inside an element?
I prefer to avoid opening and analysing the family file.
Is it possible to get the reference plane directly from the element via Revit API?

**Answer:** You don't need to open the family.
If there is a placed instance, you can call `GetReferenceByName` on that instance, or get all the references from it.

**Response:** I am able to get the reference using 

Reference reference=famInst.GetReferenceByName("Center (Left/Right)");

Now, how to get the reference plane from the reference?

**Answer:** Use `GetGeometryObjectFromReference`.

**Response:** That throws an exception saying that I cannot convert `GeometryObject` to `ReferencePlane`:

<center>
<img src="img/cast_geometryobject.png" alt="Cast error" title="Cast error" width="1000" height=""/> <!-- 1168 x 37 -->
</center>

**Answer:** I think the problem is that you're actually getting back a surface instead of a plane, so your cast fails.
try casting to a surface, or, better yet, check out the underlying type in Visual Studio.

A reference in an element is not a ReferencePlane element.
In the family definition, a ReferencePlane element is added, but in the family symbol and instance, all geometry generated is consumed into the single element.  
Therefore, you are receiving a Surface or more specifically a Plane, which gives you the location of the plane.
If you want to attach a dimension to it, use the Reference you get originally.
You don't have access to a separate element from this.

####<a name="4"></a> Remove Revisions on Sheets

[Remove Revisions on Sheets](https://forums.autodesk.com/t5/revit-api-forum/remove-revisions-on-sheets/m-p/11449618)

 rhanzlick 1171 Views, 6 Replies
‎2021-02-01 11:13 AM 
Remove Revisions on Sheets
I am writing an addin to edit revisions for many sheets simultaneously. I see in the API that revisions can be added to a particular sheet using the 'SetAdditionalRevisionIds' method. However, I don't see an obvious way to remove revisions currently on a sheet. (This is all assuming we are modifying revisions that were manually added and not controlled by any content on the sheet eg revision clouds.) Is there a way to remove revisions from a sheet in the API?

Any help would be much appreciated.

Thanks,

Ryan

 Solved by TripleM-Dev.net. Go to Solution.

Tags (0)
Add tags
Report
6 REPLIES 
Sort: 
MESSAGE 2 OF 7
TripleM-Dev.net
 Advisor TripleM-Dev.net in reply to: rhanzlick
‎2021-02-01 12:51 PM 
Hi,

Use GetAdditionalRevisionIds  , these are the revisions not created by revisionclouds on the sheet or in any of the placed views.

From the returned collection remove the id's of the revisions that need removing.

Then set SetAdditionalRevisionIds  with the edited collection from above.

ps: GetAllRevisionIds will return ALL revisions, from Revision clouds and "Revision on sheet" checkboxes.

conclusion: GetAllRevisionIds - GetAdditionalRevisionIds = RevisionIds set by revisionclouds.

Note deleting (and adding) Revisionclouds only possible if the Issued state of the revision is off.

This is not needed for SetAdditionalRevisionId (like in UI).

- Michel

Tags (3)
Tags:GetAdditionalRevisionIdsGetAllRevisionIdsrevision
 
Add tags
Report
MESSAGE 3 OF 7
rhanzlick
 Enthusiast rhanzlick in reply to: rhanzlick
‎2021-02-01 02:26 PM 
I thought I tried this method, and it made no changes (the revisions were set to 'Issued'). However, I must have done something wrong the first time, because when I reimplemented, it works great!

Thanks,

Ryan

Tags (0)
Add tags
Report
MESSAGE 4 OF 7
tc
 Participant tc in reply to: rhanzlick
‎2022-09-28 02:40 AM 
Dear fellow coders,

This week I had to solve a similar problem. Another way to approach this matter is by using the FilteredElementCollector. Assuming everything related to all the revisions on a sheet will be deleted, you can break it down in steps. First delete all revision cloud tags, secondly delete the revision clouds and finally the revisions. Below you can see an example. To complete the routine I also added a bit of code to avoid conflicts with revisions that are already issued. Hopefully this will help somebody for future use.

Dim revs As New SortedList(Of String, Boolean)
Dim colRev As New FilteredElementCollector(doc)

For Each r As Revision In colRev.OfCategory(BuiltInCategory.OST_Revisions)
   revs.Add(r.UniqueId, r.Issued)

   r.Issued = False
Next

Dim colRevClTags As New FilteredElementCollector(doc, sheet.Id)
doc.Delete(colRevClTags.OfCategory(BuiltInCategory.OST_RevisionCloudTags).ToElementIds)

Dim colRevCl As New FilteredElementCollector(doc, sheet.Id)
doc.Delete(colRevCl.OfCategory(BuiltInCategory.OST_RevisionClouds).ToElementIds)

Dim colRevs As New FilteredElementCollector(doc, sheet.Id)
doc.Delete(colRevs.OfCategory(BuiltInCategory.OST_Revisions).ToElementIds)

For Each r In revs
   Dim rev As Revision = doc.GetElement(r.Key)

   rev.Issued = r.Value
Next

Kind regards,

Tim

Tags (0)
Add tags
Report
MESSAGE 5 OF 7
rhanzlick
 Enthusiast rhanzlick in reply to: tc
‎2022-09-28 08:19 AM 
Wow thanks for the solution, but yours goes above and beyond my initial question! I'll convert yours to C#, and try it out when I have some time available.

Tags (0)
Add tags
Report
MESSAGE 6 OF 7
tc
 Participant tc in reply to: rhanzlick
‎2022-09-29 02:02 AM 
Little correction. I just edited the code fragment I posted yesterday. Recovering the issued state of a revision doesn't seem to work by using the element id. Using the stable unique id instead fixes the problem.

Cheers!
Tags (0)
Add tags
Report
MESSAGE 7 OF 7
jeremy.tammik
 Employee jeremy.tammik in reply to: tc
‎2022-09-29 04:37 AM 
Dear Tim,

Thank you for sharing this very clear and effective direct approach.

I am a bit surprised. I would have expected each successive call to OfCategory to be added to all existing previous filters. Therefore, calling it twice with different categories ought to return zero elements, and thrice even more so. Every filtering call on a given collector narrows down the search criteria further. "You need to reinitialise a filtered element collector before reusing it. All the filters that you add to it are accumulated. If they are mutually exclusive, you will get zero results."

https://thebuildingcoder.typepad.com/blog/2019/11/design-automation-api-stacks-collectors-and-links....

So, I am surprised that you can reuse the one single collector in the way you show.

But hey, if it works, so much the better.

Congratulations and best regards,

####<a name="5"></a> LandXML P Tag

LandXML P tag
https://forums.autodesk.com/t5/revit-api-forum/a-question-about-exporting-and-reading-landxml/m-p/11405400

 bim06KBNK9 50 Views, 2 Replies
‎2022-09-07 12:56 AM 
A question about exporting and reading LandXML
 
<P id="1">Y   X  Z</P>
This may be a bit off-topic. But I wonder about exporting toposurface to LandXML.
Why the order of collocation is (Y X Z) and not (X Y Z)

https://thebuildingcoder.typepad.com/blog/2010/01/import-landxml-surface.html

Untitled.png
Screenshot_5.png

Tags (0)
Add tags
Report
2 REPLIES 
Sort: 
MESSAGE 2 OF 3
jeremy.tammik
 Employee jeremy.tammik in reply to: bim06KBNK9
‎2022-09-07 01:05 AM 
Yes. That is sort of off-topic. Not just here, but everywhere in the universe. There are probably reasons for that definition, but who cares? Are you planning to change the LandXML definition? If so, then you might want to discuss this with the people responsible for it, maybe here:

http://landxml.org

In general, when I am programming something that connects with something else, I have to accept the given conditions and adapt to them. It may help to know the underlying reasons, but only in theory, for my acceptance and motivation. If I can accept the facts and motivate myself regardless, there is no need to understand the underlying reasons. Actually, that applies to every aspect of life. Actually, to death as well: "Ours is not to question why; ours is but to do or die."

Jeremy Tammik,  Developer Advocacy and Support, The Building Coder, Autodesk Developer Network, ADN Open
Tags (0)
Add tags
Report
MESSAGE 3 OF 3
jeremy.tammik
 Employee jeremy.tammik in reply to: bim06KBNK9
‎2022-09-07 01:09 AM 
Actually, I do have an answer after all. Look at the LandXML specification for the P tag:

http://www.landxml.org/schema/LandXML-1.2/documentation/LandXML-1.2Doc_P.html#Link07F5D020

> A surface point. it contains an id attribute and a space delimited "northing easting elevation" text value.

**Question:**

**Answer:** 

**Response:** 

<pre class="code">

</pre>




