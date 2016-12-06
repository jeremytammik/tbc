<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

12375256 [Event to run a IExternalCommand]
http://forums.autodesk.com/t5/revit-api-forum/event-to-run-a-iexternalcommand/m-p/6692479

Using Other Events to Execute Add-In Code #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

The Revit API is very simple.
It is entirely event driven.
Any and every use of the Revit API requires a valid API context.
The most common and obvious way to get into a valid Revit API context &ndash; and the most commonly used event &ndash; is the one to launch an external command, which calls the <code>IExternalCommand</code> <code>Execute</code> handler method.
Other important ones to be aware of are <code>ApplicationInitialized</code> and <code>DialogBoxShowing</code>, unconnected with any external command at all...

&ndash; 

-->

### Using Other Events to Execute Add-In Code

The Revit API is very simple.

It is entirely event driven.

Any and every use of the Revit API requires a valid API context.

The most common and obvious way to get into a valid Revit API context &ndash; and the most commonly used event &ndash; is the one to launch an external command, which calls the `IExternalCommand` `Execute` handler method.

Other important ones to be aware of are `ApplicationInitialized` and `DialogBoxShowing`, unconnected with any external command at all, as you can see below, and the method described by David Echols to [load and execute assembly code on the fly](#2) in an `Idling` event handler by subscribing to that event in the application `OnStartup` method.

Here is an interesting discussion exploring some options from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
searching for an [event to run an IExternalCommand](http://forums.autodesk.com/t5/revit-api-forum/event-to-run-a-iexternalcommand/m-p/6692479):

**Question:** Wondering if you can assist with an issue I'm having that I just can't seem to solve.

Would be nice just to know if what I am trying to do is possible or not.
 
I have been playing around with the events within an `ExternalApplication` and I have got these to work fine (i.e. using the `DocumentCreated` event to flash up a "HelloWorld" task dialog when a document is created)

I have also written a few basic external commands with buttons on the addin ribbon that work fine.
 
My issue is: how do I get an event to fire off an external command instead of having to use a button?
 
I am basically trying to get the event to trigger off the command and make some changes to the document and so I need the current document which I don't believe I can access within the external application.
 
I have read all of the event samples in the SDK but still can't work it out and I'm sure it should be really simple.
 
**Answer by Matt:** I'm pretty sure that's what [PostCommand](http://www.revitapidocs.com/2017/b0df464d-1733-ea9e-ac40-399fa9c9a037.htm) is for.
 
**Answer by Erik:** Matt's response is correct and valid for all buttons in Revit (almost), but if you created the external command, you can just create an instance of the class and run the `Execute` method. That's even simpler.

**Response:** I am trying to do what you suggest, "just create an instance of the class and run the `Execute` method", but that's where I am getting stuck.

In my external app I am trying to execute the command using 

<pre class="code">
  <span style="color:#2b91af;">Command</span>&nbsp;cmd&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Command</span>();
  &#99;md.Execute();
</pre>

(Editor's note on Typepad crash: I was forced to replace the character 'c' in the last line above by the HTML escape code `&#99;` before I was able to post this.)

However, that understandably gives me the error

<pre>
Error CS7036 There is no argument given that corresponds to the required formal parameter 'commandData' of 'Command.Execute(ExternalCommandData, ref string, ElementSet)'
</pre>

Could you please expand on how I would use the `Execute` method from an external application?

**Answer by Gonçalo:** Mike, you need a handler for the event, and the `IExternaCommand.Execute` method does not have the same parameters.

You can fake the parameters (commanddata, etc.) using reflection and an adapter; but that seems awkward.

My suggestion is just to implement a method to handle the event you are after. After all, the sender is usually the application.

**Answer by Matt:** I think Erik was suggesting that if you wrote the external command, you could call the same function that you call in the external command.
 
`PostCommand` would be the best fit, if you can apply it. Why don't you tell us exactly what you're trying to do?
 
**Answer by Erik:** Maybe I read your message a bit fast. I missed the fact that you were in an external application context, I automatically thought you were in `IExternalCommand`; then you could just pass the `ExternalCommandData` object forward. 

But to solve your problem you could just refactor your code a bit to get an overload for the `Execute` method that takes a `UIApplication` object. The `Execute` method in the `IExternalCommand` interface seems a bit obsolete anyway.

Also, another thing to bear in mind here is that you should subscribe to the `ApplicationInitialized` event if you are to do anything with documents.

Here is an example code snippet:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">Command</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;Execute(&nbsp;commandData.Application&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//Do&nbsp;all&nbsp;sorts&nbsp;of&nbsp;shiny&nbsp;stuff&nbsp;with&nbsp;your&nbsp;command.&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
}
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">RevitStartup</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalApplication</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;OnShutdown(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;application&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;OnStartup(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;application&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;application.ControlledApplication
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ApplicationInitialized
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+=&nbsp;ControlledApplication_ApplicationInitialized;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;ControlledApplication_ApplicationInitialized(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;sender,
&nbsp;&nbsp;&nbsp;&nbsp;ApplicationInitializedEventArgs&nbsp;e&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;command&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Command</span>();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//I&nbsp;never&nbsp;remember&nbsp;if&nbsp;the&nbsp;sender&nbsp;is&nbsp;Application&nbsp;or&nbsp;UIApplication</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;sender&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Execute(&nbsp;sender&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;command.Execute(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UIApplication</span>(&nbsp;sender&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;)&nbsp;);
&nbsp;&nbsp;}
}
</pre>

This will also work even if you don't register a button.

And to finish off, like Mike said: tell us why you want to achieve and maybe we can find an even easier way of doing it =)
 
**Response:** Many, many thanks for all your replies.

Although none of them answered the question they did help me realise that what I was trying to do was the wrong way to approach things and that I actually didn't need the external command.
 
My reason for thinking I needed the external command was trying to get the `Document` but thanks to Erik's post I found I could get the `UIApplication` from the sender and that this then let me access the document. So now I am able to run all of my code in the external application!
 
Again, many thanks for all your assistance.
 
To finalise, my end result was to register a dialog event on startup and shutdown and then:
 
<pre class="code">
  <span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;handle_DialogBoxShowing(
  &nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;sender,
  &nbsp;&nbsp;DialogBoxShowingEventArgs&nbsp;args&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;args.DialogId&nbsp;==&nbsp;<span style="color:#a31515;">&quot;Dialog_Revit_NewProject&quot;</span>&nbsp;)
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;=&nbsp;sender&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">UIApplication</span>;
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;app&nbsp;=&nbsp;uiapp.Application;
   
  &nbsp;&nbsp;<span style="color:green;">//&nbsp;etc&nbsp;etc&nbsp;etc</span>
</pre>

Many thanks to Mike, Matt, Erik and Gonçalo for raising and helping to solve this!

<center>
<img src="img/131_apples_500x375.jpg" alt="Apples" width="500">
</center>


####<a name="2"></a>Addendum &ndash; Load and Execute Code on the Fly

In the [comment below](http://thebuildingcoder.typepad.com/blog/2016/11/using-other-events-to-execute-add-in-code.html#comment-3039130901),
David Echols explains how he can load and execute assembly code on the fly by subscribing to the `Idling` event in the application `OnStartup` method:

> I have used the following code in the last four versions of Revit for overnight batch processing of PDF and DWG exports and sheet lists across multiple workstations. This is not the most elegant way to do this, but it does show how I load the assembly on the fly and execute the command that does the processing.

<pre class="code">
<span style="color:green;">//&nbsp;The&nbsp;event&nbsp;is&nbsp;added&nbsp;in&nbsp;IExternalApplication.OnStartup.</span>
<span style="color:green;">//&nbsp;Note:&nbsp;The&nbsp;handler&nbsp;is&nbsp;unloaded&nbsp;on&nbsp;the&nbsp;first&nbsp;line&nbsp;in&nbsp;</span>
<span style="color:green;">//&nbsp;the&nbsp;handler.&nbsp;The&nbsp;event&nbsp;is&nbsp;started&nbsp;again&nbsp;in&nbsp;the&nbsp;</span>
<span style="color:green;">//&nbsp;DocumentClosed&nbsp;event&nbsp;handler&nbsp;when&nbsp;the&nbsp;current&nbsp;</span>
<span style="color:green;">//&nbsp;document&nbsp;is&nbsp;closed.</span>
 
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;RemoteJobHandler(
&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">IdlingEventArgs</span>&nbsp;e&nbsp;)
{
&nbsp;&nbsp;GlobalSettings.RevitUiControlledApp.Idling&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;-=&nbsp;IdlingHandler.RemoteJobHandler;
 
&nbsp;&nbsp;LogManager.LogMessage(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;IdlingHandler.RemoteJobHandler&nbsp;-&nbsp;Enter&nbsp;handler&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;LogSeverityType.Debug&nbsp;);
 
&nbsp;&nbsp;Job&nbsp;job&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Job(&nbsp;GlobalSettings.RemoteJobXmlData&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;dllLocation&nbsp;=&nbsp;job.DllFileName;
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;className&nbsp;=&nbsp;job.CommandName;
&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;do&nbsp;a&nbsp;bunch&nbsp;of&nbsp;stuff&nbsp;here&nbsp;to&nbsp;setup</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ModelPath</span>&nbsp;modelPath&nbsp;=&nbsp;<span style="color:#2b91af;">ModelPathUtils</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ConvertUserVisiblePathToModelPath(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;item.RevitFileName&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;uiDocument&nbsp;=&nbsp;GlobalSettings.RevitUiApp
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OpenAndActivateDocument(&nbsp;modelPath,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;openOptions,&nbsp;<span style="color:blue;">false</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Type</span>&nbsp;type&nbsp;=&nbsp;LoadDllWithReflection(&nbsp;dllLocation,&nbsp;className&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;IRemoteCommand&nbsp;command&nbsp;=&nbsp;GetCommandFromType(&nbsp;type&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;command.RunRemotely(&nbsp;uiDocument.Document,&nbsp;item&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;<span style="color:#2b91af;">Exception</span>&nbsp;ex&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;handle/log&nbsp;errors</span>
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">finally</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;cleanup</span>
&nbsp;&nbsp;}
}
 
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Type</span>&nbsp;LoadDllWithReflection(&nbsp;
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;dllLocation,
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;className&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Assembly</span>&nbsp;assembly&nbsp;=&nbsp;<span style="color:#2b91af;">Assembly</span>.LoadFrom(&nbsp;dllLocation&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Type</span>&nbsp;type&nbsp;=&nbsp;assembly.GetType(&nbsp;className&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;type;
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;<span style="color:#2b91af;">ReflectionTypeLoadException</span>&nbsp;ex&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;exceptions&nbsp;=&nbsp;ex.LoaderExceptions;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;failedType&nbsp;<span style="color:blue;">in</span>&nbsp;ex.Types&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;failedType&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.WriteLine(&nbsp;failedType.FullName&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Exception</span>&nbsp;loadException&nbsp;<span style="color:blue;">in</span>&nbsp;exceptions&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.WriteLine(&nbsp;loadException.ToString()&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>;
}
 
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;IRemoteCommand&nbsp;GetCommandFromType(&nbsp;<span style="color:#2b91af;">Type</span>&nbsp;type&nbsp;)
{
&nbsp;&nbsp;IRemoteCommand&nbsp;command&nbsp;=&nbsp;<span style="color:#2b91af;">Activator</span>.CreateInstance(&nbsp;type&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;IRemoteCommand;
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;command&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;th&nbsp;row&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Exception</span>(&nbsp;<span style="color:#a31515;">&quot;Could&nbsp;not&nbsp;get&nbsp;Remote&nbsp;Command&quot;</span>&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;command;
}
</pre>
