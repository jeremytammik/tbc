<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

RevitLookup Using Reflection Cleanup #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Last week, I presented a drastic change to RevitLookup using <code>Reflection</code> to provide more complete coverage of all the Revit database element methods and properties. Victor Chekalin took a critical look at this new version and cleaned it up significantly. Now all is well and order has been restored again
&ndash; Shock and gripe
&ndash; Commit summary
&ndash; 1. Methods and properties extraction
&ndash; 2. Show enum values
&ndash; 3. Drill down into <code>SymbolGeometry</code>
&ndash; 4. Remove duplicate <code>get_</code> property getter method
&ndash; 5. Fixed <code>Fine</code> <code>DetailLevel</code> bug
&ndash; 6. Visual style of separator
&ndash; Download and access to old functionality
&ndash; Reaction and read-only assurance...

-->

### RevitLookup with Reflection Cleanup

Last week, I presented a drastic change 
to [RevitLookup](https://github.com/jeremytammik/RevitLookup) contributed by 
Andy [@awmcc90](https://github.com/awmcc90) McCloskey, [RevDev Studios](https://twitter.com/revdevstudios),
using `Reflection` to provide more complete coverage of all the Revit database element methods and properties.

Now [Victor Chekalin](http://www.facebook.com/profile.php?id=100003616852588), aka Виктор Чекалин, took
a critical look at this new version and cleaned it up significantly to address a number of raw edges in 
his [pull request #25 &ndash; old bug fixes and improvements of the new approach ](https://github.com/jeremytammik/RevitLookup/pull/25).

At first sight, Victor was not at all impressed.
Happily, on second thoughts, all is well and order has been restored again:

- [Shock and gripe](#2)
- [Commit summary](#3)
    - [1. Methods and properties extraction](#3.1)
    - [2. Show enum values](#3.2)
    - [3. Drill down into `SymbolGeometry`](#3.3)
    - [4. Remove duplicate `get_` property getter method](#3.4)
    - [5. Fixed `Fine` `DetailLevel` bug](#3.5)
    - [6. Visual style of separator](#3.6)
- [Download and access to old functionality](#4)
- [Reaction and read-only assurance](#5)


#### <a name="2"></a>Shock and Gripe

At first sight, Victor was not pleased at all with the radical changes.

I think this is important to share our discussion on the first impression, since his sentiments are probably shared by other RevitLookup aficionados as well:

> I wanted to make some changes in RevitLookup (actually fix the bug) and noticed the bad thing;
after Andrew's commit with serious changes, RevitLookup looks ugly.
He completely changed the algorithm of data retrieving, using `Reflection`, but lost some functionality.
In the latest version, for example, I cannot get symbol geometry. It's disabled.
So, the idea is good, bad the implementation needs to be improved :-)
And I'll get send you pull request soon with bug fixed.

On second thoughts, luckily, things cleared up a bit:

> Probably yesterday I was a bit emotional. Because my first opinion was &ndash; what the hell?
Everything is absolutely different, not as usual, difficult to find a property, and I could not get the desired property `SymbolGeometry`.
I found that the new version is very raw.
Looking at it in more detail, I like the general idea to use reflection.
Indeed, it allows to get more information and not worry about the new methods/properties in future versions.

> My biggest concern is with the methods. We are getting the methods using reflection.
Andrew gets all the methods without parameters and return type is not void.
But we cannot ensure the method is just a 'get' method.
A method might modify something and return a value.
For example, `bool Remove() { // remove something return true; }`
 
> Some issues:
The properties/methods are not sorted. In the previous version, they were not sorted either.
But as all the properties were added manually, the order looked more intelligent, like `Id` and `Name` at the top.
With reflection, the properties are sorted in the custom order and difficult to find a particular property.
I think would be better to sort them. I'll do that, no prob.
 
> `SymbolGeometry` is not populated because this is a bug.
I've found where exactly in the code &ndash; `GeometryInstance` is cast to `Element`, but is not in fact derived from it.
Will try to fix it myself or submit an issue on GitHub.


#### <a name="3"></a>Commit Summary

Here is Victor's subsequent pull request commit summary, followed by a detailed list of the enhancements made to bring the new version of RevitLookup using `Reflection` up to par with what we had before:

- Get only `Public` methods
- Get only declared properties and methods
- Do not retrieve property getter methods
- Show enum values directly in list
- Order methods and properties alphabetically by name
- Changed visual style of the property/method separator
- Fixed the bug with displaying `SymbolGeometry` property for `GeometryInstance`
- Fixed the bug with Geometry extraction. Previously, whatever detail level was selected, the geometry was always extracted with `Fine` detail level

Detailed pull request description with illustrations:

#### <a name="3.1"></a>1. Methods and Properties Extraction

I changed it to get only public methods, explicitly declared only, and sorted by name.

Before:

<center>
<img src="img/revitlookup_vc_1a.png" alt="Before" width="802"/>
</center>

After:

<center>
<img src="img/revitlookup_vc_1b.png" alt="After" width="802"/>
</center>

#### <a name="3.2"></a>2. Show Enum Values

Changed behaviour to show enum values.

Before:

<center>
<img src="img/revitlookup_vc_2a.png" alt="Before" width="970"/>
</center>

After:

<center>
<img src="img/revitlookup_vc_2b.png" alt="After" width="802"/>
</center>

#### <a name="3.3"></a>3. Drill Down into SymbolGeometry

Fixed the issue related with GeometryInstance.SymbolGeometry. We could not drill down this property.

Before:

<center>
<img src="img/revitlookup_vc_3a.png" alt="Before" width="802"/>
</center>

After:

<center>
<img src="img/revitlookup_vc_3b.png" alt="After" width="802"/>
</center>

#### <a name="3.4"></a>4. Remove Duplicate `get_` Property Getter Method

Removed property getter from the methods extraction. Otherwise, for each property, we see the property itself as well as a method like `get_SomeProperty`:

Before:

<center>
<img src="img/revitlookup_vc_4a.png" alt="Before" width="802"/>
</center>

After:

<center>
<img src="img/revitlookup_vc_4b.png" alt="After" width="802"/>
</center>

#### <a name="3.5"></a>5. Fixed Fine DetailLevel Bug

Fixed the very old bug, related with geometry extraction. Whatever `DetailLevel` you select, it always showed the geometry for `Fine`.

For active view, `Medium` `DetailLevel`:

Before:

<center>
<img src="img/revitlookup_vc_5a.png" alt="Before" width="1013">
</center>

After:

<center>
<img src="img/revitlookup_vc_5b.png" alt="After" width="1116"/>
</center>

#### <a name="3.6"></a>6. Visual Style of Separator

Changed visual style of the separator. Changed colour and shifted the title a bit.

Before:

<center>
<img src="img/revitlookup_vc_6a.png" alt="Before" width="789"/>
</center>

After:

<center>
<img src="img/revitlookup_vc_6b.png" alt="After" width="570"/>
</center>


#### <a name="4"></a>Download and Access to Old Functionality

The most up-to-date version is always provided in the master branch of 
the [RevitLookup GitHub repository](https://github.com/jeremytammik/RevitLookup).

Victor's bug fixes and enhancements are provided
in [RevitLookup release 2017.0.0.16](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.16) and
later versions.

If you would like to access any part of the functionality that was removed when switching to the `Reflection` based approach, please grab it
from [release 2017.0.0.13](https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.13) or earlier.

I am also perfectly happy to restore code that was removed and that you would like preserved. Simply create a pull request for that, explain your need and motivation, and I will gladly merge it back again.


#### <a name="5"></a>Reaction and Read-Only Assurance

Andy responds to the update and answers the question on the methods that might modify something:

> The changes look great, and yes, this version is absolutely more raw, but, when all is said and done, I think it will be a lot better. 

> As far as your concern for modifying the model by executing methods that would modify the model data: this cannot happen, given that we are outside of a transaction. Method such as `Rotate`, etc., will return `false` when they cannot execute, which is what you are seeing. 

> This is one thing that I recognised early on as a potential issue but is not a problem &ndash; unless there is something I'm completely missing here. 
Let me know if you find anything to the contrary; otherwise, I still believe this version is safe. 


Ever so many thanks again to Andy for his brave initiative and for Victor for his critical and constructive clean-up!
