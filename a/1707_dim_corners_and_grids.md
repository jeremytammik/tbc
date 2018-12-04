<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 12243183 [Dimension between walls corners using Revit's API]
  http://forums.autodesk.com/t5/revit-api-forum/dimension-between-walls-corners-using-revit-s-api/m-p/6537043

- email Toshiaki Isezaki Re: Dimension on Revit

- 14840395 [Revit APIでの寸法作成時の参照設定]

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

-->

### Dimension Wall Corners and Grid Intersections

An email from a colleague brought up the topic of dimensioning between grid line intersections, which in turn prompted me to rediscover a quite interesting and fruitful dimensioning question in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160):



<center>
<img src="img/.png" alt="" width="100">
</center>

#### <a name="2"></a> 

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) asks 

**Question:** 

<pre class="code">
<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Reference</span>&gt;&nbsp;GetWallOpenings(&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;wall,&nbsp;<span style="color:#2b91af;">View3D</span>&nbsp;view&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;wall.Document;
&nbsp;&nbsp;<span style="color:#2b91af;">Level</span>&nbsp;level&nbsp;=&nbsp;doc.GetElement(&nbsp;wall.LevelId&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Level</span>;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;elevation&nbsp;=&nbsp;level.Elevation;
&nbsp;&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;c&nbsp;=&nbsp;(&nbsp;wall.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>&nbsp;).Curve;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;wallOrigin&nbsp;=&nbsp;c.GetEndPoint(&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;wallEndPoint&nbsp;=&nbsp;c.GetEndPoint(&nbsp;1&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;wallDirection&nbsp;=&nbsp;wallEndPoint&nbsp;-&nbsp;wallOrigin;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;wallLength&nbsp;=&nbsp;wallDirection.GetLength();
&nbsp;&nbsp;wallDirection&nbsp;=&nbsp;wallDirection.Normalize();
 
&nbsp;&nbsp;<span style="color:#2b91af;">UV</span>&nbsp;offsetOut&nbsp;=&nbsp;_offset&nbsp;*&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UV</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wallDirection.X,&nbsp;wallDirection.Y&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;rayStart&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wallOrigin.X&nbsp;-&nbsp;offsetOut.U,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wallOrigin.Y&nbsp;-&nbsp;offsetOut.V,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;elevation&nbsp;+&nbsp;_offset&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">ReferenceIntersector</span>&nbsp;intersector&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ReferenceIntersector</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;wall.Id,&nbsp;<span style="color:#2b91af;">FindReferenceTarget</span>.Face,&nbsp;view&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">ReferenceWithContext</span>&gt;&nbsp;refs&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;intersector.Find(&nbsp;rayStart,&nbsp;wallDirection&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Reference</span>&gt;&nbsp;faceReferenceList&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Reference</span>&gt;(&nbsp;refs
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where&lt;<span style="color:#2b91af;">ReferenceWithContext</span>&gt;(&nbsp;r&nbsp;=&gt;&nbsp;IsSurface(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;r.GetReference()&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where&lt;<span style="color:#2b91af;">ReferenceWithContext</span>&gt;(&nbsp;r&nbsp;=&gt;&nbsp;r.Proximity
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;&nbsp;wallLength&nbsp;+&nbsp;_offset&nbsp;+&nbsp;_offset&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Select&lt;<span style="color:#2b91af;">ReferenceWithContext</span>,&nbsp;<span style="color:#2b91af;">Reference</span>&gt;(&nbsp;r
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;r.GetReference()&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;faceReferenceList;
}
 
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;test(&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;<span style="color:#2b91af;">ReferenceArray</span>&nbsp;refs&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ReferenceArray</span>();
 
&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;myRef&nbsp;=&nbsp;uidoc.Selection.PickObject(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ObjectType</span>.Element,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;MySelectionFilter(&nbsp;<span style="color:#a31515;">&quot;Walls&quot;</span>&nbsp;),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Select&nbsp;a&nbsp;wall&quot;</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;wall&nbsp;=&nbsp;doc.GetElement(&nbsp;myRef&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Wall</span>;
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Creates&nbsp;an&nbsp;element&nbsp;e&nbsp;from&nbsp;the&nbsp;selected&nbsp;object&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;reference&nbsp;--&nbsp;this&nbsp;will&nbsp;be&nbsp;the&nbsp;wall&nbsp;element</span>
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;myRef&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Creates&nbsp;a&nbsp;selection&nbsp;filter&nbsp;to&nbsp;dump&nbsp;objects&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;in&nbsp;for&nbsp;later&nbsp;selection</span>
&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;selSet&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;();
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Gets&nbsp;the&nbsp;bounding&nbsp;box&nbsp;of&nbsp;the&nbsp;selected&nbsp;wall&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;element&nbsp;picked&nbsp;above</span>
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;bb&nbsp;=&nbsp;e.get_BoundingBox(&nbsp;doc.ActiveView&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;adds&nbsp;a&nbsp;buffer&nbsp;to&nbsp;the&nbsp;bounding&nbsp;box&nbsp;to&nbsp;ensure&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;all&nbsp;elements&nbsp;are&nbsp;contained&nbsp;within&nbsp;the&nbsp;box</span>
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;buffer&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0.1,&nbsp;0.1,&nbsp;0.1&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;creates&nbsp;an&nbsp;ouline&nbsp;based&nbsp;on&nbsp;the&nbsp;boundingbox&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;corners&nbsp;of&nbsp;the&nbsp;panel&nbsp;and&nbsp;adds&nbsp;the&nbsp;buffer</span>
&nbsp;&nbsp;<span style="color:#2b91af;">Outline</span>&nbsp;outline&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Outline</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;bb.Min&nbsp;-&nbsp;buffer,&nbsp;bb.Max&nbsp;+&nbsp;buffer&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;filters&nbsp;the&nbsp;selection&nbsp;by&nbsp;the&nbsp;bounding&nbsp;box&nbsp;of&nbsp;the&nbsp;selected&nbsp;object</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;the&nbsp;&quot;true&quot;&nbsp;statement&nbsp;inverts&nbsp;the&nbsp;selection&nbsp;and&nbsp;selects&nbsp;all&nbsp;other&nbsp;objects</span>
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxIsInsideFilter</span>&nbsp;bbfilter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxIsInsideFilter</span>(&nbsp;outline,&nbsp;<span style="color:blue;">false</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">BuiltInCategory</span>&gt;&nbsp;bcat&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">BuiltInCategory</span>&gt;();
 
&nbsp;&nbsp;<span style="color:green;">//creates&nbsp;a&nbsp;new&nbsp;filtered&nbsp;element&nbsp;collector&nbsp;that&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;filters&nbsp;by&nbsp;the&nbsp;active&nbsp;view&nbsp;settings</span>
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;collector&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;doc.ActiveView.Id&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//collects&nbsp;all&nbsp;objects&nbsp;that&nbsp;pass&nbsp;through&nbsp;the&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;requirements&nbsp;of&nbsp;the&nbsp;bbfilter</span>
&nbsp;&nbsp;collector.WherePasses(&nbsp;bbfilter&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//add&nbsp;all&nbsp;levels&nbsp;and&nbsp;grids&nbsp;to&nbsp;filter&nbsp;--&nbsp;these&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;are&nbsp;filtered&nbsp;out&nbsp;by&nbsp;the&nbsp;viewtemplate,&nbsp;but&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;are&nbsp;nice&nbsp;to&nbsp;have</span>
&nbsp;&nbsp;bcat.Add(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_StructConnections&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//create&nbsp;new&nbsp;multi&nbsp;category&nbsp;filter</span>
&nbsp;&nbsp;<span style="color:#2b91af;">ElementMulticategoryFilter</span>&nbsp;multiCatFilter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementMulticategoryFilter</span>(&nbsp;bcat&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//create&nbsp;new&nbsp;filtered&nbsp;element&nbsp;collector,&nbsp;add&nbsp;the&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;passing&nbsp;levels&nbsp;and&nbsp;grids,&nbsp;then&nbsp;remove&nbsp;them&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;from&nbsp;the&nbsp;selection</span>
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;el&nbsp;<span style="color:blue;">in</span>&nbsp;collector.WherePasses(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;multiCatFilter&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;el.Name.Equals(&nbsp;<span style="color:#a31515;">&quot;EMBEDS&quot;</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;selSet.Add(&nbsp;el.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>[]&nbsp;pts&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>[99];
 
&nbsp;&nbsp;<span style="color:green;">//View3D&nbsp;view&nbsp;=&nbsp;doc.ActiveView&nbsp;as&nbsp;View3D;</span>
&nbsp;&nbsp;<span style="color:#2b91af;">View3D</span>&nbsp;view&nbsp;=&nbsp;Get3dView(&nbsp;doc&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;THIS&nbsp;IS&nbsp;WHERE&nbsp;IT&nbsp;RETURNS&nbsp;THE&nbsp;WALL&nbsp;OPENING&nbsp;REFERENCES.&nbsp;&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;HOWEVER&nbsp;THEY&nbsp;ONLY&nbsp;ARE&nbsp;ABLE&nbsp;TO&nbsp;BE&nbsp;USED&nbsp;FOR&nbsp;DIMENSIONS&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;IF&nbsp;THE&nbsp;OPENING&nbsp;IS&nbsp;CREATED&nbsp;USING&nbsp;A&nbsp;FAMILY&nbsp;SUCH&nbsp;AS&nbsp;A&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;WINDOW&nbsp;OR&nbsp;DOOR.&nbsp;OPENING&nbsp;BY&nbsp;FACE/WALL&nbsp;DOES&nbsp;NOT&nbsp;WORK,&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;EVEN&nbsp;THOUGH&nbsp;IT&nbsp;RETURNS&nbsp;PROPER&nbsp;REFERENCES</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Reference</span>&gt;&nbsp;openings&nbsp;=&nbsp;GetWallOpenings(&nbsp;e&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Wall</span>,&nbsp;view&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;reference&nbsp;<span style="color:blue;">in</span>&nbsp;openings&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;refs.Append(&nbsp;reference&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;REFERE&quot;</span>,&nbsp;refs.Size.ToString()&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;wallLocation&nbsp;=&nbsp;(&nbsp;wall.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationCurve</span>&nbsp;).Curve;
 
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;i&nbsp;=&nbsp;0;
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;ele&nbsp;<span style="color:blue;">in</span>&nbsp;selSet&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;fi&nbsp;=&nbsp;doc.GetElement(&nbsp;ele&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">FamilyInstance</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;reference&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">ScottWilsonVoodooMagic</span>.GetSpecialFamilyReference(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;fi,&nbsp;<span style="color:#2b91af;">ScottWilsonVoodooMagic</span>.<span style="color:#2b91af;">SpecialReferenceType</span>.CenterLR,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;refs.Append(&nbsp;reference&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;pts[i]&nbsp;=&nbsp;(&nbsp;fi.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationPoint</span>&nbsp;).Point;
&nbsp;&nbsp;&nbsp;&nbsp;i++;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;offset&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;0,&nbsp;0,&nbsp;4&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;line&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pts[0]&nbsp;+&nbsp;offset,&nbsp;pts[1]&nbsp;+&nbsp;offset&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;dimension&nbsp;embeds&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Dimension</span>&nbsp;dim&nbsp;=&nbsp;doc.Create.NewDimension(&nbsp;doc.ActiveView,&nbsp;line,&nbsp;refs&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;}
}
</pre>

**Answer:**

<pre class="code">
</pre>


#### <a name="3"></a> Dimension Grids



#### <a name="4"></a> Dimension Detail Lines

14840395 [Revit APIでの寸法作成時の参照設定]

`DimensionBetweenDetailLines`

[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)

[release 2019.0.144.3](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.144.3)


