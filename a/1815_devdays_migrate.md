<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- DevDays online -- https://adndevblog.typepad.com/autocad/2020/01/join-us-for-our-devdays-online-webinars.html

- Upgrading Revit API Apps For Newer Revit Versions - RevThat

twitter:

Add-in migration i.e. updating API references and invitation to join the upcoming yearly DevDays Online webinars for the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/devdaysmigration

I share a contribution from fellow blogger Eric Boehlke and the announcement of the upcoming yearly DevDays Online presentations
&ndash; Add-in migration &ndash; Update API references
&ndash; Join us for our DevDays Online webinars...

linkedin:

Add-in migration, i.e., updating API references and invitation to join the upcoming yearly DevDays Online webinars for the #RevitAPI

http://bit.ly/devdaysmigration

I share a contribution from fellow blogger Eric Boehlke and the announcement of the upcoming yearly DevDays Online presentations:

- Add-in migration
- Update API references
- Join us for our DevDays Online webinars...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### DevDays Online and Add-In Migration

I share a contribution from fellow blogger Eric Boehlke and the announcement of the upcoming yearly DevDays Online presentations:

- [Add-in migration &ndash; update API references](#2)
- [Addendum &ndash; NuGet package and .NET framework version](#2.1)
- [Join us for our DevDays Online webinars](#3)

#### <a name="2"></a>Add-In Migration &ndash; Update API references

Eric Boehlke of [truevis](https://truevis.com) BIM Consulting
wrote a blog post about how to upgrade a Revit API add-in to a version of Revit.

Says he:

> You may already have done this thousands of times.
One of the reasons to write these posts is to remember for myself how to do such things a year from now.
Hence:
[Upgrading Revit API Apps For Newer Revit Versions](http://revthat.com/upgrading-revit-api-apps-for-newer-revit-versions)

Here is a [75-second video](https://youtu.be/ypC_0REg22U) reiterating the same instructions to make a dry subject more fun:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/ypC_0REg22U" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

The cautionary message on not confusing `RevitAPIUI.dll` and `RevitUIAPI.dll` is due to personally running into that issue once.

Removing the existing Revit API references, adding the new ones, and setting `Copy Local` to `false` is the foolproof way to upgrade.

In many cases, though, you can do this all in one single step by just adding the new references.

This overwrites the old references and retains the `Copy Local` `false` setting.

However, that is an unimportant detail.

It's not actually much effort to create such a video from a blog post.
[lumen5.com](https://lumen5.com) makes an initial "AI" attempt at turning my blog posts into videos.
Then I just refine it. It takes 10 or 15 minutes.
They say: "We automatically create Instant Videos for you based on your RSS feeds. "

Here's another one of Eric's videos,
on [how to get coordinates of an existing Revit View, then use them for placing other Views in Dynamo](https://youtu.be/UZl9gpFgxy0).

#### <a name="2.1"></a>Addendum &ndash; NuGet Package and .NET Framework Version

Jason Masters adds some important notes to this in
his [comment below](https://thebuildingcoder.typepad.com/blog/2020/01/devdays-online-and-add-in-migration.html#comment-4774664575):

In terms of upgrading Revit API versions, I'd highly recommend switching from references to the SDK to referencing
the [NuGet package published by Matthew Taylor (Revit_All_Main_Versions_API_x64)](https://www.nuget.org/packages/Revit_All_Main_Versions_API_x64):

<center>
<img src="img/revit_all_main_versions_api.jpg" alt="NuGet package Revit all main versions API" title="NuGet package Revit all main versions API" width="512"/> <!-- 2365 -->
</center>

This package contains all the Revit API references across all versions.
Using NuGet means that any other developer opening your repo won't necessarily need the SDK to build it, it will integrate more easily into CI/CD pipelines, and checking against different API versions is as simple as changing which version of the NuGet package you're using.

Also, if you need to migrate to 2020, you're going to need to first change the .NET target framework version of your project to 4.7, then update your references to the 2020 API.

Many thanks to Jason for these important notes!

#### <a name="3"></a>Join us for our DevDays Online Webinars

We welcome you to join us for our special series of webinars where we’ll be going over the current important development topics.
Some of them were already covered last year's DevCon events.  These webinars are a great opportunity for you to learn about Autodesk Forge and where Autodesk is taking the desktop platforms in the coming year.

Click on the links below to register for the webinar(s) of your choice.

All webinars start at 8am PST (4pm GMT, 5pm CET, 11am EST).
If the session timing is inconvenient for you to attend (which is true for most of our partners in Asia), you can rest assured we will be recording all the sessions and will post them on the web for your later viewing.

Registration is open to all except where noted below:

- <b>Tuesday 2020-02-25 &ndash; DevDays Keynotes</b>
&ndash; Jim Quanci, Senior Director for Software Partner Development, kicks off our DevDays online webinar series with the latest news for desktop developers (get ready for the 2021 releases this Spring), Forge roadmap that includes the Forge Design Automation CAD Engines on the cloud (AutoCAD, Revit, Inventor and 3ds Max) and an overview of the enhanced BIM 360 APIs for the Autodesk Construction Cloud.
<br/>[Register](https://autodesk.zoom.us/webinar/register/WN_J-iJ9Iy1TQ-TYgB3CdQoLg)
- <b>Wednesday 2020-02-26 &ndash; Forge API Update</b>
&ndash; Augusto Goncalves will discuss in detail the updated and new APIs added to the Forge platform during the last year.
<br/>[Register](https://autodesk.zoom.us/webinar/register/WN_MlyzAqW8TF-oC7XPFcC7FA)
- <b>Thursday 2020-02-27 &ndash; Revit, Civil 3D and InfraWorks API Updates</b> (for ADN members only)
&ndash; Join Sasha Crotty and Augusto Goncalves to discover the product and API changes and enhancements coming in the next releases of Revit, Civil 3D and InfraWorks.
<br/>[Register](https://autodesk.zoom.us/webinar/register/WN_jLl0gXjxTnK3PWHsGyzARg)
- <b>Tuesday 2020-03-03 &ndash; Inventor, Vault and Fusion API Update</b> (for ADN members only)
&ndash; We will review upcoming changes in the next release of Inventor as well as recent updates in Vault and Fusion 360 API.
<br/>[Register](https://autodesk.zoom.us/webinar/register/WN_XlRo7ADySLGofmc7M9cdkQ)
- <b>Wednesday 2020-03-04 &ndash; BIM 360 API Update</b>
&ndash; Learn about new APIs in the BIM 360 family products.
We’ll talk about new Model Coordination and Cost Management APIs and other API enhancements.
<br/>[Register](https://autodesk.zoom.us/webinar/register/WN_TIxv3ZpPS1228DYy_-i7HA)
- <b>Thursday 2020-03-05 &ndash; The next Release of AutoCAD APIs</b> (for ADN members only)
&ndash; Discover API changes coming in the upcoming release of AutoCAD Rogue.
<br/>[Register](https://autodesk.zoom.us/webinar/register/WN_h1Mmc-leRjKAFhqIjiv9Sw)

After registering, you will receive a confirmation email containing information about joining the webinars.

<center>
<img src="img/devdays_online_2020.jpg" alt="DevDays Online 2020" title="DevDays Online 2020" width="512"/> <!-- 1024 -->
</center>
