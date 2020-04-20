<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- parameter type change breaks families
[Q]
We have some families that fail to load (only in 2021).  We think we have narrowed it down to a Revit change.
 
Would you or someone be able to review this and let us know if it is a bug or feature?  Is there a published list of changes for other parameters that can review.
 
When you export to a type “Family Types” in Revit 2012-2020 for PIPING__FLOW (GPM) you get for instance:
Cold Water Flow##PIPING_FLOW##GALLONS_US_PER_MINUTE
 
When you do the same in Revit 2021, however you get:
Cold Water Flow##PIPING_FLOW##US_GALLONS_PER_MINUTE
 
The bad news being that Revit 2021 does not accept GALLONS_US_PER_MINUTE Anymore, instead it expects (and does not ‘upconvert’) the new US_GALLONS_PER_MINUTE.
Eg for 2000+ existing type catalogs this is a breaking change!!

[A] I found this Jira:
https://jira.autodesk.com/browse/REVIT-158981
 
GLOB: Units INCHES_OF_WATER and GALLONS_US_PER_MINUTE no longer work in Revit 2021 txt family type catalogs
 
According to Jira, the change is intentional.  But probably upgrade from the previous verion is not working?

The change to the DB string is intentional. No upgrade from the previous version’s DB strings has been implemented.
 
We documented the changes in the [developer guide](https://help.autodesk.com/view/RVT/2021/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html).

A table of changes to database identifiers may be found at the bottom of the page
on [Introduction &gt; Application and Document &gt; Document Functions &gt; Units](http://help.autodesk.com/view/RVT/2021/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Application_and_Document_Units_html).


- freecodecamp
  [How to Style Your React Apps with Less Code Using Tailwind CSS and Styled Components](https://www.freecodecamp.org/news/how-to-style-your-react-apps-with-less-code-using-tailwind-css-and-styled-components/)
  [LockdownConf – A Free Online Conference to Help You Prepare for a Post-Pandemic World](https://www.freecodecamp.org/news/lockdownconf-free-developer-conference/)
  [The Ultimate Python Beginner's Handbook](https://www.freecodecamp.org/news/the-python-guide-for-beginners/)
  [Learn Data Analysis with Python – A Free 4-Hour Course](https://www.freecodecamp.org/news/learn-data-analysis-with-python-course/)
  

- padlock url
  https://www.whynopadlock.com/results/95468a2b-f094-45a4-9a74-4250a628cc09
  why_no_padlock_test_results.png
TEST RESULTS
Test Information
 Tested URL https://thebuildingcoder.typepad.com/
 Test completed Tue, Apr 14, 2020 3:02 PM Eastern Time (GMT -5)
 Results URL https://www.whynopadlock.com/results/95468a2b‑f094‑45a4‑9a74‑4250a628cc09 
SSL Connection - Pass
 SSL Certificate Info 
Certificate Issuer
COMODO CA Limited
Certificate Type
COMODO ECC Domain Validation Secure Server CA 2
Issued On
2019-11-25
 Force HTTPS Your webserver is forcing the use of SSL.
 Valid Certificate Your SSL Certificate is installed correctly.
 Domain Matching Your SSL certificate matches your domain name!
Protected Domains:
ssl919196.cloudflaressl.com
*.typepad.com
typepad.com
 Signature Your SSL certificate is using a sha256 signature!
 Expiration Date Your SSL certificate is current. Your SSL certificate expires in 49 days. (2020-06-02)
Mixed Content - Errors
 Soft Failure An image with an insecure url of "http://thebuildingcoder.typepad.com/tbc_banner6_1200_200.png" was loaded on line: 4572 of https://thebuildingcoder.typepad.com/.
This URL will need to be updated to use a secure URL for your padlock to return.

why_no_padlock_banner_lacks_https.png

twitter:

Revit 2021 unit types in family type catalogues, Python training courses and padlocking in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/unit_type_catalog

Unfortunately, the new unit type name functionality can cause a problem loading a family with a type catalogue
&ndash; Unit type update affects family type catalogue loading
&ndash; New FreeCodeCamp courses
&ndash; Padlocking The Building Coder...

linkedin:

Revit 2021 unit types in family type catalogues, Python training courses and padlocking in the #RevitAPI

https://bit.ly/unit_type_catalog

Unfortunately, the new unit type name functionality can cause a problem loading a family with a type catalogue:

- Unit type update affects family type catalogue loading
- New FreeCodeCamp courses
- Padlocking The Building Coder...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Revit 2021 Unit Types in Family Type Catalogues 

Unfortunately, the new unit type name functionality can cause a problem loading a family with a type catalogue:

- [Unit type update affects family type catalogue loading](#2)
- [New FreeCodeCamp courses](#3)
- [Padlocking The Building Coder](#4)

####<a name="2"></a> Unit Type Update Affects Family Type Catalogue Loading

**Question:** We have some families that fail to load in Revit 2021.

We have narrowed it down to a Revit change.
 
When you export PIPING__FLOW (GPM) data to a type 'Family Types' in previous versions of Revit, you get something like this:

<pre>
Cold Water Flow##PIPING_FLOW##GALLONS_US_PER_MINUTE
</pre>
 
However, when you do the same in Revit 2021, you get:

<pre>
Cold Water Flow##PIPING_FLOW##US_GALLONS_PER_MINUTE
</pre>
 
The bad news for us being that Revit 2021 does not accept GALLONS_US_PER_MINUTE anymore.
Instead, it expects (and does not ‘upconvert’) the new US_GALLONS_PER_MINUTE.

This is a breaking change for our existing type catalogues.

Is there a published list of changes for parameters that we can review?

**Answer:** Yes, indeed, this is an intentional change.
Sorry that it is affecting you so hard.
Just as you say, no automatic upgrade from the previous version’s DB strings has been implemented.
 
We documented these changes in the [developer guide](https://help.autodesk.com/view/RVT/2021/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html).

The table of changes to database identifiers can be found at the bottom of the page
on [Introduction &gt; Application and Document &gt; Document Functions &gt; Units](http://help.autodesk.com/view/RVT/2021/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Application_and_Document_Units_html).

####<a name="3"></a> New FreeCodeCamp Courses

I always enjoy browsing through the FreeCodeCamp courses recommended in Quincy Larson's newsletter.

Last week's bunch looked especially useful to me, for instance these:

- [The Ultimate Python Beginner's Handbook](https://www.freecodecamp.org/news/the-python-guide-for-beginners/)
- [Learn Data Analysis with Python &ndash; A Free 4-Hour Course](https://www.freecodecamp.org/news/learn-data-analysis-with-python-course/)
- [LockdownConf &ndash; A Free Online Conference to Help You Prepare for a Post-Pandemic World](https://www.freecodecamp.org/news/lockdownconf-free-developer-conference/)
- [How to Style Your React Apps with Less Code Using Tailwind CSS and Styled Components](https://www.freecodecamp.org/news/how-to-style-your-react-apps-with-less-code-using-tailwind-css-and-styled-components/)

####<a name="4"></a> Padlocking The Building Coder

Last week, colleagues pointed out that some of the Autodesk developer blogs were displaying a message saying 'Not secure' in the browser address bar:

<center>
<img src="img/why_no_padlock_blog_url_not_secure.png" alt="Address bar warning 'not secure'" title="Address bar warning 'not secure'" width="504"/>
</center>

The problem is caused by a mixed content error, where some references from the blog are not secure.

You can check the site either in Firefox, or using an external page, e.g., [whynopadlock.com](https://www.whynopadlock.com), which will conveniently show the references causing the mixed content error:
 
<center>
<img src="img/why_no_padlock_test_results.png" alt="Test results" title="Test results" width="764"/>
</center>

<pre>
TEST RESULTS
Test Information
Tested URL https://thebuildingcoder.typepad.com/
Mixed Content - Errors
Soft Failure An image with an insecure url of "http://thebuildingcoder.typepad.com/tbc_banner6_1200_200.png" was loaded on line: 4572 of https://thebuildingcoder.typepad.com/.
This URL will need to be updated to use a secure URL for your padlock to return.
</pre>

In The Building Coder, it was only the banner image, and it can be simply fixed by adding `https` to the image reference in the CSS.

<center>
<img src="img/why_no_padlock_banner_lacks_https.png" alt="Adding https to banner image in CSS" title="Adding https to banner image in CSS" width="742"/>
</center>

Now all is well and the site is padlocked again:

<center>
<img src="img/why_no_padlock_fixed.png" alt="Padlocked URL" title="Padlocked URL" width="636"/>
</center>

