<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- blog about rtc and desktop-cloud

#dotnet #csharp #geometry
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #adsklabs #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon #revitapi #3dwebcoder
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#au2015 #rtceur
#adskdevnetwrk

Revit API, Jeremy Tammik, akn_include

WorksharingUtils #revitapi #bim #aec #3dwebcoder #au2015 #rtceur #cloud #svg

Rudolf 'Revitalizer' Honke frequently requested better documentation of the Revit API `*Utils` classes, e.g., when he pointed out the handy utility classes back in 2013. This is still an interesting and somewhat poorly documented area in the Revit API, as we can see from the discussion below
&ndash; WorksharingUtils and the WorksharingTooltipInfo class
&ndash; Ten interesting C# features
&ndash; A new acronym – TL;DR...

-->

### WorksharingUtils

Rudolf [Revitalizer](http://forums.autodesk.com/t5/user/viewprofilepage/user-id/1103138) Honke
has frequently requested better documentation of the Revit API `*Utils` classes, e.g., when he pointed out
the [handy utility classes](http://thebuildingcoder.typepad.com/blog/2013/04/handy-utility-classes.html) back in 2013.

This is still an interesting and somewhat poorly documented area in the Revit API, as we can see from the discussion below:

- [WorksharingUtils and the WorksharingTooltipInfo class](#2)
- [Ten interesting C# features](#3)
- [A new acronym &ndash; TL;DR](#4)



#### <a name="2"></a>WorksharingUtils and the WorksharingTooltipInfo Class

The development team answered the following recent Revit API forum thread
on [API access to the workshared element user history](http://forums.autodesk.com/t5/revit-api/workshared-element-user-history/m-p/5896907):

**Question:** Is there any access at all to Workshared Element User History?

If I turn on one of the worksharing display modes and hover my mouse over any element, I get a popup like this advising me of who that element was originally created by and who last updated it in the central model:

<center>
<img src="img/worksharing_tooltip.png" alt="Worksharing tooltip" width="490">
</center>

Is it possible to programmatically access these fields?

**Answer:** I was unable to find any information about this, so I passed on the question to the Revit development team.

Their answer is short, succinct and to the point: this is in the `WorksharingTooltipInfo` class acquired from the `WorksharingUtils.GetWorksharingTooltipInfo` method.

So we are saved once again by our beloved `Utils` classes.

They and the important functionality they provide are apparently still hard to find.

Many thanks to Scott and Arnošt for pointing this out, and the importance of improving their visibility.


#### <a name="3"></a>Ten Interesting C# Features

My colleague [Augusto Gonçalves](http://adndevblog.typepad.com/aec/augusto-goncalves.html) recently pointed out an article
on [10 features in C# that you really should learn (and use!)](http://www.codeaddiction.net/articles/15/10-features-in-c-that-you-really-should-learn-and-use).

While I do not totally agree with them all, similarly to some of the options voiced in
the comments section, it is still definitely an interesting and worthwhile quick read.

**Addendum:** In
his [comment](http://thebuildingcoder.typepad.com/blog/2015/11/worksharingutils.html#comment-2350844009) below,
Guy Robinson points out another interesting related link to the MSDN article
on [new features in C# 6](http://blogs.msdn.com/b/csharpfaq/archive/2014/11/20/new-features-in-c-6.aspx),
which is actually more structured and in-depth than the former.




#### <a name="4"></a>A New Acronym &ndash; TL;DR

I really dislike acronyms that I do not know the meaning of.

I quite like them when I do, though, and I am certain that my communication partner knows them as well.

I just discovered this one that I like, since it expresses a sentiment I often share: TL;DR &ndash; too long; didn't read.

Of course, this leads back to the first topic again: the `*Utils` classes are all documented in the Revit API help file RevitAPI.chm... all you have to do is read it &nbsp; :-)
