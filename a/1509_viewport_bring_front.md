<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

http://thebuildingcoder.typepad.com/blog/2014/04/determining-the-size-and-location-of-viewports-on-a-sheet.html#comment-3045289101

- implement a C++ Revit add-in and wizard
  http://forums.autodesk.com/t5/revit-api-forum/looking-for-c-samples-for-revit-2017-addin/m-p/6744704

Viewport Bring to Front and C++ Revit Add-In #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Two topics today, on reordering viewports to determine their respective occlusion, and setting up a C++ Revit add-in project
&ndash; Bringing a viewport to the front
&ndash; Change the draw order of a viewport (bring to front) for cases when you need to use simplified duplicate of the same view for background, e.g., ceiling grid for example
&ndash; Code clean-up
&ndash; Setting up a Visual Studio C++ Revit add-in project...

-->

### Viewport Bring to Front and C++ Revit Add-In

Two topics today, on reordering viewports to determine their respective occlusion, and setting up a C++ Revit add-in project:

- [Bringing a viewport to the front](#2)
    - [Code clean-up](#3)
- [Setting up a Visual Studio C++ Revit add-in project](#4)



####<a name="2"></a>Bringing a Viewport to the Front

Joshua Lumley submitted
a [comment](http://thebuildingcoder.typepad.com/blog/2014/04/determining-the-size-and-location-of-viewports-on-a-sheet.html#comment-3045289101) on
the discussion on [Determining the Size and Location of Viewports on a Sheet](http://thebuildingcoder.typepad.com/blog/2014/04/determining-the-size-and-location-of-viewports-on-a-sheet.html),
suggesting:

> I made code that will change the draw order of a viewport (bring to front) for cases when you need to use simplified duplicate of the same view for background (ceiling grid for example). Are you interested in it? I was hoping you could make a post and share it with the world (it has impressed my colleagues anyway).

> Here is the essence of the code:

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;ViewElementID&nbsp;=&nbsp;ViewportList1[listBox1.SelectedIndex].ViewId;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;storetheposition&nbsp;=&nbsp;ViewportList1[listBox1.SelectedIndex].GetBoxCenter();
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;storetypetypeID&nbsp;=&nbsp;ViewportList1[listBox1.SelectedIndex].GetTypeId();
 
&nbsp;&nbsp;Autodesk.Revit.DB.<span style="color:#2b91af;">View</span>&nbsp;pView&nbsp;=&nbsp;doc.ActiveView;&nbsp;Autodesk.Revit.DB.<span style="color:#2b91af;">Transaction</span>
&nbsp;&nbsp;&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Autodesk.Revit.DB.<span style="color:#2b91af;">Transaction</span>(&nbsp;doc,&nbsp;<span style="color:#a31515;">&quot;Form_2&quot;</span>&nbsp;);
&nbsp;&nbsp;t.Start();
&nbsp;&nbsp;ViewSheet1.DeleteViewport(&nbsp;ViewportList1[listBox1.SelectedIndex]&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">Viewport</span>&nbsp;vvp&nbsp;=&nbsp;<span style="color:#2b91af;">Viewport</span>.Create(&nbsp;doc,&nbsp;ViewSheet1.Id,&nbsp;ViewElementID,&nbsp;storetheposition&nbsp;);
&nbsp;&nbsp;vvp.ChangeTypeId(&nbsp;storetypetypeID&nbsp;);
&nbsp;&nbsp;t.Commit();</pre>
</pre>

As you can see, it is simply taking out a viewport and putting it back in the same place, because Revit paints viewports in the order they were created, so the most recently created viewport will come to the front.

This is the first view which has been placed on a sheet:

<center>
<img src="img/viewport_bring_front_1_initial_view.jpg" alt="The intial view" width="368"/>
</center>

Now we need to pretty it up with patterns, e.g., the ceiling grid and cable ladder fill taken from a 'medium' level detail view:

<center>
<img src="img/viewport_bring_front_2_second_third_view.jpg" alt="Placing a second and third view for ceiling grid and cable ladder background" width="369"/>
</center>

As you can see, these new views have been placed over the main view and now block it.

The code automates removing a viewport and making an identical one in the same place with a new element ID. 

You can see here the supplementary ceiling grid and ladder background (filtered to appear light red) adds value, but does not interfere with main view because it is now 'behind':

<center>
<img src="img/viewport_bring_front_3_reordering.jpg" alt="Reordering the viewports and done" width="375"/>
</center>

Btw I changed the furniture to halftone manually; this is not an effect of the layering.

Obviously, the perfect draftsman would be placing views in the correct sequence to begin with, but because the stylistic 'touch ups' are the last stages of making a drawing and you can never know in advance what you are going to need.

In my opinion making a drawing nice to look at is just as important as the content itself.


####<a name="3"></a>Code Clean-Up

I cleaned up the initial code suggestion like this:

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Bring&nbsp;viewport&nbsp;to&nbsp;front&nbsp;by&nbsp;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;deleting&nbsp;and&nbsp;recreating&nbsp;it.</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">void</span>&nbsp;ViewportBringToFront(&nbsp;<span style="color:#2b91af;">ViewSheet</span>&nbsp;sheet,&nbsp;<span style="color:#2b91af;">Viewport</span>&nbsp;viewport&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;sheet.Document;
 
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;viewId&nbsp;=&nbsp;viewport.ViewId;
&nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;boxCenter&nbsp;=&nbsp;viewport.GetBoxCenter();
 
&nbsp;&nbsp;<span style="color:green;">//ElementId&nbsp;typeId&nbsp;=&nbsp;viewport.GetTypeId();</span>
&nbsp;&nbsp;<span style="color:green;">//View&nbsp;view&nbsp;=&nbsp;doc.ActiveView;</span>
 
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;t&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;t.Start(&nbsp;<span style="color:#a31515;">&quot;Delete&nbsp;and&nbsp;Recreate&nbsp;Viewport&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;sheet.DeleteViewport(&nbsp;viewport&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Viewport</span>&nbsp;vvp&nbsp;=&nbsp;<span style="color:#2b91af;">Viewport</span>.Create(&nbsp;doc,&nbsp;sheet.Id,&nbsp;viewId,&nbsp;boxCenter&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;vvp.ChangeTypeId(&nbsp;typeId&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;t.Commit();
&nbsp;&nbsp;}
}
</pre>

Notes:

- Separate code from user interface implementation details. Isolate real functionality in separate methods. [Keep it simple](https://en.wikipedia.org/wiki/KISS_principle)!
- Encapsulate `Transaction` use in a separate `using` statement. Look at the explanations in The Building Coder topic group on [handling transactions and transaction groups](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.53).
- Simplified variable names for better readability and clarity.
- Commented out unused variables.

I added this method to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) in the
[module CmdViewsShowingElements.cs line 424-449](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdViewsShowingElements.cs#L424-L449).

Very many thanks to Joshua for sharing this and putting in all the work to document it, edit the draft markdown file and create
a [pull request](https://github.com/jeremytammik/tbc/pull/1) for it in
the [tbc GitHub repo](https://github.com/jeremytammik/tbc)!


####<a name="4"></a>Setting up a Visual Studio C++ Revit Add-In Project

I do not use C++ myself in everyday work, although I have a lot of experience with it from pre-C# times, working with ObjectARX and earlier systems.

Since the Revit API is completely .NET based, you can use any .NET capable language to access it, including managed C++.

Epogue asked how to set up a Visual Studio project to create a C++ Revit add-in in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [looking for C++ Samples for Revit 2017 add-in](http://forums.autodesk.com/t5/revit-api-forum/looking-for-c-samples-for-revit-2017-addin/m-p/6744704):

**Question:** As I look through the Revit 2017 documentation it looks like C++ has become nearly a first-class citizen.

I am mostly familiar with starting Revit add-in projects using
the [add-in wizard for C#](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20).

Is there something comparable for C++?

And/or is there sample code or a tutorial on implementing a Revit 2017 add-in using C++ instead of C# or VB? 

**Answer:** C++ has always been a first-class citizen in the .NET world, just like any other language supporting .NET, which was added to C++ with the introduction
of [managed C++](https://en.wikipedia.org/wiki/Managed_Extensions_for_C%2B%2B) over
a decade ago.
 
The Visual Studio C# and VB .NET wizards are very easy to create from scratch, and so is a C++ Revit add-in and a wizard for it.
 
- Use the standard Visual Studio class wizard to create a class.
- Add references to the Revit API assemblies.
- Implement an external command or external application.
- Done.
 
The files that you create manually through these steps can be packaged into a ZIP file and reused to create a Revit C++ add-in wizard.
 
Look at the descriptions
of [creating, modifying and updating the C# and VB wizards](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20) to
see how the same can be achieved for C++.

Here is an old description of a C++ Revit add-in and some other discussions on related topics:
 
- [C++ Revit Add-In](http://thebuildingcoder.typepad.com/blog/2010/10/c-revit-add-in.html)
- [Mixed mode C++ in Revit 2012](http://thebuildingcoder.typepad.com/blog/2012/03/mixed-mode-c-in-revit-2012.html)
- [Using the built-in Revit AcGe functionality](http://thebuildingcoder.typepad.com/blog/2013/10/using-the-built-in-revit-acge-functionality.html)
 
**Response:** Thank you! I like the Visual C++ Revit add-in step-by-step article. Regrettably the Visual Studio 2015 "New Project" function no longer includes "Class Library" as one of its options:

<center>
<img src="img/vs_cpp_new_project.png" alt="Visual Studio C++ new project wizards" width="400"/>
</center>

Any thoughts on the alternative "New Project" template to utilize to initially create C++ based Revit Add-In in Visual Studio 15 for Revit 2017?

Or a recommendation on which version of the .NET Framework to select?

**Answer:** Use the `CLR` option listed above: 

- Visual Studio &gt; File &gt; New &gt; Project &gt; Templates &gt; Other Languages &gt; Visual C++ &gt; CLR &gt; Class Library.
- Add references to the Revit API assemblies.
- Implement your external application or command.
 
The Revit 2017 API requires version the .NET framework version 4.5.2.

I wish you lots of fun and success making use of C++ with the Revit API!
