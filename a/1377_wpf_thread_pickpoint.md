<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #adsklabs #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#rtceur
#geometry #3d

Revit API, Jeremy Tammik, akn_include

PickPoint with #WPF and No Threads #revitapi #bim #aec #3dwebcoder #adsk #au2015 #apis #xaml

Here is another contribution from Saied Zarrinmehr, of the BIM-SIM Research Group at the Texas A&amp;M University, on the thread safety of the Selection.PickPoint method in Revit 2014 and 2015, resulting in a sample testing different methods for hiding and unhiding a WPF window. I trust they are interesting and useful for people working with Revit 2016 as well. Before getting to that, Calvin and Hobbes anniversary...

-->

### PickPoint with WPF and No Threads Attached

Here is another contribution from Saied Zarrinmehr of the BIM-SIM Research Group at
the [Texas A&M University](https://www.tamu.edu), on
the thread safety of the Selection.PickPoint method in Revit 2014 and 2015, resulting in a sample testing different methods for hiding and unhiding a WPF window.

I trust they are interesting and useful for people working with Revit 2016 as well.

Before getting to that, I also have to mention that yesterday was a most important anniversary, brought to my attention by Miles Tryon-Petith at the [University of Wisconsin-Madison](http://www.wisc.edu):

The first publication of [Calvin and Hobbes](https://en.wikipedia.org/wiki/Calvin_and_Hobbes) 30 years ago!

<center>
<img src="img/calvin_hobbes_1985-11-18.jpg" alt="Calvin and Hobbes">
</center>

Thank you, Miles, for the notice.

Last year, Saied already shared with us how
to [create topography contours and building masses](http://thebuildingcoder.typepad.com/blog/2014/11/creating-topography-contours-and-building-masses.html).

Here is the new topic:

#### <a name="2"></a>Question

I have developed a library for some spatial analysis tasks (crowd simulation, Isovist calculation, etc.) which works with BIM data in Revit. I wanted my library to have minimum dependency to any external library, including Revit API libraries, to be able to use it for IFC and other data schemes. Therefore, I designed a series of interface and abstract classes in C# for getting data and sometimes for visualization. The main platform for visualization is however a WPF window. I have difficulties with the uidoc.Selection.PickPoint function in Revit API library. Is it thread safe?

My code snippet for visualization is like this:

<pre class="code">
  public interface IVisualize
  {
    void VisualizeBoundary(UV[] points, double elevation);
    void VisualizeLine(UVLine line, double elevation);
    void VisualizePoint(UV pnt, double size, double elevation);
    void VisualizeLines(ICollection<UVLine> lines, double elevation);
    UV PickPoint(string message);
  }
</pre>

In the following code the Interface is implemented. The Revit document is passed to this class as a static method of the IExternalCommand Interface (ForceModel.doc).

<pre class="code">
  public class RevitVisualizer : IVisualize
  {
    [STAThread]
    public void VisualizeBoundary(SpatialAnalysis.Geometry.UV[] points, double elevation)
    {
      using (Transaction t = new Transaction(ForceModel.doc, "Draw Boundary"))
      {
        t.Start();
        FailureHandlingOptions failOpt = t.GetFailureHandlingOptions();
        failOpt.SetFailuresPreprocessor(new CurveDrawingWarningSwallower());
        t.SetFailureHandlingOptions(failOpt);
        Plane p = ForceModel.doc.Application.Create.NewPlane(XYZ.BasisZ, new XYZ(0, 0, elevation));
        SketchPlane skp = SketchPlane.Create(ForceModel.doc, p);
        for (int i = 0; i < points.Length; i++)
        {
          try
          {
            XYZ p1 = new XYZ(points[i].U, points[i].V, elevation);
            int j = (i == points.Length - 1) ? 0 : i + 1;
            XYZ p2 = new XYZ(points[j].U, points[j].V, elevation);
            Line l = Line.CreateBound(p1, p2);
            ForceModel.doc.Create.NewModelCurve(l, skp);
          }
          catch (Exception e)
          { MessageBox.Show(e.Message); }
        }
        t.Commit();
      }
    }
    [STAThread]
    public void VisualizeLine(UVLine line, double elevation)
    {
      using (Transaction t = new Transaction(ForceModel.doc, "Draw Barriers"))
      {
        t.Start();
        FailureHandlingOptions failOpt = t.GetFailureHandlingOptions();
        failOpt.SetFailuresPreprocessor(new CurveDrawingWarningSwallower());
        t.SetFailureHandlingOptions(failOpt);
        Plane p = ForceModel.doc.Application.Create.NewPlane(XYZ.BasisZ, new XYZ(0, 0, elevation));
        SketchPlane skp = SketchPlane.Create(ForceModel.doc, p);
        try
        {
          XYZ p1 = new XYZ(line.Start.U, line.Start.V, elevation);
          XYZ p2 = new XYZ(line.End.U, line.End.V, elevation);
          Line l = Line.CreateBound(p1, p2);
          ForceModel.doc.Create.NewModelCurve(l, skp);
        }
        catch (Exception e)
        { MessageBox.Show(e.Message); }

        t.Commit();
      }
    }
    [STAThread]
    public void VisualizePoint(SpatialAnalysis.Geometry.UV pnt, double size, double elevation)
    {
      XYZ p1 = new XYZ(pnt.U - size / 2, pnt.V - size / 2, elevation);
      XYZ p2 = new XYZ(pnt.U + size / 2, pnt.V + size / 2, elevation);
      XYZ q1 = new XYZ(pnt.U + size / 2, pnt.V - size / 2, elevation);
      XYZ q2 = new XYZ(pnt.U - size / 2, pnt.V + size / 2, elevation);
      using (Transaction t = new Transaction(ForceModel.doc, "Show Point"))
      {
        t.Start();
        FailureHandlingOptions failOpt = t.GetFailureHandlingOptions();
        failOpt.SetFailuresPreprocessor(new CurveDrawingWarningSwallower());
        t.SetFailureHandlingOptions(failOpt);
        Plane pln = ForceModel.doc.Application.Create.NewPlane(XYZ.BasisZ, new XYZ(0, 0, elevation));
        SketchPlane skp = SketchPlane.Create(ForceModel.doc, pln);
        Line l1 = Line.CreateBound(p1, p2);
        Line l2 = Line.CreateBound(q1, q2);
        ForceModel.doc.Create.NewModelCurve(l1, skp);
        ForceModel.doc.Create.NewModelCurve(l2, skp);
        t.Commit();
      }
      p1 = null; p2 = null; q1 = null; q2 = null;
    }
    [STAThread]
    public void VisualizeLines(ICollection<UVLine> lines, double elevation)
    {
      using (Transaction t = new Transaction(ForceModel.doc, "Draw lines"))
      {
        t.Start();
        FailureHandlingOptions failOpt = t.GetFailureHandlingOptions();
        failOpt.SetFailuresPreprocessor(new CurveDrawingWarningSwallower());
        t.SetFailureHandlingOptions(failOpt);
        Plane p = ForceModel.doc.Application.Create.NewPlane(XYZ.BasisZ, new XYZ(0, 0, elevation));
        SketchPlane skp = SketchPlane.Create(ForceModel.doc, p);
        foreach (UVLine item in lines)
        {
          try
          {
            XYZ p1 = new XYZ(item.Start.U, item.Start.V, elevation);
            XYZ p2 = new XYZ(item.End.U, item.End.V, elevation);
            Line l = Line.CreateBound(p1, p2);
            ForceModel.doc.Create.NewModelCurve(l, skp);
          }
          catch (Exception e)
          { MessageBox.Show(e.Message); }
        }
        t.Commit();
      }
    }
    [STAThread]
    public SpatialAnalysis.Geometry.UV PickPoint(string message)
    {
      UIDocument uidoc = new Autodesk.Revit.UI.UIDocument(ForceModel.doc);
      XYZ xyz = uidoc.Selection.PickPoint(message);
      return new SpatialAnalysis.Geometry.UV(xyz.X, xyz.Y);
    }
  }
</pre>

The problem that I have is with the PickPoint method. I want to use this function to allow the users to draw the visibility polygons in Revit if they want. It works great for the first time of use. But from the second time of using it, when drawing anything the following exception is raised: "Starting a transaction from an external application running outside of API context is not allowed". After that the connection of my app to Revit is lost. My code snippet for drawing an Isovist follows. The MainViewer.TargetVisualizer is a static instance of RevitVisualizer class.

<pre class="code">
    private void drawinRevit()
    {
      // hide the window
      this._host.Hide();
      // pick a point using the Interface
      UV p = MainViewer.TargetVisualizer.PickPoint("Pick a vantage point to draw polygonal Isovist");
      //show the form again
      this._host.Show();
      try
      {
        HashSet<UVLine> blocks = this._host.cellularFloor.VisualBlocks(p, this._host.isoDepth);
        //calculate the isovist polygon
        Barrier isovistPolygon = this._host.barrierEnvironment.IsovistPolygon(p, this._host.isoDepth, blocks);
        //visualize it
        isovistPolygon.Visualize(MainViewer.TargetVisualizer, this._host.barrierEnvironment.Elevation);
      }
      catch (Exception error0)
      {
        MessageBox.Show(error0.Message);
      }
    }
</pre>

Here is the Barrier class:

<pre class="code">
  public class Barrier
  {
    public UV[] BoundaryPoints { get; set; }
    public bool AscendingOrder { get; set; }
    public bool IsHole { get; set; }
    public int Length
    {
      get { return this.BoundaryPoints.Length; }
    }
    public bool IsHole { get; set; }
    public Barrier(UV[] points)
    {
      this.AscendingOrder = true;
      this.BoundaryPoints = points;
    }
    public void Visualize(IVisualize visualizer, double evevation)
    {
      visualizer.VisualizeBoundary(this.BoundaryPoints, evevation);
    }
    public static void VisualizeBarriers(IVisualize visualizer, Barrier[] barriers, double height = 0)
    {
      foreach (Barrier item in barriers)
      {
        visualizer.VisualizeBoundary(item.BoundaryPoints, height);
      }
    }
  }
</pre>

I have no problem with any other interface or abstract classes that use Revit API. It is only after using the uidoc.Selection.PickPoint when the problem starts. What is the reason for it? How can I fix it?



#### <a name="3"></a>Answer

Thank you for your very clear and interesting description.

I like your approach very much, including the dependency minimisation.

I have recently heard of other issues with PickObject, so it seems to be a stumbling point.

I cannot say off-hand what the problem is.

I can say off-hand that the Revit API is absolutely never thread safe:

- [No multithreading in Revit](http://thebuildingcoder.typepad.com/blog/2011/06/no-multithreading-in-revit.html)
- [The Revit API is never ever thread safe](http://thebuildingcoder.typepad.com/blog/2014/11/the-revit-api-is-never-ever-thread-safe.html)

Can you reconsider your approach under this assumption?

Have you read and understood the approach you need to take to drive Revit from outside?

Do you understand that the Revit API can never be used except in a valid Revit API context, and such a context is exclusively provided within event handler callback functions that you notify Revit about and that it then calls back?

This is extensively documented by The Building Coder topic group on [Idling and External Events for Modeless Access and Driving Revit from Outside](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28).

The best way to drive Revit from outside is via external events.

A good sample on how to architecture an add-in to use them is the ModelessDialog &gt; ModelessForm_ExternalEvent Revit SDK sample.

Once you can confirm understanding and implementing all the above, if the problem persists, I will pass on your query to the development team.

Re-reading your description, though, it seems that you are trying to drive Revit directly from outside.

Sorry, no go.

Unfortunately, this also means that your abstract interface class may become more complex.

I hope you can find a really simple solution for that too.

I really look forward to seeing this solved and hope that you will be willing and happy to share that in public.

Actually, I am planning an extensive blog post on the recent other problems that people encountered using pick point and trying to access the Revit API in illegal ways, so this question of yours and my answer may fit in quite well there as well just as they stand.


#### <a name="4"></a>Response

Thanks for taking your time and extensively elaborating the subject. Well I learned Revit's lack of support for multi-threading in the hard way!

I tried using the modeless method described in ModelessDialog &gt; ModelessForm_ExternalEvent SDK samples. It works, but from what I read this does not seem like a stable way of connecting to Revit and poses unknown challenges. For the moment I'd rather go with providing implementation for IExternalCommand interface, which is legal and straight forward.  By the way, illegal connection to Revit is not an unlawful action, is it!?

Attached is a [simple project](zip/sz_PickPointTest_1.zip) that you can run to see the problems that uidoc.Selection.PickPoint causes. In this simplified project if you click on the red button the API uidoc.Selection.PickPoint method will be called. And if you click on the green button the WPF mouse click event will be triggered to pick a point.  In both cases a fake isovist is produced and visualized both in WPF and Revit. When points are picked from WPF no exception is raised. When using the Revit API Pick point method for the first time, you will not encounter any exception. However, after that you will receive exceptions for any kind of drawing in Revit. It occurs to me that the  uidoc.Selection.PickPoint forces the app out of the main thread which is provided by IExternalCommand interface. You should be able to reproduce the problem now with the attached project.

I used an empty Architectural Template Revit Project for testing the code.


#### <a name="5"></a>WPF Window Hiding

I have solved the problem. I tested different methods for hiding and unhiding the WPF window, which includes

- For Hiding: `this.Visibility = System.Windows.Visibility.Hidden` and for unhiding: `this.Visibility = System.Windows.Visibility.Visible`
- For Hiding: `this.Hide()` and for unhiding: `this.Show()`
- For Hiding: `this.Hide()` and for unhiding: `this.ShowDialog()`

The thread Id of the WPF window will not change in any of the cases after using the API PickPoint method. The [attached project](zip/sz_PickPointTest_2.zip) shows that the third of the above methods works properly. I also found that every line of code after ShowDialog will wait for the WPF window to close to execute. I tested two different locations for unhiding the window. They are inside first and second code region alternatives. You may want to check lines 51 to 93 in the attached project, change it and see it for yourself.

I still do not completely understand what's happening and I appreciate your explanation. However, the good news is that the problem is solved.

Many thanks to Saied for sharing this research and solution!

Oh and regarding your question on "illegal connection to Revit is not an unlawful action, is it?" &ndash; nope, it will just hurt you and your customers if you crash Revit and corrupt the model. Nothing worse than that, though &nbsp; :-)

Maybe others can chip in and help with further explanations?

Thank you!
