<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

https://github.com/jeremytammik/ExportWaypointsJson

https://t.co/8F1cZuEpTI

https://t.co/uhLYBlue2x

HoloLens Escape Path Waypoint JSON Exporter #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

Last week, I worked with Kean Walmsley on his entry for the first global Autodesk Hackathon: a HoloLens-based tool for navigating low visibility environments, resulting in the new HoloGuide Autodesk Hackathon project. My modest contribution is the ExportWaypointsJson Revit add-in, an external application implementing the main command, exporting the waypoints, and a subsidiary option settings command, displaying a form, validating input and storing the user preferences. The main command simply prompts the user to pick a model line in the Revit project,  traverses it, determines waypoints at predefined intervals and exports them to JSON for consumption by the HoloGuide visualisation...

-->

### HoloLens Escape Path Waypoint JSON Exporter

Last week, I worked with Kean Walmsley on
his [entry for the first global Autodesk Hackathon: a HoloLens-based tool for navigating low visibility environments](http://through-the-interface.typepad.com/through_the_interface/2016/08/my-entry-for-autodesks-first-global-hackathon-a-hololens-based-tool-for-navigating-low-visibility-environments.html), resulting in the
new [HoloGuide Autodesk Hackathon project](http://through-the-interface.typepad.com/through_the_interface/2016/09/hologuide-an-autodesk-hackathon-project.html).

Watch this [three-minute video](https://www.youtube.com/watch?v=8MjBgiQZUzw) for a quick first impression of what it is all about:

<center>
<iframe width="440" height="248" src="https://www.youtube.com/embed/8MjBgiQZUzw?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

This project is part of Kean's [HoloLens project series](http://through-the-interface.typepad.com/through_the_interface/hololens), including and not limited to:

- [Using HoloLens to display diagnostic information for building components](http://through-the-interface.typepad.com/through_the_interface/2016/08/using-hololens-to-display-diagnostic-information-for-building-components.html)
- [Scaling our Unity model in HoloLens](http://through-the-interface.typepad.com/through_the_interface/2016/08/scaling-our-unity-model-in-hololens.html)
- [Adding spatial sound to our Unity model in HoloLens](http://through-the-interface.typepad.com/through_the_interface/2016/08/adding-spatial-sound-to-our-unity-model-in-hololens-part-3.html)
- [Displaying your Unity model in 3D using HoloLens](http://through-the-interface.typepad.com/through_the_interface/2016/07/displaying-your-unity-model-in-3d-using-hololens.html)

My modest contribution is the [ExportWaypointsJson Revit add-in](https://github.com/jeremytammik/ExportWaypointsJson), an external application implementing the main command, exporting the waypoints, and a subsidiary option settings command, displaying a form, validating input and storing the user preferences.

The main command and the main functionality of the add-in simply prompts the user to pick a model line in the Revit project,  traverses it, determines waypoints at predefined intervals and exports them to JSON for consumption by the HoloGuide visualisation.

In the process of implementing it, I was able to explore an issue I never looked at before, validating user input to a Windows form using the `ErrorProvider` class, `Validating` and `Validated` events.

I also implemented two different approaches to store the user-defined add-in option settings, in XML and JSON.

Today, I'll just present the external application class creating the ribbon panel with split button providing access to two commands. It reads the ribbon button icons from embedded resources and provides a method to ensure that the main command always remains the default current button.

I used the [stacked ribbon button panel  SplitButtonOptionConcept](http://thebuildingcoder.typepad.com/blog/2016/09/stacked-ribbon-button-panel-options.html) approach to retain the main command button as the current default split button option. Thus the main command is always displayed at the top and immediately accessible with a single click, while access to the other subsidiary commands requires opening the split button drop-down options first.

I'll just present the main external application implementation of this add-in today.

If you would like to explore the areas that I have not covered today right away on your own, feel free to clone, compile and debug the project from
the [ExportWaypointsJson GitHub repository](https://github.com/jeremytammik/ExportWaypointsJson).

The version discussed here
is [release 2017.0.0.11](https://github.com/jeremytammik/ExportWaypointsJson/releases/tag/2017.0.0.11).


#### <a name="2"></a>External Application Implementation

The add-in displays the following ribbon panel:

<center>
<img src="img/hololens_exit_path_ribbon_panel.png" alt="ExportWaypointsJson ribbon panel" width="378">
</center>

You can either click the main button, which is always displayed at the top as the current option, to trigger the main command, or drop down the rest of the stacked button contents to display the option button:

<center>
<img src="img/hololens_exit_path_ribbon_buttons.png" alt="ExportWaypointsJson main command and settings buttons" width="139">
</center>

I ran the application in a model of the Autodesk office building in Neuch&acirc;tel:

<center>
<img src="img/hololens_adsk_office_bldg_3d.png" alt="Autodesk office building in Neuch&acirc;tel" width="360">
</center>

The escape path we use for demonstration leads from Kean's desk to the nearest exit door:

<center>
<img src="img/hololens_adsk_3rd_floor_plan.png" alt="HoloGuide exit path plan view" width="360">
</center>

It is represented by a model curve:

<center>
<img src="img/hololens_adsk_3rd_floor_3d.png" alt="HoloGuide exit path 3D view" width="415">
</center>

The external command prompts you to select the exit path waypoints model curve, unless you pre-selected one before launching it, traverses it, determines waypoints and exports the results to JSON.

To report success, it displays the number of points exported:

<center>
<img src="img/hololens_exit_path_result_msg.png" alt="HoloGuide exit path report message" width="373">
</center>

Each waypoint can optionally also be represented by a marker in the view you generated it from:

<center>
<img src="img/hololens_exit_path_result_markers.png" alt="HoloGuide exit path markers" width="400">
</center>

The marker generation option can also be switched off.

If you do so, the command can run in read-only mode.

Right now, the transaction mode is set to manual, and a transaction is only started if the waypoint markers are requested.

Here is the entire external application implementation, which:

- Creates the ribbon tab and buttons.
- Populates the button icons from embedded resources.
- Ensures that the main command button always remains the current and default split button option.


<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">App</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalApplication</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">const</span>&nbsp;<span style="color:blue;">string</span>&nbsp;Caption&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Waypoints&quot;</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SplitButton</span>&nbsp;split_button;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;This&nbsp;external&nbsp;application&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;singleton&nbsp;class&nbsp;instance.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">internal</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">App</span>&nbsp;_app&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Provide&nbsp;access&nbsp;to&nbsp;this&nbsp;class&nbsp;instance.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">App</span>&nbsp;Instance
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>&nbsp;{&nbsp;<span style="color:blue;">return</span>&nbsp;_app;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;the&nbsp;full&nbsp;add-in&nbsp;assembly&nbsp;folder&nbsp;path.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;Path
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;System.IO.<span style="color:#2b91af;">Path</span>.GetDirectoryName(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Assembly</span>.GetExecutingAssembly().Location&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">#region</span>&nbsp;Create&nbsp;Ribbon&nbsp;Tab
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Load&nbsp;a&nbsp;new&nbsp;icon&nbsp;bitmap&nbsp;from&nbsp;embedded&nbsp;resources.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;For&nbsp;the&nbsp;BitmapImage,&nbsp;make&nbsp;sure&nbsp;you&nbsp;reference&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;WindowsBase&nbsp;and&nbsp;PresentationCore,&nbsp;and&nbsp;import&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;the&nbsp;System.Windows.Media.Imaging&nbsp;namespace.&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapImage</span>&nbsp;NewBitmapImage(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Reflection.<span style="color:#2b91af;">Assembly</span>&nbsp;a,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;imageName&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Stream</span>&nbsp;s&nbsp;=&nbsp;a.GetManifestResourceStream(&nbsp;imageName&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapImage</span>&nbsp;img&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BitmapImage</span>();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;img.BeginInit();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;img.StreamSource&nbsp;=&nbsp;s;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;img.EndInit();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;img;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">void</span>&nbsp;CreateRibbonTab(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Assembly</span>&nbsp;assembly&nbsp;=&nbsp;<span style="color:#2b91af;">Assembly</span>.GetExecutingAssembly();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;ass_path&nbsp;=&nbsp;assembly.Location;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;ass_name&nbsp;=&nbsp;assembly.GetName().Name;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;ribbon&nbsp;tab&nbsp;</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;tab_name&nbsp;=&nbsp;Caption;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a.CreateRibbonTab(&nbsp;tab_name&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;Autodesk.Revit.Exceptions.<span style="color:#2b91af;">ArgumentException</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Assume&nbsp;error&nbsp;is&nbsp;due&nbsp;to&nbsp;tab&nbsp;already&nbsp;existing</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PushButtonData</span>&nbsp;pbCommand&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Export&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Export&quot;</span>,&nbsp;ass_path,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ass_name&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.Command&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PushButtonData</span>&nbsp;pbCommandOpt&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Settings&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Settings&quot;</span>,&nbsp;ass_path,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ass_name&nbsp;+&nbsp;<span style="color:#a31515;">&quot;.CmdSettings&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pbCommand.LargeImage&nbsp;=&nbsp;NewBitmapImage(&nbsp;assembly,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;ExportWaypointsJson.iCommand.png&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pbCommandOpt.LargeImage&nbsp;=&nbsp;NewBitmapImage(&nbsp;assembly,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;ExportWaypointsJson.iCmdSettings.png&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;button&nbsp;tips&nbsp;(when&nbsp;data,&nbsp;must&nbsp;be&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;defined&nbsp;prior&nbsp;to&nbsp;adding&nbsp;button.)</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pbCommand.ToolTip&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Export&nbsp;waypoints&nbsp;to&nbsp;JSON.&quot;</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pbCommand.LongDescription&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Export&nbsp;exit&nbsp;path&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;guide&nbsp;waypoints&nbsp;to&nbsp;JSON&nbsp;for&nbsp;Hololens&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;visualisation.&quot;</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;&nbsp;&nbsp;Add&nbsp;new&nbsp;ribbon&nbsp;panel.&nbsp;</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;panel_name&nbsp;=&nbsp;Caption;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">RibbonPanel</span>&nbsp;thisNewRibbonPanel&nbsp;=&nbsp;a.CreateRibbonPanel(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tab_name,&nbsp;panel_name&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;add&nbsp;button&nbsp;to&nbsp;ribbon&nbsp;panel</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SplitButtonData</span>&nbsp;split_buttonData
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">SplitButtonData</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;splitFarClip&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;FarClip&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;split_button&nbsp;=&nbsp;thisNewRibbonPanel.AddItem(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;split_buttonData&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">SplitButton</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;split_button.AddPushButton(&nbsp;pbCommand&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;split_button.AddPushButton(&nbsp;pbCommandOpt&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Reset&nbsp;the&nbsp;top&nbsp;button&nbsp;to&nbsp;be&nbsp;the&nbsp;current&nbsp;one.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Alternative&nbsp;solution:&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;set&nbsp;RibbonItem.IsSynchronizedWithCurrentItem&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;to&nbsp;false&nbsp;after&nbsp;creating&nbsp;the&nbsp;SplitButton.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetTopButtonCurrent()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">PushButton</span>&gt;&nbsp;sbList&nbsp;=&nbsp;split_button.GetItems();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;split_button.CurrentButton&nbsp;=&nbsp;sbList[0];
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;Ribbon&nbsp;Tab</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;OnStartup(&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_app&nbsp;=&nbsp;<span style="color:blue;">this</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CreateRibbonTab(&nbsp;a&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;OnShutdown(&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

As said, the ExportWaypointsJson add-in implements a couple of other noteworthy features that we may highlight in future discussions, including:

- Main external command
    - Model curve selection
    - Parametric curve traversal
    - Conversion of XYZ points in feet to metres
    - Serialisation of two-digit truncated XYZ coordinates
    - JSON export of data using `JavaScriptSerializer` class
- Subsidiary external settings command
    - Display modal Windows form 
    - Implement form validation using `ErrorProvider` class, `Validating` and `Validated` events
    - Store add-in option settings in XML using the .NET `System.Configuration.ApplicationSettingsBase` class
    - Store add-in option settings in JSON using custom solution and `JavaScriptSerializer` class

The entire add-in project is available from 
the [ExportWaypointsJson GitHub repository](https://github.com/jeremytammik/ExportWaypointsJson),
and the version discussed here
is [release 2017.0.0.11](https://github.com/jeremytammik/ExportWaypointsJson/releases/tag/2017.0.0.11).

Have fun!

Thank you very much, Kean, for the inspiring idea and nice video!
