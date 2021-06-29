<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Asset Keyword Access
  https://app.slack.com/client/T02NW42JD/C0SR6NAP8
  Ryuji Ogasawara, RuoQian Lu and Joe Qiao 

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

Many thanks to  for this very helpful explanation!

-->

### Asset Keyword



<center>
<img src="img/" alt="" title="" width="100"/> <!-- 860 -->
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




####<a name="4"></a> 

**Response:**  


####<a name="5"></a> 




