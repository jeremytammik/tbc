<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 12628657 [Setting ActiveView during Idling event]
  http://forums.autodesk.com/t5/revit-api-forum/setting-activeview-during-idling-event/m-p/6848256

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Here is a summary of the discussion and solution for setting <code>ActiveView</code> during the <code>Idling</code> event from the Revit API discussion forum raised and solved by Rudi 'Revitalizer' and Kinjal Desai; As per API documentation, this operation should not be invalid: no open transactions; <code>IsModifiable</code> is ok; <code>IsReadOnly</code> is ok; No pre-action events around. However, trying to do so throws an <code>InvalidOperationException</code> with the message "Setting active view is temporarily disabled"...

-->

### Setting Active View During Idling

Here is a summary of the discussion and solution
for [setting `ActiveView` during the Idling event](http://forums.autodesk.com/t5/revit-api-forum/setting-activeview-during-idling-event/m-p/6848256) from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) raised
and solved by Rudi 'Revitalizer' and Kinjal Desai, founder and fullstack developer
at [Dwaravati](http://www.dwaravati.com):

**Question:** I need to set `ActiveView` in `Idling` event handler.

As per API documentation, this operation should not be invalid: no open transactions; `IsModifiable` is ok; `IsReadOnly` is ok; No pre-action events around.
 
However, trying to do so throws an `InvalidOperationException` with the message "Setting active view is temporarily disabled". 

Closest explanation I could reach to is
Jeremy's [casual reply](http://thebuildingcoder.typepad.com/blog/2011/09/activate-a-3d-view.html#comment-2097342515) "the system may be busy with something else"
to a [blog comment](http://thebuildingcoder.typepad.com/blog/2011/09/activate-a-3d-view.html#comment-2097342519)
on [activating a 3D View](http://thebuildingcoder.typepad.com/blog/2011/09/activate-a-3d-view.html).

In a sense, that is the case &ndash; the system is 'busy', because the `Idling` event is being processed at the moment.
 
However, I wonder if that is the real reason or a real limitation in this case &ndash; for the system is also busy when `IExternalCommand.Execute` is being processed, in which case setting `ActiveView` functions perfectly.
 
Looking forward to expert comments on whether setting `ActiveView` is indeed invalid in `Idling` under all circumstances and whether there is a way around it.

**Answer 1:** As a workaround, would it be possible to select the view (using the API), then use `PostCommand` to set the active view? Then wait for idling again...

**Answer 2:** There is a `UIDocument.RequestViewChange` method made for the case you described.
It is included in the [Revit 2015 API](http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html)
[Document API additions](http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html#4.02):

#### <a name="2"></a><span style="color:darkblue">UIDocument Operations and Additions</span>

<p style="color:darkblue">The new method UIDocument.RequestViewChange requests to change the active view by posting a message asynchronously. Unlike setting the ActiveView property, this will not make the change in active view immediately. Instead, the request will be posted to occur when control returns to Revit from the API context. This method is permitted to change the active view from the Idling event or an ExternalEvent callback.</p>


**Response:** Thank you for all your suggestions and for bringing this to light.

It is async, but that's clearly the author's intended way; I'll fit my logic around it.
 
Imho, the API reference would be more useful (and save some hours and brain tissues) if such 'sister' functions could be linked in 'See Also' or something.

I implemented the suggestion using `RequestViewChange`.

Using a async call wasn't ideal for my specific need, but I refactored my code to accommodate it.
 
In absence of any answers, my fall-back alternative (far from ideal, but functional nonetheless), would have been to fire `Ctrl+F4` keystrokes (conditionally) from the `Idling` event. I am not fan of this approach, but it would also have served for my specific use case.

`RequestViewChange` is definitively the API authors' intended way to achieve this.
 
I attached a sample model containing macros to demonstrate both the problem and the solution implementing the permissible and intended way to change active view from `Idling`.

It is a project file with a blank model with two floor plans, containing the code (fairly simple and self-explanatory) in document macros.

On launching the `Demo_SetActiveViewInIdling` macro, a modeless form is displayed:

<center>
<img src="img/set_view_idling_1.png" alt="Demo_SetActiveViewInIdling main form" width="262"/>
</center>

If you choose the permissible working solution, a message is displayed announcing what will happen, and the action is successfully performed:

<center>
<img src="img/set_view_idling_2.png" alt="Demo_SetActiveViewInIdling successful operation coming up" width="366"/>
</center>

If you attempt the problematic approach, this is also announced in advance:

<center>
<img src="img/set_view_idling_3.png" alt="Demo_SetActiveViewInIdling problematic approach coming up" width="366"/>
</center>

The problematic approach throws an exception, which is handled and reported as well:

<center>
<img src="img/set_view_idling_4.png" alt="Demo_SetActiveViewInIdling error report" width="366"/>
</center>

The macro mainline just launches the modeless form that does all the work:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Demo_SetActiveViewInIdling()
{
&nbsp;&nbsp;FloatingForm&nbsp;form&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FloatingForm();
&nbsp;&nbsp;form.Setup(&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;);
&nbsp;&nbsp;form.Show();
}
</pre>

Here is the actual working code implemented by `FloatingForm`:

<pre class="code">
<span style="color:blue;">namespace</span>&nbsp;SetActiveViewInIdling
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Description&nbsp;of&nbsp;FloatingForm.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">partial</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">FloatingForm</span>&nbsp;:&nbsp;WinForm.<span style="color:#2b91af;">Form</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;attemptSettingActiveView_forbidden;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;attemptSettingActiveView_async;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;deregister;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">#region</span>&nbsp;Operational&nbsp;&amp;&nbsp;misc
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;FloatingForm()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;The&nbsp;InitializeComponent()&nbsp;call&nbsp;is&nbsp;required&nbsp;for&nbsp;Windows&nbsp;Forms&nbsp;designer&nbsp;support.</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.FormClosing&nbsp;+=&nbsp;FloatingForm_FormClosing;
 
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;FloatingForm_FormClosing(&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;WinForm.<span style="color:#2b91af;">FormClosingEventArgs</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;deregister&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiApplication;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">internal</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Setup(&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiApplication&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.uiApplication&nbsp;=&nbsp;uiApplication;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.uiApplication.Idling&nbsp;+=&nbsp;Application_Idling;
 
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">#endregion</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;demoButton_Click(&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;<span style="color:#2b91af;">EventArgs</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//this&nbsp;flag&nbsp;will&nbsp;be&nbsp;picked&nbsp;by&nbsp;next&nbsp;call&nbsp;to&nbsp;our&nbsp;Idling&nbsp;event&nbsp;handler</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attemptSettingActiveView_forbidden&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;demoButton2_Click(&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;<span style="color:#2b91af;">EventArgs</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//this&nbsp;flag&nbsp;will&nbsp;be&nbsp;picked&nbsp;by&nbsp;next&nbsp;call&nbsp;to&nbsp;our&nbsp;Idling&nbsp;event&nbsp;handler</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attemptSettingActiveView_async&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Application_Idling(&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;Autodesk.Revit.UI.Events.<span style="color:#2b91af;">IdlingEventArgs</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;attemptSettingActiveView_forbidden&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attemptSettingActiveView_async&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attemptSettingActiveView_forbidden&nbsp;=&nbsp;<span style="color:blue;">false</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;arbitaryFloorPlan&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;uiApplication.ActiveUIDocument.Document&nbsp;).OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">ViewPlan</span>&nbsp;)&nbsp;).Cast&lt;<span style="color:#2b91af;">ViewPlan</span>&gt;().Where(&nbsp;x&nbsp;=&gt;&nbsp;!x.IsTemplate&nbsp;).FirstOrDefault();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;arbitaryFloorPlan&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;SetActiveViewInIdling&nbsp;sample&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Message&nbsp;from&nbsp;within&nbsp;Application_Idling()\n\nNext&nbsp;statement&nbsp;is&nbsp;attempting&nbsp;to&nbsp;set&nbsp;\&#39;&quot;</span>&nbsp;+&nbsp;arbitaryFloorPlan.ViewName&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\&#39;&nbsp;as&nbsp;UIDocument.ActiveView&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uiApplication.ActiveUIDocument.ActiveView&nbsp;=&nbsp;arbitaryFloorPlan;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;SetActiveViewInIdling&nbsp;sample&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Add&nbsp;one&nbsp;or&nbsp;more&nbsp;Floor&nbsp;Plans.&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;Autodesk.Revit.Exceptions.<span style="color:#2b91af;">InvalidOperationException</span>&nbsp;invalidOperationException&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;SetActiveViewInIdling&nbsp;sample&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;***Issue&nbsp;Reproduced***\n\nMessage:&nbsp;&quot;</span>&nbsp;+&nbsp;invalidOperationException.Message&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\n\nFor&nbsp;more&nbsp;details,&nbsp;attach&nbsp;debugger,&nbsp;breakopint&nbsp;at&nbsp;FloatingForm.Application_Idling&nbsp;&gt;&nbsp;catch.&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>&nbsp;<span style="color:blue;">if</span>(&nbsp;attemptSettingActiveView_async&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attemptSettingActiveView_async&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attemptSettingActiveView_forbidden&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;arbitaryFloorPlan&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;uiApplication.ActiveUIDocument.Document&nbsp;).OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">ViewPlan</span>&nbsp;)&nbsp;).Cast&lt;<span style="color:#2b91af;">ViewPlan</span>&gt;().Where(&nbsp;x&nbsp;=&gt;&nbsp;!x.IsTemplate&nbsp;).FirstOrDefault();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;SetActiveViewInIdling&nbsp;sample&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Message&nbsp;from&nbsp;within&nbsp;Application_Idling()\n\nNext&nbsp;statement&nbsp;will&nbsp;request&nbsp;\&#39;&quot;</span>&nbsp;+&nbsp;arbitaryFloorPlan.ViewName&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\&#39;&nbsp;to&nbsp;be&nbsp;activated&nbsp;(using&nbsp;UIDocument.RequestViewChange())&nbsp;asynchronously.&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uiApplication.ActiveUIDocument.RequestViewChange(&nbsp;arbitaryFloorPlan&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>&nbsp;<span style="color:blue;">if</span>(&nbsp;deregister&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.uiApplication.Idling&nbsp;-=&nbsp;Application_Idling;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

Many thanks to Kinjal for implementing and sharing this nice demo solution, and to Matt Taylor and above all Rudi 'Revitalizer' for their good suggestions!

<!----
<center>
<img src="img/dwaravati_logo.png" alt="Dwaravati" width="177"/>
</center>
---->
