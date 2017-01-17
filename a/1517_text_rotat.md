<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- create vertical dimensioning
12551564 [How can I create dimension line that is not parallel to detail line]
http://forums.autodesk.com/t5/revit-api-forum/how-can-i-create-dimension-line-that-is-not-parallel-to-detail/m-p/6801271

- set text note rotation
12550175 [Textnote - how to set rotation in Revit 2016]
http://forums.autodesk.com/t5/revit-api-forum/textnote-how-to-set-rotation-in-revit-2016/m-p/6800468

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

&ndash; ...

#AULondon, #UI, #innovation, #RevitAPI, @AutodeskRevit bit.ly/2j7Sxkb

-->

### Vertical Dimensioning, TextNote Rotation, Revit API QAS Research

Two more issues worth highlighting from 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160),
and more research on implementing a Revit API question answering system:

- [Creating vertical dimensioning](#2)
- [Setting `TextNote` rotation](#3)
- [Revit API QAS research continued...](#4)


####<a name="2"></a>Creating Vertical Dimensioning

A summary of the discussion thread
on [how to create dimension line that is not parallel to detail
line](http://forums.autodesk.com/t5/revit-api-forum/how-can-i-create-dimension-line-that-is-not-parallel-to-detail/m-p/6801271),
answered by Fair59:

**Question:** I am trying to create a dimension line that measures the Z component of a sloped detail line.

I can do this manually in the Revit user interface but cannot figure out how to do it with the API.

I've used [RevitLookup](https://github.com/jeremytammik/RevitLookup) to compare the differences in the dimension lines.

The `ReferenceArray` is the same; the main difference seems to be the `Curve` property which I'm assuming is set with the `Line` property when I call `NewDimension(Section, Line, ReferenceArray)`. 
 
Here is my code:

<pre class="code">
  XYZ point3 = new XYZ(417.8, 80.228, 46.8);
  XYZ point4 = new XYZ(417.8, 80.811, 46.3);
  
  Line geomLine3 = Line.CreateBound(point3, point4);
  
  Plane geomPlane = new Plane(XYZ.BasisX, XYZ.Zero);
  using (Transaction tx = new Transaction(doc))
  {
    tx.Start("tx");
    SketchPlane sketch = SketchPlane.Create(doc, geomPlane);
    DetailLine line3 = doc.Create.NewDetailCurve(viewSection, geomLine3) as DetailLine;
  
    ReferenceArray refArray = new ReferenceArray();
    refArray.Append(line3.GeometryCurve.GetEndPointReference(0));
    refArray.Append(line3.GeometryCurve.GetEndPointReference(1));
    XYZ dimPoint1 = new XYZ(417.8, 80.118, 46.8);
    XYZ dimPoint2 = new XYZ(417.8, 80.118, 46.3);
    Line dimLine3 = Line.CreateBound(dimPoint1, dimPoint2);
    Dimension dim = doc.Create.NewDimension(viewSection, dimLine3, refArray);
    
    tx.Commit();
  }
</pre>

The line I'm sending into NewDimension is along the z-axis, but the dimension line I'm given is sloped parallel to the detail line and gives the length of the detail line when I want just the z-component. Using Revit Lookup I see the Curve Direction has a y- and z-component for Direction (0, 0.759, -0.651). When I create the line I want in Revit itself the Curve has a Direction of (0, 0, 1) and gives just the length of the z-component of the sloped detail line. 
 
Attached is an image of the section with the sloped detail line at the bottom right. It shows the dimension I create with the API (labeled with length of 0' - 9 7/32") and the one I create in Revit that I want to create with the API (labeled as 0' - 6").
 
Revit Dimension Parallel.PNG

/a/case/sfdc/12551564/revit_dimension_parallel.png

<center>
<img src="img/.png" alt="" width="350"/>
</center>

I think I am in the correct plane. I'm in the Y-Z plane with the X-axis perpendicular to the plane and trying to measure a dimension in the Z-direction. This works with a detail line that has end points with the same Y-coordinate or same Z-coordinate, but is not working with the detail line with end points with different Y-coordinates and Z-coordinates, though X-coordinates are the same, so still in the Y-Z plane.
 
I did more debugging in the code and found that the `Dimension` object returned by `doc.Create.NewDimension` is correct and what I want. For example, it has the value string of "0' - 6\"" which is the length of the detail line in the Z-direction as I expect. But after the call to `Transaction.Commit`, the `Dimension` object changes. For example, the value string changes to "0' - 9 7/32\"", which is the total length of the detail line.
 
So the call to `Transaction.Commit` is changing the `Dimension` object and forcing the dimension line to be parallel to the detail line. I tried setting the `Dimension` property `IsLocked` to true before calling `Commit`, hoping that would fix the problem, but that does something even more odd. Here is an image to showing the new output, which has a line with the correct value string but parallel to the detail line instead of only in the z-direction:
 
Revit Dimension Parallel 2.PNG

/a/case/sfdc/12551564/revit_dimension_parallel_2.png
 
**Answer:** I at your code and see that you are not really using the `geomPlane`.
 
I have found this method that will result in a "stable" dimension with a correct direction and value:

<pre class="code">
  XYZ point3 = new XYZ(417.8, 80.228, 46.8);
  XYZ point4 = new XYZ(417.8, 80.811, 46.3);

  Line geomLine3 = Line.CreateBound(point3, point4);
  Line dummyLine = Line.CreateBound(XYZ.Zero, XYZ.BasisY);
  using (Transaction tx = new Transaction(doc))
  {
    tx.Start("tx");
    DetailLine line3 = doc.Create.NewDetailCurve(viewSection, geomLine3) as DetailLine;
    DetailLine dummy = doc.Create.NewDetailCurve(viewSection, dummyLine) as DetailLine;
    ReferenceArray refArray = new ReferenceArray();
    refArray.Append(dummy.GeometryCurve.Reference);
    refArray.Append(line3.GeometryCurve.GetEndPointReference(0));
    refArray.Append(line3.GeometryCurve.GetEndPointReference(1));
    XYZ dimPoint1 = new XYZ(417.8, 80.118, 46.8);
    XYZ dimPoint2 = new XYZ(417.8, 80.118, 46.3);
    Line dimLine3 = Line.CreateBound(dimPoint1, dimPoint2);
    Dimension dim = doc.Create.NewDimension(viewSection, dimLine3, refArray);

    doc.Delete(dummy.Id);
    tx.Commit();
  }
</pre>

**Response:** This solved the issue. I was able to adapt it to get the Y-dimension of the sloped line as well. Thank you for the solution!

I wonder what happens if you delete the dummy line... will the stable reference remain? Probably not...



####<a name="3"></a>Setting TextNote Rotation

####<a name="4"></a>More Research on a Revit QAS

As I already mentioned in several steps, I would like to implement a question answering system for the Revit API and all aspects of Revit API usage and add-in programming.

Luckily, this is a so-called <b><i>closed domain</i></b> and thus an easier task because the system can exploit domain-specific knowledge, as opposed to an open domain dealing with questions about nearly anything whatsoever.

I need to assemble a Revit API [ontology](https://en.wikipedia.org/wiki/Ontology_(information_science)).

I can use various sources for that:

- The Revit API help file `RevitAPI.chm` contents, now accessible [online at `revitapidocs.com`](http://www.revitapidocs.com).
- The Revit [Developer Guide](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42).
- The [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160).
- StackOverflow questions tagged with [`revit-api`](http://stackoverflow.com/questions/tagged/revit-api) and [`revitpythonshell`](http://stackoverflow.com/questions/tagged/revitpythonshell).
- [The Building Coder](http://thebuildingcoder.typepad.com) and other blog content.
- Emails I sent.
- Many other sources.

- Ontology &ndash; generate from RevitAPI.chm?
- Full text background information &ndash; generate from developer guide and blog posts?
- Existing questions and answers &ndash; generate from discussion forum threads and blog posts?