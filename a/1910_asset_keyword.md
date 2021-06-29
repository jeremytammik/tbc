<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


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


####<a name="2"></a> 

**Question:**

I have an issue setting a string value to the material appearance asset keyword property.
In one material, it can be set as expected, but another material returns an error saying, "The input value is invalid for this AssetPropertyString property.\r\nParameter name: value".
I found the blog article and JIRA ticket REVIT-170824 which explains that the keyword property on the Identity tab is not exposed yet.
https://thebuildingcoder.typepad.com/blog/2019/11/material-physical-and-thermal-assets.html#4
https://jira.autodesk.com/browse/REVIT-170824
I expect the "keyword" property on the appearance tab to accept a string value.
In addition, I can see some error message in the journal file.
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


**Answer:**

is it possible to get more custom user cases for this API? Or more comments/quotes from users? Looks like this is a valuable requirement and I would like to get more custom inputs to help us prioritize it. Thanks! (edited) 

RuoQian Lu  4 days ago
We have already had a jira https://jira.autodesk.com/browse/REVIT-171312 for this improvement.

Ryuji Ogasawara  3 days ago
Hi @RuoQian Lu,
Do you mean that modifying Keyword property on Appearance Asset through api is not supported yet?
If so, I will ask the customer to provide use case for the api.
BTW, I received the reproducible sample project from the customer.
I uploaded it and  Visual Studio project to the shared folder.
The Revit project created in Japanese, so material name has some Japanese text.
https://myshare.autodesk.com/:f:/g/personal/ryuji_ogasawara_autodesk_com/Eo2_QtCpI19Fv10JSKtzBzABtLS_jI3eI6fp1Ms4_EzruQ?e=x9tkcO (edited) 

RuoQian Lu  1 day ago
hi @Ryuji Ogasawara, thanks for the case. It is still upgrading on my machine so I need to take more time for it.
For modifying Keyword property on Appearance Asset through api, it is supported. Users can set keyword under Appearance tab in material dialog through API.
The Keyword property for Structural and Thermal have problems to set through API. Sorry for misleading you.
I will do more test and try then let you know later.

Jeremy Tammik  20 hours ago
I would also be interested in the final result. Especially two clear Yes/No answers: (i) Can the keyword property be set manually through the UI?  (ii) Can the keyword property be set programmatically through the API? Preferably with exact steps and/or sample code demonstrating both procedures in a fool-proof manner. Thank you!

RuoQian Lu  2 hours ago
hi @Ryuji Ogasawara, I can reproduce this issue on 2022. But on 2023 it works. I've created a jira https://jira.autodesk.com/browse/REVIT-179045 to track why this fails on 2022 and will evaluate it for 2022 point release fix.
Hi @Jeremy Tammik, I worked with @Joe Qiao for your question and here is our understanding after checking code.
For Keywords under Identity tab, which is the keyword for Material, it is not able to Get nor Set. There is no API exposed for it yet.
For Keywords under Appearance, it can be Get and Set. You can refer to the code Ryuji post here.
For Keywords under Thermal and Structural, we didn't test but it should be able to Get (you've confirmed that in your blog) but not able to Set (as there are some history reasons). Refer to https://jira.autodesk.com/browse/REVIT-171312 and https://jira.autodesk.com/browse/REVIT-170824




####<a name="4"></a> 

**Response:**  


####<a name="5"></a> 




