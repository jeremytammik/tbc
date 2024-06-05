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

- hide elements in linked model
  https://forums.autodesk.com/t5/revit-ideas/hide-elements-in-linked-model/idc-p/12786934#M57768
  Hide elements in linked file
  https://forums.autodesk.com/t5/revit-api-forum/hide-elements-in-linked-file/td-p/5777305
  @lvirone
  Lorenzo VIRONE
  I've found a solution:
  //Select elements using UIDocument and then use PostCommand "HideElements"
  //elemsFromRevitLinkInstance is "List<Element>", there are the elements you want to hide in the link
  var refs = elemsFromRevitLinkInstance.Select(x => new Reference(x).CreateLinkReference(revitLinkInstance)).ToList();
  uidoc.Selection.SetReferences(refs);
  uidoc.Application.PostCommand(RevitCommandId.LookupPostableCommandId(PostableCommand.HideElements));
  demonstrate how to preselect elements for PostCommand processing

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

### Hiding Panel, Button and Linked Element

####<a name="2"></a>

<center>
<img src="img/rvt_2025_1.png" alt="Revit 2025.1" title="Revit 2025.1" width="800"/> <!-- Pixel Height: 585 Pixel Width: 1,000 -->
</center>

####<a name="2"></a> Removing Ribbon Panel and Button

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

####<a name="3"></a> Hiding Linked Elements

We habve an open Revit Ideas wish list item
to [hide elements in linked model](https://forums.autodesk.com/t5/revit-ideas/hide-elements-in-linked-model/idc-p/12786934).
Lorenzo Virone shared a solution to it using `PostCommand` and element pre-selection via `Selection.SetReferences`,
explaining the detailed approach in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread on how
to [hide elements in linked file](https://forums.autodesk.com/t5/revit-api-forum/hide-elements-in-linked-file/td-p/5777305):

I found a solution that also demonstrates how to preselect elements for `PostCommand` processing:

<pre><code class="language-cs">// Select elements using UIDocument
// then use PostCommand "HideElements"
// elemsFromRevitLinkInstance is "List&lt;Element&gt;"
// these are the elements you want to hide in the link

var refs = elemsFromRevitLinkInstance.Select( x
  => new Reference(x).CreateLinkReference(revitLinkInstance))
    .ToList();

uidoc.Selection.SetReferences(refs);

uidoc.Application.PostCommand(
  RevitCommandId.LookupPostableCommandId(
    PostableCommand.HideElements));
</code></pre>

**Response:** Can you provide a description of the process for using your code to hide elements only in a linked model?
I am not familiar with the API and deploying a script like this.

**Answer:**
Here is an sample that selects the first RevitLinkInstance, retrieve its floors and hides them:

<pre><code class="language-cs">// Get a link
var filter = new ElementClassFilter(typeof(RevitLinkInstance));

var firstInstanceLink
  = (RevitLinkInstance) new FilteredElementCollector(doc)
    .WherePasses(filter)
    .FirstElement();

// Get its floors
filter = new ElementClassFilter(typeof(Floor));
var elemsFromRevitLinkInstance
  = new FilteredElementCollector(
    firstInstanceLink.GetLinkDocument())
      .WherePasses(filter)
      .ToElements();

// Isolate them
var refs = elemsFromRevitLinkInstance.Select( x
  => new Reference(x).CreateLinkReference(firstInstanceLink))
    .ToList();

uidoc.Selection.SetReferences(refs);

uidoc.Application.PostCommand(
  RevitCommandId.LookupPostableCommandId(
    PostableCommand.HideElements));
</code></pre>



What's new in Autodesk Revit 2025 API
https://www.youtube.com/playlist?list=PLuFh5NgXkweMoOwwM2NlYmQ7FdMKPEBS_
1
6:58
Introduction and .NET 8 Migration
https://www.youtube.com/watch?v=ONLf4BuGBU8&list=PLuFh5NgXkweMoOwwM2NlYmQ7FdMKPEBS_&index=1&pp=iAQB
2
15:27
Breaking changes and removed API
https://www.youtube.com/watch?v=huj3ynWwejA&list=PLuFh5NgXkweMoOwwM2NlYmQ7FdMKPEBS_&index=2&pp=iAQB
3
40:04
New APIs and Capabilities
https://www.youtube.com/watch?v=jExac5Kv-Qs&list=PLuFh5NgXkweMoOwwM2NlYmQ7FdMKPEBS_&index=3&pp=iAQB

Call for feedback: No more ZIP files when downloading Revit Cloud Models from Docs
https://aps.autodesk.com/blog/call-feedback-no-more-zip-files-when-downloading-revit-cloud-models-docs

RCM Revit Cloud Model




Mikako Harada to Everyone (4 Jun 2024, 13:43)
https://wiki.autodesk.com/pages/viewpage.action?spaceKey=DTAL&title=DevTech+Processes

Caroline Gitonga to Everyone (4 Jun 2024, 13:48)
https://github.com/autodesk-platform-services/aps-webhook-notifier
