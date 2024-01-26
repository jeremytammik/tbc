<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script> -->
<!-- https://highlightjs.org/#usage -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
</head>

<!---

- thoughts on revit precision
  SpatialElementGeometryCalculator not accurate
  https://forums.autodesk.com/t5/revit-api-forum/spatialelementgeometrycalculator-not-accurate/m-p/12417416

- need for fuzz:
  What is Fuzz?
  https://thebuildingcoder.typepad.com/blog/2023/03/uv-emergence-fuzz-and-the-get_-prefix.html#4
  Again, the Need for Fuzz
  https://thebuildingcoder.typepad.com/blog/2022/08/instances-in-room-and-need-for-fuzz.html#3
  It is very important for every programmer dealing with geometry and CAD to understand that it is impossible to exactly represent a floating point number in a digital computer. Hence, the need for fuzz when comparing two numbers:
  https://www.google.com/search?q=fuzz&as_sitesearch=thebuildingcoder.typepad.com
  To avoid any deviation in a series P1, P2, ... Pn of vertical points, you can proceed as follows. Pick one of the collinear vertical points. It does not matter which one it is, and any one will do, e.g., the bottom one. Let's assume that is P1 with coordinates (x1,y1,z1). Now, replace the entire series of points P1,...Pn by a modified series P1,Q2,Q3,...Qn by defining each Q as follows:
  Qi = (x1,y1,zi) for i = 2, 3, ... n
  In other words, define all the Q so that they lie exactly vertically above P1.
  Since the Pi are all supposed to be vertical, the difference between their x and y coordinates must be negligeable.
  So, ignore it, and Bob's your uncle. Good luck.

- valid api context
  https://autodesk.slack.com/archives/C0SR6NAP8/p1705566333752629
  Jeff Hotchkiss
  what is the correct & efficient way to validate one's execution of code is in a Revit API context? I can see in the internal C++ code of Revit that Revit API objects perform checks - how does one do this equivalent check in C#? Background in :thread:
  Answers by Dimitar Venkov

twitter:

the @AutodeskRevit #RevitAPI #BIM @AutodeskAPS @DynamoBIM

&ndash;  ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Context




**Question:**

**Answer:**

<pre><code>
</code></pre>

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 718 Pixel Width: 881 -->
</center>


####<a name="2"></a> thoughts on revit precision

thoughts on revit precision
SpatialElementGeometryCalculator not accurate
https://forums.autodesk.com/t5/revit-api-forum/spatialelementgeometrycalculator-not-accurate/m-p/12417416

####<a name="3"></a> need for fuzz:

need for fuzz:
What is Fuzz?
https://thebuildingcoder.typepad.com/blog/2023/03/uv-emergence-fuzz-and-the-get_-prefix.html#4
Again, the Need for Fuzz
https://thebuildingcoder.typepad.com/blog/2022/08/instances-in-room-and-need-for-fuzz.html#3
It is very important for every programmer dealing with geometry and CAD to understand that it is impossible to exactly represent a floating point number in a digital computer. Hence, the need for fuzz when comparing two numbers:
https://www.google.com/search?q=fuzz&as_sitesearch=thebuildingcoder.typepad.com
To avoid any deviation in a series P1, P2, ... Pn of vertical points, you can proceed as follows. Pick one of the collinear vertical points. It does not matter which one it is, and any one will do, e.g., the bottom one. Let's assume that is P1 with coordinates (x1,y1,z1). Now, replace the entire series of points P1,...Pn by a modified series P1,Q2,Q3,...Qn by defining each Q as follows:
Qi = (x1,y1,zi) for i = 2, 3, ... n
In other words, define all the Q so that they lie exactly vertically above P1.
Since the Pi are all supposed to be vertical, the difference between their x and y coordinates must be negligeable.
So, ignore it, and Bob's your uncle. Good luck.

####<a name="4"></a> Valid API Context

**Question:** What is the correct and efficient way to validate one's execution of code is in a Revit API context?
I can see in the internal C++ code of Revit that Revit API objects perform checks &ndash; how does one do this equivalent check in C#?

Background: we have a lot of complex multi-threaded app code for Revit.
The most common class of errors here fall into three cases outside of just not knowing about some Revit corner cases:

- Calling Revit from the wrong thread
- Calling Revit outside the API context
- Persisting & misusing Revit objects beyond their lifetime

This often goes awry when mixed in with modern coding practices like DI, async, etc.
I'm in the process of extending our low level libraries for using the Revit API to provide effectively Revit safe zones
&ndash; if you're in one of these, you can call Revit, if not, you need to marshal into one.
For example, a UI call might need to alter Revit and it's not immediately obvious if it's safe to do so without marshalling into an External Event.
The wrong thread is easy to detect.
Persisting Revit objects can be a mix of better defensive coding and PR review, but I've already got patterns that make it much harder to accidentally drop Revit objects into long-lived objects.
Detecting incorrect context looks to be a bit harder.

**Answer:** Any and all Revit API code will always be executed in an API context.
However, only the main execution thread can modify the Revit database.
You could probably use something along the lines of this to check if you are running in it:
[how to tell if a thread is the main thread in C#](https://stackoverflow.com/questions/2374451/how-to-tell-if-a-thread-is-the-main-thread-in-c-sharp)

All Revit elements have an `IsValidObject` property that can check if it's still connected to a native object.

**Response:**
Can you clarify why 'any and all C# code will always be executed in an API context'?
If I spin up a modeless UI in a Revit pane, the WPF context does not appear to be guaranteed safe to call Revit objects in.

**Answer:**
There are best practices like using an `IExternalEventHandler` for any multi-threaded code.
It's also best not to store Revit elements directly, but instead only their ElementIds or UniqueIds.
UniqueIds are strings, which are value objects.
`ElementId` is a class but is serializable.
Both can safely be used between threads.

[Revit.Async](https://github.com/KennanChan/Revit.Async/blob/master/README.md)
provides a really nice and in-depth explanation of this:

> Use Task-based asynchronous pattern (TAP) to run Revit API code from any execution context.

> If you have ever encountered a Revit API exception saying, "Cannot execute Revit API outside of Revit API context",
typically when you want to execute Revit API code from a modeless window, you may need this library to save your life.
A common solution for this exception is to wrap the Revit API code using `IExternalEventHandler` and
register the handler instance to Revit ahead of time to get a trigger (`ExternalEvent`).
To execute the handler, just raise the trigger from anywhere to queue the handler to the Revit command loop.
But there comes another problem...

**Response:**
Thanks &ndash; I am aware of all of these; our library uses similar techniques to `Revit.Async`, appreciated for the reinforcement, but perhaps an example will help.
Lets' say I subscribe to a Revit event, and hook that to some piece of internal logic.
That internal logic calls reentrantly into some other piece of code that needs to perform Revit functionality, e.g., altering the UI.
That same piece of code might also be called from outside a Revit event (external or internal), and therefore, as I understand it, would need to move to an external event.
As it stands, the developer has to follow that callstack for any time they connect these events up and hope they know whether they're inside Revit at the start or not.
What I'd like is a low-level dispatch that would be something like:

<pre><code>
if(InRevitContext)
   // Run code inline for efficiency
else
   // Not safe, must wrap code in external event and wait for it
</code></pre>

At a minimum a debug check to validate early and say, 'no, your code is wrong, you will get Revit failures', would be good.

**Answer:**
Not sure if such a check exists, sorry.
Getting the main thread's id inside your `IExternalAppication.OnStartup` might help with that.
But I'd recommend you always stick to the `ExternalEvent.Raise` pattern whenever you have non-modal windows or panels and not call the event handler directly.

**Response:**
Understood.
The challenge we have at the moment is that this yields sometimes a lot of small External Event Raises, that are running and queued independently.
My limited understanding is that in the current implementation of `Raise`, Revit runs all of these in idle time, which means in theory other addin code or other user interactions could alter state between the executions.
There's been concern raised that ideally they'd chain together so that we can be confident Revit state hasn't changed between them (since writing each event in a fashion that says 'check we're e.g. still on the same view we were in the last event' is non-trivial).
Maybe there's a workaround &ndash; if I have an UIApplication &ndash; is there something cheap I can call that will do that 'Cannot execute Revit API outside of Revit API context check'?
Will `IsValidObject` perform it with some level of guarantee?
(I have found that I can write 'be careful with your state, always use external events' in big bold letters in architectural guidelines as much as I like, junior devs or contractors will regularly write code, test the path, it works, ship it, and then later a different path executes it and it crashes..)

**Answer:**
What do you mean by "change state"?
No other plugin should be able to change the state of your plugin's objects.
If you mean, change the state of the Revit document and its elements, that is always a possibility and must be accounted for.
You cannot expect that the end user will only run your plugin, nor can you account for every possible combination of plugins and scripts that they might use.
What you can be sure of, is that when the execution of your plugin' starts, you can go through all the element ids that you stored and check each to make sure that an element with that ID still exists.
If it has been deleted, you can skip it and move on to the next one.
If you need more fine-grained control than this, look at the `IUpdater` interface instead.
Most of the time Revit crashes because of unhandled exceptions.
Try to execute all entry points in a try-catch context and log all exceptions.

**Response:**
Agreed; I'll see what I can do to further reinforce these patterns.
Always helpful to have these needs confirmed too!

Many thanks to Jeff Hotchkiss and Dimitar Venkov for this illuminating discussion.

####<a name="5"></a> Token-free Selective State Space Model

Is the lo#ng LLM tokenisatio#n night#m#are almost o#ver?

[MambaByte: Token-free Selective State Space Model](https://arxiv.org/html/2401.13660v1) reports:

> Token-free language models learn directly from raw bytes and remove the bias of subword tokenization. Operating on bytes, however, results in significantly longer sequences

> ... experiments indicate the computational efficiency of MambaByte compared to other byte-level models.

> ... findings establish the viability of MambaByte in enabling token-free language modeling.

