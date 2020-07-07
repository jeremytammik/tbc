<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

12914853 [several versions of a control inside same APPDomain using XAML/BAML craches REVIT]
12494378 [Addins have conflicts with DLL's from other developers]
11655345 [Addin conflicts]
http://forums.autodesk.com/t5/revit-api/proper-way-to-handle-app-config-bindingredirects-in-revit-add-in/m-p/5692149
http://forums.autodesk.com/t5/revit-api/loading-different-versions-of-same-third-party-library/m-p/6023644

http://adndevblog.typepad.com/aec/2012/06/loading-multiple-versions-of-the-same-dll-used-in-revit-plug-ins.html

Other option but a lot of changes required

Managed Extensibility Framework (MEF) -- https://blogs.msdn.microsoft.com/kcwalina/2008/04/25/managed-extensibility-framework/
A Plug-In System Using Reflection, AppDomain and ISponsor -- http://www.brad-smith.info/blog/archives/500

Loading multiple versions of the same DLL used in Revit plug-ins -- http://adndevblog.typepad.com/aec/2012/06/loading-multiple-versions-of-the-same-dll-used-in-revit-plug-ins.html#comment-6a0167607c2431970b0167676439d4970b

Maxence DELANNOY said: You can also use ILMerge to merge all you dll in a single assembly.

ILMerge -- https://www.microsoft.com/en-us/download/details.aspx?id=17630

ILMerge is a utility for merging multiple .NET assemblies into a single .NET assembly.

Merging .NET assemblies using ILMerge -- https://www.codeproject.com/articles/9364/merging-net-assemblies-using-ilmerge

13034010 [General API Questions]

Handling add-in third party library DLL hell #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/dllconflict 

A recurring question just came up again, on handling conflicts between DLLs loaded by Revit add-ins. For instance, this can be caused by a scenario in which add-ins A and B both make use of library DLL C, but specify different versions. A soon as the first add-in has loaded its version of C, the second add-in is prevented from loading the version it requires and cannot run...

-->

### Handling Third Party Library DLL Conflicts

A recurring question just came up again, on handling conflicts between DLLs loaded by Revit add-ins.

For instance, this can be caused by a scenario in which add-ins A and B both make use of library DLL C, but specify different versions. A soon as the first add-in has loaded its version of C, the second add-in is prevented from loading the version it requires and cannot run.

I have no official solution to suggest for this, just these discussion threads and articles suggesting various workarounds:

- [Proper way to handle App.config bindingRedirects in Revit add-in](http://forums.autodesk.com/t5/revit-api/proper-way-to-handle-app-config-bindingredirects-in-revit-add-in/m-p/5692149)
- [Loading different versions of same third party library](http://forums.autodesk.com/t5/revit-api/loading-different-versions-of-same-third-party-library/m-p/6023644)
- [Loading multiple versions of the same DLL used in Revit plug-ins](http://adndevblog.typepad.com/aec/2012/06/loading-multiple-versions-of-the-same-dll-used-in-revit-plug-ins.html)
- The [Managed Extensibility Framework MEF](https://blogs.msdn.microsoft.com/kcwalina/2008/04/25/managed-extensibility-framework) offers an option but requires a lot of changes.
- [A Plug-In System Using Reflection, AppDomain and ISponsor](http://www.brad-smith.info/blog/archives/500)
- You can also use [ILMerge](https://www.microsoft.com/en-us/download/details.aspx?id=17630) to merge all of your DLLs into one single .NET assembly, cf. the CodeProject article on [Merging .NET assemblies using ILMerge](https://www.codeproject.com/articles/9364/merging-net-assemblies-using-ilmerge)

<center>
<img src="img/darvasa_gas_crater_panorama.jpg" alt="DLL hell?" width="300"> 
</center>

#### Addendum &ndash; Updated ILMerge Link

As noted by Micah Gray in
his [comment below](https://thebuildingcoder.typepad.com/blog/2017/06/handling-third-party-library-dll-conflicts.html#comment-4983002843):

> The ILMerge link above no longer works.
It appears to have moved over to
the [GitHub ILMerge repository](https://github.com/dotnet/ILMerge).
