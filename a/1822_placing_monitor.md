<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/get-notified-when-a-family-type-is-about-to-place/m-p/9328378

twitter:

Multi-threading family instance placement monitor elegantly combining .NET timer and multi-threading functionality with the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/placementmonitor

Kennan Chen perfected his family instance placement monitor by elegantly combining the Revit API with additional .NET timer and multi-threading functionality in a novel fashion seldom seen in a Revit add-in...

&ndash; 
...

linkedin:

Multi-threading family instance placement monitor elegantly combining .NET timer and multi-threading functionality with the #RevitAPI #

http://bit.ly/placementmonitor

Kennan Chen perfected his family instance placement monitor by elegantly combining the Revit API with additional .NET timer and multi-threading functionality in a novel fashion seldom seen in a Revit add-in...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Multi-Threading Family Instance Placement Monitor

In the last post, I mentioned
some [undocumented UIFrameworkService utility methods](https://thebuildingcoder.typepad.com/blog/2020/02/lat-long-to-metres-and-duplicate-legend-component.html#4)
pointed out by Kennan Chen of Shanghai in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [getting notified when a family type is about to be placed](https://forums.autodesk.com/t5/revit-api-forum/get-notified-when-a-family-type-is-about-to-place/m-p/9327282).

He made use of them to implement a family instance placement monitor that could not be achieved using the pure documented Revit API alone.

After the initial publication, he perfected it by elegantly combining the Revit API with additional .NET timer and multi-threading functionality in a novel fashion seldom seen in a Revit add-in.

Let's return to the original question and start fresh from there:

**Question:** Is there an event which can notify when Revit is about to place a family type?

There are events like `Application` `FamilyLoadedIntoDocument` and `FamilyLoadingIntoDocument`.

Is it possible to have another event `FamilyTypePlacingIntoDocument` for this?

Or is there a workaround?

**Answer:** As recently discussed, you
can [use the DocumentChanged event to detect the launching of a command](https://thebuildingcoder.typepad.com/blog/2020/01/torsion-tools-command-event-and-info-in-da4r.html#3).

**Response:** It works great to catch the placing FamilyType event triggered by placing type directly from Revit UI.

After hours of struggle to improve
the [initial solution](https://thebuildingcoder.typepad.com/blog/2020/02/lat-long-to-metres-and-duplicate-legend-component.html#4),
I finally completed this by simply using a `Timer` to constantly check the currently placing type until the UI refreshes and the API call returns correctly.
It may not be the best solution, but at least it works.

I wrapped the logic up in the following class to make it easier.
Hope this can be helpful.
Pay more attention to the potential multi-thread risk and UI performance impact if someone wants to use the code.

It is my pleasure to share this to people who are in need.

As a Chinese, I'm not quite good at explaining things in English. I'll try my best to be accurate &nbsp; :-)

I'll ignore the part how to get notified about a family symbol placing event.

Once the placing event is caught, the next job is to get the placing family symbol.

The core method is the `UIFrameworkServices` `TypeSelectorService` `getCurrentTypeId` method from UIFrameworkServices.dll. This DLL ships with Revit, and can be easily found in the Revit root folder.

I believe this method is designed for the Revit UI framework to get data for the Properties panel, since every time this method is invoked, the returned value is always the element id of the `ElementType` that is currently displayed in the Properties panel.

The biggest problem is, when a family symbol is to be placed, by API call or by UI click, this method doesn't return correctly.
I guess the state doesn't change before Revit really enters placing mode.
But when Revit enters placing mode, no code can be run since the command loop in Revit is stuck.
That's frustrating!

Luckily, another loop is still running: the message loop in every Windows UI application running in STA mode.
In WPF, the message loop is started by Dispatcher in the main UI thread.
UI updates must be queued by the dispatcher to be executed in the main UI thread synchronously.
That's exactly the same mechanism adopted by Revit known as `ExternalEvent`.
In the `FamilyPlacingMonitorService` class, the `DispatcherInvoke` method tests whether the execution is currently in main UI thread.
If not, it queues the delegate method to the UI thread.

To run code after Revit entering placing mode, a `Timer` is created ahead of time to constantly try to resolve the currently placing symbol.
But the Timer doesn't run code in the UI thread, which means it's not safe to call the Revit API.
The trick is to queue the Timer callback to the main UI thread using Dispatcher.

Every UI object (DispatcherObject specifically) holds a reference to the Dispatcher instance; it's easy to get that instance from the Ribbon object (Autodesk.Windows.ComponentManager.Ribbon).

I added some comments to the code to explain it more clearly:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">FamilyPlacingMonitorService</span>
{
&nbsp;&nbsp;<span style="color:gray;">#region</span>&nbsp;Constructors
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;FamilyPlacingMonitorService(&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;app&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;app.DocumentChanged&nbsp;+=&nbsp;App_DocumentChanged;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:gray;">#endregion</span>
 
&nbsp;&nbsp;<span style="color:gray;">#region</span>&nbsp;Others
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">event</span>&nbsp;<span style="color:#2b91af;">EventHandler</span>&lt;<span style="color:#2b91af;">FamilySymbol</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;FamilySymbolPlacingIntoDocument;
 
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;App_DocumentChanged(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;DocumentChangedEventArgs&nbsp;e&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;transactionName&nbsp;=&nbsp;e.GetTransactionNames()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;transactionName&nbsp;==&nbsp;<span style="color:#a31515;">&quot;Modify&nbsp;element&nbsp;attributes&quot;</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;treat&nbsp;transactions&nbsp;with&nbsp;the&nbsp;name&nbsp;&quot;Modify&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;element&nbsp;attributes&quot;&nbsp;as&nbsp;element&nbsp;placing</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;maybe&nbsp;not&nbsp;accurate,&nbsp;but&nbsp;enough&nbsp;to&nbsp;cover&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;most&nbsp;scenes</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;document&nbsp;=&nbsp;e.GetDocument();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;try&nbsp;to&nbsp;get&nbsp;the&nbsp;current&nbsp;placing&nbsp;family&nbsp;symbol</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ResolveCurrentlyPlacingFamilySymbol(&nbsp;document&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">FamilySymbol</span>&nbsp;familySymbol&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;got&nbsp;ya,&nbsp;notify&nbsp;via&nbsp;event</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OnFamilySymbolPlacingIntoDocument(&nbsp;familySymbol&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;current&nbsp;type&nbsp;doesn&#39;t&nbsp;refreshed,&nbsp;create&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;a&nbsp;Timer&nbsp;to&nbsp;constantly&nbsp;try</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Timer&nbsp;timer&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;only&nbsp;queue&nbsp;one&nbsp;resolving&nbsp;logic&nbsp;to&nbsp;the&nbsp;main&nbsp;thread</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;checking&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;timer&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Timer(&nbsp;s&nbsp;=&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!checking&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;checking&nbsp;=&nbsp;<span style="color:blue;">true</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;try&nbsp;to&nbsp;queue&nbsp;the&nbsp;resolving&nbsp;logic&nbsp;to&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;main&nbsp;thread&nbsp;to&nbsp;avoid&nbsp;multi-thread&nbsp;risk</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DispatcherInvoke(&nbsp;()&nbsp;=&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ResolveCurrentlyPlacingFamilySymbol(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document&nbsp;)&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">FamilySymbol</span>&nbsp;symbol&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;got&nbsp;ya,&nbsp;notify&nbsp;via&nbsp;event</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OnFamilySymbolPlacingIntoDocument(&nbsp;symbol&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;release&nbsp;the&nbsp;timer</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;timer?.Change(&nbsp;0,&nbsp;Timeout.Infinite&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;timer?.Dispose();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;checking&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;},&nbsp;<span style="color:blue;">null</span>,&nbsp;0,&nbsp;100&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;DispatcherInvoke(&nbsp;<span style="color:#2b91af;">Action</span>&nbsp;action&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ComponentManager.Ribbon.Dispatcher?
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.CheckAccess()&nbsp;??&nbsp;<span style="color:blue;">false</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;currently&nbsp;on&nbsp;main&nbsp;thread,&nbsp;execute&nbsp;directly</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;action?.Invoke();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;not&nbsp;on&nbsp;main&nbsp;thread,&nbsp;queue&nbsp;the&nbsp;delegate&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;to&nbsp;main&nbsp;ui&nbsp;thread</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ComponentManager.Ribbon.Dispatcher?
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Invoke(&nbsp;action&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnFamilySymbolPlacingIntoDocument(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FamilySymbol</span>&nbsp;symbol&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;FamilySymbolPlacingIntoDocument?.Invoke(&nbsp;<span style="color:blue;">this</span>,&nbsp;symbol&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:#2b91af;">FamilySymbol</span>&nbsp;ResolveCurrentlyPlacingFamilySymbol(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;document&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;id&nbsp;=&nbsp;TypeSelectorService.getCurrentTypeId();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;id&nbsp;&gt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;document.GetElement(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;id&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">FamilySymbol</span>&nbsp;symbol&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;symbol;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:gray;">#endregion</span>
}
</pre>

Very great thanks to Kennan Chen for this extremely knowledgeable, clear and illuminating explanation and elegant juggling of the different threads and contexts!

<center>
<img src="img/surveillance.jpg" alt="Surveillance" title="Surveillance" width="400"/>
</center>
