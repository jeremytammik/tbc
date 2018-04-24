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

- compare existing VB.NET dlls and the paths specified in RvtSamples.txt
  $ find . | grep VB | grep dll | grep bin | sort
  $ grep VB.*dll RvtSamples.txt | sort | uniq | sed 's.\\./.g' | sed 's,C:/a/lib/revit/2018.1/SDK/Samples,.,'

- /a/doc/revit/tbc/git/a/zip/RvtSamples_2019.zip

- todo: add new SDK samples to RvtSamples

RvtSamples loads the Revit 2019 SDK samples #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/rvtsamples2019

I set up RvtSamples for the Revit 2019 SDK samples.
Just like the migration to previous versions, this is not a trivial undertaking.
To cut a long story short and simply share my current working RvtSamples source code for the Revit 2019 SDK, here
is RvtSamples_2019.zip containing my modified files...

--->

### RvtSamples 2019

I already described how I installed Revit 2019,
[compiled the Revit 2019 SDK samples](http://thebuildingcoder.typepad.com/blog/2018/04/compiling-the-revit-2019-sdk-samples.html),
migrated [RevitLookup to the new version](http://thebuildingcoder.typepad.com/blog/2018/04/revitlookup-2019-and-new-sdk-samples.html),
[The Building Coder samples](http://thebuildingcoder.typepad.com/blog/2018/04/forge-rvt-to-ifc-adn-xtra-tbc-and-adnrme-updates.html#2),
[the AdnRme MEP HVAC and electrical samples](http://thebuildingcoder.typepad.com/blog/2018/04/forge-rvt-to-ifc-adn-xtra-tbc-and-adnrme-updates.html#3) and
the [AdnRevitApiLabsXtra training labs](http://thebuildingcoder.typepad.com/blog/2018/04/forge-rvt-to-ifc-adn-xtra-tbc-and-adnrme-updates.html#4).

In the course of the last post, I forgot to mention setting up RvtSamples.

Just like the migration to previous versions, this is not a trivial undertaking.

Here are the discussions of the previous migrations to 
the [Revit 2017 SDK](http://thebuildingcoder.typepad.com/blog/2016/04/rvtsamples-for-revit-2017.html) and
the [Revit 2018](http://thebuildingcoder.typepad.com/blog/2017/05/sdk-update-rvtsamples-and-modifying-grid-end-point.html#3).

To curt a long story short and simply share my current working RvtSamples source code for the Revit 2019 SDK, here
is [RvtSamples_2019.zip](zip/RvtSamples_2019.zip) containing my modified files.
It also includes some of the originals renamed by adding the suffix `original`.

I hope you find this useful.

<center>
<img src="img/RvtSamples_2019.png" alt="RvtSamples 2019" width="400"/>
</center>

Please let me know if you find anything is missing.

Todo: I have not even checked yet whether
the [new Revit 2019 SDK samples](http://thebuildingcoder.typepad.com/blog/2018/04/revitlookup-2019-and-new-sdk-samples.html#3) have
been included yet.

I strongly suspect not, sorry to say... stay tuned...