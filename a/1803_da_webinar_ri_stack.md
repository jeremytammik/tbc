<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---


twitter:

- post the news about Design Automation webinars
  https://forge.autodesk.com/blog/design-automation-api-webinar-series-register-now
  /a/doc/revit/tbc/git/a/img/da_for_air3.png
 
- Nice sample showing two stacked ribbon items versus 3:
  24x24 StackedItems
  https://forums.autodesk.com/t5/revit-api-forum/24x24-stackeditems/m-p/9168470

- Very important hint from Fair59 on reinitialising the filtered element collector
  https://forums.autodesk.com/t5/revit-api-forum/collection-of-elements-created-using-elementworksetfilter-giving/m-p/9164018
  This misunderstanding caused a similar problem in another recent case...

Painless introduction to Forge Design Automation API, creating two stacked ribbon items, reinitialising filtered element collectors and picking a face in a linked file using the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/pickfaceinlink

The ultimatively painless introduction to the Forge Design Automation API, a solution for creating two stacked ribbon items, reinitialising filtered element collectors and picking a face in a linked file
&ndash; Forge Design Automation API webinars
&ndash; Stacking two 24x24 ribbon items
&ndash; Reinitialising the filtered element collector
&ndash; Use <code>CreateReferenceInLink</code> to select a face in a linked file...

linkedin:

Painless introduction to Forge Design Automation API, creating two stacked ribbon items, reinitialising filtered element collectors and picking a face in a linked file using the #RevitAPI

http://bit.ly/pickfaceinlink
 
- Forge Design Automation API webinars
- Stacking two 24x24 ribbon items
- Reinitialising the filtered element collector
- Use CreateReferenceInLink to select a face in a linked file...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Design Automation API, Stacks, Collectors and Links

If you are looking for the ultimatively painless introduction to the Forge Design Automation API, you are in luck: the Design Automation webinars are coming up soon.

If you are more interested in the desktop Revit API, the solution for creating two stacked ribbon items instead of three might be more to your taste.

In either case, the tip on reinitialising a filtered element collector before reusing it is importantissimo in both contexts:

- [Forge Design Automation API webinars](#2)
- [Stacking two 24x24 ribbon items](#3)
- [Reinitialising the filtered element collector](#4)
- [Use `CreateReferenceInLink` to select a face in a linked file](#5)

<center>
<img src="img/da_for_air3.png" alt="Forge Design Automation API for AutoCAD, Inventor, Revit and 3DS Max" width="400"> <!--800-->
</center>

####<a name="2"></a> Forge Design Automation API Webinars

A new series of webinars on
the [Forge Design Automation APIs for AutoCAD, Inventor, Revit and 3DS Max](https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/) is
coming up.

You can register now to participate and also to gain access to the recordings that will be posted after the live events.

There is still time to get hands on with the new Design Automation APIs.
Four webinars are scheduled in December that will cover each of the Design Automation APIs in depth.
The recordings will be posted after so be sure to register to take advantage.

All webinars begin at 8:00 AM Pacific Standard Time.

- <u>AutoCAD</u> &ndash; December 5
<br/>Design Automation API for AutoCAD on Forge &ndash; <u>Albert Szilvasy</u>, software Architect will share details about the new and updated API.
<br/>This webinar discusses how the Design Automation for AutoCAD empowers customers and partners to get more work done more quickly, reliably and collaboratively using cloud-based web services.
We will demonstrate the ability to run scripts on your design files, leveraging the scale of the Forge Platform to automate repetitive tasks.
&ndash; [Register](https://autodesk.zoom.us/webinar/register/WN_n-yZWaSNSW-OIMJRDdJBZw)
- <u>Revit</u> &ndash; December 10
<br/>Design Automation API for REVIT on Forge &ndash; <u>Sasha Crotty</u>, Senior Product Manager, Revit Platform & Services to share updates on this API.
<br/>This webinar discusses how the Design Automation for Revit empowers customers and partners to get more work done more quickly, reliably and collaboratively using cloud-based web services.
We will demonstrate how you can automate your most common, manual, and error-prone work to improve responsiveness and free up your time so you can focus on more valuable work. 
&ndash; [Register](https://autodesk.zoom.us/webinar/register/WN_50PU3thnSfC8m-Rh2PE2Ag)
- <u>Inventor</u> &ndash; December 11
<br/>Design Automation API for Inventor on Forge &ndash; <u>Andrew Akenson</u>, software Architect will shares details about the new and updated APIs added to the Design Automation for Inventor on Forge.
<br/>This webinar discusses how the Design Automation for Inventor empowers customers and partners to get more work done more quickly, reliably and collaboratively using cloud-based web services.
We will demonstrate how you can automate your most common, manual, and error-prone work to improve responsiveness and free up your time so you can focus on more valuable work.
&ndash; [Register](https://autodesk.zoom.us/webinar/register/WN_8poFofy4QWCfq0ciL0AYjg)
- <u>3DS Max</u> &ndash; December 12
<br/>Design Automation API for 3ds Max on Forge &ndash; <u>Kevin Vandecar</u>, Developer Advocate on the Forge Partner Development team will share details about the newly launched Design Automation API for 3ds Max on Forge.
<br/>In this webinar, we will discuss how the Design Automation for 3ds Max empowers customers and partners to get more work done more quickly, reliably and collaboratively using cloud-based web services.
Using automation routines, you can build custom solutions using 3ds Max in the cloud.
No local resources are needed, so it could be a commercial website/configurator type webapp, or it could be a pipeline automation that run from your in-house tools.
The sky (or rather the cloud) is the limit.
We will demonstrate how you can easily automate common workflows in 3ds Max.
&ndash; [Register](https://autodesk.zoom.us/webinar/register/WN_7jTFtqz3Tte76LswrUACvw)


####<a name="3"></a> Stacking Two 24x24 Ribbon Items 

Jameson Nyp, BIM Manager and IS Director at [Telios Engineering](https://teliospc.com) in Dallas, Texas, shares a nice solution for stacking two ribbon items in
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [24x24 StackedItems](https://forums.autodesk.com/t5/revit-api-forum/24x24-stackeditems/m-p/9168470):

**Question:** This may be an easy one, but so far I am struggling to find anything specific about it.

How do you make a `StackedItem` where the icons are 24x24 when there are only 2 in the stack?

It seems like it should be possible, as it is used multiple times in the modify tab:

<center>
<img src="img/modify_tab_icon_sizes.png" alt="Modify tab icon sizes" width="246"> <!--246-->
</center>

I have been able to set the `ShowText` property to false to get the 3 stacked icons, but when I use the same methodology with the 2-icon stack, it remains 16x16, regardless of the icon resolution.

I tried to obtain and change the button's height and width, minWidth and minHeight through the Autodesk.Window.RibbonItem object to no avail.

Has anyone had any success in creating these icons?

**Answer:** I found a solution.

In order to display the button at the 24x24 size, the Autodesk.Windows.RibbonItem.Size needs to be manually set to Autodesk.Windows.RibbonItemSize.Large enum and a 24x24 icon needs to be set to the button's `LargeImage` property.

Here is a code sample:

<pre class="code">
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.UI;
<span style="color:blue;">using</span>&nbsp;Autodesk.Windows;
<span style="color:blue;">using</span>&nbsp;System.Collections.Generic;
<span style="color:blue;">using</span>&nbsp;System.IO;
<span style="color:blue;">using</span>&nbsp;System.Reflection;
<span style="color:blue;">using</span>&nbsp;System.Windows.Media.Imaging;
<span style="color:blue;">using</span>&nbsp;YourCustomUtilityLibrary;
 
<span style="color:blue;">namespace</span>&nbsp;ReallyCoolAddin
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">StackedButton</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">IList</span>&lt;Autodesk.Revit.RibbonItem&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Create(&nbsp;<span style="color:#2b91af;">RibbonPanel</span>&nbsp;ribbonPanel&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;Assembly</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Assembly</span>&nbsp;assembly&nbsp;=&nbsp;<span style="color:#2b91af;">Assembly</span>.GetExecutingAssembly();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;assemblyLocation&nbsp;=&nbsp;assembly.Location;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;DLL&nbsp;Location</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;executableLocation&nbsp;=&nbsp;<span style="color:#2b91af;">Path</span>.GetDirectoryName(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;assemblyLocation&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;dllLocationTest&nbsp;=&nbsp;<span style="color:#2b91af;">Path</span>.Combine(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;executableLocation,&nbsp;<span style="color:#a31515;">&quot;TestDLLName.dll&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;Image</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;pb1Image&nbsp;=&nbsp;UTILImage.GetEmbeddedImage(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;assembly,&nbsp;<span style="color:#a31515;">&quot;Resources.16x16_Button1.ico&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;pb2Image&nbsp;=&nbsp;UTILImage.GetEmbeddedImage(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;assembly,&nbsp;<span style="color:#a31515;">&quot;Resources.16x16_Button2.ico&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;pb1LargeImage&nbsp;=&nbsp;UTILImage.GetEmbeddedImage(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;assembly,&nbsp;<span style="color:#a31515;">&quot;Resources.24x24_Button1.ico&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;pb2LargeImage&nbsp;=&nbsp;UTILImage.GetEmbeddedImage(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;assembly,&nbsp;<span style="color:#a31515;">&quot;Resources.24x24_Button2.ico&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;Button&nbsp;Name</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;buttonName1&nbsp;=&nbsp;<span style="color:#a31515;">&quot;ButtonTest1&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;buttonName2&nbsp;=&nbsp;<span style="color:#a31515;">&quot;ButtonTest2&quot;</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;push&nbsp;buttons</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PushButtonData</span>&nbsp;buttondata1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttonName1,&nbsp;buttonTextTest,&nbsp;dllLocationTest,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Command1&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttondata1.Image&nbsp;=&nbsp;pb1Image;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttondata1.LargeImage&nbsp;=&nbsp;pb1LargeImage;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PushButtonData</span>&nbsp;buttondata2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttonName2,&nbsp;buttonTextTest,&nbsp;dllLocationTest,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Command2&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttondata2.Image&nbsp;=&nbsp;pb2Image;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttondata2.LargeImage&nbsp;=&nbsp;pb2LargeImage;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;StackedItem</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;Autodesk.Revit.RibbonItem&gt;&nbsp;ribbonItem&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;ribbonPanel.AddStackedItems(&nbsp;buttondata1,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttondata2&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Find&nbsp;Autodes.Windows.RibbonItems</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;UTILRibbonItem&nbsp;utilRibbon&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;UTILRibbonItem();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;btnTest1&nbsp;=&nbsp;utilRibbon.getButton(&nbsp;<span style="color:#a31515;">&quot;Tab&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Panel&quot;</span>,&nbsp;buttonName1&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;btnTest2&nbsp;=&nbsp;utilRibbon.getButton(&nbsp;<span style="color:#a31515;">&quot;Tab&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Panel&quot;</span>,&nbsp;buttonName2&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;Size&nbsp;and&nbsp;Text&nbsp;Visibility</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;btnTest1.Size&nbsp;=&nbsp;<span style="color:#2b91af;">RibbonItemSize</span>.Large;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;btnTest1.ShowText&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;btnTest2.Size&nbsp;=&nbsp;<span style="color:#2b91af;">RibbonItemSize</span>.Large;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;btnTest2.ShowText&nbsp;=&nbsp;<span style="color:blue;">false</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Return&nbsp;StackedItem</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;ribbonItem;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

Many thanks to Jameson for raising and solving this issue.


####<a name="4"></a> Reinitialising the Filtered Element Collector

Yet another important hint
from Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen
on reinitialising the filtered element collector
for [collection of elements created using `ElementWorksetFilter` giving incorrect count](https://forums.autodesk.com/t5/revit-api-forum/collection-of-elements-created-using-elementworksetfilter-giving/m-p/9164018):

You need to reinitialise a filtered element collector before reusing it.
All the filters that you add to it are accumulated.
If they are mutually exclusive, you will get zero results.

**Question:** I'm trying to retrieve empty worksets, but the count method of the collection of elements in a particular workset is not giving correct results.
Here is my code:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;fec&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredWorksetCollector</span>&nbsp;fwc&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredWorksetCollector</span>(&nbsp;doc&nbsp;);
 
&nbsp;&nbsp;fwc.OfKind(&nbsp;<span style="color:#2b91af;">WorksetKind</span>.UserWorkset&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;msg&nbsp;=&nbsp;<span style="color:#a31515;">&quot;&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;count&nbsp;=&nbsp;0;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Check&nbsp;Empty&nbsp;Worksets&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Workset</span>&nbsp;w&nbsp;<span style="color:blue;">in</span>&nbsp;fwc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementWorksetFilter</span>&nbsp;ewf&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementWorksetFilter</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;w.Id,&nbsp;<span style="color:blue;">false</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;elemIds&nbsp;=&nbsp;fec.WherePasses(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ewf&nbsp;).ToElementIds();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;foundElems&nbsp;=&nbsp;elemIds.Count;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Elements:&quot;</span>,&nbsp;w.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;:&nbsp;&quot;</span>&nbsp;+&nbsp;foundElems.ToString()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;foundElems&nbsp;==&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;count++;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;msg&nbsp;+=&nbsp;count.ToString()&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.&nbsp;&quot;</span>&nbsp;+&nbsp;w.Name&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\n&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;count&nbsp;==&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;msg&nbsp;=&nbsp;<span style="color:#a31515;">&quot;None&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Empty&nbsp;Worksets:&nbsp;&quot;</span>,&nbsp;msg&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;t.Dispose();
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;<span style="color:#2b91af;">Exception</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Error&quot;</span>,&nbsp;e.ToString()&nbsp;);
&nbsp;&nbsp;}
</pre>

**Answer:** A `FilteredElementCollector` isn't a static variable, but a dynamic collection.

Every time you apply a filter, the elements that don't pass the filter are removed from the collection.

So, after the first pass of the `foreach` loop, the collector only contains the elements belonging to the first workset.

All those elements aren't part of the second workset (2nd pass) and therefore the collector is empty after the second pass. 

Solution: reinitialize the collector in every pass:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Workset</span>&nbsp;w&nbsp;<span style="color:blue;">in</span>&nbsp;fwc&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementWorksetFilter</span>&nbsp;ewf&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementWorksetFilter</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;w.Id,&nbsp;<span style="color:blue;">false</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;elemIds&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;ewf&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToElementIds();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;foundElems&nbsp;=&nbsp;elemIds.Count;
 
&nbsp;&nbsp;&nbsp;&nbsp;count++;
 
&nbsp;&nbsp;&nbsp;&nbsp;msg&nbsp;+=&nbsp;foundElems.ToString()&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.&nbsp;&quot;</span>&nbsp;+&nbsp;w.Name&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\n&quot;</span>;
&nbsp;&nbsp;}
</pre>

Many thanks to Fair59 for yet another invaluable hint.

By the way, you might also want
to [simplify your transaction handling by wrapping it in a `using` statement](https://thebuildingcoder.typepad.com/blog/2012/04/using-using-automagically-disposes-and-rolls-back.html).
However, I also wonder whether you need any transaction at all for this read-only operation.

This misunderstanding caused a similar initial problem in another recent case involving 
a [material assets collector for appearance, structural (physical) and thermal](https://forums.autodesk.com/t5/revit-api-forum/material-assets-collector-appearance-structural-physical-amp/m-p/7256944).

####<a name="5"></a> Use CreateReferenceInLink to Select a Face in a Linked File

Back in 2012, we discussed a pretty convoluted solution 
for [selecting a face in a linked file](https://thebuildingcoder.typepad.com/blog/2012/05/selecting-a-face-in-a-linked-file.html#comment-4704876157).

Joshua Lumley added a [comment](https://thebuildingcoder.typepad.com/blog/2012/05/selecting-a-face-in-a-linked-file.html#comment-4704877758) to
that old post, pointing out that:

> The `CreateReferenceInLink` was added after that discussion, in Revit 2014.

> To select any face anywhere, all you need is this:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;SelectFace(&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uiapp.ActiveUIDocument.Document;
 
&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Document</span>&gt;&nbsp;doc2&nbsp;=&nbsp;GetLinkedDocuments(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc&nbsp;);
 
&nbsp;&nbsp;Autodesk.Revit.UI.Selection.<span style="color:#2b91af;">Selection</span>&nbsp;sel&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;uiapp.ActiveUIDocument.Selection;
 
&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;pickedRef&nbsp;=&nbsp;sel.PickObject(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Autodesk.Revit.UI.Selection.<span style="color:#2b91af;">ObjectType</span>.PointOnElement,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Please&nbsp;select&nbsp;a&nbsp;Face&quot;</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;elem&nbsp;=&nbsp;doc.GetElement(&nbsp;pickedRef.ElementId&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Type</span>&nbsp;et&nbsp;=&nbsp;elem.GetType();
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">RevitLinkType</span>&nbsp;)&nbsp;==&nbsp;et&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">RevitLinkInstance</span>&nbsp;)&nbsp;==&nbsp;et&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Instance</span>&nbsp;)&nbsp;==&nbsp;et&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;d&nbsp;<span style="color:blue;">in</span>&nbsp;doc2&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;elem.Name.Contains(&nbsp;d.Title&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;pickedRefInLink&nbsp;=&nbsp;pickedRef
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.CreateReferenceInLink();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;myElement&nbsp;=&nbsp;d.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pickedRefInLink.ElementId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;myGeometryObject&nbsp;=&nbsp;myElement
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetGeometryObjectFromReference(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pickedRefInLink&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Face</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;myGeometryObject;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;myElement&nbsp;=&nbsp;doc.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pickedRef.ElementId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;myGeometryObject&nbsp;=&nbsp;myElement
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetGeometryObjectFromReference(&nbsp;pickedRef&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Face</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;myGeometryObject;
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>;
}
</pre>

Many thanks to Joshua for this important update.
