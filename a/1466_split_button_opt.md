<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

[aksaks](http://forums.autodesk.com/t5/user/viewprofilepage/user-id/540057)

Stacked Ribbon Button Panel Options #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

Topic: a C# .NET Revit add-in demonstrating use of a split ribbon button to access a secondary command, e.g., option settings. In the Revit API discussion forum thread on using a <code>StackButton</code> as a <code>PushButton</code> with options, Allan 'aksaks' Seidel recently proposed a neat UI trick, saying: Perhaps this idea might be usable for others. The <code>StackButton</code> ribbon control is a stack of different <code>PushButton</code> instances where the last used <code>PushButton</code> remains visible to be used again. That visible button is reflected in the stack button's <code>CurrentButton</code> property. Imagine if the <code>StackButton</code> control always shows the first button in the stack, and the other button(s) are secondary to the first button's purpose...

&ndash; ...

-->

### Stacked Ribbon Button Panel Options

Topic: a C# .NET Revit add-in demonstrating use of a split ribbon button to access a secondary command, e.g., option settings.

In the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on using [a StackButton as a PushButton with options](http://forums.autodesk.com/t5/revit-api/a-stackbutton-can-be-a-pushbutton-with-options/td-p/6530274),
[Allan 'aksaks' Seidel](http://wmtao.com) recently proposed a neat UI trick, saying:

Perhaps this idea might be usable for others.
 
The `StackButton` ribbon control is a stack of different `PushButton` instances where the last used `PushButton` remains visible to be used again. That visible button is reflected in the stack button's `CurrentButton` property. Imagine if the `StackButton` control always shows the first button in the stack, and the other button(s) are secondary to the first button's purpose.
 
Using the call-back concept described in The Building Coder suggestion 
to [roll your own toggle button](http://thebuildingcoder.typepad.com/blog/2012/11/roll-your-own-toggle-button.html), you can have the second `Pushbutton` in a two-button `StackButton` reset the current button property back to that of the first button. Therefore, this `StackButton` item always shows and activates the first button's action on its button face, but also has a secondary option to invoke a settings manager activated by the second button.
 
Set the first button in the button stack to your add-in command of choice. Set the second button in the button stack to show a Windows Forms or WPF window to be the first button's settings manager. Have the settings communicated through the add-in's Properties.Settings. The first button's command reads the current settings prior to acting. The second button's actions reads, sets and saves the settings the first button uses. It ends with a call-back function that resets the StackButton's current button to the first button in the stack. These settings would also persist between Revit sessions.
 
For example, this is what a second button might invoke:

<pre class="code">
[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">SectionFarClipResetOptions</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;uiapp.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;thisDoc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FarClipSettingWPF</span>&nbsp;settingWPF&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FarClipSettingWPF</span>(&nbsp;thisDoc&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;settingWPF.ShowDialog();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AppFarClip</span>.Instance.SetSplitButtonFarClipToTop();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;}
}
</pre>

This is what the call-back function might be in a hardcoded style:
 
<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetSplitButtonFarClipToTop()&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">PushButton</span>&gt;&nbsp;sbList&nbsp;=&nbsp;sbFarClip.GetItems();
&nbsp;&nbsp;&nbsp;&nbsp;sbFarClip.CurrentButton&nbsp;=&nbsp;sbList[0];
&nbsp;&nbsp;}
</pre>

`sbFarClip` is a public `SplitButton` in this case.

Many thanks to Allan for this neat suggestion and sample code.

He provided the <a href="zip/as_SplitButtonOptionConcept.zip">SplitButtonOptionConcept</a>Visual Studio solution implementing a full sample Revit add-in demonstrating the concept.

I created the new [SplitButtonOptionConcept GitHub repository](https://github.com/jeremytammik/SplitButtonOptionConcept) for it to live in.

His original solution is set up to compile with the Revit 2013 API assemblies and be automatically installed in the post-build event for Revit 2015 and Revit 2016.

I stored that initial version as [release 2016.0.0.0](https://github.com/jeremytammik/SplitButtonOptionConcept/releases/tag/2016.0.0.0).

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

As Allan says, perhaps this idea is usable for others as well.

I plan to use it right away for my Revit add-in complementing Kean Walmsley's [entry for Autodesk’s first Global Hackathon: a HoloLens-based tool for navigating low visibility environments](http://through-the-interface.typepad.com/through_the_interface/2016/08/my-entry-for-autodesks-first-global-hackathon-a-hololens-based-tool-for-navigating-low-visibility-environments.html),
part of his [HoloLens project series](http://through-the-interface.typepad.com/through_the_interface/hololens), including and not limited to:

- [Using HoloLens to display diagnostic information for building components](http://through-the-interface.typepad.com/through_the_interface/2016/08/using-hololens-to-display-diagnostic-information-for-building-components.html)
- [Scaling our Unity model in HoloLens](http://through-the-interface.typepad.com/through_the_interface/2016/08/scaling-our-unity-model-in-hololens.html)
- [Adding spatial sound to our Unity model in HoloLens](http://through-the-interface.typepad.com/through_the_interface/2016/08/adding-spatial-sound-to-our-unity-model-in-hololens-part-3.html)
- [Displaying your Unity model in 3D using HoloLens](http://through-the-interface.typepad.com/through_the_interface/2016/07/displaying-your-unity-model-in-3d-using-hololens.html)

Many thanks again to Allan, have fun yourself, and please wish me and Kean lots of luck, fun and success in our hackathon efforts during the next two days.

#### <a name="2"></a>Addendum &ndash; Simpler Solution

Please do not overlook the much simpler solution to this task provided directly by the Revit API and pointed out in
the [comment](http://thebuildingcoder.typepad.com/blog/2016/09/stacked-ribbon-button-panel-options.html#comment-2880240616) below by Ehsan Iran Nejad:

> Or you can just set the `RibbonItem.IsSynchronizedWithCurrentItem` to `False` after creating the `SplitButton`. The first button will be fixed and all the other buttons can only be used from the dropdown menu.

#### <a name="3"></a>Not so fast...

Allan responds to the simpler solution suggestion:

> Not so fast. Perhaps that is missing the forest because of the trees. The example is just a two button split button where the second button is the settings button. Consider that split button having three or more buttons where the last button is the settings button and you want the split button to show the last button activated, but never the settings button. The other method no longer works, but the same concepts used in the roll your own method do work. One way, and there may be better ones, takes advantage of the splitbutton's current button value not yet having changed when the newly selected settings button is activated. That name can be saved and then used just before the command return to reset the split button back to what was showing when the used pulled the split button down to select the settings button.
