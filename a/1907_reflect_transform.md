<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Quote of the Week: “There are only two kinds of programming languages: the ones people complain about and the ones nobody uses.” ― Bjarne Stroustrup, creator of the C++ programming language

- a careful analysis by rpthomnas to clarify the effects of rotation and reflection achieved by mirroring and flipping on BIM element transform
  GetTransform() does not include reflection into the transformation
  https://forums.autodesk.com/t5/revit-api-forum/gettransform-does-not-include-reflection-into-the-transformation/m-p/10334547
  GetTransform_ignores_mirror_1.png 1401
  GetTransform_ignores_mirror_2.png 216
  GetTransform_ignores_mirror_3.png 1144
  
- Document.MakeTransientElements
  https://forums.autodesk.com/t5/revit-api-forum/document-maketransientelements/m-p/10333812
  hacky and unsupported but fun to hack
  13903607 [Document.MakeTransientElements]
  https://forums.autodesk.com/t5/revit-api-forum/document-maketransientelements/m-p/7774471

- shared versus non-shared parameter creation
  Create Project Parameter (not shared parameter)
  https://forums.autodesk.com/t5/revit-api-forum/create-project-parameter-not-shared-parameter/m-p/10335503
  12125641 [Create Project Parameter(not shared parameter)]
  http://forums.autodesk.com/t5/revit-api/create-project-parameter-not-shared-parameter/m-p/5150182

- Design Automation for Revit 2022 now support exporting to PDF directly
  https://forge.autodesk.com/blog/design-automation-revit-2022-now-support-exporting-pdf-directly
  by Zhong Wu

twitter:

 #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Flip, Mirror and Transform

As so often in the past,
Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
provided numerous useful solutions and explanation in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160):

Before diving in, a nice quote of the week from Quincy Larson's [freecodecamp](https://www.freecodecamp.org) newsletter:

<blockquote>
<p><i>There are only two kinds of programming languages: the ones people complain about and the ones nobody uses.</i></p>
<p style="text-align: right; font-style: italic">&ndash; Bjarne Stroustrup, creator of the C++ programming language</p>
</blockquote>

####<a name="2"></a> Flip, Mirror and Transform

A careful analysis by Richard in the thread 
on [`GetTransform` does not include reflection into the transformation](https://forums.autodesk.com/t5/revit-api-forum/gettransform-does-not-include-reflection-into-the-transformation/m-p/10334547) clarifies
the effects of rotation and reflection achieved by mirroring and flipping on the BIM element transform:

**Question:** The 
[`Instance.GetTransform` method](https://www.revitapidocs.com/2015/50aa275d-031e-ce19-9cfd-18a7a341ed19.htm)
does not include reflection, e.g.,
the [family mirrored property](https://www.revitapidocs.com/2015/20ab2f32-e3ca-8173-aac3-a03e998fd0ab.htm) into
its transformation.
Below a family instance which is mirrored and outputs the equivalent `GetTansform` values:

<center>
<img src="img/GetTransform_ignores_mirror_1.png" alt="GetTransform ignores reflection" title="GetTransform ignores reflection" width="800"/> <!-- 1401 -->
</center>

Here is the python code:

<pre class="prettyprint">
import sys
import clr
clr.AddReference('ProtoGeometry')
from Autodesk.DesignScript.Geometry import *
data= UnwrapElement(IN[0])

output=[]
for i in data:
	output.append(i.Location.Point)
	output.append(i.GetTransform().BasisX)
	output.append(i.GetTransform().BasisY)
	output.append(i.GetTransform().BasisZ)
	output.append("")
OUT = output
</pre>

That is expected and intentional in Revit.

However, from a mathematical perspective, it should not be expected.
The [Wikipedia article on Transformation matrix](https://en.wikipedia.org/wiki/Transformation_matrix) shows
clearly that an element that is reflected around the `X` axis should have a different transformation matrix:

<center>
<img src="img/GetTransform_ignores_mirror_2.png" alt="Transformation with reflection" title="Transformation with reflection" width="216"/> <!-- 216 -->
</center>

Can you please share any explanation and why this intentional for Revit? 

**Answer:** I found results that indicate Revit uses a combination of reflection and rotation for the various mirror and flip operations:

<center>
<img src="img/GetTransform_ignores_mirror_3.png" alt="Flip and mirror" title="Flip and mirror" width="800"/> <!-- 1144 -->
</center>

One thing that stands out is the difference between horizontal double flip control and mirror command about same axis (noted red).
These operations are almost identical apart from the horizontal one that results in opposite facing and handed state.
Graphically, it appears the same, but not according to facing/handed orientation.

It has been noted previously that single flip control is more like rotating rather than mirroring (it doesn't result in reflected geometry).
We see by transform that it is reflected but facing/handed state is also set to true.

Generally, I think of the facing/handed state as being an internal to the family state, i.e., the internal geometry may be reflected but the family itself isn't (unless it is by transform).

You probably need to look at flip state/rotation and transform to get a definitive idea of the situation.
These controls long ago I believe were introduced for doors, which side they are hung and swing direction.
As they started being used for other things, the ambiguities crept in, i.e., double negative (same ultimate representation but two definitions for it).

Many thanks to Richard for the helpful explanation!

####<a name="3"></a> Transient Elements Hack

Back in 2018, the development team clearly stated that the
interesting-looking [`Document.MakeTransientElements` method](https://www.revitapidocs.com/2021.1/0decdddc-ae4a-d46d-d141-9d37e7973e05.htm)
is 'half-finished' work that should not have been exposed to the public API and will probably be removed again.

It has not been removed yet, though, and Moustafa Khalil and Richard discuss a hacky approach to make it do something at all in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [`Document.MakeTransientElements`](https://forums.autodesk.com/t5/revit-api-forum/document-maketransientelements/m-p/7774471),
in case you are interested in taking a further look yourself.

Just for the sake of completeness, some other officially supported approaches to display non-BIM-geometry include
AVF, DirectContext3D and, new in Revit 2022, see below, the [temporary incanvas graphics API](#4).

Here are some AVF samples:
 
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/12/%D0%B4%D0%BE%D0%BF%D0%BE%D0%BB%D0%BD%D0%B5%D0%BD%D0%B8%D1%8F-%D0%B4%D0%BB%D1%8F-%D1%80%D0%B5%D0%B2%D0%B8%D1%82-add-ins-for-revit.html">Russian Add-ins for Revit</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/03/rvtfader-avf-ray-tracing-and-signal-attenuation.html">RvtFader, AVF, Ray Tracing and Signal Attenuation</a></li>
</ul>

I have not explored the direct context 3D functionality myself yet, but here are some bits on that:
 
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/03/revitlookup-enhancements-future-revit-and-other-api-news.html">RevitLookup and DevDays Online API News</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html">What's New in the Revit 2018 API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/05/revit-2017-and-2018-sdk-samples.html">Revit 2017 and 2018 SDK Samples</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/01/transient-graphics-humane-ai-basic-income-and-lockdown.html">Transient Graphics, Humane AI, BI and Lockdown</a></li>
</ul>

####<a name="4"></a> Temporary InCanvas Graphics API Video

Bobby the [3rd Dimension Developer](https://www.youtube.com/channel/UCPCZ59KhJ4XrdkHgzmhZXKA/about) shared a video tutorial on
making use of
the new Revit 2022 [temporary in-canvas graphics](https://thebuildingcoder.typepad.com/blog/2021/04/whats-new-in-the-revit-2022-api.html#4.2.8.1) functionality in his thread
on [temporary incanvas graphics image colors](https://forums.autodesk.com/t5/revit-api-forum/temporary-incanvas-graphics-image-colors/m-p/10318210)

> I created a [video on the awesome new Temporary InCanvas Graphics API](https://youtu.be/ekLz54hLcHc).
That raises a question on the image colours.
As you can see, they are... off...
What are the rules the images, and colours, we can use in this feature?
...

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/ekLz54hLcHc" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

Unfortunately, the question on the colour mapping currently still remains unresolved.

####<a name="4"></a>

####<a name="5"></a> 

- Document.MakeTransientElements
  https://forums.autodesk.com/t5/revit-api-forum/document-maketransientelements/m-p/10333812
  hacky and unsupported but fun to hack
  
- shared versus non-shared parameter creation
  Create Project Parameter (not shared parameter)
  https://forums.autodesk.com/t5/revit-api-forum/create-project-parameter-not-shared-parameter/m-p/10335503
  12125641 [Create Project Parameter(not shared parameter)]
  http://forums.autodesk.com/t5/revit-api/create-project-parameter-not-shared-parameter/m-p/5150182

- Design Automation for Revit 2022 now support exporting to PDF directly
  https://forge.autodesk.com/blog/design-automation-revit-2022-now-support-exporting-pdf-directly
  by Zhong Wu

<pre class="code">

</pre>

Thank you, , for pointing this out!

