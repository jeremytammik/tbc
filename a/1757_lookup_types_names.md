<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

twitter:

 in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon

&ndash;
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

-->

### RevitLookup Display Family Types and Parameter Definition Names


####<a name="2"></a>

Alexander [@aignatovich](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478) [@CADBIMDeveloper](https://github.com/CADBIMDeveloper) Ignatovich, aka Александр Игнатович,
submitted yet another useful [RevitLookup](https://github.com/jeremytammik/RevitLookup) enhancement
in [pull request #53 &ndash; available values for parameters (`ParameterType.FamilyType`) and `FamilyParameter` titles](https://github.com/jeremytammik/RevitLookup/pull/53).

In his own words:

> I added 2 improvements to the RevitLookup tool.

> The first is about available parameters values for parameters with `ParameterType` == `ParameterType.FamilyType`:

<center>
<img src="img/revitlookup_pull_request_53_1.png" alt="RevitLookup lists family types" width="415">
</center>

> We can retrieve these values using the `Family.GetFamilyTypeParameterValues` method.
The elements are either of class `ElementType` or `NestedFamilyTypeReference`:

<center>
<img src="img/revitlookup_pull_request_53_2.png" alt="RevitLookup lists family types" width="667">
</center>

> The second is very simple: Now the tool shows `FamilyParameter` definition names in the left pane:

<center>
<img src="img/revitlookup_pull_request_53_3.png" alt="RevitLookup lists family types" width="596">
</center>

Yet again many thanks to Alexander for his numerous invaluable contributions!

This enhancement is captured
in [RevitLookup release 2020.0.0.2](https://github.com/jeremytammik/RevitLookup/releases/tag/2020.0.0.2).

<pre class="code">
</pre>



I added this sample code
to [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
in [release 2020.0.146.0](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2020.0.146.0)


####<a name="3"></a>

<pre class="prettyprint">
</pre>
