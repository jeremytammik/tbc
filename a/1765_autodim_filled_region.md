<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 15582252 [Create dimensions for Filled Region Boundary]
  https://forums.autodesk.com/t5/revit-api-forum/create-dimensions-for-filled-region-boundary/m-p/8926301

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash;
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

-->

### Auto-Dimension Filled Region Boundary

I am back from my break in the French Jura and looking at all the
interesting [Revit API forum discussions](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) again.

One that stands out and that I'll pick to get back into the blogging rhythm again is
about [creating dimensions for a filled region boundary](https://forums.autodesk.com/t5/revit-api-forum/create-dimensions-for-filled-region-boundary/m-p/8926301):


####<a name="2"></a> Revit

####<a name="3"></a>


**Question:**

<pre>
</pre>


**Answer:**

####<a name="4"></a>


####<a name="5"></a>

<center>
<img src="img/.png" alt="" width="100">
</center>


15582252 [Create dimensions for Filled Region Boundary]

Jorge Villarroel, jvillarroel@renelagos.com, RENE LAGOS [GP]

https://forums.autodesk.com/t5/revit-api-forum/create-dimensions-for-filled-region-boundary/m-p/8926301

jvillaroel  16 Views, 0 Replies 2019-07-23
‎2019-07-23 09:08 PM
Create dimensions for Filled Region Boundary

Hi All,

I've been working with dimensions for a while for multiple objects. The method needs a ReferenceArray to work. Now, I need to create dimensions for a filled region and I can't get the reference for the curves contained in the boundary.

/a/case/sfdc/15582252/attach/filled_region.png

Filled Region

/a/case/sfdc/15582252/attach/filled_region_dimensions.png

Dimensions for the Filled Region.

Any tip of advice will be very well received.

Thanks in advance!

This topic was escalated to Salesforce Case 15582252 on 2019-07-23 by an automatic escalation. Case status: Open.

-----------------------------------------------------------------------
jeremytammik  in reply to:  Rankjvillaroel
‎2019-07-24 03:04 PM
Re: Create dimensions for Filled Region Boundary

Dear Jorge,

Thank you for your query.

How did you create the dimensions in your second image?

Hove you used RevitLookup to explore in depth the filled region geometry and the dimension object with their respective references?

I have passed on your question to the development team for advice for you.

Best regards,

Jeremy

-----------------------------------------------------------------------
https://autodesk.slack.com/archives/C0SR6NAP8/p1563973390020000

@conoves A developer is trying to create dimensions for a filled region. The method requires a `ReferenceArray` to work. How can the appropriate references be retrieved from the filled region? They don't seem to be included with its boundary curves. The image shows dimensions for the Filled Region, presumably created via UI -- 15582252 -- https://forums.autodesk.com/t5/revit-api-forum/create-dimensions-for-filled-region-boundary/m-p/8926301

/a/case/sfdc/15582252/attach/filled_region_dimensions.png

-----------------------------------------------------------------------
jvillaroel  in reply to:  Rankjeremytammik
‎2019-07-24 05:01 PM
Re: Create dimensions for Filled Region Boundary

Hi @jeremytammik , Thanks for your reply.

The dimensions in the second image were created using native commands (no API, just clicking using "Align Dimension").

Indeed, I used Revit Lookup searching for some "Reference" in the Filled Region sub-elements with no results. Already tried to get the references from the CUrveLoop curves, but again, with no results. All I get is "Reference = null".

Any tip of advice will be very well received.

Thanks for escalating the query to the development team.

Regards

-----------------------------------------------------------------------
aignatovich
2019-07-24 05:21 PM
Re: Create dimensions for Filled Region Boundary

Hi! Try this code:

[Transaction(TransactionMode.Manual)]
public class CreateFillledRegionDimensionsCommand : IExternalCommand
{
  public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
  {
	var uiapp = commandData.Application;
	var uidoc = uiapp.ActiveUIDocument;
	var doc = uidoc.Document;

	var view = uidoc.ActiveGraphicalView;

	var filledRegions = FindFilledRegions(doc, view.Id).ToList();

	using (var transaction = new Transaction(doc, "filled regions dimensions"))
	{
	  transaction.Start();

	  foreach (var filledRegion in filledRegions)
	  {
		CreateDimensions(filledRegion, -1*view.RightDirection);

		CreateDimensions(filledRegion, view.UpDirection);
	  }

	  transaction.Commit();
	}
	return Result.Succeeded;
  }

  private static void CreateDimensions(FilledRegion filledRegion, XYZ dimensionDirection)
  {
	var document = filledRegion.Document;

	var view = (View) document.GetElement(filledRegion.OwnerViewId);

	var edgesDirection = dimensionDirection.CrossProduct(view.ViewDirection);

	var edges = FindRegionEdges(filledRegion)
	  .Where(x => IsEdgeDirectionSatisfied(x, edgesDirection))
	  .ToList();

	if (edges.Count < 2)
	  return;

	var shift = UnitUtils.ConvertToInternalUnits(-10*view.Scale, DisplayUnitType.DUT_MILLIMETERS)*edgesDirection;

	var dimensionLine = Line.CreateUnbound(filledRegion.get_BoundingBox(view).Min + shift, dimensionDirection);

	var references = new ReferenceArray();

	foreach (var edge in edges)
	  references.Append(edge.Reference);

	document.Create.NewDimension(view, dimensionLine, references);
  }

  private static bool IsEdgeDirectionSatisfied(Edge edge, XYZ edgeDirection)
  {
	var edgeCurve = edge.AsCurve() as Line;

	if (edgeCurve == null)
	  return false;

	return edgeCurve.Direction.CrossProduct(edgeDirection).IsAlmostEqualTo(XYZ.Zero);
  }

  private static IEnumerable<Edge> FindRegionEdges(FilledRegion filledRegion)
  {
	var view = (View)filledRegion.Document.GetElement(filledRegion.OwnerViewId);

	var options = new Options
	  {
		View = view,
		ComputeReferences = true
	  };

	return filledRegion
	  .get_Geometry(options)
	  .OfType<Solid>()
	  .SelectMany(x => x.Edges.Cast<Edge>());
  }

  private static IEnumerable<FilledRegion> FindFilledRegions(Document document, ElementId viewId)
  {
	var collector = new FilteredElementCollector(document, viewId);

	return collector
	  .OfClass(typeof (FilledRegion))
	  .Cast<FilledRegion>();
  }
}

It produces something like this:

dimensioned-filled-region.PNG

/a/case/sfdc/15582252/attach/dimensioned-filled-region.png

-----------------------------------------------------------------------
jvillaroel
2019-07-24 06:21 PM
Re: Create dimensions for Filled Region Boundary

Thanks, @aignatovich. I really appreciate it.

I'll give it a try with my script and I'll post here how it turned!.

Regards

-----------------------------------------------------------------------
aignatovich
2019-07-24 06:33 PM
Re: Create dimensions for Filled Region Boundary

Dimensioning in Revit is one of my favorite topics Robot Happy

-----------------------------------------------------------------------
jvillaroel  in reply to:  Rankaignatovich
‎2019-07-24 10:48 PM
Re: Create dimensions for Filled Region Boundary

Glad to hear! Now I know where to focus my questions! 😄

-----------------------------------------------------------------------
jvillaroel
2019-07-26 09:32 PM
Re: Create dimensions for Filled Region Boundary

Hi @aignatovich . Your suggestion was the solution to my problem!. I took (if you don't mind) the liberty to extend the approach, so the method asks for the type name (string) of the dimension you want to assign:

  private void CreateDimensions(
    FilledRegion filledRegion,
    XYZ dimensionDirection,
    string typeName)
  {
    var document = filledRegion.Document;

    var view = (View)document.GetElement(filledRegion.OwnerViewId);

    var edgesDirection = dimensionDirection.CrossProduct(view.ViewDirection);

	var edges = FindRegionEdges(filledRegion)
	  .Where(x => IsEdgeDirectionSatisfied(x, edgesDirection))
	  .ToList();

	if (edges.Count < 2)
	  return;

	// Se hace este ajuste para que la distancia no depende de la escala. <<<<<< evaluar para información de acotado y etiquetado!!!
	var shift = UnitUtils.ConvertToInternalUnits(5 * view.Scale, DisplayUnitType.DUT_MILLIMETERS) * edgesDirection;

	var dimensionLine = Line.CreateUnbound(
	  filledRegion.get_BoundingBox(view).Min + shift,
	  dimensionDirection);

	var references = new ReferenceArray();

	foreach (var edge in edges)
	  references.Append(edge.Reference);

	Dimension dim = document.Create.NewDimension(view, dimensionLine, references);

	ElementId dr_id = DimensionTypeId(
		document, typeName);

	if (dr_id != null)
	{
	  dim.ChangeTypeId(dr_id);
	}
  }

  private static bool IsEdgeDirectionSatisfied(Edge edge, XYZ edgeDirection)
  {
	var edgeCurve = edge.AsCurve() as Line;

	if (edgeCurve == null)
	  return false;

	return edgeCurve.Direction.CrossProduct(edgeDirection).IsAlmostEqualTo(XYZ.Zero);
  }

  private static IEnumerable<FilledRegion> FindFilledRegions(Document document, ElementId viewId)
  {
	var collector = new FilteredElementCollector(document, viewId);

	return collector
	  .OfClass(typeof(FilledRegion))
	  .Cast<FilledRegion>();
  }

  private static IEnumerable<Edge> FindRegionEdges(FilledRegion filledRegion)
  {
	var view = (View)filledRegion.Document.GetElement(filledRegion.OwnerViewId);

	var options = new Options
	{
	  View = view,
	  ComputeReferences = true
	};

	return filledRegion
	  .get_Geometry(options)
	  .OfType<Solid>()
	  .SelectMany(x => x.Edges.Cast<Edge>());
  }

  private static ElementId DimensionTypeId(
	Document doc,
	string typeName)
  {
	List<Element> mt_coll = new FilteredElementCollector(doc)
	  .OfClass(typeof(DimensionType))
	  .WhereElementIsElementType()
	  .ToList();

	DimensionType dimType = null;

	foreach (Element type in mt_coll)
	{
	  if (type is DimensionType)
	  {
		if (type.Name == typeName)
		{
		  dimType = type as DimensionType;
		  break;
		}
	  }
	}
	return dimType.Id;
  }

image.png

/a/case/sfdc/15582252/attach/filled_region_dimensions_auto.png

Hope this helps someone else!

Regards!

-----------------------------------------------------------------------


img/filled_region.png
img/filled_region_dimensioned_by_ai.png
img/filled_region_dimensions.png
img/filled_region_dimensions_auto.png
