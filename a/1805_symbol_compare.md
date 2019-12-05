<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

twitter:

 in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/combiningedges

&ndash; 
...

linkedin:


#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Comparing Symbols


####<a name="2"></a> Comparing Family Symbols and Other Stuff

Family symbols, aka family types, should normally be relied on to be constant.

However, since familes and types can actually be edited at will, they are sometimes not.

Hence, the need to check and compare.

**Question:** I have two different models for two different projects.

How can I detect that a specific `FamilySymbol` is the same in both of them?

Currently, I just compare the three `Name` properties of the `FamilySymbol` `Family` and `FamilyCategory`.

Is there a more reasonable way?

Apparently, as expected, the `FamilySymbol` has a different `UniqueId` and `Id` in the two projects.

**Answer:** If you are doing this programmatically, the cleanest, easiest and most effective method is to implement a comparison operator, or, in .NET, a comparison class.

In the comparison operator, you can compare absolutely anything you like.

For instance, it definitely makes sense to compare the three properties you mention.

I would say category first, then family, then family symbol name.

However, as you say, there may be differences between symbols, even if these three properties are identical.

To test that, you need to drill further down into other, more specific properties: everything that is of interest to you.

One important aspect of comparing objects is that you need to define a canonical form for them.

For instance, if you have two objects with properties, a canonical form should normally be independent of the order of the properties.

For instance, given two objects with the same properties in different order:

<pre>
  A = { “property_1” : “X”,  “property_2” : “Y” }
  B = { “property_2” : “Y”,  “property_1” : “X” }
</pre>

They should be considered equal.

One easy way to achieve an invariant form for a list of properties is to sort them consistently, for instance, alphabetically by property name.

Next, you may want to consider the symbol geometry.

If you are only interested in solid geometry, you might compare simple incomplete aspects or the full-blown thing:

- The number of disjunct solids
- Their volumes
- Their areas
- Their numbers of faces and edges
- Their vertices
- Use a full-blown Boolean operation to determine exact equality

Long ago, I
suggested [defining your own key for comparison purposes](https://thebuildingcoder.typepad.com/blog/2012/03/great-ocean-road-and-creating-your-own-key.html#2).

Here is a simple `XyzEqualityComparer` that shows a pretty trivial equality comparison class for 3D points and vectors:

<pre class="code">
class XyzEqualityComparer : IEqualityComparer<XYZ>
{
  public bool Equals( XYZ p, XYZ q )
  {
    return p.AlmostEqual( q );
  }
 
  public int GetHashCode( XYZ p )
  {
    return Util.PointString( p ).GetHashCode();
  }
}
</pre>

That just answers the question ‘are they equal or not’.

A more useful and powerful comparison method answers the question ‘is one of them smaller than, equal, or larger than the other’.

Such methods are needed for sorting objects, for instance, in order to use them as keys in a dictionary.

I defined a comparison operator for `XYZ` points like this 
for [tracking element modification](https://thebuildingcoder.typepad.com/blog/2016/01/tracking-element-modification.html#5.1):

<pre class="code">
  public static bool IsZero(
    double a,
    double tolerance )
  {
    return tolerance > Math.Abs( a );
  }
 
  public static bool IsZero( double a )
  {
    return IsZero( a, _eps );
  }
 
  public static bool IsEqual( double a, double b )
  {
    return IsZero( b - a );
  }
 
  public static int Compare( double a, double b )
  {
    return IsEqual( a, b ) ? 0 : ( a < b ? -1 : 1 );
  }
 
  public static int Compare( XYZ p, XYZ q )
  {
    int d = Compare( p.X, q.X );
 
    if( 0 == d )
    {
      d = Compare( p.Y, q.Y );
 
      if( 0 == d )
      {
        d = Compare( p.Z, q.Z );
      }
    }
    return d;
  }
</pre>

As you see, you start from the most basic property data types, e.g., int, double, string, and then build up further and further to achieve all you need.

We recently discussed a more complex `IComparer` implementation for comparing column marks, `ColumnMarkComparer`, 
for [replicating schedule sort order](https://thebuildingcoder.typepad.com/blog/2019/11/dll-conflicts-and-replicating-schedule-sort-order.html#4).

That shows how you can concatenate any number of different comparisons for all the different properties of interest to get a finer and finer distinguishing capability.

You need to decide exactly what differences may occur between the potentiaslly different family instances.

With thast in hand, you can implement a nice clean comparison operator for them and run that over all occurrernces in all projects to ensure that all the symbols really are identical if their category name, family name and type name match.



####<a name="3"></a> 

####<a name="4"></a> 

####<a name="5"></a> 

<center>
<img src="img/.png" alt="" width="100"> <!--680-->
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Answer:** Two steps:

**Response:** 

<pre class="code">
</pre>

Many thanks to  for raising and solving this interesting task.
