<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- loading support DLLs
  Jim Jia suggested using an assembly resolver by reracting to the `AppDomain.CurrentDomain.AssemblyResolve` event
  https://forums.autodesk.com/t5/revit-api-forum/missing-system-componentmodel-annotations-v4-2-0-0/m-p/8255546
  [Revit Add-in Unit Testing](http://thebuildingcoder.typepad.com/blog/2013/07/revit-add-in-unit-testing.html)
  [Security, Framing Cross Section Analyser and REX](http://thebuildingcoder.typepad.com/blog/2013/12/security-framing-cross-section-analyser-and-rex.html)
  [RvtVa3c Assembly Resolver](http://thebuildingcoder.typepad.com/blog/2014/05/rvtva3c-assembly-resolver.html)
  [Framing Cross Section Analyser and REX in Revit 2015](http://thebuildingcoder.typepad.com/blog/2015/03/framing-cross-section-analyser-and-rex-in-revit-2015.html)
  [REX Add-In Development and Migration](http://thebuildingcoder.typepad.com/blog/2015/12/rex-app-development-and-migration.html)
  [Add-In Templates Supporting Edit and Continue](http://thebuildingcoder.typepad.com/blog/2017/02/add-in-templates-supporting-edit-and-continue.html)

- Please help tweeting about Boston Accelerator – deadline for proposals Sept. 15th.
  You may reference https://forums.autodesk.com/t5/bim-360-api-forum/forge-accelerator-boston-ma-october-1-5-2018/td-p/8249266
  Images attached.
  Natalia Polikarpova Re: Please tweet about Boston Accelerator

Support DLL, Zero Doc State and Forge Accelerators #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/zerodoc2019

If you are interested in Forge programming, don't miss the upcoming deadline for proposals for the Boston Forge accelerator!
Looking at the Revit API, I migrated my sample demonstrating how
to enable ribbon items in zero document state to Revit 2019.
Let's look at that, and another recurring topic, on loading add-in support DLLs
&ndash; Migrating the ZeroDocPanel to Revit 2019
&ndash; Loading add-in support DLLs
&ndash; Rome and Boston Forge accelerators...

-->

### Support DLL, Zero Doc State and Forge Accelerators

If you are interested in diving deep into Forge programming, don't miss the upcoming deadline for proposals for the Boston Forge accelerator!

Looking at the Revit API, I implemented a sample demonstrating how
to [enable ribbon items in zero document state](http://thebuildingcoder.typepad.com/blog/2011/02/enable-ribbon-items-in-zero-document-state.html) back
in 2011, for Revit 2011.

How long would it take to port such an add-in to Revit 2019?

Well, this one took ten or twenty minutes, including some debugging, screen snapshots, and documentation.

Admittedly, it is an absolutely trivial example  :-)

Let's look at that, and another recurring topic, on loading add-in support DLLs:

- [Migrating the ZeroDocPanel to Revit 2019](#2) 
- [Loading add-in support DLLs](#3) 
- [Rome and Boston Forge accelerators](#4) 

<center>
<img src="img/zero_doc_button_2019.png" alt="External command in zero document state" width="468">
</center>

#### <a name="2"></a> Migrating the ZeroDocPanel to Revit 2019

John raised an issue in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [`IsCommandAvailable` causing a nullref exception](https://forums.autodesk.com/t5/revit-api-forum/iscommandavailable-causes-nullref-exception/m-p/8258750).

That prompted me to update my original implementation
to [enable ribbon items in zero document state](http://thebuildingcoder.typepad.com/blog/2011/02/enable-ribbon-items-in-zero-document-state.html) in
Revit 2011 and test it in Revit 2019.

I performed the flat migration from Revit 2011 to Revit 2019, i.e., updated the Revit API assembly references and set the .NET framework to version 4.7.

Next, I tried to load and debug the add-in.

Revit displayed an error due to a lack of the `VendorId` tag in the add-in manifest:

> External Tools – No VendorId Node for Add-in

> Failed to initialize the add-in “ZeroDocPanel.addin” because the add-in registration is missing the required value of VendorId node. The VendorId node identify the vendor of the add-in application. For Revit to run the add-in, you must register the node defined in manifest “ZeroDocPanel.addin” file.

<center>
<img src="img/no_vendorid_node.png" alt="VendorId node missing in add-in manifest" width="370">
</center>

The `VendorId` node had not yet been invented in 2011 &nbsp; :-)

I added the vendor id, and all was well.

I debugged the add-in and ensured that the `OnStartup` and `IsCommandAvailable` methods are both called just as expected.

The external command is listed and can be executed in the external tools menu in zero document state, cf. the screen snapshot above.

The command execution displays its little task dialogue:

<center>
<img src="img/zero_doc_cmd_msg_2019.png" alt="Zero document state external command displaying a task dialogue" width="373">
</center>

The complete add-in sample including Visual Studio project,  source code and add-in manifest now lives in its
own [ZeroDocPanel GitHub repository](https://github.com/jeremytammik/ZeroDocPanel).

Here are [all the commits I made for the migration](https://github.com/jeremytammik/ZeroDocPanel/commits/master):

- Initial commit from original blog post
- Added author and license
- Flat migration to Revit 2019 &ndash; updated Revit API assembly references and .NET framework to version 4.7
- Removed references to Revit 2011
- Added `VendorId` node
- Added assembly description
- Added link to original blog post and new question thread


#### <a name="3"></a> Loading Add-In Support DLLs

In
another [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on loading the [missing `System.ComponentModel.Annotations` v4.2.0.0](https://forums.autodesk.com/t5/revit-api-forum/missing-system-componentmodel-annotations-v4-2-0-0/m-p/8255546), 
my colleague Jim Jia suggested using an assembly resolver reacting to the `AppDomain.CurrentDomain.AssemblyResolve` event:

> You may try to force loading the assembly if it cannot be loaded successfully. The code snippet below should provide a safe solution, per my experience; hope it is helpful:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Try&nbsp;to&nbsp;load&nbsp;assembly&nbsp;if&nbsp;Revit&nbsp;command&nbsp;fails&nbsp;to&nbsp;load&nbsp;it.</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AppDomain</span>.CurrentDomain.AssemblyResolve
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ResolveEventHandler</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OnAssemblyResolve&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;your&nbsp;other&nbsp;code...</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//</span>
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:#2b91af;">Assembly</span>&nbsp;OnAssemblyResolve(&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;<span style="color:#2b91af;">ResolveEventArgs</span>&nbsp;args&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Assembly</span>&nbsp;a&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;args.Name.Contains(&nbsp;<span style="color:#a31515;">&quot;ComponentModel.Annotations&quot;</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;assemblyFile&nbsp;=&nbsp;<span style="color:#2b91af;">Path</span>.Combine(&nbsp;addinDir,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;ComponentModel.Annotations.dll&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:#2b91af;">File</span>.Exists(&nbsp;assemblyFile&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a&nbsp;=&nbsp;<span style="color:#2b91af;">Assembly</span>.LoadFrom(&nbsp;assemblyFile&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;a;
&nbsp;&nbsp;}
</pre>

We explored some related issues and had success with this approach in the past, e.g., here:
  
- [Revit Add-in Unit Testing](http://thebuildingcoder.typepad.com/blog/2013/07/revit-add-in-unit-testing.html)
- [Security, Framing Cross Section Analyser and REX](http://thebuildingcoder.typepad.com/blog/2013/12/security-framing-cross-section-analyser-and-rex.html)
- [RvtVa3c Assembly Resolver](http://thebuildingcoder.typepad.com/blog/2014/05/rvtva3c-assembly-resolver.html)
- [Framing Cross Section Analyser and REX in Revit 2015](http://thebuildingcoder.typepad.com/blog/2015/03/framing-cross-section-analyser-and-rex-in-revit-2015.html)
- [REX Add-In Development and Migration](http://thebuildingcoder.typepad.com/blog/2015/12/rex-app-development-and-migration.html)
- [Add-In Templates Supporting Edit and Continue](http://thebuildingcoder.typepad.com/blog/2017/02/add-in-templates-supporting-edit-and-continue.html)


#### <a name="4"></a> Rome and  Boston Forge Accelerators

I recently mentioned that I am attending
the [Forge accelerator in Rome](http://thebuildingcoder.typepad.com/blog/2018/08/revit-20191-cefsharp-forge-accelerator-in-rome.html#5) end
of this month, September 24-28.

I expect that it is too late to decide to join that one now.

However, [you can still submit proposals for the Boston accelerator taking place in October](https://forums.autodesk.com/t5/bim-360-api-forum/forge-accelerator-boston-ma-october-1-5-2018/td-p/8249266)
&ndash; note that the deadline for submitting one is next Saturday, September 15:

The Forge team is finishing the review of proposals for the upcoming accelerator taking place in the Autodesk Boston office October 1-5.

Note that the BIM 360 Issue API is now publicly available, cf. the .NET and Node.js
samples [introducing BIM 360 Issues API](https://forge.autodesk.com/blog/introducing-bim-360-issues-api) and
accessing [BIM 360 Issues API with Node.js](https://forge.autodesk.com/blog/bim-360-issues-api-sample-nodejs).

You can now also simulate the use of the [Design Automation API](https://forge.autodesk.com/en/docs/design-automation/v2/developers_guide/overview) for Revit on your machine (note that it is still in private beta!).

Therefore, the Forge team would love to support you and your software development team in working intensively on a Forge-based project making use of these new technologies with one-on-one support and advice from Autodesk Forge API experts.

If you have an idea for a new web or mobile application based on Autodesk Forge APIs, or need help getting an existing application working, then come along to the event.

There is no cost to attend the accelerator, other than your own travel and living expenses.

However, we do ask that you submit a proposal as part of your application so that we can verify that the intended use of the Autodesk Forge APIs in your project is feasible.

You can [apply and submit your proposal here](http://autodeskcloudaccelerator.com/apply).

For more information, please visit the [Forge Accelerator website](http://autodeskcloudaccelerator.com/forge-accelerator).
