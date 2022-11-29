<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- How to make an Interior Designer Happy (with Electron, IFC.js and Revit API)
  https://www.linkedin.com/pulse/how-make-interior-designer-happy-electron-ifcjs-revit-capasso
  speed up the process of comparing and selecting families for our interior design projects.
  numerous categories of families with hundreds of families in each

twitter:

 with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### 64-Bit Element Ids



####<a name="2"></a> 64-Bit Element Ids

Autodesk almost never discusses any upcoming functionality in future products whatsoever, for legal reasons.

Still just as a heads-up warning, the development team thought it worthwhile to point out that we are thinking about possibly converting the internal representation of Revit element ids from 32 to 64 bit.

<!--

> Forward looking statements Disclaimer. This document contains “forward looking statements” as defined or implied in common law and within the meaning of the Corporations Law. All expressions an expectations or belief sas to future events or results are expressed in good faith... However, forward looking statements are subject to risks, uncertainties and other factors, which could cause actual results to differ materially from future results expressed, projected or implied by such forward looking statements...

-->

Here is a writeup of what that would imply and what a developer would need to do about it:

ElementIds will change from storing 32-bit integers to storing 64-bit values (type `long` in C# or `Int64` in .NET).
This will allow for larger and more complicated models.
Most functions which take or return ElementIds will continue to work with no changes.
However, there are a few things to keep in mind:
 
If you are storing ElementIds externally as integers, you will need to update your storage to take 64-bit values.
 
The constructor Autodesk.Revit.DB.ElementId(Int32) has been deprecated and replaced by a new constructor, Autodesk.Revit.DB.ElementId(Int64).

The property Autodesk.Revit.DB.ElementId.IntegerValue has been deprecated.
It returns only the lowest 32-bits of the ElementId value.
Please use the replacement property Autodesk.Revit.DB.ElementId.Value, which will return the entire value.
 
Support has been added for using 64-bit types in ExtensibleStorage.
Both Autodesk.Revit.DB.ExtensibleStorage.SchemaBuilder.AddSimpleField() and AddMapField() can now take in 64-bit values for the fieldType, keyType, and valueType parameters.
 
Two binary breaking changes have been made.
Both Autodesk.Revit.DB.BuiltInCategory and Autodesk.Revit.DB.BuiltInParameter have been updated so that the size type of the underlying enums are 64-bits instead of 32.
Code built against earlier versions of the API may experience type cast and other type related exceptions when run against the next versions of the API when working with the enum.
Please rebuild your addins against the next release's API when it is available.
 
Here is a table showing the deprecated and replacement members:

<table>
<tr><td>Deprecated API</td><td>Replacement</td></tr>
<tr><td>Autodesk.Revit.DB.ElementId(Int32)</td><td>Autodesk.Revit.DB.ElementId(Int64)</td></tr>
<tr><td>Autodesk.Revit.DB.ElementId.IntegerValue</td><td>Autodesk.Revit.DB.ElementId.Value</td></tr>
</table>
 
####<a name="3"></a> Beyond Dynamo: Python manual for Revit

####<a name="4"></a> Beyond Dynamo: Python manual for Revit

I avoid advertising commercial products, but I made an exception
for [Más Allá de Dynamo](https://thebuildingcoder.typepad.com/blog/2020/12/dynamo-book-and-texture-bitmap-uv-coordinates.html#3),
the Spanish-Language Python manual focused on Dynamo and the Revit API.

It is now also available in English
as [Beyond Dynamo: Python manual for Revit](https://www.amazon.com/dp/B0BMSV6YXD),
by Kevin Himmelreich, Alejandro Martín-Herrer and Ignacio Moreu:

> This is a Python handbook specifically created for working on BIM methodology with Autodesk Dynamo and Revit.
It has a practical approach aimed at professionals who have never programmed before.
If you know how to program in Python, it will be equally useful, as it explains deeply most of the classes, methods and properties of the Revit API.

<center>
<img src="img/beyond_dynamo_en.png" alt="" title="" width="252"/>  <!-- 252 × 330 -->
</center>

Note that we also discussed lots of other resources
for [learning Python and Dynamo](https://thebuildingcoder.typepad.com/blog/2021/02/addin-file-learning-python-and-ifcjs.html#3).



**Response:** 





 
- Revit Schedule - Title/headers
https://forums.autodesk.com/t5/revit-api-forum/revit-schedule-title-headers/m-p/11573145#M67572
10168713 [Revit Schedule - Title/headers]
Many asked for a snippet of sample code. 
Hernan Echevarria [H.echeva](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3063892)
jumoped in andkindly shared his implementation:
I found this post which was very helpful, thank you @jeremytammik for the info.
I have created a small example macro that gets the Header text (it is very basic and may need to be made safer). I hope this helps
/// <summary>
/// This macro gets the header text of the active Schedule View
/// </summary>
public void ScheduleHeader()
{
UIDocument uidoc = this.ActiveUIDocument;
Document doc = uidoc.Document;

ViewSchedule mySchedule =  uidoc.ActiveView as ViewSchedule;

TableData myTableData = mySchedule.GetTableData();

TableSectionData myData = myTableData.GetSectionData(SectionType.Header);

TaskDialog.Show("Header Info", "The Header Text is: \n" +myData.GetCellText(0,0));
}

####<a name="5"></a> Managing Thousands of Families with Electron and IFC.js

Emiliano Capasso, Head of BIM at [Antonio Citterio Patricia Viel](https://www.citterio-viel.com),
shared some interesting advice 
on [how to make an interior designer happy with Electron, IFC.js and Revit API](https://www.linkedin.com/pulse/how-make-interior-designer-happy-electron-ifcjs-revit-capasso).

The aim is to speed up the process of comparing and selecting families for large interior design projects.
They require numerous categories of families with hundreds of families in each.
Initial idea: developing a nice web viewer in IFC.js for viewing Families instead of buildings.
Now, every interior designer or architect in the office can navigate (way faster than opening the showrooms in Revit) inside all the showrooms using their browser.
That leads to the even better idea...

Thank you Emiliano, for the nice write-up.
