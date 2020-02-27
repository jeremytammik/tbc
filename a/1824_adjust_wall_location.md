<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- Adjusting Wall.LocationCurve.Curve results in unexpected behaviour
  https://forums.autodesk.com/t5/revit-api-forum/adjusting-wall-locationcurve-curve-results-in-unexpected/m-p/9328145

- How can I get the center point of the part of wall which inside the room?
  https://forums.autodesk.com/t5/revit-api-forum/how-can-i-get-the-center-point-of-the-part-of-wall-which-inside/m-p/9331417#M44758
  beautiful solution by fair59

- Advice From The CIA: How To Sabotage Your Workplace
  https://corporate-rebels.com/cia-field-manual
  if you want to work effectively in a team, here are some very valid hints on what to avoid.

twitter:

 in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Adjusting the Wall Location Curve


#### <a name="2"></a>How to Create 24x24 Stacked Ribbon Items



**Question:** 

**Solution:** 
 
<pre class="code">
</pre>

Many thanks to ... for implementing and sharing this nice clean solution, and congratulations on such prowess "only a couple months into his C# developer life"!

#### <a name="3"></a>

#### <a name="5"></a>Adjusting versus Recreating Wall Location Curve

Harald Schmidt pointed out an interesting aspect and important enhancement to the old discussion of how
to [edit wall length](https://thebuildingcoder.typepad.com/blog/2010/08/edit-wall-length.html) in
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [adjusting Wall.LocationCurve.Curve results in unexpected behaviour](https://forums.autodesk.com/t5/revit-api-forum/adjusting-wall-locationcurve-curve-results-in-unexpected/m-p/9328145),
and once again Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen
came to the rescue with the final solution:

**Question:** My add-in adjusts walls lines slightly (moves and rotates them a bit, and also slightly adjusts their length).

Using `wall.Location.Move` and `wall.Location.Rotate` enables adjusting the location and rotation of the walls, but not their length.

So, we decided to follow the approach suggested 10 years ago by The Building Coder to [
to [edit wall length](https://thebuildingcoder.typepad.com/blog/2010/08/edit-wall-length.html) by
creating a completely new wall location line from scratch like this:

<pre class="code">
  // get the current wall location
  LocationCurve wallLocation = myWall.Location 
    as LocationCurve;
 
  // get the points
  XYZ pt1 = wallLocation.Curve.get_EndPoint( 0 );
  XYZ pt2 = wallLocation.Curve.get_EndPoint( 1 );
 
  // change one point, e.g. move 1000 mm on X axis
 
  pt2 = pt2.Add( new XYZ( 0.01 ), 0, 0 ) );
 
  // create a new LineBound
  Line newWallLine = app.Create.NewLineBound( 
    pt1, pt2 );
 
  // update the wall curve
  wallLocation.Curve = newWallLine;
</pre>

This works fine for single walls, but fails in many cases where the wall has hosted elements like windows, and even in complex scenarios with multiple walls.

Note: the movement is always less than about some mm!

To show you what I mean, I wrote a short macro embedded in the RVT attached.

The macro `LocationLineReset` just moves the wall inside the RVT by approx. 3 mm using the method suggested by The Building Coder:

<center>
<img src="img/adjust_wall_locationcurve_curve_macros.png" alt="Adjust wall LocationCurve curve macros" title="Adjust wall LocationCurve curve macros" width="600"/> 1104
</center>

The result is the following:

<center>
<img src="img/adjust_wall_locationcurve_curve_error.png" alt="Adjust wall LocationCurve curve error" title="Adjust wall LocationCurve curve error" width="600"/> 840
</center>

This seem like an unexpected behavior.
The window is now located outside the wall, although the wall has moved only about 3mm.
Is this a bug?

If I use the second macro `ShiftWall`, which just calls `wall.LocationCurve.Move` to create an equivalent translation comparable to the former marco, the result is fine.

Is there any method to adjust the length of the wall except resetting the `wall.LocationCurve.Curve` by creating a new curve using `Line.CreateBound`? That would solve our issue as well.

**Answer:** Thank you for your interesting observation and careful analysis.

I looked at your macros and can reproduce what you say.

Besides the macro you list, `LocationLineReset`, there is another one that slightly moves the existing location line instead of creating a new one, `ShiftWall`.

That latter macro completes successfully:

<pre class="code">
  public void LocationLineReset()
  {
    doc = this.Application.ActiveUIDocument.Document;
    Wall wall = doc.GetElement( new ElementId( 305891 ) ) as Wall;
    TransactionStatus status;
    using ( Transaction trans = new Transaction( doc, "bla" ) )
    {
      trans.Start();
      LocationCurve locationCurve = wall.Location as LocationCurve;
      Line wallLine = locationCurve.Curve as Line;
      
      XYZ startPoint = wallLine.GetEndPoint(0);
      XYZ endPoint = wallLine.GetEndPoint(1);

      XYZ minimalMoveVector = new XYZ( 0.01 /* =~ 3mm*/, 0.01, 0.0);
      startPoint += minimalMoveVector;
      endPoint += minimalMoveVector;
      
      locationCurve.Curve = Line.CreateBound( startPoint, endPoint );
      status = trans.Commit();
    }
    
    if( status != TransactionStatus.Committed ) 
      MessageBox.Show( "Commit failed" );
  }
  
  public void ShiftWall()
  {
    doc = this.Application.ActiveUIDocument.Document;
    Wall wall = doc.GetElement( new ElementId( 305891 ) ) as Wall;
    TransactionStatus status;
    bool bSuccess = false;
    using ( Transaction trans = new Transaction( doc, "bla" ) )
    {
      trans.Start();

      XYZ minimalMoveVector = new XYZ( 0.01 /* =~ 3mm*/, 0.01, 0.0);				
      bSuccess = wall.Location.Move( minimalMoveVector );
      
      status = trans.Commit();
    }
    
    if( status == TransactionStatus.Committed && bSuccess) 
      MessageBox.Show( "Shift succeeded" );
  }
</pre>

I would assume that during the process of resetting the wall location line from scratch, the window position gets completely lost.

When you simply perform a small adjustment to the existing location line, the window position is retained and adjusted accordingly.

Therefore, I would suggest using the latter approach whenever possible.

You could even make use of the latter approach in several steps, adjust first one and then the other location line endpoint.

I hope this enables you to handle all required situations.


SetEndPoint method


<pre class="code">
  public void ShortenWall()
  {
    Document doc = this.Application.ActiveUIDocument.Document;
    Wall wall = doc.GetElement( new ElementId( 305891 ) ) as Wall;
    TransactionStatus status;
    using ( Transaction trans = new Transaction( doc ) )
    {
      trans.Start( "Shorten Wall");
      
      LocationCurve lc = wall.Location as LocationCurve;
      Line ll = lc.Curve as Line;
      
      double pstart = ll.GetEndParameter(0);
      double pend = ll.GetEndParameter(1);
      double pdelta = 0.05 * (pend - pstart);
      
      lc.Curve.MakeBound(pstart + pdelta, pend - pdelta); // no observable change to wall
      
      (wall.Location as LocationCurve).Curve.MakeUnbound();
      
      (wall.Location as LocationCurve).Curve.MakeBound( // no observable change to wall
        pstart + pdelta, pend - pdelta);
  
      status = trans.Commit();
    }
    
    if( status == TransactionStatus.Committed) 
      MessageBox.Show( "Shorten Wall succeeded" );
  }
</pre>



jeremytammik in reply to: Participantharald.schmidt
‎2020-02-25 10:53 AM 
Re: Adjusting Wall.LocationCurve.Curve results in unexpected behaviour 
Rereading your question in more detail, I now address the question you raise at the end:

 

> Is there any method to adjust the length of the wall except resetting the `wall.LocationCurve.Curve` by creating a new curve using `Line.CreateBound`? That would solve our issue as well.

 

Yes, absolutely. You can modify just one of the line endpoints without changing the other one, and you can also change both endpoints at the same time by moving them by different translation vectors...

 

... or so I thought, until this very moment. 

 

Now I looked a the Revit API documentation Curve and Line member methods, expecting to point out the SetEndPoint methods to you, only to discover they do not exist:

 

https://www.revitapidocs.com/2020/92a388f3-4949-465c-b938-2906ff6bdf5b.htm

  

Oh dear.

  

In that case, you really do have to create a new curve from scratch, and the problem you describe arises.

 

The only other way I could think of to try to modify an existing curve's length is to change its start and end parameters.

 

In theory, this can be achieved by calling MakeBound. However, all my attempts to use that method to modify the wall length had no result. Here is the final attempt:

 

  public void ShortenWall()
  {
    Document doc = this.Application.ActiveUIDocument.Document;
    Wall wall = doc.GetElement( new ElementId( 305891 ) ) as Wall;
    TransactionStatus status;
    using ( Transaction trans = new Transaction( doc ) )
    {
      trans.Start( "Shorten Wall");
      
      LocationCurve lc = wall.Location as LocationCurve;
      Line ll = lc.Curve as Line;
      
      double pstart = ll.GetEndParameter(0);
      double pend = ll.GetEndParameter(1);
      double pdelta = 0.05 * (pend - pstart);
      
      lc.Curve.MakeBound(pstart + pdelta, pend - pdelta); // no observable change to wall
      
      (wall.Location as LocationCurve).Curve.MakeUnbound();
      
      (wall.Location as LocationCurve).Curve.MakeBound( // no observable change to wall
        pstart + pdelta, pend - pdelta);
  
      status = trans.Commit();
    }
    
    if( status == TransactionStatus.Committed) 
      MessageBox.Show( "Shorten Wall succeeded" );
  }
 

Sorry for the bad news.

 

I can raise an issue with the development team for this for you and see what they have to say about it.

 

Best regards,

 

Jeremy

 



Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 4 OF 7
FAIR59
 Advisor FAIR59 in reply to: Participantharald.schmidt
‎2020-02-25 01:33 PM 
 Re: Adjusting Wall.LocationCurve.Curve results in unexpected behaviour
You can adjust the curve of a wall with hosted elements, if the "curve definition" stays the same, i.e. just changing the start- and end parameters.

	LocationCurve lc = wall.Location as LocationCurve;
	Curve ll = lc.Curve;
      
	double pstart = ll.GetEndParameter(0);
	double pend = ll.GetEndParameter(1);
	double pdelta = 0.001 * (pend - pstart);
      
	ll.MakeUnbound();
	ll.MakeBound(pstart + pdelta, pend - pdelta);
      
	lc.Curve = ll;
 

Add tags
Report
MESSAGE 5 OF 7
jeremytammik
 Employee jeremytammik in reply to: AdvisorFAIR59
‎2020-02-25 01:47 PM 
Re: Adjusting Wall.LocationCurve.Curve results in unexpected behaviour
Great! Brilliant! So my code was just lacking the assignment! Thank you!

 



Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 6 OF 7
harald.schmidt
 Participant harald.schmidt in reply to: AdvisorFAIR59
‎2020-02-25 02:19 PM 
Re: Adjusting Wall.LocationCurve.Curve results in unexpected behaviour
Thank you and Jeremy for solving the issue! 

 

Just a minor question, can we expect that the wall line parameter scales the position of the start/end point uniformly to feet? E.g. if we want to adjust the length of the wall by 0.1 feet, can we always do the following:

        double pstart = ll.GetEndParameter(0);
	double pend = ll.GetEndParameter(1);
      
	ll.MakeUnbound();
	ll.MakeBound( pstart, pend + 0.1 /* moves always the end point by 0.1 feet */);       
	lc.Curve = ll;
In my example it did, but I am not sure if this is an invariant we can rely on?

Thanks again

**Answer:**

See the remarks in the [online documentation of the GetEndParameter method](https://www.revitapidocs.com/2016/0f4b2c25-35f8-4e3c-c71a-0d41fb6935ce.htm):

> Returns the raw parameter value at the start or end of this curve.

> The start and end value of the parameter can be any value (as it is determined by the system based on the inputs).
For curves with regular curvature like lines and arcs, the raw parameter can be used to measure along the curve in Revit's default units (feet).
Raw parameters are also the only way to evaluate points along unbound curves.
