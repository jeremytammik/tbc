<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

Forge Design Automation and DesignScript with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 


&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Forge Design Automation and DesignScript


####<a name="2"></a> DesignScript with Dynamo and Revit

A question in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [DesignScript](https://forums.autodesk.com/t5/revit-api-forum/design-script/m-p/9741288) led
to an interesting conversation and clarification with some colleagues from the Revit development team:

**Question:** Is it correct to assume that you can access Design Script in the Revit API environment by using Dynamo?
Is it also correct to assume that we have no plans to make Design Script accessible in the Revit API environment without Dynamo?

**Answer:** To clarify some terms, the Dynamo geometry library and DesignScript are distinct things.
DesignScript is specific to Dynamo; there is no DesignScript without Dynamo.
The geometry library, however, is a set of normal C#/C++ wrappers around Autodesk geometry kernels.
It doesn't have to be consumed via DesignScript, and in fact many popular Dynamo packages are written in C# and call the geometry library functions directly from their C# implementations, as users are attempting to do here.

It sounds like what the users in that thread are asking for is to use the Dynamo geometry library from Revit API add-ins.
The obstacle that they are running into is (just guessing) that these libraries depend on ASM DLLs which are shipped only with the host apps, not with Dynamo, and part of the job of the host-Dynamo link is to establish which specific version of ASM should be consumed by Dynamo geometry library.

If all they're doing is referencing the assemblies, this connection is probably not correctly established, since normally the Dynamo-Revit addin is involved in setting everything up.

If we wanted to support and document this use case, we probably could.
There may be business and licensing questions we'd have to explore around expanding the sets of scenarios that users can access ASM from &ndash; Revit would still be required in this scenario, so it's likely fine, but we'd need to make sure there aren't any loopholes.

So far on the technical background.

We currently do not actively support this behaviour; as discussed it’s possible, but only if you spin up Dynamo too.

We get requests for access to Dynamo on the Cloud or access to a Geometry kernel via licensing to be used externally all the time; this is the first real request to use DesignScript inside of an entirely separate add-in inside of Revit.

Licensing might be akin to the way we provide ASM to Dynamo customers today &ndash; via their subscription to other ADSK software, in this case, Revit.

As with everything, if the need is there, and high enough, we can investigate, but it would need to be stacked up against all other development effort (i.e Cost/Benefit analysis). Our current efforts are focused around Lowering the Barrier to Entry inside of Dynamo and an Ecosystem play where you can get Dynamo in more places and/or access it more easily.
Adding DesignScript support to Revit sans Dynamo is not currently in our sights.

####<a name="3"></a> Forge Getting Started Material

Here is an updated overview of some important things to read or watch to prepare for one of
the [Forge Accelerators](https://forge.autodesk.com/accelerator-program):

- [Tutorial](https://learnforge.autodesk.io) to get more familiar with the Forge API
- [The Forge Blog](https://forge.autodesk.com/blog)
- [Documentation](https://forge.autodesk.com/developer/documentation)
- [Postman Collection of API calls](https://gist.github.com/petrbroz/5d28d996738bb0da4f7838ca43d53765)
- [Design Automation Postman Tutorial](https://github.com/Autodesk-Forge/forge-tutorial-postman)
- [Sample repositories](https://github.com/Autodesk-Forge?tabs=repositories)
- [StackOverflow for existing Q&amp;A &ndash;
  [General](https://stackoverflow.com/questions/tagged/autodesk-forge) /
  [Viewer](https://stackoverflow.com/questions/tagged/autodesk-viewer) /
  [Data Management](https://stackoverflow.com/questions/tagged/autodesk-data-management) /
  [Model Derivative](https://stackoverflow.com/questions/tagged/autodesk-model-derivative) /
  [Design Automation](https://stackoverflow.com/questions/tagged/autodesk-designautomation) /
  [BIM360](https://stackoverflow.com/questions/tagged/autodesk-bim360) /
  [Reality Capture](https://stackoverflow.com/questions/tagged/autodesk-realitycapture) /
  [WebHook](https://stackoverflow.com/questions/tagged/autodesk-webhooks)

Tools:

- [VS Code Plugin](https://forge.autodesk.com/blog/beginners-guide-design-automation-visual-studio-code)
- [OSS Manager](https://oss-manager.autodesk.io)
- [DA Manager](https://da-manager.autodesk.io)
- [Node-Red](https://forge.autodesk.com/blog/forge-node-red-visual-programming-forge)


<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 896 -->
</center>


<pre class="code">

</pre>




####<a name="4"></a> 
