<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- Ehsan Iran-Nejad

- Gui Talarico

twitter:

pyRevit home page consolidation and ApiDocs code sample collection for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/apidocscode

Two exciting Revit API related news announcements from Ehsan Iran-Nejad and Gui Talarico
&ndash; New comprehensive pyRevit home page
&ndash; ApiDocs.co code search sample collection...

linkedin:

pyRevit home page consolidation and ApiDocs code sample collection for the #RevitAPI

http://bit.ly/apidocscode

Two exciting Revit API related news announcements from Ehsan Iran-Nejad and Gui Talarico:

- New comprehensive pyRevit home page
- ApiDocs.co code search sample collection...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### pyRevit Home and ApiDocs Code Sample Collection

Two exciting Revit API related news announcements from Ehsan Iran-Nejad and Gui Talarico:

- [New comprehensive pyRevit home page](#2)
- [ApiDocs.co code search sample collection](#3)
    - [How does it work?](#3.1)
    - [Limitations](#3.2)
    - [How to contribute](#3.3)
    - [RevitApiDocs and ApiDocs comparison](#3.4)


####<a name="2"></a> New Comprehensive pyRevit Home Page

Ehsan [@eirannejad](https://twitter.com/eirannejad) Iran-Nejad
[unified the pyRevit online experience](https://twitter.com/eirannejad/status/1170576981538172928?ref_src=twsrc%5Etfw),
creating a new [pyRevit home page](http://wiki.pyrevitlabs.io) that
takes you through all project related aspects:

<!--
<center>
<blockquote class="twitter-tweet">
<p lang="en" dir="ltr">Finally unified the <a href="https://twitter.com/pyrevit?ref_src=twsrc%5Etfw">@pyrevit</a> online experience.
The new pyRevit home takes you through everything related to the pyRevit project
<a href="https://t.co/lsnJrwFUbv">https://t.co/lsnJrwFUbv</a></p>&mdash; Ehsan Iran-Nejad (@eirannejad)
<a href="https://twitter.com/eirannejad/status/1170576981538172928?ref_src=twsrc%5Etfw">September 8, 2019</a>
</blockquote>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</center>
-->

<center>
<img src="img/pyrevit_home_page_2.png" alt="pyRevit home page" width="499">
</center>



####<a name="3"></a> ApiDocs.co Code Search Sample Collection

Gui [@gtalarico](https://twitter.com/gtalarico) Talarico, the author of both 
the [online Revit API documentation revitapidocs.com](https://www.revitapidocs.com) and the more
general [Apidocs.co](https://apidocs.co) covering Grasshopper, Navisworks and Rhino as well,
[announced an expanded search functionality](https://twitter.com/gtalarico/status/1170473246275145729?ref_src=twsrc%5Etfw) in
the latter that provides code samples directly within its pages by searching a whole collection of samples hosted in the
new [ApiDocs.co code search sample repository](https://github.com/gtalarico/apidocs.samples).

<center>
<img src="img/apidocs.co.png" alt="ApiDocs.co" width="700">
</center>

<!--
<center>
<blockquote class="twitter-tweet">
<p lang="en" dir="ltr">Code samples are fetched this repo<a href="https://t.co/MxgdIuPlX3">https://t.co/MxgdIuPlX3</a><br>includes code from
<a href="https://twitter.com/jeremytammik?ref_src=twsrc%5Etfw">@jeremytammik</a>
<a href="https://twitter.com/a_dieckmann?ref_src=twsrc%5Etfw">@a_dieckmann</a>
<a href="https://twitter.com/arch_laboratory?ref_src=twsrc%5Etfw">@arch_laboratory</a>
<a href="https://twitter.com/5devene?ref_src=twsrc%5Etfw">@5devene</a>
<a href="https://twitter.com/teocomi?ref_src=twsrc%5Etfw">@teocomi</a> and others</p>
&mdash; Gui Talarico (@gtalarico)
<a href="https://twitter.com/gtalarico/status/1170473246275145729?ref_src=twsrc%5Etfw">September 7, 2019</a>
</blockquote>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</center>
-->

#####<a name="3.1"></a> How Does it Work?

[Apidocs.co](https://apidocs.co) uses the GitHub Code Search API against this repo to provide Code Samples directly within pages.

Because the GitHub Code Search API is limited to a single user or repo, this repository aggregates multiple relevant repos so they can all be searchable in a single request.

#####<a name="3.2"></a> Limitations

- It's plain text search &ndash; some generic names like `Application` can trigger many false positives
- It's limited to certain entity types (e.g., `Class`, `Method`, `Property`, etc.)

#####<a name="3.3"></a> How to Contribute

- Fork this repo
- Add a relevant repo to `repos.json`
- Run `python update.py`
- Send a [Pull Request](https://github.com/gtalarico/apidocs.samples/pulls)

#####<a name="3.4"></a> RevitApiDocs and ApiDocs Comparison

**Question:** Could the new code sample search functionality be added to
both [apidocs.co](http://apidocs.co)
and [revitapidocs](https://www.revitapidocs.com)?
It is tricky to know when to use which...

**Answer:** Users can use whichever they prefer.
While [revitapidocs](https://www.revitapidocs.com) will likely continue to get new API versions, it will not get new features &ndash; the code base has not aged well and adding new features to it is no fun.
