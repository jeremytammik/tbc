<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href="bc.css">
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>
</head>

<!---

- devcon email Marnee Dupont <marnee.dupont@autodesk.com>
  Registration for Munich DevCon on September 11th and 12th is now OPEN. Attendees can now register for both San Francisco and Munich.
  All the information you need to know as well as both registration links are in this blog post.
  We would love your help in promoting these events. We’ve created these Promo cheat sheets that have social posts, email drafts, and much more to help you spread the word. Please reach out to your customers, reshare our social media posts, and I’ve also posted on Bonfire as well.
  docx icon Munich DevCon Promo Cheat Sheet .docx
  docx icon San Francisco DevCon Promo Cheat Sheet.docx
  A couple notes:
  At this time, this event is for customers and partners ONLY. We do not have capacity to open this up to Autodesk employees at this time.
  If you are an employee who is working or speaking at the event, we have a Staff link that will be shared with you to register. Please do NOT use the registration link in the blog.

- forma
  3-minute video
  Autodesk Forma: Make tomorrow's cities
  https://youtu.be/6iKM0fsk_Jw
  <iframe width="560" height="315" src="https://www.youtube.com/embed/6iKM0fsk_Jw" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
  Start a free 30-day trial: https://www.autodesk.com/products/forma/free-trial
  Find out more about Autodesk Forma: https://www.autodesk.com/forma
  Visit the blog: http://blogs.autodesk.com/forma/

- email [jeremytammik/RevitLookup] Release 2024.0.8 - 2024.0.8

- how to compare parameters in different environments by rpthomas
  https://forums.autodesk.com/t5/revit-api-forum/how-to-determine-if-a-parameter-value-is-derived-from-a-formula/m-p/12069026#M72402

- User MEP Calculation error
  https://forums.autodesk.com/t5/revit-api-forum/user-mep-calculation-error/td-p/12063928
  tamsann  149 Views, 4 Replies
  I am researching User MEP calculation, and when I tried to run the program, I encountered some errors:
  Error: "FormatUtils doesn't exist in the current context."
  Error: "CS1061 'Selection' does not contain a definition for 'Elements' and no accessible extension method 'Elements' accepting a first argument of type 'Selection' could be found (are you missing a using directive or an assembly reference?)"
  Could someone with experience in fixing these errors please help me? I'm new to this and would greatly appreciate your assistance.
  sragan  in reply to: tamsann
  Formatutils is an obsolete command that "Formats a number with units into a string based on the units formatting settings for a document."
  You will have to find another way to do this.
  FormatUtils Members (revitapidocs.com)
  https://www.revitapidocs.com/2015/b4779336-e429-0b51-8c0e-63b5657f1810.htm
  reylorente1  in reply to: sragan
  Hola,aqui te dejo UserMepCalculation2024,no obtante, el Revit SDK 2024,tiene un Complemento(Addin) llamado NetworkPressureLossReport, que te puede servir tambien.
  Espero que te ayude,y suerte
  Hello, here I leave you UserMepCalculation2024:
  UserMepCalculation2024.rar
  /Users/jta/a/doc/revit/tbc/git/a/zip/UserMepCalculation2024.rar
  however, the Revit SDK 2024 has a Complement (Addin) called NetworkPressureLossReport, which can also be useful for you:
  > This addin sample shows how to access the MEP analytical model data and traverse the network. The flow and pressure loss results are exported to a csv file or displayed in Analysis Visualization Framework (AVF).
  I hope it helps you, and good luck

- Wastewater pipe calculation
  https://forums.autodesk.com/t5/revit-api-forum/watsewater-pipe-calculation/m-p/12075059

- The Password Game
  https://neal.fun/password-game/
  /Users/jta/a/doc/revit/tbc/git/a/img/passwordgame.png
  https://autodesk.slack.com/archives/C02NW42JP/p1688127875185579
  Chris Blocher
  A little Friday frustration fun, in a geeky sort of way. https://neal.fun/password-game/. I made it to the 11th rule before I gave up:wink: (edited)
  Please choose a password (68 kB)
  https://neal.fun/password-game/
  Phillip Doup
  I made it to the whole "find a youtube video of X mins and Y seconds" one and gave up.
  Jeremy Tammik
  nice game! my 11th rule seems to be different from yours. i gave up at this point too...

twitter:

 @AutodeskRevit #RevitAPI #BIM @DynamoBIM @AutodeskAPS

&ndash;
...

linkedin:

#BIM #DynamoBIM #AutodeskAPS #Revit #API #IFC #SDK #Autodesk #AEC #adsk

the [Revit API discussion forum](http://forums.autodesk.com/t5/revit-api-forum/bd-p/160) thread

<center>
<img src="img/" alt="" title="" width="600"/>
<p style="font-size: 80%; font-style:italic"></p>
</center>

-->

### MEP Calculation and APS DevCon

####<a name="2"></a> APS DevCon in Munich and SF

devcon email Marnee Dupont <marnee.dupont@autodesk.com>
Registration for Munich DevCon on September 11th and 12th is now OPEN. Attendees can now register for both San Francisco and Munich.
All the information you need to know as well as both registration links are in this blog post.
We would love your help in promoting these events. We’ve created these Promo cheat sheets that have social posts, email drafts, and much more to help you spread the word. Please reach out to your customers, reshare our social media posts, and I’ve also posted on Bonfire as well.
docx icon Munich DevCon Promo Cheat Sheet .docx
docx icon San Francisco DevCon Promo Cheat Sheet.docx
A couple notes:
At this time, this event is for customers and partners ONLY. We do not have capacity to open this up to Autodesk employees at this time.
If you are an employee who is working or speaking at the event, we have a Staff link that will be shared with you to register. Please do NOT use the registration link in the blog.

####<a name="3"></a> Forma Video


<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2022/09/aps-au-and-miter-wall-join-for-full-face.html">APS, AU, and Miter Wall Join for Full Face</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2023/06/sustainability-lookup-sub-elements-and-wall-types.html">Sustainability, Lookup, Sub-Elements and Wall Types</a></li>
</ul>

[Forma for AEC]()

- [AU 2022 announcement](https://thebuildingcoder.typepad.com/blog/2022/09/aps-au-and-miter-wall-join-for-full-face.html#3)
- [ustainability flyer](/Users/jta/a/doc/revit/tbc/git/a/zip/forma_sustainability_flyer.pdf):

> [Design a Better Future with Forma’s Suite of Sustainability Solutions](https://blogs.autodesk.com/forma/2023/05/08/sustainability-solutions):

> Cities consume more than two-thirds of the world’s energy and account for over 70% of global carbon emissions
([source](https://unfccc.int/news/urban-climate-action-is-crucial-to-bend-the-emissions-curve#:~:text=Cities%20consume%20over%20two%2Dthirds,Asia%20and%20Sub%2DSaharan%20Africa)).
This means architects, real estate developers, and urban planners have an exceptional opportunity to mitigate the environmental impact of our cities by designing buildings and communities with sustainable outcomes in mind.

> Sustainable outcomes are best achieved through a proactive, data-driven approach that starts at the earliest stages of design before it becomes costly and difficult to make changes.
Autodesk Forma’s powerful suite of real-time analyses equips design teams with the quick, visual insights needed to prioritize sustainability from day one of a project.

- forma
3-minute video
Autodesk Forma: Make tomorrow's cities
https://youtu.be/6iKM0fsk_Jw
<iframe width="560" height="315" src="https://www.youtube.com/embed/6iKM0fsk_Jw" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
Start a free 30-day trial: https://www.autodesk.com/products/forma/free-trial
Find out more about Autodesk Forma: https://www.autodesk.com/forma
Visit the blog: http://blogs.autodesk.com/forma/

####<a name="4"></a> RevitLookup 2024.0.8

- email [jeremytammik/RevitLookup] Release 2024.0.8 - 2024.0.8

Features

Core

Computing Time Tracking

This feature includes the ability to monitor the computing time taken to invoke a member, such as methods or properties.
By tracking the execution time, you can identify and analyze slow-performing methods or properties, gaining insights into their overall performance.
The computing time is displayed in a separate column and a tooltip, providing you with detailed information. This feature is optional and disabled by default

image

User interface

Context Menu

A convenient context menu has been added to the table, providing you with additional options to manage columns and update contents.
This menu enables you to customize your table view and effortlessly perform actions to enhance your experience.

image

Enhanced Visualization

Icons have been added to the context menu, making it more visually appealing and intuitive for users to navigate and interact with the available options.

image

Improvements

Added async support for unit dialogs
Added API for external programs #171
Added FamilyParameter support by @CADBIMDeveloper in #174
Added FamilyManager.GetAssociatedFamilyParameter extension by @CADBIMDeveloper in #175
Bugs

Fixed shortcuts reloading leading to incorrect ribbon update #177
Full changelog: 2024.0.7...2024.0.8

—
This release has 10 assets:

RevitLookup-2021.2.8-MultiUser.msi
RevitLookup-2021.2.8-SingleUser.msi
RevitLookup-2022.2.8-MultiUser.msi
RevitLookup-2022.2.8-SingleUser.msi
RevitLookup-2023.2.8-MultiUser.msi
RevitLookup-2023.2.8-SingleUser.msi
RevitLookup-2024.0.8-MultiUser.msi
RevitLookup-2024.0.8-SingleUser.msi
Source code (zip)
Source code (tar.gz)
Visit the release page to download them.



####<a name="5"></a> Comparing Parameters

- how to compare parameters in different environments by rpthomas
https://forums.autodesk.com/t5/revit-api-forum/how-to-determine-if-a-parameter-value-is-derived-from-a-formula/m-p/12069026#M72402

####<a name="6"></a> User MEP Calculation 2024

- User MEP Calculation error
https://forums.autodesk.com/t5/revit-api-forum/user-mep-calculation-error/td-p/12063928
tamsann  149 Views, 4 Replies
I am researching User MEP calculation, and when I tried to run the program, I encountered some errors:
Error: "FormatUtils doesn't exist in the current context."
Error: "CS1061 'Selection' does not contain a definition for 'Elements' and no accessible extension method 'Elements' accepting a first argument of type 'Selection' could be found (are you missing a using directive or an assembly reference?)"
Could someone with experience in fixing these errors please help me? I'm new to this and would greatly appreciate your assistance.
sragan  in reply to: tamsann
Formatutils is an obsolete command that "Formats a number with units into a string based on the units formatting settings for a document."
You will have to find another way to do this.
FormatUtils Members (revitapidocs.com)
https://www.revitapidocs.com/2015/b4779336-e429-0b51-8c0e-63b5657f1810.htm
reylorente1  in reply to: sragan
Hola,aqui te dejo UserMepCalculation2024,no obtante, el Revit SDK 2024,tiene un Complemento(Addin) llamado NetworkPressureLossReport, que te puede servir tambien.
Espero que te ayude,y suerte
Hello, here I leave you UserMepCalculation2024:
UserMepCalculation2024.rar
/Users/jta/a/doc/revit/tbc/git/a/zip/UserMepCalculation2024.rar
however, the Revit SDK 2024 has a Complement (Addin) called NetworkPressureLossReport, which can also be useful for you:
> This addin sample shows how to access the MEP analytical model data and traverse the network. The flow and pressure loss results are exported to a csv file or displayed in Analysis Visualization Framework (AVF).
I hope it helps you, and good luck

####<a name="7"></a> Wastewater Pipe Calculation

- Wastewater pipe calculation
https://forums.autodesk.com/t5/revit-api-forum/watsewater-pipe-calculation/m-p/12075059

####<a name="8"></a> The Password Game

- The Password Game
https://neal.fun/password-game/
/Users/jta/a/doc/revit/tbc/git/a/img/passwordgame.png
https://autodesk.slack.com/archives/C02NW42JP/p1688127875185579
Chris Blocher
A little Friday frustration fun, in a geeky sort of way. https://neal.fun/password-game/. I made it to the 11th rule before I gave up:wink: (edited)
Please choose a password (68 kB)
https://neal.fun/password-game/
Phillip Doup
I made it to the whole "find a youtube video of X mins and Y seconds" one and gave up.
Jeremy Tammik
nice game! my 11th rule seems to be different from yours. i gave up at this point too...



**Question:**

**Answer 1:**

<pre class="prettyprint">

</pre>

<center>
<img src="img/.png" alt="" title="" width="100"/> <!-- Pixel Height: 1,064 Pixel Width: 1,026 -->
</center>
