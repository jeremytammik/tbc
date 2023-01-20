<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

Happy New Year of the Rabbit, xīnnián hǎo, 新年好! Pondering the looming possibility of a dark theme in the @AutodeskRevit UI and #RevitAPI #BIM @AutodeskAPS: Dark theme switching, API, icons and code example to handle themed ribbon icons https://autode.sk/darktheme

Happy New Year of the Rabbit, xīnnián hǎo, 新年好! Pondering the looming possibility of a dark theme in the Revit UI and API
&ndash; Dark theme switching
&ndash; API
&ndash; icons
&ndash; Code example: handling themed ribbon icons
&ndash; New public roadmaps...

linkedin:

Happy New Year of the Rabbit, xīnnián hǎo, 新年好!

Pondering the looming possibility of a dark theme in the Revit UI and #RevitAPI:

Dark theme switching, API, icons and code example to handle themed ribbon icons

https://autode.sk/darktheme

#bim #DynamoBim #AutodeskAPS #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### New Roadmaps, Dark Theme Possibility Looming

Happy New Year of the Rabbit, xīnnián hǎo, 新年好!

<center>
<img src="img/2023_year_of_the_rabbit_1.jpg" alt="Happy New Year of the Rabbit!" title="Happy New Year of the Rabbit!" width="400"/> <!-- 800 × 514 pixels -->
</center>

The Spring Festival is coming up this weekend, starting on Sunday, January 22, celebrating 
the [Chinese New Year](https://en.wikipedia.org/wiki/Chinese_New_Year) and 
another [Year of the Rabbit](https://en.wikipedia.org/wiki/Rabbit_(zodiac)).

In the lunar calendar, 2023 is a Water Rabbit Year.
The sign of the Rabbit is a symbol of longevity, peace, and prosperity in Chinese culture.
2023 is predicted to be a year of hope, especially after the long pandemic period.
Wishing all of us lots of health, energy and happiness in the new year!

In this new year, the Revit development team has another topic to share with us:

- [Dark theme possibility looming](#2)
- [Dark theme switching](#2.1)
- [Dark theme API](#2.2)
- [Dark theme icons](#2.3)
- [Code example: handling themed ribbon icons](#2.4)
- [Dark theme additional notes](#2.5)
- [Autodesk icon guidelines snapshot](#2.6)
- [Autodesk AEC Public Roadmaps](#3)

####<a name="2"></a> Dark Theme Possibility Looming

We recently announced internal thoughts
on [possibly converting the internal representation of Revit element ids from 32 to 64 bit in a future release of Revit](https://thebuildingcoder.typepad.com/blog/2022/11/64-bit-element-ids-maybe.html).

In a similar vein, here is another internal topic being pondered.
Please note the important safe harbor statement concerning these thoughts:
 
> Roadmaps are plans, not promises.
We’re as excited as you to see new functionality make it into the products, but the development, releases, and timing of any features or functionality remains at our sole discretion.
These updates should not be used to make purchasing decisions.

So, the possibility that I would like to present today concerns supporting the Dark Theme and how to handle it in a Revit add-in:

####<a name="2.1"></a> Dark Theme Switching

Setting the UI Active Theme will switch the appearance of the Ribbon between light grey and dark blue, with three options:

- Light
- Dark 
- Use system setting
&ndash; Windows supports light and dark colour schemes.
If you choose this option, Revit will use the Windows colour scheme and switch to a matching theme accordingly.

Light:

<center>
<img src="img/dark_theme_2.png" alt="Dark theme &ndash; light" title="Dark theme &ndash; light" width="500"/> <!-- 624 × 162 pixels -->
</center>

Dark:

<center>
<img src="img/dark_theme_3.png" alt="Dark theme &ndash; dark" title="Dark theme &ndash; dark" width="500"/> <!-- 624 × 160 pixels -->
</center>

The UI Active Theme options can define other colour settings to override the default ones:

<center>
<img src="img/dark_theme_1.png" alt="Dark theme" title="Dark theme" width="584"/> <!-- 584 × 692 pixels -->
</center>
 
####<a name="2.2"></a> Dark Theme API

New properties and events may be added for dark theme support:

- ThemeChangedEventArgs &ndash; Arguments for the ThemeChanged event
- UIThemeManager.CurrentTheme &ndash; Allows you to set /get the overall theme for the Revit session
- UIThemeManager.FollowSystemColorTheme &ndash; Allows you to set /get if the overall theme follows operating system color theme 
- UIThemeManager.CurrentCanvasTheme &ndash; Allows you to set/get a canvas theme for the current Revit session (as opposed to the default theme)
- ColorOption &ndash; Allows you to set/get the colours in the current canvas theme

####<a name="2.3"></a> Dark Theme Icons

Here are samples of the default dark theme ribbon background and button colour settings:

Light ribbon background:

<center>
<img src="img/dark_theme_4.png" alt="Dark theme &ndash; light ribbon background" title="Dark theme &ndash; light ribbon background" width="500"/> <!-- 508 × 160 pixels -->
</center>

Dark ribbon background:

<center>
<img src="img/dark_theme_5.png" alt="Dark theme &ndash; light ribbon background" title="Dark theme &ndash; light ribbon background" width="500"/> <!-- 496 × 160 pixels -->
</center>
 
Light ribbon buttons:

<center>
<img src="img/dark_theme_6.png" alt="Dark theme &ndash; light ribbon buttons" title="Dark theme &ndash; light ribbon buttons" width="1200"/> <!-- 1430 × 415 pixels -->
</center>

Dark ribbon buttons:

<center>
<img src="img/dark_theme_7.png" alt="Dark theme &ndash; dark ribbon buttons" title="Dark theme &ndash; dark ribbon buttons" width="1200"/> <!-- 1430 × 394 pixels -->
</center>
 
- Small button size: 16x16px
- Large button size: 32x32px
- Resolution: 96 DPI
- Icons

####<a name="2.4"></a> Code Example: Handling Themed Ribbon Icons

<div style="border: #000080 1px solid; color: #000; font-family: 'Cascadia Mono', Consolas, 'Courier New', Courier, Monospace; font-size: 10pt">
<div style="background: #f3f3f3; color: #000000; max-height: 500px; overflow: auto">
<ol start="18" style="background: #ffffff; margin: 0; padding: 0;">
<li><span style="color:#0000ff">internal</span> <span style="color:#0000ff">class</span> <span style="color:#2b91af">TestRibbon</span> : IExternalApplication</li>
<li style="background: #f3f3f3">{</li>
<li>&#160; <span style="color:#0000ff">private</span> PushButton m_ribbonBtn;</li>
<li style="background: #f3f3f3">&#160; <span style="color:#0000ff">public</span> Result OnStartup(UIControlledApplication application)</li>
<li>&#160; {</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#0000ff">var</span> ribbonPanel = application.CreateRibbonPanel(<span style="color:#a31515">&quot;33900745-04F5-4CC2-9BAC-3230716E3A54&quot;</span>, <span style="color:#a31515">&quot;Test&quot;</span>);</li>
<li>&#160;&#160;&#160; <span style="color:#0000ff">var</span> buttonData = <span style="color:#0000ff">new</span> PushButtonData(<span style="color:#a31515">&quot;Test&quot;</span>, <span style="color:#a31515">&quot;Test&quot;</span>, <span style="color:#0000ff">typeof</span>(CmdEntry).Assembly.Location, <span style="color:#0000ff">typeof</span>(CmdEntry).FullName);</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; buttonData.AvailabilityClassName = <span style="color:#0000ff">typeof</span>(CmdEntry).FullName;</li>
<li>&#160;&#160;&#160; m_ribbonBtn = ribbonPanel.AddItem(buttonData) <span style="color:#0000ff">as</span> PushButton;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; updateImageByTheme();</li>
<li>&#160;&#160;&#160; application.ThemeChanged += ThemeChanged;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#0000ff">return</span> Result.Succeeded;</li>
<li>&#160; }</li>
<li style="background: #f3f3f3">&#160; <span style="color:#0000ff">private</span> <span style="color:#0000ff">void</span> setButtonImage(<span style="color:#0000ff">string</span> pic, <span style="color:#0000ff">string</span> largePic)</li>
<li>&#160; {</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#0000ff">var</span> assemblyLocation = <span style="color:#0000ff">typeof</span>(TestRibbon).Assembly.Location;</li>
<li>&#160;&#160;&#160; <span style="color:#0000ff">var</span> assemblyDirectory = Path.GetDirectoryName(assemblyLocation);</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#0000ff">var</span> imagePath = Path.Combine(assemblyDirectory, pic);</li>
<li>&#160;&#160;&#160; <span style="color:#0000ff">var</span> largeImagePath = Path.Combine(assemblyDirectory, largePic);</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#0000ff">if</span> (File.Exists(imagePath))</li>
<li>&#160;&#160;&#160;&#160;&#160; m_ribbonBtn.Image = <span style="color:#0000ff">new</span> System.Windows.Media.Imaging.BitmapImage(<span style="color:#0000ff">new</span> Uri(imagePath));</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#0000ff">if</span> (File.Exists(largeImagePath))</li>
<li>&#160;&#160;&#160;&#160;&#160; m_ribbonBtn.LargeImage = <span style="color:#0000ff">new</span> System.Windows.Media.Imaging.BitmapImage(<span style="color:#0000ff">new</span> Uri(largeImagePath));</li>
<li style="background: #f3f3f3">&#160; }</li>
<li>&#160; <span style="color:#0000ff">private</span> <span style="color:#0000ff">void</span> updateImageByTheme()</li>
<li style="background: #f3f3f3">&#160; {</li>
<li>&#160;&#160;&#160; UITheme theme = UIThemeManager.CurrentTheme;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; <span style="color:#0000ff">switch</span> (theme)</li>
<li>&#160;&#160;&#160; {</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> UITheme.Dark:</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; setButtonImage(<span style="color:#a31515">&quot;dark.png&quot;</span>, <span style="color:#a31515">&quot;darkLarge.png&quot;</span>);</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li>&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">case</span> UITheme.Light:</li>
<li style="background: #f3f3f3">&#160;&#160;&#160;&#160;&#160;&#160;&#160; setButtonImage(<span style="color:#a31515">&quot;light.png&quot;</span>, <span style="color:#a31515">&quot;lightLarge.png&quot;</span>);</li>
<li>&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color:#0000ff">break</span>;</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; }</li>
<li>&#160; }</li>
<li style="background: #f3f3f3">&#160; <span style="color:#0000ff">private</span> <span style="color:#0000ff">void</span> ThemeChanged(<span style="color:#0000ff">object</span> sender, Autodesk.Revit.UI.Events.ThemeChangedEventArgs e)</li>
<li>&#160; {</li>
<li style="background: #f3f3f3">&#160;&#160;&#160; updateImageByTheme();</li>
<li>&#160; }</li>
<li style="background: #f3f3f3">}</li>
</ol>
</div>
</div>

####<a name="2.5"></a> Dark Theme Additional Notes

Please note that only the 1st level UI supports the dark theme option.

####<a name="2.6"></a> Autodesk Icon Guidelines Snapshot

Prompted by [Luiz'](https://thebuildingcoder.typepad.com/blog/2023/01/dark-theme-possibility-looming.html#comment-6093239283)
and [Gábor's comments below](https://thebuildingcoder.typepad.com/blog/2023/01/dark-theme-possibility-looming.html#comment-6093873493),
I acquired and posted an up-to-date snapshot of the current state of the Autodesk icon guidelines including the images above:

- [Autodesk icon guidelines PDF](https://thebuildingcoder.typepad.com/icon/2023-01-20_icon_design_guidelines.pdf)
- [Zip file including badges and PNG instructiuons](https://thebuildingcoder.typepad.com/icon/2023-01-20_icon_design_guideline.zip)

Thank you for asking, Luiz and Gábor.

<!--

**Question:** 

**Answer:** 

**Response:** 

https://www.btl-blog.com/between_the_lines/2023/01/aec-product-roadmap-ask-me-anything-series-this-week.html

AEC Product Roadmap Ask Me Anything Series This Week

AEC Product Roadmap Ask Me Anything Series This Week
Join us and learn about highlights from recent releases, including Revit and the AEC Collection and ask the Product Managers and experts about the future of Autodesk AEC products such as Revit, Dynamo, InfraWorks, Civil 3D, ReCap, Structures, and MEP.

Join the conversations!

Tuesday
Architecture and Platform Product (Revit) Roadmap Ask Me Anything
Tuesday, January 17, 2023, at 8:00 am PDT
Computational Design and Automation (Dynamo) Roadmap Ask Me Anything
Tuesday, January 17, 2023, at 10:00 am PDT

Wednesday:
Infrastructure Product Roadmap Ask Me Anything
Wednesday, January 18, 2023, at 8:00 am PDT

Thursday:

Structures Product Roadmap Ask Me Anything
Thursday, January 19, 2022, at 8:00 am PDT
MEP Product Roadmap Ask Me Anything
Thursday, January 19, 2023, at 10:00 am PDT

All these live events will be live in English and have Zoom’s live translation captioning available from English to 11 languages

-->

####<a name="3"></a> Autodesk AEC Public Roadmaps

For more exciting news on possible upcoming product enhancements, check out
the [Autodesk AEC Public Roadmaps](https://blogs.autodesk.com/revit/roadmap) with dozens of items in each of the sections:

- Revit &ndash; Architecture
- Revit &ndash; Structures
- Revit &ndash; MEP
- Dynamo Public Roadmap

