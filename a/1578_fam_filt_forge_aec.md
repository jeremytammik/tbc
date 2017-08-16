<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- publish /a/doc/forge/ recording for aec jim q

- devcon promo
  DevCon promo toolbox has slides for presentations or flyers for meet-ups - Please use:
  https://forge.autodesk.com/devcon-promo-tools
  https://autodesk.app.box.com/folder/34726869489
  You're our awesome dev community influencers - please retweet, share and like AutodeskForge Tweets about DevCon (and more;-) (edited)
  Other resources you might find helpful for Dev evangelism:
  For info about Forge and what the APIs offer -
  https://autodesk.box.com/s/khqe1tohno1fzhveegoziynkmpmh4lhu
  Community Success stories & use cases to see what people are doing with Forge -
  https://autodesk.box.com/s/wt2nica2rwfst47hkt80szdvyrjk3ptd
  Forge OTS page for all other Forge info and resources -
  https://autodesksales.gosavo.com/CustomPage/View.aspx?id=39136343&srlid=51521699&srisprm=False&sritidx=1&srpgidx=0&srpgsz=2

- How to pull the starting view for document using the Revit API
  https://stackoverflow.com/questions/45696372/how-to-pull-the-starting-view-for-document-using-the-revit-api

- Moving Detail Group only moves its LocationPoint, not detail items inside
  Moving items inside Detail Group
  https://forums.autodesk.com/t5/revit-api-forum/moving-detail-group-only-moves-its-locationpoint-not-detail/m-p/7302496
  
- 13269724 [Family Instance Filter]
  https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.134.2
  https://forums.autodesk.com/t5/revit-api-forum/family-instance-filter/m-p/7287113

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

&ndash; ...

--->

### Forge for AEC, DevCon, Revit versus Forge, Ids and Add-In Installation



####<a name="2"></a>Forge for AEC Demos and Sample Code

Last week, I briefly discussed some pros and cons
of [Revit versus Forge for BIM programming](http://thebuildingcoder.typepad.com/blog/2017/08/revit-versus-forge-ids-and-add-in-installation.html#2).

Since then, im Quanci, Senior Director Software Partner Development at Autodesk,
published [Autodesk Forge - what is it?](https://youtu.be/5xVwvBzemkg), a 45 minute video recording showing what's possible to do
with [Forge](https://forge.autodesk.com) through a set of demos and live code samples:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/5xVwvBzemkg?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

If this spiked your interest, you will certainly benefit from the [Forge developer conference DevCon](https://forge.autodesk.com/devcon-2017) taking place just before Autodesk University in Las vegas November 13-14.


####<a name="3"></a>Determining the Starting View

[Skeletank](https://stackoverflow.com/users/180529/skeletank) shared
an interesting and illuminating solution on StackOverflow 
showing [how to pull the starting view for document using the Revit API](https://stackoverflow.com/questions/45696372/how-to-pull-the-starting-view-for-document-using-the-revit-api):

**Question:** How can I use the Revit API to get the Starting View for a Document? The equivalent way to access it using the user interface is seen below:

<center>
<img src="img/starting_view_ui.png" alt="Starting view" width="651"/>
</center>

**Answer:** I used
the [RevitLookup tool](https://github.com/jeremytammik/RevitLookup) and
browsed through the database to find a class called `StartingViewSettings` with the property `ViewId` that returns the `ElementId` of the starting view:

<center>
<img src="img/starting_view_snoop.png" alt="Starting view" width="804"/>
</center>

Here is my actual code for getting the view:

<pre pre class="code">
  FilteredElementCollector a
    = new FilteredElementCollector( doc )
      .OfClass( typeof( StartingViewSettings ) );

  View startingView = null;
  
  foreach( StartingViewSettings settings in a )
  {
    startingView = doc.GetElement(
      settings.ViewId ) as View;
  }
</pre>

Many thanks to skeletank for researching and sharing this, and describing the effective approach to do so!


####<a name="4"></a>Detect Installed Revit Version

Another issue was raised by my consulting colleague Miro Schonauer:

**Question:** What is the best way to check if any version and, if so, what version of Revit is installed?

I checked Windows registry keys mentioned in the discussion on 
the [perpetual GUID algorithm and Revit 2014 product GUIDs](http://thebuildingcoder.typepad.com/blog/2013/04/perpetual-guid-algorithm-and-revit-2014-product-guids.html), 
but that is only up to Revit 2014 and still not sure we can detect 'any version'.

**Answer:** One choice it to use `RevitProductUtility` API provided by the `RevitAddInUtility.dll` assembly:

<pre pre class="code">
            IList < RevitProduct > allInstalledRevitProducts  = RevitProductUtility.GetAllInstalledRevitProducts();
            foreach (RevitProduct revitProduct in allInstalledRevitProducts)
            {
                if (revitProduct.Version == RevitVersion.Revit2015)
                {
                    // Revit 2015 installed.
                } 
                else if (revitProduct.Version == RevitVersion.Revit2017)
                {
                    // Revit 2017 installed.
                }
         }
</pre>


####<a name="5"></a>Detect Installed C3D Version

Another query from Miro, not on Revit, but such a closely related topic, so let's mention it as well:

**Question:** I would like to issue a warning if there is no C3D installed on the machine.

What is the best way to verify this?

Can I simply check if a certain registry entry exists?

I thought of using:

- C3D 2015
    - HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\AutoCAD\R20.0\ACAD-E000:409 &ndash; English
    - HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\AutoCAD\R20.0\InstalledProducts\C3D &ndash; any locale
- C3D 2015
    - HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\AutoCAD\R21.0\ACAD-0000:409
    - HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\AutoCAD\R21.0\InstalledProducts\C3D

**Answer:** It’s better to use these:

- HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\AutoCAD\R20.0\InstalledProducts\C3D
- HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\AutoCAD\R21.0\InstalledProducts\C3D

Many thanks to Miro for raising and clarifying this!


####<a name="6"></a>Moving Items Inside a Detail Group

Fair59 solved another issue in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread 
on [moving detail group only moves its locationpoint, not detail items inside](https://forums.autodesk.com/t5/revit-api-forum/moving-detail-group-only-moves-its-locationpoint-not-detail/m-p/7302496):

**Question:** 

**Answer:**

####<a name="7"></a>Filter for Family Instances and Types by Family Name

Yet another issue in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on implermenting
a [family instance filter](https://forums.autodesk.com/t5/revit-api-forum/family-instance-filter/m-p/7287113) led
to a little update and
new [release 2018.0.134.2](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.134.2)
of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples):

**Question:** 

**Answer:**
