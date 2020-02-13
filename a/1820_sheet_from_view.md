<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- get sheet from view 
  12966349 [Get ViewSheet from View]
  https://forums.autodesk.com/t5/revit-api-forum/get-viewsheet-from-view/m-p/7075550

twitter:

 the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Get Sheet from View and Title Block


#### <a name="2"></a>

- get sheet from view 
  12966349 [Get ViewSheet from View]
  https://forums.autodesk.com/t5/revit-api-forum/get-viewsheet-from-view/m-p/7075550



#### <a name="3"></a>Title Block Data Access

**Question:** I am searching for some sample code for use in a Design Automation API project.
 
Rather than reinvent to wheel, I thought you might have a snippet handy showing how to read Revit drawing title block attributes like these:

<center>
<img src="img/title_block_data_access.png" alt="Title block data access" title="Title block data access" width="600"/> <!-- 1390 -->
</center>

**Answer:** I discussed accessing
the [title block of a sheet](https://thebuildingcoder.typepad.com/blog/2009/11/title-block-of-sheet.html) myself
back in 2009, but that information is rather antiquated now.

The question was also raised and answered in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [TitleBlock](https://forums.autodesk.com/t5/revit-api-forum/titleblock/td-p/3802588),
and that information is perfectly valid.

Ah, I now found a more recent and useful article on how
to [determine sheet size](https://thebuildingcoder.typepad.com/blog/2010/05/determine-sheet-size.html) that
should provide all you need.

The code is included in
[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[module CmdSheetSize.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdSheetSize.cs).

The title block instances are family instance elements.
You can access the title block element using a filtered element collector, e.g.:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;title_block_instances
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_TitleBlocks&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;);
</pre>

You can loop through these elements and retrieve the required data from their built-in parameters, such as SHEET_NAME, SHEET_NUMBER, SHEET_DRAWN_BY, SHEET_CHECKED_BY etc., like this:

<pre class="code">
  <span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;e&nbsp;<span style="color:blue;">in</span>&nbsp;a&nbsp;)
  {
  &nbsp;&nbsp;p&nbsp;=&nbsp;e.get_Parameter(
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.SHEET_NUMBER&nbsp;);
   
  &nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;p,
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;valid&nbsp;sheet&nbsp;number&quot;</span>&nbsp;);
   
  &nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;sheet_number&nbsp;=&nbsp;p.AsString();
   
  &nbsp;&nbsp;p&nbsp;=&nbsp;e.get_Parameter(
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.SHEET_WIDTH&nbsp;);
   
  &nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;p,
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;valid&nbsp;sheet&nbsp;width&quot;</span>&nbsp;);
   
  &nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;swidth&nbsp;=&nbsp;p.AsValueString();
  &nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;width&nbsp;=&nbsp;p.AsDouble();

    . . . 
  }
</pre>

Als always, you can use [RevitLookup](https://github.com/jeremytammik/RevitLookup) to explore this data interactively yourself in your own model to see which properties are available where and what other title block information may be of interest to your application.

