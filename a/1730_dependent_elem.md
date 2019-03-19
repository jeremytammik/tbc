<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

Retrieving and snooping dependent elements in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/snoopdependents

Håvard Leding of Symetri raises another exciting topic on using the the <code>GetDependentElements</code> method to retrieve and snoop dependent elements, e.g., the sketch of a floor and the model lines defining the floor boundary in that sketch
&ndash; The <code>GetDependentElements</code> method
&ndash; Snoop dependent elements
&ndash; <code>CmdSnoopModScopeDependents</code>
&ndash; RevitLookup update...

linkedin:

Retrieving and snooping dependent elements in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/snoopdependents

Håvard Leding of Symetri raises another exciting topic on using the the GetDependentElements method to retrieve and snoop dependent elements, e.g., the sketch of a floor and the model lines defining the floor boundary in that sketch:

- The GetDependentElements method
- Snoop dependent elements
- CmdSnoopModScopeDependents
- RevitLookup update...

of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) recently

-->

### Retrieving and Snooping Dependent Elements

Håvard Leding of [Symetri](https://www.symetri.com) already made some exciting contributions here,
adding <a href="https://thebuildingcoder.typepad.com/blog/2019/01/new-revitlookup-snoops-edge-face-link.html">RevitLookup commands to snoops edges, faces and links</a>
and <a href="https://thebuildingcoder.typepad.com/blog/2019/02/stable-reference-relationships.html">exploring stable reference relationships</a>.

Now he raises another exciting topic on using the the `GetDependentElements` method to retrieve and snoop dependent elements, e.g., the sketch of a floor and the model lines defining the floor boundary in that sketch.

Some previous related discussions include use of the temporary transaction trick to 
the [change the boundary of floor slabs](https://adndevblog.typepad.com/aec/2013/10/change-the-boundary-of-floorsslabs.html)
and [editing a floor profile](https://thebuildingcoder.typepad.com/blog/2008/11/editing-a-floor-profile.html).
 
In Håvard's own words:

- [The `GetDependentElements` method](#2) 
- [Snoop dependent elements](#3) 
- [`CmdSnoopModScopeDependents`](#4) 
- [RevitLookup update](#5) 

#### <a name="2"></a> The GetDependentElements Method

We were talking about getting the boundaries of a floor through the Sketch element.
 
The Revit 2019 API provides a new helpful method for this:

The `Element` class has a method `GetDependentElements` taking an `ElementFilter` argument that returns 'elements which will be deleted along with this element'.

It is listed
in [What's New in the Revit 2019 API](https://thebuildingcoder.typepad.com/blog/2018/04/whats-new-in-the-revit-2019-api.html)
under [Find Element Dependencies](https://thebuildingcoder.typepad.com/blog/2018/04/whats-new-in-the-revit-2019-api.html#4.2.3):

> The new method `Element.GetDependentElements` returns a list of ids of elements which are 'children' of this element; that is, those elements which will be deleted along with this element. The method optionally takes an `ElementFilter` which can be used to reduce the output list to the collection of elements matching specific criteria.

That includes the `Sketch` element with the profile containing the boundary lines of a floor.

So, to get the boundary lines of a floor, wall or anything else that you can sketch, you can do this
(it might also apply to things like Crop Boundaries &ndash; not sure):

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ElementClassFilter</span>&nbsp;filter&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementClassFilter</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Sketch</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;dependentIds&nbsp;=&nbsp;e.GetDependentElements(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;filter&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;id&nbsp;<span style="color:blue;">in</span>&nbsp;dependentIds&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;depElem&nbsp;=&nbsp;doc.GetElement(&nbsp;id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;depElem&nbsp;<span style="color:blue;">is</span>&nbsp;<span style="color:#2b91af;">Sketch</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;depElem&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Sketch</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

Then you can get the boundaries in a `CurveArrArray` from the `Sketch.Profile`.

A well as references to each `CurveElement`; for a floor, those would be model lines.
 
Taking separate openings and cuts into account, these boundaries may not be the visible shape of the element, like doors or windows, if the element is a wall.

This goes a long way.

#### <a name="3"></a> Snoop Dependent Elements

This could be a new Revit Lookup command :-)

Passing null as ElementFilter to get all ids.
 
<center>
<img src="img/snoop_dependent_elements_1.png" alt="Snoop dependent elements" width="364">
</center>
 
Here is the result after picking a floor:

<center>
<img src="img/snoop_dependent_elements_2.png" alt="Snoop dependent elements" width="802">
</center>

I realised one thing.

As you can see, there are two `Sketch` elements listed.

That is because the floor has an opening, which in turn has its own sketch.

So, it will take some elimination to get the sketch for the floor itself, but very doable.


#### <a name="4"></a> CmdSnoopModScopeDependents

Here is a RevitLookup command implementation set up to process a preselected element:
 
<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Snoop&nbsp;dependent&nbsp;elements&nbsp;using&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Element.GetDependentElements</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">CmdSnoopModScopeDependents</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;cmdData,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;msg,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elems&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;result&nbsp;=&nbsp;<span style="color:#2b91af;">Result</span>.Failed;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Snoop.CollectorExts.<span style="color:#2b91af;">CollectorExt</span>.m_app&nbsp;=&nbsp;cmdData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;cmdData.Application.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;idPickfirst&nbsp;=&nbsp;uidoc.Selection.GetElementIds();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;elemSet&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Element</span>&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;idPickfirst.Select&lt;<span style="color:#2b91af;">ElementId</span>,&nbsp;<span style="color:#2b91af;">Element</span>&gt;(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;id&nbsp;=&gt;&nbsp;doc.GetElement(&nbsp;id&nbsp;)&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ICollection</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;ids&nbsp;=&nbsp;elemSet.SelectMany(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t&nbsp;=&gt;&nbsp;t.GetDependentElements(&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;).ToList();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Snoop.Forms.<span style="color:#2b91af;">Objects</span>&nbsp;form&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Snoop.Forms.<span style="color:#2b91af;">Objects</span>(&nbsp;doc,&nbsp;ids&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ActiveDoc</span>.UIApp&nbsp;=&nbsp;cmdData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;form.ShowDialog();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;result&nbsp;=&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;System.<span style="color:#2b91af;">Exception</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;msg&nbsp;=&nbsp;e.Message;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;result;
&nbsp;&nbsp;}
}
</pre>
 
Implementing the preselection enables a view to be selected in the project browser and snooping that.

For instance, here is the result of snooping a level:

<center>
<img src="img/snoop_dependent_level.png" alt="Snoop dependent elements from level" width="634">
</center>

Ever so many thanks to Håvard for this great suggestion and implementation!


#### <a name="5"></a> RevitLookup Update

I integrated Håvard's enhancement
into [RevitLookup release 2019.0.0.9](https://github.com/jeremytammik/RevitLookup/releases/tag/2019.0.0.9).

Here are two screen snapshots from my sample run, selecting this floor with two holes, one of them elliptical:

<center>
<img src="img/snoop_dependent_floor_1.png" alt="Sample floor element" width="499">
</center>

`CmdSnoopModScopeDependents` lists the following dependent elements:

<center>
<img src="img/snoop_dependent_floor_2.png" alt="Snooping floor dependent elements" width="661">
</center>

I hope you find this useful and wish you lots of fun and success experimenting and enhancing further.
