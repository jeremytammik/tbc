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

- multi-version
  https://forums.autodesk.com/t5/revit-api-forum/optimal-add-in-code-base-approach-to-target-multiple-revit/m-p/12982599#M81063

- https://www.linkedin.com/posts/anthony-sertorio_constructiondata-autodeskconstructioncloud-activity-7236893852156977152-RFi1?utm_source=share&utm_medium=member_desktop

- https://ismy.blue/

twitter:

Multi-version @AutodeskRevit #RevitAPI add-in code base, using UIView zoom corners to determine element visibility, GPT LLM to read PDF and emails for fully automated generation of ACC issues #BIM @DynamoBIM https://autode.sk/multiversionrvtaddin

Current and recurring topics from the Revit API discussion forum, an LLM and a cultural colour topic
&ndash; Multi-version add-in code base
&ndash; UIView for element visibility
&ndash; GPT reads PDF + generates ACC issues
&ndash; My blue vs. green...

linkedin:

Multi-version #RevitAPI add-in code base, using UIView zoom corners to determine element visibility, GPT LLM to read PDF and emails for fully automated generation of ACC issues

https://autode.sk/multiversionrvtaddin

- Multi-version add-in code base
- UIView for element visibility
- GPT reads PDF + generates ACC issues
- My blue vs. green...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Element Visibility and Multi-Version Add-In Code Base

Let's look at two current and recurring topics from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160),
an LLM and a cultural colour topic:

- [Multi-version add-in code base](#2)
- [UIView for element visibility](#3)
- [GPT reads PDF + generates ACC issues](#4)
- [My blue vs. green](#5)

####<a name="2"></a> Multi-Version Add-In Code Base

Managing an add-in code base to support multiple releases of the Revit API is a recurring and constantly evolving issue.
In 2021, we discussed how to implement
a [multi-version Revit add-in](https://thebuildingcoder.typepad.com/blog/2021/10/dll-as-resource-and-multi-version-add-ins.html#4),
and in 2023 [managing multiple Revit API versions](https://thebuildingcoder.typepad.com/blog/2023/11/net-core-preview-and-open-source-add-in-projects.html#8)

The issue was raised again half a year ago in preparation of the non-trivial migration for Revit 2025 from .NET 4.8 to .NET core, in a quest for
an [optimal add-in code base approach to target multiple Revit releases](https://forums.autodesk.com/t5/revit-api-forum/optimal-add-in-code-base-approach-to-target-multiple-revit/m-p/12982599).

That query recently received some updated answers and solutions from
Roman [@Nice3point](https://t.me/nice3point) Karpovich, aka Роман Карпович,
and Nathan [@SamBerk](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3671855) Berkowitz.

Roman points out that [RevitTemplates](https://github.com/Nice3point/RevitTemplates) provides
a [step-by step guide for Revit multiversion templates](https://github.com/Nice3point/RevitTemplates/wiki/Step%E2%80%90by%E2%80%90step-Guide):

> You will be able to create a project in a few steps without spending hours setting up a solution from scratch. Try it.

Sam's simpler approach provides less coverage, saying:

I found that the *simplest* solution to maintain the same code base for 2025 and earlier is to put the `TargetFramework` in a condition.
Here is step by step:

- Create a dotnet 8 class library project
- Reference the Revit 2025 API DLL's RevitAPI.dll and RevitAPIUI.dll, set `Copy Local` to `No`
- Create configurations `2024Debug` and `2025Debug` (or release)
- Edit the project file and put the `TargetFramework` and the `Reference` in a condition:

<pre><code class="language-xml"> &lt;PropertyGroup Condition="'$(Configuration)' == '2024Debug'"&gt;
  &lt;TargetFramework&gt;net481&lt;/TargetFramework&gt;
&lt;/PropertyGroup&gt;
&lt;PropertyGroup Condition="'$(Configuration)' == '2025Debug'"&gt;
  &lt;TargetFramework&gt;net8.0&lt;/TargetFramework&gt;
&lt;/PropertyGroup&gt;

&lt;ItemGroup&gt;
   &lt;Reference Include="RevitAPI" Condition="'$(Configuration)' == '2024Debug'"&gt;
    &lt;HintPath&gt;..\..\..\..\..\..\..\..\Program Files\Autodesk\Revit 2024\RevitAPI.dll&lt;/HintPath&gt;
    &lt;Private&gt;False&lt;/Private&gt;
  &lt;/Reference&gt;
  &lt;Reference Include="RevitAPIUI" Condition="'$(Configuration)' == '2024Debug'"&gt;
    &lt;HintPath&gt;..\..\..\..\..\..\..\..\Program Files\Autodesk\Revit 2024\RevitAPIUI.dll&lt;/HintPath&gt;
    &lt;Private&gt;False&lt;/Private&gt;
  &lt;/Reference&gt;
  &lt;Reference Include="RevitAPI" Condition="'$(Configuration)' == '2025Debug'"&gt;
    &lt;HintPath&gt;..\..\..\..\..\..\..\..\Program Files\Autodesk\Revit 2025\RevitAPI.dll&lt;/HintPath&gt;
    &lt;Private&gt;False&lt;/Private&gt;
  &lt;/Reference&gt;
  &lt;Reference Include="RevitAPIUI" Condition="'$(Configuration)' == '2025Debug'"&gt;
    &lt;HintPath&gt;..\..\..\..\..\..\..\..\Program Files\Autodesk\Revit 2025\RevitAPIUI.dll&lt;/HintPath&gt;
    &lt;Private&gt;False&lt;/Private&gt;
  &lt;/Reference&gt;
&lt;/ItemGroup&gt;</code></pre>

- Create an App.cs file and implement IExternalApplication

<pre><code class="language-cs">public class App : IExternalApplication
{
  public Result OnStartup( UIControlledApplication a )
  {
    TaskDialog.Show( "Multi-Version Addin",
      $"Revit version: {a.ControlledApplication.VersionNumber}");

    return Result.Succeeded;
  }

  public Result OnShutdown( UIControlledApplication a )
  {
    return Result.Succeeded;
  }
}</code></pre>

- Create an output folder for both versions (*C:/.../output/2024* and *C:/.../output/2025*)
- Add a `.addin` file to your project for both versions (`MyAddin2024.addin` and `MyAddin2025.addin`)

<pre><code class="language-xml">&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;RevitAddIns&gt;
  &lt;AddIn Type="Application"&gt;
    &lt;Name&gt;MyAddin&lt;/Name&gt;
    &lt;Assembly&gt;C:\...\output\2024\MyAddin.dll&lt;/Assembly&gt; (or 2025)
    &lt;FullClassName&gt;MyAddin.App&lt;/FullClassName&gt;
    &lt;ClientId&gt;{client id}&lt;/ClientId&gt;
    &lt;VendorId&gt;{vendor id}&lt;/VendorId&gt;
    &lt;VendorDescription&gt;{vendor description}&lt;/VendorDescription&gt;
  &lt;/AddIn&gt;
&lt;/RevitAddIns&gt;</code></pre>

- Post-build events:

<pre><code class="language-cs">      echo Configuration: $(Configuration)
      if $(Configuration) == 2024Debug goto 2024
      if $(Configuration) == 2025Debug goto 2025

      :2024
      echo Copying results to 2024
      copy "$(ProjectDir)MyAddin2024.addin" "$(AppData)\Autodesk\REVIT\Addins\2024"
      copy "$(ProjectDir)bin\$(Configuration)\net481\*.dll" "C:\...\output\2024"
      goto exit

      :2024
      echo Copying results to 2024
      copy "$(ProjectDir)MyAddin2025.addin" "$(AppData)\Autodesk\REVIT\Addins\2025"
      copy "$(ProjectDir)bin\$(Configuration)\net8.0\*.dll" "C:\...\output\2025"
      goto exit

      :exit</code></pre>

- Build both configurations
- Open Revit 2024:

<center>
  <img src="img/multiversion_2024.png" alt="Multi-verxsion add-in" title="Multi-verxsion add-in" width="450"/>
</center>

- Open Revit 2025:

<center>
  <img src="img/multiversion_2025.png" alt="Multi-verxsion add-in" title="Multi-verxsion add-in" width="450"/>
</center>

- For debugging, add to the project file:

<pre><code class="language-xml">  &lt;PropertyGroup Condition="'$(Configuration)' == '2024Debug'"&gt;
    &lt;StartProgram&gt;C:\Program Files\Autodesk\Revit 2024\Revit.exe&lt;/StartProgram&gt;
    &lt;StartAction&gt;Program&lt;/StartAction&gt;
  &lt;/PropertyGroup&gt;

  &lt;PropertyGroup Condition="'$(Configuration)' == '2025Debug'"&gt;
    &lt;StartProgram&gt;C:\Program Files\Autodesk\Revit 2025\Revit.exe&lt;/StartProgram&gt;
    &lt;StartAction&gt;Program&lt;/StartAction&gt;
  &lt;/PropertyGroup&gt;</code></pre>

Thank you all!

####<a name="3"></a> UIView for Element Visibility

Fabio Loretti [@floretti](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/5076730) Oliveira
determined how to [select all UI visible family instances](https://forums.autodesk.com/t5/revit-api-forum/select-all-quot-ui-visible-quot-instances/td-p/12995081):

**Question:**
One of my users came up with an interesting question of whether or not it's possible to create a "Select All Instances > Visible in View" command taking into consideration the view's zoom level, meaning that everything outside the user's field of view is "not visible" and it wouldn't be part of the selection.

I've tried to find references in the API and online articles on the below topics and ran some UI tests and compared values via the Revit Lookup but no success.

- Active view zoom level
- Active view window size
- Bounding boxes XYZ and UV and whether they change under different zoom levels

**Answer:**
The first thing to try out is
the [filtered element collector taking a view element id](https://www.revitapidocs.com/2024/6359776d-915e-f8a2-4147-b31024671ee1.htm).

The description says, *Constructs a new FilteredElementCollector that will search and filter the visible elements in a view*, which exactly matches your query. I would be surprised if the two exactly matching descriptions really mean exactly the same thing, but who knows, you may be in luck.

**Response:**
Unfortunately, that overload of the FilteredElementCollector doesn't do what I'm after.
The definition of "visible" to the API is different to the definition of what a user considers visible.
The example below shows what I mean.

View B shows 8x wall elements.
If I decrease the size of a view/window or simply zoom in or pan I won't be able to see all the 8x walls anymore like shown on View A.
Regardless of whether or not I use the `FilteredElementCollector` and pass the view Id as a 2nd parameter or use the UI command *Select All Instances* &gt; *Visible in View*, Revit will select all 8x wall instances.

<center>
  <img src="img/elem_visible_1.png" alt="Elements visible in view" title="Elements visible in view" width="500"/>
</center>

**Answer:**
Does the article
on [retrieving elements visible in view](https://thebuildingcoder.typepad.com/blog/2017/05/retrieving-elements-visible-in-view.html) help?

**Response:**
I tested that approach and these are the results.

This is View A showing all 8x wall instances and its `ViewPlan.CropBox` values underneath as recommended by the article:

<center>
  <img src="img/elem_visible_2.png" alt="Elements visible in view" title="Elements visible in view" width="500"/>
</center>

I then panned across View A so only 4x wall instances are shown in the UI and again the `ViewPlan.CropBox` values underneath.

<center>
  <img src="img/elem_visible_3.png" alt="Elements visible in view" title="Elements visible in view" width="500"/>
</center>

Notice that the values didn't change; based on that, I do not expect the outcome to be different if I turn this into code. I noticed that the API has two Boolean properties to indicate whether the `CropBox` is either active and visible; in my case, neither of them are true.
Based on that, my assumption is that the recommended approach in the article you shared can only work via the cropbox use and not by zooming in/out and panning across a view.

<center>
  <img src="img/elem_visible_4.png" alt="Elements visible in view" title="Elements visible in view" width="500"/>
</center>

**Answer:**
OK, I see that the crop box approach does not help in this case.
Searching the forum, I found these two specific solutions for other situations:

- [How to list only elements that are "Visible" in a view](https://forums.autodesk.com/t5/revit-api-forum/how-to-list-only-elements-that-are-quot-visible-quot-in-a-view/m-p/10663861)
- [Selection filter for only what is visible from camera](https://forums.autodesk.com/t5/revit-api-forum/selection-filter-for-only-what-is-visible-from-camera/m-p/12534209)

In your case, maybe
the [`UIView`](https://www.revitapidocs.com/2024/2a070256-00f0-5cab-1412-bee5bbfcfc5e.htm) can help:

The `View` element is part of the document and lives in the database.
It maybe does not know how it is currently being "looked at".
The `UIView` may know that and provides the current zoom corners from which you can determine whether an element is currently within them or not.
However, for non-planar views, you will have some interesting calculations to perform.

I used the `UIView` to implement
a [tooltip that detects which elements are visible under the cursor](https://thebuildingcoder.typepad.com/blog/2012/10/uiview-windows-coordinates-referenceintersector-and-my-own-tooltip.html).
It also uses the `ReferenceIntersector`, like one of the solutions I pointed out above.

**Response:**
Amazing, thanks Jeremy.
The `GetZoomCorners` method is exactly what I need.
Much appreciated.
:-)

Just giving back, here is the solution I implemented.

Note this code searches for a specific `familyName` and `familyType`, so it works with family instances and it won't work with system families:

<pre><code class="language-cs">// Get active view's zoom corners
var zc = new List&lt;XYZ&gt;();

var openUIviews = uidoc.GetOpenUIViews();
foreach (var uiView in openUIviews)
{
  if(uiView.ViewId == doc.ActiveView.Id)
    zc = uiView.GetZoomCorners().ToList();
}

// Get selection and expand it
var selIds = uidoc.Selection.GetElementIds();
var finalSelectionIds = new List&lt;ElementId&gt;();

foreach (ElementId id in selIds)
{
  Outline viewExtents = new Outline(
    new XYZ(zc.First().X, zc.First().Y, -1000),
    new XYZ(zc.Last().X, zc.Last().Y, 1000));

  var filter = new BoundingBoxIntersectsFilter(viewExtents);

  var famInst = doc.GetElement(id) as FamilyInstance;
  var allFamInst
    = new FilteredElementCollector(doc, doc.ActiveView.Id)
      .WherePasses(filter)
      .OfClass(typeof(FamilyInstance))
      .Cast&lt;FamilyInstance&gt;()
      .Where(x =&gt; x.Symbol.Family.Name.Equals(familyName)) // family
      .Where(x =&gt; x.Name.Equals(familyType)); // family type

  foreach (FamilyInstance item in allFamInst)
  {
    finalSelectionIds.Add(item.Id);
  }
}

uidoc.Selection.SetElementIds(finalSelectionIds);</code></pre>

Many thanks to Fabio for testing, confirming and sharing this solution.

####<a name="4"></a> GPT Reads PDF + Generates ACC Issues

My colleague [Anthony Sertorio](https://www.linkedin.com/in/anthony-sertorio/) shared
a [post on LinkedIn](https://www.linkedin.com/posts/anthony-sertorio_constructiondata-autodeskconstructioncloud-activity-7236893852156977152-RFi1) on
using an LLM to read and analyse documentation and email text to automatically generate and submit issues to ACC:

> Using OpenAI's custom GPTs with Autodesk Construction Cloud to automate capturing issues from project documents...
... the ability to process several documents at once while identifying multiple issues to raise within each individual document
... used to analyse and extract insights from more general project documents like drawings and specifications.

> Getting this to work was a real challenge, especially since the GPT builder can be very temperamental.
Some of the hardest things to work out were the OpenAPI spec needed to communicate with Autodesk's API's, and the Authentication procedure.

> - Check out my previous post on creating issues in ACC with ACC Connect
- [Autodesk Construction Cloud APIs](https://aps.autodesk.com/autodesk-construction-cloud-apis-integrations)
- [OpenAI Custom GPTs](https://openai.com/index/introducing-gpts/)

####<a name="5"></a> My Blue vs. Green

Unrelated to BIM and CAD, my kids used to argue with me about what is green and what is blue.
My perception differed from theirs.
So, I was interested to take
the [Is My Blue test](https://ismy.blue/):

> People have different names for the colors they see.
Language can affect how we memorize and name colors.
This is a color naming test designed to measure your personal blue-green boundary.

My personal result:
my boundary is at hue 169, greener than 80% of the population.
So, I tend to classify turquoise as blue.

<center>
  <img src="img/ismyblue.png" alt="Is my blue" title="Is my blue" width="500"/>
</center>
