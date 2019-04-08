<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- slow, even slower, fast filter to determine
  simplified by ParameterFilterRuleFactory
  https://forums.autodesk.com/t5/revit-api-forum/is-it-show-filter/m-p/8712175

- python
  Get date and time when photo was taken from EXIF data using PIL
  https://stackoverflow.com/questions/23064549/get-date-and-time-when-photo-was-taken-from-exif-data-using-pil
  https://github.com/ianare/exif-py
  /j/sh/ren2timestamp.py

- population growth
  [Hans Rosling: Global population growth, box by box](https://www.youtube.com/watch?v=fTznEIZRkLg)
  [TED talk](http://www.ted.com)
  The world's population will grow to 9 billion over the next 50 years &ndash; and only by raising the living standards of the poorest can we check population growth.
  This is the paradoxical answer that Hans Rosling unveils at TED@Cannes using colorful new data display technology (you'll see).

- [A quantum experiment suggests there’s no such thing as objective reality](https://www.technologyreview.com/s/613092/a-quantum-experiment-suggests-theres-no-such-thing-as-objective-reality)

- [Can AI be a fair judge in court? Estonia thinks so](https://www.wired.com/story/can-ai-be-fair-judge-court-estonia-thinks-so/)

twitter:

Slow, slower still and faster filtering in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/fasterfilter

Today I discuss (once again) an important performance aspect of Revit element filtering, a Python script for tagging JPEG images with EXIF data, prompted by a recent ski tour, and three other interesting topics that caught my eye
&ndash; Slow, slower still and faster filtering
&ndash; Python JPEG EXIT filename tagging
&ndash; TED talks and population growth
&ndash; Objective reality does not exist
&ndash; Artificial intelligence judge...

linkedin:

Slow, slower still and faster filtering in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/fasterfilter

Today I discuss (once again) an important performance aspect of Revit element filtering, a Python script for tagging JPEG images with EXIF data, prompted by a recent ski tour, and three other interesting topics that caught my eye:

- Slow, slower still and faster filtering
- Python JPEG EXIT filename tagging
- TED talks and population growth
- Objective reality does not exist
- Artificial intelligence judge...

-->

### Slow, Slower Still and Faster Filtering

Today I discuss (once again) an important performance aspect of Revit element filtering, a Python script for tagging JPEG images with EXIF data, prompted by a recent ski tour, and three other interesting topics that caught my eye:

- [Slow, slower still and faster filtering](#2) 
- [Python JPEG EXIT filename tagging](#3) 
- [TED talks and population growth](#4) 
- [Objective reality does not exist](#5) 
- [Artificial intelligence judge](#6) 

#### <a name="2"></a> Slow, Slower Still and Faster Filtering

A question was raised in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
asking, [is it slow filter](https://forums.autodesk.com/t5/revit-api-forum/is-it-show-filter/m-p/8712175)?

Meseems we need to point out yet again the difference between slow and fast filters.

Much more importantly, though, the difference between using Revit filters or post-processing results using .NET and or LINQ.

The latter will always be at least twice as slow as using Revit filters, regardless of whether they are fast or slow.

The process of marshalling the Revit element data out of the Revit memory space into the .NET environment always costs much more time than retrieving the data inside of Revit in the first place.

Therefore, if you care about performance, use as many Revit filters as possible, regardless whether they are fast or slow.

In the following example, .NET post-processing is used to test a parameter value.

That can be speeded up by an order of magnitude by implementing a Revit parameter filter instead, which avoids the marshalling, post-processing, and is fast to boot:

**Question:** I have used the following code:

<pre class="code">
  string section_name = "XXXXXXX";

  IEnumerable<Element> elems = collect
    .OfClass(typeof(FamilyInstance))
    .Where(x => x.get_Parameter(
      BuiltInParameter.ELEM_FAMILY_PARAM)
        .AsValueString() == section_name );
</pre>

Is it a slow filter?

**Answer:** No, it is not. It is even slower than a slow filter.

Your code retrieves the element data from Revit to the .NET add-in memory space, and then uses .NET and LINQ to post-process it.

The difference is explained in the discussion
of [quick, slow and LINQ element filtering](http://thebuildingcoder.typepad.com/blog/2015/12/quick-slow-and-linq-element-filtering.html).

You could convert it to a fast filter by implementing
a [parameter filter to compare the family name](https://thebuildingcoder.typepad.com/blog/2018/06/forge-tutorials-and-filtering-for-a-parameter-value.html#3).

That discussion does not show how to actually implement the parameter filter.

I therefore cleaned up The Building Coder sample code
demonstrating [retrieving named family symbols using either LINQ or a parameter filter](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCollectorPerformance.cs#L1203-L1255) for
you to illustrate how to do that:

<pre class="code">
  <span style="color:blue;">#region</span>&nbsp;Retrieve&nbsp;named&nbsp;family&nbsp;symbols&nbsp;using&nbsp;either&nbsp;LINQ&nbsp;or&nbsp;a&nbsp;parameter&nbsp;filter
  <span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>
  &nbsp;&nbsp;GetStructuralColumnSymbolCollector(
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_StructuralColumns&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilySymbol</span>&nbsp;)&nbsp;);
  }
   
  <span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;Linq(&nbsp;
  &nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;
  &nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;familySymbolName&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;GetStructuralColumnSymbolCollector(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x&nbsp;=&gt;&nbsp;x.Name&nbsp;==&nbsp;familySymbolName&nbsp;);
  }
   
  <span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;Linq2(&nbsp;
  &nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;
  &nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;familySymbolName&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;GetStructuralColumnSymbolCollector(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;x&nbsp;=&gt;&nbsp;x.get_Parameter(&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.SYMBOL_NAME_PARAM&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.AsString()&nbsp;==&nbsp;familySymbolName&nbsp;);
  }
   
  <span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;FilterRule(&nbsp;
  &nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;
  &nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;familySymbolName&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;GetStructuralColumnSymbolCollector(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterStringRule</span>(
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ParameterValueProvider</span>(
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.SYMBOL_NAME_PARAM&nbsp;)&nbsp;),
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterStringEquals</span>(),&nbsp;familySymbolName,&nbsp;<span style="color:blue;">true</span>&nbsp;)&nbsp;)&nbsp;);
  }
   
  <span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">Element</span>&gt;&nbsp;Factory(&nbsp;
  &nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;
  &nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;familySymbolName&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;GetStructuralColumnSymbolCollector(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ParameterFilterRuleFactory</span>.CreateEqualsRule(
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.SYMBOL_NAME_PARAM&nbsp;),
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;familySymbolName,&nbsp;<span style="color:blue;">true</span>&nbsp;)&nbsp;)&nbsp;);
  }
  <span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;named&nbsp;family&nbsp;symbols&nbsp;using&nbsp;either&nbsp;LINQ&nbsp;or&nbsp;a&nbsp;parameter&nbsp;filter</span>
</pre>

It demonstrates two ways to implement the highly efficient parameter filter.

First, the explicit solution, implementing the separate `ElementParameterFilter`, `FilterStringRule`, and `ParameterValueProvider` components one by one.

Second, a much more succinct solution using the `ParameterFilterRuleFactory` `CreateEqualsRule` method.

Here are some other examples of using the `ParameterFilterRuleFactory`:

- [Isolating elements of a given system](https://thebuildingcoder.typepad.com/blog/2015/01/isolating-elements-of-a-given-system.html)
- [Retrieving elements visible in view](https://thebuildingcoder.typepad.com/blog/2017/05/retrieving-elements-visible-in-view.html)
- [A new way to retrieve a parameter id](https://thebuildingcoder.typepad.com/blog/2018/08/revit-20191-cefsharp-forge-accelerator-in-rome.html#7)

I hope this clarifies.

Now for some non-Revit-related topics:


#### <a name="3"></a> Python JPEG EXIT Filename Tagging

I went on a ski tour in the Silvretta area with some friends two weeks ago, climbing Haagspitze, Hintere Jamspitze, Ochsenkopf, Sattelkopf and above all Piz Buin.

Here I am crossing the Piz Buin summit ridge:

<center>
<img src="img/20190323_121516_jk_8144_jeremy_on_ridge_1024x768.jpg" alt="Jeremy on the Piz Buin summit ridge" width="512">
</center>

That excursion prompted me to implement a Python script to tag the photos shared by the participants after the tour.

How to easily sort all the different pictures chronologically?

Well, most cameras embed a timestamp in EXIF data when they generate a JPEG image.

This information can be easily accessed and used to rename each image file, e.g., prepending the timestamp to the filename.

In case of multiple photographers, it also makes sense to add the photographer initials to the filename.

I implemented a really minute little Python script `ren2timestamp.py` that does this for me, using a hint from the StackOverflow discussion
on [getting date and time when photo was taken from EXIF data using PIL](  https://stackoverflow.com/questions/23064549/get-date-and-time-when-photo-was-taken-from-exif-data-using-pil) and
the [`exif-py` easy-to-use Python module to extract Exif metadata from tiff and jpeg files](https://github.com/ianare/exif-py):

<pre class="prettyprint">
#!/usr/bin/env python
# ren2timestamp.py - rename files to add their exif timestamp as a prefix
# Copyright (C) 2018 by Jeremy Tammik, Autodesk Inc.

import exifread, datetime, os, sys, time

files = sys.argv[1:]

exif_ts_key = 'EXIF DateTimeOriginal'

def exif_timestamp(filename):
  with open(filename, 'rb') as fh:
    tags = exifread.process_file(fh, stop_tag=exif_ts_key)
    if tags.has_key(exif_ts_key):
      return tags[exif_ts_key]
    else:
      return ''

photographer_initials = 'jt'

for g in files:
  ts3 = exif_timestamp(g)
  ts3 = str(ts3).replace(':','').replace(' ','_')
  newname = ts3 + '_' + photographer_initials + '_' + g
  print("%s --> '%s'" % (g,newname))
  os.rename(g, newname)
</pre>

Here is the closing picture of the entire group on the Sattelkopf summit:

<center>
<img src="img/20190324_123346_wk_1091_gruppenfoto_sattelkopf_912x606.jpg" alt="Group photo on Sattelkopf" width="456">
</center>

#### <a name="4"></a> TED Talks and Population Growth

[TED](http://www.ted.com) is a non-profit organisation devoted to spreading ideas, usually in the form of short, powerful talks (18 minutes or less).
TED is also a global community, welcoming people from every discipline and culture who seek a deeper understanding of the world.
TED believes passionately in the power of ideas to change attitudes, lives and, ultimately, the world.
TED.com is a clearinghouse of free knowledge from the world's most inspired thinkers.

I have listened to many TED talks, enjoyed and appreciated them very much, and highlighted several of them here in the past.

Here is an illuminating one with a paradoxical answer by [Hans Rosling on Global population growth, box by box](https://youtu.be/fTznEIZRkLg):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/fTznEIZRkLg" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

The world's population will grow to 9 billion over the next 50 years &ndash; and only by raising the living standards of the poorest can we check population growth.

#### <a name="5"></a> Objective Reality Does Not Exist

[A quantum experiment suggests there’s no such thing as objective reality](https://www.technologyreview.com/s/613092/a-quantum-experiment-suggests-theres-no-such-thing-as-objective-reality).

#### <a name="6"></a> Artificial Intelligence Judge

[Can AI be a fair judge in court? Estonia thinks so](https://www.wired.com/story/can-ai-be-fair-judge-court-estonia-thinks-so).

