<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- The height and width of the dimension text
  https://forums.autodesk.com/t5/revit-api-forum/the-height-and-width-of-the-dimension-text/m-p/10873262

- get text font outline geometry from TextNode in custom export
  Converting text to geometry when performing a 2d view export
  https://forums.autodesk.com/t5/revit-api-forum/converting-text-to-geometry-when-performing-a-2d-view-export/m-p/10201712#M54774
  /Users/jta/a/doc/revit/tbc/git/a/img/text_geometry.png

- retrieve 2D geometry of generic element
  View Reference Location
  https://forums.autodesk.com/t5/revit-api-forum/view-reference-location/m-p/10867150

twitter:

The 2D custom exporter provides a basis for a powerful approach to access detailed 2D geometry, e.g., retrieve dimension text height and width, determine text font geometry and retrieve 2D geometry of any element in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/2dtextgeo

The 2D custom exporter provides a basis for a new and much more powerful approach to access detailed 2D geometry
&ndash; Retrieve dimension text height and width
&ndash; Determine text font geometry
&ndash; Retrieve 2D geometry of any element...

linkedin:

The 2D custom exporter provides a powerful approach to access detailed 2D geometry in the #RevitAPI

https://bit.ly/2dtextgeo

- Retrieve dimension text height and width
- Determine text font geometry
- Retrieve 2D geometry of any element...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Detailed 2D Text and Other Element Geometry

Determining the extents of a text element has been a recurring and challenging task with several tricky solutions suggested in the past, e.g.:

<!-- 0610 0646 1223 1440 1517 -->

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/07/text-size.html">Text Size</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/09/textnote-lost-in-space.html">TextNote Lost in Space?</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/10/new-text-note-and-text-width-calculation.html">New Text Note and Text Width Calculation</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/05/idea-station-and-textnote-bounding-box.html">TextNote Bounding Box</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/01/textnote-rotation-forge-devcon-tensorflow-and-keras.html">TextNote Rotation</a></li>
</ul>

The 2D custom exporter provides a basis for a much more powerful approach to address this, as already discussed once way back then 
in [using a custom exporter for 2D](https://thebuildingcoder.typepad.com/blog/2015/11/au-begins-and-using-a-custom-exporter-for-2d.html#2):

- [Retrieve dimension text height and width](#2)
- [Determine text font geometry](#3)
- [Retrieve 2D geometry of any element](#4)

####<a name="2"></a> Retrieve Dimension Text Height and Width

The latest question in this series asks how to determine
[the height and width of the dimension text](https://forums.autodesk.com/t5/revit-api-forum/the-height-and-width-of-the-dimension-text/m-p/10873262):

**Question:** When dimension text overlaps, I want to move one of the dimensions to avoid the overlap.
My idea is to calculate the rectangular border of the text through the position of the text and the width and height of the text, and then judge whether the rectangular borders intersect.
So, how to calculate the width and height of dimension text?

**Answer:** That should be possible using the approaches described in these two other recent threads:

Look at these two recent threads here in the forum:

- Get the text font outline geometry from the `TextNode` in a 2D custom export as described
for [converting text to geometry when performing a 2D view export](https://forums.autodesk.com/t5/revit-api-forum/converting-text-to-geometry-when-performing-a-2d-view-export/m-p/10201712)
- Retrieve 2D geometry of generic element, explained in the question
on [view reference location](https://forums.autodesk.com/t5/revit-api-forum/view-reference-location/m-p/10867150)

####<a name="3"></a> Determine Text Font Geometry

Haroon Haider describes his successful approach to access text font geometry in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [converting text to geometry when performing a 2D view export](https://forums.autodesk.com/t5/revit-api-forum/converting-text-to-geometry-when-performing-a-2d-view-export/m-p/10201712):

**Question:** We have been able to get access to the lines, grids, annotations and other basic elements of a 2D view using the `IExportContext2D` interface.
Is there a way to convert the annotation text from strings to geometry using Revit API similar to how you can do in AutoCAD? 
I'm looking to get something like this:

<center>
<img src="img/text_font_geom.png" alt="Text font geometry" title="Text font geometry" width="223"/> <!-- 223 -->
</center>

**Answer:** I ended up getting what I need by processing a `TextNode` from the export context method `OnText` call.
There is enough information in the text node class to be able to convert it to a `GraphicsPath` and pull out the geometry from there.

Many thanks to Haroon for sharing this!

####<a name="4"></a> Retrieve 2D Geometry of any Element

Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
shares a generic solution to retrieve the 2D geometry of any element in the thread 
on [view reference location](https://forums.autodesk.com/t5/revit-api-forum/view-reference-location/m-p/10867150):

**Question:** I am making a simple tool to add a leader annotation symbol to a View Reference, but have run into a bit of an issue.
In RevitLookup, it doesn't appear that the location of a View Reference is defined anywhere in the API.
Looking through the API docs as well, I can't find a class for View Reference either, which makes me think that its properties aren't accessible.
Am I missing something?
Obviously, a simple workaround is to just have the user click a start and end point, but ideally I'd like to have the start be defined by the View Reference location.

**Answer:** The Revit API supports exactly (and only) two data types for the Location property: `LocationPoint` and `LocationCurve`.
Some elements have different kinds of location definitions that do not fit into either of these two.
In that case, they are not accessible in the API, just as you surmised.
Is the view reference location visible in any way in the end user interface?
What does it look like in that context?
Is there any reason why it might be considered something that does not fit into a point or a curve?

In general, classes that show up as `Element` in RevitLookup have no specific API functionality beyond the element base class.

As the location of the element it not exposed, nor is the geometry, you have two options:

- Centre of BoundingBox, or
- Implement `IExportContext2D` and gather the geometry via that.

I would do the latter as it gives you more control.

- Use `OnElementBegin2D` to check the id against the element you want to look at; then pass `Skip` or `Proceed` depending on whether your id matches.
- `OnCurve` will give you details of curves (called for each).
- `OnText` will give you details of text.

Note that this will happen regardless of what you set for `RenderNodeAction` in `OnElementBegin2D`.
The best way to avoid calling `OnText` for text elements you are not interested in is to do as follows:

- During `OnElementBegin2d`, store the `ElemendId` as a variable
- During `OnElementEnd2d` set the `ElementId` variable to `InvalidElementId`
- During `OnText` exit the sub if variable is not set to `InvalidElementId`

To export text, you also need to set `Export2DIncludingAnnotationObjects` to true.

I'm finding the best approach is to implement `IExportContext2D` via a general class and then inherit that.
This avoids having to implement all the members each time and you can build in some filtering similar to above:

<pre class="code">
Imports Autodesk.Revit.DB

Public Class RT_ExportContext2d_Limited
  Implements IExportContext2D

  Private IntDoc As Document = Nothing
  Private FilterEIDs As List(Of ElementId) = Nothing
  Private IntCurrentEID As ElementId = ElementId.InvalidElementId

  Public Property DefaultAction As RenderNodeAction = RenderNodeAction.Proceed

  Public Sub New(D As Document, Optional TargetElementIds As List(Of ElementId) = Nothing)
    FilterEIDs = TargetElementIds
    IntDoc = D
  End Sub

  Public Overridable Function Start() As Boolean Implements IExportContext.Start
    Return True
  End Function
  Public Overridable Sub Finish() Implements IExportContext.Finish
  End Sub
  Public Overridable Function IsCanceled() As Boolean Implements IExportContext.IsCanceled
    Return False
  End Function

#Region "OnBegins"
  Public Overridable Function OnViewBegin(node As ViewNode) As RenderNodeAction Implements IExportContext.OnViewBegin
    Return DefaultAction
  End Function
  Public Function OnElementBegin2D(node As ElementNode) As RenderNodeAction Implements IExportContext2D.OnElementBegin2D
    IntCurrentEID = node.ElementId

    If FilterEIDs IsNot Nothing Then
      If FilterEIDs.Contains(node.ElementId) Then
        Return RenderNodeAction.Proceed
      Else
        Return RenderNodeAction.Skip
      End If
    Else
      Return RenderNodeAction.Proceed
    End If

    OnElementBegin2D_Overridable(node)
  End Function
  Public Overridable Sub OnElementBegin2D_Overridable(node As ElementNode)
  End Sub

  Public Function OnElementBegin(elementId As ElementId) As RenderNodeAction Implements IExportContext.OnElementBegin
    'Never called for 2D export
    Return DefaultAction
  End Function
  Public Function OnInstanceBegin(node As InstanceNode) As RenderNodeAction Implements IExportContext.OnInstanceBegin
    Return DefaultAction
  End Function
  Public Function OnFaceBegin(node As FaceNode) As RenderNodeAction Implements IExportContext.OnFaceBegin
    Return DefaultAction
  End Function
  Public Overridable Function OnFaceEdge2D(node As FaceEdgeNode) As RenderNodeAction Implements IExportContext2D.OnFaceEdge2D
    Return DefaultAction
  End Function

  Public Function OnLinkBegin(node As LinkNode) As RenderNodeAction Implements IExportContext.OnLinkBegin
    Return DefaultAction
  End Function

#End Region

#Region "OnFunctions_Returns"
  Public Overridable Function OnFaceSilhouette2D(node As FaceSilhouetteNode) As RenderNodeAction Implements IExportContext2D.OnFaceSilhouette2D
    Return DefaultAction
  End Function
  Public Overridable Function OnCurve(node As CurveNode) As RenderNodeAction Implements IExportContextBase.OnCurve
    Return DefaultAction
  End Function
  Public Overridable Function OnPolyline(node As PolylineNode) As RenderNodeAction Implements IExportContextBase.OnPolyline
    Return DefaultAction
  End Function
#End Region

#Region "OnEnds_NoReturns"
  Public Sub OnElementEnd2D(node As ElementNode) Implements IExportContext2D.OnElementEnd2D
    IntCurrentEID = Nothing
    OnElementEnd2D_Overridable(node)
  End Sub
  Public Overridable Sub OnElementEnd2D_Overridable(node As ElementNode)
  End Sub

  Public Overridable Sub OnViewEnd(elementId As ElementId) Implements IExportContext.OnViewEnd
  End Sub
  Public Sub OnElementEnd(elementId As ElementId) Implements IExportContext.OnElementEnd
  End Sub

  Public Overridable Sub OnInstanceEnd(node As InstanceNode) Implements IExportContext.OnInstanceEnd
  End Sub
  Public Overridable Sub OnLinkEnd(node As LinkNode) Implements IExportContext.OnLinkEnd
  End Sub
  Public Overridable Sub OnFaceEnd(node As FaceNode) Implements IExportContext.OnFaceEnd
  End Sub
#End Region

#Region "OnSubs_NoReturns"
  Public Overridable Sub OnLineSegment(segment As LineSegment) Implements IExportContextBase.OnLineSegment
  End Sub
  
  Public Overridable Sub OnPolylineSegments(segments As PolylineSegments) Implements IExportContextBase.OnPolylineSegments
  End Sub
  
  Public Sub OnText(node As TextNode) Implements IExportContextBase.OnText
    'For tags etc. some calls to this will be during on instance
    'e.g. fixed text within family
    'Some call will be outside OnInstance but before OnElementEnd2D of associated element
    'e.g. tag values (variable text).

    If IntCurrentEID = ElementId.InvalidElementId Then Exit Sub Else
    OnText_Overridable(node)
  End Sub

  Public Overridable Sub OnText_Overridable(node As TextNode)
  End Sub

  Public Overridable Sub OnRPC(node As RPCNode) Implements IExportContext.OnRPC
  End Sub
  Public Overridable Sub OnLight(node As LightNode) Implements IExportContext.OnLight
  End Sub
  Public Overridable Sub OnMaterial(node As MaterialNode) Implements IExportContext.OnMaterial
  End Sub
  Public Overridable Sub OnPolymesh(node As PolymeshTopology) Implements IExportContext.OnPolymesh
  End Sub
#End Region

End Class
</pre>

A lot of the members are not called for the 2D scenario.

Here is a correction required to `OnText`, i.e., check the list of filtered contains the current id.

<pre class="code">
 Public Sub OnText(node As TextNode) Implements IExportContextBase.OnText
    If FilterEIDs IsNot Nothing Then
      If FilterEIDs.Contains(IntCurrentEID) = False Then
        Exit Sub
      End If
    End If

    OnText_Overridable(node)
End Sub
</pre>

The best way to understand the order of the exporter method calls is to log them.

**Response:** Thanks so much!
That's a clever implementation of `IExportContext2d`.
Appreciate the help!

Many thanks to Richard for sharing this!


