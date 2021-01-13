<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- how to extract the geometry and the texts of the title block in a sheetview?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-extract-the-geometry-and-the-texts-of-the-title-block-in/m-p/9998687
  /a/doc/revit/tbc/git/a/img/pm_sheet_view_text_geom.jpg
  [XPS or the Open XML Paper Specification] https://en.wikipedia.org/wiki/Open_XML_Paper_Specification

- The human side of AI for chess
  https://www.microsoft.com/en-us/research/blog/the-human-side-of-ai-for-chess/

- An Engineering Argument for Basic Income
  https://scottsantens.com/engineering-argument-for-unconditional-universal-basic-income-ubi-fault-tolerance-graceful-failure-redundancy
  Utilizing fault-tolerant design in critical life support systems

- Assessing Mandatory Stay‐at‐Home and Business Closure Effects on the Spread of COVID‐19
  https://onlinelibrary.wiley.com/doi/10.1111/eci.13484

- Stanford Studie mit Top Medizin-Wissenschaftler Ioannidis zeigt keinen Nutzen von Lockdowns
  https://tkp.at/2021/01/11/stanford-studie-mit-top-medizin-wissenschaftler-ioannidis-zeigt-keinen-nutzen-von-lockdowns/

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
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### Title Block Geometry and Text

####<a name="2"></a> Extracting Title Block Geometry and Text

**Question:** I want to extract all that is visible and correctly placed in the attached image.

SheetView.jpg
84 KB
Tags (0)
Add tags
Report
15 REPLIES 
Sort: 
MESSAGE 2 OF 16
Yien_Chao
 Collaborator Yien_Chao in reply to: pmeigneux
‎2020-12-16 05:25 PM 
hi

for the text, you can get it with most of the BuiltInParameter (search for "SHEET_ ")  https://www.revitapidocs.com/2020/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm

now for the geometry, i'm not sure if i understand, you mean the box of text or the frame of the sheet?

Tags (0)
Add tags
Report
MESSAGE 3 OF 16
studio-a-int
 Advocate studio-a-int in reply to: pmeigneux
‎2020-12-16 06:31 PM 
A Title Block is a Revit Family.

You can Filter and select the elements then work with the selections to retrieve all the Parameters you need:

FilteredElementCollector collector = new FilteredElementCollector(document);
ICollection<Element> lines = collector.OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Lines).ToElements();
ICollection<Element> texts = collector.OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_TextNotes).ToElements();
ICollection<Element> filledRegions = collector.OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_FilledRegion).ToElements();
//etc.

Tags (0)
Add tags
Report
MESSAGE 4 OF 16
pmeigneux
 Advocate pmeigneux in reply to: studio-a-int
‎2020-12-17 10:36 AM 
to be more precise. I want to recreate the sheetview identically to create an exportable document. I managed to extract the content of the views that compose it but at real size and not yet find how to extract the cartridge and its content.

Tags (0)
Add tags
Report
MESSAGE 5 OF 16
pmeigneux
 Advocate pmeigneux in reply to: studio-a-int
‎2021-01-11 02:57 PM 
This solution does not work, the lists are empty.

Tags (0)
Add tags
Report
MESSAGE 6 OF 16
pmeigneux
 Advocate pmeigneux in reply to: pmeigneux
‎2021-01-11 03:01 PM 
@jeremytammikhave you a solution for this case ?

Tags (0)
Add tags
Report
MESSAGE 7 OF 16
pmeigneux
 Advocate pmeigneux in reply to: pmeigneux
‎2021-01-11 03:04 PM 
see detail..

Tags (0)
Add tags
Report
MESSAGE 8 OF 16
jeremytammik
 Employee jeremytammik in reply to: pmeigneux
‎2021-01-11 03:54 PM 
Dear Pierre,

you say you want to recreate the sheet view identically.

For that, I would suggest using the Copy and Paste API.

If you want to access the text and visible geometry to recreate a facsimile outside of Revit, I would suggest following Yien_Chao's good advice and accessing text and geometry separately, e.g., using a filtered element collector for real data and a screen snapshot for the appearance.

Cheers,

Jeremy

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Tags (0)
Add tags
Report
MESSAGE 9 OF 16
pmeigneux
 Advocate pmeigneux in reply to: jeremytammik
‎2021-01-11 04:27 PM 
I want to create a facsimile outside of Revit, but filtered element collector result lists empty.

FilteredElementCollector collector = new FilteredElementCollector(Doc);
ICollection<Element> lines = collector.OfCategory(BuiltInCategory.OST_Lines).ToElements();
ICollection<Element> texts = collector.OfCategory(BuiltInCategory.OST_TextNotes).ToElements();
ICollection<Element> filledRegions = collector.OfCategory(BuiltInCategory.OST_FilledRegion).ToElements();

foreach (DetailLine line in lines)
{
Curve curve = line.GeometryCurve;
Line LI = (Line) curve;
if (LI != null)
{
TaskDialog.Show("Line", "Start X=" + LI.GetEndPoint(0).X + "Y=" + LI.GetEndPoint(0).Y + "Z=" + LI.GetEndPoint(0).Z + "\n" +
"End X=" + LI.GetEndPoint(1).X + "Y=" + LI.GetEndPoint(1).Y + "Z=" + LI.GetEndPoint(1).Z);
}
}
foreach (TextNote text in texts)
{
}

Screenshot_2.jpg
269 KB
Tags (0)
Add tags
Report
MESSAGE 10 OF 16
jeremytammik
 Employee jeremytammik in reply to: pmeigneux
‎2021-01-11 04:38 PM 
As always, you can use RevitLookup to analyse the properties and other attributes of the Revit elements you are trying to retrieve with the filtered element collector. That will show you which filters you need to apply to extract the desired elements. If the filter returns no elements,. you are applying too restrictive or downright erroneous filters.

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Tags (0)
Add tags
Report
MESSAGE 11 OF 16
pmeigneux
 Advocate pmeigneux in reply to: jeremytammik
‎2021-01-12 12:47 PM 
On the FamilyInstance of my ViewSheet, it finds neither OST_Lines nor OST_TextNodes (for the extract the Title Block).

Do you have a filter syntax that could find them ?

Tags (0)
Add tags
Report
MESSAGE 12 OF 16
jeremytammik
 Employee jeremytammik in reply to: pmeigneux
‎2021-01-12 01:15 PM 
Dear Pierre,

The most efficient way to define such a filter is for you to pick the elements you are filtering for and explore their properties using RevitLookup.

This has been explained numerous times in the past:

https://thebuildingcoder.typepad.com/blog/2017/01/virtues-of-reproduction-research-mep-settings-onto...

Best regards,

Jeremy

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Tags (0)
Add tags
Report
MESSAGE 13 OF 16
pmeigneux
 Advocate pmeigneux in reply to: jeremytammik
‎2021-01-12 01:54 PM 
I have already explored the viewsheet and the associated familyinstance as well as the symbol and the family. None has geometry.

Tags (0)
Add tags
Report
MESSAGE 14 OF 16
RPTHOMAS108
 Advisor RPTHOMAS108 in reply to: pmeigneux
‎2021-01-13 12:15 AM 
The FamilyDocument of the title block will contain the geometry you would probably have to open the family for editing, it may have various nested annotation symbols that also need to be traversed.

Besides that there is the issue of items such as images and schedules.

Would probably be easier to read the fixed document sequence of a printed xps file. This is an XML file containing path data (mini language) similar to what you get in Xaml and 'Glyphs' entries for text objects (render transforms give their positions). Takes a while to print an XPS but if it is only the title block may be quicker. You would still have to interpret the xml but as difficult to impossible tasks go it is slightly less impossible.

XPS is a zip file, change the extension to Zip and unpack it or alternatively there are namespaces for reading packages for such directly (System.IO.Packaging).

I don't envy your task, I usually just print a pdf when I want a copy of something. HPGL is another historic plotter vector format that could be easily interpreted.

Tags (0)
Add tags
Report
MESSAGE 15 OF 16
pmeigneux
 Advocate pmeigneux in reply to: RPTHOMAS108
‎2021-01-13 12:47 PM 
my project is to export a viewsheet in svg format. currently my code export correctly all viewports but I still have the title block (drawing and data) to export. if I understand correctly there is no way to export the title block (curve and position of the data)?

Tags (0)
Add tags
Report
MESSAGE 16 OF 16
RPTHOMAS108
 Advisor RPTHOMAS108 in reply to: pmeigneux
‎2021-01-13 03:58 PM 
As you've noted Element.Geometry is null for title blocks.

The only approach I know of with the RevitAPI would be to open the title block family (Document.EditFamily) and extract such lines from the plan view within that. As noted above however this family could also contain revision schedule that you can't extract lines from, other nested generic annotation families and images. The parameter text values would not be correct but from the text strings used in Labels and parameter names you can sometimes infer such a relationship (between text locations in your title block family and positions on your ViewSheet in the project). If label has a preview parameter value (which is often the case) then this can't be done. Also the placement of multiple parameters with line breaks etc. in the same label add to complexity. You don't get a description of these with the API (which parameters used in which label).

Ideally this task would be suited to CustomExporter but I believe from previous discussions that this doesn't support ViewSheets.

XLST could potentially be used to convert Xaml path data previously noted to svg (although I know of no existing templates for such).

Another potential long winded option would be to export sheet with only title block showing to DWG and import it on a drafting view to analyse the geometry. Not sure it is a good option but could be done. DWG links favour polylines (any lines of the same layer joined together will be grouped into polylines). Text object positions could only be found be exploding dwg (not sure if there is API function for that).

<center>
<img src="img/pm_sheet_view_text_geom.jpg" alt="Sheet view title block geometry and text" title="Sheet view title block geometry and text" width="500"/> <!-- 1154 -->
</center>

**Answer:** 

**Response:**

####<a name="3"></a>

####<a name="4"></a> 
