<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- https://highlightjs.org/#usage
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
<script>hljs.highlightAll();</script>
-->

<!-- https://prismjs.com -->
<link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>
<style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style>

</head>

<!---

- perform boolean in CGAL:
  https://forums.autodesk.com/t5/revit-api-forum/how-to-execute-booleanoperations-on-revit-solid-by-autocad/m-p/13005223

- OpenCascade for Boolean operations
  https://forums.autodesk.com/t5/revit-api-forum/boolean-operation-fail/m-p/12839281#M79543
  - OpenCascade for Boolean operations
  https://forums.autodesk.com/t5/revit-api-forum/boolean-operation-fail/m-p/12713966#M78244
  - https://forums.autodesk.com/t5/revit-api-forum/how-to-create-offset-solid-geometry-for-fittings-pipe-duct/m-p/12226377
  OpenCascade?
  https://forums.autodesk.com/t5/revit-api-forum/boolean-operation-fail/m-p/11648684/highlight/true#M68255
  - https://forums.autodesk.com/t5/revit-api-forum/boolean-operation-fail/m-p/7531968
  jira REVIT-122714 [Boolean Operation Fail - case 13578517]
  sfdc 13578517
  - voids
  Generate generic voids from wall profile
  https://forums.autodesk.com/t5/revit-api-forum/generate-generic-voids-from-wall-profile/m-p/8928031
  Here are some void-related discussions by The Building Coder:
  Beam Maker Using a Void Extrusion to Cut -- http://thebuildingcoder.typepad.com/blog/2010/07/beam-maker-using-a-void-extrusion-to-cut.html
  Boolean Operations and InstanceVoidCutUtils -- http://thebuildingcoder.typepad.com/blog/2011/06/boolean-operations-and-instancevoidcututils.html
  Transaction Group Regeneration for InstanceVoidCutUtils -- http://thebuildingcoder.typepad.com/blog/2014/04/instancevoidcututils-and-need-for-regeneration.html
  Voids in the Family Editor -- https://thebuildingcoder.typepad.com/blog/2014/10/brussels-hackathon-and-determining-pipe-wall-thick...
  Provision for Voids -- https://thebuildingcoder.typepad.com/blog/2017/03/wta-mech-and-ttt-for-provision-for-voids.html

twitter:

 @AutodeskRevit #RevitAPI #BIM @DynamoBIM

&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Boolean Alternatives


####<a name="2"></a>

Andrey [@ankofl](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/11836042) Kolesov
shareda solution using
the [Computational Geometry Algorithms Library CGAL](https://en.wikipedia.org/wiki/CGAL) and
the [OFF file format](https://en.wikipedia.org/wiki/OFF_(file_format)) to
perform Boolean operations on solids, presented in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [how to execute Boolean operations on Revit solid by AutoCAD](https://forums.autodesk.com/t5/revit-api-forum/how-to-execute-booleanoperations-on-revit-solid-by-autocad/m-p/13005223):


We are all familiar with the problems with Boolean operations on solid objects. A lot of exceptions occur during the operations of union, intersection and subtraction. In one of the branches, it was proposed to export Revit-Solid to Cascad-Colid, perform painful and other operations in it, and upload back only the result of such calculations, or transfer Cascad-Solid back to Revit-Solid.
SIX years after the beginning of this branch, the developers from Autodesk have not provided us with a working solution to this problem.

In my opinion, this is too complicated and time-consuming task to translate from Revit to Cascada, and it may be better to export Solid not to Cascad, but to AutoCAD, or rather use the loaded libraries to work with AutoCAD, but perform all operations in the Revit process. The already established export from DWG to RWT speaks in favor of this decision. Maybe it is possible to export Revit-Solid to AutoCAD-Solid and perform Boolean operations already there?

RPTHOMAS108
  Mentor RPTHOMAS108  in reply to: ankofl
‎2023-08-10 04:18 AM
I've manually carried out boolean operations in AutoCad in the past (quite a while ago however) and I think you would likely get the same issues there unless they've updated what AutoCad uses since. People have noted that such operations are more stable in Dynamo since it uses a slightly different system so my focus would be more on that. It did occur to me that the reason could be the unit system. Since Revit uses ft and not a smaller unit such as inches or mm you have smaller number on the LHS of the decimal place and so the floating point errors also shift over. Or to put it another way the decimal part has errors that occur through operations that the integer part doesn't and by using ft you are relying on more decimal places for accuracy of the same real world sized object (around 3 more) than you would be for mm.

All the units in Dynamo are converted to 'Dynamo Units' before such processing it seems. Would be interesting to test if you get more stability carrying out the operations after scaling the solids up SolidUtils.CreateTransformed (transform with uniform scale). You would likely have an issue if you then scale it back down (an edge too short) but it depends on the purpose of your operation i.e. calculating volume area vs needing to use in geometry. Perhaps I'm just clutching at straws since Dynamo uses it's own library also.

The other issue is that Revit doesn't appear to support all the forms of geometry that AutoCad does so you could end up after such operations in AutoCad having geometry that can't be brought back or is partial.

I think there is already a process in place for the connections which uses Advanced steel. When I looked at that previously it was working with dwgs in the background. Obviously you can Document.Export a dwg/sat use AutoCAD as a com server from the Revit process and Document.Import it back to access the geometry in Revit.

Tags (0)
Add tags
Report
MESSAGE 3 OF 15
ankofl
  Advocate ankofl  in reply to: RPTHOMAS108
‎2023-08-14 05:54 AM
Dear RPTHOMAS108, the fact is that in AutoCAD it is impossible to pass a Solid object in the form of dots, triangles, or directly Solid itself. You can only create a Solid in AutoCAD itself using standard tools: extrusion, shift, etc. and then apply Boolean operations on them, such as addition, subtraction and intersection. If I am wrong, and any of the AutoCAD libraries makes it possible to upload Solid to it from Revit, or create Solid based on surfaces or triangles from Revit, I will be glad if you let me know. Otherwise, you will have to create many different methods to export different types of geometry to AutoCAD, which would then perform Boolean and other operations on them!
Thanks!
Tags (0)
Add tags
Report
MESSAGE 4 OF 15
RPTHOMAS108
  Mentor RPTHOMAS108  in reply to: ankofl
‎2023-08-14 04:25 PM

Not sure about the AutoCAD API not used it for a while but you can open a dwg for sure and export a dwg from Revit also.

I think I would look for methods relating to SATs. e.g. Document.Import

Although generally I would probably leave AutoCAD out of the process.
AutoCAD 2022 Developer and ObjectARX Help | Boolean Method (ActiveX) | Autodesk

Most (if not all) of what you do via ActiveX/VBA can be accessed via COM, although VBA examples are VB6 but would be easy to convert to VB.Net and therefore C#. Then instead of using ThisDrawing/ThisApplication you would be creating a reference to such via COM. So I don't think it is imposable just wouldn't be the first direction I take with this.

I would try and look more closely at what Dynamo is doing since those components now come with Revit and AutoCAD doesn't. Even if they do have access to AutoCAD it doesn't mean they've installed it along with Revit.

Also as noted I've got a lot of 3d modelling experience as a user of AutoCad and like in Revit API Boolean operations either work or they don't (you need a good overlap of volume for certain operations to work). I think it comes down to if the faces become degenerate due to vertices merging (some libraries are better at cleaning up such things).
Tags (0)
Add tags
Report
MESSAGE 5 OF 15
ankofl
  Advocate ankofl  in reply to: RPTHOMAS108
‎2023-08-15 07:46 AM
Thanks for the information.
To be honest, I don't quite understand how to transfer Boolean operations from Revit to Dynamo, and even more so how this will solve the problem, since they both rely on the same mechanisms.
In another branch, there was an idea about transferring Revit geometry to OpenCascade geometry, and performing further Boolean operations already in the OpenCascade system. It looks like I'll have to stop at this option
Tags (0)
Add tags
Report
MESSAGE 6 OF 15
RPTHOMAS108
  Mentor RPTHOMAS108  in reply to: ankofl
‎2023-08-16 02:19 AM

I think Dynamo is using a different library it has the whole proto geometry thing with you having to convert between Dynamo geometry and Revit API geometry.

There are also more functions for manipulating geometry. The Revit API just exposes what Revit uses internally and those things often have their quirks.

Revit to ProtoSolid

If we were able to pick out every single bit like this or just the bits we needed for a task then we could perhaps utilise the library in the API without Dynamo. Instead of this people focus their efforts in trying to load all the other dependencies of Dynamo just so they can do with the API what they do with Dynamo. They start with Dynamo 'oh this is so easy' they progress to the API then comes the repeated question 'how do I do this that I am doing in Dynamo with the API?' What can be said, nothing other than 'Dynamo is more end user orientated and if you want to solve the same problem as a developer you have to go back to first principles.' It is a frustration that Dynamo geometry operations are so easy in comparison.

C:\Program Files\Autodesk\Revit 2024\AddIns\DynamoForRevit\libg_229_0_0
C:\Program Files\Autodesk\Revit 2024\AddIns\DynamoForRevit\LibG.Interface.dll
C:\Program Files\Autodesk\Revit 2024\AddIns\DynamoForRevit\ProtoGeometry.dll
Tags (0)
Add tags
Report
MESSAGE 7 OF 15
ankofl
  Advocate ankofl  in reply to: ankofl
‎2023-08-23 05:45 AM

I sincerely hope that the correction of Boolean operations on solids in the Revit Api will happen in the near future, because at the moment the development of our own solution transferring Boolean operations from C# Revit to C++ OpenCascade looks excessively forced.Снимок экрана 2023-08-23 153911.jpg
Tags (0)
Add tags
Report
MESSAGE 8 OF 15
jeremy_tammik
  Autodesk jeremy_tammik  in reply to: ankofl
‎2023-08-23 05:53 AM

Yup, valid concern, and sorry about the slow progress. I asked the development team for an update for you.

Jeremy Tammik  Developer Advocacy and Support + The Building Coder + Autodesk Developer Network + ADN Open
Tags (0)
Add tags
Report
MESSAGE 9 OF 15
jeremy_tammik
  Autodesk jeremy_tammik  in reply to: ankofl
‎2023-08-24 05:28 AM
They say: Unfortunately, we haven't had a chance to make progress on the front but are definitely aware of the issues. We have also had quite a few recent problem reports elsewhere related to failed Boolean operations.

By the way, a data set of issues with Booleans in the Revit API context might be nice to have.

Bringing the geometry into Dynamo and doing the operations there would be my recommendation, as it allows the user to see the intermediate steps when there is any kind of failure, which would make things easier to troubleshoot. They would want to learn the Dynamo intricacies, and of course running in the context of Dynamo means they aren't developing an add-in anymore but a Dynamo graph (in which they can call the Revit API).

Jeremy Tammik  Developer Advocacy and Support + The Building Coder + Autodesk Developer Network + ADN Open
Tags (0)
Add tags
Report
MESSAGE 10 OF 15
ankofl
  Advocate ankofl  in reply to: ankofl
‎2023-08-24 06:16 AM

Well, after a few days of development.. My decision to transfer Boolean operations from Revit C# to OpenCascade C++ is starting to bring results!
Of course, there are still problems with data transformation and backward compatibility (there is still a lot of work to be done on this), but in general, the solution has the right to life.
Here I want to address the question of how soon it will be possible to solve the problem with Boolean operations in Revit? If it's not a matter of two weeks, apparently I will have to perform further operations in geometry based on OpenCascade
Снимок экрана 2023-08-24 160728.jpg
 
Tags (0)
Add tags
Report
MESSAGE 11 OF 15
ankofl
  Advocate ankofl  in reply to: ankofl
‎2023-10-26 04:26 AM
Воз и ныне там...
Tags (0)
Add tags
Report
MESSAGE 12 OF 15
ankofl
  Advocate ankofl  in reply to: josephjsherman97
‎2024-02-19 11:02 PM
Is this a response from ChatGPT to raise the reputation of the participant? If not, can you describe in more detail the conversion of Solid objects from Revit to AutoCAD and the result back?
Tags (0)
Add tags
Report
MESSAGE 13 OF 15
jeremy_tammik
  Autodesk jeremy_tammik  in reply to: ankofl
‎2024-02-19 11:39 PM
My sentiments exactly. Where is the Unlike button?

By the way, congratulations on your success with OpenCascade! The  timeframe for progress on development of improvements to the built-in Boolean solid operations is probably a lot larger than two weeks, so your path is definitely worth pursuing if you need an immediate solution. I would love to share the technical details of your approach on the blog, if you are interested in that. Good luck and have fun!

Jeremy Tammik  Developer Advocacy and Support + The Building Coder + Autodesk Developer Network + ADN Open
Tags (0)
Add tags
Report
MESSAGE 14 OF 15
ankofl
  Advocate ankofl  in reply to: jeremy_tammik
‎2024-09-06 08:47 AM

Dear @jeremy_tammik

Well, it seems I'm finally close to a solution that suits me

UPD 08.09.2024:

Create .off file from revit-solid:


public static bool WriteOff(this Solid solid, out List<string> listString)
    {
      listString = ["OFF"];

      if(solid.CreateMesh(out var listVectors, out var listTri))
      {
        listString.Add($"{listVectors.Count} {listTri.Count} 0");
        listString.Add($"");

        foreach (var p in listVectors)
        {
          listString.Add(p.Write());
        }

        foreach (var v in listTri)
        {
          listString.Add($"3  {v.iA} {v.iB} {v.iC}");
        }

        return true;
      }
      return false;
    }


and this:


public static bool CreateMesh(this Solid solid, out List<XYZ> listVectors, out List<Tri> listTri)
    {
      double k = UnitUtils.ConvertFromInternalUnits(1, UnitTypeId.Meters);
      listVectors = [];
      listTri = [];

      bool allPlanar = true;

      int indV = 0;
      foreach (Face face in solid.Faces)
      {
        if (face is PlanarFace pFace)
        {
          Mesh mesh = pFace.Triangulate();
          for (int tN = 0; tN < mesh.NumTriangles; tN++)
          {
            var tri = mesh.get_Triangle(tN);

            var pT = new int[3];

            for (int vN = 0; vN < 3; vN++)
            {
              var p = tri.get_Vertex(vN) * k;

              if (p.Contain(listVectors, out XYZ pF, out int index))
              {
                pT[vN] = index;
              }
              else
              {
                pT[vN] = indV;
                listVectors.Add(p);
                indV++;
              }
            }

            listTri.Add(new(pT[2], pT[1], pT[0]));
          }
        }
        else
        {
          allPlanar = false;
        }
      }

      return allPlanar;
    }




Load .off file


#pragma once

bool load_from(const char* path, Mesh& output) {
    output.clear();
    std::ifstream input;
    input.open(path);
    if (!input) {
        return false;
    }
    else if (!(input >> output)) {
        return false;
    }

    input.close();
    return true;
}


Execute boolean


#pragma once

bool boolean_simple(Mesh m1, Mesh m2, b_t type, Mesh& out) {
    out.clear();
    int code = 0;
    if (!CGAL::is_triangle_mesh(m1)) {
        PMP::triangulate_faces(m1);
    }
    if (!CGAL::is_triangle_mesh(m2)) {
        PMP::triangulate_faces(m2);
    }
    if (type == b_t::join) {
        if (!PMP::corefine_and_compute_union(m1, m2, out)){
            std::cout << "fail_join ";
            return false;
        }
    }
    else if (type == b_t::inter) {
        if (!PMP::corefine_and_compute_intersection(m1, m2, out)){
            std::cout << "fail_inter ";
            return false;
        }
    }
    else if (type == b_t::dif) {
        if (!PMP::corefine_and_compute_difference(m1, m2, out)) {
            std::cout << "fail_dif ";
            return false;
        }
    }
    else {
        throw;
    }
    return true;
}



Save .off file:


#pragma once
#include <CGAL/Polygon_mesh_processing/IO/polygon_mesh_io.h>

bool save_to(const std::string path, Mesh input) {
    if (!CGAL::is_valid_polygon_mesh(input)) {
        return false;
    }
    try {
        if (CGAL::IO::write_polygon_mesh(path + ".off", input, CGAL::parameters::stream_precision(17))) {
            return true;
        }
        else {
            return false;
        }
    }
    catch (const std::exception& e) {
        std::cout << "save_to: exception!" << std::endl;
    }
    return false;
}



Then you can upload the .off file back to revit, or do other manipulations with it. However, as far as I know, API Revit does not allow you to create a full-fledged Solid object, but only a triangular grid, i.e. you can upload the grid obtained through CGAL to Revit for viewing, but you will not be able to perform further operations on solid with it, but only view its geometry through DirectShape

jeremy_tammik

Wow! Congratulations! That looks like a brilliant solution. Thank you for sharing it. Regarding your final sentences, I believe you can in fact generate solid shapes in your DirectShape object by using the BRepBuilder class; it allows direct construction of geometry objects including solids, closed and open shells, etc.:

https://www.revitapidocs.com/2024/94c1fef4-2933-ce67-9c2d-361cbf8a42b4.htm

I am very glad to hear about it.
I looked into the very powerful [LEDA Library of Efficient Data types and Algorithms](https://en.wikipedia.org/wiki/Library_of_Efficient_Data_types_and_Algorithms) a
long time ago, before it was merged into CGAL.


**Question:**

<center>
<img src="img/.png" alt="" title="" width="100"/> <!--  -->
</center>

**Answer:**

**Response:**

<pre><code class="language-cs"></code></pre>

