<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- email [Multi-targeting Revit versions made simple ;-)] Olivier Bastide of [BBS Slama](http://www.bbs-slama.com),
  éditeur de logiciels de calculs thermiques, thermal calculation software editor.
  update on http://thebuildingcoder.typepad.com/blog/2018/05/multi-target-add-ins-ai-markdown-and-job-offer.html#2

- cade terminology translation, terms database
  Dictionary for Revit and AutoCAD Terms for Localisation
  https://forums.autodesk.com/t5/revit-api-forum/dictionary-for-revit-and-autocad-terms-for-localisation/m-p/8074233
  https://knowledge.autodesk.com/de/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2016/DEU/AutoCAD-Core/files/GUID-C4325DCB-3648-4463-8135-629EA7F72AB0-htm.html?_ga=2.27437948.1327081858.1529427624-2130181328.1465883366
  Glossary of AutoCAD Terms
  https://knowledge.autodesk.com/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Core/files/GUID-C4325DCB-3648-4463-8135-629EA7F72AB0-htm.html

- Show Revit custom texture map in the Forge Viewer by Eason Kang
  https://forge.autodesk.com/blog/show-revit-custom-texture-map-viewer

Multi-targeting Revit versions, CAD terms and texture maps in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/multitargetaddin

Here comes another solution for efficiently compiling add-ins for multiple Revit version targets from one single code base, a note on resources for CAD term databases for consistent terminology translation, and a discussion on accessing custom texture maps in Forge and Revit
&ndash; Multi-targeting Revit versions using <code>TargetFrameworks</code>
&ndash; Further enhancement using the <code>CSPROJ</code> <code>Import</code> tag
&ndash; CAD terminology resources for consistent translation
&ndash; Showing a custom Revit texture map in the Forge Viewer...

--->

### Multi-Targeting Revit Versions, CAD Terms, Textures

Here comes another solution for efficiently compiling add-ins for multiple Revit version targets from one single code base, a note on resources for CAD term databases for consistent terminology translation, and a discussion on accessing custom texture maps in Forge and Revit:

- [Multi-targeting Revit versions using `TargetFrameworks`](#2) 
- [Further enhancement using the `CSPROJ` `Import` tag](#3) 
- [CAD terminology resources for consistent translation](#4) 
- [Showing a custom Revit texture map in the Forge Viewer](#5) 

<center>
<img src="img/change_target.png" alt="Change target" width="360"/>
</center>



#### <a name="2"></a> Multi-Targeting Revit Versions Using TargetFrameworks 

Just recently, we pointed out a suggestion
for [compiling add-ins for multiple Revit versions](http://thebuildingcoder.typepad.com/blog/2018/05/multi-target-add-ins-ai-markdown-and-job-offer.html).

Here are some other, previous, related discussions:

- [Add-In Applications for Multiple Revit Products](http://thebuildingcoder.typepad.com/blog/2010/06/addin-applications-for-multiple-revit-products.html)
- [Multi-Version Add-in](http://thebuildingcoder.typepad.com/blog/2012/07/multi-version-add-in.html)
- [Multi-Version Visual Studio Revit Add-In Wizard](http://thebuildingcoder.typepad.com/blog/2013/11/multi-version-visual-studio-revit-add-in-wizard.html)
- [RevitLookup in Python Shell and Multi-Release Solution](http://thebuildingcoder.typepad.com/blog/2015/05/revitlookup-in-python-shell-and-multi-release-solution.html)
- [ADN Labs Xtra, Multi-Version Add-Ins and CNC Direct](http://thebuildingcoder.typepad.com/blog/2015/06/adn-labs-xtra-multi-version-add-ins-and-cnc-direct.html)

Today, Olivier 'Vilo' Bastide of [BBS Slama](http://www.bbs-slama.com), *éditeur de logiciels de calculs thermiques*,
thermal calculation software editor, suggests a different and simpler approach making use of the `TargetFrameworks` functionality, and a further enhancement to that using the `Import` tag.

In his own words:

I'd like to give you some new stuff I've recently found, that can vastly simplify Revit add-in deployment.

This is about add-in multi-targeting.

Targeting all Revit versions can become a nightmare in terms of VS projects.

I'm personally developing 6 projects for Revit, and I have to compile each one for all Revit versions (2016 to 2019), resulting in 24 projects to maintain.

Source code is (thankfully) unique using conditional coding, but having all these projects is not elegant at all.
 
You recently posted 
a [suggestion for handling this](http://thebuildingcoder.typepad.com/blog/2018/05/multi-target-add-ins-ai-markdown-and-job-offer.html#2), but after some tuning I couldn't manage to make it work as I wanted.

So, I dug into new `csproj` format (introduced with .NET Core) and managed to find a pretty simple solution to target all Revit versions with only one `csproj` and source code per project.
 
Here is the `csproj` code skeleton; it works with the latest VS Community 2017, at least:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Project</span>&nbsp;<span style="color:red;">Sdk</span><span style="color:blue;">=</span><span style="color:blue;">&quot;Microsoft.NET.Sdk&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">TargetFrameworks</span><span style="color:blue;">&gt;</span>net452;net46;net47<span style="color:blue;">&lt;/</span><span style="color:maroon;">TargetFrameworks</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Configurations</span><span style="color:blue;">&gt;</span>Debug;Release<span style="color:blue;">&lt;/</span><span style="color:maroon;">Configurations</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Platforms</span><span style="color:blue;">&gt;</span>x64<span style="color:blue;">&lt;/</span><span style="color:maroon;">Platforms</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">OutputPath</span><span style="color:blue;">&gt;</span>bin\$(Configuration)\<span style="color:blue;">&lt;/</span><span style="color:maroon;">OutputPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&#39;$(Configuration)&#39;==&#39;Debug&#39;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PlatformTarget</span><span style="color:blue;">&gt;</span>x64<span style="color:blue;">&lt;/</span><span style="color:maroon;">PlatformTarget</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">DefineConstants</span><span style="color:blue;">&gt;</span>DEBUG<span style="color:blue;">&lt;/</span><span style="color:maroon;">DefineConstants</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Optimize</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Optimize</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">DebugType</span><span style="color:blue;">&gt;</span>full<span style="color:blue;">&lt;/</span><span style="color:maroon;">DebugType</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">DebugSymbols</span><span style="color:blue;">&gt;</span>true<span style="color:blue;">&lt;/</span><span style="color:maroon;">DebugSymbols</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&#39;$(Configuration)&#39;==&#39;Release&#39;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PlatformTarget</span><span style="color:blue;">&gt;</span>x64<span style="color:blue;">&lt;/</span><span style="color:maroon;">PlatformTarget</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">DebugType</span><span style="color:blue;">&gt;</span>none<span style="color:blue;">&lt;/</span><span style="color:maroon;">DebugType</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">DebugSymbols</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">DebugSymbols</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net452&#39;&nbsp;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">DefineConstants</span><span style="color:blue;">&gt;</span>$(DefineConstants);REVIT2017<span style="color:blue;">&lt;/</span><span style="color:maroon;">DefineConstants</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">AssemblyName</span><span style="color:blue;">&gt;</span>SomeProject_2017<span style="color:blue;">&lt;/</span><span style="color:maroon;">AssemblyName</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net46&#39;&nbsp;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">DefineConstants</span><span style="color:blue;">&gt;</span>$(DefineConstants);REVIT2018<span style="color:blue;">&lt;/</span><span style="color:maroon;">DefineConstants</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">AssemblyName</span><span style="color:blue;">&gt;</span>SomeProject_2018<span style="color:blue;">&lt;/</span><span style="color:maroon;">AssemblyName</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net47&#39;&nbsp;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">DefineConstants</span><span style="color:blue;">&gt;</span>$(DefineConstants);REVIT2019<span style="color:blue;">&lt;/</span><span style="color:maroon;">DefineConstants</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">AssemblyName</span><span style="color:blue;">&gt;</span>SomeProject_2019<span style="color:blue;">&lt;/</span><span style="color:maroon;">AssemblyName</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">ItemGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net452&#39;&nbsp;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;AdWindows,&nbsp;Version=2015.11.1.0,&nbsp;Culture=neutral,&nbsp;PublicKeyToken=null&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>......\2017\AdWindows.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;RevitAPI&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>......\2017\RevitAPI.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;RevitAPIUI&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>......\2017\RevitAPIUI.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">ItemGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">ItemGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net46&#39;&nbsp;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;AdWindows&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>......\2018\AdWindows.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;RevitAPI&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>......\2018\RevitAPI.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;RevitAPIUI&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>......\2018\RevitAPIUI.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">ItemGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">ItemGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net47&#39;&nbsp;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;AdWindows&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>......\2019\AdWindows.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;RevitAPI&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>......\2019\RevitAPI.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;RevitAPIUI&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>......\2019\RevitAPIUI.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">ItemGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">ItemGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Compile</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;SomeFile.cs&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Compile</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">ItemGroup</span><span style="color:blue;">&gt;</span>
</pre>

Obviously, you will have to customize the file paths appropriately...
 
By using the new `TargetFrameworks` mechanism, it is possible to configure separate settings for each version.

Actually, the trick is possible because each Revit version is targeting a different .NET framework...
 
I've included 'non-mandatory' things in the above example.

For example, the `AssemblyName` tag is optional, but I've included it to show how to change the output DLL filename according to the Revit version.
 
When a new Revit version is released, the only thing to do is to declare a new target in the `TargetFrameworks` tag and configure the matching `ItemGroup` to tell VS where to find new SDK's DLLs.

If the new version does not target a different .NET framework, it is possible to keep the thing working by targeting a sub-version (like 4.7.1).
 
Finally, to make your source code `version agnostic`, you can use conditional definitions such as `REVIT2017`, `REVIT2018`, `REVIT2019`, etc., as usual.
 
Hope this will help.


#### <a name="3"></a> Further Enhancement Using the `CSPROJ` `Import` Tag

MultiTarget addins : season 01 episode 02...

Here is an addendum to my preceding suggestion with a further enhancement.

In the eternal fight to reduce efforts involved to obtain the same achievements, I've made some new progress.

The `csproj` format (both old and new) supports a kind of sequential inheritance through the tag `<Import>`.

Using that, you can tailor a 'root' `.csproj` file that contains all the basic logic about Revit versions, and then inherits from it in each of the 'real' projects, overriding only the specific varying tags.

Here is a sample root `csproj`, named *C:\ROOT.CSPROJ* for this sample to work:

<pre class="code">
<span style="color:blue;">&lt;</span><span style="color:maroon;">Project</span>&nbsp;<span style="color:red;">Sdk</span><span style="color:blue;">=</span><span style="color:blue;">&quot;Microsoft.NET.Sdk&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&#39;$(TargetFrameworks)&#39;==&#39;&#39;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">TargetFrameworks</span><span style="color:blue;">&gt;</span>net45;net452;net46;net47<span style="color:blue;">&lt;/</span><span style="color:maroon;">TargetFrameworks</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Configurations</span><span style="color:blue;">&gt;</span>Debug;Release<span style="color:blue;">&lt;/</span><span style="color:maroon;">Configurations</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Platforms</span><span style="color:blue;">&gt;</span>x64<span style="color:blue;">&lt;/</span><span style="color:maroon;">Platforms</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EnableDefaultCompileItems</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EnableDefaultCompileItems</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EnableDefaultItems</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EnableDefaultItems</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net45&#39;&nbsp;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">RevitVersion</span><span style="color:blue;">&gt;</span>2016<span style="color:blue;">&lt;/</span><span style="color:maroon;">RevitVersion</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net452&#39;&nbsp;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">RevitVersion</span><span style="color:blue;">&gt;</span>2017<span style="color:blue;">&lt;/</span><span style="color:maroon;">RevitVersion</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net46&#39;&nbsp;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">RevitVersion</span><span style="color:blue;">&gt;</span>2018<span style="color:blue;">&lt;/</span><span style="color:maroon;">RevitVersion</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span>&nbsp;<span style="color:red;">Condition</span><span style="color:blue;">=</span><span style="color:blue;">&quot;&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net47&#39;&nbsp;&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">RevitVersion</span><span style="color:blue;">&gt;</span>2019<span style="color:blue;">&lt;/</span><span style="color:maroon;">RevitVersion</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">DefineConstants</span><span style="color:blue;">&gt;</span>$(DefineConstants);REVIT$(RevitVersion)<span style="color:blue;">&lt;/</span><span style="color:maroon;">DefineConstants</span><span style="color:blue;">&gt;</span>&nbsp;<span style="color:darkgreen;">&lt;!--&nbsp;defines&nbsp;conditional&nbsp;directive&nbsp;REVIT2016,&nbsp;REVIT2017&nbsp;...&nbsp;--&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">AssemblyName</span><span style="color:blue;">&gt;</span>$(PrefixAssembly)_$(RevitVersion)<span style="color:blue;">&lt;/</span><span style="color:maroon;">AssemblyName</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Path_DLL_API</span><span style="color:blue;">&gt;</span>...&nbsp;FQPN&nbsp;to&nbsp;folder&nbsp;with&nbsp;API&nbsp;dlls,&nbsp;separated&nbsp;into&nbsp;subfolders&nbsp;named&nbsp;2016,&nbsp;2017,&nbsp;2018,&nbsp;2019,&nbsp;etc&nbsp;...<span style="color:blue;">&lt;/</span><span style="color:maroon;">Path_DLL_API</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">ItemGroup</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;AdWindows&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>$(Path_DLL_API)\$(RevitVersion)\AdWindows.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;RevitAPI&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>$(Path_DLL_API)\$(RevitVersion)\RevitAPI.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Reference</span>&nbsp;<span style="color:red;">Include</span><span style="color:blue;">=</span><span style="color:blue;">&quot;RevitAPIUI&quot;</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>$(Path_DLL_API)\$(RevitVersion)\RevitAPIUI.dll<span style="color:blue;">&lt;/</span><span style="color:maroon;">HintPath</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:maroon;">Private</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">Reference</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">ItemGroup</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:maroon;">Project</span><span style="color:blue;">&gt;</span>
</pre>

This one is suitable to compile for Revit versions 2016 to 2019 (by targeting 4 different frameworks).

And here is a sample child project using it:

<pre class="code">
<span style="color:blue;">&lt;</span><span style="color:maroon;">Project</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:darkgreen;">&lt;!--&nbsp;this&nbsp;group&nbsp;must&nbsp;stay&nbsp;before&nbsp;&lt;Import&gt;&nbsp;!!&nbsp;--&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
<span style="color:darkgreen;">&lt;!--&nbsp;this&nbsp;tag&nbsp;allows&nbsp;generic&nbsp;naming,&nbsp;for&nbsp;example&nbsp;this&nbsp;will&nbsp;lead&nbsp;to&nbsp;assemblies&nbsp;named&nbsp;&quot;SomeAssembly_2016&quot;,&nbsp;&quot;SomeAssembly_2017&quot;,&nbsp;...&nbsp;--&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">PrefixAssembly</span><span style="color:blue;">&gt;</span>SomeAssembly<span style="color:blue;">&lt;/</span><span style="color:maroon;">PrefixAssembly</span><span style="color:blue;">&gt;</span>&nbsp;
 
<span style="color:darkgreen;">&lt;!--&nbsp;by&nbsp;setting&nbsp;this&nbsp;tag&nbsp;here,&nbsp;you&nbsp;can&nbsp;override&nbsp;default&nbsp;behaviour&nbsp;that&nbsp;is&nbsp;to&nbsp;compile&nbsp;for&nbsp;versions&nbsp;2016&nbsp;to&nbsp;2019
Delete&nbsp;it&nbsp;to&nbsp;restore&nbsp;default&nbsp;behaviour&nbsp;and&nbsp;compile&nbsp;for&nbsp;2016&nbsp;to&nbsp;2019&nbsp;--&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">TargetFrameworks</span><span style="color:blue;">&gt;</span>net452<span style="color:blue;">&lt;/</span><span style="color:maroon;">TargetFrameworks</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;/</span><span style="color:maroon;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
<span style="color:darkgreen;">&lt;!--&nbsp;import&nbsp;root&nbsp;csproj&nbsp;--&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">&lt;</span><span style="color:maroon;">Import</span>&nbsp;<span style="color:red;">Project</span><span style="color:blue;">=</span><span style="color:blue;">&quot;C:\ROOT.CSPROJ&quot;</span>&nbsp;<span style="color:blue;">/&gt;</span>&nbsp;<span style="color:darkgreen;">&lt;!--&nbsp;FQPN&nbsp;to&nbsp;file&nbsp;defined&nbsp;above&nbsp;--&gt;</span>
 
<span style="color:darkgreen;">&lt;!--&nbsp;all&nbsp;other&nbsp;stuff&nbsp;that&nbsp;is&nbsp;project&nbsp;specific&nbsp;--&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;...
<span style="color:blue;">&lt;/</span><span style="color:maroon;">Project</span><span style="color:blue;">&gt;</span>
</pre>

Of course, these 2 samples are just a proof of concept, and tailored for my own needs.

Feel free to make what you want with it ;-)

Many thanks to Olivier 'Vilo' for researching and sharing this very sensible solution!

<a name="3.1"></a> Vilo responds: Cool &nbsp; &nbsp; ;-) &nbsp; &nbsp; Hope it will help some people simplify their DevEnv.

Jeremy answers: if people take the trouble to read and the time to improve, it will for sure &nbsp; &nbsp; :-) &nbsp; &nbsp; all too often, however, one does not take the time to improve things, even though it would save ten times the time it costs to make the improvement...


#### <a name="4"></a> CAD Terminology Resources for Consistent Translation

People occasionally ask for help translating CAD terms, and I already mentioned a couple of useful resources for this in the past.

This question came up again in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on a [dictionary for Revit and AutoCAD terms for localisation](https://forums.autodesk.com/t5/revit-api-forum/dictionary-for-revit-and-autocad-terms-for-localisation/m-p/8074233):

**Question:** I want to use the correct Autodesk terms in our localized add-in user documentation.

I am searching for a table of translations of Revit and AutoCAD terms into multiple languages which can be used for our automated translation dictionary.  I just need a mapping English term &rarr; Localized term. The supported languages are currently German, French, Spanish (Mexico), Chinese (Simplified), Portuguese (Brazil), Japanese and Italian.

I found a good [Glossary of German AutoCAD Terms](https://knowledge.autodesk.com/de/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2016/DEU/AutoCAD-Core/files/GUID-C4325DCB-3648-4463-8135-629EA7F72AB0-htm.html?_ga=2.226928601.558689868.1529839545-2130181328.1465883366) which
can be displayed in different languages.

I see that, e.g., `LAYER` in English is the same in German, but `SOLID` is `VOLUMENKÖRPER`.

I would prefer a table instead of an HTML page for more automation.

**Answer:** I provided an answer to a similar question
on [CAD terminology translation back in 2014](http://thebuildingcoder.typepad.com/blog/2014/10/autodesk-open-source-all-over-germany-and-japan.html#4).

It mentions a number of useful resources back then.

Here is an expanded and updated list for today:

- [Glossary of AutoCAD Terms](https://knowledge.autodesk.com/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Core/files/GUID-C4325DCB-3648-4463-8135-629EA7F72AB0-htm.html).
- [AutoCAD language packs, e.g., for AutoCAD 2019](http://knowledge.autodesk.com/support/autocad/downloads/caas/downloads/content/autocad-2019-language-packs.html).
- [AutoCAD command dictionary](http://www.cadforum.cz/cadforum_en/command.asp)
&ndash; lists AutoCAD command translations and does not support all languages.
- AutoCAD end user online document per language:
    - [English](http://help.autodesk.com/view/ACD/2019/ENU/)
    - [Japanese](http://help.autodesk.com/view/ACD/2019/JPN/)
    - [French](http://help.autodesk.com/view/ACD/2019/FRA/)
    - [Korean](http://help.autodesk.com/view/ACD/2019/KOR/)
    - [Russian](http://help.autodesk.com/view/ACD/2019/RUS/)
    - etc.
- [Autodesk localisation team cross product corpus database NeXLT](http://langtech.autodesk.com/nexlt) &ndash; terminology and message translation.

The latter resource is product agnostic, so it will hopefully include all you need for Revit, AEC and BIM.


#### <a name="5"></a> Showing a Custom Revit Texture Map in the Forge Viewer

To help address the recurring question of accessing texture map data in a Revit model, my colleague Eason Kang researched and published how 
to [show Revit custom texture map in the Forge Viewer](https://forge.autodesk.com/blog/show-revit-custom-texture-map-viewer).

Many thanks to Eason for sharing this important information!

