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

-->

### Accessing BIM360 Cloud Links


####<a name="2"></a>


My colleague Eason Kang

**Question:** In Revit 2017, I used a method based on `ExternalFileReferences` to report linked files hosted in BIM360.

In Revit 2019 this method no longer works.

- `TransmissionData` `GetAllExternalFileReferenceIds` , used in the attached sample from Jeremy’s blog, List Linked Files and TransmissionData, does not report links hosted in BIM360, and it reports local files that are linked only. This is an old sample.
- The similar `ExternalFileUtils` `GetAllExternalFileReferences` method similarly does not return these items.

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




####<a name="3"></a> RoomVolumeDirectShape Functionality





the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

