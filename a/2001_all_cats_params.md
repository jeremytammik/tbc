<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- roman [jeremytammik/RevitLookup] Revit api. Retrieve all parameters and categories (Discussion #183)

- How to get the BoundingBox that corresponds to the shape of the Family
  https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-boundingbox-that-corresponds-to-the-shape-of-the/m-p/12089970

- Transform of linked element creates an empty outline
  https://forums.autodesk.com/t5/revit-api-forum/transform-of-linked-element-creates-an-empty-outline/m-p/12089717#M72649

- nice svg path explanation --  https://www.nan.fyi/svg-paths

- claude.ai
  https://claude.ai/chats
  You have to use a USA or UK VPN.
  A competitor to OpenAI. Can upload big files. Any better for coding?
  Video about it: https://www.youtube.com/watch?v=dofH3EBjK1o
  Falls euch der aktuelle zustand der KI-Entwicklung interessieren sollte… ich habe nach der vertragsunterzeichnung andere arbeiten ausgefuehrt, und bin im laufe dessen auch auf eine neuen konkurrenten von ChatGPT gestossen, namens Claude.AI:
  https://claude.ai
  ChatGPT: bisher ueber 13 millarden $, Microsoft hauptsponsor
  Claude.AI: bisher 1.5 millarden $, Google hauptsponsor.
  [claude versus chatgpt](https://duckduckgo.com/?q=claude+versus+chatgpt)
  Ich habe claude.ai gebeten, den kuri architektenvertrag auszuwerten, und dann nach sinn und unsinn von hoai gefragt.
  Unten sind die antworten angehaengt.
  /Users/jta/a/doc/revit/tbc/git/a/img/claudeai*
  summarise my last blog post
  You could batch that last one to summarize all of your blog posts programmatically for SEO.

- Why transformative AI is really, really hard to achieve
  https://zhengdongwang.com/2023/06/27/why-transformative-ai-is-really-really-hard-to-achieve.html

twitter:


&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### All Categories and Parameters



####<a name="2"></a> All Categories and Parameters

Roman [Nice3point](https://github.com/Nice3point), principle maintained of RevitLookup, presents a new discovery; in his own words:

I made a new discovery for me.
As far as I know, it is impossible to get a list of all built-in parameters using the public Revit API.
We are offered an `enum`, but we cannot get the `Parameter` itself from it.
By exploring private unmanaged code using reflection and pointers, I managed to do it, as described in
the [RevitLookup discussion 183 &ndash; retrieve all parameters and categories](https://github.com/jeremytammik/RevitLookup/discussions/183):

<center>
<img src="img/rk_get_all_bips_1.png" alt="Get all built-in parameters" title="Get all built-in parameters" width="600"/> <!-- Pixel Height: 915 Pixel Width: 1,469 -->
</center>

####<a name="2.1"></a> Summary

I recently came across a problem from my business partner who wanted to get all the built-in Revit parameters to extract metadata such as data type, units, storage type, etc. As we know, this is not possible using the Autodesk Revit public API.

Similar situation with categories, we can't get all the built-in categories; so far, the only known available way is to get them from the document settings, but it contains a very truncated list:

<center>
<img src="img/rk_get_all_bips_1_categories.png" alt="Only some categories" title="Only some categories" width="600"/> <!-- Pixel Height: 285 Pixel Width: 941 -->
</center>

####<a name="2.2"></a> Solution

We can use private code with reflection and pointers; we only need a document.
The point of the method is not to get them but create it.

Example code to get all parameters:

<pre class="prettyprint">
public static List&lt;Parameter&gt; GetBuiltinParameters(
  Document document)
{
  const BindingFlags bindingFlags = BindingFlags.NonPublic
    | BindingFlags.Instance | BindingFlags.DeclaredOnly;

  var documentType = typeof(Document);
  var parameterType = typeof(Parameter);
  var assembly = Assembly.GetAssembly(parameterType);
  var aDocumentType = assembly.GetType("ADocument");
  var elementIdType = assembly.GetType("ElementId");
  var elementIdIdType = elementIdType.GetField("&lt;alignment member&gt;", bindingFlags)!;
  var getADocumentType = documentType.GetMethod("getADocument", bindingFlags)!;
  var parameterCtorType = parameterType.GetConstructor(bindingFlags, null, new[] {aDocumentType.MakePointerType(), elementIdType.MakePointerType()}, null)!;

  var builtinParameters = Enum.GetValues(typeof(BuiltInParameter));
  var parameters = new List&lt;Parameter&gt;(builtinParameters.Length);
  foreach (BuiltInParameter builtinParameter in builtinParameters)
  {
    var elementId = Activator.CreateInstance(elementIdType);
    elementIdIdType.SetValue(elementId, builtinParameter);

    var handle = GCHandle.Alloc(elementId);
    var elementIdPointer = GCHandle.ToIntPtr(handle);
    Marshal.StructureToPtr(elementId, elementIdPointer, true);

    var parameter = (Parameter) parameterCtorType.Invoke(
      new[] {getADocumentType.Invoke(document, null),
        elementIdPointer});
    parameters.Add(parameter);
    handle.Free();
  }

  return parameters;
}
</pre>

Example code to get all categories:

<pre class="prettyprint">
public static List&lt;Category&gt; GetBuiltinCategories(
  Document document)
{
  const BindingFlags bindingFlags = BindingFlags.NonPublic
    | BindingFlags.Instance | BindingFlags.DeclaredOnly;

  var documentType = typeof(Document);
  var categoryType = typeof(Category);
  var assembly = Assembly.GetAssembly(categoryType);
  var aDocumentType = assembly.GetType("ADocument");
  var elementIdType = assembly.GetType("ElementId");
  var elementIdIdType = elementIdType.GetField("&lt;alignment member&gt;", bindingFlags)!;
  var getADocumentType = documentType.GetMethod("getADocument", bindingFlags)!;
  var categoryCtorType = categoryType.GetConstructor(bindingFlags, null, new[] {aDocumentType.MakePointerType(), elementIdType.MakePointerType()}, null)!;

  var builtInCategories = Enum.GetValues(typeof(BuiltInCategory));
  var categories = new List&lt;Category&gt;(builtInCategories.Length);
  foreach (BuiltInCategory builtInCategory in builtInCategories)
  {
    var elementId = Activator.CreateInstance(elementIdType);
    elementIdIdType.SetValue(elementId, builtInCategory);

    var handle = GCHandle.Alloc(elementId);
    var elementIdPointer = GCHandle.ToIntPtr(handle);
    Marshal.StructureToPtr(elementId, elementIdPointer, true);

    var category = (Category) categoryCtorType.Invoke(
      new[] {getADocumentType.Invoke(document, null),
        elementIdPointer});
    categories.Add(category);
    handle.Free();
  }

  return categories;
}
</pre>

####<a name="2.3"></a> Result

As a result, we have created all the parameters and all the categories of the entire `Enum`:

<center>
<img src="img/rk_get_all_bips_2_all_params.png" alt="All built-in parameters" title="All built-in parameters" width="600"/> <!-- Pixel Height: 327 Pixel Width: 1,107 -->
<p style="font-size: 80%; font-style:italic">All built-in parameters</p>

<img src="img/rk_get_all_bips_3_all_cats.png" alt="All built-in categories" title="All built-in categories" width="600"/> <!-- Pixel Height: 326 Pixel Width: 1,018 -->
<p style="font-size: 80%; font-style:italic">All built-in categories</p>
</center>

####<a name="2.4"></a> Limitations

Created parameters have no binding to any element, and consequently have no value, only metadata.

Many thanks to Roman for this interesting in-depth research and documentation!

####<a name="3"></a> BoundingBox is Axis-Aligned

Let's move on from this in-depth research to the more mundane question
of [how to get the `BoundingBox` that corresponds to the shape of the family](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-boundingbox-that-corresponds-to-the-shape-of-the/m-p/12089970):

**Question:** I would like to inquire about `BoundingBox`.

In the left image, the area of the BoundingBox is similar to the shape of the pipe;
however, in the right one, the BoundingBox is much larger than its pipe:

<img src="img/pipe_boundingbox.png" alt="Pipe bounding box" title="Pipe bounding box" width="600"/>

It seems that this happens only for Pipes (probably for System Families).
For FamilyInstance, it seems that they always have a same size for BoundingBox, even if they are rotated.

Is there a way to know the area of the Family, which always corresponds to its shape, regardless of it being rotated or not?

**Answer:**  No, this does not only happen for pipes.
This happens for all kinds of objects that have different dimensions in different directions and are placed at an angle to the cardinal axes.
For instance, a vertical wall at a 45 degree angle in the XY plane will exhibit the exact same behaviour.
This behaviour is intentional.
The Revit bounding box is always aligned with the cardinal axes.
That makes it extremely fast and efficient to work with.
In wikipedia, they call this
an [axis-aligned bounding box](https://en.wikipedia.org/wiki/Minimum_bounding_box#Axis-aligned_minimum_bounding_box).

####<a name="4"></a> BoundingBox Transformation

That question leads over nicely to the explanation why
the [transform of linked element creates an empty outline](https://forums.autodesk.com/t5/revit-api-forum/transform-of-linked-element-creates-an-empty-outline/m-p/12089717),
solved by appropriately transforming the bounding box:


I want to create an outline from a linked element's bounding box to use in a BoundingBoxIntersectsFilter. My approach bellow works for some cases but where a link or element is rotated beyond a certain limit, the bounding box goes askew and the Outline(xyz, xyz) method returns an empty outline. I've tried scaling the bounding box up with an offset which fixes some cases, but not all of them. Some advice on solutions would be appreciated.
Screenshot (27).png
non-rotated link

xform_boundingbox_1.png  Pixel Height: 647
Pixel Width: 793
xform_boundingbox_2.png
Pixel Height: 746
Pixel Width: 961

Screenshot (28).png
rotated link
#get the elements bounding box
s_BBox = element.get_BoundingBox(doc.ActiveView)

#apply the link documents transform
s_BBox_min = link_trans.OfPoint(s_BBox.Min)
s_BBox_max = link_trans.OfPoint(s_BBox.Max)

#make the outline
new_outline = Outline(s_BBox_min, s_BBox_max)

#make the filter
bb_filter = BoundingBoxIntersectsFilter(new_outline)


 Solved by jeremy.tammik. Go to Solution.
Tags (3)
Tags:bounding boxLink Transformoutline

Add tags
Report
2 REPLIES
Sort:
MESSAGE 2 OF 3
jeremy.tammik
  Autodesk jeremy.tammik  in reply to: dean.hayton
‎2023-07-07 09:45 AM

Yes. You can corrupt the bounding box by transforming it. I would suggest the following:

Extract all eight corner vertices of the bounding box
Transform all eight vertices as individual points
Create a new bounding box from the eight transformed results

The Building Coder samples includes code to create and enlarge a bounding box point by point that will come in handy for the last step:

  /// <summary>
  ///   Expand the given bounding box to include
  ///   and contain the given point.
  /// </summary>
  public static void ExpandToContain(
    this BoundingBoxXYZ bb,
    XYZ p)
  {
    bb.Min = new XYZ(Math.Min(bb.Min.X, p.X),
      Math.Min(bb.Min.Y, p.Y),
      Math.Min(bb.Min.Z, p.Z));

    bb.Max = new XYZ(Math.Max(bb.Max.X, p.X),
      Math.Max(bb.Max.Y, p.Y),
      Math.Max(bb.Max.Z, p.Z));
  }

https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/Util.cs#L2724-L...

Jeremy Tammik,  Developer Advocacy and Support, The Building Coder, Autodesk Developer Network, ADN Open
Tags (0)
Add tags
Report
MESSAGE 3 OF 3
dean.hayton
  Participant dean.hayton  in reply to: jeremy.tammik
‎2023-07-09 03:35 PM
Hi Jeremy, this solution works well, thanks



####<a name="5"></a> Interactive Explanation of SVG Path

 nice svg path explanation --  https://www.nan.fyi/svg-paths

####<a name="6"></a> Calude.AI

ChatGPT has a new competitor,
[claude.ai](https://claude.ai).

Eric Boehlke of [truevis BIM Consulting](https://truevis.com) pointed it out to me, saying:

> You can create an account in the USA or UK, or use VPN to get there.
A competitor to OpenAI. Can upload big files. Any better for coding?

I tested it myself by asking it to summarise
my [last blog post](https://thebuildingcoder.typepad.com/blog/2023/07/rbp-materials-their-assets-and-the-visual-api.html),
evaluate a German architectural contract and discuss
the German [HOAI](https://de.wikipedia.org/wiki/Honorarordnung_f%C3%BCr_Architekten_und_Ingenieure) without
providing any hont of what that acronym might mean, or even that I was looking for anything German.
The results were completely satisfying in all three cases:

claudeai_archvertrag.png
claudeai_hoai.png
claudeai_tbc2000.png

<center>
<img src="img/claudeai_tbc2000.png" alt="Claude.AI analyses blog post" title="Claude.AI analyses blog post" width="600"/>
<p style="font-size: 80%; font-style:italic">Claude.AI analyses blog post</p>

<img src="img/claudeai_archvertrag.png" alt="Claude.AI analyses a German architectural contract" title="Claude.AI analyses a German architectural contract" width="600"/>
<p style="font-size: 80%; font-style:italic">Claude.AI analyses a German architectural contract</p>

<img src="img/claudeai_hoai.png" alt="Claude.AI discusses the HOAI" title="Claude.AI discusses the HOAI" width="600"/>
<p style="font-size: 80%; font-style:italic">Claude.AI discusses the HOAI</p>
</center>

Eric suggests:

> You could batch that blog post analysis to summarise all of your blog posts programmatically for SEO.

####<a name="7"></a> Relativating the Impact of AI

Let's close with some wise and pertinent thoughts &ndash; not following the hype &ndash; on the possible impact of AI on our life and economy, presented in the explaination
of [why transformative AI is really, really hard to achieve](https://zhengdongwang.com/2023/06/27/why-transformative-ai-is-really-really-hard-to-achieve.html).

