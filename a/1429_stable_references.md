<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 11727316 [Convert local family instance coordinate of selected Edge to project coordinates]
  http://forums.autodesk.com/t5/revit-api/convert-local-family-instance-coordinate-of-selected-edge-to/m-p/6282821

- building a custom reference for a FamilyInstance
  9 special references
  The API doesn't expose the control plane references but there is a fun hack I developed to get the embedded control plane references of a family instance. If you create a dimension in the UI which references a named control reference of a family instance, you can then study the dimension's references using the API to obtain the information required to build your own stable reference string and have the API convert it into a valid reference for you which can then be used for creation of dimensions and alignment constraints. I'll go look through my code an come back soon with a few code snippets for you.
  http://forums.autodesk.com/t5/revit-api/geometry-returns-no-reference-for-familyinstance/m-p/6025697

Reference Stable Representation Magic Voodoo #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim #python #dynamobim

Let's end the week with a truly magnificent contribution and research result provided by Scott Wilson in the Revit API discussion forum.
Scott responded to Pat Hague's recent thread on converting local family instance coordinate of a selected edge to project coordinates, saying,
Yeah the Stable Reference Strings can be used to get at areas of the Geometry API that aren't fully exposed &ndash; I love playing around with them. Sometimes I stumble upon something cool such as this solution for a situation in which the geometry returns no reference for a family instance...

-->

### Reference Stable Representation Magic Voodoo

Let's end the week with a truly magnificent contribution and research result provided by Scott Wilson in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160).

Scott responded to Pat Hague's recent thread
on [converting local family instance coordinate of a selected edge to project coordinates](http://forums.autodesk.com/t5/revit-api/convert-local-family-instance-coordinate-of-selected-edge-to/m-p/6282821),
saying:

> Yeah the Stable Reference Strings can be used to get at areas of the Geometry API that aren't fully exposed &ndash; I love playing around with them.
> Sometimes I stumble upon something cool such as this solution for a situation in which
> the [geometry returns no reference for a family instance](http://forums.autodesk.com/t5/revit-api/geometry-returns-no-reference-for-familyinstance/m-p/6025697).

I asked Scott whether he could summarise the gist of that discussion to publish here for easier reference and better legibility, and he very kindly obliged:

> No worries, glad to help.

> I made a post you can use for your blog if you like: [stable reference strings for Jeremy](http://forums.autodesk.com/t5/revit-api/for-jeremy-stable-reference-strings/m-p/6284505).

> Here's a summary of my investigations into stable reference strings created using `Reference.ConvertToStableRepresentation` and the Voodoo Magic that they can perform.

#### <a name="3"></a>Analysis and Background Information

A reference stable representation string is a collection of UniqueIds combined with table indices, delimited by the colon `:` character.

A reference string for a `PlanarFace` of a `FamilyInstance` element &ndash; accessed through its `GetSymbolGeometry` method &ndash; looks like this:

<pre>
  66a421da-90cb-4df2-8efe-c312e4f78aa0-0003d32b:0:INSTANCE:a2177cbb-03d8-4416-a4d8-8ada6fc0165a-0003ebf0:78:SURFACE
</pre>

The string obtained from the reference of the same face accessed using `GetInstanceGeometry` looks like this:

<pre>
  a2177cbb-03d8-4416-a4d8-8ada6fc0165a-0003ebf0:78:SURFACE
</pre>

As you can see, this is identical to the last 3 sections of the symbol geometry reference.

The SymbolGeometry reference looks like 2 references of 3 tokens each.

The structured data contained in the string can be used to reliably access geometry objects that are not completely exposed by the API.

This is done by building a custom string using parts of an existing valid string obtained from the API.

The fist step is to tokenise the string:

<pre class="code">
&nbsp; <span class="blue">string</span> refString = myReference
&nbsp; &nbsp; .ConvertToStableRepresentation( dbDoc );
&nbsp;
&nbsp; <span class="blue">string</span>[] refTokens = refString.Split(
&nbsp; &nbsp; <span class="blue">new</span> <span class="blue">char</span>[] { <span class="maroon">':'</span> } );
</pre>

This will give you an array of string tokens.

Here is my interpretation of each token's purpose &ndash; I could be wrong on some of them, as I've just inferred this information through experimentation, using symbol geometry string as an example:

- token0: 66a421da-90cb-4df2-8efe-c312e4f78aa0-0003d32b - UniqueId of the FamilyInstance Element.
- token1: 0 - Presumably an index into a collection within the FamilyInstance (I've not seen anything other than zero here so it could be that the collection always contains a single entry, or that other entries have some other special unknown use)
- token2: INSTANCE - signals that the referenced geometry object belongs to A FamilyInstance Element.
- token3: a2177cbb-03d8-4416-a4d8-8ada6fc0165a-0003ebf0 - UniqueId of the FamilySymbol Instance that describes the geometry of this FamilyInstance (multiple FamilyInstances that have identical Geometry will reference the same FamilySymbol Instance.
- token4: 78 - the index of the referenced geometry object within the FamilySymbol Instance's geometry table. index positions 0 through 8 are reserved for the special reference planes that have been specified in the family editor (explained later).
- token5: SURFACE - the type of geometry object referenced by this string.

I think the best way to try to follow the hierarchy is to read the tokens in reverse order as follows:

The referenced geometry object of type: SURFACE is at index: 78 of the FamilySymbol Instance with UniqueId: a2177cbb-03d8-4416-a4d8-8ada6fc0165a-0003ebf0 as represented by the FamilyInstance with UniqueId: 66a421da-90cb-4df2-8efe-c312e4f78aa0-0003d32b.

It bothers me that references from geometry objects accessed through `GetSymbolGeometry` specify the FamilyInstance while those from `GetInstanceGeometry` do not, yet the `GetInstanceGeometry` objects will be correctly transformed into project coordinates according to the FamilyInstance, where the `GetSymbolGeometry` objects are left in family coordinate space. This seems a little backwards to me.

Another strange behaviour is that the ElementId property of the reference from an object returned by `GetInstanceGeometry` does not even match the FamilyInstance it came from &ndash; it actually refers to the FamilySymbol instance instead.

Now for the Voodoo Magic that I promised:


#### <a name="4"></a>Voodoo Magic

If you have a reference in SymbolGeometry format, as obtained from a Dimension or through the `Selection.PickObject` method, and you need to get the geometry object in its correct position according to the FamilyInstance, you can tokenise the reference string as demonstrated above and then rebuild it as an InstanceGeometry reference string by discarding the first 3 tokens and joining the remaining 3 using, ':' as the delimiter.

You then obtain the InstanceGeometry from the element linked to the original reference and loop through it until you find an object that has a stable reference string that exactly matches your custom made one.

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="teal">Edge</span> GetInstanceEdgeFromSymbolRef(
&nbsp; &nbsp; <span class="teal">Reference</span> symbolRef,
&nbsp; &nbsp; <span class="teal">Document</span> dbDoc )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Edge</span> instEdge = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">Options</span> gOptions = <span class="blue">new</span> <span class="teal">Options</span>();
&nbsp; &nbsp; gOptions.ComputeReferences = <span class="blue">true</span>;
&nbsp; &nbsp; gOptions.DetailLevel = <span class="teal">ViewDetailLevel</span>.Undefined;
&nbsp; &nbsp; gOptions.IncludeNonVisibleObjects = <span class="blue">false</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">Element</span> elem = dbDoc.GetElement( symbolRef.ElementId );
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> stableRefSymbol = symbolRef
&nbsp; &nbsp; &nbsp; .ConvertToStableRepresentation( dbDoc );
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span>[] tokenList = stableRefSymbol.Split(
&nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="blue">char</span>[] { <span class="maroon">':'</span> } );
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> stableRefInst = tokenList[3] + <span class="maroon">&quot;:&quot;</span>
&nbsp; &nbsp; &nbsp; + tokenList[4] + <span class="maroon">&quot;:&quot;</span> + tokenList[5];
&nbsp;
&nbsp; &nbsp; <span class="teal">GeometryElement</span> geomElem = elem.get_Geometry(
&nbsp; &nbsp; &nbsp; gOptions );
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> geomElemObj <span class="blue">in</span> geomElem )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">GeometryInstance</span> geomInst = geomElemObj
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">GeometryInstance</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( geomInst != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> gInstGeom = geomInst
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .GetInstanceGeometry();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> gGeomObject
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">in</span> gInstGeom )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Solid</span> solid = gGeomObject <span class="blue">as</span> <span class="teal">Solid</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( solid != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Edge</span> edge <span class="blue">in</span> solid.Edges )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">string</span> stableRef = edge.Reference
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ConvertToStableRepresentation(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dbDoc );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( stableRef == stableRefInst )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; instEdge = edge;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( instEdge != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// already found, exit early</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( instEdge != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// already found, exit early</span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> instEdge;
&nbsp; }
</pre>

As I mentioned earlier, the first 9 positions are occupied by the special references that have been allocated in the family editor, with the following magic values:

- 0 = Left
- 1 = Center Left/Right
- 2 = Right
- 3 = Front
- 4 = Centre Front/Back
- 5 = Back
- 6 = Bottom
- 7 = Center Elevation
- 8 = Top

As you can see, this sequence matches the order they appear in the family editor reference type drop-down list.

These references are handy for creating Dimensions, but the API does not expose them fully. They exist in the element geometry but have the generic type of GeometryObject with no indication of their special purpose other than the index found in the reference string. To get one of these references, all you need to do is get any geometry object from the FamilyInstance using `GetSymbolGeometry` and use it's reference string as a template to build a custom reference that points to the special reference.

You just need to tokenise the reference string and replace `token4` with the index value from the list above for the reference you want.

`token5` should also be Replaced with `SURFACE`, but I've found that you can safely leave out the last token and the reference string will still be valid.

To simplify things, I created an enumeration for the indices and a static method that simplifies the process:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">enum</span> <span class="teal">SpecialReferenceType</span>
&nbsp; {
&nbsp; &nbsp; Left = 0,
&nbsp; &nbsp; CenterLR = 1,
&nbsp; &nbsp; Right = 2,
&nbsp; &nbsp; Front = 3,
&nbsp; &nbsp; CenterFB = 4,
&nbsp; &nbsp; Back = 5,
&nbsp; &nbsp; Bottom = 6,
&nbsp; &nbsp; CenterElevation = 7,
&nbsp; &nbsp; Top = 8
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="teal">Reference</span> GetSpecialFamilyReference(
&nbsp; &nbsp; <span class="teal">FamilyInstance</span> inst,
&nbsp; &nbsp; <span class="teal">SpecialReferenceType</span> refType )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Reference</span> indexRef = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">int</span> idx = (<span class="blue">int</span>) refType;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( inst != <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Document</span> dbDoc = inst.Document;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Options</span> geomOptions = dbDoc.Application.Create
&nbsp; &nbsp; &nbsp; &nbsp; .NewGeometryOptions();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( geomOptions != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; geomOptions.ComputeReferences = <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; geomOptions.DetailLevel = <span class="teal">ViewDetailLevel</span>.Undefined;
&nbsp; &nbsp; &nbsp; &nbsp; geomOptions.IncludeNonVisibleObjects = <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> gElement = inst.get_Geometry(
&nbsp; &nbsp; &nbsp; &nbsp; geomOptions );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">GeometryInstance</span> gInst = gElement.First()
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">GeometryInstance</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">String</span> sampleStableRef = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( gInst != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> gSymbol = gInst
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .GetSymbolGeometry();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( gSymbol != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> geomObj <span class="blue">in</span> gSymbol )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( geomObj <span class="blue">is</span> <span class="teal">Solid</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Solid</span> solid = geomObj <span class="blue">as</span> <span class="teal">Solid</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( solid.Faces.Size &gt; 0 )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Face</span> face = solid.Faces.get_Item( 0 );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; sampleStableRef = face.Reference
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ConvertToStableRepresentation(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dbDoc );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">else</span> <span class="blue">if</span>( geomObj <span class="blue">is</span> <span class="teal">Curve</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Curve</span> curve = geomObj <span class="blue">as</span> <span class="teal">Curve</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; sampleStableRef = curve.Reference
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ConvertToStableRepresentation( dbDoc );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">else</span> <span class="blue">if</span>( geomObj <span class="blue">is</span> <span class="teal">Point</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Point</span> point = geomObj <span class="blue">as</span> <span class="teal">Point</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; sampleStableRef = point.Reference
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ConvertToStableRepresentation( dbDoc );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">break</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( sampleStableRef != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">String</span>[] refTokens = sampleStableRef.Split(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="blue">char</span>[] { <span class="maroon">':'</span> } );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">String</span> customStableRef = refTokens[0] + <span class="maroon">&quot;:&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + refTokens[1] + <span class="maroon">&quot;:&quot;</span> + refTokens[2] + <span class="maroon">&quot;:&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + refTokens[3] + <span class="maroon">&quot;:&quot;</span> + idx.ToString();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; indexRef = <span class="teal">Reference</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ParseFromStableRepresentation(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dbDoc, customStableRef );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">GeometryObject</span> geoObj = inst
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .GetGeometryObjectFromReference(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; indexRef );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( geoObj != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">String</span> finalToken = <span class="maroon">&quot;&quot;</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( geoObj <span class="blue">is</span> <span class="teal">Edge</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; finalToken = <span class="maroon">&quot;:LINEAR&quot;</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( geoObj <span class="blue">is</span> <span class="teal">Face</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; finalToken = <span class="maroon">&quot;:SURFACE&quot;</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; customStableRef += finalToken;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; indexRef = <span class="teal">Reference</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ParseFromStableRepresentation(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dbDoc, customStableRef );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; indexRef = <span class="blue">null</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">Exception</span>( <span class="maroon">&quot;No Symbol Geometry found...&quot;</span> );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> indexRef;
&nbsp; }
</pre>

There's a little bit of unnecessary code in that method, as it was quickly cobbled together from the code I was using for research, sorry.

That's all I have for now.

Hopefully someone finds it useful.

**Response:** Wow!

I am sure we will!

A number of people commented on Scott's original post and were extremely impressed, overawed even, by this analysis and truly magical voodoo.

So am I!

Many thanks to Scott for all his research, creating, sharing, documenting this, many other solutions, all the support and profound insights he provides so selflessly in the discussion forum!

P.S. I added Scott's sample code
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2016.0.127.3](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2016.0.127.3) in the
module [CmdDimensionInstanceOrigin.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdDimensionInstanceOrigin.cs#L27-L251).

#### <a name="5"></a>Importance of Setting the Detail Level

An addendum by Pat Hague to Scott's original post:

I would like to stress the importance of setting

<pre class="code">
  gOptions.DetailLevel = <span class="teal">ViewDetailLevel</span>.Undefined;
</pre>

I just spent a solid day trying to figure out why your code would work on some families, and not others.

Finally, I was able to determine that the family itself was controlling visibility of faces based on the view detail level.

Strangely enough, when I was blindly troubleshooting the code I tried setting the following to true:

<pre class="code">
  gOptions.IncludeNonVisibleObjects = <span class="blue">true</span>; // false
</pre>

This still did not work.
Maybe I had something else wrong with my code at the time.
I don't know.

What I've been able to get to work every time (so far) is the following:

<pre class="code">
  gOptions.DetailLevel = doc.ActiveView.DetailLevel;
</pre>

To me, this ensures that I can't select a face that isn't visible through a PickObject.

Thanks again for all of your voodoo!

**Response by Scott:** I'm a bit confused pat; are you saying that using `Undefined` is good or bad?

**Answer:** Lets do some testing... for instance, this structural column, I think from a stock Autodesk family, shows straight edges in `Medium` or `Coarse` views:

<center>
<img src="img/stable_reference_mc.png" alt="Medium and Coarse" width="400">
</center>

It shows fillet edges on `Fine`:

<center>
<img src="img/stable_reference_fine.png" alt="Fine" width="400">
</center>

If I try to get the instance face or edge of the above-mentioned structural column from the symbol reference (using your code), I get a NullReferenceException error if DetailLevel is set to Undefined:

<pre class="code">
  gOptions.DetailLevel = <span class="teal">ViewDetailLevel</span>.Undefined;
</pre>

Presumably because the PickObject is allowing me to select an edge or face that is non visible &ndash; I'm not sure how to confirm this.

The same is also true if I purposely mismatch my View Detail Level and my Options.Detail Level. For example:

- Set View Detail Level to `Fine`
- Set Options.DetailLevel to `Medium`

If I try that same code on a family instance where visibility of edges or faces is not controlled by the view's detail level, then everything works out fine.

Looking up this property in the API help file I see the following:

> Type: Autodesk.Revit.DB.ViewDetailLevel: Value of the detail level. ViewDetailLevel.Undefined means no override is set.

My guess is that there is some sort of disconnect between what PickObject is allowing you to select and the Options class. I should also note that I tested this again to confirm that setting `IncludeNonVisibleObjects` to `true` or `false` made no difference in my test case.

To summarize, if I always make sure my `Options.DetailLevel` is set to that of my view detail level, then I shouldn't run into any problems (I hope).

**Response:** The Undefined setting was an attempt to make the code more generalised, It worked fine on a few cases I tested it with before posting it, but i might have just got lucky (or unlucky...). I thought that having `ViewDetailLevel` set to `Undefined` would provide the full set of geometry from which to find the matching face no-matter what the setting of the view where the selection was made. Maybe that's not how `ViewDetailLevel.Undefined` works. If it is as you say and it signifies to only return geometry that does not have the detail visibility override set, then to fix it, I would loop through on each `ViewDetailLevel` setting until the geometry was found. Thanks for testing it out and giving a heads-up. Btw, which documentation mentions "ViewDetailLevel.Undefined means no override is set"? I just checked and the docs say "View does not use Detail Level" which I think means something completly different to both mine and your interpretations; odd.

Out of interest have you changed the code to include the view in the geometry options?

If so, does removing it help?

**Answer:** Scott, I may have been looking in the wrong location on the api help documents.

I found that snippet of text under OverrideGraphicSettings.SetDetailLevel Method.

I am currently using the active view's detail level in my geometry options. For my case its the only reliable way I can ensure that the user doesn't get an exception error.

<pre class="code">
  gOptions.DetailLevel = doc.ActiveView.DetailLevel;
</pre>

#### <a name="6"></a>Reformat Stable Representation String for Dimensioning

Joshua Lumley added an important note on using the stable references to create dimensioning in
his [comment below](http://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html#comment-4068022721):

When creating dimensions based on objects from linked files, you may see an error message saying *Invalid number of references*.

To resolve that, all you do is rearrange the string a little to match the following format:

<pre>
  fb332d47-8286-4829-bd40-46c26de8ebac-000258d5:0:RVTLINK:2796184:1:SURFACE/5"
</pre>

You do NOT want this:

<pre>
  fb332d47-8286-4829-bd40-46c26de8ebac-000258d5:RVTLINK/fb332d47-8286-4829-bd40-46c26de8ebac-000258d4:2796184:1:SURFACE/5"
</pre>

Many thanks to Joshua for pointing it out!

#### <a name="7"></a>Handle Element Category Override

Mustafa Khalil invested some research in this and added
another [comment below](https://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html#comment-4710182554),
explaining:

I played with this for a while and was impressed with this detailed study.
For some reason, however, it sometimes did not work for me.

While cracking on every angle to find out why, I realized I should consider how the element is actually displayed in the current view.
Meaning, I should not only rely on the `ViewDetail` of the current view, but I should also check if there is any ViewDetail category overridden as well.
If there is no override to an element category, it will report `ViewDetailLevel.Undefined`.
Then, it is trustworthy to select the ViewDetail of the current view.

So, I considered the following resolution:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;view&nbsp;=&nbsp;doc.ActiveView;
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;overriddenCatView&nbsp;=&nbsp;view.GetCategoryOverrides(
&nbsp;&nbsp;&nbsp;&nbsp;elem.Category.Id&nbsp;);
 
&nbsp;&nbsp;op&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;DetailLevel&nbsp;=&nbsp;overriddenCatView.DetailLevel&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==&nbsp;<span style="color:#2b91af;">ViewDetailLevel</span>.Undefined
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;view.DetailLevel
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;overriddenCatView.DetailLevel,
&nbsp;&nbsp;&nbsp;&nbsp;IncludeNonVisibleObjects&nbsp;=&nbsp;<span style="color:blue;">true</span>
&nbsp;&nbsp;};
</pre>

Instead of this:
 
<pre class="code">
&nbsp;&nbsp;op&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;DetailLevel&nbsp;=&nbsp;m_doc.ActiveView.DetailLevel,
&nbsp;&nbsp;&nbsp;&nbsp;IncludeNonVisibleObjects&nbsp;=&nbsp;<span style="color:blue;">true</span>
&nbsp;&nbsp;};
</pre>

Many thanks to Mustafa for his research and explanation!
