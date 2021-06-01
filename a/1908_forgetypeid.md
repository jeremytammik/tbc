<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- ForgeTypeId
  Jacob Small, Autodesk
  https://forum.dynamobim.com/t/wall-finish-by-room/13215/34
  <div class="cooked"><p>This is the unit type change associated to the forge type ID.</p>
  <p><a class="mention" href="/u/konrad_k_sobon">@Konrad_K_Sobon</a>  did a good job highlighting some of that here: <a href="https://archi-lab.net/handling-the-revit-2022-unit-changes/">https://archi-lab.net/handling-the-revit-2022-unit-changes/</a>.</p>
  <p>There is also some good summary discussion on this building coder post from <a class="mention" href="/u/jeremytammik">@jeremytammik</a>: <a href="https://thebuildingcoder.typepad.com/blog/2021/04/pdf-export-forgetypeid-and-multi-target-add-in.html" class="inline-onebox">The Building Coder: PDF Export, ForgeTypeId and Multi-Target Add-In <span class="badge badge-notification clicks" title="1 click">1</span></a></p>
  <p>And finally, there is the API documentation includes the new class here: <a href="https://www.revitapidocs.com/2022/d9fcf276-9566-de83-2b0b-d89b65ccc8af.htm" class="inline-onebox">ForgeTypeId Class</a></p>
  <p>This isn’t intended to be a ‘fix’, but more giving you the opportunity to fix the issue yourself first - it’s not super easy, but it’s also not so difficult to do that you shouldn’t be able to make some headway and get better with Python. Should you give it a shot and still be stuck, post the DYN and a really small sample Revit model and the larger community will likely help out. <img src="https://emoji.discourse-cdn.com/twitter/slight_smile.png?v=9" title=":slight_smile:" class="emoji" alt=":slight_smile:"></p></div>

- Convert ParameterType.FixtureUnit to ForgeTypeId
  Revit 2022: ParameterType.Text to ForgeTypeId

- forge_type_id_schema_id.jpg
  Autodesk Forge is returning odd measurement data
  https://stackoverflow.com/questions/63992151/autodesk-forge-is-returning-odd-measurement-data
  email with cyrille

- 7876 [Autodesk Forge measurement units]
  2021.0.150.9 implemented ListForgeTypeIds

- unit conversion without knowing the internal unit
  Unit Conversion Question
  https://forums.autodesk.com/t5/revit-api-forum/unit-conversion-question/m-p/9840917

- DisplayUnitType
  forgetypeid
  Revit 2021 DisplayUnitType
  https://forums.autodesk.com/t5/revit-api-forum/revit-2021-displayunittype/m-p/9793861
  ForgeTypeId how to use?
  https://forums.autodesk.com/t5/revit-api-forum/forgetypeid-how-to-use/m-p/9455305
  UnitTypeId
  SpecTypeId
  https://autodesk.slack.com/archives/C0U4RCJ1M/p1610539323105400?thread_ts=1610484540.104000&cid=C0U4RCJ1M

- At [LA BIENNALE DI VENEZIA](https://www.labiennale.org),
  the [17th International Architecture Exhibition](https://www.labiennale.org/en/architecture/2021)
  is focussed on the theme of *HOW WILL WE LIVE TOGETHER?*
  > We need a new spatial contract.
  In the context of widening political divides and growing economic inequalities, we call on architects to imagine spaces in which we can generously live together.
  At the Biennale di Venezia, the
  [German Pavilion looks back from the future](https://www.floornature.com/blog/biennale-di-venezia-german-pavilion-looks-back-future-16269):
  > [2038](https://2038.xyz) is the name of the German Pavilion in the 17th International Architecture Exhibition at Biennale di Venezia.
  A look back from the future, which the curators envision as a world where many of today’s problems have been overcome.
  Digital technology makes it possible to visit the pavilion from anywhere in the world: [2038.xyz](https://2038.xyz)

twitter:

add #thebuildingcoder

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### ForgeTypeId

####<a name="2"></a> 


<!-- 1835 1836 1853 1861 1871 1899 1900 1901 1902 1903 -->
<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/revitlookup-2021-with-multi-release-support.html">RevitLookup 2021 with Multi-Release Support</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/whats-new-in-the-revit-2021-api.html">What's New in the Revit 2021 API</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/07/virtual-au-and-aec-hackathon-units-and-das-job.html">Virtual AU and AEC Hackathon, Units and DAS Job</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/08/custom-parameters-and-tile-packing.html">Custom Parameters and Tile Packing</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/10/firerevit-deprecated-api-and-elbow-centre-point.html">FireRevit, Deprecated API and Elbow Centre Point</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-released.html">Revit 2022 Released</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/pdf-export-forgetypeid-and-multi-target-add-in.html">PDF Export, ForgeTypeId and Multi-Target Add-In</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-migrates-bim360-team-to-docs.html">Revit 2022 Migrates BIM360 Team to Docs</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-sdk-and-the-building-coder-samples.html">Revit 2022 SDK and The Building Coder Samples</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/whats-new-in-the-revit-2022-api.html">What's New in the Revit 2022 API</a></li>
</ul>

ForgeTypeId
Jacob Small, Autodesk
https://forum.dynamobim.com/t/wall-finish-by-room/13215/34
<div class="cooked"><p>This is the unit type change associated to the forge type ID.</p>
<p><a class="mention" href="/u/konrad_k_sobon">@Konrad_K_Sobon</a>  did a good job highlighting some of that here: <a href="https://archi-lab.net/handling-the-revit-2022-unit-changes/">https://archi-lab.net/handling-the-revit-2022-unit-changes/</a>.</p>
<p>There is also some good summary discussion on this building coder post from <a class="mention" href="/u/jeremytammik">@jeremytammik</a>: <a href="https://thebuildingcoder.typepad.com/blog/2021/04/pdf-export-forgetypeid-and-multi-target-add-in.html" class="inline-onebox">The Building Coder: PDF Export, ForgeTypeId and Multi-Target Add-In <span class="badge badge-notification clicks" title="1 click">1</span></a></p>
<p>And finally, there is the API documentation includes the new class here: <a href="https://www.revitapidocs.com/2022/d9fcf276-9566-de83-2b0b-d89b65ccc8af.htm" class="inline-onebox">ForgeTypeId Class</a></p>
<p>This isn’t intended to be a ‘fix’, but more giving you the opportunity to fix the issue yourself first - it’s not super easy, but it’s also not so difficult to do that you shouldn’t be able to make some headway and get better with Python. Should you give it a shot and still be stuck, post the DYN and a really small sample Revit model and the larger community will likely help out. <img src="https://emoji.discourse-cdn.com/twitter/slight_smile.png?v=9" title=":slight_smile:" class="emoji" alt=":slight_smile:"></p></div>

Convert ParameterType.FixtureUnit to ForgeTypeId
Revit 2022: ParameterType.Text to ForgeTypeId

forge_type_id_schema_id.jpg
Autodesk Forge is returning odd measurement data
https://stackoverflow.com/questions/63992151/autodesk-forge-is-returning-odd-measurement-data
email with cyrille

7876 [Autodesk Forge measurement units]
2021.0.150.9 implemented ListForgeTypeIds

unit conversion without knowing the internal unit
Unit Conversion Question
https://forums.autodesk.com/t5/revit-api-forum/unit-conversion-question/m-p/9840917

DisplayUnitType
forgetypeid
Revit 2021 DisplayUnitType
https://forums.autodesk.com/t5/revit-api-forum/revit-2021-displayunittype/m-p/9793861
ForgeTypeId how to use?
https://forums.autodesk.com/t5/revit-api-forum/forgetypeid-how-to-use/m-p/9455305
UnitTypeId
SpecTypeId
https://autodesk.slack.com/archives/C0U4RCJ1M/p1610539323105400?thread_ts=1610484540.104000&cid=C0U4RCJ1M


**Question:** 

<center>
<img src="img/.png" alt="" title="" width="800"/> <!-- 1401 -->
</center>


<pre class="code">

</pre>



**Answer:** 

Many thanks to  for this very helpful explanation!

####<a name="3"></a> 

####<a name="4"></a> 


####<a name="4"></a> How Will We Live Together?

Moving from Revit to the topic of architecture in general, 
at [La Biennale di Venezia](https://www.labiennale.org),
the [17th International Architecture Exhibition](https://www.labiennale.org/en/architecture/2021)
is focussed on the theme of *How will we live together?*

> We need a new spatial contract.
In the context of widening political divides and growing economic inequalities, we call on architects to imagine spaces in which we can generously live together.

At the biennale,
the [German pavilion looks back from the future](https://www.floornature.com/blog/biennale-di-venezia-german-pavilion-looks-back-future-16269):

> [2038](https://2038.xyz) is the name of the German Pavilion in the 17th International Architecture Exhibition at Biennale di Venezia.
A look back from the future, which the curators envision as a world where many of today’s problems have been overcome.
Digital technology makes it possible to visit the pavilion from anywhere in the world: [2038.xyz](https://2038.xyz)
