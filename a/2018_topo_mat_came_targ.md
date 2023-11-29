<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

https://autodesk.slack.com/archives/C0SR6NAP8/p1700155564738649
The creation of the type is based on this thread: https://autodesk.slack.com/archives/C0SR6NAP8/p1673638599787309

twitter:

#RevitAPI @AutodeskAPS @AutodeskRevit #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Camera Target and Toposolid Subdivision Material

Today, lets take a look at two illuminating internal conversation that

####<a name="2"></a> Using the Built-In CefSharp Browser

Andrej Licanin of [Bimexperts](https://bimexperts.com/sr/home) shared
a nice succint little solution demonstrating how to use the Revit built-in CefSharp installation to display a browser in a WPF control
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [simple WPF with a chromium browser guide](https://forums.autodesk.com/t5/revit-api-forum/simple-wpf-with-a-chromium-browser-guide/td-p/12396552):

> This is for the all lost souls out there, may you avoid my suffering.

> To create a chromium web browser in your Revit addin, you need to reference cefsharp dlls.
This is where I had a major hiccup, because I just installed the newest one by nuget.
Don't do this.
Revit already has them and it initializes them on start-up.
You just need to add references to them like the other Revit API .dlls.
They are in the CefSharp folder.
For me, that is *C:\Program Files\Autodesk\Revit 2022\CefSharp*,
and the files are:

> - CefSharp
- CefSharp.BrowserSubprocess.Core
- CefSharp.Core
- CefSharp.Wpf

Once installed, you can just add a WPF window, reference the namespace and embed a chromium browser like this:

<pre class="prettyprint">
&lt;Window x:Class="RevitTestProject.TestWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:local="clr-namespace:RevitTestProject"
        xmlns:cef="clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf"
        mc:Ignorable="d"
        Width="1000" Height="500"&gt;
    &lt;Grid Background="PapayaWhip"&gt;

        &lt;cef:ChromiumWebBrowser Name="ChromiumBrowser" Address="http://www.google.com" Width="900" Height="450"  /&gt;
    &lt;/Grid&gt;
&lt;/Window&gt;
</pre>

Hope this helps someone, as I lost an entire day figuring this out.

Thank you very much, Andrej!

####<a name="3"></a> Toposolid Subdivision Material

**Question:**
In Revit 2024, I'm creating a Toposolid using a ToposolidType that has a finish layer with a texture in the material.
I then use Toposolid.CreateSubDivision to create a subdivision in the toposolid, but this subdivision is not inheriting the material from the hosting toposolid.
Any ideas what I need to do to get the material to show properly?
Is there a way to set the material for the top face of a Toposolid?

<center>
<img src="img/toposolid_subdiv_mat_1.png" alt="Toposolid subdivision material" title="Toposolid subdivision material" width="600"/>
<p/>
<img src="img/toposolid_subdiv_mat_2.png" alt="Toposolid subdivision material" title="Toposolid subdivision material" width="600"/>
</center>

The brown areas are the subdivisions.

**Answer:** The subdivision is designed NOT to inherit materials from the host toposolid.
Also, "Edit Type" is not enabled in the UI.
As an alternative, could you try to change the material Id of the subdivision element and see if that works?

**Response:** Toposolid does not have a MaterialID.
That's why I had to bake the material into the ToposolidType.

**Answer:** The subdivision has a `BuiltInParameter` `TOPOSOLID_SUBDIVIDE_MATERIAL` to control the material.
It can only be set to one material id.
I did a quick example.
Please check out the following code for reference:

<pre class="prettyprint">
  [Transaction(TransactionMode.Manual)]
  public class ChangeSubdivisionMaterial : IExternalCommand
  {
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
      var uidoc = commandData.Application.ActiveUIDocument;
      var doc = uidoc.Document;
      var sel = uidoc.Selection;

      Toposolid topo = doc.GetElement(sel.PickObject(
        ObjectType.Element, new ToposolidFilter())) as Toposolid;
      ToposolidType topoType = doc.GetElement(topo.GetTypeId()) as ToposolidType;
      ElementId materialId = topoType.GetCompoundStructure().GetLayers().First().MaterialId;

      List&lt;Toposolid&gt; subdivisions = new FilteredElementCollector(doc)
        .OfClass(typeof(Toposolid))
        .OfType&lt;Toposolid&gt;()
        .Where(t =&gt; t.HostTopoId == topo.Id)
        .ToList();
      Transaction trans = new Transaction(doc, "change material");
      trans.Start();
      subdivisions.ForEach(t =&gt; t.get_Parameter(
        BuiltInParameter.TOPOSOLID_SUBDIVIDE_MATERIAL)
          .Set(materialId));
      trans.Commit();

      return Result.Succeeded;
    }
  }
</pre>

<center>
<img src="img/toposolid_subdiv_mat_3.gif" alt="Toposolid subdivision material" title="Toposolid subdivision material" width="800"/>
</center>

####<a name="4"></a> Camera Target

https://autodesk.slack.com/archives/C0SR6NAP8/p1700492804688869
Jeff Hotchkiss
I'm looking to convert Revit views for transfer into cloud REST API calls e.g. into LMV. Looking over supplied Revit help on the subject, I've found most of what I need but I'm curious if there's any way in API to retrieve the view camera target point as listed in that documentation? The docs suggest that the target can be reset to the fov centre, but not how to get the existing value, or whether the value has any significance to Revit e.g. orbit point. Thanks in advance for any assistance. (edited)
Alex Pytel
I am not finding a way to get the target using the API. As a workaround, I can suggest trying to fake the target location. For example, if you look from above, the field of view makes a triangle. If you know the horizontal extent of the view, you can find how far the base of the triangle is from the camera's eye. Note that this is different from the distance to near / far planes. You could also perform a calculation based on the bounding box of the scene (view outline). If you can get the outline in view coordinates (and have it axis-aligned), then you can assume that the target is on the far face of the box. (This is still different from the far clipping plane.)
Unfortunately, the target is not used consistently in Revit, including for navigation. So, it is fair to say that the significance of the location is not great. This might  explain why it has been neglected in API.
Jeff Hotchkiss
I think I grasp the fundamentals you mean, but am unsure which components in the API describe them. For example, you mention a distinction between the base of the view's frustum (triangle as seen from above when including the origin) and the far clipping plane. Yet the diagram in the docs (lets' start with perspective) shows the Crop Box of the view as describing the clipping planes. Not clear to me which object contains this smaller frustum you reference. (of course this is all hard to convey in text, we might need a diagram :laughing:).
I should also have been specific in that the views of interest for us are 3D at this time.
Alex Pytel
The crop box is view aligned and all six sides are potentially clipping planes.
Near/Far as used with perspective are generally independently managed.
For your case, I would suggest that you disregard perspective near/far and just try to obtain some sensible values based on the crop box and view outline.
In other words, a workaround for not having a target position is to try to locate the target based on scene depth. You could place the fake target in the center of some bounding volume of the scene or on its far side.
You could even try to shoot a ray and intersect the closest object. I think that can be done using the API.
It will not be the camera's exact target, however. But, as I said, it is not consistently used for navigation, so it might not be that useful anyway.
Jeff Hotchkiss
Sounds good - I think my current code uses the far side of the crop box at the moment. I'll experiment further. Some of this will likely depend on what LMV and related tooling does with the input anyway.
a followup if you know - is it feasible to find out from the Revit API the actual navigation pivot point?
Alex Pytel
I doubt it, because it is a dynamic concept. For example, if you have something selected then the pivot point can be the center of selection.
It looks like you might be able to get/set a home camera using ViewNavigationToolSettings.
And it has a pivot point (but no target). That seems to be the closest one can get...
For completeness, there is a target value one can get during the custom exporter process, but it is a fake value, which is computed as I roughly described above. It's something like 0.5 * view width / tan(fov / 2).
Jeff Hotchkiss
Thanks very much!
And target and pivot are npt the same.

- [Custom Exporter GetCameraInfo](http://thebuildingcoder.typepad.com/blog/2014/09/custom-exporter-getcamerainfo.html)
- [Revit Camera Settings, Project Plasma, DA4R and AI](https://thebuildingcoder.typepad.com/blog/2019/06/revit-camera-settings-project-plasma-da4r-and-ai.html)
- [Revit Camera FOV, Forge Partner Talks and Jobs](https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html)
- [Save and Restore 3D View Camera Settings](https://thebuildingcoder.typepad.com/blog/2020/10/save-and-restore-3d-view-camera-settings.html)




####<a name="5"></a>



