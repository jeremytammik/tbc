<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- clarification symbol versus instance geometry
GetInstanceGeometry vs GetSymbolGeometry
https://forums.autodesk.com/t5/revit-api-forum/getinstancegeometry-vs-getsymbolgeometry/m-p/10819201
[Q] ... about the methods GeometryInstance.GetInstanceGeometry() and GeometryInstance.GetSymbolGeometry().

In my file I've got only one imported dwg with only one line inside it. 

There is a code below that i use to test:

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

Executing this provides the following values:

instanceRepresentation = e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000a38:2:1:LINEAR 
symbolRepresentation = e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000c0f:0:INSTANCE:e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000a38:2:1:LINEAR

These include two `UniqueId` values:

e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000a38 &ndash; `CADLinkType`
e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000c0f &ndash; `ImportInstance`

For me, it seems like these two results were mixed up.
Looks like `instanceRepresentation` refers to symbol geometry, while `symbolRepresentation` refers to instance geometry.

I will be grateful for any help.

TestProject.rvt
test.dwg

[A] Seems logical in a way, no?
When you use symbol it give the full lineage of symbol and instance of that symbol.
When you use the copy (with method noted below) it just gives the symbol it was copied from.
There is no actual instance for it, because that function just creates a copy at the time for you (is a helper method for specific purposes).

Beyond CADLinks you'll find that there are multiple version of symbol geometries for a type, i.e., there is often a symbol to represent each structural framing length (with such lengths being driven by instance variations not type variations). So equating symbol geometry to family symbols probably is confusing to start with. That is to say they all different ids and at time of extraction the form of symbol geometry you get is going to be partly decided by the instance variations not just the type variations.

Extract from [RevitAPI.chm on GeometryInstance.GetInstanceGeometry](https://www.revitapidocs.com/2022/22d4a5d4-dfc2-7227-2cae-b989729696ec.htm):

> ...This method returns a copy of the Revit geometry. It is suitable for use in a tool which extracts geometry to another format or carries out a geometric analysis; however, because it returns a copy the references found in the geometry objects contained in this element are not suitable for creating new Revit elements referencing the original element (for example, dimensioning). Only the geometry returned by GetSymbolGeometry() with no transform can be used for that purpose."

- Advanced Revit Remote Batch Command Processing
David Echols, Senior Programmer at Hankins & Anderson, Inc.
SD5980 at Autodesk University 2014
This class will explain a process to run external commands in batch mode from a central server to remote Revit® application workstations. We will cover how to use client and server applications that communicate with each other to manage Revit® software on remote workstations with WCF (Windows Communication Foundation) services. We will examine how to pass XML command data to the Revit® application to open a Revit® model and initiate batch commands. We will also show a specific use case for batch export of DWG files for sheets. We will examine a flexible system for handling Revit® dialog boxes on the fly with usage examples and code snippets, and we will discuss the failure processing API in the context of bypassing warning and error messages while custom commands are running. Finally, we will show you how to gracefully close both the open Revit® model and the Revit® application.

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

An eclectic mix of topics for today:

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

- https://forums.autodesk.com/t5/revit-api-forum/importing-and-displaying-satellite-images/m-p/10815534

####<a name="4"></a> RVT Dashboard Data Access

- how to access data in RVT for a dashboard
https://autodesk.slack.com/archives/C0SR6NAP8/p1638915918163600
[Q] I would like to collect data from Revit models for display in a dashboard.  I thought of using the model derivative APIs in Forge to retrieve the data, or use DA4R since the Revit model must be opened to access the database.  Well, this Apple team does not have access to Forge/DA4R and are asking for a way to read the data without Revit.  I let the team know that Revit must be used to unpack the database to make it useable to collect data from.  The reason for this is that much of the content in Revit is created on demand dynamically and not necessarily stored in the file database.
Am I on the right track here?  Apple has mentioned to me that Safe Software has created a platform called FME that can run on a Mac and can collect the Revit model data needed.  How is it possible for the FME platform to read the contents like this?  I expect that there is a cloud service utilizing DA4R or similar to process the model, but wanted to check in here before pushing back on Apple's desire to engage consulting to build a data collection engine outside of Revit (because I do not believe it is possible).
[A] RVT file format is a structured storage file, so some content can be pulled without opening the file directly, but you'd have to code up your own means of doing so. We don't support it directly in any way I know.
Because of that format some data is quite easy to access (ie: Transmission data and basic file info, both of which can be read with tools like Structured Storage Viewer), but other aspects are more difficult (ie: how many light fixtures are in room number 2143, how many warnings are in the model, how many doors don't have a valid mark, etc.).
Forge isn't necessarily a 'must use' as the data might be accessible via other means (ie: upload to BIM360 or use the online viewer; use the model checker which is coming to the web soon) or move to another file format for the final deliverable (ie: perhaps they should be mining data from the digital twin or an IFC instead of an RVT). Short of those two alterations it's likely best to stick with forge.

####<a name="5"></a> Marking and Retrieving a Custom Element

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

####<a name="6"></a> Advanced Revit Remote Batch Command Processing

David Echols, Senior Programmer at Hankins & Anderson, Inc.
SD5980 at Autodesk University 2014
This class will explain a process to run external commands in batch mode from a central server to remote Revit® application workstations. We will cover how to use client and server applications that communicate with each other to manage Revit® software on remote workstations with WCF (Windows Communication Foundation) services. We will examine how to pass XML command data to the Revit® application to open a Revit® model and initiate batch commands. We will also show a specific use case for batch export of DWG files for sheets. We will examine a flexible system for handling Revit® dialog boxes on the fly with usage examples and code snippets, and we will discuss the failure processing API in the context of bypassing warning and error messages while custom commands are running. Finally, we will show you how to gracefully close both the open Revit® model and the Revit® application.

