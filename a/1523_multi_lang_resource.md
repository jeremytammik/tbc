<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 12610584 [Why the '/language' key switches CurrentCulture instead of CurrentUICulture?]
  http://forums.autodesk.com/t5/revit-api-forum/why-the-language-key-switches-currentculture-instead-of/m-p/6837625

- march accelerator in SF is at risk, please sign up
  accelerator in gothenburg and barcelona

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

A large contribution today from Andrey Bushman, and a couple of upcoming Forge events
&ndash; Supporting multiple language resource files
&ndash; Creating and using localised resource <code>RESX</code> files
&ndash; Upcoming Forge accelerators...

-->

### Multiple Language RESX Resource Files

A large contribution today from Andrey Bushman, and a couple of upcoming Forge events:

- [Supporting multiple language resource files](#2)
- [Creating and using localised resource `RESX` files](#3)
- [Upcoming Forge accelerators](#4)


#### <a name="2"></a>Supporting Multiple Language Resource Files

Andrey Bushman raised another interesting issue in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [why the '/language' key switches `CurrentCulture` instead of `CurrentUICulture`](http://forums.autodesk.com/t5/revit-api-forum/why-the-language-key-switches-currentculture-instead-of/m-p/6837625)

In the course of our discussion, Andrey pointed out his highly interesting blog and shared a fully functional Revit add-in demonstrating how to support multi-language resources with all conceivable frills, bells and whistles:

- [Andrey's Revit programming notes blog](https://revit-addins.blogspot.ru/2017/01/revit-201711.html) (in Russian)
- [RevitMultiLanguageAddInExample GitHub repository](https://github.com/Andrey-Bushman/RevitMultiLanguageAddInExample) (in C# .NET)

If, like me, your command of Russian is limited, it helps to switch
on [automatic translation](https://chrome.google.com/webstore/detail/google-translate/aapbdbdomjkkjkaonfhkkikfgjllcleb) for
the Russian blog  :-)

By the way, this add-in obviously also makes use of
Andrey's [NuGet Revit API package](http://thebuildingcoder.typepad.com/blog/2016/12/nuget-revit-api-package.html),
now updated to support the recent additional Revit 2017.X.Y releases.

Andrey's reason for raising the thread in the first place was a weird behaviour setting the UI culture in Revit 2017.1.1, which I passed on to the development team for further exploration.

However, Andrey provides a workaround for that too, in the module 
[RevitPatches.cs](https://github.com/Andrey-Bushman/RevitMultiLanguageAddInExample/blob/master/RevitMultiLanguageAddInExample/RevitPatches.cs):

<pre class="code">
span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">RevitPatches</span>
{
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;This&nbsp;patch&nbsp;fixes&nbsp;the&nbsp;bug&nbsp;of&nbsp;localization&nbsp;switching</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;for&nbsp;Revit&nbsp;2017.1.1.&nbsp;It&nbsp;switches&nbsp;the&nbsp;&#39;Thread</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;.CurrentThread.CurrentUICulture&#39;&nbsp;and&nbsp;&#39;Thread</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;.CurrentThread.CurrentCulture&#39;&nbsp;properties&nbsp;according&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;the&nbsp;UI&nbsp;localization&nbsp;of&nbsp;Revit&nbsp;current&nbsp;session.</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">param</span><span style="color:gray;">&nbsp;name</span><span style="color:gray;">=</span><span style="color:gray;">&quot;</span>lang<span style="color:gray;">&quot;</span><span style="color:gray;">&gt;</span><span style="color:green;">The&nbsp;target&nbsp;language.</span><span style="color:gray;">&lt;/</span><span style="color:gray;">param</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;PatchCultures(&nbsp;<span style="color:#2b91af;">LanguageType</span>&nbsp;lang&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!<span style="color:#2b91af;">Enum</span>.IsDefined(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">LanguageType</span>&nbsp;),&nbsp;lang&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentException</span>(&nbsp;<span style="color:blue;">nameof</span>(&nbsp;lang&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;language;
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">switch</span>(&nbsp;lang&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Unknown:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.English_USA:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;en-US&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.German:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;de-DE&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Spanish:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;es-ES&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.French:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;fr-FR&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Italian:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;it-IT&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Dutch:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;nl-BE&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Chinese_Simplified:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;zh-CHS&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Chinese_Traditional:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;zh-CHT&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Japanese:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;ja-JP&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Korean:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;ko-KR&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Russian:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;ru-RU&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Czech:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;cs-CZ&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Polish:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;pl-PL&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Hungarian:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;hu-HU&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">case</span>&nbsp;<span style="color:#2b91af;">LanguageType</span>.Brazilian_Portuguese:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;language&nbsp;=&nbsp;<span style="color:#a31515;">&quot;pt-BR&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">break</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">default</span>:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Maybe&nbsp;new&nbsp;value&nbsp;of&nbsp;the&nbsp;enum&nbsp;hasn&#39;t&nbsp;own&nbsp;</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;`case`...</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentException</span>(&nbsp;<span style="color:blue;">nameof</span>(&nbsp;lang&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CultureInfo</span>&nbsp;ui_culture&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CultureInfo</span>(&nbsp;language&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CultureInfo</span>&nbsp;culture&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CultureInfo</span>(&nbsp;language&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Thread</span>.CurrentThread.CurrentUICulture&nbsp;=&nbsp;ui_culture;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Thread</span>.CurrentThread.CurrentCulture&nbsp;=&nbsp;culture;
&nbsp;&nbsp;}
}
</pre>

Here is a summary of our discussion consisting mainly of Andrey's explanation:

My external application (add-in) has two localizations: English (by default) and Russian. I now noticed that my external application (add-in) uses wrong localization on some computers. One of those computers has a 'clear' Revit without any other installed custom add-ins.
 
If I launch `revit.exe` with the `/language RUS` command line option, then the Revit UI is Russian, but my add-in  UI still uses the default localization (i.e. "en") instead of "ru".
 
I extract the localized resources through the `ResourcesManager` class in my code. That is the native way to work with resources in .NET. I know that `ResourcesManager` chooses the localized resources based on the value of  `Thread.CurrentThread.CurrentUICulture`.
 
Therefore, I assumed that this value isn't `"ru"` or `"ru-RU"`, for some reason, despite the `/language RUS` key. I checked and confirmed that assumption (look my code below):  the `Thread.CurrentThread.CurrentUICulture` property is set to `"en"` instead of `"ru"`. Also, I see that `Thread.CurrentThread.CurrentCulture` uses `"ru-RU"` instead of `"en"`.
 
Why does the `/language` key not switch the setting of `Thread.CurrentThread.CurrentUICulture` responsible for the user interface (UI)?

Is it bug?

<pre class="code">
<span style="color:#2b91af;">Result</span>&nbsp;<span style="color:#2b91af;">IExternalApplication</span>.OnStartup(
&nbsp;&nbsp;<span style="color:#2b91af;">UIControlledApplication</span>&nbsp;application&nbsp;)
{
&nbsp;&nbsp;<span style="color:green;">//&nbsp;...</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;I&nbsp;use&nbsp;the&nbsp;`/language&nbsp;RUS`&nbsp;key&nbsp;for&nbsp;revit.exe</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;But&nbsp;I&nbsp;see&nbsp;that&nbsp;my&nbsp;add-in&nbsp;user&nbsp;the&nbsp;&#39;default&#39;&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;localization&nbsp;instead&nbsp;of&nbsp;&#39;ru&#39;.&nbsp;Hm...</span>
&nbsp;&nbsp;<span style="color:green;">//</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Ok,I&nbsp;will&nbsp;check&nbsp;the&nbsp;CurrentCulture&nbsp;and&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;CurrentUICulture&nbsp;&nbsp;values...</span>
&nbsp;&nbsp;<span style="color:green;">//</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Oops...&nbsp;Localization&nbsp;was&nbsp;changed&nbsp;by&nbsp;the&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;`/language&nbsp;RUS`&nbsp;key,&nbsp;but&nbsp;not&nbsp;for&nbsp;that&nbsp;thread&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;which&nbsp;shall&nbsp;to&nbsp;have&nbsp;this&nbsp;change!&nbsp;Why???</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">CultureInfo</span>&nbsp;n&nbsp;=&nbsp;<span style="color:#2b91af;">Thread</span>.CurrentThread.CurrentCulture;&nbsp;<span style="color:green;">//&nbsp;ru-RU</span>
&nbsp;&nbsp;<span style="color:#2b91af;">CultureInfo</span>&nbsp;m&nbsp;=&nbsp;<span style="color:#2b91af;">Thread</span>.CurrentThread.CurrentUICulture;&nbsp;<span style="color:green;">//&nbsp;en</span>
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;I&nbsp;can&nbsp;fix&nbsp;this&nbsp;problem&nbsp;myself:</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;The&nbsp;CurrentUICulture&nbsp;switching&nbsp;fixes&nbsp;the&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;problem,&nbsp;but&nbsp;why&nbsp;such&nbsp;problem&nbsp;occurs&nbsp;in&nbsp;Revit?</span>
 
&nbsp;&nbsp;<span style="color:#2b91af;">CultureInfo</span>&nbsp;k&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CultureInfo</span>(&nbsp;<span style="color:#a31515;">&quot;ru&quot;</span>&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">Thread</span>.CurrentThread.CurrentUICulture&nbsp;=&nbsp;k;
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Now&nbsp;my&nbsp;add-in&nbsp;uses&nbsp;right&nbsp;localization.&nbsp;</span>
 
&nbsp;&nbsp;<span style="color:green;">//&nbsp;...</span>
}
</pre>

Later, I found that this problem exists in Revit 2017.1.1, but this problem is absent in Revit 2017.
 
For demonstration of this problem I created the project and published it on bitbucket,
in [Andrey-Bushman/research_of_revit_culture_switching_problem](https://bitbucket.org/Andrey-Bushman/research_of_revit_culture_switching_problem).

You can fix this problem in Revit 2017.1.1 by using a class like this:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">sealed</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">UICultureSwitcher</span>&nbsp;:&nbsp;<span style="color:#2b91af;">IDisposable</span>
{
&nbsp;&nbsp;<span style="color:#2b91af;">CultureInfo</span>&nbsp;previous;
 
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;UICultureSwitcher()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">CultureInfo</span>&nbsp;culture&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CultureInfo</span>(&nbsp;<span style="color:#2b91af;">Thread</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.CurrentThread.CurrentCulture.Name&nbsp;);
 
&nbsp;&nbsp;&nbsp;&nbsp;previous&nbsp;=&nbsp;<span style="color:#2b91af;">Thread</span>.CurrentThread.CurrentUICulture;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Thread</span>.CurrentThread.CurrentUICulture&nbsp;=&nbsp;culture;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:blue;">void</span>&nbsp;<span style="color:#2b91af;">IDisposable</span>.Dispose()
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Thread</span>.CurrentThread.CurrentUICulture&nbsp;=&nbsp;previous;
&nbsp;&nbsp;}
}
</pre>

Place code of methods of each external command and external application inside a `using` block with this class member initializing, and the problem will be fixed. 

Expanded info and the patch are provided in the [Russian blog post on Revit 2017.1.1](https://revit-addins.blogspot.ru/2017/01/revit-201711.html).

I also created a multi-language add-in example, published in
the [RevitMultiLanguageAddInExample GitHub repository](https://github.com/Andrey-Bushman/RevitMultiLanguageAddInExample).

When you use the `/language RUS` key the UI of my add-in, its tooltip, expanded tooltip, and help file displayed on pressing F1 are in Russian.

When you use `/language ENU` key the UI of my add-in, its tooltip, expanded tooltip, and help file displayed on pressing F1 are in English.
 
The same applies for the TaskDialog content.

The expected correct behaviour is shown in the
[two-minute video on Revit lang bug](https://www.youtube.com/watch?v=f1IBVgP7T1I):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/f1IBVgP7T1I?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

Many thanks to Andrey for this beautiful and illuminating sample!


#### <a name="3"></a>Creating and Using Localised Resource `RESX` Files

Andrey just published
an [eight-minute video tutorial on resx using](https://www.youtube.com/watch?v=DKCm3p9Gp9M) explaining
the simplest way to create and make use of localized add-in  resources.

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/DKCm3p9Gp9M?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

Additional information and expanded code examples are available from his Russian blog:

- [Binding commands to the help system](https://revit-addins.blogspot.ru/2017/01/blog-post_28.html)
- [Fix Revit 2017.1.1 localisation issue](https://revit-addins.blogspot.ru/2017/01/revit-201711.html)


#### <a name="4"></a>Upcoming Forge Accelerators

We have several [Forge accelerators](http://autodeskcloudaccelerator.com/) coming up
in the [next couple of months](http://autodeskcloudaccelerator.com/prague-2/):

- San Francisco, USA &ndash; March 6-10
- Gothenburg, Sweden &ndash; March 27-30
- Barcelona, Spain &ndash; June 12-16

<center>
<img src="img/2017-02_upcoming_accelerators.png" alt="Upcoming Forge accelerators" width="668"/>
</center>

I am planning on attending the two European ones and would love to see you there too.

Before that, however, the San Francisco accelerator provides the very next chance to attend &ndash; and the early bird gets the worm &ndash; so grab you chance while you can!

<center>
<img src="img/bird_with_worm.png" alt="Bird with worm" width="183"/>
</center>

