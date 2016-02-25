<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

11383242 [Get Model Elements]
11378249 [All model elements in project]
http://forums.autodesk.com/t5/revit-api/all-model-elements-in-project/m-p/5972035

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge

Revit API, Jeremy Tammik, akn_include

Retrieving All Model Elements #revitapi #bim #aec #3dwebcoder #adsk @AutodeskReCap @Adsk3dsMax @AutodeskRevit

Let's revisit the ever-recurring topic of <b>retrieving all model elements</b>, and also look at a well-documented workflow for retrofitting historical buildings, the renovation of the Milano Teatro Lirico opera house...

-->

### Retrieving All Model Elements

Let's revisit the ever-recurring topic of [retrieving all model elements](#3), and also look at a
well-documented [workflow for retrofitting historical buildings](#2).

<center>
<img src="/p/2015/2015-12-24_binnningen/499_cropped_600x270.jpg" alt="Red sky at night..." width="600">
</center>


#### <a name="2"></a>Workflow for Retrofitting Historical Buildings

If you are interested in the detailed description of a workflow for retrofitting historical buildings,
the [renovation of the Milano Teatro Lirico opera house](http://forums.autodesk.com/t5/reality-computing/renovating-milan-s-teatro-lirico-opera-house/ba-p/5976733) may
provide a worthwhile read.

The almost 10,000 square meter, 1500-seat theatre renovation weighs in at over 16 million euros in cost.

A technical team using a Leica laser scanner captured the detailed theatre geometry, producing 550 scans with 6.5 billion points.

The point cloud data was processed to generate an accurate 3D virtual model of the theatre in its current state using:

- [ReCap](https://recap.autodesk.com) for point cloud management
- [Revit](http://www.autodesk.com/products/revit-family/overview) for the creation of the 3D existing conditions model and the development/assessment of design alternatives
- [3ds Max](http://www.autodesk.com/products/3ds-max/overview) to generate photorealistic project visualizations

The whole process is extensively documented in the Autodesk University class [RC9940 handout document](http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2015/recap/rc9940) and
the 6-minute video on [Teatro Lirico](https://www.youtube.com/watch?v=2hSZbkB00q8):

<center>
<iframe width="400" height="225" src="https://www.youtube.com/embed/2hSZbkB00q8?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

You need a login and password to download the AU material.
You get those for free by registering at the site.
It is a very useful resource to explore and learn more about all Autodesk products and topics related to the industries and workflows using them.


#### <a name="3"></a>Retrieving All Model Elements

'All model elements' means something different to every application, of course, so you will always need to adapt the details to your own needs.

This topic was prompted by several recent cases and the Revit API discussion forum thread
on [all model elements in project](http://forums.autodesk.com/t5/revit-api/all-model-elements-in-project/m-p/5972035):

**Question:** How can I get the model elements?
This old code from The Building Coder discussion
on [selecting model elements](http://thebuildingcoder.typepad.com/blog/2009/05/selecting-model-elements.html) does not work anymore.
Thank you in advance.

**Answer:** Yes, indeed, the code that you are pointing to is pretty old.

However, as pointed out in that post, the code is part of the ADN Revit API training material, in the Revit API Introduction Lab 2-2.

That material is continuously maintained, and an up-to-date version is available from two separate GitHub repositories:

- [Autodesk Revit Training material](https://github.com/ADN-DevTech/RevitTrainingMaterial), the official training material pointed to from the [Revit Developer Centre](http://www.autodesk.com/developrevit)
- [ADN Revit API Training Labs including Xtra](https://github.com/jeremytammik/AdnRevitApiLabsXtra), an enhanced version maintained by my humble self

You could grab the current version from there, if you like, e.g., directly
from [XtraCs/Labs2.cs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs2.cs#L506-L686).

Furthermore, luckily for you, a number of different other approaches to solve this very issue have been presented by The Building Coder in the topic group on [filtering for all elements](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.9).

I just updated the links under 'Selecting Model Elements or Visible 3D Elements' for you, listing the following possibilities:

- [Valid category and non-empty geometry](http://thebuildingcoder.typepad.com/blog/2009/05/selecting-model-elements.html)
- [Ditto, and eliminate certain categories](http://thebuildingcoder.typepad.com/blog/2009/11/select-model-elements-2.html)
- [Valid category and neither category nor element is hidden](http://thebuildingcoder.typepad.com/blog/2009/11/visible-elements.html)
- [Based on the Category.HasMaterialQuantities property](http://thebuildingcoder.typepad.com/blog/2010/10/selecting-model-elements.html)
- [Based on the elements visible in a default 3D view](http://thebuildingcoder.typepad.com/blog/2010/10/model-elements-revisited.html)
- [Using a custom exporter](http://thebuildingcoder.typepad.com/blog/2013/08/determining-absolutely-all-visible-elements.html)

I hope this helps.

