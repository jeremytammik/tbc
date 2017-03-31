<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="run_prettify.js" type="text/javascript"></script> --> 
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- new ttt
  https://forums.autodesk.com/t5/revit-api-forum/how-can-i-get-the-coordinates-of-the-endpoints-for-a-section/m-p/6928342

 #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge 

We discussed several examples of using the temporary transaction trick TTT in the past.
Here is a new exquisitly subtle variant for you to enjoy, provided by Frank @Fair59 Aarssen to get the coordinates of the endpoints for a section marker line segment. Question: I have a section marker that I would like to rotate around one of the endpoints of the line segment leader, but I haven't been able to figure out how to determine the endpoint coordinates...

-->

### TTT to Create Provision For Voids

Today is the last day of 
the [Forge accelerator](http://thebuildingcoder.typepad.com/blog/2017/03/events-uv-coordinates-and-rooms-on-level.html#2),
and I am still working on 
the [ForgeFader](https://github.com/jeremytammik/forgefader) project.


#### <a name="2"></a>Google Prettifier on GitHub

I happened to notice yesterday that the Google [JavaScript code prettifier](https://github.com/google/code-prettify) moved and now lives in GitHub.

The script can now be loaded like this:

<pre class="prettyprint">
&lt;script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js"&gt;&lt;/script&gt;
</pre>


#### <a name="3"></a>Allan's Mechanical Family Placement Add-in

Allan
[@aksaks](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/540057)
[@akseidel](https://github.com/akseidel)
Seidel made a numer of contributions here in the past, including:

- [Stacked Ribbon Button Panel Options](http://thebuildingcoder.typepad.com/blog/2016/09/stacked-ribbon-button-panel-options.html)
- [FireP and 3d Aimer](http://thebuildingcoder.typepad.com/blog/2017/03/wta-firep-and-3d-aimer-tools.html)
    - Ribbon panel implementation sample
    - Fire protection related family placement tools
    - 3d Aimer that "aims" a special Revit family instance at a target
- [WTA Elec &ndash; another family plunk and concept share](http://thebuildingcoder.typepad.com/blog/2017/03/wta-firep-and-3d-aimer-tools.html#5)

His newest sample provides a similar ribbon panel and tool collection for family instance placement and management for tasks primarily oriented towards the mechanical domain:

> Apologizing for beginning to wear out a welcome, here is a video and code share for yet another custom ribbon specific task command involving family placement and tags. The ribbon panel split button control being used is a variant of what is mentioned here http://thebuildingcoder.typepad.com/blog/2016/09/stacked-ribbon-button-panel-options.html.

> One idea presented is a remote tag. Maybe this is commonly done in Revit practice. The remote tag here is simply a tag on on item where the tag itself is placed in proximity to a different item that is a system client to the former item. What is really needed is for Revit tags to show values from an indexed object instead of the object the tag tags. Perhaps it does this. Anyway, that is part of what is seen in the video.

> - [YouTube video](https://www.youtube.com/watch?v=_x7yyx4Yk_I)
> - [GitHub repository](https://github.com/akseidel/WTA_MECH)
 
> Other items of interest are "Host face normal direction used to drive symbology orientation" and "User settings for family names, types and tag parameter names". The first is a way used to orient based on which direction the host face faces. The idea could be used with a complex family that is capable of rotating within itself to produce a face based family placement that is "always level" so to speak. Only a crude family is used in the demonstration but the code behind does provide the necessary angle to "level". The second is an attempt to externalize family names, family types and parameter names outside of the hard coded add-in. The need is necessary for any type of task related ribbon customization where aspects to the task are not static.   

<center>
<img src="img/akseidel_MechRibbonTab.png" alt="WTA_MECH mechanical ribbon tab" width="500"/>
</center>

Here is Allan's more detailed description from the GitHub repo:

Revit Add-in in C# &mdash; Creates a custom ribbon tab with discipline related tools. The tools at this writing are for placing specific Revit family types with some families requiring parameter settings made on the fly.

This repository is provided for sharing and learning purposes. Perhaps someone might provide improvements or education. Perhaps it will help to boost someone further up the steep learning curve needed to create Revit task add-ins. Hopefully it does not show too much of the wrong way.  

The tools in this ribbon use classes that provide Revit family instance placement functionality with less overhead the standard Revit user interface.
The classes are intended to provide a universal mechanism for placing some types of families, including tags.
The custom tab employs menu methods not commonly explained, for example a split button sets a family placement mode that is exposed to the functions called by command picks.
Other tools use add-in application settings as a way to persist settings or communicate to code that runs subsequently within a command that provides a task workflow.

This add-in demonstrates many of the typical tasks and implementation required for a family placement tab menu interface, e.g.:

* Creating a ribbon tab populated with some controls
  - Tool tips
  - Image file to button image
  - Communication between controls and commands the controls execute
* Establishing the family type for placement
  - Determine if the correct pairing exists in the current file
  - Automatically discovering and loading the family if it does not exist in the current file but does exist somewhere starting from some set directory
* Providing the family type placement interface
  - In multiple mode or one shot mode
  - With a heads-up status/instruction interface form
    - As WPF with independent behavior
      - Sending focus back to the Revit view
  - Returning the family instance placement for further processing after the instance has been placed
  - Managing an escape out from the process
  - Handling correct view type context
* Changing family parameter values

Much of the code is by others. Its mangling and ignorant misuse is my ongoing doing. Much thanks to the professionals like Jeremy Tammik who provided the means directly or by mention one way or another for probably all the code needed.

Here are some aspects that may be of specific interest:

- **Hybrid splitbutton behavior** &mdash; A splitbutton control with four buttons, where the top two execute one of two types of the same task and the bottom two invoke a settings panel for one of the corresponding task types. The top two buttons, being tasks the user is likely going to repeat, remain as the last button selected. The bottom two buttons are incidental tasks the user would not need to repeat nor want to be the splitbutton's face button. The splitbutton face button reverts back to what is was before the user selected one of the bottom two button. That is the hybrid behavior. This idea was discussed here: [thebuildingcoder &ndash; stacked-ribbon-button-panel-options](http://thebuildingcoder.typepad.com/blog/2016/09/stacked-ribbon-button-panel-options.html)
- **Host face normal direction used to drive symbology orientation** &mdash; The HVAC sensor family placed by the tools has two symbology orientations, because Revit does not really support a universal "view independent text". The family's symbology would be wrong 50% of the time it were placed. The code attempts to use the host face normal direction to more correctly pick which of the two orientations to make visible. Each orientation's visibility is controlled by a parameter. Only one parameter needs to be set, because the other parameter is function controlled in the family to be `not(the_other)`.
- **User settings for family names, types and tag parameter names** &mdash; As a way to side step the problem of hard coding family names, types and tag parameter names, the user can set the names to some other values. Not implemented, but planned: an external file text to also house the standard settings.
- **Remote equipment tag** &mdash; Sensors, like thermostats, for example, are often the client to a piece of a mechanical system. The mechanical system name the sensor is a client for, an ID number for example, is typically placed next to the sensor on construction plans. That ID number is part of the mechanical equipment properties, but not the sensor properties, unless it is duplicated to the sensor. Therefore, tagging the sensor using the latter method involves more effort and maintenance. The remote equipment tag is a tag to the mechanical equipment placed at the sensor. That is what this add-in is doing. It would be nice if Revit were to support an indirect tag value, like the way a microprocessor does indirect addressing. Then the sensor could be tagged with a tag that gets its value from the parameter values of an indirectly addressed object.
- **Multiple view annotation placement** &mdash; This add-in can place the annotations, the tags, simultaneously in more than one view at a time. The final tag placement code iterates through a list of candidate views when placing the tag. That list is developed by a single function using specific rules, which one has to adapt to one's needs, operating on parent view names. For example, if the active view is a dependent view, its parent view name is used in the candidate logic. The reason for this feature is due to one of Revit's worst usability problems, where annotation is relegated to the output composition task and not visible during the more important design and engineering tasks.         


#### <a name="4"></a>TTT to Create Provision For Voids

We discussed numerous examples of the temporary transaction trick TTT in the past, the last one just two days back, 
to [obtain a section marker endpoint](http://thebuildingcoder.typepad.com/blog/2017/03/ttt-to-obtain-section-marker-endpoint.html),
and it is also mentioned in The Building Coder topic group
on [handling transactions and transaction groups](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.53).

Here is another interesting one for you, provided by
Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen.

He uses it to answer 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) question
on [how to get the coordinates of the endpoints for a section marker line segment](https://forums.autodesk.com/t5/revit-api-forum/how-can-i-get-the-coordinates-of-the-endpoints-for-a-section/m-p/6928342),
an issue that cannot be solved, according to the Revit development team:

**Question:** I have a section marker that I would like to rotate around one of the endpoints of the line segment leader, but I haven't been able to figure out how to determine the endpoint coordinates:

<center>
<img src="img/section_marker_coordinates.png" alt="Section marker coordinates" width="439"/>
</center>

I'm able to get the bounding box of the overall section marker but not these specific coordinates.

How can I do this?

My bigger problem involves reversing the effects of the built-in "Mirror Project" tool, and part of that includes reference section markers inside of another section that has been flipped.

I would like to fix their orientation by rotating 180 degrees around the endpoint.

**Answer:** The development team says that the section marker element is not exposed as a specific element type, so this position cannot be read at this time. 

However, Frank provided a workaround.
 
A transaction needs to be active when you call the method, as the method temporarily hides the section tag and viewer_reference_label_text:
 
<pre class="code">
<span style="color:#2b91af;">Line</span>&nbsp;GetSectionLine(&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;section,&nbsp;
&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;view&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">double</span>&nbsp;correction&nbsp;=&nbsp;21.130014403&nbsp;/&nbsp;304.8;
 
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;section.Document;
&nbsp;&nbsp;<span style="color:#2b91af;">Category</span>&nbsp;cat&nbsp;=&nbsp;section.Category;
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;cat&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentException</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Section&nbsp;has&nbsp;null&nbsp;category&quot;</span>&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Viewers
&nbsp;&nbsp;&nbsp;&nbsp;!=&nbsp;(<span style="color:#2b91af;">BuiltInCategory</span>)&nbsp;(cat.Id.IntegerValue)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentException</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Expected&nbsp;section&nbsp;with&nbsp;OST_Viewers&nbsp;category&quot;</span>&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;views&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">View</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;viewFromSection&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">View</span>&nbsp;v&nbsp;<span style="color:blue;">in</span>&nbsp;views&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;section.Name&nbsp;==&nbsp;v.Name
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;section.GetTypeId()&nbsp;==&nbsp;v.GetTypeId()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;viewFromSection&nbsp;=&nbsp;v;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;viewFromSection&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;<span style="color:#2b91af;">ViewFamilyType</span>&nbsp;vType&nbsp;=&nbsp;doc.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;section.GetTypeId()&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">ViewFamilyType</span>;
 
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;bb1&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">SubTransaction</span>&nbsp;st1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">SubTransaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;st1.Start();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;par&nbsp;=&nbsp;vType.get_Parameter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.SECTION_TAG&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;par.Set(&nbsp;<span style="color:#2b91af;">ElementId</span>.InvalidElementId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;par&nbsp;=&nbsp;vType.get_Parameter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.VIEWER_REFERENCE_LABEL_TEXT&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;par.Set(&nbsp;<span style="color:blue;">string</span>.Empty&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;view.Scale&nbsp;=&nbsp;1;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Regenerate();
&nbsp;&nbsp;&nbsp;&nbsp;bb1&nbsp;=&nbsp;section.get_BoundingBox(&nbsp;view&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;st1.RollBack();
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;bb&nbsp;=&nbsp;section.get_BoundingBox(&nbsp;view&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pt1&nbsp;=&nbsp;bb.Min;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;pt2&nbsp;=&nbsp;bb.Max;
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;bb1&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;pt1&nbsp;=&nbsp;bb1.Min;
&nbsp;&nbsp;&nbsp;&nbsp;pt2&nbsp;=&nbsp;bb1.Max;
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;Origin&nbsp;=&nbsp;viewFromSection.Origin;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;ViewBasisX&nbsp;=&nbsp;viewFromSection.RightDirection;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;ViewBasisY&nbsp;=&nbsp;viewFromSection.ViewDirection;
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ViewBasisX.X&nbsp;&lt;&nbsp;0&nbsp;^&nbsp;ViewBasisX.Y&nbsp;&lt;&nbsp;0&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;d&nbsp;=&nbsp;pt1.Y;
&nbsp;&nbsp;&nbsp;&nbsp;pt1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;pt1.X,&nbsp;pt2.Y,&nbsp;pt1.Z&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;pt2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;pt2.X,&nbsp;d,&nbsp;pt2.Z&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;ToPlane1&nbsp;=&nbsp;pt1.Add(&nbsp;ViewBasisY.Multiply(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ViewBasisY.DotProduct(&nbsp;Origin.Subtract(&nbsp;pt1&nbsp;)&nbsp;)&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;ToPlane2&nbsp;=&nbsp;pt2.Subtract(&nbsp;ViewBasisY.Multiply(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ViewBasisY.DotProduct(&nbsp;pt2.Subtract(&nbsp;Origin&nbsp;)&nbsp;)&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;correctionVector&nbsp;=&nbsp;ToPlane2.Subtract(&nbsp;ToPlane1&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.Normalize().Multiply(&nbsp;correction&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;endPoint0&nbsp;=&nbsp;ToPlane1.Add(&nbsp;correctionVector&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;endPoint1&nbsp;=&nbsp;ToPlane2.Subtract(&nbsp;correctionVector&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;endPoint0,&nbsp;endPoint1&nbsp;);
}
</pre>

**Response:** I have a few questions:
 
1. Where did you come up with the `const double correction = 21.130014403 / 304.8` value?
2. How do you distinguish between section vs. callout markers since they are both of category `OST_Viewers`?
3. Would this work for section markers that are only references (such as ones that reference drafting views)?
4. How do you know which endpoint is associated with the tail and which one is associated with the head?

**Answer 1:** The bounding box of a section line is the result of a number of elements:

- the line
- the section head
- the reference label text
- the cycle symbol

<center>
<img src="img/section_marker_bounding_box.png" alt="Section marker bounding box" width="490"/>
</center>

In the method, I "hide" 2 and 3.
You can't hide the cycle symbol, so the result is too large.
I simply added the resulting line without the correction to the project and measured the surplus length.
After checking that it is always the same, I implemented the constant  `correction = 21.130014403 / 304.8`.

The number 304.8 is the number of millimetres in a foot, so the division by that number converts from feet to mm:

<pre>
  inch = 25.4
  foot = 12 * inch = 304.8
  length_in_mm = length_in_feet / foot
</pre>
 
**Answer 2:** I didn't distinguish between section vs. callout markers, I merely presumed you had a section marker.

There are 2 ways to test:

<pre class="code">
  <span style="color:blue;">if</span>(&nbsp;_vType.FamilyName&nbsp;==&nbsp;<span style="color:#a31515;">&quot;Section&quot;</span>&nbsp;)
   
  <span style="color:blue;">if</span>(&nbsp;_viewFromSection.GetType()&nbsp;
  &nbsp;&nbsp;==&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">ViewSection</span>&nbsp;)&nbsp;)
</pre>

Callouts are PlanViews or Elevations, as far as I know.
 
**Answer 3:** I don't know.
 
**Answer 4:** I'm afraid that is truly hidden. The Section Head and Tail stay in the same position when flipping the section, but the viewDirection and RightDirection changes.

The most you can get is a line in the View.RightDirection
 
<pre class="code">
  <span style="color:#2b91af;">XYZ</span>&nbsp;_direction&nbsp;=&nbsp;ToPlane2.Subtract(&nbsp;ToPlane1&nbsp;).
  &nbsp;&nbsp;Normalize();
   
  <span style="color:blue;">return</span>&nbsp;_direction.IsAlmostEqualTo(&nbsp;ViewBasisX&nbsp;)&nbsp;
  &nbsp;&nbsp;?&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;endPoint0,&nbsp;endPoint1&nbsp;)&nbsp;
  &nbsp;&nbsp;:&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;endPoint1,&nbsp;endPoint0&nbsp;);
</pre>

Many thanks to Frank for this tricky solution, and many other extremely helpful and in-depth answers in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) as well!









#### <a name="2"></a>

#### <a name="3"></a>

<center>
<img src="img/.png" alt="" width="500"/>
</center>


#### <a name="4"></a>

#### <a name="5"></a>

A [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) question
on [getting the area scheme from an area](https://forums.autodesk.com/t5/revit-api-forum/get-area-scheme-from-an-area/m-p/6949212) turned
out to be a pretty trivial matter of accessing and evaluating a simple series of parameter values on the area and area scheme:

**Question:** 


**Answer:** Have you tried this?

<pre class="code">
</pre>

**Response:** 


 
I added these two methods to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
in [release 2017.0.132.10](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.132.10).
 

