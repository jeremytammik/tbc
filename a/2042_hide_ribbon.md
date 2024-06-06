<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>
</head>

<!---

- remove ribbon panel and ribbon button
  https://chuongmep.com/posts/2024-04-19-reload-ribbon-revit.html#remove-panel
  find ribbon tabs and or panels and delete
  https://forums.autodesk.com/t5/revit-api-forum/find-ribbon-tabs-and-or-panels-and-delete/m-p/12793159#M79071

twitter:

 in the @AutodeskRevit #RevitAPI #BIM @DynamoBIM https://autode.sk/revit_2025_1

...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Removing Docs Zip Files, Panels and Buttons

####<a name="2"></a> No Zips Downloading RCM from Docs?

This  is a reposting of
the [Call for feedback: No more ZIP files when downloading Revit Cloud Models from Docs](https://aps.autodesk.com/blog/call-feedback-no-more-zip-files-when-downloading-revit-cloud-models-docs):

<center>
<img src="img/rcm_no_zip.png" alt="No Zips Downloading RCM from Docs?" title="No Zips Downloading RCM from Docs?" width="600"/>
</center>


Does your application download Revit models from Autodesk/BIM 360 Docs, aka Revit Cloud Model or RCM?
If so, you are probably aware that the model is sometimes downloaded as ZIP.
This happens when a host model is linked to unpublished models.
It often causes confusion to both customers and application developers.

The Revit team is planning to change that and eliminate the ZIP.
Then, when downloading a host model, you will get only the host model without any linked models.

For developers, the process of downloading a source file itself does not change.
However, this will be a behaviour change.
This change might affect your application, depending on what it does.
Therefore, we would like to understand your use cases to avoid any possible disruption.

If you think this change might affect your application, are willing to share your use case scenarios and to give us feedback, please contact us through:

<center>
 [email to rcm.download.api.feedback@autodesk.com with the subject ”“RCM download API feedback"](mailto:rcm.download.api.feedback@autodesk.com?subject=“RCM download API feedback")
</center>

For more information about current behavior of downloading a Revit model from Autodesk/BIM 360 Docs, please refer to the article
on [Why is a Revit model sometimes downloaded as ZIP from BIM 360 or ACC?](https://www.autodesk.com/support/technical/article/caas/sfdcarticles/sfdcarticles/Why-a-RVT-model-is-sometimes-downloaded-as-ZIP-from-BIM-360.html)

Here are blog posts on how to handle RCM zip or composite models in APS that list potential use cases which might affect your application:

- [BIM360 Docs: Setting up external references between files (Upload Linked Files)](https://aps.autodesk.com/blog/bim360-docs-setting-external-references-between-files-upload-linked-files)
- [Make composite Revit design work with Design Automation API for Revit](https://aps.autodesk.com/blog/make-composite-revit-design-work-design-automation-api-revit)
- [Revit Cloud Worksharing - fast extraction of Revit zip files with partials](https://aps.autodesk.com/blog/revit-cloud-worksharing-fast-extraction-revit-zip-files-partials)

We looking forward to hearing from you.
Thank you!

####<a name="3"></a> Removing Ribbon Panel and Button

[Chuong Ho](https://chuongmep.com/) provided a solution to
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on how to [find ribbon tabs and or panels and delete](https://forums.autodesk.com/t5/revit-api-forum/find-ribbon-tabs-and-or-panels-and-delete/m-p/12793159) in
his comprehensive article on
[how to remove panel ribbon without restart Revit](https://chuongmep.com/posts/2024-04-19-reload-ribbon-revit.html#remove-panel):

**Question:**
Is there an option in Revit 2025 to dynamically delete "PushButtonData" from the "RibbonPanel", or maybe hide it so that a new button can link to a new DLL?
Also, is it possible to create a "PushButtonData" from a DLL located in the resources of another DLL?
Is it generally required that at the time of creation or registration in the panel (before it is clicked and called), the DLL meets all the conditions (class name, location, etc.), or can it already be solved at the time of the call?

**Answer:**
This article explains, and points out how to resolve an issue with Private Dictionary to store RibbonItemDictionary;
you need do some tricks to remove panel:

- [How to remove panel ribbon without restart Revit](https://chuongmep.com/posts/2024-04-19-reload-ribbon-revit.html#remove-panel)

**Response:**
Incredible job! Thank you very much! Added to bookmarks.

Can we get the name of the button, its description, or some kind of indicator after clicking it?

For example, I created one class MyCommand : IExternalCommand and registered it for several "PushButtonData" ("MyButtonOne", "MyButtonTwo").
After clicking on the button both times through the debugger, I will get to the same Execute() method of the MyCommand class.
In this case, is it possible to determine which of the buttons called this method? Any way, even the most perverted...

**Answer:**
You can do it with some step like this:

- Add assembly reference `AdWindows.dll`
- Add a event tracking user click on the button at IExternalApplication when user click to any button:

<pre><code class="language-cs">using AW = Autodesk.Windows;

Autodesk.Windows.ComponentManager.UIElementActivated
  += RibbonUtils.ComponentManagerOnUIElementActivated;

public static void ComponentManagerOnUIElementActivated(
  object sender,
  AW.UIElementActivatedEventArgs e)
{
  try
  {
    var id = e.Item.Id;
    // match with id string contents here and set to some where,
    // after thatm match with all command exist in your plugin
  }
</code></pre>

- Call the action from external command match with id return from the event clicked

**Response:**
Thank you so much for the prompt response!
I think this is exactly what I need!

Many thanks to Chuong Ho for the comprehensive solution.

