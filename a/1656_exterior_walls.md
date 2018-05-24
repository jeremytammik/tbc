<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 14230846 [How do I get all the outermost walls in the model?]
  https://forums.autodesk.com/t5/revit-api-forum/how-do-i-get-all-the-outermost-walls-in-the-model/m-p/7998948

- https://forums.autodesk.com/t5/revit-api-forum/how-to-filter-element-which-satisfy-filter-rule/m-p/8020317

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

&ndash; 
...

--->

### Retrieving Exterior Walls



####<a name="2"></a>Task at Hand

####<a name="3"></a>Several Possible Approaches

####<a name="4"></a>Implementing the Huge Surrounding Room Approach

####<a name="5"></a>Determining Model Extents via Wall Bounding Box



####<a name="6"></a>Retrieve Family Instances Satisfying Filter Rule

Once again, Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen
comes to the rescue, providing a succinct answer to 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on ‎[how to filter elements which satisfy filter rule](https://forums.autodesk.com/t5/revit-api-forum/how-to-filter-element-which-satisfy-filter-rule/m-p/8021978):

**Question:** I'm trying to get the family instances which satisfy a filter rule as shown in this image:

<center>
<img src="img/filters_form.png" alt="Filters" width="411"/>
</center>

So far, I'm able to get the list of a category that has a specific filter name.

However, I'd like to get the family instances of those categories which satisfy the filter rule.

I'm not sure how to do that via API.


**Explanation:** Let's say, in Revit, someone needs to find all the walls on the Level 1 or a wall that has some thickness value xyz; they apply a filter rule, and all the walls that satisfy a filter rule get highlighted. 

We need this functionality in an add-in, so we could develop a BIM-Explorer for our modellers to explore and navigate any element easily.

The same idea was implemented by Ideate Software in their explorer add-in, cf. the 12-minute demo on [Auditing Your Revit Project with Ideate Explorer](https://youtu.be/KP7XFv_VL6M):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/KP7XFv_VL6M" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center>

 
**Answer:** You can filter the document, first for the categories of the filter, then for each filter rule:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ParameterFilterElement</span>&nbsp;filter&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;<span style="color:#2b91af;">ElementMulticategoryFilter</span>&nbsp;catfilter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementMulticategoryFilter</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;filter.GetCategories()&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;elemsByFilter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WhereElementIsNotElementType()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;catfilter&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">FilterRule</span>&nbsp;rule&nbsp;<span style="color:blue;">in</span>&nbsp;filter.GetRules()&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;elemsByFilter2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;elemsByFilter.Where(&nbsp;e&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;rule.ElementPasses(&nbsp;e&nbsp;)&nbsp;);
&nbsp;&nbsp;}
</pre>
