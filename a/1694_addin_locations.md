<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://forge.autodesk.com/blog/bim-360-docs-api-changes-access-data-european-data-center

- Revit Addin file locations for production Release and Test/Debug
  https://forums.autodesk.com/t5/revit-api-forum/revit-addin-file-locations-for-production-release-and-test-debug/m-p/8325021

- https://forums.autodesk.com/t5/revit-api-forum/getting-the-ids-of-selected-elements/m-p/8314144

BIM360 EU and #RevitAPI add-in locations @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/addinlocations

I am just getting ready to leave for Darmstadt, for the Forge DevCon and German Autodesk University.
Here are some quick recent topics I want to share before jumping on the train
&ndash; BIM 360 Docs API change for European data centre access
&ndash; Revit add-in locations
&ndash; Autodesk AppStore bundle format
&ndash; Juli Zeh über das <i>Turbo-Ich</i>
&ndash; YouTube video subtitles and auto-translation...

-->

### Revit Add-In Locations and BIM360 EU

I am just getting ready to leave for Darmstadt, for the Forge DevCon and German Autodesk University.

Here are some quick recent topics I want to share before jumping on the train:

- [BIM 360 Docs API change for European data centre access](#2) 
- [Revit add-in locations](#3) 
- [Autodesk AppStore bundle format](#4) 
- [Juli Zeh über das *Turbo-Ich*](#5)
- [YouTube video subtitles and auto-translation](#6)


#### <a name="2"></a> BIM 360 Docs API Change for European Data Centre Access

BIM 360 Docs recently started using a European data centre.

Note that the BIM 360 API is already using different US and EU endpoints for some resources, such as the Account Administration feature.

Unfortunately, a breaking change occurred in the BIM360 Docs API that you need to be aware of if you are accessing the European data centre; you will need to modify the code with new endpoints.

The Data Management API is not affected.

For more details, please refer to
the [BIM 360 Account Admin API HTTP specification](https://forge.autodesk.com/en/docs/bim360/v1/reference/http/) and
Mikako Harada and Augusto Goncalves' article
on [BIM 360 Docs: API Changes to Access Data in European Data Center](https://forge.autodesk.com/blog/bim-360-docs-api-changes-access-data-european-data-center).

#### <a name="3"></a> Revit Add-In Locations

Several questions on Revit add-in locations were raised in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160),
most recently in the thread
on [Revit add-in file locations for production release and test/debug](https://forums.autodesk.com/t5/revit-api-forum/revit-addin-file-locations-for-production-release-and-test-debug/m-p/8325021):

**Question:** Where are the  appropriate file installation locations for Release and Test/Debug Revit add-in components?

I have noticed a variety of locations where Revit add-in components are deployed for Release and Test/Debug purposes.

For example, the Hello World sample for Revit 2017 recommends installing `HelloWorld.addin` to *C:\ProgramData\Autodesk\Revit\Addins\2017*, whereas the Addin Wizard generates a script that automatically loads the `*.addin` file to *C:\UserName\AppData\roaming\AppData\Roaming\Autodesk\Revit\Addins\2017*.

I have also noticed that the official release add-in installation programs that I have historically gotten back from the Autodesk Revit team seem to install the `*.addin`, `*.dll`, and other supporting files in numerous locations depending on:

- Revit version(s) supported
- All users vs. current user installation
- Roaming profiles vs. non-roaming profiles
- Version of Windows (?)
- Possibly other criteria

First, is there documentation that describes how this should be done for commercial products?

Second, in the specific example of installing an add-in for Revit 2017 running on Windows 10 for the current user only without roaming, where would I install the production components (`myaddin.addin`, `myaddin.dll`, `myaddindependent.dll`)?

Can I utilize the location for the `.addin` file for Test and Debug?

I assume that I will need to point the `myaddin.addin` file to the Debug version of the `myaddin.dll` file in order to Debug?

**Answer:** The official documentation for this can be found in the Revit API developer guide; go to:

- [Revit online help](http://help.autodesk.com/view/RVT/2017/ENU)
- Developers
- [Revit API Developers Guide](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42)
- [Introduction](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-C574D4C8-B6D2-4E45-93A5-7E35B7E289BE)
- [Add-In Integration](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-4BE74935-A15C-4536-BD9C-7778766CE392)
- [Add-in Registration](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-4FFDB03E-6936-417C-9772-8FC258A261F7)

The Building Coder also discusses
some [additional undocumented add-in folder paths](http://thebuildingcoder.typepad.com/blog/2016/02/bim-360-docs-add-in-folders-stallman-and-the-abc-conjecture.html#5).

#### <a name="4"></a> Autodesk AppStore Bundle Format

More information on using the Autodesk AppStore bundle format was shared by Nachikethan S:

I can understand your concern of noticing a variety of locations where Revit Add-in components are deployed.

There is a reason for this. Let me explain:

- *%AppData%\Autodesk\Revit\Addins\<year>* &ndash; This location is for per-user only, and it is recommended for per-user specific applications.
- *%ProgramData%\Autodesk\Revit\Addins\<year>* &ndash; This location is for multiple users, so all the users can have access to the application.

A few apps are placed in Program Files, too, depending on need.

In the Autodesk App Store, we normally recommend using the following paths:

- *%ProgramData%\Autodesk\ApplicationPlugins* (for multiple users) 
- *%AppData%\Autodesk\ApplicationPlugins* (for per user) 

In order to use these paths, you need to use the bundle format, wherein the add-in and its dependent files are stored in a bundle folder.

Revit autoloads the add-in module based on the entries in the `PackageContents.xml` file placed in the bundle folder.

For more details, please refer to
the [slide deck PPT with guidelines on preparing Revit apps for the store](zip/3_Autodesk_Exchange_Publish_Revit_Apps_Preparing_Apps_for_the_Store_Guidelines.pptx) from slide nr. 6 onward, describing the bundle format.

<center>
<img src="img/revit_appstore_overview.png" alt="AppStore publishing overview" width="700">
</center>

Here is the [same slide deck in PDF format](zip/3_Autodesk_Exchange_Publish_Revit_Apps_Preparing_Apps_for_the_Store_Guidelines.pdf) in
order to enhance its Internet discoverability and readability.

You can utilize these locations for Testing and Debugging, and, yes, you can point to the `myaddin*.addin` file to the Debug version of the `myaddin.dll` file in order to Debug.

If you need any help on the bundle format, please feel free
to [email us @appsubmissions@autodesk.com](mailto:appsubmissions@autodesk.com).


#### <a name="5"></a> Juli Zeh Über das Turbo-Ich

I end with a non-technical note that is basically of interest to all humans living in modern society and pondering today's state of politics, social media und everyday behaviour.

However, it is in German language, which presumably limits the audience somewhat, at least until the automatic translation and subtitling of YouTube videos is perfected.

This is a pointer to a brilliant cultural, social, political, critical analysis by my favourite German author and thinker Juli Zeh.

I am touched and impressed by her deep understanding and clear explanation of the interaction of modern society, social media, culture, politics and everyday life.

Ich bin beeindruckt und berührt von Juli Zehs tiefblickende Analyse von dem Wechselspiel von Social Media und aktuelle gesellschaftliche und politische Zustände.

Juli Zeh spricht über die Macht von Stimmungen und die Zukunft der Demokratie in dem Vortrag [Das Turbo-Ich - Der Mensch im Kommunikationszeitalter](https://youtu.be/-5djf2rZMD4?t=870), zu Gast bei der Tübinger Mediendozentur CampusTV Tübingen, 12 Juli 2018:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/-5djf2rZMD4?start=872" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center>

#### <a name="6"></a> YouTube Video Subtitles and Auto-Translation

Wow, here is yet another note after all, of technical nature this time.

I just tested turning on auto-generated subtitles for this video and requesting them to be auto-translated to English, with astonishingly good results.

Go to YouTube and enable these two options in the right-hand bottom corner.

Nope, looking a bit further, there is still considerable room for improvement.

This talk is still too complex for the functionality available today...

It is pretty impressive anyway, though.
