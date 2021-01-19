<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- AEC focused Forge Virtual Accelerator "Waldspirale" – February 22-26
  https://forge.autodesk.com/blog/forge-virtual-accelerator-waldspirale-february-22-26
  2021-02_forge_accelerator_waldspirale.jpg 800
  https://en.wikipedia.org/wiki/Waldspirale

- I18N
  How to convert a Revit plugin from english to other languages?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-convert-a-revit-plugin-from-english-to-other-languages/m-p/10008918#M52592
  [Q] I have been developing revit plugins for past 1-2 years in English language. I intend to convert my plugin into other languages so that I can make it useful for people all around the world. I would appreciate if anybody could help me with the procedure for doing so.
  [A] You can use Resources with string table: [How to use localization in C#](https://stackoverflow.com/questions/1142802/how-to-use-localization-in-c-sharp)
  [Lukáš Kohout](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/767846), Architect and Revit Developer, CZ

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk 

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### AEC Focused Forge Accelerator Next Week

####<a name="2"></a> Join the AEC Forge Accelerator Waldspirale

The next online Forge accelerator is coming up next week, February 22-26.

This one is AEC focused.

It is named "Waldspirale" in honour of
the Austrian artist, environmentalist and architect [Friedensreich Hundertwasser](https://en.wikipedia.org/wiki/Friedensreich_Hundertwasser) and
his [105-apartment building of the same name](https://en.wikipedia.org/wiki/Waldspirale) in Darmstadt, Germany.

We are hence looking for proposals that aim to solve problems in design, construction and building life cycle management.
We are looking forward to receiving proposals with creative ideas and to helping you to jump start your development with Forge during the week-long event!     

In addition to the APIs that are already publicly available, we are also expecting to have some new sets of BIM 360 APIs to play with by then <sup>[(*1)](#3.1)</sup>:

- Assets API 
- Data Connector API

If you are interested in using these new APIs, this event will be a great opportunity to learn and develop while "sitting" with development teams.

If you would like to know what will be possible with Assets feature and its API, please check out the recordings of the recent Autodesk University presentations:

- [Tracking Assets and Equipment in BIM 360](https://www.autodesk.com/autodesk-university/class/Tracking-Assets-and-Equipment-BIM-360-2020)
- [BIM 360 API Update and Beyond](https://www.autodesk.com/autodesk-university/class/BIM-360-API-Update-and-Beyond-2020)
&ndash; the Assets API overview starts at 16:15

In addition, the [Data Connector](https://help.autodesk.com/view/BIM360D/ENU/?guid=BIM360D_Insight_data_extractor_html) feature enables you to extract data at an account level with Executive privilege. This API will allow you to schedule extraction jobs and access extracted data.   

In case of interest, here are 
other [full articles and video recordings of the Forge classes at AU classes](https://thebuildingcoder.typepad.com/blog/2021/01/forge-at-au-and-open-source-property-access.html#3).

Please submit your proposal
to [forge.autodesk.com/accelerator-program](https://forge.autodesk.com/accelerator-program).

If you would like to find out more about the new APIs in order to detail your proposal, feel free to indicate so in your proposal.
We will get back to you and discuss further in your specific context. 

<a name="2.1"></a>
(*1) Disclaimer: until we actually see the API officially released, the public release date might change. 

<center>
<img src="img/2021-02_forge_accelerator_waldspirale.jpg" alt="AEC focused Forge accelerator Waldspirale" title="AEC focused Forge accelerator Waldspirale" width="400"/> <!-- 800 -->
</center>


####<a name="3"></a> Internationalisation using .NET Language Resources

Let's quickly address a desktop Revit API issue as well before closing for today.

[Lukáš Kohout](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/767846), Architect and Revit Developer, CZ, very kindly solved 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
about internationalisation or I18N
on [how to convert a Revit plugin from English to other languages](https://forums.autodesk.com/t5/revit-api-forum/how-to-convert-a-revit-plugin-from-english-to-other-languages/m-p/10008918):

**Question:** I have been developing Revit plugins for the past 1-2 years in English language.

Now, I intend to convert my plugin into other languages so that I can make it useful for people all around the world.

I would appreciate if anybody could help me with the procedure for doing so.

**Answer:** You can use Resources with string table as described in the StockOverflow discussion
on [how to use localization in C#](https://stackoverflow.com/questions/1142802/how-to-use-localization-in-c-sharp).

Many thanks to Lukáš for the very helpful pointer.

