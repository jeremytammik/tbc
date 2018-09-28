<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

Protecting a family from tampering and implementing a canonical key for geometrical objects in the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon http://bit.ly/familytampering

The Forge accelerator in Rome is winding down with the demonstrations of what was achieved being recorded as I write.
An interesting conversation I had at the celebratory dinner last night gave me an idea for a solution to a longstanding question on family tampering protection
&ndash; Roma accelerator group photo
&ndash; Protecting a family from tampering
&ndash; Implementing a canonical key for geometrical objects...

-->

### Protecting a Family from Tampering

The [Forge](https://autodesk-forge.github.io)
[accelerator in Rome](http://autodeskcloudaccelerator.com)  is
winding down with the demonstrations of what was achieved being recorded as I write.

An interesting conversation I had at the celebratory dinner last night gave me an idea for a solution to a longstanding question on family tampering protection:

- [Roma accelerator group photo](#2) 
- [Protecting a family from tampering](#3) 
- [Implementing a canonical key for geometrical objects](#4) 


#### <a name="2"></a> Roma Accelerator Group Photo

I missed the Roma Accelerator group photo by a few seconds:

<center>
<img src="img/accelerator_roma_participants_400x300.jpg" alt="Roma accelerator participants lacking Jeremy" width="400">
</center>

In what certainly must be one of the worst photoshop jobs ever, Jaime very kindly added me to it afterwards:

<center>
<img src="img/accelerator_roma_participants_plus_jeremy_400.jpg" alt="Roma accelerator participants plus Jeremy" width="400">
</center>

Thank you for that, Jaime! &nbsp; Keep practicing...


#### <a name="3"></a> Protecting a Family from Tampering

Here are two different aspects of protecting Revit families:

- Copy protection, to stop the competition from stealing them and the [IP or intellectual property](https://en.wikipedia.org/wiki/Intellectual_property) they contain.
- Tampering protection, to stop the customer from corrupting her own models, intentionally or not.

Often discussed, various solutions suggested.

For example, we discussed one example of a copy protection solution  
by [simplifying nested family instances](http://thebuildingcoder.typepad.com/blog/2018/06/simplifying-nested-family-instances.html) to
protect the intellectual property built into a complex hierarchy of nested family instances by replacing them with a flatter and simpler hierarchy, yet retaining all the relevant non-confidential custom data.

Here is an AUGI discussion from 2009 on the pros and cons
of [protecting Revit families](http://forums.augi.com/showthread.php?73233-Protecting-Revit-Families).

Today, I make a suggestion to protect from tampering, nothing totally safe yet afaik.

You want a fool-proof method to protect your family definitions from tampering?

You can encode a secret hash code or something and hide it somewhere.

For brevity, let's call this secret hash code your <u>key</u>.

However, the tamperer might just retain it, either intentionally or not.

How can you still detect tampering?

Well, you can easily encode bits and pieces of whatever she might want to tamper with into you key as well.

Especially, you should ensure that you generate a hash key or something that includes references to every single bit of data that is relevant for you.

If you have an algorithm to rebuild your key at will from the current state of the family, you can check the following:

- Does the original key exist? If not, alarm.
- Recompute the current key from the current state of the family. does it match the stored key? If not, alarm.

[Creating your own key](http://thebuildingcoder.typepad.com/blog/2012/03/great-ocean-road-and-creating-your-own-key.html#2) can
be useful in numerous ways, so I already discussed this topic and suggested doing so back in 2012.

Therefore, I hope that all the families that you care about are already adequately protected against tampering.

If not, go ahead and do so now.


#### <a name="4"></a> Implementing a Canonical Key for Geometrical Objects

To further illustrate what I mean by the key I mentioned above, let's illustrate how to generate a checksum or hash code from geometry, preferably using
a [canonical form](https://en.wikipedia.org/wiki/Canonical_form).

Let's explore defining a simple canonical form for a point, line (two ordered points), curve, etc.

A very simple and useful way to go is to define your canonical form as a string.

You can use this string as a dictionary key, if that fits your needs.

You can also calculate a hash from it, which should ensure that any change in the string will also cause a change in the hash.

To ensure that this works, you need to create a hash of sufficient length.

If you care about protection, you might also want to encrypt the hash after you have created it.

Here are some simplistic examples of defining canonical forms for real numbers, points, lines, and curves:

- Real number `a`, using as many decimal places as required to achieve the precision you need:

<pre class="code">
  <span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;RealString(&nbsp;<span style="color:blue;">double</span>&nbsp;a&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;a.ToString(&nbsp;<span style="color:#a31515;">&quot;0.######&quot;</span>&nbsp;);
  }
</pre>

- `XYZ p`:

<pre class="code">
  <span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;PointString(&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;p&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">string</span>.Format(&nbsp;<span style="color:#a31515;">&quot;({0},{1},{2})&quot;</span>,
  &nbsp;&nbsp;&nbsp;&nbsp;RealString(&nbsp;p.X&nbsp;),
  &nbsp;&nbsp;&nbsp;&nbsp;RealString(&nbsp;p.Y&nbsp;),
  &nbsp;&nbsp;&nbsp;&nbsp;RealString(&nbsp;p.Z&nbsp;)&nbsp;);
  }
</pre>

- A sorted array of points:

<pre class="code">
  <span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;PointArrayString(&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">XYZ</span>&gt;&nbsp;pts&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">string</span>.Join(&nbsp;<span style="color:#a31515;">&quot;,&nbsp;&quot;</span>,
  &nbsp;&nbsp;&nbsp;&nbsp;pts.Select&lt;<span style="color:#2b91af;">XYZ</span>,&nbsp;<span style="color:blue;">string</span>&gt;(
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p&nbsp;=&gt;&nbsp;PointString(&nbsp;p&nbsp;)&nbsp;)&nbsp;);
  }
</pre>

- A bounded line `(p,q)`:
    - Sort `p` and `q` lexicographically
    - Return `PointArrayString( [p,q] )`
- A curve `c`:
    - Return `PointArrayString( c.Tessellate() )`

Most of these are implemented
in [The Building Coder samples](https://github.com/jeremytammik/the_building_coder_samples)
[Util module](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/Util.cs).


I'll leave it up to your imagination to improve these and define canonical keys for more complex data as required.

Migliori saluti a Stefano!

