<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- tbc birthday ten years

- Out of clutter, find Simplicity.
  From discord, find Harmony.
  In the middle of difficulty lies Opportunity.
  ALBERT EINSTEIN
  Three Rules of Work

- Revit API with WPF (New message on the Autodesk forums or ideas)
  https://forums.autodesk.com/t5/revit-api-forum/revit-api-with-wpf/m-p/8209618
https://stackoverflow.com/questions/51918495/revit-pick-element-from-winform
Your Windows form is presumably not running as a modal form within a valid Revit API context.

Consequently, you are trying to access Revit and its API from outside. This is basically not possible. A workaround exists via the use of an [external event](http://www.revitapidocs.com/2018/05089477-4612-35b2-81a2-89c4f44370ea.htm).

This issue is currently also being discussed in the [Revit API discussion forum](https://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread on [Revit API with WPF](https://forums.autodesk.com/t5/revit-api-forum/revit-api-with-wpf/m-p/8209618).

The official approach is presented in the Revit SDK sample ModelessDialog/ModelessForm_ExternalEvent.

Many other discussions and soutions are listed by The Building Coder in the topic group on [Idling and External Events for Modeless Access and Driving Revit from Outside](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

https://forums.autodesk.com/t5/revit-api-forum/using-mvvm-amp-wpf-with-transaction-amp-doc-regenerate/m-p/8207716
using mvvm & wpf with transaction & doc.regenerate()
[Q]
i'm working on simple command
it have wpf form bonded to class as per mvvm methods
i call the wpf from my command and it show up using wpf,show();
i use the form and change data then click the buttom to run command on viewmodel from my form as usual in mvvm to pick the objects and change its data
i can close my form
when selecting the object after that i can see that parameter data has changed as i want but document can't fell that changes and i can't generate the document
my problem is i cant use transaction.start on my mvvm command and ca't regenerate the document to see the changes in the tags
how can i solve that?
[A]
It sounds as if your WPF form is not running as a modal form within a valid Revit API context.
Consequently, you are trying to access Revit and its API from outside. This is basically not possible. A workaround exists via the use of an external event:
http://www.revitapidocs.com/2018/05089477-4612-35b2-81a2-89c4f44370ea.htm
This issue was recently discussed in another thread on Revit API with WPF:
https://forums.autodesk.com/t5/revit-api-forum/revit-api-with-wpf/m-p/8209618
The official approach is presented in the Revit SDK sample ModelessDialog/ModelessForm_ExternalEvent.
Many other discussions and solutions are listed by The Building Coder in the topic group on Idling and External Events for Modeless Access and Driving Revit from Outside:
http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28

Ten Years Anniversary and Revit add-ins using MVVM, WPF and WinForm in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/tbc10years

Today is The Building Coder's tenth birthday!
The first post was a warm welcome on August 22, 2008.
Very many thanks to the entire community for all your support, interest, comments and above all numerous contributions over the years!
Today, let's pick up the recurring topic of accessing the Revit API from a modeless context...

--->

### Ten Years of TBC and Revit API with MVVM, WPF and WinForm

Today is The Building Coder's tenth birthday!

The first post was
a [warm welcome on August 22, 2008](http://thebuildingcoder.typepad.com/blog/2008/08/welcome.html).

Very many thanks to the entire community for all your support, interest, comments and above all numerous contributions over the years!

A suitable quote for the day:

<center>
<i>Out of clutter, find Simplicity.
<br/>From discord, find Harmony.
<br/>In the middle of difficulty lies Opportunity.
<br/><br/>Albert Einstein &ndash; Three Rules of Work</i>
<br/><br/><img src="img/809_bee_in_lotus_580x500.jpg" alt="Bumble bee in lotus flower" width="290"/>
</center>

Today, let's pick up the recurring topic of accessing the Revit API from a modeless context.

It was discussed fruitfully in some depth in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit API with WPF](https://forums.autodesk.com/t5/revit-api-forum/revit-api-with-wpf/m-p/8209618).

I'll skip most of the answer, because it is convoluted.

The most important aspect, though, is short and sweet:

[The Revit API cannot be used in a modeless context](http://thebuildingcoder.typepad.com/blog/2015/12/external-event-and-10-year-forum-anniversary.html#2).

It can only be used in a valid Revit API context.

A valid Revit API context is provided within the handlers executed by Revit call-backs, and nowhere else.

For instance,
an [external command](http://www.revitapidocs.com/2018/ad99887e-db50-bf8f-e4e6-2fb86082b5fb.htm) defines
an `Execute` methods and registers itself with Revit.

Revit calls that method and supplies a valid Revit API context to it via its `ExternalCommandData` input argument.

A Windows form can be displayed either using `Show` or `ShowDialog`.

If you use `Show`, it becomes a modeless dialogue and all interaction which it is outside of any valid Revit API context.

If you use `ShowDialog`, it becomes modal and remains in the same thread that launched it, possibly inheriting a valid Revit API context.

If you happen to be in a modeless context, you can implement a solution to access a valid Revit API context by implementing
an [external event](http://www.revitapidocs.com/2018/05089477-4612-35b2-81a2-89c4f44370ea.htm) and calling that.

Similarly to the external command, it also defines an `Execute` methods and registers itself with Revit.

Revit calls that method and supplies a valid Revit API context to it when it is prompted by an external trigger.

The official approach for implementing an external event is demonstrated by a Revit SDK sample:

- ModelessDialog/ModelessForm_ExternalEvent

It may require some redesign of your application logic to use.

Ryan Singleton added some good real-world advice to this:

To add my experience to what Jeremy said (you need External Events to make your transactions work modelessly), I have built a dockable dialog with WPF that used a lot of external events. Autodesk recommends that you try to use modal dialogs as much as you can instead of modeless. It will make your project much easier.

However, I can tell you that a fully functional suite of tools with external events is possible. But you need to be sure that the added complexity is worth it.

If you are going to have multiple external events, I strongly recommend that you look at the Revit SDK sample for External Events specifically. It shows how to use enums and a handler to swap out external events so that you do not have a sloppy mess of external events in your application.

You can also have a modeless dialog that, when you press a button to start a transaction, will open a modal window (progress bar or some other window), and that window will start the transaction instead. Once done, it closes, and the user feels like they are using a modeless context. A system like that would be much easier to maintain.

I hope this helps.

Here are some other recent threads raising the same question:

- [Revit Pick element from WinForm](https://stackoverflow.com/questions/51918495/revit-pick-element-from-winform)
- [Using MVVM and WPF with transaction and `doc.regenerate`](https://forums.autodesk.com/t5/revit-api-forum/using-mvvm-amp-wpf-with-transaction-amp-doc-regenerate/m-p/8207716)

Many other discussions and solutions are listed by The Building Coder in the topic group
on [Idling and external events for modeless access and driving Revit from outside](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

