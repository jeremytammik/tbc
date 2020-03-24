<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/createpipeconnector-from-referenceplane/m-p/9396013

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

Today, let's pick up a recent discussion creating a new pipe connector for a hydraulic fitting family:

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


<pre class="code">
</pre>

**Answer:** 

**Response:** 

#### <a name="3"></a>

