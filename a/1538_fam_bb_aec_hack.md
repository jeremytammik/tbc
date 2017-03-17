<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- http://aechackathon-germany.de/

AEC Hackathon Munich sports an impressive speaker line-up #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/family_bb_hack_tu_mu

I briefly mentioned the AEC Hackathon in Munich yesterday.
Here is some more information on that, highlighting the exciting speaker line-up and target topics, plus a solution for determining the bounding box of an entire family
&ndash; AEC Hackathon Munich Topics and Speakers
&ndash; Family bounding box
&ndash; Continuous integration for RevitLookup? ...

-->

### Family Bounding Box and AEC Hackathon Munich

I briefly mentioned
the [AEC Hackathon in Munich](http://thebuildingcoder.typepad.com/blog/2017/03/events-uv-coordinates-and-rooms-on-level.html#3) yesterday.

Here is some more information on that, highlighting the exciting speaker line-up and target topics, plus a solution for determining the bounding box of an entire family and a suggestion to implement a continuous integration service for RevitLookup:

- [AEC Hackathon Munich Topics and Speakers](#2)
- [Family bounding box](#3)
- [Continuous integration for RevitLookup?](#4)


#### <a name="2"></a>AEC Hackathon Munich Topics and Speakers

The AEC Hackathon is coming to Germany, March 31 - April 2, at the Technical University in Munich, Germany.

I am unable to attend, unfortunately, and will miss it sorely.

It brings a weekend of geeking at its finest.
It gives those designing, building and maintaining our built environment, the opportunity to engage, collaborate and interact on multiple aspects of the building industry.
Not all built environment professionals have to code or have much background with tech.
Just come with your knowledge, an open mind, collaborative spirit, and willingness to solve Mon-Fri problems leveraging various kinds of technology.
 
We've shaped an exhilarating Program on Robotics, Generative Design, IOT, AR/VR and web services for you!
 
Check out some of our Speakers:

- [Matt Jezyk](http://aechackathon-germany.de/speaker/matt-jezyk/), Senior Product Line Manager, AEC Conceptual Design Products, Autodesk (USA)
- [Jaime Ronsales](http://aechackathon-germany.de/speaker/chelsie/), Senior Developer Consultant [FORGE](https://forge.autodesk.com/), Autodesk (USA)
- [Maximilian Thumfart](http://aechackathon-germany.de/speaker/maximilian-thumfart/), Developer
of [Grevit](http://grevit-dev.github.io/Grevit/), [Dynamo for Rebar](https://github.com/tt-acm/DynamoForRebar),
[Dynamo PDF](https://github.com/grevit-dev/DynamoPDF) (UK)
- [Robin-Manuel Thiel](http://aechackathon-germany.de/speaker/robin-manuel-thiel/) and [Malte Lantin](http://aechackathon-germany.de/speaker/malte-lantin/), Technical Evangelists, Microsoft Developer Experience (GER)
- [Sigrid Brell Cokcan](http://aechackathon-germany.de/speaker/robots-in-architecture/), IP/RWTH Aachen and [Johannes Braumann](http://aechackathon-germany.de/speaker/robots-in-architecture/), UFG Linz &ndash; [Robots in Architecture](http://www.robotsinarchitecture.org/)
- And many more...
 
Why you should not miss this Event: 

- Test out new cutting edge Technologies
- Connect with leading BIM Experts, Developers and Students
- Solve Real World Industry Problems
 
Did we get you excited?  If you like to learn more about the Event have a look at our website:

<center>
[www.aechackathon-germany.de](http://www.aechackathon-germany.de/)
</center>

How can you Engage in the Event?
 
1. Come on by
Join us for the weekend, observe, and offer your industry expertise to teams as needed. Encourage others in your company to join you as this is a great way to get exposed to new technologies and meet other innovators in top AEC firms.
è Register
 
2. Team up and solve an Industry Problems:  
Bring a problem you want to solve or company/industry challenge. The Friday Night Lightning Rounds are open to all that want to propose an idea and form a team around improving some element of the design, build, operational process. Past teams have created high-tech solutions for tracking tools, high End BIM Solutions, using virtual reality for jobsite safety training, and much more. Visit our YouTube channel to watch past team presentation.
 
3. Learn how to develop on Innovative Software Platforms: 
If you are an IT affinitive User or a Software Developer and you always wanted to learn how to develop on FORGE, Dynamo Studio, KUKA Robot, Microsoft Mixed Reality, IoT Hub, Azure Functions and Cognitive Services come and join our free Developer Workshops in the afternoon before the Event.
 
Take a look at our [Prep Workshops](http://aechackathon-germany.de/dev-prep-workshops/).
  
If you have any Questions, please have a look at the [Frequently asked Questions](http://aechackathon-germany.de/faq/).
 
<center>
<img src="img/2017_aec_hack_tu_muc.jpg" alt="AEC Hackathon at TU Munich" width="627"/>
</center>

#### <a name="3"></a>Family Bounding Box

Kevin [@kelau1993](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4517553) Lau shared a solution to
determine the bounding box of a family in the family document environment in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [family bounding box in family document](https://forums.autodesk.com/t5/revit-api-forum/family-boundingbox-in-family-document/m-p/6946049).

It looks very cool indeed to me, so I went ahead and created a complete Visual Studio project
and [FamilyBoundingBox GitHub repository](https://github.com/jeremytammik/FamilyBoundingBox) for it to make it more accessible.

Here is Kevin's code, taken from
the [FamilyBoundingBox Command.cs module](https://github.com/jeremytammik/FamilyBoundingBox/blob/master/FamilyBoundingBox/Command.cs):
 
<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Computes&nbsp;the&nbsp;effective&nbsp;&#39;BoundingBoxXYZ&#39;&nbsp;of&nbsp;the&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;whole&nbsp;family,&nbsp;including&nbsp;all&nbsp;combined&nbsp;and&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;individual&nbsp;forms.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>document<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;Revit&nbsp;document&nbsp;that&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;contains&nbsp;the&nbsp;family.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;effective&nbsp;&#39;BoundingBoxXYZ&#39;&nbsp;of&nbsp;the&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;whole&nbsp;family.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;ComputeFamilyBoundingBoxXyz(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;document&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;familyBoundingBoxXyz&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;<span style="color:#2b91af;">HashSet</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;genericFormExclusionSet
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">HashSet</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;();
 
&nbsp;&nbsp;familyBoundingBoxXyz
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;MergeGeomCombinationBoundingBoxXyz(&nbsp;document,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;familyBoundingBoxXyz,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;genericFormExclusionSet&nbsp;);
 
&nbsp;&nbsp;familyBoundingBoxXyz
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;MergeSolidBoundingBoxXyz(&nbsp;document,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;familyBoundingBoxXyz,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;genericFormExclusionSet&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;familyBoundingBoxXyz;
}
 
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Merge&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>boundingBoxXyz<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">&nbsp;with&nbsp;the&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;&#39;BoundingBoxXYZ&#39;s&nbsp;of&nbsp;all&nbsp;&#39;GeomCombination&#39;s&nbsp;in&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>document<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">&nbsp;into&nbsp;a&nbsp;new&nbsp;&#39;BoundingBoxXYZ&#39;.&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Collect&nbsp;all&nbsp;members&nbsp;of&nbsp;the&nbsp;&#39;GeomCombination&#39;s</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;found&nbsp;into&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>geomCombinationMembers<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>document<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;Revit&nbsp;&#39;Document&#39;&nbsp;to&nbsp;search&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;for&nbsp;all&nbsp;&#39;GeomCombination&#39;s.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>boundingBoxXyz<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;&#39;BoundingBoxXYZ&#39;&nbsp;to&nbsp;merge&nbsp;with.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>geomCombinationMembers<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">A&nbsp;&#39;HashSet&#39;&nbsp;to&nbsp;collect&nbsp;all&nbsp;of&nbsp;the</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;&#39;GeomCombination&#39;&nbsp;members&nbsp;that&nbsp;form&nbsp;the&nbsp;&#39;GeomCombination&#39;.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;new&nbsp;merged&nbsp;&#39;BoundingBoxXYZ&#39;&nbsp;of</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>boundingBoxXyz<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">&nbsp;and&nbsp;all&nbsp;&#39;GeomCombination&#39;s</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;in&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>document<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;MergeGeomCombinationBoundingBoxXyz(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;document,
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;boundingBoxXyz,
&nbsp;&nbsp;<span style="color:#2b91af;">HashSet</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;geomCombinationMembers&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;mergedResult&nbsp;=&nbsp;boundingBoxXyz;
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;geomCombinationCollector
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;document&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">GeomCombination</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GeomCombination</span>&nbsp;geomCombination&nbsp;<span style="color:blue;">in</span>&nbsp;geomCombinationCollector&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;geomCombinationMembers&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GenericForm</span>&nbsp;genericForm&nbsp;<span style="color:blue;">in</span>&nbsp;geomCombination.AllMembers&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;geomCombinationMembers.Add(&nbsp;genericForm.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;geomCombinationBoundingBox&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;geomCombination.get_BoundingBox(&nbsp;<span style="color:blue;">null</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;mergedResult&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mergedResult&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mergedResult.Min&nbsp;=&nbsp;geomCombinationBoundingBox.Min;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mergedResult.Max&nbsp;=&nbsp;geomCombinationBoundingBox.Max;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">continue</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;mergedResult&nbsp;=&nbsp;MergeBoundingBoxXyz(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mergedResult,&nbsp;geomCombinationBoundingBox&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;mergedResult;
}
 
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Merge&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>boundingBoxXyz<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">&nbsp;with&nbsp;the&nbsp;&#39;BoundingBoxXYZ&#39;s&nbsp;of</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;all&nbsp;&#39;GenericForm&#39;s&nbsp;in&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>document<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">&nbsp;that&nbsp;are&nbsp;solid&nbsp;into</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;a&nbsp;new&nbsp;&#39;BoundingBoxXYZ&#39;.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Exclude&nbsp;all&nbsp;&#39;GenericForm&#39;s&nbsp;in</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>genericFormExclusionSet<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">&nbsp;from&nbsp;being&nbsp;found&nbsp;in</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>document<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>document<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;Revit&nbsp;&#39;Document&#39;&nbsp;to&nbsp;search&nbsp;for&nbsp;all</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;&#39;GenericForm&#39;s&nbsp;excluding&nbsp;the&nbsp;ones&nbsp;in</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>genericFormExclusionSet<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>boundingBoxXyz<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;&#39;BoundingBoxXYZ&#39;&nbsp;to&nbsp;merge&nbsp;with.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>genericFormExclusionSet<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">A&nbsp;&#39;HashSet&#39;&nbsp;of&nbsp;all&nbsp;the</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;&#39;GenericForm&#39;s&nbsp;to&nbsp;exclude&nbsp;from&nbsp;being&nbsp;merged&nbsp;with&nbsp;in</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>document<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;new&nbsp;merged&nbsp;&#39;BoundingBoxXYZ&#39;&nbsp;of</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>boundingBoxXyz<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">&nbsp;and&nbsp;all&nbsp;&#39;GenericForm&#39;s&nbsp;excluding</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;the&nbsp;ones&nbsp;in&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>genericFormExclusionSet<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;in&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>document<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;MergeSolidBoundingBoxXyz(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;document,
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;boundingBoxXyz,
&nbsp;&nbsp;<span style="color:#2b91af;">HashSet</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;genericFormExclusionSet&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;mergedResult&nbsp;=&nbsp;boundingBoxXyz;
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;genericFormCollector
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;document&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">GenericForm</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;genericFormExclusionSet&nbsp;!=&nbsp;<span style="color:blue;">null</span>
&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;genericFormExclusionSet.Any()&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;genericFormCollector.Excluding(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;genericFormExclusionSet&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GenericForm</span>&nbsp;solid&nbsp;<span style="color:blue;">in</span>
&nbsp;&nbsp;&nbsp;&nbsp;genericFormCollector
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">GenericForm</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;genericForm&nbsp;=&gt;&nbsp;genericForm.IsSolid&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;solidBoundingBox
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;solid.get_BoundingBox(&nbsp;<span style="color:blue;">null</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;mergedResult&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mergedResult&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mergedResult.Min&nbsp;=&nbsp;solidBoundingBox.Min;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mergedResult.Max&nbsp;=&nbsp;solidBoundingBox.Max;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">continue</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;mergedResult&nbsp;=&nbsp;MergeBoundingBoxXyz(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mergedResult,&nbsp;solidBoundingBox&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;mergedResult;
}
 
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Merge&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>boundingBoxXyz0<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">&nbsp;and</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">paramref</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>boundingBoxXyz1<span style="color:gray;">&quot;</span><span style="color:gray;">/&gt;</span><span style="color:green;">&nbsp;into&nbsp;a&nbsp;new&nbsp;&#39;BoundingBoxXYZ&#39;.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>boundingBoxXyz0<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">A&nbsp;&#39;BoundingBoxXYZ&#39;&nbsp;to&nbsp;merge</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>boundingBoxXyz1<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">A&nbsp;&#39;BoundingBoxXYZ&#39;&nbsp;to&nbsp;merge</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;new&nbsp;merged&nbsp;&#39;BoundingBoxXYZ&#39;.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;MergeBoundingBoxXyz(
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;boundingBoxXyz0,
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;boundingBoxXyz1&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;mergedResult&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>();
 
&nbsp;&nbsp;mergedResult.Min&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Min(&nbsp;boundingBoxXyz0.Min.X,&nbsp;boundingBoxXyz1.Min.X&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Min(&nbsp;boundingBoxXyz0.Min.Y,&nbsp;boundingBoxXyz1.Min.Y&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Min(&nbsp;boundingBoxXyz0.Min.Z,&nbsp;boundingBoxXyz1.Min.Z&nbsp;)&nbsp;);
 
&nbsp;&nbsp;mergedResult.Max&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Max(&nbsp;boundingBoxXyz0.Max.X,&nbsp;boundingBoxXyz1.Max.X&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Max(&nbsp;boundingBoxXyz0.Max.Y,&nbsp;boundingBoxXyz1.Max.Y&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Math</span>.Max(&nbsp;boundingBoxXyz0.Max.Z,&nbsp;boundingBoxXyz1.Max.Z&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;mergedResult;
}
</pre>


#### <a name="4"></a>Continuous integration for RevitLookup?

Peter Hirn of [Build Informed GmbH](https://www.buildinformed.com) very kindly offered to implement
a [continuous integration](https://en.wikipedia.org/wiki/Continuous_integration) service
for [RevitLookup](https://github.com/jeremytammik/RevitLookup), similar to the daily builds provided
for [Dynamo](http://www.dynamobim.org/)
at [dynamobuilds.com](http://dynamobuilds.com/).

He raised
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [CI for RevitLookup](https://forums.autodesk.com/t5/revit-api-forum/ci-for-revit-lookup/m-p/6947111) to
discuss the issue.

> We're considering to set up a public CI for [RevitLookup](https://github.com/jeremytammik/RevitLookup).

> The output could be something like the [Dynamo builds](http://dynamobuilds.com).
 
> I'm thinking [Travis](https://travis-ci.org/) or [Jenkins](https://jenkins.io/) for the builds.

> We would provide the infrastructure and maintenance for this service.
 
> I'd love to get your feedback on this idea.

If you have any thought or suggestion on this, please participate in
the [CI for RevitLookup discussion](https://forums.autodesk.com/t5/revit-api-forum/ci-for-revit-lookup/m-p/6947111) and
let us know.

Thank you!

