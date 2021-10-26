<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Modeless RevitLookup
  RevitLookup_modeless.png

- need for regen:
  https://forums.autodesk.com/t5/revit-api-forum/unable-to-get-parameter-asstring-value-when-the-parameter-is/m-p/10713499#M59301

- need for regen
  [LevelOffset not working for Arc extrusion Roof](https://forums.autodesk.com/t5/revit-api-forum/leveloffset-not-working-for-arc-extrusion-roof/m-p/7681949)
  [circular chain of reference when creating opening on a floor](https://forums.autodesk.com/t5/revit-api-forum/error-circular-chain-of-reference-when-creating-opening-on-a/td-p/7681213)

- img/outdoor_seatbelt.jpg


twitter:

add #thebuildingcoder

 the #RevitAPI #DynamoBim @AutodeskForge @AutodeskRevit #bim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

**Question:** 

**Answer:**

**Response:**  

Many thanks to  for this very helpful explanation!

<pre class="code">
</pre>

-->

### Modeless RevitLookup


####<a name="2"></a> Modeless RevitLookup

[Jmcouffin](https://jmcouffin.com) raised a
cool request for a RevitLookup enhancement
in [issue #92 &ndash; modeless window for lookup tools](https://github.com/jeremytammik/RevitLookup/issues/92):

> while not able to code it myself, especially not in C#, a nice feature would be to get the windows of the tool in modelless mode.
Allowing us to interact with the model directly.

[NeVeSpl](https://github.com/NeVeSpl) very kindly picked this up and heroically implemented and tested it in a whole serioes of pull requests:

- [93](https://github.com/jeremytammik/RevitLookup/pull/93) &ndash; Modeless windows
- [94](https://github.com/jeremytammik/RevitLookup/pull/94) &ndash; Fixed problem with tranfering focus to Revit when using selectors from modeless window
- [95](https://github.com/jeremytammik/RevitLookup/pull/95) &ndash; handle multiple open documents at the same time
- [96](https://github.com/jeremytammik/RevitLookup/pull/96) &ndash; fix for crash that happens when user cancel picking object in cmds: SnoopPickFace, SnoopPickEdge, SnoopLinkedElement
- [97](https://github.com/jeremytammik/RevitLookup/pull/97) &ndash; Restore ability to snoop plan topologies
- [99](https://github.com/jeremytammik/RevitLookup/pull/99) &ndash; Eliminate warnings from #98

Here is a sample screen snapshot showing the result, snooping a level and two different walls, simultaneously running the Revit command to create yet more new walls:

<center>
<img src="img/RevitLookup_modeless.png" alt="Modeless RevitLookup" title="Modeless RevitLookup" width="600"/> <!-- 3360 -->
</center>

Summary by Jmcouffin:

> It works much better than I even thought possible.
You can now open multiple instances of RevitLookup tool and have each grab its own set of elements and data and dig through it!
I tried all the functionalities without hitting a wall so far.
I will keep on using it this week and let you know if anything arises.
Great job @NeVeSpl!

Ever so many thanks to NeVeSpl from me too for the careful and efficient implementation and thorough testing!



####<a name="3"></a> 

**Question:** 

**Answer:** 

####<a name="4"></a> 


####<a name="5"></a> Outdoor Seatbelt?

If you learned to enjoy wearing a mask when alone, you might also feel safer with an outdoor seatbelt:

<center>
<img src="img/outdoor_seatbelt.jpg" alt="Outdoor seatbelt" title="Outdoor seatbelt" width="260"/> <!-- 1843 -->
</center>

