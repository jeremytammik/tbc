<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</head>

<!---

twitter:

Comparing Symbols and Comparison Operators in the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon http://bit.ly/comparesymbol

Family symbols, aka family types, should normally be relied on to be constant.
However, since families and types can actually be edited at will, they are sometimes not.
Hence, the need to check and compare may arise...

&ndash; 
...

linkedin:

Comparison operators and comparing symbols in the #RevitAPI

http://bit.ly/comparesymbol

Family symbols, aka family types, should normally be relied on to be constant.
However, since families and types can actually be edited at will, they are sometimes not.
Hence, the need to check and compare may arise...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<p style="font-size: 80%; font-style:italic"></p>

-->

### Comparing Symbols and Comparison Operators

I am writing this in the train station, waiting for a train to Paris, to join the Forge accelerator there this week.

Unfortunately, my originally booked train was cancelled due to the strikes currently taking place in France, and I am stranded here.

Luckily, I was able to get a ticket for the single remaining train today, two hours later.

Now I hope I will make it over there today.

Once I arrive, if I arrive, I will probably start worrying about whether I will be able to get back at all...

Anyway, returning to the Revit API:

####<a name="2"></a> Comparing Family Types

Family symbols, aka family types, should normally be relied on to be constant.

However, since families and types can actually be edited at will, they are sometimes not.

Hence, the need to check and compare may arise.

**Question:** I have two different models for two different projects.

How can I detect that a specific `FamilySymbol` is the same in both of them?

Currently, I just compare the three `Name` properties of the `FamilySymbol`, `Family` and `FamilyCategory`.

Is there a better way?

As one would expect, the `FamilySymbol` has different values for `UniqueId` and `Id` in the two projects.

**Answer:** In a perfect world, it would indeed be sufficient to compare the three properties you mention and nothing else.

I would say category first, then family, then family symbol name.

If they match, the symbols should be identical.

However, in each project, it is theoretically possible for the user to edit and modify the underlying symbol.

In that case, even if the names match, the underlying symbols may still differ.

I assume this is what you would like to detect and rectify.

To test that, you need to drill further down into other, more specific properties: everything that is of interest to you.

####<a name="3"></a> Comparison Operators

If you are doing this programmatically, the cleanest, easiest and most effective method is to implement a comparison operator or a .NET comparison class.

In the comparison operator, you can compare absolutely anything you like.

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

Furthermore, when comparing objects, .NET differentiates between equality operators and sorting comparison operators.

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

We recently discussed a more complex `IComparer` implementation to compare column marks, `ColumnMarkComparer`, 
for [replicating schedule sort order](https://thebuildingcoder.typepad.com/blog/2019/11/dll-conflicts-and-replicating-schedule-sort-order.html#4).

That shows how you can concatenate any number of different comparisons for all the different properties of interest to get a finer and finer distinguishing capability.

You need to decide exactly what differences may occur between the potentially different family instances.

With that in hand, you can implement a nice clean comparison operator for them and run that over all occurrences in all projects to ensure that all the symbols really are identical if their category name, family name and type name match.

<center>
<img src="img/cartographic_symbols.jpg" alt="Cartographic symbols" width="300"> <!--598-->
<p style="font-size: 80%; font-style:italic">Cartographic symbols</p>
</center>
