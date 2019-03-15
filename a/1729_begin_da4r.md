<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

Before getting started with Forge Design Automation for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/planforDA4R

Several people have recently asked how to get started with Forge Design Automation for Revit or DA4R.
I will not get into any nitty-gritty details, since they are well covered by the Forge Design Automation API documentation, which includes DA4R tutorials and samples.
Here are examples of two conversations on planning first steps for DA4R solutions
&ndash; Structural analysis with DA4R
&ndash; Implementing a DA4R RVT round trip...

linkedin:

Before getting started with Forge Design Automation for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/planforDA4R

Several people have recently asked how to get started with Forge Design Automation for Revit or DA4R.

I will not get into any nitty-gritty details, since they are well covered by the Forge Design Automation API documentation, which includes DA4R tutorials and samples.

Here are examples of two conversations on planning first steps for DA4R solutions

- Structural analysis with DA4R
- Implementing a DA4R RVT round trip...

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) recently

-->

### Before Getting Started with Design Automation

Several people have recently asked how to get started
with [Forge](https://forge.autodesk.com)
[Design Automation for Revit or DA4R](https://thebuildingcoder.typepad.com/blog/2018/11/forge-design-automation-for-revit-at-au-and-in-public.html).

I will not get into any nitty-gritty details, since they are well covered by
the [Forge Design Automation API documentation](https://forge.autodesk.com/api/design-automation-cover-page).

The official documentation is for the released version 2, which only supports DWG.
[Version 3](https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview) covers
RVT as well and
includes [DA4R tutorials and samples](https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit-samples).

In case you are interested in the approach I took myself to prepare an existing Revit add-in for DA4R, check out
the [IfcSpaceZoneBoundaries add-in](https://github.com/jeremytammik/IfcSpaceZoneBoundaries) and
the detailed information pointed out there.

Here are examples of two conversations on planning first steps for DA4R solutions:

- [Structural analysis with DA4R](#2) 
- [Implementing a DA4R RVT round trip](#3)

<center>
<img src="img/roomedit3dv3_running_2.png" alt="Moving a wall in the Forge viewer" width="771">
</center>

#### <a name="2"></a> Structural Analysis with DA4R

**Question:** I would like to extract the analytical structural model out of a Revit BIM via the Revit API.

That would include structural elements, e.g., steel beams (easy?), structural concrete elements (more complex?), connections types between elements, loads, degrees of freedom etc.

My goal is to connect my existing structural analysis application with the BIM.

**Answer:** This aspect of the BIM is handled by the Revit
API [`AnalyticalModel` class](https://apidocs.co/apps/revit/2019/b4466cf0-0fa0-1f67-d442-fdf0fb073fc9.htm).

The Revit SDK includes lots of Revit Structure specific material, e.g., some structure-specific information in the *Revit Structure* subfolder and a bunch of samples in the *Structural Analysis SDK*, including
the [structural extensions, REX and results builder](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.54).

Many discussions on Revit Structure specific issues are listed
in [The Building Coder `rst` tag](https://thebuildingcoder.typepad.com/blog/rst).

**Response:** Thank you very much for this.

I already have a product running on Revit that translates and connects the structural data to other products, specifically for analysis and optimization.  My query is aimed at large buildings that are modelled in the cloud.  In that case, the engineer would like to keep analysis data with the model (locally or in the cloud, but not mixed).  Hence the question whether we can extract the same information in Forge, i.e., keeping the analytical data in the cloud.  Once this has been processed, it can be read back into the Revit model (once again, all happening in the cloud).

**Answer:** Yes, you can extract the same data in the cloud from the RVT model using the Forge Design Automation for Revit API.

This analytical data will not be available in the Forge Viewer, since it has to be stripped from that to make it more performant and more generic, independent of the seed CAD model (RVT, DWG, etc.) and domain (architectural, structural, mechanical, etc.).

The data you extract can be stored locally or in the cloud in any format and/or database you like.

It can be integrated back into the Revit RVT model again, both on the desktop, using the Revit API, and in the cloud, using the Forge Design Automation for Revit API.

My favourite minimal roundtrip RVT data extraction, cloud storage, and reintegration sample is [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud).

#### <a name="3"></a> Implementing a DA4R RVT Round Trip

**Question:** I am working on a proof of concept for a Forge app providing functionality similar to
the [RvtMetaProp roundtrip properties](http://thebuildingcoder.typepad.com/blog/2017/09/use-forge-or-spreadsheet-to-create-shared-parameters.html) and
other implementations roundtripping data between a RVT BIM and cloud or Forge, cf. 
the [sample overview connecting desktop BIM and cloud](http://thebuildingcoder.typepad.com/blog/2017/10/rational-bim-programming-at-au-darmstadt.html#5).

This time, however, I would like to use Forge Design Automation for Revit to implement the transforms and the RVT BIM updating step.

Quite simple idea: enable the user to move walls in the Forge model via the Forge viewer.

My app stores the translations and Revit unique ids for each modified element, sends data to DA4R, updates the RVT BIM and hence the Forge model in the viewer.

The hard part for me is that I never used the Revit API before.

Could you point me to resources that will help me implement that part, please?

Do you see any technical difficulty in updating wall positions in DA4R based on input parameters?

Hopefully not :)

**Answer:** That sounds utterly cool and perfectly feasible.

The most important point for you as a programmer is: the Revit API is 100% event driven.

First, take a look at 
the [getting started material](https://thebuildingcoder.typepad.com/blog/about-the-author.html#2).

Then you can check out my
new [IfcSpaceZoneBoundaries add-in](https://github.com/jeremytammik/IfcSpaceZoneBoundaries).

Its functionality is very simple, extracting some room and zone data from an RVT BIM and exporting that to CSV.

It implements it:

- Initially as an external command, for manual testing,
- Then in the `ApplicationInitialised` event, for local automated testing, and
- Finally, in the `DesignAutomationReadyEvent`, for real DA4R deployment:

The repository documentation includes links to the blog posts discussing the process in more detail.

Good luck and have fun!

**Response:** Thanks for quick reply.

I already uploaded a demo plugin to DA4R following the Forge tutorials and ran an activity on my document.

Would you have a snippet that shows how to get the element from its unique id and apply a transform to it?

Or at least point me to the exact APIs and objects I need to get that done quickly?

I am planning to learn the Revit API but struggling with time for that proof of concept, so it would help if I could fast forward to get the transforms done in my plugin.

I will get back to the basics in more details later :)

**Answer:** Sure:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;uid&nbsp;=&nbsp;<span style="color:blue;">null</span>;&nbsp;<span style="color:green;">//&nbsp;UniqueId</span>
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;distance&nbsp;=&nbsp;12.34;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;tx&nbsp;=&nbsp;distance&nbsp;*&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisX;
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;uid&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">ElementTransformUtils</span>.MoveElement(&nbsp;doc,&nbsp;e.Id,&nbsp;tx&nbsp;);
</pre>

As I mentioned above, my favourite minimal roundtrip RVT data extraction, cloud storage, and reintegration sample
is [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud).

**Response:** Thanks for the help so far.

I could fake my way through Revit API.

My app is almost working, so I'm able to send transforms to DA4R from the Forge viewer.

However, depending on which elements are being moved, some constraints are being applied and prevent the transforms, cf. this message saying "Can't keep elements joined"...

<center>
<img src="img/cannot_keep_elements_joined.png" alt="Cannot keep elements joined" width="459">
</center>

Is there a way to delete the constraints or unjoin the element I am moving in DA4R?

Later: I found a way to get joined elements and tried to unjoin them.

To my great despair the error message still shows up ... any suggestions?

**Answer:** The easiest may be to implement a warning swallower like I did for the stairs example:

- [Swallowing StairsAutomation warnings](http://thebuildingcoder.typepad.com/blog/2018/09/swallowing-stairsautomation-warnings.html)
- [Auto-run an add-in for Design Automation](http://thebuildingcoder.typepad.com/blog/2018/09/auto-run-an-add-in-for-design-automation.html)

**Response:** This is what I ended up doing.

My wall editor round-trip with DA4R is working now :-)
