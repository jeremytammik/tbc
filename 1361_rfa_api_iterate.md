<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- change selected instance type
  email [Revit API]
  taking a second look at this question, i note that most of what the developer needs is already implemented in the external command Lab3_4_ChangeSelectedInstanceType in the module https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs3.cs

- IterateOverCollector
  email [Regarding [CaseNo:11090027.] ElementIdを取得する方法] Ryuji Ogasawara 

- creating an electrical family
  email [RFA API (especially in Electrical content context)]

#dotnet #csharp #geometry
#fsharp #dynamobim #python
#grevit
#responsivedesign #typepad
#ah8 #augi #au2015 #dotnet #dynamobim
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon #revitapi #3dwebcoder
#javascript
#au2015 #rtceur
#RestSharp #restapi
#au2015
#mongoosejs #mongodb #nodejs
#au2015 #rtceur

Revit API, Jeremy Tammik, akn_include

Change Type, Iterate Elements, Create Family #revitapi #bim #aec #3dwebcoder #adsk #adskdevnetwrk

Let's wrap up this hectic week with a couple more recently answered issues and one non-Revit note
&ndash; Changing element type
&ndash; Iterating over elements
&ndash; Creating an electrical family
&ndash; La costola di Adamo...

-->

### Change Type, Iterate Elements, Create Family

Let's wrap up this hectic week with a couple more recently answered issues and one non-Revit note:

- [Changing element type](#2)
- [Iterating over elements](#3)
- [Creating an electrical family](#4)
- [La costola di Adamo](#5)



#### <a name="2"></a>Changing Element Type

**Question:**
I am programing an application that basically makes three simple steps. Its main purpose is to easily change family types.

You select a Revit view. The application retrieves all the families and types in the view.
You select a family and it shows you the different types.
Finally it displays two columns A and B. In Column A you select the type of family you have in the view and want to be substituted.
In Column B you select the new one.
You click apply and the change is done.

The problem is that when I implement this in Revit, some families do not show their family names or types.

What do you think I am  missing?

**Answer:**
First of all, most questions like this have already been answered. Make sure you perform some Internet searches, check
[The Building Coder topic groups](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5), look through
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) etc.

Note that the `Category` property is not always implemented on families, so you may have to use `FamilyCategory` instead, or the category of the family's first symbol.

The thread
on [extracting family name and type from object](http://forums.autodesk.com/t5/revit-api/extracting-family-name-and-type-from-object/m-p/3093998) addresses a similar topic.

Taking a deeper look at your question, I believe that almost everything you need is already implemented in the external command Lab3_4_ChangeSelectedInstanceType of
the [ADN Xtra Labs](https://github.com/jeremytammik/AdnRevitApiLabsXtra) module [Labs3.cs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs3.cs).

Here are links to two more related discussions for you,
on [changing an element type](http://thebuildingcoder.typepad.com/blog/2010/07/change-element-type.html) and [changing the type of many instances](http://thebuildingcoder.typepad.com/blog/2011/08/changing-the-type-of-many-instances.html).



#### <a name="3"></a>Iterating over Elements

All too many people make use of filtered element collectors like this:

<pre class="code">
&nbsp; <span class="teal">FilteredElementCollector</span> collector
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc );
&nbsp;
&nbsp; collector.OfClass( <span class="blue">typeof</span>( <span class="teal">Family</span> ) ).ToElements();
&nbsp;
&nbsp; <span class="teal">IEnumerable</span>&lt;<span class="teal">Family</span>&gt; nestedFamilies
&nbsp; &nbsp; = collector.Cast&lt;<span class="teal">Family</span>&gt;();
&nbsp;
&nbsp; <span class="teal">String</span> str = <span class="maroon">&quot;&quot;</span>;
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">Family</span> f <span class="blue">in</span> nestedFamilies )
&nbsp; {
&nbsp; &nbsp; str = str + f.Name + <span class="maroon">&quot;\n&quot;</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">ElementId</span> symbolId <span class="blue">in</span>
&nbsp; &nbsp; &nbsp; f.GetFamilySymbolIds() )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="teal">Element</span> symbolElem = doc.GetElement(
&nbsp; &nbsp; &nbsp; &nbsp; symbolId );
&nbsp;
&nbsp; &nbsp; &nbsp; str = str + <span class="maroon">&quot; family type&#65306; &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + symbolElem.Name + <span class="maroon">&quot;\n&quot;</span>;
&nbsp; &nbsp; }
&nbsp; }
</pre>

Please do desist.

I have explained a couple of hundred times now that this can be significantly **simplified**, **shortened** and made **more efficient** all at the same time, e.g., in my explanation of possibilities
for [FindElement and collector optimisation](http://thebuildingcoder.typepad.com/blog/2012/09/findelement-and-collector-optimisation.html).

You can iterate directly over the filtered element collector.

In general, there is no need to create a copy of it.

Calling ToElements creates a copy, allocating space and wasting both memory and time.

There is no need for a cast either, since foreach can do that for you automatically.

Then the snippet above can be cut down to something like this:

<pre class="code">
&nbsp; <span class="teal">FilteredElementCollector</span> families
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">Family</span> ) );
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">Family</span> f <span class="blue">in</span> families )
&nbsp; {
&nbsp; &nbsp; str = str + f.Name + <span class="maroon">&quot;\n&quot;</span>;
&nbsp;
&nbsp; &nbsp; <span class="green">// ...</span>
&nbsp; }
</pre>

For future reference, I added this example code snippet
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
in the method `IterateOverCollector` in the
module [CmdCollectorPerformance.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCollectorPerformance.cs),
currently at line 299.


#### <a name="4"></a>Creating an Electrical Family

**Question:**
I’m getting involved working with electrical components and am primarily (but not exclusively) interested in intelligent creation of RFAs, most likely via a separate independent configurator application useful in its own right with an option to pass configured data to a Revit add-in for automatic creation of RFAs, probably typically based on the *Metric Generic Model* and *Metric Electrical Equipment* templates.

I haven’t been involved too much in either the RFA API or specific Electrical API so far, so I would appreciate some pointers towards sections of The Building Coder blog, API samples, whitepapers, etc., to save me research time, about where to further explore the following topics:

- Generic RFA API &ndash; I am aware of the 3_Revit_Family_API section in the Revit API Training, but where can I find the latest version?
- Specific RFA API aspects for Electrical Circuits and if any other 'E-specifics'.

As said, so far, I am mainly interested in configuration of electrical equipment.

**Answer:**
In general, Revit’s electrical data model of electrical equipment is rather simple.
The category is set to Electrical Equipment, and an electrical connector with appropriate properties of the connector set is added.
You can cheat and look at the settings on one of the existing Equipment families.
They tend to vary by geographical location, though.

Once in the project, there are some type and instance parameters that affect circuiting, but they work just like any other parameter.

The Revit SDK includes the PowerCircuit sample that shows how to create and modify Circuits.
This will hardly be necessary for an equipment configurator.

Just as you say, the generic Family API is explained and illustrated by the
standard [ADN Revit API training material](https://github.com/ADN-DevTech/RevitTrainingMaterial) in
the section [3_Revit_Family_API](https://github.com/ADN-DevTech/RevitTrainingMaterial/tree/master/Labs/3_Revit_Family_API).
The most up-to-date version is always provided on GitHub.

A quick way to find links on The Building Coder to specific topics is to look at
the [topic groups](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5).
The list of interesting posts is by no means complete, nor does it actually include all the best ones, but it is a good place to start.

For example, you might be interested in
the topic group on [Family API and Placing Family Instances](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.25).

Besides the official Revit API training labs mentioned above, you might also like
the [ADN Xtra labs](https://github.com/jeremytammik/AdnRevitApiLabsXtra), which include the former.


#### <a name="5"></a>La Costola di Adamo

To wrap up, let me mention a non-Revit-API issue:

I was deeply touched as well as horrified and angry after
reading [La Costola di Adamo](http://sellerio.it/it/catalogo/Costola-Adamo/Manzini/7240) by
Antonio Manzini, an Italian detective story. Here is a quote from the diary of the female victim of the story:

Domenica sono andata in chiesa. Non volevo sentire la messa. Voleva guardare la chiesa. Ho sbagliato orario. Sono entrata e c'era proprio la messa. Il prete ha letto la Genesi, 2:21.23. Me la sono riletta a casa. Dice: Allora l'eterno Iddio fece cadere un profondo sonno sull'uomo, che s'addormento. E prese una delle costole di lui; e richiuse la carne al posto d'essa. E l'eterno Iddio, con la costola che aveva tolto all'uomo, formo una donna e la condusse all'uomo. E l'uomo disse: "Questa finalmente e ossa delle mie ossa e carne della mia carne. Ella sarà chiamata donna perché e stata tratta dall'uomo."

Mi sono messa a pensare. A sentire la storiella, la donna nasce dall'uomo, anzi ne e proprio un pezzo. E l'uomo impazzisce per la donna, la ama. In realtà ama se stesso. Ama un pezzo di se, non un altro da se. Vive e fa i figli e fa l'amore con se stesso. Un amore concentrato sulla propria persona che niente ha che fare con l'amore. Credo che sia quanto di più perverso abbia mai letto. Il maschio e innamorato solo di se. Questo dicono le Sacre Scritture. L'inferiorità femminile non c'entra. E solo un mezzo per coprire tutto il resto.

L'appartenenza. Una persona appartiene ad un'altra. Per decreto divino. Ovvia la mia vita ha valore perché appartengo all'uomo. Bestie, case, terreni e donne. Appartengono.

*Sunday I went to church. I did not want to hear mass. I wanted to look at the church.
I mistook the time. I entered and mass had already started. The priest read Genesis 2:21.23.
I re-read it at home. It says:
And the LORD God caused a deep sleep to fall upon Adam, and he slept: and he took one of his ribs, and closed up the flesh instead thereof;
And the rib, which the LORD God had taken from man, made he a woman, and brought her unto the man.
And Adam said, This is now bone of my bones, and flesh of my flesh: she shall be called Woman, because she was taken out of Man.*

*I started to think. According to the story, the woman comes from the man, and indeed, is a piece of him.
And the man loves the woman. Actually he loves himself. He loves a piece of himself, not another.
They live together and have children and he makes love with himself.
A love focused on one's self has nothing to do with love.
I think that is the most perverse thing I have ever read.
The male in love only with himself. This is what the Holy Scriptures say.
The female inferiority is not even part of it. It is only a means to cover all the rest.*

*Belonging. One person belongs to another. By divine decree. Obviously my life has value because I belong to the man. Animals, houses, land and women. Belonging.*

In his acknowledgements at the end of the book, Manzini clarifies:

> Al 21 novembre dell'anno 2013, anno in cui ho scritto il libro, i casi di femmicidio in Italia sono stati 122. Finche il numero non si azzererà, non potremmo definirci un paese civile.

> *On November 21. 2013, the year in which I wrote this book, the cases of femicide in Italy were 122.
Until this number is reduced, we cannot consider ourselves a civilised country.*

<center>
<img src="img/la_costola_di_adamo.jpg" alt="La Costola di Adamo" width="224"/>
</center>

So, I'll end the week on this rather sombre note and wish everybody a peaceful and relaxing weekend.

<!----

<pre class="code">
</pre>

**Question:**

**Answer:**

<center>
<img src="img/.png" alt="" width=""/>
</center>

- create a roof
  http://forums.autodesk.com/t5/revit-api/help-creating-roof/m-p/5828806
  http://stackoverflow.com/questions/32718999/creating-a-roof-function/32732012#32732012

#### <a name="5"></a>Creating a Roof

email jaime...

http://stackoverflow.com/questions/32718999/creating-a-roof-function/32732012#32732012

**Question:**
I'm having trouble to create a roof via code. I know how to create a stairs for example : I start a StairsEditScope and use CreateSketchedLanding with all the right parameters to create my stairs and just commit the StairsEditScope, but for a roof i cant find a clue on how to create it from scratch, any leads?

**Answer:**
Revit provides different kinds of roofs. It is best to understand the various types from an end user point of view before starting to drive them programmatically. The simplest one is defined by a horizontal outline. You can create a roof from such an outline using the Document.NewFootPrintRoof method. Such a roof can be flat, or you can specify a slope for each edge of the outline profile. The Building Coder Xtra labs provide a working sample in the external command Lab2_0_CreateLittleHouse in Labs2.cs:

https://github.com/jeremytammik/AdnRevitApiLabsXtra/blob/master/XtraCs/Labs2.cs

Here are some other roof-related posts on The Building Coder:

RoomsRoofs SDK Sample
Roof Eave Cut
Creating an Extrusion Roof

I hope this helps.

Cheers, Jeremy.

<!- 0015 0198 1215 ->
<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/09/roomsroofs-sdk.html">RoomsRoofs SDK Sample</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/08/roof-eave-cut-in-revit-and-aca.html">Roof Eave Cut in AutoCAD Architecture and Revit</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/09/events-again-and-creating-an-extrusion-roof.html">Events, Again, and Creating an Extrusion Roof</a></li>
</ul>

-->

