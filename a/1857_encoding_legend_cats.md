<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Categories that can create legend components
  https://forums.autodesk.com/t5/revit-api-forum/categories-that-can-create-legend-components/m-p/9659069
 
- 15738224 [Encoding issues when reading data from size lookup tables with cyrillic characters]
  https://forums.autodesk.com/t5/revit-api-forum/russian-letters-doesn-t-export-in-lookup-tables/m-p/9056270

- Autodesk AEC customers demand better value
  https://www.aecmag.com/comment-mainmenu-36/2046-autodesk-aec-customers-demand-better-value
  AEC Magazine 

twitter:

Categories for legend components and encoding issue with Cyrillic characters fixed in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/legendcats

Solutions, criticism, opportunities
&ndash; Categories for legend components
&ndash; Encoding issue with Cyrillic characters fixed
&ndash; AEC customers demand better value
&ndash; Construction executive job...

linkedin:

Categories for legend components and encoding issue with Cyrillic characters fixed in the #RevitAPI

https://bit.ly/legendcats

Solutions, criticism, opportunities:

- Categories for legend components
- Encoding issue with Cyrillic characters fixed
- AEC customers demand better value
- Construction executive job...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Cyrillic Encoding and Categories for Legends

Solutions, criticism, opportunities:

- [Categories for legend components](#2)
- [Encoding issue with Cyrillic characters fixed](#3)
- [AEC customers demand better value](#4)
- [Construction executive job](#5)

####<a name="2"></a> Categories for Legend Components

Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas and
Juan Jesús [jjesusdelpino](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3486289) Del Pino Campos
collaborated nicely to determine
which [categories that can create legend components](https://forums.autodesk.com/t5/revit-api-forum/categories-that-can-create-legend-components/m-p/9659069):

**Question:** Is there a way to know if a legend component can be created from a determined category?
E.g., I can create legend components from cable trays, but I can't do it with railings.
I haven't seen any specific rule o parameter that specifies this property.
Any ideas?
Thanks.

**Answer:** I've found no obvious solution to this via the API, i.e., a Boolean property on the category class.

I believe also that Legend views don't have a specific class.

So, there is no static methods available on such for checking category suitability of placed elements that way either.

You'll find sketch based system families (floors/Railings/Stairs...) are not applicable since Legend views don't have a sketch plane.

If you think about it, apart from annotation objects, Legend views contain types not instances (since you usually drag the types onto the Legend from the project browser).

However, there is also no property for this indicated on `ElementType` class.

**Response:** I also have not found any solution for this through API, so I have ended up checking and making the list manually.

I put it here in case someone looking for the same thing comes:

<pre class="code">
FamilyInstances = [“OST_DuctAccessory”, “OST_PipeAccessory”, “OST_PlumbingFixtures”, “OST_StructuralFraming”, “OST_StructuralFoundation”, “OST_ElectricalEquipment”, “OST_SpecialityEquipment”, “OST_MechanicalEquipment”, “OST_LightingFixtures”, “OST_Furniture”, “OST_Casework”, “OST_Columns”, “OST_StructuralColumns”, ”OST_Doors”, ”OST_Sprinklers”, “OST_DuctTerminal”, “OST_DuctFitting”, “OST_PipeFitting”, “OST_Planting”, “OST_Windows”];

SystemFamilies = [“OST_CableTray”, ”OST_RoofSoffit”, “OST_Ceilings”, “OST_DuctCurves”, “OST_Roofs”, “OST_Walls”, “OST_StackedWalls”, ”OST_CurtainWallPanels”, “OST_Floors”, ”OST_PipeCurves”, ”OST_FlexPipeCurves”,” OST_Conduit”];
</pre>

Many thanks to Juan Jesús and Richard for sharing this result.

I cleaned it up and added it
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) for
posterity like this:

<pre class="code">
<span style="color:#2b91af;">BuiltInCategory</span>&nbsp;[]&nbsp;_bics_for_legend_component_with_FamilyInstance&nbsp;
&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>[]&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Casework,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Columns,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Doors,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_DuctAccessory,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_DuctFitting,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_DuctTerminal,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_ElectricalEquipment,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Furniture,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_LightingFixtures,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_MechanicalEquipment,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_PipeAccessory,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_PipeFitting,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Planting,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_PlumbingFixtures,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_SpecialityEquipment,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Sprinklers,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_StructuralColumns,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_StructuralFoundation,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_StructuralFraming,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Windows&nbsp;};
 
<span style="color:#2b91af;">BuiltInCategory</span>[]&nbsp;_bics_for_legend_component_with_SystemFamily&nbsp;
&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>[]&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_CableTray,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Ceilings,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Conduit,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_CurtainWallPanels,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_DuctCurves,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_FlexPipeCurves,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Floors,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_PipeCurves,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_RoofSoffit,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Roofs,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_StackedWalls,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Walls&nbsp;};
</pre>

Slightly [&ast;&ast;&ast;&ast;](https://en.wikipedia.org/wiki/Anal_retentiveness), I know.

**Response:** In the end I have had to use that list with `BuiltInCategory` entries in my code so your &ast;&ast;&ast;&ast; retentiveness has already been proven necessary haha.

**Answer:** Glad to hear it was useful for you!

I am in discussion with friends on the topic of perfection versus
the [Pareto 80-20 principle](https://en.wikipedia.org/wiki/Pareto_principle).

I try to favour both.

I see this as an  effective demonstration that both can be achieved simultaneously.

In simple cases only, of course.

Luckily, all programming issues are simple, always.

Unless someone seriously messed them up.

Unfortunately, the most common case...


####<a name="3"></a> Encoding Issue with Cyrillic Characters Fixed

The [encoding issues when reading data from size lookup tables with Cyrillic characters](https://forums.autodesk.com/t5/revit-api-forum/russian-letters-doesn-t-export-in-lookup-tables/m-p/9056270)
is resolved in the Revit 2021.1 update release.

The development team resolved the issue *REVIT-153006 &ndash; Encoding issue reading data from size lookup table with Cyrillic characters -- 15738224* and report the good news:

We have now fixed and closed this issue in the development release.

It has been addressed in work done for *REVIT-161509*.

That fix is present in Revit 2021.1 and any version of Revit after that.

For previous versions, one option to try is to see if generating the CSV files as UTF-16 (i.e., 2 byte Unicode) will cause them to be read properly, even in older versions of Revit that do not contain the fix.

Please note that our support for loading UTF-8 and UTF-16 requires that the files contain the proper UTF signature BOM at the beginning of the file.
The UTF-8 files provided as samples DO NOT, but you can re-save them as UTF-8 in almost any text editor and force generation of this signature.
Then Revit knows how to properly load and encode them with the fix. Revit is not a full blown text processor, so it relies on the presence of that BOM to properly handle files.
Without it, Revit assume they are 'normal' ANSI encoded files and thus will not process them correctly.

Correction after another, deeper, look at the code prior to this fix, in older versions of Revit:
Unfortunately, upon further investigation, that code assumes the file is a set of single byte characters to load.
Not even a proper ANSI code page encoding is being applied, although the workarounds look like that is possibly going on as part of the internal file read…
Anyway, I do not think using any sort of Unicode encoding on the files in question for previous versions of Revit will work after all.
It does in the current version, though.

<center>
<img src="img/cyrillic_characters.png" alt="Cyrillic characters" title="Cyrillic characters" width="316"/>
</center>

####<a name="4"></a> AEC Customers Demand Better Value

An interesting critical article in AEC Magazine discusses
how [Autodesk AEC customers demand better value](https://www.aecmag.com/comment-mainmenu-36/2046-autodesk-aec-customers-demand-better-value).

####<a name="5"></a> Construction Executive Job

Are you interested and motivated to contribute to reimagining the construction business for the digital age?

Autodesk is active in that area, enabling companies to address the most important challenges they face today while preparing for new ways of working in the future. The Autodesk Construction Cloud portfolio connects the office, trailer and field so customers can move seamlessly through each phase of a building’s lifecycle - from design and preconstruction to construction, turnover and operations - with best-in-class solutions that include Assemble Systems, BIM360, BuildingConnected and PlanGrid. General contractors, subcontractors, and owners around the world rely on ACS to win more work, enhance collaboration, speed decision-making, reduce risk, and improve overall project outcomes. We are looking for top Sales Professionals to join Autodesk Construction Solutions (ACS) team and work with clients in EMEA to uncover pain in their current processes and find value in leveraging the Autodesk Construction Cloud. Our Account Executives (AEs) represent ACS for their assigned geography in a fast paced and exciting industry! With a regional approach to selling into the construction industry, we are looking for someone to be the go-to resource and expert in their territory as we look to build net-new business and expand our existing account base across EMEA. 

If so, would you like to consider applying for a job as Construction Account Executive?

- [20WD39376 &ndash; Construction Account Executive working Home Office in Germany](https://rolp.co/GrWoi)

Responsibilities:

- Manage the sales cycle from prospect to close with a consistent track record of meeting or exceeding quotas
- Show potential customers the value of the Autodesk Construction Cloud portfolio, conducting both in-person and virtual demos (with the help of technical experts when required)
- Understand the competitive landscape and customer needs to effectively position the right Autodesk Construction Cloud products
- Manage and qualify inbound leads and pipeline, with accurate and timely responses
- Work with and mentor Business Development Reps (BDRs) to drive leads in territory
- Own and deliver on a territory plan and forecast
- Be ready to expand your sales career in the exciting & growing Construction Tech space!
 
Minimum qualifications:

- Three or more years of experience in selling SaaS/Cloud-based solutions in the field
- Results-driven and a consistent track record of exceeding quota
- Analytical, detail-driven and a master multitasker
- Experience demoing software and are comfortable selling to all audiences
- Entrepreneurial and thrive in a dynamic, growth-oriented environment
- Construction experience or familiarity is a plus, but not required (We can teach you!)
- Travel is expected, but AEs are trusted to manage their schedules appropriately based on territory and accounts

Good luck applying for that or the many other opportunities that you can find all over the world in
the [Autodesk career site](https://www.autodesk.com/careers)!

