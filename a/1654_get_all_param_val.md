<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- implement input: categories, output: dictionary of all elements with all their parameter values
  https://github.com/jeremytammik/rvtmetaprop -- read properties from Forge and update model accordingly
  https://github.com/jeremytammik/AdnRevitApiLabsXtra -- Lab4_2_ExportParametersToExcel

Getting All Parameter Values through the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/getparams

How to export all the Revit data to an external database?
It is very simple to extract all the parameter data.
Let's implement a solution to do so
&ndash; Existing sample implementations 
&ndash; Black box input 
&ndash; Choices for the output and its structure 
&ndash; Retrieve parameter values from an element 
&ndash; FilterCategoryRule versus category filters 
&ndash; Category description extension method 
&ndash; Retrieve parameter data for all elements of given categories 
&ndash; External command <code>Execute</code> mainline 
&ndash; Sample run results
&ndash; Download...

--->

### Getting All Parameter Values

People sometimes ask how to export all the Revit data to an external database.

Obviously, a huge amount of data is embedded in BIM specific constraints and relationships that are hard to extract.

It is very simple to extract all the parameter data, though, as you can see by looking at the numerous existing samples that do so.

Let's implement yet another one ourselves:

- [Existing sample implementations](#2) 
- [Black box input](#3) 
- [Choices for the output and its structure](#4) 
- [Retrieve parameter values from an element](#5) 
- [FilterCategoryRule versus category filters](#6) 
- [Category description extension method](#7) 
- [Retrieve parameter data for all elements of given categories](#8) 
- [External command `Execute` mainline](#9) 
- [Sample run results](#10) 
- [Download](#11) 


####<a name="2"></a>Existing Sample Implementations

In fact, this kind of data access and parameter value extraction is achieved by one of the very
first [ADN Revit API training labs](https://github.com/jeremytammik/AdnRevitApiLabsXtra),
the external
command [Lab4_2_ExportParametersToExcel in Labs4.cs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs4.cs#L268-L509),
which is very similar to the official Revit SDK ArchSample.

It creates a dictionary of all elements with a valid category and sorts them by category name.

Next, for each category, it determines a list of all parameters attached to any one of the elements.

Finally, for each element, it reads all its parameter values and exports them to Excel.

In the process, it creates an Excel workbook to store the data, and, for each category, a worksheet to store all the element data.

Here are some discussions of it:

- [Exporting parameter data to Excel, and re-importing](http://thebuildingcoder.typepad.com/blog/2012/09/exporting-parameter-data-to-excel.html)
- [ArchSample and retrieving element properties](http://thebuildingcoder.typepad.com/blog/2015/06/archsample-active-transaction-and-adnrme-for-revit-mep-2016.html#2)

Another sample that goes about it even more professionally is RDBLink. It was originally included in the Revit SDK sample collection, and later removed to be maintained as a separate proprietary subscription product:

- [Integration with a database or ERP system](http://thebuildingcoder.typepad.com/blog/2009/07/integration-with-a-database-or-erp-system.html)
- [Adding a column to RDBLink export](http://thebuildingcoder.typepad.com/blog/2009/11/adding-a-column-to-rdblink-export.html)
- [Parameter access and scheduling](http://thebuildingcoder.typepad.com/blog/2010/05/parameter-access-and-scheduling.html)
- [ODBC export](http://thebuildingcoder.typepad.com/blog/2012/11/survey-and-project-base-point.html#3)
- [RDBLink and exporting data from Revit](http://thebuildingcoder.typepad.com/blog/2016/02/reorg-fomt-devcon-ted-qr-custom-exporter-quality.html#6)

When an RVT file is translated by [Forge](https://autodesk-forge.github.io), the process also captures all the BIM element parameters.
Since it is easy to modify them and add new ones in the Forge viewer, I implemented an add-in to support a full read-write round-trip
workflow, [RvtMetaProp](https://github.com/jeremytammik/rvtmetaprop):

- [Forge meta property editor and RvtMetaProp Revit add-in &ndash; executive summary](http://thebuildingcoder.typepad.com/blog/2017/10/rational-bim-programming-at-au-darmstadt.html#5.5)
- [Use Forge or spreadsheet to create shared parameters](http://thebuildingcoder.typepad.com/blog/2017/09/use-forge-or-spreadsheet-to-create-shared-parameters.html) 

Today, I thought I would isolate the most basic and generic functionality conceivable to support this kind of workflow, by implementing a simple black box that takes a very specific input and returns a specific output for that:

- Input: a list of BIM categories
- Output: for each category, a dictionary mapping all the elements of that category to a collection of all their parameter values.


####<a name="3"></a>Black Box Input

The list of categories can simply consist of an array of built-in categories like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;List&nbsp;all&nbsp;built-in&nbsp;categories&nbsp;of&nbsp;interest</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>[]&nbsp;_cats&nbsp;=
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Doors,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Rooms,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Windows
&nbsp;&nbsp;};
</pre>

Defining the output and its structure is more challenging and depends on the exact requirements.


####<a name="4"></a>Choices for the Output and its Structure

As simple as this sounds, there are quite a couple of choices to be made:

- Parameter identification: name? multiple names?
- Parameter value storage: string representation, or underlying database value?
- Element parameters to retrieve: `Parameters` property, `GetOrderedParameters` method, etc.

In order to simplify things, the parameter values could all be returned as strings.
In the simplest solution, the display strings shown in the Revit user interface, returned by the `AsValueString` method.
A more complex solution might return their real underlying database values instead.

Ideally, the parameters could be identified by their name.

In that case, in the return value could be structured like this, as a dictionary mapping category names to dictionaries mapping element unique ids to dictionaries mapping each elements parameter name to the corresponding value:

<pre class="code">
  <span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
  &nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:blue;">string</span>&gt;&gt;&gt;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map_cat_to_uid_to_param_values;
</pre>

Unfortunately, though, parameter names are not guaranteed to be unique.

Therefore, it may be impossible to include all parameter values in a dictionary using the parameter name as a key.

Therefore, I resorted to the simple solution of returning a list of strings instead, where each string is formatted by separating the parameter name and the value by an equal sign '=' like this:

<pre class="code">
  param_values.Add(&nbsp;<span style="color:blue;">string</span>.Format(&nbsp;<span style="color:#a31515;">&quot;{0}={1}&quot;</span>,&nbsp;
  &nbsp;&nbsp;p.Definition.Name,&nbsp;p.AsValueString()&nbsp;)&nbsp;);
</pre>

That leads to the following structure for the return value:

<pre class="code">
  <span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
  &nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&gt;&gt;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map_cat_to_uid_to_param_values;
</pre>

Finally, we have several different possibilities to retrieve the parameters from the element.

Two obvious choices are to use the `Element` `Parameters` property that retrieves a set containing all the parameters.

Another one offered by the API is the `GetOrderedParameters` method that gets the visible parameters in the order they appear in the UI.

Finally, you can attempt to retrieve values for all the built-in parameters; this approach is used by
the [RevitLookup snooping tool](https://github.com/jeremytammik/RevitLookup) and
the [BipChecker](https://github.com/jeremytammik/BipChecker) built-in parameter checker.


####<a name="5"></a>Retrieve Parameter Values from an Element

Based on the choices described above, and opting for the simplest solution, we retrieve the parameter values from a given element like this:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;all&nbsp;the&nbsp;parameter&nbsp;values&nbsp;&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;deemed&nbsp;relevant&nbsp;for&nbsp;the&nbsp;given&nbsp;element</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;in&nbsp;string&nbsp;form.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;GetParamValues(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
{
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Two&nbsp;choices:&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Element.Parameters&nbsp;property&nbsp;--&nbsp;Retrieves&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;a&nbsp;set&nbsp;containing&nbsp;all&nbsp;&nbsp;the&nbsp;parameters.</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;GetOrderedParameters&nbsp;method&nbsp;--&nbsp;Gets&nbsp;the&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;visible&nbsp;parameters&nbsp;in&nbsp;order.</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">Parameter</span>&gt;&nbsp;ps&nbsp;=&nbsp;e.GetOrderedParameters();
 
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;param_values&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ps.Count&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;p&nbsp;<span style="color:blue;">in</span>&nbsp;ps)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;AsValueString&nbsp;displays&nbsp;the&nbsp;value&nbsp;as&nbsp;the&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;user&nbsp;sees&nbsp;it.&nbsp;In&nbsp;some&nbsp;cases,&nbsp;the&nbsp;underlying</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;database&nbsp;value&nbsp;returned&nbsp;by&nbsp;AsInteger,&nbsp;AsDouble,</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;etc.,&nbsp;may&nbsp;be&nbsp;more&nbsp;relevant.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;param_values.Add(&nbsp;<span style="color:blue;">string</span>.Format(&nbsp;<span style="color:#a31515;">&quot;{0}={1}&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p.Definition.Name,&nbsp;p.AsValueString()&nbsp;)&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;param_values;
}
</pre>


####<a name="6"></a>FilterCategoryRule versus Category Filters

Before we can retrieve the parameter data from the elements, we need to retrieve the elements from the Revit database.

As always, this is achieved using a filtered element collector.

Just last week,
we [clarified the use of the `FilterCategoryRule` class](http://thebuildingcoder.typepad.com/blog/2018/05/how-to-use-filtercategoryrule.html).

That discussion led me to believe that it could be used to achieve exactly what I need, filtering for all elements belonging to a given list of categories.

I implemented that code, and it does not seem to do what I expect at all.

In fact, the following code appears to be returning all elements, including those with null categories:

<pre class="code">
<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;ids
&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">BuiltInCategory</span>&gt;(&nbsp;cats&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.ConvertAll&lt;<span style="color:#2b91af;">ElementId</span>&gt;(&nbsp;c
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;(<span style="color:blue;">int</span>)&nbsp;c&nbsp;)&nbsp;);
 
<span style="color:#2b91af;">FilterCategoryRule</span>&nbsp;r&nbsp;
&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterCategoryRule</span>(&nbsp;ids&nbsp;);
 
<span style="color:#2b91af;">ElementParameterFilter</span>&nbsp;f
&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(&nbsp;r,&nbsp;<span style="color:blue;">true</span>&nbsp;);
 
<span style="color:green;">//&nbsp;Run&nbsp;the&nbsp;collector</span>
 
<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;els
&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsViewIndependent()
&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;f&nbsp;);
</pre>

In the end, I resorted to using my tried and proven technique using a logical `OR` of individual category filters instead, like this:

<pre class="code">
<span style="color:green;">//&nbsp;Use&nbsp;a&nbsp;logical&nbsp;OR&nbsp;of&nbsp;category&nbsp;filters</span>
 
<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">ElementFilter</span>&gt;&nbsp;a
&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementFilter</span>&gt;(&nbsp;cats.Length&nbsp;);
 
<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;bic&nbsp;<span style="color:blue;">in</span>&nbsp;cats&nbsp;)
{
&nbsp;&nbsp;a.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementCategoryFilter</span>(&nbsp;bic&nbsp;)&nbsp;);
}
 
<span style="color:#2b91af;">LogicalOrFilter</span>&nbsp;categoryFilter
&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">LogicalOrFilter</span>(&nbsp;a&nbsp;);
 
<span style="color:green;">//&nbsp;Run&nbsp;the&nbsp;collector</span>
 
<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;els
&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsViewIndependent()
&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;categoryFilter&nbsp;);
</pre>

It is shorter, and, above all, it works!


####<a name="7"></a>Category Description Extension Method

Finally, in order to define the key string for the category dictionary, I implemented a little helper method on the built-in category enum:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">JtBuiltInCategoryExtensionMethods</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;a&nbsp;descriptive&nbsp;string&nbsp;for&nbsp;a&nbsp;built-in&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;category&nbsp;by&nbsp;removing&nbsp;the&nbsp;trailing&nbsp;plural&nbsp;&#39;s&#39;&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;and&nbsp;the&nbsp;OST_&nbsp;prefix.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;Description(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;bic&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s&nbsp;=&nbsp;bic.ToString().ToLower();
&nbsp;&nbsp;&nbsp;&nbsp;s&nbsp;=&nbsp;s.Substring(&nbsp;4&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;s.EndsWith(&nbsp;<span style="color:#a31515;">&quot;s&quot;</span>&nbsp;),&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;plural&nbsp;suffix&nbsp;&#39;s&#39;&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;s&nbsp;=&nbsp;s.Substring(&nbsp;0,&nbsp;s.Length&nbsp;-&nbsp;1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;s;
&nbsp;&nbsp;}
}
</pre>

As explained in the comment, it returns a descriptive string for a built-in category enumeration value by removing its trailing plural 's' and 'OST_' prefix.


####<a name="8"></a>Retrieve Parameter Data for all Elements of Given Categories

With these helper methods in place, we can retrieve the parameter data for all elements of a given list categories like this:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;parameter&nbsp;data&nbsp;for&nbsp;all&nbsp;&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;elements&nbsp;of&nbsp;all&nbsp;the&nbsp;given&nbsp;categories</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&gt;&gt;
&nbsp;&nbsp;GetParamValuesForCats(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>[]&nbsp;cats&nbsp;)
{
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;up&nbsp;the&nbsp;return&nbsp;value&nbsp;dictionary</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&gt;&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map_cat_to_uid_to_param_values
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&gt;&gt;();
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;One&nbsp;top&nbsp;level&nbsp;dictionary&nbsp;per&nbsp;category</span>
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;cat&nbsp;<span style="color:blue;">in</span>&nbsp;cats&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;map_cat_to_uid_to_param_values.Add(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cat.Description(),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&gt;()&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Collect&nbsp;all&nbsp;required&nbsp;elements</span>
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;The&nbsp;FilterCategoryRule&nbsp;as&nbsp;used&nbsp;here&nbsp;seems&nbsp;to&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;have&nbsp;no&nbsp;filtering&nbsp;effect&nbsp;at&nbsp;all!&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;It&nbsp;passes&nbsp;every&nbsp;single&nbsp;element,&nbsp;afaict.&nbsp;</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;ids
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">BuiltInCategory</span>&gt;(&nbsp;cats&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ConvertAll&lt;<span style="color:#2b91af;">ElementId</span>&gt;(&nbsp;c
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;(<span style="color:blue;">int</span>)&nbsp;c&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilterCategoryRule</span>&nbsp;r&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterCategoryRule</span>(&nbsp;ids&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>&nbsp;f
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(&nbsp;r,&nbsp;<span style="color:blue;">true</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Use&nbsp;a&nbsp;logical&nbsp;OR&nbsp;of&nbsp;category&nbsp;filters</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">ElementFilter</span>&gt;&nbsp;a
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementFilter</span>&gt;(&nbsp;cats.Length&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;bic&nbsp;<span style="color:blue;">in</span>&nbsp;cats&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;a.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementCategoryFilter</span>(&nbsp;bic&nbsp;)&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:#2b91af;">LogicalOrFilter</span>&nbsp;categoryFilter
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">LogicalOrFilter</span>(&nbsp;a&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Run&nbsp;the&nbsp;collector</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;els
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsViewIndependent()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;categoryFilter&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;parameter&nbsp;data&nbsp;for&nbsp;each&nbsp;element</span>
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;<span style="color:blue;">in</span>&nbsp;els&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Category</span>&nbsp;cat&nbsp;=&nbsp;e.Category;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;cat&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;element&nbsp;{0}&nbsp;{1}&nbsp;has&nbsp;null&nbsp;category&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.Id,&nbsp;e.Name&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">continue</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;param_values&nbsp;=&nbsp;GetParamValues(&nbsp;e&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;bic&nbsp;=&nbsp;(<span style="color:#2b91af;">BuiltInCategory</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(e.Category.Id.IntegerValue);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;catkey&nbsp;=&nbsp;bic.Description();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;uid&nbsp;=&nbsp;e.UniqueId;
 
&nbsp;&nbsp;&nbsp;&nbsp;map_cat_to_uid_to_param_values[catkey].Add(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uid,&nbsp;param_values&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;map_cat_to_uid_to_param_values;
}
</pre>


####<a name="9"></a>External Command Execute Mainline

The code to run and test this is trivial:

<pre class="code">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&gt;&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map_cat_to_uid_to_param_values;
<span style="color:blue;">#endif</span>&nbsp;<span style="color:green;">//&nbsp;PARAMETER_NAMES_ARE_UNIQUE</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map_cat_to_uid_to_param_values&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;GetParamValuesForCats(&nbsp;doc,&nbsp;_cats&nbsp;);
</pre>

Displaying some relevant portion of the results takes much more:

<pre class="code">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;keys&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;map_cat_to_uid_to_param_values.Keys&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keys.Sort();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">string</span>&nbsp;key&nbsp;<span style="color:blue;">in</span>&nbsp;keys&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&gt;&nbsp;els&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;map_cat_to_uid_to_param_values[key];
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;n&nbsp;=&nbsp;els.Count;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;{0}&nbsp;({1}&nbsp;element{2}){3}&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key,&nbsp;n,&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;n&nbsp;),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.DotOrColon(&nbsp;n&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;0&nbsp;&lt;&nbsp;n&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;uids&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;(&nbsp;els.Keys&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;uid&nbsp;=&nbsp;uids[0];
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;param_values&nbsp;=&nbsp;els[uid];
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;param_values.Sort();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;n&nbsp;=&nbsp;param_values.Count;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;&nbsp;&nbsp;first&nbsp;element&nbsp;{0}&nbsp;has&nbsp;{1}&nbsp;parameter{2}{3}&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;uid,&nbsp;n,&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;n&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Util</span>.DotOrColon(&nbsp;n&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;param_values.ForEach(&nbsp;pv
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;&nbsp;&nbsp;&nbsp;&nbsp;&quot;</span>&nbsp;+&nbsp;pv&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
</pre>


####<a name="10"></a>Sample Run Results

In my test run, I am interested only in door, room and window categories.

From the results, I just display the numbers of elements retrieved, and the parameter values for one single sample element.

Running it in the Revit basic sample project displays the following in the Visual Studio debug output window:

<pre>
door (16 elements):
  first element 59371552-5800-43eb-9ba3-609565158fc5-00067242 has 11 parameters:
    Comments = 
    Finish = 
    Frame Material = 
    Frame Type = 
    Head Height = 2100
    Image = <None>
    Level = Level 1
    Mark = 
    Phase Created = Working Drawings
    Phase Demolished = None
    Sill Height = 0
room (14 elements):
  first element e6ac360b-aaed-4c3b-a130-36b4c2ac9d13-000d1467 has 21 parameters:
    Area = 27 m²
    Base Finish = 
    Base Offset = 0
    Ceiling Finish = 
    Comments = 
    Computation Height = 0
    Department = 
    Floor Finish = 
    Image = <None>
    Level = Level 2
    Limit Offset = 6500
    Name = 
    Number = 
    Occupancy = 
    Occupant = 
    Perimeter = 29060
    Phase = Working Drawings
    Unbounded Height = 6500
    Upper Limit = Level 2
    Volume = 118.32 m³
    Wall Finish = 
window (17 elements):
  first element 6cbabf1d-e8d0-47f0-ac4d-9a7923128d37-0006fb07 has 22 parameters:
    Bottom Hung Casement = No
    Casement Pivot = No
    Casement Swing in Plan = No
    Casement = SH_Aluminum, Anodized Black
    Comments = 
    Frame = SH_Aluminum, Anodized Black
    Glass = <By Category>
    Head Height = 2700
    Height = 2700
    Image = <None>
    Install Depth (from outside) = 80
    Level = Level 2
    Mark = 
    Phase Created = Working Drawings
    Phase Demolished = None
    Rough Height = 2700
    Rough Width = 1500
    Sill Height = 0
    Top Hung Casement = Yes
    Width = 1500
    Window Cill Exterior = SH_Aluminum, Anodized Black
    Window Cill Interior = Wood_Walnut black
</pre>


####<a name="11"></a>Download

I added this code to the
new [module CmdParamValuesForCats.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdParamValuesForCats.cs)
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2019.0.140.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.140.0).

I hope you find it useful.

<center>
<img src="img/meta_editor.png" alt="Forge meta property editor" width="401"/>
</center>

