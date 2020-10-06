<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- high-performance optimisation using Revit API for outline for many elements (>100'000 items)
  e.g., for 54000 walls (after filtering) and 18000 pipes, leading to 972'000'000 operation.
  How can I get bounding box for several elements?
  https://stackoverflow.com/questions/63083938/revit-api-how-can-i-get-bounding-box-for-several-elements
  how to get element nearby specific element using bounding box if it is just outside(some distance apart) bounding box?
  https://forums.autodesk.com/t5/revit-api-forum/how-to-get-element-nearby-specific-element-using-bounding-box-if/m-p/9741883

- set the clipped/unclipped state of the base and survey points in 2021.1
  https://autodesk.slack.com/archives/C0SR6NAP8/p1600379512087800
  Jacob Small:mega-thinking: 17 Sep at 23:51
Can anyone in Dev confirm this statement is still true?
The clipped/unclipped state of the base and survey points cannot be set via the API. You can pin them using the Element.Pinned property.
https://thebuildingcoder.typepad.com/blog/2012/11/survey-and-project-base-point.html
Jennifer (Xue) Li  5 days ago
We exposed a new property Clipped for Base Point in R2021.1. So starting from this version, you will have the ability to get/set clipped state for Survey Point. And for Project Base Point, the property is readonly and will always return false because we’ve removed the clipped state fro PBP.
:celebrate:
Jacob Small:mega-thinking:  5 days ago
Yay!!!!! Huge help thanks!

- BIM360 apps from German university startups now live
  [15 New Integrations with Autodesk Construction Cloud ](https://constructionblog.autodesk.com/15-integrations-autodesk-construction)
  two Startups from the Forge developer Universities:
  Gamma AR &ndash; RWTH Aachen 
  4d planner &ndash; TU Berlin 
  https://twitter.com/ADSK_Construct/status/1311699100312666113

- [Inventing Virtual Meetings of Tomorrow with NVIDIA AI Research](https://youtu.be/NqmMnjJ6GEg)
  [Nvidia Maxine Cloud-AI Video-Streaming Platform](https://developer.nvidia.com/maxine)
New AI breakthroughs in NVIDIA Maxine, cloud-native video streaming AI SDK, slash bandwidth use while make it possible to re-animate faces, correct gaze and animate characters for immersive and engaging meetings. Learn more: https://nvda.ws/3l9foIn
AI-based face recognition and reconstruction is used, enabling bandwidth reduction by transmitting only animated face keypoint data instead of the entire video keyframe information.

twitter:

with the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### High Performance Outline for many Elements


####<a name="2"></a> High-Performance Optimisation using Revit API for Outline for many Elements 

If you are interested in using the Revit AAPI in the most performant manner, you ought to check out the StackOverflow discussion
on [How to get the bounding box for several elements](https://stackoverflow.com/questions/63083938/revit-api-how-can-i-get-bounding-box-for-several-elements).

**Question:** I need to find an outline for many elements (>100'000 items).
Target elements come from a `FilteredElementCollector`.
As usual, I'm looking for the fastest possible way. 

For now, I tried to iterate over all elements to get its `BoudingBox.Min` and `BoudingBox.Max` and find out `minX`, `minY`, `minZ`, `maxX`, `maxY`, `maxZ`.
It works pretty accurately but takes too much time.

The problem described above is a part of a bigger one:

I need to find all the intersections of ducts, pipes and other curve-based elements from a link model with walls, ceilings, columns, etc. in the general model and then place openings in a intersection.

I tried to use a combination of `ElementIntersectElement` filter and ` IntersectSolidAndCurve` method to find a part of curve inside element.

First, with an `ElementIntersectElement`, I tried to reduce a collection for further use of `IntersectSolidAndCurve`.

`IntersectSolidAndCurve` takes two arguments, solid and curve, and has to work in two nested one in the other loops.
So, it takes for 54000 walls (after filtering) and 18000 pipes, in my case, 972'000'000 operations.

With the number of operations 10 ^ 5, the algorithm shows an acceptable time.

I decided to reduce the number of elements by dividing the search areas by levels.
This works well for high-rise buildings, but is still bad for extended low structures.
I decided to divide the building by length, but I did not find a method that finds boundaries for several elements (the whole building).

I seem to go in a wrong way. Is there are right way to make it with revit api instrument

**Answer:** In principle, what you describe is the proper approach and the only way to do it.

However, there may be many possibilities to optimise your code.
The Building Coder provides various utility functions that may help.
For instance, to [determine the bounding box of an entire family](https://thebuildingcoder.typepad.com/blog/2017/03/family-bounding-box-and-aec-hackathon-munich.html#3).
Many more in [The Building Coder samples `Util` module](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/Util.cs).
Search there for "bounding box".
I am sure they can be further optimised as well for your case.
For instance, you may be able to extract all the `X` coordinates from all the individual elements' bounding box `Max` values and use a generic `Max` function to determine their maximum in one single call instead of comparing them one by one.
[Benchmark your code](https://thebuildingcoder.typepad.com/blog/2012/01/timer-code-for-benchmarking.html) to discover optimisation possibilities and analyse their effect on the performance. 

**Response:** Thanks to Jeremy for advice and input on this issue.
I published my final result below and did some research on performance and accuracy.
The code in my answer processed in 3-5 seconds / 100'000 elements and works accurate in most cases.
However, there are cases where the `BoundingBoxIntersectsFilter` filters the item when it does not cross the `Outline`.
This happens if there is invisible geometry in the family.
There are other possible reasons that I have not yet found. More tests need to be done anyway.

**Answer:** Thank you very much for your appreciation and sharing your interesting code.
Using the built-in Revit filtering mechanisms will definitely be a lot faster than anything you can achieve in .NET, outside Revit memory.
However, I do not yet understand how you can use this to achieve the goal you describe above.
I thought you needed the collective bounding box of all elements.
You, however, seem to have an input variable of 500 meters and o be checking whether that contains all the elements.
Can you please explain the exact use of this algorithm, and the exact input and output data?

**Response:** Jeremy, you're absolutely right.
The minimum and maximum points in 3d-space are fed to the input to the method, so that all the elements are inside the outline built on that points, then we find the extreme points along `x` `y` `z`.
The output is 2 outline points. 

####<a name="2.1"></a> Solution

To find boundaries, we can take advantage of the binary search idea. 

The difference from the classic binary search algorithm is there is not an array, and we should find two numbers instead of one.

Elements in Geometry space could be presented as a 3-dimensional sorted array of `XYZ` points. 

The Revit API provides an excellent `Quick Filter`, the `BoundingBoxIntersectsFilter`, that takes an instance of an `Outline`.

So, let’s define an area that includes all the elements for which we want to find the boundaries.
For my case, for example 500 meters, and create `min` and `max` point for the initial outline:

```
  double b = 500000 / 304.8;
  XYZ min = new XYZ(-b, -b, -b);
  XYZ max = new XYZ(b, b, b);
```

Below is an implementation for one direction; you can easily use it for three directions by calling and feeding the result of the previous iteration to the input:

```
  double precision = 10e-6 / 304.8;
  var bb = new BinaryUpperLowerBoundsSearch(doc, precision);

  XYZ[] rx = bb.GetBoundaries(min, max, elems, BinaryUpperLowerBoundsSearch.Direction.X);
  rx = bb.GetBoundaries(rx[0], rx[1], elems, BinaryUpperLowerBoundsSearch.Direction.Y);
  rx = bb.GetBoundaries(rx[0], rx[1], elems, BinaryUpperLowerBoundsSearch.Direction.Z);
```

The `GetBoundaries` method returns two `XYZ` points: lower and upper, which change only in the target direction; the other two dimensions remain unchanged:

```
  public class BinaryUpperLowerBoundsSearch
  {
    private Document doc;

    private double tolerance;
    private XYZ min;
    private XYZ max;
    private XYZ direction;

    public BinaryUpperLowerBoundsSearch(Document document, double precision)
    {
      doc = document;
      this.tolerance = precision;
    }

    public enum Direction
    {
      X,
      Y,
      Z
    }

    /// <summary>
    /// Searches for an area that completely includes all elements within a given precision.
    /// The minimum and maximum points are used for the initial assessment. 
    /// The outline must contain all elements.
    /// </summary>
    /// <param name="minPoint">The minimum point of the BoundBox used for the first approximation.</param>
    /// <param name="maxPoint">The maximum point of the BoundBox used for the first approximation.</param>
    /// <param name="elements">Set of elements</param>
    /// <param name="axe">The direction along which the boundaries will be searched</param>
    /// <returns>Returns two points: first is the lower bound, second is the upper bound</returns>
    public XYZ[] GetBoundaries( XYZ minPoint, XYZ maxPoint,
      ICollection<ElementId> elements, Direction axe )
    {
      // Since Outline is not derived from an Element class there 
      // is no possibility to apply transformation, so
      // we have use as a possible directions only three vectors of basis
      
      switch (axe)
      {
        case Direction.X:
          direction = XYZ.BasisX;
          break;
        case Direction.Y:
          direction = XYZ.BasisY;
          break;
        case Direction.Z:
          direction = XYZ.BasisZ;
          break;
        default:
          break;
      }

      // Get the lower and upper bounds as a projection on a direction vector
      // Projection is an extention method
      
      double lowerBound = minPoint.Projection(direction);
      double upperBound = maxPoint.Projection(direction);

      // Set the boundary points in the plane perpendicular to the direction vector. 
      // These points are needed to create BoundingBoxIntersectsFilter when IsContainsElements calls.
      
      min = minPoint - lowerBound * direction;
      max = maxPoint - upperBound * direction;

      double[] res = UpperLower(lowerBound, upperBound, elements);
      return new XYZ[2]
      {
        res[0] * direction + min,
        res[1] * direction + max,
      };
    }

    /// <summary>
    /// Check if there are any elements contains in the segment [lower, upper]
    /// </summary>
    /// <returns>True if any elements are in the segment</returns>
    private ICollection<ElementId> IsContainsElements( double lower,
      double upper, ICollection<ElementId> ids )
    {
      var outline = new Outline(min + direction * lower, max + direction * upper);
      return new FilteredElementCollector(doc, ids)
        .WhereElementIsNotElementType()
        .WherePasses(new BoundingBoxIntersectsFilter(outline))
        .ToElementIds();
    }

    private double[] UpperLower( double lower,
      double upper, ICollection<ElementId> ids )
    {
      // Get the Midpoint for segment mid = lower + 0.5 * (upper - lower)
      
      var mid = Midpoint(lower, upper);

      // Сheck if the first segment contains elements
      
      ICollection<ElementId> idsFirst = IsContainsElements(lower, mid, ids);
      bool first = idsFirst.Any();

      // Сheck if the second segment contains elements
      
      ICollection<ElementId> idsSecond = IsContainsElements(mid, upper, ids);
      bool second = idsSecond.Any();

      // If elements are in both segments 
      // then the first segment contains the lower border 
      // and the second contains the upper
      // ---------**|***--------
      
      if (first && second)
      {
        return new double[2]
        {
          Lower(lower, mid, idsFirst),
          Upper(mid, upper, idsSecond),
        };
      }

      // If elements are only in the first segment it contains both borders. 
      // We recursively call the method UpperLower until 
      // the lower border turn out in the first segment and 
      // the upper border is in the second
      // ---*****---|-----------
      
      else if (first && !second)
        return UpperLower(lower, mid, idsFirst);

      // Do the same with the second segment
      // -----------|---*****---
      
      else if (!first && second)
        return UpperLower(mid, upper, idsSecond);

      // Elements are out of the segment
      // ** -----------|----------- **
      
      else
        throw new ArgumentException(
          "Segment does not contains elements. Try to make initial boundaries wider",
          "lower, upper" );
    }

    /// <summary>
    /// Search the lower boundary of a segment containing elements
    /// </summary>
    /// <returns>Lower boundary</returns>
    private double Lower( double lower, double upper,
      ICollection<ElementId> ids)
    {
      // If the boundaries are within tolerance return lower bound
      
      if (IsInTolerance(lower, upper))
        return lower;

      // Get the Midpoint for segment mid = lower + 0.5 * (upper - lower)
      
      var mid = Midpoint(lower, upper);

      // Сheck if the segment contains elements
      
      ICollection<ElementId> idsFirst = IsContainsElements(lower, mid, ids);
      bool first = idsFirst.Any();

      // ---*****---|-----------
      
      if (first)
        return Lower(lower, mid, idsFirst);
        
      // -----------|-----***---
      
      else
        return Lower(mid, upper, ids);
    }

    /// <summary>
    /// Search the upper boundary of a segment containing elements
    /// </summary>
    /// <returns>Upper boundary</returns>
    private double Upper( double lower, double upper,
      ICollection<ElementId> ids )
    {
      // If the boundaries are within tolerance return upper bound
      
      if (IsInTolerance(lower, upper))
        return upper;

      // Get the Midpoint for segment mid = lower + 0.5 * (upper - lower)
      
      var mid = Midpoint(lower, upper);

      // Сheck if the segment contains elements
      
      ICollection<ElementId> idsSecond = IsContainsElements(mid, upper, ids);
      bool second = idsSecond.Any();

      // -----------|----*****--
      
      if (second)
        return Upper(mid, upper, idsSecond);
        
      // ---*****---|-----------
      
      else
        return Upper(lower, mid, ids);
    }

    private double Midpoint(double lower, double upper)
      => lower + 0.5 * (upper - lower);
      
    private bool IsInTolerance(double lower, double upper)
      => upper - lower <= tolerance;
  }
```

`Projection` is an extention method for vector to determine the length of projection of one vector onto another:

```
  public static class PointExt
  {
    public static double Projection( this XYZ vector, XYZ other) =>
      vector.DotProduct(other) / other.GetLength();
  }
```

Many thanks to [Alexey Ovchinnikov](https://stackoverflow.com/users/9958255/alexey-ovchinnikov) for his impressive analysis and research and sharing the useful result.

By the way, for high performance intersection and clipping algorithms, you may want to check
out [Wykobi](https://www.wykobi.com), an

> extremly efficient, robust and simple to use C++ 2D/3D oriented computational geometry library.

<center>
<img src="img/wykobi_segmentint.png" alt="Wykobi segment intersection" title="Wykobi segment intersection" width="568"/> <!-- 568 -->
</center>

####<a name="3"></a> Set Base and Survey Clipped and Unclipped

As we pointed out in the discussion on [survey and project base points](https://thebuildingcoder.typepad.com/blog/2012/11/survey-and-project-base-point.html) in 2012, *the clipped/unclipped state of the base and survey points could not be set via the API. You could pin them using the Element.Pinned property*... back then.

Happily and finally, Revit 2021.1 exposed a new property `Clipped` for the base point,
cf. [Clipped state of BasePoint](https://thebuildingcoder.typepad.com/blog/2020/08/revit-20211-sdk-and-whats-new.html#6.3.2)
in [What's New in the Revit 2021.1 API](https://thebuildingcoder.typepad.com/blog/2020/08/revit-20211-sdk-and-whats-new.html).

So, starting from this version, you have the ability to get and set the clipped state for the Survey Point.
For Project Base Point, the property is read-only and will always return false, since the clipped state has been removed from that.


####<a name="4"></a> Two German Uni BIM360 Construction Cloud Startups

Two innovative BIM360 apps from German Forge developer university startups
are now live, [says](https://twitter.com/ADSK_Construct/status/1311699100312666113)
Phil [@contech101](https://twitter.com/contech101) Mueller, cf.
the [15 new integrations with Autodesk construction cloud](https://constructionblog.autodesk.com/15-integrations-autodesk-construction):

- [Gamma AR](https://construction.autodesk.com/integrations/gamma-ar) &ndash; RWTH Aachen 
- [4D-Planner](https://construction.autodesk.com/integrations/4d-planner) &ndash; TU Berlin 
  

####<a name="5"></a> AI-Based Face Streaming hits the Mainstream

AI-based face recognition and reconstruction is entering the mainstream through
the [Nvidia Maxine Cloud-AI Video-Streaming Platform](https://developer.nvidia.com/maxine).

It aims to drastically reduce video conferencing bandwidth requirements by transmitting only animated face keypoint data instead of the entire video keyframe information, and reconstructing the animated current presenters face based on some initial video data and the face keypoint data.

Check out the two-and-a-half-minute video on [inventing virtual meetings of tomorrow with Nvidia AI research](https://youtu.be/NqmMnjJ6GEg):

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/NqmMnjJ6GEg" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
<center>

> New AI breakthroughs in NVIDIA Maxine, cloud-native video streaming AI SDK, slash bandwidth use while making it possible to re-animate faces, correct gaze and animate characters for immersive and engaging meetings.

