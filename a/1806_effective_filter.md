<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

twitter:

 in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/combiningedges

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Effective Filtered Element Collector


####<a name="2"></a> Effective Filtered Element Collector

Efficient way to check if an element exists in a view

**Question:** I'm creating a list of views that contain .dwg ImportInstance(s).
For each view in the document, I'm using a `FilteredElementCollector` to get a list of elements meeting the criteria; if this list is not empty, the view is added to the list:

<pre class="code">
  foreach (Element e in viewElements)
  {
    View view = (View) e;
  
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    
    List<Element> elementsInView
      = new FilteredElementCollector(doc, view.Id)
        .OfClass(typeof(ImportInstance))
        .Where(e => e.Category.Name.EndsWith(".dwg"))
        .OfType<Element>()
        .ToList();
        
    stopwatch.Stop();
    
    Debug.WriteLine(view.Name + ": "
      + stopwatch.ElapsedMilliseconds + "ms");
  
    // if the current view contains at least 1 DWG
    // ImportInstance, add the view to the list
    
    if(elementsInView.Count > 0) 
    {
      viewsWithCAD.Add(view);
      continue;
    }
  }
</pre>

The FilteredElementCollector can understandably take more than 4000 ms to collect elements from a view containing many elements.

My goal is only to see if a single element exists in a view &ndash; not to collect all of the elements meeting the criteria; if I could make the FilteredElementCollector stop immediately after finding an element meeting the criteria, that would be helpful. 

I would appreciate any advice on how to achieve this more efficiently.

Thank you.

**Answer by Fair59**, [Frank Aarssen](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518):
Stopping the collector at the first element:

<pre class="code">
  Element e1
    = new FilteredElementCollector(doc, view.Id)
      .OfClass(typeof(ImportInstance))
      .FirstElement();
</pre>

Possible Speed improvement:

- Select all ImportInstances
- If it is not a DWG: exclude from collector in view-loop &rarr; no need for `category.Name` check in loop
- Else: if DWG is ViewSpecific, i.e., is "2D annotation" in view &rarr; owner view contains DWG, can be added to `viewsWithCAD`, and can be excluded from view-loop
 
<pre class="code">
  IEnumerable<ImportInstance> instances
    = new FilteredElementCollector(doc)
      .OfClass(typeof(ImportInstance))
      .Cast<ImportInstance>();

  List<ElementId> toExclude = new List<ElementId>();
  foreach(ImportInstance instance in instances)
  {
    if ( !instance.Category.Name.EndsWith(".dwg"))
    {
      toExclude.Add(instance.Id);
      continue;
    }
    if( instance.ViewSpecific) // dwg only exists in ownerview
    {
      View ownerview = doc.GetElement(instance.OwnerViewId) as View;
      viewsWithCAD.Add(ownerview);
      if( viewElements.Contains(ownerview))
        viewElements.Remove(ownerview);
    }
  }
  
  foreach (Element e in viewElements)
  {
    View view = (View) e;
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    Element e1 = null;
    if (toExclude.Count>0)
    {
      e1 = new FilteredElementCollector(doc, view.Id)
        .Excluding(toExclude)
        .OfClass(typeof(ImportInstance))
        .FirstElement();
    } else{
      e1 = new FilteredElementCollector(doc, view.Id)
        .OfClass(typeof(ImportInstance))
        .FirstElement();
    }
    stopwatch.Stop();
    Debug.WriteLine(view.Name + ": "
      + stopwatch.ElapsedMilliseconds + "ms");

    // if the current view contains at least 1 DWG
    // ImportInstance, add the view to the list

    if(e1!=null)
    {
      viewsWithCAD.Add(view);
    }
  }
</pre>

**Notes:** Many thanks for the interesting question, and many thanks to Fair59 for yet another extremely knowledgeable and helpful solution!

I do keep pointing out that converting a filtered element collector to a List is an inefficient thing to do, if you can avoid it.

It forces the collector to retrieve all the data, convert it to the .NET memory space, duplicate it, costing time and space.

For the same reasons, it is much more efficient to test and apply as many filters as possible within the Revit memory space before passing any data across to .NET.

In this case, you can test the parameter values using a parameter filter instead of the LINQ post-processing that you are applying in you sample code snippet.

As Fair59 points out and we have discussed in the past, you can cancel a collector as soon as your target has been reached:

- [Aborting Filtered Element Collection ](https://thebuildingcoder.typepad.com/blog/2019/02/cancelling-filtered-element-collection.html)

So, you can save time and space in several ways:

Use a parameter filter instead of LINQ post-processing

Do not convert to a List

Both of these force the filtered element collector to retrieve and return all results.

Here is an explanation of the various types of filters versus post-processing in .NET:

- [Slow, Slower Still and Faster Filtering](https://thebuildingcoder.typepad.com/blog/2019/04/slow-slower-still-and-faster-filtering.html)

Here are some discussions and a benchmark of the results of using a parameter filter versus LINQ post-processing:

- [Filtering for a Specific Parameter Value](https://thebuildingcoder.typepad.com/blog/2018/06/forge-tutorials-and-filtering-for-a-parameter-value.html#3)
- [Filtered Element Collector Benchmark](https://thebuildingcoder.typepad.com/blog/2019/05/filtered-element-collector-benchmark.html#3)

We also discussed the issue of finding all views displaying an element a couple of times in the past:

- [Views Displaying Given Element](https://thebuildingcoder.typepad.com/blog/2014/05/views-displaying-given-element-svg-and-nosql.html#6)
- [Determining Views Showing an Element](https://thebuildingcoder.typepad.com/blog/2016/12/determining-views-showing-an-element.html)
- [Retrieving Elements Visible in View](https://thebuildingcoder.typepad.com/blog/2017/05/retrieving-elements-visible-in-view.html)
- [Can You Avoid Generating Graphics?](https://thebuildingcoder.typepad.com/blog/2019/10/generating-graphics-and-collecting-assets.html#2)



####<a name="3"></a> 

####<a name="4"></a> 

####<a name="5"></a> 

<center>
<img src="img/.png" alt="" width="100"> <!--680-->
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Answer:** Two steps:

**Response:** 

<pre class="code">
</pre>

Many thanks to  for raising and solving this interesting task.
