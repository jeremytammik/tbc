<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<style>
table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
}
th, td {
  padding-left: 1em;
  padding-right: 1em;
}
th{
  text-align:left;
}
</style>
</head>

<!---

- 10168713 [Revit Schedule - Title/headers]
  [Revit schedule title/headers](https://forums.autodesk.com/t5/revit-api-forum/revit-schedule-title-headers/m-p/11573145).

- How to make an Interior Designer Happy (with Electron, IFC.js and Revit API)
  https://www.linkedin.com/pulse/how-make-interior-designer-happy-electron-ifcjs-revit-capasso
  speed up the process of comparing and selecting families for our interior design projects.
  numerous categories of families with hundreds of families in each

twitter:

Maybe need to scale up the handling of element id integer values, a #DynamoBim book, a web-based family showroom browser and retrieving schedule headers with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #ForgeDevCon https://autode.sk/64bitelementid

We may need to scale up the handling of element id integer values in future, a sample snippet to retrieve schedule headers, a Dynamo book, and a web-based family showroom browser
&ndash; 64-bit element ids
&ndash; Revit schedule title headers
&ndash; Beyond Dynamo: Python manual for Revit
&ndash; Web-based family management showroom
&ndash; Tree view in pure CSS
&ndash; High-documentation, low-meeting work culture...

linkedin:

Maybe need to scale up the handling of element id integer values, a Dynamo book, a web-based family showroom browser and retrieving schedule headers with the #RevitAPI

https://autode.sk/64bitelementid

- 64-bit element ids
- Revit schedule title headers
- Beyond Dynamo: Python manual for Revit
- Web-based family management showroom
- Tree view in pure CSS
- High-documentation, low-meeting work culture...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### 64-Bit Element Ids, Maybe?

We may need to scale up the handling of element id integer values in future, a sample snippet to retrieve schedule headers, a Dynamo book, and a web-based family showroom browser:

- [64-bit element ids](#2)
- [Amendment &ndash; how to handle overflow](#2.1)
- [Revit schedule title headers](#3)
- [Beyond Dynamo: Python manual for Revit](#4)
- [Web-based family management showroom](#5)
- [Tree view in pure CSS](#6)
- [High-documentation, low-meeting work culture](#7)

####<a name="2"></a> 64-Bit Element Ids

Autodesk almost never discusses any upcoming functionality in future products whatsoever, for legal reasons.

Still, just as a heads-up warning, the development team thought it worthwhile to point out that we are thinking about possibly converting the internal representation of Revit element ids from 32 to 64 bit in a future release of Revit.

<!--

> Forward looking statements Disclaimer. This document contains “forward looking statements” as defined or implied in common law and within the meaning of the Corporations Law. All expressions an expectations or belief sas to future events or results are expressed in good faith... However, forward looking statements are subject to risks, uncertainties and other factors, which could cause actual results to differ materially from future results expressed, projected or implied by such forward looking statements...

-->

Here is a writeup of what that would imply and what a developer would need to do about it:

ElementIds will change from storing 32-bit integers to storing 64-bit values (type `long` in C# or `Int64` in .NET).
This will allow for larger and more complicated models.
Most functions which take or return ElementIds will continue to work with no changes.
However, there are a few things to keep in mind:
 
If you are storing ElementIds externally as integers, you will need to update your storage to take 64-bit values.
 
The constructor Autodesk.Revit.DB.ElementId(Int32) has been deprecated and replaced by a new constructor, Autodesk.Revit.DB.ElementId(Int64).

The property Autodesk.Revit.DB.ElementId.IntegerValue has been deprecated.
It returns only the lowest 32-bits of the ElementId value.
Please use the replacement property Autodesk.Revit.DB.ElementId.Value, which will return the entire value.
 
Support has been added for using 64-bit types in ExtensibleStorage.
Both Autodesk.Revit.DB.ExtensibleStorage.SchemaBuilder.AddSimpleField() and AddMapField() can now take in 64-bit values for the fieldType, keyType, and valueType parameters.
 
Two binary breaking changes have been made.
Both Autodesk.Revit.DB.BuiltInCategory and Autodesk.Revit.DB.BuiltInParameter have been updated so that the size type of the underlying enums are 64-bits instead of 32.
Code built against earlier versions of the API may experience type cast and other type related exceptions when run against the next versions of the API when working with the enum.
Please rebuild your addins against the next release's API when it is available.
 
Here is an overview mapping the deprecated to the replacement members:

- Autodesk.Revit.DB.ElementId(Int32) <br/> &rarr; Autodesk.Revit.DB.ElementId(Int64)
- Autodesk.Revit.DB.ElementId.IntegerValue <br/> &rarr; Autodesk.Revit.DB.ElementId.Value

<!--

Here is a table showing the deprecated and replacement members:

<center>
<table>
<tr><th>Deprecated API</th><th>Replacement</th></tr>
<tr><td>Autodesk.Revit.DB.ElementId(Int32)</td><td>Autodesk.Revit.DB.ElementId(Int64)</td></tr>
<tr><td>Autodesk.Revit.DB.ElementId.IntegerValue</td><td>Autodesk.Revit.DB.ElementId.Value</td></tr>
</table>
</center>

-->

####<a name="2.1"></a> Amendment &ndash; How to Handle Overflow

We have an amendment to add to the original post.
One of the things we originally said above has actually changed, and I think it also helps
address [cadferret’s question below](https://thebuildingcoder.typepad.com/blog/2022/11/64-bit-element-ids-maybe.html#comment-6054377627):
 
With the caveat that anything we say here might change, we want to amend the initially published proposal.
 
The previous version stated that `ElementId.IntegerValue` would
handle [integer overflow](https://en.wikipedia.org/wiki/Integer_overflow) by
truncating 64-bit values down to 32 bits.
For values which will fit in 32 bits, we will return the value as an integer.
However, if the value would actually need more than 32 bits to represent it, we will throw an exception.
 
To add a bit more about our intentions, as things currently stand:
 
Our intention is to find an optimal balance between 64-bit readiness and minimising disruption for API developers.
We would NOT remap existing ElementIds to higher values.
An `Element` with an `Id` of 50 would still have an `Id` of 50, and either property would return the correct value.
 
Most models will not get so large as to exhaust the 32-bit id space.
So, in general, `ElementId.IntegerValue` would still work.
This would give developers a chance to update their applications, rather than having the function immediately disappear.
 
However, if a model were so large as to have Ids that needed more than 32 bits to store the value, the `ElementId.IntegerValue` property would throw an exception, return a truncated value, or something similar.
 
This is an attempt to allow models which need 64-bit ids sooner, simultaneously minimising outright breaking changes in the API.

####<a name="3"></a> Revit Schedule Title Headers

A number of developers asked for a snippet of sample code in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit schedule title/headers](https://forums.autodesk.com/t5/revit-api-forum/revit-schedule-title-headers/m-p/11573145).

Hernan  [H.echeva](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3063892) Echevarria
kindly jumped in and shared his implementation:

> I found this post, which was very helpful; thank you for the info.

> I created a small example macro that gets the header text.
I hope this helps:

<pre class="code">
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;This&nbsp;macro&nbsp;gets&nbsp;the&nbsp;header&nbsp;text&nbsp;of&nbsp;the&nbsp;active&nbsp;Schedule&nbsp;View</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;<span style="color:#74531f;">ScheduleHeader</span>()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;UIDocument&nbsp;<span style="color:#1f377f;">uidoc</span>&nbsp;=&nbsp;<span style="color:blue;">this</span>.ActiveUIDocument;
&nbsp;&nbsp;&nbsp;&nbsp;Document&nbsp;<span style="color:#1f377f;">doc</span>&nbsp;=&nbsp;uidoc.Document;
 
&nbsp;&nbsp;&nbsp;&nbsp;ViewSchedule&nbsp;<span style="color:#1f377f;">mySchedule</span>&nbsp;=&nbsp;uidoc.ActiveView&nbsp;<span style="color:blue;">as</span>&nbsp;ViewSchedule;
 
&nbsp;&nbsp;&nbsp;&nbsp;TableData&nbsp;<span style="color:#1f377f;">myTableData</span>&nbsp;=&nbsp;mySchedule.GetTableData();
 
&nbsp;&nbsp;&nbsp;&nbsp;TableSectionData&nbsp;<span style="color:#1f377f;">myData</span>&nbsp;=&nbsp;myTableData.GetSectionData(SectionType.Header);
 
&nbsp;&nbsp;&nbsp;&nbsp;TaskDialog.Show(<span style="color:#a31515;">&quot;Header&nbsp;Info&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;The&nbsp;Header&nbsp;Text&nbsp;is:&nbsp;\n&quot;</span>&nbsp;+&nbsp;myData.GetCellText(0,&nbsp;0));
&nbsp;&nbsp;}
</pre>

Thank you, Hernan!

####<a name="4"></a> Beyond Dynamo: Python Manual for Revit

I avoid advertising commercial products, but I made an exception
for [Más Allá de Dynamo](https://thebuildingcoder.typepad.com/blog/2020/12/dynamo-book-and-texture-bitmap-uv-coordinates.html#3),
the Spanish-Language Python manual focused on Dynamo and the Revit API.

It is now also available in English
as [Beyond Dynamo: Python manual for Revit](https://www.amazon.com/dp/B0BMSV6YXD),
by Kevin Himmelreich, Alejandro Martín-Herrer and Ignacio Moreu:

> This is a Python handbook specifically created for working on BIM methodology with Autodesk Dynamo and Revit.
It has a practical approach aimed at professionals who have never programmed before.
If you know how to program in Python, it will be equally useful, as it explains deeply most of the classes, methods and properties of the Revit API.

<center>
<img src="img/beyond_dynamo_en.png" alt="Beyond Dynamo" title="Beyond Dynamo" width="252"/>  <!-- 252 × 330 -->
</center>

Note that we also discussed lots of other resources
for [learning Python and Dynamo](https://thebuildingcoder.typepad.com/blog/2021/02/addin-file-learning-python-and-ifcjs.html#3).

####<a name="5"></a> Web-Based Family Management Showroom

Emiliano Capasso, Head of BIM at [Antonio Citterio Patricia Viel](https://www.citterio-viel.com),
shared some interesting advice on managing thousands of families and
on [how to make an interior designer happy with Electron, IFC.js and Revit API](https://www.linkedin.com/pulse/how-make-interior-designer-happy-electron-ifcjs-revit-capasso).

The aim is to speed up the process of comparing and selecting families for large interior design projects.
They require numerous categories of families with hundreds of families in each.
Initial idea: developing a nice web viewer in IFC.js for viewing families instead of buildings.
Now, every interior designer or architect in the office can navigate (way faster than opening the showrooms in Revit) inside all the showrooms using their browser.
That leads to the even better idea...

Thank you, Emiliano, for sharing this great idea and nice write-up.

####<a name="6"></a> Tree View in Pure CSS

A very nice, clean and lean tree view (collapsible list) can be created using only HTML and CSS, without any need for JavaScript.
Check out the demonstration and detailed step-by-step explanation,
[tree views in CSS](https://iamkate.com/code/tree-views).

<!--

<style>
.tree-padding{
  --spacing    : 1.5rem;
  --radius     : 10px;
  padding-left : 1rem;
}

.tree-padding li{
  display      : block;
  position     : relative;
  padding-left : calc(2 * var(--spacing) - var(--radius) - 2px);
}

.tree-padding ul{
  margin-left  : calc(var(--radius) - var(--spacing));
  padding-left : 0;
}

.tree-vertical-lines ul li{
  border-left : 2px solid var(--dark-grey);
}

.tree-vertical-lines ul li:last-child{
  border-color : transparent;
}

.tree-horizontal-lines ul li::before{
  content      : '';
  display      : block;
  position     : absolute;
  top          : calc(var(--spacing) / -2);
  left         : -2px;
  width        : calc(var(--spacing) + 2px);
  height       : calc(var(--spacing) + 1px);
  border       : solid var(--dark-grey);
  border-width : 0 0 2px 2px;
}

.tree-summaries summary{
  display : block;
  cursor  : pointer;
}

.tree-summaries summary::marker,
.tree-summaries summary::-webkit-details-marker{
  display : none;
}

.tree-summaries summary:focus{
  outline : none;
}

.tree-summaries summary:focus-visible{
  outline : 1px dotted #000;
}

.tree-markers li::after,
.tree-markers summary::before{
  content       : '';
  display       : block;
  position      : absolute;
  top           : calc(var(--spacing) / 2 - var(--radius));
  left          : calc(var(--spacing) - var(--radius) - 1px);
  width         : calc(2 * var(--radius));
  height        : calc(2 * var(--radius));
  border-radius : 50%;
  background    : var(--dark-grey);
}

.tree-buttons summary::before{
  content     : '+';
  z-index     : 1;
  background  : var(--accent);
  color       : #fff;
  font-weight : 400;
  line-height : calc(2 * var(--radius) - 2px);
  text-align  : center;
}

.tree-buttons details[open] > summary::before{
  content : '−';
}
</style>

<center>
<ul class="tree-padding tree-vertical-lines tree-horizontal-lines tree-summaries tree-markers tree-buttons">
  <li>
    <details open>
      <summary>Giant planets</summary>
      <ul>
        <li>
          <details>
            <summary>Gas giants</summary>
            <ul>
              <li>Jupiter</li>
              <li>Saturn</li>
            </ul>
          </details>
        </li>
        <li>
          <details>
            <summary>Ice giants</summary>
            <ul>
              <li>Uranus</li>
              <li>Neptune</li>
            </ul>
          </details>
        </li>
      </ul>
    </details>
  </li>
</ul>
</center>

-->

####<a name="7"></a> High-Documentation, Low-Meeting Work Culture

I enjoyed this analysis
of [the perks of a high-documentation, low-meeting work culture](https://www.tremendous.com/blog/the-perks-of-a-high-documentation-low-meeting-work-culture).
It seems highly relevant to our distributed DAS team, Autodesk Developer Advocacy and Support.
It also lines up very well with my personal experience working within our team, and also in my external interactions, both with the diffuse blog- and forum-based Revit API pseudo-community as well as occasionally consulting individually add-in developers with special requirements.
Looking forward to hearing what you think of it.

