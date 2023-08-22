<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- tbc 15th birthday august 22, almost passed its puberty by now, soon a grown-up blog, assuminbg human time spans for maturilty

- .net core migration coming in september
  dropping macro support for document macros
  https://thebuildingcoder.typepad.com/blog/2022/05/analysis-of-macros-journals-and-add-in-manager.html#2
  how to convert form project to application macro

- .net core 5
  https://autodesk.slack.com/archives/C0531NMR189/p1692239493644339
  Dave Kurtz
  Hey everyone. I manage the monthly Revit Preview Release project that publishes DEV builds to our customers for testing and feedback. I was wondering if there is some documentation on when we might expose some of this .NET work in the main branch for external testing. I have some eager and excited testers asking about it - but I remain intentionally vague about any timeline as I wasn't certain the status.
  Are there any plans in September or October to expose this work for external testers and if so will there be any documentation available to share with them? I know this is a large and complex project that is still being worked on. I'm just trying to get a sense of what may be coming down the road, and how we may want to message that to Preview testers when the time comes. Thanks! (edited)
  Michael Morris
  we are hoping to have the .NET work in the main branch for the October preview release, but we are behind schedule and aren't sure will hit our target.
  For now, thank you for being vague on the timeline.
  Mona Khanapurkar
  thanks for checking here. We are targeting a preview release in Fall 2023. As soon as we can get Revit to build with the updated .Net version we will be in a better position to provide the details. In the meantime, below is a rough draft for communications. CC
  @Jeremy Tammik for editorial review.
  https://www.keanw.com/2023/08/the-next-release-of-autocad-and-net.html
  https://adndevblog.typepad.com/autocad/2023/08/call-for-action-next-release-of-autocad.html
autocad and civil3d already published the preview
revit is still working on it
acad is platform
revit has many dependencies (dynamo, inventor) and addons
microsoft is releasing .net code 8 in november
acad migrated to .net core 6
inventor went straight to 7
revit is debating whether to move straight to 7 or 8
acad worked around the problem preventing 6 and are now discussing moving straight to 7 as well
all products are using acad components
revit september preview will not contain anything yet
revit november is not guaranteed
point to autocad one
dynamo also annouced that they are working on it

Dynamo upgrading to .NET 6
https://forum.dynamobim.com/t/dynamo-upgrading-to-net-6/90638/21?page=2

Does revit target .net standard
https://forums.autodesk.com/t5/revit-api-forum/does-revit-target-net-standard/m-p/9792894?search-action-id=812462935117&search-result-uid=9792894

RevitAPI should support .NET 5+
https://forums.autodesk.com/t5/revit-api-forum/revitapi-should-support-net-5/m-p/10533160?search-action-id=812462935117&search-result-uid=10533160

TypeLoadException on Addin startup after changing project to .NET 5
https://forums.autodesk.com/t5/revit-api-forum/typeloadexception-on-addin-startup-after-changing-project-to-net/m-p/10341283?search-action-id=812462935117&search-result-uid=10341283

Has anyone been successful in building a NetStandard-2.0 or Net-5.0 plug-in?
https://forums.autodesk.com/t5/revit-api-forum/has-anyone-been-successful-in-building-a-netstandard-2-0-or-net/m-p/10694884?search-action-id=812462935117&search-result-uid=10694884

familiarise yourself with the Revit feedback portal now.
then you will be ready to check out the new Revit API as soon as it becomes available

- polygon area algorithms
  https://forums.autodesk.com/t5/revit-api-forum/area-of-a-wall-opening/m-p/12174104#M73476

- AI recreates clip of Pink Floyd song from recordings of brain activity
  https://www.newscientist.com/article/2387343-ai-recreates-clip-of-pink-floyd-song-from-recordings-of-brain-activity/

- Allie K. Miller on LinkedIn: You can now write one sentence to train an entire ML model.
  185 comments
  You can now write one sentence to train an entire ML model.
  How does it work?
  You just describe the ML model you want ... a chain of AI systems will take that ...
  https://www.linkedin.com/posts/alliekmiller_you-can-now-write-one-sentence-to-train-an-activity-7097974848001331200-DtJE?utm_source=share&utm_medium=member_desktop\

- compression and size reduction
  1759026 Aug 22 11:33 2023-09-01_nachbarschaftstreffen.jpg height 3506 pixel
   850770 Aug 22 13:01 2023-09-01_nachbarschaftstreffen1.jpg compressed using https://compressjpeg.com/
   149291 Aug 22 13:03 2023-09-01_nachbarschaftstreffen2.jpg height 900
    97220 Aug 22 13:03 2023-09-01_nachbarschaftstreffen3.jpg compressed using https://compressjpeg.com/
  factor ca. 18.1

twitter:

 @AutodeskRevit
  the #RevitAPI  #BIM @DynamoBIM @AutodeskAPS

&ndash; ...

linkedin:


#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### 15 Yr TBC and .NET Core



####<a name="2"></a> 15 Years of The Building Coder

- tbc 15th birthday august 22, almost passed its puberty by now, soon a grown-up blog, assuminbg human time spans for maturilty


####<a name="3"></a>


- .net core migration coming
dropping macro support for document macros
https://thebuildingcoder.typepad.com/blog/2022/05/analysis-of-macros-journals-and-add-in-manager.html#2
how to convert form project to application macro

- .net core 5
https://autodesk.slack.com/archives/C0531NMR189/p1692239493644339
Dave Kurtz
Hey everyone. I manage the monthly Revit Preview Release project that publishes DEV builds to our customers for testing and feedback. I was wondering if there is some documentation on when we might expose some of this .NET work in the main branch for external testing. I have some eager and excited testers asking about it - but I remain intentionally vague about any timeline as I wasn't certain the status.
Are there any plans in September or October to expose this work for external testers and if so will there be any documentation available to share with them? I know this is a large and complex project that is still being worked on. I'm just trying to get a sense of what may be coming down the road, and how we may want to message that to Preview testers when the time comes. Thanks! (edited)
Michael Morris
we are hoping to have the .NET work in the main branch for the October preview release, but we are behind schedule and aren't sure will hit our target.
For now, thank you for being vague on the timeline.
Mona Khanapurkar
thanks for checking here. We are targeting a preview release in Fall 2023. As soon as we can get Revit to build with the updated .Net version we will be in a better position to provide the details. In the meantime, below is a rough draft for communications. CC
@Jeremy Tammik for editorial review.
https://www.keanw.com/2023/08/the-next-release-of-autocad-and-net.html
https://adndevblog.typepad.com/autocad/2023/08/call-for-action-next-release-of-autocad.html
autocad and civil3d already published the preview
revit is still working on it
acad is platform
revit has many dependencies (dynamo, inventor) and addons
microsoft is releasing .net code 8 in november
acad migrated to .net core 6
inventor went straight to 7
revit is debating whether to move straight to 7 or 8
acad worked around the problem preventing 6 and are now discussing moving straight to 7 as well
all products are using acad components
revit september preview will not contain anything yet
revit november is not guaranteed
point to autocad one
dynamo also annouced that they are working on it

Dynamo upgrading to .NET 6
https://forum.dynamobim.com/t/dynamo-upgrading-to-net-6/90638/21?page=2

Does revit target .net standard
https://forums.autodesk.com/t5/revit-api-forum/does-revit-target-net-standard/m-p/9792894?search-action-id=812462935117&search-result-uid=9792894

RevitAPI should support .NET 5+
https://forums.autodesk.com/t5/revit-api-forum/revitapi-should-support-net-5/m-p/10533160?search-action-id=812462935117&search-result-uid=10533160

TypeLoadException on Addin startup after changing project to .NET 5
https://forums.autodesk.com/t5/revit-api-forum/typeloadexception-on-addin-startup-after-changing-project-to-net/m-p/10341283?search-action-id=812462935117&search-result-uid=10341283

Has anyone been successful in building a NetStandard-2.0 or Net-5.0 plug-in?
https://forums.autodesk.com/t5/revit-api-forum/has-anyone-been-successful-in-building-a-netstandard-2-0-or-net/m-p/10694884?search-action-id=812462935117&search-result-uid=10694884

familiarise yourself with the Revit feedback portal now.
then you will be ready to check out the new Revit API as soon as it becomes available

####<a name="3"></a>

- polygon area algorithms
https://forums.autodesk.com/t5/revit-api-forum/area-of-a-wall-opening/m-p/12174104#M73476

####<a name="3"></a>

- AI recreates clip of Pink Floyd song from recordings of brain activity
https://www.newscientist.com/article/2387343-ai-recreates-clip-of-pink-floyd-song-from-recordings-of-brain-activity/

####<a name="3"></a>

- Allie K. Miller on LinkedIn: You can now write one sentence to train an entire ML model.
185 comments
You can now write one sentence to train an entire ML model.
How does it work?
You just describe the ML model you want ... a chain of AI systems will take that ...
https://www.linkedin.com/posts/alliekmiller_you-can-now-write-one-sentence-to-train-an-activity-7097974848001331200-DtJE?utm_source=share&utm_medium=member_desktop\

####<a name="3"></a> Please Compress Stuff

Some people are starting to pay attention to the carbon footprint of today's widespread and growing usage of the Internet and digital devices.

Some estimates deem it comparable with the pollution generated by airlines and flying.

carbon footprint of driving versus flying

Your flight:

From: Basel (CH), BSL to: Malaga (ES), AGP, Roundtrip, Economy Class, ca. 3,100 km, 2 travellers

CO2 amount: 1.2 t

https://co2.myclimate.org/en/portfolios?calculation_id=6115626

from Lörrach
to La Garnatilla, 18614, Granada, Spain
1,935 km
CO2 amount: 0.476 t
return trip 1 t

Emissions by sector
https://ourworldindata.org/emissions-by-sector
greenhouse_emissions_by_sector.png

The Internet’s Invisible Carbon Footprint
https://foundation.mozilla.org/en/blog/ai-internet-carbon-footprint/
> Did you know that writing an email can send 17 grams or more of carbon dioxide into the atmosphere? Or that going audio-only on Zoom calls reduces carbon emissions by up to 96%?

Powering the Internet: Your Virtual Carbon Footprint [Infographic]
https://www.webfx.com/blog/marketing/carbon-footprint-internet/
internet_carbon_footprint.png


- compression and size reduction
1759026 Aug 22 11:33 2023-09-01_nachbarschaftstreffen.jpg height 3506 pixel
850770 Aug 22 13:01 2023-09-01_nachbarschaftstreffen1.jpg compressed using https://compressjpeg.com/
149291 Aug 22 13:03 2023-09-01_nachbarschaftstreffen2.jpg height 900
97220 Aug 22 13:03 2023-09-01_nachbarschaftstreffen3.jpg compressed using https://compressjpeg.com/
factor ca. 18.1

####<a name="3"></a>


<center>
<img src="img/" alt="" title="" width="100"/> <!-- Pixel Height: 460 Pixel Width: 460 -->
</center>

**Answer:**

**Response:**

<pre class="prettyprint">
</pre>


**Question:**


####<a name="4"></a>
