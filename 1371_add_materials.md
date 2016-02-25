<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

#dotnet #csharp #geometry
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon #revitapi #3dwebcoder
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#au2015 #rtceur
#adskdevnetwrk

Revit API, Jeremy Tammik, akn_include

Fill Pattern Viewer Fix and Add Materials 2016 #revitapi #bim #aec #3dwebcoder #au2015 #rtceur @kfpopeye

@kfpopeye discovered and fixed an issue with complex fill patterns in the venerable old WPF Fill Pattern Viewer Control, part of the AddMaterials Revit add-in to load new materials into a project based on a list defined in an Excel spreadsheet
&ndash; Original implementation for Revit 2011
&ndash; Reimplementation for Revit 2014
&ndash; Improved error messages and reporting
&ndash; WPF FillPattern viewer control
&ndash; Check for already loaded materials
&ndash; FillPattern viewer benchmarking...

-->

### Fill Pattern Viewer Fix and Add Materials 2016

[@kfpopeye](https://github.com/kfpopeye) discovered and fixed an issue with complex fill patterns in the venerable
old [WPF Fill Pattern Viewer Control](http://thebuildingcoder.typepad.com/blog/2014/04/wpf-fill-pattern-viewer-control.html) by
Victor Chekalin and Alexander Ignatovich.

The fill pattern viewer control is part of
the [AddMaterials](https://github.com/jeremytammik/AddMaterials) Revit add-in
to load new materials into a project based on a list defined in an Excel spreadsheet:

- [Original implementation for Revit 2011](http://thebuildingcoder.typepad.com/blog/2010/08/add-new-materials-from-list.html#2)
- [Reimplementation for Revit 2014](http://thebuildingcoder.typepad.com/blog/2014/03/adding-new-materials-from-list-updated.html)
- [Improved error messages and reporting](http://thebuildingcoder.typepad.com/blog/2014/03/adding-new-materials-from-list-updated-again.html)
- [WPF FillPattern viewer control](http://thebuildingcoder.typepad.com/blog/2014/04/wpf-fill-pattern-viewer-control.html)
- [Check for already loaded materials](http://thebuildingcoder.typepad.com/blog/2014/04/getting-serious-adding-new-materials-from-list.html)
- [FillPattern viewer benchmarking](http://thebuildingcoder.typepad.com/blog/2014/04/wpf-fill-pattern-viewer-control-benchmark.html)

Says kfpopeye:

> I noticed the original didn't handle complex fill patterns properly. I made some enhancements to fix this.

> Here is a [link to download a Revit project that shows what I'm referring to](https://app.box.com/s/km97p85f67g8da89ps2jihbey9d3k8ji).

> It contains a macro that displays the new viewer next to the old one. You'll notice the "Wood #" patterns didn't display correctly in the old viewer.

> The old viewer would sometimes draw the fill grids off the bitmap when the matrix was shifted too far or sometimes wouldn't fill the bitmap because it only repeated the line draw so may times. I made the viewer "smarter" by checking the shift versus the initial offset amount and checking to see if the line still intersects the bitmap.

<center>
<img src="img/fill_pattern_old_new.png" alt="Old versus new fill pattern viewer">
</center>

I am providing kfpopeye's sample model with the macro definition here as well, in <a href="zip/fill_pattern_viewer.rvt">fill_pattern_viewer.rvt</a>, in case the original download link goes away.

After some struggles back and forth we got the new viewer implementation integrated and running in the AddMaterials add-in.

I also took this opportunity to migrate it to Revit 2016:

- [Updated fill pattern viewer for Revit 2015](https://github.com/jeremytammik/AddMaterials/releases/tag/2015.0.0.5)
- [AddMaterials migrated to Revit 2016](https://github.com/jeremytammik/AddMaterials/releases/tag/2016.0.0.0)

You can always grab the most up-to-date version from
the [AddMaterials GitHub repository](https://github.com/jeremytammik/AddMaterials) master branch.

Many thanks to kfpopeye for this enhancement!
