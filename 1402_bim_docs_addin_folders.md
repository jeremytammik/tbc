<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewAPI #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restAPI
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge
@AutodeskReCap @Adsk3dsMax
#revitAPI #bim #aec #3dwebcoder #adsk #adskdevnetwrk @jimquanci @keanw
#au2015 #rtceur
#eraofconnection

Revit API, Jeremy Tammik, akn_include

BIM 360 Docs, Add-In Folders, Stallman and Abc #revitAPI #3dwebcoder @AutodeskRevit @adskForge #3dwebaccel #a360 #bim #RMS @researchdigisus

I am going to the University of Bern this afternoon to listen to Richard Stallman speak For A Free Digital Society.
Here are some other recent and not-so-recent topics
&ndash; BIM 360 Docs
&ndash; Richard Stallman in Switzerland
&ndash; Is the <i>abc</i> conjecture proven?
&ndash; Add-In Folders
&ndash; BIM 360 Docs is the new Autodesk platform for construction document management.
It provides web services to ensure that the entire project team is always building from the correct version of documents, plans, and models...

-->

### BIM 360 Docs, Add-In Folders, Stallman and Abc

I am going to the University of Bern this afternoon to listen to Richard Stallman
speak [For A Free Digital Society](http://www.digitale-nachhaltigkeit.unibe.ch/veranstaltungen/richard_m_stallman/index_ger.html).

Here are some other recent and not-so-recent topics:

- [BIM 360 Docs](#2)
- [Richard Stallman in Switzerland](#3)
- [Is the *abc* conjecture proven?](#4)
- [Add-In Folders](#5)


#### <a name="2"></a>BIM 360 Docs

[BIM 360 Docs](http://www.autodesk.com/products/bim-360-docs/overview) is
the new Autodesk platform for construction document management.

The [BIM 360 Docs](http://www.autodesk.com/products/bim-360-docs/overview) web
service ensures that the entire project team is always building from the correct version of documents, plans, and models.

This is obviously absolutely fundamental to save time, reduce risk, and mitigate errors in construction projects.

It was previously available as preview technology and is now unleashed as a real product,
so [not a preview any more](http://www.engineering.com/BIM/ArticleID/11434/BIM-360-Docs-Not-a-Preview-Anymore.aspx).

You can [sign in](https://bim360docs.autodesk.com/session) for a test run right away.

Long-term, in my wildest fantasies, I can imagine BIM 360 Docs growing into something like a synthesis of Navisworks, the current BIM 360 platform, Vault, and more.

Pretty cool dream, isn't it?


#### <a name="3"></a>Richard Stallman in Switzerland

[Richard Stallman](https://de.wikipedia.org/wiki/Richard_Stallman) of
the [Free Software Foundation FSF](https://www.fsf.org) is visiting and giving talks in Switzerland:

- [Bern University](https://www.fsf.org/events/rms-20160205-bern)
&ndash; [For A Free Digital Society](http://www.digitale-nachhaltigkeit.unibe.ch/veranstaltungen/richard_m_stallman/index_ger.html)
- [Zurich ImpactHub](https://www.fsf.org/events/rms-20160208-zurich)
- [Sierre](https://www.fsf.org/events/rms-20160210-sierre)
- [More events with Stallman](https://www.fsf.org/events/rms-speeches.html)
- [More FSF events](https://www.fsf.org/events/all.html)

<center>
<img src="img/Richard_Stallman.jpg" alt="Richard Stallman" width="220">
</center>

I am going to the presentation in Bern this afternoon, and hoping to meet my colleague
[Kean Walmsley](http://through-the-interface.typepad.com/through_the_interface/about-the-author.html) there
too.


#### <a name="4"></a>Is the *abc* Conjecture Proven?

Talking about universities and academics, a local newspaper just mentioned that
the [abc Conjecture](https://en.wikipedia.org/wiki/Abc_conjecture) has now been proved
by [Shinichi Mochizuki](https://en.wikipedia.org/wiki/Shinichi_Mochizuki).

The [Wikipedia article](https://en.wikipedia.org/wiki/Abc_conjecture) does
indeed mention his efforts and how challenging it is to verify them.

Interesting stuff.
I love diving into pure maths just a little bit now and then.


#### <a name="5"></a>Add-In Folders

Back to Revit again. This rather dated question that a whole bunch of my colleagues from ADN chipped in to answer from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread on
the [auto-loader folders for application plugins](http://forums.autodesk.com/t5/revit-api/autoloader-folder-applicationplugins/td-p/5556540) has
been hanging around for a while in my to-do list:

**Question:** It appears that the folders used for AutoCAD add-ins is also valid for use with Revit.

I can therefore use this path:

> C:\ProgramData\Autodesk\ApplicationPlugins\MySuperApp.bundle

instead of the Revit specific add-in folder:

> C:\ProgramData\Autodesk\Revit\Addins\2014\MySuperApp.bundle

Is this correct?

I would much rather have a common installation folder for both AutoCAD and Revit applications managed via the XML manifest file.

**Answer:** Yes, the ApplicationPlugins folder should work for both AutoCAD and Revit (on the current versions).

Looking forward, we are thinking about moving the default to the `Program Files` folder for the AutoCAD environment.

The Revit API help file RevitAPI.chm does not have anything additional to say on this subject, as you can see under Developers &gt; Revit API Developers Guide &gt; Introduction &gt; Add-In Integration
&gt; [Add-in Registration](http://help.autodesk.com/view/RVT/2015/ENU/?guid=GUID-4FFDB03E-6936-417C-9772-8FC258A261F7).

My colleagues concur, saying:

Yes, that is the theory; Max, Maya and AutoCAD (at least) use

> C:\ProgramData\Autodesk\ApplicationPlugins

As long as the host app looks in that folder, it should work.

I just checked, and that location works for Inventor 2015 AddIns as well.

The Inventor API Help describes these four recommended locations:

- All Users, Version Independent
    - Windows 7 - %ALLUSERSPROFILE%\Autodesk\Inventor Addins\
    - Windows XP - %ALLUSERSPROFILE%\Application Data\Autodesk\Inventor Addins\
- All Users, Version Dependent
    - Windows 7 - %ALLUSERSPROFILE%\Autodesk\Inventor 2013\Addins\
    - Windows XP - %ALLUSERSPROFILE%\Application Data\Autodesk\Inventor 2013\Addins\
- Per User, Version Dependent
    - Both Window 7 and XP - %APPDATA%\Autodesk\Inventor 2013\Addins\
- Per User, Version Independent
    - Both Window 7 and XP - %APPDATA%\Autodesk\ApplicationPlugins

Here is some additional information on this from the AppStore perspective:

These two folder locations were added to support our Exchange Store apps:

- %AppData%\Autodesk\ApplicationPlugins
- %ProgramData%\Autodesk\ApplicationPlugins

You can find more information on the XML files from
the [App Store Developer Centre](http://www.autodesk.com/developapps),
including [specifics about publishing Revit apps](http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=20143032).

Further down, the page provides a table with recordings and PowerPoint slide decks.
You may want to take a look at the XML tags mentioned there.
This is the location to place exchange apps for all the different products.
Make sure that you provide all relevant information in the XML file and structure your apps as self-contained archive file bundles as described in the publisher page.
