<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

http://forums.autodesk.com/t5/revit-api-forum/can-t-obtain-macromanager/m-p/6557705

MacroManager, Materials and Getting Started #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

I am answering Revit API discussion forum threads and developer cases like crazy. Instead, as usual at this time of the year, I should be focussing and putting higher priority on the exciting upcoming events. I keep promising myself to do so and stop answering cases. Then I address just one more... Here are some of the upcoming events and recent Revit API issues
&ndash; Events &ndash; Forge Accelerators, DevDay, RTC and AU
&ndash; Accessing the MacroManager to Delete Document Macros
&ndash; Getting Started and Changing the Colour of a Wall
&ndash; Getting Started and Using the Visual Studio Revit Add-In Wizard Auto-Installer...

-->

### MacroManager, Materials and Getting Started

I am answering [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) threads
and developer cases like crazy.

Instead, as usual at this time of the year, I should be focussing and putting higher priority on the exciting upcoming events.

I keep promising myself to do so and stop answering cases. Then I address just one more...

Here are some of the upcoming events and recent Revit API issues:

- [Events &ndash; Forge Accelerators, DevDay, RTC and AU](#2)
- [Accessing the MacroManager to Delete Document Macros](#3)
- [Getting Started and Changing the Colour of a Wall](#4)
- [Getting Started and Using the Visual Studio Revit Add-In Wizard Auto-Installer](#5)

#### <a name="2"></a>Events &ndash; Forge Accelerators, DevDay, RTC and AU

Here are my main upcoming events:

- Oct. 19 &ndash; Forge and BIM, Porto University
- Oct. 20-22 &ndash; RTCEU Revit Technology Conference Europe, Porto
- Oct. 24-28 &ndash; Forge Accelerator, Munich
- Nov. 4 &ndash; Forge and BIM Workshop, Darmstadt University
- Nov. 14-17 &ndash; Autodesk university, Las vegas
- Dec. 5 &ndash; DevDay Europe, Munich
- Dec. 6-9 &ndash; Forge Accelerator, Munich

I'll present my projects and material for these as soon as I get around to preparing them.

Real soon now!


#### <a name="3"></a>Accessing the MacroManager to Delete Document Macros

Next, I address
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [obtaining the MacroManager](http://forums.autodesk.com/t5/revit-api-forum/can-t-obtain-macromanager/m-p/6557705)
as well as [Frederic's comment](http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html#comment-2842394984)
on [What's New in the Revit 2014 API](http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html):

**Question:** I'm stuck with getting MacroManager object.
There are much more class members listed in API reference then really available.
What am I doing wrong?

<center>
<img src="img/macro_manager_1.png" alt="Macros namespace" width="300">
</center>
 
Or is there any other way to delete all macros from document?

**Jeremy says:** I should think this can be done quite easily. Look
at [What's New in the Revit 2014 API](http://thebuildingcoder.typepad.com/blog/2013/04/whats-new-in-the-revit-2014-api.html) and
search for **MacroManager API**.

**Revitalizer answers:** Add `RevitAPIMacros.dll` to your VS project.

**Response:** Revitalizer, thank you very much! It's got to be that easy I knew it :-)

Actually, a reference to `RevitAPIMacrosInterop.dll` solved the problem at last (not to `RevitAPIMacros.dll`)!

**Revitalizer answers:** I cannot believe that `RevitAPIMacrosInterop.dll` solves the problem since it does not contain the `MacroManager` definition.

`RevitAPIMacros.dll` does:

<center>
<img src="img/macro_manager_2.png" alt="RevitAPIMacros assembly" width="400">
</center>

**Jeremy says:** Look
at [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
new [CmdDeleteMacros.cs module](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdDeleteMacros.cs)
in [release 2017.0.129.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.129.0).

Just as Revitalizer suggests, I was forced to add references to `RevitAPIMacros.dll` and `RevitAPIUIMacros.dll` in The Building Coder samples Visual Studio project specifically for this command.

<pre class="code">
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.Attributes;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.DB;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.DB.Macros;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.UI;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.UI.Macros;
<span style="color:blue;">using</span>&nbsp;System.Linq;
<span style="color:blue;">using</span>&nbsp;System.Diagnostics;
 
<span style="color:blue;">namespace</span>&nbsp;BuildingCoder
{
&nbsp;&nbsp;[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.ReadOnly&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">CmdDeleteMacros</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIApplication</span>&nbsp;uiapp&nbsp;=&nbsp;commandData.Application;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIDocument</span>&nbsp;uidoc&nbsp;=&nbsp;uiapp.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIMacroManager</span>&nbsp;uiapp_mgr&nbsp;=&nbsp;<span style="color:#2b91af;">UIMacroManager</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetMacroManager(&nbsp;uiapp&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UIMacroManager</span>&nbsp;uidoc_mgr&nbsp;=&nbsp;<span style="color:#2b91af;">UIMacroManager</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetMacroManager(&nbsp;uidoc&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;nModulesApp&nbsp;=&nbsp;uiapp_mgr.MacroManager.Count;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;nModulesDoc&nbsp;=&nbsp;uidoc_mgr.MacroManager.Count;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;nMacrosDoc&nbsp;=&nbsp;uidoc_mgr.MacroManager
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Aggregate&lt;<span style="color:#2b91af;">MacroModule</span>,&nbsp;<span style="color:blue;">int</span>&gt;(&nbsp;0,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(&nbsp;n,&nbsp;m&nbsp;)&nbsp;=&gt;&nbsp;n&nbsp;+&nbsp;m.Count&lt;<span style="color:#2b91af;">Macro</span>&gt;()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>&nbsp;dlg&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TaskDialog</span>(&nbsp;<span style="color:#a31515;">&quot;Delete&nbsp;Document&nbsp;Macros&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dlg.MainInstruction&nbsp;=&nbsp;<span style="color:#a31515;">&quot;Are&nbsp;you&nbsp;really&nbsp;sure&nbsp;you&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;want&nbsp;to&nbsp;delete&nbsp;all&nbsp;document&nbsp;macros?&quot;</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dlg.MainContent&nbsp;=&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;{0}&nbsp;application&nbsp;module{1}&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;and&nbsp;{2}&nbsp;document&nbsp;macro&nbsp;module{3}&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;defining&nbsp;{4}&nbsp;macro{5}.&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nModulesApp,&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;nModulesApp&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nModulesDoc,&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;nModulesDoc&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nMacrosDoc,&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;nMacrosDoc&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dlg.MainIcon&nbsp;=&nbsp;<span style="color:#2b91af;">TaskDialogIcon</span>.TaskDialogIconWarning;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dlg.CommonButtons&nbsp;=&nbsp;<span style="color:#2b91af;">TaskDialogCommonButtons</span>.Yes
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;<span style="color:#2b91af;">TaskDialogCommonButtons</span>.Cancel;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialogResult</span>&nbsp;rslt&nbsp;=&nbsp;dlg.Show();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(<span style="color:#2b91af;">TaskDialogResult</span>.Yes&nbsp;==&nbsp;rslt&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">MacroManager</span>&nbsp;mgr&nbsp;=&nbsp;<span style="color:#2b91af;">MacroManager</span>.GetMacroManager(&nbsp;doc&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">MacroManagerIterator</span>&nbsp;it&nbsp;=&nbsp;mgr.GetMacroManagerIterator();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Several&nbsp;possibilities&nbsp;to&nbsp;iterate&nbsp;macros:</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//for(&nbsp;it.Reset();&nbsp;!it.IsDone();&nbsp;it.MoveNext()&nbsp;)&nbsp;{&nbsp;}</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//IEnumerator&lt;MacroModule&gt;&nbsp;e&nbsp;=&nbsp;mgr.GetEnumerator();</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;n&nbsp;=&nbsp;0;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">MacroModule</span>&nbsp;mod&nbsp;<span style="color:blue;">in</span>&nbsp;mgr&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;module&nbsp;&quot;</span>&nbsp;+&nbsp;mod.Name&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Macro</span>&nbsp;mac&nbsp;<span style="color:blue;">in</span>&nbsp;mod&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;macro&nbsp;&quot;</span>&nbsp;+&nbsp;mac.Name&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mod.RemoveMacro(&nbsp;mac&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;++n;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Exception&nbsp;thrown:&nbsp;&#39;Autodesk.Revit.Exceptions</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;.InvalidOperationException&#39;&nbsp;in&nbsp;RevitAPIMacros.dll</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Cannot&nbsp;remove&nbsp;the&nbsp;UI&nbsp;module</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//mgr.RemoveModule(&nbsp;mod&nbsp;);</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Document&nbsp;Macros&nbsp;Deleted&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>.Format(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;{0}&nbsp;document&nbsp;macro{1}&nbsp;deleted.&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;n,&nbsp;<span style="color:#2b91af;">Util</span>.PluralSuffix(&nbsp;n&nbsp;)&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

This command is destructive in spite of being read-only, so it first prompts you for confirmation before proceeding to delete all document macros:

<center>
<img src="img/delete_macros_confirmation.png" alt="Delete document macros confirmation" width="372">
</center>

After doing its dirty deed, it confesses and reports in full:

<center>
<img src="img/delete_macros_result_msg.png" alt="Delete document macros result message" width="372">
</center>




#### <a name="4"></a>Getting Started and Changing the Colour of a Wall

Next, let's look
at [Nalika's comment](http://thebuildingcoder.typepad.com/blog/2010/06/display-webcam-image-on-building-element-face.html#comment-2890199050)
on [displaying a webcam image on a building element face](http://thebuildingcoder.typepad.com/blog/2010/06/display-webcam-image-on-building-element-face.html):

**Question:** I'm very new to Revit and very much grateful if you could help me in solving my problem.

Currently I'm trying to change the colour of a wall according to a certain value. For example: value 20 will make the wall red when I click on it and if I click on the wall again it will be green according to a new value. The values are stored in an array and currently have 4 values. I use the sample library project `WorkThread` and it uses the same `SetupDisplayStyle` function to set the display style. There I pick the colour from array values. However, the wall is coloured in 4th colour when I click on the wall for the 4th time, and the first three times the wall isn't coloured at all. Could you please tell me in which function/method should I change/iterate through in order to have different colours (defined from array values) every time I click on a wall.

**Answer:** If you are new to the Revit API, I would strongly suggest that you work through
the [Revit API getting started material](http://thebuildingcoder.typepad.com/blog/about-the-author.html#2) first
of all, especially the DevTV and My First Revit Plugin video tutorials.

If you want to dive in deeper, go through the ADN Revit API Training Labs after that.

Then start implementing your own add-ins.

The `WorkThread` and `SetupDisplayStyle` samples do not sound suitable for what you are trying to achieve.

By the way, what are you trying to achieve, and why?

Maybe you should look at the Revit SDK sample collection first? It might contain exactly what you are looking for.

One very flexible way to set colours on specific elements is by using view filters, and there is a nice sample demonstrating how to drive that programmatically as well: `ElementFilter` / `ViewFilters`.

Before looking at the Revit API at all, you definitely need some understanding of Revit from an end user point of view.

It provides a huge amount of complex functionality right out of the box.

If you start programming Revit with insufficient understanding of the basic Revit end user functionality and the optimal workflows and best practices to make use of it efficiently is doomed for disaster.

Take heed, have fun, and good luck!


#### <a name="5"></a>Getting Started and Using the Visual Studio Revit Add-In Wizard Auto-Installer

While we are at it, we might as well also
reproduce [Juan E. Calvo Ferrándiz' comment](http://thebuildingcoder.typepad.com/blog/2016/05/visual-studio-vb-and-c-net-revit-2017-add-in-wizards.html#comment-2890797869) on
the wizard auto-installation:

**Question:** Thanks for this work! Is amazing.

The add-in wizard setting exports the `.dll` to the `bin` folder and the `users/xxx/AppData...` folder, right?

Then, it also creates two copies of the `.addin` manifest, one to the project folder and another one to the `AppData...` folder.

Revit loads the `.dll` and `.addin` from the `AppData...` folder?

Revit doesn't need the `.pdb` file? It's just additional information when troubleshooting the DLL, right?

**Answer:** Yes, exactly, correct on all points.

You, the developer, write the add-in manifest `.addin`, and the source code, in C# or whatever you like.

The compiler generates the output DLL assembly in the directory you specify, by default `bin/Debug` and `bin/Release`.

Your post-built events copy the add-in manifest and the DLL to the appropriate Revit add-ins folder.

The `PDB` file contains the program debug information and is not required except for debugging.

The mother of all information on installing a Revit add-in is provided by
the [Revit online help](http://help.autodesk.com/view/RVT/2017/ENU) &gt; Developers
&gt; [Revit API Developers Guide](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42)
&gt; [Introduction](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-C574D4C8-B6D2-4E45-93A5-7E35B7E289BE)
&gt; [Add-In Integration](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-4BE74935-A15C-4536-BD9C-7778766CE392)
&gt; [Add-in Registration](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-4FFDB03E-6936-417C-9772-8FC258A261F7).

