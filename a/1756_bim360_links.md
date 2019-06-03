<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash;
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

-->

### Accessing BIM360 Cloud Links


####<a name="2"></a> Accessing BIM360 Cloud Links

My colleague Eason Kang researched how to access BIM360 file links in a Revit project and summarised the results for us:

**Question:** In Revit 2017, I used a method based on `ExternalFileReferences` to report linked files hosted in BIM360.

In Revit 2019 this method no longer works.

2019-06-03 07:00 07:10 adn_aec `TransmissionData` `GetAllExternalFileReferenceIds` , used in the attached sample from Jeremy’s blog, List Linked Files and TransmissionData, does not report links hosted in BIM360, and it reports local files that are linked only. This is an old sample.
2019-06-03 07:00 07:10 adn_aec The similar `ExternalFileUtils` `GetAllExternalFileReferences` method similarly does not return these items.

Has there been a change in this area?

Is there a different method we should be using?

**Answer:** Neither of these two methods support cloud links in Revit 2018 or Revit 2019.

In Revit 2017, they appeared to work, because back then, the cloud links were downloaded as local copies by the Autodesk Desktop Connector before linking.

Nowadays, BIM360 cloud links are treated as external sources. This behaviour started from with Revit 2018.

Therefore, please use `ExternalResourceUtils` `GetAllExternalResourceReferences` instead.

Here is a working code snippet:

<pre class="code">
// Obtain all external resource references (Saying BIM360 Cloud references and local file references this time)
var xrefs = ExternalResourceUtils.GetAllExternalResourceReferences(doc);
var msg = string.Empty;

try
{
    foreach (var eid in xrefs)
    {
        var elem = doc.GetElement(eid);
        if (elem == null) continue;

        var link = elem as RevitLinkType;  //!<<< Get RVT document links only this time
        if (link == null) continue;

        var map = link.GetExternalResourceReferences();
        var keys = map.Keys;

        foreach (var key in keys)
        {
            var reference = map[key];
            var dictinfo = reference.GetReferenceInformation(); //!<<< Contains Forge BIM360 ProjectId(i.e. LinkedModelModelId) & ModelId (i.e. LinkedModelModelId) if it's from BIM360 Docs. It can be used in ModelPathUtils#ConvertCloudGUIDsToCloudPath calls
            var displayName = reference.GetResourceShortDisplayName();  //!<<< Link Name shown on the Manage Links dialog
            var path = reference.InSessionPath;
        }

        try
        {
           //Load model temporarily to get the model path of the cloud link
            var result = link.Load();
            var mdPath = result.GetModelName();   //!<<< Link ModelPath for Revit internal use
            link.Unload(null);

            var path = ModelPathUtils.ConvertModelPathToUserVisiblePath(mdPath); //!<<< Convert model path to user visible path, i.e. saved Path shown on the Manage Links dialog
            var refType = link.AttachmentType;  //!<<< Reference Type shown on the Manage Links dialog
            msg += string.Format("{0} {1}\n", link.AttachmentType, path);
        }
        catch (Exception ex)
        {
            TaskDialog.Show("Revit", ex.Message);
        }
    }

    TaskDialog.Show("Revit", msg);
}
catch(Exception ex)
{
    TaskDialog.Show("Revit", ex.Message);
}
</pre>

And the result snapshots:

<center>
<img src="img/bim360_links_1.png" alt="BIM360 link" width="352">
</center>

<center>
<img src="img/bim360_links_2.png" alt="BIM360 link" width="876">
</center>

Many thanks to Eason for this research and clear explanation!



####<a name="3"></a> Retrieve RVT Preview Thumbnail Image with Python

https://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html#comment-4484205626

Franco Tonutti

Hi Jeremy, how can I get the preview image of a .rfa file? I'm trying the following without success

  import os.path as op
  import olefile

  def get_rvt_preview(rvt_file):
  if op.exists(rvt_file):
  if olefile.isOleFile(rvt_file):
  rvt_ole = olefile.OleFileIO(rvt_file)
  bfi = rvt_ole.openstream("RevitPreview4.0")
  readed = bfi.read()
  f = open('my_file.bmp', 'w+b')
  f.write(readed)
  f.close()
  else:
  print("file does not apper to be an ole file: {}".format(rvt_file))
  else:
  print("file not found: {}".format(rvt_file))

Thumbnail

jeremy tammik --> Franco Tonutti

Dear Franco,

Sorry, no idea. I have not looked into this for a while. Take a look at the other explorations reading RVT files without using the Revit API:

http://thebuildingcoder.typepad.com/blog/2010/06/open-revit-ole-storage.html
http://thebuildingcoder.typepad.com/blog/2013/01/basic-file-info-and-rvt-file-version.html
https://thebuildingcoder.typepad.com/blog/2015/09/lunar-eclipse-and-custom-file-properties.html#3
http://thebuildingcoder.typepad.com/blog/2016/02/reading-an-rvt-file-without-revit.html
http://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html

If that does not help, I would suggest asking the larger community in the Revit API discussion fourm:

https://forums.autodesk.com/t5/revit-api-forum/bd-p/160

Cheers, Jeremy.

https://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html#comment-4486812442

Franco Tonutti  jeremy tammik

Thank you very much, I could solve the problem by looking for the signature png

def get_rvt_preview(rvt_file, png_path):
if op.exists(rvt_file):
if olefile.isOleFile(rvt_file):
# Open ole file
rvt_ole = olefile.OleFileIO(rvt_file)
bfi = rvt_ole.openstream("RevitPreview4.0")
readed = bfi.read()

# Find png signature
readed_hex = readed.hex()
pos = readed_hex.find('89504e470d0a1a0a')
png_hex = readed_hex[pos:]
data = bytes.fromhex(png_hex)

# Save png file
f = open(png_path, 'w+b')
f.write(data)
f.close()
else:
print("file does not apper to be an ole file: {}".format(rvt_file))
else:
print("file not found: {}".format(rvt_file))

/a/doc/revit/tbc/git/a/img/get_rvt_preview_image_py.png

####<a name="3"></a> Failings of Political Establishment

German 55-minute video about the failings of the established politicians who are driving the planet to the brink of catastrophe, ignoring all scientifical evidence

https://youtu.be/4Y1lZQsyuSQ

<iframe width="560" height="315" src="https://www.youtube.com/embed/4Y1lZQsyuSQ" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>



####<a name="3"></a> Slow Macbook with kernel_task Several 100% CPU

After three years or so, my Macbook Pro slowed down to an unusable crawl, especially when it got warm.

I looked at the results in the activity monitor, worrying about a virus, and only saw that the `kernel_task` was often using seveeral hundred percent of the CPU.

After weeks of this, I decided to upgrade OSX to the newest version to see whether that would help.

The upgrade went fine, and at least the situation was no worse.

Soon, however, it got wors still.

Finally, I found the solution:

This description
of [what `kernel_task` is, and why it is running on my Mac](https://www.howtogeek.com/310293/WHAT-IS-KERNEL_TASK-AND-WHY-IS-IT-RUNNING-ON-MY-MAC),
explains how `kernel_task` pretends to use CPU cycles to keep things cool.

That is confirmed by the Apple support thread
on [`kernel_task` using a large percentage of your Mac CPU](https://support.apple.com/en-us/HT207359).

In the next step, I looked at a helpful five-minute video
on [how to clean MacBook Pro fans](https://youtu.be/ABs0L2VpLuA).

Opening the Macbook is simple, but it does require the smallest screwdriver I have ever seen,
a [1.2 mm Pentalobe](https://en.wikipedia.org/wiki/List_of_screw_drives#Pentalobe):

<center>
<img src="img/screwdriver.jpg" alt="Miniscule screwdriver" width="568">
</center>

I never opened the PC before myself.

Using that, however, it was easy to open and blow out the dust clogging the fans with a pressurised air spray can.

One fan was so clogged with dust it was probably not running at all.

It seems better now. Please keep your fingers crossed for me that it stays that way and continues working &nbsp; :-)

####<a name="3"></a> Dynamo Primer and Accessing the Revit API Compendium

I already mentions
Paolo Serra's [Dynamo Primer](https://primer.dynamobim.org)
discussing [Revit API versus Dynamo for Revit](https://thebuildingcoder.typepad.com/blog/2018/12/dynamo-symbol-vs-type-and-exporter-exception.html#2).

Here is another very valuable and extensive related resource of his, covering numerous aspects of Dynamo, its features, Python, the Revit API and the relationships between them.

> In terms of Dynamo for Revit you can refer to my [Dynamo Primer](https://primer.dynamobim.org),
specifically chapter 8 for the integration with Revit.

> You can also check out the slide deck compendium I put together over the years that covers the basics of the Revit API and how to access it through Dynamo and Python: [a360.co/2LfuM5p](https://a360.co/2LfuM5p).

> It includes the following sections:

> - Dynamo Overview
- User Interface
- Graphs Management
- Autodesk Standards
- Visual Programming Principles
- Filtering, Grouping & Sorting
- Dynamo-Excel Link
- Design Script
- Geometry Library
- Automation Applications
- Dynamo for Revit
- Dynamo and Python
- Object Oriented Programming
- Revit API Introduction
- Next Steps

