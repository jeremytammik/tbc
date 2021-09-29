<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Revit Category Guide
  https://docs.google.com/spreadsheets/d/1uNa77XYLjeN-1c63gsX6C5D5Pvn_3ZB4B0QMgPeloTw/edit#gid=1549586957
  Category Name	- Built In Enum -	User Mapped	- Display Category Name	- Display Category Name (Rus)
  
- Additional .dll files as resource
  https://forums.autodesk.com/t5/revit-api-forum/additional-dll-files-as-resource/m-p/10653802#M58650
  ricaun in reply to: antonio.hipolito
  @jrothMEIand @antonio.hipolito you could use Fody.Costura to embed the .dll references automatically, the Costura.Template has the ILTemplate.cs and Common.cs to handle all the load resources files, if the Assembly is already loaded the code does not force it to load again.
  @jeremy.tammik I use this technic on the ConduitMaterial and others plugins.
  Adding... ILTemplate.Attach(); on the IExternalApplication should do the trick.

- quote
  “Every man has two lives, and the second starts when he realizes he has just one.” — Confucius, courtesy of
  Ehsan @eirannejad https://twitter.com/eirannejad

- ExportCncFab updated for Revit 2022 https://github.com/jeremytammik/ExportCncFab/releases/tag/2022.0.0.0
  ExportCncFab eliminated deprecated API usage https://github.com/jeremytammik/ExportCncFab/releases/tag/2022.0.0.1

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

Before diving in, here is a nice little snippet of wisdom, courtesy
of [Ehsan @eirannejad](https://twitter.com/eirannejad):

<blockquote>
<p><i>Every man has two lives; the second starts when he realizes he has just one.</i></p>
<p style="text-align: right; font-style: italic">&ndash; Confucius</p>
</blockquote>
  



####<a name="2"></a> 

Chris Hildebran pointed out that 'Apply code changes' now works when debugging and editing a Revit add-in:

I'm writing as a result of a discovery I saw in Visual Studio today.
That discovery is the 'Apply Code Changes' button located to the right of the Start/Continue button:

<center>
<img src="img/apply_code_changes.png" alt="Apply code changes" title="Apply code changes" width="348"/> <!-- 348 -->
</center>

I gather it has been available for C++ for quite a while but just recently for .NET projects as of Visual Studio Version 16.11.0 Preview 1.0.

While debugging, I thought id see if this would work in Revit Addin Development.
 
Initial testing confirmed that it does indeed apply code changes which can be seen in a video demonstrating the modification of an add-in tool I'm working on &ndash; in C# at least; Need to test `.xaml`.
 
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


