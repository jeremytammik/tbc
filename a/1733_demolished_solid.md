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


of [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2019.0.145.4).

-->

### Determine Exact Opening by Demolishing

Here comes the most surprising Revit API functionality I have ever seen, put to a very useful and common task, determining the exact wall opening required for a door or window.

We have looked at numerous different approaches to determine wall openings in the past, including:

- [Opening geometry](http://thebuildingcoder.typepad.com/blog/2012/01/opening-geometry.html)
- [The temporary transaction trick for gross slab data](http://thebuildingcoder.typepad.com/blog/2012/10/the-temporary-transaction-trick-for-gross-slab-data.html)
- [Retrieving wall openings and sorting points](http://thebuildingcoder.typepad.com/blog/2015/12/retrieving-wall-openings-and-sorting-points.html)
- [Wall opening profiles](http://thebuildingcoder.typepad.com/blog/2015/12/wall-opening-profiles-and-happy-holidays.html#3)
- [Determining wall opening areas per room](http://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html#4)
- [More on wall opening areas per room](http://thebuildingcoder.typepad.com/blog/2016/04/more-on-wall-opening-areas-per-room.html)
- [Two energy model types](http://thebuildingcoder.typepad.com/blog/2017/01/family-category-and-two-energy-model-types.html#3)
- [IFC helper returns outer `CurveLoop` of door or window](https://thebuildingcoder.typepad.com/blog/2017/06/copy-local-false-and-ifc-utils-for-wall-openings.html#2)

Now Håvard Leding of [Symetri](https://www.symetri.com) contributed
yet another exciting idea which highlights a number of surprising aspects and demonstrates a further creative use case for `GetDependentElements`, expanding on the
recent [RevitLookup enhancement to retrieve and snoop dependent elements](https://thebuildingcoder.typepad.com/blog/2019/03/retrieving-and-snooping-dependent-elements.html):

toc

In his own words:


#### <a name="2"></a> Get Demolished Solid

Here is another use case for GetDependentElements().
 
Determining the opening dimensions for Doors and Windows is surprisingly difficult, since you can't trust their parameters or reference planes to be consistent.

I suggest this alternative method that uses the solid that Revit creates when you demolish an opening.

It also uses the good
old [temporary transaction trick](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.53):

<pre class="code">
/// <summary>
        /// If you demolish a door Revit will automatically fill the opening with a wall.
        /// So we will use that wall to get opening dimensions.
        /// </summary>
        /// <param name="fi">Door or Window is expected</param>
        /// <returns></returns>
        private static Solid GetDemolishedSolid(FamilyInstance fi)
        {
            Document doc = fi.Document;
            Solid solidOpening = null;
 
            using (Transaction t = new Transaction(doc, "temp"))
            {
 
                if (fi.HasPhases() && fi.ArePhasesModifiable())
                {
                    t.Start();
                    fi.DemolishedPhaseId = fi.CreatedPhaseId;
 
                    fi.Document.Regenerate();
 
                    IList<ElementId> dependents = fi.GetDependentElements(null);
                  
                    foreach (ElementId id in dependents)
                    {
                        Element e = doc.GetElement(id) as Element;
                        if (e is Wall)
                        {
                            GeometryElement geomWall = e.get_Geometry(new Options()) as GeometryElement;
                            //need to clone it as Rollback will destroy the original solid
                            solidOpening = SolidUtils.Clone((Solid)geomWall.First());
                            break;
                        }
                            
                    }
 
                    t.RollBack();
                }
            }
 
            //In the family editor the host wall is always aligned ortho to the family coordinate system
            //so we can get the opening dimensions directly from the bounding box
            Solid solidInFamilyCoordinates = SolidUtils.CreateTransformed(solidOpening, fi.GetTotalTransform().Inverse);
            XYZ dimensions = solidInFamilyCoordinates.GetBoundingBox().Max - solidInFamilyCoordinates.GetBoundingBox().Min;
 
            Autodesk.Revit.UI.TaskDialog.Show("Opening", "Opening is " + dimensions.X.ToString() + " by " + dimensions.Z.ToString());
 
            return solidOpening;
        }
 </pre>
 

#### <a name="3"></a> Why?

I agree some more explanation as to why parameters and refplanes are not to be trusted would be nice.
 
You cant rely on any builtin parameters being in use.
Or used as you expect them to be used.
Same goes for Reference planes.
 
Sometimes its just static geometry. (No parameters in use)
And sometimes its just an opening family. (No solids)
 
Still, you need to analyze something and preferably solids.
But sometimes solids dont represent the true opening, like in this case, where you have a gap:
 
<center>
<img src="img/window_opening_with_gap.jpg" alt="" width="302">
</center>
 
It's just safer and a lot easier to use the final "opening solid" from a demolished state.
 
If you want to try it out yourself, the method is easy enough to use; 
just requires a door or window ro run &nbsp; :-)
 

#### <a name="4"></a> Questions?

**Question:** reading the code in more detail, though, i don't really understand...
 
the family instance fi is a window, for example, yes?
 
from the window, you call GetDependentElements, which returns "all elements that, from a logical point of view, are the children of this Element".
 
from those, you extract the wall.
 
hmm. so the wall is dependent on the window? that seems weird.
 
ok, let's accept that the wall is returned.
 
now, from the wall, you retrieve the first geometry element.
 
that is now saved as the solid opening.
 
that seems super weird. i would have thought that the wall geometry is the wall geometry, and that the hole is a hole.
 
why is the first solid in the wall geometry a solid representing the opening?
 
does that really work?
 
super super weird, it seems to me...
 
can you explain?
 
**Answer:**

Yes, it works; just try it :-)
 
It works, because when you demolish openings, Revit automatically fills the entire hole with a new Wall.
You can see it if you manually demolish a window.
There is no longer a hole in the wall.
 
Very easy to get dimensions from the bounding box, once transformed into the family editor coordinate system.
Because there, the host wall direction is always aligned with the coordinate system cardinal axes.
I guess opening dimensions could be extracted without the transform, but I wouldn't know how to do that.
 
You could also analyze the shape of the opening by looking at the vertical front face of the solid.

You would have to do so to handle cases where the window is not rectangular, e.g., circular or something else.


Many thanks to Håvard for this innovative solution!
