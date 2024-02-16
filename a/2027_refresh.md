<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
</head>

<!---

- refresh spot elevation prefix
  https://autodesk.slack.com/archives/C0SR6NAP8/p1706517751186399

- UIDocument.UpdateAllOpenViews Method
  https://www.revitapidocs.com/2024/5cc3231e-ee7e-e1fc-2bd6-d164da617954.htm
  https://autodesk.slack.com/archives/C0SR6NAP8/p1706517751186399

- Excel -- RVT data exchange options
  https://autodesk.slack.com/archives/C0SR6NAP8/p1706289884274909

- Defining the start view using Revit API
  https://forums.autodesk.com/t5/revit-api-forum/defining-the-start-view-using-revit-api/m-p/12506862#M76426

- People are really bad at understanding just how big LLM's actually are.
  I think this is partly why they belittle them as 'just' next-word predictors.
  https://x.com/jam3scampbell/status/1748200331215835561?s=20
  Searle's Chinese Room: Slow Motion Intelligence
  http://lironshapira.blogspot.com/2011/02/searles-chinese-room-intelligence-in.html?m=1

- Base rate fallacy (redirect from False positive paradox)
  https://en.wikipedia.org/wiki/Base_rate_fallacy#False_positive_paradox
  courtesy of Cory Doctorow in Little Brother

- The period from February 2023 to January 2024 reached 1.52C of warming compared with pre-industrial levels, i.e., we have achieved
  the [world's first year-long breach of the key 1.5C warming limit](https://www.bbc.com/news/science-environment-68110310)
  [2023 confirmed as world's hottest year on record](https://www.bbc.com/news/science-environment-67861954)

twitter:

 #RevitAPI @AutodeskRevit #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Refresh Graphics



<center>
<img src="img/" alt="" title="Year of the Drago" width="100"/> <!-- Pixel Height: 500 Pixel Width: 670 -->
</center>







####<a name="2"></a> Refresh Spot Elevation Prefix

**Question:** How can I make Revit refresh spot elevation prefix automatically?
My code adds a plus-minus `&#177;` sign to the spot elevation tag "prefix" in batches.
However, nothing changes in the view unless you refresh each tag.
I call both `RefreshActiveView` and `Regenerate` to no avail.

**Answer:** Try calling `UpdateAllOpenViews`.
That forces a redraw, which I think is one step higher than a refresh
Refresh triggers a redraw only if a change is detected and it seems in this case it's failing to detect the change

**Response:** I tried `UpdateAllOpenViews` but failed.
However, I found a way to update the tag by changing the view scale manually.
Is there any method to do the similar thing through API?

**Answer:** You can change a view's scale from the `Scale` property, but I'm not sure that will help here.
I don't know of any other API call that would force the view to redraw, sorry.

Later: Try to get the UiView of the active view, close it and then re-open it with the `UiDoc.ActiveView` property.
It's pretty extreme, but if nothing else works...

**Response:** I found a solution: just select all the tags as the selected annotation elements; after this operation, all the tags will update.
Here is a code snippet:

<pre><code class="language-cs">
ts. Start ():
foreach (SpotDimension item in spotdimensionList)
{
  Parameter para = item. get Parameter (BuiltInParameter. SPOT_ _ELEV_DISPLAY_ELEVAT...
  if (para.AsInteger () == 3)
    MixResetter (item, spotdimensionTypeCollector):
  else
    DefaultResetter (item, spotdimensionIypeCollector):
  doc.Regenerate();
  uidoc.Selection.SetElementIds(new List&lt;ElementId&gt; ( item. id )):
}
//doc.ActiveView. Scale = 100;
ts. Commit():
//uidoc.UpdateAl10penViews () :
</code></pre>

<center>
<img src="img/" alt="" title="Year of the Drago" width="100"/> <!-- Pixel Height: 358 Pixel Width: 602 -->
</center>

The highlighted code is the final solution used to resolve the issue.

Many thanks to Shen Wang for sharing this!

####<a name="3"></a> UIDocument.UpdateAllOpenViews Method

UIDocument.UpdateAllOpenViews Method
https://www.revitapidocs.com/2024/5cc3231e-ee7e-e1fc-2bd6-d164da617954.htm
https://autodesk.slack.com/archives/C0SR6NAP8/p1706517751186399

####<a name="4"></a> Excel Data Exchange Options

Excel -- RVT data exchange options
https://autodesk.slack.com/archives/C0SR6NAP8/p1706289884274909

####<a name="5"></a> Defining the Start View

Defining the start view using Revit API
https://forums.autodesk.com/t5/revit-api-forum/defining-the-start-view-using-revit-api/m-p/12506862#M76426

####<a name="6"></a> People are really bad at understanding just how big LLM's actually are.

People are really bad at understanding just how big LLM's actually are.
I think this is partly why they belittle them as 'just' next-word predictors.
https://x.com/jam3scampbell/status/1748200331215835561?s=20
Searle's Chinese Room: Slow Motion Intelligence
http://lironshapira.blogspot.com/2011/02/searles-chinese-room-intelligence-in.html?m=1

####<a name="7"></a> Base Rate Fallacy

Base rate fallacy (redirect from False positive paradox)
https://en.wikipedia.org/wiki/Base_rate_fallacy#False_positive_paradox
courtesy of Cory Doctorow in Little Brother

####<a name="8"></a> Last Year Was Hot

The period from February 2023 to January 2024 reached 1.52C of warming compared with pre-industrial levels, i.e., we have achieved
the [world's first year-long breach of the key 1.5C warming limit](https://www.bbc.com/news/science-environment-68110310)
[2023 confirmed as world's hottest year on record](https://www.bbc.com/news/science-environment-67861954)

Global warming is a joke.

Even the Antarctic ice sheets are cracking up.

John Burn-Murdoch of the Financial Times
Is the west talking itself into decline?

####<a name="8"></a> Last Year Was Hot

To put that into perspective, [xkcd](https://xkcd.com) published
[A TIMELINE OF EARTH'S AVERAGE TEMPERATURE SINCE THE LAST ICE AGE GLACIATION](https://xkcd.com/1732/) to
demonstrate

> when people say "the climate has changed before", these are the kinds of changes they're talking about.

<center>
<img src="img/" alt="" title="Year of the Drago" width="700"/> <!-- Pixel Height: 29,913 Pixel Width: 1,480 -->
</center>

