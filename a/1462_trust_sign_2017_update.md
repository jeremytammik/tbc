<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

- migrated ADN Xtra labs to Revit 2017

- migrated ADN labs to Revit 2017

- revit 2017 api news
  devdays online recording
  summary rtf doc

- trusted signature
  11397533 [Code signing of Revit Addins]
  http://forums.autodesk.com/t5/revit-api/code-signing-of-revit-addins/m-p/5981560
  11782787 [Revit 2017 signed addin warning]
  11809640 [Code Signing of Revit 2017 Addins]

Trusted Signature and Revit 2017 API Resources #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

Here is a bunch of long overdue news items to round off this hot week
&ndash; Trusted add-in signature
&ndash; Revit 2017 API news summary
&ndash; ADN training labs for Revit 2017
&ndash; ADN Xtra labs Revit 2017 migration
&ndash; We had an extensive discussion on the topic of equipping each Revit add-in with a trusted signature in order to avoid the warning presented before loading it that otherwise pops up and needs to be manually confirmed by the user...

-->

### Trusted Signature and Revit 2017 API Resources

Here is a bunch of long overdue news items to round off this hot week:

- [Trusted add-in signature](#2)
- [Revit 2017 API news summary](#3)
- [ADN training labs for Revit 2017](#4)
- [ADN Xtra labs Revit 2017 migration](#5)


#### <a name="2"></a>Trusted Add-in Signature

We had an extensive discussion on the topic of equipping each Revit add-in with a trusted signature in order to avoid the warning presented before loading it that otherwise pops up and needs to be manually confirmed by the user.

It started off in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [code signing of Revit add-ins](http://forums.autodesk.com/t5/revit-api/code-signing-of-revit-addins/m-p/5981560) even
before the initial customer release.

I continued adding new material to it as various more specific queries came in from developers over time.

I hope it now includes a clear explanation of all aspects of the situation.

The latest addition is probably the most complete and succinct summary:

Question: "When Revit 2017 opens with my in-house developed add-in, it prompts the user for an action: always load, load once or do not load. Since the app is in-house developed, I wonder: is it always necessary to have a certificate also for in-house trusted area?"
 
Answer: The answer is yes; the warning will always be issued.

Our security architect adds:
 
The add-in must be signed with an approved cert or it will display the popup on first load.
 
We've documented the process
of [creating a self-signed cert that can be used for your own internal use](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-B9A067F4-234F-47F8-A5EE-0D84A93FA98E).
 
You
can <a href="https://technet.microsoft.com/en-us/library/dd807084(v=ws.11).aspx">push the cert to machines in your domain using group policy</a>.
 
If you both sign the add-in and push the cert to domain machines, users can use the add-in without the popup.


#### <a name="3"></a>Revit 2017 API News Summary

Next, here is the rather overdue publication of the Revit 2017 API news presentations from the DevDays conferences around the turn of the year:

- [Revit 2017 API News notes](/a/devdays/2015/Revit_API_News/Revit_2017_API_News_notes.pdf)
- [Revit 2017 API News slides](/a/devdays/2015/Revit_API_News/Revit_2017_API_News_slides.pdf)
- For ADN members
    - [DevDays Online 2015/2016](http://adn.autodesk.com/adn/servlet/item?siteID=4814862&id=25207102)
    - [Revit and Project Expo presentation recording](http://adn.autodesk.com/adn/servlet/file/developer_days_online_revit_and_project_expo.wmv?siteID=4814862&id=25214065) (wmv &ndash; 82440 Kb)
- For non-ADN-members
    - [Revit and Project Expo presentation recording](http://thebuildingcoder.typepad.com/revit_2017_api_news/developer_days_online_revit_and_project_expo.wmv) (wmv &ndash; 84418611 bytes)


#### <a name="4"></a>ADN Training Labs for Revit 2017

As usual, the training material used by the Autodesk Developer Network ADN for hands-on Revit API introductory trainings has been updated for Revit 2017 and is available from GitHub in
the [Revit API Labs Training Material repository](https://github.com/ADN-DevTech/RevitTrainingMaterial).

Many thanks to [Ryuji Ogasawara](http://adndevblog.typepad.com/technology_perspective/ryuji-ogasawara.html) for
the migration and clean-up for Revit 2017!


#### <a name="5"></a>ADN Xtra Labs Revit 2017 Migration

I also migrated the ADN Xtra Labs to Revit 2017.

It includes the same training material as the official ADN version mentioned above, plus a series of so-called Xtra labs containing the material we before restructuring and documenting the labs in a more didactical fashion.

The older labs have a different history and approach, provide some alternative insights, and include a couple of tools and utilities that I still find worthwhile to preserve and maintain.

The migration to Revit 2017 is very straightforward.

The first step is to update the .NET target framework from 4.5 to 4.5.2 and update the Revit API assembly references to point to the new Revit.exe folder.

Doing so produces zero errors for this add-in, just a couple of warnings.

Here are my migration steps with all the intermediate releases, differences between each step and the previous one and log files listing the warnings produced at that point:

1. [2017.0.0.0](https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2017.0.0.0)
&ndash; initial flat migration to Revit 2017
&ndash; [diffs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/compare/2016.0.0.10...2017.0.0.0)
&ndash; [25 warnings](zip/adn_xtra_2017_warnings_01.txt)
2. [2017.0.0.1](https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2017.0.0.1)
&ndash; cleaned up various paths pointing to 2016
&ndash; [diffs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/compare/2017.0.0.0...2017.0.0.1)
3. [2017.0.0.2](https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2017.0.0.2)
&ndash; fixed all warnings about deprecated API usage except Automatic transaction mode
&ndash; [diffs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/compare/2017.0.0.1...2017.0.0.2)
&ndash; [35 warnings](zip/adn_xtra_2017_warnings_02.txt)
4. [2017.0.0.3](https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2017.0.0.3)
&ndash; eliminated some of the deprecated Automatic transaction mode usages
&ndash; [diffs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/compare/2017.0.0.2...2017.0.0.3)
&ndash; [29 warnings](zip/adn_xtra_2017_warnings_03.txt)
5. [2017.0.0.4](https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2017.0.0.4)
&ndash; eliminated all deprecated Automatic transaction mode usage in all C# modules except Lab3.cs
&ndash; [diffs](https://github.com/jeremytammik/AdnRevitApiLabsXtra/compare/2017.0.0.3...2017.0.0.4)
&ndash; [19 warnings](zip/adn_xtra_2017_warnings_04.txt)

19 warnings about use of the obsolete and deprecated automatic transaction mode remain, so I am still not completely done.

In any case, these diffs and logs should give you a very clear idea of how to approach any migration issues that you may encounter.

<center>
<img src="img/little_house_3d_view.png" alt="Little house" width="380">
</center>
