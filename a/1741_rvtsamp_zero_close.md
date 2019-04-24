<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

</head>

<!---
  
twitter:

Close document, zero-doc RvtSamples and TBC samples for Revit 2020 and the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/zerodocsamples

Still getting up to speed with Revit 2020 and my new virtual machine that I set up for it...
First, however, an interesting adaptation of RvtSamples to run in zero document state
&ndash; RvtSamples adapted for zero document state
&ndash; Migrating The Building Coder samples to Revit 2020
&ndash; Installing PowerTools Copy HTML Markup
&ndash; Using <code>PostCommand</code> to close document...

linkedin:

Close document, zero-doc RvtSamples and TBC samples for Revit 2020 and the #RevitAPI #bim #DynamoBim #ForgeDevCon #Revit #API #SDK

http://bit.ly/zerodocsamples

Still getting up to speed with Revit 2020 and my new virtual machine that I set up for it...

First, however, an interesting adaptation of RvtSamples to run in zero document state:

- RvtSamples adapted for zero document state
- Migrating The Building Coder samples to Revit 2020
- Installing PowerTools Copy HTML Markup
- Using <code>PostCommand</code> to close document...


-->

### Close Doc and Zero Doc RvtSamples 

Still getting up to speed with Revit 2020 and my new virtual machine that I set up for it...

First, however, an interesting adaptation of RvtSamples to run in zero document state:

- [RvtSamples adapted for zero document state](#2) 
- [Migrating The Building Coder samples to Revit 2020](#3) 
- [Installing PowerTools Copy HTML Markup](#4) 
- [Using `PostCommand` to close document](#5) 


####<a name="2"></a> RvtSamples adapted for Zero Document State

Before the release of Revit 2020, I had an interesting discussion with my French namesake Jérémie on running RvtSamples in zero document state.

As you may know, RvtSamples is simply an external application that reads a text file containing information on (a large number of) external commands and their assembly DLL locations to populate a menu making them all accessible in the Revit user interface without a need to install a separate add-in manifest for each one of them.

It is used to easily provide debugging and testing access to all the external commands provided by the Revit SDK samples.

In his [comment](https://thebuildingcoder.typepad.com/blog/2018/04/rvtsamples-2019.html#comment-4399937612)
on [RvtSamples for Revit 2019](https://thebuildingcoder.typepad.com/blog/2018/04/rvtsamples-2019.html#comment-4399937612),
Jérémie asks:

**Question:** After several tries on Visual Studio 2017 I finally managed to load RvtSamples to Revit 2019! Thank you for this update!

However, I have a question about this tool.

This application makes it possible to use all the examples of the SDK and also to add others.
The concern is that the manifest seems created by code from the `RvtSamples.txt` file and I do not find how to modify this code to add `VisibilityMode` `=` `AlwaysVisible`.

Personally, I need some of my addins (ExternalCommand) to be `VisibilityMode` `=` `AlwaysVisible` to allow their use from the opening of Revit.

Is it possible to do this simply?

**Answer:** Yes, sure you can simply set `VisibilityMode` `=` `AlwaysVisible` in the add-in manifest for any add-in you like.

Look at the description
of [Add-in Registration settings in the Developer Guide](http://help.autodesk.com/view/RVT/2019/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Add_in_Registration_html).

The RvtSamples.txt text input file is not created by the program, but hand edited.

I implemented it originally in 2008 to [load the Revit SDK samples](https://thebuildingcoder.typepad.com/blog/2008/09/loading-sdk-sam.html).

At that time, it only held four lines of text per external command to load.

That number was later expanded to seven, adding support for the tooltip text, small and large icon image.

The visibility mode could be added to the list of data to be read from that file, but is not currently so.

You could add that support yourself if you like, by modifying the RvtSamples application yourself.

It is probably simpler to implement the specific external commands that you wish to equip with this setting in a separate add-in with their own add-in manifest.

**Response:** Yes, I have already split the `RvtSamples.txt` file to match what I want and it works without any worries.

Yes, that's exactly what I wanted to do: modify the code to add an 8th line to add this option (or more simply change the default to AlwaysVisible).

Unfortunately, I have not yet managed to understand the code enough to do that. I will continue to search in this case.

**Answer:** The code is simple. Edit the RvtSamples module `Application.cs`.

`AddSample` reads seven lines of text from RvtSamples.txt.

Modify it to read eight lines instead.

Or, as you suggest, set the default to `AlwaysVisible`. Oh no, this all makes no sense at all. AlwaysVisible applies to a standard external command loaded by Revit into the external tools menu. We are not loading anything at all into the external tools menu, but creating our own menu instead. Is the RvtSamples panel visible and active in zero document state?

**Response:** The RvtSamples panel is visible but disabled in zero document state.

But now it works. I ended up finding what was blocking me in the article
on [enabling ribbon items in zero document state](http://thebuildingcoder.typepad.com/blog/2011/02/enable-ribbon-items-in-zero-document-state.html).

To implement this in RvtSamples, it's ultimately relatively simple.

- Implement *public class Availability: IExternalCommandAvailability* (where `IsCommandAvailable` returns true) in every external DLL you want active in zero document state.
- In the RvtSamples Application, set the `AvailabiltyClassName` property of the PushButton object to *namespaceOfTheExternalDll.Availability*.

Thank you for your help.

Here is a [Dropbox link to a `7z` archive containing the project with this functionality](https://www.dropbox.com/s/j0dk8ifvw83l0n6/RvtSamplesMod.7z?dl=0).

Many thanks to Jérémie for this initiative, discussion, research and project modification!

For posterity, I copied his `7z` archive to The Building Coder repository, also converted to a corresponding `zip` version:

- [RvtSamplesEnabledInZeroDocState.7z](zip/RvtSamplesEnabledInZeroDocState.7z) (23816 bytes)
- [RvtSamplesEnabledInZeroDocState.zip](zip/RvtSamplesEnabledInZeroDocState.zip) (45212 bytes)


####<a name="3"></a> Migrating The Building Coder Samples to Revit 2020

I migrated [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) to Revit 2020.

After updating the Revit API assembly DLLs to the new version,
[one error occurred](zip/tbc_samples_errors_warnings_1.txt):

- Error CS1061 in CmdGetMaterials.cs line 736: `Application` does not contain a definition for `get_Assets` and no extension method `get_Assets` accepting a first argument of type `Application` could be found.

The removal of this property is indeed listed
in [What's New in the Revit 2020 API](https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html)
under [obsolete API removal](https://thebuildingcoder.typepad.com/blog/2019/04/whats-new-in-the-revit-2020-api.html#4.1.6).

It was referenced in the method `ListAllAssets`, whose only purpose was to test this property.
I guess it has no particular use.
Maybe it never did?
Anyway, since the method is never called, I simply commented it out.

The current version is [release 2020.0.145.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2020.0.145.0).

####<a name="4"></a> Installing PowerTools Copy HTML Markup

I prefer to colourise the C# source code I publish here on the blog, cf. below.

This can be achieved by installing an additional tool in Visual Studio.

Luckily I researched this topic three years ago
migrating [RvtSamples for Revit 2017](http://thebuildingcoder.typepad.com/blog/2016/04/rvtsamples-for-revit-2017.html) and
discovered
the [Productivity Power Tools 2015](https://visualstudiogallery.msdn.microsoft.com/34ebc6a2-2777-421d-8914-e29c1dfa7f5d).

I installed those and they still work fine:

<center>
<img src="img/visual_studio_2015_copy_html_markup_2.png" alt="Copy Html Markup menu entry" width="479">
</center>


####<a name="5"></a> Using PostCommand to Close Document

A long, long time ago, we discussed [closing the active document and why not to](https://thebuildingcoder.typepad.com/blog/2010/10/closing-the-active-document-and-why-not-to.html) do
so using a Windows message workaround, later followed by another exploration on how
to [close the last document](https://thebuildingcoder.typepad.com/blog/2012/12/closing-the-active-document.html) by
first opening a dummy document so that the other 'real' document can be closed.

Bogdan just provided a new effective suggestion for closing the active document using `PostCommand` in
his [comment on the former post](https://thebuildingcoder.typepad.com/blog/2010/10/closing-the-active-document-and-why-not-to.html#comment-4435756188):

> Maybe someone has already found this solution to close the active document in Revit; I found the following method:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">RevitCommandId</span>&nbsp;closeDoc
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">RevitCommandId</span>.LookupPostableCommandId(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PostableCommand</span>.Close&nbsp;);
 
&nbsp;&nbsp;uiapp.PostCommand(&nbsp;closeDoc&nbsp;);
</pre>

(Voila, my first use of the new Copy HTML Markup menu item &nbsp; :-)

I added the new method
to [The Building Coder samples release 2020.0.145.1](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2020.0.145.1).
