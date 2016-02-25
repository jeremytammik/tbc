<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 11470031 [GroupType GetPreviewImage always returns Null]

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewAPI #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restAPI
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge
@AutodeskReCap @Adsk3dsMax
#revitAPI #bim #aec #3dwebcoder #adsk #adskdevnetwrk @jimquanci @keanw
#au2015 #rtceur
#eraofconnection

Revit API, Jeremy Tammik, akn_include

EstoreFile, Group Preview and RoomScheduleCloud #revitAPI #3dwebcoder @AutodeskRevit @adskForge #3dwebaccel #a360 #bim

Lots of exciting topics for today
&ndash; EstoreFile, extensible storage and embedding a raster image
&ndash; Preview Image for a Group
&ndash; Thoughts on migrating the Room Schedule sample to 64-bit: Request, Implementation suggestion, Problem, Response
&ndash; Autodesk 2016 AEC showreel call for submissions...

-->

### EstoreFile, Group Preview and RoomScheduleCloud

Lots of exciting topics for today:

- [EstoreFile, extensible storage and embedding a raster image](#2)
- [Preview Image for a Group](#3)
- [Thoughts on migrating the Room Schedule sample to 64-bit](#4)
    - [Request](#4.1)
    - [Implementation suggestion](#4.2)
    - [Problem](#4.3)
    - [Response](#4.4)
- [Autodesk 2016 AEC showreel &ndash; call for submissions](#5)

#### <a name="2"></a>EstoreFile for Revit 2015 and Revit 2016, Extensible Storage and Embedding a Raster Image

I migrated my extensible storage sample
application [EstoreFile](https://github.com/jeremytammik/EstoreFile) from
Revit 2014 to Revit 2015 and Revit 2016.

EstoreFile is a C# .NET Revit add-in that stores arbitrary binary files into extensible storage on any selected elements in the Revit project.

To read in more depth about it, please refer to The Building Coder topic group
on [extensible storage](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23).

Here are the changes I made to migrate between the different versions:

- [Revit 2014 &rarr; Revit 2015](https://github.com/jeremytammik/EstoreFile/compare/2014.0.0.1...2015.0.0.0)
- [Revit 2015 &rarr; Revit 2016](https://github.com/jeremytammik/EstoreFile/compare/2015.0.0.1...2016.0.0.0)

The EstoreFile migration was prompted by the Revit API discussion forum thread
on [inserting a raster image](http://forums.autodesk.com/t5/revit-api/insert-raster-image/m-p/6021312) into
the Revit project, or embedding it, to be more precise.


#### <a name="3"></a>Preview Image for a Group

**Questions:** We want to make a preview of a detail group type of class `GroupType` that does not have a instance placed of class `Group` and not for individual elements of the group. Is that possible?

Specifically:

1. It is possible to get a preview image for a detail group represented by a `GroupType` object that is present and loaded in a document?
2. It is possible to create a PreviewControl object for previewing such a detail group?

We tried to retrieve a preview image of a GroupType by using the GetPreviewImage method, but it always returns null.

How does the GroupType:GetPreviewImage method work?

Is it possible to show a preview of a GroupType object?

**Answer:** The GetPreviewImage method will not work for what you are trying to do. Sorry about that.

Here is an explanation:

Groups don’t have preview images in the Revit user interface either.

If you go to place a family instance, the preview image is displayed like this:

<center>
<img src="img/preview_image_family_type.jpeg" alt="Preview image of a family type" width="450">
</center>

For a group, the preview image square in the type selector remains blank:

<center>
<img src="img/preview_image_group.jpeg" alt="Preview image of a group" width="450">
</center>

This is why GetPreviewImage has nothing it can return.

There are various workarounds to achieve what you want anyway:

You could create a temporary view, place an instance, and create a snapshot of that, similar to the approach described
to [export an element to a SAT file](http://thebuildingcoder.typepad.com/blog/2013/09/saving-a-solid-to-a-sat-file-implementation.html).

Here is a vaguely related post
on [setting the background colour](http://thebuildingcoder.typepad.com/blog/2013/12/setting-the-view-display-background.html) for
such a snapshot.

Regarding your other question on the PreviewControl:

The preview control displays an existing Revit view, so you could set up a dedicated 3D view to display the group you are interested in to make use of that in a similar way as for the snapshot.


#### <a name="4"></a>Thoughts on Migrating the RoomSchedule Sample to 64-Bit

Here is a really old request that I have been dragging along for ages and will finally respond to now.

It concerns the trusty old RoomSchedule Revit SDK sample that demonstrates Excel data import, export, room creation and modification, i.e., how to retrieve spread sheet data, how to create rooms without placing them and how to update spreadsheet data with BIM data of rooms.

Summary: I cannot currently address the request as it stands, but rather suggest migrating the RoomSchedule sample to a much more effective cloud-based database, like [MongoDB](https://www.mongodb.org), like I did by implementing
the [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) sample
to replace the Revit SDK FireRating one.

In detail:

#### <a name="4.1"></a>Request

I think many developers would greatly appreciate it if you could incorporate ExcelReader or some other code into the Revit SDK sample called Room Schedule so it becomes compatible with 64 bit systems.  'XlsDBConnector.cs' and 'using System.Data.OleDb' are not 64 bit compatible.  I really don't want to bother you, but after almost killing myself trying to make it work I figured I would.

The ExcelReader tests I've tried work perfectly on my 64 bit computer and it's extremely fast.  See VS Express solution I came up with in the attached zip file.  I just don't have the C# and Revit API expertise necessary to successfully incorporate the code.

#### <a name="4.2"></a>Implementation Suggestion

EXCELDATAREADER:  You are correct, it's ExcelDataReader via NuGet.  In Visual Studio Express I just followed the NuGet instructions to load ExcelDataReader.  Then sort of stumbled my way around to create the simple solution I sent you using sample code that I found on the ExcelDataReader website and GitHub.  ExcelDataReader seems to run in the background so I can only assume it somehow incorporates itself into a Revit add-in during the build.  The 64 bit compatibility and speed seems really great.  Almost too good to be true.

BACKGROUND:  I use Revit 2014 to prepare Process and Instrumentation Diagrams for a company near Los Angeles.  They often require tables of generic information on sheets to accompany them.  I’ve attached a macro-enabled Excel table as an example of what I’m talking about.  I emphasis generic because most Revit category schedules and function specific schedules (such as key schedules) do not have enough copying, filtering, and formatting flexibility to accommodate the use of these tables.

ADD-IN:  An add-in that would help me and I think many others would give the ability to import basic Excel table information/data, like that mentioned above, into the Revit room schedule.  Essentially what I believe the SDK sample is supposed to do.  For reliability purposes I think it would be best if it didn’t have a lot of bells and whistles though.  However, I'm guessing it would have to have code that recognizes shared parameters generated by the user for the columns/fields that would need to be added to the room schedule to accommodate the non-room information/data.  Those columns would carry the table information/data in unplaced rooms rows.

WORKFLOW:  A typical workflow would be to import the Excel table info/data into Revit's room schedule, then make and rename a copy of the room schedule.  Then hide the columns you don’t want to see and filter only the table information/data needed.  Now, after you drag that schedule onto a sheet, you can take advantage of the wonderful auto wrapping feature of Revit room schedules as well as all the positioning, adjusting, and formatting options Revit room schedules allow after they're on a sheet.

PRACTICAL APPLICATION:  To be a bit more specific, here’s a general rundown of how I think a practical application would work.  Note that because I’m not a programmer I may be missing something.

Revit Setup:  Add unique shared parameters in Revit and then add corresponding columns/fields in Revit room schedule.  Then, also in the Revit room schedule, insert empty rows (unplaced rooms).  Quantity to be equal or greater than the number of Excel table rows.  However, it would be nice if add-in could accomplish that task with a button or something.  I think the SDK 'Room Schedule' sample has some kind of a function for this, but I couldn’t verify because of the 64 bit issue.

Excel Setup:  Make columns with headers that match shared parameter column names in Revit room schedule.

Basic Steps to Get Excel Info/Data Onto a Revit Sheet:

1. In Excel, enter table information/data into Excel table, save, and close.
2. Open Revit project and Revit room schedule.
3. Use Room Schedule add-in as required to import Excel info/data into Revit room schedule and close.
4. In Revit Explorer, make copies of room schedule as needed for placing on one or more sheets and re-name each copy as appropriate.
5. Open each copy, set filters and sort options so only desired table information/data shows, hide columns as necessary, and close.
6. Go to sheets and drag tables onto them as necessary.  Then position table, adjust column widths, and format as desired.

IDEAS & THOUGHTS:

- Eliminate combo box/floor level filter currently in SDK Room Schedule sample add-in.  Don't see advantage to this and might reduce code and possible issues and exceptions.
- I don’t know if ExcelDataReader works on 32 bit systems, but if it does it sure seems like it would be a better way to go all around than 'using System.Data.OleDb', which is not 64 bit compatible.
- If ExcelDataReader is 32 bit compatible, delete 'using System.Data.OleDb' and XlsDBConnector.cs all together and rely only on ExcelDataReader code (or better if known).

NOTE:  Feel free to share my little ExcelDataReader solution, but if you do could you please use the one attached called KoolExcelReader.  I took my name out of it and gave more appropriate titles to the group boxes on the form.

APPROACH:  I’m not really sure how you best approach this on your end, but if you are able to post a working add-in solution using ExcelDataReader, I’m sure the Revit community would run with it in a variety of ways to improve it and come up with more applications for it.  Especially all those that have and are planning on switching to 64 bit systems, which if I understand correctly is the way of the future.

Hope I gave you the information you need. I’m excited about getting this working on my 64 bit Revit.  Just let me know what else I can do.

#### <a name="4.3"></a>Problem

Thank you.  I looked at it hard, but unfortunately was not able to make a Visual Studio solution implementing ExcelDataReader.  It's just to advanced for me:-(

About all I can do is tinker with code a bit.  Wish I had time to take some courses.

Yes, it seems like someone just needs to replace the 'using System.Data.OleDb' and XlsDBConnector.cs with ExcelDataReader, but I'm just guessing.

I liked your idea to "grab the RoomSchedule sample and demonstrate how to integrate this package [ExcelDataReader] into it."

Presenting it as a working solution in your blog would be amazing.  Hoping ExcelDataReader will open up 64 bit compatibility for a lot of people.

Also, I'm really interested to hear your opinion if you like it or not.

#### <a name="4.4"></a>Response

I am very sorry that I have still not gotten around to looking at this in any depth.

The way things look right now, I probably never will.

How many people are interested in this?

Would it not be better to move forward, use the much more effective and
advanced [NoSQL](https://en.wikipedia.org/wiki/NoSQL) open
source cloud based alternatives available today and ditch all the M$ garbage?

As said,
the [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud) sample
demonstrates exactly how this can be achieved, and also its powerful advantages:

- Much simpler &ndash; less code, less dependencies
- Much cheaper &ndash; no cost at all
- Much more powerful &ndash; unlimited scalability

I hope you agree and am looking forward to hearing more about your ideas on this going forward.


#### <a name="5"></a>Autodesk 2016 AEC Showreel &ndash; Call for Submissions

Do you have any great project in Architecture, Engineering and Construction you'd like to share with us?  We're looking for recent video content created using any or a combination of Autodesk software. This reel will be featured on Autodesk websites, Autodesk YouTube channels, as well as events throughout the year.

If so, it's as easy as:

- Review the [recommended format and guidelines](https://autodesk.box.com/s/jozub3xzxumh708n3qlk9mk3am93ssfy).
- Send your project to (mailto:AECreel@autodesk.com); you can use any online file-transferring platform you want.
- We’ll send you back a consent form (all languages are available) that you will need to fill out and email back to (mailto:AECreel@autodesk.com).

The deadline for submissions for this project is February 10th, 2016. Your work could be included in our next AEC show reel!

In case you are interested, here is [last year's one-and-a-half minute video](https://www.youtube.com/watch?v=waPVDhICcOw):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/waPVDhICcOw?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

