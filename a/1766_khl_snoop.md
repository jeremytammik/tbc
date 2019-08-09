<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- KHL Snoop
John D'Alessandro, Software Engineer at [KLH Engineers, PSC](http://www.klhengrs.com)
We at KLH Engineers decided to roll our own Revit Snoop tool a while ago and we thought we’d show it to you so you can check it out.
We released it as a class library on our GitHub so that others can use it in their own tools and solutions.
We’re also accepting issues and PRs, so the more people use the tool the better it’ll get. Let me know if you have any feedback on it or questions.
The repo is at [KLH Engineers RevitDeveloperTools](https://github.com/klhengineers/RevitDeveloperTools).
It has some similarities and overlap with  [RevitLookup](), and adds a number of features not available there.
The README includes a feature list comparing the two snooping tools:

- 15623936 [Revit API, pull text from annotation tags.]

- vertex handling
  [Q] When processing meshes or other collections of triangles, is there any way to know if one vertex is shared by multiple triangles?
  Thus the final data does not have to contain duplicated vertices, and reuse the vertex index only, in order to save data size.
  [A] I always use the following strategy when collecting mesh data:
  Implement a comparison operator for the mesh vertices
  Store all mesh vertices in their own dictionary, e.g., Dictionary<XYZ,int>. The int is just a dummy. I normally count the number of vertices received.
  Each time I encounter a new mesh vertex, I check whether it is already listed in the dictionary. The comparison operator includes enough fuzz.
  Look at GetCanonicVertices in
  https://thebuildingcoder.typepad.com/blog/2016/01/tracking-element-modification.html
  Here is another related post:
  https://thebuildingcoder.typepad.com/blog/2017/08/birthday-post-on-the-xyz-class.html#3
  In general, search The Building Coder for
  XyzEqualityComparer
  XyzComparable
  XyzProximityComparer

- img/pizza.jpg 720 px pi.z.z.a

twitter:

New PC, KLH database snooping tool, pulling annotation text and vertex handling in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/klhsnoop

I have been very busy and motivated indeed setting up a new computer this week.
Nonetheless, I was able to keep going full steam with Revit API related issues as well
&ndash; Setting up a new MacBook
&ndash; KLH Engineers RevitDeveloperTools snooping tool
&ndash; Pulling text from annotation tags
&ndash; Vertex handling
&ndash; The true meaning of pizza...

linkedin:

New PC, KLH database snooping tool, pulling annotation text and vertex handling in the #RevitAPI

http://bit.ly/klhsnoop

I have been very busy and motivated indeed setting up a new computer this week.

Nonetheless, I was able to keep going full steam with Revit API related issues as well:

- Setting up a new MacBook
- KLH Engineers RevitDeveloperTools snooping tool
- Pulling text from annotation tags
- Vertex handling
- The true meaning of pizza...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

-->

### New PC, KLH Snoop, Annotations and Vertex Handling

I have been very busy and motivated indeed setting up a new computer this week.

Nonetheless, I was able to keep going full steam with Revit API related issues as well:

- [Setting up a new MacBook](#2)
- [KLH Engineers RevitDeveloperTools snooping tool](#3)
- [Pulling text from annotation tags](#4)
- [Vertex handling](#5)
- [The true meaning of pizza](#6)

<center>
<img src="img/baby_lizard_on_macbook_1008.jpg" alt="Baby lizard on MacBook" width="300"> <!-- 1008 -->
</center>

####<a name="2"></a> Setting up a New MacBook

I picked up my new computer on Monday, a MacBook Pro.

Migration worked very well so far, installation was easier than ever before, and I am already almost completely done moving over.

These components are now up and running:

- Google Chrome
- Pulse Secure
- Microsoft Office
- Outlook credentials
- Komodo Edit
- iterm2
- Parallels
- Windows 10
- Visual Studio 2017
- Revit 2020
- VeraCrypt and osxfuse
- Skype
- VLC
- Firefox and downloadhelper
- git
- Personal Komodo editor scripts
- Created and set up new GitHub SSH key
- Migrated personal disk data to new machine
- Restored bash profile
- Python 3.7.4 
- [pip](https://pypi.org/project/pip) &ndash; `pip3 install --upgrade pip`
- [Python markdown](https://pypi.python.org/pypi/Markdown): `pip3 install Markdown`
- [git-lfs](https://git-lfs.github.com) &ndash; [installation](https://help.github.com/en/articles/installing-git-large-file-storage) 
- Internet browser bookmarks

A few security related issues remain to do:

- Desktop Duo for VPN access
- Replacing
my [obsolete TrueCrypt encryption system](https://www.comparitech.com/blog/information-security/truecrypt-is-discoutinued-try-these-free-alternatives) 

I am writing this blog on the new system, with significant help and inspiration provided by my baby lizard friend presented in the image above.

By chance, now that I almost finished, I also just read this helpful article
on [how to set up your new MacBook for coding](https://www.freecodecamp.org/news/how-to-set-up-a-brand-new-macbook) by Amber Wilkie
on [freeCodeCamp.org](https://www.freecodecamp.org).
I have already completed many of the steps she suggests; still, a few are new and useful for me as well.


####<a name="3"></a> KLH Engineers RevitDeveloperTools Snooping Tool

More Revit API related, John D'Alessandro, Software Engineer at [KLH Engineers, PSC](http://www.klhengrs.com) shared a powerful new Revit database snooping utility, explaining:

> We at KLH Engineers decided to roll our own Revit Snoop tool a while ago and we thought we’d show it to you so you can check it out.

<center>
<img src="img/klh_snoop_screenshot.png" alt="KLH Snoop" width="886">
</center>

> We released it as a class library on our GitHub so that others can use it in their own tools and solutions.

> We’re also accepting issues and PRs, so the more people use the tool the better it’ll get. Let me know if you have any feedback on it or questions.

> The repo is at [KLH Engineers RevitDeveloperTools](https://github.com/klhengineers/RevitDeveloperTools).

> It has some similarities and overlap with  [RevitLookup](https://github.com/jeremytammik/RevitLookup), and adds a number of features not available there.

> The README includes a feature list comparing the two snooping tools:

<center>
<img src="img/klh_snoop_feature_list.png" alt="KLH Snoop feature list" width="353"> <!-- 1060 -->
</center>

Many thanks to John and KLH for implementing and generously sharing this powerful new snooping tool!


####<a name="4"></a> Pulling Text from Annotation Tags

One quick note on a small issue that was not discussed in
the public [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) seems
worthwhile sharing here:

**Question:** I'm trying to pull the text from the annotation tags on a drawing using the Revit API.
Is that possible?
How? 

**Answer:** Yes, this is definitely possible.

You can use a filtered element collector to collect all the annotation tags, and then access their text via their properties.

To retrieve the tags, you need to determine what class they have. I assume they
are [IndependentTag class instances](https://www.revitapidocs.com/2020/e52073e2-9d98-6fb5-eb43-288cf9ed2e28.htm).

The property to access their text is presumably
the [TagText property](https://www.revitapidocs.com/2020/8e297dee-920d-f620-6198-0bed494e3f04.htm).

You can install and use [RevitLookup](https://github.com/jeremytammik/RevitLookup) yourself
to validate these assumptions of mine in your own specific model.

If these assumptions are correct, the final solution might look something like this in  C#:

<pre class="code">
<span style="color:blue;">#region</span>&nbsp;Pull&nbsp;Text&nbsp;from&nbsp;Annotation&nbsp;Tags
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;the&nbsp;text&nbsp;from&nbsp;all&nbsp;annotation&nbsp;tags&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;in&nbsp;a&nbsp;list&nbsp;of&nbsp;strings</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;&nbsp;PullTextFromAnnotationTags(
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;tags
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">IndependentTag</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:blue;">string</span>&gt;(&nbsp;tags
&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">IndependentTag</span>&gt;()
&nbsp;&nbsp;&nbsp;&nbsp;.Select&lt;<span style="color:#2b91af;">IndependentTag</span>,&nbsp;<span style="color:blue;">string</span>&gt;(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t&nbsp;=&gt;&nbsp;t.TagText&nbsp;)&nbsp;);
}
<span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">// Pull&nbsp;Text&nbsp;from&nbsp;Annotation&nbsp;Tags</span>
</pre>

For future reference, I also added this code to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) in
the [CmdCollectorPerformance.cs module](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCollectorPerformance.cs#L1223-L1240)

Finally, you can use the built-in Revit macro IDE (integrated development environment) to convert this code from C# to Python, if you like.

####<a name="5"></a> Vertex Handling

Another recent recurring question on vertex handling may also be of general interest:

**Question:** When processing meshes or other collections of triangles, is there any way to know if one vertex is shared by multiple triangles?

Thus, the final data does not have to contain duplicate vertices and can reuse the existing vertex indices in order to save data size.

**Answer:** I have often used strategies similar to the following when collecting mesh data:

- Implement a comparison operator for the mesh vertex coordinate data.
- Store all mesh vertices in a dedicated dictionary using the mesh vertex coordinates as keys, e.g., <code>Dictionary&lt;XYZ,int&gt;</code>.
The `int` is not really important, just the keys.
I often use the `ìnt` value to count the number of vertices encountered at each location.
- For each new mesh vertex received, check whether it is already listed in the dictionary.
If so, increment its count; else, add a new entry for it.

The comparison operator needs to accommodate an appropriate amount of [fuzz](https://thebuildingcoder.typepad.com/blog/2017/12/project-identifier-and-fuzzy-comparison.html#3),
cf. this [dimensioning application](https://thebuildingcoder.typepad.com/blog/2018/12/rebars-in-host-net-framework-and-importance-of-fuzz.html#4).

For some examples, look at `GetCanonicVertices`
for [tracking element modification](https://thebuildingcoder.typepad.com/blog/2016/01/tracking-element-modification.html) and
this [post on the `XYZ` class](https://thebuildingcoder.typepad.com/blog/2017/08/birthday-post-on-the-xyz-class.html#3).

For yet more examples and use cases, you can search The Building Coder for

- `XyzComparable`
- `XyzEqualityComparer`
- `XyzProximityComparer`
 

####<a name="6"></a> The True Meaning of Pizza

I am writing this visiting my friend Dani in Verbania, Italy.

Hence, this little note in closing:

Did you ever wonder why pizza is called pizza?

Well, pretty obviously, it simply represents the formula used by a hungry mathematician to determine its volume from its radius `z` and thickness `a`:

<pre>
  Volume = &pi; &middot; z <sup>2</sup> &middot; a = pi &middot; z &middot; z &middot; a
</pre>

<center>
<img src="img/pizza.jpg" alt="Pizza volume formula" width="360">
<p style="font-size: 80%; font-style:italic">Pizza volume formula V = pi&middot;z&middot;z&middot;a</p>
</center>
