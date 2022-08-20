<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Get FamilyInstances within The Room
  https://forums.autodesk.com/t5/revit-api-forum/get-familyinstances-within-the-room/td-p/11364696

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://autode.sk/bulkinstances

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

### Instances in Room


####<a name="2"></a> Get Family Instances Within Room

A common task is
to [Get FamilyInstances within a room](https://forums.autodesk.com/t5/revit-api-forum/get-familyinstances-within-the-room/td-p/11364696):

**Question:** I'm facing a small issue in filtering family instances within the room.
A few families do not get filtered; for example, non-solid families are getting excluded.
How to filter intersected 2D families also?
Reference Image!!!
arshad99_0-1660745687988.png
 

<center>
<img src="img/instances_in_room.png" alt="Family instances in room" title="Family instances in room" width="600"/> <!-- 1258 x 776 -->
</center>



This my Code!!!
//Get Family Instance
public List<FamilyInstance> GetFamilyInstance(Document revitDoc, Room room)
{
  //Get Closed Shell
  GeometryElement geoEle = room.ClosedShell;
  GeometryObject geoObject = null;
  //Get Geometry Object From Geometry Element
  foreach (GeometryObject obj in geoEle)
  {
      if (obj is Solid)
      {
          geoObject = obj;
      }
  }
  ElementIntersectsSolidFilter elementIntersectsSolidFilter = new ElementIntersectsSolidFilter(geoObject as Solid);
  return new FilteredElementCollector(revitDoc)
        .OfClass(typeof(FamilyInstance))
        .WhereElementIsNotElementType().
        WherePasses(elementIntersectsSolidFilter).
        Cast<FamilyInstance>().
        ToList();
}

SamBerk
Try using the Room property:
public List<FamilyInstance> GetFamilyInstance(Document revitDoc, Room room)
{
var elements = new FilteredElementCollector(revitDoc)
.OfClass(typeof(FamilyInstance))
.WhereElementIsNotElementType()
.Cast<FamilyInstance>()
.Where(i => IsInstanceInRoom(i, room))
.ToList();
return elements;    
}
bool IsInstanceInRoom(FamilyInstance instance, Room room)
{
var isInstanceInRoom = instance.Room != null && instance.Room.Id == room.Id;
return isInstanceInRoom;
}

RPTHOMAS108
ElementIntersectsSolidFilter requires element to have solid geometry and be of category supported by interference checking.
Use either as @SamBerk suggests or Room.IsPointInRoom, by constructing point based on geometry location and elevate it slightly to ensure it will be found within vertical limits of room.

####<a name="3"></a> 

####<a name="4"></a> 

**Question:** 

**Answer:** 

**Response:** 

####<a name="5"></a> 

<pre class="prettyprint">
</pre>

Many thanks to ??? for sharing this useful solution!

####<a name="6"></a> Avoid PDF for On-Screen Reading

I just read a piece of advice that I have not heeded so far, and am unsue whether to change.

However, I am very glad to be well informed about my bad manners.

The recommendation stands
to [avoid PDF for on-screen reading](https://www.nngroup.com/articles/avoid-pdf-onscreen-reading-original).
It was vaced a long time, and apparently still stand,
cf. [PDF: still unfit for human consumption, 20 years later](https://www.nngroup.com/articles/pdf-unfit-for-human-consumption).
In stead, the author recommends
to use HTML gateway pages instead of PDFs,
since [gateway pages prevent pdf shock](https://www.nngroup.com/articles/gateway-pages-prevent-pdf-shock).

I fully agree with the latter, and mostly try to clearly mark links that lead to a PDF so that the unwary reader is prepared for leaving the world of HTML.


