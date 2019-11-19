<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- 15913252 [Contradiction between shared dependencies used by Revit plugins]
  [Handling Third Party Library DLL Conflicts](https://thebuildingcoder.typepad.com/blog/2017/06/handling-third-party-library-dll-conflicts.html)

- Fair59 presents a very comprehensive and well explained solution to
  [Replicate Graphical Column Schedule Sort Order with C#]
  https://forums.autodesk.com/t5/revit-api-forum/replicate-graphical-column-schedule-sort-order-with-c/m-p/9105470
  by implementing a custom ColumnMarkComparer class implementing IComparer<FamilyInstance>
  I very strongly expect that you can implement something similar to solve your issue as well.
  [Sort Scheduled Elements base on it's Sort/Group Fields to be export in Excel]
  https://forums.autodesk.com/t5/revit-api-forum/sort-scheduled-elements-base-on-it-s-sort-group-fields-to-be/m-p/9142870

twitter:

Resolving third-party DLL library conflicts and replicating the schedule sort order the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/schedulesortorder

Today I highlight the following topics
&ndash; I caved in to smartphone
&ndash; Handling third party library DLL conflicts
&ndash; Replicating schedule sort order...

linkedin:

Resolving third-party DLL library conflicts and replicating the schedule sort order the #RevitAPI

http://bit.ly/schedulesortorder

Today I highlight the following topics:

- I caved in to smartphone
- Handling third party library DLL conflicts
- Replicating schedule sort order...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### DLL Conflicts and Replicating Schedule Sort Order

As always, I remain busy in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160).

Here, today, I highlight the following topics:

- [I caved in to smartphone](#2)
- [Handling third party library DLL conflicts](#3)
- [Replicating schedule sort order](#4)

####<a name="2"></a> I Caved in to SmartPhone

After several decades of hard resistance, I finally caved in.

Both my bank and my VPN network managers forced me to set up a smartphone.

I got an old Android S5, reset it to factory settings, installed the updates, and [removed all possible Samsung ballast](https://www.guidingtech.com/29921/remove-samsung-galaxy-s5).

It is working fine now, and I am still resisting all further temptation to go smartphone-crazy, not setting up dozens of apps or anything.
 
Just switching it on for the login credential checking, and then immediately off again.

Avoiding [smartphone danger](https://duckduckgo.com/?q=smartphone+danger);
[too much digital is bad for you](https://duckduckgo.com/?q=Too+much+digital+is+bad+for+you).

####<a name="3"></a> Handling Third Party Library DLL Conflicts

Back to a more technology friendly topic... or is it?

One recurring problematic topic is
on [handling third party library DLL conflicts](https://thebuildingcoder.typepad.com/blog/2017/06/handling-third-party-library-dll-conflicts.html).

A new query related to that came up, asking:

**Question:** I recently released a new add-in to my colleagues in the company.
Unfortunately, they report a failure starting it if they are already using another third-party add-in.
It throws the following file load exception:

<center>
<img src="img/fileloadexception_mvvmlight.png" alt="File load exception" width="553">
</center>

What can I do to fix this?

**Answer:** This looks like a conflict between two versions of a .NET assembly DLL that both of the add-ins are dependent on.

The two add-ins require different versions and a conflict ensues.

You are not the first to run into such an issue, as you can see from the list of old discussions 
on [handling third party library DLL conflicts](https://thebuildingcoder.typepad.com/blog/2017/06/handling-third-party-library-dll-conflicts.html) from 2017.

For new suggestions and solutions that came up since then, you can search
the [Revit API discussion forum](https://forums.autodesk.com/t5/revit-api-forum/bd-p/160) for
'dll conflict' and study more recent articles here on the blog:

- [Revit automatically initializes CefSharp](#https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html#4.1.1)
- [CefSharp DLL entanglement solution using IPC](#https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html#4)
- [External DLL loading](#https://thebuildingcoder.typepad.com/blog/2019/09/extensible-storage-external-event-dll-and-pipe-face.html#4)

As you can see from these examples, you have two choices:

- If Revit and / or several different add-ins use the same DLL, ensure that they all use the same version.
- If this is not possible for some reason, disconnect your usage of the problematic DLL from the Revit add-in `AppDomain` and use some other method to access the functionality you require, e.g., via [inter-process communication IPC](https://en.wikipedia.org/wiki/Inter-process_communication).

In this specific case, it sounds to me as if you are the creator of one add-in, and another add-in uses the same support DLL as you do.

In that case, I suggest you modify your add-in to work with whatever version of the support DLL happens to be loaded.

Alternatively, wrap the support DLL functionality into something that you have control over, so that you can do whatever you like with it.

####<a name="4"></a> Replicating Schedule Sort Order

Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen comes to the rescue once again, presenting a very comprehensive and well explained solution
to [replicate graphical column schedule sort order with C#](https://forums.autodesk.com/t5/revit-api-forum/replicate-graphical-column-schedule-sort-order-with-c/m-p/9105470) by
implementing a custom `ColumnMarkComparer` class implementing <code>IComparer&lt;FamilyInstance&gt;</code>:

**Question:** I am trying to write an add-in with C# to automate the process of sequentially labelling structural columns to match the sequence in the graphical column schedule.

I have written the following code to get all the concrete columns in the project and then create a list of their Column Location Marks,  i.e., A(2000)-1(2150), built-in category:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ElementCategoryFilter</span>&nbsp;filter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementCategoryFilter</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_StructuralColumns&nbsp;);
 
&nbsp;&nbsp;StructuralMaterialTypeFilter&nbsp;filter_mat&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;StructuralMaterialTypeFilter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StructuralMaterialType.Concrete&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;columns&nbsp;=&nbsp;collector
&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;filter&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;filter_mat&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;.ToElements();
 
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;&nbsp;locations&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;();
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;colmarks&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;ele&nbsp;<span style="color:blue;">in</span>&nbsp;columns&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;colmark&nbsp;=&nbsp;ele.get_Parameter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.COLUMN_LOCATION_MARK&nbsp;).AsString();
&nbsp;&nbsp;&nbsp;&nbsp;colmarks.Add(&nbsp;colmark&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;colmarks.Sort();
</pre>

My trouble comes at this point.
The `colmarks.Sort` method does not match the order of the columns in the graphical column schedule.

I have also tried sorting the columns by their `XYZ` location which doesn't match the order in the graphical column schedule either.

Can anyone provide any insight into how the graphical column schedule orders the columns and how I can replicate this?

Here is the graphical column schedule sort order I am aiming for:

<center>
<img src="img/graphical_column_schedule_sort.png" alt="Graphical column schedule sort order" width="666"> <!--1332-->
</center>

I've tried a few different ways which involve creating a list of the parameter as strings and then sorting or using `OrderBy` like this:

<pre class="code">
  <span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;sortedElements&nbsp;=&nbsp;columns
  &nbsp;&nbsp;.OrderBy(&nbsp;x&nbsp;=&gt;&nbsp;x.get_Parameter(&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.COLUMN_LOCATION_MARK&nbsp;).AsString()&nbsp;)
  &nbsp;&nbsp;.ToList();
</pre>

That does not help:

<center>
<img src="img/graphical_column_schedule_sort_cs.png" alt="Graphical column schedule sort order in C#" width="223"> <!--223-->
</center>

I have the same issue in Dynamo too; the order seems to match my code but not the graphical column schedule:

<center>
<img src="img/graphical_column_schedule_sort_dynamo.png" alt="Graphical column schedule sort order in Dynamo" width="518"> <!--1036-->
</center>

I also read the thread on
[Graphical column schedule in Revit &ndash; sort by specified parameter](https://forums.autodesk.com/t5/revit-ideas/graphical-column-schedule-in-revit-sort-by-specified-parameter/idi-p/8190431)
and still hope someone can shed some light on the logic Revit uses to sort the columns in the graphical column schedule so that I can replicate it with my own code in C#.

**Answer:** Implement your own comparison class.

LocationMark syntax:

<pre>
  A(xxxx)-1(yyyy)
</pre>

Sort order:

- Grid sequence A: alphabetic ascending
- Grid sequence 1: alphabetic ascending
- xxxx value: first the positive values from smallest to largest, then negative values from smallest to largest
- yyyy value: first the positive values from smallest to largest, then negative values from smallest to largest

<pre class="code">
<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;&nbsp;GetSortedColumns(&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;&nbsp;colums
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_StructuralColumns&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToList();
 
&nbsp;&nbsp;colums.Sort(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ColumnMarkComparer</span>()&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;colums;
}
</pre>

Extension method helper class:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">Extensions</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;GetColumnLocationMark(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;f&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;p&nbsp;=&nbsp;f.get_Parameter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.COLUMN_LOCATION_MARK&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>(&nbsp;p&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;<span style="color:blue;">string</span>.Empty
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;p.AsString();
&nbsp;&nbsp;}
}
</pre>

Comparer helper class:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">ColumnMarkComparer</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IComparer</span>&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;
{
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;<span style="color:#2b91af;">IComparer</span>&lt;<span style="color:#2b91af;">FamilyInstance</span>&gt;.Compare(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;x,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;y&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;x&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;y&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;?&nbsp;0&nbsp;:&nbsp;-1;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;y&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;1;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>[]&nbsp;mark1&nbsp;=&nbsp;x.GetColumnLocationMark()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Split(&nbsp;<span style="color:#a31515;">&#39;(&#39;</span>,&nbsp;<span style="color:#a31515;">&#39;)&#39;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>[]&nbsp;mark2&nbsp;=&nbsp;y.GetColumnLocationMark()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Split(&nbsp;<span style="color:#a31515;">&#39;(&#39;</span>,&nbsp;<span style="color:#a31515;">&#39;)&#39;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;mark1.Length&nbsp;&lt;&nbsp;4&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;mark2.Length&nbsp;&lt;&nbsp;4&nbsp;?&nbsp;0&nbsp;:&nbsp;-1;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;mark2.Length&nbsp;&lt;&nbsp;4&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;1;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;gridsequence&nbsp;A</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;res&nbsp;=&nbsp;<span style="color:blue;">string</span>.Compare(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mark1[&nbsp;0&nbsp;],&nbsp;mark2[&nbsp;0&nbsp;]&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;res&nbsp;!=&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;res;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;gridsequence&nbsp;1</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;m12&nbsp;=&nbsp;mark1[&nbsp;2&nbsp;].Remove(&nbsp;0,&nbsp;1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;m22&nbsp;=&nbsp;mark2[&nbsp;2&nbsp;].Remove(&nbsp;0,&nbsp;1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;res&nbsp;=&nbsp;<span style="color:blue;">string</span>.Compare(&nbsp;m12,&nbsp;m22&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;res&nbsp;!=&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;res;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;value&nbsp;xxxx</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;d1&nbsp;=&nbsp;0;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;d2&nbsp;=&nbsp;0;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>.TryParse(&nbsp;mark1[&nbsp;1&nbsp;],&nbsp;<span style="color:blue;">out</span>&nbsp;d1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>.TryParse(&nbsp;mark2[&nbsp;1&nbsp;],&nbsp;<span style="color:blue;">out</span>&nbsp;d2&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">Math</span>.Round(&nbsp;d1&nbsp;-&nbsp;d2,&nbsp;4&nbsp;)&nbsp;!=&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;d1&nbsp;&lt;&nbsp;0&nbsp;^&nbsp;d2&nbsp;&lt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;d1&nbsp;&lt;&nbsp;0&nbsp;?&nbsp;1&nbsp;:&nbsp;-1;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Math</span>.Abs(&nbsp;d1&nbsp;)&nbsp;&lt;&nbsp;<span style="color:#2b91af;">Math</span>.Abs(&nbsp;d2&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;-1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;1;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;value&nbsp;yyyy</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>.TryParse(&nbsp;mark1[&nbsp;3&nbsp;],&nbsp;<span style="color:blue;">out</span>&nbsp;d1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>.TryParse(&nbsp;mark2[&nbsp;3&nbsp;],&nbsp;<span style="color:blue;">out</span>&nbsp;d2&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">Math</span>.Round(&nbsp;d1&nbsp;-&nbsp;d2,&nbsp;4&nbsp;)&nbsp;!=&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;d1&nbsp;&lt;&nbsp;0&nbsp;^&nbsp;d2&nbsp;&lt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;d1&nbsp;&lt;&nbsp;0&nbsp;?&nbsp;1&nbsp;:&nbsp;-1;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Math</span>.Abs(&nbsp;d1&nbsp;)&nbsp;&lt;&nbsp;<span style="color:#2b91af;">Math</span>.Abs(&nbsp;d2&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;-1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;1;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;0;
&nbsp;&nbsp;}
}
</pre>

Many thanks to Fair59 for providing yet another effective high-level answer, which also solves a subsequent question
on [sorting scheduled elements based on their sort and group fields](https://forums.autodesk.com/t5/revit-api-forum/sort-scheduled-elements-base-on-it-s-sort-group-fields-to-be/m-p/9142870).

I added his code
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples), cf. 
the [diff to the previous release](https://github.com/jeremytammik/the_building_coder_samples/compare/2020.0.147.18...2020.0.147.19).
