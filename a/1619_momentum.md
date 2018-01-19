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

- https://github.com/tumcms/MomenTUM
  pedestrian simulation framework and tools
  the [Chair of Computational Modeling and Simulation](https://www.cms.bgu.tum.de/en/)
  [Technische Universität München](https://www.tum.de)
  The [layout-tools](https://github.com/tumcms/MomenTUM/tree/master/momentum-tools) projects provide mechanisms to generate simulation scenarios based on different input data (AutoCAD, Revit, OSM).
  https://github.com/tumcms/MomenTUM/tree/master/momentum-tools/momentum-layout-tools/momentum-layout-tools-revit
  This plugin is used to export geometrical information from a Revit project to a xml. The generated xml contains layouts (scenarios/levels) which can be used for Momentum simulations.

MomenTUM Pedestrian Simulation with #RevitAPI in @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/momentumrvt

Christian Thieme of the Chair of Computational Modeling and Simulation at TUM, the Technical University Munich, shares his tools connecting Revit to the MomenTUM agent-based pedestrian simulation framework
&ndash; Giving something back to the community
&ndash; Project overview
&ndash; Export workflow
&ndash; Import and display results workflow
&ndash; Transformation from <code>XYZ</code> to <code>UV</code>...

--->

### Revit Tools for MomenTUM Pedestrian Simulation

Occasionally, I have the delight of receiving messages entitled something like *Giving something back to the Community*.

This time, the pleasure was provided by Christian Thieme, master's degree student at
the [Chair of Computational Modeling and Simulation](https://www.cms.bgu.tum.de/en) at
[TUM, the Technical University Munich](https://www.tum.de),
working on tools connecting Revit to
the [MomenTUM](https://www.cms.bgu.tum.de/en/31-forschung/projekte/456-momentum) agent-based
pedestrian simulation framework.

Many thanks, Christian, for sharing this!

In his own words:

- [Giving something back to the community](#2)
- [Project overview](#3)
- [Export workflow](#4)
- [Import and display results workflow](#5)
- [Transformation from `XYZ` to `UV`](#6)


####<a name="2"></a>Giving Something Back to the Community

I am a Master's degree student at TUM in Germany.

As part of a project, I developed a Revit Plugin that uses
the [Revit Analysis Visualization Framework AVF](http://thebuildingcoder.typepad.com/blog/avf) to
display (arbitrary) analytical results.

In order to do so, your blog posts and the forum were a big help. And if it is of any help, I would like to share my solution to do so.

My general approach to compute and display was to extract geometrical information (e.g. the layout of a building).
I exported this information in an xml format.

Then I processed the xml-data using a Java Project of the University Chair I was working in.

The Java projects generates a 2D array out of the input xml file, and computes a metric &ndash; in my case, a type
of [space syntax](https://en.wikipedia.org/wiki/Space_syntax) operation
&ndash; [Visibility Graph Analysis](https://en.wikipedia.org/wiki/Visibility_graph_analysis)).

The result of the visibility graph analysis is a 2D array of values that is then fed in to my plugin.

You can find my Space Syntax Plugin within
the [Momentum GitHub repository](https://github.com/tumcms/MomenTUM).

The Revit Plugins are located in
the [momentum-tools subfolders](https://github.com/tumcms/MomenTUM/tree/master/momentum-tools).

- Export: [MomenTumV2RevitLayouting add-in](https://github.com/tumcms/MomenTUM/tree/master/momentum-tools/momentum-layout-tools/momentum-layout-tools-revit) exports geometrical information from a Revit project to XML.
- Import: [MomenTumV2SpaceSyntaxRevit](https://github.com/tumcms/MomenTUM/tree/master/momentum-tools/momentum-spaceSyntax-tools/momentum-spaceSyntax-tools-revit) reads the XML results and displays them using AVF.

<center>
<img src="img/momentum_gebaeude_7_rvt_export.png" alt="Gebaeude 7" width="442"/>
</center>

I hope you find this interesting and worth sharing.


####<a name="3"></a>Project Overview

The project goal was to visualize an analytical result.

- Step 1: use
the [MomenTumV2RevitLayouting Revit add-in](https://github.com/tumcms/MomenTUM/tree/master/momentum-tools/momentum-layout-tools/momentum-layout-tools-revit) to
export geometrical information from a Revit project to XML.
- Step 2: run
the [MomentumV2 Kernel the main Java project](https://github.com/tumcms/MomenTUM) to produce another XML file with result data.
- Step 3: use
the [MomentumV2SpaceSyntaxRevit Revit add-in](https://github.com/tumcms/MomenTUM/tree/master/momentum-tools/momentum-spaceSyntax-tools/momentum-spaceSyntax-tools-revit) and open the result data xml file and visualise the results.

The MomentumV2SpaceSyntaxPlugin uses AVF to display the result in the active view.

There are some limitations to the work, as mentioned in the project readme.

The space syntax plugin also contains the [transformation code from xyz to uv coordinates](#6).


####<a name="4"></a>Export Workflow

Executing the plugin prompts the user to select one level or all levels of a project. The selected level(s) will be exported as xml.

Note: Walls and doors are exported as obstacles as type 'Wall'. Stairs can will either be shown as areas of either type 'Origin' or 'Destination'.

Detailed information on the add-in installation and workflow is provided in
the [MomenTumV2RevitLayouting how-to documentation](zip/HowTo_MomenTumV2RevitLayouting.pdf).


####<a name="5"></a>Import and Display Results Workflow

The workflow assumes that you have successfully built the Revit layouting plugin and the Space Syntax plugin and integrated them into your Revit installation. Furthermore, you need to have built Momentum successfully and have a basic understanding of its usage (see momentum-documentation).

1. Use the Layouting Plugin to export geometry data of a level of a Revit project into a xml file. Make sure that you have properly set rooms in your Revit project before exporting (see layouting documentation).

2. Check if the resulting xml contains at least one area of type 'Origin' per scenario node.
(If there is none, you have to create this area by hand yourself.)

3. Create a xml configuration file and insert a lattice, Space Syntax and an outputwriter configuration.
(Hint: You can copy the spaceSyntaxExamples.xml and make sure to adapt the file paths and ids accordingly.)

4. Run your configuration with Momentum (see momentum-documentation) and check if the execution produced the xml output file defined in the Space Syntax output writer tag of the input configuration.

5. In Revit, in the same project as you exported the geometry, go to 'Add-Ins' tab and run the MomentumV2SpaceSyntax external command and select the Space Syntax result xml you just created with Momentum.

6. If you created the xml result using the Revit Layouting plugin, the Space Syntax plugin will automatically select the correct level to visualize the result from metadata in the result xml. Otherwise, a level picker will open.


####<a name="6"></a>Transformation from XYZ to UV

In my import and visualisation plugin, I managed to transform points from the global space (`XYZ`) to the local space of a face (`UV` representation of a bounding box).

In my understanding, this is quite unique, since I haven't found any other code snippet that does so.

Revit itself only supports the `Face.Transform(UV)` method that translates a `UV` coordinate into `XYZ` coordinate space.

I reversed that Method to translate `XYZ` coords to `UV` coords.

At first, I thought I could solve the reverse transformation by solving a linear equation with 2 unknown variables. But this wasn't a general solution. So, I finally found out that the transformation consists of a displacement vector and a rotation matrix.

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Create&nbsp;transformation&nbsp;matrix&nbsp;to&nbsp;transform&nbsp;points&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;from&nbsp;the&nbsp;global&nbsp;space&nbsp;(XYZ)&nbsp;to&nbsp;the&nbsp;local&nbsp;space&nbsp;of&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;a&nbsp;face&nbsp;(UV&nbsp;representation&nbsp;of&nbsp;a&nbsp;bounding&nbsp;box).</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Revit&nbsp;itself&nbsp;only&nbsp;supports&nbsp;Face.Transform(UV)&nbsp;that&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;translates&nbsp;a&nbsp;UV&nbsp;coordinate&nbsp;into&nbsp;XYZ&nbsp;coordinate&nbsp;space.&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;I&nbsp;reversed&nbsp;that&nbsp;Method&nbsp;to&nbsp;translate&nbsp;XYZ&nbsp;coords&nbsp;to&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;UV&nbsp;coords.&nbsp;At&nbsp;first&nbsp;i&nbsp;thought&nbsp;i&nbsp;could&nbsp;solve&nbsp;the&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;reverse&nbsp;transformation&nbsp;by&nbsp;solving&nbsp;a&nbsp;linear&nbsp;equation&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;with&nbsp;2&nbsp;unknown&nbsp;variables.&nbsp;But&nbsp;this&nbsp;wasn&#39;t&nbsp;general.&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;I&nbsp;finally&nbsp;found&nbsp;out&nbsp;that&nbsp;the&nbsp;transformation&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;consists&nbsp;of&nbsp;a&nbsp;displacement&nbsp;vector&nbsp;and&nbsp;a&nbsp;rotation&nbsp;matrix.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">double</span>[,]&nbsp;
&nbsp;&nbsp;CalculateMatrixForGlobalToLocalCoordinateSystem(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;face&nbsp;)
{
&nbsp;&nbsp;<span style="color:green;">//&nbsp;face.Evaluate&nbsp;uses&nbsp;a&nbsp;rotation&nbsp;matrix&nbsp;and</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;a&nbsp;displacement&nbsp;vector&nbsp;to&nbsp;translate&nbsp;points</span>

&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;originDisplacementVectorUV&nbsp;=&nbsp;face.Evaluate(&nbsp;<span style="color:#2b91af;">UV</span>.Zero&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;unitVectorUWithDisplacement&nbsp;=&nbsp;face.Evaluate(&nbsp;<span style="color:#2b91af;">UV</span>.BasisU&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;unitVectorVWithDisplacement&nbsp;=&nbsp;face.Evaluate(&nbsp;<span style="color:#2b91af;">UV</span>.BasisV&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;unitVectorU&nbsp;=&nbsp;unitVectorUWithDisplacement&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;originDisplacementVectorUV;

&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;unitVectorV&nbsp;=&nbsp;unitVectorVWithDisplacement&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;originDisplacementVectorUV;

&nbsp;&nbsp;<span style="color:green;">//&nbsp;The&nbsp;rotation&nbsp;matrix&nbsp;A&nbsp;is&nbsp;composed&nbsp;of</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;unitVectorU&nbsp;and&nbsp;unitVectorV&nbsp;transposed.</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;To&nbsp;get&nbsp;the&nbsp;rotation&nbsp;matrix&nbsp;that&nbsp;translates&nbsp;from&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;global&nbsp;space&nbsp;to&nbsp;local&nbsp;space,&nbsp;take&nbsp;the&nbsp;inverse&nbsp;of&nbsp;A.</span>

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;a11i&nbsp;=&nbsp;unitVectorU.X;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;a12i&nbsp;=&nbsp;unitVectorU.Y;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;a21i&nbsp;=&nbsp;unitVectorV.X;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;a22i&nbsp;=&nbsp;unitVectorV.Y;

&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:blue;">double</span>[2,&nbsp;2]&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;a11i,&nbsp;a12i&nbsp;},
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;a21i,&nbsp;a22i&nbsp;}};
}
</pre>
