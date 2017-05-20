<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<!--
<script src="run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true&amp;skin=sunburst&amp;lang=css" defer="defer"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true&amp;skin=sunburst" defer="defer"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js"></script>
</head>

<!---

Point Boundary Condition and Revit 2017 SDK @AutodeskForge @CubeAthens #3dwebcoder #revitapi

I arrived safe and sound in San Francisco via Vancouver and am now working on the final preparations for the Forge DevCon and 3D Web Fest.
Before getting to that, here are some quick notes from my short exploration last week to produce a rather overdue list of the new Revit 2017 SDK samples and on creating a point boundary condition on a structural column
&ndash; New Revit 2017 SDK Samples
&ndash; Creating Point Boundary Condition on End of Structural Column...

-->


### Point Boundary Condition and Revit 2017 SDK

I arrived safe and sound in San Francisco via Vancouver and am now working on the final preparations for 
the [Forge DevCon](http://forge.autodesk.com/conference) 
and [3D Web Fest](http://www.3dwebfest.com).

<center>
  <img src="/p/2016/2016-06-13_greens/471_golden_gate_and_marina.jpg" alt="Golden Gate Bridge and Marina" width="450">
</center>

Before getting to that, here are some quick notes from my short exploration last week to produce a rather overdue list of the new Revit 2017 SDK samples and on creating a point boundary condition on a structural column:

- [New Revit 2017 SDK Samples](#2)
- [Creating Point Boundary Condition on End of Structural Column](#3)


#### <a name="2"></a>New Revit 2017 SDK Samples


I used to pay much more attention in the past to the topic of new samples and Revit API functionality.

Obviously, the larger the API grows, the less difference each individual enhancement makes, and the more specialised the modifications become.

I trust you already discovered these samples yourself if they are important to you:

- Samples/CapitalizeAllTextNotes
- Samples/GenericStructuralConnection
- Samples/GeometryAPI/BRepBuilderExample
- Samples/PlacementOptions
- Structural Analysis SDK/Examples/CodeCheckingConcreteExample and CalculationPointsSelector

Here is the procedure I used to produce that list:

<pre>
/a/lib/revit/2016/SDK $ find . -type d &gt; /a/lib/revit/jeremy/ls2016.txt
/a/lib/revit/2016/SDK $ cd ../../2017/SDK/
/a/lib/revit/2017/SDK $ find . -type d &gt; /a/lib/revit/jeremy/ls2017.txt
/a/lib/revit/2017/SDK $ cd ../../jeremy/
/a/lib/revit/jeremy $ diff ls2016.txt ls2017.txt
123c123
&lt; ./REX SDK/Visual Studio templates/Items/Autodesk/Revit Extensions 2016
---
&gt; ./REX SDK/Visual Studio templates/Items/Autodesk/Revit Extensions 2017
126c126
&lt; ./REX SDK/Visual Studio templates/Projects/Autodesk/Revit Extensions 2016
---
&gt; ./REX SDK/Visual Studio templates/Projects/Autodesk/Revit Extensions 2017
170a171,173
&gt; ./Samples/CapitalizeAllTextNotes
&gt; ./Samples/CapitalizeAllTextNotes/CS
&gt; ./Samples/CapitalizeAllTextNotes/CS/Properties
384a388,390
&gt; ./Samples/GenericStructuralConnection
&gt; ./Samples/GenericStructuralConnection/CS
&gt; ./Samples/GenericStructuralConnection/CS/Properties
385a392,395
&gt; ./Samples/GeometryAPI/BRepBuilderExample
&gt; ./Samples/GeometryAPI/BRepBuilderExample/CS
&gt; ./Samples/GeometryAPI/BRepBuilderExample/CS/Properties
&gt; ./Samples/GeometryAPI/BRepBuilderExample/CS/Resources
531a542,544
&gt; ./Samples/PlacementOptions
&gt; ./Samples/PlacementOptions/CS
&gt; ./Samples/PlacementOptions/CS/Properties
695a709,710
&gt; ./Structural Analysis SDK/Examples/ASCE-7-10/bin
&gt; ./Structural Analysis SDK/Examples/ASCE-7-10/bin/Release
702a718,719
&gt; ./Structural Analysis SDK/Examples/Concrete/CodeCheckingConcreteExample/bin
&gt; ./Structural Analysis SDK/Examples/Concrete/CodeCheckingConcreteExample/bin/Release
709a727
&gt; ./Structural Analysis SDK/Examples/Concrete/CodeCheckingConcreteExample/UIComponents/CalculationPointsSelector
714a733,734
&gt; ./Structural Analysis SDK/Examples/Concrete/ConcreteCalculationsExample/bin
&gt; ./Structural Analysis SDK/Examples/Concrete/ConcreteCalculationsExample/bin/Release
716a737,738
&gt; ./Structural Analysis SDK/Examples/ExtensibleStorageDocumentation/bin
&gt; ./Structural Analysis SDK/Examples/ExtensibleStorageDocumentation/bin/Release
719a742,743
&gt; ./Structural Analysis SDK/Examples/ExtensibleStorageUI/bin
&gt; ./Structural Analysis SDK/Examples/ExtensibleStorageUI/bin/Release
723a748,749
&gt; ./Structural Analysis SDK/Examples/ResultsInRevit/QueryingResults/bin
&gt; ./Structural Analysis SDK/Examples/ResultsInRevit/QueryingResults/bin/Release
725a752,753
&gt; ./Structural Analysis SDK/Examples/ResultsInRevit/StoringResults/bin
&gt; ./Structural Analysis SDK/Examples/ResultsInRevit/StoringResults/bin/Release
727a756,757
&gt; ./Structural Analysis SDK/Examples/SectionPropertiesExplorer/bin
&gt; ./Structural Analysis SDK/Examples/SectionPropertiesExplorer/bin/Release
</pre>


#### <a name="3"></a>Creating Point Boundary Condition on End of Structural Column

**Question:** I am trying to create a fixed boundary conditions at the beginning of a column following the example code in the Revit API help file RevitAPI.chm, but the endpoint reference of the `Curve` associated to the column analytical model keeps returning a null value.

Am I missing something?

**Answer:** Creating a boundary condition on end of column should work correctly.

Here is a code snippet from one of the Revit automatic regression tests.

It creates a point boundary condition on the ends of each analytical column:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Create&nbsp;a&nbsp;point&nbsp;load&nbsp;on&nbsp;all&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;analytical&nbsp;column&nbsp;end&nbsp;points.&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">void</span>&nbsp;CreatePointLoadOnColumnEnd(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
{
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Find&nbsp;all&nbsp;AM&nbsp;column&nbsp;instances&nbsp;in&nbsp;the&nbsp;document</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;columns
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_ColumnAnalytical&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">AnalyticalModel</span>&nbsp;am&nbsp;<span style="color:blue;">in</span>&nbsp;columns&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;curve&nbsp;=&nbsp;am.GetCurve();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AnalyticalModelSelector</span>&nbsp;selector&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">AnalyticalModelSelector</span>(&nbsp;curve&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;selector.CurveSelector&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">AnalyticalCurveSelector</span>.EndPoint;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;endPointRef&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;am.GetReference(&nbsp;selector&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;NewPointBoundaryConditions&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BoundaryConditions</span>&nbsp;newPointBC&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;doc.Create.NewPointBoundaryConditions(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;endPointRef,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TranslationRotationValue</span>.Fixed,&nbsp;0,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TranslationRotationValue</span>.Spring,&nbsp;1.0,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TranslationRotationValue</span>.Fixed,&nbsp;0,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TranslationRotationValue</span>.Fixed,&nbsp;0,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TranslationRotationValue</span>.Fixed,&nbsp;0,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TranslationRotationValue</span>.Fixed,&nbsp;0&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newPointBC.SetOrientTo(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BoundaryConditionsOrientTo</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.HostLocalCoordinateSystem&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

You can run this code in the Revit Macro Manager to verify.

I added it 
to [The Building Coder Samples](https://github.com/jeremytammik/the_building_coder_samples) 
[release 2017.0.127.5](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.127.5)
in the module 
[CmdNewLineLoad.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdNewLineLoad.cs#L30-L77).
