<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>

</style>

</head>

<!---

twitter:

 the @AutodeskRevit #RevitAPI #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Multi-Version Add-In Code Base


####<a name="2"></a> Multi-Version Add-In Code Base

Managing an add-in code base to support multiple releases of the Revit API is a recurring and constantly evolving issue.
In 2021, we discussed how to implement
a [multi-version Revit add-in](https://thebuildingcoder.typepad.com/blog/2021/10/dll-as-resource-and-multi-version-add-ins.html#4),
and in 2023 [managing multiple Revit API versions](https://thebuildingcoder.typepad.com/blog/2023/11/net-core-preview-and-open-source-add-in-projects.html#8)

The issue was raised again half a year ago in preparation of the non-trivial migration for Revit 2025 from .NET 4.8 to .NET core, in a quest for
an [optimal add-in code base approach to target multiple Revit releases](https://forums.autodesk.com/t5/revit-api-forum/optimal-add-in-code-base-approach-to-target-multiple-revit/m-p/12982599).

That query recently received some updated answers and solutions from
Roman [@Nice3point](https://t.me/nice3point) Karpovich, aka Роман Карпович,
and Nathan [@SamBerk](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3671855) Berkowitz.

Roman points out that [RevitTemplates](https://github.com/Nice3point/RevitTemplates) provides
a [step-by step guide for Revit multiversion templates](https://github.com/Nice3point/RevitTemplates/wiki/Step%E2%80%90by%E2%80%90step-Guide):

> You will be able to create a project in a few steps without spending hours setting up a solution from scratch. Try it.

Sam's simpler approach provides less coverage, saying:

I found that the *simplest* solution to maintain the same code base for 2025 and earlier is to put the `TargetFramework` in a condition.
Here is step by step:

 - Create a dotnet 8 class library project
 - Reference the Revit 2025 API DLL's RevitAPI.dll and RevitAPIUI.dll, set `Copy Local` to `No`
 - Create configurations `2024Debug` and `2025Debug` (or release)
 - Edit the project file and put the `TargetFramework` and the `Reference` in a condition:


 <PropertyGroup Condition="'$(Configuration)' == '2024Debug'">
   <TargetFramework>net481</TargetFramework>
 </PropertyGroup>
 <PropertyGroup Condition="'$(Configuration)' == '2025Debug'">
   <TargetFramework>net8.0</TargetFramework>
 </PropertyGroup>

 <ItemGroup>
    <Reference Include="RevitAPI" Condition="'$(Configuration)' == '2024Debug'">
     <HintPath>..\..\..\..\..\..\..\..\Program Files\Autodesk\Revit 2024\RevitAPI.dll</HintPath>
     <Private>False</Private>
   </Reference>
   <Reference Include="RevitAPIUI" Condition="'$(Configuration)' == '2024Debug'">
     <HintPath>..\..\..\..\..\..\..\..\Program Files\Autodesk\Revit 2024\RevitAPIUI.dll</HintPath>
     <Private>False</Private>
   </Reference>
   <Reference Include="RevitAPI" Condition="'$(Configuration)' == '2025Debug'">
     <HintPath>..\..\..\..\..\..\..\..\Program Files\Autodesk\Revit 2025\RevitAPI.dll</HintPath>
     <Private>False</Private>
   </Reference>
   <Reference Include="RevitAPIUI" Condition="'$(Configuration)' == '2025Debug'">
     <HintPath>..\..\..\..\..\..\..\..\Program Files\Autodesk\Revit 2025\RevitAPIUI.dll</HintPath>
     <Private>False</Private>
   </Reference>
 </ItemGroup>


 Create an App.cs file and implement IExternalApplication

<pre><code class="language-cs">public class App : IExternalApplication
{
  public Result OnStartup( UIControlledApplication a )
  {
    TaskDialog.Show( "Multi-Version Addin",
      $"Revit version: {a.ControlledApplication.VersionNumber}");

    return Result.Succeeded;
  }

  public Result OnShutdown( UIControlledApplication a )
  {
    return Result.Succeeded;
  }
}</code></pre>


Create an output folder for both versions ('c:\...\output\2024' and 'c:\...\output\2025')
Add a .addin file to your project for both versions (MyAddin2024.addin and MyAddin2025.addin)


<?xml version="1.0" encoding="utf-8"?>
<RevitAddIns>
  <AddIn Type="Application">
    <Name>MyAddin</Name>
    <Assembly>C:\...\output\2024\MyAddin.dll</Assembly> (or 2025)
    <FullClassName>MyAddin.App</FullClassName>
    <ClientId>{client id}</ClientId>
    <VendorId>{vendor id}</VendorId>
    <VendorDescription>{vendor description}</VendorDescription>
  </AddIn>
</RevitAddIns>


 Post-build events:


      echo Configuration: $(Configuration)
      if $(Configuration) == 2024Debug goto 2024
      if $(Configuration) == 2025Debug goto 2025

      :2024
      echo Copying results to 2024
      copy "$(ProjectDir)MyAddin2024.addin" "$(AppData)\Autodesk\REVIT\Addins\2024"
      copy "$(ProjectDir)bin\$(Configuration)\net481\*.dll" "C:\...\output\2024"
      goto exit

      :2024
      echo Copying results to 2024
      copy "$(ProjectDir)MyAddin2025.addin" "$(AppData)\Autodesk\REVIT\Addins\2025"
      copy "$(ProjectDir)bin\$(Configuration)\net8.0\*.dll" "C:\...\output\2025"
      goto exit

      :exit


Build both configurations
Open Revit 2024:
SamBerk_0-1724709688908.png
Open Revit 2025:
SamBerk_1-1724709739994.png

For debugging, add to the project file:


  <PropertyGroup Condition="'$(Configuration)' == '2024Debug'">
    <StartProgram>C:\Program Files\Autodesk\Revit 2024\Revit.exe</StartProgram>
    <StartAction>Program</StartAction>
  </PropertyGroup>

  <PropertyGroup Condition="'$(Configuration)' == '2025Debug'">
    <StartProgram>C:\Program Files\Autodesk\Revit 2025\Revit.exe</StartProgram>
    <StartAction>Program</StartAction>
  </PropertyGroup>



Thank you all





an [optimal add-in code base approach to target multiple Revit releases](https://forums.autodesk.com/t5/revit-api-forum/optimal-add-in-code-base-approach-to-target-multiple-revit/m-p/12982599#M81063)

**Question:**

**Answer:**

<center>
  <img src="img/.png" alt="" title="" width="100"/>
</center>
