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


Adding Custom Geometry to the Forge Viewer #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge http://bit.ly/addforgegeom

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
- [Detailed notes and pointers](#7)



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


#### <a name="7"></a>Detailed Notes and Pointers

I made the following notes during the research and implementation steps.

- Colour gradient examples:
    - A series of three consecutive approaches to solve the task, starting with the most obvious, for the learning curve.
      However, the last in the series, using shaders, although last in the learning curve, once understood, is actually probably the most effective and simplest approach.
    - [Projecting Dynamic Textures onto Flat Surfaces with Three.js](https://forge.autodesk.com/cloud_and_mobile/2016/07/projecting-dynamic-textures-onto-flat-surfaces-with-threejs.html).
    - [Using Shaders to Generate Dynamic Textures in the Viewer API](https://forge.autodesk.com/cloud_and_mobile/2016/07/using-shaders-to-generate-dynamic-textures.html).
    - [mourner/simpleheat](https://github.com/mourner/simpleheat), A super-tiny JavaScript library for drawing heatmaps with Canvas
- Setting up the new project based on boilerplate code:
    - Fork [forge-react-boiler.nodejs](https://github.com/Autodesk-Forge/forge-react-boiler.nodejs).
    - Clone, npm install, npm start.
    - Translate my Revit model to obtain a `urn`: [models.autodesk.io](https://models.autodesk.io).
    - Load model into boilerplate: [localhost:3000/viewer?urn=...](http://localhost:3000/viewer?urn=...)
    - My urn for the little house floor is `dXJuOm...ydnQ`
    - [localhost:3000/viewer?urn=dXJuOm...ydnQ](http://localhost:3000/viewer?urn=dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bW9kZWwyMDE3LTAzLTI3LTEwLTM4LTMzLWQ0MWQ4Y2Q5OGYwMGIyMDRlOTgwMDk5OGVjZjg0MjdlL2xpdHRsZV9ob3VzZV9mbG9vci5ydnQ)
- Looked at `forge-rcdb.nodejs` sample `Viewing.Extension.Configurator.Predix.js` `onSelection`; it relies on `EventTool` and `EventsEmitter`.
- [How to get the model object tree of 2d drawing](http://stackoverflow.com/questions/41558468/how-to-get-the-model-object-tree-of-2d-drawing).
- `Viewer.Toolkit/Viewer.Toolkit.js` `getLeafNodes`.
- For ray tracing, look at `library-javascript-viewer-extensions` `Viewing.Extension.Transform/Viewing.Tool.Rotate.js` `onPointerDown` and `pointerToRaycaster`.
- [Create a custom mesh](http://stackoverflow.com/questions/9252764/how-to-create-a-custom-mesh-on-three-js) with a finer resolution than the original face.
- [Assign colours to the mesh](http://stackoverflow.com/questions/32063065/assigning-non-interpolated-colors-on-a-mesh-in-three-js).
- Call `scene.add` and pass a mesh.
- The viewer does not have any concept of the face of an element. 
  It is all just individual triangular fragments.
- Philippe implemented a snapper tool to collect as many triangles as possible to guess what the face might be in
  his  [GeometrySelector](http://viewer.autodesk.io/node/gallery/embed?id=560c6c57611ca14810e1b2bf&extIds=Autodesk.ADN.Viewing.Extension.GeometrySelector) in
  the [library-javascript-viewer-extensions](https://github.com/Autodesk-Forge/library-javascript-viewer-extensions).
- Check out the function `drawFace` in [Snapper.js](https://github.com/Autodesk-Forge/library-javascript-viewer-extensions/blob/master/src/Autodesk.ADN.Viewing.Extension.GeometrySelector/Autodesk.ADN.Viewing.Tool.Snapper.js).
- Shader produces all the points, calculates and sets result.
- [Stemkoski Three.js Examples](https://stemkoski.github.io/Three.js/),
[Shader &ndash; Attributes](https://stemkoski.github.io/Three.js/Shader-Attributes.html)
([source](view-source:https://stemkoski.github.io/Three.js/Shader-Attributes.html)).
- [Intro to Pixel Shaders in Three.js](https://www.airtightinteractive.com/2013/02/intro-to-pixel-shaders-in-three-js).

I want to attach a fragment shader to the picked floor face.
My shader should draw an image or texture directly, i.e., the desired 'heat map'.
Here are [som more complex pixel shader samples](https://threejs.org/examples/webgl_shader2.html).
I need to implement a fragment shader script and equip it with an id.
  
- WebGL shaders are written in GLSL, the [OpenGL Shading Language](https://en.wikipedia.org/wiki/OpenGL_Shading_Language).
- [Pixel Shaders](http://pixelshaders.com) by Toby Schachman and the [sample tutorial chapter](http://pixelshaders.com/sample).
- Philippe's first article on [Forge viewer custom shaders](http://adndevblog.typepad.com/cloud_and_mobile/2017/01/forge-viewer-custom-shaders-part-1.html).
