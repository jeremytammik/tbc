<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Multi-version Revit add-in template #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/multiversiontemplate

As I already mentioned, I am taking lots of time off in July.
This is just a note to let you know I am alive, well and happy, currently in Brassac in Occitanie in southern France,
on my way to practice awareness, care and attentiveness in 
the Buddhist monastery Plum Village near Bordeaux.
On the road, I'll just share this quick note from
a comment by Zhmayev Yaroslav on multi-targeting Revit versions using <code>TargetFrameworks</code>
&ndash; Multi-Version Revit Add-In Template...

--->

### Vacation and Multi-Version Revit Add-In Template

As I mentioned in
my [last post](http://thebuildingcoder.typepad.com/blog/2018/06/add-in-registration-vendorid-and-signature.html),
I am taking lots of time off in July.

This is just a note to let you know I am alive, well and happy, currently in Brassac in [Occitanie in southern France](https://en.wikipedia.org/wiki/Occitanie_(administrative_region)),
on my way to practice awareness, care and attentiveness in 
the [Buddhist monastery Plum Village](https://plumvillage.org) near Bordeaux, founded
by the Vietnamese monk and Zen master [Thich Nhat Hanh](https://plumvillage.org/about/thich-nhat-hanh).

<center>
<img src="img/719_brassac_800x600.jpg" alt="Brassac" width="400"/>
</center>

On the road, I'll just share this quick note from
a [comment by Zhmayev Yaroslav](http://thebuildingcoder.typepad.com/blog/2018/06/multi-targeting-revit-versions-cad-terms-texture-maps.html#comment-3992433596)
on [multi-targeting Revit versions using `TargetFrameworks`](http://thebuildingcoder.typepad.com/blog/2018/06/multi-targeting-revit-versions-cad-terms-texture-maps.html#2):

#### <a name="2"></a> Multi-Version Revit Add-In Template

I use .NET SDK and multi-targeting for my NuGet packages all the time and I have to admit that matching different .NET Framework versions to different Revit versions as described
in [multi-targeting Revit versions using `TargetFrameworks`](http://thebuildingcoder.typepad.com/blog/2018/06/multi-targeting-revit-versions-cad-terms-texture-maps.html#2) is
really smart.

All Revit add-in templates I've used so far had some small yet really annoying issues, such as:

- They were bloated with author's information in addin's manifest (author's name , vendor id etc.)
- Project dependencies were pointing to files somewhere on template author's PC
- Debugger tweaked to start Revit.exe from a non-existing location (most of the time it was set to some Revit Copernicus folder or similar, which I assume is the path where Revit beta version is installed or similar)
- You still had to deal with Revit 2017.1.x UI culture bug
- etc.

So, I tried to solve all the problems above and created my
own [VS2017 Revit add-in template](https://github.com/Equipple/vs-templates-revit-addin/releases).

It's a simple "ready to go" template / add-in bootstrap with NuGet dependencies, debugger tweaks for each Revit version, add-in manifest processing etc.

It supports Visual Studio 2017 (15.6+) and 64-bit Revit versions from 2014 all the way up to 2019.

Please try it and let me know if it works for you.

Many thanks to Zhmayev for sharing this!
