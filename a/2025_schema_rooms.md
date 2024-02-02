<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
</head>

<!---

- schema builder crash
  https://autodesk.slack.com/archives/C0SR6NAP8/p1706598868934699
  https://forums.autodesk.com/t5/revit-api-forum/extensiblestorage-schemabuilder-addsimplefield-causes-quot/m-p/12529666

- get rooms
  https://autodesk.slack.com/archives/C0SR6NAP8/p1706701084426099
  Want to collect the every room in apartment...

- Embed Gif in ToolTip
  https://forums.autodesk.com/t5/revit-api-forum/embed-gif-in-tooltip/m-p/12532476

- Draw order of detail items
  https://forums.autodesk.com/t5/revit-api-forum/draw-order-of-detail-items/m-p/12531008
  very briefly mentioned in [Handy Utility Classes](https://thebuildingcoder.typepad.com/blog/2013/04/handy-utility-classes.html) and in the what's new notes for 2013, 2014 and 2024, but never tested

twitter:

Using the extensible storage schema builder, retrieving all adjacent apartment rooms, embedding a GIF animation in a ribbon tooltip and controlling detail item draw order with the #RevitAPI @AutodeskRevit #BIM @DynamoBIM https://autode.sk/schemabuilderquirks

Using the extensible storage schema builder, retrieving adjacent rooms, embedding an animation in a tooltip and controlling detail item draw order
&ndash; Schema builder quirks
&ndash; Get all apartment rooms
&ndash; Embed GIF in tooltip
&ndash; Control draw order of detail items...

linkedin:

Using the extensible storage schema builder, retrieving all adjacent apartment rooms, embedding a GIF animation in a ribbon tooltip and controlling detail item draw order with the #RevitAPI

https://autode.sk/schemabuilderquirks

- Schema builder quirks
- Get all apartment rooms
- Embed GIF in tooltip
- Control draw order of detail items...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Retrieve Rooms, Build a Schema, Sort and Animate

Today, we look at a couple of things to pay attention to when using the extensible storage schema builder, retrieving adjacent rooms, embedding an animation in a tooltip and controlling the draw order of detail items:

- [Schema builder quirks](#2)
- [Get all apartment rooms](#3)
- [Embed GIF in tooltip](#4)
- [Control draw order of detail items](#5)

####<a name="2"></a> Schema Builder Quirks

A couple of interesting aspects of using the `SchemaBuilder` came up in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [`AddSimpleField` causes `ArchiveException 106`](https://forums.autodesk.com/t5/revit-api-forum/extensiblestorage-schemabuilder-addsimplefield-causes-quot/m-p/12529666):

**Question:** How can we fix an `InternalException` with `ArchiveException 106`, please?
It is triggered by a third-party plugin calling `SchemaBuilder.AddSimpleField`.
The issue only occurs for 1 user; many users at the company are using the plugin and multiple users using the plugin are working in the same projects as the user having the issue.
The user has 2 Autodesk accounts; the issue only occurs when they are using 1 of the accounts.
The issue does not occur when they are on the other account.
The issue occurs in all projects the user opens.
The journal file entry reads:

<pre>An ArchiveException 106 is raised at line 661 of E:\Ship\2023_px64\Source\Foundation\Utility\Archive\DataDict.cpp</pre>

The plugin debug log lists this:

<pre>Autodesk.Revit.Exceptions.InternalException:
  A managed exception was thrown by Revit or by one of its external applications.
  at Autodesk.Revit.DB.ExtensibleStorage.SchemaBuilder.AddSimpleField(String fieldName, Type fieldType)</pre>

Troubleshooting steps tried so far:

- Deleting the extensible storage objects created by the plugin. The thought is perhaps the objects became corrupted so deleting them and allowing the plugin to recreate new ones may resolve the issue, this did not work.
- Audit the model
- Disabling the plugin. This resolves the issue but the problem to solve is having the plugin not causing the issue for this user, this issue does not and has never occurred for any other user of the plugin.
- Switching to a different Autodesk account. This does resolve the issue but project requirements require the user to use the specific account that has the issue.

What can we do to resolve this issue, please? -- https://forums.autodesk.com/t5/revit-api-forum/extensiblesotrage-schemabuilder-addsimplefield-causes-quot/td-p/12527389

**Answer:** There is a known improvement logged in REVIT-156791.
The issue is "the `fieldName` is too long".
The current limitation in Revit is we only accept the field name with length less than 63.
Why does the issue only happen on one specific user account?
Please ask the user to check with the plugin owner about the `fieldName`.
It is possible that this `fieldName` is generated based on the current user name.

Another issue that can occur: given the following code to set up 3 simple fields for the schema and set the documentation for those fields:

<pre><code>FieldBuilder theVersion = schemaBuilder.AddSimpleField(VERSIONFIELD,
  typeof (int)); // create a field to store the version number
FieldBuilder theData = schemaBuilder.AddSimpleField(DATAFIELD,
  typeof (string)); // create a field to store the data
FieldBuilder isZipped = schemaBuilder.AddSimpleField(ZIPFIELD,
  typeof (bool)); // create a field to store the data
theVersion.SetDocumentation("The version number of the schema of the stored data.");
theData.SetDocumentation("The data as XML serialized, possibly zipped and base64 coded string.");
isZipped.SetDocumentation("Whether or not the stored data has been zipped.");</code></pre>

In the above code, the first `SetDocumentation` call may fail.
The issue has to do with the way fields are added on each call to `AddSimpleField`.
Internally, the fields are stored in a dynamic array and the return value of `AddSimpleField` is a pointer to the entry inside that array.
Subsequent calls to `AddSimpleField` will add new entries into this array and return pointers to those.
The problem is that each addition may trigger the array to be resized.
In that case, it will be reallocated and all entries moved to the new allocation.
Thus, any reference pointers become obsolete at that point and are now pointing to the wrong/deleted thing.
That can cause a crash; release builds may seemingly work, but the memory is not the write (right) memory to place the string in.

In general, when working with `SchemaBuilder` and adding fields, it seems best to add the field, set its properties, then continue onto the next field etc.
Hence, the code above can be fixed as follows:

<pre><code>// create a field to store the version number
schemaBuilder.AddSimpleField(VERSIONFIELD, typeof (int))
  .SetDocumentation("The version number of the schema of the stored data.");
// create a field to store the data
schemaBuilder.AddSimpleField(DATAFIELD, typeof (string))
  .SetDocumentation("The data as XML serialized, possibly zipped and base64 coded string.");
// create a field to store the data
schemaBuilder.AddSimpleField(ZIPFIELD, typeof (bool))
  .SetDocumentation("Whether or not the stored data has been zipped.");</code></pre>

####<a name="3"></a> Get All Apartment Rooms

**Question:** I want to collect all room in a given apartment.
The user just selects one external door.
So far, I have implemented this collection of rooms in the apartment using the properties `FromRoom` and `ToRoom`.
Now, I encountered a case where rooms are not separated by a door but by a room separator:

<center>
<img src="img/get_rooms_1.png" alt="Get adjacent rooms" title="Get adjacent rooms" width="500"/> <!-- Pixel Height: 743 Pixel Width: 1,176 -->
</center>

In this case, the logic above fails.
How can I handle this case?

**Answer:**
You have to do both the doors and the room separations.
So, after you pull the doors from the room, pull it's boundary segments using
the [GetBoundarySegments method](https://www.revitapidocs.com/2024/8e0919af-6172-9d16-26d2-268e42f7e936.htm).
Then, for each segment, check if the bounding element type is a room separation.
If so, evaluate a point at the middle of the curve, and translate it just inside and just outside the curve.
Pull the room at those points and you get the one room which is your active room, and one room which is the adjacent one.
Add the adjacent one to your list of rooms to process, and the list of 'included' rooms.

Some supplementary thoughts: there might be some complicated cases where one room separation line is the boundary of more than two rooms:

<center>
<img src="img/get_rooms_2.png" alt="Get adjacent rooms" title="Get adjacent rooms" width="300"/> <!-- Pixel Height: 269 Pixel Width: 377 -->
</center>

One way to handle this is to record the boundary information in a `Dictionary<ElementId, List<ElementId>>`, where the key is the element id of the boundary segment, and the value is the elements that are bounded by that segment.
You can loop through all rooms to construct the dictionary.

Possibly, though, the `GetBoundarySegments` call on room three will return a segment for the boundary to room one and a separate segment for the boundary to room 2; in that case there shouldn’t be much of a concern with tracking.
Worth checking, though.
Then, GetBoundarySegments should be enough for this request.

There are many solutions to this.
So, it’s up to you.

####<a name="4"></a> Embed GIF in ToolTip

Mauricio [SpeedCAD](https://github.com/SpeedCAD) Jorquera shared a nice solution
to [Embed GIF in ToolTip](https://forums.autodesk.com/t5/revit-api-forum/embed-gif-in-tooltip/m-p/12532476):

Before creating this custom class, I searched everywhere for an example of how to embed an animated gif in a tooltip for a button in Revit.
I looked at the [video animated ribbon item tooltip](http://thebuildingcoder.typepad.com/blog/2012/09/video-animated-ribbon-item-tooltip.html) from 2012,
but it seems that using a GIF is not possible, and I don't like the idea of hosting a video at a URL.
Anyway, if anyone has found any examples, please let me know.

For this reason, I developed a small DLL that allows adding a static image or an animated GIF to a tooltip.
If anyone is interested in using or testing this class, you can download the DLL from the following link to the GitHub repository:

- [SCADtools.Revit.UI.RibbonItemToolTip](https://github.com/SpeedCAD/SCADtools.Revit.UI.RibbonItemToolTip)

I hope this tool is useful!

Many thanks to Mauricio for implementing and sharing this!

####<a name="5"></a> Control Draw Order of Detail Items

Adrian Crisan of [Studio A International, LLC](http://www.studio-a-int.com) pointed out how to control
the [draw order of detail items](https://forums.autodesk.com/t5/revit-api-forum/draw-order-of-detail-items/m-p/12531008) using
the [DetailElementOrderUtils class](https://www.revitapidocs.com/2024/7153db7b-62cc-f36b-b6a5-0ded8af7b5be.htm):

**Question:**
I have seen a few old posts about this, but I can't find it in the API documentation.
Is it possible in Revit 2022 to change the draw order of detail items in a family document?

**Answer:**
Use the code below to change order of Detail Items in a Family.
Explanations are within the method.
Assign the method to a button, so pressing the button
consecutively will bring front/back the "Solid Fill - Blue":

<pre><code>
// Counter for how many button was pressed
int ct = 0;
void ChangeDetailItemDrawOrder()
{
  // Code provided courtesy of:
  // Studio A International, LLC
  // http://www.studio-a-int.com
  // The below code assumes you have 3 Detail Items overlapping in your Revit Family
  // They are arranged in order top to bottom as below
  // Each Detail Item is a Filled Region and their Type Names are:
  // "Solid Fill - Blue"
  // "Solid Fill - Yellow"
  // "Solid Fill - Red"
  // To bring a Detail Component to front, use API method
  // public static void BringToFront(Document pDocument, View pDBView, ElementId detailElementId)
  // https://www.revitapidocs.com/2019/055c8585-0e6c-13ae-c2af-891e0928a5a1.htm
  // To send Detail Component to back, use API method
  // public static void SendToBack(Document pDocument, View pDBView, ElementId detailElementId)
  // https://www.revitapidocs.com/2019/28209b7b-e75e-36d9-f916-d1cdaebe051d.htm

  if (activeDoc.IsFamilyDocument == true)
  {
    FilteredElementCollector frCollector = new FilteredElementCollector(activeDoc);
    // collect all Detail Components in the active document
    List<Autodesk.Revit.DB.FilledRegion> detailItems0 = frCollector
      .OfClass(typeof(Autodesk.Revit.DB.FilledRegion))
      .Cast<Autodesk.Revit.DB.FilledRegion>()
      .ToList();
    // collect all the Views in the Active Family Document
    FilteredElementCollector vCollector = new FilteredElementCollector(activeDoc);
    IEnumerable<View> allViews0 = new FilteredElementCollector(activeDoc)
      .OfClass(typeof(View))
      .OfCategory(BuiltInCategory.OST_Views)
      .Cast<View>();
    Autodesk.Revit.DB.FilledRegion detailBlue = detailItems0
      .Where(i => activeDoc.GetElement(i.GetTypeId())
        .get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_NAME).AsString()
          == "Solid Fill - Blue")
      .ToList()
      .FirstOrDefault();
    using (Transaction t = new Transaction(activeDoc, "Change drawing order"))
    {
      t.Start("Change drawing order for Detail Components");
      if (ct == 0)
      {
        // Send to back
        DetailElementOrderUtils.SendToBack(activeDoc,
          allViews0.ToList().FirstOrDefault(), detailBlue.Id);
        ct++;
      }
      else if (ct == 1)
      {
        // Bring to front
        DetailElementOrderUtils.BringToFront(activeDoc,
          allViews0.ToList().FirstOrDefault(), detailBlue.Id);
        ct = 0;
      }
      t.Commit();
    }
  }
}

private void buttonChangeDrawingOrder_Click(
  object sender,
  RoutedEventArgs e)
{
  ChangeDetailItemDrawOrder();
}
</code></pre>

**Response:**
Thank you! Works like a charm!

We briefly mentioned `DetailElementOrderUtils` way back in 2013 as one of
the [handy utility classes](https://thebuildingcoder.typepad.com/blog/2013/04/handy-utility-classes.html),
and again in the What's New notes for the 2013, 2014 and 2024 Revit API, but this is the first sample code snippet using them.

So, many thanks to Adrian for spelling it out for us!

