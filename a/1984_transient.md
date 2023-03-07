<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js"></script>
</head>

<!---

- RevitLookup Ideas #146
  https://github.com/jeremytammik/RevitLookup/discussions/146

- transient elements, jig, graphics:
  draw line visible on screen
  https://forums.autodesk.com/t5/revit-api-forum/draw-line-visible-on-screen/m-p/11778165#M69522

- Open files located in ACC Docs
  https://stackoverflow.com/questions/75530623/open-files-located-in-the-accdocs
  https://forums.autodesk.com/t5/revit-api-forum/opening-a-cloud-model-with-revit-api/m-p/11767222

- RVT to IFC export with DA4 Revit
  https://autodesk.slack.com/archives/C03FXKR0H6J/p1676932151859789

- stop using jpeg
  https://daniel.do/article/its-the-future-stop-using-jpegs/
  It’s the future — you can stop using JPEGs
  An overview of some compelling alternatives.

twitter:

A request for new ideas for enhancing RevitLookup, implementing transient elements for a jig, e.g., a pickpoint rubber band, and opening BIMs on ACC Docs with the @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS https://autode.sk/openaccdocs`

A request for new ideas for enhancing RevitLookup, implementing a pickpoint rubber band and opening BIMs on ACC Docs
&ndash; Request for RevitLookup ideas
&ndash; Transient elements for jig
&ndash; Opening a model in ACC Docs
&ndash; Stop using JPEG
&ndash; Stop using voice id...

linkedin:

A request for new ideas for enhancing RevitLookup, implementing transient elements for a jig, e.g., a pickpoint rubber band, and opening BIMs on ACC Docs with the #RevitAPI

https://autode.sk/openaccdocs`

- Request for RevitLookup ideas
- Transient elements for jig
- Opening a model in ACC Docs
- Stop using JPEG
- Stop using voice id...

#BIM #DynamoBim #AutodeskAPS #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Lookup Ideas, Jigs and ACC Docs Access

Today, we look at a request for new ideas for enhancing RevitLookup, implementing a pickpoint rubber band and opening BIMs on ACC Docs:

- [Request for RevitLookup ideas](#2)
- [Transient elements for jig](#3)
- [Transient `DirectShape` jig](#3.1)
- [Opening a model in ACC Docs](#4)
- [Stop using JPEG](#5)
- [Stop using voice id](#6)

####<a name="2"></a> Request for RevitLookup Ideas

Do you have any ideas
for [RevitLookup](https://github.com/jeremytammik/RevitLookup) enhancements?

A lot of exciting functionality has already been worked on in
the [dev](https://github.com/jeremytammik/RevitLookup/tree/dev)
and [dev_winui](https://github.com/jeremytammik/RevitLookup/tree/dev_winui) branches.
We expect to see that coming out quite soon.

Meanwhile, Roman [Nice3point](https://github.com/Nice3point) opened a discussion for collecting
[RevitLookup Ideas](https://github.com/jeremytammik/RevitLookup/discussions/146).
Your contributions there are welcome.
Thank you!

####<a name="3"></a> Transient Elements for Jig

A couple of ideas on creating transient elements graphics similar to the AutoCAD jig functionality using
the `IDirectContext3DServer` functionality or the temporary InCanvas graphics API were recapitulated in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [drawing line visible on screen](https://forums.autodesk.com/t5/revit-api-forum/draw-line-visible-on-screen/m-p/11778165):

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/10/onbox-directcontext-jig-and-no-cdn.html" target="_blank" rel="noopener">Onbox, DirectContext Jig and No CDN</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/01/transient-graphics-humane-ai-basic-income-and-lockdown.html" target="_blank" rel="noopener">Transient Graphics, Humane AI, BI and Lockdown</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/05/flip-mirror-transform-and-transient-graphics.html" target="_blank" rel="noopener">Flip, Mirror, Transform and Transient Graphics</a></li>
</ul>

<center>
<img src="img/pick_point_rubber_band.png" alt="Pick point rubber band" title="Pick point rubber band" width="600"/> <!-- 600 x 367 pixels -->
</center>

Lorenzo Virone shared a [different approach](https://forums.autodesk.com/t5/revit-api-forum/draw-line-visible-on-screen/m-p/11778165#M69522), creating and deleting database-resident Revit elements on the fly in a loop:

I faced a similar UI problem to create a rubber band between two points.
I used two functions, `Line.CreateBound` and `NewDetailCurve`, inside a loop to create a line at the cursor position, refresh, and delete the line every 0.1 seconds, until the user chooses the second point.
A little tricky, but it works fine for me, and Revit seems to execute these 2 functions very fast.

This trick will technically work with anything:
create new elements on each mouse movement, refresh, delete the created elements and replace them with new ones.
You can use either model or detail elements.
It's easy to implement, because you just need to call the two methods, e.g., like this:

<div style="border: #000080 1px solid; color: #000; font-family: 'Cascadia Mono', Consolas, 'Courier New', Courier, Monospace; font-size: 10pt">
<div style="background: #f3f3f3; color: #000000; max-height: 300px; overflow: auto">
<ol start="28" style="background: #ffffff; margin: 0; padding: 0;">
<li style="background: #f3f3f3">&#160; <span style="color:#0000ff">bool</span> done = <span style="color:#0000ff">false</span>;</li>
<li>&#160; List&lt;ElementId&gt; temp = <span style="color:#0000ff">new</span> List&lt;ElementId&gt;();</li>
<li style="background: #f3f3f3">&nbsp;</li>
<li>&#160; <span style="color:#0000ff">while</span> (!done)</li>
<li style="background: #f3f3f3">&#160; {</li>
<li>&#160;&#160;&#160; doc.Delete(temp);</li>
<li style="background: #f3f3f3">&nbsp;</li>
<li>&#160;&#160;&#160; <span style="color:#008000">// Create temp elements</span></li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#008000">// Save their IDs in `temp`</span></li>
<li>&#160;&#160;&#160; <span style="color:#008000">// Set `done` to `true` when finished</span></li>
<li style="background: #f3f3f3">&nbsp;</li>
<li>&#160;&#160;&#160; doc.regenerate();</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; uidoc.RefreshActiveView();</li>
<li>&#160;&#160;&#160; Thread.Sleep(500); <span style="color:#008000">// milliseconds</span></li>
<li style="background: #f3f3f3">&#160; }</li>
<li>&nbsp;</li>
<li style="background: #f3f3f3">&#160; <span style="color:#008000">// Your final elements are in `temp`</span></li>
</ol>
</div>
</div>

Many thanks to Lorenzo for sharing this nice solution.

####<a name="3.1"></a> Transient DirectShape Jig

Chuong Ho adds: This technique can also be used with a `DirectShape` element:

<div style="border: #000080 1px solid; color: #000; font-family: 'Cascadia Mono', Consolas, 'Courier New', Courier, Monospace; font-size: 10pt">
<div style="background: #f3f3f3; color: #000000; max-height: 300px; overflow: auto">
<ol start="1" style="background: #ffffff; margin: 0; padding: 0;">
<li><span style="color:#0000ff">using</span> Autodesk.Revit.DB;</li>
<li style="background: #f3f3f3"><span style="color:#0000ff">using</span> Autodesk.Revit.UI.Selection;</li>
<li><span style="color:#0000ff">using</span> System.Collections.Generic;</li>
<li style="background: #f3f3f3"><span style="color:#0000ff">using</span> Line = Autodesk.Revit.DB.Line;</li>
<li><span style="color:#0000ff">using</span> Point = Autodesk.Revit.DB.Point;</li>
<li>&nbsp;</li>
<li style="background: #f3f3f3"><span style="color:#0000ff">var</span> Doc = commandData.Application.ActiveUIDocument.Document;</li>
<li>&nbsp;</li>
<li style="background: #f3f3f3"><span style="color:#0000ff">using</span> TransactionGroup trang = <span style="color:#0000ff">new</span> TransactionGroup(Doc, <span style="color:#a31515">&quot;test&quot;</span>);</li>
<li>trang.Start();</li>
<li style="background: #f3f3f3">XYZ a = UIDoc.Selection.PickPoint(ObjectSnapTypes.None);</li>
<li>SetPoint(a);</li>
<li style="background: #f3f3f3">XYZ b = UIDoc.Selection.PickPoint(ObjectSnapTypes.None);</li>
<li>SetPoint(b);</li>
<li style="background: #f3f3f3">SetLine(a, b);</li>
<li>XYZ p1 = UIDoc.Selection.PickPoint(ObjectSnapTypes.None);</li>
<li style="background: #f3f3f3">SetPoint(p1);</li>
<li>XYZ p2 = UIDoc.Selection.PickPoint(ObjectSnapTypes.None);</li>
<li style="background: #f3f3f3">SetPoint(p2);</li>
<li><span style="color:#0000ff">bool</span> isSamSide = IsSamSide(p1, p2, a, b);</li>
<li style="background: #f3f3f3">MessageBox.Show(isSamSide.ToString());</li>
<li>trang.Assimilate();</li>
<li style="background: #f3f3f3">&nbsp;</li>
<li><span style="color:#008000">// visualize a point</span></li>
<li style="background: #f3f3f3"><span style="color:#0000ff">void</span> SetPoint(XYZ xyz)</li>
<li>{</li>
<li style="background: #f3f3f3">&nbsp;&nbsp;<span style="color:#0000ff">using</span> (Transaction tran = <span style="color:#0000ff">new</span> Transaction(Doc, <span style="color:#a31515">&quot;Add point&quot;</span>))</li>
<li>&nbsp;&nbsp;{</li>
<li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;tran.Start();</li>
<li>&nbsp;&nbsp;&nbsp;&nbsp;Point point1 = Point.Create(xyz);</li>
<li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;DirectShape ds =</li>
<li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DirectShape.CreateElement(Doc, <span style="color:#0000ff">new</span> ElementId(BuiltInCategory.OST_GenericModel));</li>
<li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;ds.SetShape(<span style="color:#0000ff">new</span> List&lt;GeometryObject&gt;() { point1 });</li>
<li>&nbsp;&nbsp;&nbsp;&nbsp;tran.Commit();</li>
<li style="background: #f3f3f3">&nbsp;&nbsp;}</li>
<li>}</li>
<li style="background: #f3f3f3">&nbsp;</li>
<li><span style="color:#008000">// visualize a line</span></li>
<li style="background: #f3f3f3"><span style="color:#0000ff">void</span> SetLine(XYZ x1, XYZ x2)</li>
<li>{</li>
<li style="background: #f3f3f3">&nbsp;&nbsp;<span style="color:#0000ff">using</span> (Transaction tran = <span style="color:#0000ff">new</span> Transaction(Doc, <span style="color:#a31515">&quot;Add line&quot;</span>))</li>
<li>&nbsp;&nbsp;{</li>
<li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;tran.Start();</li>
<li>&nbsp;&nbsp;&nbsp;&nbsp;Line line = Line.CreateBound(x1, x2);</li>
<li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;DirectShape ds = DirectShape.CreateElement(</li>
<li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Doc, <span style="color:#0000ff">new</span> ElementId(BuiltInCategory.OST_GenericModel));</li>
<li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;ds.SetShape(<span style="color:#0000ff">new</span> List&lt;GeometryObject&gt;() { line });</li>
<li>&nbsp;&nbsp;&nbsp;&nbsp;tran.Commit();</li>
<li style="background: #f3f3f3">&nbsp;&nbsp;}</li>
<li>}</li>
</ol>
</div>
</div>

<center>
<img src="img/pick_point_rubber_band_jig_directshape.gif" alt="Pick point rubber band" title="Pick point rubber band" width="426"/> <!-- 426 x 271 pixels -->
</center>

Many thanks to Chuong Ho for this addition!

####<a name="4"></a> Opening a Model in ACC Docs

We started out
discussing [opening a cloud model with Revit API](https://forums.autodesk.com/t5/revit-api-forum/opening-a-cloud-model-with-revit-api/m-p/11767222) in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160),
but then moved it over to StackOverflow, the better place for such a cloud-related topic, where my colleague Eason Kang explains
how to [open files located in ACC Docs](https://stackoverflow.com/questions/75530623/open-files-located-in-the-accdocs):

**Question:** My Visual Studio Revit API add-in open Revit files to export data in a batch.
I can add many files which are on the networks and the plugin automatically opens them all.
Is it possible to also open files that are in the ACC Docs cloud?

I know I can open AccDocs which were be already downloaded locally by searching for them in the collaboration cache folder, but how to open files which have not yet been downloaded?

**Answer:** Since you mention the collaboration cache folder, I assume you are using
the [Revit Cloud Worksharing model](https://knowledge.autodesk.com/support/bim-360/learn-explore/caas/CloudHelp/cloudhelp/ENU/About-BIM360/files/about-bim-360-design/About-BIM360-about-bim-360-design-about-revit-cloud-worksharing-html-html.html),
a.k.a. `C4R`, the model for Autodesk Collaboration for Revit.

If so, you can make use of
the [APS Data Management API](https://aps.autodesk.com/en/docs/data/v2/developers_guide/overview/) to
obtain the `projectGuid` and `modelGuid` in the model version tip like this:

<pre class="prettyprint lang-json">
{
   "type":"versions",
   "id":"urn:adsk.wipprod:fs.file:vf.abcd1234?version=1",
   "attributes":{
      "name":"fileName.rvt",
      "displayName":"fileName.rvt",
      ...
      "mimeType":"application/vnd.autodesk.r360",
      "storageSize":123456,
      "fileType":"rvt",
      "extension":{
         "type":"versions:autodesk.bim360:C4RModel",
         ....
         "data":{
            ...
            "projectGuid":"48da72af-3aa6-4b76-866b-c11bb3d53883",
            ....
            "modelGuid":"e666fa30-9808-42f4-a05b-8cb8da576fe9",
            ....
         }
      }
   },
   ....
}
</pre>

With those in hand, you can open the C4R model using Revit API like this:

<pre class="prettyprint lang-cs">
  // where is your BIM360/ACC account based, US or EU?

  var region = ModelPathUtils.CloudRegionUS;

  var projectGuid = new Guid("48da72af-3aa6-4b76-866b-c11bb3d53883");
  var modelGuid = new Guid("e666fa30-9808-42f4-a05b-8cb8da576fe9");

  // For Revit 2023 and newer:

  var modelPath = ModelPathUtils.ConvertCloudGUIDsToCloudPath(
    region, projectGuid, modelGuid );

  // For Revit 2019 - 2022:

  //var modelPath = ModelPathUtils.ConvertCloudGUIDsToCloudPath(
  //  projectGuid, modelGuid );

  var openOptions = new OpenOptions();

  app.OpenAndActivateDocument( modelPath, openOptions ); // on desktop

  // on Design Automation for Revit or
  // to not activate the model on Revit desktop:

  // app.OpenDocumentFile( modelPath, openOptions );
</pre>

You can also make use
the [Visual Studio APS Data Management package on NuGet](https://www.nuget.org/packages/Autodesk.Forge) for this.
The [Hubs Browser tutorial](https://tutorials.autodesk.io/tutorials/hubs-browser/) demonstrates its use.

References:

- [Accessing BIM 360 design models on Revit](https://aps.autodesk.com/blog/accessing-bim-360-design-models-revit)
- [How to open a cloud model](https://thebuildingcoder.typepad.com/blog/2020/04/revit-2021-cloud-model-api.html#4.4)
- [Developer's guide online help on cloud models](https://help.autodesk.com/view/RVT/2023/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Application_and_Document_CloudFiles_html)

Many thanks to Eason for this comprehensive answer!

####<a name="5"></a> Stop Using JPEG

Moving away from Revit and its API to other interesting current news, Daniel Immke suggests
that [it’s the future &ndash; you can stop using JPEGs ](https://daniel.do/article/its-the-future-stop-using-jpegs) and
presents an overview and rationale for some compelling alternatives, e.g., AVIF and WebP.

####<a name="6"></a> Stop Using Voice Id

Joseph Cox describes [how he broke into a bank account with an AI-generated voice](https://www.vice.com/en/article/dy7axa/how-i-broke-into-a-bank-account-with-an-ai-generated-voice)
&ndash; some banks tout voice ID as a secure way to log into your account.
He proves it's possible to trick such systems with free or cheap AI-generated voices.
