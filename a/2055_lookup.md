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


twitter:

 @AutodeskRevit #RevitAPI #BIM @DynamoBIM

How to handle problems with solid Boolean operations?
&ndash; Revit Booleans and OpenCascade
&ndash; CGAL solid Booleans...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### RevitLookup 2025.0.9


####<a name="2"></a> RevitLookup 2025.0.9

Roman [@Nice3point](https://t.me/nice3point) Karpovich, aka Роман Карпович,
published [RevitLookup] (https://github.com/jeremytammik/RevitLookup)
[release 2025.0.9](https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.9)
with important enhancements by himself,
[RichardPinka](https://github.com/RichardPinka) and [SergeyNefyodov](https://github.com/SergeyNefyodov):

- [Revit.ini file editor](#2.1)
- [Dependency conflict static analyzer](#2.2)
- [Public RevitLookup roadmap](#2.3)

####<a name="2.1"></a> Revit.ini File Editor

The **Revit.ini** file is a key configuration file in Revit that stores settings related to user preferences, system behavior, and project defaults.

The **Revit.ini File Editor** provides a simple and efficient way to manage these settings without the need for manual editing.
With this tool, users can quickly adjust Revit’s configurations to match project needs or personal preferences, making it an essential utility for both professionals and teams working with Revit.

/Users/jta/a/doc/revit/tbc/git/a/img/revitlookup_2025_0_9_1.png

![image](https://github.com/user-attachments/assets/701a0a97-1906-419d-950b-b70f9b852966)

This is our first public version, and we are excited for you to try it out for yourself!
Make sure to file issues you encounter on our GitHub so we can continue to improve it.
For more details, please refer to the:

- [Revit.ini file editor documentation](https://github.com/jeremytammik/RevitLookup/wiki/Revit.ini-File-Editor)

####<a name="2.2"></a> Dependency Conflict Static Analyzer

Some users experience issues launching RevitLookup, often caused by conflicts with third-party plugins, cf., [issue 269](https://github.com/jeremytammik/RevitLookup/issues/269).

To help resolve these issues, we've introduced new dependency reporting tools that allow you to analyze, identify and upgrade problematic plugins causing crashes:

/Users/jta/a/doc/revit/tbc/git/a/img/revitlookup_2025_0_9_2.png

![image](https://github.com/user-attachments/assets/a5f94bd8-7eca-4998-91c7-99d7b079fa47)

- [Download DependenciesReport tool](https://github.com/jeremytammik/RevitLookup/issues/269#issuecomment-2323309590)

Many thanks to @RichardPinka for testing tools in the discussion of [issue 281](https://github.com/jeremytammik/RevitLookup/issues/281).

####<a name="2.3"></a> Public RevitLookup Roadmap

Curious about what’s next?
Stay updated on the latest developments for RevitLookup and share your feedback.

Check out the [Public RevitLookup Roadmap](https://github.com/users/jeremytammik/projects/1) to see what’s coming up in future releases.

/Users/jta/a/doc/revit/tbc/git/a/img/revitlookup_2025_0_9_3.png

![image](https://github.com/user-attachments/assets/14c3479d-871a-4f32-a933-a4b365e566bc)

####<a name="2.4"></a> Other Improvements

**New extensions**:

By @SergeyNefyodov:L

- Type &ndash; Extension name &ndash; Description

| Pipe        | HasOpenConnector                | Checks if there is open piping connector for the pipe.            | https://github.com/jeremytammik/RevitLookup/pull/261
| Family      | FamilyCanConvertToFaceHostBased | Indicates whether the family can be converted to face host based. | https://github.com/jeremytammik/RevitLookup/pull/263
| Family      | GetProfileSymbols               | Gets the profile Family Symbols.                                  | https://github.com/jeremytammik/RevitLookup/pull/263
| Document    | GetLightFamily                  | Creates a light family object from the family document.           | https://github.com/jeremytammik/RevitLookup/pull/266
| LightFamily | GetLightTypeName                | Return the name for the light type.                               | https://github.com/jeremytammik/RevitLookup/pull/266
| LightFamily | GetLightType                    | Return a LightType object for the light type.                     | https://github.com/jeremytammik/RevitLookup/pull/266
| Application | GetMacroManager                 | Gets the Macro manager from the application.                      | https://github.com/jeremytammik/RevitLookup/pull/268
| Document    | GetMacroManager                 | Gets the Macro manager from the document.                         | https://github.com/jeremytammik/RevitLookup/pull/268

**New API support**:

- [**CylindricalFace** class support](https://github.com/jeremytammik/RevitLookup/issues/264):
    - Radius property support
- [**StructuralSettings** class support](https://github.com/jeremytammik/RevitLookup/pull/282) by @SergeyNefyodov:
    - GetStructuralSettings method support
- [**StructuralSettings** class support](https://github.com/jeremytammik/RevitLookup/pull/283) by @SergeyNefyodov:
    - GetActiveSunAndShadowSettings method support
    - GetSunrise method support
    - GetSunset method support
    - GetSunset method support
    - IsTimeIntervalValid method support
    - IsAfterStartDateAndTime method support
    - IsBeforeEndDateAndTime method support
- [**RevisionNumberingSequence** class support](https://github.com/jeremytammik/RevitLookup/pull/289) by @SergeyNefyodov:
    - GetAllRevisionNumberingSequences method support
- [**AnalyticalLinkType** class support](https://github.com/jeremytammik/RevitLookup/pull/288) by @SergeyNefyodov:
    - IsValidAnalyticalFixityState method support
- [**AreaVolumeSettings** class support](https://github.com/jeremytammik/RevitLookup/pull/287) by @SergeyNefyodov:
    - GetAreaVolumeSettings method support
    - GetSpatialElementBoundaryLocation method support

**New default settings:**

- `Show Static` members enabled by default
- `Show Events` enabled by default
- `Show Extensions` enabled by default

**Bugs**

- [Fixed missing quick access icon](https://github.com/jeremytammik/RevitLookup/issues/267)
- [Fixed DataGrid accent color](https://github.com/jeremytammik/RevitLookup/issues/273)

**Miscellaneous**

- Updated **Contributing** guide.
- Added a new GitHub **issue templates**.
- [Full changelog](https://github.com/jeremytammik/RevitLookup/compare/2025.0.8...2025.0.9)
- [RevitLookup versioning](https://github.com/jeremytammik/RevitLookup/wiki/Versions)






Revit.ini File Editor

The Revit.ini file is a key configuration file in Revit that stores settings related to user preferences, system behavior, and project defaults.

The Revit.ini File Editor provides a simple and efficient way to manage these settings without the need for manual editing.
With this tool, users can quickly adjust Revit’s configurations to match project needs or personal preferences, making it an essential utility for both professionals and teams
working with Revit.

default.png (view on web)

This is our first public version, and we are excited for you to try it out for yourself!
Make sure to file issues you encounter on our GitHub so we can continue to improve it.

Documentation: https://github.com/jeremytammik/RevitLookup/wiki/Revit.ini-File-Editor

Static dependency conflict analyzer

Some users experience issues launching RevitLookup, often caused by conflicts with third-party plugins (see issue #269).

To help resolve these issues, we've introduced new tools that allow you to analyze, identify and upgrade problematic plugins causing crashes.

default.png (view on web)

Download: #269 (comment)

Many thanks to @RichardPinka for testing tools in the discussion: #281

Public RevitLookup roadmap

Curious about what’s next? Stay updated on the latest developments for RevitLookup and share your feedback.

Check out our Public Roadmap to see what’s coming up in future releases.: https://github.com/users/jeremytammik/projects/1

default.png (view on web)

Improvements

New extensions:

Type  Extension Description Author
Pipe  HasOpenConnector  Checks if there is open piping connector for the pipe.  #261 by @SergeyNefyodov
Family  FamilyCanConvertToFaceHostBased Indicates whether the family can be converted to face host based. #263 by @SergeyNefyodov
Family  GetProfileSymbols Gets the profile Family Symbols.  #263 by @SergeyFyodorov
Document  GetLightFamily  Creates a light family object from the family document. #266 by @SergeyNefyodov
LightFamily GetLightTypeName  Return the name for the light type. #266 by @SergeyNefyodov
LightFamily GetLightType  Return a LightType object for the light type. #266 by @SergeyNefyodov
Application GetMacroManager Gets the Macro manager from the application.  #268 by @SergeyNefyodov
Document  GetMacroManager Gets the Macro manager from the document. #268 by @SergeyNefyodov
New API support:

CylindricalFace class support #264:
Radius property support
StructuralSettings class support #282 by @SergeyNefyodov:
GetStructuralSettings method support
StructuralSettings class support #283 by @SergeyNefyodov:
GetActiveSunAndShadowSettings method support
GetSunrise method support
GetSunset method support
GetSunset method support
IsTimeIntervalValid method support
IsAfterStartDateAndTime method support
IsBeforeEndDateAndTime method support
RevisionNumberingSequence class support #289 by @SergeyNefyodov:
GetAllRevisionNumberingSequences method support
AnalyticalLinkType class support #288 by @SergeyNefyodov:
IsValidAnalyticalFixityState method support
AreaVolumeSettings class support #287 by @SergeyNefyodov:
GetAreaVolumeSettings method support
GetSpatialElementBoundaryLocation method support
New default settings:

Show Static members enabled by default
Show Events enabled by default
Show Extensions enabled by default
Bugs

Fixed missing quick access icon #267
Fixed DataGrid accent color #273
Mics

Updated Contributing guide.
Added a new GitHub issue templates.
Full changelog: 2025.0.8...2025.0.9
RevitLookup versioning: https://github.com/jeremytammik/RevitLookup/wiki/Versions


**Question:**


**Answer:**


<center>
<img src="img/.png" alt="" title="" width="300"/>
</center>


<pre><code class="language-cs"></code></pre>


