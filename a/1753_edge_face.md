<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

</head>

<!---

- HOK Mission Control .NET
  https://github.com/HOKGroup/HOK-Revit-Addins/tree/master/HOK.MissionControl
  archi-lab
  [playing with Power Shell commands and Post Build events](http://archi-lab.net/playing-with-power-shell-commands-and-post-build-events)
  BY KONRAD K SOBON

  [code signing of your revit plug-ins](http://archi-lab.net/code-signing-of-your-revit-plug-ins)

- edges and faces
  - create connectors on reference line
    https://forums.autodesk.com/t5/revit-api-forum/creating-connectors-on-reference-line/m-p/8707761
  
twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

-->

### Secret Edges and Faces


####<a name="2"></a> 

**Question:** 



**Answer:** 
 
####<a name="3"></a> Creating Connectors on a Reference Line

[MarryTookMyCoffe](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3894260) points
out a surprising and useful possibility 
for [creating connectors on a reference line](https://forums.autodesk.com/t5/revit-api-forum/creating-connectors-on-reference-line/m-p/8707761):

I made some tests and thought I will share some of my results with the forum:

The problem with putting connectors on a reference line is that the API doesn’t give us any references to any surfaces, no matter for what view we ask.

There is however a way to get around this one by by adding a secret suffix to the unique id and calling the `ParseFromStableRepresentation` method on it:

<pre>
  Reference.ParseFromStableRepresentation(
    document, uniqueID );
</pre>

With the right secret suffix, this can be used to retrieve a reference to a face on the reference line.

With lots of trial and error, we learned that the codes for specific faces will be always the same:

<center>
<img src="img/create_connector_on_reference_line.png" alt="Creating connectors on a reference line" width="770">
</center>

For example, let's look at this stable representation:

<pre>
  3ded2a48-367f-42c7-83bd-9fd4f659891a-00000fd0:2
</pre>

Here are the possibile secret suffixes in a table:

<table>
<tr><td style="text-align: right">3ded2a48-367f-42c7-83bd&nbsp;&nbsp;&nbsp;<br/>-9fd4f659891a-00000fd0&nbsp;&nbsp;&nbsp;</td><td>The Reference Line<br/>itself</td></tr>
<tr><td style="text-align: right">:0&nbsp;&nbsp;&nbsp;</td><td>ref to line</td></tr>
<tr><td style="text-align: right">:1&nbsp;&nbsp;&nbsp;</td><td>ref to solid</td></tr>
<tr><td style="text-align: right">:2&nbsp;&nbsp;&nbsp;</td><td>ref to Face</td></tr>
<tr><td style="text-align: right">:7&nbsp;&nbsp;&nbsp;</td><td>ref to Face</td></tr>
<tr><td style="text-align: right">:12&nbsp;&nbsp;&nbsp;</td><td>ref to Face</td></tr>
<tr><td style="text-align: right">:17&nbsp;&nbsp;&nbsp;</td><td>ref to Face</td></tr>
</table>

The gaps between the numbers may seems strange, but I think that after every face, we have hidden edges of this face; what would explain the numeration.

[Q] Does that apply to each and every reference line?

[A] Yes, it applies to every reference line, always the same numbers.

[Q] So each reference line implicitly defines a solid and four faces?

[A] Faces are in the solid, but yes.

[Q] What solid is that?

[A] Similar to an extrusion, you can get a solid of the object with faces and lines, with the only difference that its faces don't have references. I guess the programmer forgot to set up this property.

[Q] Can you share a concrete use case for this?

[A] I use reference line with connectors in all fittings and accessories; that enables my family definition to work with minimal graphics. That way, you can easily define a family with low details (that is important in GB) and high details (that is important in Russia), without messing up how they work.

I plan to go even further and implement an auto-creation tool for fittings, accessories, and mechanical equipment. 

The idea was to add to any family made on the market the same functionally that our families have &ndash; all you had to do was set length and type of connection, like: thread, pressed etc.

Very many thanks to MarryTookMyCoffe for sharing this valuable insight!

<center>
<i>once downhill, once uphill, but always on foot.</i>
</center>

<pre class="code">
</pre>

