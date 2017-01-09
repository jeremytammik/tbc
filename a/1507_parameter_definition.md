<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Parameter Definition Overview #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

I am happy to present a pretty comprehensive overview and explanation of the process of defining a shared parameter by Scott Conover.
Question: What do I need to do to programmatically create a shared parameter? I would like to set the <code>SetAllowVaryBetweenGroups</code> flag on it.
Answer: You create the details needed to define a shared parameter from <code>ExternalDefinition</code>.
Existing shared parameter file entries can be read to become an <code>ExternalDefinition</code> in your code, or you can create a new entry in the current shared parameter file using the <code>DefinitionGroup.Create</code> method...

-->

### Parameter Definition Overview

We have repeatedly discussed all kinds of different aspects
of [Revit element parameters](http://thebuildingcoder.typepad.com/blog/parameters),
but not put together
a [topic group](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5) for them yet.

Today I am happy to present a pretty comprehensive overview and explanation of the process of defining a shared parameter by none less than Scott Conover himself, Senior Revit Engineering Manager:


**Question:** What do I need to do to programmatically create a shared parameter?
I would like to set the `SetAllowVaryBetweenGroups` flag on it. 

<center>
<img src="img/new_shared_parameter.jpg" alt="New shared parameter" width="332"/>
</center>

**Answer:** You create the details needed to define a shared parameter from `ExternalDefinition`.
Existing shared parameter file entries can be read to become an `ExternalDefinition` in your code, or you can create a new entry in the current shared parameter file using the `DefinitionGroup.Create` method.

The sample code listed in the Revit API help file `RevitAPI.chm` or the online Revit API docs
under [InstanceBinding class](http://www.revitapidocs.com/2017/7978cb57-0a48-489e-2c8f-116fa2561437.htm) shows
this process best.

Here is part of that sample snippet:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;SetNewParameterToInstanceWall(
&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;app,
&nbsp;&nbsp;<span style="color:#2b91af;">DefinitionFile</span>&nbsp;myDefinitionFile&nbsp;)
{
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;new&nbsp;group&nbsp;in&nbsp;the&nbsp;shared&nbsp;parameters&nbsp;file</span>
&nbsp;&nbsp;<span style="color:#2b91af;">DefinitionGroups</span>&nbsp;myGroups&nbsp;=&nbsp;myDefinitionFile.Groups;
&nbsp;&nbsp;<span style="color:#2b91af;">DefinitionGroup</span>&nbsp;myGroup&nbsp;=&nbsp;myGroups.Create(&nbsp;<span style="color:#a31515;">&quot;MyParameters&quot;</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;an&nbsp;instance&nbsp;definition&nbsp;in&nbsp;definition&nbsp;group&nbsp;MyParameters</span>
&nbsp;&nbsp;<span style="color:#2b91af;">ExternalDefinitionCreationOptions</span>&nbsp;option
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ExternalDefinitionCreationOptions</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Instance_ProductDate&quot;</span>,&nbsp;<span style="color:#2b91af;">ParameterType</span>.Text&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Don&#39;t&nbsp;let&nbsp;the&nbsp;user&nbsp;modify&nbsp;the&nbsp;value,&nbsp;only&nbsp;the&nbsp;API</span>
&nbsp;&nbsp;option.UserModifiable&nbsp;=&nbsp;<span style="color:blue;">false</span>;
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;tooltip</span>
&nbsp;&nbsp;option.Description&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Wall&nbsp;product&nbsp;date&quot;</span>;
&nbsp;&nbsp;<span style="color:#2b91af;">Definition</span>&nbsp;myDefinition_ProductDate
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;myGroup.Definitions.Create(&nbsp;option&nbsp;);

  . . .
</pre>

The return from `DefinitionGroup.Create` is an `ExternalDefinition`, even though the type declared is the parent class.

Once you have an `ExternalDefinition`, you add it to the document.

There are several ways:

1. Use `InstanceBinding` as shown in that sample.
2. Use `FamilyManager.AddParameter` to add the parameter to a family.
3. Use `FamilyManager.ReplaceParameter` to replace a family parameter with the shared one.
4. Use `SharedParameterElement.Create` to create the element that represents the parameter without binding it to any categories.

There are also some `RebarShape` related utilities which I would not recommend for general usage but might be OK for rebar-specific code.

Once the parameter is in the document, it has an `InternalDefinition`.

The best ways to get it:

1. If you have the `ParameterElement` from #4 you can use `ParameterElement.GetDefinition`.
2. If you have the GUID (which you should, since it is provided by the `ExternalDefinition`), you can use `SharedParameterElement.Lookup` followed by `ParameterElement.GetDefinition`.
3. If you have an instance of an element whose category has this parameter bound, get the `Parameter` and use `Parameter.Definition`.

Once you have the `InternalDefinition`, you can access the vary across groups option as well as other things.
You can also use an `InternalDefintion` for adding and removing `InstanceBindings` to categories.

Many thanks to Scott for this nice comprehensive summary and overview!


####<a name="2"></a>Addemdum

Joshua Lumley pointed out some possible enhancements
in [his two](http://thebuildingcoder.typepad.com/blog/2016/12/parameter-definition-overview.html#comment-3079825547)
[comments](http://thebuildingcoder.typepad.com/blog/2016/12/parameter-definition-overview.html#comment-3079829813) below:

To run the code more than twice I added:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;dgMatchFound&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">DefinitionGroup</span>&nbsp;dg&nbsp;<span style="color:blue;">in</span>&nbsp;myGroups&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;dg.Name&nbsp;==&nbsp;myGroupName&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dgMatchFound&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;myGroup&nbsp;=&nbsp;dg;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;dgMatchFound&nbsp;==&nbsp;<span style="color:blue;">false</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;myGroup&nbsp;=&nbsp;myGroups.Create(&nbsp;myGroupName&nbsp;);
&nbsp;&nbsp;}
</pre>

and

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;dMatchFound&nbsp;=&nbsp;<span style="color:blue;">false</span>;
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Definition</span>&nbsp;d&nbsp;<span style="color:blue;">in</span>&nbsp;myGroup.Definitions&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;d.Name&nbsp;==&nbsp;newParameterName&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dMatchFound&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;myDefinition_ProductDate&nbsp;=&nbsp;d;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!dMatchFound&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;myDefinition_ProductDate
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;myGroup.Definitions.Create(&nbsp;option&nbsp;);
&nbsp;&nbsp;}
</pre>

I called it like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">DefinitionFile</span>&nbsp;defFile&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;GetOrCreateSharedParamsFile(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveUIDocument.Application.Application&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">bool</span>&nbsp;AddParameterResult&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;SetNewParameterToInstanceWall(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveUIDocument.Application,&nbsp;defFile&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Did&nbsp;it&nbsp;work&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;AddParameterResult.ToString()&nbsp;);
</pre>

Many thanks to Josh for the helpful usage hints!
