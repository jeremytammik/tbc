<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- clarification symbol versus instance geometry
  GetInstanceGeometry vs GetSymbolGeometry
  https://forums.autodesk.com/t5/revit-api-forum/getinstancegeometry-vs-getsymbolgeometry/m-p/10819201

- https://forums.autodesk.com/t5/revit-api-forum/importing-and-displaying-satellite-images/m-p/10815534

- how to access data in RVT for a dashboard
https://autodesk.slack.com/archives/C0SR6NAP8/p1638915918163600
[Q] I would like to collect data from Revit models for display in a dashboard.  I thought of using the model derivative APIs in Forge to retrieve the data, or use DA4R since the Revit model must be opened to access the database.  Well, this Apple team does not have access to Forge/DA4R and are asking for a way to read the data without Revit.  I let the team know that Revit must be used to unpack the database to make it useable to collect data from.  The reason for this is that much of the content in Revit is created on demand dynamically and not necessarily stored in the file database.
Am I on the right track here?  Apple has mentioned to me that Safe Software has created a platform called FME that can run on a Mac and can collect the Revit model data needed.  How is it possible for the FME platform to read the contents like this?  I expect that there is a cloud service utilizing DA4R or similar to process the model, but wanted to check in here before pushing back on Apple's desire to engage consulting to build a data collection engine outside of Revit (because I do not believe it is possible).
[A] RVT file format is a structured storage file, so some content can be pulled without opening the file directly, but you'd have to code up your own means of doing so. We don't support it directly in any way I know.
Because of that format some data is quite easy to access (ie: Transmission data and basic file info, both of which can be read with tools like Structured Storage Viewer), but other aspects are more difficult (ie: how many light fixtures are in room number 2143, how many warnings are in the model, how many doors don't have a valid mark, etc.).
Forge isn't necessarily a 'must use' as the data might be accessible via other means (ie: upload to BIM360 or use the online viewer; use the model checker which is coming to the web soon) or move to another file format for the final deliverable (ie: perhaps they should be mining data from the digital twin or an IFC instead of an RVT). Short of those two alterations it's likely best to stick with forge.

- marking and retrieving a custom element
  https://autodesk.slack.com/archives/C0SR6NAP8/p1638455279133900
  Shane Bluemel 
  Hi folks. We're currently spiking a new feature in the Revit Issues Addin where we create our own view to show additional ACC data within Revit. We've hit a bump around the view ID and wondered whether there's something in the API we haven't spotted that might be able to help us.   
  We essentially want to create our own temporary view within Revit so that we can show ACC issues which are not on the current model loaded in Revit (i.e. from linked files or from a multi-model view only available in ACC). Our current spike creates a view, populates it and deletes it on shutdown. This is all fine. However, if the view is the only one open in Revit on shutdown it'll get saved into the file. This got us thinking that we could just save a default Addins view and look for it on next load. However, we can't find a way to determine the ID that's used for the view so we won't know what we're looking for. Some questions where you may be able to help us:
  Can we create a view with a read-only name (so the user can't edit it and we can search for that)?
  Can we define a Revit View ID using a GUID somehow?
  Is there somewhere we could store the view ID used within the file so that we can retrieve it on load? We considered storing it in our own settings file but that doesn't work if the file gets sent to another user.
  Any help or advice you can offer would be much appreciated.
  Jacob Small
  This is a prime case for extensible storage in my opinion. Make a new schema and save the GUID of the view into it.
  Users will delete that view though, and if it doesn’t file into the project browser correctly there will be push back. Expect to delete and recreate the view often (even mid session).
  Also ensure that we have good product documentation on why this is in the file, and how it can be worked with, and the like. Otherwise we will have a LOT support cases around the feature.
  Shane Bluemel
  Fantastic, thanks Jacob. Yes, documentation and the options we present to the user around how they use this feature are important. We also need to be careful around the default name of the view so that it's purpose is obvious enough. Thanks for the help and advice.

- Advanced Revit Remote Batch Command Processing
David Echols, Senior Programmer at Hankins & Anderson, Inc.
SD5980 at Autodesk University 2014
This class will explain a process to run external commands in batch mode from a central server to remote Revit® application workstations. We will cover how to use client and server applications that communicate with each other to manage Revit® software on remote workstations with WCF (Windows Communication Foundation) services. We will examine how to pass XML command data to the Revit® application to open a Revit® model and initiate batch commands. We will also show a specific use case for batch export of DWG files for sheets. We will examine a flexible system for handling Revit® dialog boxes on the fly with usage examples and code snippets, and we will discuss the failure processing API in the context of bypassing warning and error messages while custom commands are running. Finally, we will show you how to gracefully close both the open Revit® model and the Revit® application.

twitter:

 with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; ...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Symbol, Instance, OLE, Journal

This is probably my last post of the year, so let's wrap up with an eclectic mix of topics for today:

####<a name="2"></a> Clarification Symbol versus Instance Geometry

Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
clarifies some aspects of symbol versus instance geometry iin an imported DWG file in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [`GetInstanceGeometry` vs `GetSymbolGeometry`](https://forums.autodesk.com/t5/revit-api-forum/getinstancegeometry-vs-getsymbolgeometry/m-p/10819201):

**Question:** ... about the methods GeometryInstance.GetInstanceGeometry() and GeometryInstance.GetSymbolGeometry().

In my file, I've got only one imported DWG with only one line inside it. 

I analysed it using the following code:

<pre class="code">
var reference = _selection.PickObject(ObjectType.PointOnElement);
var element = _doc.GetElement(reference);
var options = new Options();
options.ComputeReferences = true;
options.View = _doc.ActiveView;
    
var geometryElement = element.get_Geometry(options);
var geometryInstance = geometryElement
        .FirstOrDefault(x => x is GeometryInstance) as 
         GeometryInstance;

var instanceGeometry = geometryInstance?.GetInstanceGeometry();
var instanceCurve=instanceGeometry?.FirstOrDefault(x => x is Curve) as Curve;
var instanceReference = instanceCurve?.Reference;
var instanceRepresentation = instanceReference?.ConvertToStableRepresentation(_doc);
    
var symbolGeometry = geometryInstance?.GetSymbolGeometry();
var symbolCurve=symbolGeometry?.FirstOrDefault(x => x is Curve) as Curve;
var symbolReference = symbolCurve?.Reference;
var symbolRepresentation = symbolReference?.ConvertToStableRepresentation(_doc);
</pre>

Executing this provides the following values:

- `instanceRepresentation` = e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000a38:2:1:LINEAR 
- `symbolRepresentation` = e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000c0f:0:INSTANCE:e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000a38:2:1:LINEAR

These include two `UniqueId` values:

- e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000a38 &ndash; `CADLinkType`
- e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000c0f &ndash; `ImportInstance`

For me, it seems like these two results were mixed up.
Looks like `instanceRepresentation` refers to symbol geometry, while `symbolRepresentation` refers to instance geometry.

**Answer:** Seems logical in a way, no?
When you use symbol, it give the full lineage of symbol and instance of that symbol.
When you use the copy (with method noted below), it just gives the symbol it was copied from.
There is no actual instance for it, because that function just creates a copy at the time for you (is a helper method for specific purposes).

Beyond CADLinks, you'll find that there are multiple version of symbol geometries for a type, i.e., there is often a symbol to represent each structural framing length (with such lengths being driven by instance variations not type variations).
So, equating symbol geometry to family symbols probably is confusing to start with.
That is to say they are all different ids and at time of extraction the form of symbol geometry you get is going to be partly decided by the instance variations not just the type variations.

Extract from [RevitAPI.chm on GeometryInstance.GetInstanceGeometry](https://www.revitapidocs.com/2022/22d4a5d4-dfc2-7227-2cae-b989729696ec.htm):

> ...This method returns a copy of the Revit geometry. It is suitable for use in a tool which extracts geometry to another format or carries out a geometric analysis; however, because it returns a copy the references found in the geometry objects contained in this element are not suitable for creating new Revit elements referencing the original element (for example, dimensioning). Only the geometry returned by GetSymbolGeometry() with no transform can be used for that purpose."

Here is a simple example demonstrated:

/Users/jta/a/doc/revit/tbc/git/a/img/rpt_symb_vs_inst_geom_1.png

Two beams of same family type
Two beams of same family type

/Users/jta/a/doc/revit/tbc/git/a/img/rpt_symb_vs_inst_geom_2.png

The short beam as id 427840
The short beam as id 427840

/Users/jta/a/doc/revit/tbc/git/a/img/rpt_symb_vs_inst_geom_3.png

The long beam as id 427855
The long beam as id 427855

/Users/jta/a/doc/revit/tbc/git/a/img/rpt_symb_vs_inst_geom_4.png

The FamilySymbol id 95037
The FamilySymbol id 95037

If you check the bounding boxes extents for the two geometry symbols you'll see they match the beam lengths.

This gets further complicated with cuts but it demonstrates that Revit is storing geometrical symbol variations differently to how we think of the type instance relationships based on type and instance parameters.

####<a name="3"></a>

Harm van den Brand shares a new implementation of a suggestion by Rudi *Revitalizer* Honke to create a new material and set its texture in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [importing and displaying satellite images](https://forums.autodesk.com/t5/revit-api-forum/importing-and-displaying-satellite-images/m-p/10815534):

I'm building an add-in for Revit and I would like to be able to import and display third-party satellite imagery in order to place buildings in their 'real' position.
I would like to be able to do this in a 3D view, but I don't know how.

The user workflow for my add-in is this:

- A user opens the add-in and is prompted to input a location through a WPF window.
- Once a location is confirmed, a number of things are created/imported into the active project to make it look as close as possible to its actual real-life location. One of these things is the satellite image I'm seeking to import here.

Essentially, my question is exactly [this one](), but instead doing that programmatically/automatically through an add-in. In that thread, a suggestion is made to create a decal with the desired image, but this does not seem to be supported through the API.

Another approach I found is to use `PostCommand` to create and place decals, but these commands are apparently only executed after exiting the API context and only one at a time.
As my add-in aims to perform a whole bunch of functionalities in one go, this seem ill-suited for my use case.
It seems to be possible to chain a bunch of `PostCommand` calls, but this is a little 'hacky' and not recommended, especially for commercial use.

Am I overlooking some existing functionality?
Is my use case just not supported in current Revit?
I'm new to programming for Revit, so it's very possible I've missed something.

I'm running / programming for Revit 2019 on Windows 10.

**Answer:** What about creating a new material, setting its texture path, then making a `TopoSurface` and assigning the material to it?

https://thebuildingcoder.typepad.com/blog/2017/11/modifying-material-visual-appearance.html

I don't know how to adjust the UV mapping for the TopoSurface, but if it worked, you would see your satellite image in 3D.

**Response:** Thanks to all for the replies!
It took some time to try out the proposed solution (accessing AppearanceElements is convoluted!), so that's why it took me this long to reply.

In the end, though I had to work around some weird quirks with the API, adding the image as a texture to a topography through a material works great.

Thanks again for the suggestion, I couldn't have made it work without it.

As mentioned, I ended up taking Revitalizer's suggested approach of creating a new material and setting its texture.

<pre class="code>
Material underlayMaterial = Material.Create(revitDocument, materialName);
</pre>

To this material, I link a so-called AppearanceAsset:

<pre class="code>
underlayMaterial.AppearanceAssetId = assetElement.Id;
</pre>

(more on how I get this assetElement in a moment)

And then I assign the path of a jpeg image to the texture asset:

<pre class="code>
using (AppearanceAssetEditScope editScope = new AppearanceAssetEditScope(revitDocument))
{
    Asset editableAsset = editScope.Start(assetElement.Id);

    AssetProperty assetProperty = editableAsset.FindByName("generic_diffuse");
    Asset connectedAsset = assetProperty.GetConnectedProperty(0) as Asset;

    //Edit bitmap
    if (connectedAsset.Name == "UnifiedBitmapSchema")
    {
        AssetPropertyString path = connectedAsset.FindByName(UnifiedBitmap.UnifiedbitmapBitmap) as AssetPropertyString;

        if (path.IsValidValue(imagePath))
            path.Value = imagePath;

        //You might have to fiddle a bit with the scale properties, for example when your source uses centimeters:
        AssetPropertyDistance scaleX = connectedAsset.FindByName(UnifiedBitmap.TextureRealWorldScaleX) as AssetPropertyDistance;
        AssetPropertyDistance scaleY = connectedAsset.FindByName(UnifiedBitmap.TextureRealWorldScaleY) as AssetPropertyDistance;

        //Because newly added bitmaps are displayed in inches.
        if (scaleX.DisplayUnitType == DisplayUnitType.DUT_DECIMAL_INCHES)
            scaleX.Value /= 2.54;
        if (scaleY.DisplayUnitType == DisplayUnitType.DUT_DECIMAL_INCHES)
            scaleY.Value /= 2.54;

    }
    editScope.Commit(true);
}
</pre>

Now, the reason why I would call my solution 'hacky', is because of how I retrieve the assetElement.

Instinctively, I would want to create a new, empty instance of Asset. Something like

<pre class="code>
AppearanceAssetElement assetElement = AppearanceAssetElement.Create();

However, this is not how Revit's material/texture api works. We can only use those materials/textures/etc. that are present in Revit's libraries. Therefore we can only make copies of existing ones:

<pre class="code>
// Retrieve asset library from the application (this is the only source available; instantiating from zero is impossible)
IList<Asset> assetList = commandData.Application.Application.GetAssets(AssetType.Appearance);

//Select arbitrary asset from library (200 works, not all do)
Asset asset = assetList[200];

AppearanceAssetElement assetElement;
try
{
    assetElement = AppearanceAssetElement.Create(revitDocument, someNewName, asset);
}
</pre>

Yes, I really just randomly tried indices in that assetList until I found one that worked, and hardcoded that one in. Not all AppearanceAssets in the list have the necessary "generic_diffuse" assetProperty to which we can bind a texture, so we have to select one that does.

If you are developing your addin for external parties this is risky, because we can't ensure that the same libraries are available for any particular user. It's probably best to somehow filter for valid AppearanceAssets.

Also, you can see that retrieving this appearanceAsset requires ExternalCommandData (which I named CommandData in the code given), which an addin retrieves via the 'Execute' method of a IExternalCommand-implementing class.

Also, remember to wrap most of these snippets in transactions.

I hope this helps!

####<a name="4"></a> RVT Dashboard Data Access

Some notes from an internal discussion on how to access data in RVT for a dashboard:

**Question:** I would like to collect data from Revit models for display in a dashboard.
I thought of using the model derivative APIs in Forge to retrieve the data, or use DA4R since the Revit model must be opened to access the database.
However, I do not have access to Forge or DA4R and would prefer a way to read the data without Revit.
I assume that Revit must be used to unpack the database to make it useable to collect data from.
Much of the content in Revit is created on demand dynamically and not necessarily stored in the file database.
Am I on the right track here?
I heard about some other app that can collect the Revit model data needed.
How can it read the contents without Revit?
Is there some cloud service utilizing DA4R or similar to process the model?

**Answer:** The RVT file format is a structured storage file, so some content can be pulled without opening the file directly. 
Because of that format, some data is quite easy to access, e.g., using transmission data and basic file info, both of which can be read with tools like Structured Storage Viewer:

- [BasicFileInfo](https://www.revitapidocs.com/2022/475edc09-cee7-6ff1-a0fa-4e427a56262a.htm)
- [TransmissionData](https://www.revitapidocs.com/2022/d78d1e9c-1cee-1336-88d5-b605dacd077d.htm)

Other aspects are more difficult, e.g., how many light fixtures are in room number 2143, how many warnings are in the model, how many doors don't have a valid mark, etc.
Forge isn't necessarily a 'must use' as the data might be accessible via other means, e.g, upload to BIM360 or use the online viewer; use the model checker, or move to another file format for the final deliverable.
Also, might be able to mine data from the digital twin or an IFC instead of an RVT.
Short of those two, it's likely best to stick with forge.

####<a name="5"></a> Marking and Retrieving a Custom Element

**Question:** My add-in creates its own view to show additional data within Revit.
I hit a bump around the view element id and wonder whether anything in the API might be able to help.   
I essentially want to create my own temporary view within Revit to show data that is not from the currently loaded model, i.e., from linked files or elsewhere.
Right now, I create a view, populate it and delete it on shutdown.
This is all fine.

However, if the view is the only one open in Revit on shutdown, it gets saved into the file.
This got me thinking that I could just save a default add-in view and look for it on next load.
However, I can't find a way to determine the view element id, so I don't know what to look for. 

- Can I create a view with a read-only name, so the user can't edit it and I can search for that?
- Can I define a Revit view ID using a GUID somehow?

Is there anywhere I could store the view ID, so that I can retrieve it on load? 
I considered storing it in my own settings file, but that doesn't work if the file gets sent to another user.

**Answer:** This is a prime case for [extensible storage](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23) in my opinion.
Make a new schema and save the `UniqueId` of the view into it.
Users will delete that view, though, and if it doesn’t file into the project browser correctly, there will be push back.
Expect to delete and recreate the view often (even mid session).
Also, ensure that you have good product documentation on why this is in the file, and how it can be worked with, and the like.
Otherwise you will have a LOT of support cases around the feature.

**Response:** Fantastic.
Yes, documentation and the options we present to the user around how they use this feature are important.
We also need to be careful around the default name of the view, so it's purpose is obvious enough.
Thanks for the help and advice.

####<a name="6"></a> Advanced Revit Remote Batch Command Processing

Several people recently raised questions on automating Revit workflows, and one possibility to consider is the use of Revit journal files.
David Echols, Senior Programmer at Hankins & Anderson, Inc. shared some important insights and experience in this area in his Autodesk University 2014 class SD5980 on *Advanced Revit Remote Batch Command Processing*:

> This class explains a process to run external commands in batch mode from a central server to remote Revit application workstations.
It covers how to use client and server applications that communicate with each other to manage Revit software on remote workstations with WCF (Windows Communication Foundation) services, examines how to pass XML command data to the Revit application to open a Revit model and initiate batch commands, shows a specific use case for batch export of DWG files for sheets, examines a flexible system for handling Revit dialog boxes on the fly with usage examples and code snippets, and discusses the failure processing API in the context of bypassing warning and error messages while custom commands are running. Finally, it shows you how to gracefully close both the open Revit model and the Revit application.

- [Handout: *RevitJournals.pdf*](zip/RevitJournals.pdf)

####<a name="7"></a> Midwinter Break

We are nearing the middle of winter here on the northern hemisphere, and Autodesk has announced company holidays in the last days of the calnbedar year.

I am looking forward to some peaceful time to recuperate during the end
of [advent](https://en.wikipedia.org/wiki/Advent),
followed by the [twelve-night](https://en.wikipedia.org/wiki/Twelfth_Night_(holiday)) turning point of the year,
also known as [Rauhnächte](https://de.wikipedia.org/wiki/Rauhnacht) or *raw nights* in German,
full of special depth and significance, related to the differences between
the [lunar](https://en.wikipedia.org/wiki/Lunar_calendar) and solar cycles,
beginning with [Christmas](https://en.wikipedia.org/wiki/Christmas),
[Hanukkah](https://en.wikipedia.org/wiki/Hanukkah),
Celtic [Samhain](https://en.wikipedia.org/wiki/Samhain),
Druid [Alban Arthan](https://en.wikipedia.org/wiki/Alban_Arthan),
and many other sacred traditions.

A time of confusion, breaking things, going wrong, calming down, going slowly, contemplation, relaxing into peace and quiet and new beginnings.

I wish you a wonderful midwinter break full of light and warmth!

<center>
<img src="img/candlelight_snow.jpg" alt="Candlelight in snow" title="Candlelight in snow" width="280"/>
</center>
