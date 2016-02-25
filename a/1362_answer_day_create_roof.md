<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- change selected instance type
  email [Revit API]
  taking a second look at this question, i note that most of what the developer needs is already implemented in the external command Lab3_4_ChangeSelectedInstanceType in the module https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs3.cs

- IterateOverCollector
  email [Regarding [CaseNo:11090027.] ElementIdを取得する方法] Ryuji Ogasawara 

- creating an electrical family
  email [RFA API (especially in Electrical content context)]

#dotnet #csharp #geometry
#fsharp #dynamobim #python
#grevit
#responsivedesign #typepad
#ah8 #augi #au2015 #dotnet #dynamobim
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon #revitapi #3dwebcoder
#javascript
#au2015 #rtceur
#RestSharp #restapi
#au2015
#mongoosejs #mongodb #nodejs
#au2015 #rtceur

Revit API, Jeremy Tammik, akn_include

Revit Answer Day and Creating a Roof #revitapi #bim #aec #3dwebcoder #adsk #adskdevnetwrk

Today I'll highlight two items, plus an addendum for Kinjal and Carolina
&ndash; Revit Answer Day
&ndash; Creating a Roof
&ndash; Setting FootPrintRoof Slope...

-->

### Revit Answer Day and Creating a Roof

Today I'll just highlight two items that were also already pointed out
by [Jaime Rosales](http://adndevblog.typepad.com/aec/jaime-rosales.html) on
the [AEC DevBlog](http://adndevblog.typepad.com/aec), plus an addendum for Kinjal and Carolina:

- [Revit Answer Day](#2)
- [Creating a Roof](#3)
- [Setting FootPrintRoof Slope](#4)



#### <a name="2"></a>Revit Answer Day

Do you have any Revit or Revit LT questions that you’ve always wanted to get answered?

If so, please join the next instalment
of [Ask Autodesk Anything](http://www.autode.sk/answerdays) events,
the [Revit Answer Day](http://forums.autodesk.com/t5/revit-answer-day/revit-answer-day-october-7th-2015/td-p/5787599) on
October 7, 2015.

This event is dedicated to answering your questions about Revit and Revit LT.
We’ll have some DevTech engineers attending the event to answer your API questions.
The event runs from 6 am to 6 pm Pacific Time and will take place in Autodesk Community.
You can spend a minute, an hour, or the whole day in the Autodesk Community to interact directly with the folks who can answer every question about Revit you can think of.



#### <a name="3"></a>Creating a Roof

This question was asked last week both on
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/help-creating-roof/m-p/5828806)
and [Stack Overflow](http://stackoverflow.com/questions/32718999/creating-a-roof-function/32732012#32732012):

**Question:**
I'm having trouble programmatically creating a roof.
I know how to create a stairs, for example: I start a `StairsEditScope`, use `CreateSketchedLanding` with the right parameters to create my stairs and commit the `StairsEditScope`.
I can't find a clue on how to create a roof from scratch, though.
Any leads?

**Answer:**
Please always search the Revit API help file RevitAPI.chm and
the [Revit online help](http://help.autodesk.com/view/RVT/2016/ENU) before
asking questions like this.

Here is the sample code provided by the latter, in the section
on [Roofs](http://help.autodesk.com/view/RVT/2016/ENU/?guid=GUID-33E6A6BD-96AA-4FAF-B660-1DD0C06CCD29):

<pre class="code">
&nbsp; <span class="green">// Before invoking this sample, select some walls </span>
&nbsp; <span class="green">// to add a roof over. Make sure there is a level </span>
&nbsp; <span class="green">// named &quot;Roof&quot; in the document.</span>
&nbsp;
&nbsp; <span class="teal">Level</span> level
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">Level</span> ) )
&nbsp; &nbsp; &nbsp; .Where&lt;<span class="teal">Element</span>&gt;( e =&gt;
&nbsp; &nbsp; &nbsp; &nbsp; !<span class="blue">string</span>.IsNullOrEmpty( e.Name )
&nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; e.Name.Equals( <span class="maroon">&quot;Roof&quot;</span> ) )
&nbsp; &nbsp; &nbsp; .FirstOrDefault&lt;<span class="teal">Element</span>&gt;() <span class="blue">as</span> <span class="teal">Level</span>;
&nbsp;
&nbsp; <span class="teal">RoofType</span> roofType
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">RoofType</span> ) )
&nbsp; &nbsp; &nbsp; .FirstOrDefault&lt;<span class="teal">Element</span>&gt;() <span class="blue">as</span> <span class="teal">RoofType</span>;
&nbsp;
&nbsp; <span class="green">// Get the handle of the application</span>
&nbsp; <span class="teal">Application</span> application = doc.Application;
&nbsp;
&nbsp; <span class="green">// Define the footprint for the roof based on user selection</span>
&nbsp; <span class="teal">CurveArray</span> footprint = application.Create
&nbsp; &nbsp; .NewCurveArray();
&nbsp;
&nbsp; <span class="teal">UIDocument</span> uidoc = <span class="blue">new</span> <span class="teal">UIDocument</span>( doc );
&nbsp;
&nbsp; <span class="teal">ICollection</span>&lt;<span class="teal">ElementId</span>&gt; selectedIds
&nbsp; &nbsp; = uidoc.Selection.GetElementIds();
&nbsp;
&nbsp; <span class="blue">if</span>( selectedIds.Count != 0 )
&nbsp; {
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">ElementId</span> id <span class="blue">in</span> selectedIds )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Element</span> element = doc.GetElement( id );
&nbsp; &nbsp; &nbsp; <span class="teal">Wall</span> wall = element <span class="blue">as</span> <span class="teal">Wall</span>;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( wall != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">LocationCurve</span> wallCurve = wall.Location <span class="blue">as</span> <span class="teal">LocationCurve</span>;
&nbsp; &nbsp; &nbsp; &nbsp; footprint.Append( wallCurve.Curve );
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">continue</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">ModelCurve</span> modelCurve = element <span class="blue">as</span> <span class="teal">ModelCurve</span>;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( modelCurve != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; footprint.Append( modelCurve.GeometryCurve );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">else</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">Exception</span>(
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Please select a curve loop, wall loop or &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;combination of walls and curves to &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;create a footprint roof.&quot;</span> );
&nbsp; }
&nbsp;
&nbsp; <span class="teal">ModelCurveArray</span> footPrintToModelCurveMapping
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">ModelCurveArray</span>();
&nbsp;
&nbsp; <span class="teal">FootPrintRoof</span> footprintRoof
&nbsp; &nbsp; = doc.Create.NewFootPrintRoof(
&nbsp; &nbsp; &nbsp; footprint, level, roofType,
&nbsp; &nbsp; &nbsp; <span class="blue">out</span> footPrintToModelCurveMapping );
&nbsp;
&nbsp; <span class="teal">ModelCurveArrayIterator</span> iterator
&nbsp; &nbsp; = footPrintToModelCurveMapping.ForwardIterator();
&nbsp;
&nbsp; iterator.Reset();
&nbsp; <span class="blue">while</span>( iterator.MoveNext() )
&nbsp; {
&nbsp; &nbsp; <span class="teal">ModelCurve</span> modelCurve = iterator.Current <span class="blue">as</span> <span class="teal">ModelCurve</span>;
&nbsp; &nbsp; footprintRoof.set_DefinesSlope( modelCurve, <span class="blue">true</span> );
&nbsp; &nbsp; footprintRoof.set_SlopeAngle( modelCurve, 0.5 );
&nbsp; }
</pre>

To test it, please make sure you have some walls to hold up the roof and one of your levels is named `Roof`.

I just created a simple four-wall rectangle, selected them and ran the command.

This code creates a footprint roof.

Revit also provides other kinds.

It is important to understand the Revit product and the various roof types from an end user point of view before thinking about driving them programmatically.

The footprint roof created above is defined by a horizontal outline and is created using the `Document.NewFootPrintRoof` method. Such a roof can be flat, or you can specify a slope for each edge of the outline profile.

The Building Coder [Xtra labs](https://github.com/jeremytammik/AdnRevitApiLabsXtra) provides
another working sample in the external
command [Lab2_0_CreateLittleHouse in Labs2.cs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs2.cs).

[The Building Coder](http://thebuildingcoder.typepad.com/) also
provides these other roof-related posts:

- [RoomsRoofs SDK Sample](http://thebuildingcoder.typepad.com/blog/2008/09/roomsroofs-sdk.html)
- [Roof Eave Cut](http://thebuildingcoder.typepad.com/blog/2009/08/roof-eave-cut-in-revit-and-aca.html)
- [Creating an Extrusion Roof](http://thebuildingcoder.typepad.com/blog/2014/09/events-again-and-creating-an-extrusion-roof.html)

<center>
<img src="img/little_house_roof.png" alt="Little house roof" width="220"/>
</center>


#### <a name="4"></a>Addendum &ndash; Setting FootPrintRoof Slope

For Kinjal and Carolina, to answer the Revit discussion forum thread
on [Creating a surface through the Revit API](http://forums.autodesk.com/t5/revit-api/create-a-surface-through-revit-api/m-p/5872832):

Here are all the discussions I found on this, before hitting the [last and hopefully ultimate answer above](#3) that I already forgot I had written:

- [Slope is slope, not radians](http://thebuildingcoder.typepad.com/blog/2010/08/slope-is-slope-not-radians.html)
- [Modifying floor slope programmatically &ndash; or not](http://thebuildingcoder.typepad.com/blog/2014/03/creating-a-sloped-floor.html#3)
- [New opening](http://forums.autodesk.com/t5/Revit-API/New-Opening/m-p/5104388)
- [The discussion with Dodd on creating a sloped floor](http://thebuildingcoder.typepad.com/blog/2014/03/creating-a-sloped-floor.html#comment-2231365409)
- Internal ADN cases 09714390 *New Opening*, 08886290 *create footprint roof* and 07928659 *Revit 2013如何设置屋顶的坡度*

I hope this helps once and for all and I do not forget about this answer again.


