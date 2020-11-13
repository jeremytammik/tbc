<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


twitter:

 #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Revit Add-in Dotnet Template and Prism Goodies

Today, Diego Rossi shares with us two useful GitHub repositories, saying:

In my spare time, I do R &amp; D projects for Revit-related things when the company I work for doesn't have me push through new features or just during weekends without pending hobby projects.

####<a name="2"></a> External Application Accessing {rism Goodies

Recently I really had to rely on [Prism](https://prismlibrary.com),
a common library among WPF developers which aids in the development of modular XAML/WPF apps following the MVVM paradigm to modularize C# ViewModels and XAML Views.

Since Revit does not rely on the concept of an always-running application instance for an add-in but rather on the custom-made `IExternalApplication` interface, it does not allow Prism to work without porting its features over.

I open sourced a project I worked on,
[HellPie.Revit.PrismDemo](https://github.com/HellPie/HellPie.Revit.PrismDemo),
to showcase how to include Prism in a Revit external application add-in that allows access to the full range of goodies and features that come with using Prism, MVVM and the Inversion of Control paradigms.


####<a name="3"></a> Revit Add-in Dotnet Template

Today, I further delved into how to make my job easier; after having to develop sort of per-project new add-ins for the past 2 years, I got around to
trying [DotNet Templates](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates),
a new feature introduced in the dotnet tooling released with .NET Core and now available also with .NET 5.0.

The [HellPie/HellPie.RevitTemplates GitHub repository](https://github.com/HellPie/HellPie.RevitTemplates) showcases
a Template Package which gets compiled to a NuGet package via `dotnet pack` and then installed as a Template Package using `dotnet new --install path/to/package.nuget`.
There is a prebuilt NuGet package in the Releases of the repository.

This one took me quite some time, but I got it to work making use of the very powerful templating system which allows optional and mandatory parameters to be collected upon running `dotnet new`.
Help is available through `dotnet new <template name> --help`.
It allows to configure a Visual Studio Solution with and without NUnit test projects (I did not get around to implementing deeper integration as I do not have experience automating Geberit's Revit Runner).

This project also uses the latest SDK-style Project files, which were also introduced with .NET Core but do allow specifying any kind of valid Target Framework, including any old (and new) version of .NET Framework.
The template even shows some documentation for which .NET Framework version should be used based on the desired target Revit version (defaults to Revit 2021 and .NET Framework 4.8, can be customized with the flags shown in `dotnet new revit-api --help`).

Here is a screenshot of what PowerShell shows for the help command and a couple example commands to run below:

<center>
<img src="img/dotnet_new_help.png" alt="Dotnet new help" title="Dotnet new help" width="600"/> <!-- 1160 -->
</center>

- Create a new Solution with namespace Demo.Namespace targeting Revit 2019 and .NET Framework 4.8, without Unit Tests, without EditorConfig and without README and LICENSE files:
<pre>
dotnet new revit-api --name "Demo.Namespace" --AddInName "Demo AddIn" --AddInDescription "AddIn Description for .addin File" --VendorDescription "Vendor (vendor@email)" --VendorID "example.vendor" --Framework net4.8 --Revit 2019 --Tests=false --Repository=false --EditorConfig=false
</pre>
- Create a new Solution with default settings and only mandatory fields are defined:
<pre>
dotnet new revit-api --name Custom.Namespace --AddInName "Demo AddIn" --AddInDescription "AddIn Description for .addin File" --VendorDescription "Vendor (vendor@email)" --VendorID "example.vendor"
</pre>
- The same but using short flags:
<pre>
dotnet new revit-api -n Custom.Namespace -A "Demo AddIn" -Ad "AddIn Description for .addin File" -Ve "Vendor (vendor@email)" -V "example.vendor"
</pre>

PLease mention these two projects in one of your blog posts, not for the clout, but first and foremost to let people know about them, so that they can hopefully contribute improvements and changes (more templates and maybe better templates too).

Have a nice day,

Diego Rossi

Many thanks to Diego for sharing this work, and good luck to all making use of and contributing to it.
