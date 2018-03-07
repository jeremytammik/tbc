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

- open view using UIDocument.ShowElements
  13922857 [Change active document]
  https://forums.autodesk.com/t5/revit-api-forum/change-active-document/m-p/7787792
  https://forums.autodesk.com/t5/revit-api-forum/how-to-open-and-active-a-new-document-that-is-not-saved/m-p/7710749
  http://www.architecture-tech.com/2012/09/zoom-to-awesome.html
  Zoom To Awesome!

  UIApplication app = commandData.Application;
  UIDocument doc = app.ActiveUIDocument;

  ElementSet set = doc.Selection.Elements;
  int n = set.Size;

  if( 0 == n )
  {
      message = "Please select at least one element to zoom to.";
      return Result.Cancelled;
  }
  try
  {
      doc.ShowElements(set);
  }
  catch
  {
      foreach (Element e in set)
      {
          elements.Insert(e);
      }
      message = string.Format( "Cannot zoom to element{0}.",
        1 == n ? "" : "s" );

      return Result.Failed;
  }
  return Result.Succeeded;

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon

&ndash; 
...

--->

### Switch View by Showing Elements

####<a name="2"></a>

Normally, you can open and switch between documents using the `OpenAndActivateDocument` method.

However, iut requires a file path, which may not have been defined yet, in the case of a newly created document.

In that case, you can open and switch between different documents and their views by calling the `UIDocument` `ShowElements` method instead.

This was discussed again in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [changing thee active document](https://forums.autodesk.com/t5/revit-api-forum/change-active-document/m-p/7787792),
and previously
in [how to open and active a new document that is not saved](https://forums.autodesk.com/t5/revit-api-forum/how-to-open-and-active-a-new-document-that-is-not-saved/m-p/7710749):

**Question:** When you have 2 Revit projects open and want to switch between them, it can be done with:

<pre class="code">
application.OpenAndActivateDocument(file).
</pre>

I used `Document.PathName` to get the filename required.

Now I come accross problems with files on Revit Server and BIM 360 docs, because their `Document.Pathname` stays empty.

So, my question is, how do I switch between those documents (when `Document.Pathname` is empty)?

Is there a way to switch between documents without having to specify the file name?

**Answer:** The only way I have found is indirectly via `UIDocument.ShowElements`.

You pick an element from the DB document you want to change to, create a `UIDocument` object from a DB document and use `UIDocument.ShowElements`. You have to handle the occasional “No good view found” dialogue, but it seems to always switch the active document regardless of what is found. Helps if it is a `View` specific element.

Not sure if there is a better way...

When you call `UIDocument.ShowElements`, the active `Document` will change to the document you are showing elements in, therefore:

If you filter for any element in one of the views of the newly created document, you can call it using that element to activate that document.

Sometimes you get the dialogue 'No good view found', which is odd, considering you know there is at least one view with the element in considering you are filtering for it by view. You can handle the appearance of this dialogue and the `ActiveDocument` still gets changed. I believe the new document generally has a view with `Elevation` markers, hence there is always something to find and show.

I don't know the implications of changing the `ActiveDocument` this way or why the API has no ability to directly change the `ActiveDocument` directly, but I suspect if it were easy in terms of how the API works, it would have been done by now.

This workaround was also mentioned in The Building Coder discussion
on [mirroring in a new family and changing active view](http://thebuildingcoder.typepad.com/blog/2010/11/mirroring-in-a-new-family-and-changing-active-view.html):

> The first issue that arises is that the mirror command requires a current active view, which is not automatically present in the family document. Joe discovers a workaround for that issue using the `ShowElements` method. It generates an unwanted warning message, so a second step is required to deal with eliminating that as well.

> As you can see on reading the final solution carefully, you can use the `ShowElements` method to change the active view and even switch it between the family and project documents. The official Revit 2011 API does not provide any method to switch the active view, but using `ShowElements` can be used to create a workaround for that."

####<a name="3"></a>

<pre class="code">
</pre>

I implemented a new external
command [CmdSwitchDoc](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdSwitchDoc.cs)
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) to try this out, in
[release 2018.0.138.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.138.0).

####<a name="4"></a>

<center>
<img src="img/.png" alt="" width="100"/>
</center>
