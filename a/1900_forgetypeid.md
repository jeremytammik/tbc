<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- revit 2022 migrate ParameterType
  https://autodesk.slack.com/archives/C0SR6NAP8/p1615984937008800
  https://wiki.autodesk.com/pages/viewpage.action?spaceKey=aeceng&title=Revit+API+Changes+2022
  ForgeTypeId how to use https://forums.autodesk.com/t5/revit-api-forum/forgetypeid-how-to-use/td-p/9439210
  Revit 2022: ParameterType.Text to ForgeTypeId https://forums.autodesk.com/t5/revit-api-forum/revit-2022-parametertype-text-to-forgetypeid/m-p/10225741
  https://forums.autodesk.com/t5/revit-api-forum/revit-2022-parametertype-text-to-forgetypeid/m-p/10227398

- josiah comment
  https://thebuildingcoder.typepad.com/blog/2018/06/multi-targeting-revit-versions-cad-terms-texture-maps.html#comment-5339799009
  Multi-Target 2021 and 2022 Using MSBuild
  https://forums.autodesk.com/t5/revit-api-forum/multi-target-2021-and-2022-using-msbuild/m-p/10235037

- PDF Export fails with Paper Format set as Default with other parameters
  Revit 2022 PDF Export Fails with Paper Format set as Default with Other Parameters
  https://forums.autodesk.com/t5/revit-api-forum/revit-2022-pdf-export-fails-with-paper-format-set-as-default/m-p/10223281

- 2022 PDF exporter cant use "sheet number" parameter
https://forums.autodesk.com/t5/revit-api-forum/2022-pdf-exporter-cant-use-quot-sheet-number-quot-parameter/m-p/10220287

- https://www.freecodecamp.org/news/common-mistakes-beginning-web-development-students-make/
  5 Mistakes Beginner Web Developers Make – And How to Fix Them
  Avoid spaces in file names
  Respect case sensitivity
  Understand file paths
  Name the default page `index`
  Take a break

twitter:

Using built-in PDF export and ForgeTypeId in Revit 2022, multi-target 2021 and 2022 using MSBuild for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://autode.sk/forgetypeid

Revit 2022 has been released.
Two important features are parameter API enhancements and built-in PDF export functionality
&ndash; Replace deprecated <code>ParameterType</code> with <code>ForgeTypeId</code>
&ndash; Multi-target 2021 and 2022 using MSBuild
&ndash; PDF export default paper format can fail
&ndash; PDF export output file naming
&ndash; Five beginner mistakes...

linkedin:

Using built-in PDF export and ForgeTypeId in Revit 2022, multi-target 2021 and 2022 using MSBuild for the #RevitAPI

http://autode.sk/forgetypeid

Revit 2022 has been released.
Two important features are parameter API enhancements and built-in PDF export functionality:

- Replace deprecated ParameterType with ForgeTypeId
- Multi-target 2021 and 2022 using MSBuild
- PDF export default paper format can fail
- PDF export output file naming
- Five beginner mistakes...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### PDF Export, ForgeTypeId and Multi-Target Add-In

This is blog post number 1900, just fyi,
cf. [The Building Coder index and table of contents](http://jeremytammik.github.io/tbc/a/#7).

[Revit 2022 has been released](https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-released.html) and
the time has come to migrate to the new version.

Updates for [RevitLookup](https://github.com/jeremytammik/RevitLookup),
the [Visual Studio Revit add-in wizards](https://github.com/jeremytammik/VisualStudioRevitAddinWizard)
and [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) are
in the works and not done yet... one-man-band lagging...
all Revit 2021 add-ins should work just fine in Revit 2022 as well, though.

Two important features are the parameter API enhancements and built-in PDF export functionality.
Initial issues with these two have already been discussed:

- [Replace deprecated `ParameterType` with `ForgeTypeId`](#2)
- [Multi-target 2021 and 2022 using MSBuild](#3)
- [PDF export default paper format can fail](#4)
- [PDF export output file naming](#5)
- [Five beginner mistakes](#6)

####<a name="2"></a> Replace Deprecated ParameterType with ForgeTypeId

David Becroft of Autodesk 
and Maxim Stepannikov of BIM Planet, aka Максим Степанников or [architect.bim](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4552025),
helped address 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) question
on [Revit 2022 ParameterType.Text to ForgeTypeId](https://forums.autodesk.com/t5/revit-api-forum/revit-2022-parametertype-text-to-forgetypeid/m-p/10225741):

**Question:** Could somebody please help me out with this conversion from the deprecated `ParameterType` to `ForgeTypeId` for the Revit 2022 API?

The 'old' code has a line like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;parameter.Definition.ParameterType&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;==&nbsp;ParameterType.Text&nbsp;)&nbsp;&nbsp;...
</pre>

What would be the 2022 equivalent for it?

It seems that the left side may be:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;parameter.Definition.GetDataType()&nbsp;==&nbsp;????)&nbsp;&nbsp;....
</pre>

But, for some reason, I cannot find what I have to use on right side of the operation... there must be something I am overlooking.

**Answer:** To perform this check you need to create instance of ForgeTypeId class. Use one of the SpecTypeId's properties to get value to compare with. In your case (for text parameter) you need Number property:

<pre class="prettyprint">
>>> element.Parameter[BuiltInParameter.ALL_MODEL_MARK].Definition.GetSpecTypeId() == SpecTypeId.Number
True
</pre>

 Also take a look at the conversation
 on [how to use `ForgeTypeId`](https://forums.autodesk.com/t5/revit-api-forum/forgetypeid-how-to-use/td-p/9439210).
 
**Response:** Oh dear!
This is super confusing.
I've just checked and parameter definitions that store TEXT have a `SpecTypeId` = `SpecTypeId.Number`.

Now how do you distinguish between actual Numbers and Text?

It looks like the problem is this:

The `Autodesk.Revit.DB.InternalDefinition` class has:

- `ParameterType` &ndash; this can be: Invalid | Text | Integer | Number
- `UnitType` &ndash; seemingly, for Text type parameters, this is `UT_Number`

Now with `ParameterType` becoming obsolete, we have to use `Parameter.GetSpecTypeId` that SEEMINGLY corresponds to the `UnitType` member above, and for Text parameters like `Comment`, it has a value of `SpecTypeId.Number`!

The question is: How can I know if a Parameter is Text or not in Revit 2022 &ndash; without using Definition.ParameterType ?

Same would apply to `YesNo` type parameters... `SpecTypeId` cannot be used to determine if a `ParameterDefinition` is for a `YesNo` type parameter...  

And even more: The ONLY place where I can see if a parameter is a YesNo parameter is in the Parameter.Definition.ParameterType !! If ParameterType is obsolete... how to determine if a parameter is YesNo or something else?

**Answer:** It is a well-known fact that unit type of text is number.
Actually, I don't know why &nbsp; :-) &nbsp;
But you definitely should use the `Number` property.
Each Parameter object also has a `StorageType` property.
In case of a Yes/No parameter, its value is `Integer`.
In case of Text parameter, `String`.
I hope this solves your problem.

**Response:** Well, sorry for my ignorance but seemingly there is still one 'minor' issue left for me to be answered:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;option&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;ExternalDefinitionCreationOptions(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;ExampleParamForge&quot;</span>,&nbsp;SpecTypeId.XXX&nbsp;???);
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;definition&nbsp;=&nbsp;definitionGroup.Definitions.Create(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;option&nbsp;);
</pre>

Using SpecTypeId.Number creates a Parameter that has the "Type of Parameter" set to Number (obviously!, right?)

How do I create a simple TEXT Parameter definition using the Revit 2022 API?

There must be something here that I am missing.

**Answer:** To create a text parameter, please use `SpecTypeId.String.Text`.

For context, the `ForgeTypeId` properties directly in the `SpecTypeId` class identify the measurable data types, like `SpecTypeId.Length` or `SpecTypeId.Mass`.
The non-measurable data types are organized into nested classes within `SpecTypeId`, like `SpecTypeId.String.Text`, `SpecTypeId.Boolean.YesNo`, `SpecTypeId.Int.Integer`, or `SpecTypeId.Reference.Material`.

Regarding text parameters that report their type as "Number", here's the history:

- Prior to Revit 2021, a `Definition` had a `UnitType` and a `ParameterType`.
  The `UnitType` property was only meaningful for parameters with measurable `ParameterType` values, and a parameter with `ParameterType.Text` would report a meaningless `UnitType.Number` value.
- Revit 2021 deprecated the `UnitType` property and replaced it with the `GetSpecTypeId` method.
  But the behaviour remained the same &ndash; a parameter with `ParameterType.Text` would have `GetSpecTypeId` == `SpecTypeId.Number`.
- Revit 2022 deprecated the `ParameterType` property and the `GetSpecTypeId` method, replacing them both with the `GetDataType` method.
  A parameter with `ParameterType.Text` will report `GetDataType()` == `SpecTypeId.String.Text`.
  Side note: The `GetDataType` method can also return a category identifier, indicating a Family Type parameter of that category.

Many thanks to Maxim and David for their clarification!

####<a name="3"></a> Multi-Target 2021 and 2022 Using MSBuild

Josiah Offord very kindly shared his solution to implement a multi-target add-in for several releases of Revit
using [the Microsoft Build Engine MSBuild](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild) in both
a [comment](https://thebuildingcoder.typepad.com/blog/2018/06/multi-targeting-revit-versions-cad-terms-texture-maps.html#comment-5339799009)
on [multi-targeting Revit Versions using `TargetFrameworks`](https://thebuildingcoder.typepad.com/blog/2018/06/multi-targeting-revit-versions-cad-terms-texture-maps.html#2) and in a dedicated [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [multi-targeting 2021 and 2022 using `MSBuild`](https://forums.autodesk.com/t5/revit-api-forum/multi-target-2021-and-2022-using-msbuild/m-p/10235037):

For those interested, you can configure a `.csproj` to multi-target both 2021 and 2022 on .NET Framework 4.8 using MSBuild.
I posted this on a comment in The Building Coder blog and figured it'd be useful here too.

The first task is to choose what .NET 4.8 add-in you want to actively program against.
There can only be one active add-in per .NET framework as far as I know.
In this example, I'm setting my default to Revit 2022 using the custom `RevitVersion` property if it hasn't been configured yet.

<pre class="code">
<span style="color:blue;">&lt;</span><span style="color:#a31515;">Project</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Sdk</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">Microsoft.NET.Sdk.WindowsDesktop</span>&quot;<span style="color:blue;">&nbsp;</span><span style="color:red;">InitialTargets</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">Test</span>&quot;<span style="color:blue;">&gt;</span>
&nbsp;&nbsp;...
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">TargetFrameworks</span><span style="color:blue;">&gt;</span>net461;net47;net472;net48<span style="color:blue;">&lt;/</span><span style="color:#a31515;">TargetFrameworks</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Configurations</span><span style="color:blue;">&gt;</span>Debug;Release<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Configurations</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>bin\$(Configuration)\<span style="color:blue;">&lt;/</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">UseWindowsForms</span><span style="color:blue;">&gt;</span>true<span style="color:blue;">&lt;/</span><span style="color:#a31515;">UseWindowsForms</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">RevitVersion</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(RevitVersion)&#39;&nbsp;==&nbsp;&#39;&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>2022<span style="color:blue;">&lt;/</span><span style="color:#a31515;">RevitVersion</span><span style="color:blue;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;...
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&gt;</span>
</pre>

 Next, define configurations changes per each version.

<pre class="code">
<span style="color:blue;">&lt;</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net461&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">PlatformTarget</span><span style="color:blue;">&gt;</span>x64<span style="color:blue;">&lt;/</span><span style="color:#a31515;">PlatformTarget</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">DefineConstants</span><span style="color:blue;">&gt;</span>DEBUG;REVIT2018<span style="color:blue;">&lt;/</span><span style="color:#a31515;">DefineConstants</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>bin\$(Configuration)\2018\<span style="color:blue;">&lt;/</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
<span style="color:blue;">&lt;</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net47&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">PlatformTarget</span><span style="color:blue;">&gt;</span>x64<span style="color:blue;">&lt;/</span><span style="color:#a31515;">PlatformTarget</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">DefineConstants</span><span style="color:blue;">&gt;</span>$(DefineConstants);REVIT2019<span style="color:blue;">&lt;/</span><span style="color:#a31515;">DefineConstants</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>bin\$(Configuration)\2019<span style="color:blue;">&lt;/</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
<span style="color:blue;">&lt;</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net472&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">PlatformTarget</span><span style="color:blue;">&gt;</span>x64<span style="color:blue;">&lt;/</span><span style="color:#a31515;">PlatformTarget</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">DefineConstants</span><span style="color:blue;">&gt;</span>$(DefineConstants);REVIT2020<span style="color:blue;">&lt;/</span><span style="color:#a31515;">DefineConstants</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>bin\$(Configuration)\2020\<span style="color:blue;">&lt;/</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
<span style="color:blue;">&lt;</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net48&#39;&nbsp;And&nbsp;&#39;$(RevitVersion)&#39;&nbsp;==&nbsp;&#39;2021&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">PlatformTarget</span><span style="color:blue;">&gt;</span>x64<span style="color:blue;">&lt;/</span><span style="color:#a31515;">PlatformTarget</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">DefineConstants</span><span style="color:blue;">&gt;</span>$(DefineConstants);REVIT2021<span style="color:blue;">&lt;/</span><span style="color:#a31515;">DefineConstants</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>bin\$(Configuration)\2021<span style="color:blue;">&lt;/</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&gt;</span>
 
<span style="color:blue;">&lt;</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net48&#39;&nbsp;And&nbsp;&#39;$(RevitVersion)&#39;&nbsp;==&nbsp;&#39;2022&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">PlatformTarget</span><span style="color:blue;">&gt;</span>x64<span style="color:blue;">&lt;/</span><span style="color:#a31515;">PlatformTarget</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">DefineConstants</span><span style="color:blue;">&gt;</span>$(DefineConstants);REVIT2022<span style="color:blue;">&lt;/</span><span style="color:#a31515;">DefineConstants</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>bin\$(Configuration)\2022<span style="color:blue;">&lt;/</span><span style="color:#a31515;">OutputPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">PropertyGroup</span><span style="color:blue;">&gt;</span>
</pre>

Next, load the proper dll references for each version.

<pre class="code">
<span style="color:blue;">&lt;</span><span style="color:#a31515;">ItemGroup</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net461&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">AdWindows</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2018\AdWindows.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">RevitAPI</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2018\RevitAPI.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">RevitAPIUI</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2018\RevitAPIUI.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">ItemGroup</span><span style="color:blue;">&gt;</span>
 
<span style="color:blue;">&lt;</span><span style="color:#a31515;">ItemGroup</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net47&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">AdWindows</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2019\AdWindows.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">RevitAPI</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2019\RevitAPI.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>False<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">RevitAPIUI</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2019\RevitAPIUI.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">ItemGroup</span><span style="color:blue;">&gt;</span>
 
<span style="color:blue;">&lt;</span><span style="color:#a31515;">ItemGroup</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net472&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">AdWindows</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2020\AdWindows.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">RevitAPI</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2020\RevitAPI.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">RevitAPIUI</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2020\RevitAPIUI.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">ItemGroup</span><span style="color:blue;">&gt;</span>
 
<span style="color:blue;">&lt;</span><span style="color:#a31515;">ItemGroup</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net48&#39;&nbsp;And&nbsp;&#39;$(RevitVersion)&#39;&nbsp;==&nbsp;&#39;2021&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">AdWindows</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2021\AdWindows.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">RevitAPI</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2021\RevitAPI.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">RevitAPIUI</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2021\RevitAPIUI.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">ItemGroup</span><span style="color:blue;">&gt;</span>
 
<span style="color:blue;">&lt;</span><span style="color:#a31515;">ItemGroup</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Condition</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">&nbsp;&#39;$(TargetFramework)&#39;&nbsp;==&nbsp;&#39;net48&#39;&nbsp;And&nbsp;&#39;$(RevitVersion)&#39;&nbsp;==&nbsp;&#39;2022&#39;&nbsp;</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">AdWindows</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2022\AdWindows.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">RevitAPI</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2022\RevitAPI.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Include</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">RevitAPIUI</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>C:\Program&nbsp;Files\Autodesk\Revit&nbsp;2022\RevitAPIUI.dll<span style="color:blue;">&lt;/</span><span style="color:#a31515;">HintPath</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">EmbedInteropTypes</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>false<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Private</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;/</span><span style="color:#a31515;">Reference</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">ItemGroup</span><span style="color:blue;">&gt;</span>
</pre>

At the end, you need to configure an additional build for whatever .NET 4.8 Revit add-in you didn't set as the default above. This is nice because it will catch build errors even though the active .NET 4.8 version is something else.

<pre class="code">
<span style="color:blue;">&lt;</span><span style="color:#a31515;">Target</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Name</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">Test</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Message</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Importance</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">high</span>&quot;<span style="color:blue;">&nbsp;</span><span style="color:red;">Text</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">--&nbsp;Building&nbsp;$(MSBuildProjectFile),&nbsp;TF&nbsp;=&nbsp;$(TargetFramework),&nbsp;Config&nbsp;=&nbsp;$(Configuration),&nbsp;Revit&nbsp;Version&nbsp;=&nbsp;$(RevitVersion)&nbsp;--</span>&quot;<span style="color:blue;">&nbsp;/&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Target</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;</span><span style="color:#a31515;">Target</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Name</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">Build2021</span>&quot;<span style="color:blue;">&nbsp;</span><span style="color:red;">BeforeTargets</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">DispatchToInnerBuilds</span>&quot;<span style="color:blue;">&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">Message</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Importance</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">high</span>&quot;<span style="color:blue;">&nbsp;</span><span style="color:red;">Text</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">***&nbsp;running&nbsp;pre-dispatch&nbsp;builds&nbsp;***</span>&quot;<span style="color:blue;">&nbsp;/&gt;</span>
<span style="color:blue;">&nbsp;&nbsp;&lt;</span><span style="color:#a31515;">MSBuild</span><span style="color:blue;">&nbsp;</span><span style="color:red;">Projects</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">myProject.csproj</span>&quot;<span style="color:blue;">&nbsp;</span><span style="color:red;">Properties</span><span style="color:blue;">=</span>&quot;<span style="color:blue;">Configuration=$(Configuration);TargetFramework=net48;RevitVersion=2021</span>&quot;<span style="color:blue;">&gt;&lt;/</span><span style="color:#a31515;">MSBuild</span><span style="color:blue;">&gt;</span>
<span style="color:blue;">&lt;/</span><span style="color:#a31515;">Target</span><span style="color:blue;">&gt;</span>
</pre>

Side note:
Using a multi-target solution means you need to keep references to all the old versions of Revit.
Be sure to copy out the needed dlls before removing that Revit version from your machine.

Hope this helps.

Thank you very much, Josiah, for this important and timely advice!

####<a name="4"></a> PDF Export Default Paper Format Can Fail

The new built-in PDF export is a certainly a very useful feature in Revit 2022 and has gathered a lot of interest.

Unfortunately, it also caused some misunderstandings, and a first problem was discovered and described in the thread 
on [Revit 2022 PDF export fails with paper format set as default with other parameters](https://forums.autodesk.com/t5/revit-api-forum/revit-2022-pdf-export-fails-with-paper-format-set-as-default/m-p/10223281).

The title says it all, and the development team explain:

By design, if PaperFormat is default, then PaperPlacement should always be Center.
However, there is no restriction ensuring this on the API side.
We should either silently set PaperPlacement to Center during export, or throw  an exception notifying the add-in about this.

Currently, in this case, nothing happens and no warning or error is raised.

####<a name="5"></a> PDF Export Output File Naming

Another thread question
why [2022 PDF exporter can't use the "sheet number" parameter](https://forums.autodesk.com/t5/revit-api-forum/2022-pdf-exporter-cant-use-quot-sheet-number-quot-parameter/m-p/10220287).

Apparently, you have to be sure that the Sheet Number parameter from the "Parameter Type" drop down menu is selected from the Sheet type fields.

Also, if the sheet has no revision the filename, the words 'Current revision' may be inserted into the filename instead.

The development team confirm this fallback behaviour; if the parameter you selected is empty, it will fill the parameter name (Sample Value) in the filename.
 
The sheet number in the parameter set is confusing due to the parameter type.
The designed scenario is: the customer will use this parameter only in either sheet or view, not in both of them.
If you select a mixed type of both view and sheet with this parameter, one parameter will fallback to its name due to its absence in the view type.

####<a name="6"></a> Five Beginner Mistakes

Taking a quick look beyond Revit and .NET development for the desktop, the article
on [5 mistakes beginner web developers make &ndash; and how to fix them](https://www.freecodecamp.org/news/common-mistakes-beginning-web-development-students-make) addresses
topics that are of use in a non-web environment as well, and that I actually adhere to pretty strictly myself on all platforms
&ndash; possibly excepting the last &ndash; I am still practicing that:

- Avoid spaces in file names
- Respect case sensitivity
- Understand file paths
- Name the default page `index`
- Take a break

<center>
<img src="/p/2016/2016-01-03_wildhaus/791_jeremy_reading_cropped.jpg" alt="Taking a break and reading in the Swiss winter sun" width="300">
</center>

<!-- mexico siesta  sombrero -->
