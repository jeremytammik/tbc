<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- [VASA, a Dynamo tool for 3D voxel-based architectural space analysis](https://www.keanw.com/2021/09/introducing-vasa-for-voxel-based-architectural-space-analysis.html)

- roadmap
  [@AutodeskRevit](https://twitter.com/AutodeskRevit) shares
  the [Revit Public Roadmap Update Fall 2021](https://blogs.autodesk.com/revit/2021/09/30/revit-public-roadmap-update-fall-2021)
  > The [#Revit team](https://twitter.com/hashtag/Revit) is out with the latest edition of the Revit Public Roadmap.
  Check it on the blog and don't forget to register for [#AU21](https://twitter.com/hashtag/AU21), w
  here we're hosting [3 #AMA sessions](https://twitter.com/hashtag/AMA) to take your questions
  on what's new, what's in development, and the road ahead.

Pawel Piechnik
• 1st
Product Manager, Strategist and Structural Engineer standing at the intersection of AEC and IT industries
2d • 2 days ago

... a structural recommendation for the upcoming #AU2021 : Check out this session discussing an immersive, "inside-#revit" structural design case study: "Revit-driven design of steel structures with real-time model updating using ENERCALC Structural Engineering Library design calculations.". https://lnkd.in/e2SS9J7N
#structuralengineering

- Revit Category Guide
  https://docs.google.com/spreadsheets/d/1uNa77XYLjeN-1c63gsX6C5D5Pvn_3ZB4B0QMgPeloTw/edit#gid=1549586957
  Category Name	- Built In Enum -	User Mapped	- Display Category Name	- Display Category Name (Rus)
  
- how to hide internal edges of solids
  18247333 [Appearance of DirectShape created with Dynamo vs API]

twitter:

add #thebuildingcoder

New local language Forge classes and the renewed ability to easily edit and continue while debugging a #RevitAPI add-in #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon https://autode.sk/applycodechanges

Today, I highlight our new local language Forge classes and the renewed ability to easily edit and continue while debugging a Revit add-in
&ndash; Non-mobile after computer crash
&ndash; Local language Forge classes
&ndash; Apply code changes debugging Revit add-in...

linkedin:

New local language Forge classes and the renewed ability to easily edit and continue while debugging a #RevitAPI add-in

https://autode.sk/applycodechanges

- Non-mobile after computer crash
- Local language Forge classes
- Apply code changes debugging Revit add-in...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Question:** 

**Answer:**

**Response:**  

Many thanks to  for this very helpful explanation!

<pre class="code">
</pre>

-->

### Roadmaps and Vasa 


####<a name="2"></a> 


####<a name="3"></a>


For more information on the current Forge hackathon and Autodesk University, you can look at
the [AU website](https://www.autodesk.com/autodesk-university) and
Kean's article
on [the Forge Hackathon and counting down to AU2021](https://www.keanw.com/2021/09/at-the-forge-hackathon-counting-down-to-au2021.html).


####<a name="4"></a> 

- [VASA, a Dynamo tool for 3D voxel-based architectural space analysis](https://www.keanw.com/2021/09/introducing-vasa-for-voxel-based-architectural-space-analysis.html)

- roadmap
  [@AutodeskRevit](https://twitter.com/AutodeskRevit) shares
  the [Revit Public Roadmap Update Fall 2021](https://blogs.autodesk.com/revit/2021/09/30/revit-public-roadmap-update-fall-2021)
  > The [#Revit team](https://twitter.com/hashtag/Revit) is out with the latest edition of the Revit Public Roadmap.
  Check it on the blog and don't forget to register for [#AU21](https://twitter.com/hashtag/AU21), w
  here we're hosting [3 #AMA sessions](https://twitter.com/hashtag/AMA) to take your questions
  on what's new, what's in development, and the road ahead.

Pawel Piechnik
• 1st
Product Manager, Strategist and Structural Engineer standing at the intersection of AEC and IT industries
2d • 2 days ago

... a structural recommendation for the upcoming #AU2021 : Check out this session discussing an immersive, "inside-#revit" structural design case study: "Revit-driven design of steel structures with real-time model updating using ENERCALC Structural Engineering Library design calculations.". https://lnkd.in/e2SS9J7N
#structuralengineering

####<a name="4"></a> Revit Category Guide

Revit Category Guide
  https://docs.google.com/spreadsheets/d/1uNa77XYLjeN-1c63gsX6C5D5Pvn_3ZB4B0QMgPeloTw/edit#gid=1549586957
  Category Name	- Built In Enum -	User Mapped	- Display Category Name	- Display Category Name (Rus)
  
####<a name="4"></a> How to Hide Internal Edges of Solids

We gleaned some new information on how to hide internal edges of solids in a discussion on the appearance of `DirectShape` elements created with Dynamo vs directly using the Revit API:

**Question:** I made a custom node that creates volumes using `DesignScript.Geometry.Solid`.
If I convert this solid to a `DirectShape` in my node, the resulting solids obtained in Revit, `Revit.DB.Solid`, display the internal mesh edges.
If I use the Dynamo `DirectShape.ByGeometry` node instead, the resulting Revit is smooth and no internal mesh edges are shown.

<center>
<img src="img/direct_shape_tessellation_edges.png" alt="DirectShape tessellation edges" title="DirectShape tessellation edges" width="460"/> <!-- 921 -->
</center>

Why?

How do I create a solid without mesh edges in my own code? 

See my code for the creation of DirectShape:

<pre class="code">
  RevitDB.Material material = GetPassMaterial(doc);
  
  using (RevitDB.Transaction t = new RevitDB.Transaction(doc, "Create sphere direct shape"))
  {
    t.Start();
    
    int sens = 1;
    var sortedPointsVolumesDS = pointsVolumesDS.ToArray().OrderBy(pv => origine.DistanceTo(pv.Key) * sens);
    int passNumber = 1;
    foreach (var item in sortedPointsVolumesDS)
    {
      string name = $"{fanName}_{driName}_{passNumber}";
      
      // creation de la géometrie
      
      IList<RevitDB.GeometryObject> revitGeometry = item.Value.ToRevitType(
        RevitDB.TessellatedShapeBuilderTarget.Solid,
        RevitDB.TessellatedShapeBuilderFallback.Abort,
        material.Id);
      
      //IList<RevitDB.GeometryObject> tessellatedShape = null;
      // tessellatedShape = item.Value.ToRevitType(
      // RevitDB.TessellatedShapeBuilderTarget.AnyGeometry,
      // RevitDB.TessellatedShapeBuilderFallback.Abort,
      // material.Id);
      
      if (revitGeometry?.Count>0)
      {
        RevitDB.DirectShape ds = RevitDB.DirectShape.CreateElement(doc,
          new RevitDB.ElementId(RevitDB.BuiltInCategory.OST_GenericModel));
        ds.ApplicationId = name;
        ds.ApplicationDataId = Guid.NewGuid().ToString(); // "Geometry object id";
        ds.SetShape(revitGeometry);
        //ds.SetShape(tessellatedShape);
      }
      
      // on incremente le numéro de la passe
      passNumber++;
    }
    t.Commit();
  }
</pre>

**Answer:** The quick answer is simple:

The entire Dynamo framework is open source, giving you access to
the [full implementation source code](https://github.com/DynamoDS)

So, you might be able to find out how yourself from the Dynamo source code.
You could also discuss this in detail with the Dynamo experts in
the [Dynamo discussion forum](https://forum.dynamobim.com).

However, I have a suspicion or two of my own that I would like to check with the Revit development team for you first:

Maybe, the direct shape showing all the internal face tessellation edges has been defined using lots of separate individual triangular faces, whereas the other one uses just one single more complex planar face for the top and bottom.

Actually, I believe that direct shapes are not limited to linear edges, but can include curved edges as well. Maybe, the non-tessellated shape face does not consist of triangles at all, but just two straight edges and an arc.

I hope this helps.

Best regards,

-----------------------------------------------------------------------
https://autodesk.slack.com/archives/C0SR6NAP8/p1632838728105500

Jeremy Tammik

Dear experts, creating a direct shape from Dynamo hides the internal tessellation edges. The Revit-API-generated shape shows them. How can they be hidden using the native Revit API, please? -- SFDC #18247333 -- Here is the code:

Boris Shafiro

@John Mitchell or @Lou Bianchini could you please take a look and answer the question from Jeremy?

Lou Bianchini

As I recall, Revit API doesn't have a way to turn on or off mesh edges. However creating a mesh through TessellatedShapeBuilder may attempt to hide certain edges depending on the topology of the mesh. I defer to @John Mitchell to confirm if that's the case and if any recent improvement may change that for an upcoming preview release or not.

John Mitchell 

As Lou said, we did not change or add any API functions for our recent project to hide (some) interior mesh edges. We simply modified the function that marks certain mesh edges as hidden so that it marks all two-sided mesh edges as hidden. Previously, it only marked (nearly) tangential edges as hidden.
It was already the case that TessellatedShapeBuilder called that function when it created a mesh. However, TessellatedShapeBuilder may create a "faceted" BRep (i.e., a BRep with planar faces and straight edges) instead of a mesh, in which case no edge hiding is done (as far as I know). Moreover, the effectiveness of the new edge-hiding depends very much on the topological structure of the mesh, which itself depends on several factors.
Given that the sample code uses the option TessellatedShapeBuilderTarget.Solid, it could be that the Revit object is a Solid (internally, a BRep) and not a Mesh (internally, a GPolyMesh).

Angel Velez

Right.  Dynamo is not hiding interior mesh edges.  It is creating a proper solid in this case, which it will do when it can.  Note that in Revit 2023 we will hide the mesh edges, although in this case the user will be fooled into creating sub-optimal geometry (a mesh, when a solid is possible) as a result.

Jacob Small

The original poster might also appreciate this github link to the open source Dynamo for Revit code base:

https://github.com/DynamoDS/DynamoRevit/blob/5c3b0d869ccdc2f4d5fd24b5346933f22d39f279/src/Libraries/RevitNodes/Elements/DirectShape.cs

Might not solve his problem entirely but it should give him a few strings to pull at.

Michael Kirschner

Angel has it correct - if the input geometry is a solid or surface a Dynamo tries to use the BrepBuilder, if it fails it uses the tessellated shape builder, - if the input geometry is a Mesh, then it uses the tessellated shape builder directly. (Thanks for linking to the code Jacob) - been a while since I looked at this.

Jeremy Tammik

Great! Thank you all very much for the quick and complete and very helpful answers!

-----------------------------------------------------------------------
Email: Regarding [CaseNo:18247333.] Appearance of DirectShape created with Dynamo vs API [ ref:_00D308uIL._5003gEAXvB:ref ] 10/1/2021 12:45 PM Outbound

Dear Sébastien,

I heard back from the development team, and they confirm my hunch:

The Revit API doesn't have a way to turn on or off mesh edges. However, creating a mesh through TessellatedShapeBuilder may attempt to hide certain edges depending on the topology of the mesh. 

We have not yet changed or added any API functions for our recent project to hide (some) interior mesh edges. We simply modified the function that marks certain mesh edges as hidden so that it marks all two-sided mesh edges as hidden. Previously, it only marked (nearly) tangential edges as hidden.

It was already the case that TessellatedShapeBuilder called that function when it created a mesh. However, TessellatedShapeBuilder may create a "faceted" BRep (i.e., a BRep with planar faces and straight edges) instead of a mesh, in which case no edge hiding is done (as far as I know). Moreover, the effectiveness of the new edge-hiding depends very much on the topological structure of the mesh, which itself depends on several factors.

Given that the sample code uses the option TessellatedShapeBuilderTarget.Solid, it could be that the Revit object is a Solid (internally, a BRep) and not a Mesh (internally, a GPolyMesh).

Dynamo is not hiding interior mesh edges.  It is simply creating a proper solid in this case, which it will do when it can.  Note that in the next major release, we will hide the mesh edges, although that will unfortunately fool the user into creating sub-optimal geometry as a result, creating a mesh, when a solid is possible.

The original poster might also appreciate this github link to the open source Dynamo for Revit code base:

https://github.com/DynamoDS/DynamoRevit/blob/5c3b0d869ccdc2f4d5fd24b5346933f22d39f279/src/Libraries/RevitNodes/Elements/DirectShape.cs

Might not solve his problem entirely, but it should give him a few strings to pull at.

To summarise: if the input geometry is a solid or surface, Dynamo tries to use the BrepBuilder; if it fails it uses the tessellated shape builder, e.g., if the input geometry is a Mesh, then it uses the tessellated shape builder directly. 

I hope this helps.

Best regards,

-----------------------------------------------------------------------
