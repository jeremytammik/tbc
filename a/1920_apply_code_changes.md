<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

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

### Apply Code Changes

####<a name="2"></a> 

Chris Hildebran pointed out that 'Apply code changes' now works when debugging and editing a Revit add-in:

I'm writing as a result of a discovery I saw in Visual Studio today.
That discovery is the 'Apply Code Changes' button located to the right of the Start/Continue button:

<center>
<img src="img/apply_code_changes.png" alt="Apply code changes" title="Apply code changes" width="348"/> <!-- 348 -->
</center>

I gather it has been available for C++ for quite a while but just recently for .NET projects as of Visual Studio Version 16.11.0 Preview 1.0.

While debugging, I thought id see if this would work in Revit Addin Development.
 
Initial testing confirmed that it does indeed apply code changes which can be seen in a video demonstrating the modification of an add-in tool I'm working on. (in c# at least. Need to test .xaml)
 
I've recorded a [two-minute video](https://www.screencast.com/t/5oCj1jBJha) demonstrating the initial test, which I hope is clear enough to see.
 
I had planned to implement a solution Josh Lumley proposed, but if this continues to work, I will continue using this feature to drastically speed up development.
 
Perhaps I'm late to the party, but I thought I'd mention it anyway.
 
Here is
the Microsoft article [introducing the .NET Hot Reload experience for editing code at runtime](https://devblogs.microsoft.com/dotnet/introducing-net-hot-reload).

Many thanks to Chris for sharing this!

For completeness, The Building Coder topic group
on [debuging without restart and live development](https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.49) discusses
how 'Edit and Continue' used to work way back in Revit 2008 and various other solutions suggested in the meantime.


####<a name="3"></a> 


####<a name="4"></a> 


<pre class="code">

</pre>


