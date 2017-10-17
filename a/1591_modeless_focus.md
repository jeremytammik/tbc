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

- Dianne Phillips
  RE: An update on your post "Ensure WPF Add-in Remains in Foreground"

Modeless Form Keep Revit Focus and On Top #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/modelessfocus

I am back from a nice break in Italy.
Next, I am attending the European Autodesk University in Darmstadt, Germany.
Meanwhile, solutions for two issues on keeping Revit focused and on top when working with a modeless form, and an important heads-up warning from the Revit development team on a future change coming.
We here at Ideate Software are seeing what appears to be Revit add-in ownership issues with Revit's main window. 
The behavior has changed between Revit 2017 and Revit 2018 for modeless add-ins.
For Revit 2018, when a modeless add-in is closed, Revit does not retain is focus; it is pushed behind another application...

--->

### Modeless Form Keep Revit Focus and On Top

I am back from a nice break in Italy, including a visit to
the [Giardino dei Tarocchi](https://en.wikipedia.org/wiki/Tarot_Garden),
the Tarot Garden sculpture garden based on the tarot cards, created by Niki de Saint Phalle
([photos](https://flic.kr/s/aHsm6U4nnA)):

<center>
<img src="img/975_beach_breakfast_1k.jpg" alt="Breakfost on the beach" width="500"/>
</center>

Before I get back to work properly attending the European Autodesk University in Darmstadt, Germany, here is a note from Hank DiVincenzo, Sr. Software Engineer at [Ideate, Inc](http://www.ideateinc.com), on keeping Revit focused and on top when working with a modeless form, and an important heads-up warning from the Revit development team on a future change coming:

- [Two issues](#2)
- [First issue problem](#3)
- [The first resolution](#4)
- [Code example for first resolution](#5)
- [Second issue context](#6)
- [The second problem](#7)
- [The second resolution](#8)
- [Code example for second resolution](#9)
- [Warning! Things will change in the next release](#10)

We here at Ideate Software are seeing what appears to be Revit add-in ownership issues with Revit's main window. 

The behavior has changed between Revit 2017 and Revit 2018 for modeless add-ins.

For Revit 2018, when a modeless add-in is closed, Revit does not retain is focus; it is pushed behind another application. 

With some experimentation, it was found the application that had focus before Revit is the one that gains focus after the modeless add-in is closed. 

Revit 2016 and 2017 do not show this behavior, only Revit 2018 does. 

<!--- Looking for advice on this issue. Is this a known issue? And is there a known method to solve it?  --->

Also Note: 

What we understand as the preferred method of getting Revit's MainWindowHandle has issues when there are multiple modeless add-ins opened. 

When the MainWindowHandle is gotten from the Revit process (i.e. using the `Process.GetCurrentProcess()` `MainWindowHandle` property), modeless add-in ownership is incorrect. 

Any modeless add-in that is started after the first one has its ownership set to that previous modeless add-in. This can be seen by closing any of the interim modeless add-ins and any modeless add-ins started after the one being closed, also closes. 

What I have called 'owner' is also known as the parent-child window relationship.

<!---
Any information or assistance on this would be appreciated, though our current concern is the change in the 2018 behavior.

**Answer:**

I assume that you in fact mean 'parent relationship of the modeless form' when you say 'ownership of the add-in'.

Is that assumption correct?

I myself have always used the JtWindowHandle class to retrieve the main Revit window handle, and set up the parent window relationship for my modeless forms using that as described here:

http://thebuildingcoder.typepad.com/blog/2010/06/revit-parent-window.html

I have been notified by the development team that this method will cease to work in the next major release of Revit, after Revit 2018.

I am not aware of any such issues in Revit 2018.

Did it appear in the initial release or a subsequent update?

**Response:**

First thanks for the response and yes, what I have called Owner is the parent relationship. 

Further note:

- The add-in is a WPF add-in. 
- The Revit focus issue is only seen in Revit 2018 

To reproduce the Revit 2018 focus issue: 

1) Start another app, like the Windows File Manager 
2) Start Revit 2018 
3) From the Addin-Ins Tab >> External Tool Panel Start the app “Id8 SearchWindow Owner Setup” 
4) From the add-in's dialog open the model dialog by pressing the button “Start Dialog” in the lower right corner of the add-in dialog. 
5) Close the second/modal dialog 
6) Close the add-in 
7) At this point the first app that was started, “File Manager”, will be brought to the foreground, covering Revit; Revit is no longer the top application having focus. 

Now for Some perinate history: 

Within the External Tools there are two applications. 

The “Search Window” add-in is currently how we are getting Revit's main window and exhibits the issue I am talking about. 

The second or “Id8 MainWindow Owner Setup” is how we use to get Revit's main window, and uses the prescribed way of getting the main window the Process.GetCurrentProcess.MainWindow property. 

When we moved to using WPF add-ins, using the “current process” MainWindow property had issues and we came up with the current method what I am calling the “Search Window”. This method of getting Revit's Main Window is somewhat complicated but got us working for the Revit 2017 release, it was only during the 2018 release did we see the loss of focus issue within Revit 2018. 

Now the issues we had with using the Process.GetCurrentProcess.MainWindow property. 

When multiple (any number) of modeless add-ins are started, when the first add-in that was open is closed all of the modeless add-ins close. 

Matter of fact if any modeless add-in is closed all modeless add-in start after the one being closed also close. 

i.e. if three modeless add-ins are opened, if the second add-in is closed the third started add-in is closed also. 

NOTE: this was first seen with two Forms base add-ins so WPF was not in play when we ran into this behavior. 

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

Yes, I did solve it but was hoping there was a solution like you have explained below, where an add-in could just get the Main Window handle from a Revit API call, so this is good news, I will look for it in the latest Revit Preview?

I am more than willing to do a write up on what was done to solve the issue. Let me know what form you want it in, via this case, a direct email, etc.

Let me know if you need more information or clarification.

The project is a Visual Studio 2013 project.

Thanks, Hank

--->

Here is the write-up of the two window issues we ran into and have solved.

To demonstrate, I am also sharing the Visual Studio 2013
project [ModelessAddinSolution.zip](zip/hdv_ModelessAddinSolution.zip).

It implements an add-in that can be used to demonstrate the focus issue and its solution following the instructions provided below. It also includes the add-in manifest `.addin` file in the *External Application* folder.


####<a name="2"></a>Two Issues

This describes my journey to solve two issues with Revit add-in windows.

The first issue was a general problem seen in all version of Revit, the second issue was specific to Revit 2018.

####<a name="3"></a>First Issue Problem

While developing a second modeless add-in, we ran into an issue with add-in window parenting/ownership. The problem manifested itself when having two modeless add-ins open: when the first add-in opened was closed, the second add-in opened also closed. With this behavior, it was apparent the second add-in was somehow being associated with the first add-in. We were using *Process.GetCurrentProcess().MainWindow* to get main Revit window. Somehow, once the first modeless add-in was opened, the `MainWindow` property for the Revit process changed. A new method for getting Revit's main window was needed.

####<a name="4"></a>The First Resolution

An alternate solution was needed to get the window handle for Revit's main window, the window that holds the ribbon and is the first window opened.

The following solution was implemented:

- Find all windows within the Revit process
- Find the Revit windows that do not have a parent assigned to them, i.e. their parent/owner property is null.

The thought here is windows that have no parent are those opened by the OS and not another part of Revit.

- Then, for the Revit windows not having a parent, find the one window that has “Autodesk Revit” within its title.

This method seems fragile because it depends on text from Revit's main window title, but it consistently gets the Revit main window no matter what add-ins are open. And localization is not an issue, the main window title text “Autodesk Revit” does not change for other languages, cf. the following two language examples.

French:

<center>
<img src="img/revit_title_bar_french.jpg" alt="French Revit title bar" width="925"/>
</center>

German:

<center>
<img src="img/revit_title_bar_german.png" alt="German Revit title bar" width="967"/>
</center>

####<a name="5"></a>Code Example for First Resolution

Class usage within the add-in's main window; the add-in is WPF a app:

<pre class="code">
  <span style="color:green;">//&nbsp;Set&nbsp;Revit&nbsp;as&nbsp;owner&nbsp;of&nbsp;given&nbsp;child&nbsp;window</span>
  <span style="color:#2b91af;">WindowHandleSearch</span>&nbsp;handle
  &nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">WindowHandleSearch</span>.MainWindowHandle;
  
  handle.SetAsOwner(<span style="color:blue;">this</span>);
</pre>

Main Window Handle Class:
 
<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Wrapper&nbsp;class&nbsp;for&nbsp;window&nbsp;handles</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">WindowHandleSearch</span>
&nbsp;&nbsp;:&nbsp;IWin32Window,&nbsp;System.Windows.Forms.IWin32Window
{
&nbsp;&nbsp;<span style="color:blue;">#region</span>&nbsp;Static&nbsp;methods
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Revit&nbsp;main&nbsp;window&nbsp;handle</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">WindowHandleSearch</span>&nbsp;MainWindowHandle
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">get</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;handle&nbsp;of&nbsp;main&nbsp;window</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;revitProcess&nbsp;=&nbsp;<span style="color:#2b91af;">Process</span>.GetCurrentProcess();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">WindowHandleSearch</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetMainWindow(&nbsp;revitProcess.Id&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">#endregion</span>
 
&nbsp;&nbsp;<span style="color:blue;">#region</span>&nbsp;Constructor
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Constructor&nbsp;-&nbsp;From&nbsp;WinForms&nbsp;window&nbsp;handle</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>hwnd<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">Window&nbsp;handle</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;WindowHandleSearch(&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;hwnd&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Assert&nbsp;valid&nbsp;window&nbsp;handle</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Assert(&nbsp;<span style="color:#2b91af;">IntPtr</span>.Zero&nbsp;!=&nbsp;hwnd,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;Null&nbsp;window&nbsp;handle&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;Handle&nbsp;=&nbsp;hwnd;
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">#endregion</span>
 
&nbsp;&nbsp;<span style="color:blue;">#region</span>&nbsp;Methods
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Window&nbsp;handle</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;Handle&nbsp;{&nbsp;<span style="color:blue;">get</span>;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">set</span>;&nbsp;}
 
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Set&nbsp;this&nbsp;window&nbsp;handle&nbsp;as&nbsp;the&nbsp;owner&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;of&nbsp;the&nbsp;given&nbsp;window</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>childWindow<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">Child&nbsp;window&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;whose&nbsp;parent&nbsp;will&nbsp;be&nbsp;set&nbsp;to&nbsp;be&nbsp;this&nbsp;window&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;handle</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetAsOwner(&nbsp;Window&nbsp;childWindow&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;helper&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;WindowInteropHelper(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;childWindow&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;Owner&nbsp;=&nbsp;Handle&nbsp;};
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;User32.dll&nbsp;calls&nbsp;used&nbsp;to&nbsp;get&nbsp;the&nbsp;Main&nbsp;Window&nbsp;for&nbsp;a&nbsp;Process&nbsp;Id&nbsp;(PID)</span>
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">delegate</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;<span style="color:#2b91af;">EnumWindowsProc</span>(
&nbsp;&nbsp;&nbsp;&nbsp;HWND&nbsp;hWnd,&nbsp;<span style="color:blue;">int</span>&nbsp;lParam&nbsp;);
 
&nbsp;&nbsp;[DllImport(&nbsp;<span style="color:#a31515;">&quot;user32.DLL&quot;</span>&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;EnumWindows(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">EnumWindowsProc</span>&nbsp;enumFunc,&nbsp;<span style="color:blue;">int</span>&nbsp;lParam&nbsp;);
 
&nbsp;&nbsp;[DllImport(&nbsp;<span style="color:#a31515;">&quot;user32.dll&quot;</span>,&nbsp;ExactSpelling&nbsp;=&nbsp;<span style="color:blue;">true</span>,
&nbsp;&nbsp;&nbsp;&nbsp;CharSet&nbsp;=&nbsp;CharSet.Auto&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;GetParent(&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;hWnd&nbsp;);
 
&nbsp;&nbsp;[DllImport(&nbsp;<span style="color:#a31515;">&quot;user32.dll&quot;</span>,&nbsp;SetLastError&nbsp;=&nbsp;<span style="color:blue;">true</span>&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:blue;">uint</span>&nbsp;GetWindowThreadProcessId(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;hWnd,&nbsp;<span style="color:blue;">out</span>&nbsp;<span style="color:blue;">uint</span>&nbsp;processId&nbsp;);
 
&nbsp;&nbsp;[DllImport(&nbsp;<span style="color:#a31515;">&quot;user32.DLL&quot;</span>&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;GetShellWindow();
 
&nbsp;&nbsp;[DllImport(&nbsp;<span style="color:#a31515;">&quot;user32.DLL&quot;</span>&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;IsWindowVisible(&nbsp;HWND&nbsp;hWnd&nbsp;);
 
&nbsp;&nbsp;[DllImport(&nbsp;<span style="color:#a31515;">&quot;user32.DLL&quot;</span>&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:blue;">int</span>&nbsp;GetWindowTextLength(&nbsp;HWND&nbsp;hWnd&nbsp;);
 
&nbsp;&nbsp;[DllImport(&nbsp;<span style="color:#a31515;">&quot;user32.DLL&quot;</span>&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:blue;">int</span>&nbsp;GetWindowText(&nbsp;HWND&nbsp;hWnd,
&nbsp;&nbsp;&nbsp;&nbsp;StringBuilder&nbsp;lpString,&nbsp;<span style="color:blue;">int</span>&nbsp;nMaxCount&nbsp;);
 
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Returns&nbsp;the&nbsp;main&nbsp;Window&nbsp;Handle&nbsp;for&nbsp;the&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Process&nbsp;Id&nbsp;(pid)&nbsp;passed&nbsp;in.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;IF&nbsp;the&nbsp;Main&nbsp;Window&nbsp;is&nbsp;not&nbsp;found&nbsp;then&nbsp;a&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;handle&nbsp;value&nbsp;of&nbsp;Zreo&nbsp;is&nbsp;returned,&nbsp;no&nbsp;handle.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>pid<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;GetMainWindow(&nbsp;<span style="color:blue;">int</span>&nbsp;pid&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;HWND&nbsp;shellWindow&nbsp;=&nbsp;GetShellWindow();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;HWND&gt;&nbsp;windowsForPid&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">IntPtr</span>&gt;();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EnumWindows(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;EnumWindowsProc&nbsp;Function,&nbsp;does&nbsp;the&nbsp;work&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;on&nbsp;each&nbsp;window.</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">delegate</span>&nbsp;(&nbsp;HWND&nbsp;hWnd,&nbsp;<span style="color:blue;">int</span>&nbsp;lParam&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;hWnd&nbsp;==&nbsp;shellWindow&nbsp;)&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!IsWindowVisible(&nbsp;hWnd&nbsp;)&nbsp;)&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">true</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">uint</span>&nbsp;windowPid&nbsp;=&nbsp;0;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetWindowThreadProcessId(&nbsp;hWnd,&nbsp;<span style="color:blue;">out</span>&nbsp;windowPid&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;if&nbsp;window&nbsp;is&nbsp;from&nbsp;Pid&nbsp;of&nbsp;interest,&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;see&nbsp;if&nbsp;it&#39;s&nbsp;the&nbsp;Main&nbsp;Window</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;windowPid&nbsp;==&nbsp;pid&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;By&nbsp;default&nbsp;Main&nbsp;Window&nbsp;has&nbsp;a&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Parent&nbsp;Window&nbsp;of&nbsp;Zero,&nbsp;no&nbsp;parent.</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HWND&nbsp;parentHwnd&nbsp;=&nbsp;GetParent(&nbsp;hWnd&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;parentHwnd&nbsp;==&nbsp;<span style="color:#2b91af;">IntPtr</span>.Zero&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;windowsForPid.Add(&nbsp;hWnd&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;lParam,&nbsp;nothing,&nbsp;null...</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;,&nbsp;0&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;<span style="color:#2b91af;">Exception</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;DetermineMainWindow(&nbsp;windowsForPid&nbsp;);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Finds&nbsp;Revit&#39;s&nbsp;Main&nbsp;Window&nbsp;from&nbsp;the&nbsp;list&nbsp;of&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;window&nbsp;handles&nbsp;passed&nbsp;in.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;If&nbsp;the&nbsp;Main&nbsp;Window&nbsp;for&nbsp;Revit&nbsp;is&nbsp;not&nbsp;found&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;then&nbsp;a&nbsp;Null&nbsp;(IntPtr.Zero)&nbsp;handle&nbsp;is&nbsp;returnd.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>handles<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;&lt;/</span><span style="color:gray;">returns</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;DetermineMainWindow(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;HWND&gt;&nbsp;handles&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Safty&nbsp;conditions,&nbsp;bail&nbsp;if&nbsp;not&nbsp;met.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;handles&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;||&nbsp;handles.Count&nbsp;&lt;=&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">IntPtr</span>.Zero;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;default&nbsp;Null&nbsp;handel</span>
&nbsp;&nbsp;&nbsp;&nbsp;HWND&nbsp;mainWindow&nbsp;=&nbsp;<span style="color:#2b91af;">IntPtr</span>.Zero;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;only&nbsp;one&nbsp;window&nbsp;so&nbsp;return&nbsp;it,&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;must&nbsp;be&nbsp;the&nbsp;Main&nbsp;Window??</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;handles.Count&nbsp;==&nbsp;1&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mainWindow&nbsp;=&nbsp;handles[0];
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;more&nbsp;than&nbsp;one&nbsp;window</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;more&nbsp;than&nbsp;one&nbsp;candidate&nbsp;for&nbsp;Main&nbsp;Window&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;so&nbsp;find&nbsp;the&nbsp;Main&nbsp;Window&nbsp;by&nbsp;its&nbsp;Title,&nbsp;it&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;will&nbsp;contain&nbsp;&quot;Autodesk&nbsp;Revit&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:blue;">var</span>&nbsp;hWnd&nbsp;<span style="color:blue;">in</span>&nbsp;handles&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;length&nbsp;=&nbsp;GetWindowTextLength(&nbsp;hWnd&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;length&nbsp;==&nbsp;0&nbsp;)&nbsp;<span style="color:blue;">continue</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StringBuilder&nbsp;builder&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;StringBuilder(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;length&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetWindowText(&nbsp;hWnd,&nbsp;builder,&nbsp;length&nbsp;+&nbsp;1&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Depending&nbsp;on&nbsp;the&nbsp;Title&nbsp;of&nbsp;the&nbsp;Main&nbsp;Window&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;to&nbsp;have&nbsp;&quot;Autodesk&nbsp;Revit&quot;&nbsp;in&nbsp;it.</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;builder.ToString().ToLower().Contains(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;autodesk&nbsp;revit&quot;</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mainWindow&nbsp;=&nbsp;hWnd;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;&nbsp;<span style="color:green;">//&nbsp;found&nbsp;Main&nbsp;Window&nbsp;stop&nbsp;and&nbsp;return&nbsp;it.</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;mainWindow;
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">#endregion</span>
}
</pre>


####<a name="6"></a>Second Issue Context

With the main window found and working for Revit 2016 and 2017, all was good and a couple more WPF modeless applications were developed. Then came Revit 2018...

####<a name="7"></a>The Second Problem

While porting our add-in(s) to Revit 2018, we noticed Revit lose focus from time to time. When our modeless (WinForms based) add-in window was closed, Revit was not left on top with focus; instead, the application that had focus <b>before</b> Revit was placed on top. This was a surprise, being something that Windows should handle. Notable was this new losing focus behavior was only seen in Revit 2018, and not in 2017 or 2016.

An example of the sequence would be:

- Open Revit
- Open Windows File Manager and ensure it is placed on top of Revit
- Switch back to Revit
- Open the add-in
- Close the add-in

File Manager would now be placed on top, i.e., Revit lost its position in the application stack/show, and was replaced by File Manager.

The above was the case for the WinForms based add-ins, but WPF based add-ins needed additional steps:

- Open Revit
- Open Windows File Manager and ensure it is placed on top of Revit
- Switch back to Revit
- Open the add-in
- Open a modal dialog from the modeless add-in
- Close the modal dialog
- Close the add-in

File Manager would then be placed on top, replacing Revit as the top application.

####<a name="8"></a>The Second Resolution

The resolution for the loss of focus ended up being somewhat simple.

I registered for the `OnClosing` event in the add-in's main window. Within the `OnClosing` event handler, I set Revit to be on top with the User32.dll call `SetForegroundWindow`.

Because the owner/parent of the add-in window was set correctly for Revit, setting the add-in's owner (Revit) on closing to be in the foreground solved the focus problem.

NOTE: See the section “Code for Second Resolution” below for the solution to this issue.

To Run the Sample Add-In:

- Open Revit 2018
- Open File Manager, ensure it is on top of Revit
- Switch back Revit, bring Revit to the top
- To see the solution
- Pick the “Id8 Revit 2018 Focus Solution” command From the Add-Ins Panel > Extern Tools
- Press the “Start Dialog” button
- Close the new dialog
- Close the Add-In dialog
- Revit Stays on top
- To see the Focus issue
- Pick the “Id8 Revit 2018 Focus Solution” command From the Add-Ins Panel > Extern Tools
- Uncheck the “Fix Focus Issue” checkbox
- Press the “Start Dialog” button
- Close the new dialog
- Close the Add-In dialog
- Revit is no longer on top, replaced by File Manager

####<a name="9"></a>Code Example for Second Resolution

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;User32&nbsp;calls&nbsp;used&nbsp;to&nbsp;set&nbsp;Revit&nbsp;focus</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
[DllImport(&nbsp;<span style="color:#a31515;">&quot;USER32.DLL&quot;</span>&nbsp;)]
<span style="color:blue;">internal</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">extern</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;SetForegroundWindow(&nbsp;HWND&nbsp;hWnd&nbsp;);
 
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Use&nbsp;the&nbsp;OnClose&nbsp;event&nbsp;to&nbsp;ensure&nbsp;Revit&nbsp;is&nbsp;brought&nbsp;back&nbsp;into&nbsp;focus.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>e<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">protected</span>&nbsp;<span style="color:blue;">override</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnClosing(&nbsp;CancelEventArgs&nbsp;e&nbsp;)
{
&nbsp;&nbsp;<span style="color:green;">//&nbsp;do&nbsp;the&nbsp;base&nbsp;work&nbsp;first.</span>
&nbsp;&nbsp;<span style="color:blue;">base</span>.OnClosing(&nbsp;e&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;Revit&nbsp;to&nbsp;the&nbsp;foreground</span>
&nbsp;&nbsp;<span style="color:blue;">try</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IntPtr</span>&nbsp;ownerIntPtr&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;WindowInteropHelper(&nbsp;<span style="color:blue;">this</span>&nbsp;).Owner;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;ownerIntPtr&nbsp;!=&nbsp;<span style="color:#2b91af;">IntPtr</span>.Zero&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;User32.SetForegroundWindow(&nbsp;ownerIntPtr&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">catch</span>(&nbsp;<span style="color:#2b91af;">Exception</span>&nbsp;)
&nbsp;&nbsp;{&nbsp;}
}
</pre>

Very many thanks to Hank for his persistent research and sharing this valuable solution.

####<a name="10"></a>Warning! Things Will Change in the Next Release

I myself always used the `JtWindowHandle` class to retrieve the main Revit window handle in the past, and use that
to [set up the parent window relationship for my modeless forms](http://thebuildingcoder.typepad.com/blog/2010/06/revit-parent-window.html) for
simple single modeless forms.

I have been warned by the development team that this method will cease to work in the next major release of Revit, after Revit 2018:

A developer followed the advice 
to [ensure a WPF add-in remains in foreground](http://thebuildingcoder.typepad.com/blog/2012/10/ensure-wpf-add-in-remains-in-foreground.html) in
building a new add-in. 

This was written about 5 years ago.

As of the next major release of Revit, the method described there will no longer work.

An easy way to fix this will be provided. The Revit API will include new API calls providing an official way to get the application window handle:

- Get the handle of the Revit main window.
- Return the main window handle of the Revit application. This handle should be used when displaying modal dialogs and message windows to ensure that they are properly parented. This property replaces the *System.Diagnostics.Process.GetCurrentProcess()* `MainWindowHandle` property.

