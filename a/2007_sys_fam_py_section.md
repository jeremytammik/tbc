<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- determine is family system or not?
  https://autodesk.slack.com/archives/C0SR6NAP8/p1691697108465579
  [Q] Given a Family in the API what is the most straight forward approach to determining if the family is a system family or not in C#?
  [A] Jacob Small

- you need a column family to define a column family
  Custom Family Type - BuiltIn Parameters
  https://forums.autodesk.com/t5/revit-api-forum/custom-family-type-builtin-parameters/m-p/12203945

- Create section with Revit API, Python
  creates a section that looks at a window from the outside
  https://forums.autodesk.com/t5/revit-api-forum/create-section-with-revit-api-python/m-p/12211534


twitter:

 @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS


&ndash; ...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### System Family Predicate and Python Section Creation


####<a name="2"></a> System Family Predicate

Jacob Small suggests how to identify system families:

**Question:** Given a family, what is the most straight-forward approach to determine programmatically whether it is a system family or not in the C# Revit API?

**Answer:** I would try to use a combination of the `IsEditable` and `IsInPlace` properties.
`IsEditable` will let you know if the family can be saved, which means it isn't a system family.
It would however return a false positive for in-place families.
`IsInPlace` will cover the in-place families:

- [IsEditable](https://www.revitapidocs.com/2024/d7d3ef05-d2bd-b770-47df-96b7fd280f9f.htm)
- [IsInPlace](https://www.revitapidocs.com/2024/eb138fd5-6092-5257-e6e1-073013cb8582.htm

A meaningful related question: Given an `ElementType`, does it specify a system family or a real, user-defined RFA-based family?
To answer that, you can try to cast it to a `FamilySymbol`.
If that fails, it refers to a system family.
If it succeeds, you can read he `Family` property, which is a `Family` object that you can interrogate further for in-place, editability, etc.

####<a name="3"></a> Level-Based Family Requires Apprpriate Stuff

Catergory
Template

- you need a column family to define a column family
Custom Family Type - BuiltIn Parameters
https://forums.autodesk.com/t5/revit-api-forum/custom-family-type-builtin-parameters/m-p/12203945


**Question:**

**Answer:**


####<a name="4"></a>

- Create section with Revit API, Python
  creates a section that looks at a window from the outside
  https://forums.autodesk.com/t5/revit-api-forum/create-section-with-revit-api-python/m-p/12211534


<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 353 Pixel Width: 974 -->
</center>



<pre class="prettyprint">

</pre>

Many thanks to  for raising this important question and sharing his viable and well-considered solution.

####<a name="4"></a>


