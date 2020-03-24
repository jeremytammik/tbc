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

**Question:** 

I would like to create a connector from a reference plane. CreatePipeConnector function requires a PlanarFace. Is there another function or how can I transform a ReferencePlan into PlanarFace?
Thank you.

 Solved by jeremytammik. Go to Solution.

Add tags
Report
MESSAGE 6 OF 7
jeremytammik
 Employee jeremytammik in reply to: Exploreredouard.vnuk
‎2020-03-23 01:04 PM 
 Re: CreatePipeConnector from ReferencePlane
Good. I see your point. Such situations arise regularly. You do have a real existing surface somewhere in the model that you can mount your connector on, don't you? Otherwise, you are modelling something that cannot be built. The existing surface is part of some BIM element geometry. You can identify the appropriate element and retrieve its geometry from the document by calling its Geometry property and providing an Options object with ComputeReferences set to true. Then, iterate through all its surfaces to identify the one you need. Et voila, that surface is equipped with a reference that you can use to create the connector.

I hope this helps.

Best regards,

Jeremy

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

View solution in original post

Tags (0)
Add tags
Report
MESSAGE 2 OF 7
jeremytammik
 Employee jeremytammik in reply to: Exploreredouard.vnuk
‎2020-03-21 07:27 AM 
Re: CreatePipeConnector from ReferencePlane 
The ReferencePlane documentation sample code shows how to call GetPlane to retrieve the DB Plane:

https://www.revitapidocs.com/2020/e7003ec7-1dbe-50a2-fb3d-a83a5a3b5b9f.htm

More to the point, the the CreateAirHandler SDK sample to how to use the CreatePipeConnector method.

When in doubt about how to call a Revit API method, one of the first places to always consult is the collection of Revit SDK samples. That step often helps and may save time and effort for you and others.

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 3 OF 7
edouard.vnuk
 Explorer edouard.vnuk in reply to: Employeejeremytammik
‎2020-03-23 10:03 AM 
Re: CreatePipeConnector from ReferencePlane 
Hi Jeremy. Thank you for your response.
I try several codes without success.

1st code
Autodesk.Revit.DB.Plane plane = Reference_plane.GetPlane();
ConnectorElement connector = ConnectorElement.CreatePipeConnector(family_document, PipeSystemType.Global, plane);
=> I can't compile this code because the parameter must be a reference of planar face and i don't know how to get it from the plane.
2nd code
ConnectorElement connector = ConnectorElement.CreatePipeConnector(family_document, PipeSystemType.Global, Reference_plane.GetReference());
=> I have an error during the execution : The reference is not a planar face. Parameter name: planarFace
I would just like to do these with the API :

Menu_1.jpgMenu_2.jpgMenu_3.jpgMenu_4.jpg

Thank you again for your help.

Add tags
Report
MESSAGE 4 OF 7
jeremytammik
 Employee jeremytammik in reply to: Exploreredouard.vnuk
‎2020-03-23 10:46 AM 
Re: CreatePipeConnector from ReferencePlane
Have you looked at the approached used by the CreateAirHandler SDK sample?

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

Add tags
Report
MESSAGE 5 OF 7
edouard.vnuk
 Explorer edouard.vnuk in reply to: Employeejeremytammik
‎2020-03-23 11:18 AM 
Re: CreatePipeConnector from ReferencePlane
I looked the CreateAirHandler SDK sample example. But, if I understood correctly, it created the connectors from an extruded volume. So there are planar faces available.

Add tags
Report
MESSAGE 6 OF 7
jeremytammik
 Employee jeremytammik in reply to: Exploreredouard.vnuk
‎2020-03-23 01:04 PM 
 Re: CreatePipeConnector from ReferencePlane
Good. I see your point. Such situations arise regularly. You do have a real existing surface somewhere in the model that you can mount your connector on, don't you? Otherwise, you are modelling something that cannot be built. The existing surface is part of some BIM element geometry. You can identify the appropriate element and retrieve its geometry from the document by calling its Geometry property and providing an Options object with ComputeReferences set to true. Then, iterate through all its surfaces to identify the one you need. Et voila, that surface is equipped with a reference that you can use to create the connector.

I hope this helps.

Best regards,

Jeremy

Jeremy Tammik
Developer Technical Services
Autodesk Developer Network, ADN Open
The Building Coder

View solution in original post

Add tags
Report
MESSAGE 7 OF 7
edouard.vnuk
 Explorer edouard.vnuk in reply to: Employeejeremytammik
‎2020-03-24 11:50 AM 
Re: CreatePipeConnector from ReferencePlane 
Hello. I was a bit long, but what you told me inspired me to think about it. I am creating a converter. And so I create automaticly families from basic geometric shapes. To add, my connectors that do not rest on one face, I artificially added a cylinder that I masked where I wanted a connector. Here's what I get :

Famille.jpgDocument.jpg

Thank you so much.

Best regards,

Edouard

<pre class="code">
</pre>

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 1142 -->
</center>

**Answer:** 

**Response:** 

Many thanks to Bing Yongcao for sharing this solution!

#### <a name="3"></a>

