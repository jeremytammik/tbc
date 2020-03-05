<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

- rex sdk
  /a/doc/revit/tbc/git/a/zip/
  A REX problem appeared in Revit 2019 and was resolved by sharing a custom version of the REX SDK.
  Now the same problem occurs again with Revit 2020:
  https://forums.autodesk.com/t5/revit-api-forum/issue-with-sdk-and-visual-studio/td-p/8052988
  Resolution for Revit 2019:
  https://thebuildingcoder.typepad.com/blog/2018/06/rex-sdk-visual-studio-templates-for-revit-structure-2019.html#2
  REX SDK2020.ZIP
  Structural Analysis SDK2020.zip

- https://thebuildingcoder.typepad.com/blog/2020/02/external-communication-and-async-await-event-wrapper.html#comment-4813648335

Kennan Chen wrote:

What a coincidence.
I also implemented my own async/await external event wrapper these days.
After some comparison with the one WhiteSharq provided, I found my implementation contains more useful functionality including wrapping async delegates and exposing core ability to define enhanced external events.
It internally adopts an `ExternalEvent` creator, so the developers won't accidentally experience the context problems.
Hope it can be helpful to the community.
Github:

https://github.com/KennanChan/Revit.Async

Also available via nuget.

/a/src/rvt/Revit.Async/

- headless revit
https://forums.autodesk.com/t5/revit-api-forum/family-related-memory-leaks/m-p/8738515

Kennan.Chen in reply to: Explorergeorge.aggreySNQ7D
‎2020-03-02 02:16 PM 
Re: Family related memory leaks 
A possible alternative is to run Revit in headless mode. Totally contrary to the documented approach to create an add-in, you can start an application which hosts a Revit runtime within the same process which enables you to do what you want with the top-level Application object. Just like Navisworks.

In your case, for each project, start a headless Revit to finish your process, then close the application.

The problem is, for each project, there is a waste of boot time.

Since headless Revit don't start the renderer and anything about the UI, less memory will be consumed to make it possible to run serveral tasks in parallel on your machine. Moreover you can even set up a cluster to handle tons of projects in parallel which I believe is the key to enable Forge to resolve Revit files in cloud.

As to how to set up a headless Revit, find a file named lcldrevit.dll or lcrvtutil.dll(newer version of Navisworks) under {Navisworks root folder}\Loaders\Rx folder. By decompiling that file,  LcRevitLoad.DoInit() contains all you need to start your own headless Revit.

To make things easy enough, I created a library called Revit.Headless to do all that loading logic for you. Visit https://github.com/KennanChan/Revit.Headless for more details. Also it's available via nuget.

https://github.com/KennanChan/Revit.Headless

twitter:

 the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="100"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Another Async Await, REX and Structural Analysis SDK

#### <a name="2"></a> REX SDK and Structural Analysis SDK 2020

Last year,
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on an [issue with SDK and Visual Studio](https://forums.autodesk.com/t5/revit-api-forum/issue-with-sdk-and-visual-studio/td-p/8052988)
poinbted out that the Visual Studio templates provided with the REX SDK to make use of the Revit Structure Extensions had not yet been updated for Revit 2019.

That was fixed by the development team and
an [updated set of REX SDK Visual Studio templates for Revit Structure 2019](https://thebuildingcoder.typepad.com/blog/2018/06/rex-sdk-visual-studio-templates-for-revit-structure-2019.html#2) was
shared in the forum and here on the blog.

The same situation arose again with Revit 2020, and here are the updated templates, samples and documentation for Revit 2020:

- [REX SDK2020.ZIP](zip/REX SDK2020.ZIP)
- [Structural Analysis SDK2020.zip](Structural Analysis SDK2020.zip)

**Response:** The REX SDK installed and is now working!

I read all provided PDF documentation and did the Hello World exercise.
I have some basic questions on how to get started with my specific projects and will create a separate post for that when I'm ready. 

I haven't looked at the Structural Analysis SDK as we are going to link Revit with our own custom proprietary software.
After reading about element mapping between databases and the content generator, I'm relieved to know that the REX SDK will likely reduce a lot of development time since those functionalities already exist. 

Thank you for providing the updates! I'm glad to have begun my journey down this path.


#### <a name="3"></a> Revit.Async

Last month, we briefly looked at a
simple [Async and Await External Event Wrapper](https://thebuildingcoder.typepad.com/blog/2020/02/external-communication-and-async-await-event-wrapper.html#2).

Kennan Chen kindly reacted to that post in a [comment](https://thebuildingcoder.typepad.com/blog/2020/02/external-communication-and-async-await-event-wrapper.html#comment-4813648335),
saying:

> What a coincidence.
I also implemented my own async/await external event wrapper these days.
After some comparison with the one WhiteSharq provided, I found my implementation contains more useful functionality including wrapping async delegates and exposing core ability to define enhanced external events.
It internally adopts an `ExternalEvent` creator, so the developers won't accidentally experience the context problems.
Hope it can be helpful to the community.
[Revit.Async Github repositroy](https://github.com/KennanChan/Revit.Async).
Also available via nuget.

Kennan's implementation does indeed look very complete and impressive with extensive documentation, so I'll quote some of that here:




- headless revit
https://forums.autodesk.com/t5/revit-api-forum/family-related-memory-leaks/m-p/8738515

Kennan.Chen in reply to: Explorergeorge.aggreySNQ7D
‎2020-03-02 02:16 PM 
Re: Family related memory leaks 
A possible alternative is to run Revit in headless mode. Totally contrary to the documented approach to create an add-in, you can start an application which hosts a Revit runtime within the same process which enables you to do what you want with the top-level Application object. Just like Navisworks.

In your case, for each project, start a headless Revit to finish your process, then close the application.

The problem is, for each project, there is a waste of boot time.

Since headless Revit don't start the renderer and anything about the UI, less memory will be consumed to make it possible to run serveral tasks in parallel on your machine. Moreover you can even set up a cluster to handle tons of projects in parallel which I believe is the key to enable Forge to resolve Revit files in cloud.

As to how to set up a headless Revit, find a file named lcldrevit.dll or lcrvtutil.dll(newer version of Navisworks) under {Navisworks root folder}\Loaders\Rx folder. By decompiling that file,  LcRevitLoad.DoInit() contains all you need to start your own headless Revit.

To make things easy enough, I created a library called Revit.Headless to do all that loading logic for you. Visit https://github.com/KennanChan/Revit.Headless for more details. Also it's available via nuget.

https://github.com/KennanChan/Revit.Headless


<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 1504 -->
</center>

Many thanks to  ... for ...

#### <a name="3"></a> 

#### <a name="4"></a> 
