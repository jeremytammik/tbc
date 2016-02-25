<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- create a module to identify whether changes have been made to specific elements:
  on document load: or all relevant elements, grab all relevant element data, encode into a string, create a hash
  on document save: repeat and check for differences: result: modified elements
  0739_create_own_key.htm
  [Revit project identification](http://the3dwebcoder.typepad.com/blog/2015/07/fireratingcloud-round-trip-and-on-mongolab.html#2)

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewAPI #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restAPI
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge
@AutodeskReCap @Adsk3dsMax
#revitAPI #bim #aec #3dwebcoder #adsk #adskdevnetwrk @jimquanci @keanw
#au2015 #rtceur
#eraofconnection

Revit API, Jeremy Tammik, akn_include

Tracking Element Modification #revitAPI #bim #aec #3dwebcoder #adsk @AutodeskRevit #adskdevnetwrk

How to determine and track element modification?
I implemented a solution!
&ndash; Two approaches
&ndash; Task analysis
&ndash; Modification tracker
&ndash; Creating an element state snapshot
&ndash; Determining which elements to track
&ndash; Implementation
&ndash; Geometrical comparison
&ndash; String formatting
&ndash; Retrieve solid vertices
&ndash; GetTrackedElements &ndash; retrieve elements of interest
&ndash; GetElementState &ndash; store element state
&ndash; Creating a database state snapshot
&ndash; Report differences
&ndash; External command mainline Execute method
&ndash; Sample runs
&ndash; Demo recording
&ndash; Download...

-->

### Tracking Element Modification

Several people asked me recently about how to determine and track element modification, and I have heard this same question dozens of times in the more distant past as well.

Now it was brought up again here at the Munich Cloud Accelerator by John Allan-Jones of [Atkins](http://atkinsglobal.com), and I was motivated to implement a solution:

- [Two approaches](#0)
- [Task analysis](#1)
- [Modification tracker](#2)
- [Creating an element state snapshot](#3)
- [Determining which elements to track](#4)
- [Implementation](#5)
    - [Geometrical comparison](#5.1)
    - [String formatting](#5.2)
    - [Retrieve solid vertices](#5.3)
    - [GetTrackedElements &ndash; retrieve elements of interest](#5.4)
    - [GetElementState &ndash; store element state](#5.5)
    - [Creating a database state snapshot](#5.6)
    - [Report differences](#5.7)
    - [External command mainline Execute method](#5.8)
- [Sample runs](#6)
- [Demo recording](#7)
- [Download](#8)

For a quick first impression, you can jump to the [two-and-a-half minute video recording](#7) showing it in action and read [an initial comment or two](http://forums.autodesk.com/t5/revit-api/tracking-revit-bim-database-and-individual-element-property/m-p/5998729).




#### <a name="0"></a>Two Approaches

Most ideas for approaches to this I heard so far were pretty fixed on tracking individual events in the Revit database, e.g. using the DocumentChanged event or a DMU framework dynamic model updater and keeping track of every modification.

In both cases, the add-in will be receiving and processing huge amounts of useless data and struggling to filter out what is really needed.

I suggest a radically different approach that is completely independent of events, Revit internals, real-time analysis and continuous tracking.

I suggest simply creating a snapshot that captures all the properties of interest and reporting the differences between two such snapshots.


#### <a name="1"></a>Task Analysis

Let's take a step back and think about what we really are after.

To track changes, you need to consider what kind of changes you are interested in.

These changes have to do with two things:

- Which BIM elements are of interest?
- What defines the BIM element state that is of interest?

The former issue is a simple and pretty obvious matter of defining the correct filtered element collector filters to apply.

The second aspect is one that I'll address in more depth.

It is hopefully possible to capture the BIM element state that is of interest by querying its properties, e.g.:

- Name
- Class, family, type
- Location
- Geometry
- Parameter data
- Etc.

If somebody makes a change to the BIM model that you wish to track, how and where will that change affect the model?

Certainly there must be a property somewhere in the database that you can use to determine the state before and after and identify that the modification happened.

So let's use these thoughts to define a super simple and efficient modification tracker.


#### <a name="2"></a>Modification Tracker

I suggest the following approach to implement a simple and efficient modification tracker:

- Start tracking by capturing and storing the relevant state of all elements of interest.
- Stop tracking by capturing an updated version of that data.
- Report changes by comparing the two snapshots.

No need for events!

No need for cumbersome instrumentation!

No continuous data collection overhead!

My sample implements one single command, which alternates between two actions:

Create and store a snapshot of the current database state if none is already present.
If a previous snapshot is already present, create a new one, compare them, and report the modifications.

You obviously might, if you wish, automatically execute the first action on document load and the second on document save.

If you create an external application to host a button triggering these two action, you would obviously make use of the possibility
to [roll your own toggle button](http://thebuildingcoder.typepad.com/blog/2012/11/roll-your-own-toggle-button.html).


#### <a name="3"></a>Creating an Element State Snapshot

Retrieve all the element data that you need to capture the state of the element of interest to you, e.g., its location, geometry, parameter data or any other characteristics that you would like to track.

Encode that information into a string.

Compute a hash code for that string.

We use the hash code to determine whether the state has been modified compared to a new element state snapshot made at a later time.

We could obviously also store the entire original string representation instead of using a hash code. The hash code is small and handy, whereas the entire string contains all the original data. It is up to you to choose which you would like to use.

You need to ensure that every relevant change made to the tracked element really does make a difference to the string representation that you generate, and that the hash code you compute really is affected by every modification made.

This concept is similar to my appeal
to [create your own key](http://thebuildingcoder.typepad.com/blog/2012/03/great-ocean-road-and-creating-your-own-key.html#2),
and touches on some aspects of my more
recent [Revit project identification](http://the3dwebcoder.typepad.com/blog/2015/07/fireratingcloud-round-trip-and-on-mongolab.html#2).

One interesting sub-topic here is also how to create a sensible and useful canonical snapshot of the element geometry.

I had a stab back in 2009 analysing [nested instance geometry](http://thebuildingcoder.typepad.com/blog/2009/05/nested-instance-geometry.html) and implementing the `GetVertices` method that simply returns a sorted list of all unique vertices of a given solid in lexicographical order.

Since the element geometry can contain multiple solids, and they can be nested in a whole hierarchy of transformed instances, I need to traverse the element geometry, retrieve the solids from each level and apply the appropriate transformations to them, similarly to the approach used
to retrieve [real-world concrete corner coordinates](http://thebuildingcoder.typepad.com/blog/2012/06/real-world-concrete-corner-coordinates.html).

For family instances, however, I chose to ignore the symbol geometry. The family type or symbol should be considered a constant. Any change to the geometry in a family instance should be reflected in a corresponding change to the family type.
Therefore, I skip the geometry traversal and analysis for family instances and assume that these kind of changes to the family definition will be tracked elsewhere.

For a family instance, the snapshot just stores the family name, type and category instead of the geometry vertices.

The current initial implementation grabs the following element properties to create a snapshot of its state:

- Location
- Parameter names and values
- Family instance: family, type and category
- Not a family instance: bounding box and geometry vertices

The parameter retrieval and storage is based on similar code that I implemented and used in
the [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp) and
the [CompHound](https://github.com/CompHound/CompHound.github.io)
uploader [add-in](https://github.com/CompHound/CompHoundRvt).

Note that a parameter set is unordered. We need to sort the parameters by name to ensure a canonical reproducible representation, just like we need to sort the geometry vertices lexicographically for the same purpose. If we do not, they might be retrieved in an arbitrary different order later on and our string comparison would fail, even though the element data has not really changed.

As an example, here is the first element state string representation we generated:

<pre>
Location=(0,0,0), (22.97,0,0),
Box=((-0.33,-0.33,0),(23.29,0.33,13.12)),
Vertices=(-0.33,-0.33,0), (-0.33,-0.33,13.12), (-0.33,0.33,0), (-0.33,0.33,13.12), (0.33,0.33,0), (0.33,0.33,13.12), (5.08,-0.33,3.94), (5.08,-0.33,5.94), (5.08,0.33,3.94), (5.08,0.33,5.94), (6.41,-0.33,3.94), (6.41,-0.33,5.94), (6.41,0.33,3.94), (6.41,0.33,5.94), (9.98,-0.33,0), (9.98,-0.33,7), (9.98,0.33,0), (9.98,0.33,7), (12.98,-0.33,0), (12.98,-0.33,7), (12.98,0.33,0), (12.98,0.33,7), (16.56,-0.33,3.94), (16.56,-0.33,5.94), (16.56,0.33,3.94), (16.56,0.33,5.94), (17.89,-0.33,3.94), (17.89,-0.33,5.94), (17.89,0.33,3.94), (17.89,0.33,5.94), (22.64,0.33,0), (22.64,0.33,13.12), (23.29,-0.33,0), (23.29,-0.33,13.12), (23.29,0.33,0), (23.29,0.33,13.12),
Parameters={"Area":"26 m²","Base Constraint":"Level 1","Base Extension Distance":"0","Base is Attached":"No","Base Offset":"0","Comments":"","Enable Analytical Model":"No","Image":"<None>","Length":"7000","Location Line":"Wall Centerline","Mark":"","Phase Created":"New Construction","Phase Demolished":"None","Related to Mass":"No","Room Bounding":"Yes","Structural Usage":"Non-bearing","Structural":"No","Top Constraint":"Up to level: Level 2","Top Extension Distance":"0","Top is Attached":"No","Top Offset":"0","Unconnected Height":"4000","Volume":"5.27 m³"}
</pre>

For the current final implementation, please refer to the [GetElementState method implementation](5.5) below.


#### <a name="4"></a>Determining Which Elements to Track

Element retrieval from the database is always achieved using a filtered element collector.

Determining which elements to track is just a matter of defining the appropriate filters to apply to the collector.

We already looked at numerous different filtering examples in the past.

In this case, we may be interested in all or just certain sets of elements.

You will need to define that in detail for yourself depending on your exact needs.

Here are some of the existing examples and discussions on this topic:

<!--- /a/doc/revit/tbc/ $ blmd 1392 0398 0404 -->

- [Retrieving all elements](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.9)
- [Retrieving all model elements](http://thebuildingcoder.typepad.com/blog/2016/01/retrieving-all-model-elements.html)
- [Retrieving MEP elements and connectors](http://thebuildingcoder.typepad.com/blog/2010/06/retrieve-mep-elements-and-connectors.html)
- [Retrieving structural elements](http://thebuildingcoder.typepad.com/blog/2010/07/retrieve-structural-elements.html)
- [Recent thread on model categories](http://forums.autodesk.com/t5/revit-api/all-model-elements-in-project/m-p/5972035)

The current implementation selects all elements that:

- Are not ElementType objects
- Are view independent
- Have a category whose category type is `CategoryType.Model`
- Have a non-null bounding box
- Have non-null geometry

For the current final implementation, please refer to the [GetTrackedElements method implementation](5.4) below.


#### <a name="5"></a>Implementation

I created the [TrackChanges GitHub repository](https://github.com/jeremytammik/TrackChanges) to host this project, including the entire source code, Visual Studio project and add-in manifest.

The version discussed here is [release 2016.0.0.1](https://github.com/jeremytammik/TrackChanges/releases/tag/2016.0.0.1).

If you are seriously interested in taking a deeper look, you will obviously fork and clone that repository for yourself and explore the code directly in the Visual Studio IDE.

Otherwise, for the sake of completeness and to help the Internet search engines find this discussion, let's present and discuss the complete source code for the external command and its various helper methods right here, divided into the following code regions:

- [Geometrical Comparison](#5.1)
- [String formatting](#5.2)
- [Retrieve solid vertices](#5.3)
- [Retrieve elements of interest](#5.4)
- [Store element state](#5.5)
- [Creating a Database State Snapshot](#5.6)
- [Report Differences](#5.7)
- [External Command Mainline Execute Method](#5.8)


#### <a name="5.1"></a>Geometrical Comparison

Define helper functions  and other support for geometrical comparisons:

<pre class="code">
&nbsp; <span class="blue">const</span> <span class="blue">double</span> _eps = 1.0e-9;
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">double</span> Eps
&nbsp; {
&nbsp; &nbsp; <span class="blue">get</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> _eps;
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">double</span> MinLineLength
&nbsp; {
&nbsp; &nbsp; <span class="blue">get</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> _eps;
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">double</span> TolPointOnPlane
&nbsp; {
&nbsp; &nbsp; <span class="blue">get</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> _eps;
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">bool</span> IsZero(
&nbsp; &nbsp; <span class="blue">double</span> a,
&nbsp; &nbsp; <span class="blue">double</span> tolerance )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> tolerance &gt; <span class="teal">Math</span>.Abs( a );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">bool</span> IsZero( <span class="blue">double</span> a )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> IsZero( a, _eps );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">bool</span> IsEqual( <span class="blue">double</span> a, <span class="blue">double</span> b )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> IsZero( b - a );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">int</span> Compare( <span class="blue">double</span> a, <span class="blue">double</span> b )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> IsEqual( a, b ) ? 0 : ( a &lt; b ? -1 : 1 );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">int</span> Compare( <span class="teal">XYZ</span> p, <span class="teal">XYZ</span> q )
&nbsp; {
&nbsp; &nbsp; <span class="blue">int</span> d = Compare( p.X, q.X );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( 0 == d )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; d = Compare( p.Y, q.Y );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( 0 == d )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; d = Compare( p.Z, q.Z );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> d;
&nbsp; }
</pre>

#### <a name="5.2"></a>String Formatting

Define a bunch of helper functions to generate string representations of various objects:

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Convert a string to a byte array.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="blue">byte</span>[] GetBytes( <span class="blue">string</span> str )
&nbsp; {
&nbsp; &nbsp; <span class="blue">byte</span>[] bytes = <span class="blue">new</span> <span class="blue">byte</span>[str.Length
&nbsp; &nbsp; &nbsp; * <span class="blue">sizeof</span>( <span class="blue">char</span> )];
&nbsp;
&nbsp; &nbsp; System.<span class="teal">Buffer</span>.BlockCopy( str.ToCharArray(),
&nbsp; &nbsp; &nbsp; 0, bytes, 0, bytes.Length );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> bytes;
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a string for a real number</span>
&nbsp; <span class="gray">///</span><span class="green"> formatted to two decimal places.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">string</span> RealString( <span class="blue">double</span> a )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> a.ToString( <span class="maroon">&quot;0.##&quot;</span> );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a string for an XYZ point</span>
&nbsp; <span class="gray">///</span><span class="green"> or vector with its coordinates</span>
&nbsp; <span class="gray">///</span><span class="green"> formatted to two decimal places.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">string</span> PointString( <span class="teal">XYZ</span> p )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">string</span>.Format( <span class="maroon">&quot;({0},{1},{2})&quot;</span>,
&nbsp; &nbsp; &nbsp; RealString( p.X ),
&nbsp; &nbsp; &nbsp; RealString( p.Y ),
&nbsp; &nbsp; &nbsp; RealString( p.Z ) );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a string for this bounding box</span>
&nbsp; <span class="gray">///</span><span class="green"> with its coordinates formatted to two</span>
&nbsp; <span class="gray">///</span><span class="green"> decimal places.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">string</span> BoundingBoxString(
&nbsp; &nbsp; <span class="teal">BoundingBoxXYZ</span> bb )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">string</span>.Format( <span class="maroon">&quot;({0},{1})&quot;</span>,
&nbsp; &nbsp; &nbsp; PointString( bb.Min ),
&nbsp; &nbsp; &nbsp; PointString( bb.Max ) );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a string for this point array</span>
&nbsp; <span class="gray">///</span><span class="green"> with its coordinates formatted to two</span>
&nbsp; <span class="gray">///</span><span class="green"> decimal places.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">string</span> PointArrayString( <span class="teal">IList</span>&lt;<span class="teal">XYZ</span>&gt; pts )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">string</span>.Join( <span class="maroon">&quot;, &quot;</span>,
&nbsp; &nbsp; &nbsp; pts.Select&lt;<span class="teal">XYZ</span>, <span class="blue">string</span>&gt;(
&nbsp; &nbsp; &nbsp; &nbsp; p =&gt; PointString( p ) ) );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a string for this curve with its</span>
&nbsp; <span class="gray">///</span><span class="green"> tessellated point coordinates formatted</span>
&nbsp; <span class="gray">///</span><span class="green"> to two decimal places.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">string</span> CurveTessellateString(
&nbsp; &nbsp; <span class="teal">Curve</span> curve )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> PointArrayString( curve.Tessellate() );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a string for this curve with its</span>
&nbsp; <span class="gray">///</span><span class="green"> tessellated point coordinates formatted</span>
&nbsp; <span class="gray">///</span><span class="green"> to two decimal places.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">string</span> LocationString(
&nbsp; &nbsp; <span class="teal">Location</span> location )
&nbsp; {
&nbsp; &nbsp; <span class="teal">LocationPoint</span> lp = location <span class="blue">as</span> <span class="teal">LocationPoint</span>;
&nbsp; &nbsp; <span class="teal">LocationCurve</span> lc = ( <span class="blue">null</span> == lp )
&nbsp; &nbsp; &nbsp; ? location <span class="blue">as</span> <span class="teal">LocationCurve</span>
&nbsp; &nbsp; &nbsp; : <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">null</span> == lp
&nbsp; &nbsp; &nbsp; ? ( <span class="blue">null</span> == lc
&nbsp; &nbsp; &nbsp; &nbsp; ? <span class="blue">null</span>
&nbsp; &nbsp; &nbsp; &nbsp; : CurveTessellateString( lc.Curve ) )
&nbsp; &nbsp; &nbsp; : PointString( lp.Point );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a JSON string representing a dictionary</span>
&nbsp; <span class="gray">///</span><span class="green"> of the given parameter names and values.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">string</span> GetPropertiesJson(
&nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">Parameter</span>&gt; parameters )
&nbsp; {
&nbsp; &nbsp; <span class="blue">int</span> n = parameters.Count;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="blue">string</span>&gt; a = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="blue">string</span>&gt;( n );
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Parameter</span> p <span class="blue">in</span> parameters )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; a.Add( <span class="blue">string</span>.Format( <span class="maroon">&quot;\&quot;{0}\&quot;:\&quot;{1}\&quot;&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; p.Definition.Name, p.AsValueString() ) );
&nbsp; &nbsp; }
&nbsp; &nbsp; a.Sort();
&nbsp; &nbsp; <span class="blue">string</span> s = <span class="blue">string</span>.Join( <span class="maroon">&quot;,&quot;</span>, a );
&nbsp; &nbsp; <span class="blue">return</span> <span class="maroon">&quot;{&quot;</span> + s + <span class="maroon">&quot;}&quot;</span>;
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a string describing the given element:</span>
&nbsp; <span class="gray">///</span><span class="green"> .NET type name,</span>
&nbsp; <span class="gray">///</span><span class="green"> category name,</span>
&nbsp; <span class="gray">///</span><span class="green"> family and symbol name for a family instance,</span>
&nbsp; <span class="gray">///</span><span class="green"> element id and element name.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">string</span> ElementDescription(
&nbsp; &nbsp; <span class="teal">Element</span> e )
&nbsp; {
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == e )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="maroon">&quot;&lt;null&gt;&quot;</span>;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="green">// For a wall, the element name equals the</span>
&nbsp; &nbsp; <span class="green">// wall type name, which is equivalent to the</span>
&nbsp; &nbsp; <span class="green">// family name ...</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">FamilyInstance</span> fi = e <span class="blue">as</span> <span class="teal">FamilyInstance</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> typeName = e.GetType().Name;
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> categoryName = ( <span class="blue">null</span> == e.Category )
&nbsp; &nbsp; &nbsp; ? <span class="blue">string</span>.Empty
&nbsp; &nbsp; &nbsp; : e.Category.Name + <span class="maroon">&quot; &quot;</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> familyName = ( <span class="blue">null</span> == fi )
&nbsp; &nbsp; &nbsp; ? <span class="blue">string</span>.Empty
&nbsp; &nbsp; &nbsp; : fi.Symbol.Family.Name + <span class="maroon">&quot; &quot;</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> symbolName = ( <span class="blue">null</span> == fi
&nbsp; &nbsp; &nbsp; || e.Name.Equals( fi.Symbol.Name ) )
&nbsp; &nbsp; &nbsp; &nbsp; ? <span class="blue">string</span>.Empty
&nbsp; &nbsp; &nbsp; &nbsp; : fi.Symbol.Name + <span class="maroon">&quot; &quot;</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">string</span>.Format( <span class="maroon">&quot;{0} {1}{2}{3}&lt;{4} {5}&gt;&quot;</span>,
&nbsp; &nbsp; &nbsp; typeName, categoryName, familyName,
&nbsp; &nbsp; &nbsp; symbolName, e.Id.IntegerValue, e.Name );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">string</span> ElementDescription(
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="blue">int</span> element_id )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> ElementDescription( doc.GetElement(
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">ElementId</span>( element_id ) ) );
&nbsp; }
</pre>

#### <a name="5.3"></a>Retrieve Solid Vertices

Retrieve solid vertices and sort them lexicographically to define an extremely simplified form of a partial canonical element geometry representation.

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Define equality between XYZ objects, ensuring </span>
&nbsp; <span class="gray">///</span><span class="green"> that almost equal points compare equal.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">class</span> <span class="teal">XyzEqualityComparer</span> : <span class="teal">IEqualityComparer</span>&lt;<span class="teal">XYZ</span>&gt;
&nbsp; {
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">bool</span> Equals( <span class="teal">XYZ</span> p, <span class="teal">XYZ</span> q )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> p.IsAlmostEqualTo( q );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">int</span> GetHashCode( <span class="teal">XYZ</span> p )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> PointString( p ).GetHashCode();
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Add the vertices of the given solid to </span>
&nbsp; <span class="gray">///</span><span class="green"> the vertex lookup dictionary.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="blue">void</span> AddVertices(
&nbsp; &nbsp; <span class="teal">Dictionary</span>&lt;<span class="teal">XYZ</span>, <span class="blue">int</span>&gt; vertexLookup,
&nbsp; &nbsp; <span class="teal">Transform</span> t,
&nbsp; &nbsp; <span class="teal">Solid</span> s )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Debug</span>.Assert( 0 &lt; s.Edges.Size,
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected a non-empty solid&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Face</span> f <span class="blue">in</span> s.Faces )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Mesh</span> m = f.Triangulate();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">XYZ</span> p <span class="blue">in</span> m.Vertices )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> q = t.OfPoint( p );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( !vertexLookup.ContainsKey( q ) )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; vertexLookup.Add( q, 1 );
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ++vertexLookup[q];
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Recursively add vertices of all solids found</span>
&nbsp; <span class="gray">///</span><span class="green"> in the given geometry to the vertex lookup.</span>
&nbsp; <span class="gray">///</span><span class="green"> Untested!</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="blue">void</span> AddVertices(
&nbsp; &nbsp; <span class="teal">Dictionary</span>&lt;<span class="teal">XYZ</span>, <span class="blue">int</span>&gt; vertexLookup,
&nbsp; &nbsp; <span class="teal">Transform</span> t,
&nbsp; &nbsp; <span class="teal">GeometryElement</span> geo )
&nbsp; {
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == geo )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( <span class="blue">null</span> != geo, <span class="maroon">&quot;null GeometryElement&quot;</span> );
&nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> System.<span class="teal">ArgumentException</span>( <span class="maroon">&quot;null GeometryElement&quot;</span> );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> obj <span class="blue">in</span> geo )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Solid</span> solid = obj <span class="blue">as</span> <span class="teal">Solid</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != solid )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( 0 &lt; solid.Faces.Size )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; AddVertices( vertexLookup, t, solid );
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">GeometryInstance</span> inst = obj <span class="blue">as</span> <span class="teal">GeometryInstance</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != inst )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//GeometryElement geoi = inst.GetInstanceGeometry();</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> geos = inst.GetSymbolGeometry();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//Debug.Assert( null == geoi || null == geos,</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; &quot;expected either symbol or instance geometry, not both&quot; );</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( <span class="blue">null</span> != inst.Transform,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;null inst.Transform&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//Debug.Assert( null != inst.GetSymbolGeometry(),</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; &quot;null inst.GetSymbolGeometry&quot; );</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != geos )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; AddVertices( vertexLookup,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; inst.Transform.Multiply( t ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; geos );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a sorted list of all unique vertices </span>
&nbsp; <span class="gray">///</span><span class="green"> of all solids in the given element's geometry</span>
&nbsp; <span class="gray">///</span><span class="green"> in lexicographical order.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; GetCanonicVertices( <span class="teal">Element</span> e )
&nbsp; {
&nbsp; &nbsp; <span class="teal">GeometryElement</span> geo = e.get_Geometry( <span class="blue">new</span> <span class="teal">Options</span>() );
&nbsp; &nbsp; <span class="teal">Transform</span> t = <span class="teal">Transform</span>.Identity;
&nbsp;
&nbsp; &nbsp; <span class="teal">Dictionary</span>&lt;<span class="teal">XYZ</span>, <span class="blue">int</span>&gt; vertexLookup
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">Dictionary</span>&lt;<span class="teal">XYZ</span>, <span class="blue">int</span>&gt;(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">XyzEqualityComparer</span>() );
&nbsp;
&nbsp; &nbsp; AddVertices( vertexLookup, t, geo );
&nbsp;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; keys = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt;( vertexLookup.Keys );
&nbsp;
&nbsp; &nbsp; keys.Sort( Compare );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> keys;
&nbsp; }
</pre>

#### <a name="5.4"></a>GetTrackedElements &ndash; Retrieve Elements of Interest

Retrieve all the elements of interest:

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Retrieve all elements to track.</span>
&nbsp; <span class="gray">///</span><span class="green"> It is up to you to decide which elements</span>
&nbsp; <span class="gray">///</span><span class="green"> are of interest to you.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="teal">IEnumerable</span>&lt;<span class="teal">Element</span>&gt; GetTrackedElements(
&nbsp; &nbsp; <span class="teal">Document</span> doc )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Categories</span> cats = doc.Settings.Categories;
&nbsp;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">ElementFilter</span>&gt; a = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">ElementFilter</span>&gt;();
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Category</span> c <span class="blue">in</span> cats )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="teal">CategoryType</span>.Model == c.CategoryType )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; a.Add( <span class="blue">new</span> <span class="teal">ElementCategoryFilter</span>( c.Id ) );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">ElementFilter</span> isModelCategory
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">LogicalOrFilter</span>( a );
&nbsp;
&nbsp; &nbsp; <span class="teal">Options</span> opt = <span class="blue">new</span> <span class="teal">Options</span>();
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .WhereElementIsNotElementType()
&nbsp; &nbsp; &nbsp; .WhereElementIsViewIndependent()
&nbsp; &nbsp; &nbsp; .WherePasses( isModelCategory )
&nbsp; &nbsp; &nbsp; .Where&lt;<span class="teal">Element</span>&gt;( e =&gt;
&nbsp; &nbsp; &nbsp; &nbsp; ( <span class="blue">null</span> != e.get_BoundingBox( <span class="blue">null</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; ( <span class="blue">null</span> != e.get_Geometry( opt ) ) );
&nbsp; }
</pre>

#### <a name="5.5"></a>GetElementState &ndash; Store Element State

Determine the state of an element and encode it as a string:

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a string representing the given element</span>
&nbsp; <span class="gray">///</span><span class="green"> state. This is the information you wish to track.</span>
&nbsp; <span class="gray">///</span><span class="green"> It is up to you to ensure that all data you are</span>
&nbsp; <span class="gray">///</span><span class="green"> interested in really is included in this snapshot.</span>
&nbsp; <span class="gray">///</span><span class="green"> In this case, we ignore all elements that do not</span>
&nbsp; <span class="gray">///</span><span class="green"> have a valid bounding box.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="blue">string</span> GetElementState( <span class="teal">Element</span> e )
&nbsp; {
&nbsp; &nbsp; <span class="blue">string</span> s = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">BoundingBoxXYZ</span> bb = e.get_BoundingBox( <span class="blue">null</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != bb )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="blue">string</span>&gt; properties = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="blue">string</span>&gt;();
&nbsp;
&nbsp; &nbsp; &nbsp; properties.Add( ElementDescription( e )
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot; at &quot;</span> + LocationString( e.Location ) );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( !( e <span class="blue">is</span> <span class="teal">FamilyInstance</span> ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; properties.Add( <span class="maroon">&quot;Box=&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + BoundingBoxString( bb ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; properties.Add( <span class="maroon">&quot;Vertices=&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + PointArrayString( GetCanonicVertices( e ) ) );
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; properties.Add( <span class="maroon">&quot;Parameters=&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + GetPropertiesJson( e.GetOrderedParameters() ) );
&nbsp;
&nbsp; &nbsp; &nbsp; s = <span class="blue">string</span>.Join( <span class="maroon">&quot;, &quot;</span>, properties );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">//Debug.Print( s );</span>
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> s;
&nbsp; }
</pre>

#### <a name="5.6"></a>Creating a Database State Snapshot

Retrieve each element's state, encode it as a string and store their resulting hash codes in a dictionary mapping element id to hash code:

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a dictionary mapping element id values</span>
&nbsp; <span class="gray">///</span><span class="green"> to hash codes of the element state strings. </span>
&nbsp; <span class="gray">///</span><span class="green"> This represents a snapshot of the current </span>
&nbsp; <span class="gray">///</span><span class="green"> database state.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="teal">Dictionary</span>&lt;<span class="blue">int</span>, <span class="blue">string</span>&gt; GetSnapshot(
&nbsp; &nbsp; <span class="teal">IEnumerable</span>&lt;<span class="teal">Element</span>&gt; a )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Dictionary</span>&lt;<span class="blue">int</span>, <span class="blue">string</span>&gt; d
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">Dictionary</span>&lt;<span class="blue">int</span>, <span class="blue">string</span>&gt;();
&nbsp;
&nbsp; &nbsp; <span class="teal">SHA256</span> hasher = <span class="teal">SHA256Managed</span>.Create();
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Element</span> e <span class="blue">in</span> a )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="green">//Debug.Print( e.Id.IntegerValue.ToString() </span>
&nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; + &quot; &quot; + e.GetType().Name );</span>
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">string</span> s = GetElementState( e );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != s )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">string</span> hashb64 = <span class="teal">Convert</span>.ToBase64String(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; hasher.ComputeHash( GetBytes( s ) ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; d.Add( e.Id.IntegerValue, hashb64 );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> d;
&nbsp; }
</pre>

#### <a name="5.7"></a>Report differences

Determine and report the differences between the two states at the start and end of the tracking period:

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Compare the start and end states and report the </span>
&nbsp; <span class="gray">///</span><span class="green"> differences found. In this implementation, we</span>
&nbsp; <span class="gray">///</span><span class="green"> just store a hash code of the element state.</span>
&nbsp; <span class="gray">///</span><span class="green"> If you choose to store the full string </span>
&nbsp; <span class="gray">///</span><span class="green"> representation, you can use that for comparison,</span>
&nbsp; <span class="gray">///</span><span class="green"> and then report exactly what changed and the</span>
&nbsp; <span class="gray">///</span><span class="green"> original values as well.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="blue">void</span> ReportDifferences(
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="teal">Dictionary</span>&lt;<span class="blue">int</span>, <span class="blue">string</span>&gt; start_state,
&nbsp; &nbsp; <span class="teal">Dictionary</span>&lt;<span class="blue">int</span>, <span class="blue">string</span>&gt; end_state )
&nbsp; {
&nbsp; &nbsp; <span class="blue">int</span> n1 = start_state.Keys.Count;
&nbsp; &nbsp; <span class="blue">int</span> n2 = end_state.Keys.Count;
&nbsp;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="blue">int</span>&gt; keys = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="blue">int</span>&gt;( start_state.Keys );
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="blue">int</span> id <span class="blue">in</span> end_state.Keys )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( !keys.Contains( id ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; keys.Add( id );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; keys.Sort();
&nbsp;
&nbsp; &nbsp; <span class="blue">int</span> n = keys.Count;
&nbsp;
&nbsp; &nbsp; <span class="teal">Debug</span>.Print(
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;{0} elements before, {1} elements after, {2} total&quot;</span>,
&nbsp; &nbsp; &nbsp; n1, n2, n );
&nbsp;
&nbsp; &nbsp; <span class="blue">int</span> nAdded = 0;
&nbsp; &nbsp; <span class="blue">int</span> nDeleted = 0;
&nbsp; &nbsp; <span class="blue">int</span> nModified = 0;
&nbsp; &nbsp; <span class="blue">int</span> nIdentical = 0;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="blue">string</span>&gt; report = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="blue">string</span>&gt;();
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="blue">int</span> id <span class="blue">in</span> keys )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( !start_state.ContainsKey( id ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; ++nAdded;
&nbsp; &nbsp; &nbsp; &nbsp; report.Add( id.ToString() + <span class="maroon">&quot; added &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + ElementDescription( doc, id ) );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span> <span class="blue">if</span>( !end_state.ContainsKey( id ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; ++nDeleted;
&nbsp; &nbsp; &nbsp; &nbsp; report.Add( id.ToString() + <span class="maroon">&quot; deleted&quot;</span> );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span> <span class="blue">if</span>( start_state[id] != end_state[id] )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; ++nModified;
&nbsp; &nbsp; &nbsp; &nbsp; report.Add( id.ToString() + <span class="maroon">&quot; modified &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + ElementDescription( doc, id ) );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; ++nIdentical;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> msg = <span class="blue">string</span>.Format(
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Stopped tracking changes now.\r\n&quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;{0} deleted, {1} added, {2} modified, &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;{3} identical elements:&quot;</span>,
&nbsp; &nbsp; &nbsp; nDeleted, nAdded, nModified, nIdentical );
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> s = <span class="blue">string</span>.Join( <span class="maroon">&quot;\r\n&quot;</span>, report );
&nbsp;
&nbsp; &nbsp; <span class="teal">Debug</span>.Print( msg + <span class="maroon">&quot;\r\n&quot;</span> + s );
&nbsp; &nbsp; <span class="teal">TaskDialog</span> dlg = <span class="blue">new</span> <span class="teal">TaskDialog</span>( <span class="maroon">&quot;Track Changes&quot;</span> );
&nbsp; &nbsp; dlg.MainInstruction = msg;
&nbsp; &nbsp; dlg.MainContent = s;
&nbsp; &nbsp; dlg.Show();
&nbsp; }
</pre>

#### <a name="5.8"></a>External Command Mainline Execute Method

Note that this command makes no modifications to the Revit database, so it uses the `ReadOnly` transaction mode.

The static variable `_start_state` stores the initial snapshot when we start tracking changes:

<pre class="code">
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Current snapshot of database state.</span>
&nbsp; <span class="gray">///</span><span class="green"> You could also store the entire element state </span>
&nbsp; <span class="gray">///</span><span class="green"> strings here, not just their hash code, to</span>
&nbsp; <span class="gray">///</span><span class="green"> report their complete original and modified </span>
&nbsp; <span class="gray">///</span><span class="green"> values.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="teal">Dictionary</span>&lt;<span class="blue">int</span>, <span class="blue">string</span>&gt; _start_state = <span class="blue">null</span>;
</pre>

The mainline checks whether `_start_state` has been initialised.

If not, we create a snapshot to start tracking changes and report so to the user.

Otherwise, a new snapshot of the end state is created and the differences are reported:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Application</span> app = uiapp.Application;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; <span class="teal">IEnumerable</span>&lt;<span class="teal">Element</span>&gt; a = GetTrackedElements( doc );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == _start_state )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; _start_state = GetSnapshot( a );
&nbsp; &nbsp; &nbsp; <span class="teal">TaskDialog</span>.Show( <span class="maroon">&quot;Track Changes&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Started tracking changes now.&quot;</span> );
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Dictionary</span>&lt;<span class="blue">int</span>, <span class="blue">string</span>&gt; end_state = GetSnapshot( a );
&nbsp; &nbsp; &nbsp; ReportDifferences( doc, _start_state, end_state );
&nbsp; &nbsp; &nbsp; _start_state = <span class="blue">null</span>;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
&nbsp; }
</pre>


<!---
#### <a name="8"></a>Caveats

What is the family definition is modified?

No need to worry, it will change stuff around it.

You just have to know that it is not going to pick that up.
-->


#### <a name="6"></a>Sample Runs

Let's run the element modification tracker in a real project, e.g. the Revit sample rac_advanced_sample.rvt:

<center>
<img src="img/track_rac_advanced_sample.png" alt="Revit rac_advanced_sample BIM" width="641">
</center>

Zoom in to some simple wall that we can modify:

<center>
<img src="img/track_wall_original.png" alt="Original wall" width="615">
</center>

Move it a little bit:

<center>
<img src="img/track_wall_original.png" alt="Moved wall" width="624">
</center>

That generates the following report:

<pre>
5603 elements before, 5603 elements after, 5603 total
139803 modified Floor Floors &lt;139803 Hollow Core Plank - Concrete Topping&gt;
139829 modified Floor Floors &lt;139829 Hollow Core Plank - Concrete Topping&gt;
152882 modified Wall Walls &lt;152882 Interior - 138mm Partition (1-hr)&gt;
153000 modified Wall Walls &lt;153000 Interior - 138mm Partition (1-hr)&gt;
153435 modified Wall Walls &lt;153435 Interior - 138mm Partition (1-hr)&gt;
177329 modified Room Rooms &lt;177329 Corridor 107&gt;
0 deleted, 0 added, 6 modified, 5597 identical elements
</pre>

Obviously, as always, just moving one single wall a little bit affected a number of related objects, e.g., neighbouring walls, floors, and rooms.

Slightly more interesting test after adding a dialogue box to display the report:

Again, we'll just move one single wall; this one will affect more elements:

<center>
<img src="img/track_wall2_original.png" alt="Original wall" width="854">
</center>

Move this one down a bit:

<center>
<img src="img/track_wall2_moved.png" alt="Moved wall" width="852">
</center>

That generates the following report:

<pre>
5603 elements before, 5603 elements after, 5603 total
0 deleted, 0 added, 24 modified, 5579 identical elements:
139803 modified Floor Floors &lt;139803 Hollow Core Plank - Concrete Topping&gt;
139829 modified Floor Floors &lt;139829 Hollow Core Plank - Concrete Topping&gt;
152037 modified Wall Walls &lt;152037 Interior - 138mm Partition (1-hr)&gt;
152111 modified Wall Walls &lt;152111 Interior - 138mm Partition (1-hr)&gt;
152271 modified Wall Walls &lt;152271 Interior - 138mm Partition (1-hr)&gt;
152347 modified Wall Walls &lt;152347 Interior - 138mm Partition (1-hr)&gt;
152622 modified FamilyInstance Doors M_Single-Flush &lt;152622 0915 x 2134mm&gt;
152688 modified FamilyInstance Doors M_Single-Flush &lt;152688 0915 x 2134mm&gt;
152882 modified Wall Walls &lt;152882 Interior - 138mm Partition (1-hr)&gt;
153000 modified Wall Walls &lt;153000 Interior - 138mm Partition (1-hr)&gt;
153162 modified Wall Walls &lt;153162 Interior - 138mm Partition (1-hr)&gt;
153242 modified Wall Walls &lt;153242 Interior - 138mm Partition (1-hr)&gt;
156935 modified Opening Shaft Openings &lt;156935 Opening Cut&gt;
168671 modified Ceiling Ceilings &lt;168671 600 x 600mm Grid&gt;
168679 modified Ceiling Ceilings &lt;168679 600 x 600mm Grid&gt;
168687 modified Ceiling Ceilings &lt;168687 600 x 600mm Grid&gt;
168695 modified Ceiling Ceilings &lt;168695 600 x 600mm Grid&gt;
168894 modified Ceiling Ceilings &lt;168894 600 x 600mm Grid&gt;
168902 modified Ceiling Ceilings &lt;168902 600 x 600mm Grid&gt;
177324 modified Room Rooms &lt;177324 Electrical 112&gt;
177325 modified Room Rooms &lt;177325 Lounge 111&gt;
177326 modified Room Rooms &lt;177326 Men 110&gt;
177328 modified Room Rooms &lt;177328 Women 109&gt;
177329 modified Room Rooms &lt;177329 Corridor 107&gt;
</pre>

It is displayed in a task dialogue like this:

<center>
<img src="img/track_wall2_moved_report.png" alt="Tracked element report" width="504">
</center>

For real-world usage, you would obviously implement a more intelligent reporting system, for example a two-tiered one with a top-level summary displayed to the user and a detailed report stored in a log file or somewhere.

Once again: do not forget that the external command TrackChanges acts as a toggle: every second call creates and stores a snapshot, every second one creates a new snapshot, compares it with the stored one and reports the differences.


#### <a name="7"></a>Demo Recording

Here is a [two-and-a-half minute video recording](https://vimeo.com/152442481) showing it in action:

<center>
<iframe src="https://player.vimeo.com/video/152442481?title=0&byline=0" width="400" height="250" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
</center>


#### <a name="8"></a>Download

This project lives in the [TrackChanges GitHub repository](https://github.com/jeremytammik/TrackChanges) and
the version discussed above is [release 2016.0.0.1](https://github.com/jeremytammik/TrackChanges/releases/tag/2016.0.0.1).
