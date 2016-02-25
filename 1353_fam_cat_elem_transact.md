<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- email [Revit API] Juan Carlos Garza Martinez , Cristina Jianu , Angel Velez 

- http://forums.autodesk.com/t5/revit-api/general-workflow-for-keeping-track-of-elements/m-p/5800013

- http://forums.autodesk.com/t5/revit-api/transactions-and-document-events/m-p/5782514

- updated:
  https://github.com/jeremytammik/RevitLookup [RevitLookup] GetAnalyticalModel returns null (#10)
  https://github.com/jeremytammik/AdnRevitApiLabsXtra
  https://github.com/ADN-DevTech/RevitTrainingMaterial
  https://github.com/jeremytammik/GetRevisionData

- 11039110 [Drawing revision cloud with our custom design]

- updated ADN labs xtra already previously, so two updates

Family Category Element Ids Transaction Undo RevitLookup Updates #revitapi #adsk #adsklabs #3dwebcoder #bim #aec
#fsharp
#restapi #python
#adskdevnetwrk #dotnet #csharp
#geometry
#grevit
#responsivedesign #typepad
#ah8 #augi #au2015 #dotnet #dynamobim
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce

#Markdown, #Fusion360 #Fusion360Hackathon, Revisions and Bulk Upgrade #revitapi #adsk #3dwebcoder #aec #bim

Revit API, Jeremy Tammik, akn_include

Lots of topics from Revit API discussion forum threads and GitHub updates:
Family category; Keeping track of elements; Undoing a transaction; RevitLookup, GetRevisionData and ADN Revit API Training Labs update
-->

### Family Category, Element Ids, Transaction and Updates

Lots of topics to discuss, all from
[Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) threads or GitHub updates:

- [Family category](#2)
- [Keeping track of elements](#3)
- [Undoing a transaction](#4)
- GitHub updates:
    - [RevitLookup update](#5)
    - [The Revision API and GetRevisionData update](#6)
    - [ADN Revit API Training Labs update](#7)


#### <a name="2"></a>Family Category

Recently, several people encountered issues retrieving the category of a family.

This is a known issue.

The Element.Category property is often not implemented for family definitions.

The simplest solution is to use the Family.FamilyCategory property instead.

Another solution is to retrieve any of the symbols &ndash; also know as types &ndash; defined by the family and ask it for its category instead.

Here are two threads discussing related issues:

- [Trouble getting families with specific category](http://forums.autodesk.com/t5/revit-api/trouble-with-getting-families-from-specific-category-element/m-p/5789090)
- [Family categories on project](http://forums.autodesk.com/t5/revit-api/family-categories-on-project/m-p/5778749)

The first one answers this very question and points to several example solution implementations.


#### <a name="3"></a>Keeping Track of Elements

Cwaluga raised a thread to discuss the
[general workflow for keeping track of elements](http://forums.autodesk.com/t5/revit-api/general-workflow-for-keeping-track-of-elements/m-p/5800013),
and Arnošt Löbel very kindly answered in depth:

**Question:**
I am not sure what is the best way to accomplish what I intend to do.

I am working completely integrated in Revit, so no import/export/worksharing issues for the moment.

However, I have to keep track of the elements.

Now if I understand correctly, it is discouraged to rely on Element IDs, since those may not be unique in linked scenarios and may even change within a document. Instead it is recommended to use Unique IDs for this, but the API makes working with these not easy. Moreover, if I want to obtain an Element from an ID, I have to know the document as well. I could now always check all documents if they contain an element or always store a reference to the document alongside with the ID. I could however also work directly with the elements. They know which document they belong to and in case I need something from the element, I do not have to create one from the ID first. However, if I recall correctly, I read some time ago that this may be inefficient. Is that so?

To put it in one short question: Is it all right to excessively keep references of elements around? If not: why? And is there a better way?

**Answer:**
Keep an eye on the trends set by the Revit API implementation itself.

It is consistently moving away from passing elements as arguments, and using element ids instead.

Therefore, I would agree that what you are doing is best:

- When storing stuff for a duration exceeding the length of one single Revit transaction, use unique ids.
- When storing element information just for passing around within one transaction, use element ids.
- Only access the element itself when you need to.

**Response:**
Thanks so much for your suggestions, Jeremy. I'll try to avoid keeping the elements alive more than necessary. But there are some points revolving around this, which are not obvious from the documentation.

If I may add two short follow-up questions:

- Is there any way to obtain a UniqueId from an ElementId without instantiating the element first?
- How expensive is the element instantiation really? Would a good rule of thumb be that unless you are in a part of your code that needs frequent element access, you are better off with using doc.GetElement whenever needed?

**Arnošt replies:**
I'd say it's fairly all right to store and work with Elements directly, in the public managed API anyway. We've made quite an effort to make element objects stable no matter how (and if) the model from which the elements are changes. The same does not apply internally in Revit native code where we need to be more cautious of memory re-allocation, which is why, internally, we generally prefer working with Ids. That general approach mirrors in the public API, for our goal is to have a public API that is as close to our internal API as possible.

So, my general recommendation would be:

- If there is work-sharing involved, stick with unique Ids;
- If work-sharing is not involved (and if you can somehow guarantee that), you can work with either elements directly or with Element Ids (or Unique Id, naturally), whichever way you prefer and are more comfortable with.

There is one point of caution: I've stated that we had made elements quite stable. That is certainly true for most kinds of element, but not for all, unfortunately. There are (I believe) still a few older kinds of elements that do not yet have an “ironclad” wrapper around them. Basically, if an element class does not have the IsValidObject method in their class, those elements would not be safe to hold on to, because in case the actual elements are deleted (or undone, or redone, of if their document got closed), the managed object would not know about it and any operation performed on it would lead to a crash. So, if you store elements make sure it's the kind that implements the IsValidObject method, and grow the habit of always testing that method before every use of the element.

As for performance impact, there are, again, a few points I'd like to make:

- There is practically no performance degradation from fetching an element by its Id.
- Naturally, element Ids are smaller to store – they hold only an integer, while most Elements (the managed objects) contain at least two pointers and some flags also.
- Once you have an element (in the API), there will be no slowdown or memory impact. It is because once an element is read and brought in to memory, it stays there until its document is closed or the element is deleted in some work-shared action. However, there are plenty of methods that give you element Ids of elements that have not been completely read from the memory yet. Revit is pretty savvy (some may say lazy) about what part of an element is needed and which can be deferred until later. So, if an API application is given a list of elements and immediately fetches the corresponding elements so it could store them, that application may in fact have negative impact on Revit performance, because Revit would have to completely read those elements and have them in memory.

Oh, and to answer some of the questions Cwaluga asked:

- No, it is not possible to obtain Unique Ids from a given Element Id.
- Element instantiation (the API managed object) is not so expensive. However, as I pointed above, it would be expensive if the elements has not been completely read and brought to memory yet. We might have its Id already, but if the element is not completely available yet, we'd need to read it from the file, allocate native memory for it (which may be significant), and then allocate the public API object for it.


#### <a name="4"></a>Undoing a Transaction

Troy raised another interesting question on undoing a transaction, in his thread on
[transactions and document events](http://forums.autodesk.com/t5/revit-api/transactions-and-document-events/m-p/5782514).

**Question:**
I am trying to figure out a way to undo a previous transaction that I have committed from a future transaction.

Backstory: I am doing some stuff during the DocumentPrintingEventArgs event and then allowing the print to happen. Then I am reverting all the stuff I did after the print using the DocumentPrintedEventArgs event as I don't want the changes to be permanent.

This is taking a lot of code to do, plus the larger/more complex the model, the longer it takes the code to run. I was wondering if I could roll back the previous transaction that happened during the PrintingEvent from the PrintedEvent? Or is it possible for a transaction to span these two events?

**Answer:**
The only possibility that I know of that remotely approaches what you are asking for is to wrap all of your transactions in a transaction group, commit the transactions that you need to commit, and then roll back the entire transaction group without committing it.

However, I do not know whether it is possible to launch a call to print within an open transaction group. I suspect not.

Here are two explanations of
[transactions, sub-transactions, transaction groups](http://thebuildingcoder.typepad.com/blog/2013/04/transactions-sub-transactions-and-transaction-groups.html) and
[using transaction groups](http://thebuildingcoder.typepad.com/blog/2015/02/using-transaction-groups.html).

That covers about all that you can achieve in this area programmatically.

If you really have to launch the print command, and that cannot be achieved within your own open transaction group, then I guess you would have to take recourse to the Revit Undo command instead, to roll back the already committed transactions.

I do not know whether the Undo command can be driven programmatically, e.g. using the
[PostCommand method](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.3).

If all else fails, how about saving your document before printing it, creating a copy for the complicated printing process you describe, and then reverting back to the original document once that is done?

Please
[KISS](https://en.wikipedia.org/wiki/KISS_principle), always and everywhere!

**Arnošt replies:**
I am afraid there is no solution to what you want to do. There is no tie between the pair of events (pre and post printing) and any transactions that happen during either of the events are unrelated.

Revit API does not allow undoing transactions for a reason: mainly to avoid external application to block each other. However, even if undoing was allowed, it is unlikely to help in cases like yours, because you can never be sure that your transaction was the latest finished and it is impossible to undo transaction that are further down the undo stack (without undoing later transactions, of course). There may be number of application subscribed to printing events, and it may be different applications subscribed to each of the two events. Any of the applications in any of the events can have multiple transactions that they would not like to be redone by some other application. There is simply no way around that problem.

Jeremy mentioned using transaction groups to wrap your transactions in, but that is not an option in this case either. For your transactions happen during an event (not a command) we do not allow any even handler to leave any transaction-related scope (such a transaction, transaction group, etc.) open upon returning from the handler. If Revit sees such a scope left open, it forces it to close and then deletes everything that the handler has done to the model.

Like I said, there is really nothing that can be done. It's not like we haven't thought about it, but considering all the implications we cannot provide a save and stable solution.




#### <a name="5"></a>RevitLookup Update

[Maxence Delannoy](https://github.com/mdelanno) and
[BobbyCJones](https://github.com/BobbyCJones) discovered
an exception in RevitLookup due to the GetAnalyticalModel property returning null in RAC and MEP.

Maxence fixed it and also removed the unused default constructor in the GitHub pull request
[GetAnalyticalModel returns null #10](https://github.com/jeremytammik/RevitLookup/pull/10).

Here are
[the modifications he made](https://github.com/jeremytammik/RevitLookup/pull/10/files#diff-0).

I integrated Maxence's enhancements into the
[RevitLookup master branch](https://github.com/jeremytammik/RevitLookup) in
[release 2016.0.0.10](https://github.com/jeremytammik/RevitLookup/releases/tag/2016.0.0.10).


#### <a name="6"></a>The Revision API and GetRevisionData Update

The following query on how to draw a revision cloud prompted me to update the
[GetRevisionData add-in](http://thebuildingcoder.typepad.com/blog/2014/06/the-revision-api-and-a-form-on-the-fly.html):

**Question:**
I would like to develop a Revit add-in providing functionality similar to Revit's built-in revision cloud with some custom stuff added. Does the Revit API provide the facility to draw revision clouds, or to drawing sketches of them with our custom operator?

**Answer:**
An entire new Revision API was implemented in Revit 2015, so
[What's New in the Revit 2015 API](http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html) section on the
[Revision API](http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html#3.04)
covers just about all there is to know.

You may also be interested in the
[GetRevisionData Revit add-in](http://thebuildingcoder.typepad.com/blog/2014/06/the-revision-api-and-a-form-on-the-fly.html).

I just upgraded the
[GetRevisionData GitHub repository](https://github.com/jeremytammik/GetRevisionData) to Revit 2016 for you, in
[release 2016.0.0.1](https://github.com/jeremytammik/GetRevisionData/releases/tag/2016.0.0.1).



#### <a name="7"></a>ADN Revit API Training Labs Update

The
[ADN Revit API Training material](https://github.com/ADN-DevTech/RevitTrainingMaterial)
HelloWorld module in [C#](https://github.com/ADN-DevTech/RevitTrainingMaterial/blob/master/Labs/1_Revit_API_Intro/SourceCS/1_HelloWorld.cs) and([VB](https://github.com/ADN-DevTech/RevitTrainingMaterial/blob/master/Labs/1_Revit_API_Intro/SourceVB/1_HelloWorld.vb) both implement the HelloWorldApp external command.

Among other things, it demonstrates the result of returning `Result.Failed`, which causes Revit to display an error message to the user and highlight the returned elements graphically &ndash; presumably to show the user that they are the ones causing an issue.

This command was returning `Result.Succeeded` instead, so I fixed that and published it in
[release 2016.0.0.9](https://github.com/ADN-DevTech/RevitTrainingMaterial/releases/tag/2016.0.0.9).

At the same time, I also synchronised the official ADN Revit API Training material with my
[ADN Revit API Xtra labs](https://github.com/jeremytammik/AdnRevitApiLabsXtra), publishing
[release 2016.0.0.9](https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2016.0.0.9) of those as well.

