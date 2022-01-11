<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- RevitExtensions
Nice3point
Hi guys, it's time to pump Revit API.
I started developing a library that will make it easier to write code using extensions.
In general, instead of `Method(Method(Method(Method(Method()))))`, you can write `Method.Method.Method.Method.Method`.
And, of course, I added a couple of new methods and overloads that are not in the API.
Working with the Ribbon and *Utils classes has been greatly simplified.
If you have any suggestions for improving the API, write to me about it:

https://github.com/Nice3point/RevitExtensions

> Improve your experience with Revit API now

Extensions minimize the writing of repetitive code, add new methods not included in RevitApi, and also allow you to write chained methods without worrying about API versioning:

new ElementId(123469)
.ToElement(document)
.GetParameter(BuiltInParameter.DOOR_HEIGHT)
.AsDouble()
.ToMillimeters()
.Round()

Extensions include annotations to help ReShaper parse your code and signal when a method may return null or the value returned by the method is not used in your code.

twitter:


twitter:

RevitExtensions simplifies and enhances #RevitAPI coding with extension methods, greatly simplified Ribbon and *Utils classes and other enhancements #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon https://bit.ly/rvtextensions

Off we go into a new adventurous year of BIM programming
&ndash; Happy New Year
&ndash; RevitExtensions...

linkedin:

RevitExtensions simplifies and enhances #RevitAPI coding with extension methods, greatly simplified Ribbon and *Utils classes and other enhancements

https://bit.ly/rvtextensions

Off we go into a new adventurous year of BIM programming:

- Happy New Year
- RevitExtensions...

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Happy New Year with RevitExtensions

Off we go into a new adventurous year of BIM programming:

- [Happy New Year](#2)
- [RevitExtensions](#3)

####<a name="2"></a> Happy New Year

Happy New Year and welcome to 2022!

I spent a pleasant New Year's Day climbing the Wildhauser Schafberg in warm and dry weather.

<center>
<img src="img/143943_jeremygipfelfahnengallionsfigur.jpg" alt="Jeremy figurehead on Wildhauser Schafberg" title="Jeremy figurehead on Wildhauser Schafberg" width="300"/> <!-- 800 -->
</center>

Afterwards, a friend pointed out this rather humorous New Year's greeting from 1883.
It renders better in its original German version than in English:

<p style="text-align: center; font-weight: bold">Neujahrsgebet</p>

<p style="text-align:center">Herr, setze dem Überfluss Grenzen
<br/>und lasse die Grenzen überflüssig werden.
<br/>Lasse die Leute kein falsches Geld machen
<br/>und auch Geld keine falschen Leute.
<br/>Nimm den Ehefrauen das letzte Wort
<br/>Und erinnere die Ehemänner an ihr erstes.
<br/>Schenke unseren Freunden mehr Wahrheit
<br/>und der Wahrheit mehr Freunde.
<br/>Gib den Regierenden ein besseres Deutsch
<br/>Und den Deutschen eine bessere Regierung.
<br/>Herr, sorge dafür, dass wir alle in den Himmel kommen
<br/>Aber nicht sofort!</p>

<p style="text-align:center; font-style: italic">Lord, please border abundance
<br/>and make borders superfluous.
<br/>Don't let people make bad money
<br/>and don't let money make bad people.
<br/>Take the last word away from the wives
<br/>And remind the husbands of their first.
<br/>Give more truth to our friends
<br/>and more friends to the truth.
<br/>Give the rulers a better German
<br/>And better rulers to the Germans.
<br/>Lord, please may we all go to heaven
<br/>but not right away!</p>

<p style="text-align:right; font-style: italic">Parish priest Hermann Josef Kappen of Lamberti church in Münster, 1883</p>

<center>
<img src="img/neujahrsgruss_1883.jpg" alt="Neujahrsgruss 1883" title="Neujahrsgruss 1883" width="500"/> <!-- 1130 -->
</center>

####<a name="3"></a> RevitExtensions

In the last post of the previous year, I mentioned 
Roman [Nice3point](https://github.com/Nice3point), his huge contributions
to [RevitLookup](https://github.com/jeremytammik/RevitLookup) in the past few months,
his [RevitTemplates update 1.7.0](https://thebuildingcoder.typepad.com/blog/2021/12/revittemplates-update-170.html)
and the invitation to provide feedback on them.

Let's move into this new year with yet another contribution and invitation from Roman:

Hi guys, it's time to pump the Revit API.
I started developing a library that will make it easier to write code using extensions.
In general, instead of `Method(Method(Method(Method(Method()))))`, you can write `Method.Method.Method.Method.Method`.
And, of course, I added a couple of new methods and overloads that are not in the API.
Working with the Ribbon and *Utils classes has been greatly simplified.
If you have any suggestions for improving the API, write to me about it in
the

<p style="text-align:center"><a href="https://github.com/Nice3point/RevitExtensions">RevitExtensions GitHub repository</a>.</p>

> Improve your experience with Revit API now

> Extensions minimize the writing of repetitive code, add new methods not included in the native Revit API, and also allow you to write chained methods without worrying about API versioning:

> <pre class="code">
  new ElementId(123469)
    .ToElement(document)
    .GetParameter(BuiltInParameter.DOOR_HEIGHT)
    .AsDouble()
    .ToMillimeters()
    .Round()
</pre>

> Extensions include annotations to help ReShaper parse your code and signal when a method may return null, or the value returned by the method is not used in your code.

Many thanks again to Roman for all his tremendous work supporting and enhancing Revit API development!

And, again, Happy New Year to all!
