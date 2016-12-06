<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- AU classes posted
  https://www.linkedin.com/pulse/au2016-classes-posted-fernando-malard?trk=hb_ntf_MEGAPHONE_ARTICLE_POST
  https://www.linkedin.com/pulse/au2016-classes-posted-fernando-malard

- updated ADN Xtra

- shared param on material element - email with orsi Re: API

- connect desktop and cloud recording from AU
  http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2016/revit/sd20908#chapter=0
  http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2016/forge/sd16837#
  http://player.ooyala.com/iframe.js#pbid=34ee2c04f0304af88c76585f87c5bd8c&ec=xyM2hvNzE6uwSdozxVspBc0eDumVZA5F

- Cornelius story: cured by natural remedies
  http://on-lyme.org/en/sufferers/lyme-stories/item/241-cornelius-story-cured-by-natural-remedies

Material Shared Parameters and ADN Xtra Labs #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

How to attach a shared parameter to Material elements? 
&ndash; Updated ADN Xtra labs to remove all deprecated API usage
&ndash; A new addin that creates and populates a shared parameter to provide material information for each element
&ndash; Autodesk University 2016 class materials have now been posted, including the recording of my session on connecting desktop and cloud
&ndash; Linking <code>Material</code> elements to external database entries
&ndash; Binding a shared parameter to elements
&ndash; Attaching a shared parameter to <code>Material</code> elements
&ndash; Create and populate a shared parameter <code>ForgeMaterial</code>
&ndash; Cornelius story &ndash; cured by natural remedies...

-->

### Material Shared Parameters and ADN Xtra Labs

Can we attach a shared parameter to `Material` elements?

Yes, we can!

In the course of proving that, I finally updated the ADN Xtra labs to remove all deprecated API usage.

I also implemented a new add-in that creates and populates a shared parameter to provide material information for each element to the Forge translation process... in a way, the opposite of the above, if you will.

And, talking about materials, the Autodesk University 2016 class materials have now been posted, including the recording of my session on connecting desktop and cloud.

Finally, I end with an absolutely non-technical topic, on healing:

- [AU2016 Classes posted](#2)
- [ADN Xtra labs updated](#3)
- [Linking `Material` elements to external database entries](#4)
- [Binding a shared parameter to elements](#5)
- [Attaching a shared parameter to `Material` elements](#6)
- [Create and populate a shared parameter `ForgeMaterial`](#7)
- [Cornelius story &ndash; cured by natural remedies](#8)

Before getting to all that, let me mention that I am now busy at the Munich DevDays conference.

<center>
<img src="img/2016-12-05_devday_munich_305_500x225.jpg" alt="Munich DevDay" width="500"/>
</center>

For more impressions from this event, check out Kean Walmsley's notes on [the European DevDay in Munich](http://through-the-interface.typepad.com/through_the_interface/2016/12/the-european-devday-in-munich.html).

####<a name="2"></a>AU2016 Classes Posted

All the recordings made of Autodesk University 2016 classes have now been posted and are available
via [AU Online](http://au.autodesk.com/au-online/overview).

You can explore the material
for [my two classes](http://thebuildingcoder.typepad.com/blog/2016/10/au-revit-20171-and-rex-freezedrawing.html#2)
by [filtering for `tammik`](http://au.autodesk.com/au-online/classes-on-demand/search?full-text=tammik),
or directly through the following links:

- [**SD20891** &ndash; Revit API Expert Roundtable &ndash; Open House on the Factory Floor](http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2016/revit/sd20891)
- [**SD20908** &ndash; Connect Desktop and Cloud &ndash; Free Your BIM Data!](http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2016/revit/sd20908)

<center>
<script height="468px" width="832px" src="http://player.ooyala.com/iframe.js#pbid=34ee2c04f0304af88c76585f87c5bd8c&ec=xyM2hvNzE6uwSdozxVspBc0eDumVZA5F"></script>
</center>

A related class that may also be of interest is
Fernando Malard's [**SD16837** *From Desktop to the Cloud with Autodesk Forge*](http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2016/forge/sd16837).

In case you have not yet enjoyed it, you might also want to check out
the great [AU2016 opening keynote](http://au.autodesk.com/au-online/classes-on-demand/class-catalog/2016/class-detail/22760).



####<a name="3"></a>ADN Xtra Labs Updated

The last time I worked on the *ADN Xtra Labs* hosted in
the [AdnRevitApiLabsXtra GitHub repository](https://github.com/jeremytammik/AdnRevitApiLabsXtra) was for
the [Revit 2017 migration](http://thebuildingcoder.typepad.com/blog/2016/08/trusted-signature-and-updated-revit-2017-api-resources.html#5),
and the [migration to Revit 2016](http://thebuildingcoder.typepad.com/blog/2015/06/adn-labs-xtra-multi-version-add-ins-and-cnc-direct.html#2) before that.

They are a superset of the
official [ADN Revit API labs training material]()
([2016](http://thebuildingcoder.typepad.com/blog/2015/05/autodesk-university-q1-adn-labs-and-wizard-update.html#4))
available from
the [Revit Developer Centre](http://www.autodesk.com/developrevit) and
the [Revit API Training GitHub repository](https://github.com/ADN-DevTech/RevitTrainingMaterial).

They are used for our standard two- or three-day hands-on Revit API introduction training courses.
They are also suitable for self-learning and include full step-by-step instructions, separate for both C# and VB add-ins.

I maintain an extended version of these including the precursor versions, repackaged in the ADN Revit API Training Labs Xtra modules.

The Xtra version Visual Studio solution implements the following projects:

<center>
<img src="img/AdnXtra_solution.png" alt="ADN Revit API Training Labs Xtra Visual Studio solution" width="210"/>
</center>

The first six are the official, standard, ADN training labs to introduce the Revit API basics, UI programming and Family API.

Obviously, the Xtra labs have advantages and disadvantages over the standard labs, so you should check out both and decide which you prefer for yourself.

They also include a couple of additional utilities that I frequently find useful, especially a simplified version of the
<a href="http://thebuildingcoder.typepad.com/blog/2015/05/geometry-creation-and-line-intersection-exceptions.html#5">BipChecker</a>
(<a href="http://thebuildingcoder.typepad.com/blog/2014/05/bipchecker-for-revit-2015-on-github.html#3">2015</a>) and the
<a href="http://thebuildingcoder.typepad.com/blog/2014/09/debugging-and-maintaining-the-image-relationship.html#2">element lister</a>.

Anyway, I left off the migration to the Revit 2017 API before it was entirely completed; several of the external commands were still making use of the obsolete and deprecated automatic transaction mode.

I finally got around to fixing that now, resulting in a
new [release 2017.0.0.7](https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2017.0.0.7).

You can examine the changes I made
by comparing the last few versions using the GitHub comparison functionality, e.g.,
[between 2017.0.0.4 and 2017.0.0.7](https://github.com/jeremytammik/AdnRevitApiLabsXtra/compare/2017.0.0.3...2017.0.0.7).

Here is my external application RvtSamples listing entry points to launch all the Revit SDK, ADN Xtra lab and The Building Coder samples:

<center>
<img src="img/AdnXtra_2016.png" alt="RvtSamples listing SDK, Adn Xtra and The Building Coder samples" width="452"/>
</center>

As always, the most up-to-date version of the ADN Revit API Training Labs Xtra is provided in the
<a href="https://github.com/jeremytammik/AdnRevitApiLabsXtra">AdnRevitApiLabsXtra GitHub repository</a>,
and the current version right now is
<a href="https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2017.0.0.7">release 2017.0.0.7</a>.</p>



####<a name="4"></a>Linking `Material` Elements to External Database Entries

**Question:** Is it possible (maybe through API?) to assign new parameters for materials?

The original question in in German:

*Um die Bilanzierung der Materialmassen zu automatisieren, wäre es hilfreich den Materialien in Revit eine Datenbank-ID zuzuschreiben, welche dauerhaft im Modell mitwandert. Über shared Parameter konnte ich bisher nur Elemente (Wand), aber nicht den untergeordneten Materialien zuschreiben. Die Funktionenen des Materialkatalogs wirken eingeschränkt. Gibt es hierfür Möglichkeiten Materialparameter neu zu erstellen?*

In other words, he would like to add specific IDs (parameters) to materials that he could write out to a database. He mentions the analogy with shared parameters &ndash; it seems he would like to create custom shared parameters and add them to materials.

Do you know any solution for this idea?

**Answer 1:** So, the user has an external database and wishes to link Revit materials to database entries.

That is good, and easy.

Now I would suggest that we all switch on our brains for a moment.

The Revit materials reside in the Revit database and are therefore Revit elements.

They are therefore equipped with an element id and a unique id.

The external database exists and has entries for the materials, and needs some kind of link.

You might consider adding something new to the Revit database, e.g. a shared parameter on each material.

That is completely unnecessary, as far as I can tell.

Since you are already managing an external database, and since each material already is equipped with a unique id, I would suggest that you use that existing unique id to identify the material, and store that in the database.

If you need to link multiple materials to the same database entry and are using a relational database, you might want to implement a `1:n` link using an auxiliary table.

Unless there is more to the problem than has been mentioned so far, I would maintain that this is the most efficient solution.

It also has the advantage of solving the problem without introducing any new data elements or shared parameters anywhere.

What do you think?

On the other hand, if you insist on pursuing the suggested path, the fact is that as far as I know any Revit element can be equipped with a shared parameter, as long as it has a valid category.

Is there a category for materials?

Yes, there is.

Therefore, materials can have a shared parameter attached to them.

Here is an explanation of this that I wrote while [adding a shared parameter to a DWG file](http://thebuildingcoder.typepad.com/blog/2008/11/adding-a-shared-parameter-to-a-dwg-file.html).

You can look at the [list of things I already tested right there in the C# source code](https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs4.cs#L511-L535).

I am just in the process of testing for myself whether a shared param can be added to a material as well.

I'll let you know when I know.

**Answer 2:** I verified and tested successfully that shared parameters can be added to material elements.

You can see so by checking out and
running [version 2017.0.0.6](https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2017.0.0.6).

I switched back to the standard test using doors again after that, 
in [version 2017.0.0.7](https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2017.0.0.7).

The link to the list of things I already tested has changed, since the line numbers moved
to [Labs4.cs#L518-L539](https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs4.cs#L518-L539).

You should also always be aware of the possibility to
use [extensible storage](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23) instead
of shared parameters to store custom data, cf. also the SDK samples ExtensibleStorageManager and ExtensibleStorageUtility.

Shared parameters make sense if the user and/or Revit should be aware of the properties, e.g. to view or edit them in the properties panel or to make us of them for scheduling purposes.

If the data is purely intended for internal add-in use, extensible storage is normally the better way to go.



####<a name="5"></a>Binding a Shared Parameter to Elements

One way to add custom properties to Revit elements is by using a shared parameter.
If you can do that in the user interface, then you can do so programmatically as well.

The FireRating SDK sample shows how this can be achieved, and so does my modernised version of it,
[FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud), in
the [module Cmd_1_CreateAndBindSharedParameter.cs](https://github.com/jeremytammik/FireRatingCloud/blob/master/FireRatingCloud/Cmd_1_CreateAndBindSharedParameter.cs).

However, this is one of the few things that can be done programmatically for some element types even when it is not allowed in the UI.
If the Revit development team decided that the end user should not be able to add a shared parameter to a certain element type, it will not show up in the UI.
As long as it has a valid category, you can mostly attach a shared parameter to it programmatically anyway.

The option `AllowsBoundParameters` is related to this, determining the so-called visibility of the new parameter: the
`Category.AllowsBoundParameters` property determines whether a category is allowed to have user-visible shared or project parameters. If it is false, it may not be bound to visible shared parameters using the BindingMap. Note that non-user-visible parameters can still be bound to these categories.


####<a name="6"></a>Attaching a Shared Parameter to `Material` Elements

I have repeatedly used the external command `Lab4_3_1_CreateAndBindSharedParam` implemented in
the [AdnRevitApiLabsXtra ](https://github.com/jeremytammik/AdnRevitApiLabsXtra)
[module Labs4.cs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs4.cs) to
test and verify the possibility of binding shared parameters to various classes of elements.

I added `Material` elements to that list now as well, and that worked fine.

Here is the complete list of the element types we are interested in:

The standard SDK FireRating sample uses BuiltInCategory.OST_Doors. 
We also test using BuiltInCategory.OST_Walls to demonstrate that the same technique
works with system families just as well as with standard ones.

To test attaching shared parameters to inserted DWG files,
which generate their own category on the fly, we also identify
the category by category name.
Note that every DWG file linked in generates an own new category named after the DWG filename.
Therefore, I use the filename stem to determine the category to use for a DWG file, and the built-in category for all other classes of elements.

Further tests confirm the possibility to attach 
shared parameters to model groups, model lines, 
rectangular straight wall openings and material 
elements.

I successfully tested the following built-in categories:

- OST_Doors
- OST_Walls
- OST_Lines 
- OST_SWallRectOpening 
- OST_Materials 

You can examine exactly what modifications I made to test adding a shared parameter to `Material` elements by looking at the differences between the releases before and after doing so:

- [compare 2017.0.0.5...2017.0.0.6](https://github.com/jeremytammik/AdnRevitApiLabsXtra/compare/2017.0.0.5...2017.0.0.6) &ndash; compare the original code binding to the `Doors` category and the modifications made to switch to `Materials` instead.
- [compare 2017.0.0.6...2017.0.0.7](https://github.com/jeremytammik/AdnRevitApiLabsXtra/compare/2017.0.0.6...2017.0.0.7) &ndash; compare the code binding to the `Materials` category and the code after switching back to `Doors`.


####<a name="7"></a>Create and Populate a Shared Parameter `ForgeMaterial`

Funnily enough, Philippe just came up with a request to equip a sample BIM that he needs for his Forge demo with some enhanced material information.

I chose to create and populate a new shared parameter named `ForgeMaterial` to transport that through the translation process across to the Forge ecosystem.

You can check it out in its new [PopulateMaterialProperty GitHub repository](https://github.com/jeremytammik/PopulateMaterialProperty).

Note that my algorithm to determine what material to use for each element is rather arbitrary:

If the element category `HasMaterialQuantities` property is true, the element is deemed to consist of the first material with non-zero area.

Otherwise, the material element is simply set to the element category name.


####<a name="8"></a>Cornelius Story &ndash; Cured by Natural Remedies

Let me finish off on a personal note, sharing with you my
son [Cornelius story &ndash; cured by natural remedies](http://on-lyme.org/en/sufferers/lyme-stories/item/241-cornelius-story-cured-by-natural-remedies) on suffering and recovering from Lyme chronic disease.

<center>
<img src="img/cornelius_iris.jpg" alt="Iris" width="200">
</center>
