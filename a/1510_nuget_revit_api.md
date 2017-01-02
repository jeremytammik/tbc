<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- https://twitter.com/AndreyBushman
  Andrey Bushman
  @AndreyBushman
  https://github.com/Andrey-Bushman
  https://bitbucket.org/Andrey-Bushman/
  https://www.facebook.com/andrey.bushman
  https://www.nuget.org/packages/Revit-2017x64.Base/ -- evit 2017 x64 .NET API 2.0.0
  Revit 2017 x64 .NET API

NuGet Revit API Package #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge @AndreyBushman

Most of the work of the year has been done, and it is time to settle down and clear out for the new.
Tomorrow is the winter solstice, followed by Christmas and Yuletide, ending with the Twelfth Night.
For me, this is a time of retreat, calm, reflection, and renewal of energy.
Today brings a nice gift from Andrey Bushman
&ndash; NuGet Revit API package
&ndash; RevitLookup using the NuGet Revit API package
&ndash; Creating a NuGet package from assembly DLLs
&ndash; More NuGet packages...

-->

### NuGet Revit API Package

Most of the work of the year has been done, and it is time to settle down and clear out for the new.

Tomorrow is the [winter solstice](https://en.wikipedia.org/wiki/Winter_solstice),
followed by Christmas and
[Yuletide](https://en.wikipedia.org/wiki/Yule), ending with
the [Twelfth Night](https://en.wikipedia.org/wiki/Twelfth_Night_(holiday)).

For me, this is a time of retreat, calm, reflection, and renewal of energy.

<center>
<img src="img/220px-StonehengeSunrise1980s.jpg" alt="Sunrise at Stonehenge on the Winter Solstice" width="220"/>
</center>

Today brings a nice gift from Andrey Bushman:

- [NuGet Revit API package](#2)
- [RevitLookup using the NuGet Revit API package](#3)
- [Creating a NuGet package from assembly DLLs](#4)
- [More NuGet packages](#5)


####<a name="2"></a>NuGet Revit API Package

[Andrey Bushman](http://bushman-andrey.blogspot.ru) ([twitter](https://twitter.com/AndreyBushman)),
aka Андрей Бушман, made numerous contributions to the AutoCAD developer community, provided and documented in 
his [AutoCAD.NET laboratory](https://sites.google.com/site/bushmansnetlaboratory).

Now he has provided something new for Revit as well,
in [pull request #18 for RevitLookup](https://github.com/jeremytammik/RevitLookup/pull/18),
saying:

> I replaced the explicit Revit API assembly references by the [Revit 2017 x64 .NET API NuGet package](https://www.nuget.org/packages/Revit-2017x64.Base).

> I did it because some users (who aren't programmers) had problems with Revit references when compiling this project code.

> Therefore, I wrote a [four-and-a-half-minute video](https://youtu.be/N0itQZDUEeA) for them explaining the steps to replace the explicit Revit API assembly references by the NuGet package:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/N0itQZDUEeA?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

Here is a link to the [original Russian version of the video](https://youtu.be/ajgSGp6gp5E).

> I created it for those who aren't familiar with Visual Studio and the handling of the Revit API assembly references.

> If you plan to apply my correction, then this video will stop being useful.

I have in fact applied the correction, and hope that the video remains useful anyway, at least in parts &nbsp; :-)


####<a name="3"></a>RevitLookup using the NuGet Revit API Package

I followed Andrey's advice and merged
his [pull request #18](https://github.com/jeremytammik/RevitLookup/pull/18) into
the [RevitLookup GitHub repo](https://github.com/jeremytammik/RevitLookup) master branch to
create [release 2017.0.0.7](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.7),
which automatically updates its Revit API assemblies from NuGet.


####<a name="4"></a>Creating a NuGet Package from Assembly DLLs

What happens when I need to update to a new release of the Revit API, or if I wish to support an older version?

I asked Andrey:

How easy is it to create an updated NuGet package each time the assemblies change?

> It is very simple and convenient.
To create new NuGet package takes only a couple of minutes.
Updating NuGet package through Visual Studio takes ten seconds.
I can create a video if you want.
NuGet is a very useful and popular tool.
It completely fixes the problems with the lost references.
The project can be copied on any computer and is right there sent to compilation without the need for editing references.
NuGet also allows to monitor the updates for these assemblies and to quickly apply them if the programmer wants. 

Do you have a step-by-step documentation for that also, by any chance?

> You can download and read free books about NuGet,
e.g., *[Pro NuGet &ndash; your salvation from dependency hell](http://www.allitebooks.com/pro-nuget-2nd-edition/)*
and the *[NuGet 2 essentials](http://www.allitebooks.com/nuget-2-essentials/)*;
I read the former myself.


####<a name="5"></a>More NuGet Packages

Here is a link to [all of Andrey's NuGet packages for Revit](https://www.nuget.org/packages?q=owner%3A%22Bush%22+tags%3A%22revit%22).

NuGet packages for Revit 2015 and 2016 are being added soon.

Very many thanks to Andrey for creating, sharing and documenting this useful approach!

I wish everybody a wonderful Christmas time and a peaceful and happy end of the year!

