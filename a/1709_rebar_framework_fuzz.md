<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- the importance of fuzz:
  [Not all curtain walls behaving equally](https://forums.autodesk.com/t5/revit-api-forum/not-all-curtain-walls-behaving-equally/m-p/8457387)

- how to install the .NET framework 4.7 for creating add-ins for Revit 2019.1
  [AU2018 Class and .NET 4.7 for Revit 2019.1](https://forums.autodesk.com/t5/revit-api-forum/au2018-class-and-net-4-7-for-revit-2019-1/m-p/8451317)
  14872193 [AU2018 Class and .NET 4.7 for Revit 2019.1]
  https://forums.autodesk.com/t5/revit-api-forum/au2018-class-and-net-4-7-for-revit-2019-1/m-p/8451317

- [Get all associated Rebars which attach to the Structural Element](https://forums.autodesk.com/t5/revit-api-forum/get-all-associated-rebars-which-attach-to-the-structural-element/m-p/8455579)
  getrebarsinhost

Rebars in host, .NET framework and importance of fuzz in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/rebarnetfuzz

Here are a couple more Revit API discussion forum threads well worth highlighting
&ndash; Retrieve rebars attached to structural element
&ndash; Installing the .NET framework 4.7 for Revit 2019.1 add-ins
&ndash; Importance of fuzz for curtain wall dimensioning...

-->

### Rebars in Host, .NET Framework and Importance of Fuzz

Here are a couple
more [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) threads
well worth highlighting:

- [Retrieve rebars attached to structural element](#2) 
- [Installing the .NET framework 4.7 for Revit 2019.1 add-ins](#3) 
- [Importance of fuzz for curtain wall dimensioning](#4) 


#### <a name="2"></a> Retrieve Rebars Attached to Structural Element

**Question:** How can I get all associated rebars which attach to a structural element such as a column by picking that?

**Answer:** Jeremy initially
suggested [workarounds making use of filtered element collectors](https://thebuildingcoder.typepad.com/blog/2018/12/using-an-intersection-filter-for-linked-elements.html#3).
Unfortunately, that was not very helpful in this case.

Happily, Einar Raknes came to the rescue pointing out the real solution for this:

You can use the `RebarHostData` class and the `GetRebarsInHost` method to retrieve all rebars associated with a rebar host.

To make sure you pick a valid Rebar Host; you can optionally create a selection filter for it like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">RebarHostSelectionFilter</span>&nbsp;:&nbsp;<span style="color:#2b91af;">ISelectionFilter</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;AllowElement(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>&nbsp;!=&nbsp;<span style="color:#2b91af;">RebarHostData</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetRebarHostData(&nbsp;e&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;AllowReference(&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;r,&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

Pick a rebar host, and retrieve the list of rebars from it like this:

<pre class="code">
&nbsp;&nbsp;ref1&nbsp;=&nbsp;uidoc.Selection.PickObject(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ObjectType</span>.Element,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">RebarHostSelectionFilter</span>(),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Pick&nbsp;a&nbsp;rebar&nbsp;host&quot;</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;rebarHost&nbsp;=&nbsp;doc.GetElement(&nbsp;ref1&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">Rebar</span>&gt;&nbsp;rebarsInHost&nbsp;=&nbsp;<span style="color:#2b91af;">RebarHostData</span>
&nbsp;&nbsp;&nbsp;&nbsp;.GetRebarHostData(&nbsp;rebarHost&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.GetRebarsInHost();
</pre>

Many thanks to Einar for pointing this out!

#### <a name="3"></a> Installing the .NET Framework 4.7 for Revit 2019.1 Add-Ins

**Question:** I am walking through the Autodesk University 2018 course on Revit add-ins:

- [Pushing Revit to the Next Level &ndash; an Intro to Revit Plugins with C#](https://www.autodesk.com/autodesk-university/class/Pushing-Revit-Next-Level-Intro-Revit-Plugins-C-2018)

Revit 2019.1 add-in programming apparently requires the .NET framework 4.7.

<center>
<img src="img/net47_figure_20_create_project.png" alt="Figure 20 Create Project" width="689">
<p style="font-size: 80%; font-style:italic">Figure 20 Create Project</p>

<img src="img/net47_class_library_net_framework.png" alt="Class Library .NET Framework" width="640">
<p style="font-size: 80%; font-style:italic">Class Library .NET Framework</p>
</center>

However, I cannot seem to get access to .NET Framework 4.7.

I have turned this on in Windows Features:

<center>
<img src="img/net47_in_windows_features.png" alt=".NET Framework 4.7 in Windows features" width="670">
<p style="font-size: 80%; font-style:italic">.NET Framework 4.7 in Windows features</p>
</center>

What am I missing?

**Answer:** In Visual Studio, go to Tools &rarr; Get Tools and Features &rarr; Individual Components &rarr; tick the .NET version you want to install:

<center>
<img src="img/net47_vs_tools_01.png" alt="VS tools" width="638">
<p style="font-size: 80%; font-style:italic">VS Tools</p>
<img src="img/net47_vs_tools_02.png" alt="VS tools" width="632">
</center>

Many thanks to Salvatore Dragotta for pointing this out!


#### <a name="4"></a> Importance of Fuzz for Curtain Wall Dimensioning

The Building Coder keeps on harping about the importance of fuzz, cf. the recent discussion
of [fuzzy comparison versus exact arithmetic for curve intersection](https://thebuildingcoder.typepad.com/blog/2017/12/project-identifier-and-fuzzy-comparison.html#3).

Here is yet another example underlining the importance of fuzz, described by Bram Weinreder in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [not all curtain walls behaving equally](https://forums.autodesk.com/t5/revit-api-forum/not-all-curtain-walls-behaving-equally/m-p/8457387):

**Question:** I've made an add-in to dimension curtain walls, and in my test projects it was working fairly well. That's to say, tagging worked 100% (easy win), but elevation tags and dimensioning didn't always work (let's say 70% or 80% worked for me, but for some users and some projects 100% produced the same failure).

The problem is that I'm not getting the total widths of these windows, in rare cases the total heights, or the bottom reference for spot elevations. I'm getting the references based on physical mullion faces with a certain normal, for mullions that work in a certain direction (this works very well, generally, if I need all unfiltered references). It's probably where I filter out the exterior faces that I make a mistake.

Example of how I do my filtering:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Face</span>&nbsp;fa&nbsp;<span style="color:blue;">in</span>&nbsp;faces&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Mullion</span>&nbsp;m&nbsp;=&nbsp;doc.GetElement(&nbsp;fa.Reference&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Mullion</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">LocationPoint</span>&nbsp;lp&nbsp;=&nbsp;m.Location&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">LocationPoint</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Reference</span>&nbsp;r&nbsp;=&nbsp;fa.Reference;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;direction&nbsp;==&nbsp;Direction.horizontal&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;lp.Point.Z&nbsp;==&nbsp;bb.Min.Z&nbsp;&amp;&amp;&nbsp;!minAdded
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;fa.ComputeNormal(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UV</span>(&nbsp;0,&nbsp;0&nbsp;)&nbsp;).X&nbsp;&gt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;totaalMaten.Append(&nbsp;r&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;minAdded&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;lp.Point.Z&nbsp;==&nbsp;bb.Max.Z&nbsp;&amp;&amp;&nbsp;!maxAdded
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;fa.ComputeNormal(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UV</span>(&nbsp;0,&nbsp;0&nbsp;)&nbsp;).X&nbsp;&gt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;totaalMaten.Append(&nbsp;r&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;maxAdded&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>&nbsp;<span style="color:blue;">if</span>(&nbsp;direction&nbsp;==&nbsp;Direction.vertical&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;lp.Point.X&nbsp;==&nbsp;bb.Min.X&nbsp;&amp;&amp;&nbsp;!leftAdded
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;fa.ComputeNormal(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UV</span>(&nbsp;0,&nbsp;0&nbsp;)&nbsp;).X&nbsp;&gt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;totaalMaten.Append(&nbsp;r&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;leftAdded&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;lp.Point.X&nbsp;==&nbsp;bb.Max.X&nbsp;&amp;&amp;&nbsp;!rightAdded
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;fa.ComputeNormal(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UV</span>(&nbsp;0,&nbsp;0&nbsp;)&nbsp;).X&nbsp;&gt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;totaalMaten.Append(&nbsp;r&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rightAdded&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

The Normal is always relative to the mullion interior coordinates; in short this is filtering the faces that are looking out. But apparently, this is not an adequate method in all situations.

Does anyone know whether there can be a difference between curtain walls that makes them behave differently?

Side note: is there a more reliable way to get the exterior references (say, by bounding box)?

Thanks in advance.

**Answer:** And I found my own answer.

What's vexing me is that these references don't have GlobalPoints, or I would've stumbled upon this quicker.

The error is in this part of the filtering:

<pre class="code">
  <span style="color:blue;">if</span>(&nbsp;lp.Point.Z&nbsp;==&nbsp;bb.Min.Z&nbsp;*/...<span style="color:green;">/*)</span>
</pre>

I'm not sure whether it's due to the conversion between imperial and metric, but I forgot the fundamental rule that you can't always directly compare two `XYZ` values.

I replaced the condition with this:

<pre class="code">
  <span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">Math</span>.Abs(&nbsp;lp.Point.Z&nbsp;-&nbsp;bb.Min.Z&nbsp;)&nbsp;&lt;&nbsp;0.005&nbsp;*/...<span style="color:green;">/*)</span>
</pre>

This translates to a tolerance of about 1.5mm.

Could've probably added three more zeroes there, but this is precise enough for our case.

Many thanks to Bram for pointing this out!
