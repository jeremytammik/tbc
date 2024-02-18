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
  UIDocument.UpdateAllOpenViews Method
  https://www.revitapidocs.com/2024/5cc3231e-ee7e-e1fc-2bd6-d164da617954.htm

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







####<a name="2"></a> UpdateAllOpenViews Doesn't Refresh Spot Elevation Prefix

**Question:** How can I make Revit refresh spot elevation prefix automatically?
My code adds a plus-minus `&#177;` sign to the spot elevation tag "prefix" in batches.
However, nothing changes in the view unless you refresh each tag.
I call both `RefreshActiveView` and `Regenerate` to no avail.

**Answer:** Try calling
the [`UIDocument` `UpdateAllOpenViews` method](https://www.revitapidocs.com/2024/5cc3231e-ee7e-e1fc-2bd6-d164da617954.htm).
That forces a redraw, which I think is one step higher than a refresh.
Refresh triggers a redraw only if a change is detected and it seems in this case it's failing to detect the change.
`UpdateAllOpenViews` was introduced in Revit 2028 to force
a [view update for DirectContext3D](https://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html#3.26.15).
It sounds really powerful:

> Updates all open views in this document after elements have been changed, deleted, selected or de-selected. Graphics in the views are fully redrawn regardless of which elements have changed. This function should only rarely be needed, but might be required when working with graphics drawn from outside of Revit's transactions and elements, for example, when using DirectContext3D.

> This function is potentially expensive as many views may be updated at once, including regeneration of view's geometry and redisplay of graphics. Thus for most situations it is recommended that API applications rely on the Revit application framework to update views more deliberately.

Well worth taking a look at!

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
<img src="img/select_to_refresh.png" alt="Select to refresh graphics" title="Select to refresh graphics" width="100"/> <!-- Pixel Height: 358 Pixel Width: 602 -->
</center>

The highlighted code is the final solution used to resolve the issue.

Many thanks to Shen Wang and Dimitar Venkov for sharing this!

####<a name="4"></a> Excel Data Exchange Options

Many add-ins exchange Revit data with Microsoft Excel spreadsheets.
The SDK has two samples which do import/export from Excel spreadsheets, FireRating and ArchSample.
They rely on a very old DLL which isn't playing nicely with .NET Core.
If the Revit to Excel workflow is high value, it might be worth while modernising.
If it is low value, export to CSV or some other easily supported document type may do the trick.

The [3rd most downloaded add-in in the AppStore](https://apps.autodesk.com/RVT/en/Detail/Index?id=6290726048826015851&appLang=en&os=Win64) is
an Import/Export Excel tool
using [EPPlus](https://github.com/EPPlusSoftware/EPPlus),
and that exchange with Excel is an important part of many other popular add-ins.

Here is a modern way
to [Open a spreadsheet document for read-only access](https://learn.microsoft.com/en-us/office/open-xml/spreadsheet/how-to-open-a-spreadsheet-document-for-read-only-access?tabs=cs-0%2Ccs-1%2Ccs-2%2Ccs).

You can also use a COM library that works so long as you have Excel installed on your computer.

By the way, for the sake of completeness, I implemented a multi-project cloud-based NoSql version of the FireRating SDK sample using the Revit Element `UniqueId` and MongoDB instead of element id and Excel in [FireRatingCloud](https://github.com/jeremytammik/FireRatingCloud).

####<a name="5"></a> Defining the Start View

Adrian Crisan of [Studio A International, LLC](http://www.studio-a-int.com) shared a solution using
the [StartingViewSettings class](https://www.revitapidocs.com/2024/aaa6f49c-faeb-851e-45e9-d3d5799c1753.htm)
for [defining the start view using Revit API](https://forums.autodesk.com/t5/revit-api-forum/defining-the-start-view-using-revit-api/m-p/12506862):

**Question:** Is it possible to define the start view using the API?
Similar to the UI functionality
for [specify the starting view for a model](https://help.autodesk.com/view/RVT/2024/ENU/?guid=GUID-622E667E-FB0B-47E1-8F66-E237A70771BD).
At the moment, I can only find a way to access the parameter and find out if a view is defined as the start view.
Thanks  :-)

**Answer:** Use this method to set your starting view:

<pre><code class="language-cs">void SetStartingView ()
{
  // Code provided courtesy of:
  // Studio A International, LLC
  // http://www.studio-a-int.com
  // The below code set the Starting View to a specific view that exists in Active Project
  FilteredElementCollector feCollector = new FilteredElementCollector(activeDoc);
  myView = feCollector
    .OfClass(typeof(Autodesk.Revit.DB.View)).Cast&lt;Autodesk.Revit.DB.View&gt;()
    .Where&lt;Autodesk.Revit.DB.View&gt;(v
      =&gt; ViewType.ThreeD == v.ViewType
        && v.IsTemplate == false
        && v.Name == "my3DStartingView")
    .ToList()
    .FirstOrDefault();
  FilteredElementCollector svsCollector = new FilteredElementCollector(activeDoc);
  Autodesk.Revit.DB.StartingViewSettings svs = svsCollector
    .OfClass(typeof(StartingViewSettings))
    .Cast&lt;Autodesk.Revit.DB.StartingViewSettings&gt;()
    .ToList()
    .FirstOrDefault();
  if (myView is object)
  {
  ElementId myViewId = new ElementId(Convert.ToInt32((myView.Id.ToString())));
  if (svs.IsAcceptableStartingView(myViewId))
  {
  using (Transaction t = new Transaction(activeDoc, "Set Starting View"))
  {
  t.Start("Set Starting View");
  svs.ViewId = myViewId;
  t.Commit();
  }
  }
  }
}
</code></pre>

**Response:** It works perfectly! Thanks a lot!

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

####<a name="8"></a> Previous Climate Changes Negligeable


To put that into perspective, [xkcd](https://xkcd.com) published
[A TIMELINE OF EARTH'S AVERAGE TEMPERATURE SINCE THE LAST ICE AGE GLACIATION](https://xkcd.com/1732/) to
demonstrate

> when people say "the climate has changed before", these are the kinds of changes they're talking about.

<center>
<img src="img/xkcd_earth_temperature_timeline.png" alt="Earth average temperature timeline" title="Earth average temperature timeline" width="600"/> <!-- Pixel Height: 29,913 Pixel Width: 1,480 -->
</center>

####<a name="9"></a> PV Panel Price Trend

Are you thinking about installing PV?
Now is a good time for buying PV panels...
[price trend below 13 cents per Watt](https://www.pvxchange.com/Price-Index),
lowest price ever.
Will the trend continue?
How fast can it turn around?

<center>
<img src="img/2024_pv_panel_price_trend.png" alt="PV panel price trend" title="PV panel price trend" width="600"/> <!-- Pixel Height: 1,556 Pixel Width: 1,676 -->
</center>

