<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!--
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Simplifying nested family instances in the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/simplifynestedinstance

José Alberto Torres Jaraute implemented an add-in tool to protect the intellectual property built into a complex hierarchy of nested family instances by replacing them with a flatter and simpler hierarchy, yet retaining all the relevant non-confidential custom data.
Basically, his tool also enables location of overlapping elements and duplicates elimination.
In the course of this work, Alberto raised a number of questions in
the Revit API discussion forum
&ndash; Explode nested families
&ndash; Insert a curve-based family instance associated to a face
&ndash; Explode family instance to get all the components of a family in project
&ndash; Change the host and work plane of a family...

--->

### Simplifying Nested Family Instances

José [Alberto Torres](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/71628) Jaraute has
been working on an add-in tool to protect the intellectual property built into a complex hierarchy of nested family instances by replacing them with a flatter and simpler hierarchy, yet retaining all the relevant non-confidential custom data.

Basically, his tool also enables location of overlapping elements and duplicates elimination.

In the course of this work, Alberto raised a number of questions in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160):

- [Explode nested families](https://forums.autodesk.com/t5/revit-api-forum/explode-nested-familes/m-p/8042667)
- [Insert a curve-based family instance associated to a face](https://forums.autodesk.com/t5/revit-api-forum/insert-a-family-curvebased-with-newfamilyinstance-associated-to/m-p/7390334)
- [Explode family instance to get all the components of a family in project](https://forums.autodesk.com/t5/revit-api-forum/explode-familyinstance-to-get-all-the-components-of-the-family/m-p/6984603)
- [Change the host and work plane of a family](https://forums.autodesk.com/t5/revit-api-forum/is-there-no-way-to-change-the-host-and-work-plan-of-a-family/m-p/7252070)

These discussions led to a fruitful conclusion, and Alberto now very kindly reports on the successful project completion:
 
I use the term `SET` to denote all the `FamilyInstance` children of a `FamilyInstance` with multiple sub-instances.

I implemented the following procedure to deal with a `SET`:

- Select a `SET`.
- Save in a class the main corporate parameters of this `SET`: Phases, Manufacturer, custom data, etc.
- Remove all `FamilyInstance` daughters with `GetSubComponentIds` to a collection of element ids.
- Go through each element of the collection and convert it to a `FamilyInstance`.
- From each element, retrieve its transformation from `GeometryInstance` and extract the insertion point and `BasisX` to determine its rotation.
- Create a `SketchPlane` using this data and the normal vector it defines.
- Analyse the `FamilySymbol`, `Mirrored` and `FacingFlipped` properties.
- Insert a new `FamilyInstance` into the new `SketchPlane` taking the insertion point and rotation of the original family into account:

<pre class="code">
      doc.Create.NewFamilyInstance(
        point, symbol, xvec, sketchPlane,
        StructuralType.NonStructural );
</pre>

- Copy all the parameter data of the original family instance to the new one.
- Copy the saved parameters of the SET to the new family instance.
- Determine whether the original family instance has `Mirrored` or `FacingFlipped` set.
- If either of them is `true`, calculate and apply the corresponding mirroring, calculating it from the work plane, the reference plane or the corresponding reference line.
- Last and very important: extract all extrusions from the general parent `SET` and recreate them as `DirectShape` elements, using the name of the family instance with an auto-numbering suffix.
- Finally, after asking the user, group all the new elements in a `Group` with the name of the `SET` plus an auto-numbering suffix.

I also implemented an event to cancel the warnings displayed when inserting a new family instance at a point where another one already exists. It is removed again after terminating this process.

Unfortunately, I cannot share the complete code for confidentiality reasons.

Thank you very much for the fruitful discussions!

Many thanks to Alberto for sharing his experience and workflow!

By the way, in case you are interested in flattening and simplifying, you might also want to check out the more radical approach 
of [flattening all elements to `DirectShape`](http://thebuildingcoder.typepad.com/blog/2015/11/flatten-all-elements-to-directshape.html).

<center>
<img src="img/nesting_matryoshka_dolls.png" alt="Nested matryoshka dolls" width="270"/>
</center>

