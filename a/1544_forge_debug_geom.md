<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="run_prettify.js" type="text/javascript"></script> --> 
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- debugging node.js with vs 2015 on mac
  http://through-the-interface.typepad.com/through_the_interface/2017/02/enabling-visual-studio-codes-integrated-web-debugging.html
  [Visual Studio Code](https://code.visualstudio.com)


 #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge 

I am in Gothenburg supporting the Forge accelerator.
My project here is ForgeFader, implementing the same end user functionality as the RvtFader Revit C# .NET API add-in that I implemented last week.
I have not quite finished it yet, but the existing functionality looks as if it already ought to be very useful indeed to anyone working with geometry in the Forge viewer
&ndash; Working in Visual Studio Code
&ndash; ForgeFader
&ndash; Implementation
&ndash; Adding custom geometry to the Forge Viewer
&ndash; Next steps...

-->

### Adding Custom Geometry to the Forge Viewer

I am in Gothenburg supporting the
[Forge accelerator](http://thebuildingcoder.typepad.com/blog/2017/03/events-uv-coordinates-and-rooms-on-level.html#2).

My project here is [ForgeFader](https://github.com/jeremytammik/forgefader),
implementing the same end user functionality as
the [RvtFader](https://github.com/jeremytammik/RvtFader) Revit
C# .NET API add-in that I implemented last week.

I have not quite finished it yet, but the existing functionality looks as if it already ought to be very useful indeed to anyone working with geometry in the Forge viewer.

- [Working in Visual Studio Code](#2)
- [ForgeFader](#3)
- [Implementation](#4)
- [Adding custom geometry to the Forge Viewer](#5)
- [Next steps](#6)



#### <a name="2"></a>Working in Visual Studio Code

Inspired by Kean Walmsley's note
on [enabling Visual Studio Code’s integrated web debugging](http://through-the-interface.typepad.com/through_the_interface/2017/02/enabling-visual-studio-codes-integrated-web-debugging.html),
I am using [Visual Studio Code](https://code.visualstudio.com) to develop ForgeFader and very happy with it indeed.

<center>
<img src="img/forgefader_in_vs_code.png" alt="ForgeFaver in Visual Studio Code" width="500"/>
</center>

I have not actually enabled
the [integration with Chrome debugging](https://marketplace.visualstudio.com/items?itemName=msjsdiag.debugger-for-chrome) that
is the main point of Kean's article, but just using this as an editor is a good start.

#### <a name="3"></a>ForgeFader

ForgeFader implements a Forge viewer extension to calculate and display signal attenuation caused by distance and obstacles in a building model with a floor plan containing walls.

It implements a functionality similar to [RvtFader](https://github.com/jeremytammik/RvtFader):

Given a source point, calculate the attenuation in a widening circle around it and display that as a heat map.

Two signal attenuation values in decibels are defined in the application settings:

- Attenuation per metre in air
- Attenuation by a wall

This app is based on Philippe Leefsma's [Forge React boilerplate sample](https://github.com/Autodesk-Forge/forge-react-boiler.nodejs).

Please refer to that for more details on the underlying architecture and components used.


#### <a name="4"></a>Implementation

The ForgeFader implementation lives
in [Viewing.Extension.Fader.js](https://github.com/jeremytammik/forgefader/blob/master/src/client/viewer.components/Viewing.Extension.Fader/Viewing.Extension.Fader.js).

On loading, in `onGeometryLoaded`, it determines the Revit BIM wall fragments for subsequent ray tracing.

On picking a point on a floor in the model, in `onSelection`, it launches the `attenuationCalculator` function to do the work.

That fiddles around a bit to determine the picked floor top faces and add a new mesh to the model on which to draw the attenuation map.

Once the mesh has been added, it in turn calls `rayTraceToFindWalls` to create a bitmap representing the signal attenuation to be displayed by a custom shader.

#### <a name="5"></a>Adding Custom Geometry to the Forge Viewer

When debugging any kind of geometrical programming task, it is of utmost importance to be able to comfortably visualise the situation.

In this app, I add three different kinds of geometry dynamically to the model displayed by the Forge viewer:

- Points and lines representing the top face of the floor and the picked source point.
- A mesh representing the top face of the floor to be equipped with a custom shader and offset slightly above and away from the floor element surface.
- Points and lines representing the raytracing rays.

Three example screen snapshots illustrate what I mean.

Display points and lines for debugging using `drawVertex` and `drawLine`:

<center>
<img src="img/forgefader_line_vertex_debug_marker_300.png" alt="Line and vertex debug markers" width="300"/>
</center>

Create a mesh to represent the floor top face and offset it up slightly above the floor surface:

<center>
<img src="img/forgefader_floor_top_face_mesh_250.png" alt="Floor top face mesh" width="250"/>
</center>

A debug helper displaying lines in the model representing the ray tracing rays:

<center>
<img src="img/forgefader_ray_trace_rays_250.png" alt="Ray tracing rays" width="250"/>
</center>

#### <a name="6"></a>Next Steps

- Perform the raytracing to determine the number of walls between the picked signal source point and a grid of target points
- Generate a bitmap based on that information, or simply a mapping of `(u.v)` values to the desired colour value.
- Implement a custom fragment shader to display the (u,v) to colour mapping on the floor top face mesh.

