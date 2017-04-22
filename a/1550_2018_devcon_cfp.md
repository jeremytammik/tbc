<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="run_prettify.js" type="text/javascript"></script> --> 
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Revit 2018
  https://github.com/jeremytammik/RevitLookup
  https://github.com/jeremytammik/the_building_coder_samples
  /a/doc/revit/tbc/git/a/zip/tbc_samples_2018_migr_01.txt

RevitLookup 2018 and Forge DevCon CFP #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/2018_devcon

https://twitter.com/jeremytammik/status/855709326094323712

SEO Keyword Phrase and Relevant Hashtags: DevCon, ForgeDevCon, API, Developers, BIM, IoT, VR, AR, 3D, webGL, CloudComputing, Cloud, App, MobileDev

Revit 2018 has been released.
The Revit 2018 SDK is available from
the Revit Developer Centre.
I migrated RevitLookup and The Building Coder samples.
Finally, the call for proposals has opened for the Forge DevCon at Autodesk University in Las Vegas
&ndash; Revit 2018 Software Developers Kit
&ndash; RevitLookup 2018
&ndash; The Building Coder samples 2018
&ndash; Forge DevCon call for proposals...

-->

### RevitLookup 2018 and Forge DevCon CFP

Revit 2018 has been released.

The Revit 2018 SDK (Software Developers Kit) is available from
the [Revit Developer Centre](http://www.autodesk.com/developrevit).

I migrated [RevitLookup](https://github.com/jeremytammik/RevitLookup)
and [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).

Finally, the call for proposals has opened for the Forge DevCon at Autodesk University in Las Vegas:

- [Revit 2018 Software Developers Kit](#2)
- [RevitLookup 2018](#3)
- [The Building Coder samples 2018](#4)
- [Forge DevCon call for proposals](#5)


#### <a name="2"></a>Revit 2018 Software Developers Kit

Revit 2018 has been released and the Revit 2018 SDK is available from
the [Revit Developer Centre](http://www.autodesk.com/developrevit).

First of all, you might want to check out
the [overview of new product features](http://www.autodesk.com/products/revit-family/features) and
check how they correlate with the API functionality enhancements discussed in
the [DevDays Online API news](http://thebuildingcoder.typepad.com/blog/2017/03/revitlookup-enhancements-future-revit-and-other-api-news.html).

The Revit SDK contains documentation and samples, which I unpacked in my local folder `C:\a\lib\revit\2018\SDK`:

- Subdirectories:
    - Add-In Manager
    - Macro Samples
    - Revit Server SDK
    - Revit Structure
    - REX SDK
    - Samples
    - Structural Analysis SDK
- Documents:
    - Autodesk Icon Guidelines.pdf
    - Getting Started with the Revit API.docx
    - Read Me First.doc
    - Revit Platform API Changes and Additions.docx
    - RevitAddInUtility.chm
    - RevitAPI.chm
    - RevitAPISDK_Eula.rtf

The most important things to install and always keep at hand are:

- The Revit API help file `RevitAPI.chm`
- The Visual Studio solution containing all the SDK samples, `Samples\SDKSamples.sln`

You will need these constantly for research on how to solve specific Revit API programming tasks.

Another main source of more in-depth official information is provided by 
the [developer guide](http://help.autodesk.com/view/RVT/2018/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42) in
the [online help](http://help.autodesk.com/view/RVT/2018/ENU/).

Obviously, while migrating your existing samples, the first place to consult in case of problems is the list of *Revit Platform API Changes and Additions.docx*.

It is also included as a separate section in the help file.


#### <a name="3"></a>RevitLookup 2018

After installing Revit 2018, the first thing I did was to update and
recompile [RevitLookup](https://github.com/jeremytammik/RevitLookup).

The flat migration with no significant changes produced
the [release 2018.0.0.0](https://github.com/jeremytammik/RevitLookup/releases/tag/2018.0.0.0).

That was rather easy!


#### <a name="4"></a>The Building Coder Samples 2018

Next, I turned to something more complex,
[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).

The flat migration from Revit 2017 to Revit 2018 initially caused several compilation errors.

I fixed those with the
following [GitHub commits](https://github.com/jeremytammik/the_building_coder_samples/commits/master):

- updated Revit API references to Revit 2018
- replaced old `Autodesk.Revit.Utility` namespace by new `Autodesk.Revit.DB.Visual`
- added `RevitAPIUIMacros.dll`
- `Ellipse.Create` renamed to `Ellipse.CreateCurve`
- `Ellipse.CreateCurve` returns a curve not an ellipse
- `RevitLinkLoadResult` was renamed to `LinkLoadResult`

After that, compilation completed successfully with zero errors.

Seven warnings about deprecated API usage remained:

- CmdAzimuth.cs(104,13,104,53): warning CS0618: `ProjectLocation.get_ProjectPosition(XYZ)` is obsolete: This property is deprecated in Revit 2018. Use `ProjectLocation.GetProjectPosition` instead.
- CmdUnrotateNorth.cs(48,33,48,61): warning CS0618: `ProjectLocation.SiteLocation` is obsolete: This property is deprecated in Revit 2018. Use `ProjectLocation.GetSiteLocation` instead.
- CmdUnrotateNorth.cs(79,33,79,61): warning CS0618: `ProjectLocation.SiteLocation` is obsolete: This property is deprecated in Revit 2018. Use `ProjectLocation.GetSiteLocation` instead.
- CmdUnrotateNorth.cs(163,17,163,57): warning CS0618: `ProjectLocation.get_ProjectPosition(XYZ)` is obsolete: This property is deprecated in Revit 2018. Use `ProjectLocation.GetProjectPosition` instead.
- CmdSetTagType.cs(226,30,228,48): warning CS0618: `Document.NewTag(View, Element, bool, TagMode, TagOrientation, XYZ)` is obsolete: This method is deprecated in Revit 2018 and will be removed in a future version. Use `IndependentTag.Create` instead.
- CmdGetMaterials.cs(248,48,248,62): warning CS0618: `AssetPropertyDoubleArray3d.Value` is obsolete: This property is deprecated in Revit 2018. Use `getValueAsDoubles` or `getValueAsXYZ` instead.
- CmdGetMaterials.cs(254,48,254,62): warning CS0618: `AssetPropertyDoubleArray4d.Value` is obsolete: This property is deprecated in Revit 2018. Use `getValueAsDoubles` instead.

I captured this state in [The Building Coder sample release 2018.0.132.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.132.0).

Later, I fixed the trivial warnings:

- `get_ProjectPosition` renamed to `GetProjectPosition`
- replaced `ProjectLocation` `SiteLocation` property by `GetSiteLocation` method
 
These fixes have been applied in [release 2018.0.132.1](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.132.1).

Three warnings remain to be sorted out, listed
in [my migration notes](zip/tbc_samples_2018_migr_01.txt).

The asset related ones are explained in the section on *Asset API Changes* in the *changes and additions* document mentioned above.

Good luck with your own add-in migrations!

Meanwhile, looking a bit further ahead:


#### <a name="5"></a>Forge DevCon Call for Proposals

The [Forge DevCon](https://forge.autodesk.com/DevCon)
[call for proposals](https://forge.autodesk.com/DevCon/call-for-proposals) is open.
Applications are now being accepted to speak at the November event in Las Vegas.
The deadline for submission is next month, May 17.

Forge DevCon is taking place on November 13–14 in Las Vegas and will constitute the world’s largest gathering of design- and engineering-focused software developers.

Would you like to share your experience working with the Forge platform, or how you creatively used Forge to enhance a project or solve a challenge?

If so, the [DevCon call for speakers](https://forge.autodesk.com/DevCon/call-for-proposals) is for you.
It is open now and closes May 17.

If you speak at Forge DevCon, you also get the benefit of attending [Autodesk University](http://au.autodesk.com),
the premier Autodesk learning and networking event.

AU takes place immediately after DevCon, November 14–16, in the same location.

About **DevCon**: The [Forge Developer Conference](https://forge.autodesk.com/DevCon) is
the largest global gathering of design- and engineering-focused software developers, bringing together over 1,500 software developers, engineers, business owners and information officers as a community interested in creating web and mobile applications that use the Autodesk Forge or Fusion 360 APIs.

About **Autodesk University**: [AU Las Vegas 2017](http://au.autodesk.com/las-vegas) is your chance to connect with the best in the business, share technical knowledge, solve unique business challenges, and gain a deeper understanding of cross-industry opportunities. Plus, you can learn best practices, stay on top of the latest trends, and experience advanced technology first-hand. Join your global community of professional designers and engineers as we explore the Future of Making Things.

About **Forge**: [Autodesk Forge](https://forge.autodesk.com) is a
connected [developer cloud platform](https://developer.autodesk.com) that
powers the future of making things. It comprises software components, technical resources, and an engaged community.

I am looking forward to seeing you in Las Vegas this year!

<center>
<img src="img/2017_forge_devcon_promo_image3_1024x512.png" alt="Forge DevCon" width="512"/>
</center>

