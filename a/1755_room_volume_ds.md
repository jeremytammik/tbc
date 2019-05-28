<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-value-of-the-property-quot-loss-method-quot/m-p/8816013#M39043



twitter:

Generate DirectShape elements to represent room volumes in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/roomvolumedirectshape

Yesterday, I implemented a new add-in, RoomVolumeDirectShape, that creates <code>DirectShape</code> elements representing the volumes of all the rooms
&ndash; Request to display room volumes in Forge SVF file
&ndash; RoomVolumeDirectShape functionality
&ndash; Retrieving all element properties
&ndash; Converting a .NET dictionary to JSON
&ndash; Generating <code>DirectShape</code> from <code>ClosedShell</code>
&ndash; Complete external command class <code>Execute</code> method
&ndash; Sample model and results
&ndash; Challenges encountered underway
&ndash; Cherry BIM Services
&ndash; On the value of the "Loss Method" property
&ndash; AI-generated talking head models...

linkedin:

Generate DirectShape elements to represent room volumes in the #RevitAPI

http://bit.ly/roomvolumedirectshape

Yesterday, I implemented a new add-in, RoomVolumeDirectShape, that creates DirectShape elements representing the volumes of all the rooms:

- Request to display room volumes in Forge SVF file
- RoomVolumeDirectShape functionality
- Retrieving all element properties
- Converting a .NET dictionary to JSON
- Generating DirectShape from ClosedShell
- Complete external command class Execute method
- Sample model and results
- Challenges encountered underway
- Cherry BIM Services
- On the value of the "Loss Method" property
- AI-generated talking head models...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

-->

### DirectShape Element to Represent Room Volume

Yesterday, I implemented a new add-in, RoomVolumeDirectShape, that creates `DirectShape` elements representing the volumes of all the rooms.

I'll also mention some challenges encountered en route, some free add-ins shared by Cherry BIM Services, an insight in the meaning of the MEP fitting `Loss Method` property, and AI-generated talking head models:

- [Request to display room volumes in Forge SVF file](#2) 
- [RoomVolumeDirectShape functionality](#3) 
- [Retrieving all element properties](#4) 
- [Converting a .NET dictionary to JSON](#5) 
- [Generating `DirectShape` from `ClosedShell`](#6) 
- [Complete external command class `Execute` method](#7) 
- [Sample model and results](#8) 
- [Challenges encountered underway](#9) 
    - [Licensing system error 22](#9.1) 
    - [Valid direct shape categories](#9.2) 
    - [Direct shape phase and visibility](#9.3) 
- [Cherry BIM Services](#10) 
- [On the value of the "Loss Method" property](#11) 
- [AI-generated talking head models](#12) 



####<a name="2"></a> Request to Display Room Volumes in Forge SVF File

The [RoomVolumeDirectShape add-in](https://github.com/jeremytammik/RoomVolumeDirectShape) was
inspired by the following request:

<!--
from Mustapha Bismi of [Vinci Facilities](https://www.vinci-facilities.com)
for *Génération des volumes Revit*:

Aujourd’hui, notre workflow consiste à prendre la géométrie des pièces Revit, générer des fichiers SAT, puis recréer des volumes Revit à partir de cette géométrie.

Dans le cadre d’une automatisation, c’est pas terrible terrible.
-->

The context: We are building digital twins out of BIM data. To do so, we use Revit, Dynamo, and Forge.

<!-- You can check out what we are doing with that on our [twinops website](https://www.twinops.com). -->

The issue: We rely on the rooms in Revit to perform a bunch of tasks (reassign equipment localization, rebuild a navigation tree, and so on).

Unfortunately, these rooms are not displayed in the Revit 3D view.

Therefore, they are nowhere to be found in the Forge SVF file.

Our (so-so) solution: uses Dynamo to extract the room geometry and build Revit volumes.

It works, but it is:

- Not very robust: Some rooms has to be recreated manually, Dynamo crashes, geometry with invalid faces is produced, etc.
- Not very fast: The actual script exports SAT files and reimports them.
- Manual: Obviously, and also tedious and error-prone.

The whole process amounts to several hours of manual work.

We want to fix this.

Our goal: A robust implementation that will get rid of Dynamo, automate the process in Revit, and in the end, run that in a Forge Design Automation process.

The ideal way forward is exactly what you describe: A native C# Revit API that find the rooms, creates a direct shape volume for them, and copy their properties to that.

No intermediate formats, no UI, just straight automation work.

####<a name="3"></a> RoomVolumeDirectShape Functionality

Fulfilling this request, I implemented a
new [RoomVolumeDirectShape add-in](https://github.com/jeremytammik/RoomVolumeDirectShape) that
performs the following simple steps:

- Retrieve all rooms in the BIM using a filtered element collector
- For each room:
- Query the room for its closed shell using
the [ClosedShell API call](https://www.revitapidocs.com/2020/1a510aef-63f6-4d32-c0ff-a8071f5e23b8.htm)
- Generate a [DirectShape element](https://www.revitapidocs.com/2020/bfbd137b-c2c2-71bb-6f4a-992d0dcf6ea8.htm) representing the room volume geometry
- Query the room for all its properties, stored in parameters
(cf., [getting all parameter values](https://thebuildingcoder.typepad.com/blog/2018/05/getting-all-parameter-values.html)
and [retrieving parameter values from an element](https://thebuildingcoder.typepad.com/blog/2018/05/getting-all-parameter-values.html#5))
- Generate a JSON string representing a dictionary of the room properties
- Store the room property JSON string in the `DirectShape` element `Comment` property


####<a name="4"></a> Retrieving All Element Properties

The `GetParamValues` method retrieves and returns all the element parameter values in a dictionary mapping parameter names to the corresponding values.

For each entry, it also appends a single-character indicator of the parameter storage type to the key.

It makes use of two helper methods:

- `ParameterStorageTypeChar`, to return a key character for each storage type
- `ParameterToString`, to retrieve the parameter value as a string

<pre class="code">
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;parameter&nbsp;storage&nbsp;type&nbsp;abbreviation</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">char</span>&nbsp;ParameterStorageTypeChar(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;p&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;p&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentNullException</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;p&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;non-null&nbsp;parameter&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">char</span>&nbsp;abbreviation&nbsp;=&nbsp;<span style="color:#a31515;">&#39;?&#39;</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">switch</span>(&nbsp;p.StorageType&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.Double:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;abbreviation&nbsp;=&nbsp;<span style="color:#a31515;">&#39;r&#39;</span>;&nbsp;<span style="color:green;">//&nbsp;real&nbsp;number</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.Integer:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;abbreviation&nbsp;=&nbsp;<span style="color:#a31515;">&#39;n&#39;</span>;&nbsp;<span style="color:green;">//&nbsp;integer&nbsp;number</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.String:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;abbreviation&nbsp;=&nbsp;<span style="color:#a31515;">&#39;s&#39;</span>;&nbsp;<span style="color:green;">//&nbsp;string</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.ElementId:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;abbreviation&nbsp;=&nbsp;<span style="color:#a31515;">&#39;e&#39;</span>;&nbsp;<span style="color:green;">//&nbsp;element&nbsp;id</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.None:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentOutOfRangeException</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;p&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;expected&nbsp;valid&nbsp;parameter&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;storage&nbsp;type,&nbsp;not&nbsp;&#39;None&#39;&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;abbreviation;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;parameter&nbsp;value&nbsp;formatted&nbsp;as&nbsp;string</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;ParameterToString(&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;p&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s&nbsp;=&nbsp;<span style="color:#a31515;">&quot;null&quot;</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;p&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">switch</span>(&nbsp;p.StorageType&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.Double:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;s&nbsp;=&nbsp;p.AsDouble().ToString(&nbsp;<span style="color:#a31515;">&quot;0.##&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.Integer:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;s&nbsp;=&nbsp;p.AsInteger().ToString();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.String:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;s&nbsp;=&nbsp;p.AsString();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.ElementId:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;s&nbsp;=&nbsp;p.AsElementId().IntegerValue.ToString();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">StorageType</span>.None:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;s&nbsp;=&nbsp;<span style="color:#a31515;">&quot;none&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;s;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;all&nbsp;the&nbsp;element&nbsp;parameter&nbsp;values&nbsp;in&nbsp;a</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;dictionary&nbsp;mapping&nbsp;parameter&nbsp;names&nbsp;to&nbsp;values</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:blue;">string</span>&gt;&nbsp;GetParamValues(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Two&nbsp;choices:&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Element.Parameters&nbsp;property&nbsp;--&nbsp;Retrieves&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;a&nbsp;set&nbsp;containing&nbsp;all&nbsp;the&nbsp;parameters.</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;GetOrderedParameters&nbsp;method&nbsp;--&nbsp;Gets&nbsp;the&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;visible&nbsp;parameters&nbsp;in&nbsp;order.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//IList&lt;Parameter&gt;&nbsp;ps&nbsp;=&nbsp;e.GetOrderedParameters();</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ParameterSet</span>&nbsp;pset&nbsp;=&nbsp;e.Parameters;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:blue;">string</span>&gt;&nbsp;d
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:blue;">string</span>&gt;(&nbsp;pset.Size&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;p&nbsp;<span style="color:blue;">in</span>&nbsp;pset&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;AsValueString&nbsp;displays&nbsp;the&nbsp;value&nbsp;as&nbsp;the&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;user&nbsp;sees&nbsp;it.&nbsp;In&nbsp;some&nbsp;cases,&nbsp;the&nbsp;underlying</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;database&nbsp;value&nbsp;returned&nbsp;by&nbsp;AsInteger,&nbsp;AsDouble,</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;etc.,&nbsp;may&nbsp;be&nbsp;more&nbsp;relevant,&nbsp;as&nbsp;done&nbsp;by&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;ParameterToString</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;key&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(&nbsp;<span style="color:#a31515;">&quot;{0}({1})&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p.Definition.Name,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ParameterStorageTypeChar(&nbsp;p&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;val&nbsp;=&nbsp;ParameterToString(&nbsp;p&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;d.ContainsKey(&nbsp;key&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;d[key]&nbsp;!=&nbsp;val&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d[key]&nbsp;+=&nbsp;<span style="color:#a31515;">&quot;&nbsp;|&nbsp;&quot;</span>&nbsp;+&nbsp;val;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d.Add(&nbsp;key,&nbsp;val&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;d;
&nbsp;&nbsp;&nbsp;&nbsp;}
</pre>


####<a name="5"></a> Converting a .NET Dictionary to JSON

`FormatDictAsJson` converts the .NET dictionary of element properties to a JSON-formatted string:

<pre class="code">
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;a&nbsp;JSON&nbsp;string&nbsp;representing&nbsp;a&nbsp;dictionary</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;mapping&nbsp;string&nbsp;key&nbsp;to&nbsp;string&nbsp;value.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;FormatDictAsJson(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:blue;">string</span>&gt;&nbsp;d&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;keys&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;(&nbsp;d.Keys&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keys.Sort();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;key_vals&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;keys.Count&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">string</span>&nbsp;key&nbsp;<span style="color:blue;">in</span>&nbsp;keys&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key_vals.Add(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>.Format(&nbsp;<span style="color:#a31515;">&quot;\&quot;{0}\&quot;&nbsp;:&nbsp;\&quot;{1}\&quot;&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;key,&nbsp;d[key]&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#a31515;">&quot;{&quot;</span>&nbsp;+&nbsp;<span style="color:blue;">string</span>.Join(&nbsp;<span style="color:#a31515;">&quot;,&nbsp;&quot;</span>,&nbsp;key_vals&nbsp;)&nbsp;+&nbsp;<span style="color:#a31515;">&quot;}&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
</pre>

####<a name="6"></a> Generating DirectShape from ClosedShell

With the element parameter property retrieval and JSON formatting helper methods in place, very little remains to be done.

We gather all the rooms in the BIM using a filtered element collector, aware of the fact that the `Room` class only exists in the Revit API, not internally in Revit.

The filtered element collector therefore has to retrieve `SpatialElement` objects instead and use .NET post-processing to extract the rooms,
cf. [accessing room data](http://thebuildingcoder.typepad.com/blog/2011/11/accessing-room-data.html).

Once we have the rooms, we can process each one as follows:

- Retrieve room volume from `ClosedShell`
- Retrieve room properties
- Format properties into JSON string
- Create direct shape
- Set its geometry to the room volume
- Set its application data id to the room's `UniqueId`
- Set its name to contain the room name
- Store the room property dictionary in its comment parameter

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">GeometryElement</span>&nbsp;geo&nbsp;=&nbsp;r.ClosedShell;
 
&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:blue;">string</span>&gt;&nbsp;param_values
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;GetParamValues(&nbsp;r&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;json&nbsp;=&nbsp;FormatDictAsJson(&nbsp;param_values&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">DirectShape</span>&nbsp;ds&nbsp;=&nbsp;<span style="color:#2b91af;">DirectShape</span>.CreateElement(
&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;_id_category_for_direct_shape&nbsp;);
 
&nbsp;&nbsp;ds.ApplicationId&nbsp;=&nbsp;id_addin;
&nbsp;&nbsp;ds.ApplicationDataId&nbsp;=&nbsp;r.UniqueId;
&nbsp;&nbsp;ds.SetShape(&nbsp;geo.ToList&lt;<span style="color:#2b91af;">GeometryObject</span>&gt;()&nbsp;);
&nbsp;&nbsp;ds.get_Parameter(&nbsp;_bip_properties&nbsp;).Set(&nbsp;json&nbsp;);
&nbsp;&nbsp;ds.Name&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Room&nbsp;volume&nbsp;for&nbsp;&quot;</span>&nbsp;+&nbsp;r.Name;
</pre>


####<a name="7"></a> Complete External Command Class Execute Method

For the sake of completeness, here is the entire external command class and execute method implementation:

<pre class="code">
<span style="color:blue;">#region</span>&nbsp;Namespaces
<span style="color:blue;">using</span>&nbsp;System;
<span style="color:blue;">using</span>&nbsp;System.Linq;
<span style="color:blue;">using</span>&nbsp;System.Collections.Generic;
<span style="color:blue;">using</span>&nbsp;System.Diagnostics;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.ApplicationServices;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.Attributes;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.DB;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.DB.Architecture;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.UI;
<span style="color:blue;">#endregion</span>
 
<span style="color:blue;">namespace</span>&nbsp;RoomVolumeDirectShape
{
&nbsp;&nbsp;[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">Command</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Cannot&nbsp;use&nbsp;OST_Rooms;&nbsp;DirectShape.CreateElement&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;throws&nbsp;ArgumentExceptionL:&nbsp;Element&nbsp;id&nbsp;categoryId&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;may&nbsp;not&nbsp;be&nbsp;used&nbsp;as&nbsp;a&nbsp;DirectShape&nbsp;category.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Category&nbsp;assigned&nbsp;to&nbsp;the&nbsp;room&nbsp;volume&nbsp;direct&nbsp;shape</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;_id_category_for_direct_shape
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_GenericModel&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;DirectShape&nbsp;parameter&nbsp;to&nbsp;populate&nbsp;with&nbsp;JSON</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;dictionary&nbsp;containing&nbsp;all&nbsp;room&nbsp;properies</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>&nbsp;_bip_properties
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.ALL_MODEL_INSTANCE_COMMENTS;
 
// ... Property retrieval and JSON formatting helper methods ...

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;uiapp.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Application</span>&nbsp;app&nbsp;=&nbsp;uiapp.Application;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;id_addin&nbsp;=&nbsp;uiapp.ActiveAddInId.ToString();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Room</span>&gt;&nbsp;rooms
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">SpatialElement</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;e&nbsp;=&gt;&nbsp;e.GetType()&nbsp;==&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Room</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">Room</span>&gt;();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Generate&nbsp;Direct&nbsp;Shape&nbsp;Elements&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;Representing&nbsp;Room&nbsp;Volumes&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Room</span>&nbsp;r&nbsp;<span style="color:blue;">in</span>&nbsp;rooms&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;r.Name&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">GeometryElement</span>&nbsp;geo&nbsp;=&nbsp;r.ClosedShell;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dictionary</span>&lt;<span style="color:blue;">string</span>,&nbsp;<span style="color:blue;">string</span>&gt;&nbsp;param_values
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;GetParamValues(&nbsp;r&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;json&nbsp;=&nbsp;FormatDictAsJson(&nbsp;param_values&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">DirectShape</span>&nbsp;ds&nbsp;=&nbsp;<span style="color:#2b91af;">DirectShape</span>.CreateElement(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;_id_category_for_direct_shape&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ds.ApplicationId&nbsp;=&nbsp;id_addin;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ds.ApplicationDataId&nbsp;=&nbsp;r.UniqueId;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ds.SetShape(&nbsp;geo.ToList&lt;<span style="color:#2b91af;">GeometryObject</span>&gt;()&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ds.get_Parameter(&nbsp;_bip_properties&nbsp;).Set(&nbsp;json&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ds.Name&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Room&nbsp;volume&nbsp;for&nbsp;&quot;</span>&nbsp;+&nbsp;r.Name;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

For the full Visual Studio solution and updates to the code, please refer to
The [RoomVolumeDirectShape GitHub repository](https://github.com/jeremytammik/RoomVolumeDirectShape).


####<a name="8"></a> Sample Model and Results

I tested this in the standard Revit *rac_basic_sample_project.rvt* sample model:

<center>
<img src="img/rac_basic_sample_project.png" alt="Revit Architecture rac_basic_sample_project.rvt" width="380">
</center>

Isolated, the resulting direct shapes look like this:

<center>
<img src="img/rac_basic_sample_project_room_volumes.png" alt="DirectShape elements representing room volumes" width="699">
</center>

####<a name="9"></a> Challenges Encountered Underway

I ran into a couple of issues en route that cost me time to resolve, ever though absolutely trivial, so I'll make a note of them here for my own future reference:

- [Licensing system error 22](#9.1) 
- [Valid direct shape categories](#9.2) 
- [Direct shape phase and visibility](#9.3) 


####<a name="9.1"></a> Licensing System Error 22 

Something happened on my virtual Windows machine, and I saw an error saying:

<pre>
  ---------------------------
  Autodesk Revit 2020
  ---------------------------
  Licensing System Error 22 
  Failed to locate Adls
  ---------------------------
  OK   
  ---------------------------
</pre>

Luckily, a similar issue has already been discussed in the forum thread
on [licensing system error 22 &ndash; failed to locate `Adls`](https://forums.autodesk.com/t5/installation-licensing/error-de-sistema-de-licencias-22-failed-to-locate-adls/td-p/8771037).

The solution described there worked fine in my case as well:

- Run Services.msc
- Check the entry for Autodesk Desktop Licensing Service
- If it is not already running, start the service


####<a name="9.2"></a> Valid Direct Shape Categories 

I had to fiddle a bit choosing which category to use for the `DirectShape` element creation.

The rooms category is not acceptable, generic model and structural framing is.

Attempting to use an invalid category throws an ArgumentException saying, *Element id categoryId may not be used as a DirectShape category.*


####<a name="9.3"></a> Direct Shape Phase and Visibility

Right away after the first trial run, I could see the resulting `DirectShape` elements in RevitLookup, and all their properties looked fine.

However, try as I might, I was unable to see them in the Revit 3D view...

...until I finally flipped through the phases and found the right one.

The model is apparently in a state in which newly created geometry lands in a phase that is not displayed in the default 3D view.


####<a name="10"></a> Cherry BIM Services

Enough on my activities.

Someone else has also been pretty active recently:

[Ninh Truong Huu Ha](https://github.com/haninh2612) of [Cherry BIM Services](http://www.cherrybimservices.com) recently
shared several free Revit add-ins, and also published code for one of them.

Oops, the code has disappeared again from Ninh's GitHub repository; in fact, the whole repository disappeared...

> Inspired by Jeremy Tammik and Harry Mattison who always share their incredible knowledge to the world, I decided from now on, all of my Revit add-ins will be free to use for all Revit users.
One year ago, I had absolutely zero knowledge of the coding world, e.g., C#, Revit API, Visual Studio, etc.
I would never have thought that someday I could have my own Revit add-in published in the Autodesk Store.

- Start from my first add-in: [Batch Rename Revit Type name with Naming convention](https://www.dropbox.com/sh/fs1b60jewyfkdxd/AAArHy7C6Y7edtBGckl2AIeSa?dl=0).
Here is a three-minute [demonstration video](https://youtu.be/n91iyjOALdo).
- [Warning Manager by Cherry BIM Services](https://apps.autodesk.com/RVT/en/Detail/Index?id=7980350830610368901&appLang=en&os=Win64)
- [Auto-generate curtain grids](https://youtu.be/Sacd3K6RBbU) &ndash; [Auto curtain wall Dropbox download](https://www.dropbox.com/sh/rfllne68zjjjq9t/AAA7eLI-p1LqFHkRj3fBlxpza?dl=0)
- [Batch Upgrade models, templates and families](https://youtu.be/rciLWaik2_0)

Many thanks to Ninh for sharing these tools!


####<a name="11"></a> On the Value of the 'Loss Method' Property

Next, let's point out an MEP analysis related question raised and solved by Hanley Deng in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to get the value of the property 'Loss Method'](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-value-of-the-property-quot-loss-method-quot/m-p/8816013):

**Question:** Pipe fittings have a property named "Loss Method".

In the UI, its value is "Use Definition on Type".

In the API, however, the value is a GUID, e.g., "3bf616f9-6b98-4a21-80ff-da1120c8f6d6":

<center>
<img src="img/snoop_loss_method_param_val.png" alt="Loss method parameter property" width="619">
</center>

How can I convert the API GUID value, "3bf616f9-6b98-4a21-80ff-da1120c8f6d6", into the UI value, "Use Definition on Type"?

**Answer:** The loss method can be programmed, so the GUID you see might be something like the add-in identifier, c.f. this discussion on
the [pipe fitting K factor](https://thebuildingcoder.typepad.com/blog/2017/12/pipe-fitting-k-factor-archilab-and-installer.html).

**Response:** Problem solved. This problem is solved in 2 cases:

1. For Pipe fittings, when Loss Method is "Use definition on Type":
In this case, the `parameter.AsString()` value equals the GUID stored in Autodesk.Revit.DB.MEPCalculatationServerInfo.PipeUseDefinitionOnTypeGUID.
In this case, I cannot find the UI display string for it, so I hardcode the UI display string.
2. I all other cases, including other values in Pipe Fittings, and all the values in Duct Fittings, the `ServerName` is the string in the UI display, accessible through the following API call:

<pre class="code">
  Autodesk.Revit.DB.MEPCalculatationServerInfo
    .GetMEPCalculationServerInfo(objFamilyInstance), 
</pre>

Many thanks to Hanley for clarifying this.


####<a name="12"></a> AI-Generated Talking Head Models

Finally, let's close with this impressive demonstration of AI simulated talking head models, presented in the five-minute video 
on [few-shot adversarial learning of realistic neural talking head models](https://youtu.be/p1b5aiTrGzY):

<center>
<iframe width="560" height="315" src="https://www.youtube.com/embed/p1b5aiTrGzY" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>  

