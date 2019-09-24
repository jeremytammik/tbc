<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- Modeless dialog - stays on top
  https://forums.autodesk.com/t5/revit-api-forum/modeless-dialog-stays-on-top/m-p/9042359

- buttons
  https://forums.autodesk.com/t5/revit-api-forum/how-to-modify-revit-button-picture/m-p/9034300#M41389
  https://thebuildingcoder.typepad.com/blog/2017/12/pipe-fitting-k-factor-archilab-and-installer.html#7
  https://archi-lab.net/create-your-own-tab-and-buttons-in-revit/
  https://thebuildingcoder.typepad.com/blog/2019/07/bim365-getting-started-visual-appearance-and-cpu-voltage.html#2
  https://www.bim365.tech/blog/programming-buttons-in-revit
  
- https://forums.autodesk.com/t5/revit-api-forum/external-application-with-web-ui/m-p/9036614

- Re: ExternalEvent
  https://forums.autodesk.com/t5/revit-api-forum/externalevent/m-p/9029731
  [Q] Hi! I want to create ExternalEvent on button click. But I get exception: "Attempting to create an ExternalEvent outside of a standard API execution". When I checked the thread of the main window and the Command class, they were the same thread. What's the magic? I created the class for reseting event in Command class.
  [A] Look at the Revit SDK sample ModelessDialog/ModelessForm_ExternalEvent.
  The Building Coder also shares lots of samples implementing external events:
  https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28
  Sean Page adds a helpful pointer, saying:
  I found this site to be exceptionally helpful when creating external event handlers.
  https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/Revit-API/files/GUID-0A0D656E-5C44-49E8-A891-6C29F88E35C0-htm.html
  https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/Revit-API/files/GUID-0A0D656E-5C44-49E8-A891-6C29F88E35C0-htm.html
  Very strangely, I can no longer find the Revit 2016 knowledgebase article Sean points out in the Revit 2020 knowledgebase.
  Unless someone can tell me where an up-to-date link for the current product version can be found, it might make sense to copy this to the blog or somewhere to preserve it for posterity.
  BobbyC.Jones adds:
  You cannot call ExternalEvent.Create() outside of a valid Revit API context, and a callback from a button click is most definitely not a valid context.
  What i do is create an instance of the IExternalEventHandler and call ExternalEvent.Create() from the IExternalCommand, or other valid context, and pass them to the viewmodel (or create them in the ViewModel contstructor if you're newing up the ViewModel in the IExternalCommand.  I prefer MVVM, if you do not then pass them to or create them where you're UI logic resides, a Controller, or directly in the Form, or wherever.  Then in your button click callback you pass necessary state info to your IExternalEventHandler and then call Raise().

- System.Data.Sqlite cannot be loaded
  https://forums.autodesk.com/t5/revit-api-forum/syste-data-sqlite-can-not-loaded/m-p/9039972
  [Q] I am writing an app that use sqlite to write to db and I make a refrence to System.Data.Sqlite but when I run the app inside Revit I got the error message "Could not load file or assembly or one of its dependencies"
  What can be the cause of that error? Thank you
  [A by Revitalizer] Autodesk itself uses a bunch of .NET DLL files in its own add-ins that are shipped with Revit.
  Since they are loaded before your add-in, there may be conflicts if different versions are used.
  So, make sure you use the same dll versions as Autodesk uses.
  This applies to all the DLLs you see in the Revit.exe directory, e.g., Sqlite, Log4Net, ...

- Get Sun Direction Adjusted for Project True North `GetSunDirection` by Mohsen Assaqqaf
  https://thebuildingcoder.typepad.com/blog/2013/06/sun-direction-shadow-calculation-and-wizard-update.html#comment-4614771756
  https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2020.0.147.8

- Reading RVT file
  https://stackoverflow.com/questions/57936246/reading-rvt-file

- [100 Top Free Online Courses Of All Time]
  https://www.freecodecamp.org/news/best-online-courses

twitter:

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### UI Buttons and Forms


####<a name="2"></a> 

####<a name="4"></a> Sun Direction Adjusted for Project True North

Mohsen Assaqqaf shared a method `GetSunDirection` for obtaining the vector of the sun taking the project True North angle into account
in [his comment](https://thebuildingcoder.typepad.com/blog/2013/06/sun-direction-shadow-calculation-and-wizard-update.html#comment-4614771756) on
the [Sun Direction and Shadow Calculation](https://thebuildingcoder.typepad.com/blog/2013/06/sun-direction-shadow-calculation-and-wizard-update.html#comment-4614771756) discussion.

I added it as follows
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
in [release 2020.0.147.8](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2020.0.147.8), cf.
the [diff to the previous version](https://github.com/jeremytammik/the_building_coder_samples/compare/2020.0.147.7...2020.0.147.8):

<pre style="font-family:Consolas;font-size:13px;color:black;background:white;">{
&nbsp;&nbsp;<span style="color:gray;">#region</span>&nbsp;Get&nbsp;Sun&nbsp;Direction&nbsp;Adjusted&nbsp;for&nbsp;Project&nbsp;True&nbsp;North
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Shared&nbsp;by&nbsp;Mohsen&nbsp;Assaqqaf&nbsp;in&nbsp;a&nbsp;comment&nbsp;on&nbsp;The&nbsp;Building&nbsp;Coder:</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;https://thebuildingcoder.typepad.com/blog/2013/06/sun-direction-shadow-calculation-and-wizard-update.html#comment-4614771756</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;I&nbsp;found&nbsp;that&nbsp;this&nbsp;method&nbsp;for&nbsp;getting&nbsp;the&nbsp;vector&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;of&nbsp;the&nbsp;sun&nbsp;does&nbsp;not&nbsp;take&nbsp;into&nbsp;account&nbsp;the&nbsp;True&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;North&nbsp;angle&nbsp;of&nbsp;the&nbsp;project,&nbsp;so&nbsp;I&nbsp;updated&nbsp;it&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;myself&nbsp;using&nbsp;the&nbsp;following&nbsp;code:</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Get&nbsp;sun&nbsp;direction&nbsp;adjusted&nbsp;for&nbsp;project&nbsp;true&nbsp;north</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;GetSunDirection(&nbsp;<span style="color:#2b91af;">View</span>&nbsp;view&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;view.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;sun&nbsp;and&nbsp;shadow&nbsp;settings&nbsp;from&nbsp;the&nbsp;3D&nbsp;View</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">SunAndShadowSettings</span>&nbsp;sunSettings
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;view.SunAndShadowSettings;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;the&nbsp;initial&nbsp;direction&nbsp;of&nbsp;the&nbsp;sun&nbsp;at&nbsp;ground&nbsp;level&nbsp;(like&nbsp;sunrise&nbsp;level)</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;initialDirection&nbsp;=&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;the&nbsp;altitude&nbsp;of&nbsp;the&nbsp;sun&nbsp;from&nbsp;the&nbsp;sun&nbsp;settings</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;altitude&nbsp;=&nbsp;sunSettings.GetFrameAltitude(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sunSettings.ActiveFrame&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;transform&nbsp;along&nbsp;the&nbsp;X&nbsp;axis&nbsp;based&nbsp;on&nbsp;the&nbsp;altitude&nbsp;of&nbsp;the&nbsp;sun</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;altitudeRotation&nbsp;=&nbsp;<span style="color:#2b91af;">Transform</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.CreateRotation(&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisX,&nbsp;altitude&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;rotation&nbsp;vector&nbsp;for&nbsp;the&nbsp;direction&nbsp;of&nbsp;the&nbsp;altitude&nbsp;of&nbsp;the&nbsp;sun</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;altitudeDirection&nbsp;=&nbsp;altitudeRotation
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfVector(&nbsp;initialDirection&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;the&nbsp;azimuth&nbsp;from&nbsp;the&nbsp;sun&nbsp;settings&nbsp;of&nbsp;the&nbsp;scene</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;azimuth&nbsp;=&nbsp;sunSettings.GetFrameAzimuth(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sunSettings.ActiveFrame&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Correct&nbsp;the&nbsp;value&nbsp;of&nbsp;the&nbsp;actual&nbsp;azimuth&nbsp;with&nbsp;true&nbsp;north</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;the&nbsp;true&nbsp;north&nbsp;angle&nbsp;of&nbsp;the&nbsp;project</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;projectInfoElement
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_ProjectBasePoint&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FirstElement();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>&nbsp;bipAtn
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.BASEPOINT_ANGLETON_PARAM;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;patn&nbsp;=&nbsp;projectInfoElement.get_Parameter(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bipAtn&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;trueNorthAngle&nbsp;=&nbsp;patn.AsDouble();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;the&nbsp;true&nbsp;north&nbsp;angle&nbsp;to&nbsp;the&nbsp;azimuth</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;actualAzimuth&nbsp;=&nbsp;2&nbsp;*&nbsp;<span style="color:#2b91af;">Math</span>.PI&nbsp;-&nbsp;azimuth&nbsp;+&nbsp;trueNorthAngle;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;rotation&nbsp;vector&nbsp;around&nbsp;the&nbsp;Z&nbsp;axis</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Transform</span>&nbsp;azimuthRotation&nbsp;=&nbsp;<span style="color:#2b91af;">Transform</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.CreateRotation(&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisZ,&nbsp;actualAzimuth&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Finally,&nbsp;calculate&nbsp;the&nbsp;direction&nbsp;of&nbsp;the&nbsp;sun</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;sunDirection&nbsp;=&nbsp;azimuthRotation.OfVector(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;altitudeDirection&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;sunDirection;
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:gray;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;Sun&nbsp;Direction&nbsp;Adjusted&nbsp;for&nbsp;Project&nbsp;True&nbsp;North</span>
</pre>

Many thanks to Mohsen for sharing this!

####<a name="5"></a> Reading an RVT File without Revit

This topic reappears regularly, and now surfaced again the the StackOverflow question 
on [reading RVT file](https://stackoverflow.com/questions/57936246/reading-rvt-file):

**Question:** When I open any .RVT file in notepad, I can search and find 'R E V I T', I tried everything to find the same text in C#, but no success to encode the .RVT file, any solution?

**Answer:** You may have more luck reading the text strings in Unicode encoding.

However, that will not help you decrypt very much.

To achieve more, you can read the RVT file as a structured OLE storage container.

Several different approaches are discussed in the following articles:

- [Open Revit OLE Storage](https://thebuildingcoder.typepad.com/blog/2010/06/open-revit-ole-storage.html)
- [Basic File Info and RVT File Version](http://thebuildingcoder.typepad.com/blog/2013/01/basic-file-info-and-rvt-file-version.html)
- [Custom File Properties](https://thebuildingcoder.typepad.com/blog/2015/09/lunar-eclipse-and-custom-file-properties.html#3)
- [Reading an RVT File without Revit](http://thebuildingcoder.typepad.com/blog/2016/02/reading-an-rvt-file-without-revit.html)
- [Determining RVT File Version Using Python](http://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html)
- [Retrieve RVT Preview Thumbnail Image with Python](https://thebuildingcoder.typepad.com/blog/2019/06/accessing-bim360-cloud-links-thumbnail-and-dynamo.html#3)

####<a name="6"></a> The Top 100 Free Online Courses

You should never stop learning.

A good place to browse for fresh material and inspiration is the overview of 
the [100 Top Free Online Courses Of All Time](https://www.freecodecamp.org/news/best-online-courses):

> Every year, Class Central releases a list of the Top Free Online Courses Of All Time, based on tens of thousands of user reviews.
This year, thanks to a growing number of reviews, the list is expanded from 50 to 100 courses.

The list covers various domains and classifies the courses into the following groups:

- Technology (23 courses)
- Business (16 courses)
- Humanities (16)
- Sciences (14 courses)
- Personal Development + Self Improvement (11)
- Health & Medicine (10 courses)
- Engineering (5)
- Language Learning (5)

<center>
<img src="img/.png" alt="" width="100">
</center>
