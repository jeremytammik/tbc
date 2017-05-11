<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- 12953375 [Revit API UIDocument.PromptForFamilyInstancePlacement Issue]

- 12965336 [Revit 2018 API - Undocumented Changes - Have you found any?]
  https://forums.autodesk.com/t5/revit-api-forum/revit-2018-api-undocumented-changes-have-you-found-any/m-p/7074819

Prompt Cancel Throws Exception in Revit 2018 @AutodeskForge #ForgeDevCon #RevitAPI @AutodeskRevit #adsk #aec #bim #dynamobim http://bit.ly/prompt_exception

In Revit 2018, cancelling family instance placement during a call to <code>PromptForFamilyInstancePlacement</code> throws an <code>OperationCanceledException</code> exception
&ndash; Easily fixed, once discovered
&ndash; Question
&ndash; Change in Behaviour
&ndash; Exceptions Should be Exceptional
&ndash; Answer
&ndash; The Building Coder samples <code>CmdPlaceFamilyInstance</code>...

-->

### Prompt Cancel Throws Exception in Revit 2018

I just picked up an ADN case on a topic that was already raised yesterday in 
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit 2018 API undocumented changes](https://forums.autodesk.com/t5/revit-api-forum/revit-2018-api-undocumented-changes-have-you-found-any/m-p/7074819),
so it is definitely worth highlighting here as well:

- [Question](#3)
- [Change in behaviour](#4)
- [Exceptions should be exceptional](#5)
- [Answer](#6)
- [The Building Coder samples `CmdPlaceFamilyInstance`](#7)


#### <a name="3"></a>Question

A bug may have been introduced into the Revit 2018 API `UIDocument` `PromptForFamilyInstancePlacement` method.

In Revit 2017, hitting the `Escape` key twice after placing the families would end the command.

In Revit 2018, hitting the `Escape` key twice generates an `OperationCanceledException` and all the elements that were just placed are deleted.

The problem can be observed by comparing the behaviour of the Revit 2017 SDK `PlacementOptions` sample add-in with the Revit 2018 SDK version of the `PlacementOptions` sample add-in.


#### <a name="4"></a>Change in Behaviour

Matt Taylor describes the situation differently in
his [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread
on [Revit 2018 API undocumented changes](https://forums.autodesk.com/t5/revit-api-forum/revit-2018-api-undocumented-changes-have-you-found-any/m-p/7074819):

Each year I upgrade my codebase for use with the new version of Revit. Each year, I rid my code of deprecated and/or obsolete function warnings/errors.

Each year I seem to find an undocumented change in the way the Revit API works.
 
'The Factory', can we *please* have a more detailed and complete list of changes? Can you add this change to the documentation, please?
 
My 'find' this year is a change in the way `PromptForFamilyInstancePlacement` works.

This function used to just return focus to your function upon cancelling by the Reviteer.

In Revit 2018, cancelling of this function by your Reviteers throws an `Exceptions.OperationCanceledException` [sic] exception.

Easily fixed, once discovered:

<pre class="code">
<span style="color:blue;">Try</span>
&nbsp;&nbsp;docUi.PromptForFamilyInstancePlacement(FamilySymbol)
<span style="color:blue;">Catch</span>&nbsp;ex&nbsp;<span style="color:blue;">As</span>&nbsp;Exceptions.<span style="color:#2b91af;">OperationCanceledException</span>
&nbsp;&nbsp;<span style="color:green;">&#39;&nbsp;The&nbsp;user&nbsp;cancelled&nbsp;placement.</span>
&nbsp;&nbsp;<span style="color:green;">&#39;&nbsp;This&nbsp;should&nbsp;only&nbsp;trigger&nbsp;in&nbsp;Revit&nbsp;2018.</span>
&nbsp;&nbsp;<span style="color:green;">&#39;&nbsp;Do&nbsp;something&nbsp;if&nbsp;you&nbsp;like</span>
<span style="color:blue;">End</span>&nbsp;<span style="color:blue;">Try</span>
</pre>
 
This change even makes sense!

It's a good idea!

It also fills me with dread. What else is going to throw an exception unexpectedly?

What other changes are there?

(Yes, I know that this item
is [*vaguely* alluded to in the 'what's new' document](http://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html#2.7),
but it's not documented anywhere.)
 
Have you found any hidden 'treasures' that you want to share?


#### <a name="5"></a>Exceptions Should be Exceptional

Greg '[Sherbs]()' adds a very valid additional point:

Yikes!

Good catch!

This is more than a bit concerning.

Undocumented exceptions are generally going to be application fatal.

I hope this sort of thing can be addressed more systematically in upcoming releases.

Regarding the specific find, my opinion differs:
 
There really is nothing 'exceptional' or 'unexpected' here.

Cancelling placement may be infrequent, but it is an entirely normal user action.

Why even throw in this case at all? 
 
I'm not terribly passionate about this, just throwing out another viewpoint.

I'm a bit of minimalist when it comes to the use of exceptions. 

<center>
<img src="img/interrupt_process.png" alt="Interrupt Process" width="435">
<p style="font-size:smaller">By <a href="//commons.wikimedia.org/wiki/User:Anon_lynx" title="User:Anon lynx">Stephen Charles Thompson</a> &ndash; <span class="int-own-work" lang="en">Own work</span>, <a href="http://creativecommons.org/licenses/by-sa/3.0" title="Creative Commons Attribution-Share Alike 3.0">CC BY-SA 3.0</a>, <a href="https://commons.wikimedia.org/w/index.php?curid=23385273">Link</a></p>
</center>


#### <a name="6"></a>Answer

Many thanks to Matt for pointing this out!
 
I would say that this change in behaviour is *precisely* alluded to, not *vaguely*, in the documentation
of [What's New in the Revit 2018 API](http://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html) section 
on [UIDocument.PromptForFamilyInstancePlacement() behavioral change](http://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html#2.7):

> The behavior for UIDocument.PromptForFamilyInstancePlacement() was changed to be same as that of PickObject() methods...
 
Raising the exception you mention corresponds exactly to the `PickPoint` behaviour.
 
However, just as you say, the detailed consequences are not explicitly spelled out.
 
I also fully agree with Greg's statement:
[exceptions should be exceptional](http://jacopretorius.net/2009/10/exceptions-should-be-exceptional.html).
 
Expected behaviour should not be communicated using exceptions.
 
I have been preaching this for years to little avail:

- [Fixing RvtMgdDbg for MEP Connectors](http://thebuildingcoder.typepad.com/blog/2009/08/fixing-rvtmgddbg-for-mep-connectors.html)
- [Duplicate Mark Values](http://thebuildingcoder.typepad.com/blog/2010/03/duplicate-mark-values.html)
- [Selecting Model Elements](http://thebuildingcoder.typepad.com/blog/2010/10/selecting-model-elements.html)
- [Language Independent Subcategory Creation](http://thebuildingcoder.typepad.com/blog/2011/01/language-independent-subcategory-creation.html)
- [Exporting Parameter Data to Excel, and Re-importing](http://thebuildingcoder.typepad.com/blog/2012/09/exporting-parameter-data-to-excel.html)
- [Parameter DisplayUnitType, Bretagne and Decompilers](http://thebuildingcoder.typepad.com/blog/2013/03/parameter-displayunittype-and-decompilers.html)
- [External Command Lister and Adding Ribbon Commands](http://thebuildingcoder.typepad.com/blog/2013/05/external-command-lister-and-adding-ribbon-commands.html)
- [The Pick Point Methods Throw an Exception on Cancel](http://thebuildingcoder.typepad.com/blog/2014/09/planes-projections-and-picking-points.html#07)
- [Never Catch All Exceptions](http://thebuildingcoder.typepad.com/blog/2017/02/revitlookup-using-reflection-for-cross-version-compatibility.html#12)

To answer the original [question](#3) raised above: You need to catch and handle (or ignore) the `OperationCanceledException` as shown by Matt.

If you code does not, your transaction will presumably not be committed.

The family instances that were successfully placed before the user cancelled the placement and the exception was thrown are probably removed as the transaction is rolled back.

No bug, just a change in behaviour.

I hope this clarifies and all is now illuminated.


#### <a name="7"></a>The Building Coder Samples CmdPlaceFamilyInstance

I implemented
the [external command CmdPlaceFamilyInstance](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdPlaceFamilyInstance.cs)
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples) to
exercise the `PromptForFamilyInstancePlacement` method when it was originally introduced.

It also includes code using the `DocumentChanged` event
to [retrieve the newly created elements](http://thebuildingcoder.typepad.com/blog/2010/06/place-family-instance.html).

I updated it to handle the `OperationCanceledException` as shown by Matt 
in [release 2018.0.132.2](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.132.2).

Here is the [diff to the preceding release](https://github.com/jeremytammik/the_building_coder_samples/compare/2018.0.132.1...2018.0.132.2) that
shows exactly what modifications were made; simply add an exception handler around the call to `PromptForFamilyInstancePlacement`:

<pre class="code">
  <span style="color:blue;">try</span>
  {
  &nbsp;&nbsp;uidoc.PromptForFamilyInstancePlacement(&nbsp;symbol&nbsp;);
  }
  <span style="color:blue;">catch</span>(&nbsp;Autodesk.Revit.Exceptions.<span style="color:#2b91af;">OperationCanceledException</span>&nbsp;ex&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:#2b91af;">Debug</span>.Print(&nbsp;ex.Message&nbsp;);
  }
</pre>


<center>
<img src="img/the_exception.jpg" alt="The Exception" width="220">
</center>

