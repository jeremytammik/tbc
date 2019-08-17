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

- [How to Become a Successful Freelancer poscast]
  https://www.freecodecamp.org/news/how-to-become-a-successful-freelancer-podcast/
  (90 minute listen)
  How to become a successful freelancer: a podcast interview with Kyle Prinsloo.
  Kyle dropped out of school and worked as a jewelry salesman before teaching himself to code.
  His freelance business grew, and he now runs a profitable software development consultancy in South Africa.

- load .net assembly from memory stream
  [Assembly.Load Method](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly.load?view=netframework-4.8) Loads an assembly.
  [overload taking a Byte array arguyent](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly.load?view=netframework-4.8#System_Reflection_Assembly_Load_System_Byte___) Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the application domain of the caller.
  [Q] I am interested in how the revit addin manager manages (:wink:) to reload addins on the fly - in Dynamo we have the notion of packages which are similar to addins (a set of dlls or dynamo code we might need to load) - for dynamo code, reloading or unloading it can be done, but for .net dlls - my understanding is that in net framework (prior to the release of .net core 3) you cannot unload an assembly once loaded - how does the addin manager do this? Whos the owner and is the code in the revit repo? (edited)
  [A] i am not privy to the add-in manager source code, so i can only guess. you can load .NET code either from a DLL file on disk or from a stream in memory, cf. https://stackoverflow.com/questions/40384619/how-to-load-assembly-from-stream-in-net-core. the add-in manager reads the DLL file on disk, converts it to a memory stream, and uses that to load the .NET code into the .NET environment. therefore, .NET never gets to see or touch (or lock) the DLL file.

twitter:

Successful freelancing, loading an assembly from a memory stream to prevent DLL locking, and wrapping an element for Dynamo zero-touch node in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/pipedirection

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

Dynamo Zero Touch CS#Node Element Wrapper

-->

### Zero Touch Node Wrapper and Load from Stream

Let's wrap up the week with these items:

<center>
<img src="img/.png" alt="" width="100">
</center>

####<a name="2"></a> Changing Pipe Direction

Neerav Mehta shared some research on determining precise directions for inserting pipe fittings in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [changing pipe direction](https://forums.autodesk.com/t5/revit-api-forum/changing-pipe-direction/m-p/8966993):

**Question:** I am trying to place a pipe precisely between two points: intersectionPoint and intersectionPoint + perpendicularDirection. I am using the following statement to move the pipe:

<pre class="code">
  Pipe dummyPipe = doc.GetElement(
    ElementTransformUtils.CopyElement(
      doc, branchPipe.Id, XYZ.Zero).First()) as Pipe;

  (dummyPipe.Location as LocationCurve).Curve
    = Line.CreateBound( intersectionPoint,
      intersectionPoint + perpendicularDirection );
</pre>

With the above placement, I would expect the pipe's curve's direction to be exactly equal to perpendicularDirection, but that's not the case.

As an example, if `perpendicularDirection` is set to {(-0.016831296, 0.968685013, -0.052781984)}, the direction of the pipe comes out to be {(-0.017347059, 0.998368562, -0.054399389)}.

Note that there is a slight difference in the direction and, as a result, I am not able to connect this pipe to a Tee fitting, as described in the thread
on [how to create a tee fitting with a non-90 degree branch angle for pipe](https://forums.autodesk.com/t5/revit-api-forum/how-to-create-a-tee-fitting-of-not-90-degree-branch-angle-for/td-p/8433556).

BTW, the same issue happens even if I create a new pipe between two points.

Any idea how to fix this?

**Suggestion:** In some cases, you do not need to specify an exact pipe direction to create a pipe between fittings, nor an exact fitting location to insert a fitting between existing pipes.

Revit will automatically adjust the newly created element appropriately to connect with the existing elements.

Different variations of this approach are explored and discussed in the research series
on [creating a rolling offset](http://thebuildingcoder.typepad.com/blog/2014/01/final-rolling-offset-using-pipecreate.html).

Here are some previous articles on the `NewTakeoffFitting` method and taps:

- [Use of `NewTakeOffFitting` on a duct](http://thebuildingcoder.typepad.com/blog/2011/02/use-of-newtakeofffitting-on-a-duct.html)
- [Use of `NewTakeOffFitting` on a pipe](http://thebuildingcoder.typepad.com/blog/2011/04/use-of-newtakeofffitting-on-a-pipe.html)
- [Adjustable versus perpendicular spud](http://thebuildingcoder.typepad.com/blog/2013/02/adjustable-versus-perpendicular-spud.html)
- [Splitting a duct or pipe with taps](http://thebuildingcoder.typepad.com/blog/2014/02/daylighting-extension-and-splitting-with-taps.html#3)

**Solution:** I looked at the rolling offset, but it deals only with elbows, not tee fittings.

The problem in this case, however, was due to inaccuracy in the cross product.

I was deriving the pipe direction by doing two cross-products of unit-norm vectors. In other words:

<pre>
  pipeDirection = X.CrossProduct(Y).CrossProduct(X)
</pre>

Since X and Y are of unit-norm, I was expecting `pipeDirection` to be unit-norm as well.

However, even if both X and Y are unit length, their cross product will still NOT be unit length unless they are exactly perpendicular, cf.
the [detailed explanation of the cross product](https://en.wikipedia.org/wiki/Cross_product).

Therefore, the length of `pipeDirection` was about 0.971 instead of 1.0.

Once I normalise `pipeDirection`, it works fine.

BTW, the rolling offset posts only deal with inserting elbows, which works pretty well.

In my case, I am inserting a T connection, and that API is a horrible mess. Here are the issues:

- `CreateTeeFitting` works only when the connection is 90 degrees, so you have to use the workaround mentioned in [Tee Fitting with no right angle](https://forums.autodesk.com/t5/revit-api-forum/tee-fitting-with-no-right-angle/m-p/8954339).
- The above workaround works only when the base pipe is placed in a way that's acceptable to Revit. As an example, adding a Tee connection works if the two pipes are placed along X axis and the branch pipe is along the Y-axis. But, if I rotate the three pipes slightly, for example by 0.1 degrees, the `CreateTeeFitting` function fails.
- So now I have to use another workaround, creating dummy pipes in a position that Revit likes and a 90-degree T connection. Then delete all the dummy pipes and the fittings that were automatically created, except the T connection. Now rotate the T fitting to what I want, for e.g. by 0.1 degrees. Then change the angle of the branch connector to something other than 90 degrees.

This issue along
with [line based family location don't update origin after change](https://forums.autodesk.com/t5/revit-api-forum/tee-fitting-with-no-right-angle/m-p/8954339) has
taken me about a week to create a T connection at the right place and orientation instead of probably an hour if the API had worked more intuitively in the first place.

Congratulations and many thanks to Neerav Mehta for this research and solution.

Sorry to hear that the API is so hard to use in this situation. 

####<a name="3"></a> MEP Ductwork Creation Tip 

A while ago, Ollikat shared a general MEP ductwork creation tip in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [automatic duct between fittings](https://forums.autodesk.com/t5/revit-api-forum/automatic-duct-between-fittings/m-p/8890265), summarising his extensive experience in this area:

> In summary, I have been co-working several years with my colleague in a feature where different parts of network (duct, pipes, etc.) are being created and connected via API. If we would need to redo everything from scratch today, we definitely would use only low-level methods, like `CreateFamilyInstance` etc., and then explicitly move, rotate and connect each part of the network separately (also meaning that if there's a need for reducer, create it manually). This is the best approach, because using higher level methods like `NewElbowFitting` etc. causes all sort of problems, and it is a very tedious job to implement a reliable solution for every possible scenario. Using low level methods gives you total control over what's happening. It's not convenient, but, in the long run, will save your time. This, of course, depends what kind of add-in you are developing.

> Unfortunately, the main point is that there's no easy way of generating networks in the Revit API.

