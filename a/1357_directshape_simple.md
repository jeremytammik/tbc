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

Revit API, Jeremy Tammik, akn_include

Simple DirectShape on Face, #RestSharp, #restapi PUT and POST #revitapi #3dwebcoder #javascript #mongodb #nodejs #Mongoose

Creating a DirectShape from a face used the picked face reference and its stable string representation to navigate the element geometry and determine a transformation.
Let's take a look at a much simpler approach
&ndash; Simpler DirectShape on picked face using total transform
&ndash; The CompHound component tracker
&ndash; FireRating in the cloud enhancement...

-->

### Simple DirectShape on Face, RestSharp, PUT and POST

Last Tuesday, I looked at creating a
[DirectShape from a face and sketch plane reuse](http://thebuildingcoder.typepad.com/blog/2015/09/directshape-from-face-and-sketch-plane-reuse.html).

I implemented a convoluted method using the picked face reference and its stable string representation returned by the `ConvertToStableRepresentation` method to navigate the element geometry and determine an appropriate transformation for it into the world coordinate system.

Today, let's take a look at a much simpler approach, and also point to my cloud related work including a significant enhancement to the FireRating sample in the cloud:

- [Civil &ndash; Made in France](#1)
- [Simpler DirectShape on picked face using total transform](#2)
- [The CompHound component tracker](#3)
- [FireRating in the cloud enhancement](#4)



#### <a name="1"></a>Civil &ndash; Made in France

Are you civil?

I sincerely hope so!

No, what I really meant, are you interested in civil engineering?

If so, and if you read French, or are happy to use a translator, you might also be interested in the newest addition to the Autodesk blogosphere:

<center>
<a style="font-size:200%" href="http://civilfrance.typepad.com">Civil &ndash; Made in France</a>
</center>

Restez au fait de l'actualité des solutions AEC d'Autodesk pour l'Aménagement du Territoire
&ndash;
*Keep up with the news of Autodesk AEC solutions for regional development*.

Back to the Revit API:



#### <a name="2"></a>Simpler DirectShape on Picked Face Using Total Transform

That solution that Frode and I implemented to create a DirectShape on a picked face lives in the
[DirectShapeFromFace GitHub repository](https://github.com/jeremytammik/DirectShapeFromFace).

Alexander Ignatovich, Александр Игнатович, of
[Investicionnaya Venchurnaya Companiya](http://www.iv-com.ru) took
a critical look at that sample and implemented a significant simplification.

He also added a front-end command to enable you to choose which of the three implementations to execute:

- Original code producing a DirectShape in the wrong location.
- Jeremy's geometry iteration, which works.
- Alexander's simpler solution using the family instance GetTotalTransform method, which also works.

Here is a summary of our discussion, which also briefly explains how to create a pull request for a GitHub project:

**Alexander:**
You wrote a post about creating direct shape elements from face a few days ago.

The algorithm to find appropriate transformation is difficult. I was sure, that there is much more simple way to find it, and after several experiments I've found the right way ;-)

I took the initial code and made some slight changes.

First of all, I get total family instance transform:

<pre class="code">
&nbsp; <span class="blue">var</span> familyInstance = el <span class="blue">as</span> <span class="teal">FamilyInstance</span>;
&nbsp;
&nbsp; <span class="blue">var</span> transform = familyInstance != <span class="blue">null</span>
&nbsp; &nbsp; ? familyInstance.GetTotalTransform()
&nbsp; &nbsp; : <span class="teal">Transform</span>.Identity;
</pre>

I use that when getting the mesh triangle points:

<pre class="code">
&nbsp; p1 = transform.OfPoint( p1 );
&nbsp; p2 = transform.OfPoint( p2 );
&nbsp; p3 = transform.OfPoint( p3 );
</pre>

**Jeremy:**
Does  your method work correctly for a nested family instance?

I am not sure that mine will either, but at least it tries to!

Imagine the following nested family:

- A family definition B containing a cube C, transformed and rotated.
- A family definition A containing B, transformed and rotated a lot more.
- A project containing an instance of A.

Now if you pick a face of the A instance in the projects, the total transform of the A instance will probably only take the translation from A into account, but not the additional preceding transformation of B within A.

In my algorithm,  try to determine both of those transformations and multiply them with each other.

I have not tested it, and my multiplication may well be in the wrong order.

Would you like to try that out?

If you make any changes to the code, would you like to do one of the two following?

- Ask me to make you a collaborator on my original GitHub project. Then you can clone it, commit changes, and push them back.
- Fork my project to your own repo on GitHub, clone that, modify, commit changes, and issue a pull request for me to merge them back into the original.

Both of these approaches will save me from having to compare the code and reintegrate the changes back manually.

Thank you!

**Alexander:**
I tested nested families and submitted
[pull request #1](https://github.com/jeremytammik/DirectShapeFromFace/pull/1) to your repository:

- Added initial shape builder and ability to select initial or Jeremy's shape builder.
- Added simple direct shape creating algorithm, based on initial with applying familyInstance.GetTotalTransform.
- Added Direct shapes tests.rvt for the case with nested families when:
    - A family definition B containing a cube C, transformed and rotated.
    - A family definition A containing B, transformed and rotated a lot more.
    - A project containing an instance of A.

I also added a sample rvt with nested families, both when all are shared and not:

<center>
<img src="img/directshape_test.png" alt="DirectShape test.rvt" width="348"/>
</center>

**Jeremy:**
I integrated your changes and created
[DirectShapeFromFace release 2016.0.0.13](https://github.com/jeremytammik/DirectShapeFromFace/releases/tag/2016.0.0.13).

**Alexander:**
Cool
[commit](https://github.com/jeremytammik/DirectShapeFromFace/commit/6d65ba5d102cf0246963795668e73133a74e18aa)!

I only tested the idea; thank you for cleaning up the code.

I retested, and everything is still working.

Here is the current version of the working simplified algorithm:

<pre class="code">
&nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp; <span class="teal">Selection</span> choices = uidoc.Selection;
&nbsp;
&nbsp; <span class="blue">try</span>
&nbsp; {
&nbsp; &nbsp; <span class="teal">Reference</span> reference = choices.PickObject(
&nbsp; &nbsp; &nbsp; <span class="teal">ObjectType</span>.Face );
&nbsp;
&nbsp; &nbsp; <span class="teal">Element</span> el = doc.GetElement(
&nbsp; &nbsp; &nbsp; reference.ElementId );
&nbsp;
&nbsp; &nbsp; <span class="teal">Face</span> face = el.GetGeometryObjectFromReference(
&nbsp; &nbsp; &nbsp; reference ) <span class="blue">as</span> <span class="teal">Face</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">Mesh</span> mesh = face.Triangulate();
&nbsp;
&nbsp; &nbsp; <span class="blue">var</span> familyInstance = el <span class="blue">as</span> <span class="teal">FamilyInstance</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != familyInstance )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">var</span> t = familyInstance
&nbsp; &nbsp; &nbsp; &nbsp; .GetTotalTransform();
&nbsp;
&nbsp; &nbsp; &nbsp; mesh = mesh.get_Transformed( t );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; trans.Start( <span class="maroon">&quot;Create DirectShape from Face&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">TessellatedShapeBuilder</span> builder
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">TessellatedShapeBuilder</span>();
&nbsp;
&nbsp; &nbsp; &nbsp; builder.OpenConnectedFaceSet( <span class="blue">false</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; args = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt;( 3 );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span>[] triangleCorners = <span class="blue">new</span> <span class="teal">XYZ</span>[3];
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">for</span>( <span class="blue">int</span> i = 0; i &lt; mesh.NumTriangles; ++i )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">MeshTriangle</span> triangle = mesh.get_Triangle( i );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; triangleCorners[0] = triangle.get_Vertex( 0 );
&nbsp; &nbsp; &nbsp; &nbsp; triangleCorners[1] = triangle.get_Vertex( 1 );
&nbsp; &nbsp; &nbsp; &nbsp; triangleCorners[2] = triangle.get_Vertex( 2 );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">TessellatedFace</span> tesseFace
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">TessellatedFace</span>( triangleCorners,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ElementId</span>.InvalidElementId );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( builder.DoesFaceHaveEnoughLoopsAndVertices(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; tesseFace ) )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; builder.AddFace( tesseFace );
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; builder.CloseConnectedFaceSet();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">TessellatedShapeBuilderResult</span> result
&nbsp; &nbsp; &nbsp; &nbsp; = builder.Build(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">TessellatedShapeBuilderTarget</span>.AnyGeometry,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">TessellatedShapeBuilderFallback</span>.Mesh,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ElementId</span>.InvalidElementId );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">ElementId</span> categoryId = <span class="blue">new</span> <span class="teal">ElementId</span>(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInCategory</span>.OST_GenericModel );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">DirectShape</span> ds = <span class="teal">DirectShape</span>.CreateElement(
&nbsp; &nbsp; &nbsp; &nbsp; doc, categoryId,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Assembly</span>.GetExecutingAssembly().GetType()
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .GUID.ToString(), <span class="teal">Guid</span>.NewGuid().ToString() );
&nbsp;
&nbsp; &nbsp; &nbsp; ds.SetShape( result.GetGeometricalObjects() );
&nbsp;
&nbsp; &nbsp; &nbsp; ds.Name = <span class="maroon">&quot;MyShape&quot;</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; trans.Commit();
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">catch</span>( <span class="teal">Exception</span> ex )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Debug</span>.Print( ex.Message );
&nbsp; }
</pre>

The most up-to-date version, complete Visual Studio solution and add-in manifest is provided by the
[DirectShapeFromFace GitHub repository](https://github.com/jeremytammik/DirectShapeFromFace).

The version discussed here is
[release 2016.0.0.13](https://github.com/jeremytammik/DirectShapeFromFace/releases/tag/2016.0.0.13).

Many thanks to Alexander for raising this issue, providing the important simplification and testing!


#### <a name="3"></a>The CompHound component tracker

I am overdue to prepare for my upcoming conferences this autumn,
[RTC Europe](http://www.rtcevents.com/rtc2015eu) in Budapest end of October and
[Autodesk University](http://au.autodesk.com) in Las Vegas in December.

For both of them, I committed to implementing and demonstrating a *cloud-based universal component and asset usage analysis, visualisation and reporting*:

> We describe the nitty-gritty programming details to implement a cloud-based system to analyse, visualise and report on universal component and asset usage. The components could be Revit family instances used in BIM or any other kind of assets in any other kind of system. The focus is on the cloud-based database used to manage the component occurrences, either in global or project based coordinate systems. Searches can be made based on geographical location or keywords. Models are visualised using the Autodesk View and Data API, providing support for online viewing and model navigation.

Besides the web server and database aspects already explored for the [FireRating in the cloud](https://github.com/jeremytammik/FireRatingCloud) sample that I recently discussed here in depth, the new tool will also sport a user interface.

Therefore, I started looking at the [React](https://facebook.github.io/react) JavaScript library for building user interfaces, implemented and used by none other than Facebook.

I implemented my first JsFiddle, a minimal pre-test version of a
[FireRating Door List React Component](http://jsfiddle.net/jeremytammik/tm6ebc9m/8):

<center>
<iframe width="90%" height="300" src="http://jsfiddle.net/jeremytammik/tm6ebc9m/8/embedded/" allowfullscreen="allowfullscreen" frameborder="0"></iframe>
</center>

I am also taking a new look at the
[Autodesk View and Data API](https://developer.autodesk.com/api/view-and-data-api) side
of things, and bringing it all together as quickly and painlessly as possible...

This will be a nice project to take with me to the upcoming
[cloud accelerator in Prague](http://thebuildingcoder.typepad.com/blog/2015/06/accelerator-appstore-disqus-and-aec-devblog-articles.html#2) next week!

I christened this project
*[CompHound](https://github.com/CompHound)*,
for hounding out and tracking down those pesky components &nbsp; :-)

So far, it consists of two modules:

- The main JavaScript node.js web server driving a mongo database, now awaiting a user interface and 3D visualisation components.
- A C# Revit add-in populating the database with components to analyse.

To keep these two projects nicely packaged together, I created the
[CompHound GitHub organisation](https://github.com/CompHound) and added them as subprojects to that:

- [CompHoundWeb](https://github.com/CompHound/CompHoundWeb) &ndash;
Cloud-based universal component and asset usage analysis, report and visualisation, a REST API node and mongo web server (server-side JavaScript).
- [CompHoundRvt](https://github.com/CompHound/CompHoundRvt) &ndash;
Revit add-in to populate the CompHound server (desktop C# REST API client).

I grabbed the code from the existing
[fireratingdb](https://github.com/jeremytammik/firerating) web server and
[FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) Revit add-in
to quickly achieve the following functionality for CompHound as well:

- Set up the node.js web server
- Implement a REST API for it
- Drive the mongo database from it
- Host the mongo database in the cloud on [mongolab](https://mongolab.com)
- Host the web server in the cloud on [heroku](http://heroku.com)
- Retrieve and format the Revit model data
- Send it to the server via REST API

Note that stepping through all of these tasks for the new CompHound project only took an hour or so in all, from scratch.

It took me several weeks to discover how to achieve all this the first time around, for the FireRating project &nbsp; :-)

I was once again irritated by the GET, PUT and POST issue I encountered: I have to use POST to create a new record versus PUT to update an existing one.

I finally fixed that problem in the two new projects and published
[CompHoundWeb 0.0.1](https://github.com/CompHound/CompHoundWeb/releases/tag/0.0.1) and
[CompHoundRvt 2016.0.0.0](https://github.com/CompHound/CompHoundRvt/releases/tag/2016.0.0.0), respectively.

Then I returned to the original FireRating samples to fix it there as well:



#### <a name="4"></a>FireRating in the Cloud Enhancement

I implemented a significant enhancement to the
[FireRatingCloud]() Revit add-in and its cloud-based database counterpart
[fireratingdb]().

Together, they represent a multi-project replacement for the standard Revit SDK FireRating sample, which demonstrates three things:

- Programmatically create and bind a shared parameter
- Export parameter data to an external data container
- Import back modifications and update the BIM accordingly

Last time I looked at this project, there were still two issues left open:

- Messy use of `HttpWebRequest`
- Need to first make a REST API GET call to decide whether to use PUT or POST

I now resolved them, both for the initial CompHound implementation and for the original FireRating in the cloud project:

- [GET, PUT and POST stupidity creating versus updating a record](http://the3dwebcoder.typepad.com/blog/2015/09/comphound-restsharp-mongoose-put-and-post.html#3)
- [Enabling PUT to create as well as update](http://the3dwebcoder.typepad.com/blog/2015/09/comphound-restsharp-mongoose-put-and-post.html#4)
- [Using RestSharp instead of HttpWebRequest](http://the3dwebcoder.typepad.com/blog/2015/09/comphound-restsharp-mongoose-put-and-post.html#5)
- [No need to format data as JSON](http://the3dwebcoder.typepad.com/blog/2015/09/comphound-restsharp-mongoose-put-and-post.html#6)
