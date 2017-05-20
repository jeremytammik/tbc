<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 12981176 [How to find the right Project Location]

Finding the Right Project Location @AutodeskForge #ForgeDevCon #RevitAPI @AutodeskRevit #adsk #aec #bim #dynamobim http://bit.ly/right_project_loc

An explanation of the Revit project location elements and selecting the right one to use.
I see two project location elements: 'Internal 21748' and 'Project 111428'.
The active Project Location is 'Internal 21748'.
However, the values in 'Project 111428' are correct and can be used to transform the coordinates, whereas the values in 'Internal 21748' are nonsense.
Why is the active project location wrong?
How can I determine which of the two to use?
What is the difference between these two? ...

-->

### Finding the Right Project Location

Here is an explanation of the Revit project location elements and selecting the right one to use.

#### <a name="2"></a>Question

The sample project 'rac_basic_sample_project.rvt' for Revit 2016 defines project location elements: 'Internal 21748' and 'Project 111428'.

<center>
<img src="img/project_location_1.png" alt="Project locations" width="802">
</center>

The active Project Location is 'Internal 21748'.

<center>
<img src="img/project_location_2.png" alt="Active project location" width="800">
</center>

However, the values in 'Project 111428' are correct and can be used to transform the coordinates, whereas the values in 'Internal 21748' are nonsense.

That leads to the following questions:

1. Why is the active project location 'Internal 21748' and not 'Project 111428'?
2. How can I determine which of the two to use when retrieving all project location elements, given that I cannot rely on the active project location?
3. What is the difference between these two? Why do they exist? Are there always exactly two project locations in a Revit project?

<!----
<center>
<img src="img/project_location.png" alt="Project locations" width="846">
</center>
---->

#### <a name="3"></a>Answer

In the case you describe, the `Document.ProjectLocations` property will return only one single `ProjectLocation` element &ndash; it will hide the location named "Project'.

I have to explain a little bit about how Revit works internally here. A normal Revit model has two SiteLocations &ndash; one tied to the shared coordinates, and one tied to the project coordinates. Each one starts with one `ProjectLocation`. Those are the two ProjectLocations you mention &ndash; 'Internal' for the shared coordinates, and 'Project' for the project coordinates.

The API does not expose the project ones directly, because they are confusing and not particularly helpful for normal use, as you've seen.

If you created more ProjectLocations via the Location/Weather/Site dialog on the Manage tab, they'd share the same SiteLocation as the Internal location.

As far as coordinates are concerned &ndash; assuming you have a ProjectLocation `projLoc` and you execute the following:

<pre class="code">
<span style="color:#2b91af;">ProjectPosition</span>&nbsp;pos&nbsp;=&nbsp;projLoc.GetProjectPosition(&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero&nbsp;);
<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Revit&quot;</span>,&nbsp;pos.EastWest&nbsp;);
</pre>

This will tell you the relationship between the project coordinates and shared coordinates when that location is active. For example, in the little test I did to check my understanding, it says 18, and my project base point is 18 feet east of my survey point.

I don't know what the 'Project' ProjectLocation's coordinates mean in this circumstance, because it would be comparing to itself. Possibly it's a transform between Revit's internal origin and the project coordinates.

If `Document.ProjectLocations` does actually return both ProjectLocations in Revit 2016, you should simply ignore the 'Project' one. It's part of the project coordinates and isn't actually a ProjectLocation in the sense of the locations you see in Location/Weather/Site.

This information is given in terms of the 2018 API. It should mostly apply to Revit 2016 as well. I think the code internals are the same. You might need to make small trivial changes, such as use `get_ProjectPosition()` instead of `GetProjectPosition()` or similar.

Many thanks to Diane Christoforo for this illuminating explanation!
