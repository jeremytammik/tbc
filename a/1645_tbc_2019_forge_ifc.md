<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- The Building Coder samples 2019
  https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.138.0

- /a/doc/revit/tbc/git/a/zip/tbc_samples_2019_errors_warnings_1.txt

- AdnRme 2019
  https://github.com/jeremytammik/AdnRme/releases/tag/2019.0.0.0
  
- AdnRevitApiLabsXtra
  https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2019.0.0.0
  
- https://github.com/simonmoreau/RevitToIFC
  [Simon Moreau](https://github.com/simonmoreau) of [Bouygues Immobilier], [BIM42 blog author](http://bim42.com).
  
- [Forge NextGen webinars](http://adndevblog.typepad.com/manufacturing/2018/04/forge-nextgen-webinars.html)
  [tweet](https://autodesk.slack.com/archives/C0RADSBL0/p1524089977000206)

More #RevitAPI @AutodeskRevit 2019 samples, Forge conversion to IFC, accelerators and webinars #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/rvt2019forgeifc

I already described how I installed Revit 2019, compiled the Revit 2019 SDK samples and migrated RevitLookup to the new version.
I now migrated some of the other basic samples and utilities and have some Forge news to share
&ndash; The Building Coder samples for Revit 2019
&ndash; The <code>AdnRme</code> MEP HVAC and electrical samples for Revit 2019
&ndash; AdnRevitApiLabsXtra for Revit 2019
&ndash; Convert RVT to IFC via Forge
&ndash; Forge accelerators
&ndash; Forge webinars
&ndash; Open position for an infrastructure BIM implementation consultant...

--->

### Forge RVT to IFC, ADN Xtra, TBC and AdnRme Updates

I already described how I installed Revit 2019,
[compiled the Revit 2019 SDK samples](http://thebuildingcoder.typepad.com/blog/2018/04/compiling-the-revit-2019-sdk-samples.html)
and [migrated RevitLookup](http://thebuildingcoder.typepad.com/blog/2018/04/revitlookup-2019-and-new-sdk-samples.html) to
the new version.

I now migrated some of the other basic samples and utilities and have some Forge news to share:

- [The Building Coder samples for Revit 2019](#2) 
- [The `AdnRme` MEP HVAC and electrical samples for Revit 2019](#3) 
- [AdnRevitApiLabsXtra for Revit 2019](#4) 
- [Convert RVT to IFC via Forge](#5)
- [Forge accelerators](#6) 
- [Forge webinars](#7)
- [Open Position for an Infrastructure BIM Implementation Consultant in Northern Europe](#8)


####<a name="2"></a>The Building Coder Samples for Revit 2019

The migration of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) to
Revit 2019 was just as trivial as RevitLookup.

- Update the .NET framework target version to 4.7
- Point the Revit API assembly references to the new DLLs.

No code changes were needed.

That does generate [six warning messages about deprecated Revit API usage](zip/tbc_samples_2019_errors_warnings_1.txt).

They all occur in the module `CmdGetMaterials.cs` and concern the use of the `AssetProperties` `[]` operator taking integer and string arguments, e.g.:

- warning CS0618: 'AssetProperties.this[int]' is obsolete: This property is deprecated in Revit 2019 and will be removed in the next version of Revit. We suggest you use the 'FindByName(String)' or 'Get(int)' method instead.

I will ignore them for the time being.

The flat migrated version is [release 2019.0.138.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.138.0).

The [diff between the last 2018 version, 2018.0.138.4, and 2019.0.138.0](https://github.com/jeremytammik/the_building_coder_samples/compare/2018.0.138.4...2019.0.138.0),
shows the minimal changes I made.

To add the final finishing touch, I also updated the readme file with new Revit and .NET framework version badges.

The current version is [2019.0.138.1](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.138.1).


####<a name="3"></a>The AdnRme MEP HVAC and Electrical Samples for Revit 2019

Similarly, I updated the [AdnRme MEP HVAC and electrical samples](https://github.com/jeremytammik/AdnRme) for Revit 2019.

The flat migration is captured in [release 2019.0.0.0](https://github.com/jeremytammik/AdnRme/releases/tag/2019.0.0.0), 
and I added badges here as well in [release 2019.0.0.1](https://github.com/jeremytammik/AdnRme/releases/tag/2019.0.0.1).


####<a name="4"></a>AdnRevitApiLabsXtra for Revit 2019

Next, I migrated the [AdnRevitApiLabsXtra](https://github.com/jeremytammik/AdnRevitApiLabsXtra) with similar ease.

No code changes required &ndash; again, just updated the .NET framework version and the Revit API assembly DLL references.

The current version is [2019.0.0.1](https://github.com/jeremytammik/AdnRevitApiLabsXtra/releases/tag/2019.0.0.1).


####<a name="5"></a>Convert RVT to IFC via Forge

Looking toward the future making use of web-based component technology,
[Simon Moreau](https://github.com/simonmoreau) of [Bouygues Immobilier](https://www.bouygues-immobilier.com), author of the [BIM42 blog](http://bim42.com),
shared his nice new [RevitToIFC web app](https://github.com/simonmoreau/RevitToIFC).

Revit To IFC is a web application using
the [Autodesk Forge](https://autodesk-forge.github.io) web
services to convert Revit file to the IFC format.
You can use it to upload a Revit file to Forge and download back the resulting converted file.

<center>
<img src="https://raw.githubusercontent.com/simonmoreau/RevitToIFC/master/doc/revitToIfc.gif" alt="RevitToIFC" width="500"/>
</center>


####<a name="6"></a>Forge Accelerators

Does this inspire you to want to get started with Forge yourself?

An easy way to do so is to attend a [Forge accelerator](http://autodeskcloudaccelerator.com/forge-accelerator).

The upcoming ones are here:

- Boston, USA &ndash; April 30 - May 4
- Bengaluru, India &ndash; May 7-10
- Barcelona, Spain &ndash; June 11-15
- San Francisco, USA &ndash; June 18-22
- Sydney, Australia &ndash; July 9-12

####<a name="7"></a>Forge Webinars

Two upcoming Forge webinars are dedicated to more bleeding edge technology, HFDM &ndash; High Frequency Data Management &ndash; and the new app framework:

- 2018-04-26, 8.00 AM PST, 90 minutes
&ndash; [The Future of Making Things on Forge: Sneak Peek at Forge NextGen (HFDM & App Framework)](https://attendee.gotowebinar.com/register/8209398405782333955)
- 2018-05-09, 8.00 AM PST, 90 minutes
&ndash; [A technical introduction to Forge High Frequency Data Management (HFDM) SDK and Forge App Framework](https://attendee.gotowebinar.com/register/3624195224359611394)

You might also want to check out the recording on the topic from the Forge DevCon 2017 class at Autodesk University,
[FDC125274: Introducing the Future of CAD - Forge HFDM and the Forge App Framework](http://au.autodesk.com/au-online/classes-on-demand/class-catalog/classes/year-2017/forge/fdc125274).

<center>
<img src="img/datacentricapps_800x640.gif" alt="Data-centric apps" width="400"/>
</center>

####<a name="8"></a>Open Position for an Infrastructure BIM Implementation Consultant in Northern Europe

Let's end for today with something completely different:
 
Would you be interested in a position as a *BIM Implementation Consultant for AEC Infrastructure* with a flexible location in Northern Europe?

####<a name="8.1"></a>Position Overview

Autodesk Consulting implements Autodesk BIM solutions for our customers across the region. We are looking for a BIM Implementation Consultant for the infrastructure industry (Rail, Road, Airport, Utilities) to implement our solutions on customer sites.

As a BIM Implementation Consultant, you will be essential for working directly with our customers and developing/contributing to their BIM implementation strategy (in collaboration with our sales, consulting delivery, customer success and business development teams). Our focus is on enterprise customers.

The position combines your technical and business consulting skills, your infrastructure industry experience, your technical and professional know-how to help drive successful adoption of Autodesk’s solutions.

You will provide process assessments, facilitate customer meetings, training, implementation, mentoring, as well as create documentation and customer deliverables. Provides feedback based on customers’ experiences to product development group and customer success services teams for product and process improvements.

####<a name="8.2"></a>Responsibilities

- Lead implementation of Autodesk's BIM solution on projects, collaborating with Autodesk sales and consulting delivery and business development teams
- Analyse customer business workflows and document findings
- Define, construct, and carry out the adoption of BIM implementation plans, leveraging your industry experience and best practices
- Manage customer relationships on implementation projects
- Document, share and promote BIM team best practices for redistribution and consistent implementation at a corporate level
- Own a part of the BIM Solution Portfolio corresponding to your area of expertise
- Identify opportunities for new projects to enhance customer effectiveness in their own business
- Provide support to Sales/Business Development to help customer understand the value of engaging consulting resources; provide support to Solutions Architects and Business Development in developing customer proposals

####<a name="8.3"></a>Minimum Qualifications

-  Must have at least 5 years of experience in the Infrastructure Industry.
-  Broad understanding, garnered through work in practice, of the Infrastructure industries throughout the asset lifecycle
-  Broad understanding of our Autodesk technology around BIM
-  Understanding of BIM principles and at least 2 years of hands-on experience implementing BIM in an enterprise environment and/or on projects.
-  Fluency oral and written in Swedish and English, candidates with additional language skills would have a clear advantage
-  Excellent verbal and written communication skills; ability to communicate complex technical details coherently
-  Able to travel and enjoy working with diverse groups of people with widely variable technical skills

####<a name="8.4"></a>Nice to Have Skills

- Experience in software product adoption
- Experience in business process improvement principles, and implementing change in complex organizations

Let me know and I'll refer you  :-)
