<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

</head>

<!---

- 10027136 [highlight and tag linked elements]
  http://forums.autodesk.com/t5/revit-api/highlight-and-tag-linked-elements/m-p/5294217
  tagging linked elements can be solved using two different approaches, either via RevitLinkInstance + CreateLinkReference or using the ParseFromStableRepresentation method.

- 15175390 [Tagging Linked Elements using Revit API]
  https://forums.autodesk.com/t5/revit-api-forum/tagging-linked-elements-using-revit-api/m-p/8669001


twitter:

Autodesk show reels, spatial element geometry calculator and Add-In Manager update for the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/spatialgeo2020

&ndash; 
...

linkedin:

 the #RevitAPI #bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

-->

### Tagging a Linked Element


####<a name="2"></a> 


####<a name="3"></a>

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread


**Question:** 

**Answer:** 

**Response:** 


####<a name="4"></a> list all untagged doors

Prasanna Murumkar posted a lot of questions recently.

One led to a nice answer by Naveen Kumar, Developer Technical Services, Autodesk Developer Network 

i want to check whether tag is present on door by api how should i checked?
https://forums.autodesk.com/t5/revit-api-forum/i-want-to-check-whether-tag-is-present-on-door-by-api-how-should/td-p/8532032/jump-to/first-unread-message

how to gets relation of element with its tag or its label?
https://forums.autodesk.com/t5/revit-api-forum/how-to-gets-relation-of-element-with-its-tag-or-its-label/td-p/8602124/jump-to/first-unread-message

how to verify label on element using revit api?
https://forums.autodesk.com/t5/revit-api-forum/how-to-verify-label-on-element-using-revit-api/td-p/8594801/jump-to/first-unread-message

[Q] Can we determine the relationship between a tag and its tagged element?
I can retrieve all independent tags of particular category.
E.g., having 6 doors, I can retrieve the 6 door tags.
Suppose one of door does not have tag. How can I find the particular door lacking a tag?
In other words, how to find relation between element category and element tag category.

[A] try using the below code

this code will highlight the elements which are not taggged

<pre class="code">
IList<ElementId> ElementsWithoutTag = new List<ElementId>();
foreach(Element e in new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Doors).OfClass(typeof(FamilyInstance)))
{
  if(new FilteredElementCollector(doc).OfClass(typeof(IndependentTag)).Cast<IndependentTag>().FirstOrDefault(q=>q.TaggedLocalElementId==e.Id)==null)
  {
    ElementsWithoutTag.Add(e.Id);
  }
}
uidoc.Selection.SetElementIds(ElementsWithoutTag);
uidoc.RefreshActiveView();
</pre>

[A2] aignatovich

To check whether a specific door is untagged, you can find all IndependentTag elements present in the document of OST_DoorTags category and the door elements that they are tagging like this:

<pre class="code">
var collector = new FilteredElementCollector(doc);
var doorTagsIds
  = new HashSet<ElementId>(
    collector.OfClass(typeof(IndependentTag)).OfCategory(BuiltInCategory.OST_DoorTags).OfType<IndependentTag>()
      .Select(x => x.GetTaggedLocalElement()?.Id)
      .Where(x => x != null));
</pre>

Then you can iterate your doors collection and check if doorTagsIds contains door.Id




<center>
<img src="img/.png" alt="" width="100">
</center>


