<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- purge via performance advisor
  commennt https://thebuildingcoder.typepad.com/blog/2018/08/purge-unused-using-performance-adviser.html#comment-5716062022
  by Virone Lorenzo
  VB https://thebuildingcoder.typepad.com/blog/2018/08/purge-unused-using-performance-adviser.html#comment-5716062022
  by Matt Taylor, associate and CAD developer at [WSP](https://www.wsp.com)
  migrated by Ollie Green [OliverEGreen](https://github.com/OliverEGreen)
  C# https://github.com/OliverEGreen/CodeSamples/blob/master/PurgeRevitViaAPI.cs
  Python https://github.com/OliverEGreen/CodeSamples/blob/master/PurgeRevitViaAPI.py

- Kai Kasugai <kk@formitas.de> Re: eTransmit functionality

- Now Available: Autodesk Revit IFC Manual Version 2.0
  https://blogs.autodesk.com/revit/2022/02/09/now-available-revit-ifc-manual-version-2-0/
  The Autodesk Revit IFC Manual provides technical guidance for teams working with openBIM workflows. IFC is the basis for exchanging data between different applications through openBIM workflows for building design, construction, procurement, maintenance, and operation, within project teams and across software applications.  According to buildingSMART, IFC “is a standardized, digital description of the built environment, including buildings and civil infrastructure. It is an open, international standard, meant to be vendor-neutral, or agnostic, and usable across a wide range of hardware devices, software platforms, and interfaces for many different use cases.”
  Download version 2 of the manual here, available in 9 languages:    

- AI solves small human programming puzzles
  DeepMind says its new AI coding engine is as good as an average human programmer
  https://www.theverge.com/2022/2/2/22914085/alphacode-ai-coding-program-automatic-deepmind-codeforce

twitter:

 in the #RevitAPI FormulaManager @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Purge and eTransmit for DA4R




Many thanks to ??? for raising this question and confirming the solution!

####<a name="2"></a> Purge via Performance Advisor

- purge via performance advisor
commennt https://thebuildingcoder.typepad.com/blog/2018/08/purge-unused-using-performance-adviser.html#comment-5716062022
by Virone Lorenzo
VB https://thebuildingcoder.typepad.com/blog/2018/08/purge-unused-using-performance-adviser.html#comment-5716062022
by Matt Taylor, associate and CAD developer at [WSP](https://www.wsp.com)
migrated by Ollie Green [OliverEGreen](https://github.com/OliverEGreen)
C# https://github.com/OliverEGreen/CodeSamples/blob/master/PurgeRevitViaAPI.cs
Python https://github.com/OliverEGreen/CodeSamples/blob/master/PurgeRevitViaAPI.py

####<a name="3"></a> eTransmit Functionality in DA4R

Dr. Kai Kasugai
Web       www.formitas.de

Formitas AG

sure, as we derived most of the code from the source you provided us, I am glad to share the few modifications that we made to make it work for us in Design Automation.

This code, as the DynamoRevit code, tries to delete all unused elements and materials from the document.
I think the main modification was to step out of the recursive loop once the purgeable element count does not change anymore.

This is a point that we consider to improve further, as the count itself might not have changed, but the purgeable element ids did change.

Please find attached a hopefully complete set of functions that are required to implement the functionality.


Best regards,
Kai


Hello Jeremy, 
 
I just wanted to give you some feedback on the integration of the Purge Code from the Dynamo for Revit code base, which you and the Dev Team proposed.
 
It really worked great and much faster than our previous attempt!
 
We have tested it in a lot of scenarios, small files and large files and it always worked as expected.
 
This was a really important step for us, as this automation was one of the first that we integrated for our client and
around 100 ACC users can now use that in the growing number of projects that we are currently moving from on-prem to ACC.
 
So thank you very much,
best regards,
Kai


Dear Jeremy, 
thank you very much for your (and the development teams) quick response. That looks very relevant, as it includes a method to purge materials.
I am looking forward to check how that performs in our scenario.
 
While this looks very promising, I would still appreciate to hear from you regarding the other topics discussed (possibility of implementation by Autodesk and,
if so, rough timing).
 
Best regards,
Kai

Am 13.12.2021 um 11:22 schrieb Jeremy Tammik <jeremy.tammik@autodesk.com>:
 
Dear Kai,
 
thank you for your appreciation and code snippet.
 
It was a pleasure meeting you.
 
I discussed the issue with the development team, and they reply:
 
Might be another good instance of learning from the Dynamo for Revit code base, as there is a PurgeUnused node in Dynamo for Revit 2022.

Starts around line 110 here: https://github.com/DynamoDS/DynamoRevit/blob/f1165c9a629d9fcf8ccc7b5300c83cc37e5ea5ed/src/Libraries/RevitNodes/Application/Document.cs

Not sure how closely that follows the ETransmit code, or if it’s a viable option, but worth a review all the same.
 
Here is my link to the Dynamo PurgeUnused method:
 
https://github.com/DynamoDS/DynamoRevit/blob/f1165c9a629d9fcf8ccc7b5300c83cc37e5ea5ed/src/Libraries/RevitNodes/Application/Document.cs#L111-L130
 
Maybe that will help you solve the issue right away today?
 
Best regards and happy advent!
 
jeremy
 

We are trying to implement the eTransmit functionality in DA4R, the Forge Design Automation API for Revit.





####<a name="4"></a> Updated Autodesk Revit IFC Manual

- Now Available: Autodesk Revit IFC Manual Version 2.0
https://blogs.autodesk.com/revit/2022/02/09/now-available-revit-ifc-manual-version-2-0/
The Autodesk Revit IFC Manual provides technical guidance for teams working with openBIM workflows. IFC is the basis for exchanging data between different applications through openBIM workflows for building design, construction, procurement, maintenance, and operation, within project teams and across software applications.  According to buildingSMART, IFC “is a standardized, digital description of the built environment, including buildings and civil infrastructure. It is an open, international standard, meant to be vendor-neutral, or agnostic, and usable across a wide range of hardware devices, software platforms, and interfaces for many different use cases.”
Download version 2 of the manual here, available in 9 languages:    

####<a name="5"></a> AI Solves Programming Tasks

AI solves small human programming puzzles
DeepMind says its new AI coding engine is as good as an average human programmer
https://www.theverge.com/2022/2/2/22914085/alphacode-ai-coding-program-automatic-deepmind-codeforce


**Question:** 

<center>
<img src="img/.jpg" alt="" title="" width="800"/> <!-- 1394 -->
</center>

**Answer:** 

**Response:** 

<pre class="code">
</pre>
