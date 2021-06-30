<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Asset Keyword Access
  https://app.slack.com/client/T02NW42JD/C0SR6NAP8
  Ryuji Ogasawara, RuoQian Lu and Joe Qiao 

- need to regen:
  Set Different Materials to Parts of a Wall
  https://forums.autodesk.com/t5/revit-api-forum/set-different-materials-to-parts-of-a-wall/m-p/10427403

- need to regen:
  Modifying group in api results in duplicate group
  https://forums.autodesk.com/t5/revit-api-forum/modifying-group-in-api-results-in-duplicate-group/m-p/10374430

- New post on Boost Your BIM
  Topo From Lines now in the free Revit tool set
  by harrymattison
  This tool is an"oldie but goodie" that lets you create and update a toposurface from a set of model lines. A new version for 2022 was recently requested, so I added it to the Terrific Tool project and created a 2022 build. You can download the current installer here and if you like code, that is here
  https://bitbucket.org/BoostYourBIM/boostyourbimterrifictools/src/master/BoostYourBIMTerrificTools/TopoFromLines.cs
  https://bitbucket.org/BoostYourBIM/boostyourbimterrifictools/raw/cd5589e83a158f283774d7a5e9590f07d85c904a/Boost+Your+BIM+Terrific+Tools-SetupFiles/Boost+Your+BIM+Terrific+Tools.msi&data=04%7C01%7Cjeremy.tammik%40autodesk.com%7Ce709df334bc34c20e06f08d9371557e6%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C637601387077658469%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C1000&sdata=HBFqPbGzz8Js990PpjCS5UuexeYytjlaxnU/EIDMFTw%3D&reserved=0

twitter:

add #thebuildingcoder

Painting stairs and shooting for the beams with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/paintstair

Two Revit API discussion forum threads that I am currently involved in
&ndash; Painting stairs
&ndash; Ray tracing vs bounding box to find beams intersecting columns...

linkedin:

Painting stairs and shooting for the beams with the #RevitAPI

https://autode.sk/paintstair

Two Revit API discussion forum threads that I am currently involved in:

- Painting stairs
- Ray tracing vs bounding box to find beams intersecting columns...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Question:** 

**Answer:**

**Response:**  

Many thanks to  for this very helpful explanation!

-->

### Asset Keyword

Two [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) threads
reinforce the ever-present [need to regenerate](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33),
the development team clarifies access to various asset keyword properties, and Harry Mattison shares a free tool to create and update a toposurface from a set of model lines:





<center>
<img src="img/keyword_clustering.png" alt="Keyword clustering" title="Keyword clustering" width="425"/> <!-- 850 -->
</center>

####<a name="2"></a> Asset Keyword Access

**Question:** I have an issue setting a string value to the material appearance asset keyword property.
In one material, it can be set as expected, but another material returns an error saying, "The input value is invalid for this AssetPropertyString property.\r\nParameter name: value".
I found the blog article
on [Material, Physical and Thermal Assets](https://thebuildingcoder.typepad.com/blog/2019/11/material-physical-and-thermal-assets.html) and the internal development ticket *REVIT-170824* which explains that the keyword property on the `Identity` tab is not exposed yet.
However, I still expect the "keyword" property on the appearance tab to accept a string value.
In addition, I can see some error message in the journal file when I try to run the code below.
Is it possible to set the "keyword" property of the appearance asset?

<pre class="code">
<span style="color:blue;">void</span>&nbsp;SetMaterialAppearanceAssetKeywordProperty(
&nbsp;&nbsp;AppearanceAssetElement&nbsp;assetElem,
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;new_keyword&nbsp;)
{
&nbsp;&nbsp;Document&nbsp;doc&nbsp;=&nbsp;assetElem.Document;

&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;Transaction&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Transaction(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Transaction&nbsp;Set&nbsp;Keyword&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;AppearanceAssetEditScope&nbsp;editScope&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;AppearanceAssetEditScope(&nbsp;assetElem.Document&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Asset&nbsp;editableAsset&nbsp;=&nbsp;editScope.Start(&nbsp;assetElem.Id&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;parameter&nbsp;=&nbsp;editableAsset.FindByName(&nbsp;<span style="color:#a31515;">&quot;keyword&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;parameter&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AssetPropertyString&nbsp;propKeyword&nbsp;=&nbsp;parameter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;AssetPropertyString;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;propKeyword&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">string</span>.IsNullOrEmpty(&nbsp;propKeyword.Value&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;propKeyword.Value&nbsp;=&nbsp;new_keyword;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!propKeyword.Value.Contains(&nbsp;new_keyword&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;val&nbsp;=&nbsp;propKeyword.Value&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;:&nbsp;&quot;</span>+&nbsp;new_keyword;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;propKeyword.Value&nbsp;=&nbsp;val;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;Exception&nbsp;ex&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.WriteLine(&nbsp;ex.Message&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;editScope.Commit(&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;}
}
</pre>

**Answer:** Yes, indeed, we already have a request *REVIT-171312* for this improvement.

The API does support modification of the `Keyword` property on an appearance asset.
You can set keyword under the `Appearance` tab in the material dialog through API.
You currently cannot set the `Keyword` property for `Structural` and `Thermal` through the API. 

After testing, I can reproduce this issue in Revit 2022.
It works in my internal development version, however.
I created an issue *REVIT-179045* to track why this fails in Revit 2022 and will evaluate it for an upcoming point release fix.
Here is our understanding of the current situation accessing the keyword property in the various tabs:

- For keywords under the `Identity` tab, which is the keyword for `Material`, there is currently no API exposed for either `get` or `set`.
- Keywords under `Appearance` can be both `get` and `set` using the code above.
- Keywords under `Thermal` and `Structural` can be read using `get`, as discriben
in [](https://thebuildingcoder.typepad.com/blog/2019/11/material-physical-and-thermal-assets.html).
`Set` is currently not supported for history reasons (cf. internal tickets *REVIT-171312* and *REVIT-170824*).

####<a name="3"></a> Modifying Group Requires Regen

Forgetting or not realising
the [need to regenerate](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33) and
accessing stale data can lead to pretty confusing and challenging problems.

As already noted in some previous examples, certain operations require more than just a call to `Regenerate`, e.g., starting and committing two or more separate transactions in a row.
Happily, in such cases, they can be assimilated into one single `TransactionGroup`, cf. the additional discussion
on [handling transactions and transaction groups](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.50).

It took a lengthy discussion with input from a couple of experienced add-in developers to clarify this need in the thread
on [modifying group in API results in duplicate group](https://forums.autodesk.com/t5/revit-api-forum/modifying-group-in-api-results-in-duplicate-group/m-p/10374430).

####<a name="4"></a> Modifying Part Material Requires Regen

- need to regen:
  Set Different Materials to Parts of a Wall
  https://forums.autodesk.com/t5/revit-api-forum/set-different-materials-to-parts-of-a-wall/m-p/10427403




####<a name="5"></a> Topo From Lines

[Harry Mattison](https://twitter.com/BoostYourBIM)'s new post
on [Boost Your BIM](https://boostyourbim.wordpress.com) presents
[Topo From Lines now in the free Revit tool set](https://boostyourbim.wordpress.com/2021/06/24/topo-from-lines-now-in-the-free-revit-tool-set):

> This tool is an "oldie but goodie" that lets you create and update a toposurface from a set of model lines.
A new version for 2022 was recently requested, so I added it to the Terrific Tool project and created a 2022 build.
You can download the current installer here and if you like code, that is [here](https://bitbucket.org/BoostYourBIM/boostyourbimterrifictools/src/master/BoostYourBIMTerrificTools/TopoFromLines.cs).

<center>
<img src="img/topofromlinesscreenshot.jpg" alt="Topo from lines" title="Topo from lines" width="327"/> <!-- 654 -->
</center>

Many thanks to Harry for implementing and sharing this and so many other useful tools!
