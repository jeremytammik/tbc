<head>
<title>The Building Coder</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="3dwc.css"/>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>
</head>

<!---

- 12072182 [Revit API Documentation Online]
  http://forums.autodesk.com/t5/revit-api/revit-api-documentation-online/m-p/6495377

Online Revit API Docs and Convex Hull #revitapi #3dwebcoder @AutodeskRevit @AutodeskForge #aec #bim

Today I happily present a brilliant piece of Revit API news on the documentation side of things, and another handy utility method for your Revit API programming toolbox 
&ndash; Online Revit API Documentation 
&ndash; 2D Convex Hull Algorithm in C# using <code>XYZ</code> points...

-->

### Online Revit API Docs and Convex Hull

Today I happily present a brilliant piece of Revit API news on the documentation side of things, and another handy utility method for your Revit API programming toolbox:

- [Online Revit API documentation](#2)
- [2D convex hull algorithm in C# using `XYZ` points](#3)


#### <a name="2"></a>Online Revit API Documentation

The contents of the Revit API help file RevitAPI.chm are finally available online.

And not only that, but the web site includes all three versions for Revit 2015, Revit 2016 and Revit 2017.

As you know, the two main pieces of Revit API documentation are the Revit API help file RevitAPI.chm, included with the Revit SDK, available from
the [Revit Developer Centre](http://www.autodesk.com/developrevit), and
the [developer guide](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42),
provided in the 'Developers' section of the
online [Revit Help](http://help.autodesk.com/view/RVT/2017/ENU).

The help file was not available online, though, which also means that it was not included in standard Internet searches
using [Google](https://www.google.com)
or [DuckDuckGo](https://duckduckgo.com).

With the notable exception
of [Revit 2014](http://thebuildingcoder.typepad.com/blog/2014/01/revit-api-help-online-and-hiking-on-la-palma.html),
[revitapisearch.com](http://revitapisearch.com),
implemented by Peter Boyer of the Dynamo team.

Just a few weeks ago, Arif Hanif expressed interest to do the same for Revit 2017 in
a [couple](http://thebuildingcoder.typepad.com/blog/2016/04/whats-new-in-the-revit-2017-api.html#comment-2823075296)
of [comments](http://thebuildingcoder.typepad.com/blog/2016/04/whats-new-in-the-revit-2017-api.html#comment-2824265506)
on [What's New in the Revit 2017 API](http://thebuildingcoder.typepad.com/blog/2016/04/whats-new-in-the-revit-2017-api.html).

Well, someone beat him to it.

In
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [Revit API Documentation Online ](http://forums.autodesk.com/t5/revit-api/revit-api-documentation-online/m-p/6495377),
[@gtalarico](http://forums.autodesk.com/t5/user/viewprofilepage/user-id/67891) says:

> Since Google doesn't seem too excited to index my 60k+ page website, I wanted to share it here so people can find it and hopefully use it! : )

> It currently includes the full official documentation (from the CHM file) for APIS 2015, 2016, and 2017:

<center>
<span style="font-size: 120%; font-weight: bold">
[www.revitapidocs.com](http://www.revitapidocs.com)
</span>
</center>

> It was a lot of planning and work, but came together faster and better than I expected.

<center>
<img src="img/revitapidocs.png" alt="www.revitapidocs.com" width="400">
</center>
 
Please let him know if you have any feedback on it.

Ever so many thanks to gtalarico for all his work and making this useful resource available to the global Revit API community!

#### <a name="2.1"></a>Contributing and Implementation Details

Gtalarico added some additional info on the project:

The code is on github at [github.com/gtalarico/revitapidocs](https://github.com/gtalarico/revitapidocs).
 
The project is definately open to collaborators. Welcome!

It needs +code docs, +test coverage, and can probably be improved and optimized significantly by more seasoned web developers.

Regarding github pages, it could probably be done, but I haven't used it myself, so I don't know the limitations.

Here are some of the challenges and constraints:
 
1. Namespace Menu:
    - Each API/year has an index with around 20K nested entries, sometimes many levels deep.
    Performance can get tricky, and so is creating a good and responsive UI for browsing it, which is why I wanted it  to be collapsible.
    If I recall correctly, Readthedocs for instance, limits the depth of the menu.
2. Content: 
    - The content I had access to (.html files extracted from chm) were not pretty, so I had to do some unusual CSS overrides and eventually batch processed the 60k+ html files to remove unnecessary JS and html code to make the pages look good and perform well. I was also was concerned about appearance to google crawler (cleaned code, and added schema.org structured data on every page).
3. Performance:
    - The namespace html file alone was almost 3MB and 140K lines of html code, which is not good.
    To optimize it, I am serving the menu asynchronously as json, so it loads while the rest of the content is being built, and can be cached.
4. Built-in search:
    - I originally tried using google custom search, but google can take a long time to index it (if it happens at all - 60k+ pages) 
    Even with a full sitemap, it will probably just take time, but I didn't want to wait.
    So I replaced the Google Custom Search box with my own custom search.
    I tried a JS client side search, similar to what git pages has, but it was crashing the browser (remember namespace is +100K lines), so I ended up pushing it server side which makes it reasonably fast, e.g., [www.revitapidocs.com/2015/search?query=viewschedule](http://www.revitapidocs.com/2015/search?query=viewschedule).


#### <a name="3"></a>2D Convex Hull Algorithm in C# using `XYZ`

Yesterday, I mentioned the convex hull calculation as one option for determining
the [bounding box of selected elements or entire model](http://thebuildingcoder.typepad.com/blog/2016/08/vacation-end-forge-news-and-bounding-boxes.html#8).

Maxence replied in
a [comment](http://thebuildingcoder.typepad.com/blog/2016/08/vacation-end-forge-news-and-bounding-boxes.html#comment-2839904399) on
that post and provided a convex hull implementation in C#.

It is a 2D algorithm implementing the [Jarvis march or Gift wrapping algorithm](https://en.wikipedia.org/wiki/Gift_wrapping_algorithm):

It makes use of an extension method `MinBy` on the generic `IEnumerable` class,
from [MoreLINQ](https://github.com/morelinq/MoreLINQ/blob/master/MoreLinq/MinBy.cs) by
Jonathan Skeet:

<pre class="code">
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">IEnumerableExtensions</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">tsource</span>&nbsp;MinBy&lt;<span style="color:#2b91af;">tsource</span>,&nbsp;<span style="color:#2b91af;">tkey</span>&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">tsource</span>&gt;&nbsp;source,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Func</span>&lt;<span style="color:#2b91af;">tsource</span>,&nbsp;<span style="color:#2b91af;">tkey</span>&gt;&nbsp;selector&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;source.MinBy(&nbsp;selector,&nbsp;<span style="color:#2b91af;">Comparer</span>&lt;<span style="color:#2b91af;">tkey</span>&gt;.Default&nbsp;);
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">tsource</span>&nbsp;MinBy&lt;<span style="color:#2b91af;">tsource</span>,&nbsp;<span style="color:#2b91af;">tkey</span>&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">IEnumerable</span>&lt;<span style="color:#2b91af;">tsource</span>&gt;&nbsp;source,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Func</span>&lt;<span style="color:#2b91af;">tsource</span>,&nbsp;<span style="color:#2b91af;">tkey</span>&gt;&nbsp;selector,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IComparer</span>&lt;<span style="color:#2b91af;">tkey</span>&gt;&nbsp;comparer&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;source&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentNullException</span>(&nbsp;<span style="color:blue;">nameof</span>(&nbsp;source&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;selector&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentNullException</span>(&nbsp;<span style="color:blue;">nameof</span>(&nbsp;selector&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;comparer&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentNullException</span>(&nbsp;<span style="color:blue;">nameof</span>(&nbsp;comparer&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">IEnumerator</span>&lt;<span style="color:#2b91af;">tsource</span>&gt;&nbsp;sourceIterator&nbsp;=&nbsp;source.GetEnumerator()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!sourceIterator.MoveNext()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">InvalidOperationException</span>(&nbsp;<span style="color:#a31515;">&quot;Sequence&nbsp;was&nbsp;empty&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">tsource</span>&nbsp;min&nbsp;=&nbsp;sourceIterator.Current;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">tkey</span>&nbsp;minKey&nbsp;=&nbsp;selector(&nbsp;min&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">while</span>(&nbsp;sourceIterator.MoveNext()&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">tsource</span>&nbsp;candidate&nbsp;=&nbsp;sourceIterator.Current;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">tkey</span>&nbsp;candidateProjected&nbsp;=&nbsp;selector(&nbsp;candidate&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;comparer.Compare(&nbsp;candidateProjected,&nbsp;minKey&nbsp;)&nbsp;&lt;&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;min&nbsp;=&nbsp;candidate;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;minKey&nbsp;=&nbsp;candidateProjected;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;min;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

With that helper method in hand, the convex hull implementation is quite short and sweet:

<pre class="code">
<span style="color:blue;">#region</span>&nbsp;Convex&nbsp;Hull
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;the&nbsp;convex&nbsp;hull&nbsp;of&nbsp;a&nbsp;list&nbsp;of&nbsp;points&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;using&nbsp;the&nbsp;Jarvis&nbsp;march&nbsp;or&nbsp;Gift&nbsp;wrapping:</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;https://en.wikipedia.org/wiki/Gift_wrapping_algorithm</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Written&nbsp;by&nbsp;Maxence.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;&nbsp;ConvexHull(&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;&nbsp;points&nbsp;)
{
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;points&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)&nbsp;<span style="color:blue;">throw</span>&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ArgumentNullException</span>(&nbsp;<span style="color:blue;">nameof</span>(&nbsp;points&nbsp;)&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;startPoint&nbsp;=&nbsp;points.MinBy(&nbsp;p&nbsp;=&gt;&nbsp;p.X&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;convexHullPoints&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;();
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;walkingPoint&nbsp;=&nbsp;startPoint;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;refVector&nbsp;=&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisY.Negate();
&nbsp;&nbsp;<span style="color:blue;">do</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;convexHullPoints.Add(&nbsp;walkingPoint&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;wp&nbsp;=&nbsp;walkingPoint;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;rv&nbsp;=&nbsp;refVector;
&nbsp;&nbsp;&nbsp;&nbsp;walkingPoint&nbsp;=&nbsp;points.MinBy(&nbsp;p&nbsp;=&gt;
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;angle&nbsp;=&nbsp;(&nbsp;p&nbsp;-&nbsp;wp&nbsp;).AngleOnPlaneTo(&nbsp;rv,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisZ&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;angle&nbsp;&lt;&nbsp;1e-10&nbsp;)&nbsp;angle&nbsp;=&nbsp;2&nbsp;*&nbsp;<span style="color:#2b91af;">Math</span>.PI;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;angle;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;refVector&nbsp;=&nbsp;wp&nbsp;-&nbsp;walkingPoint;
&nbsp;&nbsp;}&nbsp;<span style="color:blue;">while</span>(&nbsp;walkingPoint&nbsp;!=&nbsp;startPoint&nbsp;);
&nbsp;&nbsp;convexHullPoints.Reverse();
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;convexHullPoints;
}
<span style="color:blue;">#endregion</span>&nbsp;<span style="color:green;">//&nbsp;Convex&nbsp;Hull</span></pre>

For testing purposes, I make use of it like this in the `CmdListAllRooms` external command:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;bounding&nbsp;box&nbsp;calculated&nbsp;from&nbsp;the&nbsp;room&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;boundary&nbsp;segments.&nbsp;The&nbsp;lower&nbsp;left&nbsp;corner&nbsp;turns&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;out&nbsp;to&nbsp;be&nbsp;identical&nbsp;with&nbsp;the&nbsp;one&nbsp;returned&nbsp;by&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;the&nbsp;standard&nbsp;room&nbsp;bounding&nbsp;box.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;&nbsp;GetConvexHullOfRoomBoundary(
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">BoundarySegment</span>&gt;&gt;&nbsp;boundary&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;&nbsp;pts&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">BoundarySegment</span>&gt;&nbsp;loop&nbsp;<span style="color:blue;">in</span>&nbsp;boundary&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">BoundarySegment</span>&nbsp;seg&nbsp;<span style="color:blue;">in</span>&nbsp;loop&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;c&nbsp;=&nbsp;seg.GetCurve();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pts.AddRange(&nbsp;c.Tessellate()&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;n&nbsp;=&nbsp;pts.Count;
 
&nbsp;&nbsp;pts&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;(
&nbsp;&nbsp;&nbsp;&nbsp;pts.Distinct&lt;<span style="color:#2b91af;">XYZ</span>&gt;(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">CmdWallTopFaces</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.<span style="color:#2b91af;">XyzEqualityComparer</span>(&nbsp;1.0e-4&nbsp;)&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;{0}&nbsp;points&nbsp;from&nbsp;tessellated&nbsp;room&nbsp;boundaries,&nbsp;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#a31515;">&quot;{1}&nbsp;points&nbsp;after&nbsp;cleaning&nbsp;up&nbsp;duplicates&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;n,&nbsp;pts.Count&nbsp;);
 
&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">Util</span>.ConvexHull(&nbsp;pts);
}
</pre>

Initially, I did not include the call to `Distinct`, which eliminates duplicate points returned by Revit that are intended to represent the same room corner but have small offsets from each other due to limited precision, or too high precision, whichever way you look at it.

Here are the
diffs <a href="https://github.com/jeremytammik/the_building_coder_samples/compare/2017.0.127.8...2017.0.127.9">with just the pure convex hull implementation</a>
and <a href="https://github.com/jeremytammik/the_building_coder_samples/compare/2017.0.127.9...2017.0.127.10">after adding the call to <code>Distinct</code></a>.

I tested it on the same ten rectangular rooms as yesterday and verified that their convex hulls correspond to their bounding boxes returned by Revit.

I also tested on the following squiggly room with some spline-shaped edges:

<center>
<img src="img/room_squiggly.png" alt="Squiggly room" width="388">
</center>

That returns the following results:

<pre>
355 points from tessellated room boundaries, 324 points after cleaning up duplicates

Room nr. '1' named 'Room 1' at (-77.61,15.1,0) with lower left corner (-106.43,-8.65,0), convex hull (-104.93,-8.65,0), (-40.75,-8.51,0), (-40.41,33.07,0), (-106.43,33.27,0), bounding box ((-106.43,-8.65,0),(-40.41,33.27,13.12)) and area 1483.20391607451 sqf has 1 loop and 31 segments in first loop.
</pre>

Everything discussed above is published 
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[release 2017.0.127.10](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.127.10).

Many thanks to Maxence for providing the nice convex hull algorithm implementation!
