<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

 #revitAPI #3dwebcoder @AutodeskRevit #adsk #aec #bim @AutodeskForge #3dwebaccel


-->

### DLL Signature, AutoCAD Communication and UK BIM

A couple fo quick topics to start the week:

- [Addin DLL signature](#2)
- [BIM 2 in the UK](#3)
- [Communicating with AutoCAD](#4)


#### <a name="2"></a>Addin DLL Signature

As I mentioned [migrating RvtSamples to Revit 2017](http://thebuildingcoder.typepad.com/blog/2016/04/rvtsamples-for-revit-2017.html),
the ['Security – Unsigned Add-In' message](http://thebuildingcoder.typepad.com/blog/2016/04/rvtsamples-for-revit-2017.html#5) is
now displayed by Revit when an unsigned add-in DLL is detected.

We raised that question briefly in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) thread
on [code signing of Revit addins](http://forums.autodesk.com/t5/revit-api/code-signing-of-revit-addins/td-p/5981560),
and the issue came up again in another case yesterday:

**Question:** I signed my add-in, expecting that doing so would suppress the 'Security-Unsigned Add-in' message displayed during Revit start-up:

<center>
<img src="img/revit_security_startup_dialog_01.png" alt="'Security-Unsigned Add-in' message" width="548">
</center>

However, even when this DLL is signed, a similar dialogue appears, requiring user input:

<center>
<img src="img/revit_security_startup_dialog_02.png" alt="'Security-Unsigned Add-in' message" width="939">
</center>

Can this dialogue be suppressed?

I would prefer it not to interrupt the user experience during Revit start-up.

**Answer:** Please study the neww section
on [Digitally Signing Your Revit Add-in](http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-6D11F443-AC95-4B5B-A896-DD745BA0A46D) in
the [online Revit 2017 Help](http://help.autodesk.com/view/RVT/2017/ENU) &gt;
Developers &gt; Revit API Developers Guide &gt; Introduction &gt; Add-In Integration &gt; Digitally Signing Your Revit Add-in.

It was just updated right now and will hopefully resolve all your questions.


#### <a name="3"></a>BIM 2 in the UK



30 of the 53 warnings concern the obsolete automatic transaction mode:

- Warning CS0618: 'TransactionMode.Automatic' is obsolete: 'This mode is deprecated in Revit 2017.'

We have know for long that it will be going away.

In fact, it
was [publicly announced](http://thebuildingcoder.typepad.com/blog/2012/05/read-only-and-automatic-transaction-modes.html) as
far back as 2012.

So this clean-up is long overdue.

Unfortunately, it will cost me some effort to rewrite these 30 commands.

That is also the reason why it is so long overdue &nbsp; :-)

Later... I fixed all 30 of these external commands, reducing the count to [23 warnings](zip/tbc_samples_2017_migr_02.txt) about obsolete API usage.

Here are the [diffs](https://github.com/jeremytammik/the_building_coder_samples/compare/2017.0.127.0...2017.0.127.1) for this clean-up.


#### <a name="3"></a>Obsolete Plane Constructors and NewPlane Methods

All three overloads of the Application.NewPlane method are obsolete:

- NewPlane(CurveArray) Obsolete. Creates a new geometric plane from a loop of planar curves.
- Public method NewPlane(XYZ, XYZ) Obsolete. Creates a new geometric plane object based on a normal vector and an origin.
- Public method NewPlane(XYZ, XYZ, XYZ) Obsolete. Creates a new geometric plane object based on two coordinate vectors and an origin.

I am using the one taking two `XTZ` arguments representing a normal vector and an origin, and the one taking a `CurveArray` argument.

All three overloads of the Plane class constructor are obsolete:

- Plane() Obsolete. Default constructor
- Public method Plane(XYZ, XYZ) Obsolete. Constructs a Plane object from a normal and an origin represented as XYZ objects. Follows the standard conventions for a planar surface. The constructed Plane object will pass through origin and be perpendicular to normal. The X and Y axes of the plane will be defined arbitrarily.
- Public method Plane(XYZ, XYZ, XYZ) Obsolete. Constructs a Plane object from X and Y axes and an origin represented as XYZ objects. The plane passes through "origin" and is spanned by the basis vectors "xVec" and "yVec".

I am using the one taking two `XYZ` arguments representing a normal vector and an origin.

These three obsolete methods now generate the following warnings:

- Warning CS0618: 'Application.NewPlane(XYZ, XYZ)' is obsolete: 'This method is obsolete in Revit 2017. Please use Plane.CreateByNormalAndOrigin() instead.'
- Warning CS0618: 'Plane.Plane(XYZ, XYZ)' is obsolete: 'This method is obsolete in Revit 2017. Please use Plane.CreateByNormalAndOrigin() instead.'
- Warning CS0618: 'Application.NewPlane(CurveArray)' is obsolete: 'This method is obsolete in Revit 2017. Please use CurveLoop.GetPlane() instead.'

Let's go and do what the man says...

I replaced all occurrences of the first two warnings, reducing the count to [5 warnings](zip/tbc_samples_2017_migr_03.txt) about obsolete API usage.

Here are the [diffs](https://github.com/jeremytammik/the_building_coder_samples/compare/2017.0.127.1...2017.0.127.2) for this clean-up.


#### <a name="4"></a>Obsolete NewPlane Method Taking a CurveArray Argument

I still have three calls to the obsolete `Application.NewPlane` method taking a `CurveArray` argument, e.g., in CmdWallProfile.cs:

<pre class="code">
  <span style="color:#2b91af;">CurveArray</span>&nbsp;curves&nbsp;=&nbsp;creapp.NewCurveArray();

  <span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Curve</span>&nbsp;curve&nbsp;<span style="color:blue;">in</span>&nbsp;curveLoop2&nbsp;)
  {
  &nbsp;&nbsp;curves.Append(&nbsp;curve.CreateTransformed(&nbsp;offset&nbsp;)&nbsp;);
  }

  <span style="color:green;">//&nbsp;Create&nbsp;model&nbsp;lines&nbsp;for&nbsp;an&nbsp;curve&nbsp;loop.</span>

  <span style="color:#2b91af;">Plane</span>&nbsp;plane&nbsp;=&nbsp;creapp.NewPlane(&nbsp;curves&nbsp;);&nbsp;<span style="color:green;">//&nbsp;2016</span>
</pre>

The suggested fix is to use the `CurveLoop.GetPlane` method instead.

Luckily, in this case, the variable `curveLoop2` from which we extract the individual curves to add to the CurveArray collection is already a `CurveLoop` instance, so we can call `GetPlane` on it directly:

<pre class="code">
  <span style="color:#2b91af;">Plane</span>&nbsp;plane&nbsp;=&nbsp;curveLoop2.GetPlane();&nbsp;<span style="color:green;">//&nbsp;2017</span>
</pre>

Unfortunately, we still need to rtain and set up the `CurveArray`, because that is a required argument to the subsequent call to `NewModelCurveArray`.

<!---
I eliminated one more of these warnings, but not the other, because it is just used for an assertion in a debugging statement.

That leaves us with
just [3 warnings](zip/tbc_samples_2017_migr_04.txt) about
obsolete API usage, after making the changes captured in
these [diffs](https://github.com/jeremytammik/the_building_coder_samples/compare/2017.0.127.2...2017.0.127.3).
-->

#### <a name="5"></a>Replace View.SetVisibility by SetCategoryHidden

The `View.SetVisibility` is replaced by `SetCategoryHidden`.

Here are the calls used in Revit 2016 and Revit 12017, respectively:

<pre class="code">
  view.SetVisibility(&nbsp;catHosts,&nbsp;<span style="color:blue;">false</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;2016</span>

  view.SetCategoryHidden(&nbsp;catHosts.Id,&nbsp;<span style="color:blue;">true</span>&nbsp;);&nbsp;<span style="color:green;">//&nbsp;2017</span>
</pre>


#### <a name="6"></a>Use DirectShape ApplicationId and ApplicationDataId

In Revit 2016, the application and application data GUIDs for a DirectShape element were passed into the constructor.

In Revit 2017, they can be set later using the corresponding properties:

<pre class="code">
  <span style="color:#2b91af;">DirectShape</span>&nbsp;ds&nbsp;=&nbsp;<span style="color:#2b91af;">DirectShape</span>.CreateElement(
  &nbsp;&nbsp;doc,&nbsp;e.Category.Id,
  &nbsp;&nbsp;_direct_shape_appGUID,
  &nbsp;&nbsp;appDataGUID&nbsp;);&nbsp;<span style="color:green;">//&nbsp;2016</span>

  <span style="color:#2b91af;">DirectShape</span>&nbsp;ds&nbsp;=&nbsp;<span style="color:#2b91af;">DirectShape</span>.CreateElement(
  &nbsp;&nbsp;doc,&nbsp;e.Category.Id&nbsp;);&nbsp;<span style="color:green;">//2017</span>

  ds.ApplicationId&nbsp;=&nbsp;_direct_shape_appGUID;&nbsp;<span style="color:green;">//&nbsp;2017</span>
  ds.ApplicationDataId&nbsp;=&nbsp;appDataGUID∫∫;&nbsp;<span style="color:green;">//&nbsp;2017</span>
</pre>


#### <a name="7"></a>All Obsolete Revit API Usage Eliminated

The final result of the migration and obsolete API clean-up
is [release 2017.0.127.4](https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2017.0.127.4),
compiling with zero errors and warnings.

The newest version is always available
from [The Building Coder samples GitHub repository](https://github.com/jeremytammik/the_building_coder_samples) master branch.
