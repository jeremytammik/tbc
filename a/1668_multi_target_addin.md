<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- Show Revit custom texture map in the Forge Viewer by Eason Kang
  https://forge.autodesk.com/blog/show-revit-custom-texture-map-viewer

- cade terminology translation, terms database
  Dictionary for Revit and AutoCAD Terms for Localisation
  https://forums.autodesk.com/t5/revit-api-forum/dictionary-for-revit-and-autocad-terms-for-localisation/m-p/8074233
  https://knowledge.autodesk.com/de/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2016/DEU/AutoCAD-Core/files/GUID-C4325DCB-3648-4463-8135-629EA7F72AB0-htm.html?_ga=2.27437948.1327081858.1529427624-2130181328.1465883366
  Glossary of AutoCAD Terms
  https://knowledge.autodesk.com/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Core/files/GUID-C4325DCB-3648-4463-8135-629EA7F72AB0-htm.html

- email [Multi-targeting Revit versions made simple ;-)] Olivier Bastide of [BBS Slama](http://www.bbs-slama.com),
  éditeur de logiciels de calculs thermiques, thermal calculation software editor.
  update on http://thebuildingcoder.typepad.com/blog/2018/05/multi-target-add-ins-ai-markdown-and-job-offer.html#2

 in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

&ndash; 
...

--->

### Multi-Targeting Revit Versions

Here comes another solution for efficiently compiling add-ins for multiple Revit version targets from one single code base, and short notes on 
CAD terms databases for consistent terminology translation and access to custom texture maps in Forge and Revit:



<center>
<img src="img/.jpg" alt="" width="100"/>
</center>



#### <a name="2"></a> Multi-Targeting Revit Versions Made Simple

email [Multi-targeting Revit versions made simple ;-)] Olivier 'Vilo' Bastide of [BBS Slama](http://www.bbs-slama.com),
éditeur de logiciels de calculs thermiques, thermal calculation software editor.

update on http://thebuildingcoder.typepad.com/blog/2018/05/multi-target-add-ins-ai-markdown-and-job-offer.html#2

I'd like to give you some new stuff I've recently found, that can vastly simplify Revit addins deployment.

It is actually mostly probable that you are aware of such things, but in case you're not and/or you want to share this on your blog ...
 
This is about addins multi-targeting.

Targeting all Revit versions can become a nightmare in terms of VS projects.

I'm personally developing 6 projects for Revit, and I've to compile each one for all Revit versions (2016 to 2019), so I've 24 projects to maintain.

Source code is (thankfully) unique using conditional coding, but having all these projects is not elegant at all.
 
You have recently post something about that (http://thebuildingcoder.typepad.com/blog/2018/05/multi-target-add-ins-ai-markdown-and-job-offer.html#2), but after some tuning I didn't manage to make it work as I wanted.

So I dug into new csproj format (introduced with NET Core) and managed to find a pretty simple solution to target all Revit versions with only one csproj and source code per project.
 
Here is the csproj code skeleton (works with latest VS Community 2017, at least) :

<pre class="code">
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFrameworks>net452;net46;net47</TargetFrameworks>
        <Configurations>Debug;Release</Configurations>
        <Platforms>x64</Platforms>
        <OutputPath>bin\$(Configuration)\</OutputPath>
    </PropertyGroup>
    
    <PropertyGroup Condition="'$(Configuration)'=='Debug'">
        <PlatformTarget>x64</PlatformTarget>
        <DefineConstants>DEBUG</DefineConstants>
        <Optimize>false</Optimize>
        <DebugType>full</DebugType>
        <DebugSymbols>true</DebugSymbols>
    </PropertyGroup>

    <PropertyGroup Condition="'$(Configuration)'=='Release'">
        <PlatformTarget>x64</PlatformTarget>
        <DebugType>none</DebugType>
        <DebugSymbols>false</DebugSymbols>
    </PropertyGroup>

    <PropertyGroup Condition=" '$(TargetFramework)' == 'net452' ">
        <DefineConstants>$(DefineConstants);REVIT2017</DefineConstants>
        <AssemblyName>SomeProject_2017</AssemblyName>
    </PropertyGroup>

    <PropertyGroup Condition=" '$(TargetFramework)' == 'net46' ">
        <DefineConstants>$(DefineConstants);REVIT2018</DefineConstants>
        <AssemblyName>SomeProject_2018</AssemblyName>
    </PropertyGroup>

  <PropertyGroup Condition=" '$(TargetFramework)' == 'net47' ">
    <DefineConstants>$(DefineConstants);REVIT2019</DefineConstants>
    <AssemblyName>SomeProject_2019</AssemblyName>
  </PropertyGroup>

  <ItemGroup Condition=" '$(TargetFramework)' == 'net452' ">
        <Reference Include="AdWindows, Version=2015.11.1.0, Culture=neutral, PublicKeyToken=null">
            <HintPath>......\2017\AdWindows.dll</HintPath>
            <EmbedInteropTypes>false</EmbedInteropTypes>
            <Private>false</Private>
        </Reference>
        <Reference Include="RevitAPI">
            <HintPath>......\2017\RevitAPI.dll</HintPath>
            <EmbedInteropTypes>false</EmbedInteropTypes>
            <Private>false</Private>
        </Reference>
        <Reference Include="RevitAPIUI">
            <HintPath>......\2017\RevitAPIUI.dll</HintPath>
            <EmbedInteropTypes>false</EmbedInteropTypes>
            <Private>false</Private>
        </Reference>
    </ItemGroup>

    <ItemGroup Condition=" '$(TargetFramework)' == 'net46' ">
        <Reference Include="AdWindows">
            <HintPath>......\2018\AdWindows.dll</HintPath>
            <EmbedInteropTypes>false</EmbedInteropTypes>
            <Private>false</Private>
        </Reference>
        <Reference Include="RevitAPI">
            <HintPath>......\2018\RevitAPI.dll</HintPath>
            <EmbedInteropTypes>false</EmbedInteropTypes>
            <Private>false</Private>
        </Reference>
        <Reference Include="RevitAPIUI">
            <HintPath>......\2018\RevitAPIUI.dll</HintPath>
            <EmbedInteropTypes>false</EmbedInteropTypes>
            <Private>false</Private>
        </Reference>
    </ItemGroup>

  <ItemGroup Condition=" '$(TargetFramework)' == 'net47' ">
    <Reference Include="AdWindows">
      <HintPath>......\2019\AdWindows.dll</HintPath>
      <EmbedInteropTypes>false</EmbedInteropTypes>
      <Private>false</Private>
    </Reference>
    <Reference Include="RevitAPI">
      <HintPath>......\2019\RevitAPI.dll</HintPath>
      <EmbedInteropTypes>false</EmbedInteropTypes>
      <Private>false</Private>
    </Reference>
    <Reference Include="RevitAPIUI">
      <HintPath>......\2019\RevitAPIUI.dll</HintPath>
      <EmbedInteropTypes>false</EmbedInteropTypes>
      <Private>false</Private>
    </Reference>
  </ItemGroup>

 <ItemGroup>
    <Compile Include="SomeFile.cs">
    </Compile>
</ItemGroup>
</pre>

Obviously, user will have to customize file paths ...
 
By using the new TargetFrameworks mechanism, it is possible to configure separate settings for each version.

Actually, the trick is possible because each Revit version is targeting a different .NET framework ...
 
I've included "not mandatory" things in the above example.

For example, the AssemblyName tag is optional, but I've included it to show how to change output dll filename according to Revit version.
 
When a new Revit version is out, the only thing to do is to declare a new target in TargetFrameworks tag, and configure the matching ItemGroup. to tell VS where to find new SDK's dlls.

If the new version does not target a different .NET framework, it is possible to keep the thing working by targeting a sub-version (like 4.7.1).
 
Finally, to make source code "version agnostic", one can use conditional definitions REVIT2017, REVIT2018, REVIT2019 as usual.
 
Hope it will help.

#### <a name="4"></a> Further Enhancement Using `CSPROJ` `Import` Tag

MultiTarget addins : season 01 episode 02...

Back to you to make an addendum to my preceding mail.

In the eternal fight to reduce efforts involved to obtain the same achievements, I've made some progress.

Csproj format (both old and new) supports a kind of sequential inheritance through the tag <Import>.

Using it, one can tailor a "root" .csproj file that contains all the basic logic about Revit versions, and then inherits from it in each "real" projects, overriding only the specific varying tags.

Here is a sample root csproj (file name is C:\ROOT.CSPROJ for this sample to work) :

<pre class="code">
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup Condition="'$(TargetFrameworks)'==''">
        <TargetFrameworks>net45;net452;net46;net47</TargetFrameworks>
    </PropertyGroup>
    
    <PropertyGroup>
        <Configurations>Debug;Release</Configurations>
        <Platforms>x64</Platforms>
        <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
        <EnableDefaultItems>false</EnableDefaultItems>
    </PropertyGroup>
    
    <PropertyGroup Condition=" '$(TargetFramework)' == 'net45' ">
        <RevitVersion>2016</RevitVersion>
    </PropertyGroup>

    <PropertyGroup Condition=" '$(TargetFramework)' == 'net452' ">
        <RevitVersion>2017</RevitVersion>
    </PropertyGroup>

    <PropertyGroup Condition=" '$(TargetFramework)' == 'net46' ">
        <RevitVersion>2018</RevitVersion>
    </PropertyGroup>

    <PropertyGroup Condition=" '$(TargetFramework)' == 'net47' ">
        <RevitVersion>2019</RevitVersion>
    </PropertyGroup>
  
    <PropertyGroup>
        <DefineConstants>$(DefineConstants);REVIT$(RevitVersion)</DefineConstants> <!-- defines conditional directive REVIT2016, REVIT2017 ... -->
        <AssemblyName>$(PrefixAssembly)_$(RevitVersion)</AssemblyName>
    </PropertyGroup>
    
    <PropertyGroup>
        <Path_DLL_API>... FQPN to folder with API dlls, separated into subfolders named 2016, 2017, 2018, 2019, etc ...</Path_DLL_API>
    </PropertyGroup>
    
  <ItemGroup>
    <Reference Include="AdWindows">
      <HintPath>$(Path_DLL_API)\$(RevitVersion)\AdWindows.dll</HintPath>
      <EmbedInteropTypes>false</EmbedInteropTypes>
      <Private>false</Private>
    </Reference>
    <Reference Include="RevitAPI">
      <HintPath>$(Path_DLL_API)\$(RevitVersion)\RevitAPI.dll</HintPath>
      <EmbedInteropTypes>false</EmbedInteropTypes>
      <Private>false</Private>
    </Reference>
    <Reference Include="RevitAPIUI">
      <HintPath>$(Path_DLL_API)\$(RevitVersion)\RevitAPIUI.dll</HintPath>
      <EmbedInteropTypes>false</EmbedInteropTypes>
      <Private>false</Private>
    </Reference>
  </ItemGroup>
</Project>
</pre>

This one is suitable to compile for Revit versions 2016 to 2019 (by targeting 4 different frameworks).

And here is a sample child project using it :

<pre class="code">
<Project>
        <!-- this group must stay before <Import> !! -->
    <PropertyGroup>
<!-- this tag allows generic naming, for example this will lead to assemblies named "SomeAssembly_2016", "SomeAssembly_2017", ... -->
        <PrefixAssembly>SomeAssembly</PrefixAssembly> 

<!-- by setting this tag here, you can override default behaviour that is to compile for versions 2016 to 2019
Delete it to restore default behaviour and compile for 2016 to 2019 -->
        <TargetFrameworks>net452</TargetFrameworks>
    </PropertyGroup>

<!-- import root csproj -->
    <Import Project="C:\ROOT.CSPROJ" /> <!-- FQPN to file defined above -->

<!-- all other stuff that is project specific -->
        ...
</Project>
</pre>

Of course these 2 samples are just proof of concept, and tailored for my own needs.

Feel free to make what you want with it ;-)

#### <a name="3"></a> CAD Terminology Databases for Consistent Translation

cade terminology translation, terms database
Dictionary for Revit and AutoCAD Terms for Localisation
https://forums.autodesk.com/t5/revit-api-forum/dictionary-for-revit-and-autocad-terms-for-localisation/m-p/8074233
https://knowledge.autodesk.com/de/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2016/DEU/AutoCAD-Core/files/GUID-C4325DCB-3648-4463-8135-629EA7F72AB0-htm.html?_ga=2.27437948.1327081858.1529427624-2130181328.1465883366
Glossary of AutoCAD Terms
https://knowledge.autodesk.com/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Core/files/GUID-C4325DCB-3648-4463-8135-629EA7F72AB0-htm.html

#### <a name="4"></a> Showing a Custom Revit Texture Map in the Forge Viewer

Show Revit custom texture map in the Forge Viewer by Eason Kang
https://forge.autodesk.com/blog/show-revit-custom-texture-map-viewer



**Question:** 

**Answer:** 

<pre class="code">
</pre>


Many thanks to 
 
