<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

#dotnet #csharp #geometry
#fsharp #dynamobim #python
#grevit
#responsivedesign #typepad
#ah8 #augi #au2015 #dotnet #dynamobim
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs

Revit API, Jeremy Tammik, akn_include

Filtering Samples and RAC 2016 Features #revitapi #bim #aec #3dwebcoder #adsk #adskdevnetwrk #au2015 #rtceur

The issue of simple filtered element collector samples was raised in a private message.
I do not like to receive private messages and avoid answering them in private.
I always prefer to discuss everything I do in public and enable the entire community to contribute and share when possible.
In this case, such a message led to the discussion below.
I'll also point to an overview of Revit Architecture 2016 Features...

-->

### Filtering Samples and RAC 2016 Features

The issue of [simple filtered element collector samples](#2) was raised in a private message.

I do not like to receive private messages and avoid answering them in private.

I always prefer to discuss everything I do in public and enable the entire community to contribute and share when possible.

In this case, such a message led to the discussion [below](#2).

I'll also point to an overview of [Revit Architecture 2016 Features](#3).


#### <a name="2"></a>Simple Filtered Element Collector Samples

**Question:**
I am trying to write C# code to use the Revit API.

The nice samples in the SDK are rather complex :-)

A simple thing I think would be a help for many is some simple samples, e.g.:

- How to make a filtered element collector with a type and an instance parameter for the different categories? Maybe how to write to them as well?
- How to make a filter for system families such as walls, floors, ceilings, to get one type and one instance parameter. This could be one single sample, if the method can be used for several categories.
- Host sweeps
- Ramps and stairs

<pre class="code">
&nbsp; <span class="green">// Floors, Walls, Ceilings, Roofs etc...</span>
&nbsp; <span class="teal">FilteredElementCollector</span> FMFloorCollector
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc );
&nbsp;
&nbsp; FMFloorCollector.OfClass( <span class="blue">typeof</span>( <span class="teal">Floor</span> ) );
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">Floor</span> FMFloor <span class="blue">in</span> FMFloorCollector )
&nbsp; {
&nbsp; &nbsp; <span class="green">// Type</span>
&nbsp; &nbsp; <span class="teal">Element</span> el = FMFloor <span class="blue">as</span> <span class="teal">Element</span>;
&nbsp; &nbsp; <span class="teal">ElementId</span> typeid = el.GetTypeId();
&nbsp; &nbsp; <span class="teal">Element</span> floortype = doc.GetElement( typeid );
&nbsp;
&nbsp; &nbsp; <span class="teal">Parameter</span> pKeynote = floortype.get_Parameter(
&nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.KEYNOTE_PARAM );
&nbsp;
&nbsp; &nbsp; <span class="green">// Instance</span>
&nbsp; &nbsp; <span class="teal">Parameter</span> pArea = FMFloor.get_Parameter(
&nbsp; &nbsp; &nbsp; <span class="teal">BuiltInParameter</span>.HOST_AREA_COMPUTED );
&nbsp; }
</pre>

What people want to do with the data forms and so on is more or less standard C#.

But simply how to set/get could be nice...

**Answer:**
Have you looked at [The Building Coder](https://github.com/jeremytammik/the_building_coder_samples) samples?

Especially,
the [CmdCollectorPerformance module](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCollectorPerformance.cs) provides
a large number of such samples and probably covers all of the examples you list.

Please let me know if anything is missing.

Or, better still, fork the repository, add them yourself, and let me know so I can merge your update back in again.

Thank you!



#### <a name="3"></a>Revit Architecture 2016 Features

Nick Bowley and Carl Storms published a nice overview and video recordings of several Revit Architecture 2016 Features:

- [Part 1](http://www.engineering.com/Education/EducationArticles/ArticleID/10636/Autodesk-Revit-Introduces-New-Features-in-2016.aspx) &ndash; 3.5 min

    - Right click view on a sheet, Open Sheet, Selection Box
    - Revisions
    - Energy Settings, Analysis menu, Use Conceptual Masses and Building Elements
    - Show Energy Model

- [Part 2](http://www.engineering.com/BIM/ArticleID/10696/The-Many-Features-of-Autodesk-Revit-2016.aspx) &ndash; 15 min

    - Revit Core Features (Multi-Discipline)
        - Allowing navigation during redraw
        - Remembering view states
        - Rotate Project North
        - Multiline text
    - Architecture
        - Placing rooms automatically
        - Floor elevations
        - New door content

- [Part 3](http://www.engineering.com/BIM/ArticleID/10832/More-of-Whats-New-in-Autodesk-Revit-2016.aspx) &ndash; 15 min

    - Search in combo box (type selector and ribbon)
    - Link positioning
    - Select host for tags
    - Model upgrade interface improvements
    - PDF hyperlinks
    - Resetting camera target in perspective view
    - Toggle perspective
    - And more...
