<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Roomedit3d Live Real-Time Socket.io BIM Update #revitAPI #3dwebcoder @AutodeskForge #adsk #aec #bim #socket.io

I completed the first running version of my roomedit3d project connecting BIM and the cloud demonstrating two cool possibilities to enhance interaction with the View and Data API
&ndash; A viewer extension enabling interactive model modification, i.e., translation of selected elements
&ndash; Real-time communication of the modification back to the source CAD system using a direct socket.io connection to broadcast from the web server to any number of desktop clients...

-->

### Roomedit3d Live Real-Time BIM Update Recording

I completed the first running version of my [roomedit3d](https://github.com/jeremytammik/roomedit3d) project
connecting BIM and the cloud demonstrating two cool possibilities to enhance interaction with the View and Data API:

- A viewer extension enabling interactive model modification, i.e., translation of selected elements
- Real-time communication of the modification back to the source CAD system using a direct socket.io connection to broadcast from the web server to any number of desktop clients

Let's take a closer look at the implementation and a test run recording:

- [Overview](#1)
- [Roomedit3d implementation](#2)
- [External command to toggle broadcast subscription](#3)
- [BimUpdater external event handler](#4)
- [External application managing socket.io and external event](#5)
- [Test run video recording](#6)
- [Download](#7)
- [To do](#8)
- [iTerm2](#9)


#### <a name="1"></a>Overview

All the background information and full discussions of the implementation details are provided in my mention of
the [initial idea](http://thebuildingcoder.typepad.com/blog/2016/05/idea-station-and-textnote-bounding-box.html), the
working [proof of concept with a C# .NET console test application](http://the3dwebcoder.typepad.com/blog/2016/05/roomedit3d-viewer-translation-extension-post-and-socket.html) and
the overview of
the [Revit-independent implementation aspects](http://thebuildingcoder.typepad.com/blog/2016/05/roomedit3d-console-test-and-rendering-assets.html#2).

Here is a quick recapitulation:

- We start off with a Revit BIM
- The BIM model is translated for sharing in the [Forge](http://forge.autodesk.com) [View and Data API](https://developer.autodesk.com/api/view-and-data-api) viewer
- The [roomedit3d](https://github.com/jeremytammik/roomedit3d) node.js web server hosts the viewer and provides a REST API for it to request authorisation tokens from
- The viewer is equipped with an extension enabling manual selection and translation of individual elements
- The viewer extension detects and reports the selected element external id and translation vector back to the web server via a REST API call
- The web server opens a [socket.io](http://socket.io) channel and broadcasts the translation data
- The [Roomedit3dApp](https://github.com/jeremytammik/Roomedit3dApp) C# .NET Revit API add-in subscribes to the socket.io broadcast
- The add-in implements an external event to update the BIM when the translation data is received
- The BimUpdater class holds the requested translation tasks in a queue and processes them in its `Execute` method


#### <a name="2"></a>Roomedit3d Implementation

All the interesting aspects are implemented by just two modules, the BIM updater and the external application managing the socket.io subscription and external event.

In fact, the BIM updater is kind of trivial, too, so really `App.cs` is the only really interesting part  :-)

Let's look at both, though, and the external command as well, for the sake of completeness:

- [External command to toggle broadcast subscription](#3)
- [BimUpdater external event handler](#4)
- [External application managing socket.io and external event](#5)


#### <a name="3"></a>External Command to Toggle Broadcast Subscription

<pre class="code">
[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.ReadOnly&nbsp;)]
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">Command</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;subscribed&nbsp;=&nbsp;<span style="color:#2b91af;">App</span>.ToggleSubscription();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Toggled&nbsp;Subscription&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(&nbsp;subscribed&nbsp;?&nbsp;<span style="color:#a31515;">&quot;S&quot;</span>&nbsp;:&nbsp;<span style="color:#a31515;">&quot;Uns&quot;</span>&nbsp;)&nbsp;+&nbsp;<span style="color:#a31515;">&quot;ubscribed.&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
}
</pre>

#### <a name="4"></a>BimUpdater External Event Handler

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;BIM&nbsp;updater,&nbsp;driven&nbsp;both&nbsp;via&nbsp;external&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;command&nbsp;and&nbsp;external&nbsp;event&nbsp;handler.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">BimUpdater</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalEventHandler</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;The&nbsp;queue&nbsp;of&nbsp;pending&nbsp;tasks&nbsp;consisting&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;of&nbsp;UniqueID&nbsp;and&nbsp;translation&nbsp;offset&nbsp;vector.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:#2b91af;">Queue</span>&lt;<span style="color:#2b91af;">Tuple</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:#2b91af;">XYZ</span>&gt;&gt;&nbsp;_queue&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;BimUpdater()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;_queue&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Queue</span>&lt;<span style="color:#2b91af;">Tuple</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:#2b91af;">XYZ</span>&gt;&gt;();
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Execute&nbsp;method&nbsp;invoked&nbsp;by&nbsp;Revit&nbsp;via&nbsp;the&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;external&nbsp;event&nbsp;as&nbsp;a&nbsp;reaction&nbsp;to&nbsp;a&nbsp;call&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;to&nbsp;its&nbsp;Raise&nbsp;method.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Execute(&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;a.ActiveUIDocument.Document;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>&nbsp;(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;GetName()&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">while</span>&nbsp;(&nbsp;0&nbsp;&lt;&nbsp;_queue.Count&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Tuple</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:#2b91af;">XYZ</span>&gt;&nbsp;task&nbsp;=&nbsp;_queue.Dequeue();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;Translating&nbsp;{0}&nbsp;by&nbsp;{1}&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;task.Item1,&nbsp;<span style="color:#2b91af;">Util</span>.PointString(&nbsp;task.Item2&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;task.Item1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementTransformUtils</span>.MoveElement(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;e.Id,&nbsp;task.Item2&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Required&nbsp;IExternalEventHandler&nbsp;interface&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;method&nbsp;returning&nbsp;a&nbsp;descriptive&nbsp;name.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;GetName()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">App</span>.Caption&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;&quot;</span>&nbsp;+&nbsp;GetType().Name;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Enqueue&nbsp;a&nbsp;BIM&nbsp;update&nbsp;action&nbsp;to&nbsp;be&nbsp;performed,</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;consisting&nbsp;of&nbsp;UniqueID&nbsp;and&nbsp;translation&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;offset&nbsp;vector.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Enqueue(&nbsp;<span style="color:blue;">string</span>&nbsp;uid,&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;offset&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;_queue.Enqueue(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Tuple</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:#2b91af;">XYZ</span>&gt;(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uid,&nbsp;offset&nbsp;)&nbsp;);
&nbsp;&nbsp;}
}
</pre>

#### <a name="5"></a>External Application Managing Socket.io and External Event

<pre class="code">
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">App</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalApplication</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Caption</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;Caption&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Roomedit3d&quot;</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Socket&nbsp;broadcast&nbsp;URL.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_url&nbsp;=&nbsp;<span style="color:#a31515;">&quot;https://roomedit3d.herokuapp.com:443&quot;</span>;

&nbsp;&nbsp;<span style="color:gray;">#region</span>&nbsp;External&nbsp;event&nbsp;subscription&nbsp;and&nbsp;handling
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Store&nbsp;the&nbsp;external&nbsp;event.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">ExternalEvent</span>&nbsp;_event&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Store&nbsp;the&nbsp;external&nbsp;event.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">BimUpdater</span>&nbsp;_bimUpdater&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Store&nbsp;the&nbsp;socket&nbsp;we&nbsp;are&nbsp;listening&nbsp;to.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Socket</span>&nbsp;_socket&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Provide&nbsp;public&nbsp;read-only&nbsp;access&nbsp;to&nbsp;external&nbsp;event.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">ExternalEvent</span>&nbsp;Event
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>&nbsp;{&nbsp;<span style="color:blue;">return</span>&nbsp;_event;&nbsp;}
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Enqueue&nbsp;a&nbsp;new&nbsp;BIM&nbsp;updater&nbsp;task.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Enqueue(&nbsp;<span style="color:blue;">object</span>&nbsp;data&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">JObject</span>&nbsp;data2&nbsp;=&nbsp;<span style="color:#2b91af;">JObject</span>.FromObject(&nbsp;data&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;transform:&nbsp;uid={0}&nbsp;({1:0.00},{2:0.00},{3:0.00})&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data2[<span style="color:#a31515;">&quot;externalId&quot;</span>],&nbsp;data2[<span style="color:#a31515;">&quot;offset&quot;</span>][<span style="color:#a31515;">&quot;x&quot;</span>],
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data2[<span style="color:#a31515;">&quot;offset&quot;</span>][<span style="color:#a31515;">&quot;y&quot;</span>],&nbsp;data2[<span style="color:#a31515;">&quot;offset&quot;</span>][<span style="color:#a31515;">&quot;z&quot;</span>]&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:#a31515;">&quot;Enqueue&nbsp;task&nbsp;&quot;</span>&nbsp;+&nbsp;s&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;uid1&nbsp;=&nbsp;data2[<span style="color:#a31515;">&quot;externalId&quot;</span>].ToString();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;offset1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>.Parse(&nbsp;data2[<span style="color:#a31515;">&quot;offset&quot;</span>][<span style="color:#a31515;">&quot;x&quot;</span>].ToString()&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>.Parse(&nbsp;data2[<span style="color:#a31515;">&quot;offset&quot;</span>][<span style="color:#a31515;">&quot;y&quot;</span>].ToString()&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>.Parse(&nbsp;data2[<span style="color:#a31515;">&quot;offset&quot;</span>][<span style="color:#a31515;">&quot;z&quot;</span>].ToString()&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;uid&nbsp;=&nbsp;(<span style="color:blue;">string</span>)&nbsp;data2[<span style="color:#a31515;">&quot;externalId&quot;</span>];
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;offset&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span style="color:blue;">double</span>)&nbsp;data2[<span style="color:#a31515;">&quot;offset&quot;</span>][<span style="color:#a31515;">&quot;x&quot;</span>],
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span style="color:blue;">double</span>)&nbsp;data2[<span style="color:#a31515;">&quot;offset&quot;</span>][<span style="color:#a31515;">&quot;y&quot;</span>],
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span style="color:blue;">double</span>)&nbsp;data2[<span style="color:#a31515;">&quot;offset&quot;</span>][<span style="color:#a31515;">&quot;z&quot;</span>]&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;_bimUpdater.Enqueue(&nbsp;uid,&nbsp;offset&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;_event.Raise();
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Toggle&nbsp;on&nbsp;and&nbsp;off&nbsp;subscription&nbsp;to&nbsp;automatic&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;BIM&nbsp;update&nbsp;from&nbsp;cloud.&nbsp;Return&nbsp;true&nbsp;when&nbsp;subscribed.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;ToggleSubscription()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>&nbsp;(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;_event&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:#a31515;">&quot;Unsubscribing...&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_socket.Disconnect();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_socket&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_bimUpdater&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_event.Dispose();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_event&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:#a31515;">&quot;Unsubscribed.&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:#a31515;">&quot;Subscribing...&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_bimUpdater&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BimUpdater</span>();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;options&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">IO</span>.<span style="color:#2b91af;">Options</span>()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IgnoreServerCertificateValidation&nbsp;=&nbsp;<span style="color:blue;">true</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AutoConnect&nbsp;=&nbsp;<span style="color:blue;">true</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ForceNew&nbsp;=&nbsp;<span style="color:blue;">true</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;};

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_socket&nbsp;=&nbsp;<span style="color:#2b91af;">IO</span>.Socket(&nbsp;_url,&nbsp;options&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_socket.On(&nbsp;<span style="color:#2b91af;">Socket</span>.EVENT_CONNECT,&nbsp;()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:#a31515;">&quot;Connected&quot;</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_socket.On(&nbsp;<span style="color:#a31515;">&quot;transform&quot;</span>,&nbsp;data
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;Enqueue(&nbsp;data&nbsp;)&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_event&nbsp;=&nbsp;<span style="color:#2b91af;">ExternalEvent</span>.Create(&nbsp;_bimUpdater&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.Log(&nbsp;<span style="color:#a31515;">&quot;Subscribed.&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;_event;
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:gray;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;External&nbsp;event&nbsp;subscription&nbsp;and&nbsp;handling</span>

&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;OnStartup(&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;OnShutdown(&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
}
</pre>

That's all there is too it!

Are you surprised how short and easy it is?

La perfection est atteinte, non pas lorsqu'il n'y a plus rien à ajouter, mais lorsqu'il n'y a plus rien à retirer.

*Perfection is achieved, not when there is nothing more to add, but when there is nothing left to take away.*

<p style="text-align: right"><i>Antoine de Saint-Exupéry</i></p>


#### <a name="6"></a>Test Run Video Recording

Now the time has come to show the full solution running live, connecting the [View and Data API](https://developer.autodesk.com/api/view-and-data-api) viewer and the Revit add-in updating the BIM live in real-time.

Here is a [five-minute video recording](https://youtu.be/EbtyAZPX8Bc) showing the system up and running:

<center>
<iframe width="400" height="300" src="https://www.youtube.com/embed/EbtyAZPX8Bc" frameborder="0" allowfullscreen></iframe>
</center>

To my great surprise, I do not have to do anything at all to convert or transform the viewer coordinates back into the Revit model.

In other words, the viewer seems to be using the same units as Revit does, i.e., imperial feet, and swapping the X, Y and Z axes appropriately too.

I had expected to have implement and apply some kind of transformation myself.

#### <a name="7"></a>Download

The version up and running in the recording above is the Revit
add-in [Roomedit3dApp release 2017.0.0.4](https://github.com/jeremytammik/Roomedit3dApp/releases/tag/2017.0.0.4) with
the web server and viewer hosted by [roomedit3d release 0.0.4](https://github.com/jeremytammik/roomedit3d/releases/tag/0.0.4) running
on [Heroku](https://www.heroku.com).

The most up-to-date versions are always provided by
the [Roomedit3dApp](https://github.com/jeremytammik/Roomedit3dApp)
and [roomedit3d](https://github.com/jeremytammik/roomedit3d) master
branches, respectively, and the main documentation is in the latter.

I hope you find this useful and wish you much fun and success connecting the desktop and the cloud, and BIM with many powerful [Forge](http://forge.autodesk.com) and own custom web services.

By the way, I very much hope you can make your way to the [Forge DevCon](http://forge.autodesk.com/conference) coming up real soon now, in just two weeks time!

I look forward to seeing you there!

Many thanks to Philippe Leefsma for all his help in Barcelona getting the basics up and running! Philippe implemented most of the code in the web server, including the viewer management, viewer extension and socket.io broadcast.


#### <a name="8"></a>To Do

This completes the old first item in my [previous to do list](http://the3dwebcoder.typepad.com/blog/2016/05/roomedit3d-viewer-translation-extension-post-and-socket.html#13).

Here is an update with a new first entry:

- Explore how to update the Revit BIM automatically immediately, without having to click into the window first. This may be as easy as simply briefly setting the Windows focus on the Revit main window.
- Besides translation, I would also like the View and Data extension to handle rotation in the XY plane.
- Since this runs so well here, I would like to update the [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) sample to use the same technology and implement a socket.io connection between the FireRatingCloud C# .NET Revit add-in and its [fireratingdb](https://github.com/jeremytammik/firerating) node.js web and MongoDB server.


#### <a name="9"></a>iTerm2

I just installed and started using [iTerm2](http://iterm2.com),
again based on Philippe's recommendation last week.

So far, the switch from the standard Mac terminal to iTerm2 is completely seamless, and now I am looking forward to discovering and exploring the [numerous advantages](http://iterm2.com/features.html) one by one as they in handy.

Thanks again, Philippe!
