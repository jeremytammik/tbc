<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- What's New in Revit 2022 Sneak Peek
  https://youtu.be/FjSbv6W6tcg
  Join Senior Revit Product Manager's Sasha Crotty and Harlan Brumm for a Live Preview of What's New in Revit 2022, including their favorite new features.

- normalise arc start and end angle
  How to retrieve startAngle and endAngle of Arc object
  https://forums.autodesk.com/t5/revit-api-forum/how-to-retrieve-startangle-and-endangle-of-arc-object/m-p/10213537#M54910

- the doughnut economic model is gaining official acceptnce, e.g.,
  https://en.wikipedia.org/wiki/Doughnut_(economic_model)
  Amsterdam bet its post-Covid recovery on ‘doughnut’ economics — more cities are now following suit
  https://www.cnbc.com/2021/03/25/amsterdam-brussels-bet-on-doughnut-economics-amid-covid-crisis.html

- create a set of column types from a list of dimensions
  https://forums.autodesk.com/t5/revit-api-forum/create-columns-types/m-p/10181049

- Automatic Column Creation from Imported CAD Drawing
  https://forums.autodesk.com/t5/revit-api-forum/automatic-column-creation-from-imported-cad-drawing/m-p/9648240

- The Object Oriented Guide to Microservices & Serverless Architecture Whitepaper
  https://www.mongodb.com/collateral/download-the-oo-guide-to-microservices-and-serverless-architecture?utm_campaign=stack_ww_dg_flighted_overflowooguide_wp_dev&utm_source=stackoverflow&utm_medium=sponsored_newsletter

- Intriguing new result from the LHCb experiment at CERN
  https://home.cern/news/news/physics/intriguing-new-result-lhcb-experiment-cern
  The LHCb results strengthen hints of a violation of lepton flavour universality
  LHCb (Large Hadron Collider beauty) collaboration
  Today the LHCb experiment at CERN announced new results which, if confirmed, would suggest hints of a violation of the Standard Model of particle physics.
  
  [Standard Model](https://home.cern/science/physics/standard-model)
  The Standard Model explains how the basic building blocks of matter interact, governed by four fundamental forces.
  The theories and discoveries of thousands of physicists since the 1930s have resulted in a remarkable insight into the fundamental structure of matter: everything in the universe is found to be made from a few basic building blocks called fundamental particles, governed by four fundamental forces. Our best understanding of how these particles and three of the forces are related to each other is encapsulated in the Standard Model of particle physics. Developed in the early 1970s, it has successfully explained almost all experimental results and precisely predicted a wide variety of phenomena. Over time and through many experiments, the Standard Model has become established as a well-tested physics theory.
  
  [Standard Model](https://en.wikipedia.org/wiki/Standard_Model)

twitter:

A sneak peek at the new version of Revit coming soon, career opportunities, handling arc angles, solutions for automatic column type creation and instance placement in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://autode.sk/sneakpeek2022

A new version of Revit coming soon, solutions for automatic column type creation and instance placement, handling arc angles, career opportunities and a sustainable economic model
&ndash; What's new in Revit 2022 sneak peek
&ndash; Creating column types from list
&ndash; Normalising arc start and end angle
&ndash; Many exciting opportunities at Autodesk
&ndash; The sustainable doughnut economic model...

linkedin:

A sneak peek at the new version of Revit coming soon, career opportunities, a sustainable economic model, handling arc angles, solutions for automatic column type creation and instance placement in the #RevitAPI 

http://autode.sk/sneakpeek2022

- What's new in Revit 2022 sneak peek
- Creating column types from list
- Normalising arc start and end angle
- Many exciting opportunities at Autodesk
- The sustainable doughnut economic model...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### Sneak Peek, Automatic Columns, Arcs Angles and Careers

A new version of Revit coming soon, solutions for automatic column type creation and instance placement, handling arc angles, career opportunities and a sustainable economic model:

- [What's new in Revit 2022 sneak peek](#2)
- [Creating column types from list](#3)
- [Normalising arc start and end angle](#4)
- [Many exciting opportunities at Autodesk](#5)
- [The sustainable doughnut economic model](#6)


####<a name="2"></a> What's New in Revit 2022 Sneak Peek

Are you interested in the new features and enhancements coming in the upcoming next release of Revit?

Take the opportunity to join the sneak peek on What's New in Revit 2022 later today,
on [April 6, 2021, at 21:30 CET](https://www.timeanddate.com/worldclock/converter.html?iso=20210406T193000&p1=268):

- [What's New in Revit 2022 Sneak Peek](https://youtu.be/FjSbv6W6tcg)

Then, register to join the Autodesk product experts for the full *What's New in Revit 2022* webinar next week,
[April 13, 2021, at 10:00am PT, 1:00pm ET](https://www.timeanddate.com/worldclock/converter.html?iso=20210413T170000&p1=268&p2=tz_pt&p3=tz_et).
You can register here:

- [Link to register for the full *What's New in Revit 2022* webinar on April 13](https://autode.sk/31xUn2g)

Here are some other forward-looking Revit resources:

- [Revit Public Roadmap](https://trello.com/b/ldRXK9Gw/revit-public-roadmap)
- [Application to the Revit Community and the Revit Preview to try new features in advance](https://feedback.autodesk.com/key/LHMJFVHGJK085G2M)
- [Join the Autodesk Product Research Community](https://www.autodeskproductresearch.com/hub)

<center>
<img src="img/2021-04-06_rvt_2022_sneak_peek.png" alt="Revit 2022 sneak peek" title="Revit 2022 sneak peek" width="524"/> <!-- 524 -->
</center>

####<a name="3"></a> Creating Column Types from List

Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas provided
a nice complete solution to create all required column types from a list of rectangular width and height dimensions in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on how to [create columns types](https://forums.autodesk.com/t5/revit-api-forum/create-columns-types/m-p/10181049):

**Question:** I have the family *M_Concrete-Rectangular-Column* and two lists of doubles that represent `b` and `h` of the column.
How can I extract the unique values of the two lists and then create new family types using them?

**Answer:** Here is a solution in VB.NET:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">Private</span>&nbsp;<span style="color:blue;">Class</span>&nbsp;<span style="color:#2b91af;">ColumnType</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Inherits</span>&nbsp;EqualityComparer(<span style="color:blue;">Of</span>&nbsp;ColumnType)
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Private</span>&nbsp;IntD&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>()&nbsp;=&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:blue;">Integer</span>(1)&nbsp;{}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">ReadOnly</span>&nbsp;<span style="color:blue;">Property</span>&nbsp;H&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Get</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;IntD(0)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Get</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Property</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">ReadOnly</span>&nbsp;<span style="color:blue;">Property</span>&nbsp;W&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Get</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;IntD(1)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Get</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Property</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">ReadOnly</span>&nbsp;<span style="color:blue;">Property</span>&nbsp;Name&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Get</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:blue;">CStr</span>(H)&nbsp;&amp;&nbsp;<span style="color:#a31515;">&quot;x&quot;</span>&nbsp;&amp;&nbsp;<span style="color:blue;">CStr</span>(W)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Get</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Property</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Sub</span>&nbsp;<span style="color:blue;">New</span>(D1&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>,&nbsp;D2&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;D1&nbsp;&gt;&nbsp;D2&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IntD&nbsp;=&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:blue;">Integer</span>()&nbsp;{D1,&nbsp;D2}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IntD&nbsp;=&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:blue;">Integer</span>()&nbsp;{D2,&nbsp;D1}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Sub</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Overrides</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;Equals(x&nbsp;<span style="color:blue;">As</span>&nbsp;ColumnType,&nbsp;y&nbsp;<span style="color:blue;">As</span>&nbsp;ColumnType)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Boolean</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;x.H&nbsp;=&nbsp;y.H&nbsp;<span style="color:blue;">AndAlso</span>&nbsp;x.W&nbsp;=&nbsp;y.W
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Overrides</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;GetHashCode(obj&nbsp;<span style="color:blue;">As</span>&nbsp;ColumnType)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;obj.Name.GetHashCode
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Class</span>
 
&nbsp;&nbsp;<span style="color:blue;">Private</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;TObj168(<span style="color:blue;">ByVal</span>&nbsp;commandData&nbsp;<span style="color:blue;">As</span>&nbsp;Autodesk.Revit.UI.ExternalCommandData,
<span style="color:blue;">ByRef</span>&nbsp;message&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">String</span>,&nbsp;<span style="color:blue;">ByVal</span>&nbsp;elements&nbsp;<span style="color:blue;">As</span>&nbsp;Autodesk.Revit.DB.ElementSet)&nbsp;<span style="color:blue;">As</span>&nbsp;Result
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;UIDoc&nbsp;<span style="color:blue;">As</span>&nbsp;UIDocument&nbsp;=&nbsp;commandData.Application.ActiveUIDocument
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;UIDoc&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>&nbsp;<span style="color:blue;">Return</span>&nbsp;Result.Cancelled&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;IntDoc&nbsp;<span style="color:blue;">As</span>&nbsp;Document&nbsp;=&nbsp;UIDoc.Document
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;L1&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Double</span>()&nbsp;=&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:blue;">Double</span>()&nbsp;{100,&nbsp;200,&nbsp;150,&nbsp;500,&nbsp;400,&nbsp;300,&nbsp;250,&nbsp;250}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;L2&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Double</span>()&nbsp;=&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:blue;">Double</span>()&nbsp;{200,&nbsp;200,&nbsp;150,&nbsp;500,&nbsp;400,&nbsp;300,&nbsp;250,&nbsp;250}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;all&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;List(<span style="color:blue;">Of</span>&nbsp;ColumnType)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">For</span>&nbsp;i&nbsp;=&nbsp;0&nbsp;<span style="color:blue;">To</span>&nbsp;L1.Length&nbsp;-&nbsp;1
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;all.Add(<span style="color:blue;">New</span>&nbsp;ColumnType(L1(i),&nbsp;L2(i)))
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Next</span>
&nbsp;&nbsp;&nbsp;&nbsp;all&nbsp;=&nbsp;all.Distinct(<span style="color:blue;">New</span>&nbsp;ColumnType(0,&nbsp;0)).ToList
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;FEC&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;FilteredElementCollector(IntDoc)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;ECF&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Els&nbsp;<span style="color:blue;">As</span>&nbsp;List(<span style="color:blue;">Of</span>&nbsp;FamilySymbol)&nbsp;=&nbsp;FEC.WherePasses(ECF).WhereElementIsElementType.Cast(<span style="color:blue;">Of</span>&nbsp;FamilySymbol).ToList
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">&#39;Use&nbsp;column&nbsp;name&nbsp;here</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Existing&nbsp;<span style="color:blue;">As</span>&nbsp;List(<span style="color:blue;">Of</span>&nbsp;FamilySymbol)&nbsp;=&nbsp;Els.FindAll(<span style="color:blue;">Function</span>(x)&nbsp;x.FamilyName&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Concrete-Rectangular-Column&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;Existing.Count&nbsp;=&nbsp;0&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Result.Cancelled
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;AlreadyExists&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;List(<span style="color:blue;">Of</span>&nbsp;ColumnType)
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;ToBeMade&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;List(<span style="color:blue;">Of</span>&nbsp;ColumnType)
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">For</span>&nbsp;i&nbsp;=&nbsp;0&nbsp;<span style="color:blue;">To</span>&nbsp;all.Count&nbsp;-&nbsp;1
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Ix&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>&nbsp;=&nbsp;i
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;J&nbsp;<span style="color:blue;">As</span>&nbsp;FamilySymbol&nbsp;=&nbsp;Existing.Find(<span style="color:blue;">Function</span>(x)&nbsp;x.Name&nbsp;=&nbsp;all(Ix).Name)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;J&nbsp;<span style="color:blue;">Is</span>&nbsp;<span style="color:blue;">Nothing</span>&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ToBeMade.Add(all(i))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AlreadyExists.Add(all(i))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Next</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;ToBeMade.Count&nbsp;=&nbsp;0&nbsp;<span style="color:blue;">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Result.Cancelled
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Using</span>&nbsp;Tr&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;Transaction(IntDoc,&nbsp;<span style="color:#a31515;">&quot;Make&nbsp;types&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">If</span>&nbsp;Tr.Start&nbsp;=&nbsp;TransactionStatus.Started&nbsp;<span style="color:blue;">Then</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">For</span>&nbsp;i&nbsp;=&nbsp;0&nbsp;<span style="color:blue;">To</span>&nbsp;ToBeMade.Count&nbsp;-&nbsp;1
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Itm&nbsp;=&nbsp;ToBeMade(i)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Et&nbsp;<span style="color:blue;">As</span>&nbsp;ElementType&nbsp;=&nbsp;Existing(0).Duplicate(Itm.Name)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">&#39;Use&nbsp;actual&nbsp;type&nbsp;parameter&nbsp;names</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">&#39;Use&nbsp;GUIDs&nbsp;instead&nbsp;of&nbsp;LookupParameter&nbsp;where&nbsp;possible</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Et.LookupParameter(<span style="color:#a31515;">&quot;h&quot;</span>)?.Set(304.8&nbsp;*&nbsp;Itm.H)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Et.LookupParameter(<span style="color:#a31515;">&quot;b&quot;</span>)?.Set(304.8&nbsp;*&nbsp;Itm.W)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Next</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tr.Commit()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Using</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Result.Succeeded
&nbsp;&nbsp;<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
</pre>

I converted it to C#
and [added it to The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/compare/2021.0.150.21...2021.0.150.22) for
future reference:

<pre class="code">
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">ColumnType</span>&nbsp;:&nbsp;EqualityComparer&lt;ColumnType&gt;
{
&nbsp;&nbsp;<span style="color:blue;">int</span>[]&nbsp;_dim&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:blue;">int</span>[&nbsp;2&nbsp;];
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">int</span>&nbsp;H
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>&nbsp;{&nbsp;<span style="color:blue;">return</span>&nbsp;_dim[&nbsp;0&nbsp;];&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">int</span>&nbsp;W
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>&nbsp;{&nbsp;<span style="color:blue;">return</span>&nbsp;_dim[&nbsp;1&nbsp;];&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;Name
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>&nbsp;{&nbsp;<span style="color:blue;">return</span>&nbsp;H.ToString()&nbsp;+&nbsp;<span style="color:#a31515;">&quot;x&quot;</span>&nbsp;+&nbsp;W.ToString();&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;ColumnType(&nbsp;<span style="color:blue;">int</span>&nbsp;d1,&nbsp;<span style="color:blue;">int</span>&nbsp;d2&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;d1&nbsp;&gt;&nbsp;d2&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_dim&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:blue;">int</span>[]&nbsp;{&nbsp;d1,&nbsp;d2&nbsp;};
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_dim&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:blue;">int</span>[]&nbsp;{&nbsp;d2,&nbsp;d1&nbsp;};
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">override</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;Equals(&nbsp;ColumnType&nbsp;x,&nbsp;ColumnType&nbsp;y&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;x.H&nbsp;==&nbsp;y.H&nbsp;&amp;&amp;&nbsp;x.W&nbsp;==&nbsp;y.W;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">override</span>&nbsp;<span style="color:blue;">int</span>&nbsp;GetHashCode(&nbsp;ColumnType&nbsp;obj&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;obj.Name.GetHashCode();
&nbsp;&nbsp;}
}
 
Result&nbsp;CreateColumnTypes(&nbsp;Document&nbsp;doc&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">int</span>[]&nbsp;L1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:blue;">int</span>[]&nbsp;{&nbsp;100,&nbsp;200,&nbsp;150,&nbsp;500,&nbsp;400,&nbsp;300,&nbsp;250,&nbsp;250&nbsp;};
&nbsp;&nbsp;<span style="color:blue;">int</span>[]&nbsp;L2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:blue;">int</span>[]&nbsp;{&nbsp;200,&nbsp;200,&nbsp;150,&nbsp;500,&nbsp;400,&nbsp;300,&nbsp;250,&nbsp;250&nbsp;};
 
&nbsp;&nbsp;List&lt;ColumnType&gt;&nbsp;all&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;ColumnType&gt;();
 
&nbsp;&nbsp;<span style="color:blue;">for</span>(&nbsp;<span style="color:blue;">int</span>&nbsp;i&nbsp;=&nbsp;0;&nbsp;i&nbsp;&lt;&nbsp;L1.Length;&nbsp;++i&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;all.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;ColumnType(&nbsp;L1[&nbsp;i&nbsp;],&nbsp;L2[&nbsp;i&nbsp;]&nbsp;)&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;all&nbsp;=&nbsp;all.Distinct(&nbsp;<span style="color:blue;">new</span>&nbsp;ColumnType(&nbsp;0,&nbsp;0&nbsp;)&nbsp;).ToList();
 
&nbsp;&nbsp;FilteredElementCollector&nbsp;symbols
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;FilteredElementCollector(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;BuiltInCategory.OST_StructuralColumns&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Column&nbsp;name&nbsp;to&nbsp;use</span>
 
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;column_name&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Concrete-Rectangular-Column&quot;</span>;
 
&nbsp;&nbsp;IEnumerable&lt;FamilySymbol&gt;&nbsp;existing&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;symbols.Cast&lt;FamilySymbol&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where&lt;FamilySymbol&gt;(&nbsp;x&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;x.FamilyName.Equals(&nbsp;column_name&nbsp;)&nbsp;);&nbsp;
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;0&nbsp;==&nbsp;existing.Count()&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;Result.Cancelled;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;List&lt;ColumnType&gt;&nbsp;AlreadyExists&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;ColumnType&gt;();
&nbsp;&nbsp;List&lt;ColumnType&gt;&nbsp;ToBeMade&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;List&lt;ColumnType&gt;();
 
&nbsp;&nbsp;<span style="color:blue;">for</span>(&nbsp;<span style="color:blue;">int</span>&nbsp;i&nbsp;=&nbsp;0;&nbsp;i&nbsp;&lt;&nbsp;all.Count&nbsp;;&nbsp;++i&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;FamilySymbol&nbsp;fs&nbsp;=&nbsp;existing.FirstOrDefault(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x&nbsp;=&gt;&nbsp;x.Name&nbsp;==&nbsp;all[&nbsp;i&nbsp;].Name&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;fs&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ToBeMade.Add(&nbsp;all[&nbsp;i&nbsp;]&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AlreadyExists.Add(&nbsp;all[&nbsp;i&nbsp;]&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ToBeMade.Count&nbsp;==&nbsp;0&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;Result.Cancelled;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;Transaction&nbsp;tx&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Transaction(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Make&nbsp;types&quot;</span>&nbsp;)&nbsp;==&nbsp;TransactionStatus.Started&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FamilySymbol&nbsp;first&nbsp;=&nbsp;existing.First();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;ColumnType&nbsp;ct&nbsp;<span style="color:blue;">in</span>&nbsp;ToBeMade&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElementType&nbsp;et&nbsp;=&nbsp;first.Duplicate(&nbsp;ct.Name&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Use&nbsp;actual&nbsp;type&nbsp;parameter&nbsp;names</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Use&nbsp;GUIDs&nbsp;instead&nbsp;of&nbsp;LookupParameter&nbsp;where&nbsp;possible</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;et.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;h&quot;</span>&nbsp;).Set(&nbsp;304.8&nbsp;*&nbsp;ct.H&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;et.LookupParameter(&nbsp;<span style="color:#a31515;">&quot;b&quot;</span>&nbsp;).Set(&nbsp;304.8&nbsp;*&nbsp;ct.W&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tx.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;Result.Succeeded;
}
</pre>

An interesting aspect here is the notion of units.
If we were comparing internal units (fractions of feet), we could not have used the integer for comparison.
However, since we assume it is mm and column sizes are rounded, we can use them easily.

So, if we take the smallest measurement of length, then we usually have less problems.
In an imperial example, if we had used inches, then we can compare an integer of 6 inches rather than a double 0.5 ft.
I guess if it is 6.5 inches we are stuck.
However, the mm is the smallest unit we generally work with (unless we are talking paint), so can represent smaller fractions of something as whole numbers.

It is what it is.

It would be interesting to explore intelligent rounding and unit dependent real-number fuzz.

One can well consider 1 mm the minimal metric unit in Revit, since it does not (or hardly) support smaller lengths.

For imperial sizes, I guess I would go for 1/16 of an inch.

Depending on what units the model happens to prefer, one could select one or the other dynamically and implement an rounding algorithm that adapts accordingly.

Talking about the creation of columns and column types, let's add a pointer to another related thread
on [automatic column creation from imported CAD drawing](https://forums.autodesk.com/t5/revit-api-forum/automatic-column-creation-from-imported-cad-drawing/m-p/9648240):

<center>
<img src="img/place_columns_from_cad_dwg_3.png" alt="Place columns from imported 2D CAD" title="Place columns from imported 2D CAD" width="500"/> <!-- 1202 -->
</center>


####<a name="4"></a> Normalising Arc Start and End Angle

Richard also shared some useful notes on normalising arc start and end angles in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to retrieve startAngle and endAngle of arc object](https://forums.autodesk.com/t5/revit-api-forum/how-to-retrieve-startangle-and-endangle-of-arc-object/m-p/10213537):

What I've noticed recently is that the actual parameters of an arc can be far greater than 2 * pi, so how does this happen?

If you draw a detail arc and drag the ends of the arc one at a time around the circumference, the parameter values will accumulate and become greater than 2 pi (after 1 cycle).
They don't reset; you can effectively wind up the arc parameters this way (I discovered parameter numbers reaching 1000 degrees).
We could not create arcs otherwise since the creation method enforces p0 must be less than p1 and arcs are drawn anti clockwise.
When you cross the 2 pi or 0 boundary, you need p1 not to be starting again from 0, otherwise it would be less than p0 on the other side of the boundary.

So, as @JimJia noted, they are not reliable for arc start end angles (or are if the ends have not been manipulated in such a way).
However, in a way, you can get back to the correct angles between 0 and 2 x pi (since each rotation is a multiple of 2 x pi) i.e.

- Divide by 2 * pi
- Deduct integer part of result
- If this is less than 0 add 1 (to negative number)
- Multiply by 2 * pi

I'm not sure why you would need to do this however.

Other thing I would note is always use `Arc.XDirection` and `Arc.Normal` with `AngleOnPlaneTo` (rather than assuming), since arc can be flipped or rotated.

Also, above was related to winding the arc ends around the circumference in an anti-clockwise direction to increase parameter values.

If user drags ends around in a clockwise direction, then you get negative values for the parameters.
Takes less movement from original by user to go into negative domain, so this is more likely to be seen perhaps.

Many thanks to Richard for these helpful observations!

####<a name="5"></a> Many Exciting Opportunities at Autodesk

Autodesk is offering a many exciting career opportunities in Europe and elsewhere right now:

- Software Engineering Manager &ndash; Krakow, Poland &ndash; Job ID 21WD47267
- Construction Account Executive, Territory &ndash; Sweden, remote &ndash; Job ID 21WD47481
- Territory Account Executive, Construction &ndash; The Netherlands &ndash; Job ID 21WD47524
- Strategic Account Manager Manufacturing &ndash; Munich, Germany or home office &ndash; Job ID 21WD47310
- Principal Software Engineer &ndash; Cambridge, UK &ndash; Job ID 21WD45889

Feel free to ask me for a referral.

Good luck applying for one of them and others all over the world in
the [Autodesk career site](https://www.autodesk.com/careers)!

####<a name="6"></a> The Sustainable Doughnut Economic Model

The [doughnut economic model](https://en.wikipedia.org/wiki/Doughnut_(economic_model)) is
gaining official acceptance.
For instance, [Amsterdam bet its post-Covid recovery on doughnut economics, and more cities are now following suit](https://www.cnbc.com/2021/03/25/amsterdam-brussels-bet-on-doughnut-economics-amid-covid-crisis.html).

> The Doughnut, or Doughnut economics, is a visual framework for sustainable development &ndash; shaped like a doughnut or lifebelt &ndash; combining the concept of planetary boundaries with the complementary concept of social boundaries:

<center>
<img src="img/doughnut_economic_model.jpg" alt="Doughnut economic model" title="Doughnut economic model" width="489"/> <!-- 489 -->
</center>
