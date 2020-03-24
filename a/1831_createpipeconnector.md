<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/createpipeconnector-from-referenceplane/m-p/9396013

- https://forums.autodesk.com/t5/revit-api-forum/auto-route-a-pipe-system-between-plumbing-fixtures/m-p/9387502

twitter:

 the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

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

### ReferencePlane for CreatePipeConnector

Today, let's pick up two recent MEP related discussions, on creating a new pipe connector for a hydraulic fitting family and on automatic pipe system routing

#### <a name="2"></a>Getting a ReferencePlane for CreatePipeConnector

I had an intereseting and fruitful conversation with Edouard Vnuk in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [CreatePipeConnector from ReferencePlane](https://forums.autodesk.com/t5/revit-api-forum/createpipeconnector-from-referenceplane/m-p/9396013):

**Question:** I would like to create a connector from a reference plane.
The `CreatePipeConnector` function requires a `PlanarFace`.
Is there another function, or how can I transform a `ReferencePlane` into `PlanarFace`?

**Answer:** The [ReferencePlane documentation sample code](https://www.revitapidocs.com/2020/e7003ec7-1dbe-50a2-fb3d-a83a5a3b5b9f.htm) shows
how to call GetPlane to retrieve the DB Plane.

More to the point, the the CreateAirHandler SDK sample to how to use the `CreatePipeConnector` method.

When in doubt about how to call a Revit API method, one of the first places to always consult is the collection of Revit SDK samples.
That step often helps and may save time and effort for you and others.

**Response:** I tried several codes without success.

1st code:

<pre class="code">
Autodesk.Revit.DB.Plane plane = Reference_plane.GetPlane();
ConnectorElement connector = ConnectorElement.CreatePipeConnector(family_document, PipeSystemType.Global, plane);
</pre>

However, I can't compile this code, because the parameter must be a reference of planar face, and I don't know how to get it from the plane.

2nd code:

<pre class="code">
ConnectorElement connector = ConnectorElement.CreatePipeConnector(family_document, PipeSystemType.Global, Reference_plane.GetReference());
</pre>

This throws an exception during execution: The reference is not a planar face. Parameter name: planarFace.

These are the user interface steps I would like to complete programmatically:

<center>
<img src="img/pipe_connector_ui_1.jpg" alt="Pipe connector placement user interface" title="Pipe connector placement user interface" width="100"/> <!-- 1142 -->
<br/>
<img src="img/pipe_connector_ui_2.jpg" alt="Pipe connector placement user interface" title="Pipe connector placement user interface" width="100"/> <!-- 1142 -->
<br/>
<img src="img/pipe_connector_ui_3.jpg" alt="Pipe connector placement user interface" title="Pipe connector placement user interface" width="100"/> <!-- 1142 -->
<br/>
<img src="img/pipe_connector_ui_4.jpg" alt="Pipe connector placement user interface" title="Pipe connector placement user interface" width="100"/> <!-- 1142 -->
</center>

I looked the CreateAirHandler SDK sample example.
However, if I understood correctly, it creates the connectors from an extruded volume that provides the required planar faces.

**Answer:** Good. I see your point.
Such situations arise regularly.
You do have a real existing surface somewhere in the model that you can mount your connector on, don't you?
Otherwise, you are modelling something that cannot be built.
The existing surface is part of some BIM element geometry.
You can identify the appropriate element and retrieve its geometry from the document by calling its Geometry property and providing an Options object with ComputeReferences set to true.
Then, iterate through all its surfaces to identify the one you need.
Et voila, that surface is equipped with a reference that you can use to create the connector.

**Response:** What you told me inspired me to think about it.
I am creating a converter.
And so, I create automaticly families from basic geometric shapes.
To add my connectors that do not rest on an existing face, I artificially added a cylinder that I masked where I wanted a connector.
Here's what I get :

<center>
<img src="img/pipe_connector_in_family.jpg" alt="Pipe connector in family" title="Pipe connector in family" width="100"/> <!-- 1142 -->
<p style="font-size: 80%; font-style:italic">Pipe connector in family</p>
<img src="img/pipe_connector_in_project.jpg" alt="Pipe connector in project" title="Pipe connector in project" width="100"/> <!-- 1142 -->
<p style="font-size: 80%; font-style:italic">Pipe connector in project</p>
</center>

Thank you so much.

Edouard

Many thanks to Edouard for raising this, the in-depth discussion and sharing his clean solution!

#### <a name="2"></a>Auto-routing a Pipe System Between Plumbing Fixtures

The other item I would like to pick up here is
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [auto routing a pipe system between plumbing fixtures](https://forums.autodesk.com/t5/revit-api-forum/auto-route-a-pipe-system-between-plumbing-fixtures/m-p/9387502):

**Question:** I'm wondering if there is something within the API that will allow me to place plumbing fixture families and then generate a plumbing layout like you can in Revit by using the "Create Plumbing System --> Generate Layout" or is this something I have to do one pipe, one connector at a time?

auto_route_pipe_system_1.png

Create Pipe System.png

Selected Elements and Click Create System

auto_route_pipe_system_generate_layout.png

Generate Layout Button

auto_route_pipe_system_auto_layout.png

Auto Generated Pipes


jeremytammik

You can create a placeholder piping system programmatically as well:

https://thebuildingcoder.typepad.com/blog/2011/07/mep-placeholders.html

Or you can create a piping system right away. Here is a series of samples that create a minimal bunch of pipes for a rolling offset in lots of different ways:

http://thebuildingcoder.typepad.com/blog/2014/01/final-rolling-offset-using-pipecreate.html

tknappEUVP8

I am assuming from your response that if I want to connect from say, a water closet to a water heater, with pipes, I will have to explicitly call PipeCreate for each pipe, find connectors, insert each fitting... and then that process over and over until they connect? Is there a better work flow for what I'm trying to accomplish? Something similar to what is in the Revit user interface?

jeremytammik

Please read the discussions I pointed out. They explain everything and cover your question in full. Why do you think I publish them?

Actually, here are some additional relevant discussions of this topic:

https://thebuildingcoder.typepad.com/blog/2018/03/connector-neighbour-conduit-transition.html
https://thebuildingcoder.typepad.com/blog/2019/08/mep-ductwork-and-changing-pipe-direction.html

tknappEUVP8

Apologies, I seem to have frustrated you. I did look through the previous sent links, however I didn't find a direct answer to my question, therefor I tried to clarify. I'm still not completely clear on your answer, so I will try to solve it myself. Thanks anyway.

jeremytammik

Well, I am not an expert on this, but I think the answer is:

You can go any way you like.
You can just insert the pipes, no fittings. Then, if you connect the pipe connectors, Revit will automatically select and place and connect the appropriate fittings according to your routing preferences and adjust the pipe endpoints so they fit.
You can just insert all the fittings, no pipes. Then, if you connect the fitting connectors, Revit will automatically place and connect the appropriate pipes between them.

That is the gist of what I learned researching for and implementing the rolling offset.

<pre class="code">
</pre>

**Answer:** 

**Response:** 

#### <a name="3"></a>

