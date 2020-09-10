<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<style type="text/css">
  .jt { background-color:#eee;border-collapse:collapse; }
  .jt th { background-color:#000;color:white; }
  .jt td, .jt th { padding:5px;border:1px solid #000; }
</style>
</head>

<!---

- optimising shared parameter setting
  Modify Shared Parameters for high number of family instance
  https://forums.autodesk.com/t5/revit-api-forum/modify-shared-parameters-for-high-number-of-family-instance/m-p/9727166

- [Let's build a Full-Text Search engine](https://artem.krylysov.com/blog/2020/07/28/lets-build-a-full-text-search-engine)
  by Artem Krylysov
  https://github.com/akrylysov/simplefts
  /a/src/go/tbcfts/
  tar -cf tbcdump.tar *htm *html
  gzip tbcdump.tar  

- troubleshooting the OBJ importer
  https://truevis.com/troubleshoot-revit-mesh-import/
  Re: Addin broken on Czech language OS
  Eric Boehlke <design@truevis.com>

twitter:

Dabbling with the Go programming language, optimising setting shared parameters and full-text search for The Building Coder posts for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/optimiseparams

I have been dabbling with the Go programming language in the past week, besides optimising and answering Revit API questions
&ndash; Optimising setting shared parameters
&ndash; Full-text search for The Building Coder posts
&ndash; Decimal point woe...

linkedin:

Dabbling with the Go programming language, optimising setting shared parameters and full-text search for The Building Coder posts for the #RevitAPI

http://bit.ly/optimiseparams

I have been dabbling with the Go programming language in the past week, besides optimising and answering Revit API questions:

- Optimising setting shared parameters
- Full-text search for The Building Coder posts
- Decimal point woe...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Optimising Parameters and Full-Text Search

I have been dabbling with the Go programming language in the past week, besides optimising and answering Revit API questions:

- [Optimising setting shared parameters](#2)
- [Full-text search for The Building Coder posts](#3)
- [Decimal point woe](#4)

####<a name="2"></a>Optimising Setting Shared Parameters

I took a successful stab at optimising the setting of a large number of shared parameters in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [modifying shared parameters for a high number of family instances](https://forums.autodesk.com/t5/revit-api-forum/modify-shared-parameters-for-high-number-of-family-instance/m-p/9727166):

**Question:** I have the code below working fine when my collection contains less than 30 objects; above 30, the transaction commit doesn't affect the document and there is no error or exception raised.

<pre class="code">
<span style="color:blue;">void</span>&nbsp;<span style="color:#2b91af;">IExternalEventHandler</span>.Execute(&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;app&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;app.ActiveUIDocument;
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;view&nbsp;=&nbsp;doc.ActiveView;
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tr&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param1&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param2;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param3;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param4&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param5&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param6&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param7&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;tr.Start(&nbsp;<span style="color:#a31515;">&quot;synchro&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Main.idForSynchros&nbsp;is&nbsp;the&nbsp;collection&nbsp;of&nbsp;data</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">for</span>(&nbsp;<span style="color:blue;">int</span>&nbsp;i&nbsp;=&nbsp;0;&nbsp;i&nbsp;&lt;&nbsp;Main.idForSynchros.Count;&nbsp;i++&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IdForSynchro</span>&nbsp;i&nbsp;=&nbsp;Main.idForSynchros[&nbsp;i&nbsp;];
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;element&nbsp;=&nbsp;doc.GetElement(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Convert</span>.ToInt32(&nbsp;i.RevitId&nbsp;)&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;param1&nbsp;=&nbsp;element.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;PLUGIN_PARAM1&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;param2&nbsp;=&nbsp;element.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;PLUGIN_PARAM2&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;param3&nbsp;=&nbsp;element.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;PLUGIN_PARAM3&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;param4&nbsp;=&nbsp;element.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;PLUGIN_PARAM4&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;param5&nbsp;=&nbsp;element.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;PLUGIN_PARAM5&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;param6&nbsp;=&nbsp;element.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;PLUGIN_PARAM6&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;param7&nbsp;=&nbsp;element.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;PLUGIN_PARAM7&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;param1.AsInteger()&nbsp;&lt;&nbsp;1&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;param1.Set(&nbsp;i.param1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;param6Value&nbsp;=&nbsp;<span style="color:#2b91af;">DateTime</span>.Parse(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i.data.param6,&nbsp;CultureInfo.InvariantCulture&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToLongDateString();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;param7Value&nbsp;=&nbsp;<span style="color:#2b91af;">DateTime</span>.Parse(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i.data.param7,&nbsp;CultureInfo.InvariantCulture&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToLongDateString();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Utils.SetParameterValueString(&nbsp;param2,&nbsp;i.data.param2&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Utils.SetParameterValueString(&nbsp;param3,&nbsp;i.data.param2&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Utils.SetParameterValueString(&nbsp;param4,&nbsp;i.data.param4&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Utils.SetParameterValueString(&nbsp;param5,&nbsp;i.data.param5&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Utils.SetParameterValueString(&nbsp;param6,&nbsp;param6Value&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Utils.SetParameterValueString(&nbsp;param7,&nbsp;param7Value&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;tr.Commit();
&nbsp;&nbsp;}
}
 
<span style="color:green;">//&nbsp;Method&nbsp;of&nbsp;Utils&nbsp;class&nbsp;that&nbsp;set&nbsp;Parameter&nbsp;value</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetParameterValueString(&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;parameter,&nbsp;<span style="color:blue;">string</span>&nbsp;value&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;parameter&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;parameter.Set(&nbsp;value&nbsp;);
&nbsp;&nbsp;}
}
</pre>

Does anyone have any idea to solve this?

**Answer:** `LookupParameter` is unnecessarily costly.

There is no need to re-execute it for every parameter on every single element.

That is costing you a lot of time and hurting performance.

Every shared parameter is equipped with a GUID to identify it.

Determine the GUID once only on the first element.

Then, you can use the GUID to retrieve each parameter directly from the element without any further need for `LookupParameter` to search through the entire list of parameters each time.

Here is my suggestion for an improvement:

<pre class="code">
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">IdForSynchro</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;RevitId&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">int</span>&nbsp;Param1&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;Param2&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">double</span>&nbsp;Param3&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">set</span>;&nbsp;}
}
 
<span style="color:blue;">void</span>&nbsp;modifyParameterValues(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">IdForSynchro</span>&gt;&nbsp;data&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tr&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Guid</span>&nbsp;guid1&nbsp;=&nbsp;<span style="color:#2b91af;">Guid</span>.Empty;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Guid</span>&nbsp;guid2&nbsp;=&nbsp;<span style="color:#2b91af;">Guid</span>.Empty;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Guid</span>&nbsp;guid3&nbsp;=&nbsp;<span style="color:#2b91af;">Guid</span>.Empty;
 
&nbsp;&nbsp;&nbsp;&nbsp;tr.Start(&nbsp;<span style="color:#a31515;">&quot;synchro&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">IdForSynchro</span>&nbsp;d&nbsp;<span style="color:blue;">in</span>&nbsp;data&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;d.RevitId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">Guid</span>.Empty&nbsp;==&nbsp;guid1&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;guid1&nbsp;=&nbsp;e.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;PLUGIN_PARAM1&quot;</span>&nbsp;).GUID;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;guid2&nbsp;=&nbsp;e.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;PLUGIN_PARAM2&quot;</span>&nbsp;).GUID;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;guid3&nbsp;=&nbsp;e.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;PLUGIN_PARAM3&quot;</span>&nbsp;).GUID;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.get_Parameter(&nbsp;guid1&nbsp;).Set(&nbsp;d.Param1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.get_Parameter(&nbsp;guid2&nbsp;).Set(&nbsp;d.Param2&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.get_Parameter(&nbsp;guid3&nbsp;).Set(&nbsp;d.Param3&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;tr.Commit();
&nbsp;&nbsp;}
}
</pre>

I added this code in the original and updated forms
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).

Here is the [diff between the two](https://github.com/jeremytammik/the_building_coder_samples/commit/fea7381f51b660fc9b5660c2e64f548623b11d8b).
and the [current implementation code](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCreateSharedParams.cs#L547-L584).

Please equip your code with a little benchmark timer stopwatch and let us know whether this helps and how it affects the processing time required before and after making this modification.

**Response:** I applied the solution and the execution time improved.

Here are some results before and after the modification, correlating the number of family instances with the execution time required in milliseconds before and after the enhancement:

<center>
<table class="jt">
<tr><th class="r">Instances</th><th class="r">&nbsp;&nbsp;ms before</th><th class="r">&nbsp;&nbsp;ms after</th></tr>
<tr><td class="r">58</td><td class="r">896</td><td class="r">794</td></tr>
<tr><td class="r">375</td><td class="r">1958</td><td class="r">1549</td></tr>
<tr><td class="r">1052</td><td class="r">16777</td><td class="r">8152</td></tr>
</table>
</center>

But there are still times when changes are not applied to the document.

**Answer:** Not sure if you caught it when you refactored, but in your original code you used `param2` to set `2` and `3`.
Could that be affecting what your results are?

May be good to throw in some Try/Catch block to try and see where or if it's failing or at least a task dialog to tell user if a param is missing.

####<a name="3"></a>Full-Text Search for The Building Coder Posts

I discovered a nice article by Artem Krylysov
suggesting [let's build a full-text search engine](https://artem.krylysov.com/blog/2020/07/28/lets-build-a-full-text-search-engine)
using the [Go programming language](https://golang.org),
with an accompanying [simplefts GitHub repository](https://github.com/akrylysov/simplefts) sharing the final result.

That prompted me to dabble a bit with Go and use it to implement full text search functionality for The Building coder blog posts as well.

The work in progress is available from my [tbcfts GitHub repository](https://github.com/jeremytammik/tbcfts).

[The Building Coder blog source `tbc`](https://github.com/jeremytammik/tbc) is available from GitHub as well, so you have all you need to play with it yourself, if you please.

Here is a sample run searching for the word 'dabble', which includes today's draft post, still lacking the public URL:

<pre>
/a/src/go/tbcfts $ ./tbcfts -q "dabble"
2020/09/09 10:31:40 Starting tbcfts, p=/a/doc/revit/tbc/git/a, q=dabble
2020/09/09 10:31:41 Loaded 1863 documents in 377.397917ms
2020/09/09 10:31:44 Indexed 1863 documents in 2.876775333s
2020/09/09 10:31:44 Search for 'dabble' found 5 documents in 9.703µs
2020/09/09 10:31:44 582 [Wiki API Help, View Event and Structural Material Type](http://thebuildingcoder.typepad.com/blog/2011/05/wiki-api-help-view-event-and-structural-material-type.html)
2020/09/09 10:31:44 906 [Export Wall Parts Individually to DXF](http://thebuildingcoder.typepad.com/blog/2013/03/export-wall-parts-individually-to-dxf.html)
2020/09/09 10:31:44 961 [Super Insane MP3 and Songbird Playlist Exporter](http://thebuildingcoder.typepad.com/blog/2013/06/super-insane-mp3-and-songbird-playlist-exporter.html)
2020/09/09 10:31:44 1008 [Open MEP Connector Warning](http://thebuildingcoder.typepad.com/blog/2013/08/open-mep-connector-warning.html)
2020/09/09 10:31:44 1863 [Optimising Parameters and Full-Text Search](http thebuildingcoder.typepad.com not yet published)
</pre>

According to `wc`, the current blog post HTML source consists of 355233 lines, 2230690 words and 20676311 characters, including markup.

As you can see, loading the documents and storing their body text in memory costs ca. 400 ms.

The indexing is costly, clocking in at ca. 3 seconds.

Once indexing is complete, the lookup is very fast, consuming just 10 microseconds.

Obviously, the next feature to address would be caching the index.

Another important enhancement would be to split the documents into smaller sections.

For instance, I could create much smaller and more targeted documents to index by using the `h4` tags that delimit individual sections of text within each blog post instead of retaining each blog post in its entirety as a single document.

####<a name="4"></a>Decimal Point Woe

A few of my Revit sample add-ins have been promoted to full-fledged commercial applications.

One of them is the OBJ importer, which Eric Boehlke has published to the AppStore and continuously enhanced.

A new little issue arose with it that is useful to be aware of, since it applies to many other contexts as well:

**Question:** I had a strange phenomenon happen.
I have an OBJ Import app customer using Czech language on Windows.
His Revit is English 2020.

The problem is that with the same OBJ file, and the same Revit version, on my computer it works fine, and on his the app runs but fails and imports 0 faces.

Have you ever seen an add-in fail because the OS was a language that had non-English characters?

I really don't know what is causing the problem.

**Answer:** Yes, often!

Some language cultures use comma instead of decimal point and then crash when trying to read floating point numbers.

**Response:** Aha!

Yes, I see, e.g., in a different OBJ software,
[OBJ export is broken on locales with comma instead of dot](https://github.com/keenanwoodall/Deform/issues/17).

That was the problem.

I told the client and updated
the [Troubleshooting *Mesh Import from OBJ Files* for Revit](https://truevis.com/troubleshoot-revit-mesh-import) with the workaround:

**Region Number Format**

Some regions use commas instead of dots for the decimal place.
That may cause the import to fail.
A workaround is to set Window’s Region to United States before importing the OBJ.

<center>
<img src="img/windows_regional_number_format.png" alt="Windows regional number format" title="Windows regional number format" width="400"/> <!-- 511 -->
</center>

