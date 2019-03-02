<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://youtu.be/GW9cbFYnKAc
<iframe width="560" height="315" src="https://www.youtube.com/embed/GW9cbFYnKAc" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
TITAN VS ROBO-CAR - The Multi-Billion Dollar Company You’ve Never Heard Of
Titan travels to Zoox to meet with Chairman Carl Bass and discuss their incredible self-driving car. Not only do they have the artificial intelligence (AI) to autonomously drive the car through the streets of San Francisco, they are manufacturing their own vehicle to compete with the biggest names in ride-sharing. CNC machining, Advanced 3D printing, and 7-Axis robots to name a few of their production capabilities. Check out how their technology works, and why it is the future of driving. 

- https://youtu.be/EH-z9gE2uGY
<iframe width="560" height="315" src="https://www.youtube.com/embed/EH-z9gE2uGY" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
The Mystery at the Bottom of Physics
Actually gets deep enough into physics to wax philosophical on gratitude for life and the reason to be grateful and happy for every moment of my own personal existence.

twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) recently

-->

### Yearly Deprecated Method Cleanup

Same procedure as every year:
eliminate all deprecated Revit API usage warnings before even thinking of migrating to the next major release.

- [Deprecated API Usage Warnings](#2) 
- [Replace GetRules by GetElementFilter](#3) 
- [Deprecated Material Asset Accessors](#4) 
- [Update with Zero Compilation Warnings](#5) 

#### <a name="2"></a> Deprecated API Usage Warnings

Before I started out eliminating the deprecated Revit API usage warnings,
compiling [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
generated [eight warnings](zip/tbc_samples_2019_errors_warnings_2.txt).

The first is just a notification of unreachable code in the module `CmdListAllRooms.cs` that I recently worked on
to [export room boundaries to CSV for Forge surface classification](https://thebuildingcoder.typepad.com/blog/2019/01/room-boundaries-to-csv-and-wpf-template.html#2),
so I'll fix that first.

It was caused by a `const bool` variable `_exportInMillimetres`.
The warning disappeared after I changed that to `static bool` instead.

That leaves [seven warnings](zip/tbc_samples_2019_errors_warnings_3.txt), all numbered `CS0618` and caused by deprecated Revit API usage, from two different API calls:

- In CmdCollectorPerformance.cs: `ParameterFilterElement.GetRules` is obsolete: This method is deprecated in Revit 2019 and will be removed in the next version of Revit. We suggest you use `GetElementFilter` instead.
- In CmdGetMaterials.cs, repeated six times: `AssetProperties.this[string]` is obsolete: This property is deprecated in Revit 2019 and will be removed in the next version of Revit. We suggest you use the `FindByName(String)` or `Get(int)` method instead.

#### <a name="3"></a> Replace GetRules by GetElementFilter

Both of these deprecated API usages can be easily fixed by following the instructions given by the warning messages.

In the first case, we can replace GetRules by GetElementFilter and simplify the code using it like as follows; before:

<pre class="code">
  <span style="color:#2b91af;">ParameterFilterElement</span>&nbsp;pfe ...

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">FilterRule</span>&nbsp;rule&nbsp;<span style="color:blue;">in</span>&nbsp;pfe.GetRules()&nbsp;)&nbsp;<span style="color:green;">//&nbsp;2018</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;elemsByFilter2
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;elemsByFilter.Where(&nbsp;e
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;rule.ElementPasses(&nbsp;e&nbsp;)&nbsp;);
&nbsp;&nbsp;}
</pre>

After:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ElementFilter</span>&nbsp;ef&nbsp;=&nbsp;pfe.GetElementFilter();&nbsp;<span style="color:green;">//&nbsp;2019</span>
&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;elemsByFilter2
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;elemsByFilter.WherePasses(&nbsp;ef&nbsp;);
</pre>

#### <a name="4"></a> Deprecated Material Asset Accessors

That leaves [six warnings caused by calls to deprecated material asset accessors](zip/tbc_samples_2019_errors_warnings_4.txt).

Again, easily eliminated by doing what the man says, calling `Get` for `int` accessors and `FindByName` for the ones taking a string argument.


#### <a name="5"></a> Update with Zero Compilation Warnings

The cleaned-up code is available
from [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2019.0.145.11](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.11).

We are now ready to face whatever it takes to migrate to the new major release, if and when that shows up.




**Question:** 

**Answer:** 

**Response:** 

<pre class="code">
</pre>

<center>
<img src="img/" alt="" width="100">
</center>



<pre class="code">
</pre>

