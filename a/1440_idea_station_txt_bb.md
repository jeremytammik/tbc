<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Idea Station and TextNote Bounding Box #revitAPI #3dwebcoder @AutodeskForge #adsk #aec #bim #AutodeskAnswerDay

Thank you for your questions during the Autodesk answer day. I am still busy supporting the Barcelona Forge Accelerator participants in the Autodesk office. I am also working on a very exciting new project connecting BIM and the cloud using a direct socket.io connection to communicate changes back to the desktop, instead of the inefficient polling mechanism I implemented in previous projects. Once again, it consists of two parts, the node.js web server and View and Data API handler and the C# .NET Revit API add-in client
&ndash; Revit Idea Station
&ndash; TextNote Bounding Box Requires Regen
&ndash; Regeneration Performance Benchmark...

-->

### Idea Station and TextNote Bounding Box

Thank you for your questions during the [Autodesk answer day](http://forums.autodesk.com/t5/autodeskhelp/autodesk-answer-day-may-18th-2016/ba-p/6277390).

I am still busy supporting the Barcelona [Forge](http://forge.autodesk.com) [Accelerator](http://autodeskcloudaccelerator.com) participants in the Autodesk office.

Wednesday night, we took time off for a two-hour sailboat cruise:

<center>
<a data-flickr-embed="true"  href="https://www.flickr.com/photos/jeremytammik/albums/72157668599840235" title="Forge Accelerator Ahoy"><img src="https://farm8.staticflickr.com/7594/27014775142_d30eb90c39_n.jpg" width="320" height="240" alt="Forge Accelerator Ahoy"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
</center>

I am also working on a very exciting new project connecting BIM and the cloud using a direct [socket.io](http://socket.io) connection
to communicate changes back to the desktop, instead of the inefficient polling mechanism I implemented in previous projects such
as [RoomEditorApp](https://github.com/jeremytammik/RoomEditorApp)
and [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud).

Once again, it consists of two parts:

- [roomedit3d](https://github.com/jeremytammik/roomedit3d), the node.js web server and View and Data API handler.
- [Roomedit3dApp](https://github.com/jeremytammik/Roomedit3dApp), the C# .NET Revit API add-in client.

You can check out the GitHub projects in their current state as work in progress.

I'll talk more about them as soon as I have a moment's time.

First, however, let me mention a couple of other issues:

- [Revit Idea Station](#2)
- [TextNote Bounding Box Requires Regen](#3)
- [Regeneration Performance Benchmark](#4)



#### <a name="2"></a>Revit Idea Station

Have you ever wanted to submit new or review existing feature requests for Revit?

If so, you will be glad to hear
the [IdeaStation for Revit](http://forums.autodesk.com/t5/revit-ideas/idb-p/302/tab/most-recent) has
finally been launched.

If. like me, you are interested specifically in Revit API wishes, you can select the category `Revit` and the tag `API`.

That takes you to [Revit Ideas &gt; Tag: "API" in "Revit Ideas"](http://forums.autodesk.com/t5/tag/API/tg-p/board-id/302).

Note that there is not yet any automatic process in place to automate the connection from the idea station into the development team system.

If something is imperative, you can still file a wish list item for it using the standard existing ADN
and [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) pathways.

Idea station will help support these wish list items with votes and priorities.


#### <a name="3"></a>TextNote Bounding Box Requires Regen

**Question:** We found that in Revit 2017, the TextNote bounding box is null in a drafting view.

The following code returns a valid bounding box in previous versions:

<pre class="code">
  BoundingBoxXYZ box = element.get_BoundingBox( view );
</pre>

If the element is a TextNote and the view is a ViewDrafting, the result is always null in Revit 2017.

I use this code to position text in my electrical schematic add-in.

It worked fine in the earlier versions of Revit.

Is there any other method I can use to obtain the bounding box of a TextNote using the Revit 2017 API?

**Answer:** Matt Taylor provided a solution in
his [comment](http://thebuildingcoder.typepad.com/blog/2016/04/whats-new-in-the-revit-2017-api.html#comment-2682251724)
on [What's New in the Revit 2017 API](http://thebuildingcoder.typepad.com/blog/2016/04/whats-new-in-the-revit-2017-api.html):

When using `myTextNote.BoundingBox(view)` straight after `TextNote.Create`, it should be preceded with `doc.Regenerate`:

<pre class="code">
  dim myTextNote as TextNote = TextNote,Create(doc, ....
  doc.Regenerate '<--This is now required. Otherwise, the bounding box is null.
  dim bbox a s BoundingBox = myTextNote.BoundingBox(view)
</pre>

This is new behaviour in Revit 2017.

The *Revit Platform API Changes and Additions.docx* document should probably contain things like this.

Just sayin!


#### <a name="4"></a>Regeneration Performance Benchmark

Later, Matt added another [note on regen performance](http://thebuildingcoder.typepad.com/blog/2016/04/whats-new-in-the-revit-2017-api.html#comment-2682319277):

That was the first solution I tried, so I was pretty lucky.

I benchmarked the regeneration also, in case anyone is interested?

Approximately 1-2/1000ths of a second.

I used to be scared of using `doc.regenerate` (because of the performance hit) &ndash; not any more.

Many thanks to Matt for pointing this out!
