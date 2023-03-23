<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js"></script>
</head>

<!---

- Modify DirectShapeType
  https://forums.autodesk.com/t5/revit-api-forum/modify-directshapetype/m-p/11802821
  DirectShape versus AddExternallyTaggedGeometry

- avoid conflict with revit dlls
  Do I need to include RevitAPI.dll and RevitAPIUI.dll in my release package?
  https://forums.autodesk.com/t5/revit-api-forum/do-i-need-to-include-revitapi-dll-and-revitapiui-dll-in-my/m-p/11727761

- SSSVG Interactive SVG Reference
  https://fffuel.co/sssvg/
  SVGs are awesome! Thanks to math and geometry, SVG graphics give us a standardized way to create images and icons on the web to be displayed at any size without any loss in image quality.
  Here's an interactive reference to the most popular and/or interesting parts of the SVG spec.

- Coding Accessibility: Becky
  https://youtu.be/MmHqthzJER4

twitter:

 with the @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS https://autode.sk/linkintersector

&ndash; ...

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

### Modify DirectShapeType


####<a name="2"></a> DirectShapeType and AddExternallyTaggedGeometry

The [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on how to [modify `DirectShapeType`](https://forums.autodesk.com/t5/revit-api-forum/modify-directshapetype/m-p/11802821) led
to an onteresting explanation comparing the use of `DirectShape` with `AddExternallyTaggedGeometry`:

**Question:** Is it possible to delete a certain element from the existing `DirectShapeType` definition, and then add a new one?
Basically, I would like to know how to modify the elements inside the existing DirectShapeType definition.

**Answer:** What is your use case?
Because, another approach would just be to create a new DirectShapeType to replace the old one and update all existing references to it from the old to the new.
Here are our articles so far
on [various aspects of using direct shapes](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.50).

**Response:** We are making our doors in the project with DirectShapeType and then instantiating it.
It's much faster than creating a door family first.
Creating a new DirectShapeType in our case won't work, because then the name would have to be different.
We register each DirectShapeType definition name into client's database, and after changes are made &ndash; the name should remain the same.
I don't see the point of DirectShapeType instances, if there is no way to update their definition.

**Answer:** From the wording of
the [DirectShape.SetShape documentation](https://www.revitapidocs.com/2023/de35fe1d-a1e4-b003-ff87-66978f6a19f0.htm),
it sounds like it can be set at any future point:

> Builds the shape of this object from the supplied collection of GeometryObjects.
The objects are copied.
If the new shape is identical to the old one, the old shape will be kept.

So, if the new shape is not identical to the old one, I suspect the old one will not be kept...Confucius.

A two minute experiment would likely answer any remaining doubts.
I don't believe you can cut/join direct shapes as can be done with some other more intelligent Revit elements.
They are basically the thing you resort to when nothing else works or is practical, I doubt they are that popular with an end user (they also lack element specific parameters, i.e., only having category specific ones).
It is also possible to create an actual family with FreeFormElement items or direct shapes.

The development team adds: Reading through the thread, what I gather is, the user wants to change the geometry in an existing DirectShapeType.
Depending on what version of Revit API they're targeting, we have an older API that is widely used,
[DirectShapeType.SetShape](https://www.revitapidocs.com/2023/d8642243-fb35-0cbe-08d8-df4518929946.htm).
It overwrites the shape, just like when the DirectShapeType was originally created.
That is the oldest API that exists; an AppendShape was later added which doesn't overwrite, just appends.

Later, we added an externally tagged geometry based API where you can add geometry and give them unique names
using [`AddExternallyTaggedGeometry`](https://www.revitapidocs.com/2023/2c551429-8b90-ed46-3e29-a6b3dbc1cb95.htm).

There are two convenience methods to delete existing externally tagged geometry:

- `RemoveExternallyTaggedGeometry` that takes an `ExternalGeometryId`, and
- `ResetExternallyTaggedGeometry` which clears all `ExternallyTaggedGeometry`.

There's also a convenience method `UpdateExternallyTaggedGeometry`, which updates an existing `ExternallyTaggedGeometry`.

The advantage of `ExternallyTaggedGeometry` is, if you've got something conceptually the same but it has changed representation (e.g., a line has moved), and you had references (e.g., dimensions) to it, then updating the externally tagged geometry can preserve the dimensions, so long as it still makes geometric sense (swapping a line for an arc probably doesn't make sense).

So, calling `DirectShapeType.SetShape` will not remove any extant `ExternallyTaggedGeometry`.
They are separate universes, hence the `ResetExternallyTaggedGeometry`.

For this user, my guess is they used SetShape originally and should just need to setup a transaction and call SetShape again.

If the DirectShapeLibrary is in use, that could cause some issues, but I don't see mention of that.

As you can see from the speculation in the answer, the analysis is significantly simplified and more efficient the more specific details are provided in the original question.

By the way, just to understand the motivation behind the whole shebang: direct shapes were originally introduced to more efficiently support IFC import. Rather than creating a new family definition in a separate RFA and including instances of that for repetitive shapes, the direct shape enables repetitive shape definition in-place in the RVT BIM project file.

Some testing proves that yes, indeed, is possible to change the DirectShapeType and DirectShape using SetShape.

Here is a simple sample code snippet
to [change an existent DirectShapeType or DirectShape](https://gist.github.com/ricaun/5145ce37c3bbf0613930c8d167d4ab73).

I tested this in Revit 2021 using `DirectShapeLibrary` to create the type and works fine; in Revit 2023 it works fine as well.

**Response:** Thank you very much, all three of you!
@RPTHOMAS108 for suggesting the final solution, @jeremy.tammik for taking time to further provide more detailed possibilities, and @ricaun for testing.
I truly appreciate the help from all three of you.

@ricaun Thank you for the code.
It works like a charm on Revit 2022.1 as well!


####<a name="3"></a> Referencing and Avoiding Conflict with Revit DLLs

**Question:** [Do I need to include RevitAPI.dll and RevitAPIUI.dll in my release package?](https://forums.autodesk.com/t5/revit-api-forum/do-i-need-to-include-revitapi-dll-and-revitapiui-dll-in-my/m-p/11727761)

This is a fairly simple question that came to mind when I started researching Design Automation (APS/Forge).
One of the DA tutorials says that Revit API dlls are not required in app bundle and that is correct.
I managed to upload the app bundle with just my DLL + addin file and was able to run a successful work.

Before we switch completely to Forge, we still have our regular desktop addin.
As of now, we are including Revit API files into our release package (that is installed on client computers with Revit).
Seeing the Forge case, I started wondering, is this required for regular addins?


**Answer:** Nope, absolutely no need to include the Revit API assemblies with your desktop add-in distribution, and actually a big non-no to do so.
They are part of the Revit installation and live in the same folder as Revit.exe itself.
In your development environment, you should set the 'Copy Local' property on all Revit API assemblies to 'false' to ensure that they are not copied into your distribution package.
Your add-in should (and must) use the Revit API assemblies provided (and already loaded) by Revit:

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/06/export-family-instance-to-gbxml.html" target="_blank" rel="noopener">Export Family Instance to gbXML</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/08/set-copy-local-to-false.html" target="_blank" rel="noopener">Set Copy Local to False</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/06/copy-local-false-and-ifc-utils-for-wall-openings.html" target="_blank" rel="noopener">Copy Local False and IFC Utils for Wall Openings</a></li>
</ul>

So, it is best practice to not include the `RevitAPI.dll` or other dll file included in Revit with your release.

Most of the time Revit is gonna ignore the DLL that is already loaded.

Revit in Forge/APS works in the same way; it is basically the same engine without the UI.

In the end, is better to not include it, makes your package smaller size &ndash; RevitAPI.dll alone is around ±28kb.

**Response:** Thank you very much for the info.
This is exactly what I wanted to hear.
Awesome news.

Actually, package size is one of the reasons I asked this question, because we support multiple Revit versions, so deleting multiple revit api files is going to reduce size quite a lot &nbsp; :-)

One more question if I may:
What about other libraries like Newtonsoft.Json.dll?
I see that it also comes with Revit, so can I set "Copy Local" to false on it as well?
What happens when I use newer version of this library than the one provided with Revit?
Which one would be loaded?

**Answer:** I never copy the Newtonsoft.Json.dll and always use version 9.0.1, the same as Revit does.

If your application asks for a bigger version than Revit contains, it is probably gonna load two versions of the Newtonsoft.Json in the AppDomain and strange things could happen, like this:
[BIM 360 Links Not Found - Fix](https://forums.autodesk.com/t5/revit-api-forum/bim-360-links-not-found-fix/td-p/11463147).

**Response:** Does that mean that every time I am adding a library to my addin, I need to check if it is shipped with Revit first?
And if it is, just add the same version to my addin with "Copy Local" set to false?
However, if it is not shipped with Revit then I can freely add my version with "Copy Local" true.
Do I understand it correctly?

**Answer:** Basically yes; and, if you copy the reference to your addin folder, most of the time Revit gonna ignore it and use the version that is already loaded in the AppDomain, or load a version shipped with Revit.

Only one version can be loaded, because there is only one AppDomain for the Revit API.
Revit has already loaded its version, so your attempt to load a different one will fail.
So, you cannot use a newer version than the one used by Revit.
In some (rare?) cases, Revit includes an add-in or other piece of functionality that does not load all its dependencies up front; in that case, if you load your own ("wrong") version first, you might even end up breaking some of the built-in Revit functionality.

**Response:** Makes perfect sense.
Once again, appreciate the explanations.
Thanks.


####<a name="4"></a> SSSVG Interactive SVG Reference

A very nice example of how to illustrate programming functionality using full interaction, visually demonstrsating the effect of function input arguments:
the [SSSVG Interactive SVG Reference](https://fffuel.co/sssvg/).

> SVGs are awesome!
Thanks to math and geometry, SVG graphics give us a standardized way to create images and icons on the web to be displayed at any size without any loss in image quality.
Here's an interactive reference to the most popular and/or interesting parts of the SVG spec.

####<a name="5"></a> Coding With Eyes Only

I was very deeply touched by the ten-minute documentary
on [Coding Accessibility: Becky](https://youtu.be/MmHqthzJER4),
a young woman coding with her eyes:

> The first installment in GitHub’s “Coding Accessibility” video series features Becky Tyler, a bright, funny, and incredibly tenacious young woman with quadriplegic cerebral palsy who interacts with her computer exclusively by using her eyes.
Becky started off simply wanting to play Minecraft, but the shortcomings of available accessibility tech led her down a path beyond mining ore and into the world of open source software and collaboration.
She now attends the University of Dundee, where she studies applied computing.

<iframe width="480" height="270" src="https://www.youtube.com/embed/MmHqthzJER4" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>

I happened upon it reading the more exhaustive GitHub article
[from gaming with your eyes to coding with AI: new frontiers for accessibility](https://github.com/readme/featured/open-source-accessibility).




<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 433, Pixel Width: 1,006 -->
</center>

**Answer:**

**Response:**

<pre class="prettyprint">
</pre>
