<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/how-to-get-plugin-ui-to-be-at-the-same-level-as-revit/m-p/8392848
  asked a number of times.
  https://thebuildingcoder.typepad.com/blog/2010/06/revit-parent-window.html

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
  Also cf. [](https://forums.autodesk.com/t5/revit-api-forum/assign-a-name-to-a-string-c-process-getcurrentprocess/m-p/8365316)
  To get a text from an IntPtr window handle:
  https://www.pinvoke.net/default.aspx/user32/getwindowtext.html?diff=y
  Revitalizer

Revit window handle and parenting an add-in form in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/rvthwnd

Access to the Revit main window handle changed in Revit 2019, raising a couple of questions
&ndash; Making Revit the add-in parent
&ndash; The Revit 2019 <code>MainWindowHandle</code> API
&ndash; Docking system and multiple main window explanation
&ndash; Updating The Building Coder samples...

-->

### Revit Window Handle and Parenting an Add-In Form

Access to the Revit main window handle changed in Revit 2019, raising a couple of questions:

- [Making Revit the add-in parent](#2) 
- [The Revit 2019 `MainWindowHandle` API](#3) 
- [Docking system and multiple main window explanation](#4) 
- [Updating The Building Coder samples](#5) 

<center>
<img src="img/shattered_window.jpg" alt="Shattered window" width="320">
<p style="font-size: 80%; font-style:italic">Shattered window &#169; Benoit Brummer, <a href="https://commons.wikimedia.org/wiki/User:Trougnouf">@Trougnouf</a></p>
</center>

#### <a name="2"></a> Making Revit the Add-In Parent

A question came up in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) question
on [how to get plugin UI to be at the same level as Revit](https://forums.autodesk.com/t5/revit-api-forum/how-to-get-plugin-ui-to-be-at-the-same-level-as-revit/m-p/8392848) that
has in fact been asked repeatedly in the past.

Some of my past answers can be found by searching the forum for 'jtwindowhandle'.

As of Revit 2019, however, the answer needs to be modified and updated, so let's do so here and now:

**Question:** I'm currently setting my plug-in's UI to `TopMost`.

However, if I minimize Revit, my plugin stays on top.

Is there a way to have my plug-in's UI to match the functionality of Revit?

**Answer:** You have to ensure that your control is assigned the Revit main window as parent.

For instance, if you display your form using
the [.NET `ShowDialog` method](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.form.showdialog),
make use of its overload taking an IWin32Window argument:

- `ShowDialog(IWin32Window)` &ndash; Shows the form as a modal dialog box with the specified owner.

This approach was explained back in 2010 in the discussion on setting 
the [Revit parent window](https://thebuildingcoder.typepad.com/blog/2010/06/revit-parent-window.html).

Please note that
the [Revit 2019 API provides direct access to the Revit main window handle](https://thebuildingcoder.typepad.com/blog/2018/08/whats-new-in-the-revit-20191-api.html#3.1.4).


#### <a name="3"></a> The Revit 2019 MainWindowHandle API

Here is a brief quote from
the [Revit 2019 API news](https://thebuildingcoder.typepad.com/blog/2018/08/whats-new-in-the-revit-20191-api.html)
on the [direct access to the Revit main window handle](https://thebuildingcoder.typepad.com/blog/2018/08/whats-new-in-the-revit-20191-api.html#3.1.4):

> <b>1.4. UI API changes</b>

> <b>1.4.1. Main window handle access</b>

> Two new properties in the `Autodesk.Revit.UI` namespace provide access to the handle of the Revit main window:

> - `UIApplication.MainWindowHandle`
> - `UIControlledApplication.MainWindowHandle`

> This handle should be used when displaying modal dialogs and message windows to ensure that they are properly parented.
Use these properties instead of System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle,
which is no longer a reliable method for retrieving the main window handle starting with Revit 2019.

The change was also pointed out by The Building Coder in October 2017 with
a [warning that things will change in the next release](https://thebuildingcoder.typepad.com/blog/2017/10/modeless-form-keep-revit-focus-and-on-top.html#10).


#### <a name="4"></a> Docking System and Multiple Main Window Explanation

Revitalizer explains the need for the new property in his notes
on [assigning a name to a string in C# and `Process.GetCurrentProcess().MainWindowTitle`](https://forums.autodesk.com/t5/revit-api-forum/assign-a-name-to-a-string-c-process-getcurrentprocess/m-p/8365316):

> Due to the new docking system introduced in Revit 2019, both the `UIApplication` and `UIControlledApplication` classes now sport a `MainWindowHandle` property.

> It returns an `IntPtr` window handle that you can [P/Invoke `GetWindowText`](http://pinvoke.net/default.aspx/user32/GetWindowText.html) on to retrieve the window caption text.

> In Revit 2019, if view windows are pulled off the main window, there may be more than one Revit application window.

> If you open views in just one single Revit 2019 window, of course the 2018 code might still function, since it just finds the only one.

#### <a name="5"></a> Updating The Building Coder Samples

[The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) were
still using the now obsolete `JtWindowHandle` class up
until [release 2019.0.143.9](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.143.9).

The update to switch to the new Revit `MainWindowHandle` property instead was prompted
by [issue #8](https://github.com/jeremytammik/the_building_coder_samples/issues/8) about
a keyboard shortcut problem with `CmdPressKeys` in Revit 2019.

The code in CmdPressKeys.cs was still retrieving the Revit main window handle via a call to `GetCurrentProcess`:

<pre class="code">
    IntPtr revitHandle = System.Diagnostics.Process
     .GetCurrentProcess().MainWindowHandle;
</pre>

I modified it to use `UiApplication MainWindowHandle` instead and removed the use of `JtWindowHandle`
in [release 2019.0.143.10](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.143.10).

Look at the modifications made to the modules CmdPlaceFamilyInstance.cs, CmdPressKeys.cs and JtWindowHandle.cs in
the [diff between the two versions](https://github.com/jeremytammik/the_building_coder_samples/compare/2019.0.143.9...2019.0.143.10).



