<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

http://thebuildingcoder.typepad.com/blog/2014/04/determining-the-size-and-location-of-viewports-on-a-sheet.html#comment-3045289101

 #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

&ndash; 
...

-->

### Parameter Definition Basics

We have repeatedly discussed all kinds of different aspects
of [Revit element parameters](http://thebuildingcoder.typepad.com/blog/parameters),
but not yet put together a
comprehensive [topic group](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5) for them.

Today I am happy to present a pretty comprehensive overview and explanation of the process of definig a shared parameter by none less than Scott Conover himself, Senior Revit Engineering Manager:


**Question:** What do I need to do to programmatically create a shared parameter?
I would like to set the `SetAllowVaryBetweenGroups` flag on it. 


**Answer:** You create the details needed to define a shared parameter from `ExternalDefinition`.
Existing shared parameter file entries can be read to become an `ExternalDefinition` in your code, or you can create a new entry in the current shared parameter file using the `DefinitionGroup.Create` method.

The sample code listed in the Revit API help file `RevitAPI.chm` or the online Revit API docs
under [InstanceBinding class](http://www.revitapidocs.com/2017/7978cb57-0a48-489e-2c8f-116fa2561437.htm) shows
this process best.

Here is part of that sample snippet:

<pre class="code">
public bool SetNewParameterToInstanceWall(
  UIApplication app,
  DefinitionFile myDefinitionFile)
{
  // Create a new group in the shared parameters file
  DefinitionGroups myGroups = myDefinitionFile.Groups;
  DefinitionGroup myGroup = myGroups.Create("MyParameters");

  // Create an instance definition in definition group MyParameters
  ExternalDefinitionCreationOptions option
    = new ExternalDefinitionCreationOptions(
      "Instance_ProductDate", ParameterType.Text);
      
  // Don't let the user modify the value, only the API
  option.UserModifiable = false;
  
  // Set tooltip
  option.Description = "Wall product date";
  Definition myDefinition_ProductDate
    = myGroup.Definitions.Create(option);

  . . .
</pre>

The return from `DefinitionGroup.Create` is an `ExternalDefinition`, even though the type declared is the parent class.
 
Once you have an `ExternalDefinition`, you add it to the document.

There are several ways:
 
1. Use `InstanceBinding` as shown in that sample.
2. Use `FamilyManager.AddParameter` to add the parameter to a family.
3. Use `FamilyManager.ReplaceParameter` to replace a family parameter with the shared one.
4. Use `SharedParameterElement.Create` to create the element that represents the parameter without binding it to any categories.
 
There are also some `RebarShape` related utilities which I would not recommend for general usage but might be OK for rebar-specific code.
 
Once the parameter is in the document, it has an `InternalDefinition`.

The best ways to get it:
 
1. If you have the `ParameterElement` from #4 you can use `ParameterElement.GetDefinition`.
2. If you have the GUID (which you should, since it is provided by the `ExternalDefinition`), you can use `SharedParameterElement.Lookup` followed by `ParameterElement.GetDefinition`.
3. If you have an instance of an element whose category has this parameter bound, get the `Parameter` and use `Parameter.Definition`.
 
Once you have the `InternalDefinition`, you can access the vary across groups option as well as other things.
You can also use an `InternalDefintion` for adding and removing `InstanceBindings` to categories.

Many thanks to Scott for this nice comprehensive summary and overview!
