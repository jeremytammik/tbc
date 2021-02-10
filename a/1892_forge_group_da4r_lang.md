<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- 8473 [Question about model group for Forge] retrieve and select model group in Forge viewer, cf. select MEP system

- 8492 [Language Settings of DA4R Engine]

- [ScheduleDefinition.GetField method not showing](https://github.com/jeremytammik/RevitLookup/issues/70)

- Naked Mole-Rats Speak in Community Dialects
  https://www.treehugger.com/naked-mole-rats-speak-dialects-5101265
  > Sharing a dialect strengthens cohesiveness in the colony
  > the dialect helps with group solidarity and connection
  > In any social group, including our own, having a rapid way of identifying who belongs to the group and who is excluded is useful for many practical reasons

- van Gogh 360
  https://static.kuula.io/share/79QMS
  https://kuula.co/profile/Mathy147
  https://kuula.co/share/79QMS?fs=1&vr=0&sd=1&thumbs=1&info=1&logo=0
  <script src="https://static.kuula.io/embed.js" data-kuula="https://kuula.co/share/79QMS?fs=1&vr=0&sd=1&thumbs=1&info=1&logo=0" data-width="100%" data-height="640px"></script>
  <iframe width="100%" height="640" style="width: 100%; height: 640px; border: none; max-width: 100%;" frameborder="0" allowfullscreen allow="xr-spatial-tracking; gyroscope; accelerometer" scrolling="no" src="https://kuula.co/share/79QMS?fs=1&vr=0&sd=1&thumbs=1&info=1&logo=0"></iframe>
  https://kuula.co/post/79QMS?fs=1&vr=0&sd=1&thumbs=1&info=1&logo=0


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
<p style="font-size: 80%; font-style:italic">
<a href=""></a>
</p>
</center>

-->

### Model Group in Forge and DA4R Language

Today, let's look at two Forge questions on model groups and the Revit engine language, another RevitLookup enhancement, and, while we're at it, a language related scientific discovery on naked mole-rat dialects:




####<a name="2"></a> Retrieving Revit Model Group in Forge


<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 636 -->
</center>



####<a name="3"></a> Specifying the Revit UI Language in DA4R



####<a name="4"></a> RevitLookup Access to ScheduleDefinition GetField

Following up on his enhancement
enabling [RevitLookup to handle split region offsets](https://thebuildingcoder.typepad.com/blog/2021/02/splits-persona-collector-region-tag-modification.html#4) last
week, Michael [@RevitArkitek](https://github.com/RevitArkitek) Coffey submitted a
new [issue #70 &ndash; `ScheduleDefinition.GetField` method not showing](https://github.com/jeremytammik/RevitLookup/issues/70) and
and the subsequent [pull request #71  adds handler for GetSplitRegionOffsets](), saying:

> The method `ScheduleDefinition.GetField` does not show because it requires an integer index parameter.
A list of ScheduleFields can be returned, named by the index that was used.

> I have this working and can submit a pull request.
I have an issue though, in that there are two `GetField` methods, the other taking in an id.
I have not found a way to filter out the second method, so when viewing the `ScheduleDefinition` properties there will be two `GetField` entries.
If you know of a way to filter out that second method you can let me know or you could add it.
Otherwise, it could be left as is or put on hold.

**Answer:** That sounds great, very useful!

Thank you very much for the offer!

That would require analysing the complete `GetField` method signature.

The two overloads `GetField(Int32)` and `GetField(ScheduleFieldId)` have
different [method signatures](https://www.c-sharpcorner.com/UploadFile/puranindia/method-signatures-in-C-Sharp).

They can be distinguished using by checking their parameter types using .NET Reflection, as explained
in [how to get only methods with a specific signature out of `Type.GetMethods`](https://stackoverflow.com/questions/5152346/get-only-methods-with-specific-signature-out-of-type-getmethods).

The new functionality is captured
in [RevitLookup release 2021.0.0.13](https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.13).


- RevitLookup supports ScheduleDefinition.GetField
  https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.13
  [Q] The method ScheduleDefinition.GetField does not show because it requires an integer index parameter. A list of ScheduleFields can be returned, named by the index that was used.
  I have this working and can submit a pull request. I have an issue though, in that there are two GetField methods, the other taking in an id. I have not found a way to filter out the second method, so when viewing the ScheduleDefinition properties there will be two GetField entries. If you know of a way to filter out that second method you can let me know or you could add it. Otherwise it could be left as is or put on hold.
  [A] That sounds great, very useful!
  Thank you very much for the offer!
  That would require analysing the complete GetField method signature.
  The two overloads GetField(Int32) and GetField(ScheduleFieldId) have different method signatures:
  https://www.c-sharpcorner.com/UploadFile/puranindia/method-signatures-in-C-Sharp/
  They can be distinguished using by checking their parameter types using Reflection, as explained in how to get only methods with a specific signature out of Type.GetMethods:
  https://stackoverflow.com/questions/5152346/get-only-methods-with-specific-signature-out-of-type-getmethods



####<a name="5"></a> Naked Mole-Rats Speak in Community Dialects

A surprising scientific analysis of their vocalisations demonstrates
that [naked mole-rats speak in community dialects](https://www.treehugger.com/naked-mole-rats-speak-dialects-5101265):

> Sharing a dialect strengthens cohesiveness in the colony

> ... helps with group solidarity and connection

> In any social group, including our own, having a rapid way of identifying who belongs to the group and who is excluded is useful for many practical reasons.


####<a name="6"></a> Van Gogh 360

For something not related to programming or science, let's take a moment to simply savour and
enjoy [van Gogh 360](https://static.kuula.io/share/79QMS):

js

<center>
  <script src="https://static.kuula.io/embed.js" data-kuula="https://kuula.co/share/79QMS?fs=1&vr=0&sd=1&thumbs=1&info=1&logo=0" data-width="100%" data-height="640px"></script>
<center>

iframe

<center>
<iframe width="100%" height="640" style="width: 100%; height: 640px; border: none; max-width: 100%;" frameborder="0" allowfullscreen allow="xr-spatial-tracking; gyroscope; accelerometer" scrolling="no" src="https://kuula.co/share/79QMS?fs=1&vr=0&sd=1&thumbs=1&info=1&logo=0"></iframe>
</center>


