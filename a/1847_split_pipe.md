<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

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

### Split Pipe Tutorial

####<a name="2"></a> 

**Question:** 

<center>
<img src="img/" alt="" title="" width="100"/> <!-- 3093 -->
</center>

**Answer:**

**Response:**

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
