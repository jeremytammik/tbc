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


RevitLookup search by element and unique id in #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/lookupsearchbyid

Александр Пекшев aka Modis @Pekshev implemented another useful RevitLookup enhancement to search and snoop elements by element id or unique id
&ndash; Search and snoop by element id or unique id
&ndash; File changes
&ndash; The built-in Select by Id command, Zoom To and StringSearch
&ndash; RevitLookup update...

--->

### RevitLookup Search by Element and Unique Id

Александр Пекшев aka Modis [@Pekshev](https://github.com/Pekshev) recently
submitted a pull request
to [snoop the stable representation of references](http://thebuildingcoder.typepad.com/blog/2018/03/export-geometry-and-snoop-stable-representation-of-reference.html#2)
for [RevitLookup](https://github.com/jeremytammik/RevitLookup).

Now he implemented another useful enhancement to search and snoop elements by element id or unique id:

- [Search and snoop by element id or unique id](#2) 
- [File changes](#3) 
- [The built-in Select by Id command, Zoom To and StringSearch](#4) 
- [RevitLookup update](#5) 


####<a name="2"></a>Search and Snoop by Element Id or Unique Id

Alexander's RevitLookup [pull request #42 adds a 'search by and snoop' command](https://github.com/jeremytammik/RevitLookup/pull/42):

Add "Search by and snoop" command that allows you to search and snoop for elements by condition:

<center>
<img src="img/revitlookup_search_snoop_cmd.png" alt="Search and Snoop command" width="380"/>
</center>

A small addition to the project: search for items by `ElementId` or `UniqueId` and then snoop them.

Search options implemented in the form of a drop-down list provide for the possibility of further future expansion (for example, search by parameters or something similar):

<center>
<img src="img/revitlookup_search_snoop_form.png" alt="Search and Snoop form" width="430"/>
</center>

This view option can be useful for developers who receive an `ElementId` while debugging an application and need to learn about it later in Revit.

Thank you very much, Alexander, for this great functionality added with minimal implementation effort, as you can see by looking and
the [pull request files changed](https://github.com/jeremytammik/RevitLookup/pull/42/files).

####<a name="3"></a>File Changes

Besides the module *RevitLookup\CS\Snoop\Forms\SearchBy.cs* defining the new form, the only changes are the following to add the new menu entry and implement the new external command:

In App.cs:

<pre class="code">
  optionsBtn.AddPushButton(<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(
    <span style="color:#a31515;">&quot;Search&nbsp;and&nbsp;Snoop...&quot;</span>,&nbsp;
    <span style="color:#a31515;">&quot;Search&nbsp;and&nbsp;Snoop...&quot;</span>,&nbsp;
    ExecutingAssemblyPath,&nbsp;<span style="color:#a31515;">&quot;
    RevitLookup.CmdSearchBy&quot;</span>));
</pre>

In TestCmds.cs:

<pre class="code">
<span style="color:blue;">using</span>&nbsp;RevitLookup.Snoop.Forms;

. . .

<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Search&nbsp;by&nbsp;and&nbsp;Snoop&nbsp;command:&nbsp;Browse&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;elements&nbsp;found&nbsp;by&nbsp;the&nbsp;condition</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
[<span style="color:#2b91af;">Regeneration</span>(&nbsp;<span style="color:#2b91af;">RegenerationOption</span>.Manual&nbsp;)]
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">CmdSearchBy</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;cmdData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;msg,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elems&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;result;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Snoop.CollectorExts.<span style="color:#2b91af;">CollectorExt</span>.m_app&nbsp;=&nbsp;cmdData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;revitDoc&nbsp;=&nbsp;cmdData.Application.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;dbdoc&nbsp;=&nbsp;revitDoc.Document;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Snoop.CollectorExts.<span style="color:#2b91af;">CollectorExt</span>.m_activeDoc&nbsp;=&nbsp;dbdoc;&nbsp;<span style="color:green;">//&nbsp;TBD:&nbsp;see&nbsp;note&nbsp;in&nbsp;CollectorExt.cs</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SearchBy</span>&nbsp;searchByWin&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">SearchBy</span>(&nbsp;dbdoc&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ActiveDoc</span>.UIApp&nbsp;=&nbsp;cmdData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;searchByWin.ShowDialog();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;result&nbsp;=&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;System.<span style="color:#2b91af;">Exception</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;msg&nbsp;=&nbsp;e.Message;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;result&nbsp;=&nbsp;<span style="color:#2b91af;">Result</span>.Failed;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;result;
&nbsp;&nbsp;}
}
</pre>


####<a name="4"></a>The Built-in Select by Id Command, Zoom To and StringSearch

Revit does already provide a built-in command that you should be aware of providing similar functionality, at least for selecting an element by element id: Manage > Inquiry > Select by ID > Show:

<center>
<img src="img/select_by_id_cmd.png" alt="Select by ID command" width="476"/>
</center>

It prompts you to enter the element id and adds the element to the current selection with the option to zoom to it graphically as well:

<center>
<img src="img/select_by_id_form.png" alt="Select by ID form" width="342"/>
</center>

Once you have selected an element in this manner, you can use the traditional RevitLookup 'snoop current selection' to explore its data.

The 'zoom to' option is very similar to 
the [zoom to selected elements](http://thebuildingcoder.typepad.com/blog/2018/03/switch-view-or-document-by-showing-elements.html#3) functionality
discussed last week, implemented using `ShowElements`.

This could obviously be integrated into RevitLookup as well, if so desired.

Another useful piece of functionality that could be integrated is an adaptation of my
old [StringSearch](http://thebuildingcoder.typepad.com/blog/2016/01/all-model-text-stringsearch-2016-and-new-jobs.html) add-in,
originally [ADN Plugin of the Month](http://thebuildingcoder.typepad.com/blog/2011/10/string-search-adn-plugin-of-the-month.html) back
in 2011.

It implements the possibility to search for, list modelessly, select and zoom to elements based on strings in their properties, parameters and other data, including support for regular expressions.


####<a name="5"></a>RevitLookup Update

Back to here and now, though.

I integrated Alexander's code and fixed some minor compilation errors due to working with Visual Studio 2015.

He presumably used a newer version supporting more permissive C# syntax to create the pull request.

The new functionality is included 
in [RevitLookup](https://github.com/jeremytammik/RevitLookup)
[release 2018.0.0.8](https://github.com/jeremytammik/RevitLookup/releases/tag/2018.0.0.8).

