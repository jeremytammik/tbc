<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- tired of struggling with the Revit add-in security warning about an unsigned add-in saying, the publisher of this add-in could not be verified?
KONRAD SOBON of [archi+lab](https://archi-lab.net) explains how
to [Create a Self-signed Code Signing Certificate](https://archi-lab.net/creating-a-self-signed-code-signing-certificate) for free, valid for the next 17 years or so:
> ... <i>[detailed explanation]</i> ... That’s it! That should create your PFX file that you can now use with signtool, and code sign your Revit plugins for free! This self signed code signing certificate won’t expire for another 17 years so you should be good to go for a while.
Now, be aware of the fact that this self-signed code signing certificate is not the same as one issued to you by a 3rd party. I guess the level of “trust” here would be a little different, but in this particular case, I don’t think it matters to me. I am fed up with paying money to companies that have just atrocious customer support. If you are using these code signing certificates literally to just sign Revit plugins, then there is no reason to obtain one from a 3rd party and pay a hefty price for it on top of all the hoops that they will make you jump through.
I hope this helps some of the AEC development community out there save some money and time.

- main discussion forumthread on resolving this + other threads

- thesde threads mentioined before?

- code snippets
Maycon Freitas
Architect | Dynamo, Revit API Developer & Forge Enthusiast | Blossom Consult
Maycon Freitas  5:23 PM

Hi Jeremy, how are you?
I'm creating this repository on github to share Revit API code snippets with our Revit developers community.
If you're interested in contributing somehow, I would really appreciate.
The idea is to create an open source project to help developers to improve coding performance.
Best regards,
Maycon Freitas.

https://youtu.be/moD7CYUkJHw

RevitAPISnippets: 170+ code lines in 2 minutes (Revit API)

18 May 2022
An example of the use of Revit API snippet codes in Visual Studio.
Using snippets to improve coding performance in Revit API development.

Github Repo:

https://github.com/mayconrfreitas/RevitAPISnippets

Develop Branch with the snippets:

https://github.com/mayconrfreitas/RevitAPISnippets/tree/develop/Snippets/RevitAPI2020

- batch processing and monitoring progress
  Way to check if family is corrupt
  https://forums.autodesk.com/t5/revit-api-forum/way-to-check-if-family-is-corrupt/m-p/11174180

twitter:

the #RevitAPI SDK @AutodeskForge @AutodeskRevit #bim #DynamoBim #ForgeDevCon 

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

### Snippets, Batch and Self-Signing

Picking up some specaly interesting topics from
the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) and
elsewhere:


####<a name="2"></a> 


####<a name="3"></a> 


####<a name="4"></a> 

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- 1345 -->
</center>



<pre class="code">
  
</pre>




####<a name="5"></a> 

**Question:** 

**Answer:** 



- tired of struggling with the Revit add-in security warning about an unsigned add-in saying, the publisher of this add-in could not be verified?
KONRAD SOBON of [archi+lab](https://archi-lab.net) explains how
to [Create a Self-signed Code Signing Certificate](https://archi-lab.net/creating-a-self-signed-code-signing-certificate) for free, valid for the next 17 years or so:
> ... <i>[detailed explanation]</i> ... That’s it! That should create your PFX file that you can now use with signtool, and code sign your Revit plugins for free! This self signed code signing certificate won’t expire for another 17 years so you should be good to go for a while.
Now, be aware of the fact that this self-signed code signing certificate is not the same as one issued to you by a 3rd party. I guess the level of “trust” here would be a little different, but in this particular case, I don’t think it matters to me. I am fed up with paying money to companies that have just atrocious customer support. If you are using these code signing certificates literally to just sign Revit plugins, then there is no reason to obtain one from a 3rd party and pay a hefty price for it on top of all the hoops that they will make you jump through.
I hope this helps some of the AEC development community out there save some money and time.

- main discussion forumthread on resolving this + other threads

- thesde threads mentioined before?

####<a name="5"></a> Revit API Code Snippet Repository

Maycon Freitas, architect, Dynamo, Revit API Developer and Forge enthusiast at [Blossom Consult](https://www.blossomconsult.com),
shares a new collection of Revit API code snippets and invites the community to join in:

> I'm creating the [RevitAPISnippets GitHub repository](https://github.com/mayconrfreitas/RevitAPISnippets) to
share Revit API code snippets with our Revit developer community.
If you're interested in contributing somehow, I would really appreciate.
The idea is to create an open source project to help developers to improve coding performance.

More about this project in the two-and-a-half-minute video
on [RevitAPISnippets: 170+ code lines in 2 minutes (Revit API)](https://youtu.be/moD7CYUkJHw).

####<a name="5"></a> Batch Processing and Monitoring Progress

[Way to check if family is corrupt](https://forums.autodesk.com/t5/revit-api-forum/way-to-check-if-family-is-corrupt/m-p/11174180)
