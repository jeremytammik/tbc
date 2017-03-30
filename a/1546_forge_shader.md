<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="run_prettify.js" type="text/javascript"></script> --> 
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js"></script>
</head>

<!---

Three.js Raytracing in the Forge Viewer http://bit.ly/forgeraytrace #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #threejs #javascript

Yesterday, I showed how you can add custom geometry to the Forge viewer for debugging or other purposes and to control graphically what is going on.
Today, I address the ray tracing required to determine the number of walls between the user selected signal source point and the other target points spread out across the picked floor slab.
Please note the important information in the final section: the viewer implements built-in raycast functionality that obviates the need for this
&ndash; Connecting Visual Studio Code to the Chrome debugger
&ndash; Creating a three.js mesh from Forge viewer fragments
&ndash; Use built-in viewer raycast instead...

-->

### Adding Custom Shaders in the Forge Viewer

I am still in Gothenburg supporting
the [Forge accelerator](http://thebuildingcoder.typepad.com/blog/2017/03/events-uv-coordinates-and-rooms-on-level.html#2) and 
working on my [ForgeFader](https://github.com/jeremytammik/forgefader) project.

ForgeFader implements the same end user functionality as
the [RvtFader](https://github.com/jeremytammik/RvtFader) Revit
C# .NET API add-in that I implemented last week.

In the past days, I showed how 
to [add custom geometry to](http://thebuildingcoder.typepad.com/blog/2017/03/adding-custom-geometry-to-the-forge-viewer.html) and
use [three.js raytracing in the Forge Viewer](http://thebuildingcoder.typepad.com/blog/2017/03/threejs-raytracing-in-the-forge-viewer.html).

As a final step to bring the ForgeFader functionality up to par with RvtFader, I show how to add custom fragment shaders to a model in the Forge viewer.

The custom shader is used to display the signal attenuation between a picked source point on a floor slab and all possible target points as a colour gradient.

I also highlight another extremely useful family placement sample shared by Allan Seidel:



#### <a name="2"></a>Google Prettifier on GitHub

I happened to notice yesterday that the Google [JavaScript code prettifier](https://github.com/google/code-prettify) moved and now lives in GitHub.

The script can now be loaded like this:

<pre class="prettyprint">
&gt;script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js"&lt;&gt;/script&lt;
</pre>


#### <a name="3"></a>Allan's Mechanical Family Placement Add-in

Apologizing for beginning to wear out a welcome, here is a video and code share for yet another custom ribbon specific task command involving family placement and tags. The ribbon panel split button control being used is a variant of what is mentioned here http://thebuildingcoder.typepad.com/blog/2016/09/stacked-ribbon-button-panel-options.html.
 
One idea presented is a remote tag. Maybe this is commonly done in Revit practice. The remote tag here is simply a tag on on item where the tag itself is placed in proximity to a different item that is a system client to the former item. What is really needed is for Revit tags to show values from an indexed object instead of the object the tag tags. Perhaps it does this. Anyway, that is part of what is seen in the video.
 
Video is at youtube: https://www.youtube.com/watch?v=_x7yyx4Yk_I

Code is at repository: https://github.com/akseidel/WTA_MECH
 
Other items of interest are "Host face normal direction used to drive symbology orientation" and "User settings for family names, types and tag parameter names". The first is a way used to orient based on which direction the host face faces. The idea could be used with a complex family that is capable of rotating within itself to produce a face based family placement that is "always level" so to speak. Only a crude family is used in the demonstration but the code behind does provide the necessary angle to "level". The second is an attempt to externalize family names, family types and parameter names outside of the hard coded add-in. The need is necessary for any type of task related ribbon customization where aspects to the task are not static.   



akseidel_MechRibbonTab.png


<center>
<img src="img/.png" alt="" width="500"/>
</center>


# WTA_MECH

![RibbonTab](/MechRibbonTab.PNG)

Revit Add-in in c# &mdash; Creates a custom ribbon Tab with discipline related tools. The tools at this writing are for placing specific Revit family types with some families requiring parameter settings made on the fly.

This repository is provided for sharing and learning purposes. Perhaps someone might provide improvements or education. Perhaps it will help to boost someone further up the steep learning curve needed to create Revit task add-ins. Hopefully it does not show too much of the wrong way.  

Used by the tools in this ribbon are classes intended to provide a Revit family instance placement without the Revit user interface overhead normally required by Revit. The classes are intended to provide a universal mechanism for placing some types of Revit families. This includes tags, which is a task not in this discipline tab but is in other discipline add-ins. The custom tab employs menu methods not commonly explained, for example a split button sets a family placement mode that is exposed to the functions called by command picks. Other tools use add-in application settings as a way to persist settings or communicate to code that runs subsequently within a command that provides a task workflow.

This add-in demonstrates many of the typical tasks and implementation required for providing a tab menu interface involving family placement, e.g.:

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

## Specific Interest

**Hybrid splitbutton behavior** &mdash; A splitbutton control having four buttons where the top two buttons execute one of two types of the same task and the bottom two buttons invoke a settings panel for one of the corresponding task types. The top two buttons, being tasks the user is likely going to repeat, remain as the last button selected. The bottom two buttons are incidental tasks the user would not need to repeat nor want to be the splitbutton's face button. The splitbutton face button reverts back to what is was before the user has selected one of the bottom two button. That is the hybrid behavior. This idea was discussed here: [thebuildingcoder - stacked-ribbon-button-panel-options][dba09e78]

  [dba09e78]: http://thebuildingcoder.typepad.com/blog/2016/09/stacked-ribbon-button-panel-options.html "thebuildingcoder - stacked-ribbon-button-panel-options"

**Host face normal direction used to drive symbology orientation** &mdash; The HVAC sensor family placed by the tools has two symbology orientations in the family because Revit does not really support a universal "view independent text". The family's symbology would be wrong 50% of the time it were placed. The code attempts to use the host face normal direction to more correctly pick which of the two orientations to make visible. Each orientation's visibility is controlled by a parameter. Only one parameter is needed to be set because the other parameter is function controlled in the family to be not(the_other).

**User settings for family names, types and tag parameter names** &mdash; As a way to side step the problem of hard coding family names, types and tag parameter names the user can set the names to some other values. Not implemented, but intended, is for an external file text to also house the standard settings.

**Remote equipment tag** &mdash; Sensors, like thermostats for example, are often the client to a piece of a mechanical system. The mechanical system's name the sensor is a client for, an ID number for example, is typically placed next to the sensor on construction plans. That ID number is part of the mechanical equipment's properties but not the sensor's properties unless it where duplicated to the sensor. Therefore tagging the sensor using the latter method involves more effort and maintenance. The remote equipment tag is a tag to the mechanical equipment but placed at the sensor. That is what this add-in is doing. It would be nice if Revit were to support an indirect tag value much like the way a microprocessor does indirect addressing. Then the sensor could be tagged with a tag that gets its values from the parameter values of an indirectly addressed object.

**Multiple view annotation placement** &mdash; This add-in can place the annotations, the tags, simultaneously in more than one view at a time. The tag final placement code iterates through a list of candidate views when it places the tag. That list is developed by a single function using specific rules, which one would have to devise to their needs, that operate on parent view names. For example if the active view is a dependent view then its parent view name is used in the candidate logic. The reason for this feature is due to one of Revit's worst usability problems where annotation is relegated to the output composition task and not visible during the more important design and engineering tasks.



#### <a name="3"></a>


<pre class="prettyprint">
</pre>



The code above is included in
the [ForgeFader](https://github.com/jeremytammik/forgefader) project
in [release 0.0.16](https://github.com/jeremytammik/forgefader/releases/tag/0.0.16) and later.



