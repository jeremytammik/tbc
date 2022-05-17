<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- REX -- https://autodesk.slack.com/archives/C0SR6NAP8/p1651840528454499

- stefan dobre https://autodesk.slack.com/archives/C0SR6NAP8/p1651735070039959

twitter:

 the #RevitAPI @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

&ndash; 
...

linkedin:

#bim #DynamoBim #ForgeDevCon #Revit #API #IFC #SDK #AI #VisualStudio #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600" height=""/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### Structural Questions and Future of REX

####<a name="2"></a> Future of REX

Tomasz Wojdyla  14:35
Hi Team!
Tomek is here :wink: I am a fairly new PO for Robot Structural Analysis trying to find myself in the organization - sorry for silly questions - there could be more than a few in the next weeks and months :wink:
Recently we started to wonder about the future for Revit Extension Framework (aka. REX) which we support in yearly basis even though (we believe) there is no internal use of this component anymore. External use is some sort of the question mark - it would be great if we could collect some data about its (external) users. We know one for sure and 2 more who are more than likely no longer with REX/Autodesk toolstack. Is there any chance you may share some info on this topic, maybe know some ADN users?
All info or advisory is greatly appreciated!:thankyou:
Cheers,
TW

However, whatever we decide to do, I would suggest proceeding in the standard Revit API manner by posting very clear signs in the SDK announcing that REX is deprecated in one release and then removing it as obsolete in the next, one year later.

Jeremy Tammik  5 days ago
Alternatively, how about simply sharing the entire REX code base right now and telling the developer community: here guys, this is what we have, we will no longer deal with it, it is completely unsupported, do what you like with it?

Jeremy Tammik  5 days ago
In my (naive) eyes, that would cost zero effort and have immediate effect.

Tomasz Wojdyla  5 days ago
Thank you for feedback Jeremy! At this moment all options are open. As we discussed it with team the 1 year notice manner is clear. I will post on the forums. Of course any more thoughts from everyone are welcome. Thanks again!

####<a name="3"></a> Rebar API Questions

@Stefan Dobre, @Pawel Piechnik, we received several serious and well-founded questions on rebar API for one developer, including detailed descriptions, code and sample images:

- [AreElementsValidForMultiReferenceAnnotation](https://forums.autodesk.com/t5/revit-api-forum/areelementsvalidformultireferenceannotation/td-p/11148745)
- [GetCustomDistributionPath from RebarFreeFormAccessor](https://forums.autodesk.com/t5/revit-api-forum/getcustomdistributionpath-from-rebarfreeformaccessor/td-p/11148790)
- [Number of segments](https://forums.autodesk.com/t5/revit-api-forum/number-of-segments/td-p/11148840)
- [IsRebarInSection()](https://forums.autodesk.com/t5/revit-api-forum/isrebarinsection/td-p/11148854)

Rather than copying them over here one by one, would it be possible for you or other rebar API experts to take a look at them and respond directly in the forum?

Or what else would you suggest as the most effective way to handle this? Thank you!

Hi @Jeremy Tammik. I take quick look on these questions and I don't see any problems here. It is just a misunderstanding of the API and how the rebar works. I'll respond to them directly on the forum.

Jeremy Tammik  11 days ago
thank you ever so much, @Stefan Dobre! very kind of you!

Stefan Dobre  10 days ago
I answered on the forum to all of these questions and I entered REVIT-191469 to update the description and (maybe) the name of the Rebar.IsRebarInSection() function.

Jeremy Tammik  5 days ago
brilliant! thank you very much! i will take a look at your answers and see whether i can convert them to a blog post to ensure they are preserved.

####<a name="4"></a> AreElementsValidForMultiReferenceAnnotation

- [AreElementsValidForMultiReferenceAnnotation](https://forums.autodesk.com/t5/revit-api-forum/areelementsvalidformultireferenceannotation/td-p/11148745)

####<a name="5"></a> GetCustomDistributionPath

- [GetCustomDistributionPath from RebarFreeFormAccessor](https://forums.autodesk.com/t5/revit-api-forum/getcustomdistributionpath-from-rebarfreeformaccessor/td-p/11148790)

####<a name="6"></a> Number of Segments

- [Number of segments](https://forums.autodesk.com/t5/revit-api-forum/number-of-segments/td-p/11148840)

####<a name="7"></a> IsRebarInSection

- [IsRebarInSection()](https://forums.autodesk.com/t5/revit-api-forum/isrebarinsection/td-p/11148854)

<center>
<img src="img/.jpg" alt="" title="" width="100"/> <!-- 386 -->
</center>

