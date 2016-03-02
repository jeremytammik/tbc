<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- http://forums.autodesk.com/t5/revit-api/boundingboxintersectsfilter-not-working/td-p/5912235

- Basic access to Revit API and SDK, what is what? -- http://forums.autodesk.com/t5/revit-api/revit-api-online/m-p/6061199


#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewAPI #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restAPI
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge
@AutodeskReCap @Adsk3dsMax
#revitAPI #bim #aec #3dwebcoder #adsk #adskdevnetwrk @jimquanci @keanw
#au2015 #rtceur
#eraofconnection
#RMS @researchdigisus
@adskForge #3dwebaccel
#a360
 @github

Revit API, Jeremy Tammik, akn_include

API, SDK and View Section Box Element Intersection #revitAPI #3dwebcoder @AutodeskRevit #bim #aec #adsk #adskdevnetwrk

Trying to keep track of the overwhelming information flow provided by the numerous discussions on
the Revit API discussion forum is impossible.
Here are two little titbits from the past few days
&ndash; Basic Revit API and SDK Access, Online and Offline
&ndash; Filtering for Elements Intersecting a View Section Box...

-->

### API, SDK and View Section Box Element Intersection

Trying to keep track of the overwhelming information flow provided by the numerous discussions on
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) is impossible.

Here are two little titbits from the past few days:

- [Basic Revit API and SDK Access, Online and Offline](#2)
- [Filtering for Elements Intersecting a View Section Box](#3)


#### <a name="2"></a>Basic Revit API and SDK Access, Online and Offline

Here is a very basic question, raised by Miguel Rus in the Revit API discussion forum thread
on [Revit API online](http://forums.autodesk.com/t5/revit-api/revit-api-online/m-p/6061199) that
I reproduce here for the sake of clarity and future reference.

It belongs to the category of things to read and understand even before getting started &nbsp; :-)

**Question:** I am trying to find an the online API for Revit 2016 but I can't seem to finds it.
Could someone point me in the right direction.
The offline CHM is not of any help as the firewall at my office is blocking the connection to the Internet.

**Answer:** The Revit API, the application developer interface, is always included with Revit itself.

As far as I know, the end user product cannot run without the API installed. The API is provided by certain .NET assemblies located in the same folder as Revit.exe itself, such as `RevitAPI.dll` and `RevitAPIUI.dll`.

I guess what you mean is the Revit SDK, the software developers kit.

It contains important and helpful API documentation and samples, such as the Revit API help file `RevitAPI.chm` that you refer to.

The SDK is installed by running an executable file, `RevitSDK.exe`.

You can install the SDK from various places. It is included under 'Tools' in the standard Revit product installation.

It is also available from the main entry point for information on and getting started with the Revit API,
the [Revit Developer Centre](http://www.autodesk.com/developrevit).

The Building Coder provides more hints
on [getting started with the Revit API](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2).

If you are asking specifically about <u>Revit API help online</u>, then The Building Coder discussion
about [Revit API help online](http://thebuildingcoder.typepad.com/blog/2014/01/revit-api-help-online-and-hiking-on-la-palma.html) might
possibly help.

:-)

Unfortunately, the version pointed to there is for Revit 2014 and has not been updated: [www.revitapisearch.com](http://www.revitapisearch.com).

I would recommend simply using the CHM provided by the SDK; you can rely on that, always, store it locally, always keep it at hand, whether online or off...


#### <a name="3"></a>Filtering for Elements Intersecting a View Section Box

Here is a useful and important piece of information on determining all elements intersecting a view's section box, shared by Jan Tepelmann
at [Inreal Technologies](https://www.inreal-tech.com) in the Revit API discussion forum thread
on [BoundingBoxIntersectsFilter not working](http://forums.autodesk.com/t5/revit-api/boundingboxintersectsfilter-not-working/td-p/5912235):

**Question:** I'm a programmer at Inreal Technologies and were developing Enscape 3D, a rendering plugin for Revit.

Currently we are working on speeding up the switching between two 3D views. Right now we just recreate the scene to render but this is obviously very slow.

So I came up with the following code to find out which elements were added, removed and modified when you switch from one view (the start view) to another view (the target view). The idea behind the code is, that we want to know all elements which got cut by the start view's section box or will get cut by the target view section box. For those elements the geometry has to be updated:

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">static</span> DiffUpdateElementIds Calculate(
&nbsp; &nbsp; <span class="teal">View3D</span> startView, <span class="teal">View3D</span> targetView )
&nbsp; {
&nbsp; &nbsp; <span class="blue">var</span> diffElements = <span class="blue">new</span> DiffUpdateElementIds();
&nbsp;
&nbsp; &nbsp; <span class="green">//... here added and removed elements are calculated </span>
&nbsp; &nbsp; <span class="green">// (with exclude filter). This works just fine </span>
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( startView.IsSectionBoxActive
&nbsp; &nbsp; &nbsp; || targetView.IsSectionBoxActive )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">ElementFilter</span> intersectFilterStart = <span class="blue">null</span>;
&nbsp; &nbsp; &nbsp; <span class="teal">ElementFilter</span> intersectFilterTarget = <span class="blue">null</span>;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( startView.IsSectionBoxActive )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; Logger.info( <span class="maroon">&quot;start View has SectionBox&quot;</span> );
&nbsp; &nbsp; &nbsp; &nbsp; intersectFilterStart
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">BoundingBoxIntersectsFilter</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">Outline</span>( startView.GetSectionBox().Min,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; startView.GetSectionBox().Max ) );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( targetView.IsSectionBoxActive )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; Logger.info( <span class="maroon">&quot;target View has SectionBox&quot;</span> );
&nbsp; &nbsp; &nbsp; &nbsp; intersectFilterTarget
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">BoundingBoxIntersectsFilter</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> <span class="teal">Outline</span>( targetView.GetSectionBox().Min,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; targetView.GetSectionBox().Max ) );
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">ElementFilter</span> intersectFilter;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( intersectFilterStart != <span class="blue">null</span>
&nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; intersectFilterTarget == <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; intersectFilter = intersectFilterStart;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span> <span class="blue">if</span>( intersectFilterStart == <span class="blue">null</span>
&nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; intersectFilterTarget != <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; intersectFilter = intersectFilterTarget;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; intersectFilter = <span class="blue">new</span> <span class="teal">LogicalOrFilter</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; intersectFilterStart, intersectFilterTarget );
&nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">var</span> cutElementsStartView
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; startView.Document, startView.Id )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .WherePasses( intersectFilter );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">var</span> cutElementsTargetView
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; targetView.Document, targetView.Id )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .WherePasses( intersectFilter );
&nbsp;
&nbsp; &nbsp; &nbsp; diffElements.ModifiedElements
&nbsp; &nbsp; &nbsp; &nbsp; = cutElementsStartView.UnionWith(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cutElementsTargetView ).ToElementIds();
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> diffElements;
&nbsp; }
</pre>

The problem now is that it misses a ceiling in the Advanced Sample Project when I switch from the default 3D view to "03 - Floor Public - Day Rendering" view, cf. this screen snapshot:

<center>
<img src="img/ceiling_not_cut.png" alt="Ceiling not cut" width="600">
</center>

What am I missing? I have to say that I'm relatively new to the filtering API so any help is appreciated.

**Answer:** I could solve the problem now by myself.

Maybe it’s useful for someone else who has the same problem:

<pre class="code">
&nbsp; <span class="blue">if</span>( startView.IsSectionBoxActive )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Transform</span> t = startView.GetSectionBox().Transform;
&nbsp;
&nbsp; &nbsp; <span class="teal">Outline</span> o = <span class="blue">new</span> <span class="teal">Outline</span>(
&nbsp; &nbsp; &nbsp; t.OfPoint( startView.GetSectionBox().Min ),
&nbsp; &nbsp; &nbsp; t.OfPoint( startView.GetSectionBox().Max ) );
&nbsp;
&nbsp; &nbsp; intersectFilterStart
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">BoundingBoxIntersectsFilter</span>( o );
&nbsp; }
&nbsp; <span class="blue">if</span>( targetView.IsSectionBoxActive )
&nbsp; {
&nbsp; &nbsp; <span class="teal">Transform</span> t = targetView.GetSectionBox().Transform;
&nbsp;
&nbsp; &nbsp; <span class="teal">Outline</span> o = <span class="blue">new</span> <span class="teal">Outline</span>(
&nbsp; &nbsp; &nbsp; t.OfPoint( targetView.GetSectionBox().Min ),
&nbsp; &nbsp; &nbsp; t.OfPoint( targetView.GetSectionBox().Max ) );
&nbsp;
&nbsp; &nbsp; intersectFilterTarget
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">BoundingBoxIntersectsFilter</span>( o );
&nbsp; }
</pre>

The code I was missing is to apply the view section box transform. I was just missing that the section box can have a transformation. I thought the box is always defined in world space, but that's not true. Sometimes it is not and then the old code did not work. So hopefully this helps someone who has the same issue.

Now everything is working and the switch between two 3D views is <b><i>much</i></b> faster then before.

Great! &nbsp; :-)

Many thanks to Jan for sharing this!
