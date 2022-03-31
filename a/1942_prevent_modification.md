<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- preventing model and element modification is tricky...
  Make instance and its parameters NOT editable
  https://forums.autodesk.com/t5/revit-api-forum/make-instance-and-its-parameters-not-editable/m-p/9821547
  https://forums.autodesk.com/t5/revit-api-forum/hide-or-block-explode-import-instance-cads/m-p/10829603

- RevitLookup updates
  https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.4.0
  Minimize, maximize support #134. Fixed problem with sending a print job #133
  
  Allow user maximize form full screen #134
  
  > Enable user to maximize all forms to full screen; useful to display long string data or to  expand the form full size of review 
  
  Fix automatic execute method SubmitPrint #133
  
  > A problem when user snoops to `PrintManager` and invokes the `SubmitPrint` method
  
  Many thanks to 
  [Chuong Ho](https://github.com/chuongmep)
  [Roman 'Nice3point'](https://github.com/Nice3point)

twitter:

RevitLookup, WPF and preventing modification using the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/stopmodify

Different approaches to prevent modification of certain elements and the latest news on RevitLookup
&ndash; Prevent modification
&ndash; RevitLookup updates
&ndash; RevitLookupWpf
&ndash; Pilcrow...

linkedin:

RevitLookup, WPF and preventing modification using the #RevitAPI

https://autode.sk/stopmodify

Different approaches to prevent modification of certain elements and the latest news on RevitLookup:

- Prevent modification
- RevitLookup updates
- RevitLookupWpf
- Pilcrow...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### RevitLookup, WPF, Preventing Modification

Some notes on different approaches to prevent modification of certain elements and the latest news on RevitLookup:

- [Prevent modification](#2)
- [RevitLookup updates](#3)
- [RevitLookupWpf](#4)
- [Pilcrow](#5)

####<a name="2"></a> Prevent Modification

Revit is a design tool and as such targeted at people editing the BIM.

In many situations, orders of magnitude more people might need to view is or query certain properties.

For instance, a couple of dozen people might design an airport, thousands might build it, and millions might end up using it, wandering around and requiring a floor plan or other navigation support to do so effectively.

For such situations, you would use a viewer or other simplified or read-only access to certain aspects, subsets or properties of the BIM.

For read-only access to a model, you might want to consider using a pure viewer instead, first and foremost something globally accessible like the Forge platform.

I [recently mentioned](https://thebuildingcoder.typepad.com/blog/2022/03/drilling-holes-in-beams-and-other-projects.html#3) my
current [RvtLock3r](https://github.com/jeremytammik/RvtLock3r) side project
that ensures that certain specified parameter values have not been modified.

Two other recent queries on how to prevent model modification came up in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) threads
on how to [hide or block explode import instance](https://forums.autodesk.com/t5/revit-api-forum/hide-or-block-explode-import-instance-cads/m-p/10829603)
and how to [make instance and its parameters NOT editable](https://forums.autodesk.com/t5/revit-api-forum/make-instance-and-its-parameters-not-editable/m-p/9821547):

**Question :** At some point at creating a drawing, we have a status of instances in the drawing, that the user/architect is NOT allowed anymore to change ANY parameters in the instance and is NOT allowed to move the instance at all.

I found out that you can pin the elements and make them not selectable anymore:

<pre class="code">
  element.Pinned = true;
  
  var options =  SelectionUIOptions.GetSelectionUIOptions();
  
  options.SelectPinned = false;
</pre>

Also, I could add a Trigger to observe a custom parameter or a built in parameter.

BUT (!) the user can still check that checkbox of the "Select pinned elements" again and I cannot find an Event that observes these user action.

The `DocumentChanged` event is not fired, when I click on that checkbox.

Is there any other option to make instances not editable anymore? Or can I observe the action of the user, when he/she is checking or unchecking the Selection option?

My workaround would be, that if the user would want to change the element again and it has this kind of (locked) status set, to observe this and undo the action. But this is such a dirty workaround I would not be happy with it.

I would have to set a trigger for everything in this element. And in my opinion, this is a bad solution.

**Answer:** Pining the element to prevent change is probably the wrong starting point, since that mechanism is mostly useful for datum elements and links, i.e., the items you more often than not want to prevent movement of.
The 'select pinned items in view' button I believe is more of a user preference than a document change, so I assume that setting would be per user and therefore not stored with the Document.
Additionally, if I pin something, I can still change its parameters.

There is a slow filter 'SelectableInViewFilter' which can be used to determine if something can be selected in view.
So, one approach could be to store the ElementIds of your protected items and run this filter for a view to ensure nothing is missing. This also seems more trouble than it is worth since it is a slow filter, and you'd probably have to run it quite often.

The best approach I know of for change control is to put the protected elements on mirrored worksets:

- GridsAndLevels &rarr; GridsAndLevelsReadOnly
- PrimaryStructure &rarr; PrimaryStructureReadonly etc.

The 'ReadOnly' worksets above would have an owner that isn't a normal user, i.e., you create a fictitious user and have that user take ownership of the worksets.
This was easier in the past when you could change the Revit username.
Depending on your product licensing arrangement, this may still be a viable option for you.
I think this is the best approach, since element ownership through worksets is an inherent aspect of Revit and so you can rely on the infrastructure already in place.

An alternative approach is to implement an `IUpdater`, mark the element with a parameter value or extensible storage and then, if that Element is changed, post a failure with the only option being undo.

**Response:** Thank you for your suggestions &nbsp; :-)

I looked at
the video [BIMedge &ndash; Admin User to Lock out Worksets](https://youtu.be/0KS2nwHWZRQ) that 
shows the Workset stuff that you mean &ndash; the thing is, that I can easily change my account into "Admin" and so every other user could do it like that (change username, etc.).

Another problem I had was that the workset option was greyed out in my document.
But I think I should first make sure what a "Workset" is and how to create and use it.

I already implemented an Updater Trigger for a user parameter (as mentioned in my question).
And then dynamically add another trigger which catches EVERY change of the element (with Element.GetChangeTypeAny) and undo it, if its status is set to our defined "not editable anymore" status.

I just thought that this solution is not nice and thought there must be a nicer and cleaner solution to this.

But it seems there isn't.

So I will implement it like I already planned.

Thanks a lot for your help!!

**Answer:** That's fine; just bear in mind there is no absolute way of preventing a user from editing something, not even with the API. An add-in only enforces something if it is running.

I think there has to be a level of trust that the users will not act inappropriately; additionally, there needs to be a model audit / checking process.

**Response:** Yes, you are right.
If the user wants to commit to our database I would check, if he changed something without the add-in.
The commit can only be done via the add-in.

Users are clever, they always find a way to "cheat" ... :-)

**Answer 2:** The cleanest solution would be to prevent the user selecting the element.
So, program your own "SelectionChanged Event" as described in the thread
on [element selection changed event implementation struggles](https://forums.autodesk.com/t5/revit-api-forum/element-selection-changed-event-implementation-struggles/m-p/9229523).

In the "eventhandler", or, in my solution, the `IsCommandAvailable` method, find the selected elements in the active document `Selection` `GetElementIds`.
If your target Ids are present in the selection, remove them from the selection, effectively blocking the user from selecting the element.

I don't know how time-consuming the removal of id's will be with a large number of target Ids.

**Response:** Thanks for your answer.

The thing is, the user would then see no more information about the element.

So, I think that my first suggestion with pinning the elements and making them not selectable is a very bad idea.

Later: so, I have talked to the support.

Atm there is no way to "lock" an instance in the model...

Unfortunately, the workaround(s) are the only ways to get such behaviour...

Thanks a lot again for your help!

Many thanks to Diana, Richard and Fair59 for the interesting discussion and solutions!

####<a name="3"></a> RevitLookup Updates

Several updates were added
to [RevitLookup](https://github.com/jeremytammik/RevitLookup)
especially by very active contributor [Chuong Ho](https://github.com/chuongmep):

- Fixed problem with sending a print job [#133](https://github.com/jeremytammik/RevitLookup/pull/133):
  A problem when user snoops to `PrintManager` and invokes the `SubmitPrint` method
- Minimize and maximize window support [#134](https://github.com/jeremytammik/RevitLookup/pull/134):
  Enable user to maximize all forms to full screen; useful to display long string data and to expand the form to full size for review 
- Minor UI changes [#135](https://github.com/jeremytammik/RevitLookup/pull/135)

<center>
<img src="img/revitlookup_maximise.png" alt="RevitLookup maximise button" title="RevitLookup maximise button" width="600"/> <!-- 802 -->
</center>

Many thanks to
[Chuong Ho](https://github.com/chuongmep) for these useful enhancements and to 
[Roman 'Nice3point'](https://github.com/Nice3point) for managing the releases.

####<a name="4"></a> RevitLookup in WPF

Chuong Ho or [Hồ Văn Chương](https://chuongmep.com) is very active in other areas as well.

Among many other projects, he is working
on [RevitLookupWpf](https://github.com/weianweigan/RevitLookupWpf):

> Hello everyone,
Today, I would like to introduce to you
the [RevitLookupWpf project](https://github.com/weianweigan/RevitLookupWpf).
This project deeply supports Revit API by me and a Chinese friend is dududu collaborate;
this is one of our efforts to enhance development improvement in the construction industry.
With Revitlookupwpf, you can look into every detail clearly inside and execute it directly, and many more exciting things we are working on.
We are creating features that have never been available before and we are open to accepting any ideas and pull requests.
This is opensource project and everyone can view it.

<center>
<img src="img/revitlookupwpf.jpg" alt="RevitLookupWpf" title="RevitLookupWpf" width="600"/> <!-- 686 -->
</center>

**Question:** How is this different than the original [RevitLookup](https://github.com/jeremytammik/RevitLookup)?
Am I missing something?
Is the difference that yours is using WPF instead of WinForms?

**Answer:**  Yes, and more.

Basically it is not the same.
I will write a document explaining all after complete feature.
It is an example, you can see description of methods and properties from code, and it is linked with [revitapidocs](https://www.revitapidocs.com).

**Question:**  Is it a Modeless window now? Would be a great thing.

**Answer:**  Yes, it works in a modeless window.

**Question:**  Great! Is it free to download?

**Answer:**  Sure, it free and you can download file `msi` to install from
the [latest release](https://github.com/weianweigan/RevitLookupWpf/releases/latest).

Many thanks to Chuong Ho for this great initiative, and the best of luck with it going forward!

<!---
<center>
<img src="img/chuongmep.tiff" alt="Hồ Văn Chương" title="Hồ Văn Chương" width="256"/>
</center>
--->

####<a name="5"></a> Pilcrow

I enjoy using words that I never used before, and sometimes discover new ones that I never even heard of.

An opportunity presented itself in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [AutoCAD to Revit text importing](https://forums.autodesk.com/t5/revit-api-forum/autocad-to-revit-text-importing/m-p/10822619):

> I just performed an Internet search for &para; and learned that it is named [Pilcrow](https://en.wikipedia.org/wiki/Pilcrow).
Never heard that name before.
