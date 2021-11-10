<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- REVIT-184115 [Prepare KB article for 2022.1 API change]

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash;
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### WallCrossSection Renaming in the Revit 2022.1 API

####<a name="2"></a> WallCrossSection versus WallCrossSectionDefinition


We mentioned the unfortunate breaking change inadvertently introduced with the Revit 2022.1 API update
by [renaming `WallCrossSection` to `WallCrossSectionDefinition`](https://thebuildingcoder.typepad.com/blog/2021/11/revit-20221-sdk-revitlookup-build-and-install.html#3) and
suggested a fix for the `BuiltInParameterGroup` enumeration value.

Here is the workaround suggested by the development team to also address the `ForgeTypeId` modification to support both versions of the API:

As you know from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit API 2022.1 update change `WallCrossSection` to `WallCrossSectionDefinition`](https://forums.autodesk.com/t5/revit-api-forum/revitapi-2022-update-change-wallcrosssection-to/td-p/10720345),
there was a breaking change introduced in Revit 2022.1:

- `BuiltInParameterGroup.PG_WALL_CROSS_SECTION`
- `ForgeTypeId.WallCrossSection`

were renamed to 

- `BuiltInParameterGroup.PG_WALL_CROSS_SECTION_DEFINITION`
- `ForgeTypeId.WallCrossSectionDefinition`

respectively.

A solution for the first name change in the enum value was already suggested in the forum discussion thread:

The actual integer value can be used instead to define a constant like this:

<pre class="code">
  var PG_WALL_CROSS_SECTION = (BuiltInParameterGroup)(-5000228);
</pre>

This value be used in both Revit 2022.0 and Revit 2022.1 without causing the problem.

A workaround for the second rename, the `WallCrossSection` property of the `ForgeTypeId` class, can be implemented using Reflection in all .NET languages.

Here is a sample code snippet in C#:

<pre class="code">
  using System.Reflection;

  ForgeTypeId id = new ForgeTypeId();            
  Type type = typeof(GroupTypeId);
  
  PropertyInfo propOld = type.GetProperty("WallCrossSection",
    BindingFlags.Public | BindingFlags.Static);
    
  if(null != propOld)
  {
    id = (ForgeTypeId)propOld.GetValue(null, null);
  }
  else
  {
    PropertyInfo propNew = type.GetProperty("WallCrossSectionDefinition", BindingFlags.Public | BindingFlags.Static); 
    id = (ForgeTypeId)propNew.GetValue(null, null);
  }
</pre>

We tested it here, and it works for both Revit 2022.0 and Revit 2022.1.
  
<center>
<img src="img/tech-comics-non-breaking-change.jpeg" alt="Non-breaking change" title="Non-breaking change" width="300"/> <!-- 495 -->
<p style="font-size: 80%; font-style:italic">Non-breaking change &ndash; &copy; [Datamation](https://www.datamation.com)</p>
</center>
