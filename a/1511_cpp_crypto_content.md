<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!--
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

http://forums.autodesk.com/t5/revit-api-forum/remaking-cad-objects-necessary/m-p/6752234

 #RevitAPI @AutodeskRevit #aec #bim #dynamobim @AutodeskForge

Happy New Year to one and all! I had a great break over Christmas and New Year and hope you did as well. I continued checking into the Revit API discussion forum throughout and had several interesting discussions with the busy souls hanging out there
&ndash; C++, audio, crypto and security
&ndash; Prayer of the Mothers
&ndash; Avoid remaking CAD content...

-->

### Happy New Year, C++, Crypto and Content

Happy New Year to one and all!

<center>
<p id="dis" style="font-size: 150%; font-weight: bold; font-family: monospace; color:orange">Happy New Year!</p>
</center>

<!-- The JavaScript Source http://www.javascriptsource.com Ben Joffe http://www.joffe.tk/ -->

<script>

var text="Happy New Year!";

var symtype=new Array(" ","A","a","B","b","C","c","D","d","E","e","F","f","G","g","H","h","I","i","J","j","K","k","L","l","M","m","N","n","O","o","P","p","Q","q","R","r","S","s","T","t","U","u","V","v","W","w","X","x","Y","y","Z","z","0","1","2","3","4","5","6","7","8","9",".",",","&","!","?","-","_");

var symarray=new Array();

for (var i=0; i<text.length; i++){
  symarray[i]=" ";
}
function scroll(){
  for (var i=0; i<text.length; i++){
    if (symarray[i]!=text.substring(i,i+1)) {
      for (var x=0; x<70; x++) if (symarray[i]==symtype[x]) {symarray[i]=symtype[x+1]; break}
    }
  }
  var outsym="";
  for (var i=0; i<text.length; i++) outsym+=symarray[i];
  dis.innerHTML=outsym;
  setTimeout('scroll()',100);
}
scroll();

</script>

I had a great break over Christmas and New Year and hope you did as well.

I did continue checking into
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) anyway
throughout and had several interesting discussions with the busy souls hanging out there.

Some topics to get started with the year:

- [C++, audio, crypto and security](#2)
- [Prayer of the Mothers](#3)
- [Avoid remaking CAD content](#4)


####<a name="2"></a>C++, Audio, Crypto and Security

I obviously chatted with my kids, some of them rather dispersed.

[Chris](https://twitter.com/chtammik) is embarking on new programming adventures in C++ land.

He pointed out a couple of cool projects he is delving into, e.g.,
the [OpenAL](http://www.openal.org) cross-platform 3D audio API and 
the [PureData](http://puredata.info) open source visual programming language that can run on anything from personal computers and Raspberry Pis to smartphones, enables you to create software graphically without writing any code, and can generate output in multiple formats, including C++ suitable for OpenAL and C# suitable for Unity programming.

Cool stuff.

He also pointed out the
funny [presentation of a favourite C++ feature](https://www.youtube.com/watch?v=6eX9gPithBo) by James McNellis
and a fundamental [Crypto 101](https://www.youtube.com/watch?v=3rmCGsCYJF8) by
Laurens Van Houtven at PyCon.

A friend of my daughter, Appenzeller, implemented
an [online C++ password strength evaluator](https://appenzeller.servebeer.com/pwco) complete with a visualisation of the decomposition graph
and [full documentation and source code](https://appenzeller.servebeer.com).

<center>
<img src="img/password_complexity_graph.png" alt="Password complexity graph" width="542"/>
</center>


####<a name="3"></a>Prayer of the Mothers

On a less technical note, other friends shared the very moving 
song [Prayer of the Mothers](https://www.youtube.com/watch?v=YyFM-pWdqrY) by
Yael Deckelbaum featuring singers from all sectors and religions of Israeli society:

<center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/YyFM-pWdqrY?rel=0" frameborder="0" allowfullscreen></iframe>
</center>

It was born as a result of the movement 'Women Wage Peace', which arose during an escalation of violence between Israel and Palestine, prompting Jewish and Arab women to begin with the joint 'March of Hope' project.


####<a name="4"></a>Avoid Remaking CAD Content

Let's close with a Revit API related discussion on how to avoid
the [remaking of CAD objects](http://forums.autodesk.com/t5/revit-api-forum/remaking-cad-objects-necessary/m-p/6752234):

**Question:** We are an office furniture manufacturer that purchased the Revit software in order to offer our customers our products in the .rfa format.  We have hundreds of products in CAD and various other 3D formats.  We've been told we would need to remake every one of our products in Revit from scratch.  However, I've been able to import our CAD files and work with them in the software but I was told that because they weren't made natively in Revit that they're essentially useless.  Is this true and if so, what sort of training would be required to move forward?  Can't we just add the dimension information to our previously made items within Revit?  We've been quoted thousands just to make a portion of our library into Revit families and we've already spent thousands leasing the software thus far with no success.

**Answer:** Sorry to hear that this issue is troubling you.
 
I always recommend generating all such libraries completely parametrically from scratch.
 
That would have meant, way back then before you created any CAD content at all, defining a programmatic way to generate that CAD content from a database of parametric definitions and lists of dimensions of all your library parts.
 
I have been recommending that approach (mostly in vain) for about four decades now.
 
The people who listened to it back then are very happy now that they did.
 
They are all too few.
 
Most people go and implement CAD content by hand, over and over again, every time the system changes.
 
Well, so be it then.
 
If the content is generated programmatically from a database of rules and dimensions, you can simply implement a new content generator for each system that you wish to populate. The database content driving it remains unchanged.
 
Back to your issue at hand:
 
If you have tested using families based on the imported CAD geometry in Revit without problems, then that is obviously a feasible option.
 
However, you really need to ensure and verify that this works reliably under all circumstances.
 
For instance, if you are a chair manufacturer, try using your CAD-import-based family in a hospital or airport with thousands or tens of thousands of instances.
 
That may cause problems.
 
Furthermore, it might not be easy to equip the CAD-import-based parts with all the dimensions that drive their different sizes, aka family types aka symbols.
 
Here is a recommendation
to [think twice before importing CAD into RFA](http://thebuildingcoder.typepad.com/blog/2016/09/avoid-cad-import-in-rfa-aag16-and-endtrip.html#2).
 
Good luck finding an efficient and viable solution for your task!
 
**Response:** Thank you so much for your reply,

Your post along with the provided links will help me rationalise to the company why we need to create the content natively.

I'm essentially a one-man band over here and re-making all of the thousands of items from scratch, especially when coding
is not my forte, is not a feasible option.  

I greatly appreciate your reply!
