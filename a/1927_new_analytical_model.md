<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- Analytical Model API - Message for Public Roadmap
  New API Analytical Model
  New API to enable direct control of the structural analytical model that allows developers to operate independently the creation and modification of analytical elements, offering simplified implementations that work in abstraction from physical elements.
  new_analytical_model_api.jpg

- https://github.com/jeremytammik/RevitLookup/issues/113
  It would be nice to remove this filter and replace ids.Any() with ids.Count > 0. This will improve performance as Any creates a new Enumerator
  Regarding performance, code readability and semantic meaning are more important than performance in places where performance does not matter.
  I agree that sometimes code readability is more important, but in this case additional lines or conditions are not added. And here is the difference in speed 1200 times with memory allocation = (
  benchmark_any_vs_count.png
  wow, impressive! thank you for the conclusive benchmark. i'll add a note of that result to the blog, i think.

- Tweets of Praise for Modeless RevitLookup:
  Joshua Lumley @joshnewzealand: This is a breakthough.
  Timon Hazell @TmnHzll: These are some nice adds.  Thanks @jeremytammik
  Manjaka Rakotoarisoa @RaManjaka: A dream come true
  Jean-Marc Couffin @JeanMarcCouffin: A huge thank you to @NeVeSpl who took on my improvement request right away and made it real!
  Carl S @CarlSirid: Thanks Jeremy, RevitLookup is my favorite addin. It is the must have addin for me.
  LinkedIn
  David Wilson: Installed it yesterday. Thanks very much.
  Roy Qian: Great! 👍👍👍
  Daniel Swearson: Nice! Thanks Jeremy.
  Micheal Ibrahim: You are my hero ❤️

- use extensible storage carefully
  https://forums.autodesk.com/t5/revit-api-forum/bug-unable-to-open-revit-2019-model-after-saving-custom-schema/m-p/10736885
  
- jQuery Outdated
  https://thenewstack.io/why-outdated-jquery-is-still-the-dominant-javascript-library/

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash;
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### New Analytical Model




<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 1068 -->
</center>




####<a name="2"></a>

####<a name="3"></a>
