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

The Revit 2022 unit handling API and unit conversion, ForgeTypeId and FixtureUnit ParameterType in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://autode.sk/rvt2022unitapi

A few new aspects of the Revit 2022 unit handling API and <code>ForgeTypeId</code> usage
&ndash; FixtureUnit ParameterType
&ndash; Revit 2022 unit handling API in Dynamo
&ndash; String values for Forge units
&ndash; Unit conversion without knowing
&ndash; How will we live together?...

linkedin:

The Revit 2022 unit handling API and unit conversion, ForgeTypeId and FixtureUnit ParameterType in the #RevitAPI

http://autode.sk/rvt2022unitapi

A few new aspects of the Revit 2022 unit handling API and ForgeTypeId usage:

- FixtureUnit ParameterType
- Revit 2022 unit handling API in Dynamo
- String values for Forge units
- Unit conversion without knowing
- How will we live together?...

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

### ForgeTypeId and Units Revisited

We already discussed quite a few aspects of the Revit 2022 unit handling API and  `ForgeTypeId` usage:

<!-- 1835 1836 1853 1861 1871 1899 1900 1901 1902 1903 -->
<ul>
<!--
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/revitlookup-2021-with-multi-release-support.html">RevitLookup 2021 with Multi-Release Support</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/whats-new-in-the-revit-2021-api.html">What's New in the Revit 2021 API</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/07/virtual-au-and-aec-hackathon-units-and-das-job.html">Virtual AU and AEC Hackathon, Units and DAS Job</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/08/custom-parameters-and-tile-packing.html">Custom Parameters and Tile Packing</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/10/firerevit-deprecated-api-and-elbow-centre-point.html">FireRevit, Deprecated API and Elbow Centre Point</a></li>
-->
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-released.html">Revit 2022 released</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/pdf-export-forgetypeid-and-multi-target-add-in.html#2">Replacing deprecated ParameterType with ForgeTypeId
</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-migrates-bim360-team-to-docs.html">Removal of DisplayUnitType in RevitLookup 2022</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-sdk-and-the-building-coder-samples.html">ParameterType and ForgeTypeId in the Revit 2022 SDK and The Building Coder samples</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/whats-new-in-the-revit-2022-api.html">What's New in the Revit 2022 API</a></li>
</ul>

Here are some more related questions that came up since then:

- [`FixtureUnit` ParameterType](#2)
- [Revit 2022 unit handling API in Dynamo](#3)
- [String values for Forge units](#4)
- [Unit conversion without knowing](#5)
- [How will we live together?](#6)

<center>
<img src="img/yardstick.jpg" alt="Yardstick" title="Yardstick" width="480"/> <!-- 960 -->
</center>

Before diving into this topic, let us congratulate the China team in their celebration of the [Dragon Boat festival](https://en.wikipedia.org/wiki/Dragon_Boat_Festival) today.

- Traditional sport : dragon boating race
- Customary food: Zongzi, a kind of rice dumpling, packaged in bamboo or reed leaves, sweet or salty, even in ice cream (by Starbucks &nbsp; :-)
- Memorials on the sage [Qu Yuan](https://en.wikipedia.org/wiki/Qu_Yuan), poet and politician, known for his patriotism
- Prayer for health and peace by hanging calamus or mugwort on the door
 
<center>
<img src="img/dragon_boat_festival.png" alt="Dragon Boat festival" title="Dragon Boat festival" width="600"/> <!-- 1920 -->
</center>


####<a name="2"></a> FixtureUnit ParameterType

David Becroft comes to the rescue again answering 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on how to [convert `ParameterType.FixtureUnit` to `ForgeTypeId`](https://forums.autodesk.com/t5/revit-api-forum/convert-parametertype-fixtureunit-to-forgetypeid/m-p/10268488)

**Question:** I am trying to port some legacy code to Revit 2022 and I am running into the problem converting some `ParameterType` enum values to a `ForgeTypeId`.

`FixtureUnit` and `Invalid` (the result for `ElementId` parameters) are the most pressing ones.

There does not seem to be anything Fixture or Piping related in the `SpecTypeId` class that may apply.
`PipeDimension` comes closest but is not, I think, the same?
Plus, it also exists separately in the ParameterType enum which makes it unlikely that it got a double meaning in Revit 2022.

According to the release documentation there should be a deprecated method in the Parameter class that can help porting ParameterType enum values to ForgeTypeId objects, but Intellisense does not seem to be aware of the existence of such a method.

**Answer:** These methods are in `SpecUtils`, `GetSpecTypeId(ParameterType)` and `GetParameterType(ForgeTypeId)`.

In place of `ParameterType.FixtureUnit`, you can use `SpecTypeId.Number`.

`ParameterType.Invalid` is equivalent to an empty, default-constructed `ForgeTypeId`, i.e. `new ForgeTypeId()`.

####<a name="3"></a> Revit 2022 Unit Handling API in Dynamo

Konrad Sobon does a great job explaining how to deal with this in Dyname and Python in his discussion
of [handling the Revit 2022 unit changes](https://archi-lab.net/handling-the-revit-2022-unit-changes).

####<a name="4"></a> String Values for Forge Units

The unit handling changes also affected some Forge apps:

<center>
<img src="img/forge_type_id_schema_id.jpg" alt="RVT ForgeTypeId in Forge" title="RVT ForgeTypeId in Forge" width="800"/> <!-- 1038 -->
</center>

This was discussed and resolved in the StackOverflow question
on [Autodesk Forge returning odd measurement data](https://stackoverflow.com/questions/63992151/autodesk-forge-is-returning-odd-measurement-data)

I ended up implementing
the [method `ListForgeTypeIds` in The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/Util.cs#L1306-L1367) to help resolve the issue.

I also shared this in the discussion 
on [DisplayUnitType in Revit 2022](https://forums.autodesk.com/t5/revit-api-forum/displayunittype-in-revit-2022/m-p/10320697).

####<a name="5"></a> Unit Conversion Without Knowing

In a related vein, how can you implement a unit conversion without knowing the internal Revit unit?

This question has been around since the beginnings of The Building Coder and was first addressed in blog post #11
on [units](https://thebuildingcoder.typepad.com/blog/2008/09/units.html) in September 2008.

It came up again in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on a [unit conversion question](https://forums.autodesk.com/t5/revit-api-forum/unit-conversion-question/m-p/9840917):

Simply set the value that you are analysing to 1.0 manually through the user interface using your current display units.

Then, use RevitLookup to analyse what value `V` actually gets stored.

The ratio between those two values, i.e., `V`/1.0 == `V`, immediately provides the conversion factor from display unit to internal database unit (or v.v.).

####<a name="6"></a> How Will We Live Together?

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
