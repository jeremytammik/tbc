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


Contextual help best practice in #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/contextualhelp

Dragos Turmac of the Revit development team solved
the Revit API discussion forum questions
on contextual help not working from within a command and
on F1 help for add-in only by
explaining the current best practice to implement online help
&ndash; Question: I'm trying to add contextual help for all commands (press F1 to go to a location).
It's working fine when the tooltip is shown for my commands, but, when the add-in form is open, pressing F1 opens the Autodesk knowledge site for Revit instead...

--->

### Contextual Help Best Practice

Dragos Turmac of the Revit development team solved
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) questions
on [contextual help not working from within a command](https://forums.autodesk.com/t5/revit-api-forum/contextual-help-not-working-from-within-a-command/m-p/7842882) and
on [F1 help for add-in only](https://forums.autodesk.com/t5/revit-api-forum/f1-help-for-addin-only/m-p/7522730) by
explaining the current best practice to implement online help:

**Question:** I'm trying to add contextual help for all commands (press F1 to go to a location).

It's working fine when the tooltip is shown for my commands, but, when the add-in form is open, pressing F1 opens the Autodesk knowledge site for Revit instead.

I looked at the documentation 
on [Ribbon panels and controls](https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2017/ENU/Revit-API/files/GUID-1547E521-59BD-4819-A989-F5A238B9F2B3-htm.html) and this part:

> The `ContextualHelp` class has a `Launch` method that can be called to display the help topic specified by the contents of this `ContextualHelp` object at any time, the same as when the F1 key is pressed when the control is active. This allows the association of help topics with user interface components inside dialogs created by an add-in application.

This leads me to believe that one should be able to press F1 from within a command and get the correct contextual help.

What am I missing?

**Question 2:** This seems to be a repeat of my original request
for [F1 help for add-in only](https://forums.autodesk.com/t5/revit-api-forum/f1-help-for-addin-only/m-p/7522730):

> I am adding Help to my add-in and want the F1 key to call my help file (CHM) for the whole form, not just for the control that has focus. I am using the following code which is working fine. My problem is that F1 triggers Revit's online help after calling mine. Is there a way to prevent this keystroke going on to Revit?

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">protected</span>&nbsp;<span style="color:blue;">override</span>&nbsp;<span style="color:blue;">bool</span>&nbsp;ProcessCmdKey(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;Message&nbsp;msg,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Keys&nbsp;keyData&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;F1&nbsp;for&nbsp;help&nbsp;on&nbsp;any&nbsp;control</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;keyData&nbsp;==&nbsp;Keys.F1&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HelpClick();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;This&nbsp;keystroke&nbsp;was&nbsp;handled,&nbsp;don&#39;t&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;pass&nbsp;to&nbsp;the&nbsp;control&nbsp;with&nbsp;the&nbsp;focus</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">base</span>.ProcessCmdKey(&nbsp;<span style="color:blue;">ref</span>&nbsp;msg,&nbsp;keyData&nbsp;);
&nbsp;&nbsp;}
</pre>


**Answer:** I used the contextual help functionality a couple of times in the past and never encountered the behaviour you describe:

<!-----
$ lb ContextualHelp
0800_multi_version_addin.htm:21
0904_whats_new_2013.htm:3
1114_balloon_tip.htm:2
1283_directobjloader.htm:1

$ blbare 0800 0904 1114 1283
---->

- [Multi-Version Add-in](http://thebuildingcoder.typepad.com/blog/2012/07/multi-version-add-in.html)
- [What's New in the Revit 2013 API](http://thebuildingcoder.typepad.com/blog/2013/03/whats-new-in-the-revit-2013-api.html) &gt; Contextual help support
- [Using Balloon Tips in Revit](http://thebuildingcoder.typepad.com/blog/2014/03/using-balloon-tips-in-revit.html)
- [From Hack to App &ndash; OBJ Mesh Import to DirectShape](http://thebuildingcoder.typepad.com/blog/2015/02/from-hack-to-app-obj-mesh-import-to-directshape.htm...)
 
Unfortunately, all of these date rather far back, so I cannot guarantee that they behave as expected nowadays.

Happily, Dragos solved the issue by explaining the up-to-date solution and best practice implementing online help:

An F1 key hit in a modal C# form is normally caught using the `HelpRequested` control event, like this:

<pre class="code">
  <span style="color:green;">//&nbsp;in&nbsp;designer</span>

  <span style="color:blue;">this</span>.HelpRequested&nbsp;+=&nbsp;<span style="color:blue;">new</span>&nbsp;System.Windows.Forms
  &nbsp;&nbsp;.<span style="color:#2b91af;">HelpEventHandler</span>(&nbsp;<span style="color:blue;">this</span>.OnHelpRequested&nbsp;);

  <span style="color:green;">//&nbsp;in&nbsp;form&#39;s&nbsp;code</span>

  <span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnHelpRequested(&nbsp;
  &nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;
  &nbsp;&nbsp;HelpEventArgs&nbsp;hlpevent&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:green;">//&nbsp;Do&nbsp;stuff&nbsp;here,&nbsp;whatever&nbsp;you&nbsp;wish.&nbsp;
    // Once&nbsp;this&nbsp;event&nbsp;is&nbsp;called,&nbsp;Revit&nbsp;no&nbsp;longer&nbsp;follows</span>

  &nbsp;&nbsp;Help.ShowPopup(&nbsp;<span style="color:blue;">this</span>,&nbsp;<span style="color:#a31515;">&quot;test&nbsp;help&quot;</span>,&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Point</span>(&nbsp;somewhere.x,&nbsp;somewhere.y&nbsp;)&nbsp;);
  }
</pre>

If the above event is not subscribed to, Revit opens its own help.

If it is, Revit does nothing.

Attached a small add-in
in [contextual_help_test_addin.zip](zip//a/case/sfdc/12806669/contextual_help_test_addin.zip) that
only shows a Windows form on which an F1 press will show up a test popup help and Revit does nothing about it.

It implements a start-up command and a test dialogue.

Here is the complete code for the command:

<pre class="code">
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.UI;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.DB;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.Attributes;
 
<span style="color:blue;">namespace</span>&nbsp;TestAddin
{
&nbsp;&nbsp;[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
&nbsp;&nbsp;[<span style="color:#2b91af;">Regeneration</span>(&nbsp;<span style="color:#2b91af;">RegenerationOption</span>.Manual&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">StartUpCmd</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalCommand</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;Execute(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ExternalCommandData</span>&nbsp;commandData,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">ref</span>&nbsp;<span style="color:blue;">string</span>&nbsp;message,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementSet</span>&nbsp;elements&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TestDlg</span>&nbsp;testDlg&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TestDlg</span>();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;testDlg.ShowDialog();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

The C# portion of the test dialogue implementation looks like this:

<pre class="code">
<span style="color:blue;">using</span>&nbsp;System.Drawing;
<span style="color:blue;">using</span>&nbsp;System.Windows.Forms;
 
<span style="color:blue;">namespace</span>&nbsp;TestAddin
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">partial</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">TestDlg</span>&nbsp;:&nbsp;<span style="color:#2b91af;">Form</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;TestDlg()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;OnHelpRequested(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">HelpEventArgs</span>&nbsp;hlpevent&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Help</span>.ShowPopup(&nbsp;<span style="color:blue;">this</span>,&nbsp;<span style="color:#a31515;">&quot;test&nbsp;help&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Point</span>(&nbsp;Top,&nbsp;Left&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

**Response:** I can confirm that I used your original snippet to call my context help routine, and all worked perfectly.

Many thanks to Dragos for this very clear explanation and succinct sample!

<center>
<img src="img/help.png" alt="Help" width="228"/>
</center>


