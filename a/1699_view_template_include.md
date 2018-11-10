<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


View template include setting in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/viewtemplateinclude

A solution to a longstanding question on the view template 'include' setting
&ndash; Does the Revit API provide any access to the view template 'include' settings defined by the check boxes in this form?
&ndash; I can get the 'includes' via <code>viewTemplate.GetNonControlledTemplateParameterIds</code>.
The method returns a list of parameter ids, and you can then use <code>viewTemplate.Parameters</code> to map them...

-->

### View Template Include Setting

A quick note to highlight a solution shared by Teocomi to solve a longstanding question in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [view template 'include'](http://forums.autodesk.com/t5/revit-api/view-template-quot-include-quot/m-p/5410347):

**Question:** Does the Revit API provide any access to the view template 'include' settings defined by the check boxes in this form?

<center>
<img src="img/view_template_include_check_boxes.jpg" alt="View template include checkboxes" width="652">
</center>

**Answer:** I can get the 'includes' via `viewTemplate.GetNonControlledTemplateParameterIds`.

The method returns a list of parameter ids, and you can then use `viewTemplate.Parameters` to map them.

The same also works for setting them, cf. the following example:

<pre class="code">
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;list&nbsp;so&nbsp;that&nbsp;I&nbsp;can&nbsp;use&nbsp;linq</span>
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;viewparams&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">Parameter</span>&gt;();
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;p&nbsp;<span style="color:blue;">in</span>&nbsp;viewTemplate.Parameters&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;viewparams.Add(&nbsp;p&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;parameters&nbsp;by&nbsp;name&nbsp;(safety&nbsp;checks&nbsp;needed)</span>
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;modelOverrideParam&nbsp;=&nbsp;viewparams
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;p
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;p.Definition.Name&nbsp;==&nbsp;<span style="color:#a31515;">&quot;V/G&nbsp;Overrides&nbsp;Model&quot;</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.First();
 
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;viewScaleParam&nbsp;=&nbsp;viewparams
&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;p
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;p.Definition.Name&nbsp;==&nbsp;<span style="color:#a31515;">&quot;View&nbsp;Scale&quot;</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.First();
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;includes</span>
 
&nbsp;&nbsp;viewTemplate.SetNonControlledTemplateParameterIds(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;{
&nbsp;&nbsp;modelOverrideParam.Id,&nbsp;viewScaleParam.Id&nbsp;}&nbsp;);
</pre>

Thank you, Teocomi, for sharing this!
