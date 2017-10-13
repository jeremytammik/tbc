<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="run_prettify.js" type="text/javascript"></script>
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 13270564 [Revit 2018 loses focus on close of modeless Addin]
  two workarunds to keep Revit on top

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon 

&ndash; 
...

--->

### Modeless Form Keeping Revit Focused and On Top

I am back from a nice break on the Maremma beach:

<center>
<img src="img/.jpg" alt="" width="200"/>
</center>


Hank DiVincenzo, Sr. Software Engineer at [Ideate, Inc](http://www.ideateinc.com).

We here at Ideate Software are seeing what appears to be Revit Addin ownership issues with Revit’s Main Window. 

The behavior has changed between Revit 2017 and Revit 2018 for modeless Addins.

For Revit 2018, when a Modeless Addin is closed, Revit does not retain is focus; it is pushed behind another application. 

With some experimentation, it was found the application that had focus before Revit is the one that gains focus after the Modeless Addin is closed. 

Revit 2016 and 2017 do not show this behavior, only Revit 2018 does. 

Looking for advice on this issue. Is this a known issue? And is there a known method to solve it? 

Also Note: 

What we understand as the preferred method of getting Revit’s MainWindowHandle has issues when there are multiple Modeless Addins opened. 

When the MainWindowHandle is gotten from the Revit process (i.e. Process.GetCurrentProcess().MainWindowHandle), Modeless Addin ownership is not correct. 

Any Modeless Addin that is started after the first one has its ownership set to that previous Modeless Addin. This can be seen by closing any of the interim Modeless Addins and any Modeless Addins started after the one being closed, also closes. 

Any information or assistance on this would be appreciated, though our current concern is the change in the 2018 behavior.

**Answer:**

I assume that you in fact mean 'parent relationship of the modeless form' when you say 'ownership of the add-in'.

Is that assumption correct?

I myself have always used the JtWindowHandle class to retrieve the main Revit window handle, and set up the parent window relationshipe for my modeless forms using that as described here:

http://thebuildingcoder.typepad.com/blog/2010/06/revit-parent-window.html

I have been notified by the development team that this method will cease to work in the next major release of Revit, after Revit 2018.

I am not aware of any such issues in Revit 2018.

Did it appear in the initial release or a subsequent update?

**Response:**

First thanks for the response and yes, what I have called Owner is the parent relationship. 

NOTE:

- The Addin is a WPF Addin. 
- The Revit focus issue is only seen in Revit 2018 

To reproduce the Revit 2018 focus issue: 

1) Start another app, like the Windows File Manager 
2) Start Revit 2018 
3) From the Addin-Ins Tab >> External Tool Panel Start the app “Id8 SearchWindow Owner Setup” 
4) From the Addin’s dialog open the model dialog by pressing the button “Start Dialog” in the lower right corner of the Addin dialog. 
5) Close the second/modal dialog 
6) Close the Addin 
7) At this point the first app that was started, “File Manager”, will be brought to the foreground, covering Revit; Revit is no longer the top application having focus. 

Now for Some perinate history: 

Within the External Tools there are two applications. 

The “Search Window” Addin is currently how we are getting Revit’s main window and exhibits the issue I am talking about. 

The second or “Id8 MainWindow Owner Setup” is how we use to get Revit’s main window, and uses the prescribed way of getting the main window the Process.GetCurrentProcess.MainWindow property. 

When we moved to using WPF Addins, using the “current process” MainWindow property had issues and we came up with the current method what I am calling the “Search Window”. This method of getting Revit’s Main Window is somewhat complicated but got us working for the Revit 2017 release, it was only during the 2018 release did we see the loss of focus issue within Revit 2018. 

Now the issues we had with using the Process.GetCurrentProcess.MainWindow property. 

When multiple (any number) of Modeless Addins are started, when the first Addin that was open is closed all of the Modeless Addins close. 

Matter of fact if any Modeless Addin is closed all Modeless Addin start after the one being closed also close. 

i.e. if three Modeless Addins are opened, if the second Addin is closed the third started addin is closed also. 

NOTE: this was first seen with two Forms base Addins so WPF was not in play when we ran into this behavior. 

To Reproduce the original Closing issue: 

1) Start Revit 
2) From the Addin-Ins Tab >> External Tool Panel Start the app “Id8 MainWindow Owner Setup” 
3) Start a second “Id8 MainWindow Owner Setup” 
4) Start a third “Id8 MainWindow Owner Setup” 
5) Close the first “Id8 MainWindow Owner Setup” App 
6) All three of the “Id8 MainWindow Owner Setup” Apps close down. 

**Answer:**

Can you work around it for the time being, or live with it?

The reason I ask is the following information I received from the development team:

One of the Revit Advanced Steel developers pointed out that they followed The Building Code advice in building a new add-in:

http://thebuildingcoder.typepad.com/blog/2012/10/ensure-wpf-add-in-remains-in-foreground.html

This was written about 5 years ago.

Please note that as of Revit 2019, the method described in the blog will no longer work. 

There is a way to fix this. The Revit API team has added two new APIs that provide an official way to get the application window handle:

- `Autodesk.Revit.UI.UIControlledApplication.MainWindowHandle`
- `Autodesk.Revit.UI.UIApplication.MainWindowHandle`
- Get the handle of the Revit main window.
- Returns the main window handle of the Revit application. This handle should be used when displaying modal dialogs and message windows to ensure that they are properly parented. This property replaces the System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle property, which is no longer a reliable method for retrieving the main window handle starting with Revit 2019.

Therefore, the issue you describe has already been resolved for future versions.

I don't know how much can be done about it for the current version.

I would expect that an experienced Windows programming expert could devise some kind of workaround.

Do you have one already, or can you create one?

**Response:**

Yes, I did solve it but was hoping there was a solution like you have explained below, where an Addin could just get the Main Window handle from a Revit API call, so this is good news, I will look for it in the latest Revit Preview?

I am more than willing to do a write up on what was done to solve the issue. Let me know what form you want it in, via this case, a direct email, etc.

Here is the writeup of the window issues we ran into and have solved.

Also I have uploaded to the case (ModelessAddinSolution.zip) a VS project for an Add-In that can be used to demonstrate the focus issue and it’s solution. Note, also below in the write up are the instruction on how to duplicate the focus issue with the Add-In. Note, within the VS project is the .addin file for the Add-In, it is in the “External Application” folder.

Let me know if you need more information or clarification.

The project is a Visual Studio 2013 project.

Thanks, Hank

####<a name="2"></a>Writeup of the Issues

####<a name="2"></a>First Issue Context:

This describes my journey to solve two issues with Revit add-in windows. The first issue was a general problem seen in all version of Revit, the second issue was specific to Revit 2018.

####<a name="2"></a>The First Problem

While developing a second modeless add-in we ran into an issue with add-in window parenting/ownership. The problem manifested itself when having two modeless add-ins open: when the first add-in opened was closed, the second add-in opened also closed. With this behavior, it was apparent the second add-in was somehow being associated with the first add-in. We were using the prescribed method of getting the main window for Revit, Process.GetCurrentProcess().MainWindow. Somehow once the first modeless add-in was opened, the MainWindow property for the Revit process changed. A new method for getting Revit’s main window was needed.

####<a name="2"></a>The First Resolution

An alternate solution was needed to get the window handle for Revit’s main window, the window that holds the ribbon and is the first window opened.

The following solution was implemented:

* Find all windows within the Revit process
* Find the Revit windows that do not have a parent assigned to them, i.e. their parent/owner property is null.
The thought here is windows that have no parent are those opened by the OS and not another part of Revit.

* Then for the Revit windows not having a parent, find the one window that has “Autodesk Revit” within its title.

This method seems fragile because it depends on text from Revit’s main window title, but it consistently gets the Revit main window no matter what add-ins are open. And localization is not an issue, the main window title text “Autodesk Revit” does not change for other languages, see the two language examples.

Examples of other languages:

French:
[cid:image001.jpg@01D33936.232E7220]

German
[cid:image002.png@01D33936.232E7220]

NOTE: See the below section “Code Example for First Resolution” for the solution to this first issue.

####<a name="2"></a>Second Issue Context:

With the main window found and working for Revit 2016 and 2017, all was good and a couple more WPF modeless applications were developed. Then came Revit 2018…

####<a name="2"></a>The Second Problem:

While porting our add-in(s) to Revit 2018, we noticed Revit lose focus from time to time. When our modeless (WinForms based) add-in window was closed, Revit was not left on top with focus, rather the application that had focus before Revit was placed on top. This was a surprise being something that Windows should handle. Notable was this new losing focus behavior was only seen in Revit 2018, and not in 2017 or 2016.

An example of the sequence would be:

* open Revit
* open Windows File Manager and ensure it is placed on top of Revit
* switch back to Revit
* open the add-in
* close the add-in

File Manager would now be placed on top, i.e. Revit lost its position in the application stack/show, and was replaced by File Manager.

The above was the case for the WinForms based add-ins, but WPF based add-ins needed additional steps:

* open Revit
* open Windows File Manager and ensure it is placed on top of Revit
* switch back to Revit
* open the add-in
* open a modal dialog from the Modeless add-in
* close the modal dialog
* close the add-in

File Manager would then be placed on top, replacing Revit as the top application.

####<a name="2"></a>The Second Resolution:

The resolution for the loss of focus ended up being somewhat simple.

I registered for the OnClosing() event in the add-in’s main window. Within the OnClosing() event handler, I set Revit to be on top with the User32.dll call SetForegroundWindow().

Because the owner/parent of the add-in window was set correctly for Revit, setting the add-in’s Owner (Revit) on closing to be in the foreground solved the focus problem.

NOTE: See the section “Code for Second Resolution” below for the solution to this issue.

To Run the Sample Add-In:

* Open Revit 2018
* Open File Manager, ensure it is on top of Revit
* Switch back Revit, bring Revit to the top
* To see the solution
* Pick the “Id8 Revit 2018 Focus Solution” command From the Add-Ins Panel > Extern Tools
* Press the “Start Dialog” button
* Close the new dialog
* Close the Add-In dialog
* Revit Stays on top
* To see the Focus issue
* Pick the “Id8 Revit 2018 Focus Solution” command From the Add-Ins Panel > Extern Tools
* Uncheck the “Fix Focus Issue” checkbox
* Press the “Start Dialog” button
* Close the new dialog
* Close the Add-In dialog
* Revit is no longer on top, replaced by File Manager

####<a name="2"></a>Code Example for First Resolution:

Class usage within the Addin’s Main Window, Addin is WPF app

<pre class="code">
// Set Revit as owner of given child window
WindowHandleSearch handle = WindowHandleSearch.MainWindowHandle;
handle.SetAsOwner(this);

Main Window Handle Class

/// <summary>
/// Wrapper class for window handles
/// </summary>
public class WindowHandleSearch : IWin32Window, System.Windows.Forms.IWin32Window
{
#region Static methods
/// <summary>
/// Revit main window handle
/// </summary>
static public WindowHandleSearch MainWindowHandle
{
get
{
// Get handle of main window
var revitProcess = Process.GetCurrentProcess();
return new WindowHandleSearch(GetMainWindow(revitProcess.Id));
}
}
#endregion

#region Constructor
/// <summary>
/// Constructor - From WinForms window handle
/// </summary>
/// <param name="hwnd">Window handle</param>
public WindowHandleSearch(IntPtr hwnd)
{
// Assert valid window handle
Debug.Assert(IntPtr.Zero != hwnd, "Null window handle");

Handle = hwnd;
}
#endregion

#region Methods
/// <summary>
/// Window handle
/// </summary>
public IntPtr Handle { get; private set; }

/// <summary>
/// Set this window handle as the owner of the given window
/// </summary>
/// <param name="childWindow">Child window whose parent will be set to be this window handle</param>
public void SetAsOwner(Window childWindow)
{
var helper = new WindowInteropHelper(childWindow) { Owner = Handle };
}

// User32.dll calls used to get the Main Window for a Process Id (PID)
private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

[DllImport("user32.DLL")]
private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
private static extern IntPtr GetParent(IntPtr hWnd);

[DllImport("user32.dll", SetLastError = true)]
private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

[DllImport("user32.DLL")]
private static extern IntPtr GetShellWindow();

[DllImport("user32.DLL")]
private static extern bool IsWindowVisible(HWND hWnd);

[DllImport("user32.DLL")]
private static extern int GetWindowTextLength(HWND hWnd);

[DllImport("user32.DLL")]
private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

/// <summary>
/// Returns the main Window Handle for the Process Id (pid) passed in.
/// IF the Main Window is not found then a handle value of Zreo is returned, no handle.
/// </summary>
/// <param name="pid"></param>
/// <returns></returns>
public static IntPtr GetMainWindow(int pid)
{
HWND shellWindow = GetShellWindow();
List<HWND> windowsForPid = new List<IntPtr>();

try
{
EnumWindows(
// EnumWindowsProc Function, does the work on each window.
delegate(HWND hWnd, int lParam)
{
if (hWnd == shellWindow) return true;
if (!IsWindowVisible(hWnd)) return true;

uint windowPid = 0;
GetWindowThreadProcessId(hWnd, out windowPid);

// if window is from Pid of interest, see if its the Main Window
if (windowPid == pid)
{
// By default Main Window has a Parent Window of Zero, no parent.
HWND parentHwnd = GetParent(hWnd);
if (parentHwnd == IntPtr.Zero)
windowsForPid.Add(hWnd);
}

return true;
}
// lParam, nothing, null...
, 0);
}
catch (Exception)
{ }

return DetermineMainWindow(windowsForPid);
}

/// <summary>
/// Finds Revit's Main Window from the list of window handles passed in.
/// If the Main Window for Revit is not found then a Null (IntPtr.Zero) handle is returnd.
/// </summary>
/// <param name="handels"></param>
/// <returns></returns>
private static IntPtr DetermineMainWindow(List<HWND> handels)
{
// Safty conditions, bail if not met.
if (handels == null || handels.Count <= 0)
return IntPtr.Zero;

// default Null handel
HWND mainWindow = IntPtr.Zero;

// only one window so return it, must be the Main Window??
if (handels.Count == 1)
{
mainWindow = handels[0];
}
// more than one window
else
{
// more than one candidate for Main Window so find the
// Main Window by its Title, it will contain "Autodesk Revit"
foreach (var hWnd in handels)
{
int length = GetWindowTextLength(hWnd);
if (length == 0) continue;

StringBuilder builder = new StringBuilder(length);
GetWindowText(hWnd, builder, length + 1);

// Depending on the Title of the Main Window to have "Autodesk Revit" in it.
if (builder.ToString().ToLower().Contains("autodesk revit"))
{
mainWindow = hWnd;
break; // found Main Window stop and return it.
}
}
}

return mainWindow;
}
#endregion
}
</pre>

####<a name="2"></a>Code Example for Second Resolution:

<pre class="code">
/// <summary>
/// User32 calls used to set Revit focus
/// </summary>
[DllImport("USER32.DLL")]
internal static extern bool SetForegroundWindow(HWND hWnd);

/// <summary>
/// Use the OnClose event to ensure Revit is brought back into focus.
/// </summary>
/// <param name="e"></param>
protected override void OnClosing(CancelEventArgs e)
{
// do the base work first.
base.OnClosing(e);

// Set Revit to the foreground
try
{
IntPtr ownerIntPtr = new WindowInteropHelper(this).Owner;
if (ownerIntPtr != IntPtr.Zero)
User32.SetForegroundWindow(ownerIntPtr);
}
catch (Exception)
{}
}
</pre>



