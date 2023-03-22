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


####<a name="2"></a>


**Question:**


Is it possible to delete certain element from the existing DirectShapeType definition, and the add a new one?
Basically, I would like to know how to modify the elements inside the existing DirectShapeType definition.

Any help is appreciated.

jeremy.tammik
2023-03-06 09:23 PM
I have not looked into any details and wonder what your use case is. Because, another approach would just be to create a new DirectShapeType to replace the old one and update all existing references to it from the old to the new. In that case, the answer to your question would not really matter that much.



Disregarding that aspect, I don't see why it should not be possible to modify an existing DirectShapeType. Why do you ask? Do you have reason to think otherwise?


george1985
2023-03-07 01:35 AM
Hi @jeremy.tammik
Thank you for the quick reply.
We are making our doors in the project with DirectShapeType and then instantiating it. It's much more faster than creating a door family first.
Creating a new DirectShapeType in our case won't work, because then the name would have to be different. We register each DirectShapeType definition name into client's database, and after changes are made - the name should remain the same.

If not providing the code, do you have a Revit API method suggestion, how to delete any element inside the DirectShapeType definition?

jeremy.tammik
2023-03-07 04:45 AM
Nope, never tried to do that yet. All I have is this:



https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.50



So, your hopefully successful experience solving this will be much appreciated! Good luck!


george1985
2023-03-09 02:40 AM
Thank you @jeremy.tammik
Do you mind if I ask: is there some Autodesk API support that I can contact?
An email, a ticket system?
I am pretty sure my company has some sort of "Autodesk support" included in their license program.

jeremy.tammik
2023-03-09 02:48 AM
Dear George,



Yes, Autodesk has at least two technical support teams: product support, for end user issues, and the DAS team, developer advocacy and support, formerly ADN, the Autodesk Developer Network:



https://www.autodesk.com/developer-network/overview
https://knowledge.autodesk.com/support/autocad/learn-explore/caas/sfdcarticles/sfdcarticles/Joining-...
https://developer.autodesk.com/


For Revit API questions, the ticketing system start right here in this forum.



Please also note that this is the preferred method to submit non-confidential DAS (Autodesk Developer Advocacy and Support, previously ADN (Autodesk Developer Network)) queries or requests on the Revit API. Any thread that you submit here using your email address registered as a DAS member will be recognised as such and automatically escalated to us in the DAS team in case no one else addresses it first. At the same time, you will address a larger audience, more of your peers will see it, be able to chip in and help, and more people will see and profit from the answers we provide. We therefore prioritise cases from the discussion forum. Please submit your non-confidential queries via the Revit API discussion forum.



Thank you!

george1985
2023-03-09 03:07 AM
Dear @jeremy.tammik ,
Thank you for the reply.
This topic is posted in the Revit API forum.
As pointed out by yourself, you don't know the answer. Thus I asked if I can get further help.

I didn't understand this part:

        "Please submit your non-confidential queries via the Revit API discussion forum."


This topic is not considered as non-confidential query in the Revit API discussion forum?


jeremy.tammik
2023-03-09 04:22 AM
Sure it is. Maybe the email address that you are using here is not linked to any officially recognised DAS or ADN membership.

I have asked the development team for you. Just 'is it possible to modify, yes/no'. If you want more than that, e.g., have tried it out yourself and it does not work as expected, you can provide a complete minimal reproducible case demonstrating exactly what you are doing, how it behaves, what behaviour you would expect, and how the two differ:



https://thebuildingcoder.typepad.com/blog/about-the-author.html#1b


ricaun
2023-03-09 05:57 AM
I never tried to modify an existent DirectShapeType, but what do you mean by modify?


Do you mean to modify the shape in the DirectShapeType and then make all children DirectShape updates? This would be coll but I don't know if is possible.

Luiz Henrique Cassettari


george1985
2023-03-09 01:12 PM
Thank you very much for the help @jeremy.tammik. Apologies if my reply sounded not appropriate.

Yes, @ricaun exactly. I don't see the point of DirectShapeType instances, if there is no way to update their definition.


RPTHOMAS108
2023-03-09 05:44 PM
From wording of DirectShape.SetShape it sounds like it can be set at any future point i.e.:



RevitAPI.chm DirectShape.SetShape

"Builds the shape of this object from the supplied collection of GeometryObjects. The objects are copied. If the new shape is identical to the old one, the old shape will be kept."



So if the 'new' shape is not identical to the 'old' one I suspect the 'old' one will not be kept...Confucius



A two minute experiment would likely answer any remaining doubts. I don't believe you can cut/join direct shapes as can be done with some other more intelligent Revit elements. They are basically the thing you resort to when nothing else works or is practical, I doubt they are that popular with an end user (they also lack element specific parameters i.e. only having category specific ones). It is also possible to create an actual family with FreeFormElement items or direct shapes.

jeremy.tammik
2023-03-10 06:06 AM
I heard back from the development team:



Reading through the thread, what I gather is, the user wants to change the geometry in an existing DirectShapeType. Depending on what version of Revit API they're targeting, we have an older API that is widely used which is Autodesk.Revit.DB.DirectShapeType.SetShape:



https://www.revitapidocs.com/2023/d8642243-fb35-0cbe-08d8-df4518929946.htm



It overwrites the shape, just like when the DirectShapeType was originally created.



That is the oldest API that exists; an AppendShape was later added which doesn't overwrite, but just appends.



Then we added an externally tagged geometry based API where you can add geometry and give them unique names. This is `AddExternallyTaggedGeometry`:



https://www.revitapidocs.com/2023/2c551429-8b90-ed46-3e29-a6b3dbc1cb95.htm



There are two convenience methods to delete existing externally tagged geometry:



`RemoveExternallyTaggedGeometry` that takes an `ExternalGeometryId`, and
`ResetExternallyTaggedGeometry` which clears all `ExternallyTaggedGeometry`.


There's also a convenience method `UpdateExternallyTaggedGeometry`, which updates an existing `ExternallyTaggedGeometry`.



The advantage of `ExternallyTaggedGeometry` is, if you've got something conceptually the same but it has changed representation (e.g., a line has moved), and you had references (e.g., dimensions) to it, then updating the externally tagged geometry can preserve the dimensions, so long as it still makes geometric sense (swapping a line for an arc probably doesn't make sense).



So, calling `DirectShapeType.SetShape` will not remove any extant `ExternallyTaggedGeometry`. They are separate universes, hence the ResetExternallyTaggedGeometry.



For this user, my guess is they used SetShape originally and should just need to setup a transaction and call SetShape again.



If the DirectShapeLibrary is in use, that could cause some issues, but I don't see mention of that.



As you can see from the speculation in the answer, the analysis is significantly simplified and more efficient the more specific details are provided in the original question.


By the way, just to understand the motivation behind the whole shebang: direct shapes were originally introduced to more efficiently support IFC import. Rather than creating a new family definition in a separate RFA and including instances of that for repetitive shapes, the direct shape enables repetitive shape definition in-place in the RVT BIM project file.


ricaun
2023-03-10 06:55 AM
I tested and yes is possible to change the DirectShapeType and DirectShape using SetShape.



Here is a simple sample to change an existent DirectShapeType or DirectShape: https://gist.github.com/ricaun/5145ce37c3bbf0613930c8d167d4ab73



Edited: Tested in Revit 2021 using DirectShapeLibrary to create the type and works fine, and in Revit 2023 works fine as well.





Luiz Henrique Cassettari

george1985
2023-03-11 02:11 PM
Thank you very much, all three of your
@RPTHOMAS108 for suggesting the final solution, @jeremy.tammik for taking your time to further give me more detailed possibilities. And @ricaun for testing. I truly appreciate the help from all three of you.

@ricaun Thank you for the code. Worked like a charm on Revit 2022.1 as well!



<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 433, Pixel Width: 1,006 -->
</center>



**Answer:**

**Response:**

<pre class="prettyprint">

</pre>



**Response:**

####<a name="3"></a> Avoiding Conflict with Revit DLLs

Do I need to include RevitAPI.dll and RevitAPIUI.dll in my release package?
https://forums.autodesk.com/t5/revit-api-forum/do-i-need-to-include-revitapi-dll-and-revitapiui-dll-in-my/m-p/11727761


adam.konca
2023-02-01 07:33 PM
Do I need to include RevitAPI.dll and RevitAPIUI.dll in my release package?
Hi,

This is a fairly simple question that came to mind when I started researching Design Automation (APS/Forge). One of the DA tutorials says that Revit API dlls are not required in app bundle and that is correct. I managed to upload the app bundle with just my dll + addin file and was able to run a successful work.

Before we switch completely to Forge, we still have our regular addin and as of now, we are including Revit API files into our release package (that is installed on client computers with Revit). Seeing the Forge case, I started wondering, is this required for regular addins?

Thanks a lot for you time.


moturi.magati.george
2023-02-01 10:26 PM
Hi @adam.konca,



Forge is a cloud-based service. This means that instead of installing the software on your machine, the same is done on Autodesk servers and you are able to utilize the resources online.



This said, you don't need to include to include RevitAPI.dll and RevitAPIUI.dll when building a forge application. If some services might need it, I might not be aware of the same.



You can read more here: https://aps.autodesk.com/blog/what-forge



You can also view examples and walkthroughs here: https://github.com/orgs/Autodesk-Forge/repositories?type=all







  Moturi George,     Developer Advocacy and Support,  ADN Open


jeremy.tammik
2023-02-02 12:29 AM
Yes, and to address the desktop part of your question (well, the only part, actually):



Nope, absolutely no need to include the Revit API assemblies with your desktop add-in distribution, and actually a big non-no to do so. They are part of the Revit installation and live in the same folder as Revit.exe itself. In your development environment, you should set the 'Copy Local' property on all Revit API assemblies to 'false' to ensure that they are not copied into your distribution package. Your add-in should (and must) use the Revit API assemblies provided (and already loaded) by Revit:



Export Family Instance to gbXML
Set Copy Local to False
Copy Local False and IFC Utils for Wall Openings

ricaun
2023-02-02 04:54 AM
It's a good practice to not include the RevitAPI.dll or other dll file included in Revit with your release.

Most of the time Revit gonna ignore the dll that is already loaded.

And the Revit in Forge/APS works in the same way, is basically the same engine without the UI.

In the end, is better to not include it, makes your package smaller size. (RevitAPI.dll around ±28kb)
Luiz Henrique Cassettari

adam.konca
2023-02-03 02:52 AM
@moturi.magati.georgeYes, I already knew I don't need them in Forge 🙂

@jeremy.tammikThank you very much for the info. This is exactly what I wanted to hear. Awesome news.

@ricaunActually, package size is one of the reasons I asked this question, because we support multiple Revit versions, so deleting multiple revit api files is going to reduce size quite a lot 🙂

adam.konca
2023-02-03 07:43 AM
@jeremy.tammikOne more question if I may. What about other libraries like Newtonsoft.Json.dll? I see that it also comes with Revit, so can I set "Copy Local" to false on it as well? What happens when I use newer version of this library than the one provided with Revit? Which one would be loaded?


ricaun
2023-02-03 08:03 AM
I never copy the Newtonsoft.Json.dll and always use version 9.0.1.



If your application ask for a bigger version than that Revit contain, probably gonna load two version of the Newtonsoft.Json in the AppDomain and stages things could happen.



Like this: https://forums.autodesk.com/t5/revit-api-forum/bim-360-links-not-found-fix/td-p/11463147


adam.konca
2023-02-03 09:13 AM
Does it mean that every time I am adding a library to my addin, I need to check if it is shipped with Revit first? And if it is, just add the same version to my addin with "Copy Local" set to false?
However, if it is not shipped with Revit then I can freely add my version with "Copy Local" true.
Do I understand it correctly?

ricaun

Basically yes, and if you copy the reference to your addin folder, most of the time Revit gonna ignore it and use the version that is already loaded in the AppDomain or load a version shipped with Revit.

Luiz Henrique Cassettari

ricaun.com - Revit API Developer


adam.konca
2023-02-03 02:52 PM
Good to know. Thanks!

jeremy.tammik
2023-02-06 03:35 AM
I totally agree with Luiz. Only one version can be loaded, because there is only one AppDomain for the Revit API. Revit has already loaded its version, so your attempt to load a different one will fail. So, you cannot use a newer version than the one used by Revit. In some (rare?) cases, Revit includes an add-in or other piece of functionality that doies not load all its dependencies up front; in that case, if you load your own ("wrong") version first, you might even end up breaking some of the built-in Revit functionality.

adam.konca
2023-02-06 09:24 AM
Makes perfect sense. Once again, appreciate the explanations. Thanks.


####<a name="4"></a> SSSVG Interactive SVG Reference

A very nice example of how to illustrate programming functionality using full interaction, visually demonstrsating the effect of function input arguments:
the [SSSVG Interactive SVG Reference](https://fffuel.co/sssvg/).

> SVGs are awesome!
Thanks to math and geometry, SVG graphics give us a standardized way to create images and icons on the web to be displayed at any size without any loss in image quality.
Here's an interactive reference to the most popular and/or interesting parts of the SVG spec.

####<a name="5"></a> Coding Using Your Eyes

One of the most touching YouTube videos I have ever seen, a ten-minute documentary
on [Coding Accessibility: Becky](https://youtu.be/MmHqthzJER4),
a young woman coding with her eyes:

> The first installment in GitHub’s “Coding Accessibility” video series features Becky Tyler, a bright, funny, and incredibly tenacious young woman with quadriplegic cerebral palsy who interacts with her computer exclusively by using her eyes.
Becky started off simply wanting to play Minecraft, but the shortcomings of available accessibility tech led her down a path beyond mining ore and into the world of open source software and collaboration.
She now attends the University of Dundee, where she studies applied computing.

<iframe width="480" height="270" src="https://www.youtube.com/embed/MmHqthzJER4" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>

I happened upon it reading the more exhaustive GitHub article
[from gaming with your eyes to coding with AI: new frontiers for accessibility](https://github.com/readme/featured/open-source-accessibility).

