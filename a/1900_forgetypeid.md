<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- revit 2022 migrate ParameterType
  https://autodesk.slack.com/archives/C0SR6NAP8/p1615984937008800
  https://wiki.autodesk.com/pages/viewpage.action?spaceKey=aeceng&title=Revit+API+Changes+2022
ForgeTypeId how to use https://forums.autodesk.com/t5/revit-api-forum/forgetypeid-how-to-use/td-p/9439210
Revit 2022: ParameterType.Text to ForgeTypeId https://forums.autodesk.com/t5/revit-api-forum/revit-2022-parametertype-text-to-forgetypeid/m-p/10225741
https://forums.autodesk.com/t5/revit-api-forum/revit-2022-parametertype-text-to-forgetypeid/m-p/10227398

- 
https://forums.autodesk.com/t5/revit-api-forum/2022-pdf-exporter-cant-use-quot-sheet-number-quot-parameter/m-p/10220287

- josiah comment
  https://thebuildingcoder.typepad.com/blog/2018/06/multi-targeting-revit-versions-cad-terms-texture-maps.html#comment-5339799009

- VisualStudioRevitAddinWizard 2022
  https://forums.autodesk.com/t5/revit-api-forum/visualstudiorevitaddinwizard-2022/m-p/10233833

- RevitLookup 2022

- https://www.freecodecamp.org/news/common-mistakes-beginning-web-development-students-make/
  5 Mistakes Beginner Web Developers Make – And How to Fix Them
  Avoid spaces in file names
  Respect case sensitivity
  Understand file paths
  Name the default page `index`
  Take a break

twitter:

 #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### Replace Deprecated ParameterType with ForgeTypeId

Revit 2022 has been released and the time has come to migrate to the new version.
Two important new features are changes to the parameter API and a new built-in PDF export functionality.
Some initiual issues with these two have already been discussed:

####<a name="2"></a> Replace Deprecated ParameterType with ForgeTypeId

David Becroft of Autodesk 
and Maxim Stepannikov of BIM Planet, aka Максим Степанников or [architect.bim](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4552025),
helped address 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) question
on [Revit 2022 ParameterType.Text to ForgeTypeId](https://forums.autodesk.com/t5/revit-api-forum/revit-2022-parametertype-text-to-forgetypeid/m-p/10225741):

**Question:** Could somebody please help me out with this conversion from the deprecated `ParameterType` to `ForgeTypeId` for the Revit 2022 API?

The 'old' code has a line like this:

<pre class="code">
if (parameter.Definition.ParameterType == ParameterType.Text)  ....
</pre>

What would be the 2022 equivalent for it?

It seems that the left side may be:

<pre class="code">
if (parameter.Definition.GetDataType() == ????)  ....

but for some reason I cannot find what do I have to use on right side of the operation 😞 There must be something I am overlooking.

**Answer:** To perform this check you need to create instance of ForgeTypeId class. Use one of the SpecTypeId's properties to get value to compare with. In your case (for text parameter) you need Number property:

<pre class="code">
>>> element.Parameter[BuiltInParameter.ALL_MODEL_MARK].Definition.GetSpecTypeId() == SpecTypeId.Number
True
</pre>

 Also take a look at the conversation
 on [how to use `ForgeTypeId`](https://forums.autodesk.com/t5/revit-api-forum/forgetypeid-how-to-use/td-p/9439210).
 
**Response:** Oh dear!
This is super confusing.
I've just checked and parameter definitions that store TEXT have a `SpecTypeId` = `SpecTypeId.Number`.

Now how do you distinguish between actual Numbers and Text?

It looks like the problem is this:

The `Autodesk.Revit.DB.InternalDefinition` class has:

- `ParameterType` &ndash; this can be: Invalid | Text | Integer | Number
- `UnitType` &ndash; seemingly, for Text type parameters, this is `UT_Number`

Now with `ParameterType` becoming obsolete, we have to use `Parameter.GetSpecTypeId` that SEEMINGLY coresponds to the `UnitType` member above, and for Text parameters like `Comment`, it has a value of `SpecTypeId.Number`!

The question is: How can I know if a Parameter is Text or not in Revit 2022 &ndash; without using Definition.ParameterType ?

Same would apply to `YesNo` type parameters... `SpecTypeId` cannot be used to determine if a `ParameterDefinition` is for a `YesNo` type parameter...  

And even more: The ONLY place where I can see if a parameter is a YesNo parameter is in the Parameter.Definition.ParameterType !! If ParameterType is obsolete... how to determine if a parameter is YesNo or something else?

**Answer:** It is a well-known fact that unit type of text is number.
Actually I don't know why &nbsp; :-)
But you definnitely should use the `Number` property.
Each Parameter object also has a `StorageType` property.
In case of a Yes/No parameter, its value is `Integer`.
In case of Text parameter, `String`.
I hope this solves your problem.

**Response:** Well, sorry for my ignorance but seemingly there is still one 'minor' issue left for me to be answered:

<pre class="code">
var option = new ExternalDefinitionCreationOptions("ExampleParamForge", SpecTypeId.XXX???);
var definition = definitionGroup.Definitions.Create(option);
</pre>

Using SpecTypeId.Number creates a Parameter that has the "Type of Parameter" set to Number (obviously!, right?)

How do I create a simple TEXT Parameter definition using the Revit 2022 API?

There must be something here that I am missing.

**Answer:** To create a text parameter, please use `SpecTypeId.String.Text`.

For context, the `ForgeTypeId` properties directly in the `SpecTypeId` class identify the measurable data types, like `SpecTypeId.Length` or `SpecTypeId.Mass`.
The non-measurable data types are organized into nested classes within `SpecTypeId`, like `SpecTypeId.String.Text`, `SpecTypeId.Boolean.YesNo`, `SpecTypeId.Int.Integer`, or `SpecTypeId.Reference.Material`.

Regarding text parameters that report their type as "Number", here's the history:

- Prior to Revit 2021, a `Definition` had a `UnitType` and a `ParameterType`.
  The `UnitType` property was only meaningful for parameters with measurable `ParameterType` values, and a parameter with `ParameterType.Text` would report a meaningless `UnitType.Number` value.
- Revit 2021 deprecated the `UnitType` property and replaced it with the `GetSpecTypeId` method.
  But the behavior remained the same &ndash; a parameter with `ParameterType.Text` would have `GetSpecTypeId` == `SpecTypeId.Number`.
- Revit 2022 deprecated the `ParameterType` property and the `GetSpecTypeId` method, replacing them both with the `GetDataType` method.
  A parameter with `ParameterType.Text` will report `GetDataType()` == `SpecTypeId.String.Text`.
  Side note: The `GetDataType` method can also return a category identifier, indicating a Family Type parameter of that category.

Many thanks to Maxim and David for their clarification!

####<a name="3"></a> 

####<a name="4"></a>

Some other important areas do not appear in this list:

- Built-in PDF exporter
- Deprecation of `ParameterType` and migration to `ForgeTypeId`

####<a name="5"></a> Enhancements

Ceiling and Floor creation
Revit Cloud Model (workshared)
Parameters
Sketch API:  SketchEditScope API, Sketch.GetAllElements, Sketch.OwnerId, Ceiling.SketchId, Opening.SketchId, Wall.SketchId,

####<a name="6"></a> 

<center>
<img src="img/.png" alt="" title="" width="1200"/> <!-- 600 -->
</center>
