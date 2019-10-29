<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

[Revit : Get All ViewSheet is very slow](https://stackoverflow.com/questions/58593436/revit-get-all-viewsheet-is-very-slow)

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Generating Graphics



####<a name="2"></a> 
 
<center>
<img src="img/.png" alt="" width="100">
</center>

####<a name="3"></a> 

####<a name="4"></a> 

**Question:** 

**Answer:** 


####<a name="5"></a> 

[Revit : Get All ViewSheet is very slow](https://stackoverflow.com/questions/58593436/revit-get-all-viewsheet-is-very-slow)

for a plugin I need to get all the viewsheet in the rvt file and display informations from them in an xaml dialog
but my process is very very slow the first time I use it 
(with the debuger : 500 ms for 83 viewplan , it is very slow without the debuger too)
(if I execute my code again, the execution is istantaneous)

my code below 

can you help me ?

thanks in advance

Luc

&lt; !!!

<pre class="code">
  protected IEnumerable<Element> GetAllEl( Document doc )
  {
    return new FilteredElementCollector( doc )
      .OfCategory( BuiltInCategory.OST_Sheets )
      .WhereElementIsNotElementType()
      .OfClass( typeof( ViewSheet ) );
  }
</pre>

I do not think there is currently a known generic solution for that problem.

Here is a recent discussion with the development team on this:

**Question:** for a given element id, we need to find the list of sheet ids displaying it.
Current solution: we loop through all the sheets and views and use `FilteredElementCollector( doc, sheet.Id)`
With the results from that, we perform one more call to `FilteredElementCollector( doc, view.Id)` and look for the element id.
Issue: the current solution takes a lot of time and displays a Revit progress bar saying `Generating graphics`.
Is there any better way to know if a given element id is available in the sheet or not?
For example, something like this would be very useful:

<pre class="code">
  getAllSheets(ElementId) // returns array of sheet id
  hasGuid(ElementId,sheetId) // return true/false
</pre>

Does the API provide any such methods, to check whether a given ElementId is available in the sheet?

**Answer:** So the goal is to find a view that displays a particular element on a sheet?
Many model elements could be visible on multiple views, while most annotation elements are typically present only in one view.
What type of elements are you checking for?
And what will you do with that info?

**Response:** the goal is to find a view that displays a particular element on a sheet.
It can be any type of element.

**Answer:** Here are some previous related discussions:

- [Determining Views Showing an Element](https://thebuildingcoder.typepad.com/blog/2016/12/determining-views-showing-an-element.html)
- The inverse, [Retrieving Elements Visible in View](https://thebuildingcoder.typepad.com/blog/2017/05/retrieving-elements-visible-in-view.html)

**Response:** The problem is that the first call to `FilteredElementCollector( doc, viewId )` shows `generating graphics` in the progress bar.
Only the first time search does so. The second time, search on the same view has no issues with performance.

**Answer:** The first time is slow because in order to iterate on the elements visible in a view the graphics for that view must be generated.
I can't think of a workaround to get a precise answer.
You might be able to skip sheets which don't have model views in their viewport list to save a bit of time.
Some sheets may only have drafting views and schedules and annotations.

The development team provided a very helpful suggestion which helped work around the `generating graphics` call in a special case,
to [Loop through sheets - generating graphics](https://forums.autodesk.com/t5/revit-api-forum/loop-through-sheets-generating-graphics/m-p/8719256).

Maybe you can optimise in a similar manner for your specific case?


