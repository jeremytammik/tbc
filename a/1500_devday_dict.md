<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Caroline Ward  FW: Autodesk® Developer News - special DevDays edition 

1500 Posts DevDay and Storing a Dictionary #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge #DevLab @TT_CORE

Welcome to The Building Coder post number 1500! I returned safe and sound to Europe from Autodesk University 2016 in Las Vegas and recuperated from jetlag over the weekend. Today I discuss upcoming events, new simple samples to embed a Forge viewer in a web page or blog post, and strategies to embed a dictionary into the Revit database
&ndash; DevDay Europe in Munich
&ndash; AEC Symposium in New York
&ndash; Embedding a Forge viewer
&ndash; Storing a dictionary in the Revit database...

-->

### 1500 Posts, DevDay and Storing a Dictionary

<h2>
<script language="JavaScript1.2">
/* Neon Lights Text By JavaScript Kit (http://javascriptkit.com) */

var message="Welcome to The Building Coder post number 1500!"
var neonbasecolor="gray"
var neontextcolor="orange"
var flashspeed=100 // milliseconds

///No need to edit below this line/////

var n=0
if (document.all||document.getElementById){
document.write('')
for (m=0;m<message.length;m++)
document.write('<span id="neonlight'+m+'">'+message.charAt(m)+'</span>')
document.write('')
}
else
document.write(message)
function crossref(number){
var crossobj=document.all? eval("document.all.neonlight"+number) : document.getElementById("neonlight"+number)
return crossobj
}

function neon(){
  //Change all letters to base color
  if (n==0){
  for (m=0;m<message.length;m++)
  //eval("document.all.neonlight"+m).style.color=neonbasecolor
  crossref(m).style.color=neonbasecolor
}

//cycle through and change individual letters to neon color
crossref(n).style.color=neontextcolor

if (n<message.length-1)
n++
else{
n=0
clearInterval(flashing)
setTimeout("beginneon()",1500)
return
}
}

function beginneon(){
  if (document.all||document.getElementById)
  flashing=setInterval("neon()",flashspeed)
}

beginneon()
</script>
</h2>

I returned safe and sound to Europe from Autodesk University 2016 in Las Vegas and recuperated from jetlag over the weekend.

Today I discuss upcoming events, new simple samples to embed a Forge viewer in a web page or blog post, and strategies to embed a dictionary into the Revit database:

- [DevDay Europe in Munich](#2)
- [AEC Symposium in New York](#3)
- [Embedding a Forge viewer](#4)
- [Storing a dictionary &ndash; use `DataStorage`, not `ProjectInfo`](#5)


####<a name="2"></a>DevDay Europe in Munich

Autodesk University ended, and all went well,
including [the classes I presented](http://thebuildingcoder.typepad.com/blog/2016/10/au-revit-20171-and-rex-freezedrawing.html#2) and
the [DevDay conference](http://thebuildingcoder.typepad.com/blog/2016/11/devday-conference-at-autodesk-university.html) held
the day before AU proper started:

- [AEC breakout](http://thebuildingcoder.typepad.com/blog/2016/11/devday-conference-at-autodesk-university.html#3)
    - [Revit API news, roadmap and idea station](http://thebuildingcoder.typepad.com/blog/2016/11/devday-conference-at-autodesk-university.html#4)
    - [BIM 360](http://thebuildingcoder.typepad.com/blog/2016/11/devday-conference-at-autodesk-university.html#5)
    - [InfraWorks 360 and Civil 3D](http://thebuildingcoder.typepad.com/blog/2016/11/devday-conference-at-autodesk-university.html#6)

The next DevDay conference is coming up fast, the one and only European one this year, followed by four days of joint hacking, research and learning, in Munich, Germany:

- DevDay conference &ndash; December 5, 2016
- DevLab and Forge Accelerator &ndash; December 6-9, 2016

Read all about the annual, worldwide Developer Day Conferences and Accelerator workshops
at [autodeskdevdays.com](http://autodeskdevdays.com).

Registration is easy and open for all.

Simply visit the [event website](http://autodeskdevdays.com) and click on
the [Register button](http://autodeskdevdays.com/register).

That is the next thing for me to prepare for now.


####<a name="3"></a>AEC Symposium in New York

If you are unable to attend the European DevDay in Munich, you may be interested in visiting New York instead.

[CORE Studio at Thornton Tomasetti](http://core.thorntontomasetti.com) is hosting
the [AEC Technology Symposium and Hackathon 2016](http://core.thorntontomasetti.com/event/aec-technology-symposium-and-hackathon-2016) there
on Thursday December 8.

Workshops Include:

- **Dynamo: Mandrill** &ndash; Konrad Sobon and Leland Jobson of Grimshaw hosting a workshop to learn how to use Dynamo to tap into Revit and extract data (information about families, warnings etc.) from it and a data visualization plug-in called Mandrill to generate charts and graphs, advanced list management techniques, formatting data to create  visualizations, tell stories and combine them into a report page that can be printed and linked into your software (InDesign, Illustrator, Word etc.)
- **An introduction to Computational Fluid Dynamics with Butterfly** &ndash; Mostapha Sadeghipour Roudsari presents a workshop introducing Butterfly, the newest addition to the insect community of Ladybug + Honeybee. Butterfly enables setting up and running advanced Computational Fluid Dynamics (CFD) simulations inside Grasshopper using the validated open source engine OpenFOAM. Grasshopper is used for hands-on exercises and similar examples and workflows are shown with DynamoBIM.
- **Dynamo Dev Zero-touch to NodeModel** &ndash; Robert Cervellione presents a workshop focused on setting up the development environment to create nodes in Dynamo using C# in Visual Studio. Start simple with zero-touch nodes,  introduce the Dynamo API and how to access geometry under the hood, dive into package creation for local hosting and package manager publishing, and dive deeper into WPF and custom node UI creation to access the full power of .NET and create fully custom nodes.

Please refer to
the [AEC Technology Symposium and Hackathon 2016](http://core.thorntontomasetti.com/event/aec-technology-symposium-and-hackathon-2016) for
all further details.

<center>
<img src="img/2016-12_core_tt_aec_symposium.jpg" alt="AEC Symposium" width="436">
</center>


####<a name="4"></a>Embedding a Forge Viewer

I repeatedly mentioned
the recent [BIM and Forge workshop at TuDa](http://thebuildingcoder.typepad.com/blog/2016/11/bimtuda-devdays-forge-news-and-more-events.html).

Our hosts at the uni there implemented a web page to share the agenda, [www.bim.tu-darmstadt.de](http://www.bim.tu-darmstadt.de).

After the single day workshop on Friday, they continued exploring Forge over the weekend and updated the web site to display the university building in an embedded Forge viewer over the weekend.

You can check it out yourself and view source to see the single line of code to add the `iframe` tag hosting the embedded A360 viewer.

Another take on this is provided by the
TrueVis [A360 embedder WordPress plugin](http://truevis.com/a360-embedder-wordpress-plugin) and 
accompanying [A360 embedder examples](http://truevis.com/a360-embedder-examples).

Using those, you can completely automate the viewer embedding in a WordPress blog post.

And now, back to the Revit API.


####<a name="5"></a>Storing a Dictionary &ndash; Use `DataStorage`, not `ProjectInfo`

I had an interesting discussion with my colleagues Simon Jones, Miroslav Schonauer and Scott Conover on storing a dictionary in the Revit database &ndash; note that the AutoCAD ObjectARX environment provides support for so-called named dictionaries:

**Question:** Is there a way of defining named dictionary storage for data in a Revit project?
 
The only option I can find is to use Extensible Storage on an arbitrary element placed in the model &ndash; but is there a way that avoids the requirement for an inserted element (and the risk that someone deletes it)?

**Answer 1:** Yes.
 
Officially, extensible storage is the only way... if you place it onto the `ProjectInformation` instance, that is equivalent of per-doc data as `ProjectInformation` is <b><i>GUARANTEED</i></b> to be a singleton in RVT.
 
I personally use a mechanism that I devised a few releases before extensible storage had been introduced &ndash; serialising a class into binary stream, encoding it via `String64` and then storing into an invisible string parameter. I found it much quicker to design my custom data-class than fiddling with extensible storage definitions…
 
But in either case, the `ProjectInfo` element is the way to get per-doc data…

**Answer 2:** <b><i>No!</i></b>

Sorry, but please stop using the `ProjectInfo` class in that manner immediately.
 
I used to recommend that earlier as well, and have stopped since learning better last year.
 
This is extremely important due to issues with worksharing.
 
Scott Conover explains that very explicitly in his AU class on [add-ins that cooperate with worksharing](http://thebuildingcoder.typepad.com/blog/2014/10/worksharing-and-duplicating-element-geometry.html#2).
 
At the same time as the introduction of extensible storage, the `DataStorage` element was introduced.
 
That is a new Revit database element class whose sole purpose is to host extensible storage data.
 
You can create as many of these as you like, and tag them in any way you like in order to make your one and only or several of them with all the associated element-specific or projectwide data easily accessible.
 
The Building Coder defines a dedicated topic group on [extensible storage](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23).

One of its entries actually points to a discussion quite similar to what you are asking for,
[named GUID storage for project identification](http://thebuildingcoder.typepad.com/blog/2016/04/named-guid-storage-for-project-identification.html).
All you would need to do to adapt it to your requirements would be to replace the GUID that is stores by a suitable dictionary.

Here is some more on
the [DataStorage element hosting extensible storage in a worksharing environment](http://thebuildingcoder.typepad.com/blog/2015/02/extensible-storage-in-a-worksharing-environment.html).

**Answer 3:** If worksharing is involved, I agree &nbsp; :-) &nbsp; most of the tools we are involved with would have many serious issues with worksharing, i.e., they are by-design used only for a central-model style workflow.

In any case, if starting from scratch, I agree that `DataStorage` should be used!

**Answer 4:** If you are certain that Worksharing will not be involved, there’s not much difference between using `ProjectInfo` and `DataStorage`, except that using `DataStorage` provides better encapsulation from another add-in accidentally deleting your data from `ProjectInfo` when it accesses its own.
 
If Worksharing is to be involved, you should definitely avoid using the `ProjectInfo` instance.

Please refer to [The Building Coder topic group on extensible storage](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23) for more information on it in all its aspects.

