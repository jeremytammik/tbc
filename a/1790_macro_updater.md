<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- IUpdater in a project macro on startup
  https://forums.autodesk.com/t5/revit-api-forum/iupdater-in-a-project-macro-on-startup/m-p/9087481

twitter:

DMU or Dynamic Model Updater macro in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/dmumacro

Let's take a quick look at implementing a dynamic model updater in a macro
&ndash; Task
&ndash; Solution
&ndash; Drill up the filter...

linkedin:

DMU or Dynamic Model Updater macro in the #RevitAPI

http://bit.ly/dmumacro

Let's take a quick look at implementing a dynamic model updater in a macro:

- Task
- Solution
- Drill up the filter...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Dynamic Model Updater Macro

Let's take a quick look at implementing a dynamic model updater in a macro:

- [Task](#2)
- [Solution](#3)
- [Drill up the filter](#4)

####<a name="2"></a> Task

Dave raised and solved an interesting issue concerning macros, an area that we have not discussed much here yet, in
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [`IUpdater` in a project macro on startup](https://forums.autodesk.com/t5/revit-api-forum/iupdater-in-a-project-macro-on-startup/m-p/9087481):

I'm trying to use `IUpdater` in a macro that automatically starts when a project opens.
Is this possible?

So far, I used Macro Manager / Create to set up some boilerplate.

Then, I pasted in Autodesk's `WallUpdater` example code from the knowledge article
on [Implementing IUpdater](https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2015/ENU/Revit-API/files/GUID-6D434229-0A2E-41FE-B29D-1BB2E6471F50-htm.html)
into my `public partial class ThisDocument`.

I figured out how to run the code on project startup by calling it from the boilerplate's `private void Module_Startup`.

But I haven't had any luck calling `WallUpdater`.

Here is my code:

<pre class="code">
<span style="color:blue;">using</span>&nbsp;System;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.DB;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.UI.Selection;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.UI;
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.Attributes;
<span style="color:blue;">using</span>&nbsp;System.Collections.Generic;
<span style="color:blue;">using</span>&nbsp;System.Linq;
 
<span style="color:blue;">namespace</span>&nbsp;test
{
&nbsp;&nbsp;[<span style="color:#2b91af;">Transaction</span>(&nbsp;<span style="color:#2b91af;">TransactionMode</span>.Manual&nbsp;)]
&nbsp;&nbsp;[Autodesk.Revit.DB.Macros.<span style="color:#2b91af;">AddInId</span>(&nbsp;<span style="color:#a31515;">&quot;redacted&quot;</span>&nbsp;)]
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">partial</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">ThisDocument</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Module_Startup(&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;<span style="color:#2b91af;">EventArgs</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;hello&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;this&nbsp;pops&nbsp;up&nbsp;when&nbsp;you&nbsp;open&nbsp;the&nbsp;project&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Module_Shutdown(&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;<span style="color:#2b91af;">EventArgs</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">#region</span>&nbsp;Revit&nbsp;Macros&nbsp;generated&nbsp;code
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;InternalStartup()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.Startup&nbsp;+=&nbsp;<span style="color:blue;">new</span>&nbsp;System.<span style="color:#2b91af;">EventHandler</span>(&nbsp;Module_Startup&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>.Shutdown&nbsp;+=&nbsp;<span style="color:blue;">new</span>&nbsp;System.<span style="color:#2b91af;">EventHandler</span>(&nbsp;Module_Shutdown&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">#endregion</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">WallUpdaterApplication</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IExternalApplication</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;OnStartup(&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Register&nbsp;wall&nbsp;updater&nbsp;with&nbsp;Revit</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">WallUpdater</span>&nbsp;updater&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">WallUpdater</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a.ActiveAddInId&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UpdaterRegistry</span>.RegisterUpdater(&nbsp;updater&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Change&nbsp;Scope&nbsp;=&nbsp;any&nbsp;Wall&nbsp;element</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementClassFilter</span>&nbsp;wallFilter
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementClassFilter</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Change&nbsp;type&nbsp;=&nbsp;element&nbsp;addition</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UpdaterRegistry</span>.AddTrigger(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;updater.GetUpdaterId(),&nbsp;wallFilter,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>.GetChangeTypeElementAddition()&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">Result</span>&nbsp;OnShutdown(&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">WallUpdater</span>&nbsp;updater&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">WallUpdater</span>(&nbsp;a.ActiveAddInId&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UpdaterRegistry</span>.UnregisterUpdater(&nbsp;updater.GetUpdaterId()&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Result</span>.Succeeded;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">WallUpdater</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IUpdater</span>
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">AddInId</span>&nbsp;m_appId;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">UpdaterId</span>&nbsp;m_updaterId;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">WallType</span>&nbsp;m_wallType&nbsp;=&nbsp;<span style="color:blue;">null</span>;
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;constructor&nbsp;takes&nbsp;the&nbsp;AddInId&nbsp;for&nbsp;the&nbsp;add-in</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;associated&nbsp;with&nbsp;this&nbsp;updater</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;WallUpdater(&nbsp;<span style="color:#2b91af;">AddInId</span>&nbsp;id&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_appId&nbsp;=&nbsp;id;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_updaterId&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">UpdaterId</span>(&nbsp;m_appId,&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Guid</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;FBFBF6B2-4C06-42d4-97C1-D1B4EB593EFF&quot;</span>&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Execute(&nbsp;<span style="color:#2b91af;">UpdaterData</span>&nbsp;data&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;data.GetDocument();
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Cache&nbsp;the&nbsp;wall&nbsp;type</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;m_wallType&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;hello&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;world&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;m_wallType&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;hello&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;world&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;GetAdditionalInformation()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#a31515;">&quot;Wall&nbsp;type&nbsp;updater&nbsp;example:&nbsp;updates&nbsp;all&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;newly&nbsp;created&nbsp;walls&nbsp;to&nbsp;a&nbsp;special&nbsp;wall&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">ChangePriority</span>&nbsp;GetChangePriority()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">ChangePriority</span>.FloorsRoofsStructuralWalls;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">UpdaterId</span>&nbsp;GetUpdaterId()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;m_updaterId;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">string</span>&nbsp;GetUpdaterName()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#a31515;">&quot;Wall&nbsp;Type&nbsp;Updater&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

All the samples I've found so far appear to be written as add-ins.

I found one single sample showing an `IUpdater` in a macro, 
for [wrangling revisions with Ruby](https://thebuildingcoder.typepad.com/blog/2014/02/wrangling-revisions-with-ruby.html).
Unfortunately, that's a bit too complex for me to follow at this point, especially in Ruby.

I was hoping to find an RVT with a vanilla C# `IUpdater` macro embedded, similar to the SDK *Revit_Macro_Samples.rvt*.

####<a name="3"></a> Solution

The Boost your BIM article
on [automatically running API code when your model changes](https://boostyourbim.wordpress.com/2012/12/17/automatically-run-api-code-when-your-model-changes) looks
promising...

Yes, I got it working!

Here are the steps I followed:

- Use MacroManager to create a new C# module.
- Edit the new module in SharpDevelop.
- Add `RegisterUpdater` to `Module_Startup` as follows:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Module_Startup(&nbsp;<span style="color:blue;">object</span>&nbsp;sender,&nbsp;<span style="color:#2b91af;">EventArgs</span>&nbsp;e&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;RegisterUpdater();
&nbsp;&nbsp;}
</pre>

- Add `UnregisterUpdater` to `Module_Shutdown` as above.

- Just below the boilerplate `#endregion`, add the boostyourbim code:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">FamilyInstanceUpdater</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IUpdater</span>
&nbsp;&nbsp;{&nbsp;...&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;RegisterUpdater()
&nbsp;&nbsp;{&nbsp;...&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;UnregisterUpdater()
&nbsp;&nbsp;{&nbsp;...&nbsp;}
</pre>

Make sure `FamilyInstanceUpdater`, `RegisterUpdater`, and `UnregisterUpdater` are inside the scope of `ThisDocument`.

For testing, I found it handy to replace boostyourbim's `Execute` code with this:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">void</span>&nbsp;Execute(&nbsp;<span style="color:#2b91af;">UpdaterData</span>&nbsp;data&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;data.GetDocument();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Revit&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;hello&quot;</span>&nbsp;);
&nbsp;&nbsp;}
</pre>

- Save and hit F9 to build the macro.
Check the MacroManager to see if the build was successful.
For some reason, the build occasionally fails for me.
I've found that having AssemblyInfo.cs open in SharpDevelop helps.
No idea why.

At this point, the macro should be working.
Any time you draw an element, you'll get a little popup that says 'hello'.

If you close your project with a successfully built macro, the macro will run automatically the next time you open the project.

I'm sure lots of improvements can be made.
For instance, Jeremy recommended using DocumentOpened.
And my version currently works for columns and beams, but not for walls.
Any input would be appreciated.

Meanwhile, thanks very much to Jeremy for taking a look and to boostyourbim for their code.
Hope this helps someone.

Many thanks to Dave for his research and sharing the solution!

####<a name="4"></a> Drill Up the Filter

The initial version works for columns and beams, but not for walls.

This is due to the `FamilyInstanceUpdater`.

It defines a `familyInstanceFilter` variable as `new ElementClassFilter( typeof( FamilyInstance ) )`.

Beams and columns are family instances, and walls are not.

To expand the filter to include `Wall` objects as well as `FamilyInstance` objects, you can change its name and definition to something like this:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ElementFilter</span>&nbsp;f&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">LogicalOrFilter</span>(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementClassFilter</span>(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">FamilyInstance</span>&nbsp;)&nbsp;),
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementClassFilter</span>(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;)&nbsp;);
</pre>

<center>
<p/>
<br/>
<img src="img/macro_photography.png" alt="Macro photography" width="500">
</center>
