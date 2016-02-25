<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

Procedural Modelling and Z3 Constraint Solving in Revit via F# Mantis
F# Mantis Procedural Modelling and Z3 Constraint Solving #revitapi #fsharp #3dwebcoder #dynamobim #bim #aec
#adsk
#restapi #python
#adskdevnetwrk #dotnet #csharp
#geometry
#grevit
#responsivedesign #typepad
#ah8 #augi #au2015 #dotnet #dynamobim
#stingray #adsklabs #cloud #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce

#Markdown, #Fusion360 #Fusion360Hackathon, Revisions and Bulk Upgrade #revitapi #adsk #3dwebcoder #aec #bim

Revit API, Jeremy Tammik, akn_include

F# Mantis Procedural Modelling and Z3 Constraint Solving:
Matthew Moloney presents the potential of interactive F# coding in Revit, like the Tsunami Rhino plugin, with a number of improvements, similar to the Revit Python Shell but for the F# programming language, bringing a number of advantages including full code completion, error checking, performance, design scalability and access to powerful procedural modelling and constraint solving tools...

-->

### F# Procedural Modelling and Z3 Constraint Solving

Matthew [@moloneymb](https://twitter.com/moloneymb) Moloney is excited by the potential of
[interactive F# coding in Revit](https://twitter.com/moloneymb/status/638497862330941440).

The current implementation consists of a Revit add-in, like the Tsunami Rhino plugin, with a number of improvements.

Mantis is similar to the Revit Python Shell but for the F# programming language. This brings with it a number of specific advantages including full code completion, error checking, performance, and design scalability.

<center>
<img src="img/fs_mantis.jpg" alt="Interactive F# Mantis shell in Revit" width="500"/>
</center>

You can I try it out yourself in [beta](https://gist.github.com/moloneymb/5e5608c129337cfefa40).

Let Matt know if you need any help getting started.

Feedback would be greatly appreciated.

The use of functionally generated content for structures opens the door to a wonderful world of potential, freeing up the designer from the mundane while still being able to scale to complex models that Visual BIM systems have such a hard time with.

Says Matt:

> If something like Mantis helps, I'd like to continue working in the space. I have a ton of additional ideas for things to build on top of it &ndash; e.g., procedural generation libraries, the Z3 constraint solver, using type providers to import product catalogues, etc. I can even do an Rx Observable graph to update objects based on UI controls which would allow for interactive / live updates similar to the Dynamo experience. Instead of embedding code in a graph you embed the graph in the code and only visualize the UI inputs if and when you need them. It would be a code first approach. All of this is possible right out of the box with Mantis. I just need to explain and give demos on how it can be done :)

> This is an experiment to see if advanced language techniques is something architects actually want. I can build stuff just fine a vacuum by myself but there is little point to that :) I'm not in the business of building buildings so I can't make use of it myself. Much better to be helping other people.

> Which means that I need feedback on the basics before I can justify investing more time into it. If it doesn't look like it will get much traction then I'll focus on something else.

> It is ideal for users:
>
> - Currently using Python
> - Using the Edit and Continue feature in Visual Studio to avoid restarting Revit
> - Wanting to automate more of their workflow

More background information from Matt:

#### <a name="2"></a>Examples in Procedural Modelling

Most of these approaches use an external domain-specific language ([DSL](https://en.wikipedia.org/wiki/Domain-specific_language)) that gets interpreted. External DSLs are hard to extend, domain specialise, and compose - things that you almost always want to do. For example, you could take a building DSL and an arch DSL one to make a gothic building DSL that uses lots of archways.  Using a functional programming language you can use internal DSLs that are trivial to extend, compose, and domain specialise to your specific domain as it is all written in the same language.

- [Procedural Building in Unreal Engine 4.0](https://www.youtube.com/watch?v=KENm7IsOlCw)

#### <a name="3"></a>Z3 Constraint Solver

The [Z3 Constraint Solver](https://github.com/Z3Prover/z3) is mostly only used by academics and there is not much information out there on how to use it in other domains. In effect, it is an efficient way to automate a set of tedious tasks that would normally take far too long for a computer to figure out but is now way faster due to all the great research done in generalised solvers. Solvers used to be poor quality and very expensive but Microsoft recently made one of the best solvers available for free as open source. An example application would be to build your own routing algorithm for industrial piping that could take your specific inputs into account.  E.g. available parts, budget, safety, and regulations. If you move a tank you can just rerun the route function and the rest is automatic.

- [Procedural Modelling of Structurally Sound Masonry Buildings](https://www.youtube.com/watch?v=zXBAthLSxSQ), presented at SIGGRAPH Asia 2009, using Procedural Modelling, a solver, and a simulator to great effect.
- [SolidWorks Routing](https://www.youtube.com/watch?v=4pGyLPxangY)

#### <a name="4"></a>Type Provider Product catalogue

Instead of going to a website and manually doing a search for a component you can simply find and reference the model directly in the code as if you already had it and wrapped it in an API. For instance, instead of downloading and managing shared folders of models and then referencing the models in functions via error prone path strings the type provider will do the fetching, caching, and loading the model behind the scenes. The API for this is exposed via static members e.g. Catalog.Kitchen.Sink2. If Sink3 is later added to the catalogue it's easy to change to the new sink, as it will appear as another static parameter. If Sink2 is later removed from the online catalogue (because it is no longer made) then the static member will cease to exist and the code will no longer compile.

Example Type Provider:

- [Type Provider for the World Bank Data Catalogue](https://www.youtube.com/watch?v=7r2-B-5H_io)
