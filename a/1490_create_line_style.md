<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

Join in the Revit UI can mean several completely different things:

- Family editor, two solids -- CombineElements
- Family editor, two colinear beams of same material -- visual join only, remove edges
- Project editor, two colinear walls of same material -- creates a single wall?
- Project editor, two perpendicular walls of same material -- visual join only, remove edges
- what other joins are there?
- how are they represented and accessible in the API?


<code></code>

How to Create a New Line Style @AutodeskForge #RTCEUR @RTCEvents #revitapi @AutodeskRevit #aec #bim

I arrived safe and sound in Munich to provide support for the one-week Forge accelerator workshop at the Autodesk offices here
&ndash; Arrival in Munich and Hotel Schlicker
&ndash; Official pictures from RTC in Porto
&ndash; Art of README
&ndash; What is Join?
&ndash; How to Create a Line Style...

-->

### How to Create a New Line Style

I arrived safe and sound in Munich to provide support for the
one-week [Forge accelerator](http://autodeskcloudaccelerator.com) workshop
at the Autodesk offices here.

- [Arrival in Munich and Hotel Schlicker](#2)
- [Official pictures from RTC in Porto](#3)
- [Art of README](#4)
- [What is Join?](#5)
- [How to Create a Line Style](#6)


#### <a name="2"></a>Arrival in Munich and Hotel Schlicker

I am visiting a friend here in Munich starting today and needed a hotel for the first night.

I had no time to research and book anything in advance, so I just got off the subway at Marienplatz and started walking.

As luck would have it, I discovered the nicest hotel I have ever stayed at.

With just three stars and a moderate price,
[Hotel Schlicker](http://www.hotel-schlicker.de) beats
every single one of the five-star global hotel chain establishments that I normally have the bad luck to end up in.

Highly recommended!


#### <a name="3"></a>Official Pictures from RTC in Porto

Here are links to the official photo collections from 
the [RTC Revit Technology Conference Europe](http://www.rtcevents.com/rtc2016eur) in
Porto during the last few days:

- [Highlights Day 1](https://www.dropbox.com/sh/4ni6jamslxbc2yn/AACcdvHV_E4YogTswTF4nBPza?dl=0)
- [Highlights Day 2](https://www.dropbox.com/sh/7lqifbca203ttg1/AAB3IksT6rRdY3Ofs-xGe9sea?dl=0)
- [Highlights Day 3](https://www.dropbox.com/sh/m4rhkakiseh7pyz/AADZxZYhJcLWF41Utw7wb2AOa?dl=0)
 
Thanks to Lejla Secerbegovic for sharing these!

Here are my own pictures from the RTC Saturday evening gala dinner in the [Palácio da Bolsa](https://en.wikipedia.org/wiki/Pal%C3%A1cio_da_Bolsa), including the one and only Jay Zallan in his beautiful stripy fur coat in the arabic hall, a perfect fit tor the highlight of the palace, decorated in the exotic Moorish Revival style, used as reception hall for visiting personalities and heads of state:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157672114668283" title="RTC Gala Dinner"><img src="https://c3.staticflickr.com/6/5645/29924672034_fb23f87594_n.jpg" width="320" height="240" alt="RTC Gala Dinner"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>


#### <a name="4"></a>Art of README

A very nice and fundamentally important article for anyone sharing code, whether on GitHub or elsewhere, pointed out by  [Philippe Leefsma](http://twitter.com/F3lipek):

<center>
<span style="font-size: 120%; font-weight: bold">
[Art of README](https://github.com/noffle/art-of-readme)
</span>
</center>

Thank you, Philippe, for this instructive read.



#### <a name="5"></a>What is Join?

I made a shocking discovery in a discussion with a participant here at the Forge accelerator.

I thought the Revit 'Join' operation was a sort of Boolean operation.

Apparently, that is not the case at all, at least not always.

In some cases, it is just a means to suppress the display of certain edges between adjacent collinear and coplanar elements of the same type and material.

Maybe this is also the functionality driven by the [JoinGeometryUtils class](http://www.revitapidocs.com/2017/c45b6484-3efd-1d81-0b47-ba678857fff1.htm)?

Not a Boolean operation at all?

Under other circumstances, it can apparently mean other things.

For instance, in the family editor, joining two solids may or may not cause a Boolean operation, which may or may not correspond to the [CombineElements method](http://www.revitapidocs.com/2017/5c33a711-2891-f353-5f39-24ba175be452.htm).

I would love to have more clarity on this, and so might others as well...


#### <a name="6"></a>How to Create a Line Style

Scott Conover, Senior Engineering Manager of the Revit development team, pointed out the following important Revit 2017 API enhancement that now fully enables the programmatic creation of new line styles:

**Question:** How can I create a new line style?

**Answer:** Creating a line style is really easy.

One little stumbling block relates to terminology, as there is no `LineStyle` object in the Revit API.   

The `LineStyle` is in fact represented by a `GraphicsStyle` element in the API.

Actually new `LineStyle` instances are defined as subcategories of `Line`, so are you can set one up by making a new subcategory and setting its weight, colour, and pattern as you please.
 
Up until 2017, there was a gap around assigning the line pattern to the category. 

Happily, this is now exposed in Revit 2017.
 
Here is a macro that creates and sets up a new Line Style:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Create&nbsp;a&nbsp;new&nbsp;line&nbsp;style&nbsp;using&nbsp;NewSubcategory</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">void</span>&nbsp;CreateLineStyle(<span style="color:#2b91af;">Document</span>&nbsp;doc)
{
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Use&nbsp;this&nbsp;to&nbsp;access&nbsp;the&nbsp;current&nbsp;document&nbsp;in&nbsp;a&nbsp;macro.</span>
&nbsp;&nbsp;<span style="color:green;">//</span>
&nbsp;&nbsp;<span style="color:green;">//Document&nbsp;doc&nbsp;=&nbsp;this.ActiveUIDocument.Document;</span>
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Find&nbsp;existing&nbsp;linestyle.  Can&nbsp;also&nbsp;opt&nbsp;to</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;create&nbsp;one&nbsp;with&nbsp;LinePatternElement.Create()</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;fec&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">LinePatternElement</span>&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">LinePatternElement</span>&nbsp;linePatternElem&nbsp;=&nbsp;fec
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">LinePatternElement</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.First&lt;<span style="color:#2b91af;">LinePatternElement</span>&gt;(&nbsp;linePattern&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;linePattern.Name&nbsp;==&nbsp;<span style="color:#a31515;">&quot;Long&nbsp;dash&quot;</span>&nbsp;);
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;The&nbsp;new&nbsp;linestyle&nbsp;will&nbsp;be&nbsp;a&nbsp;subcategory&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;of&nbsp;the&nbsp;Lines&nbsp;category        </span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">Categories</span>&nbsp;categories&nbsp;=&nbsp;doc.Settings.Categories;
 
&nbsp;&nbsp;<span style="color:#2b91af;">Category</span>&nbsp;lineCat&nbsp;=&nbsp;categories.get_Item(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Lines&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Create&nbsp;LineStyle&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Add&nbsp;the&nbsp;new&nbsp;linestyle </span>
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Category</span>&nbsp;newLineStyleCat&nbsp;=&nbsp;categories
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.NewSubcategory(&nbsp;lineCat,&nbsp;<span style="color:#a31515;">&quot;New&nbsp;LineStyle&quot;</span>&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;doc.Regenerate();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;the&nbsp;linestyle&nbsp;properties&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;(weight,&nbsp;color,&nbsp;pattern).</span>
 
&nbsp;&nbsp;&nbsp;&nbsp;newLineStyleCat.SetLineWeight(&nbsp;8,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">GraphicsStyleType</span>.Projection&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;newLineStyleCat.LineColor&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Color</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0xFF,&nbsp;0x00,&nbsp;0x00&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;newLineStyleCat.SetLinePatternId(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;linePatternElem.Id,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">GraphicsStyleType</span>.Projection&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;}
}
</pre>

I added Scott's code 
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) 
[release 2017.0.131.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.130.4) 
in the new module [CmdCreateLineStyle.cs](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCreateLineStyle.cs).

Thank you very much, Scott, for pointing out and demonstrating this important enhancement!
