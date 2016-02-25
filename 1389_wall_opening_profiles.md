<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- http://forums.autodesk.com/t5/revit-api/get-wall-openings/m-p/5955467

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#rtceur
#xaml

Revit API, Jeremy Tammik, akn_include

Wall Opening Profiles and Happy Holidays #revitapi #bim #aec #3dwebcoder #adsk #geometry #3d #apis #relationship


Last week, we discussed how to retrieve wall openings. Let's take another fresh look at that, based on FindInserts instead of the ReferenceIntersector ray tracing approach, and also at
&ndash; A walk over Uetliberg
&ndash; Using FindInserts to retrieve wall openings
&ndash; CmdGetWallOpeningProfiles
&ndash; Relationships, independence and consumerism...

-->

### Wall Opening Profiles and Happy Holidays!

Last week, we discussed how to [retrieve wall openings](http://thebuildingcoder.typepad.com/blog/2015/12/retrieving-wall-openings-and-sorting-points.html).

Let's take another fresh look at that, and also at:

- [A walk over Uetliberg](#2)
- [Using FindInserts to retrieve wall openings](#3)
- [CmdGetWallOpeningProfiles](#4)
- [Relationships, independence and consumerism](#5)


#### <a name="2"></a>A Walk over Uetliberg

First, here are some snapshots from an unseasonably warm winter [walk over the Uetliberg](https://www.flickr.com/gp/jeremytammik/i819G8) at Zurich last weekend:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157662537072231" title="Uetliberg Afternoon Walk"><img src="https://farm1.staticflickr.com/696/23838892816_e6dea84f27_n.jpg" width="320" height="240" alt="Uetliberg Afternoon Walk"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>



#### <a name="3"></a>Using FindInserts to Retrieve Wall Openings

Last week, we solved the task of retrieving wall opening start and end points by shooting a ray through the wall parallel to the wall location line and asking the ReferenceIntersector for all the wall opening side faces that it hits.

That works fine.

However, in the initial query on [getting wall openings](http://forums.autodesk.com/t5/revit-api/get-wall-openings/m-p/5953513), Eirik implies that `FindInserts` cannot be used because 'it's not given that anything is inserted in the opening'.

That is a false assumption, as proven by the later answer by Scott Wilson, who very succinctly suggests:

> What about just using  `Wall.FindInserts(true, false, false, false)`

> Then check the category of each returned element for `BuiltInCategory.OST_Doors`, `BuiltInCategory.OST_Windows` and, for rectangular openings, check for element Type: `Opening`.

> To get the opening faces, loop through all perpendicular faces of the wall using `Wall.GetGeneratingElementIds` to match against your list of openings.

> `FindInserts` used with those parameters will find the windows whether or not anything is 'inserted' in the openings whatever that means.

> I use this method quite frequently.

> You could also just skip the FindInserts Step and just check each face of the wall for those created by a door / window instance or opening element.

> I whipped up a Revit 2015 command as a demo: [GetOpeningProfiles.zip](https://dl.dropboxusercontent.com/u/74965361/GetOpeningProfiles.zip).


#### <a name="4"></a>CmdGetWallOpeningProfiles

Seeing as I already implemented a command for the ray tracing approach, I went ahead and added this alternative of Scott's as well as another external
command [CmdWallOpeningProfiles](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdWallOpeningProfiles.cs) to
[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).

I tested both Scott's original Revit 2015 implementation and its new incarnation in Revit 2016.

Scott's implementation includes several noteworthy little details, e.g.:

- Drawing model lines to display the face edges and using temporary hide to display them.
- The proper clean way to convert a built-in category enumeration value to the document category element id:
  <pre class="code">
  &nbsp; <span class="teal">ElementId</span> catDoorsId = cats.get_Item(
  &nbsp; &nbsp; <span class="teal">BuiltInCategory</span>.OST_Doors ).Id;
  </pre>

The code to retrieve the wall geometry and extract all faces from all solids that belong to a specific opening  is implemented by the helper method `GetWallOpeningPlanarFaces`:

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Retrieve all planar faces belonging to the </span>
&nbsp; <span class="gray">///</span><span class="green"> specified opening in the given wall.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="teal">List</span>&lt;<span class="teal">PlanarFace</span>&gt; GetWallOpeningPlanarFaces(
&nbsp; &nbsp; <span class="teal">Wall</span> wall,
&nbsp; &nbsp; <span class="teal">ElementId</span> openingId )
&nbsp; {
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">PlanarFace</span>&gt; faceList = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">PlanarFace</span>&gt;();
&nbsp;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">Solid</span>&gt; solidList = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">Solid</span>&gt;();
&nbsp;
&nbsp; &nbsp; <span class="teal">Options</span> geomOptions = wall.Document.Application.Create.NewGeometryOptions();
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( geomOptions != <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">//geomOptions.ComputeReferences = true; // expensive, avoid if not needed</span>
&nbsp; &nbsp; &nbsp; <span class="green">//geomOptions.DetailLevel = ViewDetailLevel.Fine;</span>
&nbsp; &nbsp; &nbsp; <span class="green">//geomOptions.IncludeNonVisibleObjects = false;</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> geoElem = wall.get_Geometry( geomOptions );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( geoElem != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> geomObj <span class="blue">in</span> geoElem )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( geomObj <span class="blue">is</span> <span class="teal">Solid</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; solidList.Add( geomObj <span class="blue">as</span> <span class="teal">Solid</span> );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Solid</span> solid <span class="blue">in</span> solidList )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Face</span> face <span class="blue">in</span> solid.Faces )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( face <span class="blue">is</span> <span class="teal">PlanarFace</span> )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( wall.GetGeneratingElementIds( face )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .Any( x =&gt; x == openingId ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; faceList.Add( face <span class="blue">as</span> <span class="teal">PlanarFace</span> );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> faceList;
&nbsp; }
</pre>

This method is called in a loop iterating over the opening and insertion element ids returned by the `FindInserts` method, so it sets up the geometry options, retrieves and iterates over the entire wall solid anew from scratch for each opening.

You would obviously optimise this a bit in a real-life application.

That method is driven like this by the external command Execute method mainline:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp; &nbsp; <span class="teal">Result</span> commandResult = <span class="teal">Result</span>.Succeeded;
&nbsp; &nbsp; <span class="teal">Categories</span> cats = doc.Settings.Categories;
&nbsp;
&nbsp; &nbsp; <span class="teal">ElementId</span> catDoorsId = cats.get_Item(
&nbsp; &nbsp; &nbsp; <span class="teal">BuiltInCategory</span>.OST_Doors ).Id;
&nbsp;
&nbsp; &nbsp; <span class="teal">ElementId</span> catWindowsId = cats.get_Item(
&nbsp; &nbsp; &nbsp; <span class="teal">BuiltInCategory</span>.OST_Windows ).Id;
&nbsp;
&nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">ElementId</span>&gt; selectedIds = uidoc.Selection
&nbsp; &nbsp; &nbsp; &nbsp; .GetElementIds().ToList();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; trans.Start( <span class="maroon">&quot;Cmd: GetOpeningProfiles&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">ElementId</span>&gt; newIds = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">ElementId</span>&gt;();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">ElementId</span> selectedId <span class="blue">in</span> selectedIds )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Wall</span> wall = doc.GetElement( selectedId ) <span class="blue">as</span> <span class="teal">Wall</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( wall != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">PlanarFace</span>&gt; faceList = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">PlanarFace</span>&gt;();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">ElementId</span>&gt; insertIds = wall.FindInserts(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">true</span>, <span class="blue">false</span>, <span class="blue">false</span>, <span class="blue">false</span> ).ToList();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">ElementId</span> insertId <span class="blue">in</span> insertIds )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Element</span> elem = doc.GetElement( insertId );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( elem <span class="blue">is</span> <span class="teal">FamilyInstance</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">FamilyInstance</span> inst = elem <span class="blue">as</span> <span class="teal">FamilyInstance</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">CategoryType</span> catType = inst.Category
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .CategoryType;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Category</span> cat = inst.Category;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( catType == <span class="teal">CategoryType</span>.Model
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; ( cat.Id == catDoorsId
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; || cat.Id == catWindowsId ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; faceList.AddRange(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; GetWallOpeningPlanarFaces(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; wall, insertId ) );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">else</span> <span class="blue">if</span>( elem <span class="blue">is</span> <span class="teal">Opening</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; faceList.AddRange(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; GetWallOpeningPlanarFaces(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; wall, insertId ) );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">PlanarFace</span> face <span class="blue">in</span> faceList )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Plane</span> facePlane = <span class="blue">new</span> <span class="teal">Plane</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; face.ComputeNormal( <span class="teal">UV</span>.Zero ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; face.Origin );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">SketchPlane</span> sketchPlane
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">SketchPlane</span>.Create( doc, facePlane );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">CurveLoop</span> curveLoop <span class="blue">in</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; face.GetEdgesAsCurveLoops() )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Curve</span> curve <span class="blue">in</span> curveLoop )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ModelCurve</span> modelCurve = doc.Create
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .NewModelCurve( curve, sketchPlane );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; newIds.Add( modelCurve.Id );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( newIds.Count &gt; 0 )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">View</span> activeView = uidoc.ActiveGraphicalView;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; activeView.IsolateElementsTemporary( newIds );
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; trans.Commit();
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp;
<span class="blue">&nbsp; &nbsp; #region</span> Exception Handling
&nbsp;
&nbsp; &nbsp; <span class="blue">catch</span>( Autodesk.Revit.Exceptions
&nbsp; &nbsp; &nbsp; .<span class="teal">ExternalApplicationException</span> e )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; message = e.Message;
&nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.WriteLine(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Exception Encountered (Application)\n&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + e.Message + <span class="maroon">&quot;\nStack Trace: &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + e.StackTrace );
&nbsp;
&nbsp; &nbsp; &nbsp; commandResult = <span class="teal">Result</span>.Failed;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">catch</span>( Autodesk.Revit.Exceptions
&nbsp; &nbsp; &nbsp; .<span class="teal">OperationCanceledException</span> e )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.WriteLine( <span class="maroon">&quot;Operation cancelled. &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + e.Message );
&nbsp;
&nbsp; &nbsp; &nbsp; message = <span class="maroon">&quot;Operation cancelled.&quot;</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; commandResult = <span class="teal">Result</span>.Succeeded;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">catch</span>( <span class="teal">Exception</span> e )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; message = e.Message;
&nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.WriteLine(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Exception Encountered (General)\n&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + e.Message + <span class="maroon">&quot;\nStack Trace: &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + e.StackTrace );
&nbsp;
&nbsp; &nbsp; &nbsp; commandResult = <span class="teal">Result</span>.Failed;
&nbsp; &nbsp; }
&nbsp;
<span class="blue">&nbsp; &nbsp; #endregion</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> commandResult;
&nbsp; }
</pre>

Here is the result of using it on a wall with doors, a window and two openings in Revit 2015:

<center>
<img src="img/wall_openings_08.png" alt="Wall openings" width="330">
</center>

The result looks like this:

<center>
<img src="img/wall_openings_09.png" alt="Wall openings" width="328">
</center>

I also ran it on the same three walls as the ray tracing version:

<center>
<img src="img/wall_openings_05.png" alt="Wall openings" width="430">
</center>

The command executes, generates the model lines representing the opening face edges, and temporarily hides everything else:

<center>
<img src="img/wall_openings_10.png" alt="Wall openings" width="514">
</center>

As said, this code is provided by the external
command [CmdWallOpeningProfiles](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdWallOpeningProfiles.cs) in
[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples),
and the version presented above is
[release 2016.0.126.1](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2016.0.126.1).


#### <a name="5"></a>An Article on Relationship and Consumerism

As a fitting ending statement for this year, I present my translation from German of a short and succinct article by [Vivian Dittmar](http://viviandittmar.net) that she published in June 2015.

<center>
<img src="img/vifian_dittmar.jpg" alt="Vivian Dittmar" width="512">
</center>

The original German text is entitled *Warum das Streben nach Unabh&auml;ngigkeit unsere Beziehungsf&auml;higkeit untergr&auml;bt*.

Here are the PDF versions of the [German original](zip/vifian_dittmar_relationship_de.pdf) and my [English translation](zip/vifian_dittmar_relationship_en.pdf).

I'll also add the full English text right here, since it is not long:

#### <a name="6"></a>Why the Pursuit of Independence Undermines our Relationship Abilities

Most relationship guides proclaim nonsense, stating that only those who are self-sufficient and independent can truly love. These authors maintain that independence is a prerequisite for a healthy partnership. Is independence really the key to happiness in matters of love? The speaker, seminar leader and author Vivian Dittmar speaks about the myth of independence.

Should true love let go of the partner? Are only self-sufficient people capable of loving? Does needing one another invariably lead to the hell of co-dependency?

Independence &ndash; sounds great, and fits perfectly with our current collective fixation on individualisation.

From an early age we are taught that each of us is responsible for his own happiness and that dependence on others is something for weaklings who prefer to remain in their own secure little nest. For many, dependence equals vulnerability. Dependence also means that others have power over you. Therefore, dependency is an absolute no-go.

This belief coincides beautifully with the objectives of capitalism. Whoever wants to be independent, will mainly focus on earning sufficient money to take care of themselves.

From this perspective, all too large investments in relationships with other people become a highly dubious matter and are therefore avoided.

#### <a name="7"></a>Consumerism cannot Satisfy the Desire for Relationship

We try to compensate the deficits that we inevitably develop as social beings based on this strategy by satisfaction in the form of consumerism. Advertising helps associate our yearning for relationship with the corresponding products: the deodorant, the car, the drink, the outfit, assure that our desire for togetherness and belonging is finally answered.

These substitute satisfactions frequently lead to addiction, requiring ever-higher doses in ever-shorter intervals to cause the desired effect. However, the primordial human desire for relationship, contact, and true togetherness is not fulfilled.

This ‘real togetherness’ can only happen if we allow ourselves to need each other. I say ‘allow’ because today, more than ever before, it has become our personal decision whether to be involved with someone or not.

#### <a name="8"></a>Isolation as a Symptom of Apparent Independence

People have always shaped and maintained relationships because they were dependent on each other. Today, to an almost frightening degree, we can choose only to need people we never see, without any personal relationship at all. I refer to the employees of the companies that manufacture all our products and try to fulfil all imaginable needs with their services. On one hand, this has great advantages: we can choose the relationships we maintain to an unprecedented degree. Wow, that's really fantastic! However, it also has serious drawbacks and brings a whole new set of challenges.

One is the question of how to create, maintain and grow relations when we are able to cope alone in all areas. Unlike in traditional cultures, relationships are no longer automatic side effects of daily life. In western nations, the trend towards increasing social isolation and loneliness in the midst of society is a symptom of this development.
￼
#### <a name="9"></a>We need one another!

Another side effect is that the old rules that governed our ancestors’ relationship structure over centuries or even millennia suddenly no longer apply. The clearest example of this development is the altered power imbalance between men and women. With increasing emancipation a growing number of women became aware of their independence, decided against classical roles and thus rejected this imbalance.

Today it is obvious for most couples to pursue a relationship between equals. Unfortunately, this does not mean that they know how this can be achieved. If things go badly, both strive to fulfil our cultural ideal of independence and only notice too late that this does not guarantee a successful relationship. In these situations, fighting for power or increasing alienation is inevitable, because neither party is honest and open with their true needs.

We lack the knowledge about and practise in dealing constructively with conflicting needs. We lack role models showing how togetherness on equal terms can succeed, without pretending a perfect world, in which we either always agree or slip into infighting.

A prerequisite for the development of these skills is to start from a clean slate and admit the simple fact: We need each other! Although I can meditate myself out of my human condition or buy all possible satisfaction with just a modest fortune; we humans are and remain social beings needing other people to be happy.

We need to stop avoiding facing this fact before we can begin developing a constructive approach to it. Only then can we completely bid farewell both to the power paradigm, in which all needs in a relationship always represent power potential, and to the consumerism paradigm, in which needs represent buying potential. Then we can move into a new relationship paradigm, in which needs have a completely different meaning.

They represent neither power nor consumerism, but relationship potential.

#### <a name="10"></a>Mature Relationship: Balancing Autonomy and Dependence

The idea that adulthood means not needing anyone else is a fundamental misconception of our culture.

In fact, it is the adolescent who dreams of giving the finger to the world and disappearing into the sunset on a motorbike. Real adulthood means being aware of my self-reliance as well as my dependence and having learned to maintain a healthy balance between them. Specifically, this means that a mature relationship always includes both: needing and not needing, dependence and autonomy.

Only this interplay enables respectful, dignified and thus also fulfilling togetherness. In this respect, there is some truth in the myth of independence as the key to happiness.

But, as so often, that is only half the story.

I hope you enjoyed this little excursion into a totally different realm and send you the best seasonal greetings, wishing a Very Merry Xmas and a Happy New Year to all!
