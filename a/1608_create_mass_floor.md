<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

13663566 [APIによるマス床の生成方法 -- How to generate mass floor using API]

Creating face wall and mass floor via #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/facewallmassfloor thanks to @harrymattison 

I went on my first ski tour this season...
lots of interesting issues in
the Revit API discussion forum... 
a Japanese case on programmatically generating a mass floor, enabling us to mention yet another hitherto unmentioned Revit API usility class, <code>MassInstanceUtils</code>...
Please tell me if a method exists to generate a mass floor using the API...

--->

### Creating Face Wall and Mass Floor

I went on my first ski tour this season, on and around
the [Hochgescheid, in the Black Forest](https://en.wikipedia.org/wiki/List_of_mountains_and_hills_of_the_Black_Forest),
for a change:

<center>
<img src="img/020_michael_jeremy_1000x404.jpg" alt="Hochgescheid" width="500"/>
</center>

On the Revit API side of things, besides lots of interesting issues in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160),
a Japanese ADN case came up on programmatically generating a mass floor, enabling us to mention yet another hitherto unmentioned Revit API usility class, `MassInstanceUtils`:

**Question:** マス床をAPIで生成する方法があれば教えてください。 &ndash; Please tell me if a method exists to generate a mass floor using the API.

**Answer:** First of all, here is the description
on [creating a floor from a mass floor in the user interface](http://help.autodesk.com/view/RVT/2018/ENU/?guid=GUID-03EABD3A-4736-4762-97F8-F473FEE18162).

Please be aware that the Revit API does not support the creation of in-place mass elements.

As suggested by
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [creating components in mode 'Model In-Place'](https://forums.autodesk.com/t5/revit-api-forum/creation-components-in-mode-model-in-place/m-p/3822639),
the alternative is to create a [direct shape](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.50) instead.
Unfortunately, you will still not end up with a mass.

However, if you have already created a mass element by some other means, you can use it to generate face walls and mass floors programmatically.

This really old thread asking the same question
on [creating mass floors and or scope boxes](https://forums.autodesk.com/t5/revit-api-forum/creating-mass-floors-and-or-scope-boxes/m-p/2388509) still
remains open today.
I'll add this answer to that as soon as I complete publishing it on The Building Coder.

Nowadays, the answer to this question is provided by
the [MassInstanceUtils.AddMassLevelDataToMassInstance](http://www.revitapidocs.com/2018.1/fe3b251b-2677-094d-7e72-77fea0f49f24.htm) method.

It generates floors defined by levels based on a given mass geometry.

Its use is demonstrated and very clearly explained
by Harry Mattison of [Boost your BIM](https://boostyourbim.wordpress.com) in his post
on [automating the Building Maker workflow](https://boostyourbim.wordpress.com/2014/02/11/automating-the-building-maker-workflow).

It uses the `FaceWall` `Create` and `MassInstanceUtils` `AddMassLevelDataToMassInstance` methods to generate walls and floors based on a selected mass element's geometry, faces and levels.

He demonstrates the add-in running live in his three-and-a-half-minute video
on [Face Wall and Mass Floor creation with the Revit API](https://youtu.be/nHWen2_lN6U):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/nHWen2_lN6U" frameborder="0" gesture="media" allow="encrypted-media" allowfullscreen></iframe>
</center>

The API calls are driven by Harry's C# .NET Revit API macro `CreateFaceWallsAndMassFloors`.

Here is the code copied from Harry's post and added
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
in [lines 414-473 of CmdFaceWall.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdFaceWall.cs#L414-L473):

<pre class="code">
<span style="color:blue;">#region</span>&nbsp;CreateFaceWallsAndMassFloors
<span style="color:green;">//&nbsp;By&nbsp;Harry&nbsp;Mattison,&nbsp;Boost&nbsp;Your&nbsp;BIM,</span>
<span style="color:green;">//&nbsp;Automating&nbsp;the&nbsp;Building&nbsp;Maker&nbsp;workflow</span>
<span style="color:green;">//&nbsp;https://boostyourbim.wordpress.com/2014/02/11/automating-the-building-maker-workflow/</span>
<span style="color:green;">//&nbsp;Face&nbsp;Wall&nbsp;and&nbsp;Mass&nbsp;Floor&nbsp;creation&nbsp;with&nbsp;the&nbsp;Revit&nbsp;API</span>
<span style="color:green;">//&nbsp;https://youtu.be/nHWen2_lN6U</span>
 
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Create&nbsp;face&nbsp;walls&nbsp;and&nbsp;mass&nbsp;floors&nbsp;on&nbsp;and&nbsp;in&nbsp;selected&nbsp;mass&nbsp;element</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;CreateFaceWallsAndMassFloors(&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;fi&nbsp;=&nbsp;doc.GetElement(
&nbsp;&nbsp;&nbsp;&nbsp;uidoc.Selection.PickObject(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ObjectType</span>.Element&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">FamilyInstance</span>;
 
&nbsp;&nbsp;<span style="color:#2b91af;">WallType</span>&nbsp;wType&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">WallType</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">WallType</span>&gt;().FirstOrDefault(&nbsp;q
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;q.Name&nbsp;==&nbsp;<span style="color:#a31515;">&quot;Generic&nbsp;-&nbsp;6\&quot;&nbsp;Masonry&quot;</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Options</span>&nbsp;opt&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>();
&nbsp;&nbsp;opt.ComputeReferences&nbsp;=&nbsp;<span style="color:blue;">true</span>;
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Create&nbsp;Face&nbsp;Walls&nbsp;&amp;&nbsp;Mass&nbsp;Floors&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Solid</span>&nbsp;solid&nbsp;<span style="color:blue;">in</span>&nbsp;fi.get_Geometry(&nbsp;opt&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;q&nbsp;=&gt;&nbsp;q&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">Solid</span>&nbsp;).Cast&lt;<span style="color:#2b91af;">Solid</span>&gt;()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;f&nbsp;<span style="color:blue;">in</span>&nbsp;solid.Faces&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">FaceWall</span>.IsValidFaceReferenceForFaceWall(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;f.Reference&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FaceWall</span>.Create(&nbsp;doc,&nbsp;wType.Id,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">WallLocationLine</span>.CoreExterior,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;f.Reference&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;levels
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Level</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Level</span>&nbsp;level&nbsp;<span style="color:blue;">in</span>&nbsp;levels&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">MassInstanceUtils</span>.AddMassLevelDataToMassInstance(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;fi.Id,&nbsp;level.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;}
}
<span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;CreateFaceWallsAndMassFloors</span>
</pre>

Many thanks to Harry for documenting and implementing this!
