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
</head>

<!---

- OpenCascade for Boolean operations
  https://forums.autodesk.com/t5/revit-api-forum/boolean-operation-fail/m-p/12713966#M78244


twitter:
 the #RevitAPI @AutodeskRevit #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Migrating .NET 4.8 to .NET Core 8

The Revit 2025 API is based on .NET Core 8, a significant upgrade from the previous .NET 4.8 framework underlying the Revit 2024 API and previous:


####<a name="2"></a> Public .NET Core Migration Webinar Recording

The development team held a webinar in January on the topic of the *Autodesk Desktop API .NET Core Migration &ndash; Embracing Modern .NET*.

The one-and-a-quarter-hour [Autodesk desktop API .NET Core 8.0 migration webinar recording](https://youtu.be/RyHx5-CqKZM) has
now been published for public access:

<center>
  <iframe width="560" height="315" src="https://www.youtube.com/embed/RyHx5-CqKZM?si=06RnsnnwMor_M8G2" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</center>

In addition, here is the [presentation slide deck PDF](doc/migrating_to_net_core_8_webinar.pdf).

My colleague Madhukar Moogala also shared the recording together with the AutoCAD migration guide in his article
on [Autodesk Desktop API Update: .NET Core Migration](https://adndevblog.typepad.com/autocad/2024/04/autodesk-desktop-api-update-net-core-migration.html).

####<a name="3"></a> Public .NET Core Migration Webinar Recording

For Revit 2025, the guide on *Migrating From .NET 4.8 to .NET 8* is included as a section in the Revit 2025 API help file *RevitAPI.chm*, provided in
the Revit 2025 SDK that is available from the [Revit developer centre](https://aps.autodesk.com/developer/overview/revit).

I printed it out in PDF format, available in [migrating_to_net_core_8.pdf](doc/migrating_to_net_core_8.pdf).

Better still, though, the `CHM` file contains native `HTML` formatted text.
For your convenience and to facilitate web searches, here it is:

<!--
<html><head>
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  <meta name="Microsoft.Help.SelfBranded" content="true" /><meta name="Language" content="en-us" />
  <meta name="Microsoft.Help.Locale" content="en-us" />
  <meta name="Microsoft.Help.TopicLocale" content="en-us" />
  <link rel="shortcut icon" href="../icons/favicon.ico" />
  <link rel="stylesheet" type="text/css" href="../styles/branding.css" />
  <link rel="stylesheet" type="text/css" href="../styles/branding-en-US.css" />
  <script type="text/javascript" src="../scripts/branding.js"></script>
  <title>Migrating From .NET 4.8 to .NET 8</title>
  <meta name="Title" content="Revit .NET 8 Upgrade Tips" />
  <meta name="Microsoft.Help.Id" content="2db849bc-e193-4919-a96c-cc324cf06f66" />
  <meta name="Microsoft.Help.ContentType" content="Concepts" />
  <meta name="Description" content="Revit 2025 is built on .NET 8. In this release, the Revit API is .NET 8 only, and Revit add-ins need to be recompiled for .NET 8." />
  <link rel="stylesheet" type="text/css" href="../styles/branding-Help1.css" /></head>
-->


  <body onload="SetDefaultLanguage('cs');">
    <input type="hidden" id="userDataCache" class="userDataStyle" />
    <div id="PageHeader" class="pageHeader">Revit 2025 API</div>
    <div class="pageBody">
      <div id="TopicContent" class="topicContent">
        <table class="titleTable"><tr>
          <td class="titleColumn"><h1>Migrating from .NET 4.8 to .NET 8</h1></td></tr></table>
          <p>Revit 2025 is built on .NET 8. In this release, the Revit API is .NET 8 only, and Revit add-ins need to be recompiled for .NET 8.</p><p>The move from .NET 4.8 is to .NET 8 is a relatively large jump. .NET 8 comes from the .NET Core lineage, which has significant differences from .NET 4.8.</p><br /><strong><u>Upgrade Process</u></strong><p>There are many Microsoft documents and tools to help application developers migrate from .NET 4.8 to .NET Core/5/6/7/8. Following is a list of some helpful documents:</p><ul><li>Overview of porting from .NET Framework to .NET document:
      <a href="https://learn.microsoft.com/en-us/dotnet/core/porting/" target="_blank" rel="noopener noreferrer">https://learn.microsoft.com/en-us/dotnet/core/porting/</a></li><li>.NET Upgrade Assistant can help with the project migration:
      <a href="https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview" target="_blank" rel="noopener noreferrer">https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview</a></li><li><a href="https://docs.microsoft.com/en-us/dotnet/standard/analyzers/portability-analyzer" target="_blank" rel="noopener noreferrer">The .NET Portability Analyzer - .NET</a> on C# projects to roughly evaluate how much work is required to make the migration as well as dependencies between the assemblies.
      </li><li>Lists of breaking changes for .NET Core and .NET 5+:
      <a href="https://learn.microsoft.com/en-us/dotnet/core/compatibility/breaking-changes" target="_blank" rel="noopener noreferrer">https://learn.microsoft.com/en-us/dotnet/core/compatibility/breaking-changes</a></li><li>The .NET 8 SDK can be installed from here:
      <a href="https://dotnet.microsoft.com/en-us/download/visual-studio-sdks" target="_blank" rel="noopener noreferrer">https://dotnet.microsoft.com/en-us/download/visual-studio-sdks</a><ul><li>.NET SDK 8.0.100 is used to build the Revit 2025 release.</li><li>The Revit 2025 release will install .NET 8 Windows Desktop Runtime x64 8.0.0.33101.</li></ul></li><li>If you use Visual Studio to build .NET 8 code, you'll need
      <a href="https://visualstudio.microsoft.com" target="_blank" rel="noopener noreferrer">Visual Studio 17.8</a> or later.</li></ul><br /><br /><strong><u>Basic upgrade process for projects</u></strong><br /><br />
      <span class="code"><em>For C# projects (CSPROJ)</em></span><br />
      <ul><li>Convert C# projects to the new SDK-style format: <a href="https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview" target="_blank" rel="noopener noreferrer">https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview</a>
        <ul><li>The .NET Upgrade Assistant can help with the project migration: <a href="https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview" target="_blank" rel="noopener noreferrer">https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview</a></li><li>Convert packages.json into PackageReferences in your CSPROJ. <a href="https://learn.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference" target="_blank" rel="noopener noreferrer">https://learn.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference</a></li></ul></li><li>Update the target framework for your projects from <span class="literal"><em>&lt;TargetFrameworkVersion&gt;</em></span> to <span class="literal"><em>&lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;</em></span><ul><li>You can run the <a href="https://docs.microsoft.com/en-us/dotnet/standard/analyzers/portability-analyzer" target="_blank" rel="noopener noreferrer">.NET Portability Analyzer</a> on C# projects to evaluate how much work is required to make the migration.
      </li><li>The .NET Upgrade Assistant can help with the .NET version migration: <a href="https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview" target="_blank" rel="noopener noreferrer">https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview</a></li><li>

        If your application is a <a href="https://learn.microsoft.com/en-us/dotnet/desktop/wpf/migration/?view=netdesktop-7.0&amp;amp;preserve-view=true" target="_blank" rel="noopener noreferrer">WPF application</a>, then the CSPROJ will need

      <span class="literal"><em>&lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;</em></span> and <span class="literal"><em>
        &lt;UseWPF&gt;true&lt;/UseWPF&gt;</em></span>.</li><li>If your application uses <a href="https://learn.microsoft.com/en-us/dotnet/desktop/winforms/migration/?view=netdesktop-6.0&amp;amp;preserve-view=true" target="_blank" rel="noopener noreferrer">Windows forms</a>, then use <span class="literal"><em>&lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;</em></span> and <span class="literal"><em>&lt;UseWindowsForms&gt;true&lt;/UseWindowsForms&gt;</em></span>.</li></ul></li><li>System references can be removed from the CSPROJ, as they are available by default.</li><li>Then address incompatible packages, library references and obsolete (unsupported) code.</li></ul><br /><br /><span class="code"><em>For C++/CLI projects (VCXPROJ)</em></span><br /><p>Refer to Microsoft's guide for migrating C++/CLI projects to .NET Core/5+: <a href="https://learn.microsoft.com/en-us/dotnet/core/porting/cpp-cli" target="_blank" rel="noopener noreferrer">https://learn.microsoft.com/en-us/dotnet/core/porting/cpp-cli</a></p><ul><li>Replace

            <span class="literal"><em>
              &lt;CLRSupport&gt;true&lt;/CLRSupport&gt;
            </em></span> with
            <span class="literal"><em>
              &lt;CLRSupport&gt;NetCore&lt;/CLRSupport&gt;.
            </em></span>
          This property is often in configuration-specific property groups, so you may need to replace it in multiple places.</li><li>Replace
      <span class="literal"><em>&lt;TargetFrameworkVersion&gt;</em></span>
        A property with <span class="literal"><em>&lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;.</em></span></li><li>Remove any .NET Framework references (like
    <span class="literal"><em>&lt;Reference Include="System" /&gt;</em></span>)
      and add <span class="literal"><em>FrameworkReference</em></span> when needed
    . .NET Core SDK assemblies are automatically referenced when using
    <span class="literal"><em>NetCore </em></span>support.</li><li>Add FrameworkReferences:<ul><li>To use Windows Forms APIs, add this reference to the vcxproj file:<p /><span class="literal"><em>
        &lt;FrameworkReference Include="Microsoft.WindowsDesktop.App.WindowsForms" /&gt;
        </em></span></li><li>To use WPF APIs, add this reference to the vcxproj file:<p /><span class="literal"><em>
        &lt;FrameworkReference Include="Microsoft.WindowsDesktop.App.WPF" /&gt;
        </em></span></li><li>To use both Windows Forms and WPF APIs, add this reference to the vcxproj file:<p /><span class="literal"><em>
        &lt;FrameworkReference Include="Microsoft.WindowsDesktop.App" /&gt;
        </em></span></li></ul></li><li>

      Remove any  <span class="literal"><em>&lt;CompileAsManaged&gt;</em></span> for cpp files. It will be set as NetCore by default. Any other values may cause issues.

    </li><li>Then address incompatible packages, library references and obsolete (unsupported) code.</li></ul><br /><br /><span class="code"><em>Global.json</em></span><br /><p>You may need to set <span class="literal"><em>net8.0-windows</em></span> as the target in your global.json, if you have one. Refer the link for global.json overview: <a href="https://learn.microsoft.com/en-us/dotnet/core/tools/global-json" target="_blank" rel="noopener noreferrer">https://learn.microsoft.com/en-us/dotnet/core/tools/global-json</a></p><br /><br /><strong><u>Component Versions</u></strong><p>Your add-in may avoid instability by matching the version of these key components used by the Revit 2025 release:</p><ul><li>CefSharp<ul><li>

        "cef.redist.x64"
       Version=
        "119.4.3
        "

      </li><li>

        "cef.redist.x86" Version="119.4.3
        "

      </li><li>

        "CefSharp.Wpf.HwndHost" Version="119.4.30"

      </li><li>

        "CefSharp.Common.NetCore" Version="119.4.30"

      </li><li>

        "CefSharp.Wpf.NetCore" Version="119.1.20"

      </li></ul></li><li>Newtonsoft Json<ul><li>

        "Newtonsoft.Json"
       Version=
        "13.0.1"

      </li></ul></li></ul><p><p /></p><strong><u>Supporting Multiple Revit Releases</u></strong><p>A single code base can support older Revit releases on .NET 4.8 as well as Revit 2025 on .NET 8.  <a href="https://forums.autodesk.com/t5/revit-api-forum/optimal-add-in-code-base-approach-to-target-multiple-revit/m-p/12532755/highlight/true#M76622" target="_blank" rel="noopener noreferrer">See this discussion on the Autodesk forums</a> for ideas on configuring your projects and code to support multi-targeting.</p><br /><br /><strong><u>Common Issues</u></strong><p>Here are some common issues you may encounter when upgrading to .NET 8:</p><br /><span class="code"><em>Build Warning MSB3277</em></span><p>
    When building code that references RevitAPI or RevitUIAPI, you will see the <a href="https://learn.microsoft.com/en-us/visualstudio/msbuild/errors/msb3277?view=vs-2022" target="_blank" rel="noopener noreferrer">build warning MSB3277</a>. To fix this, add a reference to the Windows Desktop framework: <span class="literal"><em>&lt;FrameworkReference Include="Microsoft.WindowsDesktop.App"/&gt;</em></span><p /></p><span class="code"><em>
    Build Error CA1416
  </em></span><p>
    If your application uses functions that are only available on Windows systems, you may see a <a href="https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1416" target="_blank" rel="noopener noreferrer">CA1416</a> error. This can be fixed for the project by adding <span class="literal"><em>[assembly: System.Runtime.Versioning.SupportedOSPlatformAttribute("windows")]</em></span> to <span class="literal"><em>AssemblyInfo.cs</em></span>.
  </p><p><p /></p><strong><u>Obsolete Classes and Functions with .NET 8</u></strong><p>
    Your .NET 4.8 application may see compile time or runtime errors if it uses classes or functions that are obsolete or deprecated in .NET Core/5/6/7/8.
  </p><p>
    Lists of breaking changes for .NET Core/5/6/7/8 are here: <a href="https://learn.microsoft.com/en-us/dotnet/core/compatibility/breaking-changes" target="_blank" rel="noopener noreferrer">https://learn.microsoft.com/en-us/dotnet/core/compatibility/breaking-changes</a></p><ul><li><a href="https://learn.microsoft.com/en-us/dotnet/core/compatibility/serialization/5.0/binaryformatter-serialization-obsolete" target="_blank" rel="noopener noreferrer">BinaryFormatter</a> and <a href="https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.formatters.soap.soapformatter?view=netframework-4.8.1" target="_blank" rel="noopener noreferrer">SOAPFormatter</a> are obsolete.
    <ul><li>
      Resource files that contain images or bitmaps will need to be updated as <a href="https://stackoverflow.com/questions/69796653/binaryformatter-not-supported-in-net-5-app-when-loading-bitmap-resource" target="_blank" rel="noopener noreferrer">BinaryFormatter will not be available in .NET 8 to interpret those images/bitmaps</a>.
      </li><li><a href="https://github.com/dotnet/winforms/issues/9701" target="_blank" rel="noopener noreferrer">Windows Forms dialogs using ImageList</a> may need to be updated as BinaryFormatter loads the images for the ImageList.
      </li></ul></li><li><a href="https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/5.0/thread-abort-obsolete" target="_blank" rel="noopener noreferrer">System.Threading.Thread.Abort</a> is obsolete.
    </li><li><a href="https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assemblyname.codebase?view=net-7.0" target="_blank" rel="noopener noreferrer">System.Reflection.AssemblyName.CodeBase</a> and <a href="https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly.codebase?view=net-7.0" target="_blank" rel="noopener noreferrer">System.Reflection.Assembly.CodeBase</a> are deprecated.
    </li><li><a href="https://devblogs.microsoft.com/dotnet/migrating-delegate-begininvoke-calls-for-net-core/" target="_blank" rel="noopener noreferrer">Delegate.BeginInvoke</a> is not supported.
    </li><li><a href="https://github.com/dotnet/winforms/issues/3739" target="_blank" rel="noopener noreferrer">Debug.Assert failure or Debug.Fail</a> silently exits the application by default.
    </li></ul><p><p /></p><strong><u>Assembly Loading</u></strong><p>Your .NET 4.8 application may need updates to help it find and load assemblies:</p><ul><li>.NET 8 uses a different <a href="https://learn.microsoft.com/en-us/dotnet/core/dependency-loading/default-probing" target="_blank" rel="noopener noreferrer">assembly probing approach for DLL loading</a>. When in doubt, try putting the DLL to be loaded in the build output root directory.</li><li>.NET 8 <a href="https://learn.microsoft.com/en-us/dotnet/core/dependency-loading/loading-managed" target="_blank" rel="noopener noreferrer">assembly loading details</a> are different than .NET 4.8.</li><li>.NET 8 projects need <a href="https://learn.microsoft.com/en-us/dotnet/core/runtime-config/" target="_blank" rel="noopener noreferrer">runtimeconfig.json files for many DLLs</a>. The runtimeconfig.json needs to be installed next to the matching DLL, and it configures the behavior of that DLL. These files can be created with <span class="literal"><em>

        &lt;GenerateRuntimeConfigurationFiles&gt;true&lt;/GenerateRuntimeConfigurationFiles&gt;</em></span></li><li>.NET 8 projects will create <span class="literal"><em>deps.json</em></span> files for many DLLs. These <span class="literal"><em>deps.json</em></span> files can be deleted if dependencies are placed in the <a href="https://learn.microsoft.com/en-us/dotnet/core/dependency-loading/default-probing" target="_blank" rel="noopener noreferrer">same directory as the application</a>. These files can be deleted with <span class="literal"><em>

        &lt;GenerateDependencyFile
        &gt;false&lt;/GenerateDependencyFile&gt;

      </em></span></li></ul><p><p /></p><strong><u>Assembly Properties</u></strong><br /><p>After updating your application to .NET 8, you may see build errors for your assembly properties. Many assembly properties are now auto-generated and <a href="https://learn.microsoft.com/en-us/dotnet/architecture/modernize-desktop/example-migration#assemblyinfo-considerations" target="_blank" rel="noopener noreferrer">can be removed from AssemblyInfo.cs</a>.</p><p><p /></p><strong><u>Double Numbers To String</u></strong><br /><br /><p>If you have unit tests or integration tests that compare doubles as strings, they may fail when you upgrade to .NET 8. This is because the number of decimal places printed by <em>ToString()</em> for doubles is <a href="https://devblogs.microsoft.com/dotnet/floating-point-parsing-and-formatting-improvements-in-net-core-3-0/" target="_blank" rel="noopener noreferrer">different in .NET 4.8 and .NET 8</a>. You can call <em>ToString("G15")</em> when converting doubles to strings to use the old .NET 4.8 formatting.</p><p><p /></p><strong><u>String.Compare</u></strong><p>
    String.Compare behavior has changed, see <a href="https://learn.microsoft.com/en-us/dotnet/core/extensions/globalization-icu" target="_blank" rel="noopener noreferrer">.NET globalization and ICU</a> and <a href="https://learn.microsoft.com/en-us/dotnet/core/extensions/globalization-icu#use-nls-instead-of-icu" target="_blank" rel="noopener noreferrer">Use Globalization and ICU</a>.
  </p><p><p /></p><strong><u>
    Windows Dialogs May Change Appearance
  </u></strong><p>
    Your dialogs may change appearance with .NET 8.
  </p><ul><li><a href="https://github.com/dotnet/winforms/issues/9293" target="_blank" rel="noopener noreferrer">WinForms dialogs experience UI layout changes.</a> The workaround is to set Scale(new SizeF(1.0F, 1.0F)); in the dialog constructor.</li><li>The dialog <a href="https://learn.microsoft.com/en-us/dotnet/core/compatibility/winforms#default-control-font-changed-to-segoe-ui-9-pt" target="_blank" rel="noopener noreferrer">default font changed from "Microsoft Sans Serif 8 pt" to "Segoe UI"</a>. This can change dialog appearance and spacing.</li></ul><p><p /></p><strong><u>Process.Start() May Fail</u></strong><p>
    If your application is having trouble starting new processes, this may be because

    <a href="https://github.com/dotnet/core/issues/4109" target="_blank" rel="noopener noreferrer">System.Diagnostics.Process.Start(url) has a behavior change</a>. The <a href="https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.useshellexecute?view=net-7.0" target="_blank" rel="noopener noreferrer">ProcessStartInfo.UseShellExecute Property</a> defaults to <span class="literal"><em>true </em></span>in
    .NET 4.8 and <span class="literal"><em>false</em></span> in .NET 8.  Set <span class="literal"><em>UseShellExecute=true</em></span> to workaround this change.<p /></p><br /><strong><u>Encoding.Default Behaves Differently in .NET 8</u></strong><p>If your application is having problems getting the text encoding used by Windows, it may be because <span class="literal"><em>Encoding.Default</em></span> behaves differently in .NET 8. In .NET 4.8 <span class="literal"><em>Encoding.Default</em></span> would get the system's active code page, but in .NET Core/5/6/7/8
    <a href="https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.default?view=net-7.0" target="_blank" rel="noopener noreferrer">Encoding.Default is always UTF8</a>.
  </p><br /><strong><u>Items Order Differently in Sorted Lists</u></strong><p>If you see different orderings of items in sorted lists after updating to .NET 8, this may be because
    <a href="https://github.com/microsoft/dotnet/blob/main/Documentation/compatibility/list_sort-algorithm-changed.md" target="_blank" rel="noopener noreferrer">List&lt;T&gt;.Sort() behaves differently</a> in .NET 8 than .NET 4.8. The change fixes a .NET 4.8 bug which affected <span class="literal"><em>Sort()</em></span> of items of equal value.<p /></p><br /><br /><strong><u>System.ServiceModel</u></strong><br /><p>

    System.ServiceModel has been ported to .NET Core through <a href="https://dotnet.microsoft.com/en-us/platform/support/policy/corewcf" target="_blank" rel="noopener noreferrer">CoreWCF</a>, which is now available through Nuget packages. There are various changes, including <em>&lt;System.ServiceModel&gt;</em>not being supported in configuration files.

  </p><p><p /></p><strong><u>

    C# Language Updates

  </u></strong><p>

    If you are building code from .NET 4.8 in .NET 8, you may see build errors or warnings about C# nullable types.

  </p><p><a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types" target="_blank" rel="noopener noreferrer">C# has introduced nullable value types</a> and <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types" target="_blank" rel="noopener noreferrer">nullable reference types</a>.


    Prior to .NET 6, new projects used the default <span class="literal"><em>&lt;Nullable&gt;disable&lt;/Nullable&gt;</em></span>. Beginning with .NET 6, new projects include the <span class="literal"><em>&lt;Nullable&gt;enable&lt;/Nullable&gt;</em></span> element in the project file.

  </p><p>

    You can set <span class="literal"><em>&lt;Nullable&gt;disable&lt;/Nullable&gt;</em></span> if you want to revert to .NET 4.8 behavior.

  </p><p><p /></p><strong><u>Environmental Variables</u></strong><br /><p>If you use managed .NET to run native C++ code, be aware that environmental variables, including the path variable for DLL loading, are not shared from managed .NET code with native C++ code.</p></div></div><div id="PageFooter" class="pageFooter"><div class="feedbackLink">Send comments on this topic to
        <a id="HT_MailLink" href="mailto:revitapifeedback%40autodesk.com?Subject=Revit%202025%20API">Autodesk</a></div>
        <script type="text/javascript">
        var HT_mailLink = document.getElementById("HT_MailLink");
        HT_mailLink.href += ": " + document.title + "\u0026body=" + encodeURIComponent("Your feedback is used to improve the documentation and the product. Your e-mail address will not be used for any other purpose and is disposed of after the issue you report is resolved. While working to resolve the issue that you report, you may be contacted via e-mail to get further details or clarification on the feedback you sent. After the issue you report has been addressed, you may receive an e-mail to let you know that your feedback has been addressed.");
        </script></div></body></html>
