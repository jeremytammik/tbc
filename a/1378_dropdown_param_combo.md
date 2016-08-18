<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #adsklabs #rendering
#3dweb #3dviewapi #html5 #threejs #webgl #3d #apis #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restapi
#mongoosejs #mongodb #nodejs
#rtceur
#geometry #3d
#xaml

Revit API, Jeremy Tammik, akn_include

Drop-down Enumerated Parameter Values #revitapi #bim #aec #3dwebcoder #adsk #au2015 #apis

I repeatedly hear from developers who wish to define a specific enumerated set of parameter values for their add-ins and limit the selection to these values in the element property palette user interface. Internally, Revit does implement a system to handle this, for instance by using negative element ids for element property drop-down list enumerations. This has also been a long-standing wish list item, and unfortunately still remains in that state, currently incorporated in the issue CF-3498 API wish: drop-down enumeration parameters for combo box...

-->

### Drop-down Enumerated Parameter Values

I repeatedly hear from developers who wish to define a specific enumerated set of parameter values for their add-ins and limit the selection to these values in the element property palette user interface.

Internally, Revit does implement a system to handle this, for instance by
using [negative element ids for element property drop-down list enumerations](http://thebuildingcoder.typepad.com/blog/2014/04/element-id-export-unique-navisworks-and-other-ids.html#5).

This has also been a long-standing wish list item, and unfortunately still remains in that state, currently incorporated in the issue CF-3498 *API wish: drop-down enumeration parameters for combo box*.

- [Drop-down Combo of Enumerated Parameter Values](#2)
- [1. Workaround using Nested Families and Types](#3)
- [2. Workaround using Integer Values and Tooltips](#4)
- [3. Workaround using Family Instance in a Design Option](#5)

I would not bore you with this, except that Marcelo Quevedo of [hsbcad](http://hsbcad.com) recently brought it up again and also kindly provided suggestions for two workarounds:

#### <a name="2"></a>Drop-down Combo of Enumerated Parameter Values

In Marcelo's own words:

We need to create drop-down parameters for our families such as Enums in C#.
For instance, a set of enumerated values such as this:

Parameter 1: `Orientation` with following drop-down values:

- Parallel to mortise bm
- Perpendicular to mortise bm
- Parallel to projected Y axis of tenon bm
- Perpendicular to projected Y axis of tenon bm
- Parallel to projected Z axis of tenon bm
- Perpendicular to projected Z axis of tenon bm

Parameter 2: `Shape`:

- Square
- Round
- Rounded

We found two workarounds, but they aren’t perfect.

#### <a name="3"></a>1. Workaround using Nested Families and Types

For the first workaround, we created nested Generic Model families for each drop-down (one nested family for orientation, and other for Shape). We created types for these families named according to the desired drop-down values. In addition, we added two 'Generic Model Family Type' parameters: one to link the Orientation nested family, and one for the Shape nested family. However, the issue is that the 'Generic Model Family Type' parameter links the Category and displays all nested Generic Model family types as available options.

It would be better if the family type parameter would link one single family instead of the entire category.

Here is a screen snapshot of the result:

<center>
<img src="img/dropdown_param_enum_workaround_1.png" alt="Drop-down parameter enum combo" width="600">
</center>


#### <a name="4"></a>2. Workaround using Integer Values and Tooltips

For the second workaround, we created Integer parameters and defined a tooltip to explain what each integer value represents.

For example, for `Orientation`, we created an integer parameter called `Orientation` and specified the following tooltip:

0. Parallel to mortise bm
1. Perpendicular to mortise bm
2. Parallel to projected Y axis of tenon bm
3. Perpendicular to projected Y axis of tenon bm
4. Parallel to projected Z axis of tenon bm
5. Perpendicular to projected Z axis of tenon bm

The result looks like this:

<center>
<img src="img/dropdown_param_enum_workaround_2.png" alt="Drop-down parameter enum combo" width="600">
</center>

I hope this helps.

Many thanks to Marcelo for sharing these two creative workarounds!

#### <a name="5"></a>3. Workaround using Family Instance in a Design Option

Proposed by Matt Taylor in his [comment below](http://thebuildingcoder.typepad.com/blog/2015/11/drop-down-enumerated-parameter-values.html#comment-2843954426):

I've just come up with another way of doing this.

It's more of a model template solution than a family solution.

Place a family instance with each value in a design option, then just make sure that design option is omitted from all views and schedules.

Because all values for those parameters exist somewhere in the model, they still appear on the pull-down list.

Many thanks to Matt for this nice idea!
