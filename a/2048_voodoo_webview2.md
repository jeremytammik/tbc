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
</head>

<!---



twitter:

 @AutodeskRevit #RevitAPI #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Webview2 Plans and Voodoo for Links and Floors



<center>
<img src="img/.png" alt="" title="" width="100"/>
</center>



####<a name="2"></a> stable representation voodoo for ceiling in linked file:

stable representation voodoo for ceiling in linked file:
FAIR59
https://forums.autodesk.com/t5/revit-api-forum/dimension-on-hatch-pattern-slab/td-p/7063302
% lb dimension-on-hatch-pattern-slab
1565_dim_hatch.md:2
lots of interest in using the stable representartion voodoo championed by Fair59 et al for obtaining references to hatch pattern lines fone slabs.
chuog ho added a solutiuon to switch from the original solution for ceilings to floors:
Recenlly, I'm tried find the pick first point intersect of pattern and then suscess, but when you change between Floor and Ceiling, you need to change the number step of pattern:
Floor :

<pre><code class="language-cs">int index = 2 + (ip * _gridCount * 2);
int index = 3 + (ip * _gridCount * 2);

Ceiling :

<pre><code class="language-cs">int index = 1 + (ip * _gridCount * 2);
int index = 2 + (ip * _gridCount * 2);

Example to find the first intersect of pattern :

<pre><code class="language-cs">public XYZ? FindPlacePointPattern()
{
    using Autodesk.Revit.DB.Transaction tran = new Transaction(Doc, "Find Place Point Pattern");
    tran.Start();
    //check for model surfacepattern
    var hostObject = this.HostObjectPart.HostObject;
    Reference? top = HostObjectUtils.GetTopFaces(hostObject)
        .FirstOrDefault();
    PlanarFace? topFace = hostObject.GetGeometryObjectFromReference(
        top) as PlanarFace;
    Material mat = Doc.GetElement(
        topFace.MaterialElementId) as Material;
    ElementId foregroundPatternId = mat.SurfaceForegroundPatternId;
    FillPatternElement? patterntype = Doc.GetElement(
        foregroundPatternId) as FillPatternElement;
    FillPattern pattern = patterntype.GetFillPattern();
    if (pattern.IsSolidFill || pattern.Target == FillPatternTarget.Drafting) return null;

    // get number of gridLines in pattern
    int _gridCount = pattern.GridCount;
    //https://forums.autodesk.com/t5/revit-api-forum/dimension-on-hatch-pattern-slab/m-p/7078368#M22785
    // construct StableRepresentations and find the Reference to HatchLines
    string StableRef = top.ConvertToStableRepresentation(Doc);
    ReferenceArray _resArr = new ReferenceArray();
    for (int ip = 0; ip < 2; ip++)
    {
        int index = 2 + (ip * _gridCount * 2);
        string StableHatchString = StableRef + $"/{index}";
        var HatchRef = Reference.ParseFromStableRepresentation(Doc, StableHatchString);
        _resArr.Append(HatchRef);
    }

    // 2 or moreReferences => create dimension
    Dimension d1 = null;
    if (_resArr.Size > 1)
    {
        d1 = Doc.Create.NewDimension(Doc.ActiveView,
            Line.CreateBound(XYZ.Zero, new XYZ(1, 0, 0)), _resArr);
        // move dimension a tiny amount to orient the dimension perpendicular to the hatchlines
        // I can't say why it works, but it does.
        ElementTransformUtils.MoveElement(Doc, d1.Id, new XYZ(.01, 0, 0));
    }

    _resArr.Clear();
    for (int ip = 0; ip < 2; ip++)
    {
        int index = 3 + (ip * _gridCount * 2);
        string StableHatchString = StableRef + $"/{index}";
        var HatchRef = Reference.ParseFromStableRepresentation(Doc, StableHatchString);
        _resArr.Append(HatchRef);
    }

    // 2 or more References => create dimension
    Dimension? d2 = null;
    if (_resArr.Size > 1)
    {
        d2 = Doc.Create.NewDimension(Doc.ActiveView,
            Line.CreateBound(XYZ.Zero, new XYZ(0, 1, 0)), _resArr);
        // move dimension a tiny amount to orient the dimension perpendicular to the hatchlines
        // I can't say why it works, but it does.
        ElementTransformUtils.MoveElement(Doc, d2.Id, new XYZ(0, .01, 0));
    }
    // create dimension between two dimensions


    // try get cross point of two dimensions
    if (d1 != null && d2 != null)
    {
        XYZ? intersection = FindIntersectByTwoDirection(d1, d2);
        return intersection;
    }

    tran.RollBack();
    return null;
}

jorge_morente raised to issue for linked files:
Many thanks to @Chuong.Ho  and especially to @FAIR59  for this very valuable information. I have used this code and it works perfectly for me. I managed to center elements in the ceiling grid when I have the ceiling and the element in the same model. What happens when the ceiling is in a linked model? I tried it and I have a problem.
What I do to get a point from the grid and from there 2 perpendicular lines is to get the origin of each dimension, add or subtract half the value of one of the grid cells, and I have the point positioned on the line. I create a line with the point and direction, and I have "the axes" of the grid. This worked perfectly in my initial case but not with the ceiling in a linked model.
/Users/jta/a/doc/revit/tbc/git/a/img/dim_hatch_linked_model_ceiling.png
As seen in the images, I have the 2 dimensions from which I can obtain any of the green lines and thus any of the 4 red points. But the point I get with the script is the origin of the pink lines. It is displaced and I don't know why.
At first, I thought it could be because the origin point of the linked model was offset, but no, I checked and it is at (0,0,0). I can't figure out what it could be. Any ideas???
UPDATE 1: In the previous example, I had ceilings in both the model and the linked model, and it seems I was mixing things up that I shouldn't have.
I've created a model from scratch where I only have one ceiling, and it's from a linked model, and I've detected the problem. The references we created by adding some indices at the end to create the dimensions are not valid because the references I need are of the type:
33899bce-a21c-4067-af38-2260a13fa4e9-00023f0b:0:RVTLINK:147216:1/181
The string StableRef = bottom.ConvertToStableRepresentation(linkedDocument); command is giving me the following value:
f6c5042c-3953-4e47-afb0-42e0e62b1e73-00023f10:1:SURFACE
Curiously, if I dimension the grid in the model where the ceiling is, the references are with the code f6c5042... which is the UniqueId of the ceiling. If I dimension it in the model with the linked ceiling, the dimension references are 33899bce.... It's as if Revit renames the references, and I have no way of obtaining them from the ceiling because they are not those.
Any ideas?
Fair59's answer is an image. Does it say more than a thousand words? It says enough, anyway:
/Users/jta/a/doc/revit/tbc/git/a/img/dim_hatch_linked_model_ceiling.png
dim_hatch_linked_model_ceiling.png
dim_hatch_linked_model_ceiling_stable_rep.png

####<a name="2"></a> Move from CefSharp to WebView2

The heads-up that we shared in the beginning of July
on [CefSharp versus WebView2 Embedded Browser](https://thebuildingcoder.typepad.com/blog/2024/07/material-assets-chromium-and-sorting-schedules.html#3) is
stabilisingto the extens that the development team has decided to announce a plan to migrate CefSharp to WebView2 in the next major release:

> Revit is removing all CefSharp binaries from its distribution package starting in the next major release.
Revit add-ins can keep using CefSharp as a standard 3rd party component.
To do so CefSharp, please ensure your add-in will deliver CefSharp binaries with your add-in.
However, it is recommended to use WebView2 as an alternative to avoid potential issues from different CefSharp release versions in one Revit session.

####<a name="2"></a> CrowdStrike Outage

If you are interested in some technical background details on the recent CrowdStrike outage, two informative videos by Dave's Garage explain what happened quite well and reinforce the importance of input validation:

- [CrowdStrike IT outage](https://youtu.be/wAzEJxOo1ts)
- [CrowdStrike update latest news, lessons learned](https://youtu.be/ZHrayP-Y71Q)

####<a name="2"></a> Ai Models Trained on AI-Generated Data Collapse

What we all intuitively knew has now been scientifically corroborated &ndash;
[AI models collapse when trained on recursively generated data](https://www.nature.com/articles/s41586-024-07566-y):

> generative artificial intelligence (AI) such as large language models (LLMs) is here to stay and will substantially change the ecosystem of online text and images ... consider what may happen to GPT-{n} once LLMs contribute much of the text ... indiscriminate use of model-generated content in training causes irreversible defects in the resulting models, in which tails of the original content distribution disappear. We refer to this effect as ‘model collapse’ and show that it can occur in LLMs as well as in variational autoencoders (VAEs) and Gaussian mixture models (GMMs)...

####<a name="2"></a> Open Source AI Is the Path Forward

Mark Zuckerberg, founder and CEO of Meta, reiterates and clarifies their vision
that [open-source AI is the path forward](https://about.fb.com/news/2024/07/open-source-ai-is-the-path-forward/).

