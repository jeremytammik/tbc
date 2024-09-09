<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>

</style>

</head>

<!---

- Get BendRadius center of Cable Tray Fittings
  https://forums.autodesk.com/t5/revit-api-forum/get-bendradius-center-of-cable-tray-fittings/m-p/12757167

- mep tee identification and analysis
  Revit Tpiece geometric analysis
  https://forums.autodesk.com/t5/revit-api-forum/revit-tpiece-geometric-analysis/m-p/12995868

twitter:

 @AutodeskRevit #RevitAPI #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Cable Tray Bend and Tee Analysis

####<a name="2"></a> Cable Tray Bend Radius

- Get BendRadius center of Cable Tray Fittings
https://forums.autodesk.com/t5/revit-api-forum/get-bendradius-center-of-cable-tray-fittings/m-p/12757167

####<a name="3"></a> MEP Tee Branch Identification

Andrej Licanin of [bimexperts](https://bimexperts.com/) brought up a question and shared his MEP tee fitting identification and analysis solution in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit Tpiece geometric analysis](https://forums.autodesk.com/t5/revit-api-forum/revit-tpiece-geometric-analysis/m-p/12995868):

**Question:**
I'm trying to make a function that will read a t-piece and return a coefficient based on a table from DIN 1988-300:

<center>
  <img src="img/tee_identification_1.png" alt="Tee identification" title="Tee identification" width="750"/> <!-- Pixel Height: 675 Pixel Width: 993 -->
</center>

I get T pieces by collecting family instances and checking their part type in the `MEPModel` field.
After that, I retrieve their connectors and obtain the flow values, but I can't determine the coefficients based on just that.
I would need some way to identify which connector is going straight and which one is branching off.
I thought the angle property could help me here, but no luck &ndash; in the families I'm looking at, they are all 0.
Any idea how to tackle this problem?
How could I determine which connector is coming from the side of a t piece?

**Answer:**
You can look at the [connector coordinate system](https://www.revitapidocs.com/2024/cb6d725d-654a-f6f3-fed0-96cc618a42f1.htm).
Its Z axis points in the connector direction, so you can use that to determine the relative connector angles.

**Response:**
Thanks Jeremy, it works:

<center>
  <img src="img/tee_identification_2.png" alt="Tee identification" title="Tee identification" width="750"/> <!-- Pixel Height: 544 Pixel Width: 1,500 -->
</center>

Here is the code for the function:

<pre><code class="language-cs">  public static Element AnalyzeTee2(
    Element element,
    Document doc)
  {
    Element elementAtAngle = null;

    // Check if the element is a FamilyInstance

    if (!PipeNetworkSplitter.isSplitter(element))
    {
      return null;
    }

    List&lt;Connector&gt; tpieceConnectors = getConnectors(element);

    Connector connector1 = tpieceConnectors[0];
    Connector connector2 = tpieceConnectors[1];
    Connector connector3 = tpieceConnectors[2];

    XYZ zVector1 = connector1.CoordinateSystem.BasisZ;
    XYZ zVector2 = connector2.CoordinateSystem.BasisZ;
    XYZ zVector3 = connector3.CoordinateSystem.BasisZ;

    if (AreVectorsParallel(zVector1,zVector2))
      elementAtAngle = getConnectingConnector(connector3).Owner;
    if (AreVectorsParallel(zVector2,zVector3))
      elementAtAngle = getConnectingConnector(connector1).Owner;
    if (AreVectorsParallel(zVector1,zVector3))
      elementAtAngle = getConnectingConnector(connector2).Owner;

    return elementAtAngle;
  }

  public static bool AreVectorsParallel(
    XYZ vector1,
    XYZ vector2,
    double tolerance = 1e-6)
  {
    // Compute the cross product of the two vectors
    XYZ crossProduct = vector1.CrossProduct(vector2);

    // Check if the cross product is close
    // to zero vector within the given tolerance
    return crossProduct.IsAlmostEqualTo(XYZ.Zero, tolerance);
  }</code></pre>


Many thanks to Andrej for sharing this solution.
