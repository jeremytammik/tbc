<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>

</head>

<!---

- interesting approach and more effective filtering for unknown class
  Deleting unpurgeable viewport types through API
  https://forums.autodesk.com/t5/revit-api-forum/deleting-unpurgeable-viewport-types-through-api/m-p/12741221#M78537
  Sean_Page  in reply to: pieter4
  2024-04-29 07:57 AM
  Here is what I use to get Viewport Types. It may not be super language stable, but it works great for me.
  FilterRule rule = ParameterFilterRuleFactory.CreateEqualsRule(new ElementId((Int64)BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM), "Viewport");
  ElementParameterFilter filter = new ElementParameterFilter(rule);
  IList<ElementType> viewportTypes;
  using(FilteredElementCollector fec = new FilteredElementCollector(doc).OfClass(typeof(ElementType)).WherePasses(filter))
  {
    viewportTypes = fec.Cast<ElementType>().ToList();
  }
  return viewportTypes;


twitter:

 the @AutodeskRevit #RevitAPI #BIM @DynamoBIM

&ndash; ...

linkedin:



#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
<a href="img/.gif"><p style="font-size: 80%; font-style:italic">Click for animation</p></a>
</center>

-->

### Filter for Anonymous Type

####<a name="2"></a>

Sometimes, the BIM element that you need to access is hard to identify and filter out.
Some elements are not simply identifiable by category or class.
[Sean Page](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3064449) shares
an interesting approach and an effective approach to filter for such an element in the thread
on [deleting unpurgeable viewport types through API](https://forums.autodesk.com/t5/revit-api-forum/deleting-unpurgeable-viewport-types-through-api/m-p/12741221):

**Question:** When you copy views (including schedules) from one project to another, it brings in the default viewport type of that project.
That viewport type becomes unpurgeable.
So if you do this with enough files, you get a whole bunch of unpurgeable viewport types in your project.

We have a few of these unpurgeable viewport types in our template and I'm looking for a way to clean this up without being forced to recreate the template, which would take me weeks.

Anyone any ideas on how I could achieve this through the API (if possible at all)?

**Answer:**
Here is what I use to get Viewport Types.
It uses a parameter filter to return all element types whose SYMBOL_FAMILY_NAME_PARAM equals "Viewport".
It may not be super language stable, but it works great for me:

<pre><code class="language-cs">  FilterRule rule
    = ParameterFilterRuleFactory.CreateEqualsRule(
      new ElementId( (Int64)
        BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM),
      "Viewport");

  ElementParameterFilter filter = new ElementParameterFilter(rule);
  IList&lt;ElementType&gt; viewportTypes;

  using( FilteredElementCollector fec
    = new FilteredElementCollector(doc)
      .OfClass( typeof(ElementType) )
      .WherePasses(filter) )
  {
    viewportTypes = fec.Cast&lt;ElementType&gt;().ToList();
  }
  return viewportTypes;</code></pre>

Many thanks to Sean for the nice and effective solution.



</code></pre>

