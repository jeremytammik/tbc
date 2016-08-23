<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

11951105 [Problems converting units for Unit Weight]

REVIT-94710 [API: UnitUtils.ConvertFromInternalUnits DisplayUnitType.DUT_KILOGRAMS_PER_CUBIC_METER error -- 11951105]

UnitUtils Converting Units for Unit Weight #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

Here comes a quick clarification of the units used for the <code>UnitWeight</code> built-in parameter <code>PHY_MATERIAL_PARAM_UNIT_WEIGHT</code>. One example usage is to calculate the total weight of rebars in a project...

-->

### UnitUtils Converting Units for Unit Weight

Here comes a quick clarification of the units used for the `UnitWeight` built-in parameter `PHY_MATERIAL_PARAM_UNIT_WEIGHT`.

One example usage is to calculate the total weight of rebars in a project.

A developer encountered the following issue in that process:

#### <a name="2"></a>Question

My Revit add-in calculates total weight of rebars in the document using the `UnitWeight` parameter from the material.

Displayed in Revit, I write a value of 7850 Kg/m3 (kg/cubicmeters).

When using this parameter in code, I assume that the value I get from the parameter is in internal units, which I assume to be kiloNewton / cubic feet.

So when I use this, I expect to be able to use the UnitUtils.

This snapshot from the values displayed by the Visual Studio debugger immediate window show the original value and the converted ones:

<pre>
  var parameterVaule = Parameter.AsDouble();
  7154.4631104000009

  var converted = UnitUtils.ConvertFromInternalUnits(parameterVaule ,Autodesk.Revit.DB.DisplayUnitType.DUT_KILONEWTONS_PER_CUBIC_METER);
  77.01 => This is a correct value.

  var converted2 = UnitUtils.ConvertFromInternalUnits(parameterVaule ,Autodesk.Revit.DB.DisplayUnitType.DUT_KILOGRAMS_PER_CUBIC_METER);
  252657.48031496062 => ??? this not correct!
</pre>

The value string shows 77 kN/cubicmeters, which by manual calculation is 7849.13 Kg/cubicmeters.

Why doesn't the `UnitUtils` class return this value?

I assume that the method `ConvertFromInternalUnits` should be able to convert from the default internal units to the wanted unit?

Is this a case where I can't trust the `UnitUtils`?


#### <a name="3"></a>Sample Code and Workaround

Here is a snippet from the calculation of unit weight from a material.

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Get&nbsp;the&nbsp;unit&nbsp;weight&nbsp;of&nbsp;a&nbsp;material.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">internal</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">double</span>&nbsp;GetMaterialEgenvekt(&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;
&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;material,&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;rebarelement&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;rType&nbsp;=&nbsp;rebarelement.Document.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rebarelement.GetTypeId()&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">ElementType</span>;
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;paramMaterial&nbsp;=&nbsp;rType.get_Parameter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.MATERIAL_ID_PARAM&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;mat&nbsp;=&nbsp;doc.GetElement(&nbsp;paramMaterial
&nbsp;&nbsp;&nbsp;&nbsp;.AsElementId()&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Material</span>;
 
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;egenvekt&nbsp;=&nbsp;0;
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;mat&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;<span style="color:blue;">return</span>&nbsp;egenvekt;
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;property&nbsp;=&nbsp;doc.GetElement(
&nbsp;&nbsp;&nbsp;&nbsp;mat.StructuralAssetId&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">PropertySetElement</span>;
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;property&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;unitWeightParam&nbsp;=&nbsp;property.get_Parameter(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.PHY_MATERIAL_PARAM_UNIT_WEIGHT&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Not&nbsp;In&nbsp;Use&nbsp;-&nbsp;gives&nbsp;wrong&nbsp;value&nbsp;in&nbsp;metric&nbsp;unit.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;unitWeight&nbsp;=&nbsp;<span style="color:#2b91af;">UnitUtils</span>.ConvertFromInternalUnits(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;unitWeightParam.AsDouble(),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DisplayUnitType</span>.DUT_KILOGRAMS_PER_CUBIC_METER&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Manual&nbsp;calculation.&nbsp;In&nbsp;use,&nbsp;and&nbsp;calculates&nbsp;correct.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;egenvektFraMaterial&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;EgenVektFraNewtonPerSquareFootMeter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;unitWeightParam.AsDouble()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!(&nbsp;egenvekt&nbsp;&gt;&nbsp;0&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;egenvekt&nbsp;=&nbsp;egenvektFraMaterial;
&nbsp;&nbsp;}
&nbsp;&nbsp;material&nbsp;=&nbsp;mat.Name;
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;egenvekt;
}
</pre>

The manual calculation in `EgenVektFraNewtonPerSquareFootMeter` is implemented like this:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Calculate&nbsp;the&nbsp;unit&nbsp;weight&nbsp;from&nbsp;NewtonPerSquareFootMeter</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">double</span>&nbsp;EgenVektFraNewtonPerSquareFootMeter(&nbsp;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;unitweight&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;egenvekt&nbsp;=&nbsp;unitweight&nbsp;/&nbsp;9.81F;
 
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;meterPerFot&nbsp;=&nbsp;1000&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;GeoHelper.FootMillimeterKonstant;
 
&nbsp;&nbsp;egenvekt&nbsp;=&nbsp;egenvekt&nbsp;*&nbsp;<span style="color:#2b91af;">Math</span>.Pow(&nbsp;meterPerFot,&nbsp;2&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;egenvekt;
}
</pre>

The constant `GeoHelper.FootMillimeterKonstant` is defined thus;

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">double</span>&nbsp;FootMillimeterKonstant
&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">Math</span>.Round(&nbsp;304.8,&nbsp;1&nbsp;);
</pre>

Hope we can sort out the problem =)

I use the manual way today, after a heads up from a customer who ordered about 900 Kg too little rebars in a project!


#### <a name="4"></a>Explanation

The development team took a look at this issue and provided the following explanation:

`UnitUtils.ConvertFromInternalUnits` does not convert from UnitWeight (77 kN/cubicmeters) to Density (7849.13 Kg/cubicmeters).

`UnitUtils.ConvertFromInternalUnits` displays a value from Revit internal units (kg/(ft²·s²) for UnitWeight and kg/ft³ for Density) to a compatible unit (kN/cubicmeters for UnitWeight and Kg/cubicmeters for Density).

In detail:

- UnitWeight is measured in kN/m3
- Density is measured in kg/m3

UnitWeight and Density are two different units.
So it does not make sense to display some UnitWeight values in kg/m3 &ndash; there are different units.

Therefore: 

<pre class="code">
  // Does not work (different units: value in
  // UnitWeight units kg/(ft²·s²) displayed as
  // value in Density units kg/m³)
  
  var unitWeight = UnitUtils.ConvertFromInternalUnits(
    unitWeightParam.AsDouble(),
    DisplayUnitType.DUT_KILOGRAMS_PER_CUBIC_METER);
</pre>

and 

<pre class="code">
  // Works (same units: value in UnitWeight units
  // kg/(ft²·s²) displayed as value in UnitWeight
  // units kN/m3)
  
  var converted2 = UnitUtils.ConvertFromInternalUnits(
    unitWeightParam.AsDouble(),
    DisplayUnitType.DUT_KILONEWTONS_PER_CUBIC_METER);
</pre>

If you want to obtain the density in kg/m3 you can use the `PHY_MATERIAL_PARAM_STRUCTURAL_DENSITY` built-in parameter:

<pre class="code">
<span style="color:blue;">var</span>&nbsp;densityParam&nbsp;=&nbsp;property.get_Parameter(
&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.PHY_MATERIAL_PARAM_STRUCTURAL_DENSITY&nbsp;);
 
<span style="color:green;">//&nbsp;Works&nbsp;(same&nbsp;units:&nbsp;value&nbsp;in&nbsp;Density&nbsp;units&nbsp;kg/ft³</span>
<span style="color:green;">//&nbsp;displayed&nbsp;as&nbsp;value&nbsp;in&nbsp;Density&nbsp;units&nbsp;kg/m³)</span>
 
<span style="color:blue;">var</span>&nbsp;converted&nbsp;=&nbsp;<span style="color:#2b91af;">UnitUtils</span>.ConvertFromInternalUnits(
&nbsp;&nbsp;densityParam.AsDouble(),
&nbsp;&nbsp;<span style="color:#2b91af;">DisplayUnitType</span>.DUT_KILOGRAMS_PER_CUBIC_METER&nbsp;);
 
--&gt;&nbsp;converted&nbsp;=&nbsp;7849.04687&nbsp;<span style="color:blue;">double</span>
</pre>

Or use the formula as you did:

<pre class="code">
  UnitWeight = Density * g
</pre>

I hope this clarifies.

Happily, you already solved this properly yourself.
