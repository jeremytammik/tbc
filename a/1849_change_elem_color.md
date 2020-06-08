<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 16576354 [material assignment]

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Changing Element Colour and Material

The question of how to change the colour and material of individual elements has come up repeatedly over time

We discussed
how to [change element colour](https://thebuildingcoder.typepad.com/blog/2011/03/change-element-colour.html) way
back in 2011.
The principle remains unchanged, but some API details have changed a bit since then.

Various solutions to change colour have been provided in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160),
e.g., [how to change the colour of an element](https://forums.autodesk.com/t5/revit-api-forum/how-change-the-color-a-element/m-p/5651177)
and [changing colour by element id + colour palette](https://forums.autodesk.com/t5/revit-api-forum/change-color-by-element-id-color-palette/m-p/4768209),
but most of them are also out of date.

So, to pick this up once again, I added a new sample external command `CmdChangeElementColor`
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).


####<a name="2"></a> Change Element Colour in a View

**Question:** How can I change the element display colour in a given view?

**Answer:** You can use
the [OverrideGraphicSettings class](https://www.revitapidocs.com/2020/eb2bd6b6-b7b2-5452-2070-2dbadb9e068a.htm) and
its [SetProjectionLineColor method](https://www.revitapidocs.com/2020/6b780d28-87fb-2ba6-04fa-f973d85ca552.htm) to
change the colour of a selected element in the current view like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">void</span>&nbsp;ChangeElementColor(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;id&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Color</span>&nbsp;color&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Color</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span style="color:blue;">byte</span>)&nbsp;200,&nbsp;(<span style="color:blue;">byte</span>)&nbsp;100,&nbsp;(<span style="color:blue;">byte</span>)&nbsp;100&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">OverrideGraphicSettings</span>&nbsp;ogs&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">OverrideGraphicSettings</span>();
&nbsp;&nbsp;&nbsp;&nbsp;ogs.SetProjectionLineColor(&nbsp;color&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Change&nbsp;Element&nbsp;Color&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.ActiveView.SetElementOverrides(&nbsp;id,&nbsp;ogs&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;uiapp.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;view&nbsp;=&nbsp;doc.ActiveView;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;id;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Selection</span>&nbsp;sel&nbsp;=&nbsp;uidoc.Selection;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;r&nbsp;=&nbsp;sel.PickObject(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ObjectType</span>.Element,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Pick&nbsp;element&nbsp;to&nbsp;change&nbsp;its&nbsp;colour&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;id&nbsp;=&nbsp;r.ElementId;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;Autodesk.Revit.Exceptions.<span style="color:#2b91af;">OperationCanceledException</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Cancelled;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;ChangeElementColor(&nbsp;doc,&nbsp;id&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
}
</pre>

This code now lives in the 
new [sample command `CmdChangeElementColor`](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdChangeElementColor.cs)
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).

####<a name="3"></a> Assign new Material to an Element

A slightly trickier question is how to assign a new material:

**Question:** I would like to assign a material to a selected element. The material assigned will come from spreadsheet data. I already know how to create a material object from the data, and now I would like to assign it to selected elements in Revit. How can this be achieved?

**Answer:** Regarding the assignment of a material to a BIM element:

Please be aware that Revit is a BIM software. It produces a realistic building model.

In reality, you cannot simply change the material of an existing building element such as a wall or a floor.

If your floor is made of concrete, it stays concrete.

If you prefer a wooden floor, you have to remove the concrete one first.

Hence, simply swapping the material of a wall or floor is not as straightforward as one might assume.

Furthermore, the material is not determined by the wall or floor itself, but by its type.

Maybe you can simply swap the type.

Higher up in the controlling hierarchy comes the category.

The category does in fact provide
a [`Material` property](https://www.revitapidocs.com/2020/00aa768a-fca2-172f-e5d4-a4d787803983.htm) that
can be read and written.

So, one way to control the material of a wall is to set its category's material.

However, this will affect other walls sharing the same category.

You may be better served manipulating a sub-category instead.

In any case, you need to proceed with care to avoid wrecking your model.

Here is a code snippet that demonstrates changing an element category's material to a randomly chosen different material:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">void</span>&nbsp;ChangeElementMaterial(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;id&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;id&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;e.Category&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;im&nbsp;=&nbsp;e.Category.Material.Id.IntegerValue;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Material</span>&gt;&nbsp;materials&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Material</span>&gt;(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Material</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToElements()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where&lt;<span style="color:#2b91af;">Element</span>&gt;(&nbsp;m&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;m.Id.IntegerValue&nbsp;!=&nbsp;im&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">Material</span>&gt;()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Random</span>&nbsp;r&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Random</span>();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;i&nbsp;=&nbsp;r.Next(&nbsp;materials.Count&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Change&nbsp;Element&nbsp;Material&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.Category.Material&nbsp;=&nbsp;materials[&nbsp;i&nbsp;];
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

I added  a call to this method as well in `CmdChangeElementColor`.

This command is available
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
new [release 2021.0.149.1](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2021.0.149.1).

<center>
<img src="img/concrete_steel_wood_bamboo.jpg" alt="Materials" title="Materials" width="400"/> <!-- 800 -->
</center>
