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

- determine is family system or not?
https://autodesk.slack.com/archives/C0SR6NAP8/p1691697108465579
[Q] Given a Family in the API what is the most straight forward approach to determining if the family is a system family or not in C#?
[A] Jacob Small
Would be curious to know others thoughts on this, as I haven't needed this myself, but I would try to use a combination of the IsEditable and IsInPlace properties.
IsEditable will let you know if the family can be saved, which means it isn't a system family. It would however return a false positive for InPlace families.
IsInPlace will cover the in place families.
revitapidocs.comrevitapidocs.com
IsEditable Property
IsInPlace Property
B: I've also heard:
that built-in families (system families) are not stored as FamilyInstance objects. So you can do a FEC.WhereElementIsNotElementType().OfClass(typeof(FamilyInstance)) to get non-system family instances. The remainder of the objects which were not returned by the filter should then be system families.
&&
System families do not have MaterialQuantities:
doc.Settings.Categories.Cast<Category>().Where(x => x.CategoryType == CategoryType.Model).Where(x => x.HasMaterialQuantities).OrderBy(x => x.Name);
I can't personally decipher which is better (or accurate) as I'm new to both Revit + Revit API, so determining this has been a hurdle. . . curious to hear others thoughts on this
[A] The best approach is the first, what Jacob mentioned. That is the correct one.
Relying on FamilyInstance would only mean you are also assuming all families you are interested in have placed an instance. And is otherwise a hacky solution as well.
regarding the second suggestion: as you realized, your question makes little sense. If you have a Family object, it is a Family and that is that. "System Families" are just a concept, but such objects or C++/C# classes do not exist.
The only nuances, as pointed above, are is it in-place and is it "editable" (meaning not ancient and not some hacky internal concoction).
The only meaningful related question that I can think of is: "Given an ElementType, is it of a 'system family' or of a real, user-made Family?"
To answer that, you just try to cast it to a FamilySymbol. If it fails, then it is of "system family". If it succeeds, then you can get he Family property, which is a Family object that you can further interrogate for in-place, editability, etc.

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


