<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="run_prettify.js" type="text/javascript"></script> --> 
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- [Purge unused materials](https://boostyourbim.wordpress.com/2016/10/21/purge-unused-materials-for-another-rtceur-api-wish)

- [Q] I am interested in knowing the Revit family instances that are contained in a room. Apparently, Revit Rooms are currently not handled by the Revit to Forge translation. For a near term workaround, do you know of a Revit plug-in or script that generates a spreadsheet to report for each room in a model, the family instances contained by the room?  I heard the Revit API's support this, but have not seen or verified it.  The spreadsheet could be as simple as a three column table that lists Room ID, Room Name, and Family Instance ID, where the ID is the Revit Element ID.
[A] Sure; look at my [RoomEditoreApp](https://github.com/jeremytammik/RoomEditorApp), in the module [CmdUploadRooms]()  implementing the external command to upload the room data to my cloud ddatbase. The [lines ]() implement code to retrieve the furniture and equipment contained in given room.
 
https://github.com/jeremytammik/RoomEditorApp/blob/master/RoomEditorApp/CmdUploadRooms.cs#L157-L217

- https://lookupbuilds.com/
  https://forums.autodesk.com/t5/revit-api-forum/ci-for-revit-lookup/m-p/6976335

Automated RevitLookup builds using @jenkinsci http://bit.ly/2nNvKch #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge
@BoostYourBIM purges types, families and materials using #RevitAPI bit.ly/2nNvKch @AutodeskRevit #bim #dynamobim @AutodeskForge

Blogging despite having already exceeded my work quota for the week, but there is so much to share 
&ndash; ForgeFader user interface
&ndash; RevitLookup builds
&ndash; Purging types, families and materials
&ndash; Retrieving all family instances in a room...

-->

### ForgeFader UI, Lookup Builds, Purge &amp; Room Instances

Blogging despite having already exceeded my work quota for the week, but there is so much to share...

- [ForgeFader user interface](#2)
- [RevitLookup builds](#3)
- [Purging types, families and materials](#4)
- [Retrieving all family instances in a room](#5)

Enjoy!


#### <a name="2"></a>ForgeFader User Interface

Yesterday, I mentioned the
new [ForgeFader](https://github.com/jeremytammik/forgefader) 
and [RvtFader](https://github.com/jeremytammik/Rvtfader) samples
to calculate and display a 'heat map', i.e. colour gradient, representing the signal loss caused by the distance and number of walls between the signal source and target points spread throughout a building using JavaScript
and [three.js](https://threejs.org) and the Revit API, respectively.

[Cyrille Fauvel](https://twitter.com/FAUVELCyrille) implemented
the shaders displaying the calculated colour gradient texture in the Forge model, 
and [Philippe Leefsma‏](https://twitter.com/F3lipek) added a neat user interface to control the attenuation values, grid density, and turn on and off the display of the raycasting rays:

<center>
<blockquote class="twitter-tweet" data-lang="en"><p lang="en" dir="ltr">&quot;Forge Fader&quot; WiFi signal attenuation simulation by <a href="https://twitter.com/jeremytammik">@jeremytammik</a>, Shader by <a href="https://twitter.com/FAUVELCyrille">@FAUVELCyrille</a>, <a href="https://twitter.com/hashtag/ReactJS?src=hash">#ReactJS</a> UI by <a href="https://twitter.com/F3lipek">@F3lipek</a> <a href="https://t.co/SVm8MrqG3Y">https://t.co/SVm8MrqG3Y</a> <a href="https://t.co/jMghiK7NBY">pic.twitter.com/jMghiK7NBY</a></p>&mdash; Philippe Leefsma (@F3lipek) <a href="https://twitter.com/F3lipek/status/849813897469153281">April 6, 2017</a></blockquote>
<script async src="http://platform.twitter.com/widgets.js" charset="utf-8"></script>
</center>

You can try it out for yourself in 
the [forge-rcdb.autodesk.io Fader sample](http://autode.sk/2o06u3n).

<!--

https://forge-rcdb.autodesk.io/configurator?id=58e4aa3b00ac371158743857?utm_campaign=--%20Select%20a%20value%20--&utm_medium=--%20Select%20a%20value%20--&utm_source=JT&utm_content=--%20Select%20a%20value%20--&utm_term=

https://forge-rcdb.autodesk.io/configurator?id=58e4aa3b00ac371158743857?utm_campaign=Technical%20Education&utm_medium=Blog&utm_source=JT&utm_content=Forge_Viewer&utm_term=

-->

<center>
<img src="img/forgefader_ui.png" alt="ForgeFader user interface" width="446"/>
</center>


#### <a name="3"></a>RevitLookup Builds

Do you use [RevitLookup](https://github.com/jeremytammik/RevitLookup)?

If not, you might be best off to stop whatever you are doing right now and install and test it first.

You will not regret.

It is simply an interactive Revit BIM database exploration tool to view and navigate element properties and relationships.

Peter Hirn of [Build Informed GmbH](https://www.buildinformed.com) very kindly set up a
public [CI](https://en.wikipedia.org/wiki/Continuous_integration) for RevitLookup
at [lookupbuilds.com](https://lookupbuilds.com)
using [Jenkins](https://jenkins.io/index.html) in
a multi-branch project configuration to build all branches and tags from the GitHub repository.
The output is dual-signed with the Build Informed certificate, zipped and published to an Amazon S3 bucket.
For more information, please refer to 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [CI for RevitLookup](https://forums.autodesk.com/t5/revit-api-forum/ci-for-revit-lookup/m-p/6947111).

<center>
<img src="img/revitlookup_builds.png" alt="RevitLookup builds" width="405"/>
</center>


#### <a name="4"></a>Purging Types, Families and Materials

The Revit API provides no direct access to purge, and this has been high on the add-in developer wish list for a long time, cf. the discussions
on [purging unused text note types](http://thebuildingcoder.typepad.com/blog/2010/11/purge-unused-text-note-types.html) 
and [determining purgeable elements](http://thebuildingcoder.typepad.com/blog/2013/03/determining-purgeable-elements.html) in general.

Now, Harry Mattison of [Boost your BIM](https://boostyourbim.wordpress.com) provided clever solutions
to [purge types and families](https://boostyourbim.wordpress.com/2016/10/20/rtceur-api-wish-1-purging-types-and-families)
and [purge unused materials](https://boostyourbim.wordpress.com/2016/10/21/purge-unused-materials-for-another-rtceur-api-wish).

The former needs to handle and roll back if an error occurs due to attempting to delete the last type in a system family.

The latter uses the `DocumentChanged` event, rolling back transaction groups, and other good things do find materials that can be deleted without elements being modified.
It takes a bit of time to run because it is deleting all materials and undoing the deletions that modify other elements.

Many thanks to Harry for implementing and sharing these long-standing wishes!


#### <a name="5"></a>Retrieving All Family Instances in a Room

**Question:** I am interested in knowing the Revit family instances that are contained in a room. Apparently, Revit Rooms are currently not handled by the Revit to Forge translation. For a near term workaround, do you know of a Revit plug-in or script that generates a spreadsheet to report for each room in a model, the family instances contained by the room?  I heard the Revit API's support this, but have not seen or verified it.  The spreadsheet could be as simple as a three-column table that lists Room ID, Room Name, and Family Instance ID, where the ID is the Revit Element ID.

**Answer:** Sure; look at the [RoomEditoreApp sample](https://github.com/jeremytammik/RoomEditorApp), in the
module [CmdUploadRooms](https://github.com/jeremytammik/RoomEditorApp/blob/master/RoomEditorApp/CmdUploadRooms.cs) implementing
the external command to upload the room data to my cloud database.

The [lines L157-L217](https://github.com/jeremytammik/RoomEditorApp/blob/master/RoomEditorApp/CmdUploadRooms.cs#L157-L217) implement
code to retrieve the furniture and equipment contained in given room.
 
