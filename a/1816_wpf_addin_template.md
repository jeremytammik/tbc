<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/winforms-or-wpf/m-p/9284061

twitter:

WPF versus WinForms in Revit add-ins and a WPF template for the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/wpftemplate

The long-standing topic of WinForms versus WPF for Revit add-in user interface seems to be nearing a conclusion
&ndash; WinForms or WPF?
&ndash; Revit WPF template
&ndash; Readme
&ndash; Build
&ndash; Customize
&ndash; Documentation...

linkedin:

WPF versus WinForms in Revit add-ins and a WPF template for the #RevitAPI 

http://bit.ly/wpftemplate

The long-standing topic of WinForms versus WPF for Revit add-in user interface seems to be nearing a conclusion:

- WinForms or WPF?
- Revit WPF template
- Readme
- Build
- Customize
- Documentation...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Revit WPF Add-Ins and Template

The long-standing topic of WinForms versus WPF for Revit add-in user interface seems to be nearing a conclusion:

- [WinForms or WPF?](#2)
- [Revit WPF template](#3)
- [Revit WPF template readme](#4)
    - [Build](#4.1)
    - [Customize](#4.2)
    - [Documentation](#4.3)

#### <a name="2"></a>WinForms or WPF?

An extensive discussion in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) on the question of
using [WinForms or WPF](https://forums.autodesk.com/t5/revit-api-forum/winforms-or-wpf/m-p/9284061) in
Revit add-ins is pretty clearly recommending WPF as the better choice, for various reasons:

- WPF is better for dynamic UIs
- The WPF binding mechanisms work well
- WinForms has [serious scaling issues on high resolution monitors](https://thebuildingcoder.typepad.com/blog/2019/09/scaling-an-add-in-for-a-4k-high-resolution-screen.html)
- WPF apps don't have scaling issues
- WPF UIs are built in a modern way with separate style, XML layout, and code / logic documents
    - Similar to how UIs are built on other frameworks like Android / iOS / macOS / web development
    - Better preparation for expanding development knowledge
    - Separation produces cleaner, more flexible, and more reusable code
- WPF looks good, pleasing UI, users enjoy it
    - Styling and dynamic binding nature makes it easier to produce a modern UX
- MVVM is a good feature, specially dealing with objects vs views
- You can dock WPF to a Revit window

The only downside seems to be that many existing samples in the Revit SDK and elsewhere use WinForms.

That said, the Revit IFC open source UI does use WPF, so you could grab all the samples you need from there, if you like.

Here is a pretty fine 56-minute guide for getting started, the [C# WPF UI Tutorial](https://youtu.be/Vjldip84CXQ):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/Vjldip84CXQ" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center>

#### <a name="3"></a>Revit WPF Template

The latest contribution to this thread comes from Micah [kraftwerk15](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4045014) Gray: 

> We were having a conversation on Twitter and had Petr Mitev share a template example of WPF in
the [Revit WPF Template GitHub repository](https://github.com/mitevpi/revit-wpf-template).

> I'm sure there are others out there, but this adds in the Revit context that those building for the Revit API will have to get used to.
Many existing examples will not show how the WPF interacts with the Revit API.

Ever so many thanks to Micah and Petr for putting together, sharing and documenting this valuable resource!


#### <a name="4"></a>Revit WPF Template Readme

The template is well documented and includes built-in support for automatic documentation of the add-ins you create using it.

Here are some excerpts from the [GitHub readme file](https://github.com/mitevpi/revit-wpf-template):

WPF Template for Revit Add-Ins including wrapped external methods for execution in a "Valid Revit API Context".

<center>
<img src="img/revit_wpf_template_window1.png" alt="Window A" title="Window A" width="500"/>
<br/>
<img src="img/revit_wpf_template_window2.png" alt="Window B" title="Window B" width="500"/> 
<br/>
<img src="img/revit_wpf_template_window3.png" alt="Window C" title="Window C" width="500"/> 
<br/>
<img src="img/revit_wpf_template_ribbon.png" alt="Revit ribbon" title="Revit ribbon" width="800"/> <!-- 1070 -->
</center>


#### <a name="4.1"></a>Build

1. Clone/download the repository and open the `.sln` at the root of the repository with Microsoft Visual Studio.
2. Re-link references to `RevitAPI.dll` and others which may be missing.
3. Build the solution &ndash; Building the solution will automatically create and copy the add-in files to the folder for Revit 2019.
4. Open Revit &ndash; Upon opening Revit 2019, there should be a tab called "Template" in Revit, with a button to launch the WPF add-in.

#### <a name="4.2"></a>Customize

In order to use this as a starter for your application, make sure you first refactor the content in the application files (namespace, assembly name, classes, GUID, etc.) and remove the `assets` folder in the repository.

A guide to refactoring can be found in
the [docs](https://github.com/mitevpi/revit-wpf-template/blob/master/docs/RefactorInstructions.md) folder.

#### <a name="4.3"></a>Documentation

Documentation is created using [Sandcastle Help File Builder](https://github.com/EWSoftware/SHFB) by
compiling the docstrings from the compiled `.dll` and `.xml` files generated by Visual Studio upon build.
The Sandcastle project can be launched through
the `RevitTemplate.shfbproj` file in the `docs` folder.

The documentation can be found in the `docs` folder in the root of the repository.
The following documentation sources are created:

1. `.chm` &ndash; This is an interactive help file which can be launched by double-clicking on any Windows machine.
2. `index.html` &ndash; This is the documentation compiled for web deployment. 




