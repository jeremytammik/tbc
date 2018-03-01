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

Changing text colour via the text note type in #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/2CR6M2k

Here are some notes on changing text colour and a nice <code>node.js</code> web scraping tutorial
&ndash; Changing text colour via the text note type
&ndash; Web scraping using <code>node.js</code>...

--->

### Changing Text Colour

Lately, I have been spending more time participating in the interesting discussions in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) than writing blog posts.

Occasionally, I grab some new code snippet worth preserving for posterity and add it
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples).

Even more occasionally, I actually test it &nbsp; :-) &nbsp;

I did so now for the trivial task of changing text colour.

Here are some notes on that and a nice `node.js` web scraping tutorial:

- [Changing text colour via the text note type](#2) 
- [Web scraping using `node.js`](#3) 


####<a name="2"></a>Changing Text Colour via the Text Note Type

This topic came up when Rudi [@Revitalizer](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1103138) Honke
added a comment on colours in the thread 
on [changing colour of labels within tags family per tag instance](https://forums.autodesk.com/t5/revit-api-forum/changing-color-of-labels-within-tags-family-per-tag-instance/m-p/7794532), saying,

> colour parameters are of type integer, but the raw value may be difficult to read for the user.

He explained how to read them in the earlier thread 
on [how to change text colour](https://forums.autodesk.com/t5/revit-api-forum/how-to-change-text-color/td-p/2567672):

**Question:** I am using the `ColorDialog` control from Visual Studio 2008 to select a colour, and then I retrieve the RGB components in three variables:

- ColorComponentRed
- ColorComponentGreen
- ColorComponentBlue

I want to assign this colour to some text, but the following code is not working:

<pre class="code">
  <span style="color:blue;">Dim</span>&nbsp;colorparam&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Parameter</span>
  colorparam&nbsp;=&nbsp;elem.ObjectType.Parameter(
  &nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.TEXT_COLOR)
   
  <span style="color:blue;">Dim</span>&nbsp;app&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">New</span>&nbsp;Autodesk.Revit.Creation.<span style="color:#2b91af;">Application</span>()
  <span style="color:blue;">Dim</span>&nbsp;color&nbsp;<span style="color:blue;">As</span>&nbsp;Autodesk.Revit.Color&nbsp;=&nbsp;app.NewColor()
   
  color.Red&nbsp;=&nbsp;ColorComponentRed
  color.Green&nbsp;=&nbsp;ColorComponentGreen
  color.Blue&nbsp;=&nbsp;ColorComponentBlue
   
  colorparam.Set(color)
</pre>

Can anybody tell me what I am doing wrong and how to do it properly, please?

**Answer:** Hi.

<pre class="code">
  <span style="color:blue;">private</span>&nbsp;<span style="color:blue;">int</span>&nbsp;GetRevitTextColorFromSystemColor(
  &nbsp;&nbsp;System.Drawing.<span style="color:#2b91af;">Color</span>&nbsp;color&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;(<span style="color:blue;">int</span>)&nbsp;color.R&nbsp;*&nbsp;(<span style="color:blue;">int</span>)&nbsp;<span style="color:#2b91af;">Math</span>.Pow(&nbsp;2,&nbsp;0&nbsp;)&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;(<span style="color:blue;">int</span>)&nbsp;color.G&nbsp;*&nbsp;(<span style="color:blue;">int</span>)&nbsp;<span style="color:#2b91af;">Math</span>.Pow(&nbsp;2,&nbsp;8&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;(<span style="color:blue;">int</span>)&nbsp;color.B&nbsp;*&nbsp;(<span style="color:blue;">int</span>)&nbsp;<span style="color:#2b91af;">Math</span>.Pow(&nbsp;2,&nbsp;16&nbsp;);
  }
</pre>

I know that 2^0 is just 1, so you can simplify that to:

<pre class="code">
  <span style="color:blue;">private</span>&nbsp;<span style="color:blue;">int</span>&nbsp;GetRevitTextColorFromSystemColor(
  &nbsp;&nbsp;System.Drawing.<span style="color:#2b91af;">Color</span>&nbsp;color&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;(<span style="color:blue;">int</span>)&nbsp;color.R
  &nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;(<span style="color:blue;">int</span>)&nbsp;color.G&nbsp;*&nbsp;(<span style="color:blue;">int</span>)&nbsp;<span style="color:#2b91af;">Math</span>.Pow(&nbsp;2,&nbsp;8&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;(<span style="color:blue;">int</span>)&nbsp;color.B&nbsp;*&nbsp;(<span style="color:blue;">int</span>)&nbsp;<span style="color:#2b91af;">Math</span>.Pow(&nbsp;2,&nbsp;16&nbsp;);
  }
</pre>

Then, for example:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;color&nbsp;=&nbsp;GetRevitTextColorFromSystemColor(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;yourSystemColor&nbsp;);
 
&nbsp;&nbsp;textNoteType.get_Parameter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.LINE_COLOR&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Set(&nbsp;color&nbsp;);
</pre>

Also note that starting with Revit 2017, the Revit API provides a `ColorSelectionDialog`.

You can use its `SelectedColor` property to get a Revit colour instead of a system colour.

Richard [@RPTHOMAS108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas added some 'quite convenient extension methods' to that:

<pre class="code">
  &lt;Extension()&gt;
  <span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;AsRGB(<span style="color:blue;">ByVal</span>&nbsp;Parameter&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Parameter</span>)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Byte</span>()
  &nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;I&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>&nbsp;=&nbsp;Parameter.AsInteger
  &nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Red&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Byte</span>&nbsp;=&nbsp;I&nbsp;<span style="color:blue;">Mod</span>&nbsp;256
  &nbsp;&nbsp;I&nbsp;=&nbsp;I&nbsp;\&nbsp;256
  &nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Green&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Byte</span>&nbsp;=&nbsp;I&nbsp;<span style="color:blue;">Mod</span>&nbsp;256
  &nbsp;&nbsp;I&nbsp;=&nbsp;I&nbsp;\&nbsp;256
  &nbsp;&nbsp;<span style="color:blue;">Dim</span>&nbsp;Blue&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Byte</span>&nbsp;=&nbsp;I&nbsp;<span style="color:blue;">Mod</span>&nbsp;256
  &nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;<span style="color:blue;">New</span>&nbsp;<span style="color:blue;">Byte</span>(2)&nbsp;{Red,&nbsp;Green,&nbsp;Blue}
  <span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
  &lt;Extension()&gt;
  <span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;AsParameterValue(<span style="color:blue;">ByVal</span>&nbsp;Color&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:#2b91af;">Color</span>)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>
  &nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Color.Red&nbsp;+&nbsp;(256&nbsp;*&nbsp;Color.Green)&nbsp;+&nbsp;(65536&nbsp;*&nbsp;Color.Blue)
  <span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
  &lt;Extension()&gt;
  <span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;AsParameterValue(<span style="color:blue;">ByVal</span>&nbsp;Color&nbsp;<span style="color:blue;">As</span>&nbsp;Windows.Media.Color)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>
  &nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Color.R&nbsp;+&nbsp;(256&nbsp;*&nbsp;Color.G)&nbsp;+&nbsp;(65536&nbsp;*&nbsp;Color.B)
  <span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
  &lt;Extension()&gt;
  <span style="color:blue;">Public</span>&nbsp;<span style="color:blue;">Function</span>&nbsp;AsParameterValue(<span style="color:blue;">ByVal</span>&nbsp;Color&nbsp;<span style="color:blue;">As</span>&nbsp;System.Drawing.Color)&nbsp;<span style="color:blue;">As</span>&nbsp;<span style="color:blue;">Integer</span>
  &nbsp;&nbsp;<span style="color:blue;">Return</span>&nbsp;Color.R&nbsp;+&nbsp;(256&nbsp;*&nbsp;Color.G)&nbsp;+&nbsp;(65536&nbsp;*&nbsp;Color.B)
  <span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Function</span>
</pre>

Many thanks to Rudi and Richard for the helpful solutions!

I added Rudi's utility function
to [The Building Coder samples Util.cs module](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/Util.cs#L455-L488) like
this:

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">#region</span>&nbsp;Colour&nbsp;Conversion
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Revit&nbsp;text&nbsp;colour&nbsp;parameter&nbsp;value&nbsp;stored&nbsp;as&nbsp;an&nbsp;integer&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;in&nbsp;text&nbsp;note&nbsp;type&nbsp;BuiltInParameter.LINE_COLOR.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">int</span>&nbsp;ToColorParameterValue(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;red,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;green,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;blue&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;red&nbsp;+&nbsp;(green&nbsp;&lt;&lt;&nbsp;8)&nbsp;+&nbsp;(blue&nbsp;&lt;&lt;&nbsp;16);
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Revit&nbsp;text&nbsp;colour&nbsp;parameter&nbsp;value&nbsp;stored&nbsp;as&nbsp;an&nbsp;integer&nbsp;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;in&nbsp;text&nbsp;note&nbsp;type&nbsp;BuiltInParameter.LINE_COLOR.</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">int</span>&nbsp;GetRevitTextColorFromSystemColor(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;System.Drawing.<span style="color:#2b91af;">Color</span>&nbsp;color&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;ToColorParameterValue(&nbsp;color.R,&nbsp;color.G,&nbsp;color.B&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Colour&nbsp;Conversion</span></pre>
</pre>

To test it, I added an additional transaction step
to [CmdNewTextNote](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdNewTextNote.cs#L196-L216):

<pre class="code">
<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
{
&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Change&nbsp;Text&nbsp;Colour&quot;</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;color&nbsp;=&nbsp;<span style="color:#2b91af;">Util</span>.ToColorParameterValue(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;255,&nbsp;0,&nbsp;0&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;textNoteType&nbsp;=&nbsp;doc.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;txNote.GetTypeId()&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param&nbsp;=&nbsp;textNoteType.get_Parameter(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.LINE_COLOR&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Note&nbsp;that&nbsp;this&nbsp;modifies&nbsp;the&nbsp;existing&nbsp;text&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;note&nbsp;type&nbsp;for&nbsp;all&nbsp;instances&nbsp;using&nbsp;it.&nbsp;If</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;not&nbsp;desired,&nbsp;use&nbsp;Duplicate()&nbsp;first.</span>
 
&nbsp;&nbsp;param.Set(&nbsp;color&nbsp;);
 
&nbsp;&nbsp;t.Commit();
}
</pre>

As the comment says, this modifies the text note type and consequently all text note instances referring to that type.

<center>
<img src="img/text_note_color.png" alt="Text note colour" width="443"/>
</center>


####<a name="3"></a>Web Scraping Using Node.js

On a different tack, if you are interested in `node.js` and web scraping, here is a very pleasant and super clear 27-minute step by step introduction to basic [web scraping with `node.js`](https://www.youtube.com/watch?v=eUYMiztBEdY):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/eUYMiztBEdY" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center>

It implements a minimal app live using the `request-promise` and `cheerio` libraries to gather and correlate data from two web sites.

