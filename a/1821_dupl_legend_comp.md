<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- https://thebuildingcoder.typepad.com/blog/2010/05/duplicate-legend-component.html#comment-4752201924

twitter:

 in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Duplicate Legend Component and Lat Long to Metres in Python

#### <a name="2"></a>Duplicate Legend Component in Python

In a [comment](https://thebuildingcoder.typepad.com/blog/2010/05/duplicate-legend-component.html#comment-4752201924) on
the workaround to [duplicate a legend component](https://thebuildingcoder.typepad.com/blog/2010/05/duplicate-legend-component.html),
Oliwer Kulpa demonstrates how to set the `LEGEND_COMPONENT` built-in parameter after copying a legend component:

**Question:** Any news about this Legend Component API?
Is there any way I can at least do a Symbol Swap inside the Legend View?

I'm trying the following code:

<pre class="code">
  element1.get_Parameter(
    BuiltInParameter.LEGEND_COMPONENT)
      .Set(element2.Id);
</pre>

However, that produces an error saying, *The component you have selected is not visible in the selected view.*

**Solution:** I found out that you need to feed `FamilyType.Id` when setting up a copied legend component:

<pre class="code">
  CopiedLegendComponent.get_Parameter(
    BuiltInParameter.LEGEND_COMPONENT)
      .Set(FamilyType.Id)
</pre>

It worked for me!

Since it is a part of a larger Python Script in Dynamo, I've extracted the following code fragment which works on a legend view with one single legend component.

I hope it is readable enough for everyone:

<pre class="prettyprint">
  doc = DocumentManager.Instance.CurrentDBDocument
  current_view = doc.ActiveView
  
  # Get legend component in current view
  
  existing_legend_component = FilteredElementCollector(
    doc, doc.ActiveView.Id)
      .OfCategory(BuiltInCategory.OST_LegendComponents)
      .FirstElement()
  
  # Get door family type id
  
  door_family_type = FilteredElementCollector(doc)
    .OfCategory(BuiltInCategory.OST_Doors)
    .WhereElementIsElementType()
    .FirstElement()
  
  # Start Transaction
  
  TransactionManager.Instance.EnsureInTransaction(doc)
  
  # Copy legend and set new Id to represent new element
  
  new_legend_component = ElementTransformUtils
    .CopyElement(doc, existing_legend_component.Id,
      XYZ(10, 0,0))
  
  # The result of CopyElement is a list of Ids,
  # so fetch the first element from copied elements
  
  doc.GetElement(new_legend_component[0])
    .get_Parameter(BuiltInParameter.LEGEND_COMPONENT)
    .Set(door_family_type.Id)
  
  doc.Regenerate()
  
  # End Transaction
  
  TransactionManager.Instance.TransactionTaskDone()
</pre>

Here is a picture of the result of copying a window legend component and setting it to a door family type:

<center>
<img src="img/duplicate_legend_component.png" alt="Duplicate legend component" title="Duplicate legend component" width="450"/> <!-- 900 -->
</center>

Many thanks to Oliwer for sharing this useful discovery.







<pre class="code">
</pre>

**Response:** 

**Answer:** 




#### <a name="3"></a>Convert Latitude and Longitude to Metres in Python



