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


#RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

A discussion with Håvard Dagsvik on the use of <code>TransmissionData</code>, standalone access to the <code>BasicFileInfo</code> without the need for a valid Revit API context, and a Revit-independent method replacing
<code>Family</code> <code>ExtractPartAtom</code>
&ndash; No document required for <code>TransmissionData</code> access
&ndash; <code>TransmissionData</code> requires a valid Revit API context
&ndash; Standalone <code>GetFamilyXmlData</code> method replacing <code>ExtractPartAtom</code>
&ndash; Windows explorer <code>BasicFileInfo</code> right click utility...

--->

### Standalone BasicFileInfo and ExtractPartAtom

I had an interesting discussion with
Håvard Dagsvik of [Symetri](https://www.symetri.com) on
the use of `TransmissionData` and standalone access to the `BasicFileInfo`, without the need for a valid Revit API context, in the course of which Håvard shared his Revit-independent `GetFamilyXMLData` method implementation, replacing
the Revit API [`Family` `ExtractPartAtom` method](http://www.revitapidocs.com/2018.1/d477cf8f-0dfe-4055-a787-315c84ef5530.htm):

- [No document required for `TransmissionData` access](#2) 
- [`TransmissionData` requires a valid Revit API context](#3) 
- [Standalone `GetFamilyXmlData` method replacing `ExtractPartAtom`](#4) 
- [Windows explorer `BasicFileInfo` right click utility](#5) 

####<a name="2"></a>No Document Required for TransmissionData Access

**Question:** I fear I know the answer to this one already, but I will try to ask you anyway &nbsp; :-)
 
When reading `TransmissionData` or `BasicFileInfo`, you don't need a document:

<center>
<img src="img/hd_link_manager.jpg" alt="Link Manager" width="292"/>
</center>
 
From the `TransmissionData`, I can access the `ExternalFileReference` via `GetLastSavedReferenceData`, etc.

This works fine:
 
<center>
<img src="img/hd_external_file_reference.png" alt="ExternalFileReference" width="500"/>
</center>
 
But, if I implement the same functionality in a standalone executable and use my own TransmissionData function...

<center>
<img src="img/hd_transmissiondata.png" alt="Standalone TransmissionData" width="430"/>
</center>
 
It throws this exception on the first method that uses the Revit API:

<center>
<img src="img/hd_requires_revit_api.png" alt="Revit API required" width="361"/>
</center>
 
No Revit process involved here, of course.

It's most probably as you have written before:
 
> In general, as noted in both there and in the summary above, it is not possible to access Revit data or make any use whatsoever of the Revit API from outside of Revit.

> Moreover, you need to be in a valid Revit API context to make any Revit API calls.

Maybe these methods still need the Revit `Application` or `UIApplication` in some way &ndash; or they are just locked for some other reason.
 
Or, should this work and it is just as the message says:

> ... one of the dependencies to RevitAPI.dll is not found?

I'm 99% sure it finds RevitAPI.dll.
 
After all, this seems like metadata that logically could be available without Revit.


####<a name="3"></a>TransmissionData Requires a Valid Revit API Context

**Answer:** Yes indeed, you do know the answer: use of the Revit API `TransmissionData` object requires a valid Revit API context.
 
In other words, you can read data from an unopened Revit document, but you can only do so from within a valid Revit session.
 
However, there are many ways of getting at the basic file info without use of the Revit API, e.g.,
[determining RVT file version using Python](http://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html), or
one of the other approaches discussed there.


####<a name="4"></a>Standalone GetFamilyXmlData Method Replacing ExtractPartAtom

**Response:** That's great; I will definitely try to make use of Victor's raw data approach on `BasicFileInfo`.
 
Is it thinkable that `TransmissionData` and `BasicFileInfo` doesn't really need Revit?

That they just happen to be packaged within a larger library that overall can`t be used without Revit?

Maybe it is a just licensing policy decision, even though users always have a license.

Meaning, if those methods where refactored out, it could have worked, technically?
 
If so, let me be the first one who makes the request to get them refactored &nbsp; :-)
 
For example, take the `Family.ExtractPartAtom` method.

It exports an XML file with all family types, including parameters and values for each type.

But we don't use it.

First, because I (luckily) didn't realize that the Revit API provides this method.

Secondly, because my own custom method is much faster than `ExtractPartAtom`.

More importantly, it can be used without Revit, and it doesn't save to an external XML file that we don't need anyway.

It just returns the raw XML string that is parsed to JSON before it enters a database.

Method here:
 
<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Faster&nbsp;ExtractPartAtom&nbsp;reimplementation,</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;independent&nbsp;of&nbsp;Revit&nbsp;API,&nbsp;for&nbsp;standalone&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;external&nbsp;use.&nbsp;By&nbsp;Håvard&nbsp;Dagsvik,&nbsp;Symetri.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>family_file_path<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">Family&nbsp;file&nbsp;path</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span><span style="color:green;">XML&nbsp;data</span><span style="color:gray;">&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;GetFamilyXmlData(
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;family_file_path&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">byte</span>[]&nbsp;array&nbsp;=&nbsp;<span style="color:#2b91af;">File</span>.ReadAllBytes(&nbsp;family_file_path&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;string_file&nbsp;=&nbsp;<span style="color:#2b91af;">Encoding</span>.UTF8.GetString(&nbsp;array&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;xml_data&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;start&nbsp;=&nbsp;string_file.IndexOf(&nbsp;<span style="color:#a31515;">&quot;&lt;entry&quot;</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;start&nbsp;==&nbsp;-1&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;XML&nbsp;start&nbsp;not&nbsp;detected:&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;family_file_path&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;end&nbsp;=&nbsp;string_file.IndexOf(&nbsp;<span style="color:#a31515;">&quot;/entry&gt;&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;end&nbsp;==&nbsp;-1&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;XML&nbsp;end&nbsp;not&nbsp;detected:&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;family_file_path&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;end&nbsp;=&nbsp;end&nbsp;+&nbsp;7;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;length&nbsp;=&nbsp;end&nbsp;-&nbsp;start;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;length&nbsp;&lt;=&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;<span style="color:#a31515;">&quot;XML&nbsp;length&nbsp;is&nbsp;0&nbsp;or&nbsp;less:&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;family_file_path&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xml_data&nbsp;=&nbsp;string_file.Substring(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;start,&nbsp;length&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;xml_data;
}
</pre>

I added Håvard's method to the existing external
command [CmdPartAtom](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdPartAtom.cs)
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) to try this out, in
[release 2018.0.138.4](httpshttps://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.138.4), and cleaned up the existing code calling the built-in Revit API method `ExtractPartAtomFromFamilyFile` at the same time.


####<a name="5"></a>Windows Explorer BasicFileInfo Right Click Utility

Håvard also used Victor's code as a base to implement his
own [Windows Explorer `BasicFileInfo` right click utility](https://www.screencast.com/t/o78ugUUwY), cf.
the [20-second recording of his RVT BasicFileInfo in Windows Explorer](https://youtu.be/WJCsNywPMVU)
[^](zip/ExplorerBasicFileInfo.mp4):

<!---
<center>
 <object id="scPlayer"  width="480" height="389" type="application/x-shockwave-flash" data="https://content.screencast.com/users/HD12310/folders/Jing/media/0d7dc04f-2085-44ab-8b4b-e98d53f91f2d/jingswfplayer.swf" >
 <param name="movie" value="https://content.screencast.com/users/HD12310/folders/Jing/media/0d7dc04f-2085-44ab-8b4b-e98d53f91f2d/jingswfplayer.swf" />
 <param name="quality" value="high" />
 <param name="bgcolor" value="#FFFFFF" />
 <param name="flashVars" value="thumb=https://content.screencast.com/users/HD12310/folders/Jing/media/0d7dc04f-2085-44ab-8b4b-e98d53f91f2d/FirstFrame.jpg&containerwidth=1161&containerheight=941&content=https://content.screencast.com/users/HD12310/folders/Jing/media/0d7dc04f-2085-44ab-8b4b-e98d53f91f2d/2018-04-04_2315.swf&blurover=false" />
 <param name="allowFullScreen" value="true" />
 <param name="scale" value="showall" />
 <param name="allowScriptAccess" value="always" />
 <param name="base" value="https://content.screencast.com/users/HD12310/folders/Jing/media/0d7dc04f-2085-44ab-8b4b-e98d53f91f2d/" />
 Unable to display content. Adobe Flash is required.
</object>
</center>
--->

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/WJCsNywPMVU?rel=0" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center>

Works nicely &nbsp; :-)

Not on all Revit files, though.

Workshared files seem to not work, and a few others.

The data is probably there, just have to tweak it a bit more.
 
Many thanks to Håvard for raising this question and sharing his standalone implementation!

