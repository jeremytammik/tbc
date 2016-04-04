<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- jon Kabat-Zinn Achtsamkeit Glücksformel
  search for "Kabat-Zinn Achtsamkeit Glücksformel"
  http://www.srf.ch/play/tv
  https://en.wikipedia.org/wiki/Mindfulness
  https://en.wikipedia.org/wiki/Jon_Kabat-Zinn
  http://www.srf.ch/play/tv/sternstunde-philosophie/video/jon-kabat-zinn-achtsamkeit-die-neue-gluecksformel?id=a5475697-96e6-492c-8a82-dc92e9620581
  https://en.wikipedia.org/wiki/Mindfulness#Law
  https://en.wikipedia.org/wiki/Mindfulness#Government

- Brett Young of
  3-minute video on BuildingSP Demo - REAL Conference 2016
  https://www.youtube.com/watch?v=Xb9g9vufMcI
  This is a demonstration of Reality Computing, Building Information Modeling (BIM), and Generative Design. We algorithmically route MEPS systems through a mechanical room that has been characterized by a point cloud. More information at http://www.buildingsp.com and on twitter at @genmep and @youngbrettyoung.
  2-minute video on
  BuildingSP Demo: MEP Autorouting within Point Cloud and Autodesk Revit
  https://www.youtube.com/watch?v=yXz84VbQuZ8
  Published on 10 Mar 2016
  This demo shows the clash-free auto-routing of electrical conduit through both a point cloud and architectural geometry created in Revit. More information at http://www.buildingsp.com and on twitter at @youngbrettyoung and @genmep.

- 11571635 [API issue with linked files on open detached of central file from network drive]

- Keith White <keith.white@autodesk.com> Re: Revit API Question

- busy with Revit cases, as always, and wishing I could further pursue my TrackChangesCloud project, which I also want to continue towards database and cloud areas:

https://github.com/jeremytammik/TrackChangesCloud

By the way, it has already made its way into a first commercial application:

https://twitter.com/BNIM_IS/status/699971782904664064

Vipassana and Idling vs External Events #revitAPI #3dwebcoder  #adsk @VipassanaOrg #vipassana #meditation #bim

I am leaving for a ten-day Vipassana meditation retreat next week, so I'll cram in this quick Saturday post on the meditation retreat and mindfulness before I have too much to do getting prepared next week. It includes yet another discussion of Idling versus external events for modeless dialogues and dockable panels. Vipassana means 'seeing'. In this case, seeing is enhanced by concentration on my own self, my body, my mind, my thoughts.
No talking, no communication whatsoever, no Internet, no mobile devices, no pen and paper, no reading.
No input, no output.
Sitting...

-->

### Vipassana and Idling versus External Events

I am leaving for
a ten-day [Vipassana meditation retreat](http://www.sumeru.dhamma.org) next
week, so I'll cram in this quick Saturday post
on the [meditation retreat](#2)
and [mindfulness](#3) before
I have too much to do getting prepared next week.

It includes yet another discussion
of [Idling versus external events for modeless dialogues and dockable panels](#4).


#### <a name="2"></a>Ten-Day Vipassana Meditation Retreat

[Vipassana](https://en.wikipedia.org/wiki/Vipassan%C4%81) means 'seeing'.

In this case, seeing is enhanced by concentration on my own self, my body, my mind, my thoughts.

No talking, no communication whatsoever, no Internet, no mobile devices, no pen and paper, no reading.

No input, no output.

No distraction.

Sitting.

Time and space, listening inwards.

I already participated in the same ten-day course taught by [S. N. Goenka](http://en.wikipedia.org/wiki/S._N._Goenka) and organised
by [www.dhamma.org](http://www.dhamma.org)
after [Christmas 2009](http://thebuildingcoder.typepad.com/blog/2009/12/uk-electrical-schedule-sample.html)
and in [January 2010](http://thebuildingcoder.typepad.com/blog/2010/01/happy-new-year-2010.html).

It was by far the most relaxing thing I have ever done.

[Dhamma.org](http://www.dhamma.org) organises absolutely identical courses over the entire world, completely free of charge, completely volunteer driven.


#### <a name="3"></a>Jon Kabat-Zinn Mindfulness

Talking about meditation and relaxation, I will also
mention [Jon Kabat-Zinn](https://en.wikipedia.org/wiki/Jon_Kabat-Zinn),
who teaches [mindfulness](https://en.wikipedia.org/wiki/Mindfulness_(psychology)) and developed
the [mindfulness-based stress reduction](https://en.wikipedia.org/wiki/Mindfulness-based_stress_reduction) program
offered by medical centres, hospitals, and health maintenance organizations.

Interestingly, these techniques are also applicable, useful and extremely effective in the context
of [law](  https://en.wikipedia.org/wiki/Mindfulness#Law)
and [government](https://en.wikipedia.org/wiki/Mindfulness#Government).

He gave a very nice interview for the Swiss television channel [SRF](http://www.srf.ch/play/tv) in *Sternstunde Philosophie*:
[Jon Kabat-Zinn &ndash; Achtsamkeit &ndash; die neue Gluecksformel](  http://www.srf.ch/play/tv/sternstunde-philosophie/video/jon-kabat-zinn-achtsamkeit-die-neue-gluecksformel?id=a5475697-96e6-492c-8a82-dc92e9620581)?

Before diving into my own internal workings and doing something apparently extremely idle, let's take another look at the Revit API Idling and external events:


#### <a name="4"></a>Idling versus External Events for Modeless Dialogues and Dockable Panels

This question was raised by Miroslav Schonauer, Solution Architect for Autodesk Consulting, and answered by Arnošt Löbel, Senior Principal Engineer of the Revit development team.

**Miro:** For the first time I need to use a complex Modeless dialog in Revit. Eventually I will be using a RVT-docking dialog, as in the Revit SDK DockableDialogs sample or
the [simpler dockable panel sample](http://thebuildingcoder.typepad.com/blog/2013/05/a-simpler-dockable-panel-sample.html),
but for the time being I'm only prototyping the 'communication' between the dialogue and Revit, so exploring two options: external events vs. the Idling event, as implemented in the ModelessDialog projects in the Revit SDK Samples.

Can you please confirm:

**Q1)** As far as just the 'communication' between a modeless dialog and Revit is concerned, there is not really anything that IDockablePaneProvider additionally provides compared to a simple modeless Form, i.e., we still have the 2 options: External Event or Idling Event.

I've done some research on External event vs Idling Event and read Jeremy's analysis
on [replacing an Idling event handler by an external event](http://thebuildingcoder.typepad.com/blog/2013/12/replacing-an-idling-event-handler-by-an-external-event.html).

**Q2)** Can you please comment or expand on my conclusions:

<ol type="a">
<li>If one needs to be periodically checking external sources for whatever, an external event is much better than an Idling event.</li>
<li>If one needs just to react on triggering the controls in the modeless dialog, there is no real difference between an external event and the Idling event.</li>
</ol>

My case is (b), but just in case I still intend to use external event, I would finally like to know:

**Q3)** Does anyone see ANY potential advantage (in any area) of the Idling event over external events (I hope not ;-)?


**Arnošt:** Here are my answers:

**A1)** That is correct. There is no extra communication available for docking dialogs. Docking is the advantage, and possibly some focus control (I am guessing).

**A2)** In my opinion it is the other way around. I would use Idling for your (a) case and External event for your (b) case &ndash; the case you seem to be focused on. My reasoning is that if I want to periodically, and possibly quite often, check readiness of external data, Idling may be better because it can be more responsive (it has a flag for controlling how fast it should repeat). If, however, I do not expect my external data be updated so often, I would use an external event here too. For the case (b) &ndash; I would definitely use an EE.

**A3)** There is no real difference between those two besides the fact I already mentioned &ndash; Idling can be triggered more often (= several times during just one Idling.) Under the hood, though, both IE and EE start in the same routine deep inside Revit &ndash; OnIdling. There one branch serves subscribed External Event, and another servers Idling handlers.

Of course it goes without saying that EEs have the big benefit of not blocking anything when no communication with Revit is not needed at the moment. That was a huge disadvantage in many situations in which Idling had to be used and EE was not available yet. Since application needed to stay subscribed, Revit had to always call the handlers (even though they mostly had nothing to do) and just such calling and crossing the native/managed barrier had significant impact on the performance of the machine (for the processor had always something to do). External Events address this problem.

#### <a name="2"></a>Picking an Object from a Dockable Panel versus a Modeless Dialogue

**Aaron:** By the way, you cannot call the Selection.PickObject or PickElementsByRectangle methods by clicking a button in DockableDialog with EE or IE, because they trigger an Autodesk.Revit.Exceptions.InvalidOperationException saying, 'The active view is non-graphical and does not support capture of the focus for pick operations'.

When clicking a button in the dockable dialog, the active view will lose focus because the focus is on the dockable dialog, causing the exception to be thrown.

**Arnošt:** Naturally, this is not a problem directly related to either EE or IE. A dockable palette could easily experience the same problem when trying to pick something at a time of any other event (or any API context, in fact). I would qualify this as an issue with the picking functions.  It seems to me that the methods should be either extended by allowing the caller to specify a graphic window in which picking is to occur, or at the very least it should realize that the current view is not a graphic one and should thus automatically switch to the previously active graphic view instead.

Many thanks to Miro, Aaron and Arnošt for this clarification!

By the way, numerous previous discussions and a lot more background information and examples on this area are provided by The Building Coder topic group on [Idling and external events for modeless access and driving Revit from outside](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

Now I wish you a happy relaxing weekend, with both aliveness and idling time:

<center>
<img src="img/367_jeremy_idling_400x306.jpg" alt="Idling event" width="400">
</center>

I hope to get another post in next week before leaving for my meditation retreat:

<center>
<img src="img/377_jeremy_sitting_250x400.jpg" alt="External sitting event" width="250">
</center>
