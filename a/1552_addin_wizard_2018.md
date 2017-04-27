<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- update vs revit add-in wizards for Revit 2018
  /a/doc/revit/tbc/git/a/zip $ cp Revit2018AddinWizardCs0.zip /v/C/Users/tammikj/Documents/Visual\ Studio\ 2015/Templates/ProjectTemplates/Visual\ C#/
/a/doc/revit/tbc/git/a/zip/wiz/2018/cs $ zip -r ../../../Revit2018AddinWizardCs0.zip *
/a/doc/revit/tbc/git/a/zip/wiz/2018/vb $ cp ../../../Revit2018AddinWizardCs0.zip /v/C/Users/tammikj/Documents/Visual\ Studio\ 2015/Templates/ProjectTemplates/Visual\ C#/
/a/doc/revit/tbc/git/a/zip/wiz/2018/vb $ zip -r ../../../Revit2018AddinWizardVb0.zip *
/a/doc/revit/tbc/git/a/zip/wiz/2018/vb $ cp ../../../Revit2018AddinWizardVb0.zip /v/C/Users/tammikj/Documents/Visual\ Studio\ 2015/Templates/ProjectTemplates/Visual\ Basic/

Revit 2018 Visual Studio .NET Add-in Wizards #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim #dynamobim http://bit.ly/rvt2018addinwizards

I updated the Visual Studio Revit C# and VB add-in templates for Revit 2018.
They enable you to create a new C# or VB Revit add-in in Visual Studio with one single click on File &gt; New &gt; Project... &gt; Visual Basic/Visual C# &gt; Revit 2018 Addin and define a complete skeleton Revit add-in, ready to immediately compile and run, including an add-in manifest file, external application and external command.
Just hit F5 to start debugging; the add-in manifest is installed, Revit launched and the command is immediately available...

-->

### Revit 2018 Visual Studio .NET Add-in Wizards

I updated
the [Visual Studio Revit C# and VB add-in templates](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20) for
Revit 2018.

They enable you to create a new C# or VB Revit add-in in Visual Studio with one single click on File &gt; New &gt; Project... &gt; Visual Basic/Visual C# &gt; Revit 2018 Addin:

<center>
<img src="img/revit_2018_addin_wizard.png" alt="Revit 2018 Add-in Wizards" width="800">
</center>

The templates define a complete skeleton Revit add-in, ready to immediately compile and run, including an add-in manifest file, an external application and an external command.

Just hit `F5` to start debugging; the add-in manifest is automatically copied to the proper location, Revit is launched in the Visual Studio debugger, and your shiny new add-in is immediately available in the external tools menu.

You can see it in action in this two-and-a-half-minute [Revit 2018 C# and VB .NET add-in wizard recording](https://youtu.be/OEQdKfwf0Ss):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/OEQdKfwf0Ss?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

Please refer to 
the [Visual Studio Revit add-in wizards topic group](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20) for 
further information on usage, customising the templates for your own needs and migrations in previous years.


#### <a name="3"></a>Download

The current version discussed above
is [release 2018.0.0.0](https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases/tag/2018.0.0.0).

The newest version is always available from
the [VisualStudioRevitAddinWizard GitHub repository](https://github.com/jeremytammik/VisualStudioRevitAddinWizard).

#### <a name="4"></a>Installation

The exact locations to install the wizards for Visual Studio are language dependent.

You install them by simply copying the zip file of your choice &ndash; for C#, VB, or both &ndash; to the appropriate Visual Studio project template folder in your local file system:

- C# – copy [Revit2018AddinWizardCs0.zip](zip/Revit2018AddinWizardCs0.zip)
to [My Documents]\Visual Studio 2015\Templates\ProjectTemplates\Visual C#
- Visual Basic – copy [Revit2018AddinWizardVb0.zip](zip/Revit2018AddinWizardVb0.zip)
to [My Documents]\Visual Studio 2015\Templates\ProjectTemplates\Visual Basic

Or, in other words:

<pre>
  $ cp Revit2018AddinWizardCs0.zip \
  "/v/C/Users/tammikj/Documents/Visual Studio \
  2015/Templates/ProjectTemplates/Visual C#/"

  $ cp Revit2018AddinWizardVb0.zip \
  "/v/C/Users/tammikj/Documents/Visual Studio \
  2015/Templates/ProjectTemplates/Visual Basic/"
</pre>

The GitHub repository includes a batch file `install.bat` to automate this process:

<pre class="prettyprint">
@echo off
if exist cs (goto okcs) else (echo "No cs folder found." && goto exit)
:okcs
if exist vb (goto okvb) else (echo "No vb folder found." && goto exit)
:okvb
set "D=C:\Users\%USERNAME%\Documents\Visual Studio 2015\Templates\ProjectTemplates"
set "F=%TEMP%\Revit2018AddinWizardCs0.zip"
echo Creating C# wizard archive %F%...
cd cs
zip -r "%F%" *
cd ..
echo Copying C# wizard archive to %D%\Visual C#...
copy "%F%" "%D%\Visual C#"
set "F=%TEMP%\Revit2018AddinWizardVb0.zip"
echo Creating VB wizard archive %F%...
cd vb
zip -r "%F%" *
cd ..
echo Copying VB wizard archive to %D%\Visual Basic...
copy "%F%" "%D%\Visual Basic"
:exit
</pre>

It assumes that you cloned the VisualStudioRevitAddinWizard to your local file system and call it from that directory, e.g., like this:

<pre>
C:\a\vs\VisualStudioRevitAddinWizard &gt; install.bat

Creating C# wizard archive C:\Users\tammikj\AppData\Local\Temp\Revit2018AddinWizardCs0.zip...
updating: App.cs (deflated 54%)
updating: Command.cs (deflated 59%)
updating: Properties/ (stored 0%)
updating: Properties/AssemblyInfo.cs (deflated 56%)
updating: RegisterAddin.addin (deflated 66%)
updating: TemplateIcon.ico (deflated 67%)
updating: TemplateRevitCs.csproj (deflated 69%)
updating: TemplateRevitCs.csproj.user (deflated 30%)
updating: TemplateRevitCs.vstemplate (deflated 65%)

Copying C# wizard archive to C:\Users\tammikj\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual C#...
  1 file(s) copied.

Creating VB wizard archive C:\Users\tammikj\AppData\Local\Temp\Revit2018AddinWizardVb0.zip...
updating: AdskApplication.vb (deflated 68%)
updating: AdskCommand.vb (deflated 58%)
updating: My Project/ (stored 0%)
updating: My Project/AssemblyInfo.vb (deflated 54%)
updating: RegisterAddin.addin (deflated 66%)
updating: TemplateIcon.ico (deflated 67%)
updating: TemplateRevitVb.vbproj (deflated 72%)
updating: TemplateRevitVb.vstemplate (deflated 62%)
Copying VB wizard archive to C:\Users\tammikj\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual Basic...
  1 file(s) copied.
</pre>

I hope you find this useful and look forward to hearing about your customisations and suggestions for other enhancements.

Have fun!
