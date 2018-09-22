<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- http://keanw.com/2018/09/counting-down-to-forge-devcon-europe.html

- http://thebuildingcoder.typepad.com/blog/2018/09/roadmap-ci-for-rtf-geometry-library-limitations.html#comment-4106874384

- google custom search engine CSE
Copy the following code, and paste it into a <div> element in your site's <body> section, where you want both of the search box and the search results to render.
Note: For the most cross-browser compatibility, it is recommended that your HTML pages use a supported doctype such as <!DOCTYPE html>. CSS hover effects require a supported doctype.

<script>
  (function() {
    var cx = '010209790528060308176:r44wkbirunu';
    var gcse = document.createElement('script');
    gcse.type = 'text/javascript';
    gcse.async = true;
    gcse.src = 'https://cse.google.com/cse.js?cx=' + cx;
    var s = document.getElementsByTagName('script')[0];
    s.parentNode.insertBefore(gcse, s);
  })();
</script>
<gcse:search></gcse:search>

<html>
<head>
<title>my site</title>
...
<head>
<body>
<div1>...</div1>
PASTE THE CODE HERE
<div2>...</div2>
</body>
</html>

You can customize the Search UI even more, or add per page customization by following the full documentation on CSE element.

https://developers.google.com/custom-search/docs/element

Description: The Building Coder Forge, BIM and Revit API blog by Jeremy Tammik
Search engine keywords: 'The Building Coder' Forge BIM 'Revit API' blog 'Jeremy Tammik'
Search engine ID: 010209790528060308176:r44wkbirunu
URL: https://cse.google.com/cse?cx=010209790528060308176:r44wkbirunu

https://www.google.com/search?q=eth+zurich&btnG=Search&domains=keanw.com&sitesearch=keanw.com

https://www.google.com/search?q=IExternalCommandAvailability&domains=thebuildingcoder.typepad.com

how to get typepad built-in search button?

<script type="text/javascript">
window.onload = function(){
var form = document.getElementById('search-blog');
var submit = form.getElementsByClassName('btn')[0];
//alert(submit);
//var searchinput = form.getElementsByName('filter.q');
var searchinput = form.getElementsByClassName('form-control')[0];
//alert(searchinput);
submit.addEventListener('click', function(event) {
  var s = searchinput.value;
  s = s.replace(/ /g, '+');
  var url = 'https://www.google.com/search?q='
    + s + '&as_sitesearch=thebuildingcoder.typepad.com';
  //alert(url);
  //window.location.href=url;
  var win = window.open(url, '_blank');
  win.focus();
});
}
</script>


<input type="button" value="Add Students" onclick="window.location.href='Students.html';"/>
<button onclick="myFunction()">Click me</button>

//get a reference to the element
var myBtn = document.getElementById('myButton');
//add event listener
myBtn.addEventListener('click', function(event) { window.location.href='Students.html'; });

https://moz.com/blog/the-ultimate-guide-to-the-google-search-parameters

the built-in typepad search module does not work.

i now implemented my own javascript version to open a google search engine in a separate tab.

what can i do to make the built-in typepad search module work as intended?

to see what i mean, please go to 

http://thebuildingcoder.typepad.com/

type "IExternalCommandAvailability" in the search input text box and click 'Submit'.

the built-in search reports that no results are found:

No Results Found -- Sorry, there were no results returned for “IExternalCommandAvailability” &ndash; please check your spelling, or try something less specific.

i open a new tab with a google search engine limited to thebuildingcoder site, and it reports many results.

Typepad answers:

Thank you for reaching out to us. We are aware search results are not displaying as expected, and we are currently working on an update to the search feature. We apologize for the inconvenience. If you have any other questions, please let us know.

I have no other questions, thank you, Typepad.

DevCon Europe and Google site search for The Building Coder and the #RevitAPI @AutodeskRevit #bim #dynamobim @AutodeskForge #ForgeDevCon http://bit.ly/devconeusitesearch

I am making final travel preparations for the Forge accelerator in Rome next week, and need to start preparing for DevCon Europe as well.
As always, when you have no time to spare, something else urgent cropped up as well requiring immediate attention
&ndash; Forge DevCon Europe coming
&ndash; Hijacking Typepad search input for Google site search...

-->

### DevCon Europe and Typepad versus Google Search

I am making final travel preparations for the Forge accelerator in Rome next week, and need to start preparing for DevCon Europe as well.

As always, when you have no time to spare, something else urgent cropped up as well requiring immediate attention:

- [Forge DevCon Europe coming](#2) 
- [Hijacking Typepad search input for Google site search](#3) 

#### <a name="2"></a> Forge DevCon Europe Coming

As Kean Walmsley just pointed out, we
are [counting down to Forge DevCon Europe](http://keanw.com/2018/09/counting-down-to-forge-devcon-europe.html),
the second European edition of the [Forge DevCon](https://forge.autodesk.com/devcon-2018).

It is being held in Darmstadt on October 16th, the day before this year’s AU Germany.

This is a free, English-language event where you can learn all about the capabilities that are currently (and soon will be) available through
the [Forge platform](https://autodesk-forge.github.io) (the
entire DevCon event is in English language, whereas the Autodesk University following it is in German).

<center>
<img src="img/forge_devcon_europe_2018.png" alt="Autodesk Forge Devcon Europe 2018" width="250">
</center>

Here are some reasons to attend, provided by the Forge team:

- Get to Know Forge &ndash; 
Take a deep dive into the Forge Viewer, BIM 360 API, Reality Capture, and industry specific solutions created with Forge APIs.
Plus, preview just-released Forge updates to integrations, end-to-end workflow capabilities, and much more.
- Disruptive Technologies &ndash; 
AR, VR, machine learning and generative design can be brought to life using Forge. Learn how, and see how easy it is to get started.
- Web Programming &ndash; 
Delve into topics from web application security to architecture to testing best practices, or brush up on the basics you need to transition from desktop to web programming.
- Forge Industry Stories &ndash; 
Take in cross-category case studies showing how Autodesk partners and customers used Forge to solve real business problems, and what they learned along the way.
- Construction Technology with Forge + BIM 360 &ndash; 
Learn how Forge and BIM 360 have been used to create countless apps across the construction industry, helping teams innovate, digitize job sites, minimize data loss and increase efficiency.

I will be there, together with Kean and many other world-wide colleagues.

You can [register today via this link](https://www.rayseven.com/r7/runtime/autodesk/devcon2018/registration.visitor.php).

Also, again like Kean, my next stop is next week in Rome at
the [Forge accelerator](http://autodeskcloudaccelerator.com).

I hope you can make it to one of our upcoming events as well.


#### <a name="3"></a> Hijacking Typepad Search Input for Google Site Search

Matt Taylor submitted
a [comment](http://thebuildingcoder.typepad.com/blog/2018/09/roadmap-ci-for-rtf-geometry-library-limitations.html#comment-4106874384)
on [yesterday's blog post](http://thebuildingcoder.typepad.com/blog/2018/09/roadmap-ci-for-rtf-geometry-library-limitations.html) pointing
out that the search functionality stopped working:

> Just a heads-up that the search box on your blog doesn't appear to be working.

> I tested in IE and Chrome.

> It used to work...

> I'll normally add the site to the search like this in a search engine:

> `  site:http://thebuildingcoder.typepad.com floor`

> ... Which is what I did when it didn't work.

The problem is caused by the built-in Typepad search module.

Just for your information, the entire blog source code is also available in parallel in
the [tbc GitHub repository](https://github.com/jeremytammik/tbc),
so you can always download from there to your own system and search there with full control and flexibility.

After some in-depth research and JavaScript twiddling, I am now opening my own Google search window in a new tab in parallel with the dysfunctional Typepad search.

I dove into the blog source code to determine and retrieve the search input text box and submit button and hijack it to open a second Google search window doing just that.

Here is the code that I added to the site to achieve this:

<pre class="prettyprint">
  &lt;script type="text/javascript"&gt;
  window.onload = function(){
    var form = document.getElementById('search-blog');
    var submit = form.getElementsByClassName('btn')[0];
    var searchinput = form.getElementsByClassName('form-control')[0];
    submit.addEventListener('click', function(event) {
      var s = searchinput.value;
      s = s.replace(/ /g, '+');
      var url = 'https://www.google.com/search?q='
        + s + '&as_sitesearch=thebuildingcoder.typepad.com';
      //window.location.href=url;
      var win = window.open(url, '_blank');
      win.focus();
    });
  }
  &lt;/script&gt;
</pre>

I initially tried to override the Typepad functionality completely using `window.location.href`, but that failed.

Therefore, I open the second Google site search window in parallel and set the focus to that instead.

By the way, I found
this [ultimate guide to the Google search parameters](https://moz.com/blog/the-ultimate-guide-to-the-google-search-parameters) useful
to determine what URL arguments to add, i.e., `as_sitesearch`.

Finally, I discussed the issue with Typepad, saying:

> the built-in typepad search module does not work.
i now implemented my own javascript version to open a google search engine in a separate tab.
what can i do to make the built-in typepad search module work as intended?
to see what i mean, please go to [thebuildingcoder.typepad.com](http://thebuildingcoder.typepad.com),
type `IExternalCommandAvailability` in the search input text box and click 'Submit'.
the built-in search reports that no results are found:

> *No Results Found &ndash; Sorry, there were no results returned for “IExternalCommandAvailability” &ndash; please check your spelling, or try something less specific.*

> i open a new tab with a google search engine limited to thebuildingcoder site, and it reports many results.

Typepad answers:

> Thank you for reaching out to us. We are aware search results are not displaying as expected, and we are currently working on an update to the search feature. We apologize for the inconvenience. If you have any other questions, please let us know.

I have no other questions, thank you, Typepad.

Thank you very much, Matt, for pointing this out!
