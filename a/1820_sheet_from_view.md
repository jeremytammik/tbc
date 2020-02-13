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

Getting title block data and ViewSheet from View in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/titleblockdata

Today, let's highlight two view related data access topics
&ndash; Get ViewSheet from View
&ndash; Title block data access...

linkedin:

Getting title block data and ViewSheet from View in the #RevitAPI

http://bit.ly/titleblockdata

Today, let's highlight two view related data access topics:

- Get ViewSheet from View
- Title block data access...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Getting Title Block Data and ViewSheet from View

Today, let's highlight two view related data access topics:

- [Get ViewSheet from View](#2)
- [Title block data access](#3)

#### <a name="2"></a>Get ViewSheet from View

An interesting in-depth conversation in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on how to [get ViewSheet from View](https://forums.autodesk.com/t5/revit-api-forum/get-viewsheet-from-view/m-p/7075550) presents
two different approaches to determine relationships between sheets and the views they host:

**Question:** Is there a way to get the `ViewSheet` or `ElementId` of the `ViewSheet` from a `View` that is placed on that sheet?

I can get the sheet name and sheet number from the provided parameters but do not see one for the actual ViewSheet.
Am I missing something?
The only thing I can think of to do without this info is to loop through all ViewSheets to match the one with the same sheet number (since sheet number is required to be unique).   

**Answer:** Have you looked at the sample code provided in the [description of the ViewSheet class](http://www.revitapidocs.com/2017/af2ee879-173d-df3a-9793-8d5750a17b49.htm)?

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;views&nbsp;=&nbsp;viewSheet.GetAllPlacedViews();
&nbsp;&nbsp;message&nbsp;+=&nbsp;<span style="color:#a31515;">&quot;\nNumber&nbsp;of&nbsp;views&nbsp;in&nbsp;the&nbsp;sheet&nbsp;:&nbsp;&quot;</span>&nbsp;+&nbsp;views.Count;
</pre>

If all else fails, you could use this relationship and invert it, just as you suggest.
 
The [View.Title property](http://www.revitapidocs.com/2017/bfa96650-310e-5385-3a9d-1a1248b623ce.htm) also
generally consists of the view name plus other modifiers, such as the view type, sheet number, area scheme, and/or assembly type, depending on the specifics of the view:
 
**Response:** You must have misunderstood my question.  I don't have the ViewSheet in order to use the method to get its placed views.  I have a view, that I know is on a sheet (because its sheet number parameter is not null), but I want to get a direct reference to that sheet that it is placed on.  It seems to me that you are still suggesting that I must loop through all ViewSheets to get the View that I'm concerned with.  Thanks.

**Answer:** Yes, indeed, that is exactly what I am suggesting.

Get all the view sheets, keep track of them and the views they host, and invert that relationship, as described in one of the very early discussions by The Building Coder back in 2008 on a [relationship inverter](http://thebuildingcoder.typepad.com/blog/2008/10/relationship-in.html).

That gives you a complete dictionary lookup both ways: you can look up the sheet hosting any view and you can look up all the views hosted by any sheet, instantaneously.

It takes a moment to set up the relationships; after that, all data is available and lookup is very fast.

**Other answer:** I maybe have a workaround for this question, something similar I assume.

Select some views in the Project Browser to know if the views are placed on sheet(s).

Then, run a macro or external command to execute something like code below.

If the view is placed on a sheet, it shows the sheet number, sheet name and whatever data you are interested in from the ViewSheet referenced by the views selected in the Project Browser:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;GetViewSheetFromView(&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;data&nbsp;=&nbsp;<span style="color:#a31515;">&quot;&quot;</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;selectedIds&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;uidoc.Selection.GetElementIds();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;selectedid&nbsp;<span style="color:blue;">in</span>&nbsp;selectedIds&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;selectedid&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">View</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">View</span>&nbsp;v&nbsp;<span style="color:blue;">in</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">View</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">View</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;q&nbsp;=&gt;&nbsp;q.Id.Equals(&nbsp;e.Id&nbsp;)&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;thisSheet&nbsp;=&nbsp;<span style="color:#a31515;">&quot;&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">ViewSheet</span>&nbsp;vs&nbsp;<span style="color:blue;">in</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">ViewSheet</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">ViewSheet</span>&gt;()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;eid&nbsp;<span style="color:blue;">in</span>&nbsp;vs.GetAllPlacedViews()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;ev&nbsp;=&nbsp;doc.GetElement(&nbsp;eid&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">View</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ev.Id&nbsp;==&nbsp;v.Id&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;thisSheet&nbsp;+=&nbsp;vs.SheetNumber&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;-&nbsp;&quot;</span>&nbsp;+&nbsp;vs.Name&nbsp;+&nbsp;<span style="color:#2b91af;">Environment</span>.NewLine;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;thisSheet&nbsp;!=&nbsp;<span style="color:#a31515;">&quot;&quot;</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data&nbsp;+=&nbsp;v.ViewType&nbsp;+&nbsp;<span style="color:#a31515;">&quot;:&nbsp;&quot;</span>&nbsp;+&nbsp;v.Name&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;&quot;</span>&nbsp;+&nbsp;<span style="color:#2b91af;">Environment</span>.NewLine&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;thisSheet.TrimEnd(&nbsp;<span style="color:#a31515;">&#39;&nbsp;&#39;</span>,&nbsp;<span style="color:#a31515;">&#39;,&#39;</span>&nbsp;)&nbsp;+&nbsp;<span style="color:#2b91af;">Environment</span>.NewLine;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data&nbsp;+=&nbsp;v.ViewType&nbsp;+&nbsp;<span style="color:#a31515;">&quot;:&nbsp;&quot;</span>&nbsp;+&nbsp;v.Name&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;&quot;</span>&nbsp;+&nbsp;<span style="color:#2b91af;">Environment</span>.NewLine&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;thisSheet.TrimEnd(&nbsp;<span style="color:#a31515;">&#39;&nbsp;&#39;</span>,&nbsp;<span style="color:#a31515;">&#39;,&#39;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data&nbsp;+=&nbsp;<span style="color:#a31515;">&quot;&nbsp;NOT&nbsp;ON&nbsp;SHEET&nbsp;&quot;</span>&nbsp;+&nbsp;<span style="color:#2b91af;">Environment</span>.NewLine&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\n&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;View&nbsp;Report&quot;</span>,&nbsp;data&nbsp;);
&nbsp;&nbsp;}
</pre>

<center>
<img src="img/GetViewSheetFromView.png" alt="Get ViewSheet from View" title="Get ViewSheet from View" width="800"/> <!-- 1815 -->
</center>

**Yet another answer:** You can perhaps get VIEWPORT_SHEET_NUMBER from Parameters.

**Yet another answer:** As suggested above, using the BIP for viewport sheet number with an ElementParameterFilter is probably the best approach.
Sheet numbers are unique in each Revit model, so it is safe to search by them and get the right result.

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;TObj43(<span style="color:blue;">ByVal</span>&nbsp;commandData&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ByRef</span>&nbsp;message&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>,&nbsp;<span style="color:blue;">ByVal</span>&nbsp;elements&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">ElementSet</span>)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Result</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;commandData.Application.ActiveUIDocument&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Cancelled&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;AcView&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">View</span>&nbsp;=&nbsp;commandData.Application.ActiveUIDocument.ActiveGraphicalView
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;P_Ns&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;=&nbsp;AcView.Parameter(<span style="color:#2b91af;">BuiltInParameter</span>.VIEWPORT_SHEET_NUMBER)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;P_Ns&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">GoTo</span>&nbsp;Monday
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Txt&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>&nbsp;=&nbsp;P_Ns.AsString
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;<span style="color:blue;">String</span>.IsNullOrEmpty(Txt)&nbsp;<span style="color:blue;">Then</span>&nbsp;<span style="color:blue;">GoTo</span>&nbsp;Monday&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;SeFR&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">FilterRule</span>&nbsp;=&nbsp;<span style="color:#2b91af;">ParameterFilterRuleFactory</span>.CreateEqualsRule(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(<span style="color:#2b91af;">BuiltInParameter</span>.SHEET_NUMBER),&nbsp;Txt,&nbsp;<span style="color:blue;">True</span>)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;PFilt&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(SeFR,&nbsp;<span style="color:blue;">False</span>)
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;FEC&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(AcView.Document)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;ECF&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:#2b91af;">ElementClassFilter</span>(<span style="color:blue;">GetType</span>(<span style="color:#2b91af;">ViewSheet</span>))
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;LandF&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:#2b91af;">LogicalAndFilter</span>(ECF,&nbsp;PFilt)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Els&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">List</span>(<span style="color:blue;">Of</span>&nbsp;<span style="color:#2b91af;">Element</span>)&nbsp;=&nbsp;FEC.WherePasses(LandF).ToElements
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;Els.Count&nbsp;&lt;&gt;&nbsp;1&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">GoTo</span>&nbsp;Monday
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;TD&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:#2b91af;">TaskDialog</span>(<span style="color:#a31515;">&quot;Was&nbsp;this&nbsp;your&nbsp;sheet...&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TD.MainInstruction&nbsp;=&nbsp;Txt&nbsp;&amp;&nbsp;<span style="color:#a31515;">&quot;-&quot;</span>&nbsp;&amp;&nbsp;Els(0).Name
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TD.MainContent&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Note&nbsp;that&nbsp;view&nbsp;types&nbsp;that&nbsp;can&nbsp;appear&nbsp;on&nbsp;&quot;</span>&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;multiple&nbsp;sheets&nbsp;(Legends,&nbsp;Images,&nbsp;Schedules&nbsp;etc.)&nbsp;will&nbsp;&quot;</span>&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;not&nbsp;be&nbsp;returned&nbsp;by&nbsp;this&nbsp;method.&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TD.Show()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">GoTo</span>&nbsp;Friday
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
Monday:
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(<span style="color:#a31515;">&quot;Something&nbsp;went&nbsp;amiss...&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;We&nbsp;looked&nbsp;hard&nbsp;in&nbsp;many&nbsp;places&nbsp;but&nbsp;were&nbsp;unable&nbsp;to&nbsp;&quot;</span>&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;find&nbsp;your&nbsp;sheet&nbsp;on&nbsp;this&nbsp;occasion.&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Failed
Friday:
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
</pre>

**Yet another answer:** Here is a pretty quick way of checking for a specific sheet:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:#2b91af;">ViewSheet</span>&nbsp;CheckSheet(&nbsp;<span style="color:blue;">string</span>&nbsp;_sheetNumber&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ParameterValueProvider</span>&nbsp;pvp&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ParameterValueProvider</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.SHEET_NUMBER&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilterStringRuleEvaluator</span>&nbsp;fsr&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterStringEquals</span>();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FilterRule</span>&nbsp;fRule&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterStringRule</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pvp,&nbsp;fsr,&nbsp;_sheetNumber,&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>&nbsp;filter&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(&nbsp;fRule&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Sheets&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;filter&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault()&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">ViewSheet</span>&nbsp;vs&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;vs;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

**Summary:** I believe this last solution is unbeatable if you are interested in one single lookup.

It uses a parameter filter and the `SHEET_NUMBER` built-in parameter.

Probably, the VB function `TObj43` above is similarly efficient.

`GetViewSheetFromView` demonstrates the lookup of the inverted relationship I described, but just for one single view.

That code could be used to store the entire relationships mapping sheet to hosted views and the inverted one mapping each view to hosting sheets for all views and sheets. That might possibly be more efficient if you frequently need to look up several different view to sheet relationships.

Thank you all for the very illuminating and helpful answers!

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

