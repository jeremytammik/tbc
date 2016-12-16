<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

[Forge](https://forge.autodesk.com) [Forge](http://autode.sk/2gRqSU1)
[developer.autodesk.com](https://developer.autodesk.com) [developer.autodesk.com](http://autode.sk/2gRmOmI)
[my apps](https://developer.autodesk.com/myapps) [my apps](http://autode.sk/2gRrf0Q)

- Like Nazis at a Ku Klux Klan rally, they were comfortable ideologically, but not in terms of corporate culture.
  Paul Beatty, The Sellout
  Book review of Paul Beatty’s ‘The Sellout’ By Kevin Young, April 9, 2015
  http://www.nytimes.com/2015/04/12/books/review/paul-beatty-sellout.html

- open source IFC library
  https://github.com/IfcOpenShell/IfcOpenShell
  http://ifcopenshell.org/
  http://academy.ifcopenshell.org/
  https://github.com/jmirtsch/GeometryGymIFC
  Jon Mirtschin @jmirtsch  

Forge Formats Python Script and IFC Open Shell #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge #PaulBeatty @buildingSMART
Forge Formats Python & IFC Open Shell #RevitAPI #DynamoBim @AutodeskForge #PaulBeatty @buildingSMART

I went back to answering way too many questions in the Revit API discussion forum yesterday.
However, two other interesting topics also cropped up when I finally let go of that pastime, and I just finished a good book
&ndash; The Sellout by Paul Beatty
&ndash; IFC Ids and Open Shell
&ndash; Forge Python scripts
&ndash; py_forge_formats.py
&ndash; forge_authenticate_app
&ndash; forge_formats
&ndash; jprettyprint
&ndash; Mainline...

-->

### Forge Formats Python Script and IFC Open Shell

I went back to answering way too many questions in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) yesterday.

However, two other interesting topics also cropped up when I finally let go of that pastime, and I just finished a good book:

- [The Sellout by Paul Beatty](#2)
- [IFC Ids and Open Shell](#3)
- [Forge formats Python script](#4)
    - [Setup and usage](#5)
    - [forge_authenticate_app](#6)
    - [forge_formats](#7)
    - [jprettyprint](#8)
    - [Mainline](#9)

####<a name="2"></a>The Sellout by Paul Beatty

I just finished
reading [The Sellout](https://en.wikipedia.org/wiki/The_Sellout_(book))
by [Paul Beatty](https://en.wikipedia.org/wiki/Paul_Beatty).

I struggled a bit initially before getting used to his style, then really deeply enjoyed this unique American satire on racism.

It won the [Man Booker Prize](https://en.wikipedia.org/wiki/Man_Booker_Prize), and Beatty is now the first writer from the United States ever to be thus honoured.

It hits hard to caringly and lovingly reach into and explore some pretty deep and subtly hidden everyday truths.

It is sometimes also just funny.

Here is a metaphor I liked describing two men who don't quite fit in: "Like Nazis at a Ku Klux Klan rally, they were comfortable ideologically, but not in terms of corporate culture."

:-)

<center>
<img src="img/PaulBeatty.jpg" alt="Paul Beatty" width="220"/>
</center>


####<a name="3"></a>IFC Ids and Open Shell

**Question:** Where can I ask questions about general IFC related stuff?

I would like to hack together something to display IFC models in the browser.

To do that, the server is converting uploaded IFC files first to Collada, then to a JSON format used by [three.js](https://threejs.org).

This conversion process is problematic if the IFC IDs are not unique, which sometimes they are not.

I could burp out an error at the user, but I'd rather try to fix it silently if possible.

I've thought about 'fixing' the IFC file before conversion by simply searching for duplicated IFC IDs and creating new (unique) IDs for those elements.

Since I'm only doing this conversion for 3D display purposes, do you think that could work, or is it fundamentally flawed?

I'm basing this on the assumption that all IFC elements seem to be referred to by line number, not ID, so the specific ID doesn't really matter for my purposes, as long as it's unique.

**Answer:** I am not an IFC expert by any means, but here is a recent Autodesk University class
an [IFC Technical Overview] presented by someone who is &nbsp; :-)

I was heavily involved a long, long time ago.

Yes, the IFC id just has to be unique within the context of the IFC file itself, afaik.

[Q]: Are there any situations where several different IFC files cross reference each other and refer to their respective internal ids?

I recently summarised all I know about
the [consistency of IFC GUID and `UniqueId`](http://thebuildingcoder.typepad.com/blog/2016/08/consistency-of-ifc-guid-and-uniqueid.html),
including links to earlier discussions.

If the answer to [Q] is no, then you can assign any id you like, of course.

Even if the answer is yes, but no such cross-referencing occurs in your situation, you should still be able to go ahead.

Also, if you are getting all your elements from Revit BIM, you can use the Revit element `UniqueId` just as it is. It is guaranteed to be unique, and thus enables cross referencing between different files and models... unless the files contain copies of element that occur elsewhere as well, e.g., you copied the BIM and the elements contained within it, of course. That obviously leads to duplication.

Regarding the three.js viewer:

- Are you aware of the [VA3C](https://va3c.github.io) three.js AEC viewer project?
- Are you aware of [Forge](http://autode.sk/2gRqSU1) and its [developer resources](http://autode.sk/2gRmOmI)?
- Are you aware of the [Revit IFC importer/exporter open source project](https://sourceforge.net/projects/ifcexporter)?

You might want to discuss your question with the Revit IFC open source community.

You might also want to
contact [Jon @jmirtsch Mirtschin](https://github.com/jmirtsch), author of
the [GeomGym IFC library](https://github.com/jmirtsch/GeometryGymIFC).

The [IfcOpenShell project](http://ifcopenshell.org) ([GitHub](https://github.com/IfcOpenShell/IfcOpenShell)) also looks very promising.

**Response:** I actually currently use IfcOpenShell to convert from IFC to DAE via `IfcConvert`.

After that I segment the DAE files so that each IFC storey/space/etc. gets its own file based on information in the IFC model, parsed using [Xbim](http://www.xbim.net).

That way, the clients can download only the storey/space they are interested in.
Then I convert each storey/space DAE file to JSON, which is smaller and has much less memory requirements due to the browser not needing to parse an XML document (which it has to if loading the DAE file).

**Answer:** Cool. That sounds like a very useful workflow. Might come in handy in other contexts as well.

People just recently asked how to compress large BIM models for Forge. Your approach might help there too.

Other suggestions include:

- Simplify the BIM model before translation. Reduce geometry complexity, remove overly detailed aspects.
- Split the BIM RVT into separate files, e.g., by level or discipline, before submitting them for Forge translation. You can use the Forge aggregation functionality to put them back together again at need.


####<a name="4"></a>Forge Formats Python Script

I just published the beginning of a
new [collection of Forge Python scripts](https://github.com/jeremytammik/forge_python_script),
currently with a count of one:

[py_forge_formats.py](https://github.com/jeremytammik/forge_python_script/blob/master/py_forge_formats.py)
implements a Python wrapper around two basic RESTful Forge web service calls:

- Authenticate an app &ndash; [forge_authenticate_app](#6)
- Query the file formats currently supported by the translation processes &ndash; [forge_formats](#7)

The result is prettified using the [jprettyprint](#8) helper function.

The [mainline](#9) ties it all together and presents the final result, which looks like this at the time of writing:

<pre class="prettyprint">
$ ./py_forge_formats.py
9 Forge output formats:
  dwg: f2d, f3d, rvt
  fbx: f3d
  ifc: rvt
  iges: f3d, fbx, iam, ipt, wire
  obj: asm, f3d, fbx, iam, ipt, neu, prt, sldasm, sldprt, step, stp, stpz,
    wire, x_b, x_t, asm.NNN, neu.NNN, prt.NNN
  step: f3d, fbx, iam, ipt, wire
  stl: f3d, fbx, iam, ipt, wire
  svf: 3dm, 3ds, asm, catpart, catproduct, cgr, collaboration, dae, dgn,
    dlv3, dwf, dwfx, dwg, dwt, dxf, exp, f3d, fbx, g, gbxml, iam, idw,
    ifc, ige, iges, igs, ipt, jt, max, model, neu, nwc, nwd, obj, pdf,
    prt, rcp, rvt, sab, sat, session, skp, sldasm, sldprt, smb, smt,
    ste, step, stl, stla, stlb, stp, stpz, wire, x_b, x_t, xas, xpr,
    zip, asm.NNN, neu.NNN, prt.NNN
  thumbnail: 3dm, 3ds, asm, catpart, catproduct, cgr, collaboration, dae, dgn,
    dlv3, dwf, dwfx, dwg, dwt, dxf, exp, f3d, fbx, g, gbxml, iam, idw,
    ifc, ige, iges, igs, ipt, jt, max, model, neu, nwc, nwd, obj, pdf,
    prt, rcp, rvt, sab, sat, session, skp, sldasm, sldprt, smb, smt,
    ste, step, stl, stla, stlb, stp, stpz, wire, x_b, x_t, xas, xpr,
    zip, asm.NNN, neu.NNN, prt.NNN
</pre>

This script replaces and improves on the
previous [forgeauth](https://github.com/jeremytammik/forge_python_script/blob/master/forgeauth)
and [forgeformats](https://github.com/jeremytammik/forge_python_script/blob/master/forgeformats) Unix
shell cURL scripts documented in the discussion of
the [`cURL` wrapper scripts to list Forge file formats](http://thebuildingcoder.typepad.com/blog/2016/10/forge-intro-formats-webinars-and-fusion-360-client-api.html#3).

For the sake of completeness, those two scripts have been added to this repository as well.

####<a name="5"></a>Setup and Usage

Two aspects need to be prepared: Forge and Python.

Before you can make any use of the Forge web services, you will need to register an app and request the API client id and client secret for it
at [developer.autodesk.com](http://autode.sk/2gRmOmI)
&gt; [my apps](http://autode.sk/2gRrf0Q).

These scripts assume that you have stored these credentials in the environment variables `FORGE_CLIENT_ID` and `FORGE_CLIENT_SECRET`.

Regarding the Python components:

- Install [Python](https://www.python.org).
- Install the [requests Python library](http://docs.python-requests.org).

Now you should be all set to run as shown above.

####<a name="6"></a>forge_authenticate_app

<script src="https://gist.github.com/jeremytammik/819084fdc8bc52965b7ce8f3d64cc18b.js"></script>

####<a name="7"></a>forge_formats

<script src="https://gist.github.com/jeremytammik/4e8df567c15f8fab1fa40e17962045b9.js"></script>

####<a name="8"></a>jprettyprint

<script src="https://gist.github.com/jeremytammik/d3c3b02b5fe2636436cc6acc7173bef2.js"></script>

####<a name="9"></a>Mainline

<script src="https://gist.github.com/jeremytammik/9a9caddec09a44ddceaab677abcc9887.js"></script>

<hr/>

# Cloud and Mobile

### Forge Formats Python Scripts

By [Jeremy Tammik](http://thebuildingcoder.typepad.com/blog/about-the-author.html).


