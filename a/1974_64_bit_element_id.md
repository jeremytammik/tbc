<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---


twitter:

 with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

<pre class="code">
</pre>

-->

### 64-Bit Element Ids



####<a name="2"></a> 

<center>
<img src="img/.png" alt="" title="" width="100"/>  <!--  -->
</center>

####<a name="3"></a> 

**Question:** 

**Answer:** 

####<a name="4"></a> 


Many thanks to  for his work


####<a name="5"></a> Beyond Dynamo: Python manual for Revit

I avoid advertising commercial products, but I made an exception
for [Más Allá de Dynamo](https://thebuildingcoder.typepad.com/blog/2020/12/dynamo-book-and-texture-bitmap-uv-coordinates.html#3),
the Spanish-Language Python manual focused on Dynamo and the Revit API.

It is now also available in English
as [Beyond Dynamo: Python manual for Revit](https://www.amazon.com/dp/B0BMSV6YXD),
by Kevin Himmelreich, Alejandro Martín-Herrer and Ignacio Moreu:

> This is a Python handbook specifically created for working on BIM methodology with Autodesk Dynamo and Revit.
It has a practical approach aimed at professionals who have never programmed before.
If you know how to program in Python, it will be equally useful, as it explains deeply most of the classes, methods and properties of the Revit API.

<center>
<img src="img/beyond_dynamo_en.png" alt="" title="" width="252"/>  <!-- 252 × 330 -->
</center>

Note that we also discussed lots of other resources
for [learning Python and Dynamo](https://thebuildingcoder.typepad.com/blog/2021/02/addin-file-learning-python-and-ifcjs.html#3).



**Response:** 





Ah, ok cool. This is the technical writeup of what changed: 

Here is a writeup of what’s changing and what a developer would need to do about it. @Harlan Brumm – is this ok as-is? I am assuming that since we decided this was worth talking about pre-release that it’s ok to explain the exact API as it stands now. Let me know if you want me to change anything.
 
Diane

We will be changing ElementIds from storing 32-bit integers to storing 64-bit values (type long in C# or Int64 in .NET). This will allow for larger and more complicated models. Most functions which take or return ElementIds will continue to work with no changes. However, there are a few things to keep in mind:
 
If you are storing ElementIds externally as integers, you will need to update your storage to take 64-bit values.
 
The constructor Autodesk.Revit.DB.ElementId(Int32) has been deprecated and replaced by a new constructor, Autodesk.Revit.DB.ElementId(Int64).
                                                                                                                                                                       
The property Autodesk.Revit.DB.ElementId.IntegerValue has been deprecated. It returns only the lowest 32-bits of the ElementId value. Please use the replacement property Autodesk.Revit.DB.ElementId.Value, which will return the entire value.
 
Support has been added for using 64-bit types in ExtensibleStorage. Both Autodesk.Revit.DB.ExtensibleStorage.SchemaBuilder.AddSimpleField() and AddMapField() can now take in 64-bit values for the fieldType, keyType, and valueType parameters.
 
Two binary breaking changes have been made. Both Autodesk.Revit.DB.BuiltInCategory and Autodesk.Revit.DB.BuiltInParameter have been updated so that the size type of the underlying enums are 64-bits instead of 32. Code built against earlier versions of the API may experience type cast and other type related exceptions when run against the next versions of the API when working with the enum. Please rebuild your addins against the next release's API when it is available.
 
Here is a table showing the deprecated and replacement members:
Deprecated API
Replacement
Autodesk.Revit.DB.ElementId(Int32)
Autodesk.Revit.DB.ElementId(Int64)
Autodesk.Revit.DB.ElementId.IntegerValue
Autodesk.Revit.DB.ElementId.Value
 
 