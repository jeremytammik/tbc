<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

- http://thebuildingcoder.typepad.com/blog/2016/12/nuget-revit-api-package.html#comment-3133102375
  Eric Anastas: I created another Revit related Nuget package you and your readers may be interested in. It's a .NET library that wraps the Revit Server REST API.
  https://www.nuget.org/packages/RevitServerAPILib/
  https://bitbucket.org/somddg/revitserverapilib

- merged [pull request #21](https://github.com/jeremytammik/RevitLookup/pull/21) by [@eibre](https://github.com/eibre) adding `UnitType` property on the parameter `Definition` class
  https://github.com/jeremytammik/RevitLookup/releases/tag/2017.0.0.12

- Component of a truss: Truss.Members and FamilyInstance.GetSubComponentIds
  http://forums.autodesk.com/t5/revit-api-forum/component-of-a-truss/m-p/6845784

- GeometryObject layer name
  http://forums.autodesk.com/t5/revit-api-forum/geometryobject-layer-name/m-p/6835165

#RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Today, we proudly present
&ndash; NuGet Revit Server REST API Library
&ndash; RevitLookup Updates
&ndash; Truss Members and FamilyInstance Sub-Components
&ndash; GeometryObject Layer Name...

-->

### Revit Server API Lib, Truss Members and Layers

Today, we proudly present:

- [NuGet Revit Server REST API Library](#2)
- [RevitLookup Updates](#3)
- [Truss Members and FamilyInstance Sub-Components](#4)
- [GeometryObject Layer Name](#5)


#### <a name="2"></a>NuGet Revit Server REST API Library

Eric Anastas posted
a [comment](http://thebuildingcoder.typepad.com/blog/2016/12/nuget-revit-api-package.html#comment-3133102375) on
the discussion of Andrey Bushman's [NuGet Revit API package](http://thebuildingcoder.typepad.com/blog/2016/12/nuget-revit-api-package.html):

> I created another Revit related Nuget package you and your readers may be interested in.
It's a .NET library that wraps the Revit Server REST API:

- [RevitServerAPILib package on NuGet ](https://www.nuget.org/packages/RevitServerAPILib)
- [RevitServerAPILib source on BitBucket](https://bitbucket.org/somddg/revitserverapilib)

Many thanks to Eric for implementing and sharing this!

<center>
<img src="img/nuget_logo.png" alt="" width="64"/>
</center>


#### <a name="3"></a>RevitLookup Updates

I integrated another couple of pull requests into RevitLookup.

Today, I added [Einar Raknes](https://github.com/eibre)' simple but significant one-liner
to [display the `UnitType` or a parameter `Definition` class instance](https://github.com/jeremytammik/RevitLookup/compare/2017.0.0.11...2017.0.0.12):

<pre class="code">
&nbsp;&nbsp;data.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;Snoop.Data.<span style="color:#2b91af;">String</span>(&nbsp;<span style="color:#a31515;">&quot;Unit&nbsp;type&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;paramDef.UnitType.ToString()&nbsp;)&nbsp;);
</pre>

Thanks to Einar for spotting this and creating the [pull request #21](https://github.com/jeremytammik/RevitLookup/pull/21) for it!

Here it is with a little bit more context:

<pre class="code">
<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>
Stream(&nbsp;<span style="color:#2b91af;">ArrayList</span>&nbsp;data,&nbsp;<span style="color:#2b91af;">Definition</span>&nbsp;paramDef&nbsp;)
{
&nbsp;&nbsp;data.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;Snoop.Data.<span style="color:#2b91af;">ClassSeparator</span>(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Definition</span>&nbsp;)&nbsp;)&nbsp;);
 
&nbsp;&nbsp;data.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;Snoop.Data.<span style="color:#2b91af;">String</span>(&nbsp;<span style="color:#a31515;">&quot;Name&quot;</span>,&nbsp;paramDef.Name&nbsp;)&nbsp;);
&nbsp;&nbsp;data.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;Snoop.Data.<span style="color:#2b91af;">String</span>(&nbsp;<span style="color:#a31515;">&quot;Parameter&nbsp;type&quot;</span>,&nbsp;paramDef.ParameterType.ToString()&nbsp;)&nbsp;);
&nbsp;&nbsp;data.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;Snoop.Data.<span style="color:#2b91af;">String</span>(&nbsp;<span style="color:#a31515;">&quot;Parameter&nbsp;group&quot;</span>,&nbsp;paramDef.ParameterGroup.ToString()&nbsp;)&nbsp;);
&nbsp;&nbsp;data.Add(&nbsp;<span style="color:blue;">new</span>&nbsp;Snoop.Data.<span style="color:#2b91af;">String</span>(&nbsp;<span style="color:#a31515;">&quot;Unit&nbsp;type&quot;</span>,&nbsp;paramDef.UnitType.ToString()&nbsp;)&nbsp;);
 
&nbsp;&nbsp;<span style="color:#2b91af;">ExternalDefinition</span>&nbsp;extDef&nbsp;=&nbsp;paramDef&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">ExternalDefinition</span>;
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;extDef&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;Stream(&nbsp;data,&nbsp;extDef&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>;
&nbsp;&nbsp;}
 
&nbsp;&nbsp;<span style="color:#2b91af;">InternalDefinition</span>&nbsp;intrnalDef&nbsp;=&nbsp;paramDef&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">InternalDefinition</span>;
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;intrnalDef&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;Stream(&nbsp;data,&nbsp;intrnalDef&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>;
&nbsp;&nbsp;}
}
</pre>

Check out the newest release in the [RevitLookup GitHub repository](https://github.com/jeremytammik/RevitLookup).

I am looking forward to your pull requests to add further enhancements that are important for you.


#### <a name="4"></a>Truss Members and FamilyInstance Sub-Components

I have been pretty active lately in
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160),
and so have Matt Taylor and Frank 'Fair59', who provided many important answers that I was not aware of.

I'll pick up two of Franks nice succinct answers here today.

The first is on retrieving the [components of a truss](http://forums.autodesk.com/t5/revit-api-forum/component-of-a-truss/m-p/6845784):

**Question:** I would like to get the components of a truss. 
Using the API, I tried via the `GroupId` property, but it doesn't work.
And in general, the components of a family.

**Answer:** Components of a truss:

<pre class="code">
  Autodesk.Revit.DB.Structure.<span style="color:#2b91af;">Truss</span>&nbsp;_truss;
  List&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;_members&nbsp;=&nbsp;_truss.Members.ToList&lt;<span style="color:#2b91af;">ElementId</span>&gt;();
</pre>

In general, for user created families:

<pre class="code">
  <span style="color:#2b91af;">FamilyInstance</span>&nbsp;_instance;
  List&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;_members&nbsp;=&nbsp;_instance.GetSubComponentIds()
    .ToList&lt;<span style="color:#2b91af;">ElementId</span>&gt;();
</pre>


#### <a name="5"></a>GeometryObject Layer Name

The second nice succinct answer by Frank 'Fair59' is on
the [`GeometryObject` Layer Name](http://forums.autodesk.com/t5/revit-api-forum/geometryobject-layer-name/m-p/6835165):

**Question:** I can loop the objects of a linked CAD file like this:

<pre class="code">
  <span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">GeometryObject</span>&nbsp;geometryObj&nbsp;<span style="color:blue;">in</span>&nbsp;
  &nbsp;&nbsp;dwg.get_Geometry(&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Options</span>()&nbsp;)&nbsp;)
  {
  }
</pre>

How can I access the layer name of the object?

**Answer:** The information is contained in the `GraphicalStyle` element:
 
<pre class="code">
  <span style="color:#2b91af;">GraphicsStyle</span>&nbsp;gStyle&nbsp;=&nbsp;document.GetElement(
    geometryObj.GraphicsStyleId&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">GraphicsStyle</span>;
</pre>

The layer name is provided by `gStyle.GraphicsStyleCategory.Name`.

