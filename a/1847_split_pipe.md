<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://github.com/jeremytammik/Pipe-Split
  /a/src/rvt/Pipe-Split/

- split a conduit
  https://forums.autodesk.com/t5/revit-api-forum/trouble-of-my-own-implementation-of-breakcurve-on-conduit/m-p/8426899/highlight/false
  16561381 [Programatically Place Conduit Fitting]

- split pipe into lengths
  14201186 [Need to call command Split Element (SL) with defined filter and length]


twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Split Pipe Tutorial and Related Cases

Today, let's focus on splitting pipes and other things, starting with a nicely structured tutorial on splitting pipes shared
by Abdelaziz [Zizobiko25](https://github.com/Zizobiko25) Fadoul:

- [Abdelaziz' split pipe tutorial](#3)
- [Calling the SL split element command](#4)
- [Splitting a conduit](#5)

####<a name="3"></a> Abdelaziz' Split Pipe Tutorial

Hi folks,

Hope you are all safe and keeping well.

As there is no explicit SDK sample for splitting pipe instances into standard lengths without using the fabrication configurations, so I thought to share with you the below snippet. This is built upon a contribution that I came across on this platform, and uses functions that are developed on other samples (e.g. Find connected element, etc.), however, I thought to enhance it and explain the steps to achieve such a task. The fundamental idea for this is to delete the selected pipe and replace it with the creation of 2 pipes instead. 

1-	Establish a new transaction: As this is going to modify the Revit document, then we need to have it done within a transaction context.

<pre class="code">
  Transaction tx = new Transaction(activeDoc.Document);

  tx.Start("split pipe");

  ElementId systemtype = system.GetTypeId();

  SplitPipe(pipes[0], system, activeDoc, systemtype, pipeType);

  tx.Commit();

<span style="color:#2b91af;">Transaction</span>&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(activeDoc.Document);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(<span style="color:#a31515;">&quot;split&nbsp;pipe&quot;</span>);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;systemtype&nbsp;=&nbsp;system.GetTypeId();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SplitPipe(pipes[0],&nbsp;system,&nbsp;activeDoc,&nbsp;systemtype,&nbsp;pipeType);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
</pre>

2-	We then obtain the selected pipe basic data including its length, associated level Id and system Id.

<pre class="code">
  ElementId levelId = segment.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM).AsElementId();
  // system.LevelId;
  ElementId systemtype = _system.GetTypeId();

  Curve c1 = (segment.Location as LocationCurve).Curve;// selecting one pipe and taking its location.
  
  //Pipe diameter
  double pipeDia = UnitUtils.ConvertFromInternalUnits(segment.get_Parameter(BuiltInParameter.RBS_PIPE_DIAMETER_PARAM).AsDouble(), DisplayUnitType.DUT_MILLIMETERS);
</pre>

3-	We then compare the obtained length, including the fitting length, with the standard length that we want to assign (for instance 6m here). If it is longer than that, then we proceed. 

<pre class="code">
  //Standard length
  double l = 6000;

  //Coupling length
  double fittinglength = (1.1 * pipeDia + 14.4);

  // finding the length of the selected pipe.
  double len = UnitUtils.ConvertFromInternalUnits(segment.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), DisplayUnitType.DUT_MILLIMETERS);

  if (len <= l)
    return;
</pre>

4-	We then determine the splitting point by taking the fracture between the required length to the total length.

<pre class="code">
  var startPoint = c1.GetEndPoint(0);
  var endPoint = c1.GetEndPoint(1);
  
  
  XYZ splitpoint = (endPoint - startPoint) * (l / len);
  
  
  var newpoint = startPoint + splitpoint;
  
  
  Pipe pp = segment as Pipe;
  
  
  // Find two connectors which pipe's two ends connector connected to. 
  Connector startConn = FindConnectedTo(pp, startPoint);
  Connector endConn = FindConnectedTo(pp, endPoint);
</pre>

5-	We are then able to create the first and second pipe segments using the command create:

<pre class="code">
  // creating first pipe 
  Pipe pipe = null;
  if (null != _pipeType)
  {
    pipe = Pipe.Create(_activeDoc.Document, _pipeType.Id, levelId, startConn, newpoint);
  }
  
  
  Connector conn1 = FindConnector(pipe, newpoint);
  
  
  //Check + fitting
  XYZ fittingend = (endPoint - startPoint) * ((l + (fittinglength / 2)) / len);
  
  
  //New point after the fitting gap
  var endOfFitting = startPoint + fittingend;
  // creating second pipe
  
  Pipe pipe1 = Pipe.Create(_activeDoc.Document, systemtype, _pipeType.Id, levelId, endOfFitting, endPoint);
  
  
  // Copy parameters from previous pipe to the following Pipe. 
  CopyParameters(pipe, pipe1);
  //}
  
  
  Connector conn2 = FindConnector(pipe1, endOfFitting);
  _ = _activeDoc.Document.Create.NewUnionFitting(conn1, conn2);
  
  
  if (null != endConn)
  {
    Connector pipeEndConn = FindConnector(pipe1, endPoint);
    pipeEndConn.ConnectTo(endConn);
  }
</pre>

6-	Once this is done successfully, we can then delete the original pipe segment

<pre class="code">
  ICollection<Autodesk.Revit.DB.ElementId> deletedIdSet = _activeDoc.Document.Delete(segment.Id);
  
  if (0 == deletedIdSet.Count)
  {
    throw new Exception("Deleting the selected elements in Revit failed.");
  }
</pre>

Link to the full code is accessible here:

https://github.com/Zizobiko25/Pipe-Split.git

Hope this useful for someone. Give me a shout if you have any questions.

Thanks, Abdelaziz

####<a name="4"></a> Calling the SL Split Element Command

While on this topic, here is another related old case, 14201186 *Need to call command Split Element (SL) with defined filter and length*:

**Question:** I wanted to call the Split Element (SL) command through API and wants to give the filter to select only Pipe with predefined length to split.

Can you please help us with this?

**Answer:** You can call many built-in Revit commands using
the [`PostCommand` API](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.3).

However, this just launches the built-in command as is, prompting the user for all further input and other interaction.

Therefore, if you wish to drive this completely automatically, it will be hard to make use of the built-in command.

<!--
obsolete:

Here is a discussion of the basics of [creating and modifying pipes through the API](

http://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2015/ENU/Revit-API/files/GUID-0B91BAE8-59FD-4D4B-84E6-53B6A21DE00A-htm.html?_ga=2.49832166.1581756714.1525612885-2130181328.1465883366
-->

The specific question of splitting a pipe into shorter segments of a given length has been discussed repeatedly in the Revit API discussion forum:

- [Split a pipe at a specific lenght](https://forums.autodesk.com/t5/revit-api-forum/split-a-pipe-at-a-specific-lenght/m-p/5533100)
- [Pipe / Duct spliting using the Revit API](https://forums.autodesk.com/t5/revit-api/pipe-duck-spliting-using-the-revit-api/m-p/3312303)
- [Split Ducts with Union](https://forums.autodesk.com/t5/revit-api/split-ducts-with-union/m-p/5489135)


####<a name="5"></a> Splitting a Conduit

Another relasted case 16561381 *Programatically Place Conduit Fitting* on splitting a conduit:

**Question:** We would like to know how to programatically place a conduit fitting at a certain location on a piece of conduit.
Basically, we want to exactly what this tool does (screenshots included as well) but we do not want the user to have to click where they place it &ndash; we want to programatically control it's placement on a piece of conduit.

<center>
<img src="img/place_conduit_fitting_1.png" alt="Place conduit fitting" title="Place conduit fitting" width="529"/> <!-- 529 -->
<img src="img/place_conduit_fitting_2.png" alt="Place conduit fitting" title="Place conduit fitting" width="190"/> <!-- 190 -->
</center>

We have been unable to find anything within the API or forums which accomplish this. Please advise on how this can be done.

Tool help info:

- [Add Conduit Fittings](https://help.autodesk.com/view/RVT/2019/ENU/?guid=GUID-12A049FB-F779-4184-97A9-2E700945DE66)

**Answer:** I believe that one possible approach to achieve this would be to place the fitting in the appropriate position in the model first using `NewFamilyInstance`, and then connect it to the neighbouring conduits.
I would expect them to adjust automatically.

I discovered and demonstrated that approach in the series of posts
on [implementing a rolling offset](http://thebuildingcoder.typepad.com/blog/2014/01/final-rolling-offset-using-pipecreate.html).

To make sure I am not missing anything, I am checking with the development team for you as well.

Meanwhile, here are some other discussions on placing similar fittings:

- [Cable Tray Orientation and Fittings](http://thebuildingcoder.typepad.com/blog/2010/05/cable-tray-orientation-and-fittings.html)
- [Use of NewTakeOffFitting on a Duct](http://thebuildingcoder.typepad.com/blog/2011/02/use-of-newtakeofffitting-on-a-duct.html)
- [Set Elbow Fitting Type](http://thebuildingcoder.typepad.com/blog/2011/02/set-elbow-fitting-type.html)
- [Use of NewTakeOffFitting on a Pipe](http://thebuildingcoder.typepad.com/blog/2011/04/use-of-newtakeofffitting-on-a-pipe.html)
- [Simpler Rolling Offset Using NewElbowFitting](http://thebuildingcoder.typepad.com/blog/2014/01/newelbowfitting-easily-places-rolling-offset-elbow-fittings.html)
- [NewCrossFitting Connection Order](http://thebuildingcoder.typepad.com/blog/2014/10/newcrossfitting-connection-order.html)

The development team added:

You can use `Document.Create.NewElbowFitting` taking two `Connector` arguments.

Depending on the orientation of the MEP Curves it might fail and then require a Transition or a Union instead.

If there are three connectors, it requires a Tee; if there are four connectors, it requires a Cross.

It uses the family routing settings to assign the type and the `size_lookup` in the fitting family to determine the geometrical representation of the FamilyInstance.

So, in case of different diameters to connect, it will create other objects in between, such as transitions (e.g., for pipes or ducts) or a junction box (e.g., for conduits).

*Response:** We are already doing something similar, but it seems wildly inefficient, and I was hoping the API provided something we were missing as Pipe (and other curve based families) seem to have 'special' methods that conduit lacks.

[Trouble of my own implementation of BreakCurve on Conduit](https://forums.autodesk.com/t5/revit-api-forum/trouble-of-my-own-implementation-of-breakcurve-on-conduit/m-p/8426899)

To be sure, we did research this thoroughly but as noted above our implementation has a 'clunky' feel to it when compared to the ease of the fitting tool in the UI (although watching updater messages shows Revit may be handling it in a similar way we are). We are under a time crunch at the moment and hope this methodology will ease some of the 'heartburn' we are experiencing with fittings on sloped conduit as well.

**Answer:** Yes, it does indeed look as if the Revit API forum thread you pointed out covers it pretty well.

I'm afraid I have nothing constructive to add to that, and, as you say, Revit probably does something similar internally as well.

