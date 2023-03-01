<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js"></script>
</head>

<!---

- RevitLookup Ideas #146
  https://github.com/jeremytammik/RevitLookup/discussions/146

- transient elements, jig, graphics:
  draw line visible on screen
  https://forums.autodesk.com/t5/revit-api-forum/draw-line-visible-on-screen/m-p/11778165#M69522

- Open files located in ACC Docs
  https://stackoverflow.com/questions/75530623/open-files-located-in-the-accdocs
  https://forums.autodesk.com/t5/revit-api-forum/opening-a-cloud-model-with-revit-api/m-p/11767222

- RVT to IFC export with DA4 Revit
  https://autodesk.slack.com/archives/C03FXKR0H6J/p1676932151859789

- stop using jpeg
  https://daniel.do/article/its-the-future-stop-using-jpegs/
  It’s the future — you can stop using JPEGs
  An overview of some compelling alternatives.

twitter:

 @DynamoBIM  with the @AutodeskRevit #RevitAPI #BIM @AutodeskAPS https://autode.sk/simplifycurveloop

&ndash;
...

linkedin:

#BIM #DynamoBim #AutodeskAPS #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Transient


####<a name="2"></a> RevitLookup Ideas #146

RevitLookup Ideas #146
https://github.com/jeremytammik/RevitLookup/discussions/146

####<a name="3"></a> transient elements, jig, graphics:

A couple of ideas on creating transient elements graphics similar to the AutoCAD jig functionality using
the `IDirectContext3DServer` functionality or the temporary InCanvas graphics API were recapitulated in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [drawing line visible on screen](https://forums.autodesk.com/t5/revit-api-forum/draw-line-visible-on-screen/m-p/11778165):

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/10/onbox-directcontext-jig-and-no-cdn.html" target="_blank" rel="noopener">Onbox, DirectContext Jig and No CDN</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/01/transient-graphics-humane-ai-basic-income-and-lockdown.html" target="_blank" rel="noopener">Transient Graphics, Humane AI, BI and Lockdown</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/05/flip-mirror-transform-and-transient-graphics.html" target="_blank" rel="noopener">Flip, Mirror, Transform and Transient Graphics</a></li>
</ul>

Lorenzo Virone shared a [different approach](https://forums.autodesk.com/t5/revit-api-forum/draw-line-visible-on-screen/m-p/11778165#M69522), creating and deleting database-resident Revit elements on the fly in a loop:

I faced a similar UI problem to create a rubber band between two points.
I used these two functions, `Line.CreateBound` and `NewDetailCurve`, inside a loop to create a line at the cursor position, refresh, and delete the line every 0.1 seconds, until the user chooses the second point.
A little tricky, but it worked fine for me and Revit seems to execute these 2 functions very fast.

I didn't use it to create a line, but this trick will work with anything:
create new elements on each mouse movement, refresh, delete the created elements and replace them with new ones.
It can technically work with anything, model or detail elements, and it's easy to implement, because you just need to call the two methods, like this:

<pre class="prettyprint lang-cs">
  bool done = false;
  List<ElementId> temp = new List<ElementId>();

  while(!done)
  {
    doc.Delete(temp);

    // Create temp elements
    // Save their IDs in `temp`
    // Set `done` to `true` when finished

    doc.regenerate();
    uidoc.RefreshActiveView();
    Thread.Sleep(500); // milliseconds
  }

  // Your final elements are in `temp`
</pre>

Many thanks to Lorenzo for sharing this nice solution.

####<a name="4"></a> Open files located in ACC Docs

We started out
discussing [opening a cloud model with Revit API](https://forums.autodesk.com/t5/revit-api-forum/opening-a-cloud-model-with-revit-api/m-p/11767222) in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160),
but then moved over to a better place for such a topic, on StackOverflow,
on how to [open files located in ACC Docs](https://stackoverflow.com/questions/75530623/open-files-located-in-the-accdocs):

**Question:** My Visual Studio Revit API add-in open Revit files to export data in a batch.
I can add many files which are on the networks and the plugin automatically opens them all.
Is it possible to also open files that are in the ACC Docs cloud?

I know I can open AccDocs which were be already downloaded locally by searching for them in the collaboration cache folder, but how to open files which have not yet been downloaded?

**Answer:** Since you mention the `collaboration cache` folder, I suppose your Revit model is the [Revit Cloud Worksharing model](https://knowledge.autodesk.com/support/bim-360/learn-explore/caas/CloudHelp/cloudhelp/ENU/About-BIM360/files/about-bim-360-design/About-BIM360-about-bim-360-design-about-revit-cloud-worksharing-html-html.html),
a.k.a `C4R` model, the model for Autodesk Collaboration for Revit.

If so, we can call APS Data Management to obtain the `projectGuid` and `modelGuid` in the model's version tip like this:

<pre class="prettyprint lang-json">
{
   "type":"versions",
   "id":"urn:adsk.wipprod:fs.file:vf.abcd1234?version=1",
   "attributes":{
      "name":"fileName.rvt",
      "displayName":"fileName.rvt",
      ...
      "mimeType":"application/vnd.autodesk.r360",
      "storageSize":123456,
      "fileType":"rvt",
      "extension":{
         "type":"versions:autodesk.bim360:C4RModel",
         ....
         "data":{
            ...
            "projectGuid":"48da72af-3aa6-4b76-866b-c11bb3d53883",
            ....
            "modelGuid":"e666fa30-9808-42f4-a05b-8cb8da576fe9",
            ....
         }
      }
   },
   ....
}
</pre>

Afterward, open the C4R model using Revit API like below:

<pre class="prettyprint lang-cs">
var region = ModelPathUtils.CloudRegionUS; //!<<< depends on where your BIM360/ACC account is based, US or EU.
var projectGuid = new Guid("48da72af-3aa6-4b76-866b-c11bb3d53883");
var modelGuid = new Guid("e666fa30-9808-42f4-a05b-8cb8da576fe9");

var modelPath = ModelPathUtils.ConvertCloudGUIDsToCloudPath( region, projectGuid, modelGuid ); //!<<< For Revit 2023 and newer.
//var modelPath = ModelPathUtils.ConvertCloudGUIDsToCloudPath( projectGuid, modelGuid ); //!<<< For Revit 2019 ~ 2022

var openOptions = new OpenOptions();
app.OpenAndActivateDocument( modelPath, openOptions ); //!<<< on desktop
// app.OpenDocumentFile( modelPath, openOptions ); //!<<< on Design Automation for Revit or don't want to activate the model on Revit desktop.
</pre>

References:
- https://aps.autodesk.com/blog/accessing-bim-360-design-models-revit
- https://thebuildingcoder.typepad.com/blog/2020/04/revit-2021-cloud-model-api.html#4.4
- https://help.autodesk.com/view/RVT/2023/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Application_and_Document_CloudFiles_html


####<a name="5"></a> RVT to IFC export with DA4 Revit

RVT to IFC export with DA4 Revit
https://autodesk.slack.com/archives/C03FXKR0H6J/p1676932151859789

Hi team, Question about RVT to IFC export with DA4 Revit…
For customising IFC exports, we'd like to set which 3D view is used for export and customise all the settings available in the open-source revit-ifc add-on, including checkboxes like exporting things only visible in view, and using external user defined property set text configuration files. Is this possible with the Forge IFC exporter? It doesn't seem so, based on https://aps.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/job-POST/ which shows there is only one setting I can configure.
If that's not possible with the Forge IFC exporter, can you explain how to do this with DA4Revit? I assume it would be similar to this: https://github.com/simonmoreau/RevitToIFCBundle however what isn't clear to me is what version of revit-ifc does the DA4Revit plug-in ship with? Because often we find that the version Revit comes with is heavily outdated and has bugs. Also not clear is assuming it does ship with it, we'd need to build a plugin which supports 3 Revit versions worth of IfcExportOptions (https://forums.autodesk.com/t5/revit-api-forum/ifc-export-options/td-p/9686404) with all of their respective quirks which are not documented.
——
Anyone tried DA4revit ?
simonmoreau/RevitToIFCBundle
The Revit add-in to export the model as an IFC file
Stars
5
Language
C#
Added by GitHub
Autodesk Community
IFC Export Options
Hey,   i need to write an IFC exporter, but I can't find any Class in the API for all export settings. I mean, yes the is the
12 Aug 2020


Zhong Wu

I believe this could be achieved by Revit DA, and about the IFC Exporter version on Revit DA, I got this from
@Rahul Bhobe
at another thread https://autodesk.slack.com/archives/C011QHANM8R/p1675449885962709?thread_ts=1674647872.714689&cid=C011QHANM8R ,
@Eason Kang
may have more comments about on this..
The Revit build date that we are currently running on backend instance is 20221018_1515_2023.1.1 which also matches what Lijuan said. This has the IFC exporter 23.2.0.0. In the next deployment, we shall upgrade to latest Revit 2023.1.2 build which will upgrade the IFC exporter to 23.2.3.0  (which is what Jan has).


Rahul Bhobe
I’m not sure if it’s the case, but I encountered a similar issue with Revit Desktop before. if I recall correctly, I got that fixed by upgrading my Revit IFC addin.
The Revit build date that we are currently running on backend instance is 20221018_1515_2023.1.1 which also matches what Lijuan said. This has the IFC exporter 23.2.0.0. In the next deployment, we shall upgrade to latest Revit 2023.1.2 build which will upgrade the IFC exporter to 23.2.3.0  (which is what Jan has).
Next deployment is planned for in two weeks (ETA Feb 17).
Show more (edited)
From a thread in forge-design-automation | 3 Feb | View reply


Eason Kang

Both export only visible in view, and using external user defined property set text configuration files are not supported in Revit extractor.
The export only visible in view one is technical limitation since the view used in this option is Document.ActiveView, which is not supported in RCE (UI-less Revit). Here is the conclusion for the discussion between engineering team and myself last week:  https://autodesk.slack.com/archives/C0U4RCJ1M/p1675747499601929
The user property set is unsupported due to one technical difficulty here, but we have a wishlist item for supporting this. However, recent Revit IFC addin uses the new JSON-based extensible storage, which is unsupported in current Revit extractor. So, Revit extractor cannot parse custom IFC export settings using the new JSON scheme at this moment. This improvement also addressed in the wishlist item. Now we must wait for engineering team for this.
(edited)


Eason Kang

We got a Forge customer is asking what the IFC option only visible elements in view  means when exporting IFC from model derivative (Revit extractor). After some research, I found it means Revit IFC addin will export elements visible in Document.ActiveView when specifying this option. Does this apply to Revit extractor?
If yes, which Revit view will be Document.ActiveView when exporting IFC file from Revit extractor? Is it the last view that had focus on UI or the default 3D view {3D}?
Show more (edited)
Thread in bid-team-nexus | 7 Feb | View message


Boyuan Liu
We could only handle the schema which is saved within the Revit model. If it needs an external file it will not work. Because we only could download the Revit model file during extraction. That is a technique problem in the ticket. I will post this to my team to help PO prioritize. (edited)
From a thread in forge-modelderivative | 31 Jan | View reply


Eason Kang
Hi Team, FYI, I noticed that Revit extractor seems not to support the new IFC export JOSN schema and user defined property sets while helping a Forge customer.
With my research, the recent Revit IFC changed how it stores and reads custom IFC export settings in the Revit file, it’s JSON-based now.
So, I logged Jira wish to support both. Thank you!
Show more (edited)
Posted in bid-team-nexus | 10 Aug 2022 | View message


Eason Kang

For exporting IFC on Revit DA, here is my sample appbundle that can handle IFC export settings created by the recent Revit IFC addin (saying the JSON-based) and user-defined property set files.
https://github.com/yiskang/forge-revit-ifc-exporter-appbundle
https://github.com/yiskang/forge-revit-ifc-exporter-appbundle#example-of-userpropertysetsfile-for-ifc


Eason Kang

To make export only visible in view work, the customer can modify my DA sample code here (saying ActiveViewId) to his desired view id, since Document.ActiveView is not unavailable in UI-less Revit (e.g. Revit extractor and Revit DA)
https://github.com/yiskang/forge-revit-ifc-exporter-appbundle/blob/e9d76f03c64a2db624a15dbfff33f54461fd2b89/RevitIfcExportor/MainApp.cs#L142 (edited)
MainApp.cs
exportConfig.ActiveViewId = activeViewId.IntegerValue;
<https://github.com/yiskang/forge-revit-ifc-exporter-appbundle|yiskang/forge-revit-ifc-exporter-appbundle>yiskang/forge-revit-ifc-exporter-appbundle | Added by GitHub


####<a name="6"></a> Stop Using JPEG

[stop using JPEG](https://daniel.do/article/its-the-future-stop-using-jpegs)
It’s the future — you can stop using JPEGs
An overview of some compelling alternatives.

####<a name="7"></a> Stop Using Voice Id

Joseph Cox describes [How I broke into a bank account with an AI-generated voice](https://www.vice.com/en/article/dy7axa/how-i-broke-into-a-bank-account-with-an-ai-generated-voice)
&ndash; some banks tout voice ID as a secure way to log into your account.
I proved it's possible to trick such systems with free or cheap AI-generated voices.


Many thanks to
 for the interesting pointers!

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 349 x 231 pixels -->
</center>

**Question:**

**Answer:**

**Response:**

 
