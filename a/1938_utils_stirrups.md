<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- another utility to keep in mind:
  Use `ParameterFilterUtilities` to
  Get all possible FilterRule parameters
  https://forums.autodesk.com/t5/revit-api-forum/get-all-possible-filterrule-parameters/m-p/10936268

- Constraining Stirrups to the Cover of the Host Element
  https://forums.autodesk.com/t5/revit-api-forum/problem-with-constraining-stirrups-to-the-cover-of-the-host/td-p/10045899

- zero energy house https://www.edx.org/course/zero-energy-design-an-approach-to-make-your-buildi?hs_analytics_source=referrals&utm_source=mooc.org&utm_medium=referral&utm_campaign=mooc.org-course-list
TUDelft rowers 220 W continuous power, 330 peak; a match or candle flame provides about 100 W; to a water heater to provide 10 kW, you can see an array of 10 x 10 = 100 gas flames, where 100 x 100 W = 10 kW.

- Zero-Energy Design: an approach to make your building sustainable
  /j/doc/book/zero_energy_design_sustainable_building/zed.txt

twitter:

 in the #RevitAPI FormulaManager @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

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

### Utility Classes and Constraining Stirrups



####<a name="2"></a> 

Another example of the useful functionality provided by
the [Revit API `*Utils` classes that are often overlooked](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.52) was
highlighted by Samuel Kreuz' question and answer in the thread on using `ParameterFilterUtilities`
to [get all possible `FilterRule` parameters](https://forums.autodesk.com/t5/revit-api-forum/get-all-possible-filterrule-parameters/m-p/10936268):

**Question:** I would like to list all possible `ViewFilter` parameters for a given category, e.g., for the category `Walls` the parameters shown in this screenshot:

<center>
<img src="img/filter_rule_valid_params_for_wall.jpg" alt="" title="" width="800"/> <!-- 1394 -->
</center>

It seems like these are combinations of Project Parameters, Type Parameters and Instance Parameters.
How to retrieve all parameters allowed for creating a filter rule?
Or at least a way to check if a parameter is valid for creating a filter rule?

**Answer:** Take a look at the `ParameterFilterUtilities` class, specifically
the [GetFilterableParametersInCommon method](https://www.revitapidocs.com/2022/7ea624c7-2c0d-c9bb-3b2c-1ac798cf6606.htm).

**Response:** This is exactly what I am looking for!
I wasn't aware of the `ParameterFilterUtilities` until now.
They seem to have some really useful methods for working with ViewFilters.
This is how I checked for valid parameters for the category is `Walls`:

<pre class="code">
&nbsp;&nbsp;List&lt;<span style="color:blue;">string</span>&gt;&nbsp;<span style="color:#1f377f;">parameterNames</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;<span style="color:blue;">string</span>&gt;();
&nbsp;&nbsp;IList&lt;ElementId&gt;&nbsp;<span style="color:#1f377f;">wallCatList</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;ElementId&gt;()&nbsp;
&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;ElementId(BuiltInCategory.OST_Walls)&nbsp;
&nbsp;&nbsp;};
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">paramColl</span>&nbsp;=&nbsp;ParameterFilterUtilities
&nbsp;&nbsp;&nbsp;&nbsp;.GetFilterableParametersInCommon(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;wallCatList);
 
&nbsp;&nbsp;<span style="color:#8f08c4;">foreach</span>&nbsp;(ElementId&nbsp;<span style="color:#1f377f;">param</span>&nbsp;<span style="color:#8f08c4;">in</span>&nbsp;paramColl)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;BuiltInParameter&nbsp;<span style="color:#1f377f;">bip</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;(BuiltInParameter)&nbsp;param.IntegerValue;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;<span style="color:#1f377f;">label</span>&nbsp;=&nbsp;LabelUtils.GetLabelFor(bip);
&nbsp;&nbsp;&nbsp;&nbsp;parameterNames.Add(label);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;parameterNames.Sort();
&nbsp;&nbsp;StringBuilder&nbsp;<span style="color:#1f377f;">sb</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;StringBuilder();
&nbsp;&nbsp;parameterNames.ForEach(<span style="color:#1f377f;">e</span>&nbsp;=&gt;&nbsp;sb.Append(e&nbsp;+&nbsp;<span style="color:#a31515;">&quot;\r\n&quot;</span>));
&nbsp;&nbsp;TaskDialog.Show(<span style="color:#a31515;">&quot;Filtered&nbsp;Parameters&quot;</span>,&nbsp;sb.ToString());</pre>
</code>

The result includes all built-in parameters that can be used for filtering:

<center>
<img src="img/GetFilterableParameters.jpg" alt="GetFilterableParameters" title="GetFilterableParameters" width="600"/> <!-- 673 -->
</center>

GetFilterableParameters.jpg

Thanks 👌




####<a name="3"></a> 


####<a name="4"></a> 



- another utility to keep in mind:
  Use `ParameterFilterUtilities` to
  Get all possible FilterRule parameters
  https://forums.autodesk.com/t5/revit-api-forum/get-all-possible-filterrule-parameters/m-p/10936268

- Constraining Stirrups to the Cover of the Host Element
  https://forums.autodesk.com/t5/revit-api-forum/problem-with-constraining-stirrups-to-the-cover-of-the-host/td-p/10045899

- zero energy house https://www.edx.org/course/zero-energy-design-an-approach-to-make-your-buildi?hs_analytics_source=referrals&utm_source=mooc.org&utm_medium=referral&utm_campaign=mooc.org-course-list
TUDelft rowers 220 W continuous power, 330 peak; a match or candle flame provides about 100 W; to a water heater to provide 10 kW, you can see an array of 10 x 10 = 100 gas flames, where 100 x 100 W = 10 kW.

- Zero-Energy Design: an approach to make your building sustainable
  /j/doc/book/zero_energy_design_sustainable_building/zed.txt


