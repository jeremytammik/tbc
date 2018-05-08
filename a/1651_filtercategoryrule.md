<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 14200462 [Using FilterCategoryRule in the Revit API]
  https://forums.autodesk.com/t5/revit-api-forum/using-filtercategoryrule-in-the-revit-api/m-p/7983645

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

&ndash; 
...

--->

### How to Use FilterCategoryRule

[@CaptainDan](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1003305) raised
a pertinent question in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [using `FilterCategoryRule` in the Revit API](https://forums.autodesk.com/t5/revit-api-forum/using-filtercategoryrule-in-the-revit-api/m-p/7983645):

**Question:** I have three questions regarding the filter rule represented by the class `FilterCategoryRule`:

1. What does this rule correspond to in the Revit user interface?
2. If this filters by category, then what is the relationship between the set of categories assigned to this rule and the set of categories passed
to [`ParameterFilterElement.Create`](http://www.revitapidocs.com/2018.1/afa22520-52de-c6b3-fac8-246fe2f8e4fe.htm)?
3. How do I successfully create a filter based on this filter rule?

Whenever I try to create a filter using this filter rule, it throws a Revit exception of type `InternalException` saying:

- *An internal error has occurred*

I tried the same code in Revit 2017 and 2018 with the same exception occurring.

I searched the internet and SDK and couldn't find much mention of it anywhere.

The Revit API help docs on [FilterCategoryRule](http://www.revitapidocs.com/2018.1/7df5b10b-c423-b5c8-6492-1274d7a447d9.htm) just
say *A filter rule that matches elements of a set of categories*.



**Answer:** Yes, the only significant mention of this that I can find is from The Building Coder
on [What's New in the Revit 2014 API](http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html)
[small enhancements &amp; interface changes](http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html#3):

####<a name="3"></a>FilterCategoryRule

The new class FilterCategoryRule can be used in the definition of a ParameterFilterElement.
It represents a filter rule that matches elements of a set of categories.

The related method:

- ParameterFilterElement.AllCategoriesFilterable()

has been replaced by

- FilterCategoryRule.AllCategoriesFilterable()

I passed this on to the development team, and they reply:

Q1. What does `FilterCategoryRule` correspond to in the Revit user interface?

A1. There is no direct correspondence in Revit UI, this is the FilterRule to be used in API.

Q2. If this filters by category, then what is the relationship between the set of categories assigned to this rule and the set of categories passed to `ParameterFilterElement.Create()`?

A2. There is no relation to `ParameterFilterElement.Create()`.

Q3. How do I successfully create a filter based on this filter rule?

A3. You cannot create a `ParameterFilterElement` based on this `FilterCategoryRule`, but you can create an `ElementParameterFilter`, an API construct, based on it.

Here is an example:

<pre class="code">
  // Find all walls and windows in the document

  <span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;cats&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;();
  cats.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Walls&nbsp;)&nbsp;);
  cats.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Windows&nbsp;)&nbsp;);
  <span style="color:#2b91af;">FilterCategoryRule</span>&nbsp;f&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterCategoryRule</span>(&nbsp;cats&nbsp;);
   
  <span style="color:#2b91af;">ElementParameterFilter</span>&nbsp;wallsAndWindows&nbsp;
  &nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(&nbsp;f,&nbsp;<span style="color:blue;">true</span>&nbsp;);
   
  <span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;doorsAndWindows&nbsp;
  &nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;wallsAndWindows&nbsp;);
</pre>

Just as [Benoit](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4496032) points
out, this can be seen as an extension of
the [ElementCategoryFilter](http://www.revitapidocs.com/2018.1/b492ddf4-3058-8f9b-dfcc-8d5c4abb3605.htm)
and theassociated quick filter shortcut methods 
[OfCategory](http://www.revitapidocs.com/2018.1/c3523c35-4a07-9723-3c28-de3cc47b2ad0.htm)
and [OfCategoryId](http://www.revitapidocs.com/2018.1/63304108-73f8-844e-82fc-5b8fad9839b0.htm).

<center>
<img src="img/steel_ruler_closeup.jpg" alt="Steel rule" width="320"/>
</center>
