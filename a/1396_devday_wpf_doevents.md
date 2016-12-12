<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 11399855 [modeless dialog, external event and asynchronous API calls]
  WPF DoEvents sample code

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewAPI #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restAPI
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge
@AutodeskReCap @Adsk3dsMax
#revitAPI #bim #aec #3dwebcoder #adsk #adskdevnetwrk @jimquanci @keanw
#au2015 #rtceur
#eraofconnection

Revit API, Jeremy Tammik, akn_include

DevDay Conference in Munich and WPF DoEvents #revitAPI #bim #aec #3dwebcoder #adsk @AutodeskRevit #adskdevnetwrk @jimquanci @keanw

Let's talk about doing events, from two radically different perspectives, one great big external one and lots of teeny-weeny little internal ones.
I'll share some pictures from the European DevDay conference and snow in Munich today, then discuss a WPF issue that came up last week
&ndash; DevDay conference in Munich
&ndash; WPF DoEvents...

-->

### DevDay Conference in Munich and WPF DoEvents

Let's talk about doing events, from two radically different perspectives, one great big external one and lots of teeny-weeny little internal ones.

I'll share some pictures from the European DevDay conference and snow in Munich today, then discuss a WPF issue that came up last week:

- [DevDay conference in Munich](#2)
- [WPF DoEvents](#3)
- [Addendum on WPF versus WinForms](#4)
- [Addendum on Not Using Revit API within WPF `DataContext`](#5)
- [WPF element id converter](#6)


#### <a name="2"></a>DevDay Conference in Munich

Today we held the one and only European DevDay conference in Munich:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157662970001709" title="DevDay Conference Munich"><img src="https://farm2.staticflickr.com/1675/23827373054_eb50a70d72_n.jpg" width="320" height="240" alt="DevDay Conference Munich"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

[Jaime Rosales Duque](http://adndevblog.typepad.com/aec/jaime-rosales.html) came all the way from New York to help, and
[Jim Quanci](http://dances-with-elephants.typepad.com/blog/about-the-author.html) even further, from San Francisco.

We have participants from all over Europe and even some from India.

The next few days are dedicated to an abbreviated [Cloud Accelerator](http://autodeskcloudaccelerator.com) here that I am looking forward to very much.

Meanwhile, here is Maciej Szlek's WPF issue and solution:


#### <a name="3"></a>WPF DoEvents

Exactly two months back, we discussed [PickPoint with WPF and no threads attached](http://thebuildingcoder.typepad.com/blog/2015/11/pickpoint-with-wpf-and-no-threads.html).

Now another modeless WPF issue was raised and solved by [Maciej 'Max' Szlek](http://maciejszlek.pl):

**Question:** I'm creating a WPF addin that performs operations which will take much time.

During this time the add-in dialogue don't respond until the operation ends, even when calling the external event Raise method by view-model command.

Do you have some workaround to achieve dialogue responsively during performing API operations?

**Answer:** Yes, I have heard about similar issues in the past involving blocking of modeless WPF forms.

Unfortunately, I cannot find the relevant thread any longer.

The workarounds involved stuff like setting the window focus, e.g. using GetForegroundWindow and SetForegroundWindow, and allowing the WPF form to access the Windows message queue.

I think Revit was somehow blocking the message queue, for some reason.

I think the solution involved [calling the DoEvents method](http://stackoverflow.com/questions/5181777/use-of-application-doevents).

A couple of WPF issues came up in the forum in the past couple of years, e.g.:

- [Revit API preventing WPF window regeneration](http://forums.autodesk.com/t5/revit-API/revit-API-dll-preventing-wpf-window-regeneration/m-p/5545975)
- [WPF window loses control when Revit API displays an error](http://forums.autodesk.com/t5/revit-API/wpf-window-loses-control-when-revit-API-displays-an-error/m-p/5319853)
- [WPF tutorial using WPF in Winforms](http://tech.pro/tutorial/799/wpf-tutorial-using-wpf-in-winforms)

On the other hand, you can tell from these discussions that some people are successfully using WPF forms, and the development team are not aware of any issues with them.

I recommend [sticking with Windows Forms](http://forums.autodesk.com/t5/revit-API/winforms-or-wpf/m-p/5558289) if you have a choice.

Here is another recent article on [modeless WPF forms, PickPoint and multithreading](http://thebuildingcoder.typepad.com/blog/2015/11/pickpoint-with-wpf-and-no-threads.html), addressing other issues that also might be of interest to you,
an [older one on WPF](http://thebuildingcoder.typepad.com/blog/2012/10/ensure-wpf-add-in-remains-in-foreground.html#2), and
a [comment on triggering an event from Jon](http://thebuildingcoder.typepad.com/blog/2013/12/triggering-immediate-external-event-execute.html?cid=6a00e553e16897883301a3fd3c071c970b#comment-6a00e553e16897883301a3fd3c071c970b).

Later, one little addition; I searched for "[Revit API WPF DoEvents](https://duckduckgo.com/?q=revit+API+WPF+doevents)" and found this article
on [multithreading throwing exceptions in Revit 2015](http://thebuildingcoder.typepad.com/blog/2014/05/multithreading-throws-exceptions-in-revit-2015.html).

**Response:** I grabbed this [DoEvents implementation on StackOverflow](http://stackoverflow.com/a/11899439) to
"start work on a method that runs on the Dispatcher thread, and it needs to block without blocking the UI Thread... implement a DoEvents based on the Dispatcher itself".

I don't like it, it's not too elegant, it forces to
break [mvvm](https://de.wikipedia.org/wiki/Model_View_ViewModel) pattern, but it works.

If you would like to see my ugly non-refactored test code, clone
my [ExternalEventTests sample on BitBucket](https://bitbucket.org/kedziormsz/externaleventtests.git),
also [saved locally](zip/kedziormsz-externaleventtests-6c9eee0b346a.zip) here on The Building Coder.

I haven't tested it much yet but it seems to be stable.

You can run it from the add-in manager &ndash; lack of static references to IExternalApplication.

On the occasion it allows to check if raised external events are queued or running next to each other. They are queued which is gooood but I think safer would be adapting below solution to the one external event &ndash; we don't know how the API engine will change in the future... ;) [pattern for semi-asynchronous Idling API access](http://thebuildingcoder.typepad.com/blog/2010/11/pattern-for-semi-asynchronous-idling-API-access.html)...

Anyways thank you for your very accurate advice!

What would be in your circle of interest is WpfCommand.

WpfWindow contains a WPF implementation of the DoEvents method (in the way mentioned before) which is injected to ViewModel to minimize dependencies.

ViewModel contains 3 commands.

The first picks doors (it use part of my little cross API version framework) &ndash; simply to check how modeless WPF window would behave during hiding, picking and showing again.

The second and third flip doors &ndash; both have their own external event with different handler implementation. ViewModel's setStatus method calls injected DoEvents method.

If you have some more questions let me know.

Oh, and you can feel free to publish this solution on the blog of course, I don't have any problem with that ;)

**Answer:** When you mention picking and flipping doors, that reminds me of the Revit SDK ModelessDialog `ModelessForm_ExternalEvent` and `ModelessForm_IdlingEvent` samples.

Are you aware of those?

Is there any similarity, or is your sample completely unrelated to those?

**Response:** Yes, I'm aware of those, but my approach is quite different.

Congratulations to Max on solving this, and many thanks for sharing it here with us!


#### <a name="4"></a>Addendum on WPF versus WinForms

Jeroen Van Vlierden adds an update:

> Just to let you know: I noticed that you mentioned my thread on
a [WPF window losing control when Revit API displays an error](http://forums.autodesk.com/t5/revit-API/wpf-window-loses-control-when-revit-API-displays-an-error/m-p/5319853) above.

> I can add to this that I managed to work around the issue by converting the wpf form to a user control and using this user control in a winforms window with a wpf element host.

> That works fine.

> It was however disappointing that I was forced to do this after I spent a lot of work on my application.

> Converting the application to winforms is no longer an option, so I will stick to this for now.


#### <a name="5"></a>Addendum on Not Using Revit API within WPF `DataContext`

Hps Anave shares another nugget of WPF experience in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread 
on [Revit API DLL preventing WPF window regeneration](http://forums.autodesk.com/t5/revit-api-forum/revit-api-dll-preventing-wpf-window-regeneration/m-p/6742531):

> Well, after a very long time integrating WPF in my own programs, my recommendation is NOT to use any Revit API class inside the view model class where you assign to the WPF window's `DataContext`.

> If ever you want to pass or get any information coming from an element or a parameter, it is better to extract its element Id's `IntegerValue`, and, when you are done with the WPF window, just create an `ElementId` from the integer value you acquired from the WPF window.

> There may be other solutions out there but this is the solution I have so far.

Many thanks to Hps for sharing this!


#### <a name="6"></a>WPF Element Id Converter

In the same thread, Gonçalo Feio shared his WPF element id converter, saying, This works for me:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">ElementIdConverter</span>&nbsp;:&nbsp;IValueConverter
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">object</span>&nbsp;Convert(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;value,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Type</span>&nbsp;targetType,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;parameter,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CultureInfo&nbsp;culture&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;value&nbsp;<span style="color:blue;">is</span>&nbsp;R.ElementId&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;(&nbsp;value&nbsp;<span style="color:blue;">as</span>&nbsp;ElementId&nbsp;).IntegerValue;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;-1;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">object</span>&nbsp;ConvertBack(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;value,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Type</span>&nbsp;targetType,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;parameter,
&nbsp;&nbsp;&nbsp;&nbsp;CultureInfo&nbsp;culture&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;value&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:blue;">string</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;id;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">int</span>.TryParse(&nbsp;value&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:blue;">string</span>,&nbsp;<span style="color:blue;">out</span>&nbsp;id&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;ElementId(&nbsp;id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;ElementId.InvalidElementId;
&nbsp;&nbsp;}
}
</pre>

In this case, I expose a ElementId property in the view model.

You can also add validation to give some feedback to the end user.

Many thanks to Gonçalo for sharing this!

#### Hi
