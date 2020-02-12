<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- in 15056740 [Adding Project Template to 'New Project' via API]
https://forums.autodesk.com/t5/revit-api-forum/adding-project-template-to-new-project-via-api/m-p/8585348
Peter @cig_ad Ciganek just discovered:
> Autodesk.Revit.ApplicationServices.Application.CurrentUsersDataFolderPath returns the path where Revit.ini is located.

- in [localization website](https://forums.autodesk.com/t5/revit-api-forum/localization-website/m-p/8500166),
Susan Renna pointed out the new location for the Autodesk NeXLT localization website:
> You can find it here:  https://ls-wst.autodesk.com/app/nexlt-plus/app/home/search

- Torsion Tools Update #2 - Copy Linked Legends, Schedules and Reference Views
  https://youtu.be/C2dBRqXl9UA
  <iframe width="560" height="315" src="https://www.youtube.com/embed/C2dBRqXl9UA" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
  GitHub: https://github.com/TorsionTools/R20
  Autodesk Revit 2020 API Visual Studio Solution Template with Code Examples for Common Tools
  Update #2 will walk through the added tools to Copy Legends, Schedules, and Reference Views  from a Linked Model. 
  The Copy Legends and Schedules tools allow you to select the Link to copy them from, and then select the Legends or Schedules you would like to copy. It will check to see if they already exist in the project and prompt you if they do. 
  The Linked Views tool will allow you to select a Linked Model, Title Block, Viewport Type and then the Views you would like from the Linked Model. It will create a drafting view in the current Document with the Name, Detail Number and Sheet of the Linked Model.


twitter:

the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 


&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Ini File, Localisation and Torsion Tools Two




#### <a name="2"></a>Torsion Tools Two

Continuing right on from the
fruitful [collection of videos and tutorials](https://thebuildingcoder.typepad.com/blog/2020/02/search-for-getting-started-tutorials-and-videos.html) presented
yesterday, an update to
the [Torsion Tools add-in template with code examples for common tools](https://thebuildingcoder.typepad.com/blog/2020/01/torsion-tools-command-event-and-info-in-da4r.html) introduced last week:

[Torsion Tools Update #2 &ndash; Copy Linked Legends, Schedules and Reference Views](https://youtu.be/C2dBRqXl9UA):

> Autodesk Revit 2020 API Visual Studio Solution Template with Code Examples for Common Tools.
Update #2 walks through the added tools to copy legends, schedules, and reference views from a linked model.

> The Copy Legends and Schedules tools allow you to select the Link to copy them from, then select the Legends or Schedules you would like to copy.
It will check whether they already exist in the project and prompt you if they do.

> The Linked Views tool allows you to select a linked model, title block, viewport type and then the views you would like from the linked model.
It creates a drafting view in the current document with the name, detail number and sheet of the linked model.

> [TorsionTools GitHub repo](https://github.com/TorsionTools/R20)

<center>
<iframe width="560" height="315" src="https://www.youtube.com/embed/C2dBRqXl9UA" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

Many thanks to Torsion Tools for creating and sharing this powerful resource!

#### <a name="3"></a>Retrieve the Path to Revit.ini

A small note from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [adding project template to 'New Project' via API](https://forums.autodesk.com/t5/revit-api-forum/adding-project-template-to-new-project-via-api/m-p/8585348) by 
Peter @cig_ad Ciganek:

> *Autodesk.Revit.ApplicationServices.Application.CurrentUsersDataFolderPath* returns the path where Revit.ini is located.

#### <a name="4"></a>Updated NeXLT Localization Website

In another small not from 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on the [localization website](https://forums.autodesk.com/t5/revit-api-forum/localization-website/m-p/8500166),
Susan Renna points out the new Autodesk NeXLT localization website URL:

> You can find it here: [ls-wst.autodesk.com/app/nexlt-plus/app/home/search](https://ls-wst.autodesk.com/app/nexlt-plus/app/home/search)




**Question:** 

**Answer:** 


<pre class="code">
</pre>


<center>
<img src="img/" alt="" title="" width="100"/>
</center>


