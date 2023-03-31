<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-ideas/change-project-units-between-imperial-and-metric-with-one-button/idc-p/11852849#M50519
  https://github.com/jeremytammik/UnitMigration/releases/tag/2021.0.0.0
  https://github.com/jeremytammik/UnitMigration/releases/tag/2021.0.0.1
  https://github.com/jeremytammik/UnitMigration/releases/tag/2023.0.0.0
  https://github.com/jeremytammik/UnitMigration/releases/tag/2023.0.0.1

- What is UV?
  https://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-6144595024

- What is fuzz
  https://forums.autodesk.com/t5/revit-api-forum/analytical-node-vs-analytical-member-coordinate-accuracy-issue/m-p/11848971

- Whats the deal with get_Parameter()? (insert Seinfeld Bassline here)
  https://forums.autodesk.com/t5/revit-api-forum/whats-the-deal-with-get-parameter-insert-seinfeld-bassline-here/m-p/11845778

- https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2023.1.153.6
  https://autodesk.slack.com/archives/C0SR6NAP8/p1679402072325839
  https://forums.autodesk.com/t5/revit-api-forum/doc-enableworksharing-amp-language-versions/m-p/11845252
  Additional comments on this by Julian: I decided to implement a default language checker which comes back with names for both of worksets. It was rather tedious job, so please find it below. I haven't double-checked all of them, so please report if something is wrong. There are some interesting findings in it.
  There is an "Unknown type" - I didn't know if I should add it (default in a switch would cover it). In the end, I left it in the code. What should we do to get this type from Revit API?
  We have two langague types in the Revit API, hungarian and dutch, that are not listed in the [language help](https://help.autodesk.com/view/RVT/2023/ENU/?guid=GUID-BD09C1B4-5520-475D-BE7E-773642EEBD6C). I assume that those language versions are being developed, so, for now, I commented them out in the code.
  Also, it is interesting that some translators decided to keep `Workset1` as default - We have it not only in English but also in Italian and Brazilian Portuguese. I like this approach because, in my opinion,  the Polish translation `Zadanie1` is rather bad and confusing; it means something closer to "task". Because of that, I have seen some projects where worksets were named "Tasks of Adam", "Tasks of Kate", etc.
  The other interesting thing is lack of consistency regarding the number `1`. In most languages, there is no extra space between `Workset` and `1`, but in Russian, French and traditional Chinese there is. Chinese is double interesting, because in simplified Chinese we don't have empty space.

twitter:

 with the #RevitAPI  @AutodeskRevit #BIM @DynamoBIM @AutodeskAPS

&ndash;
...

linkedin:

#BIM #DynamoBim #AutodeskAPS #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### UV, Fuzz and the get_ Prefix

Here are some recent topics that came up in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160):



####<a name="2"></a> Switch Metric + Imperial Units

https://forums.autodesk.com/t5/revit-ideas/change-project-units-between-imperial-and-metric-with-one-button/idc-p/11852849#M50519
https://github.com/jeremytammik/UnitMigration/releases/tag/2021.0.0.0
https://github.com/jeremytammik/UnitMigration/releases/tag/2021.0.0.1
https://github.com/jeremytammik/UnitMigration/releases/tag/2023.0.0.0
https://github.com/jeremytammik/UnitMigration/releases/tag/2023.0.0.1

####<a name="3"></a> What is UV?

Angelo Mastroberardino [commented](https://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#comment-6144595024)
on [Planes, Projections and Picking Points](https://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html) and
the [revitapidocs `ComputeDerivatives` method](https://www.revitapidocs.com/2021.1/77ca18ef-783e-9db5-a37a-2d76f637d1a1.htm) asking:

**Question:** What is the point `UV`?
What is its reference frame and origin?
Are these `UV` the same (x,y) coordinates of any `XYZ` points of the `Face.EdgeLoops` without the z-coordinate?

**Answer:** The best place to start understanding UV coordinates is Scott Conover's 2010 AU class
on [Analysing Building Geometry](https://thebuildingcoder.typepad.com/blog/2010/01/analyse-building-geometry.html),
spread out over ten or so blog posts.

Read that and all will be clear.
Drilling down into further detail and practical application, here is an article explaining
the [relationship between 2D UV and 3D XYZ coordinates](https://thebuildingcoder.typepad.com/blog/2011/03/converting-between-2d-uv-and-3d-xyz-coordinates.html).

To round off, here is a nice debugging explanation showing how you
can [use AVF to document and label the UV coordinates](https://thebuildingcoder.typepad.com/blog/2020/12/dynamo-book-and-texture-bitmap-uv-coordinates.html#2).


####<a name="4"></a> What is Fuzz

Confusion around comparison of floating point values persists, and came up agin in the question
on [analytical node vs analytical member coordinate accuracy](https://forums.autodesk.com/t5/revit-api-forum/analytical-node-vs-analytical-member-coordinate-accuracy-issue/m-p/11848971):

**Question:** I came across a puzzling inconsistency with the new Analytical Members vs Analytical Nodes.
It seems that somewhere in the guts of Revit, the geometry curve end point coordinates that get reported get mysteriously rounded, but the `ReferencePoint` coordinates do not.
In my case, the AnalyticalNode ReferencePoint X Coordinate is 100.000002115674, but the AnalyticalMember Line.EndPoint(0) sees it's X coordinate as 100.000000000.
I'm no expert, but that seems bad to me; even though it's very small numbers, any operation looking for A==B is not going to have any success unless someone knew ahead of time that they needed to go on and round down to 5 decimal places before doing a comparison.

**Answer:** Yes.
In fact, anyone experienced in the comparison of real floating-point numbers does already know that you [need to perform some rounding operation when comparing any and all such numbers on a digital computer](https://duckduckgo.com/?q=comparing+floating+point+number).
That is standard.
The Building Coder quite regularly repeats the [need for fuzz](https://www.google.com/search?q=fuzz&as_sitesearch=thebuildingcoder.typepad.com).

In the case, of Revit, matters are even worse than in some other areas, since the Revit database represents property values and dimensions such as legth using `float` instead of `double`.
Hence, the need to [think big in Revit](https://thebuildingcoder.typepad.com/blog/2009/07/think-big-in-revit.html) and ignore every deviation below a certain (quite large) tolerance as irrelevant to the BIM, any "length below about 0.004 feet, i.e. ca. 0.05 inches or 1.2 millimetres".

Personally, when I retrieve vertex or coordinate data from Revit, I simply round it to the closest millimetre while retrieving it. I add every vertex or coordinate data item as a key to a dictionary. Every new item is looked up in the dictionary and conidered equal to the existing item if it lies within a millimetre of it.

So, in my case, A==B if A-B is smaller than 1 mm.

####<a name="5"></a> What is get_Parameter?

For a final FAQ reiterration, we address
[What's the deal with `get_Parameter`](https://forums.autodesk.com/t5/revit-api-forum/whats-the-deal-with-get-parameter-insert-seinfeld-bassline-here/m-p/11845778)

**Question:** This is more of a request for a history lesson here about the Revit API, but what's the deal with the method `get_Parameter`?
I've seen it used in past forum posts for use in Revit versions before 2022, but I don't see any references to it in the [revitapidocs](https://www.revitapidocs.com/).
Does anyone know where it stems from (the API, another API, the enchanted jungle of BIM?) and if still a valid method to use today in lieu of the `GetParameter` method (seems far easier to just use `get_Parameter` with a GUID vs `GetParameter`.

**Answer:** The [`GetParameter` method](https://www.revitapidocs.com/2023/9c22c68a-8fd5-850e-9aa8-cf7298ceebd0.htm) is
only available in the family definition environemt, working with an RFA.
The `get_` prefix is automatically generated by .NET to enable a C# client to pass in arguments to a property call, making the [`Element.Parameter` property](https://www.revitapidocs.com/2023/a742d71a-b415-9e99-2978-abd3b5bae7f2.htm) with its various overloads taking a GUID, a parameter id or a parameter definition look like a method instead of a property.

In C#, the same prefix is also added to the [`Geometry` property](https://www.revitapidocs.com/2023/d8a55a5b-2a69-d5ab-3e1f-6cf1ee43c8ec.htm), as explained by Maxim [architect.bim](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4552025) Stepannikov
in [ImportInstance Geometry vs get_Geometry](https://forums.autodesk.com/t5/revit-api-forum/importinstance-geometry-vs-get-geometry/m-p/11720577):

I am working thru some sample code that is meant to get the geometry from an Autocad import instance and I have a question about the get_Geometry call shown below:







 Reference r = uidoc.Selection.PickObject(
      ObjectType.Element,
      new JtElementsOfClassSelectionFilter<ImportInstance>() );

    var import = doc.GetElement( r ) as ImportInstance;

    // Get Geometry

    var ge = import.get_Geometry( new Options() );

    foreach( var go in ge )
    {
      if( go is GeometryInstance )
      {
        var gi = go as GeometryInstance;

        var ge2 = gi.GetInstanceGeometry();

        if( ge2 != null )




 Retrieve Visible dwg geometry Link



My understanding is that the code gets an ImportInstance and the calls get_Geometry to get the geometry. However, when I look at the Revit API docs for this object I see only a Geometry property but not a get_Geometry property or method. Shouldn't the code call the Geometry property since that is what the API says is available? What am I missing? (I am trying to convert the code to Python.) ( Revit API link  )



Also when I am working in the Revit Pyhon Shell app, the autocomplete shows that the Geometry property is available for use but not get_Geometry. If I try to use the Geometry property I get an error but if I try get_Geometry things work. So why is get_Geometry not listed? And why does Geometry returns an error? (TypeError: indexer# is not callable).



Sincerely;

Michelle


I you look into the documentation you can see that Geometry is actually an indexer. To get its value you need to specify the index in square brackets:

# Python sample
geometry = import.Geometry[Options()]
But also any property, including the indexer, can be replaced by a method with a get prefix. In this case, the value is passed in parentheses, not square brackets. This is not mentioned in the Revit API documentation as it is the basic syntax of IronPython and C# languages.

# Python sample
geometry = import.get_Geometry(Options())

Maxim Stepannikov | Architect, BIM Manager, Instructor
Tags (0)
Add tags
Report
MESSAGE 3 OF 4
michellem
 Contributor michellem in reply to: architect.bim
‎2023-02-01 02:21 PM
Hello Maxim;



Thank you for the response and for including the link to Indexers. So after reading the material on Indexers, I see that it is the word "this" (and maybe the square brackets) in the Revit API that indicates that ImportInstance.Geometry is an indexer. Since I don't know C# and barely remember the original C language, it was certainly something that flew over my head.

public GeometryElement this[
  Options options
] { get; }


Is it correct to say, the statement y = ImportInstance.Geometry[Options()] returns a value based on the index presented by Options()? Put another way, if I had bucket of misc fruit with an indexer attached to it and wrote y =  FruitBucket["apple"], the indexer would return an object associated with index of "apple" - that is an Apple object.



Also, if there is a "get_" prefix would there also be a "set_" prefix in IronPython?

Finally, would anyone know of a good book, or online link, that would discuss this "_get" prefix in more detail? I tried to Google it and had no luck.

Michelle





Tags (0)
Add tags
Report
MESSAGE 4 OF 4
jeremy.tammik
 Autodesk jeremy.tammik in reply to: michellem
‎2023-02-02 12:42 AM
Here is a nice little booklet on the topic for you to print at your leisure:



https://duckduckgo.com/?q=.net+get_+property+prefix



🙂








####<a name="6"></a> Default Localised Workset Names

https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2023.1.153.6
https://autodesk.slack.com/archives/C0SR6NAP8/p1679402072325839
https://forums.autodesk.com/t5/revit-api-forum/doc-enableworksharing-amp-language-versions/m-p/11845252
Additional comments on this by Julian: I decided to implement a default language checker which comes back with names for both of worksets. It was rather tedious job, so please find it below. I haven't double-checked all of them, so please report if something is wrong. There are some interesting findings in it.
There is an "Unknown type" - I didn't know if I should add it (default in a switch would cover it). In the end, I left it in the code. What should we do to get this type from Revit API?
We have two langague types in the Revit API, hungarian and dutch, that are not listed in the [language help](https://help.autodesk.com/view/RVT/2023/ENU/?guid=GUID-BD09C1B4-5520-475D-BE7E-773642EEBD6C). I assume that those language versions are being developed, so, for now, I commented them out in the code.
Also, it is interesting that some translators decided to keep `Workset1` as default - We have it not only in English but also in Italian and Brazilian Portuguese. I like this approach because, in my opinion,  the Polish translation `Zadanie1` is rather bad and confusing; it means something closer to "task". Because of that, I have seen some projects where worksets were named "Tasks of Adam", "Tasks of Kate", etc.
The other interesting thing is lack of consistency regarding the number `1`. In most languages, there is no extra space between `Workset` and `1`, but in Russian, French and traditional Chinese there is. Chinese is double interesting, because in simplified Chinese we don't have empty space.


**Question:**

**Answer:**

**Response:**

<center>
<img src="img/.jpg" alt="" title="" width="100"/> <!-- Pixel Height: 240 Pixel Width: 300 -->
</center>

