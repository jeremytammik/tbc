<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- REX -- https://autodesk.slack.com/archives/C0SR6NAP8/p1651840528454499

- stefan dobre https://autodesk.slack.com/archives/C0SR6NAP8/p1651735070039959

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Structural Questions and Future of REX

Today, we ponder some structural rebar questions and the future of the REX structural Revit extensions included with the Revit SDK:

- [Future of REX](#2)
- [Rebar API questions](#3)
- [GetCustomDistributionPath](#4)
- [Number of segments](#5)
- [IsRebarInSection](#6)

####<a name="2"></a> Future of REX

Tomasz Wojdyla  14:35
Hi Team!
Tomek is here :wink: I am a fairly new PO for Robot Structural Analysis trying to find myself in the organization - sorry for silly questions - there could be more than a few in the next weeks and months :wink:
Recently we started to wonder about the future for Revit Extension Framework (aka. REX) which we support in yearly basis even though (we believe) there is no internal use of this component anymore. External use is some sort of the question mark - it would be great if we could collect some data about its (external) users. We know one for sure and 2 more who are more than likely no longer with REX/Autodesk toolstack. Is there any chance you may share some info on this topic, maybe know some ADN users?
All info or advisory is greatly appreciated!:thankyou:
Cheers,
TW

However, whatever we decide to do, I would suggest proceeding in the standard Revit API manner by posting very clear signs in the SDK announcing that REX is deprecated in one release and then removing it as obsolete in the next, one year later.

Jeremy Tammik  5 days ago
Alternatively, how about simply sharing the entire REX code base right now and telling the developer community: here guys, this is what we have, we will no longer deal with it, it is completely unsupported, do what you like with it?

Jeremy Tammik  5 days ago
In my (naive) eyes, that would cost zero effort and have immediate effect.

Tomasz Wojdyla  5 days ago
Thank you for feedback Jeremy! At this moment all options are open. As we discussed it with team the 1 year notice manner is clear. I will post on the forums. Of course any more thoughts from everyone are welcome. Thanks again!

####<a name="3"></a> Rebar API Questions

Miguel [MiguelGT17](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/5130624) Gutierrez raised a number of rebar questions in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) that were very kindly answered
by Stefan Dobre, ‪senior principal engineer of the Revit Structural development team:

1. [GetCustomDistributionPath from RebarFreeFormAccessor](https://forums.autodesk.com/t5/revit-api-forum/getcustomdistributionpath-from-rebarfreeformaccessor/td-p/11148790)
2. [Number of segments](https://forums.autodesk.com/t5/revit-api-forum/number-of-segments/td-p/11148840)
3. [IsRebarInSection()](https://forums.autodesk.com/t5/revit-api-forum/isrebarinsection/td-p/11148854)

<!--
4. [AreElementsValidForMultiReferenceAnnotation](https://forums.autodesk.com/t5/revit-api-forum/areelementsvalidformultireferenceannotation/td-p/11148745)
-->

He responded: After a quick look on these questions, I don't see any problems.
It is just a misunderstanding of the API and how the rebar works.
I answered in the forum and submitted a change request *REVIT-191469* to update the description and (maybe) the name of the `Rebar.IsRebarInSection` function.

Many thanks to Stefan foir the quick solutions!

####<a name="4"></a> GetCustomDistributionPath

First, how to
call [GetCustomDistributionPath from RebarFreeFormAccessor](https://forums.autodesk.com/t5/revit-api-forum/getcustomdistributionpath-from-rebarfreeformaccessor/td-p/11148790)?

**Question:** Is there a way to group up rebars without using the `RebarContainer` command and loading the distribution path data?

<center>
<img src="img/mg_distributionpath_1.png" alt="Distribution path" title="Distribution path" width="100"/> <!--  -->
</center>

Furthermore, There is something strange going on when creating a rebar container.
Its distribution path is not correlated to the actual true distribution path:

<center>
<img src="img/mg_distributionpath_1.png" alt="Distribution path" title="Distribution path" width="100"/> <!--  -->
</center>

**Answer to Question 1:** There are two types of Free Form rebar:

The one created from curves, and which doesn’t have any constraints to the host. The input curves for each bar in set can be in any position and it is not possible to set a distribution path for it. An example of such rebar is the sketched free form. To create such Rebar you should call       public static Rebar CreateFreeForm(Document doc, RebarBarType barType, Element host, IList<CurveLoop> curves, out RebarFreeFormValidationResult error);

The one created through a server (callback), and which has constraints to the host. Any time when the constraints are modified, the server is called to recompute the Rebar curves. During this calculation, a distribution path can be set too. This distribution path is a list of curves. Aligned and Surface distributions are examples of such free form. To create such Rebar, you should use public static Rebar CreateFreeForm(Document doc, Guid serverGUID, RebarBarType barType, Element host). Look also at the documentation for IRebarUpdateServer class. In the RevitSDK there is a sample that demonstrates how such a free form rebar can be created.

**Answer to Question 2:** RebarContainerItem is created from a Rebar element (which is a set).

It has its own number of bars and of course it will have its own distribution path which is the source rebar’s distribution path.

RebarContainer is just storing a list of RebarContainerItems without having any relations between them.

**Response:** Appreciate those comments;
indeed, they are very deep insights about question 1.

Concerning question 2 reply, I'm worried about it.
I'll find the best approach to reach my goal with the information you have provided so far.
Stay blessed.

####<a name="5"></a> Number of Segments

Next, Miguel raises a question on 
the [number of segments](https://forums.autodesk.com/t5/revit-api-forum/number-of-segments/td-p/11148840):

**Question:** This set of rebars has been sketched as a free form.

<center>
<img src="img/mg_nr_segments_1.png" alt="Number of segments" title="Number of segments" width="100"/> <!--  -->
</center>

RevitLookup shows a single segment for this bar:

<center>
<img src="img/mg_nr_segments_2.png" alt="Number of segments" title="Number of segments" width="100"/> <!--  -->
</center>

This is not true:

<center>
<img src="img/mg_nr_segments_3.png" alt="Number of segments" title="Number of segments" width="100"/> <!--  -->
</center>

Moreover, the `IsRebarInSection(view)` command always return false, regardless of the view.
So, I am not going to have the appropriate data when I sketch rebars as freeform?

**Answer:** As I can see in your images, you have a free form rebar that has the Workshop Instructions parameter set to Keep Straight. This means that no matter what curves the free form has, it will always be matched with a straight shape (M_00).

If you want the bar to be matched with other shapes, you should set the workshop parameter to Bend. One you set this option, each bar in the set will be matched with the existing rebar shapes from the project. If it doesn't match with any existing shapes, it will try to create new Rebar Shapes. If it can’t create new Rebar Shapes and error message will be posted and will continue to consider the bar as a straight one.

For more details on how the shape matching is working  you can have a look on this: https://knowledge.autodesk.com/support/revit/learn-explore/caas/CloudHelp/cloudhelp/2019/ENU/Revit-M...

Rebar.IsRebarInSection(View view) returns true only if the view is a section or elevation and the view plane is cutting at least one of the rebar curves, false otherwise. This API function is the correspondent of this UI option:

<center>
<img src="img/mg_nr_segments_4.png" alt="IsRebarInSection" title="IsRebarInSection" width="100"/> <!--  -->
</center>

**Response:** Thanks for your prompt reply; my bad, I was not aware of those parameters.
I will double check them and perform another test this weekend.
Cheers!

####<a name="6"></a> IsRebarInSection

Finally, on [IsRebarInSection()](https://forums.autodesk.com/t5/revit-api-forum/isrebarinsection/td-p/11148854):

**Question:** I have placed a set of rebars in a viewPlan that only has 1 segment:

MiguelGT17_0-1651726217067.png

I was expecting the `IsRebarInSection` method to return a `true` Boolean, as the rebars are shown as a cross section.
If that is not the case, what does this method stand for, and which API method should I be looking up instead?

Test code:

<pre class="code">
  <span style="color:#8f08c4;">foreach</span>&nbsp;(Element&nbsp;<span style="color:#1f377f;">element</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;rebars)
  {
    <span style="color:#8f08c4;">if</span>&nbsp;(element&nbsp;<span style="color:blue;">is</span>&nbsp;RebarContainer)
    {
   
    }
    <span style="color:#8f08c4;">else</span>&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(element&nbsp;<span style="color:blue;">is</span>&nbsp;Rebar)
    {
   
      Rebar&nbsp;<span style="color:#1f377f;">el</span>&nbsp;=&nbsp;element&nbsp;<span style="color:blue;">as</span>&nbsp;Rebar;
      <span style="color:#8f08c4;">if</span>&nbsp;(el.IsRebarFreeForm()&nbsp;==&nbsp;<span style="color:blue;">true</span>)
      {
   
      }
      <span style="color:#8f08c4;">else</span>&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(el.IsRebarShapeDriven()&nbsp;==&nbsp;<span style="color:blue;">true</span>)
      {
        RebarShapeDrivenAccessor&nbsp;<span style="color:#1f377f;">acc</span>&nbsp;=&nbsp;(element&nbsp;<span style="color:blue;">as</span>&nbsp;Rebar).GetShapeDrivenAccessor();
        XYZ&nbsp;<span style="color:#1f377f;">dir</span>&nbsp;=&nbsp;acc.GetDistributionPath().Direction;
   
        <span style="color:blue;">double</span>&nbsp;<span style="color:#1f377f;">angle</span>&nbsp;=&nbsp;dir.AngleTo(view.ViewDirection);
        angle&nbsp;=&nbsp;angle&nbsp;*&nbsp;(180&nbsp;/&nbsp;Math.PI);&nbsp;<span style="color:green;">//90,0</span>
        stb.AppendLine(angle.ToString());
        stb.AppendLine(el.IsRebarInSection(view).ToString());
      }
    }
    TaskDialog.Show(<span style="color:#a31515;">&quot;dd&quot;</span>,&nbsp;stb.ToString());
  }
</pre>

**Answer:** `IsRebarInSection` returns true only if the view is a section or elevation and the view plane is cutting at least one of the rebar curves, false otherwise.

This API function is the correspondent of this UI option:

<center>
<img src="img/mg_nr_segments_4.png" alt="IsRebarInSection" title="IsRebarInSection" width="100"/> <!--  -->
</center>

In your case, to see that the straight bar is shown as a point you can verify this on your own.
You can get the centerline curves like this:

<pre class="code">
  rebar.GetTransformedCenterlineCurves(
    <span style="color:blue;">false</span>,&nbsp;<span style="color:blue;">true</span>,&nbsp;<span style="color:blue;">true</span>,
    MultiplanarOption.IncludeOnlyPlanarCurves,
    0);
</pre>

You will get only one line. If the line’s direction is parallel with view’s direction it means that the bar is shown as a cross section, false otherwise.


<!-- 
####<a name="7"></a> AreElementsValidForMultiReferenceAnnotation

- [AreElementsValidForMultiReferenceAnnotation](https://forums.autodesk.com/t5/revit-api-forum/areelementsvalidformultireferenceannotation/td-p/11148745)

-->