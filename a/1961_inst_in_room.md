<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Get FamilyInstances within The Room
  https://forums.autodesk.com/t5/revit-api-forum/get-familyinstances-within-the-room/td-p/11364696

- another look at fuzz:
  Unit converted parameter value not matching parsed string value
  https://forums.autodesk.com/t5/revit-api-forum/unit-converted-parameter-value-not-matching-parsed-string-value/m-p/11353053
  Comparing double values in C#
  https://stackoverflow.com/questions/1398753/comparing-double-values-in-c-sharp

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

<center>
<img src="img/instances_in_room.png" alt="Family instances in room" title="Family instances in room" width="600"/> <!-- 1258 x 776 -->
</center>

How to filter intersected 2D families also?

This my code:

<pre class="code">
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
  
  ElementIntersectsSolidFilter elementIntersectsSolidFilter
    = new ElementIntersectsSolidFilter(geoObject as Solid);
  
  return new FilteredElementCollector(revitDoc)
        .OfClass(typeof(FamilyInstance))
        .WhereElementIsNotElementType().
        WherePasses(elementIntersectsSolidFilter).
        Cast<FamilyInstance>().
        ToList();
}
</pre>

**Answer:** The `ElementIntersectsSolidFilter` requires the filtered elements to have solid geometry and be of a category supported by interference checking.
Your 2D instances do not fulfil this requirement.

You can try using the family instance `Room` property like this:

<pre class="code">
bool IsInstanceInRoom(FamilyInstance instance, Room room)
{
var isInstanceInRoom = instance.Room != null && instance.Room.Id == room.Id;
return isInstanceInRoom;
}

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
</pre>

Another approach is to use the `Room.IsPointInRoom` predicate and check the family instance location point or constructing some other point based on geometry location.
You may need to elevate it slightly off the floor to ensure it will be found within vertical limits of room.

Many thanks to Sam Berk and
Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas for their helpful advice!

####<a name="3"></a> Again, the Need for Fuzz

We take yet another look at fuzz, required in order to deal with comparison of real numbers on digital computers.

The topic came up once again in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [unit converted parameter value not matching parsed string value](https://forums.autodesk.com/t5/revit-api-forum/unit-converted-parameter-value-not-matching-parsed-string-value/m-p/11353053).

To skip all the jabber and jump right to a solution, please refer to the StackOverflow discussion
on [comparing double values in C#](https://stackoverflow.com/questions/1398753/comparing-double-values-in-c-sharp).

**Question:** Strictly speaking, this is not a Revit API issue I'm facing, but after some research I couldn't find the answer here or anywhere else and it's still a challenge related to Revit.

I'm comparing a family parameter value with values in a spreadsheet so I can update the parameter accordingly if they don't match, but I'm getting weird results when comparing them. The snippet below shows how I'm extracting the double value of the `WW_Width` parameter and converting it to millimetres. Then, I compare it to the a parsed string with the exact same value as a double, but I get a false result to whether or not they match.

<pre class="code">
var famPars = doc.FamilyManager.Parameters;
var famTypes = doc.FamilyManager.Types;

foreach (FamilyParameter param in famPars)
{
	foreach (FamilyType famtype in famTypes)
	{
		if (param.Definition.Name == "WW_Width")
		{
			console.ShowBoldMessage("WW_Width");
			console.ShowMessage($"UnitType: {param.Definition.UnitType}");
			console.ShowMessage($"StorageType: {param.StorageType}\n");

			double valueInMM = UnitUtils.ConvertFromInternalUnits((double)famtype.AsDouble(param),
																	DisplayUnitType.DUT_MILLIMETERS);
			double parsedString = double.Parse("2448");

			console.ShowMessage($"Value: {valueInMM}");
			console.ShowMessage($"Parsed string: {parsedString}");
			console.ShowMessage($"Values match: {valueInMM == parsedString}");
		}
	}
}
</pre>

This is what my console shows as a result; I don't get why I'm getting a mismatch:

<pre class="code">
  WW_Width
  UnitType: UT_Length
  StorageType: Double
  
  Value: 2448
  Parsed string: 2448
  Values match: False
</pre>

**Answer:** You need to add some fuzz; you
can [search The Building Coder for 'fuzz'](https://www.google.com/search?q=fuzz&as_sitesearch=thebuildingcoder.typepad.com).

**Response:** Thanks for the steer in the right direction.
I wasn't aware of that issue with doubles/floats.
Instead of directly comparing the doubles, I'm subtracting one from the other and checking if the difference is under a certain tolerance (e.g. A - B < 0.001), and that works.

The StackOverflow article
on [comparing double values in C#](https://stackoverflow.com/questions/1398753/comparing-double-values-in-c-sharp) explains
it well in case anyone faces this issue in the future.

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

