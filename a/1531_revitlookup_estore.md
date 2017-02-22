<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

RevitLookup Extensible Storage Restored #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

I already discussed the first clean-up of the new <code>Reflection</code> based version of RevitLookup; here comes the second one, restoring access to view extensible storage data
&ndash; Recent changes to RevitLookup
&ndash; Restore access to extensible storage data
&ndash; Download and access to old functionality...

-->

### RevitLookup Extensible Storage Restored

I already discussed the first clean-up of the new `Reflection` based version 
of [RevitLookup](https://github.com/jeremytammik/RevitLookup);
here comes the second one, restoring access to view extensible storage data:

- [Recent changes to RevitLookup](#2)
- [Restore access to extensible storage data](#3)
- [Download and access to old functionality](#4)


#### <a name="2"></a>Recent Changes to RevitLookup

Here is an overview of the recent changes so far:

- [Using `Reflection` for cross-version compatibility](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-using-reflection-for-cross-version-compatibility.html)
- [Basic clean-up](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-with-reflection-cleanup.html)

On February 6, I presented a drastic change 
to [RevitLookup](https://github.com/jeremytammik/RevitLookup) contributed by 
Andy [@awmcc90](https://github.com/awmcc90) McCloskey, [RevDev Studios](https://twitter.com/revdevstudios),
using `Reflection` to provide more complete coverage of all the Revit database element methods and properties.

[Victor Chekalin](http://www.facebook.com/profile.php?id=100003616852588), aka Виктор Чекалин, took
a critical look at the new version and cleaned it up significantly to address some raw edges in 
his [pull request #25 &ndash; old bug fixes and improvements of the new approach ](https://github.com/jeremytammik/RevitLookup/pull/25),
presented February 17.



#### <a name="3"></a>Restore Access to Extensible Storage Data

Alexander Ignatovich, [@CADBIMDeveloper](https://github.com/CADBIMDeveloper), aka Александр Игнатович, examined the new version and says:

> I worked a lot with extensible storage in the past.
This theme remains important for me nowadays as well.
With the new `Reflection` approach, the extensible storage data display disappeared from RevitLookup.

> Therefore, I submitted a [pull request #26](https://github.com/jeremytammik/RevitLookup/pull/26) to
the RevitLookup GitHub repo.

> I restored the ability to see extensible storage content and made some refactorings.

> I also added the ability to see GUID values.

Many thanks to Alexander for restoring this critical piece of functionality!



#### <a name="4"></a>Download and Access to Old Functionality

The most up-to-date version is always provided in the master branch of 
the [RevitLookup GitHub repository](https://github.com/jeremytammik/RevitLookup).

Alexander's enhancement is provided
in [RevitLookup release 2017.0.0.17](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.17) and
later versions.

If you would like to access any part of the functionality that was removed when switching to the `Reflection` based approach, please grab it
from [release 2017.0.0.13](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.13) or earlier.

I am also perfectly happy to restore other code that was removed and that you would like preserved.
Simply create a pull request for that, explain your need and motivation, and I will gladly merge it back again.

