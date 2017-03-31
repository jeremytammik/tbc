<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<!-- <script src="run_prettify.js" type="text/javascript"></script> --> 
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js"></script>
</head>

<!---

 #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #threejs #javascript

&ndash; 
...

-->

### Adding Custom Shaders in the Forge Viewer

Today is the last day of 
the [Forge accelerator](http://thebuildingcoder.typepad.com/blog/2017/03/events-uv-coordinates-and-rooms-on-level.html#2),
and I am adding the finishing touches to the first revision of
my [ForgeFader](https://github.com/jeremytammik/forgefader) project.

ForgeFader implements the same end user functionality as
the [RvtFader](https://github.com/jeremytammik/RvtFader) Revit
C# .NET API add-in that I implemented last week.

In the past days, I showed how 
to [add custom geometry to](http://thebuildingcoder.typepad.com/blog/2017/03/adding-custom-geometry-to-the-forge-viewer.html) and
use [three.js raytracing in the Forge Viewer](http://thebuildingcoder.typepad.com/blog/2017/03/threejs-raytracing-in-the-forge-viewer.html).

As a final step to bring the ForgeFader functionality up to par with RvtFader, I show how to add custom fragment shaders to a model in the Forge viewer.

The custom shader is used to display the signal attenuation between a picked source point on a floor slab and all possible target points as a colour gradient.


#### <a name="3"></a>Slack Conversation

Jeremy Tammik [3:42 PM] 
what parameters can we use in a custom shader for lmv?
we currently have a totally trivial shader up and running:
https://github.com/jeremytammik/forgefader/blob/master/src/client/viewer.components/Viewing.Extension.Fader/Viewing.Extension.Fader.js#L10-L21
https://github.com/jeremytammik/forgefader/blob/master/src/client/viewer.components/Viewing.Extension.Fader/Viewing.Extension.Fader.js#L10-L21.
unfortunately, it just defines a constant colour.
how are the uv coordinates passed in?
what other parameters exist?

Traian Stanev [3:48 PM] 
@tammikj Are you interested in vertex attributes or adding more uniforms?

Jeremy Tammik [3:50 PM] 
both, vertex + fragment, existing + adding more. thx!

Traian Stanev [4:00 PM] 
Should be possible to set a breakpoint after the shader is generated to see what's already there. Or, you can look in `src/wgs.js/render/WebGLProgram.js` to see what uniforms it adds generally to all shaders (search for "identifiers"). As far as vertex attributes, that will vary per geometry, each geometry has an attributes property that you can look to see what vertex channels it has.

Jeremy Tammik [4:18 PM] 
where to set that breakpoint? we cannot find it in the viewer code.

[4:18]  
what file could it be?

[4:20]  
looking now at wgs.js...

Traian Stanev [4:23 PM] 
You can set it in your own code, e.g. here: https://github.com/jeremytammik/forgefader/blob/master/src/client/viewer.components/Viewing.Extension.Fader/Viewing.Extension.Fader.js#L183  and when it gets hit, look inside the shader material that you created, it should have the full shader text in it. (edited)

Jeremy Tammik [4:27 PM] 
ok. apparently, also in wgs.js at the lines `var vertexShader = resolve(material.__webglShader.vertexShader)` and `var fragmentShader = resolve(material.__webglShader.fragmentShader)`

Jeremy Tammik [4:44 PM] 
setting the breakpoint you suggest, i only see the shaders i created myself on the material i created myself. how to see from there what the default shaders provide?

Traian Stanev [4:48 PM] 
As mentioned: https://git.autodesk.com/A360/firefly.js/blob/develop/src/wgs.js/render/WebGLProgram.js#L225  That function adds a quite long preamble of stuff to every shader.

Ben Humberston [4:50 PM] 
If it helps, you also might add a `debugger` line here to see shader code right as it's being sent to the GPU: https://git.autodesk.com/A360/firefly.js/blob/develop/src/wgs.js/render/WebGLShader.js#L15



Grab the viewer source file from the Autodesk server, save to a local file and explore in depth:

- [viewer3D.js](https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.js?v=v2.13)
- [wgs.js](https://developer.api.autodesk.com/viewingservice/v1/viewers/wgs.js)

You can try it out live
at [forge-rcdb.autodesk.io/configurator](https://forge-rcdb.autodesk.io/configurator)
&gt; [Fader](https://forge-rcdb.autodesk.io/configurator?id=58dd255f57ecef9ad46149f6)


<pre class="prettyprint">
</pre>



The code above is included in
the [ForgeFader](https://github.com/jeremytammik/forgefader) project
in [release 0.0.16](https://github.com/jeremytammik/forgefader/releases/tag/0.0.16) and later.



