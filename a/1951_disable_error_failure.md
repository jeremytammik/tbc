<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://www.revitapidocs.com/2023/

- [Upgrading Revit API Apps For Newer Revit Versions](https://revthat.com/upgrading-revit-api-apps-for-newer-revit-versions)
  Eric Boehlke truevis

- Disable error with 'Error' severity
  https://forums.autodesk.com/t5/revit-api-forum/disable-error-with-error-severity/m-p/11004220

twitter:

Updated #RevitAPI documentation for Revit 2023 and a deep dive into the mysteries and pitfalls of the Failure API to disable a failure with error severity @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/errorfailure

We continue updating all systems the new release and take a deep dive into the mysteries and pitfalls of the Failure API
&ndash; RevitApiDocs support for Revit 2023
&ndash; Migrating add-ins to Revit 2023
&ndash; Disable failure with error severity...

linkedin:

Updated #RevitAPI documentation for Revit 2023 and a deep dive into the mysteries and pitfalls of the Failure API to disable a failure with error severity

https://autode.sk/errorfailure

We continue updating all systems the new release:

- RevitApiDocs support for Revit 2023
- Migrating add-ins to Revit 2023
- Disable failure with error severity...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Revit 2023 API Docs and Disabling an Error Failure

We continue updating all systems for the new release and take a deep dive into the mysteries and pitfalls of the Failure API:

- [RevitApiDocs support for Revit 2023](#2)
- [Migrating add-ins to Revit 2023](#3)
- [Disable failure with error severity](#4)

####<a name="2"></a> RevitApiDocs Support for Revit 2023

[Gui Talarico](https://twitter.com/gtalarico) updated the online Revit API documentation for Revit 2023, both:

- [apidocs](https://apidocs.co/apps/revit/2023/d4648875-d41a-783b-d5f4-638df39ee413.htm) and
- [revitapidocs](https://www.revitapidocs.com)

Notifications of new features are pubished on twitter at:

- [@ApiDocsCo](https://twitter.com/ApiDocsCo) and
- [@RevitApiDocs](https://twitter.com/RevitApiDocs)

Very many thanks to Gui for all his work on these invaluable resources, and the fast turn-around time for the Revit 2023 API!

<center>
<img src="img/revitapidocs2023.png" alt="Revit API Docs 2023" title="Revit API Docs 2023" width="600"/> <!-- 1000 -->
</center>

####<a name="3"></a> Migrating Add-Ins to Revit 2023

Eric Boehlke of [truevis BIM Consulting](https://truevis.com) shares
his experience and detailed illustrated process
of [upgrading Revit API apps for newer Revit versions](https://revthat.com/upgrading-revit-api-apps-for-newer-revit-versions).

For The Building Coder's notes on the topic this time around, please simply search this site
for [migration 2023](https://www.google.com/search?q=migration+2023&as_sitesearch=thebuildingcoder.typepad.com).

####<a name="4"></a> Disable Failure with Error Severity

We discussed numerous solutions 
for [detecting and automatically handling dialogues and failures](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.32).
The Failure API is a great help in many cases.
Handling warnings can be achieved using a [warning swallower](http://thebuildingcoder.typepad.com/blog/2016/09/warning-swallower-and-roomedit3d-viewer-extension.html#2).
Alexander [@aignatovich](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478) [@CADBIMDeveloper](https://github.com/CADBIMDeveloper) Ignatovich, aka Александр Игнатович,
explains how non-warning errors can also be handled in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [disabling error with `Error`  severity](https://forums.autodesk.com/t5/revit-api-forum/disable-error-with-error-severity/m-p/11004220):

**Question:** I have a specific failure that I want to disable.

I'm able to 'catch' the error with the `FailuresProcessing` event.

The problem is that I can't find a way to disable or delete the error.

`DeleteWarning` only works for `Warning` severity, and my failure is `Error` severity.

In the user interface, I'd click `Cancel` on the popup message &ndash; it's the message that warns that deleting a part will cause other parts to be deleted.

My goal is to not show this error.
I'm cancelling the operation itself by posting another failure message saying "operation was cancelled", but the warning of 'deleting part will cause other part to be deleted' is still shown.

My goal is to not show this error message from the start.
In the end, I am not performing the deletion &ndash; I'm blocking it with creating my own error using FailuresProcessing as mentioned above.
So, I want to prevent from this error from appearing in the first place.

**Answer:** You could try to either resolve the failure or roll back the transaction.

If so, you could try to delete elements automatically in your failure preprocessor:

<pre class="code">
&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(failureAccessor.HasResolutionOfType(
&nbsp;&nbsp;&nbsp;&nbsp;FailureResolutionType.DeleteElements))
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;failureAccessor.SetCurrentResolutionType(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailureResolutionType.DeleteElements);
 
&nbsp;&nbsp;&nbsp;&nbsp;failuresAccessor.ResolveFailure(failureAccessor);
&nbsp;&nbsp;}
</pre>

You should return `FailureProcessingResult.ProceedWithCommit` if you set resolution type.

This failure preprocessor solves a bit different task, but I think it could help you:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">AutoDetachOrDeleteFailurePreprocessor</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;IFailuresPreprocessor
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;FailureProcessingResult&nbsp;<span style="color:#74531f;">PreprocessFailures</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailuresAccessor&nbsp;<span style="color:#1f377f;">failuresAccessor</span>)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">preprocessorMessages</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;failuresAccessor.GetFailureMessages(FailureSeverity.Error)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Union(failuresAccessor.GetFailureMessages(FailureSeverity.Warning))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(<span style="color:#1f377f;">x</span>&nbsp;=&gt;&nbsp;x.HasResolutionOfType(FailureResolutionType.DeleteElements)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;x.HasResolutionOfType(FailureResolutionType.DetachElements))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(preprocessorMessages.Count&nbsp;==&nbsp;0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;FailureProcessingResult.Continue;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">foreach</span>&nbsp;(var&nbsp;<span style="color:#1f377f;">failureAccessor</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;preprocessorMessages)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureAccessor.SetCurrentResolutionType(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureAccessor.HasResolutionOfType(FailureResolutionType.DetachElements)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;FailureResolutionType.DetachElements&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;FailureResolutionType.DeleteElements);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failuresAccessor.ResolveFailure(failureAccessor);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;FailureProcessingResult.ProceedWithCommit;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

If you can't resolve the error with FailureResolutionType.DeleteElements, then you can return `FailureProcessingResult.ProceedWithRollBack`.

In that case, you have to set:

<pre class="code">
  failureOptions.SetClearAfterRollback(true);
</pre>

for your transaction.

**Response:** I'll explain my workflow with more details:

- I'm successfully preventing the users from deleting parts that have a specific scheme I made
- No deletion is done whatsoever. Which is good &ndash; that was my goal
- Although no deletion is done, the user still gets this warning

I want to hide it because it can be confusing to my user.
The problem is that the severity of this message is `Error` and not warning, and therefore I can't use the Failure Accessor `DeleteWarning` method.

So my question is: Is is possible to prevent showing failures of `Error` severity?

<center>
<img src="img/suppress_error_failure.png" alt="Error failure" title="Error failure" width="410" height="200"/>
</center>

**Answer:** Failures with `Error` severity cannot be "swallowed".
They should be resolved using some resolution type (if supported) or by transition rollback.
If the transaction is rolled back and you don't want to see messages in the UI, you should set clear after rolling back transaction option.

**Response:** I tried returning `ProceedWithCommit`, but the error still appears...
although I think I'm getting closer.

But there's nothing to rollback, because the transaction (deletion) didn't happen yet.
That's what the error warns me about.
I'm trying to return `ProceedWithCommit` and `Continue`, and still no success.

This is my `PreprocessFailures`;
I'm getting into the `for` loop, and inside my `if` statement.
And, of course, returning `ProceedWithRollback`.
The error still appears:

<pre class="code">
  public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
    {
      IList<FailureMessageAccessor> failList = new List<FailureMessageAccessor>();
      failList = failuresAccessor.GetFailureMessages(); // Inside event handler, get all warnings

      foreach (FailureMessageAccessor failure in failList)
      {
        FailureDefinitionId failID = failure.GetFailureDefinitionId();
        if (failID == BuiltInFailures.DPartFailures.DeletingDPartWillDeleteMorePartsError)
        {
          failure.SetCurrentResolutionType(FailureResolutionType.Default);
          failuresAccessor.ResolveFailure(failure);
          failuresAccessor.GetFailureHandlingOptions().SetClearAfterRollback(true);
          return FailureProcessingResult.ProceedWithRollBack;
        }
      }

      return FailureProcessingResult.Continue;
    }
</pre>

**Answer:** From the API dopcumentation on `FailureResolutionType`:

> Default &ndash; Special (reserved) type. It cannot be used as a type when defining a resolution, but can be used as a key to query default resolution from FailureMessage or FailureDefinition.

I don't think you have to call `ResolveFailure` if you want to rollback the transaction.

I would try to set "clear after rollback" through transaction options before starting the transaction, e.g.:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">failureOptions</span>&nbsp;=&nbsp;transaction.GetFailureHandlingOptions();
&nbsp;&nbsp;failureOptions.SetClearAfterRollback(<span style="color:blue;">true</span>);
&nbsp;&nbsp;<span style="color:green;">//...</span>
&nbsp;&nbsp;transaction.SetFailureHandlingOptions(failureOptions);
&nbsp;&nbsp;transaction.Start();
</pre>

**Response:** But I'm not starting any transaction... at any point.

This method is triggered by the user when she tries to delete an element (that's what I want).
I'm preventing the user from deleting the element (Raising a different error saying "Operation is cancelled").
I just don't want to get the error I attached in the picture a few comments above.

**Answer:** Do you use the `Application.RegisterFailuresProcessor` method?

Personally, I would suggest avoiding this...

From the API docs:

> Replaces Revit's default user interface (if present) with alternative handling for all warnings and errors (including those not generated by your application) for the rest of the Revit session; if your application is not prepared to respond to all warnings and errors, consider use of IFailuresPreprocessor (in your opened Transaction) or the FailuresProcessing event instead of this interface.
 
I think a better idea would be to subscribe to `Application.FailuresProcessing` event.
 
Then &ndash; yes, set "clear after rollback" as you did.

**Respnse:** I am registered to `FailuresProcessing`:

<pre class="code">
&nbsp;&nbsp;uiControlledApplication.ControlledApplication.FailuresProcessing&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;+=&nbsp;ControlledApplication_FailuresProcessing;
</pre>

And it still shows the error.

I am also using `IFailuresPreprocessor`:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">FailuresPreProcessor</span>&nbsp;:&nbsp;IFailuresPreprocessor
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;FailureProcessingResult&nbsp;<span style="color:#74531f;">PreprocessFailures</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailuresAccessor&nbsp;<span style="color:#1f377f;">failuresAccessor</span>)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IList&lt;FailureMessageAccessor&gt;&nbsp;<span style="color:#1f377f;">failList</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;FailureMessageAccessor&gt;();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Inside&nbsp;event&nbsp;handler,&nbsp;get&nbsp;all&nbsp;warnings</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failList&nbsp;=&nbsp;failuresAccessor.GetFailureMessages();&nbsp;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">foreach</span>&nbsp;(FailureMessageAccessor&nbsp;<span style="color:#1f377f;">failure</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;failList)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailureDefinitionId&nbsp;<span style="color:#1f377f;">failID</span>&nbsp;=&nbsp;failure.GetFailureDefinitionId();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(failID&nbsp;==&nbsp;BuiltInFailures.DPartFailures
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.DeletingDPartWillDeleteMorePartsError)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failure.SetCurrentResolutionType(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailureResolutionType.Others);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failuresAccessor.GetFailureHandlingOptions()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.SetClearAfterRollback(<span style="color:blue;">true</span>);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failuresAccessor.ResolveFailure(failure);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;FailureProcessingResult
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ProceedWithRollBack;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;FailureProcessingResult.Continue;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

Thanks so much for trying to help. I appreciate it! 

**Answer:** I guess you should pick one.
Have you tried to remove `ResolveFailure` and leave only:

<pre class="code">
  return FailureProcessingResult.ProceedWithRollBack;
</pre>

?

**Response:** What do you mean by 'pick one'?

I tried to return `ProceedWithRollBack` and removing `ResolveFailure` &ndash; didn't help.

**Answer:** Pick one means: choose either an application level failure processor (as for me &ndash; it's a bad choice) or a failure processing event.

Could you prepare a simple reproducible case: addin + model + what to do to reproduce?

**Response:** Yep, I'm using a failure processing event.

I am doing multiple things but let's focus only on the problem:
The goal is this:
When the user tries to delete a part (That was created in the past by my code and has a schema I made), raise a popup saying he can't do that and block the deletion.
I am doing that successfully!
In addition to blocking the deletion, I don't want the error message I attached yesterday to be shown, so there won't be a confusion.

To block the deletion, I have a class which inherits from `IUpdater` and uses a `FailureDefinition` GUID that I registered when Revit loads:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">SplitElementUpdater</span>&nbsp;:&nbsp;IUpdater
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;AddInId&nbsp;m_appId;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;UpdaterId&nbsp;m_updaterId;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Finds&nbsp;the&nbsp;failure&nbsp;definition&nbsp;id&nbsp;based&nbsp;on&nbsp;a&nbsp;constant&nbsp;GUID</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;FailureDefinitionId&nbsp;<span style="color:#74531f;">GetFailureDefinitionId</span>()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailureDefinitionRegistry&nbsp;<span style="color:#1f377f;">failureDefinitionRegistry</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;Autodesk.Revit.ApplicationServices.Application
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetFailureDefinitionRegistry();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailureDefinitionId&nbsp;<span style="color:#1f377f;">FailureDefinitionId</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FailureDefinitionId(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailureDefinitionIdGuid.Value);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;failureDefinitionRegistry
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FindFailureDefinition(FailureDefinitionId)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetId();
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;FailureDefinitionId&nbsp;_failureId&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;GetFailureDefinitionId();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;constructor&nbsp;takes&nbsp;the&nbsp;AddInId&nbsp;for&nbsp;the&nbsp;add-in&nbsp;associated&nbsp;with&nbsp;this&nbsp;updater</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">SplitElementUpdater</span>(AddInId&nbsp;<span style="color:#1f377f;">id</span>)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_appId&nbsp;=&nbsp;id;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_updaterId&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;UpdaterId(m_appId,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Guid.NewGuid());
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;<span style="color:#74531f;">Execute</span>(UpdaterData&nbsp;<span style="color:#1f377f;">data</span>)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;<span style="color:#1f377f;">doc</span>&nbsp;=&nbsp;data.GetDocument();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ICollection&lt;ElementId&gt;&nbsp;<span style="color:#1f377f;">changedElements</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;data.GetModifiedElementIds().Concat(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data.GetDeletedElementIds()).ToList();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DialogResult&nbsp;<span style="color:#1f377f;">userResult</span>&nbsp;=&nbsp;MessageBox.Show(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;You&nbsp;are&nbsp;trying&nbsp;to&nbsp;edit&nbsp;a&nbsp;part&nbsp;which&nbsp;was&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;divided&nbsp;by&nbsp;an&nbsp;automation&nbsp;tool.&nbsp;To&nbsp;edit&nbsp;this&nbsp;part,&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;first&nbsp;revert&nbsp;the&nbsp;division,&nbsp;then&nbsp;edit&nbsp;the&nbsp;part&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;normally.&nbsp;Do&nbsp;you&nbsp;wish&nbsp;to&nbsp;open&nbsp;the&nbsp;“Surface&nbsp;Split”&nbsp;tool?&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;&quot;</span>,&nbsp;MessageBoxButtons.OKCancel);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(userResult.Equals(DialogResult.OK))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RevitDBUtils.InitializeStaticUtils(doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RevitDBUtils.uidoc,&nbsp;RevitDBUtils.uiapp,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RevitDBUtils.dllFolder,&nbsp;eDiscipline.Architectural);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RevitDBUtils.ExecuteMethodInEvent(()&nbsp;=&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SurfaceSplitTabsWindow&nbsp;<span style="color:#1f377f;">window</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;SurfaceSplitTabsWindow(1);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;window.Show();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;},&nbsp;<span style="color:#a31515;">&quot;Open&nbsp;Revert&nbsp;Surface&nbsp;Split&nbsp;window&quot;</span>);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;failure&nbsp;message&nbsp;that&nbsp;will&nbsp;cancel&nbsp;the&nbsp;operation</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailureMessage&nbsp;<span style="color:#1f377f;">failureMessage</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FailureMessage(_failureId);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureMessage.SetFailingElements(changedElements);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.PostFailure(failureMessage);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">catch</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;<span style="color:#74531f;">GetAdditionalInformation</span>()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;<span style="color:#a31515;">&quot;Surface&nbsp;Split&nbsp;Updater&nbsp;for&nbsp;preventing&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;modifying&nbsp;elements&nbsp;divided&nbsp;by&nbsp;Surface&nbsp;Split&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;ChangePriority&nbsp;<span style="color:#74531f;">GetChangePriority</span>()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;ChangePriority.FreeStandingComponents;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;UpdaterId&nbsp;<span style="color:#74531f;">GetUpdaterId</span>()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;m_updaterId;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;<span style="color:#74531f;">GetUpdaterName</span>()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;<span style="color:#a31515;">&quot;Surface&nbsp;Split&nbsp;Updater&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

This class works as expected and blocks the deletion; I'm setting up the trigger here:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;UpdaterId&nbsp;<span style="color:#74531f;">SurfaceSplitElementUpdaterSetup</span>(AddInId&nbsp;<span style="color:#1f377f;">addinId</span>,&nbsp;Document&nbsp;<span style="color:#1f377f;">doc</span>)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;SplitElementUpdater&nbsp;<span style="color:#1f377f;">splitUpdater</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;SplitElementUpdater(addinId);<span style="color:green;">//Create&nbsp;a&nbsp;surface&nbsp;split&nbsp;updater&nbsp;for&nbsp;alerting&nbsp;on&nbsp;modified&nbsp;divided&nbsp;elements</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;UpdaterRegistry.RegisterUpdater(splitUpdater);<span style="color:green;">//register&nbsp;the&nbsp;updater</span>
&nbsp;&nbsp;&nbsp;&nbsp;UpdaterRegistered&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Creating&nbsp;filters&nbsp;for&nbsp;the&nbsp;updater:</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;ElementMulticategoryFilter&nbsp;<span style="color:#1f377f;">catFilter</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;ElementMulticategoryFilter(<span style="color:blue;">new</span>&nbsp;List&lt;BuiltInCategory&gt;&nbsp;{&nbsp;BuiltInCategory.OST_Parts&nbsp;});<span style="color:green;">//Create&nbsp;categories&nbsp;filter</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;ExtensibleStorageFilter&nbsp;<span style="color:#1f377f;">extensibleStorageFilter</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;ExtensibleStorageFilter(SplitFlag.GetGuid());
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//Create&nbsp;extensible&nbsp;storage&nbsp;filter&nbsp;of&nbsp;elements&nbsp;with&nbsp;the&nbsp;Surface&nbsp;split&nbsp;Element&nbsp;Info&nbsp;Guid</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;LogicalAndFilter&nbsp;<span style="color:#1f377f;">bothFilters</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;LogicalAndFilter(catFilter,&nbsp;extensibleStorageFilter);<span style="color:green;">//combine&nbsp;both&nbsp;filters&nbsp;to&nbsp;a&nbsp;single&nbsp;filter</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;ChangeType&nbsp;<span style="color:#1f377f;">elementDeletion</span>&nbsp;=&nbsp;Element.GetChangeTypeElementDeletion();<span style="color:green;">//the&nbsp;change&nbsp;type&nbsp;of&nbsp;an&nbsp;element&nbsp;deletion</span>
&nbsp;&nbsp;&nbsp;&nbsp;ChangeType&nbsp;<span style="color:#1f377f;">geometryChange</span>&nbsp;=&nbsp;Element.GetChangeTypeGeometry();<span style="color:green;">//the&nbsp;change&nbsp;type&nbsp;of&nbsp;a&nbsp;geometry&nbsp;change</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;UpdaterId&nbsp;<span style="color:#1f377f;">updaterId</span>&nbsp;=&nbsp;splitUpdater.GetUpdaterId();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//We&nbsp;want&nbsp;to&nbsp;trigger&nbsp;when&nbsp;changing&nbsp;some&nbsp;of&nbsp;the&nbsp;params:</span>
&nbsp;&nbsp;&nbsp;&nbsp;List&lt;Parameter&gt;&nbsp;<span style="color:#1f377f;">parameters</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;Parameter&gt;();
&nbsp;&nbsp;&nbsp;&nbsp;parameters.Add(GetPanelIDUtils.GetPanelIdParameter(doc));
&nbsp;&nbsp;&nbsp;&nbsp;parameters.Add(GetFromDocUtils.GetParameter(ParametersConstants.FACTORY,&nbsp;doc));
&nbsp;&nbsp;&nbsp;&nbsp;parameters.Add(GetFromDocUtils.GetParameter(ParametersConstants.MATERIAL,&nbsp;doc));
&nbsp;&nbsp;&nbsp;&nbsp;parameters.Add(GetFromDocUtils.GetParameter(BuiltInParameter.ROOF_BASE_LEVEL_PARAM,&nbsp;doc));
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">foreach</span>&nbsp;(Parameter&nbsp;<span style="color:#1f377f;">parameter</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;parameters)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(parameter&nbsp;!=&nbsp;<span style="color:blue;">null</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ChangeType&nbsp;<span style="color:#1f377f;">paramChange</span>&nbsp;=&nbsp;Element.GetChangeTypeParameter(parameter);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;UpdaterRegistry.AddTrigger(updaterId,&nbsp;bothFilters,&nbsp;paramChange);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;add&nbsp;the&nbsp;triggers&nbsp;for&nbsp;the&nbsp;updater</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;UpdaterRegistry.AddTrigger(updaterId,&nbsp;bothFilters,&nbsp;elementDeletion);
&nbsp;&nbsp;&nbsp;&nbsp;UpdaterRegistry.AddTrigger(updaterId,&nbsp;bothFilters,&nbsp;geometryChange);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;splitUpdater.GetUpdaterId();
&nbsp;&nbsp;}
</pre>

All of this is fine!

And now, the problem:

We know we can't swallow the error because it's of 'error' severity, but I'm unable to resolve it as well.

I tried with and without resolve failure, tried proceeding with commit and continue, nothing worked.

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;FailureProcessingResult&nbsp;<span style="color:#74531f;">PreprocessFailures</span>(FailuresAccessor&nbsp;<span style="color:#1f377f;">failuresAccessor</span>)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;IList&lt;FailureMessageAccessor&gt;&nbsp;<span style="color:#1f377f;">failList</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;FailureMessageAccessor&gt;();
&nbsp;&nbsp;&nbsp;&nbsp;failList&nbsp;=&nbsp;failuresAccessor.GetFailureMessages();&nbsp;<span style="color:green;">//&nbsp;Inside&nbsp;event&nbsp;handler,&nbsp;get&nbsp;all&nbsp;warnings</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">foreach</span>&nbsp;(FailureMessageAccessor&nbsp;<span style="color:#1f377f;">failure</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;failList)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailureDefinitionId&nbsp;<span style="color:#1f377f;">failID</span>&nbsp;=&nbsp;failure.GetFailureDefinitionId();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(failID&nbsp;==&nbsp;BuiltInFailures.DPartFailures.DeletingDPartWillDeleteMorePartsError)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failure.SetCurrentResolutionType(FailureResolutionType.Default);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failuresAccessor.GetFailureHandlingOptions().SetClearAfterRollback(<span style="color:blue;">true</span>);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//failuresAccessor.ResolveFailure(failure);</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransactionStatus&nbsp;<span style="color:#1f377f;">a</span>&nbsp;=&nbsp;failuresAccessor.RollBackPendingTransaction();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;FailureProcessingResult.ProceedWithRollBack;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;FailureProcessingResult.Continue;
&nbsp;&nbsp;}
</pre>

Thank you!

**Answer:** Try something like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">RevitApplication</span>&nbsp;:&nbsp;IExternalApplication
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;SyntheticFailureReplacement&nbsp;failureReplacement;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;Result&nbsp;<span style="color:#74531f;">OnStartup</span>(UIControlledApplication&nbsp;<span style="color:#1f377f;">application</span>)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureReplacement&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;SyntheticFailureReplacement();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;application.ControlledApplication.FailuresProcessing&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+=&nbsp;ControlledApplicationOnFailuresProcessing;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;Result.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;Result&nbsp;<span style="color:#74531f;">OnShutdown</span>(UIControlledApplication&nbsp;<span style="color:#1f377f;">application</span>)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;application.ControlledApplication.FailuresProcessing&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-=&nbsp;ControlledApplicationOnFailuresProcessing;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;Result.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;<span style="color:#74531f;">ControlledApplicationOnFailuresProcessing</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;<span style="color:#1f377f;">sender</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailuresProcessingEventArgs&nbsp;<span style="color:#1f377f;">e</span>)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">failuresAccessor</span>&nbsp;=&nbsp;e.GetFailuresAccessor();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">failureMessages</span>&nbsp;=&nbsp;failuresAccessor
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetFailureMessages(FailureSeverity.Error)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(<span style="color:#1f377f;">x</span>&nbsp;=&gt;&nbsp;x.GetFailureDefinitionId()&nbsp;==&nbsp;BuiltInFailures
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.DPartFailures.DeletingDPartWillDeleteMorePartsError)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(failureMessages.Any())
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">failureHandlingOptions</span>&nbsp;=&nbsp;failuresAccessor
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetFailureHandlingOptions();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureHandlingOptions.SetClearAfterRollback(<span style="color:blue;">true</span>);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failuresAccessor.SetFailureHandlingOptions(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureHandlingOptions);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.SetProcessingResult(FailureProcessingResult
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ProceedWithRollBack);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureReplacement.PostFailure(failureMessages
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.SelectMany(<span style="color:#1f377f;">x</span>&nbsp;=&gt;&nbsp;x.GetFailingElementIds()));
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">SyntheticFailureReplacement</span>&nbsp;:&nbsp;IExternalEventHandler
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">readonly</span>&nbsp;ExternalEvent&nbsp;externalEvent;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">readonly</span>&nbsp;List&lt;ElementId&gt;&nbsp;failingElementIds&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;ElementId&gt;();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">readonly</span>&nbsp;FailureDefinitionId&nbsp;failureDefinitionId&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FailureDefinitionId(<span style="color:blue;">new</span>&nbsp;Guid(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;bc0dc2ef-d928-42e4-9c9b-521cb822d3fd&quot;</span>));
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">SyntheticFailureReplacement</span>()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;externalEvent&nbsp;=&nbsp;ExternalEvent.Create(<span style="color:blue;">this</span>);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FailureDefinition.CreateFailureDefinition(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureDefinitionId,&nbsp;FailureSeverity.Warning,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;My&nbsp;accurate&nbsp;message&nbsp;replacement&quot;</span>);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;<span style="color:#74531f;">PostFailure</span>(IEnumerable&lt;ElementId&gt;&nbsp;<span style="color:#1f377f;">failingElements</span>)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failingElementIds.Clear();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failingElementIds.AddRange(failingElements);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;externalEvent.Raise();
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;<span style="color:#74531f;">Execute</span>(UIApplication&nbsp;<span style="color:#1f377f;">app</span>)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">document</span>&nbsp;=&nbsp;app.ActiveUIDocument.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>&nbsp;(<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">transaction</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Transaction(document,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;auxiliary&nbsp;transaction&quot;</span>))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">failureHandlingOptions</span>&nbsp;=&nbsp;transaction
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetFailureHandlingOptions();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureHandlingOptions.SetForcedModalHandling(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">false</span>);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.SetFailureHandlingOptions(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureHandlingOptions);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Start();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">failureMessage</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FailureMessage(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureDefinitionId);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failureMessage.SetFailingElements(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;failingElementIds);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document.PostFailure(failureMessage);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transaction.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;<span style="color:#74531f;">GetName</span>()&nbsp;=&gt;&nbsp;nameof(SyntheticFailureReplacement);
&nbsp;&nbsp;}
</pre>

**Response:** Worked like a charm. Thanks!

Many thanks to Alexander for his deep expertise in this area, and above all for his super-human patience providing such detailed guidance!
