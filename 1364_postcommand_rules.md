<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 11091133 [Revit 2016 Service Pack 2,  results in exceptions (JVA/AP)]

- 11052512 [Revit 2016 SP2 causes InternalException]

#dotnet #csharp #geometry
#fsharp #dynamobim #python
#grevit
#responsivedesign #typepad
#ah8 #augi #au2015 #dotnet #dynamobim
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon #revitapi #3dwebcoder
#javascript
#au2015 #rtceur
#RestSharp #restapi
#au2015
#mongoosejs #mongodb #nodejs
#au2015 #rtceur

Revit API, Jeremy Tammik, akn_include

PostCommand causes Internal Exceptions #revitapi #bim #aec #3dwebcoder #adsk #adskdevnetwrk

For the past few years now, the development team has been adding various checks to prevent illegal use of Revit API entry points from outside a valid Revit API context. Read all about the valid Revit API context and how to access it &ndash; Idling and External Events for modeless access and driving Revit from outside &ndash; New checks were just added in the Revit 2016 service pack 2 which uncovered more long-standing illegal API calls
&ndash; CompHound connects desktop and cloud
&ndash; Do not call `PostCommand` from a modeless context
&ndash; Do Not Call `PostCommand` from an `Idling` Event Handler
&ndash; Vacation time...

-->

### PostCommand causes Internal Exceptions, a Break

Inscrutable are the ways of the Lord, and innumerable the illegal usage examples of the Revit API...

For the past few years now, the development team has been adding various checks to prevent illegal use of Revit API entry points from outside a valid Revit API context.

Read all about the valid Revit API context and how to access it in The Building Coder topic group
on [Idling and External Events for modeless access and driving Revit from outside](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

Apparently, some new checks were just added in the Revit 2016 service pack 2 which uncovered more long-standing illegal API calls, addressed below by our resident Revit API expert Arnošt Löbel:

- [CompHound connects desktop and cloud](#1)
- [Do not call `PostCommand` from a modeless context](#2)
- [Do Not Call `PostCommand` from an `Idling` Event Handler](#3)
- [Vacation time](#4)


#### <a name="1"></a>CompHound Connects Desktop and Cloud

I have been spending part of my time in the past few days working on
the [CompHound](https://github.com/CompHound/CompHound.github.io) project
for two presentations on connecting the desktop with the cloud at
[RTC Europe](http://www.rtcevents.com/rtc2015eu) in Budapest end of October and
[Autodesk University](http://au.autodesk.com) in Las Vegas in December.

Yesterday, I finally hooked up
the [LMV viewer](http://the3dwebcoder.typepad.com/blog/2015/10/comphound-viewer-and-authorisation-service.html#5) and
set up a [View and Data API](https://developer.autodesk.com)
[authorisation token server](http://the3dwebcoder.typepad.com/blog/2015/10/comphound-viewer-and-authorisation-service.html#3) for it.

I have therefore been posting more
to [The 3D Web Coder](http://the3dwebcoder.typepad.com) than here...

If you are interested in connecting Revit or any other desktop application with the cloud, you may be interested in that, and in the preceding, simpler,
[FireRating in the Cloud](https://github.com/jeremytammik/firerating) sample.

Now let's talk a bit about the pure desktop Revit API, before taking a break...


#### <a name="2"></a>Do Not Call PostCommand from a Modeless Context

**Question:**
Revit throws an internal exception when I call Revit API methods that have user interaction.

I can reproduce it like this:

1. Run in Revit 2016 Service Pack 2, build 20150714_1515.
2. Start a custom command via `PostCommand` from a modeless window.
3. From within the custom command call an API method that has user interaction, e.g., `PromptForFamilyInstancePlacement`, `PickObject`, `PickElementsByRectangle`.
4. An `InternalException` is thrown and Revit is left in a ‘frozen’ state, e.g.:

<pre>
Autodesk.Revit.Exceptions.InvalidOperationException was caught
HResult=-2146233088
Message=Unprocessed internal exception is caught.
Source=RevitAPIUI
StackTrace:
at Autodesk.Revit.UI.UIDocument.PromptForFamilyInstancePlacement(FamilySymbol familySymbol)

Autodesk.Revit.Exceptions.InternalException was caught
HResult=-2146233088
Message=A managed exception was thrown by Revit or by one of its external applications.
Source=RevitAPIUI
StackTrace:
at ?A0x86b480fb.PerformObjectsPick(ADocument* pADoc, ObjectType objectType, Boolean bMulSel, ISelectionFilter pSelFilter, String pUserDefindStatusPrompt, IList`1 pPreSelected)
at Autodesk.Revit.UI.Selection.Selection.PickObjects(ObjectType objectType, ISelectionFilter selectionFilter, String statusPrompt)
</pre>

**Answer by Arnošt:**
You say that your add-in is using `PostCommand` to call another external command that calls `PickObject`.

Now, regardless of this particular workflow leading to an exception (justified or not), we have an obligation to inform the user that this workflow is wrong and should be avoided. Just because we allow it (or rather pretend allowing it) does not make this workflow right or justified. If the user when in his external command posts another external command of his it means that the design of his application is flawed. There should never be a need for such an obscure complication. Since both commands belong to the same programmer (my assumption, but I believe I am correct), the programmer has far better means to call one command from another. Even if those two commands are in different assemblies, there are still ways of invoking one from the other. Using the posting mechanism and `Idling` to invoke functions in the same application is quite crazy, even if it is allowed. I do not think we should allow picking or posting a command from another posted external command, and to be perfectly honest, I am not even sure that we should allow posting external commands from regularly invoked external commands (but that is another topic).

**Response:**
We do NOT post an external command from within another external command!

Problem:

We have a MODELESS form. When the user clicks a button we want to start a command in Revit.

How do we get in the ‘best’ Revit context to call the Revit API?

In my opinion is that via `PostCommand`. Then our class that implements IExternalCommand is called and we get also the ExternalCommandData.

In order to get in the SAFEST way into the ‘BEST’ Revit context we perform some steps:

1. First we send 2 <ESCAPE> characters to the Revit window.
2. Then we have a timer 100 ms.
3. Then we call `CanPostCommand`. If it returns false we send again an <ESCAPE> character to the Revit window and have again a timer 100 ms.
4. If `CanPostCommand` returns true, we raise an ExternalEvent.
5. In the `IExternalEventHander.Execute` we call `PostCommand`.

If this is not correct please:

- Provide the OFFICIAL way how this should be done.
- Publish this in the SDK documentation.
- Let it be stable. Don’t change it each Revit version.

**Answer:**
I am sorry to say that your approach is not valid either.

It is actually even more illegal than I thought it was...

I am happy to inform you that the correct and valid approaches have been documented numerous times already, both in the Revit SDK samples and on The Building Coder blog.

A valid Revit API context is only provided by Revit API events for which you implement an event handler.

If you are calling `PostCommand` or even just `CanPostCommand` from a modeless dialogue, then you are NOT in a valid API context and that call can cause problems all in itself.

This applies to every single Revit API call, not just `PostCommand` or `CanPostCommand`.

The only correct way to achieve what you described above is by using an external event.

There are several ways to access a valid Revit API context from a modeless context.

The best place to start is to study the two ModelessDialog SDK samples, ModelessForm_ExternalEvent and ModelessForm_IdlingEvent.

As said, this topic has been extensively discussed by The Building Coder. Some posts are listed in the topic group
on [Idling and External Events for modeless access and driving Revit from outside](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

You might want to start at the end.

You will probably need to re-architect your add-in to use external events.

If you are averse to reading the discussions pointed to above, you can simply jump straight to the appropriate Revit SDK sample, `ModelessDialog` &rarr; `ModelessForm_ExternalEvent`, and work it out from there yourself.

It demonstrates the best practices accessing the Revit API from a modeless form via an external event.

Base your application on that and you can continue to avoid all additional reading.

#### <a name="3"></a>Do Not Call PostCommand from an Idling Event Handler

**Question:**
We have just observed that certain circumstances cause Revit add-in command to throw an internal exception.

This apparently happens always when calling the `PickObject` method from the external command code WHEN the corresponding command has been invoked with `UIApplication.PostCommand`:

<!---
<center>
<img src="img/command_failure_for_external_command.png" alt="Exception" width="400"/>
</center>
-->

Calling `PickObject` in a "normal" external command scenario seems to be working as expected.

**Answer by Arnošt:**
The same applies as above:

Regardless of this particular workflow leading to an exception (justified or not), we have an obligation to inform the user that this workflow is wrong and should be avoided. Just because we allow it (or rather pretend allowing it) does not make this workflow right or justified. If the user when in his external command posts another external command of his it means that the design of his application is flawed. There should never be a need for such an obscure complication. Since both commands belong to the same programmer (my assumption, but I believe I am correct), the programmer has far better means to call one command from another. Even if those two commands are in different assemblies, there are still ways of invoking one from the other. Using the posting mechanism and `Idling` to invoke functions in the same application is quite crazy, even if it is allowed. I do not think we should allow picking or posting a command from another posted external command, and to be perfectly honest, I am not even sure that we should allow posting external commands from regularly invoked external commands (but that is another topic).

**Response:**
I think I need to explain the scenario where we are using the technique. I believe it’s perfectly reasonable but I also understand that it’s testing the limits of Revit API. I would be more than happy if you could show me better way of doing it.

We have a command that allows user to give certain options that she wants to use during the (i.e.) duct drawing. After the options are accepted, our add-in enables those and starts Revit’s own duct drawing method. Same time an `Idling` event has been registered to inform our code, when the drawing ends.

There can be various reasons why drawing ends, one which tells that user actually wants to do some additional things we offer. These functionalities we offer are implemented within a separate command. Therefore we use `PostCommand` from the `Idling` event handler to launch external command properly. As there’s no way of constructing an ExternalCommandData object manually, we have decided to use `PostCommand` to deal the command invoke.

I created a [two-minute screencast recording](http://screencast.com/t/YMsRQJJGV) that demonstrates using our add-in in practice in Revit 2015, where everything works well.

**Answer by Arnošt:**
My advice to you is still: Please try to modify your application so posting a command and then picking objects from it is not necessary. Technically, as far as I know, posting an external command should never be necessary. If the customer already knows that an external command exists and where, and also knows what the command does, then the customer most likely knows how the command is implemented. Thus invoking the command (or another main method that the command executes) via dynamic binding not only solves this customer's problem, but is also a preferred programming technique. By the way, this is also how things are done inside Revit.

**Response:**
I understand the criticism against the design, where one external command post for another. Yes. It would be crazy as I understand that two or more external commands could be chained more easily.

But there’s a misunderstanding here! Although I made the example code for you where another command was posted from another one, that’s not the actual case we have! In our case we post the command from the `Idling` event. From there it’s the only way of invoking our own external command properly I think. If I’m wrong, please tell me asap :-).

I’m launching our external command from the `Idling` event handler (and yes, the symptoms are the same).

How can I do that properly without `PostCommand`?

I know that doing it is pretty obvious actually, but it means that we would need to cache the ExternalCommandData object. It doesn’t feel reliable. Other possibility is to refactor the related external command class to offer alternative interface, where only mandatory set of data is needed. But this is not a small thing to do, as every command has been build assuming that it will get complete and fresh set of data from the ExternalCommandData. Using `PostCommand`, the advantage IMO is that the invoked command is guaranteed to get fresh data from the Revit, just like in a command invocation normal scenario.

This is my honest feedback to API team: The current fact that we should not use the `PostCommand` for external commands is not in any means obvious! It’s totally other way around. The `PostCommand` API supports calling other commands than those listed in the enumeration!! We have even read tips on The Building Coder how to get the command id for the external command. I have to admit I’m a bit disappointed about the current situation :-(.

**Answer by Arnošt:**
You say, "We have even read tips on The Building Coder how to get the command id for the external command. I have to admit I’m a bit disappointed about the current situation."

The Building Coder is not an official support site run by Autodesk and/or the Revit development team. Jeremy can pretty much publish whatever he wants, whether it is correct, valid, officially supported, or not. Just because something is on Building Coder does not make it a supported way. We certainly do not mean to restrict users in what they can do, but we cannot support just any idea they might chose to pursue.

You say, "The current fact that we should not use the `PostCommand` for external commands is not in any means obvious! It’s totally other way around."

I agree that some may see it that way. On the other hand, I could also claim (without even remotely trying be controversial), that it was not all obvious to us that users would ever try posting an external command. The fact that doing so is such an obscure technique kind of put it out of our radar and concerns. We simply did not consider it because they are other, standard ways of invoking code that is from the same add-in (or even in another add-in). And that is my honest point of view.

You say, "But this is not a small thing to do, as every command has been build assuming that it will get complete and fresh set of data from the ExternalCommandData."

Actually, I do not think it is necessary to instantiate ExternalCommandData in order to execute a method that implements an external command. It depends on how the external command is structured. I seriously doubt that external commands need anything else from ExternalCommandData other than the UIApplication object or the currently active document. I am quite confident that is the majority of external commands out there, and definitely all of those which do not need journaling (not many those exist), which does not look like it’s the case.

The way I personally structure my external commands is (simplified):

<pre classs="code">
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> ExecuteImpl( commandData.Application );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> ExecuteImpl( <span class="teal">UIApplication</span> uiapp )
&nbsp; {
&nbsp; &nbsp; <span class="green">// .. implementation of the commands goes here</span>
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
</pre>

Naturally, other ways are possible &ndash; for example I have a lot of tests that need only the DB `Application` instead or just the current DB `Document`. However, the main idea here is that the implementation (typically a DB part of the command) is separated from the command itself. By structuring my code this way I am free to invoke the implementation routine from anywhere I wish, within or out of the command’s assembly.

I typically do not use the ‘message’ and ‘elements’ arguments of external commands, but if I need them I can easily pass them to my implementation routine too along with the application instance.

The "application instance" is available pretty much in any situation when Revit invokes an add-in. For example in the `Idling` event the application is the ‘sender’ of the event. Thus, by simply casting the ‘sender’ to `UIApplication` the programmer has enough data to call a command’s execution routine with.

You say, "We have a command that allows user to give certain options which he/she wants to use during the (i.e.) duct drawing. After the options are accepted, our add-in enables those and starts Revit’s own duct drawing method. Same time an `Idling` event has been registered to inform our code, when the drawing ends."

This is pretty risky workflow, although I admit it is quite popular among our users (and popularized by Jeremy too). Again, without trying to dictate users what they may or may not do, I hope that my twenty plus years of experience as a software engineer will persuade most of them to not use such a technique. The problem is manifold:

1. In most cases it is not obvious to decide when (and even if) a command ends.
2. It cannot be guaranteed that the `Idling` handler which is waiting for the command to finish is the only `Idling` handler waiting for his chance. Imagine a session with five `Idling` handlers with each wanting to post commands or external commands. Order of their execution could be pretty much random, thus none of them could ever be sure what has really happened since the original command was posted (the one the `Idling` handler is waiting for to finish).

You say, "Revit 2015 is used where everything works well."

Actually, not everything in Revit 2015 works well. Besides other issues, there had been multiple problems with posting commands, which was why we had to use significantly different implementation, now available in 2015UR2. That is actually our right to improve our product. I am very sorry, I really am, that some workflows have been broken by the fix. It was totally not our intention. Again, it never had occurred to us that someone might actually depend on a workflow in which an external command is posted and especially several times (PickObject is kind of posted command too).

Naturally, this discussion could continue for much longer, one’s argument put against the others. However, I believe our stand is quite reasonable. There is a very reasonable way to invoke one command’s execution routine, and that way most likely satisfies most developers. On the other hand, a workflow that involves recursively posting commands is quite technically challenging to implement and thus, in my personal opinion at least, does not justify the need.

**Response:**
Thank you all for your detailed answers.

There has been a lot of discussion around the issue, so I agree it’s time for actions now. But still it has been very useful discussion. It’s much easier to understand the circumstances you have had.

It’s easy to agree on those technical approaches.

We can probably fix everything and get satisfactory results.

It just requires work that we haven’t scheduled  :-(


#### <a name="4"></a>Vacation Time

I'm leaving on vacation for two weeks.

<center>
<img src="/j/photo/jeremy/2008/2008-10-28_la_garnatilla/jeremy_in_wave_2.jpg" alt="Jeremy in a wave" width="300"/>
</center>

Take care!