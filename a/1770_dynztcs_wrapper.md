<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Wrapper required to pass an element from Revit to Dynamo
  Trying to retrieve the DimensionType of a Dimension
  https://forums.autodesk.com/t5/revit-api-forum/trying-to-retrieve-the-dimensiontype-of-a-dimension/m-p/8968599
  Alexandra Nelson
  Frank Aarssen
  [Q] I'm trying to build a ZeroTouchNode with C# in Visual Studio that's input is a Dimension element and it's output is the DimensionType of that Dimension element. It seemed simple, but I'm having trouble returning the DimensionType element. Instead, the node is returning "Autodesk.Revit.DB.DimensionType". How do I retrieve the actual element? My code is included below. Thanks in advance for the help! 
  When I try to return the id, it returned the id of the dimension type, but not the dimension type element. Also, when I tried to return the element using the "Get Element" method and changed the return type to "Element" (as seen below) rather than "ElementType" as I had previously, it still gives me the same output.
  [A] You can not directly return a Revit class. You need to "wrap" into a Dynamo wrapper class.
  see [Become a Dynamo Zero Touch C# Node Developer in 75 Minutes](https://forum.dynamobim.com/t/become-a-dynamo-zero-touch-c-node-developer-in-75-minutes/28007)
  and download the handout.
  [R] Thank you for your help and for sharing the link with me. I worked through the concepts in that handout and got it working.
  Here's the working script :
    using Autodesk.Revit.DB;
    using RevitServices.Persistence;
    using Revit.Elements;
    namespace theWorks
    {
      public class Dimensions
      {
        private Dimensions() { }
        public static Revit.Elements.Element GetDimType(Revit.Elements.Element dimension)
        {
          Document doc = DocumentManager.Instance.CurrentDBDocument;
          Autodesk.Revit.DB.Element UnwrappedElement = dimension.InternalElement;
          ElementId id = UnwrappedElement.GetTypeId();
          ElementType dimType = doc.GetElement(id) as ElementType;
          return dimType.ToDSType(false);
        }
      }
    }  

- load .net assembly from memory stream
  [Assembly.Load Method](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly.load?view=netframework-4.8) Loads an assembly.
  [overload taking a Byte array arguyent](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly.load?view=netframework-4.8#System_Reflection_Assembly_Load_System_Byte___) Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the application domain of the caller.
  [Q] I am interested in how the revit addin manager manages (:wink:) to reload addins on the fly - in Dynamo we have the notion of packages which are similar to addins (a set of dlls or dynamo code we might need to load) - for dynamo code, reloading or unloading it can be done, but for .net dlls - my understanding is that in net framework (prior to the release of .net core 3) you cannot unload an assembly once loaded - how does the addin manager do this? Whos the owner and is the code in the revit repo? (edited)
  [A] i am not privy to the add-in manager source code, so i can only guess. you can load .NET code either from a DLL file on disk or from a stream in memory, cf. https://stackoverflow.com/questions/40384619/how-to-load-assembly-from-stream-in-net-core. the add-in manager reads the DLL file on disk, converts it to a memory stream, and uses that to load the .NET code into the .NET environment. therefore, .NET never gets to see or touch (or lock) the DLL file.

- [How to Become a Successful Freelancer poscast]
  https://www.freecodecamp.org/news/how-to-become-a-successful-freelancer-podcast/
  (90 minute listen)
  How to become a successful freelancer: a podcast interview with Kyle Prinsloo.
  Kyle dropped out of school and worked as a jewelry salesman before teaching himself to code.
  His freelance business grew, and he now runs a profitable software development consultancy in South Africa.

twitter:

Successful freelancing, loading an assembly from a memory stream to prevent DLL locking, and wrapping an element for Dynamo zero-touch node in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/zerotouchnodewrappeer

Here are some topics that came up last week
&ndash; Dynamo Zero Touch Node Revit element wrapper
&ndash; Loading a .NET assembly from a memory stream
&ndash; How to become a successful freelancer
&ndash; Talking about memory streams, I hiked up Rio Chillar in Nerja, Andalusia...

linkedin:

Successful freelancing, loading an assembly from a memory stream to prevent DLL locking, and wrapping an element for Dynamo zero-touch node in the #RevitAPI 

http://bit.ly/zerotouchnodewrappeer

Here are some topics that came up last week:

- Dynamo Zero Touch Node Revit element wrapper
- Loading a .NET assembly from a memory stream
- How to become a successful freelancer

Talking about memory streams, I hiked up Rio Chillar in Nerja, Andalusia...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

Dynamo Zero Touch CS#Node Element Wrapper

-->

### Zero Touch Node Wrapper and Load from Stream

Let's start this week with these topics that came up in the last one:

- [Dynamo Zero Touch Node Revit element wrapper](#2)
- [Loading a .NET assembly from a memory stream](#3)
- [How to become a successful freelancer](#4)

Talking about memory streams, I hiked up Rio Chillar in Nerja, Andalusia:

<center>
<img src="img/rio_chillar.jpg" alt="Rio Chillar" width="400">
<p style="font-size: 80%; font-style:italic">Natural stream &ndash; Rio Chillar</p>
</center>


####<a name="2"></a> Dynamo Zero Touch Node Revit Element Wrapper

Yet another solution suggested by
Frank [@Fair59](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518) Aarssen
helped Alexandra Nelson share the solution to implement a wrapper for passing back a Revit element to Dynamo from a C# Zero-Touch node, in the thread 
on [retrieving the `DimensionType` of a `Dimension`](https://forums.autodesk.com/t5/revit-api-forum/trying-to-retrieve-the-dimensiontype-of-a-dimension/m-p/8968599):

**Question:** I'm trying to build a ZeroTouchNode with C# in Visual Studio whose input is a `Dimension` element and outputs its `DimensionType`. It seemed simple, but I'm having trouble returning the `DimensionType` element. Instead, the node is returning "Autodesk.Revit.DB.DimensionType". How do I retrieve the actual element? 

When I try to return the element id, the id of the dimension type appears, but not the dimension type element.

When I tried to return the Revit element itself using the `GetElement` method and changed the return type to `Element` rather than `ElementType`, it still gives me the same output.

**Answer:** You cannot directly return a Revit class. You need to wrap it in a Dynamo wrapper class.

Please see how to [become a Dynamo Zero Touch C# Node developer in 75 minutes](https://forum.dynamobim.com/t/become-a-dynamo-zero-touch-c-node-developer-in-75-minutes/28007)
and download the handout.

**Answer:** Thank you for your help and for sharing the link with me. I worked through the concepts in that handout and got it working.

Here's the working script:

<pre class="code">
  using Autodesk.Revit.DB;
  using RevitServices.Persistence;
  using Revit.Elements;
  
  namespace theWorks
  {
    public class Dimensions
    {
      private Dimensions() { }
      
      public static Revit.Elements.Element GetDimType(
        Revit.Elements.Element dimension )
      {
        Document doc = DocumentManager.Instance
          .CurrentDBDocument;
          
        Autodesk.Revit.DB.Element UnwrappedElement
          = dimension.InternalElement;
          
        ElementId id = UnwrappedElement.GetTypeId();
        
        ElementType dimType = doc.GetElement(id)
          as ElementType;
          
        return dimType.ToDSType(false);
      }
    }
  }  
</pre>

Many thanks for Alexandra and Frank for clarifying this!

####<a name="3"></a> Loading a .NET Assembly from a Memory Stream

A little note on how the add-in manager avoids locking the DLLs it loads:

**Question:** I am interested in how the Revit add-in manager manages (:wink:) to reload addins on the fly &ndash; in Dynamo, we have the notion of packages, which are similar to add-ins (a set of DLLs or Dynamo code we might need to load)  &ndash; reloading or unloading can be done for Dynamo code, but for .NET DLLs &ndash; my understanding is that in the NET framework (prior to the release of .NET core 3) you cannot unload an assembly once loaded &ndash; how does the addin manager do this?

**Answer:** You can load .NET code either from a DLL file on disk or from a stream in memory.

The [Assembly.Load method](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly.load?view=netframework-4.8) Loads an assembly.

It also provides
an [overload taking a `Byte` array argument](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly.load?view=netframework-4.8#System_Reflection_Assembly_Load_System_Byte___). That loads the assembly from a memory stream instead.
Cf. also [how to load assembly from stream](https://stackoverflow.com/questions/40384619/how-to-load-assembly-from-stream-in-net-core).

The add-in manager reads the DLL file from disk, converts it to a memory stream, and uses that to load the .NET code into the .NET environment. Therefore, .NET never gets to see or touch (or lock) the DLL file.

This approach has also been used to implement frameworks
enabling [debug and continue functionality for Revit add-ins](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.49).


####<a name="4"></a> How to Become a Successful Freelancer

Let\s close with this 90-minute [freeCodeCamp](https://www.freecodecamp.org) interview
on [how to become a successful freelancer](https://www.freecodecamp.org/news/how-to-become-a-successful-freelancer-podcast):

> Kyle dropped out of school and worked as a jewellery salesman before teaching himself to code.

> His freelance business grew, and he now runs a profitable software development consultancy...

