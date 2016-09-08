<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

https://github.com/jeremytammik/ExportWaypointsJson

 #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

&ndash; ...

-->

### HoloLens Escape Path Waypoint Exporter

External application with one main command and an option settings command displaying a form, validating input and storing the user preferences

Implementing an ribbon panel with a main command and a subsidiary option settings button
Storing add-in option settings in XML using the .NET System.Configuration.ApplicationSettingsBase class
Storing add-in option settings in JSON using custom solution and JavaScriptSerializer class
Implementing form validation using ErrorProvider class, Validating and Validated events
Parametrically traversing a curve
Serialisation of two-digit XYZ coordinates
Converting XYZ point to metres

<pre class="code">
</pre>


I created the new [SplitButtonOptionConcept GitHub repository](https://github.com/jeremytammik/SplitButtonOptionConcept) for it to live in.


Next, I migrated and tested it in Revit 2017, tagging that as [release 2017.0.0.0](https://github.com/jeremytammik/SplitButtonOptionConcept/releases/tag/2017.0.0.0).

The add-in displays the following ribbon panel:

<center>
<img src="img/split_button_options_panel.png" alt="SplitButtonOptionConcept ribbon panel" width="107">
</center>

You can either click the main button, which is always displayed at the top as the current option, to trigger the main command, or drop down the rest of the stacked button contents to display the option button:

<center>
<img src="img/split_button_options_buttons.png" alt="SplitButtonOptionConcept buttons" width="108">
</center>

The current version is [release 2017.0.0.2](https://github.com/jeremytammik/SplitButtonOptionConcept/releases/tag/2017.0.0.2) including
some further minor clean-up.


#### <a name="2"></a>

