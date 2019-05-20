<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

</head>

<!---

- filtering benchmark
  https://thebuildingcoder.typepad.com/blog/2019/04/slow-slower-still-and-faster-filtering.html#comment-4421443231

twitter:

Forge learning resource and filtered element collector benchmark for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/filterbenchmark

Today, let's present a benchmark monitoring filtered element collector performance.
First, however, a quick note on a very useful Forge learning resource
&ndash; Forge learning resource
&ndash; Filtered element collector benchmark...

linkedin:

Forge learning resource and filtered element collector benchmark for the #RevitAPI

http://bit.ly/filterbenchmark

Today, let's present a benchmark monitoring filtered element collector performance.

First, however, a quick note on a very useful Forge learning resource:

- Forge learning resource
- Filtered element collector benchmark...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

-->

### Filtered Element Collector Benchmark

Today, let's present a benchmark monitoring filtered element collector performance.

First, however, a quick note on a very useful Forge learning resource:

- [Forge learning resource](#2) 
- [Filtered element collector benchmark](#3) 

<center>
<img src="img/stopwatch.png" alt="Stopwatch" width="450">
</center>

####<a name="2"></a> Forge Learning Resource

If you are new to Forge or want to dive in deeper, you can find a collection of very cool Forge training material
at [learnforge.autodesk.io](https://learnforge.autodesk.io), focusing specifically on BIM360 and design automation.

Here is the table of contents:

- Before you start coding
- Tools
- OAuth
- View your models
    - Create a server
    - Authenticate
    - Upload file to OSS
    - Translate the file
    - Show on Viewer
- View BIM 360 & Fusion models
    - Create a server
    - Authorize
    - List hubs & projects
    - User information
    - Show on Viewer
- Modify your models
    - Create a server
    - Basic app UI
    - Prepare a plugin
    - Define an activity


####<a name="3"></a> Filtered Element Collector Benchmark

Back to the Revit API, I recently reiterated the differences between [slow, slower still and faster filtering](https://thebuildingcoder.typepad.com/blog/2019/04/slow-slower-still-and-faster-filtering.html).

In the end, the only way to tell whether your filter is performing well or not is to implement
some [benchmarking](https://en.wikipedia.org/wiki/Benchmark_(computing)) for it.

Jai Hari Hara Sudhan very commendably did so, documenting his progress and sharing his results in
a [series of](https://thebuildingcoder.typepad.com/blog/2019/04/slow-slower-still-and-faster-filtering.html#comment-4421084673)
[comments on](https://thebuildingcoder.typepad.com/blog/2019/04/slow-slower-still-and-faster-filtering.html#comment-4421183783)
[that post](https://thebuildingcoder.typepad.com/blog/2019/04/slow-slower-still-and-faster-filtering.html#comment-4421443231) and in
his [API speed test screencast](https://knowledge.autodesk.com/community/screencast/99858ef7-c4c8-4599-ba6d-0394ff830d62) demonstrating
the benchmark running live.

<!-- https://autode.sk/2IwemGr -->

Here is a summary of our discussion and his final benchmarking code:

**Question:** The above content is very useful.

I am using three methods to filter and select the element:

1. Using `FilterRule` method (filters in floor by floor / level)
2. Using `Factory` Method (filters in the projects)
3. Selection with interface (element by element)

Question 1. Which one is the best in performance (quick)? `FilterRule` or `Factory` method?

Question 2. In the FamilySelectionFilter method, is there any better or more performant method to select the elements?

**Answer:** Nobody can tell you beforehand how these different approaches will perform in your specific context.

I therefore suggest that you benchmark them yourself and let us know the result.

[The Building Coder topic groups](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5) lists
several benchmarking examples that you can look at to see how.


**Response:** I implemented a benchmark.

It is impossible to determine exact constant performance (time), because the results differ from run to run. 

Please refer to my [API speed test screencast](https://knowledge.autodesk.com/community/screencast/99858ef7-c4c8-4599-ba6d-0394ff830d62). <!-- https://autode.sk/2IwemGr -->

Finally, I rank the different approaches as follows:

<!--
<ul style="list-style-type: none">
<li>1st &ndash; `Factory` method (average time = 766.1 microseconds) 100% </li>
<li>2nd &ndash; `FilterRule` method (average time = 889.0 microseconds) &ndash; 116% slower than `Factory`</li>
<li>3rd &ndash; `Linq2` method (average time = 983.5 microseconds) &ndash; 128% slower than `Factory`</li>
<li>4th &ndash; `Linq1` method (average time = 1173.3 microseconds) &ndash; 153% slower than `Factory`</li>
</ul>
-->

<ol>
<li>`Factory` method (average time = 766.1 microseconds) <br/>&ndash; 100% </li>
<li>`FilterRule` method (average time = 889.0 microseconds) <br/>&ndash; 116% slower than `Factory`</li>
<li>`Linq2` method (average time = 983.5 microseconds) <br/>&ndash; 128% slower than `Factory`</li>
<li>`Linq1` method (average time = 1173.3 microseconds) <br/>&ndash; 153% slower than `Factory`</li>
</ol>

The code as follows:

<pre class="code">
[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">Elec_test</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;uiapp.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;app&nbsp;=&nbsp;uiapp.Application;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Selection</span>&nbsp;sel&nbsp;=&nbsp;uidoc.Selection;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;BuilDTecH&nbsp;Architects&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;BuilDTecH&nbsp;Architects&nbsp;by&nbsp;Sudhan&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">InputData</span>&nbsp;InputData&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">InputData</span>();
&nbsp;&nbsp;&nbsp;&nbsp;InputData.ShowDialog();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:blue;">object</span>&gt;&nbsp;data&nbsp;=&nbsp;InputData.Data;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">///////////////////////////////&nbsp;Input&nbsp;Values</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;LeftOffset&nbsp;=&nbsp;<span style="color:#2b91af;">Convert</span>.ToDouble(&nbsp;data[1]&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;RightOffset&nbsp;=&nbsp;<span style="color:#2b91af;">Convert</span>.ToDouble(&nbsp;data[2]&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;TopOffset&nbsp;=&nbsp;<span style="color:#2b91af;">Convert</span>.ToDouble(&nbsp;data[3]&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;BottomOffset&nbsp;=&nbsp;<span style="color:#2b91af;">Convert</span>.ToDouble(&nbsp;data[4]&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;NearClipOffset&nbsp;=&nbsp;-<span style="color:#2b91af;">Convert</span>.ToDouble(&nbsp;data[5]&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;FarClipOffset&nbsp;=&nbsp;<span style="color:#2b91af;">Convert</span>.ToDouble(&nbsp;data[6]&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;Section_Name&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Electrical&nbsp;GangBox&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;ElectricalEquipment&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Modular&nbsp;Gang&nbsp;Box&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//Input&nbsp;///////////////////////////////&nbsp;Input&nbsp;Values</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;data[0].Equals(&nbsp;<span style="color:#2b91af;">InputData</span>.<span style="color:#2b91af;">Option</span>.ByFloor&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Timer</span>&nbsp;floortimeLinq1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Timer</span>();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floortimeLinq1.Start();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;elems&nbsp;=&nbsp;Linq1(&nbsp;doc,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_ElectricalEquipment,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElectricalEquipment&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floortimeLinq1.Stop();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;time&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;LINQ1&nbsp;Method&nbsp;Time&nbsp;=&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;floortimeLinq1.Duration.ToString()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;No.&nbsp;of&nbsp;Elements&nbsp;=&nbsp;&quot;</span>&nbsp;+&nbsp;elems.Count().ToString()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;elems&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Timer</span>&nbsp;floortimeLinq2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Timer</span>();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floortimeLinq2.Start();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;elems&nbsp;=&nbsp;Linq2(&nbsp;doc,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_ElectricalEquipment,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElectricalEquipment&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floortimeLinq2.Stop();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;time&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;LINQ2&nbsp;Method&nbsp;Time&nbsp;=&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;floortimeLinq2.Duration.ToString()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;No.&nbsp;of&nbsp;Elements&nbsp;=&nbsp;&quot;</span>&nbsp;+&nbsp;elems.Count().ToString()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;elems&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Timer</span>&nbsp;floortimeFilterRule&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Timer</span>();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floortimeFilterRule.Start();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;elems&nbsp;=&nbsp;FilterRule(&nbsp;doc,&nbsp;<span style="color:green;">//&nbsp;uidoc.ActiveView.Id,</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_ElectricalEquipment,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElectricalEquipment&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floortimeFilterRule.Stop();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;time&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Filter&nbsp;Rule&nbsp;Method&nbsp;Time&nbsp;=&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;floortimeFilterRule.Duration.ToString()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;No.&nbsp;of&nbsp;Elements&nbsp;=&nbsp;&quot;</span>&nbsp;+&nbsp;elems.Count().ToString()&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;elems&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Timer</span>&nbsp;floortimeFactoryRule&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Timer</span>();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floortimeFactoryRule.Start();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;elems&nbsp;=&nbsp;Factory(&nbsp;doc,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_ElectricalEquipment,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElectricalEquipment&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floortimeFactoryRule.Stop();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;time&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;&nbsp;Factory&nbsp;Rule&nbsp;Method&nbsp;Time&nbsp;=&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;floortimeFactoryRule.Duration.ToString()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;No.&nbsp;of&nbsp;Elements&nbsp;=&nbsp;&quot;</span>&nbsp;+&nbsp;elems.Count().ToString()&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>&nbsp;<span style="color:blue;">if</span>(&nbsp;data[0].Equals(&nbsp;<span style="color:#2b91af;">InputData</span>.<span style="color:#2b91af;">Option</span>.BySingle&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>&nbsp;td&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TaskDialog</span>(&nbsp;<span style="color:#a31515;">&quot;Element&nbsp;By&nbsp;Element&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;td.Title&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Want&nbsp;to&nbsp;Continue&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;td.MainInstruction&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Do&nbsp;you&nbsp;want&nbsp;to&nbsp;create&nbsp;a&nbsp;new&nbsp;section&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;td.CommonButtons&nbsp;=&nbsp;<span style="color:#2b91af;">TaskDialogCommonButtons</span>.Yes
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;<span style="color:#2b91af;">TaskDialogCommonButtons</span>.No;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;td.DefaultButton&nbsp;=&nbsp;<span style="color:#2b91af;">TaskDialogResult</span>.Yes;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;next&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">while</span>(&nbsp;next&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ISelectionFilter</span>&nbsp;selFilter
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FamilySelectionFilter</span>(&nbsp;doc,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_ElectricalEquipment,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElectricalEquipment&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;refe&nbsp;=&nbsp;sel.PickObject(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ObjectType</span>.Element,&nbsp;selFilter,&nbsp;<span style="color:#a31515;">&quot;Select&nbsp;Object&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;ele&nbsp;=&nbsp;doc.GetElement(&nbsp;refe&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Draw_Section</span>.Draw(&nbsp;doc,&nbsp;ele,&nbsp;Section_Name,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;LeftOffset,&nbsp;RightOffset,&nbsp;TopOffset,&nbsp;BottomOffset,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NearClipOffset,&nbsp;FarClipOffset&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialogResult</span>&nbsp;tdRes&nbsp;=&nbsp;td.Show();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;tdRes&nbsp;==&nbsp;<span style="color:#2b91af;">TaskDialogResult</span>.No&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;next&nbsp;=&nbsp;<span style="color:blue;">false</span>;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>&nbsp;<span style="color:blue;">if</span>(&nbsp;data[0].Equals(&nbsp;<span style="color:#2b91af;">InputData</span>.<span style="color:#2b91af;">Option</span>.ByProject&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Timer</span>&nbsp;floortimeFactoryRule&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Timer</span>();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floortimeFactoryRule.Start();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;elems&nbsp;=&nbsp;Factory(&nbsp;doc,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_ElectricalEquipment,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElectricalEquipment&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;floortimeFactoryRule.Stop();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;time&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;&nbsp;Factory&nbsp;Rule&nbsp;Method&nbsp;Time&nbsp;=&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;floortimeFactoryRule.Duration.ToString()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;&nbsp;No.&nbsp;of&nbsp;Elements&nbsp;=&nbsp;&quot;</span>&nbsp;+&nbsp;elems.Count().ToString()&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;ele&nbsp;<span style="color:blue;">in</span>&nbsp;elems&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Draw_Section</span>.Draw(&nbsp;doc,&nbsp;ele,&nbsp;Section_Name,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;LeftOffset,&nbsp;RightOffset,&nbsp;TopOffset,&nbsp;BottomOffset,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NearClipOffset,&nbsp;FarClipOffset&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">FamilySelectionFilter</span>&nbsp;:&nbsp;<span style="color:#2b91af;">ISelectionFilter</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;Doc;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;FmlyName&nbsp;=&nbsp;<span style="color:#a31515;">&quot;&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;BultCatId;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;FamilySelectionFilter(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;BuiltInCat,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;familyTypeName&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Doc&nbsp;=&nbsp;doc;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FmlyName&nbsp;=&nbsp;familyTypeName;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BultCatId&nbsp;=&nbsp;(<span style="color:blue;">int</span>)&nbsp;BuiltInCat;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;AllowElement(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;elem&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;elem.Category.Id.IntegerValue&nbsp;==&nbsp;BultCatId;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;AllowReference(&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;refer,&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;point&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;Doc.GetElement(&nbsp;refer&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;e.get_Parameter(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.ELEM_FAMILY_PARAM&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.AsValueString().Equals(&nbsp;FmlyName&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">#region</span>&nbsp;Retrieve&nbsp;named&nbsp;family&nbsp;type&nbsp;using&nbsp;either&nbsp;LINQ&nbsp;or&nbsp;a&nbsp;parameter&nbsp;filter&nbsp;
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;Linq1(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,
&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;BultCat,
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;familyTypeName&nbsp;)
&nbsp;&nbsp;{
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;).OfCategory(&nbsp;BultCat&nbsp;).OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x&nbsp;=&gt;&nbsp;x.Symbol.Family.Name.Equals(&nbsp;familyTypeName&nbsp;)&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;Linq2(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,
&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;BultCat,
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;familyTypeName&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;).OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;).Cast&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x&nbsp;=&gt;&nbsp;x.get_Parameter(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.ELEM_FAMILY_PARAM&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.AsValueString()&nbsp;==&nbsp;familyTypeName&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;FilterRule(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,
&nbsp;&nbsp;<span style="color:green;">//&nbsp;ElementId&nbsp;ActiveViewId,</span>
&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;BultCat,
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;familyTypeName&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)<span style="color:green;">//,ActiveViewId)</span>
&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;BultCat&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterStringRule</span>(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ParameterValueProvider</span>(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.ELEM_FAMILY_PARAM&nbsp;)&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterStringEquals</span>(),&nbsp;familyTypeName,&nbsp;<span style="color:blue;">true</span>&nbsp;)&nbsp;)&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;Factory(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,
&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;BultCat,
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;familyTypeName&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;BultCat&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ParameterFilterRuleFactory</span>.CreateEqualsRule(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.ELEM_FAMILY_PARAM&nbsp;),&nbsp;familyTypeName,&nbsp;<span style="color:blue;">true</span>&nbsp;)&nbsp;)&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;named&nbsp;family&nbsp;symbols&nbsp;using&nbsp;either&nbsp;LINQ&nbsp;or&nbsp;a&nbsp;parameter&nbsp;filter</span>
 
&nbsp;&nbsp;<span style="color:blue;">#region</span>&nbsp;Timer
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">Timer</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;[<span style="color:#2b91af;">DllImport</span>(&nbsp;<span style="color:#a31515;">&quot;Kernel32.dll&quot;</span>&nbsp;)]
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;QueryPerformanceCounter(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">out</span>&nbsp;<span style="color:blue;">long</span>&nbsp;lpPerformanceCount&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;[<span style="color:#2b91af;">DllImport</span>(&nbsp;<span style="color:#a31515;">&quot;Kernel32.dll&quot;</span>&nbsp;)]
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;QueryPerformanceFrequency(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">out</span>&nbsp;<span style="color:blue;">long</span>&nbsp;lpFrequency&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">long</span>&nbsp;startTime,&nbsp;stopTime;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">long</span>&nbsp;freq;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Constructor</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;Timer()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;startTime&nbsp;=&nbsp;0;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stopTime&nbsp;=&nbsp;0;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!QueryPerformanceFrequency(&nbsp;<span style="color:blue;">out</span>&nbsp;freq&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Win32Exception</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;high-performance&nbsp;counter&nbsp;not&nbsp;supported&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Start&nbsp;the&nbsp;timer</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Start()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Thread</span>.Sleep(&nbsp;0&nbsp;);&nbsp;<span style="color:green;">//&nbsp;let&nbsp;waiting&nbsp;threads&nbsp;work</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;QueryPerformanceCounter(&nbsp;<span style="color:blue;">out</span>&nbsp;startTime&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">Stop&nbsp;the&nbsp;timer&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Stop()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;QueryPerformanceCounter(&nbsp;<span style="color:blue;">out</span>&nbsp;stopTime&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;the&nbsp;duration&nbsp;of&nbsp;the&nbsp;timer&nbsp;in&nbsp;seconds</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">double</span>&nbsp;Duration
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;(<span style="color:blue;">double</span>)&nbsp;(&nbsp;stopTime&nbsp;-&nbsp;startTime&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;(<span style="color:blue;">double</span>)&nbsp;freq;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Timer</span>
</pre>

Many thanks to Sudhan for implementing this benchmark and reporting these useful (and reassuring) results!

I created a complete Visual Studio solution and added the missing bits and pieces to test this live in
the [FilterBenchmark GitHub repository](https://github.com/jeremytammik/FilterBenchmark).

I hope that this encourages you to do some benchmarking as well and helps you optimise your own filtered element collectors.


<!--

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

**Question:** 

**Answer:** 

<pre class="code">
</pre>

Dear Sudhan,

I grabbed this code of yours and created an add-in from it. However, I cannot test or reproduce your result, since the definitions of the Draw_Section and InputData classes are lacking. I created a GitHub repository to hold the current state of the sample I created:

https://github.com/jeremytammik/FilterBenchmark

Would you like to fork that, add the missing definitions, and submit a pull request for me to integrate your changes?

Thank you!

Cheers, Jeremy.

-->
