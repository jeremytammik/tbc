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
  https://forums.autodesk.com/t5/revit-api-forum/analytical-node-vs-analytical-member-coordinate-accuracy-issue/m-p/11848971/highlight/false#M70197

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

###

####<a name="2"></a>

**Question:**

**Answer:**

**Response:**

<center>
<img src="img/.jpg" alt="" title="" width="100"/> <!-- Pixel Height: 240 Pixel Width: 300 -->
</center>


####<a name="3"></a>


####<a name="4"></a>

