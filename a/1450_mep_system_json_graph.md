<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

MEP System Structure in Hierarchical JSON Graph #revitapi #3dwebcoder @AutodeskForge #ForgeDevCon #3dwebaccel

Yesterday, I presented the new TraverseAllSystems add-in to traverse all MEP system graphs and export their connected hierarchical structure to JSON and XML that I am helping the USC team with here at the San Francisco cloud accelerator. I continued with that today, and also integrated a minor enhancement to RevitLookup
&ndash; TraverseAllSystems updates
&ndash; Shared parameter creation
&ndash; Options
&ndash; Bottom-up JSON structure
&ndash; Top-down JSON structure
&ndash; TraversalTree JSON output generator
&ndash; TreeNode JSON output generator
&ndash; Download and to do
&ndash; RevitLookup updates...

-->

### MEP System Structure in Hierarchical JSON Graph

Yesterday, I presented the
new [TraverseAllSystems](https://github.com/jeremytammik/TraverseAllSystems) add-in
to [traverse all MEP system graphs](http://thebuildingcoder.typepad.com/blog/2016/06/traversing-and-exporting-all-mep-system-graphs.html) and
export their connected hierarchical structure to JSON and XML that I am helping
the [USC](http://www.usc.edu) team with here at the San Francisco cloud accelerator.

<center>
<img src="img/2016-06_sf_accelerator.jpg" alt="San Francisco cloud accelerator" width="400">
</center>

I continued with that today, and also integrated a minor enhancement to RevitLookup:

- [TraverseAllSystems updates](#1)
- [Shared parameter creation](#2)
- [Options](#3)
- [Bottom-up JSON structure](#4)
- [Top-down JSON structure](#5)
- [TraversalTree JSON output generator](#6)
- [TreeNode JSON output generator](#7)
- [Download and to do](#8)
- [RevitLookup updates](#9)


#### <a name="1"></a>TraverseAllSystems Updates

The aim of the TraverseAllSystems project is to present the MEP system graphs in a separate tree view panel integrated in
the [Forge viewer](https://developer.autodesk.com/en/docs/viewer/v2/overview) and
hook up the tree view nodes bi-directionally with the 2D and 3D viewer elements.

To achieve that, I implemented a couple of significant enhancements over the simple XML file storage:

- Store the MEP system graph structure in JSON instead of XML
- Implement both bottom-up and top-down storage according to
the [jsTree JSON spec](https://www.jstree.com/docs/json).
- Support both element id and UniqueId node identifiers.
- Store the JSON output in a shared parameter attached to the MEP system element,
so that it is automatically included in the Forge SVF translation generated from the RVT input file.

Here is a list of the update releases so far:

- [2017.0.0.2](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.2) &ndash; implemented visited element dictionary to prevent infinite recursion loop
- [2017.0.0.3](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.3) &ndash; implemented DumpToJson
- [2017.0.0.4](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.4) &ndash; implemented shared parameter creation
- [2017.0.0.5](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.5) &ndash; implemented shared parameter value population, tested and verified graph structure json is written out
- [2017.0.0.6](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.6) &ndash; renamed json text field to name
- [2017.0.0.7](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.7) &ndash; implemented top-down json graph storage
- [2017.0.0.8](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.8) &ndash; automatically create shared parameter, eliminated separate command, wrap json strings in double quotes, validated json output

#### <a name="2"></a>Shared Parameter Creation

I implemented a new `SharedParameterMgr` class to create the shared parameter to store the JSON output in.

This class is based on
the [ExportCncFab](https://github.com/jeremytammik/ExportCncFab)
[ExportParameters.cs module](https://github.com/jeremytammik/ExportCncFab/blob/master/ExportCncFab/ExportParameters.cs).

The shared parameter is automatically created if not already present, as in the following usage example:

<pre class="code">
<span style="color:green;">//&nbsp;Check&nbsp;for&nbsp;shared&nbsp;parameter</span>
<span style="color:green;">//&nbsp;to&nbsp;store&nbsp;graph&nbsp;information.</span>

<span style="color:#2b91af;">Definition</span>&nbsp;def&nbsp;=&nbsp;<span style="color:#2b91af;">SharedParameterMgr</span>.GetDefinition(
&nbsp;&nbsp;desirableSystems.First&lt;<span style="color:#2b91af;">MEPSystem</span>&gt;()&nbsp;);

<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;def&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">SharedParameterMgr</span>.Create(&nbsp;doc&nbsp;);

&nbsp;&nbsp;def&nbsp;=&nbsp;<span style="color:#2b91af;">SharedParameterMgr</span>.GetDefinition(
&nbsp;&nbsp;&nbsp;&nbsp;desirableSystems.First&lt;<span style="color:#2b91af;">MEPSystem</span>&gt;()&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;def&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;message&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Error&nbsp;creating&nbsp;the&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;storage&nbsp;shared&nbsp;parameter.&quot;</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Failed;
&nbsp;&nbsp;}
}
</pre>

Here is the `SharedParameterMgr` class implementation:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Shared&nbsp;parameters&nbsp;to&nbsp;keep&nbsp;store&nbsp;MEP&nbsp;system&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;graph&nbsp;structure&nbsp;in&nbsp;JSON&nbsp;strings.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">SharedParameterMgr</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Define&nbsp;the&nbsp;user&nbsp;visible&nbsp;shared&nbsp;parameter&nbsp;name.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_shared_param_name&nbsp;=&nbsp;<span style="color:#a31515;">&quot;MepSystemGraphJson&quot;</span>;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;the&nbsp;parameter&nbsp;definition&nbsp;from</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;the&nbsp;given&nbsp;element&nbsp;and&nbsp;parameter&nbsp;name.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Definition</span>&nbsp;GetDefinition(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">Parameter</span>&gt;&nbsp;ps&nbsp;=&nbsp;e.GetParameters(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_shared_param_name&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;n&nbsp;=&nbsp;ps.Count;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;1&nbsp;&gt;=&nbsp;n,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;maximum&nbsp;one&nbsp;shared&nbsp;parameters&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;named&nbsp;&quot;</span>&nbsp;+&nbsp;_shared_param_name&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Definition</span>&nbsp;d&nbsp;=&nbsp;(&nbsp;0&nbsp;==&nbsp;n&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;<span style="color:blue;">null</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;ps[0].Definition;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;d;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Create&nbsp;a&nbsp;new&nbsp;shared&nbsp;parameter&nbsp;definition&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;in&nbsp;the&nbsp;specified&nbsp;grpup.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Definition</span>&nbsp;CreateNewDefinition(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DefinitionGroup</span>&nbsp;group,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;parameter_name,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ParameterType</span>&nbsp;parameter_type&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;group.Definitions.Create(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ExternalDefinitionCreationOptions</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parameter_name,&nbsp;parameter_type&nbsp;)&nbsp;);
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Create&nbsp;the&nbsp;shared&nbsp;parameter.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Create(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Shared&nbsp;parameters&nbsp;filename;&nbsp;used&nbsp;only&nbsp;in&nbsp;case</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;none&nbsp;is&nbsp;set&nbsp;and&nbsp;we&nbsp;need&nbsp;to&nbsp;create&nbsp;the&nbsp;export</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;history&nbsp;shared&nbsp;parameters.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_shared_parameters_filename
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#a31515;">&quot;shared_parameters.txt&quot;</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_definition_group_name
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#a31515;">&quot;TraverseAllSystems&quot;</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;app&nbsp;=&nbsp;doc.Application;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;shared&nbsp;parameter&nbsp;file&nbsp;name</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;sharedParamsFileName
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;app.SharedParametersFilename;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;sharedParamsFileName
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;0&nbsp;==&nbsp;sharedParamsFileName.Length&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;path&nbsp;=&nbsp;<span style="color:#2b91af;">Path</span>.GetTempPath();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;path&nbsp;=&nbsp;<span style="color:#2b91af;">Path</span>.Combine(&nbsp;path,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_shared_parameters_filename&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">StreamWriter</span>&nbsp;stream;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stream&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">StreamWriter</span>(&nbsp;path&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stream.Close();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;app.SharedParametersFilename&nbsp;=&nbsp;path;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sharedParamsFileName
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;app.SharedParametersFilename;
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;shared&nbsp;parameter&nbsp;file&nbsp;object</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DefinitionFile</span>&nbsp;f
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;app.OpenSharedParameterFile();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Create&nbsp;TraverseAllSystems&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;Shared&nbsp;Parameters&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;the&nbsp;category&nbsp;set&nbsp;for&nbsp;binding</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CategorySet</span>&nbsp;catSet&nbsp;=&nbsp;app.Create.NewCategorySet();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Category</span>&nbsp;cat&nbsp;=&nbsp;doc.Settings.Categories.get_Item(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_DuctSystem&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;catSet.Insert(&nbsp;cat&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cat&nbsp;=&nbsp;doc.Settings.Categories.get_Item(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_PipingSystem&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;catSet.Insert(&nbsp;cat&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Binding</span>&nbsp;binding&nbsp;=&nbsp;app.Create.NewInstanceBinding(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;catSet&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;or&nbsp;create&nbsp;shared&nbsp;parameter&nbsp;group</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DefinitionGroup</span>&nbsp;group
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;f.Groups.get_Item(&nbsp;_definition_group_name&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;??&nbsp;f.Groups.Create(&nbsp;_definition_group_name&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;or&nbsp;create&nbsp;the&nbsp;three&nbsp;parameters;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;we&nbsp;could&nbsp;check&nbsp;if&nbsp;they&nbsp;are&nbsp;already&nbsp;bound,&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;but&nbsp;it&nbsp;looks&nbsp;like&nbsp;Insert&nbsp;will&nbsp;just&nbsp;ignore&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;them&nbsp;in&nbsp;that&nbsp;case.</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Definition</span>&nbsp;definition
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;group.Definitions.get_Item(&nbsp;_shared_param_name&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;??&nbsp;CreateNewDefinition(&nbsp;group,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_shared_param_name,&nbsp;<span style="color:#2b91af;">ParameterType</span>.Text&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.ParameterBindings.Insert(&nbsp;definition,&nbsp;binding,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameterGroup</span>.PG_GENERAL&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

#### <a name="3"></a>Options

I implemented a new `Options` class to control two settings:

- Use element id or UniqueId for to identify node
- Store JSON graph bottom-up or top-down

The class implementation is short, sweet and trivial:

<pre class="code">
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">Options</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Store&nbsp;element&nbsp;id&nbsp;or&nbsp;UniqueId&nbsp;in&nbsp;JSON&nbsp;output?</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;StoreUniqueId&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;StoreElementId&nbsp;=&nbsp;!StoreUniqueId;

&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Store&nbsp;parent&nbsp;node&nbsp;id&nbsp;in&nbsp;child,&nbsp;or&nbsp;recursive&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;tree&nbsp;of&nbsp;children&nbsp;in&nbsp;parent?</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;StoreJsonGraphBottomUp&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;StoreJsonGraphTopDown
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;!StoreJsonGraphBottomUp;
}
</pre>

The two bottom-up and top-down JSON storage structures both comply with
the [jsTree JSON spec](https://www.jstree.com/docs/json).

#### <a name="4"></a>Bottom-Up JSON Structure

<pre class="prettyprint">
[
  { "id" : "ajson1", "parent" : "#", "text" : "Simple root node" },
  { "id" : "ajson2", "parent" : "#", "text" : "Root node 2" },
  { "id" : "ajson3", "parent" : "ajson2", "text" : "Child 1" },
  { "id" : "ajson4", "parent" : "ajson2", "text" : "Child 2" },
]
</pre>

#### <a name="5"></a>Top-Down JSON Structure

<pre class="prettyprint">
{
  id: -1,
  name: 'Root',
  children: [
  {
    id: 0,
    name: 'Mechanical System',
    children: [
    {
      id: 0_1,
      name: 'Child 0_1',
      type: 'window',
      otherField: 'something...',
      children: [
      {
        id: 0_1_1,
        name: 'Grandchild 0_1_1'
      }]
    }, {
      id: 0_2,
      name: 'Child 0_2',
      children: [
      {
        id: 0_2_1,
        name: 'Grandchild 0_2_1'
      }]
    }]
  }, {
    id: 2,
    name: 'Electrical System',
    children: [
    {
      id: 2_1,
      name: 'Child 2_1',
      children: [{
        id: 2_1_1,
        name: 'Grandchild 2_1_1'
      }]
    },
    {
      id: 2_2,
      name: 'Child 2_2',
      children: [{
        id: 2_2_1,
        name: 'Grandchild 2_2_1'
      }]
    }]
  },
  {
    id: 3,
    name: 'Piping System',
    children: [
    {
      id: 3_1,
      name: 'Child 3_1',
      children: [{
        id: 3_1_1,
        name: 'Grandchild 3_1_1'
      }]
    },
    {
      id: 3_2,
      name: 'Child 3_2',
      children: [{
        id: 3_2_1,
        name: 'Grandchild 3_2_1'
      }]
    }]
  }]
}
</pre>

#### <a name="6"></a>TraversalTree JSON Output Generator

The two `TraversalTree` JSON output generators `DumpToJsonTopDown` and `DumpToJsonBottomUp` are pretty trivial as well, since all the work is done by the individual tree nodes:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Dump&nbsp;the&nbsp;top-down&nbsp;traversal&nbsp;graph&nbsp;into&nbsp;JSON.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;In&nbsp;this&nbsp;case,&nbsp;each&nbsp;parent&nbsp;node&nbsp;is&nbsp;populated</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;with&nbsp;a&nbsp;full&nbsp;hierarchical&nbsp;graph&nbsp;of&nbsp;all&nbsp;its</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;children,&nbsp;cf.&nbsp;https://www.jstree.com/docs/json.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;DumpToJsonTopDown()
{
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;m_startingElementNode
&nbsp;&nbsp;&nbsp;&nbsp;.DumpToJsonTopDown();
}

<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Dump&nbsp;the&nbsp;bottom-up&nbsp;traversal&nbsp;graph&nbsp;into&nbsp;JSON.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;In&nbsp;this&nbsp;case,&nbsp;each&nbsp;child&nbsp;node&nbsp;is&nbsp;equipped&nbsp;with&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;a&nbsp;&#39;parent&#39;&nbsp;pointer,&nbsp;cf.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;https://www.jstree.com/docs/json/</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;DumpToJsonBottomUp()
{
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;a&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;();
&nbsp;&nbsp;m_startingElementNode.DumpToJsonBottomUp(&nbsp;a,&nbsp;<span style="color:#a31515;">&quot;#&quot;</span>&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#a31515;">&quot;[&quot;</span>&nbsp;+&nbsp;<span style="color:blue;">string</span>.Join(&nbsp;<span style="color:#a31515;">&quot;,&quot;</span>,&nbsp;a&nbsp;)&nbsp;+&nbsp;<span style="color:#a31515;">&quot;]&quot;</span>;
}
</pre>

#### <a name="7"></a>TreeNode JSON Output Generator

The two `TreeNode` JSON output generators are only slightly more complicated.

Here are the two formatting strings that they use:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Format&nbsp;a&nbsp;tree&nbsp;node&nbsp;to&nbsp;JSON&nbsp;storing&nbsp;parent&nbsp;id&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;in&nbsp;child&nbsp;node&nbsp;for&nbsp;bottom-up&nbsp;structure.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_json_format_to_store_parent_in_child
&nbsp;&nbsp;=&nbsp;<span style="color:#a31515;">&quot;&#123;&#123;&quot;</span>
&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\&quot;id\&quot;&nbsp;:&nbsp;{0},&nbsp;&quot;</span>
&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\&quot;name\&quot;&nbsp;:&nbsp;\&quot;{1}\&quot;,&nbsp;&quot;</span>
&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\&quot;parent\&quot;&nbsp;:&nbsp;{2}}}&quot;</span>;

<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Format&nbsp;a&nbsp;tree&nbsp;node&nbsp;to&nbsp;JSON&nbsp;storing&nbsp;a&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;hierarchical&nbsp;tree&nbsp;of&nbsp;children&nbsp;ids&nbsp;in&nbsp;parent&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;for&nbsp;top-down&nbsp;structure.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;_json_format_to_store_children_in_parent
&nbsp;&nbsp;=&nbsp;<span style="color:#a31515;">&quot;&#123;&#123;&quot;</span>
&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\&quot;id\&quot;&nbsp;:&nbsp;{0},&nbsp;&quot;</span>
&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\&quot;name\&quot;&nbsp;:&nbsp;\&quot;{1}\&quot;,&nbsp;&quot;</span>
&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\&quot;children\&quot;&nbsp;:&nbsp;[{2}]}}&quot;</span>;
</pre>

Here are the two recursive functions implementing the JSON output:

<pre class="code">
<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;GetName(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;e.Name.Replace(&nbsp;<span style="color:#a31515;">&quot;\&quot;&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;&#39;&quot;</span>&nbsp;);
}

<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;GetId(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Options</span>.StoreUniqueId
&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;<span style="color:#a31515;">&quot;\&quot;&quot;</span>&nbsp;+&nbsp;e.UniqueId&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\&quot;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;e.Id.IntegerValue.ToString();
}

<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Add&nbsp;JSON&nbsp;strings&nbsp;representing&nbsp;all&nbsp;children&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;of&nbsp;this&nbsp;node&nbsp;to&nbsp;the&nbsp;given&nbsp;collection.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;DumpToJsonBottomUp(
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;json_collector,
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;parent_id&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;GetElementById(&nbsp;m_Id&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;id&nbsp;=&nbsp;GetId(&nbsp;e&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;json&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;_json_format_to_store_parent_in_child,
&nbsp;&nbsp;&nbsp;&nbsp;id,&nbsp;GetName(&nbsp;e&nbsp;),&nbsp;parent_id&nbsp;);

&nbsp;&nbsp;json_collector.Add(&nbsp;json&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">TreeNode</span>&nbsp;node&nbsp;<span style="color:blue;">in</span>&nbsp;m_childNodes&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;node.DumpToJsonBottomUp(&nbsp;json_collector,&nbsp;id&nbsp;);
&nbsp;&nbsp;}
}

<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;a&nbsp;JSON&nbsp;string&nbsp;representing&nbsp;this&nbsp;node&nbsp;and</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;including&nbsp;the&nbsp;recursive&nbsp;hierarchical&nbsp;graph&nbsp;of&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;all&nbsp;its&nbsp;all&nbsp;children.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;DumpToJsonTopDown()
{
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;GetElementById(&nbsp;m_Id&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;json_collector&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;();

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">TreeNode</span>&nbsp;child&nbsp;<span style="color:blue;">in</span>&nbsp;m_childNodes&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;json_collector.Add(&nbsp;child.DumpToJsonTopDown()&nbsp;);
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;json_kids&nbsp;=&nbsp;<span style="color:blue;">string</span>.Join(&nbsp;<span style="color:#a31515;">&quot;,&quot;</span>,&nbsp;json_collector&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;json&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;_json_format_to_store_children_in_parent,
&nbsp;&nbsp;&nbsp;&nbsp;GetId(&nbsp;e&nbsp;),&nbsp;GetName(&nbsp;e&nbsp;),&nbsp;json_kids&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;json;
}
</pre>


#### <a name="8"></a>Download and To Do

The current state of this project is available from
the [TraverseAllSystems GitHub repository](https://github.com/jeremytammik/TraverseAllSystems), and the version discussed above
is [release 2017.0.0.9](https://github.com/jeremytammik/TraverseAllSystems/releases/tag/2017.0.0.9).

The next step will consist of the Forge viewer extension implementation displaying a custom panel in the user interface hosting a tree view of the MEP system graphs and implementing two-way linking and selection functionality back and forth between the tree view nodes and the 2D and 3D viewer elements.


#### <a name="9"></a>RevitLookup Updates

A couple of enhancement have been added to [RevitLookup](https://github.com/jeremytammik/RevitLookup) since
I last mentioned it, most lately
by [awmcc90](https://github.com/awmcc90)
and [Shayne Hamel](https://github.com/Shayneham)
to handle exceptions snooping MEP elements, electrical circuits, flex ducts and flex pipes.

Here are the diffs:

- [2017.0.0.5](https://github.com/jeremytammik/RevitLookup/compare/2017.0.0.4...2017.0.0.5) &ndash;
merged pull request #14 by Shayneham to handle exceptions snooping flex pipe and duct lacking levels etc.
- [2017.0.0.4](https://github.com/jeremytammik/RevitLookup/compare/2017.0.0.3...2017.0.0.4) &ndash;
merged pull request #13 by awmcc90 to skip mepSys.Elements for OST_ElectricalInternalCircuits category

Thank you very much for those improvements!

If you run into any issues with RevitLookup yourself, please fork the repository, implement and test your changes, and issue a pull request for me to integrate them back into the master branch.

Thank you!
