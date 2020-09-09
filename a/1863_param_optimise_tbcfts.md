<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
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

### Optimising Parameters and Full-Text Search

<center>
<img src="img/" alt="" title="" width="100"/>
</center>

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
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">IdForSynchro</span>&nbsp;d&nbsp;<span style="color:blue;">in</span>&nbsp;data&nbsp;)&nbsp;<span style="color:green;">//&nbsp;Main.idForSynchro&nbsp;is&nbsp;the&nbsp;collection&nbsp;of&nbsp;data</span>
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

**Response:** I apply the solution and the execution time improved.

Here is some results before and after the modification, correlating the number of family instances with the execution time required before and after the enhancement:

<table>
<th><td class="r">Instances</td><td class="r">ms before</td><td class="r">ms after</td></th>
<tr><td class="r">58</td><td class="r">896</td><td class="r">794</td></tr>
<tr><td class="r">375</td><td class="r">1958</td><td class="r">1549</td></tr>
<tr><td class="r">1052</td><td class="r">16777</td><td class="r">8152</td></tr>
<table>

But there are still times when changes are not applied to the document.

**Answer:** Not sure if you caught it when you refactored, but in your original code you used `param2` to set `2` and `3`.
Could that be affecting what your results are?

May be good to throw in some Try/Catch block to try and see where or if it's failing or at least a task dialog to tell user if a param is missing.

####<a name="3"></a>

<pre class="code">
</pre>

