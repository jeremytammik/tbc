<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

RevitLookup Using Reflection Cleanup #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Last week, I presented a drastic change to RevitLookup using <code>Reflection</code> to provide more complete coverage of all the Revit database element methods and properties. Victor Chekalin took a critical look at this new version and cleaned it up significantly. Now all is well and order has been restored again
&ndash; Shock and gripe
&ndash; Commit summary
&ndash; 1. Methods and properties extraction
&ndash; 2. Show enum values
&ndash; 3. Drill down into <code>SymbolGeometry</code>
&ndash; 4. Remove duplicate <code>get_</code> property getter method
&ndash; 5. Fixed <code>Fine</code> <code>DetailLevel</code> bug
&ndash; 6. Visual style of separator
&ndash; Download and access to old functionality
&ndash; Reaction and read-only assurance...

-->

### Template Enhancement with Edit and Continue

Last week, I presented Andray Bushman's templates

Furthermore, we have discussed many aspects of [edit and continue](), including the solution to use the [Add-in Manager]().

These two topics have now met, united and fruitfully multiplied.

Andrey added support for


to [RevitLookup](https://github.com/jeremytammik/RevitLookup) contributed by 


- [](#2)


[commit](https://github.com/Andrey-Bushman/Revit2017AddInTemplateSet/commit/e1a3ceb811717929b5d758cacd41e9f46917758f):

+  * Revit 2017 Class Library (C# Project Template)
+  * Revit 2017 New Class (C# Item Template)
+The `Revit 2017 External Application` contains the special additional configuration - `Debug via Revit Add-In Manager`. It allows to you edit and debug your commands code without Revit restarting. 
+Templates of all projects contain the search engine for searching of the absent assemblies. You can use the `AssemblyResolves.xml` configuration file for managing of where is located your absent assemblies.
+ video tutorial on [Debugging with using the Add-In Manager](https://www.youtube.com/watch?v=QFFwG6rz0gc)

Please refer to the complete documentation to see the full context and the list of other video tutorial on using this set of templates:

[Revit2017AddInTemplateSet](https://github.com/Andrey-Bushman/Revit2017AddInTemplateSet)

#### <a name="2"></a>

#### <a name="3"></a>


#### <a name="4"></a>


#### <a name="5"></a>

