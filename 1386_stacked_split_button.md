<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- implement split button using AdWindows.dll
  http://forums.autodesk.com/t5/revit-api/ribbonpanel-addstackeditems-and-splitbutton/m-p/5950120
  intentional user interface restrictions
  REVIT-71373 [As a Revit add-in developer, I would like to be able to add a split button to a stacked section of buttons, so I can create the UI I need for my application.]

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#rtceur
#geometry #3d
#xaml

Revit API, Jeremy Tammik, akn_include

Adding a Stacked Split Button to the Ribbon #revitapi #bim #aec #3dwebcoder #adsk

I have been rather busy on the Revit API discussion forum these last few days. One of the issues I got involved with was Michał Helt's thread on RibbonPanel.AddStackedItems and SplitButton, discussing how to add a split button to a ribbon panel using the AddStackedItems method. Question: Currently, Split Buttons cannot be created in the ribbon using the AddStackedItems method. Only PushButtons, PulldownButtons, ComboBoxes and TextBoxes can be added this way. Would it be possible to remove this limitation?...

-->

### Adding a Stacked Split Button to the Ribbon

I have been rather busy on the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) these last few days.

One of the issues I got involved with was Michał Helt's thread
on [RibbonPanel.AddStackedItems and SplitButton](http://forums.autodesk.com/t5/revit-api/ribbonpanel-addstackeditems-and-splitbutton/m-p/5949953),
discussing how to add a split button to a ribbon panel using the AddStackedItems method.

**Question:** Currently, Split Buttons cannot be created in the ribbon using the AddStackedItems method.
Only PushButtons, PulldownButtons, ComboBoxes and TextBoxes can be added this way.
Would it be possible to remove this limitation?
I would like to achieve a button similar to this selected one:

<center>
<img src="img/split_button.png" alt="Split button" width="438">
</center>

I've already overcome this using the ribbon features from AdWindows.dll, but I am curious whether the lack of it in RevitAPIUI.dll is intentional or not.

**Answer:** Thank you for reporting this!

This issue has already been logged as REVIT-71373 *As a Revit add-in developer, I would like to be able to add a split button to a stacked section of buttons, so I can create the UI I need for my application* and is currently being addressed, so you will have official access to this functionality in future.

**Response:** Thank you.

Here is my current workaround making use of
the [unsupported](http://thebuildingcoder.typepad.com/blog/about-the-author.html#4) `AdWindows.dll` functionality:

<pre class="code">
&nbsp; <span class="blue">var</span> bd0 = <span class="blue">new</span> <span class="teal">PulldownButtonData</span>( <span class="maroon">&quot;A&quot;</span>, <span class="maroon">&quot;A&quot;</span> );
&nbsp; <span class="blue">var</span> bd1 = <span class="blue">new</span> <span class="teal">PulldownButtonData</span>( <span class="maroon">&quot;B&quot;</span>, <span class="maroon">&quot;B&quot;</span> );
&nbsp; <span class="blue">var</span> bd2 = <span class="blue">new</span> <span class="teal">PulldownButtonData</span>( <span class="maroon">&quot;C&quot;</span>, <span class="maroon">&quot;C&quot;</span> );
&nbsp;
&nbsp; <span class="blue">var</span> stackedItems = ribbonPanel.AddStackedItems(
&nbsp; &nbsp; bd0, bd1, bd2 );
&nbsp;
&nbsp; <span class="blue">var</span> button0 = (<span class="teal">PulldownButton</span>) stackedItems[0];
&nbsp;
&nbsp; <span class="blue">string</span> sid = <span class="blue">string</span>.Join(
&nbsp; &nbsp; <span class="maroon">&quot;%&quot;</span>,
&nbsp; &nbsp; <span class="maroon">&quot;CustomCtrl_&quot;</span>,
&nbsp; &nbsp; <span class="maroon">&quot;CustomCtrl_&quot;</span>,
&nbsp; &nbsp; ribbonTabName,
&nbsp; &nbsp; ribbonPanel.Name,
&nbsp; &nbsp; button0.Name );
&nbsp;
&nbsp; <span class="blue">var</span> splitButton = (Autodesk.Windows.<span class="teal">RibbonSplitButton</span>)
&nbsp; &nbsp; UIFramework.RevitRibbonControl.RibbonControl
&nbsp; &nbsp; &nbsp; .findRibbonItemById( sid );
&nbsp;
&nbsp; splitButton.IsSplit = <span class="blue">true</span>;
&nbsp; splitButton.IsSynchronizedWithCurrentItem = <span class="blue">true</span>;
</pre>

That's the cleanest solution that I have found so far.

Thank you, Michał, for raising the issue and sharing this solution.

Don't forget to replace this by the official solution once the new functionality mentioned above becomes available.

<!---

Other threads:

http://forums.autodesk.com/t5/Revit-API/How-to-read-Browser-Organization-info-from-the-Revit-API-2013/m-p/5028076
http://forums.autodesk.com/t5/revit-api/3d-sweep-path/m-p/5909779
http://forums.autodesk.com/t5/revit-api/access-opened-files-on-revit-application/m-p/5948657
http://forums.autodesk.com/t5/revit-api/access-rebar-element-through-rebar-container/m-p/5925040
http://forums.autodesk.com/t5/revit-api/api-bug-in-document-regenerate/m-p/5911198
http://forums.autodesk.com/t5/revit-api/boundingboxintersectsfilter-not-working/m-p/5912235
http://forums.autodesk.com/t5/revit-api/c-add-a-type-in-a-family/m-p/5934695
http://forums.autodesk.com/t5/revit-api/c-c/m-p/5949029
http://forums.autodesk.com/t5/revit-api/call-revit-s-api-from-external-thread/m-p/5941308
http://forums.autodesk.com/t5/revit-api/catching-column-faces-problem/m-p/5949315
http://forums.autodesk.com/t5/revit-api/change-parameter-values-from-modaldialog/m-p/5950302
http://forums.autodesk.com/t5/revit-api/change-the-selection-of-a-wall/m-p/5894008
http://forums.autodesk.com/t5/revit-api/circle-radius-alignment-and-dimension/m-p/5905736
http://forums.autodesk.com/t5/revit-api/closing-a-model-without-creating-a-backupfile/m-p/5911656
http://forums.autodesk.com/t5/revit-api/create-non-shared-project-parameters/m-p/5908682
http://forums.autodesk.com/t5/revit-api/create-project-parameter-with-quot-values-can-vary-by-group/m-p/5939455
http://forums.autodesk.com/t5/revit-api/creating-a-new-line-style/m-p/5943347
http://forums.autodesk.com/t5/revit-api/disable-current-view-only-in-link-cad-command/m-p/5918755
http://forums.autodesk.com/t5/revit-api/disable-or-overwrite-quot-editing-request-quot-for-worksharing/m-p/5920035
http://forums.autodesk.com/t5/revit-api/documentation-inquery/m-p/5952369
http://forums.autodesk.com/t5/revit-api/energy-analysis-and-gbs-license-period/m-p/5922082
http://forums.autodesk.com/t5/revit-api/export-family-to-dwg/m-p/5921317
http://forums.autodesk.com/t5/revit-api/filteredelementcollector-or-elementset-order-by-closest-of-same/m-p/5943350
http://forums.autodesk.com/t5/revit-api/get-all-familyinstances-in-a-room/m-p/5943551
http://forums.autodesk.com/t5/revit-api/get-parameter-value-for-a-collection-of-family-instances/m-p/5896191
http://forums.autodesk.com/t5/revit-api/get-wall-openings/m-p/5953513
http://forums.autodesk.com/t5/revit-api/getting-started-with-the-revit-net-api/m-p/1506776
http://forums.autodesk.com/t5/revit-api/how-to-get-all-disciplines/m-p/5951718
http://forums.autodesk.com/t5/revit-api/how-to-loop-through-the-design-option-sets-of-a-document/m-p/5950656
http://forums.autodesk.com/t5/revit-api/how-to-make-a-graphical-view-active/m-p/5891780
http://forums.autodesk.com/t5/revit-api/how-to-read-browser-organization-info-from-the-revit-api-2013/m-p/5894209
http://forums.autodesk.com/t5/revit-api/installation-errors/m-p/5926697
http://forums.autodesk.com/t5/revit-api/instancevoidcututils-during-dynamic-update-event/m-p/5891597
http://forums.autodesk.com/t5/revit-api/invalid-face-references-after-regenerate/m-p/5893667
http://forums.autodesk.com/t5/revit-api/invalidoperation-exception/m-p/5894269
http://forums.autodesk.com/t5/revit-api/loop-through-elements-and-set-parameter/m-p/5919866
http://forums.autodesk.com/t5/revit-api/loop-trough-elements-and-set-parameter/m-p/5919860
http://forums.autodesk.com/t5/revit-api/openings-on-roof-in-window-family-via-api/m-p/5950116
http://forums.autodesk.com/t5/revit-api/path-to-the-dll/m-p/5953600
http://forums.autodesk.com/t5/revit-api/placing-cutouts-generic-families/m-p/5947220
http://forums.autodesk.com/t5/revit-api/reference-plane-on-drafting-view/m-p/5897179
http://forums.autodesk.com/t5/revit-api/revit-color-picker/m-p/5887112
http://forums.autodesk.com/t5/revit-api/revit-to-ifc-unrecoverable-error-during-export/m-p/5946695
http://forums.autodesk.com/t5/revit-api/revit2016-service-pack2-not-active-after-update/m-p/5897691
http://forums.autodesk.com/t5/revit-api/ribbonpanel-addstackeditems-and-splitbutton/m-p/5949953
http://forums.autodesk.com/t5/revit-api/rotate-viewsection/m-p/5921812
http://forums.autodesk.com/t5/revit-api/set-value-of-parameter-editfamily-fails/m-p/5947155
http://forums.autodesk.com/t5/revit-api/shared-coordinate-system-definition/m-p/5928300
http://forums.autodesk.com/t5/revit-api/synchronizing-with-a-centrale-file-revit-located-in-a-database/m-p/5942784
http://forums.autodesk.com/t5/revit-api/trigger-when-adding-new-element/m-p/5952018
http://forums.autodesk.com/t5/revit-api/trying-to-modify-the-family-parameter-quot-work-plane-based-quot/m-p/5900742
http://forums.autodesk.com/t5/revit-api/update-linked-files-path/m-p/5950215
http://forums.autodesk.com/t5/revit-api/visual-studio-2015-sp1/m-p/5933391
http://forums.autodesk.com/t5/revit-api/wall-attach-top-base-no-api/m-p/5929145
http://forums.autodesk.com/t5/revit-api/workshared-element-user-history/m-p/5897153
https://forums.autodesk.com/t5/revit-api/change-the-selection-of-a-wall/m-p/5890510
https://forums.autodesk.com/t5/revit-api/reporting-on-project-parameter-definitions-need-guids/m-p/4706047
-->
