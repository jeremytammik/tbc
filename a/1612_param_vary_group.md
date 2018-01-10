<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


Setting parameter varies between groups in #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/paramvarygroups

Looking at setting the <code>SetAllowVaryBetweenGroups</code> flag on a shared parameter;
is there any way to set <i>can vary by group instance</i> (what I need) the default for API-created bindings? ...

--->

### Setting Parameter Varies Between Groups

We already looked at the topic of setting the `SetAllowVaryBetweenGroups` flag on a shared parameter
in Scott Conover's [parameter definition overview](http://thebuildingcoder.typepad.com/blog/2016/12/parameter-definition-overview.html).

The setting was introduced in the [Revit 2014 API](http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html),
cf. *Parameter variance among group instances*.

Now Miroslav Schonauer raised it again, asking:

**Question:** Is the following option for group behaviour of shared param *instance* bindings exposed to API?
 
<center>
<img src="img/vary_between_groups_1.png" alt="SetAllowVaryBetweenGroups" width="695"/>
</center>

If yes, that solves all the issues :-)
 
If not &ndash; the default outcome seems to be *aligned per group type*.

Is there any way to set *can vary by group instance* (what I need) the default for API-created bindings?

Later: I found
this [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread 
on [creating a project parameter with *values can vary by group instance* selected](https://forums.autodesk.com/t5/revit-api-forum/create-project-parameter-with-quot-values-can-vary-by-group/m-p/5939455)
which explains that it is kind-of possible.
 
The problem remains that `SetAllowVaryBetweenGroups` is available only on `InternalDefinition`, while my programmatically created shared param has `ExternalDefinition`.

That thread explains that getting the binding after it has been created (i.e., in a 'Step 2') does return `InternalDefinition`, so this method can be used.
 
Can someone at least confirm that there is nothing simpler to do than the above 2-step process?

**Answer:** Yes. You need to do the two-step process:

- Register the shared parameter
- Find the internal definition
- Set the appropriate value for this property
 
The easiest way to go from one to the other:
 
- [SharedParameterElement.Lookup(GUID)](http://www.revitapidocs.com/2018.1/4dce82de-7495-523a-c8d4-4b3fc709e85e.htm)
- [ParameterElement.GetDefinition()](http://www.revitapidocs.com/2018.1/ec9b3cd3-4379-6eb8-7c7d-c220ba03f359.htm)

**Response:** I implemented this method to handle the setting of the `SetAllowVaryBetweenGroups` flag:
 
<pre class="code">
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Helper&nbsp;method&nbsp;to&nbsp;control&nbsp;`SetAllowVaryBetweenGroups`&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;option&nbsp;for&nbsp;instance&nbsp;binding&nbsp;param</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetInstanceParamVaryBetweenGroupsBehaviour(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Guid</span>&nbsp;guid,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;allowVaryBetweenGroups&nbsp;=&nbsp;<span style="color:blue;">true</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>&nbsp;<span style="color:green;">//&nbsp;last&nbsp;resort</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SharedParameterElement</span>&nbsp;sp&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">SharedParameterElement</span>.Lookup(&nbsp;doc,&nbsp;guid&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Should&nbsp;never&nbsp;happen&nbsp;as&nbsp;we&nbsp;will&nbsp;call&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;this&nbsp;only&nbsp;for&nbsp;*existing*&nbsp;shared&nbsp;param.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;sp&nbsp;)&nbsp;<span style="color:blue;">return</span>;&nbsp;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">InternalDefinition</span>&nbsp;def&nbsp;=&nbsp;sp.GetDefinition();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;def.VariesAcrossGroups&nbsp;!=&nbsp;allowVaryBetweenGroups&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Must&nbsp;be&nbsp;within&nbsp;an&nbsp;outer&nbsp;transaction!</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;def.SetAllowVaryBetweenGroups(&nbsp;doc,&nbsp;allowVaryBetweenGroups&nbsp;);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>&nbsp;{&nbsp;}&nbsp;<span style="color:green;">//&nbsp;ideally,&nbsp;should&nbsp;report&nbsp;something&nbsp;to&nbsp;log...</span>
&nbsp;&nbsp;}
</pre>

It assumes that `guid` comes from a known shared parameter.
 
Further good news: this can be called not only immediately after programmatically binding a new shared param, but also to *silently* change this specific setting for an *existing* shared parameter.

For example, we all typically have our own helper methods to get-or-create a shared parameter binding, cf., e.g., my method
to [add a category to a shared parameter binding](http://thebuildingcoder.typepad.com/blog/2012/04/adding-a-category-to-a-shared-parameter-binding.html).

Here is a code snippet providing enough to get the gist of how the above can be used (ignore my helper classes and error handling):

<pre class="code">
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Assumes&nbsp;outer&nbsp;transaction</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;GetOrCreateElemSharedParam(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;elem,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;paramName,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;grpName,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ParameterType</span>&nbsp;paramType,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;visible,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;instanceBinding,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;userModifiable,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Guid</span>&nbsp;guid,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;useTempSharedParamFile,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;tooltip&nbsp;=&nbsp;<span style="color:#a31515;">&quot;&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameterGroup</span>&nbsp;uiGrp&nbsp;=&nbsp;<span style="color:#2b91af;">BuiltInParameterGroup</span>.INVALID,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;allowVaryBetweenGroups&nbsp;=&nbsp;<span style="color:blue;">true</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Check&nbsp;if&nbsp;existing</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param&nbsp;=&nbsp;elem.LookupParameter(&nbsp;paramName&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;param&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;NOTE:&nbsp;If&nbsp;you&nbsp;don&#39;t&nbsp;want&nbsp;forcefully&nbsp;setting&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;the&nbsp;&quot;old&quot;&nbsp;instance&nbsp;params&nbsp;to&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;allowVaryBetweenGroups&nbsp;=true,</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;just&nbsp;comment&nbsp;the&nbsp;next&nbsp;3&nbsp;lines.</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;instanceBinding&nbsp;&amp;&amp;&nbsp;allowVaryBetweenGroups&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetInstanceParamVaryBetweenGroupsBehaviour(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;elem.Document,&nbsp;guid,&nbsp;allowVaryBetweenGroups&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;param;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;If&nbsp;here,&nbsp;need&nbsp;to&nbsp;create&nbsp;it&nbsp;(my&nbsp;custom&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;implementation&nbsp;and&nbsp;classes…)</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BindSharedParamResult&nbsp;res&nbsp;=&nbsp;BindSharedParam(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;elem.Document,&nbsp;elem.Category,&nbsp;paramName,&nbsp;grpName,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;paramType,&nbsp;visible,&nbsp;instanceBinding,&nbsp;userModifiable,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;guid,&nbsp;useTempSharedParamFile,&nbsp;tooltip,&nbsp;uiGrp&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;res&nbsp;!=&nbsp;BindSharedParamResult.eSuccessfullyBound
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;res&nbsp;!=&nbsp;BindSharedParamResult.eAlreadyBound&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;AllowVaryBetweenGroups&nbsp;for&nbsp;NEW&nbsp;Instance&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Binding&nbsp;Shared&nbsp;Param</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;instanceBinding&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetInstanceParamVaryBetweenGroupsBehaviour(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;elem.Document,&nbsp;guid,&nbsp;allowVaryBetweenGroups&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;If&nbsp;here,&nbsp;binding&nbsp;is&nbsp;OK&nbsp;and&nbsp;param&nbsp;seems&nbsp;to&nbsp;be</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;IMMEDIATELY&nbsp;available&nbsp;from&nbsp;the&nbsp;very&nbsp;same&nbsp;command</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;elem.LookupParameter(&nbsp;paramName&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;<span style="color:#2b91af;">Exception</span>&nbsp;ex&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Windows.Forms.<span style="color:#2b91af;">MessageBox</span>.Show(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>.Format(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Error&nbsp;in&nbsp;getting&nbsp;or&nbsp;creating&nbsp;Element&nbsp;Param:&nbsp;{0}&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ex.Message&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

I added Miro's method `SetInstanceParamVaryBetweenGroupsBehaviour` 
to [The Building Coder samples release 2018.0.134.11](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.134.11) in
the module [CmdCreateSharedParams.cs L441-L470](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCreateSharedParams.cs#L441-L470).

Many thanks to Miro for raising this issue and sharing his approach to solve it!
