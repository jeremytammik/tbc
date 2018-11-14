<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/how-to-get-plugin-ui-to-be-at-the-same-level-as-revit/m-p/8392848
  asked a number of times.

- https://github.com/jeremytammik/the_building_coder_samples/issues/8
  Q: the "cmdpresskey" method worked for me since Revit 2014 - Revit 2018.
  I made keyboard shortcuts for many commands like Textnotes, Dimensions.
  But, I could not make these keyboard shortcuts work anymore for Revit 2019.
  A: Ok, I see the code now in CmdPressKeys.cs. Sorry for my case sensitivity. The problem is presumably caused by the line
    IntPtr revitHandle = System.Diagnostics.Process
     .GetCurrentProcess().MainWindowHandle;
  Please refer to this explanation:
  https://thebuildingcoder.typepad.com/blog/2017/10/modeless-form-keep-revit-focus-and-on-top.html#10
  The solution is given here:
  https://thebuildingcoder.typepad.com/blog/2018/04/whats-new-in-the-revit-2019-api.html#4.1.4
  use UiApplication MainWindowHandle to address issue #8
  I modified the code to use UiApplication MainWindowHandle to address issue #8 and removed use of JtWindowHandle in release 2019.0.143.10. please test and confirm that it works.
  1.4. UI API changes
  1.4.1. Main window handle access
  Two new properties allow access to the handle of the Revit main window:
  Autodesk.Revit.UI.UIApplication.MainWindowHandle
  Autodesk.Revit.UI.UIControlledApplication.MainWindowHandle
  This handle should be used when displaying modal dialogs and message windows to insure that they are properly parented. Use these properties instead of System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle, which is no longer a reliable method for retrieving the main window handle starting with Revit 2019.
  Also cf. [](https://forums.autodesk.com/t5/revit-api-forum/assign-a-name-to-a-string-c-process-getcurrentprocess/m-p/8365316)
  To get a text from an IntPtr window handle:
  https://www.pinvoke.net/default.aspx/user32/getwindowtext.html?diff=y
  Revitalizer

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash; 
...

-->

### Revit Window Handle and Parenting an Add-In Form

A question came up in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) question
on [how to get plugin UI to be at the same level as Revit](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-plugin-ui-to-be-at-the-same-level-as-revit/m-p/8392848) that
has in fact been asked repeatedly in the past.

Some of my past answers can be found by searching the forum for 'jtwindowhandle'.

As of Revit 2019, however, the answer needs to be modified and updated, so let's do so here and now:

**Question:** I'm currently setting my plug-in's UI to `TopMost`.

Howeer, if I minimize Revit, my plugin stays on top.

Is there a way to have my plug-in's UI to match the functionality of Revit?

**Answer:** You have to ensure that your control is assigned the Revit main window as parent.

For instance, if you display your form using
the [.NET `ShowDialog` method](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.form.showdialog),
make use of its overload taking an IWin32Window argument:

- ShowDialog(IWin32Window) &ndash; Shows the form as a modal dialog box with the specified owner.

https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.form.showdialog?view=netframework-4...

https://thebuildingcoder.typepad.com/blog/2010/06/revit-parent-window.html

Also note that the Revit 2019 API provides direct access to the Revit main window handle:

https://thebuildingcoder.typepad.com/blog/2018/08/whats-new-in-the-revit-20191-api.html#3.1.4


<pre class="code">
</pre>

<center>
<img src="img/.png" alt="" width="100">
</center>

