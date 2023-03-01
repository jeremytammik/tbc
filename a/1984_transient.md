<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- RevitLookup Ideas #146
  https://github.com/jeremytammik/RevitLookup/discussions/146

- transient elements, jig, graphics:
  draw line visible on screen
  https://forums.autodesk.com/t5/revit-api-forum/draw-line-visible-on-screen/m-p/11778165#M69522

- Open files located in ACC Docs
  https://stackoverflow.com/questions/75530623/open-files-located-in-the-accdocs
  https://forums.autodesk.com/t5/revit-api-forum/opening-a-cloud-model-with-revit-api/m-p/11767222

- RVT to IFC export with DA4 Revit
  https://autodesk.slack.com/archives/C03FXKR0H6J/p1676932151859789

- stop using jpeg
  https://daniel.do/article/its-the-future-stop-using-jpegs/
  It’s the future — you can stop using JPEGs
  An overview of some compelling alternatives.

twitter:

 @DynamoBIM  with the @AutodeskRevit #RevitAPI #BIM @AutodeskAPS https://autode.sk/simplifycurveloop

&ndash;
...

linkedin:

#BIM #DynamoBim #AutodeskAPS #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### Transient


####<a name="2"></a> RevitLookup Ideas #146

RevitLookup Ideas #146
https://github.com/jeremytammik/RevitLookup/discussions/146

####<a name="3"></a> transient elements, jig, graphics:

A couple of ideas on creating transient elements graphics similar to the AutoCAD jig functionality using
the `IDirectContext3DServer` functionality or the temporary InCanvas graphics API were recapitulated in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [drawing line visible on screen](https://forums.autodesk.com/t5/revit-api-forum/draw-line-visible-on-screen/m-p/11778165):

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/10/onbox-directcontext-jig-and-no-cdn.html" target="_blank" rel="noopener">Onbox, DirectContext Jig and No CDN</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/01/transient-graphics-humane-ai-basic-income-and-lockdown.html" target="_blank" rel="noopener">Transient Graphics, Humane AI, BI and Lockdown</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/05/flip-mirror-transform-and-transient-graphics.html" target="_blank" rel="noopener">Flip, Mirror, Transform and Transient Graphics</a></li>
</ul>

Lorenzo Virone shared a [different approach](https://forums.autodesk.com/t5/revit-api-forum/draw-line-visible-on-screen/m-p/11778165#M69522), creating and deleting database-resident Revit elements on the fly in a loop:

I faced a similar UI problem to create a rubber band between two points.
I used these two functions, `Line.CreateBound` and `NewDetailCurve`, inside a loop to create a line at the cursor position, refresh, and delete the line every 0.1 seconds, until the user chooses the second point.
A little tricky, but it worked fine for me and Revit seems to execute these 2 functions very fast.

I didn't use it to create a line, but this trick will work with anything:
create new elements on each mouse movement, refresh, delete the created elements and replace them with new ones.
It can technically work with anything, model or detail elements, and it's easy to implement, because you just need to call the two methods, like this:

<pre class="prettyprint">
  bool done = false;
  List<ElementId> temp = new List<ElementId>();

  while(!done)
  {
    doc.Delete(temp);

    // Create temp elements
    // Save their IDs in `temp`
    // Set `done` to `true` when finished

    doc.regenerate();
    uidoc.RefreshActiveView();
    Thread.Sleep(500); // milliseconds
  }

  // Your final elements are in `temp`
</pre>

Many thanks to Lorenzo for sharing this nice solution.

####<a name="4"></a> Open files located in ACC Docs

Open files located in ACC Docs
https://stackoverflow.com/questions/75530623/open-files-located-in-the-accdocs
https://forums.autodesk.com/t5/revit-api-forum/opening-a-cloud-model-with-revit-api/m-p/11767222

####<a name="5"></a> RVT to IFC export with DA4 Revit

RVT to IFC export with DA4 Revit
https://autodesk.slack.com/archives/C03FXKR0H6J/p1676932151859789

####<a name="6"></a> stop using jpeg

stop using jpeg
https://daniel.do/article/its-the-future-stop-using-jpegs/
It’s the future — you can stop using JPEGs
An overview of some compelling alternatives.


Many thanks to
 for the interesting pointers!

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 349 x 231 pixels -->
</center>

**Question:**

**Answer:**

**Response:**

 
