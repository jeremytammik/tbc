<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- https://forums.autodesk.com/t5/revit-api-forum/mesh-data-export-to-json/m-p/8459164

- https://forums.autodesk.com/t5/revit-api-forum/difference-between-familysymbol-and-elementtype/m-p/8461805

- 14880808 [Dynamo 2.0.X vs box shipped 1.3.3 - Which to use for production environment?]
  https://forums.autodesk.com/t5/revit-api-forum/dynamo-2-0-x-vs-box-shipped-1-3-3-which-to-use-for-production/m-p/8457964

- Revit API vs. Dynamo for Revit
  Paolo Serra <paolo.serra@autodesk.com>

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash; 
...

-->

### Dynamo, Type vs. Symbol and Exporter Exception

Let's highlight a couple of Dynamo considerations, Revit family and element fundamental concepts, and an exception handler required for a custom exporter:

#### <a name="2"></a> Revit API versus Dynamo for Revit

sharpdevelop_cs_vb_rb_py.png 579 px


#### <a name="3"></a> Dynamo 2.0.X versus 1.3.3

Talking about Dynamo, here is a question on the different versions floating around, and which of them to use, from 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Dynamo 2.0.X vs box shipped 1.3.3 &ndash; Which to use for production environment?](https://forums.autodesk.com/t5/revit-api-forum/dynamo-2-0-x-vs-box-shipped-1-3-3-which-to-use-for-production/m-p/8457964):

**Question:** Been searching online for some answers and thought maybe the best would be to post to this forumn as I don't have a login for dynamo's git hub to post there.  We have some conflicting opinions in our department and I am looking at getting some "good" "professional" concrete advice.

One of the guys here has been putting together some dynamo scripts using 2.0 and now upgraded to 2.0.1.  We are running Revit 2019.1 for the most part.  Autodesk however packages 1.3.3 (?) with their 2019 software releases.

For scripts we release to production/end users to use, would you recommend we stick with 1.3.3 or develop in 2.0.X.  From what I understand 2.0 is a pretty big overhaul of dynamo and it's node packages.  So I am aware of the upgrading and compatibility issues with trying to write scripts in 1.3.3 vs 2.0.  Pretty much have to re-write them.  However with the stability of 2.0 up in the air there is some hesitation to move forward with this.  What are some thoughts out there on the subject?  Is there "real" definite reasons on why a person would or would not want to use 2.0.X or 1.3.3 ?

**Answer:** 1.3.3 is the latest version that was shipped with a point release of Revit, and 1.3.4 is in the pipeline for the next update release.

Users/offices who are comfortable going ahead of the Revit shipping product can access both 1.3.4 and 2.0.2 currently on the Latest Stable release download page:

http://dynamobuilds.com/
http://dynamobim.org/download/

As far as why they would upgrade and what the impact is, Dynamo 2.0 does represent an upgrade process for 1.x scripts, and does have enhancements to what is offered. The bigger feature items are:

- Python improvements
- List management improvements (Dictionaries)
- Library browser UX
- A collection of new nodal functionality
- The ability to load Extensions from the Package Manager (an even more powerful manner of exchanging functionality)

The bulk of feature information is here from the original 2.0 release:

http://dynamobim.org/to-dynamo-2-0-and-beyond/

Feature information on extensions is here:

http://dynamobim.org/extensions-now-supported-in-package-manager/

Keep in mind that users can also have 2.x installed side-by-side with 1.x, so if they want to do their own comparison, this is pretty easy.

Again, if the user is going to upgrade, definitely take the latest build (2.0.2)

More information on what it takes to upgrade packages:

http://dynamobim.org/new-dynamo-developer-resources-and-updating-packages-for-dynamo-2-0/

The more granular release notes:

https://github.com/DynamoDS/Dynamo/wiki/Release-Notes

I hope this provides a good and professional complete answer to your question.


#### <a name="4"></a> Difference Between FamilySymbol and ElementType

Another recent [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread concerns
the [difference between `FamilySymbol` and `ElementType`](https://forums.autodesk.com/t5/revit-api-forum/difference-between-familysymbol-and-elementtype/m-p/8461805):

**Question:** Both FamilySymbol and ElementType seem to represent the same idea.  What is the difference between the two?  (I know one derives from the other)

A related question is: what's the difference between Element and FamilyInstance?  I can't understand why stairs are of class Stairs (derived from Element), while doors are of class FamilyInstance.  Are stairs not an "instance" of a particular "family"?

**Answer:** Families come in several flavours.

The two main ones are standard and built-in.

Actually, they are called 'component' and 'system', as I just learned from the developer guide section I point out below:

<center>
<img src="img/family_system_versus_component.png" alt="System versus component families" width="492">
<p style="font-size: 80%; font-style:italic">System versus component families</p>
</center>

Standard are defined in RFA family definitions.

Built-in are built into Revit directly.

Standard families define family symbols, and when you place a family symbol, it is represented by a family instance.

Other elements placed in the model are not called family instances; they are just elements.

Element is a super-class of FamilyInstance.

Analogously, ElementType is a superclass of FamilySymbol.

I hope this explains.

For more info, check
the [Revit API developer guide](http://help.autodesk.com/view/RVT/2019/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html) in
the [online Revit help](http://help.autodesk.com/view/RVT/2019/ENU), in the
section [Introduction](http://help.autodesk.com/view/RVT/2019/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_html)
&rarr; [Elements Essentials](http://help.autodesk.com/view/RVT/2019/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Elements_Essentials_html)
&rarr; [Other Classifications](http://help.autodesk.com/view/RVT/2019/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Elements_Essentials_Other_Classifications_html).


#### <a name="5"></a> Custom Exporter Execute May Throw

[mesh data export to json](https://forums.autodesk.com/t5/revit-api-forum/mesh-data-export-to-json/m-p/8459164)

**Question:** I try to retrieve the geometry info from a Revit model to JSON data that I can load to draw geometry by three.js.

This page: https://thebuildingcoder.typepad.com/blog/2013/07/adn-mesh-data-custom-exporter-to-json.html

Him had show me how to do.

Of course, the demo is right and work well by some changes from revit 2015or6 to 2019.

https://thebuildingcoder.typepad.com/files/curvedwall.rvt

this reivt file work well in my computer.

But when I do with other revit file, error comes from the

`exporter.Export(view)`

which the error say: `cannot devide by zero`


**Answer:** Run the add-in in the debugger and see for yourself which line of code is causing the problem.

Then you will probably be able to see how to fix it as well.


**Response:** The debugger does not help, because the exception is thrown inside Revit internal code while executing the `???` method.

The debugger will just raise a error top of the logic.

Finally I add the `try` `catch` handler and caught the error.

Thank you very much, and thanks for your demo about the use of `CustomExportContext`.


**Answer:** Glad to hear you resolved your problem.

By the way, rather belatedly, may I point out that others ran into the same problem in the past:

https://thebuildingcoder.typepad.com/blog/2016/07/exporting-rvt-bims-to-webgl-and-forge.html#3

The exception thrown by the internal custom exporter implementation can simply be caught and ignored:

https://github.com/jeremytammik/CustomExporterAdnMeshJson/commit/23a95aad8f4a3cca85a72b32e2b699bde1d...

