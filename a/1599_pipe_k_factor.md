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

- 11843337 [PipeKfactor table]

- 11843886 [Custom K factor calculation for pipe fittings.]
  http://forums.autodesk.com/t5/revit-mep/custom-k-factor-calculation-for-pipe-fittings/m-p/6350133

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

&ndash; 
...

--->

### Pipe Fitting K Factor

Two recent Revit MEP related cases brought up some new information and partial results, still leaving a few open questions regarding the pipe `K` factor table and custom `K` factor calculation for pipe fittings:

- [Pipe `K` factor table](#2)
- [custom `K` factor calculation for pipe fittings](#3)


####<a name="2"></a>Pipe K Factor Table

**Question:** I know that Revit calculates the pressure loss for the fittings and some pipe accessories with the factors that are defined in the file `pipekfactor.xml`.

However, I do not know what formulas are actually user for the calculations. The mysterious numbers don't tell much.

Could you clarify which formulas are used for the calculation of the pressure loss or where this information is available, please?

Here are some fractions from `pipekfactors.xml`:

<pre class="prettyprint">
&lt;SubTable Dimensions="1" Number="3" Result="K"&gt;
&lt;SubTableData1D X="Theta"&gt;&lt;XAxis&gt;0 10 20 30 40 50 60 70 80 90 100 110 120 130 140 150 160 170 180&lt;/XAxis&gt;
&lt;Data&gt; 0.0 0.18 0.4 0.675 0.9 1.05 1.1 1.125 1.1 1.075 1.06 1.05 1.045 1.035 1.025 1.015 1.01 1.005 1.0 &lt;/Data&gt;
&lt;/SubTableData1D&gt;

&lt;SubTable Dimensions="0" Number="4" Result="K" DataUsage="UseLargePipeVelocity"&gt;
&lt;SubTableData Coefficient="Equation 2-27"/&gt;
&lt;Formula&gt;D1 &gt; D2 && Theta &gt; 45 && Theta &lt;= 180&lt;/Formula&gt;
&lt;/SubTable&gt;
&lt;Formula&gt;D1 &lt; D2 && Theta &lt; 180&lt;/Formula&gt;
&lt;/SubTable&gt;
</pre>

**Answer:** The Revit UI provides a lot of detail on these calculations if you navigate to
 
- Manage &gt; MEP Settings &gt; Mechanical Settings &gt; Pipe Settings &gt; Calculation
 
Additional information is provided in the Revit product help pages
on [Hydronic Pipe Sizing and Calculation Methods](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-7D4BF4BC-89DF-4EB2-93EB-52900B8583B5).
 
In general, the loss on a fitting is the velocity pressure multiplied by the loss coefficient. This can be confirmed in the pressure loss report:

<center>
<img src="img/pressure_loss_report.png" alt="Pressure loss report" width="934"/>
</center>



####<a name="3"></a>Custom K Factor Calculation for Pipe Fittings

A related question was raised by Js1900 and Jared in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread 
on [custom `K` factor calculation for pipe fittings](http://forums.autodesk.com/t5/revit-mep/custom-k-factor-calculation-for-pipe-fittings/m-p/6350133):

**Question:** We have a requirement to use our own numbers for the K factor calculation for pipe fittings.
 
This could be done through the API by implementing the `IPipeFittingAndAccessoryPressureDropServer` interface, but it seems like it could be simpler to just add an entry to
the file `PipeKFactors.xml`:

- C:\Program Files\Autodesk\Revit 201x\PipeKFactors.xml

It looks to be a simple enough for elbows; we can just create a new table in the xml with the diameters and coefficients we need, and Revit will use it to get our value.
 
However, for Tees, the table is more complex and Revit seems to be doing more behind the scenes. For example, we can put our coefficients in, but Revit seems to output a number 4 times larger than expected every time. It gets even more complex for the table *Converging/Diverging Tee/Wye*, which seems to have no numbers in the xml and instead references equations defined elsewhere.
 
Does anyone have any insight into the algorithm used on these tables so we can be sure we can get the right values? Or have an idea why our edits to the Tee coefficients (*Custom Welded Tee* in the [attached xml pipe_k_factors.xml](zip/pipe_k_factors.xml)) result in in Revit giving a factor 4 times greater when we inspect it through the pressure loss report?

**Answer:** For the '4x greater' case of the custom tee, we tried to reproduce and were unable to do so.
 
I had a different problem with it altogether, namely, no friction factor was found in the table at all (see image below).
 
What would be helpful in this case is to get a sample model demonstrating this issue. In some cases, the family and/or its instance parameters are defined in such a way that factors are applied multiple times, so it may not necessarily be the XML at issue.
 
Can you provide details of where you are getting your data from for your ‘Custom Welded Tee’? E.g., do you have table(s) of data you can provide for the various conditions through the tee?
 
It looks like you are trying to input the 'Custom Welded Tee' data in a format similar to the 'Converging/Diverging/Side Tee' in the default .xml file. Can you confirm that? Any details of what exactly was changed would probably be helpful as well.

<center>
<img src="img/no_friction_factor.png" alt="No friction factor" width="918"/>
</center>
 
Converging/Diverging Tee/Wye is implemented in code, as it is not based on simple tabular data. It is based on the method in the Crane Technical Paper 410.

**Response:** We had looked through those calculations in Revit help which are well explained for pipes.

However, for fittings, it simply says you can choose a `K` Coefficient from table, without really saying how it gets the number.
 
You are right; 'Custom Tee' in our xml is simply the 'Converging/Diverging/Side Tee' table copied. Then we modified XAxis values, which appear to be diameter ratios, to be 2 numbers far apart. This was to cover all diameter values. We repeated data for these X values to avoid Revit interpolating between them (We only want this to cover equal tees, so perhaps we could have just had a single X value of 1.0?)

Then we just put the K coefficients we wanted in the data element, mapped to the flow ratio.
 
This was all based on guesswork and some testing, and it does appear the logic makes sense, apart from the factor of 4!
 
I have attached the data we are trying to get Revit to use for Tee coefficients, a basic test model and test PipeKFactors.xml
in [custom_tee_test.zip](zip/custom_tee_test.zip).

The XML includes a Tee table with the data we want to use/4, along with some test tables containing only values of 1. Replacing the default PipeKFactors.xml and running a pressure loss report gives this result:

<center>
<img src="img/loss_report.png" alt="Loss report" width="1887"/>
</center>
 
We have just been using guesswork and experimentation to get this far in modifying the xml, but it does seem like we can get useful results, so appreciate any insight!
 

####<a name="4"></a>


