<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Equality methods of GeometryObject
  https://forums.autodesk.com/t5/revit-api-forum/equality-methods-of-geometryobject/m-p/12155610

- on single threaded API versus multi-threaded Revit
  Is my plugin restricted by the computing resources of Revit?
  https://forums.autodesk.com/t5/revit-api-forum/is-my-plugin-restricted-by-the-computing-resources-of-revit/m-p/12155865#M73273

twitter:

Deep Revit API questions requiring clarification on add-in threads, GeometryObject comparison methods and accessing MS Access in the @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS https://autode.sk/objequals

Deep Revit API questions requiring clarification and a simple database connection issue resolved
&ndash; Add-in threads
&ndash; <code>GeometryObject</code> comparison methods
&ndash; Accessing Access
&ndash; The INTERCAL programming language...

linkedin:

Deep Revit API questions requiring clarification on add-in threads, GeometryObject comparison methods and accessing MS Access in the #RevitAPI

https://autode.sk/objequals

- Add-in threads
- GeometryObject comparison methods
- Accessing Access
- The INTERCAL programming language...

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Add-In Threads and Geometry Comparison

A couple of rather deep Revit API questions can use some clarification, plus a simple database connection issue is resolved:

- [Add-in threads](#2)
- [`GeometryObject` comparison methods](#3)
- [Accessing Access](#4)
- [The INTERCAL programming language](#5)

####<a name="2"></a> Add-In Threads

Some aspects of the single-threaded Revit API versus multi-threaded Revit.exe and add-ins were highlighted in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
asking [Is my plugin restricted by the computing resources of Revit?](https://forums.autodesk.com/t5/revit-api-forum/is-my-plugin-restricted-by-the-computing-resources-of-revit/m-p/12155865):

**Question:** The Revit API is a single-threaded (therefore single-core) process.
I was wondering if a Revit plugin would then have to live with the same restriction or whether a plugin could theoretically run multi-threaded/multi-processed?
And what about GPU access?
I know that some commercial plugin providers have developed standalone software with bidirectional connections to and from Revit, and I was wondering whether the hardware restrictions of Revit were the reason behind that.

**Answer:** The limitation of running tasks on a single thread in the Revit API is not due to hardware restrictions but rather a deliberate design choice aimed at ensuring data consistency.

To clarify, the term "single-threaded" does not necessarily mean it all operates on a single core; it depends on the nature of the data your application handles during different phases.

When we refer to "single-threaded," we are likely talking about the "event loop" utilized by Revit to manage model modifications. The event loop is a common design pattern used in UI applications. However, internally, Revit can utilize multi-core or GPU cores to parallelize rendering processes and generate model data into images.

In the majority of cases, achieving parallel execution of tasks requires a careful design to split jobs into multiple individual sub-jobs. This can be quite challenging for most real-world scenarios.

For many UI applications, opting for a single-threaded approach offers greater stability and simplicity in maintaining data consistency. This holds particularly true for Revit, which is a software equipped with a third-party plugin system. In Revit, all operations related to reading and updating the model are queued and executed sequentially on the main thread, a decision rooted in Revit's specific business domain.

While it is possible to design Revit to support multi-threading, similar to a database software, doing so would entail sacrificing simplicity and introduce complexities related to handling thread-safety during modifications.

Moreover, it's important to note that even for writing data in parallel, a database relies on locking mechanisms to ensure data consistency, effectively making it single-threaded at its core. This approach is based on the widely accepted understanding that modifying related data must be regulated to guarantee data consistency.

Your add-in is not restricted in any way.
The only restriction imposed by Revit is the single-threaded implementation of the Revit API and access to Revit functionality.
All other operations performed by your add-in are not restricted, limited or influenced by Revit in any way whatsoever.
The next limiting factor might be the .NET environment in which your add-in lives, and the `AppDomain` that Revit provides for it.
Please examine the official .NET documentation for that.

However, you also can always implement whatever functionality you like outside of your Revit add-in and communicate between that and the add-in in a number of ways.

Many thanks to Kennan Chen for this clarification!

####<a name="3"></a> GeometryObject Comparison Methods

A pertinent question from
Richard [RPThomas108](https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859) Thomas
on [equality methods of `GeometryObject`](https://forums.autodesk.com/t5/revit-api-forum/equality-methods-of-geometryobject/td-p/12149045):
was clarified by the development team:

**Question:** Can someone confirm what the equality methods of `GeometryObject` are based upon, i.e.:

- Memory reference
- Geometric configuration and/or properties of the class
- Id assigned to the class alone (GeometryObject.Id)
- Something else?

I'm specifically interested in the equality/inequality operators and the overridden `Object.Equals`:

<center>
<img src="img/geo_obj_comparison.png" alt="Object.Equals et al" title="Object.Equals et al" width="800"/> <!-- Pixel Height: 353 Pixel Width: 974 -->
</center>

I guess the simple yes / no answer (if it exists) would be: are the equality methods noted above based on memory reference alone?
However, isn't `Object.Equals` based on memory reference to begin with?
If it is overridden, then how is it overridden?
I tend to create a lot of class objects whose purpose is to wrap an API object and attach an id due to uncertainty about the equality of the API objects.
Some clarification here or in the documentation would probably be quite helpful in reducing this aspect.
I see the following in the RevitAPI.chm under GeometryObject.Equals in the Remarks section:

> This compares the internal identifiers of the geometry, and doesn't compare them geometrically

Now that we've ruled out the geometric configuration, we just need to know what the 'internal identifiers' are exactly.
Is it the `GeometryObject.Id` or something else?
What kind of operations affect these identifiers?
Do they exist for every API object created or just the items coming directly from element geometry?

I've now also reviewed the below from RevitAPI.chm on `GeometryObject.Id`:

> This id can be stored and used for future referencing.
The reference should be stable between minor geometric changes and modifications, but may not remain valid if there are major changes to the element or its surroundings.
Note that the id may be negative (and thus invalid for referencing) if obtained from view-specific geometry, or if obtained from most GeometryObjects created in memory by the API.
Negative ids cannot be used for referencing.
These integer ids should not be used for comparison purposes (other than to check if they are equivalent or not).
Nothing should be assumed about rules about how an element populates the sequence of different numeric values as this may change based on the element's definition.

I've noticed previously that geometry from two different elements could have the same id (the id values are low so that is reasonable).
That being the case, I'm also looking to confirm that the equality methods noted above are not equating two geometry objects with the same id from two different elements as being the same (internal identifier <> GeometryObject.Id).

I think knowing more about this could be quite helpful either way.
We could test and find answers, but it would then remain an unknown aspect to new people encountering the API.

Personally, I always look at the equals functions in the documentation to see if it has been overridden for a class or not.
That then informs me if I can use Linq extensions such as `distinct` and `union` with it (so we need to know how it is overridden also).

For classes such as `ElementId`, it is inherently obvious, but some objects are less so and need more description.
If it is just using GeometryObject.Id, then I can live with the limitations of that; I just need to know that is what it is doing.

**Answer:** Good question, might be something worth documenting:

- GeometryObject.Equals &ndash; This compares the memory references of the compared objects.
- GeometryObject.operator == &ndash; This compares the pointers of the Revit-internal geometry objects.
- GeometryObject.GetHashCode &ndash; This is a hash of the pointer to the Revit-internal geometry object.

In principle, there could be two different Revit.DB.GeometryObject objects that are both handles to the same Revit-internal geometry object; for example, if you retrieve the GeometryObject from the same `Reference` twice in succession, you will end up with two different GeometryObjects that both are handles to the same Revit-internal object.
In this case, GeometryObject.Equals would return false, but GeometryObject.operator == would return true.
Both GeometryObjects would have the same hash code.

If either comparison method returns true, then that guarantees that they both have the same GeometryObject.Id, which is an attribute of the underlying Revit object.
However, two totally unrelated GeometryObjects may have the same Id, since this is not intended to be a globally unique identifier.

For completeness, the inequality operator is simply the negation of the equality operator.
Two GeometryObjects that internally hold null pointers to Revit-internal objects are considered equal when compared with operator ==.

I think this behaviour could be considered reasonable, if we update the documentation to reflect the actual behaviour.

Also, as noted, it may not make sense to override Object.Equals at all.

**Response:** I think I'm largely happy with the status quo; as stated, it is probably more important that how it works is better described in the documentation; we then can make the right allowances based on that.

Seems like there is no real logic in two managed objects that point to the same unmanaged object not returning true for Equals.
I can't think of a reason off of the top of my head as to why the Equals function doesn't work exactly the same way as the == operator.
However, the Linq extensions will only be using 'Equals', not ==, so it might be useful therefore to make Equals the same as ==.
Secondly, `GetHashCode` will be called by Linq extensions prior to `Equals`, so therefore it should be overridden to return 0 to force the comparison by `Equals` or be overridden to perhaps provide a better hashing function related to the unmanaged memory reference (not the managed counterpart.

I have a feeling that the default algorithm that Object.GetHashCode uses in .NET is fast rather than perfect in terms of comparing two objects as being the same.
Since the hash code is based on the reference (64 bit on 64-bit system) but the hash code itself is only 32 bits.
So, in theory there can be clashes that then need further resolution via `Equals`.
In contrast, if you are comparing the value of 64-bit memory pointers directly on the unmanaged side, then there can be no doubt it is the same object.

All I'm aiming for in the end is that when I extract objects from Revit I can then pass those objects through my functions which could perhaps sort them or group them, but in the end I can compare what comes out against the original set.
I also need to remove items from a List (List&lt;T&gt;.Remove) which is sometimes hard if `Equals` and `GetHashCode` have not been overridden, since that method will use the default equality comparer.

To deal with these kinds of things I recently wrote the following generic classes where comparisons are conducted during the context of the external command.
I think a lot of it comes down to trust in `GetHashCode`.
Obviously, the below is for all kinds of things, not just geometry objects.
The pain of this kind of approach is that you have to create a new list of items of the below classes from the existing list of API objects rather than just knowing you can use the API objects directly.

<pre class="prettyprint">
  Public Class RT_GeometryObjWithId(Of T As GeometryObject)
    Inherits RT_ApiObjectWithId(Of T)

    Public ReadOnly Property GeometryId As Integer
    Public Sub New(Obj As T)
      MyBase.New(Obj)
      GeometryId = Obj.Id
    End Sub
  End Class

  Public Class RT_ApiObjectWithId(Of T As APIObject)
    Implements IDisposable

    Public ReadOnly Property UID As Guid = Guid.NewGuid
    Private disposedValue As Boolean
    Private IntAPIObj As T
    Public Property APIObject As T
      Get
        Return IntAPIObj
      End Get
      Set(value As T)
        IntAPIObj = value
      End Set
    End Property
    Public Sub New(Obj As T)
      IntAPIObj = Obj
    End Sub

    Public Overrides Function GetHashCode() As Integer
      'Force consideration of Equals
      'Likely I could leave this alone and rely on the default implementation
      'However I have more trust in comparing two guids than GetHashCode
      Return 0
    End Function
    Public Overrides Function Equals(obj As Object) As Boolean
      Dim Other As RT_ApiObjectWithId(Of T) = _
        TryCast(obj, RT_ApiObjectWithId(Of T))
      If Other Is Nothing Then Return False else
      Return Me.UID = Other.UID
    End Function

    Public Shared Operator =(A As RT_ApiObjectWithId(Of T), _
      B As RT_ApiObjectWithId(Of T)) As Boolean
      Return A.Equals(B)
    End Operator
    Public Shared Operator &lt;&gt;(A As RT_ApiObjectWithId(Of T), _
      B As RT_ApiObjectWithId(Of T)) As Boolean
      Return Not A.Equals(B)
    End Operator

    Protected Overridable Sub Dispose(disposing As Boolean)
      If Not disposedValue Then
        If disposing Then
          ' TODO: dispose managed state (managed objects)
          If APIObject IsNot Nothing Then
            APIObject.Dispose()
          End If
        End If

        ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
        ' TODO: set large fields to null
        disposedValue = True
      End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '   ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '   Dispose(disposing:=False)
    '   MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
      ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
      Dispose(disposing:=True)
      GC.SuppressFinalize(Me)
    End Sub
  End Class
</pre>

Many thanks to Richard for raising this important question and sharing his viable and well-considered solution.

####<a name="4"></a> Accessing Access

A repeating question
on [connecting with a MS Access database](https://forums.autodesk.com/t5/revit-api-forum/connecting-to-ms-access/m-p/12208698) came
up once again, and the answer is known, so worthwhile to point out here as well.

Oleg 'OR_AND_NO' Rodygin of [RZD Project](https://en.wikipedia.org/wiki/Russian_Railways) very kindly pointed out that the answer is provided in a previous post:
[Can't make connection to Access database](https://forums.autodesk.com/t5/revit-api-forum/cant-make-connection-to-acces-database/m-p/10879626):

> You need to install the [Microsoft Access 2013 Runtime](https://www.microsoft.com/en-us/download/details.aspx?id=39358)

Thank you, Oleg.

####<a name="5"></a> The INTERCAL Programming Language

For some slightly less useful information,
in case you are interested in weird programming languages,
[INTERCAL](https://en.wikipedia.org/wiki/INTERCAL) may rank pretty high.
Reading the Wikipedia entry made me laugh out loud.

