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

The creation of a DirectShape element on an interactively selected existing element face requires a transformation to be applied to the face of a family instance returned by the PickObject method to convert it from the family symbol space to the family instance real world coordinates
&ndash; Creating DirectShape from face mesh
&ndash; Determining real world transform of a family instance face returned by PickObject
&ndash; Iterating over element geometry to find a specific target geometry object
&ndash; Reusing sketch planes for model curve creation...

-->

### DirectShape From Face and Sketch Plane Reuse

Frode Tørresdal of [Norconsult Informasjonssystemer AS](https://www.nois.no) just raised an interesting issue regarding the creation of a DirectShape element on an interactively selected existing element face.

As it turned out, his specific issue was not related to the DirectShape creation at all, but rather to the transformation that needs to be applied to the face of a family instance returned by the PickObject method to convert it from the family symbol space to the family instance real world coordinates.

Here is our discussion of the problem and the evolution of my
[DirectShapeFromFace](https://github.com/jeremytammik/DirectShapeFromFace) solution that I implemented to address this.

It ends up demonstrating several interesting aspects:

- [Creating a DirectShape element from a face mesh](#2)
- [Determining the real world transform of a family instance face returned by PickObject](#3)
- [Iterating over element geometry to find a specific target geometry object](#4)
- [Reusing sketch planes for model curve creation](#5)
- [Complete solution](#6)
- [Download](#7)


#### <a name="2"></a>Creating a DirectShape Element from a Face Mesh

**Question:**
I have some issues when creating DirectShape elements in Revit. I attached a sample project and a Revit model:

<center>
<img src="img/faces_for_directshape.png" alt="Faces for DirectShape" width="376"/>
</center>

The sample project creates a DirectShape from a selected face.

Here is the code:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">void</span> Execute1(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Transaction</span> trans = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = commandData.Application
&nbsp; &nbsp; &nbsp; .ActiveUIDocument;
&nbsp;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Selection</span> choices = uidoc.Selection;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Reference</span> reference = choices.PickObject(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ObjectType</span>.Face );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Element</span> el = doc.GetElement(
&nbsp; &nbsp; &nbsp; &nbsp; reference.ElementId );
&nbsp;
&nbsp; &nbsp; &nbsp; trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc, <span class="maroon">&quot;Create elements&quot;</span> );
&nbsp; &nbsp; &nbsp; trans.Start();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">TessellatedShapeBuilder</span> builder
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">TessellatedShapeBuilder</span>();
&nbsp;
&nbsp; &nbsp; &nbsp; builder.OpenConnectedFaceSet( <span class="blue">false</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Face</span> face = el.GetGeometryObjectFromReference(
&nbsp; &nbsp; &nbsp; &nbsp; reference ) <span class="blue">as</span> <span class="teal">Face</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Mesh</span> mesh = face.Triangulate();
&nbsp; &nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt; args = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">XYZ</span>&gt;( 3 );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> offset = <span class="blue">new</span> <span class="teal">XYZ</span>();
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( el.Location <span class="blue">is</span> <span class="teal">LocationPoint</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">LocationPoint</span> locationPoint = el.Location
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">LocationPoint</span>;
&nbsp; &nbsp; &nbsp; &nbsp; offset = locationPoint.Point;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">for</span>( <span class="blue">int</span> i = 0; i &lt; mesh.NumTriangles; i++ )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">MeshTriangle</span> triangle = mesh.get_Triangle(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; i );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> p1 = triangle.get_Vertex( 0 );
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> p2 = triangle.get_Vertex( 1 );
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> p3 = triangle.get_Vertex( 2 );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; p1 = p1.Add( offset );
&nbsp; &nbsp; &nbsp; &nbsp; p2 = p2.Add( offset );
&nbsp; &nbsp; &nbsp; &nbsp; p3 = p3.Add( offset );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; args.Clear();
&nbsp; &nbsp; &nbsp; &nbsp; args.Add( p1 );
&nbsp; &nbsp; &nbsp; &nbsp; args.Add( p2 );
&nbsp; &nbsp; &nbsp; &nbsp; args.Add( p3 );
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">TessellatedFace</span> tesseFace
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">TessellatedFace</span>( args,
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
&nbsp; &nbsp; &nbsp; &nbsp; .GUID.ToString(), <span class="teal">Guid</span>.NewGuid().ToString() );
&nbsp;
&nbsp; &nbsp; &nbsp; ds.SetShape( result.GetGeometricalObjects() );
&nbsp;
&nbsp; &nbsp; &nbsp; ds.Name = <span class="maroon">&quot;MyShape&quot;</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; trans.Commit();
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">catch</span>( <span class="teal">Exception</span> ex )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( trans != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; &nbsp; trans.RollBack();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print( ex.Message );
&nbsp; &nbsp; }
&nbsp; }
</pre>

The code works fine on the columns. I managed this by calculating an offset from the LocationPoint. But this unfortunately affects the big Generic Model. This Generic Model element has been moved and when I run the code on this object, the DirectShape is created in the wrong location. If I remove the lines that get the offset from the location point this works fine on the generic model, but not on the columns. How do I know when to use the offset? Is there a way to write code that works on both cases?

Also in this model there are two columns. The code works fine on one of them, but not the other. Why is that?

I really hope you can help me with this!


#### <a name="3"></a>Determining the Real World Transform of a Family Instance Face returned by PickObject

**Answer:**
Thank you for your interesting query and sample material.

I compiled the add-in and can reproduce the behaviour you describe.

It looks to me as if you are not taking the translation of the selected element properly into account.

Just like you, I would have expected the GetGeometryObjectFromReference method to do that automatically for me.

Maybe it will work better and work for all types of elements if you use a different approach to retrieve the geometry.

For family instances, there is a difference between symbol geometry and the translated family instance geometry.

I have made good experiences using the GeometryElement.GetTransformed method and passing in an identity transform to retrieve element geometry in its real world location.

You can possibly use the geometry reference returned by the pick operation to select the same face from the element geometry with the option ComputeReferences turned on.

Another, more pertinent question:

Are you sure this is an issue with the DirectShape?

I would have thought that it is more an issue of the geometry retrieval, and has nothing to do with the DirectShape creation.

Therefore, I would suggest the following test of the intermediate results:

1. Retrieve the three MeshTriangle vertices.
2. Create a plane containing all three, and a sketch plane. For efficiency, we can cache and reuse already existing sketch planes.
3. Draw three model lines representing the mesh triangle.

Then you can see exactly what geometry is being returned.

I cleaned up your sample a little bit to test that myself.

I created the [DirectShapeFromFace GitHub repository](https://github.com/jeremytammik/DirectShapeFromFace) for it to keep track of my modifications.

You might want to take a look at that in its current state, and maybe synchronise your sample with mine.

I have not finished yet, though, and am still working on it.

I am sure we will find a perfect resolution for this.

**Update:**
I updated my sample code to create model lines displaying the original triangles obtained from the element geometry via the GetGeometryObjectFromReference method.

Look at the new [release 2016.0.0.1](https://github.com/jeremytammik/DirectShapeFromFace/releases/tag/2016.0.0.1).

I can confirm that the mesh triangles are located in a different place than the original element.

There is certainly a really simple solution to this.

In any case, the problem has nothing to do with the DirectShape creation, just with the geometry retrieval.

I have seen and handled similar issues in the past, when traversing element geometry for various export processes.

In those cases, I was using geometry retrieved from the `Element.get_Geometry` property instead of the `GetGeometryObjectFromReference` method.

I therefore know how this issue can be handled.

I would still like to learn what the optimal, simplest and most efficient approach really is.

The Revit API often moves in mysterious ways...

**Question simplified:**
The following geometry retrieval returns a face in a different project location that the original selected element:

<pre class="code">
&nbsp; <span class="teal">Selection</span> choices = uidoc.Selection;
&nbsp;
&nbsp; <span class="teal">Reference</span> reference = choices.PickObject(
&nbsp; &nbsp; <span class="teal">ObjectType</span>.Face );
&nbsp;
&nbsp; <span class="teal">Element</span> el = doc.GetElement(
&nbsp; &nbsp; reference.ElementId );
&nbsp;
&nbsp; <span class="teal">Face</span> face = el.GetGeometryObjectFromReference(
&nbsp; &nbsp; reference ) <span class="blue">as</span> <span class="teal">Face</span>;
</pre>

What is the proper and efficient way to obtain the face in the same location as the original selected element?

The face obtained as shown above is in an unexpected location, often far away from the selected element.

Apparently, the problem is that `PickObject` returns a reference to a face, and that face may be in the symbol geometry, not the instance geometry.

How can I find the correct transformation to the instance geometry location?

I tried applying `FamilyInstance.GetTransform` to it, to no avail.

I also tried iterating through all the (possibly nested) element geometry instances to calculate the appropriate transform, but I cannot find any way to identify the face returned by PickObject.

Both the equality operator `==` and a comparison using the `Face.Reference` property always return false for all the faces that I find.

Very mystifying.

**Answer:**
It is possible that some families (and their representative geometry) are nested several levels deep, and you need all the transforms.

You should be able to compare references by the strings returned by the `ConvertToStableRepresentation` method.

It would be a nice enhancement to make `Reference.Equals` work in this manner.  Unfortunately, it does not currently do so.

**Response:**
That really sounds quite bad.

`FamilyInstance.GetTransform` works for some family instances and not for others.

I would love that to work, then the problem would be almost resolved.

Otherwise I have to resort to determining the transform myself.

When a user calls `PickObject( ObjectType.Face )`, nothing is known except the resulting element id and reference to the face.

Are you really telling me that at this point I have to:

1. Check whether the element happens to be a family instance.
2. If so, determine the selected face's ConvertToStableRepresentation string, iterate through all the geometry, possibly through several levels of nested family instances, keep track of all the transforms, find and identify the picked face by checking and comparing the ConvertToStableRepresentation string, use the result of that to decide at which point I need to stop traversing the geometry, exit the traversal when the target is found, put together the list of nested transforms in the proper manner, and finally apply the resulting total transform to the selected face?

I think that should be packaged and provided by the API.

Could you provide example code that implements this in the correct manner?

I have tried to achieve this and not succeeded so far, e.g., like this:

<pre class="code">
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
<span class="gray">///</span><span class="green"> Determine the stack of transforms to apply to</span>
<span class="gray">///</span><span class="green"> the given target geometry object to bring it</span>
<span class="gray">///</span><span class="green"> to the proper location in the project coordinates.</span>
<span class="gray">///</span><span class="green"> Unfortunetely, we have not found any way at all</span>
<span class="gray">///</span><span class="green"> yet to identify the target object we are after.</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
<span class="blue">static</span> <span class="blue">bool</span> GetTransformStackForObject(
&nbsp; <span class="teal">Stack</span>&lt;<span class="teal">Transform</span>&gt; tstack,
&nbsp; <span class="teal">GeometryElement</span> geo,
&nbsp; <span class="teal">GeometryObject</span> targetObj,
&nbsp; <span class="teal">Reference</span> targetRef )
{
&nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> obj <span class="blue">in</span> geo )
&nbsp; {
&nbsp; &nbsp; <span class="blue">if</span>( obj == targetObj )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">GeometryInstance</span> gi = obj <span class="blue">as</span> <span class="teal">GeometryInstance</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != gi )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; tstack.Push( gi.Transform );
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> GetTransformStackForObject( tstack,
&nbsp; &nbsp; &nbsp; &nbsp; gi.GetInstanceGeometry(), targetObj, targetRef );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">Solid</span> solid = obj <span class="blue">as</span> <span class="teal">Solid</span>;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != solid )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( 0 &lt; solid.Faces.Size )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Face</span> face <span class="blue">in</span> solid.Faces )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( face == targetObj )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( face.Reference == targetRef )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( 0 &lt; solid.Edges.Size )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Edge</span> edge <span class="blue">in</span> solid.Edges )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( edge == targetObj )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( edge.Reference == targetRef )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">return</span> <span class="blue">false</span>;
}
</pre>

This code never found the target.

Maybe it will if I use `ConvertToStableRepresentation`.

Still, I would appreciate further input and confirmation before I continue in these struggles.

I think 'PickObject( ObjectType.Face )' is pretty hard to use out of the box if it requires all these additional calculations...


**Response 2:**
After further testing, I still cannot identify the picked face within the element geometry in the manner suggested.

Here are the relevant code snippets:

<pre class="code">
&nbsp; <span class="teal">Selection</span> choices = uidoc.Selection;
&nbsp;
&nbsp; <span class="teal">Reference</span> faceref = choices.PickObject(
&nbsp; &nbsp; <span class="teal">ObjectType</span>.Face );
&nbsp;
&nbsp; <span class="blue">string</span> rep = faceref
&nbsp; &nbsp; .ConvertToStableRepresentation( doc );
&nbsp;
&nbsp; <span class="teal">Debug</span>.Print( <span class="maroon">&quot;Face reference picked: &quot;</span>
&nbsp; &nbsp; + rep );
&nbsp;
&nbsp; <span class="teal">Element</span> el = doc.GetElement(
&nbsp; &nbsp; faceref.ElementId );

...

&nbsp; <span class="teal">Transform</span> t = <span class="blue">null</span>;
&nbsp;
&nbsp; <span class="teal">Options</span> opt = <span class="blue">new</span> <span class="teal">Options</span>();
&nbsp; opt.ComputeReferences = <span class="blue">true</span>;
&nbsp; <span class="teal">GeometryElement</span> geo = el.get_Geometry( opt );
&nbsp; <span class="teal">Stack</span>&lt;<span class="teal">Transform</span>&gt; tstack = <span class="blue">new</span> <span class="teal">Stack</span>&lt;<span class="teal">Transform</span>&gt;();
&nbsp;
&nbsp; <span class="blue">if</span>( GetTransformStackForObject( tstack, geo, doc, rep )
&nbsp; &nbsp; &amp;&amp; 0 &lt; tstack.Count )
&nbsp; {
&nbsp; &nbsp; t = <span class="teal">Transform</span>.Identity;
&nbsp;
&nbsp; &nbsp; <span class="blue">while</span>( 0 &lt; tstack.Count )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; t *= tstack.Pop();
&nbsp; &nbsp; }
&nbsp; }

...

<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
<span class="gray">///</span><span class="green"> Determine the stack of transforms to apply to</span>
<span class="gray">///</span><span class="green"> the given target geometry object to bring it</span>
<span class="gray">///</span><span class="green"> to the proper location in the project coordinates.</span>
<span class="gray">///</span><span class="green"> Unfortunetely, we have not found any way at all</span>
<span class="gray">///</span><span class="green"> yet to identify the target object we are after.</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
<span class="blue">static</span> <span class="blue">bool</span> GetTransformStackForObject(
&nbsp; <span class="teal">Stack</span>&lt;<span class="teal">Transform</span>&gt; tstack,
&nbsp; <span class="teal">GeometryElement</span> geo,
&nbsp; <span class="teal">Document</span> doc,
&nbsp; <span class="blue">string</span> stable_representation )
{
&nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> obj <span class="blue">in</span> geo )
&nbsp; {
&nbsp; &nbsp; <span class="teal">GeometryInstance</span> gi = obj <span class="blue">as</span> <span class="teal">GeometryInstance</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != gi )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; tstack.Push( gi.Transform );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> GetTransformStackForObject( tstack,
&nbsp; &nbsp; &nbsp; &nbsp; gi.GetInstanceGeometry(), doc,
&nbsp; &nbsp; &nbsp; &nbsp; stable_representation );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">Solid</span> solid = obj <span class="blue">as</span> <span class="teal">Solid</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != solid )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">string</span> rep;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( 0 &lt; solid.Faces.Size )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Face</span> face <span class="blue">in</span> solid.Faces )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; rep = face.Reference
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ConvertToStableRepresentation( doc );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( rep.Equals( stable_representation ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( 0 &lt; solid.Edges.Size )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Edge</span> edge <span class="blue">in</span> solid.Edges )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; rep = edge.Reference
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ConvertToStableRepresentation( doc );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( rep.Equals( stable_representation ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="blue">return</span> <span class="blue">false</span>;
}
</pre>

I tested this with a structural concrete rectangular column.

All the faces were visited, and none of them returned a `ConvertToStableRepresentation` string that matched the picked face's one.


#### <a name="4"></a>Iterating over Element Geometry to Find a Specific Target Geometry Object

**Answer:**
Short answer:

`GetInstanceGeometry` is incorrect in extracting usable references &ndash; you must call `GetSymbolGeometry` with no arguments instead.

This should result in comparable stable references.

More details of instance transforms etc. are available in Scott Conover's AU courses and have been rolled into material that is in the Developer’s Guide in the Revit online help.

**Response:**
Thank you!

That helped.

It seems to be working now, in [DirectShapeFromFace release 2016.0.0.5](https://github.com/jeremytammik/DirectShapeFromFace/releases/tag/2016.0.0.5):

<center>
<img src="img/direct_shape_from_face.png" alt="DirectShape from face" width="320"/>
</center>

I am surprised it is so hard.

The sequence of prior attempts and tests is described by the preceding [release messages](https://github.com/jeremytammik/DirectShapeFromFace/releases).

It still needs to be tested with more samples, especially with faces nested several levels deep within nested families.


#### <a name="5"></a>Reusing Sketch Planes for Model Curve Creation

One final cool implementation details to note:

For testing purposes, I create model lines representing the original face mesh triangles as well the final direct shape.

The model lines require a sketch plane to host them.

To avoid recreating hundreds and thousands of identical sketch planes for this purpose, I try to reuse the existing ones as much as possible.

In tried to limit the reuse to my own sketch planes and mark them by specifying their element name, but that does not work.

The always end up named "&lt;not associated&gt;".

So I modified my reusage algorithm to reuse only such sketch planes, and it seems to work fine, cf. the `SketchPlaneMatches` and `GetSketchPlane` methods below.


#### <a name="6"></a>Complete Solution

To wrap up, here is the complete code implementing this:

<pre class="code">
<span class="blue">class</span> <span class="teal">CreateDirectShape</span>
{
&nbsp; <span class="blue">const</span> <span class="blue">string</span> _sketch_plane_name_prefix
&nbsp; &nbsp; = <span class="maroon">&quot;The Building Coder&quot;</span>;
&nbsp;
&nbsp; <span class="blue">const</span> <span class="blue">string</span> _sketch_plane_name_prefix2
&nbsp; &nbsp; = <span class="maroon">&quot;&lt;not associated&gt;&quot;</span>;
&nbsp;
<span class="blue">&nbsp; #region</span> Geometrical Comparison
&nbsp; <span class="blue">const</span> <span class="blue">double</span> _eps = 1.0e-9;
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">bool</span> IsAlmostZero(
&nbsp; &nbsp; <span class="blue">double</span> a,
&nbsp; &nbsp; <span class="blue">double</span> tolerance )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> tolerance &gt; <span class="teal">Math</span>.Abs( a );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">bool</span> IsAlmostZero( <span class="blue">double</span> a )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> IsAlmostZero( a, _eps );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">bool</span> IsAlmostEqual( <span class="blue">double</span> a, <span class="blue">double</span> b )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> IsAlmostZero( b - a );
&nbsp; }
<span class="blue">&nbsp; #endregion</span> <span class="green">// Geometrical Comparison</span>
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return the normal of a plane&nbsp; </span>
&nbsp; <span class="gray">///</span><span class="green"> spanned by the two given vectors.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="teal">XYZ</span> GetNormal( <span class="teal">XYZ</span> v1, <span class="teal">XYZ</span> v2 )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> v1
&nbsp; &nbsp; &nbsp; .CrossProduct( v2 )
&nbsp; &nbsp; &nbsp; .Normalize();
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return the normal of a plane spanned by the</span>
&nbsp; <span class="gray">///</span><span class="green"> three given triangle corner points.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="teal">XYZ</span> GetNormal( <span class="teal">XYZ</span>[] triangleCorners )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> GetNormal(
&nbsp; &nbsp; &nbsp; triangleCorners[1] - triangleCorners[0],
&nbsp; &nbsp; &nbsp; triangleCorners[2] - triangleCorners[0] );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return signed distance from plane to a given point.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">double</span> SignedDistanceTo(
&nbsp; &nbsp; <span class="teal">Plane</span> plane,
&nbsp; &nbsp; <span class="teal">XYZ</span> p )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Debug</span>.Assert(
&nbsp; &nbsp; &nbsp; IsAlmostEqual( plane.Normal.GetLength(), 1 ),
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected normalised plane normal&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="teal">XYZ</span> v = p - plane.Origin;
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> plane.Normal.DotProduct( v );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return true if the sketch plane belongs to us</span>
&nbsp; <span class="gray">///</span><span class="green"> and its origin and normal vector match the </span>
&nbsp; <span class="gray">///</span><span class="green"> given targets.</span>
&nbsp; <span class="gray">///</span><span class="green"> Nope, we are unable to set the sketch plane </span>
&nbsp; <span class="gray">///</span><span class="green"> name. However, Revit throws an exception if </span>
&nbsp; <span class="gray">///</span><span class="green"> we try to draw on the skatch plane named</span>
&nbsp; <span class="gray">///</span><span class="green"> 'Level 1', so lets ensure we use '</span><span class="gray">&lt;not </span>
&nbsp; <span class="gray">/// associated&gt;</span><span class="green">'.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="blue">bool</span> SketchPlaneMatches(
&nbsp; &nbsp; <span class="teal">SketchPlane</span> sketchPlane,
&nbsp; &nbsp; <span class="teal">XYZ</span> origin,
&nbsp; &nbsp; <span class="teal">XYZ</span> normal )
&nbsp; {
&nbsp; &nbsp; <span class="green">//bool rc = sketchPlane.Name.StartsWith(</span>
&nbsp; &nbsp; <span class="green">//&nbsp; _sketch_plane_name_prefix );</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">bool</span> rc = sketchPlane.Name.Equals(
&nbsp; &nbsp; &nbsp; _sketch_plane_name_prefix2 );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( rc )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Plane</span> plane = sketchPlane.GetPlane();
&nbsp;
&nbsp; &nbsp; &nbsp; rc = plane.Normal.IsAlmostEqualTo( normal )
&nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; IsAlmostZero( SignedDistanceTo(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; plane, origin ) );
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> rc;
&nbsp; }
&nbsp;
&nbsp; <span class="blue">static</span> <span class="blue">int</span> _sketch_plane_creation_counter = 0;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return a sketch plane through the given origin</span>
&nbsp; <span class="gray">///</span><span class="green"> point with the given normal, either by creating</span>
&nbsp; <span class="gray">///</span><span class="green"> a new one or reusing an existing one.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="teal">SketchPlane</span> GetSketchPlane(
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="teal">XYZ</span> origin,
&nbsp; &nbsp; <span class="teal">XYZ</span> normal )
&nbsp; {
&nbsp; &nbsp; <span class="blue">string</span> s = <span class="maroon">&quot;reusing&quot;</span>;
&nbsp;
&nbsp; &nbsp; <span class="green">// If we could reliably set the sketch plane Name</span>
&nbsp; &nbsp; <span class="green">// property or find some other relaible marker </span>
&nbsp; &nbsp; <span class="green">// that is reflected in a parameter, we could </span>
&nbsp; &nbsp; <span class="green">// replace the sketchPlane.Name.Equals check in</span>
&nbsp; &nbsp; <span class="green">// SketchPlaneMatches by a parameter filter in</span>
&nbsp; &nbsp; <span class="green">// the filtered element collector framework</span>
&nbsp; &nbsp; <span class="green">// to move the test into native Revit code </span>
&nbsp; &nbsp; <span class="green">// instead of post-processing in .NET, which</span>
&nbsp; &nbsp; <span class="green">// would give a 50% performance enhancement.</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">SketchPlane</span> sketchPlane
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">SketchPlane</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; .Cast&lt;<span class="teal">SketchPlane</span>&gt;()
&nbsp; &nbsp; &nbsp; &nbsp; .FirstOrDefault&lt;<span class="teal">SketchPlane</span>&gt;( x =&gt;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; SketchPlaneMatches( x, origin, normal ) );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> == sketchPlane )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Plane</span> plane = <span class="blue">new</span> <span class="teal">Plane</span>( normal, origin );
&nbsp;
&nbsp; &nbsp; &nbsp; sketchPlane = <span class="teal">SketchPlane</span>.Create( doc, plane );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="green">//sketchPlane.Name = string.Format(</span>
&nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; &quot;{0} {1}&quot;, _sketch_plane_name_prefix,</span>
&nbsp; &nbsp; &nbsp; <span class="green">//&nbsp; _sketch_plane_creation_counter++ );</span>
&nbsp;
&nbsp; &nbsp; &nbsp; ++_sketch_plane_creation_counter;
&nbsp;
&nbsp; &nbsp; &nbsp; s = <span class="maroon">&quot;created&quot;</span>;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="teal">Debug</span>.Print( <span class="maroon">&quot;GetSketchPlane: {0} '{1}' ({2})&quot;</span>,
&nbsp; &nbsp; &nbsp; s, sketchPlane.Name,
&nbsp; &nbsp; &nbsp; _sketch_plane_creation_counter );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> sketchPlane;
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Create model lines representing a closed </span>
&nbsp; <span class="gray">///</span><span class="green"> planar loop in the given sketch plane.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="blue">void</span> DrawModelLineLoop(
&nbsp; &nbsp; <span class="teal">SketchPlane</span> sketchPlane,
&nbsp; &nbsp; <span class="teal">XYZ</span>[] corners )
&nbsp; {
&nbsp; &nbsp; Autodesk.Revit.Creation.<span class="teal">Document</span> factory
&nbsp; &nbsp; &nbsp; = sketchPlane.Document.Create;
&nbsp;
&nbsp; &nbsp; <span class="blue">int</span> n = corners.GetLength( 0 );
&nbsp;
&nbsp; &nbsp; <span class="blue">for</span>( <span class="blue">int</span> i = 0; i &lt; n; ++i )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">int</span> j = 0 == i ? n - 1 : i - 1;
&nbsp;
&nbsp; &nbsp; &nbsp; factory.NewModelCurve( <span class="teal">Line</span>.CreateBound(
&nbsp; &nbsp; &nbsp; &nbsp; corners[j], corners[i] ), sketchPlane );
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Determine the stack of transforms to apply to </span>
&nbsp; <span class="gray">///</span><span class="green"> the given target geometry object to bring it </span>
&nbsp; <span class="gray">///</span><span class="green"> to the proper location in the project coordinates.</span>
&nbsp; <span class="gray">///</span><span class="green"> Unfortunetely, we have not found any way at all </span>
&nbsp; <span class="gray">///</span><span class="green"> yet to identify the target object we are after.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">static</span> <span class="blue">bool</span> GetTransformStackForObject(
&nbsp; &nbsp; <span class="teal">Stack</span>&lt;<span class="teal">Transform</span>&gt; tstack,
&nbsp; &nbsp; <span class="teal">GeometryElement</span> geo,
&nbsp; &nbsp; <span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="blue">string</span> stable_representation )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Debug</span>.Print( <span class="maroon">&quot;enter GetTransformStackForObject &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;with tstack count {0}&quot;</span>, tstack.Count );
&nbsp;
&nbsp; &nbsp; <span class="blue">bool</span> found = <span class="blue">false</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">GeometryObject</span> obj <span class="blue">in</span> geo )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">GeometryInstance</span> gi = obj <span class="blue">as</span> <span class="teal">GeometryInstance</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != gi )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; tstack.Push( gi.Transform );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; found = GetTransformStackForObject( tstack,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; gi.GetSymbolGeometry(), doc,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; stable_representation );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( found ) { <span class="blue">return</span> found; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; tstack.Pop();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">continue</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Solid</span> solid = obj <span class="blue">as</span> <span class="teal">Solid</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != solid )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">string</span> rep;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">bool</span> isFace = stable_representation.EndsWith(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;SURFACE&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">bool</span> isEdge = stable_representation.EndsWith(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;LINEAR&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( isFace || isEdge,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;GetTransformStackForObject currently only supports faces and edges&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( isFace &amp;&amp; 0 &lt; solid.Faces.Size )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Face</span> face <span class="blue">in</span> solid.Faces )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; rep = face.Reference
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ConvertToStableRepresentation( doc );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( rep.Equals( stable_representation ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( isEdge &amp;&amp; 0 &lt; solid.Edges.Size )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Edge</span> edge <span class="blue">in</span> solid.Edges )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; rep = edge.Reference
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .ConvertToStableRepresentation( doc );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( rep.Equals( stable_representation ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">false</span>;
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">static</span> <span class="blue">void</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Transaction</span> trans = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; &nbsp; <span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
&nbsp; &nbsp; <span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
&nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Selection</span> choices = uidoc.Selection;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Reference</span> faceref = choices.PickObject(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ObjectType</span>.Face );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">string</span> rep = faceref
&nbsp; &nbsp; &nbsp; &nbsp; .ConvertToStableRepresentation( doc );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Assert( rep.EndsWith( <span class="maroon">&quot;:SURFACE&quot;</span> ),
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected stable representation to end with SURFACE&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print( <span class="maroon">&quot;Face reference picked: &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + rep );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Element</span> el = doc.GetElement(
&nbsp; &nbsp; &nbsp; &nbsp; faceref.ElementId );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">using</span>( trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; trans.Start( <span class="maroon">&quot;Create elements&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">TessellatedShapeBuilder</span> builder
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">TessellatedShapeBuilder</span>();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; builder.OpenConnectedFaceSet( <span class="blue">false</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// This may return a face in the family </span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// symbol definition with no family instance </span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// transform applied. Use the GeometryElement</span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// GetTransformed method to retrieve the face </span>
&nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// with the instance transformation applied.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Face</span> face = el.GetGeometryObjectFromReference(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; faceref ) <span class="blue">as</span> <span class="teal">Face</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print( <span class="maroon">&quot;Face reference property: &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + ( ( <span class="blue">null</span> == face.Reference )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ? <span class="maroon">&quot;&lt;nil&gt;&quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; : face.Reference.ConvertToStableRepresentation( doc ) ) );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Transform</span> t = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">FamilyInstance</span> fi = el <span class="blue">as</span> <span class="teal">FamilyInstance</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != fi )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Will this handle a face selected</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// in a nested family instance?</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Some, yes, but not all.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//t = fi.GetTransform();</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// This also works for some instances</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// but not all.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">//Transform t1 = fi.GetTotalTransform();</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Options</span> opt = <span class="blue">new</span> <span class="teal">Options</span>();
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; opt.ComputeReferences = <span class="blue">true</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> geo = el.get_Geometry( opt );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">GeometryElement</span> geo2 = geo.GetTransformed(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Transform</span>.Identity );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Stack</span>&lt;<span class="teal">Transform</span>&gt; tstack
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">Stack</span>&lt;<span class="teal">Transform</span>&gt;();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( GetTransformStackForObject( tstack,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; geo, doc, rep ) &amp;&amp; 0 &lt; tstack.Count )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Debug</span>.Print( <span class="maroon">&quot;GetTransformStackForObject &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;returned true with tstack count {0}&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; tstack.Count );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; t = <span class="teal">Transform</span>.Identity;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">while</span>( 0 &lt; tstack.Count )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; t *= tstack.Pop();
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Mesh</span> mesh = face.Triangulate();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != t )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; mesh = mesh.get_Transformed( t );
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span>[] triangleCorners = <span class="blue">new</span> <span class="teal">XYZ</span>[3];
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">for</span>( <span class="blue">int</span> i = 0; i &lt; mesh.NumTriangles; i++ )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">MeshTriangle</span> triangle = mesh.get_Triangle( i );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; triangleCorners[0] = triangle.get_Vertex( 0 );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; triangleCorners[1] = triangle.get_Vertex( 1 );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; triangleCorners[2] = triangle.get_Vertex( 2 );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">XYZ</span> normal = GetNormal( triangleCorners );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">SketchPlane</span> sketchPlane = GetSketchPlane(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc, triangleCorners[0], normal );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; DrawModelLineLoop( sketchPlane, triangleCorners );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">TessellatedFace</span> tesseFace
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">TessellatedFace</span>( triangleCorners,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ElementId</span>.InvalidElementId );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( builder.DoesFaceHaveEnoughLoopsAndVertices(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; tesseFace ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; builder.AddFace( tesseFace );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; builder.CloseConnectedFaceSet();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">TessellatedShapeBuilderResult</span> result
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = builder.Build(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">TessellatedShapeBuilderTarget</span>.AnyGeometry,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">TessellatedShapeBuilderFallback</span>.Mesh,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ElementId</span>.InvalidElementId );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">ElementId</span> categoryId = <span class="blue">new</span> <span class="teal">ElementId</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInCategory</span>.OST_GenericModel );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">DirectShape</span> ds = <span class="teal">DirectShape</span>.CreateElement(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc, categoryId,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Assembly</span>.GetExecutingAssembly().GetType().GUID.ToString(),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Guid</span>.NewGuid().ToString() );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; ds.SetShape( result.GetGeometricalObjects() );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; ds.Name = <span class="maroon">&quot;MyShape&quot;</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; trans.Commit();
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">catch</span>( <span class="teal">Exception</span> ex )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">TaskDialog</span>.Show( <span class="maroon">&quot;Error&quot;</span>, ex.Message );
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

#### <a name="7"></a>Download

The most up-to-date version, complete Visual Studio solution and add-in manifest is provided by the
[DirectShapeFromFace GitHub repository](https://github.com/jeremytammik/DirectShapeFromFace).

The version discussed here is
[release 2016.0.0.9](https://github.com/jeremytammik/DirectShapeFromFace/releases/tag/2016.0.0.9)

I hope you find this as interesting and useful as I do.

Many thanks to Frode for raising the issue and providing the original code to create the DirectShape element.
