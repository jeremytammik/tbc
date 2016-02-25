<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="run_prettify.js" type="text/javascript"></script>
<!---
<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
-->
</head>

<!---

11486046 [API Constraint management] on how to debug a complex issue

added this as a P.P.S. to the reproducible case blurb http://thebuildingcoder.typepad.com/blog/about-the-author.html#1b

#dotnet #csharp
#fsharp #python
#grevit
#responsivedesign #typepad
#ah8 #augi #dotnet
#stingray #rendering
#3dweb #3dviewAPI #html5 #threejs #webgl #3d #mobile #vr #ecommerce
#Markdown #Fusion360 #Fusion360Hackathon
#javascript
#RestSharp #restAPI
#mongoosejs #mongodb #nodejs
#rtceur
#xaml
#3dweb #a360 #3dwebaccel #webgl @adskForge
@AutodeskReCap @Adsk3dsMax
#revitAPI #bim #aec #3dwebcoder #adsk #adskdevnetwrk @jimquanci @keanw
#au2015 #rtceur
#eraofconnection

Revit API, Jeremy Tammik, akn_include

Happy Monkey, How To Ask a Question and Debug #revitAPI #3dwebcoder @AutodeskRevit @adskForge #3dwebaccel #a360 #bim #RMS @researchdigisus


I seem to become more and more fanatically didactical as time goes on.
I guess I answer too many questions, and am irritated when they are not asked in an optimal way.
One important point, of course, as in
all communication,
is to formulate your message with the receiver in mind.
I explained that for two specific cases today, and thought I would mention my suggestions here for future reference, after wishing everybody
&ndash; Happy New Year of the Monkey
&ndash; How to debug a complex issue
&ndash; How to ask a question...

-->

### Happy Monkey, How To Ask a Question and Debug

I seem to become more and more fanatically didactical as time goes on.

I guess I answer too many questions, and am irritated when they are not asked in an optimal way.

One important point, of course, as in
all [communication](https://en.wikipedia.org/wiki/Communication),
is to formulate your message with the receiver in mind.

I explained that for two specific cases today, and thought I would mention my suggestions here for future reference, after wishing everybody

- [Happy New Year of the Monkey!](#2)
- [How to debug a complex issue](#3)
- [How to ask a question](#4)


#### <a name="2"></a>Happy New Year of the Monkey!

Our Chinese colleagues are already in full celebration of the Chinese New Year.
It began yesterday, February 8.
In fact, the whole of this week is a holiday in China and our offices there.

In case of interest, check out the [Chinese New Year impressions](http://thebuildingcoder.typepad.com/blog/2012/01/chinese-new-year-impressions.html) from the Year of the Dragon back in 2012.

This year is Monkey, charming, charismatic and extremely inventive.

<center>
<img src="img/year_of_the_monkey.jpeg" alt="Happy New Year of the Monkey!" width="636">
</center>

This [Chinese Zodiac](http://www.chinahighlights.com/travelguide/chinese-zodiac/monkey.htm) provides descriptions of the different types of monkey and enables you to look up your own Chinese Zodiac sign.

The Chinese New Year is also called Spring Festival.

After the holiday, Spring will come, the start of one more year.

Happy New Year!


#### <a name="3"></a>How to Debug a Complex Issue

People frequently raise questions about some programming problem in their Revit add-ins and try to find help for that from others.

This help is often hard to provide due to the complexity of the issue.

Such a point arose again today in the Revit API discussion
on [API constraint management](http://forums.autodesk.com/t5/revit-api/api-constraint-management/td-p/6021191/jump-to/first-unread-message).

Here is my recommendation on how to approach this, which I now also added as a post-post-scriptum to the [instructions for a reproducible test case](http://thebuildingcoder.typepad.com/blog/about-the-author.html#1b):

I would love to dive in and try to help you debug this, but I am sorry to say I do not have the time.

You will have to continue exploring it yourself.

All I can suggest is to keep at it.

One approach to debugging a problem like this is:

1. Simplify it down to something absolutely trivial and stupid that is guaranteed to work &ndash; dumb it down.
2. Once that is working, add the required complications one by one until it either works completely or fails.

Once you have determined the exact point of failure, you can narrow that down further and create a <u>minimal</u> [reproducible case](http://thebuildingcoder.typepad.com/blog/about-the-author.html#1b).

With the minimal reproducible case in hand, I can either take a look myself of pass it on to the Revit development team for further analysis.

Thank you!

**Addendum:** You might also want to read another example of how to handle this kind of issue that just came up in the Revit API discussion forum thread
on [using DMU to retrieve in-place mass reference level](http://forums.autodesk.com/t5/revit-api/retrive-in-place-mass-reference-level/m-p/6050735).


#### <a name="4"></a>How to Ask a Question

In a related vein, I often receive requests by email or otherwise to solve a programming issue, or, in some cases, assess the possibility of implementing a programmatic solution for some given task.

Here is my most recent reaction to such a request:

- Ask a question on this in the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api/bd-p/160) instead of asking me directly.
- Make sure that your issue is formulated in a generic way.
- Strip out as much  complexity as possible. [KISS](https://en.wikipedia.org/wiki/KISS_principle)!

Posting an issue in public enables me to take a look at it, my colleagues as well, and the entire Revit developer community also.

My standard motivation blurb for goes like this:

> I do nothing at all that cannot be shared and published, and share and publish absolutely everything I work on.

> I therefore avoid discussing questions like this, and actually all questions whatsoever, one-on-one.

> I prefer discussing everything in public and enabling the entire community to contribute and share.

> That has several advantages. Among others, your peers can join in and help you, and our conversation becomes visible to others, to help them resolve their issues as well.

In addition to formulating a generic question and publishing it on the forum, a minimal [reproducible case](http://thebuildingcoder.typepad.com/blog/about-the-author.html#1b) can also help a lot.

Put together an absolutely minimal example that clearly demonstrates the problem and also makes it easy to check whether a solution really achieves the desired result.

That will help people understand the issue better and motivate them much more to explore and chip in and help.
