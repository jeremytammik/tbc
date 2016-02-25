<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

#revitapi #3dwebcoder #bim #aec #adskdevnetwrk #dotnet #csharp #geometry
#adsk #fsharp #dynamobim
#restapi #python
#grevit
#responsivedesign #typepad
#ah8 #augi #au2015 #dotnet #dynamobim
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon #revitapi #adsk #3dwebcoder #aec #bim
#Mongoose #javascript
#au2015 #rtceur

Revit API, Jeremy Tammik, akn_include

FireRating in the Cloud and RevitLookup Update #RestSharp #restapi #revitapi #3dwebcoder #mongodb #nodejs #bim

I am currently working intensely at the Autodesk Cloud Accelerator in Prague.
I spent a lot of my time so far enhancing the FireRating in the Cloud sample.
&ndash; FireRating in the Cloud enhancements
&ndash; RevitLookup update to handle null analytical model
&ndash; Retrieving all model elements...

-->

### FireRating in the Cloud and RevitLookup Update

I am currently working intensely at the
[Autodesk Cloud Accelerator in Prague](http://autodeskcloudaccelerator.com/prague).

It is exciting and we are making good progress.

I spent a lot of my time so far enhancing the
[FireRating in the Cloud](https://github.com/jeremytammik/FireRatingCloud) sample
and documenting these improvements.

Here are my topics for today:

- [FireRating in the Cloud enhancements](#2)
- [RevitLookup update to handle null analytical model](#3)
- [Retrieving all model elements](#4)


I glad to report that I found
[Mlsná Kavka](http://www.mlsnakavka.cz) (*Picky Jackdaw* in English),
a nice and friendly non-smoking vegetarian restaurant, just around the corner from the Autodesk office:

<center>
<img src="file:////j/photo/jeremy/2015/2015-09-15_prag/958.jpg" alt="View from Mlsná Kavka" width="400"/>
</center>

You can also check out Kean Walmsley's report
[halfway through the Cloud Accelerator Extension](http://through-the-interface.typepad.com/through_the_interface/2015/09/at-the-cloud-accelerator-in-prague.html).



#### <a name="2"></a>FireRating in the Cloud Enhancements

My real goal is to switch back and continue work on the new
[CompHound component tracker](http://the3dwebcoder.typepad.com/blog/2015/09/comphound-restsharp-mongoose-put-and-post.html#2) as
fast as possible, in preparation for the upcoming conference presentations at
[RTC Europe](http://www.rtcevents.com/rtc2015eu) in Budapest end of October and
[Autodesk University](http://au.autodesk.com) in Las Vegas in December.

However, since the
[FireRating in the Cloud](https://github.com/jeremytammik/FireRatingCloud) sample
is more generic and fundamental, I want to clean it up to utter perfection first, before expanding into new areas.

[FireRating in the Cloud](https://github.com/jeremytammik/FireRatingCloud) covers the connection of Revit database and element data to a cloud-hosted external database.

The [CompHound component tracker](https://github.com/CompHound) will add a user interface, reports, and a 3D model viewer and navigator to that.

Here is a list of the recent firerating enhancements:

- [Using RestSharp for Rest API GET](http://the3dwebcoder.typepad.com/blog/2015/09/using-restsharp-for-rest-api-get.html)
- [Mongodb Upsert](http://the3dwebcoder.typepad.com/blog/2015/09/mongodb-upsert.html)
- C# DoorData and Node.js DoorService Classes
    - [DoorData Container Class](http://the3dwebcoder.typepad.com/blog/2015/09/c-doordata-and-nodejs-doorservice-classes.html#3)
    - [REST GET returns a list of deserialised DoorData instances](http://the3dwebcoder.typepad.com/blog/2015/09/c-doordata-and-nodejs-doorservice-classes.html#4)
    - [Passing a DoorData instance to the Put method](http://the3dwebcoder.typepad.com/blog/2015/09/c-doordata-and-nodejs-doorservice-classes.html#5)
    - [Implementing a REST API router DoorService class](http://the3dwebcoder.typepad.com/blog/2015/09/c-doordata-and-nodejs-doorservice-classes.html#6)


#### <a name="3"></a>RevitLookup Update to Handle Null Analytical Model

In between other things here a user reported a problem with RevitLookup, saying that, "I loaded and ran RevitLookup 2016 'Snoop Current Selection...' today and encountered a rather spectacular error crashing Revit without an error message when a Generic Floor was preselected."

The error can be solved by commenting out the line accessing the analytical model in the Stream method handling a floor element:

<pre class="code">
  data.Add( new Snoop.Data.String( "Structural usage",
    floor.GetAnalyticalModel().GetAnalyzeAs().ToString() ) );
</pre>

In Revit Architecture or MEP, the GetAnalyticalModel will return null, causing the call to ToString to fail.

This was actually fixed &ndash; more elegantly, I'm glad to say &ndash; in
[RevitLookup release 2016.0.0.10](https://github.com/jeremytammik/RevitLookup/releases/tag/2016.0.0.10),
so please do make sure to download and install the latest version if you encounter this situation.

I actually already [mentioned this](http://thebuildingcoder.typepad.com/blog/2015/09/family-category-element-ids-transaction-undo-and-updates.html#5) two weeks ago...


#### <a name="4"></a>Retrieving All Model Elements

Once again, let's revisit the topic of
[selecting model elements](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.9), raised this time by Dale in the Revit API discussion thread on
[traversing all model elements](http://forums.autodesk.com/t5/revit-api/traverse-all-model-elements-in-a-project-top-down-approach/m-p/5815247):

**Question:**
To be clear, "all model elements" could be defined as all objects that exist in the real world: chairs, doors, walls, etc. &ndash; component and system families. Graphic elements such as levels, sheets, views, dimensions, annotation, profiles, etc. would not be included. I have been through all The Building Coder samples (and others) many times, and whilst there are lots of examples to filter for a specific category or feature, I have been unable to come up with a solution that provides all model elements as defined above. Your
[example from 2009](http://thebuildingcoder.typepad.com/blog/2009/05/selecting-model-elements.html)
([2](http://thebuildingcoder.typepad.com/blog/2009/11/select-model-elements-2.html)) may be the best,
but given that it is several years old, I have been looking for more contemporary code. Apologies if I am overlooking something obvious; I thought this would be a five-minute job but I have been unable to come up with a simple, solid solution.

**Answer:**
Does this fit the bill?

<pre class="code">
&nbsp; <span class="teal">IEnumerable</span>&lt;<span class="teal">Element</span>&gt; GetAllModelElements(
&nbsp; &nbsp; <span class="teal">Document</span> doc )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Options</span> opt = <span class="blue">new</span> <span class="teal">Options</span>();
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .WhereElementIsNotElementType()
&nbsp; &nbsp; &nbsp; .WhereElementIsViewIndependent()
&nbsp; &nbsp; &nbsp; .Where&lt;<span class="teal">Element</span>&gt;( e
&nbsp; &nbsp; &nbsp; &nbsp; =&gt; <span class="blue">null</span> != e.Category
&nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; <span class="blue">null</span> != e.get_Geometry( opt ) );
&nbsp; }
</pre>

I added and published this in
[The Building Coder samples release 2016.0.120.14](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2016.0.120.14).

Please try it out, and let's ensure that we really get something up and running that suits your needs.

**Response:**
If I ever get to AU again, I owe you a beer! Two maybe. Works perfectly. Thanks again, I hope this is helpful for others as I really struggled to resolve it. Dale
