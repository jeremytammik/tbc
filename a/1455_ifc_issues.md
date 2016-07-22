<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

- gross spannort fotos

- Autodesk Forge Platform Gets New APIs
  http://www.engineering.com/DesignSoftware/DesignSoftwareArticles/ArticleID/12640/Autodesk-Forge-Platform-Gets-New-APIs.aspx
  
- Autodesk Forge Platform: Little Machines
  https://www.youtube.com/watch?v=tta-V6wyUKY
  
- 11988606 [Ifc Import shows incorrect result]

- IFC open source
  http://forums.autodesk.com/t5/revit-api/opensource-api-for-reading-ifc-files/m-p/6433280
  11980002 [Opensource API for reading IFC Files]

- http://forums.autodesk.com/t5/revit-api/open-ifc-through-api/m-p/6451258

- 11996877 [customize IFC Import]

- IFC supports families
  http://forums.autodesk.com/t5/revit-api/open-ifc-through-api/td-p/6451258/
  
#IFC Import and Open Source #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim @openbimstandard

Quite a few issues revolving around IFC came up lately in various ADN cases and in the Revit API discussion forum. Before getting to the technical stuff, some pictures from my latest mountaineering trip to climb Gross Spannort
&ndash; Autodesk Forge Platform &mdash; Little machines
&ndash; IFC import scaling issue
&ndash; IFC open source C# library
&ndash; Customising IFC import
&ndash; IFC family support... 

-->

### IFC Import and Open Source

Quite a few issues revolving around IFC came up lately in various ADN cases and in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160).

Before getting to the technical stuff, here are some pictures from my latest mountaineering trip to climb Gross Spannort:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157670398939461" title="Gross Spannort"><img src="https://c2.staticflickr.com/9/8614/28245261321_169d513752_n.jpg" width="320" height="240" alt="Gross Spannort"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

Back to the Forge, Revit and IFC related programming topics:

- [Autodesk Forge Platform &mdash; Little machines](#2)
- [IFC import scaling issue](#3)
- [IFC open source C# library](#4)
- [Customising IFC import](#5)
- [IFC family support](#6)



#### <a name="2"></a>Autodesk Forge Platform &mdash; Little Machines

Engineering.com published a nice article highlighting
the [Autodesk Forge Platform New APIs](http://www.engineering.com/DesignSoftware/DesignSoftwareArticles/ArticleID/12640/Autodesk-Forge-Platform-Gets-New-APIs.aspx).

Autodesk itself published an interesting short video envisioning its use in the shape
of [Little Machines](https://www.youtube.com/watch?v=tta-V6wyUKY) addressing various design, manufacturing and maintenance aspects:

<center>
<iframe width="420" height="236" src="https://www.youtube.com/embed/tta-V6wyUKY?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

Imagine powerful 3D viewing data state of the art reality capture Internet of Things functionality augmented reality workspaces generative design processes with real-world performance simulation building native design applications to deliver innovative customer experiences leveraging powerful 3D print APIs all on one platform: [Autodesk Forge](https://forge.autodesk.com).

#### <a name="3"></a>IFC Import Scaling Issue

**Question:** I created a simple IFC file representing two pipes and an elbow in an external software.

I can view it perfectly well in several common IFC viewers, e.g.:

- FZKViewer Version V4.6 (64Bit)
- Solibri V.9.6
- Tekla BimSight V1.9.7
- Bim Vision 2.10

However, I tried to import it into Revit, and it seems that the specified transformation settings have no effect.

What could be causing these issues in Revit and how can I modify the IFC sample file so that Revit can read it correctly?

I already searched for a solution and asked some IFC specialists before turning to you.

I also created a pipe_elbow example in Revit to analyse the difference in the IFC structure, but couldn't find any essential issue.

I store `IfcFlowSegment` and `IfcFlowFitting` in the IFC file.

I took a look at the objects that Revit and MEP export.

My approach to analyse the problem was to create an IFC file with simple elements (1 pipe or 1 elbow) in Revit and MEP to analyse the result in a text editor.

Afterwards I created a similar file with my engine and compared the difference.

As far as I can tell, Revit also uses `IfcFaces`, `IfcPolyloops` and `IfcMappedItems`, just like I do.

For me it seems Revit requires some special conditions that I need to be aware of to create proper Revit IFC files.


**Answer:** I uploaded your sample file to A360 and confirm that it does indeed display what looks like two pipes and an elbow:

<center>
<img src="img/simple_pipe_elbow_mh_a360.png" alt="IFC pipes and elbow in A360" width="317">
</center>

I tried opening the file in Revit and see nothing at all.

I am discussing with our IFC experts.

> I think the issue is that the pipe is DEFINED in meters, and the scaling transform converts it to millimetres.  It is slightly non-standard to have such an extreme transformation (basically, please scale this down by 1/1000th in every direction), and the Revit Transform code may be getting confused thinking that this is too close to zero.  Still, it isn’t that close to zero, we will take a look and have filed an internal task to do so.


**Response:** I could figure out following behaviour:

I modified the row #949 from its original value:

<pre>
#949 = IFCCARTESIANTRANSFORMATIONOPERATOR3DNONUNIFORM(#950, #951, #952, 5.00000023748726E-4, #953, 5.00000023748726E-4, 5.00000023748726E-4);
</pre>

I raised the scaling factor from 1E-4 to 1E-3:

<pre>
#949 = IFCCARTESIANTRANSFORMATIONOPERATOR3DNONUNIFORM(#950, #951, #952, 5.00000023748726E-3, #953, 5.00000023748726E-3, 5.00000023748726E-3);
</pre>

With that modification, it works.

Probably the original value was below an internal limit that invalidates the whole transformation.

Currently I’m using metres for the data.

Probably it will work better if using millimetres.


**Answer:** Yes, using meters probably explains the issue right away.

Please be aware that all Revit internal length units are considered to
be [imperial feet](http://thebuildingcoder.typepad.com/blog/2011/03/internal-imperial-units.html), and
that [Revit does not support very small objects](http://thebuildingcoder.typepad.com/blog/2009/07/think-big-in-revit.html).

Everything that is too small will be ignored. The limit is around 1/16th or an inch, which is about 1.2 mm.

I hope that specifying millimetres instead will resolve the problem.

You might end up with too large objects, though  :-)

On the other hand, the units or sizing should in no way affect any transformations being performed.

In theory, all transformations should be completely unit-less.

In practice, Revit adds some sanity checks that make sense inside of Revit and just happened to get in the way of your dtat structuring approach.


**Response:** I confirmed that the scaling value of the `IfcCartesianTransformationOperator3DnonUniform` causes the issue.

If I use scale values smaller than `1.E-3`, the transformation matrix is ignored.

After using millimetres instead of metres it works without issues.


#### <a name="4"></a>IFC Open Source C# Library

Another IFC issue came up in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread on
an [open source API for reading IFC Files](http://forums.autodesk.com/t5/revit-api/opensource-api-for-reading-ifc-files/td-p/6433280).

In it, Jon Mirtschin points out his open source IFC C# classes:

**Question:** Can anyone suggest an open source API for reading IFC files?

**Answer:** Which language?  .NET (i.e., c# or vb.net)?
 
I opensourced the core of
my [GeometryGymIFC library](https://github.com/jmirtsch/GeometryGymIFC) earlier in the year.

This will handle IFC2x3 and IFC4 simultaneously, and I'm enabling ifcxml at the moment (and also ifcjson).
 
Here are some [GeometryGymIFC examples](https://github.com/jmirtsch/GeometryGymIFCExamples).
 
It's also available from nuget.  
 
There are alternatives, BIMServer.  For c++, I'd suggest trying IfcOpenShell or the CSTB.

Regarding generic tools, the Building Smart organisation page also provides several pointers
to [IFC open source](http://www.buildingsmart-tech.org/implementation/get-started/ifc-open-source).

Another popular IFC ENGINE is [IFCEngine from RDF](http://rdf.bg/products.php).



#### <a name="5"></a>Customising IFC Import

**Question:** I would like to enhance the IFC import to enable importing and creating new electrical distribution board configuration families with connectors and all relevant parameters.

I have therefore two questions:

1. Can I use the Revit IFC API to programmatically adapt (hook into) the IFC import process or to enable a separate IFC import?
2. Another possibility (in the long run) would be to contribute to the Autodesk Revit open source IFC for Revit project.

Are there plans to develop or allow a more specialized IFC import, creating families (instances) that are more parametric (e.g., support connectors, properties, etc.) and can better be used in later Revit workflows?

**Answer:** Thank you for your query.

1. Regarding your first question, afaik, the IFC API is intended to enable to open source IFC import and export, and nothing further.

It does not provide any functionality to hook into it, only to implement all the aspects required for the complete IFC import and export.

2. Yes, that is basically the way to go for any enhancements that you require, afaik, both in the short and long run.

I am not aware of any plans for any specific enhancements to the open source project.

I believe it lives a life of its own, and you should contact the project owners to learn more.

Actually, you could combine your questions one and two by implementing the hooks that you are interested in in the existing open source implementation.

The development team adds:
 
> You can actually override the internal Open IFC command with a custom API.  Basically, Open IFC currently goes through the importer and then calls one API function that does pretty much everything.  You should be able to do your own thing instead.  I don’t know that anyone has tried that so of course it may have faults.
 


#### <a name="6"></a>IFC Family Support

I was not aware that IFC supports anything like the concept of Revit families, and Jon set me right on that as well, saying:

I disagree with your statements:

*Family is a pure Revit concept... IFC knows nothing about families and never will.*

IFC has classes derived from an `IfcTypeProduct` which are to all practical purposes the same as a Revit Family, and this concept is understand in various BIM software.  Read the [IfcTypeProduct description](http://www.buildingsmart-tech.org/ifc/IFC4/final/html/link/ifctypeproduct.htm). Note this was introduced in IFC2x which as I understand it was released circa 2000, a similar time to early development of Revit?  If a family is a Revit concept, I'm not sure how [Bentley recognizes anything in a .rfa file](http://communities.bentley.com/products/building/building_analysis___design/f/5917/t/87255). I am pretty sure that manufacturers have expressed object libraries in various forms including CAD prior to this.
 
Types as a concept in Revit (variations of a master parametric definition) is not typically implemented in IFC developments but hopefully this changes soon.  IFC4 offers this and I am working on some object library projects that wish to utilize this.
 
*An IFC file can define a project, not a family.*

This is again incorrect.  IFC4 added a new context, the [`IfcProjectLibrary`](http://www.buildingsmart-tech.org/ifc/IFC4/final/html/link/ifcprojectlibrary.htm). 
This is appropriate for exchanging data in the form of object libraries and the like as per it's definition.  It would not contain project and instance elements.  I don't believe Revit supports this context in its native importer or exporter.
 
*Therefore, if you open an IFC file, it will appear in the project view and never as a family.*

This might be true at this point in time, but I would hope it might change at some point in time if Autodesk wants to support openBIM workflows.
 
To reply to the original question, Geometry Gym offers a 3rd party importer that does generate families and create parameters based on types in `IfcProject` and `IfcProjectLibrary`, cf. [Geometry Gym &gt; Revit](http://geometrygym.blogspot.com.au/search/label/Revit). This is primarily from the user interface, however if you require from the API then I can refactor the method slightly and provide it as a public method in the DLL.  An alternative would be to edit the open source importer and make it recognize these IFC concepts.
 
Many thanks to Jon for his in-depth knowledge and explanation!


#### <a name="7"></a>IFC Family Support Discussion Links

Ryan Schulz points out a number of additional interesting discussions on the topic in
his [comment below](http://thebuildingcoder.typepad.com/blog/2016/07/ifc-import-and-open-source.html#comment-2797771810):

- [IFC for Revit &ndash; Support for Revit Groups?](https://sourceforge.net/p/ifcexporter/discussion/general/thread/fad5b2f8)
- [BuildingSMART/mvdXML issue #3 &ndash; Possible to outline an MVD exchange requirement to address the concept of groups? ](https://github.com/BuildingSMART/mvdXML/issues/3)
- [BuildingSMART/IFC4-CV issue #16 &ndash; Support for a One-to-Many (non-type) of Group](https://github.com/BuildingSMART/IFC4-CV/issues/16)
- [FreeCAD &ndash; Convert IfcGroups into FreeCAD Compounds](http://forum.freecadweb.org/viewtopic.php?f=23&t=12131)
- [IndustryFoundationClasses/Questions issue #3 &ndash; Suggested IFC Entity to save a Profile Type](https://github.com/IndustryFoundationClasses/Questions/issues/3)
