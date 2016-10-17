<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---


- why Autodesk has a Labs
  I want to get the word out broadly as to why Autodesk has a Labs. I considered writing a Medium blog article, but rather than create yet another avenue, Dan Zucker suggested that I simply write an article on my blog:
  http://labs.blogs.com/its_alive_in_the_lab/2016/04/why-does-autodesk-have-a-labs.html

- http://blogs.autodesk.com/dalbiminpoi
  It's my pleasure to announce a new blog dedicated to AEC in Italy (and in Italian), administrated by Stefano Toparini, authored by Ilaria lagazio and Stefano :
  "dal BIM in poi" means "Beyond BIM".
  Ilaria and Stefano will post there in order to drive and help adoption of our BIM Solutions in Italy.
  STEFANO TOPARINI
  Stefano si occupa da molti anni di BIM per le Infrastrutture nonché di reti tecnologiche e di gestione del territorio, con esperienze professionali maturate sia in Italia che all'estero. Lavora nella filiale Italiana di Autodesk Inc. da oltre 10 anni dopo esperienze in varie altre aziende di informatica e servizi nel mondo della progettazione e delle pubbliche amministrazioni.
  ILARIA LAGAZIO
  Laureata in Ingegneria Civile e specializzata in Strutture, dopo una breve esperienza nel campo della progettazione si dedica all’industrializzazione dei sistemi edilizi come Building System Development Manager, gestendo il flusso delle informazioni dei componenti edilizi dal modello al cantiere. L’interesse per l’industrializzazione del cantiere e la gestione del dato progettuale la porta ad una esperienza negli Emirati Arabi e dal 2008 in Autodesk , dove oggi ricopre il ruolo di Senior Technical Sales Specialist

Determining Wall Opening Areas per Room #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim @AutodeskLabs

We continue the rather exhaustive exploration of calculating net and gross wall areas per room, and two other announcements, pointers to interesting sources of information
&ndash; Why Autodesk has a Labs
&ndash; Dal BIM in poi &ndash; Italian BIM
&ndash; Determining wall opening areas per room
&ndash; Håvard's SpatialElementGeometryCalculator enhancement
&ndash; External command mainline
&ndash; Test run...

-->

### Determining Wall Opening Areas per Room

We continue the rather exhaustive exploration of calculating net and gross wall areas per room, and two other announcements, pointers to interesting sources of information besides The Building Coder:

- [Why Autodesk has a Labs](#2)
- [Dal BIM in poi &ndash; Italian BIM](#3)
- [Determining wall opening areas per room](#4)
- [Håvard's SpatialElementGeometryCalculator enhancement](#5)
- [External command mainline](#6)
- [Test run](#7)


#### <a name="2"></a>Why Autodesk has a Labs

I hope you know and appreciate that Autodesk has a Labs, and why.

If not, please refer to Scott Sheppard's
explanation [why Autodesk has a Labs](http://labs.blogs.com/its_alive_in_the_lab/2016/04/why-does-autodesk-have-a-labs.html).

#### <a name="3"></a>Dal BIM in Poi &ndash; Italian BIM

It is my pleasure to announce a new Italian AEC and BIM blog,
[dal BIM in poi](http://blogs.autodesk.com/dalbiminpoi), by Ilaria Lagazio and Stefano Toparini.

'Dal BIM in poi' means 'Beyond BIM'.

*Stefano si occupa da molti anni di BIM per le Infrastrutture nonché di reti tecnologiche e di gestione del territorio, con esperienze professionali maturate sia in Italia che all'estero. Lavora nella filiale Italiana di Autodesk Inc. da oltre 10 anni dopo esperienze in varie altre aziende di informatica e servizi nel mondo della progettazione e delle pubbliche amministrazioni.*

Stefano has been working with BIM for Infrastructure and GIS for many years, with professional experience both in Italy and abroad. He has been with the Italian subsidiary of Autodesk Inc. for over 10 years after experiences in several other computer companies and services in the design and public administration domains.

*Ilaria e Laureata in Ingegneria Civile e specializzata in Strutture, dopo una breve esperienza nel campo della progettazione si dedica all’industrializzazione dei sistemi edilizi come Building System Development Manager, gestendo il flusso delle informazioni dei componenti edilizi dal modello al cantiere. L’interesse per l’industrializzazione del cantiere e la gestione del dato progettuale la porta ad una esperienza negli Emirati Arabi e dal 2008 in Autodesk, dove oggi ricopre il ruolo di Senior Technical Sales Specialist.*

Ilaria graduated in civil engineering and specialised in structures. After a short experience in design she worked as Building System Development Manager, managing the information flow from BIM to construction site. Her interest in efficient construction site and project management took her to the UAE, and from 2008 onwards to Autodesk in the role as Senior Technical Sales Specialist.

I wish the new blog and all its readers all the best and many exciting stories!


#### <a name="4"></a>Determining Wall Opening Areas per Room

As I pointed out last week in the preceding article on this topic, Håvard Dagsvik of the newly renamed Scandinavian AEC and BIM company [Symetri](http://www.symetri.com), previously CAD-Q, invested significant effort in enhancing our joint efforts to determine wall opening areas per room.

Here are links to the previous discussions:

- [Calculating gross and net wall areas](http://thebuildingcoder.typepad.com/blog/2015/03/calculating-gross-and-net-wall-areas.html)
- [IFCExportUtils to determine the door and window area](http://thebuildingcoder.typepad.com/blog/2015/03/ifcexportutils-methods-determine-door-and-window-area.html)
- [Determining wall cut area for a specific room](http://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-cut-area-for-a-specific-room.html)

The results are captured in these two Revit add-in projects and GitHub repositories:

- [SpatialElementGeometryCalculator](https://github.com/jeremytammik/SpatialElementGeometryCalculator)
- [ExporterIfcUtilsWinArea](https://github.com/jeremytammik/ExporterIfcUtilsWinArea)


#### <a name="5"></a>Håvard's SpatialElementGeometryCalculator Enhancement

I integrated Håvard's final running test code into
the [SpatialElementGeometryCalculator project](https://github.com/jeremytammik/SpatialElementGeometryCalculator) and
am very happy to present the following results that you can now reproduce yourself as well.

In Håvard's own words:

Here it is.

Simplified and cleaned up.

I couldn’t use the existing dictionary so I implemented a small class `SpatialBoundaryCache` to cache the core spatial data.

I demonstrate how you can structure and present the resulting data, areas square metres, in three different ways:

- By room
- By wall type
- By wall material

Material is my choice because I need to know if the surface is plaster or concrete &ndash; note that walls can have different layers on each side.

Just as before, I use both geometric analysis based on the wall solids as well as the IFC utility classes for the calculations.

I removed the `GetAreaFromFamilyParameters` method because it is somewhat unreliable and I already use the IFC utils instead.

I also used your `IsInRoom` method instead of mine.

In the long run, I think all of it will be just solid intersections also IsInRoom.

Both stacked walls and embedded curtain walls are handled, though probably not yet an embedded curtain wall within a stacked wall.

That could be solved following the same solid intersection logic.

I am quite sure there is still room for improvement in the opening handler.

Take a look and see what you make of it &nbsp; :-)

I am attaching [the sample file](zip/SpatialElementGeometryCalculatorTest.rvt) that I worked with as well.

Many thanks to Håvard for his research, hard work, and sharing this!

I integrated Håvard's functionality in the
public  [SpatialElementGeometryCalculator GitHub project](https://github.com/jeremytammik/SpatialElementGeometryCalculator),
in [release 2016.0.0.3](https://github.com/jeremytammik/SpatialElementGeometryCalculator/releases/tag/2016.0.0.3).


#### <a name="6"></a>External Command Mainline

The main execute method demonstrates how to:

- Retrieve the rooms from the model
- Run the `SpatialElementGeometryCalculator` calculator
- Retrieve the results
- Extract, sort and report the data

Here is the complete implementation:

<pre class="code">
<span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; <span class="teal">ElementSet</span> elements )
{
&nbsp; <span class="teal">UIApplication</span> uiapp = commandData.Application;
&nbsp; <span class="teal">Document</span> doc = uiapp.ActiveUIDocument.Document;
&nbsp; <span class="teal">Result</span> rc;
&nbsp;
&nbsp; <span class="blue">try</span>
&nbsp; {
&nbsp; &nbsp; <span class="teal">SpatialElementBoundaryOptions</span> sebOptions
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">SpatialElementBoundaryOptions</span> {
&nbsp; &nbsp; &nbsp; &nbsp; SpatialElementBoundaryLocation
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">SpatialElementBoundaryLocation</span>.Finish };
&nbsp;
&nbsp; &nbsp; <span class="teal">IEnumerable</span>&lt;<span class="teal">Element</span>&gt; rooms
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">SpatialElement</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; .Where&lt;<span class="teal">Element</span>&gt;( e =&gt; (e <span class="blue">is</span> <span class="teal">Room</span>) );
&nbsp;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="blue">string</span>&gt; compareWallAndRoom = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="blue">string</span>&gt;();
&nbsp; &nbsp; <span class="teal">OpeningHandler</span> openingHandler = <span class="blue">new</span> <span class="teal">OpeningHandler</span>();
&nbsp;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">SpatialBoundaryCache</span>&gt; lstSpatialBoundaryCache
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">SpatialBoundaryCache</span>&gt;();
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Room</span> room <span class="blue">in</span> rooms )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( room == <span class="blue">null</span> ) <span class="blue">continue</span>;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( room.Location == <span class="blue">null</span> ) <span class="blue">continue</span>;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( room.Area.Equals( 0 ) ) <span class="blue">continue</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; Autodesk.Revit.DB.<span class="teal">SpatialElementGeometryCalculator</span> calc =
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">new</span> Autodesk.Revit.DB.<span class="teal">SpatialElementGeometryCalculator</span>(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; doc, sebOptions );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">SpatialElementGeometryResults</span> results
&nbsp; &nbsp; &nbsp; &nbsp; = calc.CalculateSpatialElementGeometry(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; room );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">Solid</span> roomSolid = results.GetGeometry();
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Face</span> face <span class="blue">in</span> results.GetGeometry().Faces )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">SpatialElementBoundarySubface</span>&gt; boundaryFaceInfo
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = results.GetBoundaryFaceInfo( face );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="blue">var</span> spatialSubFace <span class="blue">in</span> boundaryFaceInfo )
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( spatialSubFace.SubfaceType
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; != <span class="teal">SubfaceType</span>.Side )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">continue</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">SpatialBoundaryCache</span> spatialData
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">SpatialBoundaryCache</span>();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Wall</span> wall = doc.GetElement( spatialSubFace
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .SpatialBoundaryElement.HostElementId )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">Wall</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( wall == <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">continue</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">WallType</span> wallType = doc.GetElement(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; wall.GetTypeId() ) <span class="blue">as</span> <span class="teal">WallType</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( wallType.Kind == <span class="teal">WallKind</span>.Curtain )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Leave out, as curtain walls are not painted.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">LogCreator</span>.LogEntry( <span class="maroon">&quot;WallType is CurtainWall&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">continue</span>;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">HostObject</span> hostObject = wall <span class="blue">as</span> <span class="teal">HostObject</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">ElementId</span>&gt; insertsThisHost
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; = hostObject.FindInserts(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">true</span>, <span class="blue">false</span>, <span class="blue">true</span>, <span class="blue">true</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">double</span> openingArea = 0;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">ElementId</span> idInsert <span class="blue">in</span> insertsThisHost )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">string</span> countOnce = room.Id.ToString()
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + wall.Id.ToString() + idInsert.ToString();
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">if</span>( !compareWallAndRoom.Contains( countOnce ) )
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">Element</span> elemOpening = doc.GetElement(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; idInsert ) <span class="blue">as</span> <span class="teal">Element</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; openingArea = openingArea
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; + openingHandler.GetOpeningArea(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; wall, elemOpening, room, roomSolid );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; compareWallAndRoom.Add( countOnce );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="green">// Cache SpatialElementBoundarySubface info.</span>
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; spatialData.roomName = room.Name;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; spatialData.idElement = wall.Id;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; spatialData.idMaterial = spatialSubFace
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .GetBoundingElementFace().MaterialElementId;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; spatialData.dblNetArea = <span class="teal">Util</span>.sqFootToSquareM(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; spatialSubFace.GetSubface().Area );
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; spatialData.dblOpeningArea = <span class="teal">Util</span>.sqFootToSquareM(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; openingArea );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lstSpatialBoundaryCache.Add( spatialData );
&nbsp;
&nbsp; &nbsp; &nbsp; &nbsp; } <span class="green">// end foreach subface from which room bounding elements are derived</span>
&nbsp;
&nbsp; &nbsp; &nbsp; } <span class="green">// end foreach Face</span>
&nbsp;
&nbsp; &nbsp; } <span class="green">// end foreach Room</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="blue">string</span>&gt; t = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="blue">string</span>&gt;();
&nbsp;
&nbsp; &nbsp; <span class="teal">List</span>&lt;<span class="teal">SpatialBoundaryCache</span>&gt; groupedData
&nbsp; &nbsp; &nbsp; = SortByRoom( lstSpatialBoundaryCache );
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">SpatialBoundaryCache</span> sbc <span class="blue">in</span> groupedData )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; t.Add( sbc.roomName
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;; all wall types and materials: &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + sbc.AreaReport );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">Util</span>.InfoMsg2( <span class="maroon">&quot;Total Net Area in m2 by Room&quot;</span>,
&nbsp; &nbsp; &nbsp; <span class="blue">string</span>.Join(System.<span class="teal">Environment</span>.NewLine, t ) );
&nbsp;
&nbsp; &nbsp; t.Clear();
&nbsp;
&nbsp; &nbsp; groupedData = SortByRoomAndWallType(
&nbsp; &nbsp; &nbsp; lstSpatialBoundaryCache );
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">SpatialBoundaryCache</span> sbc <span class="blue">in</span> groupedData )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Element</span> elemWall = doc.GetElement(
&nbsp; &nbsp; &nbsp; &nbsp; sbc.idElement ) <span class="blue">as</span> <span class="teal">Element</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; t.Add( sbc.roomName + <span class="maroon">&quot;; &quot;</span> + elemWall.Name
&nbsp; &nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;(&quot;</span> + sbc.idElement.ToString() + <span class="maroon">&quot;): &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + sbc.AreaReport );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">Util</span>.InfoMsg2( <span class="maroon">&quot;Net Area in m2 by Wall Type&quot;</span>,
&nbsp; &nbsp; &nbsp; <span class="blue">string</span>.Join( System.<span class="teal">Environment</span>.NewLine, t ) );
&nbsp;
&nbsp; &nbsp; t.Clear();
&nbsp;
&nbsp; &nbsp; groupedData = SortByRoomAndMaterial(
&nbsp; &nbsp; &nbsp; lstSpatialBoundaryCache );
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">SpatialBoundaryCache</span> sbc <span class="blue">in</span> groupedData )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">string</span> materialName
&nbsp; &nbsp; &nbsp; &nbsp; = (sbc.idMaterial == <span class="teal">ElementId</span>.InvalidElementId)
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ? <span class="blue">string</span>.Empty
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; : doc.GetElement( sbc.idMaterial ).Name;
&nbsp;
&nbsp; &nbsp; &nbsp; t.Add( sbc.roomName + <span class="maroon">&quot;; &quot;</span> + materialName + <span class="maroon">&quot;: &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + sbc.AreaReport );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">Util</span>.InfoMsg2(
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Net Area in m2 by Outer Layer Material&quot;</span>,
&nbsp; &nbsp; &nbsp; <span class="blue">string</span>.Join( System.<span class="teal">Environment</span>.NewLine, t ) );
&nbsp;
&nbsp; &nbsp; rc = <span class="teal">Result</span>.Succeeded;
&nbsp; }
&nbsp; <span class="blue">catch</span>( <span class="teal">Exception</span> ex )
&nbsp; {
&nbsp; &nbsp; <span class="teal">TaskDialog</span>.Show( <span class="maroon">&quot;Room Boundaries&quot;</span>,
&nbsp; &nbsp; &nbsp; ex.Message + <span class="maroon">&quot;\r\n&quot;</span> + ex.StackTrace );
&nbsp;
&nbsp; &nbsp; rc = <span class="teal">Result</span>.Failed;
&nbsp; }
&nbsp; <span class="blue">return</span> rc;
}
&nbsp;
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
<span class="gray">///</span><span class="green"> Convert square feet to square meters</span>
<span class="gray">///</span><span class="green"> with two decimal places precision.</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
<span class="blue">static</span> <span class="blue">double</span> SqFootToSquareM( <span class="blue">double</span> sqFoot )
{
&nbsp; <span class="blue">return</span> <span class="teal">Math</span>.Round( sqFoot * 0.092903, 2 );
}
&nbsp;
<span class="teal">List</span>&lt;<span class="teal">SpatialBoundaryCache</span>&gt; SortByRoom(
&nbsp; <span class="teal">List</span>&lt;<span class="teal">SpatialBoundaryCache</span>&gt; lstRawData )
{
&nbsp; <span class="blue">var</span> sortedCache
&nbsp; &nbsp; = <span class="blue">from</span> rawData <span class="blue">in</span> lstRawData
&nbsp; &nbsp; &nbsp; <span class="blue">group</span> rawData <span class="blue">by</span> <span class="blue">new</span> { room = rawData.roomName }
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">into</span> sortedData
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">select</span> <span class="blue">new</span> <span class="teal">SpatialBoundaryCache</span>()
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; roomName = sortedData.Key.room,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; idElement = <span class="teal">ElementId</span>.InvalidElementId,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dblNetArea = sortedData.Sum( x =&gt; x.dblNetArea ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dblOpeningArea = sortedData.Sum(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; y =&gt; y.dblOpeningArea ),
&nbsp; &nbsp; &nbsp; &nbsp; };
&nbsp;
&nbsp; <span class="blue">return</span> sortedCache.ToList();
}
&nbsp;
<span class="teal">List</span>&lt;<span class="teal">SpatialBoundaryCache</span>&gt; SortByRoomAndWallType(
&nbsp; <span class="teal">List</span>&lt;<span class="teal">SpatialBoundaryCache</span>&gt; lstRawData )
{
&nbsp; <span class="blue">var</span> sortedCache
&nbsp; &nbsp; = <span class="blue">from</span> rawData <span class="blue">in</span> lstRawData
&nbsp; &nbsp; &nbsp; <span class="blue">group</span> rawData <span class="blue">by</span> <span class="blue">new</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; room = rawData.roomName,
&nbsp; &nbsp; &nbsp; &nbsp; wallid = rawData.idElement
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">into</span> sortedData
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">select</span> <span class="blue">new</span> <span class="teal">SpatialBoundaryCache</span>()
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; roomName = sortedData.Key.room,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; idElement = sortedData.Key.wallid,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dblNetArea = sortedData.Sum( x =&gt; x.dblNetArea ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dblOpeningArea = sortedData.Sum(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; y =&gt; y.dblOpeningArea ),
&nbsp; &nbsp; &nbsp; &nbsp; };
&nbsp;
&nbsp; <span class="blue">return</span> sortedCache.ToList();
}
&nbsp;
<span class="teal">List</span>&lt;<span class="teal">SpatialBoundaryCache</span>&gt; SortByRoomAndMaterial(
&nbsp; <span class="teal">List</span>&lt;<span class="teal">SpatialBoundaryCache</span>&gt; lstRawData )
{
&nbsp; <span class="blue">var</span> sortedCache
&nbsp; &nbsp; = <span class="blue">from</span> rawData <span class="blue">in</span> lstRawData
&nbsp; &nbsp; &nbsp; <span class="blue">group</span> rawData <span class="blue">by</span> <span class="blue">new</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; room = rawData.roomName,
&nbsp; &nbsp; &nbsp; &nbsp; mid = rawData.idMaterial
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">into</span> sortedData
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">select</span> <span class="blue">new</span> <span class="teal">SpatialBoundaryCache</span>()
&nbsp; &nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; roomName = sortedData.Key.room,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; idMaterial = sortedData.Key.mid,
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dblNetArea = sortedData.Sum( x =&gt; x.dblNetArea ),
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; dblOpeningArea = sortedData.Sum(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; y =&gt; y.dblOpeningArea ),
&nbsp; &nbsp; &nbsp; &nbsp; };
&nbsp;
&nbsp; <span class="blue">return</span> sortedCache.ToList();
}
</pre>


#### <a name="7"></a>Test Run

Here are the results from a test run on the sample model provided:

The simple test model looks like this in plan view:

<center>
<img src="img/SpatialElementGeometryCalculator1Test.png" alt="Test model plan view" width="400">
</center>

In 3D view, you can see the stacked wall and the opening spanning several different spaces:

<center>
<img src="img/SpatialElementGeometryCalculator2Test3d.png" alt="Test model 3D view" width="400">
</center>

The results sorted by room, wall type and material are presented in three sequential task dialogues.

The first one, by room, is the simplest:

<center>
<img src="img/SpatialElementGeometryCalculator6AreaByRoom2.png" alt="Net, opening and gross area by room" width="366">
</center>

Each room is bounded by several different wall types:

<center>
<img src="img/SpatialElementGeometryCalculator7AreaByWallType2.png" alt="Net, opening and gross area by wall type" width="446">
</center>

Finally, here are the areas grouped by material:

<center>
<img src="img/SpatialElementGeometryCalculator8AreaByMaterial2.png" alt="Net, opening and gross area by material" width="389">
</center>

For the sake of the search engines and legibility, here are the same results in text form as well, as reported on the Visual Studio debug console:

<pre>
Total Net Area in m2 by Room

Room 3; all wall types and materials: net 150; opening 9.2; gross 159.2
Room 5; all wall types and materials: net 42; opening 3.97; gross 45.97
Room 6; all wall types and materials: net 120; opening 7; gross 127
Room 7; all wall types and materials: net 120; opening 2; gross 122

Net Area in m2 by Wall Type

Room 3; Exterior - Block on Mtl. Stud(308814): net 30; opening 0; gross 30
Room 3; CW 102-85-215p(309825): net 45; opening 4; gross 49
Room 3; Wall4(310052): net 30; opening 0; gross 30
Room 3; Wall1(308815): net 45; opening 5.2; gross 50.2
Room 5; Wall1(308815): net 9; opening 1.97; gross 10.97
Room 5; Exterior - Block on Mtl. Stud(311213): net 3.6; opening 0.9; gross 4.5
Room 5; Exterior - Brick on Mtl. Stud(311214): net 8.4; opening 1.1; gross 9.5
Room 5; Wall1(310409): net 9; opening 0; gross 9
Room 5; Generic - 225mm Masonry(308816): net 12; opening 0; gross 12
Room 6; Exterior - Block on Mtl. Stud(308814): net 30; opening 0; gross 30
Room 6; Wall1(308815): net 30; opening 3; gross 33
Room 6; Generic - 225mm Masonry(308816): net 30; opening 2; gross 32
Room 6; Wall3(308817): net 30; opening 2; gross 32
Room 7; Exterior - Block on Mtl. Stud(308814): net 30; opening 0; gross 30
Room 7; Wall3(308817): net 30; opening 2; gross 32
Room 7; Generic - 225mm Masonry(308816): net 30; opening 0; gross 30
Room 7; Generic - 225mm Masonry(309758): net 30; opening 0; gross 30

Net Area in m2 by Outer Layer Material

Room 3; Gypsum Wall Board: net 75; opening 4; gross 79
Room 3; Default Wall: net 75; opening 5.2; gross 80.2
Room 5; Default Wall: net 18; opening 1.97; gross 19.97
Room 5; Gypsum Wall Board: net 12; opening 2; gross 14
Room 5; Concrete Masonry Units: net 12; opening 0; gross 12
Room 6; Gypsum Wall Board: net 30; opening 0; gross 30
Room 6; Default Wall: net 60; opening 5; gross 65
Room 6; Concrete Masonry Units: net 30; opening 2; gross 32
Room 7; Gypsum Wall Board: net 30; opening 0; gross 30
Room 7; Default Wall: net 30; opening 2; gross 32
Room 7; Concrete Masonry Units: net 60; opening 0; gross 60
</pre>

Wonderful, isn't it?

Thank you again, Håvard!


#### <a name="8"></a>Addendum

[Zhbing0322](https://github.com/zhbing0322) added
a [comment](https://github.com/jeremytammik/SpatialElementGeometryCalculator/commit/18ebcd1283bf64a595505871987c3089d53037e6#commitcomment-19391593) on
a [commit](https://github.com/jeremytammik/SpatialElementGeometryCalculator/commit/18ebcd1283bf64a595505871987c3089d53037e6):

> The result of `spatialSubFace.GetSubface().Area` is the Gross area of the Wall in a room.
