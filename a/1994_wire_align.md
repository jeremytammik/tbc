<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Electrical Wire not found in BoundingBoxIsInsideFilter
  https://forums.autodesk.com/t5/revit-api-forum/electrical-wire-not-found-in-boundingboxisinsidefilter/m-p/11938583
  ricaun created the BoundingBoxViewIntersectsFilter and BoundingBoxViewIsInsideFilter:
  https://gist.github.com/ricaun/14ec0730e7efb3cc737f2134475e2539

- https://forums.autodesk.com/t5/revit-api-forum/visualizing-circuits-in-3d/td-p/11937368

- align two elements:
  How to use the Alignment method for family Instance
  https://forums.autodesk.com/t5/revit-api-forum/how-to-use-the-alignment-method-using-for-family-instance/m-p/11938454

twitter:

 in the @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS

&ndash;
...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Align an Instance and Find a Wire







####<a name="2"></a>

<pre class="prettyprint">

</pre>




####<a name="3"></a>



####<a name="4"></a> Align Two Elements

How to use the Alignment method Using # for family Instance
Hi Guys .
I Need to align a FamilyInstance in which I created using C# to a Line that I also Created Via C# in Revit 2023. For example the First Picture would show a structural column and a ModelCurve which are not aligned together.


<center>
<img src="img/align_element_1.png" alt="Align element &ndash; not aligned" title="Align element &ndash; not aligned" width="100"/> <!-- Pixel Height: 300 Pixel Width: 639 -->
</center>

 The second picture would Show How I want my column to be aligned to a ModelCurve.


<center>
<img src="img/align_element_2.png" alt="Align element" title="Align element" width="100"/> <!-- Pixel Height: 300 Pixel Width: 639 -->
</center>

I would greatly apricate if someone can show me just the method of how can I use the alignment method in C# . Here is just a sample code for my script :


Line line = Line.CreateBound(startPoint, endPoint);
Element newPile = doc.Create.NewFamilyInstance(point, symbol, Level, structuralType);




 Solved by jeremy.tammik. Go to Solution.

Tags (0)
Add tags
Report
8 REPLIES
Sort:
MESSAGE 2 OF 9
jeremy.tammik
 Autodesk jeremy.tammik in reply to: ahmadkhalaf7892
‎2023-05-03 02:16 AM
Well, first of all you need to understand how to implement such a constraint manually in the end user interface. I believe you define a dimension between the two objects to do so, and constrain it to a zero distance. The Family API samples may demonstrate how such a constraint can be set up programmatically:



https://thebuildingcoder.typepad.com/blog/2009/08/the-revit-family-api.html


Reading that myself, I discover that the NewAlignment method might come in handy:



https://www.revitapidocs.com/2023/b3c10008-aba6-9eee-99c9-7e05ace75796.htm


Searching this forum for NewAlignment ought to turn up something useful for you.



Good luck!



Jeremy Tammik,  Developer Advocacy and Support, The Building Coder, Autodesk Developer Network, ADN Open
Tags (0)
Add tags
Report
MESSAGE 3 OF 9
ahmadkhalaf7892
 Advocate ahmadkhalaf7892 in reply to: jeremy.tammik
‎2023-05-03 04:21 AM
Hi Jeremy .
Thanks a  lot for introducing this method for me .  I am trying to align the family instance called newPile to the model Curve however I am getting this error under :
Autodesk.Revit.Exceptions.ArgumentException: 'The two references are not geometrically aligned so the Alignment cannot be created.
Parameter name: reference2'

I have created both the model Curve and the familyinstance in the same level.

This is the code that I am using :

// Create a list of curves
List<Curve> curves = new List<Curve>();
foreach (Floor floor in tunnelFloors.Floors)
{
Sketch sketch = doc.GetElement(floor.SketchId) as Sketch;
foreach (CurveArray curveArray in sketch.Profile)
{
foreach (Curve curve in curveArray)
{
XYZ p0 = (new XYZ(curve.GetEndPoint(0).X, curve.GetEndPoint(0).Y, 0));
XYZ p1 = (new XYZ(curve.GetEndPoint(1).X, curve.GetEndPoint(1).Y, 0));
Curve c1 = Line.CreateBound(p0, p1);
curves.Add(c1);
}

}
}

foreach (Curve curve in curves)
{

ModelCurve m1 = doc.Create.NewModelCurve(curve, sketchPlane);
// Move the family instance along the curve by the distance variable
double length = curve.Length;
int count = (int)(length / x);
for (int j = 1; j <= count; j++)
{
XYZ point = curve.Evaluate((double)j * x / length, true);
FamilyInstance newPile = doc.Create.NewFamilyInstance(point, symbol, Level, structuralType);
Reference s= newPile.GetReferenceByName("SS");
// Get the reference plane named "SS" from the family instance

//uidoc.Selection.PickObject(ObjectType.PointOnElement);
Dimension alignToLine3 = doc.Create.NewAlignment(viewPlan, m1.GeometryCurve.Reference, s);

}
}

Tags (0)
Add tags
Report
MESSAGE 4 OF 9
ahmadkhalaf7892
 Advocate ahmadkhalaf7892 in reply to: jeremy.tammik
‎2023-05-03 05:14 AM
here is the full constructor in case needed :


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Document = Autodesk.Revit.DB.Document;

namespace Tunnel
{
public class SheetPile
{
double meters = 3.28084;

public SheetPile(Document doc, double x, String PileName, Level Level, TunnelFloors tunnelFloors)
{

Options options = new Options();
Plane plane = Plane.CreateByNormalAndOrigin(new XYZ(0, 0, 1), new XYZ(0, 0, 0));
SketchPlane sketchPlane = SketchPlane.Create(doc, plane);
// Create a new view plan for Level 1
// Get the floor plan view family type
ViewFamilyType viewFamilyType = new FilteredElementCollector(doc)
.OfClass(typeof(ViewFamilyType))
.Cast<ViewFamilyType>()
.FirstOrDefault(v => v.ViewFamily == ViewFamily.StructuralPlan);
// Create a new view plan for Level 1

ViewPlan viewPlan = ViewPlan.Create(doc, viewFamilyType.Id, Level.Id);
// Convert input values from feet to meters
x = x * meters;

// Define the family symbol and structural type
FamilySymbol symbol = new FilteredElementCollector(doc)
.OfClass(typeof(FamilySymbol))
.OfCategory(BuiltInCategory.OST_StructuralColumns)
.FirstOrDefault(e => e.Name == PileName) as FamilySymbol;
StructuralType structuralType = StructuralType.Column;

// Create a list of curves
List<Curve> curves = new List<Curve>();
foreach (Floor floor in tunnelFloors.Floors)
{
Sketch sketch = doc.GetElement(floor.SketchId) as Sketch;
foreach (CurveArray curveArray in sketch.Profile)
{
foreach (Curve curve in curveArray)
{
XYZ p0 = (new XYZ(curve.GetEndPoint(0).X, curve.GetEndPoint(0).Y, 0));
XYZ p1 = (new XYZ(curve.GetEndPoint(1).X, curve.GetEndPoint(1).Y, 0));
Curve c1 = Line.CreateBound(p0, p1);
curves.Add(curve);
}

}
}

foreach (Curve curve in curves)
{

ModelCurve m1 = doc.Create.NewModelCurve(curve, sketchPlane);
// Move the family instance along the curve by the distance variable
double length = curve.Length;
int count = (int)(length / x);
for (int j = 1; j <= count; j++)
{
XYZ point = curve.Evaluate((double)j * x / length, true);
FamilyInstance newPile = doc.Create.NewFamilyInstance(point, symbol, Level, structuralType);
newPile.LookupParameter("Top Level").Set("Level 2");
Reference s = newPile.GetReferenceByName("SS");
// Get the reference plane named "SS" from the family instance

//uidoc.Selection.PickObject(ObjectType.PointOnElement);
doc.Create.NewAlignment(viewPlan, s, curve.Reference);

}
}
}

}
}

Tags (1)
Tags:is the full

Add tags
Report
MESSAGE 5 OF 9
jeremy.tammik
 Autodesk jeremy.tammik in reply to: ahmadkhalaf7892
‎2023-05-03 05:26 AM
Did you read the remarks in the Revit API docs?



https://www.revitapidocs.com/2023/b3c10008-aba6-9eee-99c9-7e05ace75796.htm


> These references must be already geometrically aligned (this function will not force them to become aligned).



Jeremy Tammik,  Developer Advocacy and Support, The Building Coder, Autodesk Developer Network, ADN Open
Tags (0)
Add tags
Report
MESSAGE 6 OF 9
ahmadkhalaf7892
 Advocate ahmadkhalaf7892 in reply to: jeremy.tammik
‎2023-05-03 05:29 AM
Ah Sorry , I have been working on this for hours . I'm loosing my concentration , I didn't pay attention to it .
Is there any way I can force Them to be aligned using the Revit API?
I have been trying for the past 4 hours. If it is a dead end please inform me .
Thanks very much.
Tags (0)
Add tags
Report
MESSAGE 7 OF 9
jeremy.tammik
 Autodesk jeremy.tammik in reply to: ahmadkhalaf7892
‎2023-05-03 05:36 AM
Take a rest! Go for a walk!



The easiest way to ensure they are aligned is to create them accordingly in the first place, if they are being generated from scratch. Otherwise, you can use the standard translation and rotation functionality provided by ElementTransformUtils. Or, you can set the location curve via the Location property.



Jeremy Tammik,  Developer Advocacy and Support, The Building Coder, Autodesk Developer Network, ADN Open
Tags (0)
Add tags
Report
MESSAGE 8 OF 9
ahmadkhalaf7892
 Advocate ahmadkhalaf7892 in reply to: jeremy.tammik
‎2023-05-03 05:43 AM
I will rest for a few then see which approach fits better. I am using a family which is already loaded in the Project and I am placing them on a line with a specific distance . However I want them to rotate according to the Curve or Line they are placed on. I haven't been able to do such thing. I will see what I can do .
I should use the ElementTransformUtils.Rotate in this case ?
I really appreciate the help Jeremy
Tags (0)
Add tags
Report
MESSAGE 9 OF 9
jeremy.tammik
 Autodesk jeremy.tammik in reply to: ahmadkhalaf7892
‎2023-05-03 05:47 AM
Either ElementTransformUtils.Rotate or just manipulate the LocationPoint or LocationCurve via Rotate, e.g.:



https://www.revitapidocs.com/2023/e1071a1b-b98e-5875-2e13-b673e2b9fef6.htm
https://www.revitapidocs.com/2023/ed4de043-9a60-f6cd-c09b-b13c4612b343.htm


Enjoy your break.

